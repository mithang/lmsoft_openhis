using Newtouch.HIS.Domain.Entity.SystemManage;
using Newtouch.HIS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.IRepository.SystemManage
{
    public interface ISysConsultRepo : IRepositoryBase<SysConsultEntity>
    {
        /// <summary>
        /// Lấy danh sách phòng khám
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<SysConsultEntity> GetConsultList(string orgId);

        /// <summary>
        /// Lấy danh sách phòng khám thuộc khoa
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="ksCode"></param>
        /// <returns></returns>
        List<SysConsultEntity> GetConsultListByDept(string orgId, string ksCode);
        /// <summary>
        /// Lấy danh sách phòng khám hiệu lực thuộc khoa
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="ksCode"></param>
        /// <returns></returns>
        List<SysConsultEntity> GetValidConsultListByDept(string orgId, string ksCode);
        /// <summary>
        /// Thêm phòng khám mới
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ksCode"></param>
        void SubmitForm(SysConsultEntity entity, string ksCode);
    }
}
