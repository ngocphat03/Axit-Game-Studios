# M6-A Loader Foundation — Human Promotion Review

Date: 2026-08-11
Status: HUMAN_PROMOTED
Target repository: `ngocphat03/Axit-Code`
Validated execution branch: `feature/m6a-loader`
Validated baseline: `release@1b8c1fc021c2663728dfce2fb644c48a4a697618`

## Decision

Human review promotes M6-A without rerun.

M6-A proved the accepted ADR-002 Loader Contract v1 inside the authoritative `@axitcode/agent` runtime.

Validated product diff:

```text
packages/axitcode-agent/src/asset-loader.ts
packages/axitcode-agent/src/index.ts
packages/axitcode-agent/test/agent-assets-loader.test.mjs
```

The branch was one commit ahead of the selected release baseline and contained no package/dependency-manifest drift.

## Evidence accepted for promotion

```text
Loader Contract v1             PASS
Implementation                 PASS
Focused loader tests           PASS — 16/16
Package build                   PASS
Package typecheck               PASS
Root npm run verify             PASS
Canonical fixture demonstration PASS
Scope/non-goals                PASS
Closure verifier               PASS
Post-verdict consistency audit PASS
```

Canonical fixture:

```text
.agents/profiles/feature-development.md
```

## Repair history accepted

The first fresh closure verifier found a real contract defect: inline `#` comments in strict v1 frontmatter were misclassified. Bounded repair attempt 1/2 corrected the behavior and added regression tests. Fresh verification then passed.

The first post-verdict consistency audit later found a closure-artifact-only newline mismatch in the fixture manifest. The manifest representation was corrected without product/runtime changes and the fresh audit rerun passed.

Neither incident requires a product rerun.

## Durable lessons

1. A deliberately bounded parser must freeze and test unsupported syntax explicitly; `strict` is not enough unless comments/quoting/blank-lines/coercions are classified.
2. Independent verification must be allowed to find a product defect and reopen one bounded repair loop without invalidating the milestone.
3. When a closure artifact claims byte-exact fixture preservation, terminal-newline semantics are part of the representation claim.
4. Closure metadata should preserve unavailable measurements as unavailable rather than infer configured model/routing values.
5. Future long-run runbooks should record trustworthy start/end timestamps early enough to persist `terminal_end_to_end` without relying on UI history.

Human-observed UI timing for this run was approximately:

```text
terminal user-visible duration = 28m 02s
provenance = human-observed Codex UI screenshot, not repository-durable runtime telemetry
```

Do not use this UI observation as stronger provenance than it has.

## Promotion boundary

Promotion validates the M6-A capability and contract. It does **not** itself merge `feature/m6a-loader` into Axit-Code `release`.

M6-B execution must not start until the chosen Axit-Code execution baseline actually contains the promoted M6-A loader implementation and its accepted ADR-002/fixture prerequisites.

M6-B may be designed now, but execution remains separately gated and human-authorized.
