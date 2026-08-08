# First Vertical Slice Findings

## Scope

This note records what was learned by translating the first production workflow slice:

```text
story-readiness
  -> dev-story
  -> code-review
  -> story-done
```

The purpose is not to freeze a Workflow Spec yet. It is to pressure-test Skill/Profile/Capability v1 against real legacy behavior.

## Finding 1 — Skills need explicit side-effect classification

The legacy set contains both strictly read-only skills (`story-readiness`, `code-review`) and mutating skills (`dev-story`, `story-done`). That distinction was previously encoded only in prose and provider tool allowlists.

Added:

```yaml
execution:
  side_effects: none | project-write | external
```

This gives adapters and future Axit-Code Harness logic a provider-neutral signal before model execution.

## Finding 2 — A Skill needs preferred ownership without requiring a subagent

`code-review` is conceptually owned by `lead-programmer`, while implementation is routed to a programmer profile. The legacy repo expresses this through Claude agent metadata and Task calls.

Added:

```yaml
execution:
  preferred_profile: lead-programmer
```

This means "this role should own the work" without requiring a separate model instance. A runtime can satisfy the routing through profile switching, a subagent, another model call, or an Axit-Code actor.

## Finding 3 — Gate/review Skills produce semantic verdicts

The first slice repeatedly relies on finite outcomes:

- story readiness: READY / NEEDS WORK / BLOCKED;
- code review: APPROVED / APPROVED WITH SUGGESTIONS / CHANGES REQUIRED;
- story completion: COMPLETE / COMPLETE WITH NOTES / BLOCKED.

Added canonical `verdicts` so downstream logic can depend on normalized outcomes rather than parsing prose.

## Finding 4 — Completion Skills need declarative state transitions

`story-done` does more than report: after verification and approval, it changes story/session/sprint state.

Added:

```yaml
state_transitions:
  - when: verdict == complete
    target: story.status
    from: in_progress
    to: complete
```

Critical state mutation should not live only in Markdown guidance.

## Finding 5 — Review mode is configuration/input, not provider behavior

`full | lean | solo` changes which independent review gates are executed. This is valid product behavior and should survive provider changes.

In v1 this is represented as a normal enum input/config value. The underlying implementation of the optional consultation can vary by runtime.

## Finding 6 — Multi-agent consultation is optional execution strategy

The legacy implementation uses Claude `Task` to spawn engine specialists, QA, directors, and lead programmers, sometimes in parallel.

The canonical translation uses:

- `agent.consult`
- `agent.delegate`
- `agent.parallel`

Correctness should not depend on native Claude subagents unless a Skill explicitly declares the semantic collaboration capability as required. Most first-slice consultation is optional and can degrade to same-model profile switching or sequential model calls.

## Finding 7 — Conditional verification is necessary

Evidence requirements depend heavily on story type:

- Logic -> automated unit test;
- Integration -> integration test/playtest;
- Visual/Feel -> manual visual/playtest evidence;
- UI -> walkthrough or interaction evidence;
- Config/Data -> smoke evidence.

The existing `verification.conditional` concept is therefore validated and should remain in v1.

## Finding 8 — Workflow Spec should remain deferred

The four translations can already express their own semantics using Skill/Profile/Capability contracts. The missing concern is composition/order across Skills, but there is not yet enough evidence to decide whether Workflow Spec needs:

- a simple DAG;
- phase/gate semantics;
- artifact-driven transitions;
- state-machine semantics;
- all of the above.

Recommendation: migrate at least two more materially different slices before freezing `axit.workflow/v1`.

Suggested next slices:

1. design: `brainstorm -> design-system -> design-review`;
2. QA: `qa-plan -> smoke-check -> regression-suite -> test-evidence-review`.

## Current conclusion

The provider-neutral direction is holding up under the first real migration.

The strongest emerging separation is:

```text
Skill/Profile/Workflow intent
        -> Capability requirements
        -> Provider adapter / Axit-Code runtime
        -> Policy enforcement
        -> Verification evidence
```

The current Claude implementation remains useful as a reference runtime, but Claude primitives are no longer required to define the canonical behavior.