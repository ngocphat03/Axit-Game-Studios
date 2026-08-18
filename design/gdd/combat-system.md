# Game Design Document: Combat & Dual-Weapon System (Pure 3D Action)

- **Mã tài liệu**: `GDD-COMBAT-001`
- **Hệ thống**: Combat System (Chiến đấu 3D Tươi Sáng, Chuyển đổi Song Vũ khí, Camera Nightfall 3D, Hướng Mặt Khóa Theo Camera & Đấu trường 4 Đợt)
- **Tác giả**: Game Designer (Axit Framework)
- **Trạng thái**: Approved Master Specification
- **Phiên bản**: 2.1.0
- **Ngày cập nhật**: 2026-08-19
- **Định dạng Không gian**: **100% Pure 3D (Không gian 3 chiều thực thể, Nhân vật 3D Model, Camera 3D TPV, Tuyệt đối KHÔNG phải 2D)**
- **Nền tảng Mục tiêu**: **PC (Bàn phím & Chuột / Keyboard & Mouse)**

---

> [!IMPORTANT]
> **QUY CHUẨN KỸ THUẬT & KHÔNG GIAN BẮT BUỘC (3D DIMENSIONALITY & SCALE MANDATE)**:
> - **Môi trường & Tỉ lệ Kích thước (3D Metric Scales)**:
>   - **Nhân vật chính (Player)**: Chiều cao chuẩn **$1.8\text{m}$** (`CharacterController`: $H = 1.8\text{m}, R = 0.4\text{m}$).
>   - **Zombie Cận chiến & Zombie Cung thủ**: Chiều cao chuẩn **$1.8\text{m}$** (`CapsuleCollider`: $H = 1.8\text{m}, R = 0.4\text{m}$).
>   - **Quái Hộ Vệ / Cầm Khiên (Shielded Brute)**: Chiều cao vượt trội **$3.0\text{m}$** (`CapsuleCollider`: $H = 3.0\text{m}, R = 0.9\text{m}$).
>   - **Trùm Tối Thượng (Final Boss Goliath)**: Chiều cao khổng lồ **$5.0\text{m}$** (`CapsuleCollider`: $H = 5.0\text{m}, R = 1.8\text{m}$).
> - **Hệ thống Khóa Hướng Mặt Theo Camera (Camera-Locked Facing / Strafe Mode)**:
>   - Khi xoay camera (di chuyển chuột), **thân và mặt nhân vật luôn luôn tự động xoay theo trục Y hướng nhìn của Camera / Tâm ngắm Crosshair**. Nhân vật luôn nhìn thẳng về hướng mà Camera đang hướng tới.
>   - Di chuyển dạng Strafe: `W` đi tiến thẳng, `S` đi lùi (mặt vẫn nhìn về phía trước), `A`/`D` bước ngang sang trái/phải.
> - **Vật lý & Chống Xuyên Thấu (3D Body Collision & Flocking Separation)**:
>   - Toàn bộ thực thể sở hữu vùng va chạm hình trụ/vòng tròn 3D (`CapsuleCollider 3D`). Người chơi và quái không thể đi xuyên qua nhau.
>   - Quái vật áp dụng thuật toán tách bầy (*Flocking Separation Force*), tự động giữ khoảng cách và đẩy nhau ra, ngăn chặn hoàn toàn hiện tượng xếp chồng mô hình 3D.
> - **Hệ thống Điều Khiển Chuột & Tâm Ngắm**:
>   - Khóa và ẩn con trỏ chuột (`PointerLock / Cursor.lockState`), bổ sung **Tâm ngắm Dấu Cộng (`+`)** cố định tại chính giữa màn hình. Di chuột tự do để xoay camera và điều hướng hướng nhìn nhân vật.
>   - Tự động mở khóa và hiện lại con trỏ chuột khi xuất hiện Modal giao diện (Bệ Thờ Thần Khí, Màn hình Thắng / Thua).

---

## 1. Overview & Visual Art Style (Tổng quan & Phong cách Mỹ thuật)

### 1.1. Tổng quan Dự án
Hệ thống Chiến đấu (Combat System) là vòng lặp hành vi trọng tâm của tựa game **3D Hack & Slash Roguelite góc nhìn thứ ba (Third-Person View - TPV)** trên nền tảng **PC**. Trò chơi mang đến trải nghiệm **chặt chém 3D nhịp độ cao liên tục (Non-stop 3D Combat Flow)** không tốn thể lực, kết hợp cơ chế hướng mặt luôn nhìn theo Camera, chuỗi combo Đánh Thường (`LMB`) / Đánh Mạnh (`RMB`) kèm **Thanh Điểm Sáng Combo 4 Nhịp (4-Pip Pipeline Meter)**, hỗ trợ nhắm mục tiêu **3D Soft-Lock với Khung ngắm 8 góc**, phân cấp ngắt chiêu quái (*Interrupt Priority*), hoán đổi 2 vũ khí tức thời (*Switch Strike*), né tránh đòn đánh trùm có vùng báo hiệu Decal 3D (*Boss Spatial 3D Telegraph & Rotation Lock*), điểm chạm kinh doanh nhận Thần Khí (*Rewarded Ad Power Spike*), hệ thống **Camera 3D Nightfall TPV**, và tiến trình đấu trường **4 Đợt Quái 3D (4-Wave 3D Arena)**.

### 1.2. Định hướng Mỹ thuật: Tươi Sáng & Rực Rỡ (Vibrant Stylized Fantasy)
- **Tone Màu & Ánh Sáng**: Thế giới tràn ngập ánh nắng ban ngày ấm áp (Warm Sunlight), bầu trời trong xanh mây trắng (Vibrant Skybox), đấu trường thảm cỏ xanh mướt điểm xuyết hoa lá cách điệu (*Zelda: Breath of the Wild / Genshin Impact style*).
- **Phong Cách Mô Hình & Shading**: 3D Stylized / Cel-Shaded với vân bề mặt vẽ tay mịn màng (Hand-painted Textures), các đường viền rõ nét và hình khối ấn tượng.
- **Màu Sắc Kỹ Xảo (VFX Palette)**:
  - *Dual Daggers*: Vệt chém màu Hồng Neon / Xanh Ngọc Cyan lấp lánh.
  - *Greatsword*: Vệt chém Lửa Cam vàng rực rỡ tỏa sóng xung kích.
  - *Death Scythe*: Vệt xoáy Tím Huyền Bí pha ánh trăng sáng rực.
  - *Vũ khí Mythic*: Bùng nổ ánh Vàng Kim Thần Thánh (Radiant Gold) và các hạt sao lấp lánh (Starburst Particles).

