using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysCardRepo : IRepositoryBase<SysCardEntity>
    {
        /// <summary>
        /// Lấy tất cả thẻ
        /// </summary>
        /// <returns></returns>
        List<SysCardEntity> GetList(string orgId);

        /// <summary>
        /// Lấy mã nội bộ bệnh nhân theo số thẻ
        /// </summary>
        /// <param name="cardno"></param>
        /// <returns></returns>
        string GetPatidByCardNo(string cardno, string orgId);

        /// <summary>
        /// Lấy thẻ ảo mới Số thẻ
        /// </summary>
        /// <returns></returns>
        string GetCardSerialNo(string orgId);

        void SubmitForm(SysCardEntity SysPatBasicInfoEntity, string keyValue, string orgId);

        /// <summary>
        /// Lấy loại thẻ theo số thẻ
        /// </summary>
        /// <param name="cardno"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        string GetCardType(string cardno, string orgId);

        /// <summary>
        /// Lấy thông tin thẻ theo số thẻ và tổ chức(Trước tiên giả định các loại thẻ khác nhau Số thẻ sẽ không trùng lặp)
        /// </summary>
        /// <param name="cardno"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        SysCardEntity GetCardEntity(string cardno, string orgId);

        /// <summary>
        /// Lấy thực thể thẻ
        /// </summary>
        /// <param name="cardType"></param>
        /// <param name="cardno"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        SysCardEntity GetCardEntity(string cardType, string cardno, string orgId);
        SysCardEntity GetCardEntity(int patid, string cardType, string orgId);
    }
}
