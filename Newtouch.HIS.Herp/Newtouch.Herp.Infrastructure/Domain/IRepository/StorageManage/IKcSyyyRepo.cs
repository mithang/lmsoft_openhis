using Newtouch.Herp.Domain.Entity;

namespace Newtouch.Herp.Domain.IRepository
{
    /// <summary>
    /// Nguyên nhân chênh lệch
    /// </summary>
    public interface IKcSyyyRepo : IRepositoryBase<KcSyyyEntity>
    {
        /// <summary>
        /// delete Syyy by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int DeleteSyyyById(string id);

        /// <summary>
        /// submit syyy maintenance form
        /// </summary>
        /// <param name="kcSyyyEntity"></param>
        /// <param name="keyWord"></param>
        int SubmitForm(KcSyyyEntity kcSyyyEntity, string keyWord);
    }
}