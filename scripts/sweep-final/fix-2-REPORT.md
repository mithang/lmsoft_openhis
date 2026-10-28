# BÁO CÁO FIX-2 — Vòng fix sweep cuối OpenHIS (worker FIX-2B tiếp nối FIX-2)

Ngày: 2026-10-28. Base lúc bắt đầu: V2.2 HEAD `e66c7bda`; trong phiên, FIX-1
commit xong `3f8d9723` (SSO/MRQC/Sett/Center — không trùng file nào với phạm vi
này, đã đối chiếu `git show 3f8d9723 --stat`). Commit của phiên này nằm trên
`3f8d9723`, CHỈ gồm pathspec phạm vi FIX-2 + báo cáo này (không `git add -A`,
không đụng `roles-shots-progress.md`, không DLL/PDB, không secret, không đổi
schema demo, không deploy prod).

## 0. Tình trạng bàn giao — đã kiểm chứng lại, không tin mù

- FIX-2 kết thúc đột ngột, không báo cáo tổng kết, không commit. Toàn bộ sửa code
của FIX-2 nằm uncommitted trong working tree; FIX-2B soát từng file, kiểm
chứng live từng bug, sửa thêm những chỗ FIX-2 sót/sai, rồi mới commit.
- Instance CIS scratch mồ côi PID 15053 (cwd `fix2-scratch/cis`, cổng 16234):
kiểm chứng lại login thành công ngay đầu phiên, dùng tiếp cho toàn bộ kiểm
chứng CIS, rebuild 1 lần (xem B-CIS-05), cuối phiên kill đúng PID.
BaseAPI scratch FIX-2 PID 14071 (cổng 17201) giữ lại làm BaseAPI staging cho
mọi kiểm chứng, cuối phiên kill đúng PID. Không còn tiến trình fix2-scratch
mồ côi nào khác (Herp/MRMS/OR/EMR/PDS chỉ có thư mục, không có process).
Cuối phiên: không còn process fix2-scratch nào, mọi cổng scratch đã DOWN.
- Fixture STG tồn đọng: `Newtouch_CIS_STG.dbo.xt_fzcd` về baseline 0 dòng
(không có dòng SWEEP/FIX2 nào, không có dòng tạo ngày 2026-10-28) — FIX-2 đã
dọn hoặc chưa kịp tạo; không cần dọn thêm trước khi kiểm chứng mới.
- Mọi kiểm chứng ghi CHỈ trên DB `*_STG`, dọn sạch fixture, count về baseline.
Không ghi DB demo. Login kiểm chứng duy nhất: 000000 / md5(000000) qua
`md5sum`; không lần nào báo khóa/sai (không tốn lượt lockout).
- Build: tuần tự, mỗi lần 1 build (FIX-1 cũng đang build song song lúc đầu,
tổng tối đa 2). Build từ bản sao `fix2-src` (FIX-2 đã đổi `UseUrls` sang cổng
scratch trong bản sao này; `openhis-core` giữ nguyên cổng prod) rồi publish
sang `fix2-scratch/<app>`. Quy tắc copy-DLL: kill app trước, publish sau,
restart ngay — không để tiến trình cũ chạy trên DLL mới.
- Sửa của FIX-2 đã có trong diff được giữ nguyên trừ khi ghi rõ FIX-2B sửa thêm.

## 1. Kết quả từng bug

### B-CIS-01 (cao) — MainBusiness/Index crash duplicate key ISOpenZycfnew
- Sửa đã có sẵn (FIX-2): `Views/MainBusiness/Index.cshtml` dùng indexer
`["ISOpenZycfnew"] =...` thay cho `Add` khi tạo `ViewDataDictionary`.
- Bằng chứng STG: login CIS scratch 16234 thành công →
`GET /MainBusiness/Index` = 200, body HTML (không còn JSON lỗi duplicate key).
Kiểm lại sau rebuild CIS: vẫn 200.
- Kết luận: XONG.

