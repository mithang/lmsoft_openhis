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
        /// Tổ chứcId(cócụ thểthểnghiệp vụcủayviện)
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
        /// 0 tất cảthẻđềucó thểchọncủaLoại   1 Bảo hiểm y tếthẻcó thểchọncủaLoại
        /// </summary>
        public string brxzlb { get; set; }

        /// <summary>
        /// 0 khônggiao dịch 1 thông thườnggiao dịch 2 lớnbệnhgiao dịch 3 nhàgiườnggiao dịch
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
        /// 0 khôngSử dụngChứng từ   1 Sử dụngChứng từ
        /// </summary>
        public string pzbz { get; set; }

        /// <summary>
        /// 0 Chung 1 chỉNgoại trú 2 chỉNhập viện    3 Hệ thống(Ngoại trúNhập việnđềukhôngcó thểdùng)VDnếu:nhàgiường
        /// </summary>
        public string mzzybz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? bzs { get; set; }

        /// <summary>
        /// 0 khônghưởng giảm miễn(khámtrịphí)   1 hưởng giảm miễn
        /// </summary>
        public string sqjmbz { get; set; }

        /// <summary>
        /// from xt_zhxz   nếunếucóCài đặt:thìởquyết toánkhi,ưutrướckhấutrừCài đặtcủaTài khoản   (VDnếu:Hỗ trợ khó khănbệnh nhânnàychữđoạnCài đặtlà4,khiquyết toánkhi,trướckhấutrừHỗ trợ khó khănTài khoản)
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
        /// liên kếtKhóa chínhbrxzbh
        /// </summary>
        public int? ParentId { get; set; }
        public string insutype { get; set; }
    }
}
