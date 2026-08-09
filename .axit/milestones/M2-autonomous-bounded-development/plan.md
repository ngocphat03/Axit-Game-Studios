# M2 — Autonomous Bounded Development

Status: ready-for-local-execution

## Goal

Prove Axit can autonomously complete several materially different bounded development tasks without routine user steering, while preserving scope control, worker/verifier independence, bounded recovery, durable checkpoints, and evidence-bounded completion.

M2 is about **autonomous development behavior**, not expanding the Unity Capability catalog. M1 Runtime Binding remains stable during this milestone.

## Promotion context

M1 — Unity Runtime Binding is human-approved for promotion into M2.

One known issue is deliberately deferred for this milestone:

```text
DEFERRED_PRIMARY_REASONING_CONFIG
```

The prior run observed the primary session at `gpt-5.6-sol / medium` while the repository intended a stronger reasoning setting. The user explicitly chose to prioritize M2 before repairing that configuration mismatch.

For **M2 only**:

- inspect and record the effective primary/sub-agent model and reasoning level;
- do not block M2 solely because the known primary reasoning mismatch persists;
- do not silently call the mismatch resolved;
- include it in the M2 retrospective and promotion review;
- do not automatically carry this exception into M3.

All other execution-readiness failures remain subject to normal hard-stop rules.

## Required context

Read only what is needed:

1. `.axit/workspace.yaml`;
2. `.axit/memory/project-memory.md`;
3. `.axit/roadmap/milestone-closure.md`;
4. this plan;
5. relevant System/Core context on demand;
6. M1 binding only when a selected scenario actually needs Unity evidence.

Do not recursively preload `.axit/`.

## Operating contract

- Primary thread is orchestrator only when sub-agents are available.
- Delegatable exploration, implementation, tests, evidence acquisition, repair, and verification belong to sub-agents.
- Keep worker and verifier roles separate.
- Read-heavy independent discovery may run in parallel; overlapping writes and Unity runtime mutations must be serialized.
- Routine scenario/phase completion never requires user confirmation.
- Each same required failure gets at most two bounded repair/replacement attempts.
- A scenario-specific candidate may be replaced once when its blocker is specific to that candidate rather than systemic. Record the replacement and reason; do not cherry-pick until something passes.
- Follow `.axit/roadmap/milestone-closure.md` before returning `MILESTONE_DONE`.

## Git-status policy

Respect `.axit/workspace.yaml -> safety.check_git_status`.

For this workspace it defaults to `false`, so Git status must not be used as a readiness gate or blocker. This does not authorize reset, clean, revert, destructive deletion, or blind overwrite.

## Framework-growth boundary

During M2, do not automatically add or redesign:

- Core Profiles;
- Core Skills;
- Core Workflows;
- semantic Capability ids;
- additional Runtime Binding mappings.

If repeated evidence suggests one of those is needed, record a demonstrated gap for human promotion review.

M2 may automatically make a **small framework/config/checklist/routing hardening fix** when a real pipeline incident proves the need and the fix does not change a material architecture/public contract/permission boundary. Such a fix must gain regression protection and be recorded in the retrospective/decision log.

## Product-intent discipline

Never invent product behavior merely to satisfy M2.

Scenario selection must distinguish:

```text
REAL
  acceptance already exists in source/tests/ADR/rules/accepted project evidence

BENCHMARK
  self-contained milestone contract created solely to exercise agent behavior
```

A BENCHMARK scenario must not be presented as a user/product requirement.

Prefer REAL scenarios. Use BENCHMARK fallback only when no safe real candidate with sufficiently explicit acceptance exists for that scenario slot.

Controlled benchmark fixtures should live at the narrowest suitable test/fixture location and must not change public product contracts.

## Evidence provenance

Every scenario record must identify evidence as one or more of:

- `canonical-pushed` — reproducible from the canonical pushed branch;
- `local-or-separately-tracked` — current local System/test evidence not present in root pushed branch;
- `ephemeral-runtime` — live editor/runtime evidence.

Do not force Git tracking merely to upgrade provenance classification.

---

# Phase 0 — M2 readiness

Delegate a read-only readiness lane.

Confirm:

- M1 binding remains active for exactly the proven three-capability slice;
- Core v1 / Workspace-System v1 / Capability semantic v1 remain stable;
- multi-agent/sub-agent execution is available;
- current effective primary and sub-agent model/reasoning values are recorded;
- `safety.check_git_status` is honored;
- the current source tree can be read and bounded test mechanisms can be identified;
- if Unity evidence is later selected, the user-configured MCP transport remains user-owned and must not be installed/configured/repaired by Axit.

