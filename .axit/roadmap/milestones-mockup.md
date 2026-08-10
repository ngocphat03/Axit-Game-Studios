# Axit Milestones Mockup

Status: alignment-map

This file is a **human/assistant roadmap mockup**, not an executable continuous plan.

Its purpose is to keep long-term direction stable across conversations and long autonomous runs. Detailed execution belongs in milestone-specific plans. Milestone boundaries may be refined by evidence, but the roadmap should not drift silently.

## Milestone operating contract

Inside one accepted milestone:

- run continuously without routine confirmation;
- primary Sol thread orchestrates, sub-agents perform delegatable work;
- recover/replace bounded sub-agent failures automatically;
- independently verify important outcomes;
- record incidents and harden the framework when a repeatable pipeline weakness is found;
- stop only at a declared hard blocker or milestone completion.

At milestone completion:

1. produce a compact Milestone Report;
2. produce/update a retrospective and incident hardening entries;
3. stop before the next milestone;
4. user + assistant review architecture, evidence, failures, and control quality;
5. either promote the milestone and open the next one, or patch/regress/rerun the current milestone.

A milestone is not complete because implementation exists. It is complete when its capability is demonstrated by accepted evidence and its important failure modes have been exercised sufficiently.

---

## M0 — Foundation Contracts

Status: substantially proven / foundation

Goal: establish the smallest reusable Axit architecture and operating semantics.

Stable foundation:

- four Core Profiles;
- `implement-change` + `verify-change`;
- `bounded-change`;
- root Workspace/System routing;
- semantic Capability layer;
- continuous orchestrator/sub-agent model;
- configurable safety behavior.

Do not repeatedly redesign M0 unless later milestones expose a demonstrated flaw.

---

## M1 — Unity Runtime Binding

Status: HUMAN_PROMOTED

Goal: prove semantic Unity evidence need -> reviewed Runtime Binding -> real user-configured transport -> evidence -> independent verdict.

Promoted scope remains exactly:

- `unity.prefab.inspect`
- `unity.serialized-fields.inspect`
- `unity.playmode.verify`

M1 closure artifacts: `.axit/milestones/M1-unity-runtime-binding/`.

---

## M2 — Autonomous Bounded Development

Status: HUMAN_PROMOTED on 2026-08-10

Goal: prove Axit can autonomously execute materially different bounded software tasks without routine user steering.

Result:

- S1 REAL bug fix PASS;
- S2 BENCHMARK small feature PASS;
- S3 REAL behavior-preserving refactor PASS;
- S4 BENCHMARK Unity-backed restoration PASS;
- context continuity PASS;
- two stalled sub-agent lanes recovered/replaced within policy;
- independent closure verification PASS.

Promotion review: `.axit/milestones/M2-autonomous-bounded-development/promotion-review.md`.

Post-M2 hardening:

- primary project reasoning config changed to `xhigh`; M3 must verify the effective fresh-session value;
- runtime target paths must be resolved from current runtime context rather than guessed from prefab hierarchy;
- evidence provenance is System-aware for independently tracked repositories;
- active state is compacted after promotion.

---

## M3 — Unity Execution Coverage

Status: current / ready-for-local-execution

Execution plan:

```text
.axit/milestones/M3-unity-execution-coverage/plan.md
```

Goal: prove the smallest Unity execution/evidence surface needed for representative real development work.

First demonstrated gap:

```text
unity.compile
```

M3 must live-discover the concrete compile operation before mapping it, prove a full QuickGun baseline compile, validate compile acquisition/failure semantics, and complete at least one REAL QuickGun product scenario for which full Unity compile is REQUIRED after production C# changes.

Do not bind every remaining Unity Capability speculatively. Additional mappings require demonstrated `REQUIRED_NOW` evidence need.

Exit gate: representative Unity work combines appropriate source/tests/compile/assets/runtime evidence and reaches an evidence-bounded independent verdict while Runtime Bindings remain narrow, reviewed, and replaceable.

---

## M4 — Cross-System Workspace

Status: mockup / waits for real interacting systems

Goal: prove Axit can reason and verify across at least two real interacting Systems in the root Workspace.

Expected principles:

- executable provider contract is source of truth;
- `.axit` stores ownership/routing/relationships rather than copied DTO/schema truth;
- provider and consumers are inspected before assigning blame;
- contract/integration/e2e tests validate shared boundaries;
- cross-system architecture/state ownership remains explicit.

Exit gate: at least one real cross-system change/failure is implemented or diagnosed end-to-end with contract-aware evidence and boundary tests.

---

## M5 — Project Bootstrap & Knowledge Plane

Status: mockup

Goal: given a new real Workspace, discover and bootstrap enough durable context to work effectively without generating a giant speculative agent catalog.

Expected flow:

```text
inspect repository
  -> identify Systems and repository boundaries
  -> identify executable contracts/tests
  -> materialize workspace routing
  -> capture architecture/rules
  -> curate necessary Knowledge
  -> add specialization only from demonstrated gaps
```

Exit gate: a second materially different real project can be bootstrapped and complete representative work with limited manual Axit configuration.

---

## M6 — Axit-Code Productization

Status: mockup

Goal: move contracts proven in Axit-Game-Studios into the actual Axit-Code runtime/product architecture.

Candidate proven pieces:

- Profile/Skill/Workflow loading;
- Workspace/System routing;
- context building/progressive loading;
- Capability resolution;
- Runtime Binding resolution;
- Harness/policy boundary;
- independent Verification;
- Run Ledger/checkpoint/resume semantics;
- provider-independent model interface.

Boundary: Axit-Game-Studios remains a spec/reference/benchmark lab.

---

## M7 — Long-Run Reliability & Release Readiness

Status: mockup

Goal: prove extended autonomous execution and safe recovery under realistic failures.

Failure families include sub-agent stall/replacement, context compaction, interrupted/resumed runs, Unity transport disconnect/reconnect, policy denial, partial implementation, verifier failure, nested/submodule/separately tracked Systems, large-context pressure, unavailable dependencies, provider/model changes, and stale metadata.

Exit gate: representative long-running milestones complete with bounded intervention, trustworthy evidence, recoverable state, and no known recurring incident lacking an accepted limitation or regression protection.

---

# Promotion review questions

Before promotion, user + assistant explicitly review:

1. Was the capability proven or only a happy path?
2. Did the orchestrator recover routine stalls itself?
3. Did hard-stops fire correctly?
4. Was evidence correctly REQUIRED/SUPPORTING and provenance-aware?
5. Did setup assumptions cause avoidable interruption?
6. Did context survive without chat replay?
7. Did the run create unnecessary framework complexity?
8. Which incident must become a permanent rule/config/regression?
9. Is the next milestone still the smallest valuable next capability?
10. Should this map be deliberately revised before promotion?

Do not silently rewrite milestone goals after execution starts.
