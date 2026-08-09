# Axit Workspace Active State

Updated: 2026-08-09
Status: continuous-subagent-runtime-binding-ready

## Current task

Run the Runtime Binding v1 phase continuously from repository root using a GPT-5.6 Sol high-reasoning primary thread as **orchestrator only**, with sub-agents performing setup, implementation, evidence acquisition, repair, and independent verification.

Canonical continuous plan:

```text
.axit/plans/continuous-runtime-binding-v1.md
```

## Stable foundations

### Core v1

- Profiles: `game-designer`, `technical-architect`, `implementation-engineer`, `quality-verifier`.
- Skills: `implement-change`, `verify-change`.
- Workflow: `bounded-change`.
- Status: stable/frozen unless repeated live evidence demonstrates a reusable gap.

### Workspace/System v1

- Root-first routing is stable.
- `src/QuickGun-MVP` resolves to System `unity-client`.
- Missing cross-System contracts remain explicit unknowns rather than inferred behavior.

### Capability semantic v1

- Capability semantic v1 is stable after live Cases 1-5 passed.
- Stable Unity set: `.axit/capabilities/unity/evidence.yaml`.
- Unity System routing: `.axit/systems/unity-client/capabilities.yaml`.
- Verification semantics remain owned by `verify-change`.

### Runtime Binding v1

- Spec: `.axit/specs/runtime-binding-spec-v1.md`.
- Validation checklist: `.axit/checklists/runtime-binding-validation.md`.
- Current repository binding status before local setup: **unbound**.
- First intended mapping slice remains:
  - `unity.prefab.inspect`
  - `unity.serialized-fields.inspect`
  - `unity.playmode.verify`

## Codex orchestration configuration

Project runtime configuration now exists at:

```text
.codex/config.toml
```

Intended project defaults:

- primary model: `gpt-5.6-sol`;
- primary execution reasoning: `max`;
- Plan Mode reasoning: `xhigh`;
- sub-agent default model: `gpt-5.6-sol`;
- sub-agent default reasoning: `max`;
- multi-agent enabled with a four-sub-agent concurrency ceiling;
- `on-request` approvals reviewed by the auto-reviewer under the bounded local-development policy;
- workspace-write sandbox with network access;
- project MCP client entry `unityMCP` at `http://localhost:8080/mcp`, non-required at startup.

Project-scoped Codex configuration applies only when the repository is trusted by Codex. Runtime/session overrides may still affect effective behavior and must be checked by the baseline sub-agent.

Custom project verifier:

```text
.codex/agents/axit-verifier.toml
```

The verifier is read-only and must not repair its own findings.

## Orchestrator policy

Root `AGENTS.md` now requires the primary thread to coordinate rather than directly perform delegatable work.

Actual setup/exploration/source edits/tests/Unity evidence/repairs/verification are delegated to sub-agents.

If a sub-agent pauses or fails inside the accepted scope, the orchestrator should:

1. capture useful state;
2. steer/resume when safe;
3. replace with a fresh distilled-context sub-agent when needed;
4. allow at most two bounded recovery/replacement attempts for the same lane/failure;
5. stop only at the declared hard blockers.

Routine phase boundaries do not require user confirmation.

## CoplayDev MCP for Unity authorization

The user explicitly approved local setup of CoplayDev `unity-mcp` for this workspace.

The continuous setup lane may, without another routine confirmation:

- inspect the real local Unity project/package state;
- add the approved CoplayDev Unity Package Manager Git dependency if missing;
- install documented user-local prerequisites such as `uv` when needed and possible without sudo/admin;
- open/reach the QuickGun Unity project/editor;
- start/configure the Unity-side bridge using the installed package's real interface;
- connect Codex to the configured localhost MCP endpoint;
- inspect the real tool/operation inventory.

This authorization does **not** include sudo/admin escalation, secrets, production/cloud writes, broad destructive changes, or overwriting unrelated user work.

The repository-side MCP entry is configuration only. It does not prove the Unity package, bridge, or editor is currently installed/running on the user's machine.

## First continuous run

The next local run should start from a fresh trusted Codex session at repository root and execute:

```text
.axit/plans/continuous-runtime-binding-v1.md
```

The run should continue until `DONE` or one declared `HARD_BLOCKER`, not pause for routine phase confirmations.

## Expected phases

1. baseline/multi-agent readiness;
2. CoplayDev MCP for Unity local setup;
3. live transport/tool discovery;
4. narrow Runtime Binding materialization;
5. acquisition-state regression;
6. first real Unity evidence vertical slice;
7. bounded FAIL -> repair -> reacquire -> reverify recovery;
8. binding promotion when proven;
9. demonstrated-gap analysis.

## Boundaries kept unchanged

- no Core Skill #3;
- no Workflow #2;
- no broad Unity specialist catalog;
- no invented Capability or transport operation ids;
- no automatic commit/push/PR;
- no changes to legacy `.claude/**`.
