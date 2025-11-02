// ThienTriTue.Web/Program.cs
using OrchardCore.Logging;

var builder = WebApplication.CreateBuilder(args);

// Thêm dịch vụ Orchard Core
builder.Services
    .AddOrchardCore()
    .AddMvc()
    .AddSetupFeatures("OrchardCore.AutoSetup") // Cho phép setup tự động nếu muốn
    .AddSetupTheme("TheAgencyTheme"); // Tùy chọn, có thể dùng theme setup mặc định

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

// Sử dụng Orchard Core Middleware
app.UseOrchardCore();

app.Run();