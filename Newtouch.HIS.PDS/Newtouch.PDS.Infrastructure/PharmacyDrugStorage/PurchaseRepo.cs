using Newtouch.HIS.Domain.DTO.PharmacyDrugStorage;
using FrameworkBase.MultiOrg.Infrastructure;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Exceptions;
using FrameworkBase.MultiOrg.Repository;
using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity.PharmacyDrugStorage;
using Newtouch.HIS.Domain.IRepository.PharmacyDrugStorage;
using Newtouch.HIS.Domain.VO;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using SqlSugar;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Repository.PharmacyDrugStorage
{

    public class PurchaseRepo : RepositoryBase<PurchaseEntity>, IPurchaseRepo
    {
        public PurchaseRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public IList<PurchaseVo> GetPurchaseGridJson(Pagination pagination, DateTime kssj, DateTime jssj, string OrganizeId,int ddzt, string yyjhdjh = null, string gysCode = null, string ddbh = null, string ddyy = null)
        {
            var sql =new StringBuilder(@"select  cg.cgId,cg.OrganizeId,cg.ddsj,cg.czlx,cg.yybm,cg.psdbm,cg.ddlx,cg.ddbh,cg.ddbh fph,cg.yyjhdh,
cg.zwdhrq,cg.jls,cg.ddzt,cg.CreateTime,cg.CreatorCode,cg.gysCode,cg.gysName,sum(isnull(cgmx.zje,convert(decimal(18,2),cgsl*cgdj))) zje 
from xt_yp_cg cg (nolock)
left join xt_yp_cgmx cgmx (nolock) on cgmx.cgId=cg.cgId and cgmx.OrganizeId=cg.OrganizeId
where cg.zt=1 and cg.organizeId=@OrganizeId
and cg.createtime BETWEEN @kssj  AND  @jssj+' 23:59:59' ");

            if (ddzt != 0)
            {
                sql.Append(" and ddzt=@ddzt");
            }
            if (!string.IsNullOrWhiteSpace(yyjhdjh))
            {
                sql.Append(" and yyjhdh like @yyjhdh");
            }
            if (!string.IsNullOrWhiteSpace(ddbh))
            {
                sql.Append(" and ddbh like @ddbh");
            }
            if (!string.IsNullOrWhiteSpace(gysCode))
            {
                sql.Append(" and gysCode in (select col from f_split(@gysCode,',')) ");
            }
            if (!string.IsNullOrWhiteSpace(ddyy))
            {
                sql.Append(@" and  not exists (select * from xt_yp_crkmx rkdjmx,xt_yp_crkdj rkdj where rkdjmx.fph=cg.ddbh 

    and rkdj.crkId = rkdjmx.crkId and rkdj.shzt != '2' and rkdj.OrganizeId =@OrganizeId )");
            }
            sql.Append(@" group by cg.cgId,cg.OrganizeId,cg.ddsj,cg.czlx,cg.yybm,cg.psdbm,cg.ddlx,cg.ddbh,cg.yyjhdh,
cg.zwdhrq, cg.jls, cg.ddzt, cg.CreateTime, cg.CreatorCode, cg.gysCode, cg.gysName");
            var parms = new List<SugarParameter>
            {
                new SugarParameter("@OrganizeId", OrganizeId),
                new SugarParameter("@kssj", kssj),
                new SugarParameter("@jssj", jssj),
                new SugarParameter("@ddzt", ddzt),
                new SugarParameter("@ddbh", ddbh+'%'),
                new SugarParameter("@yyjhdh", yyjhdjh+'%'),
                new SugarParameter("@gysCode", gysCode??""),
            };

            return QueryWithPage<PurchaseVo>(sql.ToString(), pagination, parms.ToArray());
        }

        public void PurchaseDelete(string cgId, string orgId )
        {
                //G27-F3: lọc org (base xóa được row cross-org) + chỉ xóa khi ddzt=1 (base xóa cả đơn đã duyệt/truyền báo, UI chỉ cho xóa khi đã lưu)
                var dbEntity = this.FindEntity(p => p.cgId == cgId && p.OrganizeId == orgId && p.zt == "1");
                if (dbEntity == null) throw new FailedException("Không tìm thấy đơn mua hàng.");
                if (dbEntity.ddzt != 1) throw new FailedException("Chỉ được xóa đơn hàng đang ở trạng thái đã lưu.");
                //properties
                dbEntity.zt = "0";
                dbEntity.Modify(cgId);
                this.Update(dbEntity);
           
        }
        /// <summary>
        /// Sửa hàng loạt trạng thái duyệt
        /// </summary>
        /// <param name="cgId"></param>
        /// <param name="ddzt"></param>
        /// <param name="orgId"></param>
        public void PurchaseStateUpdate(string cgId,int ddzt, string orgId)
        {
            //G27-F4: base: cgId bogus/rỗng vẫn Success (không check row-count), ddzt=99/0 ghi thẳng, không check trạng thái hiện tại.
            //Máy trạng thái suy từ UI (Index + PurchaseApproval + DjshUpdate): 1→2 (phê duyệt), 1→3 (hủy bỏ), 2→1 (thu hồi), 2→3 (nghiệm thu); 3 là terminal.
            if (string.IsNullOrWhiteSpace(cgId)) throw new FailedException("Mã đơn mua hàng không được để trống.");
            if (ddzt < 1 || ddzt > 3) throw new FailedException("Trạng thái đơn hàng không hợp lệ.");
            var ids = cgId.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).Distinct().ToList();
            if (ids.Count == 0) throw new FailedException("Mã đơn mua hàng không được để trống.");
            foreach (var id in ids)
            {
                var row = this.FindEntity(p => p.cgId == id && p.OrganizeId == orgId && p.zt == "1");
                if (row == null) throw new FailedException("Không tìm thấy đơn mua hàng.");
                var cur = row.ddzt;
                bool ok = (cur == 1 && (ddzt == 2 || ddzt == 3)) || (cur == 2 && (ddzt == 1 || ddzt == 3));
                if (!ok) throw new FailedException($"Đơn hàng đang ở trạng thái {cur}, không thể chuyển sang trạng thái {ddzt}.");
            }
            string sqlstr = "update [xt_yp_cg] set ddzt=@ddzt,LastModifyTime=GETDATE(),LastModifierCode=@userCode  where OrganizeId=@orgId and zt='1' and cgId in (select col from f_split(@cgId,','))";
            var affected = ExecuteSqlCommand(sqlstr, new SugarParameter("@cgId", cgId), new SugarParameter("@ddzt", ddzt), new SugarParameter("@userCode", OperatorProvider.GetCurrent().UserCode), new SugarParameter("@orgId", orgId));
            if (affected != ids.Count) throw new FailedException("Cập nhật trạng thái đơn hàng thất bại.");
            //var dbEntity = this.FindEntity(cgId);
            ////properties
            //dbEntity.ddzt = ddzt;
            //dbEntity.Modify(cgId);
            //this.Update(dbEntity);

        }

        /// <summary>
        /// Cập nhật Số đơn hàng của đơn mua hàng
        /// </summary>
        /// <param name="cgId"></param>
        /// <param name="ddbh"></param>
        /// <param name="orgId"></param>

        public void PurchaseDdbhUpdate(string cgId, string ddbh, string orgId)
        {
            //G27-F3: lọc org (base sửa được row cross-org) + validate ddbh (base: rỗng → NULL, dài >20 → truncation; YY009 fail còn gán message lỗi dài vào đây)
            var dbEntity = this.FindEntity(p => p.cgId == cgId && p.OrganizeId == orgId && p.zt == "1");
            if (dbEntity == null) throw new FailedException("Không tìm thấy đơn mua hàng.");
            if (string.IsNullOrWhiteSpace(ddbh)) throw new FailedException("Số đơn hàng không được để trống.");
            if (ddbh.Length > 20) throw new FailedException("Số đơn hàng không vượt quá 20 ký tự.");
            //properties
            dbEntity.ddbh = ddbh;
            dbEntity.Modify(cgId);
            this.Update(dbEntity);

        }
        public string SubmitForm(PurchaseEntity entity,string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                //G27-F2: lọc org (base sửa được row cross-org) + merge-skip-null (base: form Form.cshtml không post gysCode/gysName → edit xóa trắng 2 field; họ G2).
                //Trade-off: không thể clear field về null qua form — UI không có luồng clear 2 field này.
                var dbEntity = this.FindEntity(p => p.cgId == keyValue && p.OrganizeId == entity.OrganizeId && p.zt == "1");
                if (dbEntity == null) throw new FailedException("Không tìm thấy đơn mua hàng.");
                if (entity.ddlx != null) dbEntity.ddlx = entity.ddlx;
                if (entity.czlx != null) dbEntity.czlx = entity.czlx;
                dbEntity.jls = entity.jls;
                if (entity.gysCode != null) dbEntity.gysCode = entity.gysCode;
                if (entity.gysName != null) dbEntity.gysName = entity.gysName;
                dbEntity.Modify(keyValue);
                this.Update(dbEntity);
                return keyValue;
            }
            else
            {
                entity.cgId = Guid.NewGuid().ToString();
                entity.ddsj = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                entity.ddzt = 1; //1Đã lưu
                entity.Create(true);
                this.Insert(entity);
                return entity.cgId;
            }
        }

    }
}
