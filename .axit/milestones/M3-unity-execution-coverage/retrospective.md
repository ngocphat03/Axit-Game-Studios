# M3 — Unity Execution Coverage Retrospective

Date: 2026-08-10
Milestone result: **PASS candidate**, pending final independent closure re-verification
Human promotion: **NOT_PROMOTED**

## What worked

- Phase 0 verified the effective fresh primary as `gpt-5.6-sol / xhigh`
  from authoritative root turn context rather than trusting project config
  alone; sub-agent execution was observed at `gpt-5.6-sol / max`.
- Live discovery preceded mapping. The binding names only concrete CoplayDev
  operations that were observed in the current interface.
- The full QuickGun baseline compile and the post-product-change compile both
  reached independently verified terminal evidence with zero compiler errors.
- The REAL contract was frozen and independently approved before source work.
  Pre-fix reproduction, a two-file bounded fix, 3/3 final deterministic tests,
  source inspection, and fresh Unity compilation stayed aligned with that
  contract.
- Acquisition state remained distinct from verdict: compiler errors were
  acquired evidence, while `unavailable`, `denied`, and `transport_error`
  remained non-verdict acquisition states.
- The initial closure verifier caught a cross-artifact contract/evidence
  mismatch before `MILESTONE_DONE`, proving the final gate added material
  independence rather than repeating earlier phase verdicts.
- No product/baseline repair loop, sub-agent replacement, hard blocker,
  framework redesign, or user micromanagement was needed. One bounded closure
  repair aligned the existing binding and reacquired affected evidence.

## Incidents and root causes

### Initial final-closure verification failed

Observed:
The first final-closure verifier returned `FAIL`. The promoted binding required
a sampled post-request nonterminal boolean state, while Phase 1 live discovery
and Phase 4/7 evidence also relied on advanced compile/domain-reload markers.
Historical Phase 4 additionally reused one post-request state payload for both
freshness correlation and terminal readiness.

Root cause:
The static binding wording and the actually exercised acquisition protocol had
diverged. Earlier verification checked their local phase goals without a
flag-by-flag, cross-artifact mapping of each evidence observation to the
binding's required freshness and terminal conditions.

Self-recovery:
Closure repair loop 1 changed only the existing `unity.compile` correlation
contract to accept the three live-discovered freshness alternatives, while
requiring a distinct later terminal observation. An independent binding
verifier returned `PASS`; Phase 4 reacquired both live branches and received
independent revalidation `PASS`; Phase 7 freshly reacquired 3/3 tests and a
zero-error full compile and received independent `PASS`.

Framework/config fix:
Keep freshness and terminal completion as separate explicit flags:
`fresh_cycle_correlated` and `terminal_state_observed`. No new Capability,
operation, permission, outcome, or mapped scope was added.

Regression protection:
Verifier scripts must map concrete observations to both explicit flags. A
freshness observation may not also satisfy terminal state; request acceptance
or a transient not-ready read is insufficient.

Why earlier gates missed it:

- Phase 2 validated candidate scope, structure, and outcome boundaries but did
  not map every Phase 1 live-discovered freshness alternative into the binding
  conditions; sampled nonterminal state therefore became mandatory by mistake.
- Phase 4 and Phase 7 validated acquisition outcomes and final compiler state
  without mapping each observation to explicit binding flags; historical
  Phase 4 also reused its freshness snapshot as terminal proof.
- Phase 9 checked promotion scope and relied on the earlier independent phase
  verdicts instead of re-auditing their underlying observations against the
  exact promoted correlation contract.
- The initial closure verifier was the first gate to compare discovery,
  binding text, Phase 4, Phase 7, and promotion evidence end to end.

### Unity 6 request returned before terminal compile evidence

Observed:
`refresh_unity(... compile=request, wait_for_ready=false)` accepted the request
before compilation/reload and complete diagnostics were terminal.

Root cause:
Request acknowledgement describes dispatch, not completion. Unity 6 domain
reload also invalidates assumptions about uninterrupted state reads or
continuity of acquisition-local timing fields.

Self-recovery:
The repaired protocol polls within bounded time, establishes freshness through
sampled nonterminal state, an advanced compile marker, or an advanced
domain-reload marker, then requires a distinct later terminal-ready snapshot,
re-resolves exact project/editor identity, and pages diagnostics to completion.

