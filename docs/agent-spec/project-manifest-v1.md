# Project Manifest v1

## Purpose

`project.yaml` is the smallest project-specific contract needed to specialize the reusable Game Studio Base.

It answers **what this game is, what it needs, and which reusable Packs should be activated** without copying all Core agents/skills into the project.

Suggested canonical location:

```text
.agents/project.yaml
```

## Template

```yaml
spec_version: axit.project/v1

project:
  id: my-game
  name: My Game
  summary: Short description of the player experience.
  stage: concept

product:
  genres: []
  core_loop: ""
  player_count:
    min: 1
    max: 1
  session_model: single_session
  target_platforms: [pc]

engine:
  name: unity
  version: ""
  language: csharp

features:
  multiplayer:
    enabled: false
    topology: none
    authority: none
    transport: none
    reconnect: false
    late_join: false

  missions_quests:
    enabled: false
    complexity: none
    branching: false
    scope: []
    persistent_progress: false

  puzzle:
    enabled: false
    systemic: false
    reset_required: false
    hints: false

  procedural_generation:
    enabled: false
    deterministic: false
    seed_authority: none

  persistence:
    enabled: false
    scope: local
    authority: local
    migration_required: false

  economy_progression:
    enabled: false

  combat:
    enabled: false

  inventory:
    enabled: false

packs:
  selected:
    - engine-unity
  proposed: []

runtime:
  preferred_ai_providers: []
  editor_bridge: none
  axit_code: optional

project_rules:
  source: .agents/rules/project-rules.md

architecture_registry:
  source: .agents/registry/architecture.yaml

validation:
  static_checks: []
  automated_tests: []
  editor_checks: []
  playtest_matrix: []

customization:
  custom_profiles: []
  custom_skills: []
  custom_workflows: []
```

## Field guidance

### `product`

Keep this short and factual. It is routing context, not a full GDD.

`core_loop` should be one sentence or a compact chain, for example:

```text
explore -> inspect clues -> solve room puzzle -> unlock next area
```

or:

```text
lobby -> select mission -> deploy -> complete objectives -> extract -> progression
```

### `features`

Feature flags drive checklist and Pack selection. Do not enable a feature because it may exist someday; enable it when the current project scope depends on it.

### Multiplayer topology values

Suggested values:

```text
none
local
host-client
p2p
listen-server
dedicated-server
lockstep
rollback
```

`authority` should describe the gameplay truth owner, for example:

```text
host
server
peer-owner
deterministic-simulation
hybrid
```

### Mission/quest scope

When missions/quests are enabled, declare relevant progress scopes:

```yaml
scope:
  - player
  - party
  - session
  - world
```

This forces the project to confront multiplayer/persistence semantics before implementation.

### `packs.selected`

Only activate Packs that currently add value. A small puzzle prototype might use:

```yaml
packs:
  selected:
    - engine-unity
    - puzzle
    - ui-ux
```

A co-op mission game might use:

```yaml
packs:
  selected:
    - engine-unity
    - online-session
    - host-authoritative
    - mission-quest
    - inventory
    - economy-progression
    - ui-ux
```

### `runtime`

Provider preferences are runtime choices, not Skill/Profile semantics.

Example:

```yaml
runtime:
  preferred_ai_providers:
    - codex
    - claude
  editor_bridge: mcp-for-unity
  axit_code: optional
```

### `validation`

Validation is project-specific and should be established during bootstrap.

Example:

```yaml
validation:
  static_checks:
    - git diff --check
  automated_tests:
    - Unity EditMode tests
  editor_checks:
    - compile with zero Console errors
    - validate required scene anchors
  playtest_matrix:
    - solo
    - host + 1 client
    - host + 3 clients
```

## Manifest update rule

Update `project.yaml` when one of these changes materially:

- game scope;
- engine/version;
- network topology/authority;
- persistence model;
- selected Packs;
- major project validation baseline.

Do not update it for ordinary feature/story changes.

## Relationship to project customization

The Project Expander reads this manifest and asks:

```text
What is already covered by Core?
What selected Pack covers this need?
What remains genuinely project-specific?
```

Only the final category should produce custom Agent Profiles or Skills by default.
