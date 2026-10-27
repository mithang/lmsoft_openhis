# Role screenshots — administrator (000000) — progress

Coordinator file. Workers write per-app files under `shots-roles/<app>-progress.md`;
the coordinator merges the coverage table here at the end.

- Role reality: ONLY demo account 000000 (password 000000) works. xzadmin password
  unknown (do not attempt), admin has no role (do not attempt). So "each role" =
  the one HospAdministrator role across every system.
- Scope: Web UI apps only. API hosts (5320, 5331–5337) and center hosts
  (8015, 8016) have no UI and are EXCLUDED.
- Shots: `shots-roles/<app>-<page>.png`, viewport only, NEW files only.
- Apps: SSO 5208, MRQC 5220, Sett 5221, MRMS 5232, OR 5231, CIS 5234, EMR 5235,
  Herp 5236, Base HOSP 5233, Base Web 5238, PDS 5237.
- Workers run max 2 at a time (RAM).

## Batches — ALL DONE (2026-10-27 ~10:09 ICT)

- Batch 1: MRQC 5220, Sett 5221. Batch 2: CIS 5234, PDS 5237.
- Batch 3: EMR 5235, Herp 5236. Batch 4: OR 5231, MRMS 5232.
- Batch 5: Base HOSP 5233, Base Web 5238. Batch 6: SSO portal 5208.
- Max 2 workers at once throughout. On-disk total verified by coordinator: **1009 PNGs**.
- Restarts: NO worker restarted a healthy app and NO rebuilds happened. Two VM
  restarts hit mid-sweep (Base HOSP + Base Web workers); both recovered via
  `recovery.sh` and re-shot/resumed — shots survive because they live in workspace.

## Coverage table (merged by coordinator; full per-URL lists in shots-roles/<app>-progress.md)

