# Axit Runtime Bindings

Runtime Bindings map stable semantic Capability ids to verified concrete transport operations.

Canonical form:

```text
.axit/bindings/<system-id>/<binding-id>.yaml
```

This directory intentionally contains **no concrete binding yet**.

The current `unity-client` Capability set is semantically stable but remains unbound because this branch does not contain a verified repository-level Unity transport configuration or concrete transport operation catalog.

Rules:

- do not create a binding from assumed MCP/CLI/editor tool names;
- inspect the actual transport first;
- bind only Capability ids already declared by the affected System's active capability sets;
- transport/provider names belong here, not in canonical Capability ids;
- source-controlled binding definitions do not prove current runtime availability;
- keep credentials, tokens, machine-specific endpoints, ports, and other secrets outside `.axit`;
- Runtime/Harness policy still controls execution permission;
- acquisition results remain evidence, not PASS/FAIL/BLOCKED.

First intended Unity binding slice after real transport discovery:

```text
unity.prefab.inspect
unity.serialized-fields.inspect
unity.playmode.verify
```

See [`Runtime Binding Spec v1`](../specs/runtime-binding-spec-v1.md).
