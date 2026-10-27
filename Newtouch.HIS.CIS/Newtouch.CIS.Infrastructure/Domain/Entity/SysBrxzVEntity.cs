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
        /// Tổ chứcId（cócụ thểthểnghiệp vụcủayviện）
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
        /// 0：thông thường  1：nghỉ hưu  2：Thương tật   đâycủaCài đặtchỉhìnhhưởngđếnThêm mớibệnh nhânkhicủa“bệnh nhânTính chất”qua滤，tất cảgiao dịchChi phícủaphạm viXác nhậnSử dụng“giao dịchChi phíphạm vi”đếnđiều khiển      --nàychữđoạnchứanghĩatồn tạiởcủahỏiđề！！！！！！   khi“Thương tật”bệnh nhânkhi，khôngphương phápchọntự chi trảThuộc tính
        /// </summary>
        public string ybtsdy { get; set; }

        /// <summary>
        /// Bảo hiểm y tếgiao dịchcủaChi phíphạm vi   0 có thểghi帐bộphúttham sốvàTính toán（thông thườngBảo hiểm y tế）   1 có thểghi帐＋Tự trả theo loạivàodònggiao dịch（Thương tật）   2 có thểghi帐＋Tự trả theo loại＋tựlývàodònggiao dịch（chưa có）      ----cmsHủy bỏchữđoạn
        /// </summary>
        public string jyfyfw { get; set; }

        /// <summary>
        /// 0 khôngSử dụngChứng từ   1 Sử dụngChứng từ
        /// </summary>
        public string pzbz { get; set; }

        /// <summary>
        /// 0 Chung 1 chỉNgoại trú 2 chỉNhập viện    3 Hệ thống（Ngoại trúNhập việnđềukhôngcó thểdùng）VDnếu：nhàgiường
        /// </summary>
        public string mzzybz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? bzs { get; set; }

        /// <summary>
        /// 0 khônghưởng giảm miễn（khámtrịphí）   1 hưởng giảm miễn
        /// </summary>
        public string sqjmbz { get; set; }

        /// <summary>
        /// from xt_zhxz   nếunếucóCài đặt：thìởquyết toánkhi，ưutrướckhấutrừCài đặtcủaTài khoản   （VDnếu：Hỗ trợ khó khănbệnh nhânnàychữđoạnCài đặtlà4，khiquyết toánkhi，trướckhấutrừHỗ trợ khó khănTài khoản）
        /// </summary>
        public string zhxz { get; set; }

        /// <summary>
        /// 0 Vô hiệu 1 Hiệu lực
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 0：thành phốbảo 1：cáithểBảo hiểm y tế 2：nhỏthành phốtrấn 3：giờcông   A:dânchínhy tếHỗ trợ khó khăn   B：giànămdi嘱 C:nămbảogià người D：trọng残 ngườiviên   E:trongnhỏhọcsinhvà婴幼儿  F： Kháccư dân   G：y tếlẫn nhaugiúpHỗ trợ khó khănđối tượng   
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
