# Danh sách Skill khả dụng (Slash Commands)

73 lệnh slash command được tổ chức theo từng giai đoạn. Gõ `/` trong Antigravity để truy cập bất kỳ lệnh nào.

## Khởi động & Điều hướng (Onboarding & Navigation)

| Lệnh | Mục đích |
|---|---|
| `/start` | Khởi động lần đầu — hỏi bạn đang ở đâu, sau đó dẫn dắt bạn tới quy trình phù hợp |
| `/help` | Nhận biết ngữ cảnh "tôi cần làm gì tiếp theo?" — đọc giai đoạn hiện tại và gợi ý bước bắt buộc kế tiếp |
| `/project-stage-detect` | Kiểm toán toàn bộ dự án — phát hiện giai đoạn, xác định các khoảng trống tài liệu, đề xuất bước tiếp theo |
| `/setup-engine` | Cấu hình engine + phiên bản, phát hiện khoảng trống kiến thức, điền tài liệu tham chiếu nhận biết phiên bản |
| `/adopt` | Audit định dạng dự án cũ — kiểm tra cấu trúc nội bộ của các GDD/ADR/story hiện có, lập kế hoạch migration |

## Thiết kế Game (Game Design)

| Lệnh | Mục đích |
|---|---|
| `/brainstorm` | Lên ý tưởng có hướng dẫn bằng các phương pháp studio chuyên nghiệp (MDA, SDT, Bartle, verb-first) |
| `/map-systems` | Phân rã concept game thành các hệ thống, lập bản đồ phụ thuộc, ưu tiên thứ tự thiết kế |
| `/design-system` | Soạn thảo GDD từng phần có hướng dẫn cho một hệ thống game đơn lẻ |
| `/quick-design` | Bản đặc tả thiết kế tinh gọn cho các thay đổi nhỏ — tinh chỉnh cân bằng, sửa đổi nhỏ |
| `/review-all-gdds` | Đánh giá tính nhất quán chéo và tính toàn diện trong thiết kế game trên tất cả các GDD |
| `/propagate-design-change` | Khi một GDD được sửa đổi, tìm các ADR bị ảnh hưởng và tạo báo cáo tác động |

## Mỹ thuật & Tài nguyên (Art & Assets)

| Lệnh | Mục đích |
|---|---|
| `/art-bible` | Soạn thảo Art Bible từng phần có hướng dẫn — tạo đặc tả nhận diện hình ảnh trước khi sản xuất asset |
| `/asset-spec` | Tạo đặc tả hình ảnh từng asset và prompt sinh ảnh AI từ GDD, tài liệu màn chơi, hoặc hồ sơ nhân vật |
| `/asset-audit` | Kiểm toán asset về quy ước đặt tên, ngân sách dung lượng file và tính tuân thủ pipeline |

## Thiết kế Giao diện & Trải nghiệm UX (UX & Interface Design)

| Lệnh | Mục đích |
|---|---|
| `/ux-design` | Soạn thảo đặc tả UX từng phần có hướng dẫn (màn hình/luồng, HUD, hoặc thư viện mẫu tương tác) |
| `/ux-review` | Xác thực các đặc tả UX về độ khớp GDD, khả năng tiếp cận (accessibility) và tuân thủ mẫu tương tác |

## Kiến trúc (Architecture)

| Lệnh | Mục đích |
|---|---|
| `/create-architecture` | Soạn thảo có hướng dẫn cho tài liệu kiến trúc tổng thể (master architecture) |
| `/architecture-decision` | Tạo một bản ghi quyết định kiến trúc (ADR) |
| `/architecture-review` | Xác thực tất cả các ADR về độ hoàn thiện, thứ tự phụ thuộc và độ bao phủ GDD |
| `/create-control-manifest` | Tạo bảng quy tắc lập trình phẳng từ các ADR đã được chấp thuận |

## Stories & Sprints

| Lệnh | Mục đích |
|---|---|
| `/create-epics` | Chuyển đổi GDD + ADR thành các epic — mỗi module kiến trúc một epic |
| `/create-stories` | Phân rã một epic đơn lẻ thành các file story có thể triển khai code |
| `/dev-story` | Đọc một story và triển khai code — điều phối tới đúng agent lập trình |
| `/sprint-plan` | Tạo hoặc cập nhật kế hoạch sprint; khởi tạo sprint-status.yaml |
| `/sprint-status` | Ảnh chụp nhanh sprint 30 dòng nhanh chóng (đọc từ sprint-status.yaml) |
| `/story-readiness` | Xác thực một story đã sẵn sàng triển khai trước khi nhận (READY/NEEDS WORK/BLOCKED) |
| `/story-done` | Đánh giá hoàn thành 8 giai đoạn sau khi code xong; cập nhật file story, hiển thị story tiếp theo |
| `/estimate` | Ước lượng nỗ lực có cấu trúc kèm phân tích độ phức tạp, phụ thuộc và rủi ro |

## Đánh giá & Phân tích (Reviews & Analysis)

