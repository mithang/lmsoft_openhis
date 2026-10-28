# SWEEP-E — BÁO CÁO KIỂM CHỨNG CUỐI (PDS + Base/HOSP + Base Web)

- Worker: SWEEP-E. Base: V2.2 HEAD `e66c7bda` tại `~/workspace/openhis-core`.
- Ngày: 2026-10-28 (ICT). Phạm vi: PDS Web 5237 (+ API 5337), Base HOSP Web 5233 (+ HOSP API 5333), Base Web 5238. Các app khác KHÔNG đụng tới.
- Nguyên tắc tuân thủ: prod chỉ đọc / kiểm tra cổng validation / login (KHÔNG ghi database demo dưới mọi hình thức);
  mọi kiểm thử ghi chạy qua app staging tương ứng; proxy env UNSET; `DOTNET_SYSTEM_NET_DISABLEIPV6=true`;
  kill đúng PID; mật khẩu SA giữ nguyên, chỉ dùng transient qua env file, không in ra.
- KHÔNG sửa code, KHÔNG commit gì. KHÔNG sửa `roles-shots-progress.md` (điều phối viên sở hữu).
- Tài khoản kiểm chứng duy nhất: username `000000` / mật khẩu `000000` (theo bổ sung của điều phối viên).
  Trước bổ sung, worker này thử login 0 lần. Sau bổ sung, chỉ dùng đúng mật khẩu này, không đoán mật khẩu khác.

## 0. Tóm tắt kết quả

| Hạng mục | Kết quả |
|---|---|
| PDS-03 lưu thật trên STG | ✅ CHỨNG MINH ĐƯỢC (xem §2.1) — phiếu tạo thành công, đã dọn sạch, STG trở về trạng thái trước test, demo không đổi |
| Menu chết PDS (PDS-07) | ✅ Sạch cả demo lẫn STG (3/3 `zt=0`), menu sống không ảnh hưởng (18 menu sống cùng cha còn nguyên), backup đủ 3 dòng cả 2 DB |
| ZYTYInterface quirk AllFailure | ✅ Ghi đúng hành vi gốc (code) + kiểm chứng live nhánh validation/thất bại; nhánh thành công KHÔNG live-provable mà không ghi demo (xem §2.2) |
| PDS djqz | ✅ Giữ nguyên (field chuỗi, không ép enum) |
| BaseAPI Query / HOSP API Query 10000 | ✅ Hồi quy thành công trên prod với token thật (xem §2.3) |
| Bug mới tìm được | 2 bug ghi mới (E-01, E-02) + 1 lỗi widget trang chủ PDS staging (E-03) + các quan sát E-04…E-07 |
| Ảnh | Login 3 app × 2 viewport; trang chính PDS staging × 2 viewport; ảnh trang chính prod bằng browser KHÔNG lấy được do VM thrash (xem §6) |

## 1. Sự cố ngoài phạm vi kiểm chứng (ảnh hưởng tới phiên làm việc — cần điều phối viên biết)

### 1.1 Tài khoản demo prod từng bị khóa, đã mở khóa

- Trong phiên này, login prod bằng browser ban đầu đều thất bại; 1 lần chẩn đoán trả về thông điệp
  "Nhập sai mật khẩu liên tiếp quá 3 lần, tài khoản đã bị khóa"; SELECT demo xác nhận `Sys_UserLogOn.Locked = 1`.
- Theo thông báo khẩn của điều phối viên: khóa do **1 worker khác đoán mật khẩu trước khi nhận bổ sung**;
  điều phối viên **ĐÃ mở khóa bằng SQL và kiểm chứng login prod thành công trở lại**.
- Sau mở khóa, worker này login prod 3 app (PDS/HOSP/Base) đều **thành công ngay lần đầu** bằng `000000/000000`.
  Hiện tại SELECT demo xác nhận `Locked = 0`.
- Worker này KHÔNG đoán mật khẩu nào khác; khi gặp thông báo khóa đã DỪNG đường login prod ngay và chỉ quay lại
  sau thông báo mở khóa. Các ảnh `*-login-locked.png` là bằng chứng của giai đoạn bị khóa (chúng chụp trang login, không phải trang chính).

### 1.2 SQL Server chết 3 lần trong phiên (do tải VM, không phải do test)

- SQL Server (1433) chết và được hồi phục bằng `~/workspace/recovery.sh` (idempotent, chỉ khởi động service đang down,
  không đụng staging) vào các thời điểm: ~07:51 UTC, ~08:19–08:25 UTC, ~08:2x UTC.
- Hậu quả trực tiếp, đã chứng minh bằng thử lại sau hồi phục (không phải do mật khẩu):
  lần login HOSP staging đầu tiên và lần login Base staging đầu tiên đều báo lỗi chung chung
  (tiếng Trung "登录失败，请联系管理员"); sau khi SQL hồi phục, cả hai login đều thành công ngay.
  BaseAPI staging trong lúc SQL chết trả `code 20000 "Không kết nối được cơ sở dữ liệu"`.
- RAM VM ~8GB với 24+ tiến trình dotnet + SQL + nhiều browser song song của 5 worker là nguyên nhân gốc có thể nhất.
  Đề xuất cho điều phối viên (không tự làm): cân nhắc giảm tải song song hoặc tăng RAM trước vòng fix.

### 1.3 Binary staging cũ hơn prod — kết quả staging phải đọc kèm vintage

