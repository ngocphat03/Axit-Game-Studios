# Axit Workspace Active State

Updated: 2026-08-09
Status: ready-after-manual-unity-mcp-setup

## Current task

Run the Runtime Binding v1 continuous multi-agent plan after the user has manually configured MCP for Unity.

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
- Current repository binding status: **unbound**.
- First intended mapping slice remains:
  - `unity.prefab.inspect`
  - `unity.serialized-fields.inspect`
  - `unity.playmode.verify`

## Workspace safety configuration

Current workspace config:

```yaml
safety:
  check_git_status: false
```

Semantics:

- Git-status-based worktree safety checking is opt-in and defaults to disabled.
- With `false`, continuous Axit runs must not inspect root/nested/submodule `git status` as a readiness gate and must not raise `DIRTY_WORKTREE_RISK` from modified/deleted/untracked/nested/submodule counts.
- Current filesystem/source content is treated as the working baseline for bounded tasks.
- This does not authorize reset, checkout, clean, destructive deletion, reverting user work, or blind overwrites.
- Another workspace may set `safety.check_git_status: true` when strict worktree status validation is desired.

This policy intentionally allows Axit workspaces whose Systems are untracked directories, nested repositories, or later represented as submodules without making Git topology a default execution blocker.

## Codex orchestration configuration

Project runtime configuration:

```text
.codex/config.toml
```

Intended defaults:

- primary model: `gpt-5.6-sol`;
- primary execution reasoning: `max`;
- Plan Mode reasoning: `xhigh`;
- sub-agent default model: `gpt-5.6-sol`;
- sub-agent default reasoning: `max`;
- multi-agent enabled with a four-sub-agent concurrency ceiling;
- `on-request` approvals reviewed by the auto-reviewer under the bounded local-development policy;
- workspace-write sandbox with network access.

No Unity MCP server/endpoint is configured in project `.codex/config.toml`. MCP configuration is intentionally left to the user's manual local setup.

Project-scoped Codex configuration applies only when the repository is trusted by Codex. Runtime/session overrides may still affect effective behavior and must be checked by the baseline sub-agent.

Custom project verifier:

```text
.codex/agents/axit-verifier.toml
```

The verifier is read-only and must not repair its own findings.

## Orchestrator policy

Root `AGENTS.md` requires the primary thread to coordinate rather than directly perform delegatable work.

Actual source exploration/edits/tests/Unity evidence/repairs/verification are delegated to sub-agents.

If an ordinary sub-agent lane pauses or fails inside accepted scope, the orchestrator may steer/resume/replace it for at most two bounded recovery attempts.

Routine phase boundaries do not require user confirmation.

## Manual Unity MCP prerequisite

MCP setup is **not** part of the continuous execution envelope.

Before running the continuous plan, the user manually:

- installs/configures the chosen MCP for Unity transport;
- starts/enables the Unity-side bridge/server as required by that transport;
- configures Codex MCP access locally as required;
- opens the intended QuickGun project/editor;
- confirms the transport is expected to be available to the new Codex session.

Axit agents may inspect and use that already-configured transport, but they may not install, configure, upgrade, start, or repair it.

If readiness inspection fails, the run stops with:

```text
HARD_BLOCKER: UNITY_MCP_NOT_READY
```

The agent reports the observed missing prerequisite and does not attempt setup.

## Next local run

After the user finishes manual MCP configuration, start a fresh trusted Codex session at repository root and execute the continuous plan.

Expected phases:

1. baseline/multi-agent readiness with Git status skipped because `safety.check_git_status: false`;
2. manual Unity MCP readiness gate;
3. live transport/tool discovery;
4. narrow Runtime Binding materialization;
5. acquisition-state regression;
6. first real Unity evidence vertical slice;
7. bounded FAIL -> repair -> reacquire -> reverify recovery;
8. binding promotion when proven;
9. demonstrated-gap analysis.

## Boundaries kept unchanged

- no agent-driven MCP installation/configuration;
- no Core Skill #3;
- no Workflow #2;
- no broad Unity specialist catalog;
- no invented Capability or transport operation ids;
- no automatic commit/push/PR;
- no changes to legacy `.claude/**`.
