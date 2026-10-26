/**********************************************************/
// Copyright (C) 2016 Newtouch Bảo lưu bản quyền. 
// Description:Tích hợp mẫu hạng mục thu phí Và danh sách hạng mục thu phí tương ứng 
// Author:hlf
// CreateDate: 2017/1/10 12:24:52 
//**********************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects
{
    public class OutPatChargeItemTempVO
    {
        /// <summary>
        /// Danh sách mẫu
        /// </summary>
        public List<ChargeItemTemplateVO> tempList { get; set; }

        /// <summary>
        /// Thông tin chi tiết mẫu
        /// </summary>
        public List<OutPatChargeItemDetailVO> tempDetailList { get; set; }
    } 
}
