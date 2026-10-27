using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SqlSugar;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Utilities;
using NewtouchHIS.Lib.Framework.Filter;
using NewtouchHIS.Lib.Framework.Operator;

namespace Newtouch.MR.ManageSystem.Domain.ValueObjects
{
    public class BasyRelVO
    {
        /// <summary>
        /// Khung nhìn tổng hợp trang đầu bệnh án-Mã liên kết
        /// </summary>
        public string R_YLFKFS { get; set; }
        /// <summary>
        /// Số bệnh án
        /// </summary>
        /// <returns></returns>
        public string R_BAH { get; set; }
        /// <summary>
        /// Giới tính
        /// </summary>
        /// <returns></returns>
        public string R_XB { get; set; }
        /// <summary>
        /// Quốc tịch
        /// </summary>
        /// <returns></returns>
        public string R_GJ { get; set; }
        /// <summary>
        /// Dân tộc
        /// </summary>
        /// <returns></returns>
        public string R_MZ { get; set; }
        /// <summary>
        /// Nghề nghiệp
        /// </summary>
        /// <returns></returns>
        public string R_ZY { get; set; }
        /// <summary>
        /// Hôn nhân
        /// </summary>
        /// <returns></returns>
        public string R_HY { get; set; }
        /// <summary>
        /// Quan hệ
        /// </summary>
        /// <returns></returns>
        public string R_GX { get; set; }
        /// <summary>
        /// Đường nhập viện
        /// </summary>
        /// <returns></returns>
        public string R_RYTJ { get; set; }
        /// <summary>
        /// Khoa nhập viện
        /// </summary>
        /// <returns></returns>
        public string R_RYKB { get; set; }
        /// <summary>
        /// Phòng bệnh nhập viện
        /// </summary>
        /// <returns></returns>
        public string R_RYBF { get; set; }

        /// <summary>
        /// Khoa chuyển
        /// </summary>
        /// <returns></returns>
        public string R_ZKKB { get; set; }
        /// <summary>
        /// Mã: Mã khoa
        /// </summary>
        /// <returns></returns>
        public string R_CYKB { get; set; }

        /// <summary>
        /// Phòng bệnh xuất viện
        /// </summary>
        /// <returns></returns>
        public string R_CYBF { get; set; }
        /// <summary>
        /// Mã bệnh
        /// </summary>
        /// <returns></returns>
        public string R_BLZDDM { get; set; }
        /// <summary>
        /// Mã: Mã nguồn bệnh nhân
        /// </summary>
        /// <returns></returns>
        public string R_BRLY { get; set; }
        /// <summary>
        /// Dị ứng thuốc
        /// </summary>
        /// <returns></returns>
        public string R_YWGM { get; set; }
        /// <summary>
        /// Nhóm máu
        /// </summary>
        /// <returns></returns>
        public string R_XX { get; set; }

        /// <summary>
        /// Mã: Rh
        /// </summary>
        /// <returns></returns>
        public string R_RH { get; set; }
        /// <summary>
        /// Trưởng khoa
        /// </summary>
        /// <returns></returns>
        public string R_KZR { get; set; }

        /// <summary>
        /// Trưởng(Phó trưởng)Bác sĩ
        /// </summary>
        /// <returns></returns>
        public string R_ZRYS { get; set; }

        /// <summary>
        /// Bác sĩ điều trị chính
        /// </summary>
        /// <returns></returns>
        public string R_ZZYS { get; set; }

        /// <summary>
        /// Bác sĩ nội trú
        /// </summary>
        /// <returns></returns>
        public string R_ZYYS { get; set; }

        /// <summary>
        /// Điều dưỡng chịu trách nhiệm
        /// </summary>
        /// <returns></returns>
        public string R_ZRHS { get; set; }

        /// <summary>
        /// Bác sĩ tu nghiệp
        /// </summary>
        /// <returns></returns>
        public string R_JXYS { get; set; }

        /// <summary>
        /// Bác sĩ thực tập
        /// </summary>
        /// <returns></returns>
        public string R_SXYS { get; set; }

        /// <summary>
        /// Nhân viên mã hóa
        /// </summary>
        /// <returns></returns>
        public string R_BMY { get; set; }

        /// <summary>
        /// Mã: Chất lượng bệnh án
        /// </summary>
        /// <returns></returns>
        public string R_BAZL { get; set; }

        /// <summary>
        /// Bác sĩ kiểm soát chất lượng
        /// </summary>
        /// <returns></returns>
        public string R_ZKYS { get; set; }

        /// <summary>
        /// Điều dưỡng kiểm soát chất lượng
        /// </summary>
        /// <returns></returns>
        public string R_ZKHS { get; set; }
        /// <summary>
        /// Cách rời viện
        /// </summary>
        /// <returns></returns>
        public string R_LYFS { get; set; }
        /// <summary>
        /// Phân loại tình trạng bệnh 1 Nguy kịch 2 Bệnh nặng 3 Khó 4 Cấp cứu 5 Thông thường
        /// </summary>
        public string R_BQFX { get; set; }
        /// <summary>
        /// Có kế hoạch phẫu thuật tái nhập viện trong 31 ngày sau xuất viện không
        /// </summary>
        /// <returns></returns>
        public string R_SFZZYJH { get; set; }
    }
}
