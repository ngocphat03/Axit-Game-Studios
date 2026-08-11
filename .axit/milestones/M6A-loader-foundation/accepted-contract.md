# M6-A Accepted Contract — Loader Contract v1

Date: 2026-08-11
Status: HUMAN_ACCEPTED_READY_FOR_IMPLEMENTATION

## Phase 0 result reviewed

Target evidence branch:

```text
ngocphat03/Axit-Code@feature/m6
```

Phase 0 returned:

```text
Status: PHASE0_DONE
Decision: MISSING_ACCEPTED_SEMANTICS
Verifier: PASS
Final audit: CONSISTENT
```

Review found the stop valid: current canonical Axit-Code direction authorized loader work but did not yet freeze the first schema/discovery/error/fixture contract.

No Phase 0 rerun is required.

## Human decision

The user accepted Loader Contract v1 on 2026-08-11.

Canonical target technical contract is now:

```text
ngocphat03/Axit-Code@release
docs/decisions/ADR-002-loader-contract-v1.md
```

Canonical target fixture:

```text
.agents/profiles/feature-development.md
```

This acceptance resolves the Phase 0 missing semantics for M6-A implementation. It does not authorize Context Builder or M6-B.

## Accepted v1 shape

```text
root: .agents/
kinds: profiles, rules, workflows, skills, knowledge
kind source: directory
file form: *.md + strict v1 frontmatter
required: id + schema_version: 1
identity: (kind, id)
source identity: repo-relative path
discovery order: lexical repo-relative path
duplicate (kind,id): ERROR
same id across kinds: allowed
precedence/override: none
status/activation/scope: not represented
references: not resolved in M6-A
legacy package-local knowledge: excluded from loader input
real fixture: .agents/profiles/feature-development.md
```

Declarative artifacts do not grant execution permission or bypass Harness.

## Implementation authorization

M6-A implementation is authorized under the target-local runbook:

```text
ngocphat03/Axit-Code@release:.codex/m6a/runbook.md
```

Allowed product layer is the authoritative `@axitcode/agent` package plus bounded tests. Dependency/package-manifest growth is not implicitly authorized.

M6-A must stop for human promotion review after durable closure. M6-B remains unauthorized.

## Phase 0 verifier-protocol incident

Phase 0's first closure verifier returned FAIL only because a clearly pre-verdict draft still said `Verifier: PENDING`; a second verifier then PASSed after persistence repair.

This is orchestration overhead, not a product/discovery failure.

Durable rule:

```text
pre-verdict draft may say PENDING
-> verifier must evaluate evidence/scope, not fail solely for PENDING
-> verifier returns actual verdict
-> actual verdict is persisted
-> post-verdict audit requires no stale PENDING
```

A predicted PASS written before verification is still forbidden.
