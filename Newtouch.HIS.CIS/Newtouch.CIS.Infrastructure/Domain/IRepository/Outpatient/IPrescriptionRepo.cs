using System.Collections.Generic;
using Newtouch.CIS.APIRequest.Prescription;
using Newtouch.Domain.Entity;
using Newtouch.CIS.Repository;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPrescriptionRepo : IRepositoryBase<PrescriptionEntity>
    {
        /// <summary>
        /// hơn Đơn thuốc mới Cờ thu phívà Đồng bộ Cờ
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="tbbz"></param>
        /// <param name="sfbz"></param>
        /// <param name="orgId"></param>
        /// <param name="account"></param>
        void UpdateChargeStatus(string cfh, bool tbbz, bool? sfbz, string orgId, string account);

        /// <summary>
        /// hơn Đơn thuốc mới Cờ thu phí
        /// </summary>
        /// <param name="part"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        void UpdateChargeStatus(PrescriptionChargeRequest part, string organizeId, string userCode);

        /// <summary>
        /// hơn Đơn thuốc mới Cờ thu phí
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="cfh"></param>
        /// <param name="sfbz"></param>
        /// <param name="account"></param>
        void UpdateChargeStatus(string orgId, string cfh, bool sfbz, string account);

        /// <summary>
        /// hơn Đơn thuốc mớitrả Cờ
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="cfh"></param>
        /// <param name="tbz"></param>
        /// <param name="account"></param>
        void UpdateChargeTbz(string orgId, string cfh, bool tbz, string account);

        /// <summary>
        /// Lấy Đơn thuốcthông tin
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        PrescriptionEntity FindListByCfh(string cfh, string orgId);

        /// <summary>
        /// Lấy Đơn thuốcthông tin
        /// </summary>
        /// <param name="jzId"></param>
        /// <param name="sfbz"></param>
        /// <returns></returns>
        List<PrescriptionEntity> FindList(string jzId, bool sfbz);
    }
}
