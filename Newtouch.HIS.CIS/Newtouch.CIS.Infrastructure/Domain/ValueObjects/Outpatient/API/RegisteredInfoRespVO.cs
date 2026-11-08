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
        /// Khám bệnh Loại 1thông thường Ngoại trú 2gấp khám 3chuyên gia Ngoại trú
        /// </summary>
        public string mjzbz { get; set; }

        /// <summary>
        /// Thao tác Thời gian
        /// </summary>
        public DateTime operatingTime { get; set; }
        /// <summary>
        /// Bác sĩ Mã
        /// </summary>
        public string ys { get; set; }
        /// <summary>
        /// Khoa Mã
        /// </summary>
        public string ks { get; set; }
        /// <summary>
        /// Bảo hiểm y tế quyết toán số(Bảo hiểm y tế Quay lại)
        /// </summary>
        public string ybjsh { get; set; }
        /// <summary>
        /// lại khám Cờ 1lại khám
        /// </summary>
        public string fzbz { get; set; }
        /// <summary>
        /// xã bảo Số hiệu
        /// </summary>
        public string sbbh { get; set; }
        /// <summary>
        /// tham số bảo địa Mã
        /// </summary>
        public string cbdbm { get; set; }
        /// <summary>
        /// Họ tên Pinyin
        /// </summary>
        public string py { get; set; }
        /// <summary>
        /// Số thẻ
        /// </summary>
        public string kh { get; set; }
        /// <summary>
        /// Điện thoại liên hệ
        /// </summary>
        public string ContactNum { get; set; }

        /// <summary>
        /// tỉnh
        /// </summary>
        public string province { get; set; }
        /// <summary>
        /// thành phố
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// huyện
        /// </summary>
        public string county { get; set; }
        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string address { get; set; }

        public string nlshow { get; set; }
        /// <summary>
        /// đăng ký khám đến nguồn Cờ
        /// </summary>
        public string ghlybz { get; set; }
        /// <summary>
        /// Bảo hiểm y tế Cá nhân Số hiệu
        /// </summary>
        public string grbh { get; set; }
        public short? queno { get; set; }
    }
}
