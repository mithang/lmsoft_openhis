using System;
using Newtouch.HIS.API.Common;

namespace Newtouch.PDS.Requset
{
    /// <summary>
    /// Bổ sung thông tin cơ bản bệnh nhân
    /// </summary>
    public class SupplementPatientBaseInfoRequest : RequestBase
    {
        /// <summary>
        /// Số đơn thuốc
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// Bệnh nhânID
        /// </summary>
        public int patid { get; set; }

        /// <summary>
        /// Tổ chứcId（Bệnh viện có nghiệp vụ cụ thể）
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Tính chất gần nhất của bệnh nhânxt_brxz.brxz
        /// </summary>
        public string brxz { get; set; }

        /// <summary>
        /// Số thẻ từ của bệnh viện này (bao gồm bệnh nhân bảo hiểm y tế nghỉ hưu), hoặc số thẻ bảo hiểm y tế
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
        /// 
        /// </summary>
        public string zjh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dh { get; set; }

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

        /// <summary>
        /// 
        /// </summary>
        public string qx { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string yb { get; set; }

        /// <summary>
        /// from xtdy
        /// </summary>
        public int? dybh { get; set; }

        /// <summary>
        /// Mã đơn vị
        /// </summary>
        public string dwdm { get; set; }

        /// <summary>
        /// Mã quận huyện
        /// </summary>
        public string qxdm { get; set; }

        /// <summary>
        /// Tên đơn vị
        /// </summary>
        public string dwmc { get; set; }

        /// <summary>
        /// Chứng từ nghỉ hưu
        /// </summary>
        public string pzh { get; set; }

        /// <summary>
        /// Hạng mục y tế
        /// </summary>
        public string ylxm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? pzksrq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? pzzzrq { get; set; }

        /// <summary>
        /// Chẩn đoán chứng từ
        /// </summary>
        public string pzzd { get; set; }

        /// <summary>
        /// --2006-07-9 Hủy???Quy tắc tài khoản, thanh toán giường tại nhà, khi truyền biến lấy số âm, không sửa bảng      --2006.07.09 Không cho phép tự động hủy tài khoản, để tránh nhầm lẫn với trường hợp hủy giường tại nhà thất bại. Tài khoản âm chỉ có một ý nghĩa, đó là hủy giường tại nhà thất bại. Tài khoản đặc ước của đơn vị, tài khoản đặc ước cá nhân from xtzh   Khi tài khoản là số âm, nghĩa là tài khoản này đã bị hệ thống tự động hủy(Đã quá hạn dùng, hoặc giao dịch giường tại nhà thành công nhưng hủy giường thất bại。)
        /// </summary>
        public int? zh { get; set; }

        /// <summary>
        /// 0 Không hợp lệ 1 Hợp lệ
        /// </summary>
        public string zt { get; set; }

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
        /// 
        /// </summary>
        public string ywxm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string yddh { get; set; }

        /// <summary>
        /// 0.Khác 1.Nhà ở 2.Công ty
        /// </summary>
        public byte? dzxz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string gzdw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string gzdz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string jjllr { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string jjlldh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string gzdwdh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string bz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public short? bxgs { get; set; }

        /// <summary>
        /// Cờ trả về từ thẻ bảo hiểm y tế, thẻ không phải bảo hiểm y tế là ký tự rỗng
        /// </summary>
        public string zhbz { get; set; }

        /// <summary>
        /// 
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
        /// 
        /// </summary>
        public string zjlxfs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zjlx { get; set; }

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

        public string Jsr { get; set; }
        public string brly { get; set; }

        public string jjllrgx { get; set; }

        /// <summary>
        /// Số bảo hiểm xã hội cá nhân
        /// </summary>
        public string sbbh { get; set; }

        /// <summary>
        /// Mã nơi tham gia bảo hiểm（181019Mã nơi tham gia bảo hiểm y tế）
        /// </summary>
        public string cbdbm { get; set; }

        /// <summary>
        /// Thực hiện biện pháp bảo hiểm xã hội
        /// </summary>
        public string zxsbbf { get; set; }
        /// <summary>
        /// Mã cá nhân nông thôn mới hợp tác
        /// </summary>
        public string xnhgrbm { get; set; }
        /// <summary>
        /// Số chứng nhận y tế nông thôn mới hợp tác
        /// </summary>
        public string xnhylzh { get; set; }
    }
}