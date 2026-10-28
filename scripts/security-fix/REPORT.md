# BÁO CÁO WORKER 7-FIX — SỬA CÁC TỒN ĐỌNG P0 CỦA GIAI ĐOẠN 7

- Worker: 7-fix (subagent, làm liên tục tự duyệt rồi dừng).
- Base: nhánh V2.2 trong `~/workspace/openhis-core`, tip lúc bắt đầu `1236e83c`.
- Thời gian làm: 2026-10-27 (giờ ICT). **KHÔNG commit, KHÔNG push** — điều phối viên soát rồi commit.
- Ngôn ngữ báo cáo: tiếng Việt. Báo cáo này KHÔNG chứa secret: không mật khẩu SA/Redis,
  không JWT/token đầy đủ, không giá trị md5 mật khẩu, không AppSecret, không công thức/
  giá trị/hash của mật khẩu suy ra cho tài khoản `root` (chỉ ghi "đã thử login root").
- Mọi kiểm chứng ghi chạy trên DB `*_STG` và tài khoản CHỈ-CÓ-TRÊN-STG (`7fixlock`,
  clone từ `qtvmoi`). **KHÔNG ghi DB demo trong giai đoạn này** (trừ một sự cố ghi nhầm
  ở mục 4.2 đã dọn sạch ngay và ghi rõ bên dưới); **KHÔNG vô hiệu tài khoản demo**;
  **KHÔNG thử khóa tài khoản demo 000000**.
- Quy tắc chạy app tuân thủ toàn bộ: `ASPNETCORE_URLS` rõ ràng,
  `DOTNET_SYSTEM_NET_DISABLEIPV6=true`, proxy env UNSET khi start dotnet, kill theo PID
  CHÍNH XÁC (không pkill theo tên DLL), build tuần tự (RAM tight), dừng app theo đúng PID
  trước khi rebuild (lỗi PDB MSB3027/MSB3021 không phải lỗi code).
- Hạ tầng staging tái sử dụng: 14 DB `*_STG`, Redis staging 6380, BaseAPI-stg 17001,
  launcher staging `run-*-stg.sh` ở repo root, worktree staging `~/workspace/openhis-stg`.

## TÓM TẮT KẾT QUẢ

| Mục | Kết quả |
|---|---|
| 1. Xóa backdoor `root` | **ĐẠT** (staging + prod) |
| 2. Lockout thật trên live path | **ĐẠT** (staging + prod; kèm 2 sửa nhỏ ngoài ConfigId) |
| 3. Tham số hóa `updateRecordStu` + dọn nối chuỗi | **ĐẠT staging + prod** (kèm 3 fix nối chuỗi phát hiện thêm trong mục 4) |
| 4. Kiểm chứng ghi live từng đường đợt 2 | **Một phần ĐẠT, một phần CHƯA** — xem bảng chi tiết §4; mọi đường CHƯA đều có lý do + dữ liệu cần để kiểm chứng sau, KHÔNG báo đạt khống |

Trạng thái cuối: **prod 26/26 cổng UP** (1433, 6379, 41142, 5208, 7001, 7002, 8015, 8016,
webs 5220/5221/5231–5238, APIs 5320/5331–5337), tài khoản demo prod không đổi
(000000 zt=1 Locked=0; xzadmin zt=1 Locked=NULL; admin zt=1 Locked=0).
**Chưa xác nhận (cần parent/coordinator làm, worker KHÔNG tự làm — xem §6):**
login SSO 000000 thành công trên prod bằng mật khẩu demo ĐÚNG ngay từ lần thử đầu tiên.
Lý do: worker không có mật khẩu demo trong context, và sau khi lockout đã live trên prod,
mọi lần đoán sai đều có nguy cơ khóa tài khoản demo prod (đủ 3 lần sai liên tiếp trong
60 phút) — tuyệt đối không thử đoán.

---

## 1. [P0] XÓA BACKDOOR TÀI KHOẢN `root` — ĐẠT

### 1.1 Code đã sửa trong main checkout (chưa commit)

1. `Newtouch.HIS.Center/NewtouchHIS.Base.DomainService/SysUser/SysUserVDmnService.cs`:
   - Xóa nhánh `if (login.Username == "root" && CheckRootPwd(login.Password))` trong
     `UserLogin` (trả success với Id="root", Account="root" không cần tài khoản DB, chạy
     trước mọi kiểm tra `Locked`).
   - Xóa hoàn toàn method `CheckRootPwd` (suy ra mật khẩu từ thời gian server, băm md5).
   - Xóa nhánh `userId == "root"` trong `BuildLoginStatusOpr(string, ...)` — nay userId
     "root" tra DB, không thấy → null → từ chối. Đơn giản hóa ternary
     `Account = userId == "root" ? userId : user?.Account` thành `user?.Account`.
   - Hardening bổ sung (ghi rõ là mở rộng ngoài (b), không phải login bypass): bỏ miễn
     liên kết nhân viên cho account "root" trong `BuildLoginStatusOpr` overload (chỉ còn
     "admin" giữ đặc quyền cũ); bỏ nhánh gán `operatorModel.IsRoot = true` cho account
     "root" (IsRoot giữ nguyên kiểu, mặc định false); sửa comment cũ nói
     "root admin không thể liên kết".
2. `Newtouch.HIS.Center/NewtouchHIS.Base.DomainService/SysManage/SysConfigDmnService.cs`:
   bỏ miễn kiểm tra quyền admin `userId != "root"` trong `GetMenuListbyAppId`
   (nay chỉ `!isAdmin` thì throw).
3. `Newtouch.HIS.Center/NewtouchHIS.Base.DomainService/SysManage/SysModuleDmnService.cs`:
   bỏ `if (user == "root") return true;` trong `MenuEditAuthcation`.
4. Grep toàn cây: không còn code `CheckRootPwd` nào (chỉ còn nhắc trong comment giải
   thích việc xóa). Không còn nhánh login nào khác không qua DB ở LoginController các app,
   SysUserApp, center UserLogin.
5. **GIỮ NGUYÊN có lý do**: 2 nhánh `userCode == "root"` trong `HomeController.GetVisitNum`
   của HOSP Web và Base Web — chỉ là logic hiển thị (root → dữ liệu null, BO = null),
   đã ở sau session đã xác thực (`OperatorProvider.GetCurrent()`), không phải backdoor.
   Đã xác nhận không có tài khoản DB nào tên root:
   `SELECT COUNT(*) FROM Sys_User WHERE Account='root' OR Id='root'` = 0 trên cả demo
   và `*_STG` (2026-10-27).

### 1.2 Graph triển khai (xác định bằng grep)

- Dùng `UserLogin` trực tiếp: HIS.BaseAPI (ProjectReference tới DomainService),
  AuthCenter, WebAPI.Manage (cùng ProjectReference).
- Dùng `BuildLoginStatusOpr` cục bộ: mọi web app (qua quét bin DLL) + Sett web (đăng ký
  tường minh `AddScoped<ISysUserVDmnService, SysUserVDmnService>` trong
  `Newtouch.Sett.Web/Program.cs` dòng 30).
- API không gọi `UserLogin`/`BuildLoginStatusOpr` (grep 0 kết quả trong các project API);
  OR/MRMS/CIS/MRQC API trong bin thậm chí không có `NewtouchHIS.Base.DomainService.dll`.
- Web/API csproj tham chiếu DomainService DLL qua HintPath trỏ vào run copy
  (`lmsoft_openhis_run/.../HIS.BaseAPI/bin/Release/net6.0/...`) — run copy KHÔNG được sửa;
  vì vậy triển khai web/API bằng copy DLL tươi vào chính bin của app đó (theo mẫu roll Lib
  giai đoạn 7), không rebuild web từ main ở mục này.
- `his-dll-common/Net6/NewtouchHIS.Base.DomainService.dll` KHÔNG được refresh (không csproj
  nào tham chiếu nó qua HintPath cho assembly này; center hosts dùng ProjectReference,
  không phải DLL chung — khác trường hợp Lib.Base ở giai đoạn 7).

### 1.3 Triển khai prod mục 1+2 (DLL tươi build từ main, md5 file giống nhau ở
### DomainService bin và BaseAPI bin)

- 11 web + SSO portal: kill đúng PID → backup DLL cũ thành `.bak-7fix` trong bin → copy
  DLL tươi → launcher → cổng UP → trang `/Login/Index` 200 (đủ 11 web + SSO). PIDs cũ lúc
  roll: SSO 5208 PID 5067 → mới 18767; MRQC 5220 PID 5174 → 18834; Sett 5221 PID 5244 →
  18887; MRMS 5232 PID 5428 → 18941 (sau đó rebuild mục 3 thành 21095); HOSP 5233 PID 5505
  → 19004; CIS 5234 PID 10435 → 19064; EMR 5235 PID 9693 → 19126 (sau đó rebuild mục 3
  thành 21280); Herp 5236 PID 5802 → 19182; PDS 5237 PID 10174 → 19235; Base.Web 5238
  PID 5965 → 19297; OR 5231 PID 9918 → 19354.
- Cấu hình launcher giữ nguyên (MRMS web Debug, OR web Debug, CIS web Debug; còn lại
  Release — không chuẩn hóa ở worker này).
- BaseAPI 7001: kill PID 4930 → build main Release 0 lỗi → `run-his-baseapi.sh` → 7001 UP.
- AuthCenter 8015: kill PID 10732 → build main Release 0 lỗi → `run-authcenter.sh` →
  8015 UP; GetAppToken prod `{AppId MRQC, Data API.Manage}` → 10000, token đã cấp
  (không ghi nội dung token).
- WebAPI.Manage 8016: kill PID 10879 → build main Release 0 lỗi → `run-apimanage.sh` →
  8016 UP; no-token POST `api/Outpatient/OutpatientRegist` trả HTTP 200 với envelope
  `HttpStatus: 401` ("Xác thực token thất bại") — đây chính là "no-token 401" của giai đoạn 7
  (lần thử GET trước đó trả 405 vì route chỉ nhận POST, không phải hồi quy).
- HOSP prod no-cookie `Sys/Duty/GetGridJson` → 302 (fix auth giai đoạn 7 còn nguyên sau
  triển khai).
- API bins: copy DLL tươi (+ backup `.bak-7fix`) vào EMR API, HOSP API, Herp API, PDS API
  mà KHÔNG restart (các API này không gọi service); OR/MRMS/CIS/MRQC API không có DLL
  trong bin nên không có gì để copy. Sett-API host (7002) không có DLL này trong bin, bỏ qua.
- Demo prod sau triển khai (chỉ đọc): 000000 zt=1 Locked=0; xzadmin zt=1 Locked=NULL;
  admin zt=1 Locked=0 — không bị khóa/không đổi.

### 1.4 Bằng chứng live staging

Cụm staging: Redis-stg 6380, BaseAPI-stg 17001 (binary worktree, code 7-fix),
HOSP-stg 15233, AuthCenter-stg 18015, CIS API-stg 15334.

- Login `root` qua HOSP-stg 15233 với mật khẩu suy ra từ giờ server bị từ chối ở cả
  2 múi giờ thử (UTC và ICT) với thông điệp "Không tìm thấy thông tin người dùng".
