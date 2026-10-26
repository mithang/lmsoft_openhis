using Newtouch.HIS.Domain.Entity.OutpatientManage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.OutpatientManage
{
    [NotMapped]
    public class OutBookScheduleVO : OutBookScheduleEntity
    {
        /// <summary>
        /// Tên hạng mục đăng ký khám
        /// </summary>
        public string sfxmmc { get; set; }
        /// <summary>
        /// Tên hạng mục khám chữa bệnh
        /// </summary>
        public string zlxmmc { get; set; }
    }
}
