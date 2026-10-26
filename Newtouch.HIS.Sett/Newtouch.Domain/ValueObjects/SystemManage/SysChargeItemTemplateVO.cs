using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.SystemManage
{
    public class SysChargeItemTemplateVO : SysChargeItemVEntity
    {
        public int? sl { get; set; }

        public string kflb { get; set; }
        public string kflbmc { get; set; }

        /// <summary>
        /// Liều điều trị
        /// </summary>
        public int? zll { get; set; }

        /// <summary>
        /// Số lần thực hiện
        /// </summary>
        public int? zxcs { get; set; }

        /// <summary>
        /// Tần suất y lệnh
        /// </summary>
        public string yzpc { get; set; }

        /// <summary>
        /// Vị trí(Đề xuất điều trị phục hồi chức năng)
        /// </summary>
        public string bw { get; set; }
        public string yfdm { get; set; }
        public string yfmc { get; set; }
    }
}