- Tài khoản STG-only `7fixlock` login thành công qua cùng đường.
- Nếu lúc kiểm chứng phát hiện luồng hợp pháp nào phụ thuộc login `root` thì khôi phục từ
  backup `.bak-7fix` + báo lại — không phát hiện luồng nào như vậy.

### 1.5 Quy trình break-glass thay thế (KHÔNG tạo backdoor mới)

- Admin bệnh viện tạo được qua web (đường tạo admin giai đoạn 7 mục 3, đã sửa).
- Mở khóa thủ công bằng SQL (xem §2.5).
- Khôi phục khẩn cấp code: DLL backup `.bak-7fix` trong từng bin (xem §1.3) + git revert
  các file §1.1 (chưa commit nên chỉ cần `git checkout` các file đó rồi build lại).

---

## 2. [P0] LOCKOUT THẬT TRÊN LIVE PATH — ĐẠT

### 2.1 Thiết kế đã implement trong `SysUserVDmnService.UserLogin` (cùng file mục 1)

- Ngưỡng dùng đúng config `LoginFailedTimesLimit_Count` /
  `LoginFailedTimesLimit_Minutes` qua helper `FailureLimit` / `FailureWindowMinutes`,
  fallback giống legacy là **3 lần / 60 phút** khi config null hoặc ≤ 0. BaseAPI có key
  3/60 trong appsettings; AuthCenter/Manage KHÔNG có 2 key này nên nhận fallback — cùng
  giá trị 3/60, ghi rõ để khỏi nhầm là khác ngưỡng.
- Nhánh sai mật khẩu nay: (1) ghi 1 dòng thất bại vào `Sys_Log` (bảng của entity
  `SysUserLoginLog`) bằng SQL liệt kê cột tường minh — **bắt buộc** vì `Sys_Log` thực tế
  KHÔNG có cột `OrganizeId` mà entity có (14 cột thực tế: Id, TopOrganizeId, Date, Account,
  NickName, Type, IPAddress, IPAddressName, ModuleId, ModuleName, Result, Description,
  CreateTime, px; đã kiểm chứng schema trên `NewtouchHIS_Base_STG`; không có trigger nào
  trên `Sys_Log` STG); (2) đếm thất bại LIÊN TIẾP mới nhất trong cửa sổ (gặp dòng thành
  công thì dừng đếm; dòng Result NULL tính như thất bại, giống ngữ nghĩa legacy
  `!Any(p => p.Value)`); đủ ngưỡng thì
  `UPDATE Sys_UserLogOn SET Locked=1 WHERE UserId=@userId AND zt='1'` (SQL tham số hóa,
  chạy trên connection BaseDb) và trả thông điệp khóa dùng `FailureLimit` (lần sai thứ N
  đã trả thông điệp khóa, giống legacy); (3) lỗi bookkeeping (ghi log/đếm) được catch và
  trả thông điệp sai mật khẩu như cũ — không làm hỏng đường login, ghi rõ trade-off này.
- Nhánh login thành công nay ghi thêm 1 dòng success (`Result=1`, Description
  "Dang nhap thanh cong") vào `Sys_Log` trong try/catch nuốt lỗi — **phát hiện thiết kế
  quan trọng**: trước đây đường success không ghi dòng Sys_Log nào (logEntity trong
  `BuildLoginStatusOpr` không bao giờ được lưu), nên nếu không có dòng success thì sau mở
  khóa, 1 lần sai mới sẽ cộng dồn với các dòng thất bại cũ và khóa lại ngay; dòng success
  reset chuỗi liên tiếp đúng nghĩa.
- KHÔNG tự mở khóa (đúng thông điệp code "Không cung cấp Tự động mở khóa").
- Tài khoản không tồn tại → trả "Không tìm thấy thông tin người dùng", không ghi log thất
  bại (tránh spam log/enumeration).

### 2.2 Kiểm chứng staging (tài khoản STG-only `7fixlock`, TUYỆT ĐỐI không test khóa
### trên demo)

- Web qua HOSP-stg 15233 → BaseAPI-stg 17001: sai lần 1, 2 → "Mật khẩu không đúng, vui
  lòng nhập lại"; sai lần 3 → "Nhập sai mật khẩu liên tiếp quá 3 lần, tài khoản đã bị khóa";
  login đúng khi đang khóa → vẫn thông điệp khóa; SQL: `Locked=1`, `Sys_Log` cho `7fixlock`
  = 4 dòng (1 success trước đó + 3 failure, mới nhất trước: 0,0,0,1). Mở khóa SQL →
  `Locked=0` → login web thành công trở lại.
- Center `UserLogin` qua AuthCenter-stg 18015: sau khi sửa xong (§2.4), sai → 40004 sai
  mật khẩu; đúng → 10000 + JWT đã cấp.
- Kế thừa lockout chéo: sau mở khóa + login thành công, 3 lần sai qua center → khóa
  (`Locked=1`) → web login cũng bị từ chối với thông điệp khóa; mở khóa → cả hai lại chạy.
  Tương tự qua GetToken CIS API-stg 15334: GetToken đúng baseline → 10000; sai 3 lần →
  `Locked=1` → web login bị khóa; mở khóa → GetToken đúng 10000 + web login success.
  (GetToken envelope khi sai chỉ lấy được code 40004, msg rỗng trong trích xuất — code mới
  là bằng chứng chính; GetToken gọi BaseAPI CheckPwd qua HTTP, xem
  `Newtouch.HIS.CIS/Newtouch.CIS.API/Controllers/AuthController.cs`.)
- Baseline trước test khóa: `Sys_Log` cho `7fixlock` = 1 dòng (dòng success từ lần login
  đầu), `Locked=NULL`.
- Tài khoản kiểm chứng STG: `qtvmoi` (STG-only từ giai đoạn 7, đã có staff link
  `G7QTV-LINK` → staff `G7QTV-STAFF`, TopOrganizeId đã chuyển về top org demo trên STG,
  zt=1, Locked=NULL lúc đầu); `7fixlock` (mới tạo trong worker này bằng cách clone hàng
  `Sys_User` + `Sys_UserLogOn` (giữ nguyên secret/hash → cùng mật khẩu với `qtvmoi`) +
  `Sys_UserStaff` của `qtvmoi` sang Id mới, Account `7fixlock`, zt=1, Locked=NULL ban đầu,
  1 staff link). Sau các test cuối, trạng thái của nó: đã mở khóa (`Locked=0`) và login
  thành công trở lại (cả web lẫn GetToken). `000000` trên STG KHÔNG dùng để test khóa;
  lần thử login STG bằng mật khẩu cũ đã fail "Mật khẩu không đúng" (mật khẩu demo khác
  mật khẩu mặc định cũ) — đó chỉ là bằng chứng đường đi hoạt động, không phải bằng chứng
  login thành công.

### 2.3 Các tiểu mục 2(c)–(e)

- (c) Legacy `SysUserApp.CheckLogin`
  (`Newtouch.HIS.Base/Newtouch.HIS.Base.Infrastructure/Application/Implementation/SystemManage/SysUserApp.cs`):
  **DEAD CODE đối với CheckLogin** — bằng chứng grep: không caller nào gọi `.CheckLogin(`
  (chỉ interface + implementation); các controller inject `ISysUserApp` chỉ dùng
  `RevisePassword` (HOSP `Sys/UserController.cs:112`) và `CreateDefaultAdminToOrg`
  (2 `OrganizeController`); PDS web/API chỉ đăng ký DI `ISysUserApp` mà không tiêu thụ.
  Theo đúng brief (dead code thì ghi rõ, không sửa), **KHÔNG sửa bug `Locked=false`**
  trong dead code này — lưu ý: nếu sau này khôi phục thì phải sửa `false`→`true` và bỏ
  miễn `admin`. (Phân biệt: PDS có class `SysUserApp` khác, 3 tham số
  `CheckLogin(topOrganizeId, username, password)`, cũng không có caller và không có logic
  khóa — chỉ kiểm tra `zt`.)
- (d) PDS `SysUserExDmnService.CheckLogin`
  (`Newtouch.HIS.PDS/Newtouch.PDS.Infrastructure/SystemManage/SysUserExDmnService.cs`):
  code khóa đúng (`UPDATE... Sys_UserLogOn SET Locked=1`, `userEntity.Locked = true`,
  ngưỡng 3/60) nhưng **CHƯA BAO GIỜ khóa thật trên live path** vì 2 lý do: (1) không caller
  nào gọi `CheckLogin` của nó (PDS `LoginController` chỉ dùng service này cho
  `GetYfbmCodeListByUserId` ở `OprBeforeSave`; login web đi qua `LoginBaseController` →
  BaseAPI CheckPwd → center `UserLogin`); (2) logic đếm phụ thuộc dòng `sys_log` Type='Login'
  mà trước 7-fix không có đường nào ghi dòng thất bại trên live path (đường success duy nhất
  tạo logEntity cũng không lưu). Nay center `UserLogin` đã tự ghi dòng thất bại/thành công
  vào `Sys_Log`, nên nếu đường PDS này được nối lại sau này thì nguồn đếm mới tồn tại —
  ghi rõ, không tự nối lại trong worker này.
- (e) Lỗi center `ConfigId was not found 1`: **NGUYÊN NHÂN GỐC ĐÃ TÌM RA VÀ SỬA**.
  `SysUserVDmnService` constructor gọi `SqlSugarDbContext.Db.GetConnection(_mainDB)` với
  `_mainDB = DbConfig.MainDB`; `ConnectionConfigInit` đăng ký `ConfigId = ConnId`
  (BaseDb/SettDb/...); cả 2 appsettings center đặt `"MainDB": "1"` (giá trị mặc định rơi
  rớt từ fallback `dbMain.ConnId?? "1"` trong code cũ) — không ConnId nào là "1" nên SqlSugar
  throw đúng thông điệp đó ngay khi khởi tạo service (5 service center dùng
  `GetConnection(_mainDB)`: SysConfig, SysModule, SysRole, SysUserVDmn, UserRoleAuth;
  MsgQueue không dùng nên vẫn chạy — giải thích vì sao các luồng center khác vẫn sống).
  Sửa nhỏ: `"MainDB": "1"` → `"MainDB": "BaseDb"` trong
  `NewtouchHIS.AuthenticationCenter/appsettings.json:65` và
  `NewtouchHIS.WebAPI.Manage/appsettings.json:38` (surgical bằng sed, diff mỗi file 1 dòng).
  `DefaultDb` không còn nơi nào khác dùng.
- Phát hiện thêm trên cùng endpoint center (sửa nhỏ, đúng phạm vi "sửa nếu nhỏ"): sau khi
  hết lỗi ConfigId, login đúng vẫn fail ở bước cấp JWT với "[Xác thực]: Thông tin tài khoản
  không được để trống" từ `HSJWTService.OptionValid` (AuthType.Web bắt buộc cả `Account` và
  `UserCode`), vì `LoginController.UserLoginAsync` của AuthCenter chỉ set `Account` mà không
  set `UserCode` trong `AuthIdentity`. Đã sửa: thêm `UserCode = logResult.Data.Account` vào
  initializer trong
  `Newtouch.HIS.Center/NewtouchHIS.AuthenticationCenter/Controllers/LoginController.cs`.
  Sau sửa: center UserLogin đúng → 10000 + JWT; sai → 40004 sai mật khẩu. Lưu ý test:
  `OrganizeId` trong RequestBus phải gửi kèm (đã thử gửi rỗng vẫn fail ở bước JWT — nhưng
  nguyên nhân thật là thiếu UserCode, không phải OrganizeId; sau sửa đã test với OrganizeId
  = top org demo).

