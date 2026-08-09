# Axit Workspace and System Spec v1

## Purpose

Axit treats the repository root as the **product workspace** when multiple interacting components live in one repository and Codex is normally opened from that root.

The workspace may contain a game client, backend, CMS, workers, tools, or other services under `src/`. These are **Systems**, not separate Axit projects by default.

The model is:

```text
Axit Core
  -> Workspace
      -> Systems
          -> Source + executable contracts + tests
```

This layout lets Codex trace behavior across real provider/consumer boundaries instead of reasoning from one isolated source tree.

## Canonical layout

```text
AGENTS.md

.agents/
└── skills/
    └── ... active Codex discovery entries

.axit/
├── workspace.yaml
├── core/
├── systems/
│   └── <system-id>/
│       ├── system.yaml
│       ├── rules.md
│       ├── architecture.yaml
│       └── knowledge/
├── registry/
│   ├── architecture.yaml
│   └── integrations.yaml
├── state/
│   └── active.md
├── specs/
├── templates/
└── checklists/

src/
├── GameClient/
├── Backend/
├── CMS/
└── Services/

tests/
├── contracts/
├── integration/
└── e2e/
```

Only directories that are actually needed should be materialized. Empty system extension catalogs are preferred over speculative agents/skills.

## Root-first Codex routing

`AGENTS.md` is the lightweight root router.

When a task names or touches a source path, Codex should:

1. read `.axit/workspace.yaml`;
2. map the affected source path to one or more registered Systems;
3. read only the relevant system manifests/rules/architecture;
4. read root integration/architecture registries when the task crosses system boundaries;
5. load Core Profiles/Skills/Workflows only when their responsibility/procedure is required.

Do not rely on changing the Codex working directory to activate a nested project context.

Do not recursively preload `.axit/`.

## Workspace manifest

Canonical file:

```text
.axit/workspace.yaml
```

The workspace manifest is routing context, not a product requirements document or architecture dump.

It should identify:

- workspace id/name/summary;
- source root;
- shared Axit Core;
- workspace registries and active state;
- registered Systems and their source/context paths;
- optional workspace-level extensions;
- repository-level contract/integration/e2e validation roots.

A System should not be registered until its source root or intended boundary is sufficiently known.

## System manifest

Canonical file:

```text
.axit/systems/<system-id>/system.yaml
```

A System is one coherent runtime/application/tool boundary inside the product workspace, for example:

- Unity game client;
- backend API;
- CMS/admin application;
- matchmaking service;
- worker/processor;
- shared protocol/package when it owns executable behavior.

The System manifest should identify:

- stable system id/name/kind;
- source root;
- runtime/framework identity when useful;
- local rules, architecture, and knowledge paths;
- interfaces it provides/consumes when known;
- validation routes;
- reviewed system-specific extensions;
- facts still intentionally unconfirmed.

Do not put full API docs or large framework references in `system.yaml`.

## Workspace vs System ownership

Use the narrowest correct owner.

System-local examples:

- Unity damage processing order;
- a backend module dependency rule;
- CMS form conventions;
- a service-specific retry policy.

Workspace/cross-system examples:

- backend is authoritative owner of inventory state;
- Unity and CMS consume the same PlayerProfile API;
- CMS writes inventory only through an admin API;
- a shared protocol version must remain compatible across providers and consumers.

Do not promote a local decision into workspace architecture merely because it is important inside one System.

## Workspace architecture registry

Canonical file:

```text
.axit/registry/architecture.yaml
```

Record only shared/high-risk architecture truth such as:

- authoritative state ownership across Systems;
- cross-system dependency direction;
- public boundary decisions;
- persistence/network topology shared by multiple Systems;
- workspace performance/security constraints;
- forbidden cross-system patterns.

System-local architecture belongs under:

```text
.axit/systems/<system-id>/architecture.yaml
```

## Integration registry

Canonical file:

```text
.axit/registry/integrations.yaml
```

