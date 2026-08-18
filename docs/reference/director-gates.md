# Cổng Director — Mẫu Đánh giá Dùng chung (Director Gates — Shared Review Pattern)

Tài liệu này định nghĩa các prompt cổng (gate prompts) chuẩn cho tất cả các đánh giá của giám đốc (director) và trưởng bộ phận (lead) xuyên suốt mọi giai đoạn phát triển. Các skill chỉ cần tham chiếu Gate ID từ tài liệu này thay vì nhúng toàn bộ prompt trực tiếp — loại bỏ sự sai lệch khi các prompt cần cập nhật.

**Phạm vi**: Cả 7 giai đoạn sản xuất (Concept → Release), cả 3 giám đốc Tier 1, và các lead Tier 2 chủ chốt. Mọi skill, bộ điều phối team hoặc workflow đều có thể gọi các cổng này.

---

## Cách sử dụng Tài liệu này

Trong bất kỳ skill nào, hãy thay thế prompt director trực tiếp bằng một tham chiếu:

```
Gọi `creative-director` qua Task sử dụng cổng **CD-PILLARS** từ
`docs/reference/director-gates.md`.
```

Truyền ngữ cảnh được liệt kê trong mục **Context to pass** của cổng đó, sau đó xử lý kết luận bằng các quy tắc **Verdict handling** bên dưới.

---

## Các Chế độ Review (Review Modes)

Mức độ review kiểm soát việc các cổng director có chạy hay không. Chế độ này có thể được thiết lập toàn cục (lưu qua các phiên) hoặc ghi đè theo từng lần chạy skill.

**Cấu hình toàn cục**: `production/review-mode.txt` — một từ duy nhất: `full`, `lean`, hoặc `solo`.
Thiết lập một lần trong `/start`. Chỉnh sửa trực tiếp file để thay đổi bất kỳ lúc nào.

**Ghi đè theo lần chạy**: bất kỳ skill nào có dùng cổng đều chấp nhận đối số `--review [full|lean|solo]`. Điều này chỉ ghi đè cấu hình toàn cục cho lần chạy đó.

Ví dụ:
```
/brainstorm space horror           → dùng chế độ toàn cục
/brainstorm space horror --review full   → ép buộc chế độ full lần chạy này
/architecture-decision --review solo     → bỏ qua mọi cổng lần chạy này
```

| Chế độ | Những gì sẽ chạy | Phù hợp nhất cho |
|---|---|---|
| `full` | Mọi cổng đều hoạt động — mọi bước workflow đều được review | Đội nhóm, người dùng đang học hỏi, hoặc khi muốn có phản hồi kỹ lưỡng từ director ở từng bước |
| `lean` | Chỉ chạy PHASE-GATE (`/gate-check`) — bỏ qua các cổng inline theo từng skill | **Mặc định** — solo dev và nhóm nhỏ; director chỉ review tại các mốc milestone |
| `solo` | Không chạy cổng director ở bất kỳ đâu | Game jams, prototypes, tốc độ tối đa |

**Mẫu kiểm tra — áp dụng trước mỗi lần gọi cổng:**

```
Trước khi gọi cổng [GATE-ID]:
1. Nếu skill được gọi kèm --review [mode], dùng chế độ đó
2. Ngược lại, đọc production/review-mode.txt
3. Ngược lại, mặc định là lean

Áp dụng chế độ đã phân giải:
- solo → bỏ qua mọi cổng. Ghi chú: "[GATE-ID] skipped — Solo mode"
- lean → bỏ qua trừ khi đây là PHASE-GATE (CD-PHASE-GATE, TD-PHASE-GATE, PR-PHASE-GATE, AD-PHASE-GATE)
         Ghi chú: "[GATE-ID] skipped — Lean mode"
- full → gọi bình thường
```

---

## Mẫu Gọi Cổng (Invocation Pattern)

