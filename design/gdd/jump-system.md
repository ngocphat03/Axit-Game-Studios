# Game Design Document: Jump System

- **Document ID**: `GDD-JUMP-001`
- **System**: Jump, Airborne Movement & Terrain Traversal
- **Status**: Approved Design + Reverse-documented Prototype Snapshot
- **Version**: 1.1.0
- **Last Updated**: 2026-08-25
- **Target Platform**: CrazyGames — Unity WebGL
- **Target Input**: Keyboard & Mouse
- **Related Document**: [combat-system.md](combat-system.md)

> [!IMPORTANT]
> This document defines Jump only. Aerial Light Attack, Aerial Heavy Attack, and complete aerial combos are outside the current scope. The system exposes the states and events those mechanics will use later.

## Prototype implementation snapshot (2026-08-25)

The browser prototype currently uses the following playtest values. They supersede older prototype values but do not automatically replace every production target later in this document:

| Runtime behavior | Current value |
| :--- | :--- |
| Initial vertical velocity | `13.5 m/s` |
| Rising gravity | `24 m/s²` |
| Falling gravity | `32 m/s²` |
| Early-release cap | `25%` of initial jump velocity |
| Coyote time | `0.15 s` |
| Input buffer | `0.15 s` |
| Air-control multiplier | `75%` |

The animation chain uses ExplosiveLLC clips and explicit phase handling: `Jump` while rising, `Fall` only after vertical velocity becomes negative, then `Land` on grounded contact. Because Three.js does not interpret Unity `.meta` clip ranges, the prototype recreates them at runtime: Jump frames `0–18`, Land frames `21–34`, and looping Fall frames `0–32`. No double jump or jump I-Frames are implemented.

---

---

## 1. Overview

Jump is a core part of movement in the game's 3D environment. It lets the player traverse low obstacles, short gaps, platforms, and uneven terrain; avoid attacks whose hitboxes remain close to the ground; and enter an airborne state that can support future Light and Heavy aerial attacks.

The system uses only `Space`, requires no stamina or cooldown, and includes hidden input assistance so it remains responsive for the target audience. It replaces the Dash and Dash I-Frame mechanics currently described in `combat-system.md`.

### 1.1. Scope

The first version includes:

- Grounded jump using `Space`.
- Variable jump height based on a short button hold.
- Coyote time and jump input buffering.
- Limited horizontal control while airborne.
- Reliable interaction with terrain, slopes, ceilings, and platform edges.
- Spatial avoidance of low attacks without invulnerability.
- Airborne state and event hooks for future aerial combat.

The first version does not include:

- Double jump.
- Wall jump or wall run.
- Ledge grab or climbing.
- Glide.
- Fall damage.
- Jump stamina or jump cooldown.
- Complex moving platforms.
- A complete aerial moveset or aerial combo rules.

---

## 2. Player Fantasy

The player should feel light, responsive, and in control of their position in a 3D space. Jumping should be easy enough to use without precise timing while still allowing the player to intentionally choose a low hop, a full jump, a new elevation, or an airborne attack opportunity.

The system should make a simple input produce confident movement. It must not feel like a precision-platforming challenge or require the player to manage an additional resource.

---

## 3. Input Mapping

| Action | Input | Result |
| :--- | :--- | :--- |
| Jump | `Space` pressed | Starts a grounded jump when allowed |
| Variable Height | Hold `Space` briefly | Extends the upward phase up to the configured maximum |
| Air Movement | `WASD` | Adjusts horizontal direction while airborne |
| Air Light Request | `LMB` while airborne | Sends an `AirLightRequested` event |
| Air Heavy Request | `RMB` while airborne | Sends an `AirHeavyRequested` event |

`LMB` and `RMB` requests are interface hooks only in this version. Their animations, damage, hitboxes, cancel rules, and combo chains will be defined by the future aerial combat design.

---

## 4. Detailed Rules

### 4.1. Jump Eligibility

