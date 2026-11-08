using System;
using System.Collections.Generic;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.Domain.DTO;
using Newtouch.Domain.DTO.InputDto.Inpatient;
using Newtouch.Domain.DTO.OutputDto;
using Newtouch.Domain.Entity;
using Newtouch.Domain.ValueObjects.Apply;
using Newtouch.Domain.ValueObjects.Inpatient;
using Newtouch.Domain.ValueObjects.Operation;
using Newtouch.Domain.ViewModels;
using Newtouch.Infrastructure;

namespace Newtouch.Domain.IDomainServices
{
    public interface IDoctorserviceDmnService
    {
        /// <summary>
        /// Y lệnh chuyển đổi thành Dữ liệu bảng của Trạng thái
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="reqdoctorservices"></param>
        void SubmitdoctorService(string orgId, List<DoctorServiceRequestDto> reqdoctorservices, List<string> deldata);

        /// <summary>
        /// Y lệnh chuyển đổi thành Dữ liệu bảng của Trạng thái Dài hạn,Tạm thời không đi cấu hình,có Bác sĩ chính xem điều khiển
        /// </summary>
        /// <param name="orgId"></param>3
        /// <param name="reqdoctorservices"></param>
        /// <param name="deldata"></param>
        /// <param name="yzh"></param>
        void SubmitdoctorServiceV2(string orgId, List<DoctorServiceRequestDto> reqdoctorservices, List<string> deldata, out string yzh);

        /// <summary>
        /// Lấy khi ngày Hiệu lựcY lệnh
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="zyh"></param>
        /// <returns></returns>
        IList<CurrentViewVO> GetTodayValidService(Pagination pagination, string orgId, string zyh);

        IList<DSrepeatResponseDto> DoctorserviceValidate(IList<DSrepeatRequestDto> req, string zyh, string orgId);
        /// <summary>
        /// Tần suất Mã chuyển đổi thành dài hạn/tạm thời Cờ
        /// </summary>
        /// <param name="req"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<DSrepeatRequestDto> DSTransferCL(IList<DSrepeatRequestVO> req, string orgId);

        void GetypPredata(string pcdm, string yfdm, string ypdm, string dwlb, string orgId, ref string pcmc, ref string yfmc, ref string redundant_jldw, ref string jx, ref decimal? jlzhxs, ref decimal? zyzhxs, ref string qzfs);

        /// <summary>
        /// Lấy Số lượng đơn vị tính
        /// </summary>
        /// <param name="sfxmCode"></param>
        /// <param name="orgId"></param>
        /// <param name="dwjls"></param>
         void GetdwjlsBysfxmCode(string sfxmCode, string orgId, ref int? dwjls,ref decimal? dj);
        /// <summary>
        /// Lấy Khoa thực hiện Tên
        /// </summary>
        /// <param name="zxksCode"></param>
        /// <param name="orgId"></param>
        /// <param name="zxksmc"></param>
        void Getzxksmc(string zxksCode, string orgId, ref string zxksmc);

        /// <summary>
        /// Tính toán mỗi ngày Tần suất
        /// </summary>
        /// <param name="zxdw"></param>
        /// <param name="zydw"></param>
        /// <param name="zxcs"></param>
        /// <param name="zxzq"></param>
        /// <param name="zxzqdw"></param>
        /// <returns></returns>
        float Getpcsl(int zxcs, int zxzq, EnumZxzqdw zxzqdw);

        /// <summary>
        /// Tính toán Thuốc Số lượng
        /// </summary>
        /// <param name="jlzhxs"></param>
        /// <param name="zyzhxs"></param>
        /// <param name="ypjl"></param>
        /// <param name="ts"></param>
        /// <param name="dwlb"></param>
        /// <param name="orgId"></param>
        /// <param name="zxcs"></param>
        /// <param name="zxzq"></param>
        /// <param name="zxzqdw"></param>
        /// <returns></returns>
        int Getypsl(decimal? jlzhxs, decimal? zyzhxs, float ypjl, EnumYPdwlb dwlb, string orgId, int zxcs, int zxzq, EnumZxzqdw zxzqdw, string qzfs, int ts = 1);

        #region Y lệnh Tra cứu
        List<AdviceListGridVO> AdviceGridView(Pagination pagination, AdviceListRequestVO req);

        /// <summary>
        /// DừngY lệnh dài hạn hoặc Hủy bỏY lệnh tạm thời
        /// </summary>
        /// <param name="yzId"></param>
        /// <param name="yzlb"></param>
        /// <param name="tzsj"></param>
        /// <param name="czr"></param>
        void AdviceStop(string yzId, DateTime? tzsj, string czr, string orgId,string yzlx,string zyh,string iszt);

