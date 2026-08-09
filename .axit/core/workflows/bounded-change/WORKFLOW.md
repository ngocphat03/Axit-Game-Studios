---
spec_version: axit.workflow/v1
id: bounded-change
summary: Implement and independently verify one bounded change whose intent and architecture are already sufficiently resolved.
status: active
---

# Entry Conditions

Start this Workflow only when all of these are true:

- the requested change is bounded enough to identify an implementation surface;
- intended product/game behavior is sufficiently clear for implementation;
- no unresolved material architecture decision is required to begin;
- the user or project expects both implementation and independent verification, not only one of those jobs.

Do not start this Workflow merely because code may eventually be changed.

If intended behavior is materially ambiguous, hand off to `game-designer` before implementation.

If implementation requires deciding or changing a public contract, system boundary, shared-state owner, major dependency direction, topology, or project-wide technical stance, hand off to `technical-architect` before implementation.

# Participants

This Workflow composes two accepted Core Skills:

1. `implement-change`
2. `verify-change`

Responsibility lenses:

- implementation work uses `implementation-engineer`;
- final evidence judgment uses `quality-verifier`.

Do not duplicate either Skill's internal procedure here.

# Flow

## 1. Implement the bounded change

Run `implement-change` against the current accepted behavior, architecture constraints, project rules, and working-tree state.

The implementation step must return a current handoff containing:

- bounded outcome and scope;
- files changed;
- implementation-side checks and results;
- skipped/unavailable checks;
- assumptions, deviations, and known risks;
- acceptance criteria or expected behaviors ready for verification.

Implementation readiness is not workflow completion.

## 2. Independently verify the current change

Run `verify-change` against the **current** change state and the implementation handoff.

The verifier must inspect current evidence rather than inheriting the implementer's confidence.

Use the verifier's final verdict as the workflow transition input.

# Transitions

## `PASS`

When `verify-change` returns `PASS`:

- mark the Workflow complete for the bounded scope;
- preserve residual risks and skipped supporting checks in the final output;
- do not expand into adjacent cleanup or additional features.

## `FAIL`

When `verify-change` returns `FAIL`:

1. identify the concrete failed required criterion or regression;
2. decide whether the repair remains a local implementation change inside the already accepted scope and architecture;
3. if yes, run `implement-change` again using the verifier finding as the bounded defect;
4. run `verify-change` again against the new current state.

Do not reuse the previous PASS/FAIL evidence after the implementation changes.

If fixing the failure requires changing product intent, acceptance criteria, architecture, or material scope, stop the repair loop and hand off to the owning responsibility.

Do not create an unbounded retry loop. Each repair iteration must address a concrete new or still-failing verifier finding. If the same failure repeats without new actionable information, stop and surface the unresolved issue.

## `BLOCKED`

When `verify-change` returns `BLOCKED`:

- stop the Workflow;
- report the exact missing required intent, target resolution, evidence, tooling, or environment capability;
- do not modify code merely to avoid the blocker.

If the blocker is later resolved and the implementation state has not changed, resume at verification.

If the implementation state changed while blocked, verify the new current state rather than reusing stale evidence.

# Completion

This Workflow completes successfully only when the **current** bounded change receives `PASS` from `verify-change`.

The following do not count as successful completion by themselves:

- implementation finished;
- focused tests passed during implementation;
- code review found no obvious issue;
- a previous verification run passed before later edits;
- runtime evidence is unavailable but required by the accepted criterion.

Completion remains scoped to the requested bounded change and does not imply that the whole project, release, or game system is globally verified.

# Stop / Handoff Conditions

Stop or hand off instead of silently continuing when:

- product/game behavior is materially ambiguous -> `game-designer`;
- a material architecture stance must be created or changed -> `technical-architect`;
- requested scope expands beyond the accepted bounded change -> surface the scope decision;
- destructive/high-impact side effects require approval under project/runtime rules -> wait for that approval;
- verifier evidence is `BLOCKED` by required tooling/environment -> wait for evidence capability;
- repeated repair attempts do not produce new actionable information -> stop and report the unresolved failure.

This Workflow does not grant permissions and does not bypass runtime/harness policy.

# Output

Return a concise workflow result containing:

```text
Workflow: bounded-change
Status: COMPLETE | FAILED | BLOCKED | HANDED_OFF

Change:
- bounded outcome

Implementation:
- changed files
- implementation-side checks

Verification:
- PASS | FAIL | BLOCKED
- required evidence summary
- residual risk

Iterations:
- number and reason for any repair passes

Handoff / blocker:
- owning responsibility or missing capability, if any
```

Use `COMPLETE` only when the latest verification verdict is `PASS`.