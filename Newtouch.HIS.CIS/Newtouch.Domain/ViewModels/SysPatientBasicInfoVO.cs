using System;

namespace Newtouch.Domain.ViewModels
{
    /// <summary>
    /// Thông tin cơ bản hệ thống và cá nhân
    /// </summary>
    public class SysPatientBasicInfoVO
    {
        /// <summary>
        /// Bệnh nhânID
        /// </summary>
        public int patid { get; set; }

        /// <summary>
        /// Tổ chứcId(Bệnh viện có nghiệp vụ cụ thể)
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Tính chất gần nhất của bệnh nhânxt_brxz.brxz
        /// </summary>
        public string brxz { get; set; }

        /// <summary>
        /// Tên tính chất bệnh nhân
        /// </summary>
        public string brxzmc { get; set; }

        /// <summary>
        /// Số thẻ từ của bệnh viện này(Bao gồm bệnh nhân BHYT nghỉ hưu),hoặc số thẻ BHYT
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
        /// Mã quận/huyện
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
        /// --2006-07-9 Hủy???Quy tắc tài khoản,Quyết toán giường gia đình,Khi truyền biến lấy số âm,Không sửa bảng      --2006.07.09 Không cho phép tự động hủy tài khoản,Để không nhầm lẫn với thất bại rút giường gia đình   Tài khoản âm chỉ có một ý nghĩa,Chính là thất bại rút giường      Tài khoản đặc ước đơn vị,Tài khoản đặc ước cá nhân from xtzh   Khi tài khoản là số âm,Biểu thị tài khoản này đã bị hệ thống tự động hủy(Đã quá hạn sử dụng,hoặc giao dịch giường gia đình thành công,Nhưng rút giường thất bại.)
        /// </summary>
        public int? zh { get; set; }

        /// <summary>
        /// 0 Vô hiệu 1 Có hiệu lực
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
        /// Bit cờ thẻ BHYT trả về,Thẻ không phải BHYT là ký tự trống
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
        /// Mã nơi tham gia bảo hiểm(181019Mã nơi tham gia BHYT)
        /// </summary>
        public string cbdbm { get; set; }

        /// <summary>
        /// Thực hiện biện pháp bảo hiểm xã hội
        /// </summary>
        public string zxsbbf { get; set; }

        /// <summary>
        /// Mã cá nhân hợp tác xã y tế nông thôn mới
        /// </summary>
        public string xnhgrbm { get; set; }

        /// <summary>
        /// Số chứng nhận y tế hợp tác xã nông thôn mới
        /// </summary>
        public string xnhylzh { get; set; }

        /// <summary>
        /// Số thẻ
        /// </summary>
        public string CardNo { get; set; }

        /// <summary>
        /// Loại thẻ
        /// </summary>
        public string CardType { get; set; }

        /// <summary>
        /// Tên loại thẻ
        /// </summary>
        public string CardTypeName { get; set; }
    }

    /// <summary>
    /// Thông tin thẻ bệnh nhân hệ thống
    /// </summary>
    public class SysPatientCardDetail
    {
        /// <summary>
        /// ThẻID  Khóa chính bảng số thẻ hệ thống
        /// </summary>
        public string CardId { get; set; }

        /// <summary>
        /// Tổ chứcID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Số thẻ
        /// </summary>
        public string CardNo { get; set; }

        /// <summary>
        /// Loại thẻ 1-Thẻ ảo  2-Thẻ giao dịch BHYT  3-Thẻ giao dịch hợp tác xã nông thôn mới
        /// </summary>
        public string CardType { get; set; }

        /// <summary>
        /// Tên loại thẻ
        /// </summary>
        public string CardTypeName { get; set; }

        /// <summary>
        /// Mã nội bộ bệnh nhân
        /// </summary>
        public int patid { get; set; }

        /// <summary>
        /// Họ tên bệnh nhân
        /// </summary>
        public string hzxm { get; set; }

        /// <summary>
        /// Số BHYT cá nhân
        /// </summary>
        public string grybh { get; set; }

        /// <summary>
        /// Số BHYT đơn vị
        /// </summary>
        public string dwybh { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// Trạng thái  1:Có hiệu lực  0:Vô hiệu
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Sắp xếp
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Người sửa đổi
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// Thời gian sửa đổi
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
    }
}