using Newtouch.Domain.ViewModels;

namespace Newtouch.Application.Interface
{
    /// <summary>
    /// Cài đặt khám ngoại trú
    /// </summary>
    public interface IVisitDeptSetApp
    {
        /// <summary>
        /// Gửi biểu mẫu
        /// </summary>
        /// <param name="vo"></param>
        /// <param name="userCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        string SubmitForm(VisitDeptDetail vo, string userCode, string organizeId);
    }
}