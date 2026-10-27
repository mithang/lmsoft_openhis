
using Newtouch.CIS.Repository;
using Newtouch.Domain.Entity;
using Newtouch.Domain.Entity.Clinic;
using Newtouch.Domain.IRepository;
using Newtouch.Domain.IRepository.Clinic;
using System;
using Newtouch.CIS.Compat;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Repository.Clinic
{
    public class ClinicApplyRepo : RepositoryBase<ClinicApplyInfoEntity>, IClinicApplyRepo
    {
        /// <summary>
        /// Mặc địnhHàm khởi tạosố
        /// </summary>
        /// <param name="databaseFactory">EFtrêndướivănNhà máy</param>
        public ClinicApplyRepo()
            : base()
        {

        }

        /// <summary>
        /// Cập nhậtTrạng thái đăng ký
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="sqzt"></param>
        public void Updatesqzt(string Id, int sqzt,string orgId)
        {
            //var dbEntity = this.FindEntity(Id);

            var dbEntity = this.IQueryable().Where(p => p.Id == Id && p.zt == "1" && p.OrganizeId== orgId).FirstOrDefault();
            if (dbEntity != null)
            {
                //properties
                dbEntity.sqzt = sqzt;
                dbEntity.Modify(Id);
                this.Update(dbEntity);
            }

        }

        public ClinicApplyInfoEntity GetYczl(string Id, string orgId)
        {
            var result = this.IQueryable().Where(p => p.Id == Id && p.OrganizeId == orgId && p.zt == "1").FirstOrDefault();
            return result;
        }

    }
}
