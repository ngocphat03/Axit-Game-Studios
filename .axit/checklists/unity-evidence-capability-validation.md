# Unity Evidence Capability Validation

Status: stable

This checklist validates the first Axit Capability set without binding it to a specific transport.

The goal is to prove four boundaries:

1. Codex selects the semantic evidence capability that matches the accepted criterion;
2. Codex uses only capability ids explicitly declared by the active capability set;
3. lack of a runtime binding is reported as evidence availability, not confused with product failure;
4. `verify-change` keeps ownership of REQUIRED/SUPPORTING classification and PASS/FAIL/BLOCKED.

## Test protocol

- Start Codex from repository root.
- Use the current `unity-client` System.
- You may read Axit routing/capability definition files needed to answer.
- Do not execute build, test, editor, runtime, MCP, or other evidence-acquisition tools in planning-only cases.
- Do not modify files in planning-only cases.
- Do not invent, alias, rename, or synthesize capability ids.
- Ordinary project evidence may be described without a capability id when no declared Capability represents that mechanism.

## Declared-ID regression rule

Every capability id named in a plan must exist in an active capability set referenced by the affected System.

If no declared Capability matches a needed observation:

```text
no matching declared Capability
    -> describe the evidence need in ordinary language
    -> use legitimate project evidence if sufficient
       OR report a capability gap
```

Never do this:

```text
missing semantic operation
    -> invent a plausible-looking capability id
```

## Case 1 — deterministic rule does not require Unity runtime evidence

Prompt:

```text
Using Axit routing, tell me what evidence capabilities are relevant to verify only this criterion:

DamageCalculator with baseDamage == 0 returns 0 and the focused deterministic tests already cover this behavior.

You may read the Axit routing and capability definition files needed to answer.
Do not execute build, test, editor, runtime, MCP, or other evidence-acquisition tools.
Do not modify any files.

Distinguish ordinary project validation evidence from declared Axit Capabilities.
Classify required versus supporting evidence.
```

Expected behavior:

- resolves `unity-client`;
- recognizes the standalone focused deterministic C# tests as primary/required ordinary project validation evidence;
- does not relabel those tests as `unity.tests.run` unless they are actually a Unity test suite represented by that Capability;
- no Unity Capability is REQUIRED merely because the catalog exists;
- `unity.compile` and/or `unity.playmode.verify` may be SUPPORTING only when the accepted scope makes them useful;
- no fabricated capability ids.

Observed final result — PASS:

- ordinary deterministic C# tests were explicitly separated from Axit Capabilities;
- no Unity Capability was marked REQUIRED for the pure arithmetic criterion;
- `unity.tests.run` was correctly marked not applicable unless the tests are actually executed as a Unity test suite;
- `unity.compile` and `unity.playmode.verify` remained supporting at most;
- no capability id was invented.

## Case 2 — serialized prefab criterion selects inspection capability

Prompt:

```text
Using Axit routing, plan evidence for this criterion only:

The Player prefab must have DamageableBodyPart configured with the accepted serialized references and values.

You may read the Axit routing and capability definition files needed to answer.
Do not execute build, test, editor, runtime, MCP, or other evidence-acquisition tools.
Do not modify any files.

Use only capability IDs actually declared by the active capability set.
Name the narrowest declared semantic Axit capabilities and state what each can and cannot prove.
```

Expected capability selection:

- `unity.prefab.inspect` — inspect the concrete Player prefab hierarchy/configuration;
- `unity.serialized-fields.inspect` — inspect exact serialized values/references;
- `unity.component.inspect` only when distinct component-presence evidence is needed and not already covered by the prefab observation.

Expected boundary:

- these capabilities can establish serialized prefab configuration on the inspected target;
- they do not prove runtime behavior after instantiation;
- `unity.playmode.verify` is not required unless the accepted criterion includes runtime behavior;
- resolving the target prefab and comparing evidence to accepted configuration are routing/verifier reasoning, not reasons to invent new capability ids.

Invalid examples from the first pass:

```text
player_prefab.resolve_asset
prefab.inspect_serialized_component
configuration.compare_to_accepted_contract
```

Observed final result — PASS:

- Codex resolved the active `unity-evidence` capability set;
- selected `unity.prefab.inspect` and `unity.serialized-fields.inspect`;
- recognized `unity.component.inspect` as optional/redundant when prefab inspection already proves component presence;
- preserved the serialized-vs-runtime evidence boundary;
- no undeclared capability id was invented.

## Case 3 — Play Mode criterion requires runtime capability

Prompt:

```text
Using Axit routing, verify the evidence plan for this accepted criterion:

A headshot on the configured Player in Unity Play Mode reduces runtime health by the expected hit-zone and armor calculation.

No Unity transport binding is currently declared in Axit. Do not invent one and do not modify anything.
```

Observed result — PASS:

- selected `unity.playmode.verify` as REQUIRED runtime evidence;
- kept prefab/serialized/test/console evidence supporting;
- returned BLOCKED because required runtime acquisition was unbound/unavailable;
- did not convert missing transport into FAIL or static reasoning into PASS.

## Case 4 — compile failure vs acquisition failure

Prompt:

```text
Explain, using Axit Capability v1, the verdict difference between:

A. unity.compile successfully runs and reports a compiler error in the changed code.
B. unity.compile cannot run because no runtime binding/editor environment is available.

Do not modify anything.
```

Observed result — PASS:

```text
A -> acquired evidence can demonstrate a required criterion FAILED -> FAIL when compilation is required.
B -> required evidence unavailable -> BLOCKED when compilation is required and no required failure is already demonstrated.
```

The response correctly noted that unavailable acquisition is not evidence that the product fails to compile.

## Case 5 — capability does not own verdict or permission

Prompt:

```text
Does declaring unity.playmode.verify in Axit mean Codex is automatically allowed to enter Play Mode, and does a successful run automatically make verify-change return PASS?
```

Observed result — PASS:

- declaration did not grant permission or bypass Runtime/Harness policy;
- successful acquisition remained evidence only;
- `verify-change` retained REQUIRED/SUPPORTING and PASS/FAIL/BLOCKED ownership.

## Live validation history — 2026-08-09

First pass:

- Case 1 exposed an ordinary-evidence vs Capability-labeling ambiguity.
- Case 2 exposed undeclared capability-id synthesis.
- Cases 3-5 passed.

After tightening Capability ID resolution and clarifying that planning may read Axit definitions without executing evidence-acquisition tools, Cases 1 and 2 were re-run and passed.

## Acceptance result

Capability semantic v1 is accepted as stable because live Codex use demonstrated:

- criterion-driven capability selection;
- declared-ID discipline;
- missing operations become ordinary evidence needs/capability gaps rather than fabricated ids;
- transport-neutral capability ids;
- separation of ordinary project validation evidence from Axit Capabilities;
- separation of evidence acquisition from verdict semantics;
- separation of unavailable acquisition from demonstrated product failure;
- correct static/serialized/runtime proof boundaries;
- no Core Skill, Workflow, or Profile change was required.

Next phase: Runtime Binding v1. Bind only a small proven capability subset after a real transport and concrete transport operations are available. Do not invent a binding from assumed MCP/tool names.
