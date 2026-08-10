# Runtime Binding v1 Validation

Status: active

This checklist validates the Runtime Binding layer after Capability semantic v1 has been accepted as stable.

The goal is to prove that transport-specific execution can be introduced without leaking provider/tool semantics back into Core, Workspace/System routing, or canonical Capability ids.

## Current binding state

The `unity-client` System references the reviewed active `coplaydev-unity-mcp` binding. The active mapping is limited to exactly:

```text
unity.prefab.inspect
unity.serialized-fields.inspect
unity.playmode.verify
```

The other six declared Unity capabilities remain explicitly unbound:

```text
unity.project.inspect
unity.compile
unity.tests.run
unity.scene.inspect
unity.component.inspect
unity.console.inspect
```

`active` records the reviewed source-controlled mapping. It does not claim that the transport is currently connected or available; runtime availability must still be resolved for each acquisition.

The discovery rules below continue to apply to any new or expanded mapping. Do not fabricate a binding or operation name.

## Active first live binding scope

The active binding maps only:

```text
unity.prefab.inspect
unity.serialized-fields.inspect
unity.playmode.verify
```

Leave all other Unity capabilities unbound until a live task demonstrates need and the same validation criteria are satisfied.

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

## Case 8 — runtime target identity must be resolved from current runtime context

M1 and M2 both demonstrated the same failure pattern: a prefab-relative hierarchy was treated as a complete live scene hierarchy and omitted the `GameEnvironment/` runtime wrapper.

For any path-addressed runtime acquisition or mutation:

```text
prefab/source hierarchy
!=
full runtime hierarchy
```

Required procedure:

1. acquire or derive the current scene/runtime hierarchy using current read-only evidence available to the task;
2. resolve the complete runtime target path/identity before the first path-addressed mutation/acquisition;
3. freeze that resolved identity only for the current acquisition window;
4. if refresh/reload invalidates the identity, resolve it again rather than guessing or reusing stale identifiers;
5. perform mandatory cleanup even when target resolution/acquisition fails.

Do not invent a new Capability merely to satisfy this procedure. Ordinary read-only source/scene evidence may resolve identity when sufficient. Add/bind a broader scene/component capability only if a later accepted criterion demonstrates that such evidence is REQUIRED and ordinary evidence is insufficient.

Pass condition:

- no full runtime target path is synthesized solely from prefab-relative names;
- the evidence record states how current target identity was resolved;
- stale runtime instance ids/paths are not persisted as canonical truth.

## Acceptance criteria for Runtime Binding v1 and later expansions

A Runtime Binding mapping is ready to promote when:

- the transport and concrete operation names were verified from the actual environment;
- only demonstrated Capability ids are mapped;
- semantic Capability ids remain unchanged;
- current availability is resolved at runtime rather than inferred from committed config;
- acquired/unavailable/denied/transport_error remain distinct;
- permissions remain under Runtime/Harness control;
- no secrets or ephemeral machine state are committed;
- path-addressed runtime operations obey current target-resolution discipline;
- the exercised vertical slice produces traceable evidence and a correct `verify-change` verdict.

Apply these criteria to every candidate or expanded binding scope. Until they are met for a Capability, keep that Capability unbound. The current active `unity-client` scope remains limited to the three proven mappings above until M3 or a later milestone proves an expansion.
