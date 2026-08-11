# M6-B Selection Review — Context Builder Foundation

Date: 2026-08-11
Status: accepted-for-design / integration-gated / NOT_AUTHORIZED_FOR_EXECUTION

## Decision

After human promotion of M6-A, select the next bounded Axit-Code productization slice:

```text
M6-B — Context Builder Foundation
```

M6-B consumes promoted loader records and constructs the first deterministic, source-aware context representation for model reasoning.

This is a design decision only. It does not authorize an autonomous M6-B implementation run.

## Why this slice

Current Axit-Code canonical execution flow places Context Builder immediately after profile/context acquisition. It combines requirement/constraints, project inspection, profile/workflow/skills, relevant source/artifacts, prior task results, and allowed tool descriptors while preserving provenance.

M6-A deliberately stopped before reference resolution, profile selection, relevance selection, token budgeting, or context construction. A Context Builder slice is therefore the next smallest product capability that uses M6-A output without jumping to Harness, Tool Gateway, Run Ledger, provider integration, CLI orchestration, or Unity.

## Integration gate

M6-A was validated on:

```text
ngocphat03/Axit-Code@feature/m6a-loader
```

Human promotion does not automatically merge that product diff into `release`.

Before any M6-B execution, the selected writable Axit-Code baseline must contain the promoted M6-A loader implementation plus accepted ADR-002 and canonical fixture.

If it does not, stop with:

```text
HARD_BLOCKER: M6A_PRODUCT_BASELINE_NOT_INTEGRATED
```

Do not silently copy loader files, switch to a stale branch, merge, cherry-pick, or open a PR merely to satisfy this gate unless the user separately authorizes that integration operation.

## Initial product boundary

M6-B should prove only a provider-neutral context-building boundary. It must not automatically own:

```text
profile auto-selection
Harness permission enforcement
Tool Gateway execution
Run Ledger persistence
provider API calls
planning loop orchestration
CLI commands
Unity integration
Capability/Binding runtime
M6-C+
```

Reference resolution and relevance/token-budget semantics are M6-B discovery questions, not assumptions.

## Design principle

Start from current canonical Axit-Code truth and promoted M6-A records. Do not copy Game-Studios context structures wholesale.

A valid first Context Builder contract must preserve source identity and must not turn Markdown/source content into system policy or execution authority.
