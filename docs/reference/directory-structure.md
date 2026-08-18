# Cấu trúc thư mục (Directory Structure)

```text
/
├── GEMINI.md                    # Cấu hình chính của dự án & chỉ dẫn điều phối Antigravity
├── .agents/                     # Định nghĩa Skills, Rules, MCP config của Antigravity
├── .axit/                       # Axit Framework (Core profiles, system registries, plans, active state)
├── src/                         # Mã nguồn game (Unity client, backend, services, core, gameplay)
├── assets/                      # Tài nguyên game (art, audio, vfx, shaders, data)
├── design/                      # Tài liệu thiết kế game (GDD, cốt truyện, màn chơi, cân bằng)
├── docs/                        # Tài liệu kỹ thuật & kiến trúc (ADRs, Master Architecture, reference)
│   ├── reference/               # Tài liệu tham chiếu, director gates, technical preferences, templates
│   └── engine-reference/        # Bản chụp nhanh API engine đã tuyển chọn (cố định theo phiên bản)
├── tests/                       # Bộ kiểm thử (unit, integration, performance, playtest)
├── tools/                       # Công cụ build và pipeline (ci, build, asset-pipeline)
├── prototypes/                  # Các bản prototype thử nghiệm nhanh (cô lập khỏi src/)
└── production/                  # Quản lý sản xuất (sprints, milestones, releases)
    ├── session-state/           # Trạng thái phiên làm việc tạm thời (active.md — đã gitignore)
    └── session-logs/            # Nhật ký audit phiên làm việc (đã gitignore)
```
