# Axit Milestones Mockup

Status: alignment-map

This is the human/assistant roadmap alignment map, not an executable plan. Execution is authorized only by `.axit/state/active.md` plus an accepted milestone-specific plan.

## Operating contract

Inside an accepted milestone:

- primary orchestrator: `gpt-5.6-sol / xhigh`;
- every child lane: `gpt-5.6-luna / medium` by default under `.axit/policies/model-routing.md`;
- no silent child model escalation;
- parallelize only materially independent read-heavy work;
- serialize overlapping writes and editor/runtime mutations;
- independently verify accepted outcomes;
- persist report + retrospective + closure-verifier verdict;
- run a post-verdict consistency audit;
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

Status: HUMAN_PROMOTED on 2026-08-10

Promotion review:

```text
.axit/milestones/M3-unity-execution-coverage/promotion-review.md
```

Proved:

- live-discovered `unity.compile` mapping;
- full QuickGun baseline compile PASS;
- compile success/error acquisition semantics;
- REAL duplicate-release bug fix PASS;
- final focused tests 3/3;
- fresh post-change full Unity compile PASS;
- closure repair + final closure PASS;
- no additional `REQUIRED_NOW` Unity mapping.

Active mappings after M3:

```text
unity.prefab.inspect
unity.serialized-fields.inspect
unity.playmode.verify
unity.compile
```

Five remaining Unity capabilities stay unbound until demonstrated need.

Post-M3 hardening:

- final closure verdict must be persisted before `MILESTONE_DONE`;
- future child lanes use Luna/medium;
- no silent child model escalation;
- long milestones record wall-clock/model/child/replacement/repair accounting when observable.

## M4 — Cross-System Workspace

Status: DEFERRED_WAITING_REAL_SECOND_SYSTEM

Goal: prove one real end-to-end change/failure across at least two interacting Systems with executable contract awareness.

M4 is deferred, not cancelled. Current QuickGun evidence does not justify inventing a backend/CMS/service or synthetic second System merely to preserve milestone numbering.

Resume M4 only when a real accepted workspace exposes at least two interacting Systems with an executable provider/consumer boundary worth changing or diagnosing.

Selection rationale:

```text
.axit/milestones/M5-project-bootstrap-knowledge-plane/selection-review.md
```

## M5 — Project Bootstrap & Knowledge Plane

Status: CURRENT_DESIGNED_AWAITING_TARGET_BOOTSTRAP

Target:

```text
repository: ngocphat03/Axit-Code
canonical ref: release
```

Control plan:

```text
.axit/milestones/M5-project-bootstrap-knowledge-plane/plan.md
```

Target bootstrap contract:

```text
.axit/milestones/M5-project-bootstrap-knowledge-plane/target-bootstrap.md
```

Goal: prove Axit can enter a materially different real repository, discover only the durable context actually needed, recover that context in a fresh child without chat replay, and complete one frozen REAL task using target-native validation.

Important execution boundary: product work must run from a trusted writable local Axit-Code checkout. Do not guess its filesystem path, do not mutate Axit-Code through cloud writes as a substitute, and do not start the long target run until project-scoped model routing is present before session start.

M5 is also the first intended long-run benchmark of the durable cost policy:

```text
primary = Sol / xhigh
children = Luna / medium
model overrides expected = 0
```

Exit gate: minimal pointer-first bootstrap passes independent review, fresh-context continuity passes, one REAL target task passes target-native verification, model-routing control is compliant, cost/performance accounting is recorded when observable, and closure is durable before human promotion.

## M6 — Axit-Code Productization

Status: mockup / NOT STARTED

Goal: migrate contracts proven in Axit-Game-Studios into the actual Axit-Code runtime: loading, routing, context, Capability/Binding resolution, Harness, Verification, Run Ledger, provider independence, and model/cost routing.

M5 does not authorize M6. Bootstrap validation must not be presented as production runtime integration.

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
7. Did child lanes remain Luna/medium unless explicitly human-overridden?
8. What were wall-clock duration, child count, replacements, repair loops, and model overrides?
9. Which incident needs a permanent rule/regression?
10. Is the next milestone still the smallest valuable next capability?

Do not silently rewrite a milestone goal after execution starts.