### 2.4 Quy trình MỞ KHÓA THỦ CÔNG (đã kiểm chứng trên STG)

```sql
-- 1. Xác định Id trước khi update (ghi rõ Id ra giấy/log vận hành)
SELECT Id, Account, Locked FROM Sys_User u
JOIN Sys_UserLogOn l ON u.Id = l.UserId WHERE u.Account = '<tài khoản>';
-- 2. (Khuyến nghị) backup hàng Sys_UserLogOn trước khi sửa.
-- 3. Mở khóa:
UPDATE Sys_UserLogOn SET Locked = 0
WHERE UserId = (SELECT Id FROM Sys_User WHERE Account = '<tài khoản>');
-- 4. Kiểm chứng: login lại thành công; Sys_Log có dòng success mới.
```

Lưu ý: mở khóa không xóa các dòng thất bại cũ trong `Sys_Log` — nhưng dòng success mới
ghi khi login thành công sẽ reset chuỗi đếm liên tiếp, nên tài khoản không bị khóa lại ngay
ở lần sai tiếp theo (đúng thiết kế §2.1).

---

## 3. THAM SỐ HÓA `updateRecordStu` + DỌN NỐI CHUỖI SQL — ĐẠT

### 3.1 Code đã sửa trong main (chưa commit)

1. `Newtouch.HIS.MRMS/Newtouch.MRMS.Infrastructure/DomainServices/RecordDmnService.cs` —
   `updateRecordStu`:
   - Tách `id` theo dấu phẩy, trim, distinct; mỗi Id một tham số `@id0, @id1,...`; nối
     bằng `string.Join(",", names)` (tên tham số nội bộ, không phải input user).
   - Id rỗng / tách ra rỗng → thêm `and 1=0` (0 dòng). Đây đồng thời đóng lỗ hổng
     mass-update cũ: trước đây id rỗng thì không có điều kiện id, và nếu organizeId cũng
     rỗng thì update toàn bảng.
   - `LockRecord` trong cùng file: tham số hóa `LastModifierCode` (`@UserCode`); tên bảng
     động `bllxmc` vẫn nối chuỗi (không tham số hóa được, lấy từ bảng ánh xạ bllx trong DB
     qua `GetBllxTB`, không phải input trực tiếp của user).
2. `Newtouch.HIS.MRMS/Newtouch.MRMS.Infrastructure/DomainServices/MainRecordDmnService.cs`:
   tham số hóa `BABAH` (`@bah`) trong UPDATE `mr_basy` của EMR; 2 proc
   `usp_sync_basy_Exportscv` / `usp_sync_basy_Exportscvfy` chuyển sang
   `@orgId,@kssj,@jssj` (dùng `SqlQueryForDataTatable(sql, params...)` đã hỗ trợ params);
   LIKE con trong `PatMainList` chuyển sang `@pykeyword` với giá trị `"%" + keyword + "%"`.
3. `Newtouch.HIS.EMR/Newtouch.EMR.Infrastructure/Repository/HljlDataRepo.cs`: tham số hóa
   `blIdselect` (`@mbbh,@zyh,@orgId`), `BtnSubmit` (delete: `@rygh,@itemId` — trước đây nối
   `rygh` KHÔNG dấu nháy + Id trực tiếp; insert: `@userCode,@userOrgId,@zyh` — trước đây
   nối trực tiếp), `Infodiv` (`@zyh,@orgId`), `mbbhselect` (`@zyh,@mbbh,@orgId,@kssj,@jssj`).
   Hành vi giữ nguyên: `mbbhselect` cũ so sánh `a.mbId= '<mbbh> '` (thừa dấu cách trong
   literal) — nay trim mbbh; so sánh varchar của SQL Server vốn bỏ qua dấu cách cuối nên
   tương đương; `jssj` nay cộng `" 23:59:59"` vào giá trị tham số (giống nối chuỗi cũ
   trong SQL).
4. `Newtouch.HIS.OR/Newtouch.OR.Infrastructure/ORApplyInfoRepo.cs` — `DeleteForm`: tham số
   hóa cả câu SELECT kiểm tra và câu UPDATE `zy_lsyz`
   (`@zyh,@ssmcn,@ysgh,@orgId,@sssj`); giữ nguyên ngữ nghĩa cũ dùng `ExecuteSqlCommand`
   trả về khác 0 thì update. Sửa lỗi build phát sinh: `sssj` là `DateTime?` nên không dùng
   `?? ""` được — đổi thành `(object)dbEntity.sssj ?? System.DBNull.Value`.
5. `Newtouch.HIS.CIS/Newtouch.CIS.Infrastructure/DomainServices/Outpatient/ReportDmnService.cs`
   — `ReportAdd`: tham số hóa `RptCode` (`@rptCode`, dùng `SqlParameter` như code sẵn có).
6. `Newtouch.HIS.PDS/Newtouch.PDS.Infrastructure/HospitalizationPharmacy/DispenseIndexInfoDmnService.cs`
   — `SelectDispenseDrugDetail`: tham số hóa `yzxz` (`@yzxz`; giá trị vốn đã whitelist
   "1"/"2" ở nhánh trên). Bài học: `param` ở đây là mảng `SugarParameter[]`, không phải
   List — lần đầu viết `param.Add(...)` là sai, đã sửa thành thêm phần tử vào mảng khởi tạo
   (thừa khi nhánh không chạy cũng vô hại).
7. **3 fix nối chuỗi phát hiện thêm TRONG LÚC làm mục 4** (cùng lớp vấn đề, cùng file
   giai đoạn 7 — sửa ngay thay vì để tồn đọng):
   - (a) `Newtouch.HIS.OR/Newtouch.OR.Infrastructure/SysChargeItemDmnService.cs`:
     INSERT `xt_sfmbxm` theo từng item trước đây nối `OrganizeId/sfmbbh/sfxmCode/user/sl`
     trực tiếp vào chuỗi SQL (trong đó `sfxmCode/sl` đến từ JSON do client gửi) — nay mỗi
     item một INSERT tham số hóa (`@orgId,@sfmbbh,@sfxm,@user,@sl`).
   - (b) `Newtouch.HIS.CIS/Newtouch.CIS.Infrastructure/DomainServices/Inpatient/OrderAuditDmnService.cs`
     — `Inputskintestresults`: INSERT-SELECT `xt_gmxx` trước đây nối toàn bộ field
     (`hzxm/xmdm/xmmc/psbz/Remark/user/yzid/zyh/OrganizeId`) — nay tham số hóa toàn bộ +
     thêm danh sách cột tường minh + giá trị `cancel='1'` (như `Enteragain`): bản cũ không
     liệt kê cột mà chỉ select 21 giá trị cho bảng 22 cột nên INSERT không bao giờ chạy
     được (xem thêm §4.4 — đường này vẫn CHƯA live được vì `_allergyRepo` null, xem §4.4).
   - (c) `Newtouch.HIS.CIS/Newtouch.CIS.Infrastructure/DomainServices/Outpatient/MedicalRecordDmnService.cs`
     — `delDzcf`: 2 câu UPDATE `xt_dzcf/xt_dzcfmx` trước đây dùng `string.Format` nối `cfId`
     trực tiếp — nay `@cfId` (dùng 2 lần trong cùng command, SqlClient cho phép).
8. Rà soát: grep mẫu nối chuỗi nguy hiểm (`+ var +`, `IN (" +`, `string.Format` SQL...)
   trên 39 file `.cs` giai đoạn 7. Kết luận: `MrbasyRepo` thực tế đã tham số hóa xong phần
   còn lại — các nối còn lại chỉ là `DbNames.Name(...)` (tên DB từ cấu hình, an toàn) và
   giá trị tham số `%keyword%` trong `SugarParameter` (không phải nối SQL). Các điểm KHÔNG
   sửa trong worker này, ghi rõ làm tồn đọng kèm file:dòng và lý do:
   - Khối INSERT bulk dựng chuỗi trong `EMR MedicalRecordDmnService` (BLJG_Save, `sqlval`
     nối từng field — cần viết lại dạng tham số đa dòng, rủi ro cao).
   - SQL động `DispenseIndexInfoDmnService` StrSQL (giá trị là Id lấy từ DB, không phải
     input trực tiếp).
   - Danh sách IN nối chuỗi nội bộ trong CIS `OrderAuditDmnService` (456–491: `listcqzt`,
     `liscqyfzt`, `jyjcyzhs`, `lislsyfzt` — nối từ kết quả truy vấn DB trước đó, không phải
     input user).
   - Nối `TOP` bằng int nội bộ trong PDS `DrugStorageDmnService` (912/952/993, `topCount` int).
   - Nối enum int trong Herp `WzProductDmnService:289`.
   - Nối message log (không phải SQL) trong nhiều file.
   - Các câu SELECT đọc còn hardcode tên DB demo (read leak, KHÔNG phải write leak — đã có
     tài liệu giai đoạn 7, không chặn): ví dụ `ReportAdd` câu check trùng dùng
     `[NewtouchHIS_Base]`, `SynchronizationZD` câu count dùng `[Newtouch_CIS]`,
     `delDzcf` câu SELECT dùng `Newtouch_CIS..xt_dzcf`, `SelectApplyNoStatus` dùng
     `Newtouch_CIS.[dbo].zy_bqksby`, `IsCouldReturn` dùng `NewtouchHIS_PDS` +
     `NewtouchHIS_Base`, `DiagnosticSave` dùng `[Newtouch_EMR].[dbo].[Sys_Config]`,
     `ISfalgPatient` dùng `NewtouchHIS_Sett..mz_gh`. Trên prod (tên DB thật trùng hardcode)
     các câu này đọc đúng DB; trên staging chúng đọc demo — đây là lý do một số đường mục 4
     không kiểm chứng live được (ghi rõ từng đường ở §4).

### 3.2 Kiểm chứng staging mục 3 (MRMS-stg 15232, build từ worktree)

- Fixture: `Newtouch_EMR_STG.zy_brjbxx` zyh 01000 (Id `72D5923E-6503-4476-84A6-FBC8F91F195D`,
  OrganizeId = top org demo, RecordStu=2 trước test), zyh 01999 (Id NULL, OrganizeId
  `00000018`, RecordStu=1); demo `Newtouch_EMR.zy_brjbxx` zyh 01000 RecordStu=1.
- Route đúng là `/RecordManage/Record/updateRecordStu` (không phải `/Record/...` → 404).
  Bài học harness: file jar cookie của curl không gửi được cookie (dòng session expires
  rỗng) dù login success — phải dùng python opener giữ cookie trong bộ nhớ
  (`~/workspace/7fix-scratch/stg_call.py`); đừng kết luận 302 là lỗi session trước khi kiểm
  tra cơ chế gửi cookie. Bài học thứ hai: **path không có dấu `/` đầu làm URL sai**
  (harness nối `port + path` không có `/`) — các call EMR đầu tiên timeout/disconnect chỉ vì
  vậy, không phải lỗi app.
