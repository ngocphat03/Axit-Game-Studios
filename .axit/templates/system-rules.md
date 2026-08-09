# System Rules

Use this file for durable constraints that apply broadly to one registered System.

Keep rules short and operational. Put detailed framework/API references in system Knowledge instead.

## Scope and change boundaries

- Add only system-specific constraints not already covered by Axit Core or workspace registries.

## Architecture constraints

- Keep system-local decisions in the system architecture file.
- Read root workspace architecture/integration registries before changing cross-system contracts or ownership.

## Implementation constraints

- Add source layout, generated-file, serialization, code-generation, or runtime conventions that apply broadly to this System.

## Validation requirements

- Add checks that are mandatory for specific change categories in this System.
- Put shared provider/consumer verification in repository-level contract/integration tests.

## Notes

- Do not duplicate full API schemas, product documents, or task history here.
- Remove empty sections after bootstrap when they add no value.