**BẮT BUỘC: Phân giải chế độ review trước mỗi lần gọi cổng.** Tuyệt đối không gọi cổng mà chưa kiểm tra. Chế độ được xác định một lần cho mỗi lượt chạy skill:
1. Nếu skill được gọi kèm `--review [mode]`, dùng chế độ đó
2. Ngược lại, đọc `production/review-mode.txt`
3. Ngược lại, mặc định là `lean`

Áp dụng chế độ đã phân giải:
- `solo` → **bỏ qua mọi cổng**. Ghi chú trong đầu ra: `[GATE-ID] skipped — Solo mode`
- `lean` → **bỏ qua trừ khi đây là PHASE-GATE** (CD-PHASE-GATE, TD-PHASE-GATE, PR-PHASE-GATE, AD-PHASE-GATE). Ghi chú: `[GATE-ID] skipped — Lean mode`
- `full` → gọi bình thường

```
# Áp dụng kiểm tra chế độ, sau đó:
Gọi `[agent-name]` qua Task:
- Cổng: [GATE-ID] (xem docs/reference/director-gates.md)
- Ngữ cảnh: [các trường liệt kê dưới cổng đó]
- Chờ kết luận trước khi tiếp tục.
```

Đối với việc gọi song song (nhiều director tại cùng một điểm cổng):

```
# Áp dụng kiểm tra chế độ cho từng cổng trước, sau đó gọi tất cả các cổng hợp lệ:
Gọi đồng thời tất cả [N] agents qua Task — phát ra tất cả các lệnh gọi Task trước
khi chờ đợi bất kỳ kết quả nào. Thu thập tất cả kết luận trước khi tiếp tục.
```

---

## Định dạng Kết luận Chuẩn (Standard Verdict Format)

Tất cả các cổng đều trả về một trong ba kết luận. Các skill phải xử lý cả ba trường hợp:

| Kết luận | Ý nghĩa | Hành động mặc định |
|---|---|---|
| **APPROVE / READY** | Không có vấn đề. Tiến hành tiếp. | Tiếp tục workflow |
| **CONCERNS [list]** | Có vấn đề nhưng không chặn hoàn toàn. | Hiển thị cho người dùng qua `AskUserQuestion` — các lựa chọn: `Sửa đổi các mục được đánh dấu` / `Chấp nhận và tiếp tục` / `Thảo luận thêm` |
| **REJECT / NOT READY [blockers]** | Có vấn đề chặn nghiêm trọng. Không được tiếp tục. | Hiển thị các điểm nghẽn cho người dùng. Không ghi file hoặc chuyển giai đoạn cho đến khi được giải quyết. |

**Quy tắc phân xử**: Khi nhiều director được gọi song song, hãy áp dụng kết luận nghiêm ngặt nhất — chỉ cần một kết luận NOT READY sẽ ghi đè tất cả các kết luận READY.

---

## Ghi nhận Kết quả Cổng

Sau khi một cổng hoàn tất phân giải, hãy ghi nhận kết luận vào tiêu đề trạng thái của tài liệu liên quan:

```markdown
> **[Director] Review ([GATE-ID])**: APPROVED [date] / CONCERNS (accepted) [date] / REVISED [date]
```

Đối với các cổng giai đoạn, hãy ghi nhận vào `docs/architecture/architecture.md` hoặc `production/session-state/active.md` khi phù hợp.

---

## Phân tầng 1 — Các cổng Creative Director

Agent: `creative-director` | Phân tầng Model: Opus | Lĩnh vực: Tầm nhìn, trụ cột, trải nghiệm người chơi

---

### CD-PILLARS — Kiểm tra Áp lực Trụ cột (Pillar Stress Test)

**Kích hoạt**: Sau khi các trụ cột và phản trụ cột (anti-pillars) được xác định (brainstorm Giai đoạn 4, hoặc bất cứ khi nào trụ cột được sửa đổi)

**Ngữ cảnh cần truyền**:
- Toàn bộ tập hợp trụ cột kèm tên, định nghĩa và bài test thiết kế
- Danh sách phản trụ cột
- Tuyên bố hình dung cốt lõi (core fantasy)
- Điểm nhấn độc đáo (Unique hook)

