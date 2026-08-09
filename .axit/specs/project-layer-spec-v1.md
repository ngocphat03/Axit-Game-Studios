# Axit Project Layer Spec v1

## Purpose

The Project Layer contains **game-specific context and extensions** that should not be promoted into reusable Axit Core merely because one project needs them.

It answers:

- What project is Codex working on?
- Which local rules and architecture decisions constrain work?
- Which project-specific Profiles, Skills, Workflows, and Knowledge are active?
- Which validation commands or evidence paths matter for this project?
- What current task state should another session be able to resume?

The Project Layer must remain small and explicit. It is not a place to copy the legacy Game Studios catalog.

## Materialized project layout

A concrete game using Axit should converge toward this shape:

```text
AGENTS.md

.axit/
├── project.yaml
├── core/
│   └── ... reviewed reusable Axit Core
├── project/
│   ├── rules.md
│   ├── profiles/
│   ├── skills/
│   ├── workflows/
│   └── knowledge/
├── registry/
│   └── architecture.yaml
└── state/
    └── active.md

.agents/
└── skills/
    └── ... Codex discovery entries for active Skills only
```

The framework repository may keep bootstrap copies under `.axit/templates/`. A concrete game owns the materialized files above.

## Source-of-truth rules

- `.axit/project.yaml` is the project routing manifest.
- `.axit/project/rules.md` contains local working constraints that apply broadly to project changes.
- `.axit/registry/architecture.yaml` contains accepted project-wide architecture truth worth preserving across tasks.
- `.axit/state/active.md` contains compact resumable task state, not long-form conversation history.
- `.axit/project/knowledge/` contains project-specific reference material loaded only when relevant.
- `.axit/project/profiles/`, `skills/`, and `workflows/` contain only reviewed project extensions.
- `.agents/skills/` is a Codex discovery projection, never the canonical Axit source.

## Core relationship

Core stays reusable and project-agnostic.

The Project Layer may constrain or specialize work, but it must not silently rewrite Core contracts.

Rules:

1. Prefer project Rules or Knowledge when the difference is contextual rather than procedural.
2. Add a project Skill only when the project has a repeatable procedure that is not represented by Core.
3. Add a project Profile only when a durable responsibility gap exists that cannot be represented by the four Core responsibility lenses plus focused Skills/Knowledge.
4. Add a project Workflow only when stable composition/transition behavior is repeatedly useful for this project.
5. Do not reuse a Core artifact ID for a different project meaning. Core IDs are reserved in v1.
6. Do not promote project artifacts into Core without reuse evidence from materially different projects.

## Project manifest contract

Canonical file:

```text
.axit/project.yaml
```

The manifest is routing context, not a GDD, architecture document, or task tracker.

It should contain compact information in these categories:

### Identity

- project id/name/summary;
- current development stage when useful.

### Product surface

Only coarse information useful for routing, such as core loop summary, supported player-count range, and target platforms.

Do not duplicate full design documentation in the manifest.

### Engine/runtime identity

Record the engine/runtime/language versions needed to route technical knowledge and tools.

Do not embed API documentation here.

### Axit paths

Point to the canonical local Rules, architecture Registry, state file, and Project Layer roots.

### Active project extensions

List only project-specific Profiles, Skills, Workflows, and Knowledge entries that are currently active.

An empty list is valid and preferred until specialization is demonstrated.

### Validation

Declare project-specific checks or commands that may supply evidence, grouped by semantic purpose rather than provider name where practical.

Examples include static checks, automated tests, editor/runtime checks, and playtest/manual evidence.

The manifest declares expected evidence paths; Skills still decide which evidence is required for a bounded criterion.

## Activation semantics

Project artifacts are not automatically loaded merely because they exist under `.axit/project/`.

An artifact becomes active when one of these is true:

- the project manifest explicitly lists it;
- the user explicitly requests it;
- a selected Workflow/Skill references it as relevant project context.

Do not recursively preload `.axit/project/`.

For Codex Skills, only active Skills should receive discovery entries under `.agents/skills/`.

## Precedence and conflict handling

Use this order when interpreting a concrete task:

```text
user request / accepted task scope
        ↓
project Rules + accepted architecture Registry
        ↓
active Project Layer specialization
        ↓
Axit Core procedure / responsibility
        ↓
implementation details
```

This is not a license for project files to weaken runtime permissions or safety controls.

If project Rules/Registry conflict with the requested change, surface the conflict instead of silently choosing whichever source is convenient.

If two active project artifacts conflict, stop and resolve the project configuration rather than relying on file order.

## Project Profiles

Canonical form:

```text
.axit/project/profiles/<profile-id>/PROFILE.md
```

Project Profiles use Profile Spec v1.

Create one only when the project has a durable responsibility not covered by:

- `game-designer`;
- `technical-architect`;
- `implementation-engineer`;
- `quality-verifier`;
- focused project Skills/Knowledge.

Do not recreate job-title catalogs such as network-programmer, ui-programmer, engine-programmer, or producer by default.

## Project Skills

Canonical form:

```text
.axit/project/skills/<skill-id>/SKILL.md
```

Project Skills use Skill Spec v1 and must remain valid Codex Skills when exposed.

Good project Skill candidates are repeatable local procedures such as a project-specific content import, scene validation, save migration, or deployment check.

Engine/API reference material alone belongs in Knowledge rather than a Skill.

## Project Workflows

Canonical form:

```text
.axit/project/workflows/<workflow-id>/WORKFLOW.md
```

Project Workflows use Workflow Spec v1.

Do not create a Workflow merely to list every possible development phase. Encode only composition that the project repeatedly benefits from.

## Project Knowledge

Canonical root:

```text
.axit/project/knowledge/
```

Use Knowledge for facts and guidance such as:

- gameplay/system rules too detailed for `project.yaml`;
- engine/package conventions;
- network or persistence model explanation;
- project naming/layout conventions;
- external integration guidance;
- design references used repeatedly.

Keep large material split by topic so Codex can load it on demand.

## Architecture Registry

Canonical file:

```text
.axit/registry/architecture.yaml
```

Register only decisions that are shared, high-risk, or likely to be violated accidentally across tasks, such as:

- authoritative state ownership;
- public/cross-system interfaces;
- dependency direction;
- persistence/networking topology decisions;
- performance budgets;
- explicitly forbidden architecture patterns.

Do not record every private helper or local implementation detail.

## Active state

Canonical file:

```text
.axit/state/active.md
```

Keep it compact and resumable:

- current bounded task;
- accepted decisions relevant to continuation;
- progress/current phase;
- files materially changed;
- verification state;
- blocker or next action.

The file is a checkpoint, not a transcript.

## Codex exposure

`AGENTS.md` remains the lightweight bootstrap.

It should tell Codex where the project manifest and Axit routing sources live without preloading all of them.

Only active Core/Project Skills should be exposed under `.agents/skills/`.

Profiles, Workflows, Rules, Registry, Knowledge, and State remain `.axit` artifacts loaded when routing requires them.

## Project Layer quality test

Before adding a project-specific artifact, ask:

1. Is this truly specific to the project or proven reusable domain behavior?
2. Could the same need be satisfied by a small Rule or Knowledge entry instead?
3. If it is a Skill, is there a repeatable procedure rather than just information?
4. If it is a Profile, is there a durable responsibility gap rather than a specialist job title?
5. If it is a Workflow, is there stable repeated composition worth encoding?
6. Is activation explicit and context-efficient?
7. Does this preserve the Core instead of forking it silently?

If these are not satisfied, keep the project simpler.
