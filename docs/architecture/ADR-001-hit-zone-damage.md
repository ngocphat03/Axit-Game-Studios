# ADR-001: Cấu hình hệ số nhân sát thương theo từng vùng trúng đòn (hit zone)

- Trạng thái: Accepted
- Ngày: 2026-08-09
- Phạm vi: QuickGun combat

## Bối cảnh (Context)

QuickGun hiện đã điều hướng va chạm của đạn qua `DamageableBodyPart`, nhưng mọi bộ phận cơ thể đều chuyển tiếp cùng một lượng sát thương như nhau. Character prefab có các collider tách biệt cho đầu (head), tóc (hair) và thân (body), do đó dữ liệu va chạm cần thiết cho các vùng trúng đòn có ý nghĩa đã tồn tại sẵn.

## Quyết định (Decision)

Mỗi `DamageableBodyPart` sở hữu một hệ số nhân sát thương kiểu serialized, không âm. Một class thuần túy `DamageCalculator` áp dụng hệ số nhân đó và chuyển đổi kết quả thành sát thương máu kiểu số nguyên (integer health damage). Các đòn đánh trúng hợp lệ gây ra ít nhất 1 sát thương, trong khi đầu vào không hợp lệ (không dương) sẽ gây 0 sát thương.

Character prefab dùng chung được cấu hình:

- Head (Đầu): 2x
- Hair (Tóc): 2x
- Body (Thân): 1x

Cả người chơi (player) và bot đều dùng chung prefab này, do đó quy tắc chiến đấu được áp dụng đối xứng như nhau.

## Hệ quả (Consequences)

- Game designer có thể tái cân bằng các vùng trúng đòn trong prefab mà không cần thay đổi code.
- Các quy tắc sát thương có thể kiểm thử được mà không cần load Unity scene.
- Đòn đánh chí mạng hiện dùng hiệu ứng lóe sáng (hit flash) có sẵn kèm combat log; có thể bổ sung tín hiệu UI/âm thanh chuyên dụng sau này mà không làm thay đổi hợp đồng tính sát thương.
