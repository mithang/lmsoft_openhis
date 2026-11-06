using System;
using NewtouchHIS.Lib.Base.Model;
using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.Entity.V;
using Newtouch.HIS.Domain.ValueObjects;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// Đơn xin phát thuốc nội bộ
    /// </summary>
    public interface IApplyDmnService
    {
        /// <summary>
        /// Gửi đơn xin phát thuốc nội bộ
        /// </summary>
        /// <param name="sysMedicineRequisitionEntity"></param>
        /// <param name="sysMedicineRequisitionDetailEntities"></param>
        /// <returns></returns>
        string SubmitApply(SysMedicineRequisitionEntity sysMedicineRequisitionEntity, List<SysMedicineRequisitionDetailEntity> sysMedicineRequisitionDetailEntities);

        /// <summary>
        /// Lấy thông tin chính đơn lĩnh
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="ffzt"></param>
        /// <param name="djh"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeid"></param>
        /// <param name="slbm">Bộ phận lĩnh</param>
        /// <returns></returns>
        IList<ApplyMainVEntity> GetApplyMainInfo(Pagination pagination, int ffzt, string djh, DateTime startTime, DateTime endTime, string yfbmCode, string organizeid, string slbm = "");

        /// <summary>
        /// Lấy chi tiết đơn lĩnh
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="sldId"></param>
        /// <param name="organizeid"></param>
        /// <returns></returns>
        IList<ApplyDetailVEntity> GetApplyDetails(Pagination pagination, string sldId, string organizeid);

        /// <summary>
        /// Lấy chi tiết xuất kho theo đơn lĩnh
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="sldIds"></param>
        /// <param name="yfbmCode">Bộ phận hiện tại</param>
        /// <param name="organizeid"></param>
        /// <returns></returns>
        IList<ApplyOutStockVEntity> GetApplyOutStockDetail(Pagination pagination, string sldIds, string yfbmCode, string organizeid);

        /// <summary>
        /// Sửa trạng thái phát đơn lĩnh
        /// </summary>
        /// <param name="sldId"></param>
        /// <param name="ffzt">Trạng thái đích</param>
        /// <param name="organizeid"></param>
        /// <returns></returns>
        string UpgradeStatus(string sldId, int ffzt, string organizeid);

        /// <summary>
        /// Gửi phát thuốc theo đơn lĩnh Lấy đơn lĩnh làm nguyên tử,Kết quả chỉ có hai loại 1.Tất cả thành công 2.Tất cả thất bại   Không có trường hợp thành công một nửa 
        /// </summary>
        /// <param name="group">Thông tin phát thuốc của cùng một đơn lĩnh</param>
        /// <param name="source">Tất cả thông tin phát thuốc</param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        string SubmitAppOutStock(List<ApplyOutStockVEntity> group, List<ApplyOutStockVEntity> source, string organizeId, string userCode);

        /// <summary>
        /// Theo chứng từIDLấy chi tiết chứng từ xuất nhập kho
        /// </summary>
        /// <param name="crkId"></param>
        /// <param name="djlx"></param>
        /// <returns></returns>
        List<ReceiptQueryDetailVO> SelectOutOrInStorageBillDetail(string crkId, int djlx);
        List<DrupreparationMXVO> SelectDrupreparationInfoMX(string byid);

        /// <summary>
        /// Hủy chứng từ xuất nhập kho vừa tạo khi quy trình sau đó thất bại
        /// (Transfer.SubmitApply gọi khi ApplyProcess thất bại để rollback
        /// receipt vừa insert trong cùng request).
        /// </summary>
        /// <param name="crkId">Id chứng từ xt_yp_crkdj vừa tạo</param>
        /// <param name="userCode">Mã người thao tác (ghi nhận, hiện chưa dùng
        /// trong thao tác xóa rollback)</param>
        // NX2-001: method vendor khong con trong code. Implement theo dung mau
        // DeleteCrkDj da co tren IDrugStorageDmnService (xoa header + detail
        // trong 1 transaction) — dung cho rollback receipt vua tao, khong phai
        // huy chung tu nghiep vu da duyet. Composition, can kiem chung live.
        void Cancel(string crkId, string userCode);
    }
}