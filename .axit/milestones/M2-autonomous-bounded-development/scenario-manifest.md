# M2 Scenario Manifest

Status: **M2-done-pending-human-promotion**

Independent Phase-1 review approved all four 13/13-field contracts for freeze with no mandatory corrections. S1–S4 have independent terminal `PASS` results. The S4 fixture is restored and its cleanup debt is cleared. Frozen acceptance has not been rewritten. Independent closure verifier `/root/m2_closure_verifier` returned `PASS` with no findings and recommendation `PROMOTE`.

## Phase 0 readiness checkpoint

Status: **READY**

- Execution configuration recorded: primary `gpt-5.6-sol / max` (M2 plan expectation: `xhigh`); sub-agent `gpt-5.6-sol / max`. Exact live session metadata and service tier are unavailable.
- Multi-agent execution is proven available. The primary remains orchestrator-only.
- `.axit/workspace.yaml -> safety.check_git_status` is `false`; Git status is not a readiness gate.
- The source tree is readable and focused standalone C# test/stub mechanisms under `tests/QuickGun-MVP/` are available.
- Unity is ready: editor `QuickGun-MVP@83671098b43c28dd`, Unity `6000.4.8f1`, `MainScene`, idle in Edit Mode. Allowed `Player.prefab` inspection succeeded. Unity MCP remains manually configured and user-owned.
- Expected provenance is `local-or-separately-tracked` for current source/config/test evidence and `ephemeral-runtime` for live editor/runtime evidence.
- Blockers: none.
- `DEFERRED_PRIMARY_REASONING_CONFIG` remains visible and non-blocking for M2 only. It is unresolved because the configured primary reasoning differs from the plan expectation and exact live metadata is unavailable; this exception is prohibited from carrying into M3.

## Orchestration recovery record

- The discovery-candidate lane was steered, then interrupted without replacement because parallel evidence was sufficient.
- The original manifest-author lane was steered, then interrupted. This lane is manifest-author replacement **#1**.
- The first independent contract-review lane was steered, then interrupted and replaced once. Reviewer `/root/m2_contract_review_replacement` returned `APPROVE_FOR_FREEZE` with no mandatory corrections.
- Scenario repair loops: none. Fixture setup/restoration in S4 is not a repair loop.

## S1

Contract status: **FROZEN**

- `id`: `S1`
- `type`: `REAL`
- `category`: bug fix — `AudioSourcePool` empty-pool double lease
- `goal`: Correct the empty-pool path so two consecutive `Get()` calls without an intervening `Release()` lease distinct `AudioSource` instances, while a released instance becomes reusable.
- `acceptance-source`: The existing `AudioSourcePool.Get`/`Release` pooling API and its `_pool` versus `_active` ownership model in `src/QuickGun-MVP/Assets/QuickGunCore/Scripts/Services/AudioSourcePool.cs`.
- `accepted behavior`: With an empty available pool, consecutive unreleased leases are distinct. After one leased item is released, a later `Get()` may reuse that released item. Existing public API and architecture remain unchanged.
- `allowed files/scope`: `src/QuickGun-MVP/Assets/QuickGunCore/Scripts/Services/AudioSourcePool.cs`; focused standalone test files and only the stubs required by those tests under `tests/QuickGun-MVP/`.
- `forbidden scope`: `SoundService`; prefabs; scenes; packages/manifests; other product files; public API or architecture changes; unrelated pooling cleanup.
- `required evidence`: Deterministic focused proof that (1) two consecutive `Get()` calls from an empty pool return distinct instances and (2) a released instance is subsequently reusable; post-change source inspection confirming the lease path does not leave a newly created item simultaneously available and active.
- `supporting evidence`: Focused standalone compile of the implementation with its test/stub harness.
- `expected cleanup/restoration`: Dispose/remove any transient test objects or outputs; no editor/runtime state or product asset restoration is expected.
- `evidence provenance expected`: Source and focused test evidence: `local-or-separately-tracked`.
- `hard-blocker triggers`: The fix requires a public-contract/architecture change, an edit outside the allowed scope, or the same required behavior still fails after two bounded repair loops.
- `result`: `PASS — independently verified; repair loops 0; replacement attempts 0`

### S1 terminal checkpoint

