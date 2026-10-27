# Herp Web (127.0.0.1:5236) — role screenshots, 2026-10-27

Login: demo account 000000 only (form-encoded POST /Login/CheckLogin,
password = md5("000000") computed with md5sum). xzadmin/admin NOT attempted.
Tool: own script /tmp/shot_herp.py, Playwright 1.64 + Firefox headless,
viewport 1366x900 only (never full_page). No rebuilds, no restarts, no DB writes.

## Counts

- Shots in ~/workspace/openhis-core/shots-roles/herp-*.png: **96 files**
  (login + home + 94 page shots; 1 file shared between forms/extras lists)
- Menu URLs attempted: 65 (61 DB Sys_Module rows with zt=1 + non-empty
  UrlAddress, + 4 rendered-sidebar area entries not in DB rows) → **58 shot (200)**
- Form companions attempted: 30 → **29 shot (200)**
- Equivalent/notice URLs attempted: 8 → **8 shot (200)**
- Failed with reason: **7 menu URLs + 1 form URL** (below)
- Needs-parameter: **none found** — every Form companion loaded without parameters.
  Pages needing parameters (detail views with ids) were not in scope.
- Restarts: **none** — Herp 5236 was UP the whole run (PID from recovery launch).
- Viewed by me: herp-login.png, herp-home-index.png,
  herp-PurchaseManage-Purchase-Index.png,
  herp-SystemManage-SystemManage-SysModule.png — all rendered, not blank,
  not error pages. Empty grids ("Không tìm thấy dữ liệu liên quan!") are
  correct on the empty demo DB, not failures.

## Enumeration sources (deduplicated)

1. Rendered home sidebar (13 links, all Herp's own area entries — NO shared
   BaseAPI cross-app role menu on this host, so no cross-app 404 pattern to
   sample; the PDS lesson does not apply here):
   /WarehouseManage/Warehouse/Index, /BillManage/ApplyBill/Index,
   /DeliveryManage/Delivery/Index, /DepartmentManage/Department/Index,
   /LicenceManage/Licence/Index, /ProductManage/Product/Index,
   /PurchaseManage/Purchase/Index, /StorageManage/Storage/Index,
   /SupplierManage/Supplier/Index, /SystemManage/System/Index,
   /Report/Index, /Home/KfChange, /Login/OutLogin.
2. DB Sys_Module (NewtouchHIS_herp, SELECT only, zt=1, UrlAddress non-empty):
   61 distinct URLs across 10 areas (BillManage, DeliveryManage,
   DepartmentManage, LicenceManage, ProductManage, PurchaseManage,
   StorageManage, SupplierManage, SystemManage, WarehouseManage).

## Menu URLs shot (58, all HTTP 200)

- BillManage: ApplyBill/Query, ApplyBill/Index (sidebar), OutOrInStorageBill/Approval,
  OutOrInStorageBill/MyTemporaryCrkdj, OutOrInStorageBill/Query,
  PurchasingOrder/AuditPurchaseOrder, PurchasingOrder/GeneratingPurchaseOrder,
  PurchasingOrder/PurchaseOrderQuery, PurchasingPlan/AuditPurchasingPlan,
  PurchasingPlan/FillPurchasingPlan, PurchasingPlan/PurchasingPlan,
  PurchasingPlan/PurchasingPlanQuery
- LicenceManage: Licence/Index, Licence/LicenceBelonged, Licence/LicenceType
- ProductManage: PriceAdjustment/PriceAdjustmentApproval,
  PriceAdjustment/PriceAdjustmentHistory,
  PriceAdjustment/PriceAdjustmentProfitAndLossQuery,
  PriceAdjustment/PriceAdjustmentRequest, Product/Index, ProductCrkfs/Index,
  ProductSfxm/Index, ProductType/Index, ProductUnit/Index
- PurchaseManage: Purchase/Bill, Purchase/BillPay, Purchase/Delivery,
  Purchase/Index, Purchase/LocationIndex, Purchase/QueryYY151,
  Purchase/QueryYY152, Purchase/QueryYY154, Purchase/QueryYY157,
  Purchase/QueryYY159, Purchase/QueryYY160, Purchase/QueryYY161,
  Purchase/QueryYY162, Purchase/QueryYY164, Purchase/ReIndex
