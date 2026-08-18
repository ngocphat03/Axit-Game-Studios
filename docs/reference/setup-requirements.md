# Yêu cầu cài đặt (Setup Requirements)

Template này yêu cầu một số công cụ được cài đặt để hoạt động đầy đủ tính năng.
Tất cả các hook đều xử lý lỗi linh hoạt nếu thiếu công cụ — không có gì bị hỏng, nhưng bạn sẽ mất đi các tính năng kiểm tra xác thực.

## Bắt buộc

| Công cụ | Mục đích | Cài đặt |
|---|---|---|
| **Git** | Quản lý phiên bản, quản lý nhánh | [git-scm.com](https://git-scm.com/) |
| **Antigravity (AGY)** | AI agent CLI / IDE | Google Antigravity CLI / IDE (`agy`) |

## Khuyến nghị

| Công cụ | Được sử dụng bởi | Mục đích | Cài đặt |
|---|---|---|---|
| **jq** | Hooks (7/12) | Phân tích cú pháp JSON trong hook commit/push/asset/agent | Xem bên dưới |
| **Python 3** | Hooks (2/12) | Xác thực JSON cho các file dữ liệu | [python.org](https://www.python.org/) |
| **Bash** | Tất cả các hook | Thực thi shell script | Đã đi kèm với Git for Windows |

### Cài đặt jq

**Windows** (một trong các cách sau):
```
winget install jqlang.jq
choco install jq
scoop install jq
```

**macOS**:
```
brew install jq
```

**Linux**:
```
sudo apt install jq     # Debian/Ubuntu
sudo dnf install jq     # Fedora
sudo pacman -S jq       # Arch
```

## Ghi chú theo nền tảng

### Windows
- Git for Windows bao gồm **Git Bash**, cung cấp lệnh `bash` được sử dụng bởi các hook trong `.agents/hooks.json`
- Đảm bảo Git Bash nằm trong biến môi trường PATH của bạn (mặc định nếu cài qua trình cài đặt Git)
- Các hook sử dụng `bash .agents/hooks/[name].sh` — hoạt động mượt mà trên Windows

### macOS / Linux
- Bash đã có sẵn theo mặc định
- Cài đặt `jq` qua trình quản lý gói để hỗ trợ đầy đủ các hook

## Kiểm tra môi trường cài đặt

Chạy các lệnh sau để kiểm tra điều kiện tiên quyết:

```bash
git --version          # Hiển thị phiên bản git
bash --version         # Hiển thị phiên bản bash
jq --version           # Hiển thị phiên bản jq (tùy chọn)
python3 --version      # Hiển thị phiên bản python (tùy chọn)
```

## Điều gì xảy ra nếu thiếu công cụ tùy chọn

| Công cụ bị thiếu | Ảnh hưởng |
|---|---|
| **jq** | Xác thực commit, bảo vệ push, xác thực asset và hook audit agent sẽ âm thầm bỏ qua kiểm tra. Commit và push vẫn diễn ra bình thường. |
| **Python 3** | Xác thực file dữ liệu JSON trong hook commit và asset bị bỏ qua. JSON không hợp lệ có thể bị commit mà không có cảnh báo. |
| **Cả hai** | Tất cả các hook vẫn chạy mà không báo lỗi (exit 0) nhưng không cung cấp tính năng xác thực nào. Bạn đang hoạt động không có lưới bảo vệ an toàn. |

## Môi trường Khuyến nghị

Template được tối ưu hóa cho:
- **Google Antigravity IDE**
- **Antigravity CLI** (`agy`)
- **VS Code / Cursor** với Antigravity extension
