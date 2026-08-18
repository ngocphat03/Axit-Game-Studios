# Kiến trúc Game Studio Agent -- Hướng dẫn Khởi động Nhanh (Quick Start Guide)

## Đây là gì?

Đây là một kiến trúc agent toàn diện của Antigravity dành cho phát triển game. Nó tổ chức 49 AI agent chuyên biệt thành một hệ thống phân cấp studio mô phỏng các đội ngũ làm game thực tế, với trách nhiệm được xác định rõ ràng, quy tắc ủy quyền và các giao thức điều phối. Hệ thống bao gồm các agent chuyên trách engine cho Godot, Unity, và Unreal — mỗi engine đều có các chuyên viên phụ trách các hệ thống con quan trọng. Tất cả các agent thiết kế và template đều được xây dựng dựa trên lý thuyết thiết kế game vững chắc (MDA Framework, Self-Determination Theory, Flow State, Bartle Player Types). Hãy sử dụng bộ engine phù hợp với dự án của bạn.

## Cách sử dụng

### 1. Hiểu rõ hệ thống phân cấp (Hierarchy)

Hệ thống gồm 3 phân tầng agent:

- **Phân tầng 1 (Tier 1 - Opus)**: Các giám đốc đưa ra quyết định cấp cao
  - `creative-director` -- tầm nhìn sáng tạo và giải quyết xung đột ý tưởng
  - `technical-director` -- kiến trúc và quyết định công nghệ
  - `producer` -- lịch trình, điều phối và quản lý rủi ro

- **Phân tầng 2 (Tier 2 - Sonnet)**: Các trưởng bộ phận nắm quyền sở hữu lĩnh vực của mình
  - `game-designer`, `lead-programmer`, `art-director`, `audio-director`, `narrative-director`, `qa-lead`, `release-manager`, `localization-lead`

- **Phân tầng 3 (Tier 3 - Sonnet/Haiku)**: Các chuyên viên thực thi nhiệm vụ trong phạm vi của mình
  - Nhà thiết kế, lập trình viên, họa sĩ, nhà văn, tester, kỹ sư

### 2. Chọn đúng Agent cho công việc

Hãy tự hỏi: "Bộ phận nào sẽ xử lý việc này trong một studio thực tế?"

| Tôi cần... | Hãy sử dụng agent này |
|---|---|
| Thiết kế một cơ chế mới | `game-designer` |
| Viết code chiến đấu (combat) | `gameplay-programmer` |
| Tạo shader | `technical-artist` |
| Viết lời thoại | `writer` |
| Lên kế hoạch sprint tiếp theo | `producer` |
| Đánh giá chất lượng code | `lead-programmer` |
| Viết test cases | `qa-tester` |
| Thiết kế màn chơi | `level-designer` |
| Khắc phục sự cố hiệu năng | `performance-analyst` |
| Thiết lập CI/CD | `devops-engineer` |
| Thiết kế bảng loot đồ | `economy-designer` |
| Giải quyết xung đột sáng tạo | `creative-director` |
| Đưa ra quyết định kiến trúc | `technical-director` |
| Quản lý bản phát hành | `release-manager` |
| Chuẩn bị chuỗi ngôn ngữ để dịch | `localization-lead` |
| Thử nghiệm nhanh một ý tưởng cơ chế | `prototyper` |
| Đánh giá code về mặt bảo mật | `security-engineer` |
| Kiểm tra tuân thủ khả năng tiếp cận | `accessibility-specialist` |
| Xin tư vấn Unreal Engine | `unreal-specialist` |
| Xin tư vấn Unity | `unity-specialist` |
| Xin tư vấn Godot | `godot-specialist` |
| Thiết kế kỹ năng/hiệu ứng GAS trong Unreal | `ue-gas-specialist` |
| Xác định ranh giới Blueprint/C++ | `ue-blueprint-specialist` |
| Triển khai replication Unreal | `ue-replication-specialist` |
| Xây dựng widget UMG/CommonUI | `ue-umg-specialist` |
| Thiết kế kiến trúc DOTS/ECS trong Unity | `unity-dots-specialist` |
| Viết shader/VFX Unity | `unity-shader-specialist` |
| Quản lý tài nguyên Addressables | `unity-addressables-specialist` |
| Xây dựng màn hình UI Toolkit/UGUI | `unity-ui-specialist` |
| Viết code GDScript chuẩn quy ước | `godot-gdscript-specialist` |
| Viết code C# trong Godot | `godot-csharp-specialist` |
| Tạo shader trong Godot | `godot-shader-specialist` |
| Xây dựng module GDExtension | `godot-gdextension-specialist` |
| Lên kế hoạch sự kiện và mùa giải live-ops | `live-ops-designer` |
| Viết patch notes cho người chơi | `community-manager` |
| Brainstorm ý tưởng game mới | Dùng skill `/brainstorm` |

