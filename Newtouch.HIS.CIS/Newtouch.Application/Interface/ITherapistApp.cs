using Newtouch.Domain.DTO;
using System.Collections.Generic;

namespace Newtouch.Application.Interface
{
    /// <summary>
    /// Liên quan đến điều trị
    /// </summary>
    public interface ITherapistApp
    {
        /// <summary>
        /// Lấy danh sách đề xuất điều trị đang chờ khám
        /// </summary>
        /// <param name="brlx">Loại bệnh nhân--1 ngoại trú 2 nội trú</param>
        /// <param name="mzzyh">Số ngoại trú/nội trú</param>
        /// <returns></returns>
        IList<WaitCvTherapistSuggestionDTO> GetWaitCvSuggestionList(string brlx, string mzzyh, int dayNum);

        /// <summary>
        /// Cập nhật trạng thái chuyển đổi của đề xuất
        /// </summary>
        /// <param name="cvList"></param>
        void UpdateSuggestionCvStatus(IList<SuggestionCvStatuDTO> cvList);

    }
}
