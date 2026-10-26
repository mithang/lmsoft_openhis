
using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Common;
using Newtouch.Common.Operator;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.DTO;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.IRepository.OutpatientManage;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.ValueObjects.OutpatientManage;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using static Newtouch.Infrastructure.Constants;

namespace Newtouch.HIS.DomainServices
{
    /// <summary>
    /// Quyết toán ngoại trú
    /// </summary>
    public class OutPatientSettleDmnService : DmnServiceBase, IOutPatientSettleDmnService
    {
        private readonly IOutpatientRegistRepo _outpatientRegRepo;
        private readonly ISysConfigRepo _sysConfigRepo;
        private readonly IOutpatientSettlementRepo _outPatientSettleRepo;
        private readonly ISysPatientBasicInfoRepo _sysPatBasicInfoRepository;
        private readonly IOutpatientSettlementDetailRepo _outPatientSettleDetailRepo;
        private readonly IOutpatientSettlementPaymentModelRepo _outPatientSettlePayMethodRepo;
        private readonly IOutpatientSettlementCategoryRepo _outPatientSettleCategRepo;
        private readonly IFinancialInvoiceRepo _financialInvoiceEntityRepository;
        private readonly IOutpatientSettlementCategoryRepo _outPatientSettlementCategoryRepo;
        private readonly ISysCardRepo _sysCardRepo;
        private readonly ISysPatientChargeWaiverRepo _sysPatiChargeWaiRepo; // Số tiền giảm trừ
        private readonly IFinancialInvoiceRepo _financialInvoiceRepo;
        private readonly IOutpatientItemRepo _outpatientItemRepo;
        private readonly IOutPatientDmnService _outPatientDmnService;
        private readonly ICqybSett23Repo _iCqybSett23Repo;
        public OutPatientSettleDmnService(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// Lấy phí đ ăng ký khám chẩn đoán và điều trị 费 phí thẻ từ phí làm thẻ
        /// </summary>
        /// <param name="ghxm">Loại đ ăng ký khám:Ngoại trú cấp cứu đ ặc biệt </param>
        /// <param name="isZzjm"> chuyển tuyến giảm trừ</param>
        /// <param name="isCkf">Phí thẻ từ</param>
        /// <param name="isGbf">Phí làm thẻ</param>
        /// <returns></returns>
        public GHZGroupAndFeesVO GetOutpatientFees(string ghxm, string zlxm, bool isCkf, bool isGbf,string orgId=null)
        {
            var fee = new GHZGroupAndFeesVO();
            var sfxmList = new Dictionary<string, SysChargeItemVEntity>();
			if (string.IsNullOrEmpty(orgId))
			{
				orgId = OperatorProvider.GetCurrent().OrganizeId;
			}
			if (!string.IsNullOrWhiteSpace(ghxm))
            {
                //Lấy phí đ ăng ký khám
                var ghsfxmSql = new StringBuilder(@"select * from NewtouchHIS_Base..V_S_xt_sfxm where sfxmCode in('" + ghxm + "') and OrganizeId=@OrganizeId");
                DbParameter[] param = {
                    new SqlParameter ("@OrganizeId",orgId)
                };
                var ghsfxmEntity = FindList<SysChargeItemVEntity>(ghsfxmSql.ToString(), param).FirstOrDefault();
                fee.ghfPrice = ghsfxmEntity.dj;
                sfxmList.Add("ghsfxm", ghsfxmEntity);
            }
            if (!string.IsNullOrWhiteSpace(zlxm))
            {
                //Và chẩn đoán và điều trị 费
                var zlsfxmSql = new StringBuilder(@"select * from NewtouchHIS_Base..V_S_xt_sfxm where sfxmCode in('" + zlxm + "') and OrganizeId=@OrganizeId");
                DbParameter[] param2 = {
                    new SqlParameter ("@OrganizeId",orgId)
                };
                var zlsfxmEntity = FindList<SysChargeItemVEntity>(zlsfxmSql.ToString(), param2).FirstOrDefault();
                fee.zlfPrice = zlsfxmEntity.dj;
                sfxmList.Add("zlsfxm", zlsfxmEntity);
            }

            if (isCkf)
            {
                var pzmz = _sysConfigRepo.GetByCode(Constants.xtmzpz.SFXM_GHCKF, orgId);
                var cksfxmSql = new StringBuilder(@"select * from NewtouchHIS_Base..V_S_xt_sfxm where sfxmCode in('" + pzmz.Value + "') and OrganizeId=@OrganizeId");
                DbParameter[] param = {
                    new SqlParameter ("@OrganizeId",orgId)
                };
                var cksfxmEntity = FindList<SysChargeItemVEntity>(cksfxmSql.ToString(), param).FirstOrDefault();
                if (cksfxmEntity == null)
                {
                    throw new FailedCodeException("THE_CIKA_CHARGE_ITEM_WAS_NOT_FOUND");
                }
                fee.ckfPrice = cksfxmEntity.dj;
                sfxmList.Add("cksfxm", cksfxmEntity);
            }
            else
            {
                fee.ckfPrice = 0;
            }
            if (isGbf)
            {
                var pzmz = _sysConfigRepo.GetByCode(Constants.xtmzpz.SFXM_GHGBF, orgId);
                var gbsfxmSql = new StringBuilder(@"select * from NewtouchHIS_Base..V_S_xt_sfxm where sfxmCode in('" + pzmz.Value + "') and OrganizeId=@OrganizeId");
                DbParameter[] param = {
                    new SqlParameter ("@OrganizeId",orgId)
                };
                var gbsfxmEntity = FindList<SysChargeItemVEntity>(gbsfxmSql.ToString(), param).FirstOrDefault();
                if (gbsfxmEntity == null)
                {
                    throw new FailedCodeException("THE_GONGBEN_CHARGE_ITEM_WAS_NOT_FOUND");
                }
                fee.gbfPrice = gbsfxmEntity.dj;
                sfxmList.Add("gbsfxm", gbsfxmEntity);
            }
            else
            {
                fee.gbfPrice = 0;
            }
            fee.totalfees = fee.ghfPrice + fee.zlfPrice + fee.ckfPrice + fee.gbfPrice;
            fee.sfxmList = sfxmList;
            return fee;
        }

        #region 项目组合费用
        public GHZGroupAndFeesVO GetOutpatientFeesbyGroup(string ghxm, string zlxm, bool isCkf, bool isGbf, string orgId = null)
        {
            var fee = new GHZGroupAndFeesVO();
            var sfxmzhList = new Dictionary<string, List<SysChargeItemVEntity>>();
            if (string.IsNullOrEmpty(orgId))
            {
                orgId = OperatorProvider.GetCurrent().OrganizeId;
            }
            if (!string.IsNullOrWhiteSpace(ghxm))
            {
                //Lấy phí đ ăng ký khám
                var ghsfxmSql = new StringBuilder(@"select * from NewtouchHIS_Base..V_S_xt_sfxm where sfxmCode in('" + ghxm + "') and OrganizeId=@OrganizeId");
                var ghsfxmEntity = FindList<SysChargeItemVEntity>(ghsfxmSql.ToString(), new SqlParameter[] { new SqlParameter("OrganizeId", orgId)});
                if (ghsfxmEntity != null && ghsfxmEntity.Count > 0)
                {
                    foreach (var g in ghsfxmEntity)
                    {
                        fee.ghfPrice += g.dj;
                    }
                }
                else if (ghsfxmEntity.Count > 0)
                {
                    fee.ghfPrice = ghsfxmEntity.FirstOrDefault().dj;
                }
                sfxmzhList.Add("ghsfxm", ghsfxmEntity);
            }
            if (!string.IsNullOrWhiteSpace(zlxm))
            {
                //Và chẩn đoán và điều trị 费
                var zlsfxmSql = new StringBuilder(@"select * from NewtouchHIS_Base..V_S_xt_sfxm with(nolock)
where sfxmCode in( select zlxm from mz_gh_zlxmzh with(nolock) where zt='1' and OrganizeId=@OrganizeId and zhcode='" + zlxm + "') and OrganizeId=@OrganizeId");
                var zlsfxmEntity = FindList<SysChargeItemVEntity>(zlsfxmSql.ToString(), new SqlParameter[] { new SqlParameter("OrganizeId", orgId) });
                if (zlsfxmEntity != null && zlsfxmEntity.Count > 0)
                {
                    foreach (var z in zlsfxmEntity)
                    {
                        fee.zlfPrice += z.dj;
                    }
                }
                else if(zlsfxmEntity.Count>0) {
                    fee.zlfPrice = zlsfxmEntity.FirstOrDefault().dj;
                }
                sfxmzhList.Add("zlsfxm", zlsfxmEntity);
            }

            if (isCkf)
            {
                var pzmz = _sysConfigRepo.GetByCode(Constants.xtmzpz.SFXM_GHCKF, orgId);
                var cksfxmSql = new StringBuilder(@"select * from NewtouchHIS_Base..V_S_xt_sfxm where sfxmCode in('" + pzmz.Value + "') and OrganizeId=@OrganizeId");
                var cksfxmEntity = FindList<SysChargeItemVEntity>(cksfxmSql.ToString(), new SqlParameter[] { new SqlParameter("OrganizeId", orgId) });
                if (cksfxmEntity == null)
                {
                    throw new FailedCodeException("THE_CIKA_CHARGE_ITEM_WAS_NOT_FOUND");
                }
                fee.ckfPrice = cksfxmEntity.FirstOrDefault().dj;
                sfxmzhList.Add("cksfxm", cksfxmEntity);
            }
            else
            {
                fee.ckfPrice = 0;
            }
            if (isGbf)
            {
                var pzmz = _sysConfigRepo.GetByCode(Constants.xtmzpz.SFXM_GHGBF, orgId);
                var gbsfxmSql = new StringBuilder(@"select * from NewtouchHIS_Base..V_S_xt_sfxm where sfxmCode in('" + pzmz.Value + "') and OrganizeId=@OrganizeId");
                var gbsfxmEntity = FindList<SysChargeItemVEntity>(gbsfxmSql.ToString(), new SqlParameter[] { new SqlParameter("OrganizeId", orgId) });
                if (gbsfxmEntity == null)
                {
                    throw new FailedCodeException("THE_GONGBEN_CHARGE_ITEM_WAS_NOT_FOUND");
                }
                fee.gbfPrice = gbsfxmEntity.FirstOrDefault().dj;
                sfxmzhList.Add("gbsfxm", gbsfxmEntity);
            }
            else
            {
                fee.gbfPrice = 0;
            }
            fee.totalfees = fee.ghfPrice + fee.zlfPrice + fee.ckfPrice + fee.gbfPrice;
            fee.sfxmzhList = sfxmzhList;
            return fee;
        }

        #endregion 

        #region 查询当日已挂号

        /// <summary>
        /// Tra cứukhi NgàyĐ ãĐ ăng ký khám
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="creatorCode"></param>
        /// <returns></returns>
        public IList<OutPatientRegInfoVO> GetCurrentDayRegList(string orgId, string creatorCode)
        {
            var sb = new StringBuilder();
            var pars = new List<SqlParameter>();
	        var GhSelectOnlyCreator = _sysConfigRepo.GetValueByCode("GhSelectOnlyCreator", orgId);
			sb.Append(@"select staff.Name ysmc,dept.Name ksmc,brxz.brxzmc,* 
from mz_gh(nolock) gh
left join [NewtouchHIS_Base]..V_S_Sys_Staff staff
on staff.gh = gh.ys and staff.Organizeid = gh.OrganizeId
left join [NewtouchHIS_Base]..V_S_Sys_Department dept
on dept.Code = gh.ks and dept.Organizeid = gh.OrganizeId
inner join xt_card kxx
on kxx.CardNo=gh.kh and kxx.CardType=gh.CardType and kxx.OrganizeId=gh.OrganizeId and kxx.zt=1
left join xt_brxz brxz
on brxz.brxz = kxx.brxz and brxz.OrganizeId = kxx.OrganizeId and brxz.zt=1
left join xt_brjbxx jbxx
on jbxx.patid = gh.patid and jbxx.zt = '1' and jbxx.OrganizeId = gh.OrganizeId
where gh.OrganizeId = @orgId  and gh.zt = '1'
and gh.CreateTime >= CONVERT(varchar(100), GETDATE(), 23) ");
	        if (!string.IsNullOrEmpty(creatorCode) && GhSelectOnlyCreator=="ON")
	        {
		        sb.Append(@" and gh.CreatorCode = @creatorCode ");
		        pars.Add(new SqlParameter("@creatorCode", creatorCode));
			}
	        sb.Append(@" order by gh.CreateTime desc ");
			pars.Add(new SqlParameter("@orgId", orgId));

            return this.FindList<OutPatientRegInfoVO>(sb.ToString(), pars.ToArray());
        }

        #endregion

        /// <summary>
        /// Ngoại trú trả lại số (chưa结Đ ăng ký khám)
        /// </summary>
        /// <param name="mzh"></param>
        /// <returns></returns>
        public void UnSettedRegBackNum(string mzh, string orgId)
        {
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                var regEntity = db.IQueryable<OutpatientRegistEntity>(p => p.mzh == mzh && p.OrganizeId == orgId).FirstOrDefault();
                if (regEntity != null)
                {
                    if (regEntity.jzbz == ((int)EnumOutpatientJzbz.Jzz).ToString())
                    {
                        throw new FailedException("就诊中状态不能退号");
                    }
                    var dbXmEntityList = db.IQueryable<OutpatientItemEntity>(p => p.ghnm == regEntity.ghnm && p.zt == "1").ToList();
                    var dbCfEntityList = db.IQueryable<OutpatientPrescriptionEntity>(p => p.ghnm == regEntity.ghnm && p.zt == "1").ToList();
                    if (regEntity.jzbz == ((int)EnumOutpatientJzbz.Jsjz).ToString())
                    {
                        //if (dbXmEntityList.Count > 0 || dbCfEntityList.Count > 0)
                        if (dbCfEntityList.Count > 0)
                        {
                            // chỉ cần 开 qua Đ ơn thuốc thì không có thể trả lại 
                            throw new FailedException("就诊结束状态不能退号");
                        }
                    }
                    regEntity.ghzt = "2";   //2Đ ã hoàn
                    regEntity.Modify();
                    db.Update(regEntity);
                    foreach (var entity in dbXmEntityList)
                    {
                        entity.zt = "0";
                        entity.Modify();
                        db.Update(entity);
                    }
                    foreach (var entity in dbCfEntityList)
                    {
                        entity.zt = "0";
                        entity.Modify();
                        db.Update(entity);
                    }
                    db.Commit();
                }
                else
                {
                    throw new FailedException("未找到挂号信息");
                }
            }
        }

        public bool FullBack(int jsnm, List<OutpatientRegistNonAttendanceEntity> backNumList, bool isReturnAll)
        {
            bool result = false;
            // theo jsnm Lấymz_js bản ghi 
            OutpatientSettlementEntity mzjs = _outPatientSettleRepo.SelectMZJS(jsnm, OperatorProvider.GetCurrent().OrganizeId);
            // theo patid lấykhi trư ớc bệnh nhân hiệu lực của phư ơng thức thanh toán 
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
    		               select * from xt_xjzffs where zt='1' 
		                   and 
		                   ( exists 
			                  (select * from xt_brzh zh where zh.zt='1' and zh.patid=@patid and zh.zhxz = xt_xjzffs.zhxz  and zh.OrganizeId=@OrganizeId)	
		                      or zhxz='a'   --a:all
		                    )
                         ");
            SqlParameter[] param =
                {
                    new SqlParameter("@patid",mzjs.patid),
                    new SqlParameter("@OrganizeId",OperatorProvider.GetCurrent().OrganizeId)
                };
            List<SysCashPaymentModelEntity> xjzffsList = this.FindList<SysCashPaymentModelEntity>(strSql.ToString(), param);

            // thu tiền quyết toán phư ơng thức thanh toán 
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append(@"
    		                     SELECT ROW_NUMBER()OVER(ORDER BY XJZFFSMC) AS NUM, MZ_JSZFFS.*,XJZFFSMC  FROM MZ_JSZFFS
		                         LEFT JOIN XT_XJZFFS ON MZ_JSZFFS.XJZFFS = XT_XJZFFS.XJZFFS
		                         WHERE XT_XJZFFS.ZT = '1' AND MZ_JSZFFS.JSNM = @jsnm and MZ_JSZFFS.OrganizeId=@OrganizeId
                              ");
            SqlParameter[] param2 =
                {
                        new SqlParameter(@"jsnm",jsnm),
                        new SqlParameter("@OrganizeId",OperatorProvider.GetCurrent().OrganizeId)
                    };
            // theo jsnm tra cứu 出mz_jszffs
            List<OutPatientSettlePayMethodVO> mzjszffsList = this.FindList<OutPatientSettlePayMethodVO>(strSql2.ToString(), param2);
            //Tính tổng số tiền thực thu
            if (mzjszffsList.Count > 1)
            {
                // theo jsnm tra cứu 出mz_jszffs trong có几dòng quyết toán ngoại trú thanh toán bản ghi,nếu có nhiều dòng,thì số tiền相加
                //Tính tổng số tiền thực thu
                decimal ssk = 0m;
                foreach (var mzjszffsItem in mzjszffsList)
                {
                    ssk += mzjszffsItem.zfje;
                }
            }
            //Tất cả hoàn tiền
            result = MzJsFullBack(mzjs, backNumList);

            return result;


        }
        /// <summary>
        /// Xác nhậnHoàn toàn bộ
        /// </summary>
        /// <param name="kh">Số thẻ</param>
        /// <param name="isGh">Có hay kh ôngĐ ăng ký khám</param>
        /// <param name="mzJs">Quyết toán ngoại trú</param>
        /// <param name="resultList">Chi tiết</param>
        public bool MzJsFullBack(OutpatientSettlementEntity mzJs, List<OutpatientRegistNonAttendanceEntity> backNumList)
        {
            bool result = false;
            SysPatientBasicInfoEntity brjbxx = _sysPatBasicInfoRepository.GetInfoByPatid(mzJs.patid.ToString(), OperatorProvider.GetCurrent().OrganizeId);
            //Trạng thái quyết toán 2 là đã hoàn
            mzJs.jszt = (int)EnumJieSuanZT.YT;

            // phán đoán có hay không chèn nhiều dòng trùng lặp hoàn toàn bộ bản ghi 
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
			               select count(jsnm) from mz_js b  where  b.jszt=" + (int)EnumJieSuanZT.YT + @" 
			               and b.cxjsnm = @jsnm
			               and b.fph =@fph
                           and b.OrganizeId=@OrganizeId
                         ");
            SqlParameter[] param =
                {
                    new SqlParameter("@jsnm",mzJs.jsnm),
                    new SqlParameter("@fph",mzJs.fph),
                    new SqlParameter("@OrganizeId",OperatorProvider.GetCurrent().OrganizeId)
                };
            int count = this.FindList<int>(strSql.ToString(), param).FirstOrDefault();
            if (count > 0)
            {
                return true;
            }
            //Hệ thống bệnh nhân tài khoản 收支 bản ghi tạm thời không 写
            #region 退款成功，插门诊结算，门诊结算明细。改门诊结算的jsnm.改结算明细内码
            //Chi tiết quyết toán ngoại trú_ trả lại (theo jsnm mz_jsmx)
            List<OutpatientSettlementDetailEntity> mzJsmxList = _outPatientSettleDetailRepo.SelectMzjsmxByJsnm(mzJs.jsnm, OperatorProvider.GetCurrent().OrganizeId);
            List<OutpatientSettlementPaymentModelEntity> mzJszffsList = _outPatientSettlePayMethodRepo.SelectMzjszffsByJsnm(mzJs.jsnm, OperatorProvider.GetCurrent().OrganizeId);
            OutpatientRegistEntity registEntity = _outpatientRegRepo.SelectOutPatientReg(mzJs.ghnm, OperatorProvider.GetCurrent().OrganizeId);
            #region 新增系统病人帐户收支记录 注意:只有账户支付，才会有账户收支记录
            #endregion

            // theo jsnm tra cứu quyết toán ngoại trú loại lớn 
            List<OutpatientSettlementCategoryEntity> jsdlList = _outPatientSettleCategRepo.SelectMzjsdlByJsnm(mzJs.jsnm, OperatorProvider.GetCurrent().OrganizeId);
            // cũ quyết toán là trả lại mới của hủy bỏ mã nội bộ quyết toán
            mzJs.cxjsnm = mzJs.jsnm;

            #region 插入全退
            try
            {
                using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
                {
                    //update mz_gh bảng của ghzt
                    if (registEntity != null)
                    {
                        registEntity.ghzt = "2";   //0 chờ quyết toán 1 Đ ã quyết toán 2 Đ ã hoàn
                        registEntity.Modify();
                        db.Update(registEntity);
                    }
                    //Chènmz_js
                    mzJs.jsnm = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt(OutpatientSettlementEntity.GetTableName());
                    mzJs.OrganizeId = OperatorProvider.GetCurrent().OrganizeId;
                    mzJs.Create();
                    db.Insert(mzJs);
                    //Chènmz_jsmx
                    foreach (var jsmx in mzJsmxList)
                    {
                        jsmx.jsmxnm = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt(OutpatientSettlementDetailEntity.GetTableName());
                        jsmx.jsnm = mzJs.jsnm;
                        jsmx.OrganizeId = OperatorProvider.GetCurrent().OrganizeId;
                        jsmx.Create();
                        db.Insert(jsmx);
                    }
                    //Phư ơng thức thanh toán quyết toán ngoại trú bản ghi 
                    foreach (var jszffs in mzJszffsList)
                    {
                        jszffs.mzjszffsbh = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt(OutpatientSettlementPaymentModelEntity.GetTableName());
                        jszffs.jsnm = mzJs.jsnm;
                        jszffs.ssrq = DateTime.Now;
                        jszffs.OrganizeId = OperatorProvider.GetCurrent().OrganizeId;
                        jszffs.Create();
                        db.Insert(jszffs);
                    }
                    //Quyết toán ngoại trú loại lớn 
                    foreach (var jsdlItem in jsdlList)
                    {
                        jsdlItem.jsnm = mzJs.jsnm;
                        jsdlItem.jsrq = DateTime.Now;
                        jsdlItem.OrganizeId = OperatorProvider.GetCurrent().OrganizeId;
                        jsdlItem.Create(true);  // tự 增string khóa chính
                        db.Insert(jsdlItem);
                    }
                    //Ngoại trú trả lại số 
                    foreach (var thItem in backNumList)
                    {
                        thItem.OrganizeId = OperatorProvider.GetCurrent().OrganizeId;
                        thItem.Create();
                        db.Insert(thItem);
                    }
                    db.Commit();
                    result = true;
                }

            }
            catch (Exception e)
            {
                result = false;
                throw;
            }

            #endregion
            #endregion
            return result;
        }

        /// <summary>
        /// check bệnh nhân có hay không có chờ quyết toánchi phí đăng ký khám bản ghi (không Quận分 một lần Đ ăng ký khám)
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="patid"></param>
        /// <returns></returns>
        public bool CheckHasUnsettedRegister(string orgId, int patid)
        {
            //Đ ăng ký khám chờ quyết toán trạng thái
            var sql = @"select '1' from mz_gh(nolock) where patid = @patid and OrganizeId = @orgId and zt = '1' and ghzt = '0'  --不判断ghzt null";
            var exist = this.FirstOrDefault<string>(sql, new[] { new SqlParameter("@patid", patid), new SqlParameter("@orgId", orgId) });
            // tại đ ây phán đoán 
            return exist == "1";
        }

        /// <summary>
        /// check bệnh nhân có hay không có chưa结chi phí bản ghi (không Quận分 một lần Đ ăng ký khám)
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="patid"></param>
        /// <returns></returns>
        public bool CheckHasUnsetted(string orgId, int patid)
        {
            //Chờ quyết toán
            var sql = @"select '1' from mz_xm(nolock) where patid = @patid and OrganizeId = @orgId and zt = '1' and xmzt = '0'  --不判断xmzt null
union
select '1' from mz_cf(nolock)  where patid = @patid and OrganizeId = @orgId and zt = '1' and cfzt = '0'";
            var exist = this.FirstOrDefault<string>(sql, new[] { new SqlParameter("@patid", patid), new SqlParameter("@orgId", orgId) });
            // tại đ ây phán đoán 
            return exist == "1";
        }

        /// <summary>
        /// Phư ơng thức thanh toán quyết toán ngoại trú
        /// </summary>
        /// <param name="jsnm">Mã nội bộ quyết toán</param>
        /// <returns></returns>
        public List<OutPatientSettlePayMethodVO> GetMzJszffs(int? jsnm)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
    		              SELECT ROW_NUMBER()OVER(ORDER BY XJZFFSMC) AS NUM, XJZFFSMC,ZFJE,ZH FROM MZ_JSZFFS
		                  LEFT JOIN XT_XJZFFS ON MZ_JSZFFS.XJZFFS = XT_XJZFFS.XJZFFS
		                  WHERE XT_XJZFFS.ZT = '1' AND MZ_JSZFFS.JSNM = @jsnm and MZ_JSZFFS.OrganizeId=@OrganizeId
                         ");
            SqlParameter[] param =
                {
                    new SqlParameter("@jsnm",jsnm),
                    new SqlParameter("@OrganizeId",OperatorProvider.GetCurrent().OrganizeId)
                };
            return this.FindList<OutPatientSettlePayMethodVO>(strSql.ToString(), param);
        }

        #region 保存

        /// <summary>
        /// Đ ăng ký khám ngoại trú lưu
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="patid"></param>
        /// <param name="kh"></param>
        /// <param name="ghly"></param>
        /// <param name="mjzbz"></param>
        /// <param name="ks"></param>
        /// <param name="ys"></param>
        /// <param name="ksmc"></param>
        /// <param name="ysmc"></param>
        /// <param name="ghxm"></param>
        /// <param name="zlxm"></param>
        /// <param name="fph"></param>
        /// <param name="sfrq"></param>
        /// <param name="isCkf"></param>
        /// <param name="isGbf"></param>
        /// <param name="jzxh"></param>
        /// <param name="ghpbId"></param>
        /// <param name="feeRelated"></param>
        /// <param name="brxz">Tính chất bệnh nhân由trang truyền qua 来,因là BHYTbệnh nhân có thể挂tự trả</param>
        /// <param name="ybjsh">BHYT端số quyết toán</param>
        /// <param name="mzh">Số khám ngoại trú</param>
        /// <param name="mzyyghid">Đ ặt lịch đ ăng ký khám ngoại trú ID Tư ơng ứngNewtouch_CIS.dbo.mz_yygh.Id</param>
        /// <returns></returns>
        public void Save(string orgId, int patid, string kh, string ghly, string mjzbz,
            string ks, string ys, string ksmc, string ysmc, string ghxm, string zlxm
            , string fph, DateTime? sfrq, bool isCkf, bool isGbf
            , int jzxh
            , int ghpbId
            , OutpatientSettFeeRelatedDTO feeRelated, string brxz
            , string ybjsh, string mzh
            , string jzyy, string jzid, string jzlx, string bzbm, string bzmc, string mzyyghId,string isjm, out object newJszbInfo)
        {
            newJszbInfo = null;
            //Số khám ngoại trú检重
            if (_outpatientRegRepo.IQueryable(p => p.mzh == mzh).Any())
            {
                throw new FailedException("门诊号重复");
            }
            // cần quay lại(作là hóa đ ơn in của入参)
            int jsnm = 0;
            // theo patid lấy thông tin cơ bản bệnh nhân thực thể
            var brjbxxEntity = _sysPatBasicInfoRepository.GetInfoByPatid(patid.ToString(), orgId);
            if (brjbxxEntity == null)
            {
                throw new FailedCodeException("OUTPAT_NO_PATIENT_INFORMATION");
            }
            // xây dựng mz_gh Entity
            var gh = new OutpatientRegistEntity
            {
                ghnm = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt(OutpatientRegistEntity.GetTableName()),
                ybjsh = ybjsh,
                jzyy = jzyy,
                OrganizeId = orgId,
                ghly = ghly,
                mjzbz = mjzbz,
                ks = ks,
                ys = ys,
                jzbz = ((int)EnumOutpatientJzbz.Djz).ToString(),
                jzxh = (short)jzxh,
                yyghId = mzyyghId,
                //Số khám ngoại trú
                mzh = mzh,
                ghlx = ghxm,
                // cần loại trừ Đ ã hoàn của
                fzbz = _outpatientRegRepo.IQueryable(p => p.patid == patid && p.zt == "1" && p.ghzt != "2").Any() ? (byte)1 : (byte)0,
                kh = kh,
                ScheduId = Convert.ToDecimal(ghpbId),
                jzid=jzid,
                jzpzlx=jzlx,
                bzbm=bzbm,
                bzmc=bzmc,
                ecToken= feeRelated.ecToken
            };
            var cardEntity = _sysCardRepo.GetCardEntity(gh.kh, brjbxxEntity.OrganizeId);
            gh.CardType = cardEntity != null ? cardEntity.CardType : "";
            gh.CardTypeName = cardEntity != null ? cardEntity.CardTypeName : "";
            gh.ghrq = DateTime.Now;
            gh.patid = patid;
            gh.brxz = brxz;
            AssembleGhInfo(brjbxxEntity, gh);
            //LấyĐ ăng ký khám, chẩn đoán và điều trị hạng mụclist
            GHZGroupAndFeesVO ghFeeResult;
            //Có thể quay lạiCount=0 挂 trống số 
            var xmList = GetXmList(gh, isCkf, isGbf, out ghFeeResult, ghxm, zlxm, orgId);
            gh.ghf = ghFeeResult.ghfPrice;
            gh.zlf = ghFeeResult.zlfPrice;
            gh.ckf = ghFeeResult.ckfPrice;
            gh.gbf = ghFeeResult.gbfPrice;
            //notSett true tạm thời không 结(ngoại trú thu phí giờ cùng nhau 结),nếu không结
            bool? notSett = null;
	        var medicalInsurance = _sysConfigRepo.GetValueByCode("Outpatient_MedicalInsurance", orgId);
			if (brxz != "0" && gh.CardType == ((int)EnumCardType.YBJYK).ToString())
            {
                // không phải tự trả(không cần BHYT thẻ) và nhiều 加 một lớp phán đoán dùng BHYT thẻ 
                notSett = (_sysConfigRepo.GetBoolValueByCode("Outpatient_ChargeFee_OpenYbSett", orgId) ?? false) && (_sysConfigRepo.GetBoolValueByCode("Outpatient_ChargeFee_yb_SettOnce", orgId) ?? false);
            }
            if (!(notSett == true))
            {
                //贵安 Đ ăng ký khám ngoại trú không quyết toán
                
	            if (medicalInsurance == "guian")
				{
					notSett = true;
				}
	            if (medicalInsurance == "chongqing")
	            {
		            notSett = false;
	            }
			}
            if (notSett == true)
            {
                //Chờ quyết toánTrạng thái
                gh.ghzt = "0";
            }
            else
            {
                //Đ ã quyết toán
                gh.ghzt = "1";
                gh.jsrq = DateTime.Now;
            }
            using (var db = new EFDbTransaction(_databaseFactory).BeginTrans())
            {
                gh.Create();
				#region 照顾自助机接口，获取不到登录用户
				if (string.IsNullOrEmpty(gh.CreatorCode))
				{
					gh.CreatorCode = "zzjadmin";//Nếu trống,Đ ặt thành đ ăng ký khám tại máy tự phục vụ(giao diện)
				}
				if (string.IsNullOrEmpty(gh.CreateTime.ToString()))
				{
					gh.CreateTime = DateTime.Now;
				}
				#endregion
				db.Insert(gh);

                if (xmList != null && xmList.Count > 0)
                {
                    if (gh.ghzt == "0")
                    {
                        //ghzt chờ quyết toán //Có tính phí hạng mục,nhưng này giờ không quyết toán,作là ngoại trú thu phí giờ 带入 của 
                        foreach (var xmItem in xmList)
                        {
                            xmItem.ghnm = gh.ghnm;
                            xmItem.xmzt = "0";  //mz_xmchi tiết nơi 于chờ quyết toán trạng thái
                            xmItem.ys = ys;
                            xmItem.ks = ks;
                            xmItem.ysmc = ysmc;
                            xmItem.ksmc = ksmc;
                            xmItem.Create();
							#region 照顾自助机接口，获取不到登录用户
							if (string.IsNullOrEmpty(xmItem.CreatorCode))
							{
								xmItem.CreatorCode = "zzjadmin";//Nếu trống,Đ ặt thành đ ăng ký khám tại máy tự phục vụ(giao diện)
							}
							if (string.IsNullOrEmpty(xmItem.CreateTime.ToString()))
							{
								xmItem.CreateTime = DateTime.Now;
							}
							#endregion
							db.Insert(xmItem);
                        }
                    }
                    else
                    {
                        jsnm = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt(OutpatientSettlementEntity.GetTableName());
                        var jsrq = DateTime.Now;
                        //Đ ăng ký khámthu phí
                        var jslx = ((int)EnumJslx.GH).ToString();

                        //Tổng số tiền
                        decimal mxzje = 0;
                        foreach (var xmItem in xmList)
                        {
                            mxzje += xmItem.je;
                            xmItem.ghnm = gh.ghnm;
                            xmItem.sfrq = sfrq;
                            xmItem.ys = ys;
                            xmItem.ks = ks;
                            xmItem.ysmc = ysmc;
                            xmItem.ksmc = ksmc;
                            xmItem.Create();
                            #region 照顾自助机接口，获取不到登录用户
                            if (string.IsNullOrEmpty(xmItem.CreatorCode))
                            {
                                xmItem.CreatorCode = "zzjadmin";//Nếu trống,Đ ặt thành đ ăng ký khám tại máy tự phục vụ(giao diện)
                            }
                            if (string.IsNullOrEmpty(xmItem.CreateTime.ToString()))
                            {
                                xmItem.CreateTime = DateTime.Now;
                            }
                            #endregion
                            db.Insert(xmItem);

                            //Chi tiết quyết toán ngoại trú
                            var jsmxEntity = new OutpatientSettlementDetailEntity
                            {
                                jsmxnm = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt(OutpatientSettlementDetailEntity.GetTableName()),
                                jsnm = jsnm,
                                mxnm = xmItem.xmnm,
                                sl = xmItem.sl,
                                jslx = jslx,
                                jyje = xmItem.je,
                                OrganizeId = orgId
                            };
                            //Chi tiết đ ơn thuốc mã nội bộ
                            jsmxEntity.Create();
                            #region 照顾自助机接口，获取不到登录用户
                            if (string.IsNullOrEmpty(jsmxEntity.CreatorCode))
                            {
                                jsmxEntity.CreatorCode = "zzjadmin";//Nếu trống,Đ ặt thành đ ăng ký khám tại máy tự phục vụ(giao diện)
                            }
                            if (string.IsNullOrEmpty(jsmxEntity.CreateTime.ToString()))
                            {
                                jsmxEntity.CreateTime = DateTime.Now;
                            }
                            #endregion
                            db.Insert(jsmxEntity);
                        }
                        //
                        if (mxzje != feeRelated.zje)
                        {
                            throw new Exception("结算总金额异常" + mxzje.ToString() + "不等于" + feeRelated.zje.ToString());
                        }
                        //Quyết toán ngoại trú bảng chính 
                        var jszbEntity = new OutpatientSettlementEntity
                        {
                            jsnm = jsnm,
                            OrganizeId = orgId,
                            patid = gh.patid,
                            brxz = gh.brxz,
                            jslx = jslx,
                            fph = fph,
                            jszt = (int)EnumJieSuanZT.YJ,
                            ghnm = gh.ghnm,
                            xm = gh.xm,
                            xb = gh.xb,
                            csny = gh.csny,
                            blh = gh.blh,
                            zjh = gh.zjh,
                            zjlx = gh.zjlx,
                            jzsj = sfrq,
                            jmbl = feeRelated.zkbl ?? 0,
                            jmje = feeRelated.zkje ?? 0,
                            xjzf = feeRelated.xjzfys ?? 0,
                            zje = mxzje,
                            xjzffs= feeRelated.zffs1,
                            //Ghi sổ một phần V.v.
                            ysk = feeRelated.ssk,
                            zl = feeRelated.zhaoling,
                            ybjslsh=feeRelated.ybjslsh,
                            isjm = isjm
                        };
                        jszbEntity.Create();
                        #region 照顾自助机接口，获取不到登录用户
                        if (string.IsNullOrEmpty(jszbEntity.CreatorCode))
                        {
                            jszbEntity.CreatorCode = "zzjadmin";//Nếu trống,Đ ặt thành đ ăng ký khám tại máy tự phục vụ(giao diện)
                        }
                        if (string.IsNullOrEmpty(jszbEntity.CreateTime.ToString()))
                        {
                            jszbEntity.CreateTime = DateTime.Now;
                        }
                        #endregion
                        db.Insert(jszbEntity);
                        if (medicalInsurance == "chongqing")
                        {
                            var S23Entity =
                                _iCqybSett23Repo.FindEntity(p => p.dyjylsh == feeRelated.dyjylsh && p.zt == "1");
                            if (S23Entity != null)
                            {
                                S23Entity.jsnm = jszbEntity.jsnm;
                                S23Entity.Modify();
                                db.Update(S23Entity);
                            }
                        }
                        UpdateCurrentFinancialInvoice(db, orgId, fph);
						if (medicalInsurance != "chongqing") // bên trong mã觉得có问题,恒 sẽ báo lỗi,tạm thời跳 qua lixin 20191216 Trùng Khánh BHYT
						{
							//if (feeRelated.orglxjzfys.HasValue && feeRelated.xjzfys.HasValue)
							//{
							//	// cần 减去ghi sổ một phần V.v.Số tiền
							//	if (feeRelated.orglxjzfys.Value != jszbEntity.zje - 0)
							//	{
							//		throw new FailedException("ERROR_SETT_JE_ERROR", "Số tiền quyết toán bất thư ờng" + feeRelated.orglxjzfys.Value.ToString() + " không V.v.于" + jszbEntity.zje.ToString());
							//	}
							//	//orglxjzfys xjzfys zkbl zkje cần 保持 nhất quán 性
							//	var zkje = feeRelated.orglxjzfys.Value * Math.Abs(feeRelated.zkbl ?? 0) + Math.Abs(feeRelated.zkje ?? 0);
							//	if (Math.Abs(zkje + feeRelated.xjzfys.Value - feeRelated.orglxjzfys.Value) > (decimal)0.01)
							//	{
							//		throw new FailedException("ERROR_ZKJE_ZFJE_ERROR", "折扣số tiền bất thư ờng");
							//	}
							//}
							//else
							//{
							//	// cần 减去ghi sổ một phần V.v.Số tiền
							//	jszbEntity.xjzf = jszbEntity.zje - 0;
							//}
						}
                        

                        if (feeRelated.ssk.HasValue && feeRelated.zhaoling.HasValue)
                        {
                            jszbEntity.xjwc =(decimal)feeRelated.xjwc;
                            //Sai lệch tiền mặt nhận đ ược của-phải thu
                            //jszbEntity.xjwc = jszbEntity.xjzf - (feeRelated.ssk.Value - feeRelated.zhaoling.Value);
                            //if (Math.Abs(jszbEntity.xjwc) >= (decimal)0.1)
                            //{
                            // throw new FailedException("ERROR_SSK_ZHAOLING", " thực thu trả lại tiền thừa số tiền bất thư ờng");
                            //}
                        }

                        PaymentModelAccountReserveII(db, jszbEntity, feeRelated);
                    }
                }
                //else nếu không không có chi phí chi tiết không 必 cần tạo quyết toán

                //Thêm mới phí dịch vụ hạng mục(hủy)
               var result = db.Commit();
                // SiteCISAPIHelper.KeepAnAppointment(mzyyghId, DateTime.Now, gh.CreatorCode);
            }

            newJszbInfo = new
            {
                mzh = mzh,

                jsnm = jsnm,
                ybjsh = ybjsh,
                jszje = feeRelated.zje,
                jsxjzf = feeRelated.xjzfys,
            };
        }

        /// <summary>
        /// Tạo duy nhất số khám ngoại trú, số thứ tự khám bệnh
        /// </summary>
        /// <param name="patid"></param>
        /// <param name="ghlx"></param>
        /// <param name="ks"></param>
        /// <param name="ys"></param>
        /// <returns></returns>
        public Tuple<short, string> GetMzhJzxh(int patid, string ghpbId, string ks, string ys,string OrganizeId,string UserCode)
        {
            //Xác minh tính chất bệnh nhân có hay không áp dụng cho ngoại trú
            //CheckBrxz(gh.kh, updateBrxz);

            if (patid <= 0 || string.IsNullOrWhiteSpace(ghpbId) || string.IsNullOrWhiteSpace(ks))
            {
                throw new FailedException("数据不完整");
            }
            //Có hay không không có视có hay không tồn tại tại chưa结,có thể trùng lặp Đ ăng ký khám
            var regMulti = _sysConfigRepo.GetBoolValueByCode("Outpatient_Register_UnSetted_Multi", OrganizeId);
            if (!(regMulti == true))
            {
                //Check có hay không có chưa结Đ ăng ký khám //?Đ ăng ký khám giờ đồng thời cần 挂 hai 个 số như thế nào 办
                var hasUnsettedReg = CheckHasUnsettedRegister(OrganizeId, patid);
                if (hasUnsettedReg)
                {
                    throw new FailedException("存在历史未结挂号，不能新挂号");
                }
                //Check có hay không có chưa结của lịch sử,có thì không 允许挂 mới số 
                var hasUnsetted = CheckHasUnsetted(OrganizeId, patid);
                if (hasUnsetted)
                {
                    throw new FailedException("存在历史未结，不能新挂号");
                }
            }
            //Lấykhi ngày, khi trư ớc lịch trực số thứ tự khám bệnh
            //20180930由 truyền ghxm đ ổi là truyền ghpbid ghpbid duy nhất 
            //Số thứ tự khám bệnh
            var jzxh = _outPatientDmnService.GetJzxh(ghpbId, ks, ys, "", UserCode, OrganizeId);
            //Số khám ngoại trú
            var ghpbIdStr = ghpbId.ToString().PadLeft(3, '0');
            if (ghpbIdStr.Length > 3)
            {
                //维护至超 qua 999 của lịch trực giờ sẽ có trùng lặp 漏洞
                ghpbIdStr = ghpbIdStr.Substring(ghpbIdStr.Length - 3, 3);
            }
            var mzh = DateTime.Now.ToString("yyMMdd") + ghpbIdStr + jzxh.ToString().PadLeft(4, '0');
            return Tuple.Create(jzxh, mzh);
        }

        /// <summary>
        /// theo Đ ã知jzxh Đ ăng ký khám ngày 得số khám ngoại trú
        /// </summary>
        /// <param name="patid"></param>
        /// <param name="ghpbId"></param>
        /// <param name="ks"></param>
        /// <param name="OrganizeId"></param>
        /// <param name="UserCode"></param>
        /// <param name="jzxh"></param>
        /// <param name="jzrq"></param>
        /// <returns></returns>
        public string GetRegMzh(int patid, string ghpbId, string ks, string OrganizeId, string UserCode,int jzxh,string jzrq)
        {
            //Xác minh tính chất bệnh nhân có hay không áp dụng cho ngoại trú
            //CheckBrxz(gh.kh, updateBrxz);

            if (patid <= 0 || string.IsNullOrWhiteSpace(ghpbId) || string.IsNullOrWhiteSpace(ks))
            {
                throw new FailedException("数据不完整");
            }
            //Có hay không không có视có hay không tồn tại tại chưa结,có thể trùng lặp Đ ăng ký khám
            var regMulti = _sysConfigRepo.GetBoolValueByCode("Outpatient_Register_UnSetted_Multi", OrganizeId);
            if (!(regMulti == true))
            {
                //Check có hay không có chưa结Đ ăng ký khám //?Đ ăng ký khám giờ đồng thời cần 挂 hai 个 số như thế nào 办
                var hasUnsettedReg = CheckHasUnsettedRegister(OrganizeId, patid);
                if (hasUnsettedReg)
                {
                    throw new FailedException("存在历史未结挂号，不能新挂号");
                }
                //Check có hay không có chưa结của lịch sử,có thì không 允许挂 mới số 
                var hasUnsetted = CheckHasUnsetted(OrganizeId, patid);
                if (hasUnsetted)
                {
                    throw new FailedException("存在历史未结，不能新挂号");
                }
            }
            //Lấykhi ngày, khi trư ớc lịch trực số thứ tự khám bệnh
            //20180930由 truyền ghxm đ ổi là truyền ghpbid ghpbid duy nhất 
            //Số thứ tự khám bệnh
            //var jzxh = _outPatientDmnService.GetJzxh(ghpbId, ks, ys, "", UserCode, OrganizeId);
            //Số khám ngoại trú
            var ghpbIdStr = ghpbId.ToString().PadLeft(3, '0');
            if (ghpbIdStr.Length > 3)
            {
                //维护至超 qua 999 của lịch trực giờ sẽ có trùng lặp 漏洞
                ghpbIdStr = ghpbIdStr.Substring(ghpbIdStr.Length - 3, 3);
            }

            var ghrq = Convert.ToDateTime(jzrq).ToString("yyMMdd");
            var mzh = ghrq + ghpbIdStr + jzxh.ToString().PadLeft(4, '0');
            return mzh;
        }
        /// <summary>
        /// chongqing mới Đ ăng ký khám lịch trực tạo duy nhất số khám ngoại trú số thứ tự khám bệnh
        /// </summary>
        /// <param name="patid"></param>
        /// <param name="ghpbId"></param>
        /// <param name="ks"></param>
        /// <param name="ys"></param>
        /// <param name="OrganizeId"></param>
        /// <param name="UserCode"></param>
        /// <param name="mjzbz"></param>
        /// <returns></returns>
        public Tuple<short, string> GetCQMzhJzxh(int patid, string ghpbId, string ks, string ys, string OrganizeId, string UserCode,string mjzbz,string QueueNo,string OutDate)
        {
            short jzxh=0;
            string mzh="";
            // này bệnh nhân có hay kh ôngĐ ãĐ ặt lịch đ ăng ký khám
            //var isAppointment = _outPatientDmnService.GetIsMzghBookSchedule(mjzbz, patid.ToString(),OrganizeId);
            if (!string.IsNullOrWhiteSpace(QueueNo))
            {
                //采 dùng đ ặt lịch số thứ tự khám bệnh
                jzxh = Convert.ToInt16(QueueNo);
                mzh = GetRegMzh(patid, ghpbId, ks, OrganizeId, UserCode, jzxh, OutDate);
            }
            else
            {
                var mzhjzxh = GetMzhJzxh(patid, ghpbId.ToString(), ks, ys, OrganizeId, UserCode);
                jzxh = mzhjzxh.Item1;
                mzh = GetRegMzh(patid, ghpbId, ks, OrganizeId, UserCode, jzxh, DateTime.Now.ToString());
            }
            
            return Tuple.Create(jzxh, mzh);
        }

        /// <summary>
        /// nhóm 装Đ ăng ký khám信thông tin, dùng cơ bản thông tin gán giá trị Đ ăng ký khám ngoại trú
        /// </summary>
        /// <param name="brjbxx"></param>
        /// <param name="gh"></param>
        private void AssembleGhInfo(SysPatientBasicInfoEntity brjbxx, OutpatientRegistEntity gh)
        {
            gh.xm = brjbxx.xm;
            gh.xb = brjbxx.xb;
            gh.csny = brjbxx.csny;
            if (gh.csny.HasValue)
            {
                var nage = DateTimeHelper.getAgeFromBirthTime(gh.csny);
                gh.nlshow = nage.text;
            }
            gh.blh = brjbxx.blh;
            gh.lxr = brjbxx.jjllr;
            gh.lxrgx = brjbxx.jjllrgx;
            gh.lxrdh = brjbxx.jjlldh;
            gh.zjlx = brjbxx.zjlx;
            gh.zjh = brjbxx.zjh;
        }

        #region 订单操作

        /// <summary>
        /// 预定
        /// </summary>
        /// <param name="gh"></param>
        /// <param name="ssk"></param>
        /// <param name="fph"></param>
        /// <param name="isCkf"></param>
        /// <param name="isGbf"></param>
        public long Book(OutpatientRegistEntity gh, decimal ssk, string fph, bool isCkf, bool isGbf)
        {
            //LấyĐ ăng ký khám hạng mụclist
            //var ghxmList = GetGhxmList(gh, isCkf, isGbf);
            //if (ghxmList == null || ghxmList.Count == 0)
            //{
            //    throw new FailedCodeException("OUTPAT_BILLING_ITEM_CAN_NOT_BE_EMPTY");
            //}
            GHZGroupAndFeesVO ghFeeResult;
            var xmList = GetXmList(gh, isCkf, isGbf, out ghFeeResult, gh.ghlx, null);
            if (xmList == null || xmList.Count == 0)
            {
                throw new FailedCodeException("OUTPAT_BILLING_ITEM_CAN_NOT_BE_EMPTY");
            }
            //Lấy thông tin bệnh nhân theo mã nội bộ bệnh nhân
            var brjbxx = _sysPatBasicInfoRepository.GetInfoByPatid(gh.patid.ToString(), OperatorProvider.GetCurrent().OrganizeId);
            //Lưu bản ghi nhật ký thay đ ổi cũ
            if (brjbxx == null)
            {
                throw new FailedCodeException("OUTPAT_NO_PATIENT_INFORMATION");
            }


            //Đ ăng ký khám ngoại trú bảng //规thì lỗi
            gh.mzh = DateTime.Now.ToString("yyyyMMdd") + gh.jzxh.ToString().PadLeft(5, '0');  //Số khám ngoại trú

            gh.zjlx = brjbxx.zjlx;
            gh.zjh = brjbxx.zjh;
            gh.OrganizeId = OperatorProvider.GetCurrent().OrganizeId;
            gh.Create();

            //mz_js Quyết toán ngoại trú
            var mzjs = new OutpatientSettlementEntity
            {
                patid = gh.patid,
                brxz = gh.brxz,
                jslx = ((int)EnumJslx.GH).ToString(),
                flzffy = 0,
                jzfy = 0,
                xjzf = ssk,
                xjwc = 0,     //Sai lệch tiền mặt là指？
                zhjz = 0,
                xjzffs = "0", //Phư ơng thức thanh toán tiền mặt:A giư ờng gia đ ình tài khoản 0 tiền mặt 1 POS 2 ngân hàng chuyển 账 3 预交tài khoản
                fph = fph,
                jszt = (int)EnumJieSuanZT.YJ,    //1 Đ ã quyết toán(chưa hoàn) 2 Đ ã hoàn 3 补收 4 补收又 trả lại 
                cxjsnm = 0,   // hủy bỏ mã nội bộ quyết toán
                zh = 0,
                fpdm = "",    // hóa đ ơn mã
                jmbl = 0,
                jmje = 0,
                jylx = "1",    // giao dịch loại 1 Đ ăng ký khám ngoại trú
                jch = 0,       //加床 số 
                ghnm = gh.ghnm,
                xm = gh.xm,
                xb = gh.xb,
                csny = gh.csny,
                blh = gh.blh,
                zjlx = gh.zjlx,
                zjh = gh.zjh,
                OrganizeId = OperatorProvider.GetCurrent().OrganizeId
            };
            //往Đ ăng ký khám hạng mục bảng 插 bản ghi 
            foreach (var ghxmItem in xmList)
            {
                mzjs.zje += ghxmItem.je;
                ghxmItem.ghnm = gh.ghnm;
                ghxmItem.Create();
            }
            mzjs.zffy = mzjs.zje;
            mzjs.ysk = mzjs.zje;
            mzjs.zl = ssk - mzjs.zje;
            mzjs.Create();

            return OrderBookProcess(gh, mzjs, xmList);
        }

        /// <summary>
        /// đ ơn hàng 预定xử lý
        /// </summary>
        /// <param name="gh"></param>
        /// <param name="mzjs"></param>
        /// <param name="ghxmList"></param>
        private static long OrderBookProcess(OutpatientRegistEntity gh, OutpatientSettlementEntity mzjs, List<OutpatientItemEntity> ghxmList)
        {
            var result = Proxy.CreateOrderProxy.Instance.Book(gh, mzjs, ghxmList);
            if (result != null && result.ResponseStatus.IsSucceed)
            {
                return result.OrderId;
            }
            return 0;
        }

        /// <summary>
        /// Gửi正 kiểu đ ơn thao tác
        /// </summary>
        /// <param name="gh"></param>
        private static void OrderCommitProcess(OutpatientRegistEntity gh)
        {
            Proxy.CreateOrderProxy.Instance.Commit(gh.OrderId);
        }

        #endregion

        /// <summary>
        /// LấyĐ ăng ký khám hạng mục
        /// </summary>
        /// <param name="gh">Thông tin đ ăng ký khám</param>
        /// <param name="isCkf">Phí thẻ từ</param>
        /// <param name="isGbf">Phí làm thẻ</param>
        /// <returns></returns>
        public List<OutpatientRegistItemEntity> GetGhxmList(OutpatientRegistEntity gh, bool isCkf, bool isGbf)
        {
            var sfxmList = GetOutpatientFees(gh.ghlx, "", isCkf, isGbf).sfxmList;
            //Đ ăng ký khám hạng mục
            var ghxmList = new List<OutpatientRegistItemEntity>();
            foreach (var pair in sfxmList)
            {
                var sfxm = pair.Value;
                var ghxm = new OutpatientRegistItemEntity
                {
                    patid = gh.patid,
                    brxz = gh.brxz,
                    ys = gh.ys,
                    ks = gh.ks,
                    sfxm = sfxm.sfxmCode,
                    dl = sfxm.sfdlCode,
                    sl = 1.0m,
                    zfbl = sfxm.zfbl,
                    zfxz = sfxm.zfxz,
                    dj = sfxm.dj,
                    fwfdj = 0
                };

                //ghxm.sfxmbh = sfxm.sfxmbh;
                // phí dịch vụ Đ ơn giá tự trả tạm thời 写
                ghxm.je = (sfxm.dj + ghxm.fwfdj) * ghxm.sl;
                ghxm.jmbl = 0;  //Tỷ lệ giảm trừ 
                ghxm.jmje = 0;  //Số tiền giảm trừ

                ghxmList.Add(ghxm);
            }
            return ghxmList;
        }

        /// <summary>
        /// LấyĐ ăng ký khám hạng mục
        /// </summary>
        /// <param name="gh">Thông tin đ ăng ký khám</param>
        /// <param name="isCkf">Phí thẻ từ</param>
        /// <param name="isGbf">Phí làm thẻ</param>
        /// <returns></returns>
        public List<OutpatientItemEntity> GetXmList(OutpatientRegistEntity gh, bool isCkf, bool isGbf, out GHZGroupAndFeesVO ghFeeResult, string ghxm, string zlxm,string orgId=null)
        {
            //Đ ăng ký khám hạng mục
            var xmList = new List<OutpatientItemEntity>();
            Dictionary<string, SysChargeItemVEntity> sfxmList = new Dictionary<string, SysChargeItemVEntity>();
            Dictionary<string, List<SysChargeItemVEntity>> sfxmzhList = new Dictionary<string, List<SysChargeItemVEntity>>();
            string zlxmzhconf = _sysConfigRepo.GetValueByCode("EnableZlxmGroup", orgId);
            if (!string.IsNullOrWhiteSpace(zlxmzhconf) && zlxmzhconf == "1")
            {
                ghFeeResult = GetOutpatientFeesbyGroup(ghxm, zlxm, isCkf, isGbf, orgId);
                sfxmzhList = ghFeeResult.sfxmzhList;
                foreach (var pair in sfxmzhList)
                {
                    var sfxm = pair.Value;
                    foreach (var item in pair.Value)
                    {
                        var xm = new OutpatientItemEntity
                        {
                            xmnm = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt(OutpatientItemEntity.GetTableName()),
                            ghnm = gh.ghnm,
                            patid = gh.patid,
                            brxz = gh.brxz,
                            mjzbz = gh.mjzbz,
                            xmzt = "1", //Đ ã quyết toán
                                        //ssbz = "1",
                                        //ssry = 
                                        //ssrq = DateTime.Now,
                                        //jsnm = 0,
                                        //jsrq = DateTime.Now,
                            ys = gh.ys,
                            ks = gh.ks,
                            sfxm = item.sfxmCode,
                            dl = item.sfdlCode,
                            sl = 1.0m,
                            dw = item.dw,
                            zfbl = item.zfbl,
                            zfxz = item.zfxz,
                            dj = item.dj,
                            zt = "1",
                            jmbl = 0,  //Tỷ lệ giảm trừ 
                            jmje = 0,  //Số tiền giảm trừ
                            OrganizeId = gh.OrganizeId,
                        };
                        xm.je = item.dj * xm.sl;
                        xmList.Add(xm);
                    }

                }
            }
            else
            {
                ghFeeResult = GetOutpatientFees(ghxm, zlxm, isCkf, isGbf, orgId);
                sfxmList = ghFeeResult.sfxmList;
                foreach (var pair in sfxmList)
                {
                    var sfxm = pair.Value;
                    var xm = new OutpatientItemEntity
                    {
                        xmnm = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt(OutpatientItemEntity.GetTableName()),
                        ghnm = gh.ghnm,
                        patid = gh.patid,
                        brxz = gh.brxz,
                        mjzbz = gh.mjzbz,
                        xmzt = "1", //Đ ã quyết toán
                                    //ssbz = "1",
                                    //ssry = 
                                    //ssrq = DateTime.Now,
                                    //jsnm = 0,
                                    //jsrq = DateTime.Now,
                        ys = gh.ys,
                        ks = gh.ks,
                        sfxm = sfxm.sfxmCode,
                        dl = sfxm.sfdlCode,
                        sl = 1.0m,
                        dw = sfxm.dw,
                        zfbl = sfxm.zfbl,
                        zfxz = sfxm.zfxz,
                        dj = sfxm.dj,
                        zt = "1",
                        jmbl = 0,  //Tỷ lệ giảm trừ 
                        jmje = 0,  //Số tiền giảm trừ
                        OrganizeId = gh.OrganizeId,
                    };
                    xm.je = sfxm.dj * xm.sl;
                    xmList.Add(xm);
                }
            }                
            
            
            
            return xmList;
        }
        /// <summary>
        /// Lấykhoa Tư ơng ứng医师số giấy tờ
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="ks"></param>
        /// <returns></returns>
        public CqybGjbmInfoVo GetDepartmentDoctorIdC(string orgId, string ks,string ys)
        {
            CqybGjbmInfoVo vo = new CqybGjbmInfoVo();
            string sql = "";
            if (ys!=null)
            {
                sql = @"select zjh,gjybdm,b.ybksbm,a.gh,a.name,b.Name ksmc
 from [NewtouchHIS_Base].[dbo].[V_S_Sys_Staff]  a
 left join [NewtouchHIS_Base].[dbo].Sys_Department  b on a.DepartmentCode=b.Code and a.OrganizeId=b.OrganizeId and b.zt='1'
 where a.OrganizeId =@orgId
                                                              AND gh =@ysgh
                                                              AND a.zt = '1'";
                vo = this.FirstOrDefault<CqybGjbmInfoVo>(sql, new[] { new SqlParameter("@ysgh", ys), new SqlParameter("@orgId", orgId) });

            }
            else {
                sql = @"select isnull(a.zjh,'') zjh  ,gjybdm,dept.ybksbm,b.gh,b.name,dept.Name ksmc  from [dbo].[Sys_DepartmentBinding] a
left join [NewtouchHIS_Base]..V_S_Sys_Staff b on a.gh=b.gh and a.OrganizeId=b.OrganizeId 
left join  NewtouchHIS_Base.dbo.V_S_Sys_Department dept on dept.code=a.ks and dept.OrganizeId=a.OrganizeId and dept.zt='1'
where ks=@ks and a.OrganizeId=@orgId and a.zt=1";
                var zjhobj = this.FirstOrDefault<CqybGjbmInfoVo>(sql, new[] { new SqlParameter("@ks", ks), new SqlParameter("@orgId", orgId) });
                if (zjhobj != null)
                {
                    vo.gh = zjhobj.gh;
                    vo.name = zjhobj.name;
                    vo.zjh = zjhobj.zjh;
                    vo.gjybdm = zjhobj.gjybdm;
                    vo.ksmc = zjhobj.ksmc;
                }

            }
            if (ks != null)
            {
                string kssql = @"select ybksbm from [NewtouchHIS_Base].[dbo].Sys_Department where OrganizeId = @orgId
                                                              AND Code = @ks
                                                              AND zt = '1' ";
                string ybksbm = this.FirstOrDefault<string>(kssql, new[] { new SqlParameter("@ks", ks), new SqlParameter("@orgId", orgId) });
                vo.ybksbm = ybksbm;
            }
            return vo;
        }
        #endregion

        #region private methods

        /// <summary>
        /// Cập nhậtkhi trư ớc số hóa đ ơn
        /// </summary>
        /// <param name="db"></param>
        /// <param name="orgId"></param>
        /// <param name="fph"></param>
        private void UpdateCurrentFinancialInvoice(Infrastructure.EF.EFDbTransaction db, string orgId, string fph)
        {
            if (!string.IsNullOrWhiteSpace(fph))
            {
                // có thể dùng hóa đ ơn cập nhật
                //Chèn/Cập nhậtcw_fp
                var opr = Newtouch.Common.Operator.OperatorProvider.GetCurrent();
                if (opr != null)
                {
                    FinancialInvoiceEntity fpUpdateEntity, fpInsertEntity;
                    _financialInvoiceRepo.UpdateCurrentGetEntitys(fph, opr.UserCode, out fpUpdateEntity, out fpInsertEntity, orgId);
                    if (fpUpdateEntity != null)
                    {
                        db.Update(fpUpdateEntity);
                    }
                    if (fpInsertEntity != null)
                    {
                        db.Insert(fpInsertEntity);
                    }
                }
                else
                {
                    throw new FailedException("FINANCIALINVOICE_UPDATE_ERROR", "发票号更新异常");
                }
            }
        }

        /// <summary>
        /// Quyết toán phư ơng thức thanh toán, thanh toán tạm ứng thu chi tài khoản
        /// </summary>
        /// <param name="db"></param>
        /// <param name="jszbEntity"></param>
        /// <param name="feeRelated"></param>
        private void PaymentModelAccountReserve(Infrastructure.EF.EFDbTransaction db, OutpatientSettlementEntity jszbEntity
             , OutpatientSettFeeRelatedDTO feeRelated)
        {
            if (jszbEntity.isQfyj == true)
            {
                return; // quyết toán trư ớc khi nợ phí return
            }
            OutpatientSettlementPaymentModelEntity mzzfEntity1 = null;
            OutpatientSettlementPaymentModelEntity mzzfEntity2 = null;

            if (!string.IsNullOrWhiteSpace(feeRelated.zffs1) && (feeRelated.zfje1 ?? 0) > 0)
            {
                mzzfEntity1 = new OutpatientSettlementPaymentModelEntity();
                mzzfEntity1.mzjszffsbh = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_jszffs");
                mzzfEntity1.OrganizeId = jszbEntity.OrganizeId;
                mzzfEntity1.jsnm = jszbEntity.jsnm;
                mzzfEntity1.xjzffs = feeRelated.zffs1;
                mzzfEntity1.zfje = feeRelated.zfje1.Value;
                mzzfEntity1.zt = "1";
                mzzfEntity1.Create();
                db.Insert(mzzfEntity1);
            }
            if (!string.IsNullOrWhiteSpace(feeRelated.zffs2) && (feeRelated.zfje2 ?? 0) > 0)
            {
                mzzfEntity2 = new OutpatientSettlementPaymentModelEntity();
                mzzfEntity2.mzjszffsbh = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_jszffs");
                mzzfEntity2.OrganizeId = jszbEntity.OrganizeId;
                mzzfEntity2.jsnm = jszbEntity.jsnm;
                mzzfEntity2.xjzffs = feeRelated.zffs2;
                mzzfEntity2.zfje = feeRelated.zfje2.Value;
                mzzfEntity2.zt = "1";
                mzzfEntity2.Create();
                mzzfEntity2.CreateTime = DateTime.Now.AddSeconds(1);
                db.Insert(mzzfEntity2);
            }

            //Thanh toán tạm ứng xây dựng thu chi tài khoản //Thanh toán tạm ứng nhất đ ịnh 作là thứ một phư ơng thức thanh toán 
            if (mzzfEntity1 != null && mzzfEntity1.xjzffs == xtzffs.ZYYJZHZF)
            {
                var accountEntity = db.IQueryable<SysAccountEntity>(p => p.patid == jszbEntity.patid).FirstOrDefault();
                if (accountEntity != null)
                {
                    mzzfEntity1.zh = accountEntity.zhCode;
                    if (!(mzzfEntity1.zfje == accountEntity.zhye
                        || (mzzfEntity1.zfje <= feeRelated.xjzfys.Value && mzzfEntity1.zfje <= accountEntity.zhye)))
                    {
                        throw new FailedException("", "预交账户支付金额异常");
                    }
                    var zhszEntity = new SysAccountRevenueAndExpenseEntity()
                    {
                        OrganizeId = jszbEntity.OrganizeId,
                        zhCode = accountEntity.zhCode,
                        patid = accountEntity.patid,
                        szje = 0 - Math.Min(mzzfEntity1.zfje, feeRelated.xjzfys.Value),
                        zhye = accountEntity.zhye - Math.Min(mzzfEntity1.zfje, feeRelated.xjzfys.Value),
                        pzh = null,
                        szxz = (int)EnumSZXZ.mzjs,
                        xjzffs = Constants.xtzffs.ZYYJZHZF,
                        jsnm = jszbEntity.jsnm,
                        zt = "1",
                    };
                    zhszEntity.Create(true);
                    db.Insert(zhszEntity);
                    //
                    accountEntity.zhye = zhszEntity.zhye;
                    db.Update(accountEntity);
                    //Thanh toán tạm ứng Thu chi tài khoản
                    if (mzzfEntity1.zfje > feeRelated.xjzfys.Value)
                    {
                        //Còn một bản ghi thu chi rút tiền(Có lẽ là hoàn toàn bộ số dư tạm ứng)
                        var zhszEntity2 = new SysAccountRevenueAndExpenseEntity()
                        {
                            OrganizeId = jszbEntity.OrganizeId,
                            zhCode = accountEntity.zhCode,
                            patid = accountEntity.patid,
                            szje = 0 - (mzzfEntity1.zfje - feeRelated.xjzfys.Value),
                            zhye = accountEntity.zhye - (mzzfEntity1.zfje - feeRelated.xjzfys.Value),
                            pzh = null,
                            szxz = (int)EnumSZXZ.qk,
                            //xjzffs = Constants.xtzffs.ZYYJZHZF,
                            xjzffs = "0",   //Tiền mặt
                            zt = "1",
                        };
                        zhszEntity2.Create(true);
                        zhszEntity2.CreateTime = DateTime.Now.AddSeconds(1);
                        db.Insert(zhszEntity2);
                        //
                        accountEntity.zhye = zhszEntity2.zhye;
                        //db.Update(accountEntity);
                    }
                }
                else
                {
                    throw new FailedException("ERROR_ACCOUNT_INFO", "获取预交金账户信息失败");
                }
            }
        }

        private void PaymentModelAccountReserveII(Infrastructure.EF.EFDbTransaction db, OutpatientSettlementEntity jszbEntity
            , OutpatientSettFeeRelatedDTO feeRelated)
        {
            if (jszbEntity.isQfyj == true)
            {
                return; // quyết toán trư ớc khi nợ phí return
            }
            OutpatientSettlementPaymentModelEntity mzzfEntity1 = null;
            OutpatientSettlementPaymentModelEntity mzzfEntity2 = null;
            if (feeRelated.yjjzfje.Value > 0)
            {
                mzzfEntity1 = new OutpatientSettlementPaymentModelEntity();
                mzzfEntity1.mzjszffsbh = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_jszffs");
                mzzfEntity1.OrganizeId = jszbEntity.OrganizeId;
                mzzfEntity1.jsnm = jszbEntity.jsnm;
                mzzfEntity1.xjzffs = xtzffs.ZYYJZHZF;
                mzzfEntity1.zfje = feeRelated.yjjzfje.Value;
                mzzfEntity1.zt = "1";
                mzzfEntity1.Create();
                db.Insert(mzzfEntity1);
                jszbEntity.xjzffs = xtzffs.ZYYJZHZF;
            }
            if (feeRelated.patZflist.Count() > 0)
            {
                foreach (var item in feeRelated.patZflist)
                {
                    mzzfEntity2 = new OutpatientSettlementPaymentModelEntity();
                    mzzfEntity2.mzjszffsbh = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_jszffs");
                    mzzfEntity2.OrganizeId = jszbEntity.OrganizeId;
                    mzzfEntity2.jsnm = jszbEntity.jsnm;
                    mzzfEntity2.xjzffs = item.zffsmc;
                    mzzfEntity2.zfje = (decimal)item.zfje;
                    mzzfEntity2.zt = "1";
                    mzzfEntity2.Create();
                    #region 照顾自助机接口，获取不到登录用户
                    if (string.IsNullOrEmpty(mzzfEntity2.CreatorCode))
                    {
                        mzzfEntity2.CreatorCode = "zzjadmin";//Nếu trống,Đ ặt thành đ ăng ký khám tại máy tự phục vụ(giao diện)
                    }
                    #endregion
                    mzzfEntity2.CreateTime = DateTime.Now.AddSeconds(1);
                    db.Insert(mzzfEntity2);
                }
                jszbEntity.xjzffs = feeRelated.djjesszffs;
            }
            //Thanh toán tạm ứng xây dựng thu chi tài khoản //Thanh toán tạm ứng nhất đ ịnh 作là thứ một phư ơng thức thanh toán 
            if (mzzfEntity1 != null && mzzfEntity1.xjzffs == xtzffs.ZYYJZHZF)
            {
                decimal tye = 0;
                if (feeRelated.yjjtye.Value > 0)
                {
                    tye = feeRelated.yjjtye.Value;
                }
                var accountEntity = db.IQueryable<SysAccountEntity>(p => p.patid == jszbEntity.patid).FirstOrDefault();
                if (accountEntity != null)
                {
                    mzzfEntity1.zh = accountEntity.zhCode;
                    if (!((mzzfEntity1.zfje + tye) == accountEntity.zhye
                        || (mzzfEntity1.zfje <= feeRelated.xjzfys.Value && (mzzfEntity1.zfje + tye) <= accountEntity.zhye)))
                    {
                        throw new FailedException("", "预交账户支付金额异常");
                    }
                    var zhszEntity = new SysAccountRevenueAndExpenseEntity()
                    {
                        OrganizeId = jszbEntity.OrganizeId,
                        zhCode = accountEntity.zhCode,
                        patid = accountEntity.patid,
                        //szje = 0 - Math.Min(mzzfEntity1.zfje, feeRelated.xjzfys.Value),
                        //zhye = accountEntity.zhye - Math.Min(mzzfEntity1.zfje, feeRelated.xjzfys.Value),
                        szje = 0 - mzzfEntity1.zfje,
                        zhye = accountEntity.zhye - mzzfEntity1.zfje,
                        pzh = null,
                        szxz = (int)EnumSZXZ.mzjs,
                        xjzffs = Constants.xtzffs.ZYYJZHZF,
                        jsnm = jszbEntity.jsnm,
                        zt = "1",
                    };
                    zhszEntity.Create(true);
                    db.Insert(zhszEntity);
                    //
                    accountEntity.zhye = zhszEntity.zhye;
                    db.Update(accountEntity);
                    //Thanh toán tạm ứng Thu chi tài khoản
                    if (feeRelated.yjjtye.Value > 0 && accountEntity.zhye == feeRelated.yjjtye.Value)
                    {
                        //Còn một bản ghi thu chi rút tiền(Có lẽ là hoàn toàn bộ số dư tạm ứng)
                        var zhszEntity2 = new SysAccountRevenueAndExpenseEntity()
                        {
                            OrganizeId = jszbEntity.OrganizeId,
                            zhCode = accountEntity.zhCode,
                            patid = accountEntity.patid,
                            szje = 0 - feeRelated.yjjtye.Value,
                            zhye = accountEntity.zhye - feeRelated.yjjtye.Value,
                            pzh = null,
                            szxz = (int)EnumSZXZ.qk,
                            //xjzffs = Constants.xtzffs.ZYYJZHZF,
                            xjzffs = Constants.xtzffs.XJZF,   //Tiền mặt
                            zt = "1",
                        };
                        zhszEntity2.Create(true);
                        zhszEntity2.CreateTime = DateTime.Now.AddSeconds(1);
                        db.Insert(zhszEntity2);
                        //
                        accountEntity.zhye = zhszEntity2.zhye;
                    }
                    else if (feeRelated.yjjtye.Value > 0)
                    {
                        throw new FailedException("ERROR_ACCOUNT_INFO", "预交账户结算成功，退余额失败");
                    }
                    //if (mzzfEntity1.zfje > feeRelated.xjzfys.Value)
                    //{
                    //    //Còn một bản ghi thu chi rút tiền(Có lẽ là hoàn toàn bộ số dư tạm ứng)
                    //    var zhszEntity2 = new SysAccountRevenueAndExpenseEntity()
                    //    {
                    //        OrganizeId = jszbEntity.OrganizeId,
                    //        zhCode = accountEntity.zhCode,
                    //        patid = accountEntity.patid,
                    //        szje = 0 - (mzzfEntity1.zfje - feeRelated.xjzfys.Value),
                    //        zhye = accountEntity.zhye - (mzzfEntity1.zfje - feeRelated.xjzfys.Value),
                    //        pzh = null,
                    //        szxz = (int)EnumSZXZ.qk,
                    //        //xjzffs = Constants.xtzffs.ZYYJZHZF,
                    //        xjzffs = "0",   //Tiền mặt
                    //        zt = "1",
                    //    };
                    //    zhszEntity2.Create(true);
                    //    zhszEntity2.CreateTime = DateTime.Now.AddSeconds(1);
                    //    db.Insert(zhszEntity2);
                    //    //
                    //    accountEntity.zhye = zhszEntity2.zhye;
                    //    //db.Update(accountEntity);
                    //}
                    db.Update(accountEntity);
                }
                else
                {
                    throw new FailedException("ERROR_ACCOUNT_INFO", "获取预交金账户信息失败");
                }
            }
        }

        #endregion
        public List<GhJzInfo> GetRegListJson(Inparameter inparameter, string orgid)
        {
            var sb = new StringBuilder();
            var pars = new List<SqlParameter>();

            sb.Append(@"select  gh.ghnm,gh.mzh,gh.blh,gh.xm,gh.zjh,gh.xb,gh.brxz,dept.Name ghksmc,gh.ks ghks,staff.Name ghysmc,gh.ys ghys,gh.ghrq,gh.ghzt,
gh.jzbz jzzt,jz.createtime jzrq,jzdept.Name jzksmc,jz.jzks,jz.jzys,jz.jzysmc,js.fph,
case when gh.ghzt='1' then '否' else '是' end syth,
case when gh.ghzt='1' then null else thys.Name end thys,
case when gh.ghzt='1' then null else convert(varchar(30), gh.LastModifyTime,120) end thrq
from mz_gh(nolock) gh
left join mz_js (nolock) js on gh.ghnm=js.ghnm and gh.OrganizeId=js.OrganizeId and js.jslx='0' and js.jszt='1' and js.zt='1'
left join Newtouch_CIS..xt_jz jz (nolock) on gh.mzh=jz.mzh and gh.OrganizeId=jz.OrganizeId and jz.zt='1'
left join [NewtouchHIS_Base]..V_S_Sys_Staff staff on staff.gh = gh.ys and staff.Organizeid = gh.OrganizeId
left join [NewtouchHIS_Base]..V_S_Sys_Staff thys on thys.gh=gh.LastModifierCode and thys.OrganizeId=gh.OrganizeId
left join [NewtouchHIS_Base]..V_S_Sys_Department dept on dept.Code = gh.ks and dept.Organizeid = gh.OrganizeId
left join [NewtouchHIS_Base]..V_S_Sys_Department jzdept on jzdept.Code = jz.jzks and jzdept.Organizeid = jz.OrganizeId
left join xt_brjbxx jbxx on jbxx.patid = gh.patid and jbxx.zt = '1' and jbxx.OrganizeId = gh.OrganizeId
where gh.OrganizeId = @orgId  and gh.zt = '1'

");
            if (inparameter.ksrq != null && inparameter.jsrq != null)
            {
                sb.Append(@" and CONVERT(varchar(100), gh.ghrq, 23) >= CONVERT(varchar(100), @ksrq, 23)  ");
                sb.Append(@" and CONVERT(varchar(100), gh.ghrq, 23) <= CONVERT(varchar(100), @jsrq, 23)  ");
                pars.Add(new SqlParameter("@ksrq", inparameter.ksrq));
                pars.Add(new SqlParameter("@jsrq", inparameter.jsrq));
            }
            if (!string.IsNullOrWhiteSpace(inparameter.keywordo))
            {
                sb.Append(@" and gh.ks = @kscode ");
                pars.Add(new SqlParameter("@kscode", inparameter.keywordo));
            }

            sb.Append(@" order by gh.ghrq desc ");
            pars.Add(new SqlParameter("@orgId", orgid));

            return this.FindList<GhJzInfo>(sb.ToString(), pars.ToArray());
        }

        #region  医保相关
        public CqybGjbmInfoVo GetYbjzdjVo(string mzh, string orgId)
        {
            string sqlstr = @" select staff.zjh,staff.gjybdm,staff.gh,staff.name,mzgh.ks,dept.ybksbm,dept.Name ksmc,jzid
from mz_gh mzgh (nolock)
left join NewtouchHIS_Base..V_S_Sys_Staff staff on staff.gh=mzgh.ys and staff.OrganizeId=mzgh.OrganizeId and staff.zt='1'
left join NewtouchHIS_Base.dbo.V_S_Sys_Department dept on dept.code=mzgh.ks and dept.OrganizeId=mzgh.OrganizeId and dept.zt='1'
where mzgh.mzh=@mzh and mzgh.OrganizeId=@orgId and mzgh.zt='1' ";
            return  this.FirstOrDefault<CqybGjbmInfoVo>(sqlstr, new[] { new SqlParameter("@mzh", mzh), new SqlParameter("@orgId", orgId) });
        }
        /// <summary>
        /// Lấy bệnh viện dòng 政Quận划mã来 phán đoán có hay không nơi khác BHYT
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public string GetOrgXzqh(string orgId)
        {
            string sqlstr = "select ybxzqhCode from NewtouchHIS_Base..sys_organize where id = @orgId and zt = '1' ";
            return this.FirstOrDefault<string>(sqlstr, new[] { new SqlParameter("@orgId", orgId) });
        }
        #endregion
    }
}