---

## 2. PC Input Mapping & Hệ Thống Di Chuyển Khóa Theo Camera (Camera-Locked Movement)

### 2.1. Bảng Phím Điều Khiển Chuẩn PC

| Hành động (Action) | Phím Bấm PC Chính | Phím Phụ (Alternative) | Mô tả Chi Tiết Cơ Chế |
| :--- | :--- | :--- | :--- |
| **Di chuyển Strafe (3D Movement)** | `W - A - S - D` | 4 Phím Mũi tên (`↑ ↓ ← →`) | Di chuyển 360 độ khóa mặt theo hướng Camera |
| **Đánh Thường (Light Attack `L`)** | **Chuột Trái (`LMB`)** | Phím `J` | Spam liên tục, ngắt chiêu quái ở `Windup` |
| **Đánh Mạnh / Finisher (`H`)** | **Chuột Phải (`RMB`)** | Phím `K` | Lướt tới phá chiêu tuyệt đối (`Hyper Interrupt`) |
| **Lướt Né đòn (3D Dash)** | **Phím Cách (`Spacebar`)** | Phím `Shift Trái` | Lướt không tốn thể lực, có `0.15s` bất tử |
| **Đổi Vũ khí (Switch Strike)** | **Phím `Q` / `Tab`** | Cuộn chuột (`Scroll`) / Phím `E` | Kích hoạt đòn đánh chuyển đổi trong Cancel Window |
| **Xoay Camera & Hướng Nhìn Nhân Vật** | **Di chuyển Chuột (Mouse Move)** | — | **Xoay camera đồng thời xoay mặt nhân vật nhìn theo** |

### 2.2. Quy Tắc Xoay Mặt & Di Chuyển Khóa Theo Hướng Camera (Camera-Facing Strafe Mechanics)
- **Đồng Bộ Xoay Thân (Character Yaw = Camera Yaw)**: Khi di chuột xoay camera, trục $Y$ của nhân vật **lập tức xoay đồng bộ theo hướng nhìn của Camera / Tâm ngắm Crosshair**. Nhân vật luôn nhìn thẳng vào mục tiêu mà người chơi đang ngắm.
- **Hệ Thống Di Chuyển Strafe 3D**:
  - Phím `W`: Nhân vật tiến thẳng về phía trước theo hướng nhìn của Camera (mặt nhìn thẳng).
  - Phím `S`: Nhân vật đi lùi (*Backstep*), mắt và thân vẫn giữ nguyên hướng nhìn về phía trước của Camera.
  - Phím `D`: Nhân vật bước ngang sang phải (*Strafe Right*), thân vẫn hướng về phía trước.
  - Phím `A`: Nhân vật bước ngang sang trái (*Strafe Left*), thân vẫn hướng về phía trước.

---

## 3. Detailed Rules (Quy tắc Chi tiết)

### 3.1. Cấu hình Trang bị & Tiến trình Đổi Vũ khí (Dynamic Loadout & Switch Strike)
- **Slot Vũ khí trong Run**:
  - **Khởi đầu**: Người chơi xuất phát với **1 vũ khí 3D cơ bản** tại `Slot 1`.
  - **Trong Run**: Khi bước qua Bệ thờ Thần Khí 3D (*Mythic Armory Shrine*) tại Wave 3, người chơi mở khóa thêm `Slot 2` để trang bị vũ khí thứ 2, kích hoạt cơ chế chiến đấu kết hợp song vũ khí.
- **Phím chuyển đổi (`[SWAP]` - Phím `Q` / `Tab`)**: Hoán đổi tức thì giữa Slot 1 và Slot 2.
- **Cơ chế Switch Strike (Đòn Chuyển Đổi Tức Thì)**:
  - Trong giai đoạn `Active` và `Recovery` của bất kỳ đòn đánh nào, cửa sổ **Cancel Window** mở ra: nhấn `[SWAP]` sẽ lập tức hủy động tác cũ và tự động tung ra đòn đánh đặc biệt **Switch Strike** của vũ khí mới mà không bị khựng lại.

```text
[ Đòn đánh Vũ khí A: Active/Recovery ] ──(Nhấn [SWAP] trong Cancel Window)──► [ Tức thì thi triển Switch Strike Vũ khí B ]
```

---

### 3.2. Hệ thống Né đòn Vô tận & Hủy Động tác (Stamina-less 3D Dash & I-Frames)
- **Không tốn Thể lực (Stamina-free)**: Người chơi có thể tự do Dash 3D liên tục theo hướng di chuyển phím bấm ($X, Z$) bằng phím `Spacebar`.
- **Thông số Dash 3D**:
  - **Thời gian lướt**: `0.3s`.
  - **Cooldown đệm giữa 2 lần Dash**: `0.1s` (ngăn xung đột animation 3D).
  - **Khung bất tử (I-frames)**: `0.15s` đầu tiên kể từ lúc bắt đầu Dash, nhân vật miễn nhiễm hoàn toàn với mọi sát thương và khống chế.
- **Dash Recovery Cancel**: Cú Dash có thể hủy tức thì giai đoạn `Recovery` của mọi đòn đánh thường, đòn gồng (Hold) hay Switch Strike.

---

### 3.3. Căn Chỉnh Vùng Đánh, Hướng Mặt & Chuỗi Combo 4 Nhịp (4-Pip Pipeline)

