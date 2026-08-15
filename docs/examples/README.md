# Các ví dụ về phiên làm việc cộng tác (Collaborative Session Examples)

Thư mục này chứa các bản ghi chép (transcripts) phiên làm việc thực tế, end-to-end, minh họa cách thức hoạt động của Kiến trúc Game Studio Agent trong thực tế. Mỗi ví dụ thể hiện **quy trình làm việc cộng tác (collaborative workflow)** nơi các agent đặt câu hỏi, đưa ra các lựa chọn, và chờ sự phê duyệt của người dùng thay vì tự ý sinh nội dung một cách đơn phương.

---

## Tham chiếu trực quan (Visual Reference)

**Bạn mới làm quen với hệ thống? Hãy bắt đầu tại đây:**
[Sơ đồ luồng Skill (Skill Flow Diagrams)](skill-flow-diagrams.md) — bản đồ trực quan về toàn bộ 7 giai đoạn và cách các skill liên kết với nhau.

---

## 📚 **Các ví dụ hiện có**

### WORKFLOW CỐT LÕI

### [Sơ đồ luồng Skill (Skill Flow Diagrams)](skill-flow-diagrams.md)
**Loại:** Tham chiếu trực quan  
**Độ phức tạp:** Mọi cấp độ  

Tổng quan toàn bộ pipeline (từ số 0 đến phát hành), kèm theo các sơ đồ chuỗi chi tiết cho:
design-system, vòng đời story, pipeline UX, và tiếp nhận dự án cũ (brownfield).  
**Bắt đầu từ đây nếu bạn muốn hiểu cách các thành phần khớp nối với nhau.**

---

### [Phiên làm việc: Soạn thảo GDD với /design-system](session-design-system-skill.md)
**Loại:** Thiết kế (định hướng bởi skill)  
**Skill:** `/design-system`  
**Thời lượng:** ~60 phút (14 lượt thoại)  
**Độ phức tạp:** Trung bình  

**Kịch bản:**
Lập trình viên chạy `/design-system movement` sau khi `/map-systems` đã tạo ra systems index. Skill tải ngữ cảnh từ concept game và các GDD phụ thuộc, chạy kiểm tra sơ bộ tính khả thi kỹ thuật, sau đó dẫn dắt qua toàn bộ 8 phần của GDD từng phần một — soạn thảo, phê duyệt và ghi từng phần vào đĩa trước khi chuyển sang phần tiếp theo.

**Các khoảnh khắc chính:**
- Kiểm tra tính khả thi kỹ thuật phát hiện thay đổi mặc định của Jolt physics (Godot 4.6)
- Ghi file tăng dần (Incremental writing): mỗi phần được lưu trên đĩa ngay sau khi duyệt
- Phiên làm việc bị crash ở phần 5 → agent khôi phục tiếp tục từ phần trống đầu tiên
- Các tín hiệu phụ thuộc (stamina, inventory) được hiển thị trong mục Phụ thuộc (Dependencies)
- Kết thúc bằng việc bàn giao rõ ràng: "chạy `/design-review` trước khi làm hệ thống tiếp theo"

**Những gì bạn học được:**
- `/design-system` khác biệt thế nào so với việc chỉ bảo agent "viết một GDD"
- Chu kỳ từng phần ngăn chặn việc phình to ngữ cảnh 30k token ra sao
- Ghi file tăng dần giúp sống sót qua các sự cố crash phiên làm việc thế nào
- Skill làm nổi bật các hợp đồng phụ thuộc hạ nguồn như thế nào

---

### [Phiên làm việc: Vòng đời Story hoàn chỉnh](session-story-lifecycle.md)
**Loại:** Toàn bộ Workflow  
**Skills:** `/story-readiness` → triển khai code → `/story-done`  
**Thời lượng:** ~50 phút (13 lượt thoại)  
**Độ phức tạp:** Trung bình  

**Kịch bản:**
Lập trình viên nhận một story từ sprint backlog. `/story-readiness` phát hiện điểm mơ hồ về hướng lăn (roll-direction) trước khi bất kỳ dòng code nào được viết. Sau khi triển khai xong, `/story-done` xác minh 9 tiêu chí chấp nhận, xác định 2 tiêu chí bị hoãn lại (do inventory chưa tích hợp), và đóng story kèm ghi chú.

