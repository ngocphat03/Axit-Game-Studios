# Incident — Stale PR used as readiness gate

Date: 2026-08-11
Status: hardened

## Incident

M6-A was initially marked dependency-gated on Axit-Code PR #5 because that draft described a rule/catalog foundation intended to precede loader work.

Fresh review showed:

```text
PR #5 created/last updated: 2026-08-07
review date: 2026-08-11
state: open / draft / unmerged
review comments: none
branch vs current release: diverged
```

Meanwhile current canonical `docs/PLAN.md` already states Phase 1 Slice 2 should implement profile loading/context loading from files with schema/diagnostics/tests.

## Root cause

A historical workflow artifact (a PR number/state) was promoted into a durable dependency without a freshness/canonical-authority check.

## Fix

Readiness gates must be based on current canonical accepted sources and actual missing semantics, not on the existence or state of a historical PR.

A stale/open/draft PR may be used as optional prior art only.

## Regression rule

Before a milestone treats a PR/branch/issue as a blocker:

1. reacquire its current state and last meaningful update;
2. compare it with current canonical branch/source authority;
3. determine whether canonical product truth explicitly requires that artifact or only the underlying semantic decision;
4. block only on the actual missing accepted decision/contract, not on the ticket/PR identifier;
5. never auto-merge/rebase/close a stale artifact merely to satisfy a milestone gate.

Example:

```text
BAD:
PR #5 unmerged -> M6-A blocked

GOOD:
current canonical sources insufficient to define required loader semantics
-> block on exact missing accepted contract
```

## Framework effect

M6-A now allows Phase 0 canonical discovery without waiting for PR #5. PR #5 remains optional historical input and non-canonical until separately accepted by normal product review.
