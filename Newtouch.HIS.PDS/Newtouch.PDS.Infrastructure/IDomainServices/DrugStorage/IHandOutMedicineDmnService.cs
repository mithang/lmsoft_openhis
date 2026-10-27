using System.Collections.Generic;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.DTO.DrugStorage;
using Newtouch.HIS.Domain.Entity.V;
using Newtouch.HIS.Domain.ValueObjects;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// Xuất kho
    /// </summary>
    public interface IHandOutMedicineDmnService
    {
        /// <summary>
        /// Tự động gợi ý mã nhập
        /// </summary>
        /// <returns></returns>
        List<HandOutMedicinesrmVO> GetsrmInfoFlevelList(string yfbmCode, string srm);

        /// <summary>
        ///  Lấy bản ghi tồn kho thuốc của khoa
        /// </summary>
        /// <param name="ypdm"></param>
        /// <returns></returns>
        List<MedicinepcInfo> GetpcList(string ypdm);

        /// <summary>
        /// Phát thuốc(Xuất kho trực tiếp)
        /// </summary>
        /// <param name="xtYpLsNbfymxk"></param>
        /// <param name="lybm"></param>
        /// <param name="fydh"></param>
        /// <param name="fyfs"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        string HandOutMedicine(List<XT_YP_LS_NBFYMXK> xtYpLsNbfymxk, string lybm, string fydh, string fyfs, int type);

        /// <summary>
        /// Nhà thuốc phát thuốc cho khoa
        /// </summary>
        /// <param name="xtYpLsNbfymxk"></param>
        /// <param name="lybm"></param>
        /// <param name="pdh"></param>
        /// <param name="fyfs"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        string DispensingMedicineToKs(List<XT_YP_LS_NBFYMXK> xtYpLsNbfymxk, string lybm, string pdh, string fyfs, int type);

        /// <summary>
        /// Xuất kho theo đơn lĩnh
        /// </summary>
        /// <param name="xtYpLsNbfymxk"></param>
        /// <param name="fydh"></param>
        /// <param name="fyfs"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        string HandOutMedicineByRequest(Dictionary<string, List<XT_YP_LS_NBFYMXK>> xtYpLsNbfymxk, string fydh, string fyfs, int type);

        /// <summary>
        /// Trả thuốc
        /// </summary>
        /// <param name="xtYpLsNbfymxk"></param>
        /// <param name="tydh"></param>
        /// <param name="fyfs"></param>
        /// <param name="tybm"></param>
        /// <param name="type"></param>
        string RequestOfReturnMedicine(List<XT_YP_LS_NBFYMXK> xtYpLsNbfymxk, string tybm, string tydh, string fyfs, int type);

        /// <summary>
        /// Lấy chi tiết thuốc trả
        /// </summary>
        /// <param name="ckbm">Bộ phận xuất kho Bộ phận xin trả thuốc</param>
        /// <param name="rkbm">Bộ phận nhập kho Bộ phận thuốc sắp trả về</param>
        /// <param name="srm">Mã nhập Từ khóa</param>
        /// <returns></returns>
        List<HandOutMedicinesrmVO> GetTyypBySrm(string ckbm, string rkbm, string srm);

        /// <summary>
        /// Lấy thông tin thuốc  Nhóm theo lô/số lô
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<HandOutMedicinesrmVO> GetDrugGroupByPc(string ypCode, string yfbmCode, string organizeId);

        /// <summary>
        /// Lấy thông tin tồn kho thuốc xuất kho hàng loạt
        /// </summary>
        /// <param name="yfbmCode">Bộ phận hiện tại</param>
        /// <param name="rkbm">Bộ phận nhập kho</param>
        /// <param name="organizeid"></param>
        /// <returns></returns>
        IList<DrugStockInfoVEntity> GetDirectDeliveryDrugsList(Pagination pagination, string yfbmCode, string rkbm, string organizeid);

        /// <summary>
        /// Lấy thông tin tồn kho thuốc xuất kho hàng loạt
        /// </summary>
        /// <param name="yfbmCode">Bộ phận hiện tại</param>
        /// <param name="rkbm">Bộ phận nhập kho</param>
        /// <param name="organizeid"></param>
        /// <returns></returns>
        IList<DrugStockInfoVEntity> GetDirectDeliveryDrugsList(string yfbmCode, string rkbm, string organizeid);

        /// <summary>
        /// Hôm trước gửi xuất kho trực tiếp hàng loạt
        /// </summary>
        /// <param name="paraDto"></param>
        /// <returns></returns>
        string DirectDeliveryBatchSubmit(DirectDeliveryBatchDTO paraDto);
    }
}
