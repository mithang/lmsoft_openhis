using Newtouch.MR.ManageSystem.Domain.DTO.InputDto;
using Newtouch.MR.ManageSystem.Domain.Entity;
using Newtouch.MR.ManageSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SqlSugar;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Utilities;
using NewtouchHIS.Lib.Framework.Filter;
using NewtouchHIS.Lib.Framework.Operator;

namespace Newtouch.MR.ManageSystem.Domain.IDomainServices
{
    public interface IMainRecordDmnService
    {

        BasyVO GetMainRecord(string orgId,string mainId);
        BasyVO GetMainRecordbybrjbxx(string orgId, string zyh);
        /// <summary>
        /// Chi phí nằm viện
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="zyh"></param>
        /// <returns></returns>
        BasyVO GetMainRecordZyFee(string orgId, string zyh);
        BasyVO GetMainRecordZyFeeDetail(BasyVO vo, string orgId, string zyh);
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
        void PatBasicSubmit(BasyVO vo, BasyRelVO reldto, string orgId, string user);
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
        IList<BasyVO> PatMainList(Pagination pagination, string orgId, string cyksrq, string cyjsrq, string bazt, string bq, string keyword, string sfzh,string cykb);
        /// <summary>
        ///  Lấy chẩn đoán xuất viện của bệnh nhân
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <param name="zdlb">Loại chẩn đoán 1Chẩn đoán nhập viện2Chẩn đoán xuất viện</param>
        /// <returns></returns>
        IList<PatZDListVO> GetPatHisZDInfo(string bah, string zyh, string orgId, int? zdlb);
        /// <summary>
        /// Lấy danh sách phẫu thuật của bệnh nhân
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<BasyOpDto> GetPatHisOperInfo(string bah, string zyh, string orgId);
        
        #region Bệnh nhân đã thanh toán không thể lưu chẩn đoán
        IList<PatZDListVO> SettlementQuery(string zyh,string orgId);
        List<ButtonEnableVO> DiagnosticSave(string Code, string orgId);
        #endregion
        string MedicalRecordExportQuery(string kssj, string jssj, string orgId);
        string MedicalRecordExportFYQuery(string kssj, string jssj, string orgId);
    }
}
