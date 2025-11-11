# Hướng Dẫn Sử Dụng Tri Thức Phật Giáo

## 1. Giới thiệu
Chào mừng bạn đến với **Thư Viện Tổng Hợp Tri Thức** – dự án web xây dựng trên Orchard Core nhằm tập hợp, phân loại và chia sẻ các bài viết về giáo dục, Phật giáo và những câu chuyện nhân quả. Giao diện mặc định sử dụng theme tùy biến `ThienTriTue.Theme` với thiết kế Tailwind CSS hiện đại, dễ đọc và thân thiện với thiết bị di động.【F:ThienTriTue.Theme/Controllers/HomeController.cs†L7-L23】【F:ThienTriTue.Web/ThienTriTue.Theme/Views/Shared/_Layout.cshtml†L1-L66】

> **Miễn trừ trách nhiệm:** Bộ mã nguồn cung cấp sẵn tài khoản quản trị mặc định để tự động khởi tạo (Auto Setup). Vui lòng đổi ngay mật khẩu và thông tin liên hệ sau khi đăng nhập lần đầu nhằm đảm bảo an toàn. Tác giả không chịu trách nhiệm đối với mọi tổn thất phát sinh nếu bạn sử dụng tài khoản mặc định trong môi trường sản xuất.【F:ThienTriTue.Web/appsettings.json†L10-L24】

## 2. Bắt đầu
### 2.1 Yêu cầu hệ thống
- .NET SDK 8.0.121 (hoặc cao hơn tương thích) theo cấu hình `global.json` của dự án.【F:global.json†L1-L6】
- Node.js 18 LTS hoặc 20 LTS để chạy Tailwind CLI và chuỗi build CSS (đã kiểm thử với `@tailwindcss/cli` v4).【F:ThienTriTue.Web/package.json†L1-L34】
- SQLite được tích hợp sẵn, không cần cài đặt riêng.

### 2.2 Cài đặt
1. **Clone mã nguồn:**
   ```bash
   git clone <repository-url>
   cd TriThucPhatGiao
   ```
2. **Khôi phục gói .NET:**
   ```bash
   dotnet restore
   ```
3. **Cài đặt phụ thuộc Node.js:**
   ```bash
   cd ThienTriTue.Web
   npm install
   ```
4. **Biên dịch CSS Tailwind (tùy chọn trong phát triển):**
   ```bash
   npm run watch:css
   ```
   Lệnh sẽ theo dõi `Styles/app.css` và xuất file đã biên dịch vào `wwwroot/css/app.css`.【F:ThienTriTue.Web/package.json†L6-L16】
5. **Chạy website ở môi trường phát triển:**
   ```bash
   dotnet run
   ```
   Ứng dụng sẽ khởi động tại `https://localhost:5001` (hoặc cổng được hiển thị).

### 2.3 Tự động tạo site & tài khoản
- Orchard Core sẽ đọc cấu hình Auto Setup và tạo sẵn tenant **Default**, site name, dữ liệu SQLite và tài khoản quản trị `admin`/`123changePassplease!` ngay lần chạy đầu tiên.【F:ThienTriTue.Web/appsettings.json†L10-L24】
- Tập tin cơ sở dữ liệu được đặt tại `App_Data/Sites/Default/ThienTriTue.db`. Sao lưu định kỳ để tránh mất dữ liệu.【F:ThienTriTue.Web/appsettings.json†L18-L20】
- Sau khi đăng nhập, đổi mật khẩu và cập nhật thông tin website tại **Configuration → General**.

## 3. Tổng quan Giao diện
Giao diện người dùng gồm những thành phần chính sau:
- **Header**: Tiêu đề “Thư Viện Tổng Hợp Tri Thức & An Lạc” và mô tả ngắn, hiển thị trên mọi trang.【F:ThienTriTue.Web/ThienTriTue.Theme/Views/Shared/_Layout.cshtml†L28-L47】
- **Vùng nội dung chính**: Render nội dung từ Razor view hoặc hình dạng (shape) Orchard Core tại phần `Content`.【F:ThienTriTue.Web/ThienTriTue.Theme/Views/Shared/_Layout.cshtml†L7-L66】
- **Footer**: Dòng bản quyền mặc định năm 2025, có thể chỉnh sửa trong `_Layout.cshtml`.【F:ThienTriTue.Web/ThienTriTue.Theme/Views/Shared/_Layout.cshtml†L67-L76】
- **Bố cục trang con**: Mỗi chuyên mục (`/giao-duc`, `/phat-giao`, `/nhan-qua`) có header màu sắc riêng và danh sách bài viết dạng lưới 2 cột trên desktop.【F:ThienTriTue.Web/ThienTriTue.Theme/Views/Education/Index.cshtml†L1-L72】【F:ThienTriTue.Web/ThienTriTue.Theme/Views/Buddha/Index.cshtml†L1-L72】【F:ThienTriTue.Web/ThienTriTue.Theme/Views/Morals/Index.cshtml†L1-L72】

