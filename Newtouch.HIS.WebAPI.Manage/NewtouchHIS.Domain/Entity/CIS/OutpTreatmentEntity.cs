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
        /// khám bệnhId
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public string jzId { get; set; }

        /// <summary>
        /// số khám ngoại trú
        /// sett.mz_gh
        /// </summary>
        public string mzh { get; set; }

        /// <summary>
        /// thông tin đăng ký khám dự phòng  enum 1 khám ngoại trú thông thường 2 cấp cứu 3chuyên gia
        /// </summary>
        public int mjzbz { get; set; }

        /// <summary>
        /// tương ứngbillingsố bệnh án trong giao diện
        /// </summary>
        public string blh { get; set; }

        /// <summary>
        /// trường thông tin bệnh nhân dự phòng
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// trường thông tin bệnh nhân dự phòng
        /// </summary>
        public string xb { get; set; }

        /// <summary>
        /// trường thông tin bệnh nhân dự phòng
        /// </summary>
        public DateTime? csny { get; set; }

        /// <summary>
        /// trường thông tin bệnh nhân dự phòng
        /// </summary>
        public string brxzmc { get; set; }

        /// <summary>
        /// trường thông tin bệnh nhân dự phòng enum
        /// </summary>
        public int? zjlx { get; set; }

        /// <summary>
        /// trường thông tin bệnh nhân dự phòng
        /// </summary>
        public string zjh { get; set; }

        /// <summary>
        /// trường dự phòng của bảng đăng ký khám tên khoa
        /// </summary>
        public string ghksmc { get; set; }

        /// <summary>
        /// trường dự phòng của bảng đăng ký khám tên bác sĩ
        /// </summary>
        public string ghys { get; set; }

        /// <summary>
        /// trường dự phòng của bảng đăng ký khám
        /// </summary>
        public DateTime ghsj { get; set; }

        /// <summary>
        /// thời gian thao tác đăng ký khám
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
        /// phương thức đo đường huyết sau bữa ăn lúc đói ngẫu nhiên
        /// </summary>
        public string xuetangclfs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? xuetang { get; set; }

        /// <summary>
        /// đối với một bệnh bác sĩ phán đoán
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
        /// liên kếtbasecơ sở dữ liệu bảng khoa
        /// </summary>
        public string jzks { get; set; }

        /// <summary>
        /// liên kếtbasecơ sở dữ liệu bảng nhân viên（bác sĩ khám cuối cùng）
        /// </summary>
        public string jzys { get; set; }

        /// <summary>
        /// （cuối cùng）tên bác sĩ khám
        /// </summary>
        public string? jzysmc { get; set; }

        /// <summary>
        /// enum 1 chưa khám 2 đang khám 3 đã khám （dùng để khôi phục hồ sơ đã kết thúc khám bệnh）
        /// </summary>
        public int? jzzt { get; set; }
     

        public string? brxzCode { get; set; }

        /// <summary>
        /// số thanh toán bảo hiểm y tế（trả về bảo hiểm y tế）
        /// </summary>
        public string? ybjsh { get; set; }
        /// <summary>
        /// cờ khám lần đầu – tái khám 1tái khám
        /// </summary>
        public string? cfzbz { get; set; }
        /// <summary>
        /// số hiệu bảo hiểm xã hội
        /// </summary>
        public string? sbbh { get; set; }
        /// <summary>
        /// mã nơi tham gia bảo hiểm
        /// </summary>
        public string? cbdbm { get; set; }
        /// <summary>
        /// chiều cao
        /// </summary>
        public decimal? shengao { get; set; }
        /// <summary>
        /// huyết áp tâm thu
        /// </summary>
        public decimal? shousuoya { get; set; }
        /// <summary>
        /// huyết áp tâm trương
        /// </summary>
        public decimal? shuzhangya { get; set; }
        /// <summary>
        /// phiên âm
        /// </summary>
        public string? py { get; set; }
        /// <summary>
        /// điện thoại liên hệ
        /// </summary>
        public string? ContactNum { get; set; }
        /// <summary>
        /// số thẻ khám bệnh
        /// </summary>
        public string? kh { get; set; }

        public string? nlshow { get; set; }
        /// <summary>
        /// hô hấp
        /// </summary>
        public decimal? huxi { get; set; }

        /// <summary>
        /// cờ nguồn đăng ký khám
        /// </summary>
        public string? ghlybz { get; set; }
        /// <summary>
        /// số hiệu cá nhân bảo hiểm y tế
        /// </summary>
        public string? grbh { get; set; }

        public string? hy { get; set; }
    }

    #region VO对象

    #endregion
}
