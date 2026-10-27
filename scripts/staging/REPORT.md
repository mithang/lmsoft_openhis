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

## 4. Herp — XONG (checkpoint 2026-10-27 tối; web 15236 + BaseAPI-stg 17001 + Redis 6380)

- Cụm staging đã dùng và **đã dừng**: BaseAPI-stg 17001 (PID 8677),
  Herp web-stg 15236 (PID 8848, build worktree Release 0 lỗi), Redis
  6380 (PID 8667, giữ lại cho app sau — chỉ là Redis staging, không
  phải app). Login web staging 000000 qua BaseAPI-stg ĐẠT.
  Launcher `run-herp-stg.sh` đã có sẵn, chỉ chmod +x lại trước khi dùng.
- Probe cách ly (trước mọi luồng ghi): fixture `G6-PROBE-001` chỉ chèn
  vào `xt_wz_cg` của STG → `GetPurchaseGridJson` của app staging trả
  đúng dòng đó → app đọc đúng STG (không cần `appsettings.Staging.json`
  như PDS; Herp dùng DmnService/Process tôn trọng env DBConf).

| Luồng ghi | Kết quả live (STG) |
|---|---|
| Mua hàng `PurchaseManage/Purchase/SubmitPurchase` (đủ trường NOT NULL) | success; `xt_wz_cg` dòng mới cgId `c99d09d9…`, ddzt=1, yyjhdh `CG20261027134020054`; `xt_wz_cgmx` 1 dòng productCode G6PROD, sl 10, dj 5.5. **Quirk:** thiếu `cgEntity.zt` → lỗi NULL `zt` ở header; thiếu `sxh/cglx` ở chi tiết → lỗi NULL tương ứng — và mỗi lần lỗi chi tiết vẫn để lại header mồ côi (không transaction: 2 header G6YY/G6YY2 không có chi tiết) |
| Phê duyệt/chuyển trạng thái mua hàng `PurchaseStateUpdate` (cgId trên, ddzt 1→2) | success; STG ddzt=2 |
| Nhập kho `StorageManage/Storage/SaveInStorageInfo` (djlx 1, Pdh G6RK001) | **LỖI MỚI — BUG PORTING: `Cannot insert explicit value for identity column in table 'kf_crkdj'`**. `KfCrkdjEntity.Id` là long identity nhưng `[SugarColumn(IsPrimaryKey=true)]` không có IsIdentity → SqlSugar insert cả Id=0. Không ghi gì |
| Xuất kho ngoài `SaveOutStorageInfo` (djlx 2, Pdh G6CK001) | **Cùng lỗi identity `kf_crkdj`** |
| Giao hàng cho khoa `SaveDeliveryToDepartment` (Pdh G6CKZKS001) | error chung "Xử lý nội bộ thất bại, vui lòng liên hệ quản trị viên" (Process nuốt lỗi gốc — không xác định được từ response có phải cùng lỗi identity không; chuyển cho build xác minh cùng nhóm bug) |
| Lĩnh thuốc khoa `SubmitDepartmentApply` (sldh G6SL001) | **Cùng lỗi identity, bảng `kf_applyOrder`** |
| Duyệt phiếu `BillManage/OutOrInStorageBill/SubmitReceiptApproval` | success trên fixture SQL chỉ có trong STG (Id=1, djlx 1, Pdh G6RK-FIX): auditState 0→1, shczy=000000 → **đường duyệt bản thân đạt**, chỉ bị chặn ở khâu tạo phiếu. Negative: djId không tồn tại → error "Không tìm thấy phiếu" đúng |
| Giao hàng nhà cung cấp `PurchaseManage/Purchase/DeliveryAccept` | error "Nghiệm thu phiếu giao hàng thất bại: Permission denied (192.168.20.110:80)" — gọi host Sun bên ngoài không tới được (phụ thuộc ngoài, không phải lỗi port; giống nhóm SunUnavailable ở các app khác) |

- Chứng minh không ghi demo (trước = sau, đếm trực tiếp): demo Herp
  mọi bảng ghi đều 0 trước và sau (`xt_wz_cg`, `xt_wz_cgmx`,
  `kf_crkdj`, `kf_crkmx`, `kf_applyOrder`, `kf_applyOrderDetail`,
  `gys_delivery`, `gys_deliveryDetail`). STG sau: `xt_wz_cg` 4
  (1 probe + 3 header mua hàng), `xt_wz_cgmx` 1, `kf_crkdj` 1
  (fixture duyệt), còn lại 0.
- Dừng cụm: Herp-stg 15236 + BaseAPI-stg 17001 đã kill theo PID chính
  xác, cổng đã down; prod 5236/7001 vẫn UP sau đó.

---

## 5. EMR — XONG (checkpoint 2026-10-27 tối; web 15235 Release từ worktree + BaseAPI-stg 17001 + Redis 6380)

