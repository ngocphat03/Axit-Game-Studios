# Bản đồ Điều phối và Ủy quyền Agent (Agent Coordination and Delegation Map)

## Cơ cấu Tổ chức (Organizational Hierarchy)

```
                           [Lập trình viên / Con người]
                                  |
                  +---------------+---------------+
                  |               |               |
          creative-director  technical-director  producer
                  |               |               |
         +--------+--------+      |        (điều phối tất cả)
         |        |        |      |
   game-designer art-dir  narr-dir  lead-programmer  qa-lead  audio-dir
         |        |        |         |                |        |
      +--+--+     |     +--+--+  +--+--+--+--+--+   |        |
      |  |  |     |     |     |  |  |  |  |  |  |   |        |
     sys lvl eco  ta   wrt  wrld gp ep  ai net tl ui qa-t    snd
                                  |
                              +---+---+
                              |       |
                           perf-a   devops   analytics

  Các Lead bổ sung (báo cáo cho producer/directors):
    release-manager         -- Pipeline phát hành, phân phiên bản, triển khai
    localization-lead       -- Quốc tế hóa i18n, bảng chuỗi, pipeline dịch thuật
    prototyper              -- Tạo prototype nhanh, kiểm chứng ý tưởng
    security-engineer       -- Chống hack/cheat, ngăn chặn khai thác, bảo mật mạng
    accessibility-specialist -- Tuân thủ WCAG, hỗ trợ mù màu, gán phím, phóng to chữ
    live-ops-designer       -- Mùa giải, sự kiện, battle passes, giữ chân, kinh tế live
    community-manager       -- Patch notes, phản hồi người chơi, xử lý khủng hoảng

  Chuyên viên Engine (sử dụng BỘ tương ứng với engine của bạn):
    unreal-specialist  -- Lead UE5: Blueprint/C++, tổng quan GAS, hệ thống con UE
      ue-gas-specialist         -- GAS: kỹ năng, hiệu ứng, thuộc tính, tags, đoán trước
      ue-blueprint-specialist   -- Blueprint: ranh giới BP/C++, chuẩn đồ thị, tối ưu
      ue-replication-specialist -- Mạng: replication, RPCs, đoán trước, băng thông
      ue-umg-specialist         -- UI: UMG, CommonUI, phân cấp widget, data binding

    unity-specialist   -- Lead Unity: MonoBehaviour/DOTS, Addressables, URP/HDRP
      unity-dots-specialist         -- DOTS/ECS: Jobs, Burst, hybrid renderer
      unity-shader-specialist       -- Shaders: Shader Graph, VFX Graph, SRP
      unity-addressables-specialist -- Asset: tải bất đồng bộ, bundles, bộ nhớ, CDN
      unity-ui-specialist           -- UI: UI Toolkit, UGUI, UXML/USS, data binding

    godot-specialist   -- Lead Godot 4: GDScript, node/scene, signals, resources
      godot-gdscript-specialist    -- GDScript: static typing, design patterns, hiệu năng
      godot-csharp-specialist      -- C#: mẫu thiết kế .NET, delegates [Signal], async
      godot-shader-specialist      -- Shaders: ngôn ngữ shader Godot, visual shaders, VFX
      godot-gdextension-specialist -- Native: liên kết C++/Rust, GDExtension, hệ thống build
```

### Chú giải (Legend)
```
sys  = systems-designer       gp  = gameplay-programmer
lvl  = level-designer         ep  = engine-programmer
eco  = economy-designer       ai  = ai-programmer
ta   = technical-artist       net = network-programmer
wrt  = writer                 tl  = tools-programmer
wrld = world-builder          ui  = ui-programmer
snd  = sound-designer         qa-t = qa-tester
narr-dir = narrative-director perf-a = performance-analyst
art-dir = art-director
```

## Quy tắc Ủy quyền (Delegation Rules)

### Ai có thể ủy quyền cho ai

