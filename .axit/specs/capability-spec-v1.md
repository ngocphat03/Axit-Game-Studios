# Đặc tả Axit Capability v1 (Axit Capability Spec v1)

## Mục đích (Purpose)

Một Capability là một **thao tác mang tính ngữ nghĩa có thể thu thập bằng chứng hoặc thực hiện một bước thực thi có kiểm soát** cho Axit.

Capability trả lời câu hỏi:

> Runtime có thể quan sát hoặc thực thi điều gì để thu thập bằng chứng?

Chúng không trả lời các câu hỏi:

- ai sở hữu quyết định -> Profile;
- quy trình nào cần tuân theo -> Skill;
- kết hợp nhiều quy trình ra sao -> Workflow;
- bằng chứng đã đủ hay chưa -> `verify-change`;
- provider/công cụ nào truyền tải thao tác -> runtime binding;
- thao tác có được cấp quyền hay không -> Runtime/Harness policy.

Sự phân tách này là có chủ đích:

```text
Skill / Workflow
    -> yêu cầu bằng chứng (asks for evidence)
        -> Capability ngữ nghĩa
            -> runtime binding
                -> MCP / CLI / Axit host / editor automation / transport khác
                    -> bằng chứng (evidence)
                        -> đánh giá của verify-change
```

Mã định danh Capability (Capability id) phải duy trì tính ổn định ngay cả khi transport bên dưới thay đổi.

## Các tập hợp Capability chuẩn mực (Canonical capability sets)

Các tập hợp capability ngữ nghĩa có thể tái sử dụng nằm dưới:

```text
.axit/capabilities/<domain>/<set>.yaml
```

Ví dụ:

```text
.axit/capabilities/unity/evidence.yaml
```

Một tập hợp capability nhóm các thao tác có chung domain và mục đích. Nó không phải là một file cấu hình công cụ.

Cấu trúc cấp cao nhất được khuyến nghị:

```yaml
spec_version: axit.capability-set/v1
id: unity-evidence
domain: unity
purpose: evidence-acquisition
status: validation

capabilities:
  - id: unity.compile
    summary: Observe whether Unity scripts compile in the current configured environment.
    operation: execute
    side_effects: derived-artifacts
    evidence:
      can_establish: []
      cannot_establish: []
```

Chỉ những trường được sử dụng bởi một tập hợp capability thực tế mới nên được thêm vào v1.

## Định danh Capability (Capability identity)

Capability id mô tả **ý đồ ngữ nghĩa (semantic intent)**, không mô tả cú pháp công cụ.

Tốt:

```text
unity.compile
unity.prefab.inspect
unity.playmode.verify
```

Tránh:

```text
mcp.unity.execute_menu_item
unity-cli.batchmode-command
coplay.get_prefab
```

Tên provider/công cụ thuộc về runtime binding, không thuộc về capability id chuẩn mực.

## Phân giải Capability ID

Khi lên kế hoạch, yêu cầu hoặc báo cáo một Capability, **chỉ sử dụng các id được khai báo rõ ràng bởi tập hợp capability đang hoạt động của System bị ảnh hưởng**.

Tuyệt đối không tự ý bịa ra, đặt bí danh, đổi tên, viết tắt hoặc tổng hợp các capability id, bao gồm cả những tên gọi thân thiện hoặc đặc thù dự án nghe có vẻ phù hợp.

Ví dụ, nếu tập hợp đang hoạt động khai báo:

```text
unity.prefab.inspect
unity.component.inspect
unity.serialized-fields.inspect
```

thì các tên như sau là không hợp lệ trừ khi chúng được khai báo riêng trong một tập hợp đang hoạt động:

```text
player_prefab.resolve_asset
prefab.inspect_serialized_component
configuration.compare_to_accepted_contract
```

Nhu cầu bằng chứng bằng ngôn ngữ tự nhiên không tự động trở thành một Capability.

Nếu bằng chứng cần thiết không khớp với Capability đã khai báo nào:

1. nêu rõ rằng hiện chưa có Axit Capability phù hợp nào được khai báo;
2. mô tả nhu cầu bằng chứng còn thiếu bằng ngôn ngữ thông thường;
3. sử dụng các bằng chứng hoặc luồng xác thực dự án hợp lệ không phải capability khi chúng có thể thỏa mãn tiêu chí;
4. nếu không, hãy báo cáo **khoảng trống capability (capability gap)** hoặc nhu cầu bằng chứng chưa khả dụng;
5. không bịa ra một id để làm cho kế hoạch bằng chứng trông có vẻ hoàn chỉnh.

Các cơ chế dự án vẫn là bằng chứng thông thường cho đến khi chúng được chấp nhận một cách có chủ đích vào một tập hợp Capability. Ví dụ: một lệnh test C# tất định độc lập không tự nhiên trở thành `unity.tests.run` chỉ vì nó test code của dự án Unity; `unity.tests.run` chỉ áp dụng khi bằng chứng được chọn thực sự là một bộ test Unity được đại diện bởi hợp đồng Capability đó.

