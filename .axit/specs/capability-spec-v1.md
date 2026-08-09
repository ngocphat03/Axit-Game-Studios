# Axit Capability Spec v1

## Purpose

A Capability is a **semantic operation that can acquire evidence or perform a controlled execution step** for Axit.

Capabilities answer:

> What can the runtime observe or execute to obtain evidence?

They do not answer:

- who owns a decision -> Profile;
- what procedure to follow -> Skill;
- how multiple procedures compose -> Workflow;
- whether evidence is sufficient -> `verify-change`;
- which provider/tool transports the operation -> runtime binding;
- whether an operation is authorized -> Runtime/Harness policy.

The separation is intentional:

```text
Skill / Workflow
    -> asks for evidence
        -> semantic Capability
            -> runtime binding
                -> MCP / CLI / Axit host / editor automation / other transport
                    -> evidence
                        -> verify-change judgment
```

A Capability id must remain stable even when the underlying transport changes.

## Canonical capability sets

Reusable semantic capability sets live under:

```text
.axit/capabilities/<domain>/<set>.yaml
```

Example:

```text
.axit/capabilities/unity/evidence.yaml
```

A capability set groups operations that share a domain and purpose. It is not a tool configuration file.

Recommended top-level shape:

```yaml
spec_version: axit.capability-set/v1
id: unity-evidence
domain: unity
purpose: evidence-acquisition
status: validation

capabilities:
  - id: unity.compile
    summary: Observe whether Unity scripts compile in the current configured environment.
    operation: execute
    side_effects: derived-artifacts
    evidence:
      can_establish: []
      cannot_establish: []
```

Only fields exercised by a real capability set should be added to v1.

## Capability identity

Capability ids describe **semantic intent**, not tool syntax.

Good:

```text
unity.compile
unity.prefab.inspect
unity.playmode.verify
```

Avoid:

```text
mcp.unity.execute_menu_item
unity-cli.batchmode-command
coplay.get_prefab
```

Provider/tool names belong in runtime bindings, not canonical capability ids.

## Operation classes

v1 uses two operation classes:

- `inspect` — observe existing source/editor/runtime state without intentionally starting a behavioral execution;
- `execute` — run a bounded check or controlled runtime action to produce evidence.

This describes intent only. It does not grant permission.

## Side-effect classes

A capability declares the smallest expected side-effect class:

- `read-only` — inspect state without intentionally changing project/runtime state;
- `derived-artifacts` — may create caches, logs, test/build outputs, or other reproducible derived files;
- `controlled-runtime` — may enter/drive a bounded runtime/editor state such as Play Mode and then return evidence.

A future mutation capability may require additional classes, but evidence v1 does not introduce source-writing capability semantics.

Runtime/Harness policy remains authoritative. Declaring a Capability never bypasses approval, path, command, or environment restrictions.

## Evidence contract

Each capability should state:

```yaml
evidence:
  can_establish:
    - ...
  cannot_establish:
    - ...
```

`can_establish` describes observations the capability can legitimately contribute when successfully acquired.

`cannot_establish` prevents overclaiming. For example, successful compilation does not prove gameplay behavior, and prefab inspection does not prove the runtime instantiated object behaves correctly.

Do not turn capability documentation into acceptance criteria. The accepted task and project rules still define what must be proven.

## Evidence vs verdict

Capability output is **evidence**, never the final verdict.

`verify-change` still decides:

- which criterion matters;
- which evidence is `REQUIRED` or `SUPPORTING`;
- whether the current evidence proves, fails, or leaves the criterion unresolved;
- the final `PASS`, `FAIL`, or `BLOCKED` verdict.

Examples:

```text
Criterion: scripts must compile
unity.compile observes compile errors
=> evidence can demonstrate FAIL
```

```text
Criterion: deterministic damage arithmetic is correct
focused deterministic tests already prove it
unity.playmode.verify unavailable
=> runtime evidence may remain SUPPORTING rather than blocking PASS
```

```text
Criterion: feature works in Unity Play Mode
unity.playmode.verify unavailable
=> REQUIRED evidence unavailable -> BLOCKED unless another required failure is already demonstrated
```

Do not make a Capability globally `REQUIRED` merely because it exists.

## Acquisition failure vs product failure

Keep these distinct:

1. **Evidence demonstrates target failure** — for example the Unity compiler ran and reported a compile error in the affected code. This may prove a required criterion `FAILED`.
2. **Capability is unavailable** — no usable runtime binding, required editor instance, environment, permission, or dependency exists. This is missing evidence, not proof the product is broken.
3. **Acquisition mechanism errors** — the binding/tool failed before a trustworthy target observation was produced. Treat this as an evidence acquisition problem unless the output itself proves a product failure.

The runtime binding should preserve enough detail for the verifier to distinguish these cases.

## Runtime binding separation

Canonical Capability definitions do not contain provider/tool calls.

A runtime binding maps:

```text
semantic capability id
    -> available execution transport
```

Possible transports include:

- Unity MCP;
- Axit Unity host;
- Unity CLI/batch mode;
- local test/build commands;
- editor automation;
- future remote execution providers.

Bindings may differ by developer machine or runtime environment without changing Skills, Workflows, Profiles, or capability ids.

Do not materialize a binding catalog before a real transport is connected and tested.

## System capability routing

A System may declare which semantic capability sets apply through the optional sidecar:

```text
.axit/systems/<system-id>/capabilities.yaml
```

Minimal shape:

```yaml
spec_version: axit.system-capabilities/v1
system: unity-client
capability_sets:
  - .axit/capabilities/unity/evidence.yaml
```

The sidecar means the semantic capability set is relevant to the System. It does **not** mean every capability is currently executable.

Availability depends on runtime bindings and environment state.

Root routing should read this sidecar only when evidence acquisition for that System is relevant. Do not preload capability catalogs for ordinary design or architecture-only work.

## Capability selection

When a Skill/Workflow needs evidence:

1. identify the accepted criterion;
2. choose the smallest evidence type that can establish it;
3. inspect the affected System's declared capability sets when execution/editor evidence is relevant;
4. choose the narrowest semantic Capability that can acquire the evidence;
5. let runtime/Harness resolve availability, permissions, and transport binding;
6. return acquired evidence to the owning Skill/Workflow;
7. keep verdict semantics in `verify-change`.

Do not call broader runtime checks when narrower deterministic evidence is sufficient.

## Scope ownership

Reusable engine/domain capabilities belong under `.axit/capabilities/`.

System-local facts such as project-specific scene names, prefab paths, test suites, or conventions belong in System Rules/Architecture/Knowledge or source/tests.

Example:

```text
unity.prefab.inspect
    = reusable Capability

"Player.prefab must have DamageableBodyPart"
    = project/System criterion or architecture fact
```

Do not create one capability id per game feature.

## Capability quality test

Before accepting a capability into a reusable set, ask:

1. Is this an observable/executable semantic operation rather than a procedure or responsibility?
2. Can its id survive a change from MCP to another transport?
3. Does it produce evidence that a verifier can interpret without granting it verdict authority?
4. Are its side effects clear enough for Runtime/Harness policy?
5. Does `cannot_establish` prevent common overclaims?
6. Is it reusable across materially different projects in the same domain?
7. Can system/project specifics stay outside the capability definition?

If not, keep the behavior in the owning Skill, System context, runtime binding, or tool layer instead.
