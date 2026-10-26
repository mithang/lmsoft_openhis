using Newtouch.HIS.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.IRepository
{
    public interface ISysStaffWardRepo:IRepositoryBase<SysStaffWardEntity>
    {
        /// <summary>
        /// Tìm thông tin liên kết khu bệnh của nhân viên
        /// </summary>
        /// <param name="staffId"></param>
        /// <returns></returns>
        IList<SysStaffWardEntity> GetStaffWardList(string staffId);
    }
}