- Trường hợp A Id chuỗi `G6-PROBE`: HTTP 200 "Thao tác thành công", STG 01000 giữ 2,
  01999 giữ 1 — KHÔNG còn crash "Invalid column name".
- Trường hợp B chuỗi injection `' OR '1'='1`: HTTP 200, STG giữ nguyên 2/1 — không đổi
  dòng ngoài ý muốn.
- Trường hợp C Id thật `72D5923E-...`, blzt=3: STG 01000 2→3, 01999 giữ 1, demo 01000 giữ 1.
  Sau test đã khôi phục STG 01000 về 2.
- Kết luận mục 3 staging: ĐẠT.

### 3.3 Triển khai prod mục 3

- ĐÃ XONG: MRMS web (Debug, PID cũ 18941 → 21095) + MRMS API (Release, PID 7921 →
  21190) + EMR web (Release, PID 19126 → 21280) + EMR API (Release, PID 9806 → 21358),
  mỗi host kill đúng PID → build main đúng cấu hình launcher → launcher → cổng UP. Xác minh
  code mới trong bin bằng `strings -e l` trên DLL UTF-16: MRMS web bin có `@pykeyword`,
  `@UserCode`; EMR web/API bin có `@mbbh`, `@itemId`, `@userCode`, `@userOrgId`.
- ĐÃ XONG (sau mục 4): OR web (Debug, PID cũ 21786 → 27779) với fix (a) — xác minh chuỗi
  `values(newid(),@orgId,@sfmbbh,@sfxm,@user,getdate(),'1',@sl)` trong DLL + trang login 200.
  OR API không dùng `SysChargeItem` nên không redeploy.
- ĐÃ XONG (sau mục 4): CIS web (Debug, PID cũ 21970 → 31056, rồi sau sự cố crash toàn cụm
  restart lại bằng launcher → PID mới, login 200) với fix (b) + (c) — xác minh chuỗi
  `@hzxm` và `cfid=@cfId` trong DLL. CIS API không chứa 2 service này nên không redeploy.
- **CẢNH BÁO script (đã sửa)**: `~/workspace/7fix-scratch/rebuild-restart.sh` từng có lỗi
  pipeline — `if ! dotnet build... | tail` chỉ kiểm tra exit code của `tail` (luôn 0),
  KHÔNG bắt được build fail; đã sửa (build ra log file, kiểm tra `$?` trực tiếp) TRƯỚC khi
  dùng tiếp cho OR/CIS/PDS, và mọi lần rebuild sau đều phải xác minh DLL bằng
  strings/timestamp, không tin cổng UP (build fail vẫn để lại DLL cũ trong bin và app vẫn
  lên cổng với code cũ).
- PDS web (Release, PID 19235 → 22174) + PDS API (Release, PID 10295 → 22261) đã redeploy
  ở đợt mục 3 đầu tiên, login 200. PDS không có fix addendum nên không cần redeploy lại.
  Tóm lại: **mục 3 prod đã đủ các host chứa code sửa** (MRMS web+API, EMR web+API, OR web,
  CIS web, PDS web+API; OR API và CIS API không chứa code sửa nên không redeploy là đúng).

---

## 4. KIỂM CHỨNG GHI LIVE TỪNG ĐƯỜNG ĐỢT 2 — MỘT PHẦN ĐẠT, MỘT PHẦN CHƯA

Phạm vi theo REPORT giai đoạn 7 §2c + §11 (đối chiếu từng file, không bỏ sót theo cảm
tính). Quy tắc áp dụng: với mỗi nhóm, dựng fixture STG tối thiểu + gọi đúng action/API +
kiểm chứng bằng SELECT rằng ghi đã vào STG (đúng bảng, đúng giá trị) và demo KHÔNG đổi
(đếm demo trước/sau). Đường nào vẫn ghi demo thì đó là P0 — SỬA NGAY (trường hợp OR
ChargeTemplate dưới đây là ví dụ, đã sửa bằng cách build đúng cấu hình, không phải sửa
DbNames). Đường nào không dựng được fixture thì ghi rõ: đường nào, vì sao, cần dữ liệu/
nền tảng gì — KHÔNG báo đạt những đường này. Chạy từng cụm staging một lúc (RAM tight),
dừng ngay sau mỗi app theo PID chính xác.

### 4.0 Chuẩn bị chung cho mục 4 (quan trọng, đừng làm lại sai)

- Worktree đang ở commit trước giai đoạn 7 nên ban đầu thiếu code DbNames — đã copy toàn bộ
  file giai đoạn 7 còn thiếu từ main sang worktree (các file CIS/EMR/OR/PDS/Center DbNames
  + DomainService/Infra, trừ các file đã có sẵn trong worktree như MRMS, ControllerBase,
  JwtService, LogTracking, ReportDmnService, DispenseIndexInfoDmnService, HljlDataRepo,
  ORApplyInfoRepo đã copy ở mục 3). **KHÔNG copy `Program.cs`** (worktree Program.cs đã có
  literal cổng staging + dòng ConfigurationHelper; ví dụ HOSP Web Program.cs diff chỉ có
  2 dòng: cổng 5233→15233 và dòng ConfigurationHelper).
- Đã copy 4 appsettings từ main sang worktree (worktree CIS/PDS appsettings trước đó không
  đổi so với 0a730b1b; AuthCenter/Manage worktree trước đó chỉ khác 1 dòng MainDB đã có
  trong main): CIS Web (nay có đủ 8 ConnId gồm OrDb), PDS Web (nay có CisDb), AuthCenter +
  Manage (nay có MainDB=BaseDb). Đã xác minh: CIS ConnId gồm
  BaseDb/SettDb/EmrDb/MrmsDb/MrQcDb/CisDb/UnionDb/OrDb; PDS ConnId gồm
  BaseDb/UnionDb/PdsDb/CisDb.
- **Bẫy cấu hình launcher staging:** TẤT CẢ launcher `run-*-stg.sh` đều chạy binary
  `bin/Release/net6.0/*.dll`. Lần đầu build OR-stg bằng Debug đã gây ra binary cũ (xem
  sự cố OR §4.2). Từ đó về sau mọi build staging đều dùng Release (trừ khi launcher ghi
  khác — luôn kiểm tra `grep bin/... run-*-stg.sh` trước khi build).
- **Bẫy harness:** `stg_call.py` yêu cầu path có dấu `/` ở đầu (ví dụ `/MRHomePage/...`).
  Lần đầu gọi EMR không có `/` đã tạo URL sai và gây timeout/RemoteDisconnected giả —
  không phải lỗi app. Luôn dùng path có `/`.
- **Sự cố hạ tầng trong phiên này (đã xử lý):** SQL Server (1433), Redis-stg (6380),
  BaseAPI-stg (17001), AuthCenter-stg (18015) lần lượt chết trong lúc làm CIS (nghi
  OOM/restart runtime; thư mục `~/workspace/redis-stg/data` có mtime 17:06 ngày 2026-10-27).
  Đã chạy `~/workspace/recovery.sh` MỘT lần (đúng quy tắc), khôi phục prod gần đủ, còn
  thiếu 8015 + 5234 nên khởi động lại thủ công bằng launcher (`run-authcenter.sh`,
  `Newtouch.HIS.CIS/run-5234.sh`) → prod đủ 26/26, CIS prod login 200. Redis-stg khởi động
  lại thủ công từ `~/workspace/redis-stg/redis-stg.conf` (daemonize yes, port 6380, dir
  `~/workspace/redis-stg/data`) với `LD_LIBRARY_PATH` chứa `~/workspace/redis-libs/...`
  (quirk liblzf); BaseAPI-stg/AuthCenter-stg khởi động lại bằng launcher worktree trong
  `~/workspace/7fix-scratch/`. Sau recovery, **mọi PID prod/staging cũ đều không còn tin
  cậy — khi tiếp tục, lấy PID bằng `ss -tlnp` trước khi kill**. CIS web prod sau recovery
  chạy bằng bin đã có fix OrderAudit (do restart lại từ cùng bin), nhưng **chưa có fix
  delDzcf** — đã triển khai lại trong §3.3 (lần redeploy cuối, strings xác nhận `@cfId`).
- Lưu ý: worktree từng được patch tạm để in exception thật của OrderAudit (thay message
  catch bằng DEBUG + ex.Message) rồi **đã revert** bằng cách copy lại file main (chưa fix
  concat) rồi sau đó copy lại file main đã fix concat — worktree hiện tại KHÔNG còn patch
  debug (xác minh bằng grep chuỗi DEBUG trước khi build staging tiếp).

### 4.1 EMR (EMR-stg 15235, build worktree Release) — 5 ĐẠT, 4 CHƯA

| Đường | Kết quả | Bằng chứng / lý do |
|---|---|---|
| `MRHomePageDmnService.GetMainRecord` → UPDATE `mr_basy` CYSJ/SJZYTS từ CIS `zy_brxxk` (DbNames CisDb+EmrDb) qua `MRHomePage/MRHomePage/GetFormJson keyValue=G7-EMR-BASY` | **ĐẠT** | Lần đầu NRE ở dòng dùng `mrbasy.ZYH` vì fixture `G7-EMR-BASY` có OrganizeId=NULL (lỗi fixture giai đoạn 7, không phải lỗi code) → đã sửa fixture STG thành OrganizeId = top org demo. Sau sửa: CYSJ NULL→2024-10-24 17:27:04 (khớp CIS STG `zy_brxxk.cqrq`), SJZYTS NULL→8 (get_zyts 10-16→10-24). Demo `mr_basy` zyh 01000 = 0 dòng, không đổi. |
| `GetMainRecordbybrjbxx` → UPDATE `mr_basy` ZYCS qua Sett `get_zycs` (qua `GetFormJson zyh=01000`) | **ĐẠT** | ZYCS = 1, khớp trực tiếp `SELECT dbo.get_zycs('01000','',<top org>)` trên Sett STG = 1. Demo không đổi. |
| `HljlDataRepo.BtnSubmit` qua `TemplateDesignController.BtnSubmit` (nhánh không mbbh) → INSERT `bl_hljldata` | **ĐẠT** | STG `bl_hljldata` 0→1 dòng (zyh 01000, jlrq 2026-10-27, tw 36.8, blId=''); demo `bl_hljldata` = 0, không đổi. Sau test đã xóa dòng test. |
| `MedicalRecordDmnService.updateLock` qua `MedicalRecordController.UpdateLockzt` → UPDATE `bl_hljl` IsLock | **ĐẠT** | Fixture STG `bl_hljl` Id `7FIX-HLJL` (phải sửa: `pzhm_bl` là numeric, `IsLock` là int; lần insert đầu fail do sai kiểu) IsLock 1→0. Sau test đã xóa fixture. |
| `MRHomePageDmnService.ZdListSubmit` qua `MRHomePage/SubmitZdList` — nhánh UPDATE | **ĐẠT** | Fixture STG `mr_basy_zd` Id `7FIX-ZD1` (BAH=G7BAH) JBMC ChanDoanCu→ChanDoanMoi; demo `mr_basy_zd` 01000 = 0. Sau test đã xóa fixture + khôi phục `mr_basy` CYSJ/SJZYTS về NULL. |
| `ZdListSubmit` — nhánh INSERT | **CHƯA** (lỗi pre-existing, không phải ghi demo) | INSERT fail: `Cannot insert NULL into column 'CreatorCode', table 'Newtouch_EMR_STG.dbo.mr_basy_zd'`. Code nhánh insert không bao giờ set CreatorCode (signature `ZdListSubmit(list, orgId)` không nhận user) — cần quyết định sửa riêng (lấy CreatorCode từ session, đổi signature). Không tự sửa trong mục 4. |
| `SynchronizationZD` (DELETE + INSERT CIS `zy_PatDxInfo` zdlb='2') | **CHƯA live** (code routing đã soát, gate chặn staging) | DELETE + INSERT dùng DbNames CisDb ✓, nhưng: (a) trigger `SubmitZdList` chỉ gọi sync khi `DiagnosticSave(Code)` trả ON, mà `DiagnosticSave` đọc `[Newtouch_EMR].[dbo].[Sys_Config]` cứng (demo) — không thể tạo điều kiện sync trên staging nếu không ghi demo (cấm); (b) câu COUNT trong sync (`sqlcx`) cũng đọc `[Newtouch_CIS]` cứng (read leak, không đổi đích ghi). Ở production (DB thật trùng tên hardcode) luồng này chạy bình thường — ghi rõ điểm này. |
| `MedicalRecordDmnService.BLJG_Delete` (DELETE `bl_ysjgnr`) | **CHƯA** | Các caller duy nhất là luồng lưu tài liệu DCSoft (`MedicalRecordController` 254/547/2666/2778) + `PatInfoController.UpdatePatMedRecordRel` (bị chặn trước bởi `UpdtPatMedRecord` NRE đã biết từ giai đoạn 6). SQL dùng DbNames EmrDb (đã soát code), nhưng không có action gọi được live. |
| `BlmblbRepo.BtnSubmit` (INSERT `bl_hljldata`:149) | **CHƯA có đường gọi** | Grep không thấy caller nào ngoài interface/impl (TemplateDesignController dùng `_HljlDataRepo.BtnSubmit`, không dùng BlmblbRepo). Ghi rõ, không báo đạt. |

