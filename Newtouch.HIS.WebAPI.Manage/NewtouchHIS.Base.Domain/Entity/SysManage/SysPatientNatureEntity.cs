using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewtouchHIS.Base.Domain.Entity.SysManage
{
    ///<summary>
    /// vị trí hệ thống
    ///</summary>
    [Tenant(DBEnum.SettDb)]
    [SugarTable("xt_brxz", "病人信息")]
    public partial class SysPatientNatureEntity : ISysEntity
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int brxzbh { get; set; }

        /// <summary>
        /// tổ chứcId（bệnh viện có nghiệp vụ cụ thể）
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
        /// 0 loại tất cả các thẻ đều có thể chọn   1 loại thẻ bảo hiểm y tế có thể chọn
        /// </summary>
        public string brxzlb { get; set; }

        /// <summary>
        /// 0 không giao dịch 1 giao dịch thông thường 2 giao dịch bệnh nặng 3 giao dịch giường tại nhà
        /// </summary>
        public string ybjylx { get; set; }

        /// <summary>
        /// 0：thông thường  1：nghỉ hưu  2：thương tật   cài đặt ở đây chỉ ảnh hưởng khi thêm bệnh nhân mới“tính chất bệnh nhân”lọc，tất cả phạm vi chi phí giao dịch xác định sử dụng“phạm vi chi phí giao dịch”để điều khiển      --vấn đề về ý nghĩa của trường này！！！！！！   khi“thương tật”khi bệnh nhân，không thể chọn thuộc tính tự trả
        /// </summary>
        public string ybtsdy { get; set; }

        /// <summary>
        /// phạm vi chi phí giao dịch bảo hiểm y tế   0 phần có thể ghi sổ tham gia tính toán（bảo hiểm y tế thông thường）   1 có thể ghi sổ＋giao dịch theo tự trả phân loại（thương tật）   2 có thể ghi sổ＋tự trả theo phân loại＋giao dịch theo tự lo（tạm thời không có）      ----cmstrường hủy
        /// </summary>
        public string jyfyfw { get; set; }

        /// <summary>
        /// 0 không sử dụng chứng từ   1 sử dụng chứng từ
        /// </summary>
        public string pzbz { get; set; }

        /// <summary>
        /// 0 chung 1 chỉ ngoại trú 2 chỉ nội trú    3 hệ thống（cả ngoại trú và nội trú đều không dùng được）Ví dụ：giường tại nhà
        /// </summary>
        public string mzzybz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? bzs { get; set; }

        /// <summary>
        /// 0 không được giảm miễn（phí khám chữa bệnh）   1 được giảm miễn
        /// </summary>
        public string sqjmbz { get; set; }

        /// <summary>
        /// from xt_zhxz   nếu có đặt：thì khi thanh toán，ưu tiên khấu trừ tài khoản đã đặt   （Ví dụ：đối với bệnh nhân được hỗ trợ khó khăn, trường này đặt là4，khi thanh toán，khấu trừ trước tài khoản hỗ trợ khó khăn）
        /// </summary>
        public string zhxz { get; set; }

        /// <summary>
        /// 0 không hiệu lực 1 hiệu lực
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 0：bảo hiểm thành phố 1：bảo hiểm y tế cá nhân 2：thị trấn nhỏ 3：công nhân theo giờ   A:hỗ trợ khó khăn y tế dân chính   B：di chúc người già C:người già được bảo trợ năm mặt D：người tàn tật nặng   E:học sinh tiểu học, trung học và trẻ sơ sinh  F： cư dân khác   G：đối tượng hỗ trợ khó khăn tương trợ y tế   
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
        /// khóa chính liên kếtbrxzbh
        /// </summary>
        public int? ParentId { get; set; }
        public string insutype { get; set; }
    }
}
