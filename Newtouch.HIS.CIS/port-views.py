import os, re, shutil
SRC = "/home/hatch/workspace/lmsoft_openhis_run/Newtouch.HIS.CIS/Newtouch.CIS.Web"
DST = "/home/hatch/workspace/openhis-core/Newtouch.HIS.CIS/Newtouch.CIS.Web"
STAGED = "/home/hatch/workspace/openhis-core/Newtouch.HIS.CIS/_porting-src/Shared"
copied = 0
for sub in ["Views", "Areas"]:
    for root, dirs, files in os.walk(os.path.join(SRC, sub)):
        # skip App_Start entirely
        if "App_Start" in root: continue
        dirs[:] = [d for d in dirs if d != "App_Start"]
        for fn in files:
            if not fn.endswith(".cshtml"): continue
            src = os.path.join(root, fn)
            rel = os.path.relpath(src, SRC)
            dst = os.path.join(DST, rel)
            os.makedirs(os.path.dirname(dst), exist_ok=True)
            # skeleton files: keep existing Login/Index + Shared/Error + Shared/_FrontReference? No:
            # CIS's own _FrontReference (staged) replaces skeleton's; Login/Index keeps MRQC's for now
            # (rebranded later); Error keeps MRQC's (CIS has no staged Error).
            if rel == "Views/Login/Index.cshtml" or rel == "Views/Shared/Error.cshtml":
                continue
            shutil.copyfile(src, dst)
            copied += 1
print("copied views:", copied)
# staged shared layouts -> Web/Views/Shared (skip Error, already kept)
for fn in os.listdir(STAGED):
    if fn.endswith(".cshtml") and fn != "Error.cshtml":
        shutil.copyfile(os.path.join(STAGED, fn), os.path.join(DST, "Views/Shared", fn))
print("staged shared copied")

VI_NOTICE = """<div class="alert alert-warning" style="margin:20px;padding:15px;border:1px solid #faebcc;background:#fcf8e3;color:#8a6d3b;">
<strong>Thông báo:</strong> Tích hợp LIS ActiveX (điều khiển COM NewtouchActiveX, chỉ chạy trên Internet Explorer + Windows) không thể chạy trên Linux.
Chức năng này cần giải pháp thay thế (SDK của nhà cung cấp / tiện ích trình duyệt / native messaging host) và đã được ghi nhận để triển khai riêng.
Các chức năng khác của trang vẫn hoạt động bình thường.
</div>"""

n_siteurl = n_cfg = n_appid = n_activex = 0
for root, dirs, files in os.walk(DST):
    if "_porting-src" in root: continue
    for fn in files:
        if not fn.endswith(".cshtml"): continue
        p = os.path.join(root, fn)
        with open(p, encoding="utf-8-sig") as f: s = f.read()
        orig = s
        n = s.count("SiteUrl.GetStaticResourceScriptUrl")
        s = s.replace("SiteUrl.GetStaticResourceScriptUrl", "MvcHelper.GetStaticResourceScriptUrl")
        n_siteurl += n
        s = s.replace("Core.Common.Utils.ConfigurationHelper.GetAppConfigValue(", "Newtouch.CIS.Compat.AppConfig.Get(")
        s = s.replace("ConfigurationHelper.GetAppConfigValue(", "Newtouch.CIS.Compat.AppConfig.Get(")
        n_cfg += orig.count("GetAppConfigValue(")
        if "ConstantsBase.AppId" in s:
            s = s.replace("FrameworkBase.MultiOrg.Infrastructure.ConstantsBase.AppId", "Newtouch.CIS.Compat.AppConfig.AppId")
            n_appid += 1
        # jqGrid vi locale after cn locale (layouts only contain the cn line)
        if "grid.locale-cn.js" in s and "grid.locale-vi.js" not in s:
            s = s.replace('grid.locale-cn.js"))</script>',
                          'grid.locale-cn.js"))</script>\n    <script src="/js/jqgrid/grid.locale-vi.js"></script>')
        # cis-vi modal override after framework-ui.js
        if "framework-ui.js" in s and "cis-vi.js" not in s:
            s = s.replace('js/framework-ui.js"))</script>',
                          'js/framework-ui.js"))</script>\n    <script src="/js/cis-vi.js"></script>')
        # ActiveX object blocks -> VI notice
        if "<object" in s and "classid" in s.lower():
            s2 = re.sub(r"<object[^>]*>.*?</object>", VI_NOTICE, s, flags=re.DOTALL | re.IGNORECASE)
            if s2 != s:
                n_activex += 1
                s = s2
        if s != orig:
            with open(p, "w", encoding="utf-8") as f: f.write(s)
print("SiteUrl replaced:", n_siteurl, "| GetAppConfigValue sites:", n_cfg, "| AppId sites:", n_appid, "| ActiveX files:", n_activex)

# _ViewImports for Areas (+ Operator using in both)
vi = "@using NewtouchHIS.Lib.Base.Utilities\n@using NewtouchHIS.Lib.Base\n@using NewtouchHIS.Lib.Framework.Operator\n@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers\n"
for p in [os.path.join(DST, "Views/_ViewImports.cshtml"), os.path.join(DST, "Areas/_ViewImports.cshtml")]:
    with open(p, "w", encoding="utf-8") as f: f.write(vi)
print("ViewImports written")
