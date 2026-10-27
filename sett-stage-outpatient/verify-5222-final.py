"""Sett OutpatientManage staging (5222) FINAL verification (unauthenticated-safe).

Login is blocked by the sandbox network filter (dotnet dual-mode IPv6 sockets
are redirected to the egress proxy, which denies them); this script records
that failure honestly and verifies everything reachable without login.
"""
import os
import sys

from playwright.sync_api import sync_playwright

BASE = "http://127.0.0.1:5222"
OUT = "/home/hatch/workspace/openhis-core/sett-stage-outpatient/shots"
os.makedirs(OUT, exist_ok=True)


def main():
    with sync_playwright() as p:
        browser = p.firefox.launch(headless=True)
        ctx = browser.new_context(viewport={"width": 1366, "height": 900})
        page = ctx.new_page()

        print("STEP login attempt (expected to fail: sandbox proxy blocks dotnet->BaseAPI)", flush=True)
        page.goto(BASE + "/Login/Index", wait_until="networkidle", timeout=60000)
        page.fill("#txt_account", "000000")
        page.fill("#txt_password", "000000")
        page.click("#login_button")
        page.wait_for_timeout(3000)
        print("after login click url:", page.url, flush=True)
        tips = page.locator(".tips_msg").inner_text() if page.locator(".tips_msg").count() else ""
        print("login message:", tips[:200], flush=True)
        page.screenshot(path=f"{OUT}/login.png")
        print("saved login.png", flush=True)

        for name, url in [
            ("chargeindex", "/OutpatientManage/OutpatCharge/ChargeIndex"),
            ("reg2018", "/OutpatientManage/OutpatientReg/OutpatientReg2018"),
        ]:
            resp = page.goto(BASE + url, wait_until="networkidle", timeout=60000)
            print(f"{name} status:", resp.status if resp else None, flush=True)
            page.wait_for_timeout(1000)
            page.screenshot(path=f"{OUT}/{name}.png")
            print(f"saved {name}.png", flush=True)

        def fetch(method, url, body=None):
            return page.evaluate(
                """async ([m, u, b]) => {
                    const r = await fetch(u, {method: m, body: b,
                        headers: b ? {'Content-Type': 'application/x-www-form-urlencoded'} : {}});
                    return {status: r.status, text: (await r.text()).slice(0, 3000)};
                }""",
                [method, BASE + url, body],
            )

        for label, method, url, body in [
            ("reglist-2024-10", "GET", "/OutpatientManage/OutpatientReg/GetRegListJson?ksrq=2024-10-01&jsrq=2024-10-31", None),
            ("reglist-currentday", "GET", "/OutpatientManage/OutpatientReg/GetCurrentDayRegListJson", None),
            ("regsched-post", "POST", "/OutpatientManage/OutpatientReg/GetRegScheduleList", "keyword=&mzlx="),
        ]:
            r = fetch(method, url, body)
            print(f"=== {label}: HTTP {r['status']} ===", flush=True)
            print(r["text"][:800], flush=True)
            with open(f"{OUT}/{label}.json", "w", encoding="utf-8") as f:
                f.write(r["text"])

        browser.close()


if __name__ == "__main__":
    sys.exit(main())
