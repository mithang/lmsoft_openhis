using Newtouch.Domain.Entity.Outpatient;
using Newtouch.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.IRepository
{
    public interface IPacs_ExamBodyPartsRepo : IRepositoryBase<Pacs_ExamBodyPartsEntity>
    {
        /// <summary>
        /// Lấy danh sách
        /// </summary>
        /// <returns></returns>
        List<Pacs_ExamBodyPartsEntity> GetListByOrg(string orgId);

        /// <summary>
        /// Lưu
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(Pacs_ExamBodyPartsEntity entity, string keyValue);
        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="keyValue"></param>
        void DeleteForm(string keyValue);
    }
}
