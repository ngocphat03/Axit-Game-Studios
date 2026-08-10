# M3 — Unity Execution Coverage Report

Status: **candidate for `MILESTONE_DONE`**, pending final independent closure re-verification
Review state: pending-human-review-after-final-closure-gate
Human promotion: **NOT_PROMOTED**
Date: 2026-08-10

`MILESTONE_DONE` is the execution/closure terminal state proposed by this
report. It is not `HUMAN_PROMOTED`, does not authorize M4, and is not issued
until a fresh independent closure verifier accepts the repaired artifacts and
current evidence boundaries below.

## Capability proven

M3 demonstrated that Axit can acquire trustworthy full-project Unity script
compilation evidence for the exact current QuickGun editor/project and use it
as REQUIRED evidence after a bounded REAL production C# fix.

The live-discovered `unity.compile` acquisition is a composite:

1. resolve exact current instance, project, and editor state;
2. clear the Unity Console to open a fresh diagnostic window;
3. call `refresh_unity(mode=force, scope=scripts, compile=request,
   wait_for_ready=false)`;
4. establish `fresh_cycle_correlated` through one of three live-discovered
   alternatives—sampled nonterminal state, an advanced compile marker, or an
   advanced domain-reload marker—then establish `terminal_state_observed`
   from a distinct later state snapshot;
5. re-resolve exact instance/project identity after reload; and
6. page detailed error/warning diagnostics from cursor zero through
   `nextCursor == null`.

Request acceptance is not compilation success. Acquisition outcome remains
separate from the verifier's product/criterion verdict.

## Scenarios executed

| Phase/scenario | Result | Durable outcome |
|---|---|---|
| Phase 0 readiness | `READY` | Authoritative fresh root turn context reported effective primary `gpt-5.6-sol / xhigh`; sub-agent execution reported `gpt-5.6-sol / max`. The exact QuickGun project was reachable in safe Edit Mode. |
| Phase 1 live discovery | complete | The current CoplayDev interface supplied the composite operation contract above; no operation name was inferred. |
| Phase 2 candidate mapping | independent `PASS` | Only `unity.compile` was added to the reviewed candidate scope; semantic ids, permission boundaries, outcome classes, and the original three mappings were preserved. |
| Phase 3 baseline compile | independent `PASS` | A fresh full QuickGun Unity compile reached terminal evidence with zero compiler errors. A CoplayDev WebSocket warning during reload was recoverable transport/package noise, not a project compiler error or setup blocker. |
| Phase 4 acquisition regression, closure-repair reacquisition | independent revalidation `PASS` | Reacquired error and clean branches each used distinct freshness observation A and later terminal observation B. The unique temporary `CS1029` fixture proved `acquired + compilation errors`; fixture, generated `.meta`, and marker cleanup were verified; final diagnostics returned `total=0`. |
| `M3-REAL-01` contract | `FROZEN_APPROVED` before implementation | The accepted defect was duplicate `AudioSourcePool.Release` ownership causing one source to back two outstanding leases. |
| `M3-REAL-01` implementation and fresh post-repair Phase 7 verification | independent `PASS` | Pre-fix reproduction failed for the expected alias reason; the current deterministic harness passed 3/3. An advanced domain-reload marker established compile freshness, a distinct later snapshot established terminal state, exact identity matched, and complete diagnostics returned `total=0`. No product repair or compile reacquisition retry occurred. |
| Phase 8 gap analysis | complete | `REQUIRED_NOW: none`; no additional binding was authorized. |
| Phase 9 binding promotion | independent `PASS` | `unity.compile` became active beside the three M1 mappings; the other five declared capabilities remain unbound. |
| Initial final-closure verification | `FAIL` | The promoted binding required a sampled nonterminal boolean state even though Phase 1 discovery and marker-based Phase 4/7 evidence used other valid freshness signals; historical Phase 4 also reused one snapshot for freshness and terminal state. The gate correctly withheld `MILESTONE_DONE`. |
| Closure repair loop 1 | binding repair independent `PASS` | The existing `unity.compile` mapping now accepts the three discovered freshness alternatives and separately requires a later terminal snapshot. No operation, permission, outcome, verdict boundary, semantic Capability, or mapped/unbound partition changed. |

