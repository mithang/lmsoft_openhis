/*******************************************************************************
 * Copyright © 2016 Newtouch Giữ bản quyền
 * Author: Newtouch
 * Description: 
*********************************************************************************/
using System;
using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;
using Newtouch.Tools;
using Newtouch.HIS.Domain.ValueObjects.SystemManage;
using Newtouch.Common;

namespace Newtouch.HIS.Application
{
    public interface ISysCIRemindContentApp
    {
        /// <summary>
        /// Thu phí loại lớn tất cả các dữ liệu
        /// </summary>
        /// <returns></returns>
        List<SysChargeItemWarningContentEntity> GetList();

        /// <summary>
        /// Atrang mang một đ ối tư ợng sangBtrang
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        SysCIRemindContentVO GetForm(int keyValue);
        void DeleteForm(int keyValue);
        void SubmitForm(SysChargeItemWarningContentEntity xt_sfxmjsnrEntity, string keyValue);

        /// <summary>
        /// Lấy tất cả các thu phí hộp thả xuống loại lớn 
        /// </summary>
        /// <returns></returns>
        string GetdlSelect();

        /// <summary>
        /// GridHiển thị tra cứu lọc
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        List<SysCIRemindContentVO> GetListBySearch(Pagination pagination, string keyword);
    }
}
