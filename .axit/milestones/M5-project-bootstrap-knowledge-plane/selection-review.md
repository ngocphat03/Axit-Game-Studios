# M5 Selection Review

Date: 2026-08-11
Status: accepted-for-design

## Decision

Defer M4 — Cross-System Workspace until a real accepted workspace contains at least two interacting Systems with executable boundary evidence.

Select M5 — Project Bootstrap & Knowledge Plane as the next milestone.

Target:

```text
ngocphat03/Axit-Code
canonical ref: release
```

## Why M4 is deferred

The current QuickGun Workspace registers one real System, `unity-client`. Creating a fake backend/CMS/service or a synthetic cross-system benchmark would violate the evidence-driven evolution rule.

M4 remains valuable and is not cancelled. It resumes when a genuine second interacting System and accepted provider/consumer boundary exist.

## Why Axit-Code is the M5 target

Axit-Code is materially different from QuickGun:

- TypeScript workspace rather than Unity gameplay project;
- canonical product roadmap in `docs/PLAN.md`;
- authoritative Agent Runtime under the AxitCode product architecture;
- repository-native build/typecheck/test/verify surface;
- current work is still before the full Feature Development + Unity end-to-end product slice.

This makes it a useful real test of whether Axit bootstrap/context can transfer across project types without copying the Game-Studios structure wholesale.

## Control boundaries

- M5 is bootstrap/Knowledge Plane validation, not M6 productization.
- No speculative Profile/Skill/Workflow catalog.
- No automatic M4 or M6 start.
- Primary is Sol/xhigh; every child lane is Luna/medium unless the user explicitly authorizes a bounded override.
- M5 must execute product work from a trusted writable Axit-Code local checkout; no guessed path and no cloud-write substitute.
