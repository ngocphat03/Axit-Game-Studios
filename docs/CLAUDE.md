# Thư mục Docs (Docs Directory)

Khi tạo mới hoặc chỉnh sửa các file trong thư mục này, hãy tuân theo các tiêu chuẩn sau.

## Hồ sơ quyết định kiến trúc (`docs/architecture/` - Architecture Decision Records / ADR)

Sử dụng template ADR: `.claude/docs/templates/architecture-decision-record.md`

**Các phần bắt buộc:** Tiêu đề (Title), Trạng thái (Status), Bối cảnh (Context), Quyết định (Decision), Hệ quả (Consequences),
Phụ thuộc ADR (ADR Dependencies), Tương thích Engine (Engine Compatibility), Yêu cầu GDD được giải quyết (GDD Requirements Addressed)

**Vòng đời trạng thái (Status lifecycle):** `Proposed` → `Accepted` → `Superseded`
- Tuyệt đối không bỏ qua `Accepted` — các story tham chiếu đến một ADR đang ở trạng thái `Proposed` sẽ tự động bị chặn (auto-blocked)
- Sử dụng `/architecture-decision` để tạo ADR qua quy trình có hướng dẫn

**TR Registry:** `docs/architecture/tr-registry.yaml`
- Các mã định danh yêu cầu kỹ thuật ổn định (ví dụ: `TR-MOV-001`) liên kết các yêu cầu GDD với story
- Không bao giờ đánh lại số các ID hiện có — chỉ nối thêm (append) các ID mới
- Được cập nhật bởi `/architecture-review` Phase 8

**Control Manifest:** `docs/architecture/control-manifest.md`
- Bảng quy tắc ngắn gọn cho lập trình viên: Bắt buộc (Required) / Cấm (Forbidden) / Lan can bảo vệ (Guardrails) theo từng tầng kiến trúc
- Ghi dấu ngày tháng `Manifest Version:` ở phần header
- Story nhúng phiên bản này; `/story-done` sẽ kiểm tra tính lỗi thời (staleness)

**Xác thực:** Chạy `/architecture-review` sau khi hoàn thành một tập hợp các ADR.

## Tham chiếu Engine (`docs/engine-reference/`)

Ảnh chụp nhanh (snapshot) API engine đã ghim phiên bản. **Luôn kiểm tra tại đây trước khi sử dụng bất kỳ API engine nào** — dữ liệu huấn luyện của LLM có thể cũ hơn phiên bản engine đã ghim.

Engine hiện tại: xem `docs/engine-reference/godot/VERSION.md`
