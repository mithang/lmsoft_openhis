using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Phụ thêm tính phí bệnh nhân hệ thống
    /// </summary>
    public class SysPatiChargeAddVo
    {

        /// <summary>
        /// Số phụ thêm tính phí bệnh nhân(xt_brsffj)
        /// </summary>
        public int brsffjbh { get; set; }

        /// <summary>
        /// Tính chất bệnh nhân(xt_brxz)
        /// </summary>
        public string brxzmc { get; set; }

        public string brxz { get; set; }

        /// <summary>
        /// Loại lớn(xt_sfdl)
        /// </summary>
        public string dlmc { get; set; }

        public string dl { get; set; }

        /// <summary>
        /// Tên loại lớn hiển thị bổ sung(xt_fjsfdl)
        /// </summary>
        public string fjxsdlmc { get; set; }

        public string fjxsdl { get; set; }

        /// <summary>
        /// Hạng mục thu phí(xt_fjsfdl)
        /// </summary>
        public string sfxmmc { get; set; }

        public string sfxm { get; set; }

        /// <summary>
        /// Tỷ lệ phí dịch vụ(xt_brsffj)
        /// </summary>
        public decimal fwfbl { get; set; }

        /// <summary>
        /// Trạng thái(xt_brsffj)
        /// </summary>
        public string zt { get; set; }
        /// <summary>
        /// Nhân viên lập hồ sơ(xt_brsffj)
        /// </summary>
        public string CreatorCode { get; set; }
        /// <summary>
        /// Ngày lập hồ sơ(xt_brsffj)
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
