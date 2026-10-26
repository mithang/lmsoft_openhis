using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.DTO.OutputDto
{
    public class InZyfymxxrDto
    {
        /// <summary>
        /// Số thứ tự khám
        /// </summary>
        public string akc190 { get; set; }
        /// <summary>
        /// Loại thanh toán
        /// </summary>
        public string aka130 { get; set; }
        /// <summary>
        /// Mã trung tâm chi nhánh
        /// </summary>
        public string yab003 { get; set; }
        /// <summary>
        /// Mã cá nhân
        /// </summary>
        public string aac001 { get; set; }
        /// <summary>
        /// Phương pháp BHXH
        /// </summary>
        public string ykb065 { get; set; }
        /// <summary>
        /// Chi tiết chi phí nội trú
        /// </summary>
        public List<ZyfymxNodeDto> zyfymx { get; set; }
    }

    public class ZyfymxNodeDto
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
        /// Số CMND của bác sĩ kê đơn_ykd102
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
        /// Số CMND của bác sĩ nhận đơn_ykd106
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
        public int yke186 { get; set; }
    }
}
