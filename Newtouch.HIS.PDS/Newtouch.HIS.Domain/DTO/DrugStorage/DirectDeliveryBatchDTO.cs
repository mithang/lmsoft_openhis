using System;

namespace Newtouch.HIS.Domain.DTO.DrugStorage
{
    /// <summary>
    /// hàng loạt xuất kho trực tiếp thực hiện tham số
    /// </summary>
    public class DirectDeliveryBatchDTO
    {
        /// <summary>
        /// cơ cấu tổ chức
        /// </summary>
        public string Organizeid { get; set; }

        /// <summary>
        /// số chứng từ
        /// </summary>
        public string djh { get; set; }

        /// <summary>
        /// chứng từ nhập/xuất kho ID
        /// </summary>
        public string crkId { get; set; }

        /// <summary>
        /// loại chứng từ
        /// </summary>
        public int djlx { get; set; }

        /// <summary>
        /// bộ phận nhập kho mã
        /// </summary>
        public string rkbm { get; set; }

        /// <summary>
        /// hiện tại bộ phận nhà thuốc/xuất kho bộ phận nhà thuốc
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// thao tác người
        /// </summary>
        public string userCode { get; set; }

        /// <summary>
        /// phương thức nhập/xuất kho mã
        /// </summary>
        public string crkfs { get; set; }

        /// <summary>
        /// trạng thái duyệt
        /// </summary>
        public string shzt { get; set; }

        private string _ypCodes = "";
        /// <summary>
        /// mã thuốc tập hợp dùng ',' phân tách, độ dài ký tự không được vượt quá 1000
        /// </summary>
        public string ypCodes
        {
            get { return _ypCodes; }
            set
            {
                if (value.Length > 1000)
                {
                    throw new Exception("请控制药品种数，拼接后的药品代码长度不得超过1000");
                }
                _ypCodes = value;
            }
        }
    }
}