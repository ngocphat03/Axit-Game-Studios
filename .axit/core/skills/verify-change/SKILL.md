---
name: verify-change
description: Verify a bounded code or project change against accepted requirements and current evidence, then return PASS, FAIL, or BLOCKED. Use after implementation, for fix verification, or when asked whether a change is actually complete; do not use as the implementation workflow itself.
---

# Purpose

Independently determine whether a bounded change satisfies its accepted behavior with sufficient current evidence.

Use the `quality-verifier` responsibility lens. Verification is separate from implementation: do not repair the change while issuing the verdict.

# Inputs

Use only inputs relevant to the bounded change:

- the user request, defect description, or accepted acceptance criteria;
- relevant design intent when player-facing behavior matters;
- relevant architecture constraints and invariants;
- the current diff or changed files;
- implementation handoff notes when available;
- relevant tests, build/static checks, runtime/editor checks, and produced artifacts;
- project rules that define required validation or approvals.

If explicit acceptance criteria do not exist, derive the smallest observable expected-behavior list from the explicit request and existing project evidence. Mark derived criteria as inferred. Do not invent new product requirements.

# Procedure

## 1. Bound the verification scope

State what change is being verified and what is outside this verification pass.

Identify the current change set before evaluating behavior. Do not verify an assumed or stale implementation state.

## 2. Build the criterion list

Collect the accepted requirements or observable expected behaviors that matter for this change.

For each criterion, classify it as one of:

- explicit;
- inferred from existing project evidence;
- unresolved.

If a material criterion is unresolved because product intent is ambiguous, stop and hand the question to the Game Designer responsibility.

## 3. Identify evidence needed

For each material criterion, choose the smallest useful evidence type.

Prefer, when applicable:

1. compile/build/static checks;
2. focused automated tests;
3. integration tests or machine-readable state;
4. runtime/editor observations;
5. screenshots, walkthroughs, playtests, or human judgment for genuinely visual or experiential behavior.

Do not require manual evidence when deterministic evidence can prove the criterion more reliably.

## 4. Inspect the current change

Review the relevant diff and affected boundaries.

Check for material concerns such as:

- behavior outside the requested scope;
- changed public contracts;
- changed shared-state ownership or dependency direction;
- contradictions with accepted architecture or project rules;
- missing failure or recovery behavior relevant to the change.

Architecture conflicts are verification findings, not problems to silently repair during this procedure.

## 5. Run or inspect evidence

Execute or inspect the selected checks using the project's available mechanisms.

Start with targeted checks. Expand to broader regression coverage only when the affected surface or failure risk justifies it.

Record failures, warnings, skipped checks, flaky behavior, environment limitations, and assumptions. Do not hide them behind a summary.

## 6. Check affected regressions

Identify critical existing behavior adjacent to the change and verify the smallest meaningful regression set.

A bug fix should demonstrate both:

- the reported failure no longer occurs; and
- the fix did not break the important affected path around it.

A feature should demonstrate both:

- the new accepted behavior works; and
- existing behavior materially touched by the change still holds.

## 7. Map criteria to evidence

Produce a compact mapping:

```text
Criterion -> Evidence -> Result
```

Each material criterion must be one of:

- `PROVEN`;
- `FAILED`;
- `UNRESOLVED`.

Do not count an implementation plan, diff description, code review comment, or model assertion as behavioral proof by itself.

## 8. Issue the verdict

Use exactly one final verdict:

- `PASS` — current evidence supports all required criteria for the bounded scope.
- `FAIL` — current evidence shows a required criterion is not satisfied or a material regression exists.
- `BLOCKED` — a required conclusion cannot be reached because essential intent, evidence, tooling, or environment capability is unavailable.

Do not convert missing required evidence into PASS.

# Stop / Handoff Conditions

Stop or hand off instead of guessing when:

- intended behavior is materially ambiguous -> Game Designer;
- verification reveals a public-contract, state-ownership, dependency-direction, or architecture conflict -> Technical Architect;
- evidence shows an implementation defect requiring code/data changes -> Implementation Engineer;
- required evidence cannot be produced in the current environment -> return `BLOCKED`;
- the requested scope expands materially beyond the original change -> surface the scope change before continuing.

If a fix is made after a `FAIL`, run verification again against the new current change state. Do not reuse the old verdict.

# Output

Return a concise verification report containing:

```text
Verdict: PASS | FAIL | BLOCKED

Scope:
- what was verified

Evidence:
- criterion -> evidence -> result

Findings:
- failures, blockers, regressions, or none

Skipped / unavailable checks:
- explicit list or none

Residual risk:
- remaining uncertainty or none
```

Keep the report traceable and proportional to the change. The goal is confidence supported by evidence, not verification ceremony.