**Prompt**:
> "Review these game pillars. Are they falsifiable — could a real design decision
> actually fail this pillar? Do they create meaningful tension with each other? Do
> they differentiate this game from its closest comparables? Would they help resolve
> a design disagreement in practice, or are they too vague to be useful? Return
> specific feedback for each pillar and an overall verdict: APPROVE (strong), CONCERNS
> [list] (needs sharpening), or REJECT (weak — pillars do not carry weight)."

**Kết luận**: APPROVE / CONCERNS / REJECT

---

### CD-GDD-ALIGN — Kiểm tra Độ đồng bộ Trụ cột trong GDD

**Kích hoạt**: Sau khi GDD của một hệ thống được soạn thảo (design-system, quick-design, hoặc bất kỳ workflow nào tạo ra GDD)

**Ngữ cảnh cần truyền**:
- Đường dẫn file GDD
- Các trụ cột game (từ `design/gdd/game-concept.md` hoặc `design/gdd/game-pillars.md`)
- Mục tiêu thẩm mỹ MDA của game này
- Phần Hình dung của người chơi (Player Fantasy) của hệ thống

**Prompt**:
> "Review this system GDD for pillar alignment. Does every section serve the stated
> pillars? Are there mechanics or rules that contradict or weaken a pillar? Does
> the Player Fantasy section match the game's core fantasy? Return APPROVE, CONCERNS
> [specific sections with issues], or REJECT [pillar violations that must be
> redesigned before this system is implementable]."

**Kết luận**: APPROVE / CONCERNS / REJECT

---

### CD-SYSTEMS — Kiểm tra Tầm nhìn Phân rã Hệ thống

**Kích hoạt**: Sau khi systems index được ghi bởi `/map-systems` — xác thực toàn bộ tập hợp hệ thống trước khi bắt đầu viết GDD

**Ngữ cảnh cần truyền**:
- Đường dẫn systems index (`design/gdd/systems-index.md`)
- Các trụ cột game và hình dung cốt lõi
- Phân bổ phân tầng ưu tiên (MVP / Vertical Slice / Alpha / Full Vision)
- Bất kỳ hệ thống rủi ro cao hoặc điểm nghẽn cổ chai nào được xác định

**Prompt**:
> "Review this systems decomposition against the game's design pillars. Does the
> full set of MVP-tier systems collectively deliver the core fantasy? Are there
> systems whose mechanics don't serve any stated pillar — indicating they may be
> scope creep? Are there pillar-critical player experiences that have no system
> assigned to deliver them? Are any systems missing that the core loop requires?
> Return APPROVE (systems serve the vision), CONCERNS [specific gaps or
> misalignments with their pillar implications], or REJECT [fundamental gaps —
> the decomposition misses critical design intent and must be revised before GDD
> authoring begins]."

**Kết luận**: APPROVE / CONCERNS / REJECT

---

### CD-NARRATIVE — Kiểm tra Tính nhất quán Cốt truyện

**Kích hoạt**: Sau khi các GDD cốt truyện, tài liệu lore, đặc tả lời thoại hoặc tài liệu xây dựng thế giới được soạn thảo

**Ngữ cảnh cần truyền**:
- Đường dẫn file tài liệu
- Các trụ cột game
- Bản tóm tắt định hướng cốt truyện hoặc hướng dẫn tông điệu
- Bất kỳ lore hiện có nào mà tài liệu mới tham chiếu tới

**Prompt**:
> "Review this narrative content for consistency with the game's pillars and
> established world rules. Does the tone match the game's established voice? Are
> there contradictions with existing lore or world-building? Does the content serve
> the player experience pillar? Return APPROVE, CONCERNS [specific inconsistencies],
> or REJECT [contradictions that break world coherence]."

**Kết luận**: APPROVE / CONCERNS / REJECT

---

### CD-PLAYTEST — Xác thực Trải nghiệm Người chơi

