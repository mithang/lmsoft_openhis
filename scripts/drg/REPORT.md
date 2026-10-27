# Báo cáo Giai đoạn 5 — Kiểm chứng live 3 endpoint DRG (host 8016)

- Ngày kiểm chứng: 2026-10-27 (buổi tối, giờ ICT).
- Người thực hiện: worker Giai đoạn 5. KHÔNG commit, KHÔNG push (báo cáo này là file
  untracked duy nhất phát sinh; điều phối viên soát rồi commit nếu cần).
- Repo: `~/workspace/openhis-core`, nhánh `V2.2`, tip lúc bắt đầu: `d8d9aea1`
  ("Giai doan 4: EMR Web + API chuyen Debug sang Release"), `git status` sạch
  lúc bắt đầu và lúc kết thúc (ngoại trừ chính file báo cáo này, chưa add).
- Spec đối chiếu: `~/workspace/production-readiness-design.md` §3.3 (staging) và §3.5
  (kế hoạch 4 bước DRG). Hai điểm trong spec đã được hiệu chỉnh bằng code thực tế
  (xem §1.4 và §5).

## 0. Trạng thái host trước kiểm chứng

- 8016 (WebAPI.Manage) LISTEN, PID 15602, CWD
  `Newtouch.HIS.Center/NewtouchHIS.WebAPI.Manage`, chạy bằng `run-apimanage.sh`
  (Development, override `SiteSettAPIHost` → 7002). 8015 (AuthCenter) LISTEN.
- Đĩa: `/home/hatch` trống 66 GB, `/` trống 7,2 GB — đủ cho mọi restore nếu cần,
  nhưng thực tế KHÔNG CẦN restore STG mới (lý do ở §2).
- RAM lúc bắt đầu: tổng 7935 MB, available ~611 MB (eo hẹp nhưng host 8016 đứng
  vững suốt đợt kiểm chứng, không crash, không phải dừng app nào khác).

## 1. Contract từng endpoint (đọc code, không đoán)

File controller:
`Newtouch.HIS.Center/NewtouchHIS.WebAPI.Manage/Areas/DRGS/Controllers/DRGGrouperController.cs`,
`[Route("api/[controller]")]` + `[ApiController]`, tất cả đều **HTTP POST**.
Envelope ngoài do `AppResultFilter` bọc: `{HttpStatus, Message, BusData, TimeOut}`,
trong đó `BusData = {code, msg, data}` với `code`: 10000 = SUCCESS, 20000 = ERROR
(lỗi nội bộ/model validation), 40004 = FAIL nghiệp vụ. HTTP luôn 200 kể cả khi lỗi
nghiệp vụ (trừ lỗi auth cũng trả HTTP 200 với `HttpStatus: 401`).
**Tất cả các route đều yêu cầu JWT Bearer** (global `AuthorizeFilter` + JWT auth);
không token → `{HttpStatus: 401, Message: "Xác thực token thất bại:"}` (đã kiểm
chứng bằng 1 request không token trước khi lấy JWT).

### 1.1 `POST api/DRGGrouper/DrgGroupByMedRecord`

- Request: `RequestBus<DrgAreaMedicalRecord>` = `{AppId (bắt buộc), Timestamp
  ("yyyy-MM-dd HH:mm:ss"), Token/TokenType (tùy chọn), Data}`.
- `Data` = `DrgMedicalRecord` + `version` (+ `versionPolicy` tùy chọn):
  `{Index, gender, age, ageDay, weight, dept, inHospitalTime, leavingType,
  zdList: string[], ssList: string[], remark, version}`.
- Validation ở controller: `Data == null` → FAIL 40004 "Thông tin bệnh án quan
  trọng không được để trống". Nếu `version` trống → mặc định
  `SysConfig.DrgGroupVersion` (appsettings 8016: `"chs_drg_11"`) — tức enum
  `chs_drg_11 = 11` → policy `ChsDrg11Policy`.
