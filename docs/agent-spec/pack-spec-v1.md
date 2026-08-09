# Pack Spec v1

## Purpose

A Pack is a reusable specialization bundle between Universal Core and a concrete Project Layer.

Examples:

```text
engine-unity
puzzle
mission-quest
online-session
host-authoritative
p2p
procedural-generation
persistence
ui-ux
```

A Pack exists so that project-specific needs do not bloat Universal Core and do not need to be recreated independently in every similar project.

## Core shape

```yaml
spec_version: axit.pack/v1
id: mission-quest
name: Mission / Quest Systems
category: gameplay-system
status: draft

description: Reusable design, architecture, implementation, and verification guidance for mission/quest-driven games.

activation:
  project_features:
    - missions_quests.enabled == true

requires: []
conflicts: []

contributes:
  profiles:
    - quest-designer
  skills:
    - author-mission-model
    - review-mission-state
    - verify-mission-progression
  workflows:
    - mission-feature-delivery
  capabilities: []
  checklist_sections:
    - mission-quest

architecture_questions:
  - What is the difference between definition data and runtime instance state?
  - What scopes can own progress: player, party, session, world?
  - What authority owns each scope?
  - What progress must persist?

verification_requirements:
  - transition tests
  - unreachable-content validation where feasible
  - duplicate-completion/idempotency tests

knowledge: []
```

## Field semantics

### `id`

Stable lowercase kebab-case Pack ID.

### `category`

Recommended categories:

```text
engine
network-topology
gameplay-system
production-discipline
platform-service
quality
```

### `activation`

Describes conditions that make the Pack relevant.

Activation is recommendation logic, not automatic authority. The Project Expander may propose a Pack when conditions match, but the Project Manifest records the actual selected set.

### `requires`

Other Packs required for this Pack to function coherently.

Example:

```yaml
id: host-authoritative
requires:
  - online-session
```

Avoid dependency chains based on organizational preference alone.

### `conflicts`

Use only for real architectural incompatibilities.

Example: two network topology Packs may conflict if both claim the same gameplay authority model.

A Pack does not conflict merely because it is usually unnecessary alongside another.

### `contributes`

Pack contributions are references to reusable artifacts.

#### Profiles

Only add a Pack-level specialist Profile when the domain repeatedly requires a distinct responsibility/context package across projects.

#### Skills

Preferred contribution. Most specialization should happen through reusable Skills rather than many new Agents.

#### Workflows

Use when the domain has a repeated multi-Skill process.

#### Capabilities

Only add semantic capabilities when runtime/tool compatibility or policy needs to distinguish them.

#### Checklist sections

Adds conditional project checks.

### `architecture_questions`

Questions every project using the Pack must answer before serious implementation.

These are one of the Pack's most valuable outputs because architecture choices remain project-specific even when the domain is reusable.

### `verification_requirements`

Defines categories of evidence that specialization introduces.

A Pack should make verification stronger, not merely add more prompt text.

## What belongs in a Pack?

A reusable artifact belongs in a Pack when:

- it is not universal across unrelated games;
- it is useful across multiple projects sharing a meaningful domain;
- project names/assets/business rules can be removed;
- inputs/context/capabilities remain semantic;
- verification remains meaningful across projects.

## What does NOT belong in a Pack?

Do not package:

- one project's story/lore/content;
- one project's exact network authority decision when alternatives are valid;
- provider-specific tool mappings;
- engine-specific details inside a gameplay Pack when an Engine Pack can own them;
- speculative systems not yet exercised by a project.

## Pack composition examples

### Small single-player puzzle game

```yaml
packs:
  selected:
    - engine-unity
    - puzzle
    - ui-ux
```

### Host-authoritative online mission game

```yaml
packs:
  selected:
    - engine-unity
    - online-session
    - host-authoritative
    - mission-quest
    - persistence
    - inventory
    - economy-progression
    - ui-ux
```

### P2P systems game

```yaml
packs:
  selected:
    - engine-unity
    - online-session
    - p2p
    - persistence
```

The `p2p` Pack should force the project to specify what P2P means: transport-only peers, distributed ownership, host election, lockstep, etc. It must not smuggle in a single architecture as the default.

## Suggested initial Pack catalog

The following are candidates, not all mandatory to implement immediately.

### Engine

- `engine-unity`
- later: `engine-godot`, `engine-unreal`

### Network / topology

- `online-session`
- `host-authoritative`
- `p2p`
- later: `dedicated-server`, `rollback-deterministic`

### Gameplay systems

- `mission-quest`
- `puzzle`
- `inventory`
- `combat`
- `procedural-generation`
- `economy-progression`
- `dialogue-narrative`

### Production disciplines

- `ui-ux`
- `level-design`
- `art-pipeline`
- `audio`
- `accessibility`
- `localization`
- `release`

Implement Packs only when a vertical slice or project requires them.

## Project overrides

A Project may extend a selected Pack through project-local artifacts.

Example:

```text
Pack: mission-quest
  provides generic mission semantics

Project: MyOnlineRPG
  adds raid-mission-designer profile
  adds guild-contract skill
  adds account-persistent reward transaction rules
```

The project extension must not edit Pack semantics merely to fit one game. If a reusable improvement is discovered, evaluate it separately for promotion.

## Provider and runtime neutrality

Pack artifacts use the same canonical contracts as Core and Project Layer:

```text
Agent Profile Spec
Skill Spec
Workflow Spec
Capability Spec
```

Provider adapters remain outside Pack semantics.

## Pack proof rule

A Pack is not considered stable because its documentation is complete.

It should be proven by at least one project vertical slice, and preferably more than one materially different project before being treated as broadly reusable.
