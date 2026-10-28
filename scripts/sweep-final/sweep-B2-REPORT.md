# BÁO CÁO SWEEP-B2 — Sett Web 5221/15221 post-login (follow-up của SWEEP-B)

- **Worker:** SWEEP-B2 (Sett Web post-login), đợt sweep cuối OpenHIS.
- **Thời gian:** 2026-10-28 (giờ ICT; các mốc UTC ghi rõ khi cần).
- **Base:** V2.2 HEAD `e66c7bda` tại `~/workspace/openhis-core` (xác nhận trước và
  sau khi làm việc; không đổi). Prod đã redeploy 23/23 app (theo brief).
- **Nguyên tắc tuân thủ:** KHÔNG sửa code, KHÔNG commit, KHÔNG sửa
  `roles-shots-progress.md` (chỉ đọc để đối chiếu mã SETT-xx). Trên PROD chỉ kiểm
  tra đọc/không cookie (xem §3 — **không login prod lần nào**). Mọi kiểm thử ghi
  đều qua STAGING (cổng 15221, DB `*_STG`). Proxy env UNSET cho mọi lệnh loopback,
  `DOTNET_SYSTEM_NET_DISABLEIPV6=true` cho build/launcher. Mật khẩu SA giữ nguyên,
  chỉ đọc qua env file lúc chạy, không ghi vào báo cáo. Mật khẩu demo dùng duy
  nhất: username `000000` / mật khẩu `000000` (theo bổ sung của điều phối viên).
  **Số lần login sai mật khẩu trong phiên này: 0.** Lần login staging thất bại
  duy nhất (§3) là do SQL Server sập, không phải sai mật khẩu (BaseAPI chưa từng
  chạy tới bước kiểm tra mật khẩu), và đã được chẩn đoán trước khi thử lại.
- **Kết quả tổng quan:** Checklist §7–§8 của SWEEP-B **đã chạy được phần lớn**:
  login staging thành công, 27/27 route đại diện render sau login (curl có cookie),
  12 trang tương tác trình duyệt, ảnh SETT-17 thật (desktop + mobile 390×844).
  **Không có lần ghi nào qua giao diện Sett Web hoàn thành** — không phải do thiếu
  session, mà do các đường ghi của Sett Web trong bản port này phần lớn là stub
  (xem §8: Area Submit/Delete trả lỗi stub tiếng Việt sạch; tra cứu quyết toán
  nội trú là stub; lưới SettQuery lỗi null). Đây là kết luận kiểm chứng, không
  phải "chưa test". Sort có dữ liệu và ghi lâm sàng qua UI **chưa kiểm chứng
  được** (lý do ở §10). Bug mới: B3, B4 (xem §7); B1, B2 **vẫn còn** (xem §6).

## 1. Binary staging — đã giải quyết (không cần checkout, chỉ rebuild)

- Launcher staging `run-sett-stg.sh` nạp DLL từ worktree staging-only
  `~/workspace/openhis-stg/Newtouch.Sett.Web/bin/Release/net6.0/Newtouch.Sett.Web.dll`
  (port hard-code `15221` trong `Program.cs` của worktree này — vì `UseUrls` thắng
  `ASPNETCORE_URLS`, nên **không dùng chung bin với prod**; prod PID 9208 có cwd
  là `openhis-core/Newtouch.Sett.Web`, tức bin prod đã redeploy từ HEAD).
- Kiểm chứng trước build: source Sett Web trong worktree staging **đã giống hệt
  HEAD `e66c7bda`** (diff chỉ khác `Program.cs` port 15221 và `appsettings.json`
  — phần Redis staging), kể cả `Views/Login/Index.cshtml` và
  `Controllers/ControllerBase.cs`. SWEEP-B ghi "binary ở commit cũ 0a730b1b" là
  đúng về **HEAD của repo staging**, nhưng **không đúng về nội dung DLL**:
- Đã build lại từ source staging (= HEAD) trong worktree staging, 1 build tại một
  thời điểm, proxy UNSET: `dotnet build -c Release --no-incremental` →
  **0 Error, 220 Warning** (chủ yếu MSB3277/CA2200/MVC1000 có sẵn).
- **MD5 của DLL sau rebuild giống hệt trước rebuild**
  (`7a37b62d2a4ab99f296a5d74cd60d7df`; build có tính xác định) — chứng tỏ DLL
  staging trước đó **vốn đã tương đương HEAD**, chỉ khác prod ở literal port.
  Kết luận: staging chạy đúng code HEAD sau bước này, không còn nghi ngờ binary cũ.
- Không sửa code prod, không commit gì trong cả 2 repo. Worktree staging giữ
  nguyên các sửa local khác của nó (chỉ rebuild Sett Web).

## 2. Sự cố hạ tầng giữa phiên — SQL Server sập, đã khôi phục (chỉ phần SQL)

