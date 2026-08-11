# Axit Workspace Active State

Updated: 2026-08-11
Status: M6A-human-promoted-M6B-designed-integration-gated

## Roadmap state

```text
M1 — Unity Runtime Binding: HUMAN_PROMOTED
M2 — Autonomous Bounded Development: HUMAN_PROMOTED
M3 — Unity Execution Coverage: HUMAN_PROMOTED
M4 — Cross-System Workspace: DEFERRED_WAITING_REAL_SECOND_SYSTEM
M5 — Project Bootstrap & Knowledge Plane: HUMAN_PROMOTED
M6-A — Loader Foundation: HUMAN_PROMOTED
M6-B — Context Builder Foundation: DESIGNED_INTEGRATION_GATED / NOT_AUTHORIZED_FOR_EXECUTION
M6-C+ — NOT_AUTHORIZED
```

## M6-A promoted result

Promotion review:

```text
.axit/milestones/M6A-loader-foundation/promotion-review.md
```

Validated target branch:

```text
ngocphat03/Axit-Code@feature/m6a-loader
```

Validated capability:

```text
.agents Markdown
  -> strict Loader Contract v1 validation
  -> normalized artifact records + source identity
  -> deterministic ordering
  -> structured error atomicity
```

Accepted evidence:

```text
focused loader tests = 16/16 PASS
package build/typecheck = PASS
root npm run verify = PASS
closure verifier = PASS
post-verdict consistency audit = PASS
product repair loops = 1/2
```

Human-observed Codex UI duration was about `28m 02s`; repository-durable timing telemetry was unavailable, so this remains UI provenance only.

Human promotion validates the capability but does **not** imply `feature/m6a-loader` has been merged into Axit-Code `release`.

## M6-B design

Selection review:

```text
.axit/milestones/M6B-context-builder/selection-review.md
```

Plan:

```text
.axit/milestones/M6B-context-builder/plan.md
```

Goal:

```text
explicit accepted context inputs
+ promoted M6-A records
-> deterministic source-aware context
-> preserved provenance
-> explicit omission/truncation behavior when contractually accepted
```

M6-B is design-only right now. It is **not authorized for execution**.

Before any M6-B run, the chosen Axit-Code writable baseline must actually contain the promoted M6-A loader implementation, ADR-002, canonical fixture, and passing target-native verification.

If not:

```text
HARD_BLOCKER: M6A_PRODUCT_BASELINE_NOT_INTEGRATED
```

Do not auto-merge/cherry-pick/rebase/open a PR merely to clear this gate.

## Stable execution policy

```text
primary = gpt-5.6-sol / xhigh
child preferred = gpt-5.6-luna / medium when supported
child compatibility fallback = gpt-5.6-terra / medium when Luna is unavailable
child Sol = forbidden without explicit current human override
```

- Primary orchestrates; children handle materially independent work.
- Avoid per-checkpoint child churn.
- Preserve unrelated filesystem state.
- Actual verifier verdict must be persisted after it returns, then consistency-audited.
- A pre-verdict draft may contain `PENDING`; predicted PASS is forbidden.
- Do not start M6-B, M6-C, Harness, Tool Gateway, Run Ledger, provider/CLI orchestration, Unity integration, or Capability/Binding work automatically.

## Next action

Make an explicit product-integration decision for the promoted M6-A diff before any M6-B execution planning on Axit-Code.

M6-B design may be reviewed now; execution remains gated.
