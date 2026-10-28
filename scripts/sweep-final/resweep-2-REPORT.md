# RE-SWEEP-2 — Kiểm chứng lại fix FIX-2B sau redeploy-2 (CIS + Herp + MRMS + OR + EMR + PDS + Base)

- Worker: RE-SWEEP-2. Ngày: 2026-10-28 (ICT).
- Base: nhánh V2.2 HEAD `e9d625e3` tại `~/workspace/openhis-core` (đã kiểm chứng `git rev-parse`;
FIX-1 `3f8d9723` + FIX-2B `e9d625e3`, prod đã redeploy xong 16/16 app theo
`scripts/sweep-final/redeploy2-REPORT.md` — đã đọc trước khi làm).
- Phạm vi: VERIFY-ONLY. Không sửa code, không commit, không sửa `roles-shots-progress.md`,
không deploy/khởi động lại app prod nào. Không app prod nào tắt trong phiên
(cuối phiên 9 cổng prod kiểm tra lại đều LISTEN: 5231–5238, 7001).
- Tài khoản kiểm chứng duy nhất: `000000` / `000000` (POST CheckLogin dùng
`password = md5(000000) = 670b14728ad9902aecba32e22fa4f6bd`). Không lần login nào báo
khóa/sai; không đoán mật khẩu. Mọi ghi kiểm chứng CHỈ trên DB `*_STG`, đã dọn sạch
(xem §8); trên PROD chỉ đọc + kiểm tra validation-gate, 0 ghi DB demo.
- Cách đọc: `ĐẠT` = kiểm chứng live (ghi rõ PROD hay STG); `CHƯA ĐẠT/NỢ` = chưa kiểm
chứng được + lý do chính xác, không tuyên bố xong. Ảnh ở `shots-sweep/` (đã gitignore
theo pattern `shots-*/`, không lọt commit).

## 0. Hạ tầng kiểm chứng dùng trong phiên (scratch từ HEAD, đã kill hết cuối phiên)

Hạ tầng staging dùng chung từ các phiên trước (SSO 15208, BaseAPI 17001, center 18015/18016)
lúc bắt đầu phiên này đều KHÔNG chạy, nên worker tự dựng lại bằng scratch từ HEAD
(không dùng binary cũ `openhis-stg`, không sửa launcher repo):

- Thư mục scratch: `~/workspace/resweep2-scratch/` (ngoài repo; giữ đúng độ sâu thư mục để
HintPath tới `lmsoft_openhis_run` và `his-dll-common` vẫn đúng; chỉ đổi literal cổng trong
`Program.cs` của bản scratch, repo gốc không đổi — đã đối chiếu `Program.cs` prod vẫn giữ
cổng gốc sau phiên).
- Build tuần tự, mỗi lần 1 build, proxy env UNSET, `DOTNET_SYSTEM_NET_DISABLEIPV6=true`,
đúng cấu hình launcher prod từng app (Debug: CIS Web/API, MRMS Web, OR Web; Release: còn lại).
Tất cả build đều 0 error (BaseAPI, CIS Web/API, Sett API, Herp Web/API, MRMS Web/API,
OR Web, EMR Web/API, HOSP Web, Base Web, PDS Web).
- BaseAPI staging scratch: cổng **17201**, PID **28725** (coi như hạ tầng dùng chung thay cho
17001 đã tắt; không tính vào giới hạn 2 app staging cùng lúc — cùng lúc tối đa chỉ 2 app
kiểm chứng + BaseAPI infra). Launcher: `resweep2-scratch/run-rs2-baseapi.sh`
(DB `*_STG`, Redis 6380). Probe CheckPwd trực tiếp bằng body tự đoán định dạng trả
message chung chung 40004 — worker KHÔNG dùng đường đó để kết luận; BaseAPI staging được
chứng minh khỏe bằng login module thật qua nó (CIS/Herp/MRMS/OR/EMR/HOSP/Base/PDS staging
đều login thành công ngay lần đầu).
- Sett API scratch: cổng **17002**, PID **29713** (chỉ phục vụ B-CIS-04, xem §1.4).
- App staging (cổng/PID, tất cả đã kill đúng PID, cổng xác nhận DOWN cuối phiên):
CIS Web 16234/29165, Herp Web 16236/30361 (+bad-DB 30557, +khỏe lại 30755),
Herp API 16336/30362, MRMS Web 16232/31199 (+chụp lại 34585), OR Web 16231/32108,
EMR Web 16235/32303, EMR API 16335/32304, HOSP Web 16233/32693, Base Web 16238/32694,
PDS Web 16237/33268. Cuối phiên: không còn process `resweep2-scratch` nào, mọi cổng
scratch đã DOWN.
- Lưu ý về phương pháp chụp ảnh: cookie-injection từ file cookie curl KHÔNG vào được session
(browser vẫn về trang Login) nên ảnh chụp bằng form-login trực tiếp trong Firefox
(Playwright local, `domcontentloaded` — không dùng `networkidle` vì kẹt, theo lesson C2).