#### A. Căn Chính Diện Vệt Chém Theo Hướng Nhìn Camera (Center-Forward Slash Arc)
1. **Định Hướng Đòn Đánh Tức Thì**: Vì nhân vật luôn nhìn theo Camera, mọi cú nhấp `LMB` hoặc `RMB` đều tung ra chính xác vào vị trí tâm ngắm Crosshair mà không có độ trễ xoay người.
2. **Căn Chính Diện Vệt Chém $180^\circ$**: Vệt chém 3D (*Slash Arc*) và chùm hạt *Tip Splash* quét hình vòng cung $180^\circ$ đối xứng ngay chính giữa trước ngực nhân vật, không bị lệch sang bên cánh tay.

#### B. Chuỗi Đòn Đánh 4 Nhịp & Cơ Chế Bùng Nổ Finisher (4-Pip Combo Pipeline)
- **Nhấp `LMB` lần 1, 2, 3**:
  - Phát ra vệt chém mỏng kèm chùm hạt **Particle mờ dần dạng Splash (Fading Arc Splash VFX)** tại đầu kiếm.
  - Tích sáng lần lượt **Pip 1 $\to$ Pip 2 $\to$ Pip 3** trên thanh HUD.
  - Ngắt đòn quái thường đang ở phase `Windup` (*Soft Interrupt*).
- **Trạng Thái Báo Hiệu Đòn Kết Liễu (3/4 Pre-Finisher Flare)**:
  - Ngay khi đạt nhát thứ 3, Ô số 4 (**💥 FINISHER**) trên HUD sẽ **nhấp nháy dồn dập (tần số `0.18s`)** chuyển đổi giữa màu Vàng kim và Hồng Neon kèm dòng chữ cảnh báo: `⚡ ĐÒN KẾ: FINISHER! (3/4)`.
- **Nhấp Lần Thứ 4 (hoặc bấm `RMB` bất kỳ lúc nào)**:
  - Ô số 4 bùng nổ toàn diện trên HUD.
  - Nhân vật **lướt vọt tới $3.8\text{m}$ theo hướng Camera**, tung cú dậm nổ **Mega Shockwave Splash bán kính $R = 3.0\text{m}$** chấn động mặt đất.
  - **Phá đòn quái 100% (Hyper Interrupt)** và hất tung kẻ địch lên trục $Y \ge 2.5\text{m}$.
  - **Thi Triển Đầy Đủ Trong Không Khí (Air Execution)**: Đòn Finisher thứ 4 thi triển trọn vẹn toàn bộ animation lướt $3.8\text{m}$, hiệu ứng chấn động $3.0\text{m}$ và âm thanh nổ ngay cả khi chém vào không khí mà không có mục tiêu.

---

### 3.4. Hệ thống Khóa Mục Tiêu 3D Tự Động & Khung Ngắm 8 Góc (3D Soft-Lock & 8-Corner Reticle)
- **Cơ chế 3D Soft-Lock**: Khi người chơi tung đòn tấn công gần quái trong bán kính $5\text{m}$ và góc nón $60^\circ$ theo hướng Camera, hệ thống tự động bám dính hitbox vào con quái gần nhất.
- **Khung Ngắm Trực Quan (8-Corner Reticle)**:
  - Khung ngắm hình vuông viền 8 góc (8-Corner Reticle Bracket) hiển thị dạng World-Space UI nhấp nháy ôm sát quanh thân mô hình 3D của con quái đang được chọn (tự động căn chỉnh theo chiều cao $1.8\text{m}$, $3.0\text{m}$ hoặc $5.0\text{m}$).

---

### 3.5. Hệ thống Máu, Hồi Phục & Ngừng Đánh Khi Thất Bại (Defeat Ceasefire)
- **Thanh Máu Người Chơi**: Gồm **10 Quả Tim (10 Hearts = 100 HP)**, mỗi quả tim tương ứng 10 HP.
- **Hồi Máu Đòn Kết Liễu (Execution Health Drop)**:
  - Khi tiêu diệt quái bằng đòn Kết liễu Đánh Mạnh (`RMB` hoặc `Switch Strike`), quái rơi ra **Ngọc Máu 3D (Blood Orb 3D Mesh/Particle)** tự động bay theo đường cong vật lý về phía người chơi để hồi ngay **+1 Quả Tim (+10 HP)**.
- **Ngừng Tấn Công Tuyệt Đối Khi Thua (Defeat Ceasefire & Cleanup)**:
  - Khi nhân vật hết 10 Tim Máu (Player Death): Toàn bộ quái thường và Trùm Goliath **lập tức ngừng tấn công**, tắt toàn bộ tia laser ngắm bắn của Skeleton Archer, và dọn sạch mọi mũi tên đang bay trên không gian.

---

### 3.6. Chi tiết 3 Archetype Vũ khí 3D Khởi đầu (MVP 3D Weapon Roster)

#### 🗡️ Vũ khí 1: Song Đoản Kiếm 3D (Dual Daggers)
- **Định vị**: Tốc độ đánh cực cao, tầm gần, tích tầng Bleed và Crit.
- **Moveset**:
  - `LMB - LMB - LMB - LMB`: 4 đòn chém 3D nhanh, đòn thứ 4 lướt $3.8\text{m}$ xoay kiếm kích nổ Bleed.
  - `LMB - RMB`: Xoay kiếm hất tung quái thường lên trục $Y \ge 2.5\text{m}$ (Airborne Launcher).
  - `Spacebar + LMB`: Lướt xuyên mục tiêu, làm chậm quái 30% trong 2s.
- **Switch Strike (*Shadow Step*)**: Dịch chuyển chớp nhoáng (Blink 3D) ra sau lưng mục tiêu và tung 3 nhát chém dồn dập.

