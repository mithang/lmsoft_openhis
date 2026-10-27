using Newtouch.Sett.Infrastructure;
using NewtouchHIS.Lib.Framework.Operator;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Newtouch.Sett.Domain
{
    // Real (read-only) service implementations for the verification paths.
    // SQL follows the Framework source; the zybz CASE labels are translated to
    // Vietnamese here (they are code strings, not DB values).

    public class SysConfigRepo : ISysConfigRepo
    {
        private SysConfigEntity Find(string code, string orgId)
        {
            // Sys_Config does not exist in the Sett DB in this deployment, so a
            // straight Sett query throws SqlException ("Invalid object name").
            // Try Sett first, fall back to Base on exception or empty result.
            try
            {
                var e = SettDb.GetConnection().Queryable<SysConfigEntity>()
                    .Where(a => a.Code == code && a.OrganizeId == orgId && a.zt == "1").First();
                if (e != null)
                {
                    return e;
                }
            }
            catch
            {
                // fall through to Base DB
            }
            try
            {
                return SettDb.GetBaseConnection().Queryable<SysConfigEntity>()
                    .Where(a => a.Code == code && a.OrganizeId == orgId && a.zt == "1").First();
            }
            catch
            {
                return null;
            }
        }

        public SysConfigEntity GetByCode(string code, string orgId)
        {
            return Find(code, orgId);
        }

        public string GetValueByCode(string code, string orgId)
        {
            return Find(code, orgId)?.Value ?? "";
        }

        public int GetIntValueByCode(string code, string orgId, int defaultValue = 0)
        {
            var val = GetValueByCode(code, orgId);
            if (int.TryParse(val, out var returnVal))
            {
                return returnVal;
            }
            return defaultValue;
        }

        public bool? GetBoolValueByCode(string code, string orgId, bool? defaultValue = null)
        {
            var val = GetValueByCode(code, orgId);
            if (val == "1" || val == "0")
            {
                return val == "1";
            }
            if (!string.IsNullOrEmpty(val) && bool.TryParse(val, out var returnVal))
            {
                return returnVal;
            }
            return defaultValue;
        }
    }

    public class PatientBasicInfoDmnService : IPatientBasicInfoDmnService
    {
        public List<SysHosBasicInfoVO> GetSysBasicByZHY(string kh, string zyh, string orgid)
        {
            var sql = @"SELECT a.zyh, a.patid,
                (CASE a.zybz WHEN 0 THEN 'Đăng ký nhập viện' WHEN 1 THEN 'Đang ở khoa'
                    WHEN 2 THEN 'Ra khoa' WHEN 3 THEN 'Đã xuất viện' WHEN 9 THEN 'Hủy nhập viện' ELSE '' END) zybz,
                CONVERT(VARCHAR(100), a.ryrq, 23) ryrq, a.rybq, a.xm, a.xb,
                CONVERT(VARCHAR(100), a.csny, 23) csny, a.kh, c.brxzbh, c.brxz, c.brxzmc,
                d.Code ks, d.Name ksmc, e.bqCode bq, e.bqmc, f.cwCode cw, f.cwmc,
                g.zh, g.zhye, h.zdCode, h.icd10, h.zdmc
                FROM zy_brjbxx a
                LEFT JOIN xt_brxz c ON a.brxz = c.brxz AND c.OrganizeId = @OrganizeId
                INNER JOIN [NewtouchHIS_Base]..V_S_Sys_Department d ON a.ks = d.Code AND d.OrganizeId = @OrganizeId
                INNER JOIN [NewtouchHIS_Base]..V_S_xt_bq e ON a.bq = e.bqCode AND e.OrganizeId = @OrganizeId
                LEFT JOIN [NewtouchHIS_Base]..V_S_xt_cw f ON a.cw = f.cwCode AND f.OrganizeId = @OrganizeId
                INNER JOIN xt_brzh g ON a.zyh = g.zyh AND a.patid = g.patid AND g.zhxz = 3 AND g.zt = 1 AND g.OrganizeId = @OrganizeId
                LEFT JOIN [NewtouchHIS_Base]..V_S_xt_zd AS h ON a.ryzd = h.zdId AND (h.OrganizeId = @OrganizeId OR h.OrganizeId = '*')
                WHERE zybz NOT IN (0, 4)";
            var pars = new Dictionary<string, object> { { "OrganizeId", orgid } };
            if (!string.IsNullOrEmpty(zyh))
            {
                sql += " AND a.zyh = @zyh";
                pars["zyh"] = zyh;
            }
            if (!string.IsNullOrWhiteSpace(kh))
            {
                sql += " AND a.kh = @kh";
                pars["kh"] = kh;
            }
            sql += " ORDER BY a.zyh DESC";
            var parArr = pars.Select(kv => new SugarParameter(kv.Key, kv.Value)).ToArray();
            return SettDb.GetConnection().SqlQueryable<SysHosBasicInfoVO>(sql).AddParameters(parArr).ToList();
        }

        public DateTime? IFCQRQISJZSJ(string zyh, string orgId)
        {
            try
            {
                var row = SettDb.GetConnection()
                    .SqlQueryable<CqrqRow>("select cqrq from [Newtouch_CIS]..zy_brxxk where organizeid=@orgId and zyh=@zyh and zt='1'")
                    .AddParameters(new SugarParameter[] { new SugarParameter("orgId", orgId), new SugarParameter("zyh", zyh) })
                    .First();
                return row?.cqrq;
            }
            catch
            {
                // CIS DB is not present in this deployment; callers treat null as "no out-zone date".
                return null;
            }
        }
    }

    public class HospSettApp : IHospSettApp
    {
        public object GetPatHospStatusDetail(string zyh, string kh)
        {
            throw new NotSupportedException("Tra cứu trạng thái结算 nhập viện chưa được chuyển đổi trong giai đoạn này.");
        }

        public HosSettPatClassifyChargePreviewDto GetHospSettPatClassifyChargePreview(string zyh, DateTime expectedcyrq, decimal jsje)
        {
            throw new NotSupportedException("Xem trước phân loại phí结算 nhập viện chưa được chuyển đổi trong giai đoạn này.");
        }

        public IList<HospFeeClassifyWithDLBO> GetHospFeeClassifyWithDLBOList(string zyh)
        {
            // Mirrors Framework HospSettApp: item fees grouped by dl, medicin fees grouped by dl.
            var orgId = OperatorProvider.GetCurrent()?.OrganizeId;
            try
            {
                var items = SettDb.GetConnection().SqlQueryable<HospItemFeeDetailVO>(
                    @"select a.jfbbh, a.dj, isnull(a.fwfdj,0) fwfdj, sl, c.sfxmmc, b.dlCode dl, b.dlmc,
                      a.CreateTime, isnull(a.jmje,0) jmje, isnull(a.jmbl,0) jmbl, a.zfbl, a.zfxz, a.clzhxmbh, c.xnhybdm ybbm
                      from zy_xmjfb a
                      left join [NewtouchHIS_Base]..V_S_xt_sfdl b on a.dl = b.dlCode and b.OrganizeId = @orgId
                      left join [NewtouchHIS_Base]..V_S_xt_sfxm c on a.sfxm = c.sfxmCode and c.OrganizeId = @orgId
                      where a.zyh = @zyh and a.OrganizeId = @orgId")
                    .AddParameters(new SugarParameter[] { new SugarParameter("zyh", zyh), new SugarParameter("orgId", orgId) })
                    .ToList();
                var meds = SettDb.GetConnection().SqlQueryable<HospMedicinFeeDetailVO>(
                    @"select a.jfbbh, a.dj, a.sl, c.ypmc, b.dlCode dl, b.dlmc,
                      a.CreateTime, isnull(a.jmje,0) jmje, isnull(a.jmbl,0) jmbl, a.zfbl, a.zfxz, c.ybbm
                      from zy_ypjfb a
                      left join [NewtouchHIS_Base]..V_S_xt_sfdl b on a.dl = b.dlCode and b.OrganizeId = @orgId
                      left join [NewtouchHIS_Base]..V_S_xt_yp c on a.yp = c.ypCode and c.OrganizeId = @orgId
                      where a.zyh = @zyh and a.OrganizeId = @orgId")
                    .AddParameters(new SugarParameter[] { new SugarParameter("zyh", zyh), new SugarParameter("orgId", orgId) })
                    .ToList();
                var itemBOs = items.GroupBy(p => new { p.dl, p.dlmc }).Select(p => new HospFeeClassifyWithDLBO
                {
                    dl = p.Key.dl, dlmc = p.Key.dlmc,
                    jsje = p.Select(i => (i.fwfdj + i.dj) * i.sl).Sum(),
                    ItemFeeList = p.ToList(),
                });
                var medBOs = meds.GroupBy(p => new { p.dl, p.dlmc }).Select(p => new HospFeeClassifyWithDLBO
                {
                    dl = p.Key.dl, dlmc = p.Key.dlmc,
                    jsje = p.Select(i => i.dj * i.sl).Sum(),
                    MedicinFeeList = p.ToList(),
                });
                return itemBOs.Union(medBOs).ToList();
            }
            catch
            {
                // Fallback when the Base views are unavailable: group raw billing rows by dl.
                var db = SettDb.GetConnection();
                var items = db.Queryable<HospItemBillingEntity>().Where(a => a.zyh == zyh && (orgId == null || a.OrganizeId == orgId)).ToList();
                var meds = db.Queryable<HospDrugBillingEntity>().Where(a => a.zyh == zyh && (orgId == null || a.OrganizeId == orgId)).ToList();
                var itemBOs = items.GroupBy(p => p.dl).Select(p => new HospFeeClassifyWithDLBO
                {
                    dl = p.Key, dlmc = p.Key,
                    jsje = p.Select(i => ((i.fwfdj ?? 0) + i.dj) * i.sl).Sum(),
                });
                var medBOs = meds.GroupBy(p => p.dl).Select(p => new HospFeeClassifyWithDLBO
                {
                    dl = p.Key, dlmc = p.Key,
                    jsje = p.Select(i => i.dj * i.sl).Sum(),
                });
                return itemBOs.Union(medBOs).ToList();
            }
        }

        public SettSaveSuccessResultDto SaveSett(string zyh, DateTime expectedcyrq, string fph, decimal expectedyjjzhye, decimal expectedjsje, decimal expectedzhaoling, string xjzfListStr, decimal shishoukuan)
        {
            throw new NotSupportedException("Lưu结算 nhập viện chưa được chuyển đổi trong giai đoạn này (không ghi DB).");
        }
    }
}

    internal class CqrqRow
    {
        public DateTime? cqrq { get; set; }
    }