**Các khoảnh khắc chính:**
- `/story-readiness` bắt lỗi mơ hồ trong đặc tả ở Lượt 2 — được giải quyết trước khi code bắt đầu
- Kiểm tra trạng thái ADR: story sẽ bị BLOCKED nếu ADR vẫn đang ở trạng thái Proposed
- Kiểm tra phiên bản Manifest: xác nhận chỉ dẫn của story không bị trôi lệch so với kiến trúc hiện tại
- Tiêu chí hoãn lại được theo dõi (không bị mất) khi việc tích hợp chưa khả thi
- `sprint-status.yaml` được cập nhật khi đóng story, story sẵn sàng tiếp theo tự động hiển thị

**Những gì bạn học được:**
- Vì sao `/story-readiness` ngăn chặn sự mơ hồ muộn màng trong khâu triển khai
- Cách thức hoạt động của các tiêu chí hoãn lại (COMPLETE WITH NOTES vs. BLOCKED)
- Cách tham chiếu mã TR-ID ngăn chặn cảnh báo sai lệch không đáng có
- Vòng lặp trọn vẹn từ backlog → triển khai → đóng story

---

### [Phiên làm việc: Kiểm tra cổng và Chuyển giao giai đoạn](session-gate-check-phase-transition.md)
**Loại:** Cổng giai đoạn (Phase Gate)  
**Skill:** `/gate-check`  
**Thời lượng:** ~20 phút (7 lượt thoại)  
**Độ phức tạp:** Thấp  

**Kịch bản:**
Lập trình viên hoàn thành giai đoạn Thiết kế Hệ thống và chạy `/gate-check` để tiến bước. Cổng kiểm tra thấy tất cả 6 GDD MVP đã hoàn thiện, đánh giá chéo đã vượt qua với một lưu ý mức độ thấp. Cổng PASS, `stage.txt` được cập nhật, và agent cung cấp checklist có thứ tự cụ thể cho Thiết lập Kỹ thuật.

**Các khoảnh khắc chính:**
- Cổng xác thực sự hiện diện của tài liệu VÀ tính hoàn thiện nội bộ (đủ 8 phần mỗi GDD)
- CONCERNS ≠ FAIL: ghi chú đánh giá chéo mức độ thấp vẫn vượt qua cổng
- Cập nhật `stage.txt` thay đổi những gì `/help`, `/sprint-status`, và tất cả các skill nhìn thấy về sau
- Agent biến lưu ý đánh giá chéo thành một ADR cụ thể cần viết tiếp theo
- Checklist giai đoạn tiếp theo rất cụ thể và có thứ tự, không chung chung

**Những gì bạn học được:**
- Kiểm tra cổng thực sự xác thực những gì (không chỉ là "file có tồn tại không?")
- Cách thức hoạt động của các kết luận PASS/CONCERNS/FAIL
- Tại sao `stage.txt` là căn cứ chính thức để theo dõi giai đoạn
- Những gì thay đổi sau khi chuyển giai đoạn

---

### [Phiên làm việc: Pipeline UX — /ux-design → /ux-review → /team-ui](session-ux-pipeline.md)
**Loại:** UX Design Pipeline  
**Skills:** `/ux-design`, `/ux-review`, `/team-ui`  
**Thời lượng:** ~90 phút (16 lượt thoại)  
**Độ phức tạp:** Trung bình - Cao  

**Kịch bản:**
Lập trình viên thiết kế HUD và màn hình túi đồ. `/ux-design` đọc hành trình người chơi và GDD để neo các quyết định vào trạng thái cảm xúc của người chơi. `/ux-review` phát hiện khoảng trống accessibility nghiêm trọng (không có phương án bàn phím thay thế cho kéo-thả) và một vấn đề mù màu mức khuyến cáo. Sau khi sửa, `/team-ui` tiếp nhận bàn giao.

**Các khoảnh khắc chính:**
- Lựa chọn triết lý HUD (diegetic vs. persistent vs. tactical) gắn liền với thể loại sinh tồn
- `/ux-review` phân biệt rõ BLOCKING (dừng bàn giao) vs. ADVISORY (có thể sửa ở đợt làm đồ họa)
- Lỗi Accessibility được phát hiện trước khi code, không phải đợi đến khâu QA
- Bổ sung phương án bàn phím thay thế trong 1 lượt; review chạy lại và vượt qua
- `/team-ui` kiểm tra kết quả `/ux-review` đã PASS trước khi bắt đầu thiết kế hình ảnh

**Những gì bạn học được:**
- Cách `/ux-design` sử dụng ngữ cảnh hành trình người chơi để đưa ra quyết định UI
- `/ux-review` thực sự kiểm tra những gì (không chỉ là "đặc tả có tồn tại không?")
- Sự khác biệt giữa tài liệu HUD tổng quan (`design/ux/hud.md`) và đặc tả từng màn hình
- Cách các vấn đề accessibility được xử lý ở khâu thiết kế so với khâu triển khai

