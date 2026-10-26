using System.Collections.Generic;

namespace Newtouch.Herp.Infrastructure.Model
{
    /// <summary>
    /// Thông tin tài khoản đang đăng nhập
    /// </summary>
    public class LoginUserCurrentKfModel
    {
        /// <summary>
        /// Người dùng ID
        /// </summary>
        public string userId { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public string gh { get; set; }

        /// <summary>
        /// Tên nhân viên
        /// </summary>
        public string staffName { get; set; }

        /// <summary>
        /// Kho hiện tại ID
        /// </summary>
        public string currentKfId { get; set; }

        /// <summary>
        /// Tên kho hiện tại
        /// </summary>
        public string currentKfName { get; set; }

        /// <summary>
        /// Cấp kho
        /// </summary>
        public int currentKfLevel { get; set; }

        private List<KfInfoModel> _kfList = new List<KfInfoModel>();
        /// <summary>
        /// Kho thuộc về
        /// </summary>
        public List<KfInfoModel> kfList
        {
            get { return _kfList; }
            set { _kfList = value; }
        }

        /// <summary>
        /// Chức vụ
        /// </summary>
        public string dutyName { get; set; }

        /// <summary>
        /// Tổ chức
        /// </summary>
        public string OrganizeId { get; set; }
    }

    /// <summary>
    /// Thông tin kho
    /// </summary>
    public class KfInfoModel
    {
        /// <summary>
        /// Kho hiện tại ID
        /// </summary>
        public string kfId { get; set; }

        /// <summary>
        /// Tên kho hiện tại
        /// </summary>
        public string kfName { get; set; }

        /// <summary>
        /// Cấp kho
        /// </summary>
        public int kfLeve { get; set; }
    }
}
