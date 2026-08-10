# Axit Workspace Active State

Updated: 2026-08-10
Status: M3-human-promoted-awaiting-next-milestone-design

## Current milestone

```text
none active
```

M3 — Unity Execution Coverage is **HUMAN_PROMOTED**. M4 has not started and no next milestone execution plan is currently authorized.

If the user asks to continue the roadmap before a new milestone is deliberately accepted, stop at planning/review rather than inferring M4 execution from the mockup.

## Promoted milestone pointers

```text
M1 -> .axit/milestones/M1-unity-runtime-binding/
M2 -> .axit/milestones/M2-autonomous-bounded-development/
M3 -> .axit/milestones/M3-unity-execution-coverage/
```

M3 human promotion record:

```text
.axit/milestones/M3-unity-execution-coverage/promotion-review.md
```

## Active Unity binding

Reviewed definition:

```text
.axit/bindings/unity-client/coplaydev-unity-mcp.yaml
```

Active mappings are exactly:

- `unity.prefab.inspect`
- `unity.serialized-fields.inspect`
- `unity.playmode.verify`
- `unity.compile`

Remaining unbound capabilities are exactly:

- `unity.project.inspect`
- `unity.tests.run`
- `unity.scene.inspect`
- `unity.component.inspect`
- `unity.console.inspect`

Binding `active` means reviewed operation definition, not current transport connectivity.

## Model / cost policy

Canonical policy:

```text
.axit/policies/model-routing.md
```

Defaults:

```text
primary orchestrator = gpt-5.6-sol / xhigh
all child lanes       = gpt-5.6-luna / medium
```

The child rule includes explorers, workers, Unity/MCP evidence lanes, repair/recovery agents, independent verifiers, closure verifiers, report authors, and custom sub-agents.

Do not silently escalate a child to Terra/Sol or above `medium`. Only an explicit current human instruction may authorize a bounded override.

Historical M3 used Sol/max children; do not reinterpret that historical run as Luna/medium.

## Stable execution rules

- Root `.axit/workspace.yaml` is the Workspace router.
- `safety.check_git_status: false`; Git status is not a readiness gate.
- Unity MCP setup remains manually configured/user-owned.
- Path-addressed runtime operations must resolve current full runtime identity; do not infer complete scene paths from prefab hierarchy.
- Unity 6 compile evidence requires fresh-cycle correlation, a distinct later terminal observation, exact identity re-resolution, and complete diagnostics paging.
- Milestone closure must persist the closure-verifier result and pass a post-verdict consistency audit before `MILESTONE_DONE`.
- Detailed completed-milestone history belongs in report/retrospective/manifest/promotion-review, not this active file.

## QuickGun System

```text
system: unity-client
source: src/QuickGun-MVP
canonical repository: ngocphat03/QuickGun-MVP
canonical ref: release
M3 promoted production fix commit: e2e1b1b6f3b0720d91e51def6b610f5714e17c52
```

Resolve moving repository heads again when future evidence is acquired.

## Next action

User + assistant decide the smallest valuable next milestone.

Do not auto-start M4. If a real second interacting System does not yet exist with accepted boundaries, reconsider milestone ordering rather than manufacturing a cross-system benchmark.
