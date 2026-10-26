using System;

namespace Newtouch.HIS.Domain.DTO.OutputDto
{
    /// <summary>
    /// Bản tin trả về tra cứu thông tin khám bệnh của bệnh nhân
    /// </summary>
    public class PatientTreatmentInfoQueryResponseDTO
    {
        /// <summary>
        /// Khám bệnhID
        /// </summary>
        public string jzId { get; set; }

        /// <summary>
        /// Tổ chứcID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Liên kếtbillingBảng đăng ký khám của hệ thống
        /// </summary>
        public string mzh { get; set; }

        /// <summary>
        /// Thông tin đăng ký khám dư thừa  Enum 1 Khám ngoại trú thông thường 2 Cấp cứu 3Chuyên gia
        /// </summary>
        public int mjzbz { get; set; }

        /// <summary>
        /// Tương ứngbillingSố bệnh án trong giao diện
        /// </summary>
        public string blh { get; set; }

        /// <summary>
        /// Trường thông tin bệnh nhân dư thừa
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// Trường thông tin bệnh nhân dư thừa
        /// </summary>
        public string xb { get; set; }

        /// <summary>
        /// Trường thông tin bệnh nhân dư thừa
        /// </summary>
        public DateTime? csny { get; set; }

        /// <summary>
        /// Trường thông tin bệnh nhân dư thừa
        /// </summary>
        public string brxzmc { get; set; }

        /// <summary>
        /// Trường thông tin bệnh nhân dư thừa Enum
        /// </summary>
        public int? zjlx { get; set; }

        /// <summary>
        /// Trường thông tin bệnh nhân dư thừa
        /// </summary>
        public string zjh { get; set; }

        /// <summary>
        /// Trường dư thừa của bảng đăng ký khám Tên khoa
        /// </summary>
        public string ghksmc { get; set; }

        /// <summary>
        /// Trường dư thừa của bảng đăng ký khám Tên bác sĩ
        /// </summary>
        public string ghys { get; set; }

        /// <summary>
        /// Trường dư thừa của bảng đăng ký khám
        /// </summary>
        public DateTime ghsj { get; set; }

        /// <summary>
        /// Thời gian thao tác đăng ký khám
        /// </summary>
        public DateTime? ghczsj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? tizhong { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? tiwen { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? maibo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? xueya { get; set; }

        /// <summary>
        /// Cách đo đường huyết Sau bữa ăn Lúc đói Ngẫu nhiên
        /// </summary>
        public string xuetangclfs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? xuetang { get; set; }

        /// <summary>
        /// Đối với một bệnh Bác sĩ xác định
        /// </summary>
        public bool fzbz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime zlkssj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? zljssj { get; set; }

        /// <summary>
        /// Liên kếtbasekho Bảng khoa
        /// </summary>
        public string jzks { get; set; }

        /// <summary>
        /// Liên kếtbasekho Bảng nhân viên(Bác sĩ khám cuối cùng)
        /// </summary>
        public string jzys { get; set; }

        /// <summary>
        /// (Cuối cùng)Tên bác sĩ khám bệnh
        /// </summary>
        public string jzysmc { get; set; }

        /// <summary>
        /// Enum 1 Chưa khám 2 Đang khám 3 Đã khám (Dùng để khôi phục bản ghi kết thúc khám bệnh)
        /// </summary>
        public int jzzt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }

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

        public string brxzCode { get; set; }

        /// <summary>
        /// Số quyết toán BHYT(BHYT trả về)
        /// </summary>
        public string ybjsh { get; set; }
        /// <summary>
        /// Cờ khám lần đầu/tái khám 1Tái khám
        /// </summary>
        public string cfzbz { get; set; }
        /// <summary>
        /// Số BHXH
        /// </summary>
        public string sbbh { get; set; }
        /// <summary>
        /// Mã nơi tham gia bảo hiểm
        /// </summary>
        public string cbdbm { get; set; }
        /// <summary>
        /// Chiều cao
        /// </summary>
        public decimal? shengao { get; set; }
        /// <summary>
        /// Huyết áp tâm thu
        /// </summary>
        public decimal? shousuoya { get; set; }
        /// <summary>
        /// Huyết áp tâm trương
        /// </summary>
        public decimal? shuzhangya { get; set; }
        /// <summary>
        /// Pinyin
        /// </summary>
        public string py { get; set; }
        /// <summary>
        /// Điện thoại liên hệ
        /// </summary>
        public string ContactNum { get; set; }
        /// <summary>
        /// Số thẻ khám bệnh
        /// </summary>
        public string kh { get; set; }

        public string nlshow { get; set; }
        /// <summary>
        /// Hô hấp
        /// </summary>
        public decimal? huxi { get; set; }

        /// <summary>
        /// Mã chẩn đoán Tây y
        /// </summary>
        public string xyzdCode { get; set; }

        /// <summary>
        /// Tên chẩn đoán Tây y
        /// </summary>
        public string xyzdmc { get; set; }

        /// <summary>
        /// Mã chẩn đoán Trung y
        /// </summary>
        public string zyzdCode { get; set; }

        /// <summary>
        /// Tên chẩn đoán Trung y
        /// </summary>
        public string zyzdmc { get; set; }
    }
}