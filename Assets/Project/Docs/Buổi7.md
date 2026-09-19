# **Bài cũ**
## 1. Animator Controller

- Asset .controller quản lý việc chuyển đổi giữa các Animation Clip
- Gồm State (gắn với một Clip), Transition (đường nối giữa các State), Parameter (biến điều kiện để chuyển State)
- Các loại Parameter: Bool, Float, Int, Trigger

```csharp
float currentSpeed = animator.GetFloat("speed");
bool running = animator.GetBool("isRunning");
```

## 2. Animation Clip
- File chứa dữ liệu chuyển động của một hành động cụ thể như đi, chạy, nhảy
- Ghi lại thay đổi vị trí, xoay, scale, sprite theo thời gian dưới dạng keyframe
- Đuôi file .anim
- Tạo bằng Animation Window hoặc import từ FBX
- Có thể gắn Animation Event để gọi hàm tại một frame cụ thể

```csharp
public void OnFootstepEvent()
{
    Debug.Log("Chân chạm đất");
}
```

## 3. Animator Component

- Component gắn lên GameObject
- Là cầu nối giữa GameObject và Animator Controller
- Dùng để gán/đọc giá trị Parameter và điều khiển state từ script

```csharp
private Animator animator;
private InputAction moveAction;
private InputAction jumpAction;

void Awake()
{
    animator = GetComponent<Animator>();
    moveAction = InputSystem.actions.FindAction("Move");
    jumpAction = InputSystem.actions.FindAction("Jump");
}

void Update()
{
    Vector2 move = moveAction.ReadValue<Vector2>();
    animator.SetFloat("speed", move.magnitude);

    if (jumpAction.WasPressedThisFrame())
        animator.SetTrigger("Jump");
}
```

## 4. Blend Tree

- Một loại State đặc biệt trong Animator Controller
- Pha trộn nhiều Animation Clip dựa trên giá trị Parameter (thường là Float)
- Dùng phổ biến cho chuyển động đi/chạy mượt theo tốc độ
- Tạo trong Editor: chuột phải trong Animator > Create State > From New Blend Tree

# **Bài mới**
## 1. Tự viết FSM

- Ý tưởng: tất cả state và logic xử lý nằm chung trong 1 script, dùng `enum` để đánh dấu "đang ở state nào"
- Dùng `switch` trong `Update()` để mỗi frame kiểm tra state hiện tại và chạy đúng đoạn code tương ứng
Ví dụ:
```csharp
    public enum PlayerState { Idle, Attack1, Attack2 }
    private PlayerState currentState = PlayerState.Idle;

    void Update()
    {
        switch (currentState)
        {
            case PlayerState.Idle:
              if (attackPressed) currentState = PlayerState.Attack1;
              break;

            case PlayerState.Attack1:
              // logic riêng của Attack1
              if (attackPressed) currentState = PlayerState.Attack2;
              break;
        }
    }
```
- Ưu điểm: viết nhanh, dễ hiểu ngay khi đọc, không cần tạo nhiều file
- Nhược điểm: khi thêm nhiều state (Attack3, Jump, Dash...), khối `switch` càng lúc càng dài, code của các state bị trộn lẫn trong cùng 1 hàm → khó đọc, khó sửa mà không ảnh hưởng state khác
- Phù hợp: project nhỏ, số state ít (dưới 5-6 state)

## 2. FSM dùng Interface/Abstract
- Ý tưởng: mỗi state là **1 class riêng biệt**, không nhét chung vào 1 script như cách trên

- Tất cả các class state đều tuân theo 1 khuôn chung (interface hoặc abstract class), có sẵn các hàm: `Enter()` (chạy khi vừa vào state), `Update()` (chạy mỗi frame khi đang ở state đó), `Exit()` (chạy khi rời state)

Ví dụ:
```csharp
    public interface IPlayerState
    {
      void Enter();
      void Update();
      void Exit();
    }
```
- Ví dụ 1 state cụ thể implement theo khuôn đó:
```csharp
    public class AttackState1 : IPlayerState
    {
      public void Enter()  { /* bật animation Attack1 */ }
      public void Update() { /* check input để chuyển sang Attack2 */ }
      public void Exit()   { /* dọn dẹp trước khi rời state */ }
    }
```
- State machine chính chỉ cần giữ 1 biến `currentState` kiểu `IPlayerState`, gọi `currentState.Update()` mỗi frame — không cần biết bên trong state đó làm gì
- Ưu điểm: mỗi state nằm gọn trong 1 file riêng, sửa state này không sợ ảnh hưởng state khác, dễ test và dễ đọc khi số state nhiều
- Thêm state mới: chỉ cần tạo thêm 1 class implement `IPlayerState`, không cần sửa code của các state cũ
- Phù hợp: project lớn, nhiều state, hoặc logic mỗi state phức tạp (nên nhiều dòng code)
