using System;
using System.Collections.Generic;

namespace Newtouch.PDS.Requset.PharmacyDepartment
{
    /// <summary>
    /// Ủy quyền khoa dược
    /// </summary>
    public class EmpowermentPharmacyDepartmentAndRemoveOldRequestDto
    {
        /// <summary>
        /// Ủy quyền khoa dược
        /// </summary>
        public List<EmpowermentPharmacyDepartment> epds { get; set; }
        public DateTime Timestamp { get; set; }
    }

    /// <summary>
    /// Ủy quyền khoa dược
    /// </summary>
    public class EmpowermentPharmacyDepartmentRequestDto
    {
        /// <summary>
        /// Thuốc của khoaId
        /// </summary>
        public string bmypId { get; set; }
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Tổ chứcID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Khoa dược
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// Mã thuốc
        /// </summary>
        public string Ypdm { get; set; }

        /// <summary>
        /// Vị trí kho thuốc
        /// </summary>
        public string Ypkw { get; set; }

        /// <summary>
        /// Số thứ tự sổ sách
        /// </summary>
        public string Zcxh { get; set; }

        /// <summary>
        /// Cách sắp xếp1
        /// </summary>
        public string Pxfs1 { get; set; }

        /// <summary>
        /// Cách sắp xếp2
        /// </summary>
        public string Pxfs2 { get; set; }

        /// <summary>
        /// Giới hạn trên tồn kho
        /// </summary>
        public int? Kcsx { get; set; }

        /// <summary>
        /// Giới hạn dưới tồn kho
        /// </summary>
        public int? Kcxx { get; set; }

        /// <summary>
        /// Điểm nhập hàng Thấp hơn điểm nhập hàng thì tự động tạo phiếu lĩnh nội bộ
        /// </summary>
        public int? Jhd { get; set; }

        /// <summary>
        /// Số lượng kế hoạch Điểm nhập hàng tương ứng，Chỉ định số lượng lĩnh nội bộ
        /// </summary>
        public int? Jhl { get; set; }

        /// <summary>
        /// Thuộc tính thuốc Đến từ bảng xt_yptssx
        /// </summary>
        public string Ypsxdm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? Ylsx { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? Sysx { get; set; }

        /// <summary>
        /// Thứ tự
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// Trạng thái  1：Bình thường 0：Kiểm soát
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Tạo lái thử
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Thời gian sửa
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người sửa
        /// </summary>
        public string LastModifierCode { get; set; }
    }

    /// <summary>
    /// Ủy quyền nhà thuốc
    /// </summary>
    public class EmpowermentPharmacyDepartment
    {
        /// <summary>
        /// Thuốc của khoaId
        /// </summary>
        public string bmypId { get; set; }

        /// <summary>
        /// Tổ chứcID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Khoa dược
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// Mã thuốc
        /// </summary>
        public string Ypdm { get; set; }

        /// <summary>
        /// Vị trí kho thuốc
        /// </summary>
        public string Ypkw { get; set; }

        /// <summary>
        /// Số thứ tự sổ sách
        /// </summary>
        public string Zcxh { get; set; }

        /// <summary>
        /// Cách sắp xếp1
        /// </summary>
        public string Pxfs1 { get; set; }

        /// <summary>
        /// Cách sắp xếp2
        /// </summary>
        public string Pxfs2 { get; set; }

        /// <summary>
        /// Giới hạn trên tồn kho
        /// </summary>
        public int? Kcsx { get; set; }

        /// <summary>
        /// Giới hạn dưới tồn kho
        /// </summary>
        public int? Kcxx { get; set; }

        /// <summary>
        /// Điểm nhập hàng Thấp hơn điểm nhập hàng thì tự động tạo phiếu lĩnh nội bộ
        /// </summary>
        public int? Jhd { get; set; }

        /// <summary>
        /// Số lượng kế hoạch Điểm nhập hàng tương ứng，Chỉ định số lượng lĩnh nội bộ
        /// </summary>
        public int? Jhl { get; set; }

        /// <summary>
        /// Thuộc tính thuốc Đến từ bảng xt_yptssx
        /// </summary>
        public string Ypsxdm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? Ylsx { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? Sysx { get; set; }

        /// <summary>
        /// Thứ tự
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// Trạng thái  1：Bình thường 0：Kiểm soát
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Tạo lái thử
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Thời gian sửa
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người sửa
        /// </summary>
        public string LastModifierCode { get; set; }
    }
}