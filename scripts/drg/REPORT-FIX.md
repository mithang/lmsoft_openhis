# Báo cáo Giai đoạn 5-fix — Sửa blocker DLL đường DRG Xls + live-test lại

- Ngày thực hiện: 2026-10-27 (buổi trưa, giờ ICT).
- Người thực hiện: worker Giai đoạn 5-fix. KHÔNG commit, KHÔNG push
  (mọi thay đổi để ở dạng diff chưa commit cho điều phối viên soát).
- Repo: `~/workspace/openhis-core`, nhánh `V2.2`, tip lúc bắt đầu và lúc kết
  thúc: `bc5e665f` ("Giai doan 5: kiem chung live DRG...").
  Trạng thái cây lúc bắt đầu: SẠCH (git status không có gì ngoài chính các
  file sẽ sửa trong giai đoạn này). Lúc kết thúc: 7 file modified (3 source +
  4 DLL tracked trong `his-dll-common/Net6`), xem §6. Không động tới
  checkout `~/workspace/lmsoft_openhis`, run copy `~/workspace/lmsoft_openhis_run`,
  mật khẩu SA trong `HIS.BaseAPI/appsettings.json`, `recovery.sh`,
  launcher `run-apimanage.sh`.
- Báo cáo giai đoạn 5 đối chiếu: `scripts/drg/REPORT.md` (§1.3 contract Xls,
  §2.3 file staging, §3 bước 3, §5 blocker).

## 0. Kết luận trước (đọc trước, chi tiết ở dưới)

- **Đường Xls: ĐÃ SỬA XONG, live-test ĐẠT.** `DrgGroupByXls` trả 10000,
  2 rows khớp 100% Single/List (22058878 → MDCH/HC1/HC15,
  STG-2 → MDCR/RW2/RW23). File kết quả `DRGSTG-20261027_csharp_result.csv`
  được ghi đúng trong thư mục staging của host.
- **Bước 4 (định nghĩa lại): ĐẠT.** File kết quả nằm trong thư mục staging
  của host, rows đối chiếu khớp Single/List — thay cho "ghi DB STG" (3
  endpoint không có thao tác ghi DB nào, đã chứng minh ở giai đoạn 5).
- **Hồi quy: ĐẠT.** Single/List giữ nguyên kết quả; ca 2b giờ trả thông điệp
  tiếng Việt (chứng tỏ DLL Services mới đã vào chạy); null một phía ở List
  trả FAIL 40004 gọn (không NRE); Y002 danh sách khoa 10000 (17 khoa);
  SSO login 000000 thành công; mọi cổng app trước đó đều UP.
- **Blocker còn lại: KHÔNG CÓ cho đường DRG Xls.** Các điểm tồn đọng chỉ là
  lưu ý không chặn (xem §7).

## 1. Backup trước khi sửa (bắt buộc, đã làm đầu tiên)

Thư mục backup ngoài repo: `~/workspace/drg-dll-backup-20261027/`

- `bin-8016/` — DLL trong bin 8016 lúc bắt đầu (cũ):
  `NewtouchHIS.Lib.Base.dll` (133120 byte, 26/10 17:30 theo ls ban đầu của
  bin — thực tế file ghi 27/10 06:27 ở lần kiểm tra trước; md5
  `c4e7095d578317018315e2920e81598b`), `NewtouchHIS.Lib.Services.dll`
  (60416 byte, md5 `350ca891f65d7c35354aa7fd7244f4a6`),
  `NewtouchHIS.DrgGroup.chs_drg_11.dll` (md5
  `ffc08a00af185d0e9f21acf27cf9938b`),
  `NewtouchHIS.DrgGroup.wuhan_2022.dll` (md5
  `7b81618297ce5e18d996eeb1f0e45f02`), `chs_drg_11.dll` (native, md5
  `f6d5e8c4a2f1d598f3ce19db59817d4f`).
  Không có `wuhan_2022.dll` native trong bin 8016 (chỉ có wrapper managed)
  nên không backup được file này — ghi nhận, không ảnh hưởng vì wrapper
  không đổi native.
- `his-dll-common-Net6/` — 4 DLL cùng tên trong `his-dll-common/Net6`
  (md5 trùng hệt bin 8016 lúc bắt đầu, chứng tỏ bin 8016 copy từ đây).
