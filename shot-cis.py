import os
from playwright.sync_api import sync_playwright

OUT = "/home/hatch/workspace/openhis-core/shots-cis"
BASE = "http://127.0.0.1:5234"
PAGES = [
    ("login", "/Login/Index"),
    ("home", "/Home/Index"),
    ("clinicapply", "/ClinicManage/ClinicApply/Index"),
    ("medicine", "/DoctorManage/Medicine/Index"),
    ("inhosquery", "/NurseManage/InpatientList/InHosQueryIndex"),
    ("queueinfo", "/QueueManage/QueueInfo/Index"),
    ("dispensingquery", "/ReportManage/Report/DispensingQuery"),
    ("chat", "/SystemManage/InspectionCategory/Chat"),
    ("prestemplate", "/TemplateManage/PresTemplate/Index"),
    ("performindex", "/OutpatientNurse/PerformIndex"),
    ("examination", "/Apply/Examination"),
]
os.makedirs(OUT, exist_ok=True)
with sync_playwright() as p:
    browser = p.firefox.launch(headless=True)
    ctx = browser.new_context(viewport={"width": 1366, "height": 900})
    page = ctx.new_page()
    for name, path in PAGES:
        try:
            page.goto(BASE + path, wait_until="networkidle", timeout=60000)
            page.wait_for_timeout(1500)
            page.screenshot(path=f"{OUT}/{name}.png")
            print(f"{name}: {page.url} title={page.title()[:60]}", flush=True)
        except Exception as e:
            print(f"{name}: FAILED {str(e)[:200]}", flush=True)
    browser.close()
print("done")
