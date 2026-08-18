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
The user normally opens Antigravity at repository root and keeps Unity/backend/CMS/services together under `src/` so cross-system debugging and tests can inspect both provider and consumer truth.

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
Add workspace setting `safety.check_git_status: false`.

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
The roadmap composes milestone execution plans and requires explicit human promotion after review.

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
`.axit/roadmap/milestone-closure.md` is active. Every future milestone plan must persist report + retrospective before returning `MILESTONE_DONE`, then stop for human promotion review.

Regression / follow-up:
Use milestone report/retrospective templates; closure verification must check documentation/state consistency and durable incident hardening.

## 2026-08-10 — Verify effective reasoning, not configured intent
Type: incident
Status: active

Context:
M1 loaded the trusted project config, but the primary session resolved to `gpt-5.6-sol / medium` while `.agents/mcp_config.json` requested `model_reasoning_effort = "max"`. Spawned sub-agents were observed at Sol/max.

Decision / Root cause:
Configured intent was incorrectly treated as sufficient until the live effective session exposed the mismatch.

Why:
Long autonomous milestones should not silently run at a materially lower primary reasoning level than intended.

Framework effect:
Milestone readiness must inspect effective primary/sub-agent model and reasoning levels.

Regression / follow-up:
M3 Phase 0 verified a fresh effective primary `gpt-5.6-sol / xhigh` from authoritative root turn context, closing the milestone-specific mismatch. Future long milestones must retain effective-value readiness checks rather than infer behavior from config alone.

## 2026-08-10 — Classify evidence provenance without forcing Git tracking
Type: decision
Status: superseded

Context:
M1 reported local evidence not present on the pushed root GitHub branch.

Decision / Root cause:
Initially distinguish canonical pushed evidence, separately tracked/untracked local evidence, and ephemeral runtime evidence.

Why:
Review needed honest auditability without forcing Git tracking.

Framework effect:
Initial milestone templates used root-oriented provenance classes.

Regression / follow-up:
Superseded by the System-aware provenance decision below after QuickGun's separate canonical repository was audited.

Superseded by: 2026-08-10 — Evidence provenance is Workspace/System aware

## 2026-08-10 — Promote M1 and defer primary reasoning repair through M2
Type: promotion
Status: superseded

Context:
M1 passed technical review and the user approved moving to M2 while explicitly deferring the reasoning mismatch.

Decision / Root cause:
Promote M1 and authorize M2 with `DEFERRED_PRIMARY_REASONING_CONFIG` as an M2-only exception.

Why:
Priority was validating autonomous bounded development without pretending the reasoning mismatch was fixed.

Framework effect:
M2 could proceed while recording the issue.

Regression / follow-up:
The exception expired at M2 human review. M3 does not inherit it.

Superseded by: 2026-08-10 — Primary project reasoning set to xhigh for M3

## 2026-08-10 — Runtime paths require current target resolution
Type: incident
Status: active

Context:
M1 and M2 independently repeated the same hierarchy error: a prefab-relative path omitted the live `GameEnvironment/` scene wrapper.

Decision / Root cause:
Prefab/source hierarchy was incorrectly treated as the complete runtime hierarchy.

Why:
Two milestones repeating the same failure establishes a durable procedural gap.

Framework effect:
Runtime Binding validation Case 8 now requires current full runtime target identity to be acquired/derived before path-addressed runtime operations. Refresh/reload invalidates stale identity and requires re-resolution.

Regression / follow-up:
Do not add a new Capability solely for target resolution when ordinary current read-only evidence is sufficient.

## 2026-08-10 — Evidence provenance is Workspace/System aware
Type: decision
Status: active

Context:
Post-M2 audit found that QuickGun production changes were canonical and pushed in `ngocphat03/QuickGun-MVP` even though the root Axit-Game-Studios repository did not track them.

Decision / Root cause:
Repository provenance belongs to the relevant Workspace/System boundary, not only the root repository.

Why:
A product Workspace may mount nested, submodule, or independently tracked Systems while still needing accurate canonical auditability.

Framework effect:
Use `workspace-canonical-pushed`, `system-canonical-pushed`, `local-or-separately-tracked`, and `ephemeral-runtime`. `system-canonical-pushed` records System id + repository + ref + commit when known. `unity-client` records `ngocphat03/QuickGun-MVP` / `release` as canonical repository metadata.

Regression / follow-up:
Resolve moving branch commits at evidence time; never treat an old branch-head commit as timeless truth. Do not change Git topology solely for provenance.

## 2026-08-10 — Primary project reasoning set to xhigh for M3
Type: decision
Status: active

Context:
The earlier project `max` setting did not yield a reliably observed maximum primary session, while M2 was explicitly allowed to defer the issue.

Decision / Root cause:
After M2 promotion, change the primary project setting to `model_reasoning_effort = "xhigh"`.

Why:
Use a stable high-reasoning primary orchestrator while verifying the effective value at readiness.

Framework effect:
M3 Phase 0 required effective fresh primary `gpt-5.6-sol / xhigh` and verified it successfully.

Regression / follow-up:
The primary Sol/xhigh part remains active. The earlier sub-agent-max default is superseded by the later Luna/medium model-routing policy.

## 2026-08-10 — Promote M2 and prioritize Unity compile coverage
Type: promotion
Status: active

Context:
M2 completed four frozen scenarios, context continuity, bounded lane replacement, and independent closure verification. A direct audit of `ngocphat03/QuickGun-MVP` confirmed the real S1 bug fix and S3 behavior-preserving refactor in canonical System source.

