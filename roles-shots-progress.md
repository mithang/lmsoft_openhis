# Role screenshots — administrator (000000) — progress

Coordinator file. Workers write per-app files under `shots-roles/<app>-progress.md`;
the coordinator merges the coverage table here at the end.

- Role reality: ONLY demo account 000000 (password 000000) works. xzadmin password
  unknown (do not attempt), admin has no role (do not attempt). So "each role" =
  the one HospAdministrator role across every system.
- Scope: Web UI apps only. API hosts (5320, 5331–5337) and center hosts
  (8015, 8016) have no UI and are EXCLUDED.
- Shots: `shots-roles/<app>-<page>.png`, viewport only, NEW files only.
- Apps: SSO 5208, MRQC 5220, Sett 5221, MRMS 5232, OR 5231, CIS 5234, EMR 5235,
  Herp 5236, Base HOSP 5233, Base Web 5238, PDS 5237.
- Workers run max 2 at a time (RAM).

## Batches — ALL DONE (2026-10-27 ~10:09 ICT)

- Batch 1: MRQC 5220, Sett 5221. Batch 2: CIS 5234, PDS 5237.
- Batch 3: EMR 5235, Herp 5236. Batch 4: OR 5231, MRMS 5232.
- Batch 5: Base HOSP 5233, Base Web 5238. Batch 6: SSO portal 5208.
- Max 2 workers at once throughout. On-disk total verified by coordinator: **1009 PNGs**.
- Restarts: NO worker restarted a healthy app and NO rebuilds happened. Two VM
  restarts hit mid-sweep (Base HOSP + Base Web workers); both recovered via
  `recovery.sh` and re-shot/resumed — shots survive because they live in workspace.

## Coverage table (merged by coordinator; full per-URL lists in shots-roles/<app>-progress.md)

