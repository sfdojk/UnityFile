# **Class C#**
1. Khái niệm cơ bản
- Class: Khuôn mẫu định nghĩa một nhóm đối tượng.

- Thuộc tính: Những gì đối tượng có (vd: hp, speed).

- Hành vi: Những gì đối tượng làm được (vd: TanCong(), Nhay()).

- Object: Một thực thể cụ thể được "đúc" ra từ Class (vd: từ class QuaiVat, tạo ra một object cụ thể là con Goblin hoặc Slime).

2. Bốn tính chất cốt lõi

2.1. Kế thừa (Inheritance):
Class con thừa hưởng lại toàn bộ thuộc tính và hành vi của class cha, giúp tái sử dụng code (vd: class Boss kế thừa class QuaiVat).

   2.2. Trừu tượng (Abstraction):
Ẩn đi các xử lý phức tạp bên trong, chỉ hiện ra những gì cần thiết để xài. (vd: Khi gọi hàm TimDuong(), bạn chỉ cần lấy kết quả đường đi mà không cần quan tâm bên trong nó chạy thuật toán nội bộ gì).

   2.3. Đa hình (Polymorphism):
Một hành động có thể được thực hiện theo nhiều cách khác nhau. Gồm 2 từ khóa:

- Override (Ghi đè): Class con viết lại hàm của class cha (vd: cha có hàm DiChuyen(), con ghi đè lại thành bay trên trời thay vì đi bộ).

- Overload (Nạp chồng): Các hàm giống y hệt tên nhau nhưng khác tham số truyền vào.

2.4. Đóng gói (Encapsulation):
Bảo vệ dữ liệu không bị sửa đổi lung tung từ bên ngoài. Trọng tâm nằm ở Access Modifiers:

- public: Ở đâu cũng truy cập được.
- private: Chỉ bên trong chính class đó mới dùng được.
- protected: Class đó và các class con kế thừa nó mới dùng được.

# **MonoBehavior**
1. **Định nghĩa**
- Là một script kết nối với hệ thống bên trong Unity bằng cách định nghĩa một class kế thừa từ class có sẵn
- Class này như một bản thiết kế để tạo ra một loại component mới có thể gắn vào GameObject
- Chú ý là tên class phải trùng với tên file

2. **Vòng đời**

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