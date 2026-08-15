# Tiêu chí đánh giá chất lượng Skill (Skill Quality Rubric)

Được sử dụng bởi `/skill-test category [name|all]` để đánh giá các skill vượt ra ngoài việc tuân thủ cấu trúc thông thường.
Mỗi danh mục xác định 4–5 chỉ số nhị phân PASS/FAIL đặc thù cho nhiệm vụ của skill đó.

Một chỉ số đạt PASS khi các chỉ dẫn bằng văn bản của skill đáp ứng rõ ràng tiêu chí.
Một chỉ số bị FAIL khi các chỉ dẫn bị thiếu, mơ hồ, hoặc mâu thuẫn.
Một chỉ số bị WARN khi các chỉ dẫn chỉ giải quyết được một phần tiêu chí.

---

## Các danh mục Skill (Skill Categories)

### `gate`

**Skills**: gate-check

Các skill dạng Gate kiểm soát việc chuyển đổi giai đoạn. Chúng phải thực thi tính đúng đắn mà không tự động chuyển giai đoạn (auto-advance) và phải tôn trọng ba chế độ review.

| Chỉ số | Tiêu chí PASS |
|---|---|
| **G1 — Đọc chế độ review (Review mode read)** | Skill đọc `production/session-state/review-mode.txt` (hoặc tương đương) trước khi quyết định gọi director nào |
| **G2 — Chế độ Full: gọi cả 4 directors** | Ở chế độ `full`, tất cả 4 giám đốc Tier-1 (CD, TD, PR, AD) với prompt PHASE-GATE đều được gọi song song |
| **G3 — Chế độ Lean: chỉ PHASE-GATE** | Ở chế độ `lean`, chỉ các cổng `*-PHASE-GATE` chạy; các cổng inline (CD-PILLARS, TD-ARCHITECTURE, v.v.) được bỏ qua |
| **G4 — Chế độ Solo: không qua directors** | Ở chế độ `solo`, không có cổng director nào được gọi; mỗi cổng đều được ghi chú "bỏ qua — Chế độ Solo" |
| **G5 — Không tự ý chuyển giai đoạn (No auto-advance)** | Skill không bao giờ tự ý ghi `production/stage.txt` nếu không có sự xác nhận rõ ràng của người dùng qua "May I write" |

---

### `review`

**Skills**: design-review, architecture-review, review-all-gdds

Các skill dạng Review đọc tài liệu và đưa ra các kết luận có cấu trúc. Chúng chủ yếu là chỉ đọc (read-only) và không được kích hoạt các cổng director trong giai đoạn phân tích.

| Chỉ số | Tiêu chí PASS |
|---|---|
| **R1 — Thực thi chỉ đọc (Read-only enforcement)** | Skill không chỉnh sửa tài liệu được review nếu không có sự phê duyệt rõ ràng của người dùng; mọi thao tác ghi (review logs, cập nhật chỉ mục) đều phải đứng sau "May I write" |
| **R2 — Kiểm tra đủ 8 phần** | Skill đánh giá rõ ràng tất cả 8 phần bắt buộc của GDD (hoặc các phần kiến trúc tương đương) |
| **R3 — Từ vựng kết luận chuẩn xác** | Kết luận phải là một trong các từ khóa: APPROVED / NEEDS REVISION / MAJOR REVISION NEEDED (cho design) hoặc PASS / CONCERNS / FAIL (cho architecture) |
| **R4 — Không gọi cổng director khi đang phân tích** | Skill không gọi các cổng director trong các giai đoạn phân tích; đánh giá của director sau phân tích (như trong architecture-review) được chấp nhận khi phạm vi và tầm quan trọng đòi hỏi |
| **R5 — Phát hiện có cấu trúc** | Đầu ra chứa bảng trạng thái hoặc checklist theo từng phần trước khi đưa ra kết luận cuối cùng |

---

### `authoring`

**Skills**: design-system, quick-design, architecture-decision, ux-design, ux-review, art-bible, create-architecture

Các skill dạng Authoring tạo hoặc cập nhật các tài liệu thiết kế một cách cộng tác. Các skill soạn thảo GDD/UX đầy đủ sử dụng chu kỳ từng phần; các skill soạn thảo tinh gọn sử dụng mô hình một bản thảo duy nhất phù hợp với phạm vi nhỏ hơn.

