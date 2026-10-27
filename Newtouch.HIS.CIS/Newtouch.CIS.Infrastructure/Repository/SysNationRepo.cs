using Newtouch.Domain.Entity;

using Newtouch.CIS.Repository;
using Newtouch.Domain.IRepository;
using System.Collections.Generic;
using Newtouch.CIS.Compat;

namespace Newtouch.Repository
{
    public class SysNationRepo : RepositoryBase<SysNationVEntity>, ISysNationRepo
    {

        public SysNationRepo()
            : base()
        {

        }

        /// <summary>
        /// 获取有效民族下拉框
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysNationVEntity> GetmzList()
        {
            var sql = "select mzCode, mzmc, py from [NewtouchHIS_Base]..V_S_xt_mz width(nolock) where zt = '1'";
            return this.FindList<SysNationVEntity>(sql);
        }

    }
}
