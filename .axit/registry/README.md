# Axit Workspace Registries

This directory owns repository-level and cross-system routing truth.

- `architecture.yaml` — authoritative ownership, public boundaries, dependency direction, shared decisions, budgets, and forbidden cross-system patterns.
- `integrations.yaml` — provider/consumer relationships, executable contract locations, and boundary-test routes.

System-local architecture belongs under `.axit/systems/<system-id>/architecture.yaml`.

Do not copy OpenAPI/protobuf/DTO/schema bodies into these registries. Point to the real executable contract source so Codex can compare provider and consumer against the same truth.