**Kích hoạt**: Sau khi báo cáo playtest được tạo (`/playtest-report`), hoặc sau bất kỳ phiên nào tạo ra phản hồi của người chơi

**Ngữ cảnh cần truyền**:
- Đường dẫn file báo cáo playtest
- Các trụ cột game và tuyên bố hình dung cốt lõi
- Giả thuyết cụ thể đang được kiểm chứng

**Prompt**:
> "Review this playtest report against the game's design pillars and core fantasy.
> Is the player experience matching the intended fantasy? Are there systematic issues
> that represent pillar drift — mechanics that feel fine in isolation but undermine
> the intended experience? Return APPROVE (core fantasy is landing), CONCERNS [gaps
> between intended and actual experience], or REJECT [core fantasy is not present —
> redesign needed before further playtesting]."

**Kết luận**: APPROVE / CONCERNS / REJECT

---

### CD-PHASE-GATE — Đánh giá Sẵn sàng Sáng tạo khi Chuyển Giai đoạn

**Kích hoạt**: Luôn chạy tại `/gate-check` — gọi song song với TD-PHASE-GATE, PR-PHASE-GATE, và AD-PHASE-GATE

**Ngữ cảnh cần truyền**:
- Tên giai đoạn mục tiêu
- Danh sách tất cả các sản phẩm hiện có (đường dẫn file)
- Các trụ cột game và hình dung cốt lõi

**Prompt**:
> "Review the current project state for [target phase] gate readiness from a
> creative direction perspective. Are the game pillars faithfully represented in
> all design artifacts? Does the current state preserve the core fantasy? Are there
> any design decisions across GDDs or architecture that compromise the intended
> player experience? Return READY, CONCERNS [list], or NOT READY [blockers]."

**Kết luận**: READY / CONCERNS / NOT READY

---

## Phân tầng 1 — Các cổng Technical Director

Agent: `technical-director` | Phân tầng Model: Opus | Lĩnh vực: Kiến trúc, rủi ro engine, hiệu năng

---

### TD-SYSTEM-BOUNDARY — Đánh giá Kiến trúc Ranh giới Hệ thống

**Kích hoạt**: Sau khi lập bản đồ phụ thuộc của `/map-systems` được thống nhất nhưng trước khi viết GDD

**Ngữ cảnh cần truyền**:
- Đường dẫn systems index
- Phân bổ phân tầng (Foundation / Core / Feature / Presentation / Polish)
- Toàn bộ đồ thị phụ thuộc
- Các hệ thống điểm nghẽn được đánh dấu
- Các phụ thuộc vòng tròn được phát hiện và giải pháp đề xuất

**Prompt**:
> "Review this systems decomposition from an architectural perspective before GDD
> authoring begins. Are the system boundaries clean — does each system own a
> distinct concern with minimal overlap? Are there God Object risks (systems doing
> too much)? Does the dependency ordering create implementation-sequencing problems?
> Are there implicit shared-state problems in the proposed boundaries that will
> cause tight coupling when implemented? Are any Foundation-layer systems actually
> dependent on Feature-layer systems (inverted dependency)? Return APPROVE
> (boundaries are architecturally sound — proceed to GDD authoring), CONCERNS
> [specific boundary issues to address in the GDDs themselves], or REJECT
> [fundamental boundary problems — the system structure will cause architectural
> issues and must be restructured before any GDD is written]."

**Kết luận**: APPROVE / CONCERNS / REJECT

---

### TD-FEASIBILITY — Đánh giá Tính khả thi Kỹ thuật

**Kích hoạt**: Sau khi các rủi ro kỹ thuật lớn nhất được xác định trong giai đoạn phạm vi/khả thi

**Ngữ cảnh cần truyền**:
- Mô tả vòng lặp cốt lõi của concept
- Nền tảng mục tiêu
- Lựa chọn engine
- Danh sách rủi ro kỹ thuật đã xác định

