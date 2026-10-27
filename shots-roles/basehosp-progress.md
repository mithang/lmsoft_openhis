# Base HOSP Web (5233) — role screenshot progress

- App: http://127.0.0.1:5233, login demo `000000` (form-encoded POST /Login/CheckLogin, md5 via md5sum). xzadmin/admin NOT attempted.
- Tool: Playwright 1.64 + Firefox headless, viewport 1366x900 only, cookies reused per page.
- Shots dir: `~/workspace/openhis-core/shots-roles/basehosp-*.png`
- **Shot count: 148 files** = login + home-index + 146 URL shots (of 148 attempted URLs).
- Viewed by worker (≥2, actually 4): `basehosp-login.png` (VI login form, rendered),
  `basehosp-home-index.png` (empty portal frame — BY DESIGN, see below),
  `basehosp-sysmedicine-index.png` (real grid, 304 rows, VI headers),
  `basehosp-project-projecttype.png` (JSON view-not-found body, faithful).
- Full curl status sweep (authenticated, 150 URLs incl. login+home): 148× HTTP 200, 2× HTTP 404.
  Sweep file: `~/workspace/openhis-core/bh-sweep.tsv`; URL list: `~/workspace/openhis-core/bh-urls.txt`.

## Home / portal frame — BY DESIGN, not a failure

