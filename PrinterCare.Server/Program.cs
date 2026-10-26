using Microsoft.EntityFrameworkCore;
using PrinterCare.Server.Data;
using PrinterCare.Server.Extentions;

var builder = WebApplication.CreateBuilder(args);
//Подключение БД
var connectionString = builder.Configuration.GetConnectionString("printersDB")
    ?? throw new InvalidOperationException("Connection string 'printersDB' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(o => o.UseNpgsql(connectionString));

builder.Services.AddApplicationServices(); // регистрации репозиториев и сервисов

builder.Services.AddControllers();
builder.Services.AddProblemDetails();
builder.Services.AddSwaggerGen();

var allowedOrigins = builder.Configuration.GetSection("Cors:Origins").Get<string[]>() ?? [];
builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactPolicy", policy => policy
        .WithOrigins(allowedOrigins)
        .AllowAnyHeader()
        .AllowAnyMethod());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using var scope = app.Services.CreateScope();
    scope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.Migrate();
}
else
{
    app.UseExceptionHandler();
}

app.UseHttpsRedirection();
app.UseDefaultFiles();
app.MapStaticAssets();

app.UseCors("ReactPolicy");
app.UseAuthorization();

app.MapControllers();
app.MapFallbackToFile("/index.html");

app.Run();