### B-CIS-02 (cao) — Tạo từ điển phụ lỗi NULL Id
- Sửa đã có sẵn (FIX-2): `ServiceCompat.Create` sinh GUID khi khóa string/Guid
trống (theo mẫu Herp), bỏ qua khóa int (DB tự sinh).
- Bằng chứng STG: `POST /SystemManage/AuxiliaryDictionary/SubmitForm`
(không Id, `Code=FIX2B-TEST`) → `success "Thao tác thành công."`; SQL xác nhận
dòng mới Id GUID `c4e1de09-…`, `CreatorCode=000000`, `zt=1`.
- Kết luận: XONG.

### B-CIS-03 (cao) — Sửa từ điển phụ lỗi NULL CreatorCode
- Sửa đã có sẵn (FIX-2): `SysAuxiliaryDictionaryRepo` giữ CreatorCode/CreateTime
gốc khi form post trống.
- Rà edit-path cùng mẫu trong CIS (FIX-2B rà toàn bộ, xem §2): FIX-2 đã sửa
thêm 6 repo cùng mẫu: `InpatientDietBaseRepo`, `InspectionCategoryRepo`,
`PatientVitalSignsRepo`, `Pacs_ExamBodyPartsRepo`, `SysBodyPartsRepo`,
`SysDoctorRemarkRepo`. FIX-2B sửa thêm 1 repo cùng mẫu còn sót:
`SysMedicineProfitLossReasonRepo.SubmitForm` (entity mới chỉ 4 field +
full-column Update). Hạn chế: bảng `xt_ksbysyyy` KHÔNG có trên STG và không
có route web nào gọi `SubmitForm` này (chỉ `GetLossProfitReasonListByType`
được dùng) nên chỉ kiểm chứng bằng code, KHÔNG có bằng chứng live.
- Bằng chứng STG (AuxiliaryDictionary): sửa dòng FIX2B-TEST đổi Name →
success; `GetFormJson` + SQL xác nhận Name mới, `CreatorCode=000000` giữ
nguyên, `CreateTime` giữ nguyên. Xóa sau đó success, count về 0.
- Kết luận: XONG (trừ ProfitLossReason: sửa code, chưa kiểm chứng live).

### B-CIS-04 (trung bình) — Grid chờ khám NRE cả GET lẫn POST
- Sửa đã có sẵn (FIX-2): `PatientListController` inject `visitDeptSetRepo`
(trước đây field không bao giờ được gán), guard `pagination` null, guard
`apiResp`/`data`/`list`/`pagination` null.
- Bằng chứng STG: GET và POST `SelectNotYetTreateList` đều hết NRE, trả JSON
lỗi nghiệp vụ đúng dạng `{"state":"error","message":"Gọi giao diện thất bại"}`
(Sett API host 18016 không chạy trong môi trường scratch nên API trả null —
đúng nhánh guard). Happy path (Sett API trả dữ liệu thật) CHƯA kiểm chứng
được vì không có Sett API scratch khỏe; ghi rõ, không tuyên bố ngược.
- Kết luận: XONG ở mức guard (happy path chưa kiểm chứng).

### B-CIS-05 (trung bình) — Trang truyền dịch lỗi với đầu vào rỗng/sai
- Sửa đã có sẵn (FIX-2): `InfusionManageController.Query` luôn bind
`ViewBag.drugs`/`patInfo`, `TryParse` tags, thông báo qua
`ViewBag.InfusionNotice` + view `query.cshtml` hiển thị.
- Sửa thêm của FIX-2B (nguyên nhân gốc còn sót): `InfusionApp` KHÔNG CÓ
constructor DI nên 4 field readonly luôn null → mọi id dạng số hợp lệ đều
NRE tại lambda `MzsyxxQuery` (`InfusionApp.cs:293`), dù controller đã guard
tags rỗng/sai định dạng. FIX-2B thêm constructor DI (cả 4 service đều đã
đăng ký trong `Program.cs`; mẫu theo `BespeakRegisterApp`).
- Bằng chứng STG (sau rebuild CIS): tags rỗng → view + thông báo
"Chưa nhập mã phiếu truyền dịch."; tags `abc` → "Mã phiếu truyền dịch không
hợp lệ."; tags số không tồn tại `999999999` → view + "Không tìm thấy phiếu
truyền dịch." (trước fix FIX-2B: NRE thô). Lưu ý: thông báo render dạng
HTML-entity cho dấu tiếng Việt nên grep phải theo dạng entity.
- Kết luận: XONG (đủ 3 dạng đầu vào).

