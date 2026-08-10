# Axit Workspace Active State

Updated: 2026-08-11
Status: M5-repair-ready-awaiting-target-root

## Current milestone

```text
M5 — Project Bootstrap & Knowledge Plane
```

Control plan:

```text
.axit/milestones/M5-project-bootstrap-knowledge-plane/plan.md
```

Target bootstrap contract:

```text
.axit/milestones/M5-project-bootstrap-knowledge-plane/target-bootstrap.md
```

Runtime compatibility overlay:

```text
.axit/milestones/M5-project-bootstrap-knowledge-plane/runtime-compatibility.md
```

Selection rationale:

```text
.axit/milestones/M5-project-bootstrap-knowledge-plane/selection-review.md
```

M5 is deliberately selected ahead of M4. M4 remains deferred until a real accepted workspace contains at least two interacting Systems with an executable boundary.

## Promotion state

```text
M1 — Unity Runtime Binding: HUMAN_PROMOTED
M2 — Autonomous Bounded Development: HUMAN_PROMOTED
M3 — Unity Execution Coverage: HUMAN_PROMOTED
M4 — Cross-System Workspace: DEFERRED_WAITING_REAL_SECOND_SYSTEM
M5 — Project Bootstrap & Knowledge Plane: CURRENT_REPAIR_READY_AWAITING_TARGET_ROOT
M6 — Axit-Code Productization: NOT_AUTHORIZED
```

## M5 first launch result

The first M5 attempt stopped correctly before product work:

```text
Status: HARD_BLOCKER
Blocker: TARGET_WORKSPACE_NOT_READY
child lanes successfully launched: 0
product changes: none
Git publish actions: none
M6 actions: none
```

Observed causes:

- the Codex session was rooted at Axit-Game-Studios rather than Axit-Code;
- the accepted local Axit-Code path was intentionally unresolved;
- the current child runtime exposed Sol and Terra but not Luna.

This is a launch/setup-policy incident, not an M5 product failure. Do not rerun from the Game-Studios root.

## M5 target

```text
repository: ngocphat03/Axit-Code
canonical ref: release
execution root: trusted writable local Axit-Code checkout root
local path: selected by the user when opening the target checkout; do not guess
```

Do not perform Axit-Code product edits from Axit-Game-Studios or through GitHub/cloud writes as a substitute for a writable target workspace.

## Model / cost policy

Canonical policy:

```text
.axit/policies/model-routing.md
```

Accepted routing:

```text
primary orchestrator = gpt-5.6-sol / xhigh
child preferred      = gpt-5.6-luna / medium when supported
child compat fallback= gpt-5.6-terra / medium when Luna is unavailable
child Sol            = forbidden unless explicitly human-authorized
```

For the currently observed runtime, the compatible child route is:

```text
COMPAT_TERRA
model: gpt-5.6-terra
reasoning: medium
reason: LUNA_UNAVAILABLE
```

This is compliant and does not count as a human model override. If a future runtime exposes Luna, switch back to `PREFERRED_LUNA`.

Never silently fall back to child Sol. Child reasoning above medium also requires explicit human override.

Because project-scoped Codex defaults are loaded at session start, target model routing must be prepared before launching the fresh Axit-Code session.

## M5 authoritative target anchors

Reacquire current target truth at run time. Current known source hierarchy:

```text
docs/PLAN.md          = canonical product goal/scope/roadmap
README.md             = current repository summary
docs/architecture.md  = architecture detail subordinate to PLAN
package.json          = target-native workspace validation scripts
```

Current known repository validation entrypoint:

```text
npm run verify
```

Do not freeze these facts if the target repository has changed; current target files win.

## Stable promoted foundations carried into M5

- Core remains intentionally small: four Profiles, two Skills, one Workflow.
- Evidence-driven evolution; no speculative Profile/Skill/Workflow/Capability growth.
- Milestone automation is high inside one accepted milestone and stops for human promotion review.
- Closure verifier result must be persisted, followed by a fresh post-verdict consistency audit, before `MILESTONE_DONE`.
- Git status is not a universal readiness gate when workspace policy disables it; never reset/clean/revert unrelated work.
- Evidence provenance must distinguish canonical target source, generated bootstrap metadata, local-only execution evidence, and ephemeral runtime/process evidence.

## QuickGun / Unity foundation remains promoted

Active reviewed Unity mappings remain exactly:

```text
unity.prefab.inspect
unity.serialized-fields.inspect
unity.playmode.verify
unity.compile
```

The remaining five Unity capabilities stay unbound until demonstrated need. M5 must not expand Unity Runtime Bindings merely because it is current.

## Next action

1. Open/select the real local Axit-Code checkout.
2. Prepare target-local project Codex config before session start using the current compatible child route (`Terra / medium` unless Luna is demonstrably available).
3. Make the M5 plan plus runtime compatibility overlay locally readable as execution metadata.
4. Start a fresh trusted Codex session at the Axit-Code repository root.
5. Run M5 continuously.

Do not start M5 again from Game-Studios root. Do not start M4 or M6 automatically.
