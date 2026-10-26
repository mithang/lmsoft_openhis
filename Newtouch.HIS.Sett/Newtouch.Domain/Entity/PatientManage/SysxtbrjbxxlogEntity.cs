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
    [Table("xt_brjbxxLOG")]
    public class SysxtbrjbxxlogEntity : IEntity<SysxtbrjbxxlogEntity>
    {

        [Key]
        public string Id { get; set; }

        public int patid { get; set; }

        /// <summary>
        /// Tổ chứcId(Bệnh viện có nghiệp vụ cụ thể)
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Số thẻ từ của bệnh viện(Bao gồm bệnh nhân BHYT nghỉ hưu),Hoặc số thẻ BHYT
        /// </summary>
        public string blh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string xb { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? csny { get; set; }

        /// <summary>
        /// Loại giấy tờ
        /// </summary>
        public string zjlx { get; set; }
        /// <summary>
        /// Số giấy tờ
        /// </summary>
        public string zjh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cs_sheng { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cs_shi { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cs_xian { get; set; }
        public string cs_dz { get; set; }
        /// <summary>
        /// Mã bưu điện
        /// </summary>
        public string cs_yb { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string hu_sheng { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string hu_shi { get; set; }

        /// <summary>
        /// Huyện hộ khẩu
        /// </summary>
        public string hu_xian { get; set; }

        /// <summary>
        /// Địa chỉ hộ khẩu
        /// </summary>
        public string hu_dz { get; set; }
        public string hu_yb { get; set; }

        /// <summary>
        /// Tỉnh nơi ở hiện tại
        /// </summary>
        public string xian_sheng { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string xian_shi { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string xian_xian { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string xian_dz { get; set; }
        public string xian_yb { get; set; }
        /// <summary>
        /// Tỉnh người liên hệ khẩn cấp
        /// </summary>
        public string jjlxr_sheng { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string jjlxr_shi { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string jjlxr_xian { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string jjlxr_dz { get; set; }

        /// <summary>
        /// Quan hệ người liên hệ khẩn cấp
        /// </summary>
        public string jjllrgx { get; set; }
        /// <summary>
        /// Người liên hệ khẩn cấp
        /// </summary>
        public string jjllr { get; set; }
        /// <summary>
        /// Điện thoại liên hệ khẩn cấp
        /// </summary>
        public string jjlldh { get; set; }
        /// <summary>
        /// Có phải khác địa phương hay không
        /// </summary>
        public int? dybh { get; set; }
        /// <summary>
        /// Tên đơn vị
        /// </summary>
        public string dwmc { get; set; }

        /// <summary>
        /// Địa chỉ đơn vị
        /// </summary>
        public string dwdz { get; set; }
      

        /// <summary>
        /// 
        /// </summary>
        public string xl { get; set; }

        /// <summary>
        /// xt_gj.gj
        /// </summary>
        public string gj { get; set; }

        /// <summary>
        /// EnumEnumHF
        /// </summary>
        public byte? hf { get; set; }

        /// <summary>
        /// Dân tộc
        /// </summary>
        public string mz { get; set; }
        

        /// <summary>
        /// Nghề nghiệp
        /// </summary>
        public string zy { get; set; }

        /// <summary>
        /// Phương thức liên hệ tốt nhất
        /// </summary>
        public string zjlxfs { get; set; }
        /// <summary>
        /// Điện thoại
        /// </summary>
        public string dh { get; set; }

        /// <summary>
        /// Điện thoại di động
        /// </summary>
        public string phone { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string wechat { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// Nguồn bệnh nhân
        /// </summary>
        public string brly { get; set; }
        /// <summary>
        /// Tiền sử dị ứng
        /// </summary>
        public string gms { get; set; }
        /// <summary>
        /// Tiền sử bệnh
        /// </summary>
        public string jbs { get; set; }
        /// <summary>
        /// Ghi chú
        /// </summary>
        public string bz { get; set; }

        public string xt_CreatorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? xt_CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? xt_LastModifyTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string xt_LastModifierCode { get; set; }

        public string px { get; set; }
        /// <summary>
        /// 0 Không hiệu lực 1 Hiệu lực
        /// </summary>
        public string zt { get; set; }


        public string dwdh { get; set; }
        public string dwyb { get; set; }
        public string jg { get; set; }
        
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
        public string data_name { get; set; }
        public string datavalue_old { get; set; }
        public string datavalue_new { get; set; }

    }
}
