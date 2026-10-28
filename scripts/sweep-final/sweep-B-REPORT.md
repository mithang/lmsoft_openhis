# BÁO CÁO SWEEP-B — Sett Web 5221 + Sett-API 7002

- **Worker:** SWEEP-B (Sett + Sett-API), đợt sweep cuối OpenHIS.
- **Thời gian:** 2026-10-28 (giờ ICT, ngày 2026-10-28).
- **Base:** V2.2 HEAD `e66c7bda` tại `~/workspace/openhis-core` (xác nhận `git log -1` trước và sau khi làm việc; không đổi).
- **Nguyên tắc tuân thủ:** KHÔNG sửa code, KHÔNG commit, KHÔNG sửa `roles-shots-progress.md`
  (chỉ đọc để đối chiếu mã SETT-xx). Trên PROD chỉ kiểm tra đọc/cổng validation
  (không ghi DB demo dưới mọi hình thức). Mọi kiểm thử ghi chạy qua STAGING.
  Proxy env UNSET, `DOTNET_SYSTEM_NET_DISABLEIPV6=true` cho mọi lệnh loopback.
  Mật khẩu SA giữ nguyên, chỉ đọc qua env file lúc chạy, không ghi vào báo cáo.
- **Kết quả tổng quan:** Sett-API kiểm chứng **đầy đủ, đạt** (mọi mã Y có caller 8016,
  Y026 thành công trên STG, Pay* fail sạch). Sett Web **chỉ kiểm chứng được một phần
  do blocker login** (xem §1): 22/22 area đã thăm bằng route-probe + đối chiếu source,
  nhưng **không trang nào sau login được render/kiểm thử tương tác** vì không có
  session. 2 bug mới ghi nhận (B1, B2). Ảnh mobile cho SETT-17 (trang Settlement sau
  login) **vẫn còn nợ** vì cùng blocker.

## 1. BLOCKER — không có mật khẩu demo trong phiên này (quan trọng nhất)

- Biến `SSO_PASSWORD` **không tồn tại** trong shell của worker này.
- Quét workspace không thấy file nào gán `SSO_PASSWORD`; `.appsecrets.env` không chứa nó.
- Đây đúng là blocker mà báo cáo `stg-login-fix-REPORT.md` đã nêu (worker đó cũng
  không có mật khẩu và đã dừng để tránh khóa tài khoản).
- **Số lần thử login tài khoản 000000 trong phiên này: 0.** Không đoán mò mật khẩu,
  vì lockout thật là **3 lần sai liên tiếp → khóa tài khoản** (7-fix). Một lần đoán
  sai cũng là 1 strike vào tài khoản mà các worker sweep khác vẫn cần.
- Hệ quả trực tiếp:
  - Không có session SSO staging (15208) → không vào được Sett Web staging (15221)
    lẫn Sett Web prod (5221) sau login; mọi trang sau login đều 302 về `/Login/Index`.
  - Không kiểm thử được: render trang sau login, thêm/sửa/xóa/lọc/tìm kiếm/sắp xếp
    trên giao diện web, trang Settlement mobile của SETT-17, thông báo login thất bại
    SETT-16 (cần 1 lần login sai có kiểm soát — không làm vì lý do lockout trên).
  - **Cần điều phối viên cung cấp mật khẩu demo (kênh đã dùng cho các lần kiểm chứng
    trước) hoặc một session cookie staging còn sống**, rồi chạy lại đúng phần web
    sau login theo checklist §7. Phần Sett-API không bị ảnh hưởng (xem §4) vì các
    route đó tokenless theo thiết kế gốc.
- Quyết định kèm theo: **không khởi động Sett Web staging 15221.** Lý do: (a) không
  có session thì staging web cũng chỉ cho kết quả 302 như prod; (b) binary staging
  trong worktree `~/workspace/openhis-stg` ở commit cũ `0a730b1b` (trước phase 2),
  nên thăm nó sẽ kiểm chứng code cũ, không phải HEAD — ghi nhận để điều phối viên
  quyết định có rebuild staging web từ HEAD trước vòng follow-up hay không.
  Trong phiên này chỉ có đúng 1 app staging do worker này khởi động: Sett-API
  17002 (PID 18080, đã kill sau khi xong; cổng 17002 đã đóng, kiểm chứng bằng `ss`).
  SSO staging 15208 và BaseAPI staging 17001 đã chạy sẵn từ trước, không đụng tới.

## 2. Kiểm kê chức năng từ source (Sett Web) — số liệu thật từ quét source

- **22 area** (đủ, theo `Newtouch.Sett.Web/Areas/`): BusinessManage, DRGManage,
  DaySettleManage, DeanInquiryManage, ExampleManage, FeeConfirmManage, HighchartsManage,
  HospitalizationManage, InsuranceManage, IssueTreatRecord, JGManage, KPI,
  KnowledgeBaseManage, NonTreatmentItemManage, OutpatientManage, PatientManage,
  PayManage, ReportManage, SiteManage, SystemManage, SystemSecurity, TherapeutistManage.