**Prompt**:
> "Review these technical risks for a [genre] game targeting [platform] using
> [engine or 'undecided engine']. Flag any HIGH risk items that could invalidate
> the concept as described, any risks that are engine-specific and should influence
> the engine choice, and any risks that are commonly underestimated by solo
> developers. Return VIABLE (risks are manageable), CONCERNS [list with mitigation
> suggestions], or HIGH RISK [blockers that require concept or scope revision]."

**Kết luận**: VIABLE / CONCERNS / HIGH RISK

---

### TD-ARCHITECTURE — Ký duyệt Kiến trúc

**Kích hoạt**: Sau khi tài liệu kiến trúc tổng thể được soạn thảo (`/create-architecture`), và sau bất kỳ sửa đổi kiến trúc lớn nào

**Ngữ cảnh cần truyền**:
- Đường dẫn tài liệu kiến trúc (`docs/architecture/architecture.md`)
- Đường cơ sở yêu cầu kỹ thuật (TR-IDs và số lượng)
- Danh sách ADR kèm trạng thái
- Bản kiểm kê khoảng trống kiến thức engine

**Prompt**:
> "Review this master architecture document for technical soundness. Check: (1) Is
> every technical requirement from the baseline covered by an architectural decision?
> (2) Are all HIGH risk engine domains explicitly addressed or flagged as open
> questions? (3) Are the API boundaries clean, minimal, and implementable? (4) Are
> Foundation layer ADR gaps resolved before implementation begins? Return APPROVE,
> CONCERNS [list], or REJECT [blockers that must be resolved before coding starts]."

**Kết luận**: APPROVE / CONCERNS / REJECT

---

### TD-ADR — Đánh giá Quyết định Kiến trúc

**Kích hoạt**: Sau khi một ADR riêng lẻ được soạn thảo (`/architecture-decision`), trước khi nó được đánh dấu Accepted

**Ngữ cảnh cần truyền**:
- Đường dẫn file ADR
- Phiên bản engine và mức rủi ro khoảng trống kiến thức cho lĩnh vực đó
- Các ADR liên quan (nếu có)

**Prompt**:
> "Review this Architecture Decision Record. Does it have a clear problem statement
> and rationale? Are the rejected alternatives genuinely considered? Does the
> Consequences section acknowledge the trade-offs honestly? Is the engine version
> stamped? Are post-cutoff API risks flagged? Does it link to the GDD requirements
> it covers? Return APPROVE, CONCERNS [specific gaps], or REJECT [the decision is
> underspecified or makes unsound technical assumptions]."

**Kết luận**: APPROVE / CONCERNS / REJECT

---

### TD-PHASE-GATE — Đánh giá Sẵn sàng Kỹ thuật khi Chuyển Giai đoạn

**Kích hoạt**: Luôn chạy tại `/gate-check` — gọi song song với CD-PHASE-GATE, PR-PHASE-GATE, và AD-PHASE-GATE

**Ngữ cảnh cần truyền**:
- Tên giai đoạn mục tiêu
- Đường dẫn tài liệu kiến trúc (nếu có)
- Đường dẫn tài liệu tham chiếu engine
- Danh sách ADR

**Prompt**:
> "Review the current project state for [target phase] gate readiness from a
> technical direction perspective. Is the architecture sound for this phase? Are
> all high-risk engine domains addressed? Are performance budgets realistic and
> documented? Are Foundation-layer decisions complete enough to begin implementation?
> Return READY, CONCERNS [list], or NOT READY [blockers]."

**Kết luận**: READY / CONCERNS / NOT READY

---

## Phân tầng 1 — Các cổng Producer

Agent: `producer` | Phân tầng Model: Opus | Lĩnh vực: Quy mô, tiến độ, phụ thuộc, rủi ro sản xuất

---

### PR-SCOPE — Xác thực Quy mô và Tiến độ

**Kích hoạt**: Sau khi các phân tầng quy mô được xác định

**Ngữ cảnh cần truyền**:
- Mô tả quy mô tầm nhìn đầy đủ
- Định nghĩa MVP
- Ước lượng tiến độ thời gian
- Quy mô nhóm (solo / nhóm nhỏ / v.v.)
- Các phân tầng quy mô (những gì sẽ phát hành nếu hết thời gian)

