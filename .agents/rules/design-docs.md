# Tiêu chuẩn Tài liệu Thiết kế Game (Design Document Rules)

Áp dụng cho tất cả tài liệu thiết kế trong `design/gdd/**`.

- **Cấu trúc 8 Phần Bắt buộc**: Mọi GDD cho một hệ thống game phải có đủ 8 phần:
  1. **Overview (Tổng quan)**: Tóm tắt mục đích hệ thống trong 1 đoạn văn.
  2. **Player Fantasy (Hình dung người chơi)**: Cảm xúc và trải nghiệm người chơi hướng tới.
  3. **Detailed Rules (Quy tắc chi tiết)**: Các quy tắc cơ chế không mơ hồ.
  4. **Formulas (Công thức toán học)**: Toàn bộ công thức tính toán kèm định nghĩa biến.
  5. **Edge Cases (Trường hợp biên)**: Xử lý các tình huống bất thường hoặc ranh giới.
  6. **Dependencies (Các phụ thuộc)**: Danh sách các hệ thống liên quan và dữ liệu trao đổi.
  7. **Tuning Knobs (Các nút tinh chỉnh)**: Xác định rõ các biến có thể cân bằng.
  8. **Acceptance Criteria (Tiêu chí chấp nhận)**: Các điều kiện có thể kiểm thử để nghiệm thu.
- **Tính nhất quán**: Các chỉ số và thuật ngữ phải khớp với Entity Registry và Concept Game gốc.
