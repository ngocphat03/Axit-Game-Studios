# M5 — Project Bootstrap & Knowledge Plane

Status: designed-awaiting-target-bootstrap

## Goal

Prove that Axit can enter a materially different real repository, discover the smallest durable context needed to work correctly, bootstrap that context without generating a speculative agent catalog, recover that context in a fresh child, and complete one bounded REAL task using the target repository's own authoritative requirements and validation commands.

M5 target:

```text
repository: ngocphat03/Axit-Code
canonical ref: release
product: AxitCode
```

Axit-Code is deliberately different from the QuickGun Unity System. It is a TypeScript workspace/agent-runtime product. M5 tests bootstrap and Knowledge Plane quality, not Unity execution coverage.

M4 — Cross-System Workspace is deferred until a real product workspace contains at least two interacting Systems with accepted boundaries. Do not manufacture a second System or cross-system benchmark merely to preserve milestone numbering.

## Execution topology

M5 is specified in Axit-Game-Studios but must execute product work from a trusted writable local checkout of the target Axit-Code repository.

Do not assume or invent a local filesystem path for Axit-Code.

Do not use GitHub/cloud writes as a substitute for a local writable target workspace. Do not clone, relocate, or reconfigure the user's target repository automatically just to satisfy M5.

Before launching the long target run, apply the target bootstrap described in:

```text
.axit/milestones/M5-project-bootstrap-knowledge-plane/target-bootstrap.md
```

The target bootstrap exists so the fresh Axit-Code Antigravity session starts with the accepted model/cost policy already active. Creating model config after the session has already spawned children is insufficient evidence of compliance.

If the target cannot be opened as a trusted writable workspace with the accepted bootstrap, stop before product work with:

```text
HARD_BLOCKER: TARGET_WORKSPACE_NOT_READY
```

This is a setup prerequisite, not authorization to change machine-wide Antigravity configuration.

## Authoritative target sources

At bootstrap start, reacquire current target truth rather than assuming these files are unchanged. Current known anchors are:

```text
docs/PLAN.md          = canonical product goal/scope/roadmap
README.md             = current repository/product summary
docs/architecture.md  = architecture detail subordinate to PLAN
package.json          = workspace scripts and Node requirement
```

Known current validation entrypoint:

```text
npm run verify
```

Known current platform boundary:

```text
@axitcode/agent = authoritative TypeScript Agent Runtime for MVP
```

These are starting pointers, not permission to freeze stale facts. If current target content differs, current `docs/PLAN.md` wins for product goal/roadmap.

## Model / cost contract

M5 is the first long milestone intended to benchmark the durable cost policy.

```text
primary orchestrator = gpt-5.6-sol / xhigh
all child lanes       = gpt-5.6-luna / medium
```

All child roles are covered: discovery, bootstrap authoring, implementation, tests, verification, recovery, closure verification, and report/retrospective authoring.

No child may silently escalate model class or reasoning effort. Follow `.axit/policies/model-routing.md` semantics from Game-Studios as the M5 control contract. A target-local bootstrap copy may express the same rule, but must not invent a weaker policy.

At readiness record configured values and effective values when observable. If child runtime metadata is unavailable, say unavailable; do not infer it.

An unapproved child escalation is a control finding. Use context sharpening, decomposition, steer/resume, or Luna/medium replacement rather than buying a larger child model.

## Operating contract

- Primary thread orchestrates only when delegation is available.
- Use Luna/medium children for all delegatable work.
- Parallelize materially independent read-only discovery lanes.
- Serialize overlapping writes, target bootstrap metadata writes, product writes, and state/report writes.
- Do not ask for routine confirmations inside accepted scope.
- Maximum two bounded recovery/replacement attempts for the same required lane/failure.
- Do not commit, push, publish, or open a PR.
- Do not change public product goals, architecture ownership, provider policy, Harness trust boundaries, or roadmap intent without accepted evidence.
- Do not add Profiles/Skills/Workflows merely because their names seem useful.
- Do not copy whole authoritative product docs into bootstrap knowledge; store concise facts and pointers.
- Follow milestone closure including post-verdict persistence before `MILESTONE_DONE`.
- Stop for human promotion review. Do not start M6 automatically.

## Hard blockers

```text
TARGET_WORKSPACE_NOT_READY
PRIMARY_REASONING_NOT_EFFECTIVE
MODEL_ROUTING_NOT_EFFECTIVE
PRODUCT_INTENT
ARCHITECTURE_DECISION
DESTRUCTIVE_SCOPE
SECRET_OR_PRODUCTION
ADMIN_ESCALATION
REPEATED_REQUIRED_FAILURE
```

