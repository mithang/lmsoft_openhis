using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.DTO
{
    /// <summary>
    /// Liên quan thanh toán số tiền quyết toán ngoại trú
    /// </summary>
    public class OutpatientSettFeeRelatedDTO
    {
        /// <summary>
        /// Tổng số tiền quyết toán(dj*slTổng)
        /// </summary>
        public decimal? zje { get; set; }
        /// <summary>
        /// Khoản phải thu thanh toán ban đầu
        /// </summary>
        public decimal? orglxjzfys { get; set; }
        /// <summary>
        /// Tỷ lệ chiết khấu
        /// </summary>
        public decimal? zkbl { get; set; }
        /// <summary>
        /// Số tiền chiết khấu
        /// </summary>
        public decimal? zkje { get; set; }
        /// <summary>
        /// Khoản phải thu thanh toán(Khoản phải thu thanh toán ban đầu sau chiết khấu)
        /// </summary>
        public decimal? xjzfys { get; set; }
        /// <summary>
        /// Số tiền thực thu
        /// </summary>
        public decimal? ssk { get; set; }
        public string zffs1 { get; set; }
        public decimal? zfje1 { get; set; }
        public string zffs2 { get; set; }
        public decimal? zfje2 { get; set; }
        /// <summary>
        /// Tiền thối lại
        /// </summary>
        public decimal? zhaoling { get; set; }
        /// <summary>
        /// Số giao dịch khấu trừ,Khấu trừ tài khoản BHYT Trùng Khánh
        /// </summary>
        public string dyjylsh { get; set; }

        #region new
        /// <summary>
        /// Số tiền thanh toán tạm ứng
        /// </summary>
        public decimal? yjjzfje { get; set; }
        /// <summary>
        /// Số dư tạm ứng có thể trả lại
        /// </summary>
        public decimal? yjjtye { get; set; }
        /// <summary>
        /// Số tiền thực thu tại cửa sổ
        /// </summary>
        public decimal? djjess { get; set; }
        /// <summary>
        /// Phương thức thanh toán thực thu tại cửa sổ
        /// </summary>
        public string djjesszffs { get; set; }

        #endregion
        /// <summary>
        /// Số giao dịch quyết toán BHYT 
        /// </summary>
        public string ybjslsh { get; set; }
        /// <summary>
        /// Mã chứng từ điện tử
        /// </summary>
        public string ecToken { get; set; }

        public List<PatZfList> patZflist { get; set; }
        public decimal? xjwc { get; set; }
    }
    public class PatZfList
    {
        /// <summary>
        /// Số tiền thực thu tại cửa sổ
        /// </summary>
        public decimal? zfje { get; set; }
        /// <summary>
        /// Phương thức thanh toán thực thu tại cửa sổ
        /// </summary>
        public string zffsmc { get; set; }
    }

    /// <summary>
    /// Chi phí liên quan BHYT quyết toán ngoại trú
    /// </summary>
    public class OutpatientSettYbFeeRelatedDTO
    {
        /// <summary>
        /// Tổng chi phí
        /// </summary>
        public decimal? ZFY { get; set; }
        /// <summary>
        /// Thanh toán tiền mặt
        /// </summary>
        public decimal? XJZF { get; set; }
        /// <summary>
        /// Chi phí thông thường(Bảo hiểm cư dân:Chi phí BHYT)
        /// </summary>
        public decimal? YBFY { get; set; }
        /// <summary>
        /// Chi phí đặc biệt
        /// </summary>
        public decimal? TSFY { get; set; }
        /// <summary>
        /// Chi trả cơ bản(Bảo hiểm cư dân:Thanh toán từ tài khoản)
        /// </summary>
        public decimal? JBZF { get; set; }
        /// <summary>
        /// Thanh toán trợ cấp công
        /// </summary>
        public decimal? GBZF { get; set; }
        /// <summary>
        /// Lũy kế chi phí thông thường được thanh toán trong đoạn tự chi trả ngoại trú(Mức khởi điểm bảo hiểm cư dân)
        /// </summary>
        public decimal? SUMZFDYBFY { get; set; }
        /// <summary>
        /// Chi phí thông thường được thanh toán trong đoạn tự chi trả ngoại trú(Bảo hiểm cư dân:Đoạn tự chi trả ngoại trú lần này)
        /// </summary>
        public decimal? ZFDYBFY { get; set; }
        /// <summary>
        /// Số dư cơ bản
        /// </summary>
        public decimal? JBYE { get; set; }
        /// <summary>
        /// Số dư trợ cấp công
        /// </summary>
        public decimal? GBYE { get; set; }
        /// <summary>
        /// Có thể đến trung tâm hoàn trả chi phí thông thường
        /// </summary>
        public decimal? KBXYBFY { get; set; }
        /// <summary>
        /// Có thể đến trung tâm hoàn trả chi phí đặc biệt
        /// </summary>
        public decimal? KBXTSFY { get; set; }


        /// <summary>
        /// (Bảo hiểm cư dân)Chi trả từ quỹ chung
        /// </summary>
        public decimal? TCZF { get; set; }
        /// <summary>
        /// (Bảo hiểm cư dân)Cứu trợ(Ưu đãi)Thanh toán
        /// </summary>
        public decimal? JZZF { get; set; }
        /// <summary>
        /// (Bảo hiểm cư dân)Thanh toán bổ sung bệnh nặng
        /// </summary>
        public decimal? DKC023 { get; set; }

        /// <summary>
        /// Chi từ tài khoản BHYT
        /// </summary>
        public decimal? YBZHZC
        {
            get
            {
                if (JBZF.HasValue || GBZF.HasValue)
                {
                    return (JBZF ?? 0) + (GBZF ?? 0);
                }
                return null;
            }
        }

    }
    /// <summary>
    /// Hợp nhất trường BHYT Quý An và Thường Thục trả về
    /// </summary>
    public class OutpatientSettGAYbFeeRelatedDTO: OutpatientSettYbFeeRelatedDTO
    {

        /// <summary>
        /// Số giao dịch
        /// </summary>
        public string astr_jylsh { get; set; }
        /// <summary>
        /// Mã xác minh giao dịch
        /// </summary>
        public string astr_jyyzm { get; set; }
        /// <summary>
        /// Cờ giao dịch
        /// </summary>
        public long aint_appcode { get; set; }
        /// <summary>
        /// Thông tin giao dịch
        /// </summary>
        public string astr_appmsg { get; set; }
        /// <summary>
        /// Số khám bệnh
        /// </summary>
        public string prm_akc190 { get; set; }
        /// <summary>
        /// Mã trung tâm chi nhánh
        /// </summary>
        public string prm_yab003 { get; set; }
        /// <summary>
        /// Số quyết toán
        /// </summary>
        public string prm_yka103 { get; set; }
        /// <summary>
        /// Số cá nhân
        /// </summary>
        public string prm_aac001 { get; set; }
        /// <summary>
        /// Số tiền thanh toán từ tài khoản cá nhân
        /// </summary>
        public decimal? prm_yka065 { get; set; }
        /// <summary>
        /// Thời gian xử lý
        /// </summary>
        public DateTime? prm_aae036 { get; set; }
        /// <summary>
        /// Tổng chi phí y tế
        /// </summary>
        public decimal prm_yka055 { get; set; }
        /// <summary>
        /// Tổng số tiền tự chi trả toàn bộ
        /// </summary>
        public decimal prm_yka056 { get; set; }
        /// <summary>
        /// Số tiền tự chi trả liên kết
        /// </summary>
        public decimal prm_yka057 { get; set; }
        /// <summary>
        /// Số tiền phù hợp phạm vi
        /// </summary>
        public decimal prm_yka111 { get; set; }
        /// <summary>
        /// Số tiền vào mức khởi điểm
        /// </summary>
        public decimal? prm_yka058 { get; set; }
        /// <summary>
        /// Số tiền chi trả từ quỹ chung y tế cơ bản
        /// </summary>
        public decimal? prm_yka248 { get; set; }
        /// <summary>
        /// Số tiền thanh toán y tế lớn
        /// </summary>
        public decimal? prm_yka062 { get; set; }
        /// <summary>
        /// Số tiền hoàn trả trợ cấp công chức
        /// </summary>
        public decimal prm_yke030 { get; set; }
        /// <summary>
        /// Quỹ bảo đảm y tế cho người thương tật
        /// </summary>
        public decimal prm_ake032 { get; set; }
        /// <summary>
        /// Quỹ trợ cấp dân chính
        /// </summary>
        public decimal prm_ake181 { get; set; }
        /// <summary>
        /// Chi từ quỹ khác
        /// </summary>
        public decimal prm_ake173 { get; set; }
        /// <summary>
        /// Số dư tài khoản sau thanh toán lần này
        /// </summary>
        public decimal prm_akc087 { get; set; }
        /// <summary>
        /// Trung tâm chi nhánh thanh toán
        /// </summary>
        public string prm_ykb037 { get; set; }
        /// <summary>
        /// Loại thanh toán
        /// </summary>
        public string prm_yka316 { get; set; }
        /// <summary>
        /// Loại nhân viên y tế
        /// </summary>
        public string prm_akc021 { get; set; }
        /// <summary>
        /// Cấp công chức
        /// </summary>
        public string prm_ykc120 { get; set; }
        /// <summary>
        /// Trung tâm chi nhánh nơi tham gia bảo hiểm
        /// </summary>
        public string prm_yab139 { get; set; }
        /// <summary>
        /// Họ tên
        /// </summary>
        public string prm_aac003 { get; set; }
        /// <summary>
        /// Giới tính
        /// </summary>
        public string prm_aac004 { get; set; }
        /// <summary>
        /// Số CMND/CCCD
        /// </summary>
        public string prm_aac002 { get; set; }
        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime prm_aac006 { get; set; }
        /// <summary>
        /// Tuổi thực
        /// </summary>
        public decimal? prm_akc023 { get; set; }
        /// <summary>
        /// Mã đơn vị
        /// </summary>
        public string prm_aab001 { get; set; }
        /// <summary>
        /// Tên đơn vị
        /// </summary>
        public string prm_aab004 { get; set; }
        /// <summary>
        /// Trạng thái tham gia bảo hiểm cá nhân
        /// </summary>
        public string prm_aac031 { get; set; }
        /// <summary>
        /// Loại nhân viên y tế cư dân
        /// </summary>
        public string prm_ykc280 { get; set; }
        /// <summary>
        /// Thân phận nhân viên y tế cư dân
        /// </summary>
        public string prm_ykc281 { get; set; }
        /// <summary>
        /// Phương thức thanh toán
        /// </summary>
        public string prm_yka054 { get; set; }
        /// <summary>
        /// Số kỳ thanh toán
        /// </summary>
        public string prm_yae366 { get; set; }
        /// <summary>
        /// Số lần nội trú thực tế trong năm
        /// </summary>
        public decimal? prm_akc090 { get; set; }
        /// <summary>
        /// Số tiền lũy kế quỹ chung cơ bản
        /// </summary>
        public decimal? prm_yka120 { get; set; }
        /// <summary>
        /// Số tiền lũy kế quỹ chung lớn
        /// </summary>
        public decimal? prm_yka122 { get; set; }
        /// <summary>
        /// Lũy kế năm mức khởi điểm khám ngoại trú thông thường được trợ cấp công chức(Bao gồm bệnh mãn tính)
        /// </summary>
        public decimal? prm_yka368 { get; set; }
        /// <summary>
        /// Lũy kế trợ cấp ngoại trú cho công chức trong năm(Bao gồm bệnh mãn tính)
        /// </summary>
        public decimal? prm_yke025 { get; set; }
        /// <summary>
        /// Lũy kế mức khởi điểm loại bệnh quy định
        /// </summary>
        public decimal? prm_yka900 { get; set; }
        /// <summary>
        /// Năm
        /// </summary>
        public decimal? prm_aae001 { get; set; }
        /// <summary>
        /// Bệnh đơn lẻ(Quyết toán)Mã
        /// </summary>
        public string prm_yka089 { get; set; }
        /// <summary>
        /// Bệnh đơn lẻ(Quyết toán)Tên loại bệnh
        /// </summary>
        public string prm_yka027 { get; set; }
        /// <summary>
        /// Bệnh đơn lẻ(Quyết toán)Chi phí tự chi trả của cơ sở y tế
        /// </summary>
        public decimal? prm_yka028 { get; set; }
        /// <summary>
        /// Bệnh đơn lẻ(Quyết toán)Tiêu chuẩn khoán
        /// </summary>
        public decimal? prm_yka345 { get; set; }
        /// <summary>
        /// Loại địa phương BHYT,Quý An:guian,Thường Thục:changshu
        /// </summary>
        public string ybdlx { get; set; }


        //BHYT dưới đây trả về có giá trị
        /// <summary>
        /// 
        /// </summary>
        public string prm_aka130 { get; set; }
        public string prm_ykc299 { get; set; }
        public string prm_yka128 { get; set; }
        public decimal? prm_sdxj { get; set; }
        public decimal? prm_bzxj { get; set; }
        public decimal? prm_ysxj { get; set; }

        
    }
}
