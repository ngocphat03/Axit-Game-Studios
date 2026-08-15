# Đóng góp cho Claude Code Game Studios

CCGS là một framework điều phối phục vụ phát triển game indie sử dụng Claude Code.
Chúng tôi luôn chào đón các đóng góp — sửa lỗi (bug fixes), các skill mới giải quyết khoảng trống thực tế, cải tiến agent và sửa hook. Các PR không phù hợp với định hướng của framework sẽ bị đóng mà không cần giải thích dài dòng.

## Thế nào là một PR tốt

- **Sửa lỗi (Bug fixes)** — cái gì đó bị lỗi, đây là bản sửa lỗi
- **Skills mới** giải quyết một khoảng trống trong workflow chưa được hỗ trợ
- **Cải tiến** cho các agent, skill, hoặc hook hiện có
- **Sửa lỗi tài liệu (Documentation corrections)** — thông tin sai, tham chiếu hỏng, các bước đã lỗi thời

Các yêu cầu tính năng (feature requests) gửi dưới dạng PR sẽ bị đóng. Hãy mở một issue để thảo luận thay vì tạo PR.

**Những gì repo này KHÔNG chứa:**
CCGS là hệ thống giúp bạn xây dựng game, không phải là nơi lưu trữ các game mà bạn tạo ra với nó. Các GDD, ADR, PRD, ý tưởng game, level design, narrative doc hoặc bất kỳ tài liệu đầu ra nào do CCGS tạo cho dự án riêng của bạn sẽ không được merge vào đây — hãy giữ chúng trong repo của riêng bạn.

## Các quy tắc kỹ thuật không thể thương lượng

Dưới đây là những điều sẽ khiến PR của bạn bị từ chối nếu bạn bỏ qua:

**File Skill**
- Skill nằm trong `.claude/skills/<name>/SKILL.md` — định dạng thư mục con là bắt buộc. Các file `.md` nằm ngang hàng phẳng sẽ bị Claude Code bỏ qua trong im lặng.
- `SKILL.md` phải bao gồm YAML frontmatter: `name`, `description`, `argument-hint`, `allowed-tools`, và `model`
- Phân tầng Model: `haiku` cho các kiểm tra trạng thái chỉ đọc (read-only status checks), `opus` cho tổng hợp đa tài liệu và các cổng giai đoạn (phase gates), `sonnet` cho các tác vụ còn lại

**Hooks**
- Sử dụng `grep -E` — tuyệt đối không dùng `grep -P` (regex Perl sẽ bị lỗi trên Windows Git Bash)
- Bao gồm cơ chế dự phòng (fallback) cho các hệ thống chưa cài đặt `jq` hoặc `python`
- Hook chạy khi mở mỗi phiên làm việc — chúng phải thoát nhanh chóng và an toàn (`exit 0`) khi không áp dụng

**Agents**
- Agent mới phải bao gồm phần **Collaboration Protocol** mô tả cách agent đặt câu hỏi và chuyển giao quyền quyết định cho người dùng
- Agent không được chỉnh sửa các file nằm ngoài phạm vi phụ trách đã được ghi nhận trong tài liệu nếu không có ủy quyền rõ ràng từ người dùng

**Tài liệu tham chiếu (Reference docs)**
- Nếu PR của bạn thêm hoặc sửa đổi skill, agent, hoặc hook, hãy cập nhật tài liệu tham chiếu tương ứng (agent-roster, skills-reference, hooks-reference, hoặc rules-reference). Các PR thêm thành phần mới mà không cập nhật chỉ mục sẽ bị yêu cầu chỉnh sửa lại.

## Nguyên tắc cộng tác (The Collaborative Principle)

CCGS không phải là một hệ thống tự động hoàn toàn (autonomous). Mọi workflow đều tuân theo:
**Hỏi (Question) → Đưa lựa chọn (Options) → Quyết định (Decision) → Bản thảo (Draft) → Phê duyệt (Approval) → Ghi file (Write)**

Skill và agent phải hỏi ý kiến trước khi hành động. Không có thao tác ghi file nào diễn ra nếu thiếu sự xác nhận rõ ràng của người dùng. Nếu đóng góp của bạn để agent tự ý ra quyết định hoặc đơn phương ghi file, PR đó sẽ không được merge.

## Kiểm thử các thay đổi của bạn

Hãy chạy thử trong một phiên làm việc Claude Code và xác nhận tính năng hoạt động end-to-end. Đối với skill, gọi skill đó và xác minh kết quả đầu ra khớp với những gì skill cam kết thực hiện. Đối với hook, kích hoạt sự kiện liên quan và xác nhận hook hoạt động chính xác cũng như thoát sạch sẽ.

Đính kèm một ghi chú ngắn trong phần mô tả PR giải thích những gì bạn đã kiểm thử và kết quả đầu ra trông như thế nào.

## Định dạng Commit

Sử dụng chuẩn [Conventional Commits](https://www.conventionalcommits.org/):

```
feat: add /retrospective skill for end-of-sprint reviews
fix: correct grep -P usage in session-start hook
docs: update skills-reference with new /qa-plan entry
```

Các loại commit (Types): `feat`, `fix`, `docs`, `chore`, `refactor`, `test`

## Quy trình xử lý PR

- PR của bạn sẽ được tự động phân công cho maintainer qua CODEOWNERS
- Việc đánh giá (review) sẽ diễn ra khi maintainer sắp xếp được thời gian — đây là dự án được duy trì bởi một người
- Nếu PR của bạn mở mà chưa có phản hồi sau vài tuần, việc để lại một bình luận nhắc nhở là hoàn toàn bình thường
- Người đóng góp có PR được merge sẽ được ghi nhận trong release notes

## Tính tương thích nền tảng

CCGS phải hoạt động trên Windows (Git Bash), macOS, và Linux. Nếu hook hoặc script của bạn sử dụng bất kỳ thứ gì chỉ dành riêng cho một hệ điều hành, nó sẽ bị từ chối. Khi phân vân, hãy kiểm thử trên Windows.
