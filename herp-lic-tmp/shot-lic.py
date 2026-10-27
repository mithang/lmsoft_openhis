"""LicenceManage screenshots (viewport only) via local Firefox/Playwright.
Password comes from env LIC_PASSWORD (never stored in this file)."""
import os
from playwright.sync_api import sync_playwright

BASE = "http://127.0.0.1:5242"
OUT = "/home/hatch/workspace/openhis-core/shots-herp-lic"

def main():
    with sync_playwright() as p:
        browser = p.firefox.launch(headless=True)
        ctx = browser.new_context(viewport={"width": 1366, "height": 900})
        page = ctx.new_page()
        page.goto(BASE + "/Login/Index", wait_until="networkidle", timeout=60000)
        page.screenshot(path=OUT + "/login.png")
        print("login.png url=", page.url, flush=True)
        page.fill("#txt_account", "000000")
        page.fill("#txt_password", os.environ.get("LIC_PASSWORD", ""))
        page.click("#login_button")
        try:
            page.wait_for_url("**/Home/Index", timeout=30000)
        except Exception as e:
            print("no navigation to /Home/Index:", str(e)[:200], flush=True)
        page.wait_for_timeout(2000)
        print("after login url=", page.url, flush=True)
        page.screenshot(path=OUT + "/home.png")
        for name, path in [
            ("licence-index", "/LicenceManage/Licence/Index"),
            ("licence-type", "/LicenceManage/Licence/LicenceType"),
            ("licence-belonged", "/LicenceManage/Licence/LicenceBelonged"),
            ("licence-form", "/LicenceManage/Licence/Form"),
            ("licence-type-form", "/LicenceManage/Licence/LicenceTypeForm"),
            ("licence-belonged-form", "/LicenceManage/Licence/LicenceBelongedForm"),
        ]:
            page.goto(BASE + path, wait_until="networkidle", timeout=60000)
            page.wait_for_timeout(2500)
            page.screenshot(path=f"{OUT}/{name}.png")
            print(name, "url=", page.url, flush=True)
        browser.close()

main()
