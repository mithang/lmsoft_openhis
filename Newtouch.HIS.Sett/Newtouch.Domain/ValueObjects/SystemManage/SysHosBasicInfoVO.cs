using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    public class SysHosBasicInfoVO
    {
        /// <summary>
        /// Loại thẻ Enum tương ứngEnumCardType
        /// </summary>
        public string cardtype { get; set; }
        public string CardTypeName { get; set; }
        /// <summary>
        /// Mã nội bộ bệnh nhân
        /// </summary>
        public int? patid { get; set; }
        /// <summary>
        /// Số thẻ
        /// </summary>
        public string kh { get; set; }

        /// <summary>
        /// Số nhập viện
        /// </summary>
        public string zyh { get; set; }

        /// <summary>
        /// Số nhập viện
        /// </summary>
        public string zyh2 { get; set; }

        /// <summary>
        /// Tính chất bệnh nhân
        /// </summary>
        public string brxz { get; set; }

        /// <summary>
        /// Bệnh nặng  add by caishanshan  Tạm thêm
        /// </summary>
        public string db { get; set; }

        /// <summary>
        /// Chẩn đoán bệnh nặng  add by caishanshan  Tạm thêm
        /// </summary>
        public string dbzd { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? brxzbh { get; set; }

        public string brxzmc { get; set; }

        public decimal wjje { get; set; }

        #region 病人基本信息
        /// <summary>
        /// Họ tên
        /// </summary>
        public string xm { get; set; }
        /// <summary>
        /// Năm tháng sinh
        /// </summary>
        public string csny { get; set; }
        /// <summary>
        /// CMND/CCCD
        /// </summary>
        public string zjh { get; set; }
        /// <summary>
        /// Giới tính
        /// </summary>
        public string xb { get; set; }
        /// <summary>
        /// Tuổi
        /// </summary>
        public short? nl { get; set; }

        public string zjlx { get; set; }
        /// <summary>
        /// Số bệnh án
        /// </summary>
        public string blh { get; set; }
        /// <summary>
        /// Điện thoại
        /// </summary>
        public string dh { get; set; }
        /// <summary>
        /// Khu vực
        /// </summary>
        public string dy { get; set; }

        public int? dybh { get; set; }

        /// <summary>
        /// Khu vực
        /// </summary>
        public string dymc { get; set; }
        /// <summary>
        /// Tình trạng hôn nhân
        /// </summary>
        public byte? hf { get; set; }

        public string wechat { get; set; }

        public string zjlxfs { get; set; }
        public string yddh { get; set; }
        public string email { get; set; }

        public string py { get; set; }

        public string xl { get; set; }

        public string gj2 { get; set; }

        public string mz2 { get; set; }

        public string jsr { get; set; }

        public string phone { get; set; }
        public string brly { get; set; }

        public string jjlldh { get; set; }
        public string jjllr { get; set; }
        public string jjllrgx { get; set; }
        /// <summary>
        /// Mã bưu điện
        /// </summary>
        public string cs_yb { get; set; }
        public string hu_yb { get; set; }
        public string xian_yb { get; set; }
        public string dwyb { get; set; }
        /// <summary>
        /// Tiền sử bệnh
        /// </summary>
        public string jbs { get; set; }
        /// <summary>
        /// Quê quán
        /// </summary>
        public string jg { get; set; }

        #endregion

        #region 住院基本信息
        /// <summary>
        /// Huấn luyện viên sức khỏe
        /// </summary>
        public string jkjl { get; set; }

        /// <summary>
        /// Tên huấn luyện viên sức khỏe
        /// </summary>
        public string jkjlmc { get; set; }

        /// <summary>
        /// Bác sĩ
        /// </summary>
        public string doctor { get; set; }

        /// <summary>
        /// Tên bác sĩ
        /// </summary>
        public string doctormc { get; set; }

        /// <summary>
        /// Giường bệnh
        /// </summary>
        public string cw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ks { get; set; }

        /// <summary>
        /// Tên khoa
        /// </summary>
        public string ksmc { get; set; }

        /// <summary>
        /// Vì khi nhập viện chưa xác định giường bệnh,Vì vậy lưu khu bệnh trước,Để y tá khu này có thể thao tác bệnh nhân này
        /// </summary>
        public string bq { get; set; }

        public string bqmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ryrq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cyrq { get; set; }

        /// <summary>
        /// Đường nhập viện EnumEnumRYTJ
        /// </summary>
        public string rytj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zy { get; set; }

        public string mzmc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? mz { get; set; }

        public string mzCode { get; set; }

        public string gjCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? gj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string gjmc { get; set; }

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
        /// 
        /// </summary>
        public string hu_sheng { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string hu_shi { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string hu_xian { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string hu_dz { get; set; }

        /// <summary>
        /// 
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
        public string jjlxr_sheng { get; set; }
        public string jjlxr_shi { get; set; }
        public string jjlxr_xian { get; set; }
        public string jjlxr_dz { get; set; }

        /// <summary>
        /// EnumEnumHF
        /// </summary>
        public int? hy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? bje { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string lxr { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string lxrgx { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string lxrdh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string lxrdz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string lxrjtdh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string lxrWebchat { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string lxrEmail { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string lxr2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string lxrgx2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string lxryddh2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string lxrjtdh2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string lxrWebchat2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string lxrEmail2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string lxrdz2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string gms { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ys { get; set; }

        public string zdmc { get; set; }
        public string zdmc1 { get; set; }
        public string zdmc2 { get; set; }
        public string zdmc3 { get; set; }

        public string ryzd1 { get; set; }
        public string ryzd2 { get; set; }
        public string ryzd3 { get; set; }

        public string rybq { get; set; }

        public decimal zhye { get; set; }

        public string zybz { get; set; }

        public string dwmc { get; set; }
        public string dwdz { get; set; }
        public string dwdh { get; set; }
        public string bz { get; set; }
        #endregion

        /// <summary>
        /// Số BHXH
        /// </summary>
        public string sbbh { get; set; }

        /// <summary>
        /// Mã nơi tham gia bảo hiểm
        /// </summary>
        public string cbdbm { get; set; }

        public string zxsbbf { get; set; }
        /// <summary>
        /// Mã cá nhân Bảo hiểm nông thôn mới
        /// </summary>
        public string xnhgrbm { get; set; }

        public string xnhylzh { get; set; }
        /// <summary>
        /// Số thứ tự bồi thường nội trú Bảo hiểm nông thôn mới trả về
        /// </summary>
        public string inpId { get; set; }
		/// <summary>
		/// Loại tham gia bảo hiểm
		/// </summary>
	    public string cblb { get; set; }
		/// <summary>
		/// Bệnh viện chuyển đi
		/// </summary>
		public string zcyy { get; set; }
        public string dwbh { get; set; }
        public string grbh { get; set; }
        /// <summary>
        /// Loại hình bảo hiểm
        /// </summary>
        public string xzlx { get; set; }
        public string jzlx { get; set; }
        public string bzbm { get; set; }
        public string bzmc { get; set; }
        public string ssczdm { get; set; }
        public string ssczmc { get; set; }
        public string syfwzh { get; set; }
        public string sylb { get; set; }
        public string sysslb { get; set; }
        public string wybz { get; set; }
        public string yzs { get; set; }
        public string tc { get; set; }
        public string tes { get; set; }
        public string zcbz { get; set; }
        public DateTime? syrq { get; set; }
        /// <summary>
        /// yktmodify Sửa thông tin thẻ liên thông/ yktregister Tạo mới thông tin thẻ liên thông
        /// /yktcardregister Tạo mới thông tin thẻ
        /// </summary>
        public string yktbz { get; set; }
        /// <summary>
        /// Có phải là thẻ sức khỏe bên thứ ba hay không Y:là N:không
        /// </summary>
        public string isdsfly { get; set; }
        /// <summary>
        /// Cờ tài khoản
        /// </summary>
        public string accountattr { get; set; }

    }
}
