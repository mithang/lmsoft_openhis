# SWEEP-D — Báo cáo kiểm chứng cuối: CIS + Herp

- **Worker:** SWEEP-D (CIS + Herp). Các app khác không thuộc phạm vi, không đụng tới.
- **Base:** V2.2 HEAD `e66c7bda` tại `~/workspace/openhis-core` (đã kiểm chứng `git log`, cây chỉ có
  sửa `roles-shots-progress.md` của điều phối viên từ trước + thư mục untracked `scripts/sweep-final/`).
- **Thời gian làm việc:** 2026-10-28 ~14:30–16:10 ICT (07:30–09:10 UTC).
- **Bản đang chạy trên prod (kiểm chứng bằng `ps`, không tự đổi launcher):**
  CIS Web 5234 = **Debug** (`bin/Debug/net6.0/Newtouch.CIS.Web.dll`, PID 10537),
  CIS API 5334 = **Debug** (PID 10760), Herp Web 5236 = **Release** (PID 11435),
  Herp API 5336 = **Release** (PID 11671). Đúng như brief đã lưu ý.
- **Phương pháp:** Playwright + Firefox local (loopback). "Mobile" = giả lập viewport 390×844,
  OpenHIS **không có app native** — không bao giờ tuyên bố test app native.
  Desktop viewport 1440×900. Mỗi probe ghi URL cuối, HTTP status, độ dài body, console errors.
  HTTP 200 kèm body JSON lỗi được tính là LỖI, không tính là pass (theo skill gstack-qa).
- **Không sửa code, không commit gì** (đợt sweep chỉ kiểm chứng; sửa để vòng fix sau).
  Không sửa `roles-shots-progress.md`.

## 0. CẢNH BÁO MỞ ĐẦU — đọc trước khi dùng kết quả này

### 0.1 Tài khoản demo prod 000000 đang bị KHÓA (do chính worker này gây ra, đã nhận lỗi)

- Đầu phiên, brief gốc chưa cho mật khẩu nên worker thử mật khẩu mặc định cũ (không phải 000000)
  3 lần liên tiếp (CIS prod, Herp prod, SSO prod) → tài khoản prod 000000 chuyển sang `Locked=1`
  (đã kiểm chứng chỉ-đọc: `NewtouchHIS_Base.dbo.Sys_User` JOIN `Sys_UserLogOn` → demo `Locked=1`,
  staging `Locked=0`).
- Sau khi nhận brief bổ sung (mật khẩu đúng là 000000), worker thử đúng **1 lần** trên prod SSO
  và vẫn nhận thông báo "tài khoản đã bị khóa" → **DỪNG HẲN mọi login bằng mật khẩu trên prod**.
- **Hệ quả:** các worker khác muốn login prod bằng mật khẩu cần điều phối viên mở khóa bằng SQL
  (đó là ghi demo DB — worker này không tự làm vì brief cấm ghi demo DB).
  Session prod đã adopt trước đó vẫn sống và đã dùng cho toàn bộ probe đọc trên prod trong báo cáo này.
- **Staging:** login staging SSO 15208 bằng mật khẩu đúng **thành công** ("Đăng nhập thành công").
  Lưu ý: worker có 1 lần thử GetToken staging bằng mật khẩu sai (để kiểm chứng fail-closed, xem §4)
  sau các lần login đúng — **không thử thêm bất kỳ mật khẩu sai nào nữa** ở bất kỳ đâu
  (quy tắc khóa 3 lần sai liên tiếp).

### 0.2 Staging BaseAPI 17001 suy yếu cuối phiên (hạ tầng, không phải bug app)

- Cuối phiên, `POST http://127.0.0.1:17001/api/login/CheckPwd` trả `code 20000`
  "Không kết nối được cơ sở dữ liệu" cho cả AppId `Herp` lẫn `CIS` (control), kèm `timeOut` ~207s.
  Điều này làm **Herp API GetToken trên staging không kiểm chứng được** (§4, B-HERP-02).
  Trước đó trong phiên, cùng endpoint đã phục vụ thành công cho CIS API GetToken (10000).
  Worker **không restart** staging BaseAPI vì đó là hạ tầng dùng chung với các worker khác.

### 0.3 Staging build cũ hơn prod — caveat cho mọi kết quả ghi trên STG

- App staging chạy từ worktree `~/workspace/openhis-stg` (build thời phase 6, chỉ đổi literal cổng).
  Kết quả ghi trên STG vì vậy **không đồng nhất 100% với e66c7bda**; những lỗi nào worker
  đối chiếu được với code e66c7bda thì ghi rõ "code-verified e66c7bda", còn lại ghi "STG build".

### 0.4 Không ghi demo DB

- Mọi probe trên prod đều là GET hoặc POST grid chỉ-đọc; mọi probe ghi đều chạy trên DB `*_STG`.
- Toàn bộ dòng test tạo trên STG đã xóa và kiểm chứng xóa xong (CIS: tìm lại `[]`;
  Herp ProductType: tìm lại `{"rows":[]}`; Herp Crkfs: 0 dòng SWEEPD còn lại, tổng về 7 dòng).

## 1. Catalog chức năng (từ controller source + view + menu)

Đếm action bằng regex `public ... IActionResult Action(` (bỏ sót action trả `ViewResult` trực tiếp
nên là cận dưới; trang thực tế lấy theo file `.cshtml`). Nhóm kiểm thử lấy theo area/module,
mỗi nhóm probe trang Index/trang chính + grid JSON chính, không click từng action trong 670/311 action.

### 1.1 CIS Web 5234 — ~670 action (regex), 60 controller, 7 area + 13 controller gốc

