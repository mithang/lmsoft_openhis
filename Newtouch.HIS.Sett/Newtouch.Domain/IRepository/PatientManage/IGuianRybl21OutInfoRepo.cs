using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects.PatientManage;
using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IGuianRybl21OutInfoRepo : IRepositoryBase<GuianRybl21OutInfoEntity>
    {
        /// <summary>
        /// Lấy thông tin phản hồi BHYT nội trú Quý An theo số nhập viện
        /// </summary>
        /// <param name="zyh">Số nhập viện</param>
        /// <param name="OrganizeId">Tổ chứcID</param>
        /// <returns></returns>
        GuianRybl21OutInfoEntity GetInfoByZyh(string zyh, string OrganizeId);
    }
}
