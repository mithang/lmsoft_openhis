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
        /// liên kết billing Hệ thống của đăng ký khám bảng
        /// </summary>
        public string mzh { get; set; }

        /// <summary>
        /// Dư thừa đăng ký khám thông tin  Liệt kê 1 thông thường Ngoại trú 2 gấp khám 3chuyên gia
        /// </summary>
        public int mjzbz { get; set; }

        /// <summary>
        /// tương ứng billing giao diện trong của Số bệnh án
        /// </summary>
        public string blh { get; set; }

        /// <summary>
        /// Dư thừa Thông tin bệnh nhân chữ đoạn
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// Dư thừa Thông tin bệnh nhân chữ đoạn
        /// </summary>
        public string xb { get; set; }

        /// <summary>
        /// Dư thừa Thông tin bệnh nhân chữ đoạn
        /// </summary>
        public DateTime? csny { get; set; }

        /// <summary>
        /// Dư thừa Thông tin bệnh nhân chữ đoạn
        /// </summary>
        public string brxzmc { get; set; }

        /// <summary>
        /// Dư thừa Thông tin bệnh nhân chữ đoạn Liệt kê
        /// </summary>
        public int? zjlx { get; set; }

        /// <summary>
        /// Dư thừa Thông tin bệnh nhân chữ đoạn
        /// </summary>
        public string zjh { get; set; }

        /// <summary>
        /// Dư thừa đăng ký khám bảng chữ đoạn Tên khoa
        /// </summary>
        public string ghksmc { get; set; }

        /// <summary>
        /// Dư thừa đăng ký khám bảng chữ đoạn Bác sĩ Tên
        /// </summary>
        public string ghys { get; set; }

        /// <summary>
        /// Dư thừa đăng ký khám bảng chữ đoạn
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
        /// đường huyết phương thức đo Sau ăn Lúc đói Ngẫu nhiên
        /// </summary>
        public string xuetangclfs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? xuetang { get; set; }

        /// <summary>
        /// kim đối một bệnh bệnh Bác sĩ vào dòng kiểm tra
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
        /// liên kết base kho Khoa bảng
        /// </summary>
        public string jzks { get; set; }

        /// <summary>
        /// liên kết base kho  người viên bảng(cuối cùng xem khám Bác sĩ)
        /// </summary>
        public string jzys { get; set; }

        /// <summary>
        /// (cuối cùng)xem khám Bác sĩ Tên
        /// </summary>
        public string jzysmc { get; set; }

        /// <summary>
        /// Liệt kê 1 chưa Khám bệnh 2 Khám bệnh trong 3 đã Khám bệnh (dùng đến khôi phục lại Kết thúc Khám bệnh của ghi chép)
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
        /// Bảo hiểm y tế quyết toán số(Bảo hiểm y tế Quay lại)
        /// </summary>
        public string ybjsh { get; set; }
        /// <summary>
        /// ban đầu lại khám Cờ 1lại khám
        /// </summary>
        public string cfzbz { get; set; }
        /// <summary>
        /// xã bảo Số hiệu
        /// </summary>
        public string sbbh { get; set; }
        /// <summary>
        /// tham số bảo địa Mã
        /// </summary>
        public string cbdbm { get; set; }
        /// <summary>
        /// thân cao
        /// </summary>
        public decimal? shengao { get; set; }
        /// <summary>
        /// thu Huyết áp tâm thu
        /// </summary>
        public decimal? shousuoya { get; set; }
        /// <summary>
        /// thư tờ áp
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
        /// đăng ký khám đến nguồn Cờ
        /// </summary>
        public string ghlybz { get; set; }
        /// <summary>
        /// Bảo hiểm y tế Cá nhân Số hiệu
        /// </summary>
        public string grbh { get; set; }

        public string hy { get; set; }

    }
}
