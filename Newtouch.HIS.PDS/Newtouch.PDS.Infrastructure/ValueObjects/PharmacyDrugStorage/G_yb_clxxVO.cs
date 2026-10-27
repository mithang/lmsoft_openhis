using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.PharmacyDrugStorage
{
    public class G_yb_clxxVO
    {
        /// <summary>
        /// Mã vật tư
        /// </summary>
        public string hcdm { get; set; }
        /// <summary>
        /// Phân loại cấp 1
        /// </summary>
        public string yjfl { get; set; }
        /// <summary>
        /// Phân loại cấp 2
        /// </summary>
        public string ejfl { get; set; }
        /// <summary>
        /// Phân loại 3 cấp
        /// </summary>
        public string sjfl { get; set; }
        /// <summary>
        /// Tên chung BHYT
        /// </summary>
        public string ybtym { get; set; }
        /// <summary>
        /// Chất liệu
        /// </summary>
        public string cz { get; set; }
        /// <summary>
        /// Đặc điểm
        /// </summary>
        public string tz { get; set; }
        /// <summary>
        /// Số đăng ký lưu trữ
        /// </summary>
        public string zcbah { get; set; }
        /// <summary>
        /// Tên sản phẩm đơn chiếc
        /// </summary>
        public string djcpmc { get; set; }
        /// <summary>
        /// Doanh nghiệp vật tư
        /// </summary>
        public string hcqy { get; set; }
        /// <summary>
        /// Số quy cách mẫu mã
        /// </summary>
        public double ggxhs { get; set; }
        /// <summary>
        /// Người đăng ký lưu trữ
        /// </summary>
        public string zcbar { get; set; }
        /// <summary>
        /// Mã phiên âm
        /// </summary>
        public string pym { get; set; }
        /// <summary>
        /// Tính chất BHYT
        /// </summary>
        public string ybxz { get; set; }

    }
}
