# Axit Specs

This directory contains compact format contracts for Axit-owned artifacts such as Profiles, Skills, Workflows, project manifests, and registries.

Specifications are added only when the first accepted Core artifact exercises the contract.

Current specs:

- [`Profile Spec v1`](profile-spec-v1.md) — responsibility/decision lens used by Core and future project Profiles.

Rules:

- Codex compatibility must not dictate the whole Axit architecture.
- A Core Skill must still be expressible as a valid Codex `SKILL.md` when the Skill contract is introduced.
- Avoid speculative schema fields that have not been exercised by a real Core artifact.
- Keep provider/runtime configuration out of canonical Axit Profiles.