- Worker: `/root/m2_s1_worker`.
- Independent verifier: `/root/m2_s1_verifier`; final verdict: `PASS`.
- Required evidence: consecutive exhausted-pool leases are distinct; an item becomes reusable after `Release`; newly created active sources are not enqueued; constructor prewarm behavior is preserved; public signatures and allowed scope are preserved.
- Deterministic execution: independent `mcs` compile plus `mono` run passed `2/2` focused tests against the actual production `AudioSourcePool.cs`.
- Scope evidence: scoped diff inspection was clean.
- Supporting evidence skipped: full Unity compile. Residual risk is limited to unobserved full-project Unity compilation; it was SUPPORTING rather than REQUIRED for the frozen contract.
- Files changed: `src/QuickGun-MVP/Assets/QuickGunCore/Scripts/Services/AudioSourcePool.cs`; `tests/QuickGun-MVP/AudioSourcePoolTests.cs`; `tests/QuickGun-MVP/UnityEngineAudioStubs.cs`.
- Evidence provenance: production/test files are `local-or-separately-tracked`; runtime commands are `ephemeral-runtime`.
- Repair loops: `0`. Replacement attempts: `0`.

## S2

Contract status: **FROZEN**

- `id`: `S2`
- `type`: `BENCHMARK`
- `category`: small test-only feature — countdown formatting
- `goal`: Add test-only `BenchmarkCountdownFormatter.Format(float)` under `tests/QuickGun-MVP/M2Benchmarks/`.
- `acceptance-source`: This benchmark contract itself, only after independent review and freeze. It is an M2 exercise contract, not accepted product intent.
- `accepted behavior`: Non-positive inputs format as invariant text `"0"`. Positive inputs are floored to invariant whole-second text; required examples include `30 => "30"` and `5.9 => "5"`.
- `allowed files/scope`: Only the formatter file and its focused test file(s) under `tests/QuickGun-MVP/M2Benchmarks/`.
- `forbidden scope`: Product source; Unity assets/scenes; packages/manifests; project/public contracts; product-facing countdown behavior; files outside the benchmark formatter/tests.
- `required evidence`: Deterministic focused tests for a non-positive input, an exact positive whole-second input, and a positive fractional input, including the required `30` and `5.9` outcomes and invariant formatting.
- `supporting evidence`: Focused standalone compile of the formatter and tests.
- `expected cleanup/restoration`: Remove transient test/build outputs; no product or Unity state is changed.
- `evidence provenance expected`: Benchmark formatter/test evidence: `local-or-separately-tracked`.
- `hard-blocker triggers`: Satisfying the contract requires a product/public change, a file outside the allowed benchmark scope, or the same required behavior still fails after two bounded repair loops.
- `result`: `PASS — independently verified; repair loops 0; replacement attempts 0`

### S2 terminal checkpoint

- Worker: `/root/m2_s2_worker`.
- Independent verifier: `/root/m2_s2_verifier`; final verdict: `PASS`.
- Required evidence: deterministic cases proved negative and zero inputs, positive fractional `0.9`, exact `30`, fractional `5.9`, invariant output under `fr-FR`, and benchmark-only scope.
- Deterministic execution: `mcs` with warnings as errors compiled successfully; `mono` passed `5/5` focused tests.
- Supporting evidence skipped: Unity/full-project compile. Residual risk is limited to unobserved full-project integration; it was SUPPORTING rather than REQUIRED for this test-only benchmark contract.
- Files changed: `tests/QuickGun-MVP/M2Benchmarks/BenchmarkCountdownFormatter.cs`; `tests/QuickGun-MVP/M2Benchmarks/BenchmarkCountdownFormatterTests.cs`.
- Evidence provenance: benchmark files are `local-or-separately-tracked`; runtime commands are `ephemeral-runtime`.
- Repair loops: `0`. Replacement attempts: `0`.
- Checkpoint timing: the verifier observed the expected stale `NOT_STARTED` result before this write; this checkpoint resolves it normally and it is not an incident.

## S3

Contract status: **FROZEN**