- Staging worktree `~/workspace/openhis-stg` ở commit `0a730b1b` (file PDS Form đã có guard PDS-03);
  prod đã redeploy `e66c7bda`.
- Hệ quả thấy rõ: `Home/Index` ẩn danh trên staging HOSP/Base trả 200 + JSON NRE kèm stack trace
  (thiếu guard `opr == null`), trong khi prod `e66c7bda` trả 302 về Login (guard đã có trong source mới).
  Vì vậy: lỗi tìm được trên staging mà source `e66c7bda` đã khác thì báo cáo ghi rõ vintage, không kết luận prod bị y hệt.

## 2. Các điểm còn nợ thuộc SWEEP-E — kết quả kiểm chứng

### 2.1 PDS-03 — LƯU THẬT THÀNH CÔNG TRÊN STG (đã dọn sạch)

Đường lưu thật (đã đọc code, không đoán): form `/BillManage/OutOrInStorageBill/Form` → phím F8
→ `DeleteCrkDj` (form mới, `crkId` rỗng) →
`POST /OutOrInStoredManage/InStorage/SubmitInStorage` (controller ghi đè `djlx = 1` (yaopinruku),
`rkbm = CurrentYfbm` của session; phiếu mới nhận `shzt = WaitingApprove`, bỏ qua `shzt` client gửi;
domain service chỉ INSERT, chưa động tồn kho — tồn kho chỉ động khi duyệt, bước duyệt KHÔNG thực hiện).

- Baseline STG PDS trước test: `xt_yp_crkdj = 112`, `xt_yp_crkmx = 1133`, marker `Pdh = 'SWEEP-E-PDS03'` = 0.
- Lời gọi F8 mô phỏng đúng payload UI gửi (form-encoded, mảng `mx[0][...]`, session cookie staging):
  `djlx=1, djh=SWEEP-E-PDS03, fyfs=1 (Nhap kho binh thuong), ckbm=00000030`,
  dòng `ypdm=00001915, sl=10, Zje=100.00, ph=SWEEP-E-LOT, pfj=8.00, lsj=10.00, rkdw=chai, Rkbmkc=1000, Rkzhyz=1, kl=0, pfjze=80.00`
  → trả `{"state":"success"}`.
- Hàng tạo được xác minh bằng SELECT: `crkId = 6401a48a-494f-40ba-a072-57f36beeebfc`,
  `Pdh = SWEEP-E-PDS03, djlx = 1, Ckbm = 00000030, Crkfsdm = 1, shzt = 0 (WaitingApprove),
  Rkbm = 00000018` (khoa dược của user demo, server tự resolve), `CreatorCode = 000000`;
  chi tiết `Ypdm = 00001915, Sl = 10, Zje = 100.0000, Ph = SWEEP-E-LOT, Pfj = 8.0000, Lsj = 10.0000`.
  Đếm sau lưu: `crkdj = 113, crkmx = 1134`.
- Dọn dẹp: `DELETE xt_yp_crkmx + xt_yp_crkdj WHERE crkId = ...` trên STG → đếm trở về `112 / 1133`,
  marker = 0. Demo kiểm tra cuối: marker = 0.
- Ghi chú kèm theo (không phải bug chặn lưu):
  - `DeleteCrkDj` với `crkId` rỗng trả `{"state":"error","message":"抱歉，系统处理异常！请查看系统日志。"}` (tiếng Trung, xem E-04).
    Luồng F8 vẫn tiếp tục lưu vì JS chỉ chặn khi `res < 0` (object lỗi không thỏa) — đúng như phân tích phase 2.
  - Form mới: `GetCrkDjh` trả `null` (chưa có chứng từ) — guard PDS-03 xử lý đúng (bỏ qua lặng khi không có `crkId`).
  - Lỗi load `res is null` gốc: KHÔNG tái hiện trên form staging (guard đã có cả ở source `e66c7bda` lẫn worktree staging).

### 2.2 PDS Hospitalization ZYTYInterface — quirk AllFailure là hành vi gốc, ghi đúng, không "sửa"

- Code (`Newtouch.HIS.PDS/Newtouch.PDS.API/Controllers/HospitalizationController.cs`):
  sau vòng lặp, `res.ResultCode = (int)ResultCode.AllFailure` được gán VÔ ĐIỀU KIỆN (dòng ~153),
  và các dòng thành công KHÔNG được append vào `resultList` — nên nhánh thành công cho
  `IsSucceed = true, ResultCode = AllFailure (2), Data = []`. Comment trong code ghi rõ đây là quirk gốc, client cũ có thể dựa vào.
