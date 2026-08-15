# Tiêu chuẩn lập trình (Coding Standards)

- Mọi code game phải bao gồm doc comments trên các public API
- Mỗi hệ thống phải có một bản ghi quyết định kiến trúc (ADR) tương ứng trong `docs/architecture/`
- Các giá trị gameplay phải được hướng dữ liệu (cấu hình bên ngoài), tuyệt đối không hardcode
- Tất cả các public method phải có khả năng viết unit test (ưu tiên dependency injection hơn singletons)
- Các commit phải tham chiếu tới tài liệu thiết kế hoặc ID tác vụ liên quan
- **Thông điệp Commit**: Sử dụng định dạng Conventional Commits — `feat:`, `fix:`, `chore:`, `docs:`, `test:`, `refactor:`. Tham chiếu ID story hoặc task trong phần thân (ví dụ: `Story: EPIC-001-S02`).
- **Phát triển hướng xác minh (Verification-Driven Development)**: Viết test trước khi thêm các hệ thống gameplay.
  Đối với các thay đổi UI, xác minh bằng ảnh chụp màn hình. So sánh kết quả kỳ vọng với kết quả thực tế trước khi đánh dấu công việc hoàn thành. Mọi triển khai code đều phải có cách để chứng minh nó hoạt động.

# Tiêu chuẩn tài liệu thiết kế (Design Document Standards)

- Tất cả các tài liệu thiết kế đều sử dụng định dạng Markdown
- Mỗi cơ chế gameplay có một tài liệu riêng trong `design/gdd/`
- Tài liệu phải bao gồm đủ 8 phần bắt buộc:
  1. **Overview (Tổng quan)** -- tóm tắt trong một đoạn văn
  2. **Player Fantasy (Hình dung của người chơi)** -- cảm xúc và trải nghiệm dự kiến
  3. **Detailed Rules (Quy tắc chi tiết)** -- các cơ chế rõ ràng, không mơ hồ
  4. **Formulas (Công thức toán học)** -- tất cả các phép toán được định nghĩa kèm biến số
  5. **Edge Cases (Trường hợp biên)** -- xử lý các tình huống bất thường
  6. **Dependencies (Các phụ thuộc)** -- danh sách các hệ thống liên quan
  7. **Tuning Knobs (Các nút tinh chỉnh)** -- xác định các giá trị có thể cấu hình
  8. **Acceptance Criteria (Tiêu chí chấp nhận)** -- các điều kiện thành công có thể kiểm thử
- Các giá trị cân bằng phải liên kết tới công thức nguồn hoặc lý do thiết kế

# Tiêu chuẩn kiểm thử (Testing Standards)

## Bằng chứng kiểm thử theo loại Story

Tất cả các story phải có bằng chứng kiểm thử phù hợp trước khi có thể đánh dấu Hoàn thành (Done):

| Loại Story | Bằng chứng bắt buộc | Vị trí lưu trữ | Mức độ Cổng |
|---|---|---|---|
| **Logic** (công thức, AI, máy trạng thái) | Unit test tự động — bắt buộc phải pass | `tests/unit/[system]/` | BLOCKING |
| **Integration** (liên kết đa hệ thống) | Integration test HOẶC biên bản playtest | `tests/integration/[system]/` | BLOCKING |
| **Visual/Feel** (animation, VFX, cảm giác chơi) | Ảnh chụp màn hình + lead ký duyệt | `production/qa/evidence/` | ADVISORY |
| **UI** (menus, HUD, các màn hình) | Tài liệu walkthrough thủ công HOẶC test tương tác | `production/qa/evidence/` | ADVISORY |
| **Config/Data** (tinh chỉnh cân bằng) | Vượt qua smoke check | `production/qa/smoke-[date].md` | ADVISORY |

## Quy tắc kiểm thử tự động (Automated Test Rules)

- **Đặt tên**: `[system]_[feature]_test.[ext]` cho các file; `test_[scenario]_[expected]` cho các hàm test
- **Tính tất định (Determinism)**: Các bài test phải cho ra cùng một kết quả ở mỗi lần chạy — không dùng random seeds ngẫu nhiên, không dùng assertion phụ thuộc thời gian
- **Tính cô lập (Isolation)**: Mỗi bài test tự thiết lập và dọn dẹp trạng thái của chính nó; các test không được phụ thuộc vào thứ tự thực thi
- **Không dùng dữ liệu hardcode**: Dữ liệu mẫu (test fixtures) sử dụng file hằng số hoặc hàm factory, không dùng magic numbers trực tiếp
  (ngoại lệ: các bài test giá trị biên nơi con số chính xác LÀ trọng tâm cần test)
- **Tính độc lập (Independence)**: Unit test không gọi API bên ngoài, cơ sở dữ liệu, hoặc I/O file — sử dụng dependency injection

## Những gì KHÔNG NÊN tự động hóa

- Độ trung thực hình ảnh (đầu ra shader, giao diện VFX, đường cong animation)
- Các yếu tố "Cảm giác chơi" (độ nhạy điều khiển, cảm giác trọng lượng, thời điểm nhấn phím)
- Render đặc thù theo nền tảng (kiểm thử trên phần cứng thực tế, không chạy headless)
- Toàn bộ phiên chơi game (được bao phủ bởi playtest thực tế của người chơi, không phải tự động hóa)

## Quy tắc CI/CD

- Bộ test tự động chạy trên mỗi lần push vào nhánh main và trên mỗi PR
- Không cho phép merge nếu test thất bại — test là cổng chặn (blocking gate) trong CI
- Tuyệt đối không tắt hoặc bỏ qua các test bị lỗi để CI vượt qua — hãy sửa lỗi gốc bên dưới
- Các lệnh CI đặc thù theo Engine:
  - **Godot**: `godot --headless --script tests/gdunit4_runner.gd`
  - **Unity**: `game-ci/unity-test-runner@v4` (GitHub Actions)
  - **Unreal**: headless runner với cờ `-nullrhi`
