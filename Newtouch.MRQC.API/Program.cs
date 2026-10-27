using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtouch.MRQC.API.Services;
using NewtouchHIS.Framework.Web.Implementation;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Services.HttpService;

var builder = WebApplication.CreateBuilder(args);

// BusinessDB + RedisConfig dùng chung file appsettings của MRQC Web (cùng deployment,
// cùng trust boundary; tránh copy credential sang file mới). Launcher vẫn override
// BusinessDB DefaultConn bằng env như Web (xem run-mrqc-api.sh).
// bin/Release/net6.0 -> up 4 = openhis-core, then sibling Newtouch.MRQC.Web.
var webAppsettings = Path.GetFullPath(Path.Combine(
    AppContext.BaseDirectory, "..", "..", "..", "..", "Newtouch.MRQC.Web", "appsettings.json"));
if (File.Exists(webAppsettings))
{
    builder.Configuration.AddJsonFile(webAppsettings, optional: true, reloadOnChange: false);
}
else
{
    Console.WriteLine("WARNING: MRQC Web appsettings not found at " + webAppsettings
        + " — BusinessDB/RedisConfig will be missing.");
}
// Web file is added AFTER the default env provider, so without this line it would
// shadow launcher env overrides (e.g. BusinessDB__DefaultConn with the live SA
// password). Re-adding env vars last restores the normal precedence: env > files.
builder.Configuration.AddEnvironmentVariables();

builder.WebHost.UseUrls("http://127.0.0.1:5320");

builder.Services.AddLibLogging();

// Same as MRQC Web: AddWebServiceBase auto-registers Framework AppServices that
// require these two singletons; without them the container fails validation.
builder.Services.AddSingleton<IHttpClientHelper, HttpClientHelper>();
builder.Services.AddSingleton<IAuthCenterAppService, AuthCenterAppService>();
builder.Services.AddWebServiceBase(builder.Configuration);

builder.Services.AddSingleton<TokenStore>();
builder.Services.AddScoped<ISysConfigRepo, SysConfigRepo>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});
builder.Services.AddControllers();

var app = builder.Build();
app.UseCors("AllowAll");
app.UseRouting();
app.MapControllers();
app.Run();