Standalone `/Home/Index` shows an empty portal frame (header "Benh vien Newtouch HIS",
user 000000, "Thao tác tab", empty sidebar/body). Its sidebar comes from
`top.clients.authorizeMenu` supplied by the SSO portal host; the frame is designed to
run inside SSO. Screenshot counts as covered. Rendered home links collected in
Playwright: only `#`, `/Login/OutLogin`, `/User/PersonalInfo`, and the vendor external
link `https://open.tntlinking.com/communityTreaty` — NO sidebar menu links and NO
shared-role-menu cross-app routes on this host. So this app does NOT follow the
shared-role-menu pattern: no cross-app 404 sample was applicable, and none of the
DB menu URLs below belongs to another app (all are this app's own routes).

## Menu URLs found (Sys_Module, NewtouchHIS_Base, SELECT-only) — 41 real + 1 bad row

All 41 real distinct menu URLs shot, all HTTP 200:

- /Home/Notice, /Module/Index, /Role/Index, /ItemsData/Index
- /SysMedicineElectronicPrescription/Index, /MedicineSupplier/Index, /PharmacyWindow/Index
- /SysMedicine/Index, /PharmacyDepartment/Index, /PharmacyDepartment/OpenMedicineIndex
- /PharmacyDrugStorage/SysMedicineStorageIOMode/Index
- /AntibioticType/AntibioticTypeQuerry, /SysWardRoom/Index, /Consult/Index
- /Organize/Index, /Department/Index, /Ward/Index, /SysWardBed/Index
- /Staff/Index, /User/Index
- /Sys/Combination/Index, /Sys/Combination/projectIndex, /Sys/Combination/materialIndex,
  /Sys/Combination/CatalogComparison
- /Settlement/GzybBaseInfo/MedicalInsuranceCatalogue, /ReportManagement/Index
- /PharmacyDrugStorage/SysMedicineUsage/Index, /PharmacyDrugStorage/SysMedicineFormulation/Index
- /SysChargeCategoryTypeRelation/Index, /SysTCMSyndrome/Index, /SysDiagnosis/Index
- /SysMedicalOrderFrequency/Index, /SysAgriInsuranceChargeCategory/Index
- /SysMedicalRecordChargeCategory/Index, /ChargeCategory/Index, /SysChargeItem/Index
- /SysMedicineAuthority/Index, /SysMedicineAuthorityRelation/Index
- /SysMedicineClassification/Index, /SysMedicineFormulation/Index, /SysMedicineUsage/Index

Bad menu-data row (NOT a missing screen): DB row "血液类型基础维护" (blood-type maintenance)
has UrlAddress `/1` → HTTP 404, empty body. No such route exists; menu data error, not a view bug.

## Shot (beyond menus) — all HTTP 200

- Home companions: /Home/Default, /Home/About, /Home/UserOrgChoose
- Form companions (no parameters): Module/Form, Role/Form, ItemsData/Form,
  MedicineSupplier/Form, PharmacyWindow/Form, PharmacyDepartment/Form,
  PharmacyDepartment/OpenMadicineForm, SysMedicineStorageIOMode/Form (area),
  AntibioticTypeSettingForm, SysWardRoom/Form, Consult/NormalForm, Consult/ExpertForm,
  Organize/Form, Department/Form, Ward/Form, SysWardBed/Form, Staff/Form, User/Form,
  ReportManagement/Form, ReportManagement/MianForm, SysMedicineUsage/Form (area),
  SysMedicineFormulation/Form (area), SysMedicineFormulation/CorrelationUsage,
  SysMedicineClassification/Form (area), SysMedicineUnit/Form (area),
  SysChargeCategoryTypeRelation/Form, SysTCMSyndrome/Form, SysDiagnosis/Form,
  SysDiagnosis/Surgery, SysDiagnosis/SurgeryFrom, SysMedicalOrderFrequency/Form,
  SysAgriInsuranceChargeCategory/Form, SysMedicalRecordChargeCategory/Form,
  ChargeCategory/Form, SysChargeItem/Form, SysChargeItem/ybbxblForm,
  SysMedicineAuthority/Form, SysMedicineAuthorityRelation/Form,
  SysChargeClassification/Form, SysMedicine/SysMedicineAdd,
  Settlement/GzybBaseInfo/Index, Settlement/SysDiagnosis/Index,
  Settlement/SysTCMSyndrome/Index + Form, CommonLibrary/Index,
  CommonLibrary/ChargeItemForm, DataMedicalInsurance/Index, Demo/Index, Duty/Index,
  ItemsType/Index + Form, ModuleButton/Index + Form, SysNation/Index,
  SysNationality/Index, RehabChargeClassification/Index + Form,
  RehabChargeItem/Index + Form, RehabChargeItemComparison/Index + Form,
  Sys/Application/Index + Form, Sys/Duty/Index + Form, Sys/ItemsData/Index + Form,
  Sys/ItemsType/Index + Form, Sys/Organize/Index + Form,
  Sys/SysGlobalConfig/Index + Form, Sys/SysNation/Index + Form,
  Sys/SysNationality/Index + Form, ProductManage/ProductType/Index + Form,
  ProductManage/ProductUnit/Index + Form, ProductManage/ProductCrkfs/Index + Form,
  AccessManage/Registor/Index + Form
- Extra companions: Staff/Selector, Staff/Duties, Staff/ConsultInfo, User/Selector,
  User/PersonalInfo, User/Roles, User/CorrelationPharmacy, Module/Details,
  Department/Details, Department/WardInfo

## Failed / no screenshot (2) — with evidence, not stalled on

- /Role/Users — curl HTTP 404, empty body. No `Users` action exists in RoleController
  (view file Views/Role/Users.cshtml exists but no action serves it). Playwright+Firefox
  fails this URL twice with NS_ERROR_NET_EMPTY_RESPONSE (curl gets a clean 404); app
  stays healthy afterwards. Extra companion only, NOT a menu URL.
- /1 — curl HTTP 404, empty body; same Firefox empty-response behaviour. Nonsense
  Sys_Module UrlAddress (see above), not a real route.

## Faithful view-not-found (recorded once each, NOT new bugs)

HTTP 200 + JSON error body "The view '…' was not found", searched locations
/Views/Project/*.cshtml + /Views/Shared/*.cshtml; no such folder in the ORIGINAL tree
either. Screenshots show the JSON error body:

- /Project/ProjectType, /Project/ProjectTypeForm, /Project/ProjectTypeChildForm

## Needs-parameter list

None. Every shot URL loaded without parameters; no page required a parameter to render.

## Notes (not failures)

- Vendor logo/footer proper nouns are keeps (login page, home footer).
- DB-sourced row values in Chinese are DATA (e.g. SysMedicine grid drug names), not view failures.
- Empty grids on empty demo tables are correct where they occur.
- No Vietnamese stub/notice pages were encountered on this app (no explicit stubs to shoot).

## Restarts

- Mid-task VM restart (~02:54 UTC, uptime showed 1 min) killed all services and wiped
  /tmp, losing the first run's results file after 120 shots (shots themselves survived
  in the workspace). Stack restored with `bash ~/workspace/recovery.sh` (restarts only
  down services); 5233 verified 200 afterwards. The 30 tail URLs were then re-shot;
  only the 2 known 404s above failed. No other app was touched or restarted by hand.
- No rebuilds, no DB writes (SELECT only), no git operations.

## Soát tiếng Trung toàn Base/HOSP — 2026-10-27 (worker Base, chỉ sửa text, không build/khởi động/commit/ghi DB)
- Phạm vi: toàn bộ Newtouch.HIS.Base (HOSP.Web, Base Web, HOSP.API 5333, Infrastructure); 740 file diện quét (.cshtml/.cs/.js không vendor-min/.config; không có .resx). SSO: KHÔNG có project/file SSO nào trong Newtouch.HIS.Base (sln chỉ có 4 project trên) — SSO nằm ở ~/workspace/lmsoft_openhis_run/Newtouch.HIS.SSO/. BaseAPI cổng 7001: KHÔNG nằm trong Base, nằm ở ~/workspace/lmsoft_openhis_run/Newtouch.HIS.SSO/HIS.BaseAPI — không quét (chú ý: run-base-api.sh trong openhis-core thực chất là HOSP.API cổng 5333, không phải BaseAPI 7001).
- Snapshot trước sửa: /tmp/base-cjk-before.txt (3.146 dòng chứa CJK, 19.921 ký tự CJK). Đếm theo subproject — HOSP.API: 32 dòng; HOSP.Web: 1.342 dòng; Infrastructure: 1.609 dòng; Base Web: 163 dòng.
- Đã đổi: 510 file (run 1: 509 file + 3.950 chuỗi; run 2–3 vá thêm 11 file/32 chuỗi sau khi phát hiện 2 lỗi bộ áp dụng: Delete( bị nhận nhầm là từ khóa SQL; dòng verbatim bắt đầu cùng dòng với từ khóa SQL không bật trạng thái verbatim). Tổng chuỗi hiển thị đã dịch: 366; comment đã dịch: 3.616; tổng cộng 3.982 chuỗi (run 1: 3.950; run 2: 30; run 3: 2).
- Sau sửa: còn 51 dòng chứa CJK, toàn bộ thuộc danh sách keeps đã liệt kê (SQL/identifier/enum/logic/config/vendor/font/tên file giao tiếp BHYT) — xem chi tiết trong báo cáo gửi điều phối viên; không còn chuỗi hiển thị hay comment tiếng Trung nào ngoài keeps.
- Tự kiểm chứng: số dòng mỗi file không đổi so với snapshot (wc -l khớp 740/740 file); không thay đổi token code (đếm ký tự "/{}/()/[]/; trên các cặp dòng diff = 0 lệch); file GBK duy nhất (indextab.js ×2, HOSP.Web + Base Web) đã chuyển sang UTF-8 kèm backup gốc tại /tmp/base-gbk-backup/ (lý do: trang UTF-8 nạp file GBK sẽ hiển thị lỗi font; sau chuyển đổi file decode UTF-8 sạch, 296 dòng không đổi).
- Dữ liệu tiếng Trung từ database (chỉ ghi nhận từ code, KHÔNG sửa code để che, không truy vấn DB trong đợt này): tên menu Sys_Module so sánh trong SysModuleDmnService (系统管理/角色管理/...); Sys_Items like '%医保科室%'; giá trị DB VER='无', lx='项目'; tiêu đề chức danh do SQL CASE trả về (科主任/护士长/康复治疗师长); tên file giao tiếp BHYT Quý Châu (中心ICD10数据). Ngoài ra phát hiện: SysMedicineDmnService.cs:313 có định danh cột DB bị dịch dở từ đợt trước ([Ghi chú（特殊属性标识）] bztssxbs) — giữ nguyên, cần user/điều phối viên xác minh tên cột thật trong DB trước khi đụng vào.
- Chưa build/khởi động lại app theo đúng giới hạn nhiệm vụ — coordinator cần rebuild + restart + verify live trước khi push V2.2.

## Parity audit Base/HOSP — 2026-10-27 (worker Base, chỉ sửa trong Newtouch.HIS.Base, không commit/push/ghi DB)
- Đối chiếu: bản gốc lấy bằng `git show V2.2:<path>` / `git archive V2.2 Newtouch.HIS.Base` (checkout lmsoft_openhis đang ở dich-tieng-viet nên không dùng working-copy để đếm).
- HOSP Web: controller 72/72 file (1 file chỉ đổi vị trí: Notice/NoticeController.cs gốc → Controllers/NoticeController.cs port, route api/notice giữ nguyên); action 390 gốc → 384 port, lệch duy nhất ở LoginController (xem dưới); view 178/178 gốc đều có mặt, port thừa 6 file _ViewImports.cshtml (hạ tầng Razor, không phải trang); field token trên view (input/select/textarea + tên cột jqGrid) 2170/2170, 0 view lệch số lượng.
- Base Web: controller 20/20; action 86 gốc → 81 port, lệch duy nhất ở LoginController; view 50/50 đều có mặt (+1 _ViewImports.cshtml); field token 296/296, 0 view lệch.
- HOSP API: controller 9/9, action 22/22, route khớp hết; tham số chỉ khác ở attribute [FromBody] (tương đương binding của Core, không mất tham số); DTO request 8 file/21 thuộc tính khớp bản gốc 9 file (trừ AssemblyInfo.cs)/21 thuộc tính; ItemsController gốc đã không có action nào nên port không expose endpoint là đúng gốc; envelope 10000/40004 + jsonpCallback overload có mặt (bản gốc HOSP.API cũng gọi CreateResponse thường cho các Query, không wrap jsonp — port khớp).
- Infrastructure: entity phi-V 75/75 class khớp thuộc tính từng cái (0 lệch); 31 class *VEntity gốc không có trong port nhưng 0 consumer ngoài Domain (chỉ 1 VEntity có consumer thật là SysPharmacyDepartmentVEntity thì port vẫn giữ); DmnServiceBase có ở Infra root; CISApiDmnService không có trong port nhưng không có caller nào trong Base (consumer duy nhất trong toàn cây gốc là CIS MedicalRecordController gọi qua HTTP tới route /api/MedicineStorageIOMode/GETYNMidecine — route này bản gốc HOSP.API cũng không có, chỉ có Query — nên không phải gap của Base).
- FIX 1 (cả 2 web): thêm LoginController.ReturnSSOHome theo đúng mẫu CIS đã port (xóa operator + cookie LoginFlag/LoginFromFlag, redirect tới SSOHomeHost nếu cấu hình, không thì về Login/Index). Trước fix: Views/Home/Index cả 2 web đều link tới ~/Login/ReturnSSOHome nhưng action không tồn tại → curl 404 cả 5233 và 5238. Sau fix: 302 về / (trang Login), không còn 404. File đổi: Newtouch.HIS.Base.HOSP.Web/Controllers/LoginController.cs, Newtouch.HIS.Base.Web/Controllers/LoginController.cs.
- FIX 2 (chỉ HOSP): thêm LoginController.GetWebClientIp (X-Forwarded-For, không thì RemoteIpAddress) — bản gốc Base Web không có action này nên không thêm ở Base Web. Live: GET /Login/GetWebClientIp → 200, body "127.0.0.1".
- KHÔNG port (lý do rõ): SSOCallback + CheckLogin() GET không tham số — cặp này chỉ phục vụ SSO callback qua endpoint GetOpenId mà SSO 5208 bản port không có (cùng lý do CIS đã ghi); login SSO trong bản port đi qua SSO + CheckLogin POST → BaseAPI, đã verify live.
- Live sau rebuild Release 0 lỗi cả 2 web + restart đúng PID (5233 PID 19968, 5238 PID 19969; API 5333 PID 3581 không động tới): Login/Index 200 cả 2; CheckLogin 000000 (md5 như JS trang login) → {"state":"success","message":"Đăng nhập thành công"} cả 2; Home/Index authed 200 cả 2.
- Flow tới validation gate (không ghi DB): HOSP /PharmacyDrugStorage/SysMedicineClassification/Index 200 + GetGridJson trả rows thật, Form 200, SubmitForm rỗng → {"state":"error","message":"Vui lòng nhập mã"}; Base Web /Organize/Index 200, Form 200, SubmitForm rỗng → {"state":"error","message":"Mã không được để trống"}; API POST /api/MedicineSupplier/Query đủ Timestamp → {"code":10000,"data":[]}; thiếu Timestamp → 40004 TIMESTAMP_IS_REQUIRED; POST /api/Auth/GetToken → 40004 GETTOKEN_DISABLED (stub giữ nguyên, không bật lại); POST /api/Ward/UpdateBedOccupy rỗng → validation cwCode required.
- Ảnh -fixed (Playwright + Firefox, 1366x900, không ghi đè ảnh cũ): basehosp-login-fixed.png, basehosp-home-index-fixed.png, baseweb-login-fixed.png, baseweb-home-index-fixed.png. Đã tự xem basehosp-home-index-fixed.png: header tiếng Việt, user 000000, giống hệt ảnh gốc (cùng sha256 với basehosp-home-index.png — render sidebar trống là hiện tượng headless cũ, không phải do rebuild).
- Không fix được / không thuộc phạm vi: dữ liệu tiếng Trung từ DB/enum không sửa (gặp đúng các case đã ghi ở đợt soát: tên menu Sys_Module, DB VER/lx, tiêu đề chức danh SQL CASE — báo riêng, không sửa code để che); Organize/Applications 404 của Base Web là đúng gốc (view mồ côi, bản gốc cũng không có action) nên giữ nguyên.
- Không commit, không push, không ghi DB (mọi SubmitForm/UpdateBedOccupy chỉ tới cổng validation và trả lỗi; GetGridJson/Query chỉ đọc).
