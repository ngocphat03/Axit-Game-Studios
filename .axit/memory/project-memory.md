# Axit Project Memory

Updated: 2026-08-11

## North star

Build Axit into a trustworthy coding agent/runtime for game development that can understand a product workspace, use external models for reasoning, control side effects through runtime/policy boundaries, acquire real evidence, independently verify outcomes, recover from bounded failures, and preserve enough state for audit/resume.

Product split:

```text
Axit-Game-Studios = spec / reference / validation lab
Axit-Code         = canonical product runtime/orchestration/safety/verification
future axitcode-unity = Unity execution host
```

Do not turn Game-Studios into the final Axit-Code runtime. Productize proven semantics into Axit-Code-native contracts.

## Stable Game-Studios foundation

Core v1 remains intentionally small:

```text
Profiles: game-designer, technical-architect, implementation-engineer, quality-verifier
Skills: implement-change, verify-change
Workflow: bounded-change
```

No new Core Profile/Skill/Workflow without repeated demonstrated need.

Workspace/System v1:

```text
repository root = product workspace
src/*           = interacting Systems
```

A System may have an independent canonical Git repository without changing the Axit Workspace/System boundary.

QuickGun Unity System canonical repository:

```text
ngocphat03/QuickGun-MVP @ release
```

Resolve moving branch heads at evidence time.

## Unity Capability / Runtime Binding state

Promoted active Unity mappings are exactly:

```text
unity.prefab.inspect
unity.serialized-fields.inspect
unity.playmode.verify
unity.compile
```

Still unbound until demonstrated need:

```text
unity.project.inspect
unity.tests.run
unity.scene.inspect
unity.component.inspect
unity.console.inspect
```

Capability semantics are provider-neutral. Runtime Binding maps them to reviewed transport operations. Capability output is evidence; `verify-change` owns REQUIRED/SUPPORTING and PASS/FAIL/BLOCKED.

MCP for Unity setup remains manual/user-owned.

Runtime full paths must be resolved from current runtime context, not inferred from prefab-relative hierarchy. Unity 6 compile success requires fresh-cycle correlation, a distinct later terminal observation, exact target re-resolution, and complete diagnostics paging.

## Model / cost policy

Canonical policy:

```text
.axit/policies/model-routing.md
```

Current hierarchy:

```text
primary = gpt-5.6-sol / xhigh
child preferred = gpt-5.6-luna / medium when supported
child compatibility fallback = gpt-5.6-terra / medium when Luna is unavailable
child Sol = forbidden without explicit current human override
```

The current Codex runtime observed during M5 did not expose Luna children, so `COMPAT_TERRA` / medium is accepted. This is a compatibility route, not a human model override and not a permanent preference change.

Child underperformance response:

```text
sharpen context
  -> steer/resume allowed medium-tier child
  -> replace with fresh allowed medium-tier child
  -> decompose
  -> reacquire evidence
```

Never silently buy a child Sol as recovery.

Primary is orchestration-only when delegation is available. Parallelize materially independent read-heavy work; serialize overlapping writes/editor mutations. Thread limits are ceilings, not targets.

M5 showed that lane count itself can become overhead: 19 child lanes for a small product diff with peak useful parallelism 4. Do not create a new child merely for every phase/checkpoint. Keep fresh lanes when independence materially matters, especially verification.

## Human control and milestone closure

Automation is high inside one accepted milestone. Every milestone stops for human promotion review before the next slice.

Closure sequence must be:

```text
draft closure artifacts
  -> independent closure verifier returns actual verdict
  -> persist actual verdict
  -> fresh read-only consistency audit
  -> MILESTONE_DONE
  -> STOP
```

Do not pre-write a predicted PASS and call that persisted verifier evidence.

For long-run performance accounting retain both when available:

```text
execution_to_preclosure
terminal_end_to_end
```

Use terminal end-to-end duration for milestone latency comparisons.

## Promoted milestone state