| App | Shots | Failed (no shot possible) | Needs-parameter / error-body shot |
|-----|------:|---------------------------|-----------------------------------|
| SSO 5208 | 17 | 1 page: /SysManage/User/UserRoles renders EMPTY role tree (served-code bug: GetRoleList nodes lack id → treeview throws) | UserRoles standalone (keyValue+orgId); SysModule/Form?keyValue=, SysRole/Form?keyValue= edit variants |
| MRQC 5220 | 11 | 3× 404 SSO-served: /SystemManage/SysModule|SysUser|SysRole/Index | none |
| Sett 5221 | 183 | 43× 404, grouped: ChargeManage not in merged app (6); DRGGroup not ported (3); DischargeSettleDemo/Prescribing/PrescriptionExecution not ported (5); OutpatCharge/IndexDemo (1); HospiterRes Eng variants (2); ReportManage/Highcharts samples live under HighchartsManage (15); Report actions not in merged ReportController (5); SSO-served Sys pages, only User/Index stubbed (6) | none; ReportManage/Report/Index is a NON-ROUTE (guessed URL, JSON view-not-found shot, not missing) |
| MRMS 5232 | 44 | /RecordManage/PatInfo/MedRecordTreeEdit 404 (orphan view, no controller); /ReportManage/Report/Index shot but body = view-not-found error (genuine defect) | none (Main* forms render empty without record, no error) |
| OR 5231 | 48 | 0 | none |
| CIS 5234 | 204 | 16× 404: other apps' menus (Dietary/*, HospitalizationPharmacy/DispenseIndex, MedicalRecordManage/PatInfo/*, OutpatientManage/OutpatientReg/OutpatientReg2018, StorageManage/Storage/DepartmentApply); SSO-served SysModule|SysUser|SysRole|SysLog|SysConfig/Index; /SystemManage/Template/Index; /SystemManage/TTCataloguesComparison/DetailIndex; broken home link /SystemManage/BaseData/Index (no Index action); POST-only 405: /BespeakRegister/CancelYY, /NurseManage/StockCarryOver/CarryOverProduct | /InfusionManage/Query without ?tags=… (JSON error, faithful); /Apply/Test JSON view-not-found (dev-test row); ClinicMetting blank by design (226-byte view); 4 Apply Examination/Inspection pages near-blank standalone (frame-hosted) |
| EMR 5235 | 90 | 1: /MRHomePage/Report/BasyReport → JS-redirect to legacy SSRS ReportViewer.aspx → 404 (EMR's SSRS gap) | PreViewEdit (blid+bllx); MedicalRecord EdiForAdd/PreView/PrintView (record id); MainPreView commented-out, not a page; error bodies shot: MRHomePage/Index, Report/Index, TemplateDesign/Index, OutHosPatQuery (faithful view-not-found) |
| Herp 5236 | 96 | 8: 5 Sys* menu URLs redirect-loop (SysModule|SysRole|SysUser|SysLog|SysConfig/Index → 302 to self; notice pages at /SystemManage/SystemManage/SysX ARE shot — content covered, menu URLs need port fix); /DeliveryManage/Delivery/Index 404 (stale link); /SystemManage/System/Index 404 (no SystemController); /SystemManage/SystemManage/Notice 404 (private helper, not routable) | none |
| Base HOSP 5233 | 148 | 2: /Role/Users 404 (view exists, no action; extra companion, not menu URL); /1 404 (nonsense Sys_Module UrlAddress on blood-type row — bad menu data) | none; faithful view-not-found: /Project/ProjectType{,Form,ChildForm} (no Views/Project/ in original either) |
| Base Web 5238 | 41 | 2: /Organize/Applications 404 (orphaned view, no action); /Home/Notice 404 (stale DB menu row; BaseHOSP serves Notice on 5233) | 0 |
| PDS 5237 | 127 | 8 compile-removed by design (AntibioticTypeQuery, PharmacyWindow/Index, Transfer Apply/Warehousing, SystemManage SysConfig/SysModule/SysRole/SysUser + PersonalInfo link); 3 genuine port gaps: /Medicine/PharmaceuticalStocks, /ReportManage/YpckQuery/DecoctingfeeQuery, /ReportManage/YpckQuery/DoctorsOrderQuery; 38 view files with no route + JSON/text endpoints | examples: DrugStorage/RequestMedicineInfo?sldId=…, OutPatientPharmacy/SearchFyDetail?cfh=…, PharmacyDrugStorage/EndInventory?pdId=…; excluded once: /testPrint/Index (demo, fake patient data) |

Cross-app menu scope (applies to the whole sweep, NOT missing screens): the
rendered sidebar on PDS/MRMS/Base Web (and sampled on others) shows the SHARED
BaseAPI role menu (authorizeMenu), including other apps' routes that 404 on
that host. Those are served by their own apps and covered by their workers.
Herp's sidebar is Herp-only; Base HOSP's home has no sidebar menus (SSO portal
frame). Coverage scope per app = that app's OWN menus (app DB Sys_Module rows +
own rendered links) + Form companions.

Other findings carried to the final report (not shot failures):
- MRQC MedicalCenter grid empty: GetPatList NullReferenceException (live bug).
- MRQC preview blank by design (ViewBag.WriterControlHtml unset on direct GET).
- Sett home IS the MRQC pilot landing page (real content); 2 Sett chart pages
  blank on empty demo data (chart JS throws), 1 partial.
- OR home stub links to non-existent /SystemManage/SysChargeTemplate/Index
  (real page ChargeTemplateIndex shot) — one-line stub-link fix pending decision.
- Herp Sys* redirect-loop menu URLs need a port fix (notice content already shot).
- PDS: 2 Forms return HTTP 200 live though no Form action exists in current
  controller source (running DLL predates source for those actions) — shot, untouched.
- DB/file-sourced Chinese values in grids (dept/drug/diagnosis/surgery names,
  template elements, ICD-10 rows) are DATA, not view failures — DB/enum pass item.
