# Capability Spec v1

## Purpose

Capabilities are provider-neutral names for actions or interaction primitives that a Skill/Profile may require from a runtime.

They solve a core portability problem:

```text
Canonical Skill -> semantic capability -> provider/runtime adapter -> concrete tool/API
```

A canonical artifact asks for `file.read`, not `Read`; `agent.delegate`, not `Task`; `user.approval`, not `AskUserQuestion`.

## Naming

Capabilities use dot-separated lowercase namespaces:

```text
<domain>.<action>
```

Examples:

- `file.read`
- `file.write`
- `code.edit`
- `repo.search`
- `git.diff`
- `git.commit`
- `process.execute`
- `test.run`
- `build.run`
- `user.approval`
- `agent.delegate`
- `artifact.capture`
- `unity.inspect`
- `unity.modify`
- `unity.console.read`

Capability IDs describe semantics, not implementation.

## Initial v1 registry

### Files and code

| Capability | Meaning |
|---|---|
| `file.read` | Read project files. |
| `file.write` | Create or replace a file. |
| `code.edit` | Make targeted source-code changes. |
| `file.search` | Search file names/content in a workspace. |
| `repo.search` | Search repository-wide symbols/content using runtime facilities. |

### Git and change inspection

| Capability | Meaning |
|---|---|
| `git.status` | Inspect working-tree state. |
| `git.diff` | Inspect proposed/current changes. |
| `git.history` | Read commit history. |
| `git.commit` | Create a commit when runtime policy permits. |
| `git.push` | Push changes when runtime policy permits. |

`git.commit` and `git.push` are intentionally distinct because approval/policy commonly differs.

### Process, build, and test

| Capability | Meaning |
|---|---|
| `process.execute` | Execute an allowed local process/command. |
| `build.run` | Invoke the project build/compile operation. |
| `test.run` | Run automated tests. |
| `test.inspect` | Read/interpret existing test results. |

### User interaction

| Capability | Meaning |
|---|---|
| `user.question` | Ask the user for missing non-security information. |
| `user.approval` | Obtain explicit approval for a scoped operation. |
| `user.choice` | Present bounded alternatives and obtain a decision. |

Approval is distinct from a generic question because runtimes may enforce it structurally.

### Agent collaboration

| Capability | Meaning |
|---|---|
| `agent.delegate` | Delegate a bounded task to another profile/agent context. |
| `agent.consult` | Request analysis from another profile without granting change ownership. |
| `agent.parallel` | Execute independent delegated tasks concurrently. |

A runtime that lacks subagents may emulate these capabilities using additional model calls or may report them unsupported.

### Evidence and artifacts

| Capability | Meaning |
|---|---|
| `artifact.capture` | Produce/store an evidence artifact. |
| `artifact.inspect` | Inspect an existing evidence artifact. |
| `screenshot.capture` | Capture a visual screenshot when supported. |

### Unity

| Capability | Meaning |
|---|---|
| `unity.inspect` | Read Unity project/editor state. |
| `unity.modify` | Perform Unity-side modifications. |
| `unity.console.read` | Read Unity Console output. |
| `unity.test.run` | Run Unity EditMode/PlayMode tests. |
| `unity.artifact.capture` | Capture Unity-specific evidence such as scene/prefab state or screenshots. |

The Unity registry is deliberately broad in v1. More granular capability names should be added only after real migrations show policy/routing value.

## Required vs optional

Skills and Profiles declare:

```yaml
capabilities:
  required:
    - file.read
    - code.edit
  optional:
    - agent.delegate
```

If a required capability is unavailable, the runtime MUST fail capability resolution before execution or choose a compatible fallback explicitly defined by the adapter.

Missing optional capabilities must not silently change correctness requirements. The runtime may choose a simpler execution strategy.

## Capability resolution

Before executing a Skill, the runtime resolves:

```text
Skill required capabilities
+ Profile required capabilities
+ Project/runtime policy
+ Adapter support
```

Recommended resolution result:

```yaml
resolution:
  file.read:
    status: available
    implementation: provider.read_tool
  code.edit:
    status: available
    implementation: provider.edit_tool
  user.approval:
    status: approval-gated
    implementation: runtime.approval
  unity.modify:
    status: unavailable
```

## Capability is not permission

A capability describes what an environment can do. It does not mean the active model is allowed to do it.

Example:

```text
Runtime supports git.push
Profile requests git.push
Project policy denies git.push
=> denied
```

Future Axit-Code must enforce this distinction through Harness policy.

## Adapter mapping

Provider adapters map semantic capabilities to concrete primitives.

Conceptual example:

```yaml
provider: claude-code
maps:
  file.read: Read
  file.write: Write
  code.edit: Edit
  process.execute: Bash
  agent.delegate: Task
  user.approval: AskUserQuestion
```

This mapping is adapter configuration, never canonical Skill/Profile content.

## Extending the registry

Add a new capability only when at least one of the following is true:

1. different runtimes expose materially different implementations;
2. runtime policy needs to allow/deny/approve it independently;
3. skills need it for routing or compatibility checks;
4. verification needs to know whether the environment can produce required evidence.

Do not create capabilities merely to mirror every provider tool name.