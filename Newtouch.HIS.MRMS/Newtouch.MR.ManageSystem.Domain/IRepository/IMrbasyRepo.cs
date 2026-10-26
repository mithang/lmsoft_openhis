using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.MR.ManageSystem.Domain.Entity;
using Newtouch.Infrastructure.EF;
using System;

namespace Newtouch.MR.ManageSystem.Domain.IRepository
{
    /// <summary>
    /// Người tạo：chl
    /// Ngày：2019-11-20 11:05
    /// Mô tả: Trang đầu bệnh án
    /// </summary>
    public interface IMrbasyRepo : IRepositoryBase<MrbasyEntity>
    {
        /// <summary>
        /// Lưu biểu mẫu (thêm mới, sửa đổi）
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        int SubmitForm(MrbasyEntity entity, string keyValue);

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        int DeleteForm(string keyValue);

		/// <summary>
		/// Lấy bệnhngườiThông tin nhập viện
		/// </summary>
		/// <param name="pagination"></param>
		/// <param name="organizeId"></param>
		/// <param name="keyword"></param>
		/// <returns></returns>
		IList<MrbasyEntity> GetPagintionHospitalList(Pagination pagination, string organizeId, string jkkh, string keyword);


        /// <summary>
        /// Sửa đổi trạng thái lưu trữ
        /// </summary>
        /// <param name="dataId"></param>
        /// <param name="ZYH"></param>
        /// <returns></returns>
        int Updatebazt(string dataId, string ZYH,string XM);

        int CXUpdatebazt(string dataId, string ZYH, string XM, string organizeId, string LastModifierCode, DateTime LastModifyTime);
        int CXGDZT(string dataId, string ZYH, string XM, string organizeId, string LastModifierCode, DateTime LastModifyTime);

    }
}