using System.Collections.Generic;
using System.Web;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.ValueObjects.SystemManage;

namespace Newtouch.HIS.Domain.IDomainServices
{
    public interface IGRSCostEarningDmnService
    {
        /// <summary>
        /// Thêm thông tin chi phílist
        /// </summary>
        /// <param name="vo"></param>
        /// <param name="srxxId"></param>
        void AddCostList(List<jgssCostVO> vo, string srxxId);

        /// <summary>
        /// Bảng thống kê thu chi của trạm-Chi tiết thông tin thu nhập
        /// </summary>
        /// <param name="site"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        List<jgssSiteCostEarningVO> GetCosttable(string site, string year, string month);


        /// <summary>
        /// Thêm thu chi
        /// </summary>
        /// <param name="vo"></param>
        void submitEarningInfo(SiteCostEarningVO vo, string keyvalue, string orgId);
        List<jgssxxGridVO> GetjgszInfoList(Pagination pagination, string siteId, string year, string month, string shzt, bool verify);

        List<jgszchargemoneyVO> GetMoneyDetailList(Pagination pagination, string siteId, string year, string month, string type, string dlcode);

        /// <summary>
        /// Xem chi tiết số tiền thu phí trạm
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="type"></param>
        /// <param name="dlCode"></param>
        /// <returns></returns>
        IList<jgszchargemoneyVO> GetMoneyDetailList(string siteId, string year, string month, string type, string dlCode);

        /// <summary>
        /// Xem bảng thống kê thu chi trạm
        /// </summary>
        /// <param name="srxxId"></param>
        /// <returns></returns>
        SiteCostEarningVO GetEarningInfo(string srxxId, string orgId);

        /// <summary>
        /// Lấy thực thu của cơ sở vàGRSThực thu
        /// </summary>
        /// <param name="site"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        jgssandgrsssVO Getss(string site, string year, string month);

        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="srxxId"></param>
        void deletess(string srxxId);

        string UploadFile(HttpPostedFileBase file, out string parth);

        /// <summary>
        /// Lấy thông tin chi phí tháng trước
        /// </summary>
        /// <param name="site"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        List<jgssCostVO> getLastcbxx(string site, string year, string month);
    }
}
