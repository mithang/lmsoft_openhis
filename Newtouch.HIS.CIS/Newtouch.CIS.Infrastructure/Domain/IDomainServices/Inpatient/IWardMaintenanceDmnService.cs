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
        /// LấykhingàyHiệu lựcY lệnh
        /// </summary>
        /// <param name="bedCode"></param>
        /// <returns></returns>
        List<BedItemsVO> GetBedItems(WardMaintenanceRequestDto req);
        IList<Dispensing> GetDispensings(Pagination pagination, DispensingMXRequestDto req);
        IList<DispensingMX> GetDispensingMXs(Pagination pagination, string bysj);
        /// <summary>
        /// Lưugiường ký tựgắnChi phíthông tin
        /// </summary>
        /// <param name="req"></param>
        void SaveBedItems(List<BedItemsVO> mxList, WardMaintenanceRequestDto req, string zyh);
        /// <summary>
        /// Lấygiường ký tựgắnBác sĩthông tin
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        List<BedDocsVO> GetBedDocs(patRequestDto req);

        /// <summary>
        /// Lưugiường ký tựgắnBác sĩthông tin
        /// </summary>
        /// <param name="req"></param>
        void SaveBedDocs(List<BedDocsVO> mxList, patRequestDto req);

        /// <summary>
        /// LấyBệnh nhânChẩn đoánthông tin
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        List<PatDiagnosisVO> GetPatDiagnosis(patRequestDto req);
        /// <summary>
        /// kiểm traBệnh nhâncó thểKhôngra khỏi khu
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        string GetPatIsOutArea(patBedFeeRequestDto req);
        /// <summary>
        /// LưuBệnh nhânChẩn đoánthông tin
        /// </summary>
        /// <param name="patOutAreaVO"></param>
        /// <param name="req"></param>
        string SavaPatDiagnosis(PatOutAreaInfoVO patOutAreaVO, patRequestDto req);
        /// <summary>
        /// LưuXuất việnChẩn đoánThông tin bệnh nhân
        /// </summary>
        /// <param name="patOutAreaVO"></param>
        /// <param name="req"></param>
        /// <returns></returns>
       string SavaOutPatDiagnosis(PatOutAreaVO patOutAreaVO, patRequestDto req);
		/// <summary>
		/// Lưura khỏi khuChẩn đoánList
		/// </summary>
		/// <param name="PatDxList"></param>
		/// <returns></returns>
		string SavaPatDxList(List<InpatientPatientDiagnosisEntity> PatDxList,string OrganizeId);
		/// <summary>
		/// LưuXuất việnChẩn đoánEntity
		/// </summary>
		/// <param name="PatDxEntity"></param>
		/// <returns></returns>
		//string SavaPatDxEntity(InpatientPatientDiagnosisEntity PatDxEntity);
		/// <summary>
		/// LấyNhập việngiườngthẻ
		/// </summary>
		/// <param></param>
		List<InpatientBedCardVo> GetBedCard(string zyh, string OrganizeId);

        InpatientVo GetZyPatInfo(string zyh,string orgId);

        InpatiContinuePrintVo GetZyPatContinuePrint(string zyh, string orgId, string yzlx);
        InpatiContinuePrintPageVo GetZyPatContinuePrintPage(string zyh, string orgId, string yzlx, string xh, string page);

    }
}
