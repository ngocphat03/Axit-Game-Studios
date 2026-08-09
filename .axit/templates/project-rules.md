# Project Rules

Use this file for **broad project-local constraints** that should apply across many tasks.

Keep rules short and operational. Put detailed explanations or API references in `.axit/project/knowledge/` instead.

## Scope and change boundaries

- Add only project-specific constraints that are not already covered by Axit Core.

## Design constraints

- Add durable product/gameplay constraints that implementation must not silently change.

## Architecture constraints

- Add broad constraints here only when they apply across tasks.
- Put accepted shared-state ownership, cross-system interfaces, topology decisions, budgets, and forbidden architecture patterns in `.axit/registry/architecture.yaml`.

## Implementation constraints

- Add project naming, layout, generated-file, or code-generation constraints when they apply broadly.

## Validation requirements

- Add project-wide checks that are mandatory for specific change categories.
- Keep command/tool specifics minimal when the same requirement can be expressed semantically.

## Approval requirements

- List project-local changes that require explicit approval before execution.

## Notes

- Do not duplicate full design documents, ADRs, package documentation, or task history here.
- Remove empty sections after project bootstrap if they add no value.
