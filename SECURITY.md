# Chính sách bảo mật (Security Policy)

## Các phiên bản được hỗ trợ

Chỉ có nhánh `main` nhận các bản vá bảo mật. Các bản fork và các bản phát hành cũ hơn không được hỗ trợ.

## Báo cáo lỗ hổng bảo mật

**Tuyệt đối không báo cáo các lỗ hổng bảo mật qua GitHub issues công khai.**

Thay vào đó, hãy sử dụng tính năng báo cáo lỗ hổng bảo mật riêng tư của GitHub:

**[Báo cáo lỗ hổng bảo mật →](https://github.com/Donchitos/Claude-Code-Game-Studios/security/advisories/new)**

Vui lòng cung cấp càng nhiều thông tin chi tiết càng tốt:
- Mô tả lỗ hổng và phạm vi ảnh hưởng
- Các bước tái hiện
- Tác động tiềm ẩn và các kịch bản tấn công
- Bất kỳ giải pháp khắc phục/giảm thiểu (mitigations) nào được đề xuất

**Quy trình phản hồi dự kiến:**
- Xác nhận đã nhận thông tin trong vòng **48 giờ**
- Cập nhật tình trạng xử lý trong vòng **7 ngày**
- Xử lý dứt điểm trong vòng **90 ngày** đối với các lỗ hổng đã được xác nhận

## Phạm vi áp dụng (What Is In Scope)

CCGS là một **công cụ phát triển cục bộ (local development tool)** — hệ thống cài đặt các shell hook và điều phối các AI agent chạy trực tiếp trên máy của bạn. Các vấn đề bảo mật chủ yếu xoay quanh code đóng góp thực thi trong môi trường của người dùng mà người dùng không hay biết.

### Mức độ nghiêm trọng cao (High Severity)
- Các hook (`.claude/hooks/*.sh`) thực thi các lệnh shell độc hại hoặc không được công khai trên máy người dùng
- Các skill hoặc agent đánh cắp biến môi trường, API keys, hoặc các bí mật (secrets)
- Tấn công Prompt injection thông qua các định nghĩa skill hoặc agent khiến Claude bỏ qua các biện pháp an toàn hoặc thực hiện các hành động phá hoại trái phép
- Các đóng góp âm thầm thay đổi hành vi theo cách người dùng không thể kiểm tra (audit)

### Mức độ nghiêm trọng trung bình (Medium Severity)
- Các skill thực hiện các yêu cầu mạng ra ngoài (outbound network requests) không được công khai
- Các định nghĩa agent leo thang quyền hạn hoặc bỏ qua các lời nhắc xác nhận của người dùng
- Các mẫu hook hoạt động khác nhau giữa các nền tảng nhằm che giấu hành vi
- Các skill ghi dữ liệu ra ngoài phạm vi đã ghi nhận trong tài liệu mà không có bước phê duyệt rõ ràng của người dùng

### Ngoài phạm vi (Out of Scope)
- Hành vi của Claude hoặc bản thân CLI Claude Code (hãy báo cáo tới [Anthropic](https://www.anthropic.com/security))
- Các lỗi trong bản cài đặt Claude Code của người dùng hoặc extension của trình soạn thảo
- Các lỗ hổng trên lý thuyết không có đường dẫn tấn công thực tế
- Các vấn đề đòi hỏi quyền truy cập vật lý vào máy của người dùng

## Hướng dẫn bảo mật cho người đóng góp

Khi đóng góp hooks, skills, hoặc agents:

- **Hook phải tương thích POSIX** — sử dụng `grep -E`, không dùng `grep -P`; tránh cú pháp đặc thù của từng nền tảng có hành vi khác nhau giữa các hệ điều hành
- **Không thực hiện gọi mạng ngầm (silent network calls)** từ hooks hoặc skills trừ khi đã được ghi nhận rõ ràng trong tài liệu và người dùng chủ động bật (opt-in)
- **Không đọc secrets hoặc biến môi trường** vượt quá mức tối thiểu cần thiết và phải được ghi nhận rõ ràng ở phần header của skill
- **Skill không được ghi dữ liệu ra ngoài phạm vi đã ghi nhận trong tài liệu** mà không có bước xác nhận rõ ràng từ người dùng

## Chính sách công bố (Disclosure Policy)

Chúng tôi tuân thủ quy trình **công bố có điều phối trong 90 ngày (90-day coordinated disclosure)**:

1. Bạn gửi thông tin lỗ hổng một cách riêng tư
2. Chúng tôi xác nhận trong vòng 48 giờ
3. Chúng tôi xác minh và đánh giá mức độ nghiêm trọng trong vòng 7 ngày
4. Chúng tôi phát triển và kiểm thử bản vá
5. Chúng tôi thông báo cho bạn trước khi có bất kỳ công bố rộng rãi nào
6. Việc công bố công khai diễn ra sau khi bản vá được phát hành, hoặc sau 90 ngày — tùy điều kiện nào đến trước

Chúng tôi sẽ ghi nhận công lao của người báo cáo trong release notes trừ khi bạn muốn ẩn danh.
