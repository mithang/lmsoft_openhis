using Newtouch.Core.Common;
using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// Thông tin cơ bản bảo hiểm y tế Quý Châu
    /// </summary>
    public interface IGzybBaseInfoDmnService
    {
        /// <summary>
        /// Lấy danh mục hạng mục dịch vụ Quý Châu, số thứ tự lớn nhất
        /// </summary>
        /// <returns></returns>
        int GetItemCodeMaxLsh();

        #region 目录相关
        string Header(string tbname);

        IList<G_yb_mluCommon_Info> Get_G_yb_mluCommon_Info(Pagination pagination, string mlbh, string key);
        #endregion
    }
}
