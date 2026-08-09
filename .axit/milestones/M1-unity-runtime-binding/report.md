# M1 — Unity Runtime Binding Report

Status: DONE
Review state: HUMAN_PROMOTED
Date: 2026-08-10

## Capability proven

Axit demonstrated the complete bounded chain:

```text
accepted criterion
  -> semantic Capability
  -> reviewed Runtime Binding
  -> real CoplayDev unity-mcp operation(s)
  -> current Unity evidence
  -> independent verify-change verdict
```

The reviewed binding is `.axit/bindings/unity-client/coplaydev-unity-mcp.yaml` and is active for exactly:

- `unity.prefab.inspect`
- `unity.serialized-fields.inspect`
- `unity.playmode.verify`

The remaining six declared Unity capabilities remain explicitly unbound.

## Scenarios executed

- live Unity transport/tool discovery against the intended QuickGun project;
- Runtime Binding materialization and independent validation;
- acquisition-state regression for `acquired`, `unavailable`, `denied`, and `transport_error` semantics;
- deterministic armored-head damage evidence;
- Player prefab hierarchy and serialized DamageableBodyPart inspection;
- bounded Play Mode Head damage scenario;
- one real `FAIL -> repair -> reacquire -> PASS` recovery path;
- binding promotion and closure audit.

## Evidence summary

- transport: user-configured `unityMCP` backed by CoplayDev `unity-mcp`;
- Unity project: QuickGun-MVP, observed Unity `6000.4.8f1` during the run;
- serialized Player Body/Head/Hair multipliers: `1 / 2 / 2`;
- bounded runtime target: `GameEnvironment/Bot/Model/Head`;
- runtime observation: multiplier `2`, armor `0.8`, health `3 -> 1`, observed damage `2`;
- deterministic local test suite reported `16/16` PASS, including `DamageCalculator.Calculate(5, 2f, 0.8f) == 2`;
- final independent verifier: PASS;
- final editor state returned to Edit Mode.

## Recovery evidence

One controlled project-local mismatch changed the Player Head damage multiplier from `2` to `1`.

Fresh acquisition demonstrated the failure, an independent verifier returned FAIL, a separate worker restored only the demonstrated defect, evidence was reacquired, and a fresh verifier returned PASS.

Recovery iterations: 1.

## Incidents encountered

- initial runtime hierarchy target omitted `GameEnvironment/`;
- transient Unity transport disconnects during asset refresh;
- Roslyn unavailable; verified CodeDom execution path used instead;
- stale binding/current-state documentation detected and repaired;
- primary Codex session resolved to `medium` despite repository intent for strongest reasoning;
- milestone closure artifacts were not emitted by the original execution plan itself.

## Remote-review scope

The pushed branch contains the Axit binding, sidecar, checklist, and completion state needed to review the framework result.

The reported local file `tests/QuickGun-MVP/DamageCalculatorTests.cs` is not present on the current GitHub branch, consistent with a workspace policy that may leave product/test trees untracked or separately tracked. Therefore this remote review can confirm the recorded local test evidence and framework checkpoints, but cannot independently re-read that local test source from GitHub.

This is an auditability limitation, not by itself a Runtime Binding failure.

## Known residual risks

- proof covers one synchronous Head-damage path, not projectile delivery, effects, all hit zones, or broad combat correctness;
- runtime connectivity remains resolve-at-runtime;
- transport refresh/disconnect behavior was recovered but not exhaustively stress-tested;
- `DEFERRED_PRIMARY_REASONING_CONFIG`: primary-session reasoning compatibility remains unresolved and must be reviewed again after M2 before M3.

## Promotion decision

`PROMOTE` — human-approved on 2026-08-10.

The user explicitly chose to proceed to M2 before repairing the known primary reasoning mismatch. That mismatch is accepted as a **temporary M2-only deferred issue**, not as a resolved configuration.

M2 may begin. M3 must not inherit this exception silently; the issue must be revisited at the M2 promotion review.