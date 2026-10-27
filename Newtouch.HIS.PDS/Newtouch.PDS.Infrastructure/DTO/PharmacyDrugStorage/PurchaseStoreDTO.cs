using Newtouch.HIS.Domain.Entity.PharmacyDrugStorage;
using Newtouch.HIS.Domain.Entity.V;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.DTO.PharmacyDrugStorage
{
    /// <summary>
    /// Nhập kho mua hàng 
    /// </summary>
    public class PurchaseStoreDTO
    {
        public string cgmxId { get; set; }
        
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
        /// Đơn vị
        /// </summary>
        public string dw { get; set; }
        /// <summary>
        /// Tên doanh nghiệp dược
        /// </summary>

        public string yqmc { get; set; }


        /// <summary>
        /// Mã thuốc
        /// </summary>
        public string ypdm { get; set; }

        /// <summary>
        /// Tên thuốc
        /// </summary>
        public string ypmc { get; set; }
    
        /// <summary>
        /// Tên loại lớn
        /// </summary>
        public string dlmc { get; set; }
        
        public int kykc { get; set; }
        /// <summary>
        /// Số lượng bộ phận kèm đơn vị
        /// </summary>
        public string slStr { get; set; }
        

        /// <summary>
        /// Đơn vị số đóng gói
        /// </summary>
        public string bzdw { get; set; }

        /// <summary>
        /// Hệ số quy đổi
        /// </summary>
        public string zhyz { get; set; }

        /// <summary>
        /// Số lượng đóng gói
        /// </summary>
        public decimal bzs { get; set; }
        public string zxdw { get; set; }
        public string pzwh { get; set; }
        /// <summary>
        /// Nhà sản xuất
        /// </summary>
        public string sccj { get; set; }

        /// <summary>
        /// Giá bán lẻ đơn vị nhỏ nhất
        /// </summary>
        public decimal? zxdwlsj { get; set; }
        /// <summary>
        /// Giá bán lẻ đơn vị nhỏ nhất
        /// </summary>
        public decimal? zxdwpfj { get; set; }
        /// <summary>
        /// Giá bán buôn kho thuốc
        /// </summary>
        public decimal? ykpfj { get; set; }

        /// <summary>
        /// Giá bán lẻ kho thuốc
        /// </summary>
        public decimal? yklsj { get; set; }

       
        /// <summary>
        /// Đơn vị đơn giá bán lẻ
        /// </summary>
        public string lsjdjdw { get; set; }
        public decimal? zje { get; set; }
        public string zxspdm { get; set; }


    }
}
