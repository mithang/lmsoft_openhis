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

## Fix follow-up 2026-10-27 ~11:21 ICT — /ReportManage/Report/Index: KHÔNG SỬA CODE, hành vi trung thực với bản gốc (verified, not fixed by invention)
- Đối chiếu cây Framework gốc CHỈ ĐỌC (~/workspace/lmsoft_openhis, grep toàn cây): bản gốc MRMS CÓ action
  `ReportController.Index()` trả `View()` (Newtouch.MR.ManageSystem.Web/Areas/ReportManage/Controllers/ReportController.cs:14)
  nhưng KHÔNG CÓ view `Views/Report/Index.cshtml` — thư mục Views/Report của gốc chỉ có EMRBasyPrintReport.cshtml và
  HomePageReport.cshtml, và .csproj gốc cũng chỉ liệt kê đúng 2 file đó (dòng 203, 207). Không có view nào để port.
- Quét toàn cây gốc: 9 ReportController (CIS x2, EMR MRHomePage, Herp, MRMS, OR, PDS, Sett Rehab, Sett Web) — KHÔNG
  app nào có ReportManage/Views/Report/Index.cshtml (CIS/OR/Sett: không có Index.cshtml; OR còn comment out Index()).
  Views/Report/Index.cshtml duy nhất trong gốc thuộc Herp KHÔNG area (route /Report/Index, app khác) — không phải
  route /ReportManage/Report/Index của MRMS, port sang MRMS sẽ là bịa trang gốc không có nên KHÔNG làm.
- Khả năng base class: bản port override `Index()` (biên dịch được nên base nhị phân có virtual Index()), và action
  Index() cũng được khai báo tường minh trong gốc — action không phải "thiếu", chỉ view là không tồn tại ở cả hai
  bản. Middleware bản port trả JSON view-not-found kèm HTTP 200 (mềm hơn bản gốc Framework, vốn sẽ throw
  view-not-found) — đó là hành vi chung của middleware, không phải lỗi riêng của route này.
- Kết luận: mục "Failed with reason (2)" ở trên được GIẢI QUYẾT theo nghĩa xác minh trung thực, không phải bằng
  sửa code: giữ nguyên controller + không tạo view giả. Nếu người dùng muốn trang này hiển thị được thì đó là
  quyết định THIẾT KẾ MỚI (tạo view Index từ đầu), không phải port — cần chỉ đạo riêng.
- Bằng chứng live 2026-10-27 ~11:21 ICT (không restart, không rebuild, không ghi DB; app 5232 PID 2558 giữ nguyên):
  login POST /Login/CheckLogin 000000/md5 → HTTP 200 `{"state":"success","message":"Đăng nhập thành công"}`;
  GET /ReportManage/Report/Index (authenticated) → HTTP 200, body JSON view-not-found liệt kê đúng 3 vị trí đã tìm
  (/Areas/ReportManage/Views/Report/Index.cshtml, /Areas/ReportManage/Views/Shared/Index.cshtml,
  /Views/Shared/Index.cshtml); 2 action anh em cùng controller vẫn 200 HTML thật (HomePageReport 7273 bytes,
  EMRBasyPrintReport 6000 bytes). Không chụp ảnh -fixed (không có thay đổi hiển thị nào để chụp).
  Git: không có thay đổi nào trong Newtouch.HIS.MRMS/; không commit/push (điều phối viên giữ).