- Validation ở policy (`DrgGroupBasePolicy.Validata`): `zdList` rỗng → ném
  `FailedException` (xem §3.2 về thông điệp thực tế). Lưu ý: các trường
  non-nullable (`zdList`, `ssList`, `FileCols`, …) bị `[ApiController]` coi là
  bắt buộc — thiếu hẳn (null) thì trả ERROR 20000 dạng "The zdList field is
  required." trước khi vào policy (đã kiểm chứng, §3.2 ca 2c/2f).
- Engine: `ChsDrg11Policy` chuyển record thành chuỗi input
  `Index,gender,age,ageDay,weight,dept,inHospitalTime,leavingType,"zd1|zd2","ss1|ss2",remark`
  rồi gọi `GroupHelper.GroupRecord` → native `drg_group.GroupProxy.group_record`
  (file `chs_drg_11.dll` + dữ liệu ICD `ICD/SS_INFO.dat, SS_MAP.dat, ZD_INFO.dat,
  ZD_MAP.dat` trong thư mục host). **Thuần in-memory, không đọc/ghi DB.**
- Response `data` = `DrgGroupResult {Index, status, messages: string[],
  mdc, adrg, drg, record: null}`. `status` = "分组成功" khi nhóm thành công;
  `drg` = "00" khi không nhóm được, "*QY" khi vào nhóm mơ hồ (theo chú thích
  trong model).

### 1.2 `POST api/DRGGrouper/DrgGroupByMedRecordList`

- Request: `RequestBus<DrgAreaMedicalRecordS>` với `Data = {version,
  versionPolicy?, medicalList: DrgMedicalRecord[], medicalArr: string[]}`.
  `medicalArr` là các chuỗi input đã định dạng sẵn (cùng định dạng
  `ToInputString()` ở trên); nếu `medicalList` có phần tử thì policy nối
  `medicalList` (đã convert) + `medicalArr` rồi nhóm hàng loạt bằng
  `GroupHelper.GroupRecord(string[])`.
- Validation ở controller: `Data == null` HOẶC (`medicalArr` rỗng VÀ
  `medicalList` rỗng) → FAIL 40004 (thông điệp dài, xem §3.2 ca 2d).
  Lưu ý code controller dereference `medicalArr.Length`/`medicalList.Count`
  trực tiếp — nếu caller chỉ gửi một trong hai trường (trường còn lại null)
  thì có nguy cơ NullReference thay vì FAIL gọn; trong đợt này luôn gửi đủ
  cả hai trường nên không kiểm chứng nhánh null một phía (ghi nhận là rủi ro
  nhỏ, không chặn).
- Response `data` = `List<DrgGroupResult>` (cùng shape như 1.1, `record` không
  có trong kết quả list).
- Cũng **không đọc/ghi DB**.

### 1.3 `POST api/DRGGrouper/DrgGroupByXls`

- Request: `RequestBus<DrgAreaFileInfo>` với `Data = {FileName, FilePath,
  FileExtName, FileCols: string[], IsTmpFile (mặc định true), version,
  versionPolicy?}`.
- **QUAN TRỌNG — đây KHÔNG phải endpoint upload file.** Nó chỉ nhận tên file
  đã nằm sẵn trên đĩa của host và đọc file đó:
  - `Validata(ref DrgAreaFileInfo)` tính `defaultPath =
    <CWD của host> + SysConfig.FileConfig.DrgDataFileDirPath` (= `/File` +
    `/Drg` theo appsettings 8016 → thư mục `File/Drg` trong thư mục host).
  - Nếu `FilePath` + `FileName` tồn tại thì dùng trực tiếp; nếu không thì tìm
    đệ quy trong `defaultPath` theo mẫu `{FileName}*.csv` và `{FileName}`,
    lấy file đầu tiên KHÔNG chứa `_result.csv`. Không tìm thấy → ném
    `FailedException` "Vui lòng tải lên tệp Drg liên quan trước".
  - Luồng upload file thật nằm ở controller khác: `FileController.DrgExcelUpload`
    (nhận `IFormFile`, lưu `<guid>.xlsx`, convert sang `<guid>.xlsx.csv`
    bằng Spire.XLS) — caller phải upload trước rồi mới gọi endpoint này với
    tên file CSV đã convert.
