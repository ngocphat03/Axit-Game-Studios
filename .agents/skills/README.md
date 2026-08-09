# Codex Skill Compatibility Layer

This directory exists only because Codex discovers repository skills under `.agents/skills/`.

It is not the Axit source of truth.

Canonical Core Skills live at:

```text
.axit/core/skills/<skill-id>/SKILL.md
```

Current Codex discovery entries:

- `verify-change` -> canonical `.axit/core/skills/verify-change/SKILL.md`

Until repository symlink handling is standardized in this branch, a discovery entry may be a tiny `SKILL.md` shim that contains only Codex discovery metadata and points Codex to the canonical Axit file. Do not duplicate the full procedure here.

Do not copy legacy Game Studios skills here in bulk.
