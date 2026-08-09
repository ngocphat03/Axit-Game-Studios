# Axit Game Studio Base

This repository is being redesigned as a Codex-first, reusable game-development base.

## Canonical source

- Axit-owned configuration and reusable framework files live under `.axit/`.
- Read `.axit/README.md` first when working on the Axit framework itself.
- Do not preload or recursively read the entire `.axit/` tree. Read only files relevant to the current task.

## Codex compatibility

- `.agents/skills/` exists only for Codex-discoverable Skill entries.
- Canonical Skill content lives under `.axit/core/skills/` or, in a concrete game, reviewed `.axit/project/skills/` entries.
- Do not treat `.agents/` as the Axit source of truth.

## Workflow routing

- When the user explicitly requests end-to-end implementation **and** independent verification for one bounded change, read and follow `.axit/core/workflows/bounded-change/WORKFLOW.md`.
- Do not route to that Workflow when the request is only implementation, only verification, design-only work, or architecture-only work.
- If product intent or a material architecture decision is unresolved, hand off to the owning Profile instead of using the Workflow to guess.

## Project Layer routing

- A concrete game should use `.axit/project.yaml` as compact routing context.
- Read project Rules, Registry, Knowledge, State, Profiles, Skills, or Workflows only when the current task requires them.
- Do not assume every artifact under `.axit/project/` is active; activation should be explicit in the project manifest or current task routing.
- Do not create project-specific Profiles or Skills merely to reproduce legacy specialist job titles.

## Migration boundary

- The existing `.claude/` tree is legacy/reference material only.
- Do not copy, migrate, or activate legacy agents/skills unless explicitly selected for review.
- Do not introduce large agent or skill catalogs by default.

## Current implementation phase

1. keep the reviewed Core stable at four Profiles, two Skills, and one active Workflow;
2. define and validate the Project Layer contract without adding speculative project-specific catalogs;
3. bootstrap a real project from `.axit/templates/` and observe which local Rules, Knowledge, or extensions are actually needed;
4. prefer Rules/Knowledge before creating project Skills, and prefer focused Skills before creating additional Profiles;
5. add MCP-backed evidence capabilities as execution support without changing Core verification semantics;
6. promote new behavior into Core only after reuse is demonstrated across materially different projects.
