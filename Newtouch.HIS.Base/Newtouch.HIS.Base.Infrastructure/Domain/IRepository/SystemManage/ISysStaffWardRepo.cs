using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Repository;
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
        /// Tìm thông tin gắn kết nhân viên - khu bệnh
        /// </summary>
        /// <param name="staffId"></param>
        /// <returns></returns>
        IList<SysStaffWardEntity> GetStaffWardList(string staffId);
    }
}
