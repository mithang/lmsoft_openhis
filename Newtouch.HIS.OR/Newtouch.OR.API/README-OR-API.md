# OR.API — .NET 6 port (Scope B)

Host API của module OR, chạy trên **http://127.0.0.1:5331** (launcher
`~/workspace/openhis-core/run-or-api.sh`, log `~/workspace/openhis-core/or-api.log`).
Project: `Newtouch.HIS.OR/Newtouch.OR.API/` (đã thêm vào `Newtouch.HIS.OR.sln`).
Build: **0 error** (chỉ warning MSB3277 xung đột version, cùng họ với bản Web/SSO).

## Tái dùng, không viết lại

- Data layer của OR Web: `ISysConfigRepo`, `IOPRegisterDmnService` (SqlSugar, `OrDb`
  → `Newtouch_OR`) qua ProjectReference tới `Newtouch.OR.Infrastructure`.
- Cấu hình `BusinessDB` + `RedisConfig` đọc từ file appsettings của OR Web
  (`../Newtouch.OR.Web/appsettings.json`, nạp thêm trong `Program.cs`) để không
  copy credential sang file mới. Launcher override `BusinessDB__DefaultConn` bằng
  env, lấy mật khẩu SA từ `~/.sqlserver.env` lúc chạy (không ghi vào script).
- `Program.cs` phải nạp lại env vars SAU file Web (`AddEnvironmentVariables()`),
  vì provider thêm sau sẽ thắng provider trước — nếu không, `DefaultConn` trong
  file Web sẽ lấn át env override và gây lỗi `Login failed for user 'sa'`.
- Bắt buộc như OR Web: proxy env UNSET + `DOTNET_SYSTEM_NET_DISABLEIPV6=true` +
  `ASPNETCORE_URLS` explicit (DLL chạy trực tiếp bỏ qua launchSettings).

## Envelope (tái tạo, không bịa)

`FrameworkBase.API.ApiControllerBase` chỉ có DLL + bản mirror source bên PDS
(`Newtouch.PDS.API/App_Start/ApiControllerBase.cs`). Bản port tái tạo đúng:

- `code`/`sub_code`/`sub_msg`/`data`; mã số theo họ Lib.Base
  (`SUCCESS=10000`, `ERROR=20000`, `FAIL=40004`).
- `CommonExecute`: request null → `SOMEARGUMENTS_IS_REQUIRED`; thiếu
  `Timestamp` → `TIMESTAMP_IS_REQUIRED`; `Validate()` kiểm tra `[Required]` →
  `{FIELD}_IS_REQUIRED`; map exception token → `TOKEN_TYPE_IS_REQUIRED` /
  `TOKEN_IS_REQUIRED` / `TOKEN_IS_EXPIRED` / `TOKEN_TYPE_NOT_MATCH` /
  `TOKEN_IS_ILLEGAL`; lỗi hệ thống → `ERROR`/`INTERNAL_SERVER_ERROR`.
- `Authorize`: controller/action có `[DefaultAuthorize]` thì bắt buộc identity;
  token resolve từ Redis (key = token, value = JSON identity, gia hạn 20 phút).
- JSON trả về: Newtonsoft, indent, ngày `yyyy-MM-dd HH:mm:ss`; hỗ trợ
  `jsonpCallback` như gốc.

Khác biệt có chủ đích (ghi rõ, không giấu):

- Không ghi monitor log qua AppLogger (host không wiring NLog monitor).
- `sub_msg` lỗi hệ thống chỉ chứa `Exception.Message`, KHÔNG kèm StackTrace
  (gốc kèm cả stacktrace — rò rỉ nội bộ).
- Token dùng THÔ làm key Redis. Gốc có `TokenDecryptFilterAttribute` toàn cục
  để giải mã token trước khi Authorize (trừ `[IgnoreTokenDecrypt]`), nhưng thuật
  toán giải mã nằm trong DLL API.Common, không có source — không tự bịa.
  Client gửi token đã mã hóa DES sẽ nhận `TOKEN_IS_EXPIRED`. UA GetUserInfo
  vốn dùng token thô (`[IgnoreTokenDecrypt]`) nên không bị ảnh hưởng.

## Endpoint

| Route | Trạng thái |
|---|---|
| `POST api/Default/Query` | Chạy thật (trả toàn bộ `Sys_GlobalConfig`, đúng hành vi gốc: không lọc theo `keyword`). Demo DB: bảng rỗng → `code 10000, data []` là đúng. |
| `GET api/Default/{id}`, `POST api/Default`, `PUT api/Default/{id}`, `DELETE api/Default/{id}` | Stub `NOT_IMPLEMENTED` (scaffold template gốc chỉ trả `"value"`/rỗng, không có nghiệp vụ). |
| `POST api/ORPatient/ORPatList` (`[DefaultAuthorize]`) | Chạy thật qua `IOPRegisterDmnService.GetPatOpRegistList`. Demo DB: `OR_Registration` rỗng → `data []` khi có token hợp lệ. |
| `POST api/UA/GetUserInfo` | Chạy thật: token thô → Redis, gia hạn 120 phút, thiếu/sai → `AUTH_FAILED` (đúng gốc). |
| `POST api/Auth/GetToken` | **Stub lỗi `GETTOKEN_DISABLED`, KHÔNG BAO GIỜ trả token.** Bản gốc không xác thực credential (identity toàn `"?"`, bước 1 chỉ là comment) mà vẫn cấp token Redis thật — port nguyên vẹn sẽ là hồi quy bảo mật. Muốn mở lại phải gắn kiểm tra credential thật (ví dụ BaseAPI CheckPwd) trước. |

## Bug gốc đã sửa trong port

- `DefaultController._sysConfigRepo` và `ORPatientController._opRegisterDmnService`
  ở bản Framework không bao giờ được gán (ctor chỉ nhận `IComponentContext`) —
  bản Core inject qua DI nên chạy thật thay vì NullReference.

## Vấn đề gặp lúc chạy (đã fix)

- Thiếu package `Log4Net.AspNetCore` + file `Configs/Log4Net.config` (lấy từ Web
  qua Content reference) + package `Mapster` 7.4.0 (AddWebServiceBase cần) +
  thiếu 2 singleton `IHttpClientHelper`/`IAuthCenterAppService` (Web đăng ký
  trước AddWebServiceBase) — đều đã bổ sung, host lên được.
- Runtime restart giữa chừng (lần 3 trong ngày) giết toàn bộ stack; đã chạy
  `recovery.sh` khôi phục đủ (1433/6379/41142/7001/5208/5220/5221/5231/5232)
  rồi khởi động lại API.

## Còn lại (thuộc về parent, không tự làm)

- **OR.API chưa có trong `recovery.sh`/watchdog** — restart máy sẽ mất 5331
  cho tới khi parent thêm guard (launcher `run-or-api.sh` đã sẵn).
- Token mã hóa DES (xem mục TokenDecrypt ở trên) và việc mở lại GetToken
  kèm xác thực thật.
- Không ghi DB, không đổi schema trong toàn bộ task này.
