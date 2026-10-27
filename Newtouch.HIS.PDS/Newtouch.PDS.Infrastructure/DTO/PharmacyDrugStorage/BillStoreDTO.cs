using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.DTO.PharmacyDrugStorage
{
    public class BillStoreDTO
    {
        public string Id { get; set; }
        public string OrganizeId { get; set; }
        public string fph { get; set; }

        public string fprq { get; set; }
        public decimal fphszje { get; set; }
        public string yqbm { get; set; }
        public string yybm { get; set; }
        public string psdbm { get; set; }
        public string dlsgbz { get; set; }
        public string fpbz { get; set; }
        public string sfwpsfp { get; set; }
        public string wpsfpsm { get; set; }
        public string fpmxbh { get; set; }
        public string splx { get; set; }
        public string sfch { get; set; }
        public string zxspbm { get; set; }
        public string scph { get; set; }
        public string scrq { get; set; }
        public decimal spsl { get; set; }
        public string glmxbh { get; set; }
        public string xsddh { get; set; }
        public int? sxh { get; set; }
        public string yxrq { get; set; }
        public decimal wsdj { get; set; }
        public decimal hsdj { get; set; }
        public decimal sl { get; set; }
        public decimal se { get; set; }
        public decimal hsje { get; set; }
        public decimal pfj { get; set; }
        public decimal lsj { get; set; }
        public string pzwh { get; set; }


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


        public string dlsgbzmc { get; set; }
        public string sfwpsfpmc { get; set; }
        public string splxmc { get; set; }
        public string sfchmc { get; set; }

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

        /// <summary>
        /// Quy cách
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        /// Số lượng bộ phận kèm đơn vị
        /// </summary>
        public string slStr { get; set; }


        /// <summary>
        /// Đơn vị số đóng gói
        /// </summary>
        public string bzdw { get; set; }

        /// <summary>
        /// Đơn vị nhỏ nhất
        /// </summary>
        public string zxdw { get; set; }


        /// <summary>
        /// Đơn vị
        /// </summary>
        public string dw { get; set; }


        /// <summary>
        /// Hệ số quy đổi
        /// </summary>
        public int? zhyz { get; set; }

        /// <summary>
        /// Số lượng đóng gói
        /// </summary>
        public int? bzs { get; set; }

        /// <summary>
        /// Giá bán buôn bộ phận
        /// </summary>
        //public decimal? pfj { get; set; }

        ///// <summary>
        ///// Giá bán lẻ bộ phận
        ///// </summary>
        //public decimal? lsj { get; set; }

        /// <summary>
        /// Giá bán lẻ đơn vị nhỏ nhất
        /// </summary>
        public decimal? zxdwlsj { get; set; }

        /// <summary>
        /// Giá bán buôn kho thuốc
        /// </summary>
        public decimal? ykpfj { get; set; }

        /// <summary>
        /// Giá bán lẻ kho thuốc
        /// </summary>
        public decimal? yklsj { get; set; }

        /// <summary>
        /// Nhà sản xuất
        /// </summary>
        public string sccj { get; set; }

        /// <summary>
        /// Đơn vị đơn giá bán lẻ
        /// </summary>
        public string lsjdjdw { get; set; }
    }
}