- Triệu chứng đầu tiên: login staging Sett lần 1 thất bại với thông báo chung
  tiếng Trung (xem §3). Chẩn đoán trước khi thử lại: log BaseAPI staging 17001
  cho thấy mọi `CheckPwd` đều lỗi `Could not open a connection to SQL Server`
  (error 40, `ConfigId=BaseDb`); `ss` xác nhận **cổng 1433 không còn LISTEN,
  không còn tiến trình `sqlservr`**; errorlog SQL dừng ở 07:37 UTC (có vài lần
  login `sa` thất bại 18456 lúc 07:32–07:37 UTC từ worker khác — không liên quan
  tới phiên này, không đụng mật khẩu SA). Cùng thời điểm, log BaseAPI staging còn
  cho thấy worker khác cũng đang gọi staging và lỗi DB tương tự — đây là sự cố
  hạ tầng chung, không phải lỗi Sett.
- Khôi phục: **chỉ chạy đúng phần SQL của `recovery.sh`** (không chạy toàn bộ
  recovery — tránh restart các app của worker khác): symlink `/var/opt/mssql`
  vẫn nguyên; khởi động `sqlservr` từ `~/workspace/mssql-native` với env
  `.sqlserver.env`; cổng 1433 LISTEN sau ~20s (PID 29936, vẫn chạy cuối phiên).
  **Flag cho điều phối viên:** SQL đã chết không rõ nguyên nhân sau 07:37 UTC;
  watchdog nên để mắt tới 1433 (lần chết này làm sập mọi login staging/prod cho
  tới khi khôi phục).
- Sau khôi phục, kiểm chứng chỉ đọc bằng `scripts/staging/sql.sh`:
  14 DB `*_STG` đầy đủ; fixture của SWEEP-B **còn nguyên**: lịch
  `mz_ghpb_schedule.ScheduId=11597`, `OutDate=2026-10-28` (hôm nay), khoa KS04,
  `IsBook=1`, `CreatorCode=SWEEP-B`; 2 dòng `mz_gh` 2610285970001/002 còn đủ
  (COUNT=2); tài khoản STG `000000` có `Locked=0, zt=1` — lần login thất bại
  **không ghi strike nào** (CheckPwd chưa từng chạy tới bước so mật khẩu).
  Fixture được **để lại nguyên** theo brief (worker khác còn cần) — không xóa.

## 3. Login — staging thành công; prod KHÔNG login (tuân thủ thông báo khẩn)

- Lần 1 (staging Sett 15221, qua Playwright/Firefox): thất bại, `.tips_msg` =
  `登录失败，请联系管理员` (thông báo chung, không nói sai mật khẩu). Nguyên nhân
  là §2 (SQL sập). **Đã dừng để chẩn đoán, không thử lại ngay.**
- Lần 2 (sau khi SQL lên lại, cùng mật khẩu đúng): **thành công ngay**,
  redirect về `/Home/Index`, trang chủ Sett thật render (xem §4). Login lại bằng
  curl `POST /Login/CheckLogin` (password = md5(000000) tính bằng `md5sum`) cũng
  trả `{"state":"success","message":"Đăng nhập thành công"}`; cookie session là
  `Sett_newtouch_loginuserkey_2016` + `Sett_CookieKey_CurUserCode=000000` —
  **AppId session là `Sett` đúng**, bất chấp view login hard-code `MRQC` (xem §6,
  B1 đã thu hẹp phạm vi ảnh hưởng).
- Giữa phiên, điều phối viên gửi thông báo khẩn: tài khoản demo **prod** từng bị
  khóa do 1 worker khác đoán mật khẩu, đã mở khóa và kiểm chứng login prod thành
  công; quy tắc sắt: chỉ dùng 000000/000000, login nào báo khóa/sai thì dừng ngay
  đường đó, không thử lại, không đoán. **Tuân thủ:** phiên này **không login
  prod lần nào** (mọi login đều là staging; mọi lần login staging sau lần 1 đều
  thành công với mật khẩu đúng). Kiểm chứng prod chỉ ở mức không cookie:
  cuối phiên, prod 5221 `/Login/Index` = 200, `/HospitalizationManage/Settlement/Index`
  không cookie = 302 về `/Login/Index` — prod nguyên vẹn, không bị đụng.
  (Hệ quả: phần "prod chỉ đọc" trong checklist SWEEP-B §8 được hiểu là: render
  sau login trên prod **chưa** kiểm chứng trực tiếp — staging là bằng chứng thay
  thế, vì cùng code HEAD đã redeploy; ghi rõ, không phóng đại.)

## 4. Render sau login — 27/27 route (curl có cookie, staging 15221)

Tất cả đều không còn redirect về login (khác hẳn 302 của SWEEP-B). Bảng ghi
HTTP status thật + thời gian; "200" ở đây nghĩa là server trả HTML/JSON, chưa
phải "chức năng đã đúng" (xem §5 cho kiểm chứng trình duyệt).

