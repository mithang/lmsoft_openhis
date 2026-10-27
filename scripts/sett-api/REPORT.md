# BÁO CÁO — Giai đoạn 3: Sett-API host cổng 7002

> Worker giai đoạn 3, chương trình xây dựng chạy thật OpenHIS.
> KHÔNG commit, KHÔNG push — diff để ở trạng thái chưa commit cho điều phối
> viên soát. KHÔNG sửa `recovery.sh`/cron/watchdog (chỉ chuẩn bị snippet
> ở §9). KHÔNG sửa checkout `~/workspace/lmsoft_openhis` và run copy
> `~/workspace/lmsoft_openhis_run`.
> Mọi mật khẩu/secret/token chỉ dùng transient qua env/config đang chạy,
> KHÔNG ghi vào git, KHÔNG in vào báo cáo này.

## 1. Trạng thái cây lúc bắt đầu và lúc kết thúc

- Bắt đầu: `git status` sạch, tip `379da6b4` ("Giai doan 2: GetToken..."),
  local = remote.
- Kết thúc: tip vẫn `379da6b4`, KHÔNG commit. Diff chỉ gồm file MỚI
  (untracked), không sửa file tracked nào:
  - `Newtouch.HIS.Sett/Newtouch.Sett.API/` (project mới, xem §8).
  - `run-sett-api.sh`, `run-sett-api-stg.sh` (launcher mới, xem §8).
  - `scripts/sett-api/REPORT.md` (file này).
- Build: `dotnet build -c Release` **0 error** (4 warning CS0649 vô hại:
  các field `FeeRelated.zkbl/zkje/ybjslsh/ecToken` không được gán trong
  luồng API — đúng vì luồng API không gửi các trường đó).

## 2. Đối chiếu cây gốc và phía caller 8016

### 2.1. Project Sett API gốc có tồn tại

Checkout chỉ-đọc `~/workspace/lmsoft_openhis` CÓ project gốc
`Newtouch.HIS.Sett/Newtouch.HIS.Sett.API/` (.NET Framework Web API):

- `Controllers/Booking/BookingController.cs` (~1200 dòng, switch Y002–Y030).
- `Controllers/OrderController.cs` (517 dòng, bản có xác thực).
- `Controllers/UnAuthorized/OrderController.cs` (401 dòng).
- `Controllers/PatientApiController.cs` (272 dòng).
- Logic nghiệp vụ: `BookingDmnService`, `OutPatientSettleDmnService`,
  `OrderDmnService`, `DischargeSettleDmnService`, `OutPatChargeDmnService`.

Host mới port đúng 3 nhóm route + contract của project này, không tự
thiết kế lại contract. Nơi service gốc chỉ là stub hoặc nằm ngoài
checkout thì tái cấu trúc từ SQL gốc còn lại + schema DB thật (ghi rõ
từng chỗ, xem §5, §6).

### 2.2. Cơ chế gọi 8016 → host (đã đọc code, không đoán)

- 8016 dùng `IHttpClientHelper.PostAsync<T>(url, data)` overload 2 tham
  số: **không gửi header Authorization/token nào**, chỉ
  `Content-Type: application/json` + `Connection: keep-alive`.
  Body = `apiReq.ToJson()` (Newtonsoft, DefaultContractResolver, ngày
  `yyyy-MM-dd HH:mm:ss`).
- Toàn bộ 3 nhóm route gốc đều `[IgnoreTokenDecrypt]` → host mới
  **không yêu cầu xác thực**, không tự chế scheme mới.
- Host mới **luôn trả HTTP 200 kèm envelope** kể cả FAIL/ERROR: nếu trả
  HTTP lỗi thì `PostAsync` của 8016 trả null và 8016 báo "Tài nguyên
  được yêu cầu không phản hồi".
- `SettHttpUrl` lấy từ config `AppAPIHost.SiteSettAPIHost` của 8016.

### 2.3. Contract

**Request** `BookingRequest`: `{AppId, OrganizeId, methodcode (chữ
thường m), optype, user, Timestamp, paradata}`. `paradata` là object
phẳng (không phải `Dictionary<string,string>` như gốc) nên host đọc qua
JObject, **case-insensitive** (điểm lệch có chủ ý: 8016 gửi key theo tên
property của nó — `ScheduId`, `RegType`, `ysgh`, `ksrq`/`jsrq` — trong
khi code gốc đọc key khác — `scheduId`, `regType`, `ys`,
`outDate`/`FromoutDate` — qua Dictionary case-sensitive nên các filter
đó từng bị bỏ qua lặng lẽ; host map thêm `ksrq` → từ ngày, `jsrq` →
đến ngày, `outDate` → đúng ngày).

**Response** `ResponseBaseOld`: `{code, msg, data, sub_code, sub_msg}`;
code: SUCCESS `10000`, ERROR `20000`, FAIL `40004`. 8016 bọc lại bằng
`BuildResponseBase`: `code = resp.code`, `msg = msg + sub_code +
sub_msg`, `data = resp.data`. Bản phân trang: nếu SUCCESS và `sub_msg`
khác rỗng thì deserialise `Pagination` từ `sub_msg`,
`data = {page, sidx, records, sord, rows, pagedata = resp.data}` — vì
vậy Y009 optype 1 trả `sub_msg` = JSON
`{page, rows, sidx, sord, records = tổng}` và `data` = lát cắt trang
(đã kiểm chứng live, §10).

### 2.4. PHÁT HIỆN 1 — 8016 đang chạy trỏ sang 17361, không phải 7002

8016 chạy với `ASPNETCORE_ENVIRONMENT=Development` (theo
`run-apimanage.sh`), nên `appsettings.Development.json` của nó override
`SiteSettAPIHost` thành `http://localhost:17361/`, trong khi
`appsettings.json` (production) mới là `http://localhost:7002/`.

Bằng chứng live: gọi 8016 `api/Outpatient/OutpatientRegist` lần đầu
trả code `20000` msg `[Ngoại lệ]:Connection refused (localhost:17361)`.

Hệ quả và cách xử lý trong giai đoạn này (không đổi config 8016):