```text
M1 — Unity Runtime Binding: HUMAN_PROMOTED
M2 — Autonomous Bounded Development: HUMAN_PROMOTED
M3 — Unity Execution Coverage: HUMAN_PROMOTED
M4 — Cross-System Workspace: DEFERRED_WAITING_REAL_SECOND_SYSTEM
M5 — Project Bootstrap & Knowledge Plane: HUMAN_PROMOTED
```

M4 remains deferred because no real accepted second interacting QuickGun System exists. Do not manufacture a cross-system benchmark.

## M5 promoted result

Target:

```text
ngocphat03/Axit-Code
execution/audit branch: feature/m5
```

M5 proved:

- pointer-first bootstrap in a materially different real repository;
- `docs/PLAN.md` remained target-owned canonical product/roadmap truth;
- bootstrap review PASS;
- fresh-context continuity PASS without chat replay;
- one frozen REAL Axit-Code task PASS;
- focused agent tests reported 10/10 PASS;
- root `npm run verify` reported PASS;
- medium-tier delegated execution via `COMPAT_TERRA` with 0 human model overrides;
- final independent closure PASS.

Timing sample:

```text
execution_to_preclosure = 22m 12s
terminal_end_to_end     = 26m 02s
child lanes             = 19
peak useful parallelism = 4
replacements            = 0
REAL repair loops       = 0
```

M5 initially repeated the closure-persistence defect: artifacts predicted terminal PASS before the final verifier returned. Post-run review repaired metadata only and persisted the actual verifier PASS. No product/runtime rerun was necessary.

M5 audit metadata may remain on `feature/m5`; human promotion does not require merging all `.codex/m5/**` into `release`. The valid test-only product diff can be integrated separately.

## M6 decomposition

M6 is Axit-Code Productization and must proceed by bounded slices, not one giant run.

Current designed slice:

```text
M6-A — Profile / Rule / Workflow / Knowledge Loader Foundation
```

Artifacts:

```text
.axit/milestones/M6A-loader-foundation/plan.md
.axit/milestones/M6A-loader-foundation/selection-review.md
```

M6-A only proves deterministic declarative loading/validation, normalized records, source identity/provenance, deterministic ordering, and explicit conflict/error behavior.

It explicitly excludes Context Builder, Harness/Tool Gateway, Run Ledger, provider integration, CLI orchestration, Unity integration, Capability/Binding runtime, and M6-B.

### Current M6-A dependency

At 2026-08-11, Axit-Code PR #5 `docs: establish AxitCode and Game Design rules` is open, draft, and unmerged. Its own stated sequence places rule/catalog foundation before Profile/Rule/Workflow/Knowledge loader implementation.

Do not automatically merge/rebase/close or treat draft PR #5 as canonical. Human review must accept, revise, or supersede that foundation.

Until an accepted loader-input foundation exists on the chosen Axit-Code baseline:

```text
HARD_BLOCKER: KNOWLEDGE_FOUNDATION_NOT_ACCEPTED
```

Context Builder becomes M6-B only after M6-A is promoted.

## Git/status and provenance

Game-Studios workspace default:

```yaml
safety:
  check_git_status: false
```

Dirty/untracked/nested Git state is not itself a readiness blocker when this flag is false. This never authorizes reset/clean/revert/destructive overwrite.

Evidence provenance remains Workspace/System-aware. Do not change Git topology solely for auditability.

For target-repository bootstrap/productization work, distinguish canonical target truth from execution metadata; execution metadata must not masquerade as product roadmap or architecture authority.

## Evidence-driven evolution

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

Do not grow Axit from imagination.

## Memory discipline

`project-memory.md` = durable alignment.

`.axit/state/active.md` = compact current execution truth.

Completed detail = milestone report / retrospective / manifest / promotion-review.

`milestones-mockup.md` = human/assistant roadmap alignment.

If this memory conflicts with newer accepted truth, update this file rather than forcing newer implementation back to an old assumption.
