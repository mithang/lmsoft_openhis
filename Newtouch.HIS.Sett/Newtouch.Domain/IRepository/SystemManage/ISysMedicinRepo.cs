using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// Thuốc hệ thống
    /// </summary>
    public interface ISysMedicinRepo
    {
        /// <summary>
        /// Theo thuốcCodeLấydlCode
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="ypCode"></param>
        /// <returns></returns>
        string GetdlCodeByYpCode(string orgId, string ypCode);

        /// <summary>
        /// Lấy thực thể thuốc
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="ypCode"></param>
        /// <returns></returns>
        SysMedicineVEntity GetEntityByCode(string orgId, string ypCode);
    }
}
