using System;
using System.Collections.Generic;

namespace Newtouch.Sett.Domain
{
    // Source-true VOs copied from Newtouch.Domain/ValueObjects/SystemManage/
    // (jgssEarningVO.cs, jgssEarningGridVO.cs, jgssCostVO.cs). Property names
    // kept verbatim: they are JSON field names used by SiteStatistics views.
    public class jgssEarningVO
    {
        public string Id { get; set; }
        public string siteId { get; set; }
        public string zdmc { get; set; }
        public string year { get; set; }
        public string month { get; set; }
        public decimal jgss { get; set; }
        public decimal grsss { get; set; }
        public string shzt { get; set; }
        public decimal zsr { get; set; }
        public decimal mzsfzje { get; set; }
        public decimal zysfzje { get; set; }
        public string shr { get; set; }
        public string cjr { get; set; }
        public string jgssfdbl { get; set; }
    }

    public class jgssAttachmentVO
    {
        public string Id { get; set; }
        public string fjmc { get; set; }
        public string ContentType { get; set; }
        public string fjPath { get; set; }
    }

    public class jgssEarningGridVO
    {
        public decimal mzsfje { get; set; }
        public decimal mzzxje { get; set; }
        public decimal zysfje { get; set; }
        public decimal zyzxje { get; set; }
        public decimal zsr { get; set; }
        public string dlmc { get; set; }
        public string dlCode { get; set; }
        public decimal hssr { get; set; }
        public decimal ce { get; set; }
        public string tzsm { get; set; }
        public decimal fcbl { get; set; }
    }

    public class jgssCostVO
    {
        public string Id { get; set; }
        public string cblb { get; set; }
        public string kmcode { get; set; }
        public string lbmc { get; set; }
        public string kmmc { get; set; }
        public decimal je { get; set; }
    }

    public class jgssandgrsssVO
    {
        public decimal jgss { get; set; }
        public decimal grsss { get; set; }
    }

    // Source: Newtouch.Domain/ValueObjects/SystemManage/SiteCostEarningVO.cs
    public class SiteCostEarningVO
    {
        public jgssEarningVO srxx { get; set; }
        public List<jgssEarningGridVO> srxxList { get; set; }
        public List<jgssCostVO> cbxxList { get; set; }
        public List<jgssAttachmentVO> fjxx { get; set; }
    }

    // Source: Newtouch.Domain/Entity/SystemManage/jgss_AttachmentInfoEntity.cs
    // (IEntity base dropped: merged Infra uses plain POCOs.)
    public class jgss_AttachmentInfoEntity
    {
        public string Id { get; set; }
        public string srxxId { get; set; }
        public string fjmc { get; set; }
        public string ContentType { get; set; }
        public string fjPath { get; set; }
        public string CreatorCode { get; set; }
        public DateTime CreateTime { get; set; }
        public string LastModifierCode { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string zt { get; set; }
    }

    // Source: Newtouch.Domain/Entity/SystemManage/jgss_EarningInfoEntity.cs
    public class jgss_EarningInfoEntity
    {
        public string Id { get; set; }
        public string siteId { get; set; }
        public string year { get; set; }
        public string month { get; set; }
        public decimal jgss { get; set; }
        public decimal grsss { get; set; }
        public string fjmc { get; set; }
        public string ContentType { get; set; }
        public string fjPath { get; set; }
        public string CreatorCode { get; set; }
        public DateTime CreateTime { get; set; }
        public string LastModifierCode { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string zt { get; set; }
        public string shzt { get; set; }
        public decimal mzsfzje { get; set; }
        public decimal zysfzje { get; set; }
        public decimal zsr { get; set; }
        public string shr { get; set; }
        public DateTime? shsj { get; set; }
    }

    // Source: Newtouch.Domain/Entity/V/SysOrganizeVEntity.cs (shared org view
    // entity; also used by SystemManage later — defined once, here).
    public class SysOrganizeVEntity
    {
        public string Id { get; set; }
        public string ParentId { get; set; }
        public string TopOrganizeId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string CategoryCode { get; set; }
        public string zt { get; set; }
    }
}