### 4.2 OR (OR-stg 15231, build worktree Release) — 2 ĐẠT, 3 CHƯA; kèm 1 sự cố P0 đã khắc phục

- **Sự cố P0 trong lúc test (ghi trung thực):** Lần đầu chạy OR-stg bằng binary Debug (sai
  cấu hình launcher Release) nên code cũ không DbNames đã ghi vào Sett DEMO: `xt_sfmb`
  sfmb=7FIXMB + `xt_sfmbxm` sfxm=7FIXCODE (STG vẫn 0, demo tăng 1). Đã xóa ngay 2 dòng demo,
  xác minh demo về 0/0. Sau đó kill OR-stg, build lại worktree Release 0 lỗi, restart, test
  lại mới đúng. Bài học: luôn đối chiếu cấu hình build với binary mà launcher chạy; demo-write
  đầu tiên là do binary cũ, không phải do code DbNames sai.
- `SysChargeItemDmnService.ChargeTemplate_SubmitForm` (tạo mới) qua
  `SystemManage/SysChargeTemplate/ChargeTemplate_SubmitForm` → INSERT Sett `xt_sfmb` +
  `xt_sfmbxm` (với code items đã tham số hóa, §3.1.7a): **ĐẠT** — STG `xt_sfmb` (7FIXMB,
  MauThuPhi7Fix, zt=1) + `xt_sfmbxm` (7FIXCODE, sl=2, zt=1); demo 0/0. Sau test đã xóa dòng
  STG. Lưu ý: lần tạo đầu cần đủ trường VO, controller tự set `zt`/`fybz` từ "true"→"1" và
  `ks` từ SysConfig `OperationDeptConfig`.
- `OpApplyController.DeleteApply` (area Operation) → UPDATE OR `sqzt`: **ĐẠT** — fixture OR
  STG `OR_ApplyInfo` Applyno `7FIX-OR1` (Id, OrganizeId demo org, zt=1, sqzt=1) sqzt 1→3
  (EnumSqzt.yqx=3); demo 0. Sau test đã xóa fixture.
- `ORApplyInfoRepo.DeleteForm(keyValue, orgId, ssmcn)` → SELECT + UPDATE CIS `zy_lsyz`:
  **CHƯA có đường gọi thật** — caller duy nhất có thể gọi đã bị comment out trong
  `Newtouch.HIS.OR/Newtouch.OR.Web/Controllers/ORApplyInfoController.cs:60`
  (`//_oRApplyInfoRepo.DeleteForm(keyValue);`, action vẫn trả Success giả).
  `OpApplyController.DeleteApply` (area) tự update sqzt, không chạm zy_lsyz. Method đã tham
  số hóa ở mục 3 + DbNames-routed, nhưng không gọi live được — KHÔNG tự bỏ comment
  (đổi hành vi, cần duyệt).
- `Temporary_ordersRepo` (INSERT CIS `zy_lsyz`): **CHƯA có đường gọi** — chỉ được inject vào
  `OpApplyController` (ctor) nhưng không method nào trong controller dùng nó (đăng ký DI ở
  `Program.cs:44`).
- `SysRepos` (`.AS("[NewtouchHIS_Base]..Sys_GlobalConfig")`): **CHƯA có đường gọi** — grep
  không thấy caller nào ngoài chính file repo.

### 4.3 PDS (PDS-stg 15237 web Release + PDS API-stg 15337) — 4 ĐẠT, nhiều CHƯA có lý do

Lưu ý hạ tầng PDS: worktree PDS Web vẫn còn `appsettings.Staging.json` từ giai đoạn 6 (fix
repo tĩnh PdsDb) — đã giữ nguyên, không ghi đè. PDS API không có appsettings riêng, nó load
PDS Web appsettings (cùng pattern CIS API; xem `PDS.API/Program.cs` dòng ~15-27). Launcher
staging cho PDS API không có sẵn trong repo nên đã tạo mới ở
`~/workspace/7fix-scratch/run-pds-api-stg.sh` (port 15337, binary worktree Release, DBConf
PDS Web: 0 BaseDb→Base STG, 1 UnionDb→Union STG, 2 PdsDb→PDS STG, 3 CisDb→CIS STG; BaseAPI
17001, Redis 6380).

| Đường | Kết quả | Bằng chứng / lý do |
|---|---|---|
| `MzCfRepo.UpdateCfYsshyj` qua `OutPatientPharmacyController:1487` (route `/OutpatientPharmacy/UpdateCfYsshyj`, không có area) → UPDATE CIS `xt_cf.ysshyj` | **ĐẠT** | Fixture CIS STG `xt_cf` cfh=7FIXCF (các cột NOT NULL: cfId, OrganizeId, jzId, sfbz, cflx, cfh, zje, ys, CreateTime, CreatorCode, zt) ysshyj NULL→YSSHYJ7FIX; demo 0. Route lần đầu thử `/OutPatientPharmacy/OutPatientPharmacy/...` → 404 (controller không có area). Sau test đã xóa fixture. |
| `DispenseIndexInfoDmnService.DeleteYzxx` qua `HospitalizationPharmacyController:208` → UPDATE PDS `zy_ypyzxx.zt` | **ĐẠT** | Fixture PDS STG `zy_ypyzxx` (yzId=7FIXYZ, zxId=7FIXZX, fybz='0', OrganizeId demo org; lưu ý Id là IDENTITY, không insert Id tường minh; zt mặc định lạ là 'NU') zt NU→0; demo 0. Đồng thời chứng minh repo tĩnh (`ZyYpyzzxRepo` qua `_dataContext`) đọc/ghi STG trên PDS-stg (nhờ appsettings.Staging.json). Sau test đã xóa fixture. |
| `SysPharmacyDepartmentBaseDmnService.submitForm` qua `PharmacyDepartmentController.submitForm` → INSERT Base `xt_yfbm` | **ĐẠT** | Cần đủ trường: OrganizeId phải là tổ chức y tế (kiểm tra `IsMedicalOrganize`), yfbmCode=7FIXYF, yfbmmc, ksCode, mzzybz=2, py (lần lượt fail ksCode NULL → mzzybz NULL → py NULL mới ra nguyên nhân; lỗi nào cũng ghi rõ đích là `NewtouchHIS_Base_STG.dbo.xt_yfbm`, chứng minh routing đúng ngay cả khi fail). Kết quả: STG có dòng (7FIXYF, KhoaDuoc7Fix, zt=1); demo 0. Sau test đã xóa fixture. |
| `DrugStorageDmnService.SaveInStorageInfo` qua `DrugStorageController:83` → INSERT PDS `xt_yp_crkdj` + `xt_yp_crkmx` | **ĐẠT** | Cần 2 bước cùng session (xem bẫy dưới): `Home/SetUserPharmacy` (yfbmCode=7FIXYF, mzzybz=2, yfbmjb=1) rồi `SaveInStorageInfo` (Pdh=7FIXPDH, Ckbm, Crkfsdm, detail Ypdm/Sl/Pfj/Lsj/Zje/pc). Lỗi lần lượt: Rkbm NULL (do CurrentYfbm null) → pc NULL (detail). Kết quả: STG `xt_yp_crkdj` (7FIXPDH, Rkbm=7FIXYF, zt=1) + `xt_yp_crkmx` (7FIXYP, Sl=10, pc=7FIXPC); demo 0. Sau test đã xóa fixture. Bẫy: PDS `LoginController` reset CurrentYfbm (nếu user không có danh sách yfbm thì set null) nên `SetUserPharmacy` + `SaveInStorageInfo` PHẢI chạy trong cùng 1 session — đã viết `~/workspace/7fix-scratch/stg_call2.py` (login 1 lần, gọi 2 POST liên tiếp). Ngoài ra log PDS-stg luôn có lỗi session Redis `ArgumentNullException (Parameter 'configuration')` ở `SessionMiddleware` khi commit session (key Redis session của PDS Web khác key launcher set) — chỉ là nhiễu log, không chặn action (các action khác vẫn success). |
| `SysUserExDmnService.RevisePassword` qua PDS `UserController.SubmitPersonalPassword` | **CHƯA có đường gọi live** | Controller `Controllers/Base/UserController.cs` bị loại khỏi build bởi csproj: `Newtouch.HIS.PDS/Newtouch.PDS.Web/Newtouch.PDS.Web.csproj:71` `<Compile Remove="Controllers/Base/UserController.cs" />`. Đã xác minh: DLL Release (cả worktree lẫn main) không chứa tên method `SubmitPersonalPassword` (chỉ còn chuỗi JS `url: "/User/SubmitPersonalPassword"`); gọi `/User/SubmitPersonalPassword`, `/Base/User/...`, `/Base/UserController/...` đều 404 và không vào log pipeline. Lưu ý: `:110` (Locked UPDATE) nằm trong `CheckLogin` đã chết (xem §2.3d); `:308` chính là `RevisePassword` này. |
| `SysPharmacyWindowDmnService.submitForm` qua `PharmacyWindowController.submitForm` | **CHƯA có đường gọi live** | Cùng csproj: dòng 64 `<Compile Remove="Controllers/PharmacyWindowController.cs" />`. Gọi thử → 404. Ghi rõ đây là artifact của quá trình port (nhiều controller PDS bị Remove khỏi build), không phải lỗi DbNames. |
| `SysMedicine*` (catalog thuốc → Base `xt_yp`/`xt_ypsx`/`xt_yfbm_yp`/`xt_kssType`) | **CHƯA có đường gọi ghi live** | `SysMedicineController.submitForm` đã bị comment out, và toàn bộ `SysMedicineController.cs` cũng bị Remove khỏi build (csproj dòng 65). Các method còn lại của service chỉ được dùng cho đọc (GetGridJson...). |
| `ZyYpyzzxRepo.UpdateFybzTy` (UPDATE PDS `zy_ypyzxx.fybz`) | **CHƯA có đường gọi** | Method repo, grep không thấy caller nào trong Web/API controllers. |
| `TSqlStock` (`KsbyStock`/`KsbyTslStock`) | **CHƯA kiểm chứng ghi (đúng bản chất: chỉ đọc)** | Được dùng bởi `HandOutMedicineDmnService` (direct_delivery_batch_search/submit) và `SysMedicineStockCarryDownDmnService` (yp_xt_kcjz) — đều là truy vấn tồn kho, không phải đường ghi; không probe đọc trong mục 4 (mục 4 là kiểm chứng ghi). Ghi rõ để không ai hiểu nhầm là "bỏ sót đường ghi". |
| `DispenseIndexInfoDmnService` các method thao tác khác (`ZYBQFYOperate`, `ZYBQFYReturn`, `UPBQPYyzzxZT`, `InsertReturnDispensingMedicine`, `ApplyNoSendDrugs`, `KcApplyNoReturnDrugs`) | **CHƯA** (lý do: chuỗi fixture nặng, không phải không thể làm) | Cần chuỗi fixture sqd/yzxx/zx/phiếu lĩnh-trả (nhiều bảng PDS + CIS). Đã ưu tiên các đường có fixture đơn trong thời gian còn lại; ghi rõ cần dựng fixture chuỗi này mới kiểm chứng được, KHÔNG báo đạt. |
| `HospitalizationController.ZYTYInterface` (PDS API, route `api/Hospitalization/ZYTYInterface`) → UPDATE PDS `zy_ypyzxx.sqtybz` | **CHƯA live phần ghi (gate chặn staging, đã chứng minh)** | Controller gốc KHÔNG kiểm tra token (giữ nguyên theo comment trong code; không cần token). `NewDb()` hardcode `string.Format(template, "NewtouchHIS_PDS")` (kết nối demo) — nhưng UPDATE dùng DbNames PdsDb nên về lý thuyết vẫn nhắm STG qua tên 3 phần. Tuy nhiên gate `IsCouldReturn` đọc HARDCODE `NewtouchHIS_PDS.dbo.zy_ypyzxx` + `NewtouchHIS_Base.dbo.V_C_xt_yp` (demo): trên staging với fixture STG-only (zxId=7FIXZX2, ypCode=7FIXYP, fybz='2', sqtybz='0', zxrq=2026-10-27) gọi ZYTYInterface trả đúng Error3 "Không thể tra cứu thông tin y lệnh", STG sqtybz giữ 0, demo 0. Ở production (DB thật trùng tên hardcode) gate + write cùng DB nên luồng cohérent. Cần ghi rõ: write SQL đã DbNames, gate demo-bound là điểm chặn staging. PDS API GetToken staging đã kiểm chứng riêng: POST `api/Auth/GetToken` (Account + Password dạng md5 + TokenType + Timestamp dạng "yyyy-MM-dd HH:mm:ss") → 10000 + token đã cấp (không ghi token). |

