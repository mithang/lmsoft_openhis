using System;

namespace Newtouch.HIS.Domain.DTO.DrugStorage
{
    /// <summary>
    /// Tham Số thực hiện xuất kho trực tiếp hàng loạt
    /// </summary>
    public class DirectDeliveryBatchDTO
    {
        /// <summary>
        /// Cơ cấu tổ chức
        /// </summary>
        public string Organizeid { get; set; }

        /// <summary>
        /// Số chứng từ
        /// </summary>
        public string djh { get; set; }

        /// <summary>
        /// Chứng từ xuất nhập khoID
        /// </summary>
        public string crkId { get; set; }

        /// <summary>
        /// Loại chứng từ
        /// </summary>
        public int djlx { get; set; }

        /// <summary>
        /// Mã bộ phận nhập kho
        /// </summary>
        public string rkbm { get; set; }

        /// <summary>
        /// Bộ phận nhà thuốc hiện tại/Bộ phận nhà thuốc xuất kho
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// Người thao tác
        /// </summary>
        public string userCode { get; set; }

        /// <summary>
        /// Mã phương thức xuất nhập kho
        /// </summary>
        public string crkfs { get; set; }

        /// <summary>
        /// Trạng thái duyệt
        /// </summary>
        public string shzt { get; set; }

        private string _ypCodes = "";
        /// <summary>
        /// Tập hợp mã thuốc dùng','Phân tách,Độ dài ký tự không được vượt quá1000
        /// </summary>
        public string ypCodes
        {
            get { return _ypCodes; }
            set
            {
                if (value.Length > 1000)
                {
                    throw new Exception("Vui lòng khống chế số loại thuốc，Độ dài mã thuốc sau khi nối không được vượt quá1000");
                }
                _ypCodes = value;
            }
        }
    }
}