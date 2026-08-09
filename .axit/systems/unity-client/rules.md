# Unity Client System Rules

These rules apply to `src/QuickGun-MVP` and constrain system-local work without replacing Axit Core procedures or workspace architecture registries.

## Scope discipline

- Preserve unrelated working-tree changes across the repository.
- Keep implementation bounded to the accepted task; do not refactor adjacent combat systems merely because they are nearby.
- Do not add a new package, framework, external service, or project-wide runtime assumption without surfacing the architectural consequence.
- Do not change cross-system API contracts, networking topology, or authoritative state ownership as an ordinary local implementation detail.

## Architecture discipline

- Read `.axit/systems/unity-client/architecture.yaml` before changing registered QuickGun-local damage behavior.
- Read root `.axit/registry/architecture.yaml` and `.axit/registry/integrations.yaml` when a change crosses the Unity client boundary.
- If source truth contradicts an Axit registry entry, surface the conflict and update the owning source/registry deliberately rather than silently choosing one.
- Preserve one shared Player/Bot damage path while that contract remains accepted.

## Verification discipline

- Prefer deterministic tests for arithmetic, clamping, ordering, serialization transforms, and other pure rules.
- Use Unity/editor/runtime evidence when acceptance criteria depend on scene wiring, serialized state, lifecycle behavior, or presentation.
- Treat unavailable Unity tooling as an evidence limitation; it blocks completion only when that evidence is required.
- For cross-system changes, add or update contract/integration tests at the repository level when practical.

## Extension discipline

- No Unity-client-specific Profile, Skill, Workflow, or Knowledge entry is active yet.
- Add a system extension only after repeated work demonstrates a stable gap that Core plus Rules/Architecture cannot express cleanly.
- Do not import legacy Game Studios agents or skills by default.