## Soát tiếng Trung 2026-10-27 (worker MRMS, chỉ sửa text, không build/khởi động/commit)
- Phạm vi: toàn bộ Newtouch.HIS.MRMS (Web + API + Infrastructure). Kiểm kê 222 file (.cs 129, .cshtml 62, .js 31; .resx: 0). Tất cả UTF-8, không file nào vướng encoding, không chuyển đổi GBK→UTF-8.
- Đã dịch 82 file, 996 dòng, 1364 chuỗi CJK: chuỗi hiển thị 4 dòng/4 chuỗi (3 label page.common.js + 1 message FailedException ở RecordController:252); comment 992 dòng/1360 chuỗi (/// tóm tắt trường thực thể/VO, // cuối dòng, #region, code đã comment-out).
- CJK trước/sau (phạm vi không vendor): Infra 859 dòng/1174 chuỗi → 2 dòng/2 chuỗi; Web 147 dòng/208 chuỗi → 8 dòng/16 chuỗi; API 0 → 0. Còn lại 10 dòng toàn keeps (xem báo cáo worker).
- Tự kiểm chứng: wc -l trước/sau giống hệt mọi file; 996 dòng đổi đều nằm trong tập dòng CJK ban đầu; phần code trước comment không đổi ngoài 4 dòng hiển thị; encoding sau vẫn UTF-8.
- Không sửa: JS vendor/minified (1334 dòng CJK, gồm dc_main.js 1293), file root .py tooling dịch cũ, ảnh 开始.png/结束.png, 2 file CSV dữ liệu ở Web root.
- Chưa kiểm chứng live (không khởi động app theo brief): dữ liệu tiếng Trung từ DB vẫn chờ quyết định của user; code tham chiếu các bảng từ điển mr_dic_* và Mrbasy* là nguồn dữ liệu đó.

## Kiểm tra đầy đủ chức năng (parity audit) 2026-10-27 ~12:15 ICT (worker MRMS, chỉ sửa MRMS, không commit, không ghi DB thật)

- Đối chiếu trực tiếp gốc Framework (lmsoft_openhis, chỉ đọc) với bản port, đếm từ source:
  - Controller Web: 26/26 file giống hệt tên + vị trí. Action: gốc 131, port 135 — chênh lệch duy nhất sau khi loại trừ là OutLogin (gốc khai báo tường minh, port kế thừa từ LoginBaseController, đã kiểm chứng live: trước OutLogin Home=200, sau OutLogin Home=302 về Login/Index). HomeController port-only Default/Index/SwithOrg/UserOrgChoose là các action của base nhị phân được viết tường minh — không phải thiếu. RecordController.ExecuteResult chỉ là helper của nested class DCWriterActionResult, không phải action.
  - View: gốc 74, port 62 (+2 _ViewImports hạ tầng). 60 view chung khớp; chênh lệch duy nhất = 14 view Sys* (xem dưới). So sánh field/cột/id của 60 view chung: chỉ 2 khác biệt đều là hệ quả dịch (label 'Sửa đổi'→'Sửa' ở MainDiag; biểu thức AppId ở Home/Index). View mồ côi PatInfo/MedRecordTreeEdit gốc không có controller thì port cũng giữ nguyên như gốc.
  - Entity: 11/11 file, mọi thuộc tính khớp 100% (Mrbasy 157 props, Mrbasyrelcode 36, Mrbasyss 22, Mrbasyzd 16, Mrdicblzd 9, Mrdicdept 11, Mrreldept 10, TreeViewModel 18, bafee 14, bafeeRel 10, item 33). DTO API: DefaultQueryRequest(keyword) + OrgJSONRequestBase(OrganizeId bắt buộc) port nguyên trường.
  - API: 2 controller, 6 action giống gốc (GetToken, Query, Get, Post, Put, Delete), cùng tham số; chỉ khác kiểu trả Framework (HttpResponseMessage→IActionResult).
- XÁC MINH LẠI 2 điểm cũ (không tin mù):
  - 14 view Sys*: gốc KHÔNG có controller Sys* nào trong cây MRMS — chỉ có CommonController. Route SystemManage đăng ký 2 namespace, namespace thứ hai là FrameworkBase.MultiOrg.Web.Areas.SystemManage.Controllers (nhị phân his-dll-common, không source). Các view gọi action JSON của controller nhị phân (SysConfig/GetGridJson/SubmitForm, SysUser/GetPagintionGridJson/SubmitPersonalPassword, SysRole, SysModule, SysLog, SysStaff). Kết luận cũ "SSO phục vụ" là CHƯA CHÍNH XÁC hoàn toàn: SSO chỉ phục vụ các route KHÁC (/SysManage/User, /SysManage/SysRole, /SysManage/SysModule), còn route /SystemManage/SysUser/PersonalInfo mà header MRMS đang link tới thì 404 thật (đã kiểm chứng trước fix).
  - /ReportManage/Report/Index: xác nhận lại — gốc có action nhưng không có view (Views/Report chỉ có EMRBasyPrintReport + HomePageReport); bản port trả JSON view-not-found HTTP 200, trung thực, KHÔNG tạo view giả.
- FIX DUY NHẤT (theo mẫu stub Sys* của EMR): 6 controller stub mới + 1 view thông báo dùng chung, KHÔNG dựng lại controller nhị phân, KHÔNG dữ liệu giả, KHÔNG ghi DB:
  - Newtouch.MRMS.Web/Areas/SystemManage/Controllers/SysConfigController.cs (Index/Form override + TemplateIndex/TemplateForm), SysUserController.cs (Index/PersonalInfo/Roles/Selector), SysRoleController.cs (Index/Form), SysModuleController.cs (Index/Form), SysLogController.cs (Index), SysStaffController.cs (Selector) — tổng 14 action đúng 14 route view gốc.
  - Newtouch.MRMS.Web/Areas/SystemManage/Views/Shared/SysManageNotice.cshtml — thông báo tiếng Việt; trang có tương đương SSO thì gắn link 5208 đã kiểm chứng tồn tại (/SysManage/User/Index, /SysManage/User/UserRoles, /SysManage/User/Selector, /SysManage/SysRole/Index, /SysManage/SysModule/Index — đều 302 về Login khi chưa đăng nhập SSO, tức route tồn tại); trang không có tương đương (SysConfig, SysLog, SysStaff, PersonalInfo) ghi rõ là chưa có.
  - Lưu ý build: cây port HIỆN KHÔNG BUILD ĐƯỢC ở trạng thái checkout (lỗi MSB3552 "Resource file **/*.resx cannot be found" có từ trước, không phải do fix này). Nguyên nhân: 2 file CSV tên kiểu Windows `D:\N041_...csv`, `D:\N043_...csv` + thư mục rỗng `D:\` trong Web root (tạo 2026-10-26 15:37–15:39, sau lần build DLL 12:00) làm hỏng glob evaluation của MSBuild trên Linux. Cách build lần này: tạm dời 3 mục đó ra /tmp, kill đúng PID 3004, build Debug 0 lỗi, khôi phục CSV nguyên vẹn về chỗ cũ (git status của CSV: không đổi), khởi động lại bằng run-mrms.sh (PID mới 10710, 5232 UP). Lần build sau của điều phối viên sẽ gặp lại lỗi này nếu không dời CSV hoặc không sửa csproj — cần quyết định riêng (đổi tên file tracked hoặc thêm DefaultItemExcludes).
- KIỂM CHỨNG LIVE sau fix (000000, session cookie đầy đủ gồm MR_ClientId lấy từ GET /Login/Index trước):
  - Login POST → 200 "Đăng nhập thành công". 14/14 route Sys* → 200 HTML thông báo tiếng Việt (tiêu đề từng trang đúng). Home/Index 200 (23576B), MrDept/Index 200, MainRecord/Index 200, MrDept/GetPagintionList 200 (17 dòng, lưới thật).
  - API 5332 (không sửa, PID 3763 Release giữ nguyên): Query + Timestamp ISO → 10000 + dữ liệu Sys_Config thật; TokenType "1" không token → 10000; token giả → 40004 TOKEN_IS_EXPIRED (tiếng Việt); GetToken → 40004 GETTOKEN_DISABLED (tiếng Việt); jsonpCallback trong body → bọc cb(...); GET api/Default/1 → 40004 NOT_IMPLEMENTED (stub tường minh, gốc chỉ trả chuỗi mẫu).
  - Ảnh -fixed (Playwright + Firefox, viewport 1366x900, không full_page, không ghi đè ảnh cũ): 8 ảnh mrms-systemmanage-{sysconfig-index,sysconfig-form,sysuser-index,sysuser-personalinfo,sysrole-index,sysmodule-index,syslog-index,sysstaff-selector}-fixed.png. Đã TỰ XEM mrms-systemmanage-sysuser-index-fixed.png: tiêu đề "Người dùng hệ thống", nội dung thông báo tiếng Việt, nút "Người dùng trên cổng SSO" (icon font-awesome hiển thị dạng ô vuông trong Firefox headless, chỉ là vấn đề font, giống các ảnh khác).
- CÒN THIẾU / KHÔNG FIX (nêu rõ, không giấu):
  - Chức năng Sys* đầy đủ (lưới dữ liệu thật, thêm/sửa/xóa, đổi mật khẩu) VẪN CHƯA có — stub chỉ thay 404 bằng thông báo. Dựng lại đầy đủ cần tái tạo 6 controller nhị phân + service + quyền trên bảng Sys_*, gồm cả thao tác ghi nhạy cảm (SubmitPersonalPassword) — vượt phạm vi worker này và cần quyết định thiết kế riêng.
  - Các action JSON Sys* gốc (GetGridJson, SubmitForm, DeleteForm, GetPagintionGridJson...) vẫn 404 khi gọi trực tiếp (không có view nào gọi chúng nữa vì view gốc không được port; chỉ các action trang được stub).
  - /ReportManage/Report/Index vẫn là JSON view-not-found (trung thực với gốc, cố ý không sửa).
  - Thao tác ghi của app (ví dụ MrDept/SubmitForm) KHÔNG có validation: probe rỗng của worker này trả "success" nhưng kiểm chứng bằng SELECT chỉ đọc cho thấy KHÔNG có dòng nào bị insert/update (probe rơi vào nhánh UPDATE với điều kiện hisdept='' không khớp dòng NULL nào; 2 dòng NULL trong mr_rel_dept có CreateTime 2026-10-26 15:37/15:39, tức từ trước probe này, không phải do probe này tạo; LastModifyTime hôm nay = 0 dòng). Đây là hành vi gốc (code SubmitForm gốc tương tự), không phải hồi quy của port — nhưng điều phối viên nên biết: audit này KHÔNG dọn 2 dòng NULL có sẵn đó vì brief cấm DELETE.
  - Giá trị tiếng Trung trong dữ liệu trả về (tên Sys_Config, tên khoa trong lưới) là dữ liệu demo DB, ngoài phạm vi code, giữ nguyên theo quyết định DB.
- Git: không commit/push. File mới duy nhất của audit này: 6 controller Sys* + 1 view SysManageNotice (untracked, ??). Các file M khác trong MRMS là từ các bước trước, không động tới.
