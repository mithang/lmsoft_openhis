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
    /// Ngoại trútruyền dịchThuốcthông tin
    /// </summary>
    public interface IMzsyypxxDmnService
    {

        /// <summary>
        /// TheoSố thẻLấytruyền dịchthông tin
        /// </summary>
        /// <param name="kh"></param>
        /// <param name="organizeId"></param>
        /// <param name="isEnd">true：đãKết thúctruyền dịch；false：chưaKết thúctruyền dịch</param>
        /// <returns></returns>
        IList<MzsyypxxVO> SelectMzsyypxxByKh(Pagination pagination, string kh,string mzh, string organizeId, bool isEnd);

        /// <summary>
        /// TheoSố thẻLấytruyền dịchthông tin
        /// </summary>
        /// <param name="id"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        MzsyypxxVO SelectMzsyypxxById(long id, string organizeId);

        /// <summary>
        /// chưaKết thúccủatruyền dịchBệnh nhân
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="kh"></param>
        /// <param name="fph"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="organizeId"></param>
        /// <param name="isEnd">làKhôngKết thúc  false：truyền dịchchưaKết thúc  true：truyền dịchđãKết thúc</param>
        /// <returns></returns>
        IList<MzsyhzxxVO> SelectKhAndXm(Pagination pagination, string kh,string mzh, string fph, DateTime kssj, DateTime jssj, string organizeId, bool isEnd = false);

        /// <summary>
        /// Chènmớitruyền dịchthông tin
        /// </summary>
        /// <param name="item"></param>
        /// <param name="organizeId"></param>
        /// <param name="creatorCode"></param>
        /// <returns></returns>
        int InsertNewMzsyypxx(OutpatientSettledRpQueryResponseDTO item, string organizeId, string creatorCode);

		/// <summary>
		/// đãquyết toánĐơn thuốcchi tiếtTra cứu
		/// </summary>
		/// <param name="organizeId"></param>
		/// <param name="kssj"></param>
		/// <param name="jssj"></param>
		/// <param name="fph"></param>
		/// <param name="kh"></param>
		/// <param name="yfCode">Cách dùngMã</param>
		/// <returns></returns>
		List<OutpatientSettledRpQueryResponseDTO> OutpatientSettledRpQuery(string organizeId, DateTime kssj, DateTime jssj, string fph, string kh, string yfCode, string mzh);
		/// <summary>
		/// Lấytất cảSố nhóm
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