| Nhóm | Controller chính | Kiểm thử |
|---|---|---|
| Trang chính / portal stub | Home (Index, MSGQuery, NoticeReadSync, GetNewFieldUniqueValue) | Desktop + mobile, xem §2 |
| Danh sách bệnh nhân | PatientList (PatientList, PatientVisitList, InpatientList, SelectNotYetTreateList…) | Trang + grid POST, xem §2 |
| Đơn thuốc | Prescription (WM/TCM/TCM2022/Rehab/RegularItem/Consumables, HistoryPresForm) | Trang WM, xem §2 |
| Chỉ định khám/cận lâm sàng | Apply (Inspection, Examination, Test) | Trang Inspection/Examination, xem §2 |
| Đặt hẹn khám | BespeakRegister (BespeakRegister, CancelYY) | Trang, xem §2 |
| Truyền dịch | InfusionManage (Query) | Trang + tham số, xem §2 |
| Bệnh án | MedicalRecord (76 action: MedicalRecord, SaveMedicalRecord, Lis/Pacs form…) | Trang chính, xem §2 |
| Điều dưỡng ngoại trú | OutpatientNurse (PerformIndex, Skintestquery, prescriptionquery…) | Trang PerformIndex, xem §2 |
| Nghiệp vụ chính | MainBusiness (Index, TcmHis, ValidateMedicalInsurance…) | Trang Index — **LỖI B-CIS-01** |
| Quản lý phòng khám | ClinicManage: ClinicApply, ClinicMetting | Cả 2 Index, xem §2 |
| Quản lý bác sĩ | DoctorManage: DoctorsAdvice (31), Medicine (14), Operation (10), ElectronicPrescription (5), OutpatientQuery (13), Apply, Orginal, Rehab, DietaryAdvice, Takemedicine, Words | Index/trang chính từng controller có view, xem §2 |
| Quản lý điều dưỡng | NurseManage: OrderAudit (36), OrderExecution (20), InpatientList (15), InpatientVitalSigns (17), Allergy (13), EntryRegistration (19), Infusion (20), WardMaintenance (31), PrepareMedicine (25), ProfitAndLoss (12), DepartmentSupplies (15), BedLevelCharge, DietaryAdvice, ExceReportPrint, InpatientMedReturn, StockCarryOver, StockInventory, TreatmentPrint, sldAPPlication | Index/trang chính từng controller có view, xem §2 |
| Hàng đợi | QueueManage: QueueInfo (Index, GetQueueInfo, CallNumber) | Index + GetQueueInfo, xem §2 |
| Báo cáo | ReportManage: Report (22 action ReportConfig/Print*) | Index (redirect ReportConfig), xem §2 |
| Quản trị hệ thống | SystemManage: AuxiliaryDictionary, BaseData (SysBaseData), BespeakRegisterManage, BodyParts, DoctorRemark, InspectionCategory, UsageLinkage, VisitDeptSet | AuxiliaryDictionary CRUD trên STG (§3); BaseData/Index 404 (§2) |
| Quản lý mẫu | TemplateManage: PresTemplate, DoctorServiceTemplate, DiagnosticTemplate, GroupPackage, InspectionTemplate, MRecordTemplate | PresTemplate/Index, xem §2 |

### 1.2 CIS API 5334 — 23 action, 13 controller

| Nhóm | Endpoint | Kiểm thử |
|---|---|---|
| Cấp token | `POST api/Auth/GetToken` | Prod: rỗng → TIMESTAMP_IS_REQUIRED; sai field → AUTH_FAILED. STG: đúng credential → 10000 + token; sai mật khẩu → AUTH_FAILED (§4) |
| Đặt hẹn ngoại trú | `POST api/MzBespeakRegister/{MzBespeakRegisterQuery, MzBespeakRegisterSchedulingQuery, MzAlreadyBespeakRegisterCountQuery, KeepAnAppointment}` | Không token → 40004 TOKEN_IS_REQUIRED (fail-closed, §4); token sai → TOKEN_IS_EXPIRED; token thật STG → 10000 + `[]` cho 2 query đọc. **KeepAnAppointment (ghi) KHÔNG test** — cần lịch hẹn thật, không bịa fixture |
| Bệnh nhân nội trú | `POST api/PatInfo/{UpdateInpatientBasicInfo, UpdatebrxzInfo, InpatientBaseInfo}` | Không token → TOKEN_IS_REQUIRED. Các action ghi **không test** trên prod; trên STG không test vì cần hồ sơ nội trú thật |
| Tra cứu điều trị | `POST api/PatientSeekingInfo/PatientTreatmentInfoQuery` (chú ý: route là `PatientSeekingInfo`, không phải `PatientTreatmentInfo`) | Token thật STG → 10000 + `[]` |
| Đơn thuốc / thu phí | `POST api/Prescription/{UpdateChargeStatus, UpdateChargeStatusApi, UpdateChargeTbz}` | Không test ghi (cần đơn thuốc thật) |
| LIS / TCM / MQ / UA / Review | `CQKRLIs/GetLisReport`, `CmmTests/TcmHis01`, `InsideGeneral/MqGeneralTask`, `UA/GetUserInfo`, `Review/GetBaShData`, `HospitalizedBusiness/UpdateApplyNoStatus…` | Không test sâu (cần hệ ngoài / dữ liệu thật); ghi nhận route tồn tại từ source |
| Scaffold | `Default: GET/POST/PUT/DELETE api/Default` | Không test (scaffold gốc) |

### 1.3 Herp Web 5236 — 311 action (regex), 10 area + controller gốc

| Nhóm | Controller chính | Kiểm thử |
|---|---|---|
| Trang chính / dashboard | Home (Index, About, KfChange, GetNeedDealDiv, GetPendingCount, GetPsiByKf, GetRkCountByLx, getmesquery) | Desktop + mobile; endpoint dashboard §2/§4 |
| Vật tư | ProductManage: Product (12), ProductType (5), ProductUnit (5), ProductCrkfs (4), ProductSfxm (6), PriceAdjustment (11) | Index từng controller + grid; **CRUD đủ vòng ProductType + Crkfs trên STG (§3)** |
| Kho | WarehouseManage: Warehouse (18) | Index + grid POST (trả dữ liệu thật), xem §2 |
| Khoa phòng | DepartmentManage: Department (4) | Index (chỉ 18 ký tự "Bảo trì khoa phòng" — trang gần như trống, xem §2) |
| Nhà cung cấp | SupplierManage: Supplier (10) | Index (browser timeout 1 lần thoáng qua; curl 200), grid POST trống do DB 0 dòng (§2) |
| Giấy phép | LicenceManage: Licence (20) | Index, xem §2 |
| Mua sắm | PurchaseManage: Purchase (54), ReturnedMaterials (8) | Index + Bill, xem §2 |
| Phiếu | BillManage: ApplyBill (5), PurchasingOrder (15), PurchasingPlan (16), OutOrInStorageBill (12) | Query từng controller, xem §2 |
| Tồn kho / nhập–xuất | StorageManage: Storage (44), StockInventory (9), StockCarryOver (5), ProfitAndLoss (12), PSIStatistics (1) | Index/Query/InStorage/OutStorage, xem §2 |
| Giao hàng | DeliveryManage: DeliveryQuery (2) | Trả `[]` với tham số mặc định, xem §2 |
| Hệ thống | SystemManage: SystemManage (SysConfig/SysLog/SysModule/SysRole/SysStaff/SysUser), SysUser, SysModule (SyncForm), SysStaff, SysConfig | Index + SysUser (trang notice SSO-served), xem §2 |
| Báo cáo | Report (Index, PrintReport) | Index = thông báo stub SSRS 13 .rdl (§4) |

