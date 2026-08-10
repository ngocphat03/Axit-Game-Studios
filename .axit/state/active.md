# Axit Workspace Active State

Updated: 2026-08-10
Status: M3-ready-for-local-execution

## Current milestone

```text
M3 — Unity Execution Coverage
```

Execution plan:

```text
.axit/milestones/M3-unity-execution-coverage/plan.md
```

## Promotion state

### M1 — Unity Runtime Binding

Status: **HUMAN_PROMOTED**.

Closure artifacts:

```text
.axit/milestones/M1-unity-runtime-binding/report.md
.axit/milestones/M1-unity-runtime-binding/retrospective.md
```

### M2 — Autonomous Bounded Development

Status: **HUMAN_PROMOTED** on 2026-08-10.

Durable artifacts:

```text
.axit/milestones/M2-autonomous-bounded-development/report.md
.axit/milestones/M2-autonomous-bounded-development/retrospective.md
.axit/milestones/M2-autonomous-bounded-development/scenario-manifest.md
.axit/milestones/M2-autonomous-bounded-development/promotion-review.md
```

M2 does not need a full rerun.

## Stable Unity binding before M3

Reviewed active binding:

```text
.axit/bindings/unity-client/coplaydev-unity-mcp.yaml
```

Mapped capabilities remain exactly:

- `unity.prefab.inspect`
- `unity.serialized-fields.inspect`
- `unity.playmode.verify`

Still unbound at M3 start:

- `unity.project.inspect`
- `unity.compile`
- `unity.tests.run`
- `unity.scene.inspect`
- `unity.component.inspect`
- `unity.console.inspect`

M3's first demonstrated gap is `unity.compile`. Do not bind the other five speculatively.

## QuickGun System boundary

System:

```text
unity-client
```

Local source root:

```text
src/QuickGun-MVP
```

Canonical System repository metadata:

```text
repository: ngocphat03/QuickGun-MVP
canonical ref: release
```

Resolve the current System commit during evidence acquisition. A System may have its own Git history even when mounted under the root Workspace.

Evidence provenance classes are now:

```text
workspace-canonical-pushed
system-canonical-pushed
local-or-separately-tracked
ephemeral-runtime
```

## Pre-M3 hardening completed

- M2 human promotion recorded.
- Project primary reasoning config changed from `max` to `xhigh`.
- Sub-agent default remains `gpt-5.6-sol / max`.
- M3 Phase 0 must verify effective primary `gpt-5.6-sol / xhigh` in a fresh trusted session; configured intent alone is insufficient.
- Runtime Binding validation Case 8 now requires current full runtime target identity to be resolved before path-addressed runtime acquisition/mutation. Do not infer the complete scene path from prefab hierarchy.
- Milestone closure/templates now support System-aware Git provenance.
- Post-promotion active state is intentionally compact; completed milestone details live in their durable artifacts.

## Workspace safety

Current configuration:

```yaml
safety:
  check_git_status: false
```

Git status is not a readiness gate. This does not authorize reset, clean, revert, destructive deletion, or blind overwrite.

Unity MCP remains manually configured/user-owned. Axit may use an already ready transport but must not install/configure/start/repair it.

## M3 readiness requirement

Before any M3 mapping/edit work, verify:

- fresh effective primary = `gpt-5.6-sol / xhigh`;
- sub-agent execution available;
- intended QuickGun editor/project reachable through user-configured Unity MCP;
- safe initial editor state;
- current Unity version/project identity;
- current QuickGun System repo/ref/commit when available;
- existing M1 three-capability binding still valid;
- `unity.compile` is still unbound before live transport discovery.

## Next action

Start a fresh trusted Codex session at repository root and execute the current M3 plan continuously until `MILESTONE_DONE`, `FAILED`, or a declared `HARD_BLOCKER`.

Do not start M4 automatically.
