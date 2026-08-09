# Axit Workspace Active State

Updated: 2026-08-09
Status: root-routing-v1-live-validation

## Current task

Validate Axit's root-first Workspace/System routing through live Codex use before registering additional Systems or adding specialization.

## Confirmed workspace facts

- Codex is normally opened from repository root.
- Systems that need to reason about each other are expected to live under `src/` in the same repository workspace.
- The current concrete System is `unity-client` at `src/QuickGun-MVP`.
- The reviewed Axit Core remains four Profiles, two Skills, and one active `bounded-change` Workflow.
- Unity-client damage behavior has live deterministic validation from prior QuickGun tests.

## Canonical routing

- Root `AGENTS.md` is the lightweight Codex router.
- `.axit/workspace.yaml` maps source roots to Systems.
- `.axit/systems/<system-id>/` contains system-local routing, rules, architecture, and optional knowledge/extensions.
- `.axit/registry/architecture.yaml` owns workspace-level/cross-system architecture truth.
- `.axit/registry/integrations.yaml` maps provider/consumer relationships and points to executable contract sources.
- `.axit` does not duplicate OpenAPI/protobuf/DTO/schema contracts that already have an executable source of truth.

## Current systems

- `unity-client` -> `src/QuickGun-MVP`

No backend, CMS, or service source roots are registered yet because they have not been materialized/confirmed on this branch.

## Active extensions

- Workspace Profiles: none.
- Workspace Skills: none.
- Workspace Workflows: none beyond shared Core.
- Unity-client Profiles/Skills/Workflows/Knowledge: none.

## Completed refactor

- Added root workspace manifest and Workspace/System Spec v1.
- Added Unity-client System metadata at `.axit/systems/unity-client/`.
- Added workspace architecture and integration registries.
- Replaced Project Layer bootstrap templates with Workspace/System templates.
- Removed nested `src/QuickGun-MVP/.axit/` metadata and nested `AGENTS.md` routing.
- Removed the superseded Project Layer spec/templates.
- Added `.axit/checklists/root-routing-validation.md` with fixed single-system, cross-system, contract-source, and future integration verification regression cases.

## Next actions

1. Pull this branch into the local workspace and start a fresh Codex session from repository root.
2. Run the currently executable cases in `.axit/checklists/root-routing-validation.md` without adding manual routing hints.
3. Record any routing mistake before changing instructions so the regression remains reproducible.
4. Add a backend/CMS/service System only when a real source boundary exists.
5. Validate the first real cross-system API change with an executable contract plus contract/integration tests.
6. Do not add Core Skill #3 or another Core Workflow until a repeated gap is demonstrated.