### B-CIS-06 (thấp) — Hàng đợi thiếu ngày → SqlDateTime overflow
- Sửa đã có sẵn (FIX-2): `QueueInfoController.TryQueueRange` (parse + khoảng
1753-01-01..9999-12-30) cho cả `GetQueueInfo` và `getNextCall`.
- Bằng chứng STG: không ngày, ngày `1000-01-01`, và `getNextCall` không ngày
đều trả `{"state":"error","message":"Vui lòng chọn ngày xếp hàng hợp lệ
(từ 01/01/1753 đến 30/12/9999)."}`, không còn SqlDateTime overflow.
- Kết luận: XONG.

### B-HERP-01 (thấp) — Thông báo lỗi DB Herp chứa tiền tố "中文提示:"
- Gốc đã xác định (FIX-2B): tiền tố nằm trong message của chính DLL SqlSugar
(`SqlSugar.Check.Exception`: format "中文提示: … English Message: …"),
KHÔNG có trong source app (grep toàn bộ Herp/Base/Lib chỉ thấy trong log
và DLL). Không sửa được bằng chuỗi app, không dịch framework.
- Sửa của FIX-2B (chỉ chuỗi hiển thị, đúng phạm vi task): bọc try/catch tại
seam là 3 action `HomeController.GetPsiByKf`, `GetRkCountByLx`, `getmesquery`,
trả `Error("Không thể lấy dữ liệu từ cơ sở dữ liệu. Vui lòng thử lại sau.")`.
Đường thành công không đổi. Đánh đổi ghi rõ: chi tiết lỗi thô không còn
trong JSON (log server vẫn là nơi chẩn đoán).
- Bằng chứng STG: (1) đường khỏe: cả 3 endpoint trả dữ liệu như cũ
(12 tháng = 0; `[]`; rows rỗng). (2) đường lỗi — tái hiện bằng instance Herp
trỏ HerpDb sang DB không tồn tại `NoSuchDb_FIX2B` (chỉ đổi env instance đó,
không chạm dữ liệu STG): cả 3 endpoint trả đúng thông báo tiếng Việt, không
còn "中文提示". Bonus: login trên instance DB-sai tái hiện đúng message gốc
tiếng Trung, củng cố cơ chế lỗi. Sau đó khôi phục Herp khỏe, đường khỏe
kiểm lại vẫn đúng.
- Kết luận: XONG.

### C2-B1 (cao) — MRMS MrDept sửa/xem/xóa lỗi SqlSugar "parameter format is wrong"
- Sửa đã có sẵn (FIX-2): `MRMS RepositoryBase.FindEntity` truyền
`SugarParameter[]` thay cho 1 `SugarParameter` đơn lẻ.
- Bằng chứng STG (MRMS scratch 16232): tạo `SubmitDicDept` (ksbm=FIX2B1) →
success, Id `ef8ebb1f-…`; `GetFormJson` trả đúng entity; sửa đổi tên →
success, SQL xác nhận tên mới; `DeleteData` → success, `zt` 1→0 (xóa mềm
theo thiết kế repo — sweep C2 đếm không lọc `zt` nên tưởng còn dòng).
Dọn vật lý bằng SQL sau đó, count về 0.
- Kết luận: XONG.