- Cụm staging đã dùng và **đã dừng**: BaseAPI-stg 17001 (PID 9737),
  EMR web-stg 15235 (PID 9879, build worktree **Release** 0 lỗi —
  đúng yêu cầu giai đoạn 4, prod 5235 cũng đang chạy Release),
  Redis 6380. Login web staging 000000 qua BaseAPI-stg ĐẠT.
  Launcher staging mới `run-emr-stg.sh` (DBConf EMR web: 0 Base, 1 Sett,
  2 Emr, 3 Mrms, 4 MrQc, 5 Cis, 6 Union → toàn bộ `*_STG`).
- Probe cách ly (trước mọi luồng ghi): fixture `G6-PROBE-MB-001`
  (mẫu "Mau kiem chung G6") chỉ chèn vào `bl_mblb` của STG →
  `MedRecordTemplate/GetGridJson?keyword=G6MB` của app staging trả
  đúng dòng đó → app đọc đúng STG, không cần `appsettings.Staging.json`.
  Lưu ý đọc rò rỉ (không chặn): `CommonDmnService.GetBllxListDetail`
  đọc hardcode `[NewtouchHIS_Base]..V_S_Sys_Duty` (demo Base) — cùng
  lớp với Sett/CIS đã ghi.

| Luồng ghi | Kết quả live (STG) |
|---|---|
| Tạo bệnh án `MedicalRecord/SavePatientBl` (mẫu probe, zyh 01000, submitData base64 HTML, KHÔNG docData) | success; BLID `7b108692…`, BLLX 1026, file HTML đã ghi dưới worktree `File/住院病历/2026/10/27/01000/`; `zy_meddocs_relation` STG tăng thêm 1 |
| Tạo bệnh án `SavePatientBl` CÓ docData (keyCode G6KEY) | **LỖI MỚI — cùng mẫu thiếu Id như CIS vital signs:** `BL_Save` chạy xong (file + relation đã tạo) rồi `BLJG_Save` fail "Cannot insert NULL into column 'Id', table 'bl_ysjgnr'" — repo không tự sinh Id cho dòng yếu tố bệnh án. Không transaction nên lần gọi này vẫn để lại relation/file mồ côi |
| Sửa bệnh án `UpdatePatientBl` | Không kiểm chứng riêng: cùng đường BLJG/BL_Save với SavePatientBl, bị chặn bởi cùng lỗi NULL Id khi có docData |
| Mẫu bệnh án `MedRecordTemplate/submitForm` (tạo mới, thử cả bllx="1026" và bllx=Id "Van ban khac") | **LỖI MỚI — NRE tại dòng 162 (`blmb.bllxId = bllxDic.Id`)**: `GetBllxListDetail` trả rỗng nên bllxDic null, controller không null-check. Tạo/sửa mẫu qua web đều chặn. Đọc mẫu (`GetGridJson`, `GetFormJson` theo keyValue) vẫn đạt |
| Xóa mẫu `DeleteForm` (keyValue không tồn tại) | NRE tại controller (không null-check entity trước khi xóa) — negative cũng crash thay vì báo "không tìm thấy" |
| Ký/khóa bệnh án `MedicalRecord/LockRecord` (blid mới tạo, bllx 1026, isToLock=1; và blid không tồn tại) | **LỖI MỚI — NRE trong `MedicalRecordDmnService.LockRecord`** cả hai trường hợp → đường khóa/ký bệnh án chặn hoàn toàn |
| Khóa `UpdateLockzt` (blid mới tạo) | success (lưu ý: code trả Success vô điều kiện kể cả khi update = 0, nên "success" này yếu — chỉ ghi nhận response) |
| Nộp bệnh án `PatInfo/CommitPatMedRecord` (zyh 01000) | success "Bệnh án đã nộp" |
| Sửa liên kết bệnh án `UpdatePatMedRecordRel` (blId mới tạo) | **LỖI MỚI — NRE trong `ZybrjbxxDmnService.UpdtPatMedRecord`** |
| Đổi trạng thái kế hoạch `UpdatePlanStu` (blId mới tạo, stu rỗng) | success "Thao tác thành công"; STG PlanStu = 1 (đã đổi) |

- Chứng minh không ghi demo: demo `bl_mblb` 52, `bl_ysjgnr` 22,
  `zy_meddocs_relation` 2, `bl_patrecords` 0 — giữ nguyên trước/sau.
  STG sau: `bl_mblb` 53 (chỉ probe), `bl_ysjgnr` 22,
  `zy_meddocs_relation` 4 (2 lần SavePatientBl), `bl_patrecords` 0.
- Dừng cụm: EMR-stg 15235 + BaseAPI-stg 17001 đã kill theo PID chính
  xác, cổng đã down; prod 5235/7001 vẫn UP sau đó.

---

## 6. MRQC — XONG (checkpoint 2026-10-27 tối; web 15220 + BaseAPI-stg 17001 + AuthCenter-stg 18015 + WebAPI.Manage-stg 18016 + Redis 6380)

