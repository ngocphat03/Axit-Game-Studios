# M2 — Autonomous Bounded Development Retrospective

Date: 2026-08-10
Milestone result: PASS
Closure state: M2-done-pending-human-promotion
Closure verifier: `/root/m2_closure_verifier` — PASS

`PASS` records the cross-scenario and independent closure results. Human promotion is still required, and M3 remains unauthorized.

## What worked

- Four frozen 13/13-field contracts ran sequentially without acceptance rewrites: S1 `REAL` bug, S2 `BENCHMARK` feature, S3 `REAL` refactor, and S4 `BENCHMARK` Unity restoration.
- Fresh independent verifiers returned exactly four `PASS` results while implementation workers did not self-certify.
- Focused deterministic evidence matched each frozen criterion; REQUIRED evidence stayed distinct from SUPPORTING full-project coverage.
- S4 recovered from a real evidence-target miss, reacquired fresh evidence, restored the fixture, discarded runtime mutation, and ended idle in Edit Mode.
- The restricted-input continuity probe returned `PASS` and reconstructed current state without conversation replay.
- Existing steer/replacement limits recovered two stalled lanes without user escalation.
- Scope discipline held: scenario repair loops `0`, candidate replacements `0`, and no Core/Profile/Skill/Workflow/Capability/Binding/configuration/decision-log mutation.
- Independent closure verifier `/root/m2_closure_verifier` proved C1–C10, reran current S1/S2/S3 evidence at `2/2`, `5/5`, and `3/3`, confirmed current S4 idle Edit Mode and serialized `1/2/2`, and reported no findings.

## Incidents and root causes

### Recurring runtime-wrapper miss

Observed:
The S4 worker's `Bot/Model/Head/Hair` target and one bounded retry returned `target_not_found` without mutation. The same omitted `GameEnvironment/` wrapper assumption had occurred in M1.

Root cause:
A prefab-derived hierarchy was treated as if it were the complete live scene hierarchy.

Self-recovery:
The worker performed mandatory cleanup. A fresh verifier used local read-only scene/source evidence to derive `GameEnvironment/Bot/Model/Head/Hair`, then acquired the required Play Mode evidence on its first attempt and confirmed idle Edit Mode cleanup.

Framework/config fix:
None automatically. Target diagnostics/enumeration would alter the frozen Runtime Binding/Capability boundary.

Regression protection:
The recurring miss is persisted as an accepted limitation for human M3 review, while the existing requirement to freshly resolve runtime identity remains in force.

### Manifest-author and contract-reviewer stalls

Observed:
The original manifest author and initial independent contract reviewer were each steered, interrupted, and replaced once: two sub-agent replacements total.

Root cause:
Both lanes stalled before returning complete durable work; no product, architecture, or evidence blocker was found.

Self-recovery:
Fresh distilled-context replacements completed the manifest and returned `APPROVE_FOR_FREEZE` with no mandatory corrections.

Framework/config fix:
None. The current steer/replace policy handled both events within budget.

Regression protection:
Existing replacement accounting and the two-attempt ceiling remain sufficient; a new Workflow is not justified.

### Candidate-discovery interruption

Observed:
The discovery-candidate lane was steered and interrupted without replacement.

Root cause:
The lane became unnecessary after parallel evidence supplied sufficient bounded candidates.

Self-recovery:
Execution continued from the complete parallel evidence without manufacturing a replacement or cherry-picking a later scenario.

Framework/config fix:
None.

Regression protection:
None needed for this one-off interruption; the no-replacement rationale is durably recorded.

### Supporting cleanup observer unavailable

Observed:
The cleanup observer returned `UNAVAILABLE` after one read because it could not parse the response wrapper.

Root cause:
The supporting observer assumed a different response shape.

Self-recovery:
No mutation occurred. Direct verifier cleanup evidence, which was REQUIRED, already proved idle ready Edit Mode and carried the verdict.

Framework/config fix:
None; this was a one-off SUPPORTING lane issue.

Regression protection:
Required cleanup remains attached to the verifier result rather than delegated solely to a supporting observer.

### Recurring full-project compile gap

Observed:
S1–S3 passed focused standalone evidence, but full Unity/project builds were skipped.

Root cause:
The frozen contracts made focused deterministic behavior and wiring REQUIRED while full Unity integration was only SUPPORTING; no reviewed compile binding was in the M1 three-capability slice.

Self-recovery:
Verifiers bounded their claims and retained the skipped builds as explicit residual risks rather than substituting or inventing a Capability.

Framework/config fix:
None automatically.

Regression protection:
Treat the gap as an accepted limitation. Human M3 review may consider a compile binding only if a future frozen contract makes compilation REQUIRED.

