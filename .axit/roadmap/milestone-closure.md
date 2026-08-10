# Axit Milestone Closure Contract

Status: active

This contract applies to every milestone-specific execution plan.

A milestone executor must not return `MILESTONE_DONE` until closure artifacts are persisted, a closure verifier has checked them, and the verifier's final result has itself been persisted and consistency-checked.

## Required closure artifacts

For milestone `<id>-<slug>` create/update:

```text
.axit/milestones/<id>-<slug>/report.md
.axit/milestones/<id>-<slug>/retrospective.md
```

Use:

```text
.axit/templates/milestone-report.md
.axit/templates/milestone-retrospective.md
```

Material pipeline incidents may also be appended to `.axit/memory/decision-log.md` when they create or change a durable rule, config expectation, limitation, or regression requirement.

## Closure sequence

```text
scenario execution complete
  -> final independent scenario verification
  -> documentation/state consistency scan
  -> write milestone report
  -> write retrospective
  -> encode durable incident hardening
  -> closure verifier checks artifacts/evidence boundaries
  -> persist closure-verifier verdict into report + retrospective + active state
  -> fresh read-only post-verdict consistency audit
  -> MILESTONE_DONE
  -> STOP for human promotion review
```

The closure verifier's console/output verdict is not enough by itself. `MILESTONE_DONE` may be emitted only after durable artifacts no longer describe the milestone as pending the verifier that has already completed.

Do not open or execute the next milestone automatically.

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
- auditability across the Workspace repository, independently tracked System repositories, local-only evidence, and ephemeral runtime evidence;
- unnecessary framework growth that should be rejected.

## Evidence provenance classes

Use the narrowest truthful class for each material artifact/evidence item:

```text
workspace-canonical-pushed
system-canonical-pushed
local-or-separately-tracked
ephemeral-runtime
```

`workspace-canonical-pushed` means the evidence is reproducible from the canonical pushed Workspace repository/ref.

`system-canonical-pushed` means a System has its own canonical repository and the evidence is reproducible there. Record, when known:

```text
system id
repository
ref
commit
```

Do not assume the root Workspace repository owns a System's Git history merely because the System is mounted under `src/`.

`local-or-separately-tracked` means the current evidence is not proven reproducible from a canonical pushed Workspace/System commit.

`ephemeral-runtime` means the evidence exists only as a live command/editor/runtime observation.

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
child preferred      = gpt-5.6-luna / medium
child compat fallback= gpt-5.6-terra / medium when Luna is unavailable
child Sol            = forbidden without explicit human override
```

At readiness and closure, record configured and effective values when observable. Do not claim configured intent as observed runtime truth.

A closure verifier must distinguish:

- `PREFERRED_LUNA` — Luna/medium was actually available and used;
- `COMPAT_TERRA` — Luna was unavailable and Terra/medium was used truthfully;
- `HUMAN_OVERRIDE` — an explicit bounded user-authorized deviation;
- `CONTROL_FINDING` — unapproved Sol child use, reasoning above medium, hidden fallback, or false model claims.

Terra/medium caused solely by observed Luna unavailability is compliant and is not a human override.

Child underperformance must be handled by context sharpening, steer/resume, replacement, or decomposition before any human-authorized model override is considered.

When observable, milestone reports/retrospectives should include child lanes spawned, replacements, repair/recovery loops, wall-clock duration, child route, and human model override count.

## Reasoning/config readiness

A long autonomous milestone must verify the effective primary and child model/reasoning settings during readiness when the runtime exposes them.

If the milestone requires a primary reasoning level and the session resolves lower, do not silently treat configured intent as active. Record the mismatch and repair/stop according to milestone policy.

If child availability metadata is exposed, use it. If not, report effective child metadata as unavailable rather than inferring it. Never treat absence of Luna as permission to fall back to Sol.

## Post-verdict persistence regression

After a closure verifier returns terminal PASS/FAIL/BLOCKED:

1. persist that exact closure result in the milestone report;
2. persist matching closure state in the retrospective;
3. update `.axit/state/active.md` to the same terminal technical state;
4. run one fresh read-only consistency check across those durable artifacts;
5. only then emit `MILESTONE_DONE` when the closure result permits it.

A console-only final verdict with durable files still saying `pending final verifier` is a closure defect and must be repaired without rerunning unaffected product/runtime evidence.

## Post-promotion state compaction

After human promotion, keep detailed scenario/checkpoint history in the milestone report, retrospective, and scenario manifest.

Compact `.axit/state/active.md` to current execution truth only:

- currently authorized milestone and plan pointer, or explicit `none active`;
- promoted milestone pointers;
- stable bindings/foundations needed for routing;
- unresolved debt/blockers relevant to the next run;
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
