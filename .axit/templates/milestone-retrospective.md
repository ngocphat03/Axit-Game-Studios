# <M#> — <Milestone Name> Retrospective

Date: YYYY-MM-DD
Milestone result: PASS | FAIL | HARD_BLOCKER
Closure verification: PASS | FAIL | BLOCKED | pending

## What worked

<What behaved correctly without user micromanagement.>

## Incidents and root causes

For each meaningful incident record:

### <Incident>
Observed:
Root cause:
Self-recovery:
Framework/config fix:
Regression protection:

## Setup/pipeline assumptions that failed

<Anything that caused avoidable interruption, fallback, stale context, incorrect hard-stop behavior, or excessive cost/latency.>

## Evidence/control review

- Was REQUIRED vs SUPPORTING evidence correct?
- Did verifier overclaim?
- Was stale evidence reused after repair?
- Were worker/verifier lanes independent?
- Did any sub-agent escalate something the orchestrator should have resolved?
- Was the closure-verifier verdict durably persisted before terminal output?

## Model / cost / latency review

Use `.axit/policies/model-routing.md` as the contract.

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

Review explicitly:

- Did every child stay at `gpt-5.6-luna / medium` unless the user explicitly authorized a bounded override?
- Did any task use a larger model when better decomposition/context would have been sufficient?
- Were useful independent read lanes parallelized?
- Were overlapping writes and Unity/editor mutations correctly serialized?
- Were finished/obsolete lanes closed promptly?
- Did child prompts contain distilled task context instead of full conversation/history?
- What concrete change should reduce the next milestone's wall-clock time or token cost without weakening evidence quality?

## Context/memory review

<Anything that existed only in conversation and should become durable memory/decision/spec/config.>

## Auditability review

Distinguish:

- `workspace-canonical-pushed` evidence reproducible from the canonical Workspace repository/ref;
- `system-canonical-pushed` evidence reproducible from a System's canonical repository/ref/commit;
- `local-or-separately-tracked` evidence not yet proven canonical in either repository boundary;
- `ephemeral-runtime` editor/runtime/command evidence.

When using `system-canonical-pushed`, record System id + repository + ref + commit when known.

Do not force Git tracking or repository-topology changes solely for auditability classification.

## Framework changes justified

Required:

<Smallest correct hardening changes.>

Not justified:

<Speculative Profile/Skill/Workflow/Capability/model escalation growth rejected.>

## Promotion recommendation

PROMOTE | REPAIR_AND_RERUN

The milestone executor must persist this retrospective, persist the final closure-verifier result across durable state, and pass the post-verdict consistency audit before returning `MILESTONE_DONE`.

Do not automatically start the next milestone.