- **115 controller** (đếm file `*Controller.cs` trong Areas) và **484 view `.cshtml`**
  (đếm file trong `Areas/*/Views`; con số "~365 trang" trong task thấp hơn số view
  thực tế vì view gồm cả partial/shared/form con).
- **~1.067 action** (đếm bằng regex `public (IActionResult|JsonResult|ActionResult|
  Task<IActionResult>) <Tên>(` trên controller các area; là số liệu quét source,
  không phải số trang đã click). Phân nhóm thô theo tiền tố tên action
  (get/list ≈ Get/Select/Load/Query/Index/Form/List; save ≈ Save/Submit/Add/Create/
  Update/Edit/Confirm; del ≈ Delete/Remove/Cancel):

| Area | Actions (quét) | Get/List | Save | Del/Cancel | Khác |
|---|---|---|---|---|---|
| BusinessManage | 23 | 13 | 2 | 0 | 8 |
| DRGManage | 9 | 6 | 0 | 0 | 3 |
| DaySettleManage | 26 | 10 | 2 | 0 | 14 |
| DeanInquiryManage | 48 | 38 | 0 | 0 | 10 |
| ExampleManage | 1 | 0 | 0 | 0 | 1 |
| FeeConfirmManage | 3 | 1 | 0 | 0 | 2 |
| HighchartsManage | 33 | 10 | 0 | 0 | 23 |
| HospitalizationManage | 135 | 47 | 11 | 6 | 71 |
| InsuranceManage | 8 | 4 | 2 | 2 | 0 |
| IssueTreatRecord | 2 | 1 | 0 | 0 | 1 |
| JGManage | 10 | 6 | 0 | 0 | 4 |
| KPI | 14 | 4 | 2 | 0 | 8 |
| KnowledgeBaseManage | 4 | 2 | 1 | 1 | 0 |
| NonTreatmentItemManage | 10 | 4 | 2 | 0 | 4 |
| OutpatientManage | 214 | 71 | 37 | 3 | 103 |
| PatientManage | 134 | 40 | 9 | 2 | 83 |
| PayManage | 9 | 1 | 0 | 0 | 8 |
| ReportManage | 117 | 13 | 1 | 0 | 103 |
| SiteManage | 21 | 6 | 0 | 1 | 14 |
| SystemManage | 210 | 106 | 31 | 27 | 46 |
| SystemSecurity | 12 | 4 | 3 | 3 | 2 |
| TherapeutistManage | 24 | 11 | 7 | 0 | 6 |
| **Tổng** | **1.067** | | | | |

- Nhóm chức năng theo area (từ tên controller/view, tóm tắt, không bịa route):
  BusinessManage (đăng ký ngoại trú, tra cứu thu phí, hoàn phí); DRGManage (upload
  DRG); DaySettleManage (đối chiếu sổ sách ngày — DzrIndex/DzrForm/DzrCheck/DzrHistory);
  DeanInquiryManage (10 controller thống kê viện trưởng: xếp hạng, vật tư, thuốc,
  khối lượng, chi phí…); ExampleManage (mã vạch, in ấn, gửi mail/SMS, con dấu);
  FeeConfirmManage (xác nhận phí); HighchartsManage (biểu đồ); HospitalizationManage
  (ghi sổ nội trú, quyết toán xuất viện, hoàn phí, Settlement); InsuranceManage
  (bảo hiểm thương mại); IssueTreatRecord (xác nhận phiếu điều trị); JGManage
  (upload JG); KPI (thuật toán/cấu hình KPI tổ chức và trị liệu); KnowledgeBaseManage
  (tri thức BHYT); NonTreatmentItemManage (ghi sổ hạng mục không điều trị);
  OutpatientManage (13 controller: đăng ký, lịch hẹn, thu phí, hoàn phí, quyết toán,
  kế toán ngoại trú, Pay, Settlement); PatientManage (tài khoản BN, nhập viện,
  danh sách nội trú/xuất viện, hồ sơ bệnh án); PayManage (MicroPay: thanh toán,
  tra cứu giao dịch, hoàn tiền); ReportManage (báo cáo + Echarts); SiteManage
  (thống kê site); SystemManage (42 controller: danh mục hệ thống lớn nhất — khu vực,
  chức vụ, hạng mục thu phí, hóa đơn, biên lai, log API…); SystemSecurity (sao lưu
  DB, chặn IP, giám sát server); TherapeutistManage (lời khuyên trị liệu, hạng mục
  không tính phí, lịch trực).

## 3. Phủ kiểm thử Sett Web trên PROD 5221 (chỉ đọc, không ghi)

Phương pháp (do blocker §1): với mỗi area, thăm **1 trang đại diện bằng GET không
cookie** và ghi HTTP status thật. 302 về `/Login/Index` = route tồn tại + cổng
auth hoạt động (đúng hành vi sau phase 7: no-cookie → 302). 404 = route không tồn
tại. Đây là bằng chứng về **tồn tại route + auth gate**, KHÔNG phải bằng chứng
trang render đúng — báo cáo ghi đúng mức này, không phóng đại thành "trang đã test xong".

