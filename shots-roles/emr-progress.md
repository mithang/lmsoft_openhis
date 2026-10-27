# EMR Web (5235) — role screenshot progress

- Date: 2026-10-27. Account: 000000 only (xzadmin/admin NOT attempted).
- Method: local Playwright + Firefox (viewport 1366x900, never full_page), script /tmp/emr_shot.py.
  Login = form-encoded POST /Login/CheckLogin (username=000000, password=md5("000000") via md5sum),
  cookies reused for all pages. Login page shot first in logged-out state.
- App state: EMR 5235 was UP the whole run (PID 4510, Debug build via run-emr.sh). NO restart,
  NO rebuild, no other app touched.
- Shots dir: ~/workspace/openhis-core/shots-roles/emr-*.png
- Shot count: **90 files** = emr-login.png + 89 page shots (all NEW files in shots-roles/).
- Viewed by worker: emr-home-index.png (renders, sidebar + default tab),
  emr-mrhomepage-main.png (real Vietnamese home-page form), emr-report-basyreport.png (Firefox 404 page),
  emr-blys-yslist.png (minimal real view) — 4 viewed, requirement ≥2 met.

## Menu enumeration (two ways, deduplicated)

1. DB (authoritative for EMR): `Newtouch_EMR.dbo.Sys_Module` = 26 rows, **20 distinct non-null URLs** —
   all 20 shot (list under "Menu URLs shot" below).
