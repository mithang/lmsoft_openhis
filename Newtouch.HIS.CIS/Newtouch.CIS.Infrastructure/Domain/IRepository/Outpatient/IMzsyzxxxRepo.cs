using Newtouch.Domain.Entity;
using Newtouch.CIS.Repository;
using System;
using System.Collections.Generic;

namespace Newtouch.Domain.IRepository
{
    /// <summary>
    /// Ngoại trútruyền dịchThực hiệnthông tin
    /// </summary>
    public interface IMzsyzxxxRepo : IRepositoryBase<MzsyzxxxEntity>
    {
        /// <summary>
        /// GửiThực hiện
        /// </summary>
        /// <param name="syIds"></param>
        /// <param name="zt"></param>
        /// <param name="dispenser"></param>
        /// <param name="executor"></param>
        /// <param name="remark"></param>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        string Exec(List<long> syIds, string seatNum, string zt, string dispenser, string executor, DateTime? sykssj, DateTime? syjssj, string remark, string OrganizeId);
        /// <summary>
        /// truyền dịchThu hồiThực hiện
        /// </summary>
        /// <param name="syIds"></param>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        string CanCelExec(List<string> syIds,string OrganizeId);
    }
}
