using Newtouch.Domain.Entity;
using Newtouch.CIS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.IRepository
{
    public interface IXtjyjcFileUploadRepo : IRepositoryBase<XtjyjcFileUploadEntity>
    {

        void SubmitForm(List<XtjyjcFileUploadEntity> entityList);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        void DeleteForm(string keyValue);
    }
}
