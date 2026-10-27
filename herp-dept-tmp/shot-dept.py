"""DepartmentManage screenshots (port 5241, Playwright Firefox, viewport only)."""
import os
from playwright.sync_api import sync_playwright

BASE = "http://127.0.0.1:5241"
OUT = "/home/hatch/workspace/openhis-core/shots-herp-dept"
os.makedirs(OUT, exist_ok=True)

with sync_playwright() as p:
    browser = p.firefox.launch(headless=True)
    ctx = browser.new_context(viewport={"width": 1366, "height": 900})
    page = ctx.new_page()
    page.goto(BASE + "/Login/Index", wait_until="networkidle", timeout=60000)
    page.fill("#txt_account", "000000")
    page.fill("#txt_password", "000000")
    page.click("#login_button")
    try:
        page.wait_for_url("**/Home/Index", timeout=30000)
    except Exception as e:
        print("no navigation to /Home/Index:", str(e)[:200])
    page.wait_for_timeout(2000)
    print("after login, url=", page.url)
    page.goto(BASE + "/DepartmentManage/Department/Index", wait_until="networkidle", timeout=60000)
    page.wait_for_timeout(1500)
    page.screenshot(path=OUT + "/dept-index.png")
    print("saved dept-index.png, url=", page.url)
    page.goto(BASE + "/DepartmentManage/Department/Form", wait_until="networkidle", timeout=60000)
    page.wait_for_timeout(1500)
    page.screenshot(path=OUT + "/dept-form.png")
    print("saved dept-form.png, url=", page.url)
    browser.close()
print("done")
