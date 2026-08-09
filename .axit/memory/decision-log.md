# Axit Decision & Incident Log

Append material decisions and pipeline incidents here. Keep entries short enough to scan during future design reviews.

Use this format:

```text
## YYYY-MM-DD — <title>
Type: decision | incident | promotion | deprecation
Status: active | superseded

Context:
Decision / Root cause:
Why:
Framework effect:
Regression / follow-up:
Superseded by: <optional>
```

---

## 2026-08-09 — Keep Core intentionally small
Type: decision
Status: active

Context:
Legacy Game Studios contained dozens of agents/skills and community evidence showed ceremony/token cost problems.

Decision / Root cause:
Freeze the default Core responsibility set at four Profiles, then add Skills/Workflows only after repeated live evidence.

Why:
Specialization should come from Skills/Knowledge before creating more job-title agents.

Framework effect:
Core v1 = four Profiles, two Skills, one Workflow.

Regression / follow-up:
No Core Skill #3, Workflow #2, or new Profile without a demonstrated durable gap.

## 2026-08-09 — Workspace root is the product boundary
Type: decision
Status: active

Context:
The user normally opens Codex at repository root and keeps Unity/backend/CMS/services together under `src/` so cross-system debugging and tests can inspect both provider and consumer truth.

Decision / Root cause:
Treat repository root as Workspace/Product and `src/*` as Systems, not separate Axit projects by default.

Why:
This enables cross-system contract reasoning and avoids relying on nested working-directory activation.

Framework effect:
Root `.axit/workspace.yaml` routes to `.axit/systems/<system-id>/`.

Regression / follow-up:
Do not duplicate executable API/schema contracts into `.axit`.

## 2026-08-09 — Verification verdict precedence
Type: incident
Status: active

Context:
Early live `verify-change` tests exposed ambiguity between missing runtime evidence and already-demonstrated required failures.

Decision / Root cause:
Required demonstrated failure takes precedence over missing supporting/other evidence; missing essential required evidence becomes BLOCKED only when no required criterion is already known failed.

Why:
Unavailable tooling must not hide a real defect.

Framework effect:
`verify-change` distinguishes PASS / FAIL / BLOCKED with REQUIRED vs SUPPORTING evidence.

Regression / follow-up:
Keep live cases for normal multiplier, zero-damage defect, and unresolved/missing target behavior.

## 2026-08-09 — Capability ids are declared, never invented
Type: incident
Status: active

Context:
A live planning case synthesized plausible but nonexistent capability names.

Decision / Root cause:
Only ids declared by the affected System's active capability set may be named as Axit Capabilities.

Why:
Runtime bindings require stable semantic identifiers.

Framework effect:
Missing operation becomes ordinary evidence need or capability gap, never a fabricated id.

Regression / follow-up:
Capability validation Cases 1–5 are stable regression evidence.

## 2026-08-09 — Runtime binding is separate from Capability semantics
Type: decision
Status: active

Context:
Unity evidence needs transport-specific operations while Core/Capability contracts must remain provider-neutral.

Decision / Root cause:
Semantic Capability -> reviewed Runtime Binding -> concrete operation -> acquisition evidence -> verifier.

Why:
Transport can change without rewriting Profiles/Skills/Workflows/Capability ids.

Framework effect:
Runtime Binding v1 introduced with acquisition states `acquired`, `unavailable`, `denied`, `transport_error`.

Regression / follow-up:
Do not encode transport names back into semantic Capability ids.

## 2026-08-10 — MCP for Unity setup is user-owned
Type: decision
Status: active

Context:
The first continuous plan attempted to include automatic MCP setup, but the user prefers to configure the Unity transport manually before execution.

Decision / Root cause:
Continuous Axit runs may inspect and use an already configured MCP for Unity transport but may not install/configure/start/repair it.

Why:
Local Unity/MCP setup requires user control and should not become an autonomous setup side effect.

Framework effect:
Phase 1 is now a manual prerequisite readiness gate.

Regression / follow-up:
If transport is not ready, stop with `UNITY_MCP_NOT_READY`; do not attempt repair.

## 2026-08-10 — Git status is optional and ignored by default
Type: incident
Status: active

