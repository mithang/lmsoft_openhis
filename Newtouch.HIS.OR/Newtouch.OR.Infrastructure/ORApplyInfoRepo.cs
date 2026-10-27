using Newtouch.OR.ManageSystem.Domain.Entity;
using Newtouch.OR.ManageSystem.Domain.IRepository;
using Newtouch.OR.ManageSystem.Infrastructure;
using NewtouchHIS.Lib.Base.Exceptions;
using SqlSugar;
using System.Collections.Generic;
using System.Linq;
using NewtouchHIS.Lib.Base.Model;
using System.Data;

namespace Newtouch.OR.ManageSystem.Repository
{
    /// <summary>
    /// Tạo bởi：chl
    /// Ngày：2019-10-31 10:04
    /// Mô tả：Hồ sơ đơn đăng ký phẫu thuật
    /// </summary>
    public class ORApplyInfoRepo : RepositoryBase<ORApplyInfoEntity>, IORApplyInfoRepo
    {
        /// <summary>
        /// Hàm tạo mặc định
        /// </summary>
        /// <param name="databaseFactory">EFFactory ngữ cảnh</param>
        public ORApplyInfoRepo()
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
                
                EntityHelper.Modify(dbEntity, keyValue);
                return Update(dbEntity);
            }
            EntityHelper.Create(entity, true);
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
                throw new FailedException("Thao tác thất bại, không tìm thấy đơn liên quan.");
            }
        }

        /// <summary>
        /// Xếp lịch phẫu thuậtsau,Cập nhật trạng thái đơn đăng ký(2.Đã xếp lịch)
        /// HủyXếp lịch phẫu thuậtsau,Cập nhật trạng thái đơn đăng ký(1.Đã đăng ký)
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public int UpdateSqzt(string keyValue,string sqzt )
        {
            var dbEntity = this.FindEntity(keyValue);
            //properties
            dbEntity.sqzt = sqzt;
            EntityHelper.Modify(dbEntity, keyValue);
            return Update(dbEntity);
        }

    }
}