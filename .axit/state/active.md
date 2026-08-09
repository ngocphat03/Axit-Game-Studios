# Axit Workspace Active State

Updated: 2026-08-10
Status: DONE

## Current task

The Runtime Binding v1 continuous multi-agent plan is complete.

Canonical continuous plan:

```text
.axit/plans/continuous-runtime-binding-v1.md
```

## Completion summary

- Status: **DONE**.
- Phases completed: **0–8**.
- Transport: the user-configured `unityMCP` transport backed by the CoplayDev `unity-mcp` adapter.
- Binding: `coplaydev-unity-mcp` is active for exactly `unity.prefab.inspect`, `unity.serialized-fields.inspect`, and `unity.playmode.verify`; the other six declared Unity capabilities remain explicitly unbound.
- Vertical-slice verifier: **PASS**.
- Recovery iterations: **1**, with a real `FAIL -> repair -> reacquire -> PASS` path.
- Documentation cleanup: **PASS**.
- Remaining blockers: **none**.
- Next recommended step: **none**.

## Current progress

- Phase 0 — Baseline and execution readiness: **PASS** on 2026-08-09.
- Phase 1 — Manual Unity MCP readiness gate: **PASS** on 2026-08-09.
- Phase 2 — Real transport discovery: **COMPLETE** on 2026-08-09; no hard blocker was found.
- Phase 3 — Materialize Runtime Binding v1: **COMPLETE** on 2026-08-09; the independent verifier returned **PASS** for the validation binding and Unity capability sidecar reference.
- Phase 4 — Runtime Binding acquisition regression: **COMPLETE** on 2026-08-10; the independent verifier returned **PASS** for preservation of the acquisition-state boundary.
- Phase 5 — First end-to-end evidence vertical slice: **COMPLETE** on 2026-08-10; the independent verifier returned **PASS** for the bounded QuickGun Head damage criterion.
- Phase 6 — Bounded failure/recovery validation: **COMPLETE** on 2026-08-10; a fresh independent verifier returned **PASS** after the required real `FAIL -> repair -> reacquire -> reverify` path.
- Phase 7 — Promote reviewed binding: **COMPLETE** on 2026-08-10; the independent verifier returned **PASS** for the exact proven semantic slice.
- Phase 7 documentation cleanup: **COMPLETE** on 2026-08-10; the independent verifier returned **PASS**.
- Phase 8 — Demonstrated-gap analysis: **COMPLETE** on 2026-08-10; no additional capability binding, System Rule, Knowledge, or Core extension is justified by the completed flow.
- During live validation, the user-configured `unityMCP` server was reachable through namespace `mcp__unityMCP__` over HTTP and exposed resources through the `mcpforunity://` scheme.
- Live validation used the manually configured `unityMCP` server backed by the CoplayDev `unity-mcp` adapter.
- Live validation reached one intended QuickGun-MVP Unity instance rooted at `src/QuickGun-MVP`, running Unity `6000.4.8f1`, ready for tools, with no observed errors; no instance or session identifier is persisted.
- Live validation confirmed that the concrete Unity operations were present and inspectable. The observed `unity_bridge_connected: null` was non-blocking because instance, project, and editor reads succeeded in that validation run.
- Workspace safety resolved to `safety.check_git_status: false`; Git status was skipped for the root, nested repositories, and submodules.
- Trusted project configuration loaded from `.codex/config.toml`.
- Effective primary session: `gpt-5.6-sol` / `medium`; this differs from the project `max` default but is non-blocking for orchestration.
- Sub-agent runtime: `gpt-5.6-sol` / `max`; multi-agent execution is available.
- Core v1, Workspace/System v1, and Capability semantic v1 remain stable.
- `src/QuickGun-MVP` maps to System `unity-client`.
- Runtime Binding v1 is materialized at `.axit/bindings/unity-client/coplaydev-unity-mcp.yaml` with reviewed definition status `active` and is referenced by `.axit/systems/unity-client/capabilities.yaml`.
- Exactly `unity.prefab.inspect`, `unity.serialized-fields.inspect`, and `unity.playmode.verify` are mapped. The other six declared Unity capabilities remain explicitly unbound.
- Read-only live verifier probes confirmed the concrete operation contracts and response normalization without mutating source, assets, scenes, or Play Mode state.
- No secret, endpoint, port, session identity, live instance hash, live timestamp, or other ephemeral machine value is persisted in the binding, sidecar, or Phase 7 checkpoint.
- Phase 6 ended with the Unity Editor safely returned to Edit Mode.
- Recovery iterations: **1**.