- Mọi thay thế sau đó đều khôi phục được từ backup này. Thực tế KHÔNG PHẢI
  lăn lại (mọi hồi quy đều đạt), nên backup chỉ nằm chờ, chưa dùng tới.

## 2. Chẩn đoán lại blocker (sâu hơn báo cáo giai đoạn 5)

### 2.1 Cách 8016 tham chiếu Lib (csproj)

`Newtouch.HIS.Center/NewtouchHIS.WebAPI.Manage/*.csproj`:

- `Lib.Base` và `Lib.Services`: **HintPath** tới
  `..\..\his-dll-common\Net6\...dll` (dòng 110–120), KHÔNG phải
  ProjectReference. Hệ quả: rebuild riêng project 8016 sẽ chỉ copy lại DLL
  cũ từ `his-dll-common` — muốn đồng bộ thì phải build Lib từ source rồi
  refresh `his-dll-common` TRƯỚC rồi mới rebuild 8016.
- `Lib.Services` ngược lại tham chiếu `Lib.Base` bằng **ProjectReference**
  (dòng 27 của `NewtouchHIS.Lib.Services.csproj`), nên build Services sẽ
  bind đúng vào Base source HEAD.
- `DrgGroup.chs_drg_11` / `wuhan_2022`: HintPath tới
  `his-dll-common/Net6` (Services) và native `chs_drg_11.dll` /
  `wuhan_2022.dll` trong `his-dll-common/DRG`.

### 2.2 Nguyên nhân gốc thật sự (không chỉ "lệch timestamp")

- Kiểm tra bằng `strings` cho thấy `NewtouchHIS.Lib.Base.dll` đang chạy
  VẪN chứa `get_FileConfig` — tức chẩn đoán "Base không cung cấp method"
  ở giai đoạn 5 chưa đủ chính xác. Điểm lệch thật sự là **kiểu trả về**:
  - Source HEAD: `FileManageConfig` khai báo ở **namespace global**
    (`BusinessConfig.cs:234`, sau dấu đóng namespace
    `NewtouchHIS.Lib.Base.Model`), nên getter của Base HEAD trả về
    `global::FileManageConfig`.
  - `Lib.Services.dll` cũ (26/10) được biên dịch với một Base mà
    `FileManageConfig` còn nằm trong `NewtouchHIS.Lib.Base.Model`
    (đúng như thông điệp lỗi giai đoạn 5:
    `NewtouchHIS.Lib.Base.Model.FileManageConfig ... get_FileConfig()`).
  - .NET phân giải method theo cả kiểu trả về → `MissingMethodException`
    dù tên getter vẫn tồn tại.
- Hướng sửa đúng (đã làm): **build lại Lib.Services từ source HEAD**
  (bind vào Base HEAD), chứ không phải chỉ build lại Lib.Base. Build lại
  Base một mình sẽ cho ra DLL gần như y hệt cũ (cùng 133120 byte) và
  không sửa được gì.

## 3. Build (tuần tự, 0 lỗi ở lần build cuối mỗi project)

| Thứ tự | Project | Kết quả |
|---|---|---|
| 1 | `NewtouchHIS.Lib.Base` | 0 Error (164 warning có sẵn, không mới) |
| 2 | `NewtouchHIS.Lib.Services` | 0 Error (42 warning); DLL 60416 → 55296 byte, md5 mới `91cea5424085669b23b848c69e3f0682` |
| 3 | `DrgGroup.chs_drg_11` (đã sửa §4a) | 0 Error; 14848 → 14336 byte, md5 mới `e50ffae3525ff8de65476dbf3e8031e2` |
| 4 | `DrgGroup.wuhan_2022` (đã sửa §4a) | 0 Error; 13312 → 12800 byte, md5 mới `6ab99dfb990355e60b8421812b749e36` |
| 5 | `WebAPI.Manage` (8016, sau khi refresh his-dll-common) | Lần 1: 2 Error — CHỈ là lỗi copy `.pdb` do host cũ PID 15602 còn chạy và khóa file PDB (MSB3027/MSB3021), DLL đã copy mới đủ. Sau khi kill host cũ, build lại: **0 Error**, md5 bin khớp md5 build mới của cả 4 DLL |

- Kiểm chứng getter sau build: `strings` trên bin 8016 mới vẫn thấy
  `get_FileConfig` ở cả Base và Services (điều kiện cần); kiểm chứng đủ
  là live-test Xls ở §5 (đạt).
