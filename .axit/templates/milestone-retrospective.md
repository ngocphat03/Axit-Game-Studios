# <M#> — <Milestone Name> Retrospective

Date: YYYY-MM-DD
Milestone result: PASS | FAIL | HARD_BLOCKER

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

<Anything that caused avoidable interruption, fallback, stale context, or incorrect hard-stop behavior.>

## Evidence/control review

- Was REQUIRED vs SUPPORTING evidence correct?
- Did verifier overclaim?
- Was stale evidence reused after repair?
- Were worker/verifier lanes independent?
- Did any sub-agent escalate something the orchestrator should have resolved?

## Context/memory review

<Anything that existed only in conversation and should become durable memory/decision/spec/config.>

## Auditability review

Distinguish:

- evidence reproducible from the canonical pushed branch;
- evidence acquired from separately tracked/untracked local Systems;
- ephemeral runtime/editor evidence.

Do not force Git tracking solely for auditability classification.

## Framework changes justified

Required:

<Smallest correct hardening changes.>

Not justified:

<Speculative Profile/Skill/Workflow/Capability growth rejected.>

## Promotion recommendation

PROMOTE | REPAIR_AND_RERUN

The milestone executor must persist this retrospective before returning MILESTONE_DONE and must not automatically start the next milestone.