## Phase 2 transport-discovery checkpoint

Proposed mappings for Phase 3, using only live-verified `unityMCP`/CoplayDev operations:

- `unity.prefab.inspect` -> ordered `manage_prefabs(action="get_info")`, then `manage_prefabs(action="get_hierarchy")` for the same prefab target.
- `unity.serialized-fields.inspect` -> constrained read-only `execute_code` with `compiler="codedom"` and `safety_checks=true`, using a fixed `AssetDatabase` + `SerializedObject` inspection template. Roslyn is unavailable in the observed transport; normalize acquisition success from `structuredContent.success` rather than assuming top-level success.
- `unity.playmode.verify` -> read `mcpforunity://editor/state`; call `manage_editor(action="play")`; poll editor state until Play Mode is confirmed; run one bounded runtime-only `execute_code` scenario with `compiler="codedom"` and `safety_checks=true` that requires `Application.isPlaying`; and, in unconditional cleanup, call `manage_editor(action="stop")` and poll until Play Mode is stopped.

Live discovery evidence highlights:

- Prefab inspection resolved `Assets/QuickGunCore/Prefabs/Player.prefab`, GUID `9b0e4fcb353ba4a4281c4dc04df46b53`, with 9 hierarchy objects; serialized Body/Head/Hair damage multipliers were `1`/`2`/`2`.
- The bounded Play Mode `Bot` Head scenario observed multiplier `2`, armor `0.8`, and health `3 -> 1` from base damage `5`.

Mapping boundaries remain unchanged:

- `unity.prefab.inspect` and `unity.serialized-fields.inspect` remain `inspect` / `read-only`; they establish only the concrete serialized hierarchy, component, value, and reference facts actually inspected, not runtime behavior or uninspected overrides/state.
- `unity.playmode.verify` remains `execute` / `controlled-runtime`; it establishes only behavior directly exercised and observed by the bounded scenario, not unrelated paths or broad gameplay correctness.
- The CodeDom templates must not mutate assets or source. Runtime/Harness authorization and `verify-change` verdict ownership remain outside the binding.
- Persist the adapter and verified operation contract only. Do not persist a live instance hash, MCP session identity, endpoint, port, or other ephemeral machine state in the binding.

## Phase 3 binding-materialization checkpoint

- Binding definition: `.axit/bindings/unity-client/coplaydev-unity-mcp.yaml`.
- System sidecar reference: `.axit/systems/unity-client/capabilities.yaml`.
- Definition status: `validation`; this records a reviewed mapping and does not claim current transport connectivity.
- Mapped capabilities (exactly three):
  - `unity.prefab.inspect`;
  - `unity.serialized-fields.inspect`;
  - `unity.playmode.verify`.
- Explicitly unbound capabilities (remaining six):
  - `unity.project.inspect`;
  - `unity.compile`;
  - `unity.tests.run`;
  - `unity.scene.inspect`;
  - `unity.component.inspect`;
  - `unity.console.inspect`.
- Independent verifier result: **PASS** for Phase 3 materialization.
- Static validation confirmed the binding and sidecar parse, mapped IDs exist in the active Unity capability set, the mapped/unbound partition is complete and non-overlapping, and the status/reference values agree.
- Read-only live verifier probes confirmed the persisted concrete operation contracts and `structuredContent`/resource-payload normalization; they did not invoke the binding's Play Mode mutation path.
- Secret/ephemeral scan confirmed that no credential, endpoint, port, session identifier, live instance hash, or instance-id evidence value was persisted.
- A fresh controlled Play Mode execution remains deferred to Phase 4 as **SUPPORTING** acquisition evidence; Phase 3 verification did not use that deferred rerun as a required acceptance condition.

