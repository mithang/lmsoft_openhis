using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// Tạo bởi:张冬林
    /// Ngày:2018-06-28 15:37
    /// Mô tả:CSDL yêu cầu trả thuốc nội trú
    /// </summary>
    [Table("zy_tyjl")]
    public class InpatientMedicineReturnEntity : IEntity<InpatientMedicineReturnEntity>
    {
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// Hồ sơ phát thuốc tương ứngId
        /// </summary>
        public string fyId { get; set; }
        /// <summary>
        /// Tổ chứcID
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }
        /// <summary>
        /// Số thứ tự trả thuốc (Đánh dấu một lần trả thuốc)
        /// </summary>
        /// <returns></returns>
        public decimal tyxh { get; set; }
        /// <summary>
        /// Số thứ tự y lệnh
        /// </summary>
        /// <returns></returns>
        public string yzxh { get; set; }
        /// <summary>
        /// Số thứ tự nhóm
        /// </summary>
        public int? fzxh { get; set; }
        /// <summary>
        /// Số nội trú
        /// </summary>
        /// <returns></returns>
        public string zyh { get; set; }
        /// <summary>
        /// Họ tên bệnh nhân
        /// </summary>
        /// <returns></returns>
        public string hzxm { get; set; }
        /// <summary>
        /// Mã khu bệnh
        /// </summary>
        /// <returns></returns>
        public string WardCode { get; set; }
        /// <summary>
        /// Số thứ tự yêu cầu phát thuốc
        /// </summary>
        /// <returns></returns>
        public decimal? fyqqxh { get; set; }
        /// <summary>
        /// Mã thuốc
        /// </summary>
        /// <returns></returns>
        public string ypdm { get; set; }
        /// <summary>
        /// Số lượng trả thuốc
        /// </summary>
        /// <returns></returns>
        public int tysl { get; set; }
        /// <summary>
        /// Số lượng thuốc có thể trả
        /// </summary>
        /// <returns></returns>
        public int ktypsl { get; set; }
        /// <summary>
        /// Quy cách thuốc
        /// </summary>
        /// <returns></returns>
        public string ypgg { get; set; }
        /// <summary>
        /// Đơn vị thuốc
        /// </summary>
        /// <returns></returns>
        public string ypdw { get; set; }
        /// <summary>
        /// Hệ số đơn vị
        /// </summary>
        /// <returns></returns>
        public decimal? dwxs { get; set; }
        /// <summary>
        /// Hệ số kho thuốc
        /// </summary>
        /// <returns></returns>
        public decimal? ykxs { get; set; }
        /// <summary>
        /// Đơn giá thuốc
        /// </summary>
        /// <returns></returns>
        public decimal? ypdj { get; set; }
        /// <summary>
        /// (0 Yêu cầu trả thuốc 1 Trả thuốc thành công)
        /// </summary>
        /// <returns></returns>
        public int? tyqrbz { get; set; }
        /// <summary>
        /// Số thứ tự xác nhận trả thuốc Thay thế cho cái cũ:tyqqxh
        /// </summary>
        /// <returns></returns>
        public decimal? tyqrxh { get; set; }
        /// <summary>
        /// Mã nhân viên trả thuốc
        /// </summary>
        /// <returns></returns>
        public string tyczydm { get; set; }
        /// <summary>
        /// Mã nhân viên trả thuốc nhà thuốc
        /// </summary>
        /// <returns></returns>
        public string yftyczydm { get; set; }
        /// <summary>
        /// Ngày trả thuốc nhà thuốc
        /// </summary>
        /// <returns></returns>
        public DateTime? yftyrq { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>
        /// <returns></returns>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// CreatorCode
        /// </summary>
        /// <returns></returns>
        public string CreatorCode { get; set; }
        /// <summary>
        /// LastModifyTime
        /// </summary>
        /// <returns></returns>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// LastModifierCode
        /// </summary>
        /// <returns></returns>
        public string LastModifierCode { get; set; }
        /// <summary>
        /// 0Vô hiệu1Có hiệu lực
        /// </summary>
        /// <returns></returns>
        public string zt { get; set; }
        /// <summary>
        /// Số phiếu trả thuốc
        /// </summary>
        /// <returns></returns>
        public string tydh { get; set; }
    }
}