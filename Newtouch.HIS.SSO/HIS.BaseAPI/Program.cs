using Microsoft.AspNetCore.Mvc.Authorization;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Filter;
using NewtouchHIS.Lib.Base.Model;

var builder = WebApplication.CreateBuilder(args);

// --- Việt Nam hóa: culture mặc định vi-VN -------------------------------------
// Đặt DefaultThreadCurrentCulture/UICulture = vi-VN để:
//   * model binding parse ngày theo dd/MM/yyyy (thay vì en-US MM/dd/yyyy),
//   * decimal dùng dấu '.' phân cách hàng nghìn / ',' thập phân,
//   * ToString("d") và {0:d} render đúng định dạng Việt Nam.
// Không đổi AddControllers/AddControllersWithViews hiện có; nếu muốn thông báo
// lỗi model binding tiếng Việt thì bổ sung options.ModelBindingMessageProvider.
var vi = new System.Globalization.CultureInfo("vi-VN");
System.Globalization.CultureInfo.DefaultThreadCurrentCulture = vi;
System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = vi;
// ---------------------------------------------------------------------------


// Add services to the container.
builder.Services.AddInterfaceServiceBase(builder.Configuration);
builder.Services.AddControllers();
//Tat ca cac controller deu khoi dong xac thuc danh tinh
builder.Services.AddMvc(options =>
{
    options.Filters.Add(new AppResultFilter());
    options.Filters.Add(typeof(LogTrackingAttribute));
    options.Filters.Add(new AuthorizeFilter());//Tat ca cac dich vu MVC mac dinh them nhan uy quyen
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwagger();
builder.Services.AddSwaggerGenNewtonsoftSupport();
var jwtOptions = builder.Configuration.GetSection("SSOAuthIdentity")?.Get<List<SSOAuthIdentity>>();
if (jwtOptions != null)
{
    builder.Services.JWTAuthentication(true, jwtOptions?.FirstOrDefault());
}
else
{
    Console.WriteLine("Warnning:SSOAuthIdentity cấu hình bất thường");
}
builder.Services.AddCorsService();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
#region jwt
app.UseAuthentication();        //Them xac thuc jwt 
//Code thuc thi tu tren xuong duoi, o giua co the them dieu kien kiem tra, van de cai dat quyen
#endregion
app.UseAuthorization();
app.UseNewtouchExceptionHandler();
app.MapControllers();
app.UseCors("Cors");
app.Run();
