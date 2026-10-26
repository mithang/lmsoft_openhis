using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.MR.ManageSystem.Domain.Entity
{
    /// <summary>
    /// Người tạo：chl
    /// Ngày：2019-11-20 11:05
    /// Mô tả: Trang đầu bệnh ánHạng mục liên kết từ điển thông tin cơ bản
    [Table("mr_basy_rel_code")]
    public class MrbasyrelcodeEntity:IEntity<MrbasyrelcodeEntity>
    {
        [Key]
        public string Id { get; set; }
        public string OrganizeId { get; set; }
        public string SYId { get; set; }
        /// <summary>
        /// Hình thức thanh toán y tế
        /// </summary>
        /// <returns></returns>
        public string YLFKFS { get; set; }
        /// <summary>
        /// Số bệnh án
        /// </summary>
        /// <returns></returns>
        public string BAH { get; set; }
        /// <summary>
        /// Giới tính
        /// </summary>
        /// <returns></returns>
        public string XB { get; set; }
        /// <summary>
        /// Quốc tịch
        /// </summary>
        /// <returns></returns>
        public string GJ { get; set; }
        /// <summary>
        /// Dân tộc
        /// </summary>
        /// <returns></returns>
        public string MZ { get; set; }
        /// <summary>
        /// Nghề nghiệp
        /// </summary>
        /// <returns></returns>
        public string ZY { get; set; }
        /// <summary>
        /// Hôn nhân
        /// </summary>
        /// <returns></returns>
        public string HY { get; set; }
        /// <summary>
        /// Quan hệ
        /// </summary>
        /// <returns></returns>
        public string GX { get; set; }
        /// <summary>
        /// Đường nhập viện
        /// </summary>
        /// <returns></returns>
        public string RYTJ { get; set; }
        /// <summary>
        /// Khoa nhập viện
        /// </summary>
        /// <returns></returns>
        public string RYKB { get; set; }
        /// <summary>
        /// Phòng bệnh nhập viện
        /// </summary>
        /// <returns></returns>
        public string RYBF { get; set; }

        /// <summary>
        /// Khoa chuyển đến
        /// </summary>
        /// <returns></returns>
        public string ZKKB { get; set; }
        /// <summary>
        /// Mã: Mã khoa
        /// </summary>
        /// <returns></returns>
        public string CYKB { get; set; }

        /// <summary>
        /// Phòng bệnh xuất viện
        /// </summary>
        /// <returns></returns>
        public string CYBF { get; set; }
        /// <summary>
        /// Mã bệnh
        /// </summary>
        /// <returns></returns>
        public string BLZDDM { get; set; }
        /// <summary>
        /// Mã: Mã nguồn bệnh nhân
        /// </summary>
        /// <returns></returns>
        public string BRLY { get; set; }
        /// <summary>
        /// Dị ứng thuốc
        /// </summary>
        /// <returns></returns>
        public string YWGM { get; set; }
        /// <summary>
        /// Nhóm máu
        /// </summary>
        /// <returns></returns>
        public string XX { get; set; }

        /// <summary>
        /// Mã：Rh
        /// </summary>
        /// <returns></returns>
        public string RH { get; set; }        
        /// <summary>
        /// Trưởng khoa
        /// </summary>
        /// <returns></returns>
        public string KZR { get; set; }

        /// <summary>
        /// Bác sĩ trưởng khoa (phó trưởng khoa)
        /// </summary>
        /// <returns></returns>
        public string ZRYS { get; set; }

        /// <summary>
        /// Bác sĩ điều trị
        /// </summary>
        /// <returns></returns>
        public string ZZYS { get; set; }

        /// <summary>
        /// Bác sĩ nội trú
        /// </summary>
        /// <returns></returns>
        public string ZYYS { get; set; }

        /// <summary>
        /// Điều dưỡng phụ trách
        /// </summary>
        /// <returns></returns>
        public string ZRHS { get; set; }

        /// <summary>
        /// Bác sĩ tu nghiệp
        /// </summary>
        /// <returns></returns>
        public string JXYS { get; set; }

        /// <summary>
        /// Bác sĩ thực tập
        /// </summary>
        /// <returns></returns>
        public string SXYS { get; set; }

        /// <summary>
        /// Nhân viên mã hóa
        /// </summary>
        /// <returns></returns>
        public string BMY { get; set; }

        /// <summary>
        /// Mã: Chất lượng bệnh án
        /// </summary>
        /// <returns></returns>
        public string BAZL { get; set; }

        /// <summary>
        /// Bác sĩ kiểm soát chất lượng
        /// </summary>
        /// <returns></returns>
        public string ZKYS { get; set; }

        /// <summary>
        /// Điều dưỡng kiểm soát chất lượng
        /// </summary>
        /// <returns></returns>
        public string ZKHS { get; set; }
        /// <summary>
        /// Hình thức rời viện
        /// </summary>
        /// <returns></returns>
        public string LYFS { get; set; }
        /// <summary>
        /// Phân loại tình trạng bệnh: 1 Nguy kịch, 2 Nặng, 3 Khó, 4 Cấp cứu, 5 Thông thường
        /// </summary>
        public string BQFX { get; set; }
        /// <summary>
        /// Có kế hoạch tái nhập viện và phẫu thuật trong 31 ngày sau xuất viện hay không
        /// </summary>
        /// <returns></returns>
        public string SFZZYJH { get; set; }
    }
}
