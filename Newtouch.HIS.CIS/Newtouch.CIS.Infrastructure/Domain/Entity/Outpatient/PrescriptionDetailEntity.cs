using Newtouch.Infrastructure;
using Newtouch.CIS.Compat;
using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [SugarTable("xt_cfmx")]
    public class PrescriptionDetailEntity
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string cfmxId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// liên kếtĐơn thuốcbảng
        /// </summary>
        public string cfId { get; set; }

        /// <summary>
        /// liên kếtbasekhocủaMục thu phíbảng
        /// </summary>
        public string xmCode { get; set; }

        /// <summary>
        /// Dư thừachữđoạn
        /// </summary>
        public string xmmc { get; set; }

        /// <summary>
        /// liên kếtbasekho của thuốcbảng
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// Dư thừaMã
        /// </summary>
        public string ypmc { get; set; }
        /// <summary>
        /// Liều mỗi lần điều trị  kimđốiPhục hồi chức năngMục
        /// </summary>
        public int? mczll { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DecimalPrecision(11,4)]
        public decimal? mcjl { get; set; }

        /// <summary>
        /// lấybasekhocủaThuộc tính thuốcbảng
        /// </summary>
        public string mcjldw { get; set; }

        /// <summary>
        /// liên kếtbasekhocủaCách dùng thuốcbảng
        /// </summary>
        public string yfCode { get; set; }

        /// <summary>
        /// liên kếtbasekhocủaTần suất y lệnh
        /// </summary>
        public string pcCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// tổngtínhphíSố lượng
        /// </summary>
        public int? zl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DecimalPrecision(11,4)]
        public decimal dj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal je { get; set; }

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

        /// <summary>
        /// 
        /// </summary>
        public string zh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string bw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string urgent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string purpose { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string zxks { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? zxsj { get; set; }
        /// <summary>
        /// Bảo hiểm y tếduy nhấtmộtmã
        /// </summary>
        public string ybwym { get; set; }
        /// <summary>
        /// hạnchếSử dụngCờ0khônghạnchế 1khôngkýhợphạnchếquyđịnh 2kýhợphạnchếquyđịnh
        /// </summary>
        public string xzsybz { get; set; }
        /// <summary>
        /// Số ngày
        /// </summary>
        public decimal? ts { get; set; }
        /// <summary>
        /// Bộ mẫusởthuộcmẫu
        /// </summary>
        public string ztmbId { get; set; }
        /// <summary>
        /// Bộ mẫuid
        /// </summary>
        public string ztId { get; set; }
        /// <summary>
        /// Tên bộ mẫu
        /// </summary>
        public string ztmc { get; set; }
        /// <summary>
        /// Bộ mẫuSố hiệu
        /// </summary>
        public string ztCode { get; set; }

        /// <summary>
        /// Kháng sinhnguyên nhân
        /// </summary>
        public string kssReason { get; set; }

        /// <summary>
        /// Vị tríPhương pháp
        /// </summary>
        public string bwff { get; set; }

        public string sqlx { get; set; }

        public int? px { get; set; }
        /// <summary>
        /// tốc độ truyền
        /// </summary>
        public int? ds { get; set; }

        /// <summary>
        /// Chuyển sang tự chi trảCờ
        /// </summary>
        public int? zzfbz { get; set; }

        //dathửCờ
        public string ispsbz { get; set; }

        //giữxemCờ
        public string islgbz { get; set; }
        /// <summary>
        /// Bộ mẫuSố lượng
        /// </summary>
        public int? ztsl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? sfzt { get; set; }
        public string zysm { get; set; }
        /// <summary>
        /// Cách dùngtương ứngcủaMụcBộ mẫu
        /// </summary>
        public string syncfbz { get; set; }
        /// <summary>
        /// Mã BHYT quốc gia
        /// </summary>
        public string gjybdm { get; set; }
    }
}
