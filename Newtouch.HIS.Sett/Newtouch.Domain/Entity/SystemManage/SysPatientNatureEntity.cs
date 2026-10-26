using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("xt_brxz")]
    public class SysPatientNatureEntity : IEntity<SysPatientNatureEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int brxzbh { get; set; }

        /// <summary>
        /// Tổ chứcId(Bệnh viện có nghiệp vụ cụ thể)
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string brxz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string brxzmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// 0 Loại mà tất cả các thẻ đều có thể chọn   1 Loại thẻ BHYT có thể chọn
        /// </summary>
        public string brxzlb { get; set; }

        /// <summary>
        /// 0 Không giao dịch 1 Giao dịch thông thường 2 Giao dịch bệnh nặng 3 Giao dịch giường gia đình
        /// </summary>
        public string ybjylx { get; set; }

        /// <summary>
        /// 0:Thông thường  1:Nghỉ hưu  2:Thương tật   Cài đặt ở đây chỉ ảnh hưởng khi thêm bệnh nhân mới"Tính chất bệnh nhân"Lọc,Việc xác định phạm vi tất cả chi phí giao dịch sử dụng"Phạm vi chi phí giao dịch"Để kiểm soát      --Vấn đề tồn tại trong ý nghĩa của trường này!!!!!!   khi"Thương tật"Khi bệnh nhân,Không thể chọn thuộc tính tự chi trả
        /// </summary>
        public string ybtsdy { get; set; }

        /// <summary>
        /// Phạm vi chi phí giao dịch BHYT   0 Phần được ghi sổ tham gia tính toán(BHYT thông thường)   1 Được ghi sổ＋Giao dịch tự chi trả theo phân loại(Thương tật)   2 Được ghi sổ＋Tự chi trả theo phân loại＋Giao dịch tự lo(Tạm thời không có)      ----cmsTrường hủy
        /// </summary>
        public string jyfyfw { get; set; }

        /// <summary>
        /// 0 Không dùng chứng từ   1 Dùng chứng từ
        /// </summary>
        public string pzbz { get; set; }

        /// <summary>
        /// 0 Dùng chung 1 Chỉ ngoại trú 2 Chỉ nội trú    3 Hệ thống(Ngoại trú và nội trú đều không khả dụng)Ví dụ:Giường gia đình
        /// </summary>
        public string mzzybz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? bzs { get; set; }

        /// <summary>
        /// 0 Không được hưởng giảm miễn(Phí khám chữa bệnh)   1 Được hưởng giảm miễn
        /// </summary>
        public string sqjmbz { get; set; }

        /// <summary>
        /// from xt_zhxz   Nếu có cài đặt:Thì khi quyết toán,Tài khoản được cài đặt khấu trừ ưu tiên   (Ví dụ:Trường này của bệnh nhân trợ giúp khó khăn được đặt thành4,Khi quyết toán,Trừ tài khoản trợ giúp khó khăn trước)
        /// </summary>
        public string zhxz { get; set; }

        /// <summary>
        /// 0 Không hiệu lực 1 Hiệu lực
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 0:Bảo hiểm thành thị 1:BHYT cá thể 2:Thị trấn nhỏ 3:Lao động theo giờ   A:Giúp đỡ khó khăn y tế dân chính   B:Di chúc người già C:Người già được bảo đảm năm mặt D:Người khuyết tật nặng   E:Học sinh tiểu học, trung học và trẻ sơ sinh  F: Cư dân khác   G:Đối tượng tương trợ y tế giúp đỡ khó khăn   
        /// </summary>
        public string syybbf { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string brxxpz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string fpdymb { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string fpdyghmb { get; set; }

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
        public int? px { get; set; }
        public string bz { get; set; }

        /// <summary>
        /// Khóa chính liên kếtbrxzbh
        /// </summary>
        public int? ParentId { get; set; }
        public string insutype { get; set; }
    }
}
