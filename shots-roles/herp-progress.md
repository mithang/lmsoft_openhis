# Herp Web (127.0.0.1:5236) — role screenshots, 2026-10-27

Login: demo account 000000 only (form-encoded POST /Login/CheckLogin,
password = md5("000000") computed with md5sum). xzadmin/admin NOT attempted.
Tool: own script /tmp/shot_herp.py, Playwright 1.64 + Firefox headless,
viewport 1366x900 only (never full_page). No rebuilds, no restarts, no DB writes.

## Counts

- Shots in ~/workspace/openhis-core/shots-roles/herp-*.png: **96 files**
  (login + home + 94 page shots; 1 file shared between forms/extras lists)
- Menu URLs attempted: 65 (61 DB Sys_Module rows with zt=1 + non-empty
  UrlAddress, + 4 rendered-sidebar area entries not in DB rows) → **58 shot (200)**
- Form companions attempted: 30 → **29 shot (200)**
- Equivalent/notice URLs attempted: 8 → **8 shot (200)**
- Failed with reason: **7 menu URLs + 1 form URL** (below)
- Needs-parameter: **none found** — every Form companion loaded without parameters.
  Pages needing parameters (detail views with ids) were not in scope.
- Restarts: **none** — Herp 5236 was UP the whole run (PID from recovery launch).
- Viewed by me: herp-login.png, herp-home-index.png,
  herp-PurchaseManage-Purchase-Index.png,
  herp-SystemManage-SystemManage-SysModule.png — all rendered, not blank,
  not error pages. Empty grids ("Không tìm thấy dữ liệu liên quan!") are
  correct on the empty demo DB, not failures.

## Enumeration sources (deduplicated)

1. Rendered home sidebar (13 links, all Herp's own area entries — NO shared
   BaseAPI cross-app role menu on this host, so no cross-app 404 pattern to
   sample; the PDS lesson does not apply here):
   /WarehouseManage/Warehouse/Index, /BillManage/ApplyBill/Index,
   /DeliveryManage/Delivery/Index, /DepartmentManage/Department/Index,
   /LicenceManage/Licence/Index, /ProductManage/Product/Index,
   /PurchaseManage/Purchase/Index, /StorageManage/Storage/Index,
   /SupplierManage/Supplier/Index, /SystemManage/System/Index,
   /Report/Index, /Home/KfChange, /Login/OutLogin.
2. DB Sys_Module (NewtouchHIS_herp, SELECT only, zt=1, UrlAddress non-empty):
   61 distinct URLs across 10 areas (BillManage, DeliveryManage,
   DepartmentManage, LicenceManage, ProductManage, PurchaseManage,
   StorageManage, SupplierManage, SystemManage, WarehouseManage).

## Menu URLs shot (58, all HTTP 200)

- BillManage: ApplyBill/Query, ApplyBill/Index (sidebar), OutOrInStorageBill/Approval,
  OutOrInStorageBill/MyTemporaryCrkdj, OutOrInStorageBill/Query,
  PurchasingOrder/AuditPurchaseOrder, PurchasingOrder/GeneratingPurchaseOrder,
  PurchasingOrder/PurchaseOrderQuery, PurchasingPlan/AuditPurchasingPlan,
  PurchasingPlan/FillPurchasingPlan, PurchasingPlan/PurchasingPlan,
  PurchasingPlan/PurchasingPlanQuery
- LicenceManage: Licence/Index, Licence/LicenceBelonged, Licence/LicenceType
- ProductManage: PriceAdjustment/PriceAdjustmentApproval,
  PriceAdjustment/PriceAdjustmentHistory,
  PriceAdjustment/PriceAdjustmentProfitAndLossQuery,
  PriceAdjustment/PriceAdjustmentRequest, Product/Index, ProductCrkfs/Index,
  ProductSfxm/Index, ProductType/Index, ProductUnit/Index
- PurchaseManage: Purchase/Bill, Purchase/BillPay, Purchase/Delivery,
  Purchase/Index, Purchase/LocationIndex, Purchase/QueryYY151,
  Purchase/QueryYY152, Purchase/QueryYY154, Purchase/QueryYY157,
  Purchase/QueryYY159, Purchase/QueryYY160, Purchase/QueryYY161,
  Purchase/QueryYY162, Purchase/QueryYY164, Purchase/ReIndex
