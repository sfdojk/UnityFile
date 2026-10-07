# GDD: DOOMSDAY

## 1. Tổng quan

- **Tên game:** Doomsday
- **Thể loại:** Sinh tồn top-down 2D, theo kiểu Vampire Survivors nhưng ít cơ chế hơn
- **One rule:** **Sống sót.** Mọi cơ chế đều phục vụ quy tắc này: quái liên tục ép người chơi phải di chuyển, nâng cấp giúp người chơi trụ lâu hơn.
- **Cốt truyện:** Ngày tận thế, quái vật tràn ngập thế giới. Khu vực của bạn bị niêm phong và bạn không kịp sơ tán. Giờ bạn phải cầm cự cho đến khi đội cứu hộ đến. Nhiệm vụ duy nhất: **PHẢI SỐNG SÓT.**
- **Điều kiện thắng/thua:**
  - Thua: máu về 0.
  - Thắng: sống sót đủ thời gian quy định (đội cứu hộ đến), dự kiến loanh quanh 10 phút.

## 2. Thành phần game

- **Nhân vật chính:** có máu, tốc độ di chuyển và kỹ năng lướt (dash).
- **Quái vật (3 loại):**

| Loại | Tốc độ | Máu | Vai trò |
|---|---|---|---|
| Nhanh | Cao | Thấp | Ép người chơi di chuyển liên tục |
| Chậm | Vừa | Trung bình | Số lượng nhiều, dễ bao vây |
| Siêu chậm (tank) | Rất thấp | Rất cao | Chặn đường, buộc phải tập trung bắn |

- **Vũ khí người chơi:** súng lục.
- **Kinh nghiệm và cấp độ:** quái chết rơi ra EXP, đủ EXP thì lên cấp.
- **Thẻ nâng cấp (8 loại):**

| Thẻ | Hiệu ứng | Số lần nhận tối đa | Xuất hiện từ cấp |
|---|---|---|---|
| Máu tối đa | +20 máu tối đa | Không giới hạn | 1 |
| Sát thương | +5 sát thương | Không giới hạn | 1 |
| Tốc độ bắn | +15% tốc độ bắn | Không giới hạn | 1 |
| Tốc độ di chuyển | +10% tốc độ chạy | 5 | 1 |
| Hút máu | +2% hút máu | 5 | 2 |
| Giảm hồi chiêu lướt | -15% thời gian hồi | 4 | 2 |
| Bắn nhiều viên | +1 viên mỗi lần bắn | 4 | 3 |
| Đạn xuyên | Đạn xuyên thêm 1 quái | 3 | 5 |

Các thẻ mạnh bị giới hạn số lần nhận và chỉ mở khóa ở cấp cao hơn, để người chơi không quá mạnh và có cảm giác tiến triển rõ ràng theo thời gian.

## 3. Cơ chế game

- **Di chuyển:** WASD
- **Lướt:** Q (có thời gian hồi, vẫn nhận sát thương khi lướt)
- **Bắn:** ngắm bằng chuột, click chuột trái để bắn
- **Cấp độ:** giết quái nhận EXP, mỗi loại quái cho lượng EXP khác nhau. Mỗi lần lên cấp, game tạm dừng và người chơi chọn 1 trong 3 thẻ nâng cấp.
- **Độ khó tăng dần:** theo thời gian, quái xuất hiện nhiều hơn và mạnh hơn.

## 4. Progression

Giết quái → thu thập kinh nghiệm → lên cấp → chọn thẻ nâng cấp → mạnh hơn → sống sót lâu hơn.

