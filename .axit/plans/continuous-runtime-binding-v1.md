# Continuous Runtime Binding v1

Status: ready-for-local-execution

## Purpose

Run the next Axit phase continuously from the repository root with one high-reasoning primary thread acting only as orchestrator and sub-agents performing the actual work.

The run should continue across phase boundaries without asking for routine confirmations. Stop only at a declared hard blocker or when the plan reaches `DONE`.

## Operating contract

### Primary thread

The primary thread is coordinator only. It may:

- read the minimum Axit routing/state required to understand the active phase;
- spawn, steer, resume, replace, wait for, and close sub-agents;
- assign non-overlapping lanes;
- synthesize sub-agent results;
- decide which already-approved phase comes next;
- surface only hard blockers to the user.

The primary thread must delegate product/source changes, environment setup, build/test execution, Unity/MCP operations, evidence acquisition, bounded repairs, and independent verification.

### Sub-agent lanes

Use the smallest useful lane for each phase. Typical lanes:

- `exploration/setup` — inspect environment, transport, package state, operation inventory;
- `worker` — perform one bounded setup or implementation change;
- `verifier` — independently inspect current state/evidence and issue verification results;
- `recovery` — take over a stuck lane from distilled context when steering/resume is insufficient.

Do not let a worker self-certify a change when independent verification is practical.

Read-only lanes may run in parallel. Write-heavy lanes and Unity editor/runtime mutations should be serialized when their state can overlap.

## Recovery policy

For a sub-agent that pauses, blocks, or returns incomplete work:

1. record its useful findings, changed files, command/runtime state, and explicit blocker;
2. send one focused recovery/steering instruction to the same lane when safe;
3. if still stuck, replace the lane with a fresh sub-agent using only distilled context plus current workspace state;
4. allow at most two bounded recovery/replacement attempts for the same required lane/failure;
5. reacquire current evidence after any repair or replacement;
6. continue automatically when the blocker is resolved.

A sub-agent asking a routine clarification that can be answered from accepted Axit/source evidence should be answered by the orchestrator, not escalated to the user.

## Hard blockers

Return `HARD_BLOCKER` and stop only when at least one is materially required:

- `PRODUCT_INTENT` — material desired behavior is ambiguous and cannot be resolved from accepted evidence;
- `ARCHITECTURE_DECISION` — a material public contract, dependency direction, state ownership, or architecture stance must change outside the accepted plan;
- `DESTRUCTIVE_SCOPE` — destructive migration/deletion or broad unrelated refactor is required;
- `SECRET_OR_PRODUCTION` — credentials/secrets, production access, or external-cloud writes are required;
- `ADMIN_ESCALATION` — sudo/admin or machine-wide configuration outside user-local pre-authorization is required;
- `DIRTY_WORKTREE_RISK` — unrelated user changes would be overwritten or materially endangered;
- `REPEATED_REQUIRED_FAILURE` — the same required failure persists after two bounded repair/replacement loops;
- `MANUAL_UNITY_BRIDGE_STEP` — one unavoidable Unity GUI action remains that cannot safely be automated; report exactly the single manual action needed, then stop.

Do not stop simply because one sub-agent or one transport attempt failed when a safe recovery/replacement route remains.

## Global invariants

- Start from repository root.
- Preserve unrelated working-tree changes.
- Do not commit, push, publish, or open a PR.
- Core v1, Workspace/System v1, and Capability semantic v1 are stable; do not redesign them during this plan.
- Do not add Core Skill #3 or Workflow #2.
- Use only declared semantic Capability ids.
- Do not invent transport/server/operation names.
- Runtime Binding definitions may contain verified transport-specific names; semantic Capability definitions may not.
- Keep secrets, ephemeral ports, tokens, and machine-specific credentials out of `.axit`.
- Keep progress/checkpoint messages concise.

---

# Phase 0 — Baseline and execution readiness

Delegate a read-only baseline lane.

Confirm:

- current branch/worktree state and unrelated dirty changes;
- root `AGENTS.md` orchestrator policy is active;
- project `.codex/config.toml` is being used by the trusted repository;
- primary/sub-agent model and reasoning defaults resolve to the intended configuration;
- Core v1 stable;
- Workspace/System v1 stable;
- Capability semantic v1 stable;
- `unity-client` maps to `src/QuickGun-MVP`;
- current Runtime Binding status before setup.

Acceptance:

- no dirty-worktree collision risk;
- stable Axit layers unchanged;
- multi-agent execution is available.

If multi-agent execution itself is unavailable, stop with the smallest actionable blocker rather than letting the primary thread do the work directly.

Checkpoint and continue.

---

# Phase 1 — CoplayDev MCP for Unity setup

Use a dedicated setup sub-agent. This setup is pre-authorized only for the local development environment and this QuickGun workspace. Do not use sudo/admin unless separately approved.

