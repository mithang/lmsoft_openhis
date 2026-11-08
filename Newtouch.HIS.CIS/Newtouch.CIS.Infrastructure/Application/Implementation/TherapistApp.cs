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
    /// Điều trị liên quan
    /// </summary>
    public class TherapistApp : AppBase, ITherapistApp
    {
        /// <summary>
        /// kéo lấy chờ Khám bệnh Điều trị xây nghị danh sách
        /// </summary>
        /// <param name="brlx">bệnh nhân Loại--1Ngoại trú 2Nhập viện</param>
        /// <param name="mzzyh">Ngoại trú Số nhập viện</param>
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
        /// hơn Tạo mới nghị của chuyển đổi Trạng thái
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
