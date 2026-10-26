using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// Thuốc
    /// </summary>
    public interface ISysMedicineDmnService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="medicineEntity"></param>
        /// <param name="medicinePropEntity"></param>
        /// <param name="ypId"></param>
        void SubmitMedicine(SysMedicineVO model, int? ypId);

        /// <summary>
        /// Sửa đổi thời gian đồng bộ bảo hiểm y tế
        /// </summary>
        /// <param name="ypId"></param>
        /// <returns></returns>
        bool YibaoUpload(int ypId, out string error);

        /// <summary>
        /// Liên kết tập hợp list thuốc
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysMedicineVO> GetPaginationList(string organizeId, Pagination pagination, string zt, string ypflCode, string keyword = null);


		
		/// <summary>
		/// Khi sửa đổi thông tin, liên kết giá trị sửa đổi
		/// </summary>
		/// <param name="keyValue"></param>
		/// <returns></returns>
		SysMedicineVO GetFormJson(int keyValue);

        /// <summary>
        /// Lấy thông tin nhân viên hệ thống
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        SysMedicineVO SelectMedicineInfo(string ypCode, string organizeId);

		/// <summary>
		/// Lấy hệ số thuốc
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		string GetTradePricePlus(string name);

		/// <summary>
		/// Lấy thông tin thuốc hệ thống thuộc tổ chức hiện tại
		/// </summary>
		/// <param name="organizeId"></param>
		/// <param name="pagination"></param>
		/// <param name="keyword"></param>
		/// <returns></returns>
		IList<SysMedicineVO> GetPaginationListdm(string organizeId, Pagination pagination, string type, string keyword = null);
        IList<SysMedicineMldzxxVO> GetPaginationListMldzxx(string organizeId, Pagination pagination, string type, string keyword = null);
        /// <summary>
        /// Truy vấn thông tin thuốc bảo hiểm y tế
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="ypmc"></param>
        /// <param name="py"></param>
        /// <param name="gjybdm"></param>
        /// <param name="pzwh"></param>
        /// <returns></returns>
        IList<G_yb_ypxxVO> GetYpypxxlist(string organizeId, string ypmc, string py, string gjybdm, string pzwh,string dataSource);

		/// <summary>
		/// Lưu thông tin đối mã bảo hiểm y tế
		/// </summary>
		/// <param name="ybxx"></param>
		/// <param name="ypid"></param>
		/// <param name="organizeId"></param>
		/// <returns></returns>
		int SaveYpYb(G_yb_ypxxVO ybxx, int? ypid, string organizeId);


		/// <summary>
		/// Thông tin vật liệu hệ thống
		/// </summary>
		/// <param name="organizeId"></param>
		/// <param name="pagination"></param>
		/// <param name="type"></param>
		/// <param name="keyword"></param>
		/// <returns></returns>
		IList<SysMedicineVO> GetclxxList(string organizeId, Pagination pagination, string type, string keyword = null);

		/// <summary>
		/// Truy vấn thông tin vật liệu bảo hiểm y tế theo thông tin vật liệu hệ thống
		/// </summary>
		/// <param name="organizeId"></param>
		/// <param name="ypmc"></param>
		/// <param name="py"></param>
		/// <param name="gjybdm"></param>
		/// <param name="pzwh"></param>
		/// <returns></returns>
		IList<G_yb_clxxVO> GetYbclxxlist(string organizeId, string ypmc, string py, string gjybdm, string pzwh,string dataSource);

		/// <summary>
		/// Lưu đối mã vật liệu
		/// </summary>
		/// <param name="ybxx"></param>
		/// <param name="ypid"></param>
		/// <param name="organizeId"></param>
		/// <returns></returns>
		int SaveYpcl(G_yb_clxxVO ybxx, int? ypid, string organizeId);



		/// <summary>
		/// Thông tin hạng mục hệ thống
		/// </summary>
		/// <param name="organizeId"></param>
		/// <param name="pagination"></param>
		/// <param name="type"></param>
		/// <param name="keyword"></param>
		/// <returns></returns>
		IList<SysChargeItemEntity> GetxmxxList(string organizeId, Pagination pagination, string type, string keyword = null);


		/// <summary>
		/// Truy vấn thông tin hạng mục bảo hiểm y tế theo thông tin hạng mục hệ thống
		/// </summary>
		/// <param name="organizeId"></param>
		/// <param name="ypmc"></param>
		/// <param name="py"></param>
		/// <param name="gjybdm"></param>
		/// <param name="pzwh"></param>
		/// <returns></returns>
		IList<G_yb_xmxxVO> GetYbxmxxlist(string organizeId, string ypmc, string py, string gjybdm, string pzwh,string dataSource);

		int SaveYpxm(G_yb_xmxxVO ybxx, int? ypid, string organizeId);

        /// <summary>
        /// Cách dùng thuốc
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysMedicineUsageEntity> GetUsageFloat(string keyword);

    }
}
