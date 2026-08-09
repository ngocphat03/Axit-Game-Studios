# Axit Workspace

This repository is a Codex-first product workspace. Codex is expected to run from the repository root.

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

## System discipline

- `src/*` contains interacting Systems such as game client, backend, CMS, and services; they are not separate Axit projects by default.
- System-local Rules/Architecture belong under `.axit/systems/<system-id>/`.
- Cross-system relationships belong in root registries.
- Point Axit metadata to executable contracts such as OpenAPI/protobuf/schema/source files; do not duplicate those contracts in `.axit`.
- Prefer repository-level contract/integration/e2e tests for behavior that spans Systems.

## Migration boundary

- `.claude/` is legacy/reference material only.
- Do not bulk-migrate legacy agents, skills, or workflows.
- Keep the reviewed Core stable unless real usage demonstrates a reusable gap.