| # | Area / trang | Route | Kết quả |
|---|---|---|---|
| 1 | BusinessManage | /BusinessManage/OutPatient/OutpatientReg | 200, 14.449 byte, 1,21s |
| 2 | DRGManage | /DRGManage/DRGUpload/Index | 200, 13.204 byte |
| 3 | DaySettleManage | /DaySettleManage/DaySettle/DzrIndex | 200, 7.914 byte |
| 4 | DeanInquiryManage | /DeanInquiryManage/BusinessRanking/Index | 200, 16.405 byte |
| 5 | ExampleManage | /ExampleManage/BarCode/Index | 200, 6.496 byte |
| 6 | FeeConfirmManage | /FeeConfirmManage/FeeConfirm/Index | 200, 14.500 byte |
| 7 | HighchartsManage | /HighchartsManage/Highcharts/Sample1 | 200, 2.694 byte |
| 8 | HospitalizationManage | /HospitalizationManage/Settlement/Index | 200, 17.884 byte |
| 9 | InsuranceManage | /InsuranceManage/CommercialInsurance/Index | 200, 10.035 byte |
| 10 | IssueTreatRecord | /IssueTreatRecord/ComfirmIssue/Index | 200, 11.419 byte |
| 11 | JGManage | /JGManage/JGUpload/Index | 200, 13.204 byte |
| 12 | KPI | /KPI/MedicalOrg/Index | 200, 13.443 byte |
| 13 | KnowledgeBaseManage | /KnowledgeBaseManage/MedicalInsurance/Index | 200, 9.630 byte |
| 14 | NonTreatmentItemManage | /NonTreatmentItemManage/NonTreatmentItem/Bookkeeping | 200, 30.831 byte, **29,3s** (chậm) |
| 15 | OutpatientManage | /OutpatientManage/OutpatientReg/OutPatientAppointment | 200, 9.560 byte |
| 16 | PatientManage | /PatientManage/PatientInHosList/Index | 200, 6.919 byte |
| 17 | PayManage | /PayManage/MicroPay/MicroPay | 200, 7.391 byte |
| 18 | ReportManage | /ReportManage/Report/ChargeDailysettlement | 200, 8.656 byte, **29,1s** (chậm) |
| 19 | SiteManage | /SiteManage/SiteStatistics/Index | 200, 16.288 byte |
| 20 | SystemManage | /SystemManage/Area/Index | 200, 8.340 byte |
| 21 | SystemSecurity | /SystemSecurity/ServerMonitoring/Index | 200, 17.253 byte |
| 22 | TherapeutistManage | /TherapeutistManage/TherapistAdvice/Index | 200, 31.928 byte |
| + | SETT-17 HospSettlement | /HospitalizationManage/Settlement/Index | 200 (lặp #8) |
| + | SETT-17 OutpSettQuery | /OutpatientManage/Settlement/SettQuery | 200, 10.786 byte |
| + | SETT-03 OutpatCharge | /OutpatientManage/OutpatCharge/Index2018 | xem ghi chú chậm/không ổn định dưới đây |
| + | SETT-03 BookkeepInHos | /HospitalizationManage/BookkeepInHos/Accounting | xem ghi chú dưới đây |
| + | Home | /Home/Index | 200, title `Sett — Hệ thống quản lý thu phí` |

- **Chậm/không ổn định (ghi đúng, không kết luận vội thành bug code):**
  `Bookkeeping` và `ChargeDailysettlement` đều ~29s (sát ngưỡng timeout 30s) —
  trang nặng/chậm thật trên STG. `Index2018` và `Accounting` lần đầu đều timeout
  30s (khi SQL vừa lên lại); `Index2018` thử lại với timeout 150s trả 200 sau
  **41,9s nhưng body là JSON lỗi DB** (`Could not open a connection...`,
  `ConfigId=BaseDb`, stack `OutPatientSettleDmnService.GetOrgXzqh` → retry kết nối
  ~14s × 3); thử lại vài phút sau: **200, 166.558 byte, 0,61s, trang đầy đủ**;
  kiểm chứng cuối phiên: `Index2018` 200 sau 7,6s, `Accounting` 200 sau 2,0s,
  `Settlement` 200 sau 0,48s. Diễn giải: có một giai đoạn không ổn định sau khi
  SQL khởi động lại (kết nối BaseDb của tiến trình Sett staging chập chờn rồi tự
  hết), **không tái hiện được ở cuối phiên nên không kết luận là bug code** —
  vòng fix/hạ tầng nên để mắt nếu hiện tượng lặp lại sau mỗi lần SQL restart.
  Cũng chính các trang chậm/timeout này là nguyên nhân khả dĩ khiến probe
  Playwright đầu phiên chạy >11 phút (mỗi trang timeout 60s cộng dồn) — không
  phải treo vĩnh viễn.

## 5. Tương tác trình duyệt sau login (Playwright + Firefox local, staging)

Phương pháp: mỗi trang mở bằng page mới trong cùng context đã login (page crash
thì thử lại 1 lần với page mới — đã gặp crash content-process Firefox 1 lần ở
vòng thử đầu, do máy thiếu RAM khi nhiều worker chạy song song; vòng sau chạy
ổn). "Số dòng" trong log thô đếm cả dòng cấu trúc bảng nên **không dùng làm
số liệu dữ liệu**; kết luận dựa vào nội dung text + hành vi nút.

| Trang | Lưới/filter/sort đã chạy | Kết quả |
|---|---|---|
| SystemManage/Area/Index | Lưới jqGrid render, nút Tạo mới/Sửa/Xóa hiện; tìm kiếm theo `txt_keyword` (không có nút "Tra cứu" text) | Lưới **rỗng**: `Không tìm thấy dữ liệu liên quan!` — đúng vì `GetTreeGridJson` trả null và `AreaApp.GetList()` trả rỗng (xem §8). Sort: click header đầu (cột checkbox rỗng) không đổi gì quan sát được — **sort chưa kiểm chứng được** (xem §10). |
| HospitalizationManage/Settlement/Index | Form tra cứu (Số thẻ/Mã nhập viện), lưới lịch sử nộp tiền, nút F4:Xóa, F8:Quyết toán | Render đầy đủ tiếng Việt (ảnh §9). Tra cứu theo thẻ: endpoint là stub (xem §8). Không bấm F8 (sẽ ghi lâm sàng — chưa đủ fixture, xem §10). |
| OutpatientManage/Settlement/SettQuery | Lưới + nút Tra cứu + F9:XuấtExcel; nhập `ZZZ-KHONG-CO` + Tra cứu | Chạy không crash; nhưng endpoint lưới lỗi null (B3, §7) nên lưới luôn rỗng — UI hiển thị như "không có dữ liệu" (gây hiểu lầm). |
| PatientManage/PatientInHosList/Index | Ô `txt_keyword` + nút `#btn_search` (icon-only); nhập `ZZZ-KHONG-CO` + tìm | Request gửi đúng `LoadData?...&keyword=ZZZ-KHONG-CO&rows=15&page=1&sidx=ryrq desc`; server trả `[]` nhanh (0,016s); lưới rỗng cả trước/sau (`Không tìm thấy dữ liệu liên quan!`, pager `Trang  trên tổng số 1 trang` — số trang trống, quirk nhỏ). Kết luận lọc-keyword của SWEEP-B (VO không có field) **không bác bỏ được/khẳng định thêm được bằng dữ liệu rỗng** — chỉ khẳng định được vòng request/response chạy sạch. |
| DaySettleManage/DaySettle/DzrIndex | Nhập ngày + Tra cứu | **SETT-06 đã kiểm chứng live sau login:** bấm Tra cứu hiện đúng cảnh báo tiếng Việt `Chưa cấu hình máy chủ báo cáo (ReportUrl) nên không hiển thị được báo cáo đối chiếu sổ sách. Vui lòng cấu hình ReportUrl / ReportSystemCode rồi tải lại trang.` và không nhúng iframe — đúng hành vi phase 2, không còn tự nhúng chính trang. |
| DeanInquiryManage/BusinessRanking/Index | Lưới xếp hạng + Tra cứu | Render, có dữ liệu tiêu đề các bảng xếp hạng; 1 bảng báo `Không tìm thấy dữ liệu liên quan!` (STG rỗng). Title trang sai (B4, §7). |
| OutpatientManage/OutpatientReg/OutPatientAppointment | Lưới lịch hẹn | Render, rỗng (`Không tìm thấy dữ liệu liên quan!`, pager `Trang  trên tổng số  trang`). |
| PayManage/MicroPay/MicroPayQuery | Lọc ngày + Tra cứu | Render, title `Giao dịch thanh toán quét mã`, lưới có cột `Tài khoản Alipay` (chuỗi vendor còn lại — chỉ ghi nhận). Không thử thanh toán/hoàn tiền thật (cần cổng thanh toán). |
| SystemSecurity/ServerMonitoring/Index | Trang giám sát | `window.echarts` = true — **SETT-08 đã kiểm chứng live** (thư viện biểu đồ nạp được); trang hiện `Giám sát thời gian thực`, `Tỷ lệ sử dụng IIS/CPU/RAM`, bảng thông tin máy chủ. |
| ExampleManage/BarCode/Index | Nhập chuỗi + bấm Tạo | **SETT-04 HOẠT ĐỘNG:** plugin `jquery.qrcode` nạp được từ static host (cả 2 file đều 200); bấm Tạo với `SWEEP-B2-TEST` render lại bảng QR **25×25 ô** (plugin này render bằng `<table>`, không phải img/canvas — lần đo đầu của phiên này chỉ đếm img/canvas nên đã kết luận sai, đã sửa bằng lần đo đúng: `qrcodeTable` có table 25 hàng × 25 cột, html thay đổi theo nội dung nhập). Ảnh QR lưu ở §9. |
| OutpatientManage/OutpatCharge/Index2018 | Mở trang | Trang render sau các lần thử chậm (§4); selector khoản thu phí vẫn 404 (SETT-03, §8). Không thử lưu thu phí (cần chọn khoản thu — selector hỏng; xem §10). |
| BusinessManage/OutPatient/OutpatientReg | Form đăng ký khám | Render đầy đủ: các ô Số thẻ/Số hóa đơn/Họ tên/Giới tính/..., nút `Quẹt thẻ`, `Đăng ký khám không cần thẻ`, `F4:Hủy số`, `F5:Xóa`, `F8:Lưu`, `F12:Thoát`. **Không bấm Lưu** (ghi lâm sàng, cần fixture khoản thu + BN; xem §10). |

- **Sort — kết luận chung:** chưa kiểm chứng được ở trang nào. Lý do kép:
  (a) header đầu tiên của jqGrid là cột checkbox (text rỗng) nên click generic
  không phải click sort; (b) các lưới STG đều rỗng nên không có thứ tự để quan
  sát thay đổi. Không báo "sort đã xong" — cần lưới có dữ liệu + click đúng
  header cột dữ liệu ở vòng sau.
- **Lỗi trình duyệt nền (không phải lỗi Sett):** 2 lần login Playwright timeout
  30s ở cuối phiên dù server vẫn trả login 200 trong 2,5s qua curl — nguyên nhân
  là trang login nạp jQuery từ static host chậm khi máy tải nặng, nút login chưa
  kịp bind handler đã bị click. Khắc phục: chờ `window.jQuery` defined trước khi
  điền form thì login lại thành công ngay. Ghi nhận để các worker khác khỏi
  tưởng là lỗi login: **timeout điều hướng ≠ sai mật khẩu, không được tính là
  strike và không được "thử mật khẩu khác"**.

## 6. Kiểm chứng lại B1, B2 của SWEEP-B (sau login)

### B1 — Trang login Sett mang title + appId của MRQC: VẪN CÒN (chưa sửa)

- Title tab vẫn `MRQC Hệ thống kiểm soát chất lượng` (cả curl lẫn Playwright,
  cả desktop lẫn mobile); source HEAD vẫn hard-code dòng 15
  `<title>@("MRQC Hệ thống kiểm soát chất lượng")</title>` và dòng 159
  `var appId = '@("MRQC")';` trong `Views/Login/Index.cshtml`.
- **Thu hẹp phạm vi ảnh hưởng bằng bằng chứng mới:** session cookie thực tế là
  `Sett_newtouch_loginuserkey_2016` (AppId lấy từ config `AppId: "Sett"`), nên
  appId hard-code `MRQC` trong view **chỉ** ảnh hưởng tới tên cookie lỗi đăng
  nhập (`MRQC_Newtouch_login_error`) và lệnh xóa cookie `MRQC_...` khi mở trang
  login — không làm sai session Sett. Vòng fix vẫn nên đổi sang Sett cho đúng,
  nhưng không còn lo "sai AppId gây sai session" như SWEEP-B e ngại.
- Trang sau login (Home) có title đúng `Sett — Hệ thống quản lý thu phí`.

### B2 — Trang login mobile bị bóp: VẪN CÒN (cơ chế đã xác định rõ)

- Đo được ở viewport 390×844 (emulation): `body.style.transform =
  scale(0.711458)`, `body.style.width = 548.17px`, trong khi
  `window.screen.width = 1366` (màn hình host — emulation viewport không đổi
  screen.width) và `window.innerWidth = 390`. Cơ chế nằm ngay trong view login:
  hàm `resize()` tính `s = window.screen.width / 1920`, rồi
  `body.style.transform = scale(s,s)` + `body.style.width = innerWidth / s`.
  Ảnh login mobile mới (§9) cho thấy card vẫn đọc được ở mức 71% nhưng nền bị
  cắt và có dải đen bên phải, footer copyright tràn.
- **Suy ra từ code (không phải đo trên điện thoại thật):** trên điện thoại thật
  có `screen.width = 390`, `s = 390/1920 ≈ 0,203` — toàn bộ layout 1920px sẽ bị
  thu về 20%, tệ hơn nhiều so với ảnh emulation. Vòng fix nên bỏ/sửa `resize()`
  này thay vì chỉnh CSS lặt vặt.
- **Trang sau login có bị bóp tương tự không? KHÔNG theo cơ chế này:**
  grep toàn bộ Sett Web chỉ thấy `screen.width / 1920` ở đúng
  `Views/Login/Index.cshtml`; các trang sau login có `body.style.transform = ""`.
  Ảnh Settlement mobile thật (§9) cho thấy form xếp dọc, chữ cỡ bình thường,
  dùng được — chỉ các bảng rộng bị tràn ngang, cần cuộn ngang (thiết kế desktop
  cố định, không phải bóp nhỏ). Đây là kết luận SETT-17 mobile (xem §9).

## 7. Bug / quan sát mới (chỉ ghi nhận, không sửa)

### B3 (mới, mức trung bình) — Lưới SettQuery lỗi null trên mọi lần tải

- **Trang:** `/OutpatientManage/Settlement/SettQuery` → endpoint
  `GetSettlementGridJson` (POST, có/không keyword, có/không tham số jqGrid
  `page/rows/sidx/sord` đều lỗi như nhau, 200 với body JSON error, ~0,01–0,05s).
- **Mong đợi:** trả `{rows, total, page, records}` (rỗng nếu không có dữ liệu).
- **Thực tế:** `{"state":"error","message":"Value cannot be null. (Parameter
  'source')"}`; stack: `SettlementController.GetSettlementGridJson` dòng 55
  `list.Select(...)` trong khi `list` null.
- **Nguyên nhân gốc (source):** service `GetSettlementList` trong file stub trả
  `return null` thay vì danh sách rỗng; controller không null-guard. Hệ quả:
  SettQuery **không bao giờ hiển thị được dữ liệu**, kể cả khi có quyết toán
  thật; UI jqGrid chỉ nuốt lỗi thành lưới rỗng nên nhìn qua tưởng "không có dữ
  liệu". Vòng fix: service trả list rỗng hoặc controller guard null (1 trong 2
  là đủ cho trường hợp rỗng; trường hợp có dữ liệu vẫn cần service thật).

