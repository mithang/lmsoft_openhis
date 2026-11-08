using Newtouch.CIS.Compat;
using Newtouch.Domain.ViewModels;

namespace Newtouch.Application.Interface
{
    /// <summary>
    /// Ngoại trú ra khám Cài đặt
    /// </summary>
    public interface IVisitDeptSetApp
    {
        /// <summary>
        /// Gửi bảng phiếu
        /// </summary>
        /// <param name="vo"></param>
        /// <param name="userCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        string SubmitForm(VisitDeptDetail vo, string userCode, string organizeId);
    }
}