using Newtouch.Core.Common;
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
        /// Chi tiết cây đơn thuốc lịch sử
        /// </summary>
        /// <param name="cfId"></param>
        IList<HistoryPresFormSelectVO> GetHistoryPresDetailByCfId(string cfId);

        /// <summary>
        /// TheocfIdhoặccfmxIdStrTra cứu chi tiết
        /// </summary>
        /// <param name="cflx"></param>
        /// <param name="cfId"></param>
        /// <param name="cfmxIdStr"></param>
        /// <returns></returns>
        List<PrescriptionDetailQueryVO> GetPresDetailList(int cflx, string cfId, string cfmxIdStr);

        /// <summary>
        /// Bệnh nhân BHYT duyệt hạng mục thu phí có chứa tự chi trả hay không
        /// </summary>
        /// <param name="sfxmCode"></param>
        /// <param name="cflx"></param>
        /// <returns></returns>
        List<string> ValidateMedicalInsurance(Dictionary<int, string> dic, string orgId);

		/// <summary>
		/// Đóng băng số lượng tồn kho khi kê hạng mục vật tư
		/// </summary>
		/// <param name="cfh"></param>
		/// <param name="orgId"></param>
		/// <returns></returns>
		int Sumwzdj(string[] cfh,string orgId,string rygh);

		/// <summary>
		/// Hủy đóng băng số lượng tồn kho khi kê hạng mục vật tư
		/// </summary>
		/// <param name="cfh"></param>
		/// <param name="orgId"></param>
		/// <returns></returns>
		int ZUOFwzdj(string cfh, string orgId, string rygh);

        /// <summary>
        /// Đơn thuốc điện tử BHYT
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<string> getdzcfnrxs(string cfh, string orgId);
    }
}
