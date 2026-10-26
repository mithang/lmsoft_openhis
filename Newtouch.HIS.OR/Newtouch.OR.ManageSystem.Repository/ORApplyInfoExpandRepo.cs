using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.OR.ManageSystem.Domain.Entity;
using Newtouch.OR.ManageSystem.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.OR.ManageSystem.Repository
{
	public class ORApplyInfoExpandRepo : RepositoryBase<ORApplyInfoExpandEntity>, IORApplyInfoExpandRepo
	{
		/// <summary>
		/// Hàm khởi tạo mặc định
		/// </summary>
		/// <param name="databaseFactory">EFFactory ngữ cảnh</param>
		public ORApplyInfoExpandRepo(IDefaultDatabaseFactory databaseFactory)
			: base(databaseFactory)
		{

		}

		/// <summary>
		/// Lưu biểu mẫu（Thêm mới、Sửa）
		/// </summary>
		/// <param name="entity">Đối tượng thực thể</param>
		/// <param name="keyValue">Giá trị khóa chính</param>
		public int SubmitForm(ORApplyInfoExpandEntity entity, string keyValue)
		{
			//if (!string.IsNullOrEmpty(keyValue))
			//{
			//	var dbEntity = this.FindEntity(keyValue);
			//	//properties

			//	dbEntity.Modify(keyValue);
			//	return Update(dbEntity);
			//}
			entity.Create(true);
			return Insert(entity);
		}

		/// <summary>
		/// Xóa dữ liệu
		/// </summary>
		/// <param name="keyValue">Giá trị khóa chính</param>
		public void DeleteForm(string keyValue, string orgId)
		{
			Delete(p => p.Applyno == keyValue);
		}
		
		/// <summary>
		/// Lấy thông tin nhiều ca phẫu thuật theo số đơn đăng ký
		/// </summary>
		/// <param name="orgId"></param>
		/// <param name="applyNo"></param>
		/// <returns></returns>
		public List<ORApplyInfoExpandEntity> getApplyExtendByApplyno(string orgId, string applyNo) {

			string sql = @"SELECT [OrganizeId],[Id],[Applyno],[zyh],[ssmc],[ssdm],[px] ,[zt],[CreateTime],[CreatorCode] ,[LastModifyTime],[LastModifierCode]
  FROM [Newtouch_OR].[dbo].[OR_ApplyInfo_Expand] 
  WHERE zt=1 and [OrganizeId]=@orgId ";
			if (!string.IsNullOrWhiteSpace(applyNo))
			{
				sql += " and applyNo=@applyNo ";
			}
			sql += " order by px ";
			return this.FindList<ORApplyInfoExpandEntity>(sql, new SqlParameter[] {
				new SqlParameter("@orgId",orgId),
				new SqlParameter("@applyNo",applyNo)
			});
		}
	}
}
