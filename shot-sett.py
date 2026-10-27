#!/usr/bin/env python3
"""Screenshot a Sett (5221) page with a real login session, via Playwright-Firefox.
Usage: python3 shot-sett.py <path> <out.png> [wait_ms]
Session: uses /tmp/sett-cookies.txt (Netscape jar, refreshed by caller via
curl POST /Login/CheckLogin with md5 password). No credentials in this file.
Viewport shot only (full_page crashes on very tall pages).
"""
import sys, os
sys.path.insert(0, "/home/hatch/workspace/shot")
from playwright.sync_api import sync_playwright

path, out = sys.argv[1], sys.argv[2]
wait_ms = int(sys.argv[3]) if len(sys.argv) > 3 else 2500

cookies = []
for line in open("/tmp/sett-cookies.txt", encoding="utf-8"):
    if line.startswith("#") or not line.strip():
        continue
    parts = line.rstrip("\n").split("\t")
    if len(parts) >= 7:
        cookies.append({"name": parts[5], "value": parts[6],
                        "domain": parts[0], "path": parts[2]})

with sync_playwright() as p:
    browser = p.firefox.launch(headless=True)
    ctx = browser.new_context(viewport={"width": 1366, "height": 900})
    ctx.add_cookies(cookies)
    page = ctx.new_page()
    page.goto("http://127.0.0.1:5221" + path, wait_until="networkidle", timeout=60000)
    page.wait_for_timeout(wait_ms)
    page.screenshot(path=out)
    print("saved", out, "url=", page.url, "title=", page.title())
    # visible text sample for verification
    try:
        txt = page.locator("body").inner_text()[:600]
        print("BODY:", txt.replace("\n", " | "))
    except Exception as e:
        print("body read failed:", str(e)[:200])
    browser.close()
