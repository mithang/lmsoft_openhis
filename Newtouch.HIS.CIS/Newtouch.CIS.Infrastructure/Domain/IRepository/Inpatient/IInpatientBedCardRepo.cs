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
		/// Lưu bảng phiếu(Thêm mới,Sửa)
		/// </summary>
		/// <param name="entity">thực thể đối tượng</param>
		/// <param name="keyValue">Khóa chính Giá trị</param>
		bool SubmitForm(InpatientBedCardEntity entity, string keyValue);


		/// <summary>
		/// Xóa Dữ liệu
		/// </summary>
		/// <param name="keyValue">Khóa chính Giá trị</param>
		void DeleteForm(string keyValue);
	}
}
