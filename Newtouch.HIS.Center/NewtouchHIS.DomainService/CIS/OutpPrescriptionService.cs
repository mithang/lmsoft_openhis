using Mapster;
using NewtouchHIS.Base.DomainService;
using NewtouchHIS.Domain.Entity.CIS;
using NewtouchHIS.Domain.IDomainService.CIS;
using NewtouchHIS.Domain.InterfaceObjets.CIS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.DomainService.CIS
{
    /// <summary>
    /// Thông tin đơn thuốc ngoại trú
    /// </summary>
    public class OutpPrescriptionService : BaseServices<PrescriptionEntity>, IOutpPrescriptionService
    {
        /// <summary>
        /// Lấy danh sách đơn thuốc của một lần khám by jzId
        /// </summary>
        /// <param name="jzId"></param>
        /// <returns></returns>
        public async Task<List<PrescriptionEntity>> GetPrescriptionbyJzId(string jzId)
        {
            return await baseDal.GetByWhere(p => p.jzId == jzId && p.zt == "1");
        }

        public async Task<List<OutpPrescriptionDataVO>> GetPresbyJzId(string jzId)
        {
            var presList = await baseDal.GetByWhere(p => p.jzId == jzId && p.zt == "1");
            return presList.Adapt<List<OutpPrescriptionDataVO>>();
        }
        /// <summary>
        /// LấyBệnh nhânThông tin đơn thuốc trong bệnh án by jzId
        /// </summary>
        /// <param name="jzId"></param>
        /// <returns></returns>
        public async Task<List<OutpPrescriptionDataVO>> GetPresDatabyJzId(string jzId)
        {
            var presList = await baseDal.GetByWhere(p => p.jzId == jzId && p.zt == "1");
            if (presList?.Count > 0)
            {
                var presIds = presList.Select(p => p.cfId).ToList();
                var detail = await baseDal.GetByWhereWithAttr<PrescriptionDetailEntity>(p => presIds.Contains(p.cfId) && p.zt == "1");
                var presData = presList.Adapt<List<OutpPrescriptionDataVO>>();
                foreach (var pres in presData)
                {
                    pres.cfmx = detail.Where(p => p.cfId == pres.cfId).Adapt<List<OutpPrescriptionDetailVO>>();
                }
                return presData;
            }
            return presList.Adapt<List<OutpPrescriptionDataVO>>();
        }

    }
}
