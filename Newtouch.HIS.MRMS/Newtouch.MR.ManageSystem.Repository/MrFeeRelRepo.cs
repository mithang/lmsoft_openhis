using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Core.Common;
using Newtouch.MR.ManageSystem.Domain.Entity;
using Newtouch.MR.ManageSystem.Domain.IRepository;
using Newtouch.MR.ManageSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.MR.ManageSystem.Repository
{
    public class MrFeeRelRepo : RepositoryBase<bafeeRelEntity>, IMrFeeRelRepo
    {
        /// <summary>
        /// Hàm tạo mặc định
        /// </summary>
        /// <param name="databaseFactory">EFFactory ngữ cảnh</param>
        public MrFeeRelRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }


        /// <summary>
        /// Lấy danh sách thực thể theo trang
        /// </summary>
        /// <param name="pagination">Phân trang</param>
        /// <param name="keyword">Từ khóa lọc</param>
        public IList<bafeeRelEntity> GetPagintionList(Pagination pagination, string keyword, string organizeId)
        {
            var sql = new StringBuilder();
            sql.Append("select * from mr_dic_sfxmrel(nolock) where zt != 0");
            List<SqlParameter> pars = null;
            if (!string.IsNullOrWhiteSpace(organizeId))
            {
                pars = pars ?? new List<SqlParameter>();
                sql.Append(" and organizeid = @organizeId");
                pars.Add(new SqlParameter("@organizeId", organizeId));
            }
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                pars = pars ?? new List<SqlParameter>();
                sql.Append(" and(1 = 2");
                sql.Append(" or sfxm like @keyword");
                sql.Append(" or sfxmmc like @keyword");
                sql.Append(")");
                pars.Add(new SqlParameter("@keyword", "%" + keyword.Trim() + "%"));
            }
            return this.QueryWithPage<bafeeRelEntity>(sql.ToString(), pagination, pars == null ? null : pars.ToArray());
        }

		/// <summary>
		/// Thêm hạng mục chưa phân loại vàoQuan hệtrong bảng
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public int SaveForm(bafeeRelEntity entity)
		{
			entity.Create(true);
			return Insert(entity);
		}

		public int DeleteForm(string keyValue)
		{
			var dbEntity = this.FindEntity(keyValue);
			//properties
			dbEntity.zt = "0";
			dbEntity.Modify(keyValue);
			return Update(dbEntity);
		}

	}
	}
