# Danh sách Agent (Agent Roster)

Các agent sau đây luôn sẵn sàng hoạt động. Mỗi agent có một file định nghĩa riêng trong `.claude/agents/`. Hãy sử dụng agent phù hợp nhất với nhiệm vụ cụ thể. Khi một tác vụ trải rộng qua nhiều lĩnh vực, agent điều phối (thường là `producer` hoặc lead của lĩnh vực đó) nên ủy quyền cho các chuyên viên (specialists).

## Phân tầng 1 -- Lãnh đạo cấp cao (Leadership Agents - Opus)
| Agent | Lĩnh vực | Khi nào nên sử dụng |
|---|---|---|
| `creative-director` | Tầm nhìn cấp cao | Các quyết định sáng tạo lớn, xung đột trụ cột thiết kế, định hướng tông điệu game |
| `technical-director` | Tầm nhìn kỹ thuật | Quyết định kiến trúc, lựa chọn tech stack, chiến lược tối ưu hiệu năng |
| `producer` | Quản lý sản xuất | Lập kế hoạch sprint, theo dõi milestone, quản lý rủi ro, điều phối công việc |

## Phân tầng 2 -- Trưởng bộ phận (Department Lead Agents - Sonnet)
| Agent | Lĩnh vực | Khi nào nên sử dụng |
|---|---|---|
| `game-designer` | Thiết kế game | Cơ chế gameplay, hệ thống, độ tiến trình (progression), kinh tế, cân bằng game |
| `lead-programmer` | Kiến trúc mã nguồn | Thiết kế hệ thống code, code review, thiết kế API, refactoring |
| `art-director` | Định hướng mỹ thuật | Style guides, art bible, tiêu chuẩn asset, định hướng UI/UX |
| `audio-director` | Định hướng âm thanh | Định hướng âm nhạc, bảng màu âm thanh (sound palette), chiến lược tích hợp audio |
| `narrative-director` | Cốt truyện và lời thoại | Tuyến truyện (story arcs), xây dựng thế giới (world-building), thiết kế nhân vật |
| `qa-lead` | Đảm bảo chất lượng | Chiến lược kiểm thử, phân loại bug, đánh giá sẵn sàng phát hành, kế hoạch kiểm thử hồi quy |
| `release-manager` | Pipeline phát hành | Quản lý bản build, phân phiên bản (versioning), changelog, triển khai và rollback |
| `localization-lead` | Quốc tế hóa (i18n) | Tách chuỗi ngôn ngữ (string externalization), pipeline dịch thuật, kiểm thử bản địa hóa |

## Phân tầng 3 -- Chuyên viên (Specialist Agents - Sonnet hoặc Haiku)
| Agent | Lĩnh vực | Model | Khi nào nên sử dụng |
|---|---|---|---|
| `systems-designer` | Thiết kế hệ thống | Sonnet | Triển khai cơ chế cụ thể, thiết kế công thức số học, vòng lặp gameplay |
| `level-designer` | Thiết kế màn chơi | Sonnet | Bố cục màn chơi, nhịp độ (pacing), thiết kế màn chạm trán (encounter), luồng di chuyển |
| `economy-designer` | Kinh tế/Cân bằng | Sonnet | Nền kinh tế tài nguyên, bảng tỉ lệ rơi đồ (loot tables), đường cong tiến trình |
| `gameplay-programmer` | Lập trình gameplay | Sonnet | Triển khai tính năng game, viết code các hệ thống gameplay |
| `engine-programmer` | Hệ thống engine | Sonnet | Core engine, rendering, vật lý, quản lý bộ nhớ |
| `ai-programmer` | Hệ thống AI | Sonnet | Behavior trees, tìm đường (pathfinding), logic NPC, máy trạng thái (state machines) |
| `network-programmer` | Mạng và Netcode | Sonnet | Netcode, replication, bù trễ (lag compensation), matchmaking |
| `tools-programmer` | Công cụ phát triển | Sonnet | Extension editor, công cụ pipeline, tiện ích debug |
| `ui-programmer` | Lập trình UI | Sonnet | UI framework, màn hình, widgets, liên kết dữ liệu (data binding) |
| `technical-artist` | Mỹ thuật kỹ thuật | Sonnet | Shaders, VFX, tối ưu hóa đồ họa, công cụ art pipeline |
| `sound-designer` | Thiết kế âm thanh | Sonnet | Tài liệu thiết kế SFX, danh sách sự kiện audio, ghi chú cân chỉnh âm lượng |
| `writer` | Lời thoại và Lore | Sonnet | Viết lời thoại, mục lore/cốt truyện, mô tả vật phẩm |
| `world-builder` | Thiết kế thế giới | Sonnet | Quy tắc thế giới, thiết kế phe phái (factions), lịch sử, địa lý |
| `qa-tester` | Thực thi kiểm thử | Haiku | Viết test cases, báo cáo bug, checklist kiểm thử |
| `performance-analyst` | Hiệu năng | Sonnet | Đo đạc hiệu năng (profiling), khuyến nghị tối ưu hóa, phân tích bộ nhớ |
| `devops-engineer` | Build/Triển khai | Haiku | CI/CD, script tạo bản build, quy trình quản lý phiên bản |
| `analytics-engineer` | Dữ liệu viễn thấu | Sonnet | Theo dõi sự kiện (event tracking), dashboards, thiết kế A/B testing |
| `ux-designer` | Luồng trải nghiệm UX | Sonnet | Luồng người dùng, wireframes, accessibility, xử lý điều khiển input |
| `prototyper` | Làm mẫu nhanh | Sonnet | Tạo prototype dùng một lần, thử nghiệm cơ chế, kiểm chứng tính khả thi |
| `security-engineer` | Bảo mật | Sonnet | Chống hack/cheat, ngăn chặn khai thác lỗi, mã hóa file save, bảo mật mạng |
| `accessibility-specialist` | Khả năng tiếp cận | Haiku | Tuân thủ WCAG, chế độ mù màu, gán lại phím (remapping), phóng to chữ |
| `live-ops-designer` | Vận hành trực tuyến | Sonnet | Mùa giải (seasons), sự kiện, battle passes, giữ chân người chơi (retention) |
| `community-manager` | Cộng đồng | Haiku | Patch notes, phản hồi người chơi, xử lý khủng hoảng truyền thông |

## Các Agent đặc thù theo Engine (sử dụng bộ tương ứng với engine của bạn)

### Trưởng bộ phận Engine (Engine Leads)

| Agent | Engine | Model | Khi nào nên sử dụng |
|---|---|---|---|
| `unreal-specialist` | Unreal Engine 5 | Sonnet | So sánh Blueprint vs C++, tổng quan GAS, UE subsystems, tối ưu Unreal |
| `unity-specialist` | Unity | Sonnet | So sánh MonoBehaviour vs DOTS, Addressables, URP/HDRP, tối ưu Unity |
| `godot-specialist` | Godot 4 | Sonnet | Mẫu thiết kế GDScript, kiến trúc node/scene, signals, tối ưu Godot |

### Chuyên viên phụ trách hệ thống con Unreal Engine

| Agent | Hệ thống con | Model | Khi nào nên sử dụng |
|---|---|---|---|
| `ue-gas-specialist` | Gameplay Ability System | Sonnet | Abilities, gameplay effects, attribute sets, tags, đoán trước (prediction) |
| `ue-blueprint-specialist` | Kiến trúc Blueprint | Sonnet | Ranh giới BP/C++, chuẩn đồ thị node, đặt tên, tối ưu Blueprint |
| `ue-replication-specialist` | Mạng / Đồng bộ | Sonnet | Đồng bộ thuộc tính, RPCs, đoán trước, mức độ phù hợp (relevancy), băng thông |
| `ue-umg-specialist` | UMG / CommonUI | Sonnet | Cây phân cấp Widget, data binding, CommonUI input, hiệu năng UI |

### Chuyên viên phụ trách hệ thống con Unity

| Agent | Hệ thống con | Model | Khi nào nên sử dụng |
|---|---|---|---|
| `unity-dots-specialist` | DOTS / ECS | Sonnet | Entity Component System, C# Jobs, trình biên dịch Burst, hybrid renderer |
| `unity-shader-specialist` | Shaders / VFX | Sonnet | Shader Graph, VFX Graph, tùy biến URP/HDRP, hậu kỳ (post-processing) |
| `unity-addressables-specialist` | Quản lý Asset | Sonnet | Nhóm Addressable, tải bất đồng bộ (async loading), bộ nhớ, phân phối nội dung |
| `unity-ui-specialist` | UI Toolkit / UGUI | Sonnet | UI Toolkit, UXML/USS, UGUI Canvas, data binding, input đa nền tảng |

### Chuyên viên phụ trách hệ thống con Godot

| Agent | Hệ thống con | Model | Khi nào nên sử dụng |
|---|---|---|---|
| `godot-gdscript-specialist` | GDScript | Sonnet | Static typing, design patterns, signals, coroutines, hiệu năng GDScript |
| `godot-csharp-specialist` | C# / .NET | Sonnet | Mẫu thiết kế .NET, delegates [Signal], async, nullable types, truy cập node an toàn kiểu |
| `godot-shader-specialist` | Shaders / Rendering | Sonnet | Ngôn ngữ shader Godot, visual shaders, particles, hiệu ứng hậu kỳ |
| `godot-gdextension-specialist` | GDExtension | Sonnet | Liên kết C++/Rust, hiệu năng native, tùy biến custom nodes, hệ thống build |
