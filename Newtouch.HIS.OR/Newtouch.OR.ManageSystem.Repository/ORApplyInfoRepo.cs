using Newtouch.OR.ManageSystem.Domain.Entity;
using Newtouch.OR.ManageSystem.Domain.IRepository;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.OR.ManageSystem.Infrastructure;
using Newtouch.Core.Common.Exceptions;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using Newtouch.Core.Common;
using System.Data;

namespace Newtouch.OR.ManageSystem.Repository
{
    /// <summary>
    /// Tạo lập：chl
    /// Ngày tạo：2019-10-31 10:04
    /// Mô tả：Hồ sơ đăng ký phẫu thuật
    /// </summary>
    public class ORApplyInfoRepo : RepositoryBase<ORApplyInfoEntity>, IORApplyInfoRepo
    {
        /// <summary>
        /// Hàm khởi tạo mặc định
        /// </summary>
        /// <param name="databaseFactory">EFFactory ngữ cảnh</param>
        public ORApplyInfoRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
            
        }

        /// <summary>
        /// Lưu biểu mẫu（Thêm mới、Sửa）
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        public int SubmitForm(ORApplyInfoEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var dbEntity = this.FindEntity(keyValue);
                //properties
                
                dbEntity.Modify(keyValue);
                return Update(dbEntity);
            }
            entity.Create(true);
            return Insert(entity);
        }


        


        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        public void DeleteForm(string keyValue,string orgId,string ssmcn)
        {
            //return Delete(p => p.Id == keyValue);
            
            var dbEntity = this.FindEntity(p=>p.Applyno==keyValue && p.OrganizeId==orgId);
            var mc = dbEntity.ssmcn;
            if (dbEntity != null)
            {
                dbEntity.sqzt = ((int)EnumSqzt.yqx).ToString();
                Update(dbEntity);

                var sqlcx = @"select 1 from [Newtouch_CIS].[dbo].[zy_lsyz] 
where yzlx=3  and zyh=" + dbEntity.zyh + " and xmmc='" + ssmcn+"' and ysgh='"+dbEntity.ysgh+"' and  OrganizeId='"+dbEntity.OrganizeId+"' and sssj='"+dbEntity.sssj+"'";
                var a = ExecuteSqlCommand(sqlcx);
                if (a !=0)
                {
                    var sql = @"update [Newtouch_CIS].[dbo].[zy_lsyz]
                                set zt=0
                                where yzlx=3 and yzzt=0 and zyh='"+dbEntity.zyh+"' and xmmc='" + ssmcn + "' and ysgh='" + dbEntity.ysgh + "' and  OrganizeId='" + dbEntity.OrganizeId + "'and sssj='" + dbEntity.sssj + "'";
                    ExecuteSqlCommand(sql);
                }
                
            }
            else
            {
                throw new FailedException("操作失败，未找到相关申请单。");
            }
        }

        /// <summary>
        /// Sau khi xếp lịch phẫu thuật,Cập nhật trạng thái đăng ký(2.Đã xếp lịch)
        /// Sau khi hủy xếp lịch phẫu thuật,Cập nhật trạng thái đăng ký(1.Đã gửi đơn)
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public int UpdateSqzt(string keyValue,string sqzt )
        {
            var dbEntity = this.FindEntity(keyValue);
            //properties
            dbEntity.sqzt = sqzt;
            dbEntity.Modify(keyValue);
            return Update(dbEntity);
        }

    }
}