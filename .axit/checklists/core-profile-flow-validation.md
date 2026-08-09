# Core Profile Flow Validation

This note validates the four Axit Core Profiles against common development flows before Core Workflows are expanded.

## Scope

Profiles under test:

- `game-designer`
- `technical-architect`
- `implementation-engineer`
- `quality-verifier`

Flows under test:

1. bounded feature delivery;
2. bounded bug fix.

The goal is not to prescribe one mandatory pipeline. The goal is to identify repeated procedures that deserve Core Skills.

## Feature flow

A feature may use the Profiles in this order when each responsibility is actually needed:

```text
Game Designer
  -> clarify intended player/product behavior and acceptance criteria

Technical Architect
  -> define or confirm boundaries, ownership, interfaces, and architecture constraints

Implementation Engineer
  -> implement the bounded change inside accepted design and architecture

Quality Verifier
  -> independently map acceptance criteria to current evidence and issue a verdict
```

Profiles may be skipped when their responsibility is already resolved. A small feature with accepted design and architecture may begin at implementation. A design-only task may stop before implementation.

## Bug-fix flow

A normal bug fix should stay smaller:

```text
Existing expected behavior / defect report
  -> Game Designer only if intended behavior is ambiguous
  -> Technical Architect only if the fix changes a public contract, state owner, dependency direction, or architecture stance
  -> Implementation Engineer applies the smallest coherent fix
  -> Quality Verifier independently verifies the fix and affected regressions
```

The bug flow confirms that production coordination, specialist programmer hierarchies, and mandatory multi-agent approval chains are not Core responsibilities.

## Core Skill #1 — independent verification

Both flows repeat one verification procedure regardless of domain, engine, genre, or networking model:

1. establish the bounded verification scope;
2. identify accepted criteria or observable expected behavior;
3. classify evidence as required or supporting;
4. collect evidence from the current change;
5. prefer deterministic checks when they can prove behavior;
6. add integration/runtime/manual evidence only when it is required or materially useful;
7. check material regressions and architecture invariants;
8. separate proven facts, demonstrated failures, assumptions, and unavailable evidence;
9. issue `PASS`, `FAIL`, or `BLOCKED` with traceable evidence.

This procedure is distinct from the `quality-verifier` Profile:

- Profile = responsibility and judgment lens;
- Skill = repeatable verification procedure.

Therefore Core Skill #1 is `verify-change`.

## Real Codex validation — verify-change — 2026-08-09

The first live use of `verify-change` exposed an important verdict-semantics gap.

### Case A — damage multiplier, deterministic evidence passes, Unity runtime unavailable

Observed evidence included passing focused multiplier tests, correct prefab values, shared Player/Bot prefab wiring, and a proven projectile-to-damage code path. Unity Play Mode evidence was unavailable.

Correct interpretation depends on the accepted criterion:

- If the criterion is specifically end-to-end runtime behavior such as "health decreases correctly in Play Mode", Play Mode evidence is `REQUIRED`; unavailable runtime evidence -> `BLOCKED`.
- If the bounded criterion is calculator behavior plus current project wiring, and those are already proven, Play Mode may be `SUPPORTING`; unavailable runtime evidence does not block `PASS` and must instead appear as residual risk.

Regression rule: never mark a check `REQUIRED` merely because it would increase confidence. Requirement status comes from the accepted criterion or project rules.

### Case B — multiplier has a demonstrated zero-damage defect

A focused test demonstrated that `baseDamage == 0` produced `1` instead of `0`. Other tests and static wiring evidence passed, while full Unity runtime remained unavailable.

Expected verdict: `FAIL`.

Regression rule: a demonstrated required failure takes precedence over unavailable supporting evidence. Do not return `BLOCKED` when a required criterion is already known to fail.

### Case C — requested shield/barrier system is not found

The user requested verification of a shield system, but no matching implementation, prefab, scene wiring, or tests were found in the inspected workspace.

Correct interpretation depends on target resolution:

