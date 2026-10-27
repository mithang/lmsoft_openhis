# Base HOSP Web (5233) — role screenshot progress

- App: http://127.0.0.1:5233, login demo `000000` (form-encoded POST /Login/CheckLogin, md5 via md5sum). xzadmin/admin NOT attempted.
- Tool: Playwright 1.64 + Firefox headless, viewport 1366x900 only, cookies reused per page.
- Shots dir: `~/workspace/openhis-core/shots-roles/basehosp-*.png`
- **Shot count: 148 files** = login + home-index + 146 URL shots (of 148 attempted URLs).
- Viewed by worker (≥2, actually 4): `basehosp-login.png` (VI login form, rendered),
  `basehosp-home-index.png` (empty portal frame — BY DESIGN, see below),
  `basehosp-sysmedicine-index.png` (real grid, 304 rows, VI headers),
  `basehosp-project-projecttype.png` (JSON view-not-found body, faithful).
- Full curl status sweep (authenticated, 150 URLs incl. login+home): 148× HTTP 200, 2× HTTP 404.
  Sweep file: `~/workspace/openhis-core/bh-sweep.tsv`; URL list: `~/workspace/openhis-core/bh-urls.txt`.

## Home / portal frame — BY DESIGN, not a failure

Standalone `/Home/Index` shows an empty portal frame (header "Benh vien Newtouch HIS",
user 000000, "Thao tác tab", empty sidebar/body). Its sidebar comes from
`top.clients.authorizeMenu` supplied by the SSO portal host; the frame is designed to
run inside SSO. Screenshot counts as covered. Rendered home links collected in
Playwright: only `#`, `/Login/OutLogin`, `/User/PersonalInfo`, and the vendor external
link `https://open.tntlinking.com/communityTreaty` — NO sidebar menu links and NO
shared-role-menu cross-app routes on this host. So this app does NOT follow the
shared-role-menu pattern: no cross-app 404 sample was applicable, and none of the
DB menu URLs below belongs to another app (all are this app's own routes).

## Menu URLs found (Sys_Module, NewtouchHIS_Base, SELECT-only) — 41 real + 1 bad row

All 41 real distinct menu URLs shot, all HTTP 200:

- /Home/Notice, /Module/Index, /Role/Index, /ItemsData/Index
- /SysMedicineElectronicPrescription/Index, /MedicineSupplier/Index, /PharmacyWindow/Index
- /SysMedicine/Index, /PharmacyDepartment/Index, /PharmacyDepartment/OpenMedicineIndex
- /PharmacyDrugStorage/SysMedicineStorageIOMode/Index
- /AntibioticType/AntibioticTypeQuerry, /SysWardRoom/Index, /Consult/Index
- /Organize/Index, /Department/Index, /Ward/Index, /SysWardBed/Index
- /Staff/Index, /User/Index
- /Sys/Combination/Index, /Sys/Combination/projectIndex, /Sys/Combination/materialIndex,
  /Sys/Combination/CatalogComparison
- /Settlement/GzybBaseInfo/MedicalInsuranceCatalogue, /ReportManagement/Index
- /PharmacyDrugStorage/SysMedicineUsage/Index, /PharmacyDrugStorage/SysMedicineFormulation/Index
- /SysChargeCategoryTypeRelation/Index, /SysTCMSyndrome/Index, /SysDiagnosis/Index
- /SysMedicalOrderFrequency/Index, /SysAgriInsuranceChargeCategory/Index
- /SysMedicalRecordChargeCategory/Index, /ChargeCategory/Index, /SysChargeItem/Index
- /SysMedicineAuthority/Index, /SysMedicineAuthorityRelation/Index
- /SysMedicineClassification/Index, /SysMedicineFormulation/Index, /SysMedicineUsage/Index

Bad menu-data row (NOT a missing screen): DB row "血液类型基础维护" (blood-type maintenance)
has UrlAddress `/1` → HTTP 404, empty body. No such route exists; menu data error, not a view bug.

## Shot (beyond menus) — all HTTP 200

- Home companions: /Home/Default, /Home/About, /Home/UserOrgChoose
- Form companions (no parameters): Module/Form, Role/Form, ItemsData/Form,
  MedicineSupplier/Form, PharmacyWindow/Form, PharmacyDepartment/Form,
  PharmacyDepartment/OpenMadicineForm, SysMedicineStorageIOMode/Form (area),
  AntibioticTypeSettingForm, SysWardRoom/Form, Consult/NormalForm, Consult/ExpertForm,
  Organize/Form, Department/Form, Ward/Form, SysWardBed/Form, Staff/Form, User/Form,
  ReportManagement/Form, ReportManagement/MianForm, SysMedicineUsage/Form (area),
  SysMedicineFormulation/Form (area), SysMedicineFormulation/CorrelationUsage,
  SysMedicineClassification/Form (area), SysMedicineUnit/Form (area),
  SysChargeCategoryTypeRelation/Form, SysTCMSyndrome/Form, SysDiagnosis/Form,
  SysDiagnosis/Surgery, SysDiagnosis/SurgeryFrom, SysMedicalOrderFrequency/Form,
  SysAgriInsuranceChargeCategory/Form, SysMedicalRecordChargeCategory/Form,
  ChargeCategory/Form, SysChargeItem/Form, SysChargeItem/ybbxblForm,
  SysMedicineAuthority/Form, SysMedicineAuthorityRelation/Form,
  SysChargeClassification/Form, SysMedicine/SysMedicineAdd,
  Settlement/GzybBaseInfo/Index, Settlement/SysDiagnosis/Index,
  Settlement/SysTCMSyndrome/Index + Form, CommonLibrary/Index,
  CommonLibrary/ChargeItemForm, DataMedicalInsurance/Index, Demo/Index, Duty/Index,
  ItemsType/Index + Form, ModuleButton/Index + Form, SysNation/Index,
  SysNationality/Index, RehabChargeClassification/Index + Form,
  RehabChargeItem/Index + Form, RehabChargeItemComparison/Index + Form,
  Sys/Application/Index + Form, Sys/Duty/Index + Form, Sys/ItemsData/Index + Form,
  Sys/ItemsType/Index + Form, Sys/Organize/Index + Form,
  Sys/SysGlobalConfig/Index + Form, Sys/SysNation/Index + Form,
  Sys/SysNationality/Index + Form, ProductManage/ProductType/Index + Form,
  ProductManage/ProductUnit/Index + Form, ProductManage/ProductCrkfs/Index + Form,
  AccessManage/Registor/Index + Form
- Extra companions: Staff/Selector, Staff/Duties, Staff/ConsultInfo, User/Selector,
  User/PersonalInfo, User/Roles, User/CorrelationPharmacy, Module/Details,
  Department/Details, Department/WardInfo

## Failed / no screenshot (2) — with evidence, not stalled on

- /Role/Users — curl HTTP 404, empty body. No `Users` action exists in RoleController
  (view file Views/Role/Users.cshtml exists but no action serves it). Playwright+Firefox
  fails this URL twice with NS_ERROR_NET_EMPTY_RESPONSE (curl gets a clean 404); app
  stays healthy afterwards. Extra companion only, NOT a menu URL.
- /1 — curl HTTP 404, empty body; same Firefox empty-response behaviour. Nonsense
  Sys_Module UrlAddress (see above), not a real route.

## Faithful view-not-found (recorded once each, NOT new bugs)

HTTP 200 + JSON error body "The view '…' was not found", searched locations
/Views/Project/*.cshtml + /Views/Shared/*.cshtml; no such folder in the ORIGINAL tree
either. Screenshots show the JSON error body:

- /Project/ProjectType, /Project/ProjectTypeForm, /Project/ProjectTypeChildForm

## Needs-parameter list

None. Every shot URL loaded without parameters; no page required a parameter to render.

## Notes (not failures)

- Vendor logo/footer proper nouns are keeps (login page, home footer).
- DB-sourced row values in Chinese are DATA (e.g. SysMedicine grid drug names), not view failures.
- Empty grids on empty demo tables are correct where they occur.
- No Vietnamese stub/notice pages were encountered on this app (no explicit stubs to shoot).

## Restarts

- Mid-task VM restart (~02:54 UTC, uptime showed 1 min) killed all services and wiped
  /tmp, losing the first run's results file after 120 shots (shots themselves survived
  in the workspace). Stack restored with `bash ~/workspace/recovery.sh` (restarts only
  down services); 5233 verified 200 afterwards. The 30 tail URLs were then re-shot;
  only the 2 known 404s above failed. No other app was touched or restarted by hand.
- No rebuilds, no DB writes (SELECT only), no git operations.
