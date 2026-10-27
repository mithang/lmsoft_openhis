using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using SqlSugar;
using System.Linq;
using System.Text;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Tools;
using Newtouch.HIS.Domain.ValueObjects.Medicine;

namespace Newtouch.HIS.DomainServices
{
    public class MedicineDmnService : DmnServiceBase, IMedicineDmnService
    {

        
        #region Báo hỏng báo thừa
        /// <summary>
        /// Truy vấn thuốc lãi lỗlist
        /// </summary>
        /// <param name="inputCode">Từ khóa</param>
        /// <returns></returns>
        public List<ReportLossAndProfitMedicineInfoVO> SelectLossAndProfitMedicineList(string inputCode)
        {
            var strSql = new StringBuilder(@"
SELECT * FROM (
select distinct top 100
    yp.ypCode,yp.ypmc,yp.ycmc sccj,ypsx.ypgg
    ,isnull(sum(isnull(kcxx.kcsl,0)) ,0) kcsl
    ,(select dbo.[f_getYfbmYpComplexYpSlandDw](isnull(sum(isnull(kcxx.kcsl,0)) ,0),@yfbmCode,yp.ypcode,@OrganizeId)) xykcstr
    ,yp.zxdw zxdw
    ,CONVERT(VARCHAR(10), isnull(max(kcxx.yxq),'1899-01-01'), 120) yxq
    ,LTRIM(RTRIM(kcxx.ph)) ph,LTRIM(RTRIM(kcxx.pc)) pc
    ,CONVERT(DECIMAL(12,4), dbo.f_getyfbmZhyz(yfbm.yfbmCode, yp.ypcode, @OrganizeId)) zhyz
	,dbo.f_getyfbmDw(yfbm.yfbmCode, yp.ypCode, @Organizeid) djdw
    ,CONVERT(DECIMAL(12,4),yp.lsj/yp.bzs) lsj
    ,CONVERT(DECIMAL(12,4),yp.pfj/yp.bzs) pfj
    ,CONVERT(DECIMAL(12,4),yp.lsj) Yklsj
    ,CONVERT(DECIMAL(12,4),yp.pfj) Ykpfj
	,CONVERT(DECIMAL(12,4),kcxx.jj/yp.bzs) jj
    ,sfdl.dlmc yplb
    ,case yp.mzzybz when '1' then '正常' when '0' then '停用' end ypzt
from xt_yp_kcxx(NOLOCK) kcxx
INNER join NewtouchHIS_Base.dbo.xt_yp(NOLOCK) yp on kcxx.ypdm=yp.ypCode and yp.organizeId=@OrganizeId AND yp.zt='1'
INNER join NewtouchHIS_Base.dbo.xt_ypsx(NOLOCK) ypsx on ypsx.ypId=yp.ypId AND ypsx.OrganizeId=yp.OrganizeId AND ypsx.zt='1'
INNER join NewtouchHIS_Base.dbo.V_S_xt_yfbm_yp yfbmyp on yfbmyp.yfbmCode=kcxx.yfbmCode and yfbmyp.dlCode=yp.dlcode and yfbmyp.organizeId=@OrganizeId 
INNER join NewtouchHIS_Base.dbo.V_S_xt_yfbm yfbm on yfbm.yfbmCode =yfbmyp.yfbmCode and yfbm.organizeId=@OrganizeId 
left join NewtouchHIS_Base.dbo.V_S_xt_sfdl sfdl on sfdl.dlCode=yp.dlCode and sfdl.organizeId=@OrganizeId 
where kcxx.yfbmCode =@yfbmCode 
AND kcxx.organizeId=@OrganizeId
");

            if (!string.IsNullOrEmpty(inputCode))
            {
                strSql.AppendLine("and (yp.ypcode like @inputCode or yp.ypmc like @inputCode or yp.spm like @inputCode or yp.py like @inputCode)");
            }
            strSql.Append(@"
group by kcxx.jj, yp.ypcode,yp.ypmc,yp.ycmc,ypsx.ypgg,kcxx.yxq,kcxx.ph,kcxx.pc,yfbm.mzzybz,yfbm.yfbmCode,sfdl.dlmc,yp.mzzybz,yp.bzs,yp.mzcls,yp.zycls,yp.bzdw,yp.mzcldw,yp.zycldw,yp.lsj,yp.pfj,zxdw
) a
order by a.ypcode,a.kcsl,a.yxq
                         ");
            var param = new SugarParameter[]
            {
                new SugarParameter("@inputCode","%"+(inputCode??"").Trim()+"%"),
                new SugarParameter ("@yfbmCode",Constants.CurrentYfbm.yfbmCode),
                new SugarParameter("@OrganizeId",OperatorProvider.GetCurrent().OrganizeId)
            };
            return FindList<ReportLossAndProfitMedicineInfoVO>(strSql.ToString(), param).ToList();
        }

        /// <summary>
        /// Truy vấn của thuốckcslvàjj
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="ph"></param>
        /// <param name="pc"></param>
        /// <param name="yxq"></param>
        /// <param name="kcsl"></param>
        /// <param name="jj"></param>
        public void SelectKcslAndJj(string ypCode, string ph, string pc, DateTime? yxq, out int kcsl, out decimal jj)
        {
            SugarParameter[] param =
            {
                new SugarParameter("@ypCode",ypCode),
                new SugarParameter("@pc",pc),
                new SugarParameter("@yfbmCode",Constants.CurrentYfbm.yfbmCode),
                new SugarParameter("@OrganizeId",OperatorProvider.GetCurrent().OrganizeId)
            };
            var entity = FindList<StockQuantityAndPriceVO>("SELECT ISNULL(kcsl,0) kcsl,ISNULL(jj,0) jj FROM xt_yp_kcxx(nolock) WHERE Tybz='0' AND yfbmCode=@yfbmCode AND Ypdm=@ypCode AND pc= @pc", param).FirstOrDefault();
            kcsl = entity.kcsl;
            jj = entity.jj;
        }

        /// <summary>
        /// Lưu
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="kcsl"></param>
        /// <param name="jj"></param>
        public void SaveReportLossAndProfit(SysMedicineProfitLossEntity entity, int kcsl, decimal jj)
        {
            var yfbmCode = Constants.CurrentYfbm.yfbmCode;
            var organizeId = OperatorProvider.GetCurrent().OrganizeId;
            using (var db = new EFDbTransaction().BeginTrans())
            {
                //insert Bảng lãi lỗ
                var sykc = kcsl + entity.Sysl;
                entity.Sykc = sykc; //Tồn kho lãi lỗ
                entity.Jj = jj;  //Giá nhập
                db.Insert(entity);

                //update Tồn kho
                var kcxxEntity = db.IQueryable<SysMedicineStockInfoEntity>().FirstOrDefault(a => a.zt == "1" && a.tybz == 0 && a.yfbmCode == yfbmCode && a.OrganizeId == organizeId && a.ypdm == entity.Ypdm && a.pc == (entity.pc ?? ""));
                kcxxEntity.kcsl = kcsl + entity.Sysl;
                kcxxEntity.ph = kcxxEntity.ph.Trim();
                kcxxEntity.Modify();
                db.Update(kcxxEntity);
                db.Commit();
            }
        }

        /// <summary>
        /// Báo hỏng báo thừa Lưu
        /// </summary>
        /// <param name="profitLossEntityList"></param>
        public string SaveReportLossAndProfit(List<YpSyxxVo> profitLossEntityList)
        {
            var param = new SugarParameter[]
            {
                new SugarParameter("@CreatorCode", OperatorProvider.GetCurrent().UserCode),
                new SugarParameter("@syxx", profitLossEntityList.ToDataTable()){
                    TypeName = "dbo.yp_syxx"
                }
            };
            return FirstOrDefault<string>("EXEC [dbo].[sp_yp_bsby] @CreatorCode, @syxx", param);
        }
        #endregion

        #region Truy vấn báo hỏng báo thừa
        /// <summary>
        /// Truy vấn báo hỏng báo thừa
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="inputCode"></param>
        /// <param name="syyy"></param>
        /// <param name="syqk"></param>
        /// <returns></returns>
        public IList<LossAndProditInfoVO> SelectLossAndProditInfoList(Pagination pagination, string startTime, string endTime, string inputCode, string syyy, int syqk)
        {
            var strSql = new StringBuilder(@"
         SELECT DISTINCT syxx.syId, FLOOR(syxx.sykc/syxx.Zhyz) sykc, CASE WHEN syxx.sysl > 0 THEN '报溢' ELSE '报损' END AS syqk, yp.ypmc,yp.ypCode, sf.dlmc AS yplb
	    , yp.py, syxx.ph, convert(varchar(100), syxx.yxq, 111) AS yxq, syxx.bgsj 
		,dbo.f_getYfbmYpComplexYpSlandDw(syxx.sysl,syxx.yfbmCode,syxx.ypdm,syxx.OrganizeId) sysl
		,syxx.Zhyz
        , convert(decimal(12, 4),kcxx.jj) AS jj 
        , convert(decimal(12, 2),(kcxx.jj * ((CASE WHEN syxx.sysl > 0 THEN syxx.sysl ELSE -syxx.sysl END)/syxx.Zhyz))) AS jjze
		, convert(decimal(12, 4), yp.pfj) pfj
        , convert(decimal(12, 2), (yp.pfj  * ((CASE WHEN syxx.sysl > 0 THEN syxx.sysl ELSE -syxx.sysl END)/syxx.Zhyz))) AS pjze
		, convert(decimal(12, 4), yp.lsj) lsj
	    , convert(decimal(12, 2), (yp.lsj * ((CASE WHEN syxx.sysl > 0 THEN syxx.sysl ELSE -syxx.sysl END)/syxx.Zhyz))) AS ljze
		, ypsyyy.syyy, syxx.CreatorCode, syxx.zrr
	    , syxx.djh, ypsx.ypgg
        , yp.zxdw AS dw, yp.ycmc, staff.Name
        ,syxx.CreateTime
    FROM xt_yp_syxx(NOLOCK) syxx
    left join (select kcxx.OrganizeId,kcxx.ypdm,max(yfbmCode) yfbmCode,max(kcxx.jj) jj,ph,zt
			  from NewtouchHIS_PDS.dbo.xt_yp_kcxx(NOLOCK)  as kcxx 
			  group by kcxx.OrganizeId,kcxx.ypdm,kcxx.yfbmCode,ph,zt) as kcxx on syxx.Ypdm = kcxx.ypdm and syxx.Ph = kcxx.ph and kcxx.yfbmCode = syxx.yfbmCode and kcxx.OrganizeId = @OrganizeId and kcxx.zt != '0'
    INNER JOIN NewtouchHIS_Base.dbo.xt_yp(NOLOCK) yp ON syxx.ypdm = yp.ypCode and yp.OrganizeId=@OrganizeId AND yp.zt='1'
    INNER JOIN NewtouchHIS_Base.dbo.xt_ypsx(NOLOCK) ypsx ON ypsx.ypId=yp.ypId AND ypsx.zt='1' AND ypsx.OrganizeId=yp.OrganizeId
    LEFT JOIN xt_ypsyyy(NOLOCK) ypsyyy ON syxx.syyy = ypsyyy.syyyId
    LEFT JOIN NewtouchHIS_Base.dbo.V_S_xt_sfdl sfdl ON yp.dlCode = sfdl.dlCode and sfdl.OrganizeId=@OrganizeId
    LEFT JOIN NewtouchHIS_Base.dbo.V_S_Sys_Staff staff ON syxx.CreatorCode = staff.gh and staff.OrganizeId=@OrganizeId
    LEFT JOIN (SELECT DISTINCT yfbmCode, dlCode FROM NewtouchHIS_Base.dbo.V_S_xt_yfbm_yp where OrganizeId=@OrganizeId) yfbmyp ON yfbmyp.yfbmCode = syxx.yfbmCode AND yfbmyp.dlCode = yp.dlCode
    LEFT JOIN NewtouchHIS_Base.dbo.V_S_xt_yfbm yfbm ON yfbm.yfbmCode = yfbmyp.yfbmCode and yfbm.OrganizeId=@OrganizeId
    LEFT JOIN (SELECT dlCode, substring(dlmc, 0, len(dlmc)) AS dlmc FROM NewtouchHIS_Base.dbo.V_S_xt_sfdl WHERE OrganizeId=@OrganizeId) sf ON yp.dlCode = sf.dlCode
    WHERE syxx.yfbmCode = @yfbmCode
    and syxx.zt='1'
    AND syxx.OrganizeId=@OrganizeId
    AND (@syqk = -1 OR (@syqk = 1 AND syxx.sysl > 0) OR (@syqk = 2 AND syxx.sysl < 0))
                        ");
            var inSugarParameterList = new List<SugarParameter>();
            if (!string.IsNullOrEmpty(startTime))
            {
                strSql.Append(@" AND syxx.bgsj >= @startTime ");
                inSugarParameterList.Add(new SugarParameter("@startTime", startTime));
            }
            if (!string.IsNullOrEmpty(endTime))
            {
                strSql.Append(@" AND syxx.bgsj <= @endTime ");
                inSugarParameterList.Add(new SugarParameter("@endTime", endTime));
            }
            if (!string.IsNullOrEmpty(syyy))
            {
                strSql.Append(@" AND (syxx.syyy = @syyy OR @syyy='-1')");
                inSugarParameterList.Add(new SugarParameter("@syyy", syyy));
            }
            if (!string.IsNullOrEmpty(inputCode))
            {
                strSql.Append(@" AND (yp.py LIKE @inputCode or yp.ypCode like @inputCode or yp.ypmc like @inputCode)");
                inSugarParameterList.Add(new SugarParameter("@inputCode", "%" + (inputCode ?? "") + "%"));
            }
            inSugarParameterList.Add(new SugarParameter("@syqk", syqk));
            inSugarParameterList.Add(new SugarParameter("@OrganizeId", OperatorProvider.GetCurrent().OrganizeId));
            inSugarParameterList.Add(new SugarParameter("@yfbmCode", Constants.CurrentYfbm.yfbmCode));
            return QueryWithPage<LossAndProditInfoVO>(strSql.ToString(), pagination, inSugarParameterList.ToArray());
        }

        /// <summary>
        /// Tổng số tiền giá bán buôn、Truy vấn tổng số tiền giá bán lẻ
        /// </summary>
        /// <param name="startTime">Thời gian bắt đầu</param>
        /// <param name="endTime">Thời gian kết thúc</param>
        /// <param name="syyy">Lý do lãi lỗ</param>
        /// <param name="inputCode">Từ khóa</param>
        /// <param name="syqk">Tình hình lãi lỗ</param>
        /// <returns></returns>
        public LossAndProditInfoJeVo ComputePjzeAndLjze(string startTime, string endTime, string syyy, string inputCode, int syqk)
        {
            var strSql = new StringBuilder(@"
IF EXISTS(SELECT 1 FROM tempdb..sysobjects where id=object_id(N'tempdb..#tmpyfbm_yp') and type='U')
BEGIN
	DROP TABLE #tmpyfbm_yp;
END
SELECT DISTINCT yfbmCode, dlCode INTO #tmpyfbm_yp FROM NewtouchHIS_Base.dbo.V_S_xt_yfbm_yp where OrganizeId=@OrganizeId

IF EXISTS(SELECT 1 FROM tempdb..sysobjects where id=object_id(N'tempdb..#tmpyfbm') and type='U')
BEGIN
	DROP TABLE #tmpyfbm;
END
SELECT dlCode, substring(dlmc, 0, len(dlmc)) AS dlmc INTO #tmpyfbm FROM NewtouchHIS_Base.dbo.V_S_xt_sfdl WHERE OrganizeId=@OrganizeId

SELECT ISNULL(SUM(a.ljze), 0) Ljze, ISNULL(SUM(a.pjze), 0) Pjze, ISNULL(SUM(a.jjze), 0) Jjze 
FROM (
    SELECT convert(decimal(19, 2), syxx.sysl * syxx.lsj) AS ljze
    , convert(decimal(19, 2), syxx.sysl * syxx.pfj) AS pjze
    , convert(decimal(19, 2), syxx.jj * syxx.sysl) AS jjze
    FROM xt_yp_syxx(NOLOCK) syxx
    LEFT JOIN xt_ypsyyy ypsyyy(NOLOCK) ON syxx.syyy = ypsyyy.syyyId
    INNER JOIN NewtouchHIS_Base.dbo.xt_yp(NOLOCK) yp ON syxx.ypdm = yp.ypCode and yp.OrganizeId=syxx.OrganizeId AND yp.zt='1'
    INNER JOIN NewtouchHIS_Base.dbo.xt_ypsx(NOLOCK) ypsx ON ypsx.ypId=yp.ypId AND ypsx.zt='1' AND ypsx.OrganizeId=yp.OrganizeId 
    INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_sfdl sfdl ON yp.dlCode = sfdl.dlCode and sfdl.OrganizeId=syxx.OrganizeId
    LEFT JOIN NewtouchHIS_Base.dbo.V_S_Sys_Staff staff ON syxx.CreatorCode = staff.gh and staff.OrganizeId=@OrganizeId
    LEFT JOIN #tmpyfbm_yp yfbmyp ON yfbmyp.yfbmCode = syxx.yfbmCode AND yfbmyp.dlCode = yp.dlCode
    LEFT JOIN NewtouchHIS_Base.dbo.V_S_xt_yfbm yfbm ON yfbm.yfbmCode = yfbmyp.yfbmCode and yfbm.OrganizeId=@OrganizeId
    LEFT JOIN #tmpyfbm sf ON yp.dlCode = sf.dlCode
    WHERE syxx.yfbmCode = @yfbmCode
    AND syxx.OrganizeId=@OrganizeId
    AND (@syqk = -1 OR (@syqk = 1 AND syxx.sysl > 0) OR (@syqk = 2 AND syxx.sysl < 0))
                        ");
            var inSugarParameterList = new List<SugarParameter>();
            if (!string.IsNullOrEmpty(startTime))
            {
                strSql.AppendLine(@"AND syxx.bgsj >= @startTime ");
                inSugarParameterList.Add(new SugarParameter("@startTime", startTime));
            }
            if (!string.IsNullOrEmpty(endTime))
            {
                strSql.AppendLine(@"AND syxx.bgsj <= @endTime ");
                inSugarParameterList.Add(new SugarParameter("@endTime", endTime));
            }
            if (!string.IsNullOrEmpty(syyy))
            {
                strSql.AppendLine(@"AND (syxx.syyy = @syyy OR @syyy='-1')");
                inSugarParameterList.Add(new SugarParameter("@syyy", syyy));
            }
            if (!string.IsNullOrEmpty(inputCode))
            {
                strSql.AppendLine(@"AND (yp.py LIKE @inputCode or yp.ypCode like @inputCode or yp.ypmc like @inputCode)");
                inSugarParameterList.Add(new SugarParameter("@inputCode", "%" + (inputCode ?? "") + "%"));
            }
            strSql.AppendLine(") a");
            inSugarParameterList.Add(new SugarParameter("@syqk", syqk));
            inSugarParameterList.Add(new SugarParameter("@OrganizeId", OperatorProvider.GetCurrent().OrganizeId));
            inSugarParameterList.Add(new SugarParameter("@yfbmCode", Constants.CurrentYfbm.yfbmCode));
            return FindList<LossAndProditInfoJeVo>(strSql.ToString(), inSugarParameterList.ToArray()).FirstOrDefault();
        }
        #endregion


        #region Truy vấn lãi lỗ điều chỉnh giá
        /// <summary>
        /// Truy vấn lãi lỗ điều chỉnh giá
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="srm"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="lkc"></param>
        /// <param name="allUseableYfbmCodes"></param>
        /// <returns></returns>
        public IList<SysMedicinePriceAdjustmentProfitLossVO> SelectPriceAdjustmentProfitLossList(Pagination pagination, DateTime? startTime, DateTime? endTime, string srm, string yfbmCode, bool lkc, string allUseableYfbmCodes = null)
        {
            IList<SugarParameter> inSugarParameterList = new List<SugarParameter>();
            var strSql = new StringBuilder(@"select T.TjsyId,Y.ypCode,Y.ypmc,Y.spm,Y.py,Y.bzdw,Y.mzcls ,Y.mzcldw,Y.zycls, Y.zycldw,Y.djdw,Y.zfbl,B.mzzybz,B.yfbmmc,X.ypgg,Y.ycmc,T.Dssl,
            CASE B.mzzybz WHEN '0' THEN Y.bzdw WHEN '1' THEN Y.mzcldw WHEN '2' THEN Y.zycldw END dw,
            T.Ph,T.pc,T.Tjsj,T.Tjwj,T.Ypfj,T.Xpfj,Pfjtjlr 
            ,T.Ylsj,T.Xlsj,Lsjtjlr,staff.Name,T.CreateTime,T.CreatorCode,y.zt
            From XT_YP_TJSY T 
            LEFT JOIN NewtouchHIS_Base..xt_yp Y ON Y.ypCode = T.ypCode and Y.OrganizeId = T.OrganizeId AND Y.zt='1'
            LEFT JOIN NewtouchHIS_Base..xt_ypsx X ON X.ypCode = Y.ypCode and X.OrganizeId = T.OrganizeId AND X.zt='1'
            LEFT JOIN NewtouchHIS_Base..xt_yfbm B ON B.yfbmCode = T.yfbmCode and B.OrganizeId = T.OrganizeId AND B.zt='1'
            LEFT JOIN NewtouchHIS_Base..Sys_User R ON R.Account = T.CreatorCode AND R.zt='1'
            LEFT JOIN NewtouchHIS_Base..Sys_UserStaff US ON US.UserId = R.Id AND US.zt='1'
            left join NewtouchHIS_Base..Sys_Staff staff on staff.Id = US.StaffId and staff.OrganizeId = T.OrganizeId AND US.zt='1'
            WHERE
            T.OrganizeId = @OrganizeId
            ");
            if (startTime.HasValue)
            {
                strSql.Append(" and T.Tjsj > @startTime");
                inSugarParameterList.Add(new SugarParameter("@startTime", startTime.Value));
            }
            if (endTime.HasValue)
            {
                strSql.Append(" and T.Tjsj < @endTime");
                inSugarParameterList.Add(new SugarParameter("@endTime", endTime.Value));
            }
            if (!string.IsNullOrWhiteSpace(srm))
            {
                strSql.Append(" AND (Y.ypCode LIKE @searchSrm OR Y.ypmc LIKE @searchSrm OR Y.py LIKE @searchSrm OR Y.spm LIKE @searchSrm)");
                inSugarParameterList.Add(new SugarParameter("@searchSrm", "%" + srm + "%"));
            }
            if (!string.IsNullOrWhiteSpace(yfbmCode))
            {
                strSql.Append(" AND B.yfbmCode = @yfbmCode");
                inSugarParameterList.Add(new SugarParameter("@yfbmCode", yfbmCode));
            }
            if (!lkc)
            {
                strSql.Append(" AND T.Dssl > 0");
            }
            //if (!string.IsNullOrWhiteSpace(allUseableYfbmCodes))
            //{
            //    strSql.AppendFormat(" and T.yfbmCode in ({0})", allUseableYfbmCodes);
            //}
            inSugarParameterList.Add(new SugarParameter("@OrganizeId", OperatorProvider.GetCurrent().OrganizeId));
            var list = this.QueryWithPage<SysMedicinePriceAdjustmentProfitLossVO>(strSql.ToString(), pagination, inSugarParameterList.ToArray());
            return list;
        }
        #endregion


        /// <summary>
        /// Truy vấn chi tiết kiểm kê
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="inputCode"></param>
        /// <param name="syyy"></param>
        /// <param name="syqk"></param>
        /// <returns></returns>
        public IList<InventoryQureyinfoVO> GetInventoryQureyInfo(Pagination pagination, string startTime, string endTime, string inputCode, string syyy, int syqk)
        {
            var strSql = new StringBuilder(@"
      SELECT DISTINCT syxx.pdId, FLOOR(syxx.pdkc/syxx.Zhyz) pdkc, CASE WHEN syxx.pdsl > 0 THEN '盘盈' ELSE '盘亏' END AS pdqk, yp.ypmc,yp.ypCode, sf.dlmc AS yplb
	    , yp.py, syxx.ph, convert(varchar(100), syxx.yxq, 111) AS yxq, syxx.bgsj 
		,dbo.f_getYfbmYpComplexYpSlandDw(syxx.pdsl,syxx.yfbmCode,syxx.ypdm,syxx.OrganizeId) pdsl
		,syxx.Zhyz
        ,  syxx.Jj AS jj
        , (abs(syxx.Pdsl / syxx.Zhyz)*syxx.Jj )AS jjze
		, convert(decimal(12, 4), syxx.pfj/yp.bzs*syxx.Zhyz) pfj
        , (abs(syxx.Pdsl / syxx.Zhyz)*convert(decimal(12, 4), syxx.pfj/yp.bzs*syxx.Zhyz)) AS pjze
		, convert(decimal(12, 4), syxx.lsj) lsj
	    , (abs(syxx.Pdsl / syxx.Zhyz)*syxx.lsj) AS ljze
		, syxx.CreatorCode, syxx.zrr
	    , syxx.djh, ypsx.ypgg
        , yp.zxdw AS dw, yp.ycmc, staff.Name
        ,syxx.CreateTime
    FROM xt_yp_pdmx(NOLOCK) syxx
    INNER JOIN NewtouchHIS_Base.dbo.xt_yp(NOLOCK) yp ON syxx.ypdm = yp.ypCode and yp.OrganizeId=@OrganizeId AND yp.zt='1'
    INNER JOIN NewtouchHIS_Base.dbo.xt_ypsx(NOLOCK) ypsx ON ypsx.ypId=yp.ypId AND ypsx.zt='1' AND ypsx.OrganizeId=yp.OrganizeId
    --LEFT JOIN xt_ypsyyy(NOLOCK) ypsyyy ON syxx.syyy = ypsyyy.syyyId
    LEFT JOIN NewtouchHIS_Base.dbo.V_S_xt_sfdl sfdl ON yp.dlCode = sfdl.dlCode and sfdl.OrganizeId=@OrganizeId
    LEFT JOIN NewtouchHIS_Base.dbo.V_S_Sys_Staff staff ON syxx.CreatorCode = staff.gh and staff.OrganizeId=@OrganizeId
    LEFT JOIN (SELECT DISTINCT yfbmCode, dlCode FROM NewtouchHIS_Base.dbo.V_S_xt_yfbm_yp where OrganizeId=@OrganizeId) yfbmyp ON yfbmyp.yfbmCode = syxx.yfbmCode AND yfbmyp.dlCode = yp.dlCode
    LEFT JOIN NewtouchHIS_Base.dbo.V_S_xt_yfbm yfbm ON yfbm.yfbmCode = yfbmyp.yfbmCode and yfbm.OrganizeId=@OrganizeId
    LEFT JOIN (SELECT dlCode, substring(dlmc, 0, len(dlmc)) AS dlmc FROM NewtouchHIS_Base.dbo.V_S_xt_sfdl WHERE OrganizeId=@OrganizeId) sf ON yp.dlCode = sf.dlCode
    WHERE syxx.yfbmCode = @yfbmCode
    AND syxx.OrganizeId=@OrganizeId
    AND (@syqk = -1 OR (@syqk = 1 AND syxx.pdsl > 0) OR (@syqk = 2 AND syxx.pdsl < 0))
                        ");
            var inSugarParameterList = new List<SugarParameter>();
            if (!string.IsNullOrEmpty(startTime))
            {
                strSql.Append(@" AND syxx.bgsj >= @startTime ");
                inSugarParameterList.Add(new SugarParameter("@startTime", startTime));
            }
            if (!string.IsNullOrEmpty(endTime))
            {
                strSql.Append(@" AND syxx.bgsj <= @endTime ");
                inSugarParameterList.Add(new SugarParameter("@endTime", endTime));
            }
            if (!string.IsNullOrEmpty(syyy))
            {
                strSql.Append(@" AND (syxx.syyy = @syyy OR @syyy='-1')");
                inSugarParameterList.Add(new SugarParameter("@syyy", syyy));
            }
            if (!string.IsNullOrEmpty(inputCode))
            {
                strSql.Append(@" AND (yp.py LIKE @inputCode or yp.ypCode like @inputCode or yp.ypmc like @inputCode)");
                inSugarParameterList.Add(new SugarParameter("@inputCode", "%" + (inputCode ?? "") + "%"));
            }
            inSugarParameterList.Add(new SugarParameter("@syqk", syqk));
            inSugarParameterList.Add(new SugarParameter("@OrganizeId", OperatorProvider.GetCurrent().OrganizeId));
            inSugarParameterList.Add(new SugarParameter("@yfbmCode", Constants.CurrentYfbm.yfbmCode));
            return QueryWithPage<InventoryQureyinfoVO>(strSql.ToString(), pagination, inSugarParameterList.ToArray());
        }

        #region Thống kê phát thuốc

        /// <summary>
        /// Truy vấn loại lớn thuốc theo từ khóa
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        public IList<MedicineClassificationVO> GetMedicineClassificationList(string keyword = null)
        {
            var sql = @"select * from NewtouchHIS_Base.dbo.xt_ypfl(nolock) where zt = '1' and (ypflCode like @searchKeyword or ypflmc like @searchKeyword or py like @searchKeyword) order by CreateTime desc";
            return this.FindList<MedicineClassificationVO>(sql, new SugarParameter[] {
                    new SugarParameter("@searchKeyword", "%" + (keyword ?? "") + "%")
                });
        }
        #endregion
    }
}