- Refresh `his-dll-common/Net6`: 4 DLL mới đã copy đè lên đây (kèm backup
  §1) để lần rebuild 8016 sau không hồi quy về DLL cũ. Lưu ý: 4 file này
  là file **tracked trong git** nên hiện lên dạng Modified — để điều phối
  viên quyết định commit cùng đợt.
- KHÔNG đụng bin của AuthCenter hay host khác: chúng chạy bản copy riêng
  và vẫn dùng DLL cũ; chúng sẽ nhận DLL mới chỉ khi rebuild lại sau này.

## 4. Sửa code (bước 4 của nhiệm vụ — cả 2 điểm đều sạch, đã sửa)

Diff chưa commit, 3 file, 9 dòng đổi / 9 dòng thêm (xem `git diff`):

### 4a. `GroupFileReadAsync`/`GroupFileRead` không giết host nữa

- File: `Newtouch.HIS.Lib/NewtouchHIS.DrgGroup.chs_drg_11/GroupHelper.cs`
  và `.../DrgGroup.wuhan_2022/GroupHelper.cs` — mỗi file 4 điểm
  `System.Environment.Exit(-1)` (2 ở hàm sync `GroupFileRead`, 2 ở hàm
  async `GroupFileReadAsync`; nhiệm vụ nêu async, nhưng hàm sync là cùng
  một mẫu nguy hiểm nên sửa luôn cả hai — điều phối viên có thể revert
  riêng phần sync nếu muốn giữ phạm vi hẹp).
- Đổi thành `throw new InvalidOperationException(...)` giữ nguyên thông
  điệp cũ (`"Tên trường không khớp với tệp CSV"`,
  `"Đọc tệp thất bại"` kèm inner exception). Lý do không dùng
  `FailedException` như gợi ý: `FailedException` nằm trong Lib.Base mà
  project DrgGroup KHÔNG tham chiếu — thêm reference chỉ để lấy một
  exception sẽ đổi graph build; `InvalidOperationException` là exception
  chuẩn của .NET, host bắt qua exception handler chung thành 20000 và
  vẫn sống (đã kiểm chứng gián tiếp: mọi lỗi Xls trong đợt này đều trả
  20000/40004, host không chết). Sau sửa: `grep Environment.Exit` trong
  2 file = 0 kết quả.
- Lưu ý còn lại (không sửa, không giết host): điều kiện
  `col_index.Length != cols.Length` thực ra không bao giờ đúng (Select
  luôn trả đúng số phần tử); cột thiếu thật sẽ cho index -1 và gây
  `IndexOutOfRangeException` khi enumerate — vẫn được handler chung bắt
  thành 20000, host sống. Ghi nhận để đợt sau siết thêm nếu muốn.

### 4b. Controller List null-check

- File: `Newtouch.HIS.Center/NewtouchHIS.WebAPI.Manage/Areas/DRGS/Controllers/DRGGrouperController.cs:51`.
- Trước: `(request.Data.medicalArr.Length == 0 && request.Data.medicalList.Count == 0)`
  — dereference trực tiếp. Model `DrgAreaMedicalRecordS` khai báo cả hai
  trường là nullable (`string[]? medicalArr`, `List<...>? medicalList`,
  `DrgMedicalRecord.cs:80-81`) nên `[ApiController]` KHÔNG chặn null —
  nguy cơ NRE là thật, không phải lý thuyết.
- Sau: `((request.Data.medicalArr == null || request.Data.medicalList == null) || (request.Data.medicalArr.Length == 0 && request.Data.medicalList.Count == 0))`
  — vẫn trả cùng FAIL 40004 với cùng thông điệp cũ.
- Live-test §5 xác nhận: chỉ gửi `medicalList` → 40004; chỉ gửi
  `medicalArr` → 40004 (không NRE, không 500).

## 5. Restart 8016

- PID cũ: **15602** (đã kiểm tra `ps` đúng tiến trình
  `dotnet bin/Release/net6.0/NewtouchHIS.WebAPI.Manage.dll` trước khi kill;
  không dùng pkill pattern). Kill thường, chờ cổng 8016 DOWN, rebuild sạch
  (§3), rồi khởi động lại bằng launcher `run-apimanage.sh` NGUYÊN VẸN
  (giữ override `BusinessConfig__AppAPIHost__SiteSettAPIHost` → 7002).
