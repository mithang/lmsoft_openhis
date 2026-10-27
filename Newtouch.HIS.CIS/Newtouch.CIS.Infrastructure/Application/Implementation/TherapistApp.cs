using System.Collections.Generic;

using Newtouch.Application.Interface;
using Newtouch.Domain.DTO;
using Newtouch.CIS.Compat;
using Newtouch.Common.Web;
using Newtouch.Infrastructure;
using System;

namespace Newtouch.Application.Implementation
{
    /// <summary>
    /// Điều trịliên quan
    /// </summary>
    public class TherapistApp : AppBase, ITherapistApp
    {
        /// <summary>
        /// kéolấychờKhám bệnhĐiều trịxâynghịdanh sách
        /// </summary>
        /// <param name="brlx">bệnh nhânLoại--1Ngoại trú 2Nhập viện</param>
        /// <param name="mzzyh">Ngoại trúSố nhập viện</param>
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
        /// hơnTạo mớinghịcủachuyểnđổiTrạng thái
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
