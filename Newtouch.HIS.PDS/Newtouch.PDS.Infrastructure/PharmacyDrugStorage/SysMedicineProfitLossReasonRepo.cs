using Newtouch.HIS.Domain.Entity;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Core.Common;
using System.Text;
using System.Data.Common;
using SqlSugar;
using System;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// xt_ypsyyy
    /// </summary>
    public class SysMedicineProfitLossReasonRepo : RepositoryBase<SysMedicineProfitLossReasonEntity>, ISysMedicineProfitLossReasonRepo
    {
        public SysMedicineProfitLossReasonRepo(IDefaultDatabaseFactory databaseFactory): base(databaseFactory)
        {
        }

        /// <summary>
        /// Truy vấn lý do lãi lỗ theo loại lãi lỗ
        /// </summary>
        /// <returns></returns>
        public List<SysMedicineProfitLossReasonEntity> GetLossProfitReasonListByType(string sylx)
        {
            List<SysMedicineProfitLossReasonEntity> list = null;
            string sylxstr = sylx + ",-1";
            if (!string.IsNullOrEmpty(sylx))
            {
                list = this.IQueryable().Where(a => new[] { sylx,"-1" }.Contains(a.sybz) && a.zt == "1").ToList();
            }
            else
            {
                list = this.IQueryable().Where(a => a.zt == "1").ToList();
            }
            return list;
        }

        /// <summary>
        /// Lấy danh sách thực thể phân trang
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysMedicineProfitLossReasonEntity> GetPagintionList(Pagination pagination, string keyword)
        {
            var sql = new StringBuilder(@"
            SELECT [syyyId]
                  ,[syyy]
                  ,[sybz]
                  ,[zt]
                  ,[px]
                  ,[CreatorCode]
                  ,[CreateTime]
                  ,[LastModifyTime]
                  ,[LastModifierCode]
              FROM [xt_ypsyyy](nolock) where 1=1
            ");
            var pars = new List<SugarParameter>();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql.Append(" and(1 = 2");
                sql.Append(" or syyy like @keyword");
                sql.Append(")");
                pars.Add(new SugarParameter("@keyword", "%" + keyword.Trim() + "%"));
            }
            return QueryWithPage<SysMedicineProfitLossReasonEntity>(sql.ToString(), pagination, pars.ToArray());
        }

        // G35-F2: zt chuan hoa ("true"/"1" -> "1"); G35-F5: edit merge-skip-null, tra so dong.
        private static string MapZt(string zt) => (zt == "true" || zt == "1") ? "1" : "0";

        public int SubmitForm(SysMedicineProfitLossReasonEntity SyyyDTO, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var db = FindEntity(p => p.syyyId == keyValue);
                if (db == null) return 0;
                if (SyyyDTO.syyy != null) db.syyy = SyyyDTO.syyy;
                if (SyyyDTO.sybz != null) db.sybz = SyyyDTO.sybz;
                if (SyyyDTO.px.HasValue) db.px = SyyyDTO.px;
                if (SyyyDTO.zt != null) db.zt = MapZt(SyyyDTO.zt);
                db.Modify();
                return Update(db);
            }
            else
            {
                var entity = new SysMedicineProfitLossReasonEntity
                {
                    syyy = SyyyDTO.syyy,
                    sybz = SyyyDTO.sybz,
                    px = SyyyDTO.px,
                    zt = MapZt(SyyyDTO.zt)
                };
                entity.Create(true);
                entity.zt = MapZt(SyyyDTO.zt); // Create(true) ghi de zt="1" — gan lai gia tri that (G35 1a2)
                return Insert(entity);
            }
        }
        public int DeleteForm(string keyValue)
        {
            return Delete(p => p.syyyId == keyValue);
        }
    }
}
