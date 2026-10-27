import sys
sys.path.insert(0, "/home/hatch/workspace/shot")
from playwright.sync_api import sync_playwright
BASE="http://127.0.0.1:5237"; OUT="/home/hatch/workspace/openhis-core/shots-pds"
with sync_playwright() as p:
    b=p.firefox.launch(); ctx=b.new_context(viewport={"width":1366,"height":900}); page=ctx.new_page()
    page.on("console", lambda m: print(f"  [console {m.type}] {m.text[:200]}", flush=True) if m.type=="error" else None)
    page.on("pageerror", lambda e: print(f"  [pageerror] {str(e)[:300]}", flush=True))
    page.goto(f"{BASE}/Login/Index", wait_until="networkidle", timeout=60000)
    page.fill("#txt_account","000000"); page.fill("#txt_password","000000"); page.click("#login_button")
    try:
        page.wait_for_url("**/Home/Index", timeout=30000); print("logged in", page.url, flush=True)
    except Exception as e: print("login nav warn:", str(e)[:200], "url=", page.url, flush=True)
    for url,name in [("Home/Index","home.png"),("HospitalizationPharmacy/DispenseIndex","hosp-dispense.png")]:
        try:
            page.goto(f"{BASE}/{url}", wait_until="domcontentloaded", timeout=60000)
            page.wait_for_timeout(4000)
            page.screenshot(path=f"{OUT}/{name}")
            print(f"shot {name} url={page.url} title={page.title()}", flush=True)
        except Exception as e:
            print(f"FAIL {name}: {e}", flush=True)
    b.close()
print("DONE", flush=True)
