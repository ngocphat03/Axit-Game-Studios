# Quy tắc Lập trình Core Engine (Engine Code Rules)

Áp dụng cho tất cả mã nguồn cốt lõi trong `src/core/**` và các hệ thống nền tảng Unity.

- **Zero Allocation trong Hot Paths**: Tuyệt đối không phân bổ bộ nhớ mới (tránh `new`, LINQ, boxing/unboxing, chuỗi string concat) trong `Update()`, `FixedUpdate()`, hoặc các hàm gọi theo từng frame.
- **An toàn luồng (Thread-Safety)**: Các cấu trúc dữ liệu dùng chung trên nhiều worker threads hoặc C# Jobs phải đảm bảo an toàn luồng và tránh race condition.
- **Quản lý Vòng đời & Bộ nhớ**: Chủ động giải phóng tài nguyên không quản lý (unmanaged resources), hủy đăng ký sự kiện (event unsubscription) trong `OnDestroy()` hoặc `Dispose()`.
- **Tính ổn định của API**: Các core service phải có hợp đồng interface ổn định và tương thích ngược khi mở rộng.
- **Tối ưu hóa GC**: Tái sử dụng mảng (Array pooling), object pooling và `NativeArray<T>` khi cần xử lý khối lượng lớn dữ liệu.
