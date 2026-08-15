<p align="center">
  <h1 align="center">Claude Code Game Studios</h1>
  <p align="center">
    Biến một phiên làm việc Claude Code duy nhất thành một studio phát triển game hoàn chỉnh.
    <br />
    49 agents. 73 skills. Một đội ngũ AI phối hợp nhịp nhàng.
  </p>
</p>

<p align="center">
  <a href="LICENSE"><img src="https://img.shields.io/badge/license-MIT-blue.svg" alt="MIT License"></a>
  <a href=".claude/agents"><img src="https://img.shields.io/badge/agents-49-blueviolet" alt="49 Agents"></a>
  <a href=".claude/skills"><img src="https://img.shields.io/badge/skills-73-green" alt="73 Skills"></a>
  <a href=".claude/hooks"><img src="https://img.shields.io/badge/hooks-12-orange" alt="12 Hooks"></a>
  <a href=".claude/rules"><img src="https://img.shields.io/badge/rules-11-red" alt="11 Rules"></a>
  <a href="https://docs.anthropic.com/en/docs/claude-code"><img src="https://img.shields.io/badge/built%20for-Claude%20Code-f5f5f5?logo=anthropic" alt="Built for Claude Code"></a>
  <a href="https://www.buymeacoffee.com/donchitos3"><img src="https://img.shields.io/badge/Buy%20Me%20a%20Coffee-Support%20this%20project-FFDD00?logo=buymeacoffee&logoColor=black" alt="Buy Me a Coffee"></a>
  <a href="https://github.com/sponsors/Donchitos"><img src="https://img.shields.io/badge/GitHub%20Sponsors-Support%20this%20project-ea4aaa?logo=githubsponsors&logoColor=white" alt="GitHub Sponsors"></a>
</p>

---

## Vì sao dự án này tồn tại

Tự làm game solo với AI rất mạnh mẽ — nhưng một phiên chat đơn lẻ thường thiếu cấu trúc. Không có gì ngăn bạn hardcode các magic numbers, bỏ qua tài liệu thiết kế (design docs), hay viết code kiểu spaghetti. Không có khâu kiểm thử QA, không có đánh giá thiết kế (design review), và không ai đặt câu hỏi "điều này có thực sự phù hợp với tầm nhìn của trò chơi không?"

**Claude Code Game Studios** giải quyết vấn đề này bằng cách mang đến cho phiên AI của bạn cấu trúc của một studio thực thụ. Thay vì chỉ có một trợ lý đa năng chung chung, bạn có 49 agent chuyên biệt được tổ chức theo cấp bậc studio — các giám đốc (directors) bảo vệ tầm nhìn, các trưởng bộ phận (leads) làm chủ lĩnh vực của mình, và các chuyên viên (specialists) trực tiếp thực thi. Mỗi agent đều có trách nhiệm rõ ràng, quy trình báo cáo/chuyển tiếp (escalation paths) và các cổng kiểm soát chất lượng (quality gates).

Kết quả: Bạn vẫn là người đưa ra mọi quyết định, nhưng giờ đây bạn có một đội ngũ biết đặt đúng câu hỏi, phát hiện sớm sai sót và giữ cho dự án của bạn luôn ngăn nắp từ bước brainstorm đầu tiên cho đến khi phát hành.

---

## Mục lục

