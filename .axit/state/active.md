# Axit Workspace Active State

Updated: 2026-08-11
Status: M6A-phase0-materialized-awaiting-fresh-target-session

## Roadmap state

```text
M1 — Unity Runtime Binding: HUMAN_PROMOTED
M2 — Autonomous Bounded Development: HUMAN_PROMOTED
M3 — Unity Execution Coverage: HUMAN_PROMOTED
M4 — Cross-System Workspace: DEFERRED_WAITING_REAL_SECOND_SYSTEM
M5 — Project Bootstrap & Knowledge Plane: HUMAN_PROMOTED
M6-A — Loader Foundation: PHASE0_MATERIALIZED
M6-B+ — NOT_AUTHORIZED
```

Game-Studios control plan:

```text
.axit/milestones/M6A-loader-foundation/plan.md
```

Target Axit-Code Phase 0 runbook:

```text
ngocphat03/Axit-Code@release:.codex/m6a/runbook.md
```

## Phase 0 purpose

The currently authorized run is discovery-only. It must determine whether current canonical Axit-Code sources are sufficient to freeze a minimal loader contract without guessing.

Terminal decision:

```text
SUFFICIENT_TO_FREEZE
```

or:

```text
MISSING_ACCEPTED_SEMANTICS
```

with exact evidence-backed missing decisions.

No loader implementation is authorized in this run.

## Canonical readiness rule

PR #5 is not a mandatory dependency. It is historical draft prior art only.

Canonical M6-A discovery truth is current Axit-Code:

```text
release
docs/PLAN.md
accepted architecture + ADRs
current package/source/tests
accepted declarative artifacts, if any
```

Do not promote draft PR taxonomy/frontmatter to canonical. Do not block merely because PR #5 is unmerged.

## Target execution bootstrap

Axit-Code `release` now contains:

```text
AGENTS.md
.codex/config.toml
.codex/agents/axit-verifier.toml
.codex/m6a/runbook.md
```

Fresh sessions are routed to M6-A Phase 0 only.

Phase 0 may write only:

```text
.codex/m6a/results/phase0-report.md
.codex/m6a/results/phase0-evidence.md
```

Product/source/test/schema/package/documentation changes are forbidden until Phase 0 is reviewed and a future loader contract is explicitly frozen.

## Model / cost policy

```text
primary = gpt-5.6-sol / xhigh
child preferred = gpt-5.6-luna / medium when supported
child compatibility fallback = gpt-5.6-terra / medium when Luna is unavailable
child Sol = forbidden without explicit current human override
```

Current target config uses Terra/medium compatibility fallback because Luna was not exposed by the observed child runtime. No silent Sol fallback.

## Stable execution rules

- Primary orchestrates; children do delegatable work.
- Use a small number of materially independent lanes; do not spawn one child per checklist item.
- Preserve unrelated filesystem state.
- Final verifier's actual verdict must be persisted after it returns, then consistency-audited.
- No speculative Profile/Rule/Workflow/Knowledge schema decisions.
- No M6-B, Context Builder, Harness, Tool Gateway, Run Ledger, provider, CLI orchestration, Unity integration, or Capability/Binding work.

## Next action

1. Pull/sync local `ngocphat03/Axit-Code` `release`.
2. Open a fresh trusted Codex session at Axit-Code repository root.
3. Execute `.codex/m6a/runbook.md` continuously through its Phase 0 terminal result.
4. Push/share the Phase 0 result branch only after the local run has finished, then stop for human/assistant review.

Do not start loader implementation or M6-B automatically.
