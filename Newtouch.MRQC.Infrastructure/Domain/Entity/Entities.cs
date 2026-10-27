using SqlSugar;
using System;

namespace Newtouch.MRQC.Domain.Entity.QcItemManage
{
    /// <summary>质控病历申请 (Mr_Apply)</summary>
    [SugarTable("Mr_Apply")]
    public class MrApplyEntity
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }
        public string OrganizeId { get; set; }
        public string ApplyDept { get; set; }
        public string Bed { get; set; }
        public string PatName { get; set; }
        public string ApplyDoctor { get; set; }
        public int ApplyType { get; set; }
        public string MedicalName { get; set; }
        public DateTime CompletionDate { get; set; }
        public DateTime ApplyDate { get; set; }
        public DateTime ApplyCompletionDate { get; set; }
        public string ApplyReason { get; set; }
        public int ApplyStatus { get; set; }
        public string Approver { get; set; }
        public string ApproverDept { get; set; }
        public string ApprovalDate { get; set; }
        public DateTime? Ryrq { get; set; }
        public DateTime? Cyrq { get; set; }
        public int? Px { get; set; }
        public string zt { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreatorCode { get; set; }
        public string LastModifierCode { get; set; }
        public DateTime? LastModifyTime { get; set; }
    }

    /// <summary>病历评分 (Mr_Qc_Score)</summary>
    [SugarTable("Mr_Qc_Score")]
    public class MrQcScoreEntity
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }
        public string Zyh { get; set; }
        public string BllxId { get; set; }
        public string Blmc { get; set; }
        public string ScoreCode { get; set; }
        public string ScoreMc { get; set; }
        public decimal? ScoreMcValue { get; set; }
        public int? sl { get; set; }
        public int? Px { get; set; }
        public string zt { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreatorCode { get; set; }
        public string LastModifierCode { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string OrganizeId { get; set; }
    }

    /// <summary>质控项 (MR_Qc_ItemData)</summary>
    [SugarTable("MR_Qc_ItemData")]
    public class QcItemDataEntity
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        public string BlmbId { get; set; }
        public string Bllmbmc { get; set; }
        public string zklx { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Score { get; set; }
        public string Remark { get; set; }
        public int? Px { get; set; }
        public string zt { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreatorCode { get; set; }
        public string LastModifierCode { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string OrganizeId { get; set; }
    }
}
