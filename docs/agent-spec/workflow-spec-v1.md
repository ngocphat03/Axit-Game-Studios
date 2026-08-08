# Workflow Spec v1

## Purpose

A Workflow composes canonical Skills into a provider-neutral process. It defines **ordering, dependencies, repeatability, gates, and completion evidence**.

A Workflow does not duplicate Skill procedure, prompts, tool names, or provider orchestration primitives.

```text
Workflow
  -> Skill
      -> Profile / Capability requirements
          -> Adapter or Axit-Code Runtime
```

## Core shape

```yaml
spec_version: axit.workflow/v1
id: story-delivery
name: Story Delivery
description: Take one implementation-ready story through implementation, review, and verified closure.

inputs:
  - name: story_path
    type: path
    required: true

steps:
  readiness:
    skill: story-readiness
    required: true
    inputs:
      scope: single
      story_path: $workflow.story_path
    completion:
      accepted_verdicts: [ready]
      on_unaccepted: stop

  implement:
    skill: dev-story
    required: true
    depends_on: [readiness]
    inputs:
      story_path: $workflow.story_path
    completion:
      accepted_verdicts: [implemented]
      on_unaccepted: block

  review:
    skill: code-review
    required: true
    depends_on: [implement]
    inputs:
      story_path: $workflow.story_path
      targets: $implement.outputs.implementation
    completion:
      accepted_verdicts: [approved, approved_with_suggestions]
      on_unaccepted: stop

  close:
    skill: story-done
    required: true
    depends_on: [review]
    inputs:
      story_path: $workflow.story_path
    completion:
      accepted_verdicts: [complete, complete_with_notes]
      artifacts:
        - ref: story.status
          equals: complete
      on_unaccepted: block

completion:
  strategy: all_required_steps

verdicts:
  - completed
  - paused
  - blocked
```

## Design rules

### 1. Workflow is composition only

Skill owns:

- context requirements;
- capabilities;
- model requirements;
- interaction checkpoints;
- procedure;
- outputs;
- verification;
- state transitions.

Workflow owns:

- which Skills participate;
- step dependencies/order;
- conditional execution;
- repeatability;
- what result permits progression;
- process-level completion.

Do not copy Skill instructions into Workflow files.

### 2. Steps reference canonical Skill IDs

```yaml
steps:
  review:
    skill: code-review
```

Provider-specific slash commands such as `/code-review`, Claude Task calls, or concrete model names do not belong in canonical workflows.

## Inputs and references

Workflow inputs use the same primitive input types as Skill Spec v1.

Step input mappings may reference:

- `$workflow.<input>` — workflow invocation input;
- `$<step>.outputs.<id>` — a prior step output;
- `$<step>.verdict` — a prior step verdict;
- `$item` — current item of a repeat/for-each step;
- project semantic refs such as `project.current_sprint` where the runtime can resolve them.

References pass identifiers/artifact handles where possible, not large serialized documents.

## Dependencies

```yaml
review:
  depends_on: [implement]
```

A step is eligible only after all declared dependencies reach an accepted completion state.

Independent steps may be executed concurrently by a capable runtime, but Workflow v1 does not require parallel execution for correctness.

## Conditions

```yaml
regression:
  skill: regression-suite
  when: $workflow.mode == release
```

`when` controls applicability. A false condition marks the step `skipped`, not failed.

Workflow v1 intentionally does not define a general-purpose scripting language. Conditions should remain simple comparisons over workflow inputs, project state, or prior step verdicts.

## Required and optional steps

```yaml
review:
  required: true

regression-audit:
  required: false
```

Optional steps may improve confidence or produce extra artifacts but do not prevent workflow completion unless another required step explicitly depends on them.

## Completion gates

Every step may define progression requirements:

```yaml
completion:
  accepted_verdicts:
    - approved
  artifacts:
    - ref: design.system_status
      equals: approved
  on_unaccepted: stop
```

Supported v1 `on_unaccepted` behaviors:

- `stop` — pause the workflow so findings can be resolved, then resume/re-run;
- `block` — workflow cannot progress until the blocking condition is resolved;
- `continue` — record the non-accepted result but continue; appropriate only for advisory/optional steps.

A gate is therefore not a separate execution primitive. A **required step with strict completion requirements is a gate**.

## Artifact completion

Step/workflow completion may require persistent evidence in addition to a verdict:

```yaml
artifacts:
  - ref: design.game_concept
    exists: true
  - ref: design.approved_gdds
    min_count: 3
  - ref: story.status
    equals: complete
```

Initial v1 checks:

- `exists: true|false`
- `equals: <value>`
- `min_count: <number>`
- `pattern: <semantic/string pattern>` when the adapter/runtime can safely evaluate it.

Artifact checks are evidence signals, not provider commands.

## Repeatability / for-each

Design and production workflows frequently repeat a Skill for each system/story.

```yaml
design-system:
  skill: design-system
  repeat:
    over: project.systems_to_design
    as: system
  inputs:
    system: $item
```

`repeat.over` must resolve to a finite collection before or during workflow execution. The runtime records each iteration independently.

A dependent step may repeat over the same collection:

```yaml
review-system:
  skill: design-review
  depends_on: [design-system]
  repeat:
    over: $design-system.items
    as: system_result
```

Workflow v1 does not define unbounded autonomous loops.

## Workflow-level completion

Supported v1 strategies:

### `all_required_steps`

All applicable required steps must satisfy their completion rules.

```yaml
completion:
  strategy: all_required_steps
```

### `artifacts`

A workflow may additionally require project artifacts/state:

```yaml
completion:
  strategy: all_required_steps
  artifacts:
    - ref: sprint.qa_handoff
      equals: ready
```

No model assertion can substitute for required completion evidence.

## Workflow verdicts

Recommended normalized runtime states:

- `completed` — completion contract satisfied;
- `paused` — stopped intentionally for user/revision/follow-up action;
- `blocked` — required condition cannot currently be satisfied;
- `cancelled` — user/runtime cancellation.

These describe workflow-run state and are distinct from Skill verdicts.

## Resume

A runtime should persist enough workflow state to resume:

```yaml
workflow_state:
  workflow: story-delivery
  completed_steps: [readiness, implement]
  current_step: review
  step_results:
    readiness: ready
    implement: implemented
```

Conversation history is not the canonical resume source. Persistent workflow/run state is.

Axit-Game-Studios may emulate this with project files. Axit-Code should eventually persist it in the Run Ledger / working-state layer.

## Failure semantics

Skill-level failure/retry remains owned by the Skill/runtime.

Workflow handles the resulting step status:

```text
skill executes
  -> verdict / failure / cancellation
  -> step completion contract
  -> continue / stop / block
```

Workflow v1 does not introduce retries beyond those already declared by the Skill.

## Provider neutrality

Canonical Workflow MUST NOT contain:

- Claude `Task` or slash-command syntax;
- Codex/Gemini-specific orchestration commands;
- provider model IDs;
- provider tool names;
- shell commands used to detect completion.

Adapters/runtime may compile a Workflow into provider-specific execution plans.

## Relationship to Axit-Code

Axit-Code can consume the same Workflow contracts:

```text
Workflow Spec
  -> Agent Runtime scheduler
  -> Skill/Profile loader
  -> capability resolution
  -> Harness / Tool Gateway
  -> Verification
  -> Run Ledger
```

This is the intended boundary: Game Studios proves useful workflows first; Axit-Code later owns safe, resumable execution.