`MODEL_ROUTING_NOT_EFFECTIVE` applies when the runtime demonstrably launches child lanes above Luna/medium without an explicit current human override. Do not silently continue a long cost benchmark under a different child allocation.

---

# Phase 0 — Target readiness and baseline identity

Run from the trusted Axit-Code repository root after the target bootstrap is present before session start.

Delegate a read-only readiness lane.

Required checks:

- exact current repository root is Axit-Code, not Axit-Game-Studios or QuickGun;
- repository identity/ref/current commit are recorded when available;
- `docs/PLAN.md` exists and is treated as canonical product/roadmap truth;
- package/workspace layout and validation commands are reacquired from current files;
- effective primary is `gpt-5.6-sol / xhigh` when observable;
- child default is configured `gpt-5.6-luna / medium`, and one harmless read-only child is used to confirm effective child identity when runtime metadata exposes it;
- no M6 productization plan is authorized;
- no target product files have yet been changed by M5.

Record start wall-clock timestamp for performance accounting.

Do not use Git cleanliness as a universal readiness gate. Preserve unrelated current filesystem state and never reset/clean/revert user work.

---

# Phase 1 — Parallel read-only repository discovery

Spawn materially independent Luna/medium discovery lanes, bounded to current repository truth.

Suggested lanes:

1. **Product truth lane** — canonical PLAN, README, current phase, next accepted roadmap work, explicit non-goals.
2. **Architecture lane** — authoritative runtime packages, provider boundary, Harness/Verification/Run Ledger ownership, dependency direction.
3. **Validation lane** — package manager, Node/runtime requirements, build/typecheck/test/verify entrypoints, focused package tests.
4. **Repository topology lane** — workspaces/packages, important docs/ADRs, generated/vendor areas, executable contracts.

Do not recursively summarize the entire repository. Each lane returns only durable facts, direct source pointers, ambiguity, and what should NOT be copied into bootstrap knowledge.

The orchestrator synthesizes one minimal discovery model and resolves conflicts by source authority, not majority vote.

---

# Phase 2 — Design the minimal target bootstrap

Create only the smallest durable target-local context justified by discovery.

The bootstrap should answer a fresh agent's questions:

```text
What is this product?
What document owns product intent/roadmap?
What are the current implemented boundaries versus future architecture?
Where does a requested change belong?
What validation command proves repository health?
What safety/approval boundaries must not be bypassed?
What known project rules materially affect implementation?
```

Prefer pointers to target truth over duplicated content.

Expected minimal categories may include:

- target workspace identity and source authority;
- package/System routing only if real boundaries justify it;
- architecture/ownership pointers;
- validation routes;
- compact project memory/decision notes for facts not already canonical elsewhere;
- model-routing/cost policy needed for M5 execution.

Do NOT automatically reproduce the full Game-Studios `.axit` tree. Do not create empty registries, Profile catalogs, Skills, Workflows, Capability sets, or Runtime Bindings without a demonstrated target need.

Every created bootstrap artifact must state whether it is target canonical truth, a pointer to canonical truth, or M5 execution metadata.

---

# Phase 3 — Independent bootstrap review

A fresh Luna/medium verifier reviews the proposed bootstrap before it becomes the basis for product work.

Required criteria:

- no conflict with current `docs/PLAN.md`;
- architecture detail does not pretend future components already exist;
- no duplicated executable contract or copied roadmap truth that can silently drift;
- no speculative agent/profile/skill/workflow catalog;
- validation routes point to actual current commands/files;
- model/cost policy remains Sol/xhigh primary and Luna/medium children;
- bootstrap can be removed without changing product runtime behavior.

Verdict: PASS / FAIL / BLOCKED using normal verify semantics. Bounded repairs may fix bootstrap metadata only.

---

# Phase 4 — Fresh-context continuity probe

After at least one checkpoint, spawn a fresh Luna/medium child with no chat transcript and no prior discovery handoff beyond direct bootstrap pointers plus the target task question.

The probe must independently answer, with source pointers:

- product identity and canonical roadmap source;
- current implementation phase versus target architecture;
- authoritative Agent Runtime location;
- where a new bounded agent-runtime change should likely be routed;
- repository-wide validation command;
- one explicit non-goal or boundary that prevents overbuilding.

The probe must not need the M1–M4 conversation or full Game-Studios history.

If it cannot reconstruct these facts, repair bootstrap routing/context and rerun the probe before product task selection.

---

# Phase 5 — Select and freeze one REAL Axit-Code task

Use current target source plus current canonical PLAN to identify a bounded task that is already authorized by product intent and current roadmap.

Current historical evidence suggested profile/context loader work after Agent Foundation, but M5 must reacquire current target state and must not assume that remains the next task.

Reject candidates requiring invented schema, provider choice, UX behavior, public API, architecture ownership, or roadmap sequencing.

