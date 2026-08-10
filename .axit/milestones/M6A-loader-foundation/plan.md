# M6-A — Profile / Rule / Workflow / Knowledge Loader Foundation

Status: designed / dependency-gated

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

M6-A does **not** build the Context Builder yet. It only establishes trustworthy loader contracts that a later Context Builder can consume.

## Target

```text
repository: ngocphat03/Axit-Code
canonical product branch: release
runtime owner: @axitcode/agent
canonical product/roadmap truth: docs/PLAN.md
```

Always reacquire current release/PLAN/architecture/ADR truth at execution time.

## Dependency gate — mandatory

At plan design time, Axit-Code PR #5 `docs: establish AxitCode and Game Design rules` is open, draft, and unmerged. Its stated purpose is to establish rule/catalog conventions before Profile/Rule/Workflow/Knowledge loader implementation.

Phase 0 must reacquire PR #5 and current `release` state.

If the loader would need schema/frontmatter/taxonomy conventions that are still only present in an unaccepted draft branch, stop before implementation with:

```text
HARD_BLOCKER: KNOWLEDGE_FOUNDATION_NOT_ACCEPTED
```

Do not automatically merge, rebase, close, publish, or treat draft PR #5 as canonical.

Human resolution options are external to this plan:

```text
merge accepted PR #5
revise then merge PR #5
supersede it with another accepted foundation
explicitly accept a different canonical loader-input contract
```

Once an accepted foundation exists on the chosen implementation baseline, M6-A may continue.

## Productization invariants carried from Game-Studios

Only migrate semantics that match Axit-Code product architecture:

- declarative knowledge is user-owned context, not execution permission;
- canonical product sources remain authoritative over copied summaries;
- loaders must preserve source identity/provenance;
- deterministic machine-checkable behavior is preferred;
- unknown/invalid input fails explicitly rather than being silently guessed;
- Profile/Rule/Workflow/Knowledge semantics remain provider-neutral;
- a loader cannot grant tools, bypass Harness, or declare completion;
- do not create new Profile/Skill/Workflow types just because Game-Studios contains them.

Do **not** copy the Game-Studios `.axit` directory or its Core schemas wholesale.

## Model / cost policy

```text
primary = gpt-5.6-sol / xhigh
child preferred = gpt-5.6-luna / medium when runtime supports Luna
child compatibility fallback = gpt-5.6-terra / medium when Luna is unavailable
child Sol = forbidden without explicit current human override
```

All child roles use the allowed medium tier: discovery, design review, implementation, tests, repair, independent verification, closure verification, and reporting.

No model escalation as a recovery mechanism.

## Orchestration-efficiency policy

M5 succeeded but used 19 child lanes for a small test-only diff. M6-A treats that as an optimization signal, not a hard failure.

Rules:

- spawn a new lane only for materially independent work or required verification independence;
- reuse an existing lane when responsibility and context remain the same;
- do not create a new child merely for every phase/checkpoint/report update;
- keep read-only discovery parallel but bounded by useful work;
- serialize overlapping source writes and result/state writes;
- fresh independent verifier remains required even if it increases lane count;
- record total child lanes and peak useful parallelism at closure.

Do not set an arbitrary pass/fail lane-count threshold from one M5 sample.

## Operating boundaries

- Primary orchestrates only when delegation is available.
- Do not ask for routine confirmations inside accepted scope.
- Do not commit, push, publish, merge, or open/update PRs during the autonomous M6-A run unless a separate explicit human instruction authorizes that exact action.
- Do not change `docs/PLAN.md` merely to fit implementation.
- Do not start Context Builder, Harness, Tool Gateway, Run Ledger, provider integration, CLI orchestration, Unity integration, Capability/Binding resolution, or M6-B.
- Do not introduce a new parser dependency unless current accepted architecture and frozen scope justify it; prefer existing platform/dependencies when sufficient.
- Preserve unrelated current filesystem state.
- Maximum two bounded repair attempts for the same required implementation/verification failure.
- Follow durable closure: actual final verifier verdict must be persisted, then a fresh read-only consistency audit runs before `MILESTONE_DONE`.

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

# Phase 0 — Reacquire canonical baseline and dependency state

Run from a trusted writable Axit-Code checkout on the human-selected implementation baseline.

Read-only lanes independently reacquire:

- current `docs/PLAN.md` Phase 1 status and next accepted direction;
- current `release` package/source/test structure;
- current architecture + ADR-001 ownership boundaries;
- current PR #5 state and diff intent;
- whether accepted `.agents` taxonomy/frontmatter/rule fixtures exist on the implementation baseline;
- current validation commands (`npm run verify` and relevant package tests);
- effective model routing when observable.

No product write before dependency acceptance is established.

If PR #5 remains draft/unmerged and required loader-input conventions are absent from canonical accepted source, return `KNOWLEDGE_FOUNDATION_NOT_ACCEPTED` and STOP. Do not manufacture a schema from the draft.

---

# Phase 1 — Loader contract discovery

After dependency gate passes, use read-only medium-tier lanes to identify the smallest accepted loader surface.

Determine from accepted source, not imagination:

- artifact kinds required in this slice: expected Profile / Rule / Workflow / Knowledge only if current PLAN/foundation confirms them;
- accepted file locations/discovery roots;
- accepted frontmatter/schema fields;
- required identity fields and source identity;
- optional versus required fields;
- ordering/precedence semantics actually defined;
- duplicate/conflict behavior;
- unknown field/version behavior;
- path/scope safety expectations;
- whether files are Markdown, YAML frontmatter, YAML, or another accepted representation.

Create a concise contract proposal. Every field must cite accepted target evidence or be marked unresolved.

Unresolved behavior that affects public/runtime contracts is a blocker, not a license to guess.

---

# Phase 2 — Freeze M6-A acceptance contract

Independent medium-tier reviewer approves the contract before implementation.

Freeze:

```text
loader artifact kinds
input roots/file forms
normalized record shape
source identity/provenance shape
validation/error semantics
duplicate/conflict semantics
deterministic ordering rule
allowed source/test files
forbidden scope
focused tests
repository verification
repair budget
```

The contract must explicitly state what M6-A does **not** implement.

No Context Builder behavior may be smuggled into loader acceptance.

---

# Phase 3 — Implement minimal loader foundation

Use an allowed medium-tier implementation worker.

Implementation principles:

- place authoritative runtime implementation under the current accepted `@axitcode/agent` boundary unless canonical architecture says otherwise;
- expose the smallest API required by frozen acceptance;
- preserve provider neutrality;
- preserve exact source identity needed for later context/audit;
- deterministic output for identical filesystem input;
- explicit structured validation errors;
- no implicit network access;
- no tool/Harness permission semantics;
- no Context Builder/token budgeting/relevance ranking yet;
- no runtime side effect other than bounded file reads required by the loader.

Do not port Game-Studios implementation code unless it independently fits Axit-Code's accepted contracts. Prefer re-deriving a native implementation from semantic requirements.

---

# Phase 4 — Contract and negative-path tests

Required deterministic coverage should include only cases justified by the frozen contract, typically:

- one valid artifact of each accepted kind;
- stable normalized output/source identity;
- deterministic discovery/order;
- missing required field;
- unsupported schema/version when versioning exists;
- duplicate identifier/conflict;
- malformed frontmatter/document;
- unknown/unaccepted artifact behavior;
- precedence behavior only if accepted source defines it;
- proof that loader metadata does not grant tools/permissions.

Do not add speculative test matrices for fields the accepted foundation does not define.

---

# Phase 5 — Target-native verification

Fresh independent medium-tier verifier reacquires evidence.

REQUIRED:

- frozen acceptance assertions pass;
- focused `@axitcode/agent` tests pass;
- build + typecheck for affected workspace pass;
- root `npm run verify` passes when current repository policy still defines it as canonical health check;
- diff remains within frozen loader/test scope plus M6-A execution metadata;
- no Context Builder/Harness/Tool/Run Ledger/M6-B implementation appeared;
- loader output preserves source identity and does not claim authority beyond input source;
- no unauthorized model escalation.

If bounded FAIL, separate medium-tier repair worker may repair within frozen scope, then verifier reacquires affected evidence. Maximum two loops for same required failure.

---

# Phase 6 — REAL fixture/use demonstration

Use at least one accepted real declarative artifact from Axit-Code as a loader fixture/use case; do not rely exclusively on synthetic toy input.

The demonstration proves only loader acquisition/normalization. It must not claim the agent runtime is already building full model context or executing profiles/workflows.

If no accepted real declarative artifact exists after dependency resolution, the loader foundation is premature; stop rather than inventing one.

---

# Phase 7 — Productization gap analysis

Classify observed next needs without implementing them:

```text
NO_CHANGE
LOADER_CONTRACT_REPAIR
CONTEXT_BUILDER_INPUT
HARNESS_INPUT
VERIFICATION_INPUT
RUN_LEDGER_INPUT
FUTURE_PROFILE_RUNTIME_INPUT
```

Only repeated or acceptance-critical loader gaps may modify M6-A. Everything else becomes input to later M6 slices.

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

Use **terminal end-to-end** as the primary milestone latency metric.

Review whether lane count was proportional to task complexity. Optimize decomposition/handoffs before considering a larger child model.

---

# Phase 9 — Closure and stop

Persist report + retrospective + contract/fixture manifest in the chosen M6-A execution metadata location.

Closure sequence is mandatory:

```text
draft closure artifacts
  -> fresh independent closure verifier returns actual verdict
  -> persist that actual verdict into durable artifacts
  -> fresh read-only post-verdict consistency audit
  -> MILESTONE_DONE when permitted
  -> STOP for human review
```

Do not pre-write a predicted PASS and treat it as persisted verifier output.

Terminal schema:

```text
Status: MILESTONE_DONE | FAILED | HARD_BLOCKER
Milestone: M6-A Loader Foundation
Dependency foundation: ACCEPTED | BLOCKED
Frozen loader contract: PASS | FAIL | BLOCKED
Implementation: PASS | FAIL | BLOCKED
Focused verification: PASS | FAIL | BLOCKED
Repository verification: PASS | FAIL | BLOCKED
REAL declarative fixture: PASS | FAIL | BLOCKED
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
