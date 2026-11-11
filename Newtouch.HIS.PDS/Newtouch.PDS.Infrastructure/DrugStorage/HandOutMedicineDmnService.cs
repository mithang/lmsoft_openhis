using NewtouchHIS.Lib.Base.Extension;
using System;
using NewtouchHIS.Lib.Base.Model;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using SqlSugar;
using System.Linq;
using System.Text;
using FrameworkBase.MultiOrg.Infrastructure;
using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.DTO.DrugStorage;
using Newtouch.HIS.Domain.Entity.V;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Infrastructure;
using Newtouch.Infrastructure.TSQL;
using Newtouch.Tools;

namespace Newtouch.HIS.DomainServices
{
    /// <summary>
    /// Phát thuốc
    /// </summary>
    public class HandOutMedicineDmnService : DmnServiceBase, IHandOutMedicineDmnService
    {
        
        /// <summary>
        /// Tự động gợi ý mã nhập(Nhắc nhở bộ phận cấp 1,Không có hệ số chuyển đổi,Ví dụ khoa dược)
        /// </summary>
        /// <param name="yfbmCode"></param>
        /// <param name="srm"></param>
        /// <returns></returns>
        public List<HandOutMedicinesrmVO> GetsrmInfoFlevelList(string yfbmCode, string srm)
        {
            var par = new SugarParameter[] {
                    new SugarParameter("@yfbmCode",yfbmCode),
                    new SugarParameter("@srm","%"+(string.IsNullOrEmpty(srm)?"":srm.Trim())+"%"),
                    new SugarParameter("@OrganizeId", OperatorProvider.GetCurrent().OrganizeId)
            };
            return FindList<HandOutMedicinesrmVO>(@" EXEC[dbo].[xt_yp_GetsrmInfoFlevelList] @srm, @yfbmCode, @OrganizeId ", par);
        }

        /// <summary>
        /// Lấy thông tin thuốc  Nhóm theo lô/số lô
        /// </summary>
        /// <param name="ypCode"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public List<HandOutMedicinesrmVO> GetDrugGroupByPc(string ypCode, string yfbmCode, string organizeId)
        {
            const string sql = @"
SELECT s.ypmc,s.ycmc,s.ph,s.pc,s.zhyz,s.ypCode,s.zxdw,s.bzs,s.bzdw,s.bmdw,s.yxq,CONVERT(NUMERIC(11,2),s.zxdwjj*s.zhyz) jj
,ISNULL(CONVERT(INT,s.kykcsl/s.zhyz),0) bmdwsl
,dbo.f_getComplexYpSlandDw(s.kykcsl,s.zhyz,s.bmdw, s.zxdw) klsl
,ISNULL(CONVERT(INT,s.kykcsl/s.bzs),0) bzdwsl 
FROM (
	SELECT a.ypmc ,ycmc ,LTRIM(RTRIM(b.ph)) ph ,LTRIM(RTRIM(b.pc)) pc ,a.ypCode , ISNULL(CONVERT(NUMERIC(11,4),(b.jj/a.bzs)),0) zxdwjj,a.zxdw, a.bzs, a.bzdw
	,ISNULL(CONVERT(INT,CASE e.mzzybz WHEN '0' THEN a.bzs WHEN '1' THEN a.mzcls WHEN '2' THEN a.zycls WHEN '3' THEN a.mzcls END), 0) zhyz 
	,(CASE e.mzzybz WHEN '0' THEN a.bzdw WHEN '1' THEN a.mzcldw WHEN '2' THEN a.zycldw WHEN '3' THEN a.mzcldw END) bmdw , 
	CONVERT(DATETIME,ISNULL(MAX(CONVERT(VARCHAR(10), b.yxq, 120)), '1899-01-01')) yxq ,
	ISNULL(SUM(b.kcsl - b.djsl), 0) kykcsl
	FROM NewtouchHIS_Base.dbo.V_S_xt_yp(NOLOCK) a 
	INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx(NOLOCK) ypsx ON ypsx.ypId=a.ypId AND ypsx.OrganizeId=a.OrganizeId
	INNER JOIN dbo.xt_yp_bmypxx(NOLOCK) bmypxx ON bmypxx.Ypdm=a.ypCode AND bmypxx.OrganizeId=a.OrganizeId AND bmypxx.zt='1'
	INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yfbm e WITH ( NOLOCK ) ON e.yfbmCode=bmypxx.yfbmCode AND e.OrganizeId=a.OrganizeId AND e.zt='1'
	LEFT JOIN dbo.xt_yp_kcxx(NOLOCK) b ON b.ypdm = a.ypCode AND b.yfbmCode = bmypxx.yfbmCode AND b.OrganizeId=a.OrganizeId AND b.zt='1'
	WHERE e.yfbmCode=@yfbmCode
	AND bmypxx.yfbmCode=@yfbmCode
	AND a.ypCode=@ypCode
	AND a.OrganizeId=@OrganizeId
	GROUP BY a.ypmc, a.ycmc, b.ph, b.pc, a.ypCode, a.pfj, a.lsj, b.jj, a.zxdw, a.bzs, e.mzzybz, b.yxq, a.bzdw, a.mzcls, a.zycls, a.mzcldw, a.zycldw
) s
ORDER BY s.yxq ASC
";
            var param = new SugarParameter[]
            {
                new SugarParameter("@ypCode",ypCode),
                new SugarParameter("@yfbmCode", yfbmCode),
                new SugarParameter("@OrganizeId", organizeId),
            };
            return FindList<HandOutMedicinesrmVO>(sql, param);
        }

