# Axit Game Studio Packs

Packs are optional reusable specializations selected by a concrete project's `.agents/project.yaml`.

Current starter manifests:

- `engine-unity`
- `puzzle`
- `mission-quest`
- `online-session`
- `host-authoritative`
- `p2p`
- `persistence`

These are **starter contracts**, not complete mature libraries yet. Each Pack must be proven by project vertical slices before its Agent/Skill catalog grows.

Rule of thumb:

```text
Universal across unrelated games -> .agents Core
Reusable within one meaningful domain -> packs/<domain>
Specific to one game -> that project's .agents Project Layer
```

See `docs/agent-spec/pack-spec-v1.md`.