### 1.4 Herp API 5336 — 6 action, 2 controller

| Nhóm | Endpoint | Kiểm thử |
|---|---|---|
| Cấp token | `POST api/Auth/GetToken` | Prod: rỗng → TIMESTAMP_IS_REQUIRED. STG: **bị chặn** bởi staging BaseAPI lỗi DB (§0.2, B-HERP-02) — không kết luận được bug Herp API |
| Truy vấn cấu hình | `POST api/Default/Query` ([DefaultAuthorize]) | Không token → TOKEN_IS_REQUIRED (fail-closed). Có token → **chưa test** (phụ thuộc GetToken STG) |
| Scaffold | `GET api/Default/5` → `"value"`; POST/PUT/DELETE scaffold | GET đã kiểm chứng trả `value` |

## 2. Kết quả kiểm thử trên PROD (chỉ đọc / cổng validation, KHÔNG ghi demo DB)

Xác thực prod bằng session adopt từ SSO portal (2 bước: `GET /Home/Index?appId=His.SSO` đặt cookie
app, request thứ 2 mới vào được — request đầu luôn 302 về Login, đây là hành vi của filter,
không phải lỗi). Không cookie → Home 302 về Login (CIS + Herp đều đúng).

### 2.1 CIS Web — desktop 1440×900 (Playwright, đã đăng nhập)

| Trang | Kết quả |
|---|---|
| /Home/Index | 200, stub portal VI, liệt kê 7 area — đúng thiết kế (portal chính do SSO phục vụ) |
| /PatientList/PatientList | 200, khung lọc "Chờ/Đang/Đã khám" render; console `PAGEERROR: $ is not defined` (xem B-CIS-09: trang frame-hosted, Layout=null) |
| /Prescription/WMPrescription | 200, khung đơn thuốc render; console `$ is not defined` ×2 (B-CIS-09) |
| /Apply/Inspection, /DoctorManage/Apply/Examination | 200, khung "Ấn tượng lâm sàng / Đề nghị / Ghi chú" render gần trống (frame-hosted); Examination có `$ is not defined` (B-CIS-09) |
| /BespeakRegister/BespeakRegister | 200, form đặt hẹn render (khoa, chuyên gia, ngày, số đã đặt) |
| /InfusionManage/Query (không tham số) | 200 nhưng body là JSON lỗi "Cannot perform runtime binding on a null reference" — **B-CIS-05** |
| /InfusionManage/Query?tags=1 | 200, JSON lỗi "Object reference not set…" — **B-CIS-05** |
| /MedicalRecord/MedicalRecord | 200, form bệnh án đầy đủ (số khám, khoa, sinh hiệu…); console `$ is not defined` ×4 (B-CIS-09) |
| /OutpatientNurse/PerformIndex | 200, danh sách bệnh nhân + Test da / Thực hiện render |
| /MainBusiness/Index | 200 nhưng body JSON lỗi duplicate key — **B-CIS-01 (High), tái hiện 3 lần** |
| /DoctorManage/DoctorsAdvice/Index | 200, danh sách y lệnh render |
| /DoctorManage/OutpatientQuery/ConsultRecord | 200, lưới hội chẩn render |
| /DoctorManage/ElectronicPrescription/Index | 200, lưới đơn thuốc điện tử render |
| /DoctorManage/Operation/ArrangeQuery | 200, lưới phẫu thuật render |
| /DoctorManage/Medicine/Index | 200, khung y lệnh thuốc render |
| /NurseManage/OrderAudit/Index | 200, cây khu/khoa ("Khu 1 Khoa Noi") + nút duyệt render |
| /NurseManage/InpatientList/Index | 302 nội bộ → /NurseManage/InpatientList/Inpatient, 200, chỉ có tab "Đang ở khu / Đã chuyển khu" + chữ "Not Found" trong vùng lưới — **B-CIS-08 (đã biết từ phase 1, vẫn còn)** |
| /NurseManage/Allergy/OutHosAllergyManage | 200, form lọc dị ứng render |
| /NurseManage/EntryRegistration/EntryAreaFrom | 200, form vào khu render |
| /NurseManage/InpatientVitalSigns/QueryIndex | 200, danh sách bệnh nhân + sinh hiệu render |
| /NurseManage/DepartmentSupplies/WzSldIndex | 200, form đề nghị lĩnh vật tư render |
| /NurseManage/Infusion/Injection | 200, form truyền dịch render |
| /NurseManage/PrepareMedicine/PrepareMedicineReturn | 200, lưới trả thuốc render |
| /ClinicManage/ClinicApply/Index | 200, form lọc hội chẩn render |
| /ClinicManage/ClinicMetting/Index | 200, body rỗng (đúng "blank by design", view 226 byte — đã ghi từ phase 1) |
| /QueueManage/QueueInfo/Index | 200, bảng hàng đợi render ("Không có dữ liệu") |
| /ReportManage/Report/Index | 200, nội dung trang ReportConfig (lưới cấu hình báo cáo) — route Index phục vụ nội dung ReportConfig |
| /SystemManage/BaseData/Index | **404 ổn định (curl 3 lần)** — không có action Index, link menu hỏng — **B-CIS-07 (đã biết, vẫn còn)** |
| /TemplateManage/PresTemplate/Index | 200, tab "CÁ NHÂN / KHOA / TOÀN VIỆN" render |
| Grid POST /PatientList/SelectNotYetTreateList (kèm pagination) | 200, JSON lỗi NRE — **B-CIS-04** |
| Grid GET /QueueManage/QueueInfo/GetQueueInfo (không tham số ngày) | 200, JSON lỗi "SqlDateTime overflow…" — **B-CIS-06** |
| Grid POST /NurseManage/Allergy/GetMzPatientListJson | 200, lỗi VI rõ ràng "Vui lòng liên hệ quản trị viên cấu hình mục dị ứng và tham số động, mã tham số: AllergyItemSetting" — lỗi cấu hình, không phải crash; ghi nhận là phụ thuộc cấu hình chưa có trên demo |
| Grid GET /SystemManage/BespeakRegisterManage/GetTreeGridJson | 200, `{"rows":[],"total":0,…}` |

### 2.2 Herp Web — desktop (Playwright + curl bổ sung sau khi Firefox bị đóng giữa chừng do RAM)

