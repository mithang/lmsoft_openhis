namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// thuốc điều kiện tra cứu
    /// </summary>
    public class MedicineInfoParam
    {
        /// <summary>
        /// mã thuốc
        /// </summary>
        public string ypdm { get; set; }

        /// <summary>
        /// loại thuốc
        /// </summary>
        public string yplb { get; set; }

        /// <summary>
        /// 药品熟悉
        /// </summary>
        public string ypsx { get; set; }

        /// <summary>
        /// dạng bào chế thuốc
        /// </summary>
        public string ypjx { get; set; }

        /// <summary>
        /// trạng thái sử dụng kiểm soát, bình thường
        /// </summary>
        public string syzt { get; set; }

        /// <summary>
        /// trạng thái thuốc ngừng sử dụng, kích hoạt
        /// </summary>
        public string ypzt { get; set; }

        /// <summary>
        /// mã nhập
        /// </summary>
        public string srm { get; set; }
    }
}
