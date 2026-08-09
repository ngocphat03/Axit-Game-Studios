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

### Goal

Establish the smallest reusable Axit architecture and operating semantics before expanding execution surface.

### Capability demonstrated

- Core responsibility model;
- bounded implementation + independent verification;
- root Workspace/System routing;
- semantic Capability layer;
- continuous orchestrator/sub-agent operating model;
- configurable safety behavior.

### Existing evidence

- four Core Profiles;
- `implement-change` + `verify-change`;
- `bounded-change` live validation;
- root-routing validation;
- Capability semantic Cases 1–5;
- incident fixes for verdict precedence, capability-id discipline, manual MCP ownership, and Git-status gating.

### Exit expectation

Foundation remains stable unless later milestones expose a demonstrated flaw. Do not repeatedly redesign M0 during later execution.

---

## M1 — Unity Runtime Binding

Status: current

### Goal

Prove the complete chain from semantic Unity evidence need to a real user-configured transport and independent verification.

### Initial slice

- `unity.prefab.inspect`
- `unity.serialized-fields.inspect`
- `unity.playmode.verify`

### Required demonstration

```text
accepted criterion
  -> semantic Capability
  -> reviewed Runtime Binding
  -> real MCP operation(s)
  -> current evidence
  -> independent verify-change verdict
```

Include one bounded real `FAIL -> repair -> reacquire -> reverify` path.

### Exit gate

- reviewed binding promoted only for proven mappings;
- transport operation names came from live interface, not assumptions;
- evidence provenance is sufficient;
- acquisition-state vs verification-verdict semantics remain intact;
- continuous sub-agent recovery behaves acceptably;
- milestone report + retrospective reviewed by user + assistant.

---

## M2 — Autonomous Bounded Development

Status: mockup

### Goal

Prove Axit can autonomously execute several materially different bounded software tasks, not only one prepared damage/MCP scenario.

### Scenario families

At least several of:

- feature implementation;
- bug fix;
- bounded refactor;
- gameplay behavior;
- Unity UI behavior;
- data/persistence change that does not require unresolved product architecture.

### What this milestone should stress

- correct Profile/Skill/Workflow routing;
- implementation scope control;
- sub-agent lane decomposition;
- worker/verifier independence;
- recovery/replacement after sub-agent stalls;
- bounded repair loops;
- context/checkpoint continuity over longer runs;
- no unnecessary framework growth.

### Exit gate

Axit repeatedly reaches correct completion or correct hard blockers without routine user steering, and observed repeated gaps are hardened into the smallest correct framework layer.

---

## M3 — Unity Execution Coverage

Status: mockup

### Goal

Expand Unity evidence/execution coverage only from demonstrated needs discovered during M1–M2.

### Candidate existing capabilities

Potentially bind, only if real scenarios require them:

- `unity.compile`
- `unity.tests.run`
- `unity.console.inspect`
- `unity.scene.inspect`
- `unity.component.inspect`
- `unity.project.inspect`

This milestone does **not** require binding every catalog entry.

### Target demonstration

A nontrivial Unity vertical slice can combine appropriate source/tests/assets/scene/runtime/log evidence and produce an evidence-bounded verification result without overclaiming.

### Exit gate

Unity execution surface is broad enough for representative development work, while Capability semantics and Runtime Bindings remain narrow, reviewed, and replaceable.

---

## M4 — Cross-System Workspace

Status: mockup / waits for real systems

### Goal

Prove Axit can reason and verify across at least two real interacting Systems in the root workspace.

### Expected real topology

Examples:

```text
Backend/API
   <-> Unity client
   <-> CMS
   <-> services/workers
```

### Required principles

- executable provider contract is source of truth;
- `.axit` records ownership/routing/relationships rather than copied DTO/schema truth;
- provider and consumers are inspected before assigning blame;
- contract/integration/e2e tests validate shared boundaries;
- cross-system architecture/state ownership remains explicit.

### Exit gate