## Phase 4 acquisition-regression checkpoint

- Independent verifier result: **PASS** for Phase 4 acquisition-state semantics.
- Live `acquired` evidence resolved the intended QuickGun Player prefab and its serialized Body/Head/Hair damage multipliers as `1`/`2`/`2`.
- The observed serialized `ownerEntity: null` value remains trustworthy `acquired` evidence. It is an observed target value, not missing evidence and not a product verdict.
- The explicitly unbound `unity.console.inspect` capability demonstrated `unavailable`: no mapped runtime operation exists for that Capability, and this missing-evidence state is not product failure.
- Safe bounded fixtures validated the `denied` and `transport_error` classifications only. They do not claim a current live Runtime/Harness denial or a current live Unity MCP outage.
- None of `acquired`, `unavailable`, `denied`, or `transport_error` was presented as `PASS`, `FAIL`, or `BLOCKED`; those verdicts remain owned by criterion-aware `verify-change` reasoning.
- Final editor state was confirmed safe in Edit Mode.
- Phase 4 acceptance was satisfied sufficiently to begin Phase 5's first real Unity evidence vertical slice.

## Phase 5 first-evidence-vertical-slice checkpoint

- Independent verifier result: **PASS** for the bounded QuickGun Head damage vertical slice.
- Standalone deterministic evidence compiled with warnings as errors and passed **16/16** tests; the exact armored-head assertion confirmed `DamageCalculator.Calculate(5, 2f, 0.8f) == 2`.
- Mapped prefab inspection acquired `Assets/QuickGunCore/Prefabs/Player.prefab`, GUID `9b0e4fcb353ba4a4281c4dc04df46b53`, with the `Player` root and 9-object hierarchy containing Body, Head, and Hair `DamageableBodyPart` components.
- Mapped serialized-field inspection acquired matching component/script and renderer references with Body/Head/Hair damage multipliers `1`/`2`/`2`; serialized `ownerEntity: null` remained an observed value rather than missing evidence.
- Fresh bounded Play Mode evidence resolved exact runtime target `GameEnvironment/Bot/Model/Head`, owner `Bot` / `QG.Entity.BotEntity`, multiplier `2`, armor `0.8`, and health `3 -> 1` for observed damage `2` after the damage call returned.
- Runtime evidence was traceable to the sole intended QuickGun instance, intended project root, active `MainScene`, exact prefab GUID/hierarchy, and ephemeral target/owner instance identifiers.
- Mandatory cleanup succeeded; a fresh final state confirmed safe Edit Mode, idle, ready for tools, with `MainScene` unchanged.
- Production `expectedHitZoneDamage` and `expectedFinalDamage` fields were treated as diagnostics only, not as the verification oracle; the independent oracle was the deterministic exact-value assertion plus the directly observed health transition.
- Residual risk is bounded to this one synchronous Head damage call: projectile collision delivery, asynchronous/visual effects, broad combat correctness, and other hit zones were not established. Runtime instance identifiers are intentionally ephemeral, so reproducible identity rests on the project, scene, prefab GUID, hierarchy path, and owner type.
- Phase 6 begins with **0** recovery iterations and must demonstrate one bounded `FAIL -> repair -> reacquire -> reverify` path without reusing stale evidence.

## Phase 6 bounded-failure-recovery checkpoint

