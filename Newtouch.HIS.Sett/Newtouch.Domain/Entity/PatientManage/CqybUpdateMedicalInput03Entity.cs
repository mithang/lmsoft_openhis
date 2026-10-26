using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.HIS.Domain.Entity.PatientManage
{
    /// <summary>
    /// BHYT Trùng Khánh_Cập nhật khám bệnh03Tham số đầu vào được ghi nhận
    /// </summary>
    [Table("cqyb_OutPutInPar03")]
    public class CqybUpdateMedicalInput03Entity: IEntity<CqybUpdateMedicalInput03Entity>
    {
        /// <summary>
        /// Khóa chínhID
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Số ngoại trú/nội trú
        /// </summary>
        public string zymzh { get; set; }
        /// <summary>
        /// Loại hình y tế
        /// </summary>
        public string yllb { get; set; }
        /// <summary>
        /// Dấu khám đặc biệt cho cư dân
        /// </summary>
        public string jmtsjzbj { get; set; }
        /// <summary>
        /// Loại giao dịch 1:Khám ngoại trú 2:Nhập viện
        /// </summary>
        public string jytype { get; set; }

        /// <summary>
        /// Tổ chứcId(Bệnh viện có nghiệp vụ cụ thể)
        /// </summary>
        public string OrganizeId { get; set; }
        /// <summary>
        /// Người tạoID
        /// </summary>
        public string CreatorCode { get; set; }
        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// Thời gian sửa đổi lần cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// Người sửa đổi lần cuốiID
        /// </summary>
        public string LastModifierCode { get; set; }
        /// <summary>
        /// Sắp xếp
        /// </summary>
        public int px { get; set; }
        /// <summary>
        /// Trạng thái
        /// </summary>
        public string zt { get; set; }
        /// <summary>
        /// Nhập viện(Khám bệnh)Mã khoa
        /// </summary>
        public string ryksbm { get; set; }
        /// <summary>
        /// Nhập viện(Khám bệnh)Mã bác sĩ(CMND/CCCD)
        /// </summary>
        public string ryysbm { get; set; }
        /// <summary>
        /// Mã bác sĩ quốc gia khi nhập viện
        /// </summary>
        public string rygjysbm { get; set; }
        /// <summary>
        /// Mã bác sĩ quốc gia chẩn đoán xuất viện
        /// </summary>
        public string cyzdgjysbm { get; set; }
    }
}
