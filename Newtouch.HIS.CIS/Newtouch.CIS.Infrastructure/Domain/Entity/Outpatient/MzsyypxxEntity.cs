using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// Ngoại trútruyền dịchThuốcthông tin
    /// </summary>
    [SugarTable("mz_syypxx")]
    public class MzsyypxxEntity
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
        /// Tên bệnh nhân
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// Bệnh nhânKhám bệnhSố thẻ
        /// </summary>
        public string kh { get; set; }
        public string mzh { get; set; }

        /// <summary>
        /// Hóa đơnsố
        /// </summary>
        public string fph { get; set; }

        /// <summary>
        /// Đơn thuốcchi tiếtID
        /// </summary>
        public int cfmxId { get; set; }

        /// <summary>
        /// Tính toánchi tiếttrongmã
        /// </summary>
        public int jsmxnm { get; set; }

        /// <summary>
        /// Thu phíThời gian  quyết toánThời gian
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
        /// dùnglượng
        /// </summary>
        public decimal? yl { get; set; }

        /// <summary>
        /// dùnglượngĐơn vị
        /// </summary>
        public string yldw { get; set; }

        /// <summary>
        /// Số lượng
        /// </summary>
        public decimal sl { get; set; }

        /// <summary>
        /// Số lượngĐơn vị
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// Liều lượng
        /// </summary>
        public decimal? jl { get; set; }

        /// <summary>
        /// Đơn vị liều
        /// </summary>
        public string jldw { get; set; }

        /// <summary>
        /// Số nhóm
        /// </summary>
        public string groupNo { get; set; }

        /// <summary>
        /// Trạng thái 0:Vô hiệu  1:Hiệu lực
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// tạongười
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian sửa
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Sửangười
        /// </summary>
        public string LastModifierCode { get; set; }
        public string yfCode { get; set; }
        public string pcCode { get; set; }
        public int zcs { get; set; }
        public int yzxcs { get; set; }
    }
}
