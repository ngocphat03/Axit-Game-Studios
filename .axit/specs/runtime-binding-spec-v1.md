# Axit Runtime Binding Spec v1

## Purpose

A Runtime Binding maps a stable semantic Axit Capability to one concrete execution transport.

It answers:

> How can this environment attempt to perform the semantic operation requested by Axit?

A Runtime Binding does **not** decide:

- which evidence is needed -> Skill / `verify-change`;
- whether evidence is REQUIRED or SUPPORTING -> `verify-change`;
- PASS / FAIL / BLOCKED -> `verify-change`;
- whether an operation is permitted -> Runtime/Harness policy;
- product intent or architecture -> owning Profile / Rules / Registry.

The intended relationship is:

```text
accepted criterion
  -> semantic Capability
      -> Runtime Binding
          -> concrete transport operation(s)
              -> acquisition result + evidence
                  -> verify-change judgment
```

## Canonical location

Reviewed binding definitions live under:

```text
.axit/bindings/<system-id>/<binding-id>.yaml
```

Do not create a binding file until a real transport and its concrete operations have been inspected or exercised.

A System may later reference one or more reviewed binding definitions from its capability sidecar. The current absence of a binding reference means the Capability is semantically known but unbound in repository configuration.

## Binding definition vs runtime availability

Keep these separate:

- **binding definition** — source-controlled mapping from semantic Capability ids to verified transport operations;
- **runtime availability** — whether the transport, editor/process, connection, dependency, and permission are usable now;
- **credentials/endpoints/secrets** — environment/runtime configuration outside canonical Axit files.

A committed binding with `status: active` means the mapping is reviewed. It does **not** mean the transport is connected on every developer machine.

Do not commit passwords, API keys, auth tokens, machine-specific socket paths, ephemeral ports, or other secrets into `.axit`.

## Minimal binding shape

Materialize only after a real transport is available:

```yaml
spec_version: axit.runtime-binding/v1
id: <binding-id>
system: <system-id>
status: validation

transport:
  family: <mcp|cli|editor-host|other>
  adapter: <verified transport/adapter identity>

mappings:
  - capability: unity.prefab.inspect
    operations:
      - <verified concrete transport operation>
```

Provider/tool names are allowed **inside Runtime Bindings** because this layer exists specifically to isolate transport details from canonical Capability ids.

Do not copy provider/tool names back into Capability, Skill, Workflow, Profile, or Registry ids.

## Mapping rules

1. Every `capability` in a binding must exist in an active Capability set for the affected System.
2. Never invent a transport operation name. Inspect the real transport interface first.
3. A semantic Capability may map to one or several ordered transport operations when the transport requires multiple calls to produce one trustworthy observation.
4. Map the smallest transport surface needed for the Capability.
5. Do not bind unrelated operations merely because the transport exposes them.
6. A binding must preserve the Capability's evidence boundary: it cannot claim more than `can_establish` allows.
7. A binding cannot weaken the Capability's declared side-effect class.
8. Evidence v1 bindings must not gain source-writing semantics merely because the transport can mutate project files.

## Runtime resolution

When a selected Capability needs acquisition:

1. resolve the affected System;
2. confirm the Capability id is declared by the System's active capability set;
3. resolve a reviewed binding that maps that Capability;
4. check current transport/environment availability;
5. check Runtime/Harness policy and approvals;
6. execute only the mapped operation(s) needed for the bounded evidence request;
7. normalize the acquisition result;
8. return evidence to the verifier without issuing a verification verdict.

Do not silently substitute an unregistered transport because it appears convenient. A fallback transport must have its own reviewed compatible binding or be explicitly approved as an ad-hoc runtime action outside canonical binding claims.

## Acquisition result classes

A binding/runtime must distinguish at least these outcomes:

### `acquired`

The transport produced a trustworthy observation of the requested target/scope.

The observation may demonstrate product success, product failure, or merely partial evidence. `acquired` is **not** PASS.

### `unavailable`

The semantic Capability has no usable binding or the required editor/process/environment/dependency is not currently available.

This is missing evidence, not product failure.

### `denied`

Runtime/Harness policy or required approval prevented the operation.

This is not product failure. Whether it blocks verification depends on whether the evidence was REQUIRED.

### `transport_error`

The transport/binding failed before producing a trustworthy target observation.

Treat this as acquisition failure unless the returned output itself contains reliable product evidence.

These acquisition classes are not verification verdicts.

## Evidence provenance

A successful binding should return enough information for the verifier to understand:

- semantic Capability id requested;
- concrete target/scope observed;
- transport/binding used;
- structured or raw observations relevant to the criterion;
- diagnostics/warnings that affect trust;
- whether the operation actually completed.

Do not require one universal evidence payload schema before a real transport demonstrates the useful shape. Preserve native structured evidence when possible rather than flattening everything into prose.

## Permission boundary

Binding resolution does not authorize execution.

Examples:

- `unity.prefab.inspect` is read-only semantic intent, but the transport call is still subject to Runtime/Harness access policy;
- `unity.playmode.verify` may enter controlled runtime state and may require an allowed editor/session state or approval;
- a transport that also exposes destructive editor/file operations does not make those operations part of the binding.

Runtime/Harness remains the trust boundary.

## Binding lifecycle

Use these definition statuses:

- `validation` — concrete mapping exists but has not passed live binding regression/vertical-slice evidence;
- `active` — mapping has passed live use for its declared capability scope;
- `deprecated` — kept only for migration/reference and should not be selected for new runs.

Do not use binding status to represent whether a local transport is currently online.

## First Unity binding scope

The first live Unity transport should intentionally bind only this small semantic subset:

```text
unity.prefab.inspect
unity.serialized-fields.inspect
unity.playmode.verify
```

Why this subset:

- prefab inspection proves serialized asset structure/configuration;
- serialized-field inspection proves exact configured values/references;
- Play Mode verification proves bounded runtime behavior;
- together with existing ordinary deterministic C# test evidence, they exercise distinct evidence layers without binding the entire Unity catalog.

`unity.component.inspect` may remain unbound initially because `unity.prefab.inspect` can already establish component presence on the concrete prefab for the first vertical slice.

Do not add compile, test-run, console, scene, or project-inspection mappings until a live task demonstrates they are needed for the binding slice.

## Transport discovery rule

Before creating the first Unity binding:

- inspect the actual local Codex/runtime transport configuration;
- identify the real Unity transport/adapter;
- inspect its concrete available operations and their input/output behavior;
- confirm the target Unity project/editor can be reached;
- then materialize only mappings that were actually verified.

If repository files do not identify a transport, keep the System `binding_status: unbound` and request/inspect local runtime configuration rather than guessing MCP names.

## Quality test

Before accepting a Runtime Binding, ask:

1. Does every mapped Capability id already exist in the active semantic set?
2. Are concrete transport operation names verified rather than guessed?
3. Does the mapping preserve the Capability's side-effect and evidence boundaries?
4. Are availability and credentials kept out of canonical semantic definitions?
5. Can the runtime distinguish acquired evidence from unavailable, denied, and transport-error acquisition?
6. Does policy still control whether the operation may execute?
7. Is the binding limited to the smallest proven subset?
8. Could the transport be replaced later without changing Capability/Skill/Workflow/Profile ids?

If not, keep the capability unbound.
