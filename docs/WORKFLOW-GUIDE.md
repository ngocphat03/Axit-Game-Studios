# Axit Game Studios -- Hướng dẫn Workflow hoàn chỉnh

> **Làm thế nào để đi từ con số 0 đến một tựa game hoàn chỉnh phát hành bằng Kiến trúc Agent.**
>
> Hướng dẫn này sẽ dẫn dắt bạn qua từng giai đoạn phát triển game bằng hệ thống Agents & Skills trên nền tảng Google Antigravity & Gemini.
>
> Pipeline bao gồm 7 giai đoạn. Mỗi giai đoạn đều có một cổng kiểm soát chính thức (`/gate-check`) phải vượt qua trước khi tiến bước. Trình tự giai đoạn chuẩn được định nghĩa trong `docs/reference/workflow-catalog.yaml` và được đọc bởi `/help`.

---

## Mục lục

1. [Bắt đầu nhanh (Quick Start)](#bắt-đầu-nhanh-quick-start)
2. [Giai đoạn 1: Ý tưởng (Phase 1: Concept)](#giai-đoạn-1-ý-tưởng-phase-1-concept)
3. [Giai đoạn 2: Thiết kế hệ thống (Phase 2: Systems Design)](#giai-đoạn-2-thiết-kế-hệ-thống-phase-2-systems-design)
4. [Giai đoạn 3: Thiết lập kỹ thuật (Phase 3: Technical Setup)](#giai-đoạn-3-thiết-lập-kỹ-thuật-phase-3-technical-setup)
5. [Giai đoạn 4: Tiền sản xuất (Phase 4: Pre-Production)](#giai-đoạn-4-tiền-sản-xuất-phase-4-pre-production)
6. [Giai đoạn 5: Sản xuất (Phase 5: Production)](#giai-đoạn-5-sản-xuất-phase-5-production)
7. [Giai đoạn 6: Đánh bóng (Phase 6: Polish)](#giai-đoạn-6-đánh-bóng-phase-6-polish)
8. [Giai đoạn 7: Phát hành (Phase 7: Release)](#giai-đoạn-7-phát-hành-phase-7-release)
9. [Các vấn đề xuyên suốt (Cross-Cutting Concerns)](#các-vấn-đề-xuyên-suốt-cross-cutting-concerns)
10. [Phụ lục A: Tra cứu nhanh Agent](#phụ-lục-a-tra-cứu-nhanh-agent)
11. [Phụ lục B: Tra cứu nhanh Slash Command](#phụ-lục-b-tra-cứu-nhanh-slash-command)
12. [Phụ lục C: Các Workflow phổ biến](#phụ-lục-c-các-workflow-phổ-biến)

---

## Bắt đầu nhanh (Quick Start)

### Những gì bạn cần chuẩn bị

Trước khi bắt đầu, hãy đảm bảo bạn có:

- **Google Antigravity (AGY IDE / CLI)** và model **Gemini** đã sẵn sàng
- **Git** và terminal tiêu chuẩn
- **Unity Editor** (Unity 6 / 2022+ LTS)
- **Node.js** (nếu sử dụng Unity MCP bridge)

### Bước 1: Clone và Mở dự án

```bash
git clone <repo-url> my-game
cd my-game
```

### Bước 2: Chạy /start

Nếu đây là phiên làm việc đầu tiên của bạn:

```
/start
```

Quy trình onboarding có hướng dẫn này sẽ hỏi về tình trạng hiện tại và điều hướng bạn tới đúng giai đoạn:

- **Nhánh A** -- Chưa có ý tưởng: chuyển tới `/brainstorm`
- **Nhánh B** -- Đã có ý tưởng sơ khai: chuyển tới `/brainstorm` với hạt giống ý tưởng (seed)
- **Nhánh C** -- Đã có concept rõ ràng: chuyển tới `/setup-engine` và `/map-systems`
- **Nhánh D1** -- Dự án đã có, ít tài liệu: luồng làm việc thông thường
- **Nhánh D2** -- Dự án đã có, đã có GDD/ADR: chạy `/project-stage-detect` sau đó chạy `/adopt` để tiếp nhận dự án cũ (brownfield migration)

### Bước 3: Xác minh Hooks đang hoạt động

Khởi động một phiên Antigravity mới. Bạn sẽ thấy output từ hook `session-start.sh`:

```
=== Axit Game Studios -- Session Context ===
Branch: main
Recent commits:
  abc1234 Initial commit
===================================
```

Nếu bạn thấy thông điệp này, hooks đang hoạt động tốt. Nếu không, hãy kiểm tra `.agents/hooks.json` để đảm bảo cấu hình hook chính xác.

### Bước 4: Nhận trợ giúp bất cứ lúc nào

Tại bất kỳ thời điểm nào, hãy chạy:

```
/help
```

Lệnh này sẽ đọc giai đoạn hiện tại từ `production/stage.txt`, kiểm tra xem tài liệu sản phẩm nào đã tồn tại, và chỉ dẫn chính xác những việc bạn cần làm tiếp theo. Nó phân biệt rõ ràng giữa các bước BẮT BUỘC (REQUIRED) và các cơ hội TÙY CHỌN (OPTIONAL).

### Bước 5: Tạo cấu trúc thư mục của bạn

Các thư mục sẽ được tạo khi có nhu cầu. Hệ thống kỳ vọng bố cục cấu trúc sau:

```
src/                  # Source code của game
  core/               # Code nền tảng/engine/framework
  gameplay/           # Hệ thống gameplay
  ai/                 # Hệ thống AI
  networking/         # Code nhiều người chơi (multiplayer)
  ui/                 # Code giao diện người dùng
  tools/              # Công cụ dev nội bộ
assets/               # Tài nguyên game (Assets)
  art/                # Sprites, models, textures
  audio/              # Nhạc nền, âm thanh SFX
  vfx/                # Hiệu ứng hạt (Particle effects)
  shaders/            # File Shader
  data/               # Dữ liệu cân bằng/cấu hình JSON
design/               # Tài liệu thiết kế (Design documents)
  gdd/                # Game design documents (GDD)
  narrative/          # Cốt truyện, lore, hội thoại
  levels/             # Tài liệu thiết kế màn chơi
  balance/            # Bảng tính và dữ liệu cân bằng
  ux/                 # Đặc tả UX
docs/                 # Tài liệu kỹ thuật
  architecture/       # Architecture Decision Records (ADRs)
  api/                # Tài liệu API
  postmortems/        # Báo cáo tổng kết sau dự án
tests/                # Bộ kiểm thử (Test suites)
prototypes/           # Bản mẫu thử nghiệm ngắn hạn
production/           # Kế hoạch sprint, milestones, phát hành
  sprints/
  milestones/
  releases/
  epics/              # Các file Epic và Story (từ /create-epics + /create-stories)
  playtests/          # Báo cáo chơi thử (Playtest reports)
  session-state/      # Trạng thái phiên làm việc ngắn hạn (gitignored)
  session-logs/       # Audit trail của phiên làm việc (gitignored)
```

> **Mẹo:** Bạn không cần tạo tất cả các thư mục này ngay ngày đầu tiên. Hãy tạo chúng khi bạn tiến đến giai đoạn cần dùng. Điều quan trọng là phải tuân theo cấu trúc này khi tạo, bởi vì **hệ thống rules** thực thi các tiêu chuẩn dựa trên đường dẫn file. Code trong `src/gameplay/` sẽ áp dụng rules gameplay, code trong `src/ai/` sẽ áp dụng rules AI, v.v.

---

## Giai đoạn 1: Ý tưởng (Phase 1: Concept)

### Những gì diễn ra trong giai đoạn này

Bạn đi từ chỗ "chưa có ý tưởng" hoặc "ý tưởng mơ hồ" tới một tài liệu concept game có cấu trúc với các trụ cột (pillars) và hành trình người chơi được xác định rõ ràng. Đây là lúc bạn xác định **bạn đang làm gì** và **tại sao**.

### Pipeline Giai đoạn 1

```
/brainstorm  -->  game-concept.md  -->  /design-review  -->  /setup-engine
     |                                        |                    |
     v                                        v                    v
  10 concepts     Concept doc with       Validation          Engine pinned in
  MDA analysis    pillars, MDA,          of concept          technical-preferences.md
  Player motiv.   core loop, USP         document
                                                                   |
                                                                   v
                                                             /prototype
                                                       (concept prototype — 1-3 days)
                                                        PROCEED ↓     PIVOT → /brainstorm
                                                                   |
                                                                   v (PROCEED)
                                                             /map-systems
                                                                   |
                                                                   v
                                                            systems-index.md
                                                            (all systems, deps,
                                                             priority tiers)
```

### Bước 1.1: Brainstorm với /brainstorm

Đây là điểm khởi đầu của bạn. Chạy skill brainstorm:

```
/brainstorm
```

Hoặc kèm theo gợi ý thể loại:

```
/brainstorm roguelike deckbuilder
```

**Điều gì diễn ra:** Skill brainstorm sẽ dẫn dắt bạn qua quy trình tư duy cộng tác 6 giai đoạn áp dụng các kỹ thuật studio chuyên nghiệp:

1. Đặt câu hỏi về sở thích, chủ đề và các ràng buộc của bạn
2. Tạo ra 10 hạt giống ý tưởng kèm phân tích MDA (Mechanics, Dynamics, Aesthetics)
3. Bạn chọn 2-3 ý tưởng yêu thích để phân tích sâu
4. Thực hiện lập sơ đồ động lực người chơi và nhắm mục tiêu đối tượng
5. Bạn chọn ý tưởng chiến thắng cuối cùng
6. Chuẩn hóa thành file `design/gdd/game-concept.md`

Tài liệu concept bao gồm:

- Elevator pitch (tóm tắt trong một câu)
- Cảm xúc trải nghiệm cốt lõi (Core fantasy - người chơi tưởng tượng mình đang làm gì)
- Phân tích chi tiết MDA
- Đối tượng mục tiêu (Bartle types, nhân khẩu học)
- Sơ đồ vòng lặp cốt lõi (Core loop diagram)
- Điểm bán hàng độc nhất (Unique selling proposition - USP)
- Các tựa game tương đương và điểm khác biệt
- Các trụ cột của game (3-5 giá trị thiết kế không thể thương lượng)
- Các phản trụ cột (Anti-pillars - những điều trò chơi chủ ý né tránh)

### Bước 1.2: Đánh giá Concept (Tùy chọn nhưng khuyến nghị)

```
/design-review design/gdd/game-concept.md
```

Xác thực cấu trúc và tính đầy đủ trước khi bạn tiếp tục.

### Bước 1.3: Chọn Game Engine

```
/setup-engine
```

Hoặc chỉ định engine cụ thể:

```
/setup-engine godot 4.6
```

**Những gì /setup-engine thực hiện:**

- Điền thông tin vào `docs/reference/technical-preferences.md` với quy ước đặt tên, giới hạn hiệu năng, và các mặc định theo engine
- Phát hiện khoảng trống kiến thức (phiên bản engine mới hơn dữ liệu huấn luyện của LLM) và khuyên bạn đối chiếu `docs/engine-reference/`
- Tạo các tài liệu tham chiếu đã ghim phiên bản trong `docs/engine-reference/`

**Tại sao điều này quan trọng:** Sau khi bạn thiết lập engine, hệ thống sẽ biết nên sử dụng những agent chuyên viên engine nào. Nếu bạn chọn Godot, các agent như `godot-specialist`, `godot-gdscript-specialist`, và `godot-shader-specialist` sẽ trở thành chuyên gia đồng hành cùng bạn.

### Bước 1.4: Phân rã Concept thành các Hệ thống (Decompose into Systems)

Trước khi viết từng GDD riêng lẻ, hãy liệt kê tất cả các hệ thống mà trò chơi cần:

```
/map-systems
```

Lệnh này tạo ra file `design/gdd/systems-index.md` -- một tài liệu tổng thể giúp:

- Liệt kê mọi hệ thống game cần có (combat, movement, UI, v.v.)
- Lập sơ đồ phụ thuộc giữa các hệ thống
- Phân tầng ưu tiên (MVP, Vertical Slice, Alpha, Full Vision)
- Xác định thứ tự thiết kế (Foundation > Core > Feature > Presentation > Polish)

Bước này là **bắt buộc** trước khi chuyển sang Giai đoạn 2. Nghiên cứu từ 155 báo cáo tổng kết game (postmortems) xác nhận rằng việc bỏ qua khâu liệt kê hệ thống sẽ làm tốn kém gấp 5-10 lần chi phí trong giai đoạn sản xuất.

### Cổng Giai đoạn 1 (Phase 1 Gate)

```
/gate-check concept
```

**Yêu cầu để vượt qua:**

- Engine đã được cấu hình trong `technical-preferences.md`
- `design/gdd/game-concept.md` đã tồn tại kèm theo các trụ cột
- `design/gdd/systems-index.md` đã tồn tại kèm thứ tự phụ thuộc

**Kết luận:** PASS / CONCERNS / FAIL. CONCERNS có thể vượt qua nếu chấp nhận các rủi ro đã ghi nhận. FAIL sẽ chặn việc chuyển giai đoạn.

---

## Giai đoạn 2: Thiết kế hệ thống (Phase 2: Systems Design)

### Những gì diễn ra trong giai đoạn này

Bạn tạo ra tất cả các tài liệu thiết kế xác định cách thức trò chơi hoạt động. Chưa có dòng code nào được viết — đây là khâu thiết kế thuần túy. Mỗi hệ thống được xác định trong `systems-index` sẽ có GDD riêng, được soạn thảo từng phần, được đánh giá riêng lẻ, và sau đó tất cả GDD sẽ được kiểm tra chéo về tính nhất quán.

### Pipeline Giai đoạn 2

```
/map-systems next  -->  /design-system  -->  /design-review
       |                     |                     |
       v                     v                     v
  Picks next system    Section-by-section     Validates 8
  from systems-index   GDD authoring          required sections
                       (incremental writes)   APPROVED/NEEDS REVISION
       |
       |  (lặp lại cho từng hệ thống MVP)
       v
/review-all-gdds
       |
       v
  Cross-GDD consistency + design theory review
  PASS / CONCERNS / FAIL
```

### Bước 2.1: Soạn thảo System GDDs

Thiết kế từng hệ thống theo thứ tự phụ thuộc bằng workflow có hướng dẫn:

```
/map-systems next
```

Lệnh này chọn hệ thống chưa được thiết kế có độ ưu tiên cao nhất và chuyển giao sang `/design-system`, hướng dẫn bạn tạo GDD từng phần một.

Bạn cũng có thể trực tiếp thiết kế một hệ thống cụ thể:

```
/design-system combat-system
```

**Những gì /design-system thực hiện:**

1. Đọc game concept, systems index, và các GDD liên quan ở thượng nguồn/hạ nguồn
2. Chạy kiểm tra sơ bộ tính khả thi kỹ thuật (Technical Feasibility Pre-Check)
3. Hướng dẫn bạn qua từng phần trong số 8 phần bắt buộc của GDD
4. Mỗi phần tuân theo: Ngữ cảnh > Câu hỏi > Lựa chọn > Quyết định > Bản thảo > Phê duyệt > Ghi file
5. Mỗi phần được ghi ngay vào file sau khi phê duyệt (bảo toàn khi có sự cố)
6. Cảnh báo các xung đột với những GDD đã duyệt trước đó
7. Điều hướng đến các agent chuyên viên theo danh mục (`systems-designer` cho công thức toán học, `economy-designer` cho kinh tế, `narrative-director` cho hệ thống cốt truyện)

**8 phần bắt buộc của GDD:**

| # | Mục | Nội dung cần có |
|---|---|---|
| 1 | **Tổng quan (Overview)** | Một đoạn văn tóm tắt hệ thống |
| 2 | **Cảm xúc người chơi (Player Fantasy)** | Người chơi tưởng tượng/cảm thấy gì khi sử dụng hệ thống này |
| 3 | **Quy tắc chi tiết (Detailed Rules)** | Các quy tắc cơ chế rõ ràng, không mơ hồ |
| 4 | **Công thức (Formulas)** | Mọi phép tính toán, kèm định nghĩa biến và phạm vi giá trị |
| 5 | **Trường hợp biên (Edge Cases)** | Điều gì xảy ra trong các tình huống bất thường? Phải giải quyết rõ ràng. |
| 6 | **Phụ thuộc (Dependencies)** | Kết nối với những hệ thống nào khác (hai chiều) |
| 7 | **Tham số tinh chỉnh (Tuning Knobs)** | Những giá trị nào designer có thể chỉnh sửa an toàn, kèm khoảng giá trị an toàn |
| 8 | **Tiêu chí chấp nhận (Acceptance Criteria)** | Làm sao kiểm thử hệ thống hoạt động đúng? Phải cụ thể, đo lường được. |

Kèm theo mục **Cảm giác chơi (Game Feel)**: tham chiếu cảm giác, độ phản hồi input (ms/frames), mục tiêu hoạt ảnh (startup/active/recovery), khoảnh khắc tác động, trọng lượng chuyển động.

### Bước 2.2: Đánh giá từng GDD

Trước khi bắt đầu hệ thống tiếp theo, hãy xác thực hệ thống hiện tại:

```
/design-review design/gdd/combat-system.md
```

Kiểm tra cả 8 phần về tính đầy đủ, độ rõ ràng của công thức, giải quyết trường hợp biên, phụ thuộc hai chiều và tiêu chí chấp nhận có thể kiểm thử được.

**Kết luận:** APPROVED / NEEDS REVISION / MAJOR REVISION. Chỉ những GDD đạt APPROVED mới nên đi tiếp.

### Bước 2.3: Các thay đổi nhỏ không cần GDD đầy đủ

Đối với các tinh chỉnh số liệu, bổ sung nhỏ không cần tới một GDD hoàn chỉnh:

```
/quick-design "thêm 10% sát thương thưởng cho đòn đánh bọc sườn"
```

Lệnh này tạo một đặc tả tinh gọn trong `design/quick-specs/` thay vì tạo GDD 8 phần đầy đủ.

### Bước 2.4: Đánh giá tính nhất quán xuyên suốt các GDD

Sau khi tất cả các GDD của hệ thống MVP được duyệt riêng lẻ:

```
/review-all-gdds
```

Lệnh này đọc TẤT CẢ các GDD cùng lúc và chạy hai giai đoạn phân tích:

**Phase 1 -- Tính nhất quán chéo giữa các GDD:**
- Tính hai chiều của phụ thuộc (A tham chiếu B, B có tham chiếu A không?)
- Mâu thuẫn quy tắc giữa các hệ thống
- Tham chiếu cũ tới các hệ thống đã đổi tên hoặc xóa
- Xung đột quyền sở hữu (hai hệ thống cùng nhận một trách nhiệm)
- Tương thích phạm vi công thức (đầu ra của Hệ thống A có khớp đầu vào Hệ thống B?)
- Kiểm tra chéo tiêu chí chấp nhận

**Phase 2 -- Lý thuyết thiết kế game (Game Design Holism):**
- Các vòng lặp tiến trình cạnh tranh nhau (hai hệ thống có tranh giành cùng một không gian phần thưởng?)
- Tải trọng nhận thức (Cognitive load - có quá 4 hệ thống hoạt động cùng lúc không?)
- Chiến lược áp đảo (Dominant strategies - một cách chơi làm lu mờ tất cả cách khác)
- Phân tích vòng lặp kinh tế (nguồn tạo tiền và nguồn tiêu tiền có cân bằng?)
- Tính nhất quán của đường cong độ khó trên các hệ thống
- Sự liên kết với trụ cột và vi phạm phản trụ cột
- Sự mạch lạc của trải nghiệm người chơi

**Đầu ra:** `design/gdd/gdd-cross-review-[date].md` kèm kết luận.

### Bước 2.5: Thiết kế cốt truyện (Nếu có)

Nếu trò chơi của bạn có cốt truyện, truyền thuyết lore, hoặc hội thoại:

1. **Xây dựng thế giới (World-building)** -- Sử dụng `world-builder` để xác định phe phái, lịch sử, địa lý và quy tắc thế giới
2. **Cấu trúc cốt truyện (Story structure)** -- Sử dụng `narrative-director` để thiết kế các tuyến truyện (arcs), tuyến nhân vật và các nhịp tự sự (narrative beats)
3. **Hồ sơ nhân vật (Character sheets)** -- Sử dụng template `narrative-character-sheet.md`

### Cổng Giai đoạn 2 (Phase 2 Gate)

```
/gate-check systems-design
```

**Yêu cầu để vượt qua:**

- Tất cả các hệ thống MVP trong `systems-index.md` đều có `Status: Approved`
- Mỗi hệ thống MVP đều có GDD đã được đánh giá
- Báo cáo đánh giá chéo GDD đã tồn tại (`design/gdd/gdd-cross-review-*.md`) với kết luận PASS hoặc CONCERNS (không được là FAIL)

---

## Giai đoạn 3: Thiết lập kỹ thuật (Phase 3: Technical Setup)

### Những gì diễn ra trong giai đoạn này

Bạn đưa ra các quyết định kỹ thuật quan trọng, ghi lại dưới dạng Hồ sơ quyết định kiến trúc (Architecture Decision Records - ADRs), xác thực qua đánh giá, và tạo ra một control manifest cung cấp các quy tắc rõ ràng, có thể thực thi cho lập trình viên. Bạn cũng thiết lập các nền tảng UX.

### Pipeline Giai đoạn 3

```
/create-architecture  -->  /architecture-decision (x N)  -->  /architecture-review
        |                          |                                   |
        v                          v                                   v
  Master architecture       Per-decision ADRs              Validates completeness,
  document covering         in docs/architecture/          dependency ordering,
  all systems               adr-*.md                       engine compatibility
                                                                      |
                                                                      v
                                                         /create-control-manifest
                                                                      |
                                                                      v
                                                         Flat programmer rules
                                                         docs/architecture/
                                                         control-manifest.md
        Các phần khác trong giai đoạn này:
        -------------------
        /ux-design  -->  /ux-review
        Tài liệu yêu cầu Accessibility
        Thư viện Interaction pattern library
```

### Bước 3.1: Tài liệu Kiến trúc tổng thể

```
/create-architecture
```

Tạo tài liệu kiến trúc bao quát trong `docs/architecture/architecture.md` bao gồm ranh giới hệ thống, luồng dữ liệu và các điểm tích hợp.

### Bước 3.2: Hồ sơ quyết định kiến trúc (ADRs)

Cho từng quyết định kỹ thuật quan trọng:

```
/architecture-decision "State Machine vs Behavior Tree cho NPC AI"
```

**Điều gì diễn ra:** Skill hướng dẫn bạn tạo một ADR bao gồm:
- Bối cảnh và các yếu tố thúc đẩy quyết định
- Tất cả các lựa chọn kèm ưu/nhược điểm và độ tương thích engine
- Lựa chọn được chọn kèm lập luận
- Hệ quả (tích cực, tiêu cực, rủi ro)
- Phụ thuộc (Depends On, Enables, Blocks, Ordering Note)
- Yêu cầu GDD được giải quyết (liên kết qua TR-ID)

ADRs trải qua vòng đời: Proposed > Accepted > Superseded/Deprecated.

**Tối thiểu 3 ADR ở tầng Foundation là bắt buộc** trước khi kiểm tra cổng.

**Bổ sung cho ADR cũ (Retrofitting):** Nếu bạn đã có ADR từ dự án cũ:

```
/architecture-decision retrofit docs/architecture/adr-005.md
```

Lệnh này phát hiện các phần còn thiếu so với template và chỉ bổ sung những phần đó, không bao giờ ghi đè nội dung hiện có.

### Bước 3.3: Đánh giá Kiến trúc

```
/architecture-review
```

Xác thực tất cả các ADR cùng nhau:
- Sắp xếp thứ tự topo các phụ thuộc ADR (phát hiện chu trình lặp)
- Xác minh độ tương thích với engine
- Đánh dấu sửa đổi GDD (GDD Revision Flags - đánh dấu các mục GDD cần cập nhật dựa trên quyết định ADR)
- Duy trì danh mục TR-ID (`docs/architecture/tr-registry.yaml`)

### Bước 3.4: Control Manifest

```
/create-control-manifest
```

Thu thập tất cả các ADR trạng thái Accepted và tạo ra bảng quy tắc ngắn gọn cho lập trình viên:

```
docs/architecture/control-manifest.md
```

Chứa các mẫu Bắt buộc (Required), mẫu Bị cấm (Forbidden) và Lan can bảo vệ (Guardrails) được tổ chức theo từng tầng code. Các story được tạo sau này sẽ nhúng ngày phiên bản manifest để phát hiện tính lỗi thời.

### Bước 3.5: Yêu cầu Accessibility

Tạo `design/accessibility-requirements.md` bằng cách dùng template. Cam kết một phân tầng (Basic / Standard / Comprehensive / Exemplary) và điền ma trận tính năng 4 trục (thị giác, vận động, nhận thức, thính giác).

Tài liệu này bắt buộc ở Giai đoạn 3 vì các đặc tả UX (viết trong Giai đoạn 4) sẽ tham chiếu phân tầng này.

### Cổng Giai đoạn 3 (Phase 3 Gate)

```
/gate-check technical-setup
```

**Yêu cầu để vượt qua:**

- `docs/architecture/architecture.md` đã tồn tại
- Ít nhất 3 ADR tồn tại và ở trạng thái Accepted
- Báo cáo đánh giá kiến trúc đã tồn tại
- `docs/architecture/control-manifest.md` đã tồn tại
- `design/accessibility-requirements.md` đã tồn tại

---

## Giai đoạn 4: Tiền sản xuất (Phase 4: Pre-Production)

### Những gì diễn ra trong giai đoạn này

Bạn tạo các đặc tả UX cho những màn hình chính, làm prototype cho các cơ chế rủi ro cao, biến tài liệu thiết kế thành các story có thể triển khai code, lên kế hoạch cho sprint đầu tiên, và xây dựng một Vertical Slice chứng minh vòng lặp cốt lõi thực sự vui nhộn.

### Pipeline Giai đoạn 4

```
/ux-design  -->  /vertical-slice  -->  /create-epics  -->  /create-stories  -->  /sprint-plan
    |                   |                   |                   |                       |
    v                   v                   v                   v                       v
  UX specs       Production-quality   Epic files in       Story files in          First sprint with
  design/ux/     end-to-end build     production/         production/             prioritized stories
                 in prototypes/       epics/*/EPIC.md     epics/*/story-*.md      production/sprints/
                 PROCEED/PIVOT/KILL   (one per module)    (one per behaviour)     sprint-*.md
    |                                                          |
    v                                                          v
 /ux-review                                             /story-readiness
 (validates specs                                       (validates each story
  before epics)                                          before pickup)
                                                               |
                                                               v
                                                           /dev-story
                                                         (implements the story,
                                                          routes to right agent)
```

### Bước 4.1: Đặc tả UX cho các màn hình chính

Trước khi viết epic, hãy tạo các đặc tả UX để tác giả story biết màn hình nào tồn tại và các tương tác của người chơi mà nó cần hỗ trợ.

**Đặc tả UX (UX Specs):**

```
/ux-design main-menu
/ux-design core-gameplay-hud
```

Ba chế độ: screen/flow, HUD, và interaction patterns. Đầu ra lưu vào `design/ux/`. Mỗi đặc tả bao gồm: nhu cầu người chơi, phân vùng layout, các trạng thái, sơ đồ tương tác, yêu cầu dữ liệu, sự kiện bắn ra, accessibility, đa ngôn ngữ localization.

Đọc `accessibility-requirements.md` và cấu hình input từ `technical-preferences.md` để tự động kiểm tra độ bao phủ.

**Thư viện Pattern tương tác (Interaction Pattern Library):**

```
/ux-design interaction-patterns
```

Tạo `design/ux/interaction-patterns.md` — 16 điều khiển tiêu chuẩn kèm các pattern đặc thù của game (ô túi đồ, icon kỹ năng, thanh HUD, khung hội thoại, v.v.) với tiêu chuẩn âm thanh và animation.

**Đánh giá UX (UX Review):**

```
/ux-review all
```

Xác thực đặc tả UX về sự phù hợp với GDD và mức độ accessibility. Đưa ra kết luận: APPROVED / NEEDS REVISION / MAJOR REVISION NEEDED.

### Bước 4.2: Xây dựng Vertical Slice

Vertical Slice là minh chứng đạt chất lượng sản xuất cho thấy bạn có thể xây dựng toàn bộ game loop hoàn chỉnh trước khi bước vào giai đoạn Sản xuất hàng loạt (Production).

```
/vertical-slice
```

**Mục tiêu chứng minh:** Một người chơi, bắt đầu từ con số 0, có trải nghiệm được cảm xúc cốt lõi trong vòng vài phút mà không cần lập trình viên hướng dẫn hay không?

**Sản phẩm tạo ra:** Một bản build chơi được có chất lượng gần với bản chính thức, bao gồm ít nhất một chu kỳ hoàn chỉnh [bắt đầu → thử thách → giải quyết]. Sử dụng các tầng kiến trúc thật, quy ước đặt tên thật, không hardcode giá trị — nhưng chưa cần art hay âm thanh hoàn chỉnh cuối cùng.

**Kết luận:** Vertical slice đưa ra kết luận PROCEED / PIVOT / KILL.
- **PROCEED** → chuyển sang Bước 4.3 (epics và stories)
- **PIVOT** → sửa đổi các GDD bị ảnh hưởng bằng `/design-system [mechanic]`, sau đó chạy lại `/vertical-slice`
- **KILL** → quay lại `/brainstorm` với những bài học đã rút ra

### Bước 4.3: Tạo Epics và Stories từ các tài liệu thiết kế

```
/create-epics layer: foundation
/create-stories [epic-slug]   # lặp lại cho từng epic
/create-epics layer: core
/create-stories [epic-slug]   # lặp lại cho từng core epic
```

`/create-epics` đọc GDD, ADR và kiến trúc để xác định phạm vi epic — mỗi module kiến trúc tương ứng một epic. Sau đó `/create-stories` chia nhỏ từng epic thành các file story có thể triển khai trong `production/epics/[slug]/`. Mỗi story nhúng:
- Tham chiếu yêu cầu GDD (mã TR-ID)
- Tham chiếu ADR (chỉ từ các ADR đã Accepted)
- Ngày phiên bản control manifest
- Ghi chú triển khai đặc thù theo engine
- Tiêu chí chấp nhận từ GDD

Khi đã có story, chạy `/dev-story [story-path]` để triển khai — hệ thống sẽ tự động điều phối tới đúng agent lập trình viên.

### Bước 4.4: Xác thực Story trước khi bắt đầu làm

```
/story-readiness production/epics/combat/story-combat-damage-calc.md
```

Kiểm tra: Tính hoàn thiện của thiết kế, Độ bao phủ kiến trúc, Độ rõ ràng của phạm vi, Định nghĩa hoàn thành (DoD). Kết luận: READY / NEEDS WORK / BLOCKED.

### Bước 4.5: Ước lượng khối lượng (Effort Estimation)

```
/estimate production/epics/combat/story-combat-damage-calc.md
```

Cung cấp ước lượng công sức kèm đánh giá rủi ro.

### Bước 4.6: Lên kế hoạch cho Sprint đầu tiên

```
/sprint-plan new
```

**Điều gì diễn ra:** Agent `producer` cộng tác lên kế hoạch sprint:
- Hỏi mục tiêu sprint và quỹ thời gian khả dụng
- Chia mục tiêu thành các tác vụ Bắt buộc có (Must Have) / Nên có (Should Have) / Có thì tốt (Nice to Have)
- Xác định rủi ro và các điểm nghẽn
- Tạo `production/sprints/sprint-01.md`
- Cập nhật `production/sprint-status.yaml` (theo dõi story dưới dạng máy đọc được)

### Bước 4.7: Vertical Slice (Cổng cứng - Hard Gate)

Trước khi tiến sang giai đoạn Production, bạn phải hoàn thành và chơi thử Vertical Slice:

- Một vòng lặp cốt lõi hoàn chỉnh end-to-end, chơi được từ đầu đến cuối
- Chất lượng mang tính đại diện (không phải toàn bộ là placeholder)
- Đã được chơi không cần hướng dẫn trong ít nhất 3 phiên
- Đã viết báo cáo chơi thử (`/playtest-report`)

Đây là một **cổng cứng (hard gate)** -- `/gate-check` sẽ tự động FAIL nếu chưa có người thật chơi thử bản build một cách độc lập.

### Cổng Giai đoạn 4 (Phase 4 Gate)

```
/gate-check pre-production
```

**Yêu cầu để vượt qua:**

- Ít nhất 1 đặc tả UX đã được đánh giá trong `design/ux/`
- Đánh giá UX đã hoàn tất (APPROVED hoặc NEEDS REVISION kèm rủi ro đã ghi nhận)
- Ít nhất 1 prototype kèm README
- Các file story đã tồn tại trong `production/epics/[epic-slug]/`
- Ít nhất 1 kế hoạch sprint đã tồn tại
- Ít nhất 1 báo cáo chơi thử tồn tại (Vertical Slice đã được chơi trong 3+ phiên)

---

## Giai đoạn 5: Sản xuất (Phase 5: Production)

### Những gì diễn ra trong giai đoạn này

Đây là vòng lặp sản xuất cốt lõi. Bạn làm việc theo các sprint (thường từ 1-2 tuần), triển khai các tính năng theo từng story, theo dõi tiến độ, và đóng story thông qua quy trình đánh giá hoàn thành có cấu trúc. Giai đoạn này lặp lại cho đến khi trò chơi hoàn thiện đầy đủ nội dung.

### Pipeline Giai đoạn 5 (Theo từng Sprint)

```
/sprint-plan new  -->  /story-readiness  -->  triển khai  -->  /story-done
       |                     |                    |                |
       v                     v                    v                v
  Tạo Sprint           Xác thực Story       Viết Code        Đánh giá 8 bước:
  Cập nhật             Kết luận READY       Vượt qua test    xác minh tiêu chí,
  sprint-status.yaml                                         kiểm tra sai lệch,
                                                             cập nhật trạng thái story
       |
       |  (lặp lại cho từng story đến khi xong sprint)
       v
  /sprint-status  (ảnh chụp nhanh 30 dòng bất cứ lúc nào)
  /scope-check    (nếu quy mô phình to)
  /retrospective  (ở cuối sprint)
```

### Bước 5.1: Vòng đời Story (The Story Lifecycle)

Giai đoạn sản xuất tập trung vào **vòng đời story**:

```
/story-readiness  -->  triển khai code  -->  /story-done  -->  story tiếp theo
```

**1. Sẵn sàng Story (Story Readiness):** Trước khi nhận story, hãy xác thực:

```
/story-readiness production/epics/combat/story-combat-damage-calc.md
```

Kiểm tra độ hoàn thiện thiết kế, độ bao phủ kiến trúc, trạng thái ADR, phiên bản control manifest, và độ rõ ràng của phạm vi. Kết luận: READY / NEEDS WORK / BLOCKED.

**2. Triển khai (Implementation):** Làm việc với các agent phù hợp:

- `gameplay-programmer` cho hệ thống gameplay
- `engine-programmer` cho phần lõi engine
- `ai-programmer` cho hành vi AI
- `network-programmer` cho multiplayer
- `ui-programmer` cho giao diện
- `tools-programmer` cho công cụ dev

Tất cả agent đều tuân theo giao thức cộng tác: đọc design doc, hỏi câu hỏi làm rõ, đưa ra lựa chọn kiến trúc, xin phê duyệt rồi mới viết code.

**3. Hoàn thành Story (Story Completion):** Khi một story đã làm xong:

```
/story-done production/epics/combat/story-combat-damage-calc.md
```

Chạy quy trình đánh giá hoàn thành 8 bước:
1. Tìm và đọc file story
2. Tải GDD, ADR và control manifest được tham chiếu
3. Xác minh tiêu chí chấp nhận (tự động kiểm tra, thủ công, hoãn lại)
4. Kiểm tra các sai lệch GDD/ADR (BLOCKING / ADVISORY / OUT OF SCOPE)
5. Nhắc nhở thực hiện code review
6. Tạo báo cáo hoàn thành (COMPLETE / COMPLETE WITH NOTES / BLOCKED)
7. Cập nhật story thành `Status: Complete` kèm ghi chú hoàn thành
8. Hiển thị story sẵn sàng tiếp theo

Nợ kỹ thuật (Tech debt) phát hiện trong khi review được ghi vào `docs/tech-debt-register.md`.

### Bước 5.2: Theo dõi Sprint

Kiểm tra tiến độ bất cứ lúc nào:

```
/sprint-status
```

Ảnh chụp nhanh 30 dòng đọc từ `production/sprint-status.yaml`.

Nếu quy mô công việc đang phình to:

```
/scope-check production/sprints/sprint-03.md
```

So sánh phạm vi hiện tại với kế hoạch ban đầu, cảnh báo việc tăng quy mô và khuyến nghị cắt giảm.

### Bước 5.3: Theo dõi nội dung

```
/content-audit
```

So sánh nội dung chỉ định trong GDD với những gì đã thực sự được triển khai. Phát hiện sớm các khoảng trống nội dung.

### Bước 5.4: Lan truyền thay đổi thiết kế

Khi GDD thay đổi sau khi story đã được tạo:

```
/propagate-design-change design/gdd/combat-system.md
```

So sánh git-diff GDD, tìm các ADR bị ảnh hưởng, tạo báo cáo tác động và hướng dẫn bạn đưa ra quyết định thay thế (Superseded) / cập nhật / giữ nguyên.

### Bước 5.5: Tính năng đa hệ thống (Điều phối nhóm - Team Orchestration)

Đối với các tính năng trải rộng trên nhiều lĩnh vực, sử dụng các team skill:

```
/team-combat "kỹ năng hồi máu kèm HoT và giải hiệu ứng xấu"
/team-narrative "Nội dung cốt truyện Hồi 2"
/team-ui "thiết kế lại màn hình túi đồ"
/team-level "màn chơi dungeon trong rừng"
/team-audio "hoàn thiện âm thanh chiến đấu"
```

Mỗi team skill điều phối quy trình cộng tác 6 giai đoạn:
1. **Design** -- game-designer đặt câu hỏi, đưa ra lựa chọn
2. **Architecture** -- lead-programmer đề xuất cấu trúc code
3. **Parallel Implementation** -- các chuyên viên làm việc song song
4. **Integration** -- gameplay-programmer kết nối mọi thứ lại
5. **Validation** -- qa-tester chạy kiểm tra tiêu chí chấp nhận
6. **Report** -- coordinator tóm tắt trạng thái

Việc điều phối diễn ra tự động, nhưng **các điểm quyết định luôn thuộc về bạn**.

### Bước 5.6: Tổng kết Sprint và Sprint tiếp theo

Ở cuối mỗi sprint:

```
/retrospective
```

Phân tích kế hoạch so với thực tế hoàn thành, vận tốc (velocity), các điểm nghẽn và cải tiến hành động.

Sau đó lên kế hoạch sprint tiếp theo:

```
/sprint-plan new
```

### Bước 5.7: Đánh giá Milestone

Tại các điểm kiểm tra milestone:

```
/milestone-review "alpha"
```

Đưa ra mức độ hoàn thiện tính năng, chỉ số chất lượng, đánh giá rủi ro và khuyến nghị go/no-go.

### Cổng Giai đoạn 5 (Phase 5 Gate)

```
/gate-check production
```

**Yêu cầu để vượt qua:**

- Tất cả các story MVP đã hoàn thành
- Chơi thử: 3 phiên bao gồm trải nghiệm người chơi mới, giữa game, và đường cong độ khó
- Giả thuyết trải nghiệm vui (Fun hypothesis) đã được kiểm chứng
- Không có vòng lặp gây bối rối trong dữ liệu chơi thử

---

## Giai đoạn 6: Đánh bóng (Phase 6: Polish)

### Những gì diễn ra trong giai đoạn này

Trò chơi của bạn đã hoàn thiện về tính năng (feature-complete). Bây giờ là lúc làm cho nó thật sự xuất sắc. Giai đoạn này tập trung vào hiệu năng, cân bằng, accessibility, âm thanh, trau chuốt hình ảnh và chơi thử diện rộng.

### Pipeline Giai đoạn 6

```
/perf-profile  -->  /balance-check  -->  /asset-audit  -->  /playtest-report (x3)
       |                  |                    |                    |
       v                  v                    v                    v
  Profile CPU/GPU    Phân tích công       Xác minh quy ước     Bao phủ: người chơi mới,
  bộ nhớ, tối ưu     thức & dữ liệu       đặt tên, định dạng,  giữa game, đường cong
  điểm nghẽn         tìm mất cân bằng     dung lượng           độ khó

  /tech-debt  -->  /team-polish
       |                |
       v                v
  Theo dõi & ưu    Đợt trau chuốt phối hợp:
  tiên hóa nợ      hiệu năng + art +
  kỹ thuật         âm thanh + UX + QA
```

### Bước 6.1: Đo kiểm hiệu năng (Performance Profiling)

```
/perf-profile
```

Hướng dẫn bạn qua quy trình profiling hiệu năng có cấu trúc:
- Thiết lập mục tiêu (FPS, dung lượng RAM, nền tảng)
- Xác định điểm nghẽn xếp hạng theo mức độ ảnh hưởng
- Tạo các tác vụ tối ưu hóa cụ thể kèm vị trí code và mức tăng hiệu năng kỳ vọng

### Bước 6.2: Phân tích cân bằng (Balance Analysis)

```
/balance-check assets/data/combat_damage.json
```

Phân tích dữ liệu cân bằng để tìm các điểm ngoại lai thống kê, đường cong tiến trình bị hỏng, chiến lược tiêu cực và mất cân bằng kinh tế.

### Bước 6.3: Audit tài nguyên (Asset Audit)

```
/asset-audit
```

Xác minh quy ước đặt tên, tiêu chuẩn định dạng file và giới hạn dung lượng trên toàn bộ asset.

### Bước 6.4: Chơi thử (Bắt buộc: 3 phiên)

```
/playtest-report
```

Tạo các báo cáo chơi thử có cấu trúc. Yêu cầu đủ 3 phiên bao quát:
- Trải nghiệm người chơi mới
- Hệ thống giai đoạn giữa game
- Đường cong độ khó

### Bước 6.5: Đánh giá nợ kỹ thuật

```
/tech-debt
```

Quét các comment TODO/FIXME/HACK, trùng lặp code, hàm quá phức tạp, thiếu test và dependencies lỗi thời. Mỗi mục được phân loại và sắp xếp thứ tự ưu tiên.

### Bước 6.6: Đợt đánh bóng phối hợp (Coordinated Polish Pass)

```
/team-polish "hệ thống chiến đấu"
```

Điều phối 4 chuyên viên song song:
1. Tối ưu hiệu năng (`performance-analyst`)
2. Trau chuốt hình ảnh (`technical-artist`)
3. Trau chuốt âm thanh (`sound-designer`)
4. Cảm giác game feel / juice (`gameplay-programmer` + `technical-artist`)

Bạn đặt ưu tiên; nhóm thực thi kèm theo sự phê duyệt của bạn ở từng bước.

### Bước 6.7: Đa ngôn ngữ và Accessibility

```
/localize src/
```

Quét các chuỗi văn bản bị hardcode, nối chuỗi làm hỏng bản dịch, văn bản không tính đến việc mở rộng độ dài khi dịch, và các file ngôn ngữ còn thiếu.

Accessibility được kiểm tra đối chiếu phân tầng đã cam kết ở Giai đoạn 3.

### Cổng Giai đoạn 6 (Phase 6 Gate)

```
/gate-check polish
```

**Yêu cầu để vượt qua:**

- Ít nhất 3 báo cáo chơi thử đã tồn tại
- Đợt đánh bóng phối hợp đã hoàn tất (`/team-polish`)
- Không có lỗi hiệu năng nghiêm trọng gây nghẽn
- Đạt đầy đủ các yêu cầu phân tầng accessibility

---

## Giai đoạn 7: Phát hành (Phase 7: Release)

### Những gì diễn ra trong giai đoạn này

Trò chơi đã được trau chuốt, kiểm thử kỹ lưỡng và sẵn sàng. Bây giờ là lúc phát hành ra thị trường.

### Pipeline Giai đoạn 7

```
/release-checklist  -->  /launch-checklist  -->  /team-release
        |                       |                      |
        v                       v                      v
  Xác thực tiền           Xác thực liên phòng      Điều phối:
  phát hành trên code,    ban đầy đủ (Go/No-Go     bản build, QA duyệt,
  nội dung, store, pháp lý cho từng phòng ban)     triển khai, phát hành
                    Ngoài ra: /changelog, /patch-notes, /hotfix
```

### Bước 7.1: Checklist phát hành

```
/release-checklist v1.0.0
```

Tạo danh sách kiểm tra tiền phát hành toàn diện bao gồm:
- Xác minh bản build (tất cả các nền tảng đều compile và chạy tốt)
- Yêu cầu chứng nhận Certification (theo từng nền tảng console/store)
- Dữ liệu Store (mô tả, ảnh chụp màn hình, trailer)
- Tuân thủ pháp lý (EULA, chính sách quyền riêng tư, phân loại độ tuổi)
- Tính tương thích của file lưu game (Save game compatibility)
- Xác minh hệ thống Analytics

### Bước 7.2: Sẵn sàng ra mắt (Xác thực toàn diện)

```
/launch-checklist
```

Xác thực đầy đủ xuyên suốt các phòng ban:

| Phòng ban | Những gì được kiểm tra |
|---|---|
| **Kỹ thuật (Engineering)** | Độ ổn định bản build, tỷ lệ crash, rò rỉ bộ nhớ, thời gian load |
| **Thiết kế (Design)** | Tính hoàn thiện tính năng, luồng hướng dẫn tutorial, đường cong độ khó |
| **Mỹ thuật (Art)** | Chất lượng asset, texture bị thiếu, các mức LOD |
| **Âm thanh (Audio)** | Âm thanh bị thiếu, mức cân bằng mixing, âm thanh không gian |
| **QA** | Số lượng bug mở theo mức độ nghiêm trọng, tỷ lệ pass bộ test hồi quy |
| **Cốt truyện (Narrative)** | Độ hoàn thiện hội thoại, tính nhất quán của lore, lỗi chính tả |
| **Đa ngôn ngữ (Localization)** | Tất cả chuỗi đã dịch, không bị tràn/cắt chữ, kiểm thử từng ngôn ngữ |
| **Accessibility** | Checklist tuân thủ, kiểm thử các tính năng hỗ trợ |
| **Store** | Metadata đầy đủ, screenshot đã duyệt, giá bán đã thiết lập |
| **Marketing** | Press kit sẵn sàng, trailer ra mắt, lịch đăng mạng xã hội |
| **Cộng đồng (Community)** | Bản thảo Patch notes, chuẩn bị FAQ, các kênh hỗ trợ sẵn sàng |
| **Hạ tầng (Infrastructure)** | Máy chủ đã mở rộng scale, CDN đã cấu hình, hệ thống giám sát hoạt động |
| **Pháp lý (Legal)** | EULA hoàn thiện, chính sách quyền riêng tư, tuân thủ COPPA/GDPR |

Mỗi mục nhận trạng thái **Go / No-Go**. Tất cả phải đạt Go mới được phát hành.

### Bước 7.3: Tạo nội dung hướng tới người chơi

```
/patch-notes v1.0.0
```

Tạo patch notes thân thiện với người chơi từ lịch sử git và dữ liệu sprint. Dịch ngôn ngữ lập trình viên sang ngôn ngữ game thủ.

```
/changelog v1.0.0
```

Tạo changelog nội bộ (thiên về kỹ thuật, dành cho đội ngũ phát triển).

### Bước 7.4: Điều phối phát hành

```
/team-release
```

Điều phối `release-manager`, QA, và DevOps qua:
1. Xác thực tiền phát hành
2. Quản lý bản build
3. Ký duyệt QA cuối cùng
4. Chuẩn bị triển khai
5. Quyết định Go/No-Go

### Bước 7.5: Phát hành game (Ship)

Hook `validate-push` sẽ cảnh báo bạn khi push vào nhánh `main` hoặc `develop`. Điều này là chủ ý -- việc push phát hành phải được thực hiện có chủ đích:

```bash
git tag v1.0.0
git push origin main --tags
```

### Bước 7.6: Sau phát hành (Post-Launch)

**Workflow Hotfix** cho các lỗi nghiêm trọng trên production:

```
/hotfix "Người chơi bị mất dữ liệu lưu game khi túi đồ vượt quá 99 vật phẩm"
```

Bỏ qua các quy trình sprint thông thường kèm audit trail đầy đủ:
1. Tạo nhánh hotfix
2. Triển khai bản sửa lỗi
3. Đảm bảo backport ngược lại vào nhánh phát triển
4. Ghi nhận tài liệu sự cố

**Báo cáo tổng kết sau dự án (Post-mortem)** sau khi đợt ra mắt đã ổn định:

```
Yêu cầu Antigravity tạo tài liệu post-mortem bằng cách sử dụng template tại
docs/reference/templates/post-mortem.md
```

---

## Các vấn đề xuyên suốt (Cross-Cutting Concerns)

Các chủ đề này áp dụng xuyên suốt tất cả các giai đoạn.

### Các chế độ đánh giá của Director (Director Review Modes)

Director gates là các agent chuyên gia đánh giá công việc của bạn tại các bước workflow quan trọng. Theo mặc định, chúng chạy ở mọi điểm kiểm tra. Bạn có thể kiểm soát mức độ đánh giá mình muốn nhận.

**Thiết lập mức độ đánh giá một lần trong khi chạy `/start`.** Được lưu vào `production/review-mode.txt`.

| Chế độ | Những gì chạy | Phù hợp nhất cho |
|---|---|---|
| `full` | Tất cả các cổng director ở mọi bước | Dự án mới, người mới học hệ thống |
| `lean` | Director chỉ chạy ở các điểm chuyển giai đoạn (`/gate-check`) | Lập trình viên có kinh nghiệm |
| `solo` | Không có đánh giá từ director | Game jams, prototypes, tốc độ tối đa |

**Ghi đè cho một lần chạy duy nhất** mà không thay đổi cài đặt toàn cục của bạn:

```
/brainstorm space horror --review full
/architecture-decision --review solo
```

Cờ `--review` hoạt động trên tất cả các skill có dùng cổng kiểm duyệt. Thay đổi chế độ toàn cục bất cứ lúc nào bằng cách sửa trực tiếp `production/review-mode.txt` hoặc chạy lại `/start`.

Chi tiết định nghĩa các cổng: `docs/reference/director-gates.md`

---

### Giao thức cộng tác (The Collaboration Protocol)

Hệ thống này là **cộng tác định hướng bởi người dùng (user-driven collaborative)**, không phải tự động đơn phương.

**Mô hình:** Hỏi > Lựa chọn > Quyết định > Bản thảo > Phê duyệt (Question > Options > Decision > Draft > Approval)

Mọi tương tác của agent đều tuân theo mô hình:
1. Agent đặt câu hỏi làm rõ
2. Agent đưa ra 2-4 lựa chọn kèm các đánh đổi và lập luận
3. Bạn đưa ra quyết định
4. Agent soạn thảo bản thảo dựa trên quyết định của bạn
5. Bạn đánh giá và tinh chỉnh
6. Agent hỏi "Tôi có thể ghi nội dung này vào [filepath] không?" trước khi ghi file

Xem `docs/COLLABORATIVE-DESIGN-PRINCIPLE.md` để biết toàn bộ giao thức kèm ví dụ.

### Công cụ AskUserQuestion

Agent sử dụng công cụ `AskUserQuestion` để trình bày lựa chọn có cấu trúc. Mô hình là Giải thích trước rồi mới Ghi nhận (Explain then Capture): phân tích đầy đủ trong văn bản hội thoại trước, sau đó dùng bộ chọn UI gọn gàng để chốt quyết định. Dùng cho các lựa chọn thiết kế, quyết định kiến trúc và các câu hỏi chiến lược. Không dùng cho các câu hỏi khám phá mở hoặc xác nhận có/không đơn giản.

### Phối hợp Agent (Hệ thống phân cấp 3 tầng)

```
Tier 1 (Giám đốc - Directors):    creative-director, technical-director, producer
                                          |
Tier 2 (Trưởng bộ phận - Leads):  game-designer, lead-programmer, art-director,
                                  audio-director, narrative-director, qa-lead,
                                  release-manager, localization-lead
                                          |
Tier 3 (Chuyên viên - Specialists): gameplay-programmer, engine-programmer,
                                  ai-programmer, network-programmer, ui-programmer,
                                  tools-programmer, systems-designer, level-designer,
                                  economy-designer, world-builder, writer,
                                  technical-artist, sound-designer, ux-designer,
                                  qa-tester, performance-analyst, devops-engineer,
                                  analytics-engineer, accessibility-specialist,
                                  live-ops-designer, prototyper, security-engineer,
                                  community-manager, godot-specialist,
                                  godot-gdscript-specialist, godot-shader-specialist,
                                  godot-csharp-specialist, godot-gdextension-specialist,
                                  unity-specialist, unity-dots-specialist,
                                  unity-shader-specialist, unity-addressables-specialist,
                                  unity-ui-specialist, unreal-specialist,
                                  ue-blueprint-specialist, ue-gas-specialist,
                                  ue-replication-specialist, ue-umg-specialist
```

**Quy tắc điều phối:**
- Phân quyền theo chiều dọc: Giám đốc > Trưởng bộ phận > Chuyên viên. Không bao giờ nhảy cóc tầng đối với các quyết định phức tạp.
- Tham vấn theo chiều ngang: Các agent cùng tầng có thể tham vấn nhau nhưng không được đưa ra quyết định ràng buộc ngoài phạm vi của mình.
- Giải quyết xung đột: Xung đột thiết kế chuyển lên `creative-director`. Xung đột kỹ thuật chuyển lên `technical-director`. Xung đột quy mô chuyển lên `producer`.
- Không tự ý thực hiện các thay đổi chéo lĩnh vực một cách đơn phương.

### Hooks tự động hóa (Lưới an toàn)

Hệ thống có 12 hook tự động chạy:

| Hook | Điều kiện kích hoạt | Chức năng |
|---|---|---|
| `session-start.sh` | Bắt đầu phiên | Hiển thị branch, các commit gần đây, phát hiện active.md để phục hồi |
| `detect-gaps.sh` | Bắt đầu phiên | Phát hiện dự án mới (chưa có engine, chưa có concept) và gợi ý `/start` |
| `pre-compact.sh` | Trước khi compact | Đưa trạng thái phiên vào hội thoại để tự động phục hồi |
| `post-compact.sh` | Sau khi compact | Nhắc nhở Antigravity khôi phục trạng thái phiên từ `active.md` |
| `notify.sh` | Sự kiện thông báo | Hiển thị thông báo Windows toast qua PowerShell |
| `validate-commit.sh` | Trước khi commit | Kiểm tra tham chiếu design doc, tính hợp lệ JSON, không hardcode |
| `validate-push.sh` | Trước khi push | Cảnh báo khi push vào main/develop |
| `validate-assets.sh` | Trước khi commit | Kiểm tra đặt tên và dung lượng asset |
| `validate-skill-change.sh` | Ghi file skill | Khuyến nghị chạy `/skill-test` sau khi sửa `.agents/skills/` |
| `log-agent.sh` | Agent khởi động | Ghi log quá trình gọi agent phục vụ audit trail |
| `log-agent-stop.sh` | Agent kết thúc | Hoàn tất audit trail của agent (bắt đầu + kết thúc) |
| `session-stop.sh` | Đóng phiên | Ghi log phiên làm việc lần cuối |

### Khả năng phục hồi ngữ cảnh (Context Resilience)

**File trạng thái phiên làm việc:** `production/session-state/active.md` là một checkpoint sống. Cập nhật nó sau mỗi milestone quan trọng. Sau bất kỳ gián đoạn nào (compaction, crash, `/clear`), hãy đọc file này trước tiên.

**Ghi file tăng dần (Incremental writing):** Khi tạo tài liệu nhiều phần, hãy ghi từng phần vào file ngay sau khi được duyệt. Điều này giúp các phần đã hoàn thành tồn tại qua các sự cố crash và context compaction. Các thảo luận trước đó về những phần đã ghi có thể được compact an toàn.

**Tự động phục hồi:** Hook `session-start.sh` tự động phát hiện và xem trước `active.md`. Hook `pre-compact.sh` đưa trạng thái vào hội thoại trước khi compaction diễn ra.

**Theo dõi trạng thái Sprint:** `production/sprint-status.yaml` là công cụ theo dõi story dạng máy đọc được. Được ghi bởi `/sprint-plan` (khởi tạo) và `/story-done` (cập nhật trạng thái). Được đọc bởi `/sprint-status`, `/help`, và `/story-done` (story tiếp theo). Loại bỏ hoàn toàn việc quét markdown mong manh.

### Tiếp nhận dự án có sẵn (Brownfield Adoption)

Đối với các dự án hiện có đã có sẵn một số tài liệu/code:

```
/adopt
```

Hoặc nhắm mục tiêu:

```
/adopt gdds
/adopt adrs
/adopt stories
/adopt infra
```

Lệnh này audit các sản phẩm hiện có về **định dạng** (không phải sự tồn tại), phân loại khoảng trống thành BLOCKING/HIGH/MEDIUM/LOW, xây dựng kế hoạch migration có thứ tự, và ghi ra `docs/adoption-plan-[date].md`. Nguyên tắc cốt lõi: MIGRATION chứ không phải REPLACEMENT -- không bao giờ tạo lại công việc hiện có, chỉ bổ sung các phần còn thiếu.

Các skill riêng lẻ cũng hỗ trợ chế độ retrofit:

```
/design-system retrofit design/gdd/combat-system.md
/architecture-decision retrofit docs/architecture/adr-005.md
```

### Hệ thống Cổng (Gate System)

Các cổng giai đoạn là những điểm kiểm tra chính thức. Chạy `/gate-check` kèm theo tên bước chuyển đổi:

```
/gate-check concept              # Concept -> Systems Design
/gate-check systems-design       # Systems Design -> Technical Setup
/gate-check technical-setup      # Technical Setup -> Pre-Production
/gate-check pre-production       # Pre-Production -> Production
/gate-check production           # Production -> Polish
/gate-check polish               # Polish -> Release
```

**Kết luận:**
- **PASS** -- đạt đủ tất cả yêu cầu, chuyển sang giai đoạn tiếp theo
- **CONCERNS** -- đạt yêu cầu kèm rủi ro đã ghi nhận, được phép vượt qua
- **FAIL** -- không đạt yêu cầu, chặn chuyển giai đoạn kèm chỉ dẫn khắc phục cụ thể

Khi một cổng PASS, file `production/stage.txt` sẽ được cập nhật (chỉ khi đó), điều khiển status line và hành vi của `/help`.

### Tài liệu hóa ngược (Reverse Documentation)

Đối với code đã tồn tại nhưng chưa có design docs (thường thấy sau khi tiếp nhận dự án cũ):

```
/reverse-document src/gameplay/combat/
```

Đọc code hiện có và tạo ra tài liệu thiết kế chuẩn định dạng GDD từ code đó.

---

## Phụ lục A: Tra cứu nhanh Agent

### "Tôi cần làm việc X -- tôi nên dùng Agent nào?"

| Tôi cần... | Agent | Tầng (Tier) |
|---|---|---|
| Nghĩ ý tưởng game | `/brainstorm` skill | -- |
| Thiết kế một cơ chế game | `game-designer` | 2 |
| Thiết kế công thức/số liệu cụ thể | `systems-designer` | 3 |
| Thiết kế màn chơi | `level-designer` | 3 |
| Thiết kế bảng loot / kinh tế | `economy-designer` | 3 |
| Xây dựng thế giới lore | `world-builder` | 3 |
| Viết lời thoại | `writer` | 3 |
| Lên kế hoạch cốt truyện | `narrative-director` | 2 |
| Lên kế hoạch sprint | `producer` | 1 |
| Đưa ra quyết định sáng tạo | `creative-director` | 1 |
| Đưa ra quyết định kỹ thuật | `technical-director` | 1 |
| Triển khai code gameplay | `gameplay-programmer` | 3 |
| Triển khai hệ thống lõi engine | `engine-programmer` | 3 |
| Triển khai hành vi AI | `ai-programmer` | 3 |
| Triển khai multiplayer | `network-programmer` | 3 |
| Triển khai UI | `ui-programmer` | 3 |
| Xây dựng công cụ dev | `tools-programmer` | 3 |
| Đánh giá kiến trúc code | `lead-programmer` | 2 |
| Tạo shader / VFX | `technical-artist` | 3 |
| Định hình phong cách hình ảnh | `art-director` | 2 |
| Định hình phong cách âm thanh | `audio-director` | 2 |
| Thiết kế hiệu ứng âm thanh | `sound-designer` | 3 |
| Thiết kế luồng UX | `ux-designer` | 3 |
| Viết test cases | `qa-tester` | 3 |
| Lên chiến lược kiểm thử | `qa-lead` | 2 |
| Đo kiểm hiệu năng (Profile) | `performance-analyst` | 3 |
| Thiết lập CI/CD | `devops-engineer` | 3 |
| Thiết kế analytics | `analytics-engineer` | 3 |
| Kiểm tra accessibility | `accessibility-specialist` | 3 |
| Lên kế hoạch live operations | `live-ops-designer` | 3 |
| Quản lý phát hành | `release-manager` | 2 |
| Quản lý đa ngôn ngữ | `localization-lead` | 2 |
| Tạo prototype nhanh | `prototyper` | 3 |
| Kiểm toán bảo mật | `security-engineer` | 3 |
| Giao tiếp với người chơi | `community-manager` | 3 |
| Trợ giúp chuyên sâu Godot | `godot-specialist` | 3 |
| Trợ giúp chuyên sâu GDScript | `godot-gdscript-specialist` | 3 |
| Trợ giúp Shader Godot | `godot-shader-specialist` | 3 |
| Module GDExtension | `godot-gdextension-specialist` | 3 |
| Trợ giúp chuyên sâu Unity | `unity-specialist` | 3 |
| Unity DOTS/ECS | `unity-dots-specialist` | 3 |
| Unity Shaders/VFX | `unity-shader-specialist` | 3 |
| Unity Addressables | `unity-addressables-specialist` | 3 |
| Unity UI Toolkit | `unity-ui-specialist` | 3 |
| Trợ giúp chuyên sâu Unreal | `unreal-specialist` | 3 |
| Unreal GAS | `ue-gas-specialist` | 3 |
| Unreal Blueprints | `ue-blueprint-specialist` | 3 |
| Unreal Replication | `ue-replication-specialist` | 3 |
| Unreal UMG/CommonUI | `ue-umg-specialist` | 3 |

### Sơ đồ phân cấp Agent

```
                    creative-director / technical-director / producer
                                         |
          ---------------------------------------------------------------
          |            |           |           |          |        |       |
    game-designer  lead-prog  art-dir  audio-dir  narr-dir  qa-lead  release-mgr
          |            |           |           |          |        |        |
     specialists  programmers  tech-art  snd-design  writer   qa-tester  devops
     (systems,    (gameplay,             (sound)     (world-  (perf,     (analytics,
      economy,     engine,                           builder)  access.)   security)
      level)       ai, net,
                   ui, tools)
```

**Quy tắc báo cáo (Escalation rule):** Nếu hai agent bất đồng, chuyển lên cấp trên. Xung đột thiết kế lên `creative-director`. Xung đột kỹ thuật lên `technical-director`. Xung đột quy mô lên `producer`.

---

## Phụ lục B: Tra cứu nhanh Slash Command

### Tất cả 73 lệnh theo danh mục

#### Onboarding và Điều hướng (6)

| Lệnh | Mục đích | Giai đoạn |
|---|---|---|
| `/start` | Onboarding có hướng dẫn, điều hướng tới đúng workflow | Mọi giai đoạn (phiên đầu) |
| `/help` | Nhận biết ngữ cảnh "tôi cần làm gì tiếp theo?" | Mọi giai đoạn |
| `/project-stage-detect` | Audit toàn diện dự án để xác định giai đoạn hiện tại | Mọi giai đoạn |
| `/setup-engine` | Cấu hình engine, ghim phiên bản, thiết lập tùy chọn | 1 |
| `/adopt` | Audit và kế hoạch migration cho dự án cũ | Mọi giai đoạn (dự án có sẵn) |
| `/skill-improve` | Cải tiến skill qua vòng lặp test-fix-retest | Mọi giai đoạn |

#### Game Design (6)

| Lệnh | Mục đích | Giai đoạn |
|---|---|---|
| `/brainstorm` | Tư duy ý tưởng cộng tác kèm phân tích MDA | 1 |
| `/map-systems` | Phân rã concept thành chỉ mục các hệ thống | 1-2 |
| `/design-system` | Soạn thảo GDD từng phần có hướng dẫn | 2 |
| `/quick-design` | Đặc tả tinh gọn cho các thay đổi nhỏ | 2+ |
| `/review-all-gdds` | Đánh giá tính nhất quán chéo và lý thuyết thiết kế | 2 |
| `/propagate-design-change` | Tìm các ADR/story bị ảnh hưởng khi GDD thay đổi | 5 |

#### UX và Giao diện (2)

| Lệnh | Mục đích | Giai đoạn |
|---|---|---|
| `/ux-design` | Soạn thảo đặc tả UX (màn hình/luồng, HUD, patterns) | 4 |
| `/ux-review` | Xác thực đặc tả UX về accessibility và độ khớp GDD | 4 |

#### Kiến trúc (4)

| Lệnh | Mục đích | Giai đoạn |
|---|---|---|
| `/create-architecture` | Tài liệu kiến trúc tổng thể | 3 |
| `/architecture-decision` | Tạo hoặc bổ sung một ADR | 3 |
| `/architecture-review` | Xác thực tất cả các ADR, thứ tự phụ thuộc | 3 |
| `/create-control-manifest` | Tạo quy tắc lập trình viên từ các ADR đã duyệt | 3 |

#### Stories và Sprints (8)

| Lệnh | Mục đích | Giai đoạn |
|---|---|---|
| `/create-epics` | Chuyển đổi GDDs + ADRs thành epics (mỗi module một epic) | 4 |
| `/create-stories` | Chia nhỏ một epic thành các file story | 4 |
| `/dev-story` | Triển khai một story — tự điều hướng tới đúng agent lập trình | 5 |
| `/sprint-plan` | Tạo hoặc quản lý kế hoạch sprint | 4-5 |
| `/sprint-status` | Xem nhanh ảnh chụp trạng thái sprint 30 dòng | 5 |
| `/story-readiness` | Xác thực story đã sẵn sàng để code | 4-5 |
| `/story-done` | Đánh giá hoàn thành story 8 bước | 5 |
| `/estimate` | Ước lượng công sức kèm đánh giá rủi ro | 4-5 |

#### Đánh giá và Phân tích (13)

| Lệnh | Mục đích | Giai đoạn |
|---|---|---|
| `/design-review` | Xác thực GDD theo chuẩn 8 phần | 1-2 |
| `/code-review` | Đánh giá kiến trúc code | 5+ |
| `/balance-check` | Phân tích công thức cân bằng game | 5-6 |
| `/asset-audit` | Xác minh đặt tên, định dạng, dung lượng asset | 6 |
| `/asset-spec` | Đặc tả thị giác và prompt sinh AI cho từng asset | 5-6 |
| `/content-audit` | Nội dung chỉ định trong GDD vs thực tế đã code | 5 |
| `/consistency-check` | Quét tính không nhất quán của thực thể và công thức | 2+ |
| `/scope-check` | Phát hiện phình to quy mô (scope creep) | 5 |
| `/perf-profile` | Quy trình đo kiểm và tối ưu hiệu năng | 6 |
| `/tech-debt` | Quét và sắp xếp thứ tự ưu tiên nợ kỹ thuật | 6 |
| `/gate-check` | Cổng kiểm soát giai đoạn chính thức với PASS/CONCERNS/FAIL | Tất cả các chuyển giao |
| `/reverse-document` | Tạo tài liệu thiết kế từ code hiện có | Mọi giai đoạn |
| `/security-audit` | Kiểm toán lỗ hổng bảo mật (save game, network, input) | 6-7 |

#### QA và Kiểm thử (9)

| Lệnh | Mục đích | Giai đoạn |
|---|---|---|
| `/qa-plan` | Tạo kế hoạch kiểm thử QA cho sprint hoặc tính năng | 5 |
| `/smoke-check` | Cổng smoke test luồng quan trọng trước khi giao QA | 5-6 |
| `/soak-test` | Quy trình soak test cho các phiên chơi kéo dài | 6 |
| `/regression-suite` | Lập sơ đồ test coverage, tìm bug đã sửa thiếu test | 5-6 |
| `/test-setup` | Dựng khung test và pipeline CI/CD | 4 |
| `/test-helpers` | Tạo các thư viện helper test đặc thù cho engine | 4-5 |
| `/test-evidence-review` | Đánh giá chất lượng file test và tài liệu bằng chứng | 5 |
| `/test-flakiness` | Phát hiện test chập chờn từ log CI | 5-6 |
| `/skill-test` | Xác thực file skill về cấu trúc và hành vi | Mọi giai đoạn |

#### Quản lý Sản xuất (6)

| Lệnh | Mục đích | Giai đoạn |
|---|---|---|
| `/milestone-review` | Tiến độ milestone và quyết định go/no-go | 5 |
| `/retrospective` | Phân tích tổng kết sprint (retrospective) | 5 |
| `/bug-report` | Tạo báo cáo bug có cấu trúc | 5+ |
| `/bug-triage` | Đánh giá lại bug mở về độ ưu tiên, nghiêm trọng và người phụ trách | 5+ |
| `/playtest-report` | Báo cáo phiên chơi thử có cấu trúc | 4-6 |
| `/onboard` | Onboarding thành viên mới vào nhóm | Mọi giai đoạn |

#### Phát hành (6)

| Lệnh | Mục đích | Giai đoạn |
|---|---|---|
| `/release-checklist` | Xác thực tiền phát hành | 7 |
| `/launch-checklist` | Đánh giá sẵn sàng ra mắt liên phòng ban đầy đủ | 7 |
| `/changelog` | Tự động tạo changelog nội bộ | 7 |
| `/patch-notes` | Tạo patch notes hướng tới người chơi | 7 |
| `/hotfix` | Quy trình sửa lỗi khẩn cấp | 7+ |
| `/day-one-patch` | Bản vá có phạm vi cho các lỗi phát hiện sau bản gold master | 7+ |

#### Sáng tạo (4)

| Lệnh | Mục đích | Giai đoạn |
|---|---|---|
| `/prototype` | Prototype concept — kiểm chứng ý tưởng cốt lõi trước GDD | 1 |
| `/art-bible` | Soạn thảo Art Bible có hướng dẫn — đặc tả nhận diện thị giác | 1-2 |
| `/vertical-slice` | Bản build hoàn chỉnh chất lượng sản xuất trước Production | 4 |
| `/localize` | Trích xuất và xác thực chuỗi đa ngôn ngữ | 6-7 |

#### Phối hợp nhóm (Team Orchestration) (9)

| Lệnh | Mục đích | Giai đoạn |
|---|---|---|
| `/team-combat` | Tính năng chiến đấu: từ thiết kế đến triển khai code | 5 |
| `/team-narrative` | Nội dung cốt truyện: từ cấu trúc đến lời thoại | 5 |
| `/team-ui` | Tính năng UI: từ đặc tả UX đến triển khai hoàn thiện | 5 |
| `/team-level` | Màn chơi: từ bố cục layout đến sắp đặt chạm trán | 5 |
| `/team-audio` | Âm thanh: từ định hướng đến tích hợp sự kiện | 5-6 |
| `/team-polish` | Đánh bóng phối hợp: hiệu năng + art + audio + QA | 6 |
| `/team-release` | Phối hợp phát hành: build + QA + triển khai | 7 |
| `/team-live-ops` | Kế hoạch live-ops: sự kiện mùa, battle pass, giữ chân người chơi | 7+ |
| `/team-qa` | Chu kỳ QA đầy đủ: chiến lược, thực thi, bao phủ, ký duyệt | 6-7 |

---

## Phụ lục C: Các Workflow phổ biến

### Workflow 1: "Tôi vừa mới bắt đầu và chưa có ý tưởng game"

```
1. /start (điều hướng bạn dựa trên tình trạng hiện tại)
2. /brainstorm (tư duy cộng tác, chọn một concept)
3. /setup-engine (ghim engine và phiên bản)
4. /design-review trên concept doc (tùy chọn, khuyến nghị)
5. /map-systems (phân rã concept thành các hệ thống kèm phụ thuộc và ưu tiên)
6. /gate-check concept (xác minh bạn đã sẵn sàng cho Thiết kế Hệ thống)
7. /design-system cho từng hệ thống (soạn thảo GDD có hướng dẫn)
```

### Workflow 2: "Tôi đã có thiết kế và muốn bắt đầu viết code"

```
1. /design-review trên từng GDD (đảm bảo thiết kế vững chắc)
2. /review-all-gdds (kiểm tra tính nhất quán chéo GDD)
3. /gate-check systems-design
4. /create-architecture + /architecture-decision (cho từng quyết định lớn)
5. /architecture-review
6. /create-control-manifest
7. /gate-check technical-setup
8. /create-epics layer: foundation + /create-stories [slug] (xác định epic, chia story)
9. /sprint-plan new
10. /story-readiness -> triển khai code -> /story-done (vòng đời story)
```

### Workflow 3: "Tôi cần thêm một tính năng phức tạp giữa giai đoạn sản xuất"

```
1. /design-system hoặc /quick-design (tùy theo quy mô)
2. /design-review để xác thực
3. /propagate-design-change nếu sửa đổi các GDD hiện có
4. /estimate để ước lượng công sức và rủi ro
5. /team-combat, /team-narrative, /team-ui, v.v. (chọn team skill phù hợp)
6. /story-done khi hoàn thành
7. /balance-check nếu tính năng ảnh hưởng đến cân bằng game
```

### Workflow 4: "Có lỗi phát sinh trên production"

```
1. /hotfix "mô tả sự cố"
2. Bản sửa lỗi được thực hiện trên nhánh hotfix
3. /code-review bản sửa lỗi
4. Chạy kiểm thử tests
5. /release-checklist cho bản build hotfix
6. Triển khai và backport ngược lại nhánh chính
```

### Workflow 5: "Tôi có dự án cũ và muốn áp dụng hệ thống này"

```
1. /start (chọn Nhánh D -- công việc có sẵn)
2. /project-stage-detect (xác định giai đoạn hiện tại)
3. /adopt (audit các tài liệu hiện có, lập kế hoạch migration)
4. /design-system retrofit [path] (bổ sung khoảng trống GDD)
5. /architecture-decision retrofit [path] (bổ sung khoảng trống ADR)
6. /gate-check tại điểm chuyển đổi phù hợp
```

### Workflow 6: "Bắt đầu một Sprint mới"

```
1. /retrospective (đánh giá sprint trước)
2. /sprint-plan new (tạo sprint tiếp theo)
3. /scope-check (đảm bảo quy mô có thể quản lý được)
4. /story-readiness cho từng story trước khi bắt đầu code
5. Triển khai các stories
6. /story-done cho mỗi story hoàn thành
7. /sprint-status để kiểm tra tiến độ nhanh
```

### Workflow 7: "Phát hành trò chơi"

```
1. /gate-check polish (xác minh giai đoạn Đánh bóng đã hoàn tất)
2. /tech-debt (quyết định những gì chấp nhận được khi phát hành)
3. /localize (đợt kiểm tra đa ngôn ngữ cuối cùng)
4. /release-checklist v1.0.0
5. /launch-checklist (xác thực toàn diện liên phòng ban)
6. /team-release (điều phối đợt phát hành)
7. /patch-notes và /changelog
8. Phát hành game!
9. /hotfix nếu có bất kỳ sự cố nào phát sinh sau ra mắt
10. Post-mortem sau khi đợt ra mắt đã ổn định
```

### Workflow 8: "Tôi bị lạc lối / không biết phải làm gì tiếp theo"

```
1. /help (đọc giai đoạn hiện tại, kiểm tra tài liệu, chỉ dẫn bước tiếp theo)
2. Nếu /help chưa đủ rõ ràng: /project-stage-detect (audit toàn diện)
3. Nếu giai đoạn có vẻ sai: /gate-check tại điểm chuyển đổi mà bạn nghĩ mình đang ở đó
```

---

## Các mẹo để khai thác tối đa hệ thống

1. **Luôn bắt đầu với thiết kế, sau đó mới triển khai code.** Hệ thống agent được xây dựng dựa trên giả định rằng tài liệu thiết kế phải tồn tại trước khi viết code. Các agent liên tục tham chiếu GDD.

2. **Sử dụng team skills cho các tính năng liên phòng ban.** Đừng tự mình điều phối thủ công 4 agent — hãy để `/team-combat`, `/team-narrative`, v.v. xử lý khâu điều phối.

3. **Tin tưởng hệ thống rules.** Khi một rule cảnh báo điều gì đó trong code của bạn, hãy sửa nó. Các rules đã đúc kết những bài học xương máu trong phát triển game (giá trị data-driven, delta time, accessibility, v.v.).

4. **Chủ động compact ngữ cảnh.** Khi mức sử dụng context đạt ~65-70%, hãy compact hoặc gõ `/clear`. Hook pre-compact sẽ lưu lại tiến trình của bạn. Đừng đợi đến khi chạm giới hạn trần.

5. **Sử dụng đúng tầng agent.** Đừng yêu cầu `creative-director` viết shader. Đừng yêu cầu `qa-tester` đưa ra quyết định thiết kế. Hệ thống phân cấp tồn tại là có lý do.

6. **Chạy /help khi phân vân.** Lệnh sẽ đọc trạng thái thực tế của dự án và cho bạn biết bước tiếp theo quan trọng nhất.

7. **Chạy `/design-review` trước khi giao thiết kế cho lập trình viên.** Việc này giúp phát hiện sớm các đặc tả chưa hoàn thiện, tránh phải làm lại nhiều lần.

8. **Chạy `/code-review` sau mỗi tính năng lớn.** Bắt sớm các vấn đề kiến trúc trước khi chúng lan rộng.

9. **Làm prototype cho các cơ chế rủi ro trước.** Một ngày làm prototype có thể tiết kiệm cả tuần sản xuất cho một cơ chế chơi không thấy vui.

10. **Giữ kế hoạch sprint trung thực.** Thường xuyên dùng `/scope-check`. Phình to quy mô (scope creep) là kẻ thù số một của các nhà phát triển game indie.

11. **Ghi lại các quyết định bằng ADR.** Bản thân bạn trong tương lai sẽ cảm ơn bạn ở hiện tại vì đã ghi lại *tại sao* mọi thứ lại được xây dựng theo cách đó.

12. **Tuân thủ nghiêm ngặt vòng đời story.** Dùng `/story-readiness` trước khi nhận làm, `/story-done` sau khi hoàn thành. Việc này phát hiện sớm các sai lệch và giữ cho pipeline luôn minh bạch.

13. **Ghi file sớm và thường xuyên.** Ghi từng phần tăng dần giúp các quyết định thiết kế của bạn tồn tại qua các sự cố crash và compaction. File lưu trên đĩa chính là bộ nhớ bền vững, không phải cuộc trò chuyện tạm thời.
