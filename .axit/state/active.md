# Axit Workspace Active State

Updated: 2026-08-09
Status: runtime-binding-v1-transport-discovery

## Current task

Introduce Runtime Binding v1 after promoting Capability semantic v1 to stable, without inventing a Unity transport that is not actually visible in the repository/runtime evidence.

## Stable foundations

### Core v1

- Profiles: `game-designer`, `technical-architect`, `implementation-engineer`, `quality-verifier`.
- Skills: `implement-change`, `verify-change`.
- Workflow: `bounded-change`.
- Status: frozen/stable unless repeated live use demonstrates another reusable procedural or responsibility gap.

### Workspace/System routing v1

- Root-first routing is stable after live cases 1-4 passed.
- `src/QuickGun-MVP` resolves to System `unity-client`.
- Missing cross-System contracts remain explicit unknowns rather than inferred behavior.
- Live cross-System verification remains deferred until at least two real Systems exist.

### Capability semantic v1

- Capability Spec: `.axit/specs/capability-spec-v1.md`.
- Stable Unity set: `.axit/capabilities/unity/evidence.yaml`.
- Unity System routing: `.axit/systems/unity-client/capabilities.yaml`.
- Live Cases 1-5 are now recorded PASS in `.axit/checklists/unity-evidence-capability-validation.md`.

The stable semantic layer preserves:

- criterion-driven capability selection;
- declared capability-id discipline;
- ordinary project evidence vs Capability separation;
- static/serialized/runtime proof boundaries;
- unavailable acquisition vs demonstrated product failure;
- Runtime/Harness permission boundary;
- `verify-change` verdict ownership.

## Runtime Binding v1

- Spec: `.axit/specs/runtime-binding-spec-v1.md`.
- Binding root: `.axit/bindings/`.
- Validation checklist: `.axit/checklists/runtime-binding-validation.md`.
- Current `unity-client` binding status: **unbound**.

Runtime Binding v1 separates:

```text
semantic Capability
  -> reviewed binding definition
      -> concrete transport operation(s)
          -> runtime availability / policy
              -> acquisition result + evidence
                  -> verify-change
```

Binding acquisition outcomes must distinguish:

- `acquired`;
- `unavailable`;
- `denied`;
- `transport_error`.

These are acquisition states, not PASS/FAIL/BLOCKED verdicts.

## Transport discovery result

Repository inspection on 2026-08-09 found no branch-visible evidence sufficient to create a concrete Unity binding:

- `.mcp.json` not present at repository root;
- `.codex/config.toml` not present at repository root;
- `src/QuickGun-MVP/Packages/manifest.json` not present on the remote branch;
- repository search did not identify a concrete Unity MCP/adapter operation catalog.

This does **not** prove the user's local Codex environment lacks a globally configured Unity transport. It means the repository cannot justify concrete transport operation names yet.

Do not guess them.

## First intended binding slice

After real transport discovery, bind only:

```text
unity.prefab.inspect
unity.serialized-fields.inspect
unity.playmode.verify
```

Leave other Unity capabilities unbound until live use demonstrates need.

The first vertical slice should combine:

```text
ordinary deterministic C# tests
+ prefab inspection evidence
+ serialized-field evidence
+ bounded Play Mode evidence
-> verify-change
```

## Next actions

1. Inspect the user's actual local Codex/runtime Unity transport configuration and available operation names.
2. Confirm the transport can reach the intended Unity project/editor.
3. Materialize one `.axit/bindings/unity-client/<binding-id>.yaml` only from verified operations.
4. Map only the three intended Capability ids initially.
5. Run `.axit/checklists/runtime-binding-validation.md`.
6. Execute the first end-to-end evidence vertical slice and verify acquisition-state semantics plus final `verify-change` behavior.
7. Do not add Core Skill #3, Workflow #2, or bind the entire Unity capability catalog during this phase.
