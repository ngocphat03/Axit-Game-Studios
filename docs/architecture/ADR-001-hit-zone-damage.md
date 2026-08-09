# ADR-001: Configure damage multipliers per hit zone

- Status: Accepted
- Date: 2026-08-09
- Scope: QuickGun combat

## Context

QuickGun already routes projectile collisions through `DamageableBodyPart`, but every body part forwards identical damage. The character prefab exposes separate colliders for the head, hair, and body, so the collision data needed for meaningful hit zones already exists.

## Decision

Each `DamageableBodyPart` owns a serialized, non-negative damage multiplier. A pure `DamageCalculator` applies that multiplier and converts the result to integer health damage. Positive hits deal at least one damage, while invalid non-positive inputs deal zero.

The shared character prefab configures:

- Head: 2x
- Hair: 2x
- Body: 1x

Both player and bot use this prefab, so the same combat rule applies symmetrically.

## Consequences

- Designers can rebalance hit zones in the prefab without code changes.
- Damage rules are testable without loading a Unity scene.
- Critical hits currently use the existing hit flash plus a combat log; a dedicated UI/audio cue can be added later without changing the damage contract.
