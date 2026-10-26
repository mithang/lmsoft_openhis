using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;

namespace NewtouchHIS.Domain.Entity.CIS
{
    [Tenant(DBEnum.CisDb)]
    [SugarTable("xt_zyzd", "TCMDiagnosisEntity")]
    /// <summary>
    /// chẩn đoán y học cổ truyền
    /// </summary>
    public class TCMDiagnosisEntity : IEntity
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string zyzdId { get; set; }

        /// <summary>
        /// bảng khám bệnh liên kết
        /// </summary>
        public string jzId { get; set; }

        /// <summary>
        /// enum 1 chẩn đoán chính 2 chẩn đoán phụ
        /// </summary>
        public int zdlx { get; set; }

        /// <summary>
        /// liên kếtbasecơ sở dữ liệu bảng chẩn đoán hệ thống
        /// </summary>
        public string zdCode { get; set; }

        /// <summary>
        /// trường dự phòng
        /// </summary>
        public string? zdmc { get; set; }

        /// <summary>
        /// cờ nghi ngờ
        /// </summary>
        public string? ysbz { get; set; }

        /// <summary>
        /// mã chứng hậu
        /// </summary>
        public string? zhCode { get; set; }

        /// <summary>
        /// tên chứng hậu
        /// </summary>
        public string? zhmc { get; set; }
        /// <summary>
        /// ghi chú chẩn đoán y học cổ truyền
        /// </summary>
        public string? zdbz { get; set; }
    }
}
