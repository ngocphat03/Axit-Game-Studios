# Quy tắc Lập trình Gameplay (Gameplay Code Rules)

Áp dụng cho tất cả mã nguồn gameplay trong `src/gameplay/**` và các hệ thống gameplay Unity.

- **Hướng dữ liệu (Data-Driven)**: TẤT CẢ các giá trị gameplay (sát thương, tốc độ, máu, chỉ số) PHẢI đến từ cấu hình bên ngoài (`ScriptableObject`, JSON, config assets), TUYỆT ĐỐI KHÔNG hardcode.
- **Tính độc lập tốc độ khung hình**: Sử dụng `Time.deltaTime` (hoặc `Time.fixedDeltaTime` trong `FixedUpdate`) cho tất cả các phép tính phụ thuộc thời gian.
- **Decoupled UI**: KHÔNG tham chiếu trực tiếp đến code UI — sử dụng C# Events / Actions hoặc Unity Events để giao tiếp liên hệ thống.
- **Interface rõ ràng**: Mọi hệ thống gameplay phải triển khai một interface rõ ràng (ví dụ: `IDamageable`, `ICombatSystem`).
- **Máy trạng thái (State Machines)**: Phải có bảng chuyển trạng thái tường minh kèm tài liệu các trạng thái.
- **Khả năng kiểm thử (Testability)**: Tách biệt logic tính toán khỏi Presentation (`MonoBehaviour`) để có thể viết Unit test độc lập.
- **Không dùng Singleton tĩnh cho trạng thái game**: Ưu tiên Dependency Injection hoặc Service Locator / ScriptableObject Architecture.

## Ví dụ (C# / Unity)

**Đúng (Data-driven & Event-driven):**
```csharp
[SerializeField] private PlayerStatsConfig _statsConfig;

public void ApplyDamage(float rawDamage)
{
    float finalDamage = rawDamage * _statsConfig.DamageMultiplier;
    _currentHealth -= finalDamage;
    OnHealthChanged?.Invoke(_currentHealth, _statsConfig.MaxHealth);
}
```

**Sai (Hardcoded & Coupled):**
```csharp
public void ApplyDamage()
{
    _currentHealth -= 25.0f; // VI PHẠM: hardcode giá trị gameplay
    UIManager.Instance.HealthBar.SetHealth(_currentHealth); // VI PHẠM: couple trực tiếp UI qua Singleton
}
```
