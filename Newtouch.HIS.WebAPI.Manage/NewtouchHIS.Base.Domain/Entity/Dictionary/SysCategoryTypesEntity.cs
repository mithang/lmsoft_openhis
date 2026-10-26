using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Base.Domain.Entity.Dictionary
{
    ///<summary>
    ///nhóm thu phí lớn 
    ///phân loại chung theo yêu cầu của cơ quan chủ quản cấp trên
    ///    （phí đăng ký khám00、phí khám chữa bệnh01、phí thẻ từ02、phí làm thẻ03 v.v.
    ///    mã bảo hiểm y tế ngoại trú varchar(2)
    ///    mã bảo hiểm y tế nội trú varchar(2) 
    ///    tạo lại bảng để đối ứng mã bảo hiểm y tế
    ///</summary>
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("xt_sfdl_lx", "收费大类类型")]
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
        /// liên kếtChargeCateTypehạng mục từ điển của
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
