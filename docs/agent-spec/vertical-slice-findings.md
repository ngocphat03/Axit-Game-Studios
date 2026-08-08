# Migration Slice Findings

## Scope

Three materially different slices were translated from the existing Claude-specific implementation into Axit Agent Spec v1:

```text
Production
story-readiness -> dev-story -> code-review -> story-done

Design
brainstorm -> design-system -> design-review

QA
qa-plan -> smoke-check -> regression-suite -> test-evidence-review
```

The goal was to pressure-test Skill/Profile/Capability contracts before defining Workflow Spec v1.

## Finding 1 — Side effects must be explicit

Legacy behavior mixes read-only review, project mutation, and potentially external operations. Tool allowlists are not enough to express intent.

Canonical Skill now declares:

```yaml
execution:
  side_effects: none | project-write | external
```

QA migration proved side effects can depend on mode:

```yaml
side_effect_rules:
  - when: mode == report
    side_effects: none
```

This is important for future Axit-Code Harness policy evaluation.

## Finding 2 — Ownership is semantic, not a subagent requirement

Skills often have a natural owner (`lead-programmer`, `qa-lead`, `game-designer`) but do not inherently require separate model sessions.

```yaml
execution:
  preferred_profile: qa-lead
```

The runtime may implement ownership through profile switching, a subagent, another provider call, or an Axit-Code actor.

## Finding 3 — Verdicts are first-class contracts

Across all slices, downstream behavior depends on finite outcomes:

- READY / NEEDS WORK / BLOCKED;
- APPROVED / NEEDS REVISION;
- PASS / PASS WITH WARNINGS / FAIL;
- ADEQUATE / INCOMPLETE / MISSING;
- COMPLETE / BLOCKED.

Canonical `verdicts` remove the need to parse prose and enable workflow gates.

## Finding 4 — Critical state transitions must be declarative

Completion-oriented skills mutate project state after evidence and approval.

```yaml
state_transitions:
  - when: verdict == complete and user.approval == granted
    target: story.status
    from: in_progress
    to: complete
```

State mutation should never be hidden only in prompt prose.

## Finding 5 — Human collaboration needs semantic interaction checkpoints

Design skills are intentionally collaborative. They require repeated questions, choices, decisions, approvals, and resumable checkpoints.

The canonical layer therefore distinguishes:

- `question` — missing information;
- `choice` — bounded alternative selection;
- `decision` — substantive project/design decision;
- `approval` — authorization for mutation;
- `confirmation` — manual evidence/observation.

Adapters decide whether this becomes a widget, CLI prompt, chat turn, or another UI.

## Finding 6 — Persistent checkpoints matter for long creative work

`design-system` writes approved sections incrementally and maintains active state so work survives compaction/session changes.

Canonical procedure supports checkpoint intent, while the runtime decides storage.

This reinforces the Axit-Code split:

```text
Run Ledger = full audit/history
Working State = compact resumable current state
```

## Finding 7 — Multi-agent work is a strategy, not canonical topology

The legacy repo frequently uses Claude `Task`, sometimes in parallel. Across the migrated slices, most of this behavior can be represented as:

- `agent.consult`;
- `agent.delegate`;
- `agent.parallel`.

Native subagents are optional unless the semantic capability is explicitly required. A runtime may substitute sequential calls or profile switches when correctness is preserved.

## Finding 8 — Conditional verification is essential

Evidence requirements depend on story type, runtime support, mode, and project phase.

Examples:

- Logic -> unit tests;
- Integration -> integration/playtest evidence;
- Visual/Feel -> manual evidence;
- UI -> walkthrough/interaction evidence;
- smoke check -> automated tests when runnable plus manual critical paths.

`verification.conditional` is therefore a validated v1 primitive.

## Finding 9 — Workflows need gates and artifacts, not duplicated procedures

The three slices show that Workflow only needs to compose Skills using:

- dependencies;
- required/optional steps;
- conditions;
- repeatability;
- accepted verdicts;
- persistent artifact/state checks.

A strict required step with completion rules is sufficient to model a gate; no provider-specific gate primitive is necessary.

## Finding 10 — Repeatability is required but unbounded loops are not

System design/review repeats across systems; production repeats across stories. A finite `repeat.over` collection covers this without introducing an autonomous loop engine.

## Finding 11 — Artifact-driven completion is stronger than conversational state

Existing Game Studios workflows already rely on files/status fields as evidence that a phase is complete. The provider-neutral contract preserves this pattern:

```yaml
completion:
  artifacts:
    - ref: design.game_concept
      exists: true
    - ref: story.status
      equals: complete
```

This maps cleanly to Axit-Code verification and resume semantics later.

## Finding 12 — Workflow Spec v1 can remain intentionally small

The migration does not justify a general workflow programming language.

`axit.workflow/v1` therefore defines only:

- Skill references;
- dependency/order;
- simple conditions;
- finite repeat/for-each;
- verdict/artifact completion contracts;
- workflow run state (`completed`, `paused`, `blocked`, `cancelled`).

Retries, context, tools, model selection, interaction details, and state mutation remain owned by Skills/runtime.

## Final architecture conclusion

The provider-neutral model survived three very different real-world slices:

```text
Workflow intent
    -> Skill contract
        -> Agent Profile
        -> Capability requirements
            -> Provider Adapter / Axit-Code Runtime
                -> Policy / Harness
                -> Tools
                -> Verification
                -> Persistent run state
```

The current Claude implementation remains a valuable reference runtime and migration source, but Claude primitives are no longer required to define canonical Game Studios behavior.

## Recommended next step

Do not migrate all remaining skills blindly.

Next, build one **Claude adapter/export proof** that consumes a small canonical vertical slice (for example `story-delivery`) and renders/runs equivalent Claude Code artifacts. Once round-trip behavior is proven, add Codex/OpenAI and Gemini adapters against the same canonical source before scaling migration across the remaining skill catalog.