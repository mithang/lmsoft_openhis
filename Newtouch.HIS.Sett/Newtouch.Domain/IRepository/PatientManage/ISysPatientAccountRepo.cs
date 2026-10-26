using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    public interface ISysPatientAccountRepo : IRepositoryBase<SysPatientAccountEntity>
    {
        /// <summary>
        /// Lấy thông tin tài khoản theo số nhập viện
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        SysPatientAccountEntity GetAccInfoByZHY(string zyh, string orgId);

        /// <summary>
        /// Thêm thông tin tài khoản xt_brzh
        /// </summary>
        /// <param name="entity"></param>
        bool AddAcc(SysPatientAccountEntity entity, string orgId, string curUserCode);

        /// <summary>
        /// Cập nhật thông tin tài khoản xt_brzh
        /// </summary>
        /// <param name="entity"></param>
        bool ModifyAcc(SysPatientAccountEntity entity);
    }
}
