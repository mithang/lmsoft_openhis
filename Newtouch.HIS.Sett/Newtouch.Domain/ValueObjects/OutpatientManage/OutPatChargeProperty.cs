/**********************************************************/
// Copyright (C) 2016 Newtouch Bảo lưu bản quyền. 
// Description: 
// Author:
// CreateDate: 2017/3/1 11:57:05 
//**********************************************************/

namespace Newtouch.HIS.Domain.ValueObjects
{
    public class OutPatChargeProperty
    {
        //[xt_brxz]
        /// <summary>
        /// Mã tính chất bệnh nhân
        /// </summary>
        public int brxzbh { get; set; }
        /// <summary>
        /// Tính chất bệnh nhân
        /// </summary>
        public string brxz { get; set; }
        /// <summary>
        /// Tên tính chất bệnh nhân
        /// </summary>
        public string brxzmc { get; set; }
        /// <summary>
        /// Loại giao dịch BHYT
        /// </summary>
        public string ybjylx { get; set; }

    }
}
