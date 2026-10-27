
using System.Collections.Generic;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.CIS.Repository;
using Newtouch.Domain.Entity;

namespace Newtouch.Domain.IRepository
{
    public interface IBlctglRepo : IRepositoryBase<BlctglEntity>
    {

        List<BlctglEntity> GetTreeList(string orgId, int qx, string UserCode, string parentId);

        /// <summary>
        /// Lưubảngphiếu（Thêm mới、Sửa）
        /// </summary>
        /// <param name="entity">thựcthểđối tượng</param>
        /// <param name="keyValue">Khóa chínhGiá trị</param>
        void SubmitForm(BlctglEntity entity, string keyValue);

        /// <summary>
        /// XóaDữ liệu
        /// </summary>
        /// <param name="keyValue">Khóa chínhGiá trị</param>
        void DeleteForm(string keyValue);

        BlctglEntity GetCitiaoByID(string orgId, string ID);

    }
}
