using Newtouch.EMR.Domain.Entity;
using System.Collections.Generic;
using NewtouchHIS.Lib.Base.Model;

namespace Newtouch.EMR.Domain.IRepository
{
 
    public interface Ibl_patrecordsRepo : IRepositoryBase<bl_patrecordsEntity>
    {

        bl_patrecordsEntity GetpatrecordsByID(string ID);
        void SubmitForm(bl_patrecordsEntity entity, string keyValue);

    }
}