### B4 (mới, mức thấp) — Title trang BusinessRanking sai nội dung

- **Trang:** `/DeanInquiryManage/BusinessRanking/Index`; browser tab + curl đều
  cho title `Theo dõi thuốc` trong khi nội dung là xếp hạng khối lượng/thu nhập
  bác sĩ và khoa.
- **Nguyên nhân (source):** `Index.cshtml` dòng 2 hard-code
  `ViewBag.Title = "Theo dõi thuốc";` (copy-paste từ trang theo dõi thuốc).
  Chỉ sai nhãn, không ảnh hưởng dữ liệu.

### Các khoảng trống chức năng đã kiểm chứng bằng endpoint (không phải crash)

- **Tra cứu quyết toán nội trú (Settlement lookup) chưa triển khai:**
  `POST /HospitalizationManage/Settlement/GetStatusDetail` với `kh=10000` trả
  200 + JSON lỗi tiếng Việt sạch: `Tra cứu trạng thái quyết toán nhập viện chưa
  được chuyển đổi trong giai đoạn này.` (exception từ
  `HospSettApp.GetPatHospStatusDetail`). Nút tìm kiếm trên form Settlement vì
  vậy không thể nạp thông tin BN nội trú — hiển thị thông báo này qua UI chưa
  được kiểm chứng bằng trình duyệt trong phiên này (chỉ mức endpoint), ghi rõ.
