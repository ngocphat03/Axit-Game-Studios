# Axit Game Studio Base

This repository is being redesigned as a Codex-first, reusable game-development base.

## Canonical source

- Axit-owned configuration and reusable framework files live under `.axit/`.
- Read `.axit/README.md` first when working on the Axit framework itself.
- Do not preload or recursively read the entire `.axit/` tree. Read only files relevant to the current task.

## Codex compatibility

- `.agents/skills/` is reserved only for Codex-discoverable skill entries.
- Canonical skill content will live under `.axit/core/skills/` and may later be exposed to Codex through `.agents/skills/`.
- Do not treat `.agents/` as the Axit source of truth.

## Migration boundary

- The existing `.claude/` tree is legacy/reference material only.
- Do not copy, migrate, or activate legacy agents/skills unless explicitly selected for review.
- Do not introduce large agent or skill catalogs by default.

## Current implementation phase

The current phase is structure-first:

1. keep the Core small;
2. establish the `.axit/` layout;
3. define Core contracts/checklists;
4. migrate Profiles and Skills individually only after review;
5. verify one vertical slice before expanding the catalog.