The player may begin a jump when all of the following conditions are true:

1. The player is grounded, or left a valid grounded surface no more than `CoyoteTime` seconds ago.
2. The player is not in `Stunned`, `KnockedDown`, or `Dead` state.
3. The current combat action permits a transition to `JumpRising`.
4. There is sufficient clearance above the player's capsule to begin the jump.

When the jump is accepted:

1. The character enters `JumpRising`.
2. The configured upward velocity is applied.
3. Existing horizontal movement is preserved.
4. The character remains facing the camera yaw.
5. The system emits `JumpStarted`.

Jump grants no I-Frames and does not make the player immune to damage.

### 4.2. Variable Jump Height

- Tapping and releasing `Space` early produces a lower jump.
- Holding `Space` extends the upward phase until `VariableJumpHoldTime` expires.
- Releasing `Space` before the maximum hold time increases downward acceleration so the character reaches the apex sooner.
- Holding `Space` beyond the maximum hold time has no additional effect.
- The player is not required to release and press `Space` again to enter the falling state.

### 4.3. Coyote Time

After walking or running off a valid ledge, the player retains jump eligibility for `0.12s`.

Coyote time does not apply after:

- Being launched by an enemy.
- Being knocked from a platform.
- Completing an aerial attack bounce or other forced airborne action.
- Manually jumping.

### 4.4. Jump Input Buffer

If `Space` is pressed while the player is airborne and the player lands within `0.12s`, the system performs a new jump immediately after confirming a valid landing.

The buffered command:

- Stores only one jump request.
- Expires after `JumpBufferTime`.
- Does not create a double jump.
- Is cleared by `Stunned`, `KnockedDown`, or `Dead`.

### 4.5. Air Control

- `WASD` adjusts horizontal travel relative to camera yaw.
- Air acceleration uses `70%` of grounded directional authority.
- Horizontal speed cannot exceed the normal grounded movement limit unless another system explicitly supplies external velocity.
- Releasing all movement input preserves horizontal momentum and allows it to decay gradually.
- Reversing direction in the air is permitted but cannot happen instantaneously.
- The character continues to face camera yaw while airborne.

### 4.6. Landing

A landing is valid when the player's capsule contacts a walkable surface while descending.

On a valid landing:

1. Downward velocity is cleared.
2. The character enters `Landing`.
3. The system emits `Landed` with the pre-impact vertical velocity.
4. A valid buffered jump is consumed immediately, if present.
5. Otherwise the character returns to `Grounded` without a forced recovery delay.

The MVP has no fall damage and no hard-landing lockout. Landing feedback may scale visually or audibly with impact velocity, but it must not delay player input.

---

## 5. States and Transitions

| Current State | Trigger | Next State | Notes |
| :--- | :--- | :--- | :--- |
| `Grounded` | Valid `Space` press | `JumpRising` | Emits `JumpStarted` |
| `Grounded` | Walk off ledge | `Falling` | Starts coyote-time window |
| `JumpRising` | Upward velocity reaches zero | `Falling` | Emits `ApexReached` |
| `JumpRising` | Head contacts ceiling | `Falling` | Upward velocity is cleared |
| `JumpRising` | Valid `LMB`/`RMB` request | `AirAttack` | Exact attack is defined later |
| `Falling` | Valid `LMB`/`RMB` request | `AirAttack` | Exact attack is defined later |
| `Falling` | Contacts walkable ground | `Landing` | Emits `Landed` |
| `AirAttack` | Attack ends while airborne | `Falling` | Uses remaining airborne velocity rules |
| `AirAttack` | Contacts walkable ground | `Landing` | Landing behavior may later be attack-specific |
| `Landing` | Buffered jump exists | `JumpRising` | Consumes the buffer |
| `Landing` | No buffered jump | `Grounded` | No mandatory landing delay |
| Any active state | Stun, knockdown, or death | `Disabled` | Clears buffered input |
| `Disabled` | Control restored while grounded | `Grounded` | Normal movement resumes |
| `Disabled` | Control restored while airborne | `Falling` | Gravity remains active |