- `id`: `S3`
- `type`: `REAL`
- `category`: behavior-preserving refactor — timeout combat-result resolution
- `goal`: Extract the health comparison in `GamePlayState.OnTimerEnd` into one pure internal adjacent `CombatResultResolver.Resolve(playerHealth, botHealth)` without changing observable combat completion.
- `acceptance-source`: The QuickGun README timeout/hearts rule in `src/QuickGun-MVP/.github/README.md` and the existing `GamePlayState.OnTimerEnd` branch.
- `accepted behavior`: Greater player health resolves to Victory (`ECombatResult.PlayerWin`), equal health to Draw, and lower player health to Lose (`ECombatResult.BotWin`). `OnTimerEnd` delegates once to the resolver and then preserves all existing `EndCombat` effects, counters, transitions, payloads, and public behavior.
- `allowed files/scope`: `src/QuickGun-MVP/Assets/QuickGunCore/Scripts/GameStateMachine/States/GamePlayState.cs`; one adjacent `CombatResultResolver` source file; one focused test file under `tests/QuickGun-MVP/`.
- `forbidden scope`: Unrelated state-machine code; `EndCombat` behavior; transition targets/payloads; public models/contracts; entity/combat behavior; broad cleanup or architecture changes.
- `required evidence`: Three deterministic resolver tests covering greater/equal/less health; static source inspection proving `OnTimerEnd` contains exactly one resolver delegation wired to one `EndCombat` call and no duplicate health-result branch.
- `supporting evidence`: Focused standalone compile of resolver/tests and the relevant refactored source surface when the available harness permits it.
- `expected cleanup/restoration`: Remove transient test/build outputs; no runtime/editor restoration is expected.
- `evidence provenance expected`: Source, README, and focused test evidence: `local-or-separately-tracked`.
- `hard-blocker triggers`: Preservation requires changing `EndCombat`, transitions, public models/contracts, architecture, or other forbidden files; or the same required behavior/wiring still fails after two bounded repair loops.
- `result`: `PASS — independently verified; repair loops 0; replacement attempts 0`

### S3 terminal checkpoint

- Worker: `/root/m2_s3_worker`.
- Independent verifier: `/root/m2_s3_verifier`; final verdict: `PASS`.
- Required deterministic evidence: the actual production `ECombatResult` enum plus `CombatResultResolver` compiled with warnings as errors, and `mono` passed `3/3` comparison tests. Mapping remains greater -> `PlayerWin`, equal -> `Draw`, less -> `BotWin`.
- Required wiring evidence: `OnTimerEnd` retains the same player/bot health reads and contains exactly one `CombatResultResolver.Resolve` call wired to exactly one `EndCombat` call. `EndCombat`, counters, transitions, transition payload, and public models remain unchanged.
- Scope evidence: exactly three files changed — `src/QuickGun-MVP/Assets/QuickGunCore/Scripts/GameStateMachine/States/GamePlayState.cs`, adjacent `src/QuickGun-MVP/Assets/QuickGunCore/Scripts/GameStateMachine/States/CombatResultResolver.cs`, and `tests/QuickGun-MVP/CombatResultResolverTests.cs`.
- Supporting evidence skipped: full Unity/project compile and standalone full `GamePlayState` compile. Residual risk is limited to unobserved full-project integration; both were SUPPORTING rather than REQUIRED for the frozen contract.
- Evidence provenance: production/test files are `local-or-separately-tracked`; runtime commands are `ephemeral-runtime`.
- Repair loops: `0`. Replacement attempts: `0`.

## S4

Contract status: **FROZEN**

- `id`: `S4`
- `type`: `BENCHMARK`
- `category`: Unity-backed controlled restoration fixture
- `goal`: Exercise bounded Unity restoration against the accepted Player prefab multipliers. Only after contract freeze, a separate setup lane may change Player/Hair `damageMultiplier` from `2` to `1`; the implementation restores Hair to `2` while leaving Body at `1`.
- `acceptance-source`: Accepted Unity-client ADR `hit-zone-multipliers` in `.axit/systems/unity-client/architecture.yaml`: Head/Hair `2x`, Body `1x`; the current Player prefab is the concrete fixture. This is an explicit benchmark fixture, not a natural defect or a manufactured repair loop.
- `accepted behavior`: Final serialized Player/Hair multiplier is `2` and Player/Body remains `1`. A freshly resolved Bot/Hair runtime target exercises the restored `2x` behavior through the reviewed binding, and the editor finishes in Edit Mode.
- `allowed files/scope`: Only `src/QuickGun-MVP/Assets/QuickGunCore/Prefabs/Player.prefab`. The post-freeze setup edit and restoration edit are both limited to the single Hair multiplier field.
- `forbidden scope`: C# or other source files; any other prefab/asset; scenes; packages/manifests; binding or Capability definitions; public/architecture changes; arbitrary MCP operations or caller-supplied code; user-owned MCP setup/repair.
- `required evidence`: Fresh post-restoration acquisition through exactly the allowed bound capabilities `unity.prefab.inspect`, `unity.serialized-fields.inspect`, and `unity.playmode.verify`; serialized evidence for Player/Hair `2` and Player/Body `1`; freshly resolve the current Bot/Hair runtime hierarchy path/target rather than reuse stale runtime identity; observe the bounded runtime behavior; mandatory Play Mode stop and confirmation of Edit Mode cleanup.
- `supporting evidence`: ADR/source inspection and a bounded before/setup/after fixture record showing only Player/Hair changed `2 -> 1 -> 2` and Body remained `1`.
- `expected cleanup/restoration`: `Player.prefab` ends with Hair `2`, Body `1`; all runtime mutation is discarded; Unity ends idle in Edit Mode. Cleanup is mandatory even if acquisition fails.
- `evidence provenance expected`: ADR/prefab/fixture evidence: `local-or-separately-tracked`; live editor inspection and Play Mode observations: `ephemeral-runtime`.
- `hard-blocker triggers`: `UNITY_MCP_NOT_READY`; pre-existing Play Mode cannot be safely restored; fresh Bot/Hair target cannot be resolved after bounded readiness recovery; restoration requires any forbidden operation/file or a new binding/Capability; or the same required failure persists after two bounded repair loops.
- `result`: `PASS — independently verified; scenario repair loops 0; candidate replacements 0`