- Engine: `GroupHelper.GroupFileRecord(file, FileCols)` gọi native
  `group_csv(file, cols)` — native đọc CSV đầu vào, ghi file kết quả
  `<tên>_csharp_result.csv` (thay hậu tố `.csv`), rồi policy đọc lại file kết
  quả bằng `GroupFileReadAsync` với 16 cột kết quả cố định
  `Index,gender,age,ageDay,weight,dept,inHospitalTime,leavingType,zdList,ssList,remark,status,messages,mdc,adrg,drg`
  (khớp đúng header file kết quả mẫu năm 2023 còn lưu trong `File/Drg`).
- Sau khi trả kết quả, `FileRecordEndProcAsync` **XÓA** mọi file khớp
  `{FileName}*.csv` / `{FileName}` trong `defaultPath` nếu `IsTmpFile != false`
  (mặc định true) — tức gọi với mặc định sẽ xóa cả file đầu vào lẫn file kết
  quả. Đợt này đặt `IsTmpFile: false` để giữ bằng chứng (và để không xóa
  nhầm file mẫu gốc 2023).
- Response `data` = `DrgGroupFileResult {file (tên file kết quả), rows:
  List<DrgMedicalRecordResult>}` (mỗi row = record + status/messages/mdc/adrg/drg).
- Cũng **không đọc/ghi DB** — chỉ đọc/ghi file trong `File/Drg` của host.
- **Rủi ro crash đã thấy trong code (chưa kích hoạt trong đợt này):**
  `GroupFileReadAsync` gọi `System.Environment.Exit(-1)` nếu header file kết
  quả thiếu cột hoặc nếu mở file bị `IOException` — tức một file kết quả lỗi
  có thể giết chết tiến trình host 8016 (không chỉ trả lỗi). Ghi nhận để
  giai đoạn sửa chữa xử lý; đợt này không cố tình kích hoạt.

### 1.4 Hiệu chỉnh so với spec §3.5

- Spec viết "engine là binary-only". Thực tế: lớp vỏ grouping (`GroupHelper`,
  `MedicalRecord`, policy `ChsDrg11Policy`/`DrgGroupBasePolicy`) **có source**
  trong cây; chỉ lõi `drg_group.GroupProxy` (native `chs_drg_11.dll`) là
  binary. Kiểm chứng này do đó là kiểm chứng hành vi của cả vỏ + lõi.
- Spec §3.5 bước 4 giả định "ghi kết quả grouping vào DB". Thực tế **không có
  endpoint nào trong 3 endpoint ghi kết quả vào DB** (xem §2) — bước 4 theo
  đúng chữ không thực thi được; xem kết luận §4.

## 2. Bước 0 — Staging và chứng minh không ghi DB demo

### 2.1 Xác định DB/bảng liên quan (grep toàn bộ code đường đi DRG)

- Grep `DbContext|SqlHelper|INSERT|UPDATE|Execute|ConnectionString` trong
  `Lib.Services/DrgGroup/`, `DrgGroup.chs_drg_11/`, `DrgGroup.chs_drg_10/`,
  `DrgGroup.wuhan_2022/` → **0 kết quả**.
- `LogTrackingAttribute` của 8016 chỉ ghi `ILogger` (file/console), không ghi DB.
- Host 8016 lúc khởi động có đọc cấu hình từ `NewtouchHIS_Base` (ConfigInit),
  nhưng các request DRG không phát sinh câu lệnh DB nào.
- **Kết luận bước 0: không có DB nào mà 3 endpoint có thể ghi → không cần
  tạo DB `*_STG` mới cho giai đoạn này, và không cần trỏ host sang staging.**
  DB `NewtouchHIS_Sett_STG` từ giai đoạn 3 vẫn tồn tại (8301 MB) và được giữ
  nguyên, không tạo trùng, không xóa. Toàn bộ kiểm chứng chỉ đọc DB demo ở
  mức tra cứu bảng `Sys_Auth_App`/`Sys_Auth_AppFriend` (để tìm cặp friend cho
  JWT) và đếm dòng trước/sau — không có INSERT/UPDATE/DELETE nào vào DB demo
  từ phía worker.

