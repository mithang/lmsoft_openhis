using Newtouch.HIS.Domain.Entity.PharmacyDrugStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.VO
{
    public class PurchaseDetailVO
    {
        public string ypmc { get; set; }
        public string cglxmc { get; set; }
        public string splxmc { get; set; }
        public string cgjldwmc { get; set; }
        public string dcpsbsmc { get; set; }
        public string yqmc { get; set; }
        public string psyqmc { get; set; }


        public string cgmxId { get; set; }
        /// <summary>
        /// Số mua hàng
        /// </summary>
        public string cgId { get; set; }
        public string OrganizeId { get; set; }
        /// <summary>
        /// Số hiệu thuốc
        /// </summary>
        public string ypCode { get; set; }
        /// <summary>
        /// Số thứ tự
        /// </summary>
        public int sxh { get; set; }
        /// <summary>
        /// Phương thức mua hàng
        /// </summary>
        public string cglx { get; set; }
        /// <summary>
        /// Loại hàng hóa
        /// </summary>
        public string splx { get; set; }
        /// <summary>
        /// Mã thống nhất
        /// </summary>
        public string zxspbm { get; set; }
        /// <summary>
        /// Đơn vị đo lường mua hàng
        /// </summary>
        public string cgjldw { get; set; }
        /// <summary>
        /// Quy cách đóng gói
        /// </summary>
        public string ggbz { get; set; }
        /// <summary>
        /// Số lượng mua hàng
        /// </summary>
        public decimal cgsl { get; set; }
        /// <summary>
        /// Đơn giá mua hàng
        /// </summary>
        public decimal cgdj { get; set; }
        /// <summary>
        /// Mã doanh nghiệp dược
        /// </summary>
        public string yqbm { get; set; }
        /// <summary>
        /// Cờ giao hàng nhiều lần(0Không cho phép  1Cho phép)
        /// </summary>
        public string dcpsbs { get; set; }
        /// <summary>
        /// Giải thích ghi chú
        /// </summary>
        public string bzsm { get; set; }
        /// <summary>
        /// Đơn vị
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }
        public decimal? zje { get; set; }
        public string sccj { get; set; }
    }

    public class PurchaseVo 
    {
        public string cgId { get; set; }
        /// <summary>
        /// Tổ chức
        /// </summary>
        public string OrganizeId { get; set; }
        /// <summary>
        /// Thời gian đơn hàng
        /// </summary>
        public string ddsj { get; set; }
        /// <summary>
        /// Loại thao tác
        /// </summary>
        public string czlx { get; set; }

        /// <summary>
        /// Mã BHYT
        /// </summary>
        public string yybm { get; set; }
        /// <summary>
        /// Mã điểm giao hàng
        /// </summary>
        public string psdbm { get; set; }
        /// <summary>
        /// Loại đơn hàng
        /// </summary>
        public string ddlx { get; set; }
        /// <summary>
        /// Số đơn hàng
        /// </summary>
        public string ddbh { get; set; }
        /// <summary>
        /// Số đơn kế hoạch bệnh viện
        /// </summary>
        public string yyjhdh { get; set; }
        /// <summary>
        /// Ngày nhận hàng muộn nhất
        /// </summary>
        public string zwdhrq { get; set; }
        /// <summary>
        /// Số bản ghi
        /// </summary>
        public int jls { get; set; }
        /// <summary>
        /// Trạng thái đơn hàng
        /// </summary>
        public int ddzt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }
        public string gysCode { get; set; }
        public string gysName { get; set; }
        public decimal? zje { get; set; }
        public string fph { get; set; }
    }
}
