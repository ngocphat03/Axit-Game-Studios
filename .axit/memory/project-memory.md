# Axit Project Memory

Updated: 2026-08-10

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

Core remains small. Add another Profile, Skill, or Workflow only when repeated live use demonstrates a durable gap.

### Workspace/System v1

Codex is normally opened from repository root.

```text
repository root = product workspace
src/*           = interacting Systems
```

Examples of Systems: Unity client, backend, CMS, services.

Root routing uses `.axit/workspace.yaml`. System-local context belongs under `.axit/systems/<system-id>/`; cross-system ownership/contracts belong in root registries.

A System may have its own canonical Git repository even when mounted under the Workspace source tree. Git topology does not redefine the Axit Workspace/System boundary.

Current Unity System canonical repository metadata:

```text
system: unity-client
repository: ngocphat03/QuickGun-MVP
canonical ref: release
```

Resolve the current commit at evidence time. Do not persist a moving branch head as timeless truth.

Do not duplicate executable contracts such as OpenAPI/protobuf/schema/DTO truth inside `.axit`; point to the real source.

### Capability v1

Capabilities express semantic evidence/execution intent, not provider/tool commands.

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

Bindings map stable semantic Capability ids to verified concrete transport operations.

Transport availability is runtime state, not proof encoded by the binding definition.

MCP for Unity setup is user-owned/manual. Axit may inspect/use an already configured transport but must not install/configure/repair it during continuous milestones.

Path-addressed runtime operations must resolve current full runtime identity before use. Prefab/source hierarchy is not sufficient proof of the full live scene hierarchy. After refresh/reload, resolve identity again rather than reuse stale paths/instance ids.

Unity 6 compile request acceptance is not terminal compile evidence. A trustworthy `unity.compile` acquisition clears the diagnostic window before the request, establishes `fresh_cycle_correlated` through sampled nonterminal state, an advanced compile marker, or an advanced domain-reload marker, then establishes `terminal_state_observed` from a distinct later snapshot. It re-resolves exact editor/project identity after reload and pages diagnostics to completion. Verifier scripts must map evidence to both explicit flags and must not reuse the freshness snapshot as terminal proof.

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

`all child lanes` includes explorers, workers, test/build/evidence agents, Unity/MCP acquisition agents, repair/recovery agents, independent verifiers, closure verifiers, report authors, and custom sub-agents.

Do not silently escalate a child to Terra/Sol or above `medium`. Only an explicit current human instruction may authorize a bounded override. The override expires with its stated scope unless the user deliberately changes this durable policy.

Child underperformance is handled by:

```text
distill/sharpen context
  -> steer/resume Luna/medium
  -> replace with fresh Luna/medium
  -> decompose into smaller checkable lanes
  -> reacquire current evidence
```

Do not use a larger child model as the automatic recovery mechanism.

The primary thread acts as **orchestrator only** when sub-agents are available. Delegatable exploration, setup inspection, implementation, tests, Unity evidence, repair, and verification belong to sub-agents.

Implementation and independent verification should use separate lanes when practical. Verification independence comes from fresh responsibility/context/evidence, not from using a more expensive verifier model.

Use parallelism only for materially independent lanes. Read-heavy work may run concurrently; overlapping product writes, Runtime Binding/state/report writes, and Unity/editor mutations remain serialized. The configured thread limit is a ceiling, not a target.

Maximum bounded recovery/replacement budget for the same required lane/failure remains two attempts before escalation through the milestone's existing failure/blocker semantics.

Routine phase completion does not require user confirmation.

## Human control model

Automation should be high **inside one accepted milestone**.

Between milestones, stop for human review.

Expected loop:

```text
start milestone
  -> autonomous continuous execution
  -> self-repair/replacement/reverification
  -> milestone report + retrospective
  -> persist final closure verdict
  -> STOP
  -> user + assistant review
      -> promote and design/authorize next milestone
      OR
      -> repair framework/regressions and rerun only affected scope
```

Hard decisions remain human-owned when they materially change product intent, public contracts, state ownership, architecture, destructive scope, secrets/production access, or other irreversible/high-impact boundaries.

M1, M2, and M3 are human-promoted. No M4 execution plan is currently authorized.

## M3 promoted result

M3 — Unity Execution Coverage proved:

- live-discovered and reviewed `unity.compile` Runtime Binding;
- fresh full QuickGun baseline Unity compile PASS;
- acquisition distinction for clean compile, compiler-error evidence, and non-acquired states;
- REAL scenario `M3-REAL-01` duplicate-release bug fix with 3/3 deterministic tests and fresh post-change full Unity compile PASS;
- exact active binding partition of four mapped / five unbound capabilities;
- one closure repair for freshness/terminal evidence separation;
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

Default is `false`.

When false, root/nested/submodule modified/deleted/untracked state must not by itself block execution. Current filesystem/source state is the working baseline.

This does not authorize reset, clean, reverting user work, destructive deletion, or blind overwrite.

## Evidence provenance

Use the narrowest truthful class:

```text
workspace-canonical-pushed
system-canonical-pushed
local-or-separately-tracked
ephemeral-runtime
```

For `system-canonical-pushed`, record System id + repository + ref + commit when known.

Do not force Git tracking or change repository topology solely for auditability.

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

Possible correct layers include Rule, Knowledge, existing Capability binding, new Capability only when genuinely missing, Skill, Workflow, Profile, Runtime/Harness policy, or no framework change at all.

M3 found no additional `REQUIRED_NOW` Unity capability beyond `unity.compile`. Keep the five remaining capabilities unbound until a future accepted criterion demonstrates a real required evidence gap.

## Incident hardening rule

Every meaningful pipeline incident should become:

```text
incident
  -> root cause
  -> framework/config fix when appropriate
  -> regression rule/test
  -> future runs inherit the fix
```

Examples already learned:

- verification required/supporting semantics need explicit precedence;
- missing implementation differs from unresolved target identity;
- capability ids must never be invented;
- ordinary project tests are not automatically Axit Capabilities;
- unavailable acquisition differs from observed product failure;
- Unity MCP setup remains manual/user-owned;
- Git dirty status is configurable and defaults to ignored for gating;
- runtime full paths must be resolved from current runtime context, not guessed from prefab hierarchy;
- Unity 6 compile requests require a clear diagnostic window, explicit freshness, a distinct later terminal observation, exact identity re-resolution, and complete diagnostic paging;
- evidence provenance must respect independent System repositories;
- active state must be compacted after promotion;
- closure-verifier output must be persisted into report/retrospective/active state before `MILESTONE_DONE`;
- expensive flagship reasoning is reserved for the primary orchestrator; future child lanes use Luna/medium unless the user explicitly overrides a bounded scope.

## Memory discipline

`project-memory.md` is for durable alignment, not current run detail.

Current execution state belongs in `.axit/state/active.md` and should remain compact.

Completed milestone detail belongs in its report, retrospective, scenario manifest, and promotion review.

Milestone target shape belongs in `.axit/roadmap/milestones-mockup.md`.

Canonical technical contracts remain in `.axit/specs/`, Core/System/Registry artifacts, executable source, reviewed Runtime Bindings, and `.axit/policies/`.

If this memory conflicts with newer accepted truth, update this memory instead of forcing the implementation back to old assumptions.
