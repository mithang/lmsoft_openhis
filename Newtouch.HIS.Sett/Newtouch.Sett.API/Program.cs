using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtouch.Sett.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Cổng cố định 7002 (config production của 8016 trỏ tới localhost:7002).
// Override chỉ phục vụ kiểm chứng: 8016 đang chạy với
// ASPNETCORE_ENVIRONMENT=Development nên config Development của nó trỏ sang
// localhost:17361; đặt SETT_API_URLS=http://127.0.0.1:17361 để chạy một
// instance kiểm chứng tạm thời trên 17361 mà không sửa config 8016.
// Mặc định (không có biến này) luôn là 7002.
var urls = Environment.GetEnvironmentVariable("SETT_API_URLS");
builder.WebHost.UseUrls(string.IsNullOrWhiteSpace(urls) ? "http://127.0.0.1:7002" : urls);

builder.Services.AddSingleton<SettDb>();
builder.Services.AddSingleton<BookingService>();
builder.Services.AddSingleton<PatientAnonService>();
builder.Services.AddSingleton<OrderService>();

// Newtonsoft bắt buộc: BookingRequest.paradata là JObject, System.Text.Json
// không deserialise được; 8016 cũng serialise bằng Newtonsoft.
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

app.UseRouting();
app.UseCors();

// Liveness cho watchdog / kiểm chứng cổng (không cần xác thực).
app.MapGet("/", () => Results.Json(new { service = "Newtouch.Sett.API", status = "ok", urls = string.IsNullOrWhiteSpace(urls) ? "http://127.0.0.1:7002" : urls }));

app.MapControllers();

app.Run();
