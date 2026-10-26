using Newtouch.Common.Operator;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Application
{
    public class SysPatiAreaApp : ISysPatiAreaApp
    {
        private readonly ISysWardRepo _SysPatiAreaRepository;

        public SysPatiAreaApp(ISysWardRepo SysPatiAreaRepository)
        {
            this._SysPatiAreaRepository = SysPatiAreaRepository;
        }

        /// <summary>
        /// Lấy dòng vụ dư ới khu bệnh hộp thả xuống 
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public object GetbqList()
        {
            return _SysPatiAreaRepository.GetbqList(OperatorProvider.GetCurrent().OrganizeId);
        }
    }
}