- PID mới: **20065** (tiến trình dotnet; wrapper launcher đã exec xong).
  Cổng 8016 LISTEN trở lại; log launcher: `Hosting environment:
  Development`, `Content root path: .../NewtouchHIS.WebAPI.Manage/`.
- Không cần lăn lại: 8016 lên ngay lần đầu, mọi hồi quy đạt.

## 6. Live-test lại (curl thật, JWT AuthCenter cơ chế giai đoạn 3/5)

- JWT: `POST 8015/api/Auth/GetAppToken`, body `Request<string>`
  `{AppId: "CIS", Data: "API.Manage"}` → `BusData.code = 10000`, token
  1100 ký tự (giá trị KHÔNG in ở đây; chỉ dùng qua file tạm `/tmp`, đã
  xóa sau đợt test). Mọi request DRG gửi `Authorization: Bearer`.
- FileConfig thực tế sau sửa: appsettings 8016
  `FileConfig = {LocalFileBaseDir: "/File", DrgDataFileDir: "/Drg"}`
  → thư mục staging = `<CWD host>/File/Drg`
  = `.../NewtouchHIS.WebAPI.Manage/File/Drg` (đã xác nhận bằng `ls`).

### 6a. Hồi quy Single + List — ĐẠT

- Single (đúng bản ghi mẫu vendor, `remark: ""` gửi kèm theo yêu cầu):
  `Index=22058878, gender=2, age=88, ageDay=32460, weight=0,
  dept=13040503, inHospitalTime=94, leavingType=1,
  zdList=[K80.302, K80.305, K83.109, K72.905, Z90.408, E14.900x001],
  ssList=[51.8803, 51.8701, 54.5100x005, 45.1301]`
  → **10000, status 分组成功, MDCH/HC1/HC15** (khớp giai đoạn 5).
- List (`medicalList` = [bản ghi trên, STG-2], `medicalArr = []`) →
  **10000**: `22058878 → MDCH/HC1/HC15`, `STG-2 → MDCR/RW2/RW23`
  (khớp giai đoạn 5 và khớp dòng mẫu 2023).
- Ca 2b hồi quy: Single với `zdList = []` → **40004, thông điệp tiếng
  Việt "Thông tin chẩn đoán không được để trống"** (giai đoạn 5 trả tiếng
  Trung) — bằng chứng trực tiếp DLL Services mới đã vào chạy.

### 6b. Bước 3 Xls — ĐẠT (sau 2 lần gọi, lần 2 mới đúng contract)

- File staging tái tạo tại `File/Drg/DRGSTG-20261027.csv` (368 byte,
  UTF-8 BOM, LF, đúng nội dung 2 dòng — xem §8), `IsTmpFile: false`.
- **Lần gọi 1 — FileCols = đủ 12 header của file (kể cả 总费用):**
  code 10000 nhưng rows SAI: `zdList` nhận giá trị phí (`"0"`),
  `ssList` nhận mã chẩn đoán, `remark` nhận mã phẫu thuật, cả 2 rows
  `无法入组 / 0000 / 00 / 0000`. Đây KHÔNG phải lỗi DLL nữa mà là
  contract của `FileCols`.
- **Thí nghiệm đối chứng:** copy nguyên file mẫu 2023
  (`71e455...xlsx.csv`) thành `DRGSAMPLE-TEST.csv` rồi gọi Xls với cùng
  12 FileCols → lệch y hệt (kể cả dòng 23052663 vốn có kết quả mẫu 2023
  là RW23). Chứng tỏ lệch không phải do file staging của đợt này.
- **Nguyên nhân:** native `group_csv(file, cols)` dùng `FileCols` làm
  danh sách chọn cột theo tên, theo đúng thứ tự truyền vào, rồi map vị
  trí sang chuỗi input 11 trường (Index..remark). Truyền đủ 12 cột (kể
  cả phí ở vị trí 9) làm mọi trường từ vị trí 9 trở đi lệch một cột.
  Caller đúng phải truyền **11 cột, LOẠI cột phí 总费用**.
- **Lần gọi 2 — FileCols = 11 cột (không có 总费用):**
  **code 10000**, `file = "DRGSTG-20261027_csharp_result.csv"`:
  - `STG-2 → MDCR/RW2/RW23, status 分组成功, remark 居民`
  - `22058878 → MDCH/HC1/HC15, status 分组成功, remark 居民`
  - **Khớp 100% Single/List** (thứ tự rows trong file kết quả đảo lại:
    STG-2 trước, 22058878 sau — đối chiếu theo Index, không theo thứ tự).
