namespace Newtouch.HIS.Domain.DTO
{
    /// <summary>
    /// Tra cứu đăng ký khám ngoại trú Trả vềDTO
    /// </summary>
    public class OutPatientRegistrationInfoDTO
    {
        /// <summary>
        /// Số khám ngoại trú
        /// </summary>
        public string mzh { get; set; }
        /// <summary>
        /// Số bệnh án
        /// </summary>
        public string blh { get; set; }
        /// <summary>
        /// Bác sĩ
        /// </summary>
        public string ys { get; set; }
        /// <summary>
        /// Bác sĩ
        /// </summary>
        public string ysxm { get; set; }
        /// <summary>
        /// Khoa
        /// </summary>
        public string ks { get; set; }
        /// <summary>
        /// Khoa
        /// </summary>
        public string ksmc { get; set; }
        /// <summary>
        /// Tính chất bệnh nhân
        /// </summary>
        public string brxz { get; set; }
        /// <summary>
        /// Tính chất bệnh nhân
        /// </summary>
        public string brxzmc { get; set; }
        /// <summary>
        /// Tên bệnh nhân
        /// </summary>
        public string brxm { get; set; }
        /// <summary>
        /// Tình trạng hôn nhân
        /// </summary>
        public string maritalStatus { get; set; }
        /// <summary>
        /// Tình trạng hôn nhân
        /// </summary>
        public string maritalStatusValue { get; set; }
        /// <summary>
        /// Loại giấy tờ
        /// </summary>
        public string zjlx { get; set; }
        /// <summary>
        /// Loại giấy tờ
        /// </summary>
        public string zjlxValue { get; set; }
        /// <summary>
        /// Số giấy tờ
        /// </summary>
        public string zjh { get; set; }
        /// <summary>
        /// Số CMND/CCCD
        /// </summary>
        public string idCardNo { get; set; }
        /// <summary>
        /// Số liên hệ
        /// </summary>
        public string contactNum { get; set; }
        /// <summary>
        /// Giới tính
        /// </summary>
        public string sex { get; set; }
        /// <summary>
        /// Giới tính
        /// </summary>
        public string sexValue { get; set; }
        /// <summary>
        /// Ngày sinh
        /// </summary>
        public string birth { get; set; }
        /// <summary>
        /// Trạng thái
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// Trạng thái
        /// </summary>
        public string statusValue { get; set; }
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
        /// <summary>
        /// Thời gian đăng ký khám
        /// </summary>
        public string ghsj { get; set; }
        /// <summary>
        /// Người liên hệ khẩn cấp
        /// </summary>
        public string contPerName { get; set; }
        /// <summary>
        /// Điện thoại người liên hệ khẩn cấp
        /// </summary>
        public string contPerPhoneNum { get; set; }
        /// <summary>
        /// Quan hệ người liên hệ khẩn cấp
        /// </summary>
        public string contPerRel { get; set; }
        /// <summary>
        /// Quan hệ người liên hệ khẩn cấp
        /// </summary>
        public string contPerRelValue { get; set; }
        /// <summary>
        /// Thời gian cập nhật lần cuối
        /// </summary>
        public string updateTime { get; set; }

        /// <summary>
        /// Cờ khám ngoại trú/cấp cứu
        /// </summary>
        public string mjzbz { get; set; }

        /// <summary>
        /// Cờ khám ngoại trú/cấp cứu
        /// </summary>
        public string mjzbzValue { get; set; }

        /// <summary>
        /// Cờ khám bệnh
        /// </summary>
        public string jiuzhenbz { get; set; }

        /// <summary>
        /// Cờ khám bệnh
        /// </summary>
        public string jiuzhenbzValue { get; set; }

        /// <summary>
        /// Thời gian thao tác đăng ký khám
        /// </summary>
        public string operatingTime { get; set; }

        /// <summary>
        /// Số quyết toán BHYT(Tiền tố)
        /// </summary>
        public string ybjsh { get; set; }

        /// <summary>
        /// Cờ tái khám 1Tái khám
        /// </summary>
        public string fzbz { get; set; }

        /// <summary>
        /// Số BHXH
        /// </summary>
        public string sbbh { get; set; }

        /// <summary>
        /// Mã nơi tham gia bảo hiểm
        /// </summary>
        public string cbdbm { get; set; }

        /// <summary>
        /// Pinyin(Chữ cái đầu họ tên bệnh nhân)
        /// </summary>
        public string py { get; set; }
        /// <summary>
        /// Số thẻ
        /// </summary>
        public string kh { get; set; }

        public string nlshow { get; set; }
        /// <summary>
        /// Nhãn nguồn đăng ký khám 2020-3-4 Thêm mới
        /// </summary>
        public string ghlybz { get; set; }
        public short? queno { get; set; }
    }
}
