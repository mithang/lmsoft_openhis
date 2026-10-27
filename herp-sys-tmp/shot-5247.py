"""Herp SystemManage notice screenshots (port 5247, local Firefox)."""
import os
from playwright.sync_api import sync_playwright

BASE = "http://127.0.0.1:5247"
OUT = "/home/hatch/workspace/openhis-core/shots-herp-sys"
os.makedirs(OUT, exist_ok=True)

ACTIONS = ["Index", "SysConfig", "SysLog", "SysModule", "SysRole", "SysStaff", "SysUser"]

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
        print("no navigation to /Home/Index:", str(e)[:200], flush=True)
    page.wait_for_timeout(2000)
    print("after login, url=", page.url, flush=True)
    page.screenshot(path=OUT + "/login-home.png")
    for a in ACTIONS:
        page.goto(f"{BASE}/SystemManage/SystemManage/{a}", wait_until="networkidle", timeout=60000)
        page.wait_for_timeout(800)
        page.screenshot(path=f"{OUT}/notice-{a.lower()}.png")
        print(a, "->", page.url, flush=True)
    browser.close()
print("done")