### C2-B2 (cao) — MRMS MrFee tạo mới NRE tại MrFeeRepo.SubmitForm:40
- Sửa đã có sẵn (FIX-2): `flag = oldEntity!= null && (...)` (tạo mới thì
oldEntity null → đi nhánh insert).
- Bằng chứng STG: tạo mới đúng field UI (`name=FIX2B-NhomPhi, Lev=1`) →
success; SQL xác nhận dòng `mr_dic_bafeetype` mới (Code tự sinh 11, Lev 1,
CreatorCode 000000, zt 1). Lưu ý probe: lần đầu gửi sai field (`name1` thay
vì `name`) nên lỗi NULL Name — lỗi của probe, không phải app; gửi đúng field
thì pass. Xóa qua app `DeleteData` → success, count về 0.
- Kết luận: XONG.

### C2-B4 (cao, gãy cả prod) — EMR MRHomePage grid lỗi 'dbo.mr_dic_common'
- Sửa đã có sẵn (FIX-2): truy vấn qua `DbNames` sang MRMS DB (+ BaseDb cho
`V_S_Sys_Department`), không tạo bảng, không đổi schema demo.
- Sửa thêm của FIX-2B (lỗi do chính FIX-2 gây ra): FIX-2 đặt comment C# nhiều
dòng BÊN TRONG chuỗi SQL verbatim — chỉ dòng đầu có `--`, các dòng sau
thành SQL thô → lỗi `Incorrect syntax near 'context'` khi chạy thật.
FIX-2B chuyển comment ra ngoài chuỗi SQL.
- Bằng chứng STG (EMR scratch 16235, sau rebuild): `GetGridList` trả rows
(dòng G7-EMR-BASY, ZYH 01000) — hết 'Invalid object name', hết lỗi cú pháp.
- Kết luận: XONG (hướng DbNames đã chọn, lý do như FIX-2: YLFKFS là dữ liệu
hiển thị thật của trang đầu, không dùng lưới rỗng).

### C2-B5 (thấp) — EMR NursingRecord grid không ngày → "Nullable object must have a value"
- Sửa đã có sẵn (FIX-2): cả 3 `NursingRecord*DmnService` trả lưới rỗng khi
thiếu kssj/jssj (+ guard zyh rỗng).
- Bằng chứng STG: `GetGridJson` không ngày với hllx=1 (WZ), 2 (SS), 99 (YB
nhánh else) đều trả `[]`.
- Kết luận: XONG.

### C2-B6 (cao trên staging) — MRMS RecordDmnService 6 helper hardcode [Newtouch_EMR] + message LockRecord
- Sửa đã có sẵn (FIX-2): 6 helper (`bl_rybl/bl_bcjl/bl_zqws/bl_hljl/bl_basy/
zy_meddocs_relation GetByID`) chuyển sang `DbNames` (giữ nguyên tên bảng
`bl_bcjl` của `bl_zqwsGetByID` như gốc, chỉ đổi tên DB); message khóa →
"Khóa thành công" / "Đã bị khóa" / "Mở khóa thành công" theo nhánh.
- Bằng chứng STG: chu trình trên `BlId=5a93fe30-c562-…` (baseline IsLock NULL,
LastModifierCode 000000): khóa (`isToLock=1`) → "Khóa thành công", SQL
IsLock NULL→1; mở (`isToLock=0`) → "Mở khóa thành công", IsLock 1→0; khôi
phục IsLock về NULL bằng SQL đúng baseline.
- Kết luận: XONG.

### EMR B1/B2 + nền login mobile (C-B4) (thấp)
- Sửa đã có sẵn (FIX-2): banner IE bọc `<!--[if lte IE 8]>` (Firefox coi như
comment thường nên ẩn); nhãn tab rút gọn "Chào mừng đăng nhập" → "Chào
mừng"; CSS mobile `@media (max-width: 640px)` ở cả 2 bản `login.css`
(EMR; MRMS/OR cũng có bản tương tự).
- Bằng chứng STG: HTML login EMR phục vụ chứa conditional IE + nhãn
"Chào mừng"; CSS phục vụ chứa media query 640px.
- Kết luận: XONG (kiểm chứng qua HTML/CSS phục vụ; không có live browser để
chụp mobile nên không tuyên bố bằng ảnh).

