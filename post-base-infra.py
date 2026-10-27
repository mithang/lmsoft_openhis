#!/usr/bin/env python3
"""Post-port fix-ups for Base Infra (idempotent): re-applies, after a fresh
port-base-infra.py run, every fix that lives inside PORTED files, so a re-run
reproduces the buildable tree. Hand-written base files (RepositoryBase,
DmnServiceBase, DbTrans, shims, ...) are never touched by either script.
Fixes: using injections, GetTableName()->literal, SitePdsAPIHelper rewrite,
V view entity, PDS DTOs, SysPharmacyDepartmentApp interface using.
"""
import os
import re

SRC = os.path.expanduser("~/workspace/lmsoft_openhis_run/Newtouch.HIS.Base")
DST = os.path.expanduser("~/workspace/openhis-core/Newtouch.HIS.Base/Newtouch.HIS.Base.Infrastructure")
PDS_SRC = os.path.expanduser("~/workspace/lmsoft_openhis_run/Newtouch.HIS.PDS/Newtouch.PDS.Requset/PharmacyDepartment/EmpowermentPharmacyDepartmentRequestDTO.cs")


def read(p):
    return open(p, encoding="utf-8-sig").read().replace("\r\n", "\n")


def inject_usings():
    rules = [
        (r"\bIRepositoryBase\b", "using Newtouch.HIS.Repository;", None),
        (r"\bRepositoryBase<", "using Newtouch.HIS.Repository;", None),
        (r"\bFirstSecond\b", "using Newtouch.HIS.Domain;", None),
        (r"\bDESEncrypt\b", "using NewtouchHIS.Lib.Base.Security;", None),
        (r"\bMd5\b", "using Newtouch.Tools;", "Newtouch.Tools"),
        (r"\bComm\.CreateNo\b", "using Newtouch.Tools;", "Newtouch.Tools"),
        (r"\bAppLogger\b", "using Newtouch.Tools;", "Newtouch.Tools"),
        (r"\bAPIRequestHelper\b", "using Newtouch.Tools;", "Newtouch.Tools"),
        (r"\bConfigurationHelper\b", "using Newtouch.Tools;", "Newtouch.Tools"),
        (r"\bDbTrans\b", "using Newtouch.HIS.DomainServices;", "Newtouch.HIS.DomainServices"),
        (r"\bExtLinq\b", "using Newtouch.Tools;", "Newtouch.Tools"),
        (r"\.Clone\(\)", "using Newtouch.Tools;", "Newtouch.Tools"),
        (r"\.ToInt\(\)", "using Newtouch.Tools;", "Newtouch.Tools"),
        (r"\.ToDate\(\)", "using Newtouch.Tools;", "Newtouch.Tools"),
    ]
    n = 0
    for dp, dns, fns in os.walk(DST):
        dns[:] = [d for d in dns if d not in ("obj", "bin")]
        for f in fns:
            if not f.endswith(".cs"):
                continue
            fp = os.path.join(dp, f)
            ft = open(fp, encoding="utf-8").read()
            mns = re.search(r"namespace\s+([\w\.]+)", ft)
            ns = mns.group(1) if mns else ""
            need = []
            for pat, u, uns in rules:
                if re.search(pat, ft) and u not in ft and (uns is None or (ns != uns and not ns.startswith(uns + "."))):
                    need.append(u)
            if need:
                ins = "".join(u + "\n" for u in dict.fromkeys(need))
                nt = re.sub(r"(using [^;]+;\n)", r"\1" + ins, ft, count=1)
                if nt == ft:
                    nt = ins + ft
                open(fp, "w", encoding="utf-8").write(nt)
                n += 1
    print(f"using injections: {n} files")


def gettable_literals():
    table = {}
    for dp, dns, fns in os.walk(os.path.join(DST, "Domain/Entity")):
        for f in fns:
            if not f.endswith(".cs"):
                continue
            ft = open(os.path.join(dp, f), encoding="utf-8").read()
            m = re.search(r'\[SugarTable\("([^"]+)"\)\]\s*(?:public\s+)?(?:partial\s+)?class\s+(\w+)', ft)
            if m:
                table[m.group(2)] = m.group(1)
    n = 0
    for dp, dns, fns in os.walk(DST):
        dns[:] = [d for d in dns if d not in ("obj", "bin")]
        for f in fns:
            if not f.endswith(".cs"):
                continue
            fp = os.path.join(dp, f)
            ft = open(fp, encoding="utf-8").read()
            nt = re.sub(r"(\w+)\.GetTableName\(\)", lambda m: '"%s"' % table[m.group(1)] if m.group(1) in table else m.group(0), ft)
            if nt != ft:
                open(fp, "w", encoding="utf-8").write(nt)
                n += 1
    print(f"GetTableName literals: {n} files")


