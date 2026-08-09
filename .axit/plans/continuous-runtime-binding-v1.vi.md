# Continuous Runtime Binding v1 (Ràng buộc Runtime Liên tục v1)

Trạng thái: ready-after-manual-unity-mcp-setup (sẵn sàng sau khi thiết lập thủ công Unity MCP)

## Mục đích

Chạy giai đoạn tiếp theo của Axit liên tục từ thư mục gốc của repository với một luồng chính (primary thread) lập luận cao đóng vai trò duy nhất là bộ điều phối (orchestrator) và các sub-agent thực hiện công việc thực tế.

**Điều kiện tiên quyết thủ công:** người dùng cấu hình và khởi chạy MCP cho Unity trước khi kế hoạch này được thực thi. Kế hoạch này không cài đặt, cấu hình, khởi chạy, sửa chữa hoặc nâng cấp transport Unity MCP.

Sau khi điều kiện tiên quyết được đáp ứng, quá trình chạy sẽ tiếp tục qua các ranh giới giai đoạn mà không cần hỏi xác nhận định kỳ. Chỉ dừng lại khi gặp rào cản cứng (hard blocker) đã được khai báo hoặc khi kế hoạch đạt trạng thái `DONE`.

## Hợp đồng vận hành

### Luồng chính (Primary thread)

Luồng chính chỉ đóng vai trò điều phối viên. Nó có thể:

- đọc định hướng/trạng thái Axit tối thiểu cần thiết để hiểu giai đoạn đang hoạt động;
- khởi tạo (spawn), điều hướng (steer), tiếp tục (resume), thay thế (replace), chờ đợi (wait for) và đóng (close) các sub-agent;
- phân công các luồng công việc (lanes) không chồng chéo;
- tổng hợp kết quả từ các sub-agent;
- quyết định giai đoạn nào đã được phê duyệt sẽ diễn ra tiếp theo;
- chỉ báo cáo các rào cản cứng (hard blockers) cho người dùng.

Luồng chính phải ủy quyền các công việc: khám phá mã nguồn, thay đổi sản phẩm/mã nguồn, thực thi build/test, thu thập bằng chứng Unity/MCP, sửa chữa trong phạm vi giới hạn, và kiểm tra độc lập (independent verification).

Nó không được ủy quyền hoặc tự thực hiện việc cài đặt/cấu hình Unity MCP vì điều kiện tiên quyết đó thuộc quyền sở hữu riêng của người dùng.

### Chính sách Git-status của Workspace

Đọc `.axit/workspace.yaml` -> `safety.check_git_status` trước bất kỳ bước kiểm tra độ sẵn sàng hay an toàn nào dựa trên trạng thái Git.

Hành vi mặc định là:

```yaml
safety:
  check_git_status: false
```

Khi giá trị là `false`:

- không chạy `git status` ở root, repository lồng nhau hoặc submodule như một cổng kiểm tra an toàn/sẵn sàng;
- không yêu cầu worktree phải sạch (clean)/đã lưu mốc (staged)/đã commit;
- không tạo lỗi `DIRTY_WORKTREE_RISK` từ các trạng thái modified/deleted/untracked/lồng nhau/submodule;
- coi nội dung hệ thống tập tin/mã nguồn hiện tại là mốc cơ bản làm việc cho nhiệm vụ đã chấp nhận.

Khi giá trị là `true`:

- một lane an toàn chỉ đọc có thể kiểm tra trạng thái Git liên quan;
- `DIRTY_WORKTREE_RISK` chỉ hợp lệ khi việc tiếp tục thực sự đe dọa đến công việc không liên quan.

Công tắc này không ủy quyền cho việc reset, checkout, clean, hoàn tác công việc của người dùng, xóa phá hủy hoặc ghi đè mù quáng.

### Các làn công việc Sub-agent (Sub-agent lanes)

Sử dụng làn công việc nhỏ nhất có ích cho từng giai đoạn. Các làn điển hình:

- `exploration` — kiểm tra độ sẵn sàng của nguồn/môi trường/transport mà không thay đổi thiết lập MCP;
- `worker` — thực hiện một thay đổi dự án hoặc Axit giới hạn;
- `verifier` — kiểm tra độc lập trạng thái/bằng chứng hiện tại và đưa ra kết quả kiểm tra;
- `recovery` — tiếp quản một lane bị kẹt trong phạm vi từ ngữ cảnh được tóm tắt khi việc điều hướng/tiếp tục không đủ hiệu quả.

