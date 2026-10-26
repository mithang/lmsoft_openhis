/*******************************************************************************
 * Copyright © 2016 Newtouch Giữ bản quyền
 * Author: Newtouch
 * Description: 
*********************************************************************************/
using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Application
{
    public interface ISysAgInsurChargeCategApp
    {
        List<SysAgriInsuranceChargeCategoryEntity> GetEffectiveList(int keyValue);
        SysAgriInsuranceChargeCategoryEntity GetForm(int keyValue);
        void DeleteForm(int keyValue);
        void SubmitForm(SysAgriInsuranceChargeCategoryEntity sysAgInsurChargeCategEntity, int keyValue);

        /// <summary>
        /// Lấy tất cả các bảo hiểm nông nghiệp thu phí hộp thả xuống loại lớn 
        /// </summary>
        /// <returns></returns>
        object GetnbdlSelect();
    }
}
