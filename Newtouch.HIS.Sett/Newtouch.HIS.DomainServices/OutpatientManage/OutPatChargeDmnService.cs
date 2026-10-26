/**********************************************************/
// Copyright (C) 2016 Newtouch Giữ bản quyền. 
// Description: Đ ăng ký khám ngoại trú thu phí
// Author：HLF
// CreateDate： 2016/12/23 11:12:44 
//**********************************************************/

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Common;
using Newtouch.Common.Operator;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.BusinessObjects;
using Newtouch.HIS.Domain.DTO.InputDto;
using Newtouch.HIS.Domain.DTO.InputDto.OutpatientAccounting;
using Newtouch.HIS.Domain.DTO.OutputDto.OutpatientManage;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.ValueObjects.OutpatientManage;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using DbParameter = System.Data.Common.DbParameter;

namespace Newtouch.HIS.DomainServices
{
    public class OutPatChargeDmnService : DmnServiceBase, IOutPatChargeDmnService
    {

        private readonly ISysConfigRepo _sysConfigRepo; //Hệ thống ngoại trú cấu hình
        private readonly ISysPatientChargeAdditionalRepo _sysPatChargeAddRepo; // phí dịch vụ 
        private readonly IFinancialInvoiceRepo _finInvoicRepo; //tài chính hóa đ ơn 
        private readonly ISysPatientChargeAlgorithmRepo _sysPatiAlgorithmRepo; //Hệ thốngBệnh nhânThu phí
        private readonly IOutpatientRegistRepo _outpatientRegistRepo;
        private readonly IOutPatientUniversalDmnService _outPatientUniversalDmnService;
        private readonly IOutpatientPrescriptionRepo _outpatientPrescriptionRepo;

