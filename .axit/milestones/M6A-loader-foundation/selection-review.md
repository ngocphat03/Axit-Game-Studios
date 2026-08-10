# M6-A Selection Review

Date: 2026-08-11
Status: accepted-for-design / dependency-gated

## Decision

Decompose M6 — Axit-Code Productization into bounded slices.

Select first slice:

```text
M6-A — Profile / Rule / Workflow / Knowledge Loader Foundation
```

Do **not** combine Context Builder, Harness, Tool Gateway, Run Ledger, Unity integration, provider integration, or CLI run orchestration into M6-A.

Context Builder is a later M6 slice after loader contracts are proven.

## Why this slice

Axit-Code's merged Agent Foundation work explicitly identifies Phase 1 Slice 2 as schema/loader work for declarative agent knowledge before Context Builder. Current open draft PR #5 — `docs: establish AxitCode and Game Design rules` — also states that its rule foundation is intended to precede the loader.

This matches Game-Studios evidence that knowledge/rules should be user-owned, pointer-first, provider-neutral, and separate from runtime permission policy.

## Current dependency finding

At selection time:

```text
Axit-Code PR #5
state: open
mode: draft
merged: false
purpose: rule catalog/foundation before loader
```

Therefore M6-A is designed but **not yet authorized for long implementation execution**.

Do not merge, rebase, close, or silently treat PR #5 as canonical merely to unblock M6-A.

If current Axit-Code `release` still lacks an accepted loader-input convention at execution readiness, stop with:

```text
HARD_BLOCKER: KNOWLEDGE_FOUNDATION_NOT_ACCEPTED
```

Human review decides whether PR #5 should be merged, revised, superseded, or intentionally bypassed with a new accepted foundation.

## Productization boundary

M6-A migrates **semantics**, not the Game-Studios filesystem.

Do not copy the Game-Studios `.axit` tree into Axit-Code runtime. Reuse only proven invariants that fit Axit-Code's canonical product architecture.