The integration registry maps real provider/consumer relationships.

A useful integration entry may contain:

```yaml
id: player-profile
provider: backend
consumers:
  - unity-client
  - cms
contract:
  type: openapi
  source: src/Backend/openapi.json
tests:
  - tests/contracts/player-profile
```

The registry points to executable truth; it is not another schema source.

## Contract source-of-truth rule

Never duplicate an executable cross-system contract into `.axit` when a real source already exists.

Examples of executable truth:

- OpenAPI documents;
- protobuf/schema files;
- generated client contracts;
- shared protocol code;
- database migrations/schema;
- serializer fixtures or compatibility snapshots when they are intentionally canonical.

`.axit` records **where the contract is, who owns it, who consumes it, and how it is verified**.

This prevents stale AI documentation from becoming a second incompatible API definition.

## Cross-system reasoning

For a failure such as a Unity deserialize error, the expected reasoning path is:

```text
consumer failure
  -> consuming code/model
  -> integration registry
  -> executable provider contract
  -> provider implementation
  -> other consumers when relevant
  -> contract/integration tests
```

Do not assume the consumer or provider is wrong before comparing both sides against the accepted contract.

## Cross-system validation

Repository-level tests should live outside one System when they validate a boundary shared by multiple Systems.

Typical roots:

```text
tests/contracts/
tests/integration/
tests/e2e/
```

Examples:

- backend response conforms to OpenAPI and Unity deserializes it;
- CMS and Unity consume compatible enum values;
- an API migration keeps old clients compatible when required;
- backend + worker + database produce the expected observable workflow.

`verify-change` still decides whether a particular check is REQUIRED or SUPPORTING for the accepted criterion.

## Core relationship

The reviewed Axit Core remains workspace-agnostic.

Systems may add local Rules/Knowledge and, only when demonstrated, system-specific Skills/Workflows/Profiles.

Prefer this order:

```text
Rule / Registry fact
  -> Knowledge
  -> System Skill
  -> System Workflow
  -> System Profile
```

Create a new Profile only for a durable responsibility gap, not because a System corresponds to a traditional job title.

## Extension locations

When a real gap is demonstrated, use:

```text
.axit/systems/<system-id>/profiles/<id>/PROFILE.md
.axit/systems/<system-id>/skills/<id>/SKILL.md
.axit/systems/<system-id>/workflows/<id>/WORKFLOW.md
.axit/systems/<system-id>/knowledge/
```

Workspace-level extensions may exist when the procedure/responsibility truly spans Systems:

```text
.axit/workspace/profiles/
.axit/workspace/skills/
.axit/workspace/workflows/
.axit/workspace/knowledge/
```

Do not create these directories until a real need appears.

Only active Skills should be projected into `.agents/skills/` for Codex discovery.

## Active state

Canonical file:

```text
.axit/state/active.md
```

Keep workspace-level resumable state compact:

- current bounded task;
- affected Systems;
- accepted decisions;
- current implementation/verification state;
- blockers and next action.

System-specific durable facts belong in system architecture/rules/knowledge, not in the task checkpoint.

## Precedence

For a bounded task, interpret sources in this order:

```text
user request / accepted scope
  -> executable source/contracts and accepted workspace/system registries
  -> applicable Rules and active specialization
  -> Axit Core responsibility/procedure
  -> local implementation choices
```

If executable source truth and a registry disagree, surface the discrepancy instead of silently trusting stale metadata.

## Quality test

Before adding workspace/system metadata, ask:

1. Does this help route or verify real work?
2. Is it owned at the correct workspace/system scope?
3. Is an executable source already the better source of truth?
4. Can the need be represented by a small Rule/Registry/Knowledge entry instead of a Skill/Profile?
5. Does it support root-first reasoning across interacting Systems?
6. Does it avoid duplicating source contracts?
7. Will Codex be able to load it on demand instead of preloading the workspace?

If not, keep the structure simpler.
