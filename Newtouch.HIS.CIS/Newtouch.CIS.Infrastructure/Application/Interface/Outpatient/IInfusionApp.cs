using Newtouch.Domain.DTO;
using System;
using Newtouch.CIS.Compat;
using System.Collections.Generic;
using Newtouch.Domain.ViewModels.Outpatient;

namespace Newtouch.Application.Interface
{
    /// <summary>
    /// Lưu ýxạQuản lý
    /// </summary>
    public interface IInfusionApp
    {
        /// <summary>
        /// Đồng bộđãquyết toánĐơn thuốcthông tin
        /// </summary>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="fph"></param>
        /// <param name="kh"></param>
        /// <returns></returns>
        string SyncSettRpDetail(DateTime kssj, DateTime jssj, string fph, string kh, string mzh);

        /// <summary>
        /// đãquyết toánĐơn thuốcchi tiếtTra cứu
        /// </summary>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="fph"></param>
        /// <param name="kh"></param>
        /// <returns></returns>
        List<OutpatientSettledRpQueryResponseDTO> OutpatientSettledRpQuery(DateTime kssj, DateTime jssj, string fph, string kh,string mzh);

        /// <summary>
        /// phútnhóm
        /// </summary>
        /// <param name="tags">truyền dịchchi tiếtID</param>
        /// <param name="groupNo">Số nhóm</param>
        /// <returns></returns>
        string Grouping(List<long> tags, string groupNo);

        /// <summary>
        /// truyền dịchBắt đầu
        /// </summary>
        /// <param name="tags">truyền dịchchi tiếtID</param>
        /// <param name="startDateTime">Thời gian bắt đầu</param>
        /// <param name="seatNum">ghếsố/Số giường</param>
        /// <returns></returns>
        string StartInfusion(List<long> tags, DateTime startDateTime, string seatNum);

        /// <summary>
        /// truyền dịchKết thúc
        /// </summary>
        /// <param name="tags">truyền dịchchi tiếtID</param>
        /// <param name="endDateTime">Thời gian kết thúc</param>
        /// <returns></returns>
        string EndInfusion(List<long> tags, DateTime endDateTime);

        /// <summary>
        /// tạoSố nhóm
        /// </summary>
        /// <param name="kh"></param>
        /// <returns></returns>
        string CreateGroupNo(string kh,string mzh);

        /// <summary>
        /// Cài đặtghế ký tựsố
        /// </summary>
        /// <param name="tag">Mục tiêuID</param>
        /// <param name="seatNum">ghế ký tựsố</param>
        /// <returns></returns>
        string SetSeatNum(long tag, string seatNum);

        /// <summary>
        /// Tra cứutruyền dịchthông tin
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<MzsyypxxVO> MzsyxxQuery(List<long> ids, string organizeId);
    }
}
