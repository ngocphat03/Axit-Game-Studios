# Capability Spec v1

## Purpose

Capabilities are provider-neutral names for actions or interaction primitives required from a runtime.

```text
Canonical Skill/Profile
    -> semantic capability
    -> provider/runtime adapter
    -> concrete tool/API
```

Canonical artifacts ask for `file.read`, not `Read`; `agent.delegate`, not `Task`; `user.approval`, not a provider-specific question widget.

## Naming

Capabilities use dot-separated lowercase IDs: `<domain>.<action>`.

## Initial v1 registry

### Files and code

| Capability | Meaning |
|---|---|
| `file.read` | Read project files. |
| `file.write` | Create or replace project files. |
| `code.edit` | Make targeted source/document edits. |
| `file.search` | Search workspace paths/content. |
| `repo.search` | Search repository-wide symbols/content using runtime facilities. |

### Git

| Capability | Meaning |
|---|---|
| `git.status` | Inspect working-tree state. |
| `git.diff` | Inspect changes. |
| `git.history` | Read commit history. |
| `git.commit` | Create a commit when policy permits. |
| `git.push` | Push changes when policy permits. |

Commit and push are distinct because policy/approval usually differs.

### Process, build, and test

| Capability | Meaning |
|---|---|
| `process.execute` | Execute an allowed local process/command. |
| `build.run` | Invoke build/compile. |
| `test.run` | Run automated tests or invoke a test runner adapter. |
| `test.inspect` | Read/interpret existing test results. |

### User interaction

| Capability | Meaning |
|---|---|
| `user.question` | Collect missing open-ended information. |
| `user.choice` | Present bounded alternatives and record a choice. |
| `user.approval` | Authorize a scoped mutation/operation. |
| `user.confirmation` | Record manual verification of an observable condition/evidence item. |

Approval is authority to mutate; confirmation is evidence. They must not be conflated.

### Agent collaboration

| Capability | Meaning |
|---|---|
| `agent.delegate` | Delegate bounded work to another profile/context. |
| `agent.consult` | Request independent analysis without transferring change ownership. |
| `agent.parallel` | Execute independent consultations/delegations concurrently when supported. |

A runtime without native subagents may emulate these using additional model calls/profile switches, or report them unsupported.

### Research

| Capability | Meaning |
|---|---|
| `web.research` | Retrieve current external information when project policy permits. |

Research is optional for canonical game-design workflows unless freshness/external evidence is materially required.

### Evidence and artifacts

| Capability | Meaning |
|---|---|
| `artifact.capture` | Produce/store an evidence artifact. |
| `artifact.inspect` | Inspect an existing artifact/evidence record. |
| `screenshot.capture` | Capture a visual screenshot when supported. |

### Unity

| Capability | Meaning |
|---|---|
| `unity.inspect` | Read Unity project/editor state. |
| `unity.modify` | Perform Unity-side modifications. |
| `unity.console.read` | Read Unity Console output. |
| `unity.test.run` | Run Unity EditMode/PlayMode tests. |
| `unity.artifact.capture` | Capture Unity-specific evidence such as scene/prefab state or screenshots. |

The Unity registry remains broad in v1; granularity should be added only when migrations prove policy/routing value.

## Required vs optional

```yaml
capabilities:
  required:
    - file.read
    - code.edit
  optional:
    - agent.delegate
```

If a required capability is unavailable, resolution fails unless an adapter declares a semantics-preserving fallback. Missing optional capabilities may change execution strategy but must not silently weaken correctness requirements.

## Capability resolution

Resolve before execution:

```text
Skill requirements
+ Profile requirements
+ Adapter/runtime support
+ Project/Harness policy
= effective capability set
```

Recommended result:

```yaml
resolution:
  file.read:
    status: available
    implementation: provider.read_tool
  user.approval:
    status: approval-gated
    implementation: runtime.approval
  unity.modify:
    status: unavailable
```

## Capability is not permission

Environment support does not imply authorization:

```text
Runtime supports git.push
Profile/Skill requests git.push
Project policy denies git.push
=> denied
```

Future Axit-Code must enforce this distinction through Harness policy.

## Adapter mapping

Conceptual Claude mapping:

```yaml
provider: claude-code
maps:
  file.read: Read
  file.write: Write
  code.edit: Edit
  process.execute: Bash
  agent.delegate: Task
  user.question: AskUserQuestion
  user.choice: AskUserQuestion
  user.approval: AskUserQuestion
  user.confirmation: AskUserQuestion
```

Concrete mappings live only in adapters/runtime configuration.

## Extending the registry

Add a capability only when at least one is true:

1. runtimes expose materially different implementations;
2. policy needs to allow/deny/approve it independently;
3. skills need it for compatibility/routing;
4. verification needs to know whether required evidence can be produced.

Do not mirror every provider tool name into the canonical registry.