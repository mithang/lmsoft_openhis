#!/usr/bin/env python3
"""PDS screenshots after Vietnamese translation pass (Playwright Firefox, 1366x900).
Login as 000000 via the page form (same as MRQC shots), then one shot per Area
plus the main root pages. Grids load via AJAX; networkidle + 2.5s settle.
Output: ~/workspace/openhis-core/shots-pds/"""
import os, sys
sys.path.insert(0, "/home/hatch/workspace/shot")
from playwright.sync_api import sync_playwright

BASE = "http://127.0.0.1:5237"
OUT = "/home/hatch/workspace/openhis-core/shots-pds"
os.makedirs(OUT, exist_ok=True)

PAGES = [
    ("Home/Index", "home.png"),
    ("DrugStorage/HandOutMedicine", "drugstorage-handout.png"),
    ("Medicine/MedicineInfo", "medicine-info.png"),
    ("OutPatientPharmacy/DrugSearch", "outpatient-drugsearch.png"),
    ("HospitalizationPharmacy/DispenseIndex", "hosp-dispense.png"),
    ("PharmacyDrugStorage/ReceiptQueryV2", "receiptquery.png"),
    ("Pharmacy/RequisitionSearchIndex", "requisition.png"),
    ("PharmacyDepartment/Index", "pharmdept.png"),
    ("Dictionaries/FypzIndex", "dictionaries.png"),
    ("Report/PrintReport", "report-print.png"),
    ("BillManage/ApplyBill/ApplyOutStock", "bill-applyoutstock.png"),
    ("BillManage/OutOrInStorageBill/Approval", "bill-approval.png"),
    ("OutOrInStoredManage/Apply/Index", "apply-index.png"),
    ("OutOrInStoredManage/DeliveryDirect/Index", "deliverydirect.png"),
    ("OutOrInStoredManage/InStorage/Index", "instorage.png"),
    ("OutOrInStoredManage/Purchase/Index", "purchase.png"),
    ("PurchaseManage/Purchase/LocationIndex", "location.png"),
    ("ReportManage/YpckQuery/YpckQuery", "ypckquery.png"),
    ("ReportManage/Outpatient/DrugDeliveryStatistics", "drugdelivery-stats.png"),
    ("StorageManage/DrugStockSearch/StockQuery", "stockquery.png"),
]

with sync_playwright() as p:
    b = p.firefox.launch()
    ctx = b.new_context(viewport={"width": 1366, "height": 900})
    page = ctx.new_page()
    page.on("console", lambda m: print(f"  [console {m.type}] {m.text[:160]}", flush=True)
            if m.type == "error" else None)
    page.goto(f"{BASE}/Login/Index", wait_until="networkidle", timeout=60000)
    page.wait_for_timeout(1500)
    page.screenshot(path=f"{OUT}/login.png")
    print("shot login.png", flush=True)
    page.fill("#txt_account", "000000")
    page.fill("#txt_password", "000000")
    page.click("#login_button")
    try:
        page.wait_for_url("**/Home/Index", timeout=30000)
        print("logged in, at", page.url, flush=True)
    except Exception as e:
        print(f"WARN login navigation: {e}; url={page.url}", flush=True)
    for url, name in PAGES:
        try:
            page.goto(f"{BASE}/{url}", wait_until="networkidle", timeout=60000)
            page.wait_for_timeout(2500)
            page.screenshot(path=f"{OUT}/{name}")
            print(f"shot {name} ({page.url})", flush=True)
        except Exception as e:
            print(f"FAIL {name} ({url}): {e}", flush=True)
    b.close()
print("DONE", flush=True)
