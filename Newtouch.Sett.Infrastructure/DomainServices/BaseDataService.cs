using Newtouch.Sett.Infrastructure;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;

namespace Newtouch.Sett.DomainServices
{
    /// <summary>
    /// T-12 (minimal BaseData): the Sett port never had a BaseDataController
    /// (the original FrameworkBase binary held it), but 8+ views call
    /// /SystemManage/BaseData/SelectSfxmYp, TherapistAdvice calls
    /// /basedata/GetSfxmZxksSelectJson and HealthInsuranceReport calls
    /// /SystemManage/BaseData/GetAuthedWardSelectJson. This service backs
    /// exactly those three actions with data from the Sett deployment's
    /// databases (Sett proc + Base catalog tables). Nothing else is
    /// implemented here; any further BaseData action is out of scope
    /// until a view calls it.
    /// </summary>
    public class BaseDataDmnService
    {
        /// <summary>Selector row. Property names MUST match the floating
        /// selector colModel names in the calling views (sfxmCode, sfxmmc,
        /// py, sfdlCode, sfdlmc, dw, dj, yzlx, duration, zxks, ...).</summary>
        public class SelectSfxmYpRow
        {
            public string sfxmCode { get; set; }
            public string sfxmmc { get; set; }
            public string py { get; set; }
            public string sfdlCode { get; set; }
            public string sfdlmc { get; set; }
            public string dw { get; set; }
            public decimal? dj { get; set; }
            public string yzlx { get; set; }
            public string bz { get; set; }
            public int? duration { get; set; }
            public string dwjls { get; set; }
            public string jjcl { get; set; }
            public string jldw { get; set; }
            public string jldwzhxs { get; set; }
            public string zfxz { get; set; }
            public string zfbl { get; set; }
            public string cls { get; set; }
            public string gg { get; set; }
            public string ypjxCode { get; set; }
            public string kzbz { get; set; }
            public string zyqzlx { get; set; }
            public string zxks { get; set; }
            public string zxksmc { get; set; }
            public string ybdm { get; set; }
            public string xzyy { get; set; }
            public string xzyysm { get; set; }
            public string mrjl { get; set; }
            public string mrpc { get; set; }
            public string mrpcmc { get; set; }
            public string isKss { get; set; }
            public string jlfwBegin { get; set; }
            public string jlfwEnd { get; set; }
            public string pcfwBegin { get; set; }
            public string pcfwEnd { get; set; }
            public string kssKy { get; set; }
            public string cxjje { get; set; }
        }

        public class ZxksRow
        {
            public string Code { get; set; }
            public string Name { get; set; }
            public string py { get; set; }
        }

        public class WardSelectRow
        {
            public string id { get; set; }
            public string text { get; set; }
        }

