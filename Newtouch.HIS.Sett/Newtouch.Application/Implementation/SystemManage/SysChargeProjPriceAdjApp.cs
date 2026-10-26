using FrameworkBase.MultiOrg.Application;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using System.Collections.Generic;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// 
    /// </summary>
    public class SysChargeProjPriceAdjApp : AppBase, ISysChargeProjPriceAdjApp
    {
        private readonly ISysChargeItemPriceAdjustRepo _sysChargeProjPriceAdjRepo;
        //other Repositories or DomainServices

        /// <summary>
        /// Lấy danh sách hiệu lực
        /// </summary>
        /// <returns></returns>
        public List<SysChargeItemPriceAdjustEntity> GetEffectiveList(string keyValue)
        {
            return _sysChargeProjPriceAdjRepo.GetEffectiveList(keyValue, this.OrganizeId);
        }
        /// <summary>
        /// Thêm mới hoặc sửaForm
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public SysChargeItemPriceAdjustEntity GetForm(int? keyValue)
        {
            return _sysChargeProjPriceAdjRepo.GetForm(keyValue, this.OrganizeId);
        }
        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(SysChargeItemPriceAdjustEntity sysChargeProjPriceAdjEntity)
        {
            _sysChargeProjPriceAdjRepo.DeleteForm(sysChargeProjPriceAdjEntity, this.OrganizeId);
        }
        /// <summary>
        /// Lưu
        /// </summary>
        /// <param name="sysChargeProjPriceAdjEntity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysChargeItemPriceAdjustEntity sysChargeProjPriceAdjEntity, int? keyValue)
        {
            _sysChargeProjPriceAdjRepo.SubmitForm(sysChargeProjPriceAdjEntity, keyValue, this.OrganizeId);
        }


    }
}
