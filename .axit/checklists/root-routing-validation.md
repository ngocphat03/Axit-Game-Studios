# Root Workspace Routing Validation

Status: ready-for-live-codex-test

This checklist validates the Axit root-first routing model. Codex is expected to start from the repository root, read the root `AGENTS.md`, and use `.axit/workspace.yaml` to decide which System context is relevant.

The goal is not to prove model intelligence. The goal is to prove that the routing contract keeps context scoped, resolves source ownership correctly, and refuses to invent cross-system facts that are not registered.

## Test protocol

For comparable runs:

1. Start Codex from the repository root.
2. Use a fresh session for each case when practical.
3. Do not manually point Codex at Axit files unless the test prompt explicitly does so.
4. Do not add hints after the first prompt; record routing mistakes instead.
5. Do not modify source during read-only routing cases.
6. Use the current branch state for all cases in one validation pass.

## Case 1 — identify one System from a source path

Prompt:

```text
Read only the Axit context needed to answer this question.

Which Axit System owns `src/QuickGun-MVP`, which Core does it use, and which local Rules, Architecture, and validation routes apply?
Do not modify anything.
```

Expected routing:

```text
root AGENTS.md
  -> .axit/workspace.yaml
  -> systems[unity-client]
  -> .axit/systems/unity-client/system.yaml
  -> system-local rules / architecture only as needed
```

Expected findings:

- System id: `unity-client`.
- Source root: `src/QuickGun-MVP`.
- Shared Core: `.axit/core/core.yaml`.
- System Rules: `.axit/systems/unity-client/rules.md`.
- System Architecture: `.axit/systems/unity-client/architecture.yaml`.
- Deterministic tests route through `tests/QuickGun-MVP/`.
- Exact Unity version, target platforms, and player-count range remain unconfirmed.

Failure signals:

- treats QuickGun as a separate Axit Project;
- looks for `src/QuickGun-MVP/.axit` or nested `AGENTS.md`;
- recursively reads unrelated `.axit` directories;
- invents Unity version, platforms, or player count.

## Case 2 — route a bounded Unity-only change

Prompt:

```text
Plan the Axit routing for an end-to-end change that only modifies the QuickGun damage calculation and its deterministic tests.
Do not implement it yet.
Tell me which Workspace/System/Core artifacts and validation paths you would load, and which ones you would intentionally not load.
```

Expected behavior:

- maps the task to `unity-client` through `.axit/workspace.yaml`;
- loads the Unity System Rules/Architecture because the task touches a registered damage contract;
- recognizes `bounded-change` as the Core Workflow only because the request says end-to-end change;
- keeps `implement-change` and `verify-change` as the participating Core Skills;
- does not load `.axit/registry/integrations.yaml` merely because it exists, because the task is currently single-system;
- does not create a Unity-specific Profile or Skill.

Failure signals:

- loads all Systems or all Axit knowledge by default;
- routes a single-system damage change through cross-system integration machinery;
- invents a specialist agent such as `unity-programmer` or `combat-agent`.

## Case 3 — cross-system request when other Systems are not registered

Prompt:

```text
The Unity client is failing to deserialize a response from our backend and the CMS displays the same field differently.
Use Axit routing to tell me what you can inspect right now and what is missing before you can determine the root cause.
Do not guess the backend or CMS contract and do not modify anything.
```

Expected behavior:

- reads `.axit/workspace.yaml` and recognizes only `unity-client` is currently registered;
- checks `.axit/registry/integrations.yaml` because the request is explicitly cross-system;
- reports that no backend/CMS Systems or executable integration contract are currently registered on this branch;
- may inspect the Unity consumer if present locally, but must not invent provider response shape, CMS behavior, API technology, or ownership;
- returns a clear missing-context/blocker statement and identifies the artifacts that should be registered once backend/CMS sources exist.

Failure signals:

- guesses an API response schema;
- assumes REST/OpenAPI without evidence;
- claims backend or CMS code was inspected when it is not registered/available;
- creates duplicate DTO/schema truth under `.axit`.

## Case 4 — contract-source discipline

Prompt:

```text
Suppose a future backend exposes an API consumed by both Unity and CMS.
Where should Axit store the API response contract, and what should `.axit/registry/integrations.yaml` store instead?
Do not create files.
```

Expected answer:

- executable/source contract remains in the owning source tree, for example OpenAPI/protobuf/schema/generated/shared protocol code;
- `.axit/registry/integrations.yaml` stores provider, consumers, contract type/path, and test routes;
- Axit does not duplicate DTO/response bodies as a second source of truth.

## Case 5 — verification scope across Systems

This case becomes executable only after at least two real Systems and one integration contract are registered.

Target behavior:

```text
provider implementation / executable contract
        -> consumer compatibility
        -> contract tests
        -> integration tests
        -> optional e2e/runtime evidence
        -> verify-change verdict
```

`verify-change` must classify evidence from the real boundary. It must not treat a matching Axit note as proof that the provider and consumer are compatible.

## Acceptance criteria for root routing v1

Root routing is acceptable when live Codex use demonstrates all currently executable cases below:

- source path resolves to the correct registered System;
- only relevant System context is loaded;
- shared Core is reused without copying it into `src/`;
- single-system tasks do not unnecessarily load cross-system registries;
- explicit cross-system tasks do load the integration registry;
- missing Systems/contracts produce an explicit blocker/unknown rather than invented facts;
- executable contracts remain outside `.axit` and Axit stores routing/relationship metadata only;
- no new Profile, Skill, or Workflow is created merely to route a System.

Do not register backend/CMS/service placeholders solely to make these tests pass. Add Systems only when their real source roots and boundaries exist.