using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.BusinessObjects
{
    /// <summary>
    /// Ghi sổ nội trú Hạng mục GửiFormBO
    /// </summary>
    public class InpatientAccountingPlanItemDto
    {
        /// <summary>
        /// Chi tiết kế hoạch ghi sổId
        /// </summary>
        public string jzjhmxId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string yzxz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfxmCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? duration { get; set; }

        /// <summary>
        /// Có hay không'Điều trị tập thể'
        /// </summary>
        public string ttbz { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string bz { get; set; }

        /// <summary>
        /// Bác sĩ liên kết
        /// </summary>
        public IList<InpatientAccountingPlanItemDoctorDto> ysList { get; set; }

        /// <summary>
        /// Loại y lệnh 1Thuốc 2Hạng mục
        /// </summary>
        public string yzlx { get; set; }

        /// <summary>
        /// Đơn giá(Đơn giá khi gửi kế hoạch ghi sổ)
        /// </summary>
        public decimal? dj { get; set; }

        /// <summary>
        /// Loại phục hồi chức năng
        /// </summary>
        public string kflb { get; set; }
        public int? zll { get; set; }
        public int? dwjls { get; set; }
        public int? jjcl { get; set; }
    }
}
