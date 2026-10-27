# Giai đoạn 6 — Staging + kiểm chứng toàn bộ luồng ghi (REPORT)

> Báo cáo chính của giai đoạn. Cập nhật append theo từng app, ngay khi app đó
> xong. KHÔNG commit/push trong giai đoạn này (điều phối viên soát cuối).
> Không mật khẩu, không token đầy đủ trong file này.

## 0. Trạng thái lúc bắt đầu

- Repo: `~/workspace/openhis-core`, nhánh `V2.2`, tip `0a730b1b`
  ("Giai doan 5-fix: dong bo DLL 8016 tu HEAD (sua MissingMethod FileConfig)
  + bo Environment.Exit duong DRG + null-check List").
- `git status` lúc bắt đầu: **sạch** (không có file dirty).
- Spec: `~/workspace/production-readiness-design.md` §3 (phương án C).
- Stack prod lúc bắt đầu: 25 cổng LISTEN đã xác nhận (1433 SQL, 6379 Redis,
  5208 SSO, 7001 BaseAPI, 7002 Sett-API, 8015 AuthCenter, 8016 WebAPI.Manage,
  11 web 5220/5221/5231/5232/5233/5234/5235/5236/5237/5238 + 7 API
  5320/5331/5332/5333/5334/5335/5336/5337 — đếm thực tế 25, không phải 26
  như brief ghi; Sett staging 5222 cố ý tắt, ngoài phạm vi).
- RAM lúc bắt đầu: tổng 7935 MB, available ~508 MB → chỉ chạy 1 cụm staging
  tại một thời điểm, dừng ngay sau mỗi app.
- Đĩa lúc bắt đầu: 66 GB trống.

## 0.1. Hạ tầng staging dùng chung

- **DB staging**: clone toàn bộ DB demo thành `*_STG` trên cùng SQL Server
  (backup `WITH COMPRESSION` vào `~/workspace/sqlbackup-stg/`, restore
  `WITH MOVE` sang file `*_STG` riêng dưới `~/workspace/sqldata/data/`).
  `NewtouchHIS_Sett_STG` đã có từ giai đoạn 3 → dùng lại, không tạo mới.
  KHÔNG BAO GIỜ restore đè lên DB demo.
- **Redis staging**: cổng 6380, thư mục dữ liệu riêng, mật khẩu staging riêng
  chỉ nằm trong `~/.redis-stg.env` (600, không ghi vào repo, không in ra đây).
  Mọi app staging override `RedisConfig__Connection` sang 6380.
- **BaseAPI staging**: cổng 17001, launcher `run-baseapi-stg.sh`, override toàn
  bộ `BusinessDB__DBConf__N__DBName` sang `*_STG` (thứ tự DBConf: 0 BaseDb,
  1 SettDb, 2 EmrDb, 3 MrmsDb, 4 MrQcDb, 5 CisDb, 6 UnionDb) + DefaultConn
  từ `~/.sqlserver.env` (mẫu run-his-baseapi.sh). Mọi app staging trỏ
  `HisAppBaseAPIHost`/`SiteBaseAPIHost` sang `http://127.0.0.1:17001/`.
- **Quy ước cổng staging**: BaseAPI 17001, Sett-API 17002, WebAPI.Manage
  18016, AuthCenter 18015, web app 15xxx (15221 Sett, 15234 CIS, ...),
  API module 15xxx (15334 CIS-API, ...). Không đụng cổng prod, không thêm
  vào recovery.sh.
- **Chứng minh không ghi demo**: trước/sau mỗi app, đếm dòng các bảng ghi
  liên quan ở DB demo (SELECT COUNT, chỉ đọc) và ghi số liệu vào mục app đó.
- **Tài khoản kiểm chứng**: 000000 (bản sao trong DB STG, không phải tài
  khoản prod thật).

## 0.2. Tiến trình clone DB STG

- Chạy 2026-10-27 buổi tối bằng `scripts/staging/clone-stg.sh`
  (backup `WITH COMPRESSION` → restore `WITH MOVE`, log đầy đủ tại
  `~/workspace/sqlbackup-stg/clone.log`): **14/14 OK**.
- Mới clone: Base, Union, CIS, EMR, MRMS, MRQC, OR, herp, PDS, Interface,
  His_Log, Job, bdg (13 DB, hậu tố `_STG`).
- Dùng lại từ giai đoạn 3: `NewtouchHIS_Sett_STG` (không tạo mới, không xóa;
  lưu ý: STG này có thể cũ hơn demo hiện tại vì demo đã qua đợt dịch DB
  sau giai đoạn 3 — chỉ ảnh hưởng ngôn ngữ dữ liệu nền, không ảnh hưởng
  việc kiểm chứng ghi).
- Backup nguồn giữ tại `~/workspace/sqlbackup-stg/*.bak` (để đợt sau dùng
  lại hoặc drop theo quyết định điều phối viên).
- Redis staging 6380 UP (config riêng `~/workspace/redis-stg/`, mật khẩu
  riêng chỉ trong `~/.redis-stg.env` 600).

## 0.3. Phát hiện hạ tầng: mọi app hardcode cổng prod (cách xử lý staging)

- Khảo sát `Program.cs`: **mọi** web/API (trừ SSO/BaseAPI dùng
  ASPNETCORE_URLS và Sett-API đọc `SETT_API_URLS`) đều hardcode
  `WebHost.UseUrls("http://127.0.0.1:52xx/53xx")`. UseUrls thắng cả
  biến môi trường ASPNETCORE_URLS lẫn tham số `--urls` (đã thử cả hai
  trên Sett, vẫn bind 5221) — nên binary prod KHÔNG THỂ chạy staging
  trên cổng riêng bằng env.
- Cách xử lý đã chọn (không đụng code/binary prod, không dừng prod):
  git worktree riêng `~/workspace/openhis-stg` tại đúng commit
  0a730b1b, chỉ đổi literal cổng trong `Program.cs` của worktree
  (52xx→152xx, 53xx→153xx), build staging từ worktree khi cần, launcher
  staging trỏ vào DLL worktree. Main checkout giữ nguyên, không commit
  gì trong giai đoạn này; điều phối viên có thể xóa cả worktree sau
  giai đoạn. Sett-API không cần rebuild (đọc `SETT_API_URLS`).
- Hệ quả: mỗi app cần 1 build staging (tuần tự, `-m:1`, ~40 giây cho
  Sett web) trước khi kiểm chứng; build xong mới chạy app đó.

---

## 1. Sett — XONG (checkpoint 2026-10-27 tối)

- Cụm staging: BaseAPI-stg 17001 (PID 24580) + Sett web-stg 15221
  (PID 25281, build từ worktree `~/workspace/openhis-stg` — xem §0.3) +
  Sett-API-stg 17002 (PID 25961) + Redis 6380. **Tất cả đã dừng** sau
  kiểm chứng; prod 5221/7001/7002/5208 vẫn UP (xem sự cố pkill cuối mục).
- DB: `NewtouchHIS_Sett_STG` (dùng lại từ giai đoạn 3) + Base/Interface
  STG cho Sett-API host. Tài khoản test: bản sao 000000 trong STG.

### 1.1. Sett web (15221, cookie login 000000 qua BaseAPI-stg — ĐẠT login)

Mọi action ghi trong phạm vi đều là **stub giai đoạn 1 (chỉ xác minh
đọc)**, đã chứng minh live — KHÔNG phải lỗi mới của giai đoạn 6, và
không sửa trong giai đoạn này:

| Luồng | Action | Kết quả live |
|---|---|---|
| Đăng ký khám | `OutpatientManage/OutpatientReg/Save` | error stub: "Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản .NET Core (giai đoạn 1 chỉ xác minh đọc)" |
| Đăng ký khám | `.../OutpatientReg/Book` | OrderSwitch STG ≠ T nên action trả null → lỗi framework "Cannot return null from an action method" — không ghi gì |
| Thu phí ngoại trú | `OutpatientManage/OutpatCharge/AddCfInfo` | **NGUY HIỂM DẠNG IM LẶNG: trả `success`, `data: null`, không ghi gì** (`OutPatChargeSett` stub trả null) — UI sẽ tưởng thu phí thành công |
| Hoàn phí | `OutpatientManage/OutpatientRefund/btnReturn` | error stub (qua `RefundApp.btnReturnInAcc`) |
| Nhập viện | `PatientManage/HospiterRes/InpatientSubmit` | error stub (`OutpatientRegistRepo.SubmitForm` throw) |
| Đặt lịch (web) | `OutpatientManage/OutBook/SaveDataPbhy` | error stub (`OutBookDmnService.SaveDatapb` throw) |
| Ra viện + quyết toán | `HospitalizationManage/Settlement/SubmitSett` | error stub: "Lưu quyết toán nhập viện chưa được chuyển đổi trong giai đoạn này (không ghi DB)" |

### 1.2. Sett-API host (17002, không cần token — ĐẠT toàn bộ trừ biên Pay\*)

Bệnh nhân test mới chỉ trong STG qua Y004/1: patid 100002, blh 00002,
kh "2" (CMND test sinh checksum hợp lệ, chỉ dùng trong STG).

| Luồng | Kết quả live |
|---|---|
| Tạo bệnh nhân Y004/1 | 10000, patid 100002, kh "2" |
| Đặt lịch Y022 (lịch 1597 KS01, ngày 2026-10-27) | 10000 "Đặt hẹn thành công", BookId 3, QueueNo 6, RegFee 3.00 |
| Đặt trùng Y022 (cùng nguồn số) | 40004 "Cùng một nguồn số không được đặt trùng" (negative đúng) |
| Đăng ký từ lịch Y024 (BookId 3, PayFee 3.00) | 10000, mzh `2610275970006`, jsnm 100013 (nhánh có phí tạo mz_js) |
| Đăng ký trực tiếp Y026 (KS01, kh 2) | 10000, mzh `2610275970007`, jsnm 0 (không phí, đúng thiết kế) |
| Y026 thiếu ks + ScheduId | 40004 "Thông tin quan trọng không được để trống" (negative đúng) |
| Đặt lịch Y022 (lịch 1598 KS04) rồi hủy Y023 (BookId 4) | 10000 "Hủy đặt hẹn thành công", yyzt=3, CancelReason `His;13800000001` đúng format gốc |
| Tạo đơn hàng OrderCreate MZ (mzh Y026 + fixture đơn thuốc STG `CF-G6-1` 25.50) | 10000, OrderNo `120261027130000001`, amt 25.50, stu 0; gọi lại trả cùng đơn |
| Khóa đơn LockerApply | 10000, stu 1, LockExpTime quá khứ (quirk gốc giữ nguyên: Now − 5 phút) |
| Thanh toán PayOutpOrder (đủ PayWay/PayFee/VisitNo, đơn đã khóa) | 40004 `sub_code = SETT_ENGINE_NOT_PORTED` — **blocker Pay\* đã biết từ giai đoạn 3, không ghi gì, không phải lỗi mới** |
| PayInHosOrder trên đơn MZ | 40004 "Không phải đơn hàng nội trú" (kiểm tra loại đơn chạy trước engine) |
| Tạo đơn nội trú OrderCreate ZY (zyh 01000) | 40004 "Không tìm thấy hóa đơn chờ thanh toán" — negative đúng (view phí nội trú rỗng, đã biết từ giai đoạn 3) |
| Tra cứu PatientAnon OutpatRegRecord (mzh Y026) | 10000, đúng dòng Y026 |

### 1.3. Chứng minh không ghi demo (COUNT trực tiếp)

| Bảng | Demo trước | Demo sau | STG trước | STG sau |
|---|---|---|---|---|
| mz_gh | 4 | 4 | 9 | 11 |
| mz_js | 12 | 12 | 13 | 14 |
| mz_jsmx | 20 | 20 | 21 | 22 |
| mz_cf | 4 | 4 | 5 | 6 |
| mz_gh_book | 0 | 0 | 2 | 4 |
| order_info | 0 | 0 | 1 | 2 |
| order_mz | 0 | 0 | 1 | 2 |
| xt_brjbxx | 1 | 1 | 2 | 3 |
| xt_card | 1 | 1 | 2 | 3 |

Demo giữ nguyên tuyệt đối; mọi dòng mới chỉ trong STG. Lưu ý: web
Sett-stg đọc một số view Base qua tên 3 phần hardcode
(`[NewtouchHIS_Base]..`) trong `RealServices.cs` — đó là **đọc** từ
demo Base (không ghi), ghi nhận để đợt sau sửa sang STG khi cần cách
ly đọc tuyệt đối; Sett-API host thì đã thay tên DB hardcode bằng DB
cấu hình nên đọc đúng Base_STG.

### 1.4. Sự cố trong lúc làm (đã khắc phục, ghi để đợt sau tránh)

- Lệnh `pkill -f "Newtouch.Sett.Web.dll"` đầu tiên tự khớp cả shell
  của chính nó VÀ giết luôn Sett prod 5221 (cùng tên DLL). Đã chạy
  `recovery.sh` khôi phục 5221 và kiểm chứng login prod 000000 thành
  công trở lại. Bài học: luôn dùng bracket-trick
  (`pkill -f "Newtouch[.]Sett[.]Web[.]dll"`) và giết theo PID cổng
  staging đã ghi, không pkill theo tên DLL chung.

---

## 2. CIS — XONG (web + API; chuỗi qua 8016 xem mục 11)

- Cụm staging: BaseAPI-stg 17001 (PID 27173) + CIS web-stg 15234
  (PID 27238, build worktree) + CIS API-stg 15334 (PID 27969, build
  worktree) + Redis 6380. **CIS web/API-stg đã dừng** sau kiểm chứng
  (chuỗi 8016 chạy cụm riêng ở mục 11, tránh quá tải RAM).
- DB: `Newtouch_CIS_STG` (+ Base/Sett STG cho API host). Login web
  000000 qua BaseAPI-stg ĐẠT; GetToken API thật ĐẠT (xem dưới).

### 2.1. CIS web (15234, cookie login)

| Luồng | Action | Kết quả live |
|---|---|---|
| Y lệnh — dừng y lệnh | `DoctorManage/DoctorsAdvice/StopSubmitForm` (yzId `03707ee6…`, zyh 01000, tzsj 2026-10-27) | success; STG `zy_cqyz` yzzt 4→3 (DC), tzsj + tzr=000000 đã ghi; demo giữ yzzt 4 |
| Y lệnh — xóa y lệnh | `DoctorManage/DoctorsAdvice/DelForm` (yzId `95ff8629…`) | success; STG zt 1→0 (soft delete); demo giữ zt 1 |
| Điều dưỡng — nhập dấu hiệu sinh tồn | `NurseManage/InpatientVitalSigns/SubmitForm` (zyh 01000, rq 2026-10-27, sj 8, tw 36.8, mb 72, hx 18) | success "Lưu thành công", STG `zy_brsmtz` 0→1. **Lưu ý:** lần đầu không gửi `Id` thì FAIL SQL "Cannot insert NULL into column 'Id'" — repo không tự sinh Id, UI phải tự cấp GUID (đã kiểm chứng gửi Id thì đạt). Trùng (zyh,rq,sj) → error "trùng lặpnhập" đúng |
| Đặt hẹn — hủy hẹn (web) | `BespeakRegister/CancelYY` (yyId không tồn tại) | **Trả `success` nhưng không làm gì** — action chỉ `return Success()` không gọi service nào. Dạng no-op im lặng như AddCfInfo của Sett, ghi nhận để đợt sau sửa |
| Truyền dịch | `NurseManage/Infusion/StartInfusion`, `EndInfusion`, `Grouping` | **LỖI MỚI PHÁT HIỆN (không nằm trong danh sách blocker đã biết): `InfusionApp` không có constructor gán repo — `_mzsyzxxxRepo`, `_mzsyypxx`, `_mzsyypxxDmnService` đều null → mọi action qua InfusionApp đều NullReferenceException.** Start/End với tag test 999001 đều NRE, Grouping cũng NRE. Empty tags → thông báo đúng "Vui lòng chọn…". Không sửa trong giai đoạn 6, chuyển cho chương trình build sửa DI wiring |
| Đơn thuốc (web) | Không có action submit trong `ElectronicPrescriptionController` (chỉ form + tra cứu) | Đơn thuốc ghi được kiểm chứng qua CIS API (mục 2.2) thay vì web |

### 2.2. CIS API (15334, token GetToken thật)

- GetToken: `POST api/Auth/GetToken {AppId CIS, Account 000000,
  Password md5, TokenType CIS, Timestamp ISO}` → 10000, token GUID
  (lưu file tạm, không in ra báo cáo). Thiếu Timestamp → 40004
  `TIMESTAMP_IS_REQUIRED` đúng ma trận giai đoạn 2. Lưu ý: Timestamp
  phải dạng ISO (`...T...`), dạng `yyyy-MM-dd HH:mm:ss` bị lỗi
  validation JSON của API này.
- Xác nhận đến khám `api/MzBespeakRegister/KeepAnAppointment` (token
  thật): id không tồn tại → 10000 data 0 (không crash); fixture
  `mz_yygh` STG (`G6TEST-YYGH-001`) → 10000 data 1, arrivalDate +
  arrivalOpereater đã ghi STG, demo `mz_yygh` vẫn 0 dòng.
- Đơn thuốc `api/Prescription/UpdateChargeStatusApi` (không cần token,
  theo thiết kế): cfh `R20241016N000002` sfbz true → 10000; STG
  `xt_cf` sfbz 0→1, demo giữ 0.

### 2.3. Chứng minh không ghi demo

| Bảng (CIS) | Demo trước | Demo sau | STG trước | STG sau |
|---|---|---|---|---|
| zy_cqyz | 4 | 4 | 4 | 4 (2 dòng đổi trạng thái, không đổi số dòng) |
| mz_yygh | 0 | 0 | 0 | 1 (fixture) |
| zy_brsmtz | 0 | 0 | 0 | 1 |
| mz_syzxxx | 0 | 0 | 0 | 0 (Infusion NRE nên không ghi) |
| xt_cf | 4 | 4 | 4 | 4 (1 dòng đổi sfbz) |

Demo giữ nguyên số dòng và giá trị các dòng đã kiểm tra (yzzt/zt/
sfbz của demo không đổi).

---

## 3. PDS — XONG (checkpoint 2026-10-28; web 15237 + BaseAPI-stg 17001 + Redis 6380)

- Cụm staging đã dùng và **sẽ dừng ngay sau checkpoint này**: BaseAPI-stg
  17001 (PID 4832), PDS web-stg 15237 (PID 6639 sau restart; PID 4910
  trước đó), Redis 6380. DB: `NewtouchHIS_PDS_STG` (+ Base/Union STG).
  Login web staging 000000 qua BaseAPI-stg ĐẠT (cả 2 lần chạy).
  Controller PDS bind `DjInfoDTO` từ **form** (POST JSON trả "Chi tiết
  chứng từ không được để trống"); mọi gọi ghi dưới đây đều dùng
  form-encoded `mx[i].*`. `CurrentYfbm` của user 000000 = `00000018`
  (phiếu nhập kho tự set `rkbm = 00000018`).

### Phát hiện cách ly staging quan trọng (đã khắc phục, bài học cho app sau)

- PDS có **hai đường dữ liệu**: đường DmnService/Process (nhập kho) tôn
  trọng env override DBConf → ghi đúng STG, nhưng đường repo tĩnh
  `PdsDb.GetConnection()` (SqlSugar scope tĩnh, dùng cho duyệt phiếu +
  tra cứu chi tiết) **lúc đầu vẫn đọc DB demo** dù process đã có env
  `BusinessDB__DBConf__2__DBName=NewtouchHIS_PDS_STG`: bằng chứng là
  `QueryOutOrInStorageBillDetail` trả `[]` cho phiếu chỉ có trong STG
  nhưng trả đủ dòng cho phiếu demo, và `SubmitReceiptApproval` báo
  "không tìm thấy chứng từ" cho phiếu STG.
- Cách khắc phục (staging-only, không đụng main repo): tạo
  `appsettings.Staging.json` trong **worktree**
  `~/workspace/openhis-stg/Newtouch.HIS.PDS/Newtouch.PDS.Web/`
  (copy từ appsettings.json, chỉ đổi DBConf → `*_STG`, host → staging,
  Redis → 6380, `SitePDSAPIHost` → `http://127.0.0.1:15337/` fail-closed
  để không proxy sang PDS API prod 5337), đổi launcher
  `run-pds-stg.sh` sang `ASPNETCORE_ENVIRONMENT=Staging`, restart
  PDS-stg. Sau đó tra cứu STG trả đủ dòng → cách ly đọc đã đúng.
  **Bài học: với app dùng SqlSugar scope tĩnh, phải probe đọc STG-only
  ngay sau khi dựng staging, trước khi chạy bất kỳ luồng ghi nào qua
  đường repo; env override một mình là chưa đủ bằng chứng.**
- Lưu ý an toàn: lần duyệt đầu tiên "fail" không phải lỗi app mà do
  người kiểm chứng chép tay thiếu ký tự cuối crkId (…c1e thay vì
  …c1e4). Từ đó mọi ID đều lấy bằng command substitution, không chép
  tay (cùng bài học md5 đã ghi trước đây).
- Quirk log (không chặn, ghi để đợt sau xử lý): SessionMiddleware báo
  `ArgumentNullException (configuration)` khi commit session vì
  `AddStackExchangeRedisCache` đọc `GetConnectionString(
  "RedisConfig:Connection")` (sai section, trả null); request vẫn thành
  công, chỉ log lỗi.

| Luồng ghi | Kết quả live (STG) |
|---|---|
| Nhập kho `OutOrInStoredManage/InStorage/SubmitInStorage` (4 phiếu G6-RK-001..004, ypdm `01002121`, ph `2304011100`) | success cả 4; `xt_yp_crkdj` shzt 0 (chờ duyệt), `xt_yp_crkmx` đủ chi tiết; rkbm tự set `00000018` |
| Duyệt phiếu `BillManage/OutOrInStorageBill/SubmitReceiptApproval` (nhập kho djlx 1 + xuất trực tiếp djlx 3) | success; shzt 0→1, Shczy `000000`; duyệt phiếu có `rkzhyz=1, sl=7` tạo dòng `xt_yp_kcxx` mới kcsl = 7 (nhập kho → tồn kho đã chứng minh bằng số) |
| Yêu cầu lĩnh thuốc `OutOrInStoredManage/Apply/SubmitApply` (G6-SL-001, ckbm `00000021`) | success; `xt_yp_sld` (Slbm `00000018`, Ckbm `00000021`, ffzt 0) + `xt_yp_sldmx` (ypCode `01002121`, slsl 5, yfsl 0) |
| Phát thuốc cho khoa `OutOrInStoredManage/DeliveryToDepartment/SubmitDeliveryToDepartment` (G6-PK-001, 2 đơn vị) | success (lưu ý: thiếu `fyfs` → lỗi NULL `Crkfsdm`; thiếu `pc` → lỗi NULL `pc` ở chi tiết — UI phải gửi đủ 2 trường này; Delivery không tự sinh `pc` như InStorage); phiếu djlx 6, shzt 1 (tự duyệt), tồn kho `00000018` 1000→998 |
| Xuất kho trực tiếp `OutOrInStoredManage/DeliveryDirect/SubmitDeliveryDirect` (G6-TT-001, 1 đơn vị) + duyệt | success; phiếu djlx 3, shzt 0 lúc tạo (chờ duyệt, khác phát khoa tự duyệt); sau duyệt shzt→1, tồn kho 998→997 |
| Kết chuyển tồn kho `DrugStorage/CarryOverMedicine` (POST, không tham số) | success 200; `xt_yp_kcjzk` STG 10618→11079 (+461 dòng snapshot ngày); `sys_yfkc_day`/`sys_ykkc_day` vẫn 0 (đường kết chuyển khác, không ghi 2 bảng này) |
| Chuyển kho `OutOrInStoredManage/Transfer/SubmitApply` | **KHÔNG CÓ trong bản build**: route `/OutOrInStoredManage/Transfer/*` trả 404 (cả Index); `Newtouch.PDS.Web.csproj` dòng 72 `<Compile Remove="Areas/OutOrInStoredManage/Controllers/TransferController.cs" />` — controller bị loại khỏi biên dịch ở giai đoạn port (cùng nhóm với 8 controller khác bị Remove). PDS API cũng không có controller chuyển kho. Đây là **gap porting cần chương trình build xử lý** (đưa TransferController trở lại build + sửa lỗi biên dịch gốc), không phải lỗi mới của giai đoạn 6; các bước chuyển kho còn lại (Warehousing, ExecHandOutMedicineByReq, RequestMedicineInfo) nằm cùng file bị loại nên cũng không gọi được qua web |
| Ghi chú liên kết | Phát thuốc độc lập không tự cập nhật đơn lĩnh: sau G6-PK-001, đơn G6-SL-001 vẫn ffzt 0 (delivery không mang sldId/sldmxId nên không trừ yfsl); UI muốn trừ đơn lĩnh phải gửi tham chiếu đơn lĩnh — ngoài phạm vi kiểm chứng độc lập này |

- Chứng minh không ghi demo (trước = sau, đếm trực tiếp): `xt_yp_dj`
  50, `xt_yp_crkdj` 106, `xt_yp_crkmx` 1127, `xt_yp_sld` 0,
  `xt_yp_sldmx` 0, `xt_yp_kcxx` 1114, `xt_yp_kcjzk` 10618, `t_ypkc` 0.
  STG sau: `xt_yp_crkdj` 112, `xt_yp_crkmx` 1133, `xt_yp_sld` 1,
  `xt_yp_sldmx` 1, `xt_yp_kcxx` 1117, `xt_yp_kcjzk` 11079.
  (Demo `xt_yp_kcjzk` = 10618 đúng bằng STG-trước-clone, khẳng định
  kết chuyển chỉ ghi STG.)

---

## 4. Herp — (chưa làm)

---

## 5. EMR — (chưa làm)

---

## 6. MRQC — (chưa làm)

---

## 7. MRMS — (chưa làm)

---

## 8. OR — (chưa làm)

---

## 9. Base/HOSP + SSO — (chưa làm)

---

## 10. Center — (chưa làm)

---

## 11. Liên app — XONG (chuỗi CIS→8016→Sett-API; SSO liên app xem mục 9)

- Cụm staging: Sett-API-stg 17002 (PID 3438) + AuthCenter-stg 18015
  (PID 3495) + WebAPI.Manage-stg 18016 (PID 3582) + Redis 6380.
  **Tất cả đã dừng** sau kiểm chứng. Prod 5221/5234/7001/7002/8015/
  8016 vẫn UP sau đó.
- DB: mọi host staging đều trỏ `*_STG` (AuthCenter DBConf Base/Sett/
  Emr/Mrms; Manage DBConf Base/Sett/Emr/Mrms/MrQc/Cis/Interface/Pds).

| Kiểm chứng | Kết quả live |
|---|---|
| Cấp JWT app-to-app từ AuthCenter-stg (`POST api/Auth/GetAppToken {AppId CIS, Data API.Manage}`) | 10000, JWT CIS→API.Manage đã cấp (token chỉ lưu file tạm, không in ra báo cáo; lưu ý center log vẫn ghi full JWT như đã biết) |
| Gọi 8016-stg `api/Outpatient/OutpatientRegist` (Bearer JWT staging) không kèm OrganizeId | 20000 "Id ứng dụng (AppId) bắt buộc phải truyền" ở lần gọi thiếu AppId; sau khi thêm AppId thì 8016 proxy tới Sett-API-stg và Sett-API trả 40004 "Tham số quan trọng không được để trống" — nguyên nhân: Dispatch của Sett-API bắt buộc `OrganizeId` (JWT staging không mang org) |
| Gọi lại kèm OrganizeId demo org | **10000 "Đăng ký khám thành công", mzh `2610275970008`, jsnm 0** — chuỗi CIS→8016→BookingESB Y026 thông hoàn toàn trên staging |
| Chứng minh không ghi demo | Sett_STG `mz_gh` 11→12 (dòng mới `2610275970008`, kh 2, KS01); demo Sett `mz_gh` giữ 4 |
| Caller phía CIS web (`ClinicManage/ClinicApply/Accept`, CIS-stg 15234) | Negative đúng: error "Không tìm thấy thông tin xếp lịch" — Accept dừng ở cổng đầu (cần lịch clinic czks=00000080, regFee=0, ngày hiện tại). Positive toàn phần của Accept cần thêm fixture lịch clinic + hồ sơ hội chẩn + meeting API, ngoài phạm vi kiểm chứng lại chuỗi Y026; bản thân bước nhảy 8016→Sett-API đã đạt ở trên. Lưu ý: `getClinicScheduleId` đọc hardcode `[NewtouchHIS_Sett]` (demo) — đọc rò rỉ sang demo, không ghi |

### Sự cố VM restart giữa giai đoạn (đã khắc phục)

- Giữa lúc dựng cụm chain, VM restart (uptime reset về 1 phút): toàn
  bộ stack prod down, /tmp (gồm log + cookie/token tạm) bị xóa. Đã
  chạy `recovery.sh` — lần 1 chỉ lên một phần do tiến trình recovery
  vẫn đang chạy nền, lần 2 hoàn tất: 25/25 cổng prod UP trở lại,
  login SSO 000000 đạt. DB STG không ảnh hưởng (đếm lại Sett_STG
  mz_gh = 11, CIS_STG zy_cqyz = 4, đúng số liệu trước restart).
  Redis staging 6380 khởi động lại từ config/data dưới
  `~/workspace/redis-stg` (không mất).

---

## App còn lại

Herp, EMR, MRQC, MRMS, OR, Base/HOSP + SSO, Center
(Sett, CIS, PDS, Liên app đã xong; mục 9 SSO liên app còn lại phần điều
hướng portal→module, làm cùng app 9).
