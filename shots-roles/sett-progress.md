# Sett merged Web (5221) — role screenshot progress

- App: http://127.0.0.1:5221, login demo 000000 (cookie login via POST /Login/CheckLogin, md5 via `md5sum`)
- Tool: local Playwright 1.64 + Firefox headless, viewport 1366x900, viewport shots only (never full_page)
- Script: /tmp/sett-shot.py (+ /tmp/sett-reshoot.py for blank-risk re-shots); URL list /tmp/sett-url-list.txt; probe /tmp/sett-probe.csv; results /tmp/sett-shot-results.json
- Date: 2026-10-27 (ICT). No restarts: Sett 5221 was UP the whole run (PID 4260, still 200 at end).

## Counts

- Menu URLs found:
  - DB: `NewtouchHIS_Sett..Sys_Module`, zt='1', non-empty UrlAddress → 217 distinct URLs
    after dropping junk rows (`/1`, `/1/1`, `/123`, `test`, `/Com/*` interface-call rows,
    `/MainBusiness/*`, `/Treatment/*`, `/DoctorManage/*`) and case-insensitive dedup.
  - Rendered home links (after login): 4 content links — 3 MRQC pilot links
    (`/QualityControlManage/Apply/Index`, `/QualityControlManage/SysItem/Index`,
    `/QualityControlManage/MedicalCenter/Index`) + 1 staff JSON endpoint
    (`/SystemManage/Common/GetStaffListByDutyCode?dutyCode=`). The 3 MRQC links are NOT
    Sett routes (404 on 5221; served by MRQC 5220) — shown on the home shot, not shot
    again as Sett screens.
  - Merged-app extras: Index/Form views per controller not already in the DB set
    (incl. `/PayManage/MicroPay/*`, `/OutpatientManage/Pay/*`, `/SystemManage/User/Index`,
    `/Home/MessageBox`, `/PatientManage/Inpatient/XNHToZFForm`).
- URLs attempted: 226 (login + home + 224 menu/form URLs); authed pass 225.
- SHOT: 182 authed pages + 1 logged-out login page = **183 files** in
  `~/workspace/openhis-core/shots-roles/sett-*.png` (all NEW files; no older dirs touched).
- FAILED (no render, recorded with evidence): 43 (list below).
- NEEDS-PARAMETER: none — every menu URL in the set loads without parameters
  (query-string variants `?from=mz/zy`, `?zhxz=mz/zy`, `?rpt=zybrfyhz` all shot as listed).
- NON-ROUTE (counted once, not missing): `/ReportManage/Report/Index` — shot
  (`sett-reportmanage-report-index.png`) shows the JSON view-not-found body; guessed URL,
  no action/view/menu row in either tree, original behaves identically.

## Shots viewed by me (≥2 required — 4 viewed)

- `sett-login.png` — rendered, Vietnamese login form ("Chào mừng", "Tài khoản đăng nhập",
  "Đăng nhập"); vendor logo/footer text is vendor branding, not UI.
- `sett-home-index.png` — rendered; NOTE: Sett's Home is the MRQC pilot landing page
  ("MRQC Hệ thống kiểm soát chất lượng — Bản thử nghiệm ASP.NET Core 6.0") with links
  to the MRQC app. That is the app's actual home content, not a screenshot error.
- `sett-patientmanage-inpatient-xnhtozfform.png` — Vietnamese notice stub, rendered (COVERED).
- `sett-highchartsmanage-inpatient-numberofinpatientvisits.png` — BLANK (see render-broken below).

## Render-broken but shot (200 HTML, blank/partial on empty demo data)

- `/HighchartsManage/Inpatient/NumberofInpatientVisits` — blank viewport even after
  networkidle+3s re-shot; page JS throws `OutpatientList[0] is undefined` (empty demo data).
- `/TherapeutistManage/TherapeutistCompleteList/WorkEfficiency` — blank viewport;
  page JS throws `OutpatientList is undefined` (repeated).
- `/TherapeutistManage/TherapeutistCompleteList/TatisticWorkload` — filter UI renders,
  chart JS throws (`a is undefined`); partial render.
- All other 10 smallest shots re-shot with networkidle+3s now render content
  (Highcharts salaries/visits, KPI filters, grids). SysCISpecialMark grid renders;
  its console noise is only CORS font warnings from the static host (cosmetic).

## Failed with reason (43) — evidence: curl HTTP 404 size 0 + Firefox NS_ERROR_NET_EMPTY_RESPONSE on both attempts

- ChargeManage area not in merged app (6): ChargeConfirm/EngIndex, ChargeConfirm/EngRefund,
  ChargeConfirm/Index, ChargeConfirm/Refund, ChargeConfirmVerTwo/Index, ChargeConfirmVerTwo/Refund
- DRGGroup controller not ported (3): DRGManage/DRGGroup/DeptMedicalRecordQuery,
  DRGManage/DRGGroup/PatGrouping, DRGManage/DRGGroup/PatMedicalRecordQuery
- Controllers/actions not ported (5): HospitalizationManage/DischargeSettleDemo/Index,
  HospitalizationManage/Prescribing/EngIndex, HospitalizationManage/Prescribing/Index,
  HospitalizationManage/PrescriptionExecution/EngIndex, HospitalizationManage/PrescriptionExecution/Index
- Demo action not ported (1): OutpatientManage/OutpatCharge/IndexDemo
- English variants not ported (2): PatientManage/HospiterRes/EngIndex,
  PatientManage/HospiterRes/EnginpatientRegister
- Samples live under HighchartsManage, not ReportManage (15): ReportManage/Highcharts/Sample1–Sample15
  (Sample16 has no DB row; HighchartsManage/Highcharts/Sample1–16 actions exist but are not menu rows)
- Report actions not in merged ReportController (5): ReportManage/Report/Collectiondailyreport,
  ReportManage/Report/DecoctingfeeQuery, ReportManage/Report/Departmentmedical,
  ReportManage/Report/HosHospitalizationFees, ReportManage/Report/PreDischargeInquiry
- No route in merged app — SSO-served system pages, only User/Index stubbed (6):
  SystemManage/RehabPackage/EngIndex, SystemManage/RehabPackage/Index,
  SystemManage/SysLog/Index, SystemManage/SysModule/Index, SystemManage/SysRole/Index,
  SystemManage/SysUser/Index

## Recon observation (NOT in shot set, NOT counted missing — same class as Report/Index)

- `/OutpatientManage/OutpatientReg/Index` returns JSON view-not-found (inherited base Index,
  no Index.cshtml, no DB menu row). Core registration UI is served by
  OutpatientReg2018 / OutpatientRegSearch (both shot).

## Coverage notes

- 22 areas all covered by at least one shot: BusinessManage, DRGManage, DaySettleManage,
  DeanInquiryManage, ExampleManage, FeeConfirmManage, HighchartsManage, HospitalizationManage,
  InsuranceManage, IssueTreatRecord, JGManage, KPI, KnowledgeBaseManage, NonTreatmentItemManage,
  OutpatientManage, PatientManage, PayManage, ReportManage, SiteManage, SystemManage,
  SystemSecurity, TherapeutistManage.
- Empty grids on empty demo tables appear as empty grids — correct, not failures.
- Explicit stub/notice pages shot and counted covered: SystemManage/User/Index (SSO notice),
  PatientManage/Inpatient/XNHToZFForm, Home/MessageBox (pilot temp page).
- Restarts: NONE. No rebuilds, no other apps touched, no DB writes (SELECT only), no git ops.