### 2.2 Số liệu đếm trước/sau (chứng minh không ghi demo)

Tổng số dòng (sum `row_count` của `sys.dm_db_partition_stats`, index 0/1):

| DB | Trước | Sau | Chênh lệch |
|---|---|---|---|
| NewtouchHIS_Base | 1.540.778 | 1.540.778 | 0 |
| NewtouchHIS_Sett | 52.033 | 52.033 | 0 |
| Newtouch_CIS | 36.635 | 36.635 | 0 |
| Newtouch_EMR | 43.216 | 43.216 | 0 |
| NewtouchHIS_Sett_STG (giai đoạn 3) | 52.055 | 52.055 | 0 |

Kích thước DB (MB) lúc đầu: Sett 8301, Sett_STG 8301, Base 2540, CIS 3665,
EMR 112, PDS 1618, Interface 656 — không đổi sau kiểm chứng (đếm dòng đã đủ
chứng minh; kích thước file DB không co/giãn theo từng request đọc).

### 2.3 File staging cho bước 3 (không phải DB)

- File staging duy nhất đã tạo: `File/Drg/DRGSTG-20261027.csv` (357 byte,
  UTF-8 BOM, 2 dòng dữ liệu — cùng 2 bản ghi với bước 1 và bước List),
  header = đúng 12 cột CSV mẫu 2023:
  `病案号,性别,年龄,新生儿年龄天,新生儿出生体重,出院科室,住院天数,离院方式,总费用,诊断编码,手术操作编码,医保类型`.
- File đã **xóa sau kiểm chứng** (cùng file kết quả nếu có — thực tế không có
  file kết quả nào được tạo vì bước 3 lỗi trước khi gọi engine, xem §3.3).
  Các file mẫu gốc 2023 trong `File/Drg/20231026` và `File/Drg/20231030`
  nguyên vẹn, mtime vẫn 26/10 (không bị `FileRecordEndProcAsync` xóa vì đợt
  này đặt `IsTmpFile: false` và vì bước 3 lỗi trước cả bước đọc file).

### 2.4 Cách lấy JWT cho 8016 (tiền lệ giai đoạn 3, đã tái hiện)

- `POST http://127.0.0.1:8015/api/Auth/GetAppToken`, body `Request<string>`
  `{AppId: "CIS", Timestamp, Data: "API.Manage"}` — cặp friend đã kiểm chứng
  trong DB demo: `Sys_Auth_AppFriend` (zt=1) có `API.Manage ← CIS` (cùng
  EMR, MRQC, PDS, SETT, SelfTerminal, Clinic).
- Kết quả: `BusData.code = 10000`, JWT dài 1100 ký tự (giá trị KHÔNG in ra
  đây; chỉ dùng qua file tạm `/tmp/drg_jwt.txt`, không đưa vào git).
- Mọi request DRG sau đó gửi header `Authorization: Bearer <JWT>`.

## 3. Kết quả từng bước (theo đúng thứ tự, dừng ở bước lỗi)

### Bước 1 — Bản ghi staging hợp lệ, chỉ đọc: ĐẠT ✅

- Request: `POST api/DRGGrouper/DrgGroupByMedRecord`, `Data` = đúng bản ghi
  ví dụ trong XML comment của controller (dữ liệu mẫu của vendor, không phải
  bệnh nhân thật): `Index=22058878, gender=2, age=88, ageDay=32460, weight=0,
  dept=13040503, inHospitalTime=94, leavingType=1,
  zdList=[K80.302, K80.305, K83.109, K72.905, Z90.408, E14.900x001],
  ssList=[51.8803, 51.8701, 54.5100x005, 45.1301], version=chs_drg_11`.
- Response: HTTP 200, `BusData.code = 10000`, `msg` rỗng,
  `data = {Index: "22058878", status: "分组成功", mdc: "MDCH", adrg: "HC1",
  drg: "HC15", record: null}`.