Không để làn worker tự xác nhận (self-certify) một thay đổi khi việc kiểm tra độc lập có tính khả thi.

Các làn chỉ đọc (read-only) có thể chạy song song. Các làn ghi nhiều và các thao tác thay đổi Unity editor/runtime nên được tuần tự hóa khi trạng thái của chúng có thể chồng chéo.

## Chính sách phục hồi (Recovery policy)

Đối với một sub-agent tạm dừng, bị chặn hoặc trả về công việc chưa hoàn thành **sau khi điều kiện sẵn sàng của Unity MCP đã vượt qua**:

1. ghi lại các phát hiện hữu ích, các tập tin bị thay đổi, trạng thái lệnh/runtime và lý do bị chặn rõ ràng;
2. gửi một hướng dẫn phục hồi/điều hướng tập trung đến cùng làn đó khi an toàn;
3. nếu vẫn bị kẹt, thay thế làn đó bằng một sub-agent mới chỉ sử dụng ngữ cảnh được tóm tắt cộng với trạng thái workspace hiện tại;
4. cho phép tối đa hai lần thử phục hồi/thay thế có giới hạn cho cùng một làn/lỗi bắt buộc;
5. thu thập lại bằng chứng hiện tại sau bất kỳ bước sửa chữa hoặc thay thế nào;
6. tự động tiếp tục khi rào cản được giải quyết.

Nếu sub-agent hỏi một làm rõ thông thường có thể trả lời được từ bằng chứng Axit/mã nguồn đã chấp nhận, bộ điều phối phải trả lời thay vì chuyển lên cho người dùng.

Thiết lập Unity MCP bị thiếu hoặc hỏng không phải là làn phục hồi. Đó là lỗi điều kiện tiên quyết thủ công và phải dừng quá trình chạy.

## Rào cản cứng (Hard blockers)

Trả về `HARD_BLOCKER` và dừng lại chỉ khi ít nhất một trong các điều sau thực sự bắt buộc:

- `UNITY_MCP_NOT_READY` — transport Unity MCP do người dùng cấu hình bị thiếu, không thể truy cập, không cung cấp thao tác trực tiếp sử dụng được, hoặc không kết nối đúng với editor/dự án QuickGun dự kiến;
- `PRODUCT_INTENT` — hành vi mong muốn thực tế bị mơ hồ và không thể giải quyết từ bằng chứng đã chấp nhận;
- `ARCHITECTURE_DECISION` — hợp đồng công khai, hướng phụ thuộc, sở hữu trạng thái, hoặc lập trường kiến trúc cần thay đổi ngoài kế hoạch đã chấp nhận;
- `DESTRUCTIVE_SCOPE` — yêu cầu di đống/xóa có tính phá hủy hoặc refactor rộng không liên quan;
- `SECRET_OR_PRODUCTION` — yêu cầu thông tin đăng nhập/bí mật, truy cập production, hoặc ghi lên cloud bên ngoài;
- `ADMIN_ESCALATION` — yêu cầu sudo/admin hoặc cấu hình trên toàn máy;
- `DIRTY_WORKTREE_RISK` — chỉ khi `.axit/workspace.yaml` đặt `safety.check_git_status: true` và việc tiếp tục sẽ làm nguy hại đến công việc không liên quan;
- `REPEATED_REQUIRED_FAILURE` — cùng một lỗi bắt buộc vẫn tiếp diễn sau hai vòng lặp sửa chữa/thay thế có giới hạn.

Không dừng lại chỉ vì một làn sub-agent thông thường thất bại khi vẫn còn tuyến thay thế an toàn.

## Các bất biến toàn cục (Global invariants)

