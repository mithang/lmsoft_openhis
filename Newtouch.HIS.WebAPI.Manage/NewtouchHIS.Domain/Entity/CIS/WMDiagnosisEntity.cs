using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;

namespace NewtouchHIS.Domain.Entity.CIS
{
    [Tenant(DBEnum.CisDb)]
    [SugarTable("xt_xyzd", "WMDiagnosisEntity")]
    /// <summary>
    /// chẩn đoán Tây y
    /// </summary>
    public class WMDiagnosisEntity:IEntity
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string xyzdId { get; set; }


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
        public string zdmc { get; set; }

        /// <summary>
        /// cờ nghi ngờ
        /// </summary>
        public string? ysbz { get; set; }
        /// <summary>
        /// ghi chú chẩn đoán Tây y
        /// </summary>
        public string? zdbz { get; set; }
        ///// <summary>
        ///// loại sơ đồ răng
        ///// </summary>
        //public string ywlx { get; set; }
        ///// <summary>
        ///// vị trí sơ đồ răng
        ///// </summary>
        //public string ywstr { get; set; }
        ///// <summary>
        ///// hiển thị sơ đồ răng
        ///// </summary>
        //public string ywxs { get; set; }
    }
}
