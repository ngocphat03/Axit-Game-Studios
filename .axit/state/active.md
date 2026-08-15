# Axit Workspace Active State

Updated: 2026-08-15
Status: M6A-human-promoted-integrated-M6B-phase0-materialized

## Roadmap state

```text
M1 — Unity Runtime Binding: HUMAN_PROMOTED
M2 — Autonomous Bounded Development: HUMAN_PROMOTED
M3 — Unity Execution Coverage: HUMAN_PROMOTED
M4 — Cross-System Workspace: DEFERRED_WAITING_REAL_SECOND_SYSTEM
M5 — Project Bootstrap & Knowledge Plane: HUMAN_PROMOTED
M6-A — Loader Foundation: HUMAN_PROMOTED / PRODUCT_INTEGRATED
M6-B — Context Builder Foundation: PHASE0_MATERIALIZED / DISCOVERY_ONLY
M6-C+ — NOT_AUTHORIZED
```

## M6-A integrated baseline

Promotion review:

```text
.axit/milestones/M6A-loader-foundation/promotion-review.md
```

Validated execution branch:

```text
ngocphat03/Axit-Code@feature/m6a-loader
```

Clean product integration:

```text
PR #8 -> Axit-Code release
merge commit: 0a72c3fa8620e8a41ce7cc8d82836419b8eed7b9
```

The integration contains only the promoted product/test files:

```text
packages/axitcode-agent/src/asset-loader.ts
packages/axitcode-agent/src/index.ts
packages/axitcode-agent/test/agent-assets-loader.test.mjs
```

The previous gate is cleared:

```text
M6A_PRODUCT_BASELINE_NOT_INTEGRATED = RESOLVED
```

M6-A remains human-promoted. Its Loader Contract v1 semantics do not expand merely because the product diff is now on `release`.

## M6-B current authorization

Game-Studios design:

```text
.axit/milestones/M6B-context-builder/selection-review.md
.axit/milestones/M6B-context-builder/plan.md
```

Target-local runbook:

```text
ngocphat03/Axit-Code@release:.codex/m6b/runbook.md
```

The only authorized M6-B execution is **Phase 0 canonical discovery**.

It may determine whether current canonical Axit-Code sources are sufficient to freeze a minimal Context Builder v1 contract and must return:

```text
SUFFICIENT_TO_FREEZE
```

or:

```text
MISSING_ACCEPTED_SEMANTICS
```

with exact evidence-backed missing decisions.

M6-B Phase 0 must not implement Context Builder product code.

## Phase 0 investigation boundary

Reacquire current `release`, `docs/PLAN.md`, architecture, execution-flow, accepted ADRs, M6-A loader API/source/tests, package metadata and accepted fixtures.

Resolve only enough to decide the first Context Builder contract:

```text
API owner/call boundary
first-slice input categories
M6-A record handoff vs reference resolution
provenance shape
deterministic ordering/grouping
trusted vs untrusted content boundary
secret-sensitive input handling
relevance responsibility
size/token budget
truncation/omission diagnostics
invalid-input atomicity
tool-descriptor inclusion before Harness
real canonical scenario
```

Do not pre-accept profile auto-selection, semantic retrieval/vector DB, implicit repository search, reference graph resolution, dynamic tool exposure or provider-specific prompt formatting.

## Stable execution policy

```text
primary = gpt-5.6-sol / xhigh
child preferred = gpt-5.6-luna / medium when supported
child compatibility fallback = gpt-5.6-terra / medium when Luna is unavailable
child Sol = forbidden without explicit current human override
```

- Primary orchestrates; children handle materially independent work.
- Avoid per-checkpoint child churn.
- Phase 0 product/source/test/schema/doc writes are forbidden.
- Only `.codex/m6b/results/**` target discovery metadata may be written.
- Preserve unrelated filesystem state.
- Actual verifier verdict must be persisted after it returns, then consistency-audited.
- A pre-verdict draft may contain `PENDING`; predicted PASS is forbidden.
- Do not start M6-B implementation or M6-C automatically.

## Next action

1. Pull current `ngocphat03/Axit-Code` `release`.
2. Open a fresh trusted Codex session at Axit-Code repository root.
3. Execute `.codex/m6b/runbook.md` continuously through its Phase 0 terminal result.
4. Do not implement Context Builder code.
5. Push/share the Phase 0 result branch after the local run finishes, then stop for human/assistant review.
