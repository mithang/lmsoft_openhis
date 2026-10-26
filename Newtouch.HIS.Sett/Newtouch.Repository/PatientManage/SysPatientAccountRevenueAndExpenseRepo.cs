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
    public class SysPatientAccountRevenueAndExpenseRepo : RepositoryBase<SysPatientAccountRevenueAndExpenseEntity>, ISysPatientAccountRevenueAndExpenseRepo
    {
        
        public SysPatientAccountRevenueAndExpenseRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// theo số nhập viện lấy thu chi bản ghi danh sách
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public IList<SysPatientAccountRevenueAndExpenseEntity> GetListByZyhAndYjjzh(string zyh, int zh, string orgId)
        {
            return this._dataContext.Set<SysPatientAccountRevenueAndExpenseEntity>().Where(p => p.zyh == zyh && p.zh == zh && p.OrganizeId == orgId).ToList();
        }


        /// <summary>
        /// theo tài khoản lấy số dư tài khoản
        /// </summary>
        /// <param name="zh"></param>
        /// <returns></returns>
        public SysPatientAccountRevenueAndExpenseEntity GetLastJL(int zh, string orgId)
        {
            var payList = this.IQueryable().Where(p => p.zh == zh && p.OrganizeId == orgId && p.zt == "1")
                .OrderByDescending(p => p.zhszjlbh).ToList();
            if (payList!=null && payList.Count > 0)
            {
                return payList[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Thêm bản ghi thu chi
        /// tạm ứng khoản nạp tiền
        /// </summary>
        public void AddPayInfo(SysPatientAccountRevenueAndExpenseEntity sysPatPayInfoEntity, string orgId, string curUserCode, string curUserName)
        {
                sysPatPayInfoEntity.CreatorCode = curUserCode;
                sysPatPayInfoEntity.CreatorUserName = curUserName;
                sysPatPayInfoEntity.CreateTime = DateTime.Now;
                sysPatPayInfoEntity.OrganizeId = orgId;
                sysPatPayInfoEntity.zhszjlbh = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("zhszjlbh");
                this.Insert(sysPatPayInfoEntity);
             
           
        }
         
    }
}
