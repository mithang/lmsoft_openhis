using Newtouch.OR.ManageSystem.Domain.Entity;
using Newtouch.OR.ManageSystem.Domain.IRepository;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using System.Linq;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Core.Common;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace Newtouch.OR.ManageSystem.Repository
{
    /// <summary>
    /// Tạo lập：chl
    /// Ngày tạo：2019-11-06 10:22
    /// Mô tả：Hồ sơ xếp lịch phẫu thuật
    /// </summary>
    public class ORArrangementRepo : RepositoryBase<ORArrangementEntity>, IORArrangementRepo
    {
        /// <summary>
        /// Hàm khởi tạo mặc định
        /// </summary>
        /// <param name="databaseFactory">EFFactory ngữ cảnh</param>
        public ORArrangementRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
            
        }

        /// <summary>
        /// Lưu biểu mẫu（Thêm mới、Sửa）
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
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
				dbEntity.Modify(ArrangeId);
				return Update(dbEntity);
			}
			else
			{
				if (this.IQueryable().Any(p => p.ApplyId == ApplyId && p.zt == "1"))
				{
					throw new FailedException("申请编号不可重复");
				}
				else
				{
					entity.ApplyId = ApplyId;
					entity.sqzt = "2";
					entity.Create(true);
					return Insert(entity);
				}
			}
		}

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        public int DeleteForm(string keyValue)
        {
            var dbEntity = this.FindEntity(keyValue);
            //properties
            dbEntity.sqzt = "3";
            dbEntity.Modify(keyValue);
            return Update(dbEntity);
        }


        /// <summary>
        /// Trang đăng ký phẫu thuật,Lấy danh sách xếp lịch phẫu thuật ở trạng thái đã xếp lịch và đã đăng ký
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<ORArrangementEntity> GetPagintionListForRegistration(Pagination pagination, string keyword, string bq,string OrganizeId)
        {
            var sql = new StringBuilder();
            sql.Append("select * from OR_Arrangement(nolock) where  zt != 0");
            sql.Append(" and sqzt in(2,4)");
            List<SqlParameter> pars = null;
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                pars = pars ?? new List<SqlParameter>();
                sql.Append(" and(1 = 2");
                sql.Append(" or xm like @keyword");
                sql.Append(" or zyh like @keyword");
                sql.Append(")");
                pars.Add(new SqlParameter("@keyword", "%" + keyword.Trim() + "%"));
			}
			if (!string.IsNullOrWhiteSpace(OrganizeId))
			{
				sql.Append(" and OrganizeId=@OrganizeId");
				pars.Add(new SqlParameter("@OrganizeId", OrganizeId.Trim()));
			}
			if (!string.IsNullOrWhiteSpace(bq))
            {
                sql.Append(" and bq=@bq");
                pars.Add(new SqlParameter("@bq", bq.Trim()));
            }
            return this.QueryWithPage<ORArrangementEntity>(sql.ToString(), pagination, pars == null ? null : pars.ToArray());
        }


        /// <summary>
        /// Trang đăng ký sau phẫu thuật,Sửa trạng thái xếp lịch(Lưu:Đã đăng ký Hủy:Đã xếp lịch)
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        public int UpdateSqzt(string keyValue,string value)
        {
            var dbEntity = this.FindEntity(keyValue);
            //properties
            dbEntity.sqzt = value;
            dbEntity.Modify(keyValue);
            return Update(dbEntity);
        }
    }
}