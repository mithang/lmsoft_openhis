using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.Entity.OutpatientManage
{
    [Table("mz_js_gaybjyfy")]
    public class OutpatientSettlementGAYBFeeEntity: IEntity<OutpatientSettlementGAYBFeeEntity>
    {
        /// <summary>
        /// Mã nội bộ chi phí
        /// </summary>
        [Key]
        public int id { get; set; }
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
        public decimal? prm_yka055 { get; set; }
        /// <summary>
        /// Tổng số tiền tự chi trả toàn bộ
        /// </summary>
        public decimal? prm_yka056 { get; set; }
        /// <summary>
        /// Số tiền tự chi trả liên kết
        /// </summary>
        public decimal? prm_yka057 { get; set; }
        /// <summary>
        /// Số tiền phù hợp phạm vi
        /// </summary>
        public decimal? prm_yka111 { get; set; }
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
        public decimal? prm_yke030 { get; set; }
        /// <summary>
        /// Quỹ bảo đảm y tế cho người thương tật
        /// </summary>
        public decimal? prm_ake032 { get; set; }
        /// <summary>
        /// Quỹ trợ cấp dân chính
        /// </summary>
        public decimal? prm_ake181 { get; set; }
        /// <summary>
        /// Chi từ quỹ khác
        /// </summary>
        public decimal? prm_ake173 { get; set; }
        /// <summary>
        /// Số dư tài khoản sau thanh toán lần này
        /// </summary>
        public decimal? prm_akc087 { get; set; }
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
        public DateTime? prm_aac006 { get; set; }
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
        /// Loại nhân viên cư dân thành thị và nông thôn
        /// </summary>
        public string prm_ykc299 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }
        /// <summary>
        /// Mã nội bộ quyết toán
        /// </summary>
        public int jsnm { get; set; }
        /// <summary>
        /// Số quyết toán trả về khi quay lại quyết toán
        /// </summary>
        public string prm_yka198 { get; set; }
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
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }
    }
}