| App | Shots | Failed (no shot possible) | Needs-parameter / error-body shot |
|-----|------:|---------------------------|-----------------------------------|
| SSO 5208 | 17 | 1 page: /SysManage/User/UserRoles renders EMPTY role tree (served-code bug: GetRoleList nodes lack id → treeview throws) | UserRoles standalone (keyValue+orgId); SysModule/Form?keyValue=, SysRole/Form?keyValue= edit variants |
| MRQC 5220 | 11 | 3× 404 SSO-served: /SystemManage/SysModule|SysUser|SysRole/Index | none |
| Sett 5221 | 183 | 43× 404, grouped: ChargeManage not in merged app (6); DRGGroup not ported (3); DischargeSettleDemo/Prescribing/PrescriptionExecution not ported (5); OutpatCharge/IndexDemo (1); HospiterRes Eng variants (2); ReportManage/Highcharts samples live under HighchartsManage (15); Report actions not in merged ReportController (5); SSO-served Sys pages, only User/Index stubbed (6) | none; ReportManage/Report/Index is a NON-ROUTE (guessed URL, JSON view-not-found shot, not missing) |
| MRMS 5232 | 44 | /RecordManage/PatInfo/MedRecordTreeEdit 404 (orphan view, no controller); /ReportManage/Report/Index shot but body = view-not-found error (genuine defect) | none (Main* forms render empty without record, no error) |
| OR 5231 | 48 | 0 | none |
| CIS 5234 | 204 | 16× 404: other apps' menus (Dietary/*, HospitalizationPharmacy/DispenseIndex, MedicalRecordManage/PatInfo/*, OutpatientManage/OutpatientReg/OutpatientReg2018, StorageManage/Storage/DepartmentApply); SSO-served SysModule|SysUser|SysRole|SysLog|SysConfig/Index; /SystemManage/Template/Index; /SystemManage/TTCataloguesComparison/DetailIndex; broken home link /SystemManage/BaseData/Index (no Index action); POST-only 405: /BespeakRegister/CancelYY, /NurseManage/StockCarryOver/CarryOverProduct | /InfusionManage/Query without ?tags=… (JSON error, faithful); /Apply/Test JSON view-not-found (dev-test row); ClinicMetting blank by design (226-byte view); 4 Apply Examination/Inspection pages near-blank standalone (frame-hosted) |
| EMR 5235 | 90 | 1: /MRHomePage/Report/BasyReport → JS-redirect to legacy SSRS ReportViewer.aspx → 404 (EMR's SSRS gap) | PreViewEdit (blid+bllx); MedicalRecord EdiForAdd/PreView/PrintView (record id); MainPreView commented-out, not a page; error bodies shot: MRHomePage/Index, Report/Index, TemplateDesign/Index, OutHosPatQuery (faithful view-not-found) |
| Herp 5236 | 96 | 8: 5 Sys* menu URLs redirect-loop (SysModule|SysRole|SysUser|SysLog|SysConfig/Index → 302 to self; notice pages at /SystemManage/SystemManage/SysX ARE shot — content covered, menu URLs need port fix); /DeliveryManage/Delivery/Index 404 (stale link); /SystemManage/System/Index 404 (no SystemController); /SystemManage/SystemManage/Notice 404 (private helper, not routable) | none |
| Base HOSP 5233 | 148 | 2: /Role/Users 404 (view exists, no action; extra companion, not menu URL); /1 404 (nonsense Sys_Module UrlAddress on blood-type row — bad menu data) | none; faithful view-not-found: /Project/ProjectType{,Form,ChildForm} (no Views/Project/ in original either) |
| Base Web 5238 | 41 | 2: /Organize/Applications 404 (orphaned view, no action); /Home/Notice 404 (stale DB menu row; BaseHOSP serves Notice on 5233) | 0 |
| PDS 5237 | 127 | 8 compile-removed by design (AntibioticTypeQuery, PharmacyWindow/Index, Transfer Apply/Warehousing, SystemManage SysConfig/SysModule/SysRole/SysUser + PersonalInfo link); 3 genuine port gaps: /Medicine/PharmaceuticalStocks, /ReportManage/YpckQuery/DecoctingfeeQuery, /ReportManage/YpckQuery/DoctorsOrderQuery; 38 view files with no route + JSON/text endpoints | examples: DrugStorage/RequestMedicineInfo?sldId=…, OutPatientPharmacy/SearchFyDetail?cfh=…, PharmacyDrugStorage/EndInventory?pdId=…; excluded once: /testPrint/Index (demo, fake patient data) |

Cross-app menu scope (applies to the whole sweep, NOT missing screens): the
rendered sidebar on PDS/MRMS/Base Web (and sampled on others) shows the SHARED
BaseAPI role menu (authorizeMenu), including other apps' routes that 404 on
that host. Those are served by their own apps and covered by their workers.
Herp's sidebar is Herp-only; Base HOSP's home has no sidebar menus (SSO portal
frame). Coverage scope per app = that app's OWN menus (app DB Sys_Module rows +
own rendered links) + Form companions.

Other findings carried to the final report (not shot failures):
- MRQC MedicalCenter grid empty: GetPatList NullReferenceException (live bug).
- MRQC preview blank by design (ViewBag.WriterControlHtml unset on direct GET).
- Sett home IS the MRQC pilot landing page (real content); 2 Sett chart pages
  blank on empty demo data (chart JS throws), 1 partial.
- OR home stub links to non-existent /SystemManage/SysChargeTemplate/Index
  (real page ChargeTemplateIndex shot) — one-line stub-link fix pending decision.
- Herp Sys* redirect-loop menu URLs need a port fix (notice content already shot).
- PDS: 2 Forms return HTTP 200 live though no Form action exists in current
  controller source (running DLL predates source for those actions) — shot, untouched.
- DB/file-sourced Chinese values in grids (dept/drug/diagnosis/surgery names,
  template elements, ICD-10 rows) are DATA, not view failures — DB/enum pass item.
- Login method note: CheckLogin requires FORM-ENCODED POST (JSON-body POST fails).
- Empty grids on empty demo tables are correct everywhere.
- API hosts (5320, 5331–5337) and center hosts (8015, 8016) have no UI — excluded.
- Role limitation: ONLY 000000 (HospAdministrator) usable; xzadmin password
  unknown (never attempted), admin has no role (never attempted).
