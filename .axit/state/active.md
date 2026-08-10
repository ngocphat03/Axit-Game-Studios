# Axit Workspace Active State

Updated: 2026-08-11
Status: M5-bootstrap-materialized-awaiting-fresh-target-session

## Current milestone

```text
M5 — Project Bootstrap & Knowledge Plane
```

Control plan:

```text
.axit/milestones/M5-project-bootstrap-knowledge-plane/plan.md
```

Runtime compatibility overlay:

```text
.axit/milestones/M5-project-bootstrap-knowledge-plane/runtime-compatibility.md
```

Selection rationale:

```text
.axit/milestones/M5-project-bootstrap-knowledge-plane/selection-review.md
```

M4 remains deferred until a real accepted workspace contains at least two interacting Systems with an executable boundary.

## Promotion state

```text
M1 — Unity Runtime Binding: HUMAN_PROMOTED
M2 — Autonomous Bounded Development: HUMAN_PROMOTED
M3 — Unity Execution Coverage: HUMAN_PROMOTED
M4 — Cross-System Workspace: DEFERRED_WAITING_REAL_SECOND_SYSTEM
M5 — Project Bootstrap & Knowledge Plane: CURRENT_BOOTSTRAP_MATERIALIZED
M6 — Axit-Code Productization: NOT_AUTHORIZED
```

## First M5 launch result

The first attempt stopped correctly before product work:

```text
Status: HARD_BLOCKER
Blocker: TARGET_WORKSPACE_NOT_READY
child lanes launched: 0
product changes: none
Git publish actions: none
M6 actions: none
```

Observed causes were wrong execution root and Luna not being exposed by the current child runtime. This was a setup/policy incident, not an M5 product failure.

## Target bootstrap now materialized

Axit-Code canonical target:

```text
repository: ngocphat03/Axit-Code
ref: release
latest bootstrap commit after materialization: 99f8275e7f325e7ebb9e1f59d27db6657548bf1a
```

Target execution bootstrap now exists in Axit-Code:

```text
AGENTS.md
.codex/config.toml
.codex/agents/axit-verifier.toml
.codex/m5/runbook.md
.codex/m5/runtime-compatibility.md
```

These files are execution/bootstrap metadata. They do not pre-create the M5 Knowledge Plane and do not replace `docs/PLAN.md` as Axit-Code product truth.

The user's local Axit-Code checkout must be synced to include these files before the fresh M5 session starts.

## Model / cost policy

Canonical Game-Studios policy:

```text
.axit/policies/model-routing.md
```

Accepted hierarchy:

```text
primary orchestrator = gpt-5.6-sol / xhigh
child preferred      = gpt-5.6-luna / medium when supported
child compat fallback= gpt-5.6-terra / medium when Luna is unavailable
child Sol            = forbidden unless explicitly human-authorized
```

The current observed runtime does not expose Luna child execution, so the target `.codex/config.toml` uses Terra/medium as `COMPAT_TERRA`. This does not count as a human model override.

Never silently fall back to child Sol or reasoning above medium.

## M5 authoritative target anchors

Reacquire current target truth at run time:

```text
docs/PLAN.md          = canonical product goal/scope/roadmap
README.md             = current repository summary
docs/architecture.md  = architecture detail subordinate to PLAN
package.json          = target-native workspace validation scripts
```

Current known repository validation entrypoint is `npm run verify`; current target files win if changed.

## Stable promoted foundations carried into M5

- Core remains intentionally small; no speculative Profile/Skill/Workflow/Capability growth.
- Automation is high inside one accepted milestone and stops for human promotion review.
- Closure verifier result must be persisted and post-verdict consistency-audited before `MILESTONE_DONE`.
- Preserve unrelated filesystem state; never reset/clean/revert unrelated user work.
- Evidence provenance must distinguish canonical target truth, M5 bootstrap metadata, local-only execution evidence, and ephemeral process evidence.

## Next action

1. Sync/pull the local `ngocphat03/Axit-Code` `release` checkout so the new `AGENTS.md` and `.codex/**` bootstrap files are present.
2. Open a **fresh trusted Codex session at the Axit-Code repository root**.
3. Run `.codex/m5/runbook.md`; it already includes the accepted runtime compatibility route.
4. Run continuously until M5 terminal state and stop for human review.

Do not rerun M5 from Axit-Game-Studios. Do not start M4 or M6 automatically.
