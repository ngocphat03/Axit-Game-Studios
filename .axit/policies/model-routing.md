# Axit Model Routing & Cost Policy

Status: active

This policy controls model allocation for Axit continuous milestones and delegated work. Its goal is to maximize total useful throughput and evidence quality per unit of cost, not to maximize model size on every lane.

## Default role allocation

```text
primary orchestrator = gpt-5.6-sol / xhigh
all child lanes       = gpt-5.6-luna / medium
```

`all child lanes` includes, without exception by role name:

- explorers and scouts;
- implementation workers;
- test/build/evidence workers;
- Unity/MCP acquisition workers;
- repair/recovery agents;
- independent verifiers;
- closure verifiers;
- report/retrospective authors;
- any custom project sub-agent unless a current explicit human override says otherwise.

The primary thread remains orchestration-only when delegation is available. A child does not become eligible for a larger model merely because its task is verification, recovery, or closure.

## No silent escalation

A child lane must not autonomously change from Luna to Terra/Sol or raise reasoning above `medium`.

When a child is incomplete, slow, or wrong, use this order:

```text
sharpen/distill task context
  -> steer or resume the same Luna/medium lane
  -> replace with a fresh Luna/medium lane when needed
  -> decompose the task into smaller checkable lanes
  -> reacquire current evidence
```

Do not solve a child failure by silently buying a larger model.

If the accepted recovery/replacement budget is exhausted and the required lane is still unresolved, surface the unresolved lane through the milestone's existing failure/blocker semantics. Do not auto-escalate model class or reasoning effort.

## Human override boundary

Only an explicit current user instruction may authorize a child model/reasoning override.

A bounded override must record:

- lane and reason;
- requested model/reasoning;
- scope/duration;
- result;
- whether the override should expire after the lane/milestone.

An override expires at the end of its stated scope. It must not silently become the new workspace default.

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

## Verification quality under Luna/medium

Independent verification is established by responsibility and evidence independence, not by using a more expensive model.

A verifier must still:

- use fresh context rather than trusting a worker verdict;
- reacquire or inspect current REQUIRED evidence;
- preserve REQUIRED/SUPPORTING and PASS/FAIL/BLOCKED semantics;
- remain read-only when its role requires it;
- reject stale evidence and overclaiming.

If a criterion is too broad for reliable verification, decompose it into explicit checkable assertions rather than increasing the verifier model automatically.

## Configuration enforcement

Project defaults must remain aligned with this policy:

```toml
model = "gpt-5.6-sol"
model_reasoning_effort = "xhigh"

[agents]
default_subagent_model = "gpt-5.6-luna"
default_subagent_reasoning_effort = "medium"
```

Custom sub-agent definitions must also use `gpt-5.6-luna` / `medium` unless an explicit bounded human override applies.

Long autonomous milestones must record the effective primary and child model/reasoning when observable. Configured intent must not be presented as observed runtime truth.

## Performance accounting

Each long milestone report/retrospective should record, when observable:

```text
primary model/reasoning
child default model/reasoning
child lanes spawned
sub-agent replacements
repair/recovery loops
wall-clock duration
model overrides (expected: 0 unless explicitly human-authorized)
```

A closure verifier must flag an unapproved child model/reasoning escalation as a control finding even when product evidence otherwise passes.

Performance tuning should first improve decomposition, context size, parallel read lanes, serialization boundaries, and recovery behavior. Do not treat larger child models as the default performance fix.
