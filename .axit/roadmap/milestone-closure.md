# Axit Milestone Closure Contract

Status: active

This contract applies to every milestone-specific execution plan.

A milestone executor must not return `MILESTONE_DONE` until closure artifacts are persisted, a closure verifier has checked them, the verifier's **actual returned result** has itself been persisted, and that post-verdict state has been consistency-checked.

## Required closure artifacts

For milestone `<id>-<slug>` create/update:

```text
.axit/milestones/<id>-<slug>/report.md
.axit/milestones/<id>-<slug>/retrospective.md
```

Use the current milestone templates when applicable. Target-repository productization milestones may store equivalent target-local execution metadata, but must preserve the same closure semantics.

Material pipeline incidents should become durable memory/rules when they create or change a reusable constraint.

## Closure sequence

```text
scenario execution complete
  -> final independent scenario verification
  -> documentation/state consistency scan
  -> write DRAFT milestone report
  -> write DRAFT retrospective
  -> encode durable incident hardening
  -> closure verifier checks artifacts/evidence boundaries
  -> verifier RETURNS actual PASS / FAIL / BLOCKED
  -> persist that actual returned verdict into durable artifacts
  -> fresh read-only post-verdict consistency audit
  -> MILESTONE_DONE when permitted
  -> STOP for human promotion review
```

A pre-verdict draft may truthfully contain:

```text
Closure verifier: PENDING
```

`PENDING` at this draft stage is **not itself a verifier failure**. The verifier must evaluate evidence, scope, consistency, and whether the artifact is clearly pre-verdict. It must not fail solely because its own not-yet-returned verdict is still marked PENDING.

After the verifier actually returns, `PENDING` becomes invalid: the exact returned PASS / FAIL / BLOCKED must be persisted before the post-verdict consistency audit and terminal result.

A predicted terminal PASS written **before** the closure verifier runs is only a draft expectation and must not be represented as the verifier's actual result.

Persisting the verifier verdict after it returns is an allowed closure-metadata write. It is not a product mutation and does not invalidate verifier independence.

The closure verifier's console/output verdict is not enough by itself. Do not open or execute the next milestone automatically.

## Mandatory retrospective checks

Before closure, explicitly inspect:

- sub-agent stalls/replacements and whether escalation was necessary;
- hard-stop behavior that fired too early or too late;
- setup assumptions that caused interruption or fallback;
- transport/runtime instability and whether recovery classification was correct;
- REQUIRED vs SUPPORTING evidence and verdict correctness;
- stale evidence after repair;
- worker/verifier independence;
- stale documentation/current-state references;
- effective model/reasoning/runtime configuration versus intended configuration;
- child model-routing compliance with `.axit/policies/model-routing.md`;
- context that existed only in conversation and should become durable memory;
- auditability across Workspace/System/target repositories and ephemeral runtime evidence;
- unnecessary framework growth;
- whether child-lane count was proportional to materially independent work or dominated by orchestration/checkpoint churn.

## Evidence provenance

Use the narrowest truthful provenance class available to the milestone. For Game-Studios Workspace/System work the stable classes remain:

```text
workspace-canonical-pushed
system-canonical-pushed
local-or-separately-tracked
ephemeral-runtime
```

For target-repository productization, also distinguish canonical product truth from removable execution/bootstrap metadata. Execution metadata must never masquerade as product roadmap, architecture, or runtime authority.

Do not force Git tracking or change repository topology solely to upgrade provenance classification.

## Incident hardening rule

For every meaningful repeatable incident:

```text
incident
  -> root cause
  -> smallest framework/config fix
  -> regression protection
  -> future milestone inherits the fix
```

Do not create new Profile, Skill, Workflow, Capability, or binding merely because an incident occurred. Choose the smallest correct layer.

## Model / cost policy audit

Every long autonomous milestone must follow `.axit/policies/model-routing.md`.

Allocation policy:

```text
primary orchestrator = gpt-5.6-sol / xhigh
child preferred      = gpt-5.6-luna / medium when supported
child compat fallback= gpt-5.6-terra / medium when Luna is unavailable
child Sol            = forbidden without explicit human override
```

At readiness and closure, record configured and effective values when observable. Do not claim configured intent as observed runtime truth.

A closure verifier must distinguish:

```text
PREFERRED_LUNA
COMPAT_TERRA
HUMAN_OVERRIDE
CONTROL_FINDING
```

Terra/medium caused solely by observed Luna unavailability is compliant and is not a human override.

Child underperformance is handled through context sharpening, steer/resume, allowed-tier replacement, or decomposition before any explicit human model override is considered.

## Performance accounting

For long autonomous milestones, record when observable:

```text
execution_to_preclosure
terminal_end_to_end
primary model/reasoning
child route/model/reasoning
child lanes spawned
peak useful parallel lanes
replacements
repair/recovery loops
human model overrides
```

`terminal_end_to_end` is the primary milestone latency metric because closure/verifier time is part of the real user wait. `execution_to_preclosure` may be retained as a secondary diagnostic metric.

Do not set a hard child-lane threshold from one run. Instead, review whether new lanes represented independent work or unnecessary orchestration churn. Do not spawn a child merely to create a phase/checkpoint/report message when an existing same-responsibility lane can continue safely.

Fresh independent verification remains required even when it adds a lane.

## Reasoning/config readiness

A long autonomous milestone must verify the effective primary and child model/reasoning settings during readiness when the runtime exposes them.

If the milestone requires a primary reasoning level and the session resolves lower, record the mismatch and repair/stop according to milestone policy.

If child availability metadata is exposed, use it. If not, report effective child metadata as unavailable rather than inferring it. Never treat absence of Luna as permission to fall back to Sol.

## Post-verdict persistence regression

After a closure verifier returns terminal PASS/FAIL/BLOCKED:

1. persist that **actual returned** closure result in the milestone report;
2. persist matching closure state in the retrospective and other required manifests/state;
3. record any post-verdict changes as closure metadata only unless a product repair is explicitly reopened;
4. run one fresh read-only consistency check across durable closure artifacts;
5. only then emit `MILESTONE_DONE` when the closure result permits it.

These are closure defects:

```text
console says final PASS but durable files still say pending
pre-written files predict PASS before verifier, verifier later PASSes, but actual result is never persisted afterward
post-verdict artifacts still say PENDING after the verifier has returned
```

This is **not** a closure defect by itself:

```text
clearly pre-verdict draft says PENDING before the verifier has run
```

Repair closure artifacts only; do not rerun unaffected product/runtime evidence.

## Post-promotion state compaction

After human promotion, keep detailed completed history in milestone-specific report/retrospective/manifests/promotion review.

Compact `.axit/state/active.md` to current execution truth only:

- current authorized/designed milestone and plan pointer;
- promoted milestone state;
- stable foundations needed for routing;
- unresolved dependency/debt/blockers relevant to the next run;
- model-routing/cost policy pointer;
- next action.

Do not carry completed milestone transcripts forward in active state.

## Promotion boundary

The milestone report may recommend:

```text
PROMOTE
REPAIR_AND_RERUN
```

Only human review promotes the milestone. `MILESTONE_DONE` means execution/closure finished, not that the next milestone is authorized.