| # | Area | Trang đại diện đã thăm (route thật) | PROD 5221 no-cookie | Viewport |
|---|---|---|---|---|
| 1 | BusinessManage | /BusinessManage/OutPatient/OutpatientReg | 302 → /Login/Index | curl (không render) |
| 2 | DRGManage | /DRGManage/DRGUpload/Index | 302 | curl |
| 3 | DaySettleManage | /DaySettleManage/DaySettle/DzrIndex | 302 | curl |
| 4 | DeanInquiryManage | /DeanInquiryManage/BusinessRanking/Index | 302 | curl |
| 5 | ExampleManage | /ExampleManage/BarCode/Index | 302 | curl |
| 6 | FeeConfirmManage | /FeeConfirmManage/FeeConfirm/Index | 302 | curl |
| 7 | HighchartsManage | /HighchartsManage/Highcharts/Sample1 | 302 | curl |
| 8 | HospitalizationManage | /HospitalizationManage/Settlement/Index | 302 | curl |
| 9 | InsuranceManage | /InsuranceManage/CommercialInsurance/Index | 302 | curl |
| 10 | IssueTreatRecord | /IssueTreatRecord/ComfirmIssue/Index | 302 | curl |
| 11 | JGManage | /JGManage/JGUpload/Index | 302 | curl |
| 12 | KPI | /KPI/MedicalOrg/Index | 302 | curl |
| 13 | KnowledgeBaseManage | /KnowledgeBaseManage/MedicalInsurance/Index | 302 | curl |
| 14 | NonTreatmentItemManage | /NonTreatmentItemManage/NonTreatmentItem/Bookkeeping | 302 | curl |
| 15 | OutpatientManage | /OutpatientManage/OutpatientReg/OutPatientAppointment | 302 | curl |
| 16 | PatientManage | /PatientManage/PatientInHosList/Index | 302 | curl |
| 17 | PayManage | /PayManage/MicroPay/MicroPay | 302 | curl |
| 18 | ReportManage | /ReportManage/Report/ChargeDailysettlement | 302 | curl |
| 19 | SiteManage | /SiteManage/SiteStatistics/Index | 302 | curl |
| 20 | SystemManage | /SystemManage/Area/Index | 302 | curl |
| 21 | SystemSecurity | /SystemSecurity/ServerMonitoring/Index | 302 | curl |
| 22 | TherapeutistManage | /TherapeutistManage/TherapistAdvice/Index | 302 | curl |
| + | OutpatientManage (SETT-17 liên quan) | /OutpatientManage/Settlement/SettQuery | 302 | curl |
| + | OutpatientManage (SETT-03) | /OutpatientManage/OutpatCharge/Index2018 | 302 | curl |
| + | HospitalizationManage (SETT-03) | /HospitalizationManage/BookkeepInHos/Accounting | 302 | curl |
| + | — | /Home/Index (no-cookie) | 302 | curl |
| + | — | /Login/Index | **200** | desktop + mobile (screenshot) |

- **Tổng số trang đã thăm thật trên Sett Web: 26 route bằng curl + 1 trang login
  render bằng Playwright/Firefox ở 2 viewport** (desktop 1366×900, mobile 390×844).
  Không có trang sau login nào được render — xem blocker §1.
- Đối chứng phân biệt 302/404 (để khẳng định 302 thực sự nghĩa là route tồn tại):
  route không tồn tại `/SettKhongTonTai/Xyz/Index` → 404; endpoint JSON
  `/SystemManage/Area/GetGridJson` → 404 (không kết luận là bug vì chưa xác minh tên
  action dữ liệu thật của controller Area — chỉ ghi nhận để vòng follow-up đối chiếu).
- Menu Sett Web: menu lấy từ DB/phiên sau login nên không trích xuất được khi không
  có session; bảng trên thay bằng liệt kê controller/view từ source (§2). Không bịa
  menu.

## 4. Sett-API trên STAGING 17002 — kiểm chứng đầy đủ, ĐẠT

Host staging khởi động bằng `run-sett-api-stg17002.sh` (code HEAD `e66c7bda`,
DB `NewtouchHIS_Sett_STG` / `NewtouchHIS_Base_STG` / `Newtouch_Interface_STG`,
không chạm DB demo). Tổ chức demo dùng cho mọi test:
`6d5752a7-234a-403e-aa1c-df8b45d3469f` (= `Top_OrganizeId` trong appsettings).
Tất cả response đều là envelope HTTP 200 `{code, msg, data, sub_code, sub_msg}`.

### 4.1 BookingESB — mọi mã Y có caller 8016

