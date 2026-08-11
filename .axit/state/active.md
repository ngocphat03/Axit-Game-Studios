# Axit Workspace Active State

Updated: 2026-08-11
Status: M6A-contract-accepted-implementation-materialized

## Roadmap state

```text
M1 — Unity Runtime Binding: HUMAN_PROMOTED
M2 — Autonomous Bounded Development: HUMAN_PROMOTED
M3 — Unity Execution Coverage: HUMAN_PROMOTED
M4 — Cross-System Workspace: DEFERRED_WAITING_REAL_SECOND_SYSTEM
M5 — Project Bootstrap & Knowledge Plane: HUMAN_PROMOTED
M6-A — Loader Foundation: CONTRACT_ACCEPTED_READY_FOR_IMPLEMENTATION
M6-B+ — NOT_AUTHORIZED
```

Game-Studios control plan:

```text
.axit/milestones/M6A-loader-foundation/plan.md
```

Accepted M6-A contract review:

```text
.axit/milestones/M6A-loader-foundation/accepted-contract.md
```

Target implementation runbook:

```text
ngocphat03/Axit-Code@release:.codex/m6a/runbook.md
```

## Phase 0 reviewed result

Target audit branch:

```text
ngocphat03/Axit-Code@feature/m6
```

Result:

```text
Status: PHASE0_DONE
Decision: MISSING_ACCEPTED_SEMANTICS
Verifier: PASS
Final audit: CONSISTENT
```

No Phase 0 rerun is required.

## Human-accepted Loader Contract v1

Canonical target technical decision:

```text
docs/decisions/ADR-002-loader-contract-v1.md
```

Canonical real fixture:

```text
.agents/profiles/feature-development.md
```

Accepted shape:

```text
root = .agents/
kinds = profile/rule/workflow/skill/knowledge from directory
file = *.md with strict v1 frontmatter
required = id + schema_version: 1
identity = (kind,id)
source = repo-relative path
order = lexical path
duplicate same kind/id = ERROR
precedence/override = none
status/activation/scope = not represented
references = not resolved in M6-A
legacy package-local knowledge = excluded
```

Declarative content never grants execution permission or bypasses Harness.

## Current authorization

M6-A implementation + target-native tests + independent verification + durable closure are authorized under ADR-002 and the target-local runbook.

M6-A must not implement:

```text
Context Builder
reference resolution
profile auto-selection
Harness / Tool Gateway
Run Ledger
provider integration
CLI orchestration
Unity integration
Capability/Binding runtime
M6-B+
```

Dependency/package-manifest growth is not implicitly authorized.

## Model / cost policy

```text
primary = gpt-5.6-sol / xhigh
child preferred = gpt-5.6-luna / medium when supported
child compatibility fallback = gpt-5.6-terra / medium when Luna is unavailable
child Sol = forbidden without explicit current human override
```

Current observed target runtime uses Terra/medium compatibility routing because Luna was unavailable. No silent Sol fallback.

## Closure protocol hardening

A clearly pre-verdict draft may contain:

```text
Closure verifier: PENDING
```

The verifier must not fail solely for that PENDING state. After the verifier actually returns, the exact verdict must be persisted and a fresh read-only consistency audit must confirm no stale PENDING remains.

Predicted PASS before verifier execution remains forbidden.

## Next action

1. Pull/sync local `ngocphat03/Axit-Code` `release`.
2. Open a fresh trusted Codex session at Axit-Code repository root.
3. Execute `.codex/m6a/runbook.md` continuously until `MILESTONE_DONE`, `FAILED`, or a declared `HARD_BLOCKER`.
4. Do not commit/push/PR inside the autonomous run.
5. After the run, push/share the result branch for human/assistant review.

Do not start M6-B automatically.