        /// <summary>
        /// Charge-item/drug selector. Calls the Sett DB's own stored proc
        /// usp_SelectSfxmYp (same parameter list the original FrameworkBase
        /// service used). The proc returns several result sets; the LAST
        /// one is the selection list (verified: keyword filter and
        /// mzzybz=1/2 both land in the last set).
        /// </summary>
        public List<SelectSfxmYpRow> SelectSfxmYp(string orgId, string keyword, string mzzybz,
            string dllb, string sfdllx, bool containyp0ck, int topCount)
        {
            var pars = new[]
            {
                new SugarParameter("@topCount", topCount),
                new SugarParameter("@orgId", orgId ?? ""),
                new SugarParameter("@mzzybz", mzzybz ?? ""),
                new SugarParameter("@dllb", dllb ?? ""),
                new SugarParameter("@sfdllx", sfdllx ?? ""),
                new SugarParameter("@keyword", keyword ?? ""),
                new SugarParameter("@dlCode", ""),
                new SugarParameter("@isContansChildDl", false),
                new SugarParameter("@useypckflag", false),
                new SugarParameter("@ypyfbmCode", ""),
                new SugarParameter("@containyp0ck", containyp0ck),
                new SugarParameter("@onlyybflag", false),
                new SugarParameter("@isQyKssKZ", false),
                new SugarParameter("@qxjb", ""),
            };
            DataSet ds = SettDb.GetConnection().Ado.GetDataSetAll(
                "EXEC usp_SelectSfxmYp @topCount, @orgId, @mzzybz, @dllb, @sfdllx, @keyword, " +
                "@dlCode, @isContansChildDl, @useypckflag, @ypyfbmCode, @containyp0ck, " +
                "@onlyybflag, @isQyKssKZ, @qxjb", pars);
            var list = new List<SelectSfxmYpRow>();
            if (ds == null || ds.Tables.Count == 0)
            {
                return list;
            }
            DataTable table = ds.Tables[ds.Tables.Count - 1];
            foreach (DataRow row in table.Rows)
            {
                list.Add(new SelectSfxmYpRow
                {
                    sfxmCode = Str(row, "sfxmCode"),
                    sfxmmc = Str(row, "sfxmmc"),
                    py = Str(row, "py"),
                    sfdlCode = Str(row, "sfdlCode"),
                    sfdlmc = Str(row, "sfdlmc"),
                    dw = Str(row, "dw"),
                    dj = Dec(row, "dj"),
                    yzlx = Str(row, "yzlx"),
                    bz = Str(row, "bz"),
                    duration = Int(row, "duration"),
                    dwjls = Str(row, "dwjls"),
                    jjcl = Str(row, "jjcl"),
                    jldw = Str(row, "jldw"),
                    jldwzhxs = Str(row, "jldwzhxs"),
                    zfxz = Str(row, "zfxz"),
                    zfbl = Str(row, "zfbl"),
                    cls = Str(row, "cls"),
                    gg = Str(row, "gg"),
                    ypjxCode = Str(row, "ypjxCode"),
                    kzbz = Str(row, "kzbz"),
                    zyqzlx = Str(row, "zyqzlx"),
                    zxks = Str(row, "zxks"),
                    zxksmc = Str(row, "zxksmc"),
                    ybdm = Str(row, "ybdm"),
                    xzyy = Str(row, "xzyy"),
                    xzyysm = Str(row, "xzyysm"),
                    mrjl = Str(row, "mrjl"),
                    mrpc = Str(row, "mrpc"),
                    mrpcmc = Str(row, "mrpcmc"),
                    isKss = Str(row, "isKss"),
                    jlfwBegin = Str(row, "jlfwBegin"),
                    jlfwEnd = Str(row, "jlfwEnd"),
                    pcfwBegin = Str(row, "pcfwBegin"),
                    pcfwEnd = Str(row, "pcfwEnd"),
                    kssKy = Str(row, "kssKy"),
                    cxjje = Str(row, "cxjje"),
                });
            }
            return list;
        }

        /// <summary>
        /// Executing-department selector (TherapistAdvice). Mirrors the CIS
        /// port's implementation, which ignores sfxmCode and returns all
        /// departments flagged zxks=1: no per-item department mapping data
        /// exists in this deployment (xt_sfxm.zxks is empty on STG), so a
        /// per-item filter would return [] for every item.
        /// </summary>
        public List<ZxksRow> GetSfxmZxksSelectJson(string orgId)
        {
            return SettDb.GetBaseConnection().SqlQueryable<ZxksRow>(
                "select Code, Name, py from Sys_Department " +
                "where zt = '1' and OrganizeId = @orgId and zxks = '1' order by Code")
                .AddParameters(new SugarParameter[] { new SugarParameter("@orgId", orgId ?? "") })
                .ToList();
        }

        /// <summary>
        /// Ward dropdown (HealthInsuranceReport bindSelect expects
        /// {id, text}). "Authed" simplification, documented: the port has
        /// no user-ward authorization table wired, so all active wards of
        /// the org are returned; the page's ward filter works, per-user
        /// restriction is NOT enforced here.
        /// </summary>
        public List<WardSelectRow> GetAuthedWardSelectJson(string orgId)
        {
            return SettDb.GetBaseConnection().SqlQueryable<WardSelectRow>(
                "select bqCode as id, bqmc as text from xt_bq " +
                "where zt = '1' and OrganizeId = @orgId order by bqCode")
                .AddParameters(new SugarParameter[] { new SugarParameter("@orgId", orgId ?? "") })
                .ToList();
        }

        private static string Str(DataRow row, string col)
        {
            if (!row.Table.Columns.Contains(col) || row.IsNull(col))
            {
                return null;
            }
            return Convert.ToString(row[col]);
        }

        private static decimal? Dec(DataRow row, string col)
        {
            string s = Str(row, col);
            return decimal.TryParse(s, out decimal v) ? v : (decimal?)null;
        }

        private static int? Int(DataRow row, string col)
        {
            string s = Str(row, col);
            return int.TryParse(s, out int v) ? v : (int?)null;
        }
    }
}
