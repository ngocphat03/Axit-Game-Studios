# Đặc tả Axit Workspace và System v1 (Axit Workspace and System Spec v1)

## Mục đích (Purpose)

Axit coi thư mục gốc của repository là **product workspace** khi nhiều thành phần tương tác cùng tồn tại trong một repository và Antigravity thông thường được mở từ thư mục gốc đó.

Workspace có thể chứa game client, backend, CMS, workers, công cụ, hoặc các services khác dưới `src/`. Đây là các **Systems**, không phải là các dự án Axit riêng biệt theo mặc định.

Mô hình phân tầng:

```text
Axit Core
  -> Workspace
      -> Systems
          -> Source + executable contracts + tests
```

Bố cục này cho phép Antigravity truy vết hành vi xuyên suốt các ranh giới thực tế giữa provider và consumer thay vì suy luận từ một cây mã nguồn cô lập.

## Bố cục chuẩn mực (Canonical layout)

```text
AGENTS.md

.agents/
└── skills/
    └── ... active Antigravity discovery entries

.axit/
├── workspace.yaml
├── core/
├── systems/
│   └── <system-id>/
│       ├── system.yaml
│       ├── rules.md
│       ├── architecture.yaml
│       └── knowledge/
├── registry/
│   ├── architecture.yaml
│   └── integrations.yaml
├── state/
│   └── active.md
├── specs/
├── templates/
└── checklists/

src/
├── GameClient/
├── Backend/
├── CMS/
└── Services/

tests/
├── contracts/
├── integration/
└── e2e/
```

Chỉ những thư mục thực sự cần thiết mới nên được tạo ra. Việc để danh mục mở rộng của system trống được ưu tiên hơn là tạo ra các agent/skill mang tính suy đoán.

## Điều hướng Antigravity từ thư mục gốc (Root-first Antigravity routing)

`AGENTS.md` là bộ điều hướng gốc tinh gọn.

Khi một tác vụ nhắc đến hoặc chạm vào một đường dẫn mã nguồn, Antigravity nên:

1. đọc `.axit/workspace.yaml`;
2. ánh xạ đường dẫn nguồn bị ảnh hưởng tới một hoặc nhiều System đã đăng ký;
3. chỉ đọc các manifest/rules/architecture của system liên quan;
4. đọc các registry kiến trúc/tích hợp gốc khi tác vụ vượt qua ranh giới giữa các system;
5. chỉ nạp Core Profiles/Skills/Workflows khi trách nhiệm/quy trình của chúng thực sự cần thiết.

Không phụ thuộc vào việc thay đổi thư mục làm việc (working directory) của Antigravity để kích hoạt ngữ cảnh dự án lồng nhau.

Không nạp trước đệ quy toàn bộ thư mục `.axit/`.

## Manifest của Workspace

File chuẩn mực:

```text
.axit/workspace.yaml
```

Manifest của workspace là ngữ cảnh điều hướng, không phải là tài liệu yêu cầu sản phẩm hay nơi xả dữ liệu kiến trúc.

Nó nên xác định:

- id/tên/mô tả tóm tắt của workspace;
- thư mục gốc chứa mã nguồn;
- Axit Core dùng chung;
- các registry của workspace và trạng thái đang hoạt động (active state);
- các System đã đăng ký và đường dẫn source/context của chúng;
- các phần mở rộng cấp workspace tùy chọn;
- các thư mục kiểm thử xác thực hợp đồng/tích hợp/e2e cấp repository.

Một System không nên được đăng ký cho đến khi thư mục nguồn hoặc ranh giới dự kiến của nó được biết rõ ràng.

## Manifest của System

File chuẩn mực:

```text
.axit/systems/<system-id>/system.yaml
```

Một System là một ranh giới runtime/ứng dụng/công cụ nhất quán bên trong product workspace, ví dụ:

- Unity game client;
- backend API;
- ứng dụng CMS/admin;
- matchmaking service;
- worker/processor;
- giao thức/package dùng chung khi nó sở hữu hành vi có thể thực thi.

Manifest của System nên xác định:

- id/tên/loại ổn định của system;
- thư mục gốc chứa mã nguồn;
- runtime/framework khi hữu ích;
- đường dẫn rules, architecture, và knowledge cục bộ;
- các interface nó cung cấp/sử dụng khi đã biết;
- các luồng xác thực (validation routes);
- các phần mở rộng đặc thù của system đã được đánh giá;
- các thông tin hiện chưa được xác nhận có chủ đích.

Không đưa toàn bộ tài liệu API hoặc tài liệu tham chiếu framework khổng lồ vào `system.yaml`.

## Quyền sở hữu giữa Workspace và System

Sử dụng phạm vi sở hữu hẹp nhất và chính xác nhất.

Ví dụ cục bộ theo System:

- thứ tự xử lý sát thương trong Unity;
- quy tắc phụ thuộc module backend;
- quy ước biểu mẫu CMS;
- chính sách thử lại đặc thù theo service.

Ví dụ cấp Workspace / liên hệ thống:

- backend là bên nắm quyền sở hữu chính thức trạng thái kho đồ;
- Unity và CMS cùng sử dụng chung PlayerProfile API;
- CMS chỉ ghi vào kho đồ thông qua admin API;
- phiên bản giao thức dùng chung phải duy trì tính tương thích giữa providers và consumers.

