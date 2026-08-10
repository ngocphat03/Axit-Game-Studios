# Axit Milestones Mockup

Status: alignment-map

This is the human/assistant roadmap alignment map, not an executable plan. Execution is authorized only by `.axit/state/active.md` plus an accepted milestone-specific plan.

## Operating contract

Inside an accepted milestone:

```text
primary = gpt-5.6-sol / xhigh
child preferred = gpt-5.6-luna / medium when supported
child compatibility fallback = gpt-5.6-terra / medium when Luna is unavailable
child Sol = forbidden without explicit current human override
```

Also:

- parallelize only materially independent read-heavy work;
- serialize overlapping writes and editor/runtime mutations;
- do not spawn a new child merely for every phase/checkpoint;
- independently verify accepted outcomes;
- persist the **actual** closure-verifier verdict after it returns;
- run a fresh post-verdict consistency audit;
- record terminal end-to-end wall-clock for long-run comparisons;
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

Proved:

- minimal pointer-first bootstrap in a materially different real repository;
- canonical product truth remained target-owned (`docs/PLAN.md`);
- independent bootstrap review PASS;
- fresh-context continuity PASS without chat replay;
- one frozen REAL Axit-Code task PASS;
- focused tests 10/10 and repository verification PASS;
- medium-tier delegated execution via `COMPAT_TERRA` with zero human model overrides;
- final closure PASS after metadata-only persistence repair.

Performance sample:

```text
terminal end-to-end = 26m 02s
execution-to-preclosure = 22m 12s
child lanes = 19
peak useful parallelism = 4
replacements = 0
REAL repair loops = 0
```

Nineteen lanes is retained as an orchestration-overhead signal, not a hard failure threshold.

## M6 — Axit-Code Productization

Status: DECOMPOSED / IN PROGRESS BY HUMAN-ACCEPTED SLICES

Goal: migrate proven semantics into the actual Axit-Code runtime without copying the Game-Studios filesystem or growing multiple subsystems at once.

### M6-A — Loader Foundation

Status: DESIGNED_DEPENDENCY_GATED

Plan:

```text
.axit/milestones/M6A-loader-foundation/plan.md
```

Selection review:

```text
.axit/milestones/M6A-loader-foundation/selection-review.md
```

Goal:

```text
accepted Profile / Rule / Workflow / Knowledge source
  -> deterministic parse/validate
  -> normalized typed records
  -> source identity/provenance
  -> explicit conflict/error semantics
```

M6-A deliberately excludes Context Builder, Harness, Tool Gateway, Run Ledger, provider integration, CLI orchestration, Unity integration and M6-B.

Current dependency gate: Axit-Code PR #5 (`docs: establish AxitCode and Game Design rules`) is open/draft/unmerged and explicitly positions its rule/catalog foundation before loader implementation. Do not auto-merge or treat it as canonical. Human review must accept, revise or supersede the foundation before M6-A long execution.

If accepted loader-input conventions remain unavailable at readiness:

```text
HARD_BLOCKER: KNOWLEDGE_FOUNDATION_NOT_ACCEPTED
```

### M6-B — Context Builder

Status: mockup / NOT AUTHORIZED

Goal: consume proven loader records to construct minimal, source-aware model context with explicit identity and deterministic tests.

Do not design implementation details until M6-A is promoted.

### Later M6 slices

Future bounded slices may cover Harness/Tool Gateway, Verification, Run Ledger, provider/runtime integration, CLI orchestration, Capability/Binding resolution and Unity host integration only when their prerequisites are proven.

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
8. What were terminal wall-clock, child count, peak useful parallelism, replacements, repair loops, and model overrides?
9. Was child-lane count proportional to actual independent work, or did orchestration churn dominate?
10. Which incident needs a permanent rule/regression?
11. Is the next slice still the smallest valuable next capability?

Do not silently rewrite a milestone goal after execution starts.
