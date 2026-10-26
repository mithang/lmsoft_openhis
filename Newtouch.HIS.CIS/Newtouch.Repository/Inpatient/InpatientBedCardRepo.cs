using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository;
using FrameworkBase.MultiOrg.Infrastructure;
using System.Collections.Generic;
using Newtouch.Core.Common;
using System.Data.SqlClient;
using System.Text;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Domain.Entity.Inpatient;
using Newtouch.Domain.IRepository.Inpatient;
using System.Linq;

namespace Newtouch.Repository.Inpatient
{
	public class InpatientBedCardRepo : RepositoryBase<InpatientBedCardEntity>, IInpatientBedCardRepo
	{
		/// <summary>
	 /// Hàm khởi tạo mặc định
	 /// </summary>
	 /// <param name="databaseFactory">Factory ngữ cảnh EF</param>
		public InpatientBedCardRepo(IDefaultDatabaseFactory databaseFactory)
			: base(databaseFactory)
		{

		}

		/// <summary>
		/// Lưu biểu mẫu (thêm mới, sửa đổi)
		/// </summary>
		/// <param name="entity">Đối tượng thực thể</param>
		/// <param name="keyValue">Giá trị khóa chính</param>
		public bool SubmitForm(InpatientBedCardEntity entity, string zyh)
		{
			if (!string.IsNullOrEmpty(zyh))
			{
				var dbEntity = this.IQueryable().FirstOrDefault(p => p.OrganizeId == entity.OrganizeId && p.zyh == entity.zyh && p.zt == "1");
				if (dbEntity != null)

				//var dbEntity = this.FindEntity(keyValue);
				//if (dbEntity != null)
				{
					//properties

					dbEntity.zrhs = entity.zrhs;
					dbEntity.zrzz = entity.zrzz;
					dbEntity.gms = entity.gms;
					dbEntity.fbsx = entity.fbsx;
					dbEntity.sjys = entity.sjys;
					dbEntity.hsz = entity.hsz;
					dbEntity.kzr = entity.kzr;
					dbEntity.xh = entity.xh;
					dbEntity.jb = entity.jb;
					dbEntity.zt = "1";
					dbEntity.Modify(dbEntity.Id);
					return this.Update(dbEntity) > 0;
				}
				else
				{
					entity.Create(true);
					return this.Insert(entity) > 0;
				}
			}
			
			return false;
		}

		/// <summary>
		/// Xóa dữ liệu
		/// </summary>
		/// <param name="keyValue">Giá trị khóa chính</param>
		public void DeleteForm(string keyValue)
		{
			this.Delete(p => p.Id == keyValue);
		}

	}
}