### 4.4 CIS (CIS-stg 15234 web Release + CIS API-stg 15334) — 5 ĐẠT, 7 CHƯA

| Đường | Kết quả | Bằng chứng / lý do |
|---|---|---|
| `AllergyManageDmnService.DeleteAllergyInfo` qua `NurseManage/Allergy/DeleteExecutedGmxx` → UPDATE CIS `xt_gmxx.zt` | **ĐẠT** | Fixture CIS STG `xt_gmxx` Id=7FIXGM1 (NOT NULL: Id, xm, xmCode, xmmc, Result, CreateTime, CreatorCode, mzzybz, OrganizeId, zt) zt 1→0; demo 0. |
| `OutpatientNurseDmnService.Enteragain` qua `OutpatientNurse/EnteragainMuti` → INSERT CIS `xt_gmxx` | **ĐẠT** | Chuỗi fixture 3 bảng: `xt_jz` (jzId=7FIXJZ, blh, xm, xb; NOT NULL gồm jzId/OrganizeId/mzh/mjzbz/blh/ghksmc/ghsj/fzbz/zlkssj/jzks/jzys/jzzt/CreateTime/CreatorCode/zt) + `xt_cf` (cfId=7FIXCF2, jzId=7FIXJZ) + `xt_cfmx` (cfmxId=7FIXMX, ispsbz='1', ypcode/ypmc). Gọi lrjg=1 → dòng gmxx mới (cfmxid=7FIXMX, Result=1, Remark=Dương tính — log console render GBK thành ký tự lạ nhưng giá trị đúng); demo 0. |
| `OutpatientConsultDmnService.UpdatePatient` qua `NurseManage/OutpatientConsult/UpdatePatient` → UPDATE `mz_ghzs.calledstu` | **ĐẠT** | Phát hiện schema quan trọng: `mz_ghzs` KHÔNG tồn tại trong Sett (cả demo lẫn STG đều 0 bảng) mà nằm trong CIS (cả 2 đều có) — UPDATE join `Sett..mz_gh` với `mz_ghzs` (không prefix, theo connection CIS của service). Fixture: Sett STG `mz_gh` (ghnm=799001 dạng INT, patid INT, các cột NOT NULL: ghnm/OrganizeId/patid/ks/ghf/zlf/ckf/gbf/CreatorCode/CreateTime/kh/zt/xb/blh/CardType/CardTypeName, mzh=7FIXMZ) + CIS STG `mz_ghzs` (Id IDENTITY tự tăng = 5, ghnm=799001, OrganizeId, zt='1', calledstu=0). Gọi calledstu=1 → 0→1; demo CIS `mz_ghzs` ghnm=799001 = 0, demo Sett `mz_gh` mzh=7FIXMZ = 0. Lưu ý: `ISfalgPatient` chạy trước đọc `NewtouchHIS_Sett..mz_gh` cứng (demo) — với mzh STG-only nó trả 0 nên không chặn (không vào nhánh falg==1); `UpdataZSinsert` chạy sau không ảnh hưởng kết quả. |
| `DoctorserviceDmnService.CancaljyjcExec` qua `NurseManage/OrderExecution/CancaljyjcExec` → DELETE CIS `xt_jyjcexec` + DELETE Sett `zy_xmjfb` | **ĐẠT** (phần Sett; phần CIS không có fixture) | Fixture Sett STG `zy_xmjfb` (jfbbh INT=799001, cxzyjfbbh INT=799002, các NOT NULL: jfbbh/OrganizeId/tdrq/sfxm/dl/ys/ks/dj/sl/cxzyjfbbh/CreatorCode/CreateTime/zt, yzwym='7FIX-LSYZ' trỏ tới CIS STG `zy_lsyz` sqdh=7FIXSQ). Gọi jyjclist=7FIXSQ → Sett count 1→0; demo 0. CIS `xt_jyjcexec` sqdh=7FIXSQ trước/sau đều 0 (không có fixture) — ghi rõ phần này chưa có bằng chứng xóa. |
| `ReportDmnService.ReportAdd` qua `ReportManage/Report/ReportEdit` → INSERT/UPDATE Base `ReportSvsConfig` | **CHƯA** (lỗi port pre-existing, không phải ghi demo) | `ReportController` khai báo `private readonly IReportDmnService reportDmnService;` (dòng 29) nhưng **không có constructor nào gán nó** (grep `public ReportController` = 0, grep gán = 0) → mọi action dùng nó đều NRE tại `ReportEdit:215` (Request.Data log cho thấy `vo` bind đúng, RptCode=7FIXRPT, OrganizeId đã set — NRE đến từ field null, không phải vo null). Service method đã DbNames + tham số hóa RptCode, nhưng không tới được. Lưu ý thêm: câu SELECT kiểm tra trùng trong `ReportAdd` vẫn đọc `[NewtouchHIS_Base]` cứng (read leak). KHÔNG tự thêm constructor (mở lại cả controller chết — cần duyệt). |
| `OutpatientConsultDoctorRepo.UpdateZS` qua `NurseManage/OutpatientConsult/SaveConsultDoctor` → UPDATE Base `xt_zs` | **CHƯA** (bị chặn bởi lỗi upstream cùng action) | `SaveConsultDoctor` gọi `SubmitForm` (INSERT `mz_zsys`) TRƯỚC `UpdateZS`; SubmitForm fail: `Cannot insert explicit value for identity column in table 'mz_zsys'` (entity Id int không được Sugar nhận là identity → INSERT kèm Id tường minh). Do đó `UpdateZS` không bao giờ chạy trong action này; `xt_zs` fixture 7FIXZS giữ nguyên NULL. SQL UpdateZS đã DbNames + tham số hóa (đã soát code). Cần sửa mapping identity của `mz_zsys` (quyết định riêng), không tự sửa trong mục 4. |
| `OrderAuditDmnService.Inputskintestresults` → UPDATE/INSERT CIS `xt_gmxx` | **CHƯA** (2 lỗi pre-existing + 1 fix đã làm) | (1) `_allergyRepo` trong `OrderAuditDmnService` KHÔNG BAO GIỜ được gán (ctor chỉ nhận 2 repo khác) → `FindEntity` NRE cho mọi item (đã chứng minh bằng patch log tạm vào worktree rồi revert: exception thật là NullReferenceException, khác với message rỗng do catch nuốt `ex.InnerException` null). (2) Nhánh INSERT cũ nối chuỗi + thiếu 1 giá trị (select 21 giá trị cho bảng 22 cột, thiếu `cancel`) nên không bao giờ chạy được — **đã sửa tham số hóa + cột tường minh + cancel='1'** (§3.1.7b), nhưng vẫn không tới được nhánh này vì (1) chặn trước. KHÔNG tự thêm DI (mở lại đường chết — cần duyệt). |
| `DoctorserviceDmnService.AdviceStop` / `AdviceDel` (qua `DoctorManage/DoctorsAdvice/StopSubmitForm` / `DelForm`) → UPDATE CIS order + UPDATE OR `OR_ApplyInfo.sqzt=3` | **CHƯA** (entity↔schema drift pre-existing) | `AdviceStop` fail ngay ở `FindEntity` đầu tiên (dòng 1840): Sugar select toàn bộ cột của `InpatientSTATOrderEntity`, trong đó có `ztmbId` (property dòng 249) nhưng bảng `zy_lsyz` KHÔNG có cột này (cả demo lẫn STG đều 0) → `Invalid column name 'ztmbId'`. Tương tự `tzsj` cũng không có cột trong bảng. SQL OR-update (DbNames OrDb, sqzt=3, điều kiện zyh/zt/OrganizeId/sqzt<>2/Applyno=@sqdh) đã soát code nhưng không tới được. Cần quyết định schema/entity (thêm cột hoặc đánh dấu ignore) — không tự sửa trong mục 4. Fixture đã dựng vẫn còn: OR STG `OR_ApplyInfo` 7FIXSQ (sqzt=1, chưa đổi), CIS STG `zy_lsyz` 7FIX-LSYZ (sqdh=7FIXSQ, sssj set, zt='1'). |
| `MedicalRecordDmnService.delDzcf` (CIS `xt_dzcf`/`xt_dzcfmx` zt→'0') qua `MedicalRecordController.delDzcf` | **CHƯA live** (gate demo-bound) + **đã fix concat** | Gate SELECT đọc `Newtouch_CIS..xt_dzcf` cứng (demo) với điều kiện `SyncStatus!='1'` — trên staging không thể thỏa nếu không có dòng demo phù hợp (cấm ghi demo). UPDATE sau gate đã DbNames, và **đã tham số hóa cfId** (§3.1.7c, đã triển khai prod). Ở production gate đọc đúng DB thật nên luồng chạy được. |
| `DeptMedicineApplyNoDmnService.UpdateApplyNoStatus` / `UpdateReturnApplyNoStatus` qua CIS API `api/Hospitalized/UpdateApplyNoStatus` → UPDATE CIS `zy_bqksby`/`zy_ksbyth` shzt=5 | **CHƯA** (bảng không tồn tại + gate hardcode) | Đã chứng minh end-to-end với token hợp lệ: CIS API GetToken → 10000 (Timestamp phải dạng ISO `yyyy-MM-ddTHH:mm:ss`, khác PDS API dùng `yyyy-MM-dd HH:mm:ss`; token truyền trong BODY field `Token` + `TokenType`, KHÔNG phải header — header `Token:` trả `TOKEN_IS_REQUIRED`). Gọi UpdateApplyNoStatus → `INTERNAL_SERVER_ERROR: Invalid object name 'Newtouch_CIS.dbo.zy_bqksby'`. Kiểm tra schema: không có bảng `zy_bqksby` hay `zy_ksbyth` trong CIS (demo/STG) hay Sett (chỉ có `Xt_Ksby_Kcfyjl`, `xt_ksby_kcxx` không liên quan). Ngoài ra gate `SelectApplyNoStatus` hardcode `Newtouch_CIS.[dbo].zy_bqksby` (demo read), còn UPDATE dùng DbNames CisDb ✓ — nhưng cả 2 đều không chạy được vì bảng không tồn tại ở DB version này. |
| `HospInterfaceSyncDmnService` / `SettInterfaceSyncDmnService.UpdateOccupyByCode` → UPDATE Base `xt_cw.sfzy` | **CHƯA live** (chuỗi fixture nặng) | Chỉ tới được qua luồng đổi giường/nhận bệnh (`EntryRegistrationDmnService.ChangeBed` và `WardMaintenanceDmnService` bed ops) — cần fixture nhập viện + giường + ward + bed-use đầy đủ. SQL đã soát: cả 2 đều dùng DbNames BaseDb + tham số hóa (`@sfzy,@bq,@cwcode,@orgId,@user`). Ghi rõ cần fixture chuỗi này, KHÔNG báo đạt. |

