# Axit Runtime Bindings

Runtime Bindings map stable semantic Capability ids to verified concrete transport operations.

Canonical form:

```text
.axit/bindings/<system-id>/<binding-id>.yaml
```

The current `unity-client` System references the reviewed active [`coplaydev-unity-mcp`](unity-client/coplaydev-unity-mcp.yaml) definition. It maps exactly:

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

`active` is the source-controlled binding lifecycle status. It does not claim that the transport is currently connected or available; runtime availability must still be resolved for each acquisition.

Rules:

- do not create a binding from assumed MCP/CLI/editor tool names;
- inspect the actual transport first;
- bind only Capability ids already declared by the affected System's active capability sets;
- transport/provider names belong here, not in canonical Capability ids;
- source-controlled binding definitions do not prove current runtime availability;
- keep credentials, tokens, machine-specific endpoints, ports, and other secrets outside `.axit`;
- Runtime/Harness policy still controls execution permission;
- acquisition results remain evidence, not PASS/FAIL/BLOCKED.

Current active Unity binding slice:

```text
unity.prefab.inspect
unity.serialized-fields.inspect
unity.playmode.verify
```

See [`Runtime Binding Spec v1`](../specs/runtime-binding-spec-v1.md).
