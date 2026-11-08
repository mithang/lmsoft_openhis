using Newtouch.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.Domain.DTO.OutputDto
{
    /// <summary>
    /// đẩy Đơn thuốc
    /// </summary>
    public class PrescriptionAPIDto
    {
        /// <summary>
        /// 1 Thuốc 2Mục
        /// </summary>
        public int cflx { get; set; }
        /// <summary>
        /// Loại đơn thuốc chi tiết (EnumCflx)
        /// </summary>
        public int cflxxf { get; set; }
        /// <summary>
        /// Đơn thuốc Tên loại
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
        /// phiếu một đăng ký khám có thể có thể có không cùng của Bác sĩ cho mở Đơn thuốc
        /// </summary>
        public string ys { get; set; }

        /// <summary>
        /// phiếu một đăng ký khám có thể có thể có không cùng của Bác sĩ cho mở Đơn thuốc
        /// </summary>
        public string ks { get; set; }

        /// <summary>
        /// lĩnh thuốc Nhà thuốc Code
        /// </summary>
        public string lyyf { get; set; }

        /// <summary>
        /// Thuốc chi tiết
        /// </summary>
        public List<MedicineItemVO> AddMedicineItems { get; set; }
        /// <summary>
        /// Mục chi tiết
        /// </summary>
        public List<TreamentItemVO> AddTreamentItems { get; set; }
    }
}