- Host chính vẫn ở **7002** (đúng config production).
- `Program.cs` đọc thêm biến env `SETT_API_URLS` (mặc định 7002) để
  chạy một **instance kiểm chứng tạm thời trên 17361** (cùng code, cùng
  DB staging) chứng minh chuỗi 8016 → host chạy được với config hiệu
  lực hiện tại. Instance 17361 đã **dừng sau kiểm chứng** (xem §10.4).
- **Quyết định cần điều phối viên/user chốt trước khi coi 7002 là
  prod**: hoặc chạy 8016 với `Production` (khi đó 7002 có hiệu lực),
  hoặc duy trì thêm listener 17361. Hiện tại, nếu chỉ để host 7002
  chạy thì chuỗi qua 8016 đang chạy vẫn hỏng ở bước proxy (connection
  refused 17361) — đây là điểm khác biệt giữa "host 7002 xong" và
  "chuỗi 8016 chạy được trong môi trường hiện tại".

### 2.5. PHÁT HIỆN 2 — Y026 qua 8016 không thể đạt 10000 nếu port trung thành tuyệt đối

Cổng Y026 gốc bắt buộc `ks + kh + ghxz`
(`BookingController.cs`, case Y026), và `OutpatReg` gốc cũng yêu cầu
`ks` khác rỗng rồi tra lịch theo `(orgId, ghrq = hôm nay, ks,
RegType = "1")`. Nhưng `OutpatientRegistRequest` của 8016 **không có
trường `ks`** (chỉ có `kh/BookId/ScheduId/ghxz/PayWay/PayFee/PayLsh`),
và `paradata = request.Data` nên `ks` không bao giờ tới được host qua
8016 — kể cả khi host 7002 hoàn hảo.

**Shim tương thích đã áp dụng (điểm lệch có chủ ý, ghi rõ để điều
phối viên quyết)**: trong `CommonRegist`, khi thiếu `ks` nhưng có
`ScheduId`, host tra chính dòng lịch trực đó (lịch hôm nay,
RegType `"1"`) rồi lấy `czks` làm `ks`, sau đó chạy tiếp đúng luồng
gốc. Khi không có cả `ks` lẫn `ScheduId` thì vẫn FAIL như gốc. Đường
`ks` đầy đủ không đổi hành vi. Phương án thay thế (thay đổi 8016 để
thêm `ks` vào `OutpatientRegistRequest`) thuộc về giai đoạn sau và cần
quyết định riêng — shim này không ngăn phương án đó (khi 8016 gửi đủ
`ks`, shim không kích hoạt).

### 2.6. Caller trực tiếp khác của SiteSettAPIHost (grep toàn cây)

Ngoài 8016, các helper sau đọc config `SiteSettAPIHost` (chỉ liệt kê,
chưa phân tích sâu caller trực tiếp của chúng; chuỗi gọi live duy nhất
đã chứng minh là qua 8016):

- PDS: `Newtouch.HIS.PDS/Newtouch.PDS.Infrastructure/SiteSettAPIHelper.cs`
  (mặc định `localhost:17361` khi config rỗng).
- EMR: `Newtouch.HIS.EMR/Newtouch.EMR.Infrastructure/Infrastructure/EmrHelpers.cs:290`.
- CIS: `Newtouch.HIS.CIS/Newtouch.CIS.Infrastructure/Infrastructure/SiteSettAPIHelper.cs`,
  `Compat/InfraCompat.cs:53,70,85`.
- Lib: `NewtouchHIS.Lib.Base/Model/Conf/BusinessConfig.cs:116`.
- BaseAPI và SSO appsettings (cùng `http://localhost:7002/`).

## 3. Bảng mã BookingESB sau khi grep đủ BuildBookRequest trong 8016

### 3.1. Triển khai (có caller 8016)

| Mã | optype | Action 8016 gọi tới | Hành vi host |
|----|--------|---------------------|--------------|
| Y002 | — | `api/Booking/GetOutpDepartment` | Danh sách khoa (`V_S_Sys_Department` Base) |
| Y003 | — | `api/Booking/GetOutpDoctor` | Danh sách bác sĩ (`Sys_Staff` Base + join Department/Duty; **giữ quirk gốc: chỉ nhân viên `Gender=true`** — nhân viên Gender=false/null bị lược, chỉ flag không tự sửa) |
| Y004 | 0 | `api/Patient/GetPatient` | Tra cứu bệnh nhân; **gốc bắt buộc `xm` + (`zjh` hoặc `kh`)** |
| Y004 | 1 | `api/Patient/PatientSelfReg` | Thêm mới bệnh nhân (cần zjh+xm+xb+csrq; CheckIDCard; trùng zjh → FAIL sub_code 10001; xb không phải 1/2 → FAIL 10003; ID sai định dạng → FAIL 10002; kh = `spFieldGetNewID("xt_card")`, blh = `spFieldGetNewID("xt_blh", "{0:D5}")`, brxz tìm theo tên đã dịch "Tu chi tra" fallback "0", cardtype XNK mã "1") |
| Y009 | null | `api/Booking/GetOutSchedule` | Lịch trực (`mz_ghpb_schedule` Sett + join `V_S_xt_sfxm` Base; giữ nguyên join thứ hai của gốc dùng `d.zt='1'` thay vì `e.zt`) |
| Y009 | 1 | `api/Booking/GetOutSchedulePage` | Như trên + phân trang server, `sub_msg` = Pagination JSON |
| Y021 | — | `api/Booking/PatBookListQuery` / `PatBookDetailQuery` | Danh sách/chi tiết lịch đã đặt (`mz_gh_book` + join brxz/Department/Staff/ItemsDetail; mặc định khoảng ngày -3 tháng/+15 ngày nếu không truyền ksrq/jsrq) |
| Y022 | — | `api/Booking/ScheduleBookApply` | Đặt lịch (cần ghxz+scheduId+kh; kiểm tra trùng cùng nguồn số và cùng khoa trong ngày; số còn lại qua `GetScheduleDetail`; `jzxh` qua `spGetJZXH`; insert `mz_gh_book` với `BookId=spGetNewID("mz_gh_book")`, `yyzt='1'`) |
| Y023 | — | `api/Booking/ScheduleBookCancel` | Hủy lịch (cần bookId+kh+lxdh theo cổng gốc — lưu ý 8016 chỉ validate kh+BookId, nên caller thiếu lxdh sẽ FAIL theo cổng gốc; cập nhật `yyzt='3'`, `CancelReason = AppId + ";" + lxdh`) |
| Y024 | "0" | `api/Booking/BookOutpatientRegist` và `api/Outpatient/BookOutpatientRegist` | Đăng ký khám từ lịch đã đặt (cần bookId+kh+PayFee+PayLsh theo cổng gốc; kiểm tra book `yyzt='1'`, `OutDate >= hôm nay`, kh khớp, `RegFee == PayFee`; rồi Save có phí — xem §4) |
| Y026 | — | `api/Booking/OutpatientRegist` và `api/Outpatient/OutpatientRegist` | Đăng ký khám trực tiếp (cần ks+kh+ghxz theo cổng gốc; **+ shim ScheduId theo §2.5**; lịch hôm nay của khoa với `RegType="1"`; `mzh = yyMMdd + ScheduId(3 số cuối) + jzxh(4 số)`; Save không phí — xem §4) |
| Y027 | — | `api/Outpatient/OutpRegTodayQuery` | **Tái cấu trúc** (không có case trong switch gốc — bản gốc rơi qua switch và trả response rỗng): `mz_gh` trong ngày (`ghzt<>'2'`, `convert(date,ghrq)=hôm nay`, lọc kh/mzh, join tên khoa/bác sĩ/brxz) |
| Y028 | — | `api/Outpatient/OutpBillUnpaidByMzh` | **Tái cấu trúc**: đơn thuốc chưa thu phí (`cfzt='0'`) của lần khám (theo `GetNewUnSettedPrescriptionByMzh` gốc — khác Y030 vốn trả hóa đơn ĐÃ quyết toán); validation 8016 dùng `||` nên thực tế yêu cầu cả kh và mzh |
| Y029 | — | `api/Outpatient/OutpBillItemDetail` | **Tái cấu trúc**: `mz_cfmx` join `mz_cf`/`mz_gh` theo danh sách `PresId` (cfnm) + mzh (khác `OutpatChargeDetail` gốc vốn tra theo `jsnm`); 8016 gộp `PresIds` thành chuỗi `PresId` |