- Kiểm chứng live (PDS API staging 15337, rồi kill):
  - Body rỗng / `null` → `IsSucceed = false, ResultCode = 3 (ValidationFailure), ResultMsg = "Dữ liệu yêu cầu trống"` (prod cũng cho kết quả này — probe validation, không chạm DB).
  - Thiếu field → `ErrorCode = 1 "Số y lệnh không được để trống"`, `ResultCode = 2`.
  - `yzId` không tồn tại → `ErrorCode = 3 "Không thể tra cứu thông tin y lệnh"`, `ResultCode = 2`.
  - Nhánh thành công KHÔNG live-provable trong khuôn khổ sweep, lý do cụ thể (không phải do thiếu cố gắng):
    điều kiện thành công cần dòng `zy_ypyzxx` có `fybz` khác 1/3 và `sqtybz = 0` (vì `sqtybz` là `int?`,
    `NULL.ToString()` = `""` ≠ `"0"` → rơi vào `Error6 "đã yêu cầu trả thuốc"` — đã kiểm chứng live với dòng demo
    `fybz = 2, sqtybz = NULL`: trả `Error6`, không UPDATE, STG giữ `NULL` trước/sau).
    Toàn bộ demo `zy_ypyzxx` chỉ có 4 dòng: 1 dòng `(fybz = 2, sqtybz = NULL)` và 3 dòng `(fybz = 3, sqtybz = 1)` —
    KHÔNG có dòng đủ điều kiện nào; hơn nữa `IsCouldReturn` đọc hardcode DB demo
    (`NewtouchHIS_PDS.dbo.zy_ypyzxx`, `NewtouchHIS_Base.dbo.V_C_xt_yp` qua `NewDb()` với `"NewtouchHIS_PDS"` cố định),
    trong khi UPDATE dùng `DbNames` theo DBConf (staging → STG). Tạo dòng đủ điều kiện bắt buộc phải ghi demo → CẤM.
    Kết luận: quirk được xác nhận ở mức code + nhánh thất bại live; nhánh thành công cần fixture demo (quyết định của điều phối viên, vòng sau).
- Ghi nhận thêm (hiện trạng, không sửa): eligibility read hardcode demo DB như trên — trên staging, probe đọc demo (chỉ SELECT),
  ghi (nếu đủ điều kiện) đi vào STG. Dưới launcher prod cả hai đều là demo nên nhất quán.

### 2.3 BaseAPI Query / HOSP API Query 10000 — HỒI QUY THÀNH CÔNG TRÊN PROD

- BaseAPI CheckPwd (user không tồn tại, cả prod 7001 lẫn staging 17001):
  `busData code 40004 "Không tìm thấy thông tin người dùng"` — không còn 500, đúng envelope.
- HOSP API prod 5333 với token thật (GetToken bằng credential thật → `code 10000` + token):
  - `POST /api/MedicineStorageIOMode/Query {crkbz: "1"}` → `code 10000` + danh sách phương thức xuất kho
    (vd: `Xuat kho binh thuong`, `Tra thuoc kho duoc`, `Xuat kho dieu chuyen`…).
  - `POST /api/MedicineSupplier/Query {OrganizeId: <demo org>}` → `code 10000` + danh sách nhà cung cấp.
- Cổng token (prod, không token): body hợp lệ + thiếu Timestamp → `40004 TIMESTAMP_IS_REQUIRED`;
  có Timestamp, thiếu Token → `40004 TOKEN_IS_REQUIRED "Token truy cập trống"`;
  token giả → `40004 TOKEN_IS_EXPIRED "Token truy cập đã hết hạn hoặc không hợp lệ"`.
  (Body thiếu field bắt buộc thì `[ApiController]` trả 400 validation trước filter — hành vi chuẩn, không phải bypass.)
- PDS API staging: GetToken credential thật → `code 10000` + token (lần đầu AUTH_FAILED chỉ vì SQL chết, xem §1.2;
  thử lại sau hồi phục thì thành công); endpoint cần token `GetDeptPharmacyDrugs {DeptCode: "mzyf"}` + token
  → `code 10000`, nhưng `data` là chuỗi `" GetDeptPharmacyDrugs 123123123"` (xem quan sát E-06).

### 2.4 PDS djqz, menu chết, SysMedicine.submitForm, schema

- `djqz`: chỉ là field chuỗi trên `SysMedicineReceiptEntity` (`djqz { get; set; }`), không có mapping enum nào
  trong code PDS — GIỮ NGUYÊN, không ép về enum. (Worker này không đổi gì.)
- Menu chết PDS-07: SELECT demo + STG đều cho 3/3 dòng `zt = 0`:
  `PharmaceuticalStocks`, `DoctorsOrderQuery`, `DecoctingfeeQuery`; URL trực tiếp vẫn 404 (đúng kỳ vọng — fix là gỡ khỏi menu, không tạo stub).
  Menu sống không ảnh hưởng: 24 menu con dưới 2 cha, trong đó 18 dòng `zt = 1` còn nguyên
  (3 dòng `zt = 0` còn lại là các dòng đã tắt từ trước, không phải do PDS-07);
  bảng backup `Sys_Module_QA2_PDS07_BACKUP` đủ 3 dòng cả demo lẫn STG.
- `SysMedicine.submitForm` (PDS Web): toàn bộ action đã bị comment trong source (`[HttpPost]` + body đều comment),
  controller vẫn build bình thường; `POST /SysMedicine/submitForm` → 404; đường thêm thuốc sống là `SysMedicineAdd`.
  Ghi đúng hiện trạng (controller này đã loại khỏi build ở một số diện khác — xem §5).
- Schema `xt_bqksby` / `xt_ksbyth`: code PDS có entity + SQL đọc 2 bảng này (`DispenseIndexInfoDmnService`),
  nhưng `sys.tables` của demo Base KHÔNG có 2 bảng này (chỉ có `xt_sfxm`, `xt_yp` trong các bảng đã kiểm tra) —
  khớp ghi nhận leftover phase 7-fix (entity ↔ schema mismatch, bảng không tồn tại ở DB version này).
  Worker này không sửa entity/SQL, chỉ ghi nhận.