| Lệnh | Mục đích |
|---|---|
| `/design-review` | Đánh giá tài liệu thiết kế game về tính hoàn thiện và tính nhất quán |
| `/code-review` | Đánh giá mã nguồn kiến trúc cho một file hoặc tập thay đổi |
| `/balance-check` | Phân tích dữ liệu cân bằng game, công thức và cấu hình — đánh dấu các giá trị bất thường |
| `/content-audit` | Kiểm toán số lượng nội dung được chỉ định trong GDD so với nội dung đã triển khai |
| `/scope-check` | Phân tích phạm vi tính năng hoặc sprint so với kế hoạch ban đầu, cảnh báo phình to quy mô |
| `/perf-profile` | Đo đạc hiệu năng có cấu trúc kèm xác định điểm nghẽn cổ chai |
| `/tech-debt` | Quét, theo dõi, sắp xếp ưu tiên và báo cáo về nợ kỹ thuật (tech debt) |
| `/gate-check` | Xác thực tính sẵn sàng để chuyển tiếp giữa các giai đoạn phát triển (PASS/CONCERNS/FAIL) |
| `/consistency-check` | Quét tất cả các GDD đối chiếu với sổ đăng ký thực thể để phát hiện mâu thuẫn chéo |
| `/security-audit` | Kiểm toán game về các lỗ hổng bảo mật: can thiệp file save, gian lận, khai thác mạng, lộ dữ liệu |

## QA & Kiểm thử (QA & Testing)

| Lệnh | Mục đích |
|---|---|
| `/qa-plan` | Tạo kế hoạch kiểm thử QA cho một sprint hoặc tính năng |
| `/smoke-check` | Chạy cổng kiểm thử nhanh (smoke test) luồng quan trọng trước khi bàn giao cho QA |
| `/soak-test` | Tạo quy trình kiểm thử độ ổn định kéo dài (soak test) cho các phiên chơi game lâu |
| `/regression-suite` | Ánh xạ độ bao phủ kiểm thử tới các luồng quan trọng trong GDD, tìm bug đã sửa nhưng thiếu test hồi quy |
| `/test-setup` | Dựng khung test framework và pipeline CI/CD cho engine của dự án |
| `/test-helpers` | Tạo các thư viện trợ giúp kiểm thử đặc thù theo engine cho bộ test |
| `/test-evidence-review` | Đánh giá chất lượng của các file test và tài liệu bằng chứng thủ công |
| `/test-flakiness` | Phát hiện các bài test không tất định (chập chờn) từ nhật ký chạy CI |
| `/skill-test` | Xác thực các file skill về độ tuân thủ cấu trúc và tính đúng đắn của hành vi |
| `/skill-improve` | Cải tiến skill bằng vòng lặp test-fix-retest — chẩn đoán, đề xuất sửa, viết lại, xác minh |

## Sản xuất (Production)

| Lệnh | Mục đích |
|---|---|
| `/milestone-review` | Đánh giá tiến độ milestone và tạo báo cáo trạng thái |
| `/retrospective` | Chạy buổi tổng kết cải tiến (retrospective) có cấu trúc cho sprint hoặc milestone |
| `/bug-report` | Tạo báo cáo bug có cấu trúc |
| `/bug-triage` | Đọc tất cả bug đang mở, đánh giá lại độ ưu tiên vs mức nghiêm trọng, phân công người xử lý |
| `/reverse-document` | Tạo tài liệu thiết kế hoặc kiến trúc từ code triển khai hiện có |
| `/playtest-report` | Tạo báo cáo playtest có cấu trúc hoặc phân tích các ghi chú playtest có sẵn |

## Phát hành (Release)

| Lệnh | Mục đích |
|---|---|
| `/release-checklist` | Tạo và xác thực checklist trước phát hành cho bản build hiện tại |
| `/launch-checklist` | Xác thực toàn diện tính sẵn sàng ra mắt trên tất cả các phòng ban |
| `/changelog` | Tự động tạo changelog từ git commits và dữ liệu sprint |
| `/patch-notes` | Tạo patch notes hướng tới người chơi từ lịch sử git và dữ liệu nội bộ |
| `/hotfix` | Quy trình sửa lỗi khẩn cấp kèm audit trail, bỏ qua quy trình sprint thông thường |
| `/day-one-patch` | Chuẩn bị bản vá ngày đầu (day-one patch) tập trung cho các sự cố đã biết sau khi chốt gold master |

## Sáng tạo & Nội dung (Creative & Content)

| Lệnh | Mục đích |
|---|---|
| `/prototype` | Prototype ý tưởng — bản build dùng một lần ngay sau brainstorm để kiểm chứng ý tưởng cốt lõi (Giai đoạn 1) |
| `/vertical-slice` | Xác thực Tiền sản xuất — bản build end-to-end chất lượng sản xuất trước khi bước vào Sản xuất (Giai đoạn 4) |
| `/onboard` | Tạo tài liệu onboarding theo ngữ cảnh cho một người đóng góp hoặc agent mới |
| `/localize` | Quy trình bản địa hóa: trích xuất chuỗi ngôn ngữ, xác thực, độ sẵn sàng dịch thuật |

## Điều phối Nhóm (Team Orchestration)

Điều phối nhiều agent trên một khu vực tính năng đơn lẻ:

| Lệnh | Điều phối các Agent |
|---|---|
| `/team-combat` | game-designer + gameplay-programmer + ai-programmer + technical-artist + sound-designer + qa-tester |
| `/team-narrative` | narrative-director + writer + world-builder + level-designer |
| `/team-ui` | ux-designer + ui-programmer + art-director + accessibility-specialist |
| `/team-release` | release-manager + qa-lead + devops-engineer + producer |
| `/team-polish` | performance-analyst + technical-artist + sound-designer + qa-tester |
| `/team-audio` | audio-director + sound-designer + technical-artist + gameplay-programmer |
| `/team-level` | level-designer + narrative-director + world-builder + art-director + systems-designer + qa-tester |
| `/team-live-ops` | live-ops-designer + economy-designer + community-manager + analytics-engineer |
| `/team-qa` | qa-lead + qa-tester + gameplay-programmer + producer |
