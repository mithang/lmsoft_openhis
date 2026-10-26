using System;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.HIS.Domain.ValueObjects
{
    public class SysMedicinePriceAdjustmentProfitLossVO
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string TjsyId { get; set; }

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
        public string Ypdm { get; set; }

        /// <summary>
        /// số lô
        /// </summary>
        public string Ph { get; set; }

        /// <summary>
        /// điều chỉnh giá thời gian
        /// </summary>
        public DateTime Tjsj { get; set; }

        /// <summary>
        /// điều chỉnh giá tệp
        /// </summary>
        public string Tjwj { get; set; }

        /// <summary>
        /// khi giờ số lượng
        /// </summary>
        public int Dssl { get; set; }

        /// <summary>
        /// giá bán buôn gốc
        /// </summary>
        public decimal Ypfj { get; set; }

        /// <summary>
        /// giá bán lẻ gốc
        /// </summary>
        public decimal Ylsj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal Yykpfj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal Yyklsj { get; set; }

        /// <summary>
        /// giá bán buôn hiện tại
        /// </summary>
        public decimal Xpfj { get; set; }

        /// <summary>
        /// giá bán lẻ hiện tại
        /// </summary>
        public decimal Xlsj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal Xykpfj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal Xyklsj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Zhyz { get; set; }

        /// <summary>
        /// người thao tác
        /// </summary>
        public string Czy { get; set; }

        /// <summary>
        /// lợi nhuận giá bán buôn
        /// </summary>
        public decimal Pfjtjlr { get; set; }

        /// <summary>
        /// lợi nhuận giá lẻ
        /// </summary>
        public decimal Lsjtjlr { get; set; }

        /// <summary>
        /// lô
        /// </summary>
        public string pc { get; set; }

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
        /// tên thuốc
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// đơn vị đóng gói
        /// </summary>
        public string bzdw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal mzcls { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string mzcldw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal zycls { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zycldw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string djdw { get; set; }
        /// <summary>
        /// đơn vị 把门诊和住院都考虑进来
        /// </summary>

        public string dw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal zfbl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string mzzybz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string spm { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string py { get; set; }
        /// <summary>
        /// quy cách thuốc
        /// </summary>
        public string ypgg { get; set; }
        /// <summary>
        /// nhà sản xuất
        /// </summary>
        public string ycmc { get; set; }

        public string zt { get; set; }
    }
}

