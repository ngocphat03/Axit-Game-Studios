# Axit Workspace Active State

Updated: 2026-08-11
Status: M5-designed-awaiting-target-bootstrap

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

Selection rationale:

```text
.axit/milestones/M5-project-bootstrap-knowledge-plane/selection-review.md
```

M5 is deliberately selected ahead of M4. M4 is deferred until a real accepted workspace contains at least two interacting Systems with an executable boundary. Do not manufacture a second System or cross-system benchmark.

## Promotion state

```text
M1 — Unity Runtime Binding: HUMAN_PROMOTED
M2 — Autonomous Bounded Development: HUMAN_PROMOTED
M3 — Unity Execution Coverage: HUMAN_PROMOTED
M4 — Cross-System Workspace: DEFERRED_WAITING_REAL_SECOND_SYSTEM
M5 — Project Bootstrap & Knowledge Plane: CURRENT_DESIGNED_AWAITING_TARGET_BOOTSTRAP
M6 — Axit-Code Productization: NOT_AUTHORIZED
```

## M5 target

```text
repository: ngocphat03/Axit-Code
canonical ref: release
execution root: trusted writable local Axit-Code checkout root
local path: intentionally unresolved
```

Do not guess the Axit-Code local path. Do not perform Axit-Code product edits from Axit-Game-Studios or through GitHub/cloud writes as a substitute for a writable target workspace.

The long M5 target run must start from a fresh trusted Codex session at the Axit-Code root after target-local project model routing is present before session start.

## Model / cost policy

Canonical Game-Studios policy:

```text
.axit/policies/model-routing.md
```

M5 required allocation:

```text
primary orchestrator = gpt-5.6-sol / xhigh
all child lanes       = gpt-5.6-luna / medium
```

This includes explorers, bootstrap authors, implementation workers, test/build lanes, recovery agents, independent verifiers, closure verifiers, and report authors.

No silent child escalation. Only an explicit current human instruction may authorize a bounded override. M5 expected model override count is zero.

Because project-scoped Codex defaults are loaded at session start, target model routing must be prepared before launching the long Axit-Code session; changing it after children have spawned does not prove compliance.

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

The remaining five Unity capabilities stay unbound until demonstrated need. M5 must not expand Unity Runtime Bindings merely because it is the current milestone.

## Next action

Prepare the M5 target-local bootstrap in the user's Axit-Code checkout, then start a fresh trusted Codex session from the Axit-Code repository root and execute the transferred M5 runbook continuously.

Do not start M5 product work from the current Game-Studios root. Do not start M4 or M6 automatically.
