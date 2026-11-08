using Newtouch.Domain.Entity;
using Newtouch.Domain.ViewModels;
using Newtouch.CIS.Repository;
using System;
using System.Collections.Generic;

namespace Newtouch.Domain.IRepository
{
    /// <summary>
    /// đặt hẹn đăng ký khám
    /// </summary>
    public interface ISysBespeakRegisterRepo : IRepositoryBase<SysBespeakRegisterEntity>
    {

        /// <summary>
        /// Lấy khi đoạn
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="regDate"></param>
        /// <param name="gh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<RegTimeVO> GetRegTime(string deptCode, DateTime regDate, string gh, string organizeId);
    }
}
