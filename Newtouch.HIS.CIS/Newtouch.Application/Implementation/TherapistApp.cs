using System.Collections.Generic;
using FrameworkBase.MultiOrg.Application;
using Newtouch.Application.Interface;
using Newtouch.Domain.DTO;
using Newtouch.Common.Web;
using Newtouch.Infrastructure;
using System;

namespace Newtouch.Application.Implementation
{
    /// <summary>
    /// Liên quan đến điều trị
    /// </summary>
    public class TherapistApp : AppBase, ITherapistApp
    {
        /// <summary>
        /// Lấy danh sách đề xuất điều trị đang chờ khám
        /// </summary>
        /// <param name="brlx">Loại bệnh nhân--1 ngoại trú 2 nội trú</param>
        /// <param name="mzzyh">Số ngoại trú/nội trú</param>
        /// <returns></returns>
        public IList<WaitCvTherapistSuggestionDTO> GetWaitCvSuggestionList(string brlx, string mzzyh,int dayNum)
        {
            if (string.IsNullOrWhiteSpace(brlx) || string.IsNullOrWhiteSpace(mzzyh))
            {
                return null;
            }
            DateTime starttime = DateTime.Now.AddDays(-dayNum);
            var reqObj = new
            {
                OrganizeId = this.OrganizeId,
                brlx = brlx,
                mzzyh = mzzyh,
                startTime= starttime
            };
            var apiResp = SiteSettAPIHelper.Request<APIRequestHelper.DefaultResponse<IList<WaitCvTherapistSuggestionDTO>>>(
                "/api/Therapist/TherapistSuggestionQuery", reqObj, autoAppendToken: false);
            return apiResp.data;
        }

        /// <summary>
        /// Cập nhật trạng thái chuyển đổi của đề xuất
        /// </summary>
        /// <param name="cvList"></param>
        public void UpdateSuggestionCvStatus(IList<SuggestionCvStatuDTO> cvList)
        {
            if (cvList == null || cvList.Count == 0)
            {
                return;
            }
            var reqObj = new
            {
                OrganizeId = this.OrganizeId,
                CvList = cvList,
            };
            var apiResp = SiteSettAPIHelper.Request<APIRequestHelper.DefaultResponse>(
                "/api/Therapist/UpdateSuggestionCvStatus", reqObj, autoAppendToken: false);
            return;
        }

    }
}