#### 🪓 Vũ khí 2: Đại Kiếm 3D (Greatsword)
- **Định vị**: Tốc độ chậm, tầm quét rộng, sát thương Stagger cực lớn, sở hữu **Super Armor** khi vung kiếm.
- **Moveset**:
  - `LMB - LMB - LMB`: 3 nhát chém vòng cung 180 độ phía trước, Knockback quái.
  - `LMB - LMB - RMB` / `Finisher`: Nhảy lướt tới $3.8\text{m}$ bổ thẳng kiếm xuống đất, tạo Mega Shockwave Splash $R = 3.0\text{m}$ phá tan đòn quái (Hyper Interrupt).
  - `LMB - RMB`: Bổ kiếm gây Choáng (Stun) trong 1.5s.
  - `Hold RMB`: Tích lực tối đa 1.0s, dậm kiếm tạo 3 làn sóng chấn động 3D bán kính $8\text{m}$.
- **Switch Strike (*Meteor Crash*)**: Nhảy vọt lên cao $3\text{m}$ trên trục $Y$ và bổ thẳng kiếm xuống đất, tạo sóng xung kích hất văng quái.

#### 🔮 Vũ khí 3: Liềm Tử Thần 3D (Death Scythe)
- **Định vị**: Tầm đánh trung bình (360 độ quanh thân), gom quái diện rộng (Vortex Pull), sát thương Ma thuật diện rộng (Magic AOE).
- **Moveset**:
  - `LMB - LMB - LMB`: Xoay liềm tròn 3 nhát liên tiếp.
  - `LMB - LMB - RMB` / `Finisher`: Lướt xoay liềm gom toàn bộ quái và bẻ gãy đòn đánh (Hyper Interrupt).
  - `LMB - RMB`: Phóng luồng xoáy năng lượng hút toàn bộ quái trong bán kính $6\text{m}$ gom về một điểm phía trước mặt.
  - `Hold RMB`: Kích nổ bóng tối, tiêu diệt tức thì quái dưới 15% HP.
- **Switch Strike (*Soul Tether*)**: Phóng xích kéo bản thân lao vào tâm điểm bầy quái, làm chậm 50% trong 5m.

---

### 3.7. Roster Kích Thước Thực Thể 3D, Va Chạm Vật Lý & Trùm Goliath

| Thực thể | Chiều cao 3D ($H$) | Bán kính Collider ($R$) | Loại Collider 3D | Cơ chế Va chạm & Hành vi |
| :--- | :--- | :--- | :--- | :--- |
| **Nhân vật chính (Player)** | **$1.8\text{m}$** | $0.4\text{m}$ | `CharacterController 3D` | Luôn nhìn theo Camera; không đi xuyên quái |
| **Zombie Cận chiến (Melee Swarmer)** | **$1.8\text{m}$** | $0.4\text{m}$ | `CapsuleCollider 3D` | Đẩy nhau ra khi vây bắt, $30\text{ HP}$, ngắt ở Windup |
| **Zombie Cung thủ (Skeleton Archer)** | **$1.8\text{m}$** | $0.4\text{m}$ | `CapsuleCollider 3D` | Đứng xa $8-12\text{m}$, ngắm tia đỏ $1.2\text{s}$, bắn tên 3D |
| **Quái Hộ Vệ (Shielded Meat Shield)** | **$3.0\text{m}$** | $0.9\text{m}$ | `CapsuleCollider 3D` | Khối lượng lớn, cản đường che chắn, $160\text{ HP}$ |
| **Trùm Tối Thượng (Boss Goliath)** | **$5.0\text{m}$** | $1.8\text{m}$ | `CapsuleCollider 3D` | Khổng lồ, $1200\text{ HP}$, $300\text{ Poise}$, Decal đỏ $0.8\text{s}$ |

---

### 3.8. Cấu trúc Đấu trường 3D 4 Đợt Quái & Địa Hình (3D Arena Geometry & 4-Wave Flow)

#### A. Cấu Trúc Không Gian & Địa Hình Đấu Trường:
- **Kích Thước**: Đấu trường hình tròn bán kính **$R = 25\text{m}$** (Đường kính $50\text{m}$) rực rỡ cỏ xanh dưới ánh nắng ban ngày.
- **Chướng Ngại Vật Chiến Thuật**: Bố trí **4 Cột Đá Cổ Thụ (Ancient Stone Pillars, $H = 4\text{m}, R = 1.2\text{m}$)** xung quanh tâm đấu trường có hiệu ứng làm mờ (*Dither Transparency*) khi che góc nhìn camera, làm vật cản tự nhiên để người chơi né tránh mũi tên của Skeleton Archer.
- **Ranh Giới**: Rào chắn ánh sáng thần tiên (Mystic Light Boundary) bao quanh mép $25\text{m}$.

#### B. Tiến Trình 4 Đợt Quái:
* **Wave 1 (Khởi động)**: 8 Zombie Cận chiến 3D ($1.8\text{m}$).
* **Wave 2 (Tập né tránh)**: 6 Zombie Cận chiến + 3 Zombie Cung thủ 3D ($1.8\text{m}$).
* **Wave 3 (Đột phá vòng vây)**: 2 Quái Hộ Vệ 3D ($3.0\text{m}$) + 4 Cận chiến + 2 Cung thủ 3D.
* 🏛️ **Điểm chạm Bệ thờ Thần Khí 3D (Wave 3 Milestone Shrine)**: Xuất hiện giữa đấu trường 3D sau khi dọn xong Wave 3. Người chơi chọn xem 1 Rewarded Ad để nhận Vũ khí Thần Thoại 3D (Mythic Tier) vào `Slot 2`.
* **Wave 4 (Trùm Tối Thượng 3D — Final Boss Goliath $5.0\text{m}$)**: Boss khổng lồ xuất hiện cùng cơ chế Rotation Lock và Decal 3D.

#### C. Điều Kiện Thắng / Thua (Win / Defeat Flow):
- **Thất Bại (Defeat)**: Khi hết 10 Tim Máu, kích hoạt Ceasefire ngừng đánh, mở khóa con trỏ chuột và hiển thị màn hình Thất Bại + nút Chơi Lại.
- **Chiến Thắng (Victory)**: Khi tiêu diệt xong Boss Goliath ở Wave 4, mở khóa con trỏ chuột, bùng nổ pháo hoa và âm nhạc vinh danh.

