# Axit Workspace Overrides

This file overrides only the Git-worktree safety behavior from root `AGENTS.md`. All other root Axit instructions remain applicable.

## Git worktree status policy

Before running any Git-status-based readiness or safety check, read `.axit/workspace.yaml` -> `safety.check_git_status`.

- Default is `false`.
- When `false`, do not run or use root, nested-repository, or submodule `git status` as a readiness gate, cleanliness requirement, or blocker.
- When `false`, do not raise `DIRTY_WORKTREE_RISK` merely because files are modified, deleted, untracked, nested, independently tracked, or represented as submodules.
- Treat the current filesystem/source state as the working baseline and continue with bounded task edits.
- When `true`, a delegated safety lane may inspect relevant Git worktree status and may raise `DIRTY_WORKTREE_RISK` only when continuing would materially endanger unrelated work.

This setting controls status-based guarding only. It never authorizes `git reset`, `git checkout` over user work, `git clean`, reverting unrelated changes, destructive deletion, or blind overwrites.
