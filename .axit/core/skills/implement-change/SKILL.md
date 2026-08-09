---
name: implement-change
description: Implement a bounded code or project change from accepted requirements and architecture, preserve scope and contracts, run implementation-side checks, and hand off current evidence for independent verification. Use for features, bug fixes, refactors, migrations, data, or configuration changes once intent is sufficiently clear; do not use to invent product behavior, resolve material architecture decisions, or issue the final PASS/FAIL/BLOCKED verdict.
---

# Purpose

Apply one bounded project change faithfully and leave it ready for independent verification.

Use the `implementation-engineer` responsibility lens. This procedure owns implementation work and local engineering judgment inside accepted constraints; it does not own product intent, architecture authority, or the final completion verdict.

# Inputs

Use only context relevant to the requested change:

- the user request, accepted behavior, or defect description;
- acceptance criteria when available;
- relevant architecture decisions, registry entries, and project rules;
- affected source, data, configuration, assets, and tests;
- current working-tree state and existing uncommitted changes;
- engine/runtime/version constraints when they materially affect implementation;
- required validation commands or project-specific evidence rules.

If acceptance criteria are not written, derive the smallest implementation target from explicit request and current project evidence. Mark assumptions clearly and do not invent new product requirements.

# Procedure

## 1. Bound the change

State the intended outcome and the smallest coherent implementation scope.

Identify what is explicitly out of scope. Inspect the current working tree before editing so unrelated user changes are preserved.

For a bug fix, capture the reported failure or a focused failing test/reproduction before editing when practical. Do not block a clear fix solely because reproduction tooling is unavailable; record the limitation.

## 2. Check decision readiness

Confirm that implementation can proceed without inventing a material decision.

Proceed with local engineering judgment when choices stay inside accepted boundaries, such as internal control flow, private helpers, local data structures, and small refactors required by the change.

Stop or hand off when implementation would require deciding or changing:

- intended player/product behavior;
- public contracts or cross-system interfaces;
- authoritative shared-state ownership;
- major dependency direction or system boundaries;
- networking or persistence topology;
- a new framework, package, service, or project-wide runtime assumption with architectural consequences.

Do not turn uncertainty into an implementation choice merely to keep moving.

## 3. Inspect the affected surface

Read the smallest relevant set of implementation files, tests, project rules, and architecture constraints.

Trace important callers, consumers, serialized/configured references, or lifecycle boundaries when the change can affect them.

Do not recursively preload the whole repository or unrelated Axit knowledge.

## 4. Choose the minimum coherent change

Prefer the smallest change that satisfies accepted behavior and preserves existing contracts.

Avoid unrelated cleanup, speculative abstractions, broad rewrites, or opportunistic dependency changes.

When a small refactor is necessary for correctness or testability, keep it inside the same bounded surface and make the reason visible in the handoff.

For important deterministic domain rules, prefer pure/testable logic separated from engine, networking, persistence, or presentation wrappers when that separation materially improves correctness without creating unnecessary abstraction.

## 5. Implement transparently

Apply code, data, configuration, asset, or project-file changes using the project's allowed mechanisms.

Preserve existing project conventions unless the requested change explicitly revises them.

Handle affected failure paths deliberately. Do not silently weaken validation, swallow errors, bypass project rules, or alter architecture to make the implementation pass.

This Skill does not grant permissions. Destructive or high-impact side effects remain subject to project rules, user approval, and runtime/harness policy.

## 6. Add implementation-side evidence

Add or update focused tests when practical for new or changed deterministic behavior.

Run the smallest relevant implementation-side checks available, such as:

- compile/build/static checks;
- focused unit tests;
- targeted integration tests;
- format/lint checks required by the project;
- engine/editor validation when available and directly relevant.

Implementation-side checks are evidence for handoff, not the final independent verdict.

If a required check cannot run, record the exact limitation instead of claiming the change is complete.

## 7. Inspect the resulting diff

Review the current diff or equivalent change set before handoff.

Confirm that:

- changed files match the bounded scope;
- unrelated user changes were not reverted or overwritten;
- no unintended public contract, state ownership, dependency direction, or architecture change slipped in;
- generated/serialized/configuration changes are intentional;
- warnings, assumptions, skipped checks, and known risks are visible.

If the diff reveals a material boundary change, stop and hand it to the appropriate responsibility before continuing.

## 8. Prepare independent verification

Do not issue the final completion verdict yourself.

Produce a compact handoff for `verify-change` containing the accepted behavior, changed files, implementation-side evidence, skipped checks, and unresolved risks.

If the verifier later returns `FAIL`, treat the fix as a new current change and run this procedure again for the bounded defect. Do not reuse old evidence as if it applied unchanged.

# Stop / Handoff Conditions

Stop or hand off instead of guessing when:

- intended behavior is materially ambiguous -> Game Designer;
- a public contract, system boundary, state owner, dependency direction, topology, or project-wide technical stance must change -> Technical Architect;
- the requested scope materially expands beyond the accepted change -> surface the scope change before editing;
- a destructive migration or irreversible side effect is required but not covered by current project rules/approval;
- required domain expertise or tooling is missing and proceeding would make correctness speculative;
- current repository state conflicts with accepted design/architecture and the conflict cannot be resolved as a local implementation detail.

Do not hand off ordinary local engineering choices merely to avoid making implementation decisions.

# Output

Return a concise implementation handoff containing:

```text
Change:
- intended outcome and bounded scope

Files changed:
- path -> purpose

Implementation notes:
- key local decisions and any bounded refactor

Checks run:
- check -> result

Skipped / unavailable checks:
- check -> reason

Assumptions / deviations:
- explicit list or none

Ready for verification:
- acceptance criteria or expected behaviors to pass to verify-change
```

Do not label the change `PASS`, `FAIL`, or `BLOCKED` as a final completion verdict. That belongs to independent verification.
