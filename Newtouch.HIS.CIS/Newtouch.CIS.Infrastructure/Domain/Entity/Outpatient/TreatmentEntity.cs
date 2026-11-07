using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [SugarTable("xt_jz")]
    public class TreatmentEntity
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
        /// liên kếtbilling Hệ thốngcủađăng ký khámbảng
        /// </summary>
        public string mzh { get; set; }

        /// <summary>
        /// Dư thừađăng ký khámthông tin  Liệt kê 1 thông thường Ngoại trú 2 gấpkhám 3chuyên gia
        /// </summary>
        public int mjzbz { get; set; }

        /// <summary>
        /// tương ứngbillinggiao diệntrongcủa Số bệnh án
        /// </summary>
        public string blh { get; set; }

        /// <summary>
        /// Dư thừa Thông tin bệnh nhânchữđoạn
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// Dư thừa Thông tin bệnh nhânchữđoạn
        /// </summary>
        public string xb { get; set; }

        /// <summary>
        /// Dư thừa Thông tin bệnh nhânchữđoạn
        /// </summary>
        public DateTime? csny { get; set; }

        /// <summary>
        /// Dư thừa Thông tin bệnh nhânchữđoạn
        /// </summary>
        public string brxzmc { get; set; }

        /// <summary>
        /// Dư thừa Thông tin bệnh nhânchữđoạn Liệt kê
        /// </summary>
        public int? zjlx { get; set; }

        /// <summary>
        /// Dư thừa Thông tin bệnh nhânchữđoạn
        /// </summary>
        public string zjh { get; set; }

        /// <summary>
        /// Dư thừađăng ký khámbảngchữđoạn Tên khoa
        /// </summary>
        public string ghksmc { get; set; }

        /// <summary>
        /// Dư thừađăng ký khámbảngchữđoạn Bác sĩ Tên
        /// </summary>
        public string ghys { get; set; }

        /// <summary>
        /// Dư thừađăng ký khámbảngchữđoạn
        /// </summary>
        public DateTime ghsj { get; set; }

        /// <summary>
        /// đăng ký khám Thao tác Thời gian
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
        /// đường huyếtphương thức đo Sau ăn Lúc đói Ngẫu nhiên
        /// </summary>
        public string xuetangclfs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? xuetang { get; set; }

        /// <summary>
        /// kimđốimộtbệnhbệnh Bác sĩvàodòngkiểm tra
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
        /// liên kếtbasekho Khoabảng
        /// </summary>
        public string jzks { get; set; }

        /// <summary>
        /// liên kếtbasekho  ngườiviênbảng(cuối cùngxemkhám Bác sĩ)
        /// </summary>
        public string jzys { get; set; }

        /// <summary>
        /// (cuối cùng)xemkhám Bác sĩ Tên
        /// </summary>
        public string jzysmc { get; set; }

        /// <summary>
        /// Liệt kê 1 chưa Khám bệnh 2 Khám bệnhtrong 3 đã Khám bệnh (dùngđếnkhôi phụclại Kết thúc Khám bệnhcủaghi chép)
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
        /// Bảo hiểm y tếquyết toánsố(Bảo hiểm y tế Quay lại)
        /// </summary>
        public string ybjsh { get; set; }
        /// <summary>
        /// ban đầulạikhám Cờ 1lạikhám
        /// </summary>
        public string cfzbz { get; set; }
        /// <summary>
        /// xãbảo Số hiệu
        /// </summary>
        public string sbbh { get; set; }
        /// <summary>
        /// tham sốbảođịa Mã
        /// </summary>
        public string cbdbm { get; set; }
        /// <summary>
        /// thâncao
        /// </summary>
        public decimal? shengao { get; set; }
        /// <summary>
        /// thu Huyết áp tâm thu
        /// </summary>
        public decimal? shousuoya { get; set; }
        /// <summary>
        /// thưtờáp
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
        /// Khám bệnh Số thẻ
        /// </summary>
        public string kh { get; set; }

        public string nlshow { get; set; }
        /// <summary>
        /// Hô hấp
        /// </summary>
        public decimal? huxi { get; set; }

        /// <summary>
        /// đăng ký khámđếnnguồn Cờ
        /// </summary>
        public string ghlybz { get; set; }
        /// <summary>
        /// Bảo hiểm y tế Cá nhân Số hiệu
        /// </summary>
        public string grbh { get; set; }

        public string hy { get; set; }

    }
}
