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


Install TAILWINDCSS
https://tailwindcss.com/docs/installation/tailwind-cli

npm init -y
npm install -D tailwindcss postcss autoprefixer
npx tailwindcss init