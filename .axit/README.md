# .axit — Canonical Axit Workspace

`.axit/` is the Axit-owned source of truth for reusable Core behavior, root Workspace/System routing, semantic Capabilities, Runtime Bindings, registries, and compact task state.

Codex integration is intentionally thin: root `AGENTS.md` routes context, and `.agents/skills/` exposes active Skills for discovery.

## Layout

```text
.axit/
├── README.md
├── workspace.yaml
├── core/
│   ├── core.yaml
│   ├── profiles/
│   ├── skills/
│   └── workflows/
├── capabilities/
│   └── <domain>/
│       └── <set>.yaml
├── bindings/
│   └── <system-id>/
│       └── <binding-id>.yaml    # only after a real transport is verified
├── systems/
│   └── <system-id>/
│       ├── system.yaml
│       ├── rules.md
│       ├── architecture.yaml
│       ├── capabilities.yaml    # optional semantic capability routing
│       └── knowledge/
├── registry/
│   ├── architecture.yaml
│   └── integrations.yaml
├── state/
│   └── active.md
├── specs/
├── templates/
├── checklists/
└── knowledge/
```

## Responsibilities

### `workspace.yaml`
Root routing manifest. Maps source roots under `src/` to registered Systems and points to shared Core, registries, and workspace validation roots.

### `core/`
Small reusable behavior proven useful across materially different workspaces/systems. Core v1 is frozen at four Profiles, two Skills, and one active Workflow until real repeated use demonstrates another reusable gap.

### `capabilities/`
Reusable semantic evidence/execution operations. Capability ids describe intent such as `unity.compile` or `unity.prefab.inspect`; provider/tool bindings such as MCP, CLI, or Axit Unity remain outside canonical capability definitions.

Capability semantic v1 is stable after live routing/selection validation. Capability output is evidence, not a verification verdict, and declaring a capability does not grant execution permission.

### `bindings/`
Reviewed mappings from semantic Capability ids to concrete transport operations. Provider/tool names are allowed here because this layer isolates transport details.

A binding definition does not prove the transport is currently connected. Runtime availability, credentials, endpoints, and permissions remain external/runtime concerns. Do not create a concrete binding until the actual transport interface has been inspected.

### `systems/`
System-local context for components such as Unity client, backend, CMS, or services. Local Rules/Architecture live here; an optional `capabilities.yaml` sidecar declares which semantic capability sets are relevant when execution/editor evidence is needed. System-specific extensions remain empty until repeated work proves a gap.

### `registry/`
Workspace-level and cross-system truth. `architecture.yaml` records shared ownership/boundaries; `integrations.yaml` maps providers, consumers, executable contract sources, and boundary tests.

### `state/`
Compact file-backed task state for root Codex sessions. It is a checkpoint, not conversation history.

### `specs/`
Axit contracts for Profiles, Skills, Workflows, Workspace/System routing, Capabilities, Runtime Bindings, and registries.

### `templates/`
Bootstrap files for new workspace/system metadata. Templates are not runtime truth after materialization.

### `checklists/`
Validation notes and framework-level regression evidence.

### `knowledge/`
Curated reusable Axit knowledge only. System or workspace-specific reference material belongs at the narrowest owning scope.

## Evidence boundary

The intended relationship is:

```text
accepted criterion
    -> Skill/Workflow selects needed evidence
        -> semantic Capability when editor/runtime execution is needed
            -> reviewed Runtime Binding
                -> concrete transport operation(s)
                    -> acquisition result + evidence
                        -> verify-change judgment
```

Do not encode PASS/FAIL/BLOCKED into Capability definitions or Runtime Bindings. Do not make a Capability required merely because it exists.

Runtime acquisition must distinguish trustworthy acquired evidence from unavailable transport/environment, denied execution, and transport errors.

## Source-of-truth boundary

Axit metadata should route to real executable truth rather than copy it.

Examples that should remain outside `.axit` when they already exist:

- OpenAPI/protobuf/schema files;
- DTO/shared protocol source;
- database migrations;
- generated client contracts;
- product source and tests;
- secrets, credentials, tokens, and ephemeral machine connection state.

`.axit` records ownership, relationships, constraints, capability semantics, reviewed binding mappings, and verification routes.

## Codex boundary

```text
.axit/core/skills/<skill>/SKILL.md
        = canonical Core Skill

.agents/skills/<skill>/SKILL.md
        = Codex discovery shim/projection
```

Only active Skills should be exposed for discovery. Capabilities and Runtime Bindings are not projected as Skills. Do not treat `.agents/` as a second Axit source of truth.