| Chỉ số | Tiêu chí PASS |
|---|---|
| **A1 — Chu kỳ từng phần** | Các skill soạn thảo đầy đủ (design-system, ux-design, art-bible) soạn từng phần một, trình bày nội dung để duyệt trước khi chuyển sang phần tiếp. Các skill tinh gọn (quick-design, architecture-decision, create-architecture) có thể soạn thảo toàn bộ tài liệu rồi mới xin duyệt — chấp nhận một bản thảo duy nhất cho các tài liệu có phạm vi triển khai dưới ~4 giờ. |
| **A2 — Xin phép May-I-write theo từng phần** | Skill soạn thảo đầy đủ hỏi "Tôi có thể ghi nội dung này vào [filepath] không?" trước khi ghi từng phần. Skill tinh gọn hỏi một lần cho toàn bộ tài liệu. |
| **A3 — Chế độ Retrofit** | Skill phát hiện file đích đã tồn tại và đề xuất cập nhật các phần cụ thể thay vì ghi đè toàn bộ tài liệu. Skill tinh gọn (quick-design) luôn tạo file mới được miễn trừ. |
| **A4 — Cổng director ở đúng phân tầng** | Nếu cổng director được định nghĩa cho skill này (ví dụ: CD-GDD-ALIGN, TD-ADR), nó phải chạy ở ngưỡng chế độ đúng (full/lean) — KHÔNG chạy ở solo |
| **A5 — Tạo khung sườn trước (Skeleton-first)** | Skill soạn thảo đầy đủ tạo khung sườn file với tất cả các tiêu đề mục trước khi điền nội dung, để bảo toàn tiến độ khi phiên làm việc bị gián đoạn. Skill tinh gọn được miễn trừ. |

---

### `readiness`

**Skills**: story-readiness, story-done

Các skill dạng Readiness xác thực các story trước hoặc sau khi triển khai code. Chúng phải đưa ra các kết luận đa chiều và tích hợp chính xác với chế độ cổng director.

| Chỉ số | Tiêu chí PASS |
|---|---|
| **RD1 — Kiểm tra đa chiều** | Skill kiểm tra ≥3 chiều độc lập (ví dụ: Design, Architecture, Scope, DoD) và báo cáo riêng từng chiều |
| **RD2 — Ba mức độ kết luận** | Phân cấp kết luận được định nghĩa rõ ràng: READY/COMPLETE > NEEDS WORK/COMPLETE WITH NOTES > BLOCKED |
| **RD3 — BLOCKED đòi hỏi hành động bên ngoài** | Kết luận BLOCKED chỉ dành riêng cho các vấn đề không thể tự sửa bởi tác giả story (ví dụ: ADR đang ở trạng thái Proposed, phụ thuộc không thể giải quyết) |
| **RD4 — Cổng director ở đúng chế độ** | Cổng QL-STORY-READY hoặc LP-CODE-REVIEW được gọi ở chế độ `full`, bỏ qua ở `lean`/`solo` kèm thông báo bỏ qua |
| **RD5 — Bàn giao story tiếp theo** | Sau khi hoàn thành, skill hiển thị story READY tiếp theo từ sprint đang hoạt động |

---

### `pipeline`

**Skills**: create-epics, create-stories, dev-story, create-control-manifest, propagate-design-change, map-systems

Các skill dạng Pipeline tạo ra các sản phẩm tài liệu mà các skill khác sẽ sử dụng. Chúng phải ghi file đúng schema, tôn trọng thứ tự phân tầng/ưu tiên, và xin phép trước khi ghi.

| Chỉ số | Tiêu chí PASS |
|---|---|
| **P1 — Schema đầu ra chính xác** | Mỗi file tạo ra phải tuân theo template của dự án (EPIC.md, frontmatter của story, v.v.); skill tham chiếu đường dẫn template |
| **P2 — Thứ tự phân tầng/ưu tiên** | Các skill tạo epic hoặc story phải tôn trọng thứ tự phân tầng (core → extended → meta) và các trường ưu tiên |
| **P3 — Xin phép May-I-write trước từng sản phẩm** | Skill hỏi "Tôi có thể ghi [sản phẩm] không?" trước khi tạo từng file đầu ra, không duyệt hàng loạt tất cả file cùng lúc |
| **P4 — Cổng director ở đúng phân tầng** | Các cổng trong phạm vi (PR-EPIC, QL-STORY-READY, LP-CODE-REVIEW, v.v.) chạy ở `full`, bỏ qua ở `lean`/`solo` kèm ghi chú |
| **P5 — Đọc trước khi ghi** | Skill đọc GDD/ADR/manifest liên quan trước khi tạo sản phẩm để đảm bảo sự đồng bộ |

---

### `analysis`

**Skills**: consistency-check, balance-check, content-audit, code-review, tech-debt, scope-check, estimate, perf-profile, asset-audit, security-audit, test-evidence-review, test-flakiness

Các skill dạng Analysis quét dự án và đưa ra các phát hiện. Chúng chỉ đọc trong quá trình phân tích và phải hỏi ý kiến trước khi khuyến nghị bất kỳ thao tác ghi file nào.

