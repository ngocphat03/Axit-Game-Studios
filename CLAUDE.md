# Claude Code Game Studios -- Kiến trúc Game Studio Agent

Quy trình phát triển game indie được quản lý thông qua 49 Claude Code subagents phối hợp nhịp nhàng.
Mỗi agent nắm giữ một lĩnh vực cụ thể, đảm bảo sự phân tách trách nhiệm và kiểm soát chất lượng.

## Technology Stack

- **Engine**: [CHOOSE: Godot 4 / Unity / Unreal Engine 5]
- **Language**: [CHOOSE: GDScript / C# / C++ / Blueprint]
- **Version Control**: Git with trunk-based development
- **Build System**: [SPECIFY sau khi chọn engine]
- **Asset Pipeline**: [SPECIFY sau khi chọn engine]

> **Lưu ý**: Các agent chuyên trách engine (Engine-specialist) đã có sẵn cho Godot, Unity, và Unreal cùng với các sub-specialists chuyên sâu. Hãy sử dụng bộ agent phù hợp với engine của bạn.

## Cấu trúc dự án (Project Structure)

@.claude/docs/directory-structure.md

## Tham chiếu phiên bản Engine (Engine Version Reference)

@docs/engine-reference/godot/VERSION.md

## Tùy chọn kỹ thuật (Technical Preferences)

@.claude/docs/technical-preferences.md

## Quy tắc điều phối (Coordination Rules)

@.claude/docs/coordination-rules.md

## Giao thức cộng tác (Collaboration Protocol)

**Cộng tác định hướng bởi người dùng, không tự ý thực thi (User-driven collaboration, not autonomous execution).**
Mọi nhiệm vụ đều tuân theo: **Hỏi (Question) -> Đưa lựa chọn (Options) -> Quyết định (Decision) -> Bản thảo (Draft) -> Phê duyệt (Approval)**

- Agent BẮT BUỘC phải hỏi "Tôi có thể ghi nội dung này vào [filepath] không?" trước khi sử dụng công cụ Write/Edit
- Agent BẮT BUỘC phải trình bày bản thảo hoặc tóm tắt trước khi yêu cầu phê duyệt
- Thay đổi nhiều file đòi hỏi sự phê duyệt rõ ràng cho toàn bộ tập thay đổi (changeset)
- Không commit nếu không có hướng dẫn từ người dùng

Xem `docs/COLLABORATIVE-DESIGN-PRINCIPLE.md` để biết toàn bộ giao thức và các ví dụ chi tiết.

> **Phiên làm việc đầu tiên?** Nếu dự án chưa cấu hình engine và chưa có concept game,
> hãy chạy `/start` để bắt đầu quy trình onboarding có hướng dẫn.

## Tiêu chuẩn viết code (Coding Standards)

@.claude/docs/coding-standards.md

## Quản lý ngữ cảnh (Context Management)

@.claude/docs/context-management.md
