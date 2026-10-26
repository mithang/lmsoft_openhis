using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("V_S_xt_ypsx")]
    public class SysMedicinePropertyVEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        public int ypsxId { get; set; }

        /// <summary>
        /// liên kết thuốc Id
        /// </summary>
        public int ypId { get; set; }

        /// <summary>
        /// thuốc Code
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// 0 hạng mục thường 1 đặc biệt hạng mục đặc biệt thuốc mô tả có đặc thù của tính chất bệnh nhân mới có thể sử dụng của.
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
        /// điều trị phương thức (cũng liên kết cách dùng thuốc)
        /// </summary>
        public string zlff { get; set; }

        /// <summary>
        /// sắp xếp thời gian
        /// </summary>
        public string sjap { get; set; }

        /// <summary>
        /// liều lượng
        /// </summary>
        public decimal? yl { get; set; }

        /// <summary>
        /// đơn vị liều lượng
        /// </summary>
        public string yldw { get; set; }

        /// <summary>
        /// quy cách thuốc
        /// </summary>
        public string ypgg { get; set; }

        /// <summary>
        /// mã BHYT
        /// </summary>
        public string ybdm { get; set; }

        /// <summary>
        /// khiến dùng ngày số (trường cài đặt khống chế số lượng BHYT)
        /// </summary>
        public int? syts { get; set; }

        /// <summary>
        /// Liều tối đa một lần (trường cài đặt khống chế số lượng BHYT)
        /// </summary>
        public decimal? dczdjl { get; set; }

        /// <summary>
        /// Số lượng tối đa một lần (trường cài đặt khống chế số lượng BHYT)
        /// </summary>
        public decimal? dczdsl { get; set; }

        /// <summary>
        /// Liều tối đa lũy kế (trường cài đặt khống chế số lượng BHYT)
        /// </summary>
        public decimal? ljzdjl { get; set; }

        /// <summary>
        /// Số lượng tối đa lũy kế (trường cài đặt khống chế số lượng BHYT)
        /// </summary>
        public decimal? ljzdsl { get; set; }

        /// <summary>
        /// số giấy phép
        /// </summary>
        public string pzwh { get; set; }

        /// <summary>
        /// thuốc đặc biệt thuộc tính
        /// </summary>
        public string yptssx { get; set; }

        /// <summary>
        /// phân loại thuốc Code
        /// </summary>
        public string ypflCode { get; set; }

        /// <summary>
        /// ghi sổ loại    0: mỗi lần làm tròn thu phí 1: mỗi lần làm tròn thu phí
        /// </summary>
        public string jzlx { get; set; }

        /// <summary>
        /// mặc định hạn bảo quản
        /// </summary>
        public int? mrbzq { get; set; }

        /// <summary>
        /// Đơn vị cung ứng
        /// </summary>
        public string ghdw { get; set; }

        /// <summary>
        /// thuốc nơi sản xuất
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
