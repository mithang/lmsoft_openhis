using NewtouchHIS.Lib.Base.Model;
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
        /// Lấy khi ngày Hiệu lựcY lệnh
        /// </summary>
        /// <param name="bedCode"></param>
        /// <returns></returns>
        List<BedItemsVO> GetBedItems(WardMaintenanceRequestDto req);
        IList<Dispensing> GetDispensings(Pagination pagination, DispensingMXRequestDto req);
        IList<DispensingMX> GetDispensingMXs(Pagination pagination, string bysj);
        /// <summary>
        /// Lưu giường ký tự gắn Chi phí thông tin
        /// </summary>
        /// <param name="req"></param>
        void SaveBedItems(List<BedItemsVO> mxList, WardMaintenanceRequestDto req, string zyh);
        /// <summary>
        /// Lấy giường ký tự gắn Bác sĩ thông tin
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        List<BedDocsVO> GetBedDocs(patRequestDto req);

        /// <summary>
        /// Lưu giường ký tự gắn Bác sĩ thông tin
        /// </summary>
        /// <param name="req"></param>
        void SaveBedDocs(List<BedDocsVO> mxList, patRequestDto req);

        /// <summary>
        /// Lấy Bệnh nhân Chẩn đoán thông tin
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        List<PatDiagnosisVO> GetPatDiagnosis(patRequestDto req);
        /// <summary>
        /// kiểm tra Bệnh nhân có thể Không ra khỏi khu
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        string GetPatIsOutArea(patBedFeeRequestDto req);
        /// <summary>
        /// Lưu Bệnh nhân Chẩn đoán thông tin
        /// </summary>
        /// <param name="patOutAreaVO"></param>
        /// <param name="req"></param>
        string SavaPatDiagnosis(PatOutAreaInfoVO patOutAreaVO, patRequestDto req);
        /// <summary>
        /// Lưu Xuất viện Chẩn đoán Thông tin bệnh nhân
        /// </summary>
        /// <param name="patOutAreaVO"></param>
        /// <param name="req"></param>
        /// <returns></returns>
       string SavaOutPatDiagnosis(PatOutAreaVO patOutAreaVO, patRequestDto req);
		/// <summary>
		/// Lưu ra khỏi khu Chẩn đoán List
		/// </summary>
		/// <param name="PatDxList"></param>
		/// <returns></returns>
		string SavaPatDxList(List<InpatientPatientDiagnosisEntity> PatDxList,string OrganizeId);
		/// <summary>
		/// Lưu Xuất viện Chẩn đoán Entity
		/// </summary>
		/// <param name="PatDxEntity"></param>
		/// <returns></returns>
		//string SavaPatDxEntity(InpatientPatientDiagnosisEntity PatDxEntity);
		/// <summary>
		/// Lấy Nhập viện giường thẻ
		/// </summary>
		/// <param></param>
		List<InpatientBedCardVo> GetBedCard(string zyh, string OrganizeId);

        InpatientVo GetZyPatInfo(string zyh,string orgId);

        InpatiContinuePrintVo GetZyPatContinuePrint(string zyh, string orgId, string yzlx);
        InpatiContinuePrintPageVo GetZyPatContinuePrintPage(string zyh, string orgId, string yzlx, string xh, string page);

    }
}
