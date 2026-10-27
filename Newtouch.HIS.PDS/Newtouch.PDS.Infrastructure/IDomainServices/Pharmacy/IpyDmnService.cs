using System;
using NewtouchHIS.Lib.Base.Model;
using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.VO;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// Xếp thuốc
    /// </summary>
    public interface IPyDmnService
    {

        /// <summary>
        /// Hủy xếp thuốc
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
        /// Lấy tổng số sự kiện chờ xử lý kho thuốc
        /// </summary>
        /// <returns></returns>
        NeedDealCountVO GetNeedDealCountByYk();

        /// <summary>
        /// Lấy tổng số sự kiện chờ xử lý nhà thuốc
        /// </summary>
        /// <returns></returns>
        NeedDealCountVO GetNeedDealCountByYf();

        /// <summary>
        /// Lấy lần phát thuốc đơn tháng ngoại trú
        /// </summary>
        /// <returns></returns>
        List<FyCountVoByYfbm> GetMzFyCountVoByYfbm();

        /// <summary>
        /// Lấy lần phát thuốc y lệnh tháng nội trú
        /// </summary>
        /// <returns></returns>
        List<FyCountVoByYfbm> GetZyFyCountVoByYfbm();

        /// <summary>
        /// Lấy lần y lệnh|Lần thuốc trong đơn
        /// </summary>
        /// <returns></returns>
        List<FyCountBydlVO> GetFyCountBydl();

        /// <summary>
        /// Chèn thông tin đơn ngoại trú
        /// </summary>
        /// <param name="mzCfEntities"></param>
        /// <param name="mzCfmxEntities"></param>
        /// <returns></returns>
        string InsertOutpatientRpInfo(List<MzCfEntity> mzCfEntities, List<MzCfmxEntity> mzCfmxEntities);


        /// <summary>
        /// Xóa vật lý bảng chính và bảng chi tiết đơn thuốc   Thận trọng khi dùng
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="organizeId"></param>
        /// <param name="yfbmCode"></param>
        /// <returns></returns>
        int DeleteRpInfo(string cfh, string organizeId, string yfbmCode);

        /// <summary>
        /// Truy vấn thông tin đơn thuốc
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="xm"></param>
        /// <param name="cardNo"></param>
        /// <param name="invoiceNo"></param>
        /// <param name="ksmc">Tên khoa</param>
        /// <param name="kssj">Thời gian xếp thuốc</param>
        /// <param name="jssj">Thời gian xếp thuốc</param>
        /// <param name="organizeId"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="jszt">Trạng thái thanh toán 0-Chưa thanh toán 1-Đã thanh toán</param>
        /// <param name="settExpired">Thanh toán đã hết hạn 1-Có 0-Không</param>
        /// <returns></returns>
        IList<CfxxVO> SelectRpList(Pagination pagination, string xm, string cardNo, string invoiceNo, string ksmc
            , DateTime kssj, DateTime jssj, string organizeId, string yfbmCode, int jszt, string settExpired);

        /// <summary>
        /// Truy vấn thông tin đơn thuốc
        /// </summary>
        /// <param name="xm"></param>
        /// <param name="cardNo"></param>
        /// <param name="invoiceNo"></param>
        /// <param name="ksmc">Tên khoa</param>
        /// <param name="kssj">Thời gian xếp thuốc</param>
        /// <param name="jssj">Thời gian xếp thuốc</param>
        /// <param name="organizeId"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="jszt">Trạng thái thanh toán 0-Chưa thanh toán 1-Đã thanh toán</param>
        /// <returns></returns>
        List<CfxxVO> SelectRpList(string xm, string cardNo, string invoiceNo, string ksmc, DateTime kssj, DateTime jssj, string organizeId, string yfbmCode, int jszt);

        /// <summary>
        /// Truy vấn chi tiết đơn thuốc
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="cfh"></param>
        /// <param name="organizeId"></param>
        /// <param name="yfbmCode"></param>
        /// <returns></returns>
        List<CfmxVO> SelectRpDetail(string cardNo, string cfh, string organizeId, string yfbmCode);

        /// <summary>
        /// Truy vấn chi tiết xếp thuốc của đơn
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="cfh"></param>
        /// <param name="organizeId"></param>
        /// <param name="yfbmCode"></param>
        /// <returns></returns>
        List<CfmxVO> SelectRpArrangementDetail(string cardNo, string cfh, string organizeId, string yfbmCode);

        /// <summary>
        /// Hủy xếp thuốc
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="cfh"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        string CancelArrangedDrug(string cardNo, string cfh, string yfbmCode, string organizeId, string userCode);

        /// <summary>
        /// Xếp thuốc ngoại trú
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="creatorCode"></param>
        /// <returns></returns>
        string DrugArrangement(patientInfoVO patientInfo, string yfbmCode, string organizeId,
           string creatorCode);

        /// <summary>
        /// Lấy thông tin xếp thuốc đã xếp chưa phát chưa thanh toán
        /// </summary>
        /// <param name="expirationDate"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="cfh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<OutpatientPrescriptionDetailBatchNumberEntity> SelectMxph(DateTime expirationDate, string yfbmCode, string organizeId);

        /// <summary>
        /// Lấy đơn thuốc cần hủy
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="processesMaxNum"></param>
        /// <param name="expirationDate"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<RpCancelVo> SelectCancelRps(string cfh, int processesMaxNum, DateTime expirationDate, string yfbmCode, string organizeId);

        /// <summary>
        /// Hủy xếp thuốc
        /// </summary>
        /// <param name="mxphs"></param>
        /// <param name="cfh"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="userCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        string OutPatientCancelArrangementDrug(List<OutpatientPrescriptionDetailBatchNumberEntity> mxphs, string yfbmCode, string userCode, string organizeId);

        /// <summary>
        /// Hủy xếp thuốc ngoại trú
        /// </summary>
        /// <param name="cfInfo"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        string OutPatientCancelArrangement(RpCancelVo cfInfo, string userCode);

        /// <summary>
        /// Xếp thuốc lên kệ，Giải đông tồn kho
        /// </summary>
        /// <param name="mxphs"></param>
        /// <param name="userCode"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        string OutPatientSubtractForzenKcAndGj(List<OutpatientPrescriptionDetailBatchNumberEntity> mxphs, string userCode, Newtouch.Infrastructure.EFDbTransaction db = null);

    }
}
