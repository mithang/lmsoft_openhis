using Newtouch.Domain.DTO;
using System;
using System.Collections.Generic;
using Newtouch.Domain.ViewModels.Outpatient;

namespace Newtouch.Application.Interface
{
    /// <summary>
    /// Quản lý tiêm
    /// </summary>
    public interface IInfusionApp
    {
        /// <summary>
        /// Đồng bộ thông tin đơn thuốc đã thanh toán
        /// </summary>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="fph"></param>
        /// <param name="kh"></param>
        /// <returns></returns>
        string SyncSettRpDetail(DateTime kssj, DateTime jssj, string fph, string kh, string mzh);

        /// <summary>
        /// Tra cứu chi tiết đơn thuốc đã thanh toán
        /// </summary>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="fph"></param>
        /// <param name="kh"></param>
        /// <returns></returns>
        List<OutpatientSettledRpQueryResponseDTO> OutpatientSettledRpQuery(DateTime kssj, DateTime jssj, string fph, string kh,string mzh);

        /// <summary>
        /// Nhóm
        /// </summary>
        /// <param name="tags">ID chi tiết truyền dịch</param>
        /// <param name="groupNo">Số nhóm</param>
        /// <returns></returns>
        string Grouping(List<long> tags, string groupNo);

        /// <summary>
        /// Bắt đầu truyền dịch
        /// </summary>
        /// <param name="tags">ID chi tiết truyền dịch</param>
        /// <param name="startDateTime">Thời gian bắt đầu</param>
        /// <param name="seatNum">Số ghế/số giường</param>
        /// <returns></returns>
        string StartInfusion(List<long> tags, DateTime startDateTime, string seatNum);

        /// <summary>
        /// Kết thúc truyền dịch
        /// </summary>
        /// <param name="tags">ID chi tiết truyền dịch</param>
        /// <param name="endDateTime">Thời gian kết thúc</param>
        /// <returns></returns>
        string EndInfusion(List<long> tags, DateTime endDateTime);

        /// <summary>
        /// Tạo số nhóm
        /// </summary>
        /// <param name="kh"></param>
        /// <returns></returns>
        string CreateGroupNo(string kh,string mzh);

        /// <summary>
        /// Đặt số ghế
        /// </summary>
        /// <param name="tag">ID đích</param>
        /// <param name="seatNum">Số ghế</param>
        /// <returns></returns>
        string SetSeatNum(long tag, string seatNum);

        /// <summary>
        /// Tra cứu thông tin truyền dịch
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<MzsyypxxVO> MzsyxxQuery(List<long> ids, string organizeId);
    }
}
