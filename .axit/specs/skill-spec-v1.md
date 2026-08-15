# Đặc tả Axit Skill v1 (Axit Skill Spec v1)

## Mục đích (Purpose)

Một Skill là một **quy trình có thể tái sử dụng, tinh gọn cho một công việc lặp lại đơn lẻ**.

Một Skill trả lời các câu hỏi:

- Khi nào quy trình này nên chạy?
- Nó cần những đầu vào hoặc bằng chứng nào?
- Những hành động có thứ tự nào cần được thực hiện?
- Những điều kiện nào đòi hỏi phải dừng lại hoặc bàn giao?
- Đầu ra hoặc bằng chứng nào cần được tạo ra?

Một Skill không sở hữu một trách nhiệm bao quát. Trách nhiệm bao quát thuộc về Profiles.

## Vị trí chuẩn mực (Canonical location)

```text
.axit/core/skills/<skill-id>/SKILL.md
```

Một Core Skill cũng phải hợp lệ cho việc khám phá của Codex. File `SKILL.md` chuẩn mực là nguồn sự thật; `.agents/skills/` chỉ là tầng tương thích phục vụ khám phá.

## Frontmatter bắt buộc (Required frontmatter)

Sử dụng mức tối thiểu tương thích với Codex:

```yaml
---
name: verify-change
description: Verify a bounded code or project change against accepted requirements and current evidence, then return PASS, FAIL, or BLOCKED. Use after implementation or when asked whether a feature/fix is actually complete; do not use as the implementation workflow itself.
---
```

Các trường bắt buộc:

- `name` — định danh ổn định viết thường nối dấu gạch ngang kebab-case.
- `description` — văn bản mô tả điều kiện kích hoạt và ranh giới súc tích. Nêu rõ cả khi nào Skill nên chạy và trường hợp quan trọng nhất mà nó không nên chạy.

Không thêm các trường frontmatter chỉ dành riêng cho Axit trừ khi tính tương thích Codex đã được xác minh và có nhu cầu thực tế.

## Các phần thân bài bắt buộc (Required body sections)

### `# Purpose`

Nêu rõ công việc lặp lại đơn lẻ do Skill thực hiện.

### `# Inputs`

Liệt kê các danh mục đầu vào mang tính ngữ nghĩa, không hardcode đường dẫn của một repository cụ thể trừ khi quy trình thực sự đòi hỏi.

### `# Procedure`

Cung cấp các bước thực hiện có thứ tự rõ ràng. Giữ cho quy trình tập trung vào một công việc duy nhất.

Các bước có thể rẽ nhánh dựa trên bằng chứng hoặc capability của dự án, nhưng không nên biến thành toàn bộ vòng đời dự án end-to-end.

### `# Stop / Handoff Conditions`

Nêu rõ khi nào quy trình phải dừng lại, trả về kết quả bị chặn (blocked), hoặc bàn giao vấn đề cho trách nhiệm khác.

### `# Output`

Xác định kết quả/bằng chứng tối thiểu do quy trình tạo ra.

## Các phần thân bài tùy chọn (Optional body sections)

Chỉ sử dụng khi thực sự hữu ích:

- `# Evidence Rules`
- `# Constraints`
- `# Examples`
- `# References`

Di chuyển các kiến thức lĩnh vực lớn, tài liệu tham chiếu API và ví dụ dài ra ngoài `SKILL.md` khi chúng không bắt buộc phải nạp trong mọi lần gọi.

## Phân tách trách nhiệm (Separation of concerns)

```text
Profile     = trách nhiệm / lăng kính đánh giá
Skill       = quy trình có thể tái sử dụng
Workflow    = sự kết hợp của nhiều trách nhiệm hoặc Skill
Knowledge   = lý thuyết / tham chiếu / chỉ dẫn lĩnh vực
Rules       = ràng buộc và phê duyệt của dự án
Registry    = chân lý kỹ thuật toàn dự án đã được chấp thuận
Runtime     = thực thi cấp quyền và kiểm soát tác dụng phụ thực tế
```

Một Skill có thể chỉ dẫn Codex tôn trọng quy tắc dự án; nó không được coi các chỉ dẫn prompt là biện pháp kiểm soát bảo mật runtime.

## Bài kiểm tra chất lượng Core Skill

Trước khi một Skill được đưa vào Core, tất cả câu trả lời phải là có:

1. Quy trình này có lặp lại trên các dự án game khác biệt thực tế không?
2. Đây có phải là một công việc tập trung thay vì toàn bộ một phòng ban hay vòng đời dự án không?
3. Quy trình này có khác biệt với mô tả trách nhiệm của Profile không?
4. Tính chuyên môn hóa theo engine/thể loại/network có thể được cung cấp bởi kiến thức dự án hoặc các Skill domain sau này không?
5. Đầu vào, điều kiện dừng và đầu ra có rõ ràng không?
6. Quy trình có đủ ngắn để nạp theo yêu cầu mà không trở thành nơi xả dữ liệu kiến thức khổng lồ không?
7. Cùng một file `SKILL.md` có thể vừa là Axit Skill chuẩn mực vừa là một Codex Skill hợp lệ không?

Nếu không, hãy giữ nó dưới dạng hành vi Profile thông thường, hướng dẫn dự án, Knowledge, hoặc một Skill đặc thù domain trong tương lai.
