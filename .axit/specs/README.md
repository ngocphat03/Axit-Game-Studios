# Axit Specs

This directory contains compact format contracts for Axit-owned artifacts such as Profiles, Skills, Workflows, Workspace/System routing, Capabilities, Runtime Bindings, and registries.

Specifications are added only when a reviewed artifact or live phase exercises the contract.

Current specs:

- [`Profile Spec v1`](profile-spec-v1.md) — responsibility/decision lens used by Core and future focused extensions.
- [`Skill Spec v1`](skill-spec-v1.md) — one focused reusable procedure, expressed as a Codex-compatible `SKILL.md`.
- [`Workflow Spec v1`](workflow-spec-v1.md) — composition and transition contract for reviewed Skills/responsibilities toward one bounded outcome.
- [`Workspace and System Spec v1`](workspace-system-spec-v1.md) — root-first routing for a product workspace containing interacting game client/backend/CMS/service Systems, cross-system registries, executable contracts, and boundary tests.
- [`Capability Spec v1`](capability-spec-v1.md) — stable transport-neutral semantic evidence/execution operations that sit below Skills/Workflows and above runtime bindings.
- [`Runtime Binding Spec v1`](runtime-binding-spec-v1.md) — maps semantic Capability ids to verified concrete transport operations while keeping runtime availability, permission, and verdict semantics separate.

Rules:

- Codex compatibility must not dictate the whole Axit architecture.
- Active Core/system/workspace Skills must remain valid Codex `SKILL.md` files when exposed for discovery.
- Workflows remain Axit-owned artifacts and are not projected into `.agents/skills/`.
- Capabilities are not Skills and are not projected into `.agents/skills/` merely for discovery.
- Capability ids must describe semantic intent rather than MCP/CLI/provider command names.
- Runtime Binding files may contain provider/tool-specific operation names because they are the isolation layer for transport details.
- Never invent a binding or concrete transport operation name without inspecting the real transport interface.
- Binding definition status is not current runtime availability; availability is resolved per environment/run.
- Capability output is evidence; `verify-change` retains REQUIRED/SUPPORTING classification and final verdict ownership.
- Declaring a Capability or Binding does not grant Runtime/Harness permission.
- Keep credentials, secrets, and ephemeral machine connection state outside canonical `.axit` files.
- Avoid speculative schema fields that have not been exercised by a real artifact or phase.
- Keep provider/runtime configuration out of canonical Axit Profiles and Capability definitions.
- Keep Skills focused on procedure; move broad responsibility, knowledge, rules, architecture, and workflow composition to their owning layers.
- Treat repository root as the workspace when Codex is run from root and interacting Systems share the repository.
- Do not duplicate executable API/schema contracts inside `.axit`; route to their real source instead.
