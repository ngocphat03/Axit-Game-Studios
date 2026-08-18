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

## Why bootstrap must happen before session start

The M5 cost/control policy requires:

```text
primary = gpt-5.6-sol / xhigh
children = gpt-5.6-luna / medium
```

Project-scoped Antigravity model defaults must be present before the fresh target session starts. Writing them after expensive children have already spawned is not evidence that the run complied.

## Required target-local Antigravity defaults

Before launching M5, the local Axit-Code checkout must have a project-scoped Antigravity configuration equivalent to:

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
default_subagent_model = "gpt-5.6-luna"
default_subagent_reasoning_effort = "medium"
interrupt_message = true
```

If Axit-Code already has a project Antigravity config, merge only the accepted M5 routing/safety semantics; do not blindly overwrite unrelated trusted settings.

Do not edit global/user Antigravity configuration to satisfy M5.

## Required independent verifier role

A target-local custom verifier, if used, must resolve to Luna/medium and remain read-only. Equivalent minimum:

```toml
name = "axit-verifier"
description = "Independent read-only verifier for M5 bootstrap and bounded target changes."
model = "gpt-5.6-luna"
model_reasoning_effort = "medium"
sandbox_mode = "read-only"
```

Its instructions must require fresh evidence, forbid implementation fixes, and preserve PASS/FAIL/BLOCKED plus REQUIRED/SUPPORTING semantics.

A role name never authorizes a larger model.

## M5 runbook source

Canonical M5 control plan remains in Axit-Game-Studios:

```text
.axit/milestones/M5-project-bootstrap-knowledge-plane/plan.md
```

For a target-local run, provide that runbook to the Axit-Code session as a local readable file or paste/transfer its accepted contents before execution. Do not ask the target agent to reconstruct M5 from memory.

The transfer itself must not overwrite existing Axit-Code product truth. A temporary/local runbook copy is execution metadata, not product architecture.

## Launch readiness gate

Before starting the long run, verify:

- Antigravity is opened at the exact Axit-Code repository root;
- project-scoped model routing is present before session start;
- primary configured Sol/xhigh;
- child default configured Luna/medium;
- target M5 runbook is locally readable;
- `docs/PLAN.md` is locally readable;
- no M6 productization is authorized;
- no Git push/PR is authorized.

If any required launch item cannot be satisfied without guessing, global configuration, or cloud writes, stop with:

```text
HARD_BLOCKER: TARGET_WORKSPACE_NOT_READY
```

## Fresh-session launch prompt

After the bootstrap is present, start a **fresh trusted Antigravity session at the Axit-Code repository root** and use:

```text
Execute M5 Project Bootstrap & Knowledge Plane from the provided M5 runbook.

Run continuously until MILESTONE_DONE, FAILED, or a declared HARD_BLOCKER.
Use the primary thread only as orchestrator.
All child/sub-agent lanes must remain GPT-5.6 Luna / medium.
Do not silently escalate child model or reasoning.
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

Those artifacts are reviewed before M5 human promotion. Their existence does not authorize M6.
