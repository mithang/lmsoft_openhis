using Newtouch.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.Domain.DTO.OutputDto
{
    /// <summary>
    /// Đẩy đơn thuốc
    /// </summary>
    public class PrescriptionAPIDto
    {
        /// <summary>
        /// 1 Thuốc 2Hạng mục
        /// </summary>
        public int cflx { get; set; }
        /// <summary>
        /// Phân loại chi tiết loại đơn thuốc EnumCflx
        /// </summary>
        public int cflxxf { get; set; }
        /// <summary>
        /// Tên loại đơn thuốc
        /// </summary>
        public string cflxmc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cfh { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? ts { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool? djbz { get; set; }

        /// <summary>
        /// Một lần đăng ký khám có thể có các bác sĩ khác nhau kê đơn
        /// </summary>
        public string ys { get; set; }

        /// <summary>
        /// Một lần đăng ký khám có thể có các bác sĩ khác nhau kê đơn
        /// </summary>
        public string ks { get; set; }

        /// <summary>
        /// Nhà thuốc lĩnh thuốcCode
        /// </summary>
        public string lyyf { get; set; }

        /// <summary>
        /// Chi tiết thuốc
        /// </summary>
        public List<MedicineItemVO> AddMedicineItems { get; set; }
        /// <summary>
        /// Chi tiết hạng mục
        /// </summary>
        public List<TreamentItemVO> AddTreamentItems { get; set; }
    }
}