- **Ghi danh mục Area chưa triển khai (by-design của bản port):**
  `POST /SystemManage/Area/SubmitForm` (đủ session + antiforgery token hợp lệ)
  và `POST /SystemManage/Area/DeleteForm` (keyValue dummy) đều trả 200 + JSON lỗi
  tiếng Việt sạch: `Chức năng ghi/chỉnh sửa này chưa được triển khai trong bản
  .NET Core (chỉ xác minh đọc).` (stack `AreaApp.SubmitForm/DeleteForm` trong
  `Stubs_SystemManage.cs`); `GetTreeSelectJson` sau đó vẫn `[]` — **không ghi
  gì**. Nút "Tạo mới khu vực" trên UI standalone còn bị chặn sớm hơn bởi lỗi
  frame-context: `can't access property "open", top.layer is undefined`
  (modal dùng `top.layer` chỉ có trong portal frame SSO) — kể cả trong frame,
  submit vẫn sẽ gặp lỗi stub trên.
- **SETT-03 vẫn đúng là thiếu (tái khẳng định sau login):**
  `GET /SystemManage/BaseData/SelectSfxmYp?keyword=test&mzzybz=1&dllb=1,2` với
  session hợp lệ → **404** (0,005s), không có `BaseDataController` trong source.
  Selector khoản thu phí trên `OutpatCharge/Index2018` và
  `BookkeepInHos/Accounting` vì vậy không lấy được dữ liệu tìm kiếm.
