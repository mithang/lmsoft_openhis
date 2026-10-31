using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.ValueObjects.MRHomePage
{
	public class BasyRelVO
	{
		/// <summary>
		/// Khung nhìn tổng hợp trang đầu bệnh án-Mã liên quan
		/// </summary>
		public string R_YLFKFS { get; set; }
		/// <summary>
		/// Số hồ sơ bệnh án
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
		/// Chuyên khoa nhập viện
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
		/// Mã:Mã khoa
		/// </summary>
		/// <returns></returns>
		public string R_CYKB { get; set; }

		/// <summary>
		/// Phòng bệnh ra viện
		/// </summary>
		/// <returns></returns>
		public string R_CYBF { get; set; }
		/// <summary>
		/// Mã bệnh
		/// </summary>
		/// <returns></returns>
		public string R_BLZDDM { get; set; }
		/// <summary>
		/// Mã:Mã nguồn bệnh nhân
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
		/// Mã:Rh
		/// </summary>
		/// <returns></returns>
		public string R_RH { get; set; }
		/// <summary>
		/// Chủ nhiệm khoa
		/// </summary>
		/// <returns></returns>
		public string R_KZR { get; set; }

		/// <summary>
		/// Chủ nhiệm(Phó chủ nhiệm)Bác sĩ
		/// </summary>
		/// <returns></returns>
		public string R_ZRYS { get; set; }

		/// <summary>
		/// Bác sĩ chẩn trị chính
		/// </summary>
		/// <returns></returns>
		public string R_ZZYS { get; set; }

		/// <summary>
		/// Bác sĩ điều trị chính
		/// </summary>
		public string R_ZZYS1 { get; set; }
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
		/// Bác sĩ đào tạo nâng cao
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
		/// Mã:Chất lượng hồ sơ bệnh án
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
		/// Có ra viện không31ngày tái nhập viện có phẫu thuật theo kế hoạch
		/// </summary>
		/// <returns></returns>
		public string R_SFZZYJH { get; set; }

		/// <summary>
		/// CSD_SN
		/// </summary>
		/// <returns></returns>
		public string R_CSD_SN { get; set; }

		/// <summary>
		/// CSD_SI
		/// </summary>
		/// <returns></returns>
		public string R_CSD_SI { get; set; }

		/// <summary>
		/// CSD_QX
		/// </summary>
		/// <returns></returns>
		public string R_CSD_QX { get; set; }

		/// <summary>
		/// CSD_JD
		/// </summary>
		/// <returns></returns>
		public string R_CSD_JD { get; set; }

		/// <summary>
		/// XZZ_SN
		/// </summary>
		/// <returns></returns>
		public string R_XZZ_SN { get; set; }

		/// <summary>
		/// XZZ_SI
		/// </summary>
		/// <returns></returns>
		public string R_XZZ_SI { get; set; }

		/// <summary>
		/// XZZ_QX
		/// </summary>
		/// <returns></returns>
		public string R_XZZ_QX { get; set; }

		/// <summary>
		/// XZZ_JD
		/// </summary>
		/// <returns></returns>
		public string R_XZZ_JD { get; set; }

		/// <summary>
		/// HKDZ_SN
		/// </summary>
		/// <returns></returns>
		public string R_HKDZ_SN { get; set; }

		/// <summary>
		/// HKDZ_SI
		/// </summary>
		/// <returns></returns>
		public string R_HKDZ_SI { get; set; }

		/// <summary>
		/// HKDZ_QX
		/// </summary>
		/// <returns></returns>
		public string R_HKDZ_QX { get; set; }

		/// <summary>
		/// HKDZ_JD
		/// </summary>
		/// <returns></returns>
		public string R_HKDZ_JD { get; set; }

		/// <summary>
		/// LXRDZ_SN
		/// </summary>
		/// <returns></returns>
		public string R_LXRDZ_SN { get; set; }

		/// <summary>
		/// LXRDZ_SI
		/// </summary>
		/// <returns></returns>
		public string R_LXRDZ_SI { get; set; }

		/// <summary>
		/// LXRDZ_QX
		/// </summary>
		/// <returns></returns>
		public string R_LXRDZ_QX { get; set; }

		/// <summary>
		/// LXRDZ_JD
		/// </summary>
		/// <returns></returns>
		public string R_LXRDZ_JD { get; set; }
	}
}