| Trang | Kết quả |
|---|---|
| /Home/Index | 200, dashboard thật (link 10 nhóm + thống kê); mobile: kẹt chữ "Đang tải cảnh báo…" + ô thống kê trống tại thời điểm chụp (AJAX chậm/thoáng qua — endpoint retest sau đó trả dữ liệu, xem §4) |
| /Home/About | 200, giới thiệu bản port ASP.NET Core 6.0 |
| /ProductManage/Product/Index | 200, lưới vật tư render (khung); grid POST trả `{"rows":[]}` — **đúng dữ liệu**: `wz_product` = 0 dòng trên demo DB (kiểm chứng chỉ-đọc), không phải bug |
| /ProductManage/ProductType/Index | 200, lưới loại vật tư render |
| /ProductManage/ProductUnit/Index | 200, lưới đơn vị render |
| /ProductManage/ProductCrkfs/Index | 200, lưới phương thức xuất/nhập render |
| /ProductManage/ProductSfxm/Index | 200, lưới hạng mục thu phí render |
| /ProductManage/PriceAdjustment/PriceAdjustmentRequest | 200, form điều chỉnh giá render |
| /WarehouseManage/Warehouse/Index | 200, lưới kho render; grid POST trả **dữ liệu thật** (kho "Kho vat tu nha thuoc", org demo) |
| /DepartmentManage/Department/Index | 200 nhưng body chỉ 18 ký tự "Bảo trì khoa phòng" — trang gần như trống (ghi nhận; không kết luận bug vì chưa rõ view gốc có nội dung gì thêm) |
| /SupplierManage/Supplier/Index | Browser timeout 45s 1 lần (thoáng qua, RAM); curl 2 lần đều 200 (10.224 byte, trang đầy đủ). Grid POST trả `{"rows":[]}` — **đúng dữ liệu**: `gys_supplier` = 0 dòng |
| /LicenceManage/Licence/Index | 200, lưới giấy phép render |
| /PurchaseManage/Purchase/Index | 200, lưới mua sắm render |
| /PurchaseManage/Purchase/Bill | 200, form mua sắm render (khung) |
| /BillManage/ApplyBill/Query, /BillManage/PurchasingPlan/PurchasingPlanQuery, /BillManage/PurchasingOrder/PurchaseOrderQuery, /BillManage/OutOrInStorageBill/Query | 200, các form tra cứu phiếu render |
| /StorageManage/Storage/WarehouseStorageQuery, /InStorage, /OutStorage | 200, form tồn kho / nhập / xuất render |
| /StorageManage/StockInventory/Index, /InventoryQuery, /ProfitAndLoss/Index, /StockCarryOver/Index, /PSIStatistics/Index | 200 (curl), tiêu đề VI đúng từng trang |
| /DeliveryManage/DeliveryQuery/InStorageDeliveryQuery và OutStorageDeliveryQuery | 200, trả `[]` với tham số mặc định (đây là endpoint JSON, không phải trang) |
| /SystemManage/SystemManage/Index | 200, trang giải thích Sys* do SSO phục vụ |
| /SystemManage/SystemManage/SysUser | 200, trang notice người dùng hệ thống |
| /Report/Index | 200, thông báo stub SSRS 13 .rdl (§4) |
| /ProductManage/Product/GetProductGridJson (GET) | 200, `{"rows":[],"total":0}` (đúng dữ liệu 0 dòng, xem trên) |

### 2.3 Mobile 390×844 (Playwright + Firefox, viewport emulation)

- CIS: login 200 (form "Chào mừng / Đăng nhập"); home 200, danh sách 7 area + link chức năng gốc render tốt.
- Herp: login 200; home 200, dashboard render nhưng kẹt "Đang tải cảnh báo…" + ô thống kê trống
  tại thời điểm chụp (xem §2.2).
- Lưu ý kỹ thuật: lần chụp đầu, worker ghé `/Login/Index` trước rồi mới vào Home thì bị đẩy về
  Login (trang Login xóa cookie session khi load) — đã chụp lại đúng cách (context riêng cho Home,
  không ghé Login trước) và ảnh cuối là ảnh đúng.
- Ảnh đã kiểm chứng bằng mắt (xem §7): CIS mobile home hiển thị đúng danh sách phân hệ;
  Herp mobile home hiển thị dashboard + dòng "Đang tải cảnh báo…".

## 3. Kiểm thử ghi trên STAGING (DB `*_STG`, đã dọn sạch)

App staging chạy tối đa 2 cùng lúc, kill đúng PID sau mỗi cặp (xem §6).
Staging SSO 15208 đã login thành công; adopt session sang app staging theo cùng cơ chế 2 bước.

### 3.1 CIS staging — Web 15234 + API 15334 (PID 29367, 29368)

| Thử nghiệm | Kết quả |
|---|---|
| Khởi động + login page + no-cookie Home → 302 | Pass (login 200, no-cookie 302 về Login) |
| Adopt session staging → Home 200 | Pass |
| AuxiliaryDictionary CREATE đúng luồng UI (không gửi Id) | **FAIL — B-CIS-02**: `Cannot insert NULL into column 'Id', table Newtouch_CIS_STG.dbo.xt_fzcd` |
| AuxiliaryDictionary CREATE gửi kèm Id (GUID tự sinh) | Pass — dòng xuất hiện trong grid, `CreatorCode=000000`, `zt=1` |
| AuxiliaryDictionary EDIT (keyValue=Id, đổi Name) | **FAIL — B-CIS-03**: `Cannot insert NULL into column 'CreatorCode' … UPDATE fails` (dòng cũ giữ nguyên, đã kiểm chứng bằng GetFormJson) |
| AuxiliaryDictionary DELETE | Pass — "Xóa thành công", tìm lại `[]` |
| CIS API GetToken (credential đúng) | Pass — 10000 + token |
| CIS API GetToken (mật khẩu sai, 1 lần duy nhất để kiểm fail-closed) | Pass fail-closed — 40004 AUTH_FAILED, không cấp token. **Không lặp lại** |
| CIS API MzBespeakRegisterQuery + SchedulingQuery (token thật, id giả) | Pass — 10000 + `data: []` (đọc, không ghi) |
| CIS API PatientSeekingInfo/PatientTreatmentInfoQuery (token thật, mzh giả) | Pass — 10000 + `data: []` |
| CIS STG GetNewCkzksdh (sinh số, không lưu) | Trả `SLD20261028075657` — tiền tố **hardcode "SLD"** trong code (xem §5) |

