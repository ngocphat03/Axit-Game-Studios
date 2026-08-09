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

Do not duplicate executable contracts such as OpenAPI/protobuf/schema/DTO truth inside `.axit`; point to the real source.

### Capability v1

Capabilities express semantic evidence/execution intent, not provider/tool commands.

Current stable Unity evidence set includes ids such as:

- `unity.prefab.inspect`
- `unity.serialized-fields.inspect`
- `unity.playmode.verify`

Capability output is evidence only. `verify-change` owns REQUIRED/SUPPORTING classification and PASS/FAIL/BLOCKED.

### Runtime Binding v1

Bindings map stable semantic Capability ids to verified concrete transport operations.

Transport availability is runtime state, not proof encoded by the binding definition.

MCP for Unity setup is currently user-owned/manual. Axit may inspect/use an already configured transport but must not install/configure/repair it during the continuous plan.

## Execution operating model

Primary model: GPT-5.6 Sol at maximum execution reasoning where supported.

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

## Git-status policy

Workspace config owns whether Git status is used as a safety gate:

```yaml
safety:
  check_git_status: false
```

Default is `false`.

When false, root/nested/submodule modified/deleted/untracked state must not by itself block execution. Current filesystem/source state is the working baseline.

This does not authorize reset, clean, reverting user work, destructive deletion, or blind overwrite.

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
- Unity MCP setup should remain manual/user-owned for this workspace;
- Git dirty status must be configurable and defaults to ignored for gating.

## Memory discipline

`project-memory.md` is for durable alignment, not current run detail.

Current execution state belongs in `.axit/state/active.md`.

Milestone target shape belongs in `.axit/roadmap/milestones-mockup.md`.

Canonical technical contracts remain in `.axit/specs/`, Core/System/Registry artifacts, executable source, and reviewed Runtime Bindings.

If this memory conflicts with newer accepted truth, update this memory instead of forcing the implementation back to old assumptions.