        public OutPatChargeDmnService(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// LấyĐ ăng ký khámthu phí giờ thông tin bệnh nhân
        /// </summary>
        /// <param name="kh">Số thẻ</param>
        /// <param name="brxz">Tính chất bệnh nhân</param>
        /// <returns></returns>
        public OutPatChargeInfoVO GetChargePatInfo(string kh, string brxz)
        {
            var strSql = new StringBuilder(@"
SELECT A.patid,a.blh,
A.xm,A.xb,A.csny,A.dz,A.pzh,A.ylxm,A.pzksrq,A.pzzzrq,A.pzzd,A.zjh,A.zjlx,
A.zh,A.zt,A.qxdm,A.dwmc,A.zjh,
B.brxzbh,B.brxz,B.brxzmc,B.ybjylx,
a.dybh,d.ghnm,d.kh,d.jzxh,d.mzh,d.fzbz
FROM dbo.mz_gh d
LEFT JOIN [dbo].[xt_brjbxx] AS A WITH(NOLOCK) ON d.patid = a.patid and A.OrganizeId=@OrganizeId
LEFT JOIN [dbo].[xt_brxz] as B WITH(NOLOCK) on d.brxz=B.brxz and B.OrganizeId=@OrganizeId
WHERE d.kh=@kh and b.brxz=@brxz
and d.CreateTime>@date and d.OrganizeId=@OrganizeId
ORDER BY mzh desc
                            ");
            SqlParameter[] par =
            {
                new SqlParameter("@date", DateTime.Now.Date),
                new SqlParameter("@kh", kh ?? ""),
                new SqlParameter("@brxz", brxz ?? ""),
                new SqlParameter("@OrganizeId", OperatorProvider.GetCurrent().OrganizeId)

            };
            var patChargeVo = FindList<OutPatChargeInfoVO>(strSql.ToString(), par).FirstOrDefault();
            if (patChargeVo == null)
            {
                throw new FailedCodeException("OUTPAT_REGIST_ISINVALID");
            }
            return patChargeVo;
        }


        /// <summary>
        /// nhận đ ược khi trư ớc bệnh nh ânĐ ăng ký khámkhoa
        /// </summary>
        /// <param name="patid">Bệnh nhân</param>
        /// <param name="startDate">Ngày bắt đ ầu</param>
        /// <param name="endDate">Ngày kết thúc</param>
        /// <returns></returns>
        public List<OutPatChargeItemVO> getOutPatItem_ghlx(int patid, string brxz, string startDate, string endDate)
        {
            //Cấu hình hệ thống
            var confEntity = _sysConfigRepo.GetByCode(Constants.xtmzpz.SFXM_GHGHF, OperatorProvider.GetCurrent().OrganizeId);
            var strSql = new StringBuilder(@"
select a.ghnm,a.ks,a.ys,c.mjzbz,c.dbxm,c.fzbz,c.CreateTime,
b.sfxmmc+'＞'+f.Name+case 
when a.ys='' and c.ghzbbh=0 then '' 
when a.ys='' and c.ghzbbh<>0 then g.ghzbmc 
else d.Name end as  gh_ks 
from mz_ghxm a 
inner join NewtouchHIS_Base..V_S_xt_sfxm b on a.sfxm=b.sfxmCode and b.OrganizeId=@OrganizeId
inner join mz_gh c on a.ghnm=c.ghnm and c.OrganizeId=@OrganizeId
left join [NewtouchHIS_Base]..V_S_Sys_Staff d on a.ys=d.gh and d.OrganizeId=@OrganizeId
inner join [NewtouchHIS_Base]..V_S_Sys_Department f on a.ks=f.Code and f.OrganizeId=@OrganizeId
left join xt_ghzb g on c.ghzb=g.ghzb and g.OrganizeId=@OrganizeId
where a.patid=@patid and c.brxz=@brxz
and a.dl=@dl
and c.CreateTime between @startDate and @endDate
and not exists(select ghnm from mz_ghth y where a.ghnm=y.ghnm)
and a.OrganizeId=@OrganizeId
                            ");
            var par = new DbParameter[]
            {
                new SqlParameter("@patid", patid),
                new SqlParameter("@brxz", brxz),
                new SqlParameter("@dl", confEntity.Value),
                new SqlParameter("@startDate", startDate),
                new SqlParameter("@endDate", endDate),
                new SqlParameter("@OrganizeId", OperatorProvider.GetCurrent().OrganizeId)
            };
            return FindList<OutPatChargeItemVO>(strSql.ToString(), par).ToList();
        }

        /// <summary>
        /// nhận đ ược khi trư ớc bệnh nh ânĐ ăng ký khámkhoa
        /// </summary>
        /// <param name="patid">Bệnh nhân</param>
        /// <param name="brxz"></param>
        /// <param name="startDate">Ngày bắt đ ầu</param>
        /// <param name="endDate">Ngày kết thúc</param>
        public List<OutPatChargeItemVO> GetOutPatGhlx(int patid, string brxz, string startDate, string endDate)
        {
            //Cấu hình hệ thống
            var confEntity = _sysConfigRepo.GetByCode(Constants.xtmzpz.SFXM_GHGHF, OperatorProvider.GetCurrent().OrganizeId);
            var strSql = new StringBuilder(@"
SELECT xm.ghnm, xm.ks, xm.ys, c.mjzbz, c.dbxm, c.fzbz, c.CreateTime, 
b.sfxmmc+'＞'+f.Name+case 
when xm.ys='' and c.ghzbbh=0 then '' 
when xm.ys='' and c.ghzbbh<>0 then g.ghzbmc 
else d.Name end as  gh_ks 
FROM dbo.mz_xm(NOLOCK) xm
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_sfxm b ON xm.sfxm=b.sfxmCode AND b.OrganizeId=xm.OrganizeId
INNER JOIN dbo.mz_gh(NOLOCK) c ON c.ghnm=xm.ghnm AND c.OrganizeId=xm.OrganizeId
LEFT JOIN dbo.xt_ghzb(NOLOCK) g ON g.ghzb=c.ghzb AND g.OrganizeId=xm.OrganizeId
LEFT JOIN NewtouchHIS_Base.dbo.V_S_Sys_Staff d ON xm.ys=d.gh AND d.OrganizeId=xm.OrganizeId
INNER JOIN NewtouchHIS_Base.dbo.V_S_Sys_Department f ON f.Code=xm.ks AND f.OrganizeId=xm.OrganizeId
WHERE xm.OrganizeId=@OrganizeId
AND xm.patid=@patid
AND c.brxz=@brxz
AND xm.dl=@dl
AND c.CreateTime BETWEEN @startDate AND @endDate
AND NOT EXISTS(SELECT 1 FROM dbo.mz_ghth(NOLOCK) y WHERE xm.ghnm=y.ghnm)

");
            var par = new DbParameter[]
            {
                new SqlParameter("@patid", patid),
                new SqlParameter("@brxz", brxz),
                new SqlParameter("@dl", confEntity.Value),
                new SqlParameter("@startDate", startDate),
                new SqlParameter("@endDate", endDate),
                new SqlParameter("@OrganizeId", OperatorProvider.GetCurrent().OrganizeId)
            };
            return FindList<OutPatChargeItemVO>(strSql.ToString(), par).ToList();
        }

        /// <summary>
        /// Lấykhi trư ớc KhoaĐ ăng ký khám thời gian có hiệu lực hay không
        /// </summary>
        /// <param name="ks">Khoa</param>
        /// <param name="mjzbz"> cửa cấp cứu dấu hiệu </param>
        /// <param name="parmJdrq">Khoa tạo ngày </param>
        /// <returns></returns>
        public bool getActiveDuration(string ks, string mjzbz, object parmJdrq)
        {
            string jdrq = Convert.ToDateTime(parmJdrq).ToString("yyyy-MM-dd HH:mm:ss");
            bool res = false;
            var mzActiveDuration = _sysConfigRepo.GetByCode(Infrastructure.Constants.xtmzpz.MZ_ACTIVE_DURATION,
                OperatorProvider.GetCurrent().OrganizeId);
            if (mzActiveDuration.Value == null)
            {
                throw new FailedCodeException("OUTPAT_TIMECONFIG_ERROR");
            }
            else
            {
                List<MzActiveDurationVO> durationList = Json.ToObject<List<MzActiveDurationVO>>(mzActiveDuration.Value);
                var entity = durationList.Find(a => a.ks == ks);
                if (entity == null)
                {
                    entity = durationList.Find(t => t.ks == "-1"); //ks(khoa):"-1"Làký tự đại diện,khớp tất cả các khoa
                }
                if (entity == null)
                {
                    throw new FailedCodeException("OUTPAT_TIMECONFIGURE_EFFECTIVELY");
                }
                int duration = -1;
                if (mjzbz == "2") //Cấp cứu
                {
                    duration = entity.jz;
                }
                else
                {
                    duration = entity.mz;
                }
                if (duration == -1) //KhiNgàyHiệu lực
                {
                    //KhiNgàyHiệu lực
                    string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
                    string isYx = jdrq.Split(' ')[0].ToString();
                    if (currentDate.Equals(isYx))
                    {
                        res = true;
                    }
                }
                else
                {
                    // một đoạn thời gian trong hiệu lực
                    DateTime currentDate = DateTime.Now;
                    DateTime isYx = Convert.ToDateTime(jdrq).AddHours(duration);

                    if (currentDate < isYx)
                    {
                        res = true;
                    }
                }
            }

            return res;
        }


        /// <summary>
        /// LấyĐ ăng ký khám bác sĩ thông tinghnm, "", 3, mjzbz, Isyzybzz, Istxzzys
        /// </summary>
        /// <param name="ghnm">Đ ăng ký khám mã nội bộ</param>
        /// <param name="parmKs">Khoa</param>
        /// <param name="type"></param>
        /// <param name="mjzbz"> cửa cấp cứu dấu hiệu </param>
        /// <param name="Isyzybzz">Có hay không xác minh bác sĩ, khoaBHYTtư cách</param>
        /// <param name="Istxzzys">Có hay không xác minh làlọc máubác sĩ(bệnh nặngcần)</param>
        /// <returns></returns>
        public OutPatChargeDoctorVO getDoctorInfo(int ghnm, string parmKs, int type, string mjzbz, bool Isyzybzz,
            bool Istxzzys)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
                            select a.Name rymc,a.gh,a.gh ry,d.Name ksmc,d.Code ks,0 ksbh
                            from mz_gh b
                            inner join [NewtouchHIS_Base]..V_S_Sys_Staff a on a.gh=b.ys and a.OrganizeId=@OrganizeId
                            inner join [NewtouchHIS_Base]..V_S_Sys_Department d on d.Code=a.DepartmentCode and d.OrganizeId=@OrganizeId
                            where b.ghnm=@ghnm and b.OrganizeId=@OrganizeId
                         ");
            SqlParameter[] par =
            {
                new SqlParameter("@ghnm", ghnm),
                new SqlParameter("@OrganizeId", OperatorProvider.GetCurrent().OrganizeId)
            };
            return this.FindList<OutPatChargeDoctorVO>(strSql.ToString(), par).FirstOrDefault();
        }


        /// <summary>
        /// Lấy về thuốc và hạng mục thu phí
        /// </summary>
        /// <param name="keyword">Từ khóa</param>
        /// <param name="yfPz"></param>
        /// <param name="OrganizeId"></param>
        /// <returns></returns> 
        public List<ChargeItemDetailVO> getYpItemList(string keyword, string yfPz, string OrganizeId)
        {
            DbParameter[] par =
            {
                new SqlParameter("@keyword", keyword),
                new SqlParameter("@yfPz", yfPz),
                new SqlParameter("@OrganizeId", OrganizeId)
            };
            return FindList<ChargeItemDetailVO>("EXEC spSelectYpItemByKey @keyword=@keyword, @yfPz =@yfPz,@OrganizeId=@OrganizeId", par);
        }

        /// <summary>
        /// Tạo số đ ơn thuốc
        /// </summary>
        /// <returns></returns>
        public string getCflsh()
        {
            string cflsh = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyString("xt_cflsh").PadLeft(5, '0');

            string suffix = "R" + DateTime.Now.ToString("yyyyMMddHH") + cflsh;
            return suffix;
        }

        /// <summary>
        /// bổ sung phí dịch vụ 
        /// </summary>
        /// <param name="parmBrxz">Tính chất bệnh nhân</param>
        /// <param name="parmDl"> loại lớn </param>
        /// <param name="parmSfxm">Hạng mục thu phí</param>
        /// <param name="parmJe">Đ ơn giá</param>
        /// <returns>count: phí dịch vụ số tiền</returns>
        public decimal? Calcfwfjm(string parmBrxz, string parmDl, string parmSfxm, string parmDJ)
        {
            // phí dịch vụ tỷ lệ, phí dịch vụ số tiền 
            decimal? count = 0;
            SysPatientChargeAdditionalEntity chargAddEntity = _sysPatChargeAddRepo.GetFWFBL(parmBrxz, null, parmSfxm, OperatorProvider.GetCurrent().OrganizeId);
            if (chargAddEntity != null && chargAddEntity.fwfbl > 0)
            {
                count = decimal.Parse(parmDJ) * chargAddEntity.fwfbl;
            }
            else
            {
                chargAddEntity = _sysPatChargeAddRepo.GetFWFBL(parmBrxz, parmDl, "", OperatorProvider.GetCurrent().OrganizeId);
                if (chargAddEntity != null && chargAddEntity.fwfbl > 0)
                {
                    count = decimal.Parse(parmDJ) * chargAddEntity.fwfbl;
                }
            }
            return count;
        }


        /// <summary>
        /// Quyết toán ngoại trú giao dịch 
        /// </summary>
        /// <param name="vo"></param>
        public void OutPatChargeSettDB(OutPatChargeSettDataVo vo)
        {
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                if (vo.mz_cf != null && vo.mz_cf.Count > 0)
                {
                    foreach (var item in vo.mz_cf)
                    {
                        db.Insert(item);
                    }
                }
                if (vo.mz_cfmxList != null && vo.mz_cfmxList.Count > 0)
                {
                    foreach (var item in vo.mz_cfmxList)
                    {
                        db.Insert(item);
                    }
                }
                if (vo.mz_xmList != null && vo.mz_xmList.Count > 0)
                {
                    foreach (var item in vo.mz_xmList)
                    {
                        db.Insert(item);
                    }
                }
                if (vo.mz_js != null)
                {
                    db.Insert(vo.mz_js);
                }
                if (vo.mz_jsmxList != null && vo.mz_jsmxList.Count > 0)
                {
                    foreach (var item in vo.mz_jsmxList)
                    {
                        db.Insert(item);
                    }
                }
                if (vo.mz_jsdlList != null && vo.mz_jsdlList.Count > 0)
                {
                    foreach (var item in vo.mz_jsdlList)
                    {
                        db.Insert(item);
                    }
                }
                if (vo.mz_jszffs != null)
                {
                    db.Insert(vo.mz_jszffs);

                }

                //Số hóa đ ơn logic 
                FinancialInvoiceEntity fpUpdateEntity, fpInsertEntity;
                _finInvoicRepo.UpdateCurrentGetEntitys(vo.fph, OperatorProvider.GetCurrent().UserCode,
                    out fpUpdateEntity, out fpInsertEntity, OperatorProvider.GetCurrent().OrganizeId);
                if (fpUpdateEntity != null)
                {
                    db.Update(fpUpdateEntity);
                }
                if (fpInsertEntity != null)
                {
                    db.Insert(fpInsertEntity);
                }

                db.Commit();
            }

        }

        /// <summary>
        /// Tínhraquyết toán ngoại trú thông tin getMzJs
        /// </summary>
        public void GetMzJsInfo(SettlementEntityVO jsEntity, SysPatientNatureEntity brxzEntity, out OutpatientSettlementEntity mzjs, out List<OutpatientSettlementCategoryEntity> jsdl)
        {
            var brxz = brxzEntity.brxz;
            var jylx = Constants.ybDealLB.yb_deal_wjy;
            //Tínhraquyết toán ngoại trú thông tin 

            // theo hạng mục thu phí và bệnh nhânthu phí thuật toán, nhận đ ược tính kết quả tập 
            var returnValue = GetSettResult(jsEntity, brxz);

            #region 门诊结算

            mzjs = new OutpatientSettlementEntity
            {
                jsnm = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_js"),
                patid = jsEntity.patid,
                ghnm = jsEntity.ghnm,
                brxz = brxzEntity.brxz,
                jslx = jsEntity.jslx,
                zlfy = returnValue.zlHj + returnValue.sfzlHj,
                zffy = returnValue.sfzfHj,
                flzffy = returnValue.flzfHj,
                jzfy = returnValue.jzfyHj,
                jmje = returnValue.jmjeHj,
                jszt = (int)Constants.jsztEnum.YJ,
                cxjsnm = 0,
                zh = 0,
                fpdm = "0",
                jmbl = 0,
                jch = 0,
                zt = "1",
                OrganizeId = OperatorProvider.GetCurrent().OrganizeId,
                CreateTime = DateTime.Now,
                CreatorCode = OperatorProvider.GetCurrent().UserCode
            };
            //Chi phí tự chi trả
            //Chi phí tự trả
            //Chi phí tự trả theo phân loại
            //Phí ghi sổ,nếu là BHYTbệnh nhân,là BHYTquay lại của kết quả
            //Chi phí giảm trừ                              
            //Trạng thái quyết toán 

            // giư ờng gia đ ình số 

            //Thanh toán tiền mặt
            decimal ssk;
            //Sai lệch tiền mặt
            decimal difference;
            //BHYTquay lại tự trả kết quả
            var ybzf = 0m;
            //BHYTquay lại có thể phí ghi sổ
            var jzfy = 0m;
            //Số tiền phải thu
            var ysk = 0m;
            var ybjylx = brxzEntity.ybjylx;
            //Số tiền là0, không truy cập BHYTgiao diện
            if (returnValue.ybjsfwze == 0 && returnValue.jyze == 0)
            {
                ybjylx = Constants.ybjylx.ybjylx0;
            }

            #region 医保 暂不处理

            //BHYTbệnh nhân,và làtúi hậu môn nhân tạohạng mục, tiến hành túi hậu môn nhân tạo giao dịch 
            if (ybjylx != Constants.ybjylx.ybjylx0)
            {
                //túi hậu môn nhân tạovà không phải túi hậu môn nhân tạohạng mục phải tách ra quyết toán
                //bool zkd = isZkd(jsEntity);
                //if (zkd)
                //{
                //    ybjylx = "ZKD";
                //}
            }
            switch (ybjylx)
            {
                // không giao dịch 
                case Constants.ybjylx.ybjylx0:
                    ysk = returnValue.totalHj;
                    jylx = Constants.ybDealLB.yb_deal_wjy;
                    break;

                //Thông thư ờng giao dịch 
                case Constants.ybjylx.ybjylx1:
                    //ybptjy(jsEntity.isGh, brjbxx, gh, brxz, returnValue.YB, out ybzf, out jzfy, out ybError, out sqxh, out isScgh);
                    //Phí ghi sổ
                    mzjs.jzfy = returnValue.FYB.jzfyHj + jzfy;
                    //Số tiền phải thu
                    ysk = returnValue.xjHj + ybzf;
                    //Nếu là đ ăng ký khám thông thư ờng cho ngư ời khuyết tật,phí đ ăng ký khám đ ược ghi sổ
                    //if (isScgh)
                    //{
                    //    mzjs.zlfy = mzjs.zlfy - returnValue.YB.fybjsfwgrzf;
                    //    ysk = ysk - returnValue.YB.fybjsfwgrzf;
                    //}
                    jylx = jsEntity.isGh ? Constants.ybDealLB.yb_deal_mzgh : Constants.ybDealLB.yb_deal_mzsf;
                    break;

                //bệnh nặng giao dịch 
                case Constants.ybjylx.ybjylx2:
                    //ybdbjy(jsEntity.isGh, brjbxx, gh, brxz, returnValue.YB, out ybzf, out jzfy, out ybError, out sqxh, out isScgh);
                    ////Phí ghi sổ
                    mzjs.jzfy = returnValue.FYB.jzfyHj + jzfy;
                    //Số tiền phải thu
                    ysk = returnValue.xjHj + ybzf;
                    //Nếu là đ ăng ký khám thông thư ờng cho ngư ời khuyết tật,phí đ ăng ký khám đ ược ghi sổ
                    //if (isScgh)
                    //{
                    //    mzjs.zlfy = mzjs.zlfy - returnValue.YB.fybjsfwgrzf;
                    //    ysk = ysk - returnValue.YB.fybjsfwgrzf;
                    //}

                    jylx = jsEntity.isGh ? Constants.ybDealLB.yb_deal_dbgh : Constants.ybDealLB.yb_deal_dbsf;
                    break;

                // giao dịch giư ờng gia đ ình 
                case Constants.ybjylx.ybjylx3:
                    // giao dịch giư ờng gia đ ình tổng số tiền(truyền cho BHYTcủa),số dư tài khoản,tổng lũy kế tiền mặt đ ã trả đoạn tự trả ngoại trú,Đ ịnh mức đoạn tự trả ngoại trú
                    decimal jcJyze = 0;
                    //decimal jc_zhye = 0, jc_mzzfdxjzfljs = 0, jc_mzzfdde = 0;
                    //Có hay không nghỉ hưu, đang công táccấu hình của tỷ lệ 
                    const decimal jcBl = 0;
                    if (jsEntity.jslx == "1")
                    {
                        jcJyze = returnValue.jyze; //(bao gồm phí ghi sổ) 
                        // giư ờng gia đ ình ghi sổ mô phỏng giao dịch 
                        //Lấy tổng lũy kế tiền mặt đ ã trả đoạn tự trả ngoại trú, đ ịnh mức đoạn tự trả ngoại trú
                        //DataTable dtYbzh = BLLFactory<yb_brzhbz>.Instance.getYBbrzhxx(brjbxx.kh);


                        // mô phỏng BHYT thuật toán update 51200 20150921
                        //jzfy = (jc_Jyze - jc_mzzfdxjzfljs - jc_mzzfdde) * jc_bl;//Phí ghi sổ BHYT có thể thanh toán
                        jzfy = jcJyze * (1 - jcBl); //Phí ghi sổ BHYT có thể thanh toán
                        ybzf = jcJyze - jzfy;
                        //Phí ghi sổ
                        mzjs.jzfy = returnValue.FYB.jzfyHj + jzfy;
                        //Số tiền phải thu
                        ysk = returnValue.xjHj + ybzf;
                        jylx = Constants.ybDealLB.yb_deal_jcjz;
                    }
                    else
                    {
                        //ybjcjy(jsEntity.isGh, brjbxx, gh, brxz, returnValue.YB, jsEntity.jch, out ybzf, out jzfy, out ybError, out sqxh);
                        //Phí ghi sổ
                        mzjs.jzfy = returnValue.FYB.jzfyHj + jzfy;
                        //Số tiền phải thu
                        ysk = returnValue.xjHj + ybzf;

                        jylx = Constants.ybDealLB.yb_deal_jcsf;
                    }

                    break;

                //tai nạn lao động giao dịch 
                case Constants.ybjylx.ybjylx4:
                    //ybgsjy(jsEntity.isGh, brjbxx, gh, brxz, returnValue.YB, out ybzf, out jzfy, out ybError, out sqxh);
                    //Phí ghi sổ
                    mzjs.jzfy = returnValue.FYB.jzfyHj + jzfy;
                    //Số tiền phải thu
                    ysk = returnValue.xjHj + ybzf;
                    jylx = jsEntity.isGh ? Constants.ybDealLB.yb_deal_gsgh : Constants.ybDealLB.yb_deal_gsmz;
                    break;

                //túi hậu môn nhân tạo giao dịch 
                case "ZKD":
                    //ybmxxmjy(jsEntity.isGh, brjbxx, gh, brxz, jsEntity, out ybzf, out jzfy, out ybError, out sqxh);
                    //Phí ghi sổ
                    mzjs.jzfy = returnValue.FYB.jzfyHj + jzfy;
                    //Số tiền phải thu
                    ysk = returnValue.xjHj + ybzf;

                    jylx = Constants.ybDealLB.yb_deal_sssf;
                    break;
            }

            #endregion

            // giao dịch loại 
            mzjs.jylx = ((int)jylx).ToString();
            //Chi phí tự trả
            mzjs.zffy += ybzf;
            Ext.get5s6r(ysk, out ssk, out difference);
            //Thanh toán tiền mặt
            mzjs.xjzf = ssk;
            //Sai lệch tiền mặt
            mzjs.xjwc = difference;
            //Tổng số tiền:Chi phí tự chi trả+Chi phí tự trả+Chi phí tự trả theo phân loại+Phí ghi sổ
            mzjs.zje = mzjs.zlfy + mzjs.zffy + mzjs.flzffy + mzjs.jzfy;

            #endregion

            //Lấy quyết toán loại lớn Đ ối tư ợng
            jsdl = getJsdl(mzjs, returnValue);

        }

        /// <summary>
        /// nhận đ ược quyết toán kết quả theo hạng mục thu phí và bệnh nhânthu phí thuật toán, nhận đ ược tính kết quả tập 
        /// </summary>
        /// <param name="jsEntity"></param>
        /// <param name="brxz">Tính chất bệnh nhân</param>
        /// <returns></returns>
        private Brsfsf_ReturnValue_YBANDFYB GetSettResult(SettlementEntityVO jsEntity, string brxz)
        {
            Brsfsf_ReturnValue_YBANDFYB returnValue = new Brsfsf_ReturnValue_YBANDFYB();

            //CầnQuyết toánCủaHạng mụcDanh sách
            List<SettleProjectVO> xmList = jsEntity.jsxmList;

            //Tải lênBHYTvà không tải lênBHYThạng mục
            var xmListYB = new List<SettleProjectVO>();
            var xmListFYB = new List<SettleProjectVO>();

            //Lấy cần tải lênBHYTvà không tải lênBHYTcủa loại lớn 
            getYBANDFYB(xmList, out xmListYB, out xmListFYB);

            //Bệnh nhân thuật toán quay lại tính của kết quả tập 
            var returnValueYB = new Brsfsf_ReturnValue();
            var returnValueFYB = new Brsfsf_ReturnValue();

            ////Tải lên và không tải lênthu phí BHYThạng mục tổng cộng 
            getSfxmHj(xmListFYB);
            getSfxmHj(xmListYB);


            // theo hạng mục thu phí tổng cộng và bệnh nhânthu phí thuật toán tính
            getBrsfResult(xmListYB, brxz, out returnValueYB);
            getBrsfResult(xmListFYB, brxz, out returnValueFYB);

            returnValue.YB = returnValueYB;
            returnValue.FYB = returnValueFYB;

            //jsEntity.jsxmList.Clear();
            //jsEntity.jsxmList.AddRange(xmListYB);
            //jsEntity.jsxmList.AddRange(xmListFYB);

            return returnValue;
        }


        /// <summary>
        /// Lấy cần tải lênBHYTvà không tải lênBHYTcủa loại lớn 
        /// </summary>
        /// <param name="xmList"></param>
        /// <param name="xmListYB"></param>
        /// <param name="xmListFYB"></param>
        private void getYBANDFYB(List<SettleProjectVO> xmList, out List<SettleProjectVO> xmListYB,
            out List<SettleProjectVO> xmListFYB)
        {

            xmListYB = new List<SettleProjectVO>();
            xmListFYB = new List<SettleProjectVO>();

            //Lấy ngoại trú cấu hình: không tải lênBHYT loại lớn 
            var pzzyBSCYBEntity = _sysConfigRepo.GetByCode(Constants.xtzypz.DL_BSCYB,
                OperatorProvider.GetCurrent().OrganizeId);
            if (pzzyBSCYBEntity == null)
            {
                xmListYB = xmList;
                return;
            }

            string[] struDl = pzzyBSCYBEntity.Value.TrimEnd(',').Split(',');
            Dictionary<string, string> dlDict = new Dictionary<string, string>();
            foreach (string dl in struDl)
            {
                if (dlDict.ContainsKey(dl))
                    continue;

                dlDict.Add(dl, dl);
            }

            // không có cấu hình không tải lênBHYT
            if (struDl.Length == 0)
            {
                xmListYB = xmList;
                return;
            }

            // theo loại lớn phân biệtcó hay không tải lênBHYT
            foreach (SettleProjectVO xm in xmList)
            {
                if (dlDict.ContainsKey(xm.dl))
                {
                    xmListFYB.Add(xm);
                }
                else
                {
                    xmListYB.Add(xm);
                }
            }
        }

        /// <summary>
        /// Tính hạng mục thu phí không làm tổng cộng 
        /// </summary>
        /// <param name="xmList"></param>
        /// <returns>Số tiền có thể ghi sổ,Số tiền tự trả theo phân loại,Số tiền tự chi trả,Số tiền giảm trừ</returns>
        private void getSfxmHj(List<SettleProjectVO> xmList)
        {

            // theo hạng mục thu phí tính
            decimal jmhje = 0;
            decimal zfbl = 0;

            foreach (SettleProjectVO xm in xmList)
            {
                //Số tiền sau giảm trừ
                jmhje = (xm.dj + xm.fwfdj) * xm.sl - xm.jmje;
                zfbl = Convert.ToDecimal(xm.zfbl == null ? 0 : xm.zfbl);

                //Số tiền tự chi trả
                if (xm.zfxz == ((int)Constants.zfxzEnum.ZF).ToString())
                {
                    xm.zlfy = jmhje;
                }
                else if (xm.zfxz == ((int)Constants.zfxzEnum.KB).ToString())
                {
                    //Số tiền có thể ghi sổ
                    xm.kbje = jmhje;
                }
                else
                {
                    //Có thể thanh toán:Chi phí ×(1－tỷ lệ tự trả)
                    //Tự trả theo phân loại:Chi phí ×tỷ lệ tự trả
                    //Khi tỷ lệ tự trả là số âm giờ,biểu thị tự trả đ ịnh mức
                    if (xm.zfbl >= 0)
                    {
                        //Số tiền có thể ghi sổ
                        xm.kbje = (jmhje * (1 - zfbl));
                        //Số tiền tự trả theo phân loại
                        xm.flzf = (jmhje * zfbl);
                    }
                    else
                    {
                        if (xm.jmbl < 0)
                        {
                            throw new FailedException("OUTPAT_JMBL_ZFBL_IS_NOTEXIT"); //Tỷ lệ giảm trừ và tỷ lệ tự trả chỉ có thể có một là định mức,vui lòng cấu hình lại！
                        }

                        //Số tiền tự trả theo phân loại: Đ ịnh mức * (1-tỷ lệ giảm trừ)* Số lư ợng
                        xm.flzf = -(zfbl) * (1 - zfbl) * xm.sl;
                        //Số tiền có thể ghi sổ
                        xm.kbje = jmhje - xm.flzf;
                    }
                }
            }
        }

        /// <summary>
        /// theo hạng mục thu phí tổng cộng và bệnh nhânthu phí thuật toán tính
        /// </summary>
        /// <param name="amountList"></param>
        /// <param name="brxz"></param>
        private void getBrsfResult(List<SettleProjectVO> amountList, string brxz, out Brsfsf_ReturnValue returnValue)
        {
            returnValue = new Brsfsf_ReturnValue();
            returnValue.dlFymxList = new List<Brsfsf_Dl_Fymx>();

            //Lấy tất cả các thuật toán 
            List<SysPatientChargeAlgorithmEntity> sfAllList = _sysPatiAlgorithmRepo.getAllMzActive();

            // theo thu phí thuật toán tính
            foreach (SettleProjectVO entity in amountList)
            {
                Brsfsf_Dl_Fymx fymx = new Brsfsf_Dl_Fymx();
                // loại lớn 
                string dl = entity.dl;
                fymx.dl = dl;
                fymx.sfxm = entity.sfxm;
                fymx.mxnm = entity.mxnm;
                //fymx.mxbm = entity.mxbm;
                fymx.cf_mxnm = entity.cf_mxnm;
                fymx.czh = entity.czh;

                //Chi phí giảm trừ
                fymx.jmje = entity.jmje;

                //// theo tính chất bệnh nhân và loại lớn lấy bệnh nhânthu phí thuật toán 
                List<SysPatientChargeAlgorithmEntity> sfList = null;
                //Hạng mục thuật toán ưu trư ớc cấp hơn cao
                if (entity.mxnm != 0)
                {
                    sfList = sfAllList.FindAll(t => t.brxz == brxz && t.sfxm == entity.sfxm);
                }
                // không có hạng mục của thuật toán,thì xem xét loại lớn 
                if (sfList == null || sfList.Count == 0)
                {
                    sfList = sfAllList.FindAll(
                        t => t.brxz == brxz && t.sfxm == "" && (t.dl == dl || t.dl.Trim() == "*"));
                }

                #region 没有病人收费算法

                //Nếu không có bệnh nhânthu phí thuật toán 
                if (sfList == null || sfList.Count == 0)
                {
                    // không có thuật toán tổng chi phí = có thể ghi sổ＋tự trả theo phân loại＋tự chi trả
                    fymx.total = entity.kbje + entity.flzf + entity.zlfy;
                    //Tự trả theo phân loại
                    fymx.flzf = entity.flzf;
                    //Phí ghi sổ
                    fymx.jzfy = entity.kbje;
                    //Chi phí tự chi trả
                    fymx.zl = entity.zlfy;
                    //Tiền mặt
                    fymx.xj = entity.flzf + entity.zlfy;

                    entity.jyje = fymx.jzfy;
                    entity.jyfwje = fymx.jzfy + fymx.flzf;

                    returnValue.dlFymxList.Add(fymx);
                    continue;
                }

                #endregion

                #region 费用合计

                if (sfList[0].fyfw == Constants.fyfw.fyfw0)
                {
                    //Có thể ghi sổ : Tổng số tiền giao dịch = Tổng chi phí trong phạm vi quyết toán BHYT - Tự trả theo phân loại;Tiền mặt:Tự trả theo phân loại+Tự chi trả+Tự trả
                    //Tiền mặt
                    fymx.xj = entity.flzf + entity.zlfy;
                    //Phí ghi sổ
                    fymx.jzfy = entity.kbje;
                    //Tự trả theo phân loại
                    fymx.flzf = entity.flzf;
                    //Chi phí tự chi trả
                    fymx.zl = entity.zlfy;
                }
                else if (sfList[0].fyfw == Constants.fyfw.fyfw1)
                {
                    //Có thể ghi sổ + Tự trả theo phân loại:Tổng số tiền giao dịch = Tổng chi phí trong phạm vi quyết toán BHYT;Tiền mặt:Tự chi trả+Tự trả
                    //Tiền mặt
                    fymx.xj = entity.zlfy;
                    //Phí ghi sổ
                    fymx.jzfy = entity.kbje + entity.flzf;
                    //Tự trả theo phân loại
                    fymx.flzf = 0m;
                    //Chi phí tự chi trả
                    fymx.zl = entity.zlfy;
                }
                else if (sfList[0].fyfw == Constants.fyfw.fyfw2)
                {
                    //Có thể ghi sổ + Tự trả theo phân loại + Tự chi trả:Tổng số tiền giao dịch = Tổng chi phí trong phạm vi quyết toán BHYT;Tiền mặt:Tự trả
                    //Tiền mặt
                    fymx.xj = 0m;
                    //Phí ghi sổ
                    fymx.jzfy = entity.kbje + entity.flzf + entity.zlfy;
                    //Tự trả theo phân loại
                    fymx.flzf = 0m;
                    //Chi phí tự chi trả
                    fymx.zl = 0m;
                }
                else if (sfList[0].fyfw == Constants.fyfw.fyfw3)
                {
                    //Có thể ghi sổ + Tự trả theo phân loại + Tự chi trả + Hoàn toàn tự trả:Tổng số tiền giao dịch = Tổng chi phí trong phạm vi quyết toán BHYT;Tiền mặt:Tự trả
                    //Tiền mặt
                    fymx.xj = 0m;
                    //Phí ghi sổ
                    fymx.jzfy = entity.kbje + entity.flzf + entity.zlfy;
                    //Tự trả theo phân loại
                    fymx.flzf = 0m;
                    //Chi phí tự chi trả
                    fymx.zl = 0m;
                }

                #endregion

                #region 费用计算

                //TínhSố tiền
                decimal jsAmount = fymx.jzfy;
                // theo chi phí phạm vi cần tính của số tiền
                decimal jsedAmount = 0m;
                decimal sfzf = 0m;
                //Nếu có chi phí trực tuyến,thì theo thuật toán cấp bậc tính(thực tế chỉ cầntínhchi phí tự chi trả) 
                foreach (SysPatientChargeAlgorithmEntity sfEntity in sfList)
                {
                    //Nếu cùng một lô thuật toán của tính chất tự trả và chi phí phạm vi không nhất quán thì trực tiếp báo lỗi 
                    if (sfEntity.zfxz != sfList[0].zfxz)
                        throw new FailedCodeException(
                            "OUTPAT_PATIENT_NATURE_AND_ZFXZ_IS_NOT_SAME_RANGE"); //Khi trư ớc tính chất bệnh nhân và loại lớn của tính chất tự trả phải giống nhau,vui lòng cấu hình lại！
                    if (sfEntity.fyfw != sfList[0].fyfw)
                        throw new FailedCodeException(
                            "HOSP_PATIENT_NATURE_AND_CATEGORY_THE_SUANFALIST_IS_NOT_SAME_RANGE"); //Khi trư ớc tính chất bệnh nhân và loại lớn củachi phí phạm vi phải giống nhau,vui lòng cấu hình lại！

                    decimal fysx = sfEntity.fysx * entity.sl;

                    if (fysx > fymx.jzfy)
                    {
                        sfzf += calZfbl(jsAmount, sfEntity.zfbl, entity.sl);
                        jsedAmount += jsAmount;
                        jsAmount = fymx.jzfy - jsedAmount;
                        break;
                    }
                    else
                    {
                        if (fysx > jsAmount)
                        {
                            jsedAmount += fysx - jsedAmount;
                            sfzf += calZfbl(jsAmount, sfEntity.zfbl, entity.sl);
                        }
                        else
                        {
                            if (fysx == 0)
                            {
                                sfzf += calZfbl(jsAmount, sfEntity.zfbl, entity.sl);
                                jsedAmount = jsAmount;
                                break;
                            }
                            else
                            {
                                jsedAmount += fysx;
                                sfzf += calZfbl(fysx, sfEntity.zfbl, entity.sl);
                            }
                        }
                        jsAmount = fymx.jzfy - jsedAmount;
                    }
                }
                if (jsedAmount != fymx.jzfy)
                {
                    sfzf += fymx.jzfy - jsedAmount;
                }

                #endregion

                if (sfList[0].zfxz == ((int)Constants.sfZfxzEnum.ZL).ToString())
                {
                    //Tính chất tự trả là tự chi trả
                    fymx.sfzl += sfzf;
                }
                else
                {
                    fymx.sfzf += sfzf;
                }
                if (sfList[0].fyfw == Constants.fyfw.fyfw3)
                {
                    //Nếu là hoàn toàn tự trả,ghi sổ số tiền là tự chi trả
                    fymx.zl += fymx.jzfy - sfzf;
                    fymx.jzfy = 0m;
                    fymx.xj += fymx.zl;
                }
                else
                {
                    // có thể phí ghi sổ
                    fymx.jzfy = fymx.jzfy - sfzf;
                }
                //Tiền mặt
                fymx.xj += sfzf;
                //Có thuật toán tổng chi phí = tiền mặt
                fymx.total = fymx.xj;

                entity.jyje = fymx.jzfy;
                entity.jyfwje = fymx.jzfy + fymx.flzf;

                returnValue.dlFymxList.Add(fymx);
            }
        }

        #region  获取结算大类

        /// <summary>
        /// Lấy quyết toán ngoại trú loại lớn 
        /// </summary>
        /// <param name="js"></param>
        /// <param name="returnValue"></param>
        /// <returns></returns>
        public List<OutpatientSettlementCategoryEntity> getJsdl(OutpatientSettlementEntity js,
            Brsfsf_ReturnValue_YBANDFYB returnValue)
        {
            List<OutpatientSettlementCategoryEntity> jsdlList = new List<OutpatientSettlementCategoryEntity>();
            //Chi phí bảo hiểm y tế
            foreach (Brsfsf_Dl_Fymx yb in returnValue.YB.dlFymxList)
            {
                OutpatientSettlementCategoryEntity jsdl = jsdlList.Find(t => t.dl == yb.dl);
                if (jsdl == null)
                {
                    jsdl = new OutpatientSettlementCategoryEntity();
                    jsdl.jsdlId = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyString("mz_jsdl");
                    jsdl.zt = "1";
                    jsdl.OrganizeId = OperatorProvider.GetCurrent().OrganizeId;
                    jsdl.CreateTime = DateTime.Now;
                    jsdl.CreatorCode = OperatorProvider.GetCurrent().UserCode;
                    jsdl.jsrq = DateTime.Now;
                    jsdlList.Add(jsdl);
                }

                jsdl.jsnm = js.jsnm;
                jsdl.dl = yb.dl; // loại lớn 
                jsdl.flzffy += yb.flzf; //Chi phí tự trả theo phân loại
                jsdl.zlfy += yb.zl + yb.sfzl; //Chi phí tự chi trả
                jsdl.jmfy += yb.jmje; //Số tiền giảm trừ

                if (returnValue.ybjsfwze == 0)
                {
                    jsdl.zffy += 0m;
                    jsdl.kbfy += 0m;
                }
                else
                {
                    jsdl.zffy += ((yb.jzfy + yb.flzf) / returnValue.ybjsfwze) * js.zffy; //Số tiền theo phân loại/Tổng số tiền theo phân loại*Số tiền tự trả
                    jsdl.kbfy += ((yb.jzfy + yb.flzf) / returnValue.ybjsfwze) * js.jzfy; //Số tiền theo phân loại/Tổng số tiền theo phân loại*Số tiền tự trả
                }
                jsdl.zje += jsdl.flzffy + jsdl.zlfy + jsdl.jmfy + jsdl.zffy + jsdl.kbfy;
            }
            // không phải chi phí bảo hiểm y tế
            foreach (Brsfsf_Dl_Fymx yb in returnValue.FYB.dlFymxList)
            {
                OutpatientSettlementCategoryEntity jsdl = jsdlList.Find(t => t.dl == yb.dl);
                if (jsdl == null)
                {
                    jsdl = new OutpatientSettlementCategoryEntity();
                    jsdl.jsdlId = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyString("mz_jsdl");
                    jsdl.zt = "1";
                    jsdl.CreateTime = DateTime.Now;
                    jsdl.CreatorCode = OperatorProvider.GetCurrent().UserCode;
                    jsdl.jsrq = DateTime.Now;
                    jsdlList.Add(jsdl);
                }

                jsdl.jsnm = js.jsnm;
                jsdl.dl = yb.dl; // loại lớn 
                jsdl.flzffy += yb.flzf; //Chi phí tự trả theo phân loại
                jsdl.zlfy += yb.zl + yb.sfzl; //Chi phí tự chi trả
                jsdl.jmfy += yb.jmje; //Số tiền giảm trừ

                jsdl.zffy += yb.sfzf;
                jsdl.kbfy += yb.jzfy;
                jsdl.zje += jsdl.flzffy + jsdl.zlfy + jsdl.jmfy + jsdl.zffy + jsdl.kbfy;
            }

            foreach (OutpatientSettlementCategoryEntity jsdl in jsdlList)
            {
                jsdl.flzffy = Ext.getsr(jsdl.flzffy);
                jsdl.zlfy = Ext.getsr(jsdl.zlfy);
                jsdl.jmfy = Ext.getsr(jsdl.jmfy);
                jsdl.zffy = Ext.getsr(jsdl.zffy);
                jsdl.kbfy = Ext.getsr(jsdl.kbfy);
                jsdl.zje = Ext.getsr(jsdl.zje);

            }

            return jsdlList;
        }

        #endregion


        /// <summary>
        /// theo tỷ lệ tự trả tính số tiền tự trả
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="zfbl"></param>
        /// <returns></returns>
        private decimal calZfbl(decimal amount, decimal zfbl, decimal sl)
        {
            if (zfbl >= 0)
            {
                return amount * zfbl;
            }
            else
            {
                if (amount > 0)
                    return -zfbl * sl;
                else
                    return 0;
            }
        }

        /// <summary>
        /// Lấy chi tiết quyết toán
        /// </summary>
        /// <param name="mzjs"></param>
        /// <param name="jsEntity"></param>
        /// <returns></returns>
        public List<OutpatientSettlementDetailEntity> getJSMX(OutpatientSettlementEntity mzjs, SettlementEntityVO jsEntity)
        {
            var jsmxList = new List<OutpatientSettlementDetailEntity>();
            if (jsEntity != null && jsEntity.jsxmList.Count > 0)
            {
                foreach (var jsxm in jsEntity.jsxmList)
                {
                    #region 结算项目明细

                    var mzjsmx = new OutpatientSettlementDetailEntity
                    {
                        jsmxnm = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_jsmx"),
                        jsnm = mzjs.jsnm,
                        mxnm = jsxm.mxnm,
                        sl = jsxm.sl,
                        jslx = mzjs.jslx,
                        cf_mxnm = jsxm.cf_mxnm,
                        jyje = jsxm.jyje,
                        jyfwje = jsxm.jyfwje,
                        zt = "1",
                        OrganizeId = OperatorProvider.GetCurrent().OrganizeId,
                        CreateTime = DateTime.Now,
                        CreatorCode = OperatorProvider.GetCurrent().UserCode
                    };
                    jsmxList.Add(mzjsmx);

                    #endregion
                }
            }
            return jsmxList;
        }


        /// <summary>
        /// Lấy chi tiết quyết toán
        /// </summary>
        /// <param name="mzjsList"></param>
        /// <param name="jsEntity"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public List<OutpatientSettlementDetailEntity> getJSMX(
            SettlementEntityVO jsEntity, string orgId)
        {
            var jsmxList = new List<OutpatientSettlementDetailEntity>();
            if (jsEntity == null || jsEntity.jsxmList.Count <= 0) return jsmxList;
            foreach (var jsxm in jsEntity.jsxmList)
            {
                #region 结算项目明细

                var mzjsmx = new OutpatientSettlementDetailEntity();
                mzjsmx.jsmxnm = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_jsmx");
                mzjsmx.jsnm = int.Parse(jsxm.jsnm.ToString());
                mzjsmx.mxnm = jsxm.mxnm;
                mzjsmx.sl = jsxm.sl;
                mzjsmx.jslx = "2";//2 biểu thị ghi sổ ngoại trú
                mzjsmx.cf_mxnm = jsxm.cf_mxnm;
                mzjsmx.jyje = jsxm.jyje;
                mzjsmx.jyfwje = jsxm.jyfwje;
                mzjsmx.zt = "1";
                mzjsmx.OrganizeId = orgId;
                mzjsmx.CreateTime = DateTime.Now;
                mzjsmx.CreatorCode = OperatorProvider.GetCurrent().UserCode;
                jsmxList.Add(mzjsmx);

                #endregion
            }

            return jsmxList;
        }

        #region 门诊结算支付方式

        /// <summary>
        /// Phư ơng thức thanh toán quyết toán ngoại trú
        /// </summary>
        /// <param name="jsnm">Mã nội bộ quyết toán</param>
        /// <param name="xjzffs"> phư ơng thức thanh toán </param>
        /// <param name="ysk">Số tiền phải thu</param>
        /// <returns></returns>
        public OutpatientSettlementPaymentModelEntity GetMzJsZffs(int jsnm, string xjzffs, string xjzffsbh, decimal ysk)
        {
            #region 结算支付方式

            var mzjszffs = new OutpatientSettlementPaymentModelEntity
            {
                mzjszffsbh = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_jszffs"),
                jsnm = jsnm,
                xjzffs = xjzffs,
                ssry = OperatorProvider.GetCurrent().UserCode,
                ssrq = DateTime.Now,
                zh = 0
            };

            if (mzjszffs.xjzffs == Constants.xtzffs.XJZF) //Tiền mặt
            {
                mzjszffs.zfje = ysk;
            }
            mzjszffs.zt = "1";
            mzjszffs.OrganizeId = OperatorProvider.GetCurrent().OrganizeId;
            mzjszffs.CreateTime = DateTime.Now;
            mzjszffs.CreatorCode = OperatorProvider.GetCurrent().UserCode;

            #endregion

            return mzjszffs;
        }

        #endregion

        #region 门诊记账

        /// <summary>
        /// theo số thẻ số hồ sơ bệnh án số khám ngoại trúmờtra cứu thông tin bệnh nhân(ghi sổ ngoại trú phải phía trênthông tin bệnh nhân)
        /// </summary>
        /// <param name="kh"></param>
        /// <param name="IsBlh"></param>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public List<OutpatAccInfoDto> GetChargePatInfoInAcc(string kh, string IsBlh, string mzh, string orgId)
        {
            string isClinic = "2";//BHYT
            DbParameter[] opar = { new SqlParameter("@Id", orgId) };
            var isZhenSuo = FirstOrDefault<string>("SELECT CategoryCode FROM NewtouchHIS_Base..Sys_Organize WHERE Id=@Id", opar);
            if (isZhenSuo == "Clinic")
            {
                isClinic = "1";//1 biểu thị phòng khám, dùng bảo hiểm thư ơng mại 
            }
            var blh = IsBlh == "ON" ? mzh : null;
            var mmzh = (IsBlh == "OFF" || IsBlh == null) ? mzh : null;
            List<OutpatAccInfoDto> patChargeVo = null;
            StringBuilder strSql = new StringBuilder();
            DbParameter[] par = new DbParameter[3];
            if (isClinic == "2")
            {
                strSql.Append(@"SELECT  A.patid ,
                                        a.blh ,
                                        A.xm ,
                                        A.xb ,
                                        A.csny ,
                                        A.zjh ,
                                        A.zjlx ,
                                        A.zjh ,
                                        c.CardNo kh ,
                                        a.jsr,
                                        --A.nl,
                                        CAST( FLOOR(datediff(DY,a.csny,getdate())/365.25) as int) nl,
                                        xz.brxz,
										xz.brxzmc,
                                        yb.sycs,
                                        A.dh,
                                        A.dybh,
                                        gh.mzh,
                                        gh.ghnm,
                                        gh.ys,
                                        CONVERT(varchar(100), gh.ghrq, 20) ghrq,
                                        A.phone
                                FROM    [dbo].[xt_brjbxx] AS A WITH ( NOLOCK )
                                        INNER JOIN dbo.xt_card c ON c.patid = a.patid
                                                                    AND A.OrganizeId = c.OrganizeId
                                        LEFT JOIN xt_brxz xz ON xz.brxz = a.brxz
                                        AND xz.OrganizeId = c.OrganizeId
                                        LEFT JOIN xt_ybbab yb ON yb.patid = a.patid
                                         AND a.OrganizeId = yb.OrganizeId
                                        RIGHT JOIN dbo.mz_gh gh ON gh.patid=a.patid 
		                                AND gh.OrganizeId=c.OrganizeId
                                WHERE  (c.CardNo = @kh OR @kh='') and (gh.mzh like @mzh OR @mzh='%%') and (A.blh  like @blh OR @blh='%%')
                                        AND a.OrganizeId = @OrganizeId");
                DbParameter[] par1 =
                {
                new SqlParameter("@kh", kh ?? ""),
                new SqlParameter("@mzh","%"+(mmzh??"")+"%"),
                 new SqlParameter("@blh", "%"+(blh??"")+"%"),
                new SqlParameter("@OrganizeId", orgId)

            };
                patChargeVo = FindList<OutpatAccInfoDto>(strSql.ToString(), par1);
            }
            else if (isClinic == "1")// bảo hiểm thư ơng mại 
            {
                StringBuilder strSql2 = new StringBuilder();
                strSql.Append(@"SELECT  A.patid ,
                                a.blh ,
                                A.xm ,
                                A.xb ,
                                A.csny ,
                                A.zjh ,
                                A.zjlx ,
                                A.zjh ,
                                c.CardNo kh ,
                                a.jsr ,
                                gh.ghnm,
                                gh.ys,                                                                --A.nl,
                                CAST(FLOOR(DATEDIFF(DY, a.csny, GETDATE()) / 365.25) AS INT) nl ,
                                xz.brxz ,
                                xz.brxzmc ,
                                sb.sycs ,
                                A.dh ,
                                A.dybh ,
                                gh.mzh ,

                                 CONVERT(varchar(100), gh.ghrq, 20) ghrq,
                                A.phone
                        FROM    [dbo].[xt_brjbxx] AS A WITH ( NOLOCK )
                                INNER JOIN dbo.xt_card c ON c.patid = a.patid
                                                            AND A.OrganizeId = c.OrganizeId
                                LEFT JOIN xt_brxz xz ON xz.brxz = a.brxz
                                                        AND xz.OrganizeId = c.OrganizeId
                                LEFT JOIN xt_sbbab sb ON sb.patid = a.patid
                                                         AND a.OrganizeId = sb.OrganizeId
                                RIGHT JOIN dbo.mz_gh gh ON gh.patid = a.patid
                                   AND gh.OrganizeId = c.OrganizeId
                                WHERE  (c.CardNo = @kh OR @kh='') and (gh.mzh like @mzh OR @mzh='%%') and (A.blh like @blh OR @blh='%%')
                                        AND a.OrganizeId = @OrganizeId");
                DbParameter[] par2 =
                {
                new SqlParameter("@kh", kh ?? ""),
                new SqlParameter("@mzh", "%"+(mmzh??"")+"%"),
                 new SqlParameter("@blh", "%"+(blh??"")+"%"),
                new SqlParameter("@OrganizeId", orgId)

            };
                patChargeVo = FindList<OutpatAccInfoDto>(strSql.ToString(), par2);
            }

            if (patChargeVo == null)
            {
                throw new FailedCodeException("OUTPAT_REGIST_ISINVALID");
            }
            return patChargeVo;
        }

        /// <summary>
        /// theo số thẻ và đăng ký khám thời giantra cứu bệnh nhân thông tin chi tiết(ngoại trú thu phí phải phía trênthông tin bệnh nhân)
        /// </summary>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="kh"></param>
        /// <param name="cardType"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public List<OutpatAccInfoDto> GetpatientAccountList(DateTime kssj, DateTime jssj, string kh, string zjh,string cardType, string orgId)
        {
            const string sql = @"
SELECT  A.patid ,
a.blh ,
A.xm ,
A.xb ,
A.csny ,
A.zjh ,
A.zjlx ,
A.zjh ,
gh.kh kh , gh.CardType, gh.CardTypeName,
--a.jsr,
--A.nl,
CAST( FLOOR(datediff(DY,a.csny,getdate())/365.25) as int) nl,
xz.brxz,
xz.brxzmc,
A.dh,
gh.mzh,
gh.ghnm,
gh.ys,
gh.ks,
gh.jzbz jiuzhenbiaozhi,
CONVERT(varchar(100), gh.ghrq, 20) ghrq,
A.phone, gh.ghzt, gh.ybjsh, gh.jzyy, gh.kh sbbh,
ysStaff.Name ysmc, ksDept.Name ksmc
FROM [dbo].[xt_brjbxx] AS A WITH ( NOLOCK )
RIGHT JOIN dbo.mz_gh(NOLOCK) gh ON gh.patid=a.patid 
left join [NewtouchHIS_Base]..V_S_Sys_Staff ysStaff on ysStaff.gh = gh.ys and ysStaff.OrganizeId = gh.OrganizeId
left join [NewtouchHIS_Base]..V_S_Sys_Department ksDept on ksDept.Code = gh.ks and ksDept.OrganizeId = gh.OrganizeId
--费用性质一定是当次挂号的
LEFT JOIN xt_brxz xz ON xz.brxz = gh.brxz AND xz.OrganizeId = gh.OrganizeId
WHERE A.zt = '1' 
and xz.zt = '1' 
AND gh.zt = '1'
and a.OrganizeId = @OrganizeId 
and (gh.kh = @kh or gh.zjh=@zjh)
AND gh.CardType = @CardType
AND gh.ghrq BETWEEN @kssj AND DATEADD(DAY,1,CONVERT(DATETIME,@jssj))
";
            var param = new DbParameter[]
            {
                new SqlParameter("@kssj", kssj),
                new SqlParameter("@jssj", jssj),
                new SqlParameter("@OrganizeId", orgId),
                new SqlParameter("@kh", kh??""),
                new SqlParameter("@zjh", zjh??""),
                new SqlParameter("@cardType", cardType??"")
            };
            return FindList<OutpatAccInfoDto>(sql, param);
        }

        /// <summary>
        /// theo số khám ngoại trú tra cứu bệnh nhân thông tin chi tiết(ngoại trú thu phí phải phía trênthông tin bệnh nhân)
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="cardType"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public List<OutpatAccInfoDto> GetOutpatChargePatInfoInAcc(string mzh, string kh,string zjh, string cardType, string orgId)
        {
            if (string.IsNullOrWhiteSpace(mzh) && (string.IsNullOrWhiteSpace(kh) || string.IsNullOrWhiteSpace(cardType)) && string.IsNullOrWhiteSpace(zjh))
            {
                throw new FailedException("缺少查询参数：门诊号或卡号");
            }
            var strSql = new StringBuilder(@"
SELECT  A.patid ,
a.blh ,
A.xm ,
A.xb ,
A.csny ,
A.zjh ,
A.zjlx ,
A.zjh ,
gh.kh kh , gh.CardType, gh.CardTypeName,
--a.jsr,
--A.nl,
CAST( FLOOR(datediff(DY,a.csny,getdate())/365.25) as int) nl,
xz.brxz,
xz.brxzmc,xz.brxzlb,
A.dh,
gh.mzh,
gh.ghnm,
gh.ys,
gh.ks,
gh.jzbz jiuzhenbiaozhi,
CONVERT(varchar(100), gh.ghrq, 20) ghrq,
A.phone, gh.ghzt, gh.ybjsh, gh.jzyy,  gh.kh sbbh,
ysStaff.Name ysmc, ksDept.Name ksmc,gh.mjzbz
FROM [dbo].[xt_brjbxx] AS A WITH ( NOLOCK )
RIGHT JOIN dbo.mz_gh(NOLOCK) gh ON gh.patid=a.patid and ISNULL(gh.ghzt, '') <> '2'
left join [NewtouchHIS_Base]..V_S_Sys_Staff ysStaff on ysStaff.gh = gh.ys and ysStaff.OrganizeId = gh.OrganizeId
left join [NewtouchHIS_Base]..V_S_Sys_Department ksDept on ksDept.Code = gh.ks and ksDept.OrganizeId = gh.OrganizeId
--费用性质一定是当次挂号的
LEFT JOIN xt_brxz xz ON xz.brxz = gh.brxz AND xz.OrganizeId = gh.OrganizeId
WHERE A.zt = '1' 
and xz.zt = '1' and gh.zt = '1'
and a.OrganizeId = @OrganizeId 
and (gh.mzh=@mzh or (gh.kh = @kh and gh.CardType = @CardType) or (gh.zjh=@zjh and gh.CardType=@CardType))
order by gh.ghrq
");
            DbParameter[] par1 =
            {
                new SqlParameter("@mzh",(mzh??"")),
                new SqlParameter("@OrganizeId", orgId),
                new SqlParameter("@kh", kh??""),
                new SqlParameter("@zjh", zjh??""),
                new SqlParameter("@cardType", cardType??""),
            };

            return FindList<OutpatAccInfoDto>(strSql.ToString(), par1);
        }

        /// <summary>
        /// theo số thẻ và số hồ sơ bệnh án lấy thông tin cơ bản bệnh nhân
        /// </summary>
        /// <param name="kh"></param>
        /// <param name="blh"></param>
        /// <param name="zjh"></param>
        /// <param name="orgId"></param>
        /// <param name="cardType"></param>
        /// <returns></returns>
        public OutpatAccInfoDto GetChargePatInfoInRegister(string kh, string blh, string zjh, string orgId, string cardType,string ly,string CardId)
        {
            OutpatAccInfoDto patxx = new OutpatAccInfoDto();
            if (ly != null)
            {
                string yktsql = @" select patid from [dbo].[xt_brjbxx](NOLOCK)
      where zjh=@zjh and OrganizeId = @OrganizeId  and zt=1
";
                DbParameter[] par1 =
               {
                new SqlParameter("@OrganizeId", orgId),
                new SqlParameter("@zjh", zjh??""),
            };
                patxx = FindList<OutpatAccInfoDto>(yktsql.ToString(), par1).FirstOrDefault();
                if (patxx == null)
                {
                    throw new FailedCodeException("OUTPAT_REGIST_ISINVALID");
                }
            }
            string strSql = @"
                    SELECT  A.patid ,
	                    a.blh ,
	                    A.xm ,
	                    A.xb ,
	                    A.csny ,
	                    A.zjh ,
	                    A.zjlx ,
	                    A.zjh ,
	                    C.CardType,
	                    C.CardTypeName,
	                    c.CardNo kh ,
	                    CAST( FLOOR(datediff(DY,a.csny,getdate())/365.25) as int) nl,
	                    xz.brxz,
	                    xz.brxzmc,
	                    xz.brxzbh ,
                        xz.brxzlb,
	                    yb.sycs,
	                    A.dh,
	                    A.dybh,
	                    A.brly,
	                    A.phone,
	                    a.jjllrgx lxrgx,
	                    a.jjlldh lxrdh,
	                    a.jjllr lxr,
	                    '' db ,
	                    '' dbzd,A.hf,
	                    A.gj gjCode, A.mz mzCode, gj.gjmc, mz.mzmc, c.CardNo sbbh,c.cblb,A.xian_sheng+xian_shi+xian_xian+xian_dz dz
                    FROM [dbo].[xt_brjbxx](NOLOCK) A  
	                    INNER JOIN dbo.xt_card(NOLOCK) c ON c.patid = a.patid AND A.OrganizeId = c.OrganizeId and c.zt=1
	                    LEFT JOIN xt_brxz(NOLOCK) xz ON xz.brxz = c.brxz AND xz.OrganizeId = c.OrganizeId and xz.zt=1
	                    LEFT JOIN xt_ybbab(NOLOCK) yb ON yb.patid = a.patid AND xz.OrganizeId = yb.OrganizeId 
	                    LEFT JOIN [NewtouchHIS_Base]..V_S_xt_gj gj ON gj.gjCode = A.gj 
	                    LEFT JOIN [NewtouchHIS_Base]..V_S_xt_mz mz ON mz.mzCode = A.mz 

                    ";
            var pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("@kh", kh ?? ""));
            pars.Add(new SqlParameter("@cardType", cardType ?? ""));
            pars.Add(new SqlParameter("@zjh", zjh ?? ""));
            pars.Add(new SqlParameter("@OrganizeId", orgId));
            if (ly == null)
            {
                strSql += @" WHERE (@kh='' or (c.CardType = @cardType and c.CardNo = @kh)) 

    AND(A.blh = @blh OR @blh = '') AND(c.CardId = @CardId OR @CardId = '') AND((A.zjh = @zjh and c.CardType = @cardType) or(@zjh = '' and @cardType = @cardType))
	AND a.OrganizeId = @OrganizeId AND A.zt = '1'
ORDER BY A.CreateTime DESC";
               
                pars.Add(new SqlParameter("@blh", blh ?? ""));
                pars.Add(new SqlParameter("@CardId", CardId ?? ""));
            }
            else {
                strSql += @" WHERE (@kh='' or (c.CardType = @cardType and c.CardNo = @kh)) 
    AND(A.zjh = @zjh )
	AND a.OrganizeId = @OrganizeId AND A.zt = '1'
ORDER BY A.CreateTime DESC";

            }

            var patChargeVo = FindList<OutpatAccInfoDto>(strSql, pars.ToArray()).FirstOrDefault();
           
            if (patChargeVo == null && !string.IsNullOrWhiteSpace(kh) && !string.IsNullOrWhiteSpace(cardType))
            {
                throw new FailedCodeException("OUTPAT_REGIST_ISINVALID");
            }
            if (patChargeVo == null && !string.IsNullOrWhiteSpace(zjh))
            {
                throw new FailedCodeException("OUTPAT_REGIST_ISINVALID");
            }
            return patChargeVo;
        }
        /// <summary>
        /// Quyết toán ngoại trú giao dịch 
        /// </summary>
        /// <param name="vo"></param>
        /// <param name="jzjhEntity">Kế hoạch ghi sổ</param>
        /// <param name="jzmxlist">Chi tiết kế hoạch ghi sổ</param>
        public void OutPatChargeSettDBInAcc(OutPatChargeSettInAccDataVo vo, OutpatientAccountEntity jzjhEntity, List<OutpatientAccountDetailEntity> jzmxlist, Dictionary<string, string> optimaId)
        {
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                if (vo.mz_cf != null && vo.mz_cf.Count > 0)
                {
                    foreach (var item in vo.mz_cf)
                    {
                        db.Insert(item);
                    }
                }
                if (vo.mz_cfmxList != null && vo.mz_cfmxList.Count > 0)
                {
                    foreach (var item in vo.mz_cfmxList)
                    {
                        db.Insert(item);
                    }
                }

                if (vo.mz_xmList != null && vo.mz_xmList.Count > 0)
                {
                    foreach (var item in vo.mz_xmList)
                    {
                        db.Insert(item);
                    }
                }

                if (optimaId != null)
                {
                    foreach (var item in optimaId)
                    {
                        var pars = new List<SqlParameter>();
                        pars.Add(new SqlParameter("@Id", item.Key));
                        pars.Add(new SqlParameter("@jfbId", item.Value));
                        pars.Add(new SqlParameter("@userCode", OperatorProvider.GetCurrent().UserCode));
                        pars.Add(new SqlParameter("@clrq", DateTime.Now));
                        string sql = @"UPDATE  dbo.TB_Sync_TreatmentServiceRecord
                                        SET clzt = 2,
                                                jfbId = @jfbId,
                                                zhclr = @userCode,
                                                zhclsj = @clrq
                                        WHERE Id = @Id
                                                AND cxclbz = 1

                                        UPDATE dbo.TB_Sync_TreatmentServiceRecord
                                        SET     clzt = 2 ,
                                                jfbId = @jfbId ,
                                                clr = @userCode ,
                                                clsj = @clrq
                                        WHERE Id = @Id
                                               AND ISNULL(cxclbz, 0) = 0";
                        db.ExecuteSqlCommand(sql, pars.ToArray());
                    }
                }
                if (vo.mz_js != null && vo.mz_js.Count > 0)
                {
                    foreach (var item in vo.mz_js)
                    {
                        db.Insert(item);
                    }

                }
                if (vo.mz_jsmxList != null && vo.mz_jsmxList.Count > 0)
                {
                    foreach (var item in vo.mz_jsmxList)
                    {
                        db.Insert(item);
                    }
                }
                //Kế hoạch ghi sổ
                if (jzjhEntity != null)
                {
                    db.Insert(jzjhEntity);
                }

                //Chi tiết kế hoạch ghi sổ
                if (jzmxlist != null && jzmxlist.Count > 0)
                {
                    foreach (var item in jzmxlist)
                    {
                        db.Insert(item);
                    }
                }

                db.Commit();
            }

        }
        #endregion

        #region optima记账
        /// <summary>
        /// 
        /// </summary>
        /// <param name="admsNum"></param>
        /// <param name="orgId"></param>
        /// <param name="type">Loại bệnh nhân:Ngoại trú/Nhập viện</param>
        /// <returns></returns>
        public OutpatAccInfoDto GetPatInfoInOptima(string admsNum, string orgId, string type)
        {
            OutpatAccInfoDto patChargeVo = null;
            StringBuilder strSql = new StringBuilder();
            if (type == "门诊")
            {
                strSql.Append(@"SELECT  A.patid ,
                                a.blh ,
                                A.xm ,
                                A.xb ,
                                A.csny ,
                                A.zjh ,
                                A.zjlx ,
                                A.zjh ,
                                c.CardNo kh ,
                                a.jsr ,
                                CAST(FLOOR(DATEDIFF(DY, a.csny, GETDATE()) / 365.25) AS INT) nl ,
                                xz.brxz ,
                                xz.brxzmc ,
                                sb.sycs ,
                                A.dh ,
                                A.dybh ,
                                gh.mzh ,
                                A.phone
                        FROM    [dbo].[xt_brjbxx] AS A WITH ( NOLOCK )
                                INNER JOIN dbo.xt_card c ON c.patid = a.patid
                                                            AND A.OrganizeId = c.OrganizeId
                                LEFT JOIN xt_brxz xz ON xz.brxz = a.brxz
                                                        AND xz.OrganizeId = c.OrganizeId
                                LEFT JOIN xt_sbbab sb ON sb.patid = a.patid
                                                         AND a.OrganizeId = sb.OrganizeId
                                RIGHT JOIN dbo.mz_gh gh ON gh.patid = a.patid
                                   AND gh.OrganizeId = c.OrganizeId
                                WHERE (gh.mzh = @mzh OR @mzh='') AND a.OrganizeId = @OrganizeId");
                DbParameter[] par =
                {
                new SqlParameter("@mzh", admsNum??""),
                new SqlParameter("@OrganizeId", orgId) };
                patChargeVo = FirstOrDefault<OutpatAccInfoDto>(strSql.ToString(), par.ToArray());
            }
            else if (type == "住院")
            {
                strSql.Append(@"SELECT  A.patid ,
                                a.blh ,
                                A.xm ,
                                A.xb ,
                                A.csny ,
                                A.zjh ,
                                A.zjlx ,
                                A.zjh ,
                                c.CardNo kh ,
                                a.jsr ,
                                CAST(FLOOR(DATEDIFF(DY, a.csny, GETDATE()) / 365.25) AS INT) nl ,
                                xz.brxz ,
                                xz.brxzmc ,
                                sb.sycs ,
                                A.dh ,
                                A.dybh ,
                                zy.zyh mzh ,
                                A.phone
                        FROM    [dbo].[xt_brjbxx] AS A WITH ( NOLOCK )
                                INNER JOIN dbo.xt_card c ON c.patid = a.patid
                                                            AND A.OrganizeId = c.OrganizeId
                                LEFT JOIN xt_brxz xz ON xz.brxz = a.brxz
                                                        AND xz.OrganizeId = c.OrganizeId
                                LEFT JOIN xt_sbbab sb ON sb.patid = a.patid
                                                         AND a.OrganizeId = sb.OrganizeId
                                RIGHT JOIN dbo.zy_brjbxx zy ON zy.patid = a.patid
                                                               AND zy.OrganizeId = c.OrganizeId
                        WHERE   ( zy.zyh = @zyh
                                  OR @zyh = ''
                                )
                                AND ( zy.zybz = 1
                                      OR zy.zybz = 2
                                    )
                                AND a.OrganizeId = @OrganizeId");
                DbParameter[] par =
                {
                new SqlParameter("@zyh", admsNum??""),
                new SqlParameter("@OrganizeId", orgId) };
                patChargeVo = FirstOrDefault<OutpatAccInfoDto>(strSql.ToString(), par.ToArray());
            }
            else
            {
                throw new FailedException("病人类型不正确！");
            }

            return patChargeVo;
        }


        /// <summary>
        /// theo số khám ngoại trú lấy lịch sử ghi sổ ngoại trú hạng mục(Đ ãxác nhận vàthủ côngghi sổ)
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<OptimAccInfoDto> GetOutAccountInfo(string mzh, string orgId, string kssj, string jssj, string rygh)
        {
            if (string.IsNullOrWhiteSpace(mzh))
            {
                throw new FailedException("门诊号不能为空");
            }

            if (string.IsNullOrWhiteSpace(orgId))
            {
                throw new FailedException("组织机构不能为空");
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"   SELECT  DISTINCT
                                CONVERT(VARCHAR(50), NEWID()) newid ,
                                '2' yzlx ,
                                xm.sfxm sfxmCode ,
                                CONVERT(VARCHAR(50), xm.xmnm) jfbId ,
                                xm.sfxm sfxm ,
                                xt_sfxm.sfxmmc sfxmmc ,
                                xm.kflb ,
                                xm.dl sfdlCode ,
                                xm.ysmc ,
                                xm.ys ,
                                xm.ks ,
                                xm.ksmc ,
                                sfdl.dlmc sfdlmc ,
                                CAST(xm.dj AS DECIMAL) AS dj ,
                                CAST(xm.sl AS INT) sl ,
                                CAST(xm.dj * xm.sl AS VARCHAR) AS JE ,
                                xmnm XMNM ,
                                xm.zfxz ZFXZ ,
                                xm.ghnm GHNM ,
                                xm.duration ,
                                xt_sfxm.dw dw ,
                                '' ,
                                xm.ssrq jzsj,
                                CAST(xm.ttbz AS VARCHAR) AS ttbz ,
                                gh.mzh,tr.clzt
                         FROM   mz_xm xm
                                LEFT JOIN mz_gh gh ON xm.ghnm = gh.ghnm
                                AND xm.OrganizeId = gh.OrganizeId
            LEFT JOIN dbo.TB_Sync_TreatmentServiceRecord tr ON tr.admsNum = CAST(gh.mzh AS VARCHAR) AND jfbId=xm.xmnm
            AND tr.siteId = xm.OrganizeId   AND clzt = 2
                                AND LOWER(tr.patientType) = 'outpatient'
                                AND wtjlbz =0 AND tr.zt=1
                                LEFT JOIN NewtouchHIS_Base..V_S_xt_sfxm xt_sfxm ON xm.sfxm = xt_sfxm.sfxmCode
                                                                                   AND xt_sfxm.OrganizeId = @OrganizeId
                                LEFT JOIN NewtouchHIS_Base..V_S_xt_sfdl sfdl ON xm.dl = sfdl.dlcode
                                                                                AND sfdl.OrganizeId = @OrganizeId
                         WHERE  xm.OrganizeId = @OrganizeId
                                AND gh.mzh = @mzh
                                AND xm.zt = 1
                                AND gh.zt = 1 
                                and (@kssj='' or xm.CreateTime>@kssj) 
                                and (@jssj='' or xm.CreateTime<(DATEADD(DAY, 1, @jssj)))
                                and xm.ys=@rygh
                         UNION ALL
                         SELECT DISTINCT
                                CONVERT(VARCHAR(50), NEWID()) newid ,
                                '1' yzlx ,
                                mz_cfmx.yp AS sfxmCode ,
                                 CONVERT(VARCHAR(50), cfmxId) jfbId ,
                                yp sfxm ,
                                ypmc sfxmmc ,
                                mz_cfmx.kflb ,
                                mz_cfmx.dl sfdlCode ,
                                mz_cf.ysmc ,
                                mz_cf.ys ,
                                mz_cf.ks ,
                                mz_cf.ksmc ,
                                dlmc sfdlmc ,
                                CONVERT(DECIMAL, ( mz_cfmx.dj + mz_cfmx.fwfdj )) dj ,
                                CONVERT(INT, dbo.mz_cfmx.sl) sl ,
                                CONVERT(VARCHAR(50), ( mz_cfmx.sl * ( mz_cfmx.dj + mz_cfmx.fwfdj ) )) AS je ,
                                mz_cf.cfnm XMNM ,
                                mz_cfmx.zfxz ,
                                mz_cf.ghnm ,
                                0 AS duration ,
                                djdw AS dw ,
                                mz_cfmx.bz ,
                                dbo.mz_cf.jsrq jzsj,
                                '0' ttbz ,
                                gh.mzh,
                                2 clzt
                                FROM   mz_gh gh
						         LEFT JOIN mz_cf ON dbo.mz_cf.ghnm = gh.ghnm
                                                   AND mz_cf.OrganizeId = @OrganizeId
                                LEFT JOIN mz_cfmx ON mz_cfmx.cfnm =mz_cf.cfnm
                                                     AND dbo.mz_cfmx.OrganizeId = gh.OrganizeId
                                LEFT JOIN NewtouchHIS_Base..V_S_xt_yp xt_yp ON mz_cfmx.yp = xt_yp.ypCode
                                                                               AND xt_yp.OrganizeId = @OrganizeId
                                LEFT JOIN NewtouchHIS_Base..V_S_xt_ypsx xt_ypsx ON mz_cfmx.yp = xt_ypsx.ypCode
                                                                                   AND xt_ypsx.OrganizeId = @OrganizeId
                                LEFT JOIN NewtouchHIS_Base..V_S_xt_sfdl xt_sfdl ON mz_cfmx.dl = xt_sfdl.dlCode
                                                                                   AND xt_sfdl.OrganizeId = @OrganizeId
                         WHERE  gh.OrganizeId = @OrganizeId
                                AND gh.mzh = @mzh
                                AND mz_cfmx.zt = 1
                                AND mz_cf.zt = 1
                                AND gh.zt = 1
                                and (@kssj='' or mz_cfmx.CreateTime>@kssj) 
                                and (@jssj='' or mz_cfmx.CreateTime<(DATEADD(DAY, 1, @jssj)))
                                and mz_cf.ys=@rygh");
            DbParameter[] par =
            {
                new SqlParameter("@mzh", mzh),
                new SqlParameter("@OrganizeId", orgId),
                new SqlParameter("@kssj", kssj),
                new SqlParameter("@jssj", jssj),
                new SqlParameter("@rygh", rygh)

            };
            return FindList<OptimAccInfoDto>(strSql.ToString(), par);
        }

        /// <summary>
        /// theo số nhập viện lấy lịch sử ghi sổ nhập viện hạng mục(Đ ãxác nhận vàthủ côngghi sổ)
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<OptimAccInfoDto> GetintAccountInfo(string zyh, string orgId, string kssj, string jssj, string rygh)
        {
            if (string.IsNullOrWhiteSpace(zyh))
            {
                throw new FailedException("住院号不能为空");
            }

            if (string.IsNullOrWhiteSpace(orgId))
            {
                throw new FailedException("组织机构不能为空");
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"  SELECT  DISTINCT
        CONVERT(VARCHAR(50), NEWID()) newid ,
        '2' yzlx ,
        xm.sfxm sfxmCode ,
        CONVERT(VARCHAR(50), xm.jfbbh) jfbId ,
        xm.sfxm sfxm ,
        xt_sfxm.sfxmmc sfxmmc ,
        xm.kflb ,
        xm.dl sfdlCode ,
        xm.ysmc ,
        xm.ys ,
        xm.ks ,
        xm.ksmc ,
        sfdl.dlmc sfdlmc ,
        CAST(xm.dj AS DECIMAL) AS dj ,
        CAST(xm.sl AS INT) sl ,
        CAST(xm.dj * xm.sl AS VARCHAR) AS JE ,
        xm.zfxz ZFXZ ,
        xm.duration ,
        xt_sfxm.dw dw ,
        xm.tdrq jzsj ,
        CAST(xm.ttbz AS VARCHAR) AS ttbz ,
        tr.admsNum mzh,
		tr.clzt
      FROM    dbo.zy_xmjfb xm
        LEFT JOIN dbo.TB_Sync_TreatmentServiceRecord tr ON tr.admsNum =xm.zyh
                                                           AND  jfbId = xm.jfbbh
                                                           AND tr.siteId = xm.OrganizeId
        AND wtjlbz = 0  AND clzt = 2  AND LOWER(tr.patientType) = 'inpatient'
        LEFT JOIN NewtouchHIS_Base..V_S_xt_sfxm xt_sfxm ON xm.sfxm = xt_sfxm.sfxmCode
                                                           AND xt_sfxm.OrganizeId =@OrganizeId
        LEFT JOIN NewtouchHIS_Base..V_S_xt_sfdl sfdl ON xm.dl = sfdl.dlcode
                                                        AND sfdl.OrganizeId =@OrganizeId
        WHERE   xm.OrganizeId =@OrganizeId
        AND xm.zyh  = @mzh  AND xm.zt = 1   
         and (@kssj='' or xm.CreateTime>@kssj) 
                                and (@jssj='' or xm.CreateTime<(DATEADD(DAY, 1, @jssj)))
                                and xm.ys=@rygh
 UNION ALL
 SELECT DISTINCT
        CONVERT(VARCHAR(50), NEWID()) newid ,
        '1' yzlx ,
        jfb.yp AS sfxmCode ,
         CONVERT(VARCHAR(50), jfb.jfbbh) jfbId ,
        yp sfxm ,
        ypmc sfxmmc ,
        jfb.kflb ,
        jfb.dl sfdlCode ,
        jfb.ysmc ,
        jfb.ys ,
        jfb.ks ,
        jfb.ksmc ,
        dlmc sfdlmc ,
        CONVERT(DECIMAL(18,2), jfb.dj) dj ,
        CONVERT(INT, jfb.sl) sl ,
        CONVERT(VARCHAR(50), ( jfb.sl *jfb.dj)) AS je ,
        jfb.zfxz ,
        0 AS duration ,
        djdw AS dw ,
        jfb.tdrq jzsj,
        '0' ttbz ,
       jfb.zyh mzh,
	    '' clzt
 FROM   dbo.zy_ypjfb jfb    
        LEFT JOIN NewtouchHIS_Base..V_S_xt_yp xt_yp ON jfb.yp = xt_yp.ypCode
                                                       AND xt_yp.OrganizeId = @OrganizeId
        LEFT JOIN NewtouchHIS_Base..V_S_xt_ypsx xt_ypsx ON jfb.yp = xt_ypsx.ypCode
                                                           AND xt_ypsx.OrganizeId = @OrganizeId
        LEFT JOIN NewtouchHIS_Base..V_S_xt_sfdl xt_sfdl ON jfb.dl = xt_sfdl.dlCode
                                                           AND xt_sfdl.OrganizeId = @OrganizeId
 WHERE  jfb.OrganizeId = @OrganizeId
        AND jfb.zyh= @mzh
        AND jfb.zt = 1
 and (@kssj='' or jfb.CreateTime>@kssj) 
                                and (@jssj='' or jfb.CreateTime<(DATEADD(DAY, 1, @jssj)))
                                and jfb.ys=@rygh");
            DbParameter[] par =
            {
                new SqlParameter("@mzh", zyh),
                new SqlParameter("@OrganizeId", orgId),
                new SqlParameter("@kssj", kssj),
                new SqlParameter("@jssj", jssj),
                new SqlParameter("@rygh", rygh)

            };
            return FindList<OptimAccInfoDto>(strSql.ToString(), par);
        }

        /// <summary>
        /// optimaGửi
        /// </summary>
        /// <param name="outaddvo">Ngoại trú thêm mớighi sổ</param>
        /// <param name="outupdatevo">Ngoại trú sửaghi sổ</param>
        /// <param name="outdelvo">Ngoại trú xóaghi sổ</param>
        /// <param name="inaddvo">Nhập việnThêm mớiGhi sổ</param>
        /// <param name="inupdatevo">Nhập việnSửaGhi sổ</param>
        /// <param name="indelvo">Nhập việnXóaGhi sổ</param>
        /// <param name="orgId"></param>
        public void PatsettDBInOptima(SettInAccOutpatDataVo outaddvo,
            updateInAccOutpatVo outupdatevo,
            delInAccOutpatVo outdelvo,
            SettInAccHospatDataVo inaddvo,
            updateInAccHospatVo inupdatevo,
            delInAccHospatVo indelvo,
            string orgId,
            List<int> cfmxIds,
            Dictionary<string, string> optimaIds)
        {
            var usercode = OperatorProvider.GetCurrent().UserCode;
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                #region 新增
                if (outaddvo != null)
                {
                    if (outaddvo.mz_cf != null && outaddvo.mz_cf.Count > 0)
                    {
                        foreach (var item in outaddvo.mz_cf)
                        {
                            db.Insert(item);
                        }
                    }

                    if (outaddvo.mz_cfmxList != null && outaddvo.mz_cfmxList.Count > 0)
                    {
                        foreach (var item in outaddvo.mz_cfmxList)
                        {
                            db.Insert(item);
                        }
                    }

                    if (outaddvo.mz_xmList != null && outaddvo.mz_xmList.Count > 0)
                    {
                        foreach (var item in outaddvo.mz_xmList)
                        {
                            db.Insert(item);
                        }
                    }

                }
                if (inaddvo != null)
                {
                    if (inaddvo.zy_xmjfbList != null && inaddvo.zy_xmjfbList.Count > 0)
                    {
                        foreach (var item in inaddvo.zy_xmjfbList)
                        {
                            db.Insert(item);
                        }
                    }
                    if (inaddvo.zy_ypjfbList != null && inaddvo.zy_ypjfbList.Count > 0)
                    {
                        foreach (var item in inaddvo.zy_ypjfbList)
                        {
                            db.Insert(item);
                        }
                    }
                }
                if (optimaIds != null && optimaIds.Count() > 0)
                {
                    foreach (var item in optimaIds)
                    {
                        var pars = new List<SqlParameter>();
                        pars.Add(new SqlParameter("@Id", item.Key));
                        pars.Add(new SqlParameter("@jfbId", item.Value));
                        pars.Add(new SqlParameter("@userCode", OperatorProvider.GetCurrent().UserCode));
                        pars.Add(new SqlParameter("@clrq", DateTime.Now));
                        string sql = @"UPDATE  dbo.TB_Sync_TreatmentServiceRecord
                                        SET clzt = 2,
                                                jfbId = @jfbId,
                                                zhclr = @userCode,
                                                zhclsj = @clrq
                                        WHERE Id = @Id
                                                AND cxclbz = 1

                                        UPDATE dbo.TB_Sync_TreatmentServiceRecord
                                        SET     clzt = 2 ,
                                                jfbId = @jfbId ,
                                                clr = @userCode ,
                                                clsj = @clrq
                                        WHERE Id = @Id
                                               AND ISNULL(cxclbz, 0) = 0";
                        db.ExecuteSqlCommand(sql, pars.ToArray());
                    }
                }
                #endregion

                #region 修改
                if (outupdatevo != null)
                {
                    if (outupdatevo.mz_cfmxList != null && outupdatevo.mz_cfmxList.Count > 0)
                    {
                        foreach (var item in outupdatevo.mz_cfmxList.Keys)
                        {
                            db.Update(outupdatevo.mz_cfmxList[item]);
                            //Lưu nhật ký thay đ ổi
                            if (outupdatevo.mz_cfmxList.Keys != null)
                            {
                                AppLogger.WriteEntityChangeRecordLog(item, outupdatevo.mz_cfmxList[item], OutpatientPrescriptionDetailEntity.GetTableName(), item.cfmxId.ToString());
                            }
                        }
                    }

                    if (outupdatevo.mz_xmList != null && outupdatevo.mz_xmList.Count > 0)
                    {
                        foreach (var item in outupdatevo.mz_xmList.Keys)
                        {
                            db.Update(outupdatevo.mz_xmList[item]);
                            //Lưu nhật ký thay đ ổi
                            if (outupdatevo.mz_xmList.Keys != null)
                            {
                                AppLogger.WriteEntityChangeRecordLog(item, outupdatevo.mz_xmList[item], OutpatientItemEntity.GetTableName(), item.xmnm.ToString());
                            }
                        }
                    }
                }

                if (inupdatevo != null)
                {
                    if (inupdatevo.zy_xmjfbList != null && inupdatevo.zy_xmjfbList.Count > 0)
                    {
                        foreach (var item in inupdatevo.zy_xmjfbList.Keys)
                        {
                            db.Update(inupdatevo.zy_xmjfbList[item]);
                            //Lưu nhật ký thay đ ổi
                            if (inupdatevo.zy_xmjfbList.Keys != null)
                            {
                                AppLogger.WriteEntityChangeRecordLog(item, inupdatevo.zy_xmjfbList[item], HospItemBillingEntity.GetTableName(), item.jfbbh.ToString());
                            }
                        }
                    }
                    if (inupdatevo.zy_ypjfbList != null && inupdatevo.zy_ypjfbList.Count > 0)
                    {
                        foreach (var item in inupdatevo.zy_ypjfbList.Keys)
                        {
                            db.Update(inupdatevo.zy_ypjfbList[item]);
                            //Lưu nhật ký thay đ ổi
                            if (inupdatevo.zy_ypjfbList.Keys != null)
                            {
                                AppLogger.WriteEntityChangeRecordLog(item, inupdatevo.zy_ypjfbList[item], HospDrugBillingEntity.GetTableName(), item.jfbbh.ToString());
                            }
                        }
                    }
                }
                #endregion

                #region 删除

                if (outdelvo != null)
                {
                    //Xóachi tiết đ ơn thuốc
                    if (outdelvo.mz_cfmxList != null && outdelvo.mz_cfmxList.Count > 0)
                    {
                        var cfmxIdslist = "'" + string.Join("','", outdelvo.mz_cfmxList.Distinct().ToArray()) + "'";
                        db.ExecuteSqlCommand("UPDATE dbo.mz_cfmx SET zt=0,LastModifierCode=@usercode,LastModifyTime=GETDATE() WHERE cfmxId IN (" + cfmxIdslist + ") AND OrganizeId = @orgId;", new[] { new SqlParameter("@orgId", orgId), new SqlParameter("@usercode", usercode) });
                    }
                    if (outdelvo.mz_xmList != null && outdelvo.mz_xmList.Count > 0)
                    {
                        var stringlist = "'" + string.Join("','", outdelvo.mz_xmList.Distinct().ToArray()) + "'";
                        db.ExecuteSqlCommand("UPDATE dbo.mz_xm SET zt=0,LastModifierCode=@usercode,LastModifyTime=GETDATE() WHERE xmnm IN (" + stringlist + ")  AND OrganizeId = @orgId;", new[] { new SqlParameter("@orgId", orgId), new SqlParameter("@usercode", usercode) });
                    }
                }

                if (indelvo != null)
                {
                    if (indelvo.zy_xmjfbList != null && indelvo.zy_xmjfbList.Count > 0)
                    {
                        var stringlist = "'" + string.Join("','", indelvo.zy_xmjfbList.Distinct().ToArray()) + "'";
                        db.ExecuteSqlCommand("UPDATE dbo.zy_xmjfb SET zt=0,LastModifierCode=@usercode,LastModifyTime=GETDATE() WHERE jfbbh IN (" + stringlist + ")  AND OrganizeId = @orgId;", new[] { new SqlParameter("@orgId", orgId), new SqlParameter("@usercode", usercode) });
                    }
                    if (indelvo.zy_ypjfbList != null && indelvo.zy_ypjfbList.Count > 0)
                    {
                        var stringlist = "'" + string.Join("','", indelvo.zy_ypjfbList.Distinct().ToArray()) + "'";
                        db.ExecuteSqlCommand("UPDATE dbo.zy_ypjfb SET zt=0,LastModifierCode=@usercode,LastModifyTime=GETDATE() WHERE jfbbh IN (" + stringlist + ")  AND OrganizeId = @orgId;", new[] { new SqlParameter("@orgId", orgId), new SqlParameter("@usercode", usercode) });
                    }
                }
                #endregion
                db.Commit();
            }
            using (var db2 = new EFDbTransaction(_databaseFactory).BeginTrans())
            {
                #region 修改处方主表
                if (cfmxIds != null && cfmxIds.Count() > 0)
                {
                    foreach (var cfmxid in cfmxIds)
                    {
                        //Lấy xóa mã nội bộ đ ơn thuốc
                        var cfnm = db2.FirstOrDefault<int>(@"SELECT DISTINCT
                                                            cfmx.cfnm
                                                    FROM    dbo.mz_cf cf
                                                            LEFT JOIN dbo.mz_cfmx mx ON mx.cfnm = cf.cfnm
                                                                                        AND cf.OrganizeId = mx.OrganizeId
                                                    WHERE   mx.cfmxId = @cfmxId
                                                            AND cfmx.zt = 0
                                                            AND js.OrganizeId = @orgId",
                                                            new[] { new SqlParameter("@cfmxId", cfmxid),
                                                                new SqlParameter("@orgId", orgId) });
                        // phán đoán Đ ơn thuốc có hay không tồn tại tại khác của chưa xóa
                        var cfnmExists = db2.FirstOrDefault<int>(@"SELECT  COUNT(1)
                        FROM    dbo.mz_cfmx
                        WHERE   cfnm = @cfnm AND zt=1 AND OrganizeId=@orgId",
                        new[] { new SqlParameter("@jsnm", cfnm),
                            new SqlParameter("@orgId", orgId),
                            new SqlParameter("@cfnm", cfnm) });
                        if (cfnmExists > 0)
                        {
                            //Cập nh ậtĐ ơn thuốc bảng chính tổng số tiền
                            db2.ExecuteSqlCommand(@"UPDATE  dbo.mz_cf
                                    SET     zje = (SELECT  SUM(dj * sl),LastModifierCode=@usercode,LastModifyTime=GETDATE()
                                                    FROM    dbo.mz_cfmx
                                                    WHERE   cfnm = @cfnm and zt=1
                                                  )
                                    WHERE cfnm = @cfnm
                                            AND OrganizeId = @orgId ", new[] { new SqlParameter("@cfnm", cfnm), new SqlParameter("@orgId", orgId), new SqlParameter("@usercode", usercode) });
                        }
                    }
                }
                #endregion
                db2.Commit();
            }
        }
        #endregion

        #region 门诊收费 Vision-1.3 门诊记账的第三个版本
        /// <summary>
        /// Lấy thời gian đoạn trong bệnh nhân tất cả các Đ ăng ký khám vàtiêu dùngtổng số tiền
        /// </summary>
        public IList<OutPatAccountingDto> GetRegisterGridJson(string begindate, string enddate, string orgId, string usercode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT  gh.xm ,
                                    gh.blh ,
                                    gh.mzh ,
                                    gh.ghrq,
                                    ( SELECT    SUM(js.zje)
                                      FROM      mz_js js
                                      WHERE     js.ghnm = gh.ghnm AND js.zt=1
                                                AND gh.OrganizeId = js.OrganizeId
                                    ) zje
                            FROM    dbo.mz_gh gh
                            WHERE    (@begindate='' or ghrq > DATEDIFF(DAY,1,@begindate))
                                   and( @enddate='' or ghrq < DATEADD(DAY, 1, @enddate))
                                  AND gh.OrganizeId=@orgId  AND gh.CreatorCode=@usercode AND gh.zt=1 AND mzh IS NOT NULL ORDER BY zje");
            DbParameter[] par =
            {
                new SqlParameter("@begindate", begindate ?? ""),
                new SqlParameter("@enddate", enddate??""),
                new SqlParameter("@orgId", orgId),
                new SqlParameter("@usercode",usercode )

            };
            return FindList<OutPatAccountingDto>(strSql.ToString(), par);
        }

        /// <summary>
        /// theo số khám ngoại trú lấy lịch sử hạng mục thu phí
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<OptimAccInfoDto> GetpatientAccountInfo(string mzh, string orgId)
        {
            if (string.IsNullOrWhiteSpace(mzh))
            {
                throw new FailedException("门诊号不能为空");
            }

            if (string.IsNullOrWhiteSpace(orgId))
            {
                throw new FailedException("组织机构不能为空");
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT   CONVERT(VARCHAR(50), NEWID()) newid ,
        '2' yzlx ,
        mz_xm.sfxm sfxmCode ,
        mz_jsmx.jsmxnm ,
        mz_xm.sfxm sfxm ,
        xt_sfxm.sfxmmc sfxmmc ,
        mz_xm.kflb ,
        mz_xm.dl sfdlCode ,
        mz_xm.ysmc ,
        mz_xm.ys ,
        mz_xm.ks ,
        mz_xm.ksmc ,
        sfdl.dlmc sfdlmc ,
        CAST(mz_xm.dj AS DECIMAL) AS dj ,
        CAST(mz_xm.sl AS INT) sl ,
        CAST(mz_xm.dj * mz_xm.sl AS VARCHAR) AS JE ,
        xmnm XMNM ,
        mz_xm.zfxz zfxz ,
        mz_xm.zfbl,
        mz_xm.ghnm GHNM ,
		mz_xm.duration,
        xt_sfxm.dw dw ,
        mz_xm.bz ,
        mz_xm.ssrq jzsj ,
        CAST(mz_xm.ttbz AS VARCHAR) AS ttbz ,
        gh.mzh --,
        --mx.zll ,
        --mx.zxzt ,
        --mx.jzjhmxId ,
        --mx.StartDate ,
        --mx.EndDate ,
        --mx.yzxz
 FROM   mz_gh gh
        INNER JOIN mz_js js ON js.ghnm = gh.ghnm AND js.OrganizeId = gh.OrganizeId
		left join mz_jsmx jsmx on jsmx.jsnm = js.jsnm AND jsmx.OrganizeId = js.OrganizeId
        LEFT JOIN mz_xm ON mz_xm.xmnm = jsmx.mxnm AND mz_xm.OrganizeId = jsmx.OrganizeId
        --LEFT JOIN dbo.mz_jzjhmx mx ON mx.jzjhmxId = mz_xm.jzjhmxId
        --                              AND mx.OrganizeId = @orgId
        LEFT JOIN NewtouchHIS_Base..V_S_xt_sfxm xt_sfxm ON mz_xm.sfxm = xt_sfxm.sfxmCode
                                                           AND xt_sfxm.OrganizeId = @orgId
        LEFT JOIN NewtouchHIS_Base..V_S_xt_sfdl sfdl ON mz_xm.dl = sfdl.dlcode
                                                        AND sfdl.OrganizeId = @orgId
        LEFT JOIN mz_jsmx ON mz_jsmx.mxnm = mz_xm.xmnm
                             AND mz_jsmx.jslx != '0'
                             AND mz_jsmx.OrganizeId = @orgId
 WHERE  mz_xm.OrganizeId = @orgId
        AND gh.mzh = @mzh
        AND mz_xm.zt = '1'
        --AND mx.zt = '1'
        AND mz_jsmx.zt = '1'
        AND gh.zt = '1'
--UNION ALL
-- SELECT DISTINCT
--        CONVERT(VARCHAR(50), NEWID()) newid ,
--        '1' yzlx ,
--        mz_cfmx.yp AS sfxmCode ,
--        mz_jsmx.jsmxnm ,
--        yp sfxm ,
--        ypmc sfxmmc ,
--        mz_cfmx.kflb ,
--        mz_cfmx.dl sfdlCode ,
--        mz_cf.ysmc ,
--        mz_cf.ys ,
--        mz_cf.ks ,
--        mz_cf.ksmc ,
--        dlmc sfdlmc ,
--        CONVERT(DECIMAL, ( mz_cfmx.dj )) dj ,
--        CONVERT(INT, mz_cfmx.sl) sl ,
--        CONVERT(VARCHAR(50), ( mz_cfmx.sl * ( mz_cfmx.dj ) )) AS je ,
--        mz_cf.cfnm XMNM ,
--        mz_cfmx.zfxz ,
--        mz_cfmx.zfbl,
--        mz_cf.ghnm ,
--        0 AS duration ,
--        djdw AS dw ,
--        mz_cfmx.bz ,
--        mz_cf.jsrq jzsj ,
--        '0' ttbz ,
--        gh.mzh --,
--        --mx.zll ,
--        --mx.zxzt ,
--        --mx.jzjhmxId ,
--        --mx.StartDate ,
--        --mx.EndDate ,
--        --mx.yzxz
-- FROM   mz_gh gh
--        INNER JOIN mz_js js ON js.ghnm = gh.ghnm
--                               AND js.OrganizeId = gh.OrganizeId
--        LEFT JOIN mz_cf ON js.jsnm = mz_cf.jsnm
--                           AND mz_cf.OrganizeId = @orgId
--        LEFT JOIN mz_cfmx ON mz_cfmx.cfnm = mz_cf.cfnm
--                             AND dbo.mz_cfmx.OrganizeId = gh.OrganizeId
--        --LEFT JOIN dbo.mz_jzjhmx mx ON mx.jzjhmxId = mz_cfmx.jzjhmxId
--        LEFT JOIN NewtouchHIS_Base..V_S_xt_yp xt_yp ON mz_cfmx.yp = xt_yp.ypCode
--                                                       AND xt_yp.OrganizeId = @orgId
--        LEFT JOIN NewtouchHIS_Base..V_S_xt_ypsx xt_ypsx ON mz_cfmx.yp = xt_ypsx.ypCode
--                                                           AND xt_ypsx.OrganizeId = @orgId
--        LEFT JOIN NewtouchHIS_Base..V_S_xt_sfdl xt_sfdl ON mz_cfmx.dl = xt_sfdl.dlCode
--                                                           AND xt_sfdl.OrganizeId = @orgId
--        INNER JOIN mz_jsmx ON mz_jsmx.jslx != '0'
--                              AND mz_jsmx.OrganizeId = @orgId
--                                                      --AND mz_jsmx.mxbm = mz_cfmx.yp
--                              AND mz_jsmx.cf_mxnm = mz_cfmx.cfmxId
-- WHERE  gh.OrganizeId = @orgId
--        AND gh.mzh = @mzh
--        AND mz_cfmx.zt = 1
--        AND mz_cf.zt = 1
--        --AND mx.zt = 1
--        AND mz_jsmx.zt = 1
--        AND gh.zt = 1

order by js.jsnm");
            DbParameter[] par =
            {
                new SqlParameter("@mzh", mzh),
                new SqlParameter("@orgId", orgId)

            };
            return FindList<OptimAccInfoDto>(strSql.ToString(), par);
        }

        /// <summary>
        /// Lưughi sổ,dữ liệu kho lưu
        /// </summary>
        /// <param name="vo">Thêm m ớiĐ ối tư ợng tập hợp </param>
        /// <param name="updatevo">SửaĐ ối tư ợng tập hợp </param>
        /// <param name="jsnmList">Sửa và xóa của mã nội bộ quyết toán tập hợp </param>
        /// <param name="delvo">XóaĐ ối tư ợng tập hợp </param>
        /// <param name="orgId"></param>
        public void OutPatsettDBInCharge(OutPatSettInAccDataVo vo, updatePatSettInAccDataVo updatevo, List<int> jsnmList, DelPatSettInAccDataVo delvo, string orgId)
        {
            var usercode = OperatorProvider.GetCurrent().UserCode;
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                #region 新增
                //if (vo.mz_cf != null && vo.mz_cf.Count > 0)
                //{
                //    foreach (var item in vo.mz_cf)
                //    {
                //        db.Insert(item);
                //    }
                //}
                //if (vo.mz_cfmxList != null && vo.mz_cfmxList.Count > 0)
                //{
                //    foreach (var item in vo.mz_cfmxList)
                //    {
                //        db.Insert(item);
                //    }
                //}

                if (vo.mz_xmList != null && vo.mz_xmList.Count > 0)
                {
                    foreach (var item in vo.mz_xmList)
                    {
                        db.Insert(item);
                    }
                }
                if (vo.mz_js != null && vo.mz_js.Count > 0)
                {
                    foreach (var item in vo.mz_js)
                    {
                        db.Insert(item);
                    }

                }
                if (vo.mz_jsmxList != null && vo.mz_jsmxList.Count > 0)
                {
                    foreach (var item in vo.mz_jsmxList)
                    {
                        db.Insert(item);
                    }
                }
                ////Kế hoạch ghi sổ
                //if (vo.mz_jzjh != null)
                //{
                //    db.Insert(vo.mz_jzjh);
                //}

                ////Chi tiết kế hoạch ghi sổ
                //if (vo.mz_jzjhmx != null && vo.mz_jzjhmx.Count > 0)
                //{
                //    foreach (var item in vo.mz_jzjhmx)
                //    {
                //        db.Insert(item);
                //    }
                //}
                #endregion

                #region 修改
                //if (updatevo.mz_cfmxList != null && updatevo.mz_cfmxList.Count > 0)
                //{
                //    foreach (var item in updatevo.mz_cfmxList.Keys)
                //    {
                //        db.Update(updatevo.mz_cfmxList[item]);
                // //Lưu nhật ký thay đ ổi
                //        if (updatevo.mz_cfmxList.Keys != null)
                //        {
                //            AppLogger.WriteEntityChangeRecordLog(item, updatevo.mz_cfmxList[item], OutpatientPrescriptionDetailEntity.GetTableName(), item.cfmxId.ToString());
                //        }
                //    }
                //}
                if (updatevo.mz_jsmxList != null && updatevo.mz_jsmxList.Count > 0)
                {
                    foreach (var item in updatevo.mz_jsmxList.Keys)
                    {
                        db.Update(updatevo.mz_jsmxList[item]);
                        //Lưu nhật ký thay đ ổi
                        if (updatevo.mz_jsmxList.Keys != null)
                        {
                            AppLogger.WriteEntityChangeRecordLog(item, updatevo.mz_jsmxList[item], OutpatientSettlementDetailEntity.GetTableName(), item.jsmxnm.ToString());
                        }
                    }
                }
                //if (updatevo.mz_jzjhmx != null && updatevo.mz_jzjhmx.Count > 0)
                //{
                //    foreach (var item in updatevo.mz_jzjhmx.Keys)
                //    {
                //        db.Update(updatevo.mz_jzjhmx[item]);
                // //Lưu nhật ký thay đ ổi
                //        if (updatevo.mz_jzjhmx.Keys != null)
                //        {
                //            AppLogger.WriteEntityChangeRecordLog(item, updatevo.mz_jzjhmx[item], OutpatientAccountDetailEntity.GetTableName(), item.jzjhmxId.ToString());
                //        }
                //    }
                //}
                if (updatevo.mz_xmList != null && updatevo.mz_xmList.Count > 0)
                {
                    foreach (var item in updatevo.mz_xmList.Keys)
                    {
                        db.Update(updatevo.mz_xmList[item]);
                        //Lưu nhật ký thay đ ổi
                        if (updatevo.mz_xmList.Keys != null)
                        {
                            AppLogger.WriteEntityChangeRecordLog(item, updatevo.mz_xmList[item], OutpatientItemEntity.GetTableName(), item.xmnm.ToString());
                        }
                    }
                }
                #endregion

                #region 删除
                //Xóabảng con 
                //if (delvo.mz_cfmxList != null && delvo.mz_cfmxList.Count > 0)
                //{
                //    var stringlist = "'" + string.Join("','", delvo.mz_cfmxList.Distinct().ToArray()) + "'";
                //    db.ExecuteSqlCommand("UPDATE dbo.mz_cfmx SET zt=0,LastModifierCode=@usercode,LastModifyTime=GETDATE() WHERE cfmxId IN (" + stringlist + ") AND OrganizeId = @orgId;", new[] { new SqlParameter("@orgId", orgId), new SqlParameter("@usercode", usercode) });
                //}
                if (delvo.mz_jsmxList != null && delvo.mz_jsmxList.Count > 0)
                {
                    var stringlist = "'" + string.Join("','", delvo.mz_jsmxList.Distinct().ToArray()) + "'";
                    db.ExecuteSqlCommand("UPDATE dbo.mz_jsmx SET zt=0,LastModifierCode=@usercode,LastModifyTime=GETDATE() WHERE jsmxnm IN (" + stringlist + ")  AND OrganizeId = @orgId;", new[] { new SqlParameter("@orgId", orgId), new SqlParameter("@usercode", usercode) });

                }
                //var jzjhmxIdslist = "'" + string.Join("','", delvo.mz_jzjhmx.Distinct().ToArray()) + "'";
                //if (delvo.mz_jzjhmx != null && delvo.mz_jzjhmx.Count > 0)
                //{
                //    db.ExecuteSqlCommand("UPDATE dbo.mz_jzjhmx SET zt=0,LastModifierCode=@usercode,LastModifyTime=GETDATE() WHERE jzjhmxId IN (" + jzjhmxIdslist + ")  AND OrganizeId = @orgId;", new[] { new SqlParameter("@orgId", orgId), new SqlParameter("@usercode", usercode) });
                //}
                if (delvo.mz_xmList != null && delvo.mz_xmList.Count > 0)
                {
                    var stringlist = "'" + string.Join("','", delvo.mz_xmList.Distinct().ToArray()) + "'";
                    db.ExecuteSqlCommand("UPDATE dbo.mz_xm SET zt=0,LastModifierCode=@usercode,LastModifyTime=GETDATE() WHERE xmnm IN (" + stringlist + ")  AND OrganizeId = @orgId;", new[] { new SqlParameter("@orgId", orgId), new SqlParameter("@usercode", usercode) });
                }

                #region
                if (jsnmList != null && jsnmList.Count() > 0)
                {
                    //Xóa và sửa giờ,làm mới mới Đ ơn thuốc bảng chính,quyết toán bảng chính của trạng thái
                    foreach (var jsnm in jsnmList)
                    {
                        ////Lấy xóa mã nội bộ đ ơn thuốc(này phiên bản điều kiện tiên quyết: một lần quyết toán nhất nhiều một tờ Đ ơn thuốc)
                        //var cfnm = db.FirstOrDefault<int>(@"SELECT
                        //                        cfmx.cfnm
                        //                 FROM   mz_js js
                        //                        LEFT JOIN dbo.mz_jsmx mx ON mx.jsnm = js.jsnm
                        //                                                    AND js.OrganizeId = mx.OrganizeId
                        //                        LEFT JOIN mz_cfmx cfmx ON cfmx.cfmxId = mx.cf_mxnm
                        //                                                  AND cfmx.OrganizeId = js.OrganizeId
                        //                 WHERE  js.jsnm = @jsnm
                        //                        AND mx.zt = 0
                        //                        AND cfmx.zt = 0
                        //                        AND js.OrganizeId = @orgId", new[] { new SqlParameter("@jsnm", jsnm), new SqlParameter("@orgId", orgId) });
                        //if (cfnm > 0)
                        //{
                        // // phán đoán Đ ơn thuốc có hay không tồn tại tại khác của chưa xóa
                        //    var cfnmExists = db.FirstOrDefault<int>(@"SELECT  COUNT(1)
                        //FROM    dbo.mz_cfmx
                        //WHERE   cfnm = @cfnm AND zt=1 AND OrganizeId=@orgId", new[] { new SqlParameter("@jsnm", jsnm), new SqlParameter("@orgId", orgId), new SqlParameter("@cfnm", cfnm) });
                        //    if (cfnmExists < 1)
                        //    {
                        //        db.ExecuteSqlCommand("UPDATE dbo.mz_cf SET zt=0,LastModifierCode=@usercode,LastModifyTime=GETDATE() WHERE cfnm=@cfnm  AND OrganizeId = @orgId;", new[] { new SqlParameter("@cfnm", cfnm), new SqlParameter("@orgId", orgId), new SqlParameter("@usercode", usercode) });
                        //    }
                        //}

                        // phán đoán quyết toán có hay không tồn tại tại khác của chưa xóa
                        var jsExists = db.FirstOrDefault<int>(@"SELECT COUNT(1) FROM dbo.mz_jsmx WHERE jsnm=@jsnm AND zt=1 AND OrganizeId=@orgId",
                            new[] { new SqlParameter("@jsnm", jsnm), new SqlParameter("@orgId", orgId) });
                        if (jsExists < 1)
                        {
                            db.ExecuteSqlCommand("UPDATE dbo.mz_js SET zt=0,LastModifierCode=@usercode,LastModifyTime=GETDATE() WHERE jsnm=@jsnm  AND OrganizeId = @orgId;",
                                new[] { new SqlParameter("@jsnm", jsnm), new SqlParameter("@orgId", orgId), new SqlParameter("@usercode", usercode) });
                        }
                    }
                }
                #endregion

                //Xóa kế hoạch ghi sổ bảng chính 

                //Lấy tất cả các Đ ãxóa kế hoạch ghi sổ khóa chính
                //var jzjhIdList = db.FindList<string>("SELECT DISTINCT jzjhId FROM dbo.mz_jzjhmx WHERE jzjhmxId IN (" + jzjhmxIdslist + ") AND zt=0", new[] { new SqlParameter("@orgId", orgId) });
                //foreach (var item in jzjhIdList)
                //{
                // // phán đoán kế hoạch ghi sổ có hay không tồn tại tại khác của chưa xóa
                //    var jzjhExists = db.FirstOrDefault<int>(@"SELECT COUNT(1) FROM dbo.mz_jzjhmx WHERE jzjhId=@jzjhId AND zt=1 AND OrganizeId=@orgId",
                //        new[] { new SqlParameter("@jzjhId", item), new SqlParameter("@orgId", orgId) });
                //    if (jzjhExists < 1)
                //    {
                //        db.ExecuteSqlCommand("UPDATE dbo.mz_jzjh SET zt=0,LastModifierCode=@usercode,LastModifyTime=GETDATE() WHERE jzjhId=@jzjhId  AND OrganizeId = @orgId;",
                //            new[] { new SqlParameter("@jzjhId", item), new SqlParameter("@orgId", orgId), new SqlParameter("@usercode", usercode) });
                //    }
                //}
                #endregion

                db.Commit();
            }

            using (var db2 = new EFDbTransaction(_databaseFactory).BeginTrans())
            {
                #region 修改处方主表，结算主表总金额
                if (jsnmList != null && jsnmList.Count() > 0)
                {
                    //SửaĐ ơn thuốc bảng chính và quyết toán bảng chính tổng số tiền
                    foreach (var jsnm in jsnmList)
                    {
                        ////Lấy xóa mã nội bộ đ ơn thuốc
                        //var cfnm = db2.FirstOrDefault<int>(@"SELECT DISTINCT
                        //                        cfmx.cfnm
                        //                 FROM   mz_js js
                        //                        LEFT JOIN dbo.mz_jsmx mx ON mx.jsnm = js.jsnm
                        //                                                    AND js.OrganizeId = mx.OrganizeId
                        //                        LEFT JOIN mz_cfmx cfmx ON cfmx.cfmxId = mx.cf_mxnm
                        //                                                  AND cfmx.OrganizeId = js.OrganizeId
                        //                 WHERE  js.jsnm = @jsnm
                        //                        AND mx.zt = 0
                        //                        AND cfmx.zt = 0
                        //                        AND js.OrganizeId = @orgId", new[] { new SqlParameter("@jsnm", jsnm), new SqlParameter("@orgId", orgId) });
                        //if (cfnm > 0)
                        //{
                        // // phán đoán Đ ơn thuốc có hay không tồn tại tại khác của chưa xóa
                        //    var cfnmExists = db2.FirstOrDefault<int>(@"SELECT  COUNT(1)
                        //FROM    dbo.mz_cfmx
                        //WHERE   cfnm = @cfnm AND zt=1 AND OrganizeId=@orgId", new[] { new SqlParameter("@jsnm", jsnm), new SqlParameter("@orgId", orgId), new SqlParameter("@cfnm", cfnm) });
                        //    if (cfnmExists > 0)
                        //    {
                        // //Cập nh ậtĐ ơn thuốc bảng chính tổng số tiền
                        //        db2.ExecuteSqlCommand(@"UPDATE  dbo.mz_cf
                        //            SET     zje = (SELECT  SUM(dj * sl),LastModifierCode=@usercode,LastModifyTime=GETDATE()
                        //                            FROM    dbo.mz_cfmx
                        //                            WHERE   cfnm = @cfnm and zt=1
                        //                          )
                        //            WHERE cfnm = @cfnm
                        //                    AND OrganizeId = @orgId ", new[] { new SqlParameter("@cfnm", cfnm), new SqlParameter("@orgId", orgId), new SqlParameter("@usercode", usercode) });
                        //    }
                        //}

                        // phán đoán quyết toán có hay không tồn tại tại khác của chưa xóa
                        var jsExists = db2.FirstOrDefault<int>(@"SELECT COUNT(1) FROM dbo.mz_jsmx WHERE jsnm=@jsnm AND zt=1 AND OrganizeId=@orgId",
                            new[] { new SqlParameter("@jsnm", jsnm), new SqlParameter("@orgId", orgId) });
                        if (jsExists > 0)
                        {
                            //Cập nhật quyết toán bảng chính tổng số tiền
                            var xmzj = FirstOrDefault<decimal>(@"SELECT sum(ISNULL(jyje, 0)) FROM mz_jsmx WHERE jsnm = @jsnm AND OrganizeId =@orgId AND zt = '1'", new[] { new SqlParameter("@jsnm", jsnm), new SqlParameter("@orgId", orgId) });
                            decimal cfzj = 0;
                            //cfzj = FirstOrDefault<decimal>(@"SELECT  ISNULL(SUM(cfmx.dj * cfmx.sl), 0)
                            //                    FROM    mz_cf cf
                            //                            LEFT JOIN dbo.mz_cfmx cfmx ON cfmx.cfnm = cf.cfnm
                            //                                                          AND cfmx.OrganizeId = cf.OrganizeId
                            //                                                          AND cf.OrganizeId =@orgId
                            //                                                          AND cf.zt = 1
                            //                                                          AND cfmx.zt = 1
                            //                                                          AND jsnm = @jsnm", new[] { new SqlParameter("@jsnm", jsnm), new SqlParameter("@orgId", orgId) });
                            db2.ExecuteSqlCommand(@"UPDATE  dbo.mz_js
                                              SET     zje = @zj,LastModifierCode=@usercode,LastModifyTime=GETDATE()
                                              WHERE   jsnm = @jsnm
                                                      AND OrganizeId = @orgId
                                                      AND zt = '1'", new[] { new SqlParameter("@jsnm", jsnm), new SqlParameter("@zj", xmzj + cfzj), new SqlParameter("@orgId", orgId), new SqlParameter("@usercode", usercode) });
                        }
                    }
                }

                #endregion
                db2.Commit();
            }

        }
        #endregion


        #region 门诊记账，第四个版本，增加单位治疗量逻辑
        /// <summary>
        /// theo số khám ngoại trú lấy lịch sử hạng mục thu phí, không bao gồm thuốc và đã hoàn thành,Đ ã dừng của kế hoạch ghi sổ
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<OptimAccInfoDto> GetpatientAccountInfoV4(string mzh, string orgId)
        {
            if (string.IsNullOrWhiteSpace(mzh))
            {
                throw new FailedException("门诊号不能为空");
            }

            if (string.IsNullOrWhiteSpace(orgId))
            {
                throw new FailedException("组织机构不能为空");
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT CONVERT(VARCHAR(50), NEWID()) newid,
                            '2' yzlx,
                            mz_xm.sfxm sfxmCode,
                            mz_xm.sfxm sfxm,
                            xt_sfxm.sfxmmc sfxmmc,
                            mz_xm.kflb,
                            mz_xm.dl sfdlCode,
                            mz_xm.ysmc,
                            mz_xm.ys,
                            mz_xm.ks,
                            mz_xm.ksmc,
                            sfdl.dlmc sfdlmc,
                            CAST(mz_xm.dj AS DECIMAL) AS dj,
                            --CAST(mz_jsmx.sl AS INT) sl,
                            ISNULL(mx.zcs,0) sl,
                            CAST(mz_xm.dj * mx.zcs AS VARCHAR) AS JE,
                            xmnm XMNM,
                            mz_xm.zfxz ZFXZ,
                            mz_xm.ghnm GHNM,
                            mx.zlsc duration,
                            xt_sfxm.dw dw,
                            mx.bz,
                            mx.jzsj,  
                            CAST(mz_xm.ttbz AS VARCHAR) AS ttbz,
                            gh.mzh,
                            mx.zll,
                            mx.zxzt,
		                    mx.jzjhmxId,
			                mx.StartDate,
			                mx.EndDate,
                            mx.yzxz,
                            xt_sfxm.dwjls dwjls,
                            xt_sfxm.jjcl
                     FROM   mz_gh gh
                            LEFT JOIN mz_xm ON mz_xm.ghnm = gh.ghnm
                                               AND mz_xm.OrganizeId = @orgId
                            LEFT JOIN dbo.mz_jzjhmx mx ON mx.jzjhmxId = mz_xm.jzjhmxId
                                                          AND mx.OrganizeId =@orgId
                            LEFT JOIN NewtouchHIS_Base..V_S_xt_sfxm xt_sfxm ON mx.sfxmCode = xt_sfxm.sfxmCode
                                                                               AND xt_sfxm.OrganizeId =@orgId
                            LEFT JOIN NewtouchHIS_Base..V_S_xt_sfdl sfdl ON mz_xm.dl = sfdl.dlcode
                                                                            AND sfdl.OrganizeId =@orgId
                     WHERE  mz_xm.OrganizeId =@orgId
                            AND gh.mzh = @mzh
                            AND mz_xm.zt = 1
                            AND mx.zt = 1
                            AND mz_xm.xmzt='1'
                            AND gh.zt = 1  
                           AND ( CASE WHEN mx.zxzt = 3
                                AND ISNULL(mx.yzxcs, 0) = 0 THEN 1
                           ELSE 0
                            END ) != 1
order by mx.jzsj desc, mx.CreateTime desc");
            DbParameter[] par =
            {
                new SqlParameter("@mzh", mzh),
                new SqlParameter("@orgId", orgId)
            };
            return FindList<OptimAccInfoDto>(strSql.ToString(), par);
        }
        #endregion
        /// <summary>
        /// Lưughi sổ,dữ liệu kho lưu
        /// </summary>
        /// <param name="vo">Thêm m ớiĐ ối tư ợng tập hợp </param>
        /// <param name="updatevo">SửaĐ ối tư ợng tập hợp </param>
        /// <param name="jsnmList">Sửa mã nội bộ quyết toán tập hợp </param>
        /// <param name="orgId"></param>
        public void OutPatsettDBInCharge(OutPatSettInAccDataVo4 vo, List<OutpatientItemExeEntity> tempVo, string orgId)
        {
            var usercode = OperatorProvider.GetCurrent().UserCode;
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                #region 新增
                if (vo.mz_xmList != null && vo.mz_xmList.Count > 0)
                {
                    foreach (var item in vo.mz_xmList)
                    {
                        db.Insert(item);
                    }
                }
                if (vo.mz_js != null && vo.mz_js.Count > 0)
                {
                    foreach (var item in vo.mz_js)
                    {
                        db.Insert(item);
                    }

                }
                if (vo.mz_jsmxList != null && vo.mz_jsmxList.Count > 0)
                {
                    foreach (var item in vo.mz_jsmxList)
                    {
                        db.Insert(item);
                    }
                }
                //Kế hoạch ghi sổ
                if (vo.mz_jzjh != null)
                {
                    db.Insert(vo.mz_jzjh);
                }

                //Chi tiết kế hoạch ghi sổ
                if (vo.mz_jzjhmx != null && vo.mz_jzjhmx.Count > 0)
                {
                    foreach (var item in vo.mz_jzjhmx)
                    {
                        db.Insert(item);
                    }
                }
                //Hạng mục ngoại trú thực hiện 
                if (tempVo != null && tempVo.Count() > 0)
                {
                    foreach (var item in tempVo)
                    {
                        db.Insert(item);
                    }
                }

                #endregion
                db.Commit();
            }
        }


        #region 收费2018版

        /// <summary>
        /// Lấy chưa thu phí của thông tin bệnh nhân
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public object GetPatientGridJson(string orgId)
        {
            var containsJZJS = _sysConfigRepo.GetBoolValueByCode("Outpatient_ChargeFee_Patient_ContainsJZJS", orgId, true);
            var lastviewday = _sysConfigRepo.GetIntValueByCode("Outpatient_ChargeFee_Patient_Lastviewday", orgId, 3);
            var strSql = new StringBuilder(@"
select tt.xm,tt.blh,tt.mzh,xx.py from
(
SELECT  distinct gh.xm,
        gh.blh,
        gh.mzh, gh.ghrq
FROM    mz_gh(NOLOCK) gh
        join mz_cf(NOLOCK) cf ON cf.ghnm = gh.ghnm
WHERE   gh.OrganizeId = @orgId AND ISNULL(gh.mzh, '') != '' AND cf.cfzt = '0' AND cf.zt = '1' and gh.zt = '1' and isnull(gh.ghzt,'0') != '2' and (1 = @isContainsJZJS or isnull(gh.jzbz, '') <> '3')

union
--20181010添加代收费项目
SELECT  distinct gh.xm,
        gh.blh,
        gh.mzh, gh.ghrq
FROM    mz_gh(NOLOCK) gh
        join mz_xm(NOLOCK) xm ON xm.ghnm = gh.ghnm
WHERE   gh.OrganizeId = @orgId AND ISNULL(gh.mzh, '') != '' AND xm.xmzt = '0' AND xm.zt = '1' and gh.zt = '1' and isnull(gh.ghzt,'0') != '2' and (1 = @isContainsJZJS or isnull(gh.jzbz, '') <> '3')
) as tt  LEFT JOIN dbo.xt_brjbxx(NOLOCK) xx ON xx.blh = tt.blh
                                      AND xx.OrganizeId = @orgId and   xx.zt = '1'
where tt.ghrq > DATEADD(DAY,-@lastviewday,GETDATE())
order by tt.ghrq desc");
            DbParameter[] par =
            {
                new SqlParameter("@orgId", orgId),
                new SqlParameter("@isContainsJZJS", containsJZJS.Value),
                new SqlParameter("@lastviewday", lastviewday)
            };
            var list = this.FindList<OutPatAccountingDto>(strSql.ToString(), par);

            var strSql2 = new StringBuilder(@"
select xm, blh, mzh, patid,py  from
(
select DISTINCT
	   gh.xm,
       gh.blh,
       gh.mzh, gh.ghrq,
       gh.patid,
       xx.py
from mz_gh(NOLOCK) gh
INNER JOIN mz_js(nolock) js ON js.ghnm = gh.ghnm
                    LEFT JOIN dbo.xt_brjbxx(NOLOCK) xx ON xx.blh = gh.blh
                                                  AND xx.OrganizeId = @orgId
                                                    and xx.zt='1'
where js.isQfyj = 1 and isnull(js.tbz, 0) <> 1 and js.zt = '1' and gh.OrganizeId = @orgId and gh.zt = '1' and isnull(gh.ghzt,'0') != '2' and (1 = @isContainsJZJS or isnull(gh.jzbz, '') <> '3')
) as ttt
where ttt.ghrq > DATEADD(DAY,-@lastviewday,GETDATE())
order by ttt.ghrq desc");
            DbParameter[] par2 =
            {
                new SqlParameter("@orgId", orgId),
                new SqlParameter("@isContainsJZJS", containsJZJS.Value),
                new SqlParameter("@lastviewday", lastviewday)
            };
            var list2 = FindList<OutPatAccountingDto>(strSql2.ToString(), par2);

            var obj = list2.Select(p => new { xm = p.xm, blh = p.blh, mzh = p.mzh, iqQfyj = true, patid = p.patid, py = p.py })
                .Union(list.Select(p => new { xm = p.xm, blh = p.blh, mzh = p.mzh, iqQfyj = false, patid = (int?)null, py = p.py }));
            ;
            return obj;
        }

        /// <summary>
        /// Lấy quyết toán trư ớc khi nợ phí của quyết toán bản ghi 
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<OutpatientSettArrearageVO> GetOutpatientSettArrearageVOList(string mzh, string orgId)
        {
            var strSql2 = new StringBuilder(@"
select js.jsnm, js.zje, js.CreateTime, js.CreatorCode
from mz_js(nolock) js
left join mz_gh(nolock) gh
on gh.ghnm = js.ghnm
where js.isQfyj = 1 and isnull(js.tbz, 0) <> 1 and js.zt = '1'
and js.OrganizeId = @orgId
and gh.mzh = @mzh");
            DbParameter[] par2 =
            {
                new SqlParameter("@orgId", orgId)
                ,new SqlParameter("@mzh", mzh)
            };
            var list2 = FindList<OutpatientSettArrearageVO>(strSql2.ToString(), par2);

            return list2;
        }

        /// <summary>
        /// theo jsnm lấy quyết toán củachi tiếtdanh sách
        /// </summary>
        /// <param name="jsnm"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<OutpatientSettDetailVO> GetOutpatientSettDetailList(int jsnm, string orgId)
        {
            var strSql2 = new StringBuilder(@"
select jsmxnm, sfxm.sfxmmc mc, jsmx.sl, jyje je from mz_jsmx jsmx
inner join mz_xm mzxm
on mzxm.xmnm = jsmx.mxnm
left join [NewtouchHIS_Base]..V_S_xt_sfxm sfxm
on sfxm.sfxmCode = mzxm.sfxm
where jsmx.jsnm = @jsnm and mzxm.OrganizeId = @orgId
and sfxm.OrganizeId = @orgId
and jsmx.zt = '1' and mzxm.zt = '1' 
union all
select jsmxnm, yp.ypmc mc, jsmx.sl, jyje je from mz_jsmx jsmx
inner join mz_cfmx ypmx
on ypmx.cfmxId = jsmx.cf_mxnm
left join [NewtouchHIS_Base]..V_S_xt_yp yp
on yp.ypCode = ypmx.yp
where jsmx.jsnm = @jsnm and ypmx.OrganizeId = @orgId
and yp.OrganizeId = @orgId
and jsmx.zt = '1' and ypmx.zt = '1'");
            DbParameter[] par2 =
            {
                new SqlParameter("@orgId", orgId)
                ,new SqlParameter("@jsnm", jsnm)
            };
            var list2 = FindList<OutpatientSettDetailVO>(strSql2.ToString(), par2);

            return list2;
        }

        /// <summary>
        /// theo số khám ngoại trú lấy chưa thu phí CủaĐ ơn thuốc
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<ChargeLeftDto> GetPrescriptionBymzh(string mzh, string orgId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT  
                            CAST(cf.cfnm AS VARCHAR(20)) cfnm,
                            cf.cfh ,
                            cf.zje ,
                            cf.ksmc ,
                            cf.ysmc,
                            CASE when isnull(cf.cflxmc, '') <> '' then cf.cflxmc 
else (case cf.cflx WHEN '1' THEN '药品' WHEN '2' THEN '项目' END) end cflx
                    FROM    mz_cf cf
                            --LEFT JOIN dbo.mz_cfmx mx ON cf.cfnm = mx.cfnm
                                                        --AND mx.OrganizeId = @orgId
                            RIGHT JOIN mz_gh gh ON cf.ghnm = gh.ghnm
                                                   AND gh.OrganizeId = @orgId
                    WHERE   gh.mzh = @mzh
                            AND gh.zt = '1'
                            AND ISNULL(cf.cfzt, 0) = '0'
                            AND cf.OrganizeId = @orgId
                            AND cf.zt='1' 
                           -- AND mx.zt='1'");
            DbParameter[] par =
            {
                new SqlParameter("@mzh", mzh),
                new SqlParameter("@orgId", orgId)
            };
            return FindList<ChargeLeftDto>(strSql.ToString(), par);
        }


        public IList<ChargeRightDto> GetPrescriptionDetailBycfnm(IList<int> cfnmList, string orgId)
        {
            if (cfnmList != null && cfnmList.Count() > 0)
            {
                var cfnm = "'" + string.Join("','", cfnmList) + "'";
                var strSql = string.Format(@"SELECT  cf.cfh ,cf.cfnm ,mx.dl sfdlCode,dl.dlmc sfdlmc,
yp.ypmc sfxmmc ,mx.yp sfxmCode,
ISNULL(mx.dj, 0.00) dj , ISNULL(CAST(mx.sl AS INT), 0) sl , ISNULL(mx.je, 0.00) zje ,
mx.dw, mx.zfbl, mx.zfxz,
null dczll, null zxcs,
'1' yzlx
FROM mz_cf cf 
LEFT JOIN  mz_cfmx mx
ON cf.cfnm = mx.cfnm
LEFT JOIN NewtouchHIS_Base..V_C_xt_yp yp ON yp.ypCode=mx.yp AND yp.OrganizeId=@orgId
LEFT JOIN NewtouchHIS_Base..xt_sfdl dl ON dl.dlCode=mx.dl AND dl.OrganizeId=@orgId
WHERE mx.OrganizeId = @orgId
--有效
AND mx.zt = '1'
--有效
and (cf.zt= '1')
and cf.cfnm in ({0})

UNION ALL
        
SELECT cf.cfh ,cf.cfnm ,xm.dl sfdlCode ,dl.dlmc sfdlmc ,
sfxm.sfxmmc sfxmmc ,
sfxm.sfxmCode sfxmCode ,
ISNULL(xm.dj, 0.00) dj , ISNULL(CAST(xm.sl AS INT), 0) sl , ISNULL(xm.je, 0.00) zje ,
xm.dw, xm.zfbl, xm.zfxz,
xm.dczll,xm.zxcs,
'2' yzlx
FROM dbo.mz_xm xm
LEFT JOIN dbo.mz_cf cf 
ON cf.cfnm = xm.cfnm AND cf.OrganizeId = @orgId
LEFT JOIN NewtouchHIS_Base..V_S_xt_sfxm sfxm 
ON sfxm.sfxmCode = xm.sfxm AND sfxm.OrganizeId = @orgId
LEFT JOIN NewtouchHIS_Base..xt_sfdl dl
ON dl.dlCode = xm.dl AND dl.OrganizeId = @orgId
WHERE xm.OrganizeId = @orgId
--有效
AND xm.zt = '1'
--未关联处方 或 处方有效
and (cf.zt is null or (cf.zt= '1'))
and xm.cfnm in ({0})", cfnm);
                DbParameter[] par = { new SqlParameter("@orgId", orgId) };
                return FindList<ChargeRightDto>(strSql, par);
            }
            return null;
        }

        /// <summary>
        /// Lấy chờ thu phí CủaĐ ơn thuốcdanh sách
        /// </summary>
        /// <param name="mzh">Số khám ngoại trú</param>
        /// <param name="orgId">Cơ quanID</param>
        /// <returns></returns>
        public IList<ChargeLeftDto> GetNewUnSettedPrescriptionByMzh(string mzh, string orgId)
        {
            const string strSql = @"
	SELECT CAST(cf.cfnm AS VARCHAR(20)) AS cfnm, cf.cfh, CAST(cf.cflx AS VARCHAR(20)) AS cflx, cf.cflxmc, cf.zje, cf.ksmc, cf.ysmc, CONVERT(VARCHAR(20),cf.createtime,120) AS klsj
	FROM mz_cf(NOLOCK) cf
	INNER JOIN mz_gh(NOLOCK) gh ON cf.ghnm=gh.ghnm AND gh.OrganizeId=cf.OrganizeId
	WHERE cf.OrganizeId = @orgId
	AND cf.zt= '1' AND cf.cfzt = '0' --处方有效且未收费
	AND gh.mzh=@mzh AND gh.zt='1' --有效挂号
    ORDER BY cf.CreateTime
";
            var par = new DbParameter[] {
                new SqlParameter("@orgId", orgId),
                new SqlParameter("@mzh", mzh)
            };
            return FindList<ChargeLeftDto>(strSql, par);
        }

        /// <summary>
        /// Lấy chờ thu phí củachi tiết đ ơn thuốcdanh sách
        /// </summary>
        /// <param name="mzh">Số khám ngoại trú</param>
        /// <param name="cfnms">dấu phẩy phân táchcủa mã nội bộ đ ơn thuốc</param>
        /// <param name="orgId">Cơ quanID</param>
        /// <returns></returns>
        public IList<ChargeRightDto> GetNewAllUnSettedListByMzh(string mzh, string cfnms, string orgId)
        {
            string flagstr = _sysConfigRepo.GetValueByCode("sfxmztbs", orgId);
            string strSql = "";
            if (flagstr == "true")
            {
                strSql = @"select * from( 
                select cfh,cfnm,sfxmmc,sfxmCode,sum(dj) dj,sum(sl) sl,sum(zje) zje,dw,yzlx,ks,ys,ysmc,ztId,ztmc,cflx,yybz from( 
                SELECT  cf.cfh ,cf.cfnm ,yp.ypmc sfxmmc ,mx.yp sfxmCode, 
	                sum(ISNULL(mx.dj, 0.00)) dj , sum(ISNULL(CAST(mx.sl AS INT), 0)) sl , sum(ISNULL(mx.je, 0.00)) zje , 
	                mx.dw, '1' yzlx, 
	                cf.ks, cf.ys, cf.ysmc,null ztId,null ztmc,cf.cflx,
                    ybyp.NAT_HI_DRUGLIST_MEMO AS yybz
	                FROM mz_cf(NOLOCK) cf  
	                INNER JOIN mz_cfmx(NOLOCK) mx ON cf.cfnm = mx.cfnm AND mx.OrganizeId=cf.OrganizeId AND mx.zt = '1' 
	                INNER JOIN dbo.mz_gh(NOLOCK) gh ON gh.ghnm=cf.ghnm AND gh.zt='1' AND gh.OrganizeId=cf.OrganizeId 
	                LEFT JOIN NewtouchHIS_Base.dbo.V_C_xt_yp yp ON yp.ypCode=mx.yp AND yp.OrganizeId=cf.OrganizeId AND yp.zt='1' 
	                 INNER JOIN  NewtouchHIS_Base.dbo.xt_ypsx ypsx ON yp.ypId=ypsx.ypId and yp.organizeid=ypsx.organizeid
	                LEFT JOIN   NewtouchHIS_Base.dbo.[G_yb_wm_tcmpat_info_b] ybyp on ypsx.gjybdm =  ybyp.MED_LIST_CODG
	                LEFT JOIN NewtouchHIS_Base.dbo.xt_sfdl(NOLOCK) dl ON dl.dlCode=mx.dl AND dl.OrganizeId=cf.OrganizeId AND dl.zt='1' 
	                WHERE cf.OrganizeId = @orgId		 
	                AND cf.zt= '1' AND cf.cfzt = '0' --处方有效且未收费 
	                AND gh.mzh=@mzh 
                    AND cf.cfnm IN (SELECT * FROM dbo.f_split(@cfnms, ',')) --选择的处方内码 
	                group by cf.cfh,cf.cfnm,yp.ypmc,mx.yp,mx.dw,cf.ks,cf.ys,cf.ysmc,cf.cflx,ybyp.NAT_HI_DRUGLIST_MEMO
                ) b 
                group by cfh ,cfnm,ztmc,ztId,ks,ys, ysmc,sfxmmc,sfxmCode,dw,yzlx,cflx ,yybz
                 union all 
                select cfh,cfnm,sfxmmc,sfxmCode,sum(case when LEN(ztId)>0 then CONVERT(decimal(18,2),dj*sl) else dj end ) dj,case when LEN(ztId)>0 then 1 else  sum(sl) end sl,sum(zje) zje,dw,yzlx,ks,ys,ysmc,ztId,ztmc,cflx,'' yybz from( 
                SELECT cf.cfh ,cf.cfnm , (case when xm.ztmc is not null then xm.ztmc else sfxm.sfxmmc end) sfxmmc , (case when xm.ztId is not null then xm.ztId else sfxm.sfxmCode end) sfxmCode , 
	                convert(decimal(18,4),sum(ISNULL(xm.dj, 0.00))) dj , sum(ISNULL(CAST(xm.sl AS INT), 0)) sl , sum(ISNULL(xm.je, 0.00)) zje ,(case when xm.ztId is not null then '组套' else xm.dw end) dw, 
	                '2' yzlx, 
	                xm.ks, xm.ys, xm.ysmc,xm.ztId,xm.ztmc,cf.cflx, 
                    '' yybz
	                FROM dbo.mz_xm(NOLOCK) xm 
	                INNER JOIN dbo.mz_gh(NOLOCK) gh ON gh.ghnm=xm.ghnm and gh.OrganizeId=xm.OrganizeId AND gh.zt='1' 
	                LEFT JOIN dbo.mz_cf(NOLOCK) cf ON cf.cfnm = xm.cfnm AND cf.OrganizeId = xm.OrganizeId 
	                LEFT JOIN NewtouchHIS_Base.dbo.V_S_xt_sfxm sfxm ON sfxm.sfxmCode = xm.sfxm AND sfxm.OrganizeId = xm.OrganizeId 
	                LEFT JOIN NewtouchHIS_Base.dbo.xt_sfdl(NOLOCK) dl ON dl.dlCode = xm.dl AND dl.OrganizeId = xm.OrganizeId AND dl.zt='1' 
	                WHERE xm.OrganizeId = @orgId	 
	                AND xm.zt = '1' and xm.xmzt = '0' --有效且未收费	 
	                and (cf.zt is null or (cf.zt= '1' and cf.cfzt = '0')) --未关联处方 或 处方有效且未收费 
	                AND gh.mzh=@mzh 
                    AND cf.cfnm IN  (SELECT * FROM dbo.f_split(@cfnms, ',')) --选择的处方内码 
	                group by cf.cfh ,cf.cfnm,xm.ztmc,xm.ztId,xm.ks, xm.ys, xm.ysmc,sfxm.sfxmmc,sfxm.sfxmCode,xm.dw,cf.cflx
	                ) a                                                                                                                                       
	                group by cfh ,cfnm,ztmc,ztId,ks,ys, ysmc,sfxmmc,sfxmCode,dw,yzlx,cflx,yybz
                ) z 
                ORDER by isnull(cfh, 'ZZZZ') ";
            }
            else {
                strSql = @"
                SELECT * 
                FROM (
                	SELECT  cf.cfh ,cf.cfnm ,mx.dl sfdlCode,dl.dlmc sfdlmc,	yp.ypmc sfxmmc ,mx.yp sfxmCode,
                	ISNULL(mx.dj, 0.00) dj , ISNULL(CAST(mx.sl AS INT), 0) sl , ISNULL(mx.je, 0.00) zje ,
                	mx.dw, mx.zfbl, mx.zfxz,null dczll, null zxcs,null xmnm, mx.cfmxId cfmxId, mx.CreateTime klsj,'1' yzlx,
                	cf.ks, cf.ys, cf.ysmc, yp.ybdm ybdm,yp.ybbz, mx.cfmxId mxId, yp.ypgg gg, ISNULL(yp.gjybdm,'') xnhybdm,
                	ybyp.NAT_HI_DRUGLIST_MEMO AS yybz
                	FROM mz_cf(NOLOCK) cf 
                	INNER JOIN mz_cfmx(NOLOCK) mx ON cf.cfnm = mx.cfnm AND mx.OrganizeId=cf.OrganizeId AND mx.zt = '1'
                	INNER JOIN dbo.mz_gh(NOLOCK) gh ON gh.ghnm=cf.ghnm AND gh.zt='1' AND gh.OrganizeId=cf.OrganizeId
                	LEFT JOIN NewtouchHIS_Base.dbo.V_C_xt_yp yp ON yp.ypCode=mx.yp AND yp.OrganizeId=cf.OrganizeId AND yp.zt='1'
                	 INNER JOIN  NewtouchHIS_Base.dbo.xt_ypsx ypsx ON yp.ypId=ypsx.ypId and yp.organizeid=ypsx.organizeid
	                LEFT JOIN   NewtouchHIS_Base.dbo.[G_yb_wm_tcmpat_info_b] ybyp on ypsx.gjybdm =  ybyp.MED_LIST_CODG
                	LEFT JOIN NewtouchHIS_Base.dbo.xt_sfdl(NOLOCK) dl ON dl.dlCode=mx.dl AND dl.OrganizeId=cf.OrganizeId AND dl.zt='1'
                	WHERE cf.OrganizeId = @orgId		
                	AND cf.zt= '1' AND cf.cfzt = '0' --处方有效且未收费
                	AND gh.mzh=@mzh
                    AND cf.cfnm IN (SELECT * FROM dbo.f_split(@cfnms, ',')) --选择的处方内码

                	UNION ALL

                	SELECT cf.cfh ,cf.cfnm ,xm.dl sfdlCode ,dl.dlmc sfdlmc ,sfxm.sfxmmc sfxmmc , sfxm.sfxmCode sfxmCode ,
                	ISNULL(xm.dj, 0.00) dj , ISNULL(CAST(xm.sl AS INT), 0) sl , ISNULL(xm.je, 0.00) zje ,xm.dw, xm.zfbl, xm.zfxz,
                	xm.dczll,xm.zxcs,xm.xmnm xmnm, null cfmxId, xm.CreateTime klsj,	'2' yzlx,
                	xm.ks, xm.ys, xm.ysmc, sfxm.ybdm ybdm,sfxm.ybbz, xm.xmnm mxId, ISNULL(sfxm.gg,'') gg, ISNULL(sfxm.gjybdm,'') xnhybdm,
                	 '' yybz   
                	FROM dbo.mz_xm(NOLOCK) xm
                	INNER JOIN dbo.mz_gh(NOLOCK) gh ON gh.ghnm=xm.ghnm and gh.OrganizeId=xm.OrganizeId AND gh.zt='1'
                	LEFT JOIN dbo.mz_cf(NOLOCK) cf ON cf.cfnm = xm.cfnm AND cf.OrganizeId = xm.OrganizeId
                	LEFT JOIN NewtouchHIS_Base.dbo.V_S_xt_sfxm sfxm ON sfxm.sfxmCode = xm.sfxm AND sfxm.OrganizeId = xm.OrganizeId
                	LEFT JOIN NewtouchHIS_Base.dbo.xt_sfdl(NOLOCK) dl ON dl.dlCode = xm.dl AND dl.OrganizeId = xm.OrganizeId AND dl.zt='1'
                	WHERE xm.OrganizeId = @orgId	
                	AND xm.zt = '1' and xm.xmzt = '0' --有效且未收费	
                	and (cf.zt is null or (cf.zt= '1' and cf.cfzt = '0')) --未关联处方 或 处方有效且未收费
                	AND gh.mzh=@mzh
                    AND cf.cfnm IN  (SELECT * FROM dbo.f_split(@cfnms, ',')) --选择的处方内码
                ) as alldata 
                ORDER by isnull(cfh, 'ZZZZ'),klsj  ---ZZZ排在最后
                ";
            }
             
            var par = new DbParameter[] {
                new SqlParameter("@orgId", orgId),
                new SqlParameter("@mzh", mzh),
                new SqlParameter("@cfnms", cfnms)
            };
            return FindList<ChargeRightDto>(strSql, par);
        }

        /// <summary>
        /// LấyChờ quyết toán
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<ChargeRightDto> GetAllUnSettedListListByMzh(string mzh, string orgId)
        {
            const string strSql = @"
SELECT * 
FROM (
	SELECT  cf.cfh ,cf.cfnm ,mx.dl sfdlCode,dl.dlmc sfdlmc,	yp.ypmc sfxmmc ,mx.yp sfxmCode,
	ISNULL(mx.dj, 0.00) dj , ISNULL(CAST(mx.sl AS INT), 0) sl , ISNULL(mx.je, 0.00) zje ,
	mx.dw, mx.zfbl, mx.zfxz,null dczll, null zxcs,null xmnm, mx.cfmxId cfmxId, mx.CreateTime klsj,'1' yzlx,
	cf.ks, cf.ys, cf.ysmc, yp.ybdm ybdm,yp.ybbz, mx.cfmxId mxId, yp.ypgg gg, ISNULL(yp.xnhybdm,'') xnhybdm
	FROM mz_cf(NOLOCK) cf 
	INNER JOIN mz_cfmx(NOLOCK) mx ON cf.cfnm = mx.cfnm AND mx.OrganizeId=cf.OrganizeId AND mx.zt = '1'
	INNER JOIN dbo.mz_gh(NOLOCK) gh ON gh.ghnm=cf.ghnm AND gh.zt='1' AND gh.OrganizeId=cf.OrganizeId
	LEFT JOIN NewtouchHIS_Base.dbo.V_C_xt_yp yp ON yp.ypCode=mx.yp AND yp.OrganizeId=cf.OrganizeId AND yp.zt='1'
	LEFT JOIN NewtouchHIS_Base.dbo.xt_sfdl(NOLOCK) dl ON dl.dlCode=mx.dl AND dl.OrganizeId=cf.OrganizeId AND dl.zt='1'
	WHERE cf.OrganizeId = @orgId		
	AND cf.zt= '1' AND cf.cfzt = '0' --处方有效且未收费
	AND gh.mzh=@mzh

	UNION ALL

	SELECT cf.cfh ,cf.cfnm ,xm.dl sfdlCode ,dl.dlmc sfdlmc ,sfxm.sfxmmc sfxmmc , sfxm.sfxmCode sfxmCode ,
	ISNULL(xm.dj, 0.00) dj , ISNULL(CAST(xm.sl AS INT), 0) sl , ISNULL(xm.je, 0.00) zje ,xm.dw, xm.zfbl, xm.zfxz,
	xm.dczll,xm.zxcs,xm.xmnm xmnm, null cfmxId, xm.CreateTime klsj,	'2' yzlx,
	xm.ks, xm.ys, xm.ysmc, sfxm.ybdm ybdm,sfxm.ybbz, xm.xmnm mxId, ISNULL(sfxm.gg,'') gg, ISNULL(sfxm.xnhybdm,'') xnhybdm
	FROM dbo.mz_xm(NOLOCK) xm
	INNER JOIN dbo.mz_gh(NOLOCK) gh ON gh.ghnm=xm.ghnm and gh.OrganizeId=xm.OrganizeId AND gh.zt='1'
	LEFT JOIN dbo.mz_cf(NOLOCK) cf ON cf.cfnm = xm.cfnm AND cf.OrganizeId = xm.OrganizeId
	LEFT JOIN NewtouchHIS_Base.dbo.V_S_xt_sfxm sfxm ON sfxm.sfxmCode = xm.sfxm AND sfxm.OrganizeId = xm.OrganizeId
	LEFT JOIN NewtouchHIS_Base.dbo.xt_sfdl(NOLOCK) dl ON dl.dlCode = xm.dl AND dl.OrganizeId = xm.OrganizeId AND dl.zt='1'
	WHERE xm.OrganizeId = @orgId	
	AND xm.zt = '1' and xm.xmzt = '0' --有效且未收费	
	and (cf.zt is null or (cf.zt= '1' and cf.cfzt = '0')) --未关联处方 或 处方有效且未收费
	AND gh.mzh=@mzh
) as alldata 
ORDER by isnull(cfh, 'ZZZZ'),klsj  ---ZZZ排在最后
";
            var par = new DbParameter[] {
                new SqlParameter("@orgId", orgId),
                new SqlParameter("@mzh", mzh)
            };
            return FindList<ChargeRightDto>(strSql, par);
        }

        /// <summary>
        /// theo mã nội bộ quyết toán lấy chi tiết quyết toán
        /// </summary>
        /// <param name="jsnm"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<GuiAnChargeRightDto> GetAllUnSettedListListByJsnm(string jsnm, string orgId)
        {
            const string strSql = @"
SELECT * 
FROM
(
	SELECT a.jsmxnm, mx.cfmxId,yp.ypmc sfxmmc ,mx.yp sfxmCode,NULL xmnm, sfdl.dlCode sfdlCode, sfdl.dlmc sfdlmc,
	ISNULL(mx.dj, 0.00) dj , ISNULL(CAST(a.sl AS INT), 0) sl , ISNULL(a.jyje, 0.00) zje ,
	mx.dw, mx.zfbl, mx.zfxz,null dczll, null zxcs,
	mx.CreateTime klsj,
	'1' yzlx,yp.ybdm ybdm,yp.ybbz, yp.ypgg gg, ISNULL(yp.xnhybdm,'') xnhybdm
	FROM [NewtouchHIS_Sett].[dbo].[mz_jsmx](NOLOCK) a
	LEFT JOIN [NewtouchHIS_Sett].[dbo].[mz_cfmx](NOLOCK) mx ON a.cf_mxnm=mx.cfmxId AND mx.OrganizeId=@orgId AND mx.zt='1'
	LEFT JOIN NewtouchHIS_Base..V_C_xt_yp yp ON yp.ypCode=mx.yp AND yp.OrganizeId=@orgId AND yp.zt='1'
	LEFT JOIN NewtouchHIS_Base.dbo.V_S_xt_sfdl sfdl ON sfdl.dlCode=yp.dlCode AND sfdl.zt='1' AND sfdl.OrganizeId=a.OrganizeId
	WHERE a.jsnm=@jsnm AND ISNULL(a.cf_mxnm,0) !=0 and a.OrganizeId = @orgId AND a.zt='1'

	UNION ALL

	SELECT a.jsmxnm, NULL cfmxId,sfxm.sfxmmc sfxmmc ,sfxm.sfxmCode sfxmCode,mx.xmnm xmnm,sfdl.dlCode sfdlCode, sfdl.dlmc sfdlmc,
	ISNULL(mx.dj, 0.00) dj , ISNULL(CAST(a.sl AS INT), 0) sl , ISNULL(a.jyje, 0.00) zje ,
	mx.dw, mx.zfbl, mx.zfxz,null dczll, null zxcs,
	mx.CreateTime klsj,
	'2' yzlx,sfxm.ybdm ybdm,sfxm.ybbz, ISNULL(sfxm.gg,'') gg, ISNULL(sfxm.xnhybdm,'') xnhybdm
	FROM [NewtouchHIS_Sett].[dbo].[mz_jsmx](NOLOCK) a
	LEFT JOIN [NewtouchHIS_Sett].[dbo].[mz_xm](NOLOCK) mx ON a.mxnm=mx.xmnm AND mx.OrganizeId=@orgId AND mx.zt='1'
	LEFT JOIN NewtouchHIS_Base..V_S_xt_sfxm sfxm ON sfxm.sfxmCode = mx.sfxm AND sfxm.OrganizeId = @orgId AND sfxm.zt='1'
	LEFT JOIN NewtouchHIS_Base.dbo.V_S_xt_sfdl sfdl ON sfdl.dlCode=sfxm.sfdlCode AND sfdl.zt='1' AND sfdl.OrganizeId=a.OrganizeId
	WHERE a.jsnm=@jsnm AND ISNULL(a.mxnm,0) !=0 and a.OrganizeId = @orgId AND a.zt='1'
) nn
";
            DbParameter[] par = {
                new SqlParameter("@orgId", orgId),
                new SqlParameter("@jsnm", jsnm) };
            return FindList<GuiAnChargeRightDto>(strSql, par);
        }
        public GuiAnMzjsPatInfoDto GetGuiAnMzjsPatInfoDto(string mzh, string orgId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
                            SELECT  e.prm_akc190 prm_akc190 ,
                                    '' prm_aac001 ,--读卡获取
                                    ISNULL(b.zdCode,'') prm_ykc173 ,
                                    '' prm_hisfyze ,--前台获取
                                    '11' prm_aka130 ,--暂时写死，11普通门诊
                                    '' prm_yka110 ,
                                    '' prm_aae013 ,
                                    '' prm_aae011 ,
                                    '' prm_ykc141 ,
                                    '' prm_ykb065  --读卡获取
                            FROM    [Newtouch_CIS].[dbo].[xt_jz] a
                                    LEFT JOIN [Newtouch_CIS].[dbo].[xt_xyzd] b ON a.jzId = b.jzId  AND b.zdlx=1
									LEFT JOIN NewtouchHIS_Sett..mz_gh c ON a.mzh=c.mzh AND a.OrganizeId=c.OrganizeId
									LEFT JOIN NewtouchHIS_Sett..mz_js d ON c.ghnm=d.ghnm AND c.OrganizeId=d.OrganizeId
									LEFT JOIN NewtouchHIS_Sett..mz_js_gaybjyfy e ON d.jsnm=e.jsnm AND d.OrganizeId=e.OrganizeId
                            WHERE   a.mzh = @mzh and a.OrganizeId=@OrganizeId
                         ");
            SqlParameter[] par =
            {
                new SqlParameter("@mzh", mzh),
                new SqlParameter("@OrganizeId", orgId)
            };
            return this.FindList<GuiAnMzjsPatInfoDto>(strSql.ToString(), par).FirstOrDefault();

        }

        /// <summary>
        /// Lấy chờ quyết toán hạng mục(không phải Đ ơn thuốc)
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<ChargeRightDto> GetAllUnSettedSingleItemListByMzh(string mzh, string orgId)
        {
            var strSql = string.Format(@"SELECT null cfh ,null cfnm ,xm.dl sfdlCode ,dl.dlmc sfdlmc ,
sfxm.sfxmmc sfxmmc ,
sfxm.sfxmCode sfxmCode ,
ISNULL(xm.dj, 0.00) dj , ISNULL(CAST(xm.sl AS INT), 0) sl , ISNULL(xm.je, 0.00) zje ,
xm.dw, xm.zfbl, xm.zfxz,
xm.dczll,xm.zxcs,
xm.xmnm xmnm, null cfmxId, xm.CreateTime klsj,
'2' yzlx,
xm.ks, xm.ys, xm.ysmc, sfxm.ybdm ybdm
FROM dbo.mz_xm xm
LEFT JOIN NewtouchHIS_Base..V_S_xt_sfxm sfxm 
ON sfxm.sfxmCode = xm.sfxm AND sfxm.OrganizeId = @orgId
LEFT JOIN NewtouchHIS_Base..xt_sfdl dl
ON dl.dlCode = xm.dl AND dl.OrganizeId = @orgId
WHERE xm.OrganizeId = @orgId
--有效且未收费
AND xm.zt = '1' and xm.xmzt = '0'
--未关联处方 或 处方有效且未收费
and isnull(xm.cfnm,0)=0
and xm.ghnm in (select ghnm from mz_gh where mzh = @mzh and OrganizeId = @orgId)
order by xm.CreateTime desc
");
            DbParameter[] par = { new SqlParameter("@orgId", orgId), new SqlParameter("@mzh", mzh) };
            return FindList<ChargeRightDto>(strSql, par);
        }

        /// <summary>
        /// Lấy chỉ đ ịnh bệnh nhân BHYT nông thôn mới chưa quyết toán của tất cả các outpid
        /// </summary>
        /// <param name="patid"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public List<string> SelectNotSettedXnhOutpId(int patid, string organizeId)
        {
            const string sql = @"
SELECT ygh.outpId 
FROM dbo.xt_brjbxx(NOLOCK) jbxx 
LEFT JOIN dbo.mz_gh(NOLOCK) ygh ON ygh.patid=jbxx.patid AND ygh.zt='1' AND ygh.OrganizeId=jbxx.OrganizeId 
LEFT JOIN dbo.mz_xnh_settResult(NOLOCK) xsr ON xsr.outpId=ygh.outpId AND xsr.OrganizeId=jbxx.OrganizeId 
WHERE jbxx.patid=@patid
AND jbxx.OrganizeId=@OrganizeId
AND (ygh.outpId<>'' AND ygh.outpId IS NOT NULL)
AND (xsr.Id IS NULL OR xsr.zt='0')
";
            var param = new DbParameter[]
            {
                new SqlParameter("@patid", patid),
                new SqlParameter("@OrganizeId", organizeId),
            };
            return FindList<string>(sql, param);
        }
        #endregion

        #region 结算查询

        /// <summary>
        /// Quyết toánTra cứu
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <returns></returns>
        public IList<OutpatientSettlementVO> GetSettlementList(Pagination pagination, string orgId, string keyword, DateTime? kssj, DateTime? jssj, bool containsTF)
        {
            var sb = new StringBuilder();
            sb.Append(@"select gh.mzh,gh.blh,js.jsnm,gh.xm,gh.xb,brxz.brxzmc brxzmc,gh.zdmc,js.jszt,js.CreateTime jsrq
,js.fph,js.zje jszje, js.xjzf jsxjzf
, ybfy.ZFY, ybfy.XJZF
--一般费用（居保：医保费用）
, ybfy.YBFY
, ybfy.TSFY, ybfy.JBZF, ybfy.GBZF, ybfy.SUMZFDYBFY, ybfy.ZFDYBFY
, ybfy.JBYE, ybfy.GBYE, ybfy.KBXYBFY, ybfy.KBXTSFY
--居保
, ybfy.TCZF, ybfy.JZZF, ybfy.DKC023
from mz_js js
inner join mz_gh gh
on gh.ghnm = js.ghnm and gh.OrganizeId = js.OrganizeId
left join xt_brxz brxz
on brxz.brxz = js.brxz and brxz.OrganizeId = js.OrganizeId
left join mz_js_ybjyfy ybfy
on ybfy.jsnm = js.jsnm and ybfy.OrganizeId = js.OrganizeId

where js.OrganizeId = @orgId and js.zt = '1'");

            var pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("@orgId", orgId));

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sb.Append(" and (gh.xm like @keyword or gh.blh like @keyword or gh.mzh like @keyword)");
                pars.Add(new SqlParameter("@keyword", "%" + keyword.Trim() + "%"));
            }
            if (kssj.HasValue)
            {
                sb.Append(" and js.CreateTime >= @kssj");
                pars.Add(new SqlParameter("@kssj", kssj.Value));
            }
            if (jssj.HasValue)
            {
                sb.Append(" and js.CreateTime < @jssj");
                pars.Add(new SqlParameter("@jssj", jssj.Value.AddDays(1)));
            }

            if (containsTF)
            {

            }
            else
            {
                sb.Append(" and isnull(js.tbz,0) = 0 and js.zje > 0");
            }

            return this.QueryWithPage<OutpatientSettlementVO>(sb.ToString(), pagination, pars.ToArray());
        }

        #endregion

        /// <summary>
        /// theo số khám ngoại trú lấy hệ thống thông tin cơ bản bệnh nhân
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public List<SysPatientBasicInfoEntity> SelectSysPatientBasicInfoEntities(string mzh, string orgId)
        {
            const string sql = @"
SELECT brxx.* 
FROM dbo.xt_brjbxx(NOLOCK) brxx
INNER JOIN dbo.mz_gh(NOLOCK) gh ON gh.patid=brxx.patid AND gh.OrganizeId=brxx.OrganizeId AND gh.zt='1'
WHERE brxx.zt='1'
AND brxx.OrganizeId=@orgId 
AND gh.mzh=@mzh
";
            var param = new DbParameter[]
            {
                new SqlParameter("@orgId",orgId ),
                new SqlParameter("@mzh", mzh)
            };
            return FindList<SysPatientBasicInfoEntity>(sql, param);
        }


		#region 重庆医保
	    /// <summary>
	    /// LấyBHYTChờ quyết toán
	    /// </summary>
	    /// <param name="mzh"></param>
	    /// <param name="orgId"></param>
	    /// <returns></returns>
	    public IList<UploadPrescriptionsListInPut> GetCQYBUnSettedListByMzh(Pagination pagination, string mzh, string orgId)
	    {
		    const string strSql = @"
			SELECT    cf.cfh ,
                    CONVERT(VARCHAR(20),mx.CreateTime,120)  kfrq ,
                    yp.ybdm xmyblsh ,
                    mx.yp yynm ,
                    yp.ypmc xmmc ,
                    ISNULL(mx.sl, 0.00) sl ,
                    ISNULL(mx.dj, 0.00) dj ,
                    '0' jzbz ,
                    cf.ysmc cfssmc ,
                    NULL jbr ,
                    mx.dw ,
                    yp.ypgg gg ,
                    yp.jx ,
                    CONVERT(VARCHAR(20),mx.cfmxId) cxmxlsh ,
                    ISNULL(mx.je, 0.00) je ,
                    ks.ybksbm ksbm ,
                    ks.Name ksmc ,
                    --cf.ys ysbm ,
                    isnull(staff.zjh,cf.ys) ysbm,
                    mx.jl mcyl ,
                    NULL yfbz ,
                    NULL zxzq ,
                    '1' xzlb ,
                    cast(isnull(zzfbz,'0') as varchar) zzfbz ,
                    NULL dcyyjl ,
                    NULL dcyyjldw ,
                    NULL dcyl ,
                    NULL zxjldw ,
                    NULL qyzl ,
                    NULL yytj ,
                    NULL sypc ,
                    NULL shid ,
                    NULL yyts ,
                    NULL yylc,
                    case xtxx.cblb when '' then null else yp.gjybdm end gjmldm,
					staff.gjybdm gjysbm
          FROM      mz_cf (NOLOCK) cf
                    INNER JOIN mz_cfmx (NOLOCK) mx ON cf.cfnm = mx.cfnm
                                                      AND mx.OrganizeId = cf.OrganizeId
                                                      AND mx.zt = '1'
                    INNER JOIN dbo.mz_gh (NOLOCK) gh ON gh.ghnm = cf.ghnm
                                                        AND gh.zt = '1'
                                                        AND gh.OrganizeId = cf.OrganizeId
                    INNER JOIN dbo.xt_brjbxx (NOLOCK) xtxx on xtxx.patid=gh.patid and xtxx.OrganizeId=gh.OrganizeId and xtxx.zt=1
                    INNER JOIN NewtouchHIS_Base.dbo.V_C_xt_yp yp ON yp.ypCode = mx.yp
                                                              AND yp.OrganizeId = cf.OrganizeId
                                                              AND yp.zt = '1'
                                                              AND yp.ybbz = '1' AND LEN(ISNULL(yp.ybdm,''))>0
                    LEFT JOIN NewtouchHIS_Base.dbo.xt_sfdl (NOLOCK) dl ON dl.dlCode = mx.dl
                                                              AND dl.OrganizeId = cf.OrganizeId
                                                              AND dl.zt = '1'
                    LEFT JOIN NewtouchHIS_Base..Sys_Department (NOLOCK) ks ON ks.OrganizeId = cf.OrganizeId
                                                              AND ks.Code = cf.ks
                                                              AND ks.zt = '1'
                    LEFT JOIN NewtouchHIS_Base..Sys_Staff (NOLOCK) staff ON staff.OrganizeId = cf.OrganizeId
                                                              AND staff.gh = cf.ys
                                                              AND staff.zt = '1'
          WHERE     cf.OrganizeId = @orgId
                    AND cf.zt = '1'
                    AND cf.cfzt = '0' --处方有效且未收费
                    AND gh.mzh = @mzh
          UNION ALL
          SELECT    cf.cfh ,
                    CONVERT(VARCHAR(20),xm.CreateTime,120)  kfrq ,
                    sfxm.ybdm xmyblsh ,
                    xm.sfxm yynm ,
                    sfxm.sfxmmc xmmc ,
                    ISNULL(xm.sl, 0.00) sl ,
                    ISNULL(xm.dj, 0.00) dj ,
                    '0' jzbz ,
                    cf.ysmc cfssmc ,
                    NULL jbr ,
                    xm.dw ,
                    NULL gg ,
                    NULL jx ,
                    CONVERT(VARCHAR(20),xm.xmnm) cxmxlsh ,
                    ISNULL(xm.je, 0.00) je ,
                    ks.ybksbm ksbm ,
                    ks.Name ksmc ,
                    staff.zjh ysbm ,
                    NULL mcyl ,
                    NULL yfbz ,
                    NULL zxzq ,
                    '1' xzlb ,
                    cast(isnull(zzfbz,'0') as varchar) zzfbz ,
                    NULL dcyyjl ,
                    NULL dcyyjldw ,
                    NULL dcyl ,
                    NULL zxjldw ,
                    NULL qyzl ,
                    NULL yytj ,
                    NULL sypc ,
                    NULL shid ,
                    NULL yyts ,
                    NULL yylc,
                    case xtxx.cblb when '' then null else sfxm.gjybdm end gjmldm,
					staff.gjybdm gjysbm
          FROM      dbo.mz_xm (NOLOCK) xm
                    INNER JOIN dbo.mz_gh (NOLOCK) gh ON gh.ghnm = xm.ghnm
                                                        AND gh.OrganizeId = xm.OrganizeId
                                                        AND gh.zt = '1'
                    INNER JOIN dbo.xt_brjbxx (NOLOCK) xtxx on xtxx.patid=gh.patid and xtxx.OrganizeId=gh.OrganizeId and xtxx.zt=1
                    LEFT JOIN dbo.mz_cf (NOLOCK) cf ON cf.cfnm = xm.cfnm
                                                       AND cf.OrganizeId = xm.OrganizeId
                    INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_sfxm sfxm ON sfxm.sfxmCode = xm.sfxm
                                                              AND sfxm.OrganizeId = xm.OrganizeId
                                                              AND sfxm.ybbz = '1' AND LEN(ISNULL(sfxm.ybdm,''))>0
                    LEFT JOIN NewtouchHIS_Base.dbo.xt_sfdl (NOLOCK) dl ON dl.dlCode = xm.dl
                                                              AND dl.OrganizeId = xm.OrganizeId
                                                              AND dl.zt = '1'
                    LEFT JOIN NewtouchHIS_Base..Sys_Department (NOLOCK) ks ON ks.OrganizeId = cf.OrganizeId
                                                              AND ks.Code = cf.ks
                                                              AND ks.zt = '1'
                    LEFT JOIN NewtouchHIS_Base..Sys_Staff (NOLOCK) staff ON staff.OrganizeId = cf.OrganizeId
                                                              AND staff.gh = cf.ys
                                                              AND staff.zt = '1'
          WHERE     xm.OrganizeId = @orgId
                    AND xm.zt = '1'
                    AND xm.xmzt = '0' --有效且未收费	
                    AND ( cf.zt IS NULL
                          OR ( cf.zt = '1'
                               AND cf.cfzt = '0'
                             )
                        ) --未关联处方 或 处方有效且未收费
                    AND gh.mzh = @mzh
			";
		    IList<SqlParameter> inSqlParameterList = null;
			inSqlParameterList = new List<SqlParameter>();
		    inSqlParameterList.Add(new SqlParameter("@mzh", mzh));
		    inSqlParameterList.Add(new SqlParameter("@orgId", orgId));
			return this.QueryWithPage<UploadPrescriptionsListInPut>(strSql.ToString(), pagination, inSqlParameterList.ToArray()).ToList(); 
	    }

	    /// <summary>
	    /// Lấychi phí bảo hiểm y tế số tiền
	    /// </summary>
	    /// <param name="mzh"></param>
	    /// <param name="orgId"></param>
	    /// <returns></returns>
	    public IList<Input_2204> GetCQZFUnSettedListByMzh(string mzh, string cfnm, string orgId)
	    {
		    const string strSql = @"
			select issc,SUM(je) je from (
SELECT    ISNULL(mx.sl, 0.00) sl ,
                    ISNULL(mx.dj, 0.00) dj ,
					ISNULL(mx.je, 0.00) je ,
					 yp.ybdm xmyblsh ,
					 yp.ybbz,
					case when isnull(yp.zfxz,'1')!='1' then 1 else 0 end issc
          FROM      mz_cf (NOLOCK) cf
                    INNER JOIN mz_cfmx (NOLOCK) mx ON cf.cfnm = mx.cfnm
                                                      AND mx.OrganizeId = cf.OrganizeId
                                                      AND mx.zt = '1'
                    INNER JOIN dbo.mz_gh (NOLOCK) gh ON gh.ghnm = cf.ghnm
                                                        AND gh.zt = '1'
                                                        AND gh.OrganizeId = cf.OrganizeId
                    INNER JOIN NewtouchHIS_Base.dbo.V_C_xt_yp yp ON yp.ypCode = mx.yp
                                                              AND yp.OrganizeId = cf.OrganizeId
                                                              AND yp.zt = '1'
                                                             
          WHERE     cf.OrganizeId = @orgId
                    AND cf.zt = '1'
                    AND cf.cfzt = '0' --处方有效且未收费
                    AND gh.mzh =@mzh
                    AND cf.cfnm IN (SELECT * FROM dbo.f_split(@cfnm, ','))
          UNION ALL
          SELECT   
                    ISNULL(xm.sl, 0.00) sl ,
                    ISNULL(xm.dj, 0.00) dj ,
                    ISNULL(xm.je, 0.00) je ,
					sfxm.ybdm xmyblsh ,sfxm.ybbz,
					case when isnull(sfxm.zfxz,'1')!='1' then 1 else 0 end issc
          FROM      dbo.mz_xm (NOLOCK) xm
                    INNER JOIN dbo.mz_gh (NOLOCK) gh ON gh.ghnm = xm.ghnm
                                                        AND gh.OrganizeId = xm.OrganizeId
                                                        AND gh.zt = '1'
                    LEFT JOIN dbo.mz_cf (NOLOCK) cf ON cf.cfnm = xm.cfnm
                                                       AND cf.OrganizeId = xm.OrganizeId
                    INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_sfxm sfxm ON sfxm.sfxmCode = xm.sfxm
                                                              AND sfxm.OrganizeId = xm.OrganizeId
          WHERE     xm.OrganizeId =  @orgId
                    AND xm.zt = '1'
                    AND xm.xmzt = '0' --有效且未收费	
                    AND ( cf.zt IS NULL
                          OR ( cf.zt = '1'
                               AND cf.cfzt = '0'
                             )
                        ) --未关联处方 或 处方有效且未收费
                    AND gh.mzh = @mzh
                    AND cf.cfnm IN (SELECT * FROM dbo.f_split(@cfnm, ','))
					) as t group by issc
			";
		    IList<SqlParameter> inSqlParameterList = null;
		    inSqlParameterList = new List<SqlParameter>();
		    inSqlParameterList.Add(new SqlParameter("@mzh", mzh));
		    inSqlParameterList.Add(new SqlParameter("@orgId", orgId));
            inSqlParameterList.Add(new SqlParameter("@cfnm", cfnm));
            return this.FindList<Input_2204>(strSql.ToString(), inSqlParameterList.ToArray()); 
	    }

	    public IList<Input_2204> GetChongQingGHMzjs(string ghxm, string zlxm, bool isCkf, bool isGbf,
		    string orgId = null)
	    {
		    string ckf = "";
		    string gbf = "";
			if (isCkf)
			{
				var pzckf = _sysConfigRepo.GetByCode(Constants.xtmzpz.SFXM_GHCKF, orgId);
			    ckf = pzckf.Value;
			}
			if (isGbf)
			{
				var pzgbf = _sysConfigRepo.GetByCode(Constants.xtmzpz.SFXM_GHGBF, orgId);
				gbf = pzgbf.Value;
			}
			IList<SqlParameter> inSqlParameterList = null;
			StringBuilder strBuilder = new StringBuilder();
				strBuilder.Append(@"
			select issc,SUM(je) je from (
SELECT  case when isnull(zfxz,'1')!='1' then 1 else 0 end issc,
		ybbz,ybdm,dj je
FROM    NewtouchHIS_Base..V_S_xt_sfxm
WHERE   OrganizeId = @orgId AND zt='1'
        AND (sfxmCode IN ( @ghxm ) or sfxmCode IN ( select zlxm from mz_gh_zlxmzh with(nolock) where zt='1' 
and OrganizeId=@orgId and zhcode=@zlxm ) or sfxmCode IN ( @ckf ) or sfxmCode IN ( @gbf ))
)  a group by issc

			");
		    inSqlParameterList = new List<SqlParameter>();
			inSqlParameterList.Add(new SqlParameter("@ghxm", ghxm));
			inSqlParameterList.Add(new SqlParameter("@zlxm", zlxm));
		    inSqlParameterList.Add(new SqlParameter("@ckf", ckf));
		    inSqlParameterList.Add(new SqlParameter("@gbf", gbf));
			inSqlParameterList.Add(new SqlParameter("@orgId", orgId));
		    return this.FindList<Input_2204>(strBuilder.ToString(), inSqlParameterList.ToArray());
		}

	    public Input_2203A GetCQjzdjInfo(string mzh, string orgId)
	    {
			StringBuilder strSql = new StringBuilder();
		    strSql.Append(@"
SELECT   
			b.jzid mdtrt_id,
			c.grbh psn_no,
			case b.mjzbz  when '2' then '13' when '4' then '14' when '6' then '14' when '5' then '9901' 
		    when '7' then '14'  when '8' then '19'  when '9' then '51' when '10' then '9906' else (case when xzlx='390' then '11' else '11' end) end med_type,
            case b.mjzbz when '9'  then '1' else '' end matn_type,
			case b.mjzbz when '9'  then '9' else '' end birctrl_type, case b.mjzbz when '9'  then convert(varchar(10),getdate(),120) else '' 
			end birctrl_matn_date,b.bzbm dise_codg,b.bzmc dise_name
  FROM      [NewtouchHIS_Sett].[dbo].[xt_brjbxx]  a
			LEFT JOIN [NewtouchHIS_Sett]..mz_gh b on  a.patid=b.patid and b.OrganizeId = a.OrganizeId AND b.zt = '1'
            LEFT JOIN [NewtouchHIS_Sett]..xt_card c on c.cardno=b.kh and c.CardType=b.CardType and c.zt='1'
  WHERE     b.mzh = @mzh
            AND a.OrganizeId = @OrganizeId
            AND a.zt = '1'; ");
		    SqlParameter[] par =
		    {
			    new SqlParameter("@mzh", mzh),
			    new SqlParameter("@OrganizeId", orgId)
		    };
		    return this.FindList<Input_2203A>(strSql.ToString(), par).FirstOrDefault();
		}

	    public IList<UploadPrescriptionsListInPut> GetCQYBTfListByJsnm(string jsnm, string mzh, string orgId)
	    {
			//cxmxlsh tồn tại vàojsmxnm
		    const string strSql = @"
			SELECT  *
FROM    ( SELECT    ISNULL(cf.cfh,'YP'+ CONVERT(VARCHAR(50),a.jsmxnm)) cfh ,
                    CONVERT(VARCHAR(20),mx.CreateTime,120)  kfrq ,
                    yp.ybdm xmyblsh ,
                    mx.yp yynm ,
                    yp.ypmc xmmc ,
                    ISNULL(mx.sl, 0.00) sl ,
                    ISNULL(mx.dj, 0.00) dj ,
                    NULL jzbz ,
                    cf.ysmc cfssmc ,
                    NULL jbr ,
                    mx.dw ,
                    yp.ypgg gg ,
                    yp.jx ,
                    CONVERT(VARCHAR(30),a.jsmxnm) cxmxlsh ,
                    ISNULL(mx.je, 0.00) je ,
                    ks.ybksbm ksbm ,
                    ks.Name ksmc ,
                    --cf.ys ysbm ,
                    staff.zjh ysbm,
                    NULL mcyl ,
                    NULL yfbz ,
                    NULL zxzq ,
                    '1' xzlb ,
                    cast(isnull(zzfbz,zzfbz) as varchar) zzfbz ,
                    NULL dcyyjl ,
                    NULL dcyyjldw ,
                    NULL dcyl ,
                    NULL zxjldw ,
                    NULL qyzl ,
                    NULL yytj ,
                    NULL sypc ,
                    NULL shid ,
                    NULL yyts ,
                    NULL yylc,
                    yp.gjybdm gjmldm,
					staff.gjybdm gjysdm
          FROM      [NewtouchHIS_Sett].[dbo].[mz_jsmx] (NOLOCK) a
                    LEFT JOIN [NewtouchHIS_Sett].[dbo].[mz_cfmx] (NOLOCK) mx ON a.cf_mxnm = mx.cfmxId
                                                              AND mx.OrganizeId = a.OrganizeId
                                                              AND mx.zt = '1'
                    LEFT JOIN [NewtouchHIS_Sett].[dbo].[mz_cf] (NOLOCK) cf ON cf.cfnm = mx.cfnm
                                                              AND cf.OrganizeId = mx.OrganizeId
                                                              AND cf.zt = '1'
                    INNER JOIN NewtouchHIS_Base..V_C_xt_yp yp ON yp.ypCode = mx.yp
                                                              AND yp.OrganizeId = mx.OrganizeId
                                                              AND yp.zt = '1'
															  AND isnull(yp.zfxz,'1')!='1'
                    LEFT JOIN NewtouchHIS_Base.dbo.V_S_xt_sfdl sfdl ON sfdl.dlCode = yp.dlCode
                                                              AND sfdl.zt = '1'
                                                              AND sfdl.OrganizeId = a.OrganizeId
                    LEFT JOIN NewtouchHIS_Base..Sys_Department (NOLOCK) ks ON ks.OrganizeId = cf.OrganizeId
                                                              AND ks.Code = cf.ks
                                                              AND ks.zt = '1'
                    LEFT JOIN NewtouchHIS_Base..Sys_Staff (NOLOCK) staff ON staff.OrganizeId = cf.OrganizeId
                                                              AND staff.gh = cf.ys
                                                              AND staff.zt = '1'
          WHERE     a.jsnm = @jsnm
                    AND ISNULL(a.sl,0)>0
                    AND ISNULL(a.cf_mxnm, 0) != 0
                    AND a.OrganizeId = @orgId
                    AND a.zt = '1'
          UNION ALL
          SELECT    ISNULL(cf.cfh,'XM'+ CONVERT(VARCHAR(50),a.jsmxnm)) cfh ,
                    CONVERT(VARCHAR(20),xm.CreateTime,120) kfrq ,
                    sfxm.ybdm xmyblsh ,
                    xm.sfxm yynm ,
                    sfxm.sfxmmc xmmc ,
                    ISNULL(xm.sl, 0.00) sl ,
                    ISNULL(xm.dj, 0.00) dj ,
                    NULL jzbz ,
                    cf.ysmc cfssmc ,
                    NULL jbr ,
                    xm.dw ,
                    NULL gg ,
                    NULL jx ,
                    CONVERT(VARCHAR(30),a.jsmxnm) cxmxlsh ,
                    ISNULL(xm.je, 0.00) je ,
                    ks.ybksbm ksbm ,
                    ks.Name ksmc ,
                    staff.zjh ysbm ,
                    NULL mcyl ,
                    NULL yfbz ,
                    NULL zxzq ,
                    '1' xzlb ,
                    cast(isnull(zzfbz,zzfbz) as varchar) zzfbz ,
                    NULL dcyyjl ,
                    NULL dcyyjldw ,
                    NULL dcyl ,
                    NULL zxjldw ,
                    xm.sl qyzl ,
                    NULL yytj ,
                    NULL sypc ,
                    NULL shid ,
                    NULL yyts ,
                    NULL yylc,
                    sfxm.gjybdm gjmldm,
					staff.gjybdm gjysdm
          FROM      [NewtouchHIS_Sett].[dbo].[mz_jsmx] (NOLOCK) a
                    LEFT JOIN [NewtouchHIS_Sett].[dbo].[mz_xm] (NOLOCK) xm ON a.mxnm = xm.xmnm
                                                              AND xm.OrganizeId = a.OrganizeId
                                                              AND xm.zt = '1'
                    LEFT JOIN [NewtouchHIS_Sett].[dbo].[mz_cf] (NOLOCK) cf ON cf.cfnm = xm.cfnm
                                                              AND cf.OrganizeId = xm.OrganizeId
                                                              AND cf.zt = '1'
                    INNER JOIN NewtouchHIS_Base..V_S_xt_sfxm sfxm ON sfxm.sfxmCode = xm.sfxm
                                                              AND sfxm.OrganizeId = xm.OrganizeId
                                                              AND sfxm.zt = '1'
															  AND isnull(sfxm.zfxz,'1')!='1' 
                    LEFT JOIN NewtouchHIS_Base.dbo.V_S_xt_sfdl sfdl ON sfdl.dlCode = sfxm.sfdlCode
                                                              AND sfdl.zt = '1'
                                                              AND sfdl.OrganizeId = a.OrganizeId
                    LEFT JOIN NewtouchHIS_Base..Sys_Department (NOLOCK) ks ON ks.OrganizeId = cf.OrganizeId
                                                              AND ks.Code = cf.ks
                                                              AND ks.zt = '1'
                    LEFT JOIN NewtouchHIS_Base..Sys_Staff (NOLOCK) staff ON staff.OrganizeId = cf.OrganizeId
                                                              AND staff.gh = cf.ys
                                                              AND staff.zt = '1'
          WHERE     a.jsnm = @jsnm
                    AND ISNULL(a.sl,0)>0
                    AND ISNULL(a.mxnm, 0) != 0
                    AND a.OrganizeId = @orgId
                    AND a.zt = '1'
        ) nn
			";
		    IList<SqlParameter> inSqlParameterList = new List<SqlParameter>();
		    inSqlParameterList.Add(new SqlParameter("@mzh", mzh));
		    inSqlParameterList.Add(new SqlParameter("@orgId", orgId));
		    inSqlParameterList.Add(new SqlParameter("@jsnm", jsnm));
			return this.FindList<UploadPrescriptionsListInPut>(strSql.ToString(), inSqlParameterList.ToArray()).ToList();
		}

		public IList<UploadPrescriptionsListInPut> GetCQZFTfListByJsnm(string jsnm, string mzh, string orgId)
	    {
			//cxmxlsh tồn tại vàojsmxnm
		    //cxmxlsh tồn tại vàojsmxnm
		    const string strSql = @"
			SELECT  *
FROM    ( SELECT    cf.cfh ,
                    CONVERT(VARCHAR(20),mx.CreateTime,120) kfrq ,
                    yp.ybdm xmyblsh ,
                    mx.yp yynm ,
                    yp.ypmc xmmc ,
                    ISNULL(mx.sl, 0.00) sl ,
                    ISNULL(mx.dj, 0.00) dj ,
                    NULL jzbz ,
                    cf.ysmc cfssmc ,
                    NULL jbr ,
                    mx.dw ,
                    yp.ypgg gg ,
                    yp.jx ,
                    CONVERT(VARCHAR(20),a.jsmxnm) cxmxlsh ,
                    ISNULL(mx.je, 0.00) je ,
                    cf.ks ksbm ,
                    ks.Name ksmc ,
                    cf.ys ysbm ,
                    NULL mcyl ,
                    NULL yfbz ,
                    NULL zxzq ,
                    '1' xzlb ,
                    cast(isnull(zzfbz,zzfbz) as varchar) zzfbz ,
                    NULL dcyyjl ,
                    NULL dcyyjldw ,
                    NULL dcyl ,
                    NULL zxjldw ,
                    NULL qyzl ,
                    NULL yytj ,
                    NULL sypc ,
                    NULL shid ,
                    NULL yyts ,
                    NULL yylc,
                    yp.gjybdm gjmldm,
					staff.gjybdm gjysdm
          FROM      [NewtouchHIS_Sett].[dbo].[mz_jsmx] (NOLOCK) a
                    LEFT JOIN [NewtouchHIS_Sett].[dbo].[mz_cfmx] (NOLOCK) mx ON a.cf_mxnm = mx.cfmxId
                                                              AND mx.OrganizeId = a.OrganizeId
                                                              AND mx.zt = '1'
                    LEFT JOIN [NewtouchHIS_Sett].[dbo].[mz_cf] (NOLOCK) cf ON cf.cfnm = mx.cfnm
                                                              AND cf.OrganizeId = mx.OrganizeId
                                                              AND cf.zt = '1'
                    INNER JOIN NewtouchHIS_Base..V_C_xt_yp yp ON yp.ypCode = mx.yp
                                                              AND yp.OrganizeId = mx.OrganizeId
                                                              AND yp.zt = '1'
															  AND isnull(yp.zfxz,'1')='1'
                    LEFT JOIN NewtouchHIS_Base.dbo.V_S_xt_sfdl sfdl ON sfdl.dlCode = yp.dlCode
                                                              AND sfdl.zt = '1'
                                                              AND sfdl.OrganizeId = a.OrganizeId
                    LEFT JOIN NewtouchHIS_Base..Sys_Department (NOLOCK) ks ON ks.OrganizeId = cf.OrganizeId
                                                              AND ks.Code = cf.ks
                                                              AND ks.zt = '1'
                    LEFT JOIN NewtouchHIS_Base..Sys_Staff (NOLOCK) staff ON staff.OrganizeId = cf.OrganizeId
                                                              AND staff.gh = cf.ys
                                                              AND staff.zt = '1'
          WHERE     a.jsnm = @jsnm
                    AND ISNULL(a.sl,0)>0
                    AND ISNULL(a.cf_mxnm, 0) != 0
                    AND a.OrganizeId = @orgId
                    AND a.zt = '1'
          UNION ALL
          SELECT    cf.cfh ,
                    CONVERT(VARCHAR(20),xm.CreateTime,120) kfrq ,
                    sfxm.ybdm xmyblsh ,
                    xm.sfxm yynm ,
                    sfxm.sfxmmc xmmc ,
                    ISNULL(xm.sl, 0.00) sl ,
                    ISNULL(xm.dj, 0.00) dj ,
                    NULL jzbz ,
                    cf.ysmc cfssmc ,
                    NULL jbr ,
                    xm.dw ,
                    NULL gg ,
                    NULL jx ,
                    CONVERT(VARCHAR(20),a.jsmxnm) cxmxlsh ,
                    ISNULL(xm.je, 0.00) je ,
                    cf.ks ksbm ,
                    ks.Name ksmc ,
                    staff.zjh ysbm ,
                    NULL mcyl ,
                    NULL yfbz ,
                    NULL zxzq ,
                    '1' xzlb ,
                    cast(isnull(zzfbz,zzfbz) as varchar) zzfbz ,
                    NULL dcyyjl ,
                    NULL dcyyjldw ,
                    NULL dcyl ,
                    NULL zxjldw ,
                    xm.sl qyzl ,
                    NULL yytj ,
                    NULL sypc ,
                    NULL shid ,
                    NULL yyts ,
                    NULL yylc,
                    sfxm.gjybdm gjmldm,
					staff.gjybdm gjysdm
          FROM      [NewtouchHIS_Sett].[dbo].[mz_jsmx] (NOLOCK) a
                    LEFT JOIN [NewtouchHIS_Sett].[dbo].[mz_xm] (NOLOCK) xm ON a.mxnm = xm.xmnm
                                                              AND xm.OrganizeId = a.OrganizeId
                                                              AND xm.zt = '1'
                    LEFT JOIN [NewtouchHIS_Sett].[dbo].[mz_cf] (NOLOCK) cf ON cf.cfnm = xm.cfnm
                                                              AND cf.OrganizeId = xm.OrganizeId
                                                              AND cf.zt = '1'
                    INNER JOIN NewtouchHIS_Base..V_S_xt_sfxm sfxm ON sfxm.sfxmCode = xm.sfxm
                                                              AND sfxm.OrganizeId = xm.OrganizeId
                                                              AND sfxm.zt = '1'
															 AND isnull(sfxm.zfxz,'1')='1' 
                    LEFT JOIN NewtouchHIS_Base.dbo.V_S_xt_sfdl sfdl ON sfdl.dlCode = sfxm.sfdlCode
                                                              AND sfdl.zt = '1'
                                                              AND sfdl.OrganizeId = a.OrganizeId
                    LEFT JOIN NewtouchHIS_Base..Sys_Department (NOLOCK) ks ON ks.OrganizeId = cf.OrganizeId
                                                              AND ks.Code = cf.ks
                                                              AND ks.zt = '1'
                    LEFT JOIN NewtouchHIS_Base..Sys_Staff (NOLOCK) staff ON staff.OrganizeId = cf.OrganizeId
                                                              AND staff.gh = cf.ys
                                                              AND staff.zt = '1'
          WHERE     a.jsnm = @jsnm
                    AND ISNULL(a.sl,0)>0
                    AND ISNULL(a.mxnm, 0) != 0
                    AND a.OrganizeId = @orgId
                    AND a.zt = '1'
        ) nn
			";
			IList<SqlParameter> inSqlParameterList = new List<SqlParameter>();
		    inSqlParameterList.Add(new SqlParameter("@mzh", mzh));
		    inSqlParameterList.Add(new SqlParameter("@orgId", orgId));
		    inSqlParameterList.Add(new SqlParameter("@jsnm", jsnm));
			return this.FindList<UploadPrescriptionsListInPut>(strSql.ToString(), inSqlParameterList.ToArray());
		}

        public IList<TbbzmlDto> GetMzbzml(string mllx)
        {
            const string sql = @" select mtbbzmldm,mtbbzdlmc,mtbbzflmc from NewtouchHIS_Base..Mz_Mtbbzml where mllx=@mllx ";
            var par = new DbParameter[] {
                new SqlParameter("@mllx", mllx)
            };
            return FindList<TbbzmlDto>(sql, par);
        }
        #endregion
    }
}