---

### [Phiên làm việc: Tiếp nhận dự án có sẵn với /adopt](session-adopt-brownfield.md)
**Loại:** Tiếp nhận dự án có sẵn (Brownfield Adoption)  
**Skill:** `/adopt`  
**Thời lượng:** ~30 phút (8 lượt thoại)  
**Độ phức tạp:** Thấp - Trung bình  

**Kịch bản:**
Lập trình viên có 3 tháng code sẵn và các ghi chú thiết kế sơ sài nhưng chưa đúng định dạng. `/adopt` audit tính tuân thủ định dạng (không chỉ sự tồn tại của file), phân loại 4 khoảng trống theo mức độ nghiêm trọng, xây dựng kế hoạch migration 7 bước có thứ tự, và lập tức khắc phục khoảng trống BLOCKING (thiếu systems index) bằng cách suy luận từ codebase.

**Các khoảnh khắc chính:**
- FORMAT audit phân biệt "file có tồn tại" với "file có cấu trúc nội bộ bắt buộc"
- Xác định khoảng trống BLOCKING: thiếu systems index khiến hơn 4 skill không thể chạy
- Kế hoạch migration có thứ tự: khoảng trống chặn trước, sau đó đến mức cao, rồi trung bình
- Systems index được khởi tạo từ cấu trúc code — code cũ chứa sẵn câu trả lời
- Chế độ Retrofit so với tạo mới: `/design-system retrofit` bổ sung khoảng trống mà không ghi đè

**Những gì bạn học được:**
- Sự khác biệt giữa `/adopt` và `/project-stage-detect`
- Cách kiểm tra tuân thủ định dạng (phát hiện các mục, không chỉ kiểm tra có file)
- Cách các dự án cũ có thể onboarding mà không làm mất code/tài liệu hiện có
- Khi nào dùng chế độ retrofit so với tạo mới hoàn toàn

---

### CÁC VÍ DỤ NỀN TẢNG

### [Phiên làm việc: Thiết kế hệ thống chế tạo (Crafting System)](session-design-crafting-system.md)
**Loại:** Thiết kế  
**Agent:** `game-designer`  
**Thời lượng:** ~45 phút (12 lượt thoại)  
**Độ phức tạp:** Trung bình  

**Kịch bản:**
Solo dev cần thiết kế hệ thống chế tạo phục vụ Trụ cột 2 ("Khám phá bất ngờ thông qua thử nghiệm"). Agent dẫn dắt qua hỏi/đáp, trình bày 3 phương án thiết kế kèm phân tích lý thuyết game, tiếp thu các chỉnh sửa của người dùng, và soạn thảo GDD tăng dần có phê duyệt ở từng bước.

**Các khoảnh khắc cộng tác chính:**
- Agent đặt trước 5 câu hỏi làm rõ
- Trình bày 3 phương án khác biệt kèm ưu/nhược điểm + độ khớp MDA
- Người dùng chỉnh sửa phương án được khuyến nghị, agent cập nhật ngay lập tức
- Chủ động cảnh báo trường hợp biên ("nếu kết hợp không ra công thức thì sao?")
- Từng phần GDD được trình bày để phê duyệt trước khi chuyển sang phần tiếp
- Hỏi rõ ràng "Tôi có thể ghi vào [file] không?" trước khi tạo file

---

### [Phiên làm việc: Triển khai tính toán sát thương chiến đấu](session-implement-combat-damage.md)
**Loại:** Triển khai code (Implementation)  
**Agent:** `gameplay-programmer`  
**Thời lượng:** ~30 phút (10 lượt thoại)  
**Độ phức tạp:** Thấp - Trung bình  

**Kịch bản:**
Người dùng có tài liệu thiết kế hoàn chỉnh và muốn triển khai tính toán sát thương. Agent đọc đặc tả, xác định 7 điểm mơ hồ/khoảng trống, đặt câu hỏi làm rõ, đề xuất kiến trúc để duyệt, triển khai code kèm thực thi quy tắc, và chủ động viết test.

**Các khoảnh khắc cộng tác chính:**
- Agent đọc tài liệu thiết kế trước, xác định 7 điểm đặc tả chưa rõ
- Đề xuất kiến trúc kèm mẫu code TRƯỚC KHI triển khai
- Người dùng yêu cầu type safety, agent tinh chỉnh và đề xuất lại
- Rules bắt lỗi (giá trị hardcode), agent sửa chữa minh bạch
- Chủ động viết test theo phương pháp Verification-Driven Development
- Agent đưa ra các lựa chọn cho bước tiếp theo thay vì tự ý suy đoán

