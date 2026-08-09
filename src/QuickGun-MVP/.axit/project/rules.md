# QuickGun-MVP Project Rules

These rules constrain project work without replacing Axit Core procedures or the architecture registry.

## Scope discipline

- Preserve unrelated working-tree changes.
- Keep each implementation bounded to the accepted task; do not refactor adjacent combat systems merely because they are nearby.
- Do not add a new package, framework, service, or project-wide runtime assumption without an explicit architecture decision.
- Do not change networking topology, shared-state ownership, or public cross-system contracts as an ordinary implementation detail.

## Architecture discipline

- Read `.axit/registry/architecture.yaml` before changing the registered shared damage pipeline or its ordering rules.
- If current local code contradicts a registered decision, surface the conflict instead of silently choosing one source of truth.
- Prefer one shared implementation path for Player and Bot where the registry declares behavior shared.

## Verification discipline

- Prefer deterministic tests for arithmetic, clamping, ordering, and other pure damage rules.
- Use Unity/editor/runtime evidence when an acceptance criterion depends on live scene wiring, serialized state, lifecycle behavior, or presentation.
- Treat unavailable Unity tooling as a recorded evidence limitation. It blocks completion only when the missing runtime/editor evidence is required by the accepted criterion or another project rule.
- Do not treat implementation-side tests as the independent completion verdict when `verify-change` is part of the requested flow.

## Project extension discipline

- No project-specific Profile, Skill, Workflow, or Knowledge entry is active yet.
- Add a project extension only after repeated QuickGun work demonstrates a stable gap that Core plus project Rules/Registry cannot express cleanly.
- Do not copy legacy Game Studios agents or skills into this project by default.
