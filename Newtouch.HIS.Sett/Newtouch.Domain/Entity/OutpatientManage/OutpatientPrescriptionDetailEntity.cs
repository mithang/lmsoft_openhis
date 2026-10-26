using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Chi tiết đơn thuốc ngoại trú
    /// </summary>
    [Table("mz_cfmx")]
    public class OutpatientPrescriptionDetailEntity : IEntity<OutpatientPrescriptionDetailEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int cfmxId { get; set; }

        /// <summary>
        /// Tổ chứcId(Bệnh viện có nghiệp vụ cụ thể)
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int cfnm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string yp { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal dj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal sl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal je { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? zfbl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zfxz { get; set; }

        /// <summary>
        /// Liều dùng
        /// </summary>
        public decimal? yl { get; set; }

        /// <summary>
        /// Đơn vị liều lượng
        /// </summary>
        public string yldw { get; set; }

        /// <summary>
        /// Liều lượng
        /// </summary>
        public decimal? jl { get; set; }

        /// <summary>
        /// Đơn vị liều dùng
        /// </summary>
        public string jldw { get; set; }

        /// <summary>
        /// Đơn vị
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// Cờ trả thuốc.0 Chưa trả lại 1 Đã trả lại 2 Trả lại một phần(Chưa kích hoạt)   Sau khi nhà thuốc nhận trả thuốc,Đặt cờ này1,   Sau khi nơi thu phí hoàn phí,Cờ này không thao tác(Chưa phát thuốc thì vẫn là0,Đã phát thuốc vẫn là1)   --2007.03.27Chú thích Sau khi nơi thu phí hoàn phí,Đặt cờ này1(Xử lý đơn thuốc chưa lĩnh thuốc)      --2008-01-06 Nếu sau này làm trả lại một phần(Đã thu2Hộp,Trả lại1Hộp),Phải kích hoạt2(Trả lại một phần),Và phải kiểm tra tất cả các báo cáo và nơi chương trình xử lý cờ này
        /// </summary>
        public string tybz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string bz { get; set; }

        /// <summary>
        /// Số lẻ đã tách ngoại trú(Tương ứng với bao nhiêu đơn vị nhỏ nhất)
        /// </summary>
        public short? cls { get; set; }

        /// <summary>
        /// Mã nhà thuốc(Nên nhất quán với bảng chính)(Thuốc của một đơn thuốc nên thuộc một nhà thuốc)
        /// </summary>
        public string yfdm { get; set; }

        /// <summary>
        /// Sắp xếp
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// Trạng thái
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Số nhóm Đặt trong bảng chi tiết đơn thuốc
        /// </summary>
        public string czh { get; set; }

        /// <summary>
        /// Chi tiết kế hoạch khám bệnhID
        /// </summary>
        public string jzjhmxId { get; set; }

        /// <summary>
        /// Loại phục hồi chức năng
        /// </summary>
        public string kflb { get; set; }

        /// <summary>
        /// Mã cách dùng
        /// </summary>
        public string yfCode { get; set; }
        /// <summary>
        /// Cờ chuyển sang tự chi trả
        /// </summary>
        public int? zzfbz { get; set; }
        /// <summary>
        /// Tần suất
        /// </summary>
        public string pcCode { get; set; }
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
    }
}