At least one real cross-system change/failure is implemented or diagnosed end-to-end with contract-aware evidence and boundary tests.

---

## M5 — Project Bootstrap & Knowledge Plane

Status: mockup

### Goal

Given a new real workspace, allow Axit to discover and bootstrap enough durable context to work effectively without generating a giant speculative agent catalog.

### Expected flow

```text
inspect repository
  -> identify Systems
  -> identify executable contracts/tests
  -> materialize workspace routing
  -> capture architecture/rules
  -> curate necessary Knowledge
  -> add specialization only from demonstrated gaps
```

### What must be avoided

- mass migration of legacy agents/skills;
- speculative Profiles for every traditional job role;
- huge auto-loaded documentation dumps;
- copied source/API truth that becomes stale.

### Exit gate

A second materially different real project can be bootstrapped and complete representative work with limited manual Axit configuration.

---

## M6 — Axit-Code Productization

Status: mockup

### Goal

Move contracts proven in Axit-Game-Studios into the actual Axit-Code runtime/product architecture.

### Candidate proven pieces to productize

- Profile/Skill/Workflow loading;
- Workspace/System routing;
- context building/progressive loading;
- Capability resolution;
- Runtime Binding resolution;
- Harness/policy boundary;
- independent Verification;
- Run Ledger/checkpoint/resume semantics;
- provider-independent model interface.

### Boundary

Axit-Game-Studios remains a spec/reference/benchmark lab. Do not turn it into the permanent runtime product merely because experiments work here.

### Exit gate

Axit-Code runs at least one previously proven vertical slice through its own runtime boundaries with equivalent or better evidence and safety behavior.

---

## M7 — Long-Run Reliability & Release Readiness

Status: mockup

### Goal

Prove the agent can run for extended periods and recover safely from realistic operational failures.

### Failure families

- sub-agent timeout/stall/replacement;
- context compaction and session recovery;
- interrupted/resumed runs;
- Unity MCP disconnect/reconnect as externally user-managed transport state;
- unavailable tool/environment;
- policy denial;
- partial implementation;
- verifier failure and bounded repair;
- nested repositories/submodules/untracked systems;
- large workspace/context pressure;
- cross-system dependency unavailable;
- provider/model replacement where supported;
- stale metadata vs executable source discrepancy.

### Audit expectation

A run should make it possible to answer:

```text
What was requested?
What decisions/scopes were accepted?
What changed?
Which agents/tools/capabilities ran?
What evidence was acquired?
Why was the final verdict reached?
What failed and how was it recovered?
Can execution resume safely?
```

### Exit gate

Representative long-running milestones complete with bounded intervention, trustworthy evidence, recoverable state, and no known recurring incident that lacks either a deliberate accepted limitation or regression protection.

---

# Milestone report mockup

Each completed milestone should leave a concise report with at least:

```text
Milestone:
Status: DONE | FAILED | HARD_BLOCKER
Started from:
Capability proven:
Scenarios executed:
Evidence summary:
Failures encountered:
Recovery/replacement loops:
Framework/config changes caused by incidents:
Regression protection added:
Known residual risks:
Recommended promotion decision: PROMOTE | REPAIR_AND_RERUN
```

# Retrospective questions

Before promotion, user + assistant should explicitly review:

1. Did the milestone prove its capability, or only a happy-path implementation?
2. Did any sub-agent stop for something the orchestrator should have resolved itself?
3. Did any hard-stop fire too early or too late?
4. Was required evidence missing, mislabeled, or overclaimed?
5. Did setup assumptions cause avoidable interruption?
6. Did any context disappear because it lived only in conversation?
7. Did the run create unnecessary Profile/Skill/Workflow/Capability complexity?
8. Which incident should become a permanent config/spec/regression rule?
9. Is the next milestone still the smallest valuable next capability?
10. Should this mockup be refined before promotion?

Do not silently rewrite milestone goals after execution starts. Record a deliberate roadmap revision when evidence justifies changing the map.