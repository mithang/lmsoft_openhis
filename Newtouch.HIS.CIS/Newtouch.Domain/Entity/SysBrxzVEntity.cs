using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.Entity
{
    public class SysBrxzVEntity
    {
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
        /// 0 Loại mà mọi thẻ đều có thể chọn   1 Loại mà thẻ BHYT có thể chọn
        /// </summary>
        public string brxzlb { get; set; }

        /// <summary>
        /// 0 Không giao dịch 1 Giao dịch thường 2 Giao dịch bệnh nặng 3 Giao dịch giường gia đình
        /// </summary>
        public string ybjylx { get; set; }

        /// <summary>
        /// 0:Thường  1:Nghỉ hưu  2:Thương tật   Cài đặt này chỉ ảnh hưởng khi thêm bệnh nhân mới"Tính chất bệnh nhân"Lọc,Việc xác định phạm vi mọi chi phí giao dịch sử dụng"Phạm vi chi phí giao dịch"để kiểm soát      --Vấn đề về ý nghĩa của trường này!!!!!!   Khi"Thương tật"khi là bệnh nhân,Không thể chọn thuộc tính tự chi trả
        /// </summary>
        public string ybtsdy { get; set; }

        /// <summary>
        /// Phạm vi chi phí giao dịch BHYT   0 Phần có thể ghi sổ tham gia tính toán(BHYT thường)   1 Có thể ghi sổ＋Giao dịch tự chi trả theo phân loại(Thương tật)   2 Có thể ghi sổ＋Tự chi trả theo phân loại＋Giao dịch tự thanh toán(Tạm thời không có)      ----cmsTrường hủy bỏ
        /// </summary>
        public string jyfyfw { get; set; }

        /// <summary>
        /// 0 Không dùng chứng từ   1 Dùng chứng từ
        /// </summary>
        public string pzbz { get; set; }

        /// <summary>
        /// 0 Chung 1 Chỉ ngoại trú 2 Chỉ nội trú    3 Hệ thống(Cả ngoại trú và nội trú đều không dùng được)Ví dụ:Giường gia đình
        /// </summary>
        public string mzzybz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? bzs { get; set; }

        /// <summary>
        /// 0 Không được giảm miễn(Phí khám chữa bệnh)   1 Được giảm miễn
        /// </summary>
        public string sqjmbz { get; set; }

        /// <summary>
        /// from xt_zhxz   Nếu có cài đặt:thì khi quyết toán,Ưu tiên trừ tài khoản đã cài đặt   (Ví dụ:Bệnh nhân được hỗ trợ thì trường này đặt là4,Khi quyết toán,Trừ tài khoản hỗ trợ trước)
        /// </summary>
        public string zhxz { get; set; }

        /// <summary>
        /// 0 Vô hiệu 1 Có hiệu lực
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 0:Bảo hiểm thành thị 1:BHYT cá thể 2:Thị trấn nhỏ 3:Công nhân theo giờ   A:Hỗ trợ y tế dân chính   B:Di chúc người cao tuổi C:Người cao tuổi được bảo trợ D:Người khuyết tật nặng   E:Học sinh tiểu học, trung học và trẻ sơ sinh  F: Cư dân khác   G:Đối tượng hỗ trợ y tế tương trợ   
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
