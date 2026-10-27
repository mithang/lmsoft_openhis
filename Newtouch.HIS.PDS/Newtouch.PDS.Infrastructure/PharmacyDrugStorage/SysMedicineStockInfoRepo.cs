using Newtouch.Infrastructure.EF;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Common;
using SqlSugar;
using System.Linq;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// xt_yp_kcxx
    /// </summary>
    public class SysMedicineStockInfoRepo : RepositoryBase<SysMedicineStockInfoEntity>, ISysMedicineStockInfoRepo
    {
        public SysMedicineStockInfoRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// Xuất kho ra ngoài thay đổi số lượng đóng băng tồn kho
        /// </summary>
        /// <param name="kcId"></param>
        /// <param name="djsl"></param>
        public void UpdateDjsl(string kcId, int djsl)
        {
            var entity = this.FindEntity(kcId);
            if (entity.kcsl - entity.djsl < djsl)
            {
                throw new FailedException("Số lượng khả dụng ít hơn Số lượng đóng băng");
            }
            entity.djsl = entity.djsl + djsl;
            entity.Modify();
            this.Update(entity);

        }

        /// <summary>
        /// get xt_yp_kcxx by sql
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="sqlParameter"></param>
        /// <returns></returns>
        public object FindEntity<T>(string sql, SugarParameter[] sqlParameter)
        {
            return FindList<T>(sql, sqlParameter);
        }

        /// <summary>
        /// get xt_yp_kcxx by yfbmcode,organizeId and pc
        /// </summary>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="pc"></param>
        /// <returns></returns>
        public SysMedicineStockInfoEntity FindEntity(string yfbmCode, string organizeId, string pc)
        {
            return FindEntity(p => p.yfbmCode == yfbmCode && p.OrganizeId == organizeId && p.pc == pc);
        }

        /// <summary>
        /// Sửa trạng thái tồn kho
        /// </summary>
        /// <param name="ypdm"></param>
        /// <param name="ph"></param>
        /// <param name="pc"></param>
        /// <param name="zt"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public int UpdateZt(string ypdm, string ph, string pc, string zt, string yfbmCode, string organizeId)
        {
            var entity = FindEntity(p => p.ph == ph && p.pc == pc && p.ypdm == ypdm && p.yfbmCode == yfbmCode && p.OrganizeId == organizeId);
            if (entity == null) return 0;
            entity.zt = zt.Trim();
            entity.Modify();
            return Update(entity);
        }

        /// <summary>
        /// Tính tồn kho khả dụng Phương thức hiện tại không có nhật ký
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public List<SysMedicineStockInfoEntity> SelectData(string ypCode, string yfbmCode, string organizeId)
        {
            const string sql = @"
		SELECT *
		FROM xt_yp_kcxx(NOLOCK) 
		WHERE ypdm = @ypCode
		AND yfbmCode = @yfbmCode 
		AND tybz = '0' 
		AND kcsl>0
		AND djsl>=0
		AND zt='1'
		AND OrganizeId = @OrganizeId 
";
            var param = new SugarParameter[]
            {
                new SugarParameter("@ypCode", ypCode),
                new SugarParameter("@yfbmCode", yfbmCode),
                new SugarParameter("@OrganizeId", organizeId),
            };
            return _dataContext.Database.SqlQuery<SysMedicineStockInfoEntity>(sql, param).ToList();
        }

        /// <summary>
        /// Tính tồn kho khả dụng Phương thức hiện tại không có nhật ký
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="pc"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="ph"></param>
        /// <returns></returns>
        public List<SysMedicineStockInfoEntity> SelectData(string ypCode, string ph, string pc, string yfbmCode, string organizeId)
        {
            const string sql = @"
		SELECT *
		FROM xt_yp_kcxx(NOLOCK) 
		WHERE ypdm = @ypCode
		AND yfbmCode = @yfbmCode 
		AND tybz = '0' 
		AND kcsl>0
		AND djsl>=0
		AND pc=@pc
		AND ph=@ph
		AND zt='1'
		AND OrganizeId = @OrganizeId 
";
            var param = new SugarParameter[]
            {
                new SugarParameter("@ypCode", ypCode),
                new SugarParameter("@yfbmCode", yfbmCode),
                new SugarParameter("@OrganizeId", organizeId),
                new SugarParameter("@pc", pc),
                new SugarParameter("@ph", ph)
            };
            return _dataContext.Database.SqlQuery<SysMedicineStockInfoEntity>(sql, param).ToList();
        }

        /// <summary>
        /// Giảm tồn kho đóng băng
        /// </summary>
        /// <param name="sl">Số lượng đơn vị nhỏ nhất Số tồn kho cần trừ</param>
        /// <param name="pc"></param>
        /// <param name="ph"></param>
        /// <param name="ypCode"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public int SubtractForzenKc(int sl, string pc, string ph, string ypCode, string yfbmCode, string organizeId, string userCode)
        {
            const string sql = @"
UPDATE dbo.xt_yp_kcxx SET djsl=djsl-@sl, LastModifyTime=GETDATE(), LastModifierCode=@userCode
WHERE pc=@pc AND ph=@ph AND ypdm=@ypCode AND OrganizeId=@OrganizeId AND yfbmCode=@yfbmCode AND zt='1' AND tybz='0'
";
            var param = new SugarParameter[]
            {
                new SugarParameter("@sl", sl ),
                new SugarParameter("@userCode", userCode),
                new SugarParameter("@pc", pc),
                new SugarParameter("@ph",ph ),
                new SugarParameter("@ypCode",ypCode ),
                new SugarParameter("@OrganizeId", organizeId),
                new SugarParameter("@yfbmCode", yfbmCode)
            };
            return _dataContext.Database.ExecuteSqlCommand(sql, param);
        }

        /// <summary>
        /// Giảm tồn kho đóng băng
        /// </summary>
        /// <param name="sl">Số lượng đơn vị nhỏ nhất Số tồn kho cần trừ</param>
        /// <param name="kcId"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public int SubtractForzenKc(int sl, string kcId ,string organizeId, string userCode)
        {
            const string sql = @"
UPDATE dbo.xt_yp_kcxx SET djsl=djsl-@sl, LastModifyTime=GETDATE(), LastModifierCode=@userCode
WHERE kcId=@kcId AND OrganizeId=@OrganizeId AND zt='1' AND tybz='0'
";
            var param = new SugarParameter[]
            {
                new SugarParameter("@sl", sl ),
                new SugarParameter("@userCode", userCode),
                new SugarParameter("@kcId", kcId),
                new SugarParameter("@OrganizeId", organizeId)
            };
            return _dataContext.Database.ExecuteSqlCommand(sql, param);
        }

        /// <summary>
        /// Giảmdjslvàkcsl
        /// </summary>
        /// <param name="sl">Số lượng đơn vị nhỏ nhất Số tồn kho cần trừ</param>
        /// <param name="pc"></param>
        /// <param name="ph"></param>
        /// <param name="ypCode"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public int SubtractDjslAndKcsl(int sl, string pc, string ph, string ypCode, string yfbmCode, string organizeId, string userCode)
        {
            const string sql = @"
UPDATE dbo.xt_yp_kcxx SET djsl=djsl-@sl, kcsl=kcsl-@sl, LastModifyTime=GETDATE(), LastModifierCode=@userCode
WHERE pc=@pc AND ph=@ph AND ypdm=@ypCode AND OrganizeId=@OrganizeId AND yfbmCode=@yfbmCode AND zt='1' AND tybz='0'
";
            var param = new SugarParameter[]
            {
                new SugarParameter("@sl", sl ),
                new SugarParameter("@userCode", userCode),
                new SugarParameter("@pc", pc),
                new SugarParameter("@ph",ph ),
                new SugarParameter("@ypCode",ypCode ),
                new SugarParameter("@OrganizeId", organizeId),
                new SugarParameter("@yfbmCode", yfbmCode)
            };
            return _dataContext.Database.ExecuteSqlCommand(sql, param);
        }

        /// <summary>
        /// Thay đổi Số lượng tồn kho
        /// </summary>
        /// <param name="sl">Số lượng đơn vị nhỏ nhất +：Tăng tồn kho  -：Giảm tồn kho</param>
        /// <param name="pc"></param>
        /// <param name="ph"></param>
        /// <param name="ypCode"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public int UpdateKcslWithTrans(int sl, string pc, string ph, string ypCode, string yfbmCode, string organizeId, string userCode, Newtouch.Infrastructure.EFDbTransaction db)
        {
            const string sql = @"
UPDATE dbo.xt_yp_kcxx SET kcsl=kcsl+@sl, LastModifyTime=GETDATE(), LastModifierCode=@userCode
WHERE pc=@pc AND ph=@ph AND ypdm=@ypCode AND OrganizeId=@OrganizeId AND yfbmCode=@yfbmCode AND zt='1' AND tybz='0'
";
            var param = new SugarParameter[]
            {
                new SugarParameter("@sl", sl ),
                new SugarParameter("@userCode", userCode),
                new SugarParameter("@pc", pc),
                new SugarParameter("@ph",ph ),
                new SugarParameter("@ypCode",ypCode ),
                new SugarParameter("@OrganizeId", organizeId),
                new SugarParameter("@yfbmCode", yfbmCode)
            };
            return db.ExecuteSqlCommand(sql, param);
        }


        public int UpdateExpired(string kcId, string yxq)
        {
            var entity = this.FindEntity(kcId);
            entity.yxq = Convert.ToDateTime(yxq);
            entity.Modify();
            return this.Update(entity);

        }
    }
}