## Các lớp thao tác (Operation classes)

v1 sử dụng hai lớp thao tác:

- `inspect` — quan sát trạng thái source/editor/runtime hiện có mà không chủ ý khởi chạy một hành vi thực thi;
- `execute` — chạy một kiểm tra có giới hạn hoặc hành động runtime có kiểm soát để tạo ra bằng chứng.

Điều này chỉ mô tả ý định. Nó không tự động cấp quyền.

## Các lớp tác dụng phụ (Side-effect classes)

Một capability khai báo lớp tác dụng phụ nhỏ nhất dự kiến:

- `read-only` — kiểm tra trạng thái mà không chủ ý thay đổi trạng thái dự án/runtime;
- `derived-artifacts` — có thể tạo cache, logs, đầu ra test/build, hoặc các file phái sinh có thể tái tạo khác;
- `controlled-runtime` — có thể bước vào/điều khiển trạng thái runtime/editor có giới hạn như Play Mode rồi trả về bằng chứng.

Một capability thay đổi trong tương lai có thể yêu cầu thêm lớp tác dụng phụ, nhưng bằng chứng v1 không đưa vào ngữ nghĩa capability ghi source code.

Chính sách Runtime/Harness vẫn giữ vai trò chính thức. Việc khai báo một Capability không bao giờ bỏ qua các giới hạn về phê duyệt, đường dẫn, câu lệnh, hoặc môi trường.

## Hợp đồng bằng chứng (Evidence contract)

Mỗi capability nên nêu rõ:

```yaml
evidence:
  can_establish:
    - ...
  cannot_establish:
    - ...
```

`can_establish` mô tả các quan sát mà capability có thể đóng góp hợp lệ khi thu thập thành công.

`cannot_establish` ngăn chặn các tuyên bố vượt quá thực tế. Ví dụ: biên dịch thành công không chứng minh được hành vi gameplay, và inspect prefab không chứng minh được đối tượng runtime được khởi tạo hoạt động chính xác.

Không biến tài liệu capability thành tiêu chí chấp nhận. Tác vụ được chấp thuận và các quy tắc dự án vẫn định nghĩa những gì phải được chứng minh.

## Bằng chứng so với Kết luận (Evidence vs verdict)

Đầu ra của Capability là **bằng chứng**, không bao giờ là kết luận cuối cùng.

`verify-change` vẫn quyết định:

- tiêu chí nào quan trọng;
- bằng chứng nào là `REQUIRED` (bắt buộc) hay `SUPPORTING` (bổ trợ);
- liệu bằng chứng hiện tại chứng minh, làm thất bại hay để ngỏ tiêu chí;
- kết luận cuối cùng `PASS`, `FAIL`, hoặc `BLOCKED`.

Ví dụ:

```text
Tiêu chí: scripts phải compile thành công
unity.compile quan sát thấy compile errors
=> bằng chứng có thể chứng minh FAIL
```

```text
Tiêu chí: phép tính số học sát thương tất định là chính xác
các test tất định tập trung đã chứng minh điều đó
unity.playmode.verify không khả dụng
=> bằng chứng runtime có thể giữ vai trò SUPPORTING thay vì chặn kết luận PASS
```

```text
Tiêu chí: tính năng hoạt động trong Unity Play Mode
unity.playmode.verify không khả dụng
=> bằng chứng REQUIRED không khả dụng -> BLOCKED trừ khi một lỗi bắt buộc khác đã được chứng minh
```

Không biến một Capability thành `REQUIRED` toàn cục chỉ vì nó tồn tại.

## Lỗi thu thập so với Lỗi sản phẩm (Acquisition failure vs product failure)

Giữ các trường hợp này tách biệt rõ ràng:

1. **Bằng chứng chứng minh sản phẩm thất bại (target failure)** — ví dụ trình biên dịch Unity đã chạy và báo lỗi compile trong code bị ảnh hưởng. Điều này có thể chứng minh một tiêu chí bắt buộc bị `FAILED`.
2. **Capability không khả dụng (unavailable)** — không có runtime binding khả dụng, thiếu phiên bản editor bắt buộc, thiếu môi trường, quyền hạn, hoặc dependency. Đây là thiếu bằng chứng, không phải bằng chứng chứng minh sản phẩm bị hỏng.
3. **Cơ chế thu thập bị lỗi (acquisition mechanism errors)** — binding/công cụ bị lỗi trước khi tạo ra quan sát đáng tin cậy. Coi đây là sự cố thu thập bằng chứng trừ khi bản thân đầu ra chứng minh sản phẩm bị lỗi.