## 1. Kiểm chứng từng fix FIX-2B

### 1.1 CIS

| Mục | PROD (chỉ đọc) | STG (HEAD rebuild) | Kết luận |
|---|---|---|---|
| B-CIS-01 MainBusiness/Index | HTML 200, không còn JSON lỗi duplicate key `ISOpenZycfnew` | HTML 200 | **ĐẠT cả 2** |
| B-CIS-02 tạo từ điển phụ | (ghi — chỉ STG) | `POST /SystemManage/AuxiliaryDictionary/SubmitForm` không Id, `Code=RS2-TEST` → `success "Thao tác thành công."`; SQL xác nhận dòng mới Id GUID `0f5e445e-…`, `CreatorCode=000000`, `zt=1` | **ĐẠT** |
| B-CIS-03 sửa từ điển phụ | (ghi — chỉ STG) | Sửa dòng RS2-TEST đổi Name → success; `GetFormJson` + SQL xác nhận Name mới, `CreatorCode=000000` giữ nguyên, `CreateTime` giữ nguyên; xóa qua app → success, count `xt_fzcd` về 0 | **ĐẠT** |
| B-CIS-03 rà edit-path | — | Code HEAD: cả 8 repo cùng mẫu đều giữ CreatorCode (`SysAuxiliaryDictionaryRepo`, `InpatientDietBaseRepo`, `InspectionCategoryRepo`, `PatientVitalSignsRepo`, `Pacs_ExamBodyPartsRepo`, `SysBodyPartsRepo`, `SysDoctorRemarkRepo`, `SysMedicineProfitLossReasonRepo`); `ServiceCompat.Create` sinh GUID (`Guid.NewGuid()` ở cả 2 nhánh string/Guid). Live chỉ cho AuxiliaryDictionary (xem §2 cho ProfitLossReason) | **ĐẠT (code + 1 live); ProfitLossReason riêng §2** |
| B-CIS-04 grid chờ khám, hết NRE | GET và POST `SelectNotYetTreateList` đều trả `{"state":"error","message":"Gọi giao diện thất bại"}` — không NRE | Chưa có Sett API: cùng lỗi đúng dạng, không NRE. Có Sett API scratch khỏe (17002, liveness `{"service":"Newtouch.Sett.API","status":"ok"}`): vẫn cùng lỗi đúng dạng, không NRE (xem §1.4 — happy path vẫn nợ, lý do chính xác) | **ĐẠT ở mức guard; happy path NỢ** |
| B-CIS-05 trang truyền dịch, 3 dạng đầu vào | rỗng → view + "Chưa nhập mã phiếu truyền dịch."; `tags=abc` → view + "Mã phiếu truyền dịch không hợp lệ."; `tags=999999999` → view + "Không tìm thấy phiếu truyền dịch." (0 chuỗi `Object reference`/`runtime binding`) | Cả 3 dạng cho đúng thông báo như trên (constructor DI mới đã live) | **ĐẠT cả 2, đủ 3 dạng** |
| B-CIS-06 hàng đợi thiếu ngày | `GetQueueInfo` không ngày và `getNextCall` không ngày đều trả đúng `{"state":"error","message":"Vui lòng chọn ngày xếp hàng hợp lệ (từ 01/01/1753 đến 30/12/9999)."}` | Không ngày, ngày `1000-01-01`, và `getNextCall` không ngày đều trả đúng message trên, không còn SqlDateTime overflow | **ĐẠT cả 2** |

