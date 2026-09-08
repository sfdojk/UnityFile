# **Bài cũ**
## 1. New Input System
- New Input System tách biệt thiết bị đầu vào khỏi logic code, giúp hỗ trợ nhiều loại thiết bị mà không sửa code
- 3 khái niệm cốt lõi:
    - **Input Action Asset:** file `.inputactions`, nơi định nghĩa các hành động như Move, Jump
    - **Action Maps:** nhóm các action lại (map player gồm move/jump, map UI gồm navigate/submit), có thể bật tắt từng map.
    - **Bindings:** gán phím/nút vật lý cho từng action (1 action có thể gán nhiều bindings)
- 2 cách đọc input trong code:
    - C1: dùng component `PlayerInput`, Unity tự gọi hàm `OnJump()`, `OnMove()`khi action xảy ra
    - C2: tự lấy action bằng code (`InputSystem.actions.FindAction("Move")`) rồi gọi `.ReadValue<T>()` mỗi frame (nếu gán là `value`) hoặc `.wasPressedThisFrame()` (nếu gán là `button`)
- Cách setup: Cài package → tạo Input Actions asset → thêm Action Map + Actions → gán binding → gắn `PlayerInput` component vào nhân vật → đọc input bằng 1 trong 2 cách trên

## 2. Rigidbody
- Rigidbody2D là component trong Unity dùng để mô phỏng vật lý 2D cho một GameObject

- Các thành phần chính
    - **Dynamic**: chịu ảnh hưởng đầy đủ của vật lý (trọng lực, lực, va chạm đẩy nhau...)
    - **Kinematic**: bạn tự điều khiển chuyển động bằng code, nhưng vẫn có thể va chạm và tác động lên vật Dynamic khác
    - **Static**: đứng yên hoàn toàn, dùng cho tường, nền đất...

- Các thuộc tính vật lý quan trọng

    - **Mass**: khối lượng, ảnh hưởng đến lực cần thiết để di chuyển vật
    - **Linear Drag / Angular Drag**: lực cản không khí làm chậm chuyển động thẳng và xoay
    - **Gravity Scale**: hệ số nhân trọng lực tác động lên vật (0 = không rơi)
    - **Collision Detection**: Discrete hay Continuous (Continuous dùng cho vật di chuyển nhanh để tránh xuyên qua vật khác)
    - **Constraints**: khóa vị trí (Freeze Position X/Y) hoặc khóa xoay (Freeze Rotation)
    - **Sleeping Mode**: tối ưu hiệu năng khi vật đứng yên lâu

- **Velocity**: Vận tốc hiện tại (vector 2 chiều: vx, vy)

Ví dụ:
```csharp
InputAction MoveAction;
InputAction JumpAction;

void Awake()
{
    MoveAction = InputSystem.actions.FindAction("Move");
    JumpAction = InputSystem.actions.FindAction("Jump");
}
// nếu MoveAction gán là value là Vector2 thì có thể tạo điều khiển như sau:
private void FixedUpdate()
{
    // di chuyển trái phải
    rb.linearVelocity = new Vector2(MoveAction.ReadValue<Vector2>().x * speed, rb.linearVelocityY);
}
```

## 3. Collider
Collider2D là component định nghĩa hình dạng va chạm của object

- Các loại Collider2D phổ biến

    - **BoxCollider2D**: hình chữ nhật, dùng cho object dạng hộp, platform, tường
    - **CircleCollider2D**: hình tròn, dùng cho bóng, nhân vật dạng tròn, hiệu năng tốt vì tính toán đơn giản
    - **CapsuleCollider2D**: hình viên nang (chữ nhật bo tròn 2 đầu), hay dùng cho nhân vật đứng
    - **PolygonCollider2D**: đa giác tùy chỉnh, tự động khớp theo hình sprite, dùng cho vật có hình dạng phức tạp
    - **EdgeCollider2D**: chỉ là các đường thẳng nối nhau (không có diện tích bên trong), dùng cho địa hình, mặt đất gồ ghề
    - **CompositeCollider2D**: gộp nhiều collider lại thành một, tối ưu cho tilemap

- Thuộc tính quan trọng

    - **Is Trigger**: nếu bật, collider không cản vật lý (không đẩy nhau) mà chỉ phát hiện sự kiện "đi qua" — dùng cho checkpoint, vùng kích hoạt, nhặt vật phẩm
    - **Material (Physics Material 2D)**: quy định độ ma sát (friction) và độ nảy (bounciness)
    - **Offset**: dịch chuyển vị trí collider so với object mà không cần đổi Transform
    - **Size / Radius / Points**: kích thước hình dạng, tùy loại collider

