# QuickGun-MVP Active State

Updated: 2026-08-09
Status: project-layer-bootstrap

## Current task

Bootstrap QuickGun-MVP as the first concrete Axit Project Layer without adding project-specific Profiles, Skills, or Workflows.

## Confirmed project facts

- Engine family: Unity.
- Language: C#.
- QuickGun is the first live project used to validate Axit Core behavior.
- The shared hit-damage path currently validated is `Bullet -> DamageableBodyPart -> DamageCalculator -> BaseEntity`.
- Player and Bot use the same validated damage path.
- The validated damage order is hit-zone multiplier -> armor reduction -> health reduction.
- Head/Hair 2x and Body 1x behavior has deterministic regression coverage in the live workspace.
- Non-positive base damage resolves to zero; the `baseDamage == 0` regression was fixed and independently verified.
- Armor behavior was validated with an effective range of 0.0 through 0.8.
- The medium vertical-slice armor test completed through `bounded-change` with 15 deterministic tests passing.

## Current evidence limitations

- Full Unity build/runtime validation was unavailable in the live test because generated/package-cache sources were incomplete in that environment.
- The missing Unity build was supporting rather than required evidence for the tested deterministic armor criteria.
- QuickGun source changes from the live test are present in the user's local workspace but are not yet visible on the current remote branch.

## Unconfirmed routing facts

- Exact Unity version.
- Player-count range.
- Target platforms.

Do not infer these values. Update `.axit/project.yaml` after reading them from the local Unity project or accepted project documentation.

## Active project extensions

- Profiles: none.
- Skills: none.
- Workflows: none beyond shared Core.
- Knowledge: none.

## Next action

1. Pull this branch into the local QuickGun workspace.
2. Start a fresh Codex session with the working directory inside `src/QuickGun-MVP`.
3. Confirm Codex resolves the nested `AGENTS.md`, `.axit/project.yaml`, project rules, and architecture registry without preloading unrelated Axit files.
4. Run another real QuickGun task and observe whether a repeated project-specific knowledge or procedure gap appears.
5. Only then consider the first Project Layer Knowledge entry or Skill.
