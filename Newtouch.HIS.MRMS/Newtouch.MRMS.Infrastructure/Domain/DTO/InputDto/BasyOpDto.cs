using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SqlSugar;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Utilities;
using NewtouchHIS.Lib.Framework.Filter;
using NewtouchHIS.Lib.Framework.Operator;

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
        /// Mã phẫu thuật và thủ thuật
        /// </summary>
        /// <returns></returns>
        public string SSJCZBM { get; set; }

        /// <summary>
        /// Ngày phẫu thuật và thủ thuật
        /// </summary>
        /// <returns></returns>
        public DateTime SSJCZRQ { get; set; }

        /// <summary>
        /// Cấp độ phẫu thuật
        /// </summary>
        /// <returns></returns>
        public string SSJB { get; set; }

        /// <summary>
        /// Tên phẫu thuật và thủ thuật
        /// </summary>
        /// <returns></returns>
        public string SSJCZMC { get; set; }

        /// <summary>
        /// Phẫu thuật viên
        /// </summary>
        /// <returns></returns>
        public string SZ { get; set; }

        /// <summary>
        /// Trợ thủ I
        /// </summary>
        /// <returns></returns>
        public string YZ { get; set; }

        /// <summary>
        /// Trợ thủ II
        /// </summary>
        /// <returns></returns>
        public string EZ { get; set; }

        /// <summary>
        /// Mức độ vết mổ
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
        /// Tên bác sĩ mổ chính
        /// </summary>
        public string SZMC { get; set; }
        /// <summary>
        /// Tên trợ thủ I
        /// </summary>
        public string YZMC { get; set; }
        /// <summary>
        /// Tên trợ thủ II
        /// </summary>
        public string EZMC { get; set; }
        /// <summary>
        /// Tên bác sĩ gây mê
        /// </summary>
        public string MZYSMC { get; set; }
        /// <summary>
        /// Mô tả phương pháp gây mê
        /// </summary>
        public string MZFSMS { get; set; }

    }
}