Freeze an explicit task contract before implementation:

```text
id
type = REAL
accepted source/requirement
goal
allowed files/scope
forbidden scope
required evidence
supporting evidence
validation commands
architecture/public-contract blockers
provenance expectation
repair budget
```

Independent Luna/medium contract reviewer must approve the frozen task before implementation.

If no safe REAL task has sufficient accepted intent, stop with `PRODUCT_INTENT`; do not substitute a benchmark.

---

# Phase 6 — Autonomous bounded implementation

Delegate implementation to a Luna/medium worker.

Rules:

- smallest change satisfying frozen acceptance;
- preserve unrelated state;
- follow current target architecture/ADR/source authority;
- no speculative subsystem or framework growth;
- no public-contract/ownership redesign;
- run focused checks during implementation;
- worker does not issue final verdict.

On stall or incomplete result: distill context -> steer/resume -> fresh Luna/medium replacement -> decompose if needed. No silent model escalation.

---

# Phase 7 — Target-native verification

A fresh Luna/medium verifier reacquires current evidence.

REQUIRED evidence must include:

- frozen acceptance criterion evidence;
- relevant focused automated tests/static checks;
- repository/package validation appropriate to changed scope;
- `npm run verify` when the frozen task or current project policy requires repository-wide verification;
- bounded source/scope inspection;
- no architecture/roadmap overclaim.

Do not relabel ordinary target tests as Game-Studios Unity Capabilities. M5 is Knowledge Plane/bootstrap validation, not Runtime Binding expansion.

If FAIL and repair is bounded, delegate separate Luna/medium repair and reacquire affected REQUIRED evidence. Maximum two repair loops for the same required failure.

---

# Phase 8 — Bootstrap quality and gap analysis

Ask what information the run actually needed that the bootstrap did not initially provide.

Classify each observed gap:

```text
NO_CHANGE
ADD_POINTER_OR_RULE
ADD_KNOWLEDGE
ROUTING_GAP
VALIDATION_GAP
FUTURE_PRODUCTIZATION_INPUT
```

Use the smallest durable fix. Do not create a new Core abstraction merely because a single target needed a pointer.

Explicitly identify bootstrap artifacts that were unused or redundant and should be removed.

The target bootstrap should become smaller or more precise when evidence shows over-collection.

---

# Phase 9 — Cost/performance review

Record, when observable:

```text
wall-clock duration
primary model/reasoning
child model/reasoning default and observed effective value
child lanes spawned
peak useful parallel lanes
sub-agent replacements
repair loops
model overrides
```

Expected model overrides: **0**.

Analyze latency by lane type. If a child was slow or weak, first attribute whether the cause was oversized context, unclear acceptance, serial dependency, repeated source discovery, weak handoff, or true reasoning difficulty.

Do not recommend a larger default child model without repeated measured evidence and explicit human review.

---

# Phase 10 — Closure, retrospective, and stop

Persist M5 closure artifacts under:

```text
.axit/milestones/M5-project-bootstrap-knowledge-plane/
```

At minimum the final handoff back to Game-Studios must contain:

```text
report.md
retrospective.md
bootstrap-manifest.md
real-task-manifest.md
```

The target workspace may hold its own bootstrap artifacts during execution. The M5 report must distinguish target canonical source from M5-generated bootstrap metadata and from local-only evidence.

Closure verifier must independently check:

- target was the real Axit-Code workspace;
- current PLAN authority was respected;
- bootstrap remained minimal and pointer-first;
- continuity probe succeeded without chat replay;
- REAL task contract was frozen before implementation;
- target-native tests/verification passed or were correctly failed/blocked;
- no speculative Profile/Skill/Workflow/Capability growth occurred;
- all child lanes complied with Luna/medium unless an explicit recorded human override existed;
- cost/performance accounting is truthful when observable;
- final closure verdict is persisted before terminal output;
- M6 has not started.

Terminal schema:

```text
Status: MILESTONE_DONE | FAILED | HARD_BLOCKER
Milestone: M5 Project Bootstrap & Knowledge Plane
Target: ngocphat03/Axit-Code
Bootstrap review: PASS | FAIL | BLOCKED
Context continuity: PASS | FAIL | BLOCKED
REAL task: <id + verdict>
Repository verification: PASS | FAIL | BLOCKED
Child routing: COMPLIANT | CONTROL_FINDING
Wall-clock: <duration or unavailable>
Child lanes: <count or unavailable>
Replacements: <count>
Repair loops: <count>
Model overrides: <count>
Bootstrap gaps: <compact>
Promotion recommendation: PROMOTE | REPAIR_AND_RERUN
```

After closure, STOP for human promotion review. Do not start M6 automatically.
