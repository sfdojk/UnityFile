# **Bài cũ**
## 1. Raycast2D
- Bắn 1 tia, nếu chạm vào GameObject sẽ trả về `RaycastHit2D` (là 1 struct), khi không trúng gì vẫn sẽ trả về struct nhưng có thể kiểm tra bằng hit.collider xem có null không
```csharp
RaycastHit2D hit = Physics2D.Raycast(origin, direction, distance);
if (hit.collider != null) { ... }
```
- `RaycastHit2D` chứa
    - `hit.point`: điểm va chạm
    - `hit.normal`: phát tuyến bề mặt
    - `hit.distance`: khoảng cách
    - `hit.collider`: Collider2D
    - `hit.rigidbody`: RigidBody2D
    - `hit.transform`

- Overlap2D sẽ trả về bool hoặc Collider2D
- Raycast, Overlap, Boxcast, ... đều thuộc Physic2D
- RaycastAll sẽ trả về mảng RaycastHit để xử lí
- OverlapAll sẽ trả về mảng collider

## 2. Trigger2D
- Điều kiện: 2 GameObject phải có Collider
- Ít nhất 1 trong 2 phải bật isTrigger
- Ít nhất 1 Rigidbody trong cặp va chạm

- Các hàm:
    - `OnTriggerEnter2D`: kiểm tra khi vừa va chạm
    - `OnTriggerStay2D`: kiểm tra mỗi frame nếu nó còn ở bên trong
    - `OnTriggerExit2D`: kiểm tra khi vừa thoát va chạm

```csharp
using UnityEngine;

public class TriggerExample : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name + " vừa đi vào vùng trigger");

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player đã kích hoạt checkpoint!");
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name + " vẫn đang ở trong vùng trigger");

        // Ví dụ: gây damage liên tục khi đứng trong vùng lửa
        if (other.CompareTag("Player"))
        {
            // DamagePlayer(1 * Time.deltaTime);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name + " đã rời khỏi vùng trigger");
    }
}

```

## 3. LayerMask
- Dùng để lọc Collider
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

## 4. Prefab và Instantiate
- Prefab là một loại asset cho phép lưu lại một GameObject hoàn chỉnh — bao gồm toàn bộ component, giá trị thuộc tính, và cả các GameObject con của nó — dưới dạng một cái gì đó tái sử dụng được
- Nếu muốn biến 1 thứ thành Prefab:
    - 1 là nó là con của 1 GameObject
    - Hoặc nó nằm trong folder Asset

- Nếu muốn spawn ra prefab, ta dùng hàm:

     `Instantiate(prefab, position, rotation, root)`

Ví dụ: spawn ra Enemy
```csharp
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnInterval = 2f;

    private float _timer;

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= spawnInterval)
        {
            _timer = 0f;
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        }
    }
}
```

# **Bài mới**
## 1. Delegate
- Delegate là kiểu dữ liệu đại diện cho 1 hàm, cho phép truyền hàm như 1 biến khác
- Delegate giống như một "con trỏ hàm", định nghĩa kiểu chữ ký (signature) gồm kiểu trả về và danh sách tham số.
- Mọi hàm được gán vào delegate phải có chữ ký khớp hoàn toàn với định nghĩa đó

```csharp
using UnityEngine;

public class Test : MonoBehaviour
{
    // Class Calculator
    public class Calculator
    {
        // Định nghĩa kiểu delegate: đại diện cho hàm nhận vào 1 int, không trả về gì
        public delegate void OnCalculateDone(int result);

        // Biến kiểu delegate — sẽ được gán hàm từ bên ngoài
        public OnCalculateDone callback;

        public void Calculate(int a, int b)
        {
            int result = a + b;
            Debug.Log("Đang tính toán...");

            // Gọi hàm đang được gán trong callback, truyền result vào
            // Nên check null trước khi gọi, tránh lỗi NullReferenceException
            if (callback != null)
            {
                callback(result);
            }
        }
    }

    // ==== Chạy thử ====
    void Start()
    {
        Calculator calc = new Calculator();

        // Gán 1 hàm
        calc.callback = PrintResult;
        calc.Calculate(3, 5);
        // Output: "Đang tính toán..." rồi "Kết quả là: 8"

        Debug.Log("------");

        // Gán thêm hàm thứ 2 bằng +=  → gọi cả 2 hàm cùng lúc
        calc.callback += ShowPopup;
        calc.Calculate(10, 20);
        // Output: "Đang tính toán...", "Kết quả là: 30", "Hiện popup với kết quả: 30"

        Debug.Log("------");

        // Bỏ bớt 1 hàm bằng -=
        calc.callback -= PrintResult;
        calc.Calculate(1, 1);
        // Output: "Đang tính toán...", "Hiện popup với kết quả: 2"  (PrintResult không chạy nữa)
    }

    void PrintResult(int result)
    {
        Debug.Log("Kết quả là: " + result);
    }

    void ShowPopup(int result)
    {
        Debug.Log("Hiện popup với kết quả: " + result);
    }
}
```

- Tóm gọn lại 1 câu để nhớ: delegate = danh sách hàm, += thêm hàm vào danh sách, -= gỡ ra, gọi 1 lần thì cả danh sách chạy hết.


## 2. Action
- bản chất Action chính là delegate, không cần tự khai báo delegate nữa.
```csharp
using UnityEngine;
using System;   // Action nằm trong namespace System, cần using dòng này

public class Test : MonoBehaviour
{
    public class Calculator
    {
        public Action<int> callback;   // thay cho delegate tự khai báo

        public void Calculate(int a, int b)
        {
            int result = a + b;
            callback?.Invoke(result);   // gọi giống hệt delegate, dùng ?.Invoke() cho an toàn
        }
    }

    void Start()
    {
        Calculator calc = new Calculator();

        calc.callback = PrintResult;
        calc.callback += ShowPopup;    // += vẫn hoạt động y hệt delegate

        calc.Calculate(3, 5);
    }

    void PrintResult(int result) => Debug.Log("Kết quả: " + result);
    void ShowPopup(int result) => Debug.Log("Popup: " + result);
}
```
- Phân biệt:
```csharp
public Action<>;         // Action thường — CHO PHÉP script khác gán đè (=) từ bên ngoài
public event Action<>   // có từ khóa "event" — script khác CHỈ được +=/-=, không được gán đè
```

## 3. Unity Event
- `UnityEvent` là bản Unity-hóa của delegate — điểm khác biệt lớn nhất: hiện trong Inspector, cho phép kéo-thả gán hàm bằng tay, không cần code.

```csharp
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    public UnityEvent onOpen;          // không tham số, hiện trong Inspector   
    public UnityEvent<int> onDamage;   // có tham số (cần UnityEvent<T> tự định nghĩa hoặc dùng sẵn với [Serializable])

    public void Open()
    {
        Debug.Log("Cửa mở");
        onOpen?.Invoke();
    }
}
```

## 4. Coroutine
- Coroutine: hàm tạm dừng giữa chừng, chạy tiếp ở frame sau (không chạy xong ngay 1 lần như hàm thường)
- Chạy luồng riêng
- Hàm coroutine trả về kiểu `IEnumerator`
- Mỗi lần gặp `yield return ...`, coroutine tạm dừng, trả quyền điều khiển lại cho Unity, và chờ điều kiện được thỏa mới chạy tiếp dòng kế tiếp
- Các kiểu `yield return` thường gặp:

```csharp
yield return null;                     // chờ đúng 1 frame rồi chạy tiếp
yield return new WaitForSeconds(2f);   // chờ 2 giây rồi chạy tiếp
```

Ví dụ
```csharp
using UnityEngine;
using System.Collections;

public class Example : MonoBehaviour
{
    IEnumerator DoSomething()
    {
        Debug.Log("Bắt đầu");

        yield return new WaitForSeconds(2f);  // dừng 2 giây tại đây

        Debug.Log("Chạy tiếp sau 2 giây");

        yield return null;  // dừng 1 frame

        Debug.Log("Chạy tiếp frame sau");
    }
}
```

- Start / Stop Coroutine
    - Muốn dùng Coroutine: `StartCoroutine(TênHàm());`
    - Muốn dừng Coroutine: `StopCoroutine(bienDaLuu)` hoặc `StopAllCoroutines()` (dừng hết Coroutine đang chạy)

```csharp
Coroutine myRoutine;

IEnumerator DoSomething()
{
    Debug.Log("Bước 1");
    yield return new WaitForSeconds(2f);

    Debug.Log("Bước 2");
    yield return new WaitForSeconds(2f);

    Debug.Log("Bước 3");   // giả sử code KHÔNG BAO GIỜ chạy tới đây nếu bị Stop trước đó
}

void Start()
{
    myRoutine = StartCoroutine(DoSomething());
}

void Update()
{
    if (Input.GetKeyDown(KeyCode.Space))   // nhấn Space để gọi StopIt()
    {
        StopIt();
    }
}

void StopIt()
{
    StopCoroutine(myRoutine);
    Debug.Log("Đã dừng coroutine");
}
```

