# Đặc tả Axit Runtime Binding v1 (Axit Runtime Binding Spec v1)

## Mục đích (Purpose)

Một Runtime Binding ánh xạ một Capability ngữ nghĩa ổn định của Axit tới một transport thực thi cụ thể.

Nó trả lời câu hỏi:

> Môi trường này có thể thực hiện thao tác ngữ nghĩa do Axit yêu cầu bằng cách nào?

Một Runtime Binding **không** quyết định:

- bằng chứng nào là cần thiết -> Skill / `verify-change`;
- bằng chứng là BẮT BUỘC (REQUIRED) hay BỔ TRỢ (SUPPORTING) -> `verify-change`;
- PASS / FAIL / BLOCKED -> `verify-change`;
- thao tác có được cấp quyền hay không -> Runtime/Harness policy;
- ý định sản phẩm hoặc kiến trúc -> Profile / Rules / Registry sở hữu.

Mối quan hệ được thiết kế theo luồng:

```text
tiêu chí được chấp thuận (accepted criterion)
  -> Capability ngữ nghĩa
      -> Runtime Binding
          -> (các) thao tác transport cụ thể
              -> kết quả thu thập + bằng chứng
                  -> đánh giá của verify-change
```

## Vị trí chuẩn mực (Canonical location)

Các định nghĩa binding đã được đánh giá nằm dưới:

```text
.axit/bindings/<system-id>/<binding-id>.yaml
```

Không tạo file binding cho đến khi transport thực tế và các thao tác cụ thể của nó đã được kiểm tra hoặc thực thi kiểm chứng.

Một System sau đó có thể tham chiếu một hoặc nhiều định nghĩa binding đã được đánh giá từ file sidecar capability của nó. Việc chưa có tham chiếu binding có nghĩa là Capability đã được biết về mặt ngữ nghĩa nhưng chưa được gắn kết (unbound) trong cấu hình repository.

## Định nghĩa binding so với Tính khả dụng tại runtime

Giữ các phần này tách biệt rõ ràng:

- **định nghĩa binding (binding definition)** — ánh xạ được quản lý theo mã nguồn từ các mã Capability ngữ nghĩa tới các thao tác transport đã xác minh;
- **tính khả dụng tại runtime (runtime availability)** — liệu transport, editor/tiến trình, kết nối, dependency và quyền hạn có thể sử dụng ngay lúc này hay không;
- **thông tin xác thực/endpoints/secrets** — cấu hình môi trường/runtime nằm ngoài các file Axit chuẩn mực.

Một binding được commit với `status: active` có nghĩa là ánh xạ đó đã được đánh giá duyệt. Nó **không** có nghĩa là transport đang kết nối trên mọi máy lập trình viên.

Không commit mật khẩu, API keys, auth tokens, đường dẫn socket máy cục bộ, cổng tạm thời hoặc các bí mật khác vào trong `.axit`.

## Cấu trúc binding tối thiểu

Chỉ tạo ra khi transport thực tế đã khả dụng:

```yaml
spec_version: axit.runtime-binding/v1
id: <binding-id>
system: <system-id>
status: validation

transport:
  family: <mcp|cli|editor-host|other>
  adapter: <verified transport/adapter identity>

mappings:
  - capability: unity.prefab.inspect
    operations:
      - <verified concrete transport operation>
```

Tên của provider/công cụ được phép xuất hiện **bên trong Runtime Bindings** vì tầng này tồn tại cụ thể để cô lập chi tiết transport khỏi các mã Capability ngữ nghĩa chuẩn mực.

Không sao chép ngược lại tên provider/công cụ vào các mã Capability, Skill, Workflow, Profile, hoặc Registry.

## Quy tắc ánh xạ

1. Mọi `capability` trong một binding phải tồn tại trong tập hợp Capability đang hoạt động của System bị ảnh hưởng.
2. Tuyệt đối không tự ý bịa tên thao tác transport. Hãy kiểm tra giao diện transport thực tế trước.
3. Một Capability ngữ nghĩa có thể ánh xạ tới một hoặc nhiều thao tác transport có thứ tự khi transport yêu cầu nhiều lệnh gọi để tạo ra một quan sát đáng tin cậy.
4. Ánh xạ phạm vi transport nhỏ nhất cần thiết cho Capability.
5. Không bind các thao tác không liên quan chỉ vì transport hiển thị chúng.
6. Một binding phải bảo toàn ranh giới bằng chứng của Capability: nó không thể tuyên bố nhiều hơn những gì `can_establish` cho phép.
7. Một binding không thể làm suy yếu lớp tác dụng phụ đã khai báo của Capability.
8. Các binding bằng chứng v1 không được có ngữ nghĩa ghi code chỉ vì transport có khả năng thay đổi file dự án.

## Phân giải tại Runtime

Khi một Capability đã chọn cần được thu thập:

1. xác định System bị ảnh hưởng;
2. xác nhận mã Capability được khai báo trong tập hợp capability đang hoạt động của System;
3. phân giải một binding đã được đánh giá có ánh xạ Capability đó;
4. kiểm tra tính khả dụng của transport/môi trường hiện tại;
5. kiểm tra chính sách Runtime/Harness và các phê duyệt;
6. chỉ thực thi (các) thao tác đã ánh xạ cần thiết cho yêu cầu bằng chứng có giới hạn;
7. chuẩn hóa kết quả thu thập;
8. trả lại bằng chứng cho verifier mà không tự ý đưa ra kết luận xác minh.

