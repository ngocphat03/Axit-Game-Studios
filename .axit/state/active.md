# Axit Workspace Active State

Updated: 2026-08-09
Status: workspace-system-refactor

## Current task

Refactor Axit metadata around the user's root-first Codex workflow: repository root is the product workspace, and `src/*` contains interacting systems such as the Unity client, backend, CMS, and services.

## Confirmed workspace facts

- Codex is normally opened from repository root.
- Systems that need to reason about each other are expected to live under `src/` in the same repository workspace.
- The current concrete system is `unity-client` at `src/QuickGun-MVP`.
- The reviewed Axit Core remains four Profiles, two Skills, and one active `bounded-change` Workflow.
- Unity-client damage behavior has live deterministic validation from prior QuickGun tests.

## Architecture direction

- `.axit/workspace.yaml` routes the repository-level product workspace.
- `.axit/systems/<system-id>/` contains system-local routing, rules, architecture, and optional knowledge/extensions.
- `.axit/registry/architecture.yaml` owns cross-system architecture truth.
- `.axit/registry/integrations.yaml` maps provider/consumer relationships and points to executable contract sources.
- `.axit` must not duplicate OpenAPI/protobuf/DTO/schema contracts that already have an executable source of truth.

## Current systems

- `unity-client` -> `src/QuickGun-MVP`

No backend, CMS, or service source roots are registered yet because they have not been materialized/confirmed on this branch.

## Active extensions

- Workspace Profiles: none.
- Workspace Skills: none.
- Workspace Workflows: none beyond shared Core.
- Unity-client Profiles/Skills/Workflows/Knowledge: none.

## Next actions

1. Complete removal of the superseded nested QuickGun `.axit` and nested `AGENTS.md` routing.
2. Validate root `AGENTS.md` routes a Unity-client task through `.axit/workspace.yaml` and `.axit/systems/unity-client/system.yaml`.
3. When backend/CMS/service source trees are added, register them as systems and point integrations to real API/schema contract sources.
4. Add repository-level contract/integration tests when a real cross-system boundary exists.
5. Do not add Core Skill #3 or another Core Workflow until a repeated gap is demonstrated.