- Cụm staging đã dùng và **đã dừng**: BaseAPI-stg 17001 (PID 10575),
  MRQC web-stg 15220 (PID 10742, build worktree Release 0 lỗi),
  AuthCenter-stg 18015 (PID 10931), WebAPI.Manage-stg 18016
  (PID 10932), Redis 6380. Login web staging 000000 qua BaseAPI-stg
  ĐẠT. Launcher staging mới `run-mrqc-stg.sh` (DBConf MRQC web:
  0 Base, 1 Sett, 2 Emr, 3 Mrms, 4 MrQc, 5 Cis, 6 Union → `*_STG`).
- Probe cách ly (trước mọi luồng ghi): fixture `G6-PROBE-APPLY` chỉ
  chèn vào `Mr_Apply` của STG → `GetBlApplyList` của app staging trả
  đúng dòng đó (cùng 2 dòng PILOT-DEMO clone) → đường DmnService đọc
  đúng STG. Lưu ý: MRQC có `MrqcDb` tĩnh (`SqlSugarDbContext.Db.
  GetConnection("MrQcDb")`) cùng lớp rủi ro với PDS, nhưng probe này
  chứng minh với cấu hình hiện tại nó đọc đúng STG — không cần
  `appsettings.Staging.json`.

| Luồng ghi | Kết quả live (STG) |
|---|---|
| Phê duyệt Mr_Apply `QualityControlManage/Apply/ApprovalApply` (chỉ có Mr_Apply STG, chưa có bản ghi EMR) | error đúng "Phê duyệt thất bại, vui lòng làm mới danh sách chờ phê duyệt" — center-stg trả `data:false` vì không tìm thấy dòng tương ứng. Không ghi gì, không silent-success |
| Phê duyệt Mr_Apply (đủ cả hai phía: thêm fixture `Mr_BlApplyRecord` vào EMR STG với applyReturnNo = Id Mr_Apply) | **success "Phê duyệt thành công" đầu-cuối:** center-stg cập nhật EMR STG `Mr_BlApplyRecord` applyStatus 0→1, ApprovePerson 000000/KS01, rồi MRQC-stg cập nhật `Mr_Apply` ApplyStatus 0→1, Approver 000000. **Phát hiện thiết kế:** `ModifyApplyApprove` của center update bảng `Mr_BlApplyRecord` ở DB EMR theo `applyReturnNo`, không update `Mr_Apply` ở DB MRQC — nên đơn MRQC không có bản ghi EMR đối ứng thì không bao giờ duyệt được qua web (chuỗi nộp đơn thật từ EMR phải tạo cả hai bản ghi; ngoài phạm vi giai đoạn 6) |
| Chấm điểm MedicalCenter `MedicalCenter/SaveScoreDate` (zyh 01000, bllx 1026, 1 item 90 điểm) | success; `Mr_Qc_Score` STG 0→1 (zyh 01000, score 90.00, CreatorCode 000000) |
| Gửi tin nhắn QC `MedicalCenter/SendMsg` (zyh 01000) | **LỖI MỚI — chặn ở bước lấy token app-to-app:** "[Xác thực] Lỗi khi lấy mã định danh Api: Unexpected character encountered while parsing value … Path 'BusData.data'" — `AuthenManageHelper` không lấy được token từ AuthCenter-stg (dù AuthCenter-stg đang chạy và chuỗi phê duyệt qua 18016 vẫn chạy). Chuyển cho build xác minh cơ chế token của MRQC |
| Đọc MedicalCenter `GetPatList` (zyh 01000), `GetMedicalCenterDiag`, `GetMedicalCenterBlTree` | đạt (bệnh nhân A Binh, chẩn đoán "Cho xac dinh", cây loại bệnh án) — lỗi NRE `GetPatList` đã sửa từ trước không tái hiện |

- Chứng minh không ghi demo: demo `Mr_Apply` 3, `Mr_Qc_Score` 0,
  EMR demo `Mr_BlApplyRecord` 0 — giữ nguyên trước/sau. STG sau:
  `Mr_Apply` 4 (chỉ probe đã duyệt), `Mr_Qc_Score` 1,
  `Mr_BlApplyRecord` STG 1 (fixture đã duyệt).
- Dừng cụm: toàn bộ PID staging đã kill theo PID chính xác, các cổng
  15220/18015/18016/17001 đã down; prod 5220/8015/8016/7001 vẫn UP.

---

## 7. MRMS — XONG (checkpoint 2026-10-27 tối; web 15232 + BaseAPI-stg 17001 + Redis 6380)

- Cụm staging đã dùng và **đã dừng**: BaseAPI-stg 17001 (PID 11427),
  MRMS web-stg 15232 (PID 11595, build worktree Release 0 lỗi),
  Redis 6380. Login web staging 000000 qua BaseAPI-stg ĐẠT.
  Launcher staging mới `run-mrms-stg.sh` (DBConf MRMS web: 0 Base,
  1 Sett, 2 Emr, 3 Mrms, 4 MrQc, 5 Cis, 6 Union → `*_STG`).
