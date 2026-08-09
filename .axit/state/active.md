# Axit Workspace Active State

Updated: 2026-08-09
Status: capability-v1-live-validation

## Current task

Validate Axit Capability Spec v1 and the first reusable Unity evidence capability set while keeping Core v1 and root Workspace/System routing frozen.

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

The semantic layer currently defines:

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
- Capability output is evidence, not PASS/FAIL/BLOCKED.
- `verify-change` still owns REQUIRED/SUPPORTING classification and final verdict semantics.
- Declaring a capability does not grant execution permission.
- A declared capability may be unavailable because no runtime binding or required environment exists.
- Unavailable acquisition is missing evidence, not automatic proof of product failure.
- Core Skills/Workflow were not modified to introduce Capability v1.

## Current System

- `unity-client` -> `src/QuickGun-MVP`.
- Runtime family: Unity / C#.
- Exact Unity version, target platforms, and player-count range remain intentionally unconfirmed until read from real local project evidence.

No backend, CMS, or service System is registered yet because no confirmed source boundary exists on this branch.

## Next actions

1. Run `.axit/checklists/unity-evidence-capability-validation.md` from a fresh root Codex session.
2. Confirm capability selection is criterion-driven rather than catalog-driven.
3. Confirm explicit Play Mode criteria become BLOCKED when required runtime evidence is unavailable, without turning missing bindings into FAIL.
4. After semantic cases pass, bind only a small subset of Unity capabilities to one real transport.
5. Use the first binding for a live vertical slice combining deterministic evidence with prefab/serialized/runtime evidence.
6. Do not add Core Skill #3, Workflow #2, or a broad Unity specialist catalog during this phase.