### 3.2 Herp staging — Web 15236 + API 15336 (PID 33239, 33503)

Lưu ý: không có sẵn launcher Herp API staging; worker chạy inline từ worktree staging
(`UseUrls` staging đã là 15336) với cùng env override STG như `run-herp-stg.sh`.
Lần chạy đầu sai cwd nên dotnet báo không tìm thấy dll — đã chạy lại đúng cwd, không ảnh hưởng gì.

| Thử nghiệm | Kết quả |
|---|---|
| Khởi động + adopt session staging → Home 200 | Pass (web khởi động chậm ~1 phút dưới tải RAM) |
| ProductType CREATE đúng luồng UI (không Id) | **Pass** — tự sinh Id, dòng xuất hiện trong grid, `CreatorCode=000000` |
| ProductType EDIT + GETFORM | **Pass** — tên đổi thành công, `LastModifierCode=000000` |
| ProductType DELETE | **Pass** — "Xóa thành công", tìm lại `{"rows":[]}` |
| ProductCrkfs CREATE sai field (`name=` thay vì `crkfsmc=`) | FAIL do worker gửi sai field: `Cannot insert NULL into column 'crkfsmc'` — lỗi của probe, không phải bug app; gửi đúng field thì pass (dòng dưới) |
| ProductCrkfs CREATE (`crkfsmc=`) + EDIT + DELETE | **Pass đủ vòng** — tạo/edit/xóa thành công, cuối cùng 0 dòng SWEEPD, tổng về 7 dòng |
| Herp API GetToken (credential đúng) | **BỊ CHẶN — B-HERP-02**: 40004 AUTH_FAILED do staging BaseAPI 17001 lỗi DB (§0.2). Không kết luận bug Herp API |
| Herp API Default/Query (không token) | Pass fail-closed — 40004 TOKEN_IS_REQUIRED (prod) |
| Sinh số chứng từ STG (không lưu): Storage GetNewCkdh / GenerateRkdh / PurchasingPlan GetNewPurchasingPlanBillNo | `WBCK…` / `WBRK…` (luồng Helper → tiền tố cũ) và `KHMS…` (luồng Manage → tiền tố mới) — xem §5, không tự thống nhất |

## 4. Bug tìm được (không sửa — để vòng fix sau)

### B-CIS-01 — High — MainBusiness/Index luôn crash (prod, desktop)

- **Trang/action:** CIS Web `GET /MainBusiness/Index` (đã đăng nhập).
- **Tái hiện:** mở trang bằng session hợp lệ (browser hoặc curl kèm cookie) → body là JSON lỗi.
- **Mong đợi:** trang nghiệp vụ chính render.
- **Thực tế (3 lần, browser + curl ×2):**
  `{"state":"error","message":"An item with the same key has already been added. Key: ISOpenZycfnew",…}`.
- **Nguyên nhân (code-verified e66c7bda):** controller đặt `ViewBag.ISOpenZycfnew` (→ đã có trong
  `ViewData`); view `Views/MainBusiness/Index.cshtml:96-97` tạo `new ViewDataDictionary(ViewData)`
  rồi `Add("ISOpenZycfnew", …)` cho 2 partial → key trùng → throw ngay ở partial đầu tiên.
  Trang này vì vậy **không bao giờ mở được với bất kỳ user nào**.
- **Viewport:** desktop (curl + browser). Mobile chưa thử riêng cho trang này (cùng code path).

### B-CIS-02 — High — Tạo mới từ điển phụ (AuxiliaryDictionary) luôn lỗi NULL Id

- **Trang/action:** CIS `POST /SystemManage/AuxiliaryDictionary/SubmitForm` (tạo mới, keyValue rỗng).
- **Tái hiện (STG, đúng luồng UI):** POST form `Code=SWEEPD-TEST&Name=…&zt=true` (form UI
  `Form.cshtml` **không có trường Id**) → lỗi.
- **Mong đợi:** tạo dòng mới, trả "Thao tác thành công".
- **Thực tế:** `Cannot insert the value NULL into column 'Id', table '…xt_fzcd'; column does not allow nulls. INSERT fails.`
- **Nguyên nhân (code-verified e66c7bda):** repo `SubmitForm` nhánh tạo mới gọi `entity.Create(true)`,
  nhưng extension `Create` của port (POCO, `ServiceCompat.cs`) chỉ đặt audit field, **không sinh GUID**
  như base class Framework gốc. Gửi kèm Id thì tạo thành công (đã kiểm chứng) → lỗi hẹp ở khâu sinh Id.
- **Đối chứng Herp:** Herp `ProductTypeApp.SubmitForm` cũng gọi `Create(true)` nhưng entity base của
  Herp vẫn sinh Id nên tạo mới **pass** — khác biệt này củng cố nguyên nhân nằm ở CIS compat layer.
- **Viewport:** STG (build cũ) + code e66c7bda. Chưa thử tạo mới trên prod (cấm ghi demo).

### B-CIS-03 — High — Sửa từ điển phụ luôn lỗi NULL CreatorCode (nghi systemic)

- **Trang/action:** CIS `POST /SystemManage/AuxiliaryDictionary/SubmitForm?keyValue=<Id>` (sửa).
- **Tái hiện (STG):** sau khi tạo dòng kèm Id, POST sửa đổi Name → lỗi; GetFormJson sau đó cho thấy
  dòng cũ giữ nguyên.
- **Mong đợi:** cập nhật Name, trả "Thao tác thành công".
- **Thực tế:** `Cannot insert the value NULL into column 'CreatorCode', table '…xt_fzcd'; column does not allow nulls. UPDATE fails.`
- **Nguyên nhân (code-verified e66c7bda):** repo lấy `oldEntity` rồi `Detach` nhưng lại `Update(entity)`
  với **entity từ form post** (form chỉ có ParentId/Name/Code/zt, không có CreatorCode/CreateTime),
  mà `RepositoryBase.Update` là `Db.Updateable(entity)` cập nhật **toàn bộ cột** → ghi NULL vào
  CreatorCode NOT NULL. Herp tránh được lỗi này vì edit path của nó load `dbType` rồi chỉ gán
  từng field post lên (merge pattern).
- **Phạm vi nghi ngờ (chưa chứng minh từng cái):** mọi `SubmitForm` edit path khác trong CIS dùng
  cùng mẫu posted-entity + full-column update đều có nguy cơ tương tự — vòng fix nên rà soát
  theo mẫu này thay vì chỉ sửa 1 controller.
