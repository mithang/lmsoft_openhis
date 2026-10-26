using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    public class SysAccountRevenueAndExpenseRepo : RepositoryBase<SysAccountRevenueAndExpenseEntity>, ISysAccountRevenueAndExpenseRepo
    {
        
        public SysAccountRevenueAndExpenseRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// theo số nhập viện lấy 收支 bản ghi danh sách
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public IList<SysAccountRevenueAndExpenseEntity> GetListByZyhAndYjjzh(int zh, string orgId)
        {
            return this.IQueryable().Where(p =>  p.zhCode == zh && p.OrganizeId == orgId&&p.zt=="1").ToList();
        }


        ///// <summary>
        ///// theo tài khoản lấy số dư tài khoản
        ///// </summary>
        ///// <param name="zh"></param>
        ///// <returns></returns>
        //public SysPatientAccountRevenueAndExpenseEntity GetLastJL(int zh, string orgId)
        //{
        //    var payList = this.IQueryable().Where(p => p.zh == zh && p.OrganizeId == orgId && p.zt == "1")
        //        .OrderByDescending(p => p.zhszjlbh).ToList();
        //    if (payList!=null && payList.Count > 0)
        //    {
        //        return payList[0];
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        /// <summary>
        /// Thêm bản ghi thu chi
        /// 预交 khoản nạp tiền
        /// </summary>
        public void AddPayInfo(SysAccountRevenueAndExpenseEntity sysPatPayInfoEntity, string orgId, string curUserCode, string curUserName)
        {
                 sysPatPayInfoEntity.Create(true);
                this.Insert(sysPatPayInfoEntity);
        }
    }
}
