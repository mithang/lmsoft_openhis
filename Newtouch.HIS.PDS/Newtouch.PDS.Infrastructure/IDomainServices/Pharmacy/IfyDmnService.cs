using Newtouch.Core.Common;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.HIS.Domain.DTO.Medicine;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.Entity.V;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.ValueObjects.OutPatientPharmacy;
using Newtouch.HIS.Domain.ValueObjects.SystemManage;
using Newtouch.HIS.Domain.VO;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// Phát thuốc
    /// </summary>
    public interface IfyDmnService
    {
        /// <summary>
        /// Qua thông tin đơn thuốc,Lấy chi tiết đơn thuốc
        /// </summary>
        /// <param name="reqdata"></param>
        /// <returns></returns>
        IList<fydisplayDetailInfo> GetDetailInfo(List<fyDetailListRequest> reqdata);

        /// <summary>
        /// Lấy chi tiết phát thuốc
        /// </summary>
        /// <param name="reqdata"></param>
        /// <returns></returns>
        IList<fydisplayDetailInfo> GetAllDetailInfo(List<fyDetailListRequest> reqdata);

        ///// <summary>
        ///// Phát thuốc ngoại trú
        ///// </summary>
        ///// <param name="reqdata"></param>
        ///// <returns></returns>
        //List<string> ExecMzHandoutMedicine(List<GetfyDetailCFList> reqdata);

        /// <summary>
        /// Phát thuốc ngoại trú
        /// </summary>
        /// <param name="reqdata"></param>
        /// <returns></returns>
        List<string> MzDistributingMedicines(List<GetfyDetailCFList> reqdata);

        /// <summary>
        /// Trả thuốc ngoại trú(mz_cfmxphKiểm tra)
        /// </summary>
        /// <param name="reqdata"></param>
        /// <returns></returns>
        List<tyCFMainInfo> ExecMZExitMedicine(List<tyCFMainInfo> reqdata);

        /// <summary>
        /// Lấy bộ phận nhận hàng
        /// </summary>
        /// <returns></returns>
        List<DrugSpecialPropertiesVO> GetMeidicineSHBMList();

        /// <summary>
        /// Trả lại tồn kho đóng băng
        /// </summary>
        /// <param name="list"></param>
        void ReleaseFrozenStock(List<OutpatientPrescriptionDetailBatchNumberEntity> list);

        /// <summary>
        /// Truy vấn phát thuốc ngoại trú hiển thị chi tiết(mz_cfmxphKiểm tra)
        /// </summary>
        /// <param name="reqdata"></param>
        /// <returns></returns>
        List<fydisplayDetailInfo> GetMzHandOutDetail(List<fyDetailListRequest> reqdata);

        /// <summary>
        /// Lấy chi tiết thuốc phát
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="operateType"></param>
        /// <returns></returns>
        List<fyMeidicneInfo> GetDeliveryDrugs(string cfh, EnumOperateType operateType = EnumOperateType.Fy);

        /// <summary>
        /// Phát thuốc ngoại trú
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="yfbmCode"></param>
        /// <returns></returns>
        string UpdateStock(string cfh, string yfbmCode);

        /// <summary>
        /// Phát thuốc ngoại trúV2.0
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="userCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        string ExecOutpatientDispensingDrugV2(string cfh, string yfbmCode, string userCode, string organizeId,string zsm);

        /// <summary>
        /// Phát thuốc ngoại trú
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="userCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        string ExecMzHandoutMedicine(string cfh, string yfbmCode, string userCode, string organizeId);

        /// <summary>
        /// Phát thuốc ngoại trú
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="ypCode"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="userCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        string UpdateStock(string cfh, string ypCode, string yfbmCode, string userCode, string organizeId);


        /// <summary>
        /// Phát thuốc ngoại trú
        /// </summary>
        /// <param name="cfh"></param>
        /// <returns></returns>
        string ReturnDrug(string cfh);

        /// <summary>
        /// Lấy đơn thuốc đã phát
        /// </summary>
        /// <param name="keyWord"></param>
        /// <param name="fph"></param>
        /// <param name="cfh"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        IList<cfInfoVo> GetDeliveredCf(string keyWord, string fph, string cfh, Pagination pagination = null);

        /// <summary>
        /// Lấy thông tin phát thuốc
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="keyWork"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<DrugDeliveryEntity> GetDrugDeliveryList(DateTime startTime, DateTime endTime, string keyWork, string yfbmCode, string organizeId);


        /// <summary>
        /// Truy vấn chi tiết đơn phát thuốc ngoại trú
        /// </summary>
        /// <param name="xm"></param>
        /// <param name="cardNo"></param>
        /// <param name="cfh"></param>
        /// <param name="organizeId"></param>
        /// <param name="yfbmCode"></param>
        /// <returns></returns>
        List<CfmxVO> SelectCfmx(string xm, string cardNo, string cfh, string organizeId, string yfbmCode);

        /// <summary>
        /// Truy vấn đã phát thuốc Thông tin đơn thuốc
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword">cardNo or xm</param>
        /// <param name="invoiceNo"></param>
        /// <param name="cfh"></param>
        /// <param name="operateType"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<CffyjlVO> SelectDispensedRpInfo(Pagination pagination, string keyword, string invoiceNo, string cfh, EnumOperateType operateType, DateTime kssj, DateTime jssj, string yfbmCode, string organizeId);

        /// <summary>
        /// Truy vấn thông tin đơn thuốc
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="fph"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<CfxxVO> SelectRpList(string keyword, string fph, DateTime kssj, DateTime jssj, string yfbmCode, string organizeId);

        /// <summary>
        /// Truy vấn chi tiết đơn thuốc
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<CfmxVO> SelectRpDetail(string cfh, string yfbmCode, string organizeId);

        /// <summary>
        /// Kiểm tra đơn thuốc này có tồn tại không
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns>Trả về số đơn thuốc hiệu lực</returns>
        int ExistEffectiveRp(string cfh, string yfbmCode, string organizeId);

        /// <summary>
        /// Lấy thông tin đơn thuốc theo số thẻ và họ tên
        /// </summary>
        /// <param name="yfbmCode"></param>
        /// <param name="cardNo"></param>
        /// <param name="xm"></param>
        /// <param name="fybz"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<MzCfEntity> GetRpInfo(string yfbmCode, string cardNo, string xm, EnumFybz fybz = EnumFybz.Yp,
           string organizeId = "");

        /// <summary>
        /// Truy vấn loại lớn thuốc
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<YpdlDTO> getYpdl(string orgId);


        #region Thuốc 、Tình hình sử dụng vật tư
        List<YfMaterialTjVo> GetMaterialList(Pagination pagination, string orgId, string ks, string ry, string slly, DateTime kssj, DateTime jssj, string keyword);

        #endregion

        #region Đơn ngoại trú
        IList<MzcfcxList> GetMzcfList(Pagination pagination, MzcfcxVo req);
        IList<MzcfcxDetailList> GetMzcfDetailList(Pagination pagination, MzcfcxVo req);

        #endregion

        #region Đơn điện tử BHYT
        /// <summary>
        /// Lấy thông tin bệnh nhân đơn điện tử
        /// </summary>
        /// <returns></returns>
        List<DzcfBrxxDTO> GetElectronicPrescriptionCfInfo(string cfh, string xm, string organizeId = "");

        #endregion
    }
}