- If project requirements identify a shield/barrier system that must exist and repository evidence demonstrates it is absent -> `FAIL`.
- If the user's term cannot be reliably mapped to a project target, or the relevant implementation may be outside the current workspace -> `BLOCKED` or request clarification.

Regression rule: distinguish a demonstrably missing required implementation from an unresolved target name.

## Verdict semantics regression matrix

```text
All required criteria proven
+ supporting check unavailable
=> PASS + residual risk

Required criterion cannot be concluded
+ no demonstrated required failure
=> BLOCKED

Required criterion demonstrably fails
=> FAIL

Required implementation demonstrably absent
=> FAIL

Requested target cannot be identified reliably
=> BLOCKED / clarification
```

The live Codex retest confirmed these semantics are now acceptable for Core v1.

## Core Skill #2 — bounded implementation

Feature delivery and bug fixing also repeat a second procedure whenever code/project mutation is actually required:

1. bound the requested change and preserve unrelated working-tree state;
2. confirm product intent and architecture are sufficiently resolved;
3. inspect only the affected implementation surface and relevant constraints;
4. choose the minimum coherent change;
5. implement without silently expanding scope or changing architecture;
6. add focused implementation-side tests/checks when practical;
7. inspect the resulting diff for unintended contract or boundary changes;
8. hand current evidence and unresolved risks to independent verification.

This procedure is distinct from the `implementation-engineer` Profile:

- Profile = implementation responsibility and local engineering judgment;
- Skill = repeatable bounded-change procedure.

The procedure remains reusable across engines and game types because engine/network/UI specialization can be supplied by project knowledge or domain Skills without changing the Core implementation loop.

Therefore Core Skill #2 is `implement-change`.

## Implementation / verification separation

The two accepted Skills intentionally form a useful pair without yet becoming a mandatory Workflow:

```text
implement-change
  -> produces bounded change + implementation-side evidence

verify-change
  -> independently evaluates the current change + evidence
```

Do not encode this pair as a Core Workflow yet. Some tasks only need verification; some tasks stop at design/architecture; and a future real project may demonstrate that another step is needed between implementation and verification.

## implement-change Codex regression cases

Before accepting a Core Workflow, test `implement-change` through Codex against at least these cases:

### Case 1 — bounded bug fix

Expected behavior:

- identify or reproduce the defect when practical;
- apply the smallest coherent fix;
- add/update a focused test when practical;
- preserve unrelated working-tree changes;
- report checks and hand off to `verify-change`;
- do not issue a final `PASS` verdict itself.

### Case 2 — ambiguous product behavior

Expected behavior:

- detect that intended behavior is unresolved;
- hand off to Game Designer before editing;
- do not choose product behavior just to continue implementation.

### Case 3 — architecture boundary change

Expected behavior:

- detect a required public-contract, state-owner, dependency-direction, topology, or project-wide stance change;
- hand off to Technical Architect before editing that boundary.

### Case 4 — dirty working tree

Expected behavior:

- identify pre-existing unrelated changes;
- do not reset, overwrite, or claim them as part of the implementation;
- keep the implementation handoff scoped to files actually changed for the task.

### Case 5 — unavailable engine/runtime tooling

Expected behavior:

- perform implementation and deterministic checks that are available when correctness is still sufficiently grounded;
- record missing runtime/editor checks explicitly;
- hand the limitation to `verify-change` rather than claiming final completion.

## Deferred candidate Skills

Do not add these yet merely because similar commands existed in the legacy repository:

- feature design;
- architecture decision;
- implementation planning;
- code review;
- project discovery;
- checkpoint/session state;
- smoke check.

Each remains a candidate until repeated use demonstrates that a stable Core procedure is needed and cannot remain ordinary Profile behavior, project rules, or domain-specific guidance.

## Validation result

The four Profiles cover the tested flows without a fifth Core responsibility.

Two repeated, responsibility-independent procedures are now justified as Core Skills:

1. `implement-change` — bounded implementation and evidence handoff;
2. `verify-change` — independent evidence-based completion judgment.

Core Workflows remain empty until live Codex use demonstrates a stable composition that is valuable enough to encode rather than merely suggested.
