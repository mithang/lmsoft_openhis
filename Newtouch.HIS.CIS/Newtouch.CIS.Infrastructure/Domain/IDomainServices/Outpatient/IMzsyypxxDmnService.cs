using NewtouchHIS.Lib.Base.Model;
using Newtouch.Domain.Entity;
using System;
using System.Collections.Generic;
using Newtouch.Domain.DTO;
using Newtouch.Domain.ViewModels.Outpatient;
using Newtouch.Domain.ViewModels;

namespace Newtouch.Domain.IDomainServices
{
    /// <summary>
    /// Ngoại trú truyền dịch Thuốc thông tin
    /// </summary>
    public interface IMzsyypxxDmnService
    {

        /// <summary>
        /// Theo Số thẻ Lấy truyền dịch thông tin
        /// </summary>
        /// <param name="kh"></param>
        /// <param name="organizeId"></param>
        /// <param name="isEnd">true:đã Kết thúc truyền dịch;false:chưa Kết thúc truyền dịch</param>
        /// <returns></returns>
        IList<MzsyypxxVO> SelectMzsyypxxByKh(Pagination pagination, string kh,string mzh, string organizeId, bool isEnd);

        /// <summary>
        /// Theo Số thẻ Lấy truyền dịch thông tin
        /// </summary>
        /// <param name="id"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        MzsyypxxVO SelectMzsyypxxById(long id, string organizeId);

        /// <summary>
        /// chưa Kết thúc của truyền dịch Bệnh nhân
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="kh"></param>
        /// <param name="fph"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="organizeId"></param>
        /// <param name="isEnd">là Không Kết thúc  false:truyền dịch chưa Kết thúc  true:truyền dịch đã Kết thúc</param>
        /// <returns></returns>
        IList<MzsyhzxxVO> SelectKhAndXm(Pagination pagination, string kh,string mzh, string fph, DateTime kssj, DateTime jssj, string organizeId, bool isEnd = false);

        /// <summary>
        /// Chèn mới truyền dịch thông tin
        /// </summary>
        /// <param name="item"></param>
        /// <param name="organizeId"></param>
        /// <param name="creatorCode"></param>
        /// <returns></returns>
        int InsertNewMzsyypxx(OutpatientSettledRpQueryResponseDTO item, string organizeId, string creatorCode);

		/// <summary>
		/// đã quyết toán Đơn thuốc chi tiết Tra cứu
		/// </summary>
		/// <param name="organizeId"></param>
		/// <param name="kssj"></param>
		/// <param name="jssj"></param>
		/// <param name="fph"></param>
		/// <param name="kh"></param>
		/// <param name="yfCode">Cách dùng Mã</param>
		/// <returns></returns>
		List<OutpatientSettledRpQueryResponseDTO> OutpatientSettledRpQuery(string organizeId, DateTime kssj, DateTime jssj, string fph, string kh, string yfCode, string mzh);
		/// <summary>
		/// Lấy tất cả Số nhóm
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
