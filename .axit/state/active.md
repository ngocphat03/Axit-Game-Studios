# Axit Workspace Active State

Updated: 2026-08-09
Status: capability-v1-rerun-cases-1-2

## Current task

Re-run Capability v1 semantic Cases 1 and 2 after tightening declared capability-id discipline, while keeping Core v1 and root Workspace/System routing frozen.

## Stable foundations

### Core v1

- Profiles: `game-designer`, `technical-architect`, `implementation-engineer`, `quality-verifier`.
- Skills: `implement-change`, `verify-change`.
- Workflow: `bounded-change`.
- Status: frozen/stable unless repeated live use demonstrates another reusable procedural or responsibility gap.

### Workspace/System routing v1

- Codex normally starts from repository root.
- Root `AGENTS.md` routes through `.axit/workspace.yaml`.
- `src/QuickGun-MVP` resolves to System `unity-client`.
- Single-System tasks load only relevant System context.
- Cross-System requests load root integration/architecture context when needed.
- Missing backend/CMS contracts remain explicit unknowns instead of inferred provider behavior.
- Live validation cases 1-4 are recorded as PASS in `.axit/checklists/root-routing-validation.md`.
- Live cross-System verification remains deferred until at least two real Systems exist.

## Capability v1 direction

- Capability Spec: `.axit/specs/capability-spec-v1.md`.
- Reusable catalog root: `.axit/capabilities/`.
- First semantic set: `.axit/capabilities/unity/evidence.yaml`.
- Unity client capability routing: `.axit/systems/unity-client/capabilities.yaml`.
- Current transport binding status: **unbound**.

The semantic layer currently defines exactly these Unity capability ids:

- `unity.project.inspect`
- `unity.compile`
- `unity.tests.run`
- `unity.scene.inspect`
- `unity.prefab.inspect`
- `unity.component.inspect`
- `unity.serialized-fields.inspect`
- `unity.console.inspect`
- `unity.playmode.verify`

## Capability invariants

- Capability ids describe semantic intent, not MCP/CLI/provider command names.
- Only ids explicitly declared by the affected System's active capability sets may be named as Axit Capabilities.
- Do not invent, alias, rename, abbreviate, or synthesize capability ids during planning.
- If no declared Capability matches an evidence need, describe the need in ordinary language and use legitimate project evidence when sufficient; otherwise report a capability gap.
- Ordinary project evidence such as standalone deterministic tests or source inspection is not automatically an Axit Capability.
- `unity.tests.run` must not be used as a label for standalone deterministic C# tests unless the selected evidence is actually a Unity test suite represented by that Capability contract.
- Capability output is evidence, not PASS/FAIL/BLOCKED.
- `verify-change` still owns REQUIRED/SUPPORTING classification and final verdict semantics.
- Declaring a capability does not grant execution permission.
- A declared capability may be unavailable because no runtime binding or required environment exists.
- Unavailable acquisition is missing evidence, not automatic proof of product failure.
- Core Skills/Workflow were not modified to introduce or repair Capability v1.

## First Capability live pass

Observed on 2026-08-09:

- Case 1: reasoning was mostly correct, but ordinary deterministic/source evidence was not clearly separated from Axit Capability ids.
- Case 2: failed declared-ID discipline by inventing `player_prefab.resolve_asset`, `prefab.inspect_serialized_component`, and `configuration.compare_to_accepted_contract`.
- Case 3: PASS — runtime Play Mode criterion correctly selected `unity.playmode.verify` as REQUIRED and returned BLOCKED while unbound.
- Case 4: PASS — observed compiler failure was distinguished from unavailable acquisition.
- Case 5: PASS — Capability declaration did not grant permission or own the verification verdict.

The Spec, root router, capability README, Unity System capability sidecar, and validation checklist were tightened after this pass.

## Current System

- `unity-client` -> `src/QuickGun-MVP`.
- Runtime family: Unity / C#.
- Exact Unity version, target platforms, and player-count range remain intentionally unconfirmed until read from real local project evidence.

No backend, CMS, or service System is registered yet because no confirmed source boundary exists on this branch.

## Next actions

1. Start a fresh Codex session from repository root.
2. Re-run Case 1 from `.axit/checklists/unity-evidence-capability-validation.md` and confirm ordinary deterministic evidence is not mislabeled as a Unity Capability.
3. Re-run Case 2 and confirm only declared ids such as `unity.prefab.inspect`, `unity.component.inspect`, and `unity.serialized-fields.inspect` are named.
4. If both pass, promote Capability v1 semantic routing and select a small subset for the first real runtime binding.
5. Do not bind MCP/CLI transport until Cases 1-2 pass.
6. Do not add Core Skill #3, Workflow #2, or a broad Unity specialist catalog during this phase.
