# Quy tắc Lập trình UI (UI Code Rules)

Áp dụng cho tất cả mã nguồn giao diện trong `src/ui/**` (UI Toolkit, UXML/USS, hoặc UGUI).

- **Không Nắm giữ Trạng thái Game**: Code UI chỉ chịu trách nhiệm hiển thị (View) và nhận input người dùng; không tự ý thay đổi logic hoặc nắm giữ dữ liệu game gốc.
- **Sẵn sàng Đa ngôn ngữ (Localization-Ready)**: Không hardcode chuỗi text hiển thị trên UI; tất cả text phải thông qua hệ thống Localization Key.
- **Khả năng tiếp cận (Accessibility - a11y)**: Hỗ trợ điều hướng bằng bàn phím / tay cầm gamepad, hỗ trợ tương phản màu sắc và cỡ chữ linh hoạt.
- **Tối ưu hóa Draw Calls & Canvas Rebuild**: Tách các thành phần UI tĩnh và động ra các Canvas / UI Document riêng biệt để hạn chế rebuild không cần thiết.