---

### 3.9. Giao diện Chiến đấu & Quản Lý Con Trỏ Chuột (HUD & Cursor Management)

```text
                                    + (Tâm ngắm Crosshair chính giữa)

       [ ❤️❤️❤️❤️❤️❤️❤️❤️❤️❤️ ] (10 Tim Máu)   ──► [ ●  ●  ●  💥 ] (4-Pip Combo Meter)
   ┌─────────────────────────────────────────────────────────────┐
   │      [ Slot 1: Daggers (Q) ]     [ Slot 2: Greatsword ]     │  (Khay Vũ Khí Chính Giữa Mép Dưới)
   └─────────────────────────────────────────────────────────────┘
```

- **Tâm Ngắm Crosshair**: Dấu cộng nhỏ (`+`) cố định tại chính giữa màn hình, định hướng tầm nhìn và đòn đánh.
- **Khay Vũ Khí & 10 Tim Máu**: Nằm ở **Chính Giữa Mép Dưới màn hình (Bottom-Center)**.
- **Thanh Điểm Sáng Combo (4-Pip Combo Pipeline Meter)**:
  - Pip 1, 2, 3 sáng dần khi đánh `LMB`.
  - Đạt 3 nhát: Pip 4 nhấp nháy vàng/hồng neon (`0.18s`) báo hiệu `⚡ ĐÒN KẾ: FINISHER! (3/4)`.
  - Nhát thứ 4: Pip 4 bùng nổ, nhân vật lướt $3.8\text{m}$ xả cú dậm đất $R = 3.0\text{m}$.
- **Tự Động Mở Khóa & Hiện Chuột Khi Mở Menu / Modal**:
  - Khi mở **Bệ Thờ Thần Khí (Wave 3)**, **Màn hình Thắng** hoặc **Màn hình Thất Bại**, hệ thống tự động giải phóng khóa chuột (`PointerLock = false`), hiển thị con trỏ chuột hệ điều hành, ẩn tâm ngắm và cố định góc quay camera để người chơi dễ dàng click thao tác.

---

### 3.10. Hệ thống Camera 3D Hành động Góc nhìn Thứ Ba (Nightfall-Style Dynamic 3D TPV Camera)

```text
┌──────────────────────────────────────────────────────────────────────────┐
│ [TẦM NHÌN TRÊN - PHẢI: BẦY QUÁI 3D, BOSS 5.0m, 3D TELEGRAPH RED DECALS]  │
│                                                                          │
│  Không gian rộng thoáng để quan sát toàn bộ bầy quái 3D và Boss khổng lồ  │
│                                                                          │
│  [MÔ HÌNH NHÂN VẬT 3D (1.8m) - LUÔN NHÌN THEO CAMERA]                   │
│  - Thấy trọn từ đỉnh đầu đến bàn chân chạm đất 3D                        │
│  - Neo tại góc Dưới - Trái: Screen Offset X = -20%, Y = -25%             │
└──────────────────────────────────────────────────────────────────────────┘
```

#### A. Căn Khung & Điểm Neo Nhân vật 3D (3D Screen Framing & Offsets)
- **Kiểu góc nhìn**: Action Over-the-Shoulder / Elevated TPV 3D phía sau lưng nhân vật.
- **Góc nghiêng (Pitch Angle)**: $28^\circ$ chúc nhẹ từ trên cao xuống.
- **Khoảng cách & Độ cao**: Khoảng cách `Distance = 5.8m`, Độ cao `Height = 2.4m` (căn chuẩn cho nhân vật $1.8\text{m}$).
- **Vị trí Nhân vật trên Màn hình**:
  - Trục ngang ($X$): Lệch sang trái `-20%` so với tâm màn hình (`Viewport X = 0.40`).
  - Trục dọc ($Y$): Lệch xuống dưới `-25%` so với tâm màn hình (`Viewport Y = 0.30`).
- **Quy tắc Thân thể Trọn vẹn**: Luôn hiển thị trọn vẹn từ đỉnh đầu đến đôi bàn chân mô hình 3D ($1.8\text{m}$) nhân vật chạm sàn.

#### B. Cơ chế Tốc độ & FOV Động khi Chạy (Sprint Dynamic FOV & Trailing)
- **Base FOV (Idle / Walk / Combat)**: `62°` — Chi tiết mô hình 3D rõ ràng, đầm tay.
- **Sprint FOV (Khi Chạy Nhanh)**: Mở rộng mượt mà lên `78°` ($+16^\circ$) trong `0.25s` kết hợp camera kéo lùi nhẹ `0.4m` (*Camera Trailing Drag*).
- **Thu hồi FOV**: Khi dừng chạy hoặc tấn công, FOV thu về cơ bản trong `0.30s`.

#### C. Chống Rung Mắt & Xử lý Vật cản (3D Smoothing & Occlusion)
- **Làm mềm Chuyển động**: Camera 3D bám theo nhân vật với hệ số trễ `Damping = 0.12s`.
- **Làm mờ Vật cản 3D**: Tự động làm trong suốt (*Dither Fade*) các cột đá che khuất giữa Camera và Nhân vật.

---

### 3.11. Hệ thống Cấp bậc Vũ khí & Điểm Chạm Quảng Cáo (Tier System & Rewarded Ads)

| Thuộc tính | Cấp Tiêu Chuẩn (Standard Tier) | Cấp Thần Thoại / Siêu Cấp (Mythic Tier) |
| :--- | :--- | :--- |
| **Nguồn gốc** | Vũ khí 3D mặc định ban đầu | Nhặt tại Bệ thờ Thần Khí 3D (Xem Rewarded Ad) |
| **Hệ số Sát thương** | $1.0\times$ (Cơ bản) | **$2.5\times - 3.0\times$** (Burst Damage áp đảo) |
| **Tầm đánh / Hitbox 3D**| Chuẩn ($100\%$) | Rộng hơn $+40\%$ ($140\%$ diện tích quét 3D) |
| **Hiệu ứng Hình ảnh (3D VFX)** | Vệt chém 3D + Tip Splash chuẩn | Vệt sáng Vàng Kim Neon rực rỡ + Hạt nổ sao lấp lánh (Starburst Particles) |
| **Hiệu ứng Âm thanh (SFX)** | Tiếng chém kim loại chuẩn | Tiếng Bass trầm bổng, đanh thép, vang vọng không gian |