- StorageManage: PSIStatistics/Index, ProfitAndLoss/Index,
  ProfitAndLoss/QueryProfitAndLoss, ProfitAndLoss/Syyy,
  StockCarryOver/Index, StockInventory/Index, Storage/ApplyOutStock,
  Storage/DeliveryOfReturnInlineEdit, Storage/DeliveryToDepartmentInlineEdit,
  Storage/DepartmentApply, Storage/DirectDeliveryInlineEdit,
  Storage/ExpiredStorageQuery, Storage/InStorageInlineEdit,
  Storage/OutStorageInlineEdit, Storage/WarehouseStorageQuery,
  Storage/Index (sidebar)
- SupplierManage: Supplier/Index
- WarehouseManage: Warehouse/Index, Warehouse/WarehouseProductQuery

## Form companions shot (29, all HTTP 200)

Department/Form, Department/Index, Licence/Form, Licence/LicenceTypeForm,
Licence/LicenceBelongedForm, Product/Form, ProductCrkfs/Form,
ProductSfxm/Form, ProductType/Form, ProductUnit/Form, Supplier/Form,
Warehouse/Form, Warehouse/ProductUnit, Warehouse/SynceProduct, Purchase/Form,
Purchase/ReForm, Purchase/BillForm, Purchase/DeliveryForm, Purchase/BillPayForm,
Purchase/LocationForm, ProfitAndLoss/SyyyForm, StockInventory/InventoryAction,
StockInventory/InventoryQuery, PurchasingOrder/PurchaseView,
PurchasingOrder/PurchaseDetailView, Home/KfChange, Home/About,
SystemManage/SystemManage/Index, Report/Index (SSRS notice page).

## Equivalent/notice URLs shot (8, all HTTP 200) — cover the failed Sys* content

