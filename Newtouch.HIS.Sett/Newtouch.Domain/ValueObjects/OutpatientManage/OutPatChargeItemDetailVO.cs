/**********************************************************/
// Copyright (C) 2016 Newtouch Bảo lưu bản quyền. 
// Description: Danh sách hạng mục tương ứng với một mẫu
// Author:hlf
// CreateDate: 2017/1/10 12:41:31 
//**********************************************************/

using System.Collections.Generic;

namespace Newtouch.HIS.Domain.ValueObjects
{
    public class OutPatChargeItemDetailVO
    {
        /// <summary>
        /// Mã mẫu
        /// </summary>
        public string ItemCode { get; set; }

        /// <summary>
        /// Danh sách hạng mục
        /// </summary>
        public List<TemplateContentVO> tempDetailList { get; set; }
    }
}