- StorageManage: PSIStatistics/Index, ProfitAndLoss/Index,
  ProfitAndLoss/QueryProfitAndLoss, ProfitAndLoss/Syyy,
  StockCarryOver/Index, StockInventory/Index, Storage/ApplyOutStock,
  Storage/DeliveryOfReturnInlineEdit, Storage/DeliveryToDepartmentInlineEdit,
  Storage/DepartmentApply, Storage/DirectDeliveryInlineEdit,
  Storage/ExpiredStorageQuery, Storage/InStorageInlineEdit,
  Storage/OutStorageInlineEdit, Storage/WarehouseStorageQuery,
  Storage/Index (sidebar)
- SupplierManage: Supplier/Index
- WarehouseManage: Warehouse/Index, Warehouse/WarehouseProductQuery

## Form companions shot (29, all HTTP 200)

Department/Form, Department/Index, Licence/Form, Licence/LicenceTypeForm,
Licence/LicenceBelongedForm, Product/Form, ProductCrkfs/Form,
ProductSfxm/Form, ProductType/Form, ProductUnit/Form, Supplier/Form,
Warehouse/Form, Warehouse/ProductUnit, Warehouse/SynceProduct, Purchase/Form,
Purchase/ReForm, Purchase/BillForm, Purchase/DeliveryForm, Purchase/BillPayForm,
Purchase/LocationForm, ProfitAndLoss/SyyyForm, StockInventory/InventoryAction,
StockInventory/InventoryQuery, PurchasingOrder/PurchaseView,
PurchasingOrder/PurchaseDetailView, Home/KfChange, Home/About,
SystemManage/SystemManage/Index, Report/Index (SSRS notice page).

## Equivalent/notice URLs shot (8, all HTTP 200) — cover the failed Sys* content

SystemManage/SystemManage/Index (already in forms),
SystemManage/SystemManage/SysModule, SysRole, SysUser, SysLog, SysConfig,
SysStaff (VI notices: system-management functions are served by SSO),
DeliveryManage/DeliveryQuery/Index (the real DeliveryManage controller;
sidebar's /DeliveryManage/Delivery/Index does not exist).

## Failed with reason (8)

1. /SystemManage/SysModule/Index, /SystemManage/SysRole/Index,
   /SystemManage/SysUser/Index, /SystemManage/SysLog/Index,
   /SystemManage/SysConfig/Index — REDIRECT LOOP (Playwright
   NS_ERROR_REDIRECT_LOOP; curl: 302 to /SystemManage/SysModule etc.,
   which 302s to itself). Root cause (read-only inspection, NOT fixed —
   fixing means rebuilding over a running app): Sys*Controller.Index()
   does RedirectToAction("<SysX>","SystemManage"), which resolves to
   area=SystemManage, controller=SysX, action=Index (itself) instead of
   the intended notice action at /SystemManage/SystemManage/SysX.
   The intended VI notice pages ARE shot (see equivalents above), so the
   content is covered; only the DB menu URL itself loops. Follow-up fix:
   redirect to action "SystemManage" controller route or return the
   notice view directly.
2. /DeliveryManage/Delivery/Index — HTTP 404, empty body. No
   DeliveryController exists in this area (only DeliveryQueryController).
   Stale sidebar link; real page /DeliveryManage/DeliveryQuery/Index shot.
3. /SystemManage/System/Index — HTTP 404, empty body. No SystemController
   exists (controller is SystemManageController). Stale sidebar link;
   real page /SystemManage/SystemManage/Index shot.
4. /SystemManage/SystemManage/Notice — HTTP 404. Not a routable page:
   Notice(title, originalUrl, description) is a private helper of
   SystemManageController, not an action. Not a missing screen.

## Notes (not failures)

- Login page renders Vietnamese, but vendor logo/footer Chinese text
  (天开开源 / copyright line) is vendor branding in the static login page,
  not a view failure.
- Home dashboard renders Vietnamese with 0 counts (empty demo DB).
- Known honest stubs were NOT re-triggered via clicks (no writes):
  SSRS Report/Index notice page shot; WCF/ProcessorFun error paths not
  exercised (would require submitting procurement/bill actions).
- Enum receipt-number prefixes (protected) not treated as failures.
- No cross-app 404 pattern: home sidebar contains only Herp routes.
