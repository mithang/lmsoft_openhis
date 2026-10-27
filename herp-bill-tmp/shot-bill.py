"""Playwright-Firefox screenshots for the Herp BillManage + DeliveryManage port (5240).
Viewport only, never full_page. Login as 000000 (plaintext in the page JS flow,
same as the parent's Herp login flow)."""
import os
from playwright.sync_api import sync_playwright

BASE = "http://127.0.0.1:5240"
OUT = "/home/hatch/workspace/openhis-core/shots-herp-bill"

PAGES = [
    ("apply-query", "/BillManage/ApplyBill/Query"),
    ("bill-query", "/BillManage/OutOrInStorageBill/Query"),
    ("bill-approval", "/BillManage/OutOrInStorageBill/Approval"),
    ("bill-temporary", "/BillManage/OutOrInStorageBill/MyTemporaryCrkdj"),
    ("order-query", "/BillManage/PurchasingOrder/PurchaseOrderQuery"),
    ("order-generating", "/BillManage/PurchasingOrder/GeneratingPurchaseOrder"),
    ("order-audit", "/BillManage/PurchasingOrder/AuditPurchaseOrder"),
    ("order-purchaseview", "/BillManage/PurchasingOrder/PurchaseView"),
    ("plan-query", "/BillManage/PurchasingPlan/PurchasingPlanQuery"),
    ("plan-fill", "/BillManage/PurchasingPlan/FillPurchasingPlan"),
    ("plan-audit", "/BillManage/PurchasingPlan/AuditPurchasingPlan"),
    ("plan-stock", "/BillManage/PurchasingPlan/PurchasingPlan"),
    ("test-index", "/BillManage/Test/Index"),
]

def main():
    os.makedirs(OUT, exist_ok=True)
    with sync_playwright() as p:
        browser = p.firefox.launch(headless=True)
        ctx = browser.new_context(viewport={"width": 1366, "height": 900})
        page = ctx.new_page()
        page.goto(BASE + "/Login/Index", wait_until="networkidle", timeout=60000)
        page.screenshot(path=OUT + "/login.png")
        print("login page shot, url=", page.url, flush=True)
        page.fill("#txt_account", "000000")
        page.fill("#txt_password", "000000")
        page.click("#login_button")
        try:
            page.wait_for_url("**/Home/Index", timeout=30000)
        except Exception as e:
            print("no navigation to /Home/Index:", str(e)[:200], flush=True)
        page.wait_for_timeout(2000)
        print("after login, url=", page.url, flush=True)
        page.screenshot(path=OUT + "/home.png")
        for name, path in PAGES:
            try:
                page.goto(BASE + path, wait_until="networkidle", timeout=60000)
                page.wait_for_timeout(1500)
                page.screenshot(path=OUT + "/" + name + ".png")
                print(f"shot {name}: url={page.url}", flush=True)
            except Exception as e:
                print(f"FAILED {name}: {str(e)[:200]}", flush=True)
        browser.close()

if __name__ == "__main__":
    main()