| Từ Agent | Có thể ủy quyền cho |
|---|---|
| creative-director | game-designer, art-director, audio-director, narrative-director |
| technical-director | lead-programmer, devops-engineer, performance-analyst, technical-artist (quyết định kỹ thuật) |
| producer | Bất kỳ agent nào (chỉ phân công nhiệm vụ trong phạm vi lĩnh vực của họ) |
| game-designer | systems-designer, level-designer, economy-designer |
| lead-programmer | gameplay-programmer, engine-programmer, ai-programmer, network-programmer, tools-programmer, ui-programmer |
| art-director | technical-artist, ux-designer |
| audio-director | sound-designer |
| narrative-director | writer, world-builder |
| qa-lead | qa-tester |
| release-manager | devops-engineer (bản build phát hành), qa-lead (kiểm thử phát hành) |
| localization-lead | writer (duyệt chuỗi dịch), ui-programmer (vừa vặn khung chữ) |
| prototyper | (làm việc độc lập, báo cáo phát hiện cho producer và các lead liên quan) |
| security-engineer | network-programmer (đánh giá bảo mật), lead-programmer (mẫu thiết kế an toàn) |
| accessibility-specialist | ux-designer (mẫu tiếp cận), ui-programmer (triển khai), qa-tester (test a11y) |
| [engine]-specialist | các chuyên viên hệ thống con engine (ủy quyền công việc đặc thù hệ thống con) |
| các chuyên viên hệ thống con [engine] | (tư vấn cho tất cả lập trình viên về mẫu thiết kế và tối ưu hệ thống con) |
| live-ops-designer | economy-designer (kinh tế live), community-manager (thông báo sự kiện), analytics-engineer (chỉ số tương tác) |
| community-manager | (làm việc với producer để xin duyệt, release-manager về thời điểm phát hành patch notes) |

### Các tuyến báo cáo xung đột (Escalation Paths)

| Tình huống | Báo cáo lên |
|---|---|
| Hai nhà thiết kế bất đồng về một cơ chế | game-designer |
| Xung đột giữa thiết kế game và cốt truyện | creative-director |
| Xung đột giữa thiết kế game và tính khả thi kỹ thuật | producer (điều phối), sau đó creative-director + technical-director |
| Xung đột tông điệu giữa mỹ thuật và âm thanh | creative-director |
| Bất đồng quan điểm về kiến trúc mã nguồn | technical-director |
| Xung đột code liên hệ thống | lead-programmer, sau đó technical-director |
| Xung đột lịch trình giữa các phòng ban | producer |
| Quy mô vượt quá năng lực xử lý | producer, sau đó creative-director để cắt giảm |
| Bất đồng quan điểm về cổng chất lượng | qa-lead, sau đó technical-director |
| Vi phạm ngân sách hiệu năng | performance-analyst cảnh báo, technical-director quyết định |

## Các mô hình quy trình phổ biến (Common Workflow Patterns)

### Mô hình 1: Tính năng mới (Toàn bộ Pipeline)

```
1. creative-director  -- Phê duyệt concept tính năng phù hợp với tầm nhìn
2. game-designer      -- Tạo tài liệu thiết kế kèm đặc tả đầy đủ
3. producer           -- Lên lịch trình, xác định phụ thuộc
4. lead-programmer    -- Thiết kế kiến trúc code, phác thảo interface
5. [chuyên viên lập trình] -- Triển khai code tính năng
6. technical-artist   -- Triển khai hiệu ứng hình ảnh (nếu cần)
7. writer             -- Tạo nội dung văn bản (nếu cần)
8. sound-designer     -- Tạo danh sách sự kiện âm thanh (nếu cần)
9. qa-tester          -- Viết test cases
10. qa-lead           -- Đánh giá và phê duyệt độ bao phủ kiểm thử
11. lead-programmer   -- Code review
12. qa-tester         -- Chạy các bài test
13. producer          -- Đánh dấu hoàn thành tác vụ
```

### Mô hình 2: Sửa lỗi (Bug Fix)

