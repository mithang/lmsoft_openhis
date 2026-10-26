using Newtouch.Core.Common;
using Newtouch.HIS.Domain.DTO;
using Newtouch.HIS.Domain.DTO.InputDto;
using Newtouch.HIS.Domain.DTO.OutputDto.HospitalizationManage;
using Newtouch.HIS.Domain.DTO.OutputDto.OutpatientManage;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.Entity.PatientManage;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.ValueObjects.PatientManage;
using Newtouch.HIS.Domain.ValueObjects.YibaoInterfaceManage;
using Newtouch.HIS.Proxy.guian.DTO;
using Newtouch.HIS.Sett.Request.Patient;
using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    public interface IPatientBasicInfoDmnService
    {

        /// <summary>
        /// Tìm thông tin cơ bản bệnh nhân theo mã nội bộ bệnh nhân
        /// </summary>
        /// <param name="patid"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
         OutpatAccInfoDto GetPatInfoByPatid(int patid, string orgId);
        /// <summary>
        /// Đồng thời lưu thông tin bệnh nhân và thẻ
        /// </summary>
        /// <returns></returns>
        void SavePatBasicCardInfo(SysHosBasicInfoVO vo, string orgId, string CreatorCode);

        DateTime? IFCQRQISJZSJ(string zyh, string orgId);

        /// <summary>
        /// TheopatidLấy thông tin cơ bản bệnh nhân đăng ký nội trú
        /// </summary>
        /// <param name="patid"></param>
        /// <param name="result"></param>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        List<SysHosBasicInfoVO> GetPatBasicCardInfo(string patid, out bool result, string OrganizeId);

        /// <summary>
        /// Lấy quản lý bệnh nhânformBiểu mẫu
        /// </summary>
        /// <param name="patid"></param>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        SysHosBasicInfoVO GetSyspatientFormJson(string patid, string OrganizeId,string zjh);

        /// <summary>
        /// Đồng thời lưu thông tin cơ bản nội trú và thông tin tài khoản tạm ứng
        /// </summary>
        /// <param name="SysHosBasicInfoVO"></param>
        /// <param name="OrganizeId"></param>
        /// <param name="res"></param>
        string SaveSysBasicAccountInfo(SysHosBasicInfoVO SysHosBasicInfoVO, string OrganizeId, out string res);

        /// <summary>
        /// check zyhCó tồn tại tính phí hiệu lực hay không
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        bool CheckHasFee(string zyh, string orgId);
        /// <summary>
        /// Bệnh nhân đã vào khu hay chưa
        /// </summary>
        /// <typeparam name="SysPatientManageSelectVO"></typeparam>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <returns></returns>
        bool BoolRuQu(string zyh, string orgid);

        #region 病人管理

        /// <summary>
        /// Tra cứu quản lý bệnh nhân
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="orgId"></param>
        /// <param name="blh"></param>
        /// <param name="xm"></param>
        /// <param name="xlbrbz"></param>
        /// <returns></returns>
        List<SysPatientManageSelectVO> GetList(Pagination pagination, string orgId,string zjh);
        #endregion

        #region 重庆医保异常交易
        List<YibaoDataVo> GetYbCancelList(Pagination pagination, string isjs, string lx, string keyword);
        #endregion

        /// <summary>
        /// Tra cứu thông tin cơ bản bệnh nhân nội trú hiệu lực theo số nhập viện
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        List<SysHosBasicInfoVO> GetSysBasicByZHY(string kh, string zyh, string orgid);

        /// <summary>
        /// Bảng kê chi phí trong ngày Dùng để tìm kiếm lớp nổi số nhập viện
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgid"></param>
        /// <returns></returns>
        List<SysHosBasicInfoVO> GetSysBasicVagueByZHY(string zyh, string orgid);

        /// <summary>
        /// Tra cứu đăng ký khám bệnh nhân ngoại trú
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="flag"></param>
        /// <param name="msg"></param>
        /// <param name="lastUpdateTime"></param>
        /// <param name="outpatientNumber"></param>
        /// <returns></returns>
        List<OutPatientRegistrationInfoDTO> OutPatientRegistrationQuery(
            string orgId, ref string flag, ref string msg
            , DateTime? lastUpdateTime = null, string outpatientNumber = null
            , string ksCode = null, string ysgh = null
            , string mjzbz = null
            , string jiuzhenbz = null
            , string keyword = null
            , Pagination pagination = null);

        /// <summary>
        /// Tra cứu bệnh nhân nội trú(Dùng cho giao diện)
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="flag"></param>
        /// <param name="msg"></param>
        /// <param name="lastUpdateTime"></param>
        /// <param name="zyh"></param>
        /// <param name="bqCode"></param>
        /// <param name="zybz"></param>
        /// <param name="keyword"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        List<InPatientInfoDTO> InPatientQuery(
            string orgId, ref string flag, ref string msg
            , DateTime? lastUpdateTime = null, string zyh = null
            , string bqCode = null, string zybz = null
            , string keyword = null
            , Pagination pagination = null);

        /// <summary>
        /// Tra cứu bệnh nhân nội trú(Dùng để tra cứu bệnh nhân)
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="flag"></param>
        /// <param name="msg"></param>
        /// <param name="lastUpdateTime"></param>
        /// <param name="zyh"></param>
        /// <param name="bqCode"></param>
        /// <param name="zybz"></param>
        /// <param name="keyword"></param>
        /// <param name="pagination"></param>
        /// <param name="ryrqkssj"></param>
        /// <param name="ryrqjssj"></param>
        /// <returns></returns>
        IList<InPatientInfoVO> GetInPatientList(
            string orgId, ref string flag, ref string msg
            , DateTime? lastUpdateTime = null, string zyh = null
            , string bqCode = null, string zybz = null
            , string keyword = null
            , Pagination pagination = null, DateTime? ryrqkssj = null, DateTime? ryrqjssj = null,string ksmc=null,string bqmc=null);

        /// <summary>
        /// Cập nhật chẩn đoán chính trong nhiều chẩn đoán của bệnh nhân nội trú
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="zyh"></param>
        /// <param name="zddm"></param>
        void UpdatePatInpMultiDiagnosis(string orgId, string zyh, string zddm,string user);

        /// <summary>
        /// Thông tin bệnh nhân nội trú Dữ liệu phân trang
        /// </summary>
        /// <param name="pagination">Thông tin phân trang</param>
        /// <param name="zyh">Số nhập viện</param>
        /// <param name="xm">Họ tên</param>
        /// <returns></returns>
        IList<HospPatientBasicInfoEntity> GetPatSearchList(Pagination pagination, string orgId, string zyh, string xm, string brzybzType = null);

        /// <summary>
        /// Hủy nhập viện
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        void CancelAdmission(string zyh, string orgId);

        /// <summary>
        /// Tự chi trả chuyển sang BHYT
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="patid"></param>
        /// <param name="cardInfo"></param>
        /// <param name="ryblInfo"></param>
        void InpatientZFchangetoYB(string orgId, string zyh, int patid, GACardReadInfoDTO cardInfo, GuianRybl21OutInfoEntity ryblInfo);

        /// <summary>
        /// Tự chi trả chuyển sang Bảo hiểm nông thôn mới
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="zyh"></param>
        /// <param name="patid"></param>
        /// <param name="cardInfo"></param>
        /// <param name="ryblInfo"></param>
        bool InpatientZFchangetoXNH(string orgId, string zyh, HosPatZFToXNHVO cardInfo,
            GuianXnhS04InfoEntity ryblInfo, out string msg);
        /// <summary>
        /// BHYT chuyển sang tự chi trả
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="zyh"></param>
        /// <param name="patid"></param>
        void InpatientYBchangetoZF(string orgId, string zyh, int patid);

        string GetUpqdScData(string kssj, string jssj);
        #region 新农合

        SysHosBasicInfoVO GetZfToXnhPatInfo(string zyh, string orgId);
         S04RequestDTO ComposeS04par(SysHosBasicInfoVO vo, string orgId);
         Response<S04ResponseDTO> S04submit(S04RequestDTO request, string orgId);
         bool InpatXnhInsertS04data(GuianXnhS04InfoEntity ryblInfo, out string msg);
         Response<string> S05submit(S05RequestDTO request, string orgId);
         Response<string> S06submit(S06RequestDTO request, string orgId);
         S06RequestDTO ComposeS06par(SysHosBasicInfoVO vo, string orgId);
        /// <summary>
        /// Xác minh đối chiếu giao diện Bảo hiểm nông thôn mới
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="code"></param>
        /// <returns></returns>
         bool validateTTentity(string orgId, string code);
		#endregion

		#region 重庆医保

	    Input_Bbrxx GetCQjzdjInfo(string zyh, string orgId);

        void InPatZFchangetoYB(string orgId, string zyh, int patid, ZYToYBDto patInfo, CqybMedicalReg02Entity ryblInfo);
        void OutPatZFchangetoYB(string orgId, string mzh, int patid, ZYToYBDto patInfo);
        void UpdateCqybOut02(string zyh,string orgId);
        #endregion

        #region 重庆医保
        Input_Bbrxx GetQHDjzdjInfo(string zyh, string orgId);

        string GetQHDTFDate(string zyh, string orgId);

        string GetQHDSzshData(string zyh, string orgId);
        #endregion

        #region 医保业务
        /// <summary>
        /// Có phải là lần khám đầu tiên hay không
        /// </summary>
        /// <param name="sfzh"></param>
        /// <param name="xm"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        SysPatBasicInfoVo ValidateFirstVisit(string sfzh, string xm, string orgId,string kh= null, string jzpzlx = null);
        #endregion
        bool SignInStateUpdate(string mzh, string calledstu,string yhcode,string orgId);
        bool PatientAppointment(string mzh, string orgId);

        IList<InPatientDTO> InPatientInfoQuery(InPatientInfoQueryRequest dto);
        IList<OutPatientRegistrationInfoDTO> OutPatientConsultationQuery(OutPatientConsultationQueryRequest dto);
        IList<InpatientDayFeeDTO> InpatientDayFee(InpatientDayFeeRequest dto);
        /// <summary>
        /// Lấy thông tin chẩn đoán nhập viện theo số nhập viện
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="HospitalID"></param>
        /// <returns></returns>
        IList<ryzd> getRyzdByZyh(string zyh, string HospitalID);
        /// <summary>
        /// Lấy thông tin chẩn đoán xuất viện theo số nhập viện
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="HospitalID"></param>
        /// <returns></returns>
        IList<cyzd> getCyzdByZyh(string zyh, string HospitalID);

        /// <summary>
        /// Hóa đơn lịch sử ngoại trú
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        IList<MZhistorybillDTO> MZhistorybill(MZhistorybillRequest req);

        /// <summary>
        /// Chi tiết hóa đơn
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        IList<MZhistorybillMXDTO> MZhistorybillMX(MZhistorybillMXRequest req);

        /// <summary>
        /// Cập nhật mức cảnh báo
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="organizeId"></param>
        /// <param name="bje"></param>
        /// <returns></returns>
        bool updateZybrxxkExpandBje(string zyh, string organizeId, decimal? bje);


        bool updateZybrxxkExpandZhye(string zyh, string organizeId, decimal? zhye);
        List<HisKsZdVO> GetksZzdList(string orgid);

        /// <summary>
        /// Tra cứu tải lên bảng kê quyết toán quỹ
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="sczt"></param>
        /// <param name="tjzt"></param>
        /// <param name="keyword"></param>
        /// <param name="pagination"></param>
        /// <param name="cykssj"></param>
        /// <param name="cyjssj"></param>
        /// <returns></returns>
        IList<InSettlementInfoVO> GetSettlementList(string orgId, string sczt, string tjzt, string keyword = null, Pagination pagination = null, DateTime? cykssj = null, DateTime? cyjssj = null);

    }
}
