using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.MR.ManageSystem.Domain.DTO.InputDto
{
    public class BasyOpDto
    {
        public string Id { get; set; }

        /// <summary>
        /// BAH
        /// </summary>
        /// <returns></returns>
        public string BAH { get; set; }

        /// <summary>
        /// ZYH
        /// </summary>
        /// <returns></returns>
        public string ZYH { get; set; }

        /// <summary>
        /// SSOrder
        /// </summary>
        /// <returns></returns>
        public int SSOrder { get; set; }

        /// <summary>
        /// Mã phẫu thuật và thao tác
        /// </summary>
        /// <returns></returns>
        public string SSJCZBM { get; set; }

        /// <summary>
        /// Ngày phẫu thuật và thao tác
        /// </summary>
        /// <returns></returns>
        public DateTime SSJCZRQ { get; set; }

        /// <summary>
        /// Cấp độ phẫu thuật
        /// </summary>
        /// <returns></returns>
        public string SSJB { get; set; }

        /// <summary>
        /// Tên phẫu thuật và thao tác
        /// </summary>
        /// <returns></returns>
        public string SSJCZMC { get; set; }

        /// <summary>
        /// Người phẫu thuật
        /// </summary>
        /// <returns></returns>
        public string SZ { get; set; }

        /// <summary>
        /// IPhụ
        /// </summary>
        /// <returns></returns>
        public string YZ { get; set; }

        /// <summary>
        /// IIPhụ
        /// </summary>
        /// <returns></returns>
        public string EZ { get; set; }

        /// <summary>
        /// Cấp độ vết mổ
        /// </summary>
        /// <returns></returns>
        public string QKDJ { get; set; }

        /// <summary>
        /// Loại liền vết mổ
        /// </summary>
        /// <returns></returns>
        public string QKYHLB { get; set; }

        /// <summary>
        /// Phương pháp gây mê
        /// </summary>
        /// <returns></returns>
        public string MZFS { get; set; }

        /// <summary>
        /// Bác sĩ gây mê
        /// </summary>
        /// <returns></returns>
        public string MZYS { get; set; }

        /// <summary>
        /// zt
        /// </summary>
        /// <returns></returns>
        public string zt { get; set; }
        public string QKYHDJ { get; set; }
        /// <summary>
        /// Bác sĩ mổ chínhHọ tên
        /// </summary>
        public string SZMC { get; set; }
        /// <summary>
        /// IHọ tên phụ mổ
        /// </summary>
        public string YZMC { get; set; }
        /// <summary>
        /// IIHọ tên phụ mổ
        /// </summary>
        public string EZMC { get; set; }
        /// <summary>
        /// Bác sĩ gây mêHọ tên
        /// </summary>
        public string MZYSMC { get; set; }
        /// <summary>
        /// Phương pháp gây mêMô tả
        /// </summary>
        public string MZFSMS { get; set; }

    }
}
