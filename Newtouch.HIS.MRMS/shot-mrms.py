"""MRMS screenshots via local Firefox (Playwright), after login as 000000.
Password comes from env MRMS_PASSWORD (never stored here)."""
import os
import sys

from playwright.sync_api import sync_playwright

OUT = "/home/hatch/workspace/openhis-core/shots-mrms"
BASE = "http://127.0.0.1:5232"


def shot(page, url, name, wait_ms=2500):
    try:
        page.goto(BASE + url, wait_until="networkidle", timeout=60000)
        page.wait_for_timeout(wait_ms)
        page.screenshot(path=f"{OUT}/{name}", full_page=False)
        print(f"saved {name} url={page.url}", flush=True)
    except Exception as e:
        print(f"FAILED {name} ({url}): {str(e)[:200]}", flush=True)


def main():
    os.makedirs(OUT, exist_ok=True)
    pw = os.environ.get("MRMS_PASSWORD", "")
    if not pw:
        print("MRMS_PASSWORD not set", flush=True)
        sys.exit(1)
    with sync_playwright() as p:
        browser = p.firefox.launch(headless=True)
        ctx = browser.new_context(viewport={"width": 1366, "height": 900})
        page = ctx.new_page()
        page.goto(BASE + "/Login/Index", wait_until="networkidle", timeout=60000)
        page.screenshot(path=f"{OUT}/login.png")
        print("saved login.png", flush=True)
        page.fill("#txt_account", "000000")
        page.fill("#txt_password", pw)
        page.click("#login_button")
        try:
            page.wait_for_url("**/Home/Index", timeout=30000)
        except Exception as e:
            print("no navigation to /Home/Index:", str(e)[:200], flush=True)
        page.wait_for_timeout(3000)
        print("after login, url=", page.url, flush=True)
        page.screenshot(path=f"{OUT}/home.png")
        print("saved home.png", flush=True)
        shot(page, "/MRHomePage/MainRecord/Index", "mainrecord-index.png")
        shot(page, "/RecordManage/MrFee/Index", "mrfee-index.png")
        shot(page, "/RecordManage/MrDept/Index", "mrdept-index.png")
        shot(page, "/RecordManage/Record/Receive", "record-receive.png")
        shot(page, "/ReportManage/InHospitalReport/Index", "inhospitalreport-index.png")
        browser.close()


if __name__ == "__main__":
    main()