The REAL scenario changed only:

- `src/QuickGun-MVP/Assets/QuickGunCore/Scripts/Services/AudioSourcePool.cs`
  — enqueue only when `_active.Remove(source)` proves an active lease;
- `tests/QuickGun-MVP/AudioSourcePoolTests.cs`
  — add the duplicate-release/non-alias regression and retain the two prior
  ownership cases.

No public signature, state owner, package, asset, scene, prefab, binding, or
semantic Capability changed as part of the product fix.

## Evidence summary

### Runtime and repository identity

- Exact Unity project: `QuickGun-MVP` at
  `/Volumes/FatDisk 1/DataProfiles/DataAXit/UnityProject/Axit-Game-Studios/src/QuickGun-MVP`.
- Editor version: Unity `6000.4.8f1`; acquisition began and ended in safe Edit
  Mode.
- System baseline: `unity-client`, repository
  `ngocphat03/QuickGun-MVP`, ref `release`, commit
  `c35143a6ea72dd17e591e67b1e965e10a0b15a27`.
- Local `HEAD` and local `origin/release` both resolved to that commit during
  the run; no network fetch was performed.

### Required evidence and verdict boundaries

- Baseline compilation: fresh `acquired + compilation succeeds`; independent
  compile criterion `PASS`; zero compiler errors.
- Acquisition regression: reacquired clean and compiler-error branches each
  used separate freshness and terminal observations; independent Phase 4
  revalidation returned `PASS`. Non-acquired states remained acquisition
  classifications rather than product failures.
- REAL scenario deterministic evidence: 3/3 current tests passed with
  warnings as errors against the actual production source.
- REAL scenario Unity evidence: fresh post-repair full-project compile after
  the final production edit; reload marker
  `1786369155112 -> 1786373156412` established freshness, a distinct later
  snapshot established terminal-ready state, exact pre/post identity matched,
  and complete diagnostics returned `total=0`; independent scenario `PASS`.
- Binding repair evidence: structural contract alignment preserved the same
  four active/five unbound partition and received independent `PASS`.
- Case 8: no path-addressed product/runtime operation was used in M3. Exact
  project/editor identity was nevertheless resolved before and re-resolved
  after compile reload. No runtime hierarchy was guessed or persisted.

### Evidence provenance

| Material evidence | Narrowest truthful class | Boundary |
|---|---|---|
| Unchanged QuickGun baseline at the exact commit above | `system-canonical-pushed` | System `unity-client`; `ngocphat03/QuickGun-MVP`; `release`; `c35143a6ea72dd17e591e67b1e965e10a0b15a27`. |
| Current `AudioSourcePool.cs` delta, root test delta, binding/sidecar/checklist state, and M3 milestone artifacts | `local-or-separately-tracked` | Current filesystem evidence; none is claimed newly committed or pushed by M3. The production delta sits on top of the canonical System baseline. |
| Effective turn configuration, standalone test process, live Unity identity/state, reload correlation, and diagnostics | `ephemeral-runtime` | Valid only for the observed acquisition/run window; no live connection state is canonicalized. |

No new M3 evidence is promoted to `workspace-canonical-pushed` without proof.
The existing M1 `HUMAN_PROMOTED` report and M2 `HUMAN_PROMOTED` promotion
review are durable authorization pointers, not claims that the current M3
edits were pushed.

## Failures and recovery

- Product/baseline repair loops: **0**.
- Closure repair loops: **1**.
- Sub-agent replacements: **0**; no agent was replaced and no lane required a
  recovery escalation.
- Hard blockers: **none fired**.
- Initial final-closure verification returned `FAIL` because the promoted
  binding's mandatory sampled-nonterminal wording did not match the
  live-discovered marker alternatives, and historical Phase 4 used one state
  snapshot for both freshness and terminal proof.
