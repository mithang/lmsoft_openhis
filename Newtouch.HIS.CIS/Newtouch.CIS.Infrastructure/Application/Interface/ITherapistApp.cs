using Newtouch.Domain.DTO;
using System.Collections.Generic;
using Newtouch.CIS.Compat;

namespace Newtouch.Application.Interface
{
    /// <summary>
    /// Điều trịliên quan
    /// </summary>
    public interface ITherapistApp
    {
        /// <summary>
        /// kéolấychờ Khám bệnh Điều trịxâynghịdanh sách
        /// </summary>
        /// <param name="brlx">bệnh nhân Loại--1Ngoại trú 2Nhập viện</param>
        /// <param name="mzzyh">Ngoại trú Số nhập viện</param>
        /// <returns></returns>
        IList<WaitCvTherapistSuggestionDTO> GetWaitCvSuggestionList(string brlx, string mzzyh, int dayNum);

        /// <summary>
        /// hơn Tạo mớinghịcủachuyểnđổi Trạng thái
        /// </summary>
        /// <param name="cvList"></param>
        void UpdateSuggestionCvStatus(IList<SuggestionCvStatuDTO> cvList);

    }
}