```
1. qa-tester          -- Tạo báo cáo bug với /bug-report
2. qa-lead            -- Phân loại mức độ nghiêm trọng và ưu tiên
3. producer           -- Gán vào sprint (nếu không phải lỗi S1 khẩn cấp)
4. lead-programmer    -- Xác định nguyên nhân gốc rễ, phân công cho lập trình viên
5. [chuyên viên lập trình] -- Sửa lỗi
6. lead-programmer    -- Code review
7. qa-tester          -- Xác minh bản sửa lỗi và chạy test hồi quy
8. qa-lead            -- Đóng bug
```

### Mô hình 3: Điều chỉnh cân bằng (Balance Adjustment)

```
1. analytics-engineer -- Xác định mất cân bằng từ dữ liệu (hoặc phản hồi người chơi)
2. game-designer      -- Đánh giá vấn đề đối chiếu với ý đồ thiết kế
3. economy-designer   -- Mô hình hóa việc điều chỉnh
4. game-designer      -- Phê duyệt các giá trị mới
5. [cập nhật file dữ liệu] -- Thay đổi giá trị cấu hình
6. qa-tester          -- Test hồi quy các hệ thống bị ảnh hưởng
7. analytics-engineer -- Theo dõi các chỉ số sau thay đổi
```

### Mô hình 4: Màn chơi / Khu vực mới (New Area/Level)

```
1. narrative-director -- Xác định mục đích cốt truyện và nhịp cảm xúc của khu vực
2. world-builder      -- Tạo bối cảnh lore và môi trường
3. level-designer     -- Thiết kế bố cục, các màn chạm trán, nhịp độ
4. game-designer      -- Đánh giá thiết kế cơ chế của các màn chạm trán
5. art-director       -- Xác định định hướng hình ảnh cho khu vực
6. audio-director     -- Xác định định hướng âm thanh cho khu vực
7. [triển khai bởi các lập trình viên và họa sĩ liên quan]
8. writer             -- Tạo nội dung văn bản đặc thù của khu vực
9. qa-tester          -- Kiểm thử toàn bộ khu vực
```

### Mô hình 5: Chu kỳ Sprint

```
1. producer           -- Lên kế hoạch sprint với /sprint-plan new
2. [Tất cả agents]    -- Thực thi các tác vụ được phân công
3. producer           -- Cập nhật trạng thái hàng ngày với /sprint-status
4. qa-lead            -- Kiểm thử liên tục trong suốt sprint
5. lead-programmer    -- Code review liên tục trong suốt sprint
6. producer           -- Chạy retrospective sau sprint
7. producer           -- Lên kế hoạch sprint tiếp theo tích hợp các bài học rút ra
```

### Mô hình 6: Điểm kiểm tra Milestone

```
1. producer           -- Chạy /milestone-review
2. creative-director  -- Đánh giá tiến độ sáng tạo
3. technical-director -- Đánh giá sức khỏe kỹ thuật
4. qa-lead            -- Đánh giá các chỉ số chất lượng
5. producer           -- Điều phối thảo luận tiếp tục hay dừng lại (go/no-go)
6. [Tất cả directors] -- Thống nhất điều chỉnh quy mô nếu cần
7. producer           -- Ghi nhận quyết định và cập nhật kế hoạch
```

### Mô hình 7: Pipeline phát hành (Release Pipeline)

```text
1. producer             -- Tuyên bố bản release candidate, xác nhận đạt tiêu chí milestone
2. release-manager      -- Cắt nhánh release, tạo /release-checklist
3. qa-lead              -- Chạy toàn bộ test hồi quy, ký duyệt chất lượng
4. localization-lead    -- Xác minh tất cả chuỗi ngôn ngữ đã dịch, vừa vặn khung chữ
5. performance-analyst  -- Xác nhận hiệu năng đạt chuẩn mục tiêu
6. devops-engineer      -- Build các sản phẩm release, chạy pipeline triển khai
7. release-manager      -- Tạo /changelog, gắn tag release, tạo release notes
8. technical-director   -- Ký duyệt cuối cùng cho các bản phát hành lớn
9. release-manager      -- Triển khai và giám sát trong 48 giờ
10. producer            -- Đánh dấu hoàn tất phát hành
```

