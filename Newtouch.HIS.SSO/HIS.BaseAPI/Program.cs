using Microsoft.AspNetCore.Mvc.Authorization;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Filter;
using NewtouchHIS.Lib.Base.Model;

var builder = WebApplication.CreateBuilder(args);

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
    Console.WriteLine("Warnning:SSOAuthIdentity ≈‰÷√“Ï≥£");
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
