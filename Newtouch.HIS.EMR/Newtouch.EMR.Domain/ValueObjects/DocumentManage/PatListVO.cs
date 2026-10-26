using Newtouch.EMR.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.ValueObjects
{
    [NotMapped]
    public class PatListVO: ZybrjbxxEntity
    {
        /// <summary>
        /// Có chọn mặc định hay không
        /// </summary>
        public int isCheck { get; set; }
        public int? cyts { get; set; }
        public string cwmc { get; set; }
        public string RecordStusm { get; set; }

		public string zybzmc { get; set; }
	}
}