## 1A. Inspect local prerequisites

Inspect actual local state before changing it:

- `src/QuickGun-MVP/ProjectSettings/ProjectVersion.txt` or equivalent project version evidence;
- `src/QuickGun-MVP/Packages/manifest.json` and existing Unity packages;
- whether the QuickGun Unity Editor is already running;
- Python version;
- whether `uv` is available;
- current Codex MCP configuration and live MCP inventory;
- whether localhost `http://localhost:8080/mcp` is reachable.

Do not infer installation state from Axit metadata alone.

## 1B. Install Unity package if missing

If CoplayDev MCP for Unity is not already installed, add the official Unity Package Manager Git dependency while preserving all existing manifest entries:

```text
https://github.com/CoplayDev/unity-mcp.git?path=/MCPForUnity#main
```

Prefer a normal Unity Package Manager/package-manifest installation. Do not copy random package source into the project.

After the package is resolved, verify its actual installed package identity/version from local Unity/package evidence.

## 1C. Install user-local prerequisite if missing

If Python or `uv` required by the installed MCP package is missing:

- prefer user-local installation documented by the actual package;
- do not use sudo/admin;
- verify the resulting executable/version;
- if the only viable route requires admin/machine-wide changes, stop with `HARD_BLOCKER: ADMIN_ESCALATION`.

## 1D. Open/connect Unity

Ensure QuickGun is opened in its recorded Unity version when practical.

Start/enable the MCP for Unity bridge/server using the installed package's real documented integration. The project Codex config already declares a Streamable HTTP client endpoint at:

```text
http://localhost:8080/mcp
```

Do not assume that declaring the endpoint starts the Unity-side bridge.

If the installed package requires one unavoidable Unity UI action that cannot be automated safely, stop only with:

```text
HARD_BLOCKER: MANUAL_UNITY_BRIDGE_STEP
Action: <one exact action>
```

Do not request a broad manual setup checklist.

Acceptance:

- package/prerequisites are locally installed or already present;
- Unity Editor can open/reach QuickGun;
- Unity-side MCP bridge is running or the one unavoidable manual action is precisely identified;
- Codex can see a real Unity MCP server/tool inventory.

Checkpoint and continue when accepted.

---

# Phase 2 — Real transport discovery

Use a fresh read-only transport-discovery sub-agent after Phase 1.

Inspect the live MCP tool/operation inventory and the current Unity editor/project connection.

Determine exact concrete operation names and input/output behavior that can implement only the initial semantic slice:

- `unity.prefab.inspect`;
- `unity.serialized-fields.inspect`;
- `unity.playmode.verify`.

Rules:

- operation names must come from the live verified interface;
- do not infer an operation from documentation if the installed version exposes something different;
- record whether one semantic capability needs several ordered MCP operations;
- preserve each Capability's `can_establish`, `cannot_establish`, operation class, and side-effect boundary;
- confirm the transport is pointed at the QuickGun editor/project, not another Unity instance.

Acceptance:

- real adapter/server identity known;
- exact operation names known;
- current QuickGun editor reachability demonstrated;
- enough information exists to create a narrow reviewed binding.

If no valid operation exists for one of the three capabilities, keep that capability unbound and report the specific binding gap. Do not invent an operation.

Checkpoint and continue if the vertical slice remains possible.

---

# Phase 3 — Materialize Runtime Binding v1

Use one worker sub-agent to create exactly one reviewed binding under:

```text
.axit/bindings/unity-client/<verified-binding-id>.yaml
```

Create it only from Phase 2 verified operations.

Initial mappings may include only:

```text
unity.prefab.inspect
unity.serialized-fields.inspect
unity.playmode.verify
```

Do not bind `unity.compile`, `unity.tests.run`, `unity.console.inspect`, `unity.scene.inspect`, `unity.component.inspect`, or `unity.project.inspect` merely because the transport exposes similar tools.

Set binding definition status to `validation`.

Update `.axit/systems/unity-client/capabilities.yaml` only as needed to reference the reviewed binding; do not encode runtime online/offline state as a permanent semantic fact.

Have a separate verifier sub-agent inspect:

- every mapped Capability id exists in the stable active set;
- every concrete operation name was verified;
- no mapping exceeds Capability evidence/side-effect boundaries;
- no secrets/ephemeral credentials were committed;
- unbound capabilities remain explicitly unbound.

Acceptance:

- verifier accepts the binding definition for validation.

Repair bounded binding-definition defects and reverify; maximum two loops.

Checkpoint and continue.

---

# Phase 4 — Runtime Binding acquisition regression

Use sub-agents to exercise the Runtime Binding v1 acquisition-state boundary.

Demonstrate where practical:

## `acquired`

A mapped operation successfully observes the intended QuickGun target and returns trustworthy evidence/provenance.

## `unavailable`

A controlled test or observed state demonstrates that required editor/bridge/dependency absence is represented as unavailable evidence, not product failure.

Do not destabilize the user's normal environment merely to manufacture this state if a trustworthy prior/current observation already proves the behavior.

## `denied`

Confirm Runtime/Harness policy denial remains distinct from target failure. Do not weaken policy just to force a denial case.

## `transport_error`

Confirm a transport failure before trustworthy target observation remains an acquisition error, not a product failure.

Do not intentionally corrupt global/user configuration to create this case.

A verifier sub-agent checks that none of these acquisition states is itself presented as `PASS`, `FAIL`, or `BLOCKED` without `verify-change` criterion reasoning.

Acceptance:

- the binding/runtime evidence path preserves acquisition-state semantics sufficiently for the first vertical slice.

Checkpoint and continue.

---

# Phase 5 — First end-to-end evidence vertical slice

Goal: prove Axit can move from accepted criterion to semantic Capability to real transport evidence to independent verification.

Use QuickGun's current damage pipeline and the configured Player prefab.

## Evidence composition

Use ordinary project validation evidence for deterministic damage arithmetic where appropriate, plus real Unity evidence for the serialized/runtime claims.

Target evidence layers:

1. current standalone deterministic C# damage tests;
2. concrete Player prefab inspection;
3. serialized `DamageableBodyPart` references/values relevant to the criterion;
4. bounded Play Mode headshot scenario;
5. observed runtime health result and enough target identity to distinguish the intended hit-zone behavior.

Do not run unrelated capabilities merely because they are available.

## Lane separation

- worker/setup lane prepares only the bounded test state if preparation is needed;
- evidence lane uses the reviewed binding to acquire current Unity evidence;
- verifier lane independently runs/follows `verify-change` against the accepted criterion and current evidence.

The verifier must classify REQUIRED vs SUPPORTING evidence independently.

Acceptance:

- the real binding produces current evidence for the mapped capabilities;
- verifier reaches the correct verdict without overclaiming beyond observed evidence;
- runtime evidence is traceable to the intended QuickGun target/session.

If verifier returns `FAIL`, continue to Phase 6 recovery rather than asking for routine confirmation.

If verifier returns `BLOCKED` due a recoverable local transport/editor state, apply sub-agent recovery policy first.

Checkpoint and continue.

---

# Phase 6 — Bounded failure/recovery validation

Validate that the continuous multi-agent system can recover from one demonstrated required failure without user micromanagement.

Use one safe bounded failure in the current vertical-slice domain. Prefer a reversible project-local mismatch whose expected behavior is already accepted, such as a serialized value/reference mismatch or similarly narrow defect.

Do not create an artificial failure that requires destructive migration, architecture change, or unrelated code churn.

Required flow:

```text
current evidence
  -> independent verifier: FAIL
      -> orchestrator assigns bounded repair worker
          -> repair only demonstrated defect
              -> reacquire current evidence
                  -> fresh verifier pass
```

Rules:

- verifier must not perform the repair;
- worker must not issue the final verification verdict;
- do not reuse stale pre-repair evidence as proof;
- maximum two repair/replacement loops;
- preserve unrelated working-tree changes.

Acceptance:

- one real `FAIL -> repair -> reacquire -> reverify` path completes correctly;
- or a hard blocker is surfaced under the declared stop rules.

Checkpoint and continue.

---

# Phase 7 — Promote reviewed binding

Only after Phases 3–6 provide sufficient live evidence:

- update the reviewed Runtime Binding definition from `validation` to `active`;
- record concise live validation evidence in the appropriate checklist/state;
- keep current runtime connectivity separate from source-controlled binding status;
- keep all non-proven Unity capabilities unbound.

Use a verifier sub-agent to confirm promotion criteria were actually met.

Acceptance:

- first Unity Runtime Binding is active only for the proven semantic slice.

Checkpoint and continue.

---

# Phase 8 — Demonstrated-gap analysis

Use one read-only analysis sub-agent after successful binding promotion.

Review the completed flow and identify only gaps that actually repeated or materially limited execution.

Possible outcomes:

- recommend binding one additional existing Capability such as `unity.compile` or `unity.console.inspect` because the completed flow demonstrated a concrete need;
- recommend a System Rule/Knowledge update because the gap was project-specific context;
- recommend no extension because the current slice was sufficient.

Do not automatically create:

- Core Skill #3;
- Workflow #2;
- a Unity specialist Profile;
- more semantic Capability ids;
- additional transport mappings.

End with exactly one smallest recommended next step or `none`.

---

# DONE output

When the plan completes, return a compact summary only:

```text
Status: DONE | HARD_BLOCKER
Phases completed: ...
Transport: ...
Binding: ...
Vertical-slice verdict: ...
Recovery iterations: ...
Remaining blockers: ...
Next recommended step: ...
```

Do not produce a long retrospective unless the user asks for it.
