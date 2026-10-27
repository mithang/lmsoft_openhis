# Base Web (http://127.0.0.1:5238) — screenshot progress

- Worker: screenshot worker, account 000000 only (xzadmin/admin never attempted).
- Login: form-encoded POST /Login/CheckLogin (username=000000, password=md5("000000") via md5sum); cookie login in Playwright+Firefox, viewport 1366x900, never full_page.
- Shots dir: `~/workspace/openhis-core/shots-roles/baseweb-*.png` (NEW files only).
- Viewed ≥2 shots personally: YES — baseweb-home-index.png (Vietnamese dashboard, sidebar, quick tiles), baseweb-sys-organize-index.png (Vietnamese grid, correct empty state), baseweb-pharmacy-medicineformulation-index.png (Vietnamese chrome, real rows; Chinese row values are DB-sourced DATA, not view failures).

## Restarts

- One full runtime restart during the run wiped /tmp (killing run 1 mid-loop after 8 shots) and ALL services (1433/5208/7001/5238/5233/6379/41142 all down). Recovered with `bash ~/workspace/recovery.sh` (restarts only down services); verified 5238 → 200 before resuming. No Base Web bin touched, no other app restarted individually.
- Note: run 1's 8 shots survived (workspace persists); run 2 resumed with skip-existing and finished the rest.

## Menu enumeration (two ways, deduplicated)

1. Rendered sidebar after login (22 anchors on /Home/Index): OWN routes — /ItemsData/Index, /Organize/Index, /SysMedicineUnit/Index, /SysMedicineFormulation/Index, /SysMedicineUsage/Index, /SysMedicineClassification/Index, /Duty/Index, /SysDiagnosis/Index, /SysNationality/Index, /SysNation/Index, /SysMedicineStorageIOMode/Index, /SysTCMSyndrome/Index — plus SHARED-role-menu cross-app routes (see below). Sidebar groups: Quản trị hệ thống, Quản lý từ điển, Tổ chức, Dữ liệu cơ bản.
2. Demo DB Sys_Module (NewtouchHIS_Base, SELECT only, zt=1, 53 rows): only ONE row carries AppId newtouch.base.web — /Home/Notice (which 404s, see failures). All other URL rows point at other apps' routes (BaseHOSP /Module, /Role, /User, /Department, /Staff, /Ward, Sett /Settlement/GzybBaseInfo/MedicalInsuranceCatalogue, etc.) — NOT Base Web menus. Base Web's real menu set is its own controllers/views (20 controllers, 41 view-backed pages), all covered below.

## Sidebar pattern (PDS lesson applies)

Base Web follows the SHARED role menu pattern (like PDS, unlike Herp): the rendered sidebar + home quick tiles show OTHER apps' routes that 404 on this host. Verified sample (curl, authed, empty 404 body — Firefox reports these as NS_ERROR_NET_EMPTY_RESPONSE):
- /Module/Index, /Role/Index, /User/Index (BaseHOSP routes) → 404
- /OutpatientManage/OutpatientAccounting/Index, /HospitalizationManage/BookkeepInHos/Accounting (Sett routes, also home quick tiles "Ghi nợ ngoại trú", "Ghi nợ nội trú", etc.) → 404
These are NOT missing screens of Base Web; they are served by their own apps. Recorded once here, not listed as failures.

## Shot (41 files, all HTTP 200)

- baseweb-login.png — login page, logged-out state (shot FIRST)
- baseweb-home-index.png, baseweb-home-default.png, baseweb-home-about.png
- Sys: baseweb-sys-application-index/form/organizes, baseweb-sys-duty-index/form, baseweb-sys-itemsdata-index/form, baseweb-sys-itemstype-index/form, baseweb-sys-organize-index/form/details, baseweb-sys-globalconfig-index/form, baseweb-sys-nation-index/form, baseweb-sys-nationality-index/form
- Pharmacy: baseweb-pharmacy-medicineclassification-index/form, baseweb-pharmacy-medicineformulation-index/form/correlationusage, baseweb-pharmacy-storageiomode-index/form, baseweb-pharmacy-medicineunit-index/form, baseweb-pharmacy-medicineusage-index/form
- Rehab: baseweb-rehab-chargeclassification-index/form, baseweb-rehab-chargeitem-index/form
- Settlement: baseweb-settlement-sysdiagnosis-index/form, baseweb-settlement-tcmsyndrome-index/form

## Failed with reason (1 own page + 1 stale DB row; all verified by curl, authed)

- /Organize/Applications → HTTP 404 (empty body), 2 Playwright retries + curl. Reason: Views/Organize/Applications.cshtml exists but OrganizeController has NO Applications action (only GetGridJson/GetFormJson/SubmitForm/GetAuthOrganizeList + inherited Index/Form), and no link in Organize/Index.cshtml references it — orphaned view, not a served page. Same class as final-verification's inherited view-not-found triage.
- /Home/Notice → HTTP 404 (empty body). Reason: the single Sys_Module row with AppId newtouch.base.web points here, but HomeController has no Notice action and no Views/Home/Notice.cshtml exists. Stale DB row (BaseHOSP serves Notice on 5233). Not a Base Web page.

## Needs parameter

- None. Every Form/Details/Organizes/CorrelationUsage page loaded without parameters (blank forms / default views, HTTP 200).

## Notes (keeps, not failures)

- Vendor logo/footer proper nouns ("新致医疗云HIS") are keeps.
- DB-sourced row values in Chinese (e.g. medicine formulation names) are DATA, not view failures.
- Empty grids on empty demo tables (e.g. Organize/Index) are CORRECT.
- Headless-Firefox icon-font glyphs render as boxes in some shots (font issue in headless render, not content).
- No Vietnamese stub/notice pages encountered; no error-body pages among own routes.

## Counts

- Shot: 41 (1 login + 3 home + 37 module pages)
- Failed own: 1 (Organize/Applications, orphaned view, 404)
- Failed stale-DB: 1 (Home/Notice, 404)
- Cross-app 404 samples verified: 5 (pattern recorded once, not failures)
- Needs parameter: 0
- Restarts: 1 (full-stack recovery.sh run after runtime restart)
