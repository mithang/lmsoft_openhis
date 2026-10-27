"""Base HOSP (5238) screenshots with local Firefox (Playwright).
Login as 000000 via the login form, then shoot session pages (viewport only).
"""
import os
import sys

from playwright.sync_api import sync_playwright

OUT = "/home/hatch/workspace/openhis-core/shots-base"
BASE = "http://127.0.0.1:5238"

PAGES = [
    ("baseweb-login", "/Login/Index", False),
    ("baseweb-home", "/Home/Index", True),
    ("baseweb-organize", "/Organize/Index", True),
    ("baseweb-itemstype", "/ItemsType/Index", True),
    ("baseweb-duty", "/Duty/Index", True),
    ("baseweb-medicineunit", "/SysMedicineUnit/Index", True),
    ("baseweb-rehabitem", "/RehabChargeItem/Index", True),
    ("baseweb-diagnosis", "/SysDiagnosis/Index", True),
    ("baseweb-nation", "/SysNation/Index", True),
]


def main():
    os.makedirs(OUT, exist_ok=True)
    with sync_playwright() as p:
        browser = p.firefox.launch(headless=True)
        ctx = browser.new_context(viewport={"width": 1366, "height": 900})
        page = ctx.new_page()
        for name, url, need_login in PAGES:
            try:
                page.goto(BASE + url, wait_until="networkidle", timeout=60000)
                if name == "baseweb-login":
                    page.screenshot(path=f"{OUT}/{name}.png")
                    print(f"saved {name}.png url={page.url}", flush=True)
                    # log in via the form (same field ids as SSO/MRQC login pages)
                    if page.locator("#txt_account").count() > 0:
                        page.fill("#txt_account", "000000")
                        page.fill("#txt_password", "000000")
                        page.click("#login_button")
                        try:
                            page.wait_for_url("**/Home/Index", timeout=30000)
                        except Exception as e:
                            print("no nav to /Home/Index:", str(e)[:150], flush=True)
                        page.wait_for_timeout(2000)
                        print("after login, url=", page.url, flush=True)
                    continue
                page.wait_for_timeout(2500)
                page.screenshot(path=f"{OUT}/{name}.png")
                print(f"saved {name}.png url={page.url}", flush=True)
            except Exception as e:
                print(f"FAILED {name} ({url}): {str(e)[:200]}", flush=True)
        browser.close()


if __name__ == "__main__":
    main()
