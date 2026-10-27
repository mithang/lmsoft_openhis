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

## Soát tiếng Trung Sett (2026-10-27, worker soát Sett — chỉ sửa text, không build/khởi động/commit/DB)

- Phạm vi: Newtouch.Sett.Web + Newtouch.Sett.Infrastructure (746 file .cshtml/.cs/.js/.resx quét; 0 file .resx; tất cả UTF-8, 0 file lỗi encoding).
- Trước: 4473 dòng chứa CJK / 32472 ký tự CJK. Sau: 1391 dòng / 12956 ký tự, trong đó dc_main.js (bundle vendor) chiếm 1293 dòng; ngoài dc_main chỉ còn 98 dòng / 538 ký tự, toàn bộ là keeps đã liệt kê (so sánh logic, đường dẫn SSRS, path server, tên font, payload BHYT, footer vendor, trích dẫn trong comment).
- Đã dịch: pass 1: 351 file, 3375 chuỗi (252 hiển thị, 3123 comment theo bộ phân loại); pass 2 (gloss ký tự cho mảnh comment): 146 lượt file, 814 chuỗi (1 hiển thị, 813 comment); cộng ~35 dòng hiển thị viết lại thủ công (nhãn + bảng mã BHYT trong YbPatientBasicInfobak, 4205/4206, nút comment).
- Kiểm chứng: số dòng mọi file không đổi (0 lệch); không có dòng CJK mới nào ngoài danh sách dòng CJK ban đầu; 3 dòng bị nhân đôi text do span bảo vệ trùng nhau (PatientBasic 883/885, ChargeTemplate_EditForm 389) đã phát hiện và khôi phục đúng nội dung snapshot.
- Staging sett-stage-* (hospitalization/outpatient/pay): KHÔNG quét, KHÔNG sửa — không nằm trong Newtouch.HIS.Sett.sln hay csproj chính (chỉ là bản sao staging có csproj riêng). Lưu ý: app staging đang chạy (vd 5222) sẽ vẫn hiện tiếng Trung cho tới khi được đồng bộ/build lại từ cây chính.
- Giữ lại cố ý, cần user quyết sau: chuỗi so sánh với giá trị DB (zybz/szxz/shzt/brxzmc/sfyb/sczt…), tên báo cáo SSRS, path "~/BHYT申报 tải lên Tệp/", font "微软雅黑", footer vendor trang Login, file "Index - 复制.cshtml" (tên file chứa chữ Trung, chưa đổi tên), Log4Net.config (331 ký tự, ngoài nhóm đuôi file quét).

## Kiểm tra đầy đủ chức năng Sett (parity audit, 2026-10-27, worker Sett — chỉ sửa Sett, không commit, không ghi DB)

- Đối chiếu trực tiếp cây gốc `lmsoft_openhis/Newtouch.HIS.Sett/Newtouch.Web` (Framework, 812e4f11) với bản chuyển đổi.
- Số đếm thật: controller gốc 124 / port 119 (trước fix); action gốc 1128 / port 1104 (trước fix, đếm mọi public method, trừ ctor/helper).
  View gốc 509 / port 505 (trước fix). Sau fix: controller 121, action 1123, view 507.
- FIELD: 496 view chung — 482 giống hệt về tên field/input/colModel, 14 khác CHỈ ở wording nhãn dịch
  (cùng số lượng, vd SiteStatisticsHighCharts, OutpatientComprehensiveAnalysis/Index) — 0 cột/field thiếu.
