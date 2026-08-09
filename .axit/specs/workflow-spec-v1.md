# Axit Workflow Spec v1

## Purpose

A Workflow is a compact, reusable **composition and transition contract** for a bounded outcome.

A Workflow answers:

- When may the flow start?
- Which accepted Skills or responsibilities participate?
- In what order do steps run?
- What result moves the flow forward, loops it, or stops it?
- What condition means the workflow is complete?

A Workflow does not duplicate the procedure inside a Skill and does not replace Profile responsibility boundaries.

## Canonical location

```text
.axit/core/workflows/<workflow-id>/WORKFLOW.md
```

Workflows are Axit-owned artifacts. They are not Codex Skills and are not exposed through `.agents/skills/`.

Codex may be routed to a Workflow by `AGENTS.md`, a user request, or a future Axit runtime, but the canonical workflow remains under `.axit/`.

## Required frontmatter

```yaml
---
spec_version: axit.workflow/v1
id: bounded-change
summary: Implement and independently verify one bounded change whose intent and architecture are already sufficiently resolved.
status: validation
---
```

Required fields:

- `spec_version` — workflow contract version.
- `id` — stable lowercase kebab-case identifier.
- `summary` — one-sentence bounded outcome.
- `status` — lifecycle state such as `validation` or `active`.

Do not add provider, model, tool, or runtime-permission configuration to Workflow frontmatter.

## Required body sections

### `# Entry Conditions`

State what must already be true before the Workflow starts.

If a missing decision belongs to a Profile responsibility, the Workflow should stop or hand off instead of silently deciding it.

### `# Participants`

List the accepted Skills or responsibility lenses used by the Workflow.

Reference them by stable ID. Do not copy their procedures into the Workflow.

### `# Flow`

Describe the ordered composition at a high level.

Each step should invoke or route to an accepted Skill/responsibility and consume the previous step's current output/evidence.

### `# Transitions`

Define how concrete outcomes change workflow state.

Examples:

- verification `PASS` -> complete;
- verification `FAIL` -> bounded repair when the failure remains inside accepted scope;
- verification `BLOCKED` -> stop until the blocker is resolved.

Transitions must not hide scope expansion or material design/architecture decisions inside an automatic loop.

### `# Completion`

Define the evidence-backed condition that means the Workflow has completed successfully.

Do not let an implementation step declare a workflow complete when an independent verification step owns the final verdict.

### `# Stop / Handoff Conditions`

State when the Workflow exits to another responsibility, waits for evidence/tooling, or stops because scope changed.

### `# Output`

Define the minimum final artifacts or evidence returned by the Workflow.

## Workflow boundaries

A Core Workflow should:

1. compose already accepted Core Skills or responsibilities;
2. remain useful across materially different game projects;
3. solve one bounded outcome rather than an entire project lifecycle;
4. avoid duplicating Skill instructions;
5. keep domain, engine, networking, and project specialization outside Core unless reuse is demonstrated;
6. make stop/loop/completion semantics explicit;
7. remain safe when a step is skipped because its responsibility is already resolved elsewhere.

A Workflow should not:

- become a second copy of a Skill procedure;
- invent runtime permissions or bypass project approval rules;
- force design or architecture work into every implementation task;
- encode sprint/release ceremony as universal game-development behavior;
- create unbounded retry loops;
- treat provider-specific agent orchestration as canonical Axit architecture.

## Separation of concerns

```text
Profile     = responsibility / judgment lens
Skill       = reusable procedure
Workflow    = composition + transitions toward one bounded outcome
Knowledge   = theory / reference / domain guidance
Rules       = project constraints and approvals
Registry    = accepted project-wide technical truth
Runtime     = actual authorization and side-effect enforcement
```

## Core Workflow quality test

Before a Workflow enters Core, all answers should be yes:

1. Is the composed outcome repeated across materially different projects or common task types?
2. Are all referenced Skills/responsibilities already accepted or clearly defined?
3. Does the Workflow add useful composition/transition semantics instead of merely listing steps?
4. Are entry, stop, loop, and completion conditions explicit?
5. Can optional responsibility steps remain outside the Workflow when they are not needed?
6. Does final completion depend on the correct evidence-owning responsibility?
7. Is the Workflow small enough to understand without loading unrelated project lifecycle material?

If not, keep the composition informal until real use demonstrates a stable Workflow.