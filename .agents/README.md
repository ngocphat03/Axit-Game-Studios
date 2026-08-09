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
- `system-design`
- `architecture-decision`
- `implementation-plan`
- `implement-change`
- `verify-feature`
- `checkpoint-state`

Core workflows:
- `project-bootstrap`
- `feature-delivery`

## Starter Packs

- `engine-unity`
- `puzzle`
- `mission-quest`
- `online-session`
- `host-authoritative`
- `p2p`
- `persistence`

Pack manifests are intentionally minimal. Their proposed Profiles/Skills are not considered implemented merely because they are named in `pack.yaml`; add them only when a project vertical slice proves they are needed.

## How to specialize a project

1. Copy `.agents/templates/project.yaml` into the target project's `.agents/project.yaml`.
2. Copy `.agents/templates/project-rules.md` into `.agents/rules/project-rules.md` and fill project-specific authority/rules.
3. Fill confirmed project facts only.
4. Run the Project Customization Checklist in `docs/agent-spec/project-customization-checklist-v1.md`.
5. Select the smallest relevant Packs from `packs/`.
6. Copy `.agents/templates/architecture-registry.yaml` into the project and record shared/high-risk architecture decisions.
7. Run `.agents/prompts/project-expander.md` against the target project to identify genuinely project-specific Agents, Skills, Workflows, and validation.
8. Prove one vertical slice before expanding the catalog.

## Important boundaries

- Core does not assume Unity, missions, networking, combat, puzzle gameplay, or a specific provider.
- Profiles are responsibility/context packages; they do not imply one model instance per profile.
- Skills request semantic capabilities rather than provider tool names.
- Packs may specialize one domain but may not grant runtime permissions.
- Reference projects such as Black Commission are evidence sources, not templates.
- Legacy `.claude/` remains separate until provider adapter parity is proven.

See `docs/agent-spec/README.md` for the full design.