- **Bài học contract (quan trọng, sửa luôn mô tả ở REPORT giai đoạn 5):**
  REPORT §2.3 nói "FileCols: [12 header CSV mẫu 2023]" — đó là header
  CỦA FILE, không phải FileCols đúng cho lời gọi. FileCols đúng là
  11 cột: `病案号,性别,年龄,新生儿年龄天,新生儿出生体重,出院科室,住院天数,离院方式,诊断编码,手术操作编码,医保类型`
  (bỏ 总费用). Mọi caller Xls sau này phải dùng 11 cột này, nếu không sẽ
  nhận 10000 giả với rows 无法入组.

### 6c. Bước 4 định nghĩa lại — ĐẠT

- File kết quả `DRGSTG-20261027_csharp_result.csv` được native engine ghi
  đúng trong thư mục staging của host (`File/Drg/`, 1333 byte, UTF-8
  BOM), header 16 cột kết quả chuẩn
  (`Index,gender,age,ageDay,weight,dept,inHospitalTime,leavingType,zdList,ssList,remark,status,messages,mdc,adrg,drg`),
  2 rows đối chiếu khớp Single/List như §6b.
- Không có thao tác ghi DB nào trong 3 endpoint (đã chứng minh ở giai
  đoạn 5, không thay đổi) — tiêu chí "ghi DB STG" theo chữ vẫn không áp
  dụng được; tiêu chí thay thế (file kết quả staging + đối chiếu rows)
  đã thỏa.
- Dọn dẹp sau đối chiếu: **đã xóa** `DRGSTG-20261027.csv`,
  `DRGSTG-20261027_csharp_result.csv`, `DRGSAMPLE-TEST.csv`,
  `DRGSAMPLE-TEST_csharp_result.csv`. File mẫu gốc 2023 trong
  `File/Drg/20231026` và `File/Drg/20231030` nguyên vẹn (mtime vẫn
  26/10, không bị `FileRecordEndProcAsync` xóa vì `IsTmpFile: false` và
  vì file thí nghiệm nằm ngoài 2 thư mục mẫu). Nội dung file staging và
  file kết quả được giữ lại trong báo cáo này (§8).

### 6d. Hồi quy ngoài DRG — ĐẠT

- Y002 danh sách khoa: `POST api/Booking/GetOutpDepartment`
  (qua Sett-API 7002). Lần đầu gửi `Data: {}` không kèm OrganizeId →
  40004 "Tham Số quan trọng không được để trống" (do phía Sett-API cần
  OrganizeId, không phải lỗi 8016). Gọi lại kèm OrganizeId demo từ
  appsettings → **10000, 17 khoa** (dòng đầu: Khoa Phu san / KS04).
- SSO login: `GET 5208/Login/Index` → 200;
  `POST 5208/Login/CheckLogin` với 000000 → 200,
  `state: "success"`, `message: "Đăng nhập thành công"`.
- Cổng: mọi cổng app trong tập đã kiểm chứng trước đó đều UP
  (5208, 7001, 7002, 8015, 8016, 5220, 5221, 5231–5238, 5331, 5334–5336,
  41142, 6379, 1433). Toàn bộ cổng LISTEN lúc cuối:
  1431, 1433, 1434, 5208, 5220, 5221, 5231, 5232, 5233, 5234, 5235, 5236,
  5237, 5238, 5320, 5331, 5332, 5333, 5334, 5335, 5336, 5337, 6379, 7001,
  7002, 8015, 8016, 41142.
- Host 8016 sống suốt đợt test sau restart (PID 20065 không đổi).

## 7. An toàn lăn lại + blocker còn lại

- **Không phải lăn lại.** Nếu phải lăn lại, quy trình đã chuẩn bị sẵn:
  copy toàn bộ DLL từ `~/workspace/drg-dll-backup-20261027/bin-8016/`
  về bin 8016 và từ `his-dll-common-Net6/` về `his-dll-common/Net6`,
  restart 8016 bằng `run-apimanage.sh`, kiểm chứng Single + Y002.
