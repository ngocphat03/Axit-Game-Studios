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

Core v1 stays intentionally small:

```text
Profiles: game-designer, technical-architect, implementation-engineer, quality-verifier
Skills: implement-change, verify-change
Workflow: bounded-change
```

No new Core Profile/Skill/Workflow without repeated demonstrated need.

Workspace/System v1:

```text
repository root = Workspace/Product
src/*           = interacting Systems
```

QuickGun canonical Unity System:

```text
ngocphat03/QuickGun-MVP @ release
```

## Unity Capability / Runtime Binding state

Promoted mappings are exactly:

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

Capability semantics are provider-neutral. Runtime Binding maps them to reviewed transport operations. `verify-change` owns REQUIRED/SUPPORTING and PASS/FAIL/BLOCKED.

MCP for Unity setup remains manual/user-owned. Runtime full paths must be resolved from current runtime context, not inferred from prefab-relative hierarchy. Unity compile success requires fresh-cycle correlation, a distinct later terminal observation, exact target re-resolution, and complete diagnostics paging.

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

Child underperformance response:

```text
sharpen context
-> steer/resume allowed medium-tier child
-> fresh allowed medium-tier replacement
-> decompose
-> reacquire evidence
```

Primary orchestrates; children handle materially independent work. Parallelize independent reads and serialize overlapping writes/editor mutations. Thread limits are ceilings, not targets. M5 showed that child-lane count can become orchestration overhead; do not spawn a new lane merely for every checkpoint.

## Human control and closure

Automation is high inside one accepted milestone. Every milestone stops for human promotion review before the next execution slice.

Closure sequence:

```text
draft closure artifacts
-> independent closure verifier returns actual verdict
-> persist actual verdict
-> fresh read-only consistency audit
-> MILESTONE_DONE
-> STOP
```

A clearly pre-verdict draft may contain `PENDING`; a verifier must not fail solely because its own verdict has not yet been returned. Predicted PASS before verifier execution is forbidden. After the verifier returns, stale PENDING is a defect.

For long runs record trustworthy timing/model/lane metrics when observable. Use terminal end-to-end as primary latency. If telemetry is unavailable, say unavailable rather than infer configured intent.

## Promoted milestone state

```text
M1 — Unity Runtime Binding: HUMAN_PROMOTED
M2 — Autonomous Bounded Development: HUMAN_PROMOTED
M3 — Unity Execution Coverage: HUMAN_PROMOTED
M4 — Cross-System Workspace: DEFERRED_WAITING_REAL_SECOND_SYSTEM
M5 — Project Bootstrap & Knowledge Plane: HUMAN_PROMOTED
M6-A — Loader Foundation: HUMAN_PROMOTED
```

M4 remains deferred because no real accepted second interacting QuickGun System exists. Do not manufacture one.

## M5 durable result

Target `ngocphat03/Axit-Code`; execution/audit branch `feature/m5`.

M5 proved pointer-first bootstrap in a materially different repository, target-owned `docs/PLAN.md` authority, fresh-context continuity, one bounded REAL Axit-Code task, root verification, medium-tier delegated execution, and independent closure.

Performance sample:

```text
execution_to_preclosure = 22m 12s
terminal_end_to_end     = 26m 02s
child lanes             = 19
peak useful parallelism = 4
replacements            = 0
REAL repair loops       = 0
```

M5 also hardened post-verdict persistence and lane-count discipline.

## M6-A promoted result — Loader Contract v1

Canonical target technical contract:

```text
ngocphat03/Axit-Code@release:docs/decisions/ADR-002-loader-contract-v1.md
```

Canonical fixture:

```text
.agents/profiles/feature-development.md
```

Validated product branch:

```text
ngocphat03/Axit-Code@feature/m6a-loader
```

Promoted capability:

```text
.agents/{profiles,rules,workflows,skills,knowledge}/**/*.md
-> strict v1 frontmatter validation
-> normalized record kind/id/schemaVersion/markdown/source
-> deterministic repo-relative lexical order
-> duplicate (kind,id) error
-> no success catalog when any ERROR exists
```

Loader v1 intentionally has no precedence/override, lifecycle/status/activation/scope, reference resolution, profile auto-selection, Context Builder, execution permission, Harness bypass, provider behavior, or Unity behavior.

Promotion evidence:

```text
focused tests = 16/16 PASS
package build/typecheck = PASS
root npm run verify = PASS
real canonical fixture = PASS
closure verifier = PASS
post-verdict consistency audit = PASS
product repair loops = 1/2
```

The first closure verifier found a real strict-parser bug involving inline `#` comments. Repair attempt 1/2 fixed it and added regression tests. A later post-verdict audit found only a fixture-manifest terminal-newline representation defect; metadata-only correction and fresh audit passed.

Human-observed Codex UI duration was about `28m 02s`, but durable repository telemetry did not preserve trustworthy start/end timing. Keep this provenance distinction.

Promotion validates M6-A capability; it does **not** imply `feature/m6a-loader` is merged into Axit-Code `release`.

## M6-B — Context Builder Foundation

Current state:

```text
DESIGNED_INTEGRATION_GATED
NOT_AUTHORIZED_FOR_EXECUTION
```

Pointers:

```text
.axit/milestones/M6B-context-builder/selection-review.md
.axit/milestones/M6B-context-builder/plan.md
```

M6-B should be the smallest source-aware Context Builder slice after M6-A. Canonical execution flow says context combines requirement/constraints, project inspection, profile/workflow/skills, relevant source/artifacts, prior task results, and allowed tool descriptors, while preserving provenance and not treating source/Markdown as system policy.

Before any M6-B execution, the selected Axit-Code writable baseline must actually contain the promoted M6-A loader implementation plus ADR-002/fixture and passing target verification. Otherwise:

```text
HARD_BLOCKER: M6A_PRODUCT_BASELINE_NOT_INTEGRATED
```

Do not auto-merge/cherry-pick/rebase/open a PR merely to clear the gate.

M6-B Phase 0 must discover/freeze exact context semantics before implementation, especially input categories, provenance, deterministic ordering, trusted-vs-untrusted content boundary, relevance responsibility, size/token/truncation behavior, reference-resolution boundary, diagnostics, and a real canonical scenario.

Do not automatically include profile auto-selection, semantic/vector search, Harness/Tool Gateway, Run Ledger, provider calls, planning-loop changes, CLI orchestration, Unity integration, Capability/Binding runtime, or M6-C+.

## Git/status and provenance

Game-Studios workspace default:

```yaml
safety:
  check_git_status: false
```

Dirty/untracked/nested Git state is not itself a readiness blocker when false. This never authorizes reset/clean/revert/destructive overwrite.

Evidence provenance remains Workspace/System-aware. For target-repository productization, distinguish canonical product truth from execution metadata and human/UI evidence.

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

Completed detail = milestone report / retrospective / manifests / promotion review.

`milestones-mockup.md` = human/assistant roadmap alignment.

If memory conflicts with newer accepted truth, update memory rather than forcing newer implementation back to an old assumption.