- **SETT-06 vẫn đúng là chưa có máy chủ báo cáo** (xác nhận live sau login, §5).
- **PatientInHosList lọc keyword:** request gửi đúng keyword, server trả `[]`;
  source `PatientListInputVO` rỗng (SWEEP-B) vẫn đúng — với dữ liệu rỗng thì
  không phân biệt được "lọc bị bỏ qua" và "không có dữ liệu khớp"; cần BN nội
  trú có dữ liệu trên STG ở vòng sau để kết luận dứt điểm.
- **Quan sát nhỏ khác (không xếp bug):** pager ở PatientInHosList và
  OutPatientAppointment hiển thị `Trang  trên tổng số ... trang` (số trang trống);
  tiêu đề cột SettQuery bị cắt ở desktop (`Phương thức t...`, `Thanh toán tài
  khoản bổ sung công...` — lưới rộng hơn viewport, cần cuộn ngang); nhãn form
  Settlement dính chữ (`Thông tinquyết toán`, `Loạquyết toán`, `Số tiềnquyết
  toán` — cùng họ với SETT-11 cột nhãn hẹp); ô ngày SettQuery bị icon lịch
  che một phần; cột `Tài khoản Alipay` còn lại trên MicroPayQuery (vendor).

## 8. Verdict thêm/sửa/xóa/lấy/lọc/tìm kiếm/sắp xếp (trung thực theo bằng chứng)

- **Lấy (GET trang + dữ liệu):** ĐẠT ở mức render — 27/27 route trả 200 sau
  login; các endpoint dữ liệu đã thử (LoadData, GetSettlementGridJson,
  GetStatusDetail, GetTreeSelectJson, SelectSfxmYp) đều phản hồi nhanh, kể cả
  khi phản hồi là lỗi có chủ đích (stub/404/null) — không có endpoint nào treo
  (trừ giai đoạn không ổn định sau SQL restart, §4).
