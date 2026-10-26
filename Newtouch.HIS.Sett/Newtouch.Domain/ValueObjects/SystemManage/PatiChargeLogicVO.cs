using System;

namespace Newtouch.HIS.Domain.ValueObjects.SystemManage
{
    public class PatiChargeLogicVO
    {
        public int brsfsfbh { get; set; }
        public string brxz { get; set; }
        public string brxzmc { get; set; }

        public string dl { get; set; }

        public string dlmc { get; set; }

        public string sfxm { get; set; }

        public string sfxmmc { get; set; }

        public byte sfjb { get; set; }

        public string fyfw { get; set; }

        public decimal zfbl { get; set; }

        /// <summary>
        /// Xác định tính chất của phần tự chi trả tính được theo tỷ lệ tự chi trả   0 Tự chi trả(Tiền mặt tự chi trả sau giao dịch BHYT)   1 Tự lo
        /// </summary>
        public string zfxz { get; set; }

        /// <summary>
        /// 0 Tất cả chi phí đều dùng tỷ lệ này,Không xét giới hạn trên
        /// </summary>
        public decimal fysx { get; set; }

        /// <summary>
        /// 0:Dùng chung,1:Khám ngoại trú,2:Nhập viện
        /// </summary>
        public string mzzybz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
