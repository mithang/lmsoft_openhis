
using Newtouch.CIS.Repository;
using Newtouch.Domain.Entity.Outpatient;
using Newtouch.Domain.IRepository.Outpatient;
using System;
using Newtouch.CIS.Compat;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Repository.Outpatient
{
    public class OutpatientRegConsultRepo : RepositoryBase<OutpatientRegConsultEntity>, IOutpatientRegConsultRepo
    {
        public OutpatientRegConsultRepo()
            : base()
        {
        }

        /// <summary>
        /// đăng ký khámPhòng khámLưu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int SubmitForm(OutpatientRegConsultEntity entity)
        {
            var ghzs = this.IQueryable().Where(p => p.ghnm == entity.ghnm && p.zt == "1" && p.OrganizeId == entity.OrganizeId).FirstOrDefault();

            //Sửa
            if(ghzs!=null)
            //if (!string.IsNullOrEmpty(ghzs.ghnm.ToString()))
            {
                
                var dbEntity = this.FindEntity(ghzs.Id);
                //properties
                dbEntity.zsCode = entity.zsCode;
                dbEntity.Modify(ghzs.Id);
                return Update(dbEntity);
            }
            //Lưu
            entity.Create();
            return Insert(entity);
        }

        /// <summary>
        /// Cập nhậtgọisốTrạng thái
        /// </summary>
        /// <param name="id"></param>
        /// <param name="calledstu"></param>
        /// <returns></returns>
        public int UpdateCalledstu(int ghnm,int calledstu) {

			 var dbEntity = this.IQueryable().Where(p => p.ghnm == ghnm && p.zt == "1").FirstOrDefault();
			//var dbEntity = this.FindEntity(ghnm);
			if (dbEntity!=null)
			{
				dbEntity.calledstu = calledstu;
				dbEntity.Modify(dbEntity.Id);
				return Update(dbEntity);
			}
			else
			{
				return 0;
			}
            
            
        }
    }
}
