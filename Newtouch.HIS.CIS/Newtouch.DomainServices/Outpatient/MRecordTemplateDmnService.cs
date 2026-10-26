using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Domain.BusinessObjects;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;
using Newtouch.Domain.ValueObjects;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using Newtouch.Domain.Entity.Outpatient;

namespace Newtouch.DomainServices
{
    public class MRecordTemplateDmnService : DmnServiceBase, IMRecordTemplateDmnService
    {
        private readonly IMRTemplateRepo _mRTemplateRepo;
        private readonly IMRTemplateWMDiagnosisRepo _mrTemplateWMDiagnosisRepo;
        private readonly IMRTemplateTCMDiagnosisRepo _mrTemplateTCMDiagnosisRepo;
        private readonly ICfTemplateGroupPackageRepo _cfTemplateGroupPackageRepo;
        public MRecordTemplateDmnService(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {

        }
        public List<MRTemplateListBO> SelectTemplateList(int mblx, string orgId, string deptCode,string userCode)
        {
            var par = new List<SqlParameter>();
            par.Add(new SqlParameter("@mblx", mblx));
            par.Add(new SqlParameter("@orgId", orgId));
            string mbSql = @"select mbId,mbmc,CreateTime,LastModifyTime 
                          from[dbo].[xt_blmb]";
            if (mblx == (int)EnumCfMbLx.personal)
            {
                mbSql += @" where zt='1' and mblx=@mblx and CreatorCode=@userCode and OrganizeId=@orgId 
                                  order by CreateTime desc ";
                par.Add(new SqlParameter("@userCode", userCode));
            }
            else if (mblx == (int)EnumCfMbLx.department)
            {
                mbSql = @" select mbId,mbmc,CreateTime,LastModifyTime 
 from [xt_blmb] blmb
 where blmb.zt='1'  and mblx=@mblx  and  blmb.ks=@deptCode and blmb.OrganizeId=@orgId 
       order by blmb.CreateTime desc";
                par.Add(new SqlParameter("@deptCode", deptCode));
            }
            else {
                mbSql += @" where zt='1' and mblx=@mblx  and OrganizeId=@orgId
                            order by CreateTime desc";
            }
            return this.FindList<MRTemplateListBO>(mbSql, par.ToArray());
        }

        /// <summary>
        /// Tra cứu chi tiết mẫu
        /// </summary>
        /// <param name="mbId"></param>
        public MRTemplateBO SelectTemplateDetailByMbId(string mbId, string orgId)
        {
            //Bệnh án
            var mbEntity = _mRTemplateRepo.IQueryable().Where(a => a.mbId == mbId && a.zt == "1").FirstOrDefault();
            if (mbEntity == null)
            {
                throw new FailedException("数据异常，未查询出病历模板信息");
            }
            //Chẩn đoán Tây y
            //var wmdiagnosisEntityList = _mrTemplateWMDiagnosisRepo.IQueryable().Where(a => a.mbId == mbId && a.OrganizeId == orgId && a.zt == "1").ToList();
            //Thêm cid10 lixin 20181016
            var xysql = @"  SELECT a.zdCode,a.zdmc,a.zdlx,a.ysbz,b.icd10 FROM Newtouch_CIS.dbo.xt_blmbxyzd a  LEFT JOIN[NewtouchHIS_Base].[dbo].[V_S_xt_zd] b ON a.zdCode = b.zdCode AND (a.OrganizeId=b.OrganizeId OR b.OrganizeId='*')  AND a.zt = '1' AND b.zt = '1' WHERE a.mbId = @mbId AND a.OrganizeId = @orgId ";
            var wmdiagnosisEntityList = this.FindList<WMDiagnosisHtmlVO>(xysql, new[] { new System.Data.SqlClient.SqlParameter("@mbId", mbId), new System.Data.SqlClient.SqlParameter("@orgId", orgId) });
            List<WMDiagnosisHtmlVO> xyzdList = new List<WMDiagnosisHtmlVO>();
            if (wmdiagnosisEntityList.Count > 0)
            {
                foreach (var item in wmdiagnosisEntityList)
                {
                    WMDiagnosisHtmlVO zdVo = new WMDiagnosisHtmlVO();
                    zdVo.zdCode = item.zdCode;
                    zdVo.zdmc = item.zdmc;
                    zdVo.ysbz = item.ysbz;
                    zdVo.zdlx = item.zdlx;
                    zdVo.icd10 = item.icd10;
                    xyzdList.Add(zdVo);
                }
            }
            //Chẩn đoán Đông y
            //var tcmdiagnosisEntityList = _mrTemplateTCMDiagnosisRepo.IQueryable().Where(a => a.mbId == mbId && a.OrganizeId == orgId && a.zt == "1").ToList();
            var zysql = @"  SELECT a.zdCode,a.zdmc,a.zdlx,a.ysbz,a.zhCode,a.zhmc, b.icd10 FROM Newtouch_CIS.dbo.xt_blmbzyzd a  LEFT JOIN[NewtouchHIS_Base].[dbo].[V_S_xt_zd] b ON a.zdCode = b.zdCode AND (a.OrganizeId=b.OrganizeId OR b.OrganizeId='*')  AND a.zt = '1' AND b.zt = '1' WHERE a.mbId = @mbId AND a.OrganizeId = @orgId ";
            var tcmdiagnosisEntityList = this.FindList<TCMDiagnosisHtmlVO>(zysql, new[] { new System.Data.SqlClient.SqlParameter("@mbId", mbId), new System.Data.SqlClient.SqlParameter("@orgId", orgId) });

            List<TCMDiagnosisHtmlVO> zyzdList = new List<TCMDiagnosisHtmlVO>();
            if (tcmdiagnosisEntityList.Count > 0)
            {
                foreach (var item in tcmdiagnosisEntityList)
                {
                    TCMDiagnosisHtmlVO zdVo = new TCMDiagnosisHtmlVO();
                    zdVo.zdCode = item.zdCode;
                    zdVo.zdmc = item.zdmc;
                    zdVo.ysbz = item.ysbz;
                    zdVo.zdlx = item.zdlx;
                    zdVo.zhCode = item.zhCode;
                    zdVo.zhmc = item.zhmc;
                    zdVo.icd10 = item.icd10;
                    zyzdList.Add(zdVo);
                }
            }
            var mbztList = _cfTemplateGroupPackageRepo.IQueryable().Where(a => a.mbId == mbId && a.OrganizeId == orgId && a.zt == "1").OrderBy(a => a.CreateTime).ThenBy(a => a.cflx).ToList();
            foreach (var item in mbztList) {
                var NztId = item.ztId;
                var Nztmc= item.ztmc;
                var NmbId= item.mbId;
                var Nmbmc= item.mbmc;
                item.mbId = NztId;
                item.mbmc = Nztmc;
                item.ztId = NmbId;
                item.ztmc = Nmbmc;
            }
            //var mbztList =_cfTemplateGroupPackageRepo.IQueryable().Where(a => a.mbId == mbId && a.OrganizeId == orgId && a.zt == "1").Select(a => new CfTemplateGroupPackageEntity { mbztId=a.mbztId, OrganizeId=a.OrganizeId, ztId=a.mbId, ztmc=a.mbmc, mbId=a.ztId, mbmc=a.ztmc, CreateTime=a.CreateTime, CreatorCode=a.CreatorCode, LastModifyTime=a.LastModifyTime, LastModifierCode=a.LastModifierCode, zt=a.zt,cflx=a.cflx }).OrderBy(a => a.cflx).ThenByDescending(a => a.CreateTime).ToList();
            var bo = new MRTemplateBO()
            {
                //Mẫu
                mbmc = mbEntity.mbmc,
                zs = mbEntity.zs,
                xbs = mbEntity.xbs,
                jws = mbEntity.jws,
                ct = mbEntity.ct,
                clfa = mbEntity.clfa,
                yjs = mbEntity.yjs,
                gms = mbEntity.gms,
                hy = mbEntity.hy,
                //Chẩn đoán Tây y
                xyzdList = xyzdList,
                //Chẩn đoán Đông y
                zyzdList = zyzdList,
                mbztList = mbztList
            };
            return bo;

        }

        /// <summary>
        /// Lưu
        /// </summary>
        /// <param name="blmbObject"></param>
        /// <param name="zdList"></param>
        public void SaveData(MRTemplateEntity blmbObject, List<WMDiagnosisHtmlVO> xyzdList, List<TCMDiagnosisHtmlVO> zyzdList,List<CfTemplateGroupPackageEntity> mbztList)
        {
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                if (!string.IsNullOrWhiteSpace(blmbObject.mbId))  //Sửa đổi
                {
                    blmbObject.Modify();
                    blmbObject.zt = "1";
                    db.Update(blmbObject);

                    db.Delete<MRTemplateWMDiagnosisEntity>(a => a.mbId == blmbObject.mbId && a.OrganizeId == blmbObject.OrganizeId);
                    db.Delete<MRTemplateTCMDiagnosisEntity>(a => a.mbId == blmbObject.mbId && a.OrganizeId == blmbObject.OrganizeId);
                    db.Delete<CfTemplateGroupPackageEntity>(a =>
                        a.mbId == blmbObject.mbId && a.OrganizeId == blmbObject.OrganizeId);
                }
                else
                {
                    //Thêm mới bảng khám bệnh
                    blmbObject.mbId = Guid.NewGuid().ToString();
                    blmbObject.Create();

                    db.Insert(blmbObject);
                }

                if (xyzdList != null)
                {
                    foreach (var item in xyzdList)
                    {
                        MRTemplateWMDiagnosisEntity diagnosisEntity = new MRTemplateWMDiagnosisEntity();
                        diagnosisEntity.Id = Guid.NewGuid().ToString();
                        diagnosisEntity.OrganizeId = blmbObject.OrganizeId;
                        diagnosisEntity.mbId = blmbObject.mbId;
                        diagnosisEntity.zdlx = item.zdlx;
                        diagnosisEntity.zdCode = item.zdCode;
                        diagnosisEntity.zdmc = item.zdmc;
                        diagnosisEntity.ysbz = item.ysbz;
                        diagnosisEntity.Create();

                        db.Insert(diagnosisEntity);
                    }
                }

                if (zyzdList != null)
                {
                    foreach (var item in zyzdList)
                    {
                        MRTemplateTCMDiagnosisEntity diagnosisEntity = new MRTemplateTCMDiagnosisEntity();
                        diagnosisEntity.Id = Guid.NewGuid().ToString();
                        diagnosisEntity.OrganizeId = blmbObject.OrganizeId;
                        diagnosisEntity.mbId = blmbObject.mbId;
                        diagnosisEntity.zdlx = item.zdlx;
                        diagnosisEntity.zdCode = item.zdCode;
                        diagnosisEntity.zdmc = item.zdmc;
                        diagnosisEntity.ysbz = item.ysbz;
                        diagnosisEntity.zhCode = item.zhCode;
                        diagnosisEntity.zhmc = item.zhmc;
                        diagnosisEntity.Create();

                        db.Insert(diagnosisEntity);
                    }
                }
                if (mbztList != null)
                {
                    foreach (var item in mbztList)
                    {
                        CfTemplateGroupPackageEntity mbzt = new CfTemplateGroupPackageEntity();
                        mbzt.mbztId = Guid.NewGuid().ToString();
                        mbzt.OrganizeId = blmbObject.OrganizeId;
                        mbzt.mbId = blmbObject.mbId;
                        mbzt.mbmc = blmbObject.mbmc;
                        mbzt.ztId = item.mbId;
                        mbzt.ztmc = item.mbmc;
                        mbzt.cflx = item.cflx;
                        mbzt.Create();
                        db.Insert(mbzt);
                    }
                }
                db.Commit();
            }
        }

        /// <summary>
        /// Xóa mẫu
        /// </summary>
        /// <param name="mbId"></param>
        public void DeleteTemplate(string mbId)
        {
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                var mbEntity = db.IQueryable<MRTemplateEntity>(p => p.mbId == mbId).FirstOrDefault();
                if (mbEntity != null)
                {
                    var mbxyzdEntityList = db.IQueryable<MRTemplateWMDiagnosisEntity>(p => p.mbId == mbId).ToList();
                    foreach (var ent in mbxyzdEntityList)
                    {
                        ent.zt = "0";
                        ent.Modify();
                        db.Update(ent);
                    }

                    var mbzyzdEntityList = db.IQueryable<MRTemplateTCMDiagnosisEntity>(p => p.mbId == mbId).ToList();
                    foreach (var ent in mbzyzdEntityList)
                    {
                        ent.zt = "0";
                        ent.Modify();
                        db.Update(ent);
                    }

                    mbEntity.zt = "0";
                    mbEntity.Modify();
                    db.Update(mbEntity);

                    db.Commit();
                }
            }
        }
    }
}
