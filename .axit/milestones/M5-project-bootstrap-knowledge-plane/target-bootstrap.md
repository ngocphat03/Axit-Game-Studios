# M5 Target Bootstrap — Axit-Code

Status: required-before-target-run

This file defines the one-time local bootstrap needed before starting the M5 long run in the real Axit-Code repository.

It is a launch contract, not Axit-Code product architecture.

## Target

```text
repository: ngocphat03/Axit-Code
canonical ref: release
required execution root: trusted local Axit-Code checkout root
```

Do not assume a filesystem path. Do not run product edits from Axit-Game-Studios and do not use GitHub/cloud writes as a substitute for a writable local target.

## Accepted compatibility overlay

Also read:

```text
.axit/milestones/M5-project-bootstrap-knowledge-plane/runtime-compatibility.md
```

That overlay supersedes only the original literal-Luna child availability requirement. All other M5 boundaries remain unchanged.

## Why bootstrap must happen before session start

Project-scoped Codex model defaults must be present before the fresh target session starts. Writing them after children have already spawned is not evidence that the run complied.

Routing policy:

```text
primary = gpt-5.6-sol / xhigh
child preferred = gpt-5.6-luna / medium when supported
child fallback  = gpt-5.6-terra / medium when Luna is unavailable
child Sol       = forbidden without explicit human override
```

The first M5 readiness attempt established that the current child runtime exposes Terra and Sol but not Luna. Therefore the current target bootstrap should use Terra/medium unless a fresh runtime demonstrably exposes Luna before session start.

## Required target-local Codex defaults for the currently observed runtime

Before launching M5, the local Axit-Code checkout should have a project-scoped Codex configuration equivalent to:

```toml
model = "gpt-5.6-sol"
model_reasoning_effort = "xhigh"
plan_mode_reasoning_effort = "xhigh"

approval_policy = "on-request"
approvals_reviewer = "auto_review"
sandbox_mode = "workspace-write"

[sandbox_workspace_write]
network_access = true

[auto_review]
policy = """
Approve routine repository-local work inside the accepted M5 frozen scope: reads, bounded source/test/bootstrap writes, dependency restore using the existing lockfile, build, typecheck, tests, repository verification, and non-destructive inspection.

Do not approve global or machine-wide installation/configuration, sudo/admin escalation, credentials or secrets, production/cloud writes, git push/publish/PR creation, destructive reset/clean/revert, dependency/package-manifest changes that were not explicitly frozen in the REAL task contract, public architecture/state-ownership changes, broad unrelated refactors, or overwriting unrelated current work.
"""

[agents]
enabled = true
max_concurrent_threads_per_session = 4
default_subagent_model = "gpt-5.6-terra"
default_subagent_reasoning_effort = "medium"
interrupt_message = true
```

If a fresh runtime demonstrably exposes Luna children, `default_subagent_model` may instead be `gpt-5.6-luna` while reasoning remains `medium`.

If Axit-Code already has a project Codex config, merge only the accepted M5 routing/safety semantics; do not blindly overwrite unrelated trusted settings.

Do not edit global/user Codex configuration to satisfy M5.

## Required independent verifier role

A target-local custom verifier, if used, must remain read-only and use the same allowed medium child route selected for the current runtime.

For the currently observed runtime:

```toml
name = "axit-verifier"
description = "Independent read-only verifier for M5 bootstrap and bounded target changes."
model = "gpt-5.6-terra"
model_reasoning_effort = "medium"
sandbox_mode = "read-only"
```

If Luna becomes supported, Luna/medium is preferred.

Its instructions must require fresh evidence, forbid implementation fixes, and preserve PASS/FAIL/BLOCKED plus REQUIRED/SUPPORTING semantics.

A role name never authorizes Sol or reasoning above medium.

## M5 runbook source

Canonical M5 control plan remains in Axit-Game-Studios:

```text
.axit/milestones/M5-project-bootstrap-knowledge-plane/plan.md
```

The runtime compatibility overlay is also part of the accepted runbook:

```text
.axit/milestones/M5-project-bootstrap-knowledge-plane/runtime-compatibility.md
```

For a target-local run, provide both accepted files to the Axit-Code session as local readable execution metadata or paste/transfer their accepted contents before execution. Do not ask the target agent to reconstruct M5 from memory.

The transfer must not overwrite existing Axit-Code product truth. A temporary/local runbook copy is execution metadata, not product architecture.

## Launch readiness gate

Before starting the long run, verify:

- Codex is opened at the exact Axit-Code repository root;
- project-scoped model routing is present before session start;
- primary configured Sol/xhigh;
- child default is an allowed medium route supported by the current runtime;
- for the currently observed runtime, Terra/medium is acceptable and should be reported as `COMPAT_TERRA` because Luna was unavailable;
- target M5 runbook and compatibility overlay are locally readable;
- `docs/PLAN.md` is locally readable;
- no M6 productization is authorized;
- no Git push/PR is authorized.

If the target root cannot be established without guessing, stop with:

```text
HARD_BLOCKER: TARGET_WORKSPACE_NOT_READY
```

If neither Luna nor Terra can run children, stop with:

```text
HARD_BLOCKER: MODEL_ROUTING_NOT_EFFECTIVE
```

Do not fall back to child Sol.

## Fresh-session launch prompt

After the bootstrap is present, start a **fresh trusted Codex session at the Axit-Code repository root** and use:

```text
Execute M5 Project Bootstrap & Knowledge Plane from the provided M5 runbook and runtime compatibility overlay.

Run continuously until MILESTONE_DONE, FAILED, or a declared HARD_BLOCKER.
Use the primary thread only as orchestrator.
Use the allowed medium child route for this runtime:
- prefer GPT-5.6 Luna / medium when supported;
- otherwise use GPT-5.6 Terra / medium and record COMPAT_TERRA;
- never use child Sol without explicit human override.
Delegate all delegatable work.
Do not ask for routine confirmations.
Do not commit, push, publish, or open a PR.
Do not start M6.
```

## Handoff back to Game-Studios

At terminal closure, preserve a compact target handoff containing:

```text
report.md
retrospective.md
bootstrap-manifest.md
real-task-manifest.md
final terminal schema
```

The report must state the actual child route used: `PREFERRED_LUNA`, `COMPAT_TERRA`, or explicit `HUMAN_OVERRIDE`.

Those artifacts are reviewed before M5 human promotion. Their existence does not authorize M6.
