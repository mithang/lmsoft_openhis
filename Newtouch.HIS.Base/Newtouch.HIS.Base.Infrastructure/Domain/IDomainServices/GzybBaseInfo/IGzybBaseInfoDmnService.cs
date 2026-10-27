using Newtouch.HIS.Domain.ValueObjects;
using NewtouchHIS.Lib.Base.Model;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// Thông tin cơ sở BHYT Quý Châu
    /// </summary>
    public interface IGzybBaseInfoDmnService
    {
        /// <summary>
        /// Lấy danh mục hạng mục dịch vụ Quý Châu,Số thứ tự lớn nhất
        /// </summary>
        /// <returns></returns>
        int GetItemCodeMaxLsh();

        #region Liên quan danh mục
        string Header(string tbname);

        IList<G_yb_mluCommon_Info> Get_G_yb_mluCommon_Info(Pagination pagination, string mlbh, string key);
        #endregion
    }
}