Không nâng một quyết định cục bộ lên thành kiến trúc workspace chỉ vì nó quan trọng bên trong một System đơn lẻ.

## Registry kiến trúc Workspace

File chuẩn mực:

```text
.axit/registry/architecture.yaml
```

Chỉ ghi lại các chân lý kiến trúc dùng chung / rủi ro cao như:

- quyền sở hữu trạng thái chính thức xuyên suốt các System;
- hướng phụ thuộc liên hệ thống;
- các quyết định ranh giới công khai;
- cấu trúc persistence/mạng dùng chung bởi nhiều System;
- ràng buộc hiệu năng/bảo mật cấp workspace;
- các mẫu thiết kế liên hệ thống bị cấm.

Kiến trúc cục bộ của System thuộc về:

```text
.axit/systems/<system-id>/architecture.yaml
```

## Registry tích hợp (Integration registry)

File chuẩn mực:

```text
.axit/registry/integrations.yaml
```

Registry tích hợp ánh xạ các mối quan hệ provider/consumer thực tế.

Một mục tích hợp hữu ích có thể chứa:

```yaml
id: player-profile
provider: backend
consumers:
  - unity-client
  - cms
contract:
  type: openapi
  source: src/Backend/openapi.json
tests:
  - tests/contracts/player-profile
```

Registry trỏ tới chân lý có thể thực thi; nó không phải là một nguồn schema khác.

## Quy tắc Nguồn sự thật của hợp đồng (Contract source-of-truth rule)

Tuyệt đối không sao chép trùng lặp một hợp đồng liên hệ thống có thể thực thi vào trong `.axit` khi nguồn thực tế đã tồn tại.

Ví dụ về chân lý có thể thực thi:

- Tài liệu OpenAPI;
- File protobuf/schema;
- Hợp đồng client được tạo tự động;
- Code giao thức dùng chung;
- Database migrations/schema;
- Dữ liệu mẫu serializer hoặc snapshot tính tương thích khi chúng là chuẩn mực.

`.axit` ghi lại **hợp đồng nằm ở đâu, ai sở hữu nó, ai sử dụng nó, và nó được xác minh như thế nào**.

Điều này ngăn tài liệu AI lỗi thời trở thành một định nghĩa API thứ hai không tương thích.

## Suy luận liên hệ thống (Cross-system reasoning)

Đối với một lỗi như lỗi deserialize trong Unity, luồng suy luận kỳ vọng là:

```text
lỗi phía consumer
  -> code/model phía consumer
  -> integration registry
  -> hợp đồng provider có thể thực thi
  -> triển khai phía provider
  -> các consumer khác khi liên quan
  -> tests hợp đồng/tích hợp
```

Không vội giả định bên consumer hay bên provider sai trước khi so sánh cả hai phía với hợp đồng đã được chấp thuận.

## Xác thực liên hệ thống (Cross-system validation)

Các bài test cấp repository nên nằm ngoài một System khi chúng xác thực ranh giới dùng chung giữa nhiều System.

Các thư mục điển hình:

```text
tests/contracts/
tests/integration/
tests/e2e/
```

Ví dụ:

- phản hồi backend tuân thủ OpenAPI và Unity deserialize thành công;
- CMS và Unity cùng sử dụng các giá trị enum tương thích;
- migration API duy trì tính tương thích với các client cũ khi cần;
- backend + worker + database tạo ra workflow quan sát được như mong đợi.

`verify-change` vẫn quyết định liệu một kiểm tra cụ thể là REQUIRED hay SUPPORTING cho tiêu chí đã chấp thuận.

## Mối quan hệ với Core

Axit Core đã đánh giá duy trì tính độc lập với workspace.

Các System có thể bổ sung Rules/Knowledge cục bộ và chỉ thêm các Skill/Workflow/Profile đặc thù của system khi đã được chứng minh cần thiết.

Ưu tiên theo thứ tự:

```text
Rule / Registry fact
  -> Knowledge
  -> System Skill
  -> System Workflow
  -> System Profile
```

Chỉ tạo một Profile mới cho một khoảng trống trách nhiệm bền vững, không phải vì một System tương ứng với một chức danh công việc truyền thống.

## Trạng thái đang hoạt động (Active state)

File chuẩn mực:

```text
.axit/state/active.md
```

Giữ trạng thái có thể tiếp tục ở cấp workspace luôn tinh gọn:

- tác vụ có giới hạn hiện tại;
- các System bị ảnh hưởng;
- các quyết định đã được chấp thuận;
- trạng thái triển khai/xác minh hiện tại;
- các điểm nghẽn và hành động tiếp theo.

Các thông tin bền vững đặc thù của system thuộc về system architecture/rules/knowledge, không nằm trong checkpoint của tác vụ.

## Thứ tự ưu tiên (Precedence)

Đối với một tác vụ có giới hạn, diễn giải các nguồn theo thứ tự sau:

```text
yêu cầu người dùng / phạm vi được chấp thuận
  -> source/contracts có thể thực thi và registries workspace/system đã chấp thuận
  -> Rules áp dụng và chuyên môn hóa đang hoạt động
  -> Trách nhiệm/quy trình của Axit Core
  -> Các lựa chọn triển khai cục bộ
```

Nếu chân lý trong mã nguồn có thể thực thi và registry không khớp nhau, hãy báo cáo sự khác biệt thay vì âm thầm tin tưởng metadata đã lỗi thời.
