using Microsoft.EntityFrameworkCore;
using MyStore.Data;
using MyStore.Middleware;
using MyStore.Repositories;
using MyStore.Services;
using Npgsql.EntityFrameworkCore.PostgreSQL; // не обязательно явно, но для надёжности

var builder = WebApplication.CreateBuilder(args);

// 1. Регистрация сервисов
builder.Services.AddControllers();                    // Включаем API-контроллеры
builder.Services.AddEndpointsApiExplorer();           // Включаем Swagger
builder.Services.AddSwaggerGen();                     // Генерация документации

// 2. Подключение к PostgreSQL через Entity Framework
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// 3. AutoMapper — для маппинга моделей в DTO (пока только регистрируем)
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

// 4. CORS — разрешаем React-фронтенду обращаться к API
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:3000")   // здесь порт вашего React-приложения
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();
// 5. Настройка middleware 
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();     // Перенаправляем HTTP на HTTPS
app.UseCors("AllowReactApp");  // Включаем CORS (до UseAuthorization)
app.UseAuthorization();        // Авторизация (пока не используется, но оставим)
app.MapControllers();          // Активируем маршруты контроллеров

app.Run();                     // Запускаем сервер