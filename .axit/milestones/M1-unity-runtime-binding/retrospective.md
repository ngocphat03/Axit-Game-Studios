# M1 — Unity Runtime Binding Retrospective

Date: 2026-08-10
Milestone result: capability PASS; promotion review pending

## What worked

- semantic Capability ids stayed transport-neutral;
- concrete operation names came from the live transport rather than guesses;
- only the intended 3-capability slice was bound;
- Runtime Binding availability remained separate from source-controlled binding status;
- acquisition states stayed separate from verification verdicts;
- implementation/evidence work and independent verification remained separated;
- one real FAIL/recovery/reacquisition/fresh-verifier path completed without user micromanagement;
- transient Unity transport disruption self-recovered without MCP reconfiguration;
- the final editor state was safely restored to Edit Mode;
- no Core/Profile/Skill/Workflow/Capability-semantic expansion was needed.

## Incidents and root causes

### 1. Wrong initial runtime target path

Observed:
The first runtime target omitted the `GameEnvironment/` prefix.

Root cause:
The scenario began from an assumed hierarchy path rather than treating the current runtime hierarchy as evidence that must be resolved first.

Hardening:
Future runtime scenarios must acquire/resolve the concrete current hierarchy path before executing a path-addressed runtime mutation. Never synthesize a full runtime hierarchy path from source or prefab names alone.

### 2. Transient Unity disconnect during asset refresh

Observed:
Unity MCP temporarily disconnected while assets refreshed.

Root cause:
The editor/transport can be temporarily unavailable while Unity processes changes even though configuration remains valid.

Hardening:
Classify short-lived transport loss as runtime acquisition/recovery state, not immediate user setup failure. Use bounded retry/reconnect within the existing recovery budget when the previously validated transport returns without configuration changes. Escalate to `UNITY_MCP_NOT_READY` only when readiness cannot be restored within the bounded recovery policy.

### 3. Roslyn unavailable

Observed:
The live transport did not provide the expected Roslyn path; CodeDom was available and validated.

Root cause:
Compiler/runtime execution support is environment/adapter-version dependent.

Hardening:
Discover and verify the actual execution compiler/operation contract before materializing a binding. Persist the verified compiler requirement in the concrete binding only; do not add compiler/provider details to semantic Capability ids.

### 4. Stale documentation after promotion

Observed:
Two current-state documentation files retained stale unbound wording after binding promotion.

Root cause:
Promotion updated canonical binding state before every summary/current-state reference was checked.

Hardening:
Milestone closure must include a documentation/state consistency scan covering the active binding, System sidecar, validation checklist, active state, and any current-state README that claims binding status.

### 5. Primary reasoning resolved to medium

Observed:
The primary session reported `gpt-5.6-sol / medium` while the project file requested `model_reasoning_effort = "max"`.

Root cause:
Current Codex project config documents primary `model_reasoning_effort` values only through `xhigh`; the repository used a value outside that documented config enum. GPT-5.6 itself may support higher reasoning outside this specific Codex config surface, but the local project setting did not produce the intended primary-session result.

Hardening:
Before M2, use the strongest Codex-supported primary-session config value and verify the effective model/effort during Phase 0. If the effective primary effort differs from the configured required value, treat that as execution-readiness failure rather than silently continuing.

### 6. Milestone closure artifacts were missing

Observed:
The continuous plan completed but created no dedicated Milestone Report, Retrospective, or incident artifact.

Root cause:
The execution plan predated the milestone operating contract and only specified a final console summary.

Hardening:
Every future milestone plan must contain an explicit final Closure Phase that writes the milestone report and retrospective before returning `MILESTONE_DONE`. The orchestrator must stop before opening the next milestone.

## Auditability note

The local run reports one added deterministic test and 16/16 PASS, but the corresponding `tests/QuickGun-MVP/DamageCalculatorTests.cs` is not present on the current pushed GitHub branch. This is compatible with the workspace's configurable/untracked Git topology and does not invalidate the live evidence by itself, but it means a remote reviewer cannot independently inspect that local test source from this branch.

Future milestone reports should explicitly distinguish:

- evidence reproducible from the canonical pushed branch;
- evidence acquired from a separately tracked/untracked local System;
- ephemeral runtime evidence.

Do not force Git tracking merely to satisfy this distinction.

## Framework changes justified by M1

Required:

- persist M1 report and retrospective;
- harden milestone closure requirements;
- correct/validate the primary Codex reasoning setting for the next run;
- record the incident lessons in durable decision/memory context.

Not justified:

- Core Skill #3;
- Workflow #2;
- new Profile;
- new semantic Capability id;
- binding additional Unity capabilities.

## Promotion recommendation

M1 capability result: PASS.

Recommended decision: `PROMOTE` once the primary-session reasoning configuration is corrected/verified and closure artifacts are persisted.

M2 must not start automatically before human review confirms promotion.
