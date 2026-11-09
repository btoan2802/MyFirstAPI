using Microsoft.EntityFrameworkCore;
using MyFirstAPI.Data;
using MyFirstAPI.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<TokenService>();

builder.Services.AddCors(options =>                          // 👈
{                                                            // 👈
    options.AddPolicy("AllowAngular", policy =>              // 👈
        policy.WithOrigins("http://localhost:4200")          // 👈
              .AllowAnyHeader()                              // 👈
              .AllowAnyMethod());                            // 👈
});                                                          // 👈

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("AllowAngular");                                 // 👈
app.MapControllers();
app.Run();