using System;
using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;
using Newtouch.Tools;
using Newtouch.Common;

namespace Newtouch.HIS.Application
{
    public interface ISysChargeClassifyApp
    {

        /// <summary>
        /// Lấy tất cả các thu phí phân loại hộp thả xuống 
        /// </summary>
        /// <returns></returns>
        object GetsfflSelect();

        /// <summary>
        /// Atrang mang một đ ối tư ợng sangBtrang
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        SysChargeClassificationEntity GetForm(int keyValue);
        void DeleteForm(int keyValue);
        void SubmitForm(SysChargeClassificationEntity xt_sfflEntity, string keyValue);

        /// <summary>
        /// GridHiển thị tra cứu lọc
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        List<SysChargeClassificationEntity> GetListBySearch(Pagination Pagination, string keyword);
    }
}
