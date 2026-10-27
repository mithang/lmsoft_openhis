using System.Collections.Generic;
using Newtouch.HIS.Domain.ValueObjects;

namespace Newtouch.HIS.Application.Interface
{
    /// <summary>
    /// Phát thuốc ngoại trú
    /// </summary>
    public interface IOutPatientDispensingApp
    {
        /// <summary>
        /// Thực hiện phát thuốc
        /// </summary>
        /// <param name="patients"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="userCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="cfhs">Số đơn phát thuốc</param>
        /// <returns></returns>
        string ExecAllDeliveryDrug(patientInfoVO[] patients, string yfbmCode, string userCode, string organizeId, out List<string> cfhs);

        /// <summary>
        /// Thực hiện phát thuốc
        /// </summary>
        /// <param name="deliveryInfo"></param>
        /// <param name="cfhList">Số đơn phát thuốc</param>
        /// <returns></returns>
        string ExecAllDeliveryDrugV2(OutpatienDrugDeliveryInfo deliveryInfo, out List<string> cfhList);
    }
}