/*******************************************************************************
 * Copyright © 2016 Newtouch Giữ bản quyền
 * Author: Newtouch
 * Description: 
*********************************************************************************/
using Newtouch.Tools;
using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using System.Linq;
using Newtouch.Common.Operator;

namespace Newtouch.HIS.Application
{
    public class SysAgInsurChargeCategApp : ISysAgInsurChargeCategApp
    {
        private readonly ISysAgriInsuranceChargeCategoryRepo _sysAgInsurChargeCategRepo;

        public SysAgInsurChargeCategApp(ISysAgriInsuranceChargeCategoryRepo sysAgInsurChargeCategRepo)
        {
            this._sysAgInsurChargeCategRepo = sysAgInsurChargeCategRepo;
        }
        /// <summary>
        /// Lấy danh sách hiệu lực
        /// </summary>
        /// <returns></returns>
        public List<SysAgriInsuranceChargeCategoryEntity> GetEffectiveList(int keyValue)
        {
            return _sysAgInsurChargeCategRepo.GetEffectiveList(keyValue);
        }
        /// <summary>
        /// Thêm mới hoặc sửaForm
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public SysAgriInsuranceChargeCategoryEntity GetForm(int keyValue)
        {
            return _sysAgInsurChargeCategRepo.GetForm(keyValue);
        }
        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(int keyValue)
        {
            _sysAgInsurChargeCategRepo.DeleteForm(keyValue);
        }
        /// <summary>
        /// Lưu
        /// </summary>
        /// <param name="sysAgInsurChargeCategEntity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysAgriInsuranceChargeCategoryEntity sysAgInsurChargeCategEntity, int keyValue)
        {
            _sysAgInsurChargeCategRepo.SubmitForm(sysAgInsurChargeCategEntity, keyValue);
        }
        /// <summary>
        /// Lấy tất cả các bảo hiểm nông nghiệp thu phí hộp thả xuống loại lớn 
        /// </summary>
        /// <returns></returns>
        public object GetnbdlSelect()
        {
            var orgId = OperatorProvider.GetCurrent().OrganizeId;
            return _sysAgInsurChargeCategRepo.GetnbdlSelect(orgId);
        }
    }
}
