# Quản lý Ngữ cảnh (Context Management)

Ngữ cảnh là tài nguyên quan trọng nhất trong một phiên làm việc Claude Code. Hãy chủ động quản lý nó.

## Trạng thái lưu bằng File (Chiến lược chính)

**File là bộ nhớ, không phải là cuộc hội thoại.** Các cuộc hội thoại mang tính tạm thời và sẽ bị nén (compacted) hoặc mất đi. Các file trên đĩa tồn tại bền vững qua các lần nén ngữ cảnh và crash phiên làm việc.

### File trạng thái phiên làm việc (Session State File)

Duy trì `production/session-state/active.md` như một checkpoint sống. Cập nhật nó sau mỗi milestone quan trọng:

- Một phần thiết kế được duyệt và ghi vào file
- Một quyết định kiến trúc được đưa ra
- Đạt được một mốc triển khai code
- Thu thập được kết quả kiểm thử

File trạng thái nên chứa: tác vụ hiện tại, checklist tiến độ, các quyết định chính đã đưa ra, các file đang được xử lý, và các câu hỏi mở.

### Khối dòng trạng thái (Status Line Block - Chỉ từ Production trở lên)

Khi dự án ở giai đoạn Production, Polish, hoặc Release, hãy thêm một khối trạng thái có cấu trúc vào `active.md` để script dòng trạng thái có thể phân tích cú pháp:

```markdown
<!-- STATUS -->
Epic: Combat System
Feature: Melee Combat
Task: Implement hitbox detection
<!-- /STATUS -->
```

- Cả 3 trường (Epic, Feature, Task) đều là tùy chọn — chỉ đưa vào những gì phù hợp
- Cập nhật khối này khi chuyển đổi khu vực trọng tâm
- Dòng trạng thái hiển thị nó dưới dạng breadcrumb: `Combat System > Melee Combat > Hitboxes`
- Xóa hoặc để trống khối này khi không có công việc nào đang tập trung

Sau bất kỳ sự gián đoạn nào (nén context, crash, `/clear`), hãy đọc file trạng thái trước tiên.

### Ghi file tăng dần (Incremental File Writing)

Khi tạo các tài liệu nhiều phần (design docs, architecture docs, cốt truyện lore):

1. Tạo file ngay lập tức với một khung sườn (tất cả các tiêu đề mục, phần thân để trống)
2. Thảo luận và soạn thảo từng phần một trong cuộc trò chuyện
3. Ghi từng phần vào file ngay sau khi được duyệt
4. Cập nhật file trạng thái phiên làm việc sau mỗi phần
5. Sau khi ghi một phần, cuộc thảo luận trước đó về phần đó có thể được nén an toàn — các quyết định đã nằm trong file

Cách này giúp cửa sổ ngữ cảnh chỉ giữ cuộc thảo luận của phần *hiện tại* (~3-5k tokens) thay vì toàn bộ lịch sử trò chuyện của cả tài liệu (~30-50k tokens).

## Chủ động Nén ngữ cảnh (Proactive Compaction)

- **Chủ động nén** ở mức ~60-70% dung lượng ngữ cảnh, không đợi đến khi chạm giới hạn mới xử lý
- **Sử dụng `/clear`** giữa các tác vụ không liên quan, hoặc sau 2+ lần thử sửa lỗi thất bại
- **Các điểm nén tự nhiên:** sau khi ghi một phần vào file, sau khi commit, sau khi hoàn thành một tác vụ, trước khi bắt đầu một chủ đề mới
- **Nén có trọng tâm:** `/compact Tập trung vào [tác vụ hiện tại] — các phần 1-3 đã được ghi vào file, đang làm phần 4`

## Ngân sách ngữ cảnh theo loại tác vụ

- Nhẹ (đọc/review): ~3k tokens khởi động
- Trung bình (triển khai tính năng): ~8k tokens
- Nặng (refactor đa hệ thống): ~15k tokens

## Ủy quyền cho Subagent

Sử dụng subagent cho việc nghiên cứu và khám phá để giữ phiên làm việc chính luôn sạch sẽ.
Subagent chạy trong cửa sổ ngữ cảnh riêng và chỉ trả về các bản tóm tắt:

- **Sử dụng subagent** khi điều tra trên nhiều file, khám phá code lạ, hoặc nghiên cứu tiêu tốn >5k tokens đọc file
- **Đọc trực tiếp** khi bạn biết chính xác 1-2 file cụ thể cần kiểm tra
- Subagent không thừa hưởng lịch sử trò chuyện — hãy cung cấp đầy đủ ngữ cảnh trong prompt

## Hướng dẫn Nén ngữ cảnh (Compaction Instructions)

Khi ngữ cảnh được nén, hãy giữ lại những thông tin sau trong bản tóm tắt:

- Tham chiếu tới `production/session-state/active.md` (đọc nó để phục hồi trạng thái)
- Danh sách các file đã chỉnh sửa trong phiên này và mục đích của chúng
- Bất kỳ quyết định kiến trúc nào đã đưa ra và lý do
- Các tác vụ sprint đang hoạt động và trạng thái hiện tại của chúng
- Các lần gọi agent và kết quả (thành công/thất bại/bị chặn)
- Kết quả test (số lượng pass/fail, các lỗi cụ thể)
- Các điểm nghẽn chưa giải quyết hoặc câu hỏi đang chờ người dùng phản hồi
- Tác vụ hiện tại và chúng ta đang ở bước nào
- Những phần nào của tài liệu hiện tại đã ghi vào file so với những phần đang làm dở

**Sau khi nén:** Đọc `production/session-state/active.md` và bất kỳ file nào đang được xử lý để phục hồi đầy đủ ngữ cảnh. Các file chứa quyết định; lịch sử trò chuyện chỉ là thứ yếu.

## Phục hồi sau sự cố Crash phiên làm việc

Nếu một phiên làm việc bị ngắt ("prompt too long") hoặc bạn bắt đầu một phiên mới để tiếp tục công việc:

1. Hook `session-start.sh` sẽ tự động phát hiện và xem trước `active.md`
2. Đọc file trạng thái đầy đủ để lấy ngữ cảnh
3. Đọc (các) file đang làm dở được liệt kê trong file trạng thái
4. Tiếp tục từ phần chưa hoàn thành hoặc tác vụ tiếp theo
