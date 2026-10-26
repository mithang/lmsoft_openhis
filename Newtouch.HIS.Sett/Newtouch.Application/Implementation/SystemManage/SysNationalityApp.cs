using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Application
{
    public class SysNationalityApp : ISysNationalityApp
    {
        private readonly ISysNationalityRepo _SysNationalityRepo;

        public SysNationalityApp(ISysNationalityRepo SysNationalityRepo)
        {
            this._SysNationalityRepo = SysNationalityRepo;
        }

        /// <summary>
        /// Lấy tất cả các quốc tịch tự đ ộng nhắc nhở
        /// </summary>
        /// <returns></returns>
        public object GetgjList()
        {
            return _SysNationalityRepo.GetgjList();
        }
    }
}