### C2-B3 (thấp) — OR OPStaff grid có keyword: rows rỗng nhưng total/records=1
- Sửa đã có sẵn (FIX-2): dùng `pagination.total/page/records` thật thay cho
hardcode 1.
- Bằng chứng STG (OR scratch 16231): keyword không khớp → rows [] với
total 0/records 0; không keyword → 1 dòng với total/records 1. Khớp nhau.
- Kết luận: XONG.

### E-01 (cao) — HOSP tạo phân loại khoản thu PHCN lỗi NULL sfflId
- Sửa đã có sẵn (FIX-2): `EntityBase.SetPk` chuyển Guid→string bằng
ToString/Parse thay cho `Convert.ChangeType` (bị catch rỗng nuốt).
- NHƯNG kiểm chứng live cho thấy FIX-2 CHƯA ĐỦ — 2 lỗi gốc sâu hơn do FIX-2B
tìm ra bằng chương trình test.NET mini chạy đúng DLL đang chạy:
1. `FindPkProperty` (EntityBase + 2 bản trong RepositoryBase) chỉ khớp tên
attribute `"SugarColumnAttribute"`, trong khi SqlSugar đặt tên là
`"SugarColumn"` → không bao giờ khớp, rơi về prop `"Id"` (không có) →
`SetPk` thành no-op, mọi `Create(true, Guid)` đều để PK NULL.
(Tiền lệ: EMR `Repositories.cs:198` đã xử lý cả 2 tên.)
2. Riêng EntityBase còn sai `at.GetType().GetProperty("IsPrimaryKey")`
(at đã là Type nên tra cứu trên RuntimeType → luôn null); phải là
`at.GetProperty(...)`. RepositoryBase dùng `a.GetType()` nên chỉ sai tên.
- Sửa thêm của FIX-2B: cả 3 điểm trên + mẫu B-CIS-03 cho 3 repo Rehab (xem
dưới). Sau rebuild HOSP, test console xác nhận `SetPk(Guid)` đặt đúng
`sfflId` dạng chuỗi.
- Sửa thêm mẫu B-CIS-03 (phát hiện khi tạo đã chạy được thì sửa mới lộ lỗi):
cả 3 repo Rehab (`Classification/Item/Comparison`) đều Update full-column
bằng posted entity → sửa ghi NULL CreatorCode (NOT NULL). Đã giữ
CreatorCode/CreateTime gốc (Classification/Item đã nạp oldEntity sẵn;
Comparison nạp thêm oldEntity qua `FindEntity` — nay đã chạy được nhờ sửa
RepositoryBase).
- Bằng chứng STG (HOSP scratch 16233): tạo Classification → success, sfflId
GUID, CreatorCode 000000; sửa → success, tên mới + CreatorCode/CreateTime
giữ nguyên; tạo Item → success (sfxmId GUID); sửa Item → success;
tạo Comparison → success (sfxmdzId GUID, bảng `xt_kf_sfxmdz`); sửa
Comparison → success; xóa cả 3 qua app → success, count về 0/0/0.
- Kết luận: XONG đủ vòng tạo/sửa/xóa cả 3 repo cùng mẫu.

