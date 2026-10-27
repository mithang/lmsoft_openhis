using SqlSugar;
using System;
using System.Collections.Generic;
using NewtouchHIS.Lib.Base.Model;

namespace Newtouch.Sett.Domain
{
    /// <summary>
    /// Port of the original TreatmentportfolioEntity (table mz_gh_zlxmzh) and
    /// TreamentviewVO, plus ITreatmentportfolioRepo — the whole surface the
    /// original TreatmentController needs. Added by the Sett parity audit
    /// 2026-10-27: the controller was missing from the port although the live
    /// OutBook/RosterCalendarForm and Treatment/Index pages call its actions.
    /// </summary>
    [SugarTable("mz_gh_zlxmzh")]
    public class TreatmentportfolioEntity
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }
        public string OrganizeId { get; set; }
        public string zhmc { get; set; }
        public string zhcode { get; set; }
        public int? ord { get; set; }
        public string zlxm { get; set; }
        public string zlxmmc { get; set; }
        public string zt { get; set; }
        public decimal price { get; set; }
        public string CreatorCode { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
        public string zlxmpy { get; set; }
        public string sfdl { get; set; }
    }

    public class TreamentviewVO
    {
        public string OrganizeId { get; set; }
        public string Id { get; set; }
        public string zhmc { get; set; }
        public string zhcode { get; set; }
        public int? ord { get; set; }
        public string zlxm { get; set; }
        public string zlxmmc { get; set; }
        public string zt { get; set; }
        public decimal price { get; set; }
        public string CreatorCode { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
        public string zlxmpy { get; set; }
        public string sfdl { get; set; }
        public string dlmc { get; set; }
        public string zhmccode { get; set; }
    }

    public interface ITreatmentportfolioRepo
    {
        void ADDInsert(TreatmentportfolioEntity entity, string keyValue);
        void ADDrowdata(TreatmentportfolioEntity entity);
        void deleteid(string keyValue, string orgId);
        void deletemc(string keyValue, string zhcodemc, string orgId);
        IList<TreatmentportfolioEntity> TJchaxun(string zhcode, string sfxmmc, string orgId);
        IList<TreatmentportfolioEntity> Codecx(string orgId, string zhcode);
        IList<TreamentviewVO> Keyword(Pagination pagination, string keyword, string orgId);
    }
}
