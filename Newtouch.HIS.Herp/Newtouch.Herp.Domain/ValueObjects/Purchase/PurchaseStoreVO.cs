using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Herp.Domain.ValueObjects.Purchase
{
    public class PurchaseStoreVO
    {

        public string productmc { get; set; }
        public string cglxmc { get; set; }
        public string psyqmc { get; set; }
        public string qymc { get; set; }
        public string productId { get; set; }


        public string cgmxId { get; set; }
        /// <summary>
        /// Số mua hàng
        /// </summary>
        public string cgId { get; set; }
        public string OrganizeId { get; set; }
        /// <summary>
        /// Số hiệu thuốc
        /// </summary>
        public string productCode { get; set; }

        public string sxh { get; set; }
        public string cglx { get; set; }
        public string hctbdm { get; set; }
        public string hcxfdm { get; set; }
        public string yybddm { get; set; }
        public string cgggxh { get; set; }
        public string pssm { get; set; }
        public decimal cgsl { get; set; }
        public decimal cgdj { get; set; }
        public string qybm { get; set; }
        public string sfjj { get; set; }
        public string psyq { get; set; }
        public string dcpsbs { get; set; }
        public string cwxx { get; set; }
        public string bzsm { get; set; }


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


        /// <summary>
        /// Vật tư ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Tên vật tư
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Tồn kho Đơn vị nhỏ nhất
        /// </summary>
        public int? kcsl { get; set; }

        /// <summary>
        /// Tồn kho khả dụng Đơn vị nhỏ nhất
        /// </summary>
        public int? kykcsl { get; set; }

        /// <summary>
        /// Số lượng đơn vị khoa phòng(Kèm đơn vị)
        /// </summary>
        public string slstr { get; set; }

        /// <summary>
        /// Giá bán lẻ đơn vị nhỏ nhất
        /// </summary>
        public decimal? minlsj { get; set; }

        /// <summary>
        /// Giá bán lẻ đơn vị khoa phòng
        /// </summary>
        public decimal? bmlsj { get; set; }

        /// <summary>
        /// Hệ số chuyển đổi
        /// </summary>
        public int? zhyz { get; set; }

        /// <summary>
        /// Hệ số chuyển đổi đơn vị khoa phòng
        /// </summary>
        public int? bmdwzhyz { get; set; }

        /// <summary>
        /// Phiên âm
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// Quy cách
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        /// Nhà sản xuất Id
        /// </summary>
        public string supplierId { get; set; }

        /// <summary>
        /// Tên nhà sản xuất
        /// </summary>
        public string supplierName { get; set; }

        /// <summary>
        /// Đơn vị nhỏ nhất ID
        /// </summary>
        public string zxdwId { get; set; }

        /// <summary>
        /// Tên đơn vị nhỏ nhất
        /// </summary>
        public string mindwmc { get; set; }

        /// <summary>
        /// Đơn vị khoa phòng ID
        /// </summary>
        public string bmdwId { get; set; }

        /// <summary>
        /// Tên đơn vị khoa phòng
        /// </summary>
        public string bmdwmc { get; set; }

        /// <summary>
        /// Tên loại
        /// </summary>
        public string lbmc { get; set; }

        /// <summary>
        /// Loại ID
        /// </summary>
        public string lbId { get; set; }
        /// <summary>
        /// Mã bảo hiểm y tế quốc gia(Vật tư tiêu hao 27 bit)
        /// </summary>
        public string hcgjybdm { get; set; }
        /// <summary>
        /// Mã thống nhất vật tư tiêu hao
        /// </summary>
        public string gjybdm { get; set; }
    }
}
