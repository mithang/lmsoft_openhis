using System.Collections.Generic;
using System.Data;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.Domain.DTO.InputDto.Outpatient;
using Newtouch.Domain.DTO.OutputDto;
using Newtouch.Domain.Entity;
using Newtouch.Domain.ValueObjects;
using Newtouch.Domain.ValueObjects.Outpatient;
using Newtouch.Domain.ViewModels;

namespace Newtouch.Domain.IDomainServices
{
    public interface IMedicalRecordDmnService
    {
        /// <summary>
        /// lịch sửHồ sơcây
        /// </summary>
        /// <param name="blh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<MedicalRecordTreeVO> GetHistoryMedicalRecordTree(string blh, int queryDate, string orgId);

        /// <summary>
        /// Chi tiếtcâytiếtđiểmnội dung (TheojzIdTra cứuHồ sơvàĐơn thuốcnội dung)
        /// </summary>
        /// <param name="jzId"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        NodeContentDto SelectNodeContent(string jzId);

        /// <summary>
        /// Chi tiếtcâytiếtđiểmnội dung (TheoMbIdTra cứuHồ sơvàĐơn thuốcnội dung)
        /// </summary>
        /// <param name="jzId"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        NodeContentDto SelectNodeContentByBlmb(string mbId,string orgId);
        
        /// <summary>
        /// LưuKhám bệnhghi chép,Chẩn đoán,Hồ sơ,Đơn thuốc,Đơn thuốcchi tiết
        /// </summary>
        /// <param name="jzObject"></param>
        /// <param name="xydList"></param>
        /// <param name="zyzdList"></param>
        /// <param name="blObject"></param>
        /// <param name="cfDto"></param>
        /// <param name="operatorCode"></param>
        /// <param name="addedYpCfList"></param>
        /// <param name="updatedYpCfList"></param>
        void SaveMedicalRecord(TreatmentEntity jzObject,
            List<WMDiagnosisHtmlVO> xydList,
            List<TCMDiagnosisHtmlVO> zyzdList,
            MedicalRecordEntity blObject,
            List<PrescriptionDTO> cfDto,
            List<CFZDDiagnosisHtmlVO> cfzdlist,
            string operatorCode,
            out List<string> addedYpCfList, out List<string> updatedYpCfList);

        /// <summary>
        /// Hủy bỏHồ sơ góigồmHồ sơChẩn đoán Đơn thuốc Đơn thuốcchi tiết
        /// </summary>
        void ObsoleteMedicalRecord(string jzId, string orgId);

        /// <summary>
        /// Hủy bỏhisMột lầnKhám bệnhcủatất cảĐơn thuốc
        /// </summary>
        void ObsoleteAllPresToHIS(string jzId, string orgId);

        /// <summary>
        /// gửiMột lầnKhám bệnhcủatất cảĐơn thuốc(nhưngkhôngđẩyĐã thu phícủa),hoặcchỉđịnhĐơn thuốc
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        /// <param name="opr">Thao tácviên</param>
        /// <param name="cfId">chỉđịnhkhigửiphiếumộtĐơn thuốc,KhôngthìgửiMột lầnKhám bệnhcủatất cảĐơn thuốc(nhưngkhôngđẩyĐã thu phícủa)</param>
        bool sendPresToHis(string mzh, string orgId, string opr, string cfId = null);

        bool sendPresToPDS(string orgId, string operatorCode, string mzh, string cfId = null, List<string> addedYpCfList = null, List<string> updatedYpCfList = null);

        /// <summary>
        /// Cập nhậtpdsđãcóĐơn thuốc
        /// </summary>
        /// <param name="jzObject"></param>
        /// <param name="cfh"></param>
        /// <param name="cfId"></param>
        /// <param name="operatorCode"></param>
        /// <param name="rpDetail"></param>
        /// <param name="ypyfList"></param>
        /// <returns></returns>
        string UpdateRpToPds(TreatmentEntity jzObject,
            string cfh,
            string cfId,
            string operatorCode,
            List<PrescriptionDetailEntity> rpDetail,
            IList<SysMedicineUsageVEntity> ypyfList);

        /// <summary>
        /// gửiĐơn thuốc mớiđếnNhà thuốcKho thuốc
        /// </summary>
        /// <param name="jzObject"></param>
        /// <param name="cfh"></param>
        /// <param name="cfId"></param>
        /// <param name="operatorCode"></param>
        /// <param name="rpDetail"></param>
        /// <param name="ypyfList"></param>
        /// <returns></returns>
        string SendNewRpToPds(TreatmentEntity jzObject,
            string cfh,
            string cfId,
            string operatorCode,
            List<PrescriptionDetailEntity> rpDetail,
            IList<SysMedicineUsageVEntity> ypyfList);

        /// <summary>
        /// Hủy đơn thuốc(vàđẩychoHIS)
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="mzh"></param>
        /// <param name="cfId"></param>
        void cancelSinglePres(string orgId, string mzh, string cfId
            , out int apicflx, out string apicfh, out bool isAlertinherit);

