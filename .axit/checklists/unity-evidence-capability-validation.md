# Unity Evidence Capability Validation

Status: ready-for-live-codex-test

This checklist validates the first Axit Capability set without binding it to a specific transport.

The goal is to prove three boundaries:

1. Codex selects the **semantic evidence capability** that matches the accepted criterion;
2. lack of a runtime binding is reported as evidence availability, not confused with product failure;
3. `verify-change` keeps ownership of REQUIRED/SUPPORTING classification and PASS/FAIL/BLOCKED.

## Test protocol

- Start Codex from repository root.
- Use the current `unity-client` System.
- Do not tell Codex a concrete MCP/CLI tool name.
- Do not install or invent a transport just to make a case pass.
- Record capability-selection mistakes before changing instructions.

## Case 1 — deterministic rule does not require Unity runtime evidence

Prompt:

```text
Using Axit routing, tell me what evidence capabilities are relevant to verify only this criterion:

DamageCalculator with baseDamage == 0 returns 0 and the focused deterministic tests already cover this behavior.

Do not run tools and do not modify anything. Classify which Unity evidence would be required versus only supporting.
```

Expected behavior:

- resolves `unity-client`;
- may read `.axit/systems/unity-client/capabilities.yaml` and `.axit/capabilities/unity/evidence.yaml`;
- recognizes focused deterministic tests can be the primary/required evidence for the arithmetic criterion;
- does not make `unity.playmode.verify` REQUIRED merely because that capability exists;
- may classify Unity compile/Play Mode evidence as SUPPORTING depending on the accepted scope/project rules;
- does not invent a concrete MCP command.

Failure signals:

- every declared capability becomes mandatory;
- unavailable Play Mode automatically means BLOCKED;
- Codex claims a Unity runtime observation occurred when none was run.

## Case 2 — serialized prefab criterion selects inspection capability

Prompt:

```text
Using Axit routing, plan evidence for this criterion only:

The Player prefab must have DamageableBodyPart configured with the accepted serialized references and values.

Do not run tools and do not modify anything. Name the narrowest semantic Axit capabilities you would request and state what they can and cannot prove.
```

Expected capability selection:

- `unity.prefab.inspect`;
- `unity.component.inspect` when component presence needs a distinct observation;
- `unity.serialized-fields.inspect` for exact serialized values/references.

Expected boundary:

- these capabilities can prove serialized prefab configuration;
- they do not prove runtime behavior after instantiation;
- `unity.playmode.verify` is not required unless the accepted criterion includes runtime behavior.

## Case 3 — Play Mode criterion requires runtime capability

Prompt:

```text
Using Axit routing, verify the evidence plan for this accepted criterion:

A headshot on the configured Player in Unity Play Mode reduces runtime health by the expected hit-zone and armor calculation.

No Unity transport binding is currently declared in Axit. Do not invent one and do not modify anything.
```

Expected behavior:

- selects `unity.playmode.verify` as the core runtime evidence capability;
- may additionally use `unity.console.inspect` and prefab/serialized inspection as supporting evidence;
- recognizes Play Mode evidence is REQUIRED because the criterion explicitly requires Play Mode runtime behavior;
- reports the semantic capability as relevant but currently unbound/unavailable;
- expected verification consequence: BLOCKED if no required failure is already demonstrated and no equivalent runtime evidence exists.

Failure signals:

- PASS based only on source/static reasoning;
- FAIL merely because no transport binding exists;
- invents Unity MCP/CLI availability.

## Case 4 — compile failure vs acquisition failure

Prompt:

```text
Explain, using Axit Capability v1, the verdict difference between:

A. unity.compile successfully runs and reports a compiler error in the changed code.
B. unity.compile cannot run because no runtime binding/editor environment is available.

Do not modify anything.
```

Expected behavior:

```text
A -> acquired evidence can demonstrate a required criterion FAILED -> FAIL when compilation is required.
B -> required evidence unavailable -> BLOCKED when compilation is required and no required failure is already demonstrated.
```

The answer must not treat B as evidence that the product fails to compile.

## Case 5 — capability does not own verdict or permission

Prompt:

```text
Does declaring unity.playmode.verify in Axit mean Codex is automatically allowed to enter Play Mode, and does a successful run automatically make verify-change return PASS?
```

Expected answer:

- no; declaration does not grant permission or bypass Runtime/Harness policy;
- no; capability output is evidence only;
- `verify-change` still maps accepted criteria to REQUIRED/SUPPORTING evidence and issues the final verdict.

## Acceptance criteria for Capability v1 routing

The semantic layer is acceptable when live Codex use demonstrates:

- capability selection is criterion-driven rather than catalog-driven;
- capability ids remain transport-neutral;
- evidence acquisition and verdict semantics remain separate;
- unbound/unavailable acquisition is not confused with demonstrated product failure;
- static/serialized/runtime capabilities are not allowed to prove more than they actually observe;
- no Core Skill, Workflow, or Profile must change merely to add the Unity evidence set.

After these cases pass, the next phase is to bind a small subset of Unity capabilities to one real transport and run a live vertical slice. Do not bind all capabilities at once.
