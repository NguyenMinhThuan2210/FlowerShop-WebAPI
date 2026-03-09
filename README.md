# 🌸 FlowerShop Web API (RESTful)

Một dự án Backend Web API cho hệ thống cửa hàng bán hoa trực tuyến. Dự án được xây dựng với mục tiêu rèn luyện tư duy thiết kế kiến trúc chuẩn mực, tối ưu hóa truy vấn cơ sở dữ liệu và xử lý bảo mật cho hệ thống E-commerce.

## 🚀 Công nghệ & Thư viện sử dụng
* **Framework:** .NET 8 (C#) / ASP.NET Core Web API
* **Database:** SQL Server & Entity Framework Core
* **Bảo mật:** JWT (JSON Web Tokens), BCrypt.Net-Next
* **Validation:** FluentValidation
* **Kiến trúc:** Clean Architecture (Core - Infrastructure - API), Dependency Injection.

## 🔥 Các điểm nhấn kỹ thuật (Key Features & Highlights)

### 1. Kiến trúc & Thiết kế
* Áp dụng nguyên tắc **Separation of Concerns (SoC)** và **Dependency Rule**: Tầng `Core` chứa logic nghiệp vụ thuần túy, không phụ thuộc vào EF Core hay bất kỳ thư viện ngoại lai nào (BCrypt, JWT).
* Sử dụng **Specific Repository Pattern** thay vì Generic Repository để kiểm soát chặt chẽ từng câu lệnh SQL và tối ưu logic nghiệp vụ cho từng thực thể.
* **Manual Mapping (Không dùng AutoMapper):** Gán DTO thủ công kết hợp với `IQueryable` (Deferred Execution) và `.Select()` (Projection). Giúp EF Core tự động sinh câu lệnh `JOIN` tối ưu dưới Database, chỉ kéo đúng những cột cần thiết lên RAM.

### 2. Tối ưu Hiệu năng (Performance)
* **Phân trang dưới Database (Database-level Pagination):** Xử lý luồng Pagination và Search Filter hoàn toàn bằng `IQueryable` trước khi gọi `.ToListAsync()`, tránh tràn RAM (Memory Leak) khi thao tác với dữ liệu lớn.
* Áp dụng `AsNoTracking()` cho các truy vấn Read-Only để giảm tải cho EF Core Change Tracker.

### 3. Bảo mật & Xử lý người dùng
* Băm mật khẩu một chiều (Hashing) bằng thuật toán **BCrypt** (tự động sinh Salt).
* Cấp phát và xác thực **JWT Token**, phân quyền hệ thống cơ bản (Role-based: Admin & Customer).
* **Bảo mật luồng Giỏ hàng:** Trích xuất an toàn `UserId` từ `Claims` của JWT Token để ngăn chặn lỗ hổng IDOR (Insecure Direct Object Reference).

### 4. Giao dịch & Xử lý lỗi
* **Manual Transaction trong luồng Checkout:** Sử dụng `BeginTransactionAsync` để bọc các thao tác: *Tạo Đơn -> Trừ Tồn Kho -> Xóa Giỏ Hàng*. Đảm bảo nguyên tắc ACID (All-or-Nothing), tự động Rollback nếu xảy ra lỗi thiếu tồn kho hoặc sự cố hệ thống.
* **Global Exception Handling:** Cài đặt Middleware xử lý lỗi tập trung, chặn đứng mọi Exception không lường trước, ẩn Stack Trace và trả về JSON thống nhất cho Frontend.
* **FluentValidation:** Chặn các request rác (Data Invalid) ngay từ cổng Controller.

## 🛠️ Hướng dẫn chạy dự án
1. Copy file `appsettings.example.json` thành `appsettings.json`
2. Điền connection string và JWT key của bạn vào
3. Chạy Update-Database
4. Nhấn F5

