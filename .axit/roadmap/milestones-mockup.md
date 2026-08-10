# Axit Milestones Mockup

Status: alignment-map

This is the human/assistant roadmap alignment map, not an executable plan. Execution is authorized only by `.axit/state/active.md` plus an accepted milestone-specific plan.

## Operating contract

Inside an accepted milestone:

- primary orchestrator: `gpt-5.6-sol / xhigh`;
- every child lane: `gpt-5.6-luna / medium` by default under `.axit/policies/model-routing.md`;
- no silent child model escalation;
- parallelize only materially independent read-heavy work;
- serialize overlapping writes and Unity/editor mutations;
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
- long milestones should record wall-clock/model/child/replacement/repair accounting.

## M4 — Cross-System Workspace

Status: mockup / NOT STARTED

Goal: prove one real end-to-end change/failure across at least two interacting Systems with executable contract awareness.

Do not start M4 until at least two real interacting Systems and accepted boundaries actually exist. If they do not, deliberately reconsider milestone order.

## M5 — Project Bootstrap & Knowledge Plane

Status: mockup

Goal: bootstrap a materially different real workspace with small durable context and no speculative agent catalog.

## M6 — Axit-Code Productization

Status: mockup

Goal: migrate proven Game-Studios contracts into the actual Axit-Code runtime: loading, routing, context, Capability/Binding resolution, Harness, Verification, Run Ledger, provider independence, and model/cost routing.

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
