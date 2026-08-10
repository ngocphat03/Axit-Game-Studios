# Axit Workspace Active State

Updated: 2026-08-11
Status: M6A-designed-dependency-gated

## Roadmap state

```text
M1 — Unity Runtime Binding: HUMAN_PROMOTED
M2 — Autonomous Bounded Development: HUMAN_PROMOTED
M3 — Unity Execution Coverage: HUMAN_PROMOTED
M4 — Cross-System Workspace: DEFERRED_WAITING_REAL_SECOND_SYSTEM
M5 — Project Bootstrap & Knowledge Plane: HUMAN_PROMOTED
M6-A — Loader Foundation: DESIGNED_DEPENDENCY_GATED
M6-B+ — NOT_AUTHORIZED
```

M5 promotion review:

```text
.axit/milestones/M5-project-bootstrap-knowledge-plane/promotion-review.md
```

M5 target audit branch:

```text
ngocphat03/Axit-Code@feature/m5
```

Promoted M5 result: bootstrap review PASS, continuity PASS, REAL task PASS, repository verification PASS, final closure verifier PASS. Closure persistence was repaired in metadata only; no full rerun was required.

Performance accounting retained from M5:

```text
execution_to_preclosure = 22m 12s
terminal_end_to_end     = 26m 02s
child lanes             = 19
peak useful parallelism = 4
replacements            = 0
REAL repair loops       = 0
human model overrides   = 0
child route             = COMPAT_TERRA / medium
```

## Current designed slice

```text
M6-A — Profile / Rule / Workflow / Knowledge Loader Foundation
```

Plan:

```text
.axit/milestones/M6A-loader-foundation/plan.md
```

Selection/dependency review:

```text
.axit/milestones/M6A-loader-foundation/selection-review.md
```

M6-A is designed but **not ready for long implementation execution**.

## Mandatory dependency gate

At current review time, Axit-Code PR #5:

```text
PR: #5 docs: establish AxitCode and Game Design rules
state: open
draft: true
merged: false
role: declarative rule/catalog foundation intended to precede loader work
```

Do not automatically merge, rebase, close, or treat PR #5 as canonical.

Before M6-A can run, human review must deliberately resolve the loader-input foundation. If canonical accepted source still lacks the required convention, M6-A readiness returns:

```text
HARD_BLOCKER: KNOWLEDGE_FOUNDATION_NOT_ACCEPTED
```

## M6-A scope boundary

M6-A implements only the smallest accepted declarative loader foundation under the authoritative Axit-Code runtime boundary.

It does not implement:

```text
Context Builder
Harness / Tool Gateway
Run Ledger
provider integration
CLI run orchestration
Unity integration
Capability/Binding runtime
M6-B
```

Do not copy Game-Studios `.axit` wholesale into Axit-Code. Productize proven semantics as Axit-Code-native contracts.

## Model / cost policy

Canonical policy:

```text
.axit/policies/model-routing.md
```

Accepted hierarchy:

```text
primary = gpt-5.6-sol / xhigh
child preferred = gpt-5.6-luna / medium when supported
child compatibility fallback = gpt-5.6-terra / medium when Luna is unavailable
child Sol = forbidden without explicit current human override
```

M5 demonstrated `COMPAT_TERRA` successfully. This does not permanently replace the preference for Luna if a future runtime exposes it.

## Stable execution rules

- Automation remains high inside one accepted milestone and stops for human review.
- Actual final verifier verdict must be persisted after it returns, then consistency-audited before `MILESTONE_DONE`.
- Use terminal end-to-end duration for milestone latency comparison; pre-closure duration may be retained as a secondary metric.
- Spawn child lanes for materially independent work or verification independence, not merely one new lane per phase/checkpoint.
- Preserve unrelated filesystem state; do not reset/clean/revert unrelated work.
- Evidence-driven evolution remains mandatory; no speculative Profile/Skill/Workflow/Capability growth.

## QuickGun / Unity foundation

Promoted Unity mappings remain exactly:

```text
unity.prefab.inspect
unity.serialized-fields.inspect
unity.playmode.verify
unity.compile
```

Five remaining Unity capabilities stay unbound until demonstrated need. M6-A must not expand Unity bindings.

## Next action

Human + assistant review Axit-Code PR #5 and decide whether its rule/catalog foundation should be accepted, revised, or superseded.

Do not start the M6-A long run until that dependency decision is explicit. Do not start M6-B or revive M4 automatically.
