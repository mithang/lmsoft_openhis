using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Base.Domain.Entity.SysManage
{
    ///<summary>
    ///bảng phương thức thanh toán tiền mặt
    ///</summary>
    [Tenant(DBEnum.SettDb)]
    [SugarTable("xt_xjzffs", "现金支付方式")]
    public partial class SysCashPaymentModelEntity : ISysEntity
    {
        /// <summary>
        /// số hiệu phương thức thanh toán tiền mặt
        /// </summary>
        public int xjzffsbh { get; set; }

        /// <summary>
        /// phương thức thanh toán tiền mặt
        /// </summary>
        public string xjzffs { get; set; }

        /// <summary>
        /// tên phương thức thanh toán tiền mặt
        /// </summary>
        public string xjzffsmc { get; set; }

        /// <summary>
        /// tính chất tài khoản
        /// </summary>
        public string zhxz { get; set; }

        /// <summary>
        /// trạng thái
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// người tạoID
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// thời gian sửa đổi lần cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// người sửa đổi lần cuốiID
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// sắp xếp
        /// </summary>
        public int? px { get; set; }


    }
}
