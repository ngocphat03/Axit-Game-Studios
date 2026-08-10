# <M#> — <Milestone Name> Report

Status: DONE | FAILED | HARD_BLOCKER
Closure verification: PASS | FAIL | BLOCKED | pending
Review state: pending-human-review
Date: YYYY-MM-DD

## Capability proven

<What durable capability this milestone demonstrated.>

## Scenarios executed

<Representative scenarios and failure paths exercised.>

## Evidence summary

<Required evidence, verifier result, and important provenance.>

Classify material evidence using the narrowest truthful class:

- `workspace-canonical-pushed`;
- `system-canonical-pushed` — include System id + repository + ref + commit when known;
- `local-or-separately-tracked`;
- `ephemeral-runtime`.

Do not assume a System mounted under the Workspace is tracked by the Workspace repository.

## Failures and recovery

<Failures, repair/replacement loops, and whether evidence was reacquired.>

## Model / performance accounting

Record when observable:

```text
primary model/reasoning:
child default model/reasoning:
child lanes spawned:
sub-agent replacements:
repair/recovery loops:
wall-clock duration:
model overrides:
```

Expected workspace default:

```text
primary = gpt-5.6-sol / xhigh
children = gpt-5.6-luna / medium
```

Any child escalation above Luna/medium must cite an explicit bounded human override. Do not infer effective runtime values from config when they were not observable.

## Framework/config changes caused by incidents

<Only changes justified by observed pipeline weaknesses.>

## Regression protection added

<Rules/tests/templates/config validation that prevent recurrence.>

## Known residual risks

<What this milestone explicitly did not prove.>

## Promotion recommendation

PROMOTE | REPAIR_AND_RERUN

Before returning `MILESTONE_DONE`, persist the final closure-verifier result into this report, the retrospective, and active state, then run the required post-verdict consistency audit.

Do not begin the next milestone automatically. Human review is the promotion gate.