Route BookingESB phục vụ cả hai prefix: `api/OutpBook/BookingESB`
(8016 thực sự gọi) và `api/Booking/BookingESB` (route gốc trong
Sett.API). Không đổi config 8016.

### 3.2. KHÔNG triển khai — không có caller 8016

- Có trong switch gốc nhưng không có caller: Y005 (thuốc), Y006 (hạng
  mục y tế), Y007 (bệnh ngoại trú), Y008 (loại đăng ký), Y010 (chi tiết
  nguồn số lịch trực), Y011 (đơn thuốc), Y012 (chi tiết thuốc đơn),
  Y025 (trả số đặt lịch), Y030 (thông tin thu phí ngoại trú
  `OutpatChargeInfo`).
- Y004 optype 3 (đăng ký đồng bộ `SysPatInfoSetPub`): không có caller
  8016, không triển khai — gọi tới trả FAIL rõ ràng.
- `LockerDelayedApply` (gia hạn khóa): có trong cả 2 controller gốc
  nhưng không có caller 8016 — không triển khai.

## 4. Đường ghi Y024/Y026 (Save) — port từ OutPatientSettleDmnService.Save

- Y026 (`CommonRegist → OutpatReg`) với `ghxm=null, zlxm=null` nên
  `xmList` rỗng → chỉ insert `mz_gh`, `ghzt="1"`, không tạo `mz_js`,
  `newJszbInfo = {mzh, jsnm=0, ybjsh=null, jszje=RegFee,
  jsxjzf=RegFee}`.
- Y024 (`OutBookPay → OutpatRegApply`) với `ghxm=ghlx, zlxm=zlxm` nên
  CÓ tạo `mz_xm` + `mz_js` + `mz_jsmx` và kiểm tra
  `sum(xm.je) == feeRelated.zje` (RegFee), nếu lệch thì FAIL. Cả hai
  đều chạy trong transaction, `CreatorCode="APIAdmin"` (mirror `GetAuth`
  của gốc), `gh.ghrq = DateTime.Now` (không phải tham số ghrq),
  `fzbz` = 1 nếu bệnh nhân đã có đăng ký đang hiệu lực
  (`zt='1' AND ghzt<>'2'`).
- `GetXmList` qua `GetOutpatientFees`/`GetOutpatientFeesbyGroup` với
  `EnableZlxmGroup=1` (demo) → `mz_gh_zlxmzh` + `V_S_xt_sfxm` Base.
- **Hai fix null-safety bắt buộc để Y024 chạy được** (ghi rõ, không
  lặng lẽ): `PaymentModelAccountReserveII` gốc đọc
  `feeRelated.yjjzfje.Value` và `feeRelated.xjwc` trực tiếp; luồng API
  không set hai trường này (null) nên bản gốc sẽ NullReferenceException
  khi xmList non-empty. Port coi null là 0/rỗng.
- `fph` luôn null trong luồng API nên bước
  `UpdateCurrentFinancialInvoice` được bỏ qua; nhánh BHYT Trùng
  Khánh/Quý An (`medicalInsurance == "chongqing"/"guian"`) không áp dụng
  vì cấu hình demo là `"tongyong"`.
- Thích ứng schema DB demo so với code gốc: `xt_brjbxx` demo không có
  cột `dz` và `brxz` (brxz nằm ở `xt_card`) nên `GetPatInfo` port join
  `xt_brxz` theo `b.brxz` và lấy `dz` từ `xian_dz`.
- Cột NOT NULL đã tra (kiểm chứng live sẽ lộ nếu thiếu): `mz_gh` bắt
  buộc ghnm, OrganizeId, patid, ks, ghf, zlf, ckf, gbf, CreatorCode,
  CreateTime, kh, zt, xb, blh, CardType, CardTypeName; `mz_js` bắt buộc
  jsnm, OrganizeId, patid, jslx, zje, zlfy, zffy, flzffy, jzfy, xjzf,
  zhjz, jszt, cxjsnm, jmbl, jmje, CreatorCode, CreateTime, zt, ghnm,
  xb, blh; `mz_xm`, `mz_jsmx`, `mz_gh_book` tương tự (xem code
  `SaveRegistration`/`BookApply`). Các cột NOT NULL còn lại của `mz_js`
  (`zlfy`...) không có trong code Save gốc — khi insert phải điền 0.