- Bắt đầu từ thư mục gốc của repository.
- Đọc `safety.check_git_status` trước bất kỳ bước kiểm tra an toàn nào dựa trên Git-status.
- Không commit, push, publish, hoặc mở PR.
- Core v1, Workspace/System v1, và Capability semantic v1 là ổn định; không thiết kế lại chúng trong kế hoạch này.
- Không thêm Core Skill #3 hoặc Workflow #2.
- Chỉ sử dụng các semantic Capability id đã khai báo.
- Không tự bịa ra tên transport/server/operation.
- Định nghĩa Runtime Binding có thể chứa các tên đã xác minh theo từng transport; định nghĩa semantic Capability thì không được chứa.
- Giữ bí mật, cổng tạm thời, token, và thông tin đăng nhập theo máy bên ngoài `.axit`.
- Không chỉnh sửa việc cài đặt Unity MCP/CoplayDev, cấu hình bridge, hoặc cấu hình Codex MCP toàn cục/người dùng.
- Giữ các chỉnh sửa giới hạn trong trạng thái mã nguồn hiện tại; không bao giờ reset/hoàn tác nội dung không liên quan.
- Giữ các thông tin tiến độ/checkpoint ngắn gọn.

---

# Giai đoạn 0 — Mốc cơ bản và độ sẵn sàng thực thi

Ủy quyền một làn mốc cơ bản (baseline) chỉ đọc.

Luôn xác nhận:

- Định hướng trong `.axit/workspace.yaml` và giá trị `safety.check_git_status`;
- Chính sách bộ điều phối trong `AGENTS.md` ở root đang hoạt động;
- `.codex/config.toml` của dự án đang được sử dụng bởi repository đáng tin cậy;
- Cấu hình mặc định về model và lập luận của primary/sub-agent giải quyết đúng cấu hình mong muốn;
- Core v1 ổn định;
- Workspace/System v1 ổn định;
- Capability semantic v1 ổn định;
- `unity-client` ánh xạ tới `src/QuickGun-MVP`;
- Trạng thái Runtime Binding hiện tại;
- Khả năng thực thi multi-agent có sẵn.

Nếu `safety.check_git_status: true`, ủy quyền thêm một bước kiểm tra an toàn Git-status cho các worktree liên quan và chỉ dừng lại khi có nguy cơ xung đột thực sự.

Nếu `safety.check_git_status: false`, bỏ qua hoàn toàn Git status đối với root, các repository lồng nhau và submodule. Số lượng modified/deleted/untracked không phải là bằng chứng sẵn sàng và không phải rào cản.

Chấp nhận:

- Các tầng Axit ổn định không bị thay đổi;
- Khả năng thực thi multi-agent có sẵn;
- Cổng an toàn Git-status đạt yêu cầu khi và chỉ khi được bật rõ ràng.

Nếu chính việc thực thi multi-agent không khả thi, dừng lại với rào cản hành động nhỏ nhất thay vì để luồng chính trực tiếp làm việc.

Tạo checkpoint và tiếp tục.

---

# Giai đoạn 1 — Cổng sẵn sàng thủ công của Unity MCP

Sử dụng một sub-agent kiểm tra độ sẵn sàng của transport chỉ đọc.

Người dùng chịu trách nhiệm cài đặt/cấu hình/khởi chạy MCP cho Unity và mở dự án/editor Unity QuickGun dự kiến trước lần chạy này.

Sub-agent chỉ được kiểm tra trạng thái trực tiếp hiện tại:

- Danh mục công cụ Codex/MCP hiện tại;
- Định danh thực tế của Unity MCP server/adapter;
- Tên các operation trực tiếp chính xác do transport được cấu hình cung cấp;
- Liệu dự án/editor QuickGun dự kiến có thể kết nối tới được không;
- Liệu instance Unity đang hoạt động có phải là dự án dự kiến không;
- Bất kỳ lỗi kết nối/runtime nào quan sát được.

Sub-agent không được:

- Cài đặt hoặc nâng cấp CoplayDev/unity-mcp;
- Chỉnh sửa `Packages/manifest.json` cho việc thiết lập MCP;
- Cài đặt `uv`, Python, hoặc các điều kiện tiên quyết MCP khác;
- Chỉnh sửa cấu hình MCP server ở cấp toàn cục/người dùng/dự án;
- Khởi chạy/cấu hình/sửa chữa cầu nối Unity MCP;
- Đoán endpoint hoặc tên operation dự kiến.

Chấp nhận:

- Một transport Unity MCP thực tế có thể nhìn thấy trong danh mục công cụ trực tiếp;
- Editor/dự án QuickGun dự kiến có thể kết nối tới được;
- Các operation cụ thể thực tế có thể kiểm tra từ giao diện trực tiếp.

Nếu thiếu bất kỳ mục chấp nhận nào, dừng ngay lập tức với:

```text
HARD_BLOCKER: UNITY_MCP_NOT_READY
Observed: <trạng thái thiếu cụ thể ngắn gọn>
Action: Cấu hình/khởi chạy MCP cho Unity thủ công, sau đó chạy lại kế hoạch liên tục.
```

Không cố gắng sửa chữa điều kiện tiên quyết.

Tạo checkpoint và tiếp tục khi được chấp nhận.

---

# Giai đoạn 2 — Khám phá transport thực tế

Sử dụng một sub-agent khám phá transport chỉ đọc mới sau Giai đoạn 1.

Kiểm tra danh mục công cụ/operation MCP trực tiếp và kết nối hiện tại với Unity editor/dự án.

Xác định tên các operation cụ thể chính xác cùng hành vi đầu vào/đầu ra có thể triển khai duy nhất lát cắt semantic ban đầu:

- `unity.prefab.inspect`;
- `unity.serialized-fields.inspect`;
- `unity.playmode.verify`.

Quy tắc:

- Tên các operation phải đến từ giao diện được xác minh trực tiếp;
- Không suy luận operation từ tài liệu nếu phiên bản đã cài đặt hiển thị thông số khác;
- Ghi lại nếu một semantic capability cần nhiều operation MCP theo thứ tự;
- Giữ nguyên `can_establish`, `cannot_establish`, lớp operation, và ranh giới tác động phụ (side-effect) của từng Capability;
- Xác nhận transport đang hướng tới editor/dự án QuickGun, chứ không phải một instance Unity khác.

Chấp nhận:

- Định danh thực tế của adapter/server được xác định;
- Tên các operation chính xác được xác định;
- Khả năng kết nối tới QuickGun editor hiện tại được chứng minh;
- Đủ thông tin để tạo một binding hẹp đã được rà soát.

Nếu một trong ba semantic capability không có operation trực tiếp tương thích, giữ nó ở trạng thái chưa ràng buộc (unbound) và chỉ dừng lại nếu khoảng trống đó làm cho lát cắt dọc (vertical slice) trở nên bất khả thi. Không bao giờ tái cấu hình MCP để cố tạo ra sự hỗ trợ bị thiếu.

Tạo checkpoint và tiếp tục nếu lát cắt dọc vẫn khả thi.

---

# Giai đoạn 3 — Cụ thể hóa Runtime Binding v1

Sử dụng một sub-agent worker để tạo chính xác một binding đã được rà soát tại:

```text
.axit/bindings/unity-client/<verified-binding-id>.yaml
```

Chỉ tạo nó từ các operation đã được xác minh ở Giai đoạn 2.

Ánh xạ ban đầu chỉ có thể bao gồm:

```text
unity.prefab.inspect
unity.serialized-fields.inspect
unity.playmode.verify
```

Không ràng buộc `unity.compile`, `unity.tests.run`, `unity.console.inspect`, `unity.scene.inspect`, `unity.component.inspect`, hoặc `unity.project.inspect` chỉ vì transport hiển thị các công cụ tương tự.

Đặt trạng thái định nghĩa binding thành `validation`.

Cập nhật `.axit/systems/unity-client/capabilities.yaml` chỉ khi cần thiết để tham chiếu đến binding đã rà soát; không mã hóa trạng thái online/offline của runtime thành một thực tế semantic vĩnh viễn.

Yêu cầu một sub-agent verifier riêng biệt kiểm tra:

- Mọi Capability id được ánh xạ đều tồn tại trong tập hợp hoạt động ổn định;
- Mọi tên operation cụ thể đều đã được xác minh trực tiếp;
- Không ánh xạ nào vượt quá ranh giới bằng chứng/tác động phụ của Capability;
- Không bí mật/thông tin đăng nhập tạm thời nào bị commit;
- Các capability chưa ràng buộc vẫn được giữ là unbound rõ ràng.

Chấp nhận:

- Verifier chấp nhận định nghĩa binding cho bước kiểm tra.

Sửa chữa các lỗi định nghĩa binding giới hạn và xác minh lại; tối đa hai vòng lặp.

Tạo checkpoint và tiếp tục.

---

# Giai đoạn 4 — Hồi quy trạng thái thu thập của Runtime Binding

Sử dụng các sub-agent để thực thi ranh giới trạng thái thu thập (acquisition state) của Runtime Binding v1.

Chứng minh ở những nơi khả thi:

- `acquired` — operation được ánh xạ quan sát mục tiêu QuickGun dự kiến và trả về bằng chứng/nguồn gốc đáng tin cậy;
- `unavailable` — trạng thái runtime quan sát được được thể hiện là bằng chứng bị thiếu, chứ không phải lỗi sản phẩm;
- `denied` — sự từ chối của Runtime/Harness tách biệt với lỗi của mục tiêu;
- `transport_error` — lỗi transport trước khi quan sát được mục tiêu đáng tin cậy vẫn được tính là lỗi thu thập.

Không cố tình làm hỏng hoặc tái cấu hình thiết lập MCP của người dùng chỉ để tạo ra các trạng thái unavailable/error. Sử dụng các trạng thái tự nhiên quan sát được hoặc các mô phỏng an toàn giới hạn khi đã đủ.

Một sub-agent verifier kiểm tra xem không có trạng thái thu thập nào trong số này tự hiển thị dưới dạng `PASS`, `FAIL`, hoặc `BLOCKED` mà không có lập luận tiêu chí của `verify-change`.

Chấp nhận:

- Tuyến bằng chứng binding/runtime bảo toàn ngữ nghĩa trạng thái thu thập đủ cho lát cắt dọc đầu tiên.

Tạo checkpoint và tiếp tục.

---

# Giai đoạn 5 — Lát cắt dọc bằng chứng end-to-end đầu tiên

Mục tiêu: chứng minh Axit có thể di chuyển từ tiêu chí được chấp nhận đến semantic Capability, đến bằng chứng transport thực tế, đến kiểm tra độc lập.

Sử dụng pipeline gây sát thương (damage pipeline) và prefab Player đã cấu hình hiện tại của QuickGun.

Các tầng bằng chứng mục tiêu:

1. Các unit test C# gây sát thương định tính hiện tại;
2. Kiểm tra prefab Player cụ thể;
3. Các tham chiếu/giá trị `DamageableBodyPart` được tuần tự hóa liên quan đến tiêu chí;
4. Kịch bản bắn vào đầu trong Play Mode có giới hạn;
5. Kết quả máu (health) runtime quan sát được và đủ định danh mục tiêu để phân biệt hành vi vùng bị trúng đạn dự kiến.

Không chạy các capability không liên quan chỉ vì chúng có sẵn.

Phân chia làn công việc:

- Làn worker chỉ chuẩn bị trạng thái test dự án giới hạn nếu cần chuẩn bị;
- Làn bằng chứng sử dụng binding đã rà soát để thu thập bằng chứng Unity hiện tại;
- Làn verifier độc lập làm theo `verify-change` đối với các tiêu chí được chấp nhận và bằng chứng hiện tại.

Verifier phải độc lập phân loại bằng chứng REQUIRED vs SUPPORTING.

Chấp nhận:

- Binding thực tế tạo ra bằng chứng hiện tại cho các capability được ánh xạ;
- Verifier đưa ra phán quyết chính xác mà không khẳng định quá mức vượt khỏi bằng chứng quan sát được;
- Bằng chứng runtime có thể truy xuất nguồn gốc đến mục tiêu/session QuickGun dự kiến.

Nếu verifier trả về `FAIL`, tiếp tục đến bước phục hồi ở Giai đoạn 6 mà không cần xác nhận định kỳ.

Nếu verifier trả về `BLOCKED` vì chính transport MCP được cấu hình thủ công trở nên không khả dụng, dừng lại với `HARD_BLOCKER: UNITY_MCP_NOT_READY`; không sửa chữa thiết lập MCP.

Tạo checkpoint và tiếp tục.

---

# Giai đoạn 6 — Kiểm tra lỗi/phục hồi trong phạm vi giới hạn

