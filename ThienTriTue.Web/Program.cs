// ThienTriTue.Web/Program.cs
using OrchardCore.Logging;
// using OrchardCore.Admin;

var builder = WebApplication.CreateBuilder(args);

// Thêm dịch vụ Orchard Core
builder.Services.AddMvc();

builder.Services
    .AddOrchardCore()
    .AddSetupFeatures("OrchardCore.AutoSetup") // Cho phép setup tự động nếu muốn
    .ConfigureServices(services =>
    {
        // To set the admin theme, configure it in appsettings.json under "OrchardCore_Admin": { "Theme": "TheAgencyTheme" }
        // If you want to set it in code, use the correct property if available, otherwise remove this line.
    });

var app = builder.Build();

// Sử dụng các cấu hình cần thiết
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// BỔ SUNG QUAN TRỌNG: Cấu hình Middleware theo thứ tự chuẩn của ASP.NET Core 9.0
// Routing phải được gọi trước Authentication/Authorization
app.UseRouting();
app.UseAuthentication(); // Thường được Orchard Core xử lý ngầm, nhưng nên thêm
app.UseAuthorization();  // Dòng khắc phục lỗi chính!

// Sử dụng Orchard Core Middleware - Phải được gọi sau Authorization
app.UseOrchardCore();

app.Run();