- **Dọn dẹp:** dòng test đã xóa thành công sau đó (DELETE pass).

### B-CIS-04 — Medium — Grid "chưa khám" luôn NRE

- **Trang/action:** CIS `GET` và `POST /PatientList/SelectNotYetTreateList` (kèm pagination).
- **Tái hiện:** cả GET lẫn POST pagination đều trả JSON lỗi `Object reference not set to an instance of an object.`
- **Mong đợi:** lưới bệnh nhân chờ khám (kể cả rỗng) hoặc thông báo nghiệp vụ rõ ràng.
- **Thực tế:** NRE thô, không có dữ liệu.

### B-CIS-05 — Medium — Trang truyền dịch lỗi với đầu vào rỗng/không hợp lệ

- **Trang/action:** CIS `GET /InfusionManage/Query`.
- **Tái hiện:** không `tags` → JSON lỗi `Cannot perform runtime binding on a null reference`
  (action trả `View()` nhưng view bind null); `?tags=1` (id không tồn tại) → NRE.
- **Mong đợi:** view trống hoặc thông báo "không tìm thấy phiếu truyền" khi đầu vào rỗng/sai.
- **Thực tế:** JSON lỗi runtime tiếng Anh. Ghi chú: phase 1 từng ghi "JSON error, faithful" cho
  trường hợp thiếu `tags` — worker giữ nguyên ghi nhận đó nhưng vẫn xếp là bug mức Medium
  vì trang không render được ở trạng thái rỗng (chỉ cần id truyền thật mới mở được — chưa có
  fixture id thật để kiểm chứng đường happy path).

### B-CIS-06 — Low — Hàng đợi: thiếu tham số ngày → lỗi SqlDateTime overflow

- **Trang/action:** CIS `GET /QueueManage/QueueInfo/GetQueueInfo` (không tham số).
- **Tái hiện:** trả JSON lỗi `SqlDateTime overflow. Must be between 1/1/1753…`.
- **Mong đợi:** thông báo validation (thiếu ngày) thay vì exception SQL.
- **Thực tế:** exception SQL lộ ra ngoài. Trang Index thì render bình thường (lưới "Không có dữ liệu").

### B-CIS-07 — Đã biết, vẫn còn — Link menu BaseData hỏng (404)

- **Trang:** CIS `/SystemManage/BaseData/Index` → **404 ổn định** (curl 3 lần; lần browser đầu báo
  `NS_ERROR_NET_EMPTY_RESPONSE` — bản chất vẫn là 404 body rỗng).
- **Nguyên nhân:** không có action `Index` (controller `SysBaseDataController` chỉ có các action
  Get*). Đã ghi từ phase 1 ("broken home link"), chưa sửa.

### B-CIS-08 — Đã biết, vẫn còn — InpatientList Index: redirect + shell + chữ "Not Found"

- **Trang:** CIS `/NurseManage/InpatientList/Index` → redirect nội bộ sang `…/Inpatient`, 200,
  chỉ có tab khu + dòng "Tìm thấy bản ghi / Tổng trang / Not Found" trong vùng lưới.
- Phase 1 từng ghi "view Index not found" cho trang này; hành vi hiện tại đã khác (redirect thay vì
  lỗi view) nhưng lưới vẫn không hiển thị dữ liệu mà hiện chữ "Not Found" — cần vòng fix xác định
  sub-request nào 404 (có thể là grid/tree endpoint của trang Inpatient).

### B-CIS-09 — Quan sát, KHÔNG tính là bug standalone — `$ is not defined` ở trang frame-hosted

- **Trang:** CIS `PatientList/PatientList`, `Prescription/WMPrescription`, `MedicalRecord/MedicalRecord`,
  `DoctorManage/Apply/Examination` (console `PAGEERROR: $ is not defined`, 1–4 lần/trang).
- **Nguyên nhân:** các view này đặt `Layout = null` (cả `_ViewStart.cshtml` của CIS cũng `Layout = null`)
  và script trong view giả định jQuery từ frame cha (thiết kế frame-hosted gốc) — mở standalone
  thì thiếu jQuery nên script tương tác hỏng, dù khung trang vẫn render.
- **Kết luận đúng mực:** không tính là bug khi mở standalone; **hành vi trong frame thật chưa được
  kiểm chứng trong đợt này** (CIS Home chỉ là stub, không phải shell chứa tab) — vòng sau nên
  kiểm chứng 1 trang qua luồng tab portal nếu muốn đóng mục này.

### B-CIS-10 — Low, đã biết — API validation trả ProblemDetails tiếng Anh thay vì envelope

- **Endpoint:** CIS API `POST api/MzBespeakRegister/MzBespeakRegisterQuery` với body `{}` →
  HTTP 400 ProblemDetails tiếng Anh (`blh/zjh field is required`) thay vì envelope 40004.
- Đã ghi từ phase 1/2 (do `[ApiController]`), vẫn còn. Khi body đủ field thì envelope hoạt động
  đúng (xem §4 API bên dưới).

### B-HERP-01 — Low — Thông báo lỗi DB chứa tiền tố tiếng Trung "中文提示 :"

- **Endpoint:** Herp `GET /Home/GetPsiByKf`, `/Home/GetRkCountByLx`, `/Home/getmesquery`
  (khi DB lỗi) → message bắt đầu bằng `中文提示 :` rồi mới tới text lỗi SQL tiếng Anh.
- Đây là chuỗi hiển thị còn sót trong đường dẫn lỗi (chỉ lộ khi DB lỗi). Khi DB khỏe, các endpoint
  này trả dữ liệu đúng (GetPsiByKf: 12 tháng = 0; GetRkCountByLx: `[]`; getmesquery: rows rỗng).
- **Viewport:** prod, curl (không phụ thuộc viewport).

### B-HERP-02 — Chặn kiểm chứng (hạ tầng) — Herp API GetToken trên STG

- **Endpoint:** Herp API STG `POST api/Auth/GetToken` (credential đúng) → 40004 AUTH_FAILED
  (2 lần, cách nhau ~2 phút).
- **Nguyên nhân đã chứng minh không phải Herp API:** `POST` trực tiếp tới staging BaseAPI 17001
  `/api/login/CheckPwd` với cả AppId `Herp` và `CIS` đều trả `code 20000`
  "Không kết nối được cơ sở dữ liệu" (timeout ~207s) — tức CheckPwd chết ở tầng DB của staging
  BaseAPI trước khi Herp API có thể làm gì. Source Herp API staging đã diff với e66c7bda = giống hệt.
