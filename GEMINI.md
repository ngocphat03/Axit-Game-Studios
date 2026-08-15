# Axit Workspace — Google Antigravity & Gemini Guide

Không gian làm việc này được cấu hình và tối ưu hóa cho **Google Antigravity (AGY)** sử dụng model **Gemini**.
Dự án áp dụng kiến trúc **Axit Framework** (`.axit/`) kết hợp với hệ thống tùy biến của Antigravity (`.agents/`).

---

## 1. Nguyên tắc Điều phối (Orchestration & Sub-agents)

- **Luồng chính (Primary Thread)**: Giữ vai trò **Orchestrator** — phân rã tác vụ, điều phối các luồng con, giám sát tiến độ, và tổng hợp kết quả.
- **Ủy quyền cho Sub-agents**: Các tác vụ nặng về tìm kiếm mã nguồn, kiểm tra môi trường, chỉnh sửa source code, chạy test/build, và xác minh độc lập đều được ủy quyền cho sub-agents.
- **Chạy song song (Parallelism)**: Khởi tạo song song các tác vụ chỉ đọc (khám phá, phân tích tĩnh, review bằng chứng). Tuần tự hóa các thao tác ghi mã nguồn, thay đổi trạng thái và đột biến Unity/Editor.
- **Độc lập xác minh**: Phân tách rõ ràng giữa khâu triển khai (`implement-change`) và khâu xác minh độc lập (`verify-change`).

---

## 2. Giao thức Thiết kế Cộng tác (Collaborative Design Protocol)

Mọi quyết định thiết kế và kiến trúc đều tuân thủ chu trình 5 bước:

```text
Hỏi (Question) → Lựa chọn (Options) → Quyết định (Decision) → Bản thảo (Draft) → Phê duyệt (Approval)
```

1. **Hỏi trước khi giả định**: Nếu yêu cầu hoặc đặc tả còn điểm mơ hồ, hãy đặt câu hỏi làm rõ thay vì tự ý suy đoán.
2. **Đưa ra các phương án (Options)**: Trình bày từ 2-3 phương án kèm ưu/nhược điểm và phân tích đánh đổi trước khi chốt hướng đi.
3. **Ghi file tăng dần (Incremental Writing)**: Đối với các tài liệu dài (GDD, ADR, Architecture), tạo khung sườn trước, sau đó thảo luận, phê duyệt và ghi từng phần vào đĩa.
4. **Xin phép trước khi ghi**: Luôn hỏi xác nhận trước khi tạo hoặc ghi đè các file quan trọng.

---

## 3. Tiêu chuẩn Kỹ thuật & Unity Development

- **Ngôn ngữ & Engine**: C# / Unity (Unity 6 / Unity 2022+ LTS).
- **Hướng dữ liệu (Data-Driven)**: Các giá trị gameplay, chỉ số cân bằng phải được cấu hình bên ngoài (ScriptableObjects, JSON, config files), tuyệt đối không hardcode.
- **Kiến trúc Decoupled**: Sử dụng Dependency Injection, Events/Signals, tránh lạm dụng Singleton toàn cục.
- **Hiệu năng Hot Path**: Không cấp phát bộ nhớ (zero allocation / no GC allocs) trong các vòng lặp `Update()`, `FixedUpdate()`, hoặc hot paths.
- **Phát triển Hướng xác minh (Verification-Driven Development)**:
  - Viết test trước khi thêm các hệ thống gameplay cốt lõi.
  - Mỗi thay đổi phải có bằng chứng chứng minh hoạt động (Unit test, Integration test, Play Mode verification, hoặc logs kiểm thử).
- **Cam kết Code (Commit Messages)**: Sử dụng định dạng Conventional Commits (`feat:`, `fix:`, `chore:`, `docs:`, `test:`, `refactor:`).

---

## 4. Cấu trúc và Nguồn sự thật (Source of Truth)

- **`.axit/`**: Nguồn sự thật chuẩn mực của Axit cho Core Profiles, Capabilities, Runtime Bindings, System Registries, Plans và Active State.
- **`.agents/`**: Thư mục tùy biến chuẩn của Antigravity:
  - `.agents/skills/`: Các Skill có thể tái sử dụng cho từng quy trình công việc (`SKILL.md`).
  - `.agents/rules/`: Các quy tắc đặc thù theo đường dẫn file.
  - `.agents/hooks.json`: Cấu hình lifecycle hooks.
  - `.agents/mcp_config.json`: Cấu hình tích hợp Unity MCP server.
- **`src/`**: Mã nguồn các hệ thống (Unity Client, Backend, Services, v.v.).
- **`design/`**: Tài liệu thiết kế game (GDD, cốt truyện, màn chơi, cân bằng).
- **`docs/`**: Tài liệu kiến trúc kỹ thuật (ADRs, Master Architecture, TR Registry).

---

## 5. Kết nối Unity MCP

- Nếu tác vụ yêu cầu thu thập bằng chứng từ Unity Editor/PlayMode, sử dụng cấu hình transport Unity MCP đã được thiết lập.
- Không tự ý can thiệp cài đặt hoặc sửa đổi bridge MCP nếu transport bị mất kết nối; hãy báo cáo trạng thái `UNITY_MCP_NOT_READY` để người dùng kiểm tra.
