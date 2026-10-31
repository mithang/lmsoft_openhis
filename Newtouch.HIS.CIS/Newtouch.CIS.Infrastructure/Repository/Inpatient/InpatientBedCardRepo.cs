using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository;

using System.Collections.Generic;
using Newtouch.CIS.Compat;
using NewtouchHIS.Lib.Base.Model;
using System.Data.SqlClient;
using System.Text;
using Newtouch.CIS.Repository;
using Newtouch.Domain.Entity.Inpatient;
using Newtouch.Domain.IRepository.Inpatient;
using System.Linq;

namespace Newtouch.Repository.Inpatient
{
	public class InpatientBedCardRepo : RepositoryBase<InpatientBedCardEntity>, IInpatientBedCardRepo
	{
		/// <summary>
	 /// Mặc địnhHàm khởi tạosố
	 /// </summary>
	 /// <param name="databaseFactory">EFtrêndướivănNhà máy</param>
		public InpatientBedCardRepo()
			: base()
		{

		}

		/// <summary>
		/// Lưubảngphiếu(Thêm mới,Sửa)
		/// </summary>
		/// <param name="entity">thựcthểđối tượng</param>
		/// <param name="keyValue">Khóa chínhGiá trị</param>
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
		/// XóaDữ liệu
		/// </summary>
		/// <param name="keyValue">Khóa chínhGiá trị</param>
		public void DeleteForm(string keyValue)
		{
			this.Delete(p => p.Id == keyValue);
		}

	}
}
