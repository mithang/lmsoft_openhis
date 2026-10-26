using Newtouch.Common;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Linq;

namespace Newtouch.HIS.Application
{
    public class SysStaffApp : ISysStaffApp
    {
        private readonly ISysStaffRepo _sysStaffRepository;

        public SysStaffApp(ISysStaffRepo sysStaffRepository)
        {
            this._sysStaffRepository = sysStaffRepository;
        }

        /// <summary>
        /// Hiệu lực viên nhân viên tự đ ộng nhắc nhở
        /// </summary>
        /// <returns></returns>
        public object GetValidStaffList()
        {
            return _sysStaffRepository.GetValidStaffList();
        }

        /// <summary>
        /// theo Account(mã nhân viên)lấy nhân sự số hiệu
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public int GetSysRybhByAccont(string account)
        {
            var entity = _sysStaffRepository.IQueryable().Where(p => p.zt == ((int)EnumZT.Valid).ToString() && p.gh == account).FirstOrDefault();
            if (entity != null)
            {
                return entity.rybh;
            }
            return 0;
        }

    }
}
