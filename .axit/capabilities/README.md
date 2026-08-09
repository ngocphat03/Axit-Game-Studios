# Axit Capabilities

This directory contains reusable **semantic capability sets** used to acquire evidence or perform bounded execution steps.

Capabilities are deliberately separate from:

- Profiles — responsibility/decision lenses;
- Skills — repeatable procedures;
- Workflows — composition/transitions;
- runtime bindings — MCP/CLI/editor/Axit-host transport details;
- verification verdicts — owned by `verify-change`.

Canonical form:

```text
.axit/capabilities/<domain>/<set>.yaml
```

Current set:

- `unity/evidence.yaml` — Unity evidence-acquisition operations used when deterministic source/test evidence is insufficient or Unity-specific state must be observed.

Rules:

- capability ids describe semantic intent, not provider/tool command names;
- use only ids explicitly declared by the affected System's active capability sets;
- do not invent, alias, rename, abbreviate, or synthesize capability ids during planning;
- if no declared Capability matches an evidence need, describe the need in ordinary language and report a capability gap unless legitimate non-capability project evidence is sufficient;
- standalone tests, source inspection, logs, and other project mechanisms remain ordinary evidence unless a declared Capability contract actually represents them;
- capability output is evidence, not PASS/FAIL/BLOCKED;
- declaration does not imply a runtime binding is currently available;
- declaration never grants permission or bypasses Runtime/Harness policy;
- load capability context only when the current task actually needs execution/editor evidence.

See [`Capability Spec v1`](../specs/capability-spec-v1.md) for the contract.
