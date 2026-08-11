# Axit Milestones Mockup

Status: alignment-map

This is the human/assistant roadmap alignment map, not an executable plan. Execution is authorized only by `.axit/state/active.md` plus an accepted milestone-specific runbook/contract.

## Operating contract

Inside an accepted milestone:

```text
primary = gpt-5.6-sol / xhigh
child preferred = gpt-5.6-luna / medium when supported
child compatibility fallback = gpt-5.6-terra / medium when Luna is unavailable
child Sol = forbidden without explicit current human override
```

Also:

- parallelize only materially independent work;
- serialize overlapping writes/editor mutations;
- avoid per-checkpoint child churn;
- independently verify accepted outcomes;
- persist the actual closure-verifier verdict after it returns;
- run a fresh post-verdict consistency audit;
- record trustworthy terminal wall-clock when observable;
- stop for human promotion review.

## M0 — Foundation Contracts

Status: foundation / stable

Goal: smallest reusable Core, Workspace/System, Capability, verification, safety, and orchestration contracts.

## M1 — Unity Runtime Binding

Status: HUMAN_PROMOTED

Promoted mappings:

```text
unity.prefab.inspect
unity.serialized-fields.inspect
unity.playmode.verify
```

## M2 — Autonomous Bounded Development

Status: HUMAN_PROMOTED

Proved materially different bounded tasks, worker/verifier separation, recovery/replacement, context continuity, and milestone closure.

## M3 — Unity Execution Coverage

Status: HUMAN_PROMOTED

Proved live `unity.compile`, full QuickGun compile evidence, a REAL product fix, bounded repair, and independent closure.

Active Unity mappings after M3:

```text
unity.prefab.inspect
unity.serialized-fields.inspect
unity.playmode.verify
unity.compile
```

Five remaining Unity capabilities stay unbound until demonstrated need.

## M4 — Cross-System Workspace

Status: DEFERRED_WAITING_REAL_SECOND_SYSTEM

Goal: prove one real end-to-end change/failure across at least two interacting Systems with executable contract awareness.

Do not manufacture a backend/CMS/service or synthetic second System merely to preserve milestone numbering.

## M5 — Project Bootstrap & Knowledge Plane

Status: HUMAN_PROMOTED on 2026-08-11

Target:

```text
ngocphat03/Axit-Code
execution branch: feature/m5
```

Promotion review:

```text
.axit/milestones/M5-project-bootstrap-knowledge-plane/promotion-review.md
```

Proved minimal pointer-first bootstrap, target-owned canonical truth, fresh-context continuity, one REAL bounded task, target verification, medium-tier delegation, and independent closure.

Performance sample:

```text
terminal end-to-end = 26m 02s
execution-to-preclosure = 22m 12s
child lanes = 19
peak useful parallelism = 4
```

## M6 — Axit-Code Productization

Status: IN PROGRESS BY BOUNDED HUMAN-ACCEPTED SLICES

Goal: migrate proven semantics into the actual Axit-Code runtime without copying Game-Studios filesystem contracts wholesale or growing multiple subsystems at once.

### M6-A — Loader Foundation

Status: HUMAN_PROMOTED on 2026-08-11

Promotion review:

```text
.axit/milestones/M6A-loader-foundation/promotion-review.md
```

Canonical target contract:

```text
ngocphat03/Axit-Code@release:docs/decisions/ADR-002-loader-contract-v1.md
```

Validated implementation branch:

```text
ngocphat03/Axit-Code@feature/m6a-loader
```

Proved:

```text
.agents Markdown acquisition
-> strict Loader Contract v1 validation
-> normalized typed records + repo-relative source identity
-> deterministic ordering
-> explicit duplicate/error behavior
-> no success catalog on ERROR
```

Evidence:

```text
focused tests = 16/16 PASS
package build/typecheck = PASS
root npm run verify = PASS
canonical fixture = PASS
closure verifier = PASS
post-verdict audit = PASS
```

The first closure verifier caught an inline-`#` strict-parser defect; bounded repair 1/2 fixed it and regression coverage passed. A later closure-artifact-only terminal-newline mismatch was corrected and re-audited without product rerun.

Human promotion does not itself merge the product branch into Axit-Code `release`.

### M6-B — Context Builder Foundation

Status: DESIGNED_INTEGRATION_GATED / NOT AUTHORIZED FOR EXECUTION

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
+ promoted M6-A artifact records
-> deterministic source-aware context
-> provenance-preserving normalized representation
-> explicit omission/truncation diagnostics when accepted
```

Canonical execution flow places Context Builder after profile/context acquisition and requires provenance, relevance discipline, secret minimization, non-policy treatment of source/Markdown, and recorded truncation strategy.

Before any M6-B execution, the chosen writable Axit-Code baseline must contain the promoted M6-A implementation plus ADR-002/fixture and pass target-native verification. Otherwise:

```text
HARD_BLOCKER: M6A_PRODUCT_BASELINE_NOT_INTEGRATED
```

M6-B Phase 0 must discover and freeze the exact minimal contract before product writes. Reference resolution, profile selection, relevance/token-budget behavior, allowed tool-descriptor inclusion, and diagnostics are questions to resolve from canonical evidence rather than assumptions.

Do not automatically include semantic/vector search, Harness/Tool Gateway, Run Ledger, provider execution, planning-loop changes, CLI orchestration, Unity integration, Capability/Binding runtime, or M6-C+.

### Later M6 slices

Future bounded slices may cover Harness/Tool Gateway, Verification, Run Ledger, provider/runtime integration, CLI orchestration, Capability/Binding resolution and Unity host integration only when prerequisites are proven.

Do not bundle them automatically.

## M7 — Long-Run Reliability & Release Readiness

Status: mockup

Goal: prove long-running recovery, resumability, evidence trust, model-routing compliance, and controlled cost/latency under realistic failures.

## Review questions before every promotion

1. Did the milestone prove the capability rather than only a happy path?
2. Were worker/verifier responsibilities independent?
3. Were REQUIRED/SUPPORTING evidence and provenance correct?
4. Did setup/control assumptions fail?
5. Did context survive without chat replay?
6. Did the run create unnecessary framework growth?
7. Did child lanes stay on the allowed medium tier unless explicitly human-overridden?
8. What timing/lane/repair/model data is trustworthy, and what is unavailable?
9. Was child-lane count proportional to actual independent work?
10. Which incident needs permanent regression protection?
11. Is the next slice still the smallest valuable next capability?
12. Is the promoted product diff actually integrated on the next slice's selected baseline?

Do not silently rewrite a milestone goal after execution starts.
