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
using Newtouch.Common;

namespace Newtouch.HIS.Application
{
    public interface ISysChargeMajorClassApp
    {

        /// <summary>
        /// Atrang mang một đ ối tư ợng sangBtrang
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        SysChargeCategoryEntity GetForm(int keyValue);

        void DeleteForm(int keyValue);
        void SubmitForm(SysChargeCategoryEntity xt_sfdlEntity, string keyValue);

        /// <summary>
        /// Thu phí hộp thả xuống loại lớn 
        /// </summary>
        /// <returns></returns>
        string GetsfdlTreeSelectJson();
    }
}
