# M6-A — Profile / Rule / Workflow / Knowledge Loader Foundation

Status: designed / ready-for-phase0-canonical-discovery

## Goal

Productize the first proven Knowledge Plane boundary inside Axit-Code by implementing the smallest deterministic loader foundation for accepted declarative agent artifacts.

M6-A proves:

```text
accepted declarative source files
  -> parse/validate
  -> normalized typed records
  -> stable source identity/provenance
  -> deterministic discovery/order
  -> explicit conflict/error behavior
  -> target-native tests
```

M6-A does **not** build Context Builder yet. Context Builder is a later M6 slice.

## Target and authority

```text
repository: ngocphat03/Axit-Code
canonical product branch: release
runtime owner: @axitcode/agent
canonical product/roadmap truth: docs/PLAN.md
```

At execution start, reacquire current `release`, `docs/PLAN.md`, architecture, ADRs, package/source/tests, and any accepted declarative artifacts.

Current canonical PLAN explicitly places profile loading/context loading with schema/diagnostics/tests in Phase 1 Slice 2. M6-A takes the loader foundation portion only.

## Historical PR handling

PR #5 `docs: establish AxitCode and Game Design rules` is stale draft prior art, not a mandatory dependency. At 2026-08-11 review it was last updated 2026-08-07, still draft/unmerged, without review comments, and its branch had diverged from current `release`.

Therefore:

- do not auto-merge/rebase/close PR #5;
- do not treat its taxonomy/frontmatter as canonical merely because it exists;
- Phase 0 may inspect it as optional historical design input;
- readiness and contract freeze must be decided from current canonical accepted sources.

Do **not** block merely because PR #5 is unmerged.

If current canonical sources do not contain enough accepted semantics to freeze a loader contract without inventing material behavior, stop for the specific missing decision with evidence, using the appropriate blocker such as:

```text
KNOWLEDGE_FOUNDATION_NOT_ACCEPTED
PRODUCT_INTENT
PUBLIC_CONTRACT_DECISION
ARCHITECTURE_DECISION
```

## Productization invariants carried from Game-Studios

Only migrate semantics that match Axit-Code product architecture:

- declarative knowledge is user-owned context, not execution permission;
- canonical product sources remain authoritative over copied summaries;
- loaders preserve source identity/provenance;
- deterministic machine-checkable behavior is preferred;
- unknown/invalid input fails explicitly rather than being silently guessed;
- Profile/Rule/Workflow/Knowledge semantics remain provider-neutral;
- a loader cannot grant tools, bypass Harness, or declare completion;
- do not create new artifact types merely because Game-Studios contains them.

Do not copy the Game-Studios `.axit` directory or schemas wholesale.

## Model / cost policy

```text
primary = gpt-5.6-sol / xhigh
child preferred = gpt-5.6-luna / medium when runtime supports Luna
child compatibility fallback = gpt-5.6-terra / medium when Luna is unavailable
child Sol = forbidden without explicit current human override
```

All child roles use the allowed medium tier: discovery, contract review, implementation, tests, repair, independent verification, closure verification, and reporting.

## Orchestration-efficiency policy

M5 succeeded but used 19 child lanes for a small test-only diff. Treat that as an optimization signal.

- spawn lanes only for materially independent work or required verification independence;
- reuse same-responsibility lanes when safe;
- do not create a new child for every phase/checkpoint/report update;
- parallelize independent read-only discovery;
- serialize overlapping source/result writes;
- fresh independent verifier remains required;
- record total child lanes and peak useful parallelism.

## Operating boundaries

- Primary orchestrates only when delegation is available.
- Do not ask for routine confirmations inside accepted scope.
- Do not commit, push, publish, merge, or open/update PRs during the autonomous run unless separately authorized by the user.
- Do not change `docs/PLAN.md` merely to fit implementation.
- Do not start Context Builder, Harness, Tool Gateway, Run Ledger, provider integration, CLI orchestration, Unity integration, Capability/Binding runtime, or M6-B.
- Do not introduce a new parser dependency unless frozen acceptance justifies it.
- Preserve unrelated filesystem state.
- Maximum two bounded repair attempts for the same required failure.
- Closure must persist the actual verifier verdict after it returns, then consistency-audit before `MILESTONE_DONE`.

## Hard blockers

```text
KNOWLEDGE_FOUNDATION_NOT_ACCEPTED
PRODUCT_INTENT
ARCHITECTURE_DECISION
PUBLIC_CONTRACT_DECISION
DEPENDENCY_CHANGE_NOT_ACCEPTED
DESTRUCTIVE_SCOPE
SECRET_OR_PRODUCTION
ADMIN_ESCALATION
MODEL_ROUTING_NOT_EFFECTIVE
REPEATED_REQUIRED_FAILURE
```

---

# Phase 0 — Reacquire canonical baseline

Run from a trusted writable Axit-Code checkout on the human-selected implementation baseline.

Read-only lanes reacquire:

- current `docs/PLAN.md` Phase 1 status and accepted direction;
- current `release` package/source/test structure;
- architecture + ADR ownership boundaries;
- current declarative artifact conventions already accepted on the baseline, if any;
- current validation commands;
- effective model routing when observable;
- optional historical inputs such as PR #5, clearly marked non-canonical.

No product write before canonical sufficiency is assessed.

Output a **canonical sufficiency decision**:

