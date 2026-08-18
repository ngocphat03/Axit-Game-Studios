# Quy tắc điều phối Agent (Agent Coordination Rules)

1. **Ủy quyền theo chiều dọc (Vertical Delegation)**: Các agent lãnh đạo ủy quyền cho các trưởng bộ phận, trưởng bộ phận ủy quyền cho các chuyên viên. Không bao giờ nhảy cóc phân tầng đối với các quyết định phức tạp.
2. **Tham vấn theo chiều ngang (Horizontal Consultation)**: Các agent ở cùng phân tầng có thể tham vấn lẫn nhau nhưng không được đưa ra quyết định ràng buộc ngoài lĩnh vực phụ trách của mình.
3. **Giải quyết xung đột (Conflict Resolution)**: Khi hai agent bất đồng quan điểm, hãy báo cáo lên cấp quản lý chung. Nếu không có quản lý chung, báo cáo lên `creative-director` cho các xung đột thiết kế hoặc `technical-director` cho các xung đột kỹ thuật.
4. **Lan truyền thay đổi (Change Propagation)**: Khi một thay đổi thiết kế ảnh hưởng tới nhiều lĩnh vực, agent `producer` sẽ điều phối việc cập nhật lan truyền.
5. **Không tự ý sửa đổi chéo lĩnh vực (No Unilateral Cross-Domain Changes)**: Một agent tuyệt đối không bao giờ được chỉnh sửa các file nằm ngoài thư mục được chỉ định của mình nếu không có sự ủy quyền rõ ràng.

## Phân bổ phân tầng Model (Model Tier Assignment)

Các skill và agent được phân bổ vào các tầng model dựa trên độ phức tạp của tác vụ:

| Phân tầng | Model | Khi nào nên sử dụng |
|---|---|---|
| **Flash** | `gemini-2.5-flash` | Kiểm tra trạng thái chỉ đọc, định dạng dữ liệu, tra cứu đơn giản, child sub-agents, verifier độc lập |
| **Pro** | `gemini-2.5-pro` | Triển khai code, soạn thảo thiết kế, phân tích các hệ thống riêng lẻ — mặc định cho hầu hết công việc |
| **Pro High** | `gemini-2.5-pro` (Mức suy luận cao) | Vai trò Orchestrator chính, tổng hợp đa tài liệu, kết luận cổng giai đoạn quan trọng, đánh giá toàn diện liên hệ thống |

Các skill sử dụng `model: flash`: `/help`, `/sprint-status`, `/story-readiness`, `/scope-check`, `/project-stage-detect`, `/changelog`, `/patch-notes`, `/onboard`

Các skill sử dụng `model: pro-high`: `/review-all-gdds`, `/architecture-review`, `/gate-check`

Tất cả các skill khác mặc định sử dụng Pro. Khi tạo skill mới, gán Flash nếu skill chỉ đọc và định dạng; gán Pro High nếu nó phải tổng hợp từ 5 tài liệu trở lên với kết quả có tính rủi ro cao; nếu không hãy để mặc định (Pro).

## Sub-agents trong Antigravity (AGY)

Dự án sử dụng mô hình điều phối Orchestrator & Sub-agents của Google Antigravity:

### Sub-agents (Ủy quyền tác vụ)
Được khởi tạo thông qua công cụ sub-agent / `Task` bên trong phiên làm việc của Antigravity. Được sử dụng bởi tất cả các skill `team-*` và các skill điều phối. Các sub-agent chạy độc lập hoặc song song trong phiên, và trả kết quả đã xác thực về cho Orchestrator.

**Khi nào nên khởi tạo song song**: Nếu đầu vào của hai sub-agent độc lập với nhau (không bên nào cần đầu ra của bên kia để bắt đầu), hãy khởi tạo đồng thời cả hai thay vì chờ đợi. Ví dụ: `/review-all-gdds` Giai đoạn 1 (tính nhất quán) và Giai đoạn 2 (lý thuyết thiết kế) là độc lập — hãy kích hoạt cả hai cùng lúc.

## Giao thức tác vụ song song (Parallel Task Protocol)

Khi một skill điều phối khởi tạo nhiều agent độc lập:

1. Phát ra tất cả các lệnh gọi Task độc lập trước khi chờ đợi bất kỳ kết quả nào
2. Thu thập đầy đủ tất cả kết quả trước khi chuyển sang các giai đoạn phụ thuộc
3. Nếu bất kỳ agent nào bị BLOCKED, hãy báo cáo ngay lập tức — không âm thầm bỏ qua
4. Luôn tạo báo cáo một phần nếu một số agent hoàn thành và các agent khác bị chặn
