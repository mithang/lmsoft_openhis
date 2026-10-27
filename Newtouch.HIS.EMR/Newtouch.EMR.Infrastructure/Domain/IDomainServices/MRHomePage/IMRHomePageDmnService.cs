using NewtouchHIS.Lib.Base.Model;
using Newtouch.EMR.Domain.DTO.InputDto;
using Newtouch.EMR.Domain.DTO.OutputDto.MRUpload;
using Newtouch.EMR.Domain.ValueObjects.MRHomePage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtouch.EMR.Domain.IRepository;

namespace Newtouch.EMR.Domain.IDomainServices
{
    public interface IMRHomePageDmnService
    {
        BasyVO GetMainRecord(string orgId, string mainId);
        BasyVO GetMainRecordbybrjbxx(string orgId, string zyh);
        /// <summary>
        /// Chi phí nội trú
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="zyh"></param>
        /// <returns></returns>
        BasyVO GetMainRecordZyFee(string orgId, string zyh);
        IList<PatZDListVO> GetDiagLsit(string orgId, string bah, string zyh);
        IList<BasyOpDto> GetOPLsit(string orgId, string bah, string zyh);
        /// <summary>
        /// Lưu danh sách chẩn đoán
        /// </summary>
        /// <param name="list"></param>
        void ZdListSubmit(List<BasyZdDto> list, string orgId);
        /// <summary>
        /// Lưu danh sách phẫu thuật
        /// </summary>
        /// <param name="list"></param>
        /// <param name="orgId"></param>
        void OpListSubmit(List<BasyOpDto> list, string orgId);
        void PatBasicSubmit(BasyVO vo, BasyRelVO reldto, string blmbId, string orgId, string user, string username = null);
        /// <summary>
        /// Danh sách bệnh án
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="orgId"></param>
        /// <param name="cyksrq"></param>
        /// <param name="cyjsrq"></param>
        /// <param name="bazt"></param>
        /// <param name="bq"></param>
        /// <param name="keyword"></param>
        /// <param name="cyts"></param>
        /// <returns></returns>
        IList<BasyVO> PatMainList(Pagination pagination, string orgId, string cyksrq, string cyjsrq, string bazt, string bq, string keyword, string cyts);
        /// <summary>
        ///  Lấy chẩn đoán ra viện của bệnh nhân
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <param name="zdlb">Loại chẩn đoán 1 Chẩn đoán nhập viện2 Chẩn đoán ra viện</param>
        /// <returns></returns>
        IList<PatZDListVO> GetPatHisZDInfo(string bah, string zyh, string orgId, int? zdlb);
        /// <summary>
        /// Lấy danh sách phẫu thuật của bệnh nhân
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<BasyOpDto> GetPatHisOperInfo(string bah, string zyh, string orgId);
        /// <summary>
        /// Lấy thông tin bệnh án tải lên bảo hiểm y tế
        /// </summary>
        /// <param name="OrgId"></param>
        /// <param name="zyh"></param>
        /// <returns></returns>
        IList<YB_7600> GetHomePageforYB(string OrgId, string zyh);
        BasyVO GetMainRecordZyFeeDetail(BasyVO vo, string orgId, string zyh);

        #region Bệnh nhân đã thanh toán không thể lưu chẩn đoán
        IList<PatZDListVO> SettlementQuery(string zyh, string orgId);
        List<ButtonEnableVO> DiagnosticSave(string Code, string orgId);
        /// <summary>
        /// Đồng bộ chẩn đoán của bệnh án sang bảng chẩn đoán xuất khoa
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        void SynchronizationZD(string zyh, string orgId);
        #endregion

        string GetYbdmByGh(string rygh);
    }
}