### Deferred primary reasoning configuration

Observed:
The project is configured for `gpt-5.6-sol / max` while the M2 plan and Antigravity configuration vocabulary expect `xhigh`; exact live primary/session metadata and service tier were unavailable.

Root cause:
Configured project intent, plan wording, and observable live metadata do not currently provide one trusted effective value. The [GPT-5.6 latest-model guide](https://developers.openai.com/api/docs/guides/latest-model) documents `max` support, while the [Antigravity configuration reference](https://developers.openai.com/antigravity/config-reference/) lists reasoning effort through `xhigh`.

Self-recovery:
`DEFERRED_PRIMARY_REASONING_CONFIG` remained visible and non-blocking for M2 only. No effective value was invented.

Framework/config fix:
None in M2; automatic configuration editing was prohibited.

Regression protection:
Before M3, a human must either change the project setting from `max` to `xhigh` and start a fresh trusted task, or explicitly re-decide the setting.

## Setup/pipeline assumptions that failed

- Prefab-relative hierarchy did not include the live `GameEnvironment/` scene wrapper.
- The supporting cleanup observer assumed a response-wrapper shape it could not parse.
- Focused standalone C# mechanisms were available, but full Unity/project compile coverage was not part of the reviewed binding.
- Configured reasoning metadata could be read, but exact live session metadata and service tier could not be confirmed.
- The S2 verifier briefly saw `NOT_STARTED` before the normal checkpoint write; this was expected timing, not stale final state or an incident.

No hard stop fired too early or too late. In particular, `target_not_found` was correctly handled as bounded evidence recovery rather than falsely escalated to `UNITY_MCP_NOT_READY`.

## Evidence/control review

- REQUIRED versus SUPPORTING classification was correct. Focused scenario criteria and S4 cleanup were REQUIRED; S1–S3 full Unity builds and the cleanup observer were SUPPORTING.
- Verifiers did not overclaim beyond focused contracts. The compile and broad-integration limits remain visible.
- Stale evidence was not reused after the S4 path failure; the fresh verifier re-derived the path and acquired current serialized/runtime/cleanup evidence.
- Worker and verifier lanes remained independent for all four scenarios.
- The two stalled lanes were recovered by orchestration without user escalation. Candidate discovery was interrupted without replacement because other evidence was sufficient.
- S4 fixture setup/restoration was not mislabeled as a repair loop. Scenario repair loops remained `0` and candidate replacements remained `0`.
- No secrets, production/cloud writes, destructive action, or admin escalation entered the run.

## Context/memory review

The continuity probe received only `workspace.yaml`, `active.md`, the M2 plan, and the scenario manifest. It returned `PASS` and reconstructed outcomes, next action, incidents, stable framework layers, the three-capability M1 binding, deferred reasoning issue, cleanup obligation, provenance, and M3 prohibition without hidden chat context.

This closure record persists the remaining promotion-critical context: two sub-agent replacements, the no-replacement discovery interruption, S4's recovered runtime path and cleanup, the two Phase-7 accepted limitations, and the reasoning/config decision required before M3. No decision-log entry was added because Phase 7 applied no durable rule or configuration change.

## Auditability review

- `canonical-pushed`: no M2 evidence is claimed in this class.
- `local-or-separately-tracked`: scenario source, tests, ADR/config, prefab, fixture, and static inspection evidence.
- `ephemeral-runtime`: standalone compile/test commands, live Unity acquisition, Play Mode behavior, and editor cleanup observations.

The report preserves these classes without forcing Git tracking. The S4 runtime proof is specifically bounded to multiplier `2`, pre-armor damage `20`, armor `0.8`, final damage `4`, and health `3 -> -1` at `GameEnvironment/Bot/Model/Head/Hair`.

## Framework changes justified

Required:

No automatic framework/config hardening was justified. The smallest safe outcome was to persist the recurring runtime-wrapper and compile-coverage gaps as accepted limitations with explicit human M3 review gates, and to keep `DEFERRED_PRIMARY_REASONING_CONFIG` unresolved until a human repairs or re-decides it.

Not justified:

- any Core, Profile, Skill, or Workflow mutation;
- any semantic Capability or Runtime Binding addition/change;
- automatic target diagnostics/enumeration;
- automatic Unity compile binding;
- project configuration or decision-log mutation;
- broad product cleanup or public-contract change.

## Promotion recommendation

PROMOTE

The recommendation rests on exactly four independent scenario `PASS` results, continuity `PASS`, zero scenario repair loops, zero candidate replacements, successful bounded recovery, restored Unity cleanup state, evidence-bounded claims, and independent closure verification `PASS` with no findings. Stop for human promotion and reasoning-config review; M3 remains prohibited and not started.
