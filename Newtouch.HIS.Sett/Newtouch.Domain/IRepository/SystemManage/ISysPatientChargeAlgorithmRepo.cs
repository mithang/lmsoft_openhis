/*******************************************************************************
 * Copyright © 2016 Newtouch Bảo lưu bản quyền
 * Author: Newtouch
 * Description: 

*********************************************************************************/
using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;
using Newtouch.HIS.Domain.ValueObjects;

namespace Newtouch.HIS.Domain.IRepository
{
    public interface ISysPatientChargeAlgorithmRepo : IRepositoryBase<SysPatientChargeAlgorithmEntity>
    {
        /// <summary>
        /// Lấy tất cả thuật toán hiệu lực
        /// </summary>
        /// <returns></returns>
        List<SysPatientChargeAlgorithmEntity> getAllMzActive();
        void SubmitForm(SysPatientChargeAlgorithmEntity entity, string keyValue);

        List<ChargeItemDetailVO> GetSFXMItemInfoByDlCode(string keyword, string dlCode, string orgId);
    }
}
