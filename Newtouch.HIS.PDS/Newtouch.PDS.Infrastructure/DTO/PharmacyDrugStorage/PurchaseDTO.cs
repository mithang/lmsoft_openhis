using Newtouch.HIS.Domain.Entity.PharmacyDrugStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.DTO.PharmacyDrugStorage
{
    public class PurchaseDTO 
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
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }
        public List<cgmx> cgmx;
    }

    public class cgmx
    {
        public string OrganizeId { get; set; }
        public string bzsm { get; set; }
        public decimal cgdj { get; set; }
        public string cgjldw { get; set; }
        public decimal cgsl { get; set; }
        public string cglx { get; set; }
        public string czlx { get; set; }
        public string dcpsbs { get; set; }
        public string ggbz { get; set; }
        public string splx { get; set; }
        public string sxh { get; set; }
        public string yqbm { get; set; }
        public string zxspbm { get; set; }
    }
}