- Login method note: CheckLogin requires FORM-ENCODED POST (JSON-body POST fails).
- Empty grids on empty demo tables are correct everywhere.
- API hosts (5320, 5331–5337) and center hosts (8015, 8016) have no UI — excluded.
- Role limitation: ONLY 000000 (HospAdministrator) usable; xzadmin password
  unknown (never attempted), admin has no role (never attempted).

## Giai đoạn 1+2+parity + đồng bộ + push V2.2 (điều phối viên ghi, 2026-10-27, sau đợt chụp 1009 ảnh)

### Giai đoạn 1 — 7 lỗi chụp màn hình (đã sửa, live-verified, ảnh -fixed, không ghi đè ảnh cũ)
- MRQC GetPatList NRE: đã sửa phòng thủ + Diag/BlTree siblings, live-verified, reshot.
- Herp 5 URL menu Sys* lặp 302: đã sửa bằng redirect literal tới /SystemManage/SystemManage/SysX, live-verified, 5 reshots; parity Herp thay thế kiểm tra lại: 5 URL 302 đúng 1 lần rồi 200, không hồi quy.
- OR home stub link: sửa 1 dòng sang ChargeTemplateIndex, live-verified, 2 reshots.
- SSO UserRoles cây rỗng: sửa GetRoleList (node có id/showcheck/checkstate + getCheckedNodes()), live-verified (5 node, tick đúng); parity SSO kiểm tra lại: không hồi quy. Nút Lưu UserRoles vẫn 404 đúng gốc (SysUserBaseController.UpdateUserRole là code chết, không controller nào kế thừa) — KHÔNG bịa action (mở luồng ghi DB).
- PDS 3 URL (PharmaceuticalStocks, YpckQuery DecoctingfeeQuery/DoctorsOrderQuery): parity PDS kiểm chứng lại bằng git grep toàn cây V2.2 = 0 kết quả — là hàng menu DB chết, gốc cũng không có, KHÔNG bịa action; quyết định còn lại: sửa/xóa dòng menu Sys_Module trong DB (ghi DB, ngoài phạm vi) hoặc chấp nhận 404 trung thực.
- PDS 2 Form 200: sau rebuild thật vẫn 200 (ControllerBase kế thừa OrgControllerBase nhị phân cấp Index()/Form() chung) — giữ nguyên, không thêm action thừa.
- MRMS Report/Index view-not-found: gốc có action, không có view — hành vi trung thực, KHÔNG bịa view; parity MRMS bổ sung 14 Sys* notice-stub routes (sửa lại nhận định cũ "SSO phục vụ chúng" — SSO phục vụ route khác).

