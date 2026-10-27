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

## Fix 2026-10-27 — stub link SysChargeTemplate (worker OR, một dòng)

- Trước sửa: `Views/Home/Index.cshtml:22` trỏ `/SystemManage/SysChargeTemplate/Index`
  nhưng controller `SysChargeTemplateController` không có action `Index()` (chỉ có
  `ChargeTemplateIndex`/`ChargeTemplate_EditForm`), nên link cũ trả view-not-found.
- Sau sửa: cùng dòng trỏ `/SystemManage/SysChargeTemplate/ChargeTemplateIndex`.
- Build: Release 0 lỗi + Debug 0 lỗi (36 warning cũ). Lưu ý: `run-or.sh` chạy DLL
  Debug nên phải build cả Debug thì app đang chạy mới nhận fix; bản Release cũng
  đã build xong cho điều phối viên commit.
- Khởi động lại: kill đúng PID cũ 2604, chạy lại bằng `run-or.sh` → PID mới 19303,
  cổng 5231 UP, trang login 200.
- Live (curl + session, tài khoản 000000 duy nhất): `POST /Login/CheckLogin` →
  `{"state":"success","message":"Đăng nhập thành công"}`; `GET /Home/Index` 200 và
  HTML đã chứa link mới; `GET /SystemManage/SysChargeTemplate/ChargeTemplateIndex`
  200, `<title>ChargeTemplateIndex`.
- Ảnh mới (viewport, Firefox/Playwright, không ghi đè ảnh cũ, đã tự xem cả hai):
  `or-home-fixed.png` (trang chủ, link "Mẫu thu phí" còn đó), và
  `or-chargetemplate-fixed.png` (grid mẫu thu phí tiếng Việt, trống vì demo DB
  không có dòng mẫu — đúng dữ liệu, không phải lỗi).
- Lưu ý ngoài phạm vi: `shot_ff.py` trong `~/workspace/shot/` là script riêng cho
  SSO (hardcode cổng 5208 + flow SSO) nên không dùng trực tiếp cho OR được; đã
  dùng script Playwright riêng `/tmp/or_fixed_shot.py` theo cùng cách (Firefox,
  viewport, không full_page). Ngoài ra `/Login/GetAuthCode` của OR tự lỗi trên
  Linux (System.Drawing không hỗ trợ) nhưng không ảnh hưởng login vì
  `Is_CheckChkCode: false` và trang login render không có ô mã xác minh.
- Không commit, không push, không ghi database (chỉ GET + 1 POST login).

---

## Soát tiếng Trung OR — đợt V2.2 (2026-10-27, worker soát OR, chỉ sửa text)

- Phạm vi: toàn bộ `Newtouch.HIS.OR` (API + Infrastructure + Web), 160 file quét
  (.cs/.cshtml/.js không vendor, .resx: 0 file). Không build, không chạy app,
  không commit, không ghi DB. Không đụng `Views/Home/Index.cshtml` (link do
  worker khác sửa, giữ nguyên).
- Trước: 1.098 dòng chứa CJK / 1.472 chuỗi CJK (API 1 dòng, Infrastructure 661,
  Web 436). Sau: 6 dòng keeps (API 0, Infrastructure 1, Web 5). Đã đổi 96 file,
  1.092 dòng — tất cả đều nằm trong danh sách dòng CJK ban đầu, số dòng mỗi
  file không đổi (đối chiếu snapshot `/tmp/or-cjk-before.txt`), encoding giữ
  nguyên UTF-8 (không file nào GBK/không chắc chắn, không cần chuyển đổi).
- Đã dịch: hiển thị ~55 dòng / ~60 chuỗi (label/caption grid, option, modalAlert,
  tiêu đề, giá trị input mẫu trong `page.common.js` và các view Operation);
  comment ~1.037 dòng / ~1.401 chuỗi (XML doc entity/DTO/repo, `#region`,
  ghi chú JS/CSS trong view, dữ liệu kiểm tra đã comment-out).
