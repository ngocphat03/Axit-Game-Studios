# Axit Project Memory

Updated: 2026-08-11

## North star

Build Axit into a trustworthy coding agent/runtime for game development that can work across a product workspace, use external models for reasoning, control side effects through policy/runtime boundaries, acquire real evidence, independently verify outcomes, recover from bounded failures, and preserve enough run state to resume and audit work.

Axit-Game-Studios is the **spec/reference/validation lab** for how the agent should work. Proven contracts should later migrate into Axit-Code rather than turning this repository into the final runtime product.

## Product split

```text
Axit-Game-Studios
  = Knowledge/spec/benchmark lab

Axit-Code
  = product runtime/orchestration/safety/verification

future axitcode-unity
  = Unity execution host when appropriate
```

Do not make Axit-Game-Studios the runtime foundation for Axit-Code.

## Stable architecture currently accepted

### Core v1

Four responsibility Profiles:

- `game-designer`
- `technical-architect`
- `implementation-engineer`
- `quality-verifier`

Two Core Skills:

- `implement-change`
- `verify-change`

One Core Workflow:

- `bounded-change`

Core remains small. Add another Profile, Skill, or Workflow only when repeated live evidence demonstrates a durable gap.

### Workspace/System v1

Codex is normally opened from repository root.

```text
repository root = product workspace
src/*           = interacting Systems
```

A System may have its own canonical Git repository even when mounted under the Workspace source tree. Git topology does not redefine the Axit Workspace/System boundary.

Current promoted Unity System metadata:

```text
system: unity-client
repository: ngocphat03/QuickGun-MVP
canonical ref: release
```

Resolve moving repository heads at evidence time. Do not duplicate executable contracts such as OpenAPI/protobuf/schema/DTO truth inside `.axit`; point to the real source.

### Capability v1

Current reviewed active Unity binding maps exactly:

- `unity.prefab.inspect`
- `unity.serialized-fields.inspect`
- `unity.playmode.verify`
- `unity.compile`

The five other declared Unity capabilities remain unbound:

- `unity.project.inspect`
- `unity.tests.run`
- `unity.scene.inspect`
- `unity.component.inspect`
- `unity.console.inspect`

Capability output is evidence only. `verify-change` owns REQUIRED/SUPPORTING classification and PASS/FAIL/BLOCKED.

### Runtime Binding v1

Bindings map stable semantic Capability ids to verified concrete transport operations. Transport availability is runtime state, not proof encoded by the binding definition.

MCP for Unity setup is user-owned/manual. Axit may inspect/use an already configured transport but must not install/configure/repair it during continuous milestones.

Path-addressed runtime operations must resolve current full runtime identity before use. Prefab/source hierarchy is not sufficient proof of the full live scene hierarchy.

Unity 6 compile request acceptance is not terminal compile evidence. A trustworthy `unity.compile` acquisition clears the diagnostic window before the request, establishes `fresh_cycle_correlated`, establishes `terminal_state_observed` from a distinct later snapshot, re-resolves exact editor/project identity after reload, and pages diagnostics to completion.

## Execution operating model

Canonical model/cost policy:

```text
.axit/policies/model-routing.md
```

Default allocation:

```text
primary orchestrator = gpt-5.6-sol / xhigh
all child lanes       = gpt-5.6-luna / medium
```

All child roles are covered. Do not silently escalate a child to Terra/Sol or above `medium`. Only an explicit current human instruction may authorize a bounded override.

Child underperformance is handled by:

```text
distill/sharpen context
  -> steer/resume Luna/medium
  -> replace with fresh Luna/medium
  -> decompose into smaller checkable lanes
  -> reacquire current evidence
```

The primary thread acts as **orchestrator only** when sub-agents are available. Parallelize materially independent read-heavy work; serialize overlapping writes and editor/runtime mutations. The thread limit is a ceiling, not a target.

Maximum bounded recovery/replacement budget for the same required lane/failure remains two attempts before escalation through the milestone's existing failure/blocker semantics.

## Human control model

Automation is high **inside one accepted milestone** and stops for human promotion review between milestones.

Expected loop:

```text
start milestone
  -> autonomous execution
  -> bounded recovery/reverification
  -> report + retrospective
  -> persist final closure verdict
  -> STOP
  -> user + assistant review
```

Hard decisions remain human-owned when they materially change product intent, public contracts, state ownership, architecture, destructive scope, secrets/production access, or other irreversible/high-impact boundaries.

M1, M2, and M3 are human-promoted.

## Roadmap alignment after M3

M4 — Cross-System Workspace is **deferred**, not cancelled.

