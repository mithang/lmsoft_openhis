using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// Ngoại trú hủy bỏ
    /// </summary>
    public class OutpatientRegistAbandonRepo : RepositoryBase<OutpatientRegistAbandonEntity>, IOutpatientRegistAbandonRepo
    {
        public OutpatientRegistAbandonRepo(INewtouchDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        ///// <summary>
        /////LưuĐ ăng ký khám hủy bỏ bản ghi 
        ///// </summary>
        ///// <param name="outPatientRegAbandEntity"></param>
        //public void SaveRegAbandRecord(int ghnm, string yhm, int rybh)
        //{
        //    OutpatientRegistAbandonEntity outPatientRegAbandEntity = new OutpatientRegistAbandonEntity();
        //    outPatientRegAbandEntity.Create();
        //    outPatientRegAbandEntity.ghnm = ghnm;
        //    outPatientRegAbandEntity.zfry = yhm;
        //    outPatientRegAbandEntity.zfrybh = rybh;
        //    outPatientRegAbandEntity.zfrq = DateTime.Now;
        //    this.Insert(outPatientRegAbandEntity);
        //}

    }
}


