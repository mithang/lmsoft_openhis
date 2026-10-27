# OR Web (http://127.0.0.1:5231) — role screenshots, 2026-10-27

Account: demo 000000 only (xzadmin/admin never attempted). Login via form-encoded
POST /Login/CheckLogin (username=000000, password=md5("000000") computed with md5sum);
response `{"state":"success","message":"Đăng nhập thành công"}`; cookies reused in Playwright+Firefox.
Viewport 1366x900 only, never full_page. Scripts: /tmp/or_shots.py, /tmp/or_shots2.py
(the shared ~/workspace/shot/shot_ff.py was NOT modified).

## App shape found (important for coverage)

- OR Home is a minimal port stub nav page (original portal frame is served by SSO),
  NOT a sidebar app. Its 12 rendered links are the app's own menu:
  OpApply, OpArrangement, OpRegister, OpOperation, OpRoom, OPStaff, OPAnesthesia,
  OpNotchGrade, OpFeeReport, ReportManage/Report, SysChargeTemplate, SysBaseData JSON.
- Demo DB Sys_Module has NO AppId='OR' rows (51 rows total, all Base/other-app
  routes) — so there is no separate OR menu table to enumerate; enumeration was
  (1) the 12 rendered home links + (2) every view action in the OR Web source
  (Areas Operation/ReportManage/SystemManage + root controllers), deduplicated.
- Shared role menu pattern (per brief): BaseAPI authorizeMenu includes OTHER apps'
  routes. Sample verified on THIS host with curl (authenticated): /Module/Index,
  /User/Index, /Department/Index all return plain HTTP 404 (Firefox shows this as
  an empty response). Recorded ONCE here as "shared role menu shows other apps'
  routes (404 on this host, served by their own apps)" — NOT OR failures, no
  screenshots taken for them.

## Shot count: 48 files in ~/workspace/openhis-core/shots-roles/or-*.png

All 1366x900. No restarts: OR 5231 was UP (200) throughout (PID 4342, Debug);
no other app touched. No DB writes, no git operations.

## Shot (real HTML page rendered) — 28

- or-login.png — login page, logged-out state (shot FIRST, before login)
- or-home-index.png — Home stub nav page (all 12 links, Vietnamese)
- Operation/OpApply: Index, ApplyIndex, ApplyCard, ApplyOPDetail, OpApplyCard
- Operation/OpArrangement: Index, ArrangementDetail, OperationProcDetail
- Operation/OpRegister: Index, PostoperativeReg
- Operation/OpOperation: Index (grid shows 13,470 real rows, e.g. 27.2401), Form
- Operation/OpRoom: Index, Form
- Operation/OPStaff: Index, Form
- Operation/OPAnesthesia: Index, Form
- Operation/OpNotchGrade: Index (shows real rows), Form
- SystemManage/SysChargeTemplate: ChargeTemplateIndex, ChargeTemplate_EditForm
- SystemManage/SysBaseData/GetOperationList — JSON body with real surgery rows
  (not an HTML page; shot as evidence of real data)
- Error pages (fresh session): or-error-notfound.png ("Not Found"),
  or-error-forbidden.png ("Forbidden"), or-error-httperror.png (Vietnamese notice)

Notes on grids: OpApply/Index shows "Không tìm thấy dữ liệu liên quan!" before any
department/patient is selected — correct unfiltered state, not a failure (its
search needs a selection). Surgery/diagnosis names in Chinese inside grids are
DB DATA (e.g. OpOperation rows), not view failures, per brief.

## Shot (honest Vietnamese stub/notice — counts as COVERED) — 3

- Operation/OpFeeReport/Index — SSRS notice: ReportServer.HOST empty, SSRS does
  not run on Linux (known stub class)
- Operation/OpRegisterReport/Index — same SSRS notice (view exists via override,
  renders the notice)
- ReportManage/Report/OperationNoticeReport — same SSRS notice

## Shot (faithful view-not-found JSON, HTTP 200 with error body — original view
## also missing in source; NOT new bugs) — 16

- Operation/Common/Index (action commented out in source)
- ReportManage/Report/Index (action commented out in source)
- SystemManage/Common/Index (action commented out in source)
- SystemManage/SysChargeTemplate/Index — see finding below (real page is
  ChargeTemplateIndex, shot and working)
- Root controllers, Index + Form each (base-class virtuals, no Views folder in
  original or port): ORApplyInfo, ORArrangement, ORRegistration, ORRoom, ORStaff,
  OROperation (12 shots)

## Shot (redirect, counts as covered) — 1

- or-login-ualogin.png — /Login/UALogin with no token redirects to the login
  page (byte-identical to or-login.png). UA host is unconfigured, per port notes.
  Side effect found: after this UALogin call the first session's Error/NotFound
  request redirected to login with "session expired"; Error pages were re-shot
  in a fresh session and render correctly (see above).

## Failed after retries — 0

No page failed to render after 2 retries. Every attempted URL produced a shot.

## Needs parameter — none as pages

All parameter-taking view actions render without parameters (blank forms / blank
detail shells / empty-before-search grids). JSON data endpoints (GetGridList,
GetFormJson, GetDiagnosisList, etc.) are APIs, not screens, and were not shot.
No URL was left unshot for lack of a parameter value.

## Menu URLs found / shot / failed

- Rendered home links found: 12 (+ logout). Shot: 12 (11 HTML/notice pages +
  1 JSON endpoint; SysChargeTemplate/Index link shot as view-not-found, its real
  page ChargeTemplateIndex shot separately).
- Declared view actions found in source: 45 attempted in run 1, all shot.
- Total distinct URLs shot: 48 files = login (1) + run-1 page list (45, incl.
  home) + extra error pages (2: Forbidden, HttpError; NotFound was in run 2
  overwriting its run-1 login-redirect version).

## Finding (minor, for coordinator — not a missing screen)

- The Home stub page links to /SystemManage/SysChargeTemplate/Index, but the
  controller only declares ChargeTemplateIndex/ChargeTemplate_EditForm, so that
  home link returns view-not-found JSON. The real template page works. Fixing
  the stub link is a one-line port-page change, left for the coordinator to
  decide (no code was touched by this worker).

## Visual verification (viewed own shots, not blank/error)

- or-login.png — login form renders (Vietnamese labels, no captcha shown)
- or-home-index.png — nav list renders, Vietnamese
- or-operation-opapply-index.png — two-pane apply page renders, Vietnamese
- or-operation-opoperation-index.png — grid renders 13,470 real rows
