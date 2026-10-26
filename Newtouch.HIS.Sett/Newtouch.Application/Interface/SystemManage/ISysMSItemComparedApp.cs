using System;
using System.Data;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Application
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysMSItemComparedApp
    {
        /// <summary>
        /// Atrang mang một đ ối tư ợng sangBtrang
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        SysMedicalTechItemMappEntity GetForm(Guid keyValue);
        void DeleteForm(int keyValue);
        void SubmitForm(SysMedicalTechItemMappEntity entity, string keyValue);

        /// <summary>
        /// GridHiển thị tra cứu lọc
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        DataTable GetListBySearch(string keyword);
    }
}