### 1.2 Herp

| Mục | PROD (chỉ đọc) | STG (HEAD rebuild) | Kết luận |
|---|---|---|---|
| B-HERP-01 đường khỏe | `GetPsiByKf` trả 12 tháng (0), `GetRkCountByLx` trả `[]`, `getmesquery` trả `{"rows":[],"kcyj":0,"gqyj":0}` | Đường khỏe giống hệt | **ĐẠT** |
| B-HERP-01 đường lỗi | (không ép lỗi trên prod) | Instance Herp trỏ HerpDb sang `NoSuchDb_RS2` (chỉ đổi env instance đó, PID 30557): cả 3 endpoint đều trả đúng `{"state":"error","message":"Không thể lấy dữ liệu từ cơ sở dữ liệu. Vui lòng thử lại sau."}`, không còn chuỗi `中文提示`. Sau đó khôi phục instance khỏe (PID 30755), đường khỏe kiểm lại vẫn đúng | **ĐẠT (đường lỗi chứng minh trên STG bad-DB)** |
| Herp API GetToken trên STG | — | `POST 16336/api/Auth/GetToken` (AppId Herp, credential đúng) → `code 10000` + token; `POST /api/Default/Query {}` → 40004 `TIMESTAMP_IS_REQUIRED` (fail-closed) | **ĐẠT** |

### 1.3 MRMS / OR / EMR / Base

