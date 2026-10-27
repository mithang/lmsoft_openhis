using NewtouchHIS.Lib.Base.Model;
using Newtouch.Herp.Domain.Entity;
using SqlSugar;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Domain.Entity.Purchase;
using Newtouch.Herp.Domain.IRepository.Purchase;
using Newtouch.Herp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Herp.Repository.Purchase
{
    public class PurchaseRepo : RepositoryBase<PurchaseEntity>, IPurchaseRepo
    {
        public PurchaseRepo()
        {
        }

        public IList<PurchaseEntity> GetPurchaseGridJson(Pagination pagination, DateTime kssj, DateTime jssj, string OrganizeId, int ddzt)
        {
            var sql = @"select * from xt_wz_cg
where zt=1 and organizeId=@OrganizeId
and createtime BETWEEN @kssj  AND  @jssj+' 23:59:59' ";
            if (ddzt != 0)
            {
                sql += " and ddzt=@ddzt";//已传报
            }
            var parms = new List<SugarParameter>
            {
                new SugarParameter("@OrganizeId", OrganizeId),
                new SugarParameter("@kssj", kssj),
                new SugarParameter("@jssj", jssj),
                new SugarParameter("@ddzt", ddzt),
            };

            return QueryPage<PurchaseEntity>(sql, pagination, parms.ToArray());
        }

        public void PurchaseDelete(string cgId, string orgId)
        {
            var dbEntity = this.FindEntity(p => p.cgId == cgId);
            //properties
            dbEntity.zt = "0";
            dbEntity.Modify(cgId);
            this.Update(dbEntity);

        }
        public void PurchaseStateUpdate(string cgId, int ddzt, string orgId)
        {
            var dbEntity = this.FindEntity(p => p.cgId == cgId);
            //properties
            dbEntity.ddzt = ddzt;
            dbEntity.Modify(cgId);
            this.Update(dbEntity);

        }

        /// <summary>
        /// 更新采购单的订单编号
        /// </summary>
        /// <param name="cgId"></param>
        /// <param name="ddbh"></param>
        /// <param name="orgId"></param>

        public void PurchaseDdbhUpdate(string cgId, string ddbh, string orgId)
        {
            var dbEntity = this.FindEntity(p => p.cgId == cgId);
            //properties
            dbEntity.ddbh = ddbh;
            dbEntity.Modify(cgId);
            this.Update(dbEntity);

        }

        public string SubmitForm(PurchaseEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var dbEntity = this.FindEntity(p => p.cgId == keyValue);
                //properties
                dbEntity.cgId = entity.cgId;
                dbEntity.czlx = entity.czlx;
                dbEntity.jls = entity.jls;
                dbEntity.Modify(keyValue);
                this.Update(dbEntity);
                return keyValue;
            }
            else
            {
                entity.cgId = Guid.NewGuid().ToString();
                entity.ddsj = DateTime.Now.ToString("yyyy-MM-dd HH:ss:mm");
                //entity.ddbh = "CG" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                entity.yyjhdh = "CG" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                entity.ddzt = 1; //1已保存
                //entity.zt = "1";
                //entity.CreatorCode = user.rygh;
                //entity.CreateTime = DateTime.Now;
                //cgEntity.LastModifierCode = "";
                //cgEntity.LastModifyTime = null;
                entity.Create(true);
                this.Insert(entity);
                return entity.cgId;
            }
        }

    
        private IList<T> QueryPage<T>(string sql, NewtouchHIS.Lib.Base.Model.Pagination pagination, SugarParameter[] parms) where T : class, new()
        {
            if (pagination.rows == null || pagination.rows <= 0) pagination.rows = 15;
            if (pagination.page == null || pagination.page <= 0) pagination.page = 1;
            int total = Db.Ado.SqlQuery<int>("SELECT COUNT(1) FROM (" + sql + ") AS _t", parms).FirstOrDefault();
            pagination.records = total;
            string pageSql = sql + " ORDER BY (SELECT NULL) OFFSET " + ((pagination.page.Value - 1) * pagination.rows.Value) + " ROWS FETCH NEXT " + pagination.rows.Value + " ROWS ONLY";
            return Db.SqlQueryable<T>(pageSql).AddParameters(parms).ToList();
        }
    }
}
