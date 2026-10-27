using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;

namespace NewtouchHIS.Domain.Entity.CIS
{
    [Tenant(DBEnum.CisDb)]
    [SugarTable("xt_zyzd", "TCMDiagnosisEntity")]
    /// <summary>
    /// Chẩn đoán y học cổ truyền
    /// </summary>
    public class TCMDiagnosisEntity : IEntity
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string zyzdId { get; set; }

        /// <summary>
        /// Bảng khám liên kết
        /// </summary>
        public string jzId { get; set; }

        /// <summary>
        /// Enum 1 Chẩn đoán chính 2 Chẩn đoán phụ
        /// </summary>
        public int zdlx { get; set; }

        /// <summary>
        /// Liên kếtbaseCơ sở dữ liệu Bảng chẩn đoán hệ thống
        /// </summary>
        public string zdCode { get; set; }

        /// <summary>
        /// Trường dư thừa
        /// </summary>
        public string? zdmc { get; set; }

        /// <summary>
        /// Cờ nghi ngờ
        /// </summary>
        public string? ysbz { get; set; }

        /// <summary>
        /// Chứng hậuMã
        /// </summary>
        public string? zhCode { get; set; }

        /// <summary>
        /// Tên chứng hậu
        /// </summary>
        public string? zhmc { get; set; }
        /// <summary>
        /// Ghi chú chẩn đoán y học cổ truyền
        /// </summary>
        public string? zdbz { get; set; }
    }
}
