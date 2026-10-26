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
    [SugarTable("V_C_Sys_ItemsDetail", "字典表")]
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
