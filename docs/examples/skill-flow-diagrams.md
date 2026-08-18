# Sơ đồ luồng Skill (Skill Flow Diagrams)

Bản đồ trực quan về cách các skill liên kết với nhau xuyên suốt 7 giai đoạn phát triển.
Các sơ đồ này cho thấy những gì chạy trước và sau mỗi skill, cùng các sản phẩm tài liệu luân chuyển giữa chúng.

---

## Toàn cảnh Pipeline hoàn chỉnh (Từ số 0 đến Phát hành)

```
GIAI ĐOẠN 1: Ý TƯỞNG (PHASE 1: CONCEPT)
  /start ──────────────────────────────────────────────────────► điều hướng tới A/B/C/D
  /brainstorm ──────────────────────────────────────────────────► design/gdd/game-concept.md
  /setup-engine ────────────────────────────────────────────────► GEMINI.md + technical-preferences.md
  /prototype [core-mechanic] ───────────────────────────────────► prototypes/[name]-concept/REPORT.md
        │ PROCEED                                                  (kiểm chứng ý tưởng TRƯỚC KHI viết GDD)
        ▼
  /design-review [game-concept.md] ────────────────────────────► concept đã được xác thực
  /gate-check ─────────────────────────────────────────────────► PASS → tiến sang systems-design
        │
        ▼
GIAI ĐOẠN 2: THIẾT KẾ HỆ THỐNG (PHASE 2: SYSTEMS DESIGN)
  /map-systems ────────────────────────────────────────────────► design/gdd/systems-index.md
        │
        ▼ (cho từng hệ thống, theo thứ tự phụ thuộc)
  /design-system [name] ──────────────────────────────────────► design/gdd/[system].md
  /design-review [system].md ─────────────────────────────────► nhận xét đánh giá từng GDD
        │
        ▼ (sau khi hoàn thành tất cả GDD MVP)
  /review-all-gdds ────────────────────────────────────────────► design/gdd/gdd-cross-review-[date].md
  /gate-check ─────────────────────────────────────────────────► PASS → tiến sang technical-setup
        │
        ▼
GIAI ĐOẠN 3: THIẾT LẬP KỸ THUẬT (PHASE 3: TECHNICAL SETUP)
  /create-architecture ────────────────────────────────────────► docs/architecture/master.md
  /architecture-decision (×N) ─────────────────────────────────► docs/architecture/[adr-nnn].md
  /architecture-review ────────────────────────────────────────► báo cáo review + docs/architecture/tr-registry.yaml
  /create-control-manifest ────────────────────────────────────► docs/architecture/control-manifest.md
  /gate-check ─────────────────────────────────────────────────► PASS → tiến sang pre-production
        │
        ▼
GIAI ĐOẠN 4: TIỀN SẢN XUẤT (PHASE 4: PRE-PRODUCTION)
  [UX — trước epic, để đặc tả sẵn sàng khi viết story]
  /ux-design [screen/hud/patterns] ────────────────────────────► design/ux/*.md
  /ux-review ──────────────────────────────────────────────────► đặc tả UX đã duyệt (CỔNG CỨNG cho /team-ui)

  [Hạ tầng kiểm thử — dựng khung trước khi story tham chiếu test]
  /test-setup ─────────────────────────────────────────────────► test framework + pipeline CI/CD
  /test-helpers ───────────────────────────────────────────────► tests/helpers/[engine-specific].gd

  [Vertical slice — trước epic, xác thực toàn bộ game loop]
  /vertical-slice ─────────────────────────────────────────────► prototypes/[name]-vertical-slice/REPORT.md
  /playtest-report ────────────────────────────────────────────► production/playtests/

  [Stories + kế hoạch sprint — chỉ sau khi vertical slice PROCEED]
  /create-epics [layer] ───────────────────────────────────────► production/epics/*/EPIC.md
  /create-stories [epic-slug] ─────────────────────────────────► production/epics/*/story-*.md
  /sprint-plan new ────────────────────────────────────────────► production/sprints/sprint-01.md
  /gate-check ─────────────────────────────────────────────────► PASS → tiến sang production
        │
        ▼
GIAI ĐOẠN 5: SẢN XUẤT (PHASE 5: PRODUCTION - vòng lặp sprint)
  /sprint-status ──────────────────────────────────────────────► ảnh chụp nhanh sprint
  /story-readiness [story] ────────────────────────────────────► story xác thực READY
        │
        ▼ (nhận story và triển khai code)
  /dev-story [story] ──────────────────────────────────────────► điều phối tới đúng agent lập trình
        │
        ▼ (trong khi code, khi cần)
  /code-review ────────────────────────────────────────────────► báo cáo code review
  /scope-check ────────────────────────────────────────────────► phát hiện phình to quy mô / an toàn
  /content-audit ──────────────────────────────────────────────► xác định khoảng trống nội dung GDD
  /bug-report ─────────────────────────────────────────────────► production/qa/bugs/bug-NNN.md
  /bug-triage ─────────────────────────────────────────────────► sắp xếp lại ưu tiên + phân công bug

  [Team skills cho từng mảng tính năng — gọi khi làm tính năng đầy đủ]
  /team-combat / /team-narrative / /team-ui / /team-level / /team-audio

  [Chu kỳ QA theo từng sprint]
  /qa-plan ────────────────────────────────────────────────────► production/qa/qa-plan-sprint-NN.md
  /smoke-check ────────────────────────────────────────────────► cổng smoke test (PASS/FAIL)
  /regression-suite ───────────────────────────────────────────► khoảng trống bao phủ + thiếu test hồi quy
  /test-evidence-review ───────────────────────────────────────► báo cáo chất lượng bằng chứng test
  /test-flakiness ─────────────────────────────────────────────► báo cáo test chập chờn
        │
        ▼
  /story-done [story] ─────────────────────────────────────────► đóng story + hiển thị story tiếp theo
  /sprint-plan [next] ─────────────────────────────────────────► sprint tiếp theo
        │
        ▼ (sau milestone Sản xuất)
  /milestone-review ───────────────────────────────────────────► báo cáo milestone
  /gate-check ─────────────────────────────────────────────────► PASS → tiến sang polish
        │
        ▼
GIAI ĐOẠN 6: ĐÁNH BÓNG (PHASE 6: POLISH)
  /perf-profile ───────────────────────────────────────────────► báo cáo hiệu năng + bản sửa lỗi
  /balance-check ──────────────────────────────────────────────► báo cáo cân bằng + bản sửa lỗi
  /asset-audit ────────────────────────────────────────────────► báo cáo tuân thủ asset
  /tech-debt ──────────────────────────────────────────────────► docs/tech-debt-register.md
  /soak-test ──────────────────────────────────────────────────► quy trình soak test + kết quả
  /localize ───────────────────────────────────────────────────► báo cáo sẵn sàng đa ngôn ngữ
  /team-polish ────────────────────────────────────────────────► điều phối sprint đánh bóng
  /team-qa ────────────────────────────────────────────────────► ký duyệt chu kỳ QA đầy đủ
  /gate-check ─────────────────────────────────────────────────► PASS → tiến sang release
        │
        ▼
GIAI ĐOẠN 7: PHÁT HÀNH (PHASE 7: RELEASE)
  /launch-checklist ───────────────────────────────────────────► báo cáo sẵn sàng ra mắt
  /release-checklist ──────────────────────────────────────────► checklist đặc thù theo nền tảng
  /changelog ──────────────────────────────────────────────────► CHANGELOG.md
  /patch-notes ────────────────────────────────────────────────► patch notes cho người chơi
  /team-release ───────────────────────────────────────────────► điều phối pipeline phát hành
        │
        ▼ (sau phát hành, liên tục)
  /hotfix ─────────────────────────────────────────────────────► sửa lỗi khẩn cấp kèm audit trail
  /team-live-ops ──────────────────────────────────────────────► kế hoạch nội dung live-ops
```

