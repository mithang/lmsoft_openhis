using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// Thông tin thuốc truyền dịch ngoại trú
    /// </summary>
    [Table("mz_syypxx")]
    public class MzsyypxxEntity : IEntity<MzsyypxxEntity>
    {

        /// <summary>
        /// Khóa chínhID
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// Tổ chức
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Họ tên bệnh nhân
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// Số thẻ khám bệnh của bệnh nhân
        /// </summary>
        public string kh { get; set; }
        public string mzh { get; set; }

        /// <summary>
        /// Số hóa đơn
        /// </summary>
        public string fph { get; set; }

        /// <summary>
        /// Chi tiết đơn thuốcID
        /// </summary>
        public int cfmxId { get; set; }

        /// <summary>
        /// Mã nội bộ chi tiết tính toán
        /// </summary>
        public int jsmxnm { get; set; }

        /// <summary>
        /// Thời gian thu phí  Thời gian quyết toán
        /// </summary>
        public DateTime sfsj { get; set; }

        /// <summary>
        /// Số đơn thuốc
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// Mã thuốc
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// Tên thuốc
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// Quy cách thuốc
        /// </summary>
        public string ypgg { get; set; }

        /// <summary>
        /// Liều dùng
        /// </summary>
        public decimal? yl { get; set; }

        /// <summary>
        /// Đơn vị liều dùng
        /// </summary>
        public string yldw { get; set; }

        /// <summary>
        /// Số lượng
        /// </summary>
        public decimal sl { get; set; }

        /// <summary>
        /// Đơn vị số lượng
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// Liều lượng
        /// </summary>
        public decimal? jl { get; set; }

        /// <summary>
        /// Đơn vị liều lượng
        /// </summary>
        public string jldw { get; set; }

        /// <summary>
        /// Số nhóm
        /// </summary>
        public string groupNo { get; set; }

        /// <summary>
        /// Trạng thái 0:Vô hiệu  1:Có hiệu lực
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian sửa đổi
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người sửa đổi
        /// </summary>
        public string LastModifierCode { get; set; }
        public string yfCode { get; set; }
        public string pcCode { get; set; }
        public int zcs { get; set; }
        public int yzxcs { get; set; }
    }
}
