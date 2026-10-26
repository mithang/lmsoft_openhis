using System;
using System.Collections.Generic;
using Newtouch.HIS.Proxy.guian.DO;
using Newtouch.HIS.Proxy.guian.DTO;
using Newtouch.HIS.Proxy.guian.DTO.S21;
using Newtouch.HIS.Proxy.guian.DTO.S24;
using Newtouch.HIS.Proxy.guian.DTO.S25;

namespace Newtouch.HIS.Application.Interface
{
    /// <summary>
    /// 贵安ngoại trú BHYT nông thôn mới 
    /// </summary>
    public interface IGuiAnOutpatientXnhApp
    {
        /// <summary>
        /// mô phỏng quyết toán全流程
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="userCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="s24"></param>
        /// <returns></returns>
        string SimulationSettWholeProcess(string mzh, string userCode, string organizeId, out S24ResponseDTO s24);

        /// <summary>
        /// mô phỏng quyết toán, đ ơn 功 có thể 
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="zlkssj"></param>
        /// <param name="organizeId"></param>
        /// <param name="s24Response"></param>
        /// <returns></returns>
        string SimulationSett(string mzh, DateTime zlkssj, string organizeId, out Response<S24ResponseDTO> s24Response);

        /// <summary>
        /// mô phỏng quyết toán
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="zlkssj"></param>
        /// <param name="s21Details">Tải lênChi tiết</param>
        /// <param name="organizeId"></param>
        /// <param name="s24Response"></param>
        /// <returns></returns>
        string SimulationSett(string mzh, DateTime zlkssj, List<S21Detail> s21Details, string organizeId, out Response<S24ResponseDTO> s24Response);

        /// <summary>
        /// trả lại trư ớc trư ớc tải lên củachi tiết
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="outpId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        string DetailedUploadRetreat(string startDate, string endDate, string outpId, string organizeId);

        /// <summary>
        /// Tra cứu số thứ tự bồi thư ờng ngoại trú
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        string OutpIdQuery(string mzh, string organizeId);

        /// <summary>
        /// Ngoại trú tải lên
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="organizeId"></param>
        /// <param name="zlkssj"> chẩn đoán và điều trị thời gian bắt đ ầu</param>
        /// <returns></returns>
        string OutpatientUpload(string mzh, string organizeId, string userCode, out DateTime zlkssj);

        /// <summary>
        /// Lưu mới số thứ tự bồi thư ờng ngoại trú
        /// </summary>
        /// <param name="ghnm"></param>
        /// <param name="userCode"></param>
        /// <param name="mzh"></param>
        /// <param name="organizeId"></param>
        /// <param name="outpId"></param>
        /// <returns></returns>
        void SaveNewOutpId(int ghnm, string userCode, string mzh, string organizeId, string outpId);

        /// <summary>
        /// Lưu mới số thứ tự bồi thư ờng ngoại trú
        /// </summary>
        /// <param name="ghnm"></param>
        /// <param name="userCode"></param>
        /// <param name="mzh"></param>
        /// <param name="organizeId"></param>
        /// <param name="newOutpId"></param>
        /// <returns></returns>
        void UpdateMzghOutpId(int ghnm, string userCode, string mzh, string organizeId, string newOutpId);

        /// <summary>
        /// Ngoại trú tải lên trả lại 
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        string OutpatientUploadReturn(string mzh, string organizeId, string userCode);

        /// <summary>
        /// Trả lại ngoại trú, có thể tại HủyĐ ăng ký khám trong sử dụng
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns>Nguyên nhân thất bại</returns>
        string OutpatientReturn(string mzh, string organizeId, string userCode);

        /// <summary>
        /// Hủy quyết toán,ngoại trú ghi đ ỏ hoàn trả 
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        string CancelSett(string mzh, string organizeId, string userCode);

        /// <summary>
        /// Hủy quyết toán,ngoại trú ghi đ ỏ hoàn trả 
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="outpId"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        string CancelSett(string mzh, string outpId, string organizeId, string userCode);

        /// <summary>
        /// BHYT nông thôn mới quyết toán ngoại trú
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <param name="s25"></param>
        /// <param name="outpId"></param>
        /// <returns></returns>
        string Sett(string mzh, string organizeId, string userCode, out S25ResponseDTO s25, out string outpId);

        /// <summary>
        /// BHYT nông thôn mới quyết toán ngoại trú
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="outpId"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <param name="s25"></param>
        /// <returns></returns>
        string Sett(string mzh, string outpId, string organizeId, string userCode, out S25ResponseDTO s25);

        /// <summary>
        /// nhóm 装hoàn phí sau cần lại tạo củachi tiết quyết toán
        /// </summary>
        /// <param name="jsnm"></param>
        /// <param name="tjsxmDict"></param>
        /// <param name="mzh"></param>
        /// <param name="organizeId"></param>
        /// <param name="outpId"></param>
        /// <returns></returns>
        TFS21RequestDTO AssembleS21RequestDto(string jsnm, Dictionary<string, decimal> tjsxmDict, string mzh, string organizeId, string outpId = "");

        /// <summary>
        /// Hoàn phí
        /// </summary>
        /// <param name="jsnm"></param>
        /// <param name="tjsxmDict"></param>
        /// <param name="mzh"></param>
        /// <param name="outpId"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <param name="tfRequest"></param>
        /// <returns></returns>
        string XnhTuiFeiProcess(string jsnm, Dictionary<string, decimal> tjsxmDict, string mzh, string outpId,
           string organizeId, string userCode, out TFS21RequestDTO tfRequest);

        /// <summary>
        /// BHYT nông thôn mới BHYT yêu cầu 
        /// </summary>
        /// <param name="itn">Giao diện tên 例:S02</param>
        /// <param name="body"></param>
        /// <param name="organizeId"></param>
        /// <param name="response">Bản tin trả về</param>
        /// <returns>Kết quả</returns>
        string XnhInterfaceSend(string itn, string body, string organizeId, out string response);
    }
}