Các biểu tượng điều hướng (mũi tên quay lại, biểu tượng chủ đề) được xây dựng bằng SVG inline trong từng thẻ card, đảm bảo hiển thị sắc nét trên mọi độ phân giải.【F:ThienTriTue.Web/ThienTriTue.Theme/Views/Home/Index.cshtml†L10-L64】

## 4. Hướng dẫn Sử dụng Tính năng
### 4.1 Trang chủ – Thẻ điều hướng chuyên mục
- **Mục đích:** Giới thiệu ba chuyên mục chính và điều hướng người dùng đến nội dung chi tiết.【F:ThienTriTue.Web/ThienTriTue.Theme/Views/Home/Index.cshtml†L1-L64】
- **Các bước sử dụng:**
  1. Mở trang `/` sau khi website khởi động.
  2. Chọn thẻ **Khoa Học & Giáo Dục**, **Phật Giáo & Chánh Pháp** hoặc **Câu Chuyện Nhân Quả & Đạo Lý**.
  3. Nhấp nút “Xem …” để chuyển đến trang chuyên mục tương ứng.
- **Ví dụ:** Nhấp “Xem Phật Giáo” sẽ điều hướng tới `/phat-giao` với giao diện màu tím.
- **Mẹo:** Muốn chỉnh sửa nội dung thẻ, cập nhật trực tiếp file `Views/Home/Index.cshtml` hoặc tạo Widget trong Orchard và render shape thay cho HTML tĩnh.

### 4.2 Chuyên mục Khoa Học & Giáo Dục (`/giao-duc`)
- **Mục đích:** Trình bày các bài viết phân tích, blog về giáo dục, khoa học và kỹ năng học tập.【F:ThienTriTue.Web/ThienTriTue.Theme/Views/Education/Index.cshtml†L17-L70】
- **Các bước sử dụng:**
  1. Từ menu hoặc trang chủ, truy cập `/giao-duc`.
  2. Duyệt danh sách bài viết dạng card; mỗi card gồm tiêu đề, ngày đăng, tác giả và mô tả ngắn.
  3. Nhấp vào tiêu đề hoặc liên kết “Đọc tiếp” để mở URL chi tiết (có thể cấu hình bài viết động trong CMS).
- **Ví dụ:** Card “Nghiên cứu Chiến lược PCTT…” liên kết tới `/giao-duc/nghien-cuu-pctt`.
- **Lưu ý:** Thẻ cuối cùng là placeholder, hãy thay thế bằng nội dung động bằng cách truy vấn `ContentItems` trong Razor hoặc sử dụng Liquid/Widget.

### 4.3 Chuyên mục Phật Giáo & Chánh Pháp (`/phat-giao`)
- **Mục đích:** Chia sẻ giáo lý, thực hành thiền định và báo cáo nghiên cứu Phật học.【F:ThienTriTue.Web/ThienTriTue.Theme/Views/Buddha/Index.cshtml†L17-L70】
- **Các bước sử dụng:**
  1. Truy cập `/phat-giao`.
  2. Quan sát header màu tím và mô tả chuyên mục.
  3. Chọn bài viết phù hợp, nhấp “Đọc tiếp”.
- **Ví dụ:** “Phân Tích Tứ Diệu Đế” trỏ tới `/phat-giao/tu-dieu-de`.
- **Mẹo:** Dùng Orchard Admin → Content → Content Items để tạo các bài viết loại `Article` rồi chỉnh sửa view để render bằng `@await DisplayAsync(item)` cho dữ liệu động.

### 4.4 Chuyên mục Nhân Quả & Đạo Lý (`/nhan-qua`)
- **Mục đích:** Truyền tải câu chuyện nhân quả, đạo lý sống.【F:ThienTriTue.Web/ThienTriTue.Theme/Views/Morals/Index.cshtml†L17-L70】
- **Các bước sử dụng:**
  1. Truy cập `/nhan-qua`.
  2. Đọc các card nội dung về bài học nhân quả.
  3. Nhấp đường dẫn để xem chi tiết.
- **Ví dụ:** “Câu Chuyện Về Người Lái Đò…” liên kết `/nhan-qua/nguoi-lai-do`.
- **Lưu ý:** Bố cục card sử dụng lớp Tailwind `border-l-4` tạo điểm nhấn màu xanh; bạn có thể đổi màu trong file Razor để phù hợp nội dung.

### 4.5 Bố cục & Theme Tailwind
- **Mục đích:** Quản lý giao diện thống nhất, hỗ trợ responsive và định dạng typography.【F:ThienTriTue.Web/ThienTriTue.Theme/Views/Shared/_Layout.cshtml†L1-L76】【F:ThienTriTue.Web/main.css†L1-L80】
- **Các bước sử dụng:**
  1. Sửa đổi cấu trúc layout trong `_Layout.cshtml` nếu muốn thay đổi header, footer hoặc thêm vùng mới.
  2. Cập nhật `Styles/app.css` (hoặc file Tailwind tương đương) và chạy `npm run build:css` để sinh CSS tối ưu.【F:ThienTriTue.Web/package.json†L6-L12】
  3. Deploy file `wwwroot/css/app.css` cùng ứng dụng.
