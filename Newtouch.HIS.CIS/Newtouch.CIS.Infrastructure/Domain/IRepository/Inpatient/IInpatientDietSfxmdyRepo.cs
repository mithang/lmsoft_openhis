using Newtouch.Domain.Entity;
using Newtouch.CIS.Repository;

namespace Newtouch.Domain.IRepository
{

    public interface IInpatientDietSfxmdyRepo : IRepositoryBase<InpatientDietSfxmdyEntity>
    {
        /// <summary>
        /// Lưubảngphiếu(Thêm mới,Sửa)
        /// </summary>
        /// <param name="entity">thựcthểđối tượng</param>
        /// <param name="keyValue">Khóa chínhGiá trị</param>
        void SubmitForm(InpatientDietSfxmdyEntity entity, string keyValue);

        /// <summary>
        /// XóaDữ liệu
        /// </summary>
        /// <param name="keyValue">Khóa chínhGiá trị</param>
        void DeleteForm(string keyValue);

    }
}
