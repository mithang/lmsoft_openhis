using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.DTO.DrugStorage;
using Newtouch.HIS.Domain.Entity.V;
using Newtouch.HIS.Domain.ValueObjects;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// xuất kho
    /// </summary>
    public interface IHandOutMedicineDmnService
    {
        /// <summary>
        /// mã nhập tự động nhắc nhở
        /// </summary>
        /// <returns></returns>
        List<HandOutMedicinesrmVO> GetsrmInfoFlevelList(string yfbmCode, string srm);

        /// <summary>
        ///  lấy khoa thuốc tồn kho bản ghi
        /// </summary>
        /// <param name="ypdm"></param>
        /// <returns></returns>
        List<MedicinepcInfo> GetpcList(string ypdm);

        /// <summary>
        /// phát thuốc(xuất kho trực tiếp)
        /// </summary>
        /// <param name="xtYpLsNbfymxk"></param>
        /// <param name="lybm"></param>
        /// <param name="fydh"></param>
        /// <param name="fyfs"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        string HandOutMedicine(List<XT_YP_LS_NBFYMXK> xtYpLsNbfymxk, string lybm, string fydh, string fyfs, int type);

        /// <summary>
        /// nhà thuốc tới khoa phát thuốc
        /// </summary>
        /// <param name="xtYpLsNbfymxk"></param>
        /// <param name="lybm"></param>
        /// <param name="pdh"></param>
        /// <param name="fyfs"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        string DispensingMedicineToKs(List<XT_YP_LS_NBFYMXK> xtYpLsNbfymxk, string lybm, string pdh, string fyfs, int type);

        /// <summary>
        /// xuất kho theo phiếu lĩnh
        /// </summary>
        /// <param name="xtYpLsNbfymxk"></param>
        /// <param name="fydh"></param>
        /// <param name="fyfs"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        string HandOutMedicineByRequest(Dictionary<string, List<XT_YP_LS_NBFYMXK>> xtYpLsNbfymxk, string fydh, string fyfs, int type);

        /// <summary>
        /// trả thuốc
        /// </summary>
        /// <param name="xtYpLsNbfymxk"></param>
        /// <param name="tydh"></param>
        /// <param name="fyfs"></param>
        /// <param name="tybm"></param>
        /// <param name="type"></param>
        string RequestOfReturnMedicine(List<XT_YP_LS_NBFYMXK> xtYpLsNbfymxk, string tybm, string tydh, string fyfs, int type);

        /// <summary>
        /// lấy trả thuốc chi tiết thuốc
        /// </summary>
        /// <param name="ckbm">bộ phận xuất kho đề nghị trả thuốc của bộ phận</param>
        /// <param name="rkbm">bộ phận nhập kho 药品即将退回到的部门</param>
        /// <param name="srm">mã nhập từ khóa</param>
        /// <returns></returns>
        List<HandOutMedicinesrmVO> GetTyypBySrm(string ckbm, string rkbm, string srm);

        /// <summary>
        /// lấy thông tin thuốc  theo lô số lô nhóm
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<HandOutMedicinesrmVO> GetDrugGroupByPc(string ypCode, string yfbmCode, string organizeId);

        /// <summary>
        /// lấy thông tin tồn kho thuốc xuất kho hàng loạt
        /// </summary>
        /// <param name="yfbmCode">bộ phận hiện tại</param>
        /// <param name="rkbm">bộ phận nhập kho</param>
        /// <param name="organizeid"></param>
        /// <returns></returns>
        IList<DrugStockInfoVEntity> GetDirectDeliveryDrugsList(Pagination pagination, string yfbmCode, string rkbm, string organizeid);

        /// <summary>
        /// lấy thông tin tồn kho thuốc xuất kho hàng loạt
        /// </summary>
        /// <param name="yfbmCode">bộ phận hiện tại</param>
        /// <param name="rkbm">bộ phận nhập kho</param>
        /// <param name="organizeid"></param>
        /// <returns></returns>
        IList<DrugStockInfoVEntity> GetDirectDeliveryDrugsList(string yfbmCode, string rkbm, string organizeid);

        /// <summary>
        /// trước ngày gửi hàng loạt xuất kho trực tiếp
        /// </summary>
        /// <param name="paraDto"></param>
        /// <returns></returns>
        string DirectDeliveryBatchSubmit(DirectDeliveryBatchDTO paraDto);
    }
}