- ĐÃ BỔ SUNG (build Release 0 lỗi, Sett 5221 khởi động lại, PID cuối 9824, login 000000/000000 200):
  - `Newtouch.Sett.Web/Controllers/ComController.cs` (mới): 4 action dữ liệu gốc bị gọi live nhưng 404 —
    GetSysNowDate, GetSelectItemsDetailListByItemCode, GetMedicineClassificationList,
    GetChargetItemClassificationList (truy vấn view Base V_S_Sys_ItemsDetail/V_S_xt_ypfl/V_S_xt_sfdl).
    Live: items RehabTreatmentMethod → 4 dòng RTM_* (RTM_PT có thật); medicine → 20KB; charge → 7,8KB.
  - `Newtouch.Sett.Web/Controllers/TreatmentController.cs` (mới) + `Views/Treatment/Index.cshtml, AForm.cshtml`
    (copy nguyên từ gốc) + `Newtouch.Sett.Infrastructure/Domain/Treatmentportfolio.cs` (entity mz_gh_zlxmzh,
    TreamentviewVO, ITreatmentportfolioRepo) + `Repository/TreatmentportfolioRepo.cs` (SqlSugar) + 1 dòng DI
    trong `Program.cs`. Live (chỉ đọc): loginfy với org demo → 2 dòng join đủ dlmc/zhmccode;
    Codecx zhcode=4 → 1 dòng; Tjchaxun khớp → "1", không khớp → "0"; Index/AForm 200.
    Ghi chú: deleteid/deletemc port thành soft-delete zt='0' (mọi truy vấn gốc đều lọc zt='1' nên
    hành vi hiển thị giống hệt, dữ liệu còn khôi phục được); rowdata null-guarded so với gốc.
    Login() giữ nguyên để đủ action nhưng gốc và port đều không có Login.cshtml, không caller live.
  - `Controllers/HomeController.cs`: thêm GetNewFieldUniqueValue (SettKeyHelper, gốc gọi live từ
    ChargeTemplate_EditForm). KHÔNG gắn [HandlerLogin] vì attribute đó nghĩa là "bỏ qua kiểm tra
    đăng nhập" — không cookie phải 302 về /Login/Index (đã kiểm chứng). KHÔNG gọi live action này
    vì spFieldGetNewID ghi DB (INSERT/UPDATE EntityFieldSerialNo).
  - Ảnh mới (viewport 1366x900, Firefox, không ghi đè): sett-treatment-index-fixed.png,
    sett-treatment-aform-fixed.png, sett-outbook-rostercalendarform-fixed.png,
    sett-dischargesettle-index-fixed.png, sett-outpatientaccounting-index-fixed.png.
    Đã tự xem 2 ảnh: Treatment/Index render đúng toolbar/lưới tiếng Việt (lưới trống vì mở trực
    tiếp không chọn tổ chức — API với org demo trả 2 dòng, không phải lỗi dữ liệu); AForm render
    đúng form tiếng Việt.
- XÁC NHẬN KHÔNG PHẢI GAP (có trong menu DB nhưng gốc cũng không có, không tự bịa route):
  ChargeManage (6 URL), DRGGroup (3), DischargeSettleDemo/Prescribing/PrescriptionExecution (5),
  OutpatCharge/IndexDemo, HospiterRes EngIndex/EnginpatientRegister, ReportManage/Report 5 action
  (Collectiondailyreport, DecoctingfeeQuery, Departmentmedical, HosHospitalizationFees,
  PreDischargeInquiry), ReportManage/Highcharts/Sample1–15 (gốc chỉ có HighchartsManage/Highcharts/
  Sample1–16), ReportManage/Report/Index (gốc và port đều không có action Index riêng — base Index
  trả JSON view-not-found, HTTP 200), OutpatientReg/Index (gốc không có Index.cshtml, port trả
  JSON view-not-found giống hệt; đăng ký thật là OutpatientReg2018/OutpatientRegSearch, 200),
  MicroPay/Index (gốc cũng không có action/view Index; action thật MicroPay/MicroPayQuery/
  TradeInfo/TradeRefund, 200).
