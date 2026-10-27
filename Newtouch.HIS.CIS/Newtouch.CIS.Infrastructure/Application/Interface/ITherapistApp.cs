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
        /// kéolấychờKhám bệnhĐiều trịxâynghịdanh sách
        /// </summary>
        /// <param name="brlx">bệnh nhânLoại--1Ngoại trú 2Nhập viện</param>
        /// <param name="mzzyh">Ngoại trúSố nhập viện</param>
        /// <returns></returns>
        IList<WaitCvTherapistSuggestionDTO> GetWaitCvSuggestionList(string brlx, string mzzyh, int dayNum);

        /// <summary>
        /// hơnTạo mớinghịcủachuyểnđổiTrạng thái
        /// </summary>
        /// <param name="cvList"></param>
        void UpdateSuggestionCvStatus(IList<SuggestionCvStatuDTO> cvList);

    }
}