- Probe cách ly (trước mọi luồng ghi): fixture `G6-PROBE-BASY`
  (mr_basy BAH G6BAH, zyh 01000) chỉ chèn vào MRMS STG →
  `MainRecord/GetFormJson?keyValue=G6-PROBE-BASY` của app staging
  trả đúng dòng đó → đường DmnService đọc đúng STG.
- **Không có controller mượn/trả hồ sơ trong bản port MRMS web:**
  grep toàn bộ Areas + API không thấy action Borrow/Return nào;
  bảng `brly` trong DB MRMS chỉ có 2 cột (F1 float, F2 nvarchar,
  42950 dòng) và không được controller nào dùng. Luồng mượn/trả
  theo thiết kế §3.4 vì vậy **không kiểm chứng được qua web port** —
  gap porting, chuyển cho chương trình build (không báo là đạt).

| Luồng ghi | Kết quả live (STG) |
|---|---|
| Tạo trang đầu bệnh án `MainRecord/SubmitPatBasic` (mới, thiếu PATID rồi đủ trường) | Lỗi kiểm soát đúng theo từng bước: thiếu trường → liệt kê đủ lỗi ("Đường nhập viện…", "Số lần nội trú…", …); đủ trường nhưng zyh 01000 đã có fixture probe → error "Bệnh nhân đã có thông tin bệnh án, vui lòng không gửi trùng lặp" (không ghi thêm). Tạo mới cho bệnh nhân chưa có hồ sơ nền không kiểm chứng được vì nguồn `V_EMR_MrBasy` chỉ có đúng 1 bệnh nhân (01000) trong STG — cần dữ liệu nền thật, không bịa |
| Sửa trang đầu `SubmitPatBasic` (keyValue = fixture probe, đổi XM) | **LỖI MỚI — ghi một phần dù trả lỗi:** response NRE tại `PatBasyInfoRelSet` (dòng rel không tồn tại), nhưng dòng `mr_basy` chính ĐÃ đổi XM thành "Benh nhan G6 sua" trước khi lỗi — UI sẽ báo lỗi trong khi dữ liệu đã đổi |
| Lưu chẩn đoán `SubmitZdList` (Code sai "G6") | IndexOutOfRange tại `dtcsxx[0]` — `DiagnosticSave` đọc hardcode `[Newtouch_MRMS].dbo.Sys_Config` (xem ghi chú đọc rò rỉ dưới) với Code không tồn tại → list rỗng, controller không kiểm tra |
| Lưu chẩn đoán `SubmitZdList` (Code đúng "DiagnosticSave" = ON) | Trả `{"data":"Không thể thêm thông tin chẩn đoán"}` (cổng kiểm tra quyết toán chặn), không ghi gì — thông điệp kiểm soát, không crash |
| Lưu phẫu thuật `SubmitOpList` (BAH G6BAH, zyh 01000, SS G6SS) | success "Lưu phẫu thuật thành công"; `mr_basy_ss` STG 0→1 |
| Lưu trữ hồ sơ `guidang` / hủy lưu trữ `CXguidang` (fixture probe) | Trả 0 cả hai (không đổi `bazt`) — **nhưng xem sự cố rò rỉ ghi demo dưới đây: nguyên nhân là 2 action này hardcode tên DB demo** |
| Đổi trạng thái hồ sơ `Record/updateRecordStu` (Id = fixture probe) | **LỖI MỚI — SQL injection bề mặt + crash:** Id dạng chuỗi được nối trực tiếp không có dấu nháy vào câu SQL → SQL Server báo "Invalid column name 'G6'/'PROBE'/'BASY'". Với Id số thì câu lệnh chạy được nhưng vẫn là nối chuỗi không tham số hóa |
| Đọc/truy vấn Main* (`GetFormJson`, `GetGridList` qua các form Main) | đạt (fixture probe đọc được; form Main* hiển thị qua view, không lỗi 404) |

### Sự cố rò rỉ GHI sang demo trong lúc kiểm chứng MRMS (đã khắc phục + khôi phục)

- `MrbasyRepo.Updatebazt` (guidang) hardcode
  `update [Newtouch_MRMS]..mr_basy` + `update [Newtouch_EMR]..mr_basy`;
  `CXUpdatebazt` hardcode `update [Newtouch_EMR]..zy_brjbxx set
  RecordStu=2 … where 1=1 and zt='1'` (**không lọc theo zyh**).
  Trên staging, các câu này vẫn trỏ về DB demo bất chấp env DBConf.
- Hậu quả thực tế: lần gọi `CXguidang` đã **ghi thật vào demo**:
  demo EMR `zy_brjbxx` zyh 01000 đổi RecordStu 1→2,
  LastModifyTime thành thời điểm gọi (2026-10-27 13:49:59).
  Phát hiện bằng đối chiếu STG (RecordStu vẫn 1, thời gian gốc
  2026-10-26 15:39:09) với demo sau gọi.
- Khắc phục ngay: đã UPDATE khôi phục dòng demo về đúng giá trị
  gốc (RecordStu=1, LastModifyTime 2026-10-26 15:39:09.017,
  LastModifierCode giữ nguyên) và kiểm chứng lại bằng SELECT.
  Các bảng demo khác không đổi (`mr_basy` demo vẫn 0 — guidang
  không khớp dòng demo nào nên không đổi gì thêm).
