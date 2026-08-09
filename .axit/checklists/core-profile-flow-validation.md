# Core Profile Flow Validation

This note validates the four Axit Core Profiles against common development flows before Core Skills or Workflows are expanded.

## Scope

Profiles under test:

- `game-designer`
- `technical-architect`
- `implementation-engineer`
- `quality-verifier`

Flows under test:

1. bounded feature delivery;
2. bounded bug fix.

The goal is not to prescribe one mandatory pipeline. The goal is to identify repeated procedures that deserve a Core Skill.

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

## Repeated procedure found

Both flows repeat one procedure regardless of domain, engine, genre, or networking model:

1. establish the bounded verification scope;
2. identify accepted criteria or observable expected behavior;
3. classify evidence as required or supporting;
4. collect evidence from the current change;
5. prefer deterministic checks when they can prove behavior;
6. add integration/runtime/manual evidence only when it is required or materially useful;
7. check material regressions and architecture invariants;
8. separate proven facts, demonstrated failures, assumptions, and unavailable evidence;
9. issue `PASS`, `FAIL`, or `BLOCKED` with traceable evidence.

This repeated procedure is distinct from the `quality-verifier` Profile:

- Profile = responsibility and judgment lens;
- Skill = repeatable verification procedure.

Therefore the first accepted Core Skill is `verify-change`.

## Real Codex validation — 2026-08-09

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

## Deferred candidate Skills

Do not add these yet merely because similar commands existed in the legacy repository:

- feature design;
- architecture decision;
- implementation planning;
- implementation execution;
- code review;
- project discovery;
- checkpoint/session state;
- smoke check.

Each remains a candidate until repeated use demonstrates that a stable Core procedure is needed and cannot remain ordinary Profile behavior, project rules, or domain-specific guidance.

## Validation result

The four Profiles cover the tested flows without a fifth Core responsibility.

The first demonstrated procedural gap is independent completion verification, so `verify-change` remains justified as Core Skill #1.

The first live Codex tests also confirmed that verdict semantics must explicitly distinguish required evidence from supporting evidence and demonstrated absence from unresolved target discovery. Those semantics are now part of `verify-change` and should be regression-tested before accepting Core Skill #2.
