using System;
using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.VO;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// xếp thuốc
    /// </summary>
    public interface IPyDmnService
    {

        /// <summary>
        /// hủy xếp thuốc
        /// </summary>
        /// <param name="mzCfmxEntity"></param>
        /// <returns></returns>
        string CancelArrangedDrug(MzCfmxEntity mzCfmxEntity);

        /// <summary>
        /// Lấy tổng số sự kiện chờ xử lý
        /// </summary>
        /// <returns></returns>
        NeedDealCountVO GetNeedDealCount();

        /// <summary>
        /// Lấy tổng số sự kiện chờ xử lý của kho thuốc
        /// </summary>
        /// <returns></returns>
        NeedDealCountVO GetNeedDealCountByYk();

        /// <summary>
        /// Lấy tổng số sự kiện chờ xử lý của nhà thuốc
        /// </summary>
        /// <returns></returns>
        NeedDealCountVO GetNeedDealCountByYf();

        /// <summary>
        /// lấy ngoại trú tháng đơn thuốc phát thuốc lần
        /// </summary>
        /// <returns></returns>
        List<FyCountVoByYfbm> GetMzFyCountVoByYfbm();

        /// <summary>
        /// lấy nội trú tháng y lệnh phát thuốc lần
        /// </summary>
        /// <returns></returns>
        List<FyCountVoByYfbm> GetZyFyCountVoByYfbm();

        /// <summary>
        /// lấy y lệnh lần|đơn thuốc thuốc lần
        /// </summary>
        /// <returns></returns>
        List<FyCountBydlVO> GetFyCountBydl();

        /// <summary>
        /// chèn ngoại trú thông tin đơn thuốc
        /// </summary>
        /// <param name="mzCfEntities"></param>
        /// <param name="mzCfmxEntities"></param>
        /// <returns></returns>
        string InsertOutpatientRpInfo(List<MzCfEntity> mzCfEntities, List<MzCfmxEntity> mzCfmxEntities);


        /// <summary>
        /// Xóa vật lý bảng chính và bảng chi tiết đơn thuốc, thận trọng khi dùng
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="organizeId"></param>
        /// <param name="yfbmCode"></param>
        /// <returns></returns>
        int DeleteRpInfo(string cfh, string organizeId, string yfbmCode);

        /// <summary>
        /// tra cứu thông tin đơn thuốc
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="xm"></param>
        /// <param name="cardNo"></param>
        /// <param name="invoiceNo"></param>
        /// <param name="ksmc">tên khoa</param>
        /// <param name="kssj">thời gian xếp thuốc</param>
        /// <param name="jssj">thời gian xếp thuốc</param>
        /// <param name="organizeId"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="jszt">quyết toán trạng thái 0-chưa quyết toán 1-đã quyết toán</param>
        /// <param name="settExpired">quyết toán đã hết hạn 1-có 0-không</param>
        /// <returns></returns>
        IList<CfxxVO> SelectRpList(Pagination pagination, string xm, string cardNo, string invoiceNo, string ksmc
            , DateTime kssj, DateTime jssj, string organizeId, string yfbmCode, int jszt, string settExpired);

        /// <summary>
        /// tra cứu thông tin đơn thuốc
        /// </summary>
        /// <param name="xm"></param>
        /// <param name="cardNo"></param>
        /// <param name="invoiceNo"></param>
        /// <param name="ksmc">tên khoa</param>
        /// <param name="kssj">thời gian xếp thuốc</param>
        /// <param name="jssj">thời gian xếp thuốc</param>
        /// <param name="organizeId"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="jszt">quyết toán trạng thái 0-chưa quyết toán 1-đã quyết toán</param>
        /// <returns></returns>
        List<CfxxVO> SelectRpList(string xm, string cardNo, string invoiceNo, string ksmc, DateTime kssj, DateTime jssj, string organizeId, string yfbmCode, int jszt);

        /// <summary>
        /// tra cứu chi tiết đơn thuốc
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="cfh"></param>
        /// <param name="organizeId"></param>
        /// <param name="yfbmCode"></param>
        /// <returns></returns>
        List<CfmxVO> SelectRpDetail(string cardNo, string cfh, string organizeId, string yfbmCode);

        /// <summary>
        /// tra cứu đơn thuốc xếp thuốc chi tiết
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="cfh"></param>
        /// <param name="organizeId"></param>
        /// <param name="yfbmCode"></param>
        /// <returns></returns>
        List<CfmxVO> SelectRpArrangementDetail(string cardNo, string cfh, string organizeId, string yfbmCode);

        /// <summary>
        /// hủy xếp thuốc
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="cfh"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        string CancelArrangedDrug(string cardNo, string cfh, string yfbmCode, string organizeId, string userCode);

        /// <summary>
        /// xếp thuốc ngoại trú
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="creatorCode"></param>
        /// <returns></returns>
        string DrugArrangement(patientInfoVO patientInfo, string yfbmCode, string organizeId,
           string creatorCode);

        /// <summary>
        /// lấy đã xếp chưa phát chưa quyết toán của xếp thuốc thông tin
        /// </summary>
        /// <param name="expirationDate"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="cfh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<OutpatientPrescriptionDetailBatchNumberEntity> SelectMxph(DateTime expirationDate, string yfbmCode, string organizeId);

        /// <summary>
        /// lấy cần hủy của đơn thuốc
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="processesMaxNum"></param>
        /// <param name="expirationDate"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<RpCancelVo> SelectCancelRps(string cfh, int processesMaxNum, DateTime expirationDate, string yfbmCode, string organizeId);

        /// <summary>
        /// hủy xếp thuốc
        /// </summary>
        /// <param name="mxphs"></param>
        /// <param name="cfh"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="userCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        string OutPatientCancelArrangementDrug(List<OutpatientPrescriptionDetailBatchNumberEntity> mxphs, string yfbmCode, string userCode, string organizeId);

        /// <summary>
        /// ngoại trú hủy xếp thuốc
        /// </summary>
        /// <param name="cfInfo"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        string OutPatientCancelArrangement(RpCancelVo cfInfo, string userCode);

        /// <summary>
        /// xếp thuốc xếp lên kệ, giải đông tồn kho
        /// </summary>
        /// <param name="mxphs"></param>
        /// <param name="userCode"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        string OutPatientSubtractForzenKcAndGj(List<OutpatientPrescriptionDetailBatchNumberEntity> mxphs, string userCode, Infrastructure.EF.EFDbTransaction db = null);

    }
}
