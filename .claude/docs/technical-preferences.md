# Tùy chọn kỹ thuật (Technical Preferences)

<!-- Được điền bởi /setup-engine. Được cập nhật khi người dùng đưa ra các quyết định trong suốt quá trình phát triển. -->
<!-- Tất cả các agent đều tham chiếu file này để nắm bắt tiêu chuẩn và quy ước đặc thù của dự án. -->

## Engine & Ngôn ngữ lập trình

- **Engine**: [CHỜ CẤU HÌNH — chạy /setup-engine]
- **Ngôn ngữ**: [CHỜ CẤU HÌNH]
- **Rendering**: [CHỜ CẤU HÌNH]
- **Physics**: [CHỜ CẤU HÌNH]

## Đầu vào điều khiển & Nền tảng (Input & Platform)

<!-- Được ghi bởi /setup-engine. Được đọc bởi /ux-design, /ux-review, /test-setup, /team-ui, và /dev-story -->
<!-- nhằm giới hạn phạm vi đặc tả tương tác, test helpers, và triển khai code đúng phương thức điều khiển. -->

- **Nền tảng mục tiêu (Target Platforms)**: [CHỜ CẤU HÌNH — ví dụ: PC, Console, Mobile, Web]
- **Phương thức điều khiển (Input Methods)**: [CHỜ CẤU HÌNH — ví dụ: Bàn phím/Chuột, Tay cầm Gamepad, Cảm ứng Touch, Hỗn hợp]
- **Điều khiển chính (Primary Input)**: [CHỜ CẤU HÌNH — phương thức điều khiển chủ đạo cho game này]
- **Hỗ trợ Gamepad**: [CHỜ CẤU HÌNH — Đầy đủ (Full) / Một phần (Partial) / Không (None)]
- **Hỗ trợ Cảm ứng**: [CHỜ CẤU HÌNH — Đầy đủ (Full) / Một phần (Partial) / Không (None)]
- **Ghi chú nền tảng**: [CHỜ CẤU HÌNH — bất kỳ ràng buộc UX đặc thù nào của nền tảng]

## Quy ước đặt tên (Naming Conventions)

- **Classes**: [CHỜ CẤU HÌNH]
- **Variables**: [CHỜ CẤU HÌNH]
- **Signals/Events**: [CHỜ CẤU HÌNH]
- **Files**: [CHỜ CẤU HÌNH]
- **Scenes/Prefabs**: [CHỜ CẤU HÌNH]
- **Constants**: [CHỜ CẤU HÌNH]

## Ngân sách hiệu năng (Performance Budgets)

- **Framerate mục tiêu**: [CHỜ CẤU HÌNH]
- **Ngân sách Frame**: [CHỜ CẤU HÌNH]
- **Draw Calls**: [CHỜ CẤU HÌNH]
- **Trần bộ nhớ (Memory Ceiling)**: [CHỜ CẤU HÌNH]

## Kiểm thử (Testing)

- **Framework**: [CHỜ CẤU HÌNH]
- **Độ bao phủ tối thiểu**: [CHỜ CẤU HÌNH]
- **Các test bắt buộc**: Công thức cân bằng, hệ thống gameplay, mạng (nếu có)

## Các mẫu thiết kế bị cấm (Forbidden Patterns)

<!-- Thêm các mẫu thiết kế tuyệt đối không được xuất hiện trong codebase của dự án -->
- [Chưa cấu hình — bổ sung khi các quyết định kiến trúc được đưa ra]

## Thư viện / Addon được phép sử dụng

<!-- Thêm các dependency bên thứ ba đã được phê duyệt tại đây -->
- [Chưa cấu hình — bổ sung khi các dependency được phê duyệt]

## Nhật ký quyết định kiến trúc (Architecture Decisions Log)

<!-- Tham chiếu nhanh liên kết tới các ADR đầy đủ trong docs/architecture/ -->
- [Chưa có ADR nào — sử dụng /architecture-decision để tạo]

## Chuyên viên phụ trách Engine (Engine Specialists)

<!-- Được ghi bởi /setup-engine khi engine được cấu hình. -->
<!-- Được đọc bởi /code-review, /architecture-decision, /architecture-review, và các team skill -->
<!-- để biết chuyên viên nào cần được gọi cho việc xác thực đặc thù theo engine. -->

- **Chuyên viên chính (Primary)**: [CHỜ CẤU HÌNH — chạy /setup-engine]
- **Chuyên viên Ngôn ngữ/Code**: [CHỜ CẤU HÌNH]
- **Chuyên viên Shader**: [CHỜ CẤU HÌNH]
- **Chuyên viên UI**: [CHỜ CẤU HÌNH]
- **Các chuyên viên bổ sung**: [CHỜ CẤU HÌNH]
- **Ghi chú điều phối**: [CHỜ CẤU HÌNH]

### Điều phối theo phần mở rộng File (File Extension Routing)

<!-- Các skill sử dụng bảng này để chọn đúng chuyên viên theo từng loại file. -->
<!-- Nếu một hàng ghi [CHỜ CẤU HÌNH], quay về dùng Chuyên viên chính (Primary) cho loại file đó. -->

| Phần mở rộng / Loại File | Chuyên viên được gọi |
|---|---|
| Mã nguồn game (ngôn ngữ chính) | [CHỜ CẤU HÌNH] |
| File Shader / Material | [CHỜ CẤU HÌNH] |
| File UI / Màn hình | [CHỜ CẤU HÌNH] |
| File Scene / Prefab / Màn chơi | [CHỜ CẤU HÌNH] |
| File GDExtension / Plugin native | [CHỜ CẤU HÌNH] |
| Đánh giá kiến trúc chung | Chuyên viên chính (Primary) |
