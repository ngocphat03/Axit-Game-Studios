# Cấu trúc thư mục (Directory Structure)

```text
/
├── CLAUDE.md                    # Cấu hình chính của dự án
├── .claude/                     # Định nghĩa Agent, skills, hooks, rules, tài liệu hướng dẫn
├── src/                         # Mã nguồn game (core, gameplay, ai, networking, ui, tools)
├── assets/                      # Tài nguyên game (art, audio, vfx, shaders, data)
├── design/                      # Tài liệu thiết kế game (gdd, cốt truyện, màn chơi, cân bằng)
├── docs/                        # Tài liệu kỹ thuật (kiến trúc, api, tổng kết sau dự án)
│   └── engine-reference/        # Bản chụp nhanh API engine đã tuyển chọn (cố định theo phiên bản)
├── tests/                       # Bộ kiểm thử (unit, integration, performance, playtest)
├── tools/                       # Công cụ build và pipeline (ci, build, asset-pipeline)
├── prototypes/                  # Các bản prototype thử nghiệm nhanh (cô lập khỏi src/)
└── production/                  # Quản lý sản xuất (sprints, milestones, releases)
    ├── session-state/           # Trạng thái phiên làm việc tạm thời (active.md — đã gitignore)
    └── session-logs/            # Nhật ký audit phiên làm việc (đã gitignore)
```