        /// <summary>
        /// Lấy chi tiết thuốc trả
        /// </summary>
        /// <param name="ckbm">Bộ phận xuất kho Bộ phận xin trả thuốc</param>
        /// <param name="rkbm">Bộ phận nhập kho Bộ phận thuốc sắp trả về</param>
        /// <param name="srm">Mã nhập Từ khóa</param>
        /// <returns></returns>
        public List<HandOutMedicinesrmVO> GetTyypBySrm(string ckbm, string rkbm, string srm)
        {
            var par = new SugarParameter[] {
                new SugarParameter("@srm",string.IsNullOrEmpty(srm)?"":srm.Trim()),
                new SugarParameter("@rkbm", rkbm),
                new SugarParameter("@yfbmCode", Constants.GetCurrentYfbm(OperatorProvider.GetCurrent().UserId).yfbmCode),
                new SugarParameter("@OrganizeId", OperatorProvider.GetCurrent().OrganizeId)
            };
            return FindList<HandOutMedicinesrmVO>(@" EXEC[dbo].[sp_yp_getTyypBySrm] @srm, @rkbm, @YfbmCode, @OrganizeId ", par);
        }

        /// <summary>
        ///  Lấy bản ghi tồn kho thuốc của khoa
        /// </summary>
        /// <param name="ypdm"></param>
        /// <returns></returns>
        public List<MedicinepcInfo> GetpcList(string ypdm)
        {
            var yfbmCode = Constants.CurrentYfbm.yfbmCode;
            var strSql = new StringBuilder(@"
        SELECT  *
        FROM    ( SELECT    ph , pc ,
                            CONVERT(VARCHAR(10), Yxq, 120) Yxq ,
                            FLOOR(kcsl-djsl) Sl
                    FROM    XT_YP_KCXX(NOLOCK)
                    WHERE   Ypdm = @ypCode
                            AND yfbmCode = @yfbmCode
							AND OrganizeId=@OrganizeId
							AND (kcsl-djsl)>0
                    GROUP BY  Ph , pc , Yxq , zhyz, kcsl, djsl
                ) tmp
        WHERE   Sl > 0
        ORDER BY Yxq
");
            SugarParameter[] par = {
                new SugarParameter("@ypCode",ypdm),
                new SugarParameter("@yfbmCode",yfbmCode),
                new SugarParameter("@OrganizeId",Common.Operator.OperatorProvider.GetCurrent().OrganizeId),
            };
            return FindList<MedicinepcInfo>(strSql.ToString(), par);
        }

        /// <summary>
        /// Xuất kho theo đơn lĩnh
        /// </summary>
        /// <param name="xtYpLsNbfymxk"></param>
        /// <param name="fydh"></param>
        /// <param name="fyfs"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public string HandOutMedicineByRequest(Dictionary<string, List<XT_YP_LS_NBFYMXK>> xtYpLsNbfymxk, string fydh, string fyfs, int type)
        {
            //G39-F7: base: dict null/rỗng vẫn NRE ở foreach (8d); key có list null cũng NRE ở .Where.
            //G39-F7b (post probe): NRE gốc đã tìm ra — KHÔNG phải dict null (dict bind đầy đủ) mà là
            //outpar2.Value null ở dòng GetSLFYInfo: proc trả @res='' (chứng minh bằng sqlcmd trực tiếp),
            //nhưng Database.SqlQuery<T> (EFDBBase) không trả output @res về SugarParameter cho call này
            //→ .ToString() NRE. outpar1 (NBFYTH @result) cùng hàm đã null-check sẵn — outpar2 thì không.
            //Fix: null-check outpar2 như outpar1; các guard chẩn đoán tạm đã gỡ.
            if (xtYpLsNbfymxk == null || xtYpLsNbfymxk.Count == 0) return "Thông tin đơn lĩnh không được để trống.";
            #region Xuất kho theo đơn lĩnh
            foreach (var key in xtYpLsNbfymxk.Keys)
            {
                if (xtYpLsNbfymxk[key] == null || xtYpLsNbfymxk[key].Count == 0) return "Thông tin thuốc của đơn lĩnh " + key + " không được để trống.";
                var ckbm = Constants.CurrentYfbm.yfbmCode;
                var organizeid = OperatorProvider.GetCurrent().OrganizeId;
                var usercode = OperatorProvider.GetCurrent().UserCode;
                //G39-F7d: như G30-F2 ở RequestOfReturnMedicine/HandOutMedicine — key là Rkbm (UI: Slbm,
                //mã khoa xin lĩnh); key/ypdm không khớp join #djmx của proc thì NBFYTH vẫn tạo header
                //rỗng + trả rỗng (Success giả — 8a/8g supp3 với key "K1" giả). Chặn ở service trước
                //khi gọi proc; chỉ kiểm item có fysl>0 (đúng tập proc sẽ xử lý).
                foreach (var item in xtYpLsNbfymxk[key].Where(p => p.fysl > 0))
                {
                    if (!IsReturnDetailJoinable(item.Ypdm, key, ckbm, organizeid))
                        return "Không tìm thấy thuốc " + (item.Ypdm ?? "") + " trong danh mục hoặc bộ phận này. Vui lòng kiểm tra lại thông tin thuốc.";
                }

                #region Gọi theo thông tin thuốcYP_XT_GetSLFYInfo Nhận lô，Số lô，Hạn dùng
                //G39-F7c: cột DataTable PHẢI có kiểu (decimal/int) — cột string mặc định làm
                //decimal 0.04 → chuỗi "0,04" theo culture vi-VN của request thread; TVP money
                //nuốt lặng ("0,04"→4.00) còn TVP numeric báo 8114 "nvarchar to numeric" (8a/8g).
                var dt = new DataTable();
                dt.Columns.Add("Ypdm", typeof(string));
                dt.Columns.Add("Sl", typeof(int));
                dt.Columns.Add("zhyz", typeof(int));
                dt.Columns.Add("bzdw", typeof(string));
                dt.Columns.Add("Lsj", typeof(decimal));
                dt.Columns.Add("Pfj", typeof(decimal));
                dt.Columns.Add("sldmxId", typeof(string));
                foreach (var item in xtYpLsNbfymxk[key].Where(p => p.fysl > 0))
                {
                    var row = dt.NewRow();
                    row["Ypdm"] = item.Ypdm;
                    row["Sl"] = item.fysl;
                    row["zhyz"] = item.zhyz;
                    row["bzdw"] = item.bzdw;
                    row["Lsj"] = item.Lsj;
                    row["Pfj"] = item.Pfj;
                    row["sldmxId"] = item.sldmxId;
                    dt.Rows.Add(row);
                }
                var sqlParList2 = new List<SugarParameter>
                {
                    new SugarParameter("@YfbmCode", ckbm),
                    new SugarParameter("@Organizeid", organizeid),
                    new SugarParameter("@OrderDetailsType", dt)
                    {
                        TypeName = "dbo.OrderDetailsType"
                    }
                };
                var outpar2 = new SugarParameter("@res", "") { DbType = System.Data.DbType.String, Size = 100, Direction = ParameterDirection.Output };
                sqlParList2.Add(outpar2);
                var retun = FindList<XT_YP_LS_NBFYMXK>("EXEC dbo.YP_XT_GetSLFYInfo @YfbmCode, @Organizeid, @OrderDetailsType,@res Out", sqlParList2.ToArray());
                //G39-F7b: outpar2.Value có thể null (xem chú thích đầu hàm) — null-check như outpar1,
                //không .ToString() trực tiếp. Value null = không đọc được message của proc, đi tiếp
                //theo retun (rỗng → "Không có thuốc khả dụng"; có dòng → NBFYTH tự kiểm tồn kho).
                if (outpar2.Value != null && !string.IsNullOrWhiteSpace(outpar2.Value.ToString())) return MapReturnMedicineMessage(outpar2.Value.ToString());
                if (retun == null || retun.Count <= 0) return "Không có thuốc khả dụng";
                #endregion
                //G39-F7c: như dt — cột số phải có kiểu, Yxq giữ string (ISO '2025-09-06 00:00:00'
                //chuyển sang datetime của TVP được; đã kiểm chứng bằng sqlcmd trực tiếp).
                var dtt = new DataTable();
                dtt.Columns.Add("Ypdm", typeof(string));
                dtt.Columns.Add("Ph", typeof(string));
                dtt.Columns.Add("pc", typeof(string));
                dtt.Columns.Add("Yxq", typeof(string));
                dtt.Columns.Add("Lsj", typeof(decimal));
                dtt.Columns.Add("Pfj", typeof(decimal));
                dtt.Columns.Add("fysl", typeof(int));
                dtt.Columns.Add("zhyz", typeof(int));
                dtt.Columns.Add("lsje", typeof(decimal));
                dtt.Columns.Add("bzdw", typeof(string));
                dtt.Columns.Add("sldmxId", typeof(string));
                retun.ForEach(p =>
                {
                    var row = dtt.NewRow();
                    row["Ypdm"] = p.Ypdm;
                    row["Ph"] = p.Ph;
                    row["pc"] = p.pc;
                    row["Yxq"] = p.Yxq;
                    row["Lsj"] = p.Lsj;
                    row["Pfj"] = p.Pfj;
                    row["fysl"] = p.fysl;
                    row["zhyz"] = p.zhyz;
                    row["lsje"] = p.lsje;
                    row["bzdw"] = p.bzdw;
                    row["sldmxId"] = p.sldmxId;
                    dtt.Rows.Add(row);
                });
                var param = new List<SugarParameter>
                {
                    new SugarParameter("@Rkbm", key),
                    new SugarParameter("@Ckbm", Constants.CurrentYfbm.yfbmCode),
                    new SugarParameter("@Pdh", fydh),
                    new SugarParameter("@Rksj", DateTime.Now),
                    new SugarParameter("@Cksj", DateTime.Now),
                    new SugarParameter("@Ckczy", usercode),
                    new SugarParameter("@Crkfsdm", fyfs),
                    new SugarParameter("@usercode", OperatorProvider.GetCurrent().UserCode),
                    new SugarParameter("@organizeid", OperatorProvider.GetCurrent().OrganizeId),
                    new SugarParameter("@type", type),
                    new SugarParameter("@XT_YP_LS_NBFYMX", dtt){
                        TypeName = "dbo.XT_YP_LS_NBFYMXK"
                    }
                };
                var outpar1 = new SugarParameter("@result", "") { DbType = System.Data.DbType.AnsiString, Size = 100, Direction = ParameterDirection.Output };
                param.Add(outpar1);
                var proResult = FindList<object>("EXEC [dbo].[YP_XT_NBFYTH] @Rkbm,@Ckbm,@Pdh,@Ckczy,@Crkfsdm,@XT_YP_LS_NBFYMX,@usercode,@organizeid,@type,@result out", param.ToArray());
                if (outpar1.Value == null || string.IsNullOrWhiteSpace(outpar1.Value.ToString()))
                {
                    continue;
                }
                return MapReturnMedicineMessage(outpar1.Value.ToString());
            }
            #endregion
            return "";
        }

        /// <summary>
        /// Trả thuốc
        /// </summary>
        /// <param name="xtYpLsNbfymxk"></param>
        /// <param name="tybm"></param>
        /// <param name="tydh"></param>
        /// <param name="fyfs"></param>
        public string RequestOfReturnMedicine(List<XT_YP_LS_NBFYMXK> xtYpLsNbfymxk, string tybm, string tydh, string fyfs, int type)
        {
            var organizeid = OperatorProvider.GetCurrent().OrganizeId;
            var usercode = OperatorProvider.GetCurrent().UserCode;
            var ckbm = Constants.CurrentYfbm.yfbmCode;
            //G30-F2: list rỗng thì proc vẫn tạo header rỗng + trả rỗng (Success giả) — chặn ở service luôn
            if (xtYpLsNbfymxk == null || xtYpLsNbfymxk.Count == 0) return "Thông tin thuốc trả không được để trống.";
            //G30-F2: kiểm tra từng item có khớp join của proc (#djmx) không — base: item hợp lệ nhưng không có trong catalog/bmypxx vẫn tạo orphan header + Success giả
            foreach (var item in xtYpLsNbfymxk)
            {
                if (!IsReturnDetailJoinable(item.Ypdm, tybm, ckbm, organizeid))
                    return "Không tìm thấy thuốc " + (item.Ypdm ?? "") + " trong danh mục hoặc bộ phận này. Vui lòng kiểm tra lại thông tin thuốc.";
            }
            var param = new List<SugarParameter>
            {
                new SugarParameter("@Rkbm", tybm),
                new SugarParameter("@Ckbm", ckbm),
                new SugarParameter("@Pdh", tydh),
                new SugarParameter("@Ckczy", usercode),
                new SugarParameter("@Crkfsdm", fyfs),
                new SugarParameter("@usercode", usercode),
                new SugarParameter("@organizeid", organizeid),
                new SugarParameter("@type", type),
                new SugarParameter("@XT_YP_LS_NBFYMX", xtYpLsNbfymxk.ToDataTable()){
                    TypeName = "dbo.XT_YP_LS_NBFYMXK"
                }
            };
            var outpar = new SugarParameter("@result", "") { DbType = System.Data.DbType.AnsiString, Size = 100, Direction = ParameterDirection.Output };
            param.Add(outpar);
            var proResult = FindList<object>("EXEC [dbo].[YP_XT_NBFYTH] @Rkbm,@Ckbm,@Pdh,@Ckczy,@Crkfsdm,@XT_YP_LS_NBFYMX,@usercode,@organizeid, @type,@result out", param.ToArray());
            return MapReturnMedicineMessage(outpar.Value.ToString());
        }

        /// <summary>G30-F2: item chỉ vào được #djmx của YP_XT_NBFYTH khi khớp đúng join của proc (catalog + bmypxx cả 2 kho + yfbm hợp lệ). Đọc [NewtouchHIS_Base] theo mẫu có sẵn trong cây (G26 F5) — trên STG nó đọc demo Base.</summary>
        private bool IsReturnDetailJoinable(string ypdm, string rkbm, string ckbm, string organizeid)
        {
            const string sql = @"
SELECT COUNT(1) FROM [NewtouchHIS_Base].dbo.V_C_xt_yp J1
INNER JOIN dbo.xt_yp_bmypxx(NOLOCK) K2 ON K2.Ypdm=J1.ypCode AND K2.OrganizeId=J1.OrganizeId AND K2.zt='1' AND K2.yfbmCode=@Ckbm
INNER JOIN [NewtouchHIS_Base].dbo.V_S_xt_yfbm K2f ON K2f.yfbmCode=K2.yfbmCode AND K2f.OrganizeId=J1.OrganizeId AND K2f.zt='1'
INNER JOIN dbo.xt_yp_bmypxx(NOLOCK) K1 ON K1.Ypdm=J1.ypCode AND K1.OrganizeId=J1.OrganizeId AND K1.zt='1' AND K1.yfbmCode=@Rkbm
INNER JOIN [NewtouchHIS_Base].dbo.V_S_xt_yfbm K1f ON K1f.yfbmCode=K1.yfbmCode AND K1f.OrganizeId=J1.OrganizeId AND K1f.zt='1'
WHERE J1.ypCode=@Ypdm AND J1.zt='1' AND J1.OrganizeId=@OrganizeId";
            var par = new SugarParameter[]
            {
                new SugarParameter("@Ypdm", ypdm ?? ""),
                new SugarParameter("@Rkbm", rkbm ?? ""),
                new SugarParameter("@Ckbm", ckbm ?? ""),
                new SugarParameter("@OrganizeId", organizeid ?? ""),
            };
            return FirstOrDefault<int>(sql, par) > 0;
        }

        /// <summary>G30-F2: dịch message tiếng Trung của proc YP_XT_NBFYTH sang tiếng Việt (giữ tên thuốc trong ngoặc).</summary>
        private static string MapReturnMedicineMessage(string message)
        {
            if (string.IsNullOrWhiteSpace(message)) return message;
            return message
                .Replace("数量必须大于零", "số lượng phải lớn hơn 0")
                .Replace("库存不足", "tồn kho không đủ")
                .Replace("写出入库单据失败", "Ghi phiếu xuất/nhập kho thất bại")
                .Replace("写出入库明细失败", "Ghi chi tiết phiếu xuất/nhập kho thất bại")
                .Replace("！", "!").Replace("，", ",");
        }

        /// <summary>
        /// Phát thuốc(Xuất kho trực tiếp)
        /// </summary>
        /// <param name="xtYpLsNbfymxk"></param>
        /// <param name="lybm"></param>
        /// <param name="fyfs"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public string HandOutMedicine(List<XT_YP_LS_NBFYMXK> xtYpLsNbfymxk, string lybm, string pdh, string fyfs, int type)
        {
            var organizeid = OperatorProvider.GetCurrent().OrganizeId;
            var usercode = OperatorProvider.GetCurrent().UserCode;
            var ckbm = Constants.CurrentYfbm.yfbmCode;
            //G39-F5: base: list rỗng/thiếu trường vẫn tạo orphan header + Success giả (6d);
            //ypdm bogus cũng tạo orphan header (6f) — chặn ở service như G30-F2 ở RequestOfReturnMedicine.
            if (xtYpLsNbfymxk == null || xtYpLsNbfymxk.Count == 0) return "Thông tin thuốc phát không được để trống.";
            foreach (var item in xtYpLsNbfymxk)
            {
                if (!IsReturnDetailJoinable(item.Ypdm, lybm, ckbm, organizeid))
                    return "Không tìm thấy thuốc " + (item.Ypdm ?? "") + " trong danh mục hoặc bộ phận này. Vui lòng kiểm tra lại thông tin thuốc.";
            }
            var param = new List<SugarParameter>
            {
                 new SugarParameter("@Rkbm", lybm),
                 new SugarParameter("@Ckbm", ckbm),
                 new SugarParameter("@Pdh", pdh),
                 new SugarParameter("@Rksj", DateTime.Now),
                 new SugarParameter("@Cksj", DateTime.Now),
                 new SugarParameter("@Ckczy", usercode),
                 new SugarParameter("@Crkfsdm", fyfs),
                 new SugarParameter("@usercode", usercode),
                 new SugarParameter("@organizeid", organizeid),
                 new SugarParameter("@type", type),
                 new SugarParameter("@XT_YP_LS_NBFYMX", xtYpLsNbfymxk.ToDataTable()){
                    TypeName = "dbo.XT_YP_LS_NBFYMXK"
                }
            };
            var outpar = new SugarParameter("@result", "") { DbType = System.Data.DbType.AnsiString, Size = 100, Direction = ParameterDirection.Output };
            param.Add(outpar);
            var proResult = FindList<object>("EXEC [dbo].[YP_XT_NBFYTH] @Rkbm,@Ckbm,@Pdh,@Ckczy,@Crkfsdm,@XT_YP_LS_NBFYMX,@usercode,@organizeid,@type,@result out", param.ToArray());
            //G39-F5: dịch message tiếng Trung của proc sang tiếng Việt (6e/6g trả raw "数量必须大于零！"/"库存不足！").
            return MapReturnMedicineMessage(outpar.Value.ToString());
        }

        /// <summary>
        /// Nhà thuốc phát thuốc cho khoa
        /// </summary>
        /// <param name="xtYpLsNbfymxk"></param>
        /// <param name="lybm"></param>
        /// <param name="fyfs"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public string DispensingMedicineToKs(List<XT_YP_LS_NBFYMXK> xtYpLsNbfymxk, string lybm, string pdh, string fyfs, int type)
        {
            var organizeid = OperatorProvider.GetCurrent().OrganizeId;
            var usercode = OperatorProvider.GetCurrent().UserCode;
            var ckbm = Constants.CurrentYfbm.yfbmCode;
            var param = new List<SugarParameter>
            {
                 new SugarParameter("@Rkbm", lybm),
                 new SugarParameter("@Ckbm", ckbm),
                 new SugarParameter("@Pdh", pdh),
                 new SugarParameter("@Ckczy", usercode),
                 new SugarParameter("@Crkfsdm", fyfs),
                 new SugarParameter("@usercode", usercode),
                 new SugarParameter("@organizeid", organizeid),
                 new SugarParameter("@type", type),
                 new SugarParameter("@XT_YP_LS_NBFYMX", xtYpLsNbfymxk.ToDataTable()){
                    TypeName = "dbo.XT_YP_LS_NBFYMXK"
                }
            };
            var outpar = new SugarParameter("@result", "") { DbType = System.Data.DbType.AnsiString, Size = 100, Direction = ParameterDirection.Output };
            param.Add(outpar);
            var proResult = FindList<object>("EXEC [NewtouchHIS_PDS].[dbo].[YP_XT_YF_KSFY] @Rkbm,@Ckbm,@Pdh,@Ckczy,@Crkfsdm,@XT_YP_LS_NBFYMX,@usercode,@organizeid,@type,@result out", param.ToArray());
            //G39-F6: dịch message proc (phòng khi proc YP_XT_YF_KSFY có lại sau này); hiện proc thiếu nên luôn lỗi ở trên.
            return MapReturnMedicineMessage(outpar.Value.ToString());
        }

        /// <summary>
        /// Lấy thông tin tồn kho thuốc xuất kho hàng loạt
        /// </summary>
        /// <param name="yfbmCode">Bộ phận hiện tại</param>
        /// <param name="rkbm">Bộ phận nhập kho</param>
        /// <param name="organizeid"></param>
        /// <returns></returns>
        public IList<DrugStockInfoVEntity> GetDirectDeliveryDrugsList(Pagination pagination, string yfbmCode, string rkbm, string organizeid)
        {
            var param = new SugarParameter[]
            {
                new SugarParameter("@yfbmCode", yfbmCode),
                new SugarParameter("@rkbm", rkbm),
                new SugarParameter("@Organizeid", organizeid)
            };
            return QueryWithPage<DrugStockInfoVEntity>(TSqlStock.direct_delivery_batch_search, pagination, param);
        }

        /// <summary>
        /// Lấy thông tin tồn kho thuốc xuất kho hàng loạt
        /// </summary>
        /// <param name="yfbmCode">Bộ phận hiện tại</param>
        /// <param name="rkbm">Bộ phận nhập kho</param>
        /// <param name="organizeid"></param>
        /// <returns></returns>
        public IList<DrugStockInfoVEntity> GetDirectDeliveryDrugsList(string yfbmCode, string rkbm, string organizeid)
        {
            var param = new SugarParameter[]
            {
                new SugarParameter("@yfbmCode", yfbmCode),
                new SugarParameter("@rkbm", rkbm),
                new SugarParameter("@Organizeid", organizeid)
            };
            return FindList<DrugStockInfoVEntity>(TSqlStock.direct_delivery_batch_search, param);
        }

        /// <summary>
        /// Hôm trước gửi xuất kho trực tiếp hàng loạt
        /// </summary>
        /// <param name="paraDto"></param>
        /// <returns></returns>
        public string DirectDeliveryBatchSubmit(DirectDeliveryBatchDTO paraDto)
        {
            var param = new SugarParameter[]
            {
                new SugarParameter("@yfbmCode", paraDto.yfbmCode),
                new SugarParameter("@rkbm",  paraDto.rkbm),
                new SugarParameter("@djh",  paraDto.djh),
                new SugarParameter("@crkId", paraDto.crkId),
                new SugarParameter("@djlx", paraDto.djlx),
                new SugarParameter("@userCode", paraDto.userCode),
                new SugarParameter("@crkfs", paraDto.crkfs),
                new SugarParameter("@shzt", paraDto.shzt),
                new SugarParameter("@Organizeid", paraDto.Organizeid),
                new SugarParameter("@ypCodes", paraDto.ypCodes),
            };
            return FindList<string>(TSqlStock.direct_delivery_batch_submit, param).FirstOrDefault();
        }
    }
}
