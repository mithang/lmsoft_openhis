using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.ValueObjects
{
    public class TreatEntityVO
    {
        public string jzId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Bảng đăng ký khám liên kết với hệ thống billing
        /// </summary>
        public string mzh { get; set; }

        /// <summary>
        /// Thông tin đăng ký khám dư thừa - liệt kê: 1 Ngoại trú thông thường, 2 Cấp cứu, 3 Chuyên gia
        /// </summary>
        public int mjzbz { get; set; }

        /// <summary>
        /// Số bệnh án tương ứng trong giao diện billing
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
        /// Trường thông tin bệnh nhân dư thừa - liệt kê
        /// </summary>
        public int? zjlx { get; set; }

        /// <summary>
        /// Trường thông tin bệnh nhân dư thừa
        /// </summary>
        public string zjh { get; set; }

        /// <summary>
        /// Trường bảng đăng ký khám dư thừa - tên khoa
        /// </summary>
        public string ghksmc { get; set; }

        /// <summary>
        /// Trường bảng đăng ký khám dư thừa - tên bác sĩ
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
        /// Cách đo đường huyết: sau ăn, lúc đói, ngẫu nhiên
        /// </summary>
        public string xuetangclfs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? xuetang { get; set; }

        /// <summary>
        /// Bác sĩ phán đoán đối với một bệnh
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
        /// Liên kết cơ sở dữ liệu base - bảng khoa
        /// </summary>
        public string jzks { get; set; }

        /// <summary>
        /// Liên kết cơ sở dữ liệu base - bảng nhân sự (bác sĩ khám cuối cùng)
        /// </summary>
        public string jzys { get; set; }

        /// <summary>
        /// Họ tên bác sĩ khám (cuối cùng)
        /// </summary>
        public string jzysmc { get; set; }

        /// <summary>
        /// Liệt kê: 1 Chưa khám, 2 Đang khám, 3 Đã khám (dùng để khôi phục bản ghi kết thúc khám)
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
        /// Số thanh toán bảo hiểm y tế (bảo hiểm y tế trả về)
        /// </summary>
        public string ybjsh { get; set; }
        /// <summary>
        /// Cờ khám lần đầu/tái khám: 1 Tái khám
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
        /// Bính âm
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
        /// Số cá nhân bảo hiểm y tế
        /// </summary>
        public string grbh { get; set; }
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
        /// Trạng thái tài liệu
        /// </summary>
        public string RecordStu { get; set; }
    }
}
