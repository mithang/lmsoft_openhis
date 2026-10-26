using Newtouch.Common.Model;
using Newtouch.HIS.Domain.DTO.InputDto;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.IDomainServices
{
    public interface ITherapistSuggestionDmnService
    {
        void SaveData(IList<TherapistSuggestionEntity> itemList, IList<string> delIds, string orgId);
        List<MzZyPatInfoDto> GetMzZyPatInfo(string mzzyh, string orgId, string brlx);
        List<TherapistAdviceDto> GetZLJYList(string mzzyh, string orgId, string brlx);

        /// <summary>
        /// Lấy danh sách đề xuất điều trị(APIdùng)
        /// </summary>
        /// <param name="mzzyh"></param>
        /// <param name="orgId"></param>
        /// <param name="brlx"></param>
        /// <param name="startTime">Thời gian bắt đầu lập</param>
        /// <returns></returns>
        IList<TherapistSuggestionVO> GetTherapistSuggestionList(string mzzyh, string orgId, string brlx, DateTime startTime);

        /// <summary>
        /// Cập nhật trạng thái chuyển đổi được đề xuất
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="cvList"></param>
        void UpdateSuggestionCvStatus(string orgId, IList<SuggestionCvStatuUpdateDTO> cvList);
    }
}
