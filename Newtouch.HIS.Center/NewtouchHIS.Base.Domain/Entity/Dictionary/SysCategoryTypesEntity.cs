using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Base.Domain.Entity.Dictionary
{
    ///<summary>
    ///Nhóm thu phí lớn 
    ///Thông thường phân loại theo yêu cầu của cơ quan chủ quản cấp trên
    ///    (Phí đăng ký khám00,Phí khám chữa bệnh01,Phí thẻ từ02,Phí làm thẻ03 v.v.
    ///    BHYT ngoại trúMã varchar(2)
    ///    BHYT nội trúMã varchar(2) 
    ///    Lập lại bảng để đối ứng mã BHYT
    ///</summary>
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("xt_sfdl_lx", "Loại nhóm thu phí lớn")]
    public partial class SysCategoryTypesEntity : IEntity
    {

        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public String Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String OrganizeId { get; set; }

        /// <summary>
        /// Liên kếtChargeCateTypemục từ điển
        /// </summary>
        public String Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String dlCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? px { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String CreatorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String LastModifierCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String zt { get; set; }

    }
}
