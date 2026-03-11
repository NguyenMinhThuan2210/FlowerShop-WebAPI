# FlowerShop Web API

Dự án Backend API cho hệ thống cửa hàng bán hoa trực tuyến, được xây dựng từ đầu nhằm rèn luyện tư duy thiết kế hệ thống, tối ưu truy vấn Database và xử lý các luồng mua sắm thực tế (Bảo mật, Giỏ hàng, Thanh toán).

## Công nghệ sử dụng
* **Ngôn ngữ & Framework:** C#, .NET 8, ASP.NET Core Web API
* **Database:** SQL Server, Entity Framework Core (EF Core)
* **Bảo mật:** JWT (JSON Web Token), BCrypt
* **Thư viện phụ trợ:** FluentValidation (Kiểm tra dữ liệu đầu vào)
* **Kiến trúc:** Clean Architecture (Chia 3 tầng Core - Infrastructure - API), Dependency Injection.

## Các điểm nhấn kỹ thuật trong dự án

### 1. Kiến trúc & Code sạch
* **Giữ tầng Core độc lập:** Tầng `Core` chỉ chứa logic nghiệp vụ thuần C#, hoàn toàn không cài đặt package của Entity Framework hay các thư viện ngoại lai để đảm bảo tính độc lập theo đúng Dependency Rule.
* **Specific Repository Pattern:** Sử dụng Repository riêng cho từng thực thể (bảng) thay vì dùng Generic. Cách này giúp cấu trúc rõ ràng và dễ dàng tùy biến các câu query SQL phức tạp.
* **Map DTO thủ công (Manual Mapping):** Hạn chế lạm dụng AutoMapper. Việc gán tay kết hợp với `IQueryable` giúp EF Core tự động sinh câu lệnh `JOIN` tối ưu dưới Database, chỉ lấy đúng các cột cần thiết lên RAM.

### 2. Tối ưu Hiệu năng (Performance)
* **Phân trang dưới Database (Database-level Pagination):** Tránh tình trạng tải toàn bộ dữ liệu lên RAM, dự án sử dụng `IQueryable` để EF Core thực thi lệnh `Skip` và `Take` trực tiếp dưới SQL Server, giúp tối ưu bộ nhớ và hiệu năng.
* **Tối ưu thao tác đọc:** Các API chỉ mang tính chất đọc dữ liệu đều được gắn thêm `.AsNoTracking()` để giảm tải cho cơ chế theo dõi sự thay đổi (Change Tracker) của EF Core.

### 3. Bảo mật Hệ thống
* Mật khẩu người dùng tuyệt đối không lưu dạng plain-text, được băm (hash) một chiều bằng thuật toán **BCrypt** (tự động sinh Salt).
* Đăng nhập và phân quyền hệ thống (Admin / Customer) thông qua **JWT Token**.
* **Bảo mật Giỏ hàng an toàn:** Khi có thao tác với giỏ hàng, hệ thống tự động bóc tách `UserId` từ chính Claims của JWT Token. Tuyệt đối không nhận `UserId` từ Client gửi lên qua Body/URL để ngăn chặn lỗ hổng IDOR (thay đổi ID để thao tác trên tài khoản người khác).

### 4. Xử lý Thanh toán (Checkout) & Xử lý lỗi
* **Giao dịch an toàn (Manual Transaction):** Luồng đặt hàng trải qua 3 bước: *Tạo hóa đơn -> Trừ tồn kho -> Xóa giỏ hàng*. Cả 3 bước được đóng gói vào một `Transaction` (All-or-Nothing). Nếu xảy ra lỗi (ví dụ: kho báo hết hàng), hệ thống tự động `Rollback` về trạng thái ban đầu, đảm bảo tính toàn vẹn dữ liệu.
* **Bắt lỗi tập trung (Global Exception Handling):** Sử dụng Middleware đánh chặn ở tầng ngoài cùng của Pipeline. Mọi Exception chưa được xử lý sẽ bị chặn lại, ẩn Stack Trace và trả về thông báo lỗi dạng JSON chuẩn mực cho Client.
* **Validate dữ liệu:** Dữ liệu đầu vào (giá âm, sai format email, chuỗi rỗng...) được kiểm tra và chặn ngay từ cổng Controller bằng **FluentValidation**.

## Hướng dẫn cài đặt & Chạy thử
1. Clone dự án về máy.
2. Tìm file `appsettings.example.json` và đổi tên thành `appsettings.json`.
3. Cập nhật chuỗi kết nối SQL Server vào phần `DefaultConnection`. Ở phần `Jwt:Key`, nhập một chuỗi mật khẩu bí mật bất kỳ (yêu cầu dài hơn 32 ký tự).
4. Mở Package Manager Console, chọn Default Project là `FlowerShop.Infrastructure` và chạy lệnh: `Update-Database` để khởi tạo cấu trúc bảng.
5. Bấm F5 để chạy ứng dụng và kiểm thử các API thông qua giao diện Swagger UI.