| Mã | Tham số đã thử | Kết quả STG 17002 |
|---|---|---|
| (cổng) | OrganizeId/methodcode rỗng | 40004 `GATEWAY_PARAM_REQUIRED` "Tham Số cổng BookingESB không được để trống" — cổng chặn trước DB |
| Y999 (không triển khai) | — | 40004 `SETT_FAIL` "không có caller 8016 nên chưa triển khai" |
| Y002 DeptInfo | org demo | **10000**, danh sách khoa (KS04 Khoa Phu san, KS18, 03, 04, KS03, KS21…) |
| Y003 StaffInfo | org demo | **10000**, danh sách nhân viên |
| Y004 optype 0 | chỉ `kh` → FAIL validation đúng (cần `xm` + `kh`/`zjh`); `kh=10000 + xm=A Binh` | **10000** "Tra cứu thành công" (patid 100000, blh 11000) |
| Y004 optype 1 (ghi BN mới) | — | **KHÔNG test** (sẽ tạo BN + thẻ mới trên STG; để vòng follow-up quyết định có cần fixture BN hay không) |
| Y004 optype 3 | — | FAIL rõ "không có caller 8016 nên chưa triển khai" (theo thiết kế) |
| Y009 Schedule | trước fixture: không có lịch ≥ hôm nay → FAIL "Không tìm thấy bản ghi" (đúng dữ liệu); sau fixture | **10000**, trả đúng dòng lịch fixture 11597 |
| Y021 BookRecord | `kh=10000` | **10000**, bản ghi đặt hẹn (BookId 1, lịch 1597 ngày 2026-10-27) |
| Y022 BookApply / Y023 BookCancel / Y024 BookPay | paradata rỗng | 40004 FAIL validation "Thông tin quan trọng không được để trống" — chặn sạch, không ghi |
| Y026 (xem §4.2) | — | **10000 "Đăng ký khám thành công."** cả 2 đường |
| Y027 RegTodayQuery | trước Y026: không có đăng ký hôm nay → FAIL; sau Y026 với `kh=10000` | **10000**, trả đăng ký hôm nay |
| Y028 BillUnpaidByMzh | `mzh=2610275970002` | **10000**, 1 dòng đơn thuốc CF-STG-1, 25.50 |
| Y029 BillItemDetail | chỉ `mzh` → FAIL thiếu tham số; `mzh + PresId=100004` | **10000**, chi tiết đơn (cfmxId 100008, 25.50) |
| Prefix alias | `POST /api/Booking/BookingESB` Y002 | **10000** — alias hoạt động như `/api/OutpBook/BookingESB` |

### 4.2 Y026 — chuỗi CIS→8016→BookingESB đã sửa: THÀNH CÔNG trên STG

Hiện trạng dữ liệu trước test: lịch trực STG mới nhất là ngày **2026-10-27**
(ScheduId 1598/1597), **không có lịch nào cho hôm nay 2026-10-28** nên mọi cuộc gọi
Y026 ban đầu đều dừng đúng ở cổng lịch trực — đây là thiếu fixture, không phải lỗi code.
Các cổng kiểm tra trước đó đều đúng:

- Y026 thiếu cả `ks` lẫn `ScheduId` → 40004 "Thông tin quan trọng không được để trống".
- Y026 `kh` không tồn tại → 40004 "Thông tin thẻ bất thường, vui lòng liên hệ quản trị viên."
- Y026 `kh=10000 + ks=KS04` (chưa có lịch hôm nay) → 40004 "Khoa này hôm nay không có lịch trực liên quan."

Để đạt trạng thái thành công theo yêu cầu, worker đã chèn **1 fixture duy nhất vào
STG** (không chạm demo): copy dòng lịch 1598 sang ngày hôm nay, identity mới
**ScheduId 11597** (KS04 Khoa Phu san, RegType 1, OutDate 2026-10-28, IsBook 1,
IsCancel 0, CreatorCode `SWEEP-B`). Sau đó:

- **Đường shim CIS→8016 (không có `ks`, chỉ `ScheduId=11597`, `kh=10000`,
  `ghxz=1`) → code 10000, msg "Đăng ký khám thành công.", data
  `{mzh: 2610285970001, jsnm: 0, jszje: 3.0000, jsxjf: 3.0000}`.** Đây chính là
  đường từng hỏng (8016 không gửi `ks`, gốc fail ở cổng `ks`); shim suy `ks` từ
  dòng lịch đã hoạt động.
- **Đường đủ `ks` (`ks=KS04`, `kh=10000`, `ghxz=1`) → code 10000,
  `{mzh: 2610285970002, jszje: 3.0000}`** (đăng ký thứ 2 cùng BN cùng khoa cùng ngày
  vẫn thành công; cả 2 dòng `mz_gh` đều có `ghzt=1` — ghi nhận hiện trạng, không
  suy diễn thêm về cổng đăng ký trùng).
- Kiểm chứng chéo: `OutpatRegRecord (kh=10000)` trả cả 2 dòng mới; `Y027` sau đó
  trả đăng ký hôm nay; `Y009 (ks=KS04)` trả dòng lịch 11597.
- **Bằng chứng không chạm demo:** `SELECT COUNT(*) FROM NewtouchHIS_Sett.dbo.mz_gh
  WHERE mzh IN ('2610285970001','2610285970002')` = **0**; 2 dòng chỉ có trong
  `NewtouchHIS_Sett_STG.dbo.mz_gh`.
