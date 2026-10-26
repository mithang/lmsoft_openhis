using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("xt_jz")]
    public class TreatmentEntity : IEntity<TreatmentEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string jzId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Liên kếtbillingBảng đăng ký khám của hệ thống
        /// </summary>
        public string mzh { get; set; }

        /// <summary>
        /// Thông tin đăng ký khám dư thừa  Enum 1 Khám thường ngoại trú 2 Cấp cứu 3Chuyên gia
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
        /// Trường bảng đăng ký khám dư thừa Tên khoa
        /// </summary>
        public string ghksmc { get; set; }

        /// <summary>
        /// Trường bảng đăng ký khám dư thừa Tên bác sĩ
        /// </summary>
        public string ghys { get; set; }

        /// <summary>
        /// Trường bảng đăng ký khám dư thừa
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
        /// Đối với một bệnh Bác sĩ phán đoán
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
        /// Liên kếtbaseCSDL Bảng khoa
        /// </summary>
        public string jzks { get; set; }

        /// <summary>
        /// Liên kếtbaseCSDL Bảng nhân viên(Bác sĩ khám cuối cùng)
        /// </summary>
        public string jzys { get; set; }

        /// <summary>
        /// (Cuối cùng)Tên bác sĩ khám
        /// </summary>
        public string jzysmc { get; set; }

        /// <summary>
        /// Enum 1 Chưa khám 2 Đang khám 3 Đã khám (Dùng để khôi phục hồ sơ kết thúc khám bệnh)
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
        /// Số bảo hiểm xã hội
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
        /// Số điện thoại liên hệ
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
        /// Cờ nguồn đăng ký khám
        /// </summary>
        public string ghlybz { get; set; }
        /// <summary>
        /// Số cá nhân BHYT
        /// </summary>
        public string grbh { get; set; }

        public string hy { get; set; }

    }
}