### E-02 (cao) — Base Web tạo danh mục khóa int lỗi IDENTITY
- Sửa đã có sẵn (FIX-2): thêm `IsIdentity = true` cho 9 entity khóa int +
`RepositoryBase` FindEntity/Delete truyền `SugarParameter[]` (cùng lỗi định
dạng như C2-B1).
- Bằng chứng STG (Base scratch 16238, rebuild với infra mới nhất): tạo mới
thành công cả 9 danh mục, ID tự tăng: Usage 62 (yfCode FIX2B-E, cần thêm
`yplx=1` — field bắt buộc của form, probe đầu thiếu nên lỗi NULL yplx,
không phải bug app), Nation 72, Unit 611, Classification 243, Formulation
72, IOMode 17 (cần thêm `crkbz=1` — select bắt buộc của form, tương tự),
Nationality 263, Diagnosis 248600, TCMSyndrome 1. Xóa Usage qua app
`DeleteForm` → success, dòng biến mất; 8 mục còn lại KHÔNG có action xóa
qua app (chỉ Usage controller có Delete) nên xóa bằng SQL; tổng tồn đọng
về 0.
- Kết luận: XONG 9/9 tạo; xóa: 1 qua app + 8 qua SQL (ghi rõ lý do).

### E-03 (trung bình) — PDS Home staging lỗi framework tiếng Trung + modal chặn trang
- TÁI HIỆN TRÊN BINARY HEAD TRƯỚC (PDS scratch 16237, FIX-2 build = HEAD đối
với PDS vì diff không có file PDS nào): dạng báo cáo KHÔNG tái hiện.
`GetPendingCount` trả counts thật (kể cả expiryDrugCount 216),
`GetNeedDealDiv` trả success + HTML, `GetFyCountBydl` → `[]`,
`GetFyCountVoByYfbm` → 2 dãy tháng toàn 0, `Home/Index` = 200 (23550 byte).
→ Phần "3 widget + modal framework" là artifact binary staging cũ.
- Tồn dư khác dạng (ghi rõ, KHÔNG tự sửa): `getmesquery` vẫn lỗi, nhưng với
lỗi SQL thật `Invalid column name 'kcyjz'` (không phải message framework):
SQL `MSGQuery` chọn `kcyjz` từ `dbo.xt_yp_bmypxx` nhưng cột này KHÔNG TỒN
TẠI ở cả DB demo lẫn STG (kiểm chứng INFORMATION_SCHEMA cả 2 chiều, chỉ
đọc). `xt_ypsx.kcyjz` có tồn tại ở Base STG nhưng toàn NULL, và
`GetDrugAndStock` cũng chọn `kcyjz` trần cùng khoảng trống schema — tức
khoảng trống schema theo phiên bản DB, không phải lỗi gõ code. Sửa code
lúc này phải tự bịa ngưỡng tồn kho lấy từ đâu (hoặc đổi schema demo — bị
cấm) nên KHÔNG tự quyết; để lại cho quyết định schema/sản phẩm. Modal
trên binary HEAD chưa kiểm chứng bằng browser (subagent không có live
browser) — chỉ có bằng chứng endpoint.
- Kết luận: E-03 dạng báo cáo = artifact binary cũ; tồn dư getmesquery-kcyjz
= MỞ, cần quyết định schema.

### E-05 — PDS Home mobile bóp méo (đã deferred)
- Đánh giá lại: GIỮ DEFERRED. Trang Home PDS là shell AdminLTE desktop đầy đủ
(sidebar-mini, control sidebar, hệ tab, `body overflow:hidden`,
`Layout = null`) — thu hẹp về 390px cần tái cấu trúc shell (hamburger, tràn
tab, reflow widget), không phải 1 media query tối thiểu như trang login;
media query token có nguy cơ phá desktop mà subagent không có live browser
để kiểm chứng mobile. Lý do giữ deferred ghi rõ như trên.
- Kết luận: GIỮ DEFERRED (không sửa).