SystemManage/SystemManage/Index (already in forms),
SystemManage/SystemManage/SysModule, SysRole, SysUser, SysLog, SysConfig,
SysStaff (VI notices: system-management functions are served by SSO),
DeliveryManage/DeliveryQuery/Index (the real DeliveryManage controller;
sidebar's /DeliveryManage/Delivery/Index does not exist).

## Failed with reason (8)

1. /SystemManage/SysModule/Index, /SystemManage/SysRole/Index,
   /SystemManage/SysUser/Index, /SystemManage/SysLog/Index,
   /SystemManage/SysConfig/Index — REDIRECT LOOP (Playwright
   NS_ERROR_REDIRECT_LOOP; curl: 302 to /SystemManage/SysModule etc.,
   which 302s to itself). Root cause (read-only inspection, NOT fixed —
   fixing means rebuilding over a running app): Sys*Controller.Index()
   does RedirectToAction("<SysX>","SystemManage"), which resolves to
   area=SystemManage, controller=SysX, action=Index (itself) instead of
   the intended notice action at /SystemManage/SystemManage/SysX.
   The intended VI notice pages ARE shot (see equivalents above), so the
   content is covered; only the DB menu URL itself loops. Follow-up fix:
   redirect to action "SystemManage" controller route or return the
   notice view directly.
2. /DeliveryManage/Delivery/Index — HTTP 404, empty body. No
   DeliveryController exists in this area (only DeliveryQueryController).
   Stale sidebar link; real page /DeliveryManage/DeliveryQuery/Index shot.
3. /SystemManage/System/Index — HTTP 404, empty body. No SystemController
   exists (controller is SystemManageController). Stale sidebar link;
   real page /SystemManage/SystemManage/Index shot.
4. /SystemManage/SystemManage/Notice — HTTP 404. Not a routable page:
   Notice(title, originalUrl, description) is a private helper of
   SystemManageController, not an action. Not a missing screen.

## Notes (not failures)

- Login page renders Vietnamese, but vendor logo/footer Chinese text
  (天开开源 / copyright line) is vendor branding in the static login page,
  not a view failure.
- Home dashboard renders Vietnamese with 0 counts (empty demo DB).
- Known honest stubs were NOT re-triggered via clicks (no writes):
  SSRS Report/Index notice page shot; WCF/ProcessorFun error paths not
  exercised (would require submitting procurement/bill actions).
- Enum receipt-number prefixes (protected) not treated as failures.
- No cross-app 404 pattern: home sidebar contains only Herp routes.

## Fix 2026-10-27: 5 URL menu Sys* hết lặp chuyển hướng (đã live-verify)
- Phần sửa dở ban đầu (6 file, đã có sẵn trong working tree khi nhận việc): các
  Sys*Controller đổi RedirectToAction("<SysX>","SystemManage") thành
  Redirect("/SystemManage/SystemManage/<SysX>") — đúng hướng, giữ nguyên và
  hoàn thiện bằng rebuild + restart (không revert, không làm lại). Nguyên nhân
  gốc: route "default" ({controller}/{action}) đăng ký trước route areas nên
  RedirectToAction sinh URL rơi về chính controller Sys* gây vòng lặp 302.
- Build Release: 0 errors (9 warnings MSB3277 có sẵn, không liên quan).
  Khởi động lại đúng PID cũ 10847 -> PID mới 12580, cổng 5236 UP,
  login 000000 -> /Home/Index, trang Login 200.
- Live sau sửa (Playwright, đã đăng nhập 000000, mỗi URL chỉ 1 lần 302 rồi 200):
  /SystemManage/SysModule/Index -> /SystemManage/SystemManage/SysModule (200,
  "Menu hệ thống"); SysRole -> .../SysRole (200, "Quản lý vai trò");
  SysUser -> .../SysUser (200, "Người dùng hệ thống");
  SysLog -> .../SysLog (200, "Nhật ký hệ thống");
  SysConfig -> .../SysConfig (200, "Cấu hình hệ thống").
  URL menu gốc giữ nguyên, không đổi liên kết menu. Các action phụ cùng pattern
  (Form, TemplateIndex/Form, PersonalInfo, Roles, Selector, SyncForm) cũng đã
  đổi sang Redirect literal trong cùng 6 file.
- Ảnh -fixed (viewport, không ghi đè ảnh cũ, đã tự xem herp-sysmodule-fixed.png
  và xác nhận nội dung thông báo tiếng Việt đúng): herp-sysmodule-fixed.png,
  herp-sysrole-fixed.png, herp-sysuser-fixed.png, herp-syslog-fixed.png,
  herp-sysconfig-fixed.png.
- Không commit/push, không ghi database, không động app khác. Tồn đọng: không
  còn vòng lặp ở 5 URL này; nội dung vẫn là trang thông báo (theo thiết kế
  port — chức năng quản lý hệ thống thật nằm ở cổng SSO), không phải CRUD thật.

## Soát tiếng Trung Herp (Web + API) — 2026-10-27 ~11:30 ICT
- Phạm vi: Newtouch.Herp.Web + Newtouch.Herp.API (176 file .cshtml/.cs/.js/.resx; API đã sạch CJK từ trước, 0 dòng).
- Trước: 460 dòng chứa CJK (UTF-8) + 6 dòng trong 1 file GBK; sau: chỉ còn 9 dòng keeps (xem dưới).
- Đã đổi: 63 file, 457 dòng — 1 chuỗi hiển thị (title "Chuyển kho" trong index.js) + 456 dòng comment.
  Không build, không chạy app, không commit, không ghi DB, không sửa SystemManage (6 file
  SystemManage trong git status là của worker fix redirect trước đó, không phải đợt này).
- index.js (wwwroot/Content/js/index.js) vốn mã hóa GBK: đã chuyển sang UTF-8 vì các trang nạp
  nó đều là UTF-8 (GBK không mã hóa được tiếng Việt có dấu; tiêu đề cũ sẽ hiển thị mojibake).
  Các file UTF-8 khác giữ nguyên encoding (page.common.js giữ BOM).
- Keeps (9 dòng, không sửa): font "微软雅黑" (Error.cshtml); footer vendor + tên giấy phép
  (Login/Index 85–86); tên bệnh viện demo "上海明德五官科医院" (Form 59, Index 148,
  PurchaseView 90); URL SSRS đã comment chứa tên báo cáo "科室备药退回单" (Index 214,
  ReIndex 191); so sánh logic với giá trị DB syyy === "其他" (ProfitAndLoss/Index 627).
- Ngoài phạm vi, chưa sửa: Newtouch.Herp.Infrastructure còn 226 file / 2243 dòng CJK
  (comment + mô tả entity/enum, gồm tiền tố số chứng từ được bảo vệ như "采购计划");
  Log4Net.config ×2 (22 dòng mỗi file: comment + nhãn mẫu log); login.css (tên font).
  Dữ liệu tiếng Trung từ DB quan sát được trong code: chỉ có 1 literal là "其他" (loại
  nguyên nhân hao hụt/thặng dư); còn lại tên kho/vật tư/nhà cung cấp hiển thị từ DB,
  cần quyết ghi DB mới dịch được.
- Tự kiểm chứng: số dòng cả 176 file không đổi; diff (trừ file SystemManage của worker
  khác và index.js đã kiểm riêng) chỉ chạm các dòng CJK trong snapshot
  /tmp/herp-cjk-before.txt; quét lại CJK = 9 dòng keeps trên.

## Soát tiếng Trung Infrastructure — 2026-10-27 (worker infra, không build/commit)
- Phạm vi: `Newtouch.HIS.Herp/Newtouch.Herp.Infrastructure` (307 file .cs nguồn, trừ bin/obj) + 6 controller SystemManage chỉ quét.
- Trước: 226 file / 2.243 dòng chứa CJK / 12.995 ký tự CJK / 917 chuỗi riêng biệt; tất cả UTF-8, không file nào vướng encoding (không chuyển đổi encoding, không backup chuyển đổi).
- Sau: 226 file đổi, 2.210 dòng đổi, CJK còn 34 dòng keeps (xem dưới); số dòng mỗi file không đổi; replay từ backup khớp 100%, không dòng nào ngoài tập CJK ban đầu bị chạm.
- Chuỗi hiển thị đã dịch (105 dòng chứa chuỗi): 6 Description ActResult (Thực thi thành công/Thành công một phần/Xác thực thất bại/Lỗi xử lý nội bộ/Lỗi khác), 5 thông báo Exception, 85 tiền tố Trace log Sunshine, 8 ghi chú `--` trong chuỗi SQL, 1 ghi chú cuối dòng EnumDB:148.
- Comment đã dịch: 2.105 dòng comment thuần + 9 dòng vừa chuỗi vừa comment (code bị comment out, dịch vì không chạy) + phần comment của các dòng code khác; tổng ~2.730 lượt chuỗi trong comment (kể cả tên #region, ghi chú `--` SQL, ghi chú cuối dòng sau case).
- Keeps (34 dòng, 3 file): 11 literal Description PROTECTED của `EnumOutOrInStorageBillType` trong EnumDB.cs (外部入库, 外部出库, 直接出库, 申领出库, 内部退货, 报损报溢, 出库至科室, 科室申领, 科室申领出库, 采购计划, 采购订单 — khóa số chứng từ, cảnh báo PROTECTED trong EnumDescriptionExtensions.cs giữ nguyên; chỉ comment /// xung quanh được dịch, không trích lại literal); 22 giá trị `jymc` live gửi nền tảng mua sắm Sunshine trong SunPurchaseDmnService.cs (tên giao dịch đối tác, đổi sẽ hỏng tích hợp); 1 giá trị payload gửi đi `head.BZXX = "备注信息"`; 1 tên stored procedure DB `exec herp_物资同步到收费项目`. Không có chuỗi so sánh logic bằng tiếng Trung trong phạm vi.
- 6 file SystemManage: 0 CJK từ trước (chỉ là shim redirect tiếng Anh), không sửa gì; lưu ý chúng đã Modified trong working tree từ trước, không phải do worker này.
- Dữ liệu tiếng Trung từ DB quan sát được trong phạm vi code: không đọc DB; chỉ thấy qua code là tên procedure `herp_物资同步到收费项目` và các khóa số chứng từ enum trên (dùng kèm mẫu `ckZKS...`); không có câu SQL nào so sánh giá trị tiếng Trung trong DB. Quyết dịch dữ liệu DB vẫn chờ user.
- Chữa nối từ: 305 dòng được chèn khoảng trắng tại biên CJK↔ASCII (vd `Top_OrganizeId Chưa cấu hình`, `...mua sắm url`, `Vật tư ID`, `bằng 0`) vì tiếng Việt cần khoảng trắng mà tiếng Trung không có.
- Chưa làm theo yêu cầu: không build, không chạy app, không commit, không ghi DB. Snapshot: /tmp/herp-infra-cjk-before.txt, /tmp/herp-infra-wc-before.txt; backup bản gốc: /tmp/herp-infra-backup/.

## Kiểm tra đầy đủ chức năng (parity audit) Herp — 2026-10-27 ~12:30 ICT (worker audit, không commit/push, không ghi DB)
- Đối chiếu với gốc V2.2 qua `git show V2.2:<path>` (không dùng working-copy cây gốc).
- FIELD: DTO/Entity/VO 136 file, 1925/1925 thuộc tính khớp tên (0 thiếu, 0 thừa); Repo interface 38 file, 62/62 method; App interface 18 file, DmnService interface 17 file — đủ tên method. Field trên view chung: chỉ lệch 1 name thật — Supplier Form gốc `name="shsh"` không khớp entity `sh` (lỗi gốc, không bind được); bản port dùng `name="sh"` nên bind đúng, coi là sửa lỗi chứ không phải thiếu field. Các "thiếu" còn lại trong quét token đều là tiêu đề cột đã dịch sang tiếng Việt (VD cột "外部入库" → "Nhập kho bên ngoài"), không mất cột.
- MÀN HÌNH: gốc 129 view; bản port có 110 file view. 19 file chênh: 15 view Sys* gốc (SysConfig/Index+Form+TemplateIndex+TemplateForm, SysLog/Index, SysModule/Index+Form, SysRole/Index+Form, SysStaff/Selector, SysUser/Index+PersonalInfo+Roles+Selector) vốn do controller nhị phân FrameworkBase phục vụ, bản port chủ ý thay bằng trang thông báo tiếng Việt qua SystemManageController + 6 shim redirect (đã live-verify lại đợt này: 5 URL menu + TemplateIndex/TemplateForm/Selector/PersonalInfo/Roles đều 302 một lần rồi 200 nội dung thông báo, không hồi quy vòng lặp); 2 file _Layout/_ViewStart của BillManage không còn cần (view dùng trực tiếp ~/Views/Shared/_Index.cshtml); Home/Default, UserOrgChoose và 4 partial khung portal gốc không port (xem tồn đọng); _ViewStart root thay bằng _ViewImports. View thừa có chủ ý: SystemManage/Index + Notice, _ViewImports ×2, StorageManage _MiddleButtonsView.
- CONTROLLER: 28 controller chung gốc 299 action (đã trừ action bị comment trong gốc như QueryYY155/158, đã cộng dạng `new IActionResult`) — bản port 299/299, không thiếu action nào. Ngoại lệ duy nhất trước audit: LoginController thiếu UALogin (OutLogin và GetUALoginAddres được kế thừa từ LoginBaseController nên vẫn chạy). DepartmentController thừa Index/Form so với gốc (gốc có view mà không có action — bản port bổ sung đúng), HomeController thừa Index (gốc kế thừa từ base Framework).
- API: 2 controller gốc, đủ cả: Auth/GetToken (POST) + Default/Query (POST) + scaffold Get/Post/Put/Delete; request DTO 3/3 (AuthGetTokenRequest rỗng đúng gốc, DefaultQueryRequest.keyword, OrgJSONRequestBase.OrganizeId [Required]); envelope SUCCESS=10000 / FAIL=40004, jsonpCallback bọc đúng. GetToken GIỮ NGUYÊN stub 40004 (sub_code AUTH_NOT_IMPLEMENTED, không cấp token giả) — không bật lại.
- FLOW (trước audit): 3 luồng ghi bị stub dù dependency đã có sẵn trong bản port (comment stub đã cũ): duyệt phiếu (OutOrInStorageBillApprovalApp), phát hàng theo phiếu lĩnh (ApplyBillApp.SubmitApplyOutStock), xuất kho tới khoa (StorageController.SaveDeliveryToDepartment). Đã port/fix trong đợt này (xem dưới). Các luồng đã đủ từ trước: đăng nhập → Home dashboard → danh sách → form → lưu (SubmitDepartmentApply, SubmitLocation, SubmitForm các master) và tra cứu → chi tiết; SSRS giữ stub thông báo tiếng Việt (13 .rdl gốc, không giả chạy); WCF Sunshine giữ nguyên đường gọi (SunPurchaseDmnService).
- FIX đợt này (chỉ phạm vi Herp):
  1. Port ProcessorFun<T> + ProcessException (mới: Infrastructure/Application/Implementation/ProcessorFun.cs, Infrastructure/ExException/ProcessException.cs; bỏ wrapper PerformanceMonitoring nhị phân và HttpContext, giữ nguyên thứ tự Validata→BeforeAction→Action→AfterAction→AsyncAction và mapping lỗi Failed→ValidationFailure/Process→InternalAbnormality/khác→Other).
  2. Port 4 process duyệt phiếu (mới: BillApprovalProcess/WbckProcess, WbrkProcess, DirectDeliveryProcess, DeliveryOfReturnProcess.cs; logic + nhánh Validata y gốc, thông điệp tiếng Việt, dependency chuyển từ field-injection nhị phân sang constructor-injection) và nối OutOrInStorageBillApprovalApp.Approval theo đúng switch djlx gốc (Wbrk=1, Wbck=2, Zjck=3, Nbth=5; default → "Không tìm thấy quy trình duyệt phù hợp").
  3. Port DeptApplyOutStockProcess (mới) + viết lại ApplyBillApp.SubmitApplyOutStock đúng bản gốc (1 phiếu xuất/kho, kiểm tra tồn kho, chia lô theo hạn dùng, chạy process; số phiếu tiền tố CKZKS như ReceiptNoHelper phía Web vì pinyin GBK không chạy trên Linux; khôi phục Id long sau Insert bằng cách đọc lại theo Pdh vì repo SqlSugar ExecuteCommand không trả identity).
  4. Port DeliveryToDepartmentProcess (mới) + nối StorageController.SaveDeliveryToDepartment đúng bản gốc (Process → nếu thành công gọi SynchWz → Success/Error; crkdjmx null được giữ null để Validata báo lỗi thay vì NRE như gốc).
  5. Khôi phục LoginController.UALogin với các nhánh không cần máy chủ UA ngoài (thiếu token → trang đăng nhập; token trùng phiên hiện tại → returnUrl nội bộ/trang chủ; còn lại → trang đăng nhập như nhánh thiếu cấu hình/lỗi đổi danh tính của gốc). Nhánh đổi token thành công qua UAUserInfoGetAPI + QuickLoginSuccess vẫn chưa có (không có trong base .NET 6, không bịa danh tính) — xem tồn đọng.
  6. Sửa 4 link chết trên Home/Index (trước audit: ApplyBill/Index lỗi view-not-found, Delivery/Index 404, Storage/Index lỗi view-not-found, System/Index 404) → ApplyBill/Query, DeliveryQuery/Index, Storage/WarehouseStorageQuery, SystemManage/Index.
- Build + live sau fix: build Release Web + API 0 errors (lần build đầu lỗi copy PDB do process cũ đang chạy giữ file — kill đúng PID 3312/3352 rồi build lại OK); khởi động lại bằng run-5236.sh/run-5336.sh, PID mới Web 22220 (5236) + API 22219 (5336), login 000000 → /Home/Index 200. Cổng kiểm tra (không ghi DB, chỉ id giả/rỗng): SubmitReceiptApproval djId giả djlx=1 → "Không tìm thấy phiếu" (qua Validata của WbrkProcess thật); djlx=99 → "Không tìm thấy quy trình duyệt phù hợp"; SubmitApplyOutStock rỗng → "Chi tiết xuất kho không được để trống!" (thông điệp gốc); SaveDeliveryToDepartment rỗng → "Chi tiết phiếu không được để trống"; UALogin không token → về trang đăng nhập; 4 link Home mới đều 200; Sys* vẫn 302→200 như trên; API GetToken vẫn 40004, Query vẫn 10000 + jsonpCallback bọc đúng.
- Ảnh -fixed (viewport 1366x900, Playwright + Firefox, không ghi đè ảnh cũ; đã tự xem herp-home-fixed.png và herp-approval-fixed.png): herp-home-fixed.png (dashboard tiếng Việt, đủ link), herp-approval-fixed.png (lưới duyệt + nút F6 Duyệt đạt/F7 Không đạt/F8 Hủy duyệt/F9 Trả lại đơn), herp-deliverytodepartment-fixed.png, herp-applyoutstock-fixed.png.
- Tiếng Trung từ DB/enum gặp trong đợt này, KHÔNG sửa (báo riêng): Query API trả Name "材料费" (Sys_Config demo DB); KfChange hiển thị tên kho "药房耗材库房" (DB); tiền tố số chứng từ enum PROTECTED (VD ckZKS) giữ nguyên theo thiết kế.
- TỒN ĐỌNG (không fix trong đợt này, nêu rõ lý do): (a) Khung portal gốc của Home (cây menu, tab, đổi tổ chức UserOrgChoose/Default + 4 partial) chưa port — trang chủ hiện là dashboard đơn giản + link, cùng cách làm với các app port khác (SSO giữ vai trò portal); khôi phục full shell là quyết định kiến trúc, không làm lén trong audit. (b) UALogin đổi token thành công cần UAUserInfoGetAPI + QuickLoginSuccess — chưa có trong base .NET 6, cần quyết định tích hợp, hiện về trang đăng nhập an toàn. (c) Luồng ghi thật (duyệt phiếu thật, xuất kho thật) chưa được chạy live vì cấm ghi DB — code đã đúng bản gốc tới cổng kiểm tra, cần test ghi trên DB thử nghiệm riêng khi có quyết định. (d) SSRS (13 .rdl), ActiveX/DCSoft, WCF Sunshine vẫn stub/thay thế chờ quyết định như các đợt trước. (e) NRE thấy ở Approval/KfChange trong probe đầu là artifact sau khi OutLogin xóa phiên (chưa đăng nhập lại), không phải lỗi khi đã đăng nhập — đã xác nhận lại khi đăng nhập: cả hai trang 200.
- Xác nhận: không commit, không push, không ghi database (mọi probe ghi đều dừng ở cổng kiểm tra với id giả/dữ liệu rỗng và trả lỗi, không tới bước INSERT/UPDATE), không động app khác (chỉ file Herp; worktree vốn đã bẩn từ các worker khác, không chạm file của họ).

## Kiểm tra đầy đủ chức năng (parity audit) Herp — 2026-10-27 ~13:00 ICT (worker thay thế, re-audit độc lập từ đầu, không commit/push, không ghi DB)
- Tình trạng tiếp nhận: KHÔNG có mục append dở — file kết thúc sạch bằng mục audit ~12:30 ICT đầy đủ của worker trước, và 2 app vẫn chạy đúng PID của worker đó (Web 22220, API 22219). Vì vậy phần worker trước được KẾ THỪA sau khi kiểm chứng lại, không giả định mù: toàn bộ số đếm và probe dưới đây do worker này chạy lại độc lập từ `git show V2.2` (bản gốc trích ra /tmp/herp-orig, không dùng working-copy cây gốc).
- Kế thừa và đã xác nhận đúng: (a) 6 file Sys* redirect literal + SystemManageController notice; (b) ProcessorFun/ProcessException + 4 BillApprovalProcess + DeptApplyOutStockProcess + DeliveryToDepartmentProcess (untracked, đã biên dịch trong DLL đang chạy); (c) LoginController.UALogin nhánh không-UA; (d) 4 link Home đã sửa. Tất cả đều khớp code đọc trực tiếp và probe live của đợt này — giữ nguyên, không sửa/bỏ gì trong số đó.
- FIELD (đếm thật, regex trên source): DTO/Entity/VO 142 file gốc → 1917 thuộc tính; 0 thuộc tính thiếu trong các file chung (file request gốc AuthGetTokenRequest/DefaultQueryRequest/OrgJSONRequestBase được gộp vào ApiRequests.cs nhưng đủ field: AuthGetTokenRequest rỗng đúng gốc, keyword, OrganizeId [Required]); bản port 1952 thuộc tính (thừa do file mới SysConfigEntity/SysStaffVEntity/API scaffold, không phải thiếu). Interface Repo/DmnService/App: 73 file gốc, 225 method → 0 method thiếu; bản port thêm 4 interface mới (IBaseDataDmnService, ISysConfigRepo, ISysStaffRepo, ISysUserDmnService). Quét name-attr trên 105 view chung: chỉ 7 view lệch, tất cả đều lành — tên tab đã dịch VI (外部入库 → "Nhập kho bên ngoài", 填写采购计划 → "Lập kế hoạch mua sắm"), cột 操作 → "Thao tác", Supplier shsh → sh (sửa lỗi bind của gốc, đã xác nhận), còn lại là meta viewport thiếu ở 2 trang (xem fix của đợt này).
- MÀN HÌNH: gốc 129 view, bản port 110. Chênh 24 file gốc không có + 5 file port thêm (129-24+5=110, khớp): 14 view Sys* (do controller nhị phân gốc, thay bằng notice SystemManage + 6 shim redirect — đã live-verify lại, không hồi quy), 2 _Layout/_ViewStart của BillManage (không cần, view dùng thẳng ~/Views/Shared/_Index.cshtml), 2 _ViewStart (root + SystemManage), Home/Default + UserOrgChoose + 4 partial khung portal (SSO giữ vai trò portal, xem tồn đọng). Port thêm có chủ ý: SystemManage/Index + Notice, _ViewImports ×2, StorageManage _MiddleButtonsView.
- CONTROLLER/ACTION: 30 controller gốc đều có mặt + 7 thêm có chủ ý (6 Sys shim + SystemManageController). Đếm tên action Web: gốc 306, bản port 331; lệch duy nhất ở controller chung là Login (OutLogin/GetUALoginAddres không nằm trong file LoginController port mà nằm trong LoginBaseController kế thừa của SSO — đã đọc code base: có đủ OutLogin, GetUALoginAddres, CheckLogin; UALogin đã khôi phục tại Herp) và 4 action thêm có chủ ý (DeliveryQuery/Index, Department/Index+Form, Home/Index — phục vụ route menu/view gốc vốn không có action). ReportController đủ Index + PrintReport (SSRS giữ stub thông báo VI).
- API: đủ cả — Auth/GetToken (POST) + Default/Query (POST) + scaffold Get/Post/Put/Delete; envelope 10000/40004; jsonpCallback bọc đúng khi đặt trong body JSON. GetToken GIỮ NGUYÊN stub 40004 AUTH_NOT_IMPLEMENTED, không bật lại. Lưu ý probe: Timestamp là DateTime? nên phải gửi dạng ISO (gửi epoch-string sẽ bị binding null → SOMEARGUMENTS_IS_REQUIRED, đó là lỗi probe, không phải lỗi app); jsonpCallback cũng phải nằm trong body, không phải query string.
- FLOW live (đăng nhập 000000 — lưu ý form hash MD5 mật khẩu trước khi POST nên probe curl phải gửi md5, gửi plaintext sẽ báo "Mật khẩu không đúng" dù app đúng): login → "Đăng nhập thành công" → Home/Index 200; 5 URL menu Sys* đều 302 đúng 1 lần về /SystemManage/SystemManage/SysX rồi 200; TemplateIndex/TemplateForm/PersonalInfo/Roles/Selector/SyncForm đều 200; 4 link Home mới (ApplyBill/Query, DeliveryQuery/Index, Storage/WarehouseStorageQuery, SystemManage/Index) đều 200; cổng kiểm tra không ghi DB: SubmitReceiptApproval djId giả djlx=1 → "Không tìm thấy phiếu" (qua WbrkProcess thật), djlx=99 → "Không tìm thấy quy trình duyệt phù hợp", SubmitApplyOutStock rỗng → "Chi tiết xuất kho không được để trống!", SaveDeliveryToDepartment rỗng → "Chi tiết phiếu không được để trống", UALogin không token → 302 về trang đăng nhập; API Query ISO-Timestamp không token → 10000 + dòng Sys_Config thật, GetToken có Timestamp → 40004 AUTH_NOT_IMPLEMENTED (VI), token giả → TOKEN_IS_EXPIRED, Query + jsonpCallback trong body → cb({...}).
- FIX CỦA ĐỢT NÀY (chỉ 2 file, ngoài ra không sửa gì thêm vì audit độc lập không tìm thêm gap thật): thêm `<meta name="viewport" content="width=device-width" />` vào Views/Home/About.cshtml và Views/Report/PrintReport.cshtml (2 trang stub độc lập duy nhất thiếu viewport so với gốc; nội dung stub SSRS/About giữ nguyên theo thiết kế). Build Release Web 0 errors (lần build đầu lại kẹt PDB do process cũ giữ file — kill đúng PID 22220 rồi build lại OK, cùng lỗi worker trước đã gặp); khởi động lại bằng run-5236.sh, PID mới Web 32299 (5236 UP, login 200, viewport có trong HTML live); API 5336 giữ nguyên PID 22219, không restart.
- Ảnh đợt này (viewport 1366x900, Playwright + Firefox qua ~/workspace/shot/venv, không ghi đè ảnh cũ; đã tự xem herp-about-fixed.png — nội dung tiếng Việt đúng): herp-about-fixed.png, herp-report-printreport-fixed.png, herp-home-reaudit.png (ảnh kiểm tra lại sau restart).
- Tiếng Trung từ DB/enum gặp trong đợt này, KHÔNG sửa (báo riêng): Query API trả Name "材料费" (Sys_Config demo DB) — cùng 1 điểm worker trước đã báo.
- TỒN ĐỌNG (kế thừa nguyên từ worker trước, không thay đổi): (a) khung portal gốc Home chưa port; (b) UALogin nhánh đổi token thành công cần UAUserInfoGetAPI + QuickLoginSuccess; (c) luồng ghi thật chưa chạy live vì cấm ghi DB; (d) SSRS 13 .rdl / ActiveX / WCF Sunshine stub chờ quyết định.
- Xác nhận: không commit, không push, không ghi database (mọi probe ghi đều dừng ở cổng kiểm tra với id giả/dữ liệu rỗng), không động app khác (chỉ 2 file Herp của đợt này + 3 ảnh shots-roles đã gitignore; worktree Herp 301 → 303 dòng status đúng bằng 2 file này).
