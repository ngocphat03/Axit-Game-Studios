# Quy tắc theo đường dẫn (Path-Specific Rules)

Các quy tắc trong `.agents/rules/` được tự động thực thi khi chỉnh sửa các file thuộc đường dẫn khớp:

| File Quy tắc | Mẫu đường dẫn | Nội dung thực thi |
|---|---|---|
| `gameplay-code.md` | `src/gameplay/**` | Giá trị hướng dữ liệu, sử dụng delta time, không tham chiếu trực tiếp UI |
| `engine-code.md` | `src/core/**` | Không phân bổ bộ nhớ (zero allocs) trong hot paths, an toàn luồng, API ổn định |
| `ai-code.md` | `src/ai/**` | Ngân sách hiệu năng, khả năng debug, tham số hướng dữ liệu |
| `network-code.md` | `src/networking/**` | Server có quyền quyết định (authoritative), định phiên bản thông điệp, bảo mật |
| `ui-code.md` | `src/ui/**` | Không nắm giữ trạng thái game, sẵn sàng đa ngôn ngữ, hỗ trợ tiếp cận (accessibility) |
| `design-docs.md` | `design/gdd/**` | Đủ 8 phần bắt buộc, định dạng công thức toán học, xử lý trường hợp biên |
| `narrative.md` | `design/narrative/**` | Nhất quán cốt truyện, giọng văn nhân vật, các cấp độ canon |
| `data-files.md` | `assets/data/**` | Tính hợp lệ của JSON, quy ước đặt tên, quy tắc schema |
| `test-standards.md` | `tests/**` | Đặt tên bài test, yêu cầu độ bao phủ, mẫu dữ liệu fixture |
| `prototype-code.md` | `prototypes/**` | Giảm bớt tiêu chuẩn khắt khe, bắt buộc có README, ghi lại giả thuyết thử nghiệm |
| `shader-code.md` | `assets/shaders/**` | Quy ước đặt tên, mục tiêu hiệu năng, quy tắc đa nền tảng |
