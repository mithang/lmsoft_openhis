"""OR screenshots via Playwright+Firefox (same route as shot_ff.py).
Auth context (honest caption): these pages render in a temporary build with
the global FrameworkAuthorizationFilter lifted, because real login is blocked
environment-wide (sandbox gatekeeper intercepts .NET HTTP/Redis). Index pages
still 302 (vendor base-class HandlerAuthorize) even in that build, so shots
use real non-Index pages. Grids are empty without a login identity (org null)
except where noted; data content itself was verified via or-datatest."""
from playwright.sync_api import sync_playwright

BASE = "http://127.0.0.1:5231"
PAGES = [
    ("login", "/Login/Index"),
    ("home", "/Home/Index"),
    ("opapply-card", "/Operation/OpApply/ApplyCard"),
    ("arrangement-detail", "/Operation/OpArrangement/ArrangementDetail"),
    ("proc-detail", "/Operation/OpArrangement/OperationProcDetail"),
    ("report-notice", "/ReportManage/Report/OperationNoticeReport"),
    ("chargetemplate-index", "/SystemManage/SysChargeTemplate/ChargeTemplateIndex"),
    ("chargetemplate-form", "/SystemManage/SysChargeTemplate/ChargeTemplate_EditForm"),
]

with sync_playwright() as p:
    b = p.firefox.launch()
    pg = b.new_page(viewport={"width": 1366, "height": 900})
    for name, path in PAGES:
        try:
            r = pg.goto(BASE + path, wait_until="networkidle", timeout=60000)
            pg.wait_for_timeout(1500)
            pg.screenshot(path=f"/home/hatch/workspace/openhis-core/shots-or/{name}.png")
            print(f"{name}: http={r.status if r else '?'}")
        except Exception as e:
            print(f"{name}: FAILED {type(e).__name__}: {str(e)[:150]}")
    b.close()
print("done")
