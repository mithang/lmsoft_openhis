using Newtouch.OR.ManageSystem.Domain.Entity;
using DbConfigHelper;
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
    /// Tạo bởi:chl
    /// Ngày:2019-10-31 10:04
    /// Mô tả:Hồ sơ đơn đăng ký phẫu thuật
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
        /// Lưu biểu mẫu(Thêm mới,Sửa)
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

                // 7-fix: tham so hoa (truoc day noi zyh/xmmc/ysgh/OrganizeId/sssj truc tiep).
                // Giu nguyen ngu nghia cu: ExecuteSqlCommand tra ve so dong/khac 0 thi update.
                var pars = new SugarParameter[] {
                    new SugarParameter("@zyh", dbEntity.zyh ?? ""),
                    new SugarParameter("@ssmcn", ssmcn ?? ""),
                    new SugarParameter("@ysgh", dbEntity.ysgh ?? ""),
                    new SugarParameter("@orgId", dbEntity.OrganizeId ?? ""),
                    new SugarParameter("@sssj", (object)dbEntity.sssj ?? System.DBNull.Value)
                };
                var sqlcx = @"select 1 from " + DbNames.Name("CisDb", "Newtouch_CIS") + @".[dbo].[zy_lsyz]
where yzlx=3 and zyh=@zyh and xmmc=@ssmcn and ysgh=@ysgh and OrganizeId=@orgId and sssj=@sssj";
                var a = ExecuteSqlCommand(sqlcx, pars);
                if (a !=0)
                {
                    var sql = @"update " + DbNames.Name("CisDb", "Newtouch_CIS") + @".[dbo].[zy_lsyz]
                                set zt=0
                                where yzlx=3 and yzzt=0 and zyh=@zyh and xmmc=@ssmcn and ysgh=@ysgh and OrganizeId=@orgId and sssj=@sssj";
                    ExecuteSqlCommand(sql, pars);
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