# Axit Profile Spec v1

## Purpose

A Profile is a compact **responsibility and decision lens** for Codex/Axit work.

A Profile answers:

- What responsibility is this role accountable for?
- When should this lens be used?
- What decisions belong to it?
- What decisions are outside its scope?
- What project context must be read before acting?
- What evidence should it expect before accepting an outcome?

A Profile does **not** define a step-by-step procedure. Procedures belong in Skills and Workflows.

## Canonical location

```text
.axit/core/profiles/<profile-id>/PROFILE.md
```

Project-specific profiles may use the same contract under a project-owned profile directory later.

## Required frontmatter

```yaml
---
spec_version: axit.profile/v1
id: technical-architect
summary: Owns technical structure, system boundaries, shared-state ownership, and cross-system contracts.
status: active
---
```

Required fields:

- `spec_version` — must be `axit.profile/v1` for this version.
- `id` — stable lowercase kebab-case identifier.
- `summary` — one sentence describing the responsibility.
- `status` — `draft`, `active`, or `deprecated`.

Do not add fields merely because a provider supports them.

## Required sections

Every Profile must contain these sections.

### `# Responsibility`

Defines the single broad responsibility the Profile owns.

A Profile should be understandable without reading another Profile.

### `# Use When`

Short conditions that make this Profile relevant.

These are semantic triggers, not provider routing syntax.

### `# Do Not Use For`

States cases that belong to another responsibility lens or do not justify loading this Profile.

This section prevents Profile overuse.

### `# Decisions Owned`

Lists decision categories this Profile may analyze and recommend.

The user remains the final authority for product/strategic decisions unless project rules explicitly say otherwise.

### `# Boundaries`

Lists actions/decisions the Profile must not silently take.

When a boundary is crossed, surface the conflict or request the appropriate responsibility/decision instead of inventing authority.

### `# Context Requirements`

Defines the minimum categories of project context that should be read when relevant.

Use semantic categories such as:

- project manifest;
- accepted architecture registry entries;
- relevant system design;
- affected source/module boundaries;
- current validation constraints.

Do not hardcode one repository layout into the Core Profile when a semantic category is enough.

### `# Verification Expectations`

Defines what evidence should exist before the Profile considers its responsibility satisfied.

Examples include:

- architecture registry consistency;
- explicit state ownership;
- interface contract coverage;
- performance/testability consequences documented;
- affected deterministic checks identified.

A Profile does not execute every verification itself; it defines the quality bar for its responsibility.

### `# Escalation`

Defines when the Profile should stop and surface a decision/conflict to the user or another responsibility lens.

Escalation is semantic. Do not encode a fixed multi-agent hierarchy.

## Optional sections

Use only when the first real Profile needs them:

- `# Default Heuristics` — compact reusable decision principles.
- `# Typical Outputs` — common artifacts or findings produced by this lens.

Avoid adding optional sections by default.

## Forbidden concerns

Core Profiles must not contain provider/runtime configuration such as:

```text
tools: Read, Write, Bash
model: opus / sonnet / gpt-*
maxTurns
memory
AskUserQuestion
Task/subagent syntax
slash commands
provider permission syntax
```

They must also avoid owning concerns that belong elsewhere:

```text
step-by-step procedure        -> Skill
multi-step composition        -> Workflow
domain theory/reference       -> Knowledge
project-specific constraints  -> Project Rules / Registry
runtime authorization         -> Runtime/Harness policy
Codex discovery metadata      -> Codex compatibility layer
```

## Profile quality test

Before a Profile is accepted into Core, all answers should be yes:

1. Is the responsibility useful across materially different game projects?
2. Is it distinct from existing Core Profiles?
3. Can specialization usually be supplied through Skills/Knowledge instead of another Profile?
4. Can the Profile remain engine-, genre-, network-, and provider-neutral?
5. Does it have clear boundaries and escalation behavior?
6. Does it define a meaningful verification quality bar?

If not, keep the behavior project/domain-specific or represent it as a Skill/Knowledge artifact instead.
