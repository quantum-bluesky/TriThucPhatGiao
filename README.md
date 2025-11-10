# TriThucPhatGiao
## 🔄 Kịch bản Triển khai (Deployment)

Khi bạn triển khai trên máy chủ web mới (sau khi clone repository Git):

| Bước | Lệnh (Tại thư mục Solution) | Giải thích |
| :--- | :--- | :--- |
| **1. Khôi phục .NET** | `dotnet restore` | Tải xuống tất cả các gói NuGet, bao gồm Orchard Core và các dependencies khác, vào thư mục cache của máy chủ. |
| **2. Khôi phục Node** | `cd ThienTriTue.Web` rồi `npm install` | Tải xuống Tailwind CLI và các gói Node.js cần thiết. |
| **3. Publish (Bao gồm Tailwind)** | `dotnet publish -c Release` | Chạy lệnh `npm run build:css` (theo Target bạn đã thêm trong `.csproj`) **trước** khi đóng gói ứng dụng. Thư mục `publish/` lúc này sẽ chứa toàn bộ các file cần thiết (kể cả các file dll đã được khôi phục) và file CSS cuối cùng. |
| **4. Triển khai** | Chép thư mục `publish/` lên máy chủ IIS/Linux/Docker. | File DLL đã được sinh ra, chỉ cần chạy ứng dụng. |

Bằng cách này, bạn đã tối ưu hóa Git Repository, đảm bảo nó chỉ lưu trữ những gì thay đổi (mã nguồn của bạn), còn thư viện tĩnh sẽ được tái tạo trong quá trình Build/Publish.