# Core Project Checklist

This checklist contains only concerns that are broadly useful across materially different game projects.

Domain-specific checks such as multiplayer, quests, puzzle solvability, procedural generation, or engine-specific validation do not belong here until they are intentionally introduced as separate extensions.

## Project identity

- [ ] Short project summary exists.
- [ ] Core player loop is explicit.
- [ ] Target player count is explicit.
- [ ] Target platforms are explicit.
- [ ] Current project stage is explicit.
- [ ] Important non-goals are recorded.

## Technical baseline

- [ ] Engine/runtime is known.
- [ ] Exact version is pinned where version drift matters.
- [ ] Build/run path is known.
- [ ] Test/validation surface is known.

## System architecture

- [ ] Major systems are mapped.
- [ ] Shared/high-risk state has an intended owner.
- [ ] Cross-system interfaces are identifiable.
- [ ] Important architecture decisions have a source of truth.

## Validation

- [ ] Static checks are known.
- [ ] Automated checks are known where practical.
- [ ] Manual/playtest evidence is explicit where deterministic validation is impossible.
- [ ] Completion requires evidence rather than model self-report.

## Working state

- [ ] Current task and important decisions can be recovered from files rather than chat history alone.
- [ ] Modified areas and unresolved blockers can be recovered after a new Codex session.

## Core admission

Before adding a Profile, Skill, or Workflow to Core:

- [ ] It is useful across materially different game projects.
- [ ] It has a distinct recurring responsibility or workflow.
- [ ] It is not better kept project-specific.
- [ ] Its required context can be loaded on demand.
- [ ] Its verification/output contract is clear enough to test in a real vertical slice.
