# Project Customization Checklist v1

## Purpose

This checklist turns the broad coverage of the original Game Studios catalog and the practical lessons from Black Commission into a **conditional project bootstrap and specialization process**.

The checklist is not a mandatory pipeline. It is a decision surface:

```text
Universal checks
    + selected feature checks
    + engine/runtime checks
    = project-specific Studio configuration
```

Use it when:

- starting a new game project from the reusable base;
- onboarding an existing game into the base;
- adding a major domain such as multiplayer, quests, procedural generation, or persistence;
- deciding whether a new Agent/Skill belongs in Core, a Pack, or the Project Layer.

---

# A. Universal Project Checklist

These apply to almost every project.

## A1. Product identity

- [ ] Project has a short summary.
- [ ] Core player loop is written as a compact sequence.
- [ ] Target player count is explicit.
- [ ] Target platforms are explicit.
- [ ] Current production stage is explicit.
- [ ] 3–7 game/product pillars or equivalent design constraints exist when appropriate.
- [ ] Non-goals / out-of-scope areas are recorded.

**Artifact target:** `.agents/project.yaml` plus project design docs.

## A2. Engine and technical baseline

- [ ] Engine is selected.
- [ ] Exact engine version is pinned.
- [ ] Primary language/runtime is known.
- [ ] Build/run method is known.
- [ ] Test framework availability is known.
- [ ] Editor automation/tooling surface is known.
- [ ] Engine/API knowledge that may be newer than model knowledge is identified.

## A3. System map

- [ ] Major game systems are listed.
- [ ] System dependencies are visible.
- [ ] Each shared state has one intended owner.
- [ ] Cross-system interfaces are identified.
- [ ] High-risk architecture decisions are identified before implementation.
- [ ] Systems whose design is still provisional are marked provisional.

**Recommended artifact:** `.agents/registry/architecture.yaml`.

## A4. Validation baseline

- [ ] Static checks are defined.
- [ ] Automated tests that can run without the full game are identified.
- [ ] Engine/editor validation commands are identified.
- [ ] Smoke/playtest path is defined.
- [ ] Feature completion requires evidence, not model self-report.
- [ ] Known manual-only verification is explicitly documented.

Black Commission's useful pattern here is not its exact commands; it is having a small repeatable baseline that every agent can run or report as unavailable.

## A5. Working state and recovery

- [ ] Current work has a file-backed checkpoint location.
- [ ] Current task / decisions / modified files / blockers are recoverable without chat history.
- [ ] Long design or implementation sessions write approved results incrementally.
- [ ] Provider/session replacement does not destroy project knowledge.

## A6. Human authority and approval

- [ ] Final product/design decision maker is known.
- [ ] Destructive or expensive operations requiring approval are listed.
- [ ] Commit/push/release policy is explicit.
- [ ] AI may not treat prompt instructions as the only security boundary.

## A7. Core specialization decision

For every proposed new Agent or Skill:

- [ ] Is this already covered by a Core Profile/Skill?
- [ ] Is it reusable across one class of games? If yes, prefer a Pack.
- [ ] Is it specific to this project? If yes, keep it Project-local.
- [ ] Does it have a distinct responsibility/output/verification contract?
- [ ] Will it be reused enough to justify a named Agent/Skill?

Do not create an Agent merely because a human studio has that job title.

---

# B. Unity Checklist — only if `engine-unity` selected

- [ ] Unity version is pinned.
- [ ] Package versions that affect architecture are recorded.
- [ ] Assembly boundaries are intentional where useful.
- [ ] EditMode vs PlayMode test responsibilities are defined.
- [ ] Scene mutation strategy is known: hand-authored, builder tools, runtime generation, or hybrid.
- [ ] High-level Editor tools exist for repetitive risky scene operations where practical.
- [ ] Unity Console compile/runtime errors are part of verification.
- [ ] Scene/assets are saved and reloadable from a cold editor state.
- [ ] Required prefabs/assets are discoverable from clean project load.
- [ ] Runtime-created state is distinguished from persisted scene/asset state.

### Unity AI/editor integration

If AI manipulates Unity:

- [ ] Editor bridge/tooling is selected (for example MCP).
- [ ] Skills use semantic capabilities (`unity.inspect`, `unity.modify`) rather than MCP tool names.
- [ ] Repetitive multi-step Editor work is wrapped in high-level deterministic tools when possible.
- [ ] Tool success is followed by state/evidence verification.
- [ ] Domain reload, compile, save, and cold-load failure cases are considered.