Ví dụ
```csharp
using UnityEngine;

public class ColliderExample : MonoBehaviour
{
    // Gọi khi va chạm vật lý thật (Is Trigger = false)
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Va chạm với: " + collision.gameObject.name);
    }

    // Gọi khi đi vào vùng trigger (Is Trigger = true)
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            Debug.Log("Nhặt được coin!");
            Destroy(other.gameObject);
        }
    }

    // Gọi khi rời khỏi vùng trigger
    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name + " đã rời khỏi vùng");
    }
}
```

# **Bài mới**
## 1. Trigger
- Trigger có 3 sự kiện chính, mỗi sự kiện được gọi tự động khi có Collider2D khác đi vào/ở trong/ra khỏi vùng trigger:

    - **OnTriggerEnter2D** — gọi 1 lần duy nhất ngay khi vật khác vừa chạm vào vùng trigger
    - **OnTriggerStay2D** — gọi liên tục mỗi frame khi vật khác vẫn còn ở trong vùng trigger
    - **OnTriggerExit2D** — gọi 1 lần duy nhất khi vật khác vừa rời khỏi vùng trigger

Cả 3 hàm đều trả về tham số `Collider2D other` — chính là collider của vật đã đi vào/ở trong/ra khỏi vùng trigger.

- Điều kiện để 3 hàm này hoạt động

    - Collider trên object chứa 3 hàm này phải bật **Is Trigger**
    - Ít nhất một trong hai object va chạm phải có **Rigidbody2D**

```csharp
using UnityEngine;

public class TriggerExample : MonoBehaviour
{
    // Gọi 1 lần khi vật khác vừa đi vào vùng trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name + " vừa đi vào vùng trigger");

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player đã kích hoạt checkpoint!");
        }
    }

    // Gọi liên tục mỗi frame khi vật khác vẫn đang ở trong vùng trigger
    void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name + " vẫn đang ở trong vùng trigger");

        // Ví dụ: gây damage liên tục khi đứng trong vùng lửa
        if (other.CompareTag("Player"))
        {
            // DamagePlayer(1 * Time.deltaTime);
        }
    }

    // Gọi 1 lần khi vật khác vừa rời khỏi vùng trigger
    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name + " đã rời khỏi vùng trigger");
    }
}
```

## 2. Raycast
Raycast là kỹ thuật bắn một tia từ một điểm theo một hướng, để kiểm tra xem tia đó có chạm vào Collider2D nào không.

- Các hàm Raycast 

    - **Physics2D.Raycast** — bắn 1 tia, trả về **kết quả va chạm đầu tiên** gặp phải
    - **Physics2D.RaycastAll** — bắn 1 tia, trả về tất cả các va chạm mà tia đi qua (dạng mảng)
    - **Physics2D.OverlapCircle** — kiểm tra vùng hình tròn xem có Collider2D nào bên trong không (không phải tia thẳng)
    - **Physics2D.OverlapBox** — tương tự nhưng vùng kiểm tra là hình chữ nhật
    - **Physics2D.OverlapPoint** — kiểm tra tại đúng 1 điểm

Ví dụ:
```csharp
using UnityEngine;

public class RaycastExample : MonoBehaviour
{
    public float rayDistance = 0.5f;

    void Update()
    {
        // Bắn tia từ vị trí object, hướng xuống dưới
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayDistance);

        if (hit.collider != null)
        {
            Debug.Log("Chạm vào: " + hit.collider.gameObject.name);
        }

        // Vẽ tia để dễ debug trong Scene view
        Debug.DrawRay(transform.position, Vector2.down * rayDistance, Color.red);
    }
}
```

## 3. LayerMask

LayerMask dùng để giới hạn Raycast/Overlap chỉ tương tác với một số Layer nhất định, thay vì kiểm tra va chạm với tất cả mọi thứ trong scene.

```csharp
using UnityEngine;

public class RaycastWithLayerMask : MonoBehaviour
{
    public float rayDistance = 0.5f;
    public LayerMask groundLayer; // Kéo layer "Ground" vào trong Inspector

    void Update()
    {
        // Chỉ bắn tia và kiểm tra va chạm với layer "groundLayer"
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayDistance, groundLayer);

        bool isGrounded = hit.collider != null;
        Debug.Log("Đang chạm đất: " + isGrounded);
    }
}
```
