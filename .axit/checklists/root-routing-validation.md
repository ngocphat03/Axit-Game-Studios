# Root Workspace Routing Validation

Status: passed-v1
Validated: 2026-08-09

This checklist validates the Axit root-first routing model. Antigravity starts from repository root, reads root `AGENTS.md`, and uses `.axit/workspace.yaml` to select only the System context relevant to the task.

## Validation protocol

The live validation used fresh root-level Antigravity requests without manually pointing the model at nested project metadata.

The goals were:

- source path -> correct System routing;
- context minimization for single-System work;
- explicit cross-System routing when needed;
- refusal to invent missing backend/CMS contracts;
- executable contract source-of-truth discipline.

## Case 1 — identify one System from a source path

Target:

```text
src/QuickGun-MVP
  -> .axit/workspace.yaml
  -> unity-client
  -> .axit/systems/unity-client/system.yaml
```

Live result: **PASS**.

Observed behavior:

- resolved `src/QuickGun-MVP` to System `unity-client` / `QuickGun-MVP`;
- resolved shared Core at `.axit/core/core.yaml`;
- resolved local Rules and Architecture correctly;
- reported `git diff --check`, `tests/QuickGun-MVP/`, and Unity compile/build routes;
- preserved exact Unity version, target platforms, and player-count range as unconfirmed.

Minor presentation issue:

- the answer labeled `.axit/workspace.yaml` as "System manifest" once; the routing itself was correct and the correct System-local files were resolved. This is terminology drift, not a routing failure.

No nested `src/QuickGun-MVP/.axit` dependency was required.

## Case 2 — bounded Unity-only change

Target behavior:

```text
AGENTS.md
  -> workspace
  -> unity-client
  -> relevant System Rules/Architecture
  -> shared bounded-change Workflow
      -> implement-change
      -> verify-change
```

Live result: **PASS**.

Observed behavior:

- correctly routed the task entirely to `unity-client`;
- loaded damage-specific System Architecture because the task touched registered damage contracts;
- selected `bounded-change`, `implement-change`, and `verify-change` for the requested end-to-end flow;
- correctly identified the implementation and quality-verifier responsibility lenses;
- explicitly did **not** load `.axit/registry/integrations.yaml`, workspace cross-System architecture, other Systems, unrelated knowledge/specs/templates, or repository-level contract/integration/e2e tests;
- did not create a Unity/combat-specific Profile, Skill, Workflow, or specialist agent.

This case confirms that root routing includes a useful "do not load" boundary, not only a list of available context.

## Case 3 — cross-System request with missing provider Systems

Scenario:

```text
Unity deserialize failure
+ CMS displays the same field differently
+ backend/CMS not yet registered
```

Live result: **PASS**.

Observed behavior:

- recognized the request as cross-System;
- read workspace routing and the integration registry;
- recognized that only `unity-client` is currently registered;
- identified the integration registry as empty;
- reported missing backend/CMS Systems, executable provider contract, provider/consumer mapping, consumer failure evidence, and boundary tests;
- did not invent backend response fields, CMS implementation, API ownership, or transport technology;
- concluded that root-cause determination is blocked until real provider/consumer evidence exists.

This is the desired anti-guessing behavior for future cross-System debugging.

## Case 4 — executable contract source discipline

Live result: **PASS**.

Observed behavior:

- kept API response contracts in the owning source tree as executable truth, for example OpenAPI/protobuf/schema/generated/shared protocol source;
- kept `.axit/registry/integrations.yaml` limited to provider, consumers, contract type/source path, and contract/integration test routes;
- explicitly rejected duplicating DTO/response schema bodies into `.axit`.

## Case 5 — live cross-System verification

Status: **DEFERRED** until at least two real Systems and one executable integration contract exist.

Target future evidence path:

```text
provider implementation / executable contract
        -> consumer compatibility
        -> contract tests
        -> integration tests
        -> optional e2e/runtime evidence
        -> verify-change verdict
```

Do not create backend/CMS placeholders solely to make this case executable.

## Root routing v1 conclusion

Root Workspace/System routing v1 is accepted as stable for the currently materialized workspace.

Validated properties:

- source paths resolve to the correct registered System;
- only relevant System context is loaded;
- shared Core is reused without nested copies;
- single-System tasks avoid unnecessary cross-System registries/tests;
- explicit cross-System tasks route to integration metadata;
- missing Systems/contracts produce explicit unknown/blocker output rather than invented facts;
- executable contracts remain outside `.axit`;
- System routing does not require new Profiles, Skills, or Workflows.

Freeze this routing contract unless later live use demonstrates a reproducible routing failure.