| Chỉ số | Tiêu chí PASS |
|---|---|
| **AN1 — Quét chỉ đọc** | Giai đoạn phân tích chỉ sử dụng các công cụ Read/Glob/Grep; không dùng Write hoặc Edit trong lúc quét |
| **AN2 — Bảng phát hiện có cấu trúc** | Đầu ra bao gồm bảng phát hiện hoặc checklist (không chỉ văn bản thuần) kèm mức độ nghiêm trọng/ưu tiên cho từng phát hiện |
| **AN3 — Không tự ý ghi file** | Bất kỳ đề xuất ghi file nào (ví dụ: sổ đăng ký tech-debt, bản vá sửa lỗi) đều phải đứng sau "May I write" |
| **AN4 — Không gọi cổng director khi phân tích** | Các skill phân tích không gọi cổng director; chúng tạo ra các phát hiện để con người đánh giá |

---

### `team`

**Skills**: team-combat, team-narrative, team-audio, team-level, team-ui, team-qa, team-release, team-polish, team-live-ops

Các skill dạng Team điều phối nhiều agent chuyên viên cho một phòng ban. Chúng phải gọi đúng agent, chạy các agent độc lập song song, và báo cáo các điểm bị chặn ngay lập tức.

| Chỉ số | Tiêu chí PASS |
|---|---|
| **T1 — Danh sách agent có tên rõ ràng** | Skill nêu rõ những agent nào sẽ được gọi và theo thứ tự nào |
| **T2 — Chạy song song khi độc lập** | Các agent có đầu vào không phụ thuộc nhau được gọi song song (trong một tin nhắn gọi nhiều Task) |
| **T3 — Báo cáo ngay khi bị chặn (BLOCKED)** | Nếu bất kỳ agent nào trả về BLOCKED hoặc thất bại, skill lập tức báo cáo và dừng công việc phụ thuộc — không bao giờ âm thầm bỏ qua |
| **T4 — Thu thập đủ kết luận trước khi tiếp tục** | Các giai đoạn phụ thuộc phải chờ tất cả các agent song song hoàn thành trước khi tiến bước |
| **T5 — Báo lỗi cú pháp khi thiếu đối số** | Nếu thiếu đối số bắt buộc (ví dụ: tên tính năng), skill in ra gợi ý cách dùng và dừng lại mà không gọi agent |

---

### `sprint`

**Skills**: sprint-plan, sprint-status, milestone-review, retrospective, changelog, patch-notes

Các skill dạng Sprint đọc trạng thái sản xuất và tạo các báo cáo hoặc tài liệu kế hoạch. Chúng có cổng PR-SPRINT hoặc PR-MILESTONE ở các ngưỡng chế độ cụ thể.

| Chỉ số | Tiêu chí PASS |
|---|---|
| **SP1 — Đọc trạng thái sprint/milestone** | Skill đọc `production/sprints/` hoặc `production/milestones/` trước khi đưa ra kết quả |
| **SP2 — Đúng cổng sprint** | Cổng PR-SPRINT (cho kế hoạch) hoặc PR-MILESTONE (cho đánh giá milestone) chạy ở chế độ `full`, bỏ qua ở `lean`/`solo` |
| **SP3 — Đầu ra có cấu trúc** | Đầu ra sử dụng cấu trúc nhất quán (bảng velocity, danh sách rủi ro, đầu việc hành động) thay vì văn bản tự do |
| **SP4 — Không tự ý commit** | Skill không bao giờ ghi các file sprint hoặc bản ghi milestone mà không có "May I write" |

---

### `utility`

**Skills**: start, help, brainstorm, onboard, adopt, hotfix, prototype, localize, launch-checklist, release-checklist, smoke-check, soak-test, test-setup, test-helpers, regression-suite, qa-plan, bug-triage, bug-report, playtest-report, asset-spec, reverse-document, project-stage-detect, setup-engine, skill-test, skill-improve, day-one-patch, và các skill khác không thuộc danh mục trên

Các skill tiện ích phải vượt qua 7 bài kiểm tra tĩnh tiêu chuẩn. Nếu có gọi cổng director, logic chế độ cổng cũng phải chính xác.

| Chỉ số | Tiêu chí PASS |
|---|---|
| **U1 — Vượt qua 7 bài kiểm tra tĩnh** | `/skill-test static [name]` trả về COMPLIANT với 0 lỗi FAIL |
| **U2 — Đúng chế độ cổng (nếu có)** | Nếu skill có gọi bất kỳ cổng director nào, nó phải đọc review-mode và áp dụng logic full/lean/solo chính xác |

---

## Các danh mục Agent (Agent Categories)

Dùng để xác thực các file đặc tả agent trong `tests/agents/`.

### `director`

**Agents**: creative-director, technical-director, art-director, producer

