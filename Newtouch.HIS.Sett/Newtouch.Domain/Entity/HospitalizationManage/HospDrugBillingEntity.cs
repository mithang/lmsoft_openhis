using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("zy_ypjfb")]
    public class HospDrugBillingEntity : IEntity<HospDrugBillingEntity>
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
        /// 
        /// </summary>
        public string yzwym { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? zxid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? mxid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime tdrq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string yp { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string yfdm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string bzdm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte? ts { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public short? cls { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zlff { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ycqh { get; set; }

        /// <summary>
        /// Tạm thời không sử dụng
        /// </summary>
        public decimal? yl { get; set; }

        /// <summary>
        /// Tạm thời không sử dụng
        /// </summary>
        public string yldw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ys { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ysmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ks { get; set; }

        /// <summary>
        /// 
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
        /// 
        /// </summary>
        public string lyyf { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public short? pyry { get; set; }

        /// <summary>
        /// Quy tắc tạm thời:In phiếu phát thuốc thì cập nhật ngày pha thuốc,Và trừ tồn kho.Ngày phát thuốc không xử lý.   	Chờ sau này khi y tá cần lĩnh thuốc thì trừ tồn kho rồi cải tạo theo quy trình phát thuốc ngoại trú
        /// </summary>
        public DateTime? pyrq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string fyry { get; set; }

        /// <summary>
        /// Thời gian thực hiện y lệnh tương ứng đồng bộ y lệnh Phiên Vân
        /// </summary>
        public DateTime? fyrq { get; set; }

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
        public DateTime? cxrq { get; set; }

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
        public int? zzfbz { get; set; }
    }
}