Không âm thầm thay thế bằng một transport chưa đăng ký vì thấy tiện lợi. Một transport dự phòng phải có binding tương thích đã được đánh giá riêng hoặc được phê duyệt rõ ràng như một hành động runtime đặc biệt nằm ngoài các tuyên bố binding chuẩn mực.

## Các lớp kết quả thu thập (Acquisition result classes)

Một binding/runtime phải phân biệt tối thiểu các kết quả sau:

### `acquired` (Đã thu thập)

Transport đã tạo ra một quan sát đáng tin cậy về target/phạm vi được yêu cầu.

Quan sát có thể chứng minh sản phẩm thành công, sản phẩm thất bại, hoặc chỉ là bằng chứng một phần. `acquired` **không** đồng nghĩa với PASS.

### `unavailable` (Không khả dụng)

Capability ngữ nghĩa không có binding khả dụng hoặc editor/tiến trình/môi trường/dependency bắt buộc hiện không khả dụng.

Đây là trường hợp thiếu bằng chứng, không phải lỗi sản phẩm.

### `denied` (Bị từ chối)

Chính sách Runtime/Harness hoặc yêu cầu phê duyệt đã ngăn chặn thao tác.

Đây không phải lỗi sản phẩm. Việc nó có chặn xác minh hay không phụ thuộc vào việc bằng chứng đó có phải là REQUIRED hay không.

### `transport_error` (Lỗi transport)

Transport/binding bị lỗi trước khi tạo ra quan sát target đáng tin cậy.

Coi đây là lỗi thu thập trừ khi bản thân đầu ra trả về chứa bằng chứng sản phẩm đáng tin cậy.

Các lớp thu thập này không phải là kết luận xác minh (verdict).

## Nguồn gốc bằng chứng (Evidence provenance)

Một binding thành công nên trả về đủ thông tin để verifier hiểu:

- mã Capability ngữ nghĩa được yêu cầu;
- target/phạm vi cụ thể được quan sát;
- transport/binding được sử dụng;
- các quan sát có cấu trúc hoặc thô liên quan đến tiêu chí;
- các chẩn đoán/cảnh báo ảnh hưởng đến độ tin cậy;
- liệu thao tác có thực sự hoàn thành.

Không bắt buộc một schema payload bằng chứng phổ quát duy nhất trước khi transport thực tế chứng minh cấu trúc hữu ích. Hãy bảo toàn bằng chứng có cấu trúc bản địa khi có thể thay vì làm phẳng mọi thứ thành văn bản thuần.

## Ranh giới Phân quyền (Permission boundary)

Việc phân giải binding không đồng nghĩa với cấp quyền thực thi.

Ví dụ:

- `unity.prefab.inspect` là ý định ngữ nghĩa chỉ đọc, nhưng lệnh gọi transport vẫn phải tuân theo chính sách truy cập Runtime/Harness;
- `unity.playmode.verify` có thể bước vào trạng thái runtime có kiểm soát và có thể yêu cầu trạng thái editor/phiên làm việc hoặc phê duyệt được phép;
- một transport đồng thời hiển thị các thao tác phá hoại file/editor không làm cho các thao tác đó trở thành một phần của binding.

Runtime/Harness vẫn là ranh giới tin cậy.

## Vòng đời Binding

Sử dụng các trạng thái định nghĩa sau:

- `validation` — ánh xạ cụ thể đã tồn tại nhưng chưa vượt qua bằng chứng kiểm thử hồi quy/vertical-slice live;
- `active` — ánh xạ đã vượt qua sử dụng thực tế cho phạm vi capability đã khai báo;
- `deprecated` — chỉ giữ lại để migration/tham chiếu và không nên chọn cho các lần chạy mới.

Không dùng trạng thái binding để biểu thị việc transport cục bộ hiện có online hay không.

## Bài kiểm tra chất lượng

Trước khi chấp nhận một Runtime Binding, hãy tự hỏi:

1. Mọi mã Capability được ánh xạ đã tồn tại trong tập hợp ngữ nghĩa đang hoạt động chưa?
2. Tên thao tác transport cụ thể đã được xác minh thay vì suy đoán chưa?
3. Ánh xạ có bảo toàn các ranh giới tác dụng phụ và bằng chứng của Capability không?
4. Tính khả dụng và thông tin xác thực có được giữ ngoài các định nghĩa ngữ nghĩa chuẩn mực không?
5. Runtime có thể phân biệt bằng chứng thu thập được với các trường hợp unavailable, denied, và transport_error không?
6. Chính sách có vẫn kiểm soát việc thao tác có được phép thực thi không?
7. Binding có được giới hạn ở tập hợp con nhỏ nhất đã được chứng minh không?
8. Transport có thể được thay thế sau này mà không làm thay đổi các mã Capability/Skill/Workflow/Profile không?

Nếu không, hãy giữ capability ở trạng thái chưa gắn kết (unbound).
