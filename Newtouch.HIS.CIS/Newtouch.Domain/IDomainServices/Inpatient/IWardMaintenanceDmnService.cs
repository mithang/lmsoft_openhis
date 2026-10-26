using Newtouch.Core.Common;
using Newtouch.Domain.DTO.InputDto.Inpatient;
using Newtouch.Domain.Entity;
using Newtouch.Domain.ValueObjects.Inpatient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.IDomainServices.Inpatient
{
    public interface IWardMaintenanceDmnService
    {
        /// <summary>
        /// Lấy y lệnh có hiệu lực trong ngày
        /// </summary>
        /// <param name="bedCode"></param>
        /// <returns></returns>
        List<BedItemsVO> GetBedItems(WardMaintenanceRequestDto req);
        IList<Dispensing> GetDispensings(Pagination pagination, DispensingMXRequestDto req);
        IList<DispensingMX> GetDispensingMXs(Pagination pagination, string bysj);
        /// <summary>
        /// Lưu thông tin chi phí liên kết giường bệnh
        /// </summary>
        /// <param name="req"></param>
        void SaveBedItems(List<BedItemsVO> mxList, WardMaintenanceRequestDto req, string zyh);
        /// <summary>
        /// Lấy thông tin bác sĩ liên kết giường bệnh
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        List<BedDocsVO> GetBedDocs(patRequestDto req);

        /// <summary>
        /// Lưu thông tin bác sĩ liên kết giường bệnh
        /// </summary>
        /// <param name="req"></param>
        void SaveBedDocs(List<BedDocsVO> mxList, patRequestDto req);

        /// <summary>
        /// Lấy thông tin chẩn đoán bệnh nhân
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        List<PatDiagnosisVO> GetPatDiagnosis(patRequestDto req);
        /// <summary>
        /// Xác thực bệnh nhân có thể ra khỏi khu hay không
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        string GetPatIsOutArea(patBedFeeRequestDto req);
        /// <summary>
        /// Lưu thông tin chẩn đoán bệnh nhân
        /// </summary>
        /// <param name="patOutAreaVO"></param>
        /// <param name="req"></param>
        string SavaPatDiagnosis(PatOutAreaInfoVO patOutAreaVO, patRequestDto req);
        /// <summary>
        /// Lưu thông tin chẩn đoán xuất viện bệnh nhân
        /// </summary>
        /// <param name="patOutAreaVO"></param>
        /// <param name="req"></param>
        /// <returns></returns>
       string SavaOutPatDiagnosis(PatOutAreaVO patOutAreaVO, patRequestDto req);
		/// <summary>
		/// Lưu chẩn đoán ra khỏi khuList
		/// </summary>
		/// <param name="PatDxList"></param>
		/// <returns></returns>
		string SavaPatDxList(List<InpatientPatientDiagnosisEntity> PatDxList,string OrganizeId);
		/// <summary>
		/// Lưu chẩn đoán xuất việnEntity
		/// </summary>
		/// <param name="PatDxEntity"></param>
		/// <returns></returns>
		//string SavaPatDxEntity(InpatientPatientDiagnosisEntity PatDxEntity);
		/// <summary>
		/// Lấy thẻ giường nội trú
		/// </summary>
		/// <param></param>
		List<InpatientBedCardVo> GetBedCard(string zyh, string OrganizeId);

        InpatientVo GetZyPatInfo(string zyh,string orgId);

        InpatiContinuePrintVo GetZyPatContinuePrint(string zyh, string orgId, string yzlx);
        InpatiContinuePrintPageVo GetZyPatContinuePrintPage(string zyh, string orgId, string yzlx, string xh, string page);

    }
}