### S4 fixture setup checkpoint

- Setup lane: `/root/m2_s4_fixture_setup`.
- Accepted baseline: Player Hair `2`, Body `1`, matching the accepted ADR.
- Controlled setup: changed only Player/Hair `damageMultiplier` from `2 -> 1` in `src/QuickGun-MVP/Assets/QuickGunCore/Prefabs/Player.prefab`.
- Post-setup serialized state: Head `2`, Hair `1`, Body `1`.
- Scope evidence: the scoped diff contains exactly one scalar change and the diff check is clean.
- Runtime/editor activity: no Unity or Play Mode operation was invoked by setup.
- Classification: benchmark fixture setup, not a natural defect and not a scenario repair loop. S4 remains `NOT_STARTED` and is not execution-complete.
- Mandatory cleanup/restoration debt recorded at setup: restore Hair to `2`, keep Body at `1`, discard any later runtime mutation, and finish in Edit Mode. The terminal checkpoint below records this debt as cleared.

### S4 terminal checkpoint

- Setup lane: `/root/m2_s4_fixture_setup`; restoration worker: `/root/m2_s4_worker`; independent verifier: `/root/m2_s4_verifier`; final verdict: `PASS`.
- Persistent restoration scope: only `src/QuickGun-MVP/Assets/QuickGunCore/Prefabs/Player.prefab` changed Hair `1 -> 2`. Final serialized state is Body `1`, Head `2`, Hair `2`.
- Required `unity.prefab.inspect` evidence: acquired the concrete prefab GUID and hierarchy paths.
- Required `unity.serialized-fields.inspect` evidence: fixed reviewed CodeDom acquisition observed Body `1`, Head `2`, Hair `2`.
- Runtime-target derivation: the verifier freshly derived `GameEnvironment/Bot/Model/Head/Hair` through local read-only scene/source inspection. This derivation is ordinary evidence and explicitly **not** an Axit Capability.
- Required fixed `unity.playmode.verify` evidence: requested and observed path both `GameEnvironment/Bot/Model/Head/Hair`; target instance `-4264`; owner `Bot` / `QG.Entity.BotEntity`, instance `38850`; multiplier `2`; base damage `10`; pre-armor damage `20`; armor `0.8`; expected and observed final damage `4`; health `3 -> -1`; `completed=true`; `callReturned=true`.
- Required cleanup evidence: final editor state is idle Edit Mode and ready. Runtime mutation was discarded; Hair `2`/Body `1` restoration debt is cleared.
- Evidence provenance: prefab/source evidence is `local-or-separately-tracked`; live acquisition/runtime evidence is `ephemeral-runtime`.
- Scenario repair loops: `0`. Candidate replacements: `0`.

#### S4 evidence-acquisition recovery incident

- The worker's prefab-derived `Bot/Model/Head/Hair` attempt and one bounded retry returned `target_not_found` with no mutation; the worker performed mandatory cleanup.
- The fresh verifier derived the corrected `GameEnvironment/`-prefixed runtime path and acquired required evidence on its first attempt.
- This is a real evidence-acquisition recovery, not a scenario repair loop. The same missing runtime-wrapper assumption occurred in M1, so it is recurring and must be classified in Phase 7. Do not change the binding or Capability catalog as part of this checkpoint.
- Supporting cleanup observer `/root/m2_s4_cleanup_observer` returned `UNAVAILABLE` after one read because of response-wrapper parsing, with no mutation. Direct verifier cleanup evidence remained REQUIRED and was proven, so the supporting observer had no verdict impact.

## Phase 1 freeze checkpoint

Status: **APPROVED_AND_FROZEN**

- Independent result: `APPROVE_FOR_FREEZE`; mandatory corrections: none.
- Review coverage: S1–S4 each contain all 13 required fields, use valid REAL/BENCHMARK intent, have bounded evidence/scope, and are materially distinct.
- S4 remains a controlled benchmark setup/restoration fixture, not a scenario repair loop; its fixture is unapplied.
- At freeze, all results were `NOT_STARTED`; scenario repair loops were none.