### 3. Sử dụng các lệnh Slash Command cho công việc phổ biến

| Lệnh | Chức năng |
|---|---|
| `/start` | Khởi động lần đầu — hỏi bạn đang ở đâu, hướng dẫn luồng công việc phù hợp |
| `/help` | Nhận biết ngữ cảnh "tôi cần làm gì tiếp theo?" — đọc giai đoạn và sản phẩm hiện tại |
| `/project-stage-detect` | Phân tích trạng thái dự án, phát hiện giai đoạn, tìm khoảng trống tài liệu |
| `/setup-engine` | Cấu hình engine + phiên bản, điền tài liệu tham chiếu |
| `/adopt` | Audit và lập kế hoạch migration cho dự án cũ có sẵn |
| `/brainstorm` | Lên ý tưởng concept game có hướng dẫn từ số 0 |
| `/map-systems` | Phân rã concept thành các hệ thống, lập bản đồ phụ thuộc, hướng dẫn làm GDD |
| `/design-system` | Soạn thảo GDD từng phần có hướng dẫn cho một hệ thống game đơn lẻ |
| `/quick-design` | Bản đặc tả tinh gọn cho các thay đổi nhỏ — cân bằng, sửa đổi nhỏ |
| `/review-all-gdds` | Đánh giá tính nhất quán chéo và lý thuyết thiết kế game trên tất cả GDD |
| `/propagate-design-change` | Tìm các ADR và story bị ảnh hưởng bởi một thay đổi trong GDD |
| `/art-bible` | Soạn thảo Art Bible từng phần có hướng dẫn — tạo đặc tả nhận diện hình ảnh |
| `/asset-spec` | Tạo đặc tả hình ảnh từng asset và prompt sinh AI từ GDD hoặc hồ sơ nhân vật |
| `/ux-design` | Soạn thảo đặc tả UX (màn hình/luồng, HUD, mẫu tương tác) |
| `/ux-review` | Xác thực các đặc tả UX về khả năng tiếp cận và độ khớp GDD |
| `/create-architecture` | Tạo tài liệu kiến trúc tổng thể cho game |
| `/architecture-decision` | Tạo một bản ghi ADR |
| `/architecture-review` | Xác thực tất cả ADR, thứ tự phụ thuộc, truy vết GDD |
| `/create-control-manifest` | Tạo bảng quy tắc lập trình phẳng từ các ADR đã Accepted |
| `/create-epics` | Chuyển đổi GDD + ADR thành các epic (mỗi module một epic) |
| `/create-stories` | Phân rã một epic thành các file story có thể code |
| `/dev-story` | Đọc một story và code — điều phối tới đúng agent lập trình |
| `/sprint-plan` | Tạo hoặc cập nhật kế hoạch sprint |
| `/sprint-status` | Ảnh chụp nhanh sprint 30 dòng nhanh chóng |
| `/story-readiness` | Xác thực story đã sẵn sàng trước khi nhận làm |
| `/story-done` | Đánh giá hoàn thành story — xác minh tiêu chí chấp nhận |
| `/estimate` | Đưa ra ước lượng nỗ lực có cấu trúc |
| `/design-review` | Đánh giá một tài liệu thiết kế |
| `/code-review` | Đánh giá code về mặt chất lượng và kiến trúc |
| `/balance-check` | Phân tích dữ liệu cân bằng game |
| `/asset-audit` | Kiểm toán asset về độ tuân thủ quy chuẩn |
| `/content-audit` | So sánh số lượng nội dung trong GDD vs thực tế triển khai |
| `/scope-check` | Phát hiện phình to quy mô so với kế hoạch |
| `/perf-profile` | Đo đạc hiệu năng và xác định điểm nghẽn |
| `/tech-debt` | Quét, theo dõi và sắp xếp ưu tiên nợ kỹ thuật |
| `/gate-check` | Xác thực tính sẵn sàng để chuyển giai đoạn (PASS/CONCERNS/FAIL) |
| `/consistency-check` | Quét toàn bộ GDD để tìm mâu thuẫn chéo giữa các tài liệu |
| `/security-audit` | Kiểm toán lỗ hổng bảo mật: can thiệp save, gian lận, mạng, lộ dữ liệu |
| `/reverse-document` | Tạo tài liệu thiết kế/kiến trúc từ code có sẵn |
| `/milestone-review` | Đánh giá tiến độ milestone |
| `/retrospective` | Chạy buổi tổng kết cải tiến sprint/milestone |
| `/bug-report` | Tạo báo cáo bug có cấu trúc |
| `/playtest-report` | Tạo hoặc phân tích phản hồi playtest |
| `/onboard` | Tạo tài liệu onboarding cho một vai trò |
| `/release-checklist` | Xác thực checklist trước khi phát hành |
| `/launch-checklist` | Xác thực toàn diện tính sẵn sàng ra mắt |
| `/changelog` | Tự động tạo changelog từ lịch sử git |
| `/patch-notes` | Tạo patch notes hướng tới người chơi |
| `/hotfix` | Sửa lỗi khẩn cấp kèm audit trail |
| `/day-one-patch` | Chuẩn bị bản vá ngày đầu cho các lỗi đã biết sau gold master |
| `/prototype` | Prototype ý tưởng — kiểm chứng ý tưởng cốt lõi trước khi viết GDD (Giai đoạn 1) |
| `/vertical-slice` | Bản build chất lượng cao end-to-end — kiểm chứng trọn vẹn game loop (Giai đoạn 4) |
| `/localize` | Quét, trích xuất và xác thực bản địa hóa |
| `/team-combat` | Điều phối toàn bộ nhóm tính năng chiến đấu |
| `/team-narrative` | Điều phối toàn bộ nhóm cốt truyện |
| `/team-ui` | Điều phối toàn bộ nhóm giao diện UI |
| `/team-release` | Điều phối toàn bộ nhóm phát hành |
| `/team-polish` | Điều phối toàn bộ nhóm đánh bóng |
| `/team-audio` | Điều phối toàn bộ nhóm âm thanh |
| `/team-level` | Điều phối toàn bộ nhóm thiết kế màn chơi |
| `/team-live-ops` | Điều phối nhóm vận hành trực tuyến cho mùa giải và sự kiện |
| `/team-qa` | Điều phối chu kỳ QA đầy đủ — kế hoạch, test cases, smoke check, ký duyệt |
| `/qa-plan` | Tạo kế hoạch kiểm thử QA cho một sprint hoặc tính năng |
| `/bug-triage` | Đánh giá lại ưu tiên bug, phân bổ vào sprint, nhận diện xu hướng |
| `/smoke-check` | Chạy smoke test các luồng quan trọng trước khi bàn giao QA (PASS/FAIL) |
| `/soak-test` | Tạo quy trình kiểm thử độ ổn định dài hạn |
| `/regression-suite` | Ánh xạ độ bao phủ tới GDD, cảnh báo khoảng trống, duy trì bộ test hồi quy |
| `/test-setup` | Dựng khung test framework + pipeline CI cho engine của dự án |
| `/test-helpers` | Tạo thư viện trợ giúp kiểm thử đặc thù theo engine |
| `/test-flakiness` | Phát hiện test chập chờn từ lịch sử CI |
| `/test-evidence-review` | Đánh giá chất lượng file test và bằng chứng thủ công |
| `/skill-test` | Xác thực file skill về tính tuân thủ và độ chính xác |
| `/skill-improve` | Cải tiến skill bằng vòng lặp test-fix-retest |

