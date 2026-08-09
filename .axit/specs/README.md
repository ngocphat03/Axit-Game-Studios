# Axit Specs

This directory contains compact format contracts for Axit-owned artifacts such as Profiles, Skills, Workflows, project manifests, and registries.

Specifications are added only when the first accepted Core artifact exercises the contract.

Current specs:

- [`Profile Spec v1`](profile-spec-v1.md) — responsibility/decision lens used by Core and future project Profiles.
- [`Skill Spec v1`](skill-spec-v1.md) — one focused reusable procedure, expressed as a Codex-compatible `SKILL.md`.
- [`Workflow Spec v1`](workflow-spec-v1.md) — composition and transition contract for reviewed Skills/responsibilities toward one bounded outcome.

Rules:

- Codex compatibility must not dictate the whole Axit architecture.
- A Core Skill must also be a valid Codex `SKILL.md`.
- Workflows remain Axit-owned artifacts and are not projected into `.agents/skills/`.
- Avoid speculative schema fields that have not been exercised by a real Core artifact.
- Keep provider/runtime configuration out of canonical Axit Profiles.
- Keep Skills focused on procedure; move broad responsibility, knowledge, project rules, and workflow composition to their owning layers.