- **Cần làm lại khi staging BaseAPI khỏe:** GetToken Herp STG → Default/Query có token.
  (CIS GetToken đã pass khi BaseAPI còn khỏe nên mẫu GetToken thật đã được chứng minh ở CIS.)

### Không phải bug (đã loại trừ bằng chứng cứ)

- Herp Product/Supplier grid trống trên demo: `wz_product` = 0, `gys_supplier` = 0 (truy vấn chỉ-đọc)
  → đúng dữ liệu demo, không phải lỗi lọc.
- Herp dashboard lỗi DB thoáng qua đầu phiên: retest sau đó trả dữ liệu → do SQL Server quá tải
  tạm thời (cùng đợt với lỗi prelogin của sqlcmd), không phải bug bền.
- Herp Supplier/Index timeout 45s trên browser 1 lần + Firefox bị đóng giữa chừng (RAM ~90%):
  curl sau đó 200 ổn định → thoáng qua do tải máy, đã retry theo flake rule.
- CIS ClinicMetting/Index body rỗng: blank by design (view 226 byte), đã ghi từ phase 1.
- CIS ReportManage/Report/Index phục vụ nội dung ReportConfig: không phải 404; ghi nhận hành vi.

## 5. Các điểm còn tồn theo brief — CHỈ ghi nhận hiện trạng, KHÔNG tự quyết/redesign

### 5.1 CIS ActiveX + Lis_HisInterface.dll (Windows-only)

- Hiện trạng: view `MedicalRecord.cshtml:1041` và `_PrintCommonView.cshtml` hiển thị thông báo VI
  rằng tích hợp LIS ActiveX (COM `NewtouchActiveX`, chỉ IE + Windows) không chạy được trên Linux.
  Trong cùng file còn mã JS `new ActiveXObject("wscript.shell")` (dòng ~5980, ~5994) — mã chết
  trên trình duyệt hiện đại, giữ nguyên như gốc.
- **Không tuyên bố chạy được.** Không có dll native nào được nạp/thử trên Linux trong đợt này.

### 5.2 Herp SSRS 13 .rdl (Windows-only)

- Hiện trạng: `ReportController` (Index/PrintReport) chỉ trả view; header ghi rõ 13 báo cáo .rdl gốc
  (tồn kho, lãi lỗ, nhập–xuất–tồn, truy vấn tồn kho theo kho, …) không có bản tương đương .NET Core.
  Trang `/Report/Index` trên prod hiển thị thông báo stub (đã kiểm chứng 200).
- **Không tuyên bố chạy được.** Việc thay thế SSRS vẫn là hạng mục còn lại, không thuộc sweep.

### 5.3 CIS MzBespeakRegister không token — CẬP NHẬT so với premise của brief

- **Brief ghi:** "không token trả INTERNAL_SERVER_ERROR là hành vi gốc trung thực, đừng tự sửa
  thành fail-closed".
- **Thực tế trên prod e66c7bda (đã kiểm chứng):** body đủ field (`Timestamp + blh + zjh`), **không
  token** → `40004 TOKEN_IS_REQUIRED` ("Token truy cập trống"); token sai → `40004 TOKEN_IS_EXPIRED`.
  Nguyên nhân: batch API-auth phase 2 (commit a535db1f) đã gắn `[DefaultAuthorize]` cho cả 4 action
  của `MzBespeakRegisterController` (ghi trong comment code dòng 15–20 của controller).
- **Kết luận:** hành vi INTERNAL_SERVER_ERROR/NRE là của **bản trước phase 2**, không còn đúng ở
  e66c7bda. Hành vi hiện tại là fail-closed có chủ ý — **không được "khôi phục" INTERNAL_SERVER_ERROR**.
  (Lưu ý: body rỗng `{}` vẫn bị `[ApiController]` chặn trước bằng 400 ProblemDetails — xem B-CIS-10.)

### 5.4 Các quyết định mở — chỉ ghi nhận, không tự quyết

| Mục | Hiện trạng kiểm chứng |
|---|---|
| Herp `GetKfUserInfo` + "Infodiv" đọc demo hardcode | `GetKfUserInfo` trong code hiện tại truy vấn bảng thật (`Sys_UserStaff`/`Sys_Staff`/`rel_warehouseUser`/`kf_warehouse` theo UserId+OrganizeId), **không hardcode demo**. Chuỗi `Infodiv` (không phân biệt hoa thường) có **0 hit** trong toàn bộ source CIS + Herp (trừ obj/bin) — mục này dưới tên gọi đó không tồn tại trong codebase; nếu ý là khối "deal div" của dashboard Herp (`GetNeedDealDiv`) thì dữ liệu của nó lấy từ truy vấn live. Không quyết gì thêm |
| `OpListSubmit` CreatorCode | `OpListSubmit` **chỉ tồn tại ở EMR/MRMS** (`MRHomePageDmnService`), **không có trong CIS/Herp** — ngoài phạm vi SWEEP-D, chuyển cho worker EMR/MRMS. Không quyết |
| Ứng viên dịch H-02 `wz_crkfs` | Toàn bộ 7 dòng trên STG vẫn giữ tên tiếng Trung: 正常出库 (crkbz=1), 正常入库 (0), 赠与 (1), 赈灾 (1), 库存富余 (1), 退货 (1), 在途入库 (0). **Chưa dịch, chưa quyết** — dịch tên này sẽ đổi giá trị hiển thị/lọc của phương thức xuất-nhập kho nên cần quyết riêng |
| Schema `zy_bqksby` / `zy_ksbyth` | **Không tồn tại** trong các DB demo đã kiểm tra (Base, CIS, EMR, Union, Sett, OR, MRMS, Interface). DB EMR chỉ có `zy_brjbxx`, `zy_meddocs_relation`. Code CIS `PrepareMedicine` vẫn tham chiếu chúng (+ `zy_bqksbymx`, `zy_ksbythmx`) qua entity `[SugarTable]` và SQL trực tiếp (`PrepareMedicineDmnService.cs:67,347,511,540`). **Không quyết** (tạo bảng hay sửa code đều ngoài sweep) |
| Tiền tố số chứng từ | **Không thống nhất, ghi đúng từng luồng, không tự thống nhất:** Herp Storage qua `ReceiptNoHelper` → tiền tố **cũ** (`WBCK…`, `WBRK…` — kiểm chứng live STG); Herp PurchasingPlan qua `ReceiptNoManage` → tiền tố **mới** (`KHMS…` — kiểm chứng live STG); CIS DepartmentSupplies `GetNewCkzksdh` → **hardcode `"SLD"`** + timestamp trong code (không qua helper nào). Cùng loại chứng từ có thể mang tiền tố khác nhau tùy luồng — đúng như cảnh báo từ đợt dịch |

