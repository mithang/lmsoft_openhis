using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("V_S_xt_ypsx")]
    [SugarTable("V_S_xt_ypsx")]
    public class SysMedicinePropertyVEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        public int ypsxId { get; set; }

        /// <summary>
        /// Thuốc liên kếtId
        /// </summary>
        public int ypId { get; set; }

        /// <summary>
        /// ThuốcCode
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// 0 Hạng mục thường 1 Hạng mục đặc biệt, thuốc đặc biệt chỉ dùng cho tính chất bệnh nhân đặc biệt。
        /// </summary>
        public string tsbz { get; set; }

        /// <summary>
        /// Thuốc quý
        /// </summary>
        public string gzy { get; set; }

        /// <summary>
        /// Thuốc gây mê
        /// </summary>
        public string mzy { get; set; }

        /// <summary>
        /// Thuốc hướng thần
        /// </summary>
        public string yljsy { get; set; }

        /// <summary>
        /// Phương pháp điều trị（Cũng vậy Liên kết Cách dùng thuốc）
        /// </summary>
        public string zlff { get; set; }

        /// <summary>
        /// Sắp xếp thời gian
        /// </summary>
        public string sjap { get; set; }

        /// <summary>
        /// Liều dùng
        /// </summary>
        public decimal? yl { get; set; }

        /// <summary>
        /// Đơn vị liều dùng
        /// </summary>
        public string yldw { get; set; }

        /// <summary>
        /// Quy cách thuốc
        /// </summary>
        public string ypgg { get; set; }

        /// <summary>
        /// Mã BHYT
        /// </summary>
        public string ybdm { get; set; }

        /// <summary>
        /// Số ngày sử dụng（Trường cài đặt khống chế số lượng BHYT）
        /// </summary>
        public int? syts { get; set; }

        /// <summary>
        /// Liều lượng tối đa mỗi lần（Trường cài đặt khống chế số lượng BHYT）
        /// </summary>
        public decimal? dczdjl { get; set; }

        /// <summary>
        /// Số lượng tối đa mỗi lần（Trường cài đặt khống chế số lượng BHYT）
        /// </summary>
        public decimal? dczdsl { get; set; }

        /// <summary>
        /// Liều lượng tối đa tích lũy（Trường cài đặt khống chế số lượng BHYT）
        /// </summary>
        public decimal? ljzdjl { get; set; }

        /// <summary>
        /// Số lượng tối đa tích lũy（Trường cài đặt khống chế số lượng BHYT）
        /// </summary>
        public decimal? ljzdsl { get; set; }

        /// <summary>
        /// Số văn bản phê duyệt
        /// </summary>
        public string pzwh { get; set; }

        /// <summary>
        /// Thuộc tính đặc biệt thuốc
        /// </summary>
        public string yptssx { get; set; }

        /// <summary>
        /// Phân loại thuốcCode
        /// </summary>
        public string ypflCode { get; set; }

        /// <summary>
        /// Loại ghi sổ    0：Thu phí làm tròn mỗi bữa1：Thu phí làm tròn mỗi lần
        /// </summary>
        public string jzlx { get; set; }

        /// <summary>
        /// Hạn bảo quản mặc định
        /// </summary>
        public int? mrbzq { get; set; }

        /// <summary>
        /// Đơn vị cung cấp
        /// </summary>
        public string ghdw { get; set; }

        /// <summary>
        /// Nơi sản xuất thuốc
        /// </summary>
        public int? ypcd { get; set; }

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
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

    }
}