---

## 4. Formulas (Công thức Toán học)

### 4.1. Công thức Tính Sát Thương Thực Nhận (Final Damage)
$$\text{FinalDamage} = \max\left(1, (\text{BaseWeaponDamage} \times \text{TierMultiplier} \times \text{MotionValue} + \text{FlatBonus}) \times (1 - \text{DamageReduction}) \times \text{CritMultiplier} \times \text{GroggyMultiplier}\right)$$

### 4.2. Công thức Trừ Thanh Poise (Poise Damage)
$$\text{PoiseDamage} = \text{BasePoiseDamage} \times \text{PoiseMotionValue} \times (1 + \text{PoiseBonusRate})$$
- Đòn Light Attack (`LMB`): $\text{PoiseMotionValue} = 1.0$.
- Đòn Heavy Attack / Finisher (`RMB` / Nhát thứ 4): $\text{PoiseMotionValue} = 3.5$.
- Greatsword: $\text{BasePoiseDamage} = 60$.
- Scythe: $\text{BasePoiseDamage} = 25$.
- Daggers: $\text{BasePoiseDamage} = 10$.

### 4.3. Công thức Hiệu ứng Chảy Máu (Bleed Burst)
- Mỗi tầng Bleed duy trì trong $5.0\text{s}$, tối đa $5\text{ tầng}$.
- Sát thương mỗi giây = $0.15 \times \text{BaseWeaponDamage} \times \text{TierMultiplier} \times \text{Stacks}$.
- Khi đạt đủ 5 tầng: Kích nổ **Bleed Burst**, gây ngay $1.5 \times \text{BaseWeaponDamage} \times \text{TierMultiplier} \times 5$ sát thương bỏ qua phòng thủ.

---

## 5. Edge Cases (Trường hợp Biên & Xử lý Ngoại lệ 3D)

| Mã ngoại lệ | Tình huống phát sinh | Cách xử lý hệ thống (3D Design Resolution) |
| :--- | :--- | :--- |
| `EC-01` | Người chơi nhấn Spacebar Dash trong khi đang bị Choáng hoặc hất tung trên không ($Y > 0$). | **Khóa Dash**: Trạng thái khống chế cứng vô hiệu hóa Input di chuyển. |
| `EC-02` | Người chơi bấm Spacebar Dash liên tục (Spamming Dash 3D). | **Hàng đợi mượt mà (Input Buffer)**: Nhận lệnh Dash tiếp theo và thực hiện ngay sau khi kết thúc khoảng đệm `0.1s`. |
| `EC-03` | Người chơi bấm `LMB`/`RMB` khi không có quái trong tầm 3D Soft-Lock. | **Đánh theo hướng Camera / Tâm ngắm**: Nhân vật tấn công thẳng theo vector hướng nhìn Camera trên mặt phẳng $(X, Z)$. |
| `EC-04` | Quái thường đang ở phase `Active` bị đánh trúng bởi đòn `Heavy Finisher (RMB)`. | **Hyper Interrupt Ưu tiên**: Đòn `RMB` phá vỡ tức thì phase `Active` của quái thường, hủy sát thương mà quái chuẩn bị gây ra. |
| `EC-05` | Quái thường đang ở phase `Active` bị đánh trúng bởi đòn `Light Attack (LMB)`. | **Không ngắt đòn**: Quái tiếp tục hoàn thành cú chém gây sát thương; nhân vật nhận sát thương trừ khi kích hoạt I-frame của Dash. |
| `EC-06` | Boss $5.0\text{m}$ đang vung đòn cố định thì người chơi Dash 3D ra sau lưng Boss. | **Duy trì Khóa Hướng**: Boss tiếp tục đánh vào vùng báo nguy hiểm cũ trước mặt; Collider 3D chỉ gây sát thương phía trước, không gây sát thương sau lưng. |
| `EC-07` | Camera 3D chạm sát vào Cột Đá Đấu Trường. | **Spherecast Collision 3D**: Camera tự động trượt dọc theo mặt phẳng góc cột và kích hoạt Dither Fade cho bề mặt cột đá 3D. |
| `EC-08` | Lỗi mạng / Không tải được quảng cáo khi mở Bệ thờ Thần Khí. | **Fallback Graceful**: Tự động cấp phiên bản vũ khí Thần Khí miễn phí hoặc bùa lợi tạm thời để không làm gián đoạn trải nghiệm. |
| `EC-09` | Người chơi mở Modal giao diện trong khi chuột đang bị khóa. | **Auto Pointer Unlock**: Ngay lập tức thoát trạng thái Pointer Lock, hiện con trỏ chuột hệ thống để tương tác UI. |

---

## 6. Dependencies (Các Phụ thuộc Hệ thống 3D)

