/**********************************************************/
// Copyright (C) 2016 Newtouch Bảo lưu bản quyền. 
// Description:Nạp tiền tài khoản và hoàn phí  Thực thể xử lý sự vụ 
// Author:HLF
// CreateDate: 2017/1/11 16:34:07 
//**********************************************************/

using Newtouch.HIS.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects
{
    public class HosPatZFToXNHVO
    {

       public string xnhgrbm { get; set; }
       public string xnhylzh { get; set; }
       public string xm { get; set; }
       public string xb { get; set; }
       public string brxzmc { get; set; }
       public string brxzcode { get; set; }
       public string zjh { get; set; }
       public string xian_dz { get; set; }
       public string zhye { get; set; }

    }
    
}
