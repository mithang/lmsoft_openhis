using Newtouch.Domain.Entity.Inpatient;
using Newtouch.CIS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.IRepository.Inpatient
{
	public interface IInpatientBedCardRepo : IRepositoryBase<InpatientBedCardEntity>
	{
		/// <summary>
		/// Lưubảngphiếu（Thêm mới、Sửa）
		/// </summary>
		/// <param name="entity">thựcthểđối tượng</param>
		/// <param name="keyValue">Khóa chínhGiá trị</param>
		bool SubmitForm(InpatientBedCardEntity entity, string keyValue);


		/// <summary>
		/// XóaDữ liệu
		/// </summary>
		/// <param name="keyValue">Khóa chínhGiá trị</param>
		void DeleteForm(string keyValue);
	}
}
