using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.PharmacyDrugStorage
{
    /// <summary>
    /// Bảng danh mục thuốc BHYT
    /// </summary>
    public class G_yb_ypxxVO
    {
        /// <summary>
        /// mã thuốc
        /// </summary>
        public string ypdm { get; set; }
        /// <summary>
        /// dữ liệu nguồn
        /// </summary>
        public string sjly { get; set; }
        /// <summary>
        /// đăng ký tên
        /// </summary>
        public string zcmc { get; set; }
        /// <summary>
        /// hàng hóa tên
        /// </summary>
        public string spmc { get; set; }
        /// <summary>
        /// đăng ký dạng bào chế
        /// </summary>
        public string zcjx { get; set; }
        /// <summary>
        /// Dạng bào chế thực tế
        /// </summary>
        public string sjjx { get; set; }
        /// <summary>
        /// đăng ký quy cách
        /// </summary>
        public string zcgg { get; set; }
        /// <summary>
        /// Quy cách thực tế
        /// </summary>
        public string sjgg { get; set; }
        /// <summary>
        /// chất liệu đóng gói
        /// </summary>
        public string bzcz { get; set; }
        /// <summary>
        /// nhỏ nhất số lượng đóng gói lượng
        /// </summary>
        public string zxbzsl { get; set; }
        /// <summary>
        /// Đơn vị chế phẩm nhỏ nhất
        /// </summary>
        public string zxzjdw { get; set; }
        /// <summary>
        /// nhỏ nhất đơn vị đóng gói
        /// </summary>
        public string zxbzdw { get; set; }
        /// <summary>
        /// Doanh nghiệp thuốc
        /// </summary>
        public string ypqy { get; set; }
        /// <summary>
        /// Người nắm giữ thuốc đã lưu hành
        /// </summary>
        public string shypcyr { get; set; }
        /// <summary>
        /// số giấy phép
        /// </summary>
        public string pzwh { get; set; }
        /// <summary>
        /// Số văn bản phê duyệt gốc
        /// </summary>
        public string ypzwh { get; set; }
        /// <summary>
        /// thuốc này vị mã
        /// </summary>
        public string ypbwm { get; set; }
        /// <summary>
        /// Tên doanh nghiệp đóng gói phân đoạn
        /// </summary>
        public string fbzqymc { get; set; }
        /// <summary>
        /// Doanh nghiệp sản xuất
        /// </summary>
        public string scqy { get; set; }
        /// <summary>
        /// Trạng thái thị trường
        /// </summary>
        public string sczt { get; set; }
        /// <summary>
        /// BHYT tên thuốc
        /// </summary>
        public string ybypmc { get; set; }
        /// <summary>
        /// Loại giáp/ất bản 2021
        /// </summary>
        public string ybjyl { get; set; }
        /// <summary>
        /// BHYT dạng bào chế
        /// </summary>
        public string ybjx { get; set; }
        /// <summary>
        /// số hiệu
        /// </summary>
        public string bh { get; set; }
        /// <summary>
        /// ghi chú
        /// </summary>
        public string bz { get; set; }
        /// <summary>
        /// A B loại
        /// </summary>
        public string jyl { get; set; }
        /// <summary>
        /// ghi chú 1
        /// </summary>
        public string bz1 { get; set; }
        /// <summary>
        /// ghi chú (đặc biệt thuộc tính dấu hiệu)
        /// </summary>
        public string bztssxbs { get; set; }
        /// <summary>
        /// Tiêu chuẩn thanh toán BHYT
        /// </summary>
        public string ybzfbz { get; set; }

        /// <summary>
        /// phiên âm mã
        /// </summary>
        public string pym { get; set; }
    }


    public class ypxxlist
    {
        public string ypid { get; set; }
    }
}