| Hệ thống phụ thuộc | Chiều tương tác | Dữ liệu & Sự kiện trao đổi |
| :--- | :--- | :--- |
| **PC Input & PointerLock Manager** | 2 chiều | Xử lý `WASD` Strafe theo Camera, `LMB`, `RMB`, `Spacebar`, `Q`, đồng bộ góc xoay nhân vật theo Camera. |
| **Cinemachine 3D Camera Manager** | 2 chiều | Quản lý FOV Lerp, Screen Offsets 3D, Follow Target 3D, Damping, Camera Shake Impulse. |
| **Combat HUD & Combo Pipeline UI** | 2 chiều | Hiển thị 10 Tim Máu, Tâm ngắm Crosshair, 4-Pip Combo Meter nhấp nháy, 8-Corner Reticle. |
| **3D Animation Controller (Mecanim)** | 2 chiều | Quản lý 3D Animation Events (`CancelWindowOpen`, `IFrameStart`, `IFrameEnd`, `HitboxOpen`, `HyperInterruptTrigger`). |
| **3D VFX Particle Manager** | Gửi | Kích hoạt Tip Splash VFX cho `LMB`, Mega Shockwave Splash VFX cho `RMB` ($R=3.0\text{m}$), vệt chém Slash Trails. |
| **3D Wave Spawner & Arena Manager** | 2 chiều | Quản lý tiến trình 4 Wave quái 3D, kích hoạt Bệ thờ Thần Khí 3D tại Wave 3, kích hoạt Ceasefire khi thua. |
| **Boss 3D Decal Projector & Telegraph** | Gửi | Kích hoạt Decal 3D vùng đỏ trên mặt đất ($0.8\text{s}$), khóa góc xoay (`LockRotationEvent`), kích hoạt Hitbox 3D. |
| **3D Hitbox & Hurtbox System** | 2 chiều | Tính toán va chạm 3D (`Physics.OverlapSphere/Box`), kiểm tra I-frames và kích hoạt Ngắt chiêu quái thường. |
| **Ad Mediation & Monetization** | 2 chiều | Gửi yêu cầu hiển thị Rewarded Ad, nhận callback `OnAdRewarded` để mở khóa Mythic Weapon 3D vào Slot 2. |
| **Audio Manager (Web Audio / FMOD)** | Gửi | Phát Upbeat Anime Action BGM 130 BPM, Procedural SFX chém kiếm, dậm đất, nhặt tim. |

---

## 7. Tuning Knobs (Các Biến Cân Bằng 3D)

| Tên biến | Mô tả | Giá trị mặc định | Khoảng an toàn |
| :--- | :--- | :--- | :--- |
| `Arena_Radius` | Bán kính đấu trường 3D | `25.0m` | `20.0m - 35.0m` |
| `Player_Height` | Chiều cao mô hình 3D nhân vật chính | `1.8m` | `1.7m - 1.9m` |
| `Shield_Brute_Height` | Chiều cao mô hình 3D Quái Hộ Vệ | `3.0m` | `2.5m - 3.5m` |
| `Boss_Goliath_Height` | Chiều cao mô hình 3D Trùm Tối Thượng | `5.0m` | `4.5m - 6.0m` |
| `Player_Max_Hearts` | Số lượng tim máu tối đa của người chơi | `10` | `5 - 20` |
| `Finisher_Lunge_Distance` | Khoảng cách lướt vọt của đòn Finisher thứ 4 | `3.8m` | `3.0m - 5.0m` |
| `Finisher_Shockwave_Radius`| Bán kính nổ Mega Shockwave của Finisher | `3.0m` | `2.0m - 4.5m` |
| `Finisher_Flash_Rate` | Tần số nhấp nháy báo hiệu của Pip 4 | `0.18s` | `0.10s - 0.25s` |
| `Combo_Pip_Timeout` | Thời gian chờ trước khi tắt các điểm sáng Combo | `1.0s` | `0.6s - 1.8s` |
| `SoftLock_Radius` | Bán kính 3D tự động bắt dính mục tiêu | `5.0m` | `3.0m - 8.0m` |
| `SoftLock_Cone_Angle` | Góc nón 3D bắt dính mục tiêu phía trước mặt | `60.0°` | `45.0° - 90.0°` |
| `Skeleton_Aim_Duration` | Thời gian giương cung ngắm bắn của Skeleton Archer | `1.2s` | `0.8s - 2.0s` |
| `Cam_Distance` | Khoảng cách 3D từ Camera đến nhân vật | `5.8m` | `4.5m - 7.5m` |
| `Cam_Height` | Độ cao trục Y 3D của Camera | `2.4m` | `1.8m - 3.2m` |
| `Cam_Pitch_Angle` | Góc nghiêng 3D chúc xuống của Camera | `28.0°` | `20.0° - 35.0°` |
| `Cam_Base_FOV` | FOV cơ bản ở trạng thái nghỉ / combat | `62.0°` | `55.0° - 65.0°` |
| `Cam_Sprint_FOV` | FOV mở rộng khi chạy nhanh | `78.0°` | `70.0° - 85.0°` |
| `Dash_Duration` | Thời gian lướt của một cú Dash 3D | `0.3s` | `0.2s - 0.45s` |
| `Dash_IFrame_Duration` | Thời gian bất tử trong cú Dash | `0.15s` | `0.1s - 0.25s` |
| `Dash_Cooldown_Buffer` | Khoảng thời gian đệm giữa 2 cú Dash | `0.1s` | `0.05s - 0.2s` |
| `Mythic_Damage_Multiplier` | Hệ số sát thương của Vũ khí Thần Thoại | `2.5x` | `2.0x - 3.5x` |
| `Mythic_Hitbox_Scale` | Tỉ lệ mở rộng vùng đánh 3D của Vũ khí Thần Thoại | `1.4x` | `1.2x - 1.8x` |
| `Boss_Telegraph_Duration` | Thời gian hiển thị cảnh báo Decal đỏ của Boss | `0.8s` | `0.5s - 1.5s` |
| `Heavy_Poise_Multiplier` | Hệ số sát thương Poise của đòn Đánh Mạnh | `3.5x` | `2.5x - 5.0x` |
| `Hitstop_Light_Duration` | Thời gian dừng hình đòn nhẹ | `0.04s` | `0.02s - 0.06s` |
| `Hitstop_Heavy_Duration` | Thời gian dừng hình đòn nặng | `0.12s` | `0.08s - 0.18s` |
| `Poise_Recovery_Delay` | Thời gian chờ trước khi Poise quái tự hồi | `4.0s` | `2.0s - 7.0s` |

---

## 8. Acceptance Criteria (Tiêu chí Chấp nhận 3D Master)

