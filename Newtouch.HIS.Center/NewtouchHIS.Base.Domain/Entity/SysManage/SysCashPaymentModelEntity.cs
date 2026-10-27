using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Base.Domain.Entity.SysManage
{
    ///<summary>
    ///Bảng phương thức thanh toán tiền mặt
    ///</summary>
    [Tenant(DBEnum.SettDb)]
    [SugarTable("xt_xjzffs", "Phương thức thanh toán tiền mặt")]
    public partial class SysCashPaymentModelEntity : ISysEntity
    {
        /// <summary>
        /// Số hiệu phương thức thanh toán tiền mặt
        /// </summary>
        public int xjzffsbh { get; set; }

        /// <summary>
        /// Phương thức thanh toán tiền mặt
        /// </summary>
        public string xjzffs { get; set; }

        /// <summary>
        /// Tên phương thức thanh toán tiền mặt
        /// </summary>
        public string xjzffsmc { get; set; }

        /// <summary>
        /// Tính chất tài khoản
        /// </summary>
        public string zhxz { get; set; }

        /// <summary>
        /// Trạng thái
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Người tạoID
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Thời gian sửa đổi cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người sửa đổi cuốiID
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// Sắp xếp
        /// </summary>
        public int? px { get; set; }


    }
}