def sitepds_rewrite():
    p = os.path.join(DST, "Infrastructure/SitePdsAPIHelper.cs")
    if not os.path.exists(p):
        return
    t = open(p, encoding="utf-8").read()
    if "TokenCacheEntry" in t:
        print("SitePdsAPIHelper: already rewritten")
        return
    t = t.replace("using NewtouchHIS.Lib.Base.Utilities;",
                  "using Newtouch.Tools;\nusing System.Collections.Concurrent;\nusing System.Net.Http;\nusing System.Text;")
    old_cache = re.search(r"            APIRequestHelper\.TokenClass tokenClass;.*?return tokenClass\.Token;", t, re.S)
    if old_cache:
        t = t.replace(old_cache.group(0),
                      "            if (!force && _tokenCache.TryGetValue(key, out var cached)\n"
                      "                && cached.LastRequestTime >= DateTime.Now.AddMinutes(-19))\n"
                      "            {\n                return cached.Token;\n            }\n"
                      "            var tk = GetTokenFromApi();\n"
                      "            if (string.IsNullOrWhiteSpace(tk))\n            {\n                return null;\n            }\n"
                      "            _tokenCache[key] = new TokenCacheEntry { Token = tk, LastRequestTime = DateTime.Now };\n"
                      "            return tk;")
    old_req = re.search(r"        public static T Request<T>\(string url, object req, int tokenExpireReTryTimes = 1\) where T : new\(\)\n        \{\n            return [^;]+;", t)
    if old_req:
        t = t.replace(old_req.group(0),
                      "        private static readonly HttpClient _http = new HttpClient();\n"
                      "        private static readonly ConcurrentDictionary<string, TokenCacheEntry> _tokenCache =\n"
                      "            new ConcurrentDictionary<string, TokenCacheEntry>();\n\n"
                      "        private class TokenCacheEntry\n"
                      "        {\n            public string Token { get; set; }\n            public DateTime LastRequestTime { get; set; }\n        }\n\n"
                      "        public static T Request<T>(string url, object req, int tokenExpireReTryTimes = 1) where T : new()\n"
                      "        {\n"
                      "            var json = Newtonsoft.Json.JsonConvert.SerializeObject(req);\n"
                      "            using var content = new StringContent(json, Encoding.UTF8, \"application/json\");\n"
                      "            var resp = _http.PostAsync(GetPdsApiUrl(url), content).GetAwaiter().GetResult();\n"
                      "            var str = resp.Content.ReadAsStringAsync().GetAwaiter().GetResult();\n"
                      "            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(str);")
    open(p, "w", encoding="utf-8").write(t)
    print("SitePdsAPIHelper rewritten")


def extra_files():
    # V view entity (not in the original Domain csproj, but referenced by the App layer)
    src = read(os.path.join(SRC, "Newtouch.HIS.Domain/Entity/V/SysPharmacyDepartmentVEntity.cs"))
    src = src.replace('[Table("V_S_xt_yfbm")]', '[SugarTable("V_S_xt_yfbm")]')
    src = re.sub(r"(using [^;]+;\n)", r"\1using SqlSugar;\n", src, count=1)
    dst = os.path.join(DST, "Domain/Entity/V/SysPharmacyDepartmentVEntity.cs")
    os.makedirs(os.path.dirname(dst), exist_ok=True)
    open(dst, "w", encoding="utf-8").write(src)
    # PDS DTOs (cross-module request types used by SysPharmacyDepartmentApp)
    src = read(PDS_SRC)
    src = src.replace("using Newtouch.HIS.API.Common;\n", "")
    src = re.sub(r"\s*:\s*RequestBase", "", src)
    src = src.replace("public List<EmpowermentPharmacyDepartment> epds { get; set; }",
                      "public List<EmpowermentPharmacyDepartment> epds { get; set; }\n        public DateTime Timestamp { get; set; }")
    src = src.replace("public string bmypId { get; set; }\n\n        /// <summary>\n        /// 组织机构ID",
                      "public string bmypId { get; set; }\n        public DateTime Timestamp { get; set; }\n\n        /// <summary>\n        /// 组织机构ID", 1)
    dst = os.path.join(DST, "PdsDtos/EmpowermentPharmacyDepartmentRequestDTO.cs")
    os.makedirs(os.path.dirname(dst), exist_ok=True)
    open(dst, "w", encoding="utf-8").write(src)
    # interface file lost its Entity using with the FrameworkBase using drop
    p = os.path.join(DST, "Application/Interface/SystemManage/ISysPharmacyDepartmentApp.cs")
    t = open(p, encoding="utf-8").read()
    if "using Newtouch.HIS.Domain.Entity;" not in t:
        t = "using Newtouch.HIS.Domain.Entity;\n" + t
        open(p, "w", encoding="utf-8").write(t)
    # queryable-Contains fixes: materialize small id lists before Contains-in-Where
    p = os.path.join(DST, "DomainServices/SystemManage/SysModuleDmnService.cs")
    t = open(p, encoding="utf-8").read()
    t = re.sub(r"(\.Select\(p => p\.ItemId\));", r"\1.ToList();", t)
    open(p, "w", encoding="utf-8").write(t)
    p = os.path.join(DST, "DomainServices/SystemManage/UserRoleAuthDmnService.cs")
    t = open(p, encoding="utf-8").read()
    t = t.replace(".Select(p => p.Id);", ".Select(p => p.Id).ToList();")
    open(p, "w", encoding="utf-8").write(t)
    print("extra files + Contains fixes applied")


if __name__ == "__main__":
    inject_usings()
    gettable_literals()
    sitepds_rewrite()
    extra_files()