## Context continuity checkpoint

- Agent: `/root/m2_continuity_probe`.
- Result: `Continuity: PASS`.
- Restricted inputs were exactly `.axit/workspace.yaml`, `.axit/state/active.md`, `.axit/milestones/M2-autonomous-bounded-development/plan.md`, and `.axit/milestones/M2-autonomous-bounded-development/scenario-manifest.md`; no hidden chat, product-source, or Git context was supplied.
- Reconstruction covered the M2 objective/current phase; S1/S2 `PASS` evidence and provenance; S3 as next action; recovery and incident state; stable Core v1, Workspace/System v1, Capability semantic v1, and the M1 three-capability binding; `DEFERRED_PRIMARY_REASONING_CONFIG`; S4 still unapplied with mandatory Edit Mode cleanup; and the M3 prohibition.
- State quality: no missing state, stale checkpoint, or contradiction found.

## Phase 7 cross-scenario analysis checkpoint

- Analyzer: `/root/m2_cross_scenario_analysis`.
- Result: `Phase7: PASS`; automation disposition: `NO_AUTO_APPLY`.
- Recurring M1 + S4 missing `GameEnvironment/` runtime wrapper: classified as real evidence-acquisition recovery and an accepted limitation requiring `HUMAN_REVIEW` for M3. Adding target diagnostics/enumeration would alter the frozen Runtime Binding/Capability boundary, so no automatic binding or Capability change is allowed.
- Manifest-author and independent contract-review stalls: recurring orchestration recovery handled successfully by the existing steer/replace limits; no new Core Workflow is justified.
- Candidate-discovery interruption: one-off, with parallel evidence already sufficient; no hardening required.
- S2 verifier's stale `NOT_STARTED`: normal checkpoint timing, not an incident.
- Cleanup-observer response-wrapper parse failure: one-off SUPPORTING observer limitation; no verdict or required-evidence impact.
- S1–S3 skipped full Unity/project builds: recurring SUPPORTING limitation. Consider an M3 compile binding only if future frozen acceptance makes compilation REQUIRED; do not expand the binding now.
- Exact live primary/session metadata and service tier remain unavailable.
- `DEFERRED_PRIMARY_REASONING_CONFIG`: `HUMAN_REVIEW` before M3.
- Official documentation check: the [GPT-5.6 latest-model guide](https://developers.openai.com/api/docs/guides/latest-model) supports `max`, while the [Antigravity configuration reference](https://developers.openai.com/antigravity/config-reference/) lists only `minimal`, `low`, `medium`, `high`, and `xhigh` for `model_reasoning_effort` and redirects to official ChatGPT Learn content.
- Configuration disposition: do not auto-edit `.agents/mcp_config.json`. At human promotion review, either change the project setting from `max` to `xhigh` and start a fresh trusted task before M3, or explicitly re-decide the setting before M3.
- Decision log: no append, because no durable rule or configuration change was applied.
- Promotion threshold: met; independent closure verification returned `PASS`.

## Phase 8 closure checkpoint

Status: **M2-done-pending-human-promotion**

- `report.md` records `Status: DONE`, `Review state: pending-human-review`, independent closure verification `PASS`, and recommendation `PROMOTE`.
- `retrospective.md` records milestone result `PASS`, closure verifier `PASS`, and recommendation `PROMOTE`.
- Results remain exactly four independent passes: `S1=PASS`, `S2=PASS`, `S3=PASS`, `S4=PASS`; continuity is `PASS`.
- Scenario repair loops remain `0`; candidate replacements remain `0`; sub-agent replacements remain `2`.
- Evidence remains classified only as `local-or-separately-tracked` or `ephemeral-runtime`; no `canonical-pushed` claim was added.
- Phase 7 remains `NO_AUTO_APPLY`; no Core/Profile/Skill/Workflow/Capability/Binding/configuration/decision-log mutation occurred.
- `DEFERRED_PRIMARY_REASONING_CONFIG` remains unresolved and requires human repair to `xhigh` plus a fresh trusted task, or explicit human re-decision, before M3.
- Closure verifier `/root/m2_closure_verifier` returned `PASS` with C1–C10 `PROVEN`, no findings, and recommendation `PROMOTE`.
- Human promotion remains pending. Technical closure does not authorize M3.

Next action: human M2 promotion and `DEFERRED_PRIMARY_REASONING_CONFIG` review only. M3 remains explicitly prohibited and not started.
