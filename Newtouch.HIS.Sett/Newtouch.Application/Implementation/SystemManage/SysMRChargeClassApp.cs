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
    public class SysMRChargeClassApp : ISysMRChargeClassApp
    {
        private readonly ISysMedicalRecordChargeCategoryRepo _sysMRChargeClassRepo;

        public SysMRChargeClassApp(ISysMedicalRecordChargeCategoryRepo sysMRChargeClassRepo)
        {
            this._sysMRChargeClassRepo = sysMRChargeClassRepo;
        }
        /// <summary>
        /// Lấy danh sách hiệu lực
        /// </summary>
        /// <returns></returns>
        public List<SysMedicalRecordChargeCategoryEntity> GetEffectiveList(string keyValue)
        {
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                return _sysMRChargeClassRepo.IQueryable().Where(a => a.dl.Contains(keyValue) || a.dlmc.Contains(keyValue) || a.py.Contains(keyValue)).ToList();
            }
            else
            {
                return _sysMRChargeClassRepo.IQueryable().ToList();
            }
            
        }
        /// <summary>
        /// Thêm mới hoặc sửaForm
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public SysMedicalRecordChargeCategoryEntity GetForm(int keyValue)
        {
            return _sysMRChargeClassRepo.FindEntity(keyValue);
        }
        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(SysMedicalRecordChargeCategoryEntity sysMRChargeClassEntity)
        {
            _sysMRChargeClassRepo.DeleteForm(sysMRChargeClassEntity);
        }
        /// <summary>
        /// Lưu
        /// </summary>
        /// <param name="sysMRChargeClassEntity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysMedicalRecordChargeCategoryEntity sysMRChargeClassEntity, int? keyValue)
        {
            _sysMRChargeClassRepo.SubmitForm(sysMRChargeClassEntity, keyValue);
        }
        /// <summary>
        /// Lấy tất cả các hồ sơ bệnh án thu phí hộp thả xuống loại lớn 
        /// </summary>
        /// <returns></returns>
        public object GetdlSelect()
        {
            var orgId = OperatorProvider.GetCurrent().OrganizeId;
            return _sysMRChargeClassRepo.GetdlSelect(orgId);
        }
    }
}