- Closure repair loop 1 aligned only the existing binding/checklist contract,
  then independently passed binding verification. Phase 4 acquired one new
  fixture window with distinct A/B observations for both error and clean
  branches and independently revalidated `PASS`. The fixture, `.meta`, and
  marker are absent.
- Phase 7 reacquired all current REQUIRED evidence after the binding repair:
  3/3 tests and a fresh, separately terminalized, exact-identity compile with
  `total=0`. This current acquisition needed no compile reacquisition retry.
- Earlier Phase 4 and Phase 7 capture/reacquisition events remain incident
  chronology only; none is reused as current closure evidence.
- Unity 6 domain reload briefly interrupted transport reads and produced a
  recoverable CoplayDev WebSocket warning. Bounded re-resolution recovered the
  same target without setup/configuration changes, so
  `UNITY_MCP_NOT_READY` was correctly not raised.

## Framework/config changes caused by incidents

- The reviewed binding now contains only the proven `unity.compile` composite
  in addition to the M1 three-mapping slice.
- Runtime Binding validation records that a Unity 6 compile request returns
  before terminal evidence, requires a clear-before-request diagnostic
  window, one of three explicit freshness alternatives, a distinct later
  terminal observation, post-reload identity resolution, and complete
  diagnostic paging.
- Project memory and the incident log now carry that smallest durable rule and
  the current four-mapped/five-unbound partition.

No Profile, Skill, Workflow, semantic Capability, package, or additional
binding was created.

## Regression protection added

- `.axit/milestones/M3-unity-execution-coverage/phase4-acquisition-regression.md`
  preserves the success/error/non-acquired decision boundaries and cleanup
  proof.
- `tests/QuickGun-MVP/AudioSourcePoolTests.cs` preserves the duplicate-release
  ownership regression alongside valid reuse and distinct lease cases.
- The active binding and Runtime Binding checklist preserve explicit
  `fresh_cycle_correlated` and `terminal_state_observed` flags. Verifier
  scripts must map evidence to both flags and must not reuse the freshness
  snapshot as terminal proof.
- `.axit/milestones/M3-unity-execution-coverage/phase8-gap-analysis.md`
  prevents compile-internal project/Console operations from silently growing
  into separate mappings.

## Known residual risks

- The active binding is a reviewed operation definition, not proof that the
  user-owned Unity MCP transport will be connected on a future run.
- Unity 6 reload can transiently interrupt reads or reset acquisition-local
  timing fields; future acquisitions must perform the same bounded correlation
  and identity re-resolution.
- The compiler proof covers the exact observed QuickGun script-compilation
  window, not player builds, all target platforms, package upgrades, broad
  gameplay correctness, or performance.
- The non-acquired Phase 4 cases are declarative decision fixtures rather than
  deliberately manufactured live disconnect/denial/crash events.
- `unity.scene.inspect` remains `REPEATED_GAP_FOR_HUMAN_REVIEW`; all five
  remaining capabilities are unbound because none was `REQUIRED_NOW`.
- Current M3 source/test/documentation changes remain
  `local-or-separately-tracked`; this milestone did not authorize or perform a
  commit, push, or pull request.
- The repaired closure candidate still requires final independent closure
  re-verification before `MILESTONE_DONE` can be issued.

## Promotion recommendation

**PROMOTE**, subject to final independent closure re-verification and then
explicit human review. Do not begin M4 automatically.

Subject to the final independent closure re-verifier, the candidate terminal
schema is:

```text
Status: MILESTONE_DONE
Milestone: M3 Unity Execution Coverage
Baseline compile: PASS
Active Unity bindings: unity.prefab.inspect, unity.serialized-fields.inspect, unity.playmode.verify, unity.compile
REAL scenario: M3-REAL-01 + PASS
Repair loops: 1 closure repair (0 product/baseline repairs)
Sub-agent replacements: 0
Additional bindings promoted: none
Context/auditability: PASS
Known residual risks: runtime connectivity remains resolve-at-runtime; Unity 6 reload requires reacquisition discipline; current M3 source/test/docs are local-or-separately-tracked; five capabilities remain unbound
Promotion recommendation: PROMOTE
```
