using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.DTO.OutputDto.OutpatientManage
{
    class GuiAnMzjsDto
    {
    }
    /// <summary>
    /// Dùng cho tra cứu cục bộ
    /// </summary>
    public class GuiAnMzjsPatInfoDto
    {
        public string prm_akc190 { get; set; }
        public string prm_aac001 { get; set; }
        public string prm_ykc173 { get; set; }
        public string prm_hisfyze { get; set; }
        public string prm_aka130 { get; set; }
        public string prm_yka110 { get; set; }
        public string prm_aae013 { get; set; }
        public string prm_aae011 { get; set; }
        public string prm_ykc141 { get; set; }
        public string prm_ykb065 { get; set; }
    }

    /// <summary>
    /// Tải lên quyết toán BHYT
    /// </summary>
    public class InPtmzjs
    {
        /// <summary>
        /// Số khám bệnh
        /// </summary>
        public string prm_akc190 { get; set; }
        /// <summary>
        /// Số cá nhân
        /// </summary>
        public string prm_aac001 { get; set; }
        /// <summary>
        /// Thông tin chẩn đoán ngoại trú
        /// </summary>
        public string prm_ykc173 { get; set; }
        /// <summary>
        /// HIS Tổng chi phí
        /// </summary>
        public string prm_hisfyze { get; set; }
        /// <summary>
        /// Loại thanh toán
        /// </summary>
        public string prm_aka130 { get; set; }
        /// <summary>
        /// Số hóa đơn
        /// </summary>
        public string prm_yka110 { get; set; }
        /// <summary>
        /// Ghi chú
        /// </summary>
        public string prm_aae013 { get; set; }
        /// <summary>
        /// Mã người xử lý
        /// </summary>
        public string prm_aae011 { get; set; }
        /// <summary>
        /// Tên người xử lý
        /// </summary>
        public string prm_ykc141 { get; set; }
        /// <summary>
        /// Thực hiện phương pháp BHXH
        /// </summary>
        public string prm_ykb065 { get; set; }
        /// <summary>
        /// Chi tiết quyết toán
        /// </summary>
        public List<FeejsMx> row { get; set; }
    }

    /// <summary>
    /// Chi tiết quyết toán chi phí
    /// </summary>
    public class FeejsMx
    {

        /// <summary>
        /// Số giao dịch ghi sổ
        /// </summary>
        public string yka105 { get; set; }
        /// <summary>
        /// YKD125
        /// </summary>
        public string ykd125 { get; set; }
        /// <summary>
        /// YKD126
        /// </summary>
        public string ykd126 { get; set; }
        /// <summary>
        /// Mã hạng mục chung BHYT_YKA002
        /// </summary>
        public string yka002 { get; set; }
        /// <summary>
        /// Tên hạng mục chung BHYT_YKA003
        /// </summary>
        public string yka003 { get; set; }
        /// <summary>
        /// Số lượng_AKC226
        /// </summary>
        public decimal akc226 { get; set; }
        /// <summary>
        /// Giá thực tế_AKC225
        /// </summary>
        public decimal akc225 { get; set; }
        /// <summary>
        /// Tổng chi phí hạng mục chi tiết_YKA315
        /// </summary>
        public decimal yka315 { get; set; }
        /// <summary>
        /// Mã khoa kê đơn_YKA097
        /// </summary>
        public string yka097 { get; set; }
        /// <summary>
        /// Tên khoa kê đơn_YKA098
        /// </summary>
        public string yka098 { get; set; }
        /// <summary>
        /// Số CMND của bác sĩ kê đơn
        /// </summary>
        public string ykd102 { get; set; }
        /// <summary>
        /// Tên bác sĩ kê đơn
        /// </summary>
        public string yka099 { get; set; }
        /// <summary>
        /// Mã khoa nhận đơn_YKA100
        /// </summary>
        public string yka100 { get; set; }
        /// <summary>
        /// Tên khoa nhận đơn_YKA101
        /// </summary>
        public string yka101 { get; set; }
        /// <summary>
        /// Số CMND của bác sĩ nhận đơn
        /// </summary>
        public string ykd106 { get; set; }
        /// <summary>
        /// Tên bác sĩ nhận đơn
        /// </summary>
        public string yka102 { get; set; }
        /// <summary>
        /// Thời gian phát sinh chi tiết_YKE123
        /// </summary>
        public string yke123 { get; set; }
        /// <summary>
        /// Tên người xử lý_YKC141
        /// </summary>
        public string ykc141 { get; set; }
        /// <summary>
        /// Thời gian xử lý_AAE036
        /// </summary>
        public string aae036 { get; set; }
        /// <summary>
        /// Ghi chú_AAE013
        /// </summary>
        public string aae013 { get; set; }
        /// <summary>
        /// Cách dùng thuốc Trung y
        /// </summary>
        public string yke201 { get; set; }
        /// <summary>
        /// Đơn vị tính giá nhỏ nhất
        /// </summary>
        public string yka295 { get; set; }
        /// <summary>
        /// Quy cách
        /// </summary>
        public string aka074 { get; set; }
        /// <summary>
        /// Dạng bào chế
        /// </summary>
        public string aka070 { get; set; }
        /// <summary>
        /// Tên dạng bào chế
        /// </summary>
        public string yae374 { get; set; }
        /// <summary>
        /// Có phải là chế phẩm của bệnh viện hay không
        /// </summary>
        public string yke009 { get; set; }
        /// <summary>
        /// Cờ phê duyệt bệnh viện
        /// </summary>
        public string yke186 { get; set; }
    }

    /// <summary>
    /// Trả về tích hợp dữ liệu frontend:BHYT+Tự lập
    /// </summary>
    public class guianMainAllOfMzjs
    {
        /// <summary>
        /// Tổng số tiền hoàn tiền
        /// </summary>
        public decimal? tkzje { get; set; }
        /// <summary>
        /// Tổng số tiền BHYT quyết toán lại
        /// </summary>
        public decimal? ybzje { get; set; }
        /// <summary>
        /// Tổng số tiền tự lập quyết toán lại
        /// </summary>
        public decimal? zlzje { get; set; }

        /// <summary>
        /// Danh sách hạng mục tự lập
        /// </summary>
        public List<FeejsMx> guianZLMainOfMzjs;

        /// <summary>
        /// Danh sách hạng mục BHYT
        /// </summary>
        public InPtmzjs guianYBMainOfMzjs;
    }
}