The known primary reasoning mismatch is a recorded non-blocking exception for M2 only.

Checkpoint and continue.

---

# Phase 1 — Discover and freeze scenario contracts

Use exploration sub-agents to inspect the current QuickGun source/tests/docs and identify safe candidates. Do not modify product/source during discovery.

Create:

```text
.axit/milestones/M2-autonomous-bounded-development/scenario-manifest.md
```

Freeze exactly four scenario slots before executing the first worker:

## S1 — Bug fix

Prefer a real existing defect or failing behavior with explicit accepted evidence.

Fallback: a controlled isolated benchmark defect whose expected behavior is specified before the repair worker starts.

## S2 — Small feature

Prefer a real small requirement already explicit in accepted project evidence and requiring no unresolved product/architecture decision.

Fallback: a self-contained benchmark feature with explicit inputs, outputs, acceptance tests, and no new public product contract.

## S3 — Behavior-preserving refactor

Must touch real project source when a safe internal candidate exists.

Acceptance is preservation of observable behavior/public contract, proven by existing plus targeted regression evidence. Do not perform a broad cleanup or speculative architecture rewrite.

If no safe real candidate exists, use a benchmark fixture and record that limitation.

## S4 — Unity-backed bounded scenario

Prefer a real bounded QuickGun change/repair whose accepted behavior is already known and whose verification benefits from the existing M1 binding.

May use only the currently mapped capabilities when appropriate:

- `unity.prefab.inspect`;
- `unity.serialized-fields.inspect`;
- `unity.playmode.verify`.

Do not bind additional capabilities during M2.

If a real Unity development candidate lacks explicit product intent, use a controlled reversible benchmark/repair scenario based only on already accepted behavior. Production state must finish in the accepted state.

## Scenario contract fields

For each S1–S4 freeze:

```text
id:
type: REAL | BENCHMARK
category:
goal:
acceptance-source:
accepted behavior:
allowed files/scope:
forbidden scope:
required evidence:
supporting evidence:
expected cleanup/restoration:
evidence provenance expected:
hard-blocker triggers:
```

Have a separate read-only reviewer confirm before execution that:

- the behavior is sufficiently explicit;
- no scenario invents product intent;
- no public contract/state ownership/material architecture decision is hidden inside the task;
- the four scenarios are materially different;
- scope/evidence are bounded enough for independent verification.

If a contract fails review, replace/refine it before any worker begins.

Checkpoint and continue.

---

# Phases 2–5 — Execute S1–S4

Execute the frozen scenarios sequentially. Do not rewrite acceptance criteria after seeing implementation results.

For each scenario:

```text
frozen scenario contract
  -> implementation worker using implement-change discipline
      -> relevant deterministic/project/Unity evidence acquisition
          -> fresh independent verifier using verify-change discipline
              -> PASS
              OR FAIL -> bounded repair worker -> reacquire -> fresh verifier
              OR BLOCKED -> recovery/replacement policy or hard blocker
```

## Worker rules

- implement the smallest bounded change that satisfies the frozen contract;
- preserve unrelated current filesystem state;
- do not broaden scope because another improvement is nearby;
- do not issue the final verification verdict;
- run appropriate implementation checks but treat them as evidence, not self-certification.

## Verifier rules

- independently classify REQUIRED vs SUPPORTING evidence;
- use current post-change evidence;
- do not repair findings while acting as verifier;
- do not infer broad correctness from one narrow test/runtime path;
- return PASS / FAIL / BLOCKED with explicit criterion mapping.

## Failure/recovery rules

On FAIL:

- assign a separate bounded repair worker;
- repair only the demonstrated required defect;
- reacquire evidence after repair;
- use a fresh verifier;
- maximum two repair loops for the same required failure.

On a stalled/incomplete sub-agent:

- steer/resume once when safe;
- replace with a fresh distilled-context sub-agent if still stuck;
- maximum two bounded recovery/replacement attempts for the same lane.

On candidate-specific BLOCKED:

- one replacement candidate is allowed for that scenario slot if the blocker does not indicate a systemic milestone failure;
- record the original candidate, blocker, and replacement rationale;
- if the replacement also blocks for the same systemic reason, stop rather than cherry-picking further.

After each scenario, update only compact progress in `.axit/state/active.md` and the scenario manifest.

---

# Phase 6 — Context/checkpoint continuity probe

After at least two scenarios have completed, delegate one fresh read-only sub-agent that receives only:

- `.axit/workspace.yaml`;
- `.axit/state/active.md`;
- this M2 plan;
- the scenario manifest;
- relevant on-demand System/Core files.

