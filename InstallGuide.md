# 1. Tạo thư mục gốc và Solution
mkdir ThienTriTueProject
cd ThienTriTueProject
dotnet new sln -n ThienTriTue

# 2. Tạo Project Host (ThienTriTue.Web)
dotnet new web -n ThienTriTue.Web
dotnet sln add ThienTriTue.Web/ThienTriTue.Web.csproj
cd ThienTriTue.Web

# 3. Thêm các gói NuGet (đã làm ở bước trước)
dotnet add package OrchardCore.Application.Targets
dotnet add package OrchardCore.Logging.NLog
dotnet add package OrchardCore.Admin

# 4. Tạo thư mục Styles
mkdir Styles

# 5. Tạo Project Theme (ThienTriTue.Theme)
cd .. # Quay lại ThienTriTueProject
dotnet new classlib -n ThienTriTue.Theme
dotnet sln add ThienTriTue.Theme/ThienTriTue.Theme.csproj
cd ThienTriTue.Theme

# 6. Thêm gói NuGet cho Theme
dotnet add package OrchardCore.DisplayManagement.Targets

# 7. Tạo thư mục Views
mkdir Views

# 8. Liên kết Project Theme vào Project Host
cd ../ThienTriTue.Web
dotnet add reference ../ThienTriTue.Theme/ThienTriTue.Theme.csproj



# Install TAILWINDCSS
## 1. Cài tailwind-cli (Simplest)
https://tailwindcss.com/docs/installation/tailwind-cli

## 2. Cài postcss
https://codetot.net/postcss/
npm install -g gulp
https://tailwindcss.com/docs/installation/using-postcss

npm init -y
npm install -D tailwindcss postcss autoprefixer
npx tailwindcss init

## 🔄 Kịch bản Triển khai (Deployment)

Khi bạn triển khai trên máy chủ web mới (sau khi clone repository Git):

| Bước | Lệnh (Tại thư mục Solution) | Giải thích |
| :--- | :--- | :--- |
| **1. Khôi phục .NET** | `dotnet restore` | Tải xuống tất cả các gói NuGet, bao gồm Orchard Core và các dependencies khác, vào thư mục cache của máy chủ. |
| **2. Khôi phục Node** | `cd ThienTriTue.Web` rồi `npm install` | Tải xuống Tailwind CLI và các gói Node.js cần thiết. |
| **3. Publish (Bao gồm Tailwind)** | `dotnet publish -c Release` | Chạy lệnh `npm run build:css` (theo Target bạn đã thêm trong `.csproj`) **trước** khi đóng gói ứng dụng. Thư mục `publish/` lúc này sẽ chứa toàn bộ các file cần thiết (kể cả các file dll đã được khôi phục) và file CSS cuối cùng. |
| **4. Triển khai** | Chép thư mục `publish/` lên máy chủ IIS/Linux/Docker. | File DLL đã được sinh ra, chỉ cần chạy ứng dụng. |