---

### [Phiên làm việc: Khủng hoảng quy mô - Ra quyết định chiến lược](session-scope-crisis-decision.md)
**Loại:** Quyết định chiến lược  
**Agent:** `creative-director`  
**Thời lượng:** ~25 phút (8 lượt thoại)  
**Độ phức tạp:** Cao  

**Kịch bản:**
Solo dev đối mặt khủng hoảng: Milestone Alpha chỉ còn 2 tuần, hệ thống chế tạo cần 3 tuần, bản demo cho nhà đầu tư mang tính sống còn. Creative director thu thập ngữ cảnh, đóng khung quyết định, trình bày 3 phương án chiến lược kèm phân tích đánh đổi trung thực, đưa ra khuyến nghị nhưng trao quyền cho người dùng, sau đó ghi nhận quyết định bằng ADR và kịch bản demo.

**Các khoảnh khắc cộng tác chính:**
- Agent đọc tài liệu ngữ cảnh trước khi đề xuất giải pháp
- Đặt 5 câu hỏi để hiểu các ràng buộc quyết định
- Đóng khung quyết định chuẩn mực (những gì đang bị đe dọa, tiêu chí đánh giá)
- Trình bày 3 phương án kèm phân tích rủi ro và tiền lệ lịch sử
- Đưa ra khuyến nghị mạnh mẽ nhưng nhấn mạnh rõ: "đây là quyết định của bạn"
- Ghi nhận quyết định + cung cấp kịch bản demo hỗ trợ người dùng

---

### [Workflow tài liệu hóa ngược (Reverse Documentation)](reverse-document-workflow-example.md)
**Loại:** Tài liệu hóa dự án cũ (Brownfield)  
**Agent:** `game-designer`  
**Thời lượng:** ~20 phút  
**Độ phức tạp:** Thấp  

**Kịch bản:**
Lập trình viên đã xây dựng hệ thống cây kỹ năng nhưng chưa từng viết design doc. Agent đọc code, suy luận ý đồ thiết kế, hỏi các câu hỏi làm rõ về các quyết định còn mơ hồ, và tạo ra GDD hồi tố (retroactive GDD).

---

## 🎯 **Những gì các ví dụ này chứng minh**

Tất cả các ví dụ đều tuân theo **mô hình quy trình cộng tác:**

```
Hỏi (Question) → Lựa chọn (Options) → Quyết định (Decision) → Bản thảo (Draft) → Phê duyệt (Approval)
```

> **Lưu ý:** Các ví dụ này minh họa mô hình cộng tác dưới dạng văn bản hội thoại.
> Trong thực tế, các agent hiện sử dụng công cụ `AskUserQuestion` tại các điểm quyết định để
> hiển thị các bộ chọn phương án có cấu trúc (kèm nhãn, mô tả và chọn nhiều).
> Mô hình là **Giải thích → Ghi nhận (Explain → Capture)**: agent giải thích phân tích trong
> hội thoại trước, sau đó hiển thị UI picker có cấu trúc để người dùng ra quyết định.

### ✅ **Các hành vi cộng tác được thể hiện:**

1. **Agent hỏi trước khi giả định**
2. **Agent đưa ra các lựa chọn, không áp đặt**
3. **Agent trình bày công việc trước khi hoàn tất**
4. **Agent xin phê duyệt trước khi ghi file**
5. **Agent lặp lại dựa trên phản hồi**

---

## 📖 **Cách sử dụng các ví dụ này**

### Dành cho người dùng mới:
Đọc các ví dụ này TRƯỚC phiên làm việc đầu tiên của bạn để thiết lập kỳ vọng thực tế:
- Agent là chuyên gia tư vấn, không phải người tự ý thực thi đơn phương
- Bạn đưa ra mọi quyết định sáng tạo / chiến lược
- Agent cung cấp chỉ dẫn chuyên môn và các lựa chọn

---

## 📝 **Tài nguyên bổ sung**

- **Tài liệu Nguyên tắc đầy đủ:** [docs/COLLABORATIVE-DESIGN-PRINCIPLE.md](../COLLABORATIVE-DESIGN-PRINCIPLE.md)
- **Hướng dẫn Workflow:** [docs/WORKFLOW-GUIDE.md](../WORKFLOW-GUIDE.md)
- **Danh sách Agent:** [.claude/docs/agent-roster.md](../../.claude/docs/agent-roster.md)
- **CLAUDE.md (Giao thức cộng tác):** [CLAUDE.md](../../CLAUDE.md#collaboration-protocol)
