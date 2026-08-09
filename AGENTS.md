# Axit Workspace

This repository is a Codex-first product workspace. Codex is expected to run from the repository root.

## Root routing

- `.axit/` is the Axit source of truth; `.agents/skills/` is Codex Skill discovery only.
- Read `.axit/workspace.yaml` when a task touches product source under `src/` or spans multiple components.
- Map affected source paths to registered Systems, then read only the relevant `.axit/systems/<system-id>/` context.
- Read `.axit/registry/architecture.yaml` and `.axit/registry/integrations.yaml` when a change crosses System boundaries, ownership, or public contracts.
- Do not recursively preload `.axit/`.

## Core behavior

- Shared Core lives under `.axit/core/`.
- When the user requests end-to-end implementation plus independent verification for one bounded change, follow `.axit/core/workflows/bounded-change/WORKFLOW.md`.
- `implement-change` and `verify-change` remain independently usable when only one job is requested.
- If product intent or a material architecture decision is unresolved, hand off to the owning Core Profile instead of guessing.

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
