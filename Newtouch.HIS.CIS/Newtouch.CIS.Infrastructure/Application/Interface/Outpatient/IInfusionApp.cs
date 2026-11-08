using Newtouch.Domain.DTO;
using System;
using Newtouch.CIS.Compat;
using System.Collections.Generic;
using Newtouch.Domain.ViewModels.Outpatient;

namespace Newtouch.Application.Interface
{
    /// <summary>
    /// Lưu ýxạ Quản lý
    /// </summary>
    public interface IInfusionApp
    {
        /// <summary>
        /// Đồng bộ đã quyết toán Đơn thuốc thông tin
        /// </summary>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="fph"></param>
        /// <param name="kh"></param>
        /// <returns></returns>
        string SyncSettRpDetail(DateTime kssj, DateTime jssj, string fph, string kh, string mzh);

        /// <summary>
        /// đã quyết toán Đơn thuốc chi tiết Tra cứu
        /// </summary>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="fph"></param>
        /// <param name="kh"></param>
        /// <returns></returns>
        List<OutpatientSettledRpQueryResponseDTO> OutpatientSettledRpQuery(DateTime kssj, DateTime jssj, string fph, string kh,string mzh);

        /// <summary>
        /// phút nhóm
        /// </summary>
        /// <param name="tags">truyền dịch chi tiết ID</param>
        /// <param name="groupNo">Số nhóm</param>
        /// <returns></returns>
        string Grouping(List<long> tags, string groupNo);

        /// <summary>
        /// truyền dịch Bắt đầu
        /// </summary>
        /// <param name="tags">truyền dịch chi tiết ID</param>
        /// <param name="startDateTime">Thời gian bắt đầu</param>
        /// <param name="seatNum">ghế số/Số giường</param>
        /// <returns></returns>
        string StartInfusion(List<long> tags, DateTime startDateTime, string seatNum);

        /// <summary>
        /// truyền dịch Kết thúc
        /// </summary>
        /// <param name="tags">truyền dịch chi tiết ID</param>
        /// <param name="endDateTime">Thời gian kết thúc</param>
        /// <returns></returns>
        string EndInfusion(List<long> tags, DateTime endDateTime);

        /// <summary>
        /// tạo Số nhóm
        /// </summary>
        /// <param name="kh"></param>
        /// <returns></returns>
        string CreateGroupNo(string kh,string mzh);

        /// <summary>
        /// Cài đặt ghế ký tự số
        /// </summary>
        /// <param name="tag">Mục tiêu ID</param>
        /// <param name="seatNum">ghế ký tự số</param>
        /// <returns></returns>
        string SetSeatNum(long tag, string seatNum);

        /// <summary>
        /// Tra cứu truyền dịch thông tin
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<MzsyypxxVO> MzsyxxQuery(List<long> ids, string organizeId);
    }
}