Black Commission is a reference for this pattern: use MCP as transport, but move important repeatable operations into project Editor tooling.

---

# C. Multiplayer / Online Checklist — only if multiplayer enabled

## C1. Topology and authority

- [ ] Network topology is explicit: host-client / P2P / dedicated / deterministic / other.
- [ ] Gameplay authority owner is explicit.
- [ ] Every shared gameplay state has one authoritative writer.
- [ ] Client intent vs authoritative mutation is clearly separated where applicable.
- [ ] Presentation-only replicated effects are distinguished from gameplay truth.

## C2. Session lifecycle

- [ ] Host/create flow is defined.
- [ ] Join flow is defined.
- [ ] Player-cap enforcement is defined.
- [ ] Build/protocol compatibility policy is defined.
- [ ] Disconnect handling is defined.
- [ ] Host disconnect behavior is defined.
- [ ] Late join behavior is defined or explicitly unsupported.
- [ ] Reconnect behavior is defined or explicitly unsupported.

## C3. Network verification

- [ ] Solo/offline behavior is tested if supported.
- [ ] Minimum multi-peer case is tested.
- [ ] Maximum supported player count is tested.
- [ ] State equality between peers is verified for critical outcomes.
- [ ] Duplicate/reordered/retried intent cannot double-apply important actions.
- [ ] Network scene transitions are verified.

Black Commission demonstrates a useful baseline: host-authoritative shared state + explicit 1/2/4-player verification. Do not inherit its exact topology unless the project chooses it.

---

# D. P2P Checklist — only if P2P selected

- [ ] Define what "P2P" means for this project: transport-only peers, distributed authority, host election, deterministic simulation, etc.
- [ ] Peer ownership model is explicit per state category.
- [ ] Conflict resolution rule exists for simultaneous writes.
- [ ] Peer leave/ownership transfer is defined.
- [ ] Trust/cheat assumptions are documented.
- [ ] NAT/session discovery/relay needs are documented.
- [ ] Save/progression authority is not accidentally tied to an arbitrary peer unless intended.
- [ ] Test split-brain/desync scenarios relevant to the topology.

If these cannot be answered, do not label the architecture simply "P2P"; select a more precise topology Pack first.

---

# E. Mission / Quest Checklist — only if `mission-quest` selected

## E1. Data vs runtime

- [ ] Mission/Quest Definition is separate from active Mission/Quest Instance state.
- [ ] Definition IDs are stable.
- [ ] Instance IDs exist when multiple runs/parties can own the same definition.
- [ ] Runtime state can be serialized or reconstructed where required.

## E2. State and transitions

- [ ] Core state machine/graph can be tested without networking/editor integration where practical.
- [ ] Conditions and transitions have deterministic semantics.
- [ ] Terminal outcomes are explicit.
- [ ] Duplicate completion cannot award twice.
- [ ] Failure/cancel/abandon semantics are explicit.

## E3. Progress scope

For every objective/progress variable:

- [ ] Scope is known: `player`, `party`, `session`, `world`, or another defined scope.
- [ ] Authority for that scope is known.
- [ ] Persistence lifetime is known.
- [ ] Merge/reconnect semantics are known if online.

## E4. Branching and content complexity

- [ ] Branch conditions are data/contracts rather than scattered ad-hoc booleans where possible.
- [ ] Story/content scripts do not silently own global gameplay state.
- [ ] Optional objectives are distinguished from completion gates.
- [ ] Quest dependencies/cycles are detectable.
- [ ] Designer-facing validation detects unreachable or impossible content where feasible.

## E5. Rewards

- [ ] Reward computation owner is explicit.
- [ ] Reward application is idempotent for important/persistent progression.
- [ ] Reward transaction/claim state survives required failure scenarios.
- [ ] Client cannot fabricate persistent rewards in authoritative online models.

Black Commission's strongest reusable lesson is to isolate pure mission logic from the Unity/NGO wrapper; its current mission data implementation is not a general quest engine and should not be copied wholesale.

---

# F. Puzzle Checklist — only if `puzzle` selected

