# Skill Spec v1

## Purpose

A Skill is a provider-neutral **execution contract plus expert guidance** for one reusable unit of AI-assisted work. Runtime-significant behavior must be structural; provider mechanics belong in adapters.

## Core shape

```yaml
spec_version: axit.skill/v1
id: example-skill
name: Example Skill
description: Describe the outcome, not a provider command.

execution:
  side_effects: project-write
  preferred_profile: gameplay-programmer
  interaction: collaborative

inputs: []
preconditions: []
context:
  required: []
  optional: []
capabilities:
  required: []
  optional: []
model_requirements: {}
procedure: []
outputs: []
verdicts: []
verification: {}
state_transitions: []
failure: {}
```

Not every Skill needs every optional section.

## `execution`

```yaml
execution:
  side_effects: none | project-write | external
  preferred_profile: lead-programmer
  interaction: autonomous | collaborative | approval-gated
```

- `none`: analyze/report only; no project mutation.
- `project-write`: may mutate workspace/project state.
- `external`: may affect remote Git, deployment, publishing, or another external system.
- `preferred_profile`: semantic owner of the work; does not require a separate model/subagent.
- `interaction`: expected human collaboration style.

Some skills have **mode-dependent side effects**. Use rules instead of duplicating the skill:

```yaml
execution:
  side_effects: project-write
  side_effect_rules:
    - when: mode == report
      side_effects: none
```

Runtime policy can always be stricter.

## `inputs`

Inputs are small references/options, not serialized project documents.

Supported v1 types:

- `string`
- `path`
- `enum`
- `boolean`
- `number`
- `id`

Modes such as `single|sprint|all`, `full|lean|solo`, or `update|audit|report` are normal enum inputs/configuration, not provider-specific flags.

## `preconditions`

```yaml
preconditions:
  - id: story-ready
    check: story.status == ready
    on_failure: block
```

Allowed failure behavior:

- `block`
- `warn`
- `ask`
- `skip`

## `context`

Declare what must/may be loaded, by reference:

```yaml
context:
  required:
    - source: input
      ref: story_path
    - source: project
      ref: architecture.governing_adr
  optional:
    - source: project
      ref: session.active_state
```

Do not embed large source documents into invocation inputs when a reference can be resolved by the runtime.

## `capabilities`

Use semantic capabilities, not provider tools:

```yaml
capabilities:
  required:
    - file.read
    - code.edit
    - test.run
    - user.approval
  optional:
    - agent.consult
```

Avoid canonical metadata such as `Read`, `Write`, `Bash`, `Task`, or `AskUserQuestion`.

## `model_requirements`

Describe capability, not model identity:

```yaml
model_requirements:
  reasoning: low | medium | high
  coding: none | low | medium | high
  long_context: none | preferred | required
  vision: none | preferred | required
  tool_use: none | preferred | required
```

Adapters/runtime map this to concrete models.

## `procedure`

Procedure stages express semantic intent:

```yaml
procedure:
  - id: inspect
    intent: Load requirements and constraints before implementation.
  - id: implement
    intent: Implement only approved scope.
```

Provider-specific calls do not belong here.

### Decision and approval checkpoints

Interactive design work proved that canonical skills need explicit collaboration checkpoints. A procedure step may declare:

```yaml
- id: approve-section
  intent: Confirm the drafted section before persistence.
  interaction:
    type: approval
    required: true
    subject: current_section
```

Supported v1 interaction types:

- `question`: collect missing information;
- `choice`: select among bounded alternatives;
- `decision`: record a substantive project/product decision;
- `approval`: authorize a scoped mutation;
- `confirmation`: verify a manual observation/evidence item.

Adapters may render these as widgets, prompts, CLI input, or another UI. Canonical semantics must not depend on one interaction API.

### Checkpoints

Long collaborative skills may persist resumable progress:

```yaml
- id: checkpoint-section
  intent: Persist the approved section and current progress marker.
  checkpoint:
    required: true
    state_ref: session.active_state
```

Checkpoint state is compact working state, not conversation history.

## `outputs`

Suggested output types:

- `report`
- `document`
- `change_set`
- `artifact`
- `decision`
- `evidence`

## `verdicts`

Finite semantic outcomes used by reviews/gates/workflows:

```yaml
verdicts:
  - approved
  - needs_revision
  - blocked
```

Downstream behavior should consume verdict IDs rather than parse prose.

## `verification`

Verification states required evidence and may be conditional:

```yaml
verification:
  required:
    - id: scope
      type: change_scope
  conditional:
    - when: story.type == logic
      check:
        id: tests
        type: tests_pass
```

Common v1 verification types:

- `tests_pass`
- `build_pass`
- `criteria_coverage`
- `change_scope`
- `artifact_exists`
- `manual_confirmation`
- `review_verdict`
- `unity_console_clean`

Deterministic verification takes precedence over model self-assessment.

## `state_transitions`

Critical state mutation must be declarative:

```yaml
state_transitions:
  - id: mark-complete
    when: verdict == complete and user.approval == granted
    target: story.status
    from: [ready, in_progress]
    to: complete
```

The runtime/adapter performs the mutation and still applies policy/approval controls.

## `failure`

```yaml
failure:
  retry:
    max_attempts: 1
  on_blocked: report | escalate
  escalation_target: technical-lead
```

Retries are always bounded.

## Guidance body

Markdown guidance may contain:

- domain rules;
- examples and heuristics;
- trade-offs;
- debugging playbooks;
- known failure modes;
- references to project knowledge.

Guidance must not pretend to be a security boundary.

## Provider-neutrality rules

A canonical Skill MUST NOT require:

- a named provider or model;
- a provider-specific subagent primitive;
- a provider-specific approval/question API;
- a provider-specific hook system;
- provider-specific command syntax when a semantic capability exists.

## Completion rule

A Skill may reach a successful completion verdict only when:

1. blocking preconditions are satisfied or validly overridden;
2. required outputs exist;
3. required verification passes or required manual confirmation is recorded;
4. required approvals occur before mutations;
5. declared state-transition conditions are satisfied;
6. no unresolved blocking failure remains.

A model saying "done" is not evidence by itself.