### 4. Sử dụng Template cho tài liệu mới

Các template nằm trong `docs/reference/templates/`:

- `game-design-document.md` -- cho các cơ chế và hệ thống mới
- `architecture-decision-record.md` -- cho các quyết định kỹ thuật
- `architecture-traceability.md` -- ánh xạ yêu cầu GDD tới ADR và story ID
- `risk-register-entry.md` -- cho các rủi ro mới
- `narrative-character-sheet.md` -- cho các nhân vật mới
- `test-plan.md` -- cho kế hoạch test tính năng
- `sprint-plan.md` -- cho lập kế hoạch sprint
- `milestone-definition.md` -- cho các milestone mới
- `level-design-document.md` -- cho các màn chơi mới
- `game-pillars.md` -- cho các trụ cột thiết kế cốt lõi
- `art-bible.md` -- cho tài liệu tham chiếu phong cách mỹ thuật
- `technical-design-document.md` -- cho thiết kế kỹ thuật từng hệ thống
- `post-mortem.md` -- cho tổng kết sau dự án / milestone
- `sound-bible.md` -- cho tài liệu tham chiếu phong cách âm thanh
- `release-checklist-template.md` -- cho checklist phát hành nền tảng
- `changelog-template.md` -- cho changelog
- `release-notes.md` -- cho ghi chú phát hành tới người chơi
- `incident-response.md` -- cho sổ tay xử lý sự cố trực tiếp
- `game-concept.md` -- cho concept game ban đầu (MDA, SDT, Flow, Bartle)
- `pitch-document.md` -- cho tài liệu thuyết trình với các bên liên quan
- `economy-model.md` -- cho thiết kế kinh tế ảo (sink/faucet)
- `faction-design.md` -- cho bản sắc phe phái, cốt truyện và vai trò gameplay
- `systems-index.md` -- cho phân rã hệ thống và ánh xạ phụ thuộc
- `project-stage-report.md` -- cho báo cáo phát hiện giai đoạn dự án
- `design-doc-from-implementation.md` -- cho tài liệu hóa ngược code thành GDD
- `architecture-doc-from-code.md` -- cho tài liệu hóa ngược code thành tài liệu kiến trúc
- `concept-doc-from-prototype.md` -- cho tài liệu hóa ngược prototype thành tài liệu concept
- `ux-spec.md` -- cho đặc tả UX từng màn hình (vùng bố cục, trạng thái, sự kiện)
- `hud-design.md` -- cho triết lý HUD toàn game, các phân vùng và đặc tả thành phần
- `accessibility-requirements.md` -- cho ma trận tính năng và phân tầng tiếp cận toàn dự án
- `interaction-pattern-library.md` -- cho các điều khiển UI tiêu chuẩn và mẫu đặc thù của game
- `player-journey.md` -- cho hành trình cảm xúc 6 giai đoạn và điểm giữ chân theo thang thời gian
- `difficulty-curve.md` -- cho các trục độ khó, đường dốc làm quen và tương tác đa hệ thống
- `test-evidence.md` -- template ghi nhận bằng chứng kiểm thử thủ công

