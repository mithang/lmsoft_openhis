using NewtouchHIS.Lib.Base.Model;
using Newtouch.Domain.Entity;
using Newtouch.CIS.Repository;
using System.Collections.Generic;

namespace Newtouch.Domain.IRepository
{

    public interface IInpatientDietBaseRepo : IRepositoryBase<InpatientDietBaseEntity>
    {
        /// <summary>
        /// Lưubảngphiếu(Thêm mới,Sửa)
        /// </summary>
        /// <param name="entity">thựcthểđối tượng</param>
        /// <param name="keyValue">Khóa chínhGiá trị</param>
        void SubmitForm(InpatientDietBaseEntity entity, string keyValue);

        /// <summary>
        /// XóaDữ liệu
        /// </summary>
        /// <param name="keyValue">Khóa chínhGiá trị</param>
        void DeleteForm(string keyValue);

        IList<InpatientDietBaseEntity> GetGridList(Pagination pagination, string lb, string keyword, string orgId);

    }
}
