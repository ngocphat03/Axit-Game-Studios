# Đặc tả Axit Workflow v1 (Axit Workflow Spec v1)

## Mục đích (Purpose)

Một Workflow là một **hợp đồng kết hợp và chuyển tiếp** tinh gọn, có thể tái sử dụng cho một kết quả có giới hạn.

Một Workflow trả lời các câu hỏi:

- Khi nào luồng công việc có thể bắt đầu?
- Những Skill hoặc trách nhiệm đã được chấp thuận nào tham gia?
- Các bước chạy theo thứ tự nào?
- Kết quả nào đưa luồng tiến lên, lặp lại, hoặc dừng lại?
- Điều kiện nào biểu thị workflow đã hoàn thành?

Một Workflow không sao chép quy trình bên trong Skill và không thay thế các ranh giới trách nhiệm của Profile.

## Vị trí chuẩn mực (Canonical location)

```text
.axit/core/workflows/<workflow-id>/WORKFLOW.md
```

Workflows là sản phẩm do Axit sở hữu. Chúng không phải là Codex Skill và không được hiển thị qua `.agents/skills/`.

Codex có thể được điều hướng tới một Workflow bởi `AGENTS.md`, yêu cầu của người dùng, hoặc một runtime Axit trong tương lai, nhưng workflow chuẩn mực vẫn nằm dưới `.axit/`.

## Frontmatter bắt buộc (Required frontmatter)

```yaml
---
spec_version: axit.workflow/v1
id: bounded-change
summary: Implement and independently verify one bounded change whose intent and architecture are already sufficiently resolved.
status: validation
---
```

Các trường bắt buộc:

- `spec_version` — phiên bản hợp đồng workflow.
- `id` — định danh ổn định viết thường nối dấu gạch ngang kebab-case.
- `summary` — một câu mô tả kết quả có giới hạn.
- `status` — trạng thái vòng đời như `validation` hoặc `active`.

Không thêm cấu hình provider, model, tool, hoặc quyền hạn runtime vào frontmatter của Workflow.

## Các phần thân bài bắt buộc (Required body sections)

### `# Entry Conditions`

Nêu rõ những điều kiện phải đúng trước khi Workflow bắt đầu.

Nếu một quyết định còn thiếu thuộc về trách nhiệm của Profile, Workflow nên dừng lại hoặc bàn giao thay vì tự ý ra quyết định trong im lặng.

### `# Participants`

Liệt kê các Skill hoặc lăng kính trách nhiệm đã được chấp thuận được Workflow sử dụng.

Tham chiếu chúng bằng ID ổn định. Không sao chép quy trình của chúng vào trong Workflow.

### `# Flow`

Mô tả sự kết hợp có thứ tự ở mức tổng quan.

Mỗi bước nên gọi hoặc điều hướng tới một Skill/trách nhiệm đã được chấp thuận và tiếp nhận đầu ra/bằng chứng hiện tại của bước trước.

### `# Transitions`

Xác định cách các kết quả cụ thể thay đổi trạng thái workflow.

Ví dụ:

- xác minh `PASS` -> hoàn thành;
- xác minh `FAIL` -> sửa chữa có giới hạn khi lỗi vẫn nằm trong phạm vi được chấp thuận;
- xác minh `BLOCKED` -> dừng lại cho đến khi điểm nghẽn được giải quyết.

Các chuyển tiếp không được che giấu việc phình to quy mô hoặc các quyết định thiết kế/kiến trúc quan trọng bên trong một vòng lặp tự động.

### `# Completion`

Xác định điều kiện có bằng chứng chứng minh rằng Workflow đã hoàn thành thành công.

Không để một bước triển khai tự tuyên bố workflow hoàn thành khi bước xác minh độc lập mới là bên nắm giữ kết luận cuối cùng.

### `# Stop / Handoff Conditions`

Nêu rõ khi nào Workflow thoát sang trách nhiệm khác, chờ bằng chứng/công cụ, hoặc dừng lại vì phạm vi thay đổi.

### `# Output`

Xác định các sản phẩm hoặc bằng chứng cuối cùng tối thiểu do Workflow trả về.

## Ranh giới Workflow

Một Core Workflow nên:

1. kết hợp các Core Skill hoặc trách nhiệm đã được chấp thuận;
2. duy trì tính hữu ích trên các dự án game khác biệt thực tế;
3. giải quyết một kết quả có giới hạn thay vì toàn bộ vòng đời dự án;
4. tránh sao chép các chỉ dẫn của Skill;
5. giữ tính chuyên môn hóa theo domain, engine, networking, và dự án nằm ngoài Core trừ khi việc tái sử dụng được chứng minh;
6. làm rõ ngữ nghĩa dừng/lặp/hoàn thành;
7. duy trì tính an toàn khi một bước bị bỏ qua vì trách nhiệm của nó đã được giải quyết ở nơi khác.

Một Workflow không nên:

- trở thành bản sao thứ hai của quy trình Skill;
- tự tạo ra quyền hạn runtime hoặc bỏ qua các quy tắc phê duyệt dự án;
- ép buộc công việc thiết kế hoặc kiến trúc vào mọi tác vụ triển khai;
- mã hóa các nghi thức sprint/phát hành thành hành vi phát triển game phổ quát;
- tạo ra các vòng lặp thử lại vô hạn;
- coi việc điều phối agent đặc thù của provider là kiến trúc chuẩn mực của Axit.

## Phân tách trách nhiệm (Separation of concerns)

```text
Profile     = trách nhiệm / lăng kính đánh giá
Skill       = quy trình có thể tái sử dụng
Workflow    = kết hợp + chuyển tiếp hướng tới một kết quả có giới hạn
Knowledge   = lý thuyết / tham chiếu / chỉ dẫn domain
Rules       = ràng buộc và phê duyệt của dự án
Registry    = chân lý kỹ thuật toàn dự án đã được chấp thuận
Runtime     = thực thi cấp quyền và kiểm soát tác dụng phụ thực tế
```

## Bài kiểm tra chất lượng Core Workflow

Trước khi một Workflow được đưa vào Core, tất cả câu trả lời phải là có:

1. Kết quả kết hợp này có lặp lại trên các dự án khác biệt thực tế hoặc các loại tác vụ phổ biến không?
2. Tất cả các Skill/trách nhiệm được tham chiếu đã được chấp thuận hoặc định nghĩa rõ ràng chưa?
3. Workflow có bổ sung ngữ nghĩa kết hợp/chuyển tiếp hữu ích thay vì chỉ liệt kê các bước không?
4. Điều kiện đầu vào, dừng, lặp và hoàn thành có rõ ràng không?
5. Các bước trách nhiệm tùy chọn có thể nằm ngoài Workflow khi không cần thiết không?
6. Việc hoàn thành cuối cùng có phụ thuộc vào đúng trách nhiệm nắm giữ bằng chứng không?
7. Workflow có đủ nhỏ gọn để hiểu mà không cần nạp các tài liệu vòng đời dự án không liên quan không?

Nếu không, hãy giữ sự kết hợp ở dạng không chính thức cho đến khi việc sử dụng thực tế chứng minh một Workflow ổn định.