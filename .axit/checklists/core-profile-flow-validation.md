# Core Profile Flow Validation

This note validates the four Axit Core Profiles against two common development flows before Core Skills or Workflows are expanded.

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
3. collect evidence from the current change;
4. prefer deterministic checks when they can prove behavior;
5. add integration/runtime/manual evidence only when needed;
6. check material regressions and architecture invariants;
7. separate proven facts from assumptions and unavailable evidence;
8. issue `PASS`, `FAIL`, or `BLOCKED` with traceable evidence.

This repeated procedure is distinct from the `quality-verifier` Profile:

- Profile = responsibility and judgment lens;
- Skill = repeatable verification procedure.

Therefore the first accepted Core Skill is `verify-change`.

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

The four Profiles cover the two tested flows without a fifth Core responsibility.

The first demonstrated procedural gap is independent completion verification, so `verify-change` is justified as Core Skill #1.
