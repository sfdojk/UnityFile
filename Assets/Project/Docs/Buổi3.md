# **Bài cũ**
## 1. Vòng đời
**a. Awake:**
- Chạy trước tất cả các hàm Start nào
- Thường được dùng để lấy tham chiếu

**b. OnEnable**
- Được gọi khi GameObject chuyển sang trạng thái active tại runtime với điều kiện script đó đang trạng thái enabled
- Luôn được gọi sau Awake() và trước Start() khi Play
- OnEnable có thể gọi lại nhiều lần mỗi khi GameObject được active lại

**c. Start**
- Được gọi đúng một lần trong vòng đời của script, vào frame đầu tiên mà script được enable, ngay trước lần gọi Update() đầu tiên 
- luôn chạy sau toàn bộ các hàm Awake() của mọi object.

**d. Update**
- Được gọi mỗi frame, miễn là MonoBehavior đang ở trạng thái enabled
- Dùng phổ biến để thực hiện logic game (di chuyển, kiểm tra input, đếm thời gian, ...) vì chúng cần chạy liên tục từng frame.

**e. FixUpdate**
- Được gọi theo một khoảng thời gian cố định (mặc định 0.02s, có thể chỉnh sửa tại Project Settings > Time > Fixed Timestep).
- Sử dụng cho các công việc tính toán liên quan đến vật lý

**f. LateUpdate**
- Được gọi mỗi frame, ngay sau khi toàn bộ các lệnh gọi Update() của mọi script trong frame đó đã hoàn tất.
- Dùng phổ biến nhất là camera bám theo nhân vật hoặc xử lý các logic cần kết quả của các logic trong update

**g. OnDisable**
- Được gọi khi component hoặc GameObject chứa nó chuyển sang trạng thái disabled/inactive

**h. OnDestroy**
- Được gọi sau khi frame update cuối cùng của object đã chạy xong, ngay trước khi object thực sự bị huỷ
- Được gọi trên các GameObject đã từng active ít nhất một lần trong đời nó => nơi phù hợp để dọn dẹp tài nguyên do script tự tạo ra

**Thứ tự:** `Awake -> OnEnable -> Start -> (FixedUpdate -> Update -> LateUpdate) -> OnDisable -> OnDestroy`

**Chú ý:** Thay đổi thứ tự các hàm thì vẫn sẽ chạy theo thứ tự định sẵn

## 2. Gizmos
- Gizmos là công cụ dùng để vẽ, đóng vai trò như những nét vẽ nháp (sẽ không được nhìn thấy trừ khi bật Gizmos).
- 2 hàm kích hoạt Gizmos:
    - OnDrawGizmos(): Vẽ liên tục mỗi frame (Có thể dùng để vẽ lưới tọa độ **(grid), waypoint, spawnoint**)
    - OnDrawGizmosSelected(): Chỉ hiển thị nét vẽ khi chọn đúng GameObject. Dùng để vẽ thông số cá nhân (**tầm nhìn, tầm đánh**)
- Các lệnh Gizmos phổ biến:
    - Gizmos.color: tạo màu cho nét vẽ
    - Gizmos.DrawLine(A, B): vẽ đoạn thẳng AB
    - Gizmos.DrawRay(origin, dir): vẽ tia vô hướng bắn từ origin đến dir
    - Gizmos.DrawWireSphere(pos, R): vẽ khung viền rỗng của hình cầu hoặc hình tròn
    - Gizmos.DrawWireCube(pos, size): vẽ khung viền rỗng của hình hộp
    - Gizmos.DrawIcon(pos, name): đặt một icon hình ảnh tạo tọa độ cụ thể

## 3. Transform
- là 1 component bắt buộc có, biểu hiện vị trí, kích thước, hướng của GameObject
- Các thuộc tính cơ bản:
    - **Position:** tọa độ không gian Oxyz, có thể là tuyệt đối (so với game) hoặc tương đối so với đối tượng chứa nó (local Position)
    - **Rotation:** hướng nhìn của đối tượng trong không gian
    - **Scale:** tỷ lệ phóng to hoặc thu nhỏ của đối tượng theo từng trục

## 4.Time và các hàm của Time
- **Time.deltaTime:** khoảng cách giữa 2 frame trong 1 scene
- **Time.fixedDeltaTime:** khoảng tgian ingame giữa 2 lần FixedUpdate (thường là 0.02s)
- **Time.unScaledDeltaTime:** là Time.deltaTime nhưng không bị ảnh hưởng bởi Time.timeScale
- **Time.timeScale:** là tốc độ của game

## 5.Mathf

