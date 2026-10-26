using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Base.Domain.Entity.SysManage
{
    ///<summary>
    /// vị trí hệ thống
    ///</summary>
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("V_S_xt_gj", "国籍")]
    public partial class SysNationalityVEntity : ISysEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        public int gjId { get; set; }

        /// <summary>
        /// mã
        /// </summary>
        public string gjCode { get; set; }

        /// <summary>
        /// tên
        /// </summary>
        public string gjmc { get; set; }

        /// <summary>
        /// chữ cái đầu của phiên âm
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// cờ hiệu lực
        /// </summary>
        public string zt { get; set; }
    }
}
