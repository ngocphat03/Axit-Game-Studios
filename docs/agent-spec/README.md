# Axit Agent Spec v1

## Status

**Draft / experimental.** This specification is introduced in `Axit-Game-Studios` as a provider-neutral design layer before any migration of the existing Claude-specific agents and skills.

The current `.claude/` implementation remains untouched and authoritative for the existing workflow until individual artifacts are migrated deliberately.

## Goal

Axit Agent Spec defines **how an AI-assisted game-development system describes work**, without depending on Claude Code, Codex, Gemini, OpenAI, Anthropic, or any future provider.

The canonical specification must describe:

- what an agent role is responsible for;
- what a skill knows how to do;
- what capabilities the runtime must provide;
- what context must be loaded;
- what outputs and verification are required;
- how failures, escalation, and approval are expressed.

Provider-specific runtimes are adapters, not sources of truth.

```text
Canonical Axit Agent Spec
        |
        +--> Claude adapter
        +--> Codex adapter
        +--> Gemini adapter
        +--> Generic adapter
        +--> Axit-Code runtime (future)
```

## Design principles

### 1. Provider-neutral by default

Canonical artifacts MUST NOT depend on provider-specific primitives such as:

- Claude `Task`;
- Claude `AskUserQuestion`;
- Claude `PreToolUse` / `PostToolUse` hooks;
- hard-coded model names such as `sonnet`, `opus`, or `haiku`;
- provider-specific tool names when a semantic capability can describe the requirement.

Provider-specific details belong in adapters.

### 2. Capabilities instead of tool names

Skills and profiles request semantic capabilities:

```yaml
capabilities:
  required:
    - file.read
    - code.edit
    - test.run
    - user.approval
```

An adapter maps those capabilities to the concrete runtime primitives available in that environment.

### 3. Skills are executable specifications, not prompts

A skill is composed of:

```text
Purpose
+ Inputs
+ Preconditions
+ Context requirements
+ Capability requirements
+ Procedure
+ Outputs
+ Verification
+ Failure / recovery behavior
```

Markdown may contain expert guidance, heuristics, and examples, but runtime-significant requirements must be represented structurally.

### 4. Agent profiles define responsibility, not identity

A profile describes a specialized role through:

- responsibilities;
- boundaries;
- skills;
- capability requirements;
- model capability requirements;
- escalation relationships;
- verification expectations.

A profile does not imply a dedicated model instance and must not hard-code a provider.

### 5. Model selection is capability-based

Canonical profiles describe model needs such as:

```yaml
model_requirements:
  reasoning: medium
  coding: high
  long_context: preferred
  tool_use: required
```

The active runtime chooses an appropriate provider/model.

### 6. Knowledge is not security

Rules, profiles, skills, and prompt instructions may constrain behavior, but they are not a security boundary.

In Claude Code today, adapters may use permissions and hooks as the best available enforcement mechanism. In Axit-Code later, side effects must be enforced by the Harness / Tool Gateway independently of model compliance.

### 7. Verification is explicit

A skill must describe evidence required for completion. The agent saying "done" is never sufficient when deterministic evidence exists.

Examples:

- compilation succeeds;
- tests pass;
- acceptance criteria map to evidence;
- modified files remain in scope;
- Unity Console contains no new errors;
- required artifacts exist.

## Canonical artifact types

The initial v1 specification defines three core artifact types:

1. [`Skill Spec`](skill-spec-v1.md)
2. [`Agent Profile Spec`](agent-profile-spec-v1.md)
3. [`Capability Spec`](capability-spec-v1.md)

Provider adapter rules are described in [`provider-adapters.md`](provider-adapters.md).

Workflow Spec will be defined after these three contracts are tested against real migrated skills. This is intentional: do not create a workflow abstraction until Skill/Profile/Capability boundaries have been validated.

## Proposed canonical layout

The future provider-neutral source tree should converge toward:

```text
.agents/
├── profiles/
├── skills/
├── workflows/
├── rules/
├── knowledge/
├── schemas/
└── config.yaml

adapters/
├── claude/
├── codex/
├── gemini/
└── generic/
```

Existing `.claude/` content is not moved in this first change.

## Migration strategy

Every existing agent/skill should be audited into one of four categories:

- **Keep** — semantics are already strong; translate into the new contract.
- **Merge** — overlapping artifacts should become one canonical skill/profile.
- **Redesign** — useful intent, but structure or provider coupling must change.
- **Delete** — redundant, low-value, or inseparable from a provider-specific behavior.

Migration should happen one vertical slice at a time.

Recommended first slice:

```text
story-readiness
    -> dev-story
    -> code-review
    -> story-done
```

`dev-story` and `gameplay-programmer` are included as example translations under `docs/agent-spec/examples/`.

## Relationship to Axit-Code

Axit-Game-Studios is the **spec lab and reference implementation** for how AI-assisted game-development work should be described and coordinated.

Axit-Code will later provide the **product runtime** that consumes these concepts and adds stronger enforcement:

```text
Axit Agent Spec
    -> Agent Runtime
    -> Harness / Tool Gateway
    -> File / Git / Process / Unity tools
    -> Verification
    -> Run Ledger
```

This separation lets the team validate useful agent behavior before committing to runtime abstractions.