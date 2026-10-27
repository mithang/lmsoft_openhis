# SSO portal screenshot progress (role: HospAdministrator, account 000000)

- App: SSO portal, http://127.0.0.1:5208, run copy (read-only, never modified)
- Tool: local Playwright 1.64 + Firefox headless, viewport 1366x900, viewport shots only
- Scripts (workspace, restart-safe): `sso-shot.py` (main run), `sso-retry.py` (domcontentloaded retry)
- Menu dump: `sso-menus.json` (117 portal links collected live from Home/Index frames)
- Restarts: NONE. SSO 5208 returned 200 at start; `recovery.sh` was not needed and not run.
  No apps restarted, no bins touched, no DB writes (SELECT only), no git operations.
- Login: form fill (#txt_account, #txt_password, #login_button), account 000000.
  No captcha on the login page (isCheckChkCode false), as expected. xzadmin/admin never attempted.
- Shots viewed by me: 4 — sso-login, sso-home-index, sso-sysmanage-sysmodule-index,
  sso-sysmanage-sysrole-form. All rendered (Vietnamese UI, grids/forms populated);
  not blank, not error pages.

## Shot count: 17 PNG files in shots-roles/ (sso-*.png)

| # | File | URL | Status | Notes |
|---|------|-----|--------|-------|
| 1 | sso-login.png | /Login/Index (logged out) | 200 | Vietnamese login form; vendor logo + footer legal name are proper nouns (keeps) |
| 2 | sso-home-index.png | /Home/Index (portal home) | 200 | Vietnamese left menu + workspace dashboard, stats 0 on demo DB (correct) |
| 3 | sso-home-dashboard.png | /Home/DashBoard | 200 | Renders ASP.NET scaffold placeholder text ("Welcome / Learn about…") — template leftover in the original view, not a 404 |
| 4 | sso-home-workspace.png | /Home/WorkSpace | 200 | Vietnamese quick-access + stats |
| 5 | sso-home-ssoindex.png | /Home/SSOIndex | 200 | Vietnamese portal shell |
| 6 | sso-home-privacy.png | /Home/Privacy | 200 | Renders sidebar-examples placeholder — template leftover, not a 404 |
| 7 | sso-home-error.png | /Home/Error | 200 | Generic error view with request ID (shot for completeness; not a menu page) |
| 8 | sso-sysmanage-sysmodule-index.png | /SysManage/SysModule/Index | 200 | Vietnamese menu tree incl. /SysManage/* rows |
| 9 | sso-sysmanage-sysmodule-form.png | /SysManage/SysModule/Form | 200 | New-menu form, no params needed |
| 10 | sso-sysmanage-sysmodule-syncform.png | /SysManage/SysModule/SyncForm | 200 | Menu-import wizard step 1 |
| 11 | sso-sysmanage-sysrole-index.png | /SysManage/SysRole/Index | 200 | Vietnamese; HospAdministrator notice text visible |
| 12 | sso-sysmanage-sysrole-form.png | /SysManage/SysRole/Form | 200 | New-role form (needed domcontentloaded retry; first attempts timed out on networkidle only — long-polling JS, page itself fine) |
| 13 | sso-sysmanage-user-index.png | /SysManage/User/Index | 200 | Vietnamese user grid |
| 14 | sso-sysmanage-user-selector.png | /SysManage/User/Selector | 200 | Org/user tree dialog (same networkidle retry note as #12) |
| 15 | sso-sysmanage-user-userroles.png | /SysManage/User/UserRoles (no params) | 200 but EMPTY | See FAILED below |
| 16 | sso-sysmanage-user-userroles-param.png | /SysManage/User/UserRoles?keyValue=<000000 id>&orgId=<demo org id> | 200 but EMPTY | See FAILED below |
| 17 | sso-sysmanage-notice-index.png | /SysManage/Notice/Index | 200 | Vietnamese message-center grid (empty on demo DB = correct) |

## Menu URLs found (two enumerations, deduplicated)

1. Rendered portal menu after login: 117 links in `sso-menus.json` —
   116 resolve to the 5208 origin (portal-relative module routes; see scope rule)
   + 1 vendor treaty link (open.tntlinking.com, proper noun, not shot).
   SSO-own menu links among them (4, ALL shot): /SysManage/Notice/Index
   ("Trung tam tin nhan"), /SysManage/SysModule/Index/ ("Menu he thong"),
   /SysManage/SysRole/Index ("Quan ly vai tro"), /SysManage/User/Index
   ("Nguoi dung he thong").
2. Demo DB (NewtouchHIS_Base, SELECT only): Sys_Module has 58 rows. These are
   the Base app's menu catalog (AppId mostly NULL; one row newtouch.base.web);
   they confirm the /SysManage/* entries exist as menu rows. SSO's own served
   surface is defined by its source: HomeController + Areas/SysManage
   (SysModule, SysRole, User, Notice, Common JSON-only). SSO source has NO
   SysLog/SysConfig view controllers — system-config pages belong to the Base
   app and are covered by the Base worker, not missing here.

## Module links (covered by their own app workers — NOT SSO failures)

All other portal links are module-app routes written as portal-relative URLs
(the browser resolves them against the 5208 origin, but they are served by the
module apps on their own ports): NurseManage 21, ReportManage 20,
HospitalizationManage 8, TemplateManage 7, OutpatientManage 6, SystemManage 5,
PatientManage 4, OutpatientNurse 4, MedicalRecordManage 3, DoctorManage 3,
DaySettleManage 3, plus Treatment/MainBusiness/MedicalRecord/PatientList 1 each,
plus Home/Index# section anchors and WorkSpace carousel anchors. Recorded once
here; not re-shot. /Login/OutLogin (logout) deliberately not shot.

## Failed with reason (1 page)

- /SysManage/User/UserRoles — HTTP 200, but renders an EMPTY role tree both
  without params and with keyValue + orgId. Evidence: Playwright pageerror
  `can't access property "replace", nd.id is undefined` from bootstrap-treeview;
  cause in source: SysRoleController.GetRoleList builds BsTreeSelectModel nodes
  with href/text/selected but NO `id` field, which treeview requires. This is a
  served-code rendering bug (dialog normally opened from User/Index), not a
  screenshot failure. Both the no-param and with-param shots are saved as
  evidence of the empty render.

## Needs parameter (with example)

- /SysManage/User/UserRoles — standalone use needs keyValue (user id) AND orgId.
  Example (demo DB, SELECT): keyValue=b245a633-1976-4dae-9153-58afed9d2b70
  (account 000000), orgId=6d5752a7-234a-403e-aa1c-df8b45d3469f. Even with both,
  the page renders empty (see FAILED above).
- Edit variants (new-record forms already shot without params):
  /SysManage/SysModule/Form?keyValue=<module id>,
  /SysManage/SysRole/Form?keyValue=<role id, e.g. Sys_Role Id 1>,
  /SysManage/SysModule/SyncForm is a wizard, not a single-record form.

## Cosmetic notes (not failures)

- FontAwesome icon glyphs render as placeholder boxes on several pages: the
  static host 127.0.0.1:41142 serves fonts without CORS headers (console:
  "Cross-Origin Request Blocked … fontawesome-webfont.woff"). Text and layout
  are unaffected; same root cause likely affects all apps sharing that host.
- DashBoard/Privacy views contain untranslated ASP.NET scaffold placeholder
  text (English template content), flagged above, not counted as missing pages.
- Empty grids on empty demo tables (Notice, stats 0) are CORRECT, not failures.
