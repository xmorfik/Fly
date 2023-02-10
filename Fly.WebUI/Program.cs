using Fly.WebUI;
using Fly.WebUI.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ApiConfiguration>(
    builder.Configuration.GetSection(ApiConfiguration.Configuration));

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.ConfigureIdentity();
builder.Services.AddControllersWithViews();
builder.Services.AddCustomLocalization();
builder.Services.AddServices();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddMapper();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRequestLocalization();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
