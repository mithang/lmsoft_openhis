/**********************************************************/
// Copyright (C) 2016 Newtouch Bảo lưu bản quyền. 
// Description:Nạp tiền tài khoản và hoàn phí  Thực thể xử lý sự vụ 
// Author:HLF
// CreateDate: 2017/1/11 16:34:07 
//**********************************************************/

using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.Entity.PatientManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects
{
    public class HosPatAccDataVO
    {

        /// <summary>
        /// Bản ghi thu chi tài khoản
        /// </summary>
        public SysPatientAccountRevenueAndExpenseEntity szjl { get; set; }
         

        /// <summary>
        /// Trước khi cập nhật Tài khoản
        /// </summary>
        public SysPatientAccountEntity oldzh { get; set; }
         

        /// <summary>
        /// Biên lai tài chính trước khi cập nhật
        /// </summary>
        public FinanceReceiptEntity oldcwsj { get; set; }

        
    }

    public class PatAccDataVO
    {

        /// <summary>
        /// Bản ghi thu chi tài khoản
        /// </summary>
        public SysAccountRevenueAndExpenseEntity szjl { get; set; }


        /// <summary>
        /// Trước khi cập nhật Tài khoản
        /// </summary>
        public SysAccountEntity oldzh { get; set; }


        /// <summary>
        /// Biên lai tài chính trước khi cập nhật
        /// </summary>
        public FinanceReceiptEntity oldcwsj { get; set; }


    }
    public class PatZyAccDataVO
    {

        /// <summary>
        /// Bản ghi thu chi tài khoản
        /// </summary>
        public InpatientAccountRevenueAndExpenseEntity szjl { get; set; }


        /// <summary>
        /// Trước khi cập nhật Tài khoản
        /// </summary>
        public InpatientAccountEntity oldzh { get; set; }


        /// <summary>
        /// Biên lai tài chính trước khi cập nhật
        /// </summary>
        public FinanceReceiptEntity oldcwsj { get; set; }


    }
}