| Mục | PROD (chỉ đọc) | STG (HEAD rebuild) | Kết luận |
|---|---|---|---|
| C2-B1 MrDept xem/sửa/xóa mềm | (ghi — chỉ STG) | Tạo `SubmitDicDept` (ksbm=RS2B1) → success, Id GUID `8fce76bd-…`, `CreatorCode=000000`, `zt=1`; `GetFormJson` trả đúng entity; sửa đổi tên → success, SQL xác nhận tên mới; `DeleteData` → success, `zt` 1→0 (xóa mềm đúng thiết kế); dọn vật lý SQL, count về 0. Lưu ý probe: lần đầu gửi `zt=true` bị lỗi truncation do cột `zt` là char(1) — lỗi của probe, gửi `zt=1` như UI thì pass | **ĐẠT đủ vòng** |
| C2-B2 MrFee tạo mới | (ghi — chỉ STG) | Tạo `submitForm` (`name=RS2 Nhom Phi, Lev=1`) → success; SQL xác nhận dòng `mr_dic_bafeetype` mới (Code tự sinh 11, Lev 1, `CreatorCode=000000`, `zt=1`); `DeleteData` → success dạng xóa mềm (`zt`→0), dọn vật lý SQL, count về 0 | **ĐẠT** |
| C2-B4 MRHomePage grid trả rows | `GetGridList` trả `{"rows":[],"total":0,…}` — hết lỗi `Invalid object name 'dbo.mr_dic_common'` (rỗng là đúng dữ liệu demo, không phải lỗi) | `GetGridList` trả rows (dòng `G7-EMR-BASY`, ZYH 01000) — hết lỗi object name, hết lỗi cú pháp | **ĐẠT cả 2** |
| C2-B4 xác nhận comment SQL | — | Code HEAD `MRHomePageDmnService.cs:59-66`: comment giải thích nằm NGOÀI chuỗi SQL verbatim (trước `string sql = @"..."`), chuỗi SQL chỉ chứa câu lệnh + `DbNames` sang MRMS/BaseDb | **ĐẠT (code + live)** |
| C2-B5 NursingRecord 3 nhánh hllx | Không ngày với hllx=1, 2, 99 đều trả `[]` | Cả 3 nhánh đều trả `[]` | **ĐẠT cả 2** |
| C2-B6 chu trình khóa→mở | (ghi — chỉ STG) | Trên `BlId=5a93fe30-c562-4b23-b11d-03e217a1a744` (baseline IsLock NULL, `LastModifierCode=000000`): khóa (`isToLock=1`) → `success "Khóa thành công"`, SQL IsLock NULL→1; mở (`isToLock=0`) → `success "Mở khóa thành công"`, IsLock 1→0; khôi phục IsLock về NULL bằng SQL đúng baseline | **ĐẠT đủ vòng, message đúng từng chiều** |
| C2-B3 OPStaff total khớp rows | Keyword không khớp → `rows=[]`, `total=0/records=0`; không keyword → 1 dòng, `total=1/records=1` | Giống hệt prod (cùng 1 dòng Id `6cc72253-…` vì STG là clone) | **ĐẠT cả 2** |
| EMR B1/B2 + CSS login mobile | HTML login chứa `<!--[if lte IE 8]>` + `errorie`, nhãn tab `Chào mừng`; `/Content/css/login.css` phục vụ chứa `@media (max-width: 640px)` | HTML login STG giống hệt; CSS STG chứa `max-width: 640px` | **ĐẠT (qua HTML/CSS phục vụ; không tuyên bố bằng ảnh render mobile cho login EMR)** |
| E-01 Classification/Item/Comparison | (ghi — chỉ STG) | Tạo Classification → success, sfflId GUID `20ab737e-…`, `CreatorCode=000000`; sửa → success, tên mới + CreatorCode/CreateTime giữ nguyên; tạo Item → success, sfxmId GUID `965befe9-…`; sửa Item → success; tạo Comparison → success, sfxmdzId GUID `1be51026-…` (bảng `xt_kf_sfxmdz`, kfsfxmCode=RS2XM, xtsfxmCode=20240613); sửa Comparison (xtsfxmCode→20240614) → success, CreatorCode/CreateTime giữ nguyên; xóa cả 3 qua app → success, count về 0/0/0. Lưu ý route: controller không có tiền tố area — route đúng là `/RehabChargeClassification/SubmitForm` (thử `/Rehabilitation/…` trả body rỗng 404, lỗi của probe) | **ĐẠT đủ vòng tạo/sửa/xóa cả 3 repo; xác nhận SetPk không còn no-op qua PK GUID được sinh thật** |
| E-02 danh mục khóa int | (ghi — chỉ STG) | Tạo mới thành công cả 9 danh mục, ID tự tăng: Usage 63 (cần thêm `yplx=1`), Nation 73, Unit 612, Classification 244, Formulation 73, IOMode 18 (cần thêm `crkbz=1`), Nationality 264, Diagnosis 248601, TCMSyndrome 2 (bảng đúng là `xt_zyzh`, không phải `xt_zh` như probe đầu đoán). Xóa Usage qua app `DeleteForm` → success, count về 0; 8 mục còn lại KHÔNG có action xóa qua app nên xóa bằng SQL; tổng tồn đọng về 0. Lưu ý route: controller Base Web không có tiền tố thư mục — route đúng là `/SysMedicineUsage/SubmitForm` (thử `/PharmacyDrugStorage/…`, `/Sys/…`, `/Settlement/…` trả body rỗng 404, lỗi của probe) | **ĐẠT 9/9 tạo; xóa: 1 qua app + 8 qua SQL (ghi rõ lý do)** |
| EMR API body-token | Body-only → `10000` + danh sách `Sys_Config`; header → `10000` + cùng danh sách | Body-only → `10000`; header → `10000` (cùng danh sách) | **ĐẠT cả 2, cả 2 môi trường** |

## 2. ProfitLossReason (FIX-2B sửa code nhưng chưa kiểm chứng live) — ĐÁNH GIÁ FIXTURE

**Kết luận: KHÔNG dựng được kiểm chứng live trung thực trong phạm vi verify-only — vẫn chỉ
kiểm chứng bằng code, không bịa kết quả live. Không tạo bảng nào trên STG (tạo bảng mà
không có đường gọi thì không chứng minh được gì, chỉ để lại schema drift).**

Bằng chứng đã kiểm chứng:

- Entity `SysMedicineProfitLossReasonEntity` map `[SugarTable("xt_ksbysyyy")]`; bảng này
**không tồn tại ở cả CIS STG lẫn CIS demo** (kiểm chứng `INFORMATION_SCHEMA` cả 2 chiều,
chỉ đọc, 0 dòng).
- `SubmitForm` của repo này **không có caller nào** trong toàn bộ CIS Web + Application layer:
chỉ có DI registration trong `Program.cs:141` và 2 action đọc `GetLossProfitReasonListByType`
(qua `PrepareMedicineApp` / `ProfitAndLossController`) — không controller/action nào post tới
`SubmitForm`. Tạo bảng STG cũng không tạo ra đường gọi qua app.
- Mâu thuẫn schema nội bộ repo (ghi thêm, không tự sửa): `GetPagintionList` của cùng repo truy
vấn bảng **`xt_ypsyyy`** (không phải `xt_ksbysyyy`), và bảng này cũng **không tồn tại** ở cả
STG lẫn demo. Tức ngay cả đường list của repo cũng không chạy được trên DB hiện tại.
- Code FIX-2B đã có trong HEAD và đọc đúng mẫu B-CIS-03: nhánh sửa nạp `oldEntity` qua
`FindEntity`, giữ `CreatorCode`/`CreateTime` gốc khi phía post trống, rồi `Update(entity)`;
nhánh tạo `entity.Create(true)` + `Insert(entity)`. Sửa này chỉ kiểm chứng được bằng code
cho tới khi (a) bảng `xt_ksbysyyy` tồn tại theo đúng phiên bản DB và (b) có route/action
gọi `SubmitForm` — cả 2 đều cần quyết định schema/sản phẩm, ngoài phạm vi verify-only.

## 3. E-03 tồn dư + E-05 PDS Home mobile

### 3.1 E-03 — getmesquery vẫn lỗi `kcyjz` (KHÔNG phải regression; quyết định mở giữ nguyên)

| Kiểm chứng | Kết quả |
|---|---|
| PROD `getmesquery` | Vẫn lỗi `{"state":"error","message":"Invalid column name 'kcyjz'.\nInvalid column name 'kcyjz'."}` kèm stack trace — đúng kỳ vọng trong task (khoảng trống schema theo phiên bản DB, quyết định mở, KHÔNG phải regression) |
| STG HEAD `getmesquery` | Trả `{"state":"error","message":"抱歉，系统处理异常！请查看系统日志。","exStackTrace":null}` (message framework chung, giấu stack); **log server STG xác nhận nguyên nhân gốc vẫn là `Invalid column name 'kcyjz'`** (2 lần trong `pds.log`). Tức cùng một lỗi gốc, chỉ khác hình thức bọc lỗi giữa binary prod PDS (chưa redeploy trong redeploy-2 vì PDS không có file đổi FIX-2) và binary STG HEAD — không phải bug mới |
| Phần còn lại của E-03 trên binary HEAD | PROD: `GetPendingCount` trả counts thật (kể cả `expiryDrugCount=216`, `todayDrugCount=216`), `GetNeedDealDiv` success + HTML, `GetFyCountBydl` → `[]`, `GetFyCountVoByYfbm` → 2 dãy tháng toàn 0, `Home/Index` 200. STG HEAD: giống hệt (chỉ khác `rkdshCount`: prod 0, STG 1 — dữ liệu STG, không phải lỗi) + `Home/Index` 200 (23.550 byte) |
| Kết luận | **E-03 dạng báo cáo vẫn là artifact đã qua; tồn dư getmesquery-kcyjz vẫn MỞ, cần quyết định schema** (như FIX-2B đã ghi; worker này không tự sửa code/schema) |

### 3.2 E-05 PDS Home mobile — GIỮ DEFERRED (đã kiểm chứng nhanh hiện trạng)

- Ảnh mới trên STG HEAD, viewport 390×844: `shots-sweep/resweep2-pds-home-mobile.png` —
shell AdminLTE desktop vẫn bị nén vào 390px: sidebar/menu đè lên nhau, widget thống kê
chen chúc, số đếm chỉ hiện một phần (0/1/2 bị cắt). Form và widget vẫn tải được (không còn
modal lỗi như ảnh sweep-E — vì trên binary HEAD các widget đã chạy, xem §3.1).
- Ảnh desktop đối chứng: `shots-sweep/resweep2-pds-home-desktop.png` (Home STG 200).
- **Giữ deferred với đúng lý do cũ:** thu hẹp shell AdminLTE đầy đủ (sidebar-mini, control
sidebar, hệ tab, `body overflow:hidden`) về 390px cần tái cấu trúc shell (hamburger, tràn
tab, reflow widget), không phải 1 media query tối thiểu; worker verify-only không tự redesign.

