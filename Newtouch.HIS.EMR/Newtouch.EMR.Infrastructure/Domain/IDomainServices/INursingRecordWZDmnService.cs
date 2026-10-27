using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.Domain.ValueObjects;
using Newtouch.EMR.Domain.BusinessObjects;
using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.ValueObjects.MedicalRecord;
using System;
using System.Collections.Generic;
using Newtouch.EMR.Domain.IRepository;

namespace Newtouch.EMR.Domain.IDomainServices
{
    public interface INursingRecordWZDmnService : IRepositoryBase<bl_hljl_wzEntity>
    {
        void SubmitForm(bl_hljl_wzEntity entity, string keyValue);
        IList<bl_hljl_wzEntity> GetPaginationListWZ(Pagination pagination, string orgId, DateTime? kssj, DateTime? jssj, string zyh, string wardCode, string isShowDelete);
    }
}
