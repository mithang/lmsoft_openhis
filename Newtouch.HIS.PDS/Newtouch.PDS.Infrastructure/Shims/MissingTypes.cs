using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Application.Implementation;
using System;
using System.Collections.Generic;
using SqlSugar;

namespace Newtouch.HIS.Domain.Entity
{
    [SugarTable("Sys_Config")]
    public class SysConfigEntity : IEntity<SysConfigEntity>
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string OrganizeId { get; set; }
        public string zt { get; set; }
        public string CreatorCode { get; set; }
        public DateTime? CreateTime { get; set; }
        public string LastModifierCode { get; set; }
        public DateTime? LastModifyTime { get; set; }
    }

    [SugarTable("HospMedicinalOrderExecuteBatchNumber")]
    public class HospMedicinalOrderExecuteBatchNumberEntity : IEntity<HospMedicinalOrderExecuteBatchNumberEntity>
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }
    }

    [SugarTable("HospitalDrugRepercussionDetail")]
    public class HospitalDrugRepercussionDetailEntity : IEntity<HospitalDrugRepercussionDetailEntity>
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }
    }
}

namespace Newtouch.HIS.Domain.IRepository
{
    public interface ISysConfigRepo : IRepositoryBase<SysConfigEntity>
    {
        SysConfigEntity GetByCode(string code, string orgId);
        string GetValueByCode(string code, string orgId);
        int GetIntValueByCode(string code, string orgId, int defaultValue = 0);
        bool? GetBoolValueByCode(string code, string orgId, bool? defaultValue = null);
        IList<SysConfigEntity> GetList(string keyword, string organizeId);
        SysConfigEntity GetForm(string keyValue);
        void SubmitForm(SysConfigEntity entity, string keyValue);
        void SetValueByCode(string code, string value, string orgId);
    }
}

namespace Newtouch.HIS.Domain.IDomainServices
{
    // Base-module service used only as an (unused) field by SysUserApp in this
    // port; kept as an empty contract so the App compiles and DI can construct it.
    public interface ISysUserDmnService
    {
    }

    public interface IBaseDataDmnService
    {
        Newtouch.HIS.Domain.Entity.SysMedicineComplexVEntity GetYpDetails(string orgId, string ypCode);
    }
}

namespace Newtouch.HIS.DomainServices
{
    public class SysUserDmnService : DmnServiceBase, Newtouch.HIS.Domain.IDomainServices.ISysUserDmnService
    {
        public SysUserDmnService(FrameworkBase.MultiOrg.Infrastructure.IDefaultDatabaseFactory databaseFactory) : base(databaseFactory) { }
    }

    public class BaseDataDmnService : DmnServiceBase, Newtouch.HIS.Domain.IDomainServices.IBaseDataDmnService
    {
        public BaseDataDmnService(FrameworkBase.MultiOrg.Infrastructure.IDefaultDatabaseFactory databaseFactory) : base(databaseFactory) { }
        public Newtouch.HIS.Domain.Entity.SysMedicineComplexVEntity GetYpDetails(string orgId, string ypCode) => new Newtouch.HIS.DomainServices.SysMedicineExDmnService(new FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure.DefaultDatabaseFactory(), false).GetYpDetails(orgId, ypCode);
    }
}

namespace Newtouch.HIS.DomainServices.PurchaseWebService
{
    // Explicit-error stub for the WCF procurement client (YsxtMainService).
    // The original SOAP contract lives in generated Reference.cs (not ported);
    // regenerating it with dotnet-svcutil is future work. All purchase-sync
    // calls through this client fail with a clear message instead of a 404.
    public class YsxtMainServiceClient
    {
        public string sendRecv(string sUser, string sPwd, string sJgbm, string sVersion, string sXxlx, string sSign, string responsexml)
        {
            throw new NewtouchHIS.Lib.Base.Exceptions.FailedException("WCF_PROCUREMENT_NOT_PORTED", "阳光采购平台 WebService 客户端尚未移植到 .NET Core（YsxtMainService.sendRecv），请使用 .NET Framework 原系统调用该接口。");
        }
    }
}

namespace Newtouch.CIS.APIRequest.Inpatient
{
    public class InpatientBaseInfoRequest
    {
        public string zyh { get; set; }
        public string xm { get; set; }
        public string OrganizeId { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class InpatientBaseInfoDo
    {
        public string xm { get; set; }
        public string OrganizeId { get; set; }
        public string zyh { get; set; }
        public int zybz { get; set; }
    }
}

namespace NLog.Client
{
    public interface ILog
    {
        void Debug(string title, object message, Exception exception = null, Dictionary<string, string> tags = null);
        void Error(string title, object message, Exception exception = null, Dictionary<string, string> tags = null);
        void Fatal(string title, object message, Exception exception = null, Dictionary<string, string> tags = null);
        void Info(string title, object message, Exception exception = null, Dictionary<string, string> tags = null);
        void Warn(string title, object message, Exception exception = null, Dictionary<string, string> tags = null);
        void Moniter(string title, object message, NLog.Contact.DTO.MoniterDTO moniter, Dictionary<string, string> tags = null);
        void ChangeRecord(string title, object message, Exception exception = null, Dictionary<string, string> tags = null);
    }

    public interface ILogV2
    {
        void Debug(string title, object message, Exception exception = null, Dictionary<string, string> tags = null);
        void Error(string title, object message, Exception exception = null, Dictionary<string, string> tags = null);
        void Fatal(string title, object message, Exception exception = null, Dictionary<string, string> tags = null);
        void Info(string title, object message, Exception exception = null, Dictionary<string, string> tags = null);
        void Warn(string title, object message, Exception exception = null, Dictionary<string, string> tags = null);
        void Moniter(string title, object message, NLog.Contact.DTO.MoniterDTO moniter, Dictionary<string, string> tags = null);
    }

    public static class LogManageV2
    {
        public static ILogV2 GetLogger(string serviceName, string organizeId)
        {
            return new ConsoleLogV2(serviceName);
        }

        private class ConsoleLogV2 : ILogV2
        {
            private readonly string _name;
            public ConsoleLogV2(string name) { _name = name; }
            public void Debug(string title, object message, Exception exception = null, Dictionary<string, string> tags = null) { }
            public void Error(string title, object message, Exception exception = null, Dictionary<string, string> tags = null) { Console.WriteLine("[{0}] ERROR {1}: {2} {3}", _name, title, message, exception); }
            public void Fatal(string title, object message, Exception exception = null, Dictionary<string, string> tags = null) { Console.WriteLine("[{0}] FATAL {1}: {2} {3}", _name, title, message, exception); }
            public void Info(string title, object message, Exception exception = null, Dictionary<string, string> tags = null) { }
            public void Warn(string title, object message, Exception exception = null, Dictionary<string, string> tags = null) { }
            public void Moniter(string title, object message, NLog.Contact.DTO.MoniterDTO moniter, Dictionary<string, string> tags = null) { }
        }
    }
}

namespace NLog.Contact.DTO
{
    public class MoniterDTO
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
