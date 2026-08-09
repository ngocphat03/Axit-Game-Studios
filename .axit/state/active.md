# Axit Workspace Active State

Updated: 2026-08-10
Status: M2-ready-for-local-execution

## Current milestone

```text
M2 — Autonomous Bounded Development
```

Execution plan:

```text
.axit/milestones/M2-autonomous-bounded-development/plan.md
```

## Promotion state

### M1 — Unity Runtime Binding

Status: **HUMAN_PROMOTED** on 2026-08-10.

Canonical closure artifacts:

```text
.axit/milestones/M1-unity-runtime-binding/report.md
.axit/milestones/M1-unity-runtime-binding/retrospective.md
```

Proven stable Runtime Binding:

```text
.axit/bindings/unity-client/coplaydev-unity-mcp.yaml
```

Mapped capabilities remain exactly:

- `unity.prefab.inspect`
- `unity.serialized-fields.inspect`
- `unity.playmode.verify`

The remaining six declared Unity capabilities remain explicitly unbound.

## M2 goal

Prove Axit can autonomously complete several materially different bounded development tasks without routine user steering.

M2 freezes four scenario contracts before implementation:

- S1 bug fix;
- S2 small feature;
- S3 behavior-preserving refactor;
- S4 Unity-backed bounded scenario.

Prefer REAL scenarios with explicit accepted behavior. Controlled BENCHMARK fallback is allowed only when no safe real candidate has sufficiently explicit intent.

## M2 operating rules

- primary thread orchestrates only when sub-agents are available;
- workers implement; independent verifiers decide PASS/FAIL/BLOCKED;
- maximum two bounded repair/replacement attempts for the same required failure/lane;
- `safety.check_git_status: false` means Git status is not a readiness gate;
- do not add Core Profile/Skill/Workflow, semantic Capability ids, or additional Runtime Binding mappings during M2;
- small proven non-architectural pipeline hardening may be applied with regression protection;
- write compact scenario progress to the M2 scenario manifest and this active state;
- closure must persist M2 report + retrospective and pass closure verification;
- stop after M2 closure for human promotion review; do not start M3 automatically.

## Deferred issue accepted for M2 only

```text
DEFERRED_PRIMARY_REASONING_CONFIG
```

Observed during M1:

- effective primary session: `gpt-5.6-sol / medium`;
- effective sub-agents: `gpt-5.6-sol / max`.

The user explicitly chose to prioritize M2 before repairing the primary reasoning mismatch.

M2 must:

- record the effective values during readiness;
- continue even if this same known mismatch persists;
- keep the issue visible in report/retrospective;
- revisit it at M2 promotion review;
- never silently mark it resolved or carry the exception into M3.

## Stable foundations

Keep unchanged unless M2 produces a demonstrated material flaw:

- Core v1: four Profiles, two Skills, one Workflow;
- Workspace/System v1 root-first routing;
- Capability semantic v1;
- M1 three-capability Unity Runtime Binding;
- manual/user-owned Unity MCP setup;
- milestone closure contract;
- evidence provenance distinction: canonical-pushed vs local/separately-tracked vs ephemeral-runtime.

## Next action

Start a fresh trusted Codex session from repository root and execute the M2 plan continuously until `MILESTONE_DONE` or a declared `HARD_BLOCKER`.