### Giai đoạn 2 — Soát tiếng Trung (chuỗi hiển thị trước, comment sau; keeps = logic/SQL/config/.xml template/project/vendor JS/DB data/enum tiền tố số chứng từ/tên riêng vendor)
- Herp Web/API 63 file, 457 dòng (1 hiển thị + 456 comment), 9 keeps; Herp Infrastructure 226 file, 2.210 dòng, 34 keeps (11 enum tiền tố số chứng từ được bảo vệ).
- Sett 746 file; 3.375 + 814 chuỗi; 98 keeps phi-vendor + vendor dc_main.js.
- OR 160 file, 1.092 dòng, 6 keeps. MRQC 31 file, 194 dòng, 13 keeps phi-vendor + vendor JS.
- CIS incl. Infrastructure 1.186 file, ~23.391 comment + ~2.219 hiển thị, 834 keeps (gồm toàn bộ 216 dòng Description enum giữ vì lý do số chứng từ/DB).
- MRMS 82 file, 996 dòng (4 hiển thị + 992 comment), 10 keeps phi-vendor.
- Base/HOSP 509 file, 3.982 chuỗi (366 hiển thị + 3.616 comment), 51 keeps.
- Lib (duy nhất 1 worker) 94 file, 1.578 dòng (211 hiển thị + 1.363 comment), 4 keeps + city.json/DRG data giữ.
- Center 263 file, 954 hiển thị + 3.763 comment, 27 keeps.
- EMR incl. Infrastructure 240 file, 3.780 dòng (275 hiển thị + 3.505 comment), gồm 56 dòng mojibake khôi phục qua dich-tieng-viet.
- PDS 642 file, 8.311 dòng (848 hiển thị + 10.097 comment theo báo cáo worker — số comment vượt tổng dòng do cách đếm worker, ghi nguyên văn), 331 keeps (gồm key djmc số chứng từ được bảo vệ).
- SSO (port vào openhis-core, 267 file) 104 → 5 keeps dòng.
- Hồi quy do soát dịch, phát hiện ở parity và đã sửa: PDS 5 literal djmc frontend '内部发药单' bị dịch nhầm → 'Đơn phát thuốc nội bộ' làm proc sp_yp_Getypdjh lỗi "truncated" (key gốc → success "NBFYD202610270038"); đã khôi phục literal gốc + comment KHÔNG dịch trong 5 file. Đây là bằng chứng: enum không cấp tiền tố là đúng, nhưng literal proc thì có — không dịch/sửa bất kỳ key proc nào nữa cho tới khi proc DB được đổi.
- Sửa thêm của điều phối viên ở quét kiểm chứng: Sett Views/Treatment/AForm.cshtml — modalAlert "TênVàMã số为Bắt buộc项！" → "Tên và Mã số là mục bắt buộc!" (chuỗi hiển thị thật còn sót, không phải logic/SQL).