**Prompt**:
> "Review this scope estimate. Is the MVP achievable in the stated timeline for
> the stated team size? Are the scope tiers correctly ordered by risk — does each
> tier deliver a shippable product if work stops there? What is the most likely
> cut point under time pressure, and is it a graceful fallback or a broken product?
> Return REALISTIC (scope matches capacity), OPTIMISTIC [specific adjustments
> recommended], or UNREALISTIC [blockers — timeline or MVP must be revised]."

**Kết luận**: REALISTIC / OPTIMISTIC / UNREALISTIC

---

### PR-SPRINT — Đánh giá Tính khả thi của Sprint

**Kích hoạt**: Trước khi chốt kế hoạch sprint (`/sprint-plan`), và sau bất kỳ thay đổi phạm vi nào giữa sprint

**Ngữ cảnh cần truyền**:
- Danh sách story đề xuất cho sprint (tiêu đề, ước lượng, phụ thuộc)
- Năng lực của nhóm (số giờ khả dụng)
- Nợ tồn đọng trong backlog sprint hiện tại (nếu có)
- Các ràng buộc của milestone

**Prompt**:
> "Review this sprint plan for feasibility. Is the story load realistic for the
> available capacity? Are stories correctly ordered by dependency? Are there hidden
> dependencies between stories that could block the sprint mid-way? Are any stories
> underestimated given their technical complexity? Return REALISTIC (plan is
> achievable), CONCERNS [specific risks], or UNREALISTIC [sprint must be
> descoped — identify which stories to defer]."

**Kết luận**: REALISTIC / CONCERNS / UNREALISTIC

---

### PR-PHASE-GATE — Đánh giá Sẵn sàng Sản xuất khi Chuyển Giai đoạn

**Kích hoạt**: Luôn chạy tại `/gate-check` — gọi song song với CD-PHASE-GATE, TD-PHASE-GATE, và AD-PHASE-GATE

**Ngữ cảnh cần truyền**:
- Tên giai đoạn mục tiêu
- Các sản phẩm sprint và milestone hiện có
- Quy mô và năng lực nhóm
- Số lượng story bị chặn hiện tại

**Prompt**:
> "Review the current project state for [target phase] gate readiness from a
> production perspective. Is the scope realistic for the stated timeline and team
> size? Are dependencies properly ordered so the team can actually execute in
> sequence? Are there milestone or sprint risks that could derail the phase within
> the first two sprints? Return READY, CONCERNS [list], or NOT READY [blockers]."

**Kết luận**: READY / CONCERNS / NOT READY

---

## Phân tầng 1 — Các cổng Art Director

Agent: `art-director` | Phân tầng Model: Sonnet | Lĩnh vực: Nhận diện hình ảnh, art bible, sẵn sàng sản xuất mỹ thuật

---

### AD-ART-BIBLE — Ký duyệt Art Bible

**Kích hoạt**: Sau khi art bible được soạn thảo (`/art-bible`), trước khi sản xuất asset bắt đầu

**Ngữ cảnh cần truyền**:
- Đường dẫn art bible (`design/art/art-bible.md`)
- Các trụ cột game và hình dung cốt lõi
- Ràng buộc nền tảng và hiệu năng
- Điểm neo nhận diện hình ảnh đã chọn trong brainstorm

**Prompt**:
> "Review this art bible for completeness and internal consistency. Does the color
> system match the mood targets? Does the shape language follow from the visual
> identity statement? Are the asset standards achievable within the platform
> constraints? Does the character design direction give artists enough to work from
> without over-specifying? Are there contradictions between sections? Would an
> outsourcing team be able to produce assets from this document without additional
> briefing? Return APPROVE (art bible is production-ready), CONCERNS [specific
> sections needing clarification], or REJECT [fundamental inconsistencies that must
> be resolved before asset production begins]."

**Kết luận**: APPROVE / CONCERNS / REJECT

---

