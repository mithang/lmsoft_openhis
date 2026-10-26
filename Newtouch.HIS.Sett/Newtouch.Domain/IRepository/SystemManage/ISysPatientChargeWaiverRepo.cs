using Newtouch.HIS.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.IRepository
{
    public interface ISysPatientChargeWaiverRepo : IRepositoryBase<SysPatientChargeWaiverEntity>
    {
        List<SysPatientChargeWaiverEntity> GetEffectiveList(int keyValue);
        SysPatientChargeWaiverEntity GetForm(int keyValue, string orgId);
        void DeleteForm(int keyValue, string orgId);
        void SubmitForm(SysPatientChargeWaiverEntity sysPatiChargeWaiverEntity, int? keyValue, string orgId);

        ///// <summary>
        ///// Tra cứu hạng mục thu phí theo chữ cái đầu
        ///// </summary>
        ///// <param name="parm"></param>
        ///// <returns></returns>
        // decimal GetXT_brsfjm(string strWhere);

        /// <summary>
        /// Theo tính chất bệnh nhân,Loại lớn,Số tiền giảm miễn hạng mục thu phí nhận được
        /// </summary>
        /// <param name="parmBrxz"></param>
        /// <param name="parmDl"></param>
        /// <param name="parmSfxm"></param>
        /// <param name="parmJe"></param>
        /// <param name="outJmbl"></param>
        /// <param name="outJmje"></param>
        /// <returns></returns>
        decimal Get_Calcjm(string parmBrxz, string parmDl, string parmSfxm, decimal parmJe, out decimal outJmbl, out decimal outJmje, string orgId);
    }

}
