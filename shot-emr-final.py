"""EMR completion screenshots (fresh pass, new filenames).
Logs in via the EMR login UI as 000000, then captures one page per Area
plus the DCSoft stub page and the new SystemManage notice page.
Viewport shots only (full-page crashes on very tall grids).
"""
import os
from playwright.sync_api import sync_playwright

OUT = "/home/hatch/workspace/openhis-core/shots-emr"
BASE = "http://127.0.0.1:5235"
PAGES = [
    ("emr2-home", "/Home/Index"),
    ("emr2-mrhomepage-main", "/MRHomePage/MRHomePage/Main"),
    ("emr2-patmedrecordlist", "/MedicalRecordManage/PatInfo/PatMedRecordList"),
    ("emr2-medicalrecord-index", "/MedicalRecordManage/MedicalRecord/Index"),
    ("emr2-blys-index", "/MedicalRecordManage/Blys/Index"),
    ("emr2-medrecordtemplate-index", "/MedicalRecordManage/MedRecordTemplate/Index"),
    ("emr2-templateedit-dcsoft", "/MedicalRecordManage/MedRecordTemplate/TemplateEdit"),
    ("emr2-sysuser-notice", "/SystemManage/SysUser/Index"),
    ("emr2-yzpreview", "/SystemManage/YzPreView/Index"),
    ("emr2-nursingrecord-index", "/MedicalRecordManage/NursingRecord/Index"),
]


def main():
    with sync_playwright() as p:
        browser = p.firefox.launch(headless=True)
        ctx = browser.new_context(viewport={"width": 1366, "height": 900})
        page = ctx.new_page()
        page.goto(BASE + "/Login/Index", wait_until="networkidle", timeout=60000)
        page.fill("#txt_account", "000000")
        page.fill("#txt_password", "000000")
        page.click("#login_button")
        page.wait_for_timeout(3000)
        print("after login, url =", page.url, flush=True)
        for name, path in PAGES:
            try:
                resp = page.goto(BASE + path, wait_until="networkidle", timeout=60000)
                status = resp.status if resp else "?"
                page.wait_for_timeout(2000)
                page.screenshot(path=f"{OUT}/{name}.png")
                print(f"{name}: {path} -> HTTP {status}, url={page.url}", flush=True)
            except Exception as e:
                print(f"{name}: FAILED {path} - {str(e)[:200]}", flush=True)
        print("DONE", flush=True)
        browser.close()


if __name__ == "__main__":
    main()
