using System;

namespace Newtouch.Domain.ValueObjects.API
{
    public class RegisteredInfoRespVO
    {
        /// <summary>
        /// 
        /// </summary>
        public string mzh { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string blh { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string brxm { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sexValue { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? birth { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string brxzmc { get; set; }

        public string brxz { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? zjlx { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string zjh { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ksmc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ysxm { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime ghsj { get; set; }
        /// <summary>
        /// Loại khám bệnh 1Khám thường ngoại trú 2Cấp cứu 3Khám chuyên gia
        /// </summary>
        public string mjzbz { get; set; }

        /// <summary>
        /// Thời gian thao tác
        /// </summary>
        public DateTime operatingTime { get; set; }
        /// <summary>
        /// Mã bác sĩ
        /// </summary>
        public string ys { get; set; }
        /// <summary>
        /// Mã khoa
        /// </summary>
        public string ks { get; set; }
        /// <summary>
        /// Số quyết toán BHYT(BHYT trả về)
        /// </summary>
        public string ybjsh { get; set; }
        /// <summary>
        /// Cờ tái khám 1Tái khám
        /// </summary>
        public string fzbz { get; set; }
        /// <summary>
        /// Số bảo hiểm xã hội
        /// </summary>
        public string sbbh { get; set; }
        /// <summary>
        /// Mã nơi tham gia bảo hiểm
        /// </summary>
        public string cbdbm { get; set; }
        /// <summary>
        /// Pinyin họ tên
        /// </summary>
        public string py { get; set; }
        /// <summary>
        /// Số thẻ
        /// </summary>
        public string kh { get; set; }
        /// <summary>
        /// Số điện thoại liên hệ
        /// </summary>
        public string ContactNum { get; set; }

        /// <summary>
        /// Tỉnh
        /// </summary>
        public string province { get; set; }
        /// <summary>
        /// Thành phố
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// Huyện
        /// </summary>
        public string county { get; set; }
        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string address { get; set; }

        public string nlshow { get; set; }
        /// <summary>
        /// Cờ nguồn đăng ký khám
        /// </summary>
        public string ghlybz { get; set; }
        /// <summary>
        /// Số cá nhân BHYT
        /// </summary>
        public string grbh { get; set; }
        public short? queno { get; set; }
    }
}
