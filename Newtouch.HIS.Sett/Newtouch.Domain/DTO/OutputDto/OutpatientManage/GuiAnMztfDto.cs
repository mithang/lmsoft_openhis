using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.DTO.OutputDto.OutpatientManage
{
    public class GuiAnMztfDto
    {
    }

    public class InMzjshtDto
    {
        /// <summary>
        /// Số khám bệnh
        /// </summary>
        public string prm_akc190 { get; set; }
        /// <summary>
        /// Mã trung tâm chi nhánh
        /// </summary>
        public string prm_yab003 { get; set; }
        /// <summary>
        /// Loại thanh toán
        /// </summary>
        public string prm_aka130 { get; set; }
        /// <summary>
        /// Số quyết toán
        /// </summary>
        public string prm_yka103 { get; set; }
        /// <summary>
        /// Mã nhân viên xử lý
        /// </summary>
        public string prm_aae011 { get; set; }
        /// <summary>
        /// Tên người xử lý
        /// </summary>
        public string prm_ykc141 { get; set; }
        /// <summary>
        /// Thời gian xử lý
        /// </summary>
        public string prm_aae036 { get; set; }
        /// <summary>
        /// Lý do hoàn phí
        /// </summary>
        public string prm_aae013 { get; set; }
        /// <summary>
        /// Phương pháp BHXH
        /// </summary>
        public string prm_ykb065 { get; set; }
        /// <summary>
        /// Số cá nhân
        /// </summary>
        public string prm_aac001 { get; set; }
    }
    /// <summary>
    /// Frontend truyền tham số về backend
    /// </summary>
    public class GuiAnMztfProDto
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
        /// Tổng số tiền
        /// </summary>
        public decimal? ZFY { get; set; }
        /// <summary>
        /// Thanh toán tiền mặt
        /// </summary>
        public decimal? XJZF { get; set; }

    }
}
