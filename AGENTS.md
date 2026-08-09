# Axit Game Studio Base

This repository is being redesigned as a Codex-first, reusable game-development base.

## Canonical source

- Axit-owned configuration and reusable framework files live under `.axit/`.
- Read `.axit/README.md` first when working on the Axit framework itself.
- Do not preload or recursively read the entire `.axit/` tree. Read only files relevant to the current task.

## Codex compatibility

- `.agents/skills/` exists only for Codex-discoverable Skill entries.
- Canonical Skill content lives under `.axit/core/skills/`.
- Do not treat `.agents/` as the Axit source of truth.

## Workflow routing

- When the user explicitly requests end-to-end implementation **and** independent verification for one bounded change, read and follow `.axit/core/workflows/bounded-change/WORKFLOW.md`.
- Do not route to that Workflow when the request is only implementation, only verification, design-only work, or architecture-only work.
- If product intent or a material architecture decision is unresolved, hand off to the owning Profile instead of using the Workflow to guess.

## Migration boundary

- The existing `.claude/` tree is legacy/reference material only.
- Do not copy, migrate, or activate legacy agents/skills unless explicitly selected for review.
- Do not introduce large agent or skill catalogs by default.

## Current implementation phase

1. keep the Core small;
2. keep the four reviewed Core Profiles stable unless a demonstrated responsibility gap appears;
3. validate `implement-change` and `verify-change` as independent Core Skills;
4. validate `bounded-change` as the first Core Workflow without making it mandatory for every task;
5. add further Skills, Workflows, domain specialization, or MCP-backed evidence capabilities only after real use demonstrates the need.