## 4. Các mục §7 của C2 còn nợ

| Mục | Kết quả |
|---|---|
| MrFeeItem/Save | **ĐÃ CHẠY ĐƯỢC (nay đã kiểm chứng, không còn nợ):** tạo nhóm phí mới qua app (Code 11) rồi POST `Save` với list đúng dạng UI gửi (`list[0].sfxmCode=20240613, list[0].sfxmmc=test, code=11` — lấy từ `Form.cshtml:224` của MrFeeItem, grid row + code) → success; SQL xác nhận dòng mới trong `mr_dic_sfxmrel` (sfxm=20240613, feetypecode=11, zt=1, CreatorCode=000000); đã dọn sạch |
| MrFeeRel/Save | **ĐÃ CHẠY ĐƯỢC (nay đã kiểm chứng, kèm ngữ nghĩa thật):** lần đầu POST không Id → success nhưng 0 dòng mới — vì `MrFeeRelDmnService.SubmitForm` là UPDATE theo `Id`, không phải INSERT (đọc code `MrFeeRelDmnService.cs:111-123`). Chạy lại đúng ngữ nghĩa: tạo 2 nhóm (RelA Code 11, RelB Code 12), tạo rel row qua MrFeeItem/Save (Id `72a2e281-…`, feetypecode=11), rồi POST `MrFeeRel/Save` với `list[0].Id + feetypecode=12` → success, SQL xác nhận feetypecode 11→12, zt vẫn 1; đã dọn sạch (rel 0, nhóm RS2 0) |
| SubmitZdList / SubmitOpList / SubmitMR | **VẪN CHƯA CHẠY (không chạy mù — lý do chính xác, mạnh hơn C2):** `OpListSubmit` bắt buộc phải có bản ghi basy cơ bản trước (`BAH + OrganizeId + zt=1`, không có thì throw "Thông tin bệnh án cơ bản của bệnh nhân chưa được nhập, không thể lưu."). Tạo basy qua `Mrbasy/SubmitForm` tối thiểu (chỉ `ZYH`, rồi `ZYH + YLFKFS=1`) đều lỗi NOT NULL — bảng `mr_basy` có 23 cột NOT NULL không default, gồm cả trường định danh bệnh nhân thật (`XM`, `SFZH`, `CSRQ`, `RYSJ`, `MZZD`, …). Dựng đủ form này đồng nghĩa bịa hồ sơ bệnh nhân — worker không làm. Ngoài ra `SubmitZdList` còn bị gate kép: chỉ lưu khi `SettlementQuery(zyh)` có dòng VÀ config `DiagnosticSave(Code)` ở trạng thái ON với điều kiện thời gian (không có settlement thì trả `{"data":""}` mà không lưu, không lỗi). Cần dữ liệu nghiệp vụ thật (bệnh nhân đã quyết toán + config ON) mới kiểm chứng được nhánh lưu của ZdList/SubmitMR |
| BljghDataDealwith dạng ghi (chỉ STG) | **ĐÃ CHẠY ĐƯỢC 2 nhánh trên STG, không gọi prod:** (1) `blmc` không khớp regex nào → `code 20000 INTERNAL_SERVER_ERROR "Value cannot be null. (Parameter 'pattern')"` — **phát hiện mới mức thấp:** 1 dòng `bl_ElementDomain` đang hoạt động (`Zt=1`) có `Regex=NULL` nên `new Regex(null)` throw trước khi tới nhánh ghi; không ghi gì. (2) `blmc="入院记录"` (khớp regex → bảng `TB_BL_RYJL`), `delzt=0`, `blId=RS2-NONEXISTENT` → `code 10000`, count `TB_BL_RYJL` giữ nguyên 2, 0 dòng mang blId RS2 — nhánh xóa chạy đúng, 0 dòng bị ảnh hưởng, không cần dọn. Nhánh ghi dữ liệu cấu trúc đầy đủ (delzt≠0) chưa chạy vì cần bản ghi bệnh án + element thật khớp regex — ghi rõ, không tuyên bố xong nhánh này |
| PatList với tham số ngày/zyh | **ĐÃ CHẠY, vẫn rỗng — không kết luận là bug:** `PatList?type=0/1/2` không tham số và `type=0&zyh=01000&kssj=2024-01-01&jssj=2026-12-31` đều trả `rows=[]`. Nguyên nhân theo code: `RecordController.PatList` lọc theo `UserIdentity.rygh` (demo user 000000) qua `_EMRDmnService.GetPagintionList` — STG không có bệnh nhân thỏa điều kiện lọc theo rygh này trong luồng Record (khác với grid MainRecord có dữ liệu). Rỗng ở đây là điều kiện dữ liệu, chưa có đối chiếu để gọi là lỗi |
| Ảnh MrDept mobile | **ĐÃ CHỤP ĐƯỢC LẦN NÀY (không còn nợ):** `shots-sweep/resweep2-mrdept-mobile.png` — trang Khoa bệnh án STG HEAD ở 390×844 render tốt: toolbar (Làm mới/Tạo mới/Sửa/Xóa), lưới 10 dòng/trang, phân trang "Trang 1 trên tổng số 4 trang / 37 bản ghi". Ghi nhận nhỏ (thẩm mỹ, không chặn): tên khoa dài bị cắt ở biên phải (lưới chưa reflow hoàn toàn trên mobile) |
| BL_Save / TemplateDesign/BtnSubmit (cần editor DCSoft) | **KHÔNG KIỂM CHỨNG ĐƯỢC (Windows-only) — ghi rõ, không tuyên bố xong, không bịa:** code HEAD vẫn giữ stub tường minh (`MedicalRecordController.cs:123`: "Trình soạn thảo văn bản DCSoft chưa khả dụng trên.NET Core (không có bản build.NET Core). Cần thay bằng trình soạn thảo web khác."); `BL_Save` (controller `:1345`) và `TemplateDesignController.BtnSubmit` (`:56`, gọi `BL_Save` ở `:97`) đều tiêu thụ nội dung do editor DCSoft sinh ra (xmlContent / `HljldataVO` từ designer UI) — không có editor trên Linux thì không có nội dung thật để chạy. Cần quyết định thay thế editor (sản phẩm), ngoài phạm vi verify-only |

