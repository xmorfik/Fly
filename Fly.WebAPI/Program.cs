using Fly.WebAPI.Extensions;
using Fly.WebAPI.Hubs;
using Fly.WebAPI.Middlewares;
using Hangfire;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddSqlServer(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddMapper();
builder.Services.AddAuthentication();
builder.Services.ConfigureCors(builder.Configuration);
builder.Services.ConfigureIdentity();
builder.Services.ConfigureRedis(builder.Configuration);
builder.Services.AddSignalR();
builder.Services.ConfigureHangfire(builder.Configuration);

builder.Services.AddControllers().AddNewtonsoftJson(x =>
{
    x.SerializerSettings.MaxDepth = 1;
    x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();

var app = builder.Build();

var logger = LoggerFactory.Create(config =>
{
    config.AddConsole();
}).CreateLogger("ExceptionHandler");
app.ConfigureExceptionHandler(logger);

//using (var scope = app.Services.CreateScope())
//{
//    var context = scope.ServiceProvider.GetRequiredService<FlyDbContext>();
//    context.
//    //context.Database.EnsureCreated();
//}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //builder.Configuration.AddUserSecrets<Program>();
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.Use(async (context, next) =>
{
    Thread.CurrentPrincipal = context.User;
    await next(context);
});

app.UseSwagger();
app.UseSwaggerUI(s =>
{
    s.SwaggerEndpoint("/swagger/v1/swagger.json", "Fly API v1");
});

app.MapControllers();
app.MapHub<LocationHub>("/locations");

app.UseHangfireDashboard("/dashboard");

app.Run();