- **Kết luận cho build:** guidang/CXguidang là luồng ghi rò rỉ demo
  đã chứng minh, không chỉ đọc rò rỉ — phải sửa sang tên DB cấu
  hình trước khi cho chạy thật, và CXUpdatebazt phải thêm điều kiện
  zyh (hiện tại 1 lần gọi đổi toàn bộ hồ sơ zt=1 trong DB đích).

- Chứng minh không ghi demo (sau khôi phục, đếm trực tiếp):
  demo `mr_basy` 0, `mr_basy_zd` 0, `mr_basy_ss` 0 — giữ nguyên.
  STG sau: `mr_basy` 1 (chỉ fixture probe, XM đã sửa),
  `mr_basy_zd` 0, `mr_basy_ss` 1.
- Dừng cụm: MRMS-stg 15232 + BaseAPI-stg 17001 đã kill theo PID
  chính xác, cổng đã down; prod 5232/7001 vẫn UP sau đó.

---

## 8. OR — XONG (checkpoint 2026-10-27 tối; web 15231 + BaseAPI-stg 17001 + Redis 6380)

- Cụm staging đã dùng và **đã dừng**: BaseAPI-stg 17001 (PID 12130),
  OR web-stg 15231 (PID 12319, build worktree Release 0 lỗi),
  Redis 6380. Login web staging 000000 qua BaseAPI-stg ĐẠT.
  Launcher staging mới `run-or-stg.sh` (DBConf OR web có 8 mục:
  0 Base, 1 Sett, 2 Emr, 3 Mrms, 4 MrQc, 5 Cis, 6 Union, 7 Or →
  toàn bộ `*_STG`).
- Probe cách ly (trước mọi luồng ghi): fixture `G6APPLY001`
  (OR_ApplyInfo, zyh 01000) chỉ chèn vào OR STG →
  `OpApply/GetFormJson?keyValue=G6APPLY001` của app staging trả
  đúng dòng đó → app đọc đúng STG.

| Luồng ghi | Kết quả live (STG) |
|---|---|
| Đăng ký mổ `Operation/OpApply/SubmitForm` (thiếu zyh; thiếu bác sĩ mổ chính) | Lỗi kiểm soát đúng: "Thông tin bệnh nhân không được để trống", rồi "Bác sĩ mổ chính không được để trống!" — không ghi gì |
| Đăng ký mổ `SubmitForm` (đủ: zyh 01000, ysgh 000000, ssmcn Phau thuat G6) | success "Gửi đơn thành công", applyno `S20261027A000001`, sqzt=1; `OR_ApplyInfo` STG tăng 1 |
| Xếp lịch mổ `Operation/OpArrangement/submitForm` (keyValue = applyno) | **LỖI MỚI — NRE tại `ORApplyInfoRepo.UpdateSqzt` dòng 95:** controller truyền applyno nhưng `UpdateSqzt` dùng `FindEntity(keyValue)` theo khóa chính Id → null → NRE. UI phải truyền Id đơn, không phải số đơn |
| Xếp lịch mổ `submitForm` (keyValue = Id đơn `8d5cf668…`) | success "Thao tác thành công"; `OR_Arrangement` STG 0→1 (ApplyId = Id đơn, Applyno đúng), đơn chuyển sqzt 1→2 |
| Hủy xếp lịch `OpArrangement/DeleteForm` (keyValue = Id đơn) | success; đơn về sqzt 2→1 (dòng Arrangement vẫn còn — DeleteForm của repo chỉ đổi trạng thái? xem ghi chú: số dòng Arrangement vẫn 1 sau hủy, tức hủy lịch không xóa dòng lịch mà chỉ trả trạng thái đơn) |
| Danh sách mổ `OpApply/GetGridListOP` (zyh 01000) | đạt: trả đúng đơn vừa tạo (kèm thông tin bệnh nhân A Binh từ nguồn Sett/EMR) |
| Hủy đơn mổ `OpApply/DeleteApply` (applyno) | success "Hủy đơn thành công"; sqzt → 3 (đã hủy), zt giữ 1 — hủy bằng trạng thái, đúng thiết kế, không xóa dòng |

- Chứng minh không ghi demo: demo `OR_ApplyInfo` 0,
  `OR_Arrangement` 0, `OR_Operation` 17599 — giữ nguyên trước/sau.
  STG sau: `OR_ApplyInfo` 2 (1 probe + 1 đơn đã hủy),
  `OR_Arrangement` 1, `OR_Operation` 17599.
- Dừng cụm: OR-stg 15231 + BaseAPI-stg 17001 đã kill theo PID chính
  xác, cổng đã down; prod 5231/7001 vẫn UP sau đó.

---

## 9. Base/HOSP + SSO — XONG (checkpoint 2026-10-27 tối; HOSP 15233 + SSO portal 15208 + BaseAPI-stg 17001 + Redis 6380)