| Chỉ số | Tiêu chí PASS |
|---|---|
| **D1 — Từ vựng kết luận chính xác** | Trả về APPROVE / CONCERNS / REJECT (hoặc từ tương đương: REALISTIC/CONCERNS/UNREALISTIC cho producer) |
| **D2 — Tôn trọng ranh giới lĩnh vực** | Không đưa ra quyết định ràng buộc ngoài lĩnh vực phụ trách đã khai báo |
| **D3 — Báo cáo khi có xung đột** | Khi hai phòng ban xung đột, báo cáo lên cấp quản lý chung phù hợp thay vì tự ý phán quyết đơn phương |
| **D4 — Phân tầng model Opus** | Agent được gán model Opus theo `coordination-rules.md` |

### `lead`

**Agents**: lead-programmer, qa-lead, narrative-director, audio-director, game-designer, systems-designer, level-designer

| Chỉ số | Tiêu chí PASS |
|---|---|
| **L1 — Kết luận theo lĩnh vực** | Trả về kết luận đặc thù theo lĩnh vực (ví dụ: FEASIBLE/INFEASIBLE cho lead-programmer, PASS/FAIL cho qa-lead) |
| **L2 — Báo cáo lên cấp quản lý chung** | Các xung đột ngoài lĩnh vực được báo cáo lên `creative-director` (thiết kế) hoặc `technical-director` (kỹ thuật) |
| **L3 — Phân tầng model Sonnet** | Agent được gán model Sonnet (mặc định) theo `coordination-rules.md` |

### `specialist`

**Agents**: gameplay-programmer, ai-programmer, technical-artist, sound-designer, engine-programmer, tools-programmer, network-programmer, security-engineer, accessibility-specialist, ux-designer, ui-programmer, performance-analyst, prototyper, qa-tester, writer, world-builder

| Chỉ số | Tiêu chí PASS |
|---|---|
| **S1 — Giữ đúng phạm vi lĩnh vực** | Giới hạn rõ ràng trong lĩnh vực khai báo; từ chối xử lý các yêu cầu ngoài phạm vi |
| **S2 — Không ra quyết định ràng buộc chéo lĩnh vực** | Không đơn phương quyết định các vấn đề thuộc quyền sở hữu của chuyên viên khác |
| **S3 — Chuyển tiếp đúng cách** | Các yêu cầu ngoài phạm vi được chuyển hướng đến đúng agent phụ trách, không từ chối trong im lặng |

### `engine`

**Agents**: godot-specialist, godot-gdscript-specialist, godot-csharp-specialist, godot-shader-specialist, godot-gdextension-specialist, unity-specialist, unity-ui-specialist, unity-shader-specialist, unity-dots-specialist, unity-addressables-specialist, unreal-specialist, ue-blueprint-specialist, ue-gas-specialist, ue-umg-specialist, ue-replication-specialist

| Chỉ số | Tiêu chí PASS |
|---|---|
| **E1 — Nhận biết phiên bản (Version-aware)** | Tham chiếu phiên bản engine từ `docs/engine-reference/` trước khi đề xuất gọi API; cảnh báo rủi ro sau ngày knowledge cutoff |
| **E2 — Điều hướng file** | Điều hướng các loại file tới đúng sub-specialist (ví dụ: `.gdshader` → `godot-shader-specialist`, không phải `godot-gdscript-specialist`) |
| **E3 — Mẫu thiết kế đặc thù theo Engine** | Thực thi các quy chuẩn đặc thù của engine (ví dụ: static typing trong GDScript, C# attribute exports, Blueprint function libraries) |

### `qa`

**Agents**: qa-tester, qa-lead, security-engineer, accessibility-specialist

| Chỉ số | Tiêu chí PASS |
|---|---|
| **Q1 — Tạo tài liệu sản phẩm, không viết code** | Đầu ra chính là test cases, bug reports, hoặc khoảng trống bao phủ — không phải code triển khai |
| **Q2 — Định dạng bằng chứng** | Test cases tuân theo định dạng bằng chứng test của dự án (unit/integration/visual/UI theo coding-standards.md) |
| **Q3 — Không tự ý phình to quy mô** | Không đề xuất các tính năng mới; đánh dấu khoảng trống để con người quyết định |

### `operations`

**Agents**: devops-engineer, release-manager, live-ops-designer, community-manager, analytics-engineer, economy-designer, localization-lead

| Chỉ số | Tiêu chí PASS |
|---|---|
| **O1 — Quyền sở hữu lĩnh vực rõ ràng** | Mô tả agent nêu rõ những gì nó nắm quyền quản lý (pipeline, releases, economy, v.v.) |
| **O2 — Chuyển giao việc triển khai code** | Không viết code gameplay hay engine; ủy quyền cho chuyên viên phù hợp |
| **O3 — Bộ công cụ khớp với vai trò** | `allowed-tools` trong frontmatter phù hợp với bản chất vận hành (không phải coding) của vai trò |
