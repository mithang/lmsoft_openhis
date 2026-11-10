using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Exceptions;
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
                sql += " and ddzt=@ddzt";//Đã báo cáo
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
            if (dbEntity == null) throw new FailedException("Không tìm thấy đơn mua hàng.");
            //properties
            dbEntity.zt = "0";
            dbEntity.Modify(cgId);
            this.Update(dbEntity);

        }
        public void PurchaseStateUpdate(string cgId, int ddzt, string orgId)
        {
            var dbEntity = this.FindEntity(p => p.cgId == cgId);
            if (dbEntity == null) throw new FailedException("Không tìm thấy đơn mua hàng.");
            // G15: base probe ddzt=9 -> Success + ghi ddzt=9; ddzt tren don da xoa
            // (zt=0) -> Success + doi ddzt. Chi nhan 1 Da luu / 2 Da truyen bao /
            // 3 Da nghiem thu (comment code + UI Index.cshtml chi gui 3), tu choi
            // don da xoa. Bao ve ca 3 caller: controller, PurchaseUpload, SureTest.
            if (ddzt != 1 && ddzt != 2 && ddzt != 3) throw new FailedException("Trạng thái đơn hàng không hợp lệ.");
            if (dbEntity.zt != "1") throw new FailedException("Đơn mua hàng đã bị xóa, không thể cập nhật trạng thái.");
            //properties
            dbEntity.ddzt = ddzt;
            dbEntity.Modify(cgId);
            this.Update(dbEntity);

        }

        /// <summary>
        /// Cập nhật mã đơn hàng của đơn mua sắm
        /// </summary>
        /// <param name="cgId"></param>
        /// <param name="ddbh"></param>
        /// <param name="orgId"></param>

        public void PurchaseDdbhUpdate(string cgId, string ddbh, string orgId)
        {
            var dbEntity = this.FindEntity(p => p.cgId == cgId);
            if (dbEntity == null) throw new FailedException("Không tìm thấy đơn mua hàng.");
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
                if (dbEntity == null) throw new FailedException("Không tìm thấy đơn mua hàng.");
                //properties
                // G15: base probe edit that -> header KHONG doi gi (dbEntity.cgId =
                // entity.cgId voi entity.cgId null vi form khong post cgId -> Update
                // 0 dong, im lang) nhung controller van DeleteItem + InsertItem ->
                // chi tiet cu bi xoa, chi tiet moi loi zt NULL -> mat het chi tiet.
                // Giu cgId cu, chi copy field form that su post; ddbh/yyjhdh la so
                // he thong (ddbh do YY111 cap, yyjhdh do repo sinh luc tao) nen
                // khong copy tu form (form post ddbh=" ", yyjhdh="test" se wipe
                // so that); ddzt/zt giu gia tri DB (form luon post ddzt=1).
                // ddsj do repo quan ly nhu nhanh tao (format da sua HH:mm:ss).
                dbEntity.psdbm = entity.psdbm;
                dbEntity.ddlx = entity.ddlx;
                dbEntity.zwdhrq = entity.zwdhrq;
                dbEntity.cgfs = entity.cgfs;
                dbEntity.xtbm = entity.xtbm;
                dbEntity.sfhbsfw = entity.sfhbsfw;
                dbEntity.yybm = entity.yybm;
                dbEntity.czlx = entity.czlx;
                dbEntity.jls = entity.jls;
                dbEntity.ddsj = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                dbEntity.Modify(keyValue);
                this.Update(dbEntity);
                return keyValue;
            }
            else
            {
                entity.cgId = Guid.NewGuid().ToString();
                // G15: format cu "yyyy-MM-dd HH:ss:mm" dao phut/giay (bang chung
                // G6: CreateTime 13:40:20 -> ddsj "13:20:40").
                entity.ddsj = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                //entity.ddbh = "DHMS" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                // G15: "KHMS"+yyyyMMddHHmmssfff = 21 ky tu > cot yyjhdh
                // varchar(20) -> moi create deu loi "String or binary data would
                // be truncated" (base probe d6-real/d7-real). Dung centisecond
                // (ff) cho vua 20 ky tu; prefix KHMS giu nguyen, khong doi
                // GetNewReceiptNo. yyjhdh khong phai khoa duy nhat.
                entity.yyjhdh = "KHMS" + DateTime.Now.ToString("yyyyMMddHHmmssff");
                entity.ddzt = 1; //1 Đã lưu
                // G15: base probe sau khi sua truncation se lo loi zt NULL
                // (Create(true) khong set zt, cot zt char(1) NOT NULL) — cung
                // lop G12 #5 / G9 #8.
                entity.zt = "1";
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
