# MRMS.API — .NET 6 port (Scope B)

Host API của module MRMS, chạy trên **http://127.0.0.1:5332** (launcher
`~/workspace/openhis-core/run-mrms-api.sh`, log `~/workspace/openhis-core/mrms-api.log`).
Project: `Newtouch.HIS.MRMS/Newtouch.MRMS.API/` (đã thêm vào `Newtouch.HIS.MRMS.sln`).
Build: **Release, 0 error** (chỉ warning MSB3277 xung đột version, cùng họ với bản Web/SSO).

## Tái dùng, không viết lại

- Data layer của MRMS Web: `ISysConfigRepo` (SqlSugar, `MrmsDb`
  → `Newtouch_MRMS..Sys_Config`) qua ProjectReference tới
  `Newtouch.MRMS.Infrastructure`.
- Cấu hình `BusinessDB` + `RedisConfig` đọc từ file appsettings của MRMS Web
  (`../Newtouch.MRMS.Web/appsettings.json`, nạp thêm trong `Program.cs`) để không
  copy credential sang file mới. Launcher override `BusinessDB__DefaultConn` bằng
  env, lấy mật khẩu SA từ `~/.sqlserver.env` lúc chạy (không ghi vào script).
- `Program.cs` phải nạp lại env vars SAU file Web (`AddEnvironmentVariables()`),
  vì provider thêm sau sẽ thắng provider trước — nếu không, `DefaultConn` trong
  file Web sẽ lấn át env override và gây lỗi `Login failed for user 'sa'`.
- Bắt buộc như MRMS Web: proxy env UNSET + `DOTNET_SYSTEM_NET_DISABLEIPV6=true` +
  `ASPNETCORE_URLS` explicit (DLL chạy trực tiếp bỏ qua launchSettings).

## Envelope (tái tạo theo OR.API, không bịa)

Bản port sao chép đúng envelope đã tái tạo ở OR.API (từ bản mirror source bên PDS
của `FrameworkBase.API.ApiControllerBase` + DLL Framework):

- `code`/`sub_code`/`sub_msg`/`data`; mã số theo họ Lib.Base
  (`SUCCESS=10000`, `ERROR=20000`, `FAIL=40004`).
- `CommonExecute`: request null → `SOMEARGUMENTS_IS_REQUIRED`; thiếu
  `Timestamp` → `TIMESTAMP_IS_REQUIRED`; `Validate()` kiểm tra `[Required]` →
  `{FIELD}_IS_REQUIRED`; map exception token → `TOKEN_TYPE_IS_REQUIRED` /
  `TOKEN_IS_REQUIRED` / `TOKEN_IS_EXPIRED` / `TOKEN_TYPE_NOT_MATCH` /
  `TOKEN_IS_ILLEGAL`; lỗi hệ thống → `ERROR`/`INTERNAL_SERVER_ERROR`.
- `Authorize`: token resolve từ Redis (key = token thô, value = JSON identity,
  gia hạn 20 phút). Default Query gốc KHÔNG gắn `[DefaultAuthorize]` nên
  không bắt token — đúng hành vi gốc.
- JSON trả về: Newtonsoft, indent, ngày `yyyy-MM-dd HH:mm:ss`; hỗ trợ
  `jsonpCallback` như gốc.

Khác biệt có chủ đích (ghi rõ, không giấu):

- Không ghi monitor log qua AppLogger (host không wiring NLog monitor).
- `sub_msg` lỗi hệ thống chỉ chứa `Exception.Message`, KHÔNG kèm StackTrace
  (gốc kèm cả stacktrace — rò rỉ nội bộ).
- Token dùng THÔ làm key Redis. Gốc có `TokenDecryptFilterAttribute` toàn cục
  để giải mã token trước khi Authorize, nhưng thuật toán giải mã nằm trong DLL
  API.Common, không có source — không tự bịa. Client gửi token đã mã hóa DES
  sẽ nhận `TOKEN_IS_EXPIRED`.
- Body rỗng: `[ApiController]` của ASP.NET Core trả HTTP 400 validation trước
  khi vào action, nên không tới được nhánh `SOMEARGUMENTS_IS_REQUIRED` của
  CommonExecute — cùng hành vi với OR.API (cũng gắn `[ApiController]`).

## Endpoint

| Route | Trạng thái |
|---|---|
| `POST api/Default/Query` | Chạy thật (trả toàn bộ `Sys_Config`, đúng hành vi gốc: không lọc theo `keyword`). Demo DB: 9 rows → `code 10000, data[9]`. |
| `GET api/Default/{id}`, `POST api/Default`, `PUT api/Default/{id}`, `DELETE api/Default/{id}` | Stub `NOT_IMPLEMENTED` (scaffold template gốc chỉ trả `"value"`/rỗng, không có nghiệp vụ). |
| `POST api/Auth/GetToken` | **Stub lỗi `GETTOKEN_DISABLED`, KHÔNG BAO GIỜ trả token.** Bản gốc không xác thực credential (identity toàn `"?"`, bước 1 chỉ là comment) mà vẫn cấp token Redis thật — port nguyên vẹn sẽ là hồi quy bảo mật. Muốn mở lại phải gắn kiểm tra credential thật (ví dụ BaseAPI CheckPwd) trước. |

## Bug gốc đã sửa trong port

- `DefaultController._sysConfigRepo` ở bản Framework không bao giờ được gán
  (ctor chỉ nhận `IComponentContext`, kèm cả pragma warning CS0649 thừa nhận
  điều đó) — bản Core inject qua DI nên Query chạy thật thay vì NullReference.

## Còn lại (thuộc về parent, không tự làm)

- **MRMS.API cần thêm vào watchdog** (recovery.sh section 22 đã thêm) — restart
  máy sẽ mất 5332 cho tới khi parent thêm guard + HTTP check.
- Token mã hóa DES (xem mục TokenDecrypt ở trên) và việc mở lại GetToken
  cần quyết định thiết kế của chủ hệ thống.
