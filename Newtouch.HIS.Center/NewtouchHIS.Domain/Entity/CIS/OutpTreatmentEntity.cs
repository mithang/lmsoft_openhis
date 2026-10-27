using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Domain.Entity.CIS
{
    [Tenant(DBEnum.CisDb)]
    [SugarTable("xt_jz", "OutpTreatmentEntity")]
    public class OutpTreatmentEntity : IEntity
    {
        /// <summary>
        /// Khám bệnhId
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public string jzId { get; set; }

        /// <summary>
        /// Số ngoại trú
        /// sett.mz_gh
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
        /// Cách đo đường huyết Sau ăn Lúc đói Ngẫu nhiên
        /// </summary>
        public string xuetangclfs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? xuetang { get; set; }

        /// <summary>
        /// Đối với một bệnh bác sĩ xét đoán
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
        /// Liên kếtbaseCơ sở dữ liệu Bảng khoa
        /// </summary>
        public string jzks { get; set; }

        /// <summary>
        /// Liên kếtbaseCơ sở dữ liệu Bảng nhân viên(Bác sĩ khám cuối cùng)
        /// </summary>
        public string jzys { get; set; }

        /// <summary>
        /// (Cuối cùng)Tên bác sĩ khám
        /// </summary>
        public string? jzysmc { get; set; }

        /// <summary>
        /// Enum 1 Chưa khám 2 Đang khám 3 Đã khám (Dùng để khôi phục hồ sơ đã kết thúc khám)
        /// </summary>
        public int? jzzt { get; set; }
     

        public string? brxzCode { get; set; }

        /// <summary>
        /// Số quyết toán BHYT(BHYT trả về)
        /// </summary>
        public string? ybjsh { get; set; }
        /// <summary>
        /// Cờ khám lần đầu/tái khám 1Tái khám
        /// </summary>
        public string? cfzbz { get; set; }
        /// <summary>
        /// Số bảo hiểm xã hội
        /// </summary>
        public string? sbbh { get; set; }
        /// <summary>
        /// Nơi tham gia bảo hiểmMã
        /// </summary>
        public string? cbdbm { get; set; }
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
        /// Phiên âm
        /// </summary>
        public string? py { get; set; }
        /// <summary>
        /// Số điện thoại liên hệ
        /// </summary>
        public string? ContactNum { get; set; }
        /// <summary>
        /// Khám bệnhSố thẻ
        /// </summary>
        public string? kh { get; set; }

        public string? nlshow { get; set; }
        /// <summary>
        /// Hô hấp
        /// </summary>
        public decimal? huxi { get; set; }

        /// <summary>
        /// Cờ nguồn đăng ký khám
        /// </summary>
        public string? ghlybz { get; set; }
        /// <summary>
        /// Số cá nhân BHYT
        /// </summary>
        public string? grbh { get; set; }

        public string? hy { get; set; }
    }

    #region VOĐối tượng

    #endregion
}
