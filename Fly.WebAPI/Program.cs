using Fly.WebAPI.Extensions;
using Fly.WebAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddPostgres(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddAutoMapper();
builder.Services.ConfigureCors();
builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJwtBearer(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var logger = LoggerFactory.Create(config =>
{
    config.AddConsole();
}).CreateLogger("ExceptionHandler");
app.ConfigureExceptionHandler(logger);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //builder.Configuration.AddUserSecrets<Program>();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
