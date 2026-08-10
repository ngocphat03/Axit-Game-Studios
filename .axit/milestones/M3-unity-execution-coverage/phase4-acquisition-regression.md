# M3 Phase 4 — Compile Acquisition-State Regression

Status: ready-for-independent-revalidation

Historical acquisition: 2026-08-10T12:58:51Z (superseded)

Closure-repair reacquisition: 2026-08-10T14:59:37Z

## Bounded scope

This audit exercises the active `unity.compile` mapping in
`.axit/bindings/unity-client/coplaydev-unity-mcp.yaml`. It does not edit the
binding, Runtime Binding checklist, Unity System sidecar, milestone
state/report/retrospective, production source, packages, project settings,
scenes, prefabs, or tests.

The only source-tree fixture was temporary and reversible:

```text
Assets/QuickGunCore/Scripts/AxitM3Phase4IntentionalCompileError.cs
Assets/QuickGunCore/Scripts/AxitM3Phase4IntentionalCompileError.cs.meta
```

The `.cs` contents were exactly:

```csharp
#if UNITY_EDITOR
#error AXIT_M3_PHASE4_INTENTIONAL_COMPILE_ERROR
#endif
```

Both fixture paths and the marker were absent before creation. The `.cs`
was created and deleted with `apply_patch`; the Unity-generated `.meta`
was inspected and deleted with `apply_patch`. No collision was overwritten.
Git status was not used as a readiness gate because
`.axit/workspace.yaml -> safety.check_git_status` is `false`.

## Closure repair history

Independent closure revalidation found that the historical error and clean
transcripts each used one post-request editor-state payload simultaneously for
fresh-cycle correlation and terminal readiness. That did not satisfy the
repaired active mapping's:

```text
terminal_state_observed.require_separate_observation_after:
  fresh_cycle_correlated
```

The older live observations are therefore historical only and are not used as
current Phase 4 evidence. An even earlier historical fixture attempt had also
been excluded because the external script had not yet obtained its generated
`.meta` and no complete unique marker diagnostic set was acquired.

Closure repair loop 1 used exactly one new fixture window. The new error and
clean acquisitions below each retain an exact pre-request snapshot, use a
first post-request state observation only to establish
`fresh_cycle_correlated=true`, and use a distinct later state read to
establish `terminal_state_observed=true`.

## Provenance and runtime identity

| Evidence | Provenance | Current observation |
|---|---|---|
| QuickGun System checkout | `system-canonical-pushed` | System `unity-client`; repository `ngocphat03/QuickGun-MVP`; ref `release`; commit `c35143a6ea72dd17e591e67b1e965e10a0b15a27`. |
| Repaired active binding/checklist inputs | `local-or-separately-tracked` | Current Workspace filesystem was the execution source; pushed state was not inferred. |
| This audit | `local-or-separately-tracked` | Durable local milestone artifact prepared for independent revalidation. |
| Unity identity, state observations, marker comparisons, and diagnostics | `ephemeral-runtime` | Acquisition-scoped evidence only; not canonical connection state. |

The exact live target resolved before and after both accepted acquisitions:

```text
connected editor: exactly one QuickGun-MVP instance; its full ephemeral
                  identifier was compared live and intentionally not persisted
project name: QuickGun-MVP
project root: /Volumes/FatDisk 1/DataProfiles/DataAXit/UnityProject/Axit-Game-Studios/src/QuickGun-MVP
Unity version: 6000.4.8f1
project build target: Android
editor platform: OSXEditor
```

The post-request instance identifier was re-resolved rather than assumed
stable across reload. No instance hash, session id, connection endpoint,
credential, token, or other secret is persisted here.

## Reacquired live error branch

### Fixture visibility and exact precondition

A narrowly scoped
`refresh_unity(mode=force, scope=assets, compile=none,
wait_for_ready=false)` made Unity import the externally created script. The
generated `.meta` was confirmed present before the counted request, and the
editor returned to a stable terminal state.

Exact instance, project, and editor state were then re-read. The pre-request
project/editor identity matched the target above and the editor was idle in
Edit Mode.

All numeric observation, sequence, and compiler-marker values in the
following block are labeled **`ephemeral-runtime`** and are not canonical
connection state:

```text
pre-request snapshot P:
  observed_at_unix_ms: 1786373853808
  sequence: 6
  last_compile_started_unix_ms: 1786373838858
  last_compile_finished_unix_ms: 1786373841815
  last_domain_reload_after_unix_ms: 1786373171969
```

### Fresh-cycle observation A

`read_console(action=clear)` returned `success=true`. The exact
`refresh_unity(mode=force, scope=scripts, compile=request,
wait_for_ready=false)` request returned outer `isError=false`, result
`success=true`, and `compile_requested=true`. Request acceptance was not
treated as compilation completion.

The first successful post-request state read was observation A. These values
are **`ephemeral-runtime`**:

```text
observation A:
  observed_at_unix_ms: 1786373865908
  sequence: 8
  last_compile_started_unix_ms: 1786373863376
  last_compile_finished_unix_ms: 1786373863710

comparison with P:
  compile-start marker advanced: 1786373863376 > 1786373838858
  compile-finish marker advanced: 1786373863710 > 1786373841815
```

Observation A establishes only:

```text
fresh_cycle_correlated: true
alternative: post-request-compile-marker-advanced
```

Although observation A already contained terminal booleans, it is not reused
as terminal proof.

### Separate terminal observation B

A distinct later editor-state read produced observation B. These values are
**`ephemeral-runtime`**:

```text
observation B:
  observed_at_unix_ms: 1786373870408
  sequence: 8
  later than observation A: true
  is_compiling: false
  is_domain_reload_pending: false
  assets.is_updating: false
  ready_for_tools: true
```

Observation B separately establishes:

```text
terminal_state_observed: true
require_separate_observation_after fresh_cycle_correlated: satisfied
```

The exact instance/project identity was then re-resolved and still matched the
target. Detailed error/warning diagnostics were paged to completion:

```text
cursor: 0
pageSize: 500
nextCursor: null
truncated: false
total: 1
item type: Error
item file: Assets/QuickGunCore/Scripts/AxitM3Phase4IntentionalCompileError.cs
item line: 2
item message: error CS1029: #error: 'AXIT_M3_PHASE4_INTENTIONAL_COMPILE_ERROR'
```

The complete page contained exactly one diagnostic, and its exact path and
marker were unique. Classification:
**`acquired + compilation errors`**, not `transport_error`.

## Mandatory cleanup and reacquired live clean branch

### Cleanup

Immediately after error diagnostics:

- both exact fixture files were inspected;
- the `.cs` and generated `.meta` were deleted with `apply_patch`;
- both paths were confirmed absent;
- `AXIT_M3_PHASE4_INTENTIONAL_COMPILE_ERROR` was confirmed absent everywhere
  under `Assets/`;
- a force/assets/no-compile refresh made Unity observe deletion;
- Unity completed the resulting import/compile/reload and returned idle in
  Edit Mode.

The cleanup terminal reload marker
`1786373906227` is **`ephemeral-runtime`** evidence only.

### Exact clean precondition

Exact instance, project, and editor state were re-read after cleanup. The
identity matched the same target. Numeric values below are
**`ephemeral-runtime`**:

```text
clean pre-request snapshot P:
  observed_at_unix_ms: 1786373912509
  sequence: 3
  last_compile_started_unix_ms: null
  last_compile_finished_unix_ms: null
  last_domain_reload_after_unix_ms: 1786373906227
```

### Fresh-cycle observation A

Console clear succeeded, and the exact force/scripts compile request returned
outer `isError=false`, result `success=true`, and
`compile_requested=true`.

The first successful post-request state read was observation A. Numeric
values are **`ephemeral-runtime`**:

```text
observation A:
  observed_at_unix_ms: 1786373931113
  sequence: 3
  last_domain_reload_after_unix_ms: 1786373928991

comparison with P:
  domain-reload marker advanced: 1786373928991 > 1786373906227
```

Observation A establishes only:

```text
fresh_cycle_correlated: true
alternative: post-request-domain-reload-marker-advanced
```

Its terminal booleans are not reused as terminal proof.

### Separate terminal observation B

