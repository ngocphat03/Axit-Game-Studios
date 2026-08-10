# Axit Model Routing & Cost Policy

Status: active

This policy controls model allocation for Axit continuous milestones and delegated work. Its goal is to maximize useful throughput and evidence quality per unit of cost, not to maximize model size on every lane.

## Role allocation

```text
primary orchestrator = gpt-5.6-sol / xhigh

child preferred      = gpt-5.6-luna / medium
child compat fallback= gpt-5.6-terra / medium
child Sol            = forbidden unless explicit human override
```

The child policy applies to every delegated role:

- explorers and scouts;
- implementation workers;
- test/build/evidence workers;
- Unity/MCP acquisition workers;
- repair/recovery agents;
- independent verifiers;
- closure verifiers;
- report/retrospective authors;
- custom project sub-agents.

The primary remains orchestration-only when delegation is available.

## Availability-aware routing

Model routing must distinguish **policy preference** from **runtime availability**.

Use this order:

```text
1. Luna / medium, when the current child runtime supports Luna
2. Terra / medium, when Luna is unavailable and Terra is supported
3. STOP, when neither allowed child model is available
```

Do not silently fall back to Sol.

A verified Terra/medium fallback caused only by Luna being unavailable is a compatibility route, not a quality escalation and not a human model override. Record it as:

```text
COMPAT_FALLBACK: LUNA_UNAVAILABLE -> TERRA_MEDIUM
```

Do not infer Luna availability from configuration alone. When the runtime exposes supported child models, use that current observation. If availability is not observable, use the configured child default and report effective child metadata as unavailable rather than inventing it.

## No silent expensive escalation

A child lane must not autonomously use Sol or raise reasoning above `medium`.

When a child is incomplete, slow, or wrong, use this order:

```text
sharpen/distill task context
  -> steer or resume the same allowed child tier
  -> replace with a fresh allowed child
  -> decompose the task into smaller checkable lanes
  -> reacquire current evidence
```

Do not solve a child failure by buying a larger model.

If the accepted recovery budget is exhausted, surface the unresolved lane through the milestone's existing failure/blocker semantics. The orchestrator may not upgrade a child to Sol by itself.

## Human override boundary

Only an explicit current user instruction may authorize a child above the allowed Luna/Terra medium tier.

A bounded human override must record:

- lane and reason;
- requested model/reasoning;
- scope/duration;
- result;
- whether the override expires after the lane/milestone.

An override expires at the end of its stated scope. It must not silently become the workspace default.

## Current compatibility default

A 2026-08-11 M5 readiness run observed that the current Codex child runtime exposed Sol and Terra but not Luna. For this observed runtime generation, project config uses:

```text
child default = gpt-5.6-terra / medium
```

This is a compatibility default, not a permanent preference change. If a future runtime demonstrably exposes Luna children, the project may switch the configured default back to Luna/medium without changing the policy hierarchy.

## Parallelism policy

Use concurrency to reduce wall-clock time only when lanes are materially independent.

- Parallelize read-only exploration, candidate discovery, static analysis, and independent evidence review when useful.
- Serialize overlapping product writes, Runtime Binding writes, state/report writes, and Unity/editor mutations.
- Do not spawn agents merely to fill available slots.
- Close completed or obsolete lanes promptly so useful work can reuse capacity.
- Prefer distilled handoffs over replaying entire conversation or milestone history.

The configured session thread limit is a ceiling, not a target.

## Context-efficiency policy

Child prompts should contain the smallest complete context needed to act correctly:

- exact accepted objective/criterion;
- allowed and forbidden scope;
- direct file/artifact pointers;
- relevant current evidence/state;
- expected compact output schema.

Do not recursively preload `.axit/`, replay full chat history, or send unrelated milestone transcripts to a child.

For continuity/replacement, distill only the last useful result, current state, unresolved work, changed files, and evidence that must be reacquired.

## Verification quality under the child tier

Verification independence comes from responsibility and evidence independence, not from a flagship model.

A verifier must still:

- use fresh context rather than trusting a worker verdict;
- reacquire or inspect current REQUIRED evidence;
- preserve REQUIRED/SUPPORTING and PASS/FAIL/BLOCKED semantics;
- remain read-only when its role requires it;
- reject stale evidence and overclaiming.

If a criterion is too broad for reliable verification, decompose it into explicit checkable assertions rather than increasing the verifier model automatically.

## Configuration enforcement

Primary project defaults remain:

```toml
model = "gpt-5.6-sol"
model_reasoning_effort = "xhigh"
```

The configured child default must be one of the allowed medium routes and should reflect current runtime support:

```toml
[agents]
default_subagent_model = "gpt-5.6-luna"   # preferred when supported
# or
# default_subagent_model = "gpt-5.6-terra" # compatibility fallback

default_subagent_reasoning_effort = "medium"
```

Custom sub-agent definitions must also use the currently selected allowed child model at `medium`, unless a bounded explicit human override applies.

Long autonomous milestones must record configured and effective primary/child values when observable. Configured intent must not be presented as observed runtime truth.

## Performance accounting

Each long milestone report/retrospective should record, when observable:

```text
primary model/reasoning
child configured model/reasoning
child availability observation
child route = PREFERRED_LUNA | COMPAT_TERRA | HUMAN_OVERRIDE
child lanes spawned
sub-agent replacements
repair/recovery loops
wall-clock duration
human model overrides
```

A closure verifier must flag:

- unapproved child Sol usage;
- child reasoning above medium without explicit human override;
- false claims that Luna was used when only Terra was available;
- hidden model fallback not recorded in the report.

A truthful Terra/medium compatibility fallback is compliant.

Performance tuning should first improve decomposition, context size, parallel read lanes, serialization boundaries, and recovery behavior. Do not treat larger child models as the default performance fix.
