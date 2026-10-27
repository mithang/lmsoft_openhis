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

## Chinese sweep 2026-10-27 (text-only worker, no build/start/commit/DB write)

- Scope: whole Newtouch.HIS.EMR (Web + API + Infrastructure), 422 source files (.cshtml/.cs/.js, no .resx).
- Before: 6,825 CJK lines / 11,316 runs (Web 4,634 / API 1 / Infrastructure 2,190). After: 3,098 CJK lines /
  6,094 runs (Web 3,086 / API 0 / Infrastructure 12) — remainder is keeps only (see below).
- Changed: 240 files, 3,780 lines (git insertions = deletions per file; every file's line count unchanged;
  BOM preserved). Split: ~276 display/metadata lines + ~3,505 comment lines (classifier: CJK before any
  comment marker, or pure-comment line). Includes 56 mojibake comment recoveries in 3 entity files
  (bl_hljl_ybEntity 44, bl_bllxEntity 8, bl_hljl_srsclEntity 4; meanings from dich-tieng-viet branch) and
  2 comment rewrites that quoted old Chinese strings (emr-vi.js, grid.locale-vi.js).
- Keeps (not edited, by group): vendor/minified JS (dc_main, jquery/bootstrap/summernote-zh-CN/citys/easyui/
  ribbon, both Content/ and wwwroot/ copies); File/DataSourceDescriptor.xml (844 CJK lines, data file) and
  city.json; fonts (SimSun/Microsoft YaHei/Heiti/Kaiti + font-picker labels); DCSoft keys/values
  (ExcludeKeywords, nodeName '数字文本', template <ID>/<Name>/<FileName>, GridLinePreviewText/LicenseText,
  demo FileOpen paths, openN() demo menu items, icon .ico filenames, demo template .xml paths);
  logic/SQL/DB values (SQL TypeName doctor roles, CONVERT 长/临, yzlb == "长", [停] marker, SSBFZMC
  submitted values, gj == "中国", sd/flag/bllxmc action & document-type comparisons, DocRelSubmit stored
  document-type name, BLMC timestamp+name, ~/file/住院病历 paths, Replace 常 pattern); stored grade codes
  (EnumMR 甲/乙/丙 + MrqcAdd basyLevel writes — literal not re-quoted here per brief); hidden form values
  (zyh/mzh placeholders, 医生签名 button); demo person names (张三/李四/上官海燕); vendor footer
  (Hubei Tian Tian Shu Lian / tntlinking license lines); testbl.cshtml L59 giant commented docContent line.
- Known prior-pass mixed artifacts left as-is (logic operands, need a caller-trace decision, not a text
  edit): sd == "保存文档" || sd == "Lưu tài liệu" (MedRecordTemplateController L259,
  MedicalRecordController L213/L496), Blys/Index flag == "Sửa" || flag == "修改" (#tijiao label).
- DB-sourced Chinese observed (code evidence only, no DB read/write, app not started): doctor-role values
  in Newtouch_CIS..zy_PatDocInfo.TypeName; stored grade codes (basyLevel); stored document-type names;
  submitted complication values; runtime labels from queries (departments/drugs/diagnoses) not verified
  live. No code was changed to mask DB values (user decision required).
- Encoding: all 422 files UTF-8 (305 plain, 117 BOM), 0 GBK/unknown — no file blocked for encoding.
- Mirrors: Content/ vs wwwroot/ JS copies verified identical after translation (domtool, floatingselector,
  page.common).

## Parity audit EMR (2026-10-27, worker ba88e1c0) — KHÔNG commit, KHÔNG ghi DB
- Đối chiếu V2.2 (git show V2.2) vs bản port. Kết quả đếm thật, xem báo cáo đầy đủ của worker.
- ĐÃ BỔ SUNG (3 file Web, build Debug 0 error, EMR Web restart PID cũ 3396 -> mới 14101; API PID 3446 không động):
  1. Controllers/LoginController.cs: port UALogin + UALoginPre (mẫu OR; preheat giữ appId CIS như gốc; QuickLoginSuccess không có tương đương Core nên fallback về trang Login; GetUALoginAddres vốn đã thừa kế từ LoginBaseController). Live trước: /Login/UALogin 404; sau: 302 về / (không còn 404).
  2. Areas/MedicalRecordManage/Controllers/MedicalRecordController.cs L213/L496/L609/L1746: thêm `|| sd == "Tải lên bảo hiểm y tế"` — nút upload trong view port post đúng giá trị này nhưng phép so sánh cũ chỉ có "上传医保" nên nhánh upload không bao giờ chạy (lỗi dịch một phía đã chứng minh bằng value của nút submit trong MedicalRecordEdit/EdiForAdd). Không đổi các phép so sánh pdf/html (không có nút HTML tương ứng; thuộc DCSoft toolbar).
  3. Areas/MRHomePage/Views/Report/BasyReport.cshtml: khi ReportServer.HOST trống thì hiện thông báo SSRS tiếng Việt thay vì redirect URL tương đối hỏng rồi 404; khi có cấu hình thì giữ nguyên redirect gốc.
- Ảnh -fixed (Playwright + Firefox local, viewport 1366x900, không full_page; ảnh cũ không ghi đè): emr-home-index-fixed.png, emr-report-basyreport-fixed.png (đã tự xem: thông báo SSRS tiếng Việt, không redirect), emr-blys-index-fixed.png. Login kiểm chứng live: POST CheckLogin 000000 + md5("000000") -> "Đăng nhập thành công"; Blys SubmitForm thiếu trường -> cổng validation từ chối, không ghi DB.
- Kết luận (a)-(d): (a) sd "Lưu tài liệu" đã khớp runtime (value nút submit trong view) nên giữ cả hai phía; flag #tijiao runtime = "Sửa" (YSList.cshtml:52 khi chọn node cây) nên phép so sánh kép đúng, không sửa — chỉ có nhánh upload là lỗi thật và đã fix. (b) Mô tả YB tiếng Việt an toàn: consumer duy nhất dùng Description để dựng thông báo lỗi thiếu trường, payload dùng tên thuộc tính/mã jydm, không khớp nhãn tiếng Trung. (c) File/DataSourceDescriptor.xml vẫn là file dữ liệu (86 file dưới File/), toCS()/XMLCreate tái tạo từ DB đúng như gốc, không port thành code. (d) BasyReport gốc cũng redirect JS sang SSRS ReportViewer.aspx và ReportServer.HOST trống ở cả hai bản — đã giữ stub thông báo rõ ràng, không bịa trang báo cáo.
- Lưu ý: git status lúc audit có nhiều file EMR đã sửa sẵn (đợt dịch giai đoạn 2 đang chạy song song) — diff 152 dòng của MedicalRecordController chủ yếu là dịch comment của đợt đó, phần của worker chỉ là 4 dòng if nêu trên.
