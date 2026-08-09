# QuickGun-MVP Axit Project

This directory is the first concrete game project used to validate the Axit Project Layer.

## Project routing

- Read `.axit/project.yaml` first for project identity, Axit paths, active extensions, and validation routes.
- Reuse the reviewed Axit Core from `../../.axit/core/`; do not copy Core Profiles, Skills, or Workflows into this project.
- Read `.axit/project/rules.md` before changing shared QuickGun behavior.
- Read `.axit/registry/architecture.yaml` before changing shared damage contracts, state ownership, public boundaries, or other registered architecture decisions.
- Read `.axit/state/active.md` when resuming ongoing work or when current project status materially affects the task.

## Context discipline

- Do not recursively preload the whole `.axit/` tree.
- Load only the project files relevant to the current task.
- Project-specific Profiles, Skills, Workflows, and Knowledge are currently empty; do not invent them merely because a task is specialized.
- If local source truth conflicts with the manifest or registry, surface the discrepancy and update the owning Axit artifact instead of silently guessing.

## Core workflow

When the user explicitly requests end-to-end implementation plus independent verification for one bounded change and its design/architecture are sufficiently resolved, follow `../../.axit/core/workflows/bounded-change/WORKFLOW.md`.

`implement-change` and `verify-change` remain independently usable when only one job is requested.
