using NewtouchHIS.Lib.Base.Model;
using Newtouch.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.Domain.IDomainServices
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPrescriptionDmnService
    {
        /// <summary>
        /// lịch sửĐơn thuốccâychi tiết
        /// </summary>
        /// <param name="cfId"></param>
        IList<HistoryPresFormSelectVO> GetHistoryPresDetailByCfId(string cfId);

        /// <summary>
        /// TheocfIdhoặccfmxIdStrTra cứuchi tiết
        /// </summary>
        /// <param name="cflx"></param>
        /// <param name="cfId"></param>
        /// <param name="cfmxIdStr"></param>
        /// <returns></returns>
        List<PrescriptionDetailQueryVO> GetPresDetailList(int cflx, string cfId, string cfmxIdStr);

        /// <summary>
        /// Bảo hiểm y tếBệnh nhânPhê duyệtMục thu phílàKhôngchứatự chi trả
        /// </summary>
        /// <param name="sfxmCode"></param>
        /// <param name="cflx"></param>
        /// <returns></returns>
        List<string> ValidateMedicalInsurance(Dictionary<int, string> dic, string orgId);

		/// <summary>
		/// mởlậpvật tưMụcđôngkếtTồn khoSố lượng
		/// </summary>
		/// <param name="cfh"></param>
		/// <param name="orgId"></param>
		/// <returns></returns>
		int Sumwzdj(string[] cfh,string orgId,string rygh);

		/// <summary>
		/// mởlậpvật tưMụcđôngkếtTồn khoSố lượngHủy bỏ
		/// </summary>
		/// <param name="cfh"></param>
		/// <param name="orgId"></param>
		/// <returns></returns>
		int ZUOFwzdj(string cfh, string orgId, string rygh);

        /// <summary>
        /// Bảo hiểm y tếđiện tửĐơn thuốc
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<string> getdzcfnrxs(string cfh, string orgId);
    }
}
