# Tiêu chuẩn Viết Kiểm thử (Test Standards)

Áp dụng cho tất cả các bài kiểm thử trong `tests/**` (NUnit, Unity Test Framework).

- **Quy ước Đặt tên**:
  - File test: `[System]_[Feature]_Tests.cs` (ví dụ: `Combat_DamageCalculation_Tests.cs`).
  - Hàm test: `[MethodName]_[Scenario]_[ExpectedResult]` (ví dụ: `CalculateDamage_WhenCriticalHit_ReturnsDoubleDamage`).
- **Tính tất định (Determinism)**: Mỗi lần chạy test phải luôn cho ra cùng một kết quả — không phụ thuộc vào `Random` mà không có seed cố định, không phụ thuộc vào thời gian thực.
- **Tính cô lập (Isolation)**: Mỗi bài test phải tự khởi tạo (Set Up) và dọn dẹp (Tear Down) trạng thái của chính nó; thứ tự thực thi test không được ảnh hưởng đến kết quả.
- **Không I/O & Network trong Unit Test**: Unit test tuyệt đối không gọi Web API, Database hoặc File I/O — sử dụng Mock/Stub hoặc Dependency Injection.
- **Phân loại Bằng chứng**:
  - **Logic (Formula, Math, State Machines)**: Unit test tự động (BẮT BUỘC - BLOCKING).
  - **Integration (Đa hệ thống)**: Integration test hoặc kịch bản Playtest (BLOCKING).
  - **Visual/UI**: Bằng chứng tương tác hoặc ảnh chụp màn hình (ADVISORY).
