# .axit — Canonical Axit Game Studio Base

`.axit/` is the source of truth owned by Axit.

It is independent from Codex discovery conventions. Codex integration is a thin compatibility layer outside this directory.

## Layout

```text
.axit/
├── README.md
├── core/
│   ├── core.yaml
│   ├── profiles/
│   ├── skills/
│   └── workflows/
├── specs/
├── templates/
├── checklists/
├── registry/
├── knowledge/
└── state/
```

## Responsibilities

### `core/`
Only reusable capabilities that are genuinely useful across materially different game projects.

The Core must remain small. A profile or skill is not Core merely because it existed in the original Game Studios repository.

### `specs/`
Axit format contracts for Profiles, Skills, Workflows, capabilities, manifests, and other canonical artifacts.

### `templates/`
Files copied into a concrete game project during bootstrap, such as `project.yaml` and an architecture registry.

### `checklists/`
Framework-level checklists. The initial checklist contains only universal project concerns. Domain-specific checks are added later only when a reusable domain module is proven.

### `registry/`
Framework-level registries. Concrete games should maintain their own architecture/state-ownership registry after bootstrap.

### `knowledge/`
Small, curated reusable knowledge that is useful across projects. Do not use this as a dump for all project documentation.

### `state/`
File-backed progress/recovery information for Axit framework development. Concrete games may use the same pattern in their own `.axit/state/`.

## Codex boundary

Codex automatically discovers repository skills from `.agents/skills/`, not from `.axit/`.

Therefore:

```text
.axit/core/skills/<skill>/SKILL.md
        = canonical Axit skill

.agents/skills/<skill>
        = Codex discovery/compatibility entry
```

The compatibility entry may later be implemented as a symlink or generated projection. It must not become a second source of truth.

## Current phase

No legacy Game Studios agents or skills are being migrated in this phase.

The goal is to confirm this directory contract first. Profiles and Skills will be reviewed and introduced one by one afterward.
