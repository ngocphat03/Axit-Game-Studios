# Hợp đồng đóng Milestone của Axit (Axit Milestone Closure Contract)

Trạng thái: active

Hợp đồng này áp dụng cho mọi kế hoạch thực thi đặc thù theo từng milestone.

Người thực thi milestone không được trả về `MILESTONE_DONE` cho đến khi các sản phẩm đóng milestone đã được lưu trữ bền vững, một closure verifier đã kiểm tra chúng, và kết quả cuối cùng của verifier cũng đã được lưu trữ và kiểm tra tính nhất quán.

## Các sản phẩm đóng milestone bắt buộc

Đối với milestone `<id>-<slug>`, hãy tạo/cập nhật:

```text
.axit/milestones/<id>-<slug>/report.md
.axit/milestones/<id>-<slug>/retrospective.md
```

Sử dụng:

```text
.axit/templates/milestone-report.md
.axit/templates/milestone-retrospective.md
```

Các sự cố quan trọng trong pipeline cũng có thể được nối thêm vào `.axit/memory/decision-log.md` khi chúng tạo ra hoặc thay đổi một quy tắc bền vững, kỳ vọng cấu hình, giới hạn, hoặc yêu cầu kiểm thử hồi quy.

## Trình tự đóng Milestone (Closure sequence)

```text
thực thi xong scenario
  -> xác minh độc lập scenario lần cuối
  -> quét tính nhất quán tài liệu/trạng thái
  -> viết báo cáo milestone report
  -> viết tổng kết retrospective
  -> mã hóa các biện pháp củng cố sự cố bền vững
  -> closure verifier kiểm tra ranh giới sản phẩm/bằng chứng
  -> lưu trữ kết luận của closure verifier vào report + retrospective + active state
  -> kiểm tra tính nhất quán chỉ đọc sau kết luận (fresh post-verdict consistency audit)
  -> MILESTONE_DONE
  -> DỪNG LẠI chờ con người đánh giá nghiệm thu (human promotion review)
```

Kết luận trên console/output của closure verifier là chưa đủ. `MILESTONE_DONE` chỉ được phát ra sau khi các tài liệu bền vững không còn mô tả milestone ở trạng thái "đang chờ verifier" vốn đã hoàn thành.

Không tự động mở hoặc thực thi milestone tiếp theo.

## Các kiểm tra tổng kết bắt buộc (Mandatory retrospective checks)

Trước khi đóng milestone, hãy kiểm tra rõ ràng:

- sự cố treo/thay thế sub-agent và liệu việc leo thang có thực sự cần thiết;
- hành vi dừng khẩn cấp (hard-stop) kích hoạt quá sớm hay quá muộn;
- các giả định thiết lập gây ra gián đoạn hoặc chuyển về phương án dự phòng;
- tính không ổn định của transport/runtime và liệu việc phân loại phục hồi có chính xác;
- bằng chứng BẮT BUỘC (REQUIRED) vs BỔ TRỢ (SUPPORTING) và tính đúng đắn của kết luận;
- bằng chứng bị lỗi thời sau khi sửa chữa;
- tính độc lập giữa worker và verifier;
- tài liệu/tham chiếu trạng thái hiện tại bị lỗi thời;
- cấu hình model/suy luận/runtime thực tế so với cấu hình dự định;
- tính tuân thủ định tuyến model con với `.axit/policies/model-routing.md`;
- ngữ cảnh chỉ tồn tại trong hội thoại cần được chuyển thành bộ nhớ bền vững;
- khả năng audit xuyên suốt Workspace repository, System repository độc lập, bằng chứng cục bộ, và bằng chứng runtime ngắn hạn;
- sự phình to framework không cần thiết cần phải bị từ chối.

## Các lớp nguồn gốc bằng chứng (Evidence provenance classes)

Sử dụng lớp chân thực hẹp nhất cho từng sản phẩm/bằng chứng quan trọng:

```text
workspace-canonical-pushed
system-canonical-pushed
local-or-separately-tracked
ephemeral-runtime
```

`workspace-canonical-pushed` nghĩa là bằng chứng có thể tái hiện từ Workspace repository/ref chuẩn mực đã được push.

`system-canonical-pushed` nghĩa là một System có repository chuẩn mực riêng và bằng chứng có thể tái hiện tại đó. Ghi nhận khi biết:

```text
system id
repository
ref
commit
```

Không tự ý giả định Workspace repository gốc nắm giữ lịch sử Git của một System chỉ vì System đó được đặt dưới `src/`.

`local-or-separately-tracked` nghĩa là bằng chứng hiện tại chưa được chứng minh có thể tái hiện từ một commit đã push chuẩn mực của Workspace/System.

`ephemeral-runtime` nghĩa là bằng chứng chỉ tồn tại dưới dạng quan sát trực tiếp từ lệnh/editor/runtime.

Không ép buộc theo dõi Git hoặc thay đổi cấu trúc repository chỉ nhằm mục đích nâng cấp phân loại nguồn gốc bằng chứng.

## Quy tắc củng cố sau sự cố (Incident hardening rule)

Đối với mỗi sự cố lặp lại có ý nghĩa:

```text
sự cố (incident)
  -> nguyên nhân gốc rễ (root cause)
  -> bản sửa framework/cấu hình nhỏ nhất
  -> bảo vệ chống hồi quy (regression protection)
  -> milestone tương lai thừa hưởng bản sửa lỗi
```

Không tạo Profile, Skill, Workflow, Capability, hoặc binding mới chỉ vì một sự cố xảy ra. Hãy chọn tầng giải pháp đúng và nhỏ nhất.

## Ranh giới nghiệm thu (Promotion boundary)

Báo cáo milestone có thể đưa ra khuyến nghị:

```text
PROMOTE
REPAIR_AND_RERUN
```

Chỉ có sự đánh giá của con người mới nghiệm thu (promote) milestone. `MILESTONE_DONE` chỉ có nghĩa là việc thực thi/đóng milestone đã hoàn tất, không có nghĩa là milestone tiếp theo đã được phép bắt đầu.
