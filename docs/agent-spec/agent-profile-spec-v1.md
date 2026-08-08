# Agent Profile Spec v1

## Purpose

An Agent Profile describes a specialized role that a runtime may assign to a model. It defines responsibility, boundaries, knowledge, skills, model requirements, and escalation behavior without binding the role to one provider or model.

An Agent Profile is **not** a long-lived autonomous worker by definition. A runtime may realize the profile as:

- the current model with a different context package;
- a subagent;
- a separate model call;
- a separate session;
- an Axit-Code runtime actor in the future.

## Required shape

```yaml
spec_version: axit.profile/v1
id: gameplay-programmer
name: Gameplay Programmer
description: Implements gameplay mechanics according to approved design and architecture constraints.

responsibilities:
  - implement-gameplay-features
  - preserve-design-intent
  - produce-testable-code

boundaries:
  must_not:
    - change-game-design-unilaterally
    - modify-networking-without-delegation
  path_scope:
    preferred:
      - src/gameplay/**
      - tests/gameplay/**

skills:
  primary:
    - dev-story
    - code-review
  supporting:
    - architecture-read
    - test-authoring

knowledge:
  required:
    - project.coding-standards
    - project.architecture
  optional:
    - project.gameplay-patterns

capabilities:
  required:
    - file.read
    - code.edit
    - test.run
  optional:
    - agent.delegate

model_requirements:
  reasoning: medium
  coding: high
  long_context: preferred
  tool_use: required

collaboration:
  reports_to: lead-programmer
  escalation:
    architecture: lead-programmer
    design: game-designer
    performance: technical-director
  consults:
    - ui-programmer
    - ai-programmer

verification:
  default:
    - tests_pass
    - change_scope
```

## Field semantics

### `spec_version`

Required. Must be `axit.profile/v1` for this version.

### `id`

Stable lowercase kebab-case identifier.

### `responsibilities`

A concise set of outcomes the role owns. Responsibilities should describe domain ownership rather than individual commands.

### `boundaries`

Defines what the role must not decide or modify without delegation or approval.

Profile boundaries are behavioral constraints in the spec layer. They do not replace runtime enforcement.

`path_scope` may express default/preferred scope for planning and routing, but hard path enforcement belongs to the active runtime/Harness.

### `skills`

Profiles reference canonical Skill IDs rather than embedding duplicate workflow instructions.

- `primary` — skills central to the role;
- `supporting` — skills the role may use when needed.

This prevents 49 role files from each carrying slightly different copies of the same implementation workflow.

### `knowledge`

References domain/project knowledge that should be included when the profile is active. Knowledge references must remain provider-neutral.

### `capabilities`

Declares semantic runtime capabilities this profile generally needs. A specific Skill may further narrow or extend the required set.

Effective capability requirements for an invocation are calculated from:

```text
Profile capabilities
+ Skill capabilities
+ Runtime policy
```

Runtime policy always has final authority.

### `model_requirements`

Profiles express capability needs rather than names like `sonnet`, `opus`, `gpt-*`, or `gemini-*`.

Adapters may choose different models for the same profile depending on availability, cost, task risk, and provider configuration.

### `collaboration`

Expresses semantic relationships used for routing and escalation:

- `reports_to`
- `escalation`
- `consults`

These relationships do not require multi-agent execution. A runtime may satisfy an escalation by asking the same model to switch profile, invoking another model, or requesting the user.

### `verification`

Default verification expectations for work performed under this profile. A Skill may add stronger requirements.

## Role hierarchy

Game Studios may retain a hierarchy such as:

```text
Director
  -> Department Lead
      -> Specialist
```

but hierarchy should be treated as a routing and responsibility model, not a mandatory process topology.

Simple work should not be forced through every tier. The runtime may route directly when the active Skill and project policy make the appropriate owner unambiguous.

## Profile composition

Prefer composition over duplicated profile prose.

For example, engine-specific expertise should be referenced through skills/knowledge/capabilities where possible instead of creating a completely independent copy of every programmer role per engine.

A future profile may support `extends`, but v1 intentionally avoids inheritance until migration shows a concrete need.

## Provider-neutrality rules

Canonical profiles MUST NOT contain:

- provider model IDs;
- provider-specific tool names;
- provider-specific subagent syntax;
- context-window numbers tied to one model family;
- provider permission/hook configuration.

Those values belong to runtime configuration or adapters.

## Security rule

A Profile can only request or narrow capabilities. It can never grant itself authority beyond runtime policy.

Future Axit-Code behavior should follow:

```text
Profile request
    -> Skill request
        -> Harness policy evaluation
            -> allowed / denied / approval required
```

The model or profile never becomes the security authority.