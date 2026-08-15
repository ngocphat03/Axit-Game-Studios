# .axit — Không gian làm việc Axit chuẩn mực (Canonical Axit Workspace)

`.axit/` là nguồn sự thật (source of truth) do Axit quản lý dành cho các hành vi Core có thể tái sử dụng, điều hướng Workspace/System gốc, các Capability ngữ nghĩa, Runtime Bindings, các registry, kế hoạch thực thi và trạng thái tác vụ tinh gọn.

Dự án vận hành trên nền tảng **Google Antigravity & Gemini**: file gốc `AGENTS.md` và `GEMINI.md` điều hướng ngữ cảnh và quy tắc, `.agents/skills/` hiển thị các Skill để Antigravity tự động phát hiện, và `.agents/` chứa cấu hình rules, hooks, MCP server.

## Bố cục cấu trúc (Layout)

```text
.axit/
├── README.md
├── workspace.yaml
├── core/
│   ├── core.yaml
│   ├── profiles/
│   ├── skills/
│   └── workflows/
├── capabilities/
│   └── <domain>/
│       └── <set>.yaml
├── bindings/
│   └── <system-id>/
│       └── <binding-id>.yaml    # chỉ sau khi transport thực tế được xác minh
├── systems/
│   └── <system-id>/
│       ├── system.yaml
│       ├── rules.md
│       ├── architecture.yaml
│       ├── capabilities.yaml    # điều hướng capability ngữ nghĩa tùy chọn
│       └── knowledge/
├── registry/
│   ├── architecture.yaml
│   └── integrations.yaml
├── plans/
│   └── <bounded-plan>.md
├── state/
│   └── active.md
├── specs/
├── templates/
├── checklists/
└── knowledge/
```

## Trách nhiệm các thành phần

### `workspace.yaml`
Manifest điều hướng gốc. Ánh xạ các thư mục nguồn dưới `src/` tới các System đã đăng ký và trỏ tới Core dùng chung, các registry, và thư mục kiểm thử xác thực workspace.

### `core/`
Hành vi tái sử dụng tinh gọn đã được chứng minh hữu ích trên các workspace/system khác biệt thực tế. Core v1 được đóng băng ở 4 Profiles, 2 Skills, và 1 active Workflow cho đến khi việc sử dụng lặp lại thực tế chứng minh có khoảng trống tái sử dụng khác.

### `capabilities/`
Các thao tác thu thập bằng chứng/thực thi mang tính ngữ nghĩa có thể tái sử dụng. Mã định danh Capability (Capability ids) mô tả ý định như `unity.compile` hoặc `unity.prefab.inspect`; các liên kết provider/công cụ như MCP, CLI, hoặc Axit Unity nằm ngoài định nghĩa capability chuẩn mực.

Ngữ nghĩa Capability v1 ổn định sau khi đã được xác thực qua điều hướng/lựa chọn thực tế. Đầu ra của Capability là bằng chứng, không phải kết luận xác minh (verdict), và việc khai báo một capability không đồng nghĩa với việc cấp quyền thực thi nó.

### `bindings/`
Ánh xạ đã được đánh giá từ các mã Capability ngữ nghĩa tới các thao tác transport cụ thể. Tên của provider/công cụ được phép xuất hiện ở đây vì tầng này cô lập chi tiết transport.

Một định nghĩa binding không chứng minh rằng transport hiện đang được kết nối. Tính khả dụng tại runtime, thông tin xác thực, endpoint và quyền hạn vẫn là mối quan tâm bên ngoài/runtime. Không tạo binding cụ thể cho đến khi giao diện transport thực tế đã được kiểm tra.

### `systems/`
Ngữ cảnh cục bộ theo System cho các thành phần như Unity client, backend, CMS, hoặc services. Rules/Architecture cục bộ nằm ở đây; file sidecar tùy chọn `capabilities.yaml` khai báo tập hợp capability ngữ nghĩa nào liên quan khi cần bằng chứng thực thi/editor. Các phần mở rộng đặc thù theo System giữ nguyên trống cho đến khi công việc lặp lại chứng minh có khoảng trống.