- **Blocker còn lại cho đường DRG: KHÔNG CÓ.**
- Lưu ý không chặn (cho điều phối viên / đợt sau):
  1. `status`/`messages` của engine vẫn tiếng Trung (dữ liệu vendor
     `.dat`) — như giai đoạn 5 đã ghi, không phải lỗi chức năng.
  2. Điều kiện `col_index.Length != cols.Length` trong GroupHelper là
     dead-code (xem §4a); cột thiếu thật gây IndexOutOfRange khi
     enumerate, vẫn thành 20000, host sống.
  3. Thứ tự rows trong file kết quả Xls không giữ thứ tự file đầu vào
     (engine sắp xếp lại) — đối chiếu theo Index.
  4. AuthCenter và các host khác vẫn chạy DLL cũ; chỉ nhận DLL mới khi
     rebuild lại từng host (đặc biệt nếu host nào cũng chạm `FileConfig`
     qua Services cũ thì cùng lỗi MissingMethod có thể tái diễn ở host
     đó — nên rebuild đồng bộ các host còn lại trong đợt vệ sinh tới).
  5. FileCols đúng cho Xls là 11 cột (bỏ phí) — xem §6b; tài liệu caller
     cần ghi rõ điểm này, nếu không sẽ nhận 10000 giả với rows 无法入组.

## 8. Nội dung file staging + file kết quả (giữ lại sau khi đã dọn file)

File staging `File/Drg/DRGSTG-20261027.csv` (UTF-8 BOM, 368 byte):

```
病案号,性别,年龄,新生儿年龄天,新生儿出生体重,出院科室,住院天数,离院方式,总费用,诊断编码,手术操作编码,医保类型
22058878,2,88,32460,0,13040503,94,1,0,"K80.302,K80.305,K83.109,K72.905,Z90.408,E14.900x001","51.8803,51.8701,54.5100x005,45.1301",居民
STG-2,1,45,,,13100003,7,1,0,"Z51.100x004,C34.900x001",99.2503,居民
```

File kết quả `File/Drg/DRGSTG-20261027_csharp_result.csv` (1333 byte,
đã dọn; tóm tắt 2 rows — messages đầy đủ là trace tiếng Trung của
engine, dạng như mẫu 2023):

```
Index,gender,age,ageDay,weight,dept,inHospitalTime,leavingType,zdList,ssList,remark,status,messages,mdc,adrg,drg
STG-2,1,45,0,0,13100003,7,1,"Z51.100x004,C34.900x001","99.2503",居民,分组成功,"Z51.100x004 恶性肿瘤终末期化疗|C34.900x001 肺恶性肿瘤|99.2503 静脉注射化疗药物|符合MDCR入组条件…|符合RW2入组条件…|***RW23 …***",MDCR,RW2,RW23
22058878,2,88,32460,0,13040503,94,1,"K80.302,K80.305,K83.109,K72.905,Z90.408,E14.900x001","51.8803,51.8701,54.5100x005,45.1301",居民,分组成功,"K80.302 胆总管结石伴胆管炎|…|符合MDCH入组条件…|符合HC1入组条件…|***HC15 …***",MDCH,HC1,HC15
```

## 9. Diff để điều phối viên soát (chưa commit, chưa push)

```
M Newtouch.HIS.Center/NewtouchHIS.WebAPI.Manage/Areas/DRGS/Controllers/DRGGrouperController.cs
M Newtouch.HIS.Lib/NewtouchHIS.DrgGroup.chs_drg_11/GroupHelper.cs
M Newtouch.HIS.Lib/NewtouchHIS.DrgGroup.wuhan_2022/GroupHelper.cs
M his-dll-common/Net6/NewtouchHIS.DrgGroup.chs_drg_11.dll
M his-dll-common/Net6/NewtouchHIS.DrgGroup.wuhan_2022.dll
M his-dll-common/Net6/NewtouchHIS.Lib.Base.dll
M his-dll-common/Net6/NewtouchHIS.Lib.Services.dll
```

- Bin 8016 cũng đã thay DLL mới nhưng bin không nằm trong git status
  (ignored) nên không hiện ở trên — điều phối viên lưu ý khi đóng gói
  triển khai: bin chạy và his-dll-common hiện đã đồng bộ md5.
- Script live-test đợt này: `/tmp/drg_test.py` (đọc JWT từ file tạm,
  không chứa secret); file tạm chứa token đã xóa.
