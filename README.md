# 🌸 FlowerShop Web API

Đây là dự án Backend API cho một hệ thống cửa hàng bán hoa trực tuyến. Em tự build project này từ đầu để luyện tập tư duy thiết kế hệ thống, tối ưu Database và xử lý các luồng mua hàng thực tế (Bảo mật, Giỏ hàng, Thanh toán).

## 🚀 Công nghệ sử dụng
* **Ngôn ngữ & Framework:** C#, .NET 8, ASP.NET Core Web API
* **Database:** SQL Server, Entity Framework Core (EF Core)
* **Bảo mật:** JWT (JSON Web Token), BCrypt (Băm mật khẩu)
* **Thư viện khác:** FluentValidation (Kiểm tra dữ liệu đầu vào)
* **Kiến trúc:** Clean Architecture (Chia 3 tầng Core - Infrastructure - API), Dependency Injection.

## 🔥 Các điểm nhấn kỹ thuật trong dự án

### 1. Kiến trúc & Code sạch
* **Giữ tầng Core sạch sẽ:** Tầng `Core` chỉ chứa logic nghiệp vụ thuần C#, em tuyệt đối không cài package của Entity Framework hay các thư viện ngoại lai vào đây để đảm bảo tính độc lập.
* **Tự viết Repository (Specific):** Em tạo Repository riêng cho từng bảng thay vì xài đồ chung (Generic). Cách này giúp code rõ ràng và cực kỳ dễ custom những câu query SQL phức tạp sau này.
* **Map DTO thủ công:** Em không lạm dụng AutoMapper. Việc map tay kết hợp với `IQueryable` giúp EF Core đủ thông minh để tự động sinh câu lệnh `JOIN` dưới Database, chỉ kéo đúng những cột cần thiết lên RAM thay vì kéo cả cục.

### 2. Tối ưu Hiệu năng (Performance)
* **Phân trang dưới Database:** Thay vì lôi cả ngàn sản phẩm lên RAM rồi mới cắt trang, em dùng `IQueryable` để ép EF Core chạy lệnh `Skip` và `Take` thẳng dưới SQL Server. Server siêu nhẹ và mượt.
* Các API chỉ mang tính chất đọc dữ liệu (như xem danh sách hoa) đều được em gắn thêm `.AsNoTracking()` để tăng tốc độ truy xuất.

### 3. Bảo mật Hệ thống
* Không bao giờ lưu mật khẩu gốc của khách. Tất cả được băm (hash) bằng **BCrypt** trước khi cất vào Database.
* Đăng nhập và phân quyền (Admin / Customer) thông qua **JWT Token**.
* **Bảo mật Giỏ hàng an toàn:** Khi khách nhét hoa vào giỏ, hệ thống tự động bóc `UserId` từ thẳng mã Token đang đăng nhập. Em tuyệt đối không nhận `UserId` từ Frontend gửi lên để chống việc hacker đổi ID tự ý mua hàng bằng tài khoản người khác.

### 4. Xử lý Thanh toán (Checkout) & Bắt lỗi
* **Giao dịch an toàn (Transaction):** Luồng đặt hàng phải trải qua 3 bước: *Tạo hóa đơn -> Trừ số lượng tồn kho -> Xóa giỏ hàng*. Em bọc cả 3 vào một `Transaction`. Nếu đang chạy mà lỗi (ví dụ kho báo hết hoa), toàn bộ hệ thống tự động `Rollback` về như cũ, tuyệt đối không bị rác dữ liệu hay mất tiền oan.
* **Bắt lỗi tập trung (Global Exception):** Em tự viết một Middleware đánh chặn ở ngoài cùng. Nếu app có sập hay dính bug ngầm, nó sẽ chặn lại, không để lòi code lỗi ra ngoài mà chỉ trả về một câu thông báo JSON lịch sự cho Frontend.
* Dữ liệu đầu vào (nhập giá tiền âm, bỏ trống tên...) đều bị tát văng ra ngay từ cổng Controller bằng **FluentValidation**.

## 🛠️ Hướng dẫn cài đặt & Chạy thử
1. Clone code về máy của bạn.
2. Tìm file `appsettings.example.json` và đổi tên nó thành `appsettings.json`.
3. Mở file đó ra, điền chuỗi kết nối SQL Server của bạn vào `DefaultConnection`. Ở phần `Jwt:Key`, hãy tự gõ một chuỗi mật khẩu bất kỳ dài hơn 32 ký tự.
4. Mở Package Manager Console, chọn Default Project là `FlowerShop.Infrastructure` và chạy lệnh: `Update-Database` để tạo các bảng trong SQL.
5. Bấm F5 để chạy app và test thử các chức năng qua giao diện Swagger.