- [Những gì bao gồm](#những-gì-bao-gồm)
- [Cấp bậc Studio (Studio Hierarchy)](#cấp-bậc-studio-studio-hierarchy)
- [Các lệnh Slash Commands](#các-lệnh-slash-commands)
- [Bắt đầu nhanh](#bắt-đầu-nhanh)
- [Nâng cấp](#nâng-cấp)
- [Cấu trúc dự án](#cấu-trúc-dự-án)
- [Cách thức hoạt động](#cách-thức-hoạt-động)
- [Triết lý thiết kế](#triết-lý-thiết-kế)
- [Tùy biến](#tùy-biến)
- [Hỗ trợ nền tảng](#hỗ-trợ-nền-tảng)
- [Cộng đồng](#cộng-đồng)
- [Ủng hộ dự án](#ủng-hộ-dự-án)
- [Giấy phép (License)](#giấy-phép-license)

---

## Những gì bao gồm

| Danh mục | Số lượng | Mô tả |
|---|---|---|
| **Agents** | 49 | Các subagent chuyên biệt trải dài trên design, programming, art, audio, narrative, QA, và production |
| **Skills** | 73 | Các lệnh slash command cho từng giai đoạn workflow (`/start`, `/design-system`, `/create-epics`, `/create-stories`, `/dev-story`, `/story-done`, v.v.) |
| **Hooks** | 12 | Tự động xác thực khi commit, push, thay đổi asset, vòng đời phiên làm việc, audit trail của agent và phát hiện lỗ hổng/khoảng trống |
| **Rules** | 11 | Tiêu chuẩn viết code theo phạm vi đường dẫn được thực thi khi chỉnh sửa gameplay, engine, AI, UI, network code, v.v. |
| **Templates** | 41 | Các template tài liệu cho GDD, UX spec, ADR, sprint plan, thiết kế HUD, accessibility, v.v. |

## Cấp bậc Studio (Studio Hierarchy)

Các agent được tổ chức thành ba tầng (tiers), mô phỏng cách hoạt động của các studio ngoài đời thực:

```
Tier 1 — Directors (Opus)
  creative-director    technical-director    producer

Tier 2 — Department Leads (Sonnet)
  game-designer        lead-programmer       art-director
  audio-director       narrative-director    qa-lead
  release-manager      localization-lead

Tier 3 — Specialists (Sonnet/Haiku)
  gameplay-programmer  engine-programmer     ai-programmer
  network-programmer   tools-programmer      ui-programmer
  systems-designer     level-designer        economy-designer
  technical-artist     sound-designer        writer
  world-builder        ux-designer           prototyper
  performance-analyst  devops-engineer       analytics-engineer
  security-engineer    qa-tester             accessibility-specialist
  live-ops-designer    community-manager
```

### Chuyên viên Game Engine (Engine Specialists)

Template này bao gồm các bộ agent cho cả 3 engine phổ biến. Hãy sử dụng bộ phù hợp với dự án của bạn:

| Engine | Lead Agent | Sub-Specialists |
|---|---|---|
| **Godot 4** | `godot-specialist` | GDScript, Shaders, GDExtension |
| **Unity** | `unity-specialist` | DOTS/ECS, Shaders/VFX, Addressables, UI Toolkit |
| **Unreal Engine 5** | `unreal-specialist` | GAS, Blueprints, Replication, UMG/CommonUI |

## Các lệnh Slash Commands

Gõ `/` trong Claude Code để truy cập toàn bộ 73 skills:

**Onboarding & Điều hướng**
`/start` `/help` `/project-stage-detect` `/setup-engine` `/adopt`

**Game Design**
`/brainstorm` `/map-systems` `/design-system` `/quick-design` `/review-all-gdds` `/propagate-design-change`

**Art & Assets**
`/art-bible` `/asset-spec` `/asset-audit`

**UX & Thiết kế giao diện**
`/ux-design` `/ux-review`

**Kiến trúc (Architecture)**
`/create-architecture` `/architecture-decision` `/architecture-review` `/create-control-manifest`

**Stories & Sprints**
`/create-epics` `/create-stories` `/dev-story` `/sprint-plan` `/sprint-status` `/story-readiness` `/story-done` `/estimate`

**Đánh giá & Phân tích (Reviews & Analysis)**
`/design-review` `/code-review` `/balance-check` `/content-audit` `/scope-check` `/perf-profile` `/tech-debt` `/gate-check` `/consistency-check` `/security-audit`

**QA & Kiểm thử (QA & Testing)**
`/qa-plan` `/smoke-check` `/soak-test` `/regression-suite` `/test-setup` `/test-helpers` `/test-evidence-review` `/test-flakiness` `/skill-test` `/skill-improve`

**Production**
`/milestone-review` `/retrospective` `/bug-report` `/bug-triage` `/reverse-document` `/playtest-report`

**Phát hành (Release)**
`/release-checklist` `/launch-checklist` `/changelog` `/patch-notes` `/hotfix` `/day-one-patch`

**Sáng tạo & Nội dung (Creative & Content)**
`/prototype` `/onboard` `/localize`

**Phối hợp đội ngũ (Team Orchestration)** (điều phối nhiều agent cho cùng một tính năng)
`/team-combat` `/team-narrative` `/team-ui` `/team-release` `/team-polish` `/team-audio` `/team-level` `/team-live-ops` `/team-qa`

## Bắt đầu nhanh

### Điều kiện tiên quyết

- [Git](https://git-scm.com/)
- [Claude Code](https://docs.anthropic.com/en/docs/claude-code) (`npm install -g @anthropic-ai/claude-code`)
- **Khuyến nghị**: [jq](https://jqlang.github.io/jq/) (để xác thực hook) và Python 3 (để xác thực JSON)

Tất cả các hook đều tự động bỏ qua an toàn nếu thiếu công cụ tùy chọn — không có gì bị lỗi, bạn chỉ tạm thời không có tính năng xác thực đó.

### Thiết lập

1. **Clone hoặc sử dụng làm template**:
   ```bash
   git clone https://github.com/Donchitos/Claude-Code-Game-Studios.git my-game
   cd my-game
   ```

2. **Mở Claude Code** và khởi động một phiên làm việc:
   ```bash
   claude
   ```

3. **Chạy `/start`** — hệ thống sẽ hỏi tình trạng hiện tại của bạn (chưa có ý tưởng, khái niệm mơ hồ, thiết kế rõ ràng, hoặc dự án có sẵn) và hướng dẫn bạn tới đúng workflow. Không đưa ra suy đoán tùy tiện.

   Hoặc nhảy trực tiếp đến một skill cụ thể nếu bạn đã biết rõ nhu cầu:
   - `/brainstorm` — khám phá các ý tưởng game từ đầu
   - `/setup-engine godot 4.6` — cấu hình game engine nếu bạn đã xác định
   - `/project-stage-detect` — phân tích dự án hiện có

## Nâng cấp

Bạn đang sử dụng phiên bản cũ hơn của template này? Xem [UPGRADING.md](UPGRADING.md) để biết hướng dẫn migration từng bước, phân tích những thay đổi giữa các phiên bản và những file nào an toàn để ghi đè so với những file cần merge thủ công.

## Cấu trúc dự án

```
CLAUDE.md                           # Cấu hình tổng thể
.claude/
  settings.json                     # Hooks, quyền hạn (permissions), quy tắc an toàn
  agents/                           # 49 định nghĩa agent (markdown + YAML frontmatter)
  skills/                           # 73 slash commands (mỗi thư mục con tương ứng một skill)
  hooks/                            # 12 hook scripts (bash, đa nền tảng)
  rules/                            # 11 quy chuẩn code theo phạm vi đường dẫn
  statusline.sh                     # Script hiển thị status line (context%, model, stage, epic breadcrumb)
  docs/
    workflow-catalog.yaml           # Định nghĩa pipeline 7 giai đoạn (được đọc bởi /help)
    templates/                      # 41 document templates
src/                                # Source code của game
assets/                             # Art, audio, VFX, shaders, data files
design/                             # GDDs, narrative docs, level designs
docs/                               # Tài liệu kỹ thuật và các quyết định kiến trúc ADR
tests/                              # Bộ kiểm thử (unit, integration, performance, playtest)
tools/                              # Công cụ build và pipeline
prototypes/                         # Bản mẫu thử nghiệm (độc lập với src/)
production/                         # Kế hoạch sprint, milestones, theo dõi release
```

## Cách thức hoạt động

### Phối hợp Agent (Agent Coordination)

Các agent tuân theo mô hình phân quyền có cấu trúc:

1. **Phân quyền theo chiều dọc (Vertical delegation)** — Giám đốc (directors) giao việc cho trưởng bộ phận (leads), trưởng bộ phận giao việc cho chuyên viên (specialists)
2. **Tham vấn theo chiều ngang (Horizontal consultation)** — Các agent cùng cấp có thể tham vấn lẫn nhau nhưng không thể đưa ra quyết định ràng buộc chéo lĩnh vực
3. **Giải quyết xung đột (Conflict resolution)** — Bất đồng quan điểm sẽ được báo cáo lên cấp quản lý chung (`creative-director` cho thiết kế, `technical-director` cho kỹ thuật)
4. **Lan truyền thay đổi (Change propagation)** — Các thay đổi xuyên suốt nhiều phòng ban do `producer` điều phối
5. **Ranh giới lĩnh vực (Domain boundaries)** — Agent không chỉnh sửa các file ngoài phạm vi phụ trách của mình nếu không có ủy quyền rõ ràng

### Tính cộng tác, không tự động tùy tiện (Collaborative, Not Autonomous)

Đây **không** phải là hệ thống tự lái (auto-pilot). Mọi agent đều tuân theo quy trình cộng tác nghiêm ngặt:

1. **Hỏi (Ask)** — Agent đặt câu hỏi trước khi đề xuất giải pháp
2. **Đưa ra các lựa chọn (Present options)** — Agent đưa ra 2-4 phương án kèm ưu/nhược điểm
3. **Bạn quyết định (You decide)** — Người dùng luôn là người đưa ra lựa chọn cuối cùng
4. **Bản thảo (Draft)** — Agent trình bày bản thảo trước khi hoàn thiện
5. **Phê duyệt (Approve)** — Không có nội dung nào được ghi nhận chính thức nếu chưa có sự phê duyệt của bạn

Bạn luôn nắm quyền kiểm soát. Các agent cung cấp cấu trúc và chuyên môn, không tự ý hành động ngoài tầm kiểm soát.

### Tự động hóa an toàn (Automated Safety)

**Hooks** tự động chạy trong mỗi phiên làm việc:

| Hook | Điều kiện kích hoạt (Trigger) | Chức năng |
|---|---|---|
| `validate-commit.sh` | PreToolUse (Bash) | Kiểm tra các giá trị hardcode, định dạng TODO, tính hợp lệ của JSON, các phần trong design doc — thoát sớm nếu lệnh không phải `git commit` |
| `validate-push.sh` | PreToolUse (Bash) | Cảnh báo khi push vào các nhánh được bảo vệ — thoát sớm nếu lệnh không phải `git push` |
| `validate-assets.sh` | PostToolUse (Write/Edit) | Xác thực quy ước đặt tên và cấu trúc JSON — thoát sớm nếu file không nằm trong `assets/` |
| `session-start.sh` | Mở phiên (Session open) | Hiển thị branch hiện tại và các commit gần đây để định hướng |
| `detect-gaps.sh` | Mở phiên (Session open) | Phát hiện dự án mới (gợi ý `/start`) và phát hiện thiếu design doc khi đã có code hoặc prototype |
| `pre-compact.sh` | Trước khi compact | Lưu lại các ghi chú tiến độ của phiên làm việc |
| `post-compact.sh` | Sau khi compact | Nhắc nhở Claude khôi phục trạng thái phiên từ `active.md` |
| `notify.sh` | Sự kiện thông báo (Notification) | Hiển thị thông báo Windows toast notification qua PowerShell |
| `session-stop.sh` | Đóng phiên (Session close) | Lưu trữ `active.md` vào session log và ghi lại hoạt động git |
| `log-agent.sh` | Agent được khởi tạo | Bắt đầu audit trail — ghi log quá trình gọi subagent |
| `log-agent-stop.sh` | Agent kết thúc | Kết thúc audit trail — hoàn tất bản ghi của subagent |
| `validate-skill-change.sh` | PostToolUse (Write/Edit) | Khuyến nghị chạy `/skill-test` sau bất kỳ thay đổi nào trong `.claude/skills/` |

> **Lưu ý**: `validate-commit.sh`, `validate-assets.sh`, và `validate-skill-change.sh` được kích hoạt trên mỗi lệnh Bash/Write tool và thoát ngay lập tức (exit 0) nếu câu lệnh hoặc đường dẫn file không liên quan. Đây là hành vi hook bình thường — không ảnh hưởng đến hiệu năng.

**Quy tắc phân quyền (Permission rules)** trong `settings.json` tự động cho phép các thao tác an toàn (git status, chạy test) và chặn các thao tác nguy hiểm (force push, `rm -rf`, đọc file `.env`).

### Quy tắc theo phạm vi đường dẫn (Path-Scoped Rules)

Các tiêu chuẩn code được tự động áp dụng dựa trên vị trí file:

| Đường dẫn (Path) | Quy chuẩn áp dụng |
|---|---|
| `src/gameplay/**` | Giá trị định hướng theo dữ liệu (data-driven), sử dụng delta time, không tham chiếu trực tiếp đến UI |
| `src/core/**` | Không cấp phát bộ nhớ (zero allocations) trong các hot paths, thread safety, tính ổn định của API |
| `src/ai/**` | Giới hạn hiệu năng (performance budgets), khả năng debug, tham số định hướng theo dữ liệu |
| `src/networking/**` | Server-authoritative, message có phiên bản, bảo mật |
| `src/ui/**` | Không trực tiếp nắm giữ game state, sẵn sàng cho đa ngôn ngữ (localization), hỗ trợ accessibility |
| `design/gdd/**` | Yêu cầu đủ 8 phần, định dạng công thức, xử lý các trường hợp biên (edge cases) |
| `tests/**` | Quy ước đặt tên test, yêu cầu độ bao phủ (coverage), các mẫu fixture |
| `prototypes/**` | Tiêu chuẩn nới lỏng, yêu cầu có README, giả thuyết được ghi nhận tài liệu rõ ràng |

## Triết lý thiết kế

Template này được xây dựng dựa trên các phương pháp phát triển game chuyên nghiệp:

- **MDA Framework** — Phân tích Mechanics, Dynamics, Aesthetics trong game design
- **Self-Determination Theory** — Tự chủ (Autonomy), Năng lực (Competence), Gắn kết (Relatedness) để thúc đẩy động lực của người chơi
- **Flow State Design** — Cân bằng giữa thử thách và kỹ năng để tạo sự lôi cuốn cho người chơi
- **Bartle Player Types** — Xác định đối tượng người chơi mục tiêu và kiểm chứng
- **Verification-Driven Development** — Viết test trước, sau đó mới triển khai code (implementation)

## Tùy biến

Đây là một **template**, không phải là một framework bị khóa cứng. Mọi thứ đều có thể tùy chỉnh:

- **Thêm/xóa agent** — Xóa các file agent bạn không cần, thêm agent mới cho các lĩnh vực của bạn
- **Chỉnh sửa prompt của agent** — Tinh chỉnh hành vi của agent, bổ sung kiến thức đặc thù của dự án
- **Sửa đổi skills** — Điều chỉnh workflows phù hợp với quy trình làm việc của đội ngũ bạn
- **Thêm rules** — Tạo các quy tắc theo đường dẫn mới cho cấu trúc thư mục của dự án
- **Tinh chỉnh hooks** — Điều chỉnh độ nghiêm ngặt của việc xác thực, thêm các bước kiểm tra mới
- **Chọn game engine của bạn** — Sử dụng bộ agent cho Godot, Unity, hoặc Unreal (hoặc không dùng bộ nào)
- **Thiết lập mức độ review** — `full` (tất cả các cổng director), `lean` (chỉ các cổng giai đoạn), hoặc `solo` (không review). Thiết lập trong `/start` hoặc chỉnh sửa `production/review-mode.txt`. Ghi đè cho từng lần chạy với `--review solo` trên bất kỳ skill nào.

## Hỗ trợ nền tảng

Môi trường phát triển và kiểm thử chính là trên **Windows 10** với Git Bash. Tất cả các hook đều sử dụng cú pháp tương thích POSIX (`grep -E`, không dùng `grep -P`) và bao gồm cơ chế dự phòng khi thiếu công cụ, do đó chúng có thể chạy tốt trên macOS và Linux. Hook `notify.sh` sử dụng PowerShell cho thông báo toast trên Windows và không thực hiện thao tác gì trên các hệ điều hành khác — tính năng thông báo desktop trên macOS/Linux đang được hoàn thiện. Việc kiểm thử đa nền tảng đang tiếp tục diễn ra; vui lòng báo cáo issue nếu gặp bất kỳ lỗi nào liên quan đến nền tảng cụ thể.

## Cộng đồng

- **Thảo luận (Discussions)** — [GitHub Discussions](https://github.com/Donchitos/Claude-Code-Game-Studios/discussions) để đặt câu hỏi, chia sẻ ý tưởng và giới thiệu sản phẩm bạn đã tạo ra
- **Issues** — [Báo cáo lỗi và yêu cầu tính năng](https://github.com/Donchitos/Claude-Code-Game-Studios/issues)

---

## Ủng hộ dự án

Claude Code Game Studios là dự án miễn phí và mã nguồn mở. Nếu nó giúp bạn tiết kiệm thời gian hoặc hỗ trợ bạn phát hành trò chơi của mình, hãy cân nhắc ủng hộ để tiếp tục phát triển:

<p>
  <a href="https://www.buymeacoffee.com/donchitos3"><img src="https://img.shields.io/badge/Buy%20Me%20a%20Coffee-FFDD00?style=for-the-badge&logo=buy-me-a-coffee&logoColor=black" alt="Buy Me a Coffee"></a>
  &nbsp;
  <a href="https://github.com/sponsors/Donchitos"><img src="https://img.shields.io/badge/GitHub%20Sponsors-ea4aaa?style=for-the-badge&logo=githubsponsors&logoColor=white" alt="GitHub Sponsors"></a>
</p>

- **[Buy Me a Coffee](https://www.buymeacoffee.com/donchitos3)** — Ủng hộ một lần
- **[GitHub Sponsors](https://github.com/sponsors/Donchitos)** — Ủng hộ định kỳ qua GitHub

Sự tài trợ giúp có thêm thời gian duy trì các skill, thêm các agent mới, cập nhật kịp thời theo các thay đổi API của Claude Code và game engine, cũng như hỗ trợ cộng đồng.

---

*Được xây dựng cho Claude Code. Duy trì và mở rộng — luôn chào đón đóng góp qua [GitHub Discussions](https://github.com/Donchitos/Claude-Code-Game-Studios/discussions).*

## Giấy phép (License)

Giấy phép MIT. Xem [LICENSE](LICENSE) để biết thêm chi tiết.
