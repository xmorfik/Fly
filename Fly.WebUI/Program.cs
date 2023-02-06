using Fly.Core.Services;
using Fly.WebUI;
using Fly.WebUI.Extensions;
using Fly.WebUI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<ApiConfiguration>(
    builder.Configuration.GetSection(ApiConfiguration.Configuration));

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = "Cookie";
        options.DefaultChallengeScheme = "oidc";
    })
    .AddCookie("Cookie")
    .AddOpenIdConnect("oidc", options =>
    {
        options.Authority = "https://localhost:5004";

        options.ClientId = "web1";
        options.ClientSecret = "secret";
        options.ResponseType = "code";
        options.SignedOutCallbackPath = "/home/index";

        options.SaveTokens = true;

        options.Scope.Clear();
        options.Scope.Add("openid");
        options.Scope.Add("offline_access");
        options.Scope.Add("api1");
    });

builder.Services.AddControllersWithViews();
builder.Services.AddCustomLocalization();
builder.Services.AddServices();

builder.Services.AddHttpClient();
builder.Services.AddHttpClient<ApiUriService>();

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
    pattern: "{controller=Home}/{action=Index}/{id?}").RequireAuthorization();

app.Run();