## 3. Bảng phủ chức năng

Quy ước cột Kết quả: ✅ = kiểm chứng có xác thực (ghi trên STG nếu là ghi);
🔒 = kiểm tra cổng ẩn danh (302 về Login) và/hoặc probe validation, chưa đi sâu từng action có xác thực;
📋 = liệt kê từ controller source + menu/view, chưa kiểm chứng từng action (ghi rõ để không báo quá).

### 3.1 PDS Web 5237 (+ staging 15237)

| Nhóm chức năng (controller/area) | Kiểm tra đã làm | Viewport | Kết quả |
|---|---|---|---|
| Login / Home / dashboard (HomeController, LoginController) | Login page desktop+mobile (0 lỗi console, validation rỗng → thông báo VI); login prod + staging thành công; Home prod 200 (curl, có xác thực); Home staging browser (cookie) | Desktop + mobile | ✅ login; 🔒 Home prod (curl); ✅ Home staging browser nhưng kèm modal lỗi widget (E-03) |
| BillManage: tra cứu/duyệt chứng từ (OutOrInStorageBill: Query/Approval/DrugPreparation/OutOrder) | Form 200 prod (có xác thực); Query ẩn danh 302; GetCrkfs prod+staging; lưu thật staging qua SubmitInStorage | Desktop + mobile (login); Form kiểm tra qua curl | ✅ Form load; 🔒 trang Query (chỉ cổng); 📋 Approval/DrugPreparation/OutOrder (liệt kê source) |
| BillManage: LƯU phiếu nhập kho (PDS-03, SubmitInStorage) | Lưu thật + xác minh hàng + dọn sạch (STG) | — (API/form POST) | ✅ (xem §2.1) |
| OutOrInStoredManage: nhập/xuất/trả/chuyển (InStorage, Delivery*, Return*, Transfer*) | Liệt kê source; TransferController đã loại khỏi build (xem §5); chỉ kiểm chứng đường InStorage qua PDS-03 | — | 📋 các controller còn lại (chưa kiểm chứng từng action có xác thực) |
| StorageManage: tồn kho/tra cứu (DrugStockSearch) | PharmacyDrugStorage/InventoryQuery ẩn danh 302; GetCrkDjh mới → null (đúng, có xác thực staging); các action tra cứu sâu khác chưa gọi có xác thực | — | 🔒/📋 |
| ReportManage: báo cáo (YpckQuery, GqYpcx, JsYpcx, Ypfy, Zyfytj, Purchase, Outpatient, yf) | URL chết DoctorsOrderQuery/DecoctingfeeQuery → 404 (đúng kỳ vọng sau dọn menu); các controller khác chỉ liệt kê | — | 📋 (trừ 2 URL chết đã xác nhận) |
| Thuốc/danh mục (Medicine, SysMedicine, Dictionaries, AntibioticType, PharmacyDepartment, PharmacyWindow, DrugStorage, MedicineStorageIOMode) | MedicineSupplierList (không keyword → `[]`; có xác thực, prod+staging); GetCrkfs ✅; SysMedicine Index ẩn danh 302; submitForm → 404 (đã comment) | — | 🔒 + ✅ (2 endpoint đọc) |
| testPrint / test actions (web) | `/testPrint/Index` ẩn danh 302; có xác thực prod → 200 | — | ✅ reachable (chỉ kiểm tra cổng/trang, không chạy in) |
| PDS API: Auth/GetToken, Stock, Hospitalization, DeptPharmacyDrugs, Outpatient, Resources, ClinicTf, Zyypyz, PharmacyDepartment, Test | GetToken ✅ (staging, token thật); Stock Query no-token 10000 (hành vi gốc có chủ đích, xem §5); ZYTY ✅ validation/thất bại; DeptPharmacyDrugs + token ✅ 10000 (data placeholder, E-06); Test* KHÔNG gọi (write-path, chờ quyết định) | — | ✅ trừ Test* (chưa gọi, có chủ đích) |

### 3.2 Base HOSP Web 5233 (+ staging 15233) và HOSP API 5333

| Nhóm chức năng | Kiểm tra đã làm | Viewport | Kết quả |
|---|---|---|---|
| Login / Home | Login page desktop+mobile (0 lỗi console); login prod + staging thành công; Home prod 200 (curl, có xác thực); Home/Default staging 200 (shell ẩn danh, frame không dữ liệu user) | Desktop + mobile (login) | ✅ login; 🔒 Home (curl) |
| Sys: User/Role/Organize/Department/Staff/Module/ModuleButton | Organize tree ✅ (demo org `Benh vien Newtouch HIS`); User grid ✅ (cần OrganizeId, xem §4.3); Role grid ✅; Staff grid ✅; Department tree ✅ | — | ✅ (đọc) |
| Sys: tạo khoa (Department SubmitForm) | Tạo thật trên STG: `Code = SWEEP-E, Name = Khoa Test Sweep E`, Id tạo mới, `CreatorCode = 000000` → đã xóa bằng SQL STG, kiểm chứng tree hết SWEEP | — | ✅ (ghi STG + dọn sạch) |
| Rehabilitation: phân loại khoản thu (RehabChargeClassification) | Tạo mới → LỖI (E-01); list baseline `[]` cho keyword SWEEP-E; sửa/xóa chưa test được (không tạo được hàng qua web) | — | ❌ tạo mới (bug E-01) |
| Settlement: SysChargeItem, SysDiagnosis, Ward, ChargeCategory… | SysChargeItem grid ✅ (cần OrganizeId; không OrgId → `[]` theo code); Ward grid ✅ envelope (`records = 0`) | — | ✅ (đọc) |
| PharmacyDrugStorage / ReportManagement / CommonLibrary / Consult | Chỉ liệt kê source; cổng ẩn danh 302 đã kiểm chứng cho các Index chính (User/Organize/Role/Department/SysChargeItem/Ward); chưa kiểm chứng từng action có xác thực | — | 📋 |
| Notice (web `api/notice/getmsg`, `sendNoticeToUser`) | Ẩn danh → 302 về Login (web). API 5333 `getmsg` → 200 `"value"` không cần auth (xem §5, quyết định mở) | — | 🔒 web; ⚠️ API (ghi nhận hiện trạng) |
| HOSP API: Auth/GetToken, MedicineStorageIOMode/Supplier Query, ChargeCategory, Items, Medicine, Formulation, Ward, Notice | GetToken prod ✅ 10000; Query ✅ 10000 (xem §2.3); no-token gates ✅; ChargeCategory/Items/Medicine/Formulation/Ward chỉ liệt kê (controller CRUD scaffold, chưa gọi) | — | ✅ Auth + 2 Query; 📋 còn lại |