- **Lọc/tìm kiếm:** CHẠY ĐƯỢC ở các trang có endpoint thật (SettQuery,
  PatientInHosList, DzrIndex, MicroPayQuery, BusinessRanking đều thực thi tìm
  kiếm không crash); **không kiểm chứng được tính đúng của lọc trên dữ liệu
  thật** vì các lưới STG đều rỗng (trừ trường hợp SettQuery lỗi B3 làm lọc vô
  nghĩa). Lọc keyword PatientInHosList gửi đúng tham số nhưng chưa chứng minh
  được server có dùng nó không.
- **Sắp xếp:** CHƯA KIỂM CHỨNG (lý do §5).
- **Thêm/sửa/xóa:** **KHÔNG HOÀN THÀNH qua Sett Web** trong phạm vi này, với
  lý do kiến trúc chứ không phải do thiếu session: quét source cho thấy
  **184 vị trí `NotSupportedException`** trong 14 file stub của
  `Newtouch.Sett.Infrastructure/DomainServices/` (HosStubs, OutStubs,
  RealServices, Stubs_SystemManage, Stubs_DaySettleManage, Stubs_InsuranceManage,
  Stubs_KPI, Stubs_KnowledgeBaseManage, Stubs_NonTreatmentItemManage,
  Stubs_PatientManage, Stubs_ReportManage, Stubs_SiteManage,
  Stubs_SystemSecurity, Stubs_TherapeutistManage) với thông điệp chung "chỉ xác
  minh đọc". Đã kiểm chứng trực tiếp 2 đường (Area Submit/Delete) đều stub.
  Đường ghi lâm sàng duy nhất đã chứng minh thành công trong đợt sweep vẫn là
  **Sett-API Y026 của SWEEP-B** (không phải Sett Web). Vòng sau muốn có 1 lần
  ghi UI dương tính thì phải chọn đường ghi đã triển khai thật (cần rà soát
  RealServices trước, không đoán), kèm fixture khoản thu (vướng SETT-03) và BN
  — không tự bịa dữ liệu lâm sàng để ép ghi.

## 9. Ảnh đã chụp (Playwright + Firefox local, staging 15221 — ảnh thật sau login)

Lưu tại `~/workspace/openhis-core/shots-sweep/` (đã gitignore theo quy tắc
`shots*/` — kiểm chứng bằng `git check-ignore`; ảnh không lọt vào commit).
Các ảnh `sett-B2-*` dưới đây đều là ảnh **sau login thật**, thay thế các ảnh
redirect-login của SWEEP-B (file `sett-B-settlement-mobile390-redirect-login.png`
cũ vẫn còn đó, không xóa để giữ lịch sử đối chiếu):

- `sett-B2-settlement-desktop.png` — **SETT-17 desktop thật:**
  `/HospitalizationManage/Settlement/Index` sau login: ô tìm `Vui lòng nhập số
  thẻ/Mã nhập viện`, form `Thông tin bệnh nhân nhập viện` (Số thẻ, Mã nhập viện,
  Số bệnh án, Họ tên, Giới tính, Khoa nhập viện, Loại bệnh nhân, tạm ứng, BHYT
  số chứng nhận, Ngày nhập/xuất viện), `Thông tin quyết toán`, `Lịch sử nộp tiền
  của bệnh nhân`, `Thông tin chứng từ`, nút `F4:Xóa`, `F8:Quyết toán`.
- `sett-B2-settlement-mobile390.png` — **SETT-17 mobile thật (390×844):** cùng
  trang, form xếp dọc, chữ cỡ bình thường, dùng được; bảng lịch sử/chứng từ tràn
  ngang (cần cuộn ngang), không bị bóp nhỏ kiểu login.
- `sett-B2-settquery-desktop.png` — `/OutpatientManage/Settlement/SettQuery`
  sau login: điều kiện lọc (Từ khóa, Ngày quyết toán mặc định 2026-10-28,
  checkbox `Bao gồm hoàn phí`, nút `Tra cứu`), tiêu đề cột lưới, dòng đỏ
  `Không tìm thấy dữ liệu liên quan!` (kết quả rỗng — xem B3: rỗng này là do lỗi
  endpoint, không phải do thực sự không có dữ liệu), pager, `F9:XuấtExcel`.
- `sett-B2-settquery-mobile390.png` — cùng trang SettQuery ở mobile 390×844.
- `sett-B2-home-desktop.png` / `sett-B2-home-mobile390.png` — `/Home/Index` sau
  login: `Sett — Hệ thống quản lý thu phí` + các nhóm link đúng area (thu phí/
  quyết toán, bệnh nhân, báo cáo/DRG/BHYT, hệ thống).
- `sett-B2-login-mobile390.png` — trang login ở mobile 390×844 (B2): card
  `Chào mừng` đọc được ở mức 71% nhưng nền cắt + dải đen phải + footer tràn.
- `sett-B2-qrcode.png` — vùng `#qrcodeTable` sau khi bấm Tạo với
  `SWEEP-B2-TEST`: bảng QR 25×25 (bằng chứng SETT-04 hoạt động).
