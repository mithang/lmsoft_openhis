using Newtouch.Domain.DTO;
using System.Collections.Generic;
using Newtouch.CIS.Compat;

namespace Newtouch.Application.Interface
{
    /// <summary>
    /// Điều trị liên quan
    /// </summary>
    public interface ITherapistApp
    {
        /// <summary>
        /// kéo lấy chờ Khám bệnh Điều trị xây nghị danh sách
        /// </summary>
        /// <param name="brlx">bệnh nhân Loại--1Ngoại trú 2Nhập viện</param>
        /// <param name="mzzyh">Ngoại trú Số nhập viện</param>
        /// <returns></returns>
        IList<WaitCvTherapistSuggestionDTO> GetWaitCvSuggestionList(string brlx, string mzzyh, int dayNum);

        /// <summary>
        /// hơn Tạo mới nghị của chuyển đổi Trạng thái
        /// </summary>
        /// <param name="cvList"></param>
        void UpdateSuggestionCvStatus(IList<SuggestionCvStatuDTO> cvList);

    }
}
