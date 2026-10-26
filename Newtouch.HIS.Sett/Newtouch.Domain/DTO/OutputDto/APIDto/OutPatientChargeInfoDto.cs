using Newtouch.HIS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.DTO.OutputDto
{
    public class OutPatientChargeMainDto {
        #region 挂号收费查询的主记录

        /// <summary>
        /// Số bệnh án
        /// </summary>
        public string blh { get; set; }
        /// <summary>
        /// Mã nội bộ quyết toán
        /// </summary>
        public int jsnm { get; set; }
        /// <summary>
        /// Số thẻ
        /// </summary>
        public string kh { get; set; }
        /// <summary>
        /// Họ tên
        /// </summary>
        public string xm { get; set; }
        /// <summary>
        /// Số hóa đơn
        /// </summary>
        public string fph { get; set; }
        /// <summary>
        /// Nhân viên lập hồ sơ
        /// </summary>
        public string CreatorCode { get; set; }
        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Tên bác sĩ đăng ký khám(Có thể là cập nhật sau)
        /// </summary>
        public string ghysmc { get; set; }
        /// <summary>
        /// Tên khoa đăng ký khám
        /// </summary>
        public string ghksmc { get; set; }
        /// <summary>
        /// Số hóa đơn cũ
        /// </summary>
        public string oldfph { get; set; }
        /// <summary>
        /// isxfph
        /// </summary>
        public string isxfph { get; set; }
        /// <summary>
        /// tCreatorCode
        /// </summary>
        public string tCreatorCode { get; set; }

        /// <summary>
        /// Tên nhân viên thu phí
        /// </summary>
        public string CreatorName { get; set; }

        /// <summary>
        /// Loại quyết toán
        /// </summary>
        public string jslx { get; set; }

        #endregion

        /// <summary>
        /// Tổng số tiền thu phí
        /// </summary>
        public decimal jszje { get; set; }
        /// <summary>
        /// Số tiền thanh toán
        /// </summary>
        public decimal? jsxjzf { get; set; }

        /// <summary>
        /// Ngày thu phí
        /// </summary>
        public DateTime? sfrq { get; set; }

        /// <summary>
        /// Tên tính chất bệnh nhân
        /// </summary>
        public string brxzmc { get; set; }
        /// <summary>
        /// Số ngoại trú của lần đăng ký khám tương ứng với quyết toán
        /// </summary>
        public string mzh { get; set; }

    }
    /// <summary>
    /// for API
    /// </summary>
    public class OutPatientChargeInfoDto
    {
        public IList<OutPatientChargeMainDto> MainRecords;
        public IList<OutPatientRegChargeDetailsVO> DetailRecords;
    }
}
