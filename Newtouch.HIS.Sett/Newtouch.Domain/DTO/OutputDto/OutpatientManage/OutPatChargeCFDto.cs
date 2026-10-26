/**********************************************************/
// Copyright (C) 2016 Newtouch Bảo lưu bản quyền. 
// Description: 
// Author:
// CreateDate: 2016/12/28 16:06:41 
//**********************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.DTO.OutputDto
{
   public  class OutPatChargeCFDto
    {
        /// <summary>
        /// Số đơn thuốc
        /// </summary>
        public string  cfh { get; set; }

        /// <summary>
        /// Phí dịch vụ
        /// </summary>
        public decimal? fwf { get; set; }

        /// <summary>
        /// Số tiền
        /// </summary>
        public double jeNum { get; set; }
    }
}