### 3.3 Base Web 5238 (+ staging 15238)

| Nhóm chức năng | Kiểm tra đã làm | Viewport | Kết quả |
|---|---|---|---|
| Login / Home | Login page desktop+mobile (0 lỗi console); login prod + staging thành công; Home prod 200 (curl) | Desktop + mobile (login) | ✅ login; 🔒 Home (curl) |
| PharmacyDrugStorage: SysMedicineUsage | Grid ✅ (prod + staging, vd `yfCode 00001 Sac truoc`); tạo mới → LỖI (E-02); sửa/xóa chưa test (không tạo được hàng) | — | ❌ tạo mới (bug E-02) |
| Sys: SysNation | Tạo mới → LỖI y hệt E-02 (bảng `xt_mz`); grid chưa kiểm chứng sâu | — | ❌ tạo mới (bug E-02) |
| SysGlobalConfig | Tạo thật trên STG: `Code = Name = SWEEP-E-TEST`, Id mới, `CreatorCode = 000000` → đã xóa bằng SQL STG, grid hết SWEEP | — | ✅ (ghi STG + dọn sạch) |
| Các nhóm còn lại (Organize, Application, Duty, ItemsData, ItemsType, SysMedicineClassification/Formulation/IOMode/Unit, RehabChargeClassification/Item, SysDiagnosis, SysTCMSyndrome) | Liệt kê source + kiểm tra cổng ẩn danh (Organize/Index, SysMedicineUsage/Index → 302); chưa kiểm chứng từng action có xác thực | — | 📋 |
| Base Web không có Areas riêng (khác HOSP) | Đã đối chiếu Views/Controllers thực tế, không suy diễn route từ HOSP | — | — |

## 4. Bug tìm được

### E-01 (Cao) — HOSP: tạo mới phân loại khoản thu PHCN luôn lỗi NULL `sfflId`

- App/trang/action: HOSP Web staging 15233, `POST /RehabChargeClassification/SubmitForm` (tạo mới, không có `sfflId`).
- Tái hiện: login staging → POST form `Code = SWEEP-E, Name = SWEEP-E-TEST, OrganizeId = <demo org>, zt = true, py = SWEEP`
  (không `sfflId`) → lỗi. List sau đó vẫn `[]`.
- Mong đợi: tạo hàng mới, trả `{"state":"success","message":"Thao tác thành công."}`.
- Thực tế: `{"state":"error","message":"Cannot insert the value NULL into column 'sfflId', table 'NewtouchHIS_Base_STG.dbo.kf_sffl'; column does not allow nulls. INSERT fails."}`
  kèm stack trace tới `RehabChargeClassificationRepo.SubmitForm` (dòng Insert).
- Nguyên nhân gốc (đã chứng minh bằng code + chương trình test .NET mini):
  nhánh insert gọi `entity.Create(true, System.Guid.NewGuid())`; `EntityBase.SetPk` chạy
  `Convert.ChangeType(Guid, typeof(string))` → ném `InvalidCastException` ("Object must implement IConvertible")
  và bị `catch {}` nuốt lặng → `sfflId` giữ `NULL` → INSERT lỗi.
  Code này vẫn có trong source `e66c7bda` (`RehabChargeClassificationRepo.cs` dòng ~81), nên KHÔNG phải chỉ do binary staging cũ.
- Phạm vi có thể (chưa kiểm chứng live, chỉ cùng mẫu code): `RehabChargeItemRepo` và `RehabChargeItemComparisonRepo`
  cũng dùng `Create(true, System.Guid.NewGuid())` — vòng fix nên kiểm chứng cả hai.
  Nhánh sửa (có `sfflId`) và xóa (theo `sfflId + OrganizeId`) không bị mẫu lỗi này (chưa test live vì không tạo được hàng).

### E-02 (Cao) — Base Web: tạo mới danh mục khóa int luôn lỗi IDENTITY

- App/trang/action: Base Web staging 15238, `POST /SysMedicineUsage/SubmitForm` (tạo mới) và `POST /SysNation/SubmitForm` (tạo mới).
- Tái hiện: login staging → POST `yfCode = SWEEP-E, yfmc = SWEEP-E-TEST, py = SWEEP, zt = true, px = 99` (không `yfId`)
  → lỗi; tương tự `mzCode = SWEEP-E, mzmc = Dan toc Test Sweep E` → lỗi. Grid sau đó không có hàng SWEEP.
- Mong đợi: tạo hàng mới với id tự tăng, trả success.
- Thực tế:
  - Usage: `"Cannot insert explicit value for identity column in table 'xt_ypyf' when IDENTITY_INSERT is set to OFF."`
  - Nation: `"Cannot insert explicit value for identity column in table 'xt_mz' when IDENTITY_INSERT is set to OFF."`
- Nguyên nhân gốc: entity dùng `[SugarColumn(IsPrimaryKey = true)]` trên khóa `int` (`yfId`, `mzId`) nhưng
  KHÔNG có `IsIdentity`; repo nhánh insert gọi `entity.Create()` (không set PK) rồi `Insert(entity)` —
  SqlSugar chèn `yfId = 0` tường minh → SQL Server từ chối. Mẫu này vẫn có trong source `e66c7bda`.
- Phạm vi có thể (cùng mẫu entity, chưa kiểm chứng live từng cái): SysMedicineUnit (`ypdwId`), SysMedicineClassification
  (`ypflId`), SysMedicineFormulation (`jxId`), SysMedicineStorageIOMode (`crkfsId`), SysNationality (`gjId`),
  SysDiagnosis (`zdId`), SysTCMSyndrome (`zhId`) — vòng fix nên kiểm chứng cả loạt.
- Đường ghi Base Web chứng minh được: `SysGlobalConfig.SubmitForm` (khóa chuỗi, `Create(true)` → `Guid.ToString()`)
  tạo thành công (xem §3.3) — cho thấy lỗi E-02 đặc thù nhóm khóa int, không phải toàn bộ Base Web.

### E-03 (Trung bình) — PDS Home staging: 3 widget lỗi, modal chặn trang chính

- App/trang/action/viewport: PDS Web staging 15237, `/Home/Index`, cả desktop (390×844 mobile cũng vậy) — ảnh
  `sweepE-pds-stg-home-desktop.png` (trang xám + modal) và `sweepE-pds-stg-home-mobile.png` (widget sau modal).
- Tái hiện: login staging → mở Home → modal "Thông báo hệ thống" với nội dung tiếng Trung (xem E-04) xuất hiện;
  gọi trực tiếp bằng session: `GetPendingCount`, `GetNeedDealDiv`, `getmesquery` đều trả
  `{"state":"error","message":"抱歉，系统处理异常！请查看系统日志。",...}`.
- Mong đợi: dashboard hiển thị số chờ xử lý / thông báo tồn kho, không có modal lỗi.
- Thực tế: 3 endpoint lỗi; 2 endpoint thống kê khác vẫn tốt (`GetFyCountBydl → []`, `GetFyCountVoByYfbm → 2 dãy tháng toàn 0`).
- Chưa isolate được nguyên nhân gốc: response giấu stack trace (`exStackTrace: null`); log của tiến trình staging hiện tại
  không xác định được file (log staging cũ chỉ có lỗi khác từ 27/10). Ghi nhận thêm: staging là binary cũ —
  vòng fix cần tái hiện trên binary `e66c7bda` trước khi kết luận prod có bị không (prod Home HTML 200 qua curl,
  widget prod chưa kiểm chứng bằng browser do VM thrash, xem §6).

### E-04 (Thấp — quan sát dịch) — Thông điệp lỗi framework chung vẫn là tiếng Trung

- Chuỗi `抱歉，系统处理异常！请查看系统日志。` ("Xin lỗi, hệ thống xử lý bất thường! Vui lòng xem nhật ký hệ thống.")
  xuất hiện ở: `DeleteCrkDj` với `crkId` rỗng (PDS) và 3 widget Home staging (E-03).
- Đây là thông điệp của tầng xử lý ngoại lệ chung, chưa qua chương trình dịch-all. Ghi nhận để vòng dịch/fix xử lý;
  worker này không tự dịch chuỗi framework (ngoài phạm vi sweep).

### E-05 (Thấp — đã deferred từ phase 2) — PDS Home mobile vẫn bóp méo

- Ảnh `sweepE-pds-stg-home-mobile.png`: frame AdminLTE desktop bị nén vào 390px, sidebar/menu đè lên nhau,
  widget "Thống kê phát thuốc" chen chúc, kèm modal E-03. Đúng trạng thái PDS-06 đã deferred (cần redesign, không sửa trong sweep).

### E-06 (Quan sát — cần chủ sở hữu xác nhận) — PDS API `GetDeptPharmacyDrugs` trả chuỗi placeholder

- `POST /api/DeptPharmacyDrugs/GetDeptPharmacyDrugs {DeptCode: "mzyf"}` + token thật (staging)
  → `code 10000, data = " GetDeptPharmacyDrugs 123123123"`.
- Đây là chuỗi trông như residue test, không phải danh sách thuốc theo khoa. Ghi nhận nguyên văn để chủ sở hữu
  xác nhận service này là stub hay có logic thật theo `DeptCode` khác; worker này không kết luận là bug.

### E-07 (Quan sát dữ liệu demo, không phải bug code) — Một số tên danh mục vẫn tiếng Trung trên demo

- Nhà cung cấp `xt_ypgys`: `00000030 = 测试供应商`, `00000032/00000033` tên công ty tiếng Trung (cả demo lẫn STG,
  kiểm chứng bằng SELECT và bằng HOSP API Supplier Query prod).
- Tổ chức demo: `Address = 新致开源` (trong Organize tree prod).
- Khoản thu STG: `sfxmmc = 门诊手册` (SysChargeItem grid staging).
- Các giá trị này thuộc phạm vi dữ liệu/dịch, không phải lỗi controller; xem thêm quyết định mở M-09/O-05 ở §5.

## 5. Các quyết định mở — CHỈ GHI NHẬN HIỆN TRẠNG, không tự quyết, không redesign

- **PDS test actions**: web `testPrintController` chỉ có `Index` (trang test in), prod có xác thực → 200.
  API `TestController` (`api/test`): `TestMultithreadTrans` / `TestEFDbTransaction` còn route nhưng KHÔNG gọi thử live
  (chạy transaction với dữ liệu thử nghiệm — write-path, theo chính sách no-write chỉ biên dịch/nối dây);
  `TestToken` đã XÓA ở phase 2 (ghi trong code). Quyết định vô hiệu hóa riêng 2 action test còn lại vẫn chờ (như báo cáo api-auth phase 2).
- **HOSP getmsg**: web (`HOSP Web api/notice/getmsg`) ẩn danh → 302 về Login (filter MVC chặn).
  API (`HOSP API api/notice/getmsg`) KHÔNG yêu cầu auth: `GET .../testuser/1` → 200 `"value"` đồng thời broadcast
  SignalR tới tất cả client (`Clients.All.SendAsync("showMessage", ...)`). Ghi nhận nguyên trạng, không đề xuất chặn/mở.
- **Report `[NewtouchHIS_Base]` read leak**: grep toàn bộ source HOSP Web KHÔNG thấy chuỗi `NewtouchHIS_Base`
  (controller `ReportManagement` chỉ delegate sang `_sysReportDmnService`). Read xuyên DB hardcode thực tế tìm thấy
  nằm ở PDS, không phải HOSP Report: `GetCrkDjh` join `NewtouchHIS_Base..xt_ypgys`; `ZYTYInterface` đọc
  `NewtouchHIS_PDS.dbo.zy_ypyzxx` + `NewtouchHIS_Base.dbo.V_C_xt_yp` (xem §2.2). Ghi nhận, không sửa.
- **SysMedicine.submitForm (PDS)**: đã comment toàn bộ (xem §2.4); controller PDS `SysMedicine` vẫn build, các action
  khác (`GetGridJson`, `SysMedicineAdd`, …) còn nguyên. Ngoài ra, csproj PDS Web loại khỏi build:
  `AntibioticTypeController`, `SystemManagerController`, `Base/ModuleController`, `Base/OrganizeController`,
  `Base/RoleAuthorizeController`, `Base/RoleController`, `Areas/OutOrInStoredManage/TransferController`
  (ghi đúng hiện trạng build, không đề xuất khôi phục).
- **Schema `xt_bqksby` / `xt_ksbyth`**: xem §2.4 — entity + SQL PDS tham chiếu 2 bảng không có trong demo Base.
- **Ứng viên dịch M-09 (`xt_sfxm`)**: vẫn là ứng viên (danh mục khoản thu, vd `sfxmmc` tiếng Trung còn thấy ở §E-07);
  worker này không dịch gì thêm.
- **O-05: explicitly KHÔNG phải ứng viên dịch** (danh mục phẫu thuật OR, ngoài phạm vi SWEEP-E):
  worker này không đề xuất dịch nó, không đụng tới.
- **Tiền tố số chứng từ**: ghi đúng hiện trạng, không tự thống nhất —
  comment enum PDS vẫn ghi tiền tố cũ (`YPRKD`, `WBCKD`, `NBFYD`, `SLCKD`, `NBFYTHD`);
  `GetCrkDjh` chỉ đọc lại số của chứng từ đã có (không sinh số mới); phiếu mới qua Form có `djh` trống
  (test PDS-03 dùng marker `SWEEP-E-PDS03` để định danh, không suy ra quy tắc sinh số thật).
  Luồng tiền tố mới Herp/CIS thuộc worker khác, không kiểm chứng lại ở đây.
- **PDS Stock/Query no-token → 10000**: hành vi gốc có chủ đích đã ghi ở phase 2 (API-03/API-07a):
  action không mang `[DefaultAuthorize]` vì caller thật là CIS (autoAppendToken: false); bề mặt này phải chặn ở tầng mạng.
  Worker này tái hiện đúng kết quả đó trên prod + staging và KHÔNG coi là bug mới.

## 6. Phần không tái hiện được / không test được + lý do

- **Ảnh trang chính prod bằng browser (3 app × 2 viewport)**: KHÔNG lấy được. Sau khi prod mở khóa, đã thử:
  cookie-injection (lỗi script của worker: bỏ qua dòng `#HttpOnly_` nên mất cookie token — đã sửa script, nhưng lần chạy
  sau đó vẫn kẹt ở `goto` do Firefox rơi vào trạng thái D (chờ I/O đĩa) dưới tải VM; thử form-login cũng kẹt tương tự).
  Thay thế bằng: Home prod 200 qua curl có xác thực cho cả 3 app + HTML title đúng + các grid đọc prod có xác thực (§3).
  Ảnh trang chính staging PDS (desktop + mobile) đã lấy được và là bằng chứng giao diện gần nhất.
  Ảnh login prod 6 file đầu tiên (giai đoạn bị khóa) đã đổi tên thành `*-login-locked.png` để khỏi nhầm với trang chính;
  file `sweepE-pds-prod-home-desktop.png` cũ (thực chất là ảnh login, trùng hash với ảnh FAILED) đã XÓA để khỏi sai nhãn.
- **Nhánh thành công ZYTYInterface live**: không thể mà không ghi demo (xem §2.2).
- **HOSP API staging**: không có launcher staging riêng (`run-*-stg.sh` không có HOSP API); HOSP API chỉ kiểm chứng trên prod
  (đọc + GetToken, không ghi DB) và qua cổng no-token. Vòng sau muốn staging HOSP API cần tạo launcher riêng (quyết định của điều phối viên).
- **Từng action có xác thực của mọi controller**: chỉ kiểm chứng đại diện + đọc/ghi then chốt như bảng §3;
  các action còn lại ở mức liệt kê source + cổng ẩn danh (302). Lý do: tải VM + SQL chết lặp lại + thời gian;
  không báo phần này là "đã test hết".
- **Sửa/xóa RehabChargeClassification, sửa/xóa SysMedicineUsage/SysNation**: không test được vì tạo mới lỗi (E-01/E-02)
  nên không có hàng test để sửa/xóa qua web; không xóa dữ liệu thật để test đường xóa.
- **Widget Home prod bằng browser**: chưa kiểm chứng (xem E-03 + §6 dòng 1); Home prod chỉ xác nhận 200 qua curl.

## 7. Ảnh đã chụp (thư mục `shots-sweep/`)

- Login (prod, desktop + mobile, 0 lỗi console, validation rỗng → thông báo VI):
  `sweepE-pds-login-desktop.png`, `sweepE-pds-login-mobile.png`,
  `sweepE-hosp-login-desktop.png`, `sweepE-hosp-login-mobile.png`,
  `sweepE-base-login-desktop.png`, `sweepE-base-login-mobile.png`.
- Trang chính PDS staging (cookie session, có xác thực):
  `sweepE-pds-stg-home-desktop.png` (trang xám + modal E-03),
  `sweepE-pds-stg-home-mobile.png` (widget + modal E-03, layout bóp méo E-05).
- Bằng chứng giai đoạn prod bị khóa (đã đổi tên, NỘI DUNG LÀ TRANG LOGIN, không phải trang chính):
  `sweepE-pds-prod-home-desktop-login-locked.png`, `sweepE-pds-prod-home-mobile-login-locked.png`,
  `sweepE-hosp-prod-home-desktop-login-locked.png`, `sweepE-hosp-prod-home-mobile-login-locked.png`,
  `sweepE-base-prod-home-desktop-login-locked.png`, `sweepE-base-prod-home-mobile-login-locked.png`.
- Ảnh là file untracked trong `shots-sweep/`; worker này KHÔNG commit gì nên ảnh không lọt vào commit.
  (Kiểm tra `git status` cuối phiên: xem §8.)

## 8. Trạng thái cuối phiên và toàn vẹn dữ liệu

- Tiến trình staging do worker này khởi động đã kill đúng PID và kiểm chứng cổng down:
  HOSP staging 15233 (PID 35924), Base staging 15238 (PID 38134), PDS API staging 15337 (PID 41221).
  Staging dùng chung không đụng trong phiên: SSO staging 15208 (PID 5454) và PDS staging 15237 (PID 5491)
  đã kiểm chứng còn lắng nghe cuối phiên; BaseAPI staging 17001 và Redis staging 6380 không bị kill
  (17001 còn trả CheckPwd 10000 sau lần hồi phục SQL cuối). Toàn bộ app prod không bị kill.
- STG trở về trạng thái trước test: PDS `xt_yp_crkdj = 112 / xt_yp_crkmx = 1133`, marker PDS-03 = 0;
  HOSP STG dept marker `SWEEP-E` = 0; Base STG GlobalConfig marker `SWEEP-E-TEST` = 0;
  dòng ZYTY STG `sqtybz` giữ `NULL` trước/sau (không có UPDATE nào chạm tới nó).
- Demo: KHÔNG có ghi nào. Kiểm chứng cuối: marker PDS-03 = 0, marker GlobalConfig = 0, marker Department = 0;
  demo `000000` hiện `Locked = 0` (sau mở khóa của điều phối viên).
  Các phiên login/token prod chỉ tạo session/token Redis tạm thời, không ghi DB.
- Git: worker này không sửa file code nào, không sửa `roles-shots-progress.md` (trạng thái `M` của file đó có từ trước phiên),
  không commit. File báo cáo này + ảnh `shots-sweep/` là untracked (cố ý, chờ điều phối viên quyết định).
- Việc chuyển cho vòng fix (không phải quyết định của worker này): E-01 (sửa `Guid` → `Guid.ToString()` hoặc sửa `SetPk`),
  E-02 (thêm `IsIdentity` hoặc sửa repo insert cho nhóm khóa int), E-03 (tái hiện trên binary `e66c7bda` + bật log server),
  E-04 (dịch thông điệp lỗi framework), E-06 (xác nhận DeptPharmacyDrugs stub), và tạo launcher HOSP API staging nếu cần.
