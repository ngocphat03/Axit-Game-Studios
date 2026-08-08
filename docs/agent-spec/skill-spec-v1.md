# Skill Spec v1

## Purpose

A Skill describes a reusable unit of AI-assisted work. It is a provider-neutral **execution contract plus expert guidance**, not merely a prompt or slash command.

A runtime should be able to inspect the structured fields before giving the skill to a model.

## Core shape

```yaml
spec_version: axit.skill/v1
id: dev-story
name: Develop Story
description: Implement one ready development story and produce verifiable evidence.

execution:
  side_effects: project-write
  preferred_profile: gameplay-programmer

inputs:
  - name: story_path
    type: path
    required: true

preconditions:
  - id: story-ready
    check: story.status == ready
    on_failure: block

context:
  required:
    - source: input
      ref: story_path
    - source: project
      ref: architecture.governing_adr
  optional: []

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
  tool_use: required

procedure:
  - id: inspect
    intent: Understand the story, constraints, dependencies, and acceptance criteria.
  - id: implement
    intent: Implement only the approved story scope.
  - id: verify
    intent: Produce evidence for every machine-verifiable acceptance criterion.

outputs:
  - id: implementation
    type: change_set
    required: true
  - id: summary
    type: report
    required: true

verdicts:
  - completed
  - blocked

verification:
  required:
    - id: acceptance-criteria
      type: criteria_coverage
    - id: tests
      type: tests_pass
    - id: scope
      type: change_scope

state_transitions:
  - id: mark-complete
    when: verdict == completed
    target: story.status
    from: [ready, in_progress]
    to: complete

failure:
  retry:
    max_attempts: 1
  on_blocked: escalate
  escalation_target: technical-lead
```

Not every Skill needs every optional section. A read-only review skill, for example, may omit `state_transitions` entirely.

A Markdown body may follow the structured header and contain domain guidance, examples, heuristics, trade-offs, and known failure modes.

## Field semantics

### `spec_version`

Required. Must be `axit.skill/v1` for this version.

### `id`

Required. Stable, lowercase kebab-case identifier. Renaming an ID is a migration event.

### `name` and `description`

Human-readable metadata. `description` should state the outcome, not implementation details tied to one provider.

### `execution`

Optional execution metadata that affects routing and safety.

```yaml
execution:
  side_effects: none | project-write | external
  preferred_profile: lead-programmer
```

`side_effects` means:

- `none` — read/analyze/report only; no project mutation;
- `project-write` — may change project/workspace state;
- `external` — may affect external systems such as remote Git, deployment, publishing, or other services.

A runtime may always impose stricter policy than the Skill requests.

`preferred_profile` identifies the canonical role that should own execution when available. It is routing guidance, not a requirement for a separate model or subagent.

### `inputs`

Explicit values required to invoke the skill. Inputs should be small references such as paths, IDs, feature names, scopes, modes, or options. Do not serialize whole project documents into input values.

Recommended input types for v1:

- `string`
- `path`
- `enum`
- `boolean`
- `number`
- `id`

Modes such as `single | sprint | all` or `full | lean | solo` should be represented as normal enum inputs/config values rather than provider-specific command flags in canonical semantics.

### `preconditions`

Conditions that must be satisfied before execution. Each precondition declares behavior on failure:

- `block` — do not execute;
- `warn` — continue but surface the risk;
- `ask` — require user decision;
- `skip` — this skill is not applicable.

Preconditions describe semantics. A provider adapter may implement them through tools, prompts, or runtime checks.

### `context`

Declares information that must or may be loaded.

Context references should point to sources rather than embedding content:

```yaml
context:
  required:
    - source: input
      ref: story_path
    - source: project
      ref: rules.coding
    - source: project
      ref: architecture.governing_adr
```

The goal is to make context selection explicit and auditable while allowing each runtime to optimize retrieval.

### `capabilities`

