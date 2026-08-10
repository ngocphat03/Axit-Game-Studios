# M2 — Human Promotion Review

Date: 2026-08-10
Decision: HUMAN_PROMOTED
Reviewer: user + assistant

## Decision

M2 is promoted. Do not rerun the full milestone.

The promotion is based on:

- four frozen scenario contracts reaching independent PASS;
- worker/verifier separation;
- bounded orchestration recovery without routine user steering;
- context-continuity PASS;
- closure verifier PASS;
- direct code-level follow-up audit against the canonical QuickGun System repository.

## QuickGun System source audit

Canonical System repository:

```text
system: unity-client
repository: ngocphat03/QuickGun-MVP
canonical ref: release
reviewed commit: c35143a6ea72dd17e591e67b1e965e10a0b15a27
```

The follow-up audit confirmed:

- S1 `AudioSourcePool` was a real ownership/double-lease defect and the current implementation removes the erroneous enqueue-on-create behavior while preserving the public API;
- S3 `CombatResultResolver` preserves the timeout result mapping and delegates exactly once from `GamePlayState.OnTimerEnd` to the resolver before `EndCombat`;
- the current damage path remains consistent with hit-zone multiplier -> armor -> health semantics;
- the Player prefab retains accepted Head/Hair/Body multipliers `2/2/1`.

The System repository moved from Unity 2022.3.35f1 to 6000.4.8f1 and its current change set includes substantial Unity/package/serialization churn. Therefore full Unity compile coverage is a demonstrated next need rather than optional polish.

## Promotion hardening applied before M3

1. Primary project reasoning setting changed from `max` to `xhigh`; M3 readiness must verify the effective fresh-session value and must not infer success from the config file alone.
2. Runtime Binding validation now requires current runtime target identity to be resolved before any path-addressed runtime operation; full runtime paths may not be guessed from prefab-relative hierarchy.
3. Evidence provenance is now System-aware and distinguishes `workspace-canonical-pushed`, `system-canonical-pushed`, `local-or-separately-tracked`, and `ephemeral-runtime`.
4. The `unity-client` System records `ngocphat03/QuickGun-MVP` / `release` as its canonical Git repository/ref without treating a moving branch head as timeless evidence.
5. Post-promotion active state must be compact; completed M2 detail remains in its report, retrospective, scenario manifest, and this promotion review.

## M3 authorization

M3 — Unity Execution Coverage is authorized to begin only from its accepted execution plan.

The first demonstrated capability gap is:

```text
unity.compile
```

M3 must discover the real current Unity transport operation before mapping it. Do not bind all remaining Unity capabilities speculatively.

After compile coverage is proven, M3 should select at least one REAL QuickGun product scenario with frozen acceptance. Full Unity compile must be REQUIRED for any production C# change in that scenario.

## Residual review items

- effective primary `xhigh` still needs live verification in the fresh M3 session;
- System source may be canonical in the QuickGun repository while test harnesses under root `tests/QuickGun-MVP/` remain local/separately tracked;
- negative health observed in the bounded damage scenario is not classified as a defect without an accepted product rule requiring health clamping;
- no new Profile, Skill, Workflow, or semantic Capability is justified by M2 promotion itself.