- `messages` (tóm tắt): tên chẩn đoán/phẫu thuật theo mã ICD, "符合MDCH入组条件"
  (đủ điều kiện vào MDCH), "符合HC1入组条件", "主诊断K80.302排除表118",
  "***HC15 胆总管手术，不伴并发症或合并症***".
- Nhận xét: grouping đúng và đầy đủ (mã DRG + MDC + ADRG + trace từng bước).
  Toàn bộ `status`/`messages` trả về **bằng tiếng Trung** — đây là dữ liệu
  của engine CHS-DRG (file `.dat` của vendor), không phải text code; đợt dịch
  tiếng Trung trước đó không chạm tới dữ liệu engine. Nếu mục tiêu là giao
  diện hoàn toàn tiếng Việt thì đây là việc còn lại thuộc về engine/dữ liệu
  vendor, không phải lỗi chức năng.

### Bước 2 — Bản ghi thiếu trường, host không crash: ĐẠT ✅

Mỗi ca xong đều gọi lại 1 request và host vẫn trả HTTP 200 ngay sau đó;
cuối bước 2 PID 15602 vẫn sống, 8016 vẫn LISTEN.

| Ca | Request | Kết quả |
|---|---|---|
| 2a | Single, `Data = null` | HTTP 200, code 40004, "Thông tin bệnh án quan trọng không được để trống" |
| 2b | Single, `zdList = []` | HTTP 200, code 40004, msg tiếng Trung "诊断信息不可为空" (xem lưu ý dưới) |
| 2c | Single, thiếu hẳn trường `zdList` (null) | HTTP 200, code 20000 (ERROR), "The zdList field is required." — validation của `[ApiController]` cho trường non-nullable, chặn trước policy nên không NRE |
| 2d | List, `medicalList = []` và `medicalArr = []` | HTTP 200, code 40004, "Thông tin bệnh án quan trọng không được để trống,medicalArr và medicalListÍt nhất một mục không được để trống" |
| 2e | List, `Data = null` | HTTP 200, code 40004, cùng thông điệp như 2d |
| 2f | Xls, `FileName = ""` (thiếu cả FileCols/FilePath/FileExtName) | HTTP 200, code 20000, "The FileCols field is required.;The FilePath field is required.;The FileExtName field is required." — lưu ý: controller chỉ kiểm tra `FileName` trống, nhưng model validation chặn trước vì các trường kia non-nullable |

- Không ca nào trả HTTP 500, không ca nào làm host crash.
- Lưu ý về ca 2b: source hiện tại (`DrgGroupBasePolicy.cs:36`) ném thông điệp
  tiếng Việt "Thông tin chẩn đoán không được để trống", nhưng host đang chạy
  trả tiếng Trung. Nguyên nhân: `NewtouchHIS.Lib.Services.dll` trong bin của
  8016 có mtime 26/10 17:30 (trước đợt dịch 27/10), trong khi
  `NewtouchHIS.WebAPI.Manage.dll`/`NewtouchHIS.Lib.Base.dll` đã rebuild
  27/10 06:27 — tức **bin đang chạy của 8016 lệch phiên bản giữa các DLL**
  (controller đã dịch, policy chưa). Đây cũng là manh mối cho blocker ở
  bước 3. Không tự rebuild/restart trong giai đoạn kiểm chứng; việc đồng bộ
  lại bin thuộc về giai đoạn sửa chữa (xem §5).

### Bước 3a — List so với Single (bổ sung cho đối chiếu): ĐẠT ✅

- Request: `DrgGroupByMedRecordList` với `medicalList` = [bản ghi bước 1,
  bản ghi STG-2 (`Index=STG-2, gender=1, age=45, dept=13100003, days=7,
  zdList=[Z51.100x004, C34.900x001], ssList=[99.2503]`)], `medicalArr = []`.
- Response: code 10000, 2 kết quả: `22058878 → MDCH/HC1/HC15` (khớp 100% bước 1)
  và `STG-2 → MDCR/RW2/RW23` (khớp với dòng kết quả mẫu 2023 trong
  `71e455..._csharp_result.csv` cho cùng cặp chẩn đoán/phẫu thuật).
