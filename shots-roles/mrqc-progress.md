# MRQC Web screenshots — progress (2026-10-27, ~09:10–09:20 ICT)

App: MRQC Web on http://127.0.0.1:5220, account 000000 (HospAdministrator).
Method: local Playwright 1.64 + Firefox (headless), viewport 1366x900 ONLY (no full_page).
Login: POST /Login/CheckLogin with {username: "000000", password: md5("000000")} via curl,
cookies reused in the Playwright context. Scripts: /tmp/mrqc_shot.py, /tmp/mrqc_retry.py
(own scripts; ~/workspace/shot/shot_ff.py untouched, SSO-specific).
Restarts: NONE. MRQC was already UP (PID 4217, Debug build) and stayed UP throughout.
No DB writes (SELECT only), no git operations, no other apps touched.

## Menu enumeration (two ways, deduplicated)

(1) Rendered home page after login (/Home/Index) — MRQC home is a plain pilot link list,
NOT a sidebar menu. Links found (5):
- /QualityControlManage/Apply/Index
- /QualityControlManage/MedicalCenter/Index
- /QualityControlManage/SysItem/Index
- /SystemManage/Common/GetStaffListByDutyCode?dutyCode= (JSON data endpoint, not a page)
- /Login/OutLogin (logout, not a page — not shot)

(2) Sys_Module rows in demo DB Newtouch_MRQC (9 rows, 7 with URLs):
- /QualityControlManage/MedicalCenter/Index (质控中心)
- /QualityControlManage/MedicalCenter/MessageQuery (质控消息查询)
- /QualityControlManage/SysItem/Index (质控项目维护)
- /QualityControlManage/Apply/Index (质控病历申请)
- /SystemManage/SysModule/Index, /SystemManage/SysUser/Index, /SystemManage/SysRole/Index
  (see FAILED below — no such controllers in the MRQC port)
- 2 parent rows with no URL (系统管理, 质控管理) — not pages.

Union of distinct page URLs: 11 (home + login + 4 QC menu pages + 3 SystemManage +
MessageBox + SysItem/Form + 3 MedicalCenter companions). All attempted.

## SHOT (11 files, all NEW in shots-roles/)

| File | URL | HTTP | Notes |
|---|---|---|---|
| mrqc-login.png | /Login/Index (logged out, shot FIRST) | 200 | Login form rendered |
| mrqc-home.png | /Home/Index | 200 | Pilot link list, fully Vietnamese |
| mrqc-medicalcenter.png | /QualityControlManage/MedicalCenter/Index | 200 | VI chrome; grid empty (see caveat) |
| mrqc-messagequery.png | /QualityControlManage/MedicalCenter/MessageQuery | 200 | VI filter + grid headers |
| mrqc-preview.png | /QualityControlManage/MedicalCenter/PreView | 200 | BLANK by design — see note |
| mrqc-qcmessagemanage.png | /QualityControlManage/MedicalCenter/QcMessageManage | 200 | VI send/query forms |
| mrqc-scorefrom.png | /QualityControlManage/MedicalCenter/ScoreFrom | 200 | VI scoring grid |
| mrqc-sysitem.png | /QualityControlManage/SysItem/Index | 200 | VI grid; rows carry DB-sourced Chinese item names (医嘱单, 护理文书…) — DB/enum pass, not view text |
| mrqc-sysitem-form.png | /QualityControlManage/SysItem/Form (no params) | 200 | VI blank form (form companion) |
| mrqc-apply.png | /QualityControlManage/Apply/Index | 200 | VI grid with 3 real PILOT-DEMO rows |
| mrqc-messagebox.png | /Home/MessageBox | 200 | VI stub notice: "Hộp tin nhắn (trang tạm thời của bản thử nghiệm)." — COVERED as stub |

Shot count: 11. Viewed by me (render confirmed, not blank/error): mrqc-home.png,
mrqc-medicalcenter.png, mrqc-apply.png (all Vietnamese, real content); mrqc-preview.png
viewed and confirmed blank (see note). Requirement ≥2 viewed: SATISFIED (4 viewed).

## FAILED with reason (3) — NOT shot, no image possible

All three return HTTP 404 with EMPTY body (Content-Length: 0) even authenticated
(verified via curl with login cookies). Firefox reports this as
NS_ERROR_NET_EMPTY_RESPONSE; retried twice with networkidle AND domcontentloaded —
same result. Cause: the MRQC port's SystemManage area contains only CommonController;
there are NO SysModule/SysUser/SysRole controllers or views in the port. Same class
as the MRMS precedent: these Sys* pages are served by SSO, not by the module app.

- /SystemManage/SysModule/Index — 404, empty body
- /SystemManage/SysUser/Index — 404, empty body
- /SystemManage/SysRole/Index — 404, empty body

## NEEDS PARAMETER

None. Every MRQC view action is parameter-free (verified in controller sources).
Two adjacent notes (not needs-parameter entries):
- PreView standalone renders blank (HTTP 200, empty body text): the view only outputs
  @Html.Raw(ViewBag.WriterControlHtml), which is unset on a direct GET — it is the
  DCSoft document-preview host and only shows content with a document context.
  Shot saved as mrqc-preview.png; flagged here rather than counted as a full page.
- MoreHandleDCWriterServicePage is NOT a view: it returns an explicit VI JSON error
  (HTTP 200, state=error): "Trình soạn thảo tài liệu DCSoft.Writer tạm thời chưa
  được hỗ trợ trong phiên bản .NET Core" — DCSoft stub, by design.

## Caveats observed (not shot failures)

- MedicalCenter GetPatList throws NullReferenceException server-side (mrqc.log,
  MedicalCenterController.cs:line 88) when the page's grid loads, so the patient
  grid stays empty even though the page chrome renders. The page itself is HTTP 200
  and shot; the grid-data error is a live bug for the parent to triage.
- SysItem grid item names are Chinese from the DB (see mrqc-sysitem.png note) —
  belongs to the open DB/enum translation decision, not to view translation.
- Home link /SystemManage/Common/GetStaffListByDutyCode is a JSON endpoint, not a
  page — not shot, correctly excluded.
- xzadmin / admin never attempted (per brief: unknown password / no role).

## Coverage verdict

Every distinct MRQC page that exists in the port is shot (11/11 existing pages),
plus the login page. The only DB menu URLs not shot are the 3 SystemManage URLs
that 404 in this app (SSO-served). No screen missing within the MRQC Web app itself.
