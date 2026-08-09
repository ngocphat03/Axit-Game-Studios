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

## 3. Classify evidence requirements

For each material criterion, choose the smallest useful evidence type and classify each proposed check as either:

- `REQUIRED` — without this evidence, the criterion cannot be concluded from current project evidence, or project rules explicitly require this check;
- `SUPPORTING` — useful additional confidence, but the criterion can already be concluded from stronger or sufficient evidence.

Prefer, when applicable:

1. compile/build/static checks;
2. focused automated tests;
3. integration tests or machine-readable state;
4. runtime/editor observations;
5. screenshots, walkthroughs, playtests, or human judgment for genuinely visual or experiential behavior.

Do not promote a check to `REQUIRED` merely because it would be nice to run or because a tool exists for it.

Examples:

- If the accepted criterion is specifically "works in Unity Play Mode", runtime evidence is `REQUIRED`.
- If deterministic tests prove the rule and current project evidence proves the relevant wiring, an unavailable Play Mode check may be `SUPPORTING`; record it as residual risk instead of blocking automatically.
- If project rules explicitly require a compile, build, integration test, screenshot, or approval for this change type, that evidence is `REQUIRED` even when other evidence looks convincing.

Do not require manual evidence when deterministic evidence can prove the criterion more reliably.

## 4. Inspect the current change and resolve the target

Review the relevant diff and affected boundaries.

Confirm that the requested system, feature, or artifact maps to an identifiable target in the current workspace before evaluating behavior.

Distinguish these cases:

- The target is identifiable and the requirement says an implementation/artifact must exist, but current repository evidence shows it is absent -> criterion `FAILED`.
- The user's term cannot be mapped reliably to a project target, or there is credible reason the relevant implementation is outside the current workspace -> criterion `UNRESOLVED`; clarify or return `BLOCKED` if material.
- The implementation exists but contradicts an accepted criterion -> criterion `FAILED`.

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

For every skipped or unavailable check, record whether it was `REQUIRED` or `SUPPORTING`.

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
Criterion -> Evidence -> Requirement -> Result
```

Each material criterion must be one of:

- `PROVEN`;
- `FAILED`;
- `UNRESOLVED`.

Do not count an implementation plan, diff description, code review comment, or model assertion as behavioral proof by itself.

Treat absence as evidence when absence itself is observable and contradicts an accepted requirement. Do not label a known-missing required implementation as `UNRESOLVED` merely because runtime execution is unavailable.

## 8. Issue the verdict

Use exactly one final verdict:

- `PASS` — all required criteria are `PROVEN`. Missing `SUPPORTING` checks may remain as skipped checks or residual risk and do not block PASS by themselves.
- `FAIL` — at least one required criterion is `FAILED`, including a required implementation/artifact that is demonstrably absent or a material regression that is demonstrated.
- `BLOCKED` — no required criterion is known to have failed, but at least one required conclusion cannot be reached because essential intent, target resolution, evidence, tooling, or environment capability is unavailable.

Verdict precedence:

1. A demonstrated required failure -> `FAIL`, even if other supporting checks are unavailable.
2. Otherwise, missing essential required evidence -> `BLOCKED`.
3. Otherwise, when all required criteria are proven -> `PASS`.

Do not convert missing required evidence into PASS. Do not convert missing supporting evidence into BLOCKED automatically.

# Stop / Handoff Conditions

Stop or hand off instead of guessing when:

- intended behavior is materially ambiguous -> Game Designer;
- the requested target cannot be reliably identified -> clarify or return `BLOCKED` if the target is essential;
- verification reveals a public-contract, state-ownership, dependency-direction, or architecture conflict -> Technical Architect;
- evidence shows an implementation defect requiring code/data changes -> Implementation Engineer;
- required evidence cannot be produced in the current environment -> return `BLOCKED` unless a required failure is already demonstrated;
- the requested scope expands materially beyond the original change -> surface the scope change before continuing.

If a fix is made after a `FAIL`, run verification again against the new current change state. Do not reuse the old verdict.

# Output

Return a concise verification report containing:

```text
Verdict: PASS | FAIL | BLOCKED

Scope:
- what was verified

Evidence:
- criterion -> evidence -> REQUIRED|SUPPORTING -> result

Findings:
- failures, blockers, regressions, or none

Skipped / unavailable checks:
- check -> REQUIRED|SUPPORTING -> impact on verdict

Residual risk:
- remaining uncertainty or none
```

Keep the report traceable and proportional to the change. The goal is confidence supported by evidence, not verification ceremony.
