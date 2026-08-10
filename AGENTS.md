# Axit Workspace

This repository is a Codex-first product workspace. Codex is expected to run from the repository root.

## Orchestrator-only execution

When the current primary model supports sub-agents, the primary thread acts as the **orchestrator only**.

The primary thread may read only the minimum Axit routing/state needed to decompose work, assign lanes, monitor progress, resolve handoffs, and synthesize results. It must not directly perform implementation or evidence-producing work that can be delegated.

Delegate actual work to sub-agents, including:

- repository/source exploration beyond minimal orchestration context;
- environment and transport readiness inspection;
- product/source edits;
- test/build execution;
- Unity/MCP evidence acquisition through an already configured transport;
- bounded repair work;
- independent verification.

For a bounded change, keep implementation and verification in separate sub-agent lanes when practical. The orchestrator must not substitute its own judgment for an independent verifier result.

Parallelize read-heavy independent exploration when useful. Serialize write-heavy lanes that could touch overlapping files or runtime/editor state.

### Manual Unity MCP prerequisite

MCP for Unity setup is user-owned and must be completed before any accepted milestone that requires Unity MCP evidence begins.

The orchestrator and sub-agents may inspect and use the currently available Unity MCP transport, but they must not:

- install or upgrade CoplayDev/unity-mcp;
- add/remove Unity packages solely to set up MCP;
- edit global/user Codex MCP configuration;
- start/configure/repair the Unity MCP bridge on the user's behalf;
- invent a missing transport or operation name.

If an accepted milestone requires Unity MCP and the expected transport is missing, unreachable, or not connected to the intended QuickGun editor/project at readiness, stop with `HARD_BLOCKER: UNITY_MCP_NOT_READY` and report only the observed missing prerequisite. Do not attempt setup.

### Sub-agent recovery

If a sub-agent pauses, blocks, times out, or returns an incomplete result, the orchestrator should recover without asking the user when the issue remains inside the accepted execution envelope:

1. capture the sub-agent's last useful result, blocker, changed files, and current workspace/runtime state;
2. steer or resume the same sub-agent with a focused recovery instruction when recovery is safe;
3. if the lane remains stuck, close/replace it with a fresh sub-agent using distilled context rather than replaying the full conversation;
4. allow at most two bounded recovery/replacement attempts for the same lane or required failure;
5. after recovery, reacquire current evidence rather than reusing stale results;
6. checkpoint meaningful current progress in `.axit/state/active.md` or the current milestone artifact through a delegated write lane and continue automatically.

Do not stop merely because one sub-agent is blocked when another safe route or replacement lane can complete the accepted task.

Do not apply recovery policy to missing/unready user-owned Unity MCP setup: that is a manual prerequisite, not an agent-repair lane.

### Hard-stop conditions

Stop the continuous run and ask the user only when at least one of these becomes materially necessary:

- `UNITY_MCP_NOT_READY` — a required manually configured Unity MCP transport is missing, unreachable, lacks the required live operations, or is not connected to the intended editor/project;
- product intent or acceptance behavior is ambiguous and cannot be resolved from accepted project evidence;
- a material architecture, public-contract, dependency-direction, or state-ownership decision is required outside the accepted plan;
- destructive deletion/migration or broad unrelated refactoring is required;
- credentials, secrets, production access, or external-cloud writes are required;
- sudo/admin escalation or machine-wide configuration is required;
- continuing would materially overwrite/endanger unrelated current source state and the accepted plan cannot preserve it;
- the same required failure remains after two bounded repair/replacement loops;
- any milestone-specific hard blocker declared by the current accepted plan.

Git modified/deleted/untracked/nested/submodule state is not itself a blocker when `.axit/workspace.yaml -> safety.check_git_status` is `false`; `AGENTS.override.md` defines the status-gating override.

Routine phase completion is not a reason to ask for confirmation.

### Continuous roadmap routing

When the user asks to continue the Axit roadmap:

1. read `.axit/state/active.md`;
2. resolve the **current milestone** and its explicit execution-plan pointer from that state;
3. execute that current accepted milestone continuously until `MILESTONE_DONE`, `FAILED`, or a declared `HARD_BLOCKER`;
4. follow `.axit/roadmap/milestone-closure.md`;
5. stop for human promotion review and do not start the next milestone automatically.

Do **not** hardcode or fall back to an older plan such as `.axit/plans/continuous-runtime-binding-v1.md` unless the current active state explicitly points to it.