Ngoài ra trong `docs/reference/templates/collaborative-protocols/` (được sử dụng bởi các agent):

- `design-agent-protocol.md` -- chu kỳ hỏi-lựa chọn-bản thảo-phê duyệt cho agent thiết kế
- `implementation-agent-protocol.md` -- chu kỳ nhận story đến /story-done cho agent lập trình
- `leadership-agent-protocol.md` -- ủy quyền liên bộ phận và báo cáo cho agent cấp giám đốc

### 5. Tuân thủ Quy tắc điều phối

1. Luồng công việc đi từ trên xuống: Giám đốc -> Trưởng bộ phận -> Chuyên viên
2. Xung đột báo cáo ngược lên cấp trên
3. Công việc liên bộ phận do `producer` điều phối
4. Agent không chỉnh sửa file ngoài lĩnh vực của mình khi chưa được ủy quyền
5. Mọi quyết định đều phải được ghi nhận thành tài liệu

## Các bước đầu tiên cho Dự án mới

**Chưa biết bắt đầu từ đâu?** Chạy `/start`. Lệnh sẽ hỏi bạn đang ở đâu và điều hướng tới quy trình phù hợp. Không áp đặt giả định về game, engine hay kinh nghiệm của bạn.

Nếu bạn đã biết mình cần gì, hãy chọn trực tiếp luồng tương ứng:

### Nhánh A: "Tôi chưa có ý tưởng sẽ làm game gì"

1. **Chạy `/start`** (hoặc `/brainstorm open`) — khám phá sáng tạo có hướng dẫn: điều gì làm bạn hứng thú, bạn đã chơi game gì, các ràng buộc của bạn
   - Sinh ra 3 concept, giúp bạn chọn 1, xác định vòng lặp cốt lõi và các trụ cột
   - Tạo tài liệu concept game và khuyến nghị engine phù hợp
2. **Thiết lập engine** — Chạy `/setup-engine` (dùng khuyến nghị từ brainstorm)
   - Cấu hình GEMINI.md, phát hiện khoảng trống kiến thức, điền tài liệu tham chiếu
   - Tạo `docs/reference/technical-preferences.md` với quy ước đặt tên, ngân sách hiệu năng
