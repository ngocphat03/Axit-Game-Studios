# Runtime Binding v1 Validation

Status: active

This checklist validates the Runtime Binding layer after Capability semantic v1 has been accepted as stable.

The goal is to prove that transport-specific execution can be introduced without leaking provider/tool semantics back into Core, Workspace/System routing, or canonical Capability ids.

## Current binding state

The `unity-client` System references the reviewed active `coplaydev-unity-mcp` binding. The active mapping set is exactly:

```text
unity.prefab.inspect
unity.serialized-fields.inspect
unity.playmode.verify
unity.compile
```

The other five declared Unity capabilities remain explicitly unbound:

```text
unity.project.inspect
unity.tests.run
unity.scene.inspect
unity.component.inspect
unity.console.inspect
```

`active` records the reviewed source-controlled mapping. It does not claim that the transport is currently connected or available; runtime availability must still be resolved for each acquisition.

The identity-resource and Console-diagnostic operations inside
`unity.compile` are composite-acquisition suboperations. They do not create
separate mappings for `unity.project.inspect` or `unity.console.inspect`.

The discovery rules below continue to apply to any new or expanded mapping. Do not fabricate a binding or operation name.

## Active reviewed binding scope

The M1 first vertical slice remains semantically unchanged:

```text
unity.prefab.inspect
unity.serialized-fields.inspect
unity.playmode.verify
```

M3 adds only the proven `unity.compile` mapping. Leave the remaining five
Unity capabilities unbound until a live task demonstrates REQUIRED need and
the same validation criteria are satisfied.

## Case 1 — real transport discovery before mapping

Before creating a binding file, inspect the actual local runtime/Antigravity environment and answer:

- Which Unity transport/adapter is actually available?
- What concrete operations does it expose?
- Can it reach the intended Unity project/editor instance?
- What structured/raw results do the relevant operations return?

Pass condition:

- every concrete transport operation written into a binding was observed in the actual transport interface;
- no tool/server/action name is inferred from memory or examples.

## Case 2 — semantic id discipline

A binding may map only Capability ids declared by the affected System's active capability sets.

The current active semantic ids are exactly:

```text
unity.prefab.inspect
unity.serialized-fields.inspect
unity.playmode.verify
unity.compile
```

A transport operation name may be provider-specific inside the binding, but it must not replace or rename the semantic Capability id.

Failure examples:

```text
mcp.get_prefab used as the Axit Capability id
unity_mcp_play_mode used as a new semantic id
prefab.read_fields invented instead of unity.serialized-fields.inspect
```

## Case 3 — binding definition is not runtime availability

A reviewed binding file may exist while the editor, MCP server, process, socket, or environment is unavailable on a specific run.

Expected behavior:

```text
binding definition exists
+ runtime transport unavailable
=> acquisition status: unavailable
=> not product FAIL by itself
```

Do not commit machine-specific `connected: true/false` state as canonical truth.

## Case 4 — acquisition outcome classification

The runtime/binding must preserve these distinctions:

```text
acquired
unavailable
denied
transport_error
```

Examples:

- prefab operation successfully returns inspected serialized state -> `acquired`;
- no compatible editor/server is reachable -> `unavailable`;
- Harness policy refuses Play Mode -> `denied`;
- MCP call crashes before trustworthy target evidence is returned -> `transport_error`.

Only acquired target evidence can directly demonstrate product behavior/configuration. The other statuses describe acquisition state, not product correctness.

## Case 5 — permission boundary

A binding for `unity.playmode.verify` must not imply automatic permission to enter or drive Play Mode.

Pass condition:

- Runtime/Harness policy remains authoritative;
- a denied operation is reported as denied/unavailable evidence as appropriate;
- no binding file broadens source-writing or destructive permissions.

## Case 6 — secret and endpoint discipline

Do not store in `.axit`:

- credentials or tokens;
- machine-specific absolute editor/socket paths when they are ephemeral;
- local-only ports that change per session;
- authentication secrets;
- other environment secrets.

A source-controlled binding may reference a stable logical runtime connection/adapter identity when one exists, while actual connection details remain external.

## Case 7 — first vertical slice

The M1 first vertical slice validated this real criterion chain with its three
original mappings:

```text
ordinary deterministic C# tests
        +
unity.prefab.inspect
        +
unity.serialized-fields.inspect
        +
unity.playmode.verify
        -> verify-change
```

Suggested criterion:

- the Player prefab has the accepted `DamageableBodyPart` serialized configuration;
- the exact accepted hit-zone/armor values/references are present;
- a bounded Play Mode headshot produces the expected runtime health result;
- the deterministic damage calculation regression tests still pass.

Expected evidence boundaries:

- deterministic tests prove pure arithmetic rules;
- prefab/serialized capabilities prove asset configuration only;
- Play Mode proves the exercised runtime behavior only;
- `verify-change` owns final criterion mapping and verdict.

## Case 8 — runtime target identity must be resolved from current runtime context

M1 and M2 both demonstrated the same failure pattern: a prefab-relative hierarchy was treated as a complete live scene hierarchy and omitted the `GameEnvironment/` runtime wrapper.

For any path-addressed runtime acquisition or mutation:

```text
prefab/source hierarchy
!=
full runtime hierarchy
```

Required procedure:

1. acquire or derive the current scene/runtime hierarchy using current read-only evidence available to the task;
2. resolve the complete runtime target path/identity before the first path-addressed mutation/acquisition;
3. freeze that resolved identity only for the current acquisition window;
4. if refresh/reload invalidates the identity, resolve it again rather than guessing or reusing stale identifiers;
5. perform mandatory cleanup even when target resolution/acquisition fails.

