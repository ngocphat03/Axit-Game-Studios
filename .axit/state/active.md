# Axit Workspace Active State

Updated: 2026-08-10
Status: M2-done-pending-human-promotion

## Current milestone

```text
M2 — Autonomous Bounded Development
```

Execution plan:

```text
.axit/milestones/M2-autonomous-bounded-development/plan.md
```

## Promotion state

### M1 — Unity Runtime Binding

Status: **HUMAN_PROMOTED** on 2026-08-10.

Canonical closure artifacts:

```text
.axit/milestones/M1-unity-runtime-binding/report.md
.axit/milestones/M1-unity-runtime-binding/retrospective.md
```

Proven stable Runtime Binding:

```text
.axit/bindings/unity-client/coplaydev-unity-mcp.yaml
```

Mapped capabilities remain exactly:

- `unity.prefab.inspect`
- `unity.serialized-fields.inspect`
- `unity.playmode.verify`

The remaining six declared Unity capabilities remain explicitly unbound.

### M2 — Autonomous Bounded Development

Status: **M2-done-pending-human-promotion**.

The report and retrospective record `PROMOTE`. Independent closure verifier `/root/m2_closure_verifier` returned `PASS` with C1–C10 `PROVEN` and no findings. Human promotion and reasoning-config review are now the only next actions; M3 remains prohibited and not started.

## M2 goal

Prove Axit can autonomously complete several materially different bounded development tasks without routine user steering.

M2 freezes four scenario contracts before implementation:

- S1 bug fix;
- S2 small feature;
- S3 behavior-preserving refactor;
- S4 Unity-backed bounded scenario.

Prefer REAL scenarios with explicit accepted behavior. Controlled BENCHMARK fallback is allowed only when no safe real candidate has sufficiently explicit intent.

## M2 execution checkpoint

Phase 0 status: **READY**.