- Fixture được **để lại nguyên trong STG** (lịch 11597 + 2 dòng `mz_gh` đã quyết
  toán `ghzt=1` nên không chặn đăng ký mới — cổng "đăng ký chưa quyết toán" chỉ
  xét `ghzt='0'`). Lý do để lại: vòng follow-up và các worker khác có thể dùng lại
  lịch hôm nay để test Y026 mà không cần chèn lại. Nếu điều phối viên muốn STG
  sạch hoàn toàn thì xóa 3 dòng này là đủ.
- Chuỗi đầy đủ qua 8016 staging (18016) **chưa** chạy: host staging 18015/18016
  không chạy trong phiên này và việc gọi 8016 prod sẽ proxy sang Sett-API prod
  7002 (DB demo) nên tuyệt đối không thử. Phần đã sửa nằm ở host Sett-API (shim
  ScheduId) và đã được chứng minh trực tiếp tại 17002; đoạn proxy 8016→7002 là
  việc của worker Center/sweep-A nếu cần, với host staging.

### 4.3 PatientAnon — 5/5 endpoint phản hồi đúng

| Endpoint | Kết quả |
|---|---|
| InHosPatientQuery (paradata rỗng) | **10000**, 1 BN nội trú (zyh 01000, kh 10000, bqmc "Khu 1 Khoa Noi") |
| InHosBillUnpaid (thiếu tham số) | 40004 FAIL validation "Thông tin quan trọng không được để trống" — sạch |
| OutpatRegRecord (`kh=10000`) | **10000**, danh sách đăng ký ngoại trú (gồm 2 dòng Y026 mới) |
| PatientLisReport (thiếu tham số) | 40004 FAIL "Thông tin quan trọng(Số ngoại trú/Số thẻ) không được để trống" |
| PatientLisReportDetail (thiếu tham số) | 40004 FAIL "Thông tin quan trọng(sqdh,ywlx) không được để trống" |

### 4.4 OrderCenter — 8/8 endpoint phản hồi đúng; Pay* fail sạch

| Endpoint | Kết quả |
|---|---|
| OrderQuery (`CardNo=10000`) | **10000**, đơn 120261027114000001 (OrderType 1, OrderStu 1 = đang khóa, 25.50) |
| OrderDetailQuery / test (`OrderNo=120261027114000001`) | **10000**, `mzList` 1 dòng CF-STG-1 25.50 (cả 2 endpoint) |
| **PayOrder** (đơn khóa hợp lệ: PayWay 0, PayFee 25.50, CardNo 10000, VisitNo 2610275970002) | **40004 `SETT_ENGINE_NOT_PORTED`** "Engine quyết toán ngoại trú chưa được port (SETT_ENGINE_NOT_PORTED)" — fail sạch, xem kiểm chứng ghi ở dưới |
| **PayOutpOrder** (cùng bộ tham số) | **40004 `SETT_ENGINE_NOT_PORTED`** — fail sạch |
| PayInHosOrder (cùng đơn ngoại trú) | 40004 `SETT_FAIL` "Không phải đơn hàng nội trú" — từ chối đúng ở cổng loại đơn, **chưa tới được cổng engine nội trú** vì STG không có đơn nội trú đang khóa nào (xem §6) |
| PayOrder (đơn không tồn tại) | 40004 `SETT_FAIL` "Thanh toán thất bại, thông tin đơn hàng đã hết hạn…" — chặn sạch |
| OrderCreate / LockerApply | **KHÔNG test** (cả 2 đều ghi/khóa đơn; OrderCreate cần bộ tham số đơn thuốc hợp lệ chưa có fixture; để vòng follow-up — không gọi mò để tránh tạo đơn rác trên STG) |

**Kiểm chứng Pay* không ghi gì:** sau các cuộc gọi Pay*, dòng đơn
120261027114000001 trong STG vẫn `OrderStu=1, PayFee=NULL, PayTime=NULL` —
không đổi. Chỉ có duy nhất 1 kiểm tra đọc trên prod: `POST /api/OutpBook/BookingESB`
tới prod 7002 với OrganizeId rỗng → 40004 `GATEWAY_PARAM_REQUIRED`, bị chặn
trước khi chạm DB (không ghi demo).

## 5. Bug tìm được (không sửa, chỉ ghi nhận cho vòng fix sau)

### B1 — Trang login của Sett Web mang title và appId của MRQC (mới, đã xác nhận bằng ảnh + source)

- **App/trang:** Sett Web, `/Login/Index` (prod 5221), cả desktop và mobile.
- **Bước tái hiện:** mở `http://127.0.0.1:5221/Login/Index` không cần login.
- **Mong đợi:** title/appId của Sett (hệ thống quyết toán) vì đây là cổng đăng nhập
  riêng của Sett.
