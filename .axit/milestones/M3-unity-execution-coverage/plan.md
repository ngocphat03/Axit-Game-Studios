# M3 — Unity Execution Coverage

Status: ready-for-local-execution

## Goal

Prove that Axit can acquire trustworthy full-Unity compilation evidence for the real QuickGun System and then use the smallest demonstrated Unity execution surface to complete and verify at least one REAL product task.

M3 starts from evidence discovered in M1/M2. It is not a mandate to bind every declared Unity Capability.

## Promotion context

M1 and M2 are human-promoted.

M2 follow-up audit identified three durable inputs to M3:

- full Unity/project compilation was repeatedly missing after production C# changes;
- the QuickGun System is canonically maintained in `ngocphat03/QuickGun-MVP` on ref `release` while the root Workspace may not track that tree;
- path-addressed runtime evidence must resolve current full runtime identity before use and may not infer full scene paths from prefab-relative hierarchy.

The first demonstrated Capability gap is exactly:

```text
unity.compile
```

Do not pre-authorize binding any other currently unbound Unity Capability merely because it exists in the catalog.

## Required context

Read only what is needed:

1. `.axit/workspace.yaml`;
2. `.axit/state/active.md`;
3. `.axit/memory/project-memory.md`;
4. `.axit/roadmap/milestone-closure.md`;
5. `.axit/checklists/runtime-binding-validation.md`;
6. `.axit/systems/unity-client/system.yaml`;
7. `.axit/systems/unity-client/capabilities.yaml`;
8. `.axit/bindings/unity-client/coplaydev-unity-mcp.yaml`;
9. this plan;
10. relevant QuickGun source/requirements only on demand.

Do not recursively preload `.axit/` or the full Unity project.

## Operating contract

- Primary thread is orchestrator only when sub-agents are available.
- Delegate exploration, transport discovery, implementation, compile acquisition, tests, repair, and verification.
- Worker and verifier lanes remain separate.
- Read-heavy discovery may run in parallel; overlapping writes and Unity editor mutations must be serialized.
- Routine phase completion does not require user confirmation.
- Maximum two bounded recovery/replacement attempts for the same required failure/lane.
- Do not push/publish/create PRs.
- Respect `.axit/workspace.yaml -> safety.check_git_status`; with `false`, Git status is not a readiness gate.
- User-owned Unity MCP setup remains manual. Axit may inspect/use an already configured transport but may not install/configure/start/repair it.
- Follow `.axit/roadmap/milestone-closure.md` before returning `MILESTONE_DONE`.

## Hard blockers

Stop only for a material blocker that cannot be resolved within the accepted bounded scope:

```text
UNITY_MCP_NOT_READY
PRIMARY_REASONING_NOT_EFFECTIVE
PRODUCT_INTENT
ARCHITECTURE_DECISION
DESTRUCTIVE_SCOPE
SECRET_OR_PRODUCTION
ADMIN_ESCALATION
REPEATED_REQUIRED_FAILURE
```

`PRIMARY_REASONING_NOT_EFFECTIVE` applies in M3: the project now requests primary `gpt-5.6-sol / xhigh`; a fresh trusted session must verify the effective primary value. Do not carry the M2 exception forward.

## Evidence provenance

Classify every material artifact using:

```text
workspace-canonical-pushed
system-canonical-pushed
local-or-separately-tracked
ephemeral-runtime
```

For `system-canonical-pushed`, record the System id plus repository/ref/commit when available.

The QuickGun canonical repository metadata is declared in `.axit/systems/unity-client/system.yaml`; resolve the current commit during the run rather than assuming the branch head stored in an old report is current.

## Runtime target regression rule

For any path-addressed runtime operation, obey Runtime Binding validation Case 8.

Never synthesize a complete runtime path from prefab-relative hierarchy alone.

Resolve current full runtime identity using current read-only evidence before the first path-addressed operation, and resolve again after reload/refresh if identity may be stale.

---

# Phase 0 — M3 readiness and baseline identity

Delegate a read-only readiness lane.

Required checks:

- M2 promotion review exists and records `HUMAN_PROMOTED`;
- current milestone is M3 and M4 is not authorized;
- project config requests primary `gpt-5.6-sol / xhigh`;
- effective fresh primary session is actually `gpt-5.6-sol / xhigh`;
- sub-agent execution is available and effective sub-agent model/reasoning is recorded without inventing unavailable metadata;
- user-configured Unity MCP reaches the intended QuickGun editor/project;
- editor begins in a safe known state, preferably idle Edit Mode;
- current QuickGun Unity version/project root are reacquired;
- current System repository/ref/commit are recorded if available;
- M1 binding remains active for exactly its previously proven three mappings before M3 expansion;
- `unity.compile` remains declared but unbound at Phase 0 start.

