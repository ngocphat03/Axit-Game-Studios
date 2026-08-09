# Continuous Runtime Binding v1

Status: ready-after-manual-unity-mcp-setup

## Purpose

Run the next Axit phase continuously from the repository root with one high-reasoning primary thread acting only as orchestrator and sub-agents performing the actual work.

**Manual prerequisite:** the user configures and starts MCP for Unity before this plan is executed. This plan does not install, configure, start, repair, or upgrade the Unity MCP transport.

After the prerequisite is satisfied, the run should continue across phase boundaries without asking for routine confirmations. Stop only at a declared hard blocker or when the plan reaches `DONE`.

## Operating contract

### Primary thread

The primary thread is coordinator only. It may:

- read the minimum Axit routing/state required to understand the active phase;
- spawn, steer, resume, replace, wait for, and close sub-agents;
- assign non-overlapping lanes;
- synthesize sub-agent results;
- decide which already-approved phase comes next;
- surface only hard blockers to the user.

The primary thread must delegate source exploration, product/source changes, build/test execution, Unity/MCP evidence acquisition, bounded repairs, and independent verification.

It must not delegate or perform Unity MCP installation/configuration because that prerequisite is explicitly user-owned.

### Sub-agent lanes

Use the smallest useful lane for each phase. Typical lanes:

- `exploration` — inspect source/environment/transport readiness without changing MCP setup;
- `worker` — perform one bounded project or Axit change;
- `verifier` — independently inspect current state/evidence and issue verification results;
- `recovery` — take over a stuck in-scope lane from distilled context when steering/resume is insufficient.

Do not let a worker self-certify a change when independent verification is practical.

Read-only lanes may run in parallel. Write-heavy lanes and Unity editor/runtime mutations should be serialized when their state can overlap.

## Recovery policy

For a sub-agent that pauses, blocks, or returns incomplete work **after Unity MCP readiness has passed**:

1. record its useful findings, changed files, command/runtime state, and explicit blocker;
2. send one focused recovery/steering instruction to the same lane when safe;
3. if still stuck, replace the lane with a fresh sub-agent using only distilled context plus current workspace state;
4. allow at most two bounded recovery/replacement attempts for the same required lane/failure;
5. reacquire current evidence after any repair or replacement;
6. continue automatically when the blocker is resolved.

A sub-agent asking a routine clarification that can be answered from accepted Axit/source evidence should be answered by the orchestrator, not escalated to the user.

Missing or broken Unity MCP setup is not a recovery lane. It is a manual prerequisite failure and must stop the run.

## Hard blockers

Return `HARD_BLOCKER` and stop only when at least one is materially required:

- `UNITY_MCP_NOT_READY` — the user-configured Unity MCP transport is missing, unreachable, exposes no usable live operations, or is not connected to the intended QuickGun editor/project;
- `PRODUCT_INTENT` — material desired behavior is ambiguous and cannot be resolved from accepted evidence;
- `ARCHITECTURE_DECISION` — a material public contract, dependency direction, state ownership, or architecture stance must change outside the accepted plan;
- `DESTRUCTIVE_SCOPE` — destructive migration/deletion or broad unrelated refactor is required;
- `SECRET_OR_PRODUCTION` — credentials/secrets, production access, or external-cloud writes are required;
- `ADMIN_ESCALATION` — sudo/admin or machine-wide configuration is required;
- `DIRTY_WORKTREE_RISK` — unrelated user changes would be overwritten or materially endangered;
- `REPEATED_REQUIRED_FAILURE` — the same required failure persists after two bounded repair/replacement loops.

Do not stop simply because one ordinary sub-agent lane failed when a safe replacement route remains.

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
- Do not modify Unity MCP/CoplayDev installation, bridge configuration, or user/global Codex MCP configuration.
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
- current Runtime Binding status.

Acceptance:

- no dirty-worktree collision risk;
- stable Axit layers unchanged;
- multi-agent execution is available.

If multi-agent execution itself is unavailable, stop with the smallest actionable blocker rather than letting the primary thread do the work directly.

Checkpoint and continue.

---

# Phase 1 — Manual Unity MCP readiness gate

Use one read-only transport-readiness sub-agent.

The user is responsible for installing/configuring/starting MCP for Unity and opening the intended QuickGun Unity project/editor before this run.

The sub-agent may inspect only current live state:

- current Codex/MCP tool inventory;
- actual Unity MCP server/adapter identity;
- exact live operation names exposed by the configured transport;
- whether the intended QuickGun project/editor is reachable;
- whether the active Unity instance is the expected project;
- any observed connection/runtime error.

