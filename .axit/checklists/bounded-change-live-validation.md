# bounded-change Live Validation

Date: 2026-08-09
Status: PASS

## Purpose

Record the live Antigravity evidence used to promote the first Axit Core Workflow from `validation` to `active`.

Workflow under test:

```text
bounded-change
  -> implement-change
  -> verify-change
```

The test intentionally used a medium-sized vertical slice rather than a one-line bug fix.

## Test task

Add armor damage reduction to the existing QuickGun shared damage pipeline while preserving existing damage behavior.

Required behavior included:

- entity armor clamped to `0.0..0.8`;
- damage order remains hit-zone multiplier -> armor reduction -> health reduction;
- armor `0` causes no reduction;
- armor `0.25` applies 25% reduction;
- values below zero behave as zero;
- values above `0.8` behave as `0.8`;
- Head/Hair `2x` and Body `1x` multipliers continue to work;
- existing `baseDamage == 0` behavior remains correct;
- Player and Bot continue through the same damage path;
- focused deterministic tests cover the new rule and regressions;
- no new package/framework, networking-topology change, or unrelated combat refactor.

## Observed implementation

Antigravity completed the Workflow against the existing QuickGun slice with a bounded three-file change:

- `DamageCalculator.cs`;
- `BaseEntity.cs`;
- `DamageCalculatorTests.cs`.

Observed behavior:

- added clamped `BaseEntity.Armor` range `0..0.8`;
- preserved multiplier -> armor -> health ordering;
- preserved the shared Player/Bot path;
- added seven focused armor cases;
- avoided package, topology, prefab, and unrelated-system changes.

## Verification evidence

The Workflow ended with:

```text
Workflow: bounded-change
Status: COMPLETE
Verification: PASS
```

Evidence reported by Antigravity:

- all 15 deterministic tests passed;
- static damage-path assertions passed;
- full Unity build was unavailable because generated package-cache sources were missing;
- unavailable Unity build evidence was treated as supporting rather than required for the bounded deterministic criteria;
- no missing supporting check was incorrectly converted into `BLOCKED`.

## What this validates

This live run demonstrates that the current Core composition can handle more than a trivial edit:

1. a new domain rule was integrated into an existing pipeline;
2. multiple existing behaviors were preserved as regressions;
3. implementation remained bounded across several files;
4. the implementation and verification responsibilities remained separate;
5. evidence classification remained consistent with `verify-change` semantics;
6. the Workflow completed only after the current change received verification `PASS`.

The run does not prove every transition branch. `FAIL -> repair -> reverify`, `BLOCKED`, design handoff, and architecture handoff remain regression behaviors defined by the Workflow and should be exercised opportunistically when real work naturally produces those conditions. They are not a reason to keep the happy-path composition in perpetual validation after the medium vertical slice succeeded.

## Promotion decision

Promote:

```text
bounded-change: validation -> active
core status: core-workflow-v1
```

Keep the Core stable at:

- 4 Profiles;
- 2 Skills;
- 1 Workflow.

Do not add another Core Skill or Workflow merely to increase catalog coverage.

## Next phase

Begin Project Layer / specialization design.

The next design problem is not another Core procedure. It is how a real game project declares its own context, rules, architecture truth, domain knowledge, selected specialization, and Antigravity-visible capabilities while continuing to reuse the small Core without copying the legacy Game Studios catalog.