- [ ] Puzzle initial state is explicit.
- [ ] Valid player actions are explicit.
- [ ] Success condition is explicit.
- [ ] Reset/restart behavior is explicit.
- [ ] Soft-lock conditions are identified.
- [ ] Puzzle state is deterministic enough to test where practical.
- [ ] If procedural, generated puzzles are solvable or validated before presentation.
- [ ] Hint system reads puzzle state without bypassing ownership rules.
- [ ] Save/reload preserves or intentionally resets puzzle state.
- [ ] Multiplayer puzzles define whether progress is shared or per-player.

Useful specialized Skills may include `puzzle-solvability-review`, `puzzle-state-test`, and `hint-authoring`; do not add them to Universal Core.

---

# G. Procedural Generation Checklist — only if selected

- [ ] Seed source is defined.
- [ ] Seed authority is defined for multiplayer.
- [ ] Same seed reproducibility requirements are explicit.
- [ ] Generation invariants are defined (reachability, spawn safety, required anchors, etc.).
- [ ] Generated output has a validator.
- [ ] Invalid generation has bounded retry/fallback behavior.
- [ ] Persist seed vs persist generated result is a deliberate decision.
- [ ] Networking sends the minimum authoritative data needed for peers to agree.

---

# H. Persistence Checklist — only if persistence enabled

- [ ] Persisted state owner is explicit.
- [ ] Save location/backend is explicit.
- [ ] Schema/version exists for long-lived saves.
- [ ] Migration strategy exists when schema changes.
- [ ] Atomicity/failure behavior is defined for important progression.
- [ ] Local settings are separated from authoritative game progression where needed.
- [ ] Multiplayer guest data cannot overwrite host/server/account-owned progression accidentally.
- [ ] Cloud/account sync conflict behavior is defined if relevant.

---

# I. Economy / Progression Checklist — only if selected

- [ ] Currency/resources have one source of truth.
- [ ] Earn/spend operations identify authority.
- [ ] Important operations are idempotent or transactional where needed.
- [ ] Progression gates reference canonical state, not UI/local copies.
- [ ] Tuning data is externalized when designers need iteration.
- [ ] Save and network synchronization semantics are clear.

---

# J. Project-specific Agent Checklist

Create a project-specific Agent Profile only when all are true:

- [ ] It owns a distinct recurring responsibility.
- [ ] It requires a different context/knowledge package from Core profiles.
- [ ] It has clear boundaries and escalation targets.
- [ ] It will be invoked repeatedly or is safety/quality critical.
- [ ] The same result is not better represented as one Skill under an existing profile.

Examples that can be justified in different projects:

```text
network-architect
quest-designer
puzzle-designer
procedural-generation-engineer
combat-designer
technical-level-designer
live-economy-reviewer
```

Avoid creating dozens of specialists by default.

---

# K. Project-specific Skill Checklist

Create a custom Skill when:

- [ ] Task has a repeatable workflow.
- [ ] Inputs/preconditions can be named.
- [ ] Context requirements can be named.
- [ ] Required semantic capabilities can be named.
- [ ] Output can be named.
- [ ] Verification/evidence can be named.
- [ ] Failure/recovery behavior can be bounded.

If the task is only one paragraph of advice or a one-off instruction, keep it as project knowledge/rules instead of creating a Skill.

---

# L. Promotion Checklist: Project -> Pack -> Core

Before promoting a custom artifact:

## Project -> Pack

- [ ] Seen in at least two projects of the same domain/type.
- [ ] Project-specific names/assets/business rules can be removed cleanly.
- [ ] Inputs/context/capabilities can be expressed semantically.
- [ ] Verification remains useful outside the source project.

## Pack -> Core

- [ ] Useful across materially different game types.
- [ ] Does not assume engine/network/genre-specific architecture.
- [ ] Removing it would create duplication across multiple Packs.
- [ ] Core stays small after promotion.

**Default answer is no promotion until reuse is demonstrated.**

---

# M. Project Ready Baseline

A project is considered customized enough to begin serious feature work when:

- [ ] `project.yaml` is populated.
- [ ] Core loop and system map exist.
- [ ] Required Packs are selected.
- [ ] Architecture/state ownership registry exists for shared/high-risk state.
- [ ] Validation baseline exists.
- [ ] Working-state checkpoint mechanism exists.
- [ ] Required custom profiles/skills are generated, but unnecessary specialists are not.
- [ ] One vertical slice can travel from request -> implementation -> verification with the selected runtime/provider.

That last item is the real gate. Documentation completeness alone is not project readiness.
