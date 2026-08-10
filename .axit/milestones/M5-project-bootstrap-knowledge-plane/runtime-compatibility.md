# M5 Runtime Compatibility Overlay

Status: accepted
Date: 2026-08-11

This file amends only the child-model availability clauses of the M5 plan and target bootstrap. All other M5 scope, safety, evidence, closure, and no-M6 boundaries remain unchanged.

## Observed failed readiness attempt

The first M5 launch stopped correctly with:

```text
HARD_BLOCKER: TARGET_WORKSPACE_NOT_READY
```

Observed facts:

- the session was rooted at Axit-Game-Studios rather than Axit-Code;
- the Axit-Code local writable path was intentionally unresolved;
- the current Codex child runtime exposed Sol and Terra but did not expose Luna;
- zero child lanes launched;
- no product files changed;
- no Git publish or M6 action occurred.

This was a setup/policy mismatch, not a product or M5 capability failure.

## Accepted child routing for M5 rerun

Use runtime-aware routing from `.axit/policies/model-routing.md`:

```text
primary = gpt-5.6-sol / xhigh

child preferred       = gpt-5.6-luna / medium, when supported
child compat fallback = gpt-5.6-terra / medium, when Luna is unavailable
child Sol             = forbidden unless explicitly human-authorized
```

For the currently observed runtime, M5 may use Terra/medium children and report:

```text
child route: COMPAT_TERRA
reason: LUNA_UNAVAILABLE
```

This is compliant. It is not a human model override and must not count as one.

If a future fresh session exposes Luna, use Luna/medium instead.

If neither Luna nor Terra is available for children, stop rather than falling back to Sol.

## M5 plan amendment

Where the original M5 plan says every child must remain Luna/medium, interpret the accepted requirement as:

```text
all children must remain on the allowed medium child tier
according to current runtime availability
```

Where the original plan would raise `MODEL_ROUTING_NOT_EFFECTIVE` solely because Luna is unavailable, do not raise that blocker if Terra/medium is available and used truthfully.

`MODEL_ROUTING_NOT_EFFECTIVE` still applies to:

- child Sol use without explicit human override;
- child reasoning above medium without explicit human override;
- hidden/unreported fallback;
- false claims about the effective child model.

## Workspace-root requirement remains unchanged

M5 product work still must run from a fresh trusted Codex session opened at the writable Axit-Code repository root.

Do not rerun M5 from Axit-Game-Studios root.

Do not guess an Axit-Code local path. The user selects/opens the real local Axit-Code checkout.