- Kết luận phần List: grouping hàng loạt nhất quán với grouping từng bản ghi.

### Bước 3 — Grouping theo file XLS so với từng bản ghi: KHÔNG ĐẠT ❌ (BLOCKER)

- Request 1: `POST api/DRGGrouper/DrgGroupByXls`,
  `Data = {FileName: "DRGSTG-20261027.csv", FilePath: "", FileExtName: ".csv",
  FileCols: [12 header CSV mẫu 2023], IsTmpFile: false, version: "chs_drg_11"}`.
- Response: HTTP 200, `BusData.code = 20000`,
  `msg = "[Ngoại lệ]:Method not found: 'NewtouchHIS.Lib.Base.Model.FileManageConfig
  NewtouchHIS.Lib.Base.Model.BusinessConfig.get_FileConfig()'."`,
  `data = null` — **không có kết quả grouping, không tạo file kết quả**.
- Request 2 (xác nhận lỗi không phụ thuộc FilePath): cùng payload nhưng
  `FilePath: "/"` → **cùng lỗi, cùng code 20000**. Host vẫn sống sau cả hai
  lần (PID 15602, 8016 LISTEN) — lỗi được exception handler bắt, không crash.
- Chẩn đoán (bằng chứng, không đoán):
  - `DrgGroupBasePolicy.Validata(ref DrgAreaFileInfo)` — dòng đầu tiên đã đọc
    `ConfigInitHelper.SysConfig!.FileConfig!.DrgDataFileDirPath`. Mọi request
    Xls đều đi qua dòng này trước khi chạm tới file, nên mọi biến thể
    FilePath/FileName đều lỗi giống nhau (đã chứng minh bằng request 2).
  - Lỗi `MissingMethodException` nghĩa là `NewtouchHIS.Lib.Services.dll`
    đang chạy (26/10) được biên dịch với một `Lib.Base` có getter
    `BusinessConfig.FileConfig` trả về `FileManageConfig`, còn
    `NewtouchHIS.Lib.Base.dll` đang chạy (27/10 06:27) không cung cấp đúng
    method đó. Source tại HEAD (`BusinessConfig.cs:83`) VẪN định nghĩa
    `FileConfig`, nên đây là **lệch DLL triển khai**, không phải thiếu code
    trong cây — hướng sửa là rebuild + redeploy đồng bộ bin của 8016 từ
    HEAD rồi live-test lại bước 3 (thuộc giai đoạn sửa chữa, không làm trong
    giai đoạn kiểm chứng này).
  - Hệ quả: **không đối chiếu được XLS vs từng bản ghi** — bước 3 chưa có
    bằng chứng nào về tính nhất quán của đường file. File staging CSV đã
    chuẩn bị sẵn (cùng 2 bản ghi với bước 1/3a) vẫn còn nguyên nội dung trong
    báo cáo này để giai đoạn sau dùng lại mà không cần tạo lại.
- Theo quy tắc "dừng ngay khi một bước bất thường", **không chạy tiếp bước 4**.

### Bước 4 — Ghi kết quả grouping vào DB STAGING: KHÔNG THỰC HIỆN ⛔

- Điều kiện tiên quyết trong thiết kế ("chỉ khi 3 bước trên đều đạt") KHÔNG
  thỏa vì bước 3 lỗi → bước 4 bị chặn theo đúng quy tắc, không ghi gì cả.
- Ngoài ra, như đã chứng minh ở §1/§2: **không tồn tại thao tác ghi kết quả
  grouping vào DB trong 3 endpoint** (không có câu lệnh DB nào trên đường đi
  DRG; "ghi" duy nhất của đường file là file `_csharp_result.csv` trong
  `File/Drg`, mà đường này cũng đang lỗi ở bước 3). Vì vậy bước 4 cần được
  điều phối viên định nghĩa lại cho giai đoạn sau, ví dụ: sau khi sửa blocker
  bước 3, kiểm chứng rằng file kết quả `_csharp_result.csv` được ghi đúng
  trong thư mục staging của host và đối chiếu `rows` với kết quả Single/List
  — thay vì "ghi vào DB STG".