If the primary effective value cannot be verified as `xhigh`, stop with `PRIMARY_REASONING_NOT_EFFECTIVE`; do not silently defer again.

Do not use Git status as a blocker when workspace safety disables it.

---

# Phase 1 — Discover the real compile transport operation

Delegate transport discovery to a read-only/exploration sub-agent.

Inspect the actual current CoplayDev/unityMCP interface and determine whether it exposes a concrete operation capable of producing trustworthy Unity project/script compilation state or diagnostics for the intended QuickGun editor.

Record exact observed operation names, arguments, result envelope, success/failure fields, diagnostic payload, and editor/project identity behavior.

Rules:

- do not infer tool names from documentation, memory, or previous versions;
- do not materialize a `unity.compile` mapping until the real operation contract is observed;
- an operation that merely runs standalone `mcs`, refreshes assets, or reads console text is not automatically `unity.compile`;
- compilation evidence must represent the Unity project's actual script/domain compilation state sufficiently for the accepted criterion.

If no trustworthy compile operation exists, leave `unity.compile` unbound and stop with a concise capability/transport blocker. Do not invent a substitute binding.

---

# Phase 2 — Materialize the minimal `unity.compile` binding

Only after Phase 1 proves a real operation, delegate a bounded binding update.

Expand the reviewed CoplayDev binding only for:

```text
unity.compile
```

Do not bind `unity.tests.run`, `unity.console.inspect`, `unity.scene.inspect`, `unity.component.inspect`, or `unity.project.inspect` in this phase.

Persist only stable transport contract details. Do not persist credentials, local ports, session ids, editor instance hashes, timestamps, or other ephemeral machine state.

Independent verifier must prove:

- `unity.compile` is declared by the active Unity capability set;
- the concrete operation was observed live;
- existing three M1 mappings are unchanged semantically;
- the mapped/unbound partition remains explicit and non-overlapping;
- binding definition does not claim current runtime availability;
- compile evidence acquisition is not itself a PASS/FAIL product verdict;
- permission/Harness boundaries remain unchanged.

Keep binding status in validation/current-review state until later M3 promotion.

---

# Phase 3 — Full QuickGun baseline compile

Acquire a fresh full Unity compilation result for the current QuickGun project through the candidate `unity.compile` mapping.

This is REQUIRED evidence.

Record:

- project/editor identity;
- System repository provenance when available;
- whether compilation completed;
- trustworthy error/warning diagnostics relevant to the compile result;
- acquisition state separately from criterion verdict.

If compile succeeds, independently verify the baseline as compile-clean for the exercised Unity compilation criterion.

If compile fails naturally:

1. classify the acquired compiler diagnostics precisely;
2. do not call the transport unavailable merely because compilation failed;
3. delegate one bounded diagnosis lane;
4. repair only when errors are clearly bounded, project-local, and do not require unresolved product/architecture decisions;
5. rerun full Unity compile after repair;
6. maximum two bounded repair loops for the same required compile failure.

If current project compilation cannot be brought to a trustworthy PASS within bounded scope, stop M3 with the correct FAIL/HARD_BLOCKER result rather than weakening the criterion.

Standalone C# tests may support diagnosis but cannot substitute for this required Unity compile evidence.

---

# Phase 4 — Compile acquisition-state regression

Validate that the new compile binding preserves acquisition/verification semantics.

At minimum demonstrate or safely fixture the distinctions:

```text
acquired + compilation succeeds
acquired + compilation errors exist
unavailable
denied
transport_error
```

Important:

- `acquired + compilation errors exist` is acquired evidence that may prove a compile criterion FAILED;
- `unavailable`, `denied`, and `transport_error` are acquisition states and are not compilation failures by themselves;
- do not manufacture a permanent product defect solely for the milestone.

If a controlled compile-error fixture is required, use a clearly temporary, bounded, reversible project-local fixture, acquire fresh failure evidence, restore/remove the fixture, then reacquire PASS. Mandatory cleanup must be independently verified.

---

# Phase 5 — Select and freeze one REAL QuickGun product scenario

Delegate candidate discovery across current QuickGun source, accepted README requirements, existing ADR/rules, and current implementation gaps.

This scenario must be `REAL`, not a benchmark.

Freeze an explicit contract before implementation with at least:

```text
id
type = REAL
goal
acceptance source
accepted behavior
allowed files/scope
forbidden scope
required evidence
supporting evidence
compile requirement
runtime cleanup/restoration
provenance expectation
hard-blocker triggers
```

Selection rules:

- do not invent missing product values/ranges/UI semantics;
- if a requirement is under-specified, reject that candidate rather than filling gaps creatively;
- prefer a bounded production C# change for which full Unity compile is genuinely meaningful;
- full Unity compile is REQUIRED after any production C# change in the selected scenario;
- reuse ordinary deterministic tests where appropriate;
- use existing M1 Unity capabilities only when the frozen acceptance actually requires asset/runtime evidence;
- do not add another Capability merely to make the scenario look richer.

