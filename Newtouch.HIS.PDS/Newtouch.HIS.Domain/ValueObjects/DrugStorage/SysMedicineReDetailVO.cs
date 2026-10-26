using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    public class SysMedicineReDetailVO
    {
        /// <summary>
        /// phiếu lĩnh ID
        /// </summary>
        public string sldId { get; set; }

        /// <summary>
        /// tồn kho khả dụng, kèm đơn vị
        /// </summary>
        public string kc { get; set; }

        /// <summary>
        /// tồn kho khả dụng (đơn vị nhỏ nhất)
        /// </summary>
        public int kcsl { get; set; }
        public string Ypdm { get; set; }
        public string sldmxId { get; set; }

        public string sldh { get; set; }

        /// <summary>
        /// tên thuốc
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// quy cách thuốc
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
        /// số lượng lĩnh, kèm đơn vị
        /// </summary>
        public string Sl { get; set; }
        public decimal Pjze { get; set; }
        public decimal lsje { get; set; }

        /// <summary>
        /// đơn vị đóng gói
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// số lượng lĩnh
        /// </summary>
        public int Slsl { get; set; }

        /// <summary>
        /// số lượng phát thuốc (số lượng lĩnh-số lượng đã phát, đơn vị nhỏ nhất)
        /// </summary>
        public int fysl { get; set; }

        /// <summary>
        /// số lượng đã phát (đơn vị nhỏ nhất)
        /// </summary>
        public int yfsl { get; set; }

        /// <summary>
        /// số lượng đã phát kèm đơn vị
        /// </summary>
        public string yfslStr { get; set; }
    }
}
