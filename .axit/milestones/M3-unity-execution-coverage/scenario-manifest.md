# M3 Phase 5 REAL Scenario Manifest

Status: **FROZEN_APPROVED**

This contract is frozen and independently approved for implementation. No product source, test, binding, sidecar, package, scene, prefab, or Unity runtime state was changed while authoring it.

## Candidate selection record

| Candidate family | Evidence-backed assessment | Disposition |
|---|---|---|
| `AudioSourcePool` duplicate `Release` | The human-promoted M2 S1 contract accepts `_pool` as available ownership, `_active` as leased ownership, distinct consecutive `Get()` results without an intervening valid `Release()`, and reuse after a valid one-time `Release()`. Current `Release` discards `_active.Remove(source)` and unconditionally enqueues the source, so releasing the same lease twice creates two available entries for one instance and lets two later unreleased `Get()` calls alias. This is a bounded current behavioral defect with accepted intent. | **SELECTED** as the smallest unequivocally REAL task. |
| Bot reaction timing | `src/QuickGun-MVP/.github/README.md` says both autonomous fire at a random 1–3 seconds and a 0.3–0.6 second delay after the player peeks. Current autonomous timing is 0.3–0.6 seconds, while the interrupt path waits 0.15 seconds before starting a `FireAsync` path that itself delays before the shot. The accepted trigger and clock boundary—interrupt receipt, peek start, or projectile fire—are not unequivocal. | **REJECTED: PRODUCT_INTENT**. Do not choose a number or timing boundary creatively. |
| Timeout/knockout result resolver extraction | M2 S3 already extracted `CombatResultResolver`, proved the greater/equal/less mapping, and received human promotion as a behavior-preserving refactor. Current source and focused tests retain that result. No current behavioral defect is established. | **REJECTED** because another extraction would be testability work, not the required REAL behavioral fix. |

## Frozen scenario contract

- `id`: `M3-REAL-01`
- `type`: `REAL`
- `category`: bug fix — duplicate `AudioSourcePool.Release` creates a double lease
- `goal`: Preserve exclusive audio-source leases by ensuring that releasing the same currently leased source twice before it is leased again cannot make that one instance available twice, while preserving ordinary distinct leasing and valid one-time release/reuse behavior.

### Exact acceptance sources

1. `.axit/milestones/M2-autonomous-bounded-development/scenario-manifest.md`, human-promoted S1 contract and terminal evidence:
   - `_pool` versus `_active` is the accepted available-versus-leased ownership model;
   - two consecutive `Get()` calls without an intervening `Release()` must return distinct instances;
   - a validly released instance becomes reusable;
   - the public API and architecture remain unchanged.
2. `.axit/milestones/M2-autonomous-bounded-development/promotion-review.md`, `QuickGun System source audit`, which promotes S1 as a real ownership/double-lease defect fix and confirms preservation of the public API.
3. Current implementation in `src/QuickGun-MVP/Assets/QuickGunCore/Scripts/Services/AudioSourcePool.cs`, used as defect evidence and as the concrete API/ownership surface. It does not independently authorize a new invalid-release policy.

### Accepted behavior and criteria

1. **Duplicate-release regression:** starting with an empty available pool, obtain one source, call `Release(source)` twice without another `Get()`, and then call `Get()` twice without an intervening `Release()`. The first later `Get()` may reuse the released source, but the two later results must be distinct references. One source must not back two simultaneous outstanding leases.
2. **Valid one-time release/reuse is preserved:** for a source obtained from this pool, one valid `Release()` makes it available once; a later `Get()` can reuse that source. Existing normal cleanup/activation behavior must not regress.
3. **Existing distinct-lease behavior is preserved:** two consecutive `Get()` calls from an empty pool, with no intervening `Release()`, return distinct references.
4. **Ownership transition remains exclusive:** an active lease may transition to available ownership once. A repeated release of that same lease before reacquisition must not add another available entry for the same instance.
5. **Public surface is unchanged:** keep the existing class, constructor, default initial size, `Get()`, `Release(AudioSource)`, and `ReleaseAll()` signatures and visibility. Do not change public ownership or introduce a new exception/logging/return-value contract.
6. **Intent boundary:** acceptance is limited to the exclusive-lease invariant above. It does not define new generalized semantics for foreign sources, cross-pool sources, destroyed Unity objects, concurrency, or stale references after a source has been leased again. Such behavior may only be constrained as needed to avoid violating the accepted invariant; it is not authorization for a broader invalid-release policy.

