# M3 Phase 8 — Demonstrated-Gap Analysis

Status: recorded

## Bounded analysis scope

This cross-run audit classifies only the five Unity evidence needs named by
M3 Phase 8. It uses the demonstrated baseline compile, compile acquisition
regression, REAL scenario implementation/verification, and the previously
recorded runtime-target incidents. It does not discover or authorize another
transport operation, alter a semantic Capability, or issue a milestone
verdict.

## Classification

| Capability | Classification | Demonstrated basis |
|---|---|---|
| `unity.tests.run` | `ORDINARY_EVIDENCE_SUFFICIENT` | The REAL scenario's focused deterministic harness exercised the actual production source and all frozen pool-ownership regressions. No accepted criterion required a Unity Test Framework run, so ordinary project evidence was sufficient. |
| `unity.console.inspect` | `NO_NEED` | No independent console-inspection criterion occurred. Console clear/read operations were internal suboperations of the proven `unity.compile` acquisition and do not constitute another Capability mapping. |
| `unity.scene.inspect` | `REPEATED_GAP_FOR_HUMAN_REVIEW` | M1 and M2 both exposed the missing `GameEnvironment/` runtime-wrapper assumption for path-addressed evidence. Runtime Binding validation Case 8 now requires current full runtime identity resolution, and the M3 REAL scenario required no scene/runtime-path evidence. The recurrence remains visible for human review but was not required in this run. |
| `unity.component.inspect` | `NO_NEED` | No M3 criterion needed component-presence/configuration evidence beyond the evidence surfaces already available, and the REAL scenario was a bounded source/test/compile change. |
| `unity.project.inspect` | `NO_NEED` | Current editor/project identity reads were internal preconditions of `unity.compile`; System metadata and ordinary repository evidence covered provenance. No independent project-inspection criterion occurred. |

## REQUIRED_NOW gate

```text
REQUIRED_NOW: none
additional binding authorized: none
```

`unity.compile` is the only M3 mapping eligible for Phase 9 promotion. The
remaining five capabilities stay unbound. In particular, compile-internal
resource reads and Console diagnostics do not map `unity.project.inspect` or
`unity.console.inspect`.

## Evidence boundary

- The QuickGun canonical baseline is System `unity-client`, repository
  `ngocphat03/QuickGun-MVP`, ref `release`, commit
  `c35143a6ea72dd17e591e67b1e965e10a0b15a27`.
- Scenario source/test changes and current Workspace audit artifacts are
  `local-or-separately-tracked`; no pushed state is inferred.
- Live Unity compilation and diagnostic observations are
  `ephemeral-runtime`; no acquisition-local identity or connection state is
  persisted here.