- Scenario manifest: `.axit/milestones/M2-autonomous-bounded-development/scenario-manifest.md`.
- Manifest closure state: **M2-done-pending-human-promotion**. All S1–S4 contracts remain frozen after independent `APPROVE_FOR_FREEZE`; mandatory corrections: none.
- Scenario state: `S1=PASS`, `S2=PASS`, `S3=PASS`, `S4=PASS`. The S4 fixture is restored; its cleanup debt is cleared.
- Configured execution metadata: primary `gpt-5.6-sol / max` (M2 plan expectation: `xhigh`); sub-agent `gpt-5.6-sol / max`. Exact live session metadata and service tier are unavailable.
- Multi-agent execution is proven. Source/test mechanisms are ready.
- Unity readiness: `QuickGun-MVP@83671098b43c28dd`, Unity `6000.4.8f1`, `MainScene`, idle Edit Mode; allowed Player prefab inspection succeeded.
- Provenance expectation: source/config/test evidence is `local-or-separately-tracked`; live editor/runtime evidence is `ephemeral-runtime`.
- Readiness blockers: none.
- Phase 1 review: all four contracts have 13/13 required fields, valid intent/fallback classification, bounded scope/evidence, and materially distinct categories. S4 setup/restoration is a benchmark fixture, not a repair loop.
- S1 terminal checkpoint: worker `/root/m2_s1_worker`; independent verifier `/root/m2_s1_verifier`; final `PASS`; repair loops `0`; replacement attempts `0`. Required distinct-lease/release-reuse and source-wiring evidence passed, including independent `mcs` + `mono` `2/2` against production source and a clean scoped diff inspection. Full Unity compile was skipped as SUPPORTING residual risk. Changed files are `AudioSourcePool.cs`, `AudioSourcePoolTests.cs`, and `UnityEngineAudioStubs.cs`; file evidence is `local-or-separately-tracked`, runtime commands are `ephemeral-runtime`.
- S2 terminal checkpoint: worker `/root/m2_s2_worker`; independent verifier `/root/m2_s2_verifier`; final `PASS`; repair loops `0`; replacement attempts `0`. Required negative/zero, `0.9`, `30`, `5.9`, `fr-FR` invariant, and benchmark-only evidence passed; `mcs` warnings-as-errors plus `mono` passed `5/5`. Unity/full-project compile was skipped as SUPPORTING with limited integration residual risk. Changed files are `BenchmarkCountdownFormatter.cs` and `BenchmarkCountdownFormatterTests.cs`; file evidence is `local-or-separately-tracked`, runtime commands are `ephemeral-runtime`. The verifier's expected stale `NOT_STARTED` observation preceded this normal checkpoint and is not an incident.
- S3 terminal checkpoint: worker `/root/m2_s3_worker`; independent verifier `/root/m2_s3_verifier`; final `PASS`; repair loops `0`; replacement attempts `0`. Production enum/resolver warnings-as-errors compilation and `mono` `3/3` proved the `PlayerWin`/`Draw`/`BotWin` mapping. Static wiring preserved the same health reads with exactly one `Resolve` and one `EndCombat`; `EndCombat`, counters, transitions, payload, and public models are unchanged. Scope was exactly `GamePlayState.cs`, adjacent `CombatResultResolver.cs`, and `CombatResultResolverTests.cs`. Full Unity/project and standalone full `GamePlayState` compilation were skipped as SUPPORTING residual risk. File evidence is `local-or-separately-tracked`; runtime commands are `ephemeral-runtime`.
- S4 fixture setup checkpoint: lane `/root/m2_s4_fixture_setup`; accepted Player Hair `2`/Body `1` baseline matched the ADR; setup changed only Player/Hair `2 -> 1` in `Player.prefab`; post-state is Head `2`, Hair `1`, Body `1`. Scoped diff is exactly one scalar and clean. No Unity or Play Mode operation ran. This is benchmark setup, not a natural defect or repair loop. Cleanup debt is explicit: restore Hair to `2`, leave Body at `1`, discard runtime mutation, and finish in Edit Mode.
- S4 terminal checkpoint: setup `/root/m2_s4_fixture_setup`; worker `/root/m2_s4_worker`; independent verifier `/root/m2_s4_verifier`; final `PASS`; scenario repair loops `0`; candidate replacements `0`. Only Player/Hair was restored `1 -> 2`; final serialized Body/Head/Hair is `1/2/2`. Bound prefab and fixed CodeDom serialized evidence passed. Fresh local read-only scene/source derivation — ordinary evidence, not a Capability — resolved `GameEnvironment/Bot/Model/Head/Hair`; Play Mode requested/observed that path with target `-4264`, Bot/QG.Entity.BotEntity `38850`, multiplier `2`, base/pre-armor/final damage `10/20/4`, armor `0.8`, health `3 -> -1`, and `completed=true`/`callReturned=true`. Required cleanup proved idle ready Edit Mode. Prefab/source evidence is `local-or-separately-tracked`; runtime evidence is `ephemeral-runtime`.
- S4 evidence incident: worker path `Bot/Model/Head/Hair` plus one bounded retry returned `target_not_found` without mutation and cleanup succeeded; the verifier freshly derived the corrected `GameEnvironment/` wrapper and acquired evidence first attempt. This is evidence-acquisition recovery, not a scenario repair loop. The same missing runtime wrapper occurred in M1 and is recurring for Phase 7 classification; no binding/Capability change is authorized here.
- Supporting cleanup observer `/root/m2_s4_cleanup_observer`: `UNAVAILABLE` after one read due response-wrapper parsing, with no mutation. Direct verifier cleanup evidence remained REQUIRED and proven, so there was no verdict impact.
- Context continuity probe: `PASS` by `/root/m2_continuity_probe`. Inputs were restricted exactly to `workspace.yaml`, `active.md`, M2 `plan.md`, and `scenario-manifest.md`, with no hidden chat/product/Git context. It reconstructed objective/phase, S1/S2 outcomes and provenance, S3 next action, recovery/incidents, stable Core/System/Capability/M1 three bindings, the deferred reasoning issue, S4 unapplied/Edit Mode cleanup, and the M3 prohibition; it found no missing, stale, or contradictory state.
- Phase 7 analysis: `/root/m2_cross_scenario_analysis` returned `PASS` with `NO_AUTO_APPLY`. The recurring M1/S4 missing `GameEnvironment/` wrapper is accepted evidence-acquisition recovery and `HUMAN_REVIEW` for M3 because diagnostics/enumeration would change the frozen binding/Capability boundary. Manifest-author/reviewer stalls were handled by existing steer/replacement limits with no new workflow; candidate discovery was one-off; S2 stale state was normal checkpoint timing; cleanup-observer parsing was one-off SUPPORTING; skipped S1–S3 full Unity builds are a recurring SUPPORTING limitation, with a possible M3 compile binding only if future evidence makes it REQUIRED.
- Reasoning/config analysis: exact live metadata and service tier remain unavailable; `DEFERRED_PRIMARY_REASONING_CONFIG` is `HUMAN_REVIEW`. Official docs record GPT-5.6 `max` support at `https://developers.openai.com/api/docs/guides/latest-model`, while `model_reasoning_effort` lists only `minimal/low/medium/high/xhigh` at `https://developers.openai.com/codex/config-reference/` (redirecting to official ChatGPT Learn). Do not auto-edit `.codex/config.toml`; promotion review must either change project `max -> xhigh` and start a fresh trusted task, or explicitly re-decide before M3.
- Phase 7 persistence: no decision-log append because no durable rule/config change was applied. Promotion threshold is met.
- Phase 8 closure: `report.md` and `retrospective.md` are persisted with scenario `PASS`/continuity `PASS`, `PROMOTE` recommendation, explicit provenance and residual risks. Closure verifier `/root/m2_closure_verifier` returned `PASS` with C1–C10 `PROVEN`, fresh S1/S2/S3 reruns `2/2`/`5/5`/`3/3`, current S4 idle Edit Mode and serialized `1/2/2`, and no findings.
- Orchestration recovery: the discovery-candidate lane was steered then interrupted without replacement because parallel evidence sufficed; the original manifest-author lane was steered then interrupted and replaced once; the first independent contract-review lane was steered then interrupted and replaced once before `/root/m2_contract_review_replacement` approved freeze.
- Scenario repair loops: none. Controlled S4 setup/restoration is not a repair loop.

