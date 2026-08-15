# Đặc tả Axit Profile v1 (Axit Profile Spec v1)

## Mục đích (Purpose)

Một Profile là một **lăng kính quyết định và trách nhiệm** tinh gọn cho công việc trong Codex/Axit.

Một Profile trả lời các câu hỏi:

- Vai trò này chịu trách nhiệm cho những nghĩa vụ nào?
- Khi nào nên sử dụng lăng kính này?
- Những quyết định nào thuộc thẩm quyền của nó?
- Những quyết định nào nằm ngoài phạm vi của nó?
- Ngữ cảnh dự án nào bắt buộc phải đọc trước khi hành động?
- Bằng chứng nào nó kỳ vọng phải có trước khi chấp nhận kết quả?

Một Profile **không** định nghĩa quy trình từng bước. Quy trình từng bước thuộc về các Skill và Workflow.

## Vị trí chuẩn mực (Canonical location)

```text
.axit/core/profiles/<profile-id>/PROFILE.md
```

Các profile đặc thù của dự án có thể sử dụng cùng một hợp đồng này dưới thư mục profile do dự án sở hữu sau này.

## Frontmatter bắt buộc (Required frontmatter)

```yaml
---
spec_version: axit.profile/v1
id: technical-architect
summary: Owns technical structure, system boundaries, shared-state ownership, and cross-system contracts.
status: active
---
```

Các trường bắt buộc:

- `spec_version` — phải là `axit.profile/v1` cho phiên bản này.
- `id` — định danh ổn định viết thường nối dấu gạch ngang kebab-case.
- `summary` — một câu mô tả trách nhiệm.
- `status` — `draft`, `active`, hoặc `deprecated`.

Không thêm các trường chỉ vì một provider hỗ trợ chúng.

## Các phần bắt buộc (Required sections)

Mỗi Profile phải chứa đầy đủ các phần sau.

### `# Responsibility`

Xác định trách nhiệm bao quát duy nhất mà Profile sở hữu.

Một Profile phải có thể hiểu được mà không cần phải đọc một Profile khác.

### `# Use When`

Các điều kiện ngắn gọn khiến Profile này trở nên phù hợp.

Đây là các kích hoạt ngữ nghĩa, không phải cú pháp định tuyến của provider.

### `# Do Not Use For`

Nêu các trường hợp thuộc về lăng kính trách nhiệm khác hoặc không xứng đáng để nạp Profile này.

Phần này giúp ngăn chặn việc lạm dụng Profile.

### `# Decisions Owned`

Liệt kê các danh mục quyết định mà Profile này có thể phân tích và đưa ra khuyến nghị.

Người dùng luôn là người có thẩm quyền cuối cùng cho các quyết định sản phẩm/chiến lược trừ khi các quy tắc của dự án chỉ định khác một cách rõ ràng.

### `# Boundaries`

Liệt kê các hành động/quyết định mà Profile không được phép âm thầm thực hiện.

Khi một ranh giới bị vượt qua, hãy báo cáo xung đột hoặc yêu cầu trách nhiệm/quyết định phù hợp thay vì tự tạo ra thẩm quyền.

### `# Context Requirements`

Xác định các danh mục ngữ cảnh dự án tối thiểu cần được đọc khi có liên quan.

Sử dụng các danh mục ngữ nghĩa như:

- manifest của dự án;
- các mục registry kiến trúc đã được chấp thuận;
- thiết kế hệ thống liên quan;
- ranh giới module/source bị ảnh hưởng;
- các ràng buộc xác thực hiện tại.

Không hardcode bố cục của một repository cụ thể vào Core Profile khi một danh mục ngữ nghĩa là đã đủ.

### `# Verification Expectations`

Xác định bằng chứng nào cần tồn tại trước khi Profile coi trách nhiệm của mình đã được thỏa mãn.

Ví dụ bao gồm:

- tính nhất quán của registry kiến trúc;
- quyền sở hữu trạng thái rõ ràng;
- độ bao phủ hợp đồng giao diện (interface contract);
- các hệ quả về hiệu năng/khả năng kiểm thử được ghi nhận;
- các kiểm tra mang tính tất định (deterministic checks) bị ảnh hưởng đã được xác định.

Một Profile không tự mình thực thi mọi kiểm tra xác minh; nó định nghĩa chuẩn mực chất lượng cho trách nhiệm của nó.

### `# Escalation`

Xác định khi nào Profile nên dừng lại và báo cáo quyết định/xung đột lên người dùng hoặc lăng kính trách nhiệm khác.

Báo cáo mang tính ngữ nghĩa. Không mã hóa cứng một hệ thống phân cấp đa agent cố định.

## Các phần tùy chọn (Optional sections)

Chỉ sử dụng khi Profile thực tế đầu tiên cần đến:

- `# Default Heuristics` — các nguyên tắc quyết định có thể tái sử dụng tinh gọn.
- `# Typical Outputs` — các sản phẩm hoặc phát hiện phổ biến do lăng kính này tạo ra.

Tránh thêm các phần tùy chọn theo mặc định.

## Các mối bận tâm bị cấm (Forbidden concerns)

Core Profiles không được chứa cấu hình provider/runtime như:

```text
tools: Read, Write, Bash
model: opus / sonnet / gpt-*
maxTurns
memory
AskUserQuestion
Cú pháp Task/subagent
slash commands
cú pháp phân quyền provider
```

Chúng cũng phải tránh sở hữu những mối bận tâm thuộc về nơi khác:

```text
quy trình từng bước              -> Skill
kết hợp đa bước                 -> Workflow
lý thuyết/tham chiếu lĩnh vực   -> Knowledge
ràng buộc đặc thù dự án         -> Project Rules / Registry
cấp quyền runtime               -> Runtime/Harness policy
metadata khám phá của Codex     -> Tầng tương thích Codex
```

## Bài kiểm tra chất lượng Profile

Trước khi một Profile được chấp nhận vào Core, tất cả câu trả lời phải là có:

1. Trách nhiệm này có hữu ích trên các dự án game khác biệt thực tế không?
2. Nó có phân biệt rõ ràng với các Core Profile hiện có không?
3. Tính chuyên môn hóa có thể được cung cấp qua Skills/Knowledge thay vì thêm một Profile khác không?
4. Profile có thể duy trì tính trung lập với engine, thể loại, network, và provider không?
5. Nó có ranh giới và hành vi báo cáo rõ ràng không?
6. Nó có định nghĩa chuẩn chất lượng xác minh có ý nghĩa không?

Nếu không, hãy giữ hành vi đó thuộc về đặc thù dự án/lĩnh vực hoặc thể hiện nó dưới dạng sản phẩm Skill/Knowledge.
