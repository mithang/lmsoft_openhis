using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.PharmacyDrugStorage
{
    /// <summary>
    /// Bảng danh mục vật tư BHYT
    /// </summary>
    public class G_yb_xmxxVO
    {
        /// <summary>
        /// mã vật tư tiêu hao
        /// </summary>
        public string hcdm { get; set; }
        /// <summary>
        /// phân loại cấp 1
        /// </summary>
        public string yjfl { get; set; }
        /// <summary>
        /// phân loại cấp 2
        /// </summary>
        public string ejfl { get; set; }
        /// <summary>
        /// phân loại cấp 3
        /// </summary>
        public string sjfl { get; set; }
        /// <summary>
        /// BHYT chung tên
        /// </summary>
        public string ybtym { get; set; }
        /// <summary>
        /// chất liệu
        /// </summary>
        public string cz { get; set; }
        /// <summary>
        /// đặc điểm
        /// </summary>
        public string tz { get; set; }
        /// <summary>
        /// số đăng ký lưu hồ sơ
        /// </summary>
        public string zcbah { get; set; }
        /// <summary>
        /// tên sản phẩm đơn chiếc
        /// </summary>
        public string djcpmc { get; set; }
        /// <summary>
        /// doanh nghiệp vật tư tiêu hao
        /// </summary>
        public string hcqy { get; set; }
        /// <summary>
        /// số mẫu quy cách
        /// </summary>
        public double ggxhs { get; set; }
        /// <summary>
        /// người đăng ký lưu hồ sơ
        /// </summary>
        public string zcbar { get; set; }
        /// <summary>
        /// phiên âm mã
        /// </summary>
        public string pym { get; set; }
        /// <summary>
        /// tính chất BHYT
        /// </summary>
        public string ybxz { get; set; }

    }
}
