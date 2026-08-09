# Axit Agent Spec v1

## Status

**Draft / experimental.** The current design is being evolved from a Claude-specific Game Studios fork into a reusable, provider-neutral base that can specialize to materially different game projects.

The existing `.claude/` tree remains untouched and authoritative for legacy execution until a replacement adapter/export path is proven.

## Goal

Describe reusable AI-assisted game-development behavior once, then specialize it for each project without forcing every game to inherit the same studio, engine, networking, or gameplay architecture.

```text
Universal Core
    -> Optional Packs
        -> Project Layer
            -> Provider Adapter
                -> Claude / Codex / Gemini / other
                    -> Tools / MCP / Axit-Code Runtime
```

Reference projects and the legacy Game Studios catalog inform the checklist and Packs. They are not universal templates.

## Reusable architecture

See [`Reusable Game Studio Base Architecture v1`](base-architecture-v1.md).

The system has three specialization layers:

1. **Universal Core** — small cross-project Profile/Skill/Workflow foundation.
2. **Optional Packs** — reusable domain modules such as Unity, Puzzle, Mission/Quest, Online Session, Host Authority, P2P, Persistence, UI/UX.
3. **Project Layer** — actual game rules, architecture choices, project-specific agents/skills, state registry, validation baseline.

The promotion rule is conservative:

```text
first occurrence -> Project
repeated in similar projects -> Pack candidate
repeated across unrelated projects -> Core candidate
```

## Canonical artifact types

### Agent Profile

Defines **who owns the responsibility**: boundaries, skills, knowledge, capability needs, model requirements, and escalation.

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

Provider/runtime adapters map these to concrete tools.

### Workflow

Defines **how Skills compose**: dependencies, optional/required steps, simple conditions, finite repeatability, verdict gates, and artifact-driven completion.

Workflow never duplicates Skill procedure.

### Pack

Defines a reusable specialization bundle between Core and Project, including optional Profiles, Skills, Workflows, checklist sections, architecture questions, and verification requirements.

### Project Manifest

Declares one concrete game's engine, player/session model, feature flags, selected Packs, runtime preferences, project rules, architecture registry, and validation baseline.

## v1 documents

### Core specification

1. [`Skill Spec v1`](skill-spec-v1.md)
2. [`Agent Profile Spec v1`](agent-profile-spec-v1.md)
3. [`Capability Spec v1`](capability-spec-v1.md)
4. [`Workflow Spec v1`](workflow-spec-v1.md)
5. [`Pack Spec v1`](pack-spec-v1.md)
6. [`Provider Adapter Boundary`](provider-adapters.md)

### Reusable project system

7. [`Reusable Game Studio Base Architecture v1`](base-architecture-v1.md)
8. [`Project Manifest v1`](project-manifest-v1.md)
9. [`Project Customization Checklist v1`](project-customization-checklist-v1.md)
10. [`Project Expander — Master Prompt`](project-expander-prompt.md)

### Migration evidence

11. [`Migration Slice Findings`](vertical-slice-findings.md)
12. Provider-neutral examples under `docs/agent-spec/examples/`

## Provider neutrality

Canonical Core/Pack/Project artifacts must not depend on:

- Claude `Task` or `AskUserQuestion`;
- Claude hooks/permission syntax;
- concrete Claude/OpenAI/Gemini model IDs;
- provider-specific tool names when semantic capabilities exist.

Profiles and Skills describe requirements:

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

## Human collaboration is semantic

Canonical interactions distinguish:

- question;
- choice;
- decision;
- approval;
- confirmation.

An adapter may render these as chat, CLI input, widgets, or another UI.

## Knowledge is not security

Prompt/rule/profile content can guide behavior but cannot be the security boundary.

Provider adapters may use native permission mechanisms today. Axit-Code later must enforce side effects through Harness/Tool Gateway policy independently of model compliance.

## Verification before completion

When deterministic evidence exists, it outranks model self-assessment:

- build/compile;
- automated tests;
- acceptance-criteria traceability;
- Git/change scope;
- engine/editor state;
- required artifacts;
- structured manual evidence when deterministic checks are impossible.

Every selected Pack should strengthen the project's verification model, not merely add prompt context.

## Reference projects

### Original Game Studios

The original/forked Game Studios catalog is a **breadth checklist** for lifecycle stages, disciplines, and potentially missing concerns.

It should not force every project to load 49 agents, dozens of skills, or every review gate.

### Black Commission

Black Commission is a **reference proof/stress test** for reusable patterns such as:

- project-specific rules and specialist agents;
- state ownership / architecture registry;
- pure domain logic separated from network/Unity wrappers;
- editor/MCP integration beneath high-level project capabilities;
- deterministic validation and smoke baselines;
- file-backed working state.

Its host-authoritative networking, NGO/Relay choices, mission loop, and persistence model remain project-specific decisions, not Core defaults.

## Migration evidence

Three materially different legacy workflow slices were translated into provider-neutral v1 examples.

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

These migrations produced the current Skill/Workflow primitives from real behavior rather than designing them in isolation.

## Target repository shape

Long term:

```text
.agents/
├── project.yaml
├── profiles/
├── skills/
├── workflows/
├── rules/
├── knowledge/
├── registry/
└── schemas/

packs/
├── engine-unity/
├── puzzle/
├── mission-quest/
├── online-session/
├── host-authoritative/
└── ...

adapters/
├── claude/
├── codex/
├── gemini/
└── generic/
```

Do not build all Packs immediately. Build only what a project vertical slice proves necessary.

## Project expansion workflow

For each new project:

```text
Discover project
    -> populate project.yaml
    -> run universal + conditional checklist
    -> select minimum Packs
    -> define architecture/state ownership
    -> establish validation baseline
    -> identify genuine custom Agent/Skill gaps
    -> prove one vertical slice
    -> expand only after evidence
```

Use [`Project Expander — Master Prompt`](project-expander-prompt.md) to perform this specialization consistently.

## Relationship to Axit-Code

`Axit-Game-Studios` is the **spec lab, reusable behavior library, Packs, and project-customization system**.

`Axit-Code` becomes the **product runtime and enforcement layer**:

```text
Core + Packs + Project Layer
    -> Workflow / Agent Runtime
    -> Profile + Skill + Context loader
    -> Capability resolution
    -> Harness / Tool Gateway
    -> File / Git / Process / Engine tools
    -> Verification
    -> Run Ledger + resumable working state
```

A project should be able to move from provider-native tools to Axit-Code without rewriting its canonical Profile/Skill/Workflow semantics.

## Next proof

Before migrating the remaining legacy catalog, use the Project Expander on two contrasting project shapes:

1. a small single-player puzzle vertical slice;
2. an online mission/quest vertical slice (host-authoritative or P2P chosen explicitly).

If both can reuse the same Core while selecting different Packs and project-specific extensions, the new Base boundary is doing its job.
