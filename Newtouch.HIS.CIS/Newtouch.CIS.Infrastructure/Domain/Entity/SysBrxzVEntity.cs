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
        /// Tổ chức Id(có cụ thể thể nghiệp vụ của y viện)
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
        /// 0 tất cả thẻ đều có thể chọn của Loại   1 Bảo hiểm y tế thẻ có thể chọn của Loại
        /// </summary>
        public string brxzlb { get; set; }

        /// <summary>
        /// 0 không giao dịch 1 thông thường giao dịch 2 lớn bệnh giao dịch 3 nhà giường giao dịch
        /// </summary>
        public string ybjylx { get; set; }

        /// <summary>
        /// 0: thông thường 1: nghỉ hưu 2: thương tật. Cài đặt này chỉ ảnh hưởng đến bộ lọc “tính chất bệnh nhân” khi thêm mới bệnh nhân; phạm vi chi phí giao dịch được xác nhận bằng “phạm vi chi phí giao dịch”. -- Ý nghĩa của trường này còn tồn tại vấn đề! Khi bệnh nhân “thương tật”, không thể chọn thuộc tính tự chi trả
        /// </summary>
        public string ybtsdy { get; set; }

        /// <summary>
        /// Phạm vi chi phí giao dịch BHYT: 0: có thể ghi sổ theo tham số bộ phận và tính toán (BHYT thông thường) 1: có thể ghi sổ + tự trả theo loại đưa vào giao dịch (thương tật) 2: có thể ghi sổ + tự trả theo loại + tự chi trả đưa vào giao dịch (chưa có) ---- CMS đã hủy bỏ trường này
        /// </summary>
        public string jyfyfw { get; set; }

        /// <summary>
        /// 0 không Sử dụng Chứng từ   1 Sử dụng Chứng từ
        /// </summary>
        public string pzbz { get; set; }

        /// <summary>
        /// 0 Chung 1 chỉ Ngoại trú 2 chỉ Nhập viện    3 Hệ thống(Ngoại trú Nhập viện đều không có thể dùng)VD nếu:nhà giường
        /// </summary>
        public string mzzybz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? bzs { get; set; }

        /// <summary>
        /// 0 không hưởng giảm miễn(khám trị phí)   1 hưởng giảm miễn
        /// </summary>
        public string sqjmbz { get; set; }

        /// <summary>
        /// from xt_zhxz   nếu nếu có Cài đặt:thì ở quyết toán khi,ưu trước khấu trừ Cài đặt của Tài khoản   (VD nếu:Hỗ trợ khó khăn bệnh nhân này chữ đoạn Cài đặt là4,khi quyết toán khi,trước khấu trừ Hỗ trợ khó khăn Tài khoản)
        /// </summary>
        public string zhxz { get; set; }

        /// <summary>
        /// 0 Vô hiệu 1 Hiệu lực
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 0: bảo hiểm thành phố 1: BHYT cá thể 2: BHYT thị trấn nhỏ 3: công nhân làm theo giờ A: đối tượng hỗ trợ khó khăn y tế dân chính B: người già theo di chúc C: người già hưởng bảo đảm năm D: người khuyết tật nặng E: học sinh tiểu học và trẻ sơ sinh F: cư dân khác G: đối tượng hỗ trợ khó khăn tương trợ y tế
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
        /// liên kết Khóa chính brxzbh
        /// </summary>
        public int? ParentId { get; set; }
        public string insutype { get; set; }
    }
}