### `registry/`
Chân lý cấp Workspace và liên hệ thống (cross-system). `architecture.yaml` ghi lại ranh giới/quyền sở hữu dùng chung; `integrations.yaml` ánh xạ các provider, consumer, nguồn hợp đồng có thể thực thi và các bài test ranh giới.

### `plans/`
Lộ trình thực thi có giới hạn kết hợp các hành vi Axit đã được chấp thuận qua một đợt chạy dài hơn. Một Plan không tự động là một Core Workflow và không được sao chép lại quy trình Skill/Workflow đã ổn định. Sử dụng Plan cho việc sắp xếp trình tự giai đoạn, giới hạn thực thi phân quyền, các điểm checkpoint và điều kiện dừng khẩn cấp (hard-stop).

### `state/`
Trạng thái tác vụ tinh gọn lưu bằng file cho các phiên làm việc gốc. Đây là một điểm kiểm tra (checkpoint), không phải lịch sử hội thoại.

### `specs/`
Các hợp đồng Axit cho Profiles, Skills, Workflows, điều hướng Workspace/System, Capabilities, Runtime Bindings, và các registry.

### `templates/`
Các file mẫu khởi tạo metadata cho workspace/system mới. Template không phải là chân lý runtime sau khi đã được cụ thể hóa.

### `checklists/`
Ghi chú xác thực và bằng chứng kiểm thử hồi quy ở cấp framework.

### `knowledge/`
Kiến thức Axit có thể tái sử dụng đã được tuyển chọn. Tài liệu tham chiếu đặc thù theo System hoặc workspace thuộc về phạm vi sở hữu hẹp nhất tương ứng.

## Ranh giới Runtime của Antigravity

Thư mục `.agents/` chứa cấu hình của Antigravity như model mặc định, cài đặt sub-agent, rules phân cấp, hooks và client MCP.

Cấu hình đó là mối quan tâm của adapter/runtime. Nó không được định nghĩa lại các Capability id của Axit, ngữ nghĩa xác minh, trách nhiệm của Profile, hoặc kiến trúc sản phẩm.

## Ranh giới Bằng chứng (Evidence boundary)

Mối quan hệ được thiết kế theo luồng:

```text
tiêu chí được chấp thuận (accepted criterion)
    -> Skill/Workflow lựa chọn bằng chứng cần thiết
        -> Capability ngữ nghĩa khi cần thực thi editor/runtime
            -> runtime binding/transport
                -> bằng chứng thu được (acquired evidence)
                    -> đánh giá của verify-change
```

Không mã hóa PASS/FAIL/BLOCKED vào trong định nghĩa Capability. Không biến một Capability thành bắt buộc chỉ vì nó tồn tại.

## Ranh giới Nguồn sự thật (Source-of-truth boundary)

Metadata của Axit nên điều hướng tới chân lý có thể thực thi thực tế thay vì sao chép nó.

Ví dụ những thứ nên nằm ngoài `.axit` khi chúng đã tồn tại sẵn:

- File OpenAPI/protobuf/schema;
- Source code DTO/giao thức dùng chung;
- Database migrations;
- Hợp đồng client được tạo tự động;
- Source code sản phẩm và các bài test.

`.axit` ghi lại quyền sở hữu, mối quan hệ, các ràng buộc, ngữ nghĩa capability và các luồng xác minh.

## Ranh giới Antigravity Skill

```text
.axit/core/skills/<skill>/SKILL.md
        = Core Skill chuẩn mực

.agents/skills/<skill>/SKILL.md
        = Bản chiếu/shim khám phá cho Antigravity
```

Chỉ những Skill đang hoạt động mới được hiển thị để khám phá. Capabilities và Plans không được chiếu dưới dạng Skill.
