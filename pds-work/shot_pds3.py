import sys
sys.path.insert(0, "/home/hatch/workspace/shot")
from playwright.sync_api import sync_playwright
BASE="http://127.0.0.1:5237"; OUT="/home/hatch/workspace/openhis-core/shots-pds"
with sync_playwright() as p:
    b=p.firefox.launch(); ctx=b.new_context(viewport={"width":1366,"height":900}); page=ctx.new_page()
    page.on("pageerror", lambda e: print(f"  [pageerror] {str(e)[:200]}", flush=True))
    page.goto(f"{BASE}/Login/Index", wait_until="networkidle", timeout=60000)
    page.wait_for_timeout(1200); page.screenshot(path=f"{OUT}/login.png"); print("shot login.png", flush=True)
    page.fill("#txt_account","000000"); page.fill("#txt_password","000000"); page.click("#login_button")
    page.wait_for_url("**/Home/Index", timeout=30000); print("logged in", flush=True)
    for url,name in [("Home/Index","home.png"),("ReportManage/YpckQuery/YpckQuery","ypckquery.png"),("ReportManage/Outpatient/DrugDeliveryStatistics","drugdelivery-stats.png"),("HospitalizationPharmacy/DispenseIndex","hosp-dispense.png")]:
        page.goto(f"{BASE}/{url}", wait_until="domcontentloaded", timeout=60000)
        page.wait_for_timeout(4000); page.screenshot(path=f"{OUT}/{name}"); print(f"shot {name}", flush=True)
    b.close()
print("DONE", flush=True)
