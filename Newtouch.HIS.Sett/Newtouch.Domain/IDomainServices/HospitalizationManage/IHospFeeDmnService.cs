using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// Tính phí nội trú DmnService
    /// </summary>
    public interface IHospFeeDmnService
    {
        /// <summary>
        /// Lấy tính phí hạng mục nội trú theo số nhập viện của Thông tin chi tiết Danh sách(Tất cả)
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        IList<HospItemFeeDetailVO> GetAllItemFeeDetailVOList(string zyh, string orgId);

        /// <summary>
        /// Lấy tính phí hạng mục nội trú theo số nhập viện của Thông tin chi tiết Danh sách(Bao gồm hoàn phí)
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        IList<HospItemFeeDetailVO> GetItemFeeDetailVOList(string zyh, string orgId);

        /// <summary>
        /// Lấy tính phí thuốc nội trú theo số nhập viện của Thông tin chi tiết Danh sách(Tất cả)
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        IList<HospMedicinFeeDetailVO> GetAllMedicinFeeDetailVOList(string zyh, string orgId);

        /// <summary>
        /// Lấy tính phí thuốc nội trú theo số nhập viện của Thông tin chi tiết Danh sách(Bao gồm hoàn phí)
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        IList<HospItemFeeDetailVO> GetMedicinFeeDetailVOList(string zyh, string orgId);
        /// <summary>
        /// Đồng bộ mới nhấtCPOEChi phí hạng mục
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="zyh"></param>
        void SyncPatFee(string orgId, string zyh,int zxtype);

        /// <summary>
        /// checkCó hay không Phát sinh chi phí không liên quan BHYT
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        bool CheckHasNonYbFee(string zyh, string orgId);
    }

}
