using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    public class SysMedicineReDetailVO
    {
        /// <summary>
        /// Đơn lĩnhID
        /// </summary>
        public string sldId { get; set; }

        /// <summary>
        /// Tồn kho khả dụng,kèm đơn vị
        /// </summary>
        public string kc { get; set; }

        /// <summary>
        /// Tồn kho khả dụng(Đơn vị nhỏ nhất)
        /// </summary>
        public int kcsl { get; set; }
        public string Ypdm { get; set; }
        public string sldmxId { get; set; }

        public string sldh { get; set; }

        /// <summary>
        /// Tên thuốc
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// Quy cách thuốc
        /// </summary>
        public string ypgg { get; set; }
        public string py { get; set; }
        public string Ph { get; set; }
        public string pc { get; set; }
        public DateTime Yxq { get; set; }
        public string ycmc { get; set; }
        public string bzdw { get; set; }
        public decimal Pfj { get; set; }
        public decimal Lsj { get; set; }

        /// <summary>
        /// Số lượng lĩnh,kèm đơn vị
        /// </summary>
        public string Sl { get; set; }
        public decimal Pjze { get; set; }
        public decimal lsje { get; set; }

        /// <summary>
        /// Đơn vị đóng gói
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// Số lượng lĩnh
        /// </summary>
        public int Slsl { get; set; }

        /// <summary>
        /// Số lượng phát thuốc(Số lượng lĩnh-Số lượng đã phát, Đơn vị nhỏ nhất)
        /// </summary>
        public int fysl { get; set; }

        /// <summary>
        /// Số lượng đã phát(Đơn vị nhỏ nhất)
        /// </summary>
        public int yfsl { get; set; }

        /// <summary>
        /// Số lượng đã phát kèm đơn vị
        /// </summary>
        public string yfslStr { get; set; }
    }
}