---

## Chuỗi Skill: Chi tiết /design-system

Cách một GDD đơn lẻ được soạn thảo, đánh giá và chuyển giao sang kiến trúc:

```
systems-index.md (đầu vào)
game-concept.md (đầu vào)
các GDD thượng nguồn (đầu vào, nếu có)
        │
        ▼
/design-system [name]
        │
        ├── Kiểm tra sơ bộ: bảng khả thi + cảnh báo rủi ro engine
        │
        ├── Chu kỳ từng phần × 8:
        │     hỏi → lựa chọn → quyết định → bản thảo → phê duyệt → GHI FILE
        │     [mỗi phần ghi vào file ngay sau khi được duyệt]
        │
        └── Đầu ra: design/gdd/[system].md (hoàn chỉnh, đủ 8 phần)
                │
                ▼
        /design-review design/gdd/[system].md
                │
                ├── APPROVED → đánh dấu DONE trong systems-index, làm hệ thống tiếp theo
                ├── NEEDS REVISION → agent chỉ ra vấn đề cụ thể, quay lại chu kỳ từng phần
                └── MAJOR REVISION → cần thiết kế lại đáng kể trước khi làm hệ thống tiếp theo
                        │
                        ▼ (sau khi xong tất cả GDD MVP + đánh giá chéo)
                /review-all-gdds
                        │
                        └── Đầu ra: gdd-cross-review-[date].md
```

---

## Chuỗi Skill: Chi tiết Pipeline UX / UI

Các đặc tả UX được soạn thảo trong Giai đoạn 4 (Tiền sản xuất), trước khi viết epic, để các tiêu chí chấp nhận của story có thể tham chiếu các sản phẩm UX cụ thể.

```
design/gdd/*.md (các yêu cầu UI/UX được trích xuất)
design/player-journey.md (hành trình cảm xúc, nếu có)
        │
        ▼
/ux-design hud              → design/ux/hud.md
/ux-design screen [name]    → design/ux/screens/[name].md
/ux-design patterns         → design/ux/interaction-patterns.md
        │
        ▼
/ux-review design/ux/
        │
        ├── APPROVED → đặc tả UX sẵn sàng, tiến hành /create-epics
        ├── NEEDS REVISION → liệt kê vấn đề nghẽn → sửa → chạy lại review
        └── MAJOR REVISION → lỗi UX căn bản → thiết kế lại trước khi viết epic
                │
                ▼ (sau khi APPROVED — trong Giai đoạn 5 khi triển khai tính năng UI)
        /team-ui
                │
                ├── Phase 1: /ux-design (nếu còn thiếu đặc tả) + /ux-review
                ├── Phase 2: thiết kế đồ họa thị giác (art-director)
                ├── Phase 3: triển khai layout (ui-programmer)
                ├── Phase 4: kiểm toán accessibility (accessibility-specialist)
                └── Phase 5: đánh giá cuối cùng
```

