// ThienTriTue.Web/Program.cs
using OrchardCore.Logging;

var builder = WebApplication.CreateBuilder(args);

// Enable NLog integration provided by Orchard Core packages.
builder.Host.UseNLogHost();

// Register Orchard Core CMS services which include MVC, Razor Pages and the
// Orchard middleware pipeline.
builder.Services.AddOrchardCms();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Orchard Core handles routing internally once this middleware is registered.
app.UseOrchardCore();

app.Run();