Xác nhận rằng hệ thống multi-agent liên tục có thể phục hồi từ một lỗi dự án bắt buộc đã chứng minh mà không cần người dùng quản lý chi tiết.

Sử dụng một lỗi giới hạn an toàn trong phạm vi lát cắt dọc hiện tại. Ưu tiên một bất đồng bộ cục bộ dự án có thể đảo ngược mà hành vi kỳ vọng đã được chấp nhận, chẳng hạn như bất đồng bộ giá trị/tham chiếu được tuần tự hóa hoặc lỗi hẹp tương tự.

Luồng bắt buộc:

```text
bằng chứng hiện tại
  -> verifier độc lập: FAIL
      -> bộ điều phối phân công worker sửa chữa giới hạn
          -> chỉ sửa chữa lỗi dự án đã được chứng minh
              -> thu thập lại bằng chứng hiện tại
                  -> lượt verifier mới
```

Quy tắc:

- Verifier không được thực hiện việc sửa chữa;
- Worker không được đưa ra phán quyết kiểm tra cuối cùng;
- Không tái sử dụng bằng chứng cũ trước khi sửa chữa để làm bằng chứng xác nhận;
- Tối đa hai vòng lặp sửa chữa/thay thế;
- Không reset/hoàn tác trạng thái mã nguồn hiện tại không liên quan;
- Không dùng giai đoạn phục hồi để thay đổi việc cài đặt/cấu hình MCP.

Chấp nhận:

- Một tuyến `FAIL -> sửa chữa -> thu thập lại -> kiểm tra lại` thực tế hoàn thành chính xác;
- Hoặc một rào cản cứng (hard blocker) đã khai báo được đưa ra.

Tạo checkpoint và tiếp tục.

---

# Giai đoạn 7 — Thăng cấp binding đã rà soát

Chỉ sau khi các Giai đoạn 3–6 cung cấp đủ bằng chứng trực tiếp:

- Cập nhật định nghĩa Runtime Binding đã rà soát từ `validation` thành `active`;
- Ghi lại bằng chứng kiểm tra trực tiếp ngắn gọn trong danh sách/trạng thái phù hợp;
- Giữ khả năng kết nối runtime hiện tại tách biệt với trạng thái binding được quản lý mã nguồn;
- Giữ tất cả các capability Unity chưa được chứng minh ở trạng thái unbound.

Sử dụng một sub-agent verifier để xác nhận các tiêu chí thăng cấp thực sự được đáp ứng.

Chấp nhận:

- Unity Runtime Binding đầu tiên ở trạng thái active chỉ dành cho lát cắt semantic đã được chứng minh.

Tạo checkpoint và tiếp tục.

---

# Giai đoạn 8 — Phân tích khoảng trống đã chứng minh

Sử dụng một sub-agent phân tích chỉ đọc sau khi thăng cấp binding thành công.

Rà soát toàn bộ luồng đã hoàn thành và chỉ xác định các khoảng trống thực sự lặp lại hoặc hạn chế đáng kể việc thực thi.

Các kết quả có thể có:

- Đề xuất ràng buộc thêm một Capability hiện có như `unity.compile` hoặc `unity.console.inspect` vì luồng đã hoàn thành chứng minh nhu cầu cụ thể;
- Đề xuất cập nhật Quy tắc System/Kiến thức vì khoảng trống đó là ngữ cảnh riêng của dự án;
- Đề xuất không mở rộng vì lát cắt hiện tại đã đủ.

Không tự động tạo:

- Core Skill #3;
- Workflow #2;
- Một Profile chuyên gia Unity;
- Thêm các semantic Capability id;
- Các ánh xạ transport bổ sung.

Kết thúc với chính xác một bước tiếp theo nhỏ nhất được đề xuất hoặc `none`.

---

# Đầu ra DONE

Khi kế hoạch hoàn thành, chỉ trả về một tóm tắt ngắn gọn:

```text
Status: DONE | HARD_BLOCKER
Phases completed: ...
Transport: ...
Binding: ...
Vertical-slice verdict: ...
Recovery iterations: ...
Remaining blockers: ...
Next recommended step: ...
```

Không tạo một báo cáo tổng kết dài trừ khi người dùng yêu cầu.
