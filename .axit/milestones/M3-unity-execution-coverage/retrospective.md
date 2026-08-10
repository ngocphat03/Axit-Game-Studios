# M3 — Unity Execution Coverage Retrospective

Date: 2026-08-10
Milestone result: **PASS**
Final closure verification: **PASS**
Human promotion: **HUMAN_PROMOTED**

## What worked

- Live transport discovery preceded `unity.compile` mapping.
- A fresh full QuickGun baseline Unity compile reached zero compiler errors.
- The REAL scenario contract was frozen and independently approved before implementation.
- `M3-REAL-01` reproduced the duplicate-release alias defect, applied a two-file bounded fix/test change, passed 3/3 focused tests, and reacquired a fresh full Unity compile with zero diagnostics.
- Acquisition state stayed separate from product verdict.
- Worker/verifier responsibility separation held.
- No product/baseline repair loop, sub-agent replacement, hard blocker, package change, MCP setup mutation, or framework expansion was required.
- The initial closure verifier caught a real cross-artifact evidence-contract mismatch before technical closure, proving the gate was useful.

## Incident — compile freshness and terminal evidence were conflated

Observed:
The initial closure verifier found that the promoted binding and historical Phase 4 evidence did not consistently distinguish compile-cycle freshness from later terminal readiness.

Root cause:
A Unity 6 compile request returns before terminal evidence, and earlier phase gates did not map every concrete observation to explicit `fresh_cycle_correlated` and `terminal_state_observed` flags.

Self-recovery:
Closure repair loop 1 aligned only the existing `unity.compile` correlation contract, then reacquired both Phase 4 branches and the REAL scenario's Phase 7 compile evidence.

Framework fix:
Freshness may be established by one of the live-discovered accepted signals, but terminal readiness must come from a distinct later observation. Exact project/editor identity is re-resolved after reload and diagnostics are paged completely.

Regression protection:
Verifier inputs must expose both flags explicitly and may not reuse the freshness observation as terminal proof.

## Incident — terminal closure verdict was not persisted

Observed:
The final run output returned `MILESTONE_DONE` / `PASS`, but pushed report, retrospective, and active state still described the milestone as pending final closure verification.

Root cause:
The closure contract ended at `closure verifier -> MILESTONE_DONE` and did not require the verifier's final verdict to be persisted and consistency-checked before terminal output.

Self-recovery:
Human/assistant review used the final run output plus pushed M3 artifacts and canonical QuickGun source to confirm technical PASS without rerunning Unity or product work.

Framework fix:
`.axit/roadmap/milestone-closure.md` now requires:

```text
closure verifier
  -> persist verdict into report/retrospective/active state
  -> fresh read-only consistency audit
  -> MILESTONE_DONE
```

Regression protection:
A console-only terminal verdict while durable files still say `pending verifier` is now a closure defect. Repair the closure artifacts only; do not rerun unaffected product/runtime evidence.

## Incident — child model cost was excessive

Observed:
The M3 run took roughly 3h15m and historical M3 child execution was observed at `gpt-5.6-sol / max`.

Root cause:
The project previously used a flagship/max default for every child role even when exploration, implementation, evidence acquisition, verification, and closure work did not each require the most expensive configuration.

Decision:
Reserve the flagship model for the primary orchestrator and use a durable child routing rule:

```text
primary orchestrator = gpt-5.6-sol / xhigh
all child lanes       = gpt-5.6-luna / medium
```

Framework fix:
`.axit/policies/model-routing.md` is now the canonical workspace policy, routed from `workspace.yaml` and root `AGENTS.md`. Project default sub-agents and the custom verifier are aligned to Luna/medium.

Regression protection:
Children may not silently escalate model class or reasoning effort. Underperformance is handled by distilled context, steer/resume, replacement, decomposition, and fresh evidence. An explicit human instruction is required for any bounded override, and future milestone closure must audit model-routing compliance.

Historical integrity:
M3 still records its actual observed child configuration as Sol/max. The Luna/medium policy applies to future child lanes and is not retroactively claimed for M3.

## Evidence/control review

- REQUIRED versus SUPPORTING classification was appropriate: full Unity compile was REQUIRED for baseline and after the REAL production C# change.
- The intentional compile-error fixture demonstrated `acquired + compilation errors` without conflating acquisition and verdict.
- Stale evidence was not reused after closure repair; affected compile evidence was reacquired.
- The duplicate-release fix is canonical on QuickGun `release` at commit `e2e1b1b6f3b0720d91e51def6b610f5714e17c52`.
- The root regression test and M3 Axit artifacts are pushed on the Workspace branch.
- No extra Unity capability was promoted because Phase 8 found `REQUIRED_NOW: none`.

## Performance review

Historical M3:

```text
primary: Sol / xhigh
children: Sol / max
wall clock: ~3h15m
sub-agent replacements: 0
product/baseline repair loops: 0
closure repair loops: 1
```

Future long milestones should record the same fields plus child count and model override count so the Luna/medium policy can be evaluated against real wall-clock/cost behavior.

Optimization order is now:

```text
better decomposition
-> smaller child context
-> useful parallel read lanes
-> serialized mutation lanes
-> steer/replace/decompose
-> never silent model escalation
```

## Framework changes justified

Required:

- active `unity.compile` mapping;
- explicit Unity 6 freshness/terminal acquisition contract;
- duplicate-release regression test;
- post-verdict closure persistence rule;
- durable model-routing/cost policy.

Not justified:

- new Profile/Skill/Workflow;
- speculative semantic Capability growth;
- automatic `unity.scene.inspect`, `unity.tests.run`, `unity.console.inspect`, `unity.component.inspect`, or `unity.project.inspect` binding;
- M4 execution without a separate accepted plan;
- automatic child escalation to larger models.

## Promotion decision

**HUMAN_PROMOTED.**

M3 does not need a full rerun. M4 remains not started and must be deliberately designed/authorized.
