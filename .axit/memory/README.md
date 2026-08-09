# Axit Memory

This directory preserves compact human/assistant alignment across long-running Codex sessions and future discussions.

Memory is **not** a second source of runtime truth. It summarizes intent, rationale, operating agreements, and lessons that would otherwise be lost when conversation context disappears.

## Files

- `project-memory.md` — compact durable context: north star, stable architecture, operating model, current milestone direction, and anti-drift rules.
- `decision-log.md` — append-oriented record of material decisions, incidents, and framework fixes with rationale.

## Resume protocol

When resuming a design/review discussion after context loss, read only:

1. `project-memory.md`;
2. the recent/relevant entries in `decision-log.md`;
3. `.axit/state/active.md` for current execution state;
4. `.axit/roadmap/milestones-mockup.md` when discussing roadmap alignment;
5. canonical specs/source only for details needed by the current question.

Do not recursively preload `.axit/` merely to reconstruct history.

## Precedence

If memory conflicts with current accepted truth, use this order:

```text
latest explicit user decision / accepted scope
  -> executable source and accepted registry/spec artifacts
  -> active milestone/report evidence
  -> memory summaries
  -> historical discussion
```

Memory must be corrected when it becomes stale; do not change canonical behavior merely to match an outdated memory summary.

## What belongs here

Good memory:

- why a major architecture direction was chosen;
- stable terminology and layer boundaries;
- user/assistant/Codex operating agreements;
- milestone review philosophy;
- recurring incidents and the framework rule added to prevent recurrence;
- unresolved strategic questions worth revisiting.

Do not store here:

- passwords, credentials, endpoints, or machine-local secrets;
- large copied API/schema/source artifacts;
- transient command output;
- detailed current task state that belongs in `.axit/state/active.md`;
- binding/tool schemas already owned by canonical specs or live transport definitions.

## Update moments

Refresh memory after a material architecture decision, milestone review, or incident that changes how future runs should behave.

Do not rewrite memory after every routine task.