Framework/config fix:
The active `unity.compile` composite explicitly records that request
acceptance is not success, names the three freshness alternatives, and
separately requires terminal observation.

Regression protection:
Clear the Console before every request, correlate only inside that acquisition
window, do not reuse the freshness snapshot as terminal proof, re-resolve
identity after reload, and accept diagnostics only after complete paging.

### Phase 4 historical captures required replacement evidence

Observed:
The first temporary error-fixture attempt did not return a complete unique
marker diagnostic set. The later historical error/clean transcript used one
post-request snapshot for both freshness and terminal readiness.

Root cause:
The first fixture had not yet obtained its generated `.meta`; separately, the
historical audit did not enforce observation independence even though the
repaired contract now makes it explicit.

Self-recovery:
Closure repair loop 1 used one new fixture window. Both error and clean
branches used distinct observation A for freshness and later observation B for
terminal readiness. The exact `CS1029` error and final `total=0` diagnostics
were acquired; fixture, `.meta`, and marker absence were verified; independent
Phase 4 revalidation returned `PASS`.

Framework/config fix:
The existing correlation contract now states observation independence, and
the audit labels historical evidence superseded. This remained a closure
evidence repair, not a product repair.

Regression protection:
The Phase 4 artifact records separate A/B observations for both branches,
fixture collision checks, unique marker evidence, mandatory `.cs`/`.meta`
cleanup, and independent revalidation.

### Phase 7 required post-repair reacquisition

Observed:
The pre-closure Phase 7 evidence could not close the repaired binding contract
because the earlier verifier had not mapped separate freshness and terminal
flags.

Root cause:
The evidence/verifier interface described a terminal correlated window but did
not expose the required flag-level observation independence.

Self-recovery:
A fresh post-repair verifier reran all three deterministic tests and acquired a
new full compile. An advanced reload marker established freshness, a distinct
later state read established terminal readiness, exact identity matched, and
complete diagnostics returned `total=0`. No product repair or compile
reacquisition retry occurred.

Framework/config fix:
Verifier inputs now expose and assert `fresh_cycle_correlated` and the distinct
later `terminal_state_observed` separately.

Regression protection:
A verifier must map evidence to both flags, reject stale/pre-repair windows,
and reacquire instead of lowering the criterion or inferring success from
request acknowledgement.

### Recoverable CoplayDev reload warning and transient read interruption

Observed:
A CoplayDev WebSocket/package warning and a transient failed state read
occurred around Unity domain reload.

Root cause:
The adapter connection can briefly cycle while Unity reloads scripts even
when user configuration and the intended editor remain valid.

Self-recovery:
Bounded reads re-established the exact same project/editor and terminal state
without transport setup or configuration changes.

Framework/config fix:
Classify the warning as recoverable transport/package noise only when exact
identity, terminal state, and complete compiler diagnostics are subsequently
re-established. It is neither a compiler error nor automatic proof of
`UNITY_MCP_NOT_READY`.

Regression protection:
Preserve bounded recovery and require a complete correlated set; escalate only
when the user-owned transport cannot be restored within the accepted budget.

## Stalls, replacements, and hard-stop behavior

- Sub-agent stalls requiring recovery: **0**.
- Sub-agent replacements: **0**. Fresh role-separated agents were normal
  delegation, not replacements.
- Product/baseline repair loops: **0**.
- Closure repair loops: **1**.
- No hard blocker fired. In particular, transient reload instability was not
  escalated prematurely to `UNITY_MCP_NOT_READY`.
- No hard stop fired late: product intent and architecture were settled by the
  frozen contract, destructive/secret/admin scope never became necessary, and
  no required failure persisted through the recovery budget.

## Setup/pipeline assumptions that failed

- The assumption that compile request acceptance could stand in for terminal
  success was rejected by live behavior.
- The assumption that one state/timestamp stream remains continuous across a
  Unity 6 domain reload was unsafe; correlation must tolerate reload and then
  re-resolve identity.
- A diagnostic read without an explicit clear-before-request boundary was not
  sufficient to prove freshness.
- A prose conclusion such as "terminal correlated" was not enough for
  verification; evidence must map to explicit freshness and terminal flags,
  and those flags require separate observations.

The user-owned Unity MCP setup itself did not fail. The run reacquired the
exact QuickGun root, editor version, repository/ref/commit, and Edit Mode state
instead of assuming them. No package, bridge, endpoint, or global configuration
was installed or repaired.

