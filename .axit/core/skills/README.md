# Core Skills

Reviewed Axit Core Skills live here.

Current active Core Skills:

- [`implement-change`](implement-change/SKILL.md) — applies one bounded project change inside accepted design/architecture, runs implementation-side checks, and prepares a verification handoff.
- [`verify-change`](verify-change/SKILL.md) — independently maps a bounded change's accepted behavior to current evidence and returns `PASS`, `FAIL`, or `BLOCKED`.

Canonical form:

```text
.axit/core/skills/<skill-id>/SKILL.md
```

A Core Skill must be one focused repeatable procedure. Responsibility belongs to Profiles; domain theory belongs to Knowledge; multi-step composition belongs to Workflows.

Implementation and verification remain separate procedures. `implement-change` must not issue the final completion verdict; `verify-change` must not silently repair the implementation it is judging.

Legacy Game Studios skills are reference material only. They are not migrated by default.

Only reviewed Core Skills are exposed through `.agents/skills/` for Antigravity discovery.
