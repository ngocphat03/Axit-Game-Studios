# Axit Workspace — Antigravity Orchestrator

This repository is an Antigravity-first product workspace running with Gemini models.

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

### Model routing and cost policy

Before spawning delegated work, read `.axit/policies/model-routing.md`.

Workspace defaults with Gemini are:

```text
primary orchestrator = gemini-2.5-pro / high
all child lanes       = gemini-2.5-flash / medium (or gemini-2.5-pro for complex verification)
```

This applies to explorers, workers, Unity/MCP evidence lanes, repair/recovery agents, independent verifiers, closure verifiers, report authors, and custom sub-agents.

A child lane must not autonomously escalate models or reasoning beyond its assigned scope. When a child underperforms, first sharpen/distill context, steer/resume, replace with another child when needed, or decompose the task.

Use concurrency only for materially independent lanes. Read-only work may parallelize; overlapping writes, Runtime Binding/state/report writes, and Unity/editor mutations must remain serialized. Do not spawn agents merely to fill available slots, and close obsolete/completed lanes promptly.

### Manual Unity MCP prerequisite

MCP for Unity setup is user-owned and must be configured in `.agents/mcp_config.json` before any accepted milestone that requires Unity MCP evidence begins.

The orchestrator and sub-agents may inspect and use the currently available Unity MCP transport, but they must not:

- install or upgrade CoplayDev/unity-mcp;
- add/remove Unity packages solely to set up MCP;
- edit global/user IDE MCP configuration;
- start/configure/repair the Unity MCP bridge on the user's behalf;
- invent a missing transport or operation name.

If an accepted milestone requires Unity MCP and the expected transport is missing, unreachable, or not connected to the intended Unity editor/project at readiness, stop with `HARD_BLOCKER: UNITY_MCP_NOT_READY` and report only the observed missing prerequisite. Do not attempt setup.

### Sub-agent recovery

If a sub-agent pauses, blocks, times out, or returns an incomplete result, the orchestrator should recover without asking the user when the issue remains inside the accepted execution envelope:

1. capture the sub-agent's last useful result, blocker, changed files, and current workspace/runtime state;
2. steer or resume the same sub-agent with a focused recovery instruction when recovery is safe;
3. if the lane remains stuck, close/replace it with a fresh sub-agent using distilled context rather than replaying the full conversation;
4. allow at most two bounded recovery/replacement attempts for the same lane or required failure;
5. after recovery, reacquire current evidence rather than reusing stale results;
6. checkpoint meaningful current progress in `.axit/state/active.md` or the current milestone artifact through a delegated write lane and continue automatically.

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

## Root routing

- `.axit/` is the Axit source of truth; `.agents/skills/` is Antigravity Skill discovery.
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
- Capability output is evidence only. `verify-change` still decides REQUIRED vs SUPPORTING evidence and PASS/FAIL/BLOCKED.

## Runtime Binding routing

- Runtime Bindings live under `.axit/bindings/` only after a real transport and concrete operation names have been verified.
- Provider/tool-specific operation names belong in Runtime Bindings, not canonical Capability ids.
- Distinguish acquisition outcomes: `acquired`, `unavailable`, `denied`, and `transport_error`. None is a verification verdict by itself.
