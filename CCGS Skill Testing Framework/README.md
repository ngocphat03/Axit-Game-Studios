# CCGS Skill Testing Framework

Hạ tầng đảm bảo chất lượng (QA) cho framework **Claude Code Game Studios**.
Kiểm thử chính các skill và agent trong hệ thống — không phải kiểm thử tựa game được xây dựng bằng chúng.

> **Thư mục này hoạt động độc lập và hoàn toàn tùy chọn.**
> Các nhà phát triển game sử dụng CCGS không nhất thiết phải dùng nó. Để xóa hoàn toàn:
> `rm -rf "CCGS Skill Testing Framework"` — không có gì trong `.claude/` bị phụ thuộc vào nó.

---

## Cấu trúc thư mục

```
CCGS Skill Testing Framework/
├── README.md              ← tài liệu bạn đang đọc
├── CLAUDE.md              ← hướng dẫn Claude cách sử dụng testing framework này
├── catalog.yaml           ← danh mục đăng ký tổng: tất cả 73 skills + 49 agents, theo dõi độ bao phủ
├── quality-rubric.md      ← tiêu chí pass/fail theo danh mục cho lệnh /skill-test category
│
├── skills/                ← các file đặc tả hành vi cho skill (mỗi skill một file)
│   ├── gate/              ← đặc tả danh mục gate
│   ├── review/            ← đặc tả danh mục review
│   ├── authoring/         ← đặc tả danh mục authoring
│   ├── readiness/         ← đặc tả danh mục readiness
│   ├── pipeline/          ← đặc tả danh mục pipeline
│   ├── analysis/          ← đặc tả danh mục analysis
│   ├── team/              ← đặc tả danh mục team
│   ├── sprint/            ← đặc tả danh mục sprint
│   └── utility/           ← đặc tả danh mục utility
│
├── agents/                ← các file đặc tả hành vi cho agent (mỗi agent một file)
│   ├── directors/         ← creative-director, technical-director, producer, art-director
│   ├── leads/             ← lead-programmer, narrative-director, audio-director, v.v.
│   ├── specialists/       ← chuyên viên engine/code/shader/UI
│   ├── godot/             ← chuyên viên đặc thù Godot
│   ├── unity/             ← chuyên viên đặc thù Unity
│   ├── unreal/            ← chuyên viên đặc thù Unreal
│   ├── operations/        ← QA, live-ops, release, localization, v.v.
│   └── creative/          ← writer, world-builder, game-designer, v.v.
│
├── templates/             ← template file đặc tả để viết spec mới
│   ├── skill-test-spec.md ← template đặc tả hành vi cho skill
│   └── agent-test-spec.md ← template đặc tả hành vi cho agent
│
└── results/               ← kết quả chạy test (ghi bởi /skill-test spec, gitignored)
```

---

## Cách sử dụng

Tất cả các bài kiểm thử đều được điều khiển bởi hai skill đã tích hợp sẵn trong framework:

### Kiểm tra tuân thủ cấu trúc (Static checks)

```
/skill-test static [skill-name]     # Kiểm tra 1 skill (7 tiêu chí)
/skill-test static all              # Kiểm tra toàn bộ 73 skills
```

### Chạy kiểm thử đặc tả hành vi (Behavioral spec test)

```
/skill-test spec gate-check         # Đánh giá skill đối chiếu với bản đặc tả đã viết
/skill-test spec design-review
```

### Kiểm tra đối chiếu tiêu chí danh mục (Category rubric)

```
/skill-test category gate-check     # Đánh giá 1 skill đối chiếu với tiêu chí danh mục của nó
/skill-test category all            # Chạy kiểm tra rubric trên tất cả các skill đã phân loại
```

### Xem bức tranh toàn cảnh về độ bao phủ (Coverage)

```
/skill-test audit                   # Skills + agents: có-spec, lần test gần nhất, kết quả
```

### Cải tiến một skill bị lỗi

```
/skill-improve gate-check           # Vòng lặp: Test → chẩn đoán → đề xuất sửa → test lại
```

---

## Các danh mục Skill

| Danh mục | Skills | Các chỉ số chính |
|---|---|---|
| `gate` | gate-check | Đọc chế độ review, bảng director full/lean/solo, không tự ý chuyển giai đoạn |
| `review` | design-review, architecture-review, review-all-gdds | Chỉ đọc, kiểm tra 8 phần, kết luận chính xác |
| `authoring` | design-system, quick-design, art-bible, create-architecture, … | Ghi từng phần có xin phép May-I-write, tạo khung sườn trước |
| `readiness` | story-readiness, story-done | Làm nổi bật điểm nghẽn blocker, cổng director ở chế độ full |
| `pipeline` | create-epics, create-stories, dev-story, map-systems, … | Kiểm tra phụ thuộc thượng nguồn, luồng bàn giao rõ ràng |
| `analysis` | consistency-check, balance-check, code-review, tech-debt, … | Báo cáo chỉ đọc, từ khóa kết luận, không tự ý ghi file |
| `team` | team-combat, team-narrative, team-audio, … | Khởi tạo đủ các agent cần thiết, làm nổi bật điểm bị chặn |
| `sprint` | sprint-plan, sprint-status, milestone-review, … | Đọc dữ liệu sprint, có từ khóa trạng thái |
| `utility` | start, adopt, hotfix, localize, setup-engine, … | Vượt qua các kiểm tra tĩnh |

---

## Phân tầng Agent

| Phân tầng | Agents |
|---|---|
| `directors` | creative-director, technical-director, producer, art-director |
| `leads` | lead-programmer, narrative-director, audio-director, ux-designer, qa-lead, release-manager, localization-lead |
| `specialists` | gameplay-programmer, engine-programmer, ui-programmer, tools-programmer, network-programmer, ai-programmer, level-designer, sound-designer, technical-artist |
| `godot` | godot-specialist, godot-gdscript-specialist, godot-csharp-specialist, godot-shader-specialist, godot-gdextension-specialist |
| `unity` | unity-specialist, unity-ui-specialist, unity-shader-specialist, unity-dots-specialist, unity-addressables-specialist |
| `unreal` | unreal-specialist, ue-gas-specialist, ue-replication-specialist, ue-umg-specialist, ue-blueprint-specialist |
| `operations` | devops-engineer, security-engineer, performance-analyst, analytics-engineer, community-manager |
| `creative` | writer, world-builder, game-designer, economy-designer, systems-designer, prototyper |

---

## Cập nhật catalog

`catalog.yaml` theo dõi độ bao phủ kiểm thử cho mọi skill và agent. Sau khi chạy test:

- `/skill-test spec [name]` sẽ đề xuất cập nhật `last_spec` và `last_spec_result`
- `/skill-test category [name]` sẽ đề xuất cập nhật `last_category` và `last_category_result`
- `last_static` và `last_static_result` được cập nhật thủ công hoặc qua `/skill-improve`

---

## Viết một bản đặc tả mới

1. Tìm template đặc tả tại `templates/skill-test-spec.md`
2. Sao chép nó sang `skills/[category]/[skill-name].md`
3. Cập nhật trường `spec:` trong `catalog.yaml` để trỏ tới file mới
4. Chạy `/skill-test spec [skill-name]` để xác thực

---

## Gỡ bỏ framework này

Thư mục này không gắn hook vào dự án chính. Để gỡ bỏ:

```bash
rm -rf "CCGS Skill Testing Framework"
```

Các skill `/skill-test` và `/skill-improve` vẫn hoạt động — chúng sẽ thông báo thiếu `catalog.yaml` và gợi ý chạy `/skill-test audit` để khởi tạo lại.