- **Thực tế:** browser tab title = **"MRQC Hệ thống kiểm soát chất lượng"**;
  source `Newtouch.Sett.Web/Views/Login/Index.cshtml` dòng 15 hard-code
  `<title>@("MRQC Hệ thống kiểm soát chất lượng")</title>` và dòng 159
  `var appId = '@("MRQC")';` — view login này copy từ MRQC mà chưa đổi sang Sett.
  Form đăng nhập thì đúng tiếng Việt ("Chào mừng", "Tài khoản đăng nhập",
  "Mật khẩu đăng nhập", nút "Đăng nhập"), nên chỉ title/appId là sai.
- **Ảnh:** `shots-sweep/sett-B-login-desktop.png` (title hiển thị trong tab của
  Playwright; nội dung trang xem trong ảnh).
- **Mức độ:** thấp về nghiệp vụ (chỉ nhãn), nhưng appId sai có thể ảnh hưởng
  session/cookie theo AppId — vòng fix nên xác minh appId Sett thật trong SSO
  trước khi đổi (không tự đổi trong đợt sweep này).

### B2 — Trang login Sett trên mobile 390×844 bị bóp, khó đọc (mới)

- **App/trang:** Sett Web `/Login/Index`, viewport mobile 390×844 (emulation
  Playwright + Firefox; **không phải app native** — OpenHIS không có app native).
- **Bước tái hiện:** mở trang login ở viewport 390×844.
- **Mong đợi:** form đăng nhập co giãn theo chiều rộng điện thoại, chữ đọc được.
- **Thực tế:** card đăng nhập giữ chiều rộng cố định, dồn về góc trên-phải, chữ
  và ô nhập rất nhỏ, gần như phải zoom mới đọc được (xem
  `shots-sweep/sett-B-login-mobile390.png`). Trang không vỡ layout hoàn toàn
  (không chồng lấn), nhưng không dùng được thoải mái trên điện thoại.
- **Lưu ý phân biệt:** đây là trang **login**, không phải trang Settlement của
  SETT-17. SETT-17 vẫn chưa kiểm chứng được (xem §6).

### Quan sát dữ liệu STG (không phải bug code — chỉ ghi nhận hiện trạng, không tự thống nhất)

- `Y009` trả `ghxmmc: "挂号费"` (tiếng Trung, từ view `V_S_xt_sfxm` của Base STG);
  `Y004` trả `klx: "虚拟卡"`; `InHosPatientQuery` trả `xm: "阿炳"`; `OrderQuery` trả
  Memo chứa ký tự lỗi `"His khóa ??n hàng(...)"` (mojibake). Các chuỗi này nằm
  trong **dữ liệu STG**, không phải code Sett-API (code đã dịch xong ở đợt
  translate-all). Ghi nhận để đợt dữ liệu quyết định có dịch nốt các dòng STG này
  hay không — worker không tự sửa dữ liệu.
- Footer trang login Sett vẫn là dòng copyright tiếng Trung của vendor
  ("Copyright © 2025湖北天天开元…"). Đây là chuỗi vendor gốc, xuất hiện cả trên
  ảnh login desktop; ghi nhận, không tự dịch/xóa.
- **Tiền tố số chứng từ:** trong phạm vi test lần này không gặp số biên lai/thu phí
  mới nào (Y026 sinh `mzh` là số đăng ký khám, không phải số chứng từ thu phí),
  nên không có gì để ghi nhận thêm về tiền tố; vấn đề không thống nhất tiền tố cũ/
  mới từ đợt translate-all vẫn giữ nguyên hiện trạng, không tự thống nhất.

## 6. Các điểm phase 2 còn tồn — kiểm chứng riêng (ghi đúng hiện trạng)