Runtime binding nên lưu giữ đủ chi tiết để verifier phân biệt các trường hợp này.

## Tách biệt Runtime binding

Định nghĩa Capability chuẩn mực không chứa các lời gọi provider/công cụ.

Một runtime binding ánh xạ:

```text
mã capability ngữ nghĩa (semantic capability id)
    -> transport thực thi khả dụng (available execution transport)
```

Các transport có thể bao gồm:

- Unity MCP;
- Axit Unity host;
- Unity CLI/batch mode;
- các lệnh test/build cục bộ;
- editor automation;
- các provider thực thi từ xa trong tương lai.

Bindings có thể khác nhau tùy theo máy của lập trình viên hoặc môi trường runtime mà không làm thay đổi Skills, Workflows, Profiles, hoặc capability ids.

Không cụ thể hóa danh mục binding trước khi transport thực tế được kết nối và kiểm thử.

## Điều hướng Capability theo System

Một System có thể khai báo tập hợp capability ngữ nghĩa nào được áp dụng thông qua file sidecar tùy chọn:

```text
.axit/systems/<system-id>/capabilities.yaml
```

Cấu trúc tối thiểu:

```yaml
spec_version: axit.system-capabilities/v1
system: unity-client
capability_sets:
  - .axit/capabilities/unity/evidence.yaml
```

File sidecar có nghĩa là tập hợp capability ngữ nghĩa có liên quan tới System. Nó **không** có nghĩa là mọi capability hiện đều có thể thực thi được.

Tính khả dụng phụ thuộc vào runtime bindings và trạng thái môi trường.

Điều hướng gốc chỉ nên đọc sidecar này khi việc thu thập bằng chứng cho System đó là phù hợp. Không nạp trước danh mục capability cho các công việc thiết kế hoặc chỉ thuần kiến trúc thông thường.

## Lựa chọn Capability

Khi một Skill/Workflow cần bằng chứng:

1. xác định tiêu chí đã được chấp thuận;
2. chọn loại bằng chứng nhỏ nhất có thể xác lập tiêu chí đó;
3. kiểm tra các tập hợp capability đã khai báo của System bị ảnh hưởng khi bằng chứng thực thi/editor là phù hợp;
4. chọn Capability ngữ nghĩa hẹp nhất **đã được khai báo** có thể thu thập bằng chứng;
5. nếu không có Capability khai báo nào khớp, hãy sử dụng bằng chứng hợp lệ không phải capability khi đã đủ hoặc báo cáo nhu cầu bằng chứng như một khoảng trống capability mà không tự ý bịa id;
6. để runtime/Harness phân giải tính khả dụng, quyền hạn, và transport binding;
7. trả lại bằng chứng đã thu thập cho Skill/Workflow sở hữu;
8. giữ ngữ nghĩa kết luận nằm trong `verify-change`.

Không gọi các kiểm tra runtime rộng hơn khi bằng chứng tất định hẹp hơn đã đủ.

## Phân định phạm vi sở hữu (Scope ownership)

Các capability engine/domain có thể tái sử dụng thuộc về `.axit/capabilities/`.

Các thông tin cục bộ theo System như tên scene đặc thù dự án, đường dẫn prefab, bộ test, hoặc quy ước thuộc về System Rules/Architecture/Knowledge hoặc source/tests.

Ví dụ:

```text
unity.prefab.inspect
    = Capability có thể tái sử dụng

"Player.prefab phải có DamageableBodyPart"
    = Tiêu chí dự án/System hoặc thông tin kiến trúc
```

Không tạo mỗi tính năng game một capability id.

## Bài kiểm tra chất lượng Capability

Trước khi chấp nhận một capability vào tập hợp tái sử dụng, hãy tự hỏi:

1. Đây có phải là một thao tác ngữ nghĩa có thể quan sát/thực thi thay vì một quy trình hay trách nhiệm không?
2. Id của nó có thể tồn tại khi thay đổi từ MCP sang transport khác không?
3. Nó có tạo ra bằng chứng mà verifier có thể diễn giải mà không trao cho nó quyền đưa ra kết luận không?
4. Tác dụng phụ của nó có đủ rõ ràng cho chính sách Runtime/Harness không?
5. `cannot_establish` có ngăn chặn được các tuyên bố vượt quá thực tế phổ biến không?
6. Nó có thể tái sử dụng trên các dự án khác biệt thực tế trong cùng domain không?
7. Các chi tiết đặc thù của system/project có thể nằm ngoài định nghĩa capability không?
8. Id có được khai báo rõ ràng thay vì được tổng hợp tùy tiện trong khi lên kế hoạch tác vụ không?

Nếu không, hãy giữ hành vi đó nằm trong Skill sở hữu, ngữ cảnh System, runtime binding, luồng xác thực dự án thông thường, hoặc tầng công cụ.