        void delDzcf(string orgId, string mzh, string cfId
            , out int apicflx, out string apicfh, out bool isAlertinherit);
        /// <summary>
        /// Hủy bỏHISphiếutờ
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="mzh"></param>
        /// <param name="cfId"></param>
        void cancelSinglePresToHIS(string orgId, string mzh, string cfId
            , int apicflx, string apicfh);

        /// <summary>
        /// Hủy bỏPDSphiếutờ
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="mzh"></param>
        /// <param name="cfId"></param>
        void cancelSinglePresToPDS(string orgId, string operatorCode, string mzh, string cfId
            , int apicflx, string apicfh);

        /// <summary>
        /// Hủy đơn thuốckhiLấy
        /// </summary>
        /// <param name="cfId"></param>
        /// <returns></returns>
        List<PrescriptionHtmlVO> GetzfcfJson(string cfId);
        /// <summary>
        /// LấyMụchoặcThuốcthông tin
        /// </summary>
        /// <param name="code"></param>
        /// <param name="cxlx"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        SqtxXmYpInfoVO GetXmYpInfo(string code, string cxlx, string orgId);

        IList<OutpatientLogQueryVO> GetLogGridList(Pagination pagination, string keyword, string kssj, string jssj, string jzysgh, string orgId, string hznl, string xtxy, string qbdc);

        IList<OutpatientLogQueryVO> GetLogGridList(string keyword, string kssj, string jssj, string jzysgh, string orgId, string hznl, string xtxy, string qbdc);

        DataTable DTXD_UploadData(YCYL_DtxdscDTO dto);
        DataTable DTXD_SqdUploadData(YCYL_DtxdSqdDTO dto);
        /// <summary>
        /// Lấyđộngtrạng tháitimđiệnTên
        /// </summary>
        /// <param name="mzh"></param>
        /// <returns></returns>
        YCYL_DtxdscDTO GetJcDtxdmc(string mzh, string orgId);
        YCYL_DtxdSqdDTO GetJcDtxdSqd(string mzh, string orgId, string sqdh);
        DataTable chakanmingxidiaoyong(string kh);

        /// <summary>
        /// xatrìnhy tếtimđiệnĐăng ký
        /// </summary>
        /// <param name="PlaceerOrderNo"></param>
        DataTable xindianshenqing(string patid, string orderno);

        /// <summary>
        /// xatrìnhXét nghiệmĐăng ký
        /// </summary>
        /// <param name="patid"></param>
        /// <param name="jzlsh"></param>
        /// <returns></returns>
        DataTable jianyanshenqing(string patid, string jzlsh, string OrgId);

        /// <summary>
        /// xatrìnhy tếHình ảnhĐăng ký
        /// </summary>
        /// <param name="hispatientid"></param>
        /// <param name="name"></param>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <returns></returns>
        DataTable yingxiangshenqing(string patid, string AccessionNumber);

        /// <summary>
        /// xatrìnhy tếtimđiệngọiđọc
        /// </summary>
        /// <param name="PlaceerOrderNo"></param>
        DataTable xindiandiaoyue(string kh, string orderno);

        List<string> GetCfhBycflx(string orgId, int cflx, string mzh);

        List<PrescriptionEntity> GetcfByJzId(string orgId, string jzId);

        /// <summary>
        /// Lấycuối cùngkhámtraphí
        /// </summary>
        /// <param name="blh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        lastzcfDto GetLastzcf(string blh, string orgId);
        /// <summary>
        /// LấyTổ chứcthông tin
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        OrganizationData GetOrgInfo(string orgId);
        /// <summary>
        /// Đơn thuốcnhắc trước
        /// </summary>
        /// <param name="xyzdList"></param>
        /// <param name="zyzdList"></param>
        /// <param name="strCfList"></param>
        /// <param name="mzh"></param>
        /// <returns></returns>
        string GetqhdCfSqtxData(List<WMDiagnosisHtmlVO> xyzdList, List<TCMDiagnosisHtmlVO> zyzdList, string strCfList, string mzh, out string jlId, string orgId, string rygh, string userName);

        List<LisReportSqdhValueVo> GetLisSqdhData(string orgId, string zymzh, string type, string ztmc, string kssj, string jssj);
        List<PacsReportSqdhValueVo> GetPacsSqdhData(string orgId, string zymzh, string type, string ztmc, string kssj, string jssj);
        IList<LisReportSqdyczValueVo> GetLisSqdyczData(Pagination pagination, string orgId, string zymzh, string type, string ztmc, string ycbz, string sqdht);

        void SyncToPdsQuery(string operatorCode, string ghrq);

        List<PrescriptionDetailVO> Getsfmbxm(string orgId, string sfmb, string sfmbmc);

        /// <summary>
        /// sắc hộMụcnội dung
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        TCMDjXMVO GetBindTCMDj(string OrganizeId);


        List<OutPatientRegistrationInfoDTO> Getdjzlist(Pagination pagination, string orgid, string ksCode, string ysgh, string mjzbz, string jiuzhenbz);

        IList<MedicineInfoVO2> GetMedicineInfoList(Pagination pagination, string xmbm, string xmmc, string ck_kc, string orgId);

        int CountLISztmz(string orgId, string jzId);

        string GetYpjx(string code, string orgId);
    }
}