2. Rendered: `GET /ClientsData/GetModuleDataJson` → `authorizeMenu` (~100 URLs, mostly CROSS-MODULE
   Sett/CIS/Base/Herp routes for role HospAdministrator, e.g. /PatientManage/*, /DoctorManage/*,
   /OutpatientManage/*, /ReportManage/*). Those belong to other apps, NOT to EMR Web, so they are
   NOT counted as EMR pages. EMR-native entries inside it: /MedicalRecordManage/PatInfo/Index,
   /MedicalRecordManage/PatInfo/PatMedRecordList (both shot). Its /SysManage/SysModule/Index etc.
   use the wrong prefix on EMR (EMR area is SystemManage) — verified: /SysManage/SysModule/Index/
   → HTTP 404 on 5235, while /SystemManage/SysModule/Index → 200 notice. Out of EMR scope.

## Menu URLs shot (20/20, all HTTP 200)

- /SystemManage/Common/RecordType (emr-common-recordtype)
- /MedicalRecordManage/MedRecordTemplate/Index (emr-medrecordtemplate-index)
- /MedicalRecordManage/Blys/Index (emr-blys-index)
- /MedicalRecordManage/Outpatient/Index (emr-outpatient-index)
- /MedicalRecordManage/Bljgh/Index (emr-bljgh-index)
- /MedicalRecordManage/MedRecordTemplate/MedicalTemplate (emr-medrecordtemplate-medicaltemplate)
- /MedicalRecordManage/NursingRecord/Index (emr-nursingrecord-index)
- /SystemManage/YzPreView/YzPreView (emr-yzpreview-yzpreview)
- /MedicalRecordManage/PatInfo/PatHistoricalMedRecord (emr-patinfo-pathistoricalmedrecord)
- /MedicalRecordManage/MedicalRules/Index (emr-medicalrules-index)
- /MedicalRecordManage/MedRecordTemplate/MedicalOpenEditor (emr-medrecordtemplate-medicalopeneditor)
- /MedicalRecordManage/PatInfo/Index (emr-patinfo-index; also the Home default tab)
- /MedicalRecordManage/PatInfo/PatMedRecordList (emr-patinfo-patmedrecordlist)
- /MedicalRecordManage/MedRecordTemplate/TemplateEdit — DCSoft-unavailable VI notice (covered)
- /MedicalRecordManage/Blctgl/Index (emr-blctgl-index)
- /SystemManage/SysModule/Index, /SystemManage/SysRole/Index, /SystemManage/SysUser/Index,
  /SystemManage/SysLog/Index, /SystemManage/SysConfig/Index — VI SysManageNotice stubs (covered)

## Rendered real pages shot (HTTP 200, incl. home + companions)

- Home: emr-home-index (/Home/Index). Sidebar renders cross-module menu groups in Vietnamese;
  icon glyphs show as missing-glyph boxes (static font), default tab showed a Chinese loading
  spinner ("数据加载中，请稍后...") at capture time — the tab itself (PatInfo/Index) is shot separately.
- MRHomePage: Main (MUST — real Vietnamese form, verified visually), MainPatInfo, MainDiag,
  MainOplist, MainOpEdit, MainZyInfo, MainFeeInfo, MainFYInfo.
- Common: RecordTypeForm, RecordTypeChildForm. Bljgh: BlmlAdd, Form. Blys: Form, YSList (minimal
  real view: one "Tên phần tử" input — viewed, not blank).
- MedRecordTemplate: FormQX, LeftMedicalTemplate, RightMedicalTemplate, BlTemplate (DCSoft init
  message incl. ServicePageUrl localhost:7027), Form, testbl (minimal test view).
- MedicalRecord: Index, RecordList, Doctor, YZForm, MergeView (VI DCSoft notice), Edit (VI DCSoft
  notice), EdiForAddV2, EditV2.
- Blctgl: CTGLList, YXFHlist, ShowTab, DiagList, ShowDiagList, Form.
- NursingRecord: SSForm, OpenOutForm, OpenPutForm, Form.
- PatInfo: CommitShow (minimal view), MedicalApplySend, MrqcAdd, MedRecordTreeEdit, MedRecordTreeEditV2.
- TemplateDesign: HljlGraph. MedicalRules: Form.
- Sys notices (all render the VI SysManageNotice view — covered): SysModule/Form, SysRole/Form,
  SysConfig/Form, SysConfig/TemplateIndex, SysConfig/TemplateForm, SysUser/PersonalInfo,
  SysUser/Roles, SysUser/Selector, SysStaff/Selector.
- DCSoft JSON stubs (VI message, covered): MedRecordTemplate/HandelDCWriterServicePage,
  MedRecordTemplate/MoreHandleDCWriterServicePage, MedicalRecord/HandleDCWriterServicePage,
  YzPreView/MoreHandleDCWriterServicePage.

## Error bodies shot (HTTP 200 with JSON error body — recorded, not counted as rendered pages)

- /MRHomePage/MRHomePage/Index → "The view 'Index' was not found" (faithful, no such view).
- /MRHomePage/Report/Index → "The view 'Index' was not found" (faithful, no such view).
- /MedicalRecordManage/TemplateDesign/Index → "The view 'Index' was not found" (faithful).
- /MRHomePage/MRHomePage/OutHosPatQuery → "The view 'OutHosPatQuery' was not found" (faithful).

## Failed (1)

- /MRHomePage/Report/BasyReport (emr-report-basyreport.png = Firefox 404 page, kept as evidence):
  initial HTML IS served (curl: HTTP 200, 5303 bytes, title "Trang đầu bệnh án"), but the page
  JS-redirects to /MRHomePage/Report/Pages/ReportViewer.aspx?%2fNewtouch.EMR.Report%2f...&rs:Command=Render&orgId=...,
  a legacy SSRS viewer that does NOT exist in the .NET Core port → 404. Retried 3x in Playwright
  (load timeout, then 404 on redirect target). This is the one remaining SSRS gap for EMR, not a
  stub page. No fix attempted (out of screenshot scope).

## Needs parameter (not failures; not shot with a record)

- /MedicalRecordManage/MedicalRecord/PreViewEdit — needs blid + bllx (known from brief).
- /MedicalRecordManage/MedicalRecord/MedicalRecordEdiForAdd — NullReference JSON without a record id.
- /MedicalRecordManage/MedicalRecord/PreView — NullReference JSON without a record id.
- /MedicalRecordManage/MedicalRecord/PrintView — NullReference JSON without a record id.
- /MRHomePage/MRHomePage/MainPreView — crawler artifact, commented out in source (not a page).

## Data endpoints, not pages (shots exist but they are JSON actions)

- /MRHomePage/MRHomePage/SettlementQuery → returns `[]` (empty demo data, correct).
- /MedicalRecordManage/Outpatient/PatList → without mjzbz: SQL error JSON ("Incorrect syntax
  near '1'"); with ?mjzbz=1 (verified via curl): real patient rows. Needs mjzbz, not a page failure.

## Notes (not failures)

- Empty grids on empty demo tables are correct (e.g. PatInfo/Index "Tìm thấy Dòng bản ghi").
- DB-sourced Chinese remains visible in template-element lists, ICD-10/diagnosis rows and some
  template names — data-file/DB content, not view failures (per brief: File/DataSourceDescriptor.xml).
- MRHomePage/Index and Report/Index view-not-found errors are faithful to the original (no views).
- Restarts: NONE. EMR stayed up; recovery.sh NOT run.