### AD-PHASE-GATE — Đánh giá Sẵn sàng Mỹ thuật khi Chuyển Giai đoạn

**Kích hoạt**: Luôn chạy tại `/gate-check` — gọi song song với CD-PHASE-GATE, TD-PHASE-GATE, và PR-PHASE-GATE

**Ngữ cảnh cần truyền**:
- Tên giai đoạn mục tiêu
- Danh sách tất cả sản phẩm hình ảnh/mỹ thuật hiện có (đường dẫn file)
- Điểm neo nhận diện hình ảnh
- Đường dẫn art bible nếu có

**Prompt**:
> "Review the current project state for [target phase] gate readiness from a visual
> direction perspective. Is the visual identity established and documented at the
> level this phase requires? Are the right visual artifacts in place? Would visual
> teams be able to begin their work without visual direction gaps that cause costly
> rework later? Are there visual decisions that are being deferred past their latest
> responsible moment? Return READY, CONCERNS [specific visual direction gaps that
> could cause production rework], or NOT READY [visual blockers that must exist
> before this phase can succeed — specify what artifact is missing and why it
> matters at this stage]."

**Kết luận**: READY / CONCERNS / NOT READY

---

## Phân tầng 2 — Các cổng Lead

Các cổng này được gọi bởi các skill điều phối và skill cấp cao khi cần sự ký duyệt tính khả thi của chuyên viên lĩnh vực. Các lead Tier 2 sử dụng Sonnet (mặc định).

---

### LP-FEASIBILITY — Tính khả thi Triển khai của Lead Programmer

**Kích hoạt**: Sau khi tài liệu kiến trúc tổng thể được viết (`/create-architecture`), hoặc khi một mẫu kiến trúc mới được đề xuất

**Ngữ cảnh cần truyền**:
- Đường dẫn tài liệu kiến trúc
- Tóm tắt đường cơ sở yêu cầu kỹ thuật
- Danh sách ADR kèm trạng thái

**Prompt**:
> "Review this architecture for implementation feasibility. Flag: (a) any decisions
> that would be difficult or impossible to implement with the stated engine and
> language, (b) any missing interface definitions that programmers would need to
> invent themselves, (c) any patterns that create avoidable technical debt or
> that contradict standard [engine] idioms. Return FEASIBLE, CONCERNS [list], or
> INFEASIBLE [blockers that make this architecture unimplementable as written]."

**Kết luận**: FEASIBLE / CONCERNS / INFEASIBLE

---

### LP-CODE-REVIEW — Code Review của Lead Programmer

**Kích hoạt**: Sau khi một dev story được triển khai code (`/dev-story`, `/story-done`), hoặc như một phần của `/code-review`

**Ngữ cảnh cần truyền**:
- Đường dẫn các file triển khai code
- Đường dẫn file story (để lấy tiêu chí chấp nhận)
- Phần GDD liên quan
- ADR chi phối hệ thống này

**Prompt**:
> "Review this implementation against the story acceptance criteria and governing
> ADR. Does the code match the architecture boundary definitions? Are there
> violations of the coding standards or forbidden patterns? Is the public API
> testable and documented? Are there any correctness issues against the GDD rules?
> Return APPROVE, CONCERNS [specific issues], or REJECT [must be revised before merge]."

**Kết luận**: APPROVE / CONCERNS / REJECT

---

### QL-STORY-READY — Kiểm tra Story Sẵn sàng của QA Lead

**Kích hoạt**: Trước khi một story được chấp nhận vào sprint — được gọi bởi `/create-stories`, `/story-readiness`, và `/sprint-plan`

**Ngữ cảnh cần truyền**:
- Đường dẫn file story
- Loại story (Logic / Integration / Visual/Feel / UI / Config/Data)
- Danh sách tiêu chí chấp nhận (nguyên văn từ story)
- Yêu cầu GDD (TR-ID và văn bản) mà story bao phủ