```text
Grounded
   | Space
   v
JumpRising ---- apex/ceiling ----> Falling
   |                                  |
   | LMB/RMB                          | ground contact
   v                                  v
AirAttack ------------------------> Landing
                                      |
                                      v
                                   Grounded
```

---

## 6. Terrain Interaction

### 6.1. Steps

- Walkable steps up to `0.35m` are traversed automatically.
- The player should not be required to jump over ordinary decorative ground variation.
- Obstacles above the step limit require jumping or another valid route.

### 6.2. Platforms and Gaps

- Jump supports short gaps and clearly readable height changes.
- MVP platforms must be large enough to support the full player capsule without precision edge balancing.
- The MVP does not require ledge grabbing.
- If the full capsule cannot occupy the landing position, the character does not snap onto the surface.

### 6.3. Slopes

- A surface at or below `MaxWalkableSlope` may be treated as ground.
- A steeper surface is not valid ground and cannot reset Jump.
- The player slides or falls from an invalid slope instead of standing or repeatedly entering grounded state.

### 6.4. Ceiling Collision

If the player capsule contacts a ceiling while rising:

1. Upward velocity is immediately cleared.
2. The player enters `Falling`.
3. The player receives no damage.
4. The capsule must not penetrate or become trapped in the ceiling.

### 6.5. Platform Edges

Ground detection must remain stable near platform edges. Contact with a wall or the side of a platform does not count as grounded contact and does not restore Jump.

---

## 7. Combat Interaction

### 7.1. Spatial Evasion

Jump avoids an attack only when the player's hurtbox is physically outside that attack's hitbox during its active damage frame.

Attacks intended to be jumpable include:

- Ground shockwaves.
- Low horizontal sweeps.
- Ground traps with low vertical hitboxes.

Attacks that may still hit an airborne player include:

- Projectiles.
- Torso-height horizontal attacks.
- Downward strikes.
- Spherical explosions.
- Any attack whose hitbox overlaps the airborne hurtbox.

Enemy telegraphs must communicate whether an attack should be jumped, moved away from, or avoided laterally.

### 7.2. Attack Transitions

- Ground attacks may transition into Jump only during cancel windows explicitly allowed by the Combat System.
- An attack's `Active` phase does not automatically become jump-cancellable.
- Air Light and Air Heavy input requests are accepted during `JumpRising` and `Falling` unless the player is disabled.
- The future aerial combat specification owns attack animation, movement, hitbox, damage, recovery, landing, and combo continuation rules.

### 7.3. Damage and Control Effects

- Jump provides no invulnerability.
- Receiving a normal hit while airborne does not automatically restore Jump.
- Knockback, launch, and stun may replace the player's current airborne velocity.
- Forced airborne states do not count as a manual Jump and do not enable another Jump.

---

## 8. Formulas

### 8.1. Initial Jump Velocity

The `InitialJumpVelocity` formula is defined as:

`InitialJumpVelocity = 2 x JumpHeight / TimeToApex`

| Variable | Symbol | Type | Normal Range | Description |
| :--- | :---: | :--- | :--- | :--- |
| Initial jump velocity | `v0` | float | Derived | Upward velocity applied at Jump start |
| Jump height | `h` | float | `1.2–1.8m` | Maximum full-hold height above takeoff point |
| Time to apex | `t` | float | `0.32–0.45s` | Time required to reach maximum height |

Using the default values:

`InitialJumpVelocity = 2 x 1.5 / 0.38 = 7.89m/s`

### 8.2. Base Gravity

The `JumpGravity` formula is defined as:

`JumpGravity = -2 x JumpHeight / TimeToApex^2`

Using the default values:

`JumpGravity = -2 x 1.5 / 0.38^2 = -20.78m/s^2`

