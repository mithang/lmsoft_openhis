using System;
using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.Entity.V;
using Newtouch.HIS.Domain.ValueObjects;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// đề nghị phát thuốc nội bộ
    /// </summary>
    public interface IApplyDmnService
    {
        /// <summary>
        /// gửi đề nghị phát thuốc nội bộ
        /// </summary>
        /// <param name="sysMedicineRequisitionEntity"></param>
        /// <param name="sysMedicineRequisitionDetailEntities"></param>
        /// <returns></returns>
        string SubmitApply(SysMedicineRequisitionEntity sysMedicineRequisitionEntity, List<SysMedicineRequisitionDetailEntity> sysMedicineRequisitionDetailEntities);

        /// <summary>
        /// lấy phiếu lĩnh chính thông tin
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="ffzt"></param>
        /// <param name="djh"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeid"></param>
        /// <param name="slbm">bộ phận lĩnh</param>
        /// <returns></returns>
        IList<ApplyMainVEntity> GetApplyMainInfo(Pagination pagination, int ffzt, string djh, DateTime startTime, DateTime endTime, string yfbmCode, string organizeid, string slbm = "");

        /// <summary>
        /// lấy chi tiết phiếu lĩnh
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="sldId"></param>
        /// <param name="organizeid"></param>
        /// <returns></returns>
        IList<ApplyDetailVEntity> GetApplyDetails(Pagination pagination, string sldId, string organizeid);

        /// <summary>
        /// lấy xuất kho theo phiếu lĩnh chi tiết
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="sldIds"></param>
        /// <param name="yfbmCode">bộ phận hiện tại</param>
        /// <param name="organizeid"></param>
        /// <returns></returns>
        IList<ApplyOutStockVEntity> GetApplyOutStockDetail(Pagination pagination, string sldIds, string yfbmCode, string organizeid);

        /// <summary>
        /// sửa phiếu lĩnh trạng thái cấp phát
        /// </summary>
        /// <param name="sldId"></param>
        /// <param name="ffzt">mục tiêu trạng thái</param>
        /// <param name="organizeid"></param>
        /// <returns></returns>
        string UpgradeStatus(string sldId, int ffzt, string organizeid);

        /// <summary>
        /// 提交申领发药 以申领单为原子, 结果只有两种 1.tất cả thành công 2.tất cả thất bại   没有成功一半的情况 
        /// </summary>
        /// <param name="group">cùng một phiếu lĩnh thông tin phát thuốc</param>
        /// <param name="source">phát thuốc toàn bộ thông tin</param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        string SubmitAppOutStock(List<ApplyOutStockVEntity> group, List<ApplyOutStockVEntity> source, string organizeId, string userCode);

        /// <summary>
        /// theo đơn chứng ID lấy chứng từ nhập/xuất kho chi tiết
        /// </summary>
        /// <param name="crkId"></param>
        /// <param name="djlx"></param>
        /// <returns></returns>
        List<ReceiptQueryDetailVO> SelectOutOrInStorageBillDetail(string crkId, int djlx);
        List<DrupreparationMXVO> SelectDrupreparationInfoMX(string byid);
    }
}