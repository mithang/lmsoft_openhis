using Newtouch.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.Domain.DTO.OutputDto
{
    /// <summary>
    /// đẩyĐơn thuốc
    /// </summary>
    public class PrescriptionAPIDto
    {
        /// <summary>
        /// 1 Thuốc 2Mục
        /// </summary>
        public int cflx { get; set; }
        /// <summary>
        /// Đơn thuốcLoại细phút EnumCflx
        /// </summary>
        public int cflxxf { get; set; }
        /// <summary>
        /// Đơn thuốcTên loại
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
        /// phiếumộtđăng ký khámcó thểcó thểcókhôngcùngcủaBác sĩchomởĐơn thuốc
        /// </summary>
        public string ys { get; set; }

        /// <summary>
        /// phiếumộtđăng ký khámcó thểcó thểcókhôngcùngcủaBác sĩchomởĐơn thuốc
        /// </summary>
        public string ks { get; set; }

        /// <summary>
        /// lĩnhthuốcNhà thuốcCode
        /// </summary>
        public string lyyf { get; set; }

        /// <summary>
        /// Thuốcchi tiết
        /// </summary>
        public List<MedicineItemVO> AddMedicineItems { get; set; }
        /// <summary>
        /// Mụcchi tiết
        /// </summary>
        public List<TreamentItemVO> AddTreamentItems { get; set; }
    }
}