Keep user-facing progress concise. Do not emit long phase-by-phase essays unless asked.

## Root routing

- `.axit/` is the Axit source of truth; `.agents/skills/` is Codex Skill discovery only.
- Read `.axit/workspace.yaml` when a task touches product source under `src/` or spans multiple components.
- Map affected source paths to registered Systems, then read only the relevant `.axit/systems/<system-id>/` context.
- Read `.axit/registry/architecture.yaml` and `.axit/registry/integrations.yaml` when a change crosses System boundaries, ownership, or public contracts.
- Do not recursively preload `.axit/`.

## Core behavior

- Shared Core lives under `.axit/core/` and Core v1 is frozen unless real repeated use demonstrates a reusable gap.
- When the user requests end-to-end implementation plus independent verification for one bounded change, follow `.axit/core/workflows/bounded-change/WORKFLOW.md`.
- `implement-change` and `verify-change` remain independently usable when only one job is requested.
- If product intent or a material architecture decision is unresolved, hand off to the owning Core Profile instead of guessing.

## Capability routing

- Capabilities are semantic evidence/execution operations under `.axit/capabilities/`; they are not Skills, Workflows, or verdict rules.
- Capability semantic v1 is stable; do not change its ids merely to match a transport.
- When a System needs editor/runtime evidence, read `.axit/systems/<system-id>/capabilities.yaml` if present, then load only the referenced capability set needed for the criterion.
- Use only capability ids explicitly declared in the active capability sets. Do not invent, alias, rename, abbreviate, or synthesize capability ids.
- If no declared capability matches an evidence need, describe that need in ordinary language and use legitimate project validation evidence when sufficient; otherwise report a capability gap. Do not fabricate an id.
- Ordinary project evidence such as standalone tests or source inspection is not automatically an Axit Capability and must not be relabeled as one unless a declared capability actually covers that mechanism.
- Capability ids must remain transport-neutral. Do not replace semantic ids with MCP, CLI, provider, or editor command names.
- A declared capability does not mean a runtime binding is currently available and does not grant permission to execute it.
- Capability output is evidence only. `verify-change` still decides REQUIRED vs SUPPORTING evidence and PASS/FAIL/BLOCKED.
- Missing/unbound acquisition is not proof that the product failed; distinguish unavailable evidence from an observed product failure.

## Runtime Binding routing

- Runtime Bindings live under `.axit/bindings/` only after a real transport and concrete operation names have been verified.
- Provider/tool-specific operation names belong in Runtime Bindings, not canonical Capability ids.
- Do not invent MCP/CLI/editor operation names from memory, examples, or capability names.
- A source-controlled binding definition does not prove the transport is currently connected; resolve availability in the current runtime/environment.
- Keep credentials, tokens, endpoints, ports, and other secrets or ephemeral machine state outside canonical `.axit` files.
- Runtime/Harness policy still owns authorization; a binding never grants permission by itself.
- Distinguish acquisition outcomes: `acquired`, `unavailable`, `denied`, and `transport_error`. None is a verification verdict by itself.
- If no reviewed binding is available for a selected Capability, report the capability as unbound/unavailable rather than silently substituting another transport.
- For path-addressed runtime evidence, follow `.axit/checklists/runtime-binding-validation.md` Case 8: resolve the current full runtime identity before use; do not infer the complete live path from prefab-relative hierarchy.

## System discipline

- `src/*` contains interacting Systems such as game client, backend, CMS, and services; they are not separate Axit projects by default.
- A System may have its own Git repository, submodule, nested repository, or separate tracking boundary without becoming a separate Axit Workspace.
- System-local Rules/Architecture/repository metadata belong under `.axit/systems/<system-id>/`.
- Cross-system relationships belong in root registries.
- Point Axit metadata to executable contracts such as OpenAPI/protobuf/schema/source files; do not duplicate those contracts in `.axit`.
- Prefer repository-level contract/integration/e2e tests for behavior that spans Systems.
- Evidence provenance must distinguish Workspace-canonical, System-canonical, local/separately-tracked, and ephemeral runtime evidence as defined by `.axit/roadmap/milestone-closure.md`.

## Migration boundary

- `.claude/` is legacy/reference material only.
- Do not bulk-migrate legacy agents, skills, or workflows.
- Keep the reviewed Core stable unless real usage demonstrates a reusable gap.
