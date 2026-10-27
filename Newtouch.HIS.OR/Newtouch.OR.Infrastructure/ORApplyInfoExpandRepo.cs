using Newtouch.OR.ManageSystem.Domain.Entity;
using Newtouch.OR.ManageSystem.Domain.IRepository;
using System;
using System.Collections.Generic;
using SqlSugar;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.OR.ManageSystem.Repository
{
	public class ORApplyInfoExpandRepo : RepositoryBase<ORApplyInfoExpandEntity>, IORApplyInfoExpandRepo
	{
		/// <summary>
		/// 默认构造函数
		/// </summary>
		/// <param name="databaseFactory">EF上下文工厂</param>
		public ORApplyInfoExpandRepo()
		{

		}

		/// <summary>
		/// 保存表单（新增、修改）
		/// </summary>
		/// <param name="entity">实体对象</param>
		/// <param name="keyValue">主键值</param>
		public int SubmitForm(ORApplyInfoExpandEntity entity, string keyValue)
		{
			//if (!string.IsNullOrEmpty(keyValue))
			//{
			//	var dbEntity = this.FindEntity(keyValue);
			//	//properties

			//	EntityHelper.Modify(dbEntity, keyValue);
			//	return Update(dbEntity);
			//}
			EntityHelper.Create(entity, true);
			return Insert(entity);
		}

		/// <summary>
		/// 删除数据
		/// </summary>
		/// <param name="keyValue">主键值</param>
		public void DeleteForm(string keyValue, string orgId)
		{
			Delete(p => p.Applyno == keyValue);
		}
		
		/// <summary>
		/// 根据申请编号获取多个手术信息
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
			return this.FindList<ORApplyInfoExpandEntity>(sql, new SugarParameter[] {
				new SugarParameter("@orgId",orgId),
				new SugarParameter("@applyNo",applyNo)
			}).ToList();
		}
	}
}