## 5. Hồi quy nhanh

- **Login prod 000000:** 8/8 web app thành công ngay lần đầu (CIS 5234, Herp 5236, MRMS 5232,
OR 5231, EMR 5235, PDS 5237, HOSP 5233, Base 5238) + `Home/Index` 200 cho cả 8.
Không cookie → `Home/Index` 302 về đúng `/Login/Index` của từng app (8/8).
- **API prod validation-gate + token thật (chỉ đọc):**
- MRMS 5332: `Query {}` → 40004 `TIMESTAMP_IS_REQUIRED`; `GetToken` thật → 10000 + token;
`Query` kèm token+TokenType trong body → 10000 + danh sách `Sys_Config`. (Lưu ý probe: lần
đầu thiếu `TokenType` nên trả 40004 `TOKEN_TYPE_IS_REQUIRED` — lỗi của probe, thêm
TokenType thì pass; giống hệt OR/HOSP.)
- OR 5331: `GetToken` thật → 10000; `ORPatList` (OrganizeId demo, zyh=01000, 2024–2026) → 10000, `data: []`.
- EMR 5335: `GetToken {}` → HTTP 400 ProblemDetails `AppId là bắt buộc` (dạng `[ApiController]`,
lệch envelope như C2-B5 đã ghi — vẫn tồn tại ở đường validation, không phải regression);
`GetToken` thật → 10000; `Query` body-only → 10000, header → 10000 (cùng danh sách).
- HOSP 5333: `GetToken` thật → 10000; `MedicineStorageIOMode/Query {crkbz:"1"}` → 10000 +
danh sách phương thức xuất kho (`Xuat kho binh thuong`, …).
- **Giá trị token không ghi vào báo cáo** (chỉ ghi độ dài/kết quả).

## 6. Phát hiện mới trong phiên (không thuộc FIX-2B, ghi để vòng sau quyết — không tự sửa)

