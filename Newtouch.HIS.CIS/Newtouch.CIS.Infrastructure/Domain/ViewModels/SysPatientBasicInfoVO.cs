using System;

namespace Newtouch.Domain.ViewModels
{
    /// <summary>
    /// Hệ thốngvàCá nhânThông tin cơ bản
    /// </summary>
    public class SysPatientBasicInfoVO
    {
        /// <summary>
        /// Bệnh nhânID
        /// </summary>
        public int patid { get; set; }

        /// <summary>
        /// Tổ chứcId（cócụ thểthểnghiệp vụcủayviện）
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// bệnh nhânnhấtgầncủaTính chấtxt_brxz.brxz
        /// </summary>
        public string brxz { get; set; }

        /// <summary>
        /// bệnh nhânTên tính chất
        /// </summary>
        public string brxzmc { get; set; }

        /// <summary>
        /// Số thẻ từ của bệnh viện (gồm bệnh nhân BHYT nghỉ hưu), hoặc số thẻ BHYT
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
        /// Đơn vịMã
        /// </summary>
        public string dwdm { get; set; }

        /// <summary>
        /// khuhuyệnMã
        /// </summary>
        public string qxdm { get; set; }

        /// <summary>
        /// Tên đơn vị
        /// </summary>
        public string dwmc { get; set; }

        /// <summary>
        /// nghỉ hưuChứng từ
        /// </summary>
        public string pzh { get; set; }

        /// <summary>
        /// y tếMục
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
        /// Chứng từChẩn đoán
        /// </summary>
        public string pzzd { get; set; }

        /// <summary>
        /// -- 2006-07-09: Khi hủy tài khoản, giường bệnh quyết toán, khi đổi lượng truyền lấy số âm, không sửa bảng. -- 2006.07.09: Không cho phép tự động ghi chú bán tài khoản; trường hợp đã không (rút giường) và rút giường thất bại lẫn lộn. Tài khoản âm chỉ có một ý nghĩa, đó là rút giường thất bại. Số tài khoản đặt hẹn của đơn vị, số tài khoản đặt hẹn cá nhân lấy từ xtzh. Khi tài khoản là số âm, biểu thị tài khoản này đã bị hệ thống tự động ghi chú bán (hạn hiệu lực đã qua, hoặc giao dịch giường bệnh thành công nhưng rút giường thất bại.)
        /// </summary>
        public int? zh { get; set; }

        /// <summary>
        /// 0 Vô hiệu 1 Hiệu lực
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
        /// Liệt kêEnumHF
        /// </summary>
        public byte? hf { get; set; }

        /// <summary>
        /// dântộc
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
        /// 0: Khác 1: Nhà ở 2: Công ty
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
        /// Bảo hiểm y tếthẻQuay lạicủaCờ ký tự，khôngBảo hiểm y tếthẻlàtrốngchữký
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
        /// Cá nhânxãbảoSố hiệu
        /// </summary>
        public string sbbh { get; set; }

        /// <summary>
        /// tham sốbảođịaMã（181019Bảo hiểm y tếtham sốbảođịaMã）
        /// </summary>
        public string cbdbm { get; set; }

        /// <summary>
        /// Thực hiệnxãsẽbảonguy hiểmlàmphương pháp
        /// </summary>
        public string zxsbbf { get; set; }

        /// <summary>
        /// BHYT nông thôn mớiCá nhânMã
        /// </summary>
        public string xnhgrbm { get; set; }

        /// <summary>
        /// BHYT nông thôn mớiSố chứng nhận y tế
        /// </summary>
        public string xnhylzh { get; set; }

        /// <summary>
        /// Số thẻ
        /// </summary>
        public string CardNo { get; set; }

        /// <summary>
        /// thẻLoại
        /// </summary>
        public string CardType { get; set; }

        /// <summary>
        /// thẻTên loại
        /// </summary>
        public string CardTypeName { get; set; }
    }

    /// <summary>
    /// Hệ thốngbệnh nhânthẻthông tin
    /// </summary>
    public class SysPatientCardDetail
    {
        /// <summary>
        /// thẻID  Hệ thốngSố thẻbảngKhóa chính
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
        /// Loại thẻ: 1 - thẻ ảo 2 - thẻ giao dịch BHYT 3 - thẻ giao dịch BHYT nông thôn mới
        /// </summary>
        public string CardType { get; set; }

        /// <summary>
        /// thẻTên loại
        /// </summary>
        public string CardTypeName { get; set; }

        /// <summary>
        /// bệnh nhântrongmã
        /// </summary>
        public int patid { get; set; }

        /// <summary>
        /// bệnh nhânHọ tên
        /// </summary>
        public string hzxm { get; set; }

        /// <summary>
        /// Cá nhânBảo hiểm y tếsố
        /// </summary>
        public string grybh { get; set; }

        /// <summary>
        /// Đơn vịBảo hiểm y tếsố
        /// </summary>
        public string dwybh { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// Trạng thái  1：Hiệu lực  0：Vô hiệu
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Thứ tự
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
        /// Sửa người
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// Thời gian sửa
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
    }
}