### 4.5 Center (SysRole) — DeleteForm ĐẠT, SubmitForm CHƯA (lỗi model chung)

Caller đã xác định: SSO portal `Newtouch.HIS.SSO/HIS.SSO/Areas/SysManage/Controllers/SysRoleController.cs`
— `SubmitForm` (dòng 101, gọi `_sysRoleDmnService.SubmitForm(roleEntity, roleAuthorizeEntityList, keyValue, UserIdentity.UserCode, OrganizeId)`)
và `DeleteForm` (dòng 143, gọi `_sysRoleDmnService.DeleteForm(keyValue)`). Ngoài ra còn có HOSP Web
`Sys/RoleController` qua `RoleApp` (DeleteForm/SubmitForm qua `_sysRoleDmnService`), và Sett Web đăng ký
`ISysRoleDmnService` ở `Program.cs:33` — nhưng test đang chạy qua SSO-stg.

Service đã soát: `SysRoleDmnService.DeleteForm(keyValue)` — `GetByWhere<SysRoleEntity>(UnionDb, zt='1' && Id==keyValue)`,
nếu Code=="Administrator" thì throw, rồi `Delete<SysRoleEntity>` + `delete from DbNames(UnionDb)..Sys_RoleAuthorize where RoleId=@RoleId`.
`SubmitForm` — tạo/cập nhật role + xóa authorize cũ theo RoleId rồi thêm mới (cùng mẫu DbNames UnionDb).

- **Nguyên nhân nghi vấn ban đầu:** gọi `POST /SysManage/SysRole/DeleteForm keyValue=7FIXROLE` qua SSO-stg → HTTP 200
  `{"state":"success","message":"Xóa thành công."}` nhưng SELECT ngay sau đó và SELECT lại sau 5 giây đều cho thấy
  STG vẫn 1 role + 2 authorize (không xóa gì), demo vẫn 0/0, log SSO-stg không thấy lỗi DeleteForm. Nghi ngờ đầu tiên:
  controller gọi `_sysRoleDmnService.DeleteForm(keyValue)` mà **KHÔNG await** (method trả `Task`, action trả `ActionResult`
  đồng bộ) — fire-and-forget.
- **Nguyên nhân thật (đã xác minh):** SSO-stg launcher `run-sso-stg.sh` chỉ override DBConf index 0–3
  (Base/Sett/Emr/Mrms) — trong khi DBConf của SSO appsettings có thứ tự 0 BaseDb, 1 SettDb, 2 EmrDb, 3 MrmsDb,
  4 MrQcDb, 5 CisDb, **6 UnionDb**. UnionDb (index 6) KHÔNG được override nên vẫn trỏ demo `Newtouch_Union`.
  Service đọc UnionDb demo → không thấy fixture STG-only 7FIXROLE → danh sách rỗng → `result[0]` throw
  trong task nền (không log, không đổi DB) — khớp mọi quan sát. **Đây là gap cấu hình staging, KHÔNG phải bug
  routing DbNames của service** (ở prod UnionDb = `Newtouch_Union` là đúng).
- **Khắc phục test:** dùng launcher scratch `~/workspace/7fix-scratch/run-sso-stg-union.sh` (bản sao
  `run-sso-stg.sh` + 3 dòng override index 4/5/6 → `Newtouch_MRQC_STG` / `Newtouch_CIS_STG` /
  `Newtouch_Union_STG`), restart SSO-stg (15208), gọi lại DeleteForm → role STG 1→0, authorize STG 2→0,
  demo 0/0. **Kết luận: DeleteForm ĐẠT.** Đề xuất: bổ sung 3 dòng override vào `run-sso-stg.sh` đã commit
  (coordinator duyệt) để lần staging sau không lặp lại gap này.
- **Lưu ý còn lại về fire-and-forget:** controller vẫn không await `DeleteForm`/`SubmitForm` (trả success
  trước khi DB commit) — lần test đầu cần chờ vài giây mới thấy kết quả. Đây là hành vi cần sửa riêng
  (await + đổi action sang `async Task<ActionResult>`) nếu muốn client nhận kết quả thật — không tự sửa
  trong worker này vì đổi signature/hành vi controller.
- `SubmitForm` tạo role mới: **CHƯA** (lỗi port pre-existing, không phải ghi demo). Gọi
  `POST /SysManage/SysRole/SubmitForm` (Code=7FIXROLE2, Name=Role7Fix2, zt=true, permissionIds=ITEM1,ITEM2)
  → HTTP 200 `{"state":"error","message":"Unable to cast object of type 'System.DateTime' to type 'System.String'"}`.
  Nguyên nhân gốc: `IEntity.CreateTime` (kiểu `DateTime`) mang attribute `[StringLength(23)]`
  (`Newtouch.HIS.Center/NewtouchHIS.Base.Domain/Entity/IEntity.cs:26-27`) — bug của generator gốc;
  ASP.NET Core model validation chạy `StringLengthAttribute.IsValid(DateTime)` → throw, nên MỌI post
  entity triển khai `IEntity` qua đường validation này đều fail (không riêng SysRole). Sửa attribute này
  ảnh hưởng toàn bộ entity dùng `IEntity` — cần quyết định riêng, KHÔNG tự sửa trong worker này.
  (Cần xác minh thêm: HOSP `RoleController` qua `RoleApp` có cùng model validation không — ghi nhận để
  kiểm chứng sau khi sửa `IEntity`.)
- Fixture Center: 7FIXROLE + 2 authorize đã xóa sạch (kiểm chứng 0/0 sau test); 7FIXROLE2 không bao giờ
  được tạo (0 dòng) nên không còn gì để dọn.

### 4.6 Fixture STG còn tồn tại sau giai đoạn (đã dọn, kiểm chứng 0)

- EMR STG: `mr_basy` G7-EMR-BASY (OrganizeId đã sửa thành top org demo — giữ như fixture đã sửa,
  CYSJ/SJZYTS đã khôi phục NULL); `zy_brjbxx` 01000/01999 (fixture mục 3, RecordStu 01000 đã khôi
  phục 2).
- CIS STG: các fixture 7FIX đã xóa (xt_gmxx 7FIXGM1/7FIXGM2 + dòng Enteragain cfmxid=7FIXMX,
  xt_cf 7FIXCF2, xt_cfmx 7FIXMX, xt_jz 7FIXJZ, zy_lsyz 7FIX-LSYZ, mz_ghzs ghnm=799001); `xt_zs`
  7FIXZS đã xóa ở Base STG (lưu ý: `xt_zs` nằm trong Base, không phải CIS — lần dọn đầu nhầm
  target CIS STG báo `Invalid object name 'xt_zs'`, đã sửa lại và xác minh Base STG = 0).
- OR STG: `OR_ApplyInfo` 7FIXSQ đã xóa.
- Sett STG: `mz_gh` mzh=7FIXMZ đã xóa; `zy_xmjfb` yzwym=7FIX-LSYZ = 0 (đã xóa bởi test);
  `xt_sfmb`/`xt_sfmbxm` 7FIX đã xóa; `xt_yfbm` 7FIXYF đã xóa.
- PDS STG: các fixture 7FIX đã xóa (xt_cf, zy_ypyzxx, crkdj/mx, xt_yfbm).
- Union STG: 7FIXROLE + authorize đã xóa (0/0); 7FIXROLE2 không tồn tại.
- Demo: đã xác minh các đếm demo liên quan đều 0 hoặc không đổi sau mỗi test, trừ sự cố OR ghi
  demo 1 lần đã xóa ngay và xác minh về 0 (§4.2). Không còn dòng demo nào do worker này tạo
  (đã soát lại lần cuối trước khi báo cáo).

---

## 5. FILE ĐÃ ĐỔI TRONG MAIN CHECKOUT (chưa commit — `git status --short`)

15 file source đã đổi (không có file untracked mới trong repo):