```text
SUFFICIENT_TO_FREEZE
or
MISSING_ACCEPTED_SEMANTICS: <exact unresolved fields/behavior>
```

If missing semantics affect public/runtime behavior, stop with the appropriate blocker rather than guessing.

---

# Phase 1 — Loader contract discovery

Using canonical accepted source only, determine the smallest loader surface:

- artifact kinds required in this slice;
- accepted file locations/discovery roots;
- accepted fields and file form;
- required identity/source provenance;
- optional vs required fields;
- ordering/precedence only when defined;
- duplicate/conflict behavior;
- unknown field/version behavior;
- path/scope safety expectations.

Every material field must cite accepted target evidence or be marked unresolved.

---

# Phase 2 — Freeze M6-A acceptance contract

Independent medium-tier reviewer approves before implementation.

Freeze:

```text
artifact kinds
input roots/file forms
normalized record shape
source identity/provenance
validation/error semantics
duplicate/conflict semantics
deterministic ordering
allowed source/test files
forbidden scope
focused tests
repository verification
repair budget
```

Explicitly state that Context Builder is not part of M6-A.

---

# Phase 3 — Implement minimal loader foundation

Use an allowed medium-tier worker.

- implementation belongs under current accepted `@axitcode/agent` boundary unless canonical architecture says otherwise;
- smallest API satisfying frozen acceptance;
- provider-neutral;
- deterministic for identical filesystem input;
- explicit structured validation errors;
- source identity preserved;
- no implicit network access;
- no tool/Harness permission semantics;
- no Context Builder/token budgeting/relevance ranking;
- only bounded file reads required by loader.

Re-derive an Axit-Code-native implementation from accepted semantics; do not port Game-Studios implementation wholesale.

---

# Phase 4 — Contract and negative-path tests

Cover only frozen behavior, normally including:

- valid accepted artifact(s);
- stable normalized output/source identity;
- deterministic discovery/order;
- missing required field;
- unsupported schema/version when versioning is accepted;
- duplicate identifier/conflict;
- malformed document/frontmatter when applicable;
- unknown/unaccepted artifact behavior;
- precedence only if canonical source defines it;
- proof loader metadata does not grant tools/permissions.

---

# Phase 5 — Target-native verification

Fresh independent medium-tier verifier reacquires evidence.

REQUIRED:

- frozen acceptance assertions pass;
- focused `@axitcode/agent` tests pass;
- affected build/typecheck pass;
- root `npm run verify` passes when still canonical repository health check;
- diff stays within frozen loader/test scope plus execution metadata;
- no M6-B/Harness/Tool/Run Ledger work appears;
- source identity is preserved;
- no unauthorized model escalation.

Bounded FAIL may use separate medium-tier repair worker; maximum two loops for same required failure.

---

# Phase 6 — REAL declarative use demonstration

Use at least one **accepted canonical declarative artifact** from Axit-Code as a real loader input if one exists after canonical discovery.

If the canonical product direction authorizes loader work but no accepted real artifact exists, do not silently promote a stale draft artifact to canonical. Report the exact missing fixture/foundation decision and block if required by frozen acceptance.

The demonstration proves loader acquisition/normalization only, not Context Builder or execution behavior.

---

# Phase 7 — Productization gap analysis

Classify next needs without implementing them:

```text
NO_CHANGE
LOADER_CONTRACT_REPAIR
CONTEXT_BUILDER_INPUT
HARNESS_INPUT
VERIFICATION_INPUT
RUN_LEDGER_INPUT
FUTURE_PROFILE_RUNTIME_INPUT
```

No automatic M6-B start.

---

# Phase 8 — Cost/performance review

Record when observable:

```text
terminal end-to-end wall-clock
execution-to-preclosure wall-clock
primary model/reasoning
child route/model/reasoning
child lanes spawned
peak useful parallel lanes
replacements
repair loops
human model overrides
```

Use terminal end-to-end as primary latency metric.

---

# Phase 9 — Closure and stop

Persist report + retrospective + contract/fixture manifest.

Mandatory sequence:

```text
draft closure artifacts
  -> fresh independent closure verifier returns actual verdict
  -> persist actual verdict
  -> fresh read-only post-verdict consistency audit
  -> MILESTONE_DONE when permitted
  -> STOP for human review
```

Do not pre-write predicted PASS as final verifier evidence.

Terminal schema:

```text
Status: MILESTONE_DONE | FAILED | HARD_BLOCKER
Milestone: M6-A Loader Foundation
Canonical sufficiency: SUFFICIENT | BLOCKED
Frozen loader contract: PASS | FAIL | BLOCKED
Implementation: PASS | FAIL | BLOCKED
Focused verification: PASS | FAIL | BLOCKED
Repository verification: PASS | FAIL | BLOCKED
REAL declarative fixture: PASS | FAIL | BLOCKED | NOT_REQUIRED
Child route: PREFERRED_LUNA | COMPAT_TERRA | CONTROL_FINDING
Terminal wall-clock: <duration or unavailable>
Child lanes: <count>
Peak useful parallel lanes: <count>
Replacements: <count>
Repair loops: <count>
Human model overrides: <count>
Closure verifier: PASS | FAIL | BLOCKED
Promotion recommendation: PROMOTE | REPAIR_AND_RERUN
```

Do not start M6-B automatically.