### Current defect evidence

The current `Release(AudioSource source)` implementation:

1. returns only for `null`;
2. performs normal source cleanup;
3. calls `_active.Remove(source)` but ignores whether removal succeeded; and
4. always calls `_pool.Enqueue(source)`.

For `source = pool.Get(); pool.Release(source); pool.Release(source);`, the first call removes the active lease and enqueues it once. The second call cannot remove it from `_active`, yet enqueues the same reference again. The next two `Get()` calls dequeue that same reference twice and add it to `_active` twice, directly violating the promoted distinct-outstanding-lease invariant. The existing `tests/QuickGun-MVP/AudioSourcePoolTests.cs` covers empty-pool distinctness and valid one-time reuse but does not cover duplicate release.

### Allowed files and bounded scope

Only these two files may be edited for implementation or repair:

- `src/QuickGun-MVP/Assets/QuickGunCore/Scripts/Services/AudioSourcePool.cs`
- `tests/QuickGun-MVP/AudioSourcePoolTests.cs`

The existing `tests/QuickGun-MVP/UnityEngineAudioStubs.cs` may be read and used unchanged by the focused standalone harness; it is not an editable scenario file. The intended production change is the smallest ownership/enqueue guard needed to satisfy the frozen criteria. The intended test change is one focused deterministic duplicate-release regression while retaining the two promoted M2 cases.

### Forbidden scope

- `SoundService` or any other production source;
- any other test or stub file;
- prefabs, scenes, assets, `.meta` files, packages, manifests, project settings, or serialized data;
- Runtime Bindings, Capability definitions/sidecars, Axit Core, System architecture/rules, or public API documentation;
- public signature, visibility, constructor-default, or state-ownership changes;
- generalized pool redesign, generic-pool extraction, capacity/prewarm changes, concurrency/thread-safety work, performance tuning, or unrelated cleanup;
- a new exception, log, return value, callback, or generalized invalid-release/cross-pool policy;
- rewriting this acceptance contract after observing implementation results;
- publishing, pushing, or creating a pull request.

### Required evidence

1. **Independent contract approval before implementation:** a contract reviewer who did not author this manifest must return `APPROVE_FOR_IMPLEMENTATION` against this frozen text. Until then, status remains `FROZEN_PENDING_REVIEW` and no implementation is authorized.
2. **Pre-fix defect reproduction:** after approval and before editing production C#, the implementation lane must add the focused duplicate-release regression to the allowed test file and record that it fails against the current production implementation for the expected reference-alias reason. A compile/harness failure unrelated to the assertion is not valid defect reproduction.
3. **Focused final deterministic tests:** compile and execute the actual production `AudioSourcePool.cs` with the existing unchanged stub harness and the allowed test file. Required cases are:
   - two consecutive empty-pool leases are distinct;
   - one validly released source is reused without aliasing another unreleased lease;
   - duplicate release does not cause the next two outstanding leases to alias.
   All cases must pass in one final run.
4. **Production ownership inspection:** inspect the final source and prove that one active lease can contribute at most one available entry, normal one-time release cleanup/reuse remains intact, and no public signature or forbidden policy was introduced.
5. **Bounded-scope inspection:** prove scenario edits are limited to the two allowed files and contain no unrelated refactor. Inspect whitespace/static-check results for the scoped edits without using Git status as a readiness gate.
6. **Fresh full-project Unity compile:** after the final production C# edit, independently acquire a full `unity.compile` result for the exact current QuickGun project through the reviewed M3 mapping. Required terminal evidence is `acquired + compilation succeeds`, complete diagnostic paging, no compiler-error diagnostics, and matching pre/post project-editor identity. Request acceptance, asset refresh alone, standalone C# compilation, or stale Phase 3/4 evidence does not satisfy this criterion.
7. **Independent final verification:** the verifier must read the frozen contract and current files, rerun the focused tests, reacquire the full Unity compile, inspect scope/ownership, and return the final scenario verdict using `verify-change` semantics. Worker-reported results are handoff evidence only.