## 5. Nhóm B — api/PatientAnon/* (5 route)

Gốc `PatientApiController`, `RoutePrefix("api/PatientAnon")`, tất cả
`[IgnoreTokenDecrypt]`, `methodcode = null` (8016 gửi
`BuildBookRequest(request, null, null)`).

| Route | Caller 8016 | Hành vi host |
|-------|-------------|--------------|
| `OutpatRegRecord` | `api/Outpatient/OutpRegQuery` | Danh sách `mz_gh` theo kh/mzh (không lọc ngày — khác Y027) |
| `InHosBillUnpaid` | `api/Sett/InHosPatientService/InHosFeeTypeBillUnpaid` | Tra bệnh nhân nội trú rồi yêu cầu `zybz` = Djz (2) hoặc Ycy (3), sau đó `GetHospGroupFeeVOList(zyh, orgId, "2")` từ view `V_C_Sys_WsfZyXmjfb`/`V_C_Sys_WsfZyYpjfb` (Sett) + `V_S_xt_sfdl` (Base). **Gốc trả SUCCESS cả khi list rỗng** (`data != null`) — giữ nguyên |
| `InHosPatientQuery` | `api/Patient/InHosPatientQuery` (8016 tự set `ywlx=zy`) | Project về `{zyh, xm, xb, zybz, brxzmc, kh, nl=nlshow, cw=cwmc, ryrq, cyrq, bqmc=wardname, zyts}`; `cwmc` từ `Base.xt_cw` (join `cwCode`), tên khu từ `Base.xt_bq` (join `bqCode`), `zyts` tính từ ryrq/cyrq |
| `PatientLisReport` | `api/Patient/OutPatientLisReport` (`ywlx="1"`) và `api/Patient/InHosPatientLisReport` (`ywlx="2"`) | Xem §6 |
| `PatientLisReportDetail` | `api/Patient/PatientLisReportDetail` (cần sqdh+ywlx; 8016 set `sqdzt=ywc` dạng chuỗi enum) | Chi tiết theo sqdh từ bảng LIS tương ứng |

### 6. Nguồn dữ liệu PatientLisReport — kết luận + bằng chứng

**Kết luận (chưa khẳng định tuyệt đối, ghi rõ mức tin cậy):**
implementation của `IPatientCenterDmnService`
(`OutPatReportList`/`InHosPatReportList`/`PatLisReportDetail`/
`GetPatRegList`/`InHosPatientBasic`) **không có trong checkout gốc**
(file `PatientCenterDmnService.cs` gốc chỉ 541 dòng, không chứa các hàm
này; grep toàn checkout chỉ thấy interface/kiểu VO, không thấy
implementation) — implementation nằm ở assembly nhị phân ngoài
checkout.

Bằng chứng schema mạnh nhất: `Newtouch_Interface.Lis_Report_MZ/ZY` có
đúng hình báo cáo LIS (sqdh, ghnm/zyh, xmid, xmzwmc, jyjg, ckz...),
nhưng bảng đang **rỗng** và không có cột `sqdzt`/`mzh`/`kh` (MZ tra qua
`mz_gh.mzh → ghnm`; ZY có sẵn `zyh`). Ngoài ra CIS có bảng `jyjc_*`
(đơn xét nghiệm) cũng là ứng viên nguồn.

