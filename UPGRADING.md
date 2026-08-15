# Hướng dẫn nâng cấp Claude Code Game Studios

Tài liệu này hướng dẫn cách nâng cấp repository dự án game hiện tại của bạn từ phiên bản template này lên phiên bản tiếp theo.

**Kiểm tra phiên bản hiện tại của bạn** trong git log:
```bash
git log --oneline | grep -i "release\|setup"
```
Hoặc kiểm tra huy hiệu phiên bản (version badge) trong `README.md`.

---

## Mục lục

- [Chiến lược nâng cấp](#chiến-lược-nâng-cấp)
- [v1.0.0-beta → v1.0](#v100-beta--v10)
- [v0.4.x → v1.0](#v04x--v10)
- [v0.4.0 → v0.4.1](#v040--v041)
- [v0.3.0 → v0.4.0](#v030--v040)
- [v0.2.0 → v0.3.0](#v020--v030)
- [v0.1.0 → v0.2.0](#v010--v020)

---

## Chiến lược nâng cấp

Có ba cách để lấy các bản cập nhật của template. Hãy chọn cách phù hợp với cấu hình repo của bạn.

### Chiến lược A — Git Remote Merge (Khuyến nghị)

Phù hợp nhất khi: Bạn đã clone template và có các commit riêng của mình trên đó.

```bash
# Thêm template làm remote (thiết lập một lần)
git remote add template https://github.com/Donchitos/Claude-Code-Game-Studios.git

# Lấy phiên bản mới về
git fetch template main

# Merge vào nhánh của bạn
git merge template/main --allow-unrelated-histories
```

Git sẽ chỉ báo conflict ở các file mà cả template *lẫn* bạn đều có chỉnh sửa. Giải quyết từng conflict — nội dung game của bạn được giữ lại, các cải tiến về cấu trúc sẽ được tích hợp vào. Sau đó commit bản merge.

**Mẹo:** Các file dễ xảy ra conflict nhất là `CLAUDE.md` và `.claude/docs/technical-preferences.md`, vì bạn đã điền thông tin engine và cài đặt dự án vào đó. Hãy giữ nội dung của bạn; chấp nhận các thay đổi cấu trúc mới.

---

### Chiến lược B — Cherry-pick các commit cụ thể

Phù hợp nhất khi: Bạn chỉ muốn một tính năng cụ thể (ví dụ: chỉ lấy skill mới, không lấy toàn bộ bản cập nhật).

```bash
git remote add template https://github.com/Donchitos/Claude-Code-Game-Studios.git
git fetch template main

# Cherry-pick commit cụ thể mà bạn muốn
git cherry-pick <commit-sha>
```

Commit SHA cho từng phiên bản được liệt kê trong phần phiên bản bên dưới.

---

### Chiến lược C — Sao chép file thủ công (Manual file copy)

Phù hợp nhất khi: Bạn không dùng git để thiết lập template (chỉ tải về file zip).

1. Tải về hoặc clone phiên bản mới song song với repo của bạn.
2. Sao chép trực tiếp các file được liệt kê dưới mục **"An toàn để ghi đè" (Safe to overwrite)**.
3. Đối với các file dưới mục **"Merge cẩn thận" (Merge carefully)**, mở cả hai phiên bản cạnh nhau và merge thủ công các thay đổi cấu trúc trong khi vẫn giữ nguyên nội dung của bạn.

---

## v0.4.1

**Ngày phát hành:** 2026-04-02  
**Chủ đề chính:** Tích hợp Art direction, quy trình đặc tả asset (asset specification pipeline)

### Những thay đổi

| Danh mục | Thay đổi |
|---|---|
| **Skill mới** | `/art-bible` — hướng dẫn tạo nhận diện thị giác từng phần (9 phần). Bắt buộc khởi tạo art-director Task cho mỗi phần. Cổng duyệt AD-ART-BIBLE. Bắt buộc ở giai đoạn Technical Setup. |
| **Skill mới** | `/asset-spec` — công cụ tạo đặc tả hình ảnh và prompt AI generation cho từng asset. Đọc art bible + tài liệu GDD/level/character. Ghi các file `design/assets/specs/` và `design/assets/asset-manifest.md`. Hỗ trợ các chế độ full/lean/solo. |
| **Cổng director mới (3)** | `AD-CONCEPT-VISUAL` (brainstorm Phase 4), `AD-ART-BIBLE` (duyệt art bible), `AD-PHASE-GATE` (bảng kiểm tra gate-check) |
| **Cập nhật `/brainstorm`** | Thêm `Task` vào allowed-tools (trước đây bị thiếu — chặn việc gọi director). Art-director hiện được gọi song song với creative-director sau khi các pillar được khóa. Ghi Visual Identity Anchor vào game-concept.md. |
| **Cập nhật `/gate-check`** | Thêm art-director làm director song song thứ 4 (AD-PHASE-GATE). Kiểm tra sản phẩm thị giác: Visual Identity Anchor (cổng Concept), art bible (cổng Technical Setup), duyệt AD-ART-BIBLE + hồ sơ hình ảnh nhân vật (cổng Pre-Production). |
| **Cập nhật `/team-level`** | Thêm art-director vào Step 1 chạy song song (định hướng hình ảnh trước khi dựng layout). Level-designer nhận mục tiêu từ art-director dưới dạng ràng buộc rõ ràng. Step 4 vai trò art-director được giới hạn chỉ cho production-concepts. |
| **Cập nhật `/team-narrative`** | Thêm art-director vào Phase 2 chạy song song (thiết kế hình ảnh nhân vật, kể chuyện qua môi trường, tông màu cinematic). |
| **Cập nhật `/design-system`** | Bảng định tuyến mở rộng với art-director + technical-artist cho các danh mục Combat, UI, Dialogue, Animation/VFX, Character. Mục Visual/Audio hiện bắt buộc (với art-director Task) cho 7 danh mục hệ thống. |
| **`workflow-catalog.yaml`** | `/art-bible` được thêm vào Technical Setup (bắt buộc). `/asset-spec` được thêm vào Pre-Production (tùy chọn, lặp lại được). |

### Các file: An toàn để ghi đè (Safe to Overwrite)

**Các file mới cần thêm:**
```
.claude/skills/art-bible/SKILL.md
.claude/skills/asset-spec/SKILL.md
.claude/docs/director-gates.md
```

**Các file hiện có ghi đè trực tiếp (không chứa nội dung người dùng):**
```
.claude/skills/brainstorm/SKILL.md
.claude/skills/gate-check/SKILL.md
.claude/skills/team-level/SKILL.md
.claude/skills/team-narrative/SKILL.md
.claude/skills/design-system/SKILL.md
.claude/docs/workflow-catalog.yaml
README.md
UPGRADING.md
```

### Các file: Merge cẩn thận (Merge Carefully)

Không có — tất cả thay đổi đều nằm trong các file hạ tầng không chứa nội dung người dùng.

---

## v1.0.0-beta → v1.0

**Ngày phát hành:** 2026-05-13  
**Phạm vi commit:** `49d1e45..HEAD`  
**Chủ đề chính:** Cổng `/vertical-slice` mới, tinh chỉnh skill & sửa lỗi, tài liệu cho người đóng góp

### Những thay đổi

| Danh mục | Thay đổi |
|---|---|
| **Skill mới** | `/vertical-slice` — Cổng Pre-Production xác thực toàn bộ game loop với bản build hoàn chỉnh đạt chất lượng sản xuất trước giai đoạn Production. Kết hợp với `/prototype` được nâng cấp (xác thực concept ngay sau `/brainstorm`). |
| **Flow mới** | Bước kiểm kê thực thể (Entity inventory) trong `/map-systems` — hiển thị tất cả các thực thể được đặt tên ngay từ đầu để viết GDD mạch lạc hơn. |
| **Tinh chỉnh UX** | Thêm các widget `AskUserQuestion` còn thiếu vào 7 skills; audit toàn diện các skill về tính nhất quán, prompt, và khoảng trống luồng xử lý; thêm cờ `--review` vào `argument-hints` cho tất cả các skill `team-*`. |
| **Sửa lỗi (Bug fixes)** | `#21` hook log-agent ghi nhận "unknown" `agent_type`; `#36` thiếu `allowed-tools` trong `/architecture-decision` và `/story-done`; `#42` `rg --type gdscript` không hợp lệ (đã chuyển sang `--glob *.gd`); `#43` bản xem trước session-start hiển thị trạng thái cũ nhất thay vì mới nhất; `#45` trùng tiêu đề `## 0.` và lỗi đánh số bước trong `/architecture-decision`. |
| **Tài liệu dự án** | Thêm `CONTRIBUTING.md` (hướng dẫn đóng góp framework) và `SECURITY.md` (chính sách công bố bảo mật). |
| **Số lượng & Tham chiếu** | Đồng bộ số lượng agent/skill/hook trên `WORKFLOW-GUIDE.md`, `README.md`, và danh sách agent; sửa tên agent cũ và trường phân tầng model của skill. |

---

### Các file: An toàn để ghi đè (Safe to Overwrite)

**Các file mới cần thêm:**
```
.claude/skills/vertical-slice/SKILL.md
CONTRIBUTING.md
SECURITY.md
```

**Các file hiện có ghi đè trực tiếp (không chứa nội dung người dùng):**
- Tất cả các file trong `.claude/skills/` được sửa đổi trong phạm vi commit (audit skill + widget AskUserQuestion + gợi ý đối số `--review`)
- `.claude/hooks/log-agent.sh` (sửa lỗi #21)
- `README.md`, `docs/WORKFLOW-GUIDE.md`, `docs/examples/skill-flow-diagrams.md`
- `UPGRADING.md`

---

### Các file: Merge cẩn thận (Merge Carefully)

Không có — tất cả thay đổi đều nằm trong các file hạ tầng không chứa nội dung người dùng.

---

## v0.4.x → v1.0

**Ngày phát hành:** 2026-03-29  
**Phạm vi commit:** `6c041ac..HEAD`  
**Chủ đề chính:** Hệ thống Director gates, các chế độ cường độ kiểm duyệt (gate intensity modes), chuyên viên Godot C# specialist

### Những thay đổi

| Danh mục | Thay đổi |
|---|---|
| **Hệ thống mới** | Director gates — các điểm kiểm tra review được đặt tên dùng chung cho tất cả các workflow skill. Được định nghĩa trong `.claude/docs/director-gates.md` |
| **Tính năng mới** | Các chế độ kiểm duyệt: `full` (tất cả các cổng director), `lean` (chỉ các cổng giai đoạn), `solo` (không qua director). Thiết lập toàn cục qua `production/review-mode.txt` trong khi chạy `/start`, hoặc ghi đè cho từng lần chạy với `--review [mode]` trên bất kỳ skill nào có dùng cổng |
| **Agent mới** | `godot-csharp-specialist` — đảm bảo chất lượng code C# trong các dự án Godot 4 |
| **Cập nhật skill (13)** | Tất cả các skill có cổng kiểm duyệt hiện đều phân tích `--review [full\|lean\|solo]` và đưa vào argument-hint: `brainstorm`, `map-systems`, `design-system`, `architecture-decision`, `create-architecture`, `create-epics`, `create-stories`, `sprint-plan`, `milestone-review`, `playtest-report`, `prototype`, `story-done`, `gate-check` |
| **Cập nhật `/start`** | Thêm Phase 3b — thiết lập chế độ review trong quá trình onboarding, ghi `production/review-mode.txt` |
| **Cập nhật `/setup-engine`** | Bước chọn ngôn ngữ cho Godot (GDScript vs C#) |
| **Tài liệu** | `director-gates.md` — toàn bộ danh mục cổng kiểm duyệt; `WORKFLOW-GUIDE.md` — mục Chế độ đánh giá của Director; `README.md` — tùy biến mức độ kiểm duyệt |

---

### Các file: An toàn để ghi đè (Safe to Overwrite)

**Các file mới cần thêm:**
```
.claude/agents/godot-csharp-specialist.md
.claude/docs/director-gates.md
```

**Các file hiện có ghi đè trực tiếp (không chứa nội dung người dùng):**
```
.claude/skills/brainstorm/SKILL.md
.claude/skills/map-systems/SKILL.md
.claude/skills/design-system/SKILL.md
.claude/skills/architecture-decision/SKILL.md
.claude/skills/create-architecture/SKILL.md
.claude/skills/create-epics/SKILL.md
.claude/skills/create-stories/SKILL.md
.claude/skills/sprint-plan/SKILL.md
.claude/skills/milestone-review/SKILL.md
.claude/skills/playtest-report/SKILL.md
.claude/skills/prototype/SKILL.md
.claude/skills/story-done/SKILL.md
.claude/skills/gate-check/SKILL.md
.claude/skills/start/SKILL.md
.claude/skills/quick-design/SKILL.md
.claude/skills/setup-engine/SKILL.md
README.md
docs/WORKFLOW-GUIDE.md
UPGRADING.md
```

---

### Các file: Merge cẩn thận (Merge Carefully)

Không có file nào yêu cầu merge thủ công trong bản phát hành này. Tất cả các thay đổi đều nằm trong các file hạ tầng không chứa nội dung người dùng.

---

### Các tính năng mới

#### Hệ thống Director Gates

Tất cả các workflow skill chính hiện tham chiếu đến các điểm kiểm tra cổng được đặt tên trong `.claude/docs/director-gates.md`. Các cổng được xác định bởi tiền tố lĩnh vực và tên (ví dụ: `CD-CONCEPT`, `TD-ARCHITECTURE`, `LP-CODE-REVIEW`). Mỗi cổng xác định director nào cần gọi, đầu vào cần truyền, ý nghĩa của các kết luận (verdicts), và cách các chế độ lean/solo tác động đến nó.

Các skill gọi cổng bằng `Task` với ID cổng và các đầu vào đã ghi nhận trong tài liệu, thay vì nhúng trực tiếp prompt director vào trong thân skill. Điều này giúp phần thân của skill luôn gọn gàng và giữ cho hành vi của cổng luôn nhất quán qua mọi giai đoạn workflow.

#### Các chế độ cường độ kiểm duyệt (Gate Intensity Modes)

Ba chế độ cho phép bạn kiểm soát mức độ đánh giá từ director:

- **`full`** (mặc định) — tất cả các cổng director đều chạy ở mọi điểm kiểm tra review
- **`lean`** — bỏ qua các đánh giá director ở từng skill riêng lẻ; các cổng giai đoạn tại `/gate-check` vẫn chạy
- **`solo`** — không có cổng director ở bất kỳ đâu; `/gate-check` chỉ kiểm tra sự tồn tại của sản phẩm tài liệu

Thiết lập toàn cục trong `/start` (ghi vào `production/review-mode.txt`). Ghi đè từng lần chạy cụ thể bằng `--review [mode]` trên bất kỳ skill nào có cổng:

```
/design-system combat --review lean
/gate-check concept --review full
/brainstorm my-game-idea --review solo
```

---

### Sau khi nâng cấp

1. Chạy `/start` một lần để thiết lập chế độ review ưa thích — hoặc tạo file `production/review-mode.txt` thủ công với nội dung `full`, `lean`, hoặc `solo`.
2. Nếu bạn đang ở giữa dự án, hãy xem `.claude/docs/director-gates.md` để hiểu cổng nào áp dụng cho giai đoạn hiện tại của bạn.
3. Chạy `/skill-test static all` để xác minh tất cả các skill đều vượt qua kiểm tra cấu trúc.

---

## v0.4.0 → v0.4.1

**Ngày phát hành:** 2026-03-26  
**Phạm vi commit:** `04ed5d5..HEAD`  
**Chủ đề chính:** Các agent trung lập thể loại (Genre-agnostic), các skill mới, sửa lỗi skill

### Những thay đổi

| Danh mục | Thay đổi |
|---|---|
| **Skill mới (1)** | `/consistency-check` — trình quét tính nhất quán của thực thể xuyên suốt các GDD |
| **Sửa lỗi skill (tất cả team-*)** | Thêm guard kiểm tra không có đối số, từ khóa `Verdict: COMPLETE / BLOCKED` chuẩn hóa, cổng AskUserQuestion theo từng bước, kiểm tra phụ thuộc khu vực liền kề (team-level), thực thi đạo đức (team-live-ops), luồng NO-GO kèm bỏ qua Phase (team-release) |
| **Sửa lỗi agent (4)** | Ngôn ngữ trung lập thể loại trong game-designer, systems-designer, economy-designer, live-ops-designer — loại bỏ các thuật ngữ chỉ dành riêng cho RPG |

---

### Các file: An toàn để ghi đè (Safe to Overwrite)

**Các file mới cần thêm:**
```
.claude/skills/consistency-check/SKILL.md
```

**Các file hiện có ghi đè trực tiếp (không chứa nội dung người dùng):**
```
.claude/skills/team-combat/SKILL.md      ← no-arg guard, verdict keywords, cải tiến cổng
.claude/skills/team-narrative/SKILL.md   ← no-arg guard, verdict keywords, cải tiến cổng
.claude/skills/team-ui/SKILL.md          ← no-arg guard, verdict keywords, cải tiến cổng
.claude/skills/team-release/SKILL.md     ← no-arg guard, verdict keywords, luồng NO-GO
.claude/skills/team-polish/SKILL.md      ← no-arg guard, verdict keywords, cải tiến cổng
.claude/skills/team-audio/SKILL.md       ← no-arg guard, verdict keywords, cải tiến cổng
.claude/skills/team-level/SKILL.md       ← no-arg guard, verdict keywords, kiểm tra khu vực liền kề
.claude/skills/team-live-ops/SKILL.md    ← no-arg guard, verdict keywords, thực thi đạo đức
.claude/skills/team-qa/SKILL.md          ← no-arg guard, verdict keywords, cải tiến cổng
.claude/skills/map-systems/SKILL.md      ← verdict keywords
.claude/skills/create-epics/SKILL.md     ← sửa giao thức "May I write", verdict keywords
.claude/skills/create-stories/SKILL.md   ← verdict keywords
.claude/agents/game-designer.md          ← ngôn ngữ trung lập thể loại
.claude/agents/systems-designer.md       ← ngôn ngữ trung lập thể loại
.claude/agents/economy-designer.md       ← ngôn ngữ trung lập thể loại
.claude/agents/live-ops-designer.md      ← ngôn ngữ trung lập thể loại
```

---

### Các file: Merge cẩn thận (Merge Carefully)

Không có file nào yêu cầu merge thủ công trong bản phát hành này. Tất cả thay đổi đều nằm trong các file hạ tầng không chứa nội dung người dùng.

---

### Sau khi nâng cấp

1. Chạy `/skill-test catalog` để xác minh tất cả các skill đã được đánh chỉ mục.
2. Chạy `/skill-test lint [skill-name]` sau bất kỳ chỉnh sửa skill nào để kiểm tra tính tuân thủ cấu trúc.
3. Nếu bạn đã tùy biến bất kỳ skill team-* nào, hãy xem lại các phiên bản đã cập nhật — guard kiểm tra không có đối số và từ khóa `Verdict:` hiện là bắt buộc cho tất cả các skill team-*.

---

## v0.3.0 → v0.4.0

**Ngày phát hành:** 2026-03-21  
**Phạm vi commit:** `b1cad29..HEAD`  
**Chủ đề chính:** Toàn bộ pipeline UX/UI, vòng đời story hoàn chỉnh, tiếp nhận dự án có sẵn (brownfield adoption), framework QA/kiểm thử toàn diện, tính toàn vẹn của pipeline, 29 skill mới

### Những thay đổi

| Danh mục | Thay đổi |
|---|---|
| **Skill mới (17)** | `/ux-design`, `/ux-review`, `/help`, `/quick-design`, `/review-all-gdds`, `/story-readiness`, `/story-done`, `/sprint-status`, `/adopt`, `/create-architecture`, `/create-control-manifest`, `/create-epics`, `/create-stories`, `/dev-story`, `/propagate-design-change`, `/content-audit`, `/architecture-review` |
| **Skill QA mới (12)** | `/qa-plan`, `/smoke-check`, `/soak-test`, `/regression-suite`, `/test-setup`, `/test-helpers`, `/test-evidence-review`, `/test-flakiness`, `/skill-test`, `/bug-triage`, `/team-live-ops`, `/team-qa` |
| **Hook mới (4)** | `log-agent-stop.sh` — dừng audit trail của agent; `notify.sh` — thông báo Windows toast; `post-compact.sh` — nhắc nhở khôi phục phiên sau compaction; `validate-skill-change.sh` — khuyến nghị `/skill-test` sau khi sửa skill |
| **Template mới (8)** | `ux-spec.md`, `hud-design.md`, `accessibility-requirements.md`, `interaction-pattern-library.md`, `player-journey.md`, `difficulty-curve.md`, và 2 template kế hoạch tiếp nhận dự án |
| **Hạ tầng mới** | `workflow-catalog.yaml` (pipeline 7 giai đoạn, đọc bởi `/help`), `docs/architecture/tr-registry.yaml` (các TR-ID ổn định), schema `production/sprint-status.yaml` |
| **Cập nhật skill** | `/gate-check` — 3 cổng hiện yêu cầu sản phẩm UX; Cổng Pre-Production yêu cầu vertical slice (cổng CỨNG - HARD gate) |
| **Cập nhật skill** | `/sprint-plan` — ghi `sprint-status.yaml`; `/sprint-status` đọc file này |
| **Cập nhật skill** | `/story-done` — đánh giá hoàn thành 8 giai đoạn, cập nhật file story, hiển thị story sẵn sàng tiếp theo |
| **Cập nhật skill** | `/design-review` — loại bỏ kiểm tra lỗ hổng kiến trúc (sai giai đoạn) |
| **Cập nhật skill** | `/team-ui` — toàn bộ pipeline UX (ux-design → ux-review → các giai đoạn nhóm) |
| **Cập nhật agent** | 14 specialist agents — thêm `memory: project` |
| **Cập nhật agent** | `prototyper` — `isolation: worktree` (làm việc thử nghiệm trên nhánh git riêng biệt) |
| **Định tuyến model** | Phân công phân tầng Haiku/Sonnet/Opus được ghi nhận trong quy tắc điều phối; các skill khai báo tầng của mình trong frontmatter |
| **File CLAUDE.md theo thư mục** | Thiết lập sẵn `design/CLAUDE.md`, `src/CLAUDE.md`, `docs/CLAUDE.md` — hướng dẫn theo phạm vi đường dẫn cho từng thư mục |
| **Tính toàn vẹn pipeline** | Tính ổn định của TR-ID, quản lý phiên bản manifest, các cổng trạng thái ADR, tham chiếu TR-ID thay vì trích dẫn |
| **Template GDD** | Thêm mục `## Game Feel` (độ phản hồi của input, mục tiêu animation, các khoảnh khắc tác động) |

---

### Các file: An toàn để ghi đè (Safe to Overwrite)

**Các file mới cần thêm:**
```
.claude/skills/ux-design/SKILL.md
.claude/skills/ux-review/SKILL.md
.claude/skills/help/SKILL.md
.claude/skills/quick-design/SKILL.md
.claude/skills/review-all-gdds/SKILL.md
.claude/skills/story-readiness/SKILL.md
.claude/skills/story-done/SKILL.md
.claude/skills/sprint-status/SKILL.md
.claude/skills/adopt/SKILL.md
.claude/skills/create-architecture/SKILL.md
.claude/skills/create-control-manifest/SKILL.md
.claude/skills/create-epics/SKILL.md
.claude/skills/create-stories/SKILL.md
.claude/skills/dev-story/SKILL.md
.claude/skills/propagate-design-change/SKILL.md
.claude/skills/content-audit/SKILL.md
.claude/skills/architecture-review/SKILL.md
.claude/skills/qa-plan/SKILL.md
.claude/skills/smoke-check/SKILL.md
.claude/skills/soak-test/SKILL.md
.claude/skills/regression-suite/SKILL.md
.claude/skills/test-setup/SKILL.md
.claude/skills/test-helpers/SKILL.md
.claude/skills/test-evidence-review/SKILL.md
.claude/skills/test-flakiness/SKILL.md
.claude/skills/skill-test/SKILL.md
.claude/skills/bug-triage/SKILL.md
.claude/skills/team-live-ops/SKILL.md
.claude/skills/team-qa/SKILL.md
.claude/hooks/log-agent-stop.sh
.claude/hooks/notify.sh
.claude/hooks/post-compact.sh
.claude/hooks/validate-skill-change.sh
.claude/docs/workflow-catalog.yaml
.claude/docs/templates/ux-spec.md
.claude/docs/templates/hud-design.md
.claude/docs/templates/accessibility-requirements.md
.claude/docs/templates/interaction-pattern-library.md
.claude/docs/templates/player-journey.md
.claude/docs/templates/difficulty-curve.md
design/CLAUDE.md
src/CLAUDE.md
docs/CLAUDE.md
```

**Các file hiện có ghi đè trực tiếp (không chứa nội dung người dùng):**
```
.claude/skills/gate-check/SKILL.md
.claude/skills/sprint-plan/SKILL.md
.claude/skills/sprint-status/SKILL.md
.claude/skills/design-review/SKILL.md
.claude/skills/team-ui/SKILL.md
.claude/skills/story-readiness/SKILL.md
.claude/skills/story-done/SKILL.md
.claude/docs/templates/game-design-document.md    ← thêm mục Game Feel
README.md
docs/WORKFLOW-GUIDE.md
UPGRADING.md
```

**Các file agent ghi đè** (nếu bạn chưa viết custom prompt vào đó):
```
.claude/agents/prototyper.md         ← thêm isolation: worktree
.claude/agents/art-director.md       ← thêm memory: project
.claude/agents/audio-director.md     ← thêm memory: project
.claude/agents/economy-designer.md   ← thêm memory: project
.claude/agents/game-designer.md      ← thêm memory: project
.claude/agents/gameplay-programmer.md ← thêm memory: project
.claude/agents/lead-programmer.md    ← thêm memory: project
.claude/agents/level-designer.md     ← thêm memory: project
.claude/agents/narrative-director.md ← thêm memory: project
.claude/agents/systems-designer.md   ← thêm memory: project
.claude/agents/technical-artist.md   ← thêm memory: project
.claude/agents/ui-programmer.md      ← thêm memory: project
.claude/agents/ux-designer.md        ← thêm memory: project
.claude/agents/world-builder.md      ← thêm memory: project
```

---

### Các file: Merge cẩn thận (Merge Carefully)

#### `.claude/settings.json`

Bốn hook mới được đăng ký trong phiên bản này. Nếu bạn chưa tùy biến `settings.json`, ghi đè là an toàn. Nếu đã tùy biến, hãy thêm các mục hook sau theo cách thủ công:

- `log-agent-stop.sh` — sự kiện `SubagentStop` (kết thúc audit trail của agent)
- `notify.sh` — sự kiện `Notification` (thông báo Windows toast)
- `post-compact.sh` — sự kiện `PostCompact` (nhắc nhở khôi phục phiên)
- `validate-skill-change.sh` — sự kiện `PostToolUse` được lọc cho các thao tác ghi vào `.claude/skills/`

#### Các file agent đã tùy biến

Nếu bạn đã thêm kiến thức đặc thù của dự án vào các file agent `.md`, hãy thực hiện so sánh diff và thêm thủ công dòng `memory: project` vào YAML frontmatter ở vị trí phù hợp. Các agent creative và technical director giữ nguyên `memory: user` — chỉ các specialist agent mới nhận `memory: project`.

---

### Các tính năng mới

#### Vòng đời Story hoàn chỉnh

Các story hiện có vòng đời chính thức được thực thi bởi hai skill:

- **`/story-readiness`** — xác thực một story đã sẵn sàng để triển khai (implementation-ready) trước khi lập trình viên bắt đầu. Kiểm tra Design (liên kết yêu cầu GDD), Architecture (ADR được chấp thuận), Scope (tiêu chí có thể kiểm thử), và DoD (phiên bản manifest hiện tại). Kết luận: READY / NEEDS WORK / BLOCKED.
- **`/story-done`** — đánh giá hoàn thành 8 giai đoạn sau khi triển khai. Xác minh từng tiêu chí chấp nhận (acceptance criterion), kiểm tra các sai lệch GDD/ADR, nhắc nhở code review, cập nhật file story thành `Status: Complete`, và hiển thị story sẵn sàng tiếp theo.

Luồng: `/story-readiness` → triển khai → `/story-done` → story tiếp theo

#### Toàn bộ Pipeline UX/UI

- **`/ux-design`** — hướng dẫn tạo đặc tả UX từng phần. Ba chế độ: screen/flow, HUD, hoặc thư viện mẫu tương tác. Đọc các yêu cầu UI của GDD và hành trình người chơi. Xuất ra `design/ux/`.
- **`/ux-review`** — xác thực các đặc tả UX đối chiếu với GDD, mức độ accessibility, và thư viện mẫu. Kết luận: APPROVED / NEEDS REVISION / MAJOR REVISION.
- **`/team-ui`** được cập nhật: Phase 1 hiện chạy `/ux-design` + `/ux-review` như một cổng cứng trước khi bắt đầu thiết kế hình ảnh.

#### Tiếp nhận dự án có sẵn (Brownfield Adoption)

**`/adopt`** đưa các dự án hiện có vào định dạng của template. Kiểm tra cấu trúc nội bộ của GDD, ADR, story, systems-index, và hạ tầng. Phân loại khoảng trống (BLOCKING/HIGH/MEDIUM/LOW). Xây dựng kế hoạch migration có thứ tự. Không bao giờ tạo lại các sản phẩm hiện có — chỉ bổ sung các phần còn thiếu.

Các chế độ đối số: `full | gdds | adrs | stories | infra`

Ngoài ra: `/design-system retrofit [path]` và `/architecture-decision retrofit [path]` phát hiện các file hiện có và chỉ thêm các phần còn thiếu.

#### File YAML theo dõi Sprint

`production/sprint-status.yaml` hiện là định dạng theo dõi story chính thức:
- Được ghi bởi `/sprint-plan` (khởi tạo tất cả story) và `/story-done` (đặt trạng thái thành `done`)
- Được đọc bởi `/sprint-status` (ảnh chụp nhanh) và `/help` (trạng thái từng story trong giai đoạn production)
- Các giá trị trạng thái: `backlog | ready-for-dev | in-progress | review | done | blocked`
- Tự động chuyển về quét markdown nếu file chưa tồn tại

#### `/help` — Bước tiếp theo nhận biết ngữ cảnh

`/help` đọc giai đoạn hiện tại và công việc đang tiến hành của bạn, kiểm tra xem sản phẩm nào đã hoàn thành, và cho bạn biết chính xác những việc cần làm tiếp theo — một bước bắt buộc chính, cùng các cơ hội tùy chọn. Khác với `/start` (chỉ dùng lần đầu) và `/project-stage-detect` (audit toàn diện).

#### Framework QA và Kiểm thử toàn diện

Chín skill QA/kiểm thử mới bao quát toàn bộ vòng đời kiểm thử:

- **`/test-setup`** — thiết lập khung kiểm thử và pipeline CI/CD cho engine của bạn
- **`/test-helpers`** — tạo các thư viện helper kiểm thử đặc thù cho engine (GDUnit4, NUnit, v.v.)
- **`/qa-plan`** — tạo kế hoạch kiểm thử QA cho một sprint hoặc tính năng, phân loại story theo loại test
- **`/smoke-check`** — chạy cổng kiểm tra smoke test luồng quan trọng trước khi bàn giao cho QA
- **`/soak-test`** — tạo quy trình kiểm thử ngâm (soak test) cho các phiên chơi kéo dài (tính ổn định, rò rỉ bộ nhớ)
- **`/regression-suite`** — ánh xạ độ bao phủ kiểm thử tới các luồng quan trọng trong GDD, xác định các lỗi đã sửa nhưng thiếu test hồi quy
- **`/test-evidence-review`** — đánh giá chất lượng các file test và tài liệu bằng chứng thủ công
- **`/test-flakiness`** — phát hiện các bài test chập chờn (non-deterministic) bằng cách đọc log chạy CI
- **`/skill-test`** — xác thực các file skill về tính tuân thủ cấu trúc và tính đúng đắn của hành vi (ba chế độ: lint, spec, catalog)

Ngoài ra: **`/bug-triage`** đánh giá lại tất cả các bug đang mở về mức độ ưu tiên, độ nghiêm trọng, và phân công phụ trách.

#### Công cụ xác thực Skill (`/skill-test`)

`/skill-test` là một meta-skill để xác thực chính khung điều phối. Chạy nó sau khi chỉnh sửa bất kỳ file skill nào. Ba chế độ:
- `lint` — xác thực YAML frontmatter và các trường bắt buộc
- `spec [skill-name]` — chạy các bài kiểm tra hành vi (behavioral spec tests) đối với một skill cụ thể
- `catalog` — kiểm tra tất cả các skill trong `.claude/skills/` đã được đánh chỉ mục trong catalog chưa

Hook `validate-skill-change.sh` mới sẽ tự động nhắc bạn chạy `/skill-test` khi một file skill bị sửa đổi.

#### Phối hợp Team Live-Ops và Team QA

- **`/team-live-ops`** — điều phối live-ops-designer + economy-designer + community-manager + analytics-engineer cho kế hoạch nội dung sau phát hành (sự kiện theo mùa, battle pass, giữ chân người chơi)
- **`/team-qa`** — điều phối qa-lead + qa-tester + gameplay-programmer + producer qua một chu kỳ QA đầy đủ: chiến lược, thực thi, độ bao phủ, và ký duyệt

#### Định tuyến phân tầng Model

Các skill hiện được gán rõ ràng vào các tầng Haiku, Sonnet, hoặc Opus dựa trên độ phức tạp của nhiệm vụ. Kiểm tra trạng thái chỉ đọc sử dụng Haiku; tổng hợp đa tài liệu phức tạp sử dụng Opus; tất cả các tác vụ còn lại mặc định là Sonnet. Việc phân tầng được ghi nhận trong `.claude/docs/coordination-rules.md`.

#### Các file CLAUDE.md theo thư mục

Ba file CLAUDE.md theo phạm vi thư mục mới (`design/`, `src/`, `docs/`) cung cấp hướng dẫn cụ thể theo đường dẫn cho các agent làm việc trong các thư mục đó. Chúng sẽ tự động được tải khi Claude Code đọc các file trong thư mục tương ứng.

---

### Sau khi nâng cấp

1. **Xác minh các hook mới** đã được đăng ký trong `.claude/settings.json` — kiểm tra đủ cả bốn: `log-agent-stop.sh`, `notify.sh`, `post-compact.sh`, `validate-skill-change.sh`.

2. **Kiểm thử audit trail** bằng cách gọi bất kỳ subagent nào — cả sự kiện bắt đầu và kết thúc đều sẽ xuất hiện trong `production/session-logs/`.

3. **Tạo sprint-status.yaml** nếu bạn đang trong giai đoạn production tích cực:
   ```
   /sprint-plan status
   ```

4. **Chạy `/adopt`** nếu bạn có các GDD hoặc ADR cũ tạo trước phiên bản template này — nó sẽ xác định những mục nào cần bổ sung mà không ghi đè nội dung của bạn.

5. **Xác thực các skill của bạn** sau bất kỳ chỉnh sửa skill nào bằng `/skill-test` — hook `validate-skill-change.sh` mới sẽ tự động nhắc bạn làm việc này.

---

## v0.2.0 → v0.3.0

**Ngày phát hành:** 2026-03-09  
**Phạm vi commit:** `e289ce9..HEAD`  
**Chủ đề chính:** Tạo GDD với `/design-system`, đổi tên `/map-systems`, tùy biến dòng trạng thái (status line)

### Thay đổi gây phá vỡ (Breaking Changes)

#### Đổi tên `/design-systems` thành `/map-systems`

Skill `/design-systems` được đổi tên thành `/map-systems` để rõ ràng hơn (phân rã = *mapping - lập sơ đồ*, không phải *designing - thiết kế*).

**Hành động bắt buộc:** Cập nhật bất kỳ tài liệu, ghi chú hoặc script nào có gọi `/design-systems`. Lệnh gọi mới là `/map-systems`.

### Những thay đổi

| Danh mục | Thay đổi |
|---|---|
| **Skill mới** | `/design-system` (hướng dẫn tạo GDD từng phần) |
| **Đổi tên skill** | `/design-systems` → `/map-systems` (đổi tên gây phá vỡ) |
| **File mới** | `.claude/statusline.sh`, cấu hình statusline trong `.claude/settings.json` |
| **Cập nhật skill** | `/gate-check` — ghi `production/stage.txt` khi PASS, các định nghĩa giai đoạn mới |
| **Cập nhật skill** | `brainstorm`, `start`, `design-review`, `project-stage-detect`, `setup-engine` — sửa tham chiếu chéo |
| **Sửa lỗi** | `log-agent.sh`, `validate-commit.sh` — sửa việc thực thi hook |
| **Tài liệu** | Thêm `UPGRADING.md`, cập nhật `README.md`, cập nhật `WORKFLOW-GUIDE.md` |

---

### Các file: An toàn để ghi đè (Safe to Overwrite)

**Các file mới cần thêm:**
```
.claude/skills/design-system/SKILL.md
.claude/statusline.sh
```

**Các file hiện có ghi đè trực tiếp (không chứa nội dung người dùng):**
```
.claude/skills/map-systems/SKILL.md      ← trước đây là design-systems/SKILL.md
.claude/skills/gate-check/SKILL.md
.claude/skills/brainstorm/SKILL.md
.claude/skills/start/SKILL.md
.claude/skills/design-review/SKILL.md
.claude/skills/project-stage-detect/SKILL.md
.claude/skills/setup-engine/SKILL.md
.claude/hooks/log-agent.sh
.claude/hooks/validate-commit.sh
README.md
docs/WORKFLOW-GUIDE.md
UPGRADING.md
```

**Xóa (được thay thế bởi việc đổi tên):**
```
.claude/skills/design-systems/   ← toàn bộ thư mục; được thay thế bởi map-systems/
```

---

### Các file: Merge cẩn thận (Merge Carefully)

#### `.claude/settings.json`

Phiên bản mới bổ sung khối cấu hình `statusLine` trỏ tới `.claude/statusline.sh`. Nếu bạn chưa tùy biến `settings.json`, ghi đè là an toàn. Nếu đã tùy biến, hãy thêm khối này thủ công:

```json
"statusLine": {
  "script": ".claude/statusline.sh"
}
```

---

### Các tính năng mới

#### Tùy biến Status Line

`.claude/statusline.sh` hiển thị breadcrumb pipeline sản xuất 7 giai đoạn trong dòng trạng thái terminal:

```
ctx: 42% | claude-sonnet-4-6 | Systems Design
```

Trong các giai đoạn Production/Polish/Release, nó cũng hiển thị Epic/Feature/Task đang hoạt động từ `production/session-state/active.md` nếu có khối `<!-- STATUS -->`:

```
ctx: 42% | claude-sonnet-4-6 | Production | Combat System > Melee Combat > Hitboxes
```

Giai đoạn hiện tại được tự động phát hiện từ các tài liệu sản phẩm của dự án, hoặc có thể được ghim cố định bằng cách ghi tên giai đoạn vào `production/stage.txt`.

#### Chuyển giai đoạn trong `/gate-check`

Khi kết luận cổng PASS được xác nhận, `/gate-check` hiện sẽ ghi tên giai đoạn mới vào `production/stage.txt`. Việc này cập nhật ngay lập tức status line cho tất cả các phiên làm việc sau này mà không yêu cầu sửa file thủ công.

---

### Sau khi nâng cấp

1. **Xóa thư mục skill cũ:**
   ```bash
   rm -rf .claude/skills/design-systems/
   ```

2. **Kiểm tra status line** bằng cách khởi động phiên Claude Code — bạn sẽ thấy breadcrumb giai đoạn ở footer terminal.

3. **Xác minh việc thực thi hook** vẫn hoạt động:
   ```bash
   bash .claude/hooks/log-agent.sh '{}' '{}'
   bash .claude/hooks/validate-commit.sh '{}' '{}'
   ```

---

## v0.1.0 → v0.2.0

**Ngày phát hành:** 2026-02-21  
**Phạm vi commit:** `ad540fe..e289ce9`  
**Chủ đề chính:** Khả năng phục hồi ngữ cảnh (Context Resilience), tích hợp AskUserQuestion, skill `/map-systems`

### Những thay đổi

| Danh mục | Thay đổi |
|---|---|
| **Skill mới** | `/start` (onboarding), `/map-systems` (phân rã hệ thống), `/design-system` (hướng dẫn tạo GDD) |
| **Hook mới** | `session-start.sh` (phục hồi), `detect-gaps.sh` (phát hiện khoảng trống) |
| **Template mới** | `systems-index.md`, 3 template collaborative-protocol |
| **Quản lý ngữ cảnh** | Viết lại lớn — bổ sung chiến lược lưu trạng thái dựa trên file (file-backed state) |
| **Cập nhật agent** | 14 design/creative agents — tích hợp AskUserQuestion |
| **Cập nhật skill** | Tất cả 7 skill `team-*` + `brainstorm` — AskUserQuestion tại các điểm chuyển giao giai đoạn |
| **CLAUDE.md** | Tinh gọn từ ~159 xuống ~60 dòng; 5 mục import tài liệu thay vì 10 |
| **Cập nhật hook** | Tất cả 8 hooks — sửa lỗi tương thích Windows, thêm tính năng mới |
| **Tài liệu bị gỡ bỏ** | `docs/IMPROVEMENTS-PROPOSAL.md`, `docs/MULTI-STAGE-DOCUMENT-WORKFLOW.md` |

---

### Các file: An toàn để ghi đè (Safe to Overwrite)

Đây là các file hạ tầng thuần túy — bạn chưa tùy biến chúng. Sao chép trực tiếp các phiên bản mới mà không ảnh hưởng đến nội dung dự án của bạn.

**Các file mới cần thêm:**
```
.claude/skills/start/SKILL.md
.claude/skills/map-systems/SKILL.md
.claude/skills/design-system/SKILL.md
.claude/docs/templates/systems-index.md
.claude/docs/templates/collaborative-protocols/design-agent-protocol.md
.claude/docs/templates/collaborative-protocols/implementation-agent-protocol.md
.claude/docs/templates/collaborative-protocols/leadership-agent-protocol.md
.claude/hooks/detect-gaps.sh
.claude/hooks/session-start.sh
production/session-state/.gitkeep
docs/examples/README.md
.github/ISSUE_TEMPLATE/bug_report.md
.github/ISSUE_TEMPLATE/feature_request.md
.github/PULL_REQUEST_TEMPLATE.md
```

**Các file hiện có ghi đè trực tiếp (không chứa nội dung người dùng):**
```
.claude/skills/brainstorm/SKILL.md
.claude/skills/design-review/SKILL.md
.claude/skills/gate-check/SKILL.md
.claude/skills/project-stage-detect/SKILL.md
.claude/skills/setup-engine/SKILL.md
.claude/skills/team-audio/SKILL.md
.claude/skills/team-combat/SKILL.md
.claude/skills/team-level/SKILL.md
.claude/skills/team-narrative/SKILL.md
.claude/skills/team-polish/SKILL.md
.claude/skills/team-release/SKILL.md
.claude/skills/team-ui/SKILL.md
.claude/hooks/log-agent.sh
.claude/hooks/pre-compact.sh
.claude/hooks/session-stop.sh
.claude/hooks/validate-assets.sh
.claude/hooks/validate-commit.sh
.claude/hooks/validate-push.sh
.claude/rules/design-docs.md
.claude/docs/hooks-reference.md
.claude/docs/skills-reference.md
.claude/docs/quick-start.md
.claude/docs/directory-structure.md
.claude/docs/context-management.md
docs/COLLABORATIVE-DESIGN-PRINCIPLE.md
docs/WORKFLOW-GUIDE.md
README.md
```

**Các file agent ghi đè** (nếu bạn chưa viết custom prompt vào đó):
```
.claude/agents/art-director.md
.claude/agents/audio-director.md
.claude/agents/creative-director.md
.claude/agents/economy-designer.md
.claude/agents/game-designer.md
.claude/agents/level-designer.md
.claude/agents/live-ops-designer.md
.claude/agents/narrative-director.md
.claude/agents/producer.md
.claude/agents/systems-designer.md
.claude/agents/technical-director.md
.claude/agents/ux-designer.md
.claude/agents/world-builder.md
.claude/agents/writer.md
```

Nếu bạn *đã* tùy biến prompt của agent, hãy xem mục "Merge cẩn thận" bên dưới.

---

### Các file: Merge cẩn thận (Merge Carefully)

Các file này chứa cả cấu trúc template và nội dung riêng của dự án bạn. **Không** ghi đè lên chúng — hãy merge các thay đổi thủ công.

#### `CLAUDE.md`

Phiên bản template được tinh gọn từ ~159 dòng xuống ~60 dòng. Thay đổi cấu trúc chính: 5 mục import tài liệu đã được gỡ bỏ vì chúng tự động được tải bởi Claude Code (agent-roster, skills-reference, hooks-reference, rules-reference, review-workflow).

**Những gì cần giữ lại từ phiên bản của bạn:**
- Phần `## Technology Stack` (lựa chọn engine/ngôn ngữ của bạn)
- Bất kỳ bổ sung đặc thù nào cho dự án mà bạn đã thực hiện

**Những gì cần áp dụng từ phiên bản mới:**
- Danh sách imports tinh gọn hơn (loại bỏ 5 mục import `@` dư thừa nếu có)
- Diễn đạt giao thức cộng tác đã cập nhật

#### `.claude/docs/technical-preferences.md`

Nếu bạn đã chạy `/setup-engine`, file này chứa cấu hình engine, quy ước đặt tên và giới hạn hiệu năng của bạn. Hãy giữ lại toàn bộ. Phiên bản template chỉ là khung mẫu trống.

#### `.claude/docs/templates/game-concept.md`

Cập nhật cấu trúc nhỏ — một mục `## Next Steps` được thêm vào trỏ tới `/map-systems`. Hãy thêm mục đó vào bản sao của bạn nếu muốn có hướng dẫn mới, nhưng không bắt buộc.

#### `.claude/settings.json`

Kiểm tra xem phiên bản mới có bổ sung quy tắc phân quyền nào bạn muốn không. Thay đổi này là nhỏ (cập nhật schema). Nếu bạn chưa tùy biến `settings.json`, ghi đè là an toàn.

#### Các file agent đã tùy biến

Nếu bạn đã thêm kiến thức đặc thù của dự án hoặc hành vi tùy chỉnh vào bất kỳ file `.md` nào của agent, hãy thực hiện so sánh diff và thêm thủ công các phần tích hợp AskUserQuestion mới thay vì ghi đè. Thay đổi trong mỗi agent là một khối giao thức cộng tác chuẩn hóa ở cuối system prompt.

---

### Các file: Xóa (Delete)

Các file này đã được loại bỏ trong v0.2.0. Nếu có trong repo của bạn, bạn có thể xóa chúng một cách an toàn — chúng đã được thay thế bằng các giải pháp tổ chức tốt hơn.

```
docs/IMPROVEMENTS-PROPOSAL.md      → được thay thế bởi WORKFLOW-GUIDE.md
docs/MULTI-STAGE-DOCUMENT-WORKFLOW.md → nội dung đã được merge vào context-management.md
```

---

### Sau khi nâng cấp

1. **Chạy `/project-stage-detect`** để xác minh hệ thống đọc dự án của bạn chính xác với logic phát hiện mới.

2. **Chạy `/start`** một lần nếu bạn chưa từng dùng — hiện tại nó sẽ nhận diện đúng giai đoạn của bạn và bỏ qua các bước onboarding bạn đã hoàn thành.

3. **Kiểm tra `production/session-state/`** tồn tại và đã được gitignore:
   ```bash
   ls production/session-state/
   cat .gitignore | grep session-state
   ```

4. **Kiểm thử việc thực thi hook** — nếu bạn dùng Windows, xác minh các hook mới chạy không có lỗi trong Git Bash:
   ```bash
   bash .claude/hooks/detect-gaps.sh '{}' '{}'
   bash .claude/hooks/session-start.sh '{}' '{}'
   ```

---

*Mỗi phiên bản tương lai sẽ có phần riêng trong tài liệu này.*