```
 M Newtouch.HIS.Center/NewtouchHIS.AuthenticationCenter/Controllers/LoginController.cs
 M Newtouch.HIS.Center/NewtouchHIS.AuthenticationCenter/appsettings.json
 M Newtouch.HIS.Center/NewtouchHIS.Base.DomainService/SysManage/SysConfigDmnService.cs
 M Newtouch.HIS.Center/NewtouchHIS.Base.DomainService/SysManage/SysModuleDmnService.cs
 M Newtouch.HIS.Center/NewtouchHIS.Base.DomainService/SysUser/SysUserVDmnService.cs
 M Newtouch.HIS.Center/NewtouchHIS.WebAPI.Manage/appsettings.json
 M Newtouch.HIS.CIS/Newtouch.CIS.Infrastructure/DomainServices/Inpatient/OrderAuditDmnService.cs
 M Newtouch.HIS.CIS/Newtouch.CIS.Infrastructure/DomainServices/Outpatient/MedicalRecordDmnService.cs
 M Newtouch.HIS.CIS/Newtouch.CIS.Infrastructure/DomainServices/Outpatient/ReportDmnService.cs
 M Newtouch.HIS.EMR/Newtouch.EMR.Infrastructure/Repository/HljlDataRepo.cs
 M Newtouch.HIS.MRMS/Newtouch.MRMS.Infrastructure/DomainServices/MainRecordDmnService.cs
 M Newtouch.HIS.MRMS/Newtouch.MRMS.Infrastructure/DomainServices/RecordDmnService.cs
 M Newtouch.HIS.OR/Newtouch.OR.Infrastructure/ORApplyInfoRepo.cs
 M Newtouch.HIS.OR/Newtouch.OR.Infrastructure/SysChargeItemDmnService.cs
 M Newtouch.HIS.PDS/Newtouch.PDS.Infrastructure/HospitalizationPharmacy/DispenseIndexInfoDmnService.cs
```

Cộng DLL nhị phân trong các bin (gitignored phần lớn; `.bak-7fix` trong bin là gitignored, giữ
trên disk để rollback). **KHÔNG chạy `git add -A`**. Lưu ý: lần build host từ main có thể làm
bẩn thêm file bin tracked — coordinator soát cuối; worker chỉ báo cáo danh sách file source đã
soát. Worktree `~/workspace/openhis-stg` KHÔNG commit gì (đã copy file từ main sang để build
staging; không đụng `Program.cs` của worktree).

### Script scratch (ngoài repo, không chứa secret — chỉ tham chiếu biến env)

- `~/workspace/7fix-scratch/sqlq.sh` (SQL helper: sqlcmd
  `~/workspace/mssql-tools/opt/mssql-tools18/bin/sqlcmd` — KHÔNG phải đường dẫn cũ
  `~/workspace/mssql-tools/bin/sqlcmd`; `LD_LIBRARY_PATH=~/workspace/mssql-tools/usr/lib/x86_64-linux-gnu`,
  `ODBCSYSINI=~/workspace` (file `~/workspace/odbcinst.ini` trỏ driver đúng đường dẫn workspace),
  `-C -b -s "," -W -h -1`, biến mật khẩu là `MSSQL_SA_PASSWORD` (không phải `SA_PASSWORD`) từ
  `~/workspace/.sqlserver.env` (600). Luôn dùng `-b` và đối chiếu kết quả với server, không tin
  exit code (bài học cũ: sqlcmd không `-b` vẫn exit 0 khi batch lỗi).
- `~/workspace/7fix-scratch/login_test.py`: harness login staging (web root/web/center), tự tính
  md5 trong bộ nhớ, chỉ in state/message (đã che token).
- `~/workspace/7fix-scratch/stg_login.py`: login staging → lưu jar (BỊ LỖI: curl không gửi được
  cookie session từ file jar này — đừng dùng cho gọi tiếp theo).
- `~/workspace/7fix-scratch/stg_call.py`: login + gọi 1 endpoint trong cùng process python
  (cookie trong bộ nhớ) — **dùng cái này** cho mọi kiểm chứng ghi staging cần session:
  `stg_call.py <port> <account> POST <path> k=v...` (path PHẢI có `/` ở đầu).
- `~/workspace/7fix-scratch/stg_call2.py`: login 1 lần rồi gọi 2 POST cùng session (dùng cho PDS
  SetUserPharmacy + SaveInStorageInfo).
- `~/workspace/7fix-scratch/start-baseapi-stg-wt.sh`, `start-authcenter-stg-wt.sh`: launcher
  staging trỏ binary worktree.
- `~/workspace/7fix-scratch/run-pds-api-stg.sh`: launcher PDS API staging tự tạo (port 15337).
- `~/workspace/7fix-scratch/run-sso-stg-union.sh`: launcher SSO-stg scratch (thêm override
  UnionDb/MrQcDb/CisDb → STG; xem §4.5).
- `~/workspace/7fix-scratch/roll-dll.sh`: roll DLL 1 host (kill PID → backup `.bak-7fix` →
  copy DLL tươi → launcher → chờ cổng).
- `~/workspace/7fix-scratch/rebuild-restart.sh`: rebuild + restart 1 host prod — **ĐÃ SỬA lỗi
  pipeline** (dùng file log + RC, xem §3.3).
- Log staging: `~/workspace/7fix-scratch/{redis-stg,baseapi-stg,authcenter-stg,hosp-stg,cis-api-stg,mrms-stg,emr-stg,or-stg,pds-stg,pds-api-stg,cis-stg,sso-stg}.log`.

### Cụm staging — trạng thái cuối giai đoạn: ĐÃ DỪNG HẾT

Đã dừng theo PID chính xác lấy từ `ss -tlnp`: SSO-stg 15208, CIS-stg 15234, CIS API-stg 15334,
BaseAPI-stg 17001, AuthCenter-stg 18015, Redis-stg 6380 (PDS API-stg 15337 đã down từ trước
do sự cố crash, không cần kill). Xác minh cuối: không còn cổng staging nào LISTEN.

---

## 6. CÒN LẠI CHO GIAI ĐOẠN 8

(Ngoài các mục đã giao sẵn cho giai đoạn 8: chuẩn hóa Debug→Release cho CIS web/OR web/MRMS
web/CIS API; Secure cookie khi có TLS; purge log cũ + retention 30 ngày; xoay AppSecret/JWT.Key
— cần quyết định riêng của user; cơ chế bắt đổi mật khẩu lần đầu; đọc rò rỉ SELECT — không
chặn, đã có tài liệu; thời điểm cắt tài khoản demo thật — quy trình ở REPORT giai đoạn 7 §3.4,
chờ duyệt thời điểm.)

1. **Xác nhận login SSO 000000 trên prod** bằng mật khẩu demo ĐÚNG ngay từ lần thử đầu tiên
   (worker không tự làm — xem phần Tóm tắt; sau lockout live, tuyệt đối không đoán mật khẩu).
2. **Lỗi model chung `IEntity.CreateTime` mang `[StringLength(23)]`** (§4.5): phá mọi post
   entity qua model validation (đã chứng minh ở SysRole SubmitForm). Cần quyết định sửa
   (bỏ attribute) + kiểm chứng lại HOSP `RoleController` qua `RoleApp` sau sửa.
3. **Thiếu DI/constructor (đường chết, cần duyệt trước khi mở lại):** `ReportController`
   (thiếu constructor gán `reportDmnService`); `OrderAuditDmnService._allergyRepo` (không bao
   giờ được gán); bỏ comment gọi `ORApplyInfoRepo.DeleteForm`; fire-and-forget
   `SysRoleController.DeleteForm`/`SubmitForm` (await + async Task).
4. **Entity↔schema drift (cần quyết định schema/entity):** `InpatientSTATOrderEntity.ztmbId`
   (+ `tzsj`) không có cột trong `zy_lsyz`; bảng `zy_bqksby`/`zy_ksbyth` không tồn tại ở DB
   version này (cả CIS lẫn Sett); `mz_ghzs` nằm trong CIS chứ không phải Sett (đã xác minh);
   `mz_zsys` identity mapping (INSERT kèm Id tường minh).
5. **PDS controller bị Remove khỏi build** (csproj): `Controllers/Base/UserController.cs`
   (dòng 71), `Controllers/PharmacyWindowController.cs` (dòng 64), `SysMedicineController.cs`
   (dòng 65) — quyết định có đưa lại vào build hay không (ảnh hưởng đường RevisePassword,
   PharmacyWindow, SysMedicine).
6. **EMR `ZdListSubmit` nhánh INSERT thiếu CreatorCode** (§4.1): cần quyết định lấy
   CreatorCode từ session / đổi signature.
7. **Các đường mục 4 cần fixture chuỗi mới kiểm chứng được:** DispenseIndex operate còn lại
   (sqd/yzxx/zx/phiếu lĩnh-trả); `HospInterfaceSyncDmnService`/`SettInterfaceSyncDmnService`
   (ChangeBed/bed-maintenance: nhập viện + giường + ward + bed-use); `SynchronizationZD`
   (cần `DiagnosticSave` ON — bị chặn bởi config demo-bound); `BLJG_Delete` (luồng DCSoft);
   `BlmblbRepo.BtnSubmit`, `Temporary_ordersRepo`, `SysRepos`, `ZyYpyzzxRepo.UpdateFybzTy`
   (no caller — quyết định nối lại hay bỏ).
8. **Nối chuỗi SQL còn tồn đọng** (liệt kê §3.1.8): INSERT bulk `EMR MedicalRecordDmnService`
   (BLJG_Save), SQL động `DispenseIndexInfoDmnService` StrSQL, IN nội bộ CIS
   `OrderAuditDmnService` (456–491), `TOP` int PDS `DrugStorageDmnService`, enum int Herp
   `WzProductDmnService:289`.
9. **Launcher staging:** bổ sung override DBConf index 4/5/6 vào `run-sso-stg.sh` đã commit
   (gap §4.5); bổ sung launcher PDS API-stg (hiện chỉ có ở scratch) nếu muốn staging PDS API
   tái sử dụng; launcher staging nào cũng phải kiểm tra cấu hình build (Release) khớp binary
   (bài học §4.2).
10. **Đề xuất chưa thành quyết định:** việc nối lại đường PDS `SysUserExDmnService.CheckLogin`
    sau này (khi đó nguồn đếm `Sys_Log` đã tồn tại nhờ 7-fix); việc sửa `mz_zsys` identity;
    việc sửa fire-and-forget SysRole — tất cả chỉ ghi nhận, không thực hiện trong worker này
    nếu chưa có phê duyệt.
11. **Gap đã biết từ giai đoạn 7, chưa đụng trong worker này:** live-path lockout KHÔNG enforced
    trước 7-fix nay đã xong (§2) — nhưng **first-login forced password change vẫn chưa có**;
    log retention (30 ngày) chưa có cơ chế + log cũ vẫn chứa JWT (purge trước bàn giao, giai
    đoạn 8); secret chưa xoay (AppSecret/JWT.Key/Redis password — chờ quyết định user);
    live-path lockout cho GetToken đã kế thừa qua CheckPwd (§2.2) — ghi rõ để khỏi nhầm là
    chưa làm.
12. **Quy trình vận hành đã kiểm chứng, cần đưa vào tài liệu bàn giao:** mở khóa thủ công
    (§2.4), break-glass (§1.5), rollback DLL `.bak-7fix` (§1.3), quy trình dừng/khởi động
    staging theo PID chính xác (§5).

---

*Hết báo cáo worker 7-fix. File này chưa commit — điều phối viên soát rồi commit.*