It must correctly summarize:

- completed scenario outcomes;
- current scenario/next action;
- unresolved blockers/incidents;
- which framework layers are stable and must not be redesigned;
- the known deferred primary reasoning issue.

This tests whether durable file state is sufficient without relying on conversation replay.

If the fresh agent cannot reconstruct the milestone state, harden checkpoint/memory routing and rerun this probe before closure.

---

# Phase 7 — Cross-scenario analysis and incident hardening

Use analysis sub-agents to compare S1–S4.

Identify:

- repeated orchestration failures;
- repeated setup assumptions;
- repeated evidence gaps;
- worker/verifier boundary failures;
- stale-state or stale-evidence problems;
- hard-stops that fired too early/late;
- framework/config weaknesses that repeated;
- candidate additions that would be unnecessary framework growth.

For every repeatable pipeline incident, apply:

```text
incident
  -> root cause
  -> smallest safe fix
  -> regression protection
```

Small non-architectural framework hardening may be applied and reverified inside M2.

Material architecture/Core/Capability/permission changes must be recorded as recommendations for human review rather than silently implemented.

Explicitly revisit `DEFERRED_PRIMARY_REASONING_CONFIG` in this analysis. It remains allowed to stay unresolved through M2 execution, but the report must state whether it should be repaired before M3.

---

# Phase 8 — M2 closure

Follow `.axit/roadmap/milestone-closure.md`.

Required persisted artifacts:

```text
.axit/milestones/M2-autonomous-bounded-development/report.md
.axit/milestones/M2-autonomous-bounded-development/retrospective.md
```

Closure verifier must confirm:

- exactly four frozen scenario slots were executed or correctly hard-blocked;
- scenario acceptance was not rewritten after implementation;
- worker/verifier separation held;
- repair/replacement limits were respected;
- evidence provenance is explicit;
- context continuity probe passed;
- no unjustified Core/Capability/Binding expansion occurred;
- repeatable incidents gained regression protection or an explicit accepted limitation;
- deferred primary reasoning mismatch is still visible if unresolved;
- active state, scenario manifest, report, retrospective, and roadmap status agree.

## M2 PASS expectation

Recommend `PROMOTE` only when:

- at least three materially different scenario categories complete with final PASS;
- all four slots have an honest terminal result (PASS or correct hard blocker), with no hidden/cherry-picked failure;
- at least one scenario exercises real project source or Unity/project state rather than only benchmark fixtures;
- at least one scenario demonstrates a real bounded repair/reverification or sub-agent recovery path during M2, unless no such failure naturally occurs—in that case report the absence rather than manufacturing a risky failure;
- the fresh-context continuity probe passes;
- no recurring pipeline failure remains unclassified/unhardened;
- independent closure verification passes.

Then return only a compact closure summary and stop for human promotion review.

Do **not** open or execute M3 automatically.

## Hard blockers

Stop the milestone only when materially necessary:

- `PRODUCT_INTENT` — required behavior cannot be resolved from accepted evidence and no safe benchmark fallback is appropriate;
- `ARCHITECTURE_DECISION` — material architecture/public contract/state ownership/dependency-direction decision is required;
- `DESTRUCTIVE_SCOPE` — destructive migration/deletion or broad unrelated refactor is required;
- `SECRET_OR_PRODUCTION` — credentials/secrets/production/cloud writes are required;
- `ADMIN_ESCALATION` — sudo/admin or machine-wide configuration is required;
- `UNITY_MCP_NOT_READY` — only when a required selected Unity scenario cannot proceed because the user-managed transport is unavailable after bounded runtime recovery; do not repair MCP configuration;
- `REPEATED_REQUIRED_FAILURE` — same required failure persists after two bounded repair loops;
- `SYSTEMIC_SCENARIO_BLOCKER` — scenario replacement exposes the same milestone-wide blocker;
- `MULTI_AGENT_UNAVAILABLE` — primary would otherwise have to perform delegatable work directly.

The known `DEFERRED_PRIMARY_REASONING_CONFIG` is explicitly **not** a hard blocker for M2 only.

## Final output

After closure artifacts and closure verification exist, return:

```text
Status: MILESTONE_DONE | HARD_BLOCKER
Milestone: M2 Autonomous Bounded Development
Scenarios: S1=<...>, S2=<...>, S3=<...>, S4=<...>
Real vs benchmark: ...
Repair loops: ...
Sub-agent replacements: ...
Context continuity: PASS | FAIL
Framework hardening: ...
Deferred issues: ...
Promotion recommendation: PROMOTE | REPAIR_AND_RERUN
```

Stop there.