        /// <summary>
        /// vật lý Xóa
        /// </summary>
        /// <param name="yzId"></param>
        void AdviceDel(string yzId, string orgId,string yzlx,string zyh,string iszt, string conflinktoOR);


        /// <summary>
        /// rút DC rút dc cùng Số nhóm củaY lệnh
        /// </summary>
        /// <param name="yzId"></param>
        void Advicedc(string yzId, string orgId,string yzlx,string zyh,string isFeeGroup);

        /// <summary>
        /// Xuất viện toàn Dừng
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="tzsj"></param>
        /// <param name="orgId"></param>
        /// <param name="czr"></param>
        void AdviceLeaveHospitalStopSubmit(string zyh, DateTime tzsj, string orgId, string czr);

        /// <summary>
        /// chuyển khu toàn Dừng
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <param name="czr"></param>
        void AdviceTransferWardStopSubmit(TransferWardRequestVO req);

        void RegistTzyz(string yzId, string OrganizeId, string yzlb, string zyh);

        #endregion
        /// <summary>
        /// Tần suất y lệnh code Lấy Tần suất entity
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        SysMedicalOrderFrequencyVEntity getpcInfoByCode(string code, string orgId);

        string GetqhdyzSqtxData(string orgId, List<DoctorServiceRequestDto> reqdoctorservices, InpatientInfo brxx, string rygh,string username,string yzcfh,out string jlId);
        /// <summary>
        /// việc trước Phê duyệt
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="reqdoctorservices"></param>
        /// <param name="brxx"></param>
        /// <param name="rygh"></param>
        /// <param name="username"></param>
        /// <param name="HospitalCode"></param>
        /// <param name="HospitalName"></param>
        /// <returns></returns>
        string GetPriorReviewData(string orgId, List<DoctorServiceRequestDto> reqdoctorservices, InpatientInfo brxx, string rygh, string username,string HospitalCode,string HospitalName,string yzcfh, string GetMAC);
        List<RehabVO> GetSfxmZxksSelectJson(string zyh);
        List<RehabVO> GetSfxmZxksSelectJson(string zyh,string kspy);
        /// <summary>
        /// bệnh án Phê duyệt
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="reqdoctorservices"></param>
        /// <param name="brxx"></param>
        /// <param name="rygh"></param>
        /// <param name="username"></param>
        /// <param name="HospitalCode"></param>
        /// <param name="HospitalName"></param>
        /// <returns></returns>
        string GetBashData(string orgId, string zyh, string rygh, string username, string GetMAC);
        /// <summary>
        /// dip chất lượng Phê duyệt
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="reqdoctorservices"></param>
        /// <param name="brxx"></param>
        /// <param name="rygh"></param>
        /// <param name="username"></param>
        /// <param name="HospitalCode"></param>
        /// <param name="HospitalName"></param>
        /// <returns></returns>
        string GetDrgData(string orgId, string zyh, string rygh, string username, string GetMAC);
        /// <summary>
        /// Phê duyệt Dữ liệu Xóa
        /// </summary>
        /// <returns></returns>
        string DeletePriorReview(string zyh, string yzid, string yzlx,string OrganizeId, string GetMAC);
        string GetDiagnoseData();
        List<SelectUpdataOpertionVO> Ssupdate(string yzId,string zyh,string OrganizeId);

        List<yfsfxmdyDto> GetSfxmYfList(string orgId,string yfcode= null,string keyword=null);


        #region  Thực hiện kỹ thuật y học
        void jyjcExec(List<jyjcExecReq> jyjclist, string orgId, string zxr);
        void UpdatejyjcExecIsjf(List<string> jyjclist, string orgId, string czr, string status);
        void CancaljyjcExec(List<string> jyjclist, string orgId, string czr);

        #endregion
        string GetmxbzdList(string orgId, string zdcode);
        string GetxzklsList(string orgId, YpxzkldataDTO Ypxzkldata);
        string addcqyz(string zyh, string yzh, string zh, List<YzbindingfeeVo> ItemFeeVO,string OrganizeId,string usercode);
        PatientMedicalDTO GetlsorcqyzData(string zyh, string yzid, string orgId);
        string DeleteBind(string zyh, string yzid, string yzxz, string orgId);
        int CountLISztzy(string orgId, string zyh);

        List<ypyfdataDto> GetYfData (string orgId);
    }
}
