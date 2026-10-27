"""Base/HOSP fresh screenshots (v2) with cookie-based login (form login flaky)."""
import hashlib, http.cookiejar, os, urllib.request
from playwright.sync_api import sync_playwright

OUT = "/home/hatch/workspace/openhis-core/shots-base"
def login_cookies(base):
    md5 = hashlib.md5(b"000000").hexdigest()
    data = f"username=000000&password={md5}".encode()
    cj = http.cookiejar.CookieJar()
    op = urllib.request.build_opener(urllib.request.HTTPCookieProcessor(cj))
    req = urllib.request.Request(base + "/Login/CheckLogin", data=data)
    op.open(req, timeout=20)
    return [{"name": c.name, "value": c.value, "domain": "127.0.0.1", "path": "/"} for c in cj]

HOSP = [("organize-v2", "/Organize/Index"), ("itemstype-v2", "/Sys/ItemsType/Index"),
        ("sysmedicine-v2", "/SysMedicine/Index"), ("wardroom-v2", "/SysWardRoom/Index"),
        ("chargecategory-v2", "/ChargeCategory/Index"), ("registor-v2", "/AccessManage/Registor/Index"),
        ("producttype-v2", "/ProductManage/ProductType/Index"), ("department-v2", "/Department/Index"),
        ("home-v2", "/Home/Index")]
BASEW = [("baseweb-home-v2", "/Home/Index"), ("baseweb-organize-v2", "/Organize/Index"),
         ("baseweb-itemstype-v2", "/ItemsType/Index"), ("baseweb-duty-v2", "/Duty/Index"),
         ("baseweb-medicineunit-v2", "/SysMedicineUnit/Index"), ("baseweb-rehabitem-v2", "/RehabChargeItem/Index"),
         ("baseweb-diagnosis-v2", "/SysDiagnosis/Index"), ("baseweb-nation-v2", "/SysNation/Index")]

def shoot(base, pages):
    cookies = login_cookies(base)
    print(base, "cookies:", [c["name"] for c in cookies], flush=True)
    with sync_playwright() as p:
        b = p.firefox.launch(headless=True)
        ctx = b.new_context(viewport={"width": 1366, "height": 900})
        ctx.add_cookies(cookies)
        pg = ctx.new_page()
        for name, url in pages:
            try:
                pg.goto(base + url, wait_until="networkidle", timeout=60000)
                pg.wait_for_timeout(2500)
                pg.screenshot(path=f"{OUT}/{name}.png")
                print(f"saved {name}.png url={pg.url}", flush=True)
            except Exception as e:
                print(f"FAILED {name}: {str(e)[:150]}", flush=True)
        b.close()

os.makedirs(OUT, exist_ok=True)
shoot("http://127.0.0.1:5233", HOSP)
shoot("http://127.0.0.1:5238", BASEW)
