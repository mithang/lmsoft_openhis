"""Base HOSP (5233) screenshots with local Firefox (Playwright).
Login as 000000 via the login form, then shoot session pages (viewport only).
"""
import os
import sys

from playwright.sync_api import sync_playwright

OUT = "/home/hatch/workspace/openhis-core/shots-base"
BASE = "http://127.0.0.1:5233"

PAGES = [
    ("login", "/Login/Index", False),
    ("home", "/Home/Index", True),
    ("organize", "/Organize/Index", True),
    ("itemstype", "/Sys/ItemsType/Index", True),
    ("sysmedicine", "/SysMedicine/Index", True),
    ("wardroom", "/SysWardRoom/Index", True),
    ("chargecategory", "/ChargeCategory/Index", True),
    ("registor", "/AccessManage/Registor/Index", True),
    ("producttype", "/ProductManage/ProductType/Index", True),
    ("department", "/Department/Index", True),
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
                if name == "login":
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
                # Grid pages load rows only after an org is picked from the
                # OrganizeId dropdown (blank org -> service returns null).
                if name in ("sysmedicine", "wardroom"):
                    try:
                        sel = page.locator("#OrganizeId")
                        if sel.count() > 0:
                            # bindSelect fills options via AJAX; wait for them
                            for _ in range(20):
                                n = page.evaluate("document.querySelectorAll('#OrganizeId option').length")
                                if n > 1:
                                    break
                                page.wait_for_timeout(1000)
                            n = page.evaluate("document.querySelectorAll('#OrganizeId option').length")
                            print(f"org options on {name}: {n}", flush=True)
                            if n > 1:
                                sel.select_option("6d5752a7-234a-403e-aa1c-df8b45d3469f")
                                page.wait_for_timeout(3000)
                                print(f"org selected on {name}", flush=True)
                    except Exception as e:
                        print(f"org select skipped on {name}: {str(e)[:120]}", flush=True)
                page.screenshot(path=f"{OUT}/{name}.png")
                print(f"saved {name}.png url={page.url}", flush=True)
            except Exception as e:
                print(f"FAILED {name} ({url}): {str(e)[:200]}", flush=True)
        browser.close()


if __name__ == "__main__":
    main()