Early button release may apply a stronger downward multiplier during the remaining ascent. The exact release multiplier is a tuning value and must not alter the full-hold maximum height.

---

## 9. Tuning Knobs

| Knob | Default | Safe Range | Purpose |
| :--- | :---: | :---: | :--- |
| `JumpHeight` | `1.5m` | `1.2–1.8m` | Full-hold maximum jump height |
| `TimeToApex` | `0.38s` | `0.32–0.45s` | Controls jump responsiveness and floatiness |
| `VariableJumpHoldTime` | `0.18s` | `0.12–0.25s` | Maximum useful Space hold duration |
| `EarlyReleaseGravityMultiplier` | `2.0x` | `1.5–3.0x` | Reduces height after an early release |
| `CoyoteTime` | `0.12s` | `0.08–0.15s` | Forgiveness after leaving a ledge |
| `JumpBufferTime` | `0.12s` | `0.08–0.15s` | Forgiveness before landing |
| `AirControlRate` | `70%` | `55–85%` | Directional authority while airborne |
| `StepHeight` | `0.35m` | `0.25–0.40m` | Height automatically traversed without Jump |
| `MaxWalkableSlope` | `45°` | `40–50°` | Maximum angle treated as grounded terrain |
| `GroundCheckDistance` | `0.08m` | `0.04–0.12m` | Ground detection tolerance beneath the capsule |

These values are provisional and require playtesting with the final player controller, camera, terrain scale, and WebGL frame-rate targets.

---

## 10. Events and System Interfaces

| Event/Data | Direction | Consumer/Purpose |
| :--- | :--- | :--- |
| `JumpStarted` | Output | Animation, audio, combat state |
| `ApexReached` | Output | Animation and future aerial combo timing |
| `FallingStarted` | Output | Animation and combat state |
| `Landed(impactVelocity)` | Output | Animation, audio, VFX, combat recovery |
| `IsGrounded` | Output | Movement, combat, AI targeting, animation |
| `IsAirborne` | Output | Combat, animation, hurtbox rules |
| `AirLightRequested` | Output | Future aerial combat resolver |
| `AirHeavyRequested` | Output | Future aerial combat resolver |
| `MovementDisabled` | Input | Stun, knockdown, death, modal/game state |
| `ExternalVelocity` | Input | Enemy launch, knockback, future aerial attacks |
| `JumpCancelPermission` | Input | Combat state/cancel-window rules |

Dependencies are provisional because `design/gdd/systems-index.md` does not yet exist.

---

## 11. Camera Requirements

- The camera does not reproduce every small vertical movement one-to-one.
- Low jumps retain a stable horizon and framing.
- When the player lands on a platform at a different elevation, the camera smoothly recenters.
- Jumping does not change FOV.
- A normal jump produces no camera shake.
- Landing feedback must remain subtle and must not obscure combat telegraphs.
- Camera obstruction handling remains active while airborne.

---

## 12. Visual and Audio Requirements

For the MVP:

- Jump startup uses one clear animation pose.
- Rising, falling, and landing states must be visually distinguishable.
- A small dust effect may play on takeoff and landing.
- Dust effects must use a low particle count suitable for WebGL.
- Jump and landing use short, readable SFX.
- No trail, glow, screen distortion, or full-screen effect is required for a normal Jump.
- Animation and effects must not hide enemy telegraphs.

---

## 13. Performance Requirements

- The system must support Unity WebGL delivery on CrazyGames.
- Ground and ceiling checks must use a bounded, constant number of collision queries per player update.
- Jump creates no per-frame particle objects or temporary gameplay objects.
- Takeoff and landing feedback should be pooled.
- Ground detection must remain stable at variable browser frame rates.
- All time-based calculations use frame delta time rather than frame counts.

---

## 14. Edge Cases

