using System;

namespace Newtouch.Domain.ViewModels
{
    /// <summary>
    /// Khám bệnhthông tin
    /// </summary>
    public class TreatmentVo
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
        /// liên kếtbillingHệ thốngcủađăng ký khámbảng
        /// </summary>
        public string mzh { get; set; }

        /// <summary>
        /// Dư thừađăng ký khámthông tin  Liệt kê 1 thông thườngNgoại trú 2 gấpkhám 3chuyên gia
        /// </summary>
        public int mjzbz { get; set; }

        /// <summary>
        /// tương ứngbillinggiao diệntrongcủaSố bệnh án
        /// </summary>
        public string blh { get; set; }

        /// <summary>
        /// Dư thừaThông tin bệnh nhânchữđoạn
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// Dư thừaThông tin bệnh nhânchữđoạn
        /// </summary>
        public string xb { get; set; }

        /// <summary>
        /// Dư thừaThông tin bệnh nhânchữđoạn
        /// </summary>
        public DateTime? csny { get; set; }

        /// <summary>
        /// Dư thừaThông tin bệnh nhânchữđoạn
        /// </summary>
        public string brxzmc { get; set; }

        /// <summary>
        /// Dư thừaThông tin bệnh nhânchữđoạn Liệt kê
        /// </summary>
        public int? zjlx { get; set; }

        /// <summary>
        /// Dư thừaThông tin bệnh nhânchữđoạn
        /// </summary>
        public string zjh { get; set; }

        /// <summary>
        /// Dư thừađăng ký khámbảngchữđoạn Tên khoa
        /// </summary>
        public string ghksmc { get; set; }

        /// <summary>
        /// Dư thừađăng ký khámbảngchữđoạn Bác sĩTên
        /// </summary>
        public string ghys { get; set; }

        /// <summary>
        /// Dư thừađăng ký khámbảngchữđoạn
        /// </summary>
        public DateTime ghsj { get; set; }

        /// <summary>
        /// đăng ký khámThao tácThời gian
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
        /// liên kếtbasekho  ngườiviênbảng(cuối cùngxemkhámBác sĩ)
        /// </summary>
        public string jzys { get; set; }

        /// <summary>
        /// (cuối cùng)xemkhámBác sĩTên
        /// </summary>
        public string jzysmc { get; set; }

        /// <summary>
        /// Liệt kê 1 chưaKhám bệnh 2 Khám bệnhtrong 3 đãKhám bệnh (dùngđếnkhôi phụclạiKết thúcKhám bệnhcủaghi chép)
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
        /// Bảo hiểm y tếquyết toánsố(Bảo hiểm y tếQuay lại)
        /// </summary>
        public string ybjsh { get; set; }
        /// <summary>
        /// ban đầulạikhámCờ 1lạikhám
        /// </summary>
        public string cfzbz { get; set; }
        /// <summary>
        /// xãbảoSố hiệu
        /// </summary>
        public string sbbh { get; set; }
        /// <summary>
        /// tham sốbảođịaMã
        /// </summary>
        public string cbdbm { get; set; }
        /// <summary>
        /// thâncao
        /// </summary>
        public decimal? shengao { get; set; }
        /// <summary>
        /// thuHuyết áp tâm thu
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
        /// Khám bệnhSố thẻ
        /// </summary>
        public string kh { get; set; }

        public string nlshow { get; set; }
        /// <summary>
        /// Hô hấp
        /// </summary>
        public decimal? huxi { get; set; }

        /// <summary>
        /// Chẩn đoán Tây yMã
        /// </summary>
        public string xyzdCode { get; set; }

        /// <summary>
        /// Chẩn đoán Tây yTên
        /// </summary>
        public string xyzdmc { get; set; }

        /// <summary>
        /// Chẩn đoán YHCTMã
        /// </summary>
        public string zyzdCode { get; set; }

        /// <summary>
        /// Chẩn đoán YHCTTên
        /// </summary>
        public string zyzdmc { get; set; }
    }
}