---

## Chuỗi Skill: Chi tiết Luồng Dev Story

Cách một story di chuyển từ backlog đến khi đóng:

```
/story-readiness [story]
        │
        ├── READY → Status: ready-for-dev → nhận để triển khai code
        ├── NEEDS WORK → agent chỉ ra khoảng trống cụ thể → khắc phục → chạy lại readiness
        └── BLOCKED → ADR vẫn đang Proposed, hoặc story thượng nguồn chưa xong
                │
                ▼ (sau khi READY)
        /dev-story [story]
                │
                ├── Đọc: file story, yêu cầu GDD liên kết, quyết định ADR, control manifest
                ├── Điều phối tới: gameplay-programmer / engine-programmer / ui-programmer / v.v.
                │
                └── Bắt đầu triển khai code
                        │
                        ▼ (tùy chọn, trong/sau khi code)
                /code-review          → đánh giá kiến trúc của tập thay đổi
                /scope-check          → xác minh không phình to quy mô so với tiêu chí ban đầu
                /test-evidence-review → xác thực chất lượng file test và bằng chứng thủ công
                        │
                        ▼
                /story-done [story]
                        │
                        ├── COMPLETE → Status: Complete, sprint-status.yaml cập nhật, hiển thị story tiếp theo
                        ├── COMPLETE WITH NOTES → hoàn thành nhưng một số tiêu chí bị hoãn (đã ghi log)
                        └── BLOCKED → không thể xác minh tiêu chí chấp nhận → điều tra điểm nghẽn
```

---

## Luồng tiếp nhận dự án cũ (Brownfield Onboarding Flow)

Dành cho các dự án đã có sẵn code/tài liệu:

```
/project-stage-detect    → báo cáo phát hiện giai đoạn
        │
        ▼
/adopt
        │
        ├── Phase 1: phát hiện những gì đang tồn tại
        ├── Phase 2: audit ĐỊNH DẠNG (không chỉ sự tồn tại)
        ├── Phase 3: phân loại khoảng trống (BLOCKING / HIGH / MEDIUM / LOW)
        ├── Phase 4: lập kế hoạch migration có thứ tự
        ├── Phase 5: ghi docs/adoption-plan-[date].md
        └── Phase 6: sửa khoảng trống khẩn cấp nhất ngay tại chỗ (tùy chọn)
                │
                ▼
        /design-system retrofit [path]    → điền các phần GDD còn thiếu
        /architecture-decision retrofit [path] → điền các phần ADR còn thiếu
        /gate-check                       → xác định bạn đang ở đâu trong pipeline
```

---

## Cách đọc các sơ đồ này

| Ký hiệu | Ý nghĩa |
|---|---|
| `──►` | Tạo ra sản phẩm tài liệu này |
| `│ ▼` | Chuyển tiếp sang bước tiếp theo |
| `├──` | Phân nhánh (nhiều kết quả khả dĩ) |
| `×N` | Chạy N lần (mỗi hệ thống, story một lần) |
| `(đầu vào)` | Được đọc bởi skill nhưng không tạo ra ở đây |
| `[tùy chọn]` | Không bắt buộc để vượt qua cổng |
| `GHI FILE` | File được ghi xuống đĩa ngay lập tức |

---

## Các điểm bắt đầu phổ biến

| Tình trạng hiện tại | Hãy chạy lệnh này |
|---|---|
| Dự án mới tinh, chưa có ý tưởng | `/start` → `/brainstorm` |
| Đã có concept, chưa có engine | `/setup-engine` |
| Đã có concept + engine | `/map-systems` |
| Đang thiết kế hệ thống | `/design-system [tên hệ thống]` hoặc `/map-systems next` |
| Đã xong tất cả GDD | `/review-all-gdds` → `/gate-check` |
| Đang thiết lập kỹ thuật | `/create-architecture` → `/architecture-decision` |
| Bắt đầu thiết kế UX | `/ux-design screen [tên]` hoặc `/ux-design hud` |
| Dựng khung kiểm thử | `/test-setup` → `/test-helpers` |
| Đã có story, sẵn sàng code | `/story-readiness [story]` → `/dev-story [story]` |
| Đã code xong story | `/story-done [story]` |
| Chạy QA cho một sprint | `/qa-plan` → `/smoke-check` → `/regression-suite` |
| Backlog bug cần sắp xếp | `/bug-triage` |
| Kiểm thử độ ổn định kéo dài | `/soak-test` |
| Chưa rõ phải làm gì | `/help` |
| Dự án có sẵn từ trước | `/adopt` |