- **Mẹo:** Với phát triển dài hạn, dùng `npm run watch:css` để cập nhật CSS theo thời gian thực.

### 4.6 Quản trị Orchard Core
- **Mục đích:** Tạo nội dung, quản lý người dùng và cấu hình site dựa trên tính năng Orchard đã bật trong recipe.【F:ThienTriTue.Web/Recipes/thientritue.theme.recipe.json†L1-L37】
- **Các bước sử dụng:**
  1. Truy cập `/admin` và đăng nhập bằng tài khoản `admin`.
  2. Vào **Content → Content Items** để tạo bài viết (Page, Article…).
  3. Dùng **Design → Widgets** để gắn nội dung vào layout.
  4. Quản lý media tại **Assets** nhờ module `OrchardCore.Media`.
- **Ví dụ:** Tạo Content Type “Article”, thêm bài viết mới, sau đó chỉnh sửa view để hiển thị danh sách bằng cách sử dụng `DisplayAsync`.
- **Lưu ý:** Sau khi đổi mật khẩu, cập nhật `appsettings.json` nếu muốn thay thông tin Auto Setup cho môi trường mới.

## 5. Xử lý sự cố (Troubleshooting)
| Sự cố | Nguyên nhân khả dĩ | Cách khắc phục |
| --- | --- | --- |
| Không chạy được `dotnet run` | Sai phiên bản .NET so với `global.json` | Cài đặt .NET SDK 8.0.121 hoặc cập nhật `global.json` cho phù hợp môi trường.【F:global.json†L1-L6】 |
| CSS không cập nhật | Chưa build lại Tailwind sau khi sửa view | Chạy `npm run build:css` hoặc `npm run watch:css` trước khi refresh trình duyệt.【F:ThienTriTue.Web/package.json†L6-L12】 |
| Không đăng nhập được admin | Sử dụng mật khẩu mặc định đã đổi | Dùng tính năng **Forgot Password** (cấu hình SMTP) hoặc cập nhật mật khẩu trong database SQLite qua công cụ quản lý người dùng Orchard. |
| Auto Setup tạo sai thông tin | Cập nhật `appsettings.json` nhưng vẫn dùng cache | Xóa thư mục `App_Data/Sites/Default` trước khi chạy lại để buộc Orchard tạo tenant mới.【F:ThienTriTue.Web/appsettings.json†L18-L20】 |

## 6. Câu hỏi thường gặp (FAQ)
1. **Làm sao đổi tiêu đề website trên header?**  
   Vào `/admin` → **Configuration → Settings → General** để chỉnh “Site name”. Nếu muốn đổi slogan, sửa trực tiếp trong `_Layout.cshtml` dòng mô tả.【F:ThienTriTue.Web/ThienTriTue.Theme/Views/Shared/_Layout.cshtml†L28-L47】
2. **Có thể bổ sung thêm chuyên mục mới không?**  
   Có. Tạo controller/action hoặc Content Type mới, thêm view trong `ThienTriTue.Theme/Views/<TenChuyenMuc>/Index.cshtml` rồi khai báo route trong `HomeController`.【F:ThienTriTue.Theme/Controllers/HomeController.cs†L12-L23】
3. **Triển khai môi trường Production như thế nào?**  
   Thực hiện `dotnet publish -c Release` (tự động build CSS trước khi đóng gói), sau đó copy thư mục `publish/` lên máy chủ IIS/Linux/Docker theo hướng dẫn trong README.【F:README.md†L3-L18】
4. **Muốn thay đổi thông tin Auto Setup?**  
   Sửa phần `OrchardCore_AutoSetup` trong `appsettings.json`, xóa database cũ rồi chạy lại ứng dụng.【F:ThienTriTue.Web/appsettings.json†L10-L24】
5. **Tailwind CSS được cấu hình ở đâu?**  
   Cấu hình trong `tailwind.config.js` (nếu có) và chuỗi build được định nghĩa trong `package.json`. CSS đầu vào nằm ở `Styles/app.css` (hoặc `main_input.css`).【F:ThienTriTue.Web/package.json†L6-L16】【F:ThienTriTue.Web/main_input.css†L1-L1】

## 7. Thông tin Liên hệ & Hỗ trợ
- **Email hỗ trợ:** `quanntu@gmail.com` (tài khoản quản trị mặc định – thay đổi nếu bạn triển khai bản chính thức).【F:ThienTriTue.Web/appsettings.json†L15-L17】
- **Tài liệu & Hỏi đáp:** Tham khảo thêm trên trang chủ Orchard Core tại [https://docs.orchardcore.net/](https://docs.orchardcore.net/).
- **Đóng góp mã nguồn:** Gửi Pull Request hoặc Issue trực tiếp trên repository.

Chúc bạn triển khai thành công và xây dựng thư viện tri thức hữu ích cho cộng đồng!
