using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("zy_xmjfb")]
    public class HospItemBillingEntity : IEntity<HospItemBillingEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int jfbbh { get; set; }

        /// <summary>
        /// Tổ chứcId(Bệnh viện có nghiệp vụ cụ thể)
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zyh { get; set; }

        /// <summary>
        /// Mã duy nhất y lệnh
        /// </summary>
        public string yzwym { get; set; }

        /// <summary>
        /// Thực hiện y lệnhId
        /// </summary>
        public int? zxid { get; set; }

        /// <summary>
        /// Chi tiết y lệnhId
        /// </summary>
        public int? mxid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime tdrq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfxm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dl { get; set; }

        /// <summary>
        /// Bác sĩCode
        /// </summary>
        public string ys { get; set; }

        /// <summary>
        /// Tên bác sĩ
        /// </summary>
        public string ysmc { get; set; }

        /// <summary>
        /// KhoaCode
        /// </summary>
        public string ks { get; set; }

        /// <summary>
        /// Tên khoa
        /// </summary>
        public string ksmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal dj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? fwfdj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal sl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string jfdw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? jmje { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? jmbl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? zfbl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zfxz { get; set; }

        /// <summary>
        /// 0 Chờ thực hiện 1 Đã thực hiện(Đã kết thúc thực hiện/Không còn thực hiện)    2 Không cần thực hiện (Trong chương trình cũng hiển thị là đã thực hiện)         //Ghi sổ nội trú,Hạng mục không cần thực hiện,Cờ thực hiện mặc định là1(Đã thực hiện)
        /// </summary>
        public string ssbz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ssry { get; set; }

        /// <summary>
        /// Thời gian thực hiện y lệnh tương ứng đồng bộ y lệnh Phiên Vân
        /// </summary>
        public DateTime? ssrq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zxks { get; set; }

        /// <summary>
        /// 1 Y lệnh tạm thời   2 Y lệnh dài hạn   3 Y lệnh xuất viện
        /// </summary>
        public string yzxz { get; set; }

        /// <summary>
        /// 1 Chưa hủy   2 Đã hủy
        /// </summary>
        public string yzzt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cxry { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? cxtdrq { get; set; }

        /// <summary>
        /// Mặc định0   Thể hiện chưa hủy quyết toán
        /// </summary>
        public int cxzyjfbbh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? price { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? lb { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ybbm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zltime { get; set; }

        /// <summary>
        /// Liên kết thực hiệnId
        /// </summary>
        public string bdzxid { get; set; }

        /// <summary>
        /// from  xt_sfclxmzh  Mã loại vật tư tổng hợp tương ứng
        /// </summary>
        public int? clzhxmbh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

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
        public int? px { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string bq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string jzjhmxId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string kflb { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? ttbz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? duration { get; set; }

        /// <summary>
        /// Tổng liều điều trị
        /// </summary>
        public int? zzll { get; set; }

        /// <summary>
        /// Bộ thu phíID xt_sfmb
        /// </summary>
        public string ztbh { get; set; }

        /// <summary>
        /// Số lượng bộ
        /// </summary>
        public decimal? ztsl { get; set; }
        /// <summary>
        /// Dấu bộ giống nhau
        /// </summary>
        public string dcztbs { get; set; }
        public int? zzfbz { get; set; }
    }
}
