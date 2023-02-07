using Fly.WebAPI.Extensions;
using Fly.WebAPI.Hubs;
using Fly.WebAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddPostgres(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddAutoMapper();
builder.Services.AddAuthentication();
builder.Services.ConfigureCors();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureRedis(builder.Configuration);
builder.Services.AddSignalR();
builder.Services.AddControllers();
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
//    context.Database.EnsureCreated();
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

app.UseSwagger();
app.UseSwaggerUI(s =>
{
    s.SwaggerEndpoint("/swagger/v1/swagger.json", "Fly API v1");
});

app.MapControllers();
app.MapHub<LocationHub>("/locations");

app.Run();
