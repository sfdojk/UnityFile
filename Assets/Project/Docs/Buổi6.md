# **Bài cũ**
## 1. Generic
- Generic cho phép viết 1 class/hàm dùng chung được cho nhiều kiểu dữ liệu khác nhau, mà không cần viết lại code cho từng kiểu

- Ký hiệu bằng chữ trong <> — đại diện cho "kiểu chưa xác định", sẽ được quyết định khi gọi thực tế

Ví dụ khi muốn lấy component:
```csharp
Rigidbody rb = GetComponent<Rigidbody>();
SpriteRenderer sr = GetComponent<SpriteRenderer>();
```

Ví dụ khi tạo hộp chứa:
```csharp
using UnityEngine;

// Class generic tự định nghĩa: chứa được bất kỳ kiểu T nào
public class ItemSlot<T>
{
    public T item;
    public bool isEmpty = true;

    public void SetItem(T newItem)
    {
        item = newItem;
        isEmpty = false;
    }
}

public class InventoryTest : MonoBehaviour
{
    void Start()
    {
        ItemSlot<string> weaponSlot = new ItemSlot<string>();
        weaponSlot.SetItem("Sword");

        ItemSlot<int> goldSlot = new ItemSlot<int>();
        goldSlot.SetItem(100);

        Debug.Log(weaponSlot.item); // "Sword"
        Debug.Log(goldSlot.item);   // 100
    }
}
```

## 2. UnityEvent
- UnityEvent là 1 kiểu event đặc biệt của Unity, cho phép bạn gắn hàm vào Inspector bằng kéo-thả, thay vì phải gọi hàm trực tiếp trong code

- Giúp code tách rời — object A không cần biết object B tồn tại, chỉ cần gọi `.Invoke()` thì tất cả đều chạy

Ví dụ
```csharp
using UnityEngine;
using UnityEngine.Events;

public class ButtonTrigger : MonoBehaviour
{
    public UnityEvent onClick; // biến này sẽ hiện ra ô trống trong Inspector

    void OnMouseDown()
    {
        onClick.Invoke(); // "bắn tín hiệu" - gọi tất cả hàm đã gắn vào ô này
    }
}
```

## 3.Coroutine
- Coroutine là 1 hàm đặc biệt (kiểu `IEnumerator`) có thể tạm dừng giữa chừng rồi chạy tiếp ở frame sau, thay vì chạy hết 1 mạch như hàm bình thường

- Dùng cho: chờ 1 khoảng thời gian, di chuyển từ từ, hiệu ứng chạy dần qua nhiều frame

- Các kiểu `yield return`
    - `yield return null`: đợi 1 frame rồi chạy tiếp

    - `yield return new WaitForSeconds(x)`: đợi x giây rồi chạy tiếp

    - `yield return new WaitUntil(() => condition)`: đợi tới khi điều kiện đúng

```csharp
using System.Collections;
using UnityEngine;

public class Example : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(MyCoroutine()); // "bấm play" cho Coroutine chạy
    }

    IEnumerator MyCoroutine()
    {
        Debug.Log("Bắt đầu");
        yield return new WaitForSeconds(2f); // tạm dừng 2 giây
        Debug.Log("Chạy tiếp sau 2 giây");
    }
}
```

# **Bài mới**
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

```csharp
animator.SetFloat("speed", currentSpeed);
```

## 5. Finite State Machine (FSM)

- Máy trạng thái hữu hạn: tập hợp các trạng thái cố định
- Tại một thời điểm chỉ ở đúng một trạng thái
- Chuyển đổi giữa các trạng thái theo điều kiện (transition)
- Animator Controller chính là một dạng FSM được trực quan hóa bằng giao diện kéo thả

```csharp
public enum PlayerState { Idle, Run, Jump }
private PlayerState currentState = PlayerState.Idle;
private InputAction moveAction;

void Awake()
{
    moveAction = InputSystem.actions.FindAction("Move");
}

void Update()
{
    float speed = moveAction.ReadValue<Vector2>().magnitude;

    switch (currentState)
    {
        case PlayerState.Idle:
            if (speed != 0) currentState = PlayerState.Run;
            break;
        case PlayerState.Run:
            if (speed == 0) currentState = PlayerState.Idle;
            break;
    }
}
```
