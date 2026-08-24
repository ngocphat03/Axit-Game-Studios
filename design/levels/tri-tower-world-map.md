# Thiết Kế Cấu Trúc Bản Đồ: Tam Giác Tam Tháp, Đại Sảnh & Đảo Nổi Đồng Trục Tại Tâm (Tri-Tower World Map Architecture - 10x Epic Scale)

- **Mã tài liệu**: `MAP-TRI-001`
- **Phiên bản**: 3.1.0 (Prototype-aligned Edition)
- **Hệ thống**: World Topology, Equilateral Triangle Geometry, Ground Plaza, 3 Bridges & Sky Isle
- **Tác giả**: Level Designer & Technical Architect (Axit Framework)
- **Trạng thái**: Approved Master Level Specification
- **Ngày cập nhật**: 2026-08-25
- **Tài liệu tham chiếu**: [world-and-story.md](../narrative/world-and-story.md), [jump-system.md](../gdd/jump-system.md), [combat-system.md](../gdd/combat-system.md)

---

> **Prototype snapshot:** Build HTML hiện đã dựng được Grand Ground Plaza, biểu tượng Bút Thần/RGB ở trung tâm, ba cầu và ba vùng tháp, cùng Sky Isle. Sàn trung tâm và các bề mặt chính có collider gameplay; camera cũng va chạm và tự kéo gần khi gặp nền/tường. Kích thước trong tài liệu này vẫn là master layout mục tiêu; mật độ encounter, boss và luồng mở khóa tháp chưa hoàn thiện đầy đủ trong prototype.


## 1. Bản Đồ Hình Học Tam Giác Đều: Đại Sảnh & Đảo Nổi Tại Tâm (Equilateral Tri-Grid - 10x Scale)

Thế giới được cấu trúc hoàn toàn đối xứng trên một **Tam Giác Đều Khổng Lồ**:
- **3 Góc Tam Giác**: Đặt **3 Tòa Tháp RGB** (Tháp Đỏ 🔴, Tháp Lục 🟢, Tháp Lam 🔵) tại bán kính **$R = 350\text{m}$**.
- **Tâm Tam Giác ($X=0, Z=0$)**: 
  - **Mặt đất ($Y = 0\text{m}$)**: **Đại Sảnh Trung Tâm (Grand Ground Plaza)** — điểm xuất phát và là nút giao chính của toàn bộ thế giới ($D = 200\text{m}$).
  - **Trên không ($Y = +250\text{m}$)**: **Đảo Nổi Trung Tâm (Central Sky Isle)** lơ lửng ngay trên đỉnh đầu của Đại Sảnh Trung Tâm ($D = 400\text{m}$).
- **3 Cây Cầu Đá ($300\text{m} \times 35\text{m}$)**: Chạy thẳng từ Tâm Đại Sảnh tỏa ra 3 Góc Tháp (đối xứng góc $120^\circ$).

```text
                                       ▲ PHÍA BẮC (Góc 1: 0°)
                                    [🟢 THÁP LỤC - RỪNG RẬM]
                                        (X = 0, Z = +350m)
                                                ▲
                                                │
                                                │ [CẦU BẮC - 300m x 35m]
                                                │
                                                ▼
                                    ┌───────────────────────┐
                                    │    TÂM TAM GIÁC ĐỀU   │
                                    │ 🏛️ ĐẠI SẢNH TRUNG TÂM │
                                    │     (X=0, Z=0, Y=0)   │
                                    │       (D = 200m)      │
                                    │                       │
                                    │  [NGAY TRÊN ĐỈNH ĐẦU: │
                                    │  ⚪ ĐẢO NỔI LƠ LỬNG   │
                                    │   (Y=+250m, D=400m)]  │
                                    └───────┬───────┬───────┘
                                           /         \
                      [CẦU TÂY NAM        /           \        [CẦU ĐÔNG NAM
                       300m x 35m]       /             \        300m x 35m]
                                        ▼               ▼
                       [🔵 THÁP LAM - BĂNG GIÁ]   [🔴 THÁP ĐỎ - HỎA DIỆM]
                         (Góc 2: 240° / Tây Nam)     (Góc 3: 120° / Đông Nam)
                         (X = -300m, Z = -175m)      (X = +300m, Z = -175m)
```

---

## 2. Thiết Kế Chi Tiết Các Khu Vực Không Gian (Spatial Metrics - 10x Scale)

```text
Mặt Cắt Đứng (Trục Thẳng Đứng Y qua Tâm Tam Giác X=0, Z=0):

    Độ Cao (Y)
      ▲
      │              [⚪ ĐẢO NỔI TRUNG TÂM (Trận Chiến Boss Cuối Void Sovereign)]
+250m ┼──────────────► ┌───────────────────────────────────────────────────┐
      │                 │   Quảng Trường Bạch Kim (D = 400m, Y = +250m)     │
      │                 └───────────────────────────────────────────────────┘
      │                                           ▲
      │                                           │ [CỘT SÁNG / THANG MÁY NĂNG LƯỢNG]
      │                                           │ (Tụ 3 Tia Sáng RGB từ 3 Tháp, V = 45m/s)
      │                                           ▼
  0m  ┼──────────────► ┌───────────────────────────────────────────────────┐
      │                 │   🏛️ ĐẠI SẢNH TRUNG TÂM (Hub Mặt Đất, D = 200m)  │
      │                 └──────────┬───────────────────────────┬────────────┘
      │               [Cầu Lam]    │                           │    [Cầu Đỏ]
      │              ◄─────────────┘                           └─────────────►
      └────────────────────────────────────────────────────────────────────────► Trục (X, Z)
```

| Khu Vực | Vị Trí Tọa Độ | Kích Thước & Hình Học | Mô Tả & Chức Năng Gameplay |
| :--- | :--- | :--- | :--- |
| **🏛️ Đại Sảnh Trung Tâm** | **Tâm Tam Giác** $(0, 0, 0)$ | Hình tròn $D = 200.0\text{m}$, sàn phẳng $Y = 0\text{m}$. | Khu vực an toàn khởi đầu (Hub trung tâm). Có 3 Cổng Vòm dẫn ra 3 cây cầu và Vòng Tròn Ma Trận ($R=25\text{m}$) ở giữa để kích hoạt Cột Sáng lên Đảo Nổi. |
| **⚪ Đảo Nổi Trung Tâm** | **Đỉnh Đầu Tâm** $(0, 0, +250\text{m})$ | Hình tròn $D = 400.0\text{m}$, lơ lửng trên mây. | Quảng trường đá cẩm thạch trắng, nơi diễn ra trận chiến sinh tử với **Boss Cuối Cùng (Void Sovereign)** khi đủ 3 tia năng lượng RGB. |
| **🌉 3 Cây Cầu Đá Tỏa Nhánh** | Nối từ Tâm $(0,0)$ ra 3 góc tháp | Rộng **$35.0\text{m}$**, dài **$300.0\text{m}$**. | Tuyến đường chiến đấu đại lộ chia 3 đợt quái quy mô lớn theo bước tiến ($50\text{m} \to 150\text{m} \to 250\text{m}$). Hỗ trợ Sprint ($32\text{m/s}$). |
| **🔵 Tháp Lam (Băng Sắc)** | Góc Tây Nam $(240^\circ)$ $(-300, 0, -175)$ | Tháp băng đá $D = 250\text{m}$, chóp $H=90\text{m}$. | Sàn đấu Boss Băng Long Glacies $\to$ Nhận **Song Băng Đoản Kiếm** $\to$ Bắn tia Lam về Đảo Nổi. |
| **🔴 Tháp Đỏ (Hỏa Sắc)** | Góc Đông Nam $(120^\circ)$ $(+300, 0, -175)$ | Tháp nham thạch $D = 250\text{m}$, chóp $H=90\text{m}$. | Sàn đấu Boss Viêm Ma Ignis $\to$ Nhận **Hỏa Đại Kiếm** $\to$ Bắn tia Đỏ về Đảo Nổi. |
| **🟢 Tháp Lục (Mộc Sắc)** | Góc Bắc $(0^\circ)$ $(0, 0, +350)$ | Tháp rừng đại thụ $D = 250\text{m}$, chóp $H=90\text{m}$. | Sàn đấu Boss Nữ Chúa Gai Sylva $\to$ Nhận **Liềm Sinh Mệnh** $\to$ Bắn tia Lục về Đảo Nổi. |

---

## 3. Cơ Chế Phân Đợt Quái Dọc Theo 3 Cây Cầu ($300\text{m} \times 35\text{m}$)

Mỗi khi người chơi mở một Cổng Vòm tại Đại Sảnh để bước lên Cầu, quái sẽ xuất hiện theo cự ly di chuyển:

```text
[CỔNG ĐẠI SẢNH (0m)] ──(50m)──► [MỐC 50m: Wave 1 (4 Swarmers)] ──(100m)──► [MỐC 150m: Wave 2 (2 Archers trên Trụ Đá 3.5m + 4 Swarmers)] ──(100m)──► [MỐC 250m: Wave 3 (2 Mini-Boss Brutes)] ──► [CỔNG THÁP]
```

1. **Mốc $50\text{m}$ (Khởi đầu cây cầu)**:
   - $4$ Zombie Cận Chiến (Swarmers) trồi lên bao vây dọc làn cầu $35\text{m}$.
   - Người chơi dùng **🎋 Gậy Tre** vung đòn combo `LMB` tích Pip 1-2-3 và nhấp Finisher nhát thứ 4 dậm nổ tiêu diệt ($R=12\text{m}$).
2. **Mốc $150\text{m}$ (Giữa cầu)**:
   - $2$ Skeleton Archers xuất hiện trên hai trụ đá cao $3.5\text{m}$ hai bên thành cầu + $4$ Swarmers xông tới.
   - Người chơi áp dụng **Jump System (mục tiêu thiết kế $H=1.5\text{m}$)** nhảy lên các điểm cao có bố trí phù hợp để triệt hạ Cung thủ, né tên bắn tầm xa.
3. **Mốc $250\text{m} - 280\text{m}$ (Cận cửa Tháp)**:
   - $2$ Mini-Boss Brutes ($H=5.5\text{m}$) rơi từ trên trời xuống tạo sóng chấn động ($R=14\text{m}$) chặn cửa Tháp.
   - Người chơi nhảy né Shockwave, phá giáp Super Armor, hạ gục Brutes $\to$ Cửa Tháp mở ra!
4. **Bên Trong Tháp ($D = 250\text{m}$)**:
   - Bước vào sàn đấu đại tháp $D = 250\text{m}$ $\to$ Đấu **Boss Tháp Khổng Lồ (Height $9.5\text{m}$)**.
   - Hạ Boss $\to$ Kích hoạt hiệu ứng Color Bloom $\to$ Nhận Vũ Khí Mới $\to$ Tháp bắn cột tia sáng năng lượng màu đó truyền thẳng về Đảo Nổi Trung Tâm ($+250\text{m}$).

---

## 4. Cơ Chế Hội Tụ Năng Lượng Tại Tâm Tam Giác & Lên Đảo Nổi

```text
                                       [🟢 THÁP LỤC]
                                             │
                                             │ (Tia Năng Lượng Lục)
                                             │
                                             ▼
 [🔵 THÁP LAM] ────────(Tia Lam)────────► [⚪ ĐẢO NỔI (+250m)] ◄────────(Tia Đỏ)──────── [🔴 THÁP ĐỎ]
                                        (TÂM TAM GIÁC)
                                             ▲
                                             │ [THANG MÁY ÁNH SÁNG V = 45m/s]
                                             │
                                    [🏛️ ĐẠI SẢNH TRUNG TÂM (0m)]
```

1. **Sau khi hoàn thành Tháp 1**: Người chơi quay về Đại Sảnh $(0,0,0)$, nhìn lên trời thấy **1 Cột Tia Sáng** chiếu thẳng vào Đảo Nổi lơ lửng ngay phía trên đầu ở độ cao $+250\text{m}$.
2. **Sau khi hoàn thành Tháp 2**: Nhìn thấy **2 Cột Tia Sáng** hội tụ về Đảo Nổi.
3. **Sau khi hoàn thành cả 3 Tháp ($3/3$)**:
   - Cả 3 tia Đỏ 🔴, Lục 🟢, Lam 🔵 cùng hội tụ vào tâm Đảo Nổi, tạo nên hiện tượng cộng hưởng **Bạch Kim Quang (White Radiant Resonance)**.
   - Tại chính giữa Đại Sảnh Trung Tâm $(0,0,0)$, một **Cột Sáng / Thang Máy Ánh Sáng** khổng lồ ($D=50\text{m}$) mở ra, đưa người chơi bay thẳng đứng từ $Y = 0\text{m}$ lên $Y = +250\text{m}$ đáp xuống Quảng Trường Đảo Nổi ($D=400\text{m}$).
4. **Trận Chiến Chung Cuộc**:
   - Người chơi bước ra Quảng Trường Đảo Nổi ($D=400\text{m}$), luân chuyển 3 vũ khí RGB bằng Switch Strike để phá khiên đổi màu liên tục của **Boss Cuối (Void Sovereign, Height $14\text{m}$)**, hoàn thành giải cứu thế giới!
