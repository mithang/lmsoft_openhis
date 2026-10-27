#!/usr/bin/env python3
import json, os, sys, time
from playwright.sync_api import sync_playwright
BASE = "http://127.0.0.1:5233"
OUT = os.path.expanduser("~/workspace/openhis-core/shots-roles")
RES = os.path.expanduser("~/workspace/openhis-core/bh-results2.tsv")
def main():
    jobs = []
    for line in open(sys.argv[1]):
        line = line.strip()
        if line and not line.startswith("#"):
            jobs.append(tuple(line.split("|", 1)))
    cookies = []
    for c in json.load(open("/tmp/bh-cookies.json")):
        cookies.append({"name": c["name"], "value": c["value"], "domain": "127.0.0.1", "path": "/"})
    with sync_playwright() as p:
        browser = p.firefox.launch(headless=True)
        ctx = browser.new_context(viewport={"width": 1366, "height": 900})
        ctx.add_cookies(cookies)
        page = ctx.new_page()
        with open(RES, "a") as rf:
            for slug, url in jobs:
                out = os.path.join(OUT, f"basehosp-{slug}.png")
                status, title, final = -1, "", ""
                for attempt in range(2):
                    try:
                        resp = page.goto(BASE + url, wait_until="domcontentloaded", timeout=30000)
                        page.wait_for_timeout(2500)
                        status = resp.status if resp else -1
                        title = page.title
                        final = page.url
                        page.screenshot(path=out)
                        break
                    except Exception as e:
                        title = f"ERR attempt{attempt}: {e}"
                        time.sleep(2)
                rf.write(f"{slug}\t{url}\t{status}\t{title}\t{final}\n"); rf.flush()
                print(f"{slug} | {url} | {status} | {title}", flush=True)
        browser.close()
    print("DONE")
if __name__ == "__main__":
    main()
