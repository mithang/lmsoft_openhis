using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.ValueObjects;
using FrameworkBase.MultiOrg.Application;

namespace Newtouch.HIS.Application
{
    /// <summary>
    /// 
    /// </summary>
    public class SysPatiChargeWaiverApp : AppBase, ISysPatiChargeWaiverApp
    {
        private readonly ISysPatientChargeWaiverRepo _sysPatiChargeWaiverRepo;
        private readonly ISysFeeDmnService _sysFeeDmnService;

        /// <summary>
        /// Lấy danh sách hiệu lực
        /// </summary>
        /// <returns></returns>
        public List<SysPatiChargeWaiverVo> GetEffectiveList(string keyValue, int? bh = null)
        {
            return _sysFeeDmnService.GetSysPatiChargeWaiverList(keyValue, bh);
        }

        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(int keyValue)
        {
            _sysPatiChargeWaiverRepo.DeleteForm(keyValue, this.OrganizeId);
        }
        /// <summary>
        /// Lưu
        /// </summary>
        /// <param name="sysPatiChargeWaiverEntity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysPatientChargeWaiverEntity sysPatiChargeWaiverEntity, int? keyValue)
        {
            _sysPatiChargeWaiverRepo.SubmitForm(sysPatiChargeWaiverEntity, keyValue, this.OrganizeId);
        }

        /// <summary>
        /// Tính hạng mục thu phí số tiền giảm trừ
        /// Bệnh nhânTrạng thái:1;--Hiệu lực
        /// thay đ ổi dấu hiệu:0;--chưa thay đ ổi 
        /// </summary>
        /// <param name="parmBrxz">Tính chất bệnh nhân</param>
        /// <param name="parmDl"> loại lớn </param>
        /// <param name="parmSfxm">Hạng mục thu phí</param>
        /// <param name="parmJe">Số tiền</param>
        /// <param name="outJmbl">Tỷ lệ giảm trừ</param>
        /// <param name="outJmje">Số tiền giảm trừ</param>
        /// <returns>count:Số tiền sau giảm trừ</returns>
        public decimal Calcjm(string parmBrxz, string parmDl, string parmSfxm, decimal parmJe, out decimal outJmbl, out decimal outJmje)
        {
            outJmbl = 0;
            outJmje = 0;
            return _sysPatiChargeWaiverRepo.Get_Calcjm(parmBrxz, parmDl, parmSfxm,parmJe,out outJmbl,out outJmje, this.OrganizeId);
        }
    }
}
