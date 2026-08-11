# M6-A Selection Review

Date: 2026-08-11
Status: accepted-for-design / ready-for-canonical-discovery

## Decision

Decompose M6 — Axit-Code Productization into bounded slices.

Select first slice:

```text
M6-A — Profile / Rule / Workflow / Knowledge Loader Foundation
```

Do **not** combine Context Builder, Harness, Tool Gateway, Run Ledger, Unity integration, provider integration, or CLI run orchestration into M6-A.

## Canonical basis

Current `Axit-Code/release` `docs/PLAN.md` remains canonical and explicitly says Phase 1 Slice 2 should implement a profile loader and context builder from files with schema/diagnostics/tests.

M6-A intentionally takes only the **loader foundation** portion first; Context Builder remains M6-B.

## PR #5 reassessment

PR #5 `docs: establish AxitCode and Game Design rules` was created and last updated on 2026-08-07. At 2026-08-11 review it remains open/draft/unmerged, has no review comments, and its branch has diverged from current `release`.

Therefore PR #5 is **historical design input, not a readiness dependency and not canonical truth**.

Rules:

- do not auto-merge/rebase/close PR #5;
- do not copy its draft frontmatter/taxonomy into runtime merely because it exists;
- M6-A Phase 0 may read it as optional prior-art context;
- current `release`, `docs/PLAN.md`, accepted ADRs/architecture, and current product source own readiness decisions.

M6-A may start Phase 0 discovery now.

If current canonical sources are insufficient to freeze a loader contract without inventing material schema/taxonomy semantics, then stop for the **actual missing decision**, e.g. `PRODUCT_INTENT`, `PUBLIC_CONTRACT_DECISION`, or `KNOWLEDGE_FOUNDATION_NOT_ACCEPTED` with evidence describing exactly what canonical input is missing.

Do not block merely because PR #5 is unmerged.

## Productization boundary

M6-A migrates **semantics**, not the Game-Studios filesystem.

Do not copy the Game-Studios `.axit` tree into Axit-Code runtime. Reuse only proven invariants that fit Axit-Code's current canonical product architecture.
