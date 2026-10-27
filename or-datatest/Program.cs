// READ-ONLY data-layer verification for the OR port (no writes, no schema changes).
// Loads the OR Web appsettings.json (same connection strings as the running app),
// initializes the shared Lib stack via AddWebServiceBase, then exercises the same
// repo/dmnservice queries the grid actions call, with the demo hospital org id.
using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewtouchHIS.Framework.Web.Implementation;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.OR.ManageSystem.Domain.IDomainServices;
using Newtouch.OR.ManageSystem.Domain.IRepository;
using Newtouch.OR.ManageSystem.DomainServices;
using Newtouch.OR.ManageSystem.Repository;

var config = new ConfigurationBuilder()
    .AddJsonFile("/home/hatch/workspace/openhis-core/Newtouch.HIS.OR/Newtouch.OR.Web/appsettings.json", optional: false)
    .AddEnvironmentVariables()
    .Build();

var services = new ServiceCollection();
services.AddSingleton<IConfiguration>(config);
services.AddWebServiceBase(config);
services.AddScoped<ICommonDmnService, CommonDmnService>();
services.AddScoped<IOPApplyDmnService, OPApplyDmnService>();
services.AddScoped<IOPRegisterDmnService, OPRegisterDmnService>();
services.AddScoped<IOpArrangementDmnService, OpArrangementDmnService>();
services.AddScoped<ISysChargeItemDmnService, SysChargeItemDmnService>();
services.AddScoped<IORApplyInfoRepo, ORApplyInfoRepo>();
services.AddScoped<IORApplyInfoExpandRepo, ORApplyInfoExpandRepo>();
services.AddScoped<IORArrangementRepo, ORArrangementRepo>();
services.AddScoped<IORRegistrationRepo, ORRegistrationRepo>();
services.AddScoped<IOROperationRepo, OROperationRepo>();
services.AddScoped<IORRoomRepo, ORRoomRepo>();
services.AddScoped<IORStaffRepo, ORStaffRepo>();
services.AddScoped<IORAnesthesiaRepo, ORAnesthesiaRepo>();
services.AddScoped<IORNotchGradeRepo, ORNotchGradeRepo>();
services.AddScoped<IOROpStaffRecordRepo, OROpStaffRecordRepo>();
services.AddScoped<ITemporary_ordersERepo, Temporary_ordersRepo>();
services.AddScoped<ISysConfigRepo, SysConfigRepo>();
services.AddScoped<ISysStaffRepo, SysStaffRepo>();
services.AddScoped<ISysDiagnosisRepo, SysDiagnosisRepo>();

var sp = services.BuildServiceProvider();
const string org = "6d5752a7-234a-403e-aa1c-df8b45d3469f";
using var scope = sp.CreateScope();
var p = scope.ServiceProvider;

var opRepo = p.GetRequiredService<IOROperationRepo>();
var ops = opRepo.OpList(org, "", false);
Console.WriteLine($"[1] OROperationRepo.OpList(org, all): {ops.Count} rows; first: {(ops.Count > 0 ? ops[0].ssmc ?? ops[0].ssdm : "(none)")}");

var common = p.GetRequiredService<ICommonDmnService>();
var dic = common.GetOperations("", org);
Console.WriteLine($"[2] CommonDmnService.GetOperations(all): {dic.Count} rows; first: {(dic.Count > 0 ? dic[0].ssmc : "(none)")}");

var diag = p.GetRequiredService<ISysDiagnosisRepo>();
var diags = diag.GetList(org, "", "", "");
Console.WriteLine($"[3] SysDiagnosisRepo.GetList(all): {diags.Count} rows; first: {(diags.Count > 0 ? diags[0].zdmc : "(none)")}");

var applyRepo = p.GetRequiredService<IORApplyInfoRepo>();
var applies = applyRepo.IQueryable(a => a.OrganizeId == org && a.zt == "1").ToList();
Console.WriteLine($"[4] ORApplyInfoRepo.FindList(org, zt=1): {applies.Count} rows");

var staff = common.GetStaffByDutyCode(org, "Doctor");
Console.WriteLine($"[5] CommonDmnService.GetStaffByDutyCode(Doctor): {staff.Count} rows; first: {(staff.Count > 0 ? staff[0].StaffName : "(none)")}");

var roomRepo = p.GetRequiredService<IORRoomRepo>();
var rooms = roomRepo.IQueryable(r => r.OrganizeId == org && r.zt == "1").ToList();
Console.WriteLine($"[6] ORRoomRepo.FindList(org, zt=1): {rooms.Count} rows; first: {(rooms.Count > 0 ? rooms[0].Name : "(none)")}");

Console.WriteLine("DATATEST OK (all queries read-only)");
