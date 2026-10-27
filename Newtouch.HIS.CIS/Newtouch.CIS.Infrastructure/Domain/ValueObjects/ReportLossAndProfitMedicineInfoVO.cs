using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects
{
    /// <summary>
    /// Báo hỏngbáo thừaMã thuốcTra cứu
    /// </summary>
    public class ReportLossAndProfitMedicineInfoVO
    {
        /// <summary>
        /// 
        /// </summary>
        public string ypCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ypmc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sccj { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ypgg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int kcsl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string zxdw { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string xykcstr { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string yxq { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ph { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal? zhyz { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string djdw { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal lsj { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal pfj { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal Ykpfj { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal Yklsj { get; set; }
        /// <summary>
        /// ThuốcLoại
        /// </summary>
        public string yplb { get; set; }

        /// <summary>
        /// ThuốcTrạng thái
        /// </summary>
        public string ypzt { get; set; }

        /// <summary>
        /// vàogiá
        /// </summary>
        public decimal? jj { get; set; }
    }
}