| Điểm | Kết luận sau kiểm chứng |
|---|---|
| **SETT-03 dữ liệu tìm kiếm** | **ĐÚNG là còn thiếu.** Plugin `sfxmFloatingSelector` trong `wwwroot/Content/js/page.common.js` gọi `url: '/SystemManage/BaseData/SelectSfxmYp'` với tham số `keyword=…&mzzybz=1&dllb=1,2`; nhưng **không có `BaseDataController`** trong `Areas/SystemManage/Controllers/` (endpoint cũ `SysChargeItemController` chỉ còn ghi chú `[Obsolete("replaced by /SystemManage/BaseData/SelectSfxmYp")]`). Kiểm chứng live trên prod: `GET /SystemManage/BaseData/SelectSfxmYp?keyword=test` → **404** (trong khi route tồn tại thì no-cookie phải 302 — đã đối chứng bằng các route 302 ở §3). Vì vậy selector tìm khoản thu phí trên các form ghi sổ (vd `OutpatCharge/Index2018`, `BookkeepInHos/Accounting`) **không lấy được dữ liệu tìm kiếm**. Chưa kiểm thử tương tác selector vì cần session (blocker §1); kết luận dựa trên source + probe 404, đủ chắc chắn. |
| **SETT-06 ReportUrl thật** | **ĐÚNG là chưa có.** `appsettings.json` của Sett Web không có key `ReportUrl`/`ReportSystemCode`; view `DaySettleManage/DaySettle/DzrIndex.cshtml` đọc `Config["ReportUrl"] ?? ""` và code phase 2 đã chặn tự nhúng: khi rỗng thì `search()` chỉ hiện cảnh báo tiếng Việt "Chưa cấu hình máy chủ báo cáo (ReportUrl) nên không hiển thị được báo cáo đối chiếu sổ sách…" và `return`, **không đặt iframe src = chính trang nữa**. Báo cáo đối chiếu sổ sách thật chỉ hiển thị được khi có máy chủ báo cáo + cấu hình — đây là việc hạ tầng/tích hợp, không phải bug code. Chưa render live được vì cần session. |
| **PatientInHosList lọc keyword** | **ĐÚNG là chưa có field.** View `PatientManage/PatientInHosList/Index.cshtml` có ô `#txt_keyword` (placeholder "Họ tên / Số nhập viện") và gửi `postData: { keyword: … }` tới `LoadData`, nhưng class `PatientListInputVO` trong `Sett.Infrastructure/Domain/Types_PatientManage.cs` là **class rỗng, không có field nào** (kể cả keyword) nên keyword không bind được vào VO. Controller `LoadData` nhận VO rỗng, gọi `_printService.PatientList(VO)`; theo ghi chú trong view, service hiện trả danh sách rỗng nên lưới chỉ hiện trạng thái rỗng. Ngoài ra `LoadData` trả `View()` khi data null — hành vi này giữ nguyên, ghi nhận không sửa. Chưa test live vì cần session. |
| **Y026 (CIS→8016→BookingESB)** | **ĐÃ SỬA, ĐÃ THÀNH CÔNG trên STG** — xem §4.2 (cả đường shim ScheduId lẫn đường đủ `ks`). |
| **Pay\* fail sạch** | **ĐÚNG.** PayOrder/PayOutpOrder → `SETT_ENGINE_NOT_PORTED`, đơn không đổi (§4.4). PayInHosOrder chưa tới được cổng engine vì thiếu fixture đơn nội trú đang khóa — không phải lỗi fail-sạch. |
| **Endpoint tokenless** | **Ghi nhận hiện trạng, không tự quyết.** Toàn bộ 3 nhóm route Sett-API (`api/OutpBook`, `api/Booking`, `api/PatientAnon`, `api/OrderCenter`) không yêu cầu xác thực theo thiết kế gốc (`[IgnoreTokenDecrypt]`, 8016 không gửi token — ghi chú trong `SettApiControllers.cs` và `BookingModels.cs`). Đã kiểm chứng live: mọi cuộc gọi §4 đều không kèm token/cookie mà vẫn được xử lý. Việc "endpoint tokenless hợp lệ cần chặn ở tầng mạng" là **quyết định mở** theo đúng task: worker chỉ ghi nhận, không tự thêm auth (thêm auth sẽ phá chuỗi 8016) và không redesign. |
| **Schema zy_bqksby / zy_ksbyth** | **Ghi nhận hiện trạng, không tự quyết.** Cả 2 bảng đều **không tồn tại** trong `NewtouchHIS_Sett_STG` lẫn `NewtouchHIS_Sett` (truy vấn `INFORMATION_SCHEMA.TABLES` trả 0 dòng cho cả 2 DB) và không có file `.cs` nào trong Sett Web tham chiếu tới chúng. Mọi đường code Sett đã chạy trong phiên này đều không chạm tới 2 bảng này. Việc bổ sung schema hay sửa code tham chiếu là quyết định mở, không làm trong đợt sweep. |

## 7. Phần không tái hiện được / không test được + lý do (đầy đủ, không giấu)

1. **Toàn bộ tương tác sau login trên Sett Web** (thêm/sửa/xóa/lấy/lọc/tìm kiếm/
   sắp xếp theo từng area, render 22 trang đại diện, dashboard, lưới jqGrid,
   validation-gate trên form): **không test được** — blocker mật khẩu §1
   (0 lần thử login). Route-probe §3 chỉ chứng minh route tồn tại + auth gate.
2. **Ảnh mobile SETT-17 (trang Settlement sau login): không chụp được.** Đã chụp
   thay thế: trang login mobile + lần mở `/HospitalizationManage/Settlement/Index`
   trên mobile (bị redirect về login — ảnh `sett-B-settlement-mobile390-redirect-login.png`
   thực chất là ảnh trang login). Ảnh Settlement thật vẫn nợ, cần session.
3. **SETT-16 (thông báo đăng nhập thất bại bằng tiếng Trung từ DLL framework):
   không test được** — cần 1 lần login sai có kiểm soát; không làm vì sẽ ghi 1
   strike lockout vào tài khoản demo khi chưa có mật khẩu đúng để "xả" strike.
4. **Y004 optype 1 (tạo BN mới), OrderCreate, LockerApply: không test** — đều là
   ghi/khóa dữ liệu, cần fixture tham số hợp lệ (BN mới / đơn thuốc chưa thu phí)
   chưa có; không gọi mò để tránh tạo dữ liệu rác trên STG.