### EMR API header-only token (điều tra hành vi gốc)
- Kết quả: GỐC NHẬN TOKEN TRONG BODY. `RequestBase` gốc của FrameworkBase.API
(mirror ở PDS/MRMS/OR) có trường `Token` trong body; bản port EMR làm mất
trường này nên body token bị bỏ qua. FIX-2 đã khôi phục: `ApiCommon`
thêm `Token` (+ `TokenType`), `AuthGetTokenRequest` bỏ khai riêng TokenType
(tránh hide), `ApiControllerBase` giải identity từ body trước khi kiểm tra
(header vẫn ưu tiên, không đổi contract client đang dùng header).
- Bằng chứng STG (EMR API scratch 16335): `GetToken` → 10000 + token;
`POST /api/Default/Query` chỉ gửi token TRONG BODY → 10000 + data
(trước fix: 40004 TOKEN_IS_EXPIRED theo sweep C2); chỉ gửi qua header →
vẫn 10000 (không hồi quy).
- Kết luận: XONG — khôi phục contract gốc, không đổi contract.

### Herp API GetToken trên STG (từng bị chặn vì BaseAPI staging lỗi DB)
- Kiểm chứng lại trên staging khỏe (BaseAPI scratch 17201 + Herp API 16336):
`POST /api/Auth/GetToken` (AppId Herp, credential đúng) → 10000 + token.
Không sửa code (chặn trước đây là do staging BaseAPI lỗi DB, nay đã khỏe).
- Kết luận: XONG, không cần sửa code.

## 2. Rà edit-path cùng mẫu B-CIS-03 trong CIS (ghi rõ đã rà)

Mẫu nguy hiểm = posted-entity + full-column `Update(entity)` (không qua
dbEntity đã nạp). Đã rà toàn bộ `Newtouch.CIS.Infrastructure/Repository`
(theo `SubmitForm` + mọi điểm gọi `Update(entity)`):
- ĐÃ SỬA (FIX-2): AuxiliaryDictionary, InpatientDietBase, InspectionCategory,
PatientVitalSigns, Pacs_ExamBodyParts, SysBodyParts, SysDoctorRemark.
- ĐÃ SỬA (FIX-2B): SysMedicineProfitLossReasonRepo (xem B-CIS-03).
- AN TOÀN theo mẫu merge/load-trước-sửa (không cần sửa): BlctglRepo,
InpatientBedBaseChargeItemRepo, InpatientBedChargeItemRepo,
InpatientBedUseRecordRepo, InpatientChargeItemDietExeRepo,
InpatientDietDetailSplitRepo, InpatientDietSfxmdyRepo,
InpatientDietTemplateDetailSplitRepo, InpatientFeeDetailRepo,
InpatientLongTermOrderRepo, InpatientMedicineGrantRepo,
InpatientMedicineReturnRepo, InpatientMedicinejxCodeComparedRepo,
InpatientOrderPackageDetailRepo, InpatientOrderPackageRepo,
InpatientPatientDiagnosisRepo, InpatientPatientDoctorRepo,
InpatientPatientInfoRepo (cả SubmitForm dbEntity lẫn UpdateInpatientStatus
load-trước), InpatientSTATOrderRepo, InpatientVitalSignsRepo,
MedicalAdviceBindingFeeRepo, QhdZnshSqtxRepo, KcPdxxmxRepo, KcSyyyRepo,
KfKcxxRepo, MzsyypxxRepo, MzsyzxxxRepo, MzyyghRepo, PrescriptionRepo
(UpdateChargeStatus/UpdateChargeTbz load-trước), TreatmentRepo
(ResumeTreat load-trước), ClinicApplyRepo, DeptStorageManage các repo còn
lại, InpatientBedCardRepo, InpatientOperationArrangementRepo.

## 3. Không sửa + lý do (kể cả ngoại lệ tuyệt đối)