## Evidence/control review

- REQUIRED vs SUPPORTING classification was correct. Full Unity compile was
  REQUIRED for the Phase 3 baseline and after the REAL production C# change;
  the 3/3 focused deterministic harness and ownership inspection were REQUIRED
  for the frozen scenario. Prefab, serialized-field, Play Mode, or a Unity Test
  Framework run was not required by this source-only acceptance.
- The verifiers did not overclaim acquisition as verdict. The Phase 4
  `acquired + compilation errors` row demonstrates that evidence can support a
  failed compile criterion while still proving the transport acquired it.
- Stale or insufficient evidence was not reused after the closure `FAIL`.
  Historical Phase 4/7 evidence remains incident chronology only. Phase 4
  reacquired both branches, and Phase 7 reacquired every affected REQUIRED
  result; neither event was counted as a product/baseline repair loop.
- Worker/verifier independence held through repair: a bounded binding worker
  was followed by an independent binding verifier; current Phase 4 and Phase 7
  evidence each received fresh independent verification; the final closure
  re-verifier remains separate. Worker evidence was treated as handoff, not
  final verdict.
- No sub-agent escalated an issue the orchestrator should have resolved; the
  bounded transport/evidence recoveries stayed inside the accepted envelope.
- Runtime Binding Case 8 was respected. No path-addressed product/runtime
  operation occurred, and exact project/editor identity was still re-resolved
  after compile reload.

## Context/memory review

Promotion-critical context is now durable in:

- this report and retrospective;
- the frozen REAL scenario manifest;
- the Phase 4 acquisition-regression artifact;
- the Phase 8 demonstrated-gap analysis;
- the active binding, System sidecar, and Runtime Binding checklist; and
- compact `.axit/state/active.md` routing.

Project memory was corrected from the stale pre-M3 statement that
`unity.compile` was unbound. The durable incident log now records only the
smallest already-adopted Unity 6 compile acquisition rule: request acceptance
is not terminal evidence; map one accepted freshness alternative to
`fresh_cycle_correlated`, then map a distinct later observation to
`terminal_state_observed`; re-resolve identity and page diagnostics completely.
No session identifier, endpoint, or instance hash was made durable.

The earlier primary-reasoning incident is resolved for M3 because the
authoritative fresh root turn context reported `gpt-5.6-sol / xhigh`; future
milestones should still verify effective settings at readiness rather than
trust config alone.

## Auditability review

- `workspace-canonical-pushed`: no new M3 artifact or edit is claimed in this
  class. Existing M1/M2 promotion records are used only as durable
  authorization pointers.
- `system-canonical-pushed`: the unchanged QuickGun baseline is System
  `unity-client`, repository `ngocphat03/QuickGun-MVP`, ref `release`, commit
  `c35143a6ea72dd17e591e67b1e965e10a0b15a27`.
- `local-or-separately-tracked`: the current production source delta, root
  test delta, reviewed binding/checklist/sidecar state, and M3 documentation.
  No new commit or push is claimed.
- `ephemeral-runtime`: effective turn metadata, standalone test execution,
  live Unity identity/state, compile/reload correlation, and diagnostics.

This distinction makes the result reviewable without pretending the root
Workspace owns the System's Git history or forcing current local changes into
a repository.

## Framework changes justified

Required:

- activate only the demonstrated `unity.compile` mapping;
- preserve the clear/request/freshness/separate-terminal/re-resolve/page
  acquisition procedure and acquisition-versus-verdict boundary in the
  existing Runtime Binding layer;
- require verifier scripts to map explicit freshness and terminal flags;
- preserve the duplicate-release regression test;
- update durable memory and compact current-state routing.

Not justified:

- a new Profile, Skill, Workflow, semantic Capability, package, transport, or
  framework layer;
- a separate `unity.project.inspect` or `unity.console.inspect` mapping for
  compile-internal identity and diagnostic suboperations;
- speculative promotion of `unity.tests.run`, `unity.scene.inspect`,
  `unity.component.inspect`, or any other unbound Capability;
- generalized audio-pool policy, unrelated cleanup, or M4 execution.

## Promotion recommendation

**PROMOTE**, after final independent closure re-verification and explicit human
review. `MILESTONE_DONE` would mean M3 execution/closure finished; it would not
mean `HUMAN_PROMOTED`. M4 must not begin automatically.
