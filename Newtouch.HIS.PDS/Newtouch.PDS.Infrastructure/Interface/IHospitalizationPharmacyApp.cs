using System.Collections.Generic;
using Newtouch.HIS.Domain.ValueObjects;

namespace Newtouch.HIS.Application.Interface
{
    /// <summary>
    /// Thao tác nhà thuốc nội trú
    /// </summary>
    public interface IHospitalizationPharmacyApp
    {
        /// <summary>
        /// Phát thuốc nội trú
        /// </summary>
        /// <param name="drugsParam"></param>
        /// <param name="userCode"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="fyid"></param>
        /// <returns></returns>
        string HospitalizationDispensing(List<DispensingDrugsParam> drugsParam, string userCode, string yfbmCode, string organizeId,out string fyid);
    }
}