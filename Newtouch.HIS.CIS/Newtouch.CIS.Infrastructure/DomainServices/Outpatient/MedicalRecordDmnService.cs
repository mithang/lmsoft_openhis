

using Newtouch.Domain.Entity;

using DbConfigHelper;
using Newtouch.Infrastructure.EF;
using Newtouch.CIS.Repository;

using Newtouch.Common;

using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.CIS.Compat;
using Newtouch.Common.Web;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.CIS.Compat;
using Newtouch.Domain.BusinessObjects;
using Newtouch.Domain.DTO.InputDto.Outpatient;
using Newtouch.Domain.DTO.OutputDto;
using Newtouch.Domain.DTO.OutputDto.Outpatient;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;
using Newtouch.Domain.IRepository.Inpatient;
using Newtouch.Domain.ValueObjects;
using Newtouch.Domain.ValueObjects.Outpatient;
using Newtouch.Domain.ViewModels;
using Newtouch.DomainServices.BGESBSercice;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using Newtouch.CIS.Compat;
using Newtouch.Repository;
using NewtouchHIS.Lib.Base.Extension;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace Newtouch.DomainServices
{
    /// <summary>
    /// Đơn thuốc
    /// </summary>
    public class MedicalRecordDmnService : DmnServiceBase, IMedicalRecordDmnService
    {
        private readonly IWMDiagnosisRepo _wmDiagnosisRepo;
        private readonly ITCMDiagnosisRepo _tcmDiagnosisRepo;
        private readonly IMedicalRecordRepo _medicalRecordRepo;
        private readonly IPrescriptionRepo _prescriptionRepo;
        private readonly IPrescriptionDetailRepo _prescriptionDetailRepo;
        private readonly ITreatmentRepo _treatmentRepo;
        private readonly ISysObjectActionRecordRepo _sysObjectActionRecordRepo;
        private readonly IBaseDataDmnService _baseDataDmnService;
        private readonly Domain.IDomainServices.IBaseDataDmnService _baseDataDmnService2;
        private readonly string yysz_esbBM = Newtouch.CIS.Compat.AppConfig.Get("yysz_esbBM");
        private readonly ITTCataloguesComparisonDmnService _tTCataloguesComparisonDmnService;
        private readonly IQhdZnshSqtxRepo _qhdznshsqtxRepo;
        private readonly ISysConfigRepo _sysConfigRepo;
        private readonly IMRecordTemplateDmnService _mRecordTemplateDmnService;
        private readonly IPresTemplateRepo _presTemplateEntityRepo;
        private readonly IPrescriptionDetailRepo _prescriptionDetailEntityRepo;
        private readonly IPresTemplateDmnService _presTemplateDmnService;
        
        
        public MedicalRecordDmnService() : base()
        {

        }

        /// <summary>
        /// lịch sửHồ sơcây
        /// </summary>
        /// <param name="blh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public List<MedicalRecordTreeVO> GetHistoryMedicalRecordTree(string blh, int queryDate, string orgId)
        {
            var sb = new StringBuilder();
            sb.Append(@"
SELECT bl.jzId,jzzt,
1 AS lsblly,    --lịch sửbệnh nhânđếnnguồn    --Hệ thống này
    (CASE jzzt WHEN 2 THEN (SELECT convert(varchar(8), ghsj, 112)) + '(Đang khám)' 
	WHEN 3 THEN (SELECT convert(varchar(8), ghsj, 112)) + '(Đã kết thúc)'
	ELSE (SELECT convert(varchar(8), ghsj, 112)) + ghksmc END)AS blmc
,jz.ghksmc
,jz.jzysmc
FROM xt_bl(nolock) bl
LEFT JOIN xt_jz(nolock) jz on bl.organizeId=jz.organizeId
and jz.jzId=bl.jzId
WHERE bl.blh=@blh and bl.organizeId=@orgId
AND jz.zt='1' AND bl.zt='1'");

            if (queryDate > 0)   //gần1 thángcủaDữ liệu
            {
                sb.Append(@" AND DateDiff(mm,ghsj,getdate())<=@queryDate");
            }
            sb.Append(@" order by ghczsj desc");

            var list = this.FindList<MedicalRecordTreeVO>(sb.ToString(), new[] { new SqlParameter("@blh", blh), new SqlParameter("@queryDate", queryDate), new SqlParameter("@orgId", orgId) });
            return list;
        }
        /// <summary>
        /// Chi tiếtcâytiếtđiểmnội dung （TheojzIdTra cứuHồ sơvàĐơn thuốcnội dung）
        /// </summary>
        /// <param name="jzId"></param>
        /// <returns></returns>
       public NodeContentDto SelectNodeContent(string jzId)
        {
            //Bệnh nhânKhám bệnhthông tin
            var jzEntity = _treatmentRepo.IQueryable().Where(a => a.jzId == jzId && a.zt == "1").FirstOrDefault();
            if (jzEntity == null)
            { 
                throw new FailedException("Dữ liệu bất thường，chưaTra cứuraBệnh nhânKhám bệnhthông tin");
            }
            //Hồ sơ
            var blEntity = _medicalRecordRepo.IQueryable().Where(a => a.jzId == jzId && a.zt == "1").FirstOrDefault();
            if (blEntity == null)
            {
                throw new FailedException("Dữ liệu bất thường，chưaTra cứuraHồ sơthông tin");
            }
            //Chẩn đoán Tây y  //chínhChẩn đoánxếpởtrướcmặt
            var xysql = @" SELECT a.zdCode,a.zdmc,a.zdlx,a.ysbz, b.icd10,a.zdbz FROM Newtouch_CIS.dbo.xt_xyzd a 
  LEFT JOIN [NewtouchHIS_Base].[dbo].[V_S_xt_zd] b ON a.zdCode=b.zdCode AND a.zdmc=b.zdmc AND (a.OrganizeId=b.OrganizeId OR b.OrganizeId='*')  AND a.zt='1' AND b.zt='1'
  WHERE a.jzId=@jzId  ORDER BY a.zdlx ";
            var xyzdList = this.FindList<WMDiagnosisHtmlVO>(xysql, new[] { new System.Data.SqlClient.SqlParameter("@jzId", jzId) });
            var zysql = @" SELECT a.zdCode,a.zdmc,a.zdlx,a.ysbz,a.zhCode,a.zhmc, b.icd10,a.zdbz FROM Newtouch_CIS.dbo.xt_zyzd a 
  LEFT JOIN [NewtouchHIS_Base].[dbo].[V_S_xt_zd] b ON a.zdCode=b.zdCode AND a.zdmc=b.zdmc AND (a.OrganizeId=b.OrganizeId OR b.OrganizeId='*')  AND a.zt='1' AND b.zt='1'
  WHERE a.jzId=@jzId   ORDER BY a.zdlx ";
            var zyzdList = this.FindList<TCMDiagnosisHtmlVO>(zysql, new[] { new System.Data.SqlClient.SqlParameter("@jzId", jzId) });

            //Đơn thuốc
            var presEntityList = _prescriptionRepo.IQueryable().Where(a => a.jzId == jzId && a.zt == "1").OrderBy(p => p.sfbz).ThenByDescending(p => p.cfh).ToList();
            List<NodeContentPresBO> boList = new List<NodeContentPresBO>();
            //Lấyđiện tửĐơn thuốc Đơn thuốcchínhbảng
            var dzcfsql = @"select * from Newtouch_CIS..xt_dzcf where jzid=@jzId and zt='1' order by cfh";
            var dzcflist = this.FindList<DzcfEntity>(dzcfsql, new[] { new System.Data.SqlClient.SqlParameter("@jzId", jzId) });

            PrescriptionEntity dzcflistend = new PrescriptionEntity();
            if (dzcflist.Count > 0)
            {
                foreach (var item in dzcflist)
                {
                    PrescriptionEntity entity = new PrescriptionEntity();
                    entity.cfId = item.cfId;
                    entity.cfh = item.cfh;
                    entity.cflx = item.cflx;
                    entity.cfzt = item.cfzt;
                    entity.zje = item.zje;
                    entity.cftag = item.cftag;
                    entity.cfyf = item.cfyf;
                    entity.CreateTime = item.CreateTime;
                    entity.CreatorCode = item.CreatorCode;
                    entity.ys = item.ys;
                    entity.ks = item.ks;
                    entity.OrganizeId = item.OrganizeId;
                    entity.tbbz = item.SyncStatus == "1" ? true : false;
                    presEntityList.Add(entity);
                }
            }

            foreach (var item in presEntityList)
            {
                NodeContentPresBO bo = new NodeContentPresBO()
                {
                    presEntity = item,   //Đơn thuốc
                    presDetailList = new List<PrescriptionDetailQueryVO> { }
                };

                //Đơn thuốcchi tiết

                var sql = "";
                if (item.cflx == (int)EnumCflx.RehabPres)   //Phục hồi chức năngĐơn thuốc
                {
                    sql = @"
SELECT cf.cfId, cfmx.xmCode, cfmx.xmCode, cfmx.xmmc, cfmx.mczll,cfmx.pcCode, cfmx.sl, cfmx.dw, 
        cf.tbz,isnull(cfmx.mczll,1)/isnull(sfxm.dwjls,1)*isnull(cfmx.sl,1) AS zl,   --theo mới nhấtdwjls，Tính ra tổng lượng
        isnull(cfmx.mczll,1)/isnull(sfxm.dwjls,1)*isnull(cfmx.sl,1)*isnull(sfxm.dj,0) je,   --theo mới nhấtdwjlsvàdj，Tính ra tổng số
        ";
                }
                else if (item.cflx == (int)EnumCflx.RegularItemPres|| item.cflx == (int)EnumCflx.Yyhc)   //Hạng mục thông thườngĐơn thuốc
                {
                    sql = @"
SELECT cf.cfId, cfmx.xmCode, cfmx.xmCode, cfmx.xmmc, cfmx.mczll,cfmx.pcCode, cfmx.sl, cfmx.dw, cfmx.ztId,cfmx.ztmc , cfmx.ztsl ,
        cf.tbz,isnull(cfmx.mczll,1)/isnull(sfxm.dwjls,1)*isnull(cfmx.sl,1) AS zl,   --theo mới nhấtdwjls，Tính ra tổng lượng
        isnull(cfmx.mczll,1)/isnull(sfxm.dwjls,1)*isnull(cfmx.sl,1)*isnull(sfxm.dj,0) je,   --theo mới nhấtdwjlsvàdj，Tính ra tổng số
        ";
                }
                if (item.cflx == (int)EnumCflx.InspectionPres || item.cflx == (int)EnumCflx.ExaminationPres)   //Xét nghiệm、Kiểm traĐơn thuốc
                {
                    sql = @"
SELECT cf.cfId, cfmx.xmCode, cfmx.xmCode, cfmx.xmmc, cfmx.mczll,cfmx.pcCode, cfmx.sl, cfmx.dw,
cf.tbz,CONVERT(DECIMAL(9,2),(isnull(cfmx.sl,1)*isnull(sfxm.dj,0))) je,cfmx.ztId,cfmx.ztmc,   --theo mới nhấtdj，Tính ra tổng số
        ";
                }
                if (item.cflx == (int)EnumCflx.RehabPres || item.cflx == (int)EnumCflx.RegularItemPres || item.cflx == (int)EnumCflx.Yyhc || item.cflx == (int)EnumCflx.InspectionPres || item.cflx == (int)EnumCflx.ExaminationPres)
                {
                    sql += @"
        isnull(sfxm.dj,0) dj,
        sfxm.dw,
        sfxm.dwjls,
        sfxm.jjcl,
        pc.yzpcmc AS pcmc,
        cf.cfh AS cfh,
        cfmx.urgent,
        cfmx.bw,
cfmx.purpose,
cfmx.Remark,   --Dặn dò
cfmx.zxks,
Department.Name AS zxksmc,
cfmx.zxsj,
cfmx.ybwym,  --Bảo hiểm y tếduy nhấtmã
cfmx.xzsybz,
cfmx.cfmxId,
cfmx.ts,
cf.tbz,
cfmx.zzfbz,cfmx.ztmbId
FROM xt_cfmx cfmx
INNER JOIN xt_cf(nolock) cf
    ON cf.cfId=cfmx.cfId
        AND cf.OrganizeId=cfmx.OrganizeId
LEFT JOIN [NewtouchHIS_Base]..V_S_xt_yzpc(nolock) pc
    ON pc.yzpcCode=cfmx.pcCode
        AND pc.OrganizeId=cfmx.OrganizeId
LEFT JOIN [NewtouchHIS_Base]..V_S_xt_sfxm(nolock) sfxm 
    ON sfxm.sfxmCode=cfmx.xmCode
        AND sfxm.OrganizeId=cfmx.OrganizeId
LEFT JOIN NewtouchHIS_Base..V_S_Sys_Department Department
    ON Department.Code=cfmx.zxks
        AND Department.OrganizeId= cfmx.OrganizeId
WHERE cfmx.zt='1' AND cf.zt='1'
        AND cfmx.cfId=@cfId order by cfmx.px
                    ";
                }
                if (item.cflx == (int)EnumCflx.WMPres || item.cflx == (int)EnumCflx.TCMPres) //ThuốcĐơn thuốc
                {
                    sql = @"
SELECT cf.cfId, cfmx.ypCode,cfmx.ypmc,cfmx.mcjl,cfmx.mcjldw
,cfmx.mcjldw AS mcjldwwwwwww,sfdl.dlCode sfdlCode,sfdl.dlmc sfdlmc
,cfmx.yfCode,cfmx.pcCode,(case when cfmx.sfzt=1 then cfmx.ztsl else cfmx.sl end) sl, 
        cfmx.zh,cf.tieshu,cf.cfyf,cf.djbz,
         case isnull(cfyp.dj,'1') when '1' then isnull(CONVERT(DECIMAL(9,4),(isnull(cfmx.sl,0)*(yp.lsj / yp.bzs * yp.mzcls))),0) else isnull(cfmx.sl,0)*isnull(cfyp.dj,0) end AS je,  --theo mới nhấtdjtính raje
        isnull(CONVERT(DECIMAL(9,4),cfyp.dj),isnull(CONVERT(DECIMAL(9,4),yp.lsj / yp.bzs * yp.mzcls,0),0)) AS dj,  --mới nhấtdj
        pc.yzpcmc AS pcmc,
        isnull(yp.ypgg,cfyp.specName) ypgg ,
        yp.mzcls AS cls,
        yf.yfmc,
        --mởcủa、Tính tiền nhất định phải dùng đơn vị ngoại trú cfmx.dw và yp.mzcldwgiống nhau
        --Thuốc Đông y？
        cfmx.dw,
        isnull(yp.jldw,cfyp.minPrepunt) AS redundant_jldw,
        cf.cfh AS cfh,
cfmx.zxks,  --Khoa thực hiện（lĩnhthuốcNhà thuốcmã）
cfmx.Remark,    --Dặn dò
cfmx.ybwym,
cfmx.xzsybz,
cfmx.cfmxId,
cfmx.ts,
isnull(case cfyp.listtype when '102' then '43' when '101' then '21' else null end ,yp.jx)  jxCode,
cfmx.ds,
cfmx.zl,
cfmx.xmmc,
cf.tbz,
cfmx.zzfbz,cfmx.ispsbz,cfmx.islgbz,cfmx.ztId,cfmx.ztmc,cfmx.sfzt,cfmx.gjybdm ,yp.jl jlzhxs,pc.zxcs
FROM xt_cfmx cfmx
INNER JOIN xt_cf(nolock) cf
    ON cf.cfId=cfmx.cfId
        AND cf.OrganizeId=cfmx.OrganizeId
LEFT JOIN [NewtouchHIS_Base]..V_S_xt_yzpc(nolock) pc
    ON pc.yzpcCode=cfmx.pcCode
        AND pc.OrganizeId=cfmx.OrganizeId
LEFT JOIN [NewtouchHIS_Base]..V_C_xt_yp(nolock) yp
    ON yp.ypCode=cfmx.ypCode
        AND yp.OrganizeId=cfmx.OrganizeId
LEFT JOIN  [NewtouchHIS_Sett]..Dzcf_CFYP_output cfyp on cfmx.ypcode=cast(cfyp.cfypcode as varchar) and cf.isdzcf='1' --điện tửĐơn thuốcDanh mục
LEFT JOIN [NewtouchHIS_Base]..V_S_xt_ypyf(nolock) yf
    ON yf.yfCode=cfmx.yfCode
left join [NewtouchHIS_Base]..V_S_xt_sfdl(nolock) sfdl 
on sfdl.dlCode=yp.dlCode
AND sfdl.OrganizeId=cfmx.OrganizeId 
WHERE cfmx.zt='1' AND cf.zt='1'
        AND cfmx.cfId=@cfId order by cfmx.px
                    ";
                }
                if (item.cflx == (int)EnumCflx.Dzcf)
                {
                    sql = @"SELECT cf.cfId, cfmx.ypCode,cfmx.ypmc,cfmx.mcjl,cfmx.mcjldw
,cfmx.mcjldw AS mcjldwwwwwww,sfdl.dlCode sfdlCode,sfdl.dlmc sfdlmc
,cfmx.yfCode,cfmx.pcCode,(case when cfmx.sfzt=1 then cfmx.ztsl else cfmx.sl end) sl, 
        cfmx.zh,cf.tieshu,cf.cfyf,cf.djbz,
        isnull(CONVERT(DECIMAL(9,4),(isnull(cfmx.sl,0)*(yp.lsj / yp.bzs * yp.mzcls))),0) AS je,  --theo mới nhấtdjtính raje
        isnull(CONVERT(DECIMAL(9,4),yp.lsj / yp.bzs * yp.mzcls,0),0) AS dj,  --mới nhấtdj
        pc.yzpcmc AS pcmc,
        yp.ypgg,
        yp.mzcls AS cls,
        yf.yfmc,
        --mởcủa、Tính tiền nhất định phải dùng đơn vị ngoại trú cfmx.dw và yp.mzcldwgiống nhau
        --Thuốc Đông y？
        cfmx.dw,
        yp.jldw AS redundant_jldw,
cf.cfh AS cfh,
cfmx.zxks,  --Khoa thực hiện（lĩnhthuốcNhà thuốcmã）
cfmx.Remark,    --Dặn dò
cfmx.ybwym,
cfmx.xzsybz,
cfmx.cfmxId,
cfmx.ts,
yp.jx jxCode,
cfmx.ds,
cfmx.zl,
cfmx.xmmc,
cf.tbz,
cfmx.zzfbz,cfmx.ispsbz,cfmx.islgbz,cfmx.ztId,cfmx.ztmc,cfmx.sfzt ,yp.jl jlzhxs,pc.zxcs
FROM Newtouch_CIS..xt_dzcfmx cfmx
INNER JOIN Newtouch_CIS..xt_dzcf(nolock) cf
    ON cf.cfId=cfmx.cfId
        AND cf.OrganizeId=cfmx.OrganizeId
LEFT JOIN [NewtouchHIS_Base]..V_S_xt_yzpc(nolock) pc
    ON pc.yzpcCode=cfmx.pcCode
        AND pc.OrganizeId=cfmx.OrganizeId
LEFT JOIN [NewtouchHIS_Base]..V_C_xt_yp(nolock) yp
    ON yp.ypCode=cfmx.ypCode
        AND yp.OrganizeId=cfmx.OrganizeId
LEFT JOIN [NewtouchHIS_Base]..V_S_xt_ypyf(nolock) yf
    ON yf.yfCode=cfmx.yfCode
left join [NewtouchHIS_Base]..V_S_xt_sfdl(nolock) sfdl 
on sfdl.dlCode=yp.dlCode
AND sfdl.OrganizeId=cfmx.OrganizeId 
WHERE cfmx.zt='1' AND cf.zt='1'
        AND cfmx.cfId=@cfId order by cfmx.px";
                }
                var presDetailEntityList = this.FindList<PrescriptionDetailQueryVO>(sql, new[] { new SqlParameter("@cfId", item.cfId) });

                List<PrescriptionDetailEntity> presEntityDetailList = new List<PrescriptionDetailEntity>();
                foreach (var cfmx in presDetailEntityList)
                {
                    bo.presDetailList.Add(cfmx);
                }

                var czjlList = _sysObjectActionRecordRepo.GetListByKey(bo.presEntity.cfh, bo.presEntity.OrganizeId);
                bo.cfzhdysj = czjlList.Where(p => p.actionType == "print").Select(p => p.zhczsj).FirstOrDefault();
                bo.zldzhdysj = czjlList.Where(p => p.actionType == "printkfzld").Select(p => p.zhczsj).FirstOrDefault();
                if (item.cflx == (int)EnumCflx.Dzcf)
                {
                    bo.sendtohisResult = bo.presEntity.tbbz == true ? "1" : "";
                }
                else
                {
                    bo.sendtohisResult = czjlList.Where(p => p.actionType == "sendtohis").Select(p => p.result).FirstOrDefault();
                }

                boList.Add(bo);
            }
            var lastzcfDto = GetLastzcf(jzEntity.blh, jzEntity.OrganizeId);
            var brxx = _baseDataDmnService2.SelectXtBrjbxx(jzEntity.blh, jzEntity.mzh, jzEntity.OrganizeId);
            var brhf = "";
            if (brxx != null && brxx.Count > 0)
            {
                brhf = brxx.FirstOrDefault().hf.ToString();
            }
            var contentDto = new NodeContentDto()
            {
                //Bệnh nhânKhám bệnhthông tin
                jzks = jzEntity.jzks,
                mjzbz = jzEntity.mjzbz,
                jzysmc = jzEntity.jzysmc,
                jzys = jzEntity.jzys,
                xb = jzEntity.xb,
                nlshow = jzEntity.nlshow,
                xm = jzEntity.xm,
                mzh = jzEntity.mzh,
                ghys = jzEntity.ghys,
                zlkssj = jzEntity.zlkssj,
                ghksmc = jzEntity.ghksmc,
                tizhong = jzEntity.tizhong,
                tiwen = jzEntity.tiwen,
                maibo = jzEntity.maibo,
                //xueya = jzEntity.xueya,
                shengao = jzEntity.shengao,
                shousuoya = jzEntity.shousuoya,
                shuzhangya = jzEntity.shuzhangya,
                xuetang = jzEntity.xuetang,
                xuetangclfs = jzEntity.xuetangclfs,//zhaoyule tăng thêmđường huyếtphương thức đo
                huxi = jzEntity.huxi,
                cfzbz = jzEntity.cfzbz,
                hy = jzEntity.hy == null ? brhf : jzEntity.hy,
                hymc = (jzEntity.hy == null ? brhf : jzEntity.hy) == "1" ? "Chưa kết hôn" :((jzEntity.hy == null ? brhf : jzEntity.hy) == "2" ? "Đã kết hôn" : ((jzEntity.hy == null ? brhf : jzEntity.hy) == "3" ? "Khong ro" : "")),
                //Hồ sơ
                zs = blEntity.zs,
                fbsj = blEntity.fbsj,
                xbs = blEntity.xbs,
                jws = blEntity.jws,
                yjs = blEntity.yjs,
                gms = blEntity.gms,
                //hy  = brhf,
                ct = blEntity.ct,
                clfa = blEntity.clfa,
                fzjc = blEntity.fzjc,
                //Chẩn đoán Tây y
                xyzdList = xyzdList,
                //Chẩn đoán YHCT
                zyzdList = zyzdList,

                //Đơn thuốc
                cfBoList = boList,
                lastzcfDto = lastzcfDto,
                sbbh = jzEntity.sbbh
            };

            return contentDto;
        }

        public NodeContentDto SelectNodeContentByBlmb(string mbId,string orgId)
        {
            List<NodeContentPresBO> boList = new List<NodeContentPresBO>();
            //Hồ sơmẫuthông tin
            var blmb = _mRecordTemplateDmnService.SelectTemplateDetailByMbId(mbId,orgId);
            //Tra cứumẫudướiĐơn thuốcBộ mẫu
            foreach (var cfTemplateGroupPackageEntity in blmb.mbztList)
            {
                var sqlllll = "";
                if (cfTemplateGroupPackageEntity.cflx == (int)EnumCflx.InspectionPres || cfTemplateGroupPackageEntity.cflx == (int)EnumCflx.ExaminationPres)
                {
                    
                    sqlllll = @"
                           select ztId mbId,OrganizeId,1 mblx,NULL kscode,case when type='1' then '4' else '5' end cflx,ztmc mbmc,NULL ysgh,CreateTime,
                            CreatorCode,LastModifyTime,LastModifierCode,zt from [dbo].[jyjc_zt]  mb
                        WHERE mb.ztId=@mbId
                                AND mb.OrganizeId=@orgId
                                                ";
                }
                else {
                    //Đơn thuốcmẫu
                    sqlllll = @"
                           SELECT *
                        FROM xt_cfmb mb
                        WHERE mb.mbId=@mbId
                                AND mb.OrganizeId=@orgId
                                                ";
                }
               
                var presTemplateEntity = this.FirstOrDefault<InspectionTemplateVO>(sqlllll, new[] { new SqlParameter("@mbId", cfTemplateGroupPackageEntity.mbId), new SqlParameter("@orgId", orgId) });
                    
                PrescriptionEntity presEntity = new PrescriptionEntity()
                {
                    cfId = presTemplateEntity.mbId,
                    OrganizeId = orgId,
                    cfh = presTemplateEntity.mbId,
                    cflx = cfTemplateGroupPackageEntity.cflx,
                };
               
                List<PrescriptionDetailQueryVO> presDetailEntityList = new List<PrescriptionDetailQueryVO>();
                var mbDetailEntityList =
                    _presTemplateDmnService.SelectPresDetailByMbId(cfTemplateGroupPackageEntity.mbId, orgId, cfTemplateGroupPackageEntity.cflx.ToString()).mbmxList;
                foreach (var mbDetailEntity in mbDetailEntityList)
                {
                    PrescriptionDetailQueryVO cfmx = new PrescriptionDetailQueryVO()
                    {
                        cfmxId = mbDetailEntity.mxId,
                        ztId = mbDetailEntity.mbId,
                        ztmc = mbDetailEntity.ztmc,
                        OrganizeId = mbDetailEntity.OrganizeId,
                        xmmc = mbDetailEntity.xmmc,
                        ypCode = mbDetailEntity.ypCode,
                        ypmc = mbDetailEntity.ypmc,
                        mczll = mbDetailEntity.mczll,
                        mcjl = mbDetailEntity.mcjl,
                        mcjldw = mbDetailEntity.mcjldw,
                        pcCode = mbDetailEntity.pcCode,
                        dw = mbDetailEntity.dw,
                        sl = mbDetailEntity.sl,
                        zl = mbDetailEntity.zl,
                        px = mbDetailEntity.px,
                        zh = mbDetailEntity.zh,
                        bw = mbDetailEntity.bw,
                        Remark = mbDetailEntity.Remark,
                        zxks = mbDetailEntity.zxks,
                        zxksmc = mbDetailEntity.zxksmc,
                        yfCode = mbDetailEntity.yfCode,
                        yfmc = mbDetailEntity.yfmc,
                        jxCode = mbDetailEntity.jxCode,
                        dj = mbDetailEntity.dj,
                        ypgg = mbDetailEntity.ypgg,
                        xmCode = mbDetailEntity.xmCode,
                        dwjls = mbDetailEntity.dwjls,
                        pcmc = mbDetailEntity.pcmc,
                        
                    };
                    presDetailEntityList.Add(cfmx);
                }
                boList.Add(new NodeContentPresBO()
                {
                    presEntity = presEntity,
                    presDetailList = presDetailEntityList
                });
            }
            var contentDto = new NodeContentDto()
            {
                zs = blmb.zs,
                xbs = blmb.xbs,
                jws = blmb.jws,
                yjs = blmb.yjs,
                gms = blmb.gms,
                hy = blmb.hy,
                ct = blmb.ct,
                clfa = blmb.clfa,
                xyzdList = blmb.xyzdList,
                zyzdList = blmb.zyzdList,
                cfBoList = boList
            };
            return contentDto;
        }
        /// <summary>
        /// LưuKhám bệnhghi chép、Chẩn đoán、Hồ sơ、Đơn thuốc、Đơn thuốcchi tiết
        /// </summary>
        /// <param name="jzObject"></param>
        /// <param name="xyzdList"></param>
        /// <param name="zyzdList"></param>
        /// <param name="blObject"></param>
        /// <param name="cfDto"></param>
        /// <param name="operatorCode"></param>
        /// <param name="addedYpCfList"></param>
        /// <param name="updatedYpCfList"></param>
        public void SaveMedicalRecord(TreatmentEntity jzObject
            , List<WMDiagnosisHtmlVO> xyzdList
            , List<TCMDiagnosisHtmlVO> zyzdList
            , MedicalRecordEntity blObject
            , List<PrescriptionDTO> cfDto
            , List<CFZDDiagnosisHtmlVO> cfzdlist
            , string operatorCode
            , out List<string> addedYpCfList, out List<string> updatedYpCfList)
        {
            var stopwatch = new Stopwatch();//  Bắt đầugiám sátxemMãvận hànhdòngThời gian
            stopwatch.Start();
            try
            {
                addedYpCfList = new List<string>();
                updatedYpCfList = new List<string>();
                using (var db = new EFDbTransaction().BeginTrans())
                {
                    //liên kếtKhám bệnhHồ sơ
                    MedicalRecordEntity blEntity = null;
                    //liên kếtKhám bệnhHồ sơ
                    if (string.IsNullOrEmpty(jzObject.mjzbz.ToString()) || jzObject.mjzbz == 0)
                    {
                        jzObject.mjzbz = 1;
                    }

                    //2.Khám bệnh、Hồ sơbảng，trướcXóacũDữ liệulạiThêm mới
                    if (!string.IsNullOrWhiteSpace(jzObject.jzId))
                    {
                        const string sql1 = @"SELECT TOP 1 * FROM dbo.xt_jz(NOLOCK) WHERE jzId=@jzId ";
                        var dbJzEntity = db.FirstOrDefault<TreatmentEntity>(sql1, new DbParameter[] { new SqlParameter("@jzId", jzObject.jzId) });
                        dbJzEntity.tizhong = jzObject.tizhong;
                        dbJzEntity.tiwen = jzObject.tiwen;
                        dbJzEntity.maibo = jzObject.maibo;
                        //dbJzEntity.xueya = jzObject.xueya;
                        dbJzEntity.shengao = jzObject.shengao;
                        dbJzEntity.shousuoya = jzObject.shousuoya;
                        dbJzEntity.shuzhangya = jzObject.shuzhangya;
                        dbJzEntity.xuetangclfs = jzObject.xuetangclfs; //zhaoyule tăng thêmđường huyếtphương thức đo
                        dbJzEntity.xuetang = jzObject.xuetang;
                        dbJzEntity.zljssj = jzObject.zljssj;
                        dbJzEntity.jzks = jzObject.jzks;
                        dbJzEntity.jzys = jzObject.jzys;
                        dbJzEntity.jzzt = jzObject.jzzt;
                        dbJzEntity.jzysmc = jzObject.jzysmc;
                        dbJzEntity.kh = jzObject.kh;
                        dbJzEntity.ContactNum = jzObject.ContactNum;
                        dbJzEntity.nlshow = jzObject.nlshow;
                        dbJzEntity.huxi = jzObject.huxi;
                        dbJzEntity.hy = jzObject.hy;
                        dbJzEntity.cfzbz = jzObject.cfzbz;
                        //1.2 SửaKhám bệnhbảng
                        dbJzEntity.Modify();
                        db.Update(dbJzEntity);
                        //XóaChẩn đoán Tây y
                        db.Delete<WMDiagnosisEntity>(a => a.jzId == jzObject.jzId);
                        //XóaChẩn đoán YHCT
                        db.Delete<TCMDiagnosisEntity>(a => a.jzId == jzObject.jzId);

                        //trarađến Khám bệnhHồ sơ
                        const string sql2 = @"SELECT TOP 1 * FROM dbo.xt_bl(NOLOCK) WHERE jzId=@jzId";
                        blEntity = db.FirstOrDefault<MedicalRecordEntity>(sql2, new DbParameter[] { new SqlParameter("@jzId", jzObject.jzId) });
                    }
                    else
                    {
                        const string sql3 = @"SELECT TOP 1 * FROM dbo.xt_jz(NOLOCK) WHERE mzh=@mzh AND OrganizeId=@OrganizeId ";
                        var param3 = new DbParameter[]
                        {
                                new SqlParameter("@mzh", jzObject.mzh),
                                new SqlParameter("@OrganizeId", jzObject.OrganizeId)
                        };
                        var jzxx = db.FindList<TreatmentEntity>(sql3, param3);
                        if (jzxx != null && jzxx.Count > 0)
                        {
                            //throw new FailedException("mộtSố khám ngoại trúchỉtương ứngmộtKhám bệnhghi chép");
                            var dbJzEntity = jzxx.First();
                            dbJzEntity.tizhong = jzObject.tizhong;
                            dbJzEntity.tiwen = jzObject.tiwen;
                            dbJzEntity.maibo = jzObject.maibo;
                            //dbJzEntity.xueya = jzObject.xueya;
                            dbJzEntity.shengao = jzObject.shengao;
                            dbJzEntity.shousuoya = jzObject.shousuoya;
                            dbJzEntity.shuzhangya = jzObject.shuzhangya;
                            dbJzEntity.xuetangclfs = jzObject.xuetangclfs; //zhaoyule tăng thêmđường huyếtphương thức đo
                            dbJzEntity.xuetang = jzObject.xuetang;
                            dbJzEntity.zljssj = jzObject.zljssj;
                            dbJzEntity.jzks = jzObject.jzks;
                            dbJzEntity.jzys = jzObject.jzys;
                            dbJzEntity.jzzt = jzObject.jzzt;
                            dbJzEntity.jzysmc = jzObject.jzysmc;
                            dbJzEntity.kh = jzObject.kh;
                            dbJzEntity.ContactNum = jzObject.ContactNum;
                            dbJzEntity.nlshow = jzObject.nlshow;
                            dbJzEntity.huxi = jzObject.huxi;
                            dbJzEntity.hy = jzObject.hy;
                            dbJzEntity.cfzbz = jzObject.cfzbz;
                            //1.2 SửaKhám bệnhbảng
                            dbJzEntity.Modify();
                            db.Update(dbJzEntity);
                            jzObject.jzId = dbJzEntity.jzId;
                            //XóaChẩn đoán Tây y
                            db.Delete<WMDiagnosisEntity>(a => a.jzId == jzObject.jzId);
                            //XóaChẩn đoán YHCT
                            db.Delete<TCMDiagnosisEntity>(a => a.jzId == jzObject.jzId);

                            //trarađến Khám bệnhHồ sơ
                            const string sql2 = @"SELECT TOP 1 * FROM dbo.xt_bl(NOLOCK) WHERE jzId=@jzId";
                            blEntity = db.FirstOrDefault<MedicalRecordEntity>(sql2, new DbParameter[] { new SqlParameter("@jzId", jzObject.jzId) });

                        }
                        else
                        {
                            //1.1 Thêm mớiKhám bệnhbảng
                            jzObject.jzId = Guid.NewGuid().ToString();
                            if (jzObject.zlkssj < new DateTime(1970, 01, 01))
                            {
                                jzObject.zlkssj = DateTime.Now; //khámtrịThời gian bắt đầu    //phòngdừngtrangmặttrênkhôngtruyềnquađến
                            }

                            jzObject.Create();
                            db.Insert(jzObject);
                        }
                    }

                    //insert Chẩn đoán
                    if (xyzdList != null)
                    {
                        foreach (var diagnosisEntity in from item in xyzdList
                                                        where !string.IsNullOrWhiteSpace(item.zdmc) && !string.IsNullOrWhiteSpace(item.zdCode)
                                                        select new WMDiagnosisEntity
                                                        {
                                                            xyzdId = Guid.NewGuid().ToString(),
                                                            OrganizeId = jzObject.OrganizeId,
                                                            jzId = jzObject.jzId,
                                                            zdlx = item.zdlx,
                                                            zdCode = item.zdCode,
                                                            zdmc = item.zdmc,
                                                            ysbz = item.ysbz,
                                                            zdbz = item.zdbz
                                                        })
                        {
                            diagnosisEntity.Create();
                            db.Insert(diagnosisEntity);
                        }
                    }

                    if (zyzdList != null)
                    {
                        foreach (var item in zyzdList)
                        {
                            if (string.IsNullOrWhiteSpace(item.zdmc) || string.IsNullOrWhiteSpace(item.zdCode))
                            {
                                continue;
                            }

                            var diagnosisEntity = new TCMDiagnosisEntity
                            {
                                zyzdId = Guid.NewGuid().ToString(),
                                OrganizeId = jzObject.OrganizeId,
                                jzId = jzObject.jzId,
                                zdlx = item.zdlx,
                                zdCode = item.zdCode,
                                zdmc = item.zdmc,
                                ysbz = item.ysbz,
                                zhCode = item.zhCode,
                                zhmc = item.zhmc,
                                zdbz = item.zdbz
                            };
                            diagnosisEntity.Create();
                            db.Insert(diagnosisEntity);
                        }
                    }

                    //3.Hồ sơbảng
                    if (blEntity != null)
                    {
                        blEntity.zs = blObject.zs; //chínhbảo
                        blEntity.fbsj = blObject.fbsj; //phátbệnhThời gian
                        blEntity.xbs = blObject.xbs; //hiệnbệnhsử
                        blEntity.jws = blObject.jws; //Tiền sử
                        blEntity.yjs = blObject.yjs; //Thángtiền sử bệnh
                        blEntity.gms = blObject.gms; //quaTiền sử dị ứng
                        blEntity.hy = blObject.hy; //quaTiền sử dị ứng
                        blEntity.ct = blObject.ct; //trathể
                        blEntity.clfa = blObject.clfa; //chỗlýphươngán
                        blEntity.fzjc = blObject.fzjc; //hỗ trợKiểm tra
                        blEntity.Modify();
                        db.Update(blEntity);
                    }
                    else
                    {
                        blEntity = blObject.Clone();
                        blEntity.blId = Guid.NewGuid().ToString();
                        blEntity.jzId = jzObject.jzId;
                        blEntity.blh = jzObject.blh;
                        blEntity.OrganizeId = jzObject.OrganizeId;
                        blEntity.Create();
                        db.Insert(blEntity);
                    }

                    blEntity.zs = blEntity.zs ?? "";

                    //4.Đơn thuốc
                    if (cfDto != null)
                    {
                        var ypyfList = _baseDataDmnService.GetMediUsageList();
                        PrescriptionEntity lastEty = null;
                        foreach (var cf in cfDto)
                        {
                            if (cf.cflx == 7)
                            {
                                if (cf.cfId != "")
                                {
                                    continue;
                                }
                                var cfEntity = new DzcfEntity
                                {
                                    cfId = Guid.NewGuid().ToString(),
                                    OrganizeId = jzObject.OrganizeId,
                                    jzId = jzObject.jzId,
                                    sfbz = cf.sfbz, //mộtđịnhlàfalse
                                    cflx = cf.cflx,
                                    cfh = cf.cfh,
                                    zje = cf.zje,
                                    ys = cf.ys,
                                    ks = cf.ks,
                                    CreateTime = DateTime.Now,
                                    CreatorCode = cf.ys,
                                    LastModifyTime = null,
                                    LastModifierCode = null,
                                    zt = "1",
                                    tieshu = cf.tieshu,
                                    cfyf = cf.cfyf,
                                    djbz = cf.djbz,
                                    lcyx = cf.lcyx,
                                    sqbz = cf.sqbz,
                                    cftag = cf.cftag,
                                    djfs = cf.djfs,
                                    djts = cf.djts,
                                    cfzt = cf.cfzt,
                                    tbbz = false,
                                    sqdh = null,
                                    SyncStatus = null,
                                    rxDrordDscr = null,
                                    valiDays = 3,
                                    reptFlag = null,
                                    maxReptCnt = 1,
                                    minInrvDays = 1,
                                    rxCotnFlag = null,
                                    longRxFlag = null,
                                    rxTraceCode = null,
                                    hiRxno = null,

                                };
                                db.Insert(cfEntity);
                                if (cfzdlist != null)
                                {
                                    int i = 1;
                                    foreach (var item in cfzdlist)
                                    {
                                        if (item.cfh == cf.cfh)
                                        {
                                            var cfzdEntity = new PrescripDiagnosisEntity
                                            {
                                                Id = Guid.NewGuid().ToString(),
                                                OrganizeId = jzObject.OrganizeId,
                                                cfh = cf.cfh,
                                                jzId = jzObject.jzId,
                                                mzh = jzObject.mzh,
                                                icd10 = item.zdCode,
                                                zdmc = item.zdmc,
                                                cftag = cf.cftag,
                                                yzpx = i.ToString(),
                                                yzlx = cf.cflx,
                                                zt = "1"
                                            };
                                            cfzdEntity.Create();
                                            db.Insert(cfzdEntity);
                                            i++;
                                        }
                                    }
                                }
                                if (cf.cfmxList == null) continue;
                                //Đơn thuốcchi tiết,Thêm mới
                                var rpDetail = new List<DzcfmxEntity>();
                                foreach (var cfmx in cf.cfmxList)
                                {
                                    var cfmxEntity = new DzcfmxEntity();
                                    cfmx.MapperTo(cfmxEntity);
                                    cfmxEntity.cfmxId = Guid.NewGuid().ToString();
                                    cfmxEntity.OrganizeId = cfEntity.OrganizeId;
                                    cfmxEntity.cfId = cfEntity.cfId;
                                    cfmxEntity.mczll = cfmxEntity.mczll ?? 1;
                                    cfmxEntity.Create();
                                    db.Insert(cfmxEntity);
                                    rpDetail.Add(cfmxEntity);
                                }
                            }
                            else
                            {
                                PrescriptionEntity dbPresEnitty = new PrescriptionEntity();
                                if (!string.IsNullOrWhiteSpace(cf.cfId))
                                {
                                    //kiểm trabảngbảngtrongĐơn thuốccủaThu phíTrạng thái
                                    const string sql4 = @"SELECT TOP 1 * FROM dbo.xt_cf(NOLOCK) WHERE cfId=@cfId";
                                    dbPresEnitty = db.FirstOrDefault<PrescriptionEntity>(sql4, new DbParameter[] { new SqlParameter("@cfId", cf.cfId) });
                                    if (dbPresEnitty == null)
                                    {
                                        throw new FailedException("Đơn thuốcNgoại lệ，Đơn thuốcdấuhiển thịcộtNgoại lệ");
                                    }

                                    if (dbPresEnitty.sfbz)
                                    {
                                        cf.sfbz = true;
                                        //throw new FailedException("Thao tácNgoại lệ，Đã thu phíĐơn thuốckhôngcho phépđổihơn");
                                    }
                                }
                                if (!cf.sfbz)
                                {
                                    if (string.IsNullOrWhiteSpace(cf.cfh))
                                    {
                                        throw new FailedException("Đơn thuốcNgoại lệ，thiếuSố đơn thuốc");
                                    }

                                    DateTime? cfklrq = null;
                                    string cfCreator = null;
                                    string sqdh = null;
                                    var needOutpatientBook = false;
                                    var needOutpatientBookModify = false;
                                    if (dbPresEnitty != null && !string.IsNullOrWhiteSpace(dbPresEnitty.cfId))
                                    {
                                        cfklrq = dbPresEnitty.CreateTime;
                                        cfCreator = dbPresEnitty.CreatorCode;
                                        sqdh = dbPresEnitty.sqdh;
                                        //bảngtrongđãtồn tạiởĐơn thuốc，trướcXóa chi tiếtvàXóachínhghi chép，lạiThêm mới
                                        // Chi tiết lặp lại khiến bảng chính của đơn thuốc bị Delete nhiều lần, tạm thời tránh, chờ tối ưu hóa
                                        if (lastEty == null || lastEty.cfId != dbPresEnitty.cfId)
                                        {
                                            db.Delete(dbPresEnitty);
                                            db.Delete<PrescriptionDetailEntity>(a => a.cfId == cf.cfId);
                                        }
                                        lastEty = dbPresEnitty;
                                        if (cf.cflx == (int)EnumCflx.WMPres || cf.cflx == (int)EnumCflx.TCMPres)
                                        {
                                            updatedYpCfList.Add(cf.cfh);
                                            needOutpatientBookModify = true;
                                        }
                                        if (cfzdlist != null && cfzdlist.Count > 0)
                                        {
                                            if (cfzdlist[0].cfh != null && cfzdlist[0].cfh != "")
                                            {
                                                if (cfzdlist[0].cfh == cf.cfh)
                                                {
                                                    const string sql5 = @"SELECT * FROM dbo.mz_clyzzd(NOLOCK) WHERE cfh=@cfh";
                                                    var dbcfzdnr = db.FirstOrDefault<PrescripDiagnosisEntity>(sql5, new DbParameter[] { new SqlParameter("@cfh", cfzdlist[0].cfh) });
                                                    if (dbcfzdnr != null)
                                                    {
                                                        db.Delete(dbcfzdnr);
                                                        db.Delete<PrescripDiagnosisEntity>(a => a.cfh == cf.cfh);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (cf.cflx == (int)EnumCflx.WMPres || cf.cflx == (int)EnumCflx.TCMPres)
                                        {
                                            addedYpCfList.Add(cf.cfh);
                                            needOutpatientBook = true;
                                        }
                                    }
                                    //Đơn thuốcchínhbảng,Thêm mới
                                    var cfEntity = new PrescriptionEntity
                                    {
                                        cfId = Guid.NewGuid().ToString(),
                                        OrganizeId = jzObject.OrganizeId,
                                        jzId = jzObject.jzId,
                                        sfbz = cf.sfbz, //mộtđịnhlàfalse
                                        cflx = cf.cflx,
                                        cfh = cf.cfh,
                                        zje = cf.zje,
                                        ys = cf.ys,
                                        ks = cf.ks,
                                        tieshu = cf.tieshu,
                                        cfyf = cf.cfyf,
                                        djbz = cf.djbz,
                                        lcyx = cf.lcyx,
                                        sqbz = cf.sqbz,
                                        cftag = cf.cftag,
                                        djfs = cf.djfs,
                                        djts = cf.djts,
                                        cfzt = cf.cfzt,
                                        isdzcf = cf.isdzcf
                                    };
                                    if (cfzdlist != null)
                                    {
                                        int i = 1;
                                        foreach (var item in cfzdlist)
                                        {
                                            if (item.cfh == cf.cfh)
                                            {
                                                var cfzdEntity = new PrescripDiagnosisEntity
                                                {
                                                    Id = Guid.NewGuid().ToString(),
                                                    OrganizeId = jzObject.OrganizeId,
                                                    cfh = cf.cfh,
                                                    jzId = jzObject.jzId,
                                                    mzh = jzObject.mzh,
                                                    icd10 = item.zdCode,
                                                    zdmc = item.zdmc,
                                                    cftag = cf.cftag,
                                                    yzpx = i.ToString(),
                                                    yzlx = cf.cflx,
                                                    zt = "1"
                                                };
                                                cfzdEntity.Create();
                                                db.Insert(cfzdEntity);
                                                i++;
                                            }
                                        }
                                    }
                                    if (cf.cflx == (int)EnumCflx.ExaminationPres ||
                                        cf.cflx == (int)EnumCflx.InspectionPres)
                                    {
                                        if (string.IsNullOrEmpty(sqdh))
                                        {
                                            sqdh = Guid.NewGuid().ToString();
                                        }

                                        cfEntity.sqdh = sqdh;
                                    }

                                    cfEntity.Create();
                                    if (cfklrq.HasValue)
                                    {
                                        cfEntity.CreateTime = cfklrq ?? cfEntity.CreateTime;
                                        cfEntity.CreatorCode = cfCreator;
                                        cfEntity.Modify();
                                    }

                                    db.Insert(cfEntity);

                                    if (cf.cfmxList == null) continue;

                                    //Đơn thuốcchi tiết,Thêm mới
                                    var rpDetail = new List<PrescriptionDetailEntity>();
                                    foreach (var cfmx in cf.cfmxList)
                                    {
                                        var cfmxEntity = new PrescriptionDetailEntity();
                                        cfmx.MapperTo(cfmxEntity);
                                        cfmxEntity.cfmxId = Guid.NewGuid().ToString();
                                        cfmxEntity.OrganizeId = cfEntity.OrganizeId;
                                        cfmxEntity.cfId = cfEntity.cfId;
                                        cfmxEntity.mczll = cfmxEntity.mczll ?? 1;
                                        cfmxEntity.Create();
                                        db.Insert(cfmxEntity);
                                        rpDetail.Add(cfmxEntity);
                                    }

                                    #region gửiĐơn thuốcđếnpds

                                    var t = "";
                                    if (cf.isdzcf != "1")//nếunếuđiđiện tửĐơn thuốc，khônggửiĐơn thuốcđếnNhà thuốc
                                    {
                                        if (needOutpatientBook)
                                        {
                                            t = SendNewRpToPds(jzObject, cf.cfh, cf.cfId, operatorCode, rpDetail, ypyfList);
                                        }
                                        else if (needOutpatientBookModify)
                                        {
                                            t = UpdateRpToPds(jzObject, cf.cfh, cf.cfId, operatorCode, rpDetail, ypyfList);
                                        }

                                        if (!string.IsNullOrWhiteSpace(t)) throw new FailedException("syncRpToPdsError", t);
                                    }
                                    
                                    #endregion
                                }
                            }

                        }
                    }
                    db.Commit();
                }
            }
            catch (Exception e)
            {
                LogCore.Error("SaveMedicalRecord error", e);
                throw;
            }
            finally
            {
                stopwatch.Stop(); //  Dừnggiám sátxem
                LogCore.Info("SaveMedicalRecord", string.Format("Thời gian chạy phương thức-{0}", stopwatch.Elapsed.TotalMilliseconds));
            }
        }

        /// <summary>
        /// Hủy bỏHồ sơ góigồmHồ sơChẩn đoán Đơn thuốc Đơn thuốcchi tiết
        /// </summary>
        public void ObsoleteMedicalRecord(string jzId, string orgId)
        {
            using (var db = new EFDbTransaction().BeginTrans())
            {
                //Khám bệnhghi chép
                var jzEntity = _treatmentRepo.IQueryable().Where(a => a.jzId == jzId && a.OrganizeId == orgId && a.zt == "1").FirstOrDefault();
                if (jzEntity == null)
                {
                    throw new FailedException("Dữ liệu bất thường，chưatrađếnKhám bệnhghi chép");
                }
                //jzEntity.zt = "0";
                //jzEntity.Modify();

                //db.Update(jzEntity);

                //Chẩn đoán
                var jzzdList = _wmDiagnosisRepo.IQueryable().Where(a => a.jzId == jzId && a.OrganizeId == orgId).ToList();
                foreach (var item in jzzdList)
                {
                    item.zt = "0";    //Hủy bỏ
                    item.Modify();

                    db.Update(item);
                }

                //Đơn thuốc
                var cfList = _prescriptionRepo.IQueryable().Where(a => a.jzId == jzId && a.OrganizeId == orgId).ToList();
                foreach (var item in cfList)
                {
                    item.zt = "0";    //Hủy bỏ
                    item.Modify();

                    db.Update(item);

                    //Đơn thuốcchi tiết
                    var cfmxList = _prescriptionDetailRepo.IQueryable().Where(a => a.cfId == jzId && a.OrganizeId == orgId).ToList();
                    foreach (var cfmx in cfmxList)
                    {
                        cfmx.zt = "0";    //Hủy bỏ
                        cfmx.Modify();

                        db.Update(cfmx);
                    }
                }

                //Hồ sơ
                var blList = _medicalRecordRepo.IQueryable().Where(a => a.jzId == jzId && a.OrganizeId == orgId).ToList();
                foreach (var item in blList)
                {
                    item.zt = "0";    //Hủy bỏ
                    item.Modify();

                    db.Update(item);
                }
                db.Commit();
            }
        }

        /// <summary>
        /// Hủy bỏhisMột lầnKhám bệnhcủatất cảĐơn thuốc
        /// </summary>
        public void ObsoleteAllPresToHIS(string jzId, string orgId)
        {
            //Hủy đơn thuốcgiao diện
            var list = _prescriptionRepo.IQueryable().Where(a => a.jzId == jzId && a.OrganizeId == orgId).ToList();
            List<ObsoletePresVO> zfcfList = new List<ObsoletePresVO>();
            foreach (var item in list)
            {
                ObsoletePresVO vo = new ObsoletePresVO();
                vo.cflx = item.cflx;
                vo.cfh = item.cfh;
            }

            //giao diệnnội dung
            var reqObj = new
            {
                orgId = orgId,
                OperateTime = DateTime.Now,
                cfList = zfcfList
            };

            var apiRespPush = SiteSettAPIHelper.Request<APIRequestHelper.DefaultResponse>(
                "/api/Prescription/Cancel", reqObj);
            if (apiRespPush.code != APIRequestHelper.ResponseResultCode.SUCCESS)
            {
                //ghingàychí
                AppLogger.Instance.Warn(string.Format("Đẩy giao diện hủy đơn thuốc thất bại，Khám bệnhId：" + jzId + "Thời gian đẩy" + DateTime.Now));
            }
        }

        /// <summary>
        /// gửiMột lầnKhám bệnhcủatất cảĐơn thuốc（nhưngkhôngđẩyĐã thu phícủa）、hoặcchỉđịnhĐơn thuốc
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        /// <param name="opr">Thao tácviên</param>
        /// <param name="cfId">chỉđịnhkhigửiphiếumộtĐơn thuốc，KhôngthìgửiMột lầnKhám bệnhcủatất cảĐơn thuốc（nhưngkhôngđẩyĐã thu phícủa）</param>
        public bool sendPresToHis(string mzh, string orgId, string opr, string cfId = null)
        {
            var pushResult = true;
            //1.chuẩnchuẩn bịDữ liệu
            var jzObjectList = _treatmentRepo.SelectDataByMzh(mzh, orgId);
            if (jzObjectList.Count != 1)
            {
                return false;
            }
            var jzObject = jzObjectList[0];
            //2Cập nhậtKhám bệnhTrạng thái,đẩyphiếumộtĐơn thuốckhikhôngCập nhậtKhám bệnhTrạng thái,đẩyphiếumộtĐơn thuốckhikhôngCập nhậtChẩn đoán
            if (string.IsNullOrWhiteSpace(cfId))
            {
                //Khám bệnhTrạng thái
                var reqObj1 = new
                {
                    outpatientNo = jzObject.mzh,
                    jiuzhenbz = jzObject.jzzt,
                    jzys = jzObject.jzys
                };
                //đẩyKhám bệnhTrạng thái
                var apiResp1 = SiteSettAPIHelper.Request<APIRequestHelper.DefaultResponse>("/api/Patient/OutPatientUpdateConsultationStatus", reqObj1);
                if (apiResp1.code != APIRequestHelper.ResponseResultCode.SUCCESS)
                {
                    AppLogger.Instance.Warn(string.Format("đẩyKhám bệnhTrạng tháithất bại，Số khám ngoại trú：" + jzObject.mzh + "Thời gian đẩy" + DateTime.Now));//ghingàychís
                }

                //2.2 đẩychínhChẩn đoán
                var diagnosisEntity = this.FirstOrDefault<SysDiagnosisVEntity>(@"
select xtzd.zdCode, xtzd.icd10, xtzd.zdmc 
from xt_xyzd(NOLOCK) xyzd
left join [NewtouchHIS_Base]..V_S_xt_zd xtzd on xtzd.zdCode = xyzd.zdCode and (xtzd.OrganizeId = '*' or xtzd.OrganizeId = xyzd.OrganizeId) and xtzd.zt = '1' and xtzd.zdlx = 'WM'
where xyzd.zdlx = 1 and jzId = @jzId and xyzd.zt = '1'
union
select xtzd.zdCode, xtzd.icd10, xtzd.zdmc 
from xt_zyzd(NOLOCK) zyzd
left join [NewtouchHIS_Base]..V_S_xt_zd xtzd on xtzd.zdCode = zyzd.zdCode and (xtzd.OrganizeId = '*' or xtzd.OrganizeId = zyzd.OrganizeId) and xtzd.zt = '1' and xtzd.zdlx = 'TCM'
where zyzd.zdlx = 1 and jzId = @jzId and zyzd.zt = '1'", new[] { new SqlParameter("@jzId", jzObject.jzId) });
                if (diagnosisEntity != null)
                {
                    var reqObj = new
                    {
                        outpatientNo = jzObject.mzh,
                        zdicd10 = diagnosisEntity.icd10,
                        zdmc = diagnosisEntity.zdmc,
                    };
                    //đẩyChẩn đoán
                    var apiResp = SiteSettAPIHelper.Request<APIRequestHelper.DefaultResponse>("/api/Patient/OutPatientUpdateDiagnosis", reqObj);
                    if (apiResp.code != APIRequestHelper.ResponseResultCode.SUCCESS)
                    {
                        AppLogger.Instance.Warn(string.Format("đẩyChẩn đoánthất bại，Số khám ngoại trú：" + jzObject.mzh + "Thời gian đẩy" + DateTime.Now));//ghingàychí
                    }
                }
            }

            //3.đẩyĐơn thuốc
            var dtoList = new List<PrescriptionAPIDto>();
            var presList = _prescriptionRepo.FindList(jzObject.jzId, false);
            if (!string.IsNullOrWhiteSpace(cfId))
            {
                presList = presList.Where(p => p.cfId == cfId).ToList();    //chỉ trả lạichỉđịnhĐơn thuốc
            }
            foreach (var cf in presList)
            {
                switch (cf.cflx)
                {
                    case (int)EnumCflx.RehabPres:
                        {
                            var dto = new PrescriptionAPIDto
                            {
                                cflx = 2,//1Thuốc 2Mục
                                cflxxf = cf.cflx,
                                cflxmc = ((EnumCflx)cf.cflx).GetDescription(),
                                cfh = cf.cfh,
                                ys = cf.ys,
                                ks = cf.ks
                            };

                            var cfmxList = _prescriptionDetailRepo.IQueryable(p => p.cfId == cf.cfId && p.zt == "1").ToList();
                            var list = cfmxList.Select(cfmx => new TreamentItemVO { 
                                sfxm = cfmx.xmCode,
                                dczll = cfmx.mczll,
                                zxcs = Convert.ToInt32(cfmx.sl),
                                dw = cfmx.dw,
                                zzfbz = cfmx.zzfbz,
                                ztId = cfmx.ztId,
                                ztmc = cfmx.ztmc,
                                ztsl = cfmx.ztsl,
                                sl = Convert.ToDecimal(cfmx.zl)
                            }).Where(p => p.sl > 0).ToList();
                            dto.AddTreamentItems = list;
                            dtoList.Add(dto);
                            break;
                        }
                    case (int)EnumCflx.RegularItemPres:
                    case (int)EnumCflx.InspectionPres:
                    case (int)EnumCflx.ExaminationPres:
                    case (int)EnumCflx.Yyhc:
                        {
                            var dto = new PrescriptionAPIDto
                            {
                                cflx = 2, //1Thuốc 2Mục
                                cflxxf = cf.cflx,
                                cflxmc = ((EnumCflx)cf.cflx).GetDescription(),
                                cfh = cf.cfh,
                                ys = cf.ys,
                                ks = cf.ks
                            };
                            var cfmxList = _prescriptionDetailRepo.SelectData(cf.cfId, false);
                            //var list1 = cfmxList.Select(cfmx => new TreamentItemVO { sfxm = cfmx.xmCode, sl = cfmx.sl, dw = cfmx.dw,zzfbz=cfmx.zzfbz }).ToList();
                            //2021năm7Tháng21ngày nhaucùngMụcSố lượnghợpvà
                            var list = cfmxList.GroupBy(cfmx => new { cfmx.xmCode, cfmx.sl, cfmx.dw, cfmx.zzfbz, cfmx.ztId, cfmx.ztmc, cfmx.ztsl }).Select(t => new TreamentItemVO { sfxm = t.Key.xmCode, sl = t.Sum(p => p.sl), dw = t.Key.dw, zzfbz = t.Key.zzfbz, ztId = t.Key.ztId, ztmc = t.Key.ztmc, ztsl = t.Key.ztsl }).Where(p => p.sl > 0).ToList();
                            dto.AddTreamentItems = list;
                            dtoList.Add(dto);
                            break;
                        }
                    case (int)EnumCflx.WMPres:
                    case (int)EnumCflx.TCMPres:
                        {
                            if (cf.isdzcf != null && cf.isdzcf == "1")
                            {
                                break;
                            }
                            if (cf.djbz ?? false)
                            {
                                var dto1 = new PrescriptionAPIDto
                                {
                                    cflx = 2, //1Thuốc 2Mục
                                    cflxxf = (int)EnumCflx.RegularItemPres,
                                    cflxmc = ((EnumCflx.RegularItemPres)).GetDescription(),
                                    cfh = cf.cfh,
                                    ys = cf.ys,
                                    ks = cf.ks
                                };
                                var cfmxList1 = _prescriptionDetailRepo.SelectDataDjxm(cf.cfId);
                                if (cfmxList1 != null)
                                {
                                    //2021năm7Tháng21ngày nhaucùngMụcSố lượnghợpvà
                                    var list1 = cfmxList1.GroupBy(cfmx => new { cfmx.xmCode, cfmx.sl, cfmx.dw, cfmx.zzfbz }).Select(t => new TreamentItemVO { sfxm = t.Key.xmCode, sl = t.Sum(p => p.sl), dw = t.Key.dw, zzfbz = t.Key.zzfbz }).Where(p => p.sl > 0).ToList();
                                    dto1.AddTreamentItems = list1;
                                    dtoList.Add(dto1);
                                }
                            }
                            var dto = new PrescriptionAPIDto
                            {
                                cflx = 1, //1Thuốc 2Mục
                                cflxxf = cf.cflx,
                                cflxmc = ((EnumCflx)cf.cflx).GetDescription(),
                                cfh = cf.cfh,
                                ts = cf.tieshu,
                                djbz = cf.djbz,
                                ys = cf.ys,
                                ks = cf.ks
                            };
                            var cfmxList = _prescriptionDetailRepo.SelectData(cf.cfId, cf.djbz ?? false);
                            var zxksList = cfmxList.Select(p => p.zxks).Distinct().ToList();
                            if (zxksList.Count == 1)
                            {
                                if (!string.IsNullOrWhiteSpace(zxksList[0]))
                                {
                                    dto.lyyf = zxksList[0];
                                }
                            }

                            var list = cfmxList.Select(cfmx => new MedicineItemVO
                            {
                                yp = cfmx.ypCode,
                                sl = cfmx.sl,
                                dw = cfmx.dw,
                                jl = cfmx.mcjl,
                                jldw = cfmx.mcjldw,
                                zh = cfmx.zh,
                                yfCode = cfmx.yfCode,
                                zzfbz = cfmx.zzfbz,
                                pcCode = cfmx.pcCode
                            }).Where(a => a.yp != "" && a.yp != null).ToList();
                            dto.AddMedicineItems = list;

                            var list2 = cfmxList.GroupBy(cfmx => new { cfmx.xmCode, cfmx.sl, cfmx.dw, cfmx.zzfbz, cfmx.ztId, cfmx.ztmc, cfmx.ztsl }).Select(t => new TreamentItemVO { sfxm = t.Key.xmCode, sl = t.Sum(p => p.sl), dw = t.Key.dw, zzfbz = t.Key.zzfbz, ztId = t.Key.ztId, ztmc = t.Key.ztmc, ztsl = t.Key.ztsl }).Where(a => a.sfxm != "" && a.sfxm != null).ToList();
                            dto.AddTreamentItems = list2;
                            dtoList.Add(dto);
                            break;
                        }
                }
            }

            //giao diệnnội dung
            var reqPush = new
            {
                mzh = jzObject.mzh,
                ys = jzObject.jzys, //cóhỏiđề   //có thểlấyBỏđã
                OperateTime = DateTime.Now,
                PrescriptionList = dtoList,
                AheadCancelCf = !string.IsNullOrWhiteSpace(cfId) && presList.Count == 1 ? presList[0].cfh : "",  //làKhông trướcHủytất cảcủaChưa thu phíĐơn thuốc
            };

            var apiRespPush = SiteSettAPIHelper.Request<APIRequestHelper.DefaultResponse<PresUpdateResultDTO>>("/api/Prescription/UpdateAllForOutNo", reqPush);
            if (apiRespPush.code != APIRequestHelper.ResponseResultCode.SUCCESS)
            {
                pushResult = false;
                AppLogger.Instance.Warn(string.Format("đẩyĐơn thuốcthất bại，Số khám ngoại trú：" + reqPush.mzh + "Đơn thuốcId：" + cfId + "Thời gian đẩy" + DateTime.Now));//ghingàychí
            }
            else
            {
                foreach (var pres in presList)
                {
                    bool? sfbz = null;
                    if (apiRespPush.data.settledCfhList != null && apiRespPush.data.settledCfhList.Contains(pres.cfh))
                    {
                        sfbz = true;   // Cập nhật lịch sử bị bỏ sót
                    }
                    _prescriptionRepo.UpdateChargeStatus(pres.cfh, true, sfbz, orgId, opr);//hơnĐơn thuốc mớiđẩyTrạng thái
                }
            }

            //hơnĐơn thuốc mớiđẩyTrạng thái
            foreach (var actionEntity in presList.Select(pres => new SysObjectActionRecordEntity
            {
                OrganizeId = pres.OrganizeId,
                objectType = "cf",
                objectKey = pres.cfh,
                actionType = "sendtohis",
                result = apiRespPush.code == APIRequestHelper.ResponseResultCode.SUCCESS ? "1" : "0"
            }))
            {
                actionEntity.Create(true);
                _sysObjectActionRecordRepo.Insert(actionEntity);
            }

            return pushResult;
        }

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
        public string UpdateRpToPds(TreatmentEntity jzObject,
            string cfh,
            string cfId,
            string operatorCode,
            List<PrescriptionDetailEntity> rpDetail,
            IList<SysMedicineUsageVEntity> ypyfList)
        {
            if (jzObject == null || rpDetail == null || rpDetail.Count == 0) return "Thông tin khám chữa bệnh và chi tiết đơn thuốc không được để trống";
            if (string.IsNullOrWhiteSpace(cfh)) return "Số đơn thuốc không được để trống";

            var result = "";
            var apiRespPush = new APIRequestHelper.DefaultResponse();
            try
            {
                var items = rpDetail.Where(c => c.sfzt == null && (c.ztId ==""  ||c.ztId==null) && c.xmCode == null).Select(p => new
                {
                    Yfbm = p.zxks,
                    Cfh = cfh,
                    ItemCode = p.ypCode,
                    ItemCount = p.sl,
                    Zhyz = 0,
                    ItemName = p.ypmc,
                    ItemSpecifications = (string)null,  //Quy cách thuốc
                    ItemUnitPrice = p.dj,
                    ItemUnitName = p.dw,
                    ItemManufacturer = (string)null,    //Mục/ThuốcsinhNhà sản xuấtTên
                    DlCode = (string)null,  //Nhóm thu phí lớn
                    Dosage = p.mcjl,
                    DosageUnit = p.mcjldw,
                    UsageName = ypyfList.Where(a => a.yfCode == p.yfCode).Select(a => a.yfmc).FirstOrDefault(),   //Tên cách dùng
                    CardNo = jzObject.kh,
                    xm = jzObject.xm,
                    nl = jzObject.csny.HasValue ? (int?)((DateTime.Now - jzObject.csny.Value).TotalDays / 365.25) : null,
                    brxzmc = jzObject.brxzmc,
                    ysmc = jzObject.jzysmc,
                    ksmc = jzObject.ghksmc,
                    Remark = (string)null,  //Ghi chú
                    GroupNum = p.zh,
                }).ToList();
                //Sửa
                var reqPush = new
                {
                    Items = items,
                    CreatorCode = operatorCode,
                    OrganizeId = jzObject.OrganizeId,
                    TimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                };
                apiRespPush = SiteYfykAPIHelper.Request<APIRequestHelper.DefaultResponse>("api/ResourcesOperate/OutpatientBookModify", reqPush, autoAppendToken: false);
                result = apiRespPush.code == APIRequestHelper.ResponseResultCode.SUCCESS ? "" : apiRespPush.sub_msg;
            }
            catch (Exception e)
            {
                LogCore.Error("SaveMedicalRecord UpdateRpToPds error", e);
                result = e.Message + (e.InnerException == null ? "" : e.InnerException.Message);
            }
            ThreadPool.QueueUserWorkItem(_ =>
            {
                Thread.CurrentThread.IsBackground = false;
                var actionEntity = new SysObjectActionRecordEntity
                {
                    OrganizeId = jzObject.OrganizeId,
                    objectType = "cf",
                    objectKey = cfh,
                    actionType = "sendtopds.modify",
                    result = apiRespPush.code == APIRequestHelper.ResponseResultCode.SUCCESS ? "1" : "0",
                    Id = Guid.NewGuid().ToString(),
                    CreatorCode = operatorCode,
                    CreateTime = DateTime.Now,
                    zt = "1"
                };
                new SysObjectActionRecordRepo().Insert(actionEntity);
            });
            return result;
        }

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
        public string SendNewRpToPds(TreatmentEntity jzObject,
            string cfh,
            string cfId,
            string operatorCode,
            List<PrescriptionDetailEntity> rpDetail,
            IList<SysMedicineUsageVEntity> ypyfList)
        {
            if (jzObject == null || rpDetail == null || rpDetail.Count == 0) return "Thông tin khám chữa bệnh và chi tiết đơn thuốc không được để trống";
            if (string.IsNullOrWhiteSpace(cfh)) return "Số đơn thuốc không được để trống";

            var result = "";
            var apiRespPush = new APIRequestHelper.DefaultResponse();
            try
            {
                var items = rpDetail.Where(c => c.xmCode == null).Select(p => new
                {
                    Yfbm = p.zxks,
                    Cfh = cfh,
                    ItemCode = p.ypCode,
                    ItemCount = p.sl,
                    Zhyz = 0,
                    ItemName = p.ypmc,
                    ItemSpecifications = (string)null,  //Quy cách thuốc
                    ItemUnitPrice = p.dj,
                    ItemUnitName = p.dw,
                    ItemManufacturer = (string)null,    //Mục/ThuốcsinhNhà sản xuấtTên
                    DlCode = (string)null,  //Nhóm thu phí lớn
                    Dosage = p.mcjl,
                    DosageUnit = p.mcjldw,
                    UsageName = ypyfList.Where(a => a.yfCode == p.yfCode).Select(a => a.yfmc).FirstOrDefault(),   //Tên cách dùng
                    CardNo = jzObject.kh,
                    xm = jzObject.xm,
                    xb = jzObject.xb,
                    nl = jzObject.csny.HasValue ? (int?)((DateTime.Now - jzObject.csny.Value).TotalDays / 365.25) : null,
                    brxzmc = jzObject.brxzmc,
                    ysmc = jzObject.jzysmc,
                    ksmc = jzObject.ghksmc,
                    Remark = (string)null,  //Ghi chú
                    GroupNum = p.zh,
                }).ToList();
                //Thêm mới
                var reqPush = new
                {
                    Items = items,
                    CreatorCode = operatorCode,
                    OrganizeId = jzObject.OrganizeId,
                    TimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                };
                apiRespPush = SiteYfykAPIHelper.Request<APIRequestHelper.DefaultResponse>("api/ResourcesOperate/OutpatientBook", reqPush, autoAppendToken: false);
                result = apiRespPush.code == APIRequestHelper.ResponseResultCode.SUCCESS ? "" : apiRespPush.sub_msg;
            }
            catch (Exception e)
            {
                LogCore.Error("SaveMedicalRecord UpdateRpToPds error", e);
                result = e.Message + (e.InnerException == null ? "" : e.InnerException.Message);
            }
            ThreadPool.QueueUserWorkItem(_ =>
            {
                Thread.CurrentThread.IsBackground = false;
                var actionEntity = new SysObjectActionRecordEntity
                {
                    OrganizeId = jzObject.OrganizeId,
                    objectType = "cf",
                    objectKey = cfh,
                    actionType = "sendtopds",
                    result = apiRespPush.code == APIRequestHelper.ResponseResultCode.SUCCESS ? "1" : "0",
                    Id = Guid.NewGuid().ToString(),
                    CreatorCode = operatorCode,
                    CreateTime = DateTime.Now,
                    zt = "1"
                };
                new SysObjectActionRecordRepo().Insert(actionEntity);
            });
            return result;
        }

        /// <summary>
        /// đẩyĐơn thuốcđếnNhà thuốcKho thuốc
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="operatorCode"></param>
        /// <param name="mzh"></param>
        /// <param name="cfId"></param>
        /// <param name="addedYpCfList"></param>
        /// <param name="updatedYpCfList"></param>
        /// <returns></returns>
        public bool sendPresToPDS(string orgId, string operatorCode, string mzh, string cfId = null, List<string> addedYpCfList = null, List<string> updatedYpCfList = null)
        {
            var pushResult = true;
            //1.chuẩnchuẩn bịDữ liệu
            var jzObjectList = _treatmentRepo.IQueryable(p => p.mzh == mzh && p.zt == "1").ToList();
            if (jzObjectList.Count != 1)
            {
                return false;
            }
            var jzObject = jzObjectList[0];

            //2.đẩyĐơn thuốc
            var dtoList = new List<PrescriptionAPIDto>();
            var presList = _prescriptionRepo.IQueryable(p => p.jzId == jzObject.jzId && p.sfbz == false && p.zt == "1").ToList();
            if (!string.IsNullOrWhiteSpace(cfId))
            {
                presList = presList.Where(p => p.cfId == cfId).ToList();    //chỉ trả lạichỉđịnhĐơn thuốc
            }
            else
            {
                presList = presList.Where(p => addedYpCfList.Contains(p.cfh) || updatedYpCfList.Contains(p.cfh)).ToList();
            }
            if (presList.Count == 0)
            {
                return false;
            }
            var cfIdList = presList.Select(p => p.cfId).ToList();
            var cfmxList = _prescriptionDetailRepo.IQueryable(p => cfIdList.Contains(p.cfId)).ToList();

            ////giao diệnnội dung
            //if (!string.IsNullOrWhiteSpace(token))
            //{
            var ypyfList = _baseDataDmnService.GetMediUsageList();

            foreach (var cf in presList)
            {
                Task.Run(() =>
                {
                    var items = cfmxList.Where(p => p.cfId == cf.cfId).ToList().Select(p => new
                    {
                        Yfbm = p.zxks,
                        Cfh = cf.cfh,
                        ItemCode = p.ypCode,
                        ItemCount = p.sl,
                        Zhyz = 0,
                        ItemName = p.ypmc,
                        ItemSpecifications = (string)null,  //Quy cách thuốc
                        ItemUnitPrice = p.dj,
                        ItemUnitName = p.dw,
                        ItemManufacturer = (string)null,    //Mục/ThuốcsinhNhà sản xuấtTên
                        DlCode = (string)null,  //Nhóm thu phí lớn
                        Dosage = p.mcjl,
                        DosageUnit = p.mcjldw,
                        UsageName = ypyfList.Where(a => a.yfCode == p.yfCode).Select(a => a.yfmc).FirstOrDefault(),   //Tên cách dùng
                        CardNo = jzObject.kh,
                        xm = jzObject.xm,
                        nl = jzObject.csny.HasValue ? (int?)((DateTime.Now - jzObject.csny.Value).TotalDays / 365.25) : null,
                        brxzmc = jzObject.brxzmc,
                        ysmc = jzObject.jzysmc,
                        ksmc = jzObject.ghksmc,
                        Remark = (string)null,  //Ghi chú
                        GroupNum = p.zh,
                    }).ToList();
                    if (updatedYpCfList.Contains(cf.cfh))
                    {
                        //Sửa
                        var reqPush = new
                        {
                            Items = items,
                            CreatorCode = operatorCode,
                            OrganizeId = orgId,
                            TimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                        };
                        var apiRespPush = SiteYfykAPIHelper.Request<APIRequestHelper.DefaultResponse>(
                                     "api/ResourcesOperate/OutpatientBookModify", reqPush, autoAppendToken: false);
                        var actionEntity = new SysObjectActionRecordEntity() { };
                        actionEntity.OrganizeId = cf.OrganizeId;
                        actionEntity.objectType = "cf";
                        actionEntity.objectKey = cf.cfh;
                        actionEntity.actionType = "sendtopds.modify";
                        actionEntity.result = apiRespPush.code == APIRequestHelper.ResponseResultCode.SUCCESS ? "1" : "0";
                        actionEntity.Id = Guid.NewGuid().ToString();
                        actionEntity.CreatorCode = operatorCode;
                        actionEntity.CreateTime = DateTime.Now;
                        actionEntity.zt = "1";
                        new SysObjectActionRecordRepo().Insert(actionEntity);
                    }
                    else
                    {
                        //Thêm mới
                        var reqPush = new
                        {
                            Items = items,
                            CreatorCode = operatorCode,
                            OrganizeId = orgId,
                            TimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                        };
                        var apiRespPush = SiteYfykAPIHelper.Request<APIRequestHelper.DefaultResponse>(
                                     "api/ResourcesOperate/OutpatientBook", reqPush, autoAppendToken: false);
                        var actionEntity = new SysObjectActionRecordEntity() { };
                        actionEntity.OrganizeId = cf.OrganizeId;
                        actionEntity.objectType = "cf";
                        actionEntity.objectKey = cf.cfh;
                        actionEntity.actionType = "sendtopds";
                        actionEntity.result = apiRespPush.code == APIRequestHelper.ResponseResultCode.SUCCESS ? "1" : "0";
                        actionEntity.Id = Guid.NewGuid().ToString();
                        actionEntity.CreatorCode = operatorCode;
                        actionEntity.CreateTime = DateTime.Now;
                        actionEntity.zt = "1";
                        new SysObjectActionRecordRepo().Insert(actionEntity);
                    }
                });
                //}
            }

            return pushResult;
        }

        /// <summary>
        /// Hủy đơn thuốc（vàđẩychoHIS）
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="mzh"></param>
        /// <param name="cfId"></param>
        public void cancelSinglePres(string orgId, string mzh, string cfId
            , out int apicflx, out string apicfh, out bool isAlertinherit)
        {
            apicflx = 0;
            apicfh = "";
            isAlertinherit = false;
            using (var db = new EFDbTransaction().BeginTrans())
            {
                //Đơn thuốc
                var cf = _prescriptionRepo.IQueryable().Where(a => a.cfId == cfId).FirstOrDefault();
                if (cf == null)
                {
                    throw new FailedException("định ký tựĐơn thuốcthất bại");
                }
                if (cf.sfbz == true)
                {
                    if (cf.tbz == true)
                    {
                        isAlertinherit = true;
                    }
                    else
                    {
                        throw new FailedException("Đơn thuốcĐã thu phí，khôngcho phéphơnsửa");
                    }

                }
                cf.zt = "0";    //Hủy bỏ
                cf.Modify();
                db.Update(cf);

                //Đơn thuốcchi tiết
                var cfmxList = _prescriptionDetailRepo.IQueryable().Where(a => a.cfId == cfId).ToList();
                foreach (var cfmx in cfmxList)
                {
                    cfmx.zt = "0";    //Hủy bỏ
                    cfmx.Modify();

                    db.Update(cfmx);
                }

                db.Commit();

                if (cf.cflx == (int)EnumCflx.RehabPres || cf.cflx == (int)EnumCflx.RegularItemPres ||cf.cflx == (int)EnumCflx.Yyhc || cf.cflx == (int)EnumCflx.InspectionPres || cf.cflx == (int)EnumCflx.ExaminationPres)
                {
                    apicflx = 2;    //1Thuốc 2Mục
                }
                else
                {
                    apicflx = 1;
                }
                apicfh = cf.cfh;
            }
        }

        public void delDzcf(string orgId, string mzh, string cfId
          , out int apicflx, out string apicfh, out bool isAlertinherit)
        {
            apicflx = 0;
            apicfh = "";
            isAlertinherit = false;
            string sql = "select * from Newtouch_CIS..xt_dzcf where cfId=@cfId  and SyncStatus!='1' ";
            var cflist = this.FindList<DzcfEntity>(sql, new[] { new SqlParameter("@cfId", cfId) }).FirstOrDefault();
            if (cflist != null)
            {
                apicflx = 1;
                apicfh = cflist.cfh;
                isAlertinherit = false;
                // 7-fix: tham so hoa cfId (truoc day string.Format noi truc tiep
                // vao 2 cau update). Luu y: SELECT kiem tra o tren van doc
                // [Newtouch_CIS] cung (read leak, khong phai write leak).
                string cfsqls = @"update " + DbNames.Name("CisDb", "Newtouch_CIS") + @"..xt_dzcf set zt='0' where cfid=@cfId and zt='1';  update " + DbNames.Name("CisDb", "Newtouch_CIS") + "..xt_dzcfmx set zt='0' where cfid=@cfId and zt='1' ";
                this.ExecuteSqlCommand(cfsqls, new[] { new SqlParameter("@cfId", cfId) });
            }
            else
            {
                throw new FailedException("Bảo hiểm y tếđiện tửĐơn thuốcĐã tải lên,trướcHủyTải lênởvàodòngHủy bỏ");
            }

        }

        /// <summary>
        /// Hủy bỏHISphiếutờ
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="mzh"></param>
        /// <param name="cfId"></param>
        public void cancelSinglePresToHIS(string orgId, string mzh, string cfId
            , int apicflx, string apicfh)
        {
            if (apicflx == 0 || string.IsNullOrWhiteSpace(apicfh))
            {
                return; //khôngđẩy
            }

            //Hủy đơn thuốcgiao diện
            List<ObsoletePresVO> zfcfList = new List<ObsoletePresVO>();
            zfcfList.Add(new ObsoletePresVO()
            {
                cflx = apicflx,
                cfh = apicfh,
            });

            //giao diệnnội dung
            var reqObj = new
            {
                orgId = orgId,
                OperateTime = DateTime.Now,
                cfList = zfcfList
            };

            var apiRespPush = SiteSettAPIHelper.Request<APIRequestHelper.DefaultResponse>(
                "/api/Prescription/Cancel", reqObj);
            if (apiRespPush.code != APIRequestHelper.ResponseResultCode.SUCCESS)
            {
                //ghingàychí
                AppLogger.Instance.Warn(string.Format("Đẩy giao diện hủy đơn thuốc thất bại，chId：" + cfId + "Thời gian đẩy" + DateTime.Now));
            }
        }

        /// <summary>
        /// Hủy bỏPDSphiếutờ
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="mzh"></param>
        /// <param name="cfId"></param>
        public void cancelSinglePresToPDS(string orgId, string operatorCode, string mzh, string cfId
            , int apicflx, string apicfh)
        {
            var pres = _prescriptionRepo.IQueryable(p => p.cfId == cfId).FirstOrDefault();
            if (pres == null)
            {
                return;
            }
            var isyp = (pres.cflx == (int)EnumCflx.WMPres) || (pres.cflx == (int)EnumCflx.TCMPres);
            if (!isyp)
            {
                return;
            }
            //giao diệnnội dung
            var reqObj = new
            {
                cfh = apicfh,
                //fyyf = 
                OrganizeId = orgId,
                CreatorCode = operatorCode,
            };

            var apiRespPush = SiteYfykAPIHelper.Request<APIRequestHelper.DefaultResponse>(
                "/api/ResourcesOperate/OutpatientCancelAllBook", reqObj);
            if (apiRespPush.code != APIRequestHelper.ResponseResultCode.SUCCESS)
            {
                //ghingàychí
                AppLogger.Instance.Warn(string.Format("đẩyPDSHủy đơn thuốcgiao diệnthất bại，chId：" + cfId + "Thời gian đẩy" + DateTime.Now));
            }
        }

        public List<PrescriptionHtmlVO> GetzfcfJson(string cfId)
        {
            var sb = new StringBuilder();
            sb.Append(@" SELECT b.cfId,b.sfbz,b.cflx,b.cfh,b.zje,b.ys,b.tieshu,b.cfyf,b.djbz,a.xmCode,a.xmmc,a.ypCode,a.ypmc,a.mczll,a.mcjl,a.mcjldw,a.yfCode,a.pcCode,a.zl,a.sl,a.dw,a.dj,a.je,a.zh,a.bw,a.urgent,a.purpose,a.Remark,a.zxks,a.zxsj,a.ybwym,a.xzsybz
  FROM [Newtouch_CIS].[dbo].[xt_cfmx] a 
  LEFT JOIN [Newtouch_CIS].[dbo].[xt_cf] b ON a.cfId=b.cfId AND a.OrganizeId=b.OrganizeId
  WHERE a.zt='1' AND b.zt='1' AND a.cfId=@cfId ");
            var list = this.FindList<PrescriptionHtmlVO>(sb.ToString(), new[] { new SqlParameter("@cfId", cfId) });
            return list;
        }
        /// <summary>
        /// LấyThuốchoặcMụccủathông tin
        /// </summary>
        /// <param name="code"></param>
        /// <param name="cxlx">1biểu thịMục 2biểu thịThuốc</param>
        /// <returns></returns>
        public SqtxXmYpInfoVO GetXmYpInfo(string code, string cxlx, string orgId)
        {
            var sb = new StringBuilder();
            if (cxlx == "1")
            {
                sb.Append(@" SELECT ybdm,CASE WHEN zfbl=1 THEN 1 ELSE 0 END zfbl,1.0 jl,dw jldw,1.0 mzcls,sfdlCode,dj  FROM NewtouchHIS_Base.dbo.V_S_xt_sfxm WHERE sfxmCode = @code AND OrganizeId = @orgId AND zt = '1'");
            }
            else
            {
                sb.Append(@"   SELECT b.ybdm,CASE WHEN a.zfbl=1 THEN 1 ELSE 0 END zfbl,jl,jldw,mzcls,dlCode sfdlCode,a.lsj dj  FROM NewtouchHIS_Base.[dbo].[V_S_xt_yp] a
  LEFT JOIN NewtouchHIS_Base.[dbo].[V_S_xt_ypsx] b ON a.ypCode=b.ypCode AND a.OrganizeId=b.OrganizeId
   WHERE  a.zt='1' AND b.zt='1' and  a.ypCode = @code AND a.OrganizeId = @orgId ");
            }
            var sqtxXmYpInfoVO = this.FirstOrDefault<SqtxXmYpInfoVO>(sb.ToString(), new[] { new SqlParameter("@code", code), new SqlParameter("@cxlx", cxlx), new SqlParameter("@orgId", orgId) });
            return sqtxXmYpInfoVO;
        }
        public string GetYpjx(string code, string orgId)
        {
            var sb = new StringBuilder();
            sb.Append(@" SELECT t1.jxmc  FROM NewtouchHIS_Base.dbo.xt_ypjx t1 inner join  NewtouchHIS_Base.dbo.xt_yp t2 on t1.jxCode = t2.jx WHERE t2.ypCode = @code AND t2.OrganizeId = @orgId AND t2.zt = '1'");
            
           var ypJxmc = this.FirstOrDefault<string>(sb.ToString(), new[] { new SqlParameter("@code", code), new SqlParameter("@orgId", orgId) });
            return  ypJxmc;
        }

        #region Ngoại trúngàychíTra cứu
        public IList<OutpatientLogQueryVO> GetLogGridList(Pagination pagination, string keyword, string kssj, string jssj, string jzysgh, string orgId, string hznl, string xtxy, string qbdc)
        {
            var sb = new StringBuilder();
            sb.Append(@"SELECT a.jzId,
       a.mzh,
       a.xm,
       a.xb,
       d.zjh sfzh,
       --CAST(FLOOR(DATEDIFF(DY, a.csny, GETDATE()) / 365.25) AS INT) age,
        a.nlshow,
       d.dh ContactNum,
       a.ghksmc,
       ISNULL((
           SELECT STUFF(
                  (
                      SELECT ';' + zdmc + (CASE ysbz
                                               WHEN 1 THEN
                                                   '??'
                                               ELSE
                                                   ''
                                           END
                                          )
                      FROM xt_xyzd xyzd
                          RIGHT JOIN xt_jz jz
                              ON xyzd.jzId = jz.jzId
                                 AND xyzd.OrganizeId = jz.OrganizeId
                                 AND xyzd.zt = '1'
                      WHERE jz.mzh = a.mzh
                            AND jz.zt = '1'
                            AND jz.OrganizeId = a.OrganizeId
                      ORDER BY zdlx ASC
                      FOR XML PATH('')
                  ),
                  1,
                  1,
                  ''
                       ) AS xyzdmc
       ),'')  +' '+
       ISNULL((
           SELECT STUFF(
                  (
                      SELECT ';' + zdmc + (CASE ysbz
                                               WHEN 1 THEN
                                                   '??'
                                               ELSE
                                                   ''
                                           END
                                          ),
                             (CASE ISNULL(zhmc, '')
                                  WHEN '' THEN
                                      ''
                                  ELSE
                                      '*【' + zhmc + '】'
                              END
                             )
                      FROM xt_zyzd zyzd
                          RIGHT JOIN xt_jz jz
                              ON zyzd.jzId = jz.jzId
                                 AND zyzd.OrganizeId = jz.OrganizeId
                                 AND zyzd.zt = '1'
                      WHERE jz.mzh = a.mzh
                            AND jz.zt = '1'
                            AND jz.OrganizeId = a.OrganizeId
                      ORDER BY zdlx ASC
                      FOR XML PATH('')
                  ),
                  1,
                  1,
                  ''
                       ) AS zyzdmc
       ),'') AS zdmc,
       b.zs,
       --a.xueya,
       CONVERT(VARCHAR(10),CONVERT(FLOAT,a.shousuoya))+'/'+CONVERT(VARCHAR(10),CONVERT(FLOAT,a.shuzhangya)) xueya,
       ISNULL(e.Name,'') +CONVERT(VARCHAR(10),CONVERT(FLOAT,a.xuetang)) xuetang,
xuetangclfs,
CONVERT(VARCHAR(10),CONVERT(FLOAT,a.xuetang)) dqxuetang,
       a.tiwen,
       d.zy,
       d.xian_dz dz,
       convert(varchar(10),b.fbsj, 120) fbsj,
       SUBSTRING(CONVERT(VARCHAR(20),b.CreateTime,120) ,1,10) jzsj,
       b.xbs,
        b.clfa,
        b.fzjc,
	    a.createtime,
       CASE
           WHEN
          a.cfzbz='1' THEN
               'Tái khám'
           ELSE
               'Khám lần đầu'
       END cfz,a.jzysmc jzys,
case when FLOOR(DATEDIFF(DY, d.csny, GETDATE()) / 365.25)<15  then d.jjllr else null end jzxm,
case when FLOOR(DATEDIFF(DY, d.csny, GETDATE()) / 365.25)<15 then d.jjlldh else null end dhhm
FROM dbo.xt_jz a
    INNER JOIN dbo.xt_bl b
        ON a.jzId = b.jzId
           AND a.OrganizeId = b.OrganizeId
    LEFT JOIN NewtouchHIS_Sett..mz_gh c
        ON a.mzh = c.mzh
           AND a.OrganizeId = c.OrganizeId
    LEFT JOIN NewtouchHIS_Sett..xt_brjbxx d
        ON c.patid = d.patid
           AND c.OrganizeId = d.OrganizeId
 left join NewtouchHIS_Base..V_C_Sys_ItemsDetail e on e.catecode='xuetangclfs' and e.Code=a.xuetangclfs
WHERE (
          a.blh LIKE @keyword
          OR a.xm LIKE @keyword
      )
      AND a.OrganizeId = @orgId
      AND @kssj <= a.ghsj
      AND DATEADD(d, 1, @jssj) > a.ghsj
      and a.zt='1' ");
            if (string.IsNullOrEmpty(qbdc) || qbdc != "1")
            {
                if (!string.IsNullOrEmpty(jzysgh))
                {
                    sb.Append(" and a.jzys=@jzysgh");
                }
            }

            if (!string.IsNullOrEmpty(hznl) && hznl == "1")
            {
                sb.Append(" and CAST(FLOOR(DATEDIFF(DY, a.csny, GETDATE()) / 365.25) AS INT)>=35 ");
            }
            if (!string.IsNullOrEmpty(xtxy) && xtxy == "1")
            {
                sb.Append(
                    " and ( a.shousuoya IS NOT NULL or a.shuzhangya IS NOT NULL or a.xuetang IS NOT NULL )");
            }
            var par = new List<SqlParameter>();
            par.Add(new SqlParameter("@keyword", "%" + keyword + "%"));
            par.Add(new SqlParameter("@kssj", kssj));
            par.Add(new SqlParameter("@jssj", jssj));
            par.Add(new SqlParameter("@orgId", orgId));
            par.Add(new SqlParameter("@jzysgh", jzysgh));
            var list = this.QueryWithPage<OutpatientLogQueryVO>(sb.ToString(), pagination, par.ToArray());
            return list;

        }

        public IList<OutpatientLogQueryVO> GetLogGridList(string keyword, string kssj, string jssj, string jzysgh, string orgId, string hznl, string xtxy, string qbdc)
        {
            var sb = new StringBuilder();
            sb.Append(@"SELECT a.jzId,
       a.mzh,
       a.xm,
       a.xb,
       d.zjh sfzh,
       --CAST(FLOOR(DATEDIFF(DY, a.csny, GETDATE()) / 365.25) AS INT) age,
        a.nlshow,
       d.dh ContactNum,
       a.ghksmc,
       ISNULL((
           SELECT STUFF(
                  (
                      SELECT ';' + zdmc + (CASE ysbz
                                               WHEN 1 THEN
                                                   '??'
                                               ELSE
                                                   ''
                                           END
                                          )
                      FROM xt_xyzd xyzd
                          RIGHT JOIN xt_jz jz
                              ON xyzd.jzId = jz.jzId
                                 AND xyzd.OrganizeId = jz.OrganizeId
                                 AND xyzd.zt = '1'
                      WHERE jz.mzh = a.mzh
                            AND jz.zt = '1'
                            AND jz.OrganizeId = a.OrganizeId
                      ORDER BY zdlx ASC
                      FOR XML PATH('')
                  ),
                  1,
                  1,
                  ''
                       ) AS xyzdmc
       ),'')  +' '+
       ISNULL((
           SELECT STUFF(
                  (
                      SELECT ';' + zdmc + (CASE ysbz
                                               WHEN 1 THEN
                                                   '??'
                                               ELSE
                                                   ''
                                           END
                                          ),
                             (CASE ISNULL(zhmc, '')
                                  WHEN '' THEN
                                      ''
                                  ELSE
                                      '*【' + zhmc + '】'
                              END
                             )
                      FROM xt_zyzd zyzd
                          RIGHT JOIN xt_jz jz
                              ON zyzd.jzId = jz.jzId
                                 AND zyzd.OrganizeId = jz.OrganizeId
                                 AND zyzd.zt = '1'
                      WHERE jz.mzh = a.mzh
                            AND jz.zt = '1'
                            AND jz.OrganizeId = a.OrganizeId
                      ORDER BY zdlx ASC
                      FOR XML PATH('')
                  ),
                  1,
                  1,
                  ''
                       ) AS zyzdmc
       ),'') AS zdmc,
       b.zs,
       CONVERT(VARCHAR(10),CONVERT(FLOAT,a.shousuoya))+'/'+CONVERT(VARCHAR(10),CONVERT(FLOAT,a.shuzhangya)) xueya,
       ISNULL(e.Name,'') +CONVERT(VARCHAR(10),CONVERT(FLOAT,a.xuetang)) xuetang,
xuetangclfs,
CONVERT(VARCHAR(10),CONVERT(FLOAT,a.xuetang)) dqxuetang,
       CAST(a.tiwen as decimal(8,1)) tiwen,
       d.zy,
       d.xian_dz dz,
       convert(varchar(10),b.fbsj, 120) fbsj,
       SUBSTRING(CONVERT(VARCHAR(20),b.CreateTime,120) ,1,10) jzsj,
       b.xbs,
       b.clfa,
       b.fzjc,
       CASE
           WHEN
          a.cfzbz='1' THEN
               'Tái khám'
           ELSE
               'Khám lần đầu'
       END cfz,a.jzysmc,a.jzys,
case when FLOOR(DATEDIFF(DY, d.csny, GETDATE()) / 365.25)<15  then d.jjllr else null end jzxm,
case when FLOOR(DATEDIFF(DY, d.csny, GETDATE()) / 365.25)<15 then d.jjlldh else null end dhhm
FROM dbo.xt_jz a
    INNER JOIN dbo.xt_bl b
        ON a.jzId = b.jzId
           AND a.OrganizeId = b.OrganizeId
    LEFT JOIN NewtouchHIS_Sett..mz_gh c
        ON a.mzh = c.mzh
           AND a.OrganizeId = c.OrganizeId
    LEFT JOIN NewtouchHIS_Sett..xt_brjbxx d
        ON c.patid = d.patid
           AND c.OrganizeId = d.OrganizeId
 left join NewtouchHIS_Base..V_C_Sys_ItemsDetail e on e.catecode='xuetangclfs' and e.Code=a.xuetangclfs
WHERE (
          a.blh LIKE @keyword
          OR a.xm LIKE @keyword
      )
      AND a.OrganizeId = @orgId
      AND @kssj <= a.ghsj
      AND DATEADD(d, 1, @jssj) > a.ghsj
      and a.zt='1' ");
            if (string.IsNullOrEmpty(qbdc) || qbdc != "1")
            {
                if (!string.IsNullOrEmpty(jzysgh))
                {
                    sb.Append(" and a.jzys=@jzysgh");
                }
            }
            if (!string.IsNullOrEmpty(hznl) && hznl == "1")
            {
                sb.Append(" and CAST(FLOOR(DATEDIFF(DY, a.csny, GETDATE()) / 365.25) AS INT)>=35 ");
            }
            if (!string.IsNullOrEmpty(xtxy) && xtxy == "1")
            {
                sb.Append(
                    " and ( a.shousuoya IS NOT NULL or a.shuzhangya IS NOT NULL or a.xuetang IS NOT NULL )");
            }
            var par = new List<SqlParameter>();
            par.Add(new SqlParameter("@keyword", "%" + keyword + "%"));
            par.Add(new SqlParameter("@kssj", kssj));
            par.Add(new SqlParameter("@jssj", jssj));
            par.Add(new SqlParameter("@orgId", orgId));
            par.Add(new SqlParameter("@jzysgh", jzysgh));
            var list = this.FindList<OutpatientLogQueryVO>(sb.ToString(), par.ToArray()).OrderBy(p => p.jzsj).OrderBy(v => v.mzh).ToList();
            return list;

        }
        #endregion

        #region Ngoại trúxatrìnhytrị

        public YCYL_DtxdscDTO GetJcDtxdmc(string mzh, string orgId)
        {
            var sb = new StringBuilder();
            sb.Append(@"  SELECT a.cfh PlaceerOrderNo ,
        i.dtxd_FileType FileType ,
        CONVERT(VARCHAR(30),e.patid) PatientID ,
        '' FileNo ,
        '' FileTotalNum ,
        '' FileStreams ,
        '' FileHash ,
        a.sqdh CheckNumber ,
        '' FileDesc,
       i.dtxd_sjscjgmc  Sjscjgmc
 FROM   [Newtouch_CIS].[dbo].[xt_cf] a
        LEFT JOIN Newtouch_CIS..xt_jz b ON a.jzId = b.jzId
                                           AND a.OrganizeId = b.OrganizeId
                                           AND b.zt = '1'
        LEFT JOIN Newtouch_CIS..xt_cfmx c ON c.cfId = a.cfId
                                             AND c.OrganizeId = a.OrganizeId
                                             AND c.zt = '1'
        LEFT JOIN NewtouchHIS_Base..xt_sfxm d ON c.xmCode = d.sfxmCode
                                                 AND d.OrganizeId = c.OrganizeId
                                                 AND d.zt = '1'
                                                 AND d.sqlx = 'DTXD'
        LEFT JOIN [NewtouchHIS_Sett].[dbo].[xt_brjbxx] e ON b.blh = e.blh
                                                            AND b.OrganizeId = e.OrganizeId
                                                            AND e.zt = '1'
        LEFT JOIN [Newtouch_CIS].[dbo].[V_ycyl_sysConfig] i ON a.OrganizeId = i.OrganizeId 
WHERE   b.mzh = @mzh
        AND a.OrganizeId = @orgId
        AND a.zt = '1' AND a.sfbz=1 
        AND LEN(ISNULL(sqdh, '')) > 0  ");
            var dtxdscmc = this.FirstOrDefault<YCYL_DtxdscDTO>(sb.ToString(),
                new[]
                {
                    new SqlParameter("@mzh", mzh), new SqlParameter("@orgId", orgId)
                });
            return dtxdscmc;
        }
        public YCYL_DtxdSqdDTO GetJcDtxdSqd(string mzh, string orgId, string sqdh)
        {
            var sb = new StringBuilder();
            sb.Append(@"  
 SELECT CONVERT(VARCHAR(20),e.patid) PatientID ,
        e.xm Name ,
        b.kh IDCardNo ,
        CONVERT(VARCHAR(5),DATEDIFF( YEAR,e.csny,GETDATE())) Age ,
        e.xb Sex ,
        CONVERT(VARCHAR(20),e.csny, 120) BirthDate ,----
        b.mzh VisitID ,
        '1' PalientClass ,
        b.kh CardNO ,
        '' PointOfCare ,
        b.jzks DeptID ,
        f.Name DeptName ,
        '' Bed ,
        a.cfh PlaceerOrderNo ,
        c.xmCode ServicelD ,
        c.xmmc ServiceText ,
        d.sfdlCode CheckType ,-----
        a.ys ProviderlD ,
        g.Name ProvideName ,
        b.jzks RequestDeptID ,  ----
        f.Name RequestDeplName ,  -----
        CONVERT(VARCHAR(30), a.CreateTime,120) RequestDate ,
        '' Reason ,
        '' Attention ,
        '' Symptom ,
        '' ClinicDiagnosis ,
        '' RelevantClinicallnfo ,
        '' PrivacyLevel ,
        CONVERT(VARCHAR(10),d.dj) Charges ,
        CONVERT(VARCHAR(10),d.dj) Payments ,
        '' ConsultationApplyDateTime ,
        '' ConsultationApplyHospitalCode ,
        '' ConsultationApplyHospitalName ,
        '' ConsultationApplyDepartmentCode ,
        '' ConsultationApplyDepartmentName ,
        '' ConsultationApplyDoctorCode ,
        '' ConsultationApplyDoctorName ,
        '50' StudyStatus ,
        i.dtxd_ModalityCode ModalityCode ,
        '' RoomNumber ,
        '' AllergyHistory ,
        a.OrganizeId ExamHospitalCode ,-----
        h.Name ExamHospitalName ,-----
        b.jzks ExamDepartmentCode ,-----
        f.Name ExamDepartmentName ,-----
        a.ys ExamDoctorCode ,-----
        g.Name ExamDoctorName ,-----
        i.dtxd_DeviceCode DeviceCode ,
        i.dtxd_DeviceName DeviceName ,
        i.dtxd_Brand Brand ,
        i.dtxd_ModelNumber ModelNumber ,
        i.dtxd_Version Version ,
        i.dtxd_ProduceDate ProduceDate ,
        '0' IsPace ,
        CONVERT(VARCHAR(30), a.CreateTime,120) CollectBeginTime ,
        '' ImportDataDocCode ,
        '' ImportDataDocName ,
        '' ImportDataDocTime
 FROM   [Newtouch_CIS].[dbo].[xt_cf] a
        LEFT JOIN Newtouch_CIS..xt_jz b ON a.jzId = b.jzId
                                           AND a.OrganizeId = b.OrganizeId
                                           AND b.zt = '1'
        LEFT JOIN Newtouch_CIS..xt_cfmx c ON c.cfId = a.cfId
                                             AND c.OrganizeId = a.OrganizeId
                                             AND c.zt = '1'
        LEFT JOIN NewtouchHIS_Base..xt_sfxm d ON c.xmCode = d.sfxmCode
                                                 AND d.OrganizeId = c.OrganizeId
                                                 AND d.zt = '1'
                                                 AND d.sqlx = 'DTXD'
        LEFT JOIN [NewtouchHIS_Sett].[dbo].[xt_brjbxx] e ON b.blh = e.blh
                                                            AND b.OrganizeId = e.OrganizeId
                                                            AND e.zt = '1'
        LEFT JOIN NewtouchHIS_Base..Sys_Department f ON b.jzks = f.Code
                                                        AND b.OrganizeId = f.OrganizeId
                                                        AND f.zt = '1'
        LEFT JOIN NewtouchHIS_Base..Sys_Staff g ON b.jzys = g.gh
                                                   AND b.OrganizeId = g.OrganizeId
                                                   AND g.zt = '1'
        LEFT JOIN NewtouchHIS_Base..Sys_Organize h ON a.OrganizeId = h.Id
                                                      AND h.zt = '1'
        LEFT JOIN [Newtouch_CIS].[dbo].[V_ycyl_sysConfig] i ON a.OrganizeId = i.OrganizeId 
WHERE   b.mzh = @mzh
        AND a.OrganizeId = @orgId
        AND a.zt = '1' AND a.sfbz=1 
        AND a.sqdh = @sqdh ");
            var dtxdscmc = this.FirstOrDefault<YCYL_DtxdSqdDTO>(sb.ToString(),
                new[]
                {
                    new SqlParameter("@mzh", mzh), new SqlParameter("@orgId", orgId), new SqlParameter("@sqdh", sqdh)
                });
            return dtxdscmc;
        }

        /// <summary>
        /// Trong việnBáo cáo điện timgọiđọc
        /// </summary>
        /// <param name="kh"></param>
        /// <returns></returns>
        
        public DataTable chakanmingxidiaoyong(string kh)
        {
            string sBusinessReq = string.Format(@"<BusinessRequest><Parameters><Card_id>{0}</Card_id><Apply_No></Apply_No><Item_No></Item_No></Parameters></BusinessRequest>", kh);

            var bgebs = new BGESB();
            //bgebs.Url = "http://59.80.30.169:21180/";
            string retstr = bgebs.CallBGESB("ESBEFSQ003", sBusinessReq, yysz_esbBM, "1.0");
            if (retstr.Contains("Return='1'"))
            {
                StringReader sr = new StringReader(retstr);
                DataSet ds = new DataSet();
                ds.ReadXml(sr);
                return ds.Tables["BusinessResult"];
            }
            return null;
        }

        /// <summary>
        /// xatrìnhy tếtimđiệnĐăng ký
        /// </summary>
        /// <param name="PlaceerOrderNo"></param>
        public DataTable xindianshenqing(string patid, string orderno)
        {
            string sBusinessReqCGQ = "<BusinessRequest><Parameters><PatientID>{0}</PatientID><PlaceerOrderNo>{1}</PlaceerOrderNo></Parameters></BusinessRequest>";
            sBusinessReqCGQ = string.Format(sBusinessReqCGQ, patid, orderno);
            var bgebs = new BGESB();
            var sReturn = bgebs.CallBGESB("ESBECGQ003", sBusinessReqCGQ, yysz_esbBM, "1.0");
            if (sReturn.Contains("Return='1'"))
            {
                StringReader sr = new StringReader(sReturn);
                DataSet ds = new DataSet();
                ds.ReadXml(sr);
                return ds.Tables["BGESBResult"];
            }
            return null;
        }

        public DataTable DTXD_UploadData(YCYL_DtxdscDTO dto)
        {
            string sBusinessReqCGQ = "<BusinessRequest><Parameters><PatientID>{0}</PatientID><PlaceerOrderNo>{1}</PlaceerOrderNo><FileDesc>{2}</FileDesc><FileType>{3}</FileType><FileNo>{4}</FileNo><FileTotalNum>{5}</FileTotalNum><FileStreams>{6}</FileStreams><FileHash>{7}</FileHash><CheckNumber>{8}</CheckNumber></Parameters></BusinessRequest> ";
            sBusinessReqCGQ = string.Format(sBusinessReqCGQ, dto.PatientID, dto.PlaceerOrderNo, dto.FileDesc, dto.FileType, dto.FileNo, dto.FileTotalNum, dto.FileStreams, dto.FileHash, dto.CheckNumber);
            var bgebs = new BGESB();
            var sReturn = bgebs.CallBGESB("ESBEXPR001", sBusinessReqCGQ, "B1C2D8FD-2D47-4E28-AAA0-559FF0447B19", "1.0");
            if (sReturn.Contains("Return='1'"))
            {
                StringReader sr = new StringReader(sReturn);
                DataSet ds = new DataSet();
                ds.ReadXml(sr);
                return ds.Tables["BGESBResult"];
            }
            return null;
        }

        public DataTable DTXD_SqdUploadData(YCYL_DtxdSqdDTO dto)
        {
            string sBusinessReqCGQ = "<BusinessRequest><Parameters><PatientID>{0}</PatientID><Name>{1}</Name><IDCardNo>{2}</IDCardNo><Age>{3}</Age><Sex>{4}</Sex><BirthDate>{5}</BirthDate><VisitID>{6}</VisitID><PalientClass>{7}</PalientClass><CardNO>{8}</CardNO><PointOfCare>{9}</PointOfCare><DeptID>{10}</DeptID><DeptName>{11}</DeptName><Bed>{12}</Bed><PlaceerOrderNo>{13}</PlaceerOrderNo><ServicelD>{14}</ServicelD><ServiceText>{15}</ServiceText><CheckType>{16}</CheckType><ProviderlD>{17}</ProviderlD><ProvideName>{18}</ProvideName><RequestDeptID>{19}</RequestDeptID><RequestDeplName>{20}</RequestDeplName><RequestDate>{21}</RequestDate><Reason>{22}</Reason><Attention>{23}</Attention><Symptom>{24}</Symptom><ClinicDiagnosis>{25}</ClinicDiagnosis><RelevantClinicallnfo>{26}</RelevantClinicallnfo><PrivacyLevel>{27}</PrivacyLevel><Charges>{28}</Charges><Payments>{29}</Payments><ConsultationApplyDateTime>{30}</ConsultationApplyDateTime><ConsultationApplyHospitalCode>{31}</ConsultationApplyHospitalCode><ConsultationApplyHospitalName>{32}</ConsultationApplyHospitalName><ConsultationApplyDepartmentCode>{33}</ConsultationApplyDepartmentCode><ConsultationApplyDepartmentName>{34}</ConsultationApplyDepartmentName><ConsultationApplyDoctorCode>{35}</ConsultationApplyDoctorCode><ConsultationApplyDoctorName>{36}</ConsultationApplyDoctorName><StudyStatus>{37}</StudyStatus><ModalityCode>{38}</ModalityCode><RoomNumber>{39}</RoomNumber><AllergyHistory>{40}</AllergyHistory><ExamHospitalCode>{41}</ExamHospitalCode><ExamHospitalName>{42}</ExamHospitalName><ExamDepartmentCode>{43}</ExamDepartmentCode><ExamDepartmentName>{44}</ExamDepartmentName><ExamDoctorCode>{45}</ExamDoctorCode><ExamDoctorName>{46}</ExamDoctorName><DeviceCode>{47}</DeviceCode><DeviceName>{48}</DeviceName><Brand>{49}</Brand><ModelNumber>{50}</ModelNumber><Ver>{51}</Ver><ProduceDate>{52}</ProduceDate><IsPace>{53}</IsPace><CollectBeginTime>{54}</CollectBeginTime><ImportDataDocCode>{55}</ImportDataDocCode><ImportDataDocName>{56}</ImportDataDocName><ImportDataDocTime>{57}</ImportDataDocTime></Parameters></BusinessRequest> ";
            sBusinessReqCGQ = string.Format(sBusinessReqCGQ, dto.PatientID, dto.Name, dto.IDCardNo, dto.Age, dto.Sex, dto.BirthDate, dto.VisitID, dto.PalientClass, dto.CardNO, dto.PointOfCare, dto.DeptID, dto.DeptName, dto.Bed, dto.PlaceerOrderNo, dto.ServicelD, dto.ServiceText, dto.CheckType, dto.ProviderlD, dto.ProvideName, dto.RequestDeptID, dto.RequestDeplName, dto.RequestDate, dto.Reason, dto.Attention, dto.Symptom, dto.ClinicDiagnosis, dto.RelevantClinicallnfo, dto.PrivacyLevel, dto.Charges, dto.Payments, dto.ConsultationApplyDateTime, dto.ConsultationApplyHospitalCode, dto.ConsultationApplyHospitalName, dto.ConsultationApplyDepartmentCode, dto.ConsultationApplyDepartmentName, dto.ConsultationApplyDoctorCode, dto.ConsultationApplyDoctorName, dto.StudyStatus, dto.ModalityCode, dto.RoomNumber, dto.AllergyHistory, dto.ExamHospitalCode, dto.ExamHospitalName, dto.ExamDepartmentCode, dto.ExamDepartmentName, dto.ExamDoctorCode, dto.ExamDoctorName, dto.DeviceCode, dto.DeviceName, dto.Brand, dto.ModelNumber, dto.Version, dto.ProduceDate, dto.IsPace, dto.CollectBeginTime, dto.ImportDataDocCode, dto.ImportDataDocCode, dto.ImportDataDocTime);
            var bgebs = new BGESB();
            var sReturn = bgebs.CallBGESB("ESBEXPR002", sBusinessReqCGQ, "EBD61E2D-1AE6-4162-92B4-FBC3D63C10CE", "1.0");
            if (sReturn.Contains("Return='1'"))
            {
                StringReader sr = new StringReader(sReturn);
                DataSet ds = new DataSet();
                ds.ReadXml(sr);
                return ds.Tables["BGESBResult"];
            }
            return null;
        }

        /// <summary>
        /// xatrìnhy tếtimđiệngọiđọc
        /// </summary>
        /// <param name="PlaceerOrderNo"></param>
        public DataTable xindiandiaoyue(string kh, string orderno)
        {
            string sBusinessReqCGQ = "<BusinessRequest><Parameters><PatientID>{0}</PatientID><PlaceerOrderNo>{1}</PlaceerOrderNo></Parameters></BusinessRequest>";
            sBusinessReqCGQ = string.Format(sBusinessReqCGQ, kh, orderno);
            var bgebs = new BGESB();
            var sReturn = bgebs.CallBGESB("ESBECGQ005", sBusinessReqCGQ, yysz_esbBM, "1.0");
            if (sReturn.Contains("Return='1'"))
            {
                StringReader sr = new StringReader(sReturn);
                DataSet ds = new DataSet();
                ds.ReadXml(sr);
                return ds.Tables["BGESBResult"];
            }
            return null;
        }

        /// <summary>
        ///  xatrìnhy tếXét nghiệmĐăng ký
        /// </summary>
        /// <param name="kh"></param>
        /// <param name="jzlsh">Khám bệnhdòngchảysố</param>
        /// <param name="calltype"></param>
        /// <returns></returns>
        public DataTable jianyanshenqing(string patid, string jzlsh, string OrgId)
        {

            string settsql = @"select convert(varchar(20),patid)patid, convert(varchar(20),ghnm) ghnm
                        from [NewtouchHIS_Sett].dbo.mz_gh with(nolock)
                        where organizeid=@orgId and mzh=@mzh and zt='1' ";
            var ety = FindList<YCYL_LisInpDTO>(settsql, new SqlParameter[] { new SqlParameter("@orgId", OrgId), new SqlParameter("@mzh", patid) }).FirstOrDefault();

            if (ety == null)
            {
                throw new FailedException("Bệnh nhânKhám bệnhthông tinNgoại lệ");
            }
            // string sBusinessReqCGQ = @"<BusinessRequest><Parameters><PatientID>{0}</PatientID><JZLSH>{1}</JZLSH></Parameters></BusinessRequest>";
            string sBusinessReqCGQ = @"<BusinessRequest>
              <Parameters>
                <PatientID>{0}</PatientID>
                <JZLSH>{1}</JZLSH>
              </Parameters>
            </BusinessRequest>
            ";
            sBusinessReqCGQ = string.Format(sBusinessReqCGQ, ety.patid, ety.ghnm);
            AppLogger.Info(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " ESBLISQ002=>[BusinessRequest]:" + sBusinessReqCGQ);
            var bgebs = new BGESB();
            try
            {
                var sReturn = bgebs.CallBGESB("ESBLISQ002", sBusinessReqCGQ, yysz_esbBM, "1.0");
                //string sReturn = @"<BGESBEnvelope >
                //     <BGESBResult Return='1' >
                //      <BusinessResult>
                //       <!--gọiQuay lạinghiệp vụDữ liệukếtnếutập-->
                //       <RMT_system>
                //        <Index>2DE1D73B-BC58-471F-8103-4475D423A044</Index>
                //       </RMT_system>
                //      </BusinessResult>
                //     </BGESBResult>
                //    </BGESBEnvelope>";

                AppLogger.Info("ESBLISQ002=>[BGESBResult]：" + sReturn);
                if (sReturn.ToLower().Contains("return='1'"))
                {
                    //StringReader sr = new StringReader(sReturn);
                    //DataSet ds = new DataSet();
                    //ds.ReadXml(sr);
                    //return ds.Tables["BGESBResult"];
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(sReturn);
                    string retKey = doc.SelectSingleNode(@"/BGESBEnvelope/BGESBResult/LIS_system/StatusCode").InnerText;
                    if (retKey == "0000") //Gửi đơn thành công
                    {
                        ////2.2pháttừxatrìnhy tếvàođộTra cứuhủytin（ESBRMTQ002）
                        //sBusinessReqCGQ = @"<BusinessRequest>
                        //  <Parameters>
                        //    <Card_id>{0}</Card_id>
                        //    <Index>{1}</Index>
                        //  </Parameters>
                        //</BusinessRequest>";
                        //sBusinessReqCGQ = string.Format(sBusinessReqCGQ, patid, retKey);
                        //AppLogger.Info(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " ESBRMTQ002=>[BusinessRequest]:" + sBusinessReqCGQ);
                        //var sReturn2 = bgebs.CallBGESB("ESBRMTQ002", sBusinessReqCGQ, yysz_esbBM, "1.0");

                        //AppLogger.Info(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " ESBRMTQ002=>[BGESBResult]:" + sReturn2);

                        //if (!string.IsNullOrWhiteSpace(sReturn2) && sReturn2.ToLower().Contains("return='1'"))
                        //{
                        //XmlDocument doc2 = new XmlDocument();
                        //doc2.LoadXml(sReturn2);
                        //string code = doc2.SelectSingleNode(@"/BGESBEnvelope/RMT_system/Code").InnerText;
                        //if (code == "0000")
                        //{
                        string cflist = "";
                        int i = 1;
                        foreach (string cf in jzlsh.Split(','))
                        {
                            if (i == 1)
                            {
                                cflist = "'" + cf + "'";
                            }
                            else
                            {
                                cflist += ",'" + cf + "'";
                            }

                            i++;
                        }

                        //Tải lênthành côngđặtcfTrạng thái
                        string sql = @"        
                                    UPDATE dbo.xt_cf
                                    SET Syncstatus = 1
                                    WHERE OrganizeId = '{0}'
                                          AND cfh in( {1})
                                          AND zt = '1';";
                        sql = string.Format(sql, OrgId, cflist);
                        this.ExecuteSqlCommand(sql);
                        AppLogger.Info("xatrìnhXét nghiệmTải lênthành công");
                        //}
                        //else
                        //{
                        //    throw new FailedException("xatrìnhXét nghiệmTải lênthất bại");
                        //}
                        //}
                        //else
                        //{
                        //    throw new FailedException("Tải lênkếtnếuTra cứuthất bại");
                        //}
                    }
                    else
                    {
                        throw new FailedException("Tải lêngiao diệnQuay lạiNgoại lệ");
                    }
                }
                else
                {
                    throw new FailedException("Tải lêngiao diệnNgoại lệ");
                }
            }
            catch (Exception ex)
            {
                AppLogger.Info(ex.InnerException + ex.Message);
                throw new FailedException("Tải lênthất bại");
            }

            return null;
        }

        /// <summary>
        /// xatrìnhy tếHình ảnhĐăng ký
        /// </summary>
        /// <param name="hispatientid"></param>
        /// <param name="name"></param>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <returns></returns>
        public DataTable yingxiangshenqing(string patid, string AccessionNumber)
        {
            string sBusinessReqCGQ = @"<BusinessRequest><Parameters><GlobalPatientId>{0}</GlobalPatientId><AccessionNumber>{1}</AccessionNumber></Parameters></BusinessRequest>";
            sBusinessReqCGQ = string.Format(sBusinessReqCGQ, patid, AccessionNumber);
            var bgebs = new BGESB();
            var sReturn = bgebs.CallBGESB("ESBPACSQ002", sBusinessReqCGQ, yysz_esbBM, "1.0");
            if (sReturn.ToLower().Contains("return='1'"))
            {
                StringReader sr = new StringReader(sReturn);
                DataSet ds = new DataSet();
                ds.ReadXml(sr);
                return ds.Tables["BGESBResult"];
            }
            return null;
        }

        public List<string> GetCfhBysqlx(string orgId, string sqlx)
        {
            var sql = @"select cf.cfh from xt_cfmx cfmx inner join xt_cf cf on cfmx.cfId=cf.cfId and cfmx.OrganizeId=cf.OrganizeId where cfmx.sqlx in
        (select id.name from NewtouchHIS_Base..Sys_ItemsDetail id
                 inner join NewtouchHIS_Base..Sys_Items i on i.id = id.itemid
                  where i.Code = 'ExamTypeID'
                  and(OrganizeId = '*' or OrganizeId = @orgId)
                  and id.code in (select value from Sys_Config where code = @sqlx and OrganizeId = @orgId))";
            return this.FindList<string>(sql, new[]
            { new SqlParameter("@sqlx", sqlx), new SqlParameter("@orgId", orgId) });

        }
        public List<string> GetCfhBycflx(string orgId, int cflx, string mzh)
        {
            var sql = @"select a.cfh 
                    from xt_cf a with(nolock) ,xt_cfmx b with(nolock)
                    where a.cfId=b.cfId and a.OrganizeId=b.OrganizeId and a.zt=1 and b.zt=1 and isnull(a.SyncStatus,0)<>1
                    and exists(select 1 from [dbo].[xt_jz] c where a.jzId=c.jzId and a.organizeid=c.organizeid and c.mzh=@mzh and c.zt=1)
                    and a.cflx=@cflx";
            return this.FindList<string>(sql, new[]
            {
              new SqlParameter("@mzh", mzh),
              new SqlParameter("@orgId", orgId),
              new SqlParameter("@cflx", (int)EnumCflx.InspectionPres)
          });

        }
        public List<PrescriptionEntity> GetcfByJzId(string orgId, string jzId)
        {
            var sql = @"select * from xt_cf where OrganizeId=@orgId and jzid=@jzid";
            return this.FindList<PrescriptionEntity>(sql, new[]
            { new SqlParameter("@jzid", jzId), new SqlParameter("@orgId", orgId) });

        }
        #endregion

        public lastzcfDto GetLastzcf(string blh, string orgId)
        {
            // Lấycuối cùngmột lầnthulấykhámtraphícủaThời gianvàMục thu phíChi tiết
            var lastsql = @"select top 1 c.xmmc zcfsfxm,convert(varchar(10),c.createtime, 20) zcfsj  from xt_jz a 
                left join xt_cf b on a.jzId=b.jzId and a.OrganizeId=b.OrganizeId
                left join xt_cfmx c on b.cfId=c.cfId and c.OrganizeId=b.OrganizeId
                where a.blh=@blh and
                b.sfbz=1 and isnull(tbz,'')<>1 and
                c.xmCode in (select sfxmCode from NewtouchHIS_Base..V_S_xt_sfxm where sfdlCode='01' and OrganizeId=a.OrganizeId) and a.OrganizeId =@orgId
                order by c.CreateTime desc";
            return this.FirstOrDefault<lastzcfDto>(lastsql,
                new[] { new SqlParameter("@blh", blh),
            new SqlParameter("@orgId", orgId)});
        }
        /// <summary>
        /// Tổ chứcTên
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public OrganizationData GetOrgInfo(string orgId)
        {
            var orgsql = @"select Code orgCode,Name orgName from [NewtouchHIS_Base]..[Sys_Organize]
                where Id=@orgId and zt=1 ";

            return this.FirstOrDefault<OrganizationData>(orgsql.ToString(), new[] { new SqlParameter("@orgId", orgId) });
        }
        /// <summary>
        /// LấythứbaphươngđốichiếuGiá trị
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="TTcode"></param>
        /// <returns></returns>
        public string GetTTItem(string orgId, string TTtype, string TTcode, string txtype)
        {
            FirstSecondThird TTobj = new FirstSecondThird();
            try
            {
                TTobj = _tTCataloguesComparisonDmnService.GetTTItem(orgId, TTtype, TTcode, txtype);
                return TTobj.First;
            }
            catch (Exception er)
            {
            }
            switch (TTtype)
            {
                case "fylb":
                    return "91";//KhácChi phí
                case "sypc":
                    return "99";
                case "yf":
                    return "9";
                default:
                    return string.Empty;
            }
        }
        /// <summary>
        /// Đơn thuốcnhắc trước
        /// </summary>
        /// <param name="xyzdList"></param>
        /// <param name="zyzdList"></param>
        /// <param name="strCfList"></param>
        /// <param name="mzh"></param>
        /// <param name="jlId"></param>
        /// <param name="orgId"></param>
        /// <param name="rygh"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string GetqhdCfSqtxData(List<WMDiagnosisHtmlVO> xyzdList, List<TCMDiagnosisHtmlVO> zyzdList, string strCfList, string mzh, out string jlId, string orgId, string rygh, string userName)
        {
            //Lấyhiện tạiBệnh nhânKhám bệnhthông tin
            TreatmentEntity patjzInfo = _treatmentRepo.IQueryable(p => p.mzh == mzh && p.OrganizeId == orgId && p.zt == "1").FirstOrDefault();
            QhdPrescriptionDTO pre = new QhdPrescriptionDTO();
            REQUESTDATA request = new REQUESTDATA();
            KC21XML kc21 = new KC21XML();
            List<KC22ROW> kc22list = new List<KC22ROW>();
            List<KC33ROW> kc33list = new List<KC33ROW>();
            //trangmặttrênGửiquađếncủaĐơn thuốcchi tiếtdanh sách    //trọngcấu trúcĐơn thuốcDữ liệu
            var cfDtoList = Tools.Json.ToList<PrescriptionHtmlDTO>(strCfList);
            OrganizationData orgInfo = GetOrgInfo(orgId);

            #region gốctiếtđiểmlắp ráp
            request.AKB020 = orgInfo.orgCode;
            request.AKB021 = orgInfo.orgName;
            request.MSGNO = "5100";
            request.MSGID = orgInfo.orgCode + DateTime.Now.ToString("yyyyMMddHHmmss");
            request.GRANTID = string.Empty;
            request.OPERID = rygh;
            request.OPERNAME = userName;
            #endregion

            #region kc21Lắp ráp dữ liệu nút
            kc21.AKC190 = mzh;
            var ybmzbz = "";
            switch (patjzInfo.mjzbz)
            {
                case 1:
                    ybmzbz = "11";
                    break;
                case 2:
                    ybmzbz = "14";
                    break;
                case 4:
                    ybmzbz = "15";
                    break;
                case 6:
                    ybmzbz = "13";
                    break;
            }
            kc21.AKA130 = ybmzbz;
            kc21.AKC192 = patjzInfo.zlkssj.ToString("yyyyMMddHHmmss");
            kc21.AKC193 = xyzdList.Count > 0 ? xyzdList[0].zdCode : zyzdList[0].zdCode;
            kc21.ZKC274 = xyzdList.Count > 0 ? xyzdList[0].zdmc : zyzdList[0].zdmc;
            kc21.AKC194 = string.Empty;
            kc21.AKC195 = string.Empty;
            kc21.AKC196 = string.Empty;
            kc21.ZKC275 = string.Empty;
            kc21.ZKC285 = string.Empty;
            kc21.ZKC286 = string.Empty;
            kc21.BKF040 = patjzInfo.jzks;
            kc21.ZKC272 = patjzInfo.ghksmc;
            kc21.BKF050 = patjzInfo.jzys;
            kc21.ZKC271 = patjzInfo.jzysmc;
            kc21.AAC001 = patjzInfo.grbh;
            kc21.CKC502 = patjzInfo.kh;
            kc21.AAC003 = patjzInfo.xm;
            kc21.AAE135 = patjzInfo.zjh;
            kc21.AAC004 = (patjzInfo.xb == "Nam" ? "1" : "2");
            kc21.BAE450 = Convert.ToDecimal(new string(patjzInfo.nlshow.TakeWhile(c => char.IsDigit(c) || c == '.').ToArray()));
            kc21.AAE011 = patjzInfo.CreatorCode;
            kc21.AAE036 = DateTime.Now.ToString("yyyyMMddHHmmss");
            request.KC21XML = kc21;
            #endregion

            #region kc22Lắp ráp dữ liệu nút
            foreach (var item in cfDtoList)
            {
                foreach (var cfitem in item.cfHtml)
                {
                    if (string.IsNullOrWhiteSpace(cfitem.cfId))
                    {
                        cfitem.cflx = item.cflx == "kfcf" ? (int)EnumCflx.RehabPres : item.cflx == "cgxmcf" ? (int)EnumCflx.RegularItemPres : item.cflx == "xycf" ? (int)EnumCflx.WMPres : item.cflx == "zycf" ? cfitem.cflx = (int)EnumCflx.TCMPres : item.cflx == "jycf" ? (int)EnumCflx.InspectionPres : item.cflx == "jccf" ? (int)EnumCflx.ExaminationPres : 0;
                        if (cfitem.cflx == (int)EnumCflx.InspectionPres || cfitem.cflx == (int)EnumCflx.ExaminationPres)
                        {
                            //Xét nghiệm Kiểm tra sl 1
                            cfitem.sl = 1;  //sl 1
                            cfitem.je = cfitem.dj;
                        }

                        KC22ROW row = new KC22ROW();
                        row.AKC190 = mzh;
                        row.AKC220 = cfitem.cfh;
                        row.AKC221 = DateTime.Now.ToString("yyyyMMddHHmmss");

                        var sfdlcode = "";
                        if (cfitem.cflx == (int)EnumCflx.InspectionPres || cfitem.cflx == (int)EnumCflx.ExaminationPres || cfitem.cflx == (int)EnumCflx.RegularItemPres || cfitem.cflx == (int)EnumCflx.RehabPres)
                        {
                            SqtxXmYpInfoVO xmInfo = GetXmYpInfo(cfitem.xmCode, "1", orgId);
                            sfdlcode = xmInfo.sfdlCode;
                            row.AKE001 = xmInfo.ybdm;
                            row.AKE002 = cfitem.xmmc;
                            row.AKC515 = cfitem.xmCode;
                            row.AKC223 = cfitem.xmmc;
                            row.AKC224 = "2";

                        }
                        else
                        {
                            SqtxXmYpInfoVO ypInfo = GetXmYpInfo(cfitem.ypCode, "2", orgId);
                            sfdlcode = ypInfo.sfdlCode;
                            row.AKE001 = ypInfo.ybdm;
                            row.AKE002 = cfitem.ypmc;
                            row.AKC515 = cfitem.ypCode;
                            row.AKC223 = cfitem.ypmc;
                            row.AKC224 = "1";
                        }
                        row.AKA063 = GetTTItem(orgId, "fylb", sfdlcode, "sqtx");
                        row.AKC225 = cfitem.dj;
                        row.AKC226 = cfitem.sl;
                        row.AKC227 = System.Decimal.Round((cfitem.dj * cfitem.sl), 4);
                        row.AKA070 = string.Empty;
                        row.CKC132 = GetTTItem(orgId, "jldw", cfitem.mcjldw, "sqtx");
                        row.AKA074 = cfitem.ypgg;
                        row.AKA071 = cfitem.mcjl == null ? 0 : cfitem.mcjl;
                        row.AKA075 = cfitem.mcjl == null ? 0 : cfitem.mcjl;
                        row.AKA072 = GetTTItem(orgId, "sypc", cfitem.pcCode, "sqtx");
                        row.AKA073 = GetTTItem(orgId, "yf", cfitem.yfCode, "sqtx");
                        row.ZKA101 = GetTTItem(orgId, "xsdw", cfitem.dw, "sqtx");
                        row.AKA067 = "1";
                        row.AKC229 = 1;
                        row.BKC127 = string.Empty;
                        row.BKF050 = rygh;
                        row.ZKC271 = userName;
                        row.AAE011 = patjzInfo.CreatorCode;
                        row.AAE036 = DateTime.Now.ToString("yyyyMMddHHmmss");

                        kc22list.Add(row);
                    }
                }
            }
            request.KC22XML = kc22list;
            #endregion

            #region kc33Dữ liệu nút
            var cnt = 0;
            if (xyzdList != null)
            {
                foreach (var xyitem in xyzdList)
                {
                    cnt++;
                    KC33ROW xyrow = new KC33ROW();
                    xyrow.AKC190 = mzh;
                    xyrow.BKE150 = cnt;
                    xyrow.CKC305 = (xyitem.zdlx == 2 ? 0 : xyitem.zdlx).ToString();
                    xyrow.CKC304 = DateTime.Now.ToString("yyyyMMddHHmmss");
                    xyrow.CKC302 = xyitem.zdCode;
                    xyrow.CKC303 = xyitem.zdmc;
                    xyrow.BKF050 = rygh;
                    xyrow.ZKC271 = userName;
                    xyrow.AAE013 = string.Empty;
                    kc33list.Add(xyrow);
                }
            }
            if (zyzdList != null)
            {
                foreach (var zyitem in zyzdList)
                {
                    cnt++;
                    KC33ROW zyrow = new KC33ROW();
                    zyrow.AKC190 = mzh;
                    zyrow.BKE150 = cnt;
                    zyrow.CKC305 = (zyitem.zdlx == 2 ? 0 : zyitem.zdlx).ToString();
                    zyrow.CKC304 = DateTime.Now.ToString("yyyyMMddHHmmss");
                    zyrow.CKC302 = zyitem.zdCode;
                    zyrow.CKC303 = zyitem.zdmc;
                    zyrow.BKF050 = rygh;
                    zyrow.ZKC271 = userName;
                    zyrow.AAE013 = string.Empty;
                    kc33list.Add(zyrow);
                }
            }
            request.KC33XML = kc33list;
            #endregion
            pre.REQUESTDATA = request;
            string responsexml = pre.XmlSerialize().Replace("<?xml version=\"1.0\"?>", "<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            var qhdznshentity = new QhdZnshSqtxEntity
            {
                OrganizeId = orgId,
                jydm = "5100",
                mzzyh = mzh,
                Type = "1",
                XmlRequest = responsexml,
                zt = "1",
            };
            var rzid = "";
            _qhdznshsqtxRepo.SubmitForm(qhdznshentity, out rzid);
            jlId = rzid;
            return responsexml;
        }

        public List<LisReportSqdhValueVo> GetLisSqdhData(string orgId, string zymzh, string type, string ztmc, string kssj, string jssj)
        {
            var sql = "";
            var par = new List<SqlParameter>();
            if (type == "mz")
            {
                sql = @" select OrganizeId,xm,lissqdh,sqdh,
stuff((select distinct ','+u.ztmc from xt_jz t 
join xt_cf y on t.jzId=y.jzId and t.OrganizeId=y.OrganizeId
join xt_cfmx u on y.cfId=u.cfId and y.OrganizeId=u.OrganizeId
where  t.OrganizeId=q.OrganizeId and q.lissqdh=y.cfh and q.sqdh=y.sqdh
for xml path('')),1,1,'') as ztmc,syncStatus,max(sqsj) sqsj from(select distinct a.OrganizeId,a.xm,b.cfh lissqdh,b.sqdh,c.ztId,c.ztmc,
 case d.sqdzt  when '0' then 'Đã đăng ký' when '1' then 'Đã nhận' when '2' then 'Đã hoàn thành' else 'Chờ đăng ký' end syncStatus,
CONVERT(varchar(100), b.CreateTime, 20) sqsj,row_number() over(partition by c.ztmc order by b.CreateTime desc) rn
 from xt_jz a
join xt_cf b on a.jzId=b.jzId and a.OrganizeId=b.OrganizeId
join xt_cfmx c on b.cfId=c.cfId and b.OrganizeId=c.OrganizeId
join [NewtouchHIS_Sett].dbo.mz_cf d on d.cfh=b.cfh and d.OrganizeId=b.OrganizeId   and d.zt='1'
where  b.cflx='4' and a.mzh=@zymzh  and a.OrganizeId=@orgId and a.zt=1";
                if (!string.IsNullOrEmpty(ztmc))
                {
                    sql += " and c.ztmc like @ztmc ";
                    par.Add(new SqlParameter("@ztmc", "%" + ztmc + "%"));
                }
                if (!string.IsNullOrEmpty(kssj))
                {
                    sql += " and b.CreateTime>=@kssj ";
                    par.Add(new SqlParameter("@kssj", kssj));
                }
                if (!string.IsNullOrEmpty(jssj))
                {
                    sql += " and b.CreateTime<=@jssj ";
                    par.Add(new SqlParameter("@jssj", jssj));
                }
                sql += ") q where q.rn=1 group by OrganizeId,xm,lissqdh,sqdh,syncStatus order by sqsj desc";
            }
            else
            {
                sql = @"select OrganizeId,xm,lissqdh,sqdh,
stuff((select distinct ','+b.ztmc from zy_lsyz b
where b.yzh=c.lissqdh and b.OrganizeId=c.OrganizeId
for xml path('')),1,1,'') as ztmc,
syncstatus,max(sqsj) sqsj ,blh FROM (select distinct a.OrganizeId,hzxm xm,yzh lissqdh,sqdh,ztId,ztmc,
case syncStatus when '0' then 'Đã đăng ký' when '1' then 'Đã nhận' when '2' then 'Đã hoàn thành' else 'Chờ đăng ký' end syncStatus
,CONVERT(varchar(100), a.CreateTime, 20) sqsj,b.blh,row_number() over(partition by a.ztmc order by a.CreateTime desc) rn  from zy_lsyz a with(nolock)
left join  zy_brxxk b on a.zyh=b.zyh and a.organizeId=b.organizeId and a.zt=b.zt where  yzlx='6' and a.zyh=@zymzh  and a.OrganizeId=@orgId and a.zt=1 and yzzt='2'";
                if (!string.IsNullOrEmpty(ztmc))
                {
                    sql += " and ztmc like @ztmc ";
                    par.Add(new SqlParameter("@ztmc", "%" + ztmc + "%"));
                }
                if (!string.IsNullOrEmpty(kssj))
                {
                    sql += " and a.CreateTime>=@kssj ";
                    par.Add(new SqlParameter("@kssj", kssj));
                }
                if (!string.IsNullOrEmpty(jssj))
                {
                    sql += " and a.CreateTime<=@jssj ";
                    par.Add(new SqlParameter("@jssj", jssj));
                }
                sql += ") c WHERE rn = 1 group by OrganizeId ,xm,lissqdh,sqdh,syncstatus,blh order by sqsj desc";
            }

            par.Add(new SqlParameter("@zymzh", zymzh));
            par.Add(new SqlParameter("@orgId", orgId));

            return this.FindList<LisReportSqdhValueVo>(sql, par.ToArray());
            //return this.FindList<LisReportSqdhValueVo>(sql, new[]
            //{ new SqlParameter("@zymzh", zymzh), new SqlParameter("@orgId", orgId), new SqlParameter("@ztmc","%" + ztmc + "%"),new SqlParameter("@kssj",kssj),new SqlParameter("@jssj",jssj) });

        }

        public IList<LisReportSqdyczValueVo> GetLisSqdyczData(Pagination pagination, string orgId, string zymzh, string type, string ztmc, string ycbz, string sqdht)
        {
            var sql = "";
            var par = new List<SqlParameter>();
            //if (type == "mz")
            //{
            sql = @"select * from (
select   q.id,
stuff((select distinct ','+u.ztmc from xt_jz t 
join xt_cf y on t.jzId=y.jzId and t.OrganizeId=y.OrganizeId
join xt_cfmx u on y.cfId=u.cfId and y.OrganizeId=u.OrganizeId
where  t.OrganizeId=q.OrganizeId and q.sqdh=y.cfh 
for xml path('')),1,1,'') as ztmc,q.sqdh,q.xmzwmc,q.xmywmc,q.jyjg,q.gdbj,q.ckzdx,q.ckzgx,q.ckz,q.sqdlx
from(
select c.id,c.sqdh,c.xmzwmc,c.xmywmc,c.jyjg,c.gdbj,c.ckzdx,c.ckzgx,c.ckz,a.OrganizeId ,b.cflx sqdlx
from xt_jz a with(nolock)
left join xt_cf b with(nolock) on a.jzid=b.jzid  and b.zt=1 and a.OrganizeId=b.OrganizeId
left join [NewtouchHIS_Sett]..mz_cf d with(nolock) on b.cfh=d.cfh and d.zt=1 and b.OrganizeId=d.OrganizeId
left join [Newtouch_Interface].[dbo].[Lis_Report_MZ] c with(nolock) on  c.sqdh=d.cfh and b.OrganizeId=c.OrganizeId
where a.zt=1 and b.cflx='4' and d.sqdzt='2' 
and a.mzh=@mzh and a.OrganizeId=@orgid
union all
select c.id,c.ReqNO sqdh,c.ExamName xmzwmc,''xmywmc,c.DiagName jyjg,''gdbj,''ckzdx,''ckzgx,''ckz,a.OrganizeId ,b.cflx sqdlx
from xt_jz a with(nolock)
left join xt_cf b with(nolock) on a.jzid=b.jzid  and b.zt=1 and a.OrganizeId=b.OrganizeId
left join [NewtouchHIS_Sett]..mz_cf d  with(nolock) on b.cfh=d.cfh and d.zt=1 and b.OrganizeId=d.OrganizeId
left join [Newtouch_Interface].[dbo].[PACS_RIS_REPORT] c with(nolock) on c.ReqNO=d.cfh and b.OrganizeId=c.OrganizeId
where a.zt=1 and b.cflx='5' and d.sqdzt='2' 
and a.mzh=@mzh and a.OrganizeId=@orgid
)q 
)g  where g.ztmc like @ztmc  and g.sqdh like @sqdht ";
            if (!string.IsNullOrEmpty(ycbz))
            {
                if (ycbz == "0")
                {
                    sql += " and g.gdbj<>'' ";
                }
                else if (ycbz == "1")
                {

                }
            }
            //}
            par.Add(new SqlParameter("@mzh", zymzh));
            par.Add(new SqlParameter("@orgid", orgId));
            par.Add(new SqlParameter("@ztmc", "%" + ztmc + "%"));
            par.Add(new SqlParameter("@sqdht", "%" + sqdht + "%"));
            return this.QueryWithPage<LisReportSqdyczValueVo>(sql, pagination, par.ToArray());
        }

        #region Bổ sungđẩyĐơn thuốcđếnPDS
        public void SyncToPdsQuery(string operatorCode, string ghrq)
        {
            string sql = "";
            string sqljz = @"select * from xt_jz with(nolock) where ghsj=convert(date,@ghrq) and jzid<>'38dc00f7-7a31-49c5-b9b1-07c0bc972633'";
            var jzlist = FindList<TreatmentEntity>(sqljz, new SqlParameter[] { new SqlParameter("ghrq", ghrq) });
            if (jzlist != null && jzlist.Count > 0)
            {
                foreach (var jz in jzlist)
                {
                    sql = @"select *
from Newtouch_CIS.dbo.xt_cf a with(nolock)
where  cflx in(1,2) 
and not exists(select 1 from NewtouchHIS_PDS.dbo.mz_cf b with(nolock) where a.cfh=b.cfh and a.OrganizeId=b.OrganizeId )
and a.zt='1' and a.jzId=@jzId ";
                    var cflist = FindList<PrescriptionDTO>(sql, new SqlParameter[] { new SqlParameter("jzId", jz.jzId) });
                    if (cflist != null && cflist.Count > 0)
                    {
                        //foreach (var cf in cflist)
                        //{
                        //    string sqlmx = @"select * from xt_cfmx with(nolock) where cfId=@cfid and zt='1'";
                        //    cf.cfmxList = FindList<PrescriptionDetailVO>(sqlmx, new SqlParameter[] { new SqlParameter("cfid", cf.cfId) });
                        //    sqlmx = "";
                        //}
                        SyncToPDS(jz, cflist, operatorCode);
                    }
                }
            }
        }

        public List<PacsReportSqdhValueVo> GetPacsSqdhData(string orgId, string zymzh, string type, string ztmc, string kssj, string jssj)
        {
            var sql = ""; var par = new List<SqlParameter>();
            if (type == "mz")
            {
                sql = @" select OrganizeId,xm,lissqdh,sqdh,
stuff((select distinct ','+u.ztmc from xt_jz t 
join xt_cf y on t.jzId=y.jzId and t.OrganizeId=y.OrganizeId
join xt_cfmx u on y.cfId=u.cfId and y.OrganizeId=u.OrganizeId
where  t.OrganizeId=c.OrganizeId and c.lissqdh=y.cfh and c.sqdh=y.sqdh
for xml path('')),1,1,'')  as ztmc,
syncstatus,max(sqsj) sqsj  FROM (select distinct a.OrganizeId,a.xm,b.cfh lissqdh,b.sqdh,c.ztId,c.ztmc,
 case d.sqdzt when '2' then 'Chờ báo cáo' when '3' then 'Đã có báo cáo' when '4' then 'Đã hủy' else 'Chờ đăng ký' end syncStatus
 ,CONVERT(varchar(100), a.CreateTime, 20) sqsj,row_number() over(partition by c.cfid order by a.CreateTime desc) rn 
 from xt_jz a
join xt_cf b on a.jzId=b.jzId and a.OrganizeId=b.OrganizeId
join xt_cfmx c on b.cfId=c.cfId and b.OrganizeId=c.OrganizeId
join [NewtouchHIS_Sett].dbo.mz_cf d on d.cfh=b.cfh and d.OrganizeId=b.OrganizeId 
where  b.cflx='5' and a.mzh=@zymzh  and a.OrganizeId=@orgId and a.zt=1 and d.zt='1'";
                if (!string.IsNullOrEmpty(ztmc))
                {
                    sql += " and c.ztmc like @ztmc ";
                    par.Add(new SqlParameter("@ztmc", "%" + ztmc + "%"));
                }
                if (!string.IsNullOrEmpty(kssj))
                {
                    sql += " and b.CreateTime>=@kssj ";
                    par.Add(new SqlParameter("@kssj", kssj));
                }
                if (!string.IsNullOrEmpty(jssj))
                {
                    sql += " and b.CreateTime<=@jssj ";
                    par.Add(new SqlParameter("@jssj", jssj));
                }
                sql += ") c WHERE rn = 1 group by OrganizeId,xm,lissqdh,sqdh,syncStatus order by sqsj desc";
            }
            else
            {
                sql = @"select OrganizeId,xm,lissqdh,sqdh,
stuff((select distinct ','+b.ztmc from zy_lsyz b with(nolock)
where b.yzh=c.lissqdh and b.OrganizeId=c.OrganizeId
for xml path('')),1,1,'') as ztmc,
syncstatus,max(sqsj) sqsj FROM (select OrganizeId,hzxm xm,yzh lissqdh,sqdh,ztId,ztmc,
case syncStatus when '2' then 'Chờ báo cáo' when '3' then 'Đã có báo cáo' when '4' then 'Đã hủy' else 'Chờ đăng ký' end syncStatus,CONVERT(varchar(100), a.CreateTime, 20) sqsj,row_number() over(partition by a.yzh order by a.CreateTime desc) rn  
from zy_lsyz a with(nolock)
where  yzlx='7' and a.zyh=@zymzh  and a.OrganizeId=@orgId and a.zt=1 and yzzt='2'";
                if (!string.IsNullOrEmpty(ztmc))
                {
                    sql += " and ztmc like @ztmc ";
                    par.Add(new SqlParameter("@ztmc", "%" + ztmc + "%"));
                }
                if (!string.IsNullOrEmpty(kssj))
                {
                    sql += " and CreateTime>=@kssj ";
                    par.Add(new SqlParameter("@kssj", kssj));
                }
                if (!string.IsNullOrEmpty(jssj))
                {
                    sql += " and CreateTime<=@jssj ";
                    par.Add(new SqlParameter("@jssj", jssj));
                }
                sql += ") c WHERE rn = 1 group by OrganizeId ,xm,lissqdh,sqdh,syncstatus order by sqsj desc";
            }

            par.Add(new SqlParameter("@zymzh", zymzh));
            par.Add(new SqlParameter("@orgId", orgId));

            return this.FindList<PacsReportSqdhValueVo>(sql, par.ToArray());

        }

        /// <summary>
        /// Bổ sungđẩyĐơn thuốc
        /// </summary>
        /// <param name="jzObject"></param>
        /// <param name="cfDto"></param>
        public void SyncToPDS(TreatmentEntity jzObject, List<PrescriptionDTO> cfDto, string operatorCode)
        {
            using (var db = new EFDbTransaction().BeginTrans())
            {
                List<string> updatedYpCfList = new List<string>();
                var addedYpCfList = new List<string>();
                //4.Đơn thuốc
                if (cfDto != null)
                {
                    var ypyfList = _baseDataDmnService.GetMediUsageList();
                    foreach (var cf in cfDto)
                    {
                        {
                            if (string.IsNullOrWhiteSpace(cf.cfh))
                            {
                                throw new FailedException("Đơn thuốcNgoại lệ，thiếuSố đơn thuốc");
                            }

                            DateTime? cfklrq = null;
                            string cfCreator = null;
                            string sqdh = null;
                            var needOutpatientBook = false;
                            var needOutpatientBookModify = false;
                            if (!string.IsNullOrWhiteSpace(cf.cfId))
                            {
                                //kiểm trabảngbảngtrongĐơn thuốccủaThu phíTrạng thái
                                const string sql4 = @"SELECT TOP 1 * FROM dbo.xt_cf(NOLOCK) WHERE cfId=@cfId";
                                var dbPresEnitty = db.FirstOrDefault<PrescriptionEntity>(sql4, new DbParameter[] { new SqlParameter("@cfId", cf.cfId) });
                                if (dbPresEnitty == null)
                                {
                                    throw new FailedException("Đơn thuốcNgoại lệ，Đơn thuốcdấuhiển thịcộtNgoại lệ");
                                }

                                cfklrq = dbPresEnitty.CreateTime;
                                cfCreator = dbPresEnitty.CreatorCode;
                                sqdh = dbPresEnitty.sqdh;

                                if (cf.cflx == (int)EnumCflx.WMPres || cf.cflx == (int)EnumCflx.TCMPres)
                                {
                                    updatedYpCfList.Add(cf.cfh);
                                    //needOutpatientBookModify = true;
                                    needOutpatientBook = true;
                                }
                            }
                            else
                            {
                                if (cf.cflx == (int)EnumCflx.WMPres || cf.cflx == (int)EnumCflx.TCMPres)
                                {
                                    addedYpCfList.Add(cf.cfh);
                                    needOutpatientBook = true;
                                }
                            }

                            //Đơn thuốcchi tiết,Thêm mới
                            var rpDetail = new List<PrescriptionDetailEntity>();
                            string sqlmx = @"select * from xt_cfmx with(nolock) where cfId=@cfid and zt='1'";
                            rpDetail = FindList<PrescriptionDetailEntity>(sqlmx, new SqlParameter[] { new SqlParameter("cfid", cf.cfId) });

                            #region gửiĐơn thuốcđếnpds

                            var t = "";
                            if (needOutpatientBook)
                            {
                                t = SendNewRpToPds(jzObject, cf.cfh, cf.cfId, operatorCode, rpDetail, ypyfList);
                            }
                            else if (needOutpatientBookModify)
                            {
                                t = UpdateRpToPds(jzObject, cf.cfh, cf.cfId, operatorCode, rpDetail, ypyfList);
                            }

                            if (!string.IsNullOrWhiteSpace(t)) throw new FailedException("syncRpToPdsError", t);

                            #endregion
                        }
                    }
                }
            }

        }
        /// <summary>
        /// LấyThu phímẫuMụcthông tin
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="sfmb"></param>
        /// <param name="sfmbmc"></param>
        /// <returns></returns>
        public List<PrescriptionDetailVO> Getsfmbxm(string orgId, string sfmb, string sfmbmc)
        {
            var sql = string.Format(@"select b.sfxm xmCode,isnull(c.sfxmmc,d.ypmc) xmmc,b.zll mczll,b.sl,c.dw,b.dj, 
convert(decimal(18,2),b.sl*b.dj) je,b.bw,b.zxks,a.sfmbbh ztId,a.sfmbmc ztmc 
from [NewtouchHIS_Sett].[dbo].[xt_sfmb] a with(nolock) 
left join [NewtouchHIS_Sett].[dbo].[xt_sfmbxm] b with(nolock) 
	on a.sfmbbh=b.sfmbbh and b.zt='1' and a.OrganizeId=b.OrganizeId 
left join NewtouchHIS_Base.[dbo].[xt_sfxm] c with(nolock) 
	on b.sfxm=c.sfxmCode and b.OrganizeId=c.OrganizeId and c.zt='1' 
left join NewtouchHIS_Base.[dbo].[xt_yp] d with(nolock) 
	on b.sfxm=d.ypCode and b.OrganizeId=d.OrganizeId and d.zt='1' 
where a.sfmb=@sfmb and sfmbmc=@sfmbmc and a.OrganizeId=@orgId ");

            var par = new List<SqlParameter>();
            par.Add(new SqlParameter("@sfmb", sfmb ?? ""));
            par.Add(new SqlParameter("@sfmbmc", sfmbmc ?? ""));
            par.Add(new SqlParameter("@orgId", orgId));

            return this.FindList<PrescriptionDetailVO>(sql, par.ToArray());
        }
        #endregion
        public TCMDjXMVO GetBindTCMDj(string OrganizeId)
        {
            var sfxmCode = _sysConfigRepo.GetValueByCode("TCM_DaiJianCode", OrganizeId);
            var sql = @"
SELECT *
  FROM[NewtouchHIS_Base]..[V_S_xt_sfxm]
WHERE zt = '1' and organizeId = @orgId and sfxmCode = @sfxmCode";
            return this.FirstOrDefault<TCMDjXMVO>(sql, new[] { new SqlParameter("@orgId", OrganizeId), new SqlParameter("@sfxmCode", sfxmCode) });

        }

        public List<OutPatientRegistrationInfoDTO> Getdjzlist(Pagination pagination, string orgid, string ksCode, string ysgh, string mjzbz, string jiuzhenbz)
        {

            var paraList = new List<SqlParameter>() { };
            paraList.Add(new SqlParameter("@orgId", orgid ?? ""));
            paraList.Add(new SqlParameter("@lastUpdateTime", ""));
            paraList.Add(new SqlParameter("@outpatientNumber", ""));
            paraList.Add(new SqlParameter("@ksCode", ksCode ?? ""));
            paraList.Add(new SqlParameter("@ysgh", ysgh ?? ""));
            paraList.Add(new SqlParameter("@mjzbz", mjzbz ?? ""));
            paraList.Add(new SqlParameter("@jiuzhenbz", jiuzhenbz ?? ""));
            paraList.Add(new SqlParameter("@keyword", "%%"));
            paraList.Add(new SqlParameter("@zzhz", "0"));
            //
            pagination = pagination ?? new Pagination();
            pagination.page = pagination.page <= 0 ? 1 : pagination.page;
            pagination.rows = pagination.rows <= 0 ? 100000 : pagination.rows;  //khôngphúttrang
            pagination.sidx = string.IsNullOrWhiteSpace(pagination.sidx) ? "UpdateTime" : pagination.sidx;

            paraList.Add(new SqlParameter("@page", pagination.page));
            paraList.Add(new SqlParameter("@rows", pagination.rows));
            paraList.Add(new SqlParameter("@sidx", pagination.sidx ?? ""));
            paraList.Add(new SqlParameter("@sord", pagination.sord ?? ""));

            var outParameter1 = new SqlParameter("@flag", System.Data.SqlDbType.VarChar, 10);
            outParameter1.Direction = ParameterDirection.Output;
            paraList.Add(outParameter1);
            var outParameter2 = new SqlParameter("@msg", System.Data.SqlDbType.VarChar, 128);
            outParameter2.Direction = ParameterDirection.Output;
            paraList.Add(outParameter2);
            //
            var outParameter3 = new SqlParameter("@records", System.Data.SqlDbType.Int);
            outParameter3.Direction = ParameterDirection.Output;
            paraList.Add(outParameter3);
            var outParameter4 = new SqlParameter("@total", System.Data.SqlDbType.Int);
            outParameter4.Direction = ParameterDirection.Output;
            paraList.Add(outParameter4);

            var list = this.FindList<OutPatientRegistrationInfoDTO>(@"exec NewtouchHIS_Sett..usp_interface_OutPatientRegistrationQuery @orgId, @lastUpdateTime
, @outpatientNumber, @ksCode, @ysgh, @mjzbz, @jiuzhenbz, @keyword,@zzhz
,@page ,@rows ,@sidx ,@sord
,@records out ,@flag out ,@msg out", paraList.ToArray());

            pagination.records = Convert.ToInt32(outParameter3.Value);

            return list;
        }

        //Thuốc、Hạng mục chẩn trị、Vật tư tiêu haothông tinTra cứu
        //
        public IList<MedicineInfoVO2> GetMedicineInfoList(Pagination pagination, string xmbm, string xmmc, string ck_kc, string orgId)
        {
            if (string.IsNullOrWhiteSpace(pagination.sidx))
            {
                throw new Exception("pagination.sidx is required");
            }

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@orgId", orgId),
                new SqlParameter("@xmbm", xmbm.Trim()),
                new SqlParameter("@xmmc", "%" + xmmc.Trim() + "%"),
                new SqlParameter("@lkc", ck_kc=="0"?0:1),
            };

            var sql = $"exec NewtouchHIS_herp.dbo.PROC_YPXXCX_YNHC @orgId,@xmbm,@xmmc,@lkc";

            if (string.IsNullOrEmpty(sql))
            {
                return new List<MedicineInfoVO2>();
            }

            var res = FindList<MedicineInfoVO2>(sql, parameters);
            return res;

            //            #region Thuốc
            //            var sql = @"SELECT convert(varchar(50),yp.ypId)  ypId,
            //        yp.ypCode ,
            //        yp.ypmc ,
            //        yp.ycmc ,
            //        sx.ypgg ,
            //        SUM(ISNULL(kc.kcsl, 0)) kcsl ,
            //        bm.Zcxh ,
            //        CASE bm.zt WHEN 1 THEN 'Bình thường' WHEN 0 THEN 'điều khiển' END syzt ,
            //		NewtouchHIS_PDS.dbo.f_getYfYpComplexYpSlandDw(SUM(ISNULL(kc.kcsl, 0)), fm.mzzybz, bm.Ypdm, bm.OrganizeId) klsl ,
            //        SUM(CONVERT(NUMERIC(11, 2),ISNULL(kc.kcsl, 0)/ yp.bzs)) YkKcsl ,
            //        yp.bzdw YkDw ,
            //       NewtouchHIS_PDS.dbo.f_getyfbmDw(bm.yfbmCode, bm.Ypdm, bm.OrganizeId) deptdw ,
            //       CONVERT(DECIMAL(11, 2),yp.lsj/yp.bzs*(CASE fm.mzzybz WHEN '0' THEN yp.bzs WHEN '1' THEN yp.mzcls WHEN '2' THEN yp.zycls WHEN '3' THEN yp.mzcls END)) lsj ,
            //        CONVERT(DECIMAL(11, 4),yp.pfj/yp.bzs*(CASE fm.mzzybz WHEN '0' THEN yp.bzs WHEN '1' THEN yp.mzcls WHEN '2' THEN yp.zycls WHEN '3' THEN yp.mzcls END)) pfj ,
            //        jx.jxmc ,
            //        bm.Ypkw ,
            //        bm.Pxfs1 ,
            //        bm.Pxfs2 ,
            //        bm.Kcsx ,
            //        bm.Kcxx ,
            //        bm.Jhd ,
            //        bm.Jhl ,
            //        RTRIM(LTRIM(dl.dlmc)) AS yplb ,
            //        CASE yp.zt WHEN '1' THEN 'Bình thường' WHEN '0' THEN 'Vô hiệu' END ypzt ,
            //        bm.Sysx,fm.yfbmCode,fm.yfbmmc
            //FROM    NewtouchHIS_PDS.dbo.XT_YP_BMYPXX(nolock) bm
            //        INNER JOIN [NewtouchHIS_Base].dbo.V_S_xt_yp yp ON yp.ypCode = bm.Ypdm and yp.OrganizeId=bm.OrganizeId and yp.mzzybz='1' and yp.isYnss='1' 
            //        INNER JOIN [NewtouchHIS_Base].dbo.V_S_xt_ypsx sx ON sx.ypId = yp.ypId and sx.OrganizeId=bm.OrganizeId
            //        INNER JOIN [NewtouchHIS_Base]..V_S_xt_yfbm fm ON fm.yfbmCode = bm.yfbmCode and fm.OrganizeId=bm.OrganizeId and fm.zt='1'
            //        LEFT  JOIN NewtouchHIS_PDS.dbo.XT_YP_KCXX(nolock) kc ON kc.yfbmCode = bm.yfbmCode AND kc.Ypdm = bm.Ypdm AND kc.OrganizeId=bm.OrganizeId
            //        LEFT JOIN [NewtouchHIS_Base]..V_S_xt_sfdl dl ON dl.dlcode = yp.dlcode and dl.OrganizeId=bm.OrganizeId and dl.zt='1'
            //        LEFT  JOIN [NewtouchHIS_Base].dbo.V_S_xt_ypjx jx ON jx.jxCode = yp.jx";

            //            var pars = new List<SqlParameter>();

            //            sql += " WHERE bm.OrganizeId=@orgId and yp.dlCode in ('01','02','03')";
            //            #endregion

            //            #region Hạng mục chẩn trị
            //            var sql2 = @"SELECT convert(varchar(50),a.sfxmId) ypId,  
            // sfxmCode ypCode, sfxmmc ypmc,'' ycmc,
            // gg ypgg,
            // 0 kcsl,
            // '' Zcxh,
            // '' syzt ,
            //'' klsl,
            //0.0 YkKcsl,
            // a.dw YkDw,
            // '' deptdw,
            // a.dj lsj,
            // a.dj pfj,
            // '' jxmc,
            // '' Ypkw,
            // '' Pxfs1,
            // '' Pxfs2,
            // 0 Kcsx,
            // 0 Kcxx,
            // 0 Jhd,
            //0 Jhl,
            // RTRIM(LTRIM(dl.dlmc)) AS yplb ,
            // CASE a.zt WHEN '1' THEN 'Bình thường' WHEN '0' THEN 'Vô hiệu' END ypzt ,
            // 0 Sysx,
            // '' yfbmCode,
            //'' yfbmmc
            // FROM [NewtouchHIS_Base]..V_S_xt_sfxm(NOLOCK) a  
            // INNER JOIN [NewtouchHIS_Base]..V_S_xt_sfdl dl ON dl.dlcode = a.sfdlCode and dl.OrganizeId=a.OrganizeId and dl.zt='1'
            // and a.sfdlCode not in ('01','02','03') and a.isYnss='1' ";

            //            var pars2 = new List<SqlParameter>();

            //            #endregion

            //            #region Vật tư tiêu hao
            //            var sql3 = @"SELECT wz.Id ypId,
            //wz.productCode ypCode,
            //wz.name ypmc,
            // cj.name ycmc,
            //wz.gg ypgg,
            //ISNULL(SUM(kcxx.kcsl),0)  kcsl,
            //'' Zcxh,
            //CASE wz.zt WHEN 1 THEN 'Bình thường' WHEN 0 THEN 'điều khiển' END syzt ,
            //NewtouchHIS_herp.dbo.f_getComplexWzSlandDw(ISNULL(SUM(kcxx.kcsl-kcxx.djsl),0), kcxx.zhyz,bmdw.name, zxdw.name) klsl,
            //0.0 YkKcsl,
            //rpu.unit YkDw,
            //rpu.unit deptdw,
            //ISNULL(CONVERT(NUMERIC(11,4),wz.lsj*rpu.zhyz),0) lsj,
            //CONVERT(NUMERIC(11,2),SUM(ISNULL(CONVERT(NUMERIC(11,4),kcxx.jj/kcxx.zhyz*kcxx.kcsl),0))) pfj,
            //'' jxmc,
            //'' Ypkw,
            //'' Pxfs1,
            //'' Pxfs2,
            //0 Kcsx,
            //0 Kcxx,
            //0 Jhd,
            //0 Jhl,
            //lb.name yplb, 
            //CASE rpw.zt WHEN '1' THEN 'Bình thường' WHEN '0' THEN 'Vô hiệu' END ypzt ,
            //0 Sysx,
            // '' yfbmCode,
            //rpw.warehouseNAME yfbmmc FROM NewtouchHIS_herp..rel_productWarehouse(NOLOCK) rpw
            //    INNER JOIN NewtouchHIS_herp.dbo.wz_product(NOLOCK) wz ON wz.Id=rpw.productId AND wz.OrganizeId=rpw.OrganizeId and wz.isYnss='1'
            //    LEFT JOIN NewtouchHIS_herp.dbo.rel_productUnit(NOLOCK) rpu ON rpu.productId=rpw.productId AND rpu.unitId=rpw.unitId AND rpu.zt='1'
            //    LEFT JOIN NewtouchHIS_herp.dbo.kf_kcxx(NOLOCK) kcxx ON kcxx.productId=rpw.productId AND kcxx.warehouseId=rpw.warehouseId AND kcxx.OrganizeId=rpw.OrganizeId
            //LEFT JOIN NewtouchHIS_Base.dbo.wz_unit(NOLOCK) bmdw ON bmdw.Id=rpu.unitId AND bmdw.zt='1'
            //LEFT JOIN NewtouchHIS_Base.dbo.wz_unit(NOLOCK) zxdw ON zxdw.Id=wz.minUnit AND zxdw.zt='1'
            //LEFT JOIN NewtouchHIS_Base.dbo.wz_type(NOLOCK) lb ON lb.Id=wz.typeId AND lb.zt='1'
            //LEFT JOIN NewtouchHIS_herp.dbo.gys_supplier(NOLOCK) cj ON cj.Id=wz.supplierId AND cj.OrganizeId=rpw.OrganizeId AND cj.zt='1'
            //where rpw.OrganizeId=@orgId ";

            //            #endregion
            //            var pars3 = new List<SqlParameter>();
            //            if (!string.IsNullOrWhiteSpace(xmbm.Trim()))
            //            {
            //                sql += " and yp.ypCode=@xmbm";
            //                sql2 += " and sfxmCode=@xmbm";
            //                sql3 += " and wz.productCode=@xmbm";

            //            }
            //            if (!string.IsNullOrWhiteSpace(xmmc.Trim()))
            //            {
            //                sql += " and yp.ypmc like @xmmc";
            //                sql2 += " and sfxmmc like @xmmc";
            //                sql3 += " and wz.name like @xmmc";
            //            }

            //            sql += " GROUP BY yp.ypId ,sx.ypgg ,bm.Zcxh ,bm.zt,bm.yfbmCode,bm.OrganizeId,bm.Ypdm,bm.Ypkw ,bm.Pxfs1 ,bm.Pxfs2 ,bm.Kcsx ,bm.Kcxx ,bm.Jhd ,bm.Jhl ,bm.Sysx,fm.mzzybz,yp.bzs,yp.mzcls,yp.zycls,yp.bzdw,yp.lsj,yp.pfj,yp.ypCode ,yp.ypmc ,yp.ycmc,yp.zt,jx.jxmc,dl.dlmc,fm.yfbmmc,fm.yfbmCode,fm.yfbmCode,fm.yfbmmc";
            //            sql3 += " GROUP BY wz.Id,wz.name,wz.py,wz.lsj,rpu.zhyz,wz.gg,wz.brand,rpw.zt,kcxx.kcsl,wz.productCode,rpu.unit,rpw.warehouseNAME,bmdw.name, zxdw.name,kcxx.zhyz,lb.name, cj.name,wz.zt";
            //            if (!string.IsNullOrWhiteSpace(ck_kc) && "0".Equals(ck_kc))
            //            {
            //                sql += " having SUM(ISNULL(kc.kcsl, 0))>0";
            //                sql3 += " having SUM(ISNULL(kcxx.kcsl, 0))>0";
            //            }
            //            switch (zxdlb)
            //            {
            //                case "ynyp":
            //                    if (!string.IsNullOrWhiteSpace(xmbm.Trim())) pars.Add(new SqlParameter("@xmbm", xmbm.Trim()));
            //                    pars.Add(new SqlParameter("@orgId", orgId));
            //                    if (!string.IsNullOrWhiteSpace(xmmc.Trim())) pars.Add(new SqlParameter("@xmmc", "%" + xmmc.Trim() + "%"));
            //                    return this.QueryWithPage<MedicineInfoVO2>(sql, pagination, pars.ToArray());
            //                case "zlxm":
            //                    pars2.Add(new SqlParameter("@orgId", orgId));
            //                    if (!string.IsNullOrWhiteSpace(xmbm.Trim())) pars2.Add(new SqlParameter("@xmbm", xmbm.Trim()));
            //                    if (!string.IsNullOrWhiteSpace(xmmc.Trim())) pars2.Add(new SqlParameter("@xmmc", "%" + xmmc.Trim() + "%"));
            //                    return this.QueryWithPage<MedicineInfoVO2>(sql2, pagination, pars2.ToArray());
            //                case "ynhc":
            //                    pars3.Add(new SqlParameter("@orgId", orgId));
            //                    if (!string.IsNullOrWhiteSpace(xmbm.Trim())) pars3.Add(new SqlParameter("@xmbm", xmbm.Trim()));
            //                    if (!string.IsNullOrWhiteSpace(xmmc.Trim())) pars3.Add(new SqlParameter("@xmmc", "%" + xmmc.Trim() + "%"));
            //                    return this.QueryWithPage<MedicineInfoVO2>(sql3, pagination, pars3.ToArray());
            //                default:
            //                    var sql4 = sql + " union all " + sql2 + " union all " + sql3;
            //                    var pars4 = new List<SqlParameter>() { new SqlParameter("@orgId", orgId) };
            //                    if (!string.IsNullOrWhiteSpace(xmbm.Trim())) pars4.Add(new SqlParameter("@xmbm", xmbm.Trim()));
            //                    if (!string.IsNullOrWhiteSpace(xmmc.Trim())) pars4.Add(new SqlParameter("@xmmc", "%" + xmmc.Trim() + "%"));
            //                    return this.QueryWithPage<MedicineInfoVO2>(sql4, pagination, pars4.ToArray());

            //            }

        }

        /// <summary>
        /// Theođăng ký khámtrongmãLấyLIS/PACSbáo cáoHoàn thànhSố lượng
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="yzh"></param>
        /// <returns></returns>
        public int CountLISztmz(string orgId, string jzId)
        {
            string sql = @" 
 select count(*)  from  [NewtouchHIS_Sett].dbo.mz_cf mzcf
 left join [Newtouch_CIS].dbo.xt_cf xtcf
 on mzcf.cfh=xtcf.cfh and mzcf.organizeId=xtcf.organizeId and mzcf.zt=xtcf.zt
 where  jzId=@jzId
 and mzcf.zt=1
 and sqdzt=2";

            var sqlpar = new List<SqlParameter>();
            sqlpar.Add(new SqlParameter("@jzId", jzId));
            sqlpar.Add(new SqlParameter("@orgId", orgId));
            return this.FirstOrDefault<int>(sql, sqlpar.ToArray());
        }

    }
}