Do not invent a new Capability merely to satisfy this procedure. Ordinary read-only source/scene evidence may resolve identity when sufficient. Add/bind a broader scene/component capability only if a later accepted criterion demonstrates that such evidence is REQUIRED and ordinary evidence is insufficient.

Pass condition:

- no full runtime target path is synthesized solely from prefab-relative names;
- the evidence record states how current target identity was resolved;
- stale runtime instance ids/paths are not persisted as canonical truth.

## M3 live validation outcome — `unity.compile`

### Live-discovered operation contract

The actual CoplayDev unity-mcp interface exposed the composite operation
surface used by the promoted mapping:

- `resources/read` for current instance, project, and editor-state identity;
- `read_console(action=clear)` to open a fresh diagnostic window;
- `refresh_unity(mode=force, scope=scripts, compile=request,
  wait_for_ready=false)` to request the script compile;
- bounded editor-state reads that retain the exact pre-request state and
  correlate a fresh compile cycle before a separate terminal-ready
  observation;
- post-reload instance/project identity reads; and
- `read_console(action=get, types=[error, warning], format=detailed)` paged
  from cursor zero until `nextCursor` is null.

The compile request being accepted is not compile success. After the exact
pre-request state and request acceptance, fresh-cycle correlation requires any
one of: a successful post-request state payload showing compilation, domain
reload, asset updating, or tools-not-ready; a post-request compile start or
finish marker advanced relative to its retained pre-request value; or a
post-request domain-reload marker advanced relative to its retained
pre-request value. A separately observed terminal-ready state, exact
post-reload identity, and the complete diagnostic page set are then required
for acquired evidence. Project-identity resources and Console operations stay
internal to `unity.compile` and do not map another Capability.

### Exercised evidence chain

- A fresh full-project baseline acquisition completed cleanly as
  `acquired + compilation succeeds`, with exact pre/post project-editor
  identity and complete diagnostics containing no compiler errors.
- A reversible compile-error fixture produced
  `acquired + compilation errors`. The fixture and its generated metadata
  were removed, their marker was confirmed absent, Unity observed the
  cleanup, and a separate fresh acquisition then returned
  `acquired + compilation succeeds` with a complete empty diagnostic window.
- Declarative decision fixtures preserved `unavailable`, `denied`, and
  `transport_error` as acquisition states. They did not manufacture an editor
  disconnect, permission change, or product failure, and none was treated as
  a compile verdict.
- REAL scenario `M3-REAL-01` independently reacquired the full-project compile
  after the production C# change. The exact project/editor matched before and
  after the request, diagnostics paging completed without compiler errors,
  and the required compile criterion received `PASS` in that scenario's
  independent verification.

### Unity 6 reload boundary

The exercised editor was Unity `6000.4.8f1`. A Unity 6 domain reload can
temporarily interrupt a state read and can reset acquisition-local compiler
timing fields, and a fast compile cycle can complete without an observable
nonterminal boolean snapshot. The binding therefore accepts the three
fresh-cycle alternatives above and does not require ephemeral session-id
equality across an expected reload. Every pre/post marker comparison must
belong to the same exact project/editor target. Session ids, editor instance
hashes, and marker timestamps remain acquisition-local and are not persisted.
A transient resource not-ready response alone is not correlation; it must be
followed by an accepted fresh marker and a separate terminal-ready
observation. The exact editor/project is re-resolved after reload, and
diagnostics are accepted only after terminal-ready state and complete paging.

### Closure repair loop 1 — correlation-contract alignment

Closure repair loop 1 aligns the promoted mapping with the Phase 1
live-discovered protocol. The earlier wording made a sampled post-request
nonterminal boolean state mandatory even though the accepted protocol also
allowed an advanced compile start/finish marker or an advanced domain-reload
marker. The repaired contract requires `fresh_cycle_correlated` through any
one accepted alternative and separately requires `terminal_state_observed`.
Request acceptance alone remains insufficient, and no operation, argument,
result path, permission boundary, acquisition outcome, verdict boundary, or
Capability partition changes in this repair.

### Current provenance

- The canonical baseline is System `unity-client`, repository
  `ngocphat03/QuickGun-MVP`, ref `release`, commit
  `c35143a6ea72dd17e591e67b1e965e10a0b15a27`:
  `system-canonical-pushed`.
- The REAL scenario's current production/test edits and the current Workspace
  binding/checklist artifacts are `local-or-separately-tracked`; pushed state
  is not inferred.
- Live editor identity, compile state, and diagnostics are
  `ephemeral-runtime`. No acquisition-local connection identity is canonical
  binding state.

### Promoted and unbound scope

```text
active:
  unity.prefab.inspect
  unity.serialized-fields.inspect
  unity.playmode.verify
  unity.compile

unbound:
  unity.project.inspect
  unity.tests.run
  unity.scene.inspect
  unity.component.inspect
  unity.console.inspect
```

M3 Phase 8 classified no capability as `REQUIRED_NOW`, so no additional
mapping is authorized.

## Acceptance criteria for Runtime Binding v1 and later expansions

A Runtime Binding mapping is ready to promote when:

- the transport and concrete operation names were verified from the actual environment;
- only demonstrated Capability ids are mapped;
- semantic Capability ids remain unchanged;
- current availability is resolved at runtime rather than inferred from committed config;
- acquired/unavailable/denied/transport_error remain distinct;
- permissions remain under Runtime/Harness control;
- no secrets or ephemeral machine state are committed;
- path-addressed runtime operations obey current target-resolution discipline;
- the exercised vertical slice produces traceable evidence and a correct `verify-change` verdict.

Apply these criteria to every candidate or expanded binding scope. Until they
are met for a Capability, keep that Capability unbound. The current active
`unity-client` scope is limited to the four proven mappings above; the other
five declared capabilities remain unbound.
