using Newtouch.Domain.Entity;
using Newtouch.Infrastructure.EF;

namespace Newtouch.Domain.IRepository
{

    public interface IInpatientDietSfxmdyRepo : IRepositoryBase<InpatientDietSfxmdyEntity>
    {
        /// <summary>
        /// Lưu biểu mẫu(Thêm mới,Sửa đổi)
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        void SubmitForm(InpatientDietSfxmdyEntity entity, string keyValue);

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        void DeleteForm(string keyValue);

    }
}