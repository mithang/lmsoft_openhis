using FrameworkBase.MultiOrg.DmnService;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtouch.HIS.API.Common;

namespace Newtouch.PDS.Requset.ResourcesOperate
{
    /// <summary>
    /// Bản tin đặt trước
    /// </summary>
    public class OutpatientBookRequestDTO : RequestBase
    {
        /// <summary>
        /// Mã thuốc
        /// </summary>
        [Required]
        public List<ItemDetail> Items { get; set; }

        /// <summary>
        /// Tổ chứcID
        /// </summary>
        [Required]
        public string OrganizeId { get; set; }

        /// <summary>
        /// Mã nhân viên thao tác
        /// </summary>
        public string CreatorCode { get; set; }
    }

    /// <summary>
    /// Chi tiết tài nguyên
    /// </summary>
    public class ItemDetail
    {

        /// <summary>
        /// Mã bộ phận nhà thuốc
        /// </summary>
        [Required]
        public string Yfbm { get; set; }

        /// <summary>
        /// Số đơn thuốc Bắt buộc nhập
        /// </summary>
        [Required]
        public string Cfh { get; set; }

        /// <summary>
        /// Hạng mục/Mã thuốc
        /// </summary>
        [Required]
        public string ItemCode { get; set; }

        /// <summary>
        /// Hạng mục/Số lượng thuốc （Đơn vị bộ phận nhà thuốc）
        /// </summary>
        [Required]
        public decimal ItemCount { get; set; }

        /// <summary>
        /// Hệ số quy đổi vàItemCountSử dụng phối hợp ItemCount*Zhyz=Số lượng đơn vị nhỏ nhất
        /// </summary>
        public int Zhyz { get; set; }

        /// <summary>
        /// Hạng mục/Tên thuốc
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// Quy cách thuốc
        /// </summary>
        public string ItemSpecifications { get; set; }

        /// <summary>
        /// Hạng mục/Đơn giá thuốc （Đơn giá đơn vị bộ phận nhà thuốc vớiItemUnitKhớp）
        /// </summary>
        public decimal ItemUnitPrice { get; set; }

        /// <summary>
        /// Hạng mục/Tên đơn vị thuốc （Đơn vị bộ phận nhà thuốc，vàItemCountKhớp）
        /// </summary>
        public string ItemUnitName { get; set; }

        /// <summary>
        /// Hạng mục/Tên nhà sản xuất thuốc
        /// </summary>
        public string ItemManufacturer { get; set; }

        /// <summary>
        /// Loại thu phí lớn
        /// </summary>
        public string DlCode { get; set; }

        /// <summary>
        /// Liều lượng
        /// </summary>
        public decimal? Dosage { get; set; }

        /// <summary>
        /// Đơn vị liều lượng
        /// </summary>
        public string DosageUnit { get; set; }

        /// <summary>
        /// Tên cách dùng
        /// </summary>
        public string UsageName { get; set; }

        /// <summary>
        /// Số thẻ
        /// </summary>
        public string CardNo { get; set; }

        /// <summary>
        /// Họ tên
        /// </summary>
        public string xm { get; set; }
        /// <summary>
        /// Giới tính
        /// </summary>
        public string xb { get; set; }

        /// <summary>
        /// Tuổi
        /// </summary>
        public int nl { get; set; }

        /// <summary>
        /// Tên tính chất bệnh nhân
        /// </summary>
        public string brxzmc { get; set; }

        /// <summary>
        /// Tên bác sĩ
        /// </summary>
        public string ysmc { get; set; }

        /// <summary>
        /// Tên khoa
        /// </summary>
        public string ksmc { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// Số nhóm
        /// </summary>
        public string GroupNum { get; set; }
    }

    /// <summary>
    /// Mục đặt trước thất bại
    /// </summary>
    public class FailItemDetail : ItemDetail
    {
        /// <summary>
        /// Lý do thất bại
        /// </summary>
        public string FailMsg { get; set; }

        /// <summary>
        /// Mã thất bại
        /// </summary>
        public string FailCode { get; set; }
    }
}
