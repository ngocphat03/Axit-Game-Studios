# Unity Evidence Capability Validation

Status: rerun-cases-1-2

This checklist validates the first Axit Capability set without binding it to a specific transport.

The goal is to prove four boundaries:

1. Codex selects the **semantic evidence capability** that matches the accepted criterion;
2. Codex uses only capability ids explicitly declared by the active capability set;
3. lack of a runtime binding is reported as evidence availability, not confused with product failure;
4. `verify-change` keeps ownership of REQUIRED/SUPPORTING classification and PASS/FAIL/BLOCKED.

## Test protocol

- Start Codex from repository root.
- Use the current `unity-client` System.
- Do not tell Codex a concrete MCP/CLI tool name.
- Do not install or invent a transport just to make a case pass.
- Do not invent, alias, rename, or synthesize capability ids.
- Ordinary project evidence may be described without a capability id when no declared Capability represents that mechanism.
- Record capability-selection mistakes before changing instructions.

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

Do not run tools and do not modify anything. Classify which Unity evidence would be required versus only supporting.
```

Expected behavior:

- resolves `unity-client`;
- may read `.axit/systems/unity-client/capabilities.yaml` and `.axit/capabilities/unity/evidence.yaml`;
- recognizes the standalone focused deterministic C# tests as the primary/required **project validation evidence** for the arithmetic criterion;
- does not relabel standalone deterministic tests as `unity.tests.run` unless they are actually a Unity test suite covered by that declared Capability;
- for this criterion, no Unity Capability needs to be REQUIRED merely because the catalog exists;
- may classify `unity.compile` or `unity.playmode.verify` as SUPPORTING only when it explains why the additional evidence is useful to the accepted scope;
- may mention source/test inspection as ordinary project evidence, but must not invent capability ids for those activities;
- does not invent a concrete MCP command.

Failure signals:

- every declared capability becomes mandatory;
- unavailable Play Mode automatically means BLOCKED;
- calls standalone deterministic tests `unity.tests.run` without evidence they are a Unity test suite;
- invents ids such as `test.execute`, `source.inspect`, `damage.tests.run`, or another undeclared Capability;
- Codex claims a Unity runtime observation occurred when none was run.

## Case 2 — serialized prefab criterion selects inspection capability

Prompt:

```text
Using Axit routing, plan evidence for this criterion only:

The Player prefab must have DamageableBodyPart configured with the accepted serialized references and values.

Do not run tools and do not modify anything. Name the narrowest semantic Axit capabilities you would request and state what they can and cannot prove.
```

Expected capability selection uses only declared ids:

- `unity.prefab.inspect` — inspect the concrete Player prefab hierarchy/configuration;
- `unity.component.inspect` — when component presence needs a distinct observation;
- `unity.serialized-fields.inspect` — inspect exact serialized values/references.

Expected boundary:

- these capabilities can prove serialized prefab configuration on the inspected target;
- they do not prove runtime behavior after instantiation;
- `unity.playmode.verify` is not required unless the accepted criterion includes runtime behavior;
- resolving which asset is the Player prefab may use existing System/source/registry evidence and does not justify inventing a `resolve_asset` Capability;
- comparison against the accepted configuration is verifier reasoning over criterion + acquired evidence and does not justify inventing a `compare_to_contract` Capability;
- if the Player prefab target or accepted configuration cannot be resolved from current evidence, report that specific target/criterion gap rather than inventing a Capability.

Explicit failure examples:

```text
player_prefab.resolve_asset
prefab.inspect_serialized_component
configuration.compare_to_accepted_contract
```

These are invalid because they are not declared in the active Unity evidence set.

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

## First live pass — 2026-08-09

Observed results before the declared-ID fix:

- Case 1: reasoning mostly correct, but ordinary deterministic/source evidence was not clearly separated from Axit Capability ids.
- Case 2: failed ID discipline by inventing `player_prefab.resolve_asset`, `prefab.inspect_serialized_component`, and `configuration.compare_to_accepted_contract` instead of using the declared Unity capabilities.
- Case 3: PASS — selected `unity.playmode.verify` as REQUIRED and returned BLOCKED because the binding was unbound.
- Case 4: PASS — distinguished observed compiler failure from unavailable acquisition.
- Case 5: PASS — preserved permission and verdict boundaries.

The Capability Spec and routing instructions were then tightened. Re-run Cases 1 and 2 before promoting Capability v1.

## Acceptance criteria for Capability v1 routing

The semantic layer is acceptable when live Codex use demonstrates:

- capability selection is criterion-driven rather than catalog-driven;
- every named capability id is explicitly declared in the active capability set;
- missing semantic operations become ordinary evidence needs/capability gaps rather than fabricated ids;
- capability ids remain transport-neutral;
- evidence acquisition and verdict semantics remain separate;
- unbound/unavailable acquisition is not confused with demonstrated product failure;
- static/serialized/runtime capabilities are not allowed to prove more than they actually observe;
- ordinary project validation evidence is not mislabeled as a Capability;
- no Core Skill, Workflow, or Profile must change merely to add the Unity evidence set.

After Cases 1 and 2 pass the rerun, the next phase is to bind a small subset of Unity capabilities to one real transport and run a live vertical slice. Do not bind all capabilities at once.
