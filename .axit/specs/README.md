# Đặc tả Axit (Axit Specs)

Thư mục này chứa các hợp đồng định dạng tinh gọn cho các sản phẩm do Axit sở hữu như Profiles, Skills, Workflows, điều hướng Workspace/System, Capabilities, Runtime Bindings, và các registry.

Các đặc tả chỉ được thêm vào khi một sản phẩm đã được đánh giá hoặc giai đoạn thực tế áp dụng hợp đồng đó.

Các đặc tả hiện tại:

- [`Profile Spec v1`](profile-spec-v1.md) — lăng kính trách nhiệm/quyết định được sử dụng bởi Core và các phần mở rộng tập trung trong tương lai.
- [`Skill Spec v1`](skill-spec-v1.md) — một quy trình tái sử dụng tập trung, được thể hiện dưới dạng file `SKILL.md` tương thích với Codex.
- [`Workflow Spec v1`](workflow-spec-v1.md) — hợp đồng kết hợp và chuyển tiếp cho các Skill/trách nhiệm đã được đánh giá hướng tới một kết quả có giới hạn.
- [`Workspace and System Spec v1`](workspace-system-spec-v1.md) — điều hướng từ thư mục gốc cho một product workspace chứa các System tương tác nhau (game client/backend/CMS/service), các registry liên hệ thống, các hợp đồng có thể thực thi và bài test ranh giới.
- [`Capability Spec v1`](capability-spec-v1.md) — các thao tác bằng chứng/thực thi mang tính ngữ nghĩa trung lập với transport ổn định, nằm dưới Skills/Workflows và nằm trên runtime bindings.
- [`Runtime Binding Spec v1`](runtime-binding-spec-v1.md) — ánh xạ các mã Capability ngữ nghĩa tới các thao tác transport cụ thể đã được xác minh trong khi vẫn giữ tách biệt tính khả dụng tại runtime, quyền hạn và ngữ nghĩa kết luận.

Quy tắc:

- Tính tương thích với Codex không được phép chi phối toàn bộ kiến trúc Axit.
- Các Core/system/workspace Skill đang hoạt động phải duy trì tính hợp lệ của file `SKILL.md` chuẩn Codex khi được hiển thị để khám phá.
- Workflows vẫn là sản phẩm do Axit sở hữu và không được chiếu vào `.agents/skills/`.
- Capabilities không phải là Skill và không được chiếu vào `.agents/skills/` chỉ để phục vụ khám phá.
- Mã định danh Capability phải mô tả ý đồ ngữ nghĩa thay vì tên lệnh của MCP/CLI/provider.
- Các file Runtime Binding có thể chứa tên thao tác đặc thù của provider/công cụ vì chúng là tầng cô lập chi tiết transport.
- Tuyệt đối không tự ý bịa ra một binding hoặc tên thao tác transport cụ thể mà chưa kiểm tra giao diện transport thực tế.
- Trạng thái định nghĩa binding không đồng nghĩa với tính khả dụng tại runtime hiện tại; tính khả dụng được phân giải theo từng môi trường/lần chạy.
- Đầu ra của Capability là bằng chứng; `verify-change` giữ quyền phân loại REQUIRED/SUPPORTING và quyền đưa ra kết luận cuối cùng.
- Việc khai báo một Capability hoặc Binding không cấp quyền Runtime/Harness.
- Giữ thông tin xác thực, secrets, và trạng thái kết nối máy tạm thời nằm ngoài các file `.axit` chuẩn mực.
- Tránh các trường schema suy đoán chưa được sử dụng bởi một sản phẩm hoặc giai đoạn thực tế.
- Giữ cấu hình provider/runtime nằm ngoài các định nghĩa Profile và Capability chuẩn mực của Axit.
- Giữ Skill tập trung vào quy trình; chuyển trách nhiệm bao quát, kiến thức, quy tắc, kiến trúc và sự kết hợp workflow về các tầng sở hữu tương ứng.
- Coi thư mục gốc repository là workspace khi Codex chạy từ gốc và các System tương tác dùng chung repository.
- Không sao chép trùng lặp các hợp đồng API/schema có thể thực thi vào trong `.axit`; hãy điều hướng tới nguồn thực sự của chúng.