**Prompt**:
> "Review this story's acceptance criteria for testability before it enters the
> sprint. Are all criteria specific enough that a developer would know unambiguously
> when they are done? For Logic-type stories: can every criterion be verified with
> an automated test? For Integration stories: is each criterion observable in a
> controlled test environment? Flag criteria that are too vague to implement
> against, and flag criteria that require a full game build to test (mark these
> DEFERRED, not BLOCKED). Return ADEQUATE (criteria are implementable as written),
> GAPS [specific criteria needing refinement], or INADEQUATE [criteria are too
> vague — story must be revised before sprint inclusion]."

**Kết luận**: ADEQUATE / GAPS / INADEQUATE

---

### QL-TEST-COVERAGE — Đánh giá Độ bao phủ Kiểm thử của QA Lead

**Kích hoạt**: Sau khi các story triển khai hoàn tất, trước khi đánh dấu một epic hoàn thành, hoặc tại `/gate-check` Production → Polish

**Ngữ cảnh cần truyền**:
- Danh sách story đã triển khai kèm loại story
- Đường dẫn file test trong `tests/`
- Tiêu chí chấp nhận GDD cho hệ thống

**Prompt**:
> "Review the test coverage for these implementation stories. Are all Logic stories
> covered by passing unit tests? Are Integration stories covered by integration
> tests or documented playtests? Are the GDD acceptance criteria each mapped to at
> least one test? Are there untested edge cases from the GDD Edge Cases section?
> Return ADEQUATE (coverage meets standards), GAPS [specific missing tests], or
> INADEQUATE [critical logic is untested — do not advance]."

**Kết luận**: ADEQUATE / GAPS / INADEQUATE

---

## Giao thức Cổng Song song (Parallel Gate Protocol)

Khi một workflow yêu cầu nhiều director tại cùng một điểm kiểm tra (thường gặp nhất tại `/gate-check`), hãy gọi đồng thời tất cả các agent:

```
Gọi song song (phát ra tất cả các lệnh gọi Task trước khi chờ đợi kết quả):
1. creative-director  → cổng CD-PHASE-GATE
2. technical-director → cổng TD-PHASE-GATE
3. producer           → cổng PR-PHASE-GATE
4. art-director       → cổng AD-PHASE-GATE

Thu thập cả 4 kết luận, sau đó áp dụng quy tắc phân xử:
- Bất kỳ kết luận NOT READY / REJECT → kết luận tổng thể tối thiểu là FAIL
- Bất kỳ kết luận CONCERNS → kết luận tổng thể tối thiểu là CONCERNS
- Tất cả READY / APPROVE → đủ điều kiện đạt PASS (vẫn phụ thuộc vào việc kiểm tra sự hiện diện của sản phẩm)
```

---

## Độ bao phủ Cổng theo từng Giai đoạn

| Giai đoạn | Các Cổng Bắt buộc | Các Cổng Tùy chọn |
|---|---|---|
| **Concept** | CD-PILLARS, AD-CONCEPT-VISUAL | TD-FEASIBILITY, PR-SCOPE |
| **Systems Design** | TD-SYSTEM-BOUNDARY, CD-SYSTEMS, PR-SCOPE, CD-GDD-ALIGN (mỗi GDD) | ND-CONSISTENCY, AD-VISUAL |
| **Technical Setup** | TD-ARCHITECTURE, TD-ADR (mỗi ADR), LP-FEASIBILITY, AD-ART-BIBLE | TD-ENGINE-RISK |
| **Pre-Production** | PR-EPIC, QL-STORY-READY (mỗi story), PR-SPRINT, cả 4 PHASE-GATE (qua gate-check) | CD-PLAYTEST |
| **Production** | LP-CODE-REVIEW (mỗi story), QL-STORY-READY, PR-SPRINT (mỗi sprint), QL-TEST-COVERAGE (khi đóng sprint) | PR-MILESTONE, AD-VISUAL |
| **Polish** | QL-TEST-COVERAGE, CD-PLAYTEST, PR-MILESTONE | AD-VISUAL |
| **Release** | Cả 4 PHASE-GATE (qua gate-check) | QL-TEST-COVERAGE |