3. **Xác thực concept** — Chạy `/design-review design/gdd/game-concept.md`
4. **Phân rã thành các hệ thống** — Chạy `/map-systems` để lập bản đồ các hệ thống và phụ thuộc
5. **Thiết kế từng hệ thống** — Chạy `/design-system [tên-hệ-thống]` (hoặc `/map-systems next`) để viết GDD theo thứ tự phụ thuộc
6. **Làm prototype cơ chế** — Chạy `/prototype [cơ-chế-cốt-lõi]` (1–3 ngày — trước khi viết GDD chi tiết)
7. **Thiết kế từng hệ thống** — Chạy `/design-system [tên-hệ-thống]` để viết GDD dựa trên phát hiện từ prototype
8. **Lập kế hoạch sprint đầu tiên** — Sau khi có kiến trúc và `/vertical-slice`, chạy `/sprint-plan new`
9. Bắt đầu xây dựng game

### Nhánh B: "Tôi đã biết rõ mình muốn làm game gì"

Nếu bạn đã có concept game và lựa chọn engine:

1. **Thiết lập engine** — Chạy `/setup-engine [engine] [version]` (ví dụ: `/setup-engine unity 6000.0`)
2. **Viết các Trụ cột Game** — ủy quyền cho `creative-director`
3. **Phân rã thành các hệ thống** — Chạy `/map-systems` để liệt kê hệ thống và phụ thuộc
4. **Thiết kế từng hệ thống** — Chạy `/design-system [tên-hệ-thống]` để viết GDD theo thứ tự phụ thuộc
5. **Tạo ADR ban đầu** — Chạy `/architecture-decision`
6. **Tạo milestone đầu tiên** trong `production/milestones/`
7. **Lập kế hoạch sprint đầu tiên** — Chạy `/sprint-plan new`
8. Bắt đầu xây dựng game

### Nhánh C: "Tôi đã có ý tưởng game nhưng chưa chọn engine"

1. **Chạy `/setup-engine`** không truyền đối số — hệ thống sẽ hỏi về nhu cầu của game (2D/3D, nền tảng, quy mô nhóm, ngôn ngữ ưa thích) và đề xuất engine phù hợp
2. Làm theo Nhánh B từ bước 2 trở đi

### Nhánh D: "Tôi đã có dự án sẵn từ trước"

Nếu bạn đã có sẵn tài liệu thiết kế, prototype hoặc code:

1. **Chạy `/start`** (hoặc `/project-stage-detect`) — phân tích những gì đang có, tìm khoảng trống và đề xuất bước tiếp theo
2. **Chạy `/adopt`** nếu bạn có GDD, ADR, hoặc story sẵn — audit định dạng và lập kế hoạch migration để điền các khoảng trống mà không ghi đè code cũ
3. **Cấu hình engine nếu cần** — Chạy `/setup-engine` nếu chưa cấu hình
4. **Xác thực tính sẵn sàng của giai đoạn** — Chạy `/gate-check` để biết bạn đang ở đâu
5. **Lập kế hoạch sprint tiếp theo** — Chạy `/sprint-plan new`

## Cấu trúc file tham khảo

```
GEMINI.md                          -- Cấu hình chính (đọc file này đầu tiên)
.agents/
  skills/                          -- 73 định nghĩa lệnh slash command / skills (YAML frontmatter)
  rules/                           -- Các file quy tắc đặc thù theo đường dẫn
  mcp_config.json                  -- Cấu hình kết nối Unity MCP
.axit/
  workspace.yaml                   -- Cấu hình workspace & mapping hệ thống
  core/, capabilities/             -- Hồ sơ năng lực và vai trò
  systems/, registry/              -- Danh mục hệ thống & kiến trúc
  plans/, state/                   -- Kế hoạch & trạng thái hoạt động (active.md)
docs/
  reference/
    quick-start.md                 -- File hướng dẫn này
    technical-preferences.md       -- Tiêu chuẩn đặc thù dự án (điền bởi /setup-engine)
    coding-standards.md            -- Tiêu chuẩn code và tài liệu thiết kế
    coordination-rules.md          -- Quy tắc điều phối agent
    director-gates.md              -- Tiêu chuẩn kiểm soát chất lượng qua các cổng (Gates)
    directory-structure.md         -- Bố cục thư mục dự án
    workflow-catalog.yaml          -- Định nghĩa pipeline 7 giai đoạn (được đọc bởi /help)
    setup-requirements.md          -- Yêu cầu hệ thống tiên quyết
    templates/                     -- Các template tài liệu (GDD, QA, ADR, v.v.)
```