5. **PayInHosOrder tới cổng engine nội trú: không tới được** — STG không có đơn
   nội trú đang khóa; không tự bịa đơn nội trú + BN nội trú giả để ép tới cổng.
6. **Chuỗi đầy đủ qua 8016 staging (18016): không chạy** — host staging 18015/
   18016 không chạy; gọi 8016 prod sẽ chạm DB demo nên cấm.
7. **Chức năng cần máy chủ ngoài:** báo cáo SSRS/ReportUrl (SETT-06), thanh toán
   MicroPay/PayManage với cổng thanh toán thật, gửi mail/SMS (ExampleManage),
   upload DRG/JG với file thật, bảo hiểm GuiAn (InsuranceManage): **không test
   được** vì không có máy chủ/cổng đối tác trong môi trường này.
8. **Mobile:** chỉ có emulation viewport 390×844 bằng Playwright + Firefox.
   OpenHIS **không có app native** nên không có — và không bao giờ tuyên bố —
   kiểm thử app native. Các trang sau login trên mobile cũng bị blocker §1.

## 8. Checklist cho vòng follow-up (khi điều phối viên cấp mật khẩu/session)

1. Login SSO staging 15208 → mở Sett Web staging (cần rebuild từ HEAD trước,
   xem §1) hoặc prod 5221 ở chế độ chỉ đọc; sau đó render đủ 22 trang đại diện
   ở §3 (desktop + mobile 390×844), chụp ảnh Settlement mobile cho SETT-17.
2. Trên staging, chạy thử nhóm chức năng mỗi area: 1 lần lấy/lọc/tìm kiếm/sắp xếp
   (đọc) + 1 lần thêm/sửa/xóa có kiểm soát (ghi STG), ưu tiên: OutpatientReg,
   OutpatCharge (kèm kiểm chứng SETT-03 sau khi có BaseData), Settlement,
   PatientInHosList (kèm kiểm chứng lọc keyword sau khi VO có field),
   DaySettle DzrIndex (khi có ReportUrl), MicroPay (chế độ test của cổng).
3. Kiểm chứng SETT-16 bằng đúng 1 lần login sai có kiểm soát **sau khi đã login
   đúng thành công** (để không tích lũy strike mù).
4. Sửa (vòng fix, không phải sweep): B1 (title/appId login Sett), B2 (login mobile),
   tạo `BaseDataController.SelectSfxmYp` hoặc đổi URL selector, thêm field
   keyword cho `PatientListInputVO`, quyết định tokenless (tầng mạng) và schema
   zy_bqksby/zy_ksbyth.

## 9. Ảnh đã chụp (Playwright + Firefox local, không qua browser task)

Lưu tại `~/workspace/openhis-core/shots-sweep/` (thư mục này đã bị gitignore
bởi quy tắc `shots*/` trong `.gitignore` — kiểm chứng bằng `git check-ignore`,
ảnh **không thể lọt vào commit**):

- `sett-B-login-desktop.png` (408.845 byte) — trang login Sett prod 5221, desktop
  1366×900; form đầy đủ `#txt_account`, `#txt_password`, `#login_button`.
- `sett-B-login-mobile390.png` (47.174 byte) — cùng trang ở 390×844; card bị bóp
  (bug B2).
- `sett-B-settlement-mobile390-redirect-login.png` (47.174 byte) — mở
  `/HospitalizationManage/Settlement/Index` trên mobile khi chưa login → redirect
  về `/Login/Index` (bằng chứng auth gate trên mobile; **không phải** ảnh
  Settlement thật — xem §7.2).

Các file `sweep-c-*` và `sweepE-*` trong cùng thư mục là của worker khác,
không thuộc SWEEP-B, không đụng tới.

## 10. Dọn dẹp và trạng thái cuối

- Sett-API staging 17002 (PID 18080) **đã kill**; `ss` xác nhận cổng 17002 và
  15221 đã đóng. Tiến trình prod Sett Web 9208 (5221) và Sett-API 9382 (7002)
  **không bị đụng** (chỉ đọc). SSO staging 15208 / BaseAPI staging 17001 để nguyên
  đang chạy như lúc nhận việc.
- **Không sửa code, không commit.** `git status` cuối: chỉ có `M roles-shots-progress.md`
  (đã dirty từ trước khi nhận việc, thuộc điều phối viên — không sửa) và thư mục
  untracked `scripts/sweep-final/` (chứa các báo cáo đợt sweep, trong đó có file này).
- **Không ghi DB demo:** mọi ghi trong phiên này (fixture lịch 11597, 2 dòng
  `mz_gh` 2610285970001/002) đều nằm trong DB `*_STG`; bằng chứng demo = 0 dòng
  ở §4.2. Trên prod chỉ có 1 POST validation rỗng tới 7002 (bị chặn ở cổng,
  không chạm DB) và các GET/redirect đọc.
- File báo cáo này: `~/workspace/openhis-core/scripts/sweep-final/sweep-B-REPORT.md`.
