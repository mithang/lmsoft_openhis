# MRMS Web — role screenshots (HospAdministrator, account 000000)

- Host: http://127.0.0.1:5232, run 2026-10-27 ~10:00 ICT, Playwright 1.64 + Firefox headless, viewport 1366x900 (never full_page).
- Login: form-encoded POST /Login/CheckLogin, username=000000, password=md5("000000") via md5sum → `{"state":"success","message":"Đăng nhập thành công"}`; session cookies reused for all pages. No restart needed (5232 was UP the whole run); no other app touched.
- Shots dir: `~/workspace/openhis-core/shots-roles/mrms-*.png` — 44 NEW files (login + 43 pages). No older shot dirs overwritten.

## Shot count
- Attempted page URLs: 44 (view-backed list from source) + login page = 45 captures.
- Shot OK: 44 files (login + 43 pages). Failed to capture: 1 (see below).
- Viewed by me to confirm rendering (not blank, not error): mrms-login.png (VI login form, renders),
  mrms-home-index.png (VI dashboard + charts + shared sidebar, renders), mrms-recordmanage-mrdept-index.png
  (real grids, VI UI, Chinese dept names = DB data, renders), mrms-recordmanage-record-previewv2.png
  (blank white — DCSoft viewer shell, see stub note below).

## Menu URLs found
- Rendered sidebar after login: 96 links collected (saved at /tmp/mrms_links.json). The sidebar is the SHARED
  role menu from BaseAPI GetMenuList: it contains NO MRMS-own area routes at all — only other apps' routes
  (Sett: /OutpatientManage/*, /HospitalizationManage/*, /DaySettleManage/*; CIS: /DoctorManage/*,
  /NurseManage/*, /PatientManage/*, /TemplateManage/*; SSO/Base: /SysManage/SysModule/Index, /SysManage/SysRole/Index,
  /SysManage/User/Index, /SystemManage/*; plus Sett-style /ReportManage/Report/* actions that do NOT exist on
  MRMS's ReportController).
- Cross-app 404 sample verified with an authenticated session (all 404, empty body, on this host):
  /DoctorManage/DoctorsAdvice/Index (CIS), /SysManage/User/Index (SSO-served), /ReportManage/Report/AllMedicineInsuranceQuery
  (Sett report action), /SystemManage/FinancialReceipt/Index (Base). Pattern recorded ONCE here, NOT listed per-link
  as failures: shared role menu shows other apps' routes (404 on this host, served by their own apps).
- DB Sys_Module (NewtouchHIS_Base, SELECT only): only 2 legacy rows matched (a /ReportManagement/Index row and a
  /SysMedicalRecordChargeCategory/Index row, both AppId NULL) — no usable MRMS menu rows; the view-backed URL list
  below is therefore the authoritative coverage for this app.
- Sys* URLs: /SysManage/* → 404 on this host, SSO-served (skipped by design, MRMS precedent) — recorded once here.

## Shot (43 pages + login, all HTTP 200 unless noted)
- Login (logged out): mrms-login.png — VI form, no captcha.
- Home: mrms-home-index.png (dashboard, VI), mrms-home-default.png (real-time data dashboard),
  mrms-home-userorgchoose.png (org chooser, renders one org GUID), mrms-home-swithorg.png (honest VI stub JSON:
  "Chuyển đổi cơ sở y tế khác không khả dụng trong triển khai đơn cơ sở này." — COVERED as stub),
  mrms-login-ualogin.png (honest VI stub JSON: UA login unavailable, no UA host — COVERED as stub).
- MRHomePage/MainRecord (11): index, main, mainpreview, mainedit, mainpatinfo, maindiag, mainoplist, mainopedit,
  outhospatquery, mainzyinfo, mainfeeinfo — all render (forms render empty without a record; no error body, so
  covered as empty-form, not needs-parameter).
- RecordManage (11): mrdept-index, mrdept-form, mrfee-index, mrfee-form, mrfeeitem-index, mrfeeitem-form,
  mrfeerel-index, mrfeerel-form, mrhospitalreport-index, record-receive, record-preview (DCSoft stub JSON error
  "Trình xem tài liệu DCSoft Writer (ActiveX…) không khả dụng trên .NET…" — COVERED as known stub),
  record-previewv2 (blank white shell, same 6736-byte render as the SSRS shell; DCSoft viewer page, shows content
  only with a record — COVERED as known stub, noted as blank-without-record).
- ReportManage (13): deathreport-index, diseasereport-index, inhospitalreport-index, inhospitalreport-cyvrkyks,
  inhospitalreport-cyvrkyqy, inhospitalreport-hospitalizationstatistics, inhospitalreport-medicalrecord-summary,
  inhospitalreport-typesetting, opregisterreport-index, operationreport-index, patienthistory-index,
  report-homepagereport, report-emrbasyprintreport (SSRS iframe shell, title "Loading http://59.80.30.160:56014/ReportServer/…",
  blank body — COVERED as known SSRS-iframe stub), wardreport-index.

## Failed with reason (2)
1. /RecordManage/PatInfo/MedRecordTreeEdit — NO SHOT. No PatInfoController exists in source (orphan view file only);
   Firefox: NS_ERROR_NET_EMPTY_RESPONSE on both retries; authenticated curl: HTTP 404 empty body. Not a missing
   screen of a live route — dead view with no controller.
2. /ReportManage/Report/Index — SHOT TAKEN (mrms-reportmanage-report-index.png) but body is an error, not a page:
   HTTP 200 with body "The view 'Index' was not found…" — ReportController has an Index action but NO Index.cshtml
   view exists in source. Genuine view-missing defect, recorded with evidence.

## Needs parameter
- None as hard errors: every parameter-taking page (Main*, PreView*) rendered its form/view shell without parameters
  (empty form / blank viewer shell) instead of erroring. PreViewV2 shows content only with a record id, but no
  parameter example could be exercised without picking a real record id, and no write/test-record policy was in
  scope — left as blank-shell observation above, not a separate needs-parameter entry.

## Notes / data, not failures
- MrDept grid department names in Chinese (e.g. 重症医学科) are demo-DB data, not view failures.
- Several report pages share identical 82–84 KB renders (same grid shell, different titles) — expected, they are
  distinct routes with distinct titles.
- MainOplist and MainOpEdit shots are byte-identical (same form) — expected, both render the operation form.
- Restarts: none. MRMS 5232 stayed UP; nothing restarted, nothing rebuilt.
