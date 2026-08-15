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
| **Haiku** | `claude-haiku-4-5-20251001` | Kiểm tra trạng thái chỉ đọc, định dạng dữ liệu, tra cứu đơn giản — không cần phán đoán sáng tạo |
| **Sonnet** | `claude-sonnet-4-6` | Triển khai code, soạn thảo thiết kế, phân tích các hệ thống riêng lẻ — mặc định cho hầu hết công việc |
| **Opus** | `claude-opus-4-6` | Tổng hợp đa tài liệu, kết luận cổng giai đoạn quan trọng, đánh giá toàn diện liên hệ thống |

Các skill sử dụng `model: haiku`: `/help`, `/sprint-status`, `/story-readiness`, `/scope-check`, `/project-stage-detect`, `/changelog`, `/patch-notes`, `/onboard`

Các skill sử dụng `model: opus`: `/review-all-gdds`, `/architecture-review`, `/gate-check`

Tất cả các skill khác mặc định sử dụng Sonnet. Khi tạo skill mới, gán Haiku nếu skill chỉ đọc và định dạng; gán Opus nếu nó phải tổng hợp từ 5 tài liệu trở lên với kết quả có tính rủi ro cao; nếu không hãy để trống (Sonnet).

## Subagents so với Agent Teams

Dự án này sử dụng hai mô hình đa agent khác biệt:

### Subagents (hiện tại, luôn hoạt động)
Được khởi tạo thông qua `Task` bên trong một phiên làm việc Claude Code đơn lẻ. Được sử dụng bởi tất cả các skill `team-*` và các skill điều phối. Các subagent dùng chung ngữ cảnh cấp quyền của phiên làm việc, chạy tuần tự hoặc song song trong phiên, và trả kết quả về cho agent cha.

**Khi nào nên khởi tạo song song**: Nếu đầu vào của hai subagent độc lập với nhau (không bên nào cần đầu ra của bên kia để bắt đầu), hãy gọi đồng thời cả hai lệnh Task thay vì chờ đợi. Ví dụ: `/review-all-gdds` Giai đoạn 1 (tính nhất quán) và Giai đoạn 2 (lý thuyết thiết kế) là độc lập — hãy gọi cả hai cùng lúc.

### Agent Teams (thử nghiệm — tùy chọn kích hoạt)
Nhiều *phiên làm việc* Claude Code độc lập chạy đồng thời, được điều phối qua một danh sách tác vụ dùng chung. Mỗi phiên có cửa sổ ngữ cảnh và ngân sách token riêng. Yêu cầu biến môi trường `CLAUDE_CODE_EXPERIMENTAL_AGENT_TEAMS=1`.

**Sử dụng agent teams khi**:
- Công việc trải rộng qua nhiều hệ thống con và không chạm vào cùng các file giống nhau
- Mỗi luồng công việc kéo dài >30 phút và được hưởng lợi từ việc chạy song song thực sự
- Một agent cấp cao (technical-director, producer) cần điều phối 3+ phiên làm việc của chuyên viên trên các epic khác nhau cùng lúc

**Không sử dụng agent teams khi**:
- Đầu ra của phiên làm việc này là đầu vào bắt buộc của phiên khác (sử dụng subagents tuần tự)
- Tác vụ vừa vặn trong ngữ cảnh của một phiên làm việc đơn lẻ (sử dụng subagents thay thế)
- Chi phí là vấn đề cần cân nhắc — mỗi thành viên trong team tiêu tốn token độc lập

## Giao thức tác vụ song song (Parallel Task Protocol)

Khi một skill điều phối khởi tạo nhiều agent độc lập:

1. Phát ra tất cả các lệnh gọi Task độc lập trước khi chờ đợi bất kỳ kết quả nào
2. Thu thập đầy đủ tất cả kết quả trước khi chuyển sang các giai đoạn phụ thuộc
3. Nếu bất kỳ agent nào bị BLOCKED, hãy báo cáo ngay lập tức — không âm thầm bỏ qua
4. Luôn tạo báo cáo một phần nếu một số agent hoàn thành và các agent khác bị chặn
