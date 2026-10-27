using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewtouchHIS.Base.Domain.Entity.SysManage
{
    ///<summary>
    /// Vị trí hệ thống
    ///</summary>
    [Tenant(DBEnum.SettDb)]
    [SugarTable("xt_brxz", "Thông tin bệnh nhân")]
    public partial class SysPatientNatureEntity : ISysEntity
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
        /// 0 Loại mà mọi thẻ đều chọn được   1 Loại mà thẻ BHYT chọn được
        /// </summary>
        public string brxzlb { get; set; }

        /// <summary>
        /// 0 Không giao dịch 1 Giao dịch thông thường 2 Giao dịch bệnh hiểm nghèo 3 Giao dịch giường tại nhà
        /// </summary>
        public string ybjylx { get; set; }

        /// <summary>
        /// 0:Thông thường  1:Nghỉ hưu theo chế độ  2:Thương tật   Cài đặt này chỉ ảnh hưởng tới việc lọc"Tính chất bệnh nhân"khi thêm bệnh nhân mới,phạm vi mọi chi phí giao dịch được xác định dùng"Phạm vi chi phí giao dịch"để khống chế      --Vấn đề về ý nghĩa của trường này!!!!!!   Khi"Thương tật"bệnh nhân,không thể chọn thuộc tính tự chi trả
        /// </summary>
        public string ybtsdy { get; set; }

        /// <summary>
        /// Phạm vi chi phí giao dịch BHYT   0 Phần được ghi sổ tham gia và Tính toán(BHYT thông thường)   1 Được ghi sổ+giao dịch với tự trả theo phân loại(Thương tật)   2 Được ghi sổ+Tự trả theo phân loại+giao dịch với tự lo(Tạm thời không có)      ----cmsTrường hủy
        /// </summary>
        public string jyfyfw { get; set; }

        /// <summary>
        /// 0 Không dùng chứng từ   1 Dùng chứng từ
        /// </summary>
        public string pzbz { get; set; }

        /// <summary>
        /// 0 Chung 1 Chỉ ngoại trú 2 Chỉ nội trú    3 Hệ thống(Cả ngoại trú và nội trú đều không dùng được)Ví dụ:Giường tại nhà
        /// </summary>
        public string mzzybz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? bzs { get; set; }

        /// <summary>
        /// 0 Không hưởng giảm miễn(Phí khám chữa bệnh)   1 Hưởng giảm miễn
        /// </summary>
        public string sqjmbz { get; set; }

        /// <summary>
        /// from xt_zhxz   Nếu có cài đặt:thì khi quyết toán,ưu tiên trừ tài khoản đã cài đặt   (Ví dụ:bệnh nhân diện hỗ trợ thì trường này đặt là4,Khi quyết toán,trừ trước tài khoản hỗ trợ)
        /// </summary>
        public string zhxz { get; set; }

        /// <summary>
        /// 0 không hợp lệ 1 Hiệu lực
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 0:Bảo hiểm thành phố 1:BHYT cá thể 2:Thị trấn nhỏ 3:Lao động theo giờ   A:Hỗ trợ y tế dân chính   B:Di chúc người cao tuổi C:Người cao tuổi diện ngũ bảo D:Người khuyết tật nặng   E:Học sinh tiểu học, trung học và trẻ sơ sinh  F: Cư dân khác   G:Đối tượng hỗ trợ tương trợ y tế   
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
        /// Liên kếtKhóa chínhbrxzbh
        /// </summary>
        public int? ParentId { get; set; }
        public string insutype { get; set; }
    }
}
