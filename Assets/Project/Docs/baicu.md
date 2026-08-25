# **CHỮA LẠI BTVN**
**Phân tích yếu tạo nên cảm giác từ hiệu ứng yêu thích**  
Ví dụ: Mortal Kombat hiệu ứng critical hit đấm vỡ xương:  
- Âm thanh: Mới đầu sẽ to lên, sau đó trầm, nhỏ lại tạo cảm giác dồn vào trọng tâm điểm đánh  
- Hoạt ảnh: tay đấm nhanh và hoạt ảnh mờ đi theo sau tạo độ nhanh, dồn trọng tâm vào điểm bị đánh. chậm lại từ từ, xương bị nứt từ từ, máu bắn ra chậm tương tự tốc độ vỡ xương, sau đó nhanh dần lại  

# **const / readonly**  
**1. const**  
- Khai báo: public ( hoặc private ) const + kiểu dữ liệu cơ bản ( int, float, bool, string, ... )
- Bắt buộc gán giá trị ngay lúc khai báo

```csharp
using System;

class Player
{
    public const int hp = 100;
}
```

**2. readonly**  
- Khai báo: public ( hoặc private ) readonly + kiểu dữ liệu ( có thể là class, struct, object, … )
- Gán lúc khai báo hoặc gán trong constructor

```csharp
using System;

class Player
{
    public readonly double hp; // có thể ko khai báo
    // dùng constructor để gán
    public Player(double hp)
    {
        this.hp = hp;
    }
}
```

# **class**
**Khái niệm:** là bản thiết kế của đối tượng, quy định đặc điểm và hành vi của đối tượng đó.  
**Cấu trúc:** gồm fields và methods
```csharp
using System;

class Player
{
    private double hp; // thuộc tính
    private double atk; // thuộc tính

    // Methods
    public void takeDamage(double dame)
    {
        hp -= dame;
    }
}
```  
**Constructor:** hàm khởi tạo
- Dùng để gán giá trị ban đầu cho class
- Quy tắc là đặt tên đúng theo tên class

```csharp
using System;

class Player
{
    private int atk;
    
    public Player(int atk)
    {
        this.atk = atk;
    }
}
```

# ref / out
**ref**
- Bắt buộc phải gán giá trị trước khi truyền vào hàm

```csharp
void Atk(ref int damage) 
{
    damage += 50;
}

int swordDamage = 10;
atk(ref swordDamage); 
Console.WriteLine(swordDamage);
// 60
```

**out**
- Không yêu cầu phải gán giá trị trước nhưng bắt buộc phải gán giá trị trong hàm

```csharp
void Player(out int hp, out int mana) 
{
    hp = 100;
    mana = 50;
}

Player(out int HP, out int Mana);

Console.WriteLine(HP);
Console.WriteLine(MANA);
// 100
// 50
```
