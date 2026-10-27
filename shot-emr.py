"""EMR authenticated screenshots (Playwright + Firefox, viewport only).
Logs in via the UI as 000000/000000 (no captcha in this deployment:
Is_CheckChkCode=false), then screenshots login + authenticated pages.
Saves into ~/workspace/openhis-core/shots-emr/.
"""
import os
from playwright.sync_api import sync_playwright

OUT = "/home/hatch/workspace/openhis-core/shots-emr"
BASE = "http://127.0.0.1:5235"
PAGES = [
    ("templateedit", "/MedicalRecordManage/MedRecordTemplate/TemplateEdit"),
    ("recordtype", "/SystemManage/Common/RecordType"),
    ("showtab", "/MedicalRecordManage/Blctgl/ShowTab"),
    ("patmedrecordlist", "/MedicalRecordManage/PatInfo/PatMedRecordList"),
    ("medicaltemplate", "/MedicalRecordManage/MedRecordTemplate/MedicalTemplate"),
]


def main():
    os.makedirs(OUT, exist_ok=True)
    with sync_playwright() as p:
        browser = p.firefox.launch(headless=True)
        ctx = browser.new_context(viewport={"width": 1366, "height": 900})
        page = ctx.new_page()
        page.goto(BASE + "/Login/Index", wait_until="networkidle", timeout=60000)
        page.screenshot(path=OUT + "/login.png")
        print("login.png saved, captcha visible:", page.locator("#imgcode").count(), flush=True)
        page.fill("#txt_account", "000000")
        page.fill("#txt_password", "000000")
        page.click("#login_button")
        try:
            page.wait_for_url("**/Home/Index", timeout=30000)
        except Exception as e:
            print("login navigation issue:", str(e)[:300], flush=True)
        page.wait_for_timeout(3000)
        print("after login, url=", page.url, flush=True)
        print("login button text now:", page.locator("#login_button").inner_text()[:80] if page.locator("#login_button").count() else "(left login page)", flush=True)
        for name, path in PAGES:
            try:
                resp = page.goto(BASE + path, wait_until="networkidle", timeout=60000)
                status = resp.status if resp else "?"
                page.wait_for_timeout(2000)
                page.screenshot(path=f"{OUT}/{name}.png")
                print(f"{name}.png saved, HTTP {status}, url={page.url}", flush=True)
            except Exception as e:
                print(f"{name} FAILED:", str(e)[:300], flush=True)
        browser.close()


if __name__ == "__main__":
    main()