A distinct later editor-state read produced observation B. Numeric values are
**`ephemeral-runtime`**:

```text
observation B:
  observed_at_unix_ms: 1786373934909
  sequence: 3
  later than observation A: true
  is_compiling: false
  is_domain_reload_pending: false
  assets.is_updating: false
  ready_for_tools: true
  play mode: false
  play-mode transition: false
```

Observation B separately establishes:

```text
terminal_state_observed: true
require_separate_observation_after fresh_cycle_correlated: satisfied
```

The exact post-reload instance/project identity was re-resolved and matched
the target. Detailed diagnostics paging completed:

```text
cursor: 0
pageSize: 500
nextCursor: null
truncated: false
total: 0
items: []
```

Classification: **`acquired + compilation succeeds`**. The final editor
state is idle Edit Mode, both fixture paths are absent, the marker is absent
under `Assets/`, and the complete fresh diagnostic window contains zero
project compiler errors or warnings.

## Acquisition decision table

| Case | Fixture/evidence | Expected acquisition classification | Product-verdict boundary |
|---|---|---|---|
| Compilation succeeds | Reacquired live clean branch with separate correlation/terminal observations and fully paged diagnostics `total=0` | `acquired + compilation succeeds` | Evidence only; not a direct verdict. |
| Compilation errors exist | Reacquired temporary `#error` branch with separate correlation/terminal observations and unique diagnostic path/marker | `acquired + compilation errors` | May support a compile criterion failure; acquisition itself is not the verdict. |
| Exact target precondition cannot resolve | Deterministic declarative decision fixture against `execution.precondition` and `outcome_boundary.unavailable` | `unavailable` | Acquisition state only; not product failure. |
| Runtime/Harness refuses before execution | Deterministic declarative decision fixture against `runtime_boundary.authorization` and `outcome_boundary.denied` | `denied` | Acquisition state only; not product failure. |
| Transport prevents terminal correlation or complete diagnostic paging | Deterministic declarative decision fixture against `outcome_boundary.transport_error` | `transport_error` | Acquisition state only; not product failure. |

The three non-acquired rows remain declarative decision fixtures. The active
binding has no executable normalizer or dedicated test seam through which to
inject unavailable identity, Harness denial, or a terminal transport failure.
No editor disconnect, permission change, crash, or fabricated operation was
manufactured for this regression.

## Focused structural assertion

A Ruby/YAML assertion loaded the repaired active binding and checked:

- binding status is `active`;
- exactly one active `unity.compile` mapping exists;
- the exact force/scripts/request/no-wait arguments remain unchanged;
- the three accepted `fresh_cycle_correlated` alternatives are present;
- `terminal_state_observed.require_separate_observation_after` equals
  `fresh_cycle_correlated`;
- all four terminal booleans are required;
- acquired evidence requires both the fresh correlation and separate terminal
  observations;
- `acquired`, `unavailable`, `denied`, and `transport_error` remain
  distinct;
- `unity.compile` is absent from the remaining unbound list.

Result:

```text
exit: 0
STRUCTURAL_ASSERTION_OK capability=unity.compile active=true separate_terminal=true outcomes=acquired,unavailable,denied,transport_error
```

Ruby emitted only the existing mounted-volume PATH-permission warning; no
YAML assertion failed.

## Final consistency requirements

The post-write consistency check must confirm:

- fixture `.cs` absent;
- fixture `.meta` absent;
- marker absent under `Assets/`;
- this audit contains distinct observation A/B evidence for both live
  branches;
- historical evidence is explicitly superseded;
- no concrete instance hash, session id, or connection endpoint is persisted.

Result:

```text
fixture .cs: absent
fixture .meta: absent
marker under Assets/: absent
AUDIT_CONSISTENCY_OK error_A_B=distinct clean_A_B=distinct historical=superseded concrete_runtime_connection_values=absent
final editor: idle Edit Mode, terminal-ready
final detailed diagnostics: total=0, nextCursor=null
```

## Implementation handoff boundary

The reacquired live success/error branches, mandatory cleanup proof,
declarative non-acquired decision fixtures, and structural check are prepared
for independent `verify-change` revalidation. This implementation artifact
does not issue a final milestone verdict.