1. **BljghDataDealwith + Regex NULL** (mức thấp, STG HEAD): 1 dòng `bl_ElementDomain` với
`Zt=1` có `Regex=NULL` làm endpoint trả 20000 `INTERNAL_SERVER_ERROR` cho mọi `blmc`
không khớp regex khác trước khi tới nhánh ghi (xem §4). Sửa thuộc về dữ liệu (điền Regex
hoặc lọc NULL) hoặc guard code — worker không tự quyết.
2. **PDS getmesquery bọc lỗi khác nhau giữa prod và STG HEAD** (ghi nhận, không phải bug mới):
prod trả thẳng lỗi SQL + stack trace, STG HEAD trả message framework chung giấu stack —
cùng nguyên nhân gốc `kcyjz` (log STG đã xác nhận). Vòng sau chuẩn hóa hình thức bọc lỗi
nếu muốn, không ảnh hưởng kết luận E-03.
3. **Route probe cần chú ý** (không phải bug app): HOSP Rehab và Base Web controller không mang
tiền tố thư mục/area trong route (`/RehabChargeClassification/…`, `/SysMedicineUsage/…`);
thử kèm tiền tố thư mục trả body rỗng 404. Ghi để worker sau khỏi mất thời gian.

## 7. Ảnh đã chụp (thư mục `shots-sweep/`, đã gitignore)

- `resweep2-pds-home-mobile.png` — PDS Home STG HEAD, 390×844 (E-05 vẫn bóp méo, xem §3.2).
- `resweep2-pds-home-desktop.png` — PDS Home STG HEAD, 1440×900 (đối chứng).
- `resweep2-mrdept-mobile.png` — MrDept STG HEAD, 390×844 (lần trước timeout, lần này chụp được).
- "Mobile" = emulation viewport bằng Playwright + Firefox local. OpenHIS không có app native —
không ở đâu tuyên bố đã test app native.

## 8. Dọn dẹp cuối phiên + toàn vẹn dữ liệu (đã kiểm chứng bằng SQL)

- Mọi staging của worker này đã kill đúng PID, cổng scratch (16231–16238, 16331–16338, 17002,
17201) xác nhận DOWN, không còn process `resweep2-scratch` nào. App prod không bị kill/restart.
- Fixture RS2 đã xác nhận về 0 trên STG: CIS `xt_fzcd` = 0; MRMS `mr_dic_dept` RS2 = 0,
`mr_dic_bafeetype` RS2 = 0, `mr_dic_sfxmrel` RS2 = 0 (tổng `mr_dic_dept` về đúng baseline 43,
`mr_dic_bafeetype` về 39); Base `kf_sffl/kf_sfxm/xt_kf_sfxmdz` RS2 = 0/0/0 và 9 danh mục
int RS2 = 0; EMR `bl_rybl` (BlId `5a93fe30-…`) IsLock về NULL đúng baseline,
`TB_BL_RYJL` mang blId RS2 = 0; MRMS `mr_basy/mr_basy_ss/mr_basy_zd` với ZYH RS2OP = 0/0/0
(các lần tạo basy tối thiểu đều lỗi NOT NULL nên không insert, không cần dọn);
PDS STG marker RS2 = 0. Không ghi DB demo (0 lệnh SQL ghi vào demo).
- Git: không sửa file code nào, không sửa `roles-shots-progress.md` (trạng thái `M` của file
đó có từ trước phiên), không commit. Báo cáo này là file untracked mới trong
`scripts/sweep-final/` (cố ý, chờ điều phối viên); ảnh `shots-sweep/` đã gitignore.
- Tóm tắt 1 dòng: **FIX-2B đạt trên tất cả các mục kiểm chứng được (PROD + STG HEAD), trừ
3 mục vẫn nợ có lý do chính xác: B-CIS-04 happy path (route `/api/Patient/OutPatientRegistrationQuery`
không tồn tại ở bất kỳ host nào đã port — Sett API scratch chỉ có Booking/PatientAnon/
OrderCenter, dù proc `usp_interface_OutPatientRegistrationQuery` vẫn có trong Sett STG),
ProfitLossReason (không bảng, không route gọi SubmitForm — chỉ kiểm chứng bằng code),
và SubmitZdList/SubmitMR nhánh lưu (cần basy thật + settlement + config ON) cùng
BljghDataDealwith nhánh ghi đầy đủ (cần bản ghi + element thật) và BL_Save/BtnSubmit
(cần editor DCSoft Windows-only).**
