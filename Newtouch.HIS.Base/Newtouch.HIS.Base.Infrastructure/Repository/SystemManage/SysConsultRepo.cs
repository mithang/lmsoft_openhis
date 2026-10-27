using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.HIS.Repository;
using Newtouch.HIS.Domain.Entity.SystemManage;
using Newtouch.HIS.Domain.IRepository.SystemManage;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Repository.SystemManage
{
    public class SysConsultRepo : RepositoryBase<SysConsultEntity>, ISysConsultRepo
    {

        public SysConsultRepo()
     
        {

        }

        /// <summary>
        /// Lấy danh sách phòng khám
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public List<SysConsultEntity> GetConsultList(string orgId)
        {
            return this.IQueryable().Where(p => p.OrganizeId == orgId).ToList();
        }

        /// <summary>
        /// Lấy danh sách phòng khám thuộc khoa
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="ksCode"></param>
        /// <returns></returns>
        public List<SysConsultEntity> GetConsultListByDept(string orgId,string ksCode)
        {
            return this.IQueryable().Where(p => p.OrganizeId == orgId && p.ksCode==ksCode).OrderBy(p => p.xh).ToList();
        }

        /// <summary>
        /// Lấy danh sách phòng khám hiệu lực thuộc khoa
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="ksCode"></param>
        /// <returns></returns>
        public List<SysConsultEntity> GetValidConsultListByDept(string orgId, string ksCode)
        {
            return this.IQueryable().Where(p => p.OrganizeId == orgId && p.ksCode == ksCode && p.zt=="1").OrderBy(p => p.xh).ToList();
        }


        /// <summary>
        /// Thêm phòng khám mới
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ksCode"></param>
        public void SubmitForm(SysConsultEntity entity, string ksCode)
        {
            if (!string.IsNullOrWhiteSpace(ksCode))
            {
                    throw new FailedException("Vui lòng chọn khoa!");
                
            }
            else
            {
                //CodeKiểm tra trùng lặp
                if (this.IQueryable().Any(p => p.zsCode == entity.zsCode))
                {
                    throw new FailedException("Mã không được trùng lặp");
                }
                entity.Create(true);
                this.Insert(entity);
            }
        }


    }
}
