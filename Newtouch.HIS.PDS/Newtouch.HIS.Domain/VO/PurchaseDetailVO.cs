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
        /// mua hàng số hiệu
        /// </summary>
        public string cgId { get; set; }
        public string OrganizeId { get; set; }
        /// <summary>
        /// số hiệu thuốc
        /// </summary>
        public string ypCode { get; set; }
        /// <summary>
        /// số thứ tự
        /// </summary>
        public int sxh { get; set; }
        /// <summary>
        /// mua hàng mô hình
        /// </summary>
        public string cglx { get; set; }
        /// <summary>
        /// loại hàng hóa
        /// </summary>
        public string splx { get; set; }
        /// <summary>
        /// thống nhất mã
        /// </summary>
        public string zxspbm { get; set; }
        /// <summary>
        /// đơn vị tính mua hàng
        /// </summary>
        public string cgjldw { get; set; }
        /// <summary>
        /// quy cách đóng gói
        /// </summary>
        public string ggbz { get; set; }
        /// <summary>
        /// số lượng mua
        /// </summary>
        public decimal cgsl { get; set; }
        /// <summary>
        /// đơn giá mua hàng
        /// </summary>
        public decimal cgdj { get; set; }
        /// <summary>
        /// mã doanh nghiệp dược
        /// </summary>
        public string yqbm { get; set; }
        /// <summary>
        /// nhiều lần phân phối dấu hiệu (0 không cho phép  1 cho phép)
        /// </summary>
        public string dcpsbs { get; set; }
        /// <summary>
        /// ghi chú giải thích
        /// </summary>
        public string bzsm { get; set; }
        /// <summary>
        /// đơn vị
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
        /// tổ chức
        /// </summary>
        public string OrganizeId { get; set; }
        /// <summary>
        /// thời gian đơn hàng
        /// </summary>
        public string ddsj { get; set; }
        /// <summary>
        /// loại thao tác
        /// </summary>
        public string czlx { get; set; }

        /// <summary>
        /// BHYT mã
        /// </summary>
        public string yybm { get; set; }
        /// <summary>
        /// mã điểm phân phối
        /// </summary>
        public string psdbm { get; set; }
        /// <summary>
        /// loại đơn hàng
        /// </summary>
        public string ddlx { get; set; }
        /// <summary>
        /// số đơn hàng
        /// </summary>
        public string ddbh { get; set; }
        /// <summary>
        /// số phiếu kế hoạch bệnh viện
        /// </summary>
        public string yyjhdh { get; set; }
        /// <summary>
        /// ngày giao hàng muộn nhất
        /// </summary>
        public string zwdhrq { get; set; }
        /// <summary>
        /// số bản ghi
        /// </summary>
        public int jls { get; set; }
        /// <summary>
        /// trạng thái đơn hàng
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
