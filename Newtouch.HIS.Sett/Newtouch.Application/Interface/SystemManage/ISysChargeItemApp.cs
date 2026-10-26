/*******************************************************************************
 * Copyright © 2016 Newtouch Giữ bản quyền
 * Author: Newtouch
 * Description: 
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Data;
using Newtouch.HIS.Domain.Entity;
using Newtouch.Tools;
using Newtouch.HIS.Domain.ValueObjects.SystemManage;
using Newtouch.Common;

namespace Newtouch.HIS.Application
{
    public interface ISysChargeItemApp
    {

        SysChargeItemVO GetForm(int keyValue);
        void DeleteForm(int keyValue);
        void SubmitForm(SysChargeItemEntity xt_sfxmEntity, string keyValue);

        List<SysChargeItemVO> GetsfxmBySearch(Pagination Pagination, string keyword);
    }
}
