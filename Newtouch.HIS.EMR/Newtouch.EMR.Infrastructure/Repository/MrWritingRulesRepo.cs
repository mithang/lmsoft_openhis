using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace Newtouch.EMR.Repository
{
    public class MrWritingRulesRepo : RepositoryBase<MrWritingRulesEntity>, IMrWritingRulesRepo
    {


        // G17: trả số dòng đã xóa để controller báo lỗi trung thực khi Id
        // không tồn tại (trước đây xóa 0 dòng vẫn Success).
        public int DeleteForm(string Id, string orgId)
        {
            return this.Delete(a => a.Id == Id && a.OrganizeId == orgId);
        }
    }
}
