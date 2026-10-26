namespace Newtouch.HIS.Domain.ValueObjects
{
    public class ChargeItemDetailVO
    {
        public int xmbh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfxm { get; set; }

        /// <summary>
        /// Tên chung
        /// </summary>
        public string xmmc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string py { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        ///
        /// </summary>
        public decimal dj { get; set; }

        /// <summary>
        /// Xem chi tiết trong bảng hạng mục hệ thống"Tỷ lệ tự chi trả"Giải thích
        /// </summary>
        public decimal zfbl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dl { get; set; }

        /// <summary>
        /// 1 Tự lo 2 Tự chi trả theo phân loại   Xem chi tiết trong bảng hạng mục hệ thống"Tính chất tự chi trả"Giải thích      Ngoại trú khả dụng,Cấp cứu không khả dụng,Hoặc ngược lại,Kiểm soát theo việc các khoa dược khác nhau có thuốc hay không
        /// </summary>
        public string zfxz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string spm { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal bzs { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string bzdw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? jl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string jldw { get; set; }
        /// <summary>
        /// xt_yc  Sửa rồi lưu trực tiếp tên
        /// </summary>
        public string ycmc { get; set; }
        /// <summary>
        /// Khi thuốc được tiêu thụ tại các nhà thuốc"Cấp đóng gói"hoặc"Thông tin số lẻ đã tách"
        /// </summary>
        public string ypbzdm { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string nbdl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string dlmc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ybdm { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string ypgg { get; set; }

        public int xmtype { get; set; }

        public string yfdm { get; set; }

        public string ksdm { get; set; }
        public string ypdm { get; set; }

        public string xmkc { get; set; }
    }
}
