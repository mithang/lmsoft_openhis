/*******************************************************************************
 * Copyright © 2016 Newtouch Giữ bản quyền
 * Author: Newtouch
 * Description: 
*********************************************************************************/

using System.Collections.Generic;
using Newtouch.HIS.Domain.ValueObjects.SystemManage;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Core.Common;

namespace Newtouch.HIS.Application
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysPatiChargeLogicApp
    {

        List<PatiChargeLogicVO> GetList(Pagination pagination, string keyword,string orgId);
        PatiChargeLogicVO GetForm(string keyValue);
        void DeleteForm(int keyValue);

        void SubmitForm(PatiChargeLogicVO PatiChargeLogicVO, string keyValue,string orgId);

        List<ChargeItemDetailVO> GetSFXMItemInfoByDlCode(string keyword, string dlCode, string orgId);
    }
}
