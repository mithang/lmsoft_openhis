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
        /// Số lô
        /// </summary>
        public string Ph { get; set; }

        /// <summary>
        /// Thời gian điều chỉnh giá
        /// </summary>
        public DateTime Tjsj { get; set; }

        /// <summary>
        /// Tệp điều chỉnh giá
        /// </summary>
        public string Tjwj { get; set; }

        /// <summary>
        /// Số lượng lúc đó
        /// </summary>
        public int Dssl { get; set; }

        /// <summary>
        /// Giá bán buôn gốc
        /// </summary>
        public decimal Ypfj { get; set; }

        /// <summary>
        /// Giá bán lẻ gốc
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
        /// Giá bán buôn hiện tại
        /// </summary>
        public decimal Xpfj { get; set; }

        /// <summary>
        /// Giá bán lẻ hiện tại
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
        /// Nhân viên thao tác
        /// </summary>
        public string Czy { get; set; }

        /// <summary>
        /// Lợi nhuận giá bán buôn
        /// </summary>
        public decimal Pfjtjlr { get; set; }

        /// <summary>
        /// Lợi nhuận giá lẻ
        /// </summary>
        public decimal Lsjtjlr { get; set; }

        /// <summary>
        /// Lô
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
        /// Tên thuốc
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// Đơn vị đóng gói
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
        /// Đơn vị Xem xét cả ngoại trú và nội trú
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
        /// Quy cách thuốc
        /// </summary>
        public string ypgg { get; set; }
        /// <summary>
        /// Nhà máy
        /// </summary>
        public string ycmc { get; set; }

        public string zt { get; set; }
    }
}

