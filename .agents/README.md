# Axit Game Studio Base

This directory is the canonical provider-neutral **Universal Core** for reusable game-development agents.

It is intentionally small. Game-specific behavior belongs in selected `packs/` and the consuming project's `.agents/` Project Layer.

## Universal Core

Profiles:
- `project-coordinator`
- `game-designer`
- `technical-architect`
- `implementation-engineer`
- `qa-verifier`

Core skills:
- `project-discovery`
- `system-map`
- `architecture-decision`
- `implementation-plan`
- `implement-change`
- `verify-feature`

Core workflows:
- `project-bootstrap`
- `feature-delivery`

## How to specialize a project

1. Copy `.agents/templates/project.yaml` into the target project's `.agents/project.yaml`.
2. Fill confirmed project facts only.
3. Run the Project Customization Checklist in `docs/agent-spec/project-customization-checklist-v1.md`.
4. Select the smallest relevant Packs from `packs/`.
5. Copy `.agents/templates/architecture-registry.yaml` into the project and record shared/high-risk architecture decisions.
6. Run `.agents/prompts/project-expander.md` against the target project to identify genuinely project-specific Agents, Skills, Workflows, and validation.
7. Prove one vertical slice before expanding the catalog.

## Important boundaries

- Core does not assume Unity, missions, networking, combat, puzzle gameplay, or a specific provider.
- Profiles are responsibility/context packages; they do not imply one model instance per profile.
- Skills request semantic capabilities rather than provider tool names.
- Packs may specialize one domain but may not grant runtime permissions.
- Reference projects such as Black Commission are evidence sources, not templates.
- Legacy `.claude/` remains separate until provider adapter parity is proven.

See `docs/agent-spec/README.md` for the full design.