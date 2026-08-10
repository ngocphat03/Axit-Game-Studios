# Axit Milestone Closure Contract

Status: active

This contract applies to every milestone-specific execution plan.

A milestone executor must not return `MILESTONE_DONE` until closure artifacts are persisted and a closure verifier has checked them.

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
  -> final independent verification
  -> documentation/state consistency scan
  -> write milestone report
  -> write retrospective
  -> encode durable incident hardening
  -> closure verifier checks artifacts/evidence boundaries
  -> MILESTONE_DONE
  -> STOP for human promotion review
```

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

## Reasoning/config readiness

A long autonomous milestone must verify the effective primary and sub-agent model/reasoning settings during its readiness phase.

If the milestone declares a required effective reasoning level and the primary session resolves to a lower value, do not silently treat the configured value as active. Record the mismatch and repair/stop according to the milestone's execution-readiness policy.

## Post-promotion state compaction

After human promotion, keep detailed scenario/checkpoint history in the milestone report, retrospective, and scenario manifest.

Compact `.axit/state/active.md` to current execution truth only:

- current milestone and plan pointer;
- promoted milestone pointers;
- stable bindings/foundations needed for routing;
- unresolved debt/blockers relevant to the next run;
- next action.

Do not carry completed milestone transcripts forward in active state.

## Promotion boundary

The milestone report may recommend:

```text
PROMOTE
REPAIR_AND_RERUN
```

Only human review promotes the milestone. `MILESTONE_DONE` means execution/closure finished, not that the next milestone is authorized.