Vì vậy host tái cấu trúc LIS từ `Lis_Report_MZ/ZY` + join Sett, và
**không lọc theo `sqdzt`** (bảng không có cột tương ứng). Kiểm chứng
LIS trên staging chỉ có thể là negative test (FAIL "không tìm thấy
báo cáo") trừ khi có fixture LIS — **không bịa dòng LIS vào DB demo**;
nếu cần positive test thì chỉ chèn fixture vào bản sao Interface
staging (chưa có kế hoạch clone Interface — cần quyết định ở bước sau,
không tự ý ghi demo Interface).

## 7. Nhóm C — api/OrderCenter/* (7 route + test)

Hợp của 2 controller gốc cùng `RoutePrefix("api/OrderCenter")`. 8016
không gửi token nên thực tế bản UnAuthorized phục vụ; các route trùng
nhau giữ hành vi bản UnAuthorized, riêng `PayOutpOrder`/`PayInHosOrder`
chỉ có ở bản có xác thực.

| Route | Caller 8016 | Hành vi host |
|-------|-------------|--------------|
| `OrderQuery` | `api/OrderCenter/PayOrder/OrderQuery` | SQL `order_info` (lọc organizeid, zt='1', OrderNo/CardNo/VisitNo/OrderStu/OrderType); chỉ lọc OrderStu/OrderType khi key có trong paradata (8016 gửi `OrderInfoBaseVO` — khác gốc ở chỗ gốc lọc `OrderStu >= 0` kể cả khi caller không gửi, tức luôn lọc dzf) |
| `OrderDetailQuery` | `api/OrderCenter/PayOrder/OrderDetail` (8016 đặt tên action `OrderDetail` nhưng gọi downstream `OrderDetailQuery`) | MZ: `order_mz` join `mz_cf`; ZY: `order_zy` group theo dlcode/dlmc; lọc `CreatorCode = AppId` khi AppId có mặt (đúng `OrderDetailMzQuery`/`OrderDetailZyQuery` gốc) |
| `OrderCreate` | `api/OrderCenter/PayOrder/BillOrderCreate` và `api/Outpatient/BillOrderCreate` (bản deprecated) | MZ qua danh sách chưa quyết toán + logic đơn tồn tại (đã trả → FAIL nếu trùng, khóa cùng app + cùng nội dung → trả lại đơn cũ, khóa khác app → FAIL, chờ trả cùng nội dung → trả lại đơn cũ, lệch → hủy đơn cũ rồi tạo mới; giữ cả nhánh rỗng của gốc khi bộ ba (cfh,cfje,sfxm) không khớp kiểu cfxmmx — rơi qua tạo đơn mới); ZY qua `GetHospGroupFeeDetailVOList` + yêu cầu `zybz` 2/3 và `brxz="0"` (bệnh nhân BHYT phải ra quầy), logic đơn tồn tại tương tự nhưng khác chi tiết (đã trả → FAIL thẳng) |
| `LockerApply` | `api/OrderCenter/PayOrder/BillOrderLockApply` | Yêu cầu đơn `dzf` + CardNo khớp + OrderAmt khớp → `zfz` + nối Memo + `LastModifierCode=terminal`. **Giữ nguyên quirk gốc: `LockExpTime = Now - expConfig`** (âm, tức đã quá hạn) — chỉ flag, không tự sửa |
| `PayOrder` | `api/Outpatient/BillOrderPay` (deprecated, chỉ ở bản UnAuthorized) | Xem hàng Pay* bên dưới |
| `PayOutpOrder` / `PayInHosOrder` | `api/OrderCenter/PayOrder/BillOrderPay` rẽ theo OrderType mz/zy | Xem hàng Pay* bên dưới |
| `test` | `api/Outpatient/PayPushTest` | Chỉ chạy `OrderDetailQuery`, **bỏ qua bước push** (xem dưới), trả SUCCESS kèm data = chi tiết đơn |

**Pay\* — không fake thành công:** `PayOrder`, `PayOutpOrder`,
`PayInHosOrder` đều cần engine quyết toán (`submitOutpatCharge` ngoại
trú / discharge settle nội trú) chưa được port. Host chạy bước kiểm
tra trước (`PayOrderBefore`: phương thức thanh toán phải tồn tại trong
`xt_xjzffs` zt='1'; đơn phải đang khóa `zfz`; OrderAmt == PayFee;
CardNo khớp; VisitNo khớp nếu gửi kèm; + kiểm tra loại đơn mz/zy; +
với ngoại trú: chi tiết đơn phải khớp đơn thuốc chưa thu phí, số dòng
bằng nhau) rồi nếu qua thì trả FAIL rõ ràng
`sub_code = SETT_ENGINE_NOT_PORTED`, **không ghi gì**. Tác dụng phụ
"hủy đơn khi thông tin đơn thuốc đã đổi" của bản gốc trong bước kiểm
tra cũng được bỏ qua có chủ ý (chỉ đọc) để Pay\* không ghi staging.
Đây là **blocker cho giai đoạn sau**, không phải lỗi lặng.

**Push sau thanh toán bị bỏ qua có chủ ý:** `PushPresInfo` gốc chỉ
chạy khi config `Outpatient_ChargeFee_AutoSyncPrescriptionStatus` bật
(demo = ON) và sẽ gọi CIS API + PDS API + ghi `mz_gh.outpId` — gọi từ
host staging sẽ chạm DB demo CIS/PDS ngoài phạm vi staging, nên route
`test` và các Pay\* không thực hiện push.

**OrderNoGen — điểm lệch có chủ ý:** gốc dùng Redis counter; host
sinh số đơn từ DB (max số đơn trong phút hiện tại + 1, kiểm tra trùng,
thử lại khi trùng; format giữ nguyên
`{orderType}{yyyyMMddHHmm}{cnt:00000}` — kiểm chứng live:
`120261027114000001`). Tránh lẫn key Redis và mơ hồ về Redis Db.

**GetNewAllUnSettedList mà OrderCreateMz gốc gọi KHÔNG có trong
checkout** (chỉ có `GetNewUnSettedPrescriptionByMzh` và
`GetNewAllUnSettedListByMzh`); danh sách đơn chưa quyết toán được tái
cấu trúc từ nhánh else của `GetNewAllUnSettedListByMzh` (từng dòng
cfmx + từng dòng xm chưa thu phí), đủ cho logic trùng đơn của
OrderCreateMz (so khớp bộ ba cfh/cfje/sfxm).

**EnumOrderStatus — hai bảng mâu thuẫn trong cây gốc:** Sett `EnumDB`
(dzf=0, yzf=1, ytk=2, zfz=3, không có `zf`) vs Base.Domain của 8016
(dzf=0, zfz=1, yzf=2, ytk=3, zf=4). Code `OrderDmnService` dùng cả
dzf/zfz/yzf/zf nên chỉ nhất quán với bảng Base.Domain — host dùng
**dzf=0, zfz=1, yzf=2, zf=4**.

Các enum khác đã hiệu chỉnh: `EnumMzpblx` all=1, yy=2, fyy=3;
`EnumMzyyzt` book=1, reg=2, cancel=3, bookreg=4, regcancel=5;
`EnumZYBZ` Xry=0, Bqz=1, Djz=2, Ycy=3; `EnumMzghly` His=0, Yy=1, Yyt=3,
WeChat=4, Alipay=5, Tj=6; `EnumGhxz` PTGH=0; `EnumOutpatientJzbz`
Djz=1; `EnumJslx` GH=0; `EnumJieSuanZT` YJ=1; `EnumOrderType` mz=1,
zy=2; `EnumBusType` mz=1, zy=2; `EnumCardType` CTK=0, XNK (thẻ ảo)=1
theo dữ liệu demo, YBJYK=2; `EnumZJLX` sfz (CMND)=1.

## 8. File/project mới, launcher, snippet recovery/watchdog

### 8.1. Project mới: `Newtouch.HIS.Sett/Newtouch.Sett.API/`

ASP.NET Core 6, tự chứa (không dùng HintPath DLL run-copy), theo mẫu
module API trong repo (controller + envelope, DI qua Program.cs, log
riêng ra stdout để launcher redirect). Package (đều có trong cache
NuGet local, build offline được): `Microsoft.Data.SqlClient 2.1.4`,
`Newtonsoft.Json 13.0.3`, `Microsoft.AspNetCore.Mvc.NewtonsoftJson
6.0.0` (bắt buộc vì `BookingRequest.paradata` là JObject).
`ImplicitUsings=enable` (bản nháp ban đầu để `disable` gây 37 lỗi thiếu
using — đã sửa trong csproj, không phải code).

- `Program.cs` — `UseUrls` mặc định `http://127.0.0.1:7002`, override
  qua env `SETT_API_URLS` (chỉ phục vụ kiểm chứng 17361, §2.4); DI
  `SettDb` singleton + 3 service; `AddControllers().AddNewtonsoftJson()`;
  CORS allow-all (theo mẫu Herp); liveness `GET /` trả
  `{service: "Newtouch.Sett.API", status: "ok", urls}`.
- `Models/BookingModels.cs` — `ResultCode`, `ResponseBaseOld`,
  `BookingRequest`, `ParaData` (case-insensitive), `Terminal.FromAppId`,
  `SettConstants`.
- `Services/SettDb.cs` — helper ADO.NET: connection Sett, thay tên DB
  hardcode (`[NewtouchHIS_Base]`/`[NewtouchHIS_Sett]`/
  `Newtouch_Interface..`) bằng tên DB cấu hình, `GetNewId`
  (spGetNewID), `GetJzxh` (spGetJZXH), `GetFieldNewId`
  (spFieldGetNewID), `SysConfig`/`SysConfigBool`, `Read`/`ReadOne`/
  `Scalar`, `Str`/`Dec`/`Int`.
- `Services/BookingService.cs` + `Services/BookingService.Reg.cs`
  (partial) — nhóm A + Save (Y024/Y026).
- `Services/PatientAnonService.cs` — nhóm B.
- `Services/OrderService.cs` — nhóm C.
- `Controllers/SettApiControllers.cs` — `OutpBookController`
  (2 prefix `api/OutpBook` + `api/Booking`), `PatientAnonController`,
  `OrderCenterController`. Không dùng `[ApiController]` để body lỗi vẫn
  trả envelope HTTP 200 thay vì 400.
- `appsettings.json` — `BusinessDB.DefaultConn` template với
  **PLACEHOLDER** (không mật khẩu thật), `BusinessConfig.AppId =
  Sett.API`, `Top_OrganizeId` = org demo,
  `SettApi = {SettDb: NewtouchHIS_Sett, BaseDb: NewtouchHIS_Base,
  InterfaceDb: Newtouch_Interface, LockedOrderExpiredMinute: 5}`.

### 8.2. Launcher (mới, chưa áp dụng vào recovery.sh)

- `run-sett-api.sh` (prod): trỏ Sett demo (qua `SettApi:SettDb` mặc
  định), URLS `http://127.0.0.1:7002`, IPV6 flag, proxy unset, secret
  qua `~/.sqlserver.env` (file env 600) → `BusinessDB__DefaultConn`
  override, theo mẫu `run-sso.sh`/`run-his-baseapi.sh`. Đã kiểm chứng
  launcher chạy được host từ trạng thái chưa chạy (cả bản prod và bản
  staging đều khởi động thành công từ trạng thái cổng trống).
- `run-sett-api-stg.sh` (staging, chỉ dùng khi kiểm chứng): giống bản
  prod nhưng thêm `SettApi__SettDb=NewtouchHIS_Sett_STG`.

### 8.3. Snippet recovery.sh/watchdog — CHUẨN BỊ SẴN, điều phối viên áp dụng

```bash
# 27. Sett-API host 7002 (only if 7002 down and its process is absent).
# Launcher run-sett-api.sh carries proxy-unset + explicit
# ASPNETCORE_URLS=http://127.0.0.1:7002 + DOTNET_SYSTEM_NET_DISABLEIPV6=true,
# Release build. PROD launcher points at the demo Sett DB; the staging
# launcher (run-sett-api-stg.sh) must NEVER be used in recovery.
if ! port_up 7002; then
  if [ -x ~/workspace/openhis-core/run-sett-api.sh ] && ! pgrep -f "Newtouch.Sett.API" > /dev/null 2>&1; then
    nohup ~/workspace/openhis-core/run-sett-api.sh < /dev/null >> ~/workspace/openhis-core/sett-api.log 2>&1 &
    wait_port 7002 24 || echo "SETT-API-NOT-UP"
  fi
fi
```

Watchdog: thêm `7002` vào danh sách cổng kiểm tra (cùng nhóm với các
cổng hiện có). Lưu ý §2.4: nếu 8016 vẫn chạy Development thì recovery
chỉ bật 7002 vẫn chưa đủ cho chuỗi 8016 — cần quyết định 17361
(§2.4) trước khi chốt watchdog.

## 9. Staging — DB, đĩa, chứng minh không ghi demo

- Đĩa trước restore: 74 GB trống trên `/dev/mapper/rv` (100 GB). File
  DB Sett: MDF 6557 MB + LDF 1744 MB (tổng ~8.3 GB, phần lớn là khoảng
  trống).
- Backup demo Sett sang
  `~/workspace/sqlbackup-stg/NewtouchHIS_Sett_preSTG.bak` (6202 pages,
  ~48 MB, 1.176 giây) rồi restore `WITH MOVE` thành
  **`NewtouchHIS_Sett_STG`** (file
  `/var/opt/mssql/data/NewtouchHIS_Sett_STG.mdf` /
  `..._log.ldf`, 22.891 giây), trạng thái ONLINE. **Không restore đè
  lên demo.**
- Snapshot count trước kiểm chứng (org demo), demo = STG: `mz_gh` 4,
  `mz_gh_book` 0, `order_info` 0, `order_mz` 0, `xt_brjbxx` 1, `xt_card` 1.
- CHỈ host 7002/17361-staging trỏ connection string sang `*_STG` (qua
  launcher env). 8016 giữ nguyên config demo (nó chỉ proxy, không ghi
  Sett trong chuỗi này). Đường ghi Y026 chỉ chạm DB Sett (insert
  `mz_gh`, và khi có phí thì `mz_xm`/`mz_js`/`mz_jsmx` cùng DB Sett)
  và đọc Base (chỉ đọc: khoa, hạng mục phí, cấu hình) — không ghi DB
  nào khác. LIS đọc Interface (chỉ đọc).
- Fixture CHỈ trong STG (không chạm demo):
  - Lịch trực ngày kiểm chứng 2026-10-27: ScheduId 1597 (KS01,
    RegType "1", RegFee 3.00, ghlx "00000315", TotalNum 10, IsBook 1,
    IsCancel 0) và ScheduId 1598 (KS04, tương tự) cho test Y023.
  - Đơn thuốc chưa quyết toán cho mzh Y026: `mz_cf` cfnm 100004
    (cfh "CF-STG-1", zje 25.50, cfzt "0") + `mz_cfmx` cfmxId 100008
    (je 25.50).
- Chứng minh không ghi demo (COUNT trực tiếp sau kiểm chứng):
  - Demo giữ nguyên: `mz_gh` 4, `mz_gh_book` 0, `mz_js` 12,
    `order_info` 0, `order_mz` 0, `xt_brjbxx` 1, `xt_card` 1.
  - STG sau kiểm chứng: `mz_gh` 8, `mz_gh_book` 2, `mz_js` 13,
    `order_info` 1, `order_mz` 1, `xt_brjbxx` 2, `xt_card` 2.
  - Dòng E2E `2610275970004` chỉ có trong STG (0 dòng trong demo).
- DB `NewtouchHIS_Sett_STG` giữ lại sau kiểm chứng để đợt sau dùng
  tiếp hoặc DROP khi đã có kết luận; backup
  `~/workspace/sqlbackup-stg/NewtouchHIS_Sett_preSTG.bak` là điểm khôi
  phục pre-STG.

## 10. Kết quả kiểm chứng

Org demo: `6d5752a7-234a-403e-aa1c-df8b45d3469f`. Bệnh nhân demo:
kh `10000`, patid `100000`. Host staging PID 14192 (7002, sau 2 lần
restart để cập nhật build) + PID 14193 (17361 tạm, đã dừng sau E2E).
RAM trong lúc chạy: tổng 7935 MB, trống ~145–207 MB, khả dụng
~430–446 MB — host đứng vững cùng stack, không cần dừng app khác.

### 10.1. Nhóm A — BookingESB (7002 trực tiếp)

| Test | Kết quả |
|------|---------|
| Y002 danh sách khoa | 10000, 17 khoa (vd KS04, KS18, 03) |
| Y003 danh sách bác sĩ | 10000 (vd 000000 Khoa Noi) |
| Y004/0 chỉ kh (không xm) | 40004 đúng cổng gốc (gốc bắt buộc xm) |
| Y004/0 kh + xm "A Binh" | 10000, patid 100000, blh 11000 |
| Y004/1 bệnh nhân mới (CMND 18 số checksum đúng) | 10000, patid 100001, blh 00001, kh mới từ spFieldGetNewID |
| Y004/1 trùng CMND | 40004 sub_code 10001 đúng gốc |
| Y009 lịch hôm nay KS01 | 10000, đúng fixture 1597 (RegFee 3.00, ghlx 00000315) |
| Y009 optype 1 phân trang | 10000, `sub_msg` = Pagination JSON `{page:1, rows:10, records:1}` |
| Y021 list trước khi đặt | 40004 "Không tìm thấy bản ghi đặt hẹn" (đúng) |
| Y022 đặt lịch 1597 | 10000 "Đặt hẹn thành công", BookId 1, QueueNo 1, RegFee 3.00 |
| Y021 list sau đặt | 10000, thấy BookId 1 |
| Y024 đăng ký từ lịch (BookId 1, PayFee 3.00) | 10000, mzh `2610275970001`, **jsnm 100012**, jszje 3.00 — Save có phí chạy đủ (mz_js tạo mới) |
| Y026 đăng ký trực tiếp (ks KS01) | 10000, mzh `2610275970002`, jsnm 0 (không phí, đúng thiết kế) |
| Y027 đăng ký trong ngày | 10000, thấy các dòng Y026 (ghzt 1, jsrq set) |
| Y022 đặt lịch 1598 (KS04) | 10000, BookId 2 |
| Y023 hủy BookId 2 (lxdh kèm theo) | 10000, yyzt chuyển cancel, CancelReason `His;13800000000` đúng format gốc |
| Y021 sau hủy | 10000, BookId 2 trạng thái đã hủy |
| Y026 chỉ ScheduId (không ks — shim §2.5) | 10000, mzh `2610275970003` |
| Y028 hóa đơn chưa trả (mzh Y026) | 10000, CF-STG-1 25.50 |
| Y029 chi tiết theo PresId 100004 | 10000, đúng dòng cfmx 25.50 |

### 10.2. Nhóm B — PatientAnon (7002 trực tiếp)

| Test | Kết quả |
|------|---------|
| OutpatRegRecord (mzh Y026) | 10000, đúng dòng `2610275970002` |
| InHosPatientQuery (zyh 01000) | 10000, zybz 3 (Ycy), zyts 8 ngày, bqmc "Khu 1 Khoa Noi" (lưu ý: tên bệnh nhân nội trú demo `zy_brjbxx` vẫn còn tiếng Trung — dữ liệu demo ngoài phạm vi đợt dịch) |
| InHosBillUnpaid (zyh 01000) | 10000, data `[]` (đúng hành vi gốc: SUCCESS cả khi rỗng; view phí nội trú của zyh 01000 đang rỗng) |
| PatientLisReport mz + zy | 40004 "Không tìm thấy báo cáo xét nghiệm" — negative test đúng (bảng LIS rỗng, §6) |
| PatientLisReportDetail (sqdh dummy) | 40004 "Không tìm thấy chi tiết báo cáo xét nghiệm" — negative test đúng |

### 10.3. Nhóm C — OrderCenter (7002 trực tiếp)

| Test | Kết quả |
|------|---------|
| OrderCreate mz (mzh Y026, fixture CF) | 10000, OrderNo `120261027114000001` (đúng format gốc), OrderAmt 25.50, stu 0 (dzf) |
| OrderCreate mz lần 2 | 10000, trả lại **cùng đơn** (logic đơn chờ trả khớp nội dung) |
| LockerApply (OrderAmt 25.50) | 10000, OrderStu 1 (zfz), LockExpTime = Now - 5 phút (quirk gốc giữ nguyên) |
| OrderDetailQuery | 10000, mzList đúng dòng CF-STG-1/cfxmmx 100004 |
| OrderQuery (CardNo 10000) | 10000, thấy đơn đã khóa, Memo ghi khóa |
| PayOutpOrder / PayOrder (đã khóa, đủ điều kiện) | 40004 `sub_code = SETT_ENGINE_NOT_PORTED`, không ghi gì |
| PayInHosOrder trên đơn mz | 40004 "Không phải đơn hàng nội trú" (kiểm tra loại đơn chạy trước) |
| test | 10000, data = chi tiết đơn, không push |
| OrderCreate zy (zyh 01000) | 40004 "Không tìm thấy hóa đơn chờ thanh toán" — negative test (view phí nội trú rỗng) |

### 10.4. Đầu cuối chuỗi hỏng cũ — QUA 8016 (E2E)

- Lấy JWT từ AuthCenter 8015 `api/Auth/GetAppToken` theo đúng cơ chế
  đang chạy (`Request<string>` với AppId caller + Data = AppId đích;
  cặp CIS → API.Manage đã đăng ký friend) — không in token/secret.
- Gọi 8016 `api/Outpatient/OutpatientRegist` với Bearer JWT, body
  `RequestBus<OutpatientRegistRequest>` `{kh: 10000, ScheduId: 1597,
  ghxz: "0"}` (không ks — đúng contract 8016).
- Lần 1: 8016 trả `20000` "Connection refused (localhost:17361)" —
  phát hiện §2.4. Chạy instance staging tạm trên 17361 (PID 14193),
  gọi lại: **code `10000`, msg "Đăng ký khám thành công",
  data `{mzh: "2610275970004", jsnm: 0, jszje: 3.0}`**.
- Dòng `2610275970004` chỉ có trong STG, không có trong demo (§9).
- Instance 17361 đã dừng sau kiểm chứng; instance 7002 staging vẫn
  chạy (PID 14192) chờ điều phối viên restart bằng `run-sett-api.sh`
  (prod) trước khi coi 7002 là prod.

### 10.5. Quét cổng cuối + SSO

- 26/26 cổng LISTEN: 1433, 5208, 5220, 5221, 5231–5238, 5320,
  5331–5337, 6379, 7001, **7002**, 8015, 8016, 41142 (17361 đã dừng có
  chủ ý sau E2E nên không nằm trong danh sách cuối).
- Login SSO 000000: thành công (`state: success`, message "Đăng nhập
  thành công") — gửi password dạng md5 theo đúng cơ chế CheckLogin
  (gửi plaintext sẽ báo "Mật khẩu không đúng" — đó là hành vi đúng của
  SSO, không phải lỗi do host mới). MRQC 5220 vẫn 200.
- **Trạng thái instance 7002 cuối giai đoạn: STAGING (trỏ STG)** —
  phải nêu rõ: điều phối viên phải restart bằng `run-sett-api.sh`
  (prod, trỏ Sett demo) trước khi coi 7002 là prod. Lý do: 8016 hiện
  proxy sang 7002 (production) / 17361 (Development), nên để host STG
  chạy thì chuỗi 8016 cũng không thể ghi demo — an toàn hơn là để
  trống hoặc trỏ demo trong lúc chương trình chưa cắt.

## 11. Blocker và việc chặn giai đoạn sau (không giấu)

1. **Engine quyết toán Pay\* chưa port** (`submitOutpatCharge` ngoại
   trú / discharge settle nội trú): PayOrder/PayOutpOrder/PayInHosOrder
   chỉ kiểm tra trước rồi FAIL `SETT_ENGINE_NOT_PORTED`. Thanh toán
   đơn hàng qua API chưa chạy được tới cùng.
2. **Dữ liệu LIS thật rỗng** (`Lis_Report_MZ/ZY` 0 dòng): LIS chỉ có
   negative test; positive test cần dữ liệu LIS thật hoặc fixture
   Interface staging (chưa quyết định clone Interface).
3. **Phí nội trú cho zyh demo rỗng** (view `V_C_Sys_WsfZy*` 0 dòng):
   `InHosBillUnpaid` SUCCESS rỗng và `OrderCreateZy` FAIL "không tìm
   thấy hóa đơn" đều là negative test; cần fixture phí nội trú
   (qua `zy_xmjfb`/`zy_ypjfb` — chưa tra định nghĩa view kỹ) hoặc dữ
   liệu thật để positive test ZY.
4. **Quyết định 17361 vs Production cho 8016** (§2.4): chuỗi 8016
   đang chạy dùng 17361, host prod dùng 7002 — cần chốt trước khi
   coi chuỗi là prod.
5. **Shim ks/ScheduId cho Y026** (§2.5): đã áp dụng để chuỗi 8016
   chạy được; phương án dài hạn (thêm `ks` vào 8016) cần quyết định
   riêng.
6. Quirk giữ nguyên cần biết: Y003 chỉ lấy nhân viên Gender=true;
   LockerApply trả LockExpTime quá khứ; hai bảng EnumOrderStatus mâu
   thuẫn (host dùng bảng Base.Domain); Y027/Y028/Y029 là tái cấu trúc
   (không có case gốc); Y004/1 bỏ qua log `xt_brjbxxLOG` ở nhánh tạo
   mới và `py` lấy bằng tên thay vì pinyin tiếng Trung; `CardTypeName`
   demo còn tiếng Trung (`虚拟卡`, ngoài phạm vi đợt dịch vì `xt_card`
   không nằm trong scope).
7. **Không có host Sett-API nào khác được tạo**: chỉ 2 launcher
   (prod + staging) cho cùng một host; không bịa thêm route ngoài 3
   nhóm 8016 gọi.

## 12. Ghi chú kỹ thuật vận hành (để đợt sau khỏi vấp lại)

- sqlcmd: binary `~/workspace/mssql-tools/opt/mssql-tools18/bin/sqlcmd`;
  biến mật khẩu là `MSSQL_SA_PASSWORD` trong `~/workspace/.sqlserver.env`
  (không phải `SA_PASSWORD`); cần `LD_LIBRARY_PATH` +
  `ODBCSYSINI=~/workspace/odbc`; luôn dùng `-C -b` và verify kết quả
  trên server, không tin exit code.
- .NET SDK: `~/.dotnet/dotnet`. Build tuần tự (1 tiến trình).
- `ScheduId` của `mz_ghpb_schedule` là IDENTITY — fixture không được
  chèn explicit ScheduId; `mz_cf`/`mz_cfmx` sinh key qua `spGetNewID`.
- `order_info`/`order_mz`/`order_zy`: cột `Id` NOT NULL nhưng không
  phải IDENTITY — insert dùng `newid()` (mirror `Create(true)` của EF
  gốc).
- SSO CheckLogin nhận password md5, không nhận plaintext.
- Log host staging lúc kiểm chứng đã chuyển sang `/tmp/sett-api-logs/`
  để diff sạch (không nằm trong commit tương lai).
