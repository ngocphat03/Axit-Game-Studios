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
- When a System needs editor/runtime evidence, read `.axit/systems/<system-id>/capabilities.yaml` if present, then load only the referenced capability set needed for the criterion.
- Capability ids must remain transport-neutral. Do not replace semantic ids with MCP, CLI, provider, or editor command names.
- A declared capability does not mean a runtime binding is currently available and does not grant permission to execute it.
- Capability output is evidence only. `verify-change` still decides REQUIRED vs SUPPORTING evidence and PASS/FAIL/BLOCKED.
- Missing/unbound acquisition is not proof that the product failed; distinguish unavailable evidence from an observed product failure.

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