### Giai đoạn mới — Parity audit theo app (bản gốc V2.2 qua git show V2.2: / bản port trước audit / đã bổ sung / còn thiếu)
- MRQC: field 18/18, view/action/API 100%; 4 Login action bỏ qua có lý do (gồm UALoginPre không credential, không port mù); port còn sửa 1 bug dbo.f_split của gốc. Không fix code.
- Sett: +2 view, +2 controller, +19 action (ComController mới, TreatmentController + views/entity/repo/DI, HomeController.GetNewFieldUniqueValue không [HandlerLogin]); 5 reshots; Sett Web không gọi 8016; sett-stage-* loại khỏi phạm vi (down, không trong sln chính). Lưu ý: [HandlerLogin] trong framework này nghĩa là BỎ QUA kiểm tra login (global filter) — các action Sett không phải Index trả 200 không cookie là đúng gốc, user cần biết. DischargeSettle/GetUploadFeeDetails vẫn là stub chưa chuyển đổi显式.
- MRMS: +14 Sys* notice-stub routes; 8 reshots. MRMS Web Debug build 0 lỗi sau khi git rm 3 file export tên D:\ (từ config MedicalRecordExport="D:\\", giá trị config giữ nguyên — chạy export trên Linux sẽ tái tạo file trong cwd app, là quyết định triển khai).
- OR: field/view/action/API 100% trừ 14 Sys* + 6 portal-frame view đã giải thích; không fix code.
- CIS: field bằng gốc (308 view; input 1362/select 164/textarea 51/name= 801/colModel 309); màn hình 308 view, 60 controller, 706 method gốc → port 694 method, +5 action (Home GetNewFieldUniqueValue/MSGQuery/NoticeReadSync, Login UALogin/ReturnSSOHome); API 13 controller/27 action đủ; flow đủ tới validation gate. Còn thiếu: SyncSysConfigParams (INSERT, SSO phục vụ Sys*, entity thiếu OrganizeId), SSOCallback (SSO 5208 không có GetOpenId), UALoginPre dạng action (cần QuickLoginSuccess), portal frame Home (SSO portal thay thế), lệch [ApiController] trả 400 ProblemDetails thay vì envelope {FIELD}_IS_REQUIRED cho request không hợp lệ (để lại, sửa toàn cục nguy cơ hồi quy). MzBespeakRegister no-token → NRE 20000 là trung thực gốc (không [DefaultAuthorize]).
- EMR: DTO 23 file/544 prop khớp; Entity gốc khớp, port thêm SysConfig/SysUserRole cho API Query; Interface khớp tuyệt đối; view 115/115; controller 18 gốc/242 action → port 24 controller/241 action +1 UALogin (+UALoginPre static, fallback về Login vì thiếu QuickLoginSuccess); API 3 controller/7 action đủ; flow đủ tới validation gate. Fix: 4 chỗ sd thêm || "Tải lên bảo hiểm y tế" (nút upload thật, nhánh cũ chết vì chỉ kiểm tra 上传医保); giữ nguyên so sánh kép 保存文档/Lưu tài liệu và Sửa/修改 (runtime port đúng là tiếng Việt nên khớp); BasyReport stub SSRS tiếng Việt khi ReportServer.HOST trống. DCSoft/SSRS chỉ chạy Windows — giữ stub.
- Herp (worker thay thế re-audit độc lập): DTO/Entity/VO gốc 142 file/1.917 prop, 0 thiếu; Interface 73 file/225 method, 0 thiếu; view gốc 129 → port 110 (24 vắng có lý do: 14 Sys* nhị phân → notice + 6 shim redirect đã verify, 2 _Layout/_ViewStart BillManage, 2 _ViewStart, portal frame do SSO giữ); controller 30 gốc/306 action đủ + 7 thêm có chủ ý; API đủ, GetToken stub 40004 AUTH_NOT_IMPLEMENTED. Fix riêng worker thay thế: 2 dòng viewport (Home/About, Report/PrintReport). Còn: khung portal Home chưa port (SSO giữ), UALogin nhánh đổi token cần UAUserInfoGetAPI + QuickLoginSuccess, SSRS/.rdl/ActiveX/WCF stub.
- Base (HOSP 5233 + Base Web 5238 + HOSP API 5333): field HOSP 2170/2170, Base Web 296/296, DTO API 21/21; view HOSP 178/178, Base Web 50/50; controller 72/72 và 20/20; API 9 controller/22 action đủ. Fix: ReturnSSOHome cho cả 2 web (trước fix 404, sau fix 302 về Login) + GetWebClientIp chỉ HOSP. Không fix: SSOCallback + CheckLogin() GET (SSO không có GetOpenId), CISApiDmnService chết (không caller trong Base), 31 *VEntity không consumer (EF view-mapping, đã thay bằng SqlSugar; SysPharmacyDepartmentVEntity có consumer thì port giữ), Organize/Applications 404 đúng gốc (view mồ côi).
- PDS: view gốc 198 → port 182 (16 view SystemManage thiếu có lý do: gốc không có controller SystemManage trong PDS, route gốc sang FrameworkBase.MultiOrg nhị phân, SSO phục vụ); controller 46/46, action 463/463 tên trùng, 0 lệch chữ ký; API 12 controller/32 action đủ; field/DTO 0 thiếu thật (lệch ban đầu là artifact gộp file trùng basename). Fix: 5 literal djmc (xem trên). 7 ảnh -fixed (2 Form dùng -fixed2). Lưu ý minh bạch: gọi InitDjh (kể cả page load thường) làm proc cấp số tăng theo cơ chế gốc, không tránh được khi kiểm chứng live.
- SSO + BaseAPI: SSO 30/30 view, 30 action con + 26 action base đủ; BaseAPI 26/26 route đủ (Login 3, OrgService 9, SysUser 2, System 10, Systemed 2). Fix: BaseAPI appsettings.json Top_OrganizeId → org demo 6d5752a7-234a-403e-aa1c-df8b45d3469f (trước fix CheckPwd trả 40004 "Không tìm thấy thông tin người dùng"; sau fix trên instance tạm 7002 → 10000 SUCCESS); refresh Lib.Base.dll chỉ vào HIS.BaseAPI/bin/Release (bin gitignored). Không tự cắt BaseAPI chạy sang openhis-core (cần launcher + recovery.sh section 5 mới, xung đột instance 7001 đang phục vụ toàn bộ app — việc của đồng bộ sau, code đã sẵn). GetOpenId: KHÔNG tồn tại ở bất cứ đâu trong source V2.2 (chỉ là lời gọi từ LoginController các module sang SSO API) — thiếu của gốc, không bịa. Captcha giữ nguyên Is_CheckChkCode=false. SSO bin vẫn dùng Lib.Base.dll cũ (chưa refresh vì SSO đang chạy; message SSO đã kiểm chứng đều tiếng Việt).
- Center (worker thay thế): AuthCenter 7 file.cs + WebAPI.Manage host 65 + subproject chung 266 = 338 file, 0 thiếu; API 34 controller/130 action (AuthCenter 5 + WebAPI.Manage 125), 0 lệch route/verb; FIELD 2.600 prop, 0 thiếu; màn hình N/A (API thuần, root / 404 là đúng). Fix duy nhất: copy Lib.Base.dll tươi vào his-dll-common/Net6/ (bin Center cũ dùng DLL 2026-10-26 nên message AppId trả tiếng Trung; sau fix message tiếng Việt + "[Ngoại lệ]:" thay ":"), rebuild Release 0 lỗi cả 2 host, PID mới 35819 (8015) / 35855 (8016). Live: GetAppToken MRQC/CIS/EMR → 10000 + JWT thật, unknown app → 40004 VI; MedicalCenterPatInfo → 10000 + 1 dòng thật; NoticeQuery → 40004 "no notices"; DoctorMeetingApply giả → 40004 VI, không ghi; DRG dummy (không phải bệnh nhân thật, không ghi DB) → 10000, mdc=MDCH, adrg=HU1, drg=HU15, status=分组成功 (chuỗi của binary engine) — engine đã live-test, khác ghi nhận cũ. Sett-area proxy VẪN HỎNG trung thực với gốc: appsettings.Development.json (gốc V2.2) override SiteSettAPIHost thành http://localhost:17361/ (appsettings.json gốc là http://localhost:7002/) — cả hai đều không tồn tại, lỗi thực tế Connection refused (localhost:17361); validation gate trước proxy vẫn đủ và tiếng Việt; còn lại cho user quyết: bịa host Sett-API mới hay để hỏng显式 như gốc. Log center vẫn ghi full JWT (62 dòng khớp eyJ trong authcenter.log, không trích nội dung) — hành vi built-in, cần chú ý lưu trữ log.
- Worker lỗi giữa chừng (không báo cáo, đã giao lại và xong): Herp gốc, Center gốc (cùng mẫu lỗi runtime "background exec disappeared"); các worker thay thế đã re-audit đầy đủ và báo cáo đầy đủ, không dùng kết quả chưa xác nhận của worker lỗi.

