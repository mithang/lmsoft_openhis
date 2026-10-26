using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.Infrastructure;
using Newtouch.Infrastructure.EF.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("xt_cfmx")]
    public class PrescriptionDetailEntity : IEntity<PrescriptionDetailEntity>
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
        /// Liên kết bảng đơn thuốc
        /// </summary>
        public string cfId { get; set; }

        /// <summary>
        /// Liên kếtbaseBảng hạng mục thu phí của CSDL
        /// </summary>
        public string xmCode { get; set; }

        /// <summary>
        /// Trường dư thừa
        /// </summary>
        public string xmmc { get; set; }

        /// <summary>
        /// Liên kếtbaseBảng thuốc của CSDL
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// Mã dư thừa
        /// </summary>
        public string ypmc { get; set; }
        /// <summary>
        /// Liều lượng mỗi lần điều trị  Đối với hạng mục phục hồi chức năng
        /// </summary>
        public int? mczll { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DecimalPrecision(11,4)]
        public decimal? mcjl { get; set; }

        /// <summary>
        /// LấybaseBảng thuộc tính thuốc của CSDL
        /// </summary>
        public string mcjldw { get; set; }

        /// <summary>
        /// Liên kếtbaseBảng cách dùng thuốc của CSDL
        /// </summary>
        public string yfCode { get; set; }

        /// <summary>
        /// Liên kếtbaseTần suất y lệnh của CSDL
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
        /// Tổng số lượng tính phí
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
        /// Mã duy nhất BHYT
        /// </summary>
        public string ybwym { get; set; }
        /// <summary>
        /// Cờ hạn chế sử dụng0Không giới hạn 1Không phù hợp quy định giới hạn 2Phù hợp quy định giới hạn
        /// </summary>
        public string xzsybz { get; set; }
        /// <summary>
        /// Số ngày
        /// </summary>
        public decimal? ts { get; set; }
        /// <summary>
        /// Mẫu thuộc về gói
        /// </summary>
        public string ztmbId { get; set; }
        /// <summary>
        /// Góiid
        /// </summary>
        public string ztId { get; set; }
        /// <summary>
        /// Tên gói
        /// </summary>
        public string ztmc { get; set; }
        /// <summary>
        /// Số gói
        /// </summary>
        public string ztCode { get; set; }

        /// <summary>
        /// Lý do dùng kháng sinh
        /// </summary>
        public string kssReason { get; set; }

        /// <summary>
        /// Phương pháp vị trí
        /// </summary>
        public string bwff { get; set; }

        public string sqlx { get; set; }

        public int? px { get; set; }
        /// <summary>
        /// Tốc độ truyền
        /// </summary>
        public int? ds { get; set; }

        /// <summary>
        /// Cờ chuyển sang tự chi trả
        /// </summary>
        public int? zzfbz { get; set; }

        //Cờ thử da
        public string ispsbz { get; set; }

        //Cờ lưu lại theo dõi
        public string islgbz { get; set; }
        /// <summary>
        /// Số lượng gói
        /// </summary>
        public int? ztsl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? sfzt { get; set; }
        public string zysm { get; set; }
        /// <summary>
        /// Gói hạng mục tương ứng với cách dùng
        /// </summary>
        public string syncfbz { get; set; }
        /// <summary>
        /// Mã BHYT quốc gia
        /// </summary>
        public string gjybdm { get; set; }
    }
}