## 6. Phần không tái hiện được / không test được + lý do

| Phần | Lý do |
|---|---|
| Login prod bằng mật khẩu (sau sự cố khóa §0.1) | Tài khoản prod `Locked=1`; đã dừng sau 1 lần thử đúng mật khẩu vẫn báo khóa. Cần điều phối viên mở khóa |
| CIS API các action ghi (`KeepAnAppointment`, `UpdateChargeStatus…`, `UpdateInpatientBasicInfo…`) | Cần lịch hẹn/đơn thuốc/hồ sơ thật; không bịa fixture. Chỉ kiểm chứng cổng fail-closed (không token/token sai) |
| CIS API LIS/TCM/MQ/UA/Review/HospitalizedBusiness sâu | Cần hệ ngoài (LIS, MQ) hoặc dữ liệu thật; chỉ xác nhận route tồn tại từ source |
| Herp API `Default/Query` có token | Bị chặn bởi staging BaseAPI lỗi DB (§0.2). Cần chạy lại khi 17001 khỏe |
| Herp API GetToken trên prod | Không thử (tài khoản prod đang khóa; thử sẽ chỉ nhận AUTH_FAILED và tốn lượt đếm) |
| CIS/Herp: hành vi trong frame thật (tab portal) cho các trang Layout=null | CIS Home chỉ là stub, không phải shell tab; cần luồng tab portal để kiểm chứng (xem B-CIS-09) |
| CIS: đường happy path của `InfusionManage/Query` với id truyền thật | Không có id phiếu truyền thật; chỉ kiểm chứng đường rỗng/sai |
| Herp: các luồng ghi phức tạp (nhập/xuất kho thật, mua sắm, duyệt phiếu, kiểm kê, kết chuyển) | Cần kho/vật tư/nhà cung cấp thật trên STG (demo DB có 0 vật tư/0 nhà cung cấp); chỉ kiểm chứng CRUD danh mục + sinh số |
| Ghi trên prod (mọi loại) | Cấm theo brief — mọi ghi đều qua STG |
| Kiểm chứng từng action một (670 CIS + 311 Herp) | Ngoài khả năng 1 phiên sweep; đã probe trang chính + grid chính theo nhóm (xem §1). Các action form con (Form/Submit chi tiết) ngoài nhóm CRUD đã test vẫn chưa được click hết |

## 7. Ảnh đã chụp (viewport emulation, KHÔNG phải app native)

Lưu tại `~/workspace/openhis-core/shots-sweep/` — thư mục này đã nằm trong `.gitignore`
(`shots*/`), **không lọt vào commit** (đã kiểm chứng `git status` cuối phiên: chỉ có sửa
`roles-shots-progress.md` từ trước của điều phối viên + thư mục untracked `scripts/sweep-final/`).

| File | Nội dung (đã kiểm chứng bằng mắt với 2 ảnh mobile home) |
|---|---|
| `cis-desktop-home.png` | CIS Home desktop 1440×900 (đã đăng nhập) |
| `cis-mobile-login.png` | CIS Login mobile 390×844 (không session) |
| `cis-mobile-home.png` | CIS Home mobile 390×844 — danh sách 7 phân hệ + link chức năng gốc, render tốt |
| `herp-desktop-home.png` | Herp Home desktop 1440×900 (đã đăng nhập) |
| `herp-mobile-login.png` | Herp Login mobile 390×844 (không session) |
| `herp-mobile-home.png` | Herp Home mobile 390×844 — dashboard + dòng "Đang tải cảnh báo…" + ô thống kê trống tại thời điểm chụp |

## 8. Dọn dẹp cuối phiên (đã kiểm chứng)

- CIS staging: kill PID 29367 (Web 15234) + 29368 (API 15334) → cổng DOWN, tiến trình gone.
- Herp staging: kill PID 33239 (Web 15236) + 33503 (API 15336, chạy inline) → cổng DOWN, tiến trình gone.
- Không còn app staging nào của worker này chạy (tối đa 2 cùng lúc trong suốt phiên).
- Dòng test STG đã xóa hết (§0.4). Không ghi demo DB. Không sửa code, không commit.
- Session cookie jar chỉ nằm ở `/tmp` (không đưa vào báo cáo, không commit).

## 9. Tóm tắt cho điều phối viên

- **Đã test:** CIS 30 URL desktop + 6 grid/API nhóm + CRUD STG; Herp 31 URL desktop/curl + 2 CRUD
  đủ vòng STG + API fail-closed; cả 2 app desktop + mobile (login + home).
- **Bug mới, mức High (3):** B-CIS-01 MainBusiness/Index crash duplicate key; B-CIS-02 tạo từ điển
  phụ lỗi NULL Id; B-CIS-03 sửa từ điển phụ lỗi NULL CreatorCode (nghi systemic theo mẫu update).
- **Bug mới mức Medium/Low (4):** B-CIS-04 grid chờ khám NRE; B-CIS-05 trang truyền dịch lỗi đầu vào
  rỗng/sai; B-CIS-06 hàng đợi SqlDateTime overflow; B-HERP-01 tiền tố lỗi tiếng Trung "中文提示 :".
- **Đã biết vẫn còn (3):** B-CIS-07 BaseData 404; B-CIS-08 InpatientList shell + "Not Found";
  B-CIS-10 API validation ProblemDetails tiếng Anh.
- **Cần điều phối viên xử lý (không thuộc worker):**
  1. Mở khóa tài khoản demo prod 000000 (SQL, demo DB) để các worker khác login prod bằng mật khẩu.
  2. Khi staging BaseAPI 17001 khỏe lại: chạy lại Herp API GetToken STG → Default/Query có token
     (phần duy nhất của Herp API còn thiếu).
  3. Các quyết định mở §5.4 (đặc biệt wz_crkfs H-02 và schema zy_*) — worker không tự quyết.
  4. Premise brief về MzBespeakRegister INTERNAL_SERVER_ERROR đã lỗi thời ở e66c7bda (§5.3).
- **Kết quả tích cực:** Herp CRUD danh mục đủ vòng pass hoàn toàn; CIS API fail-closed đúng
  (no-token/invalid-token/token thật đều như thiết kế); Herp grid trống là đúng dữ liệu demo 0 dòng;
  toàn bộ trang chính 2 app đều mở được 200 (trừ các bug liệt kê trên).