* **Clamp(val, min, max):** Khóa cứng giá trị sao cho không bao giờ vượt ra ngoài khoảng min và max. 
* **Lerp(a, b, t):** Nội suy tuyến tính từ điểm a đến b dựa trên biến t (chạy từ 0 đến 1). 
* **Abs / Sign:**  Abs trả về giá trị tuyệt đối (luôn dương), Sign trả về dấu (1 hoặc -1). 
* **Round / Ceil / Floor:** Làm tròn số (Làm tròn gần nhất / Luôn làm tròn lên / Luôn làm tròn xuống). 
* **Sin(f) / Cos(f) / Tan(f):** Tính toán lượng giác cơ bản theo hệ radian. 
* **Infinity:** Đại diện cho giá trị dương vô cực.
* **Min / Max:** so sánh 2 giá trị và lấy giá trị nhỏ hơn / lớn hơn

# **Bài mới**
## 1. New Input System
- New Input System tách biệt thiết bị đầu vào khỏi logic code, giúp hỗ trợ nhiều loại thiết bị mà không sửa code
- 3 khái niệm cốt lõi:
    - **Input Action Asset:** file `.inputactions`, nơi định nghĩa các hành động như Move, Jump
    - **Action Maps:** nhóm các action lại (map player gồm move/jump, map UI gồm navigate/submit), có thể bật tắt từng map.
    - **Bindings:** gán phím/nút vật lý cho từng action (1 action có thể gán nhiều bindings)
- 2 cách đọc input trong code:
    - C1: dùng component `PlayerInput`, Unity tự gọi hàm `OnJump()`, `OnMove()`khi action xảy ra
    - C2: tự lấy action bằng code (`InputSystem.actions.FindAction("Move")`) rồi gọi `.ReadValue<T>()` mỗi frame
- Cách setup: Cài package → tạo Input Actions asset → thêm Action Map + Actions → gán binding → gắn `PlayerInput` component vào nhân vật → đọc input bằng 1 trong 2 cách trên

## 2. Physic 2D
1. Va chạm + điều kiện xảy ra va chạm
- Điều kiện xảy ra va chạm:
    - Cả 2 đều phải có Collider 2D
    - Ít nhất 1 trong 2 phải có Rigidbody 2D

2. Rigidbody 2D
- `Dynamic`: Chịu tác động đầy đủ của trọng lực và lực tác động
- `Kinematic`: Không bị ảnh hưởng bởi trọng lực hay lực va chạm từ vật khác, di chuyển hoàn toàn bằng code
- `Static`: Không bao giờ di chuyển

3. Collision và Trigger
- **Collision (Va chạm vật lý)**
    - Collider để `Is Trigger = false`
    - Hàm callback
        - `OnCollisionEnter2D(Collision2D collision)`: Khi vừa chạm
        - `OnCollisionStay2D(Collision2D collision)`: Khi đang tiếp xúc
        - `OnCollisionExit2D(Collision2D collision)`: Khi vừa rời ra
- **Trigger (Xuyên qua)**
    - Collider để `Is Trigger = true`
    - Hàm callback:
        - `OnTriggerEnter2D(Collider2D other)`
        - `OnTriggerStay2D(Collider2D other)`
        - `OnTriggerExit2D(Collider2D other)`

4. Raycast 2D
- Bắn một tia vô hình từ một điểm theo một hướng để kiểm tra vật thể phía trước

```csharp
[SerializeField] private LayerMask groundLayer;
[SerializeField] private float checkDistance = 0.2f;

bool IsGrounded()
{
    // Bắn tia từ chân nhân vật hướng xuống dưới
    RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, checkDistance, groundLayer);
    
    // Debug hiển thị tia trong Scene view
    Debug.DrawRay(transform.position, Vector2.down * checkDistance, hit.collider ? Color.green : Color.red);
    
    return hit.collider != null;
}
```

5. LayerMask
- Dùng để gom GameObject
- Tác dụng:
    - Cho phép bật tắt va chạm giữa các Layer trong `Project Settings -> Physics 2D -> Layer Collision Matrix`
    - Lọc mục tiêu cho Raycast hoặc OverlapCircle (chỉ quét trúng đất hoặc quái, bỏ qua Player, UI)

6. Các cách di chuyển nhân vật
- `transform.Translate` (hoặc cộng thẳng vào `position`)

    - Dịch chuyển tọa độ trực tiếp, không thông qua vật lý.
    - Không có va chạm vật lý — dễ bị xuyên tường.

```csharp
Vector2 input = moveAction.ReadValue<Vector2>();
transform.Translate(input * speed * Time.deltaTime);
```

- `Rigidbody2D.velocity`

    - Gán trực tiếp giá trị input nhân với tốc độ vào `rb.velocity`.
    - Đặt trong `FixedUpdate` (không phải `Update`) — vì đây là thao tác vật lý, cần đồng bộ với physics engine.
    - Có va chạm vật lý — không xuyên tường.

```csharp
private void FixedUpdate()
{
    Vector2 input = moveAction.ReadValue<Vector2>();
    rb.velocity = new Vector2(input.x * speed, rb.velocity.y);
}
```

- `Rigidbody2D.AddForce`

    - Tác động một lực tức thời thay vì gán vận tốc trực tiếp — vật lý tự tính gia tốc.
    - Có va chạm vật lý.

```csharp
rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
```
