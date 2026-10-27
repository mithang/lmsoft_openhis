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

## FIXED 2026-10-27 ~04:20 ICT — /SysManage/User/UserRoles empty role tree (worker fix, NOT committed)

- Bug tái hiện live trước sửa: login 000000 OK, GetRoleList 200 trả 5 vai trò
  (Quan tri vien selected=true, Nha thuoc ngoai tru, Bac si, Y ta, Nha thuoc
  noi tru) nhưng node chỉ có href/text/selected, KHÔNG có id; trang UserRoles
  (keyValue=b245a633-1976-4dae-9153-58afed9d2b70, orgId=6d5752a7-234a-403e-aa1c-df8b45d3469f)
  render #roleList rỗng + pageerror `can't access property "replace",
  nd.id is undefined`. Gốc: trang load js/wdtree/tree.js (bootstrap-treeview
  đã bị comment out trong Views/Shared/_Form.cshtml); buildnode() của wdtree
  gọi nd.id.replace(...) nên node thiếu id là vỡ. Đây là bug của code gốc
  (bản lmsoft_openhis gốc cũng thiếu id), không phải do port.
- File sửa (chỉ 2 file thuộc project SSO, cây lmsoft_openhis_run — SSO không
  nằm trong openhis-core; KHÔNG commit/push, KHÔNG ghi DB, KHÔNG động app khác):
  1. Newtouch.HIS.SSO/HIS.SSO/Areas/SysManage/Controllers/SysRoleController.cs
     — GetRoleList: node giờ là anonymous object {id, href, text, selected,
     showcheck=true, checkstate} với id = role Id, checkstate = 1 nếu user đã
     có vai trò đó, 0 nếu không. Không đổi logic phân quyền (selection vẫn từ
     GetUserRoleList), không bịa vai trò, không sửa model dùng chung
     BsTreeSelectModel (nằm ở cây WebAPI.Manage, ngoài project SSO).
  2. Newtouch.HIS.SSO/HIS.SSO/Areas/SysManage/Views/User/UserRoles.cshtml —
     treeview option showCheckbox (tên của bootstrap-treeview) đổi thành
     showcheck (tên wdtree dùng); bỏ khối getCheckableNodes/checkNodes sau
     render (đó là API bootstrap-treeview — gọi lên wdtree sẽ re-init widget
     với data rỗng và XÓA cây vừa render; checkstate nay render sẵn trạng
     thái tick); AcceptClick dùng $("#roleList").getCheckedNodes() (wdtree
     native, trả về đúng các role id) thay cho treeview("getChecked").map(
     item => item.href) (không tồn tại trên wdtree).
- Build + restart: build Release 0 error (lần build đầu lỗi copy PDB vì process
  cũ còn giữ file — kill đúng PID cũ 2383 rồi build lại mới xong); restart
  riêng SSO bằng đúng lệnh recovery.sh section 6 (ASPNETCORE_URLS=
  http://*:5208, proxy unset, DOTNET_SYSTEM_NET_DISABLEIPV6=true), PID mới
  21380, cổng 5208 UP, /Login/Index 200.
- Kiểm chứng live sau sửa (login 000000, trang UserRoles có params như trên):
  cây hiển thị đủ 5 node, 0 pageerror; checkbox DOM: Quan tri vien =
  checkbox_1.png (đã tick), 4 vai trò còn lại = checkbox_0.png;
  getCheckedNodes() trả về đúng ['490e5ee1-4932-4952-98f3-9d2d0ef825f3']
  (id của Quan tri vien). Trang không params cũng 0 pageerror (cây rỗng là
  đúng vì không có orgId nên GetRoleList trả []).
- Ảnh: sso-userroles-fixed.png (Playwright + Firefox local, viewport
  1366x900, không full_page; ảnh cũ không bị ghi đè). Đã TỰ XEM ảnh: đủ 5
  tên vai trò; các icon checkbox render dạng glyph placeholder trong ảnh
  headless (icon host ngoài 59.80.30.157:20103, cùng quirk như ghi chú
  FontAwesome ở trên) nhưng DOM đã xác nhận đủ thẻ img checkbox_1/checkbox_0.
- Còn tồn đọng (ngoài phạm vi lỗi này, KHÔNG sửa): nút Lưu của dialog gọi
  /SysManage/User/UpdateUserRole nhưng action này KHÔNG tồn tại trong
  UserController của SSO (gốc cũng không có) nên lưu vẫn chưa chạy được —
  cần quyết định/bổ sung endpoint riêng, không tự bịa. File
  roles-shots-progress.md ở root không sửa (điều phối viên giữ).

## PORTED 2026-10-27 ~12:05 ICT — SSO chạy từ openhis-core (không còn phụ thuộc cây chạy)

- Nguồn copy DUY NHẤT: ~/workspace/lmsoft_openhis/Newtouch.HIS.SSO (chỉ đọc) →
  ~/workspace/openhis-core/Newtouch.HIS.SSO (267 file, copy xong diff giống hệt nguồn).
  Cây chạy ~/workspace/lmsoft_openhis_run chỉ dùng để ĐỐI CHIẾU fix, không copy file
  từ đó; DLL cây chạy giữ nguyên làm fallback (mtime không đổi).
- Fix UserRoles áp lại chính xác 2 chỗ (không sửa gì thêm ngoài fix + quét tiếng Trung
  ở bước sau): (1) SysRoleController.GetRoleList → anonymous object
  {id, href, text, selected, showcheck=true, checkstate} kèm comment tiếng Anh giải thích
  wdtree buildnode() gọi nd.id.replace(...); (2) UserRoles.cshtml: showCheckbox →
  showcheck, xóa khối getCheckableNodes/checkNodes, AcceptClick dùng getCheckedNodes().
  Đối chiếu: UserRoles.cshtml của bản copy DIFF RỖNG với cây chạy; diff
  SysRoleController chỉ còn 7 dòng không liên quan (comment tiếng Việt của checkout vs
  comment tiếng Trung của cây chạy + 3 chuỗi "操作成功/删除成功" — đã dịch ở bước quét).
- Fix cấu hình runtime trong bản copy (appsettings.json): Top_OrganizeId →
  6d5752a7-234a-403e-aa1c-df8b45d3469f (org demo), HisAppBaseAPIHost 8017 → 7001,
  DefaultConn Data Source=. → localhost; mật khẩu KHÔNG hardcode (launcher lấy từ
  ~/.sqlserver.env, env override BusinessDB__DefaultConn). Dòng AppAPIHostName
  "HisAppBaseAPIHost": "HIS.BaseAPI" là tên mapping, giữ nguyên.
- Port build: ProjectReference/sln trỏ ../../Newtouch.HIS.WebAPI.Manage (không tồn tại
  ở root openhis-core) được remap → ../../Newtouch.HIS.Center (bản copy của Center
  agent) trong 5 csproj + sln, chỉ trong thư mục SSO mới. Build Release: project SSO
  0 error (1037 warning có sẵn), build cả sln 0 error, net6.0 không retarget.
- Quét tiếng Trung bản copy: trước 104 dòng / 748 ký tự CJK; sau còn 5 dòng / 62 ký tự
  — toàn bộ là keep: tên pháp nhân vendor + tiêu đề giấy phép vendor ở footer
  Login/Index.cshtml (2 dòng) và _IndexNavCol.cshtml (2 dòng), Description project
  Lib.Framework.csproj (1 dòng). Đã dịch 28 file / 99 dòng: 96 dòng chuỗi hiển thị/
  thông báo/validation/log, 3 dòng tên font trong login.css Latin hóa thành
  Microsoft YaHei/SimSun (cùng font, giữ nguyên encoding GBK). Kiểm chứng: số dòng
  mỗi file không đổi, dòng đổi nằm trong tập CJK ban đầu. Chuỗi "合计" được dịch
  ĐỒNG BỘ cả nơi sinh (HomeController brxz) và nơi so sánh (WorkSpace.cshtml) thành
  "Tổng cộng" để logic không vỡ. File GBK duy nhất còn lại cần đổi encoding:
  HIS.BaseAPI/Program.cs đã chuyển GBK → UTF-8 BOM (tiếng Việt có dấu không viết
  được bằng GBK). Comment tiếng Anh của fix bước 2 giữ nguyên, không dịch lại.
- Launcher mới: ~/workspace/openhis-core/run-sso.sh (mẫu run-authcenter.sh: proxy
  UNSET, ASPNETCORE_URLS=http://*:5208, DOTNET_SYSTEM_NET_DISABLEIPV6=true).
- Kiểm chứng live từ bản copy (login 000000, UserRoles keyValue=
  b245a633-1976-4dae-9153-58afed9d2b70, orgId=6d5752a7-234a-403e-aa1c-df8b45d3469f):
  cổng 5208 UP, Login 200, login thành công (về Home/Index), GetRoleList 200 trả đủ
  5 node có id (Quan tri vien selected/checkstate=1, 4 vai trò còn lại 0),
  getCheckedNodes() = ['490e5ee1-4932-4952-98f3-9d2d0ef825f3'] (id Quan tri vien),
  0 pageerror (chỉ còn console error CORS font FontAwesome từ static host 41142,
  quirk cũ đã biết, chỉ ảnh hưởng glyph icon). Ảnh sso-userroles-core.png
  (Playwright + Firefox, viewport 1366x900, không full_page) đã TỰ XEM: đủ 5 tên vai
  trò; ảnh cũ sso-userroles-fixed.png không bị ghi đè. PID lần đầu từ bản copy: 4052.
- recovery.sh section 6 đã đổi sang openhis-core: backup recovery.sh.bak-20261027
  (diff giống hệt trước khi sửa); section 6 mới chỉ gọi run-sso.sh, section 5
  (BaseAPI) và các section khác không động vào; bash -n OK. Restart bằng đúng
  section 6 mới: SSO PID 4656, cwd = openhis-core/Newtouch.HIS.SSO/HIS.SSO,
  kiểm chứng lại toàn bộ đạt (login 200 + UserRoles 5 node + tick đúng như trên).
- Gián đoạn giữa chừng: 1 lần restart runtime (~11:55 ICT) giết toàn bộ service và
  xóa /tmp (script kiểm chứng phải viết lại); đã khôi phục bằng recovery.sh (khi đó
  section 6 còn trỏ cây chạy, PID 2140) rồi mới chuyển sang bản copy theo đúng thứ tự.
- Tồn đọng (KHÔNG tự bịa, chờ quyết): nút Lưu của dialog UserRoles gọi
  /SysManage/User/UpdateUserRole nhưng action này KHÔNG tồn tại trong UserController
  (gốc cũng không có) nên lưu vẫn chưa chạy được. Không commit, không push.

## PARITY AUDIT 2026-10-27 ~13:00 ICT — SSO + BaseAPI (worker audit, không commit/push, không ghi DB)
- Đối chiếu gốc = `git show V2.2` trong ~/workspace/lmsoft_openhis (không dùng working-copy dich-tieng-viet).
- SSO (Web 5208, chạy từ openhis-core, PID 4656, không khởi động lại): view 30 = 30 (file nào cũng đủ); action controller con 30/30 (ClientsData 3, Home 10, Common 1, SysModule 4, SysRole 8, User 4) + base class LoginBase 6, SysModuleBase 7, SysNoticeBase 3, SysUserBase 4, SystemBase 1 — toàn bộ trùng tên/chữ ký với gốc; diff code còn lại chỉ là dịch comment/chuỗi + fix GetRoleList giai đoạn 1. Field/grid: id/name/field/url trên mọi view đều SAME, trừ WorkSpace series name đã dịch đồng bộ (brxz "Tổng cộng" khớp cả controller lẫn view).
- KIỂM CHỨNG LẠI (1) UserRoles KHÔNG hồi quy: login 000000 thành công (CheckLogin success, Home/Index 200); GetRoleList trả đủ 5 node có id + showcheck + checkstate; ảnh mới sso-userroles-parity.png (Playwright+Firefox 1366x900, đã tự xem: đủ 5 tên vai trò) + getCheckedNodes() = ["490e5ee1-..."] = Quan tri vien. Lưu ý: URL dialog đúng phải dùng `orgId=` (User/Index.cshtml:209); dùng `organizeId=` sẽ ra cây rỗng — đó là tham số sai của người gọi, không phải lỗi port.
- KIỂM CHỨNG LẠI (2) GetOpenId: KHÔNG tồn tại ở bất cứ đâu trong source V2.2 — SSO web không có, AuthCenter 8015 cũng chỉ có GetAppToken/GetAppFrienAuthToken/UserTokenRefresh/GetUserToken. Các LoginController module (CIS/Base/MRQC/Sett/Base.HOSP) gọi `api/Auth/GetOpenId` qua GetSSOAPIUrl nhưng endpoint này không được implement trong repo gốc → thiếu này là thiếu CỦA GỐC, không phải hồi quy port, không tự bịa endpoint. SSOCallback/UALogin/UALoginPre/QuickLoginSuccess nằm ở LoginController các module (base FrameworkBase.MultiOrg, dạng binary), không thuộc phạm vi SSO web; SSO web chỉ có GetUALoginAddres (đã kiểm chứng live: appId=HIS.BaseAPI → URL UALogin kèm token, appId=CIS → 8003; appId=MRQC → null vì gốc cũng không có SiteMrqcWebHost trong AppAPIHost — đúng gốc, không sửa).
- KIỂM CHỨNG LẠI (3) captcha: appsettings SSO Is_CheckChkCode=false, giữ nguyên; login không cần mã, GetAuthCode vẫn 200 (endpoint còn, chỉ không bắt buộc).
- UpdateUserRole: XÁC NHẬN nút Lưu UserRoles vẫn 404 cả GET lẫn POST (đã kiểm chứng live, không ghi DB). Nguyên nhân đúng gốc: SSO UserController kế thừa OrgControllerBase, KHÔNG kế thừa SysUserBaseController; mà SysUserBaseController (nơi có UpdateUserRole) không được controller nào trong V2.2 kế thừa — code chết của gốc. KHÔNG tự thêm action (brief cấm bịa; thêm action = mở luồng ghi DB, không thể kiểm chứng live nếu cấm ghi). Phương án nếu điều phối viên quyết sửa: thêm action mỏng vào SSO UserController, ủy quyền đúng _userRoleAuthDmnService.UpdateUserRole đã inject sẵn (cùng chữ ký/keyValue/organizeId/roleList như view đang post), theo đúng thân hàm của SysUserBaseController.
- BaseAPI: source ĐÃ có trong openhis-core (Newtouch.HIS.SSO/HIS.BaseAPI) — không phải báo cáo phạm vi. 26 action / 5 controller, route attribute SAME toàn bộ với gốc (Login 3, OrgService 9, SysUser 2, System 10, Systemed 2). Live run-copy 7001 (PID 2078) KHÔNG động vào (không kill/khởi động lại); mọi kiểm chứng bản port chạy trên instance tạm 7002 rồi đã tắt, port trả về trống.
- FIX 1 (config, file duy nhất sửa nội dung): HIS.BaseAPI/appsettings.json Top_OrganizeId d8ee85f8-... → 6d5752a7-234a-403e-aa1c-df8b45d3469f (org demo, khớp run-copy + SSO). Trước fix, CheckPwd trên bản port trả 40004 "Không tìm thấy thông tin người dùng" (UserLogin lọc TopOrganizeId); sau fix trả 10000 SUCCESS, account 000000, id b245a633-..., topOrganizeId demo. Đây là lỗi config bản port bỏ sót (run-copy đã sửa từ trước, openhis-core chưa).
- FIX 2 (bin, không sửa source): Lib.Base source mới hơn DLL (Center dịch 04:40–04:42 nhưng DLL từ 26/10) → build lại Lib.Base Release 0 lỗi, copy DLL mới CHỈ vào HIS.BaseAPI/bin/Release (không động his-dll-common/Net6 để khỏi ảnh hưởng worker Herp đang build song song; bin/ đã gitignore). Hiệu quả live trên 7002: CheckPwd thiếu AppId trả tiếng Việt "Id ứng dụng (AppId) bắt buộc phải truyền" (run-copy 7001 vẫn trả tiếng Trung "应用Id（AppId）必须" vì DLL cũ), GetMenuList không token trả "Xác thực token thất bại:" (tiếng Việt).
- BaseAPI live 7002 (sau 2 fix): CheckPwd 10000; 24/24 route còn lại đều tới được cổng auth (401 tiếng Việt) hoặc AllowAnonymous đúng gốc trả 10000 kèm dữ liệu thật (GetMediUsageList, GetDrugFormulationUsageList — AllowAnonymous có sẵn ở gốc, không phải bypass mới). Route hay gọi nhầm: OrgList là api/organize/OrgList, user page là api/sysuser/GetSysUserPage, menu-by-DB là api/System/GetMenuListbyAppId (tên method GetMenuListbyDBAsync nhưng route là byAppId — khớp gốc).
- Lưu ý minh bạch: 1 probe POST /SysManage/SysRole/submitRoleUser với body rỗng đã trả success (action gốc không validate null, gọi thẳng Dmn). Đã kiểm tra code port: với roleId=null/userList=null, Dmn chỉ Delete where RoleId==null + không insert + commit; đọc chỉ SELECT sau đó: Sys_UserRole còn 3 dòng, 0 dòng RoleId NULL → không có dòng nào bị xóa/thêm. Không có probe ghi nào khác; không INSERT/UPDATE/DELETE chủ đích nào.
- Tồn đọng chuyển điều phối viên: (a) UpdateUserRole 404 (quyết có thêm action hay giữ đúng gốc); (b) GetOpenId không có trong gốc (quyết có implement dịch vụ SSO API ngoài repo hay để nguyên); (c) cắt sang BaseAPI openhis-core: cần launcher + recovery.sh section 5 mới (hiện vẫn trỏ run-copy) — không tự tạo vì sẽ xung đột instance 7001 đang chạy; (d) MRQC→null ở GetUALoginAddres (thiếu SiteMrqcWebHost từ gốc); (e) SSO bin vẫn dùng Lib.Base.dll cũ (chưa refresh như BaseAPI vì SSO đang chạy, không muốn restart ngoài kế hoạch — các message SSO đã kiểm chứng đều tiếng Việt).
- Ảnh: sso-userroles-parity.png mới (không ghi đè ảnh cũ). Không commit, không push, không động run copy, không động app khác.