- OR DeleteForm bỏ comment: KHÔNG SỬA (cần quyết định sản phẩm).
- E-06 placeholder GetDeptPharmacyDrugs: KHÔNG SỬA (cần chủ sở hữu xác nhận).
- E-07 tên Trung trên demo: KHÔNG SỬA (phạm vi dữ liệu).
- E-04 thông điệp lỗi framework tiếng Trung: KHÔNG DỊCH framework; chỉ sửa
chuỗi hiển thị có seam sẵn (B-HERP-01, 3 action Herp).
- Chuẩn hóa Debug→Release launcher: KHÔNG LÀM (quyết phase 8).
- Xoay secret, bắt đổi mật khẩu lần đầu: KHÔNG LÀM (chờ quyết định user).
- E-03 tồn dư getmesquery-kcyjz: KHÔNG TỰ SỬA (khoảng trống schema, xem §1).
- E-05 PDS Home mobile: GIỮ DEFERRED (cần redesign, xem §1).
- B-CIS-04 happy path (Sett API trả dữ liệu thật): CHƯA KIỂM CHỨNG (không có
Sett API scratch khỏe); chỉ xác nhận hết NRE + lỗi đúng dạng.
- B-CIS-03 ProfitLossReason: sửa code nhưng CHƯA KIỂM CHỨNG LIVE (bảng không
có trên STG, không có route web gọi SubmitForm).
- Không có bug nào trong danh sách được kết luận là artifact binary staging cũ
ngoài E-03 dạng báo cáo (xem §1) — mọi bug còn lại đều tái hiện/kiểm chứng
được trên binary HEAD hoặc đã sửa và kiểm chứng.

## 4. Fixture tồn đọng đã dọn (tất cả STG, count về baseline)

- CIS `xt_fzcd`: baseline 0 từ đầu; fixture FIX2B-TEST tạo → sửa → xóa qua app,
count về 0.
- MRMS `mr_dic_dept` FIX2B1 (tạo → sửa → xóa mềm zt=0 → xóa vật lý SQL): 0.
- MRMS `mr_dic_bafeetype` FIX2B-NhomPhi (tạo → xóa qua app → xóa SQL dự phòng): 0.
- MRMS/EMR `bl_rybl 5a93fe30-c562-…`: khóa → mở → IsLock khôi phục NULL đúng
baseline (chỉ LastModifyTime đổi theo giờ gọi, như sweep C2 đã ghi).
- Base `kf_sffl/kf_sfxm/xt_kf_sfxmdz` (FIX2B-E/XM/KF): tạo → sửa → xóa qua app,
count 0/0/0.
- Base 9 danh mục khóa int (code FIX2B-E): tạo → xóa (Usage qua app, 8 qua SQL),
tổng tồn đọng 0.
- Không tạo fixture nào trên DB demo; không còn process scratch nào chạy.

## 5. File đổi trong commit này (phạm vi FIX-2)

FIX-2 (đã có trong diff, nay commit): Base 9 entity IsIdentity + EntityBase
SetPk Guid→string + RepositoryBase SugarParameter[]; CIS ServiceCompat
(Create sinh GUID, Modify nhận keyValue), IDBDmnService null-guard, 7 repo
giữ CreatorCode, QueueInfoController guard ngày, InfusionManageController +
query.cshtml, PatientListController DI+guard, MainBusiness Index.cshtml;
EMR ApiControllerBase/ApiCommon/AuthGetTokenRequest (body token),
MRHomePageDmnService DbNames, 3 NursingRecordDmnService, login EMR (IE
conditional + nhãn tab + CSS mobile); MRMS RepositoryBase FindEntity[],
MrFeeRepo null-guard, RecordDmnService 6 helper DbNames, RecordController
message, login.css mobile; OR OPStaffController pagination thật, login.css
mobile.
FIX-2B sửa thêm: CIS InfusionApp constructor DI, CIS
SysMedicineProfitLossReasonRepo giữ CreatorCode, EMR MRHomePageDmnService
(chuyển comment ra khỏi chuỗi SQL), Base EntityBase FindPkProperty (khớp cả
"SugarColumn" + sửa `at.GetProperty`), Base RepositoryBase FindPkProperty
(khớp cả "SugarColumn", 2 điểm), 3 repo Rehab giữ CreatorCode/CreateTime,
Herp HomeController catch 3 action (B-HERP-01).
