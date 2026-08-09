# Reusable Game Studio Base Architecture v1

## Status

Draft. This document narrows Axit Game Studios from a large fixed studio simulation into a reusable base that can be specialized for very different game projects.

The base MUST be useful for projects such as:

- a small single-player puzzle game;
- a systems-heavy simulation;
- a Unity co-op game;
- host-authoritative online play;
- peer-to-peer/session-based multiplayer;
- mission/quest-heavy games;
- procedural or content-heavy projects.

No one project is the canonical architecture for all others.

## Core idea

```text
Universal Core
    |
    +--> Optional Packs
    |      +--> Engine packs
    |      +--> Gameplay/system packs
    |      +--> Network/topology packs
    |      +--> Production-discipline packs
    |
    +--> Project Layer
           +--> project manifest
           +--> project rules
           +--> project-specific agents
           +--> project-specific skills
           +--> architecture/state registry
           +--> validation baseline

Canonical spec
    -> Provider Adapter
        -> Claude / Codex / Gemini / other
            -> Tools / MCP / Axit-Code runtime
```

The **Core is small**. Specialization happens through Packs and the Project Layer.

## Layer 1 — Universal Core

The Universal Core contains only responsibilities and workflows that appear across most game projects.

### Core profiles

Recommended minimal profile set:

1. `project-coordinator`
   - maintains working state, scope, priorities, handoffs, blockers;
   - does not make final creative/technical decisions by itself.

2. `game-designer`
   - owns player-facing rules, loops, interactions, progression intent, and acceptance criteria.

3. `technical-architect`
   - owns architecture boundaries, state ownership, interfaces, engine constraints, and cross-system risks.

4. `implementation-engineer`
   - implements scoped changes according to accepted design/architecture contracts.

5. `qa-verifier`
   - owns evidence quality, regression thinking, smoke plans, test gaps, and completion verification.

A runtime does not need five simultaneous model instances. Profiles are responsibility/context packages.

### Core skill families

The Core should converge toward a small set of reusable skill families:

- `project-discovery`
- `system-map`
- `system-design`
- `architecture-decision`
- `implementation-plan`
- `implement-change`
- `code-review`
- `verify-feature`
- `checkpoint-state`
- `project-check`

Project Packs may add specialized skills such as network-authority review, puzzle validation, quest-graph authoring, Unity scene verification, or Steam lobby integration.

### Core workflow templates

Core workflows should remain generic:

```text
Project Bootstrap
  -> Discover
  -> Map Systems
  -> Select Packs
  -> Define Architecture Contracts
  -> Establish Validation Baseline

Feature Delivery
  -> Readiness
  -> Design/Plan
  -> Implement
  -> Review
  -> Verify
  -> Checkpoint
```

A Core workflow MUST NOT assume the game has combat, missions, networking, a specific engine, or a specific AI provider.

## Layer 2 — Optional Packs

A Pack is a reusable specialization bundle selected because a project actually needs it.

A Pack may contribute:

- Agent Profiles;
- Skills;
- Workflows or workflow fragments;
- capabilities;
- knowledge templates;
- project-checklist items;
- architecture questions;
- verification requirements.

### Pack categories

#### Engine packs

Examples:

- `engine-unity`
- `engine-godot`
- `engine-unreal`

An engine pack can add editor/build/test knowledge without assuming gameplay genre.

#### Gameplay/system packs

Examples:

- `puzzle`
- `mission-quest`
- `combat`
- `inventory`
- `procedural-generation`
- `economy-progression`
- `dialogue-narrative`

#### Network/topology packs

Examples:

- `online-session`
- `host-authoritative`
- `p2p`
- `dedicated-server`
- `rollback-deterministic`

Networking must never be embedded into the universal core merely because one reference project is multiplayer.

#### Production-discipline packs

Examples:

- `ui-ux`
- `level-design`
- `art-pipeline`
- `audio`
- `localization`
- `accessibility`
- `release`

Large projects can opt into more disciplines. Small projects should not pay the context/ceremony cost automatically.

## Layer 3 — Project Layer

Every real project gets a thin project-specific layer.

Suggested structure:

```text
.agents/
├── project.yaml
├── rules/
├── profiles/
├── skills/
├── workflows/
├── knowledge/
└── registry/

production/
└── session-state/
```

The project layer records facts the reusable base cannot know:

- game identity and pillars;
- engine and exact version;
- target platforms;
- gameplay loop;
- selected packs;
- architecture decisions;
- state ownership;
- persistence model;
- network authority/topology;
- project-specific tools/editor commands;
- acceptance and validation baseline;
- custom agents/skills needed by this project.

## Project-specific extension rule

When a project needs new behavior, classify it before modifying the base:

```text
Is this useful only for this project?
  -> Project Layer

Is it reusable by one class of projects?
  -> Pack

Is it genuinely universal across unrelated game projects?
  -> Core candidate
```

Default to the **lowest reusable layer**. Do not promote a project solution directly into Core.

Recommended promotion rule:

- first occurrence -> project-specific;
- repeated across materially different projects -> Pack candidate;
- repeated across multiple unrelated Packs/projects with the same semantics -> Core candidate.

## Black Commission's role

Black Commission is a **reference project and stress test**, not the new template.

Use it to extract reusable lessons such as:

- explicit project rules;
- project-specific specialist agents;
- architecture/state ownership registry;
- host-authority contracts;
- pure game/domain logic separated from network/Unity wrappers;
- deterministic validation baselines;
- high-level Unity editor tooling;
- MCP/editor integration;
- file-backed working state and recovery.

Do not copy project-specific assumptions such as:

- 1–4 player host authority;
- Relay/NGO;
- a commission/mission loop;
- host-owned persistence;
- Black Commission's art/UX/game rules.

These belong in selected Packs or one Project Layer.

## Legacy Game Studios' role

The original/forked Game Studios catalog is a **breadth checklist**.

Its large set of skills and production phases is useful for asking:

- Did this project consider concept/design/architecture/UX/testing/release?
- Is a discipline or artifact missing?
- Is a project-specific specialist needed?

It is NOT evidence that every project must load all agents, skills, phases, or review gates.

The new system should convert this breadth into conditional checklist items instead of mandatory context.

## Provider neutrality

Core, Packs, and Project Layer all use Axit Agent Spec concepts:

```text
Profile
Skill
Workflow
Capability
Project Manifest
Project Checklist
```

Provider adapters compile these concepts into Claude/Codex/Gemini-specific forms.

Provider-specific files are outputs/adapters, not canonical design truth.

## Runtime neutrality

The same project specification should support increasing levels of runtime sophistication:

```text
Level 0: Human + AI chat/files
Level 1: Claude/Codex/Gemini native tools
Level 2: MCP/editor tools
Level 3: Axit-Code Agent Runtime + Harness + Verification + Run Ledger
```

A project should not need to rewrite its core agent/skill semantics when moving between levels.

## Verification rule

Every selected Pack must answer:

1. What can fail uniquely in this domain?
2. What deterministic evidence can verify correctness?
3. What requires manual/playtest evidence?
4. What project state must be persisted for resume/recovery?
5. What operations require approval or stronger runtime policy?

This makes specialization affect verification, not just prompts.
