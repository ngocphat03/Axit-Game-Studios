# Provider Adapter Boundary

## Purpose

Provider adapters translate the canonical Axit Agent Spec into concrete runtime primitives available in Claude Code, Codex, Gemini, or another environment.

Adapters are **compatibility layers**, not places where product/workflow semantics live.

## Responsibilities

An adapter may:

- map semantic capabilities to provider/runtime tools;
- map model requirements to available provider models;
- render canonical profiles/skills into provider-specific files;
- translate user approval and delegation semantics;
- expose unsupported capabilities clearly;
- add provider-specific optimizations that do not change canonical behavior.

An adapter must not:

- redefine the meaning of a Skill;
- weaken required verification silently;
- grant capabilities denied by project/runtime policy;
- become the only location where critical workflow rules exist;
- modify canonical source during export.

## Adapter contract

Conceptual adapter configuration:

```yaml
adapter_version: axit.adapter/v1
provider: claude-code

capabilities:
  file.read:
    implementation: Read
  file.write:
    implementation: Write
  code.edit:
    implementation: Edit
  file.search:
    implementation: Glob+Grep
  process.execute:
    implementation: Bash
  agent.delegate:
    implementation: Task
  user.question:
    implementation: AskUserQuestion
  user.approval:
    implementation: AskUserQuestion

model_routing:
  - when:
      reasoning: high
    select: high-reasoning-model
  - when:
      coding: high
    select: coding-default-model
```

Concrete model names may appear inside an adapter/runtime configuration. They must not appear in canonical profiles unless documented as non-portable legacy metadata during migration.

## Export model

Long term, provider-specific trees should be generated from canonical `.agents/` sources:

```text
.agents/*
   |
   +--> adapter:claude --> .claude/*
   +--> adapter:codex  --> provider-specific output
   +--> adapter:gemini --> provider-specific output
```

Generated output should be reproducible and treated like build output where practical.

During migration, existing `.claude/` files remain hand-authored legacy/reference artifacts until a canonical replacement and exporter are proven.

## Capability fallback

Adapters may declare explicit fallbacks.

Example:

```yaml
agent.delegate:
  native: false
  fallback: model.sequential-call
```

Fallbacks must preserve semantics. If they cannot preserve required correctness or approval behavior, the capability is `unsupported` and execution must fail or choose another strategy.

## Model routing

Canonical requirement:

```yaml
model_requirements:
  reasoning: high
  coding: medium
  long_context: preferred
  tool_use: required
```

Adapter/runtime decision:

```text
requirements
+ user provider preference
+ available models
+ cost/latency policy
+ context size
=> concrete model
```

This makes profiles portable while allowing each environment to optimize model choice.

## Claude adapter notes

The existing repository already exposes useful Claude Code primitives:

- `Read`, `Glob`, `Grep`, `Write`, `Edit`, `Bash`;
- `Task` for subagent delegation;
- `AskUserQuestion` for interaction;
- permission rules;
- lifecycle hooks.

These should be mapped into capabilities rather than copied into canonical spec files.

Claude hooks may continue enforcing useful safety/validation while Game Studios runs on Claude Code, but canonical rules should describe the invariant independently.

Example:

```text
Canonical invariant:
  force-push requires explicit project policy and user approval

Claude implementation:
  permission deny + PreToolUse hook

Axit-Code implementation:
  Harness policy evaluation
```

## Codex / OpenAI adapter notes

The canonical contract should make no assumption about a specific OpenAI tool surface. An adapter should discover/declare what the active Codex/OpenAI environment supports and map only compatible capabilities.

Do not create canonical skills that depend on a provider feature merely because one runtime currently exposes it.

## Gemini adapter notes

Apply the same rule: map capabilities and model requirements at the adapter boundary. Provider-specific commands, subagent conventions, or context mechanisms must not leak into `.agents/` canonical artifacts.

## Generic adapter

A generic adapter should target the minimum useful environment:

```text
model chat
+ file read
+ file edit
+ command execution
+ user interaction
```

If a Skill requires advanced features such as delegation or Unity interaction, the generic adapter should report them unavailable rather than pretending support.

## Future Axit-Code adapter/runtime

Axit-Code is expected to become more than an adapter because it owns orchestration and enforcement. It should nevertheless consume the same semantic contracts:

```text
Skill/Profile
  -> capability resolution
  -> model routing
  -> Agent Runtime
  -> Harness policy
  -> Tool Gateway
  -> Verification
  -> Run Ledger
```

This is the main reason the spec must stay independent from Claude Code today.