- Cụm staging đã dùng và **đã dừng**: BaseAPI-stg 17001 (PID 12646),
  HOSP web-stg 15233 (PID 13049, build worktree Release 0 lỗi),
  SSO portal-stg 15208 (PID 13050, build worktree Release 0 lỗi —
  SSO không hardcode cổng nên dùng ASPNETCORE_URLS), Redis 6380.
  Login staging 000000 qua BaseAPI-stg ĐẠT ở cả HOSP và SSO portal.
  Launcher staging mới `run-hosp-stg.sh`, `run-sso-stg.sh`
  (DBConf → `*_STG`, BaseAPI 17001, Redis 6380). Lưu ý cổng worktree:
  Base.Web → 15238 (prod 5238), HOSP.Web → 15233 (prod 5233).
- Probe cách ly: fixture `G6-PROBE-ORG` (Sys_Organize, Code G6KHOA)
  chỉ chèn vào Base STG → `Sys/Organize/GetGridJson?keyword=G6KHOA`
  của HOSP-stg trả đúng dòng đó (cùng dòng khoa mới tạo) → đường
  đọc đúng STG.

| Luồng ghi | Kết quả live (STG) |
|---|---|
| Tạo khoa/phòng `Sys/Organize/SubmitForm` (Name, Code G6KHOA2, CategoryCode 1) | success; `Sys_Organize` STG tăng 1, Id `9bb5aeda…`, TopOrganizeId tự set bằng chính Id mới. **Lưu ý:** code có nhánh tự tạo tài khoản quản trị mặc định cho tổ chức cấp cao nhất (`CreateDefaultAdminToOrg`) nhưng exception bị nuốt lặng (catch rỗng) — thực tế `Sys_User` STG vẫn 3, không có admin mới nào được tạo, và response cũng không báo nhánh này |
| Sửa khoa/phòng `SubmitForm` (keyValue = Id khoa mới) | **LỖI MỚI (2 dạng):** không gửi TopOrganizeId → error "Dữ liệu bất thường, chưa chỉ định cơ sở cấp cao nhất"; gửi TopOrganizeId → lỗi SqlSugar "The parameter format is wrong" tại `SysOrganizeRepo.SubmitForm` dòng 154 (đường update của repo hỏng). Tạo thì đạt, sửa thì chặn |
| Đọc khoa `GetFormJson` (keyValue = fixture probe) | **LỖI MỚI — cùng lỗi SqlSugar "parameter format"** tại `RepositoryBase.FindEntity` — đọc chi tiết 1 khoa cũng hỏng (trong khi đọc danh sách vẫn đạt) |
| Tạo vai trò `SysManage/SysRole/SubmitForm` qua SSO portal (thử Name riêng, Name+EnCode, Name+Code+zt) | **LỖI MỚI — chặn ở validation model, không phụ thuộc input:** "Unable to cast object of type 'System.DateTime' to type 'System.String'" tại `StringLengthAttribute.IsValid` trong quá trình model binding — một thuộc tính DateTime của entity (hoặc base class) đang mang attribute StringLength. Tạo vai trò qua web chặn hoàn toàn |
| Tạo tham số `Sys/SysGlobalConfig/SubmitForm` (Code G6CFG) | success; `Sys_GlobalConfig` STG 0→1 |
| Tạo người dùng (web) | **Không có action tạo user trong bản port:** SSO SysManage/UserController chỉ có Selector/UserRoles/GetPagintionGridJson/GetSysUserSelectorTree (đọc); BaseAPI SysUserController chỉ có GetSysUserPage/GetOrgUserTreeWithDept (đọc). Tạo user qua web không kiểm chứng được — gap porting (user mới hiện chỉ tạo được qua đường tạo admin mặc định của Organize, mà nhánh đó cũng đang nuốt lỗi — xem trên) |
| Tạo hồ sơ bệnh nhân cơ bản (qua HOSP) | Không có controller bệnh nhân trong HOSP port; đường tạo bệnh nhân là Sett `OutpatientReg` — đã ghi ở §1 là stub giai đoạn 1 (không ghi). Không làm lại ở đây |
| Điều hướng SSO liên app (portal→module không đăng nhập lại) | **CHƯA ĐẠT trên staging:** trang HOSP-stg `/Sys/Organize/Index?appId=His.SSO` kèm cookie phiên SSO-stg vẫn 302 về `/Login/Index`; appId sai cũng 302 (đúng), nhưng appId đúng cũng không adoption được. Đồng thời `Login/GetUALoginAddres` của SSO-stg trả success nhưng data null (portal không có địa chỉ UALogin của module staging). Nguyên nhân chưa xác định dứt điểm (cần build đối chiếu Redis prefix/Db và cấu hình UALoginAddress của portal) — ghi nhận là blocker liên app, không báo đạt |
| Phát hiện bảo mật (kiểm chứng bằng urllib không cookie) | **LỖI MỚI — HOSP web JSON API không kiểm tra phiên:** `Sys/Organize/GetGridJson`, `Sys/Duty/GetGridJson`, `Sys/SysGlobalConfig/GetGridJson` và cả `SysGlobalConfig/SubmitForm` (POST ghi) đều thực thi khi KHÔNG gửi bất kỳ cookie nào — SubmitForm không cookie đã chạm tới INSERT (chỉ fail ở NOT NULL CreatorCode vì không có identity). Trong khi đó trang View (`/Sys/Organize/Index`) không cookie vẫn 302 về login, và endpoint tương đương của SSO portal không cookie cũng 302. Tức filter `FrameworkAuthorizationFilter` (đã đăng ký global trong HOSP Program.cs) không chặn các action JSON này — build phải xác minh và siết trước khi chạy thật, vì đây là lỗi vượt qua xác thực, không chỉ là thiếu tính năng |

- Chứng minh không ghi demo: demo `Sys_Organize` 2, `Sys_Role` 154,
  `Sys_User` 3, `Sys_GlobalConfig` 0 — giữ nguyên trước/sau.
  STG sau: `Sys_Organize` 4 (2 ban đầu + 1 probe + 1 tạo mới),
  `Sys_Role` 154,
  `Sys_User` 3, `Sys_GlobalConfig` 1 (G6CFG; dòng G6NOLOGIN thử
  không cookie đã fail NOT NULL nên không tạo dòng).
- Dừng cụm: HOSP-stg 15233 + SSO-stg 15208 + BaseAPI-stg 17001 đã
  kill theo PID chính xác, cổng đã down; prod 5233/5208/7001 vẫn UP.

---

## 10. Center — XONG (checkpoint 2026-10-27 tối; AuthCenter-stg 18015 + WebAPI.Manage-stg 18016 + BaseAPI-stg 17001 + Redis 6380)

- Cụm staging đã dùng và **đã dừng**: BaseAPI-stg 17001 (PID 14036),
  AuthCenter-stg 18015 (PID 14037), WebAPI.Manage-stg 18016
  (PID 14038), Redis 6380. Launcher staging dùng lại từ §11
  (`run-authcenter-stg.sh`, `run-apimanage-stg.sh`).
- DRG chỉ spot-check Single theo brief (không làm lại toàn bộ giai
  đoạn 5): envelope đúng của Manage API là top-level
  `{Data:{...}, AppId, OrganizeId, Timestamp}` kèm Bearer JWT
  (không phải `{"request":{...}}` như log §11 của chuỗi khác, và
  `Data` là object chứ không phải chuỗi JSON — thử sai 3 dạng mới
  ra dạng đúng, ghi lại để đợt sau khỏi mò lại).

| Kiểm chứng | Kết quả live |
|---|---|
| Cấp JWT app-to-app cặp khác §11: AuthCenter-stg `POST api/Auth/GetAppToken {AppId MRQC, Data API.Manage}` | 10000, JWT MRQC→API.Manage đã cấp (chỉ lưu file tạm, không in ra báo cáo) |
| DRG Single spot-check `api/DRGGrouper/DrgGroupByMedRecord` (Index G6DRG001, gender 1, age 25, dept KS01, zdList ["0.001"], version chs_drg_11) | 10000, engine trả kết quả có cấu trúc: status "无法入组" (không vào nhóm được), mdc 0000, adrg 00, drg 0000, messages "0.001 未知名称" + "不符合所有MDC的入组条件" — engine chạy thật, mã chẩn đoán test không có trong danh mục nên không nhóm được (đúng hành vi, không crash). Thử sai validation trước đó cũng đúng: thiếu dept → 20000 "The dept field is required." |
| MsgQueue ghi `api/MsgQueue/NewNoticeGroup` (tag G6NoticeTag001, JWT MRQC) | 10000 "Thêm thành công", Id mới `0851787a…`; `GetNoticeGroup` trả lại đúng nhóm. Tag cũ ZKNoticeBl000000 → 40004 "Nhãn nhóm đã được đăng ký" (trùng tag kiểm soát đúng) |
| Chứng minh không ghi demo | Base demo `Msg_NoticeGroup` 3, `Msg_NoticeQueue` 0 — giữ nguyên; STG sau: `Msg_NoticeGroup` 4 (chỉ nhóm G6 mới), `Msg_NoticeQueue` 0. Bảng notice nằm ở DB Base (không phải Interface như phỏng đoán ban đầu) |
| Dừng cụm | Toàn bộ PID staging đã kill theo PID chính xác, các cổng 17001/18015/18016 đã down; prod 8015/8016/7001 vẫn UP |

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

## 12. Tổng kết gap toàn giai đoạn 6 (cho chương trình build — không sửa trong giai đoạn này)

Đã biết từ trước (ghi nhận lại, không phải lỗi mới): engine Pay\*
Sett `SETT_ENGINE_NOT_PORTED`; PDS chuyển kho 404 (controller bị
loại khỏi build); CIS Infusion NRE (DI wiring); CIS hủy hẹn web
no-op im lặng; CIS vital signs cần client tự cấp GUID Id; PDS phát
thuốc cần đủ fyfs + pc và không tự trừ đơn lĩnh.

Mới phát hiện ở đợt này (7 app sau), xếp theo mức độ:

1. **Rò rỉ GHI sang demo — sửa trước khi chạy thật:**
   - MRMS `guidang`/`CXguidang` hardcode `[Newtouch_MRMS]`/
     `[Newtouch_EMR]`; `CXUpdatebazt` update `zy_brjbxx` toàn bộ
     dòng zt=1 không lọc zyh. Đã gây ghi demo thật 1 dòng trong lúc
     kiểm chứng và đã khôi phục về giá trị gốc (xem §7).
2. **Vượt qua xác thực — sửa trước khi chạy thật:**
   - HOSP web JSON API (Organize/Duty/SysGlobalConfig GetGridJson,
     SubmitForm) thực thi không cần session cookie, trong khi trang
     View và endpoint SSO tương đương vẫn chặn (xem §9).
3. **Chặn luồng ghi chính:**
   - Herp: mọi phiếu nhập/xuất kho + lĩnh thuốc fail identity
     (`kf_crkdj`/`kf_applyOrder` long Id không IsIdentity);
     giao hàng cho khoa lỗi chung không rõ nguyên nhân (§4).
   - EMR: tạo/sửa mẫu NRE (bllxDic null); xóa mẫu NRE; khóa/ký bệnh
     án NRE trong DmnService; lưu bệnh án có docData fail NULL Id
     `bl_ysjgnr`; sửa liên kết bệnh án NRE (§5).
   - MRQC: SendMsg chặn ở bước lấy token app-to-app từ AuthCenter
     (lỗi parse `BusData.data`) (§6).
   - MRMS: sửa trang đầu ghi một phần dù trả lỗi NRE (rel);
     SubmitZdList crash IndexOutOfRange khi Code tham số không tồn
     tại; `updateRecordStu` nối chuỗi SQL không tham số hóa (lỗi
     injection bề mặt + crash với Id chuỗi); không có controller
     mượn/trả hồ sơ trong bản port (§7).
   - OR: xếp lịch mổ NRE khi truyền applyno thay vì Id đơn
     (controller/repo không thống nhất key) (§8).
   - Base/SSO: sửa khoa fail 2 dạng (thiếu TopOrganizeId / lỗi
     SqlSugar parameter ở đường update); đọc chi tiết khoa fail
     cùng lỗi SqlSugar; tạo vai trò chặn ở validation DateTime→
     String; không có action tạo user qua web; adoption SSO→module
     qua trang chưa đạt trên staging (§9).
4. **Thiếu trường do UI phải tự cấp (không chặn nếu UI gửi đủ):**
   Herp SubmitPurchase cần đủ zt/sxh/cglx/… và không có transaction
   (lỗi chi tiết vẫn để lại header mồ côi); Herp duyệt phiếu bản
   thân đạt (chứng minh qua fixture); MRQC phê duyệt cần cả hai
   phía (Mr_Apply + Mr_BlApplyRecord ở EMR) mới thông.
5. **Phụ thuộc ngoài / dữ liệu nền:**
   Herp DeliveryAccept gọi host Sun không tới được; MRMS tạo trang
   đầu mới cần bệnh nhân trong `V_EMR_MrBasy` (STG chỉ có 01000);
   EMR SavePatientBl ghi file HTML dưới worktree (không phải DB).
6. **Đọc rò rỉ sang demo (không chặn, chỉ ghi nhận):**
   Sett `[NewtouchHIS_Base]`, CIS `[NewtouchHIS_Sett]`, EMR
   `GetBllxListDetail` `[NewtouchHIS_Base]`, MRMS `DiagnosticSave`
   `[Newtouch_MRMS]` + `Updatebazt` (mục 1 là ghi, khác đọc).

### Kiểm chứng cuối giai đoạn (sau khi dừng toàn bộ staging)

- 26/26 cổng prod LISTEN: 1433, 6379, 41142, 5208, 7001, 7002,
  8015, 8016, 5220, 5221, 5231–5238, 5320, 5331–5337 (không cổng
  staging nào còn nghe).
- SSO prod login 000000 thành công sau cùng.
- Git: KHÔNG commit/push trong giai đoạn này (theo brief). Thay đổi
  chờ điều phối viên soát: `scripts/staging/REPORT.md` (§4–§10, §12),
  launcher staging mới `run-emr-stg.sh`, `run-mrqc-stg.sh`,
  `run-mrms-stg.sh`, `run-or-stg.sh`, `run-hosp-stg.sh`,
  `run-sso-stg.sh` + chmod +x `run-herp-stg.sh` (đã có từ đợt trước).

---


## App còn lại

Không còn — cả 7 app đợt này (Herp, EMR, MRQC, MRMS, OR, Base/HOSP + SSO,
Center) đều đã có checkpoint §4–§10; Sett, CIS, PDS, Liên app đã xong từ
đợt trước. Còn lại cho điều phối viên: soát REPORT.md rồi commit cuối
giai đoạn (worker không commit/push theo brief).
