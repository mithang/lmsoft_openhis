using Newtouch.Domain.Entity.Inpatient;
using Newtouch.Infrastructure.EF;
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
		/// Lưu biểu mẫu(Thêm mới,Sửa đổi)
		/// </summary>
		/// <param name="entity">Đối tượng thực thể</param>
		/// <param name="keyValue">Giá trị khóa chính</param>
		bool SubmitForm(InpatientBedCardEntity entity, string keyValue);


		/// <summary>
		/// Xóa dữ liệu
		/// </summary>
		/// <param name="keyValue">Giá trị khóa chính</param>
		void DeleteForm(string keyValue);
	}
}
