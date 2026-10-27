using Newtouch.EMR.Domain.Entity;
using System.Collections.Generic;
using NewtouchHIS.Lib.Base.Model;

namespace Newtouch.EMR.Domain.IRepository
{
 
    public interface Ibl_bllxRepo : IRepositoryBase<bl_bllxEntity>
    {
        void SubmitForm(bl_bllxEntity entity, string keyValue);
        void DeleteForm(string keyValue, string orgId, string user);
    }
}
