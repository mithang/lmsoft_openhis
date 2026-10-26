using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.Entity.PatientManage
{
    /// <summary>
    /// BHYT Trùng Khánh_Đăng ký khám bệnh02Tham số đầu vào được ghi nhận
    /// </summary>
    [Table("cqyb_OutPutInPar02")]
    public class CqybMedicalInPut02Entity : IEntity<CqybMedicalInPut02Entity>
    {
        /// <summary>
        /// Khóa chínhID
        /// </summary>
        [Key]
        public int Id { get; set; }
        
        /// <summary>
        /// Loại giao dịch 1:Khám ngoại trú 2:Nhập viện
        /// </summary>
        public string jytype { get; set; }
        /// <summary>
        /// Số ngoại trú/nội trú
        /// </summary>
        public string zymzh { get; set; }
        /// <summary>
        /// Loại hình y tế
        /// </summary>
        public string yllb { get; set; }
       
        /// <summary>
        /// Số định danh BHXH
        /// </summary>
        public string shbzsbh { get; set; }
        /// <summary>
        /// Mã khoa nhập viện
        /// </summary>
        public string ryksbm { get; set; }
        /// <summary>
        /// Mã bác sĩ nhập viện
        /// </summary>
        public string ryysbm { get; set; }
        /// <summary>
        /// Ngày nhập viện
        /// </summary>
        public string ryrq { get; set; }
        /// <summary>
        /// Chẩn đoán nhập viện
        /// </summary>
        public string ryzd { get; set; }
        /// <summary>
        /// Người xử lý
        /// </summary>
        public string jbr { get; set; }
        /// <summary>
        /// Biến chứng
        /// </summary>
        public string bfz { get; set; }
        /// <summary>
        /// Thời gian cấp cứu chuyển nội trú phát sinh
        /// </summary>
        public DateTime? jzzzysj { get; set; }
        /// <summary>
        /// Số bệnh án
        /// </summary>
        public string bah { get; set; }
        /// <summary>
        /// Số giấy chứng sinh
        /// </summary>
        public string syzhm { get; set; }
        /// <summary>
        /// Ngày sinh trẻ sơ sinh
        /// </summary>
        public DateTime? xsecsrq { get; set; }
        /// <summary>
        /// Dấu khám đặc biệt cho cư dân
        /// </summary>
        public string jmtsjzbj { get; set; }
        /// <summary>
        /// Loại hình bảo hiểm
        /// </summary>
        public string xzlb { get; set; }
        /// <summary>
        /// Mã cá nhân tai nạn lao động
        /// </summary>
        public string gsgrbh { get; set; }
        /// <summary>
        /// Mã đơn vị tai nạn lao động
        /// </summary>
        public string gsdwbh { get; set; }
        /// <summary>
        /// Lời khai chính
        /// </summary>
        public string zs { get; set; }
        /// <summary>
        /// Mô tả triệu chứng
        /// </summary>
        public string zzms { get; set; }
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
        /// Phương thức xác thực 01:BHXH 02:Chứng từ điện tử
        /// </summary>

        public string rzfs { get; set; }
        /// <summary>
        /// Mã bác sĩ quốc gia khi nhập viện
        /// </summary>
        public string gjysbm { get; set; }
    }
}