Decision / Root cause:
Promote M2 without rerun and open M3 — Unity Execution Coverage.

Why:
The remaining repeated gap was trustworthy full Unity/project compilation after production changes.

Framework effect:
M3 began with live discovery and minimal mapping of `unity.compile`, then proved a full QuickGun baseline compile and a REAL product scenario with full Unity compile REQUIRED.

Regression / follow-up:
Do not bind all remaining Unity Capabilities. Additional mappings require demonstrated `REQUIRED_NOW` evidence.

## 2026-08-10 — Compact active state after promotion
Type: decision
Status: active

Context:
M2 proved continuity, but `active.md` accumulated a large completed-milestone transcript.

Decision / Root cause:
After human promotion, detailed completed history belongs in report/retrospective/scenario manifest rather than active state.

Why:
Long-running roadmap continuity must remain token-efficient and easy for a fresh agent to reconstruct.

Framework effect:
Milestone closure requires post-promotion active-state compaction.

Regression / follow-up:
Future continuity probes should succeed from compact active state plus durable pointers.

## 2026-08-10 — Unity 6 compile evidence requires correlated terminal acquisition
Type: incident
Status: active

Context:
M3 observed that the real CoplayDev compile request returns before Unity 6 has completed script compilation/reload. Initial final-closure verification then found that the promoted binding required a sampled nonterminal boolean even though live discovery and marker-based evidence allowed other freshness signals; historical Phase 4 also reused one snapshot for freshness and terminal proof.

Decision / Root cause:
Request acceptance is dispatch evidence, not compile success; freshness correlation and terminal readiness are separate facts and require separate observations.

Why:
Only exact-target, fresh-cycle, terminal, completely paged diagnostics can support a trustworthy full-project compile criterion.

Framework effect:
The existing `unity.compile` binding clears Console diagnostics before request, establishes `fresh_cycle_correlated` through sampled nonterminal state, an advanced compile marker, or an advanced domain-reload marker, then separately establishes `terminal_state_observed`. It re-resolves exact editor/project identity and pages diagnostics to completion.

Regression / follow-up:
Verifier scripts must map explicit freshness and terminal flags and may not reuse the freshness snapshot as terminal proof. Treat transient reload noise as recoverable only after exact target and complete terminal evidence are re-established.

## 2026-08-10 — Closure verdict must be persisted before MILESTONE_DONE
Type: incident
Status: active

Context:
M3 terminal output reported final closure PASS and `MILESTONE_DONE`, but the pushed report, retrospective, and active state still said final closure verification was pending.

Decision / Root cause:
The closure contract required a verifier before terminal output but omitted an explicit post-verdict persistence and consistency step.

Why:
A future session must be able to reconstruct terminal truth from durable artifacts without relying on console/chat history.

Framework effect:
Closure now requires: verifier result -> persist report/retrospective/active state -> fresh read-only consistency audit -> `MILESTONE_DONE`.

Regression / follow-up:
A console-only terminal verdict with durable files still saying pending is a closure defect. Repair closure artifacts only; do not rerun unaffected product/runtime evidence.

## 2026-08-10 — Child lanes use Luna/medium by default
Type: decision
Status: active

Context:
Historical M3 used Sol/max children and took roughly 3h15m. The user explicitly prioritized total efficiency and cost control over giving every delegated lane the flagship model.

Decision / Root cause:
Reserve GPT-5.6 Sol / xhigh for the primary orchestrator. Every child/sub-agent role defaults to GPT-5.6 Luna / medium, including explorers, workers, evidence lanes, recovery agents, independent verifiers, closure verifiers, report authors, and custom agents.

Why:
Most child work is bounded and checkable. Overall system quality depends more on decomposition, evidence, independence, recovery, and context discipline than on maximizing model size for every lane.

Framework effect:
`.axit/policies/model-routing.md` is canonical and is routed from `workspace.yaml` and root `AGENTS.md`. `.agents/mcp_config.json` and the custom `axit-verifier` use Luna/medium for child execution.

Regression / follow-up:
No silent child escalation to Terra/Sol or above medium. Underperformance uses distilled context, steer/resume, fresh Luna/medium replacement, decomposition, and evidence reacquisition. Only an explicit current human instruction may authorize a bounded override, and milestone closure must audit overrides. This supersedes only the earlier sub-agent-max portion of the M3 reasoning decision; primary Sol/xhigh remains active.

## 2026-08-10 — Promote M3 Unity Execution Coverage
Type: promotion
Status: active

Context:
M3 proved a live `unity.compile` binding, full QuickGun baseline compilation, compile acquisition-state semantics, a frozen REAL duplicate-release fix, 3/3 deterministic regression tests, fresh post-change full Unity compile, and final independent closure PASS. Remote review verified the pushed QuickGun fix and Workspace artifacts.

Decision / Root cause:
Promote M3 without a full rerun after repairing closure persistence only.

Why:
The remaining defect was durable closure state, not product/runtime evidence. Rerunning Unity would add cost without addressing the actual control-layer issue.

Framework effect:
M1, M2, and M3 are HUMAN_PROMOTED. Active Unity mappings are exactly prefab inspect, serialized-fields inspect, playmode verify, and compile. Five remaining capabilities stay unbound.

Regression / follow-up:
M4 is not auto-started. The next milestone must be deliberately selected based on real System availability and accepted boundaries, while using the Luna/medium child policy.