## 4. Trạng thái cuối

- Host 8016: vẫn LISTEN, PID 15602 không đổi từ đầu đến cuối đợt; 8015 vẫn
  LISTEN. Không restart host nào, không dừng app nào.
- DB: mọi đếm dòng trước/sau bằng nhau (§2.2) — **không ghi DB demo**.
- File: file staging CSV đã xóa; file mẫu 2023 nguyên vẹn; cây git sạch
  ngoại trừ file báo cáo này (untracked, chưa add/commit/push theo lệnh).
- RAM cuối đợt: host đứng vững, không có dấu hiệu cạn RAM thêm.

## 5. Blocker và việc cần làm tiếp (cho điều phối viên / giai đoạn sửa chữa)

1. **BLOCKER — lệch DLL bin 8016 làm hỏng toàn bộ đường Xls (bước 3):**
   rebuild + redeploy đồng bộ `NewtouchHIS.WebAPI.Manage` bin từ HEAD
   (đặc biệt `Lib.Services` + `Lib.Base`), restart 8016 bằng
   `run-apimanage.sh`, rồi live-test lại bước 3 với đúng file staging đã mô
   tả ở §2.3 (nội dung 2 dòng đã ghi trong báo cáo này) trước khi coi giai
   đoạn DRG là xong. Đồng thời kiểm tra lại thông điệp ca 2b sau rebuild
   (phải ra tiếng Việt như source).
2. **Rủi ro crash tiềm ẩn (chưa kích hoạt):** `GroupFileReadAsync` gọi
   `Environment.Exit(-1)` khi header file kết quả thiếu cột hoặc lỗi IO —
   nên sửa thành trả lỗi thay vì giết tiến trình host trước khi đưa đường
   Xls vào production.
3. **Định nghĩa lại bước 4:** không có thao tác ghi DB trong 3 endpoint;
   điều phối viên chốt lại tiêu chí bước 4 (đề xuất: file kết quả staging +
   đối chiếu rows) thay vì "ghi DB STG".
4. **Việc còn lại về ngôn ngữ (không chặn chức năng):** `status`/`messages`
   của engine CHS-DRG trả về tiếng Trung từ dữ liệu vendor (`.dat`); nếu yêu
   cầu "toàn tiếng Việt" bao gồm cả trace grouping thì cần phương án riêng
   với nhà cung cấp engine/dữ liệu — ngoài phạm vi sửa code đợt này.
5. **Rủi ro nhỏ (không chặn):** controller List dereference trực tiếp
   `medicalArr`/`medicalList` — caller chỉ gửi một trong hai trường có thể
   gây NRE thay vì FAIL gọn; nên thêm null-check khi sửa chữa.

## 6. Tóm tắt kết quả (để đối chiếu nhanh)

| Hạng mục | Kết quả |
|---|---|
| DrgGroupByMedRecord (single) | ĐẠT — 10000, HC15/MDCH/HC1 |
| DrgGroupByMedRecordList | ĐẠT — 10000, khớp 100% single (HC15, RW23) |
| DrgGroupByXls | KHÔNG ĐẠT — 20000 MissingMethod FileConfig, chưa có kết quả để đối chiếu |
| Bước 1 (single, chỉ đọc) | ĐẠT |
| Bước 2 (thiếu trường, không crash) | ĐẠT (6 ca, host sống sau mỗi ca) |
| Bước 3 (XLS vs từng bản ghi) | KHÔNG ĐẠT — blocker DLL (§5.1) |
| Bước 4 (ghi staging) | KHÔNG THỰC HIỆN — bị chặn bởi bước 3 + không có thao tác ghi DB trong endpoint |
| Không ghi DB demo | CHỨNG MINH — đếm dòng trước/sau bằng nhau ở 5 DB (§2.2) |
| DB STG mới | KHÔNG TẠO — không cần (endpoint không chạm DB); Sett_STG giai đoạn 3 giữ nguyên |
| Diff code | KHÔNG CÓ — chỉ có file báo cáo này (untracked) |
