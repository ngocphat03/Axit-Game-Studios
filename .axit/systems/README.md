# Axit Systems

Each registered System maps one coherent source/runtime boundary under the root product workspace.

Canonical shape:

```text
.axit/systems/<system-id>/
├── system.yaml
├── rules.md
├── architecture.yaml
├── capabilities.yaml    # optional; semantic capability-set routing
└── knowledge/           # only when needed
```

`capabilities.yaml` is optional. Use it only when the System needs reusable execution/editor evidence semantics. It points to semantic capability sets under `.axit/capabilities/` and does not contain transport/tool bindings.

A declared capability set does not mean every capability is currently executable. Runtime binding and environment availability are separate concerns.

System-local Profiles, Skills, or Workflows should be created only after repeated work demonstrates a gap not covered by Core plus Rules/Architecture/Knowledge/Capabilities.

Current registered System:

- `unity-client` -> `src/QuickGun-MVP`
  - semantic capability set: `.axit/capabilities/unity/evidence.yaml`
  - runtime binding status: unbound

Backend, CMS, and service Systems should be added only when their source roots/boundaries are materialized and understood.
