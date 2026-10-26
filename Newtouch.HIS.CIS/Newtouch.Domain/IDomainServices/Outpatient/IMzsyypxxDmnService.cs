using Newtouch.Core.Common;
using Newtouch.Domain.Entity;
using System;
using System.Collections.Generic;
using Newtouch.Domain.DTO;
using Newtouch.Domain.ViewModels.Outpatient;
using Newtouch.Domain.ViewModels;

namespace Newtouch.Domain.IDomainServices
{
    /// <summary>
    /// Thông tin thuốc truyền dịch ngoại trú
    /// </summary>
    public interface IMzsyypxxDmnService
    {

        /// <summary>
        /// Lấy thông tin truyền dịch theo số thẻ
        /// </summary>
        /// <param name="kh"></param>
        /// <param name="organizeId"></param>
        /// <param name="isEnd">true:Đã kết thúc truyền dịch;false:Chưa kết thúc truyền dịch</param>
        /// <returns></returns>
        IList<MzsyypxxVO> SelectMzsyypxxByKh(Pagination pagination, string kh,string mzh, string organizeId, bool isEnd);

        /// <summary>
        /// Lấy thông tin truyền dịch theo số thẻ
        /// </summary>
        /// <param name="id"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        MzsyypxxVO SelectMzsyypxxById(long id, string organizeId);

        /// <summary>
        /// Bệnh nhân truyền dịch chưa kết thúc
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="kh"></param>
        /// <param name="fph"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="organizeId"></param>
        /// <param name="isEnd">Có kết thúc hay không  false:Truyền dịch chưa kết thúc  true:Truyền dịch đã kết thúc</param>
        /// <returns></returns>
        IList<MzsyhzxxVO> SelectKhAndXm(Pagination pagination, string kh,string mzh, string fph, DateTime kssj, DateTime jssj, string organizeId, bool isEnd = false);

        /// <summary>
        /// Chèn thông tin truyền dịch mới
        /// </summary>
        /// <param name="item"></param>
        /// <param name="organizeId"></param>
        /// <param name="creatorCode"></param>
        /// <returns></returns>
        int InsertNewMzsyypxx(OutpatientSettledRpQueryResponseDTO item, string organizeId, string creatorCode);

		/// <summary>
		/// Tra cứu chi tiết đơn thuốc đã quyết toán
		/// </summary>
		/// <param name="organizeId"></param>
		/// <param name="kssj"></param>
		/// <param name="jssj"></param>
		/// <param name="fph"></param>
		/// <param name="kh"></param>
		/// <param name="yfCode">Mã cách dùng</param>
		/// <returns></returns>
		List<OutpatientSettledRpQueryResponseDTO> OutpatientSettledRpQuery(string organizeId, DateTime kssj, DateTime jssj, string fph, string kh, string yfCode, string mzh);
		/// <summary>
		/// Lấy tất cả số nhóm
		/// </summary>
		/// <param name="kh"></param>
		/// <param name="fph"></param>
		/// <param name="kssj"></param>
		/// <param name="jssj"></param>
		/// <param name="organizeId"></param>
		/// <returns></returns>
		IList<string> SelectAllGroupNoByKh(string kh, string organizeId,string mzh);
    }
}