### Supporting evidence

- Human-promoted M2 S1 contract, terminal `PASS`, and promotion audit.
- The focused standalone C# compiler/run transcript, including warnings-as-errors where supported by the existing harness.
- A scoped source diff and static/whitespace check for the two allowed files.
- Current System repository/ref/commit resolution and exact Unity project/editor identity accompanying the required compile evidence.

### Full `unity.compile` requirement

- Full-project `unity.compile` is **REQUIRED** because this scenario changes production C#.
- Acquire it only after the implementation lane's final production C# change.
- After **every repair loop**, reacquire the focused deterministic tests, affected static/scope evidence, and a fresh full `unity.compile` before a verifier may issue a verdict. No pre-repair compile result may be reused.
- `acquired + compilation errors` is acquired evidence of a failed compile criterion, not transport unavailability. `unavailable`, `denied`, and `transport_error` remain acquisition states rather than product failures.
- Do not bind another Capability or substitute standalone compilation for this requirement.

### Runtime cleanup and restoration

- No prefab, scene, serialized-field, or Play Mode acceptance evidence is required.
- Do not create a product fixture or mutate runtime gameplay state.
- Remove transient standalone test binaries/output and dispose of transient test objects after evidence acquisition.
- After every Unity compile attempt, leave the exact intended QuickGun editor terminal-ready and in Edit Mode. If acquisition fails, still perform safe cleanup/identity checks before handoff.
- No persistent product restoration is expected beyond preserving all files outside the two-file allowed scope.

### Evidence provenance expectation

- Re-resolve the current `unity-client` System repository/ref/commit at evidence time; do not assume the commit recorded by M2 or Phase 4 remains current.
- The unchanged canonical System baseline may be classified `system-canonical-pushed` only when repository/ref/commit evidence proves it. The locally modified production file is `local-or-separately-tracked` unless independently committed and pushed; this milestone does not authorize pushing.
- The root focused test, this manifest, local diffs, and standalone test transcripts are `local-or-separately-tracked` unless stronger provenance is actually proven.
- Live Unity identity, compile state/diagnostics, and standalone process observations are `ephemeral-runtime`.
- Do not infer pushed provenance from filesystem presence or a branch name.

### Hard-blocker triggers

- `PRODUCT_INTENT` or `ARCHITECTURE_DECISION`: satisfying the distinct-lease invariant materially requires choosing a new public invalid-release rule, changing public ownership, or redesigning the pool.
- `UNITY_MCP_NOT_READY`: the user-owned Unity MCP transport or reviewed `unity.compile` operation is unavailable/unreachable for required final evidence. Do not install, configure, start, or repair it.
- `DESTRUCTIVE_SCOPE`: the fix requires deletion, migration, broad refactoring, or edits outside the two allowed files.
- Continuing would materially overwrite unrelated current work in an allowed file and the bounded change cannot preserve it.
- A required focused behavior, scope criterion, or full Unity compile remains failed after at most two bounded repair loops: stop with `REPEATED_REQUIRED_FAILURE` or the appropriate independent `FAIL`; do not weaken acceptance.
- Any secret, production access, admin escalation, package change, new Capability/Binding, scene/prefab mutation, or public-contract change becomes necessary.

### Independence and repair-loop contract

- Contract author, independent contract reviewer, implementation worker, and independent final verifier are distinct responsibilities.
- The implementation worker may edit only the two allowed files, run implementation-side checks, and hand off evidence; it may not issue the final verdict.
- The final verifier must independently reacquire all REQUIRED evidence from current state and must not rely on stale worker evidence.
- On verifier `FAIL`, a bounded repair must be performed by an implementation/repair worker, followed by fresh independent verification.
- Maximum repair loops for this scenario: **2**. Acceptance, allowed scope, and evidence requirements remain frozen across repairs.

## Review gate

Current review result: **APPROVE_FOR_IMPLEMENTATION**

Independent review approved this exact frozen contract with no corrections. This manifest does not implement the product fix and does not issue a Phase 5, scenario, or milestone verdict.