- Keeps (không sửa, có lý do trong báo cáo cho điều phối viên): so sánh logic
  `sqzt=="已登记"` (`OPRegisterDmnService.cs:24`); footer vendor
  `Views/Login/Index.cshtml:85-86`; tên font `"微软雅黑"` (`Error.cshtml:13`);
  trích dẫn chuỗi gốc trong comment `grid.locale-vi.js:17`, `or-vi.js:4`.
  Lưu ý: cặp sentinel nội bộ `page.common.js` (`'无'`/`'控制'`) đã dịch đồng bộ
  cả nơi sinh và nơi so sánh (`'Không có'`/`'Kiểm soát'`) nên logic không đổi;
  riêng `sqzt` không có nơi sinh trong phạm vi nên giữ nguyên.
- Ngoài phạm vi quét (không sửa): `Configs/Log4Net.config` (22 dòng comment +
  mẫu log), `Content/css/login.css` (3 dòng tên font), file project/json/sln
  (0 CJK). Dữ liệu tiếng Trung từ DB không sửa bằng code: tên chẩn đoán/phẫu
  thuật/khoa/nhân viên/khoản thu (cột `zdmc`, `ssmc`, `bqmc`, `sfxmmc`…) hiển
  thị qua grid lấy từ DB — cần quyết định ghi DB của user.
- Lưu ý nhỏ: comment gốc `EnumDB.cs` cho `EnumSSzt.yzx` ghi "đã hủy đăng ký"
  trong khi `[Description]` đã là "Đã đăng ký" — chỉ dịch đúng chữ comment,
  không sửa logic; coordinator kiểm tra lại nếu cần.

---

## Parity audit OR — 2026-10-27 (worker audit OR, chỉ đọc + kiểm chứng live, KHÔNG sửa code)

- Phạm vi: `Newtouch.HIS.OR` (Web 5231 + API 5331 + Infrastructure). Đối chiếu với cây gốc
  nhánh V2.2 (`812e4f11`, không phải checkout `dich-tieng-viet` hiện tại). Không commit,
  không ghi DB (chỉ GET + POST login + POST form rỗng tới cổng kiểm tra; mọi probe ghi
  đều kết thúc bằng lỗi trước khi ghi hoặc bị DB chặn, xem dưới).
- Kết quả tổng: KHÔNG CÓ FIX CODE NÀO — mọi action/DTO/view thuộc sở hữu của OR đều đã
  đủ; các thiếu hụt còn lại đều là (a) trang của SSO, (b) khung portal của Framework,
  (c) stub Windows-only có chủ đích, (d) lỗi gốc được dịch trung thực. Không build lại,
  không khởi động lại (app đang chạy PID 3148/3269, Debug, giữ nguyên).
- FIELD: DTO 6/6, Entity 11/11, VO 12 gốc + 2 bổ sung (`SysDiagnosisVO`, `SysDutyStaffVO`
  phục vụ `ISysDiagnosisRepo`); thuộc tính DTO/Entity/VO: 0 lệch. 37 view chung: tập
  tên cột grid + id input khớp 100% (0 lệch). Request API 5/5 class, thuộc tính khớp.
- MÀN HÌNH: controller Web 25/25 file, action tự khai báo 122/122 (kể cả action bị
  comment-out ở cả hai bên: `ORApplyInfo/Index`, `ORArrangement/SubmitForm`,
  `Report/Index`). View: gốc 57, port 41 = 37 chung + 4 `_ViewImports.cshtml` mới.
  20 view gốc không có ở port: 14 Sys* (SysConfig/SysLog/SysModule/SysRole/SysUser/
  SysStaff — gốc cũng KHÔNG có controller cho chúng; trang Sys do SSO phục vụ, SSO có
  area SysManage) + 6 khung portal Framework (`Home/Default`, `Home/UserOrgChoose`,
  4 partial `_Index*` — action thuộc base-class nhị phân Framework, không thuộc
  `HomeController` của OR; port dùng landing stub có chủ đích theo mẫu pilot, SSO
  phục vụ khung portal; link stub đã sửa ở giai đoạn 1 vẫn đúng
  `/SystemManage/SysChargeTemplate/ChargeTemplateIndex`).
