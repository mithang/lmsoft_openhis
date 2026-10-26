using FrameworkBase.MultiOrg.Application;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// 
    /// </summary>
    public class SysCardApp : AppBase, ISysCardApp
    {
        private readonly ISysCardRepo _sysCardRepository;
        //other Repositories or DomainServices
        
        /// <summary>
        /// theo số thẻ lấy bệnh nhân mã nội bộ
        /// </summary>
        /// <param name="cardno"></param>
        /// <returns></returns>
        public string GetPatidByCardNo(string cardno)
        {
            return _sysCardRepository.GetPatidByCardNo(cardno, this.OrganizeId);
        }

        /// <summary>
        /// Lấy mới 虚拟 thẻ số thẻ
        /// </summary>
        /// <returns></returns>
        public string GetCardSerialNo()
        {
            return _sysCardRepository.GetCardSerialNo(this.OrganizeId);
        }
    }
}