Reason: the current QuickGun Workspace has one real registered System. Do not invent a backend/CMS/service or synthetic second System simply to exercise M4. Resume M4 when a genuine second interacting System and executable boundary exist.

M5 — Project Bootstrap & Knowledge Plane is the current designed milestone.

Target:

```text
repository: ngocphat03/Axit-Code
canonical ref: release
```

M5 control artifacts:

```text
.axit/milestones/M5-project-bootstrap-knowledge-plane/plan.md
.axit/milestones/M5-project-bootstrap-knowledge-plane/target-bootstrap.md
.axit/milestones/M5-project-bootstrap-knowledge-plane/selection-review.md
```

M5 tests whether Axit can bootstrap a materially different real repository with minimal pointer-first durable context, recover that context in a fresh Luna/medium child without chat replay, and complete one frozen REAL target task with target-native verification.

Important boundary: Axit-Code product work must execute from a trusted writable local Axit-Code checkout. Do not guess its filesystem path and do not use cloud/GitHub writes as a substitute for a local target workspace.

M5 is **not M6 productization**. Do not migrate Game-Studios contracts into Axit-Code runtime architecture merely because Axit-Code is the M5 target.

## M3 promoted result

M3 proved:

- live-discovered and reviewed `unity.compile` Runtime Binding;
- fresh full QuickGun baseline Unity compile PASS;
- compile acquisition success/error boundaries;
- REAL duplicate-release fix with 3/3 deterministic tests and fresh post-change full Unity compile PASS;
- exact four-mapped/five-unbound Unity capability partition;
- final closure PASS and human promotion.

Canonical QuickGun product fix:

```text
repository: ngocphat03/QuickGun-MVP
ref: release
commit: e2e1b1b6f3b0720d91e51def6b610f5714e17c52
```

Historical M3 used Sol/max children and took roughly 3h15m. That historical fact motivated the later Luna/medium child policy and must not be rewritten as if M3 itself used the cheaper routing.

## Git-status policy

Workspace config owns whether Git status is used as a safety gate:

```yaml
safety:
  check_git_status: false
```

When false, root/nested/submodule modified/deleted/untracked state must not by itself block execution. This does not authorize reset, clean, reverting user work, destructive deletion, or blind overwrite.

## Evidence provenance

Use the narrowest truthful class:

```text
workspace-canonical-pushed
system-canonical-pushed
local-or-separately-tracked
ephemeral-runtime
```

For `system-canonical-pushed`, record System id + repository + ref + commit when known. Do not force Git topology changes solely for auditability.

M5 additionally must distinguish Axit-Code canonical product truth from M5-generated bootstrap metadata; bootstrap metadata must never masquerade as the target's canonical roadmap or architecture source.

## Evidence-driven evolution rule

Do not grow Axit from imagination.

Use:

```text
real scenario
  -> evidence
  -> demonstrated gap
  -> smallest correct layer
  -> implementation
  -> independent verification
  -> regression protection
```

Possible correct layers include Rule, Knowledge, routing pointer, existing Capability binding, a new Capability only when genuinely missing, Skill, Workflow, Profile, Runtime/Harness policy, or no framework change at all.

## Incident hardening rule

Every meaningful repeatable incident should become:

```text
incident
  -> root cause
  -> smallest framework/config fix
  -> regression protection
  -> future runs inherit the fix
```

Durable lessons include:

- REQUIRED/SUPPORTING verdict precedence must be explicit;
- capability ids are declared, never invented;
- unavailable acquisition differs from observed product failure;
- Unity MCP setup remains manual/user-owned;
- Git dirty status is configurable and ignored by default for gating;
- runtime paths are resolved from current runtime context, not guessed from prefab hierarchy;
- Unity 6 compile evidence requires fresh correlation and a distinct later terminal observation;
- evidence provenance respects independently tracked System repositories;
- active state is compacted after promotion;
- closure-verifier output is persisted before `MILESTONE_DONE`;
- flagship reasoning is reserved for the primary orchestrator; child lanes default Luna/medium;
- milestone ordering follows real evidence, so M4 may be deferred rather than filled with a fake second System.

## Memory discipline

`project-memory.md` is durable alignment, not current run detail.

Current execution state belongs in `.axit/state/active.md`.

Completed milestone detail belongs in report, retrospective, scenario manifest, and promotion review.

Milestone target shape belongs in `.axit/roadmap/milestones-mockup.md`.

Canonical technical contracts remain in `.axit/specs/`, Core/System/Registry artifacts, executable source, reviewed Runtime Bindings, and `.axit/policies/`.

If this memory conflicts with newer accepted truth, update this memory instead of forcing implementation back to old assumptions.
