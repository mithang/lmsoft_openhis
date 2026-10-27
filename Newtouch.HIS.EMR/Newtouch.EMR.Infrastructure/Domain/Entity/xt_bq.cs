using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace Newtouch.EMR.Domain.Entity
{
    /// <summary>
    /// Khu bệnh hệ thống
    /// </summary>
    public class xt_bq
    {
        /// <summary>
        ///Khu bệnh ID
        /// </summary>
        public string bqCode { get; set; }
        /// <summary>
        /// Tên khu bệnh
        /// </summary>
        public string bqmc { get; set; }
    }
}
