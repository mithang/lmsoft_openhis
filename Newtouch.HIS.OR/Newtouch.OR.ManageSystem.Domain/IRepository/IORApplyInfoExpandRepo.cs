using Newtouch.Infrastructure.EF;
using Newtouch.OR.ManageSystem.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.OR.ManageSystem.Domain.IRepository
{
	public interface IORApplyInfoExpandRepo : IRepositoryBase<ORApplyInfoExpandEntity>
	{
		/// <summary>
		/// Lưu biểu mẫu（Thêm mới、Sửa）
		/// </summary>
		/// <param name="entity">Đối tượng thực thể</param>
		/// <param name="keyValue">Giá trị khóa chính</param>
		int SubmitForm(ORApplyInfoExpandEntity entity, string keyValue);

		/// <summary>
		/// Xóa dữ liệu
		/// </summary>
		/// <param name="keyValue">Số đơn đăng ký</param>
		void DeleteForm(string keyValue, string orgId);

		/// <summary>
		/// Lấy thông tin nhiều ca phẫu thuật theo số đơn đăng ký
		/// </summary>
		/// <param name="orgId"></param>
		/// <param name="applyNo"></param>
		/// <returns></returns>
		List<ORApplyInfoExpandEntity> getApplyExtendByApplyno(string orgId, string applyNo);
	}
}
