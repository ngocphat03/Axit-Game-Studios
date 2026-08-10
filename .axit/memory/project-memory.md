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

Current stable Unity evidence set includes ids such as:

- `unity.prefab.inspect`
- `unity.serialized-fields.inspect`
- `unity.playmode.verify`
- `unity.compile`

The reviewed active binding maps exactly those four ids. The five other
declared Unity capabilities remain unbound: `unity.project.inspect`,
`unity.tests.run`, `unity.scene.inspect`, `unity.component.inspect`, and
`unity.console.inspect`.

Capability output is evidence only. `verify-change` owns REQUIRED/SUPPORTING classification and PASS/FAIL/BLOCKED.

### Runtime Binding v1

Bindings map stable semantic Capability ids to verified concrete transport operations.

Transport availability is runtime state, not proof encoded by the binding definition.

MCP for Unity setup is user-owned/manual. Axit may inspect/use an already configured transport but must not install/configure/repair it during continuous milestones.

Path-addressed runtime operations must resolve current full runtime identity before use. Prefab/source hierarchy is not sufficient proof of the full live scene hierarchy. After refresh/reload, resolve identity again rather than reuse stale paths/instance ids.

Unity 6 compile request acceptance is not terminal compile evidence. A
trustworthy `unity.compile` acquisition clears the diagnostic window before
the request, establishes `fresh_cycle_correlated` through sampled nonterminal
state, an advanced compile marker, or an advanced domain-reload marker, then
establishes `terminal_state_observed` from a distinct later snapshot. It also
re-resolves exact editor/project identity after reload and pages diagnostics
to completion. Verifier scripts must map evidence to both explicit flags and
must not reuse the freshness snapshot as terminal proof. Compile-internal
identity reads and Console operations do not create separate Capability
mappings. A transient CoplayDev reload warning is recoverable only when the
same exact target and a complete terminal evidence set are re-established.

## Execution operating model

Primary orchestrator: GPT-5.6 Sol / `xhigh`.

Project primary reasoning is configured to `xhigh` after M1/M2 exposed unreliable effective behavior from the previous primary `max` project setting. A fresh milestone must verify the effective primary value rather than trust configured intent.

All child/sub-agent lanes, including independent verifier lanes, default to GPT-5.6 Luna / `medium` for cost control. Do not escalate child lanes to Terra/Sol or above `medium` reasoning unless the user explicitly changes this policy.

The primary thread acts as **orchestrator only** when sub-agents are available. Delegatable exploration, setup inspection, implementation, tests, Unity evidence, repair, and verification belong to sub-agents.

Implementation and independent verification should use separate lanes when practical.

If a sub-agent stalls inside accepted scope:

```text
steer/resume
  -> replace with distilled-context agent if needed
      -> reacquire current evidence
          -> continue
```

Maximum bounded recovery/replacement budget for the same required lane/failure: two attempts before escalation.

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
  -> STOP
  -> user + assistant review
      -> promote and start next milestone
      OR
      -> repair framework/regressions and rerun
```

Hard decisions remain human-owned when they materially change product intent, public contracts, state ownership, architecture, destructive scope, secrets/production access, or other irreversible/high-impact boundaries.

M1 and M2 are human-promoted. M3 — Unity Execution Coverage has completed
execution and closure repair loop 1 and awaits final independent closure
re-verification before human promotion review. M3 remains current, is not
`HUMAN_PROMOTED`, and M4 is not authorized.

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

M3 proved and activated `unity.compile` after full baseline, acquisition-state,
and REAL-scenario evidence. Its demonstrated-gap analysis found no additional
`REQUIRED_NOW` need. Keep the five remaining Unity Capabilities unbound until a
future accepted criterion demonstrates a real required evidence gap.

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

- verification required/supporting semantics needed explicit precedence;
- missing implementation must differ from unresolved target identity;
- capability ids must never be invented;
- ordinary project tests are not automatically Axit Capabilities;
- unavailable acquisition differs from observed product failure;
- Unity MCP setup remains manual/user-owned;
- Git dirty status is configurable and defaults to ignored for gating;
- runtime full paths must be resolved from current runtime context, not guessed from prefab hierarchy;
- Unity 6 compile requests require a clear diagnostic window, one explicit
  freshness flag, a distinct later terminal flag, exact identity
  re-resolution, and complete diagnostic paging; verifier scripts must map
  both flags and request acceptance alone is never success;
- evidence provenance must respect independent System repositories;
- active state must be compacted after promotion rather than carrying completed milestone transcripts forever;
- expensive flagship reasoning should be reserved for the primary orchestrator; child lanes use Luna/medium unless the user explicitly reconfigures cost policy.

## Memory discipline

`project-memory.md` is for durable alignment, not current run detail.

Current execution state belongs in `.axit/state/active.md` and should remain compact.

Completed milestone detail belongs in its report, retrospective, scenario manifest, and promotion review.

Milestone target shape belongs in `.axit/roadmap/milestones-mockup.md`.

Canonical technical contracts remain in `.axit/specs/`, Core/System/Registry artifacts, executable source, and reviewed Runtime Bindings.

If this memory conflicts with newer accepted truth, update this memory instead of forcing the implementation back to old assumptions.
