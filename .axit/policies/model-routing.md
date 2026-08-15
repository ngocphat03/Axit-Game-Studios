# Chính sách định tuyến Model & Chi phí của Axit (Axit Model Routing & Cost Policy)

Trạng thái: active

Chính sách này kiểm soát việc phân bổ model cho các milestone chạy liên tục và công việc được ủy quyền trong Axit với nền tảng **Google Antigravity & Gemini**. Mục tiêu là tối đa hóa tổng thông lượng hữu ích và chất lượng bằng chứng trên mỗi đơn vị chi phí.

## Phân bổ vai trò mặc định (Default Role Allocation)

```text
primary orchestrator = gemini-2.5-pro / high
all child lanes       = gemini-2.5-flash / medium (hoặc gemini-2.5-pro cho kiểm chứng phức tạp)
```

`all child lanes` (tất cả các luồng con) bao gồm đầy đủ, không có ngoại lệ theo tên vai trò:

- các agent thám hiểm và khảo sát (explorers / scouts);
- các worker triển khai code (implementation workers);
- các worker chạy test/build/thu thập bằng chứng;
- các worker thu thập dữ liệu Unity/MCP;
- các agent sửa chữa/phục hồi (repair/recovery agents);
- các verifier độc lập (independent verifiers);
- các verifier đóng milestone (closure verifiers);
- tác giả viết báo cáo / tổng kết (report/retrospective authors);
- bất kỳ sub-agent dự án tùy chỉnh nào trừ khi có chỉ thị ghi đè rõ ràng hiện tại từ con người.

Luồng chính (primary thread) chỉ giữ vai trò điều phối khi tính năng phân quyền khả dụng. Một luồng con không đương nhiên được cấp model lớn hơn chỉ vì nhiệm vụ của nó là xác minh, phục hồi hoặc đóng milestone.

## Không tự ý leo thang model (No silent escalation)

Một luồng con không được tự ý leo thang mức độ suy luận hoặc chuyển model vượt quá phạm vi được giao.

Khi một luồng con chưa hoàn thành, chạy chậm, hoặc sai sót, hãy tuân theo thứ tự sau:

```text
làm sắc bén / tinh lọc ngữ cảnh tác vụ
  -> điều chỉnh hướng dẫn hoặc tiếp tục chính luồng đó
  -> thay thế bằng một luồng sub-agent mới khi cần
  -> phân rã tác vụ thành các luồng nhỏ hơn có thể kiểm tra được
  -> thu thập lại bằng chứng hiện tại
```

Không giải quyết lỗi của luồng con bằng cách âm thầm đổi sang model đắt đỏ hơn mà không phân tích nguyên nhân.

## Ranh giới ghi đè của con người (Human override boundary)

Chỉ có chỉ dẫn rõ ràng hiện tại của người dùng mới có quyền cho phép ghi đè model/suy luận của luồng con.

Một lần ghi đè có giới hạn phải ghi nhận:

- luồng công việc và lý do;
- model/mức suy luận được yêu cầu;
- phạm vi / thời lượng;
- kết quả;
- việc ghi đè có hết hạn sau luồng/milestone đó hay không.

## Chính sách chạy song song (Parallelism policy)

Chỉ sử dụng tính năng đồng thời để giảm thời gian thực tế (wall-clock time) khi các luồng hoàn toàn độc lập với nhau.

- Chạy song song việc khám phá chỉ đọc, tìm kiếm ứng viên, phân tích tĩnh, và đánh giá bằng chứng độc lập khi thấy hữu ích.
- Tuần tự hóa các thao tác ghi sản phẩm chồng chéo, ghi Runtime Binding, ghi state/báo cáo, và các đột biến Unity/editor.
- Đóng kịp thời các luồng đã hoàn thành để tái sử dụng năng lực xử lý.

## Chính sách hiệu quả ngữ cảnh (Context-efficiency policy)

Prompt của luồng con phải chứa ngữ cảnh đầy đủ tối thiểu cần thiết để hành động chính xác:

- mục tiêu/tiêu chí chính xác đã được chấp thuận;
- phạm vi được phép và bị cấm;
- con trỏ trỏ trực tiếp tới file/sản phẩm;
- bằng chứng/trạng thái hiện tại có liên quan;
- schema đầu ra tinh gọn kỳ vọng.

## Chất lượng xác minh dưới Gemini

Việc xác minh độc lập được thiết lập bởi tính độc lập về trách nhiệm và bằng chứng, không phải bằng cách sử dụng một model đắt tiền hơn.

Một verifier vẫn phải:

- sử dụng ngữ cảnh mới thay vì tin tưởng kết luận của worker;
- thu thập lại hoặc kiểm tra bằng chứng BẮT BUỘC (REQUIRED) hiện tại;
- bảo toàn ngữ nghĩa REQUIRED/SUPPORTING và PASS/FAIL/BLOCKED;
- duy trì trạng thái chỉ đọc khi vai trò yêu cầu;
- từ chối bằng chứng lỗi thời và các tuyên bố vượt quá thực tế.
