# M3 — Human Promotion Review

Date: 2026-08-10
Decision: HUMAN_PROMOTED
Reviewer: user + assistant

## Decision

M3 — Unity Execution Coverage is promoted. Do not rerun the full milestone.

The promotion is based on:

- baseline full-project QuickGun Unity compile PASS;
- reviewed and active `unity.compile` Runtime Binding;
- Phase 4 acquisition-state regression including fresh compiler-error and clean branches;
- frozen REAL scenario `M3-REAL-01` completed with independent PASS;
- canonical QuickGun production patch verified on `release` at commit `e2e1b1b6f3b0720d91e51def6b610f5714e17c52`;
- root regression harness includes the duplicate-release case and all three accepted ownership tests;
- one bounded closure repair corrected evidence-contract consistency without product repair;
- final closure verifier returned PASS;
- remote review found no overclaim requiring a product/Unity rerun.

## Closure persistence repair

Remote review found that final console output had reached `MILESTONE_DONE`, but durable M3 report/retrospective/active state still said final closure verification was pending.

This was classified as a closure-persistence defect, not a product/runtime failure.

The milestone closure contract now requires:

```text
closure verifier result
  -> persist verdict to report/retrospective/active state
  -> fresh read-only consistency audit
  -> MILESTONE_DONE
```

No Unity recompile or REAL-scenario rerun was required to repair this documentation/control defect.

## Model / cost decision adopted at promotion

Historical M3 child lanes were observed at `gpt-5.6-sol / max` and the run took roughly 3h15m.

The user explicitly changed the durable workspace routing policy to:

```text
primary orchestrator = gpt-5.6-sol / xhigh
all child/sub-agent lanes = gpt-5.6-luna / medium
```

This includes explorers, workers, evidence lanes, repair/recovery agents, independent verifiers, closure verifiers, report authors, and custom sub-agents.

Children may not silently escalate to a larger model or reasoning level. A bounded explicit human override is required. The canonical rule is `.axit/policies/model-routing.md`.

This new cost policy applies to future child lanes. It does not rewrite M3's historical execution metadata.

## Promoted Unity execution surface

Active mappings after M3 are exactly:

```text
unity.prefab.inspect
unity.serialized-fields.inspect
unity.playmode.verify
unity.compile
```

The following remain explicitly unbound:

```text
unity.project.inspect
unity.tests.run
unity.scene.inspect
unity.component.inspect
unity.console.inspect
```

No additional mapping is authorized solely by M3 promotion.

## Next milestone boundary

M4 has **not** started.

Promotion of M3 does not auto-authorize executing the M4 mockup. The next step is a separate human/assistant decision about the smallest valuable milestone based on whether at least two real interacting Systems actually exist and have accepted boundaries.
