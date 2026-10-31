using FrameworkBase.MultiOrg.DmnService;
using System;
using System.Collections.Generic;
using Newtouch.HIS.API.Common;

namespace Newtouch.PDS.Requset.PharmacyDepartment
{
    /// <summary>
    /// Bộ phận nhà thuốc được ủy quyền
    /// </summary>
    public class EmpowermentPharmacyDepartmentAndRemoveOldRequestDto : RequestBase
    {
        /// <summary>
        /// Bộ phận nhà thuốc được ủy quyền
        /// </summary>
        public List<EmpowermentPharmacyDepartment> epds { get; set; }
    }

    /// <summary>
    /// Bộ phận nhà thuốc được ủy quyền
    /// </summary>
    public class EmpowermentPharmacyDepartmentRequestDto :  RequestBase
    {
        /// <summary>
        /// Thuốc bộ phậnId
        /// </summary>
        public string bmypId { get; set; }

        /// <summary>
        /// Tổ chứcID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Bộ phận nhà thuốc
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
        /// Điểm nhập hàng Thấp hơn điểm nhập hàng thì tự động tạo đơn lĩnh nội bộ
        /// </summary>
        public int? Jhd { get; set; }

        /// <summary>
        /// Lượng kế hoạch Điểm nhập hàng tương ứng,Số lượng lĩnh nội bộ chỉ định
        /// </summary>
        public int? Jhl { get; set; }

        /// <summary>
        /// Thuộc tính thuốc Từ bảng xt_yptssx
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
        /// Sắp xếp
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// Trạng thái  1:Bình thường 0:Kiểm soát
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
        /// Thời gian sửa đổi
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người sửa đổi
        /// </summary>
        public string LastModifierCode { get; set; }
    }

    /// <summary>
    /// Nhà thuốc ủy quyền
    /// </summary>
    public class EmpowermentPharmacyDepartment
    {
        /// <summary>
        /// Thuốc bộ phậnId
        /// </summary>
        public string bmypId { get; set; }

        /// <summary>
        /// Tổ chứcID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Bộ phận nhà thuốc
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
        /// Điểm nhập hàng Thấp hơn điểm nhập hàng thì tự động tạo đơn lĩnh nội bộ
        /// </summary>
        public int? Jhd { get; set; }

        /// <summary>
        /// Lượng kế hoạch Điểm nhập hàng tương ứng,Số lượng lĩnh nội bộ chỉ định
        /// </summary>
        public int? Jhl { get; set; }

        /// <summary>
        /// Thuộc tính thuốc Từ bảng xt_yptssx
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
        /// Sắp xếp
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// Trạng thái  1:Bình thường 0:Kiểm soát
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
        /// Thời gian sửa đổi
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người sửa đổi
        /// </summary>
        public string LastModifierCode { get; set; }
    }
}