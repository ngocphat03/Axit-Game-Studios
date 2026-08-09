# Axit Skill Spec v1

## Purpose

A Skill is a compact, reusable **procedure for one repeatable job**.

A Skill answers:

- When should this procedure run?
- What inputs or evidence does it need?
- What ordered actions should be performed?
- What conditions require stopping or handing off?
- What output or evidence should be produced?

A Skill does not own a broad responsibility. Responsibilities belong to Profiles.

## Canonical location

```text
.axit/core/skills/<skill-id>/SKILL.md
```

A Core Skill must also be valid for Codex discovery. The canonical `SKILL.md` is the source of truth; `.agents/skills/` is only a discovery compatibility layer.

## Required frontmatter

Use the Codex-compatible minimum:

```yaml
---
name: verify-change
description: Verify a bounded code or project change against accepted requirements and current evidence, then return PASS, FAIL, or BLOCKED. Use after implementation or when asked whether a feature/fix is actually complete; do not use as the implementation workflow itself.
---
```

Required fields:

- `name` — stable lowercase kebab-case identifier.
- `description` — concise trigger and boundary text. State both when the Skill should run and the most important case where it should not.

Do not add Axit-only frontmatter fields unless Codex compatibility is verified and a real need exists.

## Required body sections

### `# Purpose`

State the single repeatable job performed by the Skill.

### `# Inputs`

List semantic input categories, not one repository's hardcoded paths unless the procedure genuinely requires them.

### `# Procedure`

Provide imperative ordered steps. Keep the procedure focused on one job.

Steps may branch based on evidence or project capabilities, but should not become an end-to-end project lifecycle.

### `# Stop / Handoff Conditions`

State when the procedure must stop, return a blocked result, or hand the issue to another responsibility.

### `# Output`

Define the minimum result/evidence produced by the procedure.

## Optional body sections

Use only when materially helpful:

- `# Evidence Rules`
- `# Constraints`
- `# Examples`
- `# References`

Move large domain knowledge, API reference material, and long examples out of `SKILL.md` when they are not required on every invocation.

## Separation of concerns

```text
Profile     = responsibility / judgment lens
Skill       = reusable procedure
Workflow    = composition of multiple responsibilities or Skills
Knowledge   = theory / reference / domain guidance
Rules       = project constraints and approvals
Registry    = accepted project-wide technical truth
Runtime     = actual authorization and side-effect enforcement
```

A Skill may tell Codex to respect project rules; it must not pretend prompt instructions are runtime security controls.

## Core Skill quality test

Before a Skill enters Core, all answers should be yes:

1. Does the procedure repeat across materially different game projects?
2. Is it one focused job rather than a whole department or lifecycle?
3. Is the procedure distinct from a Profile responsibility description?
4. Can engine/genre/network specialization be supplied by project knowledge or later domain Skills?
5. Are inputs, stop conditions, and outputs explicit?
6. Is the procedure short enough to load on demand without becoming a knowledge dump?
7. Can the same `SKILL.md` serve as the canonical Axit Skill and a valid Codex Skill?

If not, keep it as ordinary Profile behavior, project guidance, Knowledge, or a future domain-specific Skill.

## Codex compatibility rule

Codex uses `name` and `description` for discovery and loads the full `SKILL.md` only when the Skill is selected. Therefore descriptions must be specific enough to avoid accidental activation, and the body should contain only instructions needed once selected.