- One controlled project-local mismatch changed only the Player prefab Head damage multiplier from `2` to `1`.
- Fresh post-change acquisition observed serialized Head multiplier `1` and the exact runtime target with multiplier `1`, armor `0.8`, health `3 -> 2`, and observed damage `1`.
- An independent verifier returned **FAIL** against the already accepted armored-Head criterion.
- A separate bounded repair restored only the Head damage multiplier from `1` to `2`; it did not revert or alter unrelated current source state.
- Evidence was reacquired after repair rather than reusing stale pre-repair results: serialized Head multiplier `2` and the exact runtime target with health `3 -> 1` and observed damage `2`.
- A fresh independent verifier returned **PASS** on the repaired current state.
- The exact required `FAIL -> repair -> reacquire -> reverify` path completed in recovery iteration **1**.
- Final editor state was confirmed safe in Edit Mode.
- Phase 7 subsequently promoted the reviewed binding from `validation` to `active` only for the proven semantic slice.

## Phase 7 reviewed-binding-promotion checkpoint

- Independent verifier result: **PASS** for Phase 7 promotion.
- The reviewed definition and matching Unity System sidecar were promoted from `validation` to `active` only for `unity.prefab.inspect`, `unity.serialized-fields.inspect`, and `unity.playmode.verify`.
- Promotion prerequisites were proven by the Phase 3 verifier **PASS** for the exact mapping/partition, the Phase 4 verifier **PASS** for acquisition-state boundaries, the Phase 5 verifier **PASS** for the live QuickGun Head vertical slice, and the Phase 6 fresh verifier **PASS** after recovery iteration **1**.
- The active scope remains exactly three mapped capabilities and six explicitly unbound capabilities; no additional Unity capability was promoted implicitly.
- Live validation exercised the intended prefab, serialized-field, and bounded Play Mode evidence chain; `active` records a reviewed definition and does not claim current transport connectivity, availability, or session identity.
- A fresh secret/ephemeral scan found no persisted credential, endpoint, port, session identity, live instance identifier/hash, live timestamp, or other ephemeral machine value in the promoted binding or matching sidecar.
- A supporting live read-only probe reacquired the intended Player prefab serialized Body/Head/Hair damage multipliers as `1`/`2`/`2`; this supported current inspectability without broadening the promotion criterion or mutating Unity state.
- Bounded cleanup of `.axit/checklists/runtime-binding-validation.md` and `.axit/README.md` resolved the identified documentation drift and received an independent verifier **PASS**.

## Phase 8 demonstrated-gap checkpoint

- The completed flow does not justify an additional capability binding, System Rule, Knowledge entry, or Core extension.
- An additional compile binding was not required, and `unity.console.inspect` served only as the explicit `unavailable` acquisition example.
- The incorrect-path issue, transient refresh behavior, Roslyn/CodeDom mismatch, and documentation drift were isolated, resolved, and did not repeat as reusable gaps.
- No additional Capability id or transport mapping is recommended.
- Remaining blockers: **none**.
- Next recommended step: **none**.

## Stable foundations

### Core v1

- Profiles: `game-designer`, `technical-architect`, `implementation-engineer`, `quality-verifier`.
- Skills: `implement-change`, `verify-change`.
- Workflow: `bounded-change`.
- Status: stable/frozen unless repeated live evidence demonstrates a reusable gap.

### Workspace/System v1

- Root-first routing is stable.
- `src/QuickGun-MVP` resolves to System `unity-client`.
- Missing cross-System contracts remain explicit unknowns rather than inferred behavior.

### Capability semantic v1

- Capability semantic v1 is stable after live Cases 1-5 passed.
- Stable Unity set: `.axit/capabilities/unity/evidence.yaml`.
- Unity System routing: `.axit/systems/unity-client/capabilities.yaml`.
- Verification semantics remain owned by `verify-change`.

### Runtime Binding v1

- Spec: `.axit/specs/runtime-binding-spec-v1.md`.
- Validation checklist: `.axit/checklists/runtime-binding-validation.md`.
- Current repository binding status: **active**, independently verified **PASS** in Phase 7.
- Reviewed definition: `.axit/bindings/unity-client/coplaydev-unity-mcp.yaml`.
- Referencing sidecar: `.axit/systems/unity-client/capabilities.yaml`.
- Current mapped slice is exactly:
  - `unity.prefab.inspect`
  - `unity.serialized-fields.inspect`
  - `unity.playmode.verify`
