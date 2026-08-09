# Core Workflows

Reviewed Axit Core Workflows live here.

Current Core Workflow under validation:

- [`bounded-change`](bounded-change/WORKFLOW.md) — composes bounded implementation with independent evidence-based verification when both jobs are explicitly needed.

Canonical form:

```text
.axit/core/workflows/<workflow-id>/WORKFLOW.md
```

Core Workflows compose reviewed Skills/responsibilities and define entry, transition, stop, loop, and completion semantics. They do not duplicate Skill procedures.

Workflows are Axit-owned artifacts, not Codex Skills. They are not exposed through `.agents/skills/`.

Legacy Game Studios workflows are reference material only. They are not migrated by default.