- [ ] **AC-01 (Pure 3D Environment & Physical Collisions)**: Toàn bộ game chạy trên không gian 3D thực thể với CharacterController 3D, CapsuleCollider 3D cho toàn bộ thực thể. Người chơi không thể đi xuyên qua quái hoặc Trùm Goliath; quái tự động giữ khoảng cách không xếp chồng lên nhau.
- [ ] **AC-02 (Camera-Locked Facing & Strafe Movement)**: Khi di chuột xoay camera, thân và mặt nhân vật **lập tức xoay đồng bộ theo hướng nhìn của Camera / Tâm ngắm**; `W` đi tiến thẳng, `S` đi lùi (mặt vẫn nhìn về phía trước), `A`/`D` bước ngang strafe.
- [ ] **AC-03 (Center Crosshair & PointerLock)**: Tâm ngắm dấu cộng (`+`) hiển thị chính giữa màn hình; chuột hệ thống được ẩn khi chơi và tự động hiện lại khi mở Menu/Modal Thắng/Thua.
- [ ] **AC-04 (Attack Alignment & Center-Forward Slash)**: Đòn đánh vung ra chuẩn xác vào hướng Camera/Tâm ngắm; vệt chém $180^\circ$ và Tip Splash quét chính diện trước ngực nhân vật.
- [ ] **AC-05 (4-Pip Combo Pipeline & Flashing Finisher)**: Đánh `LMB` tích sáng Pip 1 $\to$ 2 $\to$ 3; đạt 3 nhát Pip 4 nhấp nháy dồn dập `0.18s` báo hiệu `⚡ ĐÒN KẾ: FINISHER! (3/4)`.
- [ ] **AC-06 (4th Finisher Air Execution)**: Nhát đánh thứ 4 lướt vọt $3.8\text{m}$ tung Mega Shockwave Splash $R = 3.0\text{m}$ dậm đất chấn động, phá đòn quái 100% — thi triển trọn vẹn ngay cả khi chém vào không khí.
- [ ] **AC-07 (Continuous Flow & 3D Dash)**: Spacebar Dash liên tục không tốn thể lực; `0.15s` đầu né được mọi sát thương; hủy được động tác khựng Recovery.
- [ ] **AC-08 (Light Attack Soft Interrupt)**: Đòn đánh thường `LMB` đánh trúng quái thường khi quái đang ở phase `Windup` lập tức hủy hoạt ảnh tấn công của quái; đánh trúng khi quái đang ở phase `Active` không làm ngắt đòn đánh của quái.
- [ ] **AC-09 (3D Soft-Lock & 8-Corner Reticle)**: Khi tấn công gần quái trong phạm vi $5\text{m}$, nhân vật tự động bám dính mục tiêu và hiển thị khung ngắm 8 góc ôm quanh thân quái.
- [ ] **AC-10 (Execution Health Recovery)**: Tiêu diệt quái bằng đòn Finisher `RMB` hoặc `Switch Strike` rơi ra Ngọc Máu 3D tự động bay về người chơi hồi ngay $+1$ Tim Máu.
- [ ] **AC-11 (Defeat Ceasefire)**: Khi hết 10 Tim Máu, toàn bộ quái và Trùm lập tức ngừng đánh, tắt tia laser ngắm bắn và dọn sạch mũi tên trên không.
- [ ] **AC-12 (Skeleton Archer 3D Behavior)**: Skeleton Archer ($1.8\text{m}$) đứng từ xa giương cung ngắm bắn tia đỏ trong `1.2s` trước khi phóng tên vật lý 3D; người chơi có thể dùng Dash né tên hoặc áp sát ngắt đòn.
- [ ] **AC-13 (Heavy Meat Shield 3D Behavior)**: Quái Hộ Vệ ($3.0\text{m}$) có kích thước mô hình 3D to lớn, máu dày, đóng vai trò cản đường che chắn cho Skeleton Archer phía sau.
- [ ] **AC-14 (4-Wave 3D Arena Flow & Win/Defeat)**: Đấu trường 3D lần lượt vượt qua 3 đợt quái, mở Bệ thờ Thần Khí 3D tại Wave 3, và spawn Boss Trùm 3D Goliath ($5.0\text{m}$) tại Wave 4.
- [ ] **AC-15 (Boss 3D Rotation Lock & Decal Telegraph)**: Khi Boss $5.0\text{m}$ bắt đầu phase `Windup`, góc xoay của Boss bị khóa cố định và hiển thị Decal 3D cảnh báo đỏ trên mặt đất trong $0.8\text{s}$ khớp với phạm vi Collider 3D sát thương.
- [ ] **AC-16 (Nightfall 3D Camera Framing)**: Nhân vật ($1.8\text{m}$) luôn nằm ở góc Dưới - Trái màn hình (`Viewport X = 0.40, Y = 0.30`), toàn bộ cơ thể và bàn chân mô hình 3D chạm đất hiển thị đầy đủ, không gian phía Trên - Phải thông thoáng để quan sát bầy quái 3D và Boss $5.0\text{m}$.
- [ ] **AC-17 (Dynamic FOV on Sprint)**: Khi nhân vật chuyển sang trạng thái chạy nhanh, FOV chuyển đổi mượt mà từ `62°` lên `78°` trong `0.25s` và kéo lùi camera `0.4m` tạo cảm giác tăng tốc xé gió.
- [ ] **AC-18 (Switch Strike 3D)**: Khi nhấn `[SWAP]` trong cửa sổ Cancel Window của đòn đánh bất kỳ (khi đã mở khóa Slot 2), nhân vật hủy động tác cũ và tung đòn Switch Strike của vũ khí mới tức thì.
- [ ] **AC-19 (Mythic 3D Weapon & Ad-Reward)**: Sau khi hoàn thành xem Rewarded Ad tại Bệ thờ Thần Khí 3D, nhân vật nhận được vũ khí Mythic 3D vào Slot 2 với sát thương tăng tối thiểu `2.5x`, phạm vi mở rộng `1.4x`, và hiệu ứng VFX/SFX siêu cấp.
- [ ] **AC-20 (Dither Pillar Transparency)**: 4 Cột đá ($H = 4\text{m}$) tự động làm mờ trong suốt khi nằm giữa vị trí Camera và Nhân vật để không che khuất tầm nhìn.
