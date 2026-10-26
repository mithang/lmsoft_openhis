using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Base.Domain.EnumExtend
{
    /// <summary>
    /// loại ghi nhật ký
    /// </summary>
    public enum DbLogType
    {
        /// <summary>
        /// khác
        /// </summary>
        [Description("其他")]
        Other = 0,
        /// <summary>
        /// đăng nhập
        /// </summary>
        [Description("登录")]
        Login = 1,
        /// <summary>
        /// thoát
        /// </summary>
        [Description("退出")]
        Exit = 2,
        /// <summary>
        /// truy cập
        /// </summary>
        [Description("访问")]
        Visit = 3,
        /// <summary>
        /// thêm mới
        /// </summary>
        [Description("新增")]
        Create = 4,
        /// <summary>
        /// xóa
        /// </summary>
        [Description("删除")]
        Delete = 5,
        /// <summary>
        /// sửa đổi
        /// </summary>
        [Description("修改")]
        Update = 6,
        /// <summary>
        /// nộp
        /// </summary>
        [Description("提交")]
        Submit = 7,
        /// <summary>
        /// bất thường
        /// </summary>
        [Description("异常")]
        Exception = 8,
    }

    /// <summary>
    /// loại mục tiêu menu
    /// </summary>
    public enum EnumModuleTargetType
    {
        /// <summary>
        /// không có trang
        /// </summary>
        expand,
        /// <summary>
        /// trang khung
        /// </summary>
        iframe,
        /// <summary>
        /// trang bật lên
        /// </summary>
        open,
        /// <summary>
        /// cửa sổ mới
        /// </summary>
        blank,
        /// <summary>
        /// trang con（không hiển thị trong menu）
        /// </summary>
        subpage,
        /// <summary>
        /// cửa sổ hiện tại
        /// </summary>
        toplocation,
    }


    public enum EnumDBJoinFuncType
    {
        Equals=1
    }
}
