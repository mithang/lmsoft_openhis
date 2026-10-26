using Newtouch.Core.Common;
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
    /// phát thuốc
    /// </summary>
    public interface IfyDmnService
    {
        /// <summary>
        /// đạt thông tin đơn thuốc, lấy chi tiết đơn thuốc
        /// </summary>
        /// <param name="reqdata"></param>
        /// <returns></returns>
        IList<fydisplayDetailInfo> GetDetailInfo(List<fyDetailListRequest> reqdata);

        /// <summary>
        /// lấy chi tiết phát thuốc
        /// </summary>
        /// <param name="reqdata"></param>
        /// <returns></returns>
        IList<fydisplayDetailInfo> GetAllDetailInfo(List<fyDetailListRequest> reqdata);

        ///// <summary>
        ///// phát thuốc ngoại trú
        ///// </summary>
        ///// <param name="reqdata"></param>
        ///// <returns></returns>
        //List<string> ExecMzHandoutMedicine(List<GetfyDetailCFList> reqdata);

        /// <summary>
        /// phát thuốc ngoại trú
        /// </summary>
        /// <param name="reqdata"></param>
        /// <returns></returns>
        List<string> MzDistributingMedicines(List<GetfyDetailCFList> reqdata);

        /// <summary>
        /// trả thuốc ngoại trú (mz_cfmxph kiểm tra)
        /// </summary>
        /// <param name="reqdata"></param>
        /// <returns></returns>
        List<tyCFMainInfo> ExecMZExitMedicine(List<tyCFMainInfo> reqdata);

        /// <summary>
        /// lấy bộ phận nhận hàng
        /// </summary>
        /// <returns></returns>
        List<DrugSpecialPropertiesVO> GetMeidicineSHBMList();

        /// <summary>
        /// Hoàn trả tồn kho bị đóng băng
        /// </summary>
        /// <param name="list"></param>
        void ReleaseFrozenStock(List<OutpatientPrescriptionDetailBatchNumberEntity> list);

        /// <summary>
        /// tra cứu phát thuốc ngoại trú hiển thị thông tin chi tiết(mz_cfmxph kiểm tra)
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
        /// phát thuốc ngoại trú
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="yfbmCode"></param>
        /// <returns></returns>
        string UpdateStock(string cfh, string yfbmCode);

        /// <summary>
        /// phát thuốc ngoại trú V2.0
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="userCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        string ExecOutpatientDispensingDrugV2(string cfh, string yfbmCode, string userCode, string organizeId,string zsm);

        /// <summary>
        /// phát thuốc ngoại trú
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="userCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        string ExecMzHandoutMedicine(string cfh, string yfbmCode, string userCode, string organizeId);

        /// <summary>
        /// phát thuốc ngoại trú
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="ypCode"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="userCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        string UpdateStock(string cfh, string ypCode, string yfbmCode, string userCode, string organizeId);


        /// <summary>
        /// phát thuốc ngoại trú
        /// </summary>
        /// <param name="cfh"></param>
        /// <returns></returns>
        string ReturnDrug(string cfh);

        /// <summary>
        /// lấy đã phát đơn thuốc
        /// </summary>
        /// <param name="keyWord"></param>
        /// <param name="fph"></param>
        /// <param name="cfh"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        IList<cfInfoVo> GetDeliveredCf(string keyWord, string fph, string cfh, Pagination pagination = null);

        /// <summary>
        /// lấy thông tin phát thuốc
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="keyWork"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<DrugDeliveryEntity> GetDrugDeliveryList(DateTime startTime, DateTime endTime, string keyWork, string yfbmCode, string organizeId);


        /// <summary>
        /// phát thuốc ngoại trú chi tiết đơn thuốc tra cứu
        /// </summary>
        /// <param name="xm"></param>
        /// <param name="cardNo"></param>
        /// <param name="cfh"></param>
        /// <param name="organizeId"></param>
        /// <param name="yfbmCode"></param>
        /// <returns></returns>
        List<CfmxVO> SelectCfmx(string xm, string cardNo, string cfh, string organizeId, string yfbmCode);

        /// <summary>
        /// đã phát thuốc tra cứu thông tin đơn thuốc
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
        /// tra cứu thông tin đơn thuốc
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
        /// tra cứu chi tiết đơn thuốc
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<CfmxVO> SelectRpDetail(string cfh, string yfbmCode, string organizeId);

        /// <summary>
        /// đánh giá có nên đơn thuốc có hay không tồn tại
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns>quay lại còn hiệu lực của đơn thuốc số</returns>
        int ExistEffectiveRp(string cfh, string yfbmCode, string organizeId);

        /// <summary>
        /// lấy thông tin đơn thuốc theo số thẻ và họ tên
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
        /// tra cứu thuốc nhóm lớn
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<YpdlDTO> getYpdl(string orgId);


        #region 药品 、耗材使用情况
        List<YfMaterialTjVo> GetMaterialList(Pagination pagination, string orgId, string ks, string ry, string slly, DateTime kssj, DateTime jssj, string keyword);

        #endregion

        #region 门诊处方
        IList<MzcfcxList> GetMzcfList(Pagination pagination, MzcfcxVo req);
        IList<MzcfcxDetailList> GetMzcfDetailList(Pagination pagination, MzcfcxVo req);

        #endregion

        #region 医保电子处方
        /// <summary>
        /// lấy thông tin bệnh nhân của đơn thuốc điện tử
        /// </summary>
        /// <returns></returns>
        List<DzcfBrxxDTO> GetElectronicPrescriptionCfInfo(string cfh, string xm, string organizeId = "");

        #endregion
    }
}
