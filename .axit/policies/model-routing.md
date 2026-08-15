# Chính sách định tuyến Model & Chi phí của Axit (Axit Model Routing & Cost Policy)

Trạng thái: active

Chính sách này kiểm soát việc phân bổ model cho các milestone chạy liên tục và công việc được ủy quyền trong Axit. Mục tiêu là tối đa hóa tổng thông lượng hữu ích và chất lượng bằng chứng trên mỗi đơn vị chi phí, không phải tối đa hóa kích thước model trên mọi luồng công việc (lane).

## Phân bổ vai trò mặc định

```text
primary orchestrator = gpt-5.6-sol / xhigh
all child lanes       = gpt-5.6-luna / medium
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

Một luồng con không được tự ý chuyển từ Luna sang Terra/Sol hoặc tăng mức độ suy luận (reasoning) vượt quá mức `medium`.

Khi một luồng con chưa hoàn thành, chạy chậm, hoặc sai sót, hãy tuân theo thứ tự sau:

```text
làm sắc bén / tinh lọc ngữ cảnh tác vụ
  -> điều chỉnh hướng dẫn hoặc tiếp tục chính luồng Luna/medium đó
  -> thay thế bằng một luồng Luna/medium mới khi cần
  -> phân rã tác vụ thành các luồng nhỏ hơn có thể kiểm tra được
  -> thu thập lại bằng chứng hiện tại
```

Không giải quyết lỗi của luồng con bằng cách âm thầm mua model lớn hơn.

Nếu ngân sách phục hồi/thay thế được chấp thuận đã cạn kiệt và luồng bắt buộc vẫn chưa thể giải quyết, hãy báo cáo luồng chưa giải quyết đó thông qua ngữ nghĩa failure/blocker hiện có của milestone. Không tự động leo thang lớp model hoặc mức suy luận.

## Ranh giới ghi đè của con người (Human override boundary)

Chỉ có chỉ dẫn rõ ràng hiện tại của người dùng mới có quyền cho phép ghi đè model/suy luận của luồng con.

Một lần ghi đè có giới hạn phải ghi nhận:

- luồng công việc và lý do;
- model/mức suy luận được yêu cầu;
- phạm vi / thời lượng;
- kết quả;
- việc ghi đè có hết hạn sau luồng/milestone đó hay không.

Một lệnh ghi đè sẽ hết hạn khi kết thúc phạm vi đã nêu. Tuyệt đối không để nó âm thầm trở thành giá trị mặc định mới của workspace.

## Chính sách chạy song song (Parallelism policy)

Chỉ sử dụng tính năng đồng thời để giảm thời gian thực tế (wall-clock time) khi các luồng hoàn toàn độc lập với nhau.

- Chạy song song việc khám phá chỉ đọc, tìm kiếm ứng viên, phân tích tĩnh, và đánh giá bằng chứng độc lập khi thấy hữu ích.
- Tuần tự hóa các thao tác ghi sản phẩm chồng chéo, ghi Runtime Binding, ghi state/báo cáo, và các đột biến Unity/editor.
- Không khởi tạo agent chỉ để lấp đầy các slot trống khả dụng.
- Đóng kịp thời các luồng đã hoàn thành hoặc lỗi thời để công việc hữu ích có thể tái sử dụng năng lực xử lý.
- Ưu tiên bàn giao ngữ cảnh tinh gọn thay vì phát lại toàn bộ lịch sử hội thoại hoặc lịch sử milestone.

Giới hạn số luồng của phiên làm việc là mức trần tối đa, không phải mục tiêu cần đạt.

## Chính sách hiệu quả ngữ cảnh (Context-efficiency policy)

Prompt của luồng con phải chứa ngữ cảnh đầy đủ tối thiểu cần thiết để hành động chính xác:

- mục tiêu/tiêu chí chính xác đã được chấp thuận;
- phạm vi được phép và bị cấm;
- con trỏ trỏ trực tiếp tới file/sản phẩm;
- bằng chứng/trạng thái hiện tại có liên quan;
- schema đầu ra tinh gọn kỳ vọng.

Không nạp trước đệ quy toàn bộ thư mục `.axit/`, không phát lại toàn bộ lịch sử chat, hoặc gửi các bản ghi chép milestone không liên quan cho luồng con.

Để duy trì tính liên tục/thay thế, chỉ trích xuất kết quả hữu ích gần nhất, trạng thái hiện tại, công việc chưa giải quyết, các file đã thay đổi, và bằng chứng bắt buộc phải thu thập lại.

## Chất lượng xác minh dưới Luna/medium

Việc xác minh độc lập được thiết lập bởi tính độc lập về trách nhiệm và bằng chứng, không phải bằng cách sử dụng một model đắt tiền hơn.

Một verifier vẫn phải:

- sử dụng ngữ cảnh mới thay vì tin tưởng kết luận của worker;
- thu thập lại hoặc kiểm tra bằng chứng BẮT BUỘC (REQUIRED) hiện tại;
- bảo toàn ngữ nghĩa REQUIRED/SUPPORTING và PASS/FAIL/BLOCKED;
- duy trì trạng thái chỉ đọc khi vai trò yêu cầu;
- từ chối bằng chứng lỗi thời và các tuyên bố vượt quá thực tế.

Nếu một tiêu chí quá rộng để xác minh đáng tin cậy, hãy phân rã nó thành các xác nhận rõ ràng có thể kiểm tra được thay vì tự động tăng model của verifier.

## Thực thi cấu hình (Configuration enforcement)

Các giá trị mặc định của dự án phải duy trì sự liên kết với chính sách này:

```toml
model = "gpt-5.6-sol"
model_reasoning_effort = "xhigh"

[agents]
default_subagent_model = "gpt-5.6-luna"
default_subagent_reasoning_effort = "medium"
```

Các định nghĩa sub-agent tùy chỉnh cũng phải sử dụng `gpt-5.6-luna` / `medium` trừ khi có lệnh ghi đè rõ ràng có giới hạn từ con người.

Các milestone tự động chạy dài phải ghi lại model/mức suy luận thực tế được quan sát của primary và child. Ý định cấu hình không được coi là chân lý runtime được quan sát.

## Kế toán hiệu năng (Performance accounting)

Mỗi báo cáo/tổng kết milestone dài cần ghi nhận, khi có thể quan sát:

```text
primary model/reasoning
child default model/reasoning
child lanes spawned
sub-agent replacements
repair/recovery loops
wall-clock duration
model overrides (kỳ vọng: 0 trừ khi có ủy quyền rõ ràng từ con người)
```

Một closure verifier phải đánh dấu việc tự ý leo thang model/suy luận của luồng con chưa được phê duyệt như một vi phạm kiểm soát (control finding) ngay cả khi bằng chứng sản phẩm đạt yêu cầu.

Việc tinh chỉnh hiệu năng trước tiên phải cải thiện khâu phân rã tác vụ, kích thước ngữ cảnh, các luồng đọc song song, ranh giới tuần tự hóa và hành vi phục hồi. Không coi việc dùng model con lớn hơn là giải pháp sửa lỗi hiệu năng mặc định.