Independent contract reviewer must approve the frozen scenario before implementation.

If no safe REAL scenario has sufficient accepted intent, stop with `PRODUCT_INTENT` and list the smallest missing decisions. Do not fall back to a benchmark in M3.

---

# Phase 6 — Autonomous implementation

Delegate the frozen REAL scenario to an implementation worker.

Worker rules:

- current filesystem is the baseline; preserve unrelated state;
- make the smallest change that satisfies frozen acceptance;
- do not rewrite acceptance after seeing the implementation;
- do not redesign public contracts, state ownership, architecture, Profiles, Skills, Workflows, or semantic Capability ids;
- run relevant deterministic checks;
- acquire no final verdict;
- hand off current changed-file/evidence summary to an independent verifier.

If the worker stalls, apply normal steer/resume/replacement policy.

---

# Phase 7 — Independent full verification of the REAL scenario

A fresh verifier must reacquire current evidence.

For production C# changes, REQUIRED evidence includes:

```text
frozen criterion evidence
+ relevant deterministic tests/static checks
+ unity.compile full-project evidence
```

Add prefab/serialized/Play Mode evidence only when required by the frozen criterion.

Use `verify-change` semantics:

```text
criterion
  -> REQUIRED / SUPPORTING
  -> PROVEN / FAILED / UNRESOLVED
  -> PASS / FAIL / BLOCKED
```

Do not reuse stale compile/runtime evidence after repair.

If FAIL and repair is bounded, delegate a separate worker repair, then reacquire all affected REQUIRED evidence. Maximum two repair loops for the same required failure.

---

# Phase 8 — Demonstrated-gap analysis for additional Unity coverage

After baseline compile and the REAL scenario, delegate cross-run analysis.

Ask which remaining evidence needs actually occurred:

- test execution through Unity;
- console diagnostics;
- scene inspection;
- component inspection;
- project inspection.

For each candidate, classify:

```text
NO_NEED
ORDINARY_EVIDENCE_SUFFICIENT
REPEATED_GAP_FOR_HUMAN_REVIEW
REQUIRED_NOW
```

Only `REQUIRED_NOW` may be considered for an additional M3 binding, and it must pass the same live transport discovery + minimal mapping + independent validation discipline as `unity.compile`.

Do not bind all remaining capabilities to make the milestone appear complete.

The recurring runtime-wrapper issue by itself does not justify `unity.scene.inspect` if the new target-resolution procedure works with ordinary current read-only evidence.

---

# Phase 9 — Promote proven M3 binding scope

If `unity.compile` passed its mapping/acquisition/baseline/REAL-scenario acceptance, promote that mapping from validation to active alongside the existing M1 mappings.

Any other M3 mapping may be promoted only if independently proven under Phase 8's `REQUIRED_NOW` gate.

Update the System capability sidecar, binding documentation/checklist/current state consistently.

Run a secret/ephemeral-state scan and confirm active binding status still means reviewed definition, not current connectivity.

---

# Phase 10 — Closure, retrospective, and stop

Follow `.axit/roadmap/milestone-closure.md`.

Persist:

```text
.axit/milestones/M3-unity-execution-coverage/report.md
.axit/milestones/M3-unity-execution-coverage/retrospective.md
```

Closure verifier must independently check at least:

- effective primary `gpt-5.6-sol / xhigh` was verified in the fresh M3 run;
- `unity.compile` concrete operation was live-discovered before mapping;
- baseline full QuickGun Unity compile reached trustworthy terminal evidence;
- compile acquisition state was not conflated with product verdict;
- exactly the proven capability scope was promoted;
- the REAL QuickGun scenario contract was frozen before implementation;
- full Unity compile was REQUIRED and reacquired after any production C# repair;
- runtime target resolution followed Case 8 wherever path-addressed evidence was used;
- evidence provenance correctly identifies Workspace/System/local/runtime boundaries;
- active state is compact and points to durable M3 artifacts rather than copying the whole milestone transcript;
- M4 has not started.

Final terminal schema:

```text
Status: MILESTONE_DONE | HARD_BLOCKER | FAILED
Milestone: M3 Unity Execution Coverage
Baseline compile: PASS | FAIL | BLOCKED
Active Unity bindings: <mapped ids>
REAL scenario: <id + verdict>
Repair loops: <count>
Sub-agent replacements: <count>
Additional bindings promoted: <ids or none>
Context/auditability: PASS | issues
Known residual risks: <compact>
Promotion recommendation: PROMOTE | REPAIR_AND_RERUN
```

After writing closure artifacts and receiving closure-verifier result, STOP for human promotion review. Do not start M4 automatically.
