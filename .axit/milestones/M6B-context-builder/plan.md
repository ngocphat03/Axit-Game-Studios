# M6-B — Context Builder Foundation

Status: designed / integration-gated / NOT_AUTHORIZED_FOR_EXECUTION

## Goal

Prove the smallest provider-neutral Context Builder boundary inside Axit-Code after M6-A Loader Foundation is available on the selected execution baseline.

Target shape:

```text
accepted explicit context inputs
  + promoted M6-A artifact records
  -> deterministic context assembly
  -> source/provenance-preserving normalized context
  -> explicit omission/truncation diagnostics when accepted
  -> target-native tests
```

M6-B must not become the planning loop, Harness, Tool Gateway, Run Ledger, provider call layer, CLI orchestration, Unity integration, or a generic retrieval system.

## Target and authority

```text
repository: ngocphat03/Axit-Code
canonical product branch: release
runtime owner: @axitcode/agent
product/roadmap authority: docs/PLAN.md
accepted loader contract: docs/decisions/ADR-002-loader-contract-v1.md
```

Canonical execution-flow evidence states that Context Builder combines requirement/constraints, project inspection, profile/workflow/skills, relevant source/artifacts, prior task results, and allowed tool descriptors, and that context must preserve provenance. It also requires relevance discipline, secret minimization, non-policy treatment of source/Markdown, and a recorded truncation strategy.

## Readiness gate

M6-A was human-promoted from validated branch `feature/m6a-loader`, but promotion does not imply that product implementation is already present on Axit-Code `release`.

Before M6-B execution:

1. resolve the exact selected Axit-Code writable baseline;
2. prove that baseline contains the promoted M6-A loader implementation/API, ADR-002, canonical fixture, and passing target-native verification;
3. if not, stop with:

```text
HARD_BLOCKER: M6A_PRODUCT_BASELINE_NOT_INTEGRATED
```

No autonomous merge/cherry-pick/rebase/PR creation is authorized by this plan.

## Model / cost policy

```text
primary = gpt-5.6-sol / xhigh
child preferred = gpt-5.6-luna / medium when supported
child compatibility fallback = gpt-5.6-terra / medium when Luna is unavailable
child Sol = forbidden without explicit current human override
```

Use a small number of materially independent lanes. Preserve fresh independent verification but avoid per-checkpoint child churn.

## M6-B Phase 0 — Canonical discovery only

M6-B implementation is not yet authorized. The first future run must be discovery/contract-freeze work only.

Reacquire current canonical sources and determine whether they are sufficient to freeze the minimal Context Builder v1 contract without inventing material runtime semantics.

Required discovery questions:

```text
1. exact Context Builder API owner and call boundary
2. accepted input categories required in the first slice
3. whether selected loader records are passed explicitly or references must resolve in M6-B
4. provenance/source identity shape for every context item
5. deterministic ordering/grouping rules
6. distinction between trusted runtime/system instructions and untrusted source/Markdown content
7. secret-sensitive input handling boundary before Harness exists
8. relevance-selection responsibility in v1
9. token/size budget representation and truncation/omission behavior
10. diagnostics/result atomicity for invalid or omitted context input
11. whether allowed tool descriptors belong in this slice before Harness/Tool Gateway exists
12. one real canonical end-to-end fixture/scenario proving context construction
```

Return only:

```text
SUFFICIENT_TO_FREEZE
```

or:

```text
MISSING_ACCEPTED_SEMANTICS
```

with exact evidence-backed decisions needed.

Do not implement product code during Phase 0.

## Preferred minimal boundary

Discovery should prefer the smallest architecture-compatible contract rather than a feature-complete Context Builder.

Likely safe decomposition to evaluate, not pre-accepted semantics:

```text
explicit requirement/constraints
+ explicitly supplied M6-A records
+ explicitly supplied project/source/task-result items
-> normalized ordered context items with provenance
```

Profile auto-selection, implicit repository search, semantic retrieval, reference graph resolution, dynamic tool exposure, and model-specific prompt formatting should remain outside v1 unless current canonical evidence proves they are REQUIRED_NOW.

## Hard blockers

```text
M6A_PRODUCT_BASELINE_NOT_INTEGRATED
PRODUCT_INTENT
PUBLIC_CONTRACT_DECISION
ARCHITECTURE_DECISION
SECRET_BOUNDARY_NOT_ACCEPTED
DEPENDENCY_CHANGE_NOT_ACCEPTED
DESTRUCTIVE_SCOPE
SECRET_OR_PRODUCTION
ADMIN_ESCALATION
MODEL_ROUTING_NOT_EFFECTIVE
REPEATED_REQUIRED_FAILURE
```

## Non-goals

Do not automatically implement:

```text
profile auto-selection
full reference graph resolver
general semantic search / embeddings / vector DB
Harness / Tool Gateway
approval execution
Run Ledger
provider request execution
planning loop changes
CLI run orchestration
Unity integration
Capability/Binding runtime
M6-C+
```

Do not add a retrieval/database/vector dependency without a separately accepted demonstrated need.

## Future implementation acceptance shape

Only after Phase 0 is reviewed and a Context Builder contract is human-accepted should an implementation run freeze:

```text
public API/result shape
input categories
provenance model
ordering/grouping
trusted-vs-untrusted content boundary
size/token policy
omission/truncation diagnostics
allowed product/test files
real fixture/scenario
focused verification
root verification
repair budget
closure artifacts
```

Then use the standard closure sequence:

```text
draft artifacts
-> independent verifier actual verdict
-> persist actual verdict
-> fresh post-verdict audit
-> MILESTONE_DONE
-> STOP for human review
```

No automatic M6-C start.