- KHÔNG FIX, lý do rõ (cần quyết/không port được trong phạm vi):
  - Com XnhInterfaceRequest/XnhInterfaceSend/CqYbInterfaceRequest: GuiAn XnhInterfaceSend không có
    trên interface port (chỉ Sett/CancelSett/Simulation, toàn stub); trang Trùng Khánh post XML thô
    tới 127.0.0.1:33333 không tồn tại. Giữ stub-gap, không giả vờ chạy.
  - Home SyncSysConfigParams: luồng GHI, port ISysConfigRepo không có Insert/entity (chỉ
    GetByCode/GetValueByCode + GetList DTO), và kiểm chứng live sẽ ghi DB → hoãn, không port mù.
  - Home About/Default/GetVisitNum/GetWeekNum/GetLastWeek/GetRemindCount: widget của portal frame
    cũ, không view/call-site nào trong port gọi; portal frame đã thay bằng pilot landing (SSO phục
    vụ portal). GetVisitNum đã có bản riêng ở HighchartsManage/Inpatient+Outpatient.
  - Login SSOCallback/ReturnSSOHome/UALogin/GetUALoginAddres/OutLogin/UALoginPre: cố tình bỏ —
    đăng nhập đi qua SSO/BaseAPI, UA host không tồn tại trong deployment này.
  - LocalFile/Download, SelectDataSource/getEffectPatiNatureList, NewtouchClientsData/
    GetClientsDataJson đầy đủ: không caller nào trong port (trang dùng ClientsData/
    GetAsyncClientsDataJson); NewtouchClientsData route đã có dạng ClientsData/GetClientsDataJson.
  - Sett.API gốc (18 file, ~109 public method: Booking/Order/Patient/Prescription/...): KHÔNG có
    host Sett.API nào trong bản chuyển đổi (Sett chỉ là Web hợp nhất 5221). Không bịa host mới.
- API qua Center 8016 (ĐÃ BIẾT, không đổi): 4 controller Sett trên 8016 — Booking (10 action),
  OutpatientService (10), Patient (6+), InHosPatientService (1) — TẤT CẢ proxy sang
  SiteSettAPIHost http://localhost:7002/ (appsettings.json 8016); 7002 DOWN (curl 000), 8016 UP.
  Sett Web không gọi 8016 (grep 0 kết quả) nên luồng web Sett không ảnh hưởng; ảnh hưởng duy nhất
  là caller ngoài: đăng ký khám CIS qua 8016, booking/self-service, tra cứu LIS — đều gãy ở bước
  proxy. Cần quyết: để gãy / map sang 5221 / dựng Sett.API shim (không tự chọn).
- FLOW đã kiểm chứng live (không ghi DB): login 200; menu→danh sách→form: DischargeSettle/Index 200,
  DaySettle/InpatientDaySett 200, OutpatientRegSearch 200, OutpatientReg2018 200, MicroPay 200,
  RosterCalendarForm 200 (các AJAX Treatment/Com của nó nay 200 thay vì 404); cổng validation ghi:
  OutpatientReg/Save thiếu tham số → error "Dữ liệu yêu cầu chưa đầy đủ", chưa chạm service.
  Lưu ý tồn tại từ trước, không phải do đợt này: DischargeSettle/GetUploadFeeDetails trả stub lỗi
  rõ ràng ("chưa được chuyển đổi"); toàn bộ controller Sett kế thừa [HandlerLogin] ở ControllerBase
  nên global filter bỏ qua kiểm tra đăng nhập cho mọi action không phải Index (vd Com/RosterCalendarForm
  không cookie vẫn 200) — chỉ action Index kế thừa [HandlerAuthorize] từ BaseController mới 302;
  đây là tư thế có sẵn của cả app, không đổi trong audit này, cần parent xem xét riêng.
- sett-stage-* (hospitalization/outpatient/pay): KHÔNG nằm trong sln/csproj chính, KHÔNG quét/sửa;
  hiện KHÔNG chạy (5222/5223/5224 curl 000); sau push V2.2 cần đồng bộ lại từ cây chính rồi mới
  build/chạy lại nếu còn dùng, nếu không thì bỏ (recovery.sh cũng không khởi động chúng).
- Không commit, không ghi DB trong toàn bộ audit (mọi probe ghi đều dừng ở validation gate hoặc
  không gọi; số liệu DB chỉ đọc SELECT/sp_helptext).
