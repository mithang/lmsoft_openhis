using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.DomainServices;
using Newtouch.HIS.Repository;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using System;
using NewtouchHIS.Lib.Base.Exceptions;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysUserStaffRepo : RepositoryBase<SysUserStaffEntity>, ISysUserStaffRepo
    {
        public SysUserStaffRepo()
            
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="staffIds"></param>
        public void submitUserStaff(string userId, string staffIds)
        {
            using (var db = new DbTrans())
            {
                var staffIdArr = (staffIds??"").Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Distinct();
                foreach (var staffId in staffIdArr)
                {
                    if (db.FindEntity<SysUserStaffEntity>(p => p.UserId != userId && p.StaffId == staffId) != null) {
                        throw new FailedException("Nhân viên hệ thống bị gán trùng");
                    }
                }
                //Trước del
                db.Delete<SysUserStaffEntity>(p => p.UserId == userId);
                //Thêm tiếp
                foreach (var staffId in staffIdArr)
                {
                    var userStaffentity = new SysUserStaffEntity();
                    userStaffentity.Id = Guid.NewGuid().ToString();
                    userStaffentity.UserId = userId;
                    userStaffentity.StaffId = staffId;
                    userStaffentity.Create();
                    db.Insert(userStaffentity);
                }
                db.Commit();
            }
        }
    }
}
