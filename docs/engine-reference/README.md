# Tài liệu tham chiếu Engine (Engine Reference Documentation)

Thư mục này chứa các ảnh chụp tài liệu (documentation snapshots) được chọn lọc và ghim phiên bản cho (các) game engine được sử dụng trong dự án. Các file này tồn tại vì **kiến thức của LLM có ngày hết hạn (knowledge cutoff)** và các game engine thì cập nhật thường xuyên.

## Vì sao tài liệu này tồn tại

Dữ liệu huấn luyện của Claude có giới hạn thời gian (knowledge cutoff). Các game engine như Godot, Unity, và Unreal thường xuyên phát hành các bản cập nhật gây ra các thay đổi API gây phá vỡ (breaking changes), tính năng mới và các mẫu thiết kế đã bị ngừng hỗ trợ (deprecated). Nếu không có các file tham chiếu này, agent sẽ đề xuất code lỗi thời.

## Cấu trúc

Mỗi engine có một thư mục riêng:

```
<engine>/
├── VERSION.md              # Phiên bản đã ghim, ngày xác minh, khoảng trống kiến thức
├── breaking-changes.md     # Thay đổi API giữa các phiên bản, phân theo mức rủi ro
├── deprecated-apis.md      # Bảng tra cứu "Không dùng X → Dùng Y"
├── current-best-practices.md  # Các phương pháp hay nhất mới chưa có trong dữ liệu huấn luyện của model
└── modules/                # Tra cứu nhanh theo từng hệ thống con (~150 dòng tối đa mỗi file)
    ├── rendering.md
    ├── physics.md
    └── ...
```

## Cách Agent sử dụng các file này

Các agent chuyên trách engine (Engine-specialist) được chỉ dẫn:

1. Đọc `VERSION.md` để xác nhận phiên bản engine hiện tại
2. Kiểm tra `deprecated-apis.md` trước khi đề xuất bất kỳ API engine nào
3. Tham khảo `breaking-changes.md` để biết các lưu ý theo từng phiên bản cụ thể
4. Đọc các file `modules/*.md` liên quan khi làm việc trên hệ thống con đó

## Bảo trì

### Khi nào cần cập nhật

- Sau khi nâng cấp phiên bản engine
- Khi model LLM được cập nhật (knowledge cutoff mới)
- Sau khi chạy `/refresh-docs` (nếu khả dụng)
- Khi bạn phát hiện ra một API mà model hiểu sai

### Cách cập nhật

1. Cập nhật `VERSION.md` với phiên bản engine mới và ngày cập nhật
2. Thêm các mục mới vào `breaking-changes.md` cho bước chuyển phiên bản
3. Chuyển các API mới bị deprecated vào `deprecated-apis.md`
4. Cập nhật `current-best-practices.md` với các mẫu thiết kế mới
5. Cập nhật các file `modules/*.md` liên quan với các thay đổi API
6. Thiết lập ngày "Last verified" trên tất cả các file đã chỉnh sửa

### Quy tắc chất lượng

- Mỗi file phải có ngày "Last verified: YYYY-MM-DD"
- Giữ các file module dưới 150 dòng (tiết kiệm dung lượng ngữ cảnh context)
- Đính kèm các ví dụ code minh họa đúng/sai
- Dẫn link tới tài liệu chính thức để đối chiếu
- Chỉ ghi nhận những điều khác biệt so với dữ liệu huấn luyện của model