Context:
Continuous execution stopped because a nested Unity repository reported many modified/deleted/untracked files even though the root workspace intentionally may contain nested repos/submodules or untracked systems.

Decision / Root cause:
Add workspace setting:

```yaml
safety:
  check_git_status: false
```

Why:
Git topology/tracking state is not universally a valid readiness gate for the user's workspace style.

Framework effect:
When false, root/nested/submodule Git status must not create `DIRTY_WORKTREE_RISK` merely from modified/deleted/untracked state.

Regression / follow-up:
Projects requiring strict status checks may set the flag to `true`. The setting never authorizes reset/clean/revert/destructive overwrite.

## 2026-08-10 — Milestone automation with human promotion gates
Type: decision
Status: active

Context:
The user wants long autonomous runs while away, followed by joint review before advancing the product roadmap.

Decision / Root cause:
Automation runs continuously inside one accepted milestone; milestone completion produces report + retrospective and then stops for human review.

Why:
This maximizes autonomy without allowing unattended architectural drift across major capability boundaries.

Framework effect:
Future Master Roadmap should compose milestone execution plans and require explicit milestone promotion after review.

Regression / follow-up:
Every milestone retrospective must convert recurring pipeline failures into framework/config fixes plus regression protection where appropriate.

## 2026-08-10 — Milestone closure artifacts are mandatory
Type: incident
Status: active

Context:
M1 completed all technical phases and produced a console summary, but it did not persist the Milestone Report or Retrospective required by the later milestone operating contract.

Decision / Root cause:
The M1 execution plan predated the milestone closure convention, so closure artifacts were never part of its DONE condition.

Why:
Long autonomous runs need durable handoff artifacts so the user and assistant can review results after conversation/session context has changed.

Framework effect:
`.axit/roadmap/milestone-closure.md` is now an active contract. Every future milestone plan must persist `.axit/milestones/<id>-<slug>/report.md` and `retrospective.md` before returning `MILESTONE_DONE`, then stop for human promotion review.

Regression / follow-up:
Use `.axit/templates/milestone-report.md` and `.axit/templates/milestone-retrospective.md`; closure verification must check documentation/state consistency and durable incident hardening.

## 2026-08-10 — Verify effective reasoning, not configured intent
Type: incident
Status: active

Context:
M1 loaded the trusted project config, but the primary session resolved to `gpt-5.6-sol / medium` while `.codex/config.toml` requested `model_reasoning_effort = "max"`. Spawned sub-agents were observed at Sol/max.

Decision / Root cause:
The project used a primary Codex config value outside the currently documented `model_reasoning_effort` enum, whose strongest documented value is `xhigh`. Configured intent was incorrectly treated as sufficient until the live effective session exposed the mismatch.

Why:
Long autonomous milestones should not silently run at a materially lower primary reasoning level than intended.

Framework effect:
Milestone readiness must inspect effective primary/sub-agent model and reasoning levels. For the current Codex config surface, use the strongest supported primary setting rather than assuming the underlying model API's `max` value is accepted by the Codex config key.

Regression / follow-up:
Before M2, correct the primary project setting to the strongest Codex-supported value and verify the fresh session's effective value. If effective reasoning is below the milestone requirement, treat it as readiness failure rather than non-blocking drift.

## 2026-08-10 — Classify evidence provenance without forcing Git tracking
Type: decision
Status: active

Context:
M1 reported a local deterministic test change and 16/16 PASS, but the test source is not present on the pushed root GitHub branch because workspace Systems/tests may be untracked, nested, submodules, or separately tracked.

Decision / Root cause:
Remote review must distinguish canonical pushed evidence, separately tracked/untracked local evidence, and ephemeral runtime evidence instead of assuming every verified artifact is present in the root Git repository.

Why:
The workspace intentionally does not require Git status/tracking as an execution gate, but review still needs honest auditability boundaries.

Framework effect:
Milestone report/retrospective templates require evidence provenance classification.

Regression / follow-up:
Do not force Git tracking solely for auditability. Never claim remote source inspection when evidence exists only in the local/separately tracked System; preserve the local run's recorded evidence and state the review limitation explicitly.