## M2 operating rules

- primary thread orchestrates only when sub-agents are available;
- workers implement; independent verifiers decide PASS/FAIL/BLOCKED;
- maximum two bounded repair/replacement attempts for the same required failure/lane;
- `safety.check_git_status: false` means Git status is not a readiness gate;
- do not add Core Profile/Skill/Workflow, semantic Capability ids, or additional Runtime Binding mappings during M2;
- small proven non-architectural pipeline hardening may be applied with regression protection;
- write compact scenario progress to the M2 scenario manifest and this active state;
- closure must persist M2 report + retrospective and pass closure verification;
- stop after M2 closure for human promotion review; do not start M3 automatically.

## Deferred issue accepted for M2 only

```text
DEFERRED_PRIMARY_REASONING_CONFIG
```

Observed during M1:

- effective primary session: `gpt-5.6-sol / medium`;
- effective sub-agents: `gpt-5.6-sol / max`.

The user explicitly chose to prioritize M2 before repairing the primary reasoning mismatch.

M2 must:

- record the effective values during readiness;
- continue even if this same known mismatch persists;
- keep the issue visible in report/retrospective;
- revisit it at M2 promotion review;
- never silently mark it resolved or carry the exception into M3.

## Stable foundations

Keep unchanged unless M2 produces a demonstrated material flaw:

- Core v1: four Profiles, two Skills, one Workflow;
- Workspace/System v1 root-first routing;
- Capability semantic v1;
- M1 three-capability Unity Runtime Binding;
- manual/user-owned Unity MCP setup;
- milestone closure contract;
- evidence provenance distinction: canonical-pushed vs local/separately-tracked vs ephemeral-runtime.

## Next action

Stop for human M2 promotion and `DEFERRED_PRIMARY_REASONING_CONFIG` review only. Before M3, the human must repair to `xhigh` and start a fresh trusted task or explicitly re-decide the reasoning setting. Do not start M3.
