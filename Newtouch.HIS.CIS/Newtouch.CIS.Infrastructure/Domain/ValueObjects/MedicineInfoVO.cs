using NewtouchHIS.Lib.Base.Model;
using Newtouch.CIS.Compat;
using System;
using System.Collections.Generic;

namespace Newtouch.Domain.ValueObjects
{
    public class MedicineInfoVO
    {
        public string yfbmmc { get; set; }
        public string kcId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ypdm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? yxq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int kcsl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        //public string ypkw { get; set; }

        /// <summary>
        /// 0:khôngđiều khiển1:điều khiển
        /// </summary>
        //public short kzbz { get; set; }

        /// <summary>
        /// đãphốithuốcchưaphát(lĩnh)thuốccủa Số lượng
        /// </summary>
        public int djsl { get; set; }

        /// <summary>
        /// 0:Kích hoạt1:Vô hiệu
        /// </summary>
        public short tybz { get; set; }

        /// <summary>
        /// đếntự bảng XT_YP_CRKMXK 
        /// </summary>
        //public string crkmxId { get; set; }

        /// <summary>
        /// vàogiá  Đơn vị kho thuốc
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal? jj { get; set; }

        /// <summary>
        /// tương ứng Nhà thuốc Nhà thuốccủatháokhôngsố
        /// </summary>
        public int? zhyz { get; set; }

        /// <summary>
        /// Nơi sản xuất Danh mục
        /// </summary>
        public int? cd { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// là Khôngđãkhóa  0:chưakhóa ;    1/null:đãkhóa
        /// </summary>
        public string locked { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }

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


        public string Updating { get; set; }
        public string ksbm { get; set; }
        public string bqbm { get; set; }
        public string ksmc { get; set; }
        public string bqmc { get; set; }
        public string ypmc { get; set; }
    }

    public class MedicineInfoVO2
    {
        public string ypId { get; set; }
        public string ypCode { get; set; }

        public string ypmc { get; set; }

        public string ycmc { get; set; }

        public string ypxzmc { get; set; }

        public string jxmc { get; set; }
        public string ypgg { get; set; }

        public int kcsl { get; set; }
        public string Zcxh { get; set; }
        public string syzt { get; set; }

        /// <summary>
        /// Kho thuốc Tồn kho
        /// </summary>
        public decimal? YkKcsl { get; set; }

        /// <summary>
        /// Kho thuốckho ký tự
        /// </summary>
        public string YkDw { get; set; }

        /// <summary>
        /// Khoa phòng Đơn vị
        /// </summaYkDwry>
        public string deptdw { get; set; }

        /// <summary>
        /// Giá bán lẻ
        /// </summary>
        public decimal? lsj { get; set; }

        /// <summary>
        /// Giá bán buôn
        /// </summary>
        public decimal? pfj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Ypkw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Pxfs1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Pxfs2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Kcsx { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Kcxx { get; set; }
        public int Jhd { get; set; }
        public int Jhl { get; set; }
        public string yplb { get; set; }
        public string ypzt { get; set; }
        public int? Sysx { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string klsl { get; set; }

        /// <summary>
        /// bản Khoa phòng Thuốc ID XT_YP_BMYPXX Khóa chính
        /// </summary>
        public string bmypId { get; set; }
        /// <summary>
        /// Hạn hiệu lực
        /// </summary>
        public System.DateTime? yxq { get; set; }

        public string yfbmCode { get; set; }
        public string yfbmmc { get; set; }
    }
    public class MedicineQueryResponse
    {

        public List<MedicineInfoVO2> list { get; set; }
        public Pagination pagination { get; set; }

    }
}