- The remaining six active Unity capabilities are explicitly unbound:
  - `unity.project.inspect`
  - `unity.compile`
  - `unity.tests.run`
  - `unity.scene.inspect`
  - `unity.component.inspect`
  - `unity.console.inspect`

## Workspace safety configuration

Current workspace config:

```yaml
safety:
  check_git_status: false
```

Semantics:

- Git-status-based worktree safety checking is opt-in and defaults to disabled.
- With `false`, continuous Axit runs must not inspect root/nested/submodule `git status` as a readiness gate and must not raise `DIRTY_WORKTREE_RISK` from modified/deleted/untracked/nested/submodule counts.
- Current filesystem/source content is treated as the working baseline for bounded tasks.
- This does not authorize reset, checkout, clean, destructive deletion, reverting user work, or blind overwrites.
- Another workspace may set `safety.check_git_status: true` when strict worktree status validation is desired.

This policy intentionally allows Axit workspaces whose Systems are untracked directories, nested repositories, or later represented as submodules without making Git topology a default execution blocker.

## Codex orchestration configuration

Project runtime configuration:

```text
.codex/config.toml
```

Intended defaults:

- primary model: `gpt-5.6-sol`;
- primary execution reasoning: `max`;
- Plan Mode reasoning: `xhigh`;
- sub-agent default model: `gpt-5.6-sol`;
- sub-agent default reasoning: `max`;
- multi-agent enabled with a four-sub-agent concurrency ceiling;
- `on-request` approvals reviewed by the auto-reviewer under the bounded local-development policy;
- workspace-write sandbox with network access.

No Unity MCP server/endpoint is configured in project `.codex/config.toml`. MCP configuration is intentionally left to the user's manual local setup.

Project-scoped Codex configuration applies only when the repository is trusted by Codex. Runtime/session overrides may still affect effective behavior and must be checked by the baseline sub-agent.

Custom project verifier:

```text
.codex/agents/axit-verifier.toml
```

The verifier is read-only and must not repair its own findings.

## Orchestrator policy

Root `AGENTS.md` requires the primary thread to coordinate rather than directly perform delegatable work.

Actual source exploration/edits/tests/Unity evidence/repairs/verification are delegated to sub-agents.

If an ordinary sub-agent lane pauses or fails inside accepted scope, the orchestrator may steer/resume/replace it for at most two bounded recovery attempts.

Routine phase boundaries do not require user confirmation.

## Manual Unity MCP prerequisite

MCP setup is **not** part of the continuous execution envelope.

Before running the continuous plan, the user manually:

- installs/configures the chosen MCP for Unity transport;
- starts/enables the Unity-side bridge/server as required by that transport;
- configures Codex MCP access locally as required;
- opens the intended QuickGun project/editor;
- confirms the transport is expected to be available to the new Codex session.

Axit agents may inspect and use that already-configured transport, but they may not install, configure, upgrade, start, or repair it.

If readiness inspection fails, the run stops with:

```text
HARD_BLOCKER: UNITY_MCP_NOT_READY
```

The agent reports the observed missing prerequisite and does not attempt setup.

## Continuous run sequence

Phases 0–8, including the bounded documentation cleanup, are complete. The continuous plan has reached `DONE`.

Completed sequence:

1. baseline/multi-agent readiness with Git status skipped because `safety.check_git_status: false`;
2. manual Unity MCP readiness gate;
3. live transport/tool discovery;
4. narrow Runtime Binding materialization;
5. acquisition-state regression;
6. first real Unity evidence vertical slice;
7. bounded FAIL -> repair -> reacquire -> reverify recovery;
8. binding promotion and bounded documentation cleanup when proven;
9. demonstrated-gap analysis with no additional extension justified.

## Boundaries kept unchanged

- no agent-driven MCP installation/configuration;
- no Core Skill #3;
- no Workflow #2;
- no broad Unity specialist catalog;
- no invented Capability or transport operation ids;
- no automatic commit/push/PR;
- no changes to legacy `.claude/**`.