Semantic runtime capabilities required by the skill. Never use provider tool names here when a neutral capability exists.

Good:

```yaml
capabilities:
  required:
    - file.read
    - code.edit
    - test.run
    - user.approval
```

Avoid:

```yaml
allowed-tools: Read, Write, Bash, Task, AskUserQuestion
```

Capability naming and compatibility rules are defined in `capability-spec-v1.md`.

### `model_requirements`

Describes model capabilities, not model identity.

Initial dimensions:

```yaml
model_requirements:
  reasoning: low | medium | high
  coding: none | low | medium | high
  long_context: none | preferred | required
  vision: none | preferred | required
  tool_use: none | preferred | required
```

Provider adapters map these requirements to concrete models.

### `procedure`

Ordered semantic stages of work. Each stage should express intent and invariants, not provider mechanics.

Good:

```yaml
- id: inspect
  intent: Load the story and governing constraints before implementation.
```

Avoid:

```yaml
- Call Task with subagent_type=gameplay-programmer
```

Provider-specific orchestration belongs in adapters or runtime execution logic.

Parallelism is an execution optimization. Canonical procedure should state independence/dependency when it matters to correctness, but should not require one provider's parallel-task primitive.

### `outputs`

Declares artifacts produced by execution. Suggested v1 output types:

- `report`
- `document`
- `change_set`
- `artifact`
- `decision`
- `evidence`

### `verdicts`

Optional finite set of semantic outcomes produced by a review/gate/completion Skill.

Examples:

```yaml
verdicts:
  - ready
  - needs_work
  - blocked
```

or:

```yaml
verdicts:
  - approved
  - approved_with_suggestions
  - changes_required
```

Verdicts are part of the canonical contract and can drive later workflow/state transitions. Provider-specific wording should be normalized by adapters when necessary.

### `verification`

Declares the evidence required before the skill can report success or a completion verdict.

Verification types are semantic and may be implemented differently by runtimes:

- `tests_pass`
- `build_pass`
- `criteria_coverage`
- `change_scope`
- `artifact_exists`
- `manual_confirmation`
- `review_verdict`
- `unity_console_clean`

Conditional verification is allowed when evidence depends on story/task type:

```yaml
verification:
  conditional:
    - when: story.type == logic
      check:
        id: unit-tests
        type: tests_pass
```

If a deterministic verifier exists, it takes precedence over model self-assessment.

### `state_transitions`

Optional declarative state changes that may occur only after the Skill reaches the required verdict/verification state.

```yaml
state_transitions:
  - id: mark-complete
    when: verdict == completed
    target: story.status
    from: [ready, in_progress]
    to: complete
```

State transitions describe intended semantics. The runtime or adapter performs the actual mutation and must still apply project policy, approval, and side-effect controls.

Do not hide critical state mutations only in Markdown prose.

### `failure`

Defines bounded retry, blocked behavior, and escalation.

Retries must be finite. A skill should never encode an unbounded autonomous loop.

## Guidance body

The Markdown body after metadata may include:

- domain-specific implementation rules;
- examples;
- quality heuristics;
- design trade-offs;
- debugging playbooks;
- common mistakes;
- references to project knowledge.

The body should not contain security assumptions that only work if the model obeys them.

## Provider-neutrality rules

A canonical Skill MUST NOT require:

- a named provider;
- a named provider model;
- a provider-specific subagent primitive;
- a provider-specific approval API;
- a provider-specific hook system;
- a provider-specific command syntax.

When such behavior is needed, express the semantic requirement through a capability.

## Completion rule

A runtime may report a Skill as completed only when:

1. all blocking preconditions were satisfied or explicitly overridden by an allowed user decision;
2. all required outputs exist;
3. all required verification checks pass or are explicitly marked manual and confirmed;
4. any declared state transition preconditions are satisfied before mutation;
5. no unresolved blocking failure remains.

The model's textual statement that work is complete is not evidence by itself.