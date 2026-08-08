# Axit Agent Spec v1

## Status

**Draft / experimental.** This provider-neutral specification is being proven inside `Axit-Game-Studios` before replacing the existing Claude-specific implementation.

The current `.claude/` tree remains untouched and authoritative for existing execution until adapter/export parity is demonstrated.

## Goal

Describe AI-assisted game-development work once, then run it through different AI/runtime environments:

```text
Canonical Axit Agent Spec
        |
        +--> Claude adapter
        +--> Codex / OpenAI adapter
        +--> Gemini adapter
        +--> Generic adapter
        +--> Axit-Code runtime
```

Provider-specific runtimes are adapters, not sources of truth.

## Canonical layers

### Agent Profile

Defines **who owns the responsibility**: role boundaries, skills, knowledge, capability needs, model requirements, and escalation.

### Skill

Defines **how one reusable unit of work is performed**: inputs, preconditions, context, interactions, procedure, outputs, verification, verdicts, and state transitions.

### Capability

Defines **what the environment must be able to do** using semantic IDs such as:

```text
file.read
code.edit
test.run
user.approval
agent.consult
unity.inspect
```

Provider adapters map these to concrete tools.

### Workflow

Defines **how Skills compose**: dependencies, optional/required steps, simple conditions, finite repeatability, verdict gates, and artifact-driven completion.

Workflow never duplicates Skill procedure.

## v1 specifications

1. [`Skill Spec v1`](skill-spec-v1.md)
2. [`Agent Profile Spec v1`](agent-profile-spec-v1.md)
3. [`Capability Spec v1`](capability-spec-v1.md)
4. [`Workflow Spec v1`](workflow-spec-v1.md)
5. [`Provider Adapter Boundary`](provider-adapters.md)
6. [`Migration Slice Findings`](vertical-slice-findings.md)

## Principles

### Provider-neutral canonical source

Canonical artifacts must not depend on:

- Claude `Task` or `AskUserQuestion`;
- Claude hooks/permission syntax;
- concrete Claude/OpenAI/Gemini model IDs;
- provider-specific tool names when semantic capabilities exist.

### Capability-based model/tool routing

Profiles and Skills describe requirements, for example:

```yaml
model_requirements:
  reasoning: high
  coding: medium
  tool_use: required

capabilities:
  required:
    - file.read
    - test.run
```

The runtime/adapter chooses the concrete provider, model, and tool implementation.

### Human collaboration is semantic

Canonical interactions distinguish:

- question;
- choice;
- decision;
- approval;
- confirmation.

An adapter can render these as chat, CLI prompts, widgets, or another UI.

### Knowledge is not security

Prompt/rule/profile content can guide behavior but cannot be the security boundary.

Claude adapters may use permissions/hooks today. Axit-Code later must enforce side effects through Harness/Tool Gateway policy independently of model compliance.

### Verification before completion

When deterministic evidence exists, it outranks model self-assessment:

- build/compile;
- automated tests;
- acceptance-criteria traceability;
- Git/change scope;
- Unity Console state;
- required artifacts;
- structured manual evidence where deterministic checks are impossible.

## Migration evidence

Three different workflow slices have been translated into v1 examples.

### Production

```text
story-readiness -> dev-story -> code-review -> story-done
```

### Design

```text
brainstorm -> design-system -> design-review
```

### QA

```text
qa-plan -> smoke-check -> regression-suite -> test-evidence-review
```

These migrations produced the current Skill/Workflow primitives rather than designing them in isolation.

See `docs/agent-spec/examples/` and `docs/agent-spec/examples/workflows/`.

## Proposed canonical repository layout

Once adapter parity is proven, the source tree should converge toward:

```text
.agents/
├── profiles/
├── skills/
├── workflows/
├── rules/
├── knowledge/
├── schemas/
└── config.yaml

adapters/
├── claude/
├── codex/
├── gemini/
└── generic/
```

The existing `.claude/` tree should not be mass-migrated until an exporter can reproduce a small working slice reliably.

## Migration strategy

Audit each legacy artifact as:

- **Keep** — semantics are strong; translate.
- **Merge** — overlap should become one canonical artifact.
- **Redesign** — intent is useful but structure/provider coupling is wrong.
- **Delete** — redundant or low-value.

Migrate vertical slices, not all 73 skills at once.

## Relationship to Axit-Code

`Axit-Game-Studios` is the **spec lab / reference behavior library**.

`Axit-Code` becomes the **product runtime and enforcement layer**:

```text
Axit Agent Spec
    -> Workflow / Agent Runtime
    -> Profile + Skill + Context loader
    -> Capability resolution
    -> Harness / Tool Gateway
    -> File / Git / Process / Unity
    -> Verification
    -> Run Ledger + resumable working state
```

## Next proof

Before migrating the rest of the catalog, implement one adapter/export proof for the `story-delivery` workflow:

1. canonical v1 artifacts as source;
2. generate or adapt Claude Code artifacts;
3. verify behavior against the current legacy workflow;
4. run the same canonical artifacts through a second provider adapter (Codex/OpenAI or Gemini);
5. only then scale migration across the remaining skill catalog.
