# Danh sách Hook đang hoạt động (Active Hooks)

Các hook được cấu hình trong `.claude/settings.json` và kích hoạt tự động:

| Hook | Sự kiện | Điều kiện kích hoạt | Hành động |
|---|---|---|---|
| `validate-commit.sh` | PreToolUse (Bash) | Lệnh `git commit` | Xác thực các phần của tài liệu thiết kế, file dữ liệu JSON, giá trị hardcode, định dạng TODO |
| `validate-push.sh` | PreToolUse (Bash) | Lệnh `git push` | Cảnh báo khi push lên các nhánh được bảo vệ (develop/main) |
| `validate-assets.sh` | PostToolUse (Write/Edit) | Thay đổi file asset | Kiểm tra quy ước đặt tên và tính hợp lệ của JSON cho các file trong `assets/` |
| `session-start.sh` | SessionStart | Phiên làm việc bắt đầu | Nạp ngữ cảnh sprint, milestone, hoạt động git; phát hiện và xem trước file trạng thái active để phục hồi |
| `detect-gaps.sh` | SessionStart | Phiên làm việc bắt đầu | Phát hiện dự án mới (gợi ý /start) và tài liệu còn thiếu khi code/prototype đã tồn tại, gợi ý /reverse-document hoặc /project-stage-detect |
| `pre-compact.sh` | PreCompact | Nén ngữ cảnh | Xuất trạng thái phiên làm việc (active.md, file đã sửa, bản thảo GDD) vào cuộc trò chuyện trước khi nén để sống sót qua tóm tắt |
| `post-compact.sh` | PostCompact | Sau khi nén | Nhắc Claude khôi phục trạng thái phiên làm việc từ checkpoint `active.md` |
| `notify.sh` | Notification | Sự kiện thông báo | Hiển thị thông báo Windows toast qua PowerShell |
| `session-stop.sh` | Stop | Phiên làm việc kết thúc | Tóm tắt thành quả đạt được và cập nhật nhật ký phiên làm việc |
| `log-agent.sh` | SubagentStart | Agent được gọi | Bắt đầu vết audit — ghi log gọi subagent kèm dấu thời gian |
| `log-agent-stop.sh` | SubagentStop | Agent dừng lại | Kết thúc vết audit — hoàn tất bản ghi subagent |
| `validate-skill-change.sh` | PostToolUse (Write/Edit) | Thay đổi file skill | Khuyến nghị chạy `/skill-test` sau khi bất kỳ file nào trong `.claude/skills/` được ghi hoặc sửa |

Tài liệu tham khảo chi tiết về hook: `.claude/docs/hooks-reference/`
Tài liệu schema đầu vào của hook: `.claude/docs/hooks-reference/hook-input-schemas.md`
