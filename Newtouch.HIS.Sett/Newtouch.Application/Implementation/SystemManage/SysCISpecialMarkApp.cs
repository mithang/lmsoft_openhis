/*******************************************************************************
 * Copyright © 2016 Newtouch Giữ bản quyền
 * Author: Newtouch
 * Description: 
*********************************************************************************/
using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects.SystemManage;
using Newtouch.HIS.Domain.IDomainServices.SystemManage;
using Newtouch.Core.Common;
using FrameworkBase.MultiOrg.Application;

namespace Newtouch.HIS.Application
{
    /// <summary>
    /// 
    /// </summary>
    public class SysCISpecialMarkApp : AppBase, ISysCISpecialMarkApp
    {
        private readonly ISysChargeItemSpecialMarkRepo _xt_sfxmtsbzRepository;
        private readonly ISysCISpecialMarkDmnService _SysCISpecialMarkDmnService;

        /// <summary>
        /// Atrang mang một đ ối tư ợng sangBtrang
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public SysCISpecialMarkVO GetForm(int keyValue)
        {
            return _SysCISpecialMarkDmnService.GetFormJson(keyValue);
        }
        public void DeleteForm(int keyValue)
        {
            _xt_sfxmtsbzRepository.Delete(t => t.sfxmtsbzbh == keyValue);
        }
        public void SubmitForm(SysChargeItemSpecialMarkEntity xt_sfxmtsbzEntity, string keyValue)
        {
            _xt_sfxmtsbzRepository.submitform(xt_sfxmtsbzEntity, keyValue);
        }

        /// <summary>
        /// GridHiển thị tra cứu lọc
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<SysCISpecialMarkVO> GetListBySearch(Pagination pagination, string keyword)
        {

            return _SysCISpecialMarkDmnService.GetGridBySearch(pagination, keyword);
        }
    }
}
