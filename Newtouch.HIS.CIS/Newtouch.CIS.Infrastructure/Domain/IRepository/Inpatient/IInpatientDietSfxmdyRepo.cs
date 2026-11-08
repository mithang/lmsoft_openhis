using Newtouch.Domain.Entity;
using Newtouch.CIS.Repository;

namespace Newtouch.Domain.IRepository
{

    public interface IInpatientDietSfxmdyRepo : IRepositoryBase<InpatientDietSfxmdyEntity>
    {
        /// <summary>
        /// Lưu bảng phiếu(Thêm mới,Sửa)
        /// </summary>
        /// <param name="entity">thực thể đối tượng</param>
        /// <param name="keyValue">Khóa chính Giá trị</param>
        void SubmitForm(InpatientDietSfxmdyEntity entity, string keyValue);

        /// <summary>
        /// Xóa Dữ liệu
        /// </summary>
        /// <param name="keyValue">Khóa chính Giá trị</param>
        void DeleteForm(string keyValue);

    }
}