- API: 4/4 controller, 8/8 endpoint (GetToken, Query, Get, Post, Put, Delete,
  ORPatList, GetUserInfo), route + tham số khớp gốc; envelope 10000/40004 giữ nguyên.
- FLOW live (000000 duy nhất, 5231+5331 UP): login `{"state":"success","message":
  "Đăng nhập thành công"}`; 18 trang GET đều 200 (OpApply/OpArrangement/OpRegister/
  OpOperation/OpRoom/OPStaff/OPAnesthesia/OpNotchGrade/OpFeeReport/OpRegisterReport/
  OperationNoticeReport/ChargeTemplateIndex/EditForm/ApplyIndex/ApplyCard...);
  JSON thật: GetModuleDataJson, GetClientsDataJson (danh sách phẫu thuật thật),
  GetAsyncClientsDataJson (enums + bác sĩ thật), GetOperationList, GetGridList/
  GetPatGridList/GetPagintionListForRegistration (OpArrangement/OpAnesthesia/OpRoom/
  ChargeTemplate trả rows thật hoặc [] đúng dữ liệu demo). API 5331: Query không
  token → 10000 data [] (demo DB trống), token giả → 40004 TOKEN_IS_EXPIRED,
  ORPatList không token → 40004 TOKEN_IS_REQUIRED, GetToken → 40004 GETTOKEN_DISABLED
  (giữ stub theo quyết định đứng, không cấp token), UA GetUserInfo → 40004 AUTH_FAILED.
- Cổng kiểm tra (không ghi thật): OpApply SubmitForm rỗng → lỗi "Thông tin bệnh nhân
  không được để trống"; OpNotchGrade/ORRoom rỗng → lỗi "Mã không được trùng lặp";
  ChargeTemplate_SubmitForm rỗng → INSERT bị DB chặn NOT NULL cột `sfmb` (gốc cũng
  không có validation trước, gọi thẳng service — hành vi trung thực, câu lệnh đã
  terminate nên không tạo dòng); OpRegister submitForm rỗng → NRE tại
  `ORArrangementRepo.UpdateSqzt` vì `FindEntity(null)` = null — code port giống gốc từng dòng
  giống gốc nên đây là hành vi gốc với input suy biến, và staff list rỗng nên
  không có ghi nào trước lỗi. Tất cả đều KHÔNG ghi DB thành công.
- EnumSSzt.yzx — KẾT LUẬN: MÂU THUẪN LÀ CỦA GỐC, port dịch trung thực, KHÔNG ĐỔI.
  Gốc V2.2 (`git show V2.2:.../EnumDB.cs`): `[Description("已登记")]` (= "Đã đăng ký")
  nhưng comment `/// 已取消登记` (= "Đã hủy đăng ký", copy-paste từ mẫu của `yzf`).
  Port giữ đúng cả hai: Description "Đã đăng ký", comment "Đã hủy đăng ký". UI dùng
  Description qua `GetEnumListLocal` nên hiển thị đúng; chỉ comment XML sai, không
  ảnh hưởng runtime. Không sửa nghĩa theo đúng quy tắc brief.
- Không fix được / không fix (lý do rõ): khung portal Framework (cần base-class nhị
  phân, SSO đã phục vụ — quyết định thuộc điều phối viên, không tự dựng lại);
  báo cáo SSRS (OpFeeReport/OpRegisterReport/OperationNoticeReport) giữ thông báo
  rõ ràng vì SSRS chỉ chạy trên Windows; GetToken giữ disabled; dữ liệu tiếng Trung
  trong grid là dữ liệu DB (tên phẫu thuật/khoa), không sửa bằng code.
- Ảnh: không có trang nào thay đổi nên không chụp ảnh `-fixed` mới (ảnh giai đoạn
  trước vẫn đúng); đã kiểm chứng live bằng curl thay cho chụp lại.
