# Antigravity Skill Layer (.agents/skills/)

Thư mục này chứa các **Skill** được Google Antigravity tự động phát hiện và kích hoạt theo nhu cầu (Progressive Disclosure).

Mỗi thư mục con đại diện cho một skill với cấu trúc:
```text
.agents/skills/<skill_name>/
└── SKILL.md
```

Tất cả các skill đều có YAML frontmatter chuẩn (`name`, `description`) để Antigravity nhận biết khi nào cần nạp vào context.