### Đồng bộ + quét kiểm chứng CJK của điều phối viên (không giao worker)
- Tất cả 26 port đang LISTEN và trả đúng kỳ vọng ở thời điểm quét: SQL 1433, Redis 6379, static 41142 (200), BaseAPI 7001 (404 ở root — API thuần, đúng), SSO 5208 (200), Web 5220/5221/5231/5232/5233/5234/5235/5236/5237/5238 (200), API 5320/5331–5337 (404 ở root — API thuần, đúng; 5336/5337 trả 200 ở root do route mặc định riêng), Center 8015/8016 (404 ở root — đúng, không có default route). Login 200 với 000000 đã được từng worker kiểm chứng sau fix của mình; điều phối viên không đăng nhập lại toàn bộ (tránh làm phiền instance đang chạy) mà dựa vào bằng chứng live của worker + port/root check này.
- Quét CJK độc lập của điều phối viên trên các file sẽ commit (git ls-files cached + others, 10.917 file; phạm vi.cs/.cshtml/.js/.resx, loại scratch worker/sweep-scripts/base-translate/sett-stage/herp-tmp/pds-work, loại vendor dc_main.js/.min.js/lib, loại template EMR File/ và city.json): 5.360 file trong phạm vi, 446 file còn CJK, 14.357 ký tự. KHÔNG báo 0 một cách gian dối: phần lớn thuộc đúng danh sách keeps đã ghi (SQL literal + giá trị so sánh DB, Description enum, tên báo cáo SSRS, tên font, tên riêng vendor/footer, dữ liệu demo trong view, file ngôn ngữ vendor summernote-zh-CN.js, bản sao tham chiếu chết CIS _porting-src không nằm trong csproj phục vụ, template testbl/MedRecordTemplate). Ngoài keeps, chuỗi hiển thị thật còn sót đã tìm và sửa được 1 (AForm Sett, xem trên); các chuỗi hiển thị thật khác trong.cshtml chính (modalAlert/toast) sau khi sửa AForm đã quét lại = 0. Các file scratch worker (sweep-scripts/*.json, base-translate/*.json, sett-dict.json...) vốn đã tracked từ commit bdbf9bf và chứa từ điển dịch Trung→Việt — KHÔNG xóa trong đợt này (ngoài phạm vi, xóa sẽ làm thay đổi lịch sử làm việc của worker); chúng không phải UI đang chạy.
-.gitignore: đã thêm dòng shots-roles.zip (dòng 28) từ trước — file zip 42MB không vào commit này. shots-roles/*.md vẫn được commit (ghi chú tiến trình).
- Chưa cắt chạy: BaseAPI openhis-core (appsettings đã sửa, bin đã refresh) vẫn chưa thay instance run-copy 7001 đang phục vụ; SSO bin vẫn dùng Lib.Base.dll cũ. Cả hai đều không ảnh hưởng hành vi live hiện tại (run-copy đã có org demo đúng; message SSO đã tiếng Việt) nhưng là việc tồn đọng cần một lần restart có kế hoạch + recovery.sh section 5 mới.

### Tiếng Trung còn tồn tại nhưng NGOÀI phạm vi (cần quyết định của user, không tự sửa)
- Dữ liệu DB: tên khoa/thuốc/chẩn đoán/bệnh nhân, tuổi dạng "25岁", tên nhà thuốc, dropdown 正常出库, dòng Sys_Config "材料费", tên menu Sys_Module, DB VER/lx, tiêu đề chức danh do SQL CASE trả về, PILOT-DEMO rows trong Mr_Apply, thiếu dòng demo xt_brzh, 2 dòng NULL cũ trong mr_rel_dept (2026-10-26 15:37/15:39, không xóa).
- Enum: Description dùng làm tiền tố số chứng từ (PDS djmc, Herp PROTECTED) và mô tả enum CIS/Base giữ vì khớp giá trị DB/gateway; Center Guizhou BHYT enum Descriptions đã dịch ở Center — cần kiểm tra không app/gateway ngoài nào khớp chuỗi tiếng Trung cũ trước khi coi là xong hẳn (mục kiểm tra còn mở từ đợt trước).
- Windows-only: DCSoft editor, SSRS 13.rdl, ActiveX, WCF (Sett Guizhou BHYT, CIS HisApi, Herp/PDS procurement), Office Interop Excel, SignalR 2+OWIN (CIS) — giữ stub thông báo tiếng Việt, không giả vờ chạy được.
- Framework DataAnnotations không có custom ErrorMessage vẫn tiếng Anh; log center ghi full JWT (xem trên).
- GetToken vẫn stub 40004 ở mọi module API cho tới khi user quyết kiểm tra credential thật; Sett-API host (7002/17361) chưa quyết bịa hay để hỏng; nút Lưu UserRoles 404 đúng gốc chưa quyết có thêm action mỏng hay không (thêm = mở luồng ghi DB, cần phê duyệt riêng).
