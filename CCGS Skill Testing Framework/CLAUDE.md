# CCGS Skill Testing Framework — Hướng dẫn cho Claude

Thư mục này là tầng đảm bảo chất lượng (QA layer) cho framework skill/agent của Claude Code Game Studios. Nó hoạt động độc lập và tách biệt khỏi bất kỳ dự án game nào.

## Các file chính

| File | Mục đích |
|---|---|
| `catalog.yaml` | Danh mục đăng ký chính cho tất cả 73 skills và 49 agents. Chứa danh mục, đường dẫn spec, và các trường theo dõi lần test gần nhất. Luôn đọc file này trước tiên khi chạy bất kỳ lệnh test nào. |
| `quality-rubric.md` | Tiêu chí pass/fail theo từng danh mục cụ thể. Đọc mục `###` tương ứng với danh mục của skill khi chạy `/skill-test category`. |
| `skills/[category]/[name].md` | Đặc tả hành vi cho một skill — 5 trường hợp kiểm thử (test cases) + các xác nhận tuân thủ giao thức. |
| `agents/[tier]/[name].md` | Đặc tả hành vi cho một agent — 5 test cases + các xác nhận tuân thủ giao thức. |
| `templates/skill-test-spec.md` | Template để viết các file đặc tả skill mới. |
| `templates/agent-test-spec.md` | Template để viết các file đặc tả agent mới. |
| `results/` | Được ghi bởi `/skill-test spec` khi lưu kết quả. Đã được gitignore. |

## Quy ước đường dẫn

- Skill specs: `CCGS Skill Testing Framework/skills/[category]/[name].md`
- Agent specs: `CCGS Skill Testing Framework/agents/[tier]/[name].md`
- Catalog: `CCGS Skill Testing Framework/catalog.yaml`
- Rubric: `CCGS Skill Testing Framework/quality-rubric.md`

Trường `spec:` trong `catalog.yaml` là đường dẫn chính thức cho từng bản đặc tả skill/agent. Luôn đọc từ đó thay vì suy đoán đường dẫn.

## Danh mục Skill

```
gate        → gate-check
review      → design-review, architecture-review, review-all-gdds
authoring   → design-system, quick-design, architecture-decision, art-bible,
              create-architecture, ux-design, ux-review
readiness   → story-readiness, story-done
pipeline    → create-epics, create-stories, dev-story, create-control-manifest,
              propagate-design-change, map-systems
analysis    → consistency-check, balance-check, content-audit, code-review,
              tech-debt, scope-check, estimate, perf-profile, asset-audit,
              security-audit, test-evidence-review, test-flakiness
team        → team-combat, team-narrative, team-audio, team-level, team-ui,
              team-qa, team-release, team-polish, team-live-ops
sprint      → sprint-plan, sprint-status, milestone-review, retrospective,
              changelog, patch-notes
utility     → tất cả các skill còn lại
```

## Phân tầng Agent

```
directors   → creative-director, technical-director, producer, art-director
leads       → lead-programmer, narrative-director, audio-director, ux-designer,
              qa-lead, release-manager, localization-lead
specialists → gameplay-programmer, engine-programmer, ui-programmer,
              tools-programmer, network-programmer, ai-programmer,
              level-designer, sound-designer, technical-artist
godot       → godot-specialist, godot-gdscript-specialist, godot-csharp-specialist,
              godot-shader-specialist, godot-gdextension-specialist
unity       → unity-specialist, unity-ui-specialist, unity-shader-specialist,
              unity-dots-specialist, unity-addressables-specialist
unreal      → unreal-specialist, ue-gas-specialist, ue-replication-specialist,
              ue-umg-specialist, ue-blueprint-specialist
operations  → devops-engineer, security-engineer, performance-analyst,
              analytics-engineer, community-manager
creative    → writer, world-builder, game-designer, economy-designer,
              systems-designer, prototyper
```

## Quy trình kiểm thử một skill

1. Đọc `catalog.yaml` để lấy đường dẫn `spec:` và `category:` của skill
2. Đọc skill tại `.claude/skills/[name]/SKILL.md`
3. Đọc bản đặc tả tại đường dẫn `spec:`
4. Đánh giá các xác nhận (assertions) theo từng trường hợp
5. Đề xuất ghi kết quả vào `results/` và cập nhật `catalog.yaml`

## Quy trình cải tiến một skill

Sử dụng `/skill-improve [name]`. Lệnh này xử lý toàn bộ vòng lặp:
test → chẩn đoán → đề xuất sửa → viết lại code → test lại → giữ lại hoặc hoàn tác.

## Lưu ý về tính hợp lệ của bản đặc tả

Các spec trong thư mục này mô tả **hành vi hiện tại**, không phải hành vi lý tưởng. Chúng được viết bằng cách đọc trực tiếp từ skill, do đó có thể chứa lỗi tiềm ẩn. Khi một skill hoạt động không đúng trong thực tế, hãy sửa skill trước, sau đó cập nhật spec khớp với hành vi đã sửa. Hãy coi các lỗi spec thất bại là "cần điều tra thêm", không phải "skill chắc chắn đã sai".

## Thư mục này có thể xóa được

Không có thành phần nào trong `.claude/` import từ thư mục này. Việc xóa thư mục này không ảnh hưởng đến bản thân các skill hay agent của CCGS. `/skill-test` và `/skill-improve` sẽ chỉ thông báo thiếu `catalog.yaml` và hướng dẫn người dùng khởi tạo lại.
