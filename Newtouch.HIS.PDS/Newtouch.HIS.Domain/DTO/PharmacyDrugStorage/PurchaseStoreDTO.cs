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
    /// mua hàng nhập kho 
    /// </summary>
    public class PurchaseStoreDTO
    {
        public string cgmxId { get; set; }
        
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
        /// đơn vị
        /// </summary>
        public string dw { get; set; }
        /// <summary>
        /// doanh nghiệp dược tên
        /// </summary>

        public string yqmc { get; set; }


        /// <summary>
        /// mã thuốc
        /// </summary>
        public string ypdm { get; set; }

        /// <summary>
        /// tên thuốc
        /// </summary>
        public string ypmc { get; set; }
    
        /// <summary>
        /// tên nhóm lớn
        /// </summary>
        public string dlmc { get; set; }
        
        public int kykc { get; set; }
        /// <summary>
        /// kèm đơn vị số bộ phận lượng
        /// </summary>
        public string slStr { get; set; }
        

        /// <summary>
        /// đơn vị số lượng đóng gói
        /// </summary>
        public string bzdw { get; set; }

        /// <summary>
        /// hệ số chuyển đổi
        /// </summary>
        public string zhyz { get; set; }

        /// <summary>
        /// số lượng đóng gói
        /// </summary>
        public decimal bzs { get; set; }
        public string zxdw { get; set; }
        public string pzwh { get; set; }
        /// <summary>
        /// nhà sản xuất
        /// </summary>
        public string sccj { get; set; }

        /// <summary>
        /// giá bán lẻ đơn vị nhỏ nhất
        /// </summary>
        public decimal? zxdwlsj { get; set; }
        /// <summary>
        /// giá bán lẻ đơn vị nhỏ nhất
        /// </summary>
        public decimal? zxdwpfj { get; set; }
        /// <summary>
        /// giá bán buôn kho thuốc
        /// </summary>
        public decimal? ykpfj { get; set; }

        /// <summary>
        /// giá bán lẻ kho thuốc
        /// </summary>
        public decimal? yklsj { get; set; }

       
        /// <summary>
        /// giá bán lẻ đơn giá đơn vị
        /// </summary>
        public string lsjdjdw { get; set; }
        public decimal? zje { get; set; }
        public string zxspdm { get; set; }


    }
}