The sub-agent must not:

- install or upgrade CoplayDev/unity-mcp;
- edit `Packages/manifest.json` for MCP setup;
- install `uv`, Python, or other MCP prerequisites;
- edit global/user/project MCP server configuration;
- start/configure/repair the Unity MCP bridge;
- guess the expected endpoint or operation names.

Acceptance:

- one real Unity MCP transport is visible in the live tool inventory;
- the intended QuickGun editor/project is reachable;
- real concrete operations are inspectable from the live interface.

If any acceptance item is missing, stop immediately with:

```text
HARD_BLOCKER: UNITY_MCP_NOT_READY
Observed: <short concrete missing state>
Action: Configure/start MCP for Unity manually, then rerun the continuous plan.
```

Do not attempt to repair the prerequisite.

Checkpoint and continue when accepted.

---

# Phase 2 — Real transport discovery

Use a fresh read-only transport-discovery sub-agent after Phase 1.

Inspect the live MCP tool/operation inventory and current Unity editor/project connection.

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

If one of the three semantic capabilities has no compatible live operation, keep it unbound and stop only if that gap makes the vertical slice impossible. Never reconfigure MCP to manufacture missing support.

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
- every concrete operation name was verified live;
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

- `acquired` — mapped operation observes the intended QuickGun target and returns trustworthy evidence/provenance;
- `unavailable` — an observed runtime state is represented as missing evidence, not product failure;
- `denied` — Runtime/Harness denial remains distinct from target failure;
- `transport_error` — transport failure before trustworthy target observation remains acquisition error.

Do not intentionally break or reconfigure the user's MCP setup merely to manufacture unavailable/error states. Use naturally observed states or safe bounded simulations when sufficient.

A verifier sub-agent checks that none of these acquisition states is itself presented as `PASS`, `FAIL`, or `BLOCKED` without `verify-change` criterion reasoning.

Acceptance:

- binding/runtime evidence path preserves acquisition-state semantics sufficiently for the first vertical slice.

Checkpoint and continue.

---

# Phase 5 — First end-to-end evidence vertical slice

Goal: prove Axit can move from accepted criterion to semantic Capability to real transport evidence to independent verification.

Use QuickGun's current damage pipeline and configured Player prefab.

Target evidence layers:

1. current standalone deterministic C# damage tests;
2. concrete Player prefab inspection;
3. serialized `DamageableBodyPart` references/values relevant to the criterion;
4. bounded Play Mode headshot scenario;
5. observed runtime health result and enough target identity to distinguish intended hit-zone behavior.

Do not run unrelated capabilities merely because they are available.

Lane separation:

- worker lane prepares only bounded project test state if preparation is needed;
- evidence lane uses the reviewed binding to acquire current Unity evidence;
- verifier lane independently follows `verify-change` against accepted criteria and current evidence.

The verifier must classify REQUIRED vs SUPPORTING evidence independently.

Acceptance:

- real binding produces current evidence for mapped capabilities;
- verifier reaches the correct verdict without overclaiming beyond observed evidence;
- runtime evidence is traceable to the intended QuickGun target/session.

If verifier returns `FAIL`, continue to Phase 6 recovery without routine confirmation.

If verifier returns `BLOCKED` because the manually configured MCP transport itself became unavailable, stop with `HARD_BLOCKER: UNITY_MCP_NOT_READY`; do not repair MCP setup.

Checkpoint and continue.

---

# Phase 6 — Bounded failure/recovery validation

Validate that the continuous multi-agent system can recover from one demonstrated required project failure without user micromanagement.

Use one safe bounded failure in the current vertical-slice domain. Prefer a reversible project-local mismatch whose expected behavior is already accepted, such as a serialized value/reference mismatch or similarly narrow defect.

Required flow:

```text
current evidence
  -> independent verifier: FAIL
      -> orchestrator assigns bounded repair worker
          -> repair only demonstrated project defect
              -> reacquire current evidence
                  -> fresh verifier pass
```

Rules:

- verifier must not perform the repair;
- worker must not issue the final verification verdict;
- do not reuse stale pre-repair evidence as proof;
- maximum two repair/replacement loops;
- preserve unrelated working-tree changes;
- do not use the recovery phase to alter MCP installation/configuration.

Acceptance:

- one real `FAIL -> repair -> reacquire -> reverify` path completes correctly;
- or a declared hard blocker is surfaced.

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