### Mô hình 8: Concept Prototype (sớm — trước GDD)

```text
1. game-designer        -- Xác định giả thuyết và tiêu chí thành công
2. prototyper           -- Dựng khung prototype với /prototype
3. prototyper           -- Xây dựng bản triển khai tối thiểu (1-3 ngày)
4. game-designer        -- Đánh giá prototype đối chiếu với tiêu chí
5. prototyper           -- Ghi nhận phát hiện vào REPORT.md
6. creative-director    -- Quyết định TIẾP TỤC (PROCEED) / ĐỔI HƯỚNG (PIVOT) / HỦY (KILL) (chế độ full)
7. game-designer        -- Cung cấp bài học từ prototype cho việc viết GDD nếu PROCEED
```

### Mô hình 8b: Vertical Slice (tiền sản xuất — sau GDD và kiến trúc)

```text
1. game-designer        -- Xác nhận phạm vi bản slice đối chiếu với GDD
2. prototyper           -- Xây dựng bản build end-to-end chất lượng sản xuất với /vertical-slice
3. prototyper           -- Tiến hành các phiên playtest nội bộ (tối thiểu 1 phiên)
4. prototyper           -- Ghi nhận phát hiện vào REPORT.md
5. creative-director    -- Quyết định go/no-go về việc tiến sang Sản xuất (chế độ full)
6. producer             -- Lên lịch các epic/sprint Sản xuất nếu PROCEED
```

### Mô hình 9: Sự kiện trực tuyến / Ra mắt mùa giải

```text
1. live-ops-designer     -- Thiết kế nội dung sự kiện/mùa giải, phần thưởng, lịch trình
2. game-designer         -- Xác thực cơ chế gameplay cho sự kiện
3. economy-designer      -- Cân bằng kinh tế sự kiện và giá trị phần thưởng
4. narrative-director    -- Cung cấp chủ đề cốt truyện theo mùa
5. writer                -- Tạo mô tả sự kiện và lore
6. producer              -- Lên lịch trình triển khai
7. [triển khai bởi các lập trình viên liên quan]
8. qa-lead               -- Kiểm thử toàn bộ luồng sự kiện end-to-end
9. community-manager     -- Soạn thảo thông báo sự kiện và patch notes
10. release-manager      -- Triển khai nội dung sự kiện
11. analytics-engineer   -- Giám sát mức độ tham gia và các chỉ số
12. live-ops-designer    -- Phân tích và rút kinh nghiệm sau sự kiện
```

## Các phản hồi phản mẫu (Anti-Patterns) cần tránh

1. **Nhảy cóc phân tầng (Bypassing the hierarchy)**: Một chuyên viên tuyệt đối không bao giờ được đưa ra quyết định thuộc thẩm quyền của lead mà không có sự tham vấn.
2. **Triển khai chéo lĩnh vực (Cross-domain implementation)**: Một agent tuyệt đối không bao giờ được chỉnh sửa file ngoài phạm vi được chỉ định khi chưa có ủy quyền rõ ràng từ chủ sở hữu liên quan.
3. **Quyết định ngầm (Shadow decisions)**: Mọi quyết định phải được ghi lại thành văn bản. Thỏa thuận miệng không có lưu trữ sẽ dẫn đến mâu thuẫn.
4. **Tác vụ khổng lồ (Monolithic tasks)**: Mọi tác vụ phân công cho một agent nên có thể hoàn thành trong 1-3 ngày. Nếu lớn hơn, bắt buộc phải chia nhỏ trước.
5. **Triển khai dựa trên suy đoán (Assumption-based implementation)**: Nếu đặc tả còn mơ hồ, người triển khai phải hỏi lại người đưa ra đặc tả thay vì đoán mò. Đoán sai gây tốn kém hơn nhiều so với việc đặt câu hỏi.