- **If the player presses Jump with insufficient head clearance**: reject the Jump and remain grounded.
- **If the player hits a ceiling while rising**: clear upward velocity and enter `Falling` without damage.
- **If the player presses Jump repeatedly while airborne**: retain at most one buffered request; do not double jump.
- **If the player lands on a non-walkable slope**: do not enter `Grounded`; continue sliding or falling.
- **If ground contact is lost for one unstable collision update on an otherwise continuous surface**: ground tolerance prevents visible state flicker.
- **If the player is launched by an enemy**: enter a forced airborne state without coyote time or a refreshed Jump.
- **If Jump, Stun, and Death occur in the same update**: `Death` has highest priority, then `Stun`, then `Jump`.
- **If Jump and a valid buffered combat input occur simultaneously**: Jump begins first; the combat system may consume the attack request after entering the airborne state.
- **If the player falls below the valid play area**: the Arena/Checkpoint system owns recovery or defeat behavior.
- **If the player lands while an Air Attack is active**: the future aerial combat specification owns the attack-specific landing result; until then, transition safely to `Landing` and clear the placeholder attack.

---

## 15. Acceptance Criteria

- [ ] **GIVEN** the player is on valid ground, **WHEN** `Space` is pressed, **THEN** the character enters `JumpRising` and receives the configured upward velocity during the same simulation update.
- [ ] **GIVEN** the player released `Space` early, **WHEN** the character is still rising, **THEN** the jump reaches a lower apex than a full-hold jump.
- [ ] **GIVEN** the player left a ledge no more than `0.12s` ago, **WHEN** `Space` is pressed, **THEN** Jump still begins.
- [ ] **GIVEN** the player will land within `0.12s`, **WHEN** `Space` is pressed before contact, **THEN** one Jump begins immediately after the valid landing.
- [ ] **GIVEN** the player is airborne, **WHEN** `WASD` changes direction, **THEN** horizontal direction responds without exceeding the configured speed limit.
- [ ] **GIVEN** the player contacts a ceiling while rising, **WHEN** collision is confirmed, **THEN** upward velocity becomes non-positive and the player enters `Falling` without penetration.
- [ ] **GIVEN** the player contacts a valid walkable surface while descending, **WHEN** landing is confirmed, **THEN** the system emits `Landed` once and returns to a controllable grounded state.
- [ ] **GIVEN** the player contacts a surface steeper than `MaxWalkableSlope`, **WHEN** ground detection runs, **THEN** the surface does not reset Jump eligibility.
- [ ] **GIVEN** a low ground shockwave is active, **WHEN** the player's hurtbox is above its hitbox, **THEN** the player receives no damage.
- [ ] **GIVEN** a projectile overlaps the player's airborne hurtbox, **WHEN** no separate immunity is active, **THEN** the player receives damage normally.
- [ ] **GIVEN** the player is airborne and controllable, **WHEN** `LMB` or `RMB` is pressed, **THEN** the correct aerial attack request is emitted once.
- [ ] **GIVEN** the player is stunned, knocked down, or dead, **WHEN** `Space` is pressed, **THEN** no Jump begins and buffered Jump input is cleared.
- [ ] Repeated jumps, falls, ceiling contacts, and platform-edge transitions do not cause visible grounded-state flicker, capsule penetration, or uncontrolled memory allocation.

---

## 16. Required Follow-Up Changes

After this draft is approved and validated:

1. Remove Dash, Dash I-Frames, Dash Recovery Cancel, and Dash tuning values from `combat-system.md`.
2. Replace `Spacebar Dash` input references with Jump.
3. Update enemy and boss telegraphs to identify low attacks that can be jumped.
4. Add the Jump System to the future Systems Index.
5. Create a separate aerial combat design before implementing Air Light and Air Heavy attacks.

---

## 17. Open Questions

- Should walking off a ledge allow an aerial attack immediately, or only after a manual Jump?
- Should future Air Heavy attacks suspend falling briefly or preserve full gravity?
- Will any moving platforms be included in the first public CrazyGames build?
- What minimum target device and WebGL frame-rate budget will define the final movement tolerances?
