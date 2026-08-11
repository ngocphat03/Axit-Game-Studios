# Axit Workspace Active State

Updated: 2026-08-11
Status: M6A-ready-for-phase0-canonical-discovery

## Roadmap state

```text
M1 — Unity Runtime Binding: HUMAN_PROMOTED
M2 — Autonomous Bounded Development: HUMAN_PROMOTED
M3 — Unity Execution Coverage: HUMAN_PROMOTED
M4 — Cross-System Workspace: DEFERRED_WAITING_REAL_SECOND_SYSTEM
M5 — Project Bootstrap & Knowledge Plane: HUMAN_PROMOTED
M6-A — Loader Foundation: READY_FOR_PHASE0_CANONICAL_DISCOVERY
M6-B+ — NOT_AUTHORIZED
```

M6-A plan:

```text
.axit/milestones/M6A-loader-foundation/plan.md
```

Selection review:

```text
.axit/milestones/M6A-loader-foundation/selection-review.md
```

## Corrected readiness rule

Axit-Code PR #5 is **not** a mandatory dependency.

Review on 2026-08-11 found:

```text
PR #5 last updated: 2026-08-07
state: open / draft / unmerged
review comments: none
branch vs current release: diverged
```

Treat PR #5 only as optional historical design input. Do not auto-merge/rebase/close it and do not treat its draft taxonomy/frontmatter as canonical.

M6-A Phase 0 is authorized to start from current Axit-Code canonical truth:

```text
release
docs/PLAN.md
accepted architecture + ADRs
current package/source/tests
accepted declarative artifacts, if any
```

Current canonical PLAN explicitly places profile loader/context loading with schema/diagnostics/tests in Phase 1 Slice 2. M6-A takes only the loader foundation; Context Builder remains later.

If canonical sources are insufficient to freeze material loader semantics without guessing, stop for the exact missing accepted decision. Do not block merely because PR #5 is unmerged.

## Model / cost policy

```text
primary = gpt-5.6-sol / xhigh
child preferred = gpt-5.6-luna / medium when supported
child compatibility fallback = gpt-5.6-terra / medium when Luna is unavailable
child Sol = forbidden without explicit current human override
```

M5 demonstrated `COMPAT_TERRA` successfully. No silent Sol fallback.

## Stable execution rules

- Primary orchestrates; children do delegatable work.
- Spawn lanes only for materially independent work or required verifier independence.
- Preserve unrelated filesystem state.
- Actual final verifier verdict must be persisted after it returns, then consistency-audited before `MILESTONE_DONE`.
- Use terminal end-to-end duration as primary latency metric.
- No speculative Profile/Rule/Workflow/Knowledge schema decisions.
- No M6-B, Harness, Tool Gateway, Run Ledger, provider, CLI orchestration, Unity integration, or Capability/Binding work in M6-A.

## Next action

Run M6-A Phase 0 from a fresh trusted Axit-Code session using current canonical `release` truth. Phase 0 must return either:

```text
SUFFICIENT_TO_FREEZE
```

or an evidence-backed exact missing canonical decision.

Do not wait on PR #5 solely because it exists. Do not start M6-B automatically.
