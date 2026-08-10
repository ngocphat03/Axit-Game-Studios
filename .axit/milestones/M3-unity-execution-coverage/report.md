# M3 — Unity Execution Coverage Report

Status: **DONE**
Closure verification: **PASS**
Human promotion: **HUMAN_PROMOTED** on 2026-08-10
Date: 2026-08-10

M3 execution, one bounded closure repair, final independent closure verification, remote code/artifact audit, and human promotion are complete. M4 was not started.

## Capability proven

M3 proved that Axit can acquire trustworthy full-project Unity script-compilation evidence for the exact current QuickGun editor/project and require a fresh full Unity compile after a bounded REAL production C# change.

The reviewed active Unity binding now maps exactly:

```text
unity.prefab.inspect
unity.serialized-fields.inspect
unity.playmode.verify
unity.compile
```

The remaining five declared Unity capabilities remain unbound:

```text
unity.project.inspect
unity.tests.run
unity.scene.inspect
unity.component.inspect
unity.console.inspect
```

No additional M3 capability was promoted because Phase 8 found `REQUIRED_NOW: none`.

## Baseline compile result

**PASS.**

The live-discovered `unity.compile` acquisition uses the current CoplayDev Unity transport as a composite operation:

1. resolve the exact current editor/project identity;
2. clear the Unity Console to open a fresh diagnostic window;
3. request script compilation with the observed force/scripts/request operation;
4. establish `fresh_cycle_correlated` from an accepted post-request signal;
5. establish `terminal_state_observed` from a distinct later state observation;
6. re-resolve exact identity after reload; and
7. page compiler diagnostics to completion.

Request acceptance is dispatch evidence, not compilation success. Acquisition outcome remains separate from the verification verdict.

A fresh full QuickGun baseline compile reached terminal evidence with zero compiler errors.

## Compile acquisition regression

Phase 4 independently demonstrated the important acquisition boundary:

```text
acquired + compilation succeeds
acquired + compilation errors exist
unavailable
denied
transport_error
```

A temporary bounded `#error AXIT_M3_PHASE4_INTENTIONAL_COMPILE_ERROR` fixture produced the expected fresh `CS1029` compiler diagnostic. The fixture and generated `.meta` were removed, the clean branch was reacquired, and complete diagnostics returned `total=0`.

The initial closure verifier found that historical evidence reused one state observation for both freshness and terminal proof and that the binding wording was narrower than live discovery. Closure repair loop 1 corrected only that evidence contract, then reacquired Phase 4 and Phase 7 evidence. Freshness and terminal state are now separate explicit flags and require separate observations.

## REAL scenario — M3-REAL-01

Scenario type: **REAL bug fix**.

Accepted defect: duplicate `AudioSourcePool.Release` calls could enqueue one source twice, allowing two later outstanding leases to alias the same `AudioSource`.

The frozen contract was independently approved before implementation.

Final production change in the canonical QuickGun System:

```csharp
if (_active.Remove(source))
{
    _pool.Enqueue(source);
}
```

This preserves normal one-time release/reuse while preventing a repeated release of the same lease from creating another available-pool entry.

Required deterministic regression coverage now contains three cases:

- consecutive empty-pool leases are distinct;
- a validly released source is reusable without aliasing an unreleased lease;
- duplicate release does not alias the next two outstanding leases.

Final focused verification passed **3/3**, and a fresh full Unity compilation after the final production C# edit completed with zero compiler diagnostics.

Final scenario verdict: **PASS**.

## Recovery and accounting

```text
product/baseline repair loops: 0
closure repair loops: 1
sub-agent replacements: 0
hard blockers: none
```

The one closure repair fixed evidence-contract consistency only. It did not modify product behavior beyond the already accepted REAL fix and did not require a product or baseline compile repair.

Transient Unity 6 reload transport interruptions were recovered by bounded re-resolution of the same exact editor/project without changing user-owned MCP configuration.

## Evidence provenance after push

### System-canonical-pushed

QuickGun production fix:

```text
system: unity-client
repository: ngocphat03/QuickGun-MVP
ref: release
commit: e2e1b1b6f3b0720d91e51def6b610f5714e17c52
file: Assets/QuickGunCore/Scripts/Services/AudioSourcePool.cs
```

The pushed commit contains the bounded duplicate-release guard.

### Workspace-canonical-pushed

The pushed Axit Workspace branch `agent/axit-core-layout-v1` contains the M3 report/retrospective/scenario artifacts, active Runtime Binding and System sidecar, Phase 4/8 artifacts, and focused root regression test.

### Ephemeral-runtime

Live Unity editor identity, compile/reload observations, diagnostic windows, standalone test processes, and final runtime connection state remain acquisition-local evidence and are not canonicalized.

## Model / cost accounting

Historical M3 execution used:

```text
primary: gpt-5.6-sol / xhigh
children observed during M3: gpt-5.6-sol / max
```

That historical fact is retained for auditability.

After M3, the user adopted the durable workspace cost policy:

```text
primary orchestrator = gpt-5.6-sol / xhigh
all future child lanes = gpt-5.6-luna / medium
```

Future children may not silently escalate above Luna/medium. See `.axit/policies/model-routing.md`.

## Framework hardening caused by M3

M3 justified only the smallest demonstrated changes:

- activate `unity.compile` alongside the M1 three-mapping slice;
- preserve the Unity 6 clear/request/freshness/separate-terminal/re-resolve/page acquisition procedure;
- preserve the duplicate-release regression test;
- require post-closure-verdict persistence before emitting `MILESTONE_DONE`;
- adopt a durable child model-routing/cost policy after the long expensive M3 run.

No new Profile, Skill, Workflow, semantic Capability, package, transport, or additional Runtime Binding mapping was justified.

## Known residual risks

- Runtime Binding `active` means reviewed definition, not guaranteed future transport connectivity.
- Unity 6 domain reload may transiently interrupt transport reads; future acquisitions must reacquire exact identity and terminal evidence.
- Compilation proof covers the observed QuickGun script-compilation surface, not player builds, every target platform, package upgrades, performance, or broad gameplay correctness.
- `unity.scene.inspect` remains a repeated gap for later human review but was not `REQUIRED_NOW` in M3.
- Model/cost performance has not yet been benchmarked under the new Luna/medium child policy; the next long milestone should record wall-clock duration and child accounting for comparison.

## Promotion decision

```text
M3 technical capability: PASS
Final closure verification: PASS
Remote product patch audit: PASS
Remote artifact audit: PASS
Human decision: HUMAN_PROMOTED
Full M3 rerun: NO
M4 started: NO
```

M3 is promoted. The next milestone must be deliberately designed/authorized; do not auto-start M4 from the roadmap mockup.
