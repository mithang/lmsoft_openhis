using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Base.Domain.EnumExtend
{
    /// <summary>
    /// Loại ghi nhật ký
    /// </summary>
    public enum DbLogType
    {
        /// <summary>
        /// Khác
        /// </summary>
        [Description("Khác")]
        Other = 0,
        /// <summary>
        /// Đăng nhập
        /// </summary>
        [Description("Đăng nhập")]
        Login = 1,
        /// <summary>
        /// Thoát
        /// </summary>
        [Description("Thoát")]
        Exit = 2,
        /// <summary>
        /// Truy cập
        /// </summary>
        [Description("Truy cập")]
        Visit = 3,
        /// <summary>
        /// Thêm mới
        /// </summary>
        [Description("Thêm mới")]
        Create = 4,
        /// <summary>
        /// Xóa
        /// </summary>
        [Description("Xóa")]
        Delete = 5,
        /// <summary>
        /// Sửa đổi
        /// </summary>
        [Description("Sửa đổi")]
        Update = 6,
        /// <summary>
        /// Nộp
        /// </summary>
        [Description("Nộp")]
        Submit = 7,
        /// <summary>
        /// Ngoại lệ
        /// </summary>
        [Description("Ngoại lệ")]
        Exception = 8,
    }

    /// <summary>
    /// Loại đích của menu
    /// </summary>
    public enum EnumModuleTargetType
    {
        /// <summary>
        /// Không có trang
        /// </summary>
        expand,
        /// <summary>
        /// Trang khung
        /// </summary>
        iframe,
        /// <summary>
        /// Trang bật lên
        /// </summary>
        open,
        /// <summary>
        /// Cửa sổ mới
        /// </summary>
        blank,
        /// <summary>
        /// Trang con(Không hiển thị trong menu)
        /// </summary>
        subpage,
        /// <summary>
        /// Cửa sổ hiện tại
        /// </summary>
        toplocation,
    }


    public enum EnumDBJoinFuncType
    {
        Equals=1
    }
}
