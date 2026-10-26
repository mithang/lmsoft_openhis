using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// Bản ghi thu chi tạm ứng
    /// </summary>
    public interface ISysAccountRevenueAndExpenseRepo : IRepositoryBase<SysAccountRevenueAndExpenseEntity>
    {
        /// <summary>
        /// Theo số nhập viện,Tài khoản tạm ứng Lấy Bản ghi thu chi Danh sách
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        IList<SysAccountRevenueAndExpenseEntity> GetListByZyhAndYjjzh(int zh, string orgId);

        ///// <summary>
        ///// Theo tài khoản Lấy bản ghi cuối cùng của tài khoản(Để lấy số dư tài khoản)
        ///// add by HLF
        ///// </summary>
        ///// <param name="zh"></param>
        ///// <returns></returns>
        //SysPatientAccountRevenueAndExpenseEntity GetLastJL(int zh, string orgId);

        /// <summary>
        /// Thêm bản ghi thu chi tài khoản
        /// </summary>
        /// <param name="sysPatPayInfoEntity"></param>
        void AddPayInfo(SysAccountRevenueAndExpenseEntity sysPatPayInfoEntity, string orgId, string curUserCode, string curUserName);

    }
}
