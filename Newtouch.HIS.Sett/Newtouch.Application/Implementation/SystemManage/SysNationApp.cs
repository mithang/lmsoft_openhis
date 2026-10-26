using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Application
{
    public class SysNationApp : ISysNationApp
    {
        private readonly ISysNationRepo _xt_mzRepository;

        public SysNationApp(ISysNationRepo xt_mzRepository)
        {
            this._xt_mzRepository = xt_mzRepository;
        }

        /// <summary>
        /// Lấy hiệu lực dân tộc hộp thả xuống 
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public object GetmzList()
        {
            return _xt_mzRepository.GetmzList();
        }
    }
}
