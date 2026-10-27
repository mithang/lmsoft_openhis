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
    [SugarTable("V_C_Sys_ItemsDetail", "Bảng từ điển")]
    public partial class SysItemsDetailEntity : IEntity
    {

        /// <summary>
        /// 
        /// </summary>
        public System.String CateCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String Code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String TopOrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.String Id { get; set; }

    }
}
