"""Sett OutpatientManage staging (5222) verification: login + pages + grid JSON."""
import json
import sys

from playwright.sync_api import sync_playwright

BASE = "http://127.0.0.1:5222"
OUT = "/home/hatch/workspace/openhis-core/sett-stage-outpatient/shots"
import os
os.makedirs(OUT, exist_ok=True)

def shot(page, name):
    page.screenshot(path=f"{OUT}/{name}.png")
    print(f"shot {name}.png url={page.url}", flush=True)

def main():
    with sync_playwright() as p:
        browser = p.firefox.launch(headless=True)
        ctx = browser.new_context(viewport={"width": 1366, "height": 900})
        page = ctx.new_page()

        print("STEP login", flush=True)
        page.goto(BASE + "/Login/Index", wait_until="networkidle", timeout=60000)
        page.fill("#txt_account", "000000")
        page.fill("#txt_password", "000000")
        page.click("#login_button")
        try:
            page.wait_for_url("**/Home/Index", timeout=30000)
        except Exception as e:
            print("no navigation to /Home/Index:", str(e)[:200], flush=True)
        page.wait_for_timeout(2000)
        print("after login url:", page.url, flush=True)
        tips = page.locator(".tips_msg").inner_text() if page.locator(".tips_msg").count() else ""
        if tips:
            print("login message:", tips, flush=True)
        shot(page, "home")

        for name, url in [
            ("reg2018", "/OutpatientManage/OutpatientReg/OutpatientReg2018"),
            ("regsearch", "/OutpatientManage/OutpatientReg/OutpatientRegSearch"),
        ]:
            print(f"STEP {name}: {url}", flush=True)
            resp = page.goto(BASE + url, wait_until="networkidle", timeout=60000)
            print(f"{name} status:", resp.status if resp else None, flush=True)
            page.wait_for_timeout(1500)
            shot(page, name)

        # Grid JSON via authenticated fetch (GET with query params binds Inparameter)
        for label, url in [
            ("reglist-2024-10", "/OutpatientManage/OutpatientReg/GetRegListJson?ksrq=2024-10-01&jsrq=2024-10-31"),
            ("reglist-currentday", "/OutpatientManage/OutpatientReg/GetCurrentDayRegListJson"),
            ("regsched", "/OutpatientManage/OutpatientReg/GetRegScheduleList"),
        ]:
            body = page.evaluate(
                """async (u) => {
                    const r = await fetch(u);
                    return {status: r.status, text: (await r.text()).slice(0, 4000)};
                }""",
                BASE + url,
            )
            print(f"=== {label}: HTTP {body['status']} ===", flush=True)
            print(body["text"][:2000], flush=True)
            with open(f"{OUT}/{label}.json", "w", encoding="utf-8") as f:
                f.write(body["text"])

        browser.close()

if __name__ == "__main__":
    sys.exit(main())