- Lưu ý: 2 file `sett-B2-settlement-desktop.png` và `sett-B2-settquery-desktop.png`
  từng bị ghi đè 1 lần ở vòng probe đầu (khi login còn thất bại — lúc đó chúng
  là ảnh trang login); bản cuối cùng đã được chụp lại sau login thành công và
  đã xem trực tiếp để xác nhận nội dung là trang Settlement/SettQuery thật.

## 10. Phần không test được + lý do (đầy đủ, không giấu)

1. **Render sau login trên prod:** không login prod (tuân thủ thông báo khẩn sau
   vụ khóa tài khoản prod — tránh mọi rủi ro khóa lại; staging cùng code HEAD là
   bằng chứng thay thế; prod chỉ kiểm tra không cookie cuối phiên).
2. **Sort có dữ liệu:** lưới STG rỗng + header generic là cột checkbox (§5).
3. **Ghi lâm sàng qua UI (OutpatientReg Lưu, Settlement F8, thu phí, ghi sổ nội
   trú):** đường ghi Sett Web phần lớn là stub (§8); đường ghi thật (nếu có)
   cần selector khoản thu (SETT-03 hỏng) + fixture BN/khoản thu hợp lệ — không
   gọi mò, không bịa dữ liệu lâm sàng để ép ghi. Ghi duy nhất đã chứng minh
   trong đợt sweep là Y026 qua Sett-API (SWEEP-B).
4. **Lọc đúng trên dữ liệu thật:** các lưới STG rỗng (trừ B3 làm SettQuery vô
   nghĩa); PatientInHosList cần BN nội trú có dữ liệu để phân biệt "lọc bị bỏ
   qua" và "không khớp".
5. **Chức năng cần máy chủ/cổng ngoài:** báo cáo SSRS/ReportUrl (SETT-06),
   thanh toán MicroPay với cổng thật, gửi mail/SMS, upload DRG/JG với file thật,
   bảo hiểm GuiAn — không có máy chủ/cổng đối tác trong môi trường này.
6. **Upload file, in ấn, xuất Excel thật (F9):** chỉ thấy nút, không bấm xuất
   (tránh tải file không kiểm soát + lưới rỗng nên xuất cũng rỗng).
7. **App native:** OpenHIS không có app native — mọi "mobile" trong báo cáo này
   là emulation viewport 390×844 bằng Playwright + Firefox, không bao giờ tuyên
   bố test app native.
8. **Toàn bộ 1.067 action quét source:** chỉ kiểm chứng trang đại diện mỗi area
   + nhóm chức năng ưu tiên (theo checklist SWEEP-B §8), không click hết mọi
   action — báo cáo ghi đúng mức này.
9. **Hiển thị thông báo lỗi stub của Settlement lookup trên UI:** mới kiểm chứng
   ở mức endpoint (§7); lần chạy trình duyệt cho bước này đã crash do máy tải
   nặng trước khi tới bước đó, không chạy lại riêng vì endpoint đã đủ rõ và
   tránh tốn thêm lượt login trình duyệt trong lúc máy đang tải nặng.
10. **Chuỗi CIS→8016→BookingESB qua host staging:** thuộc worker Center/sweep-A;
    phiên này không chạy (gọi 8016 prod sẽ chạm DB demo nên cấm — như SWEEP-B).

## 11. Dọn dẹp và trạng thái cuối

- Sett Web staging 15221 (PID 27255, do phiên này khởi động) **đã kill**;
  `ss` xác nhận 15221 đã đóng. Tiến trình prod Sett Web 9208 (5221), Sett-API
  9382 (7002), SSO staging 5454 (15208), BaseAPI staging 15984 (17001) **không
  bị đụng** (chỉ đọc/gọi qua API của chúng). SQL Server đã khôi phục vẫn chạy
  (PID 29936) — là hạ tầng chung, để lại chạy cho các worker khác.
- **Không sửa code, không commit.** `roles-shots-progress.md` không đụng.
  Fixture SWEEP-B (lịch 11597 + 2 dòng `mz_gh`) để lại nguyên; phiên này
  **không ghi thêm dòng nào vào DB demo lẫn STG** (mọi "ghi" đã thử đều bị
  stub chặn sạch ở tầng service — bằng chứng `GetTreeSelectJson` vẫn `[]`).
- File báo cáo này:
  `~/workspace/openhis-core/scripts/sweep-final/sweep-B2-REPORT.md`.
  Các file `sett-B2-*` trong `shots-sweep/` đã gitignore (kiểm chứng cuối phiên
  bằng `git status` + `git check-ignore` trước khi bàn giao — xem báo cáo gửi
  kèm của worker).
- **Việc còn nợ cho điều phối viên/vòng sau (không tự quyết):**
  (a) quyết định có sửa B3 (null-guard hoặc service trả rỗng) + B4 (title) +
  B1/B2 trong vòng fix hay không; (b) rà soát đường ghi Sett Web thật để có 1
  lần ghi UI dương tính (kèm fixture, sau khi SETT-03 có endpoint);
  (c) sort + lọc trên lưới có dữ liệu; (d) theo dõi SQL 1433 sau sự cố chết
  không rõ nguyên nhân (§2); (e) render sau login trên prod khi tài khoản prod
  đã ổn định trở lại và điều phối viên cho phép login prod có kiểm soát.
