using Newtouch.OR.ManageSystem.Domain.Entity;
using Newtouch.OR.ManageSystem.Domain.IRepository;
using System.Linq;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Model;
using System.Collections.Generic;
using System.Text;
using SqlSugar;

namespace Newtouch.OR.ManageSystem.Repository
{
    /// <summary>
    /// 创 建：chl
    /// 日 期：2019-11-06 10:22
    /// 描 述：手术排班记录
    /// </summary>
    public class ORArrangementRepo : RepositoryBase<ORArrangementEntity>, IORArrangementRepo
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="databaseFactory">EF上下文工厂</param>
        public ORArrangementRepo()
        {
            
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        public int SubmitForm(ORArrangementEntity entity, string ApplyId,string ArrangeId)
        {
			if (!string.IsNullOrEmpty(ArrangeId))
			{
				var dbEntity = this.FindEntity(ArrangeId);
				//properties
				dbEntity.AnesCode = entity.AnesCode;
				dbEntity.ApplyId = ApplyId;
				dbEntity.Applyno = entity.Applyno;
				dbEntity.bq = entity.bq;
				dbEntity.ch = entity.ch;
				dbEntity.ks = entity.ks;
				dbEntity.oporder = entity.oporder;
				dbEntity.oproom = entity.oproom;
				dbEntity.OrganizeId = entity.OrganizeId;
				dbEntity.sqzt = "2";
				dbEntity.ssbw = entity.ssbw;
				dbEntity.ssdm = entity.ssdm;
				dbEntity.ssmc = entity.ssmc;
				dbEntity.sssj = entity.sssj;
				dbEntity.ssxh = entity.ssxh;
				dbEntity.xhhs = entity.xhhs;
				dbEntity.xm = entity.xm;
				dbEntity.xshs = entity.xshs;
				dbEntity.ysgh = entity.ysgh;
				dbEntity.ysxm = entity.ysxm;
				dbEntity.zd = entity.zd;
				dbEntity.zlys1 = entity.zlys1;
				dbEntity.zlys2 = entity.zlys2;
				dbEntity.zlys3 = entity.zlys3;
				dbEntity.zlys4 = entity.zlys4;
				dbEntity.zt = entity.zt;
				dbEntity.zyh = entity.zyh;
				EntityHelper.Modify(dbEntity, ArrangeId);
				return Update(dbEntity);
			}
			else
			{
				if (this.IQueryable().Any(p => p.ApplyId == ApplyId && p.zt == "1"))
				{
					throw new FailedException("申请Mã không được trùng lặp");
				}
				else
				{
					entity.ApplyId = ApplyId;
					entity.sqzt = "2";
					EntityHelper.Create(entity, true);
					return Insert(entity);
				}
			}
		}

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        public int DeleteForm(string keyValue)
        {
            var dbEntity = this.FindEntity(keyValue);
            //properties
            dbEntity.sqzt = "3";
            EntityHelper.Modify(dbEntity, keyValue);
            return Update(dbEntity);
        }


        /// <summary>
        /// 手术登记页,获取已排班已登记状态的手术排班列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<ORArrangementEntity> GetPagintionListForRegistration(Pagination pagination, string keyword, string bq,string OrganizeId)
        {
            var sql = new StringBuilder();
            sql.Append("select * from OR_Arrangement(nolock) where  zt != 0");
            sql.Append(" and sqzt in(2,4)");
            List<SugarParameter> pars = null;
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                pars = pars ?? new List<SugarParameter>();
                sql.Append(" and(1 = 2");
                sql.Append(" or xm like @keyword");
                sql.Append(" or zyh like @keyword");
                sql.Append(")");
                pars.Add(new SugarParameter("@keyword", "%" + keyword.Trim() + "%"));
			}
			if (!string.IsNullOrWhiteSpace(OrganizeId))
			{
				sql.Append(" and OrganizeId=@OrganizeId");
				pars.Add(new SugarParameter("@OrganizeId", OrganizeId.Trim()));
			}
			if (!string.IsNullOrWhiteSpace(bq))
            {
                sql.Append(" and bq=@bq");
                pars.Add(new SugarParameter("@bq", bq.Trim()));
            }
            return this.QueryWithPage<ORArrangementEntity>(sql.ToString(), pagination, pars == null ? null : pars.ToArray());
        }


        /// <summary>
        /// 术后登记页,修改排班状态(保存:已登记 取消:已排班)
        /// </summary>
        /// <param name="keyValue">主键值</param>
        public int UpdateSqzt(string keyValue,string value)
        {
            var dbEntity = this.FindEntity(keyValue);
            //properties
            dbEntity.sqzt = value;
            EntityHelper.Modify(dbEntity, keyValue);
            return Update(dbEntity);
        }
    }
}