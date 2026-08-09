# Runtime Binding v1 Validation

Status: awaiting-real-transport

This checklist validates the Runtime Binding layer after Capability semantic v1 has been accepted as stable.

The goal is to prove that transport-specific execution can be introduced without leaking provider/tool semantics back into Core, Workspace/System routing, or canonical Capability ids.

## Current precondition

The `unity-client` System is semantically ready but currently unbound.

Repository inspection on 2026-08-09 did not find:

- root `.mcp.json`;
- root `.codex/config.toml`;
- a repository-visible Unity package manifest at `src/QuickGun-MVP/Packages/manifest.json`;
- repository code-search evidence identifying a concrete Unity MCP/adapter.

This does not prove the user's local Codex environment has no globally configured transport. It only means the repository cannot currently justify a concrete binding definition.

Do not fabricate one.

## First live binding scope

Bind only:

```text
unity.prefab.inspect
unity.serialized-fields.inspect
unity.playmode.verify
```

Leave all other Unity capabilities unbound until a live task demonstrates need.

## Case 1 — real transport discovery before mapping

Before creating a binding file, inspect the actual local runtime/Codex environment and answer:

- Which Unity transport/adapter is actually available?
- What concrete operations does it expose?
- Can it reach the intended Unity project/editor instance?
- What structured/raw results do the relevant operations return?

Pass condition:

- every concrete transport operation written into a binding was observed in the actual transport interface;
- no tool/server/action name is inferred from memory or examples.

## Case 2 — semantic id discipline

A binding may map only Capability ids declared by the affected System's active capability sets.

For the first slice, valid semantic ids are exactly:

```text
unity.prefab.inspect
unity.serialized-fields.inspect
unity.playmode.verify
```

A transport operation name may be provider-specific inside the binding, but it must not replace or rename the semantic Capability id.

Failure examples:

```text
mcp.get_prefab used as the Axit Capability id
unity_mcp_play_mode used as a new semantic id
prefab.read_fields invented instead of unity.serialized-fields.inspect
```

## Case 3 — binding definition is not runtime availability

A reviewed binding file may exist while the editor, MCP server, process, socket, or environment is unavailable on a specific run.

Expected behavior:

```text
binding definition exists
+ runtime transport unavailable
=> acquisition status: unavailable
=> not product FAIL by itself
```

Do not commit machine-specific `connected: true/false` state as canonical truth.

## Case 4 — acquisition outcome classification

The runtime/binding must preserve these distinctions:

```text
acquired
unavailable
denied
transport_error
```

Examples:

- prefab operation successfully returns inspected serialized state -> `acquired`;
- no compatible editor/server is reachable -> `unavailable`;
- Harness policy refuses Play Mode -> `denied`;
- MCP call crashes before trustworthy target evidence is returned -> `transport_error`.

Only acquired target evidence can directly demonstrate product behavior/configuration. The other statuses describe acquisition state, not product correctness.

## Case 5 — permission boundary

A binding for `unity.playmode.verify` must not imply automatic permission to enter or drive Play Mode.

Pass condition:

- Runtime/Harness policy remains authoritative;
- a denied operation is reported as denied/unavailable evidence as appropriate;
- no binding file broadens source-writing or destructive permissions.

## Case 6 — secret and endpoint discipline

Do not store in `.axit`:

- credentials or tokens;
- machine-specific absolute editor/socket paths when they are ephemeral;
- local-only ports that change per session;
- authentication secrets;
- other environment secrets.

A source-controlled binding may reference a stable logical runtime connection/adapter identity when one exists, while actual connection details remain external.

## Case 7 — first vertical slice

After the three mappings are live, validate one real criterion chain:

```text
ordinary deterministic C# tests
        +
unity.prefab.inspect
        +
unity.serialized-fields.inspect
        +
unity.playmode.verify
        -> verify-change
```

Suggested criterion:

- the Player prefab has the accepted `DamageableBodyPart` serialized configuration;
- the exact accepted hit-zone/armor values/references are present;
- a bounded Play Mode headshot produces the expected runtime health result;
- the deterministic damage calculation regression tests still pass.

Expected evidence boundaries:

- deterministic tests prove pure arithmetic rules;
- prefab/serialized capabilities prove asset configuration only;
- Play Mode proves the exercised runtime behavior only;
- `verify-change` owns final criterion mapping and verdict.

## Acceptance criteria for Runtime Binding v1

Runtime Binding v1 is ready to promote when:

- the transport and concrete operation names were verified from the actual environment;
- only the three intended Capability ids are mapped initially;
- semantic Capability ids remain unchanged;
- current availability is resolved at runtime rather than inferred from committed config;
- acquired/unavailable/denied/transport_error remain distinct;
- permissions remain under Runtime/Harness control;
- no secrets or ephemeral machine state are committed;
- the vertical slice produces traceable evidence and a correct `verify-change` verdict.

Until those conditions are met, keep `unity-client` binding status unbound.
