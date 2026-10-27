using Newtouch.Domain.Entity;
using Newtouch.CIS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.IRepository
{
    public interface IMzZyBarCodeRepo : IRepositoryBase<MzZyBarCodeEntity>
    {
        void SubmitForm(MzZyBarCodeEntity entity);
    }
}
