# Axit Workspace — Google Antigravity & Gemini Guide

Không gian làm việc này được cấu hình và tối ưu hóa chuyên biệt cho **Google Antigravity (AGY)** sử dụng model **Gemini**.
Dự án áp dụng kiến trúc **Axit Framework** (`.axit/`) kết hợp với hệ thống tùy biến của Antigravity (`.agents/`).

---

## 1. Nguyên tắc Điều phối (Orchestration & Sub-agents)

- **Luồng chính (Primary Thread / Orchestrator)**: Giữ vai trò **Orchestrator duy nhất** — phân rã tác vụ, định tuyến hệ thống, điều phối các luồng con (sub-agents), giám sát tiến độ, và tổng hợp kết quả.
- **Ủy quyền cho Sub-agents**: Các tác vụ nặng về tìm kiếm mã nguồn, kiểm tra môi trường, chỉnh sửa source code, chạy test/build, và xác minh độc lập đều được ủy quyền cho sub-agents.
- **Chạy song song (Parallelism)**: Khởi tạo song song các tác vụ chỉ đọc (khám phá, phân tích tĩnh, review bằng chứng). Tuần tự hóa các thao tác ghi mã nguồn, thay đổi trạng thái và đột biến Unity/Editor.
- **Độc lập xác minh**: Phân tách rõ ràng giữa khâu triển khai (`implement-change`) và khâu xác minh độc lập (`verify-change`). Luồng điều phối không tự ý thay thế phán quyết của verifier độc lập.

### Định tuyến Model và Chi phí
- **Orchestrator chính**: `gemini-2.5-pro` (mức suy luận cao).
- **Tất cả các luồng con (Child lanes)**: `gemini-2.5-flash` (mức suy luận trung bình) hoặc `gemini-2.5-pro` cho các tác vụ kiểm chứng logic phức tạp.
- Luồng con không được tự ý leo thang model. Khi gặp lỗi, hãy tinh lọc ngữ cảnh, hướng dẫn lại hoặc thay thế luồng con mới.

### Xử lý Sự cố & Phục hồi Sub-agent
Nếu sub-agent bị tạm dừng, timeout hoặc trả về kết quả chưa hoàn chỉnh:
1. Ghi nhận kết quả hữu ích gần nhất, blocker, các file đã sửa và trạng thái workspace.
2. Hướng dẫn lại hoặc tiếp tục sub-agent đó với chỉ dẫn tập trung nếu an toàn.
3. Nếu vẫn nghẽn, đóng và thay thế bằng sub-agent mới với ngữ cảnh tinh lọc. Cho phép tối đa 2 lần thử phục hồi/thay thế.
4. Thu thập lại bằng chứng hiện tại và lưu checkpoint vào `.axit/state/active.md`.

### Điều kiện Dừng Khẩn cấp (Hard-Stop Conditions)
Chỉ dừng lại và yêu cầu người dùng can thiệp khi:
- `UNITY_MCP_NOT_READY`: Transport Unity MCP bị thiếu, mất kết nối hoặc không truy cập được editor.
- Ý định thiết kế sản phẩm hoặc tiêu chí chấp nhận bị mơ hồ mà không thể tự giải quyết từ tài liệu dự án.
- Cần quyết định lớn về kiến trúc, hợp đồng công khai hoặc quyền sở hữu trạng thái nằm ngoài kế hoạch đã duyệt.
- Yêu cầu thao tác xóa/migration mang tính phá hủy hoặc refactor diện rộng không liên quan.
- Yêu cầu quyền admin/sudo hoặc can thiệp máy ngoài workspace.
- Thất bại liên tục sau 2 vòng phục hồi/thay thế sub-agent.

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
- **Hướng dữ liệu (Data-Driven)**: Các giá trị gameplay, chỉ số cân bằng phải được cấu hình bên ngoài (`ScriptableObject`, JSON, config files), tuyệt đối không hardcode.
- **Kiến trúc Decoupled**: Sử dụng Dependency Injection, Events/Signals, tránh lạm dụng Singleton toàn cục cho trạng thái game.
- **Hiệu năng Hot Path**: Không cấp phát bộ nhớ (zero allocation / no GC allocs) trong các vòng lặp `Update()`, `FixedUpdate()`, hoặc hot paths.
- **Phát triển Hướng xác minh (Verification-Driven Development)**:
  - Viết test trước khi thêm các hệ thống gameplay cốt lõi.
  - Mỗi thay đổi phải có bằng chứng chứng minh hoạt động (Unit test, Integration test, Play Mode verification, hoặc logs kiểm thử).
- **Cam kết Code (Commit Messages)**: Sử dụng định dạng Conventional Commits (`feat:`, `fix:`, `chore:`, `docs:`, `test:`, `refactor:`).

---

## 4. Cấu trúc và Nguồn sự thật (Source of Truth)

- **`.axit/`**: Nguồn sự thật chuẩn mực của Axit cho Core Profiles, Capabilities, Runtime Bindings, System Registries, Plans và Active State.
  - Khi tác vụ chạm vào source code dưới `src/`, đọc `.axit/workspace.yaml` để ánh xạ System liên quan (`.axit/systems/<system-id>/`).
  - Khi thay đổi vượt ranh giới hệ thống, tham chiếu `.axit/registry/architecture.yaml` và `integrations.yaml`.
- **`.agents/`**: Thư mục tùy biến chuẩn của Antigravity:
  - `.agents/skills/`: 73 Skill có thể tái sử dụng cho từng quy trình công việc (`SKILL.md`).
  - `.agents/rules/`: Các quy tắc phân cấp theo đường dẫn file (`gameplay-code.md`, `engine-code.md`, `ui-code.md`, `test-standards.md`, `design-docs.md`).
  - `.agents/hooks.json`: Cấu hình lifecycle hooks.
  - `.agents/mcp_config.json`: Cấu hình tích hợp Unity MCP server.
- **`src/`**: Mã nguồn các hệ thống (Unity Client `QuickGun-MVP`, Backend, Services).
- **`design/`**: Tài liệu thiết kế game (GDD, cốt truyện, màn chơi, cân bằng).
- **`docs/`**: Tài liệu kiến trúc kỹ thuật (ADRs, Master Architecture, `docs/reference/`).

---

## 5. Chính sách An toàn Git Worktree (Git Worktree Safety)

- Đọc `.axit/workspace.yaml -> safety.check_git_status` (mặc định: `false`).
- Khi là `false`, không sử dụng trạng thái `git status` của root hay submodule làm cổng chặn (readiness blocker); xem trạng thái filesystem hiện tại là đường cơ sở làm việc và tiếp tục thực hiện thay đổi trong phạm vi tác vụ.
- Tuyệt đối không tự ý `git reset`, `git clean` hoặc ghi đè mù mờ lên các thay đổi ngoài phạm vi.

---

## 6. Kết nối Unity MCP

- Nếu tác vụ yêu cầu thu thập bằng chứng từ Unity Editor/PlayMode, sử dụng cấu hình transport Unity MCP trong `.agents/mcp_config.json`.
- Không tự ý can thiệp cài đặt hoặc sửa đổi bridge MCP nếu transport bị mất kết nối; hãy báo cáo trạng thái `UNITY_MCP_NOT_READY` để người dùng kiểm tra.
