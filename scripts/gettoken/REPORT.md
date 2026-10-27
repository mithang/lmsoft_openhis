# Báo cáo Giai đoạn 2 — GetToken kiểm tra credential thật (8 module API)

- **Ngày:** 2026-10-27 (ICT). **Worker:** giai đoạn 2, làm tuần tự từng API một.
- **Repo:** `~/workspace/openhis-core`, nhánh `V2.2`, tip lúc bắt đầu `9d051cb6`,
  `git status` lúc bắt đầu **sạch** (không có file đổi nào).
- **Kết thúc:** KHÔNG commit, KHÔNG push. Diff chưa commit để điều phối viên soát
  (22 file đổi, xem §7). File báo cáo này là untracked mới trong repo.
- **Quy tắc token/mật khẩu trong báo cáo:** chỉ ghi 8 ký tự đầu của token test;
  không ghi mật khẩu nào (kể cả dạng md5), không ghi mật khẩu SA.
- **Stack không đổi kiến trúc:** SQL Server 1433, Redis 6379, BaseAPI 127.0.0.1:7001,
  SSO *:5208, 8 module API như cũ. Cuối giai đoạn quét lại 25/25 cổng UP.

## 1. Thiết kế đã xác minh từ code thật (trước khi code)

### 1.1. BaseAPI CheckPwd — contract thật

- Endpoint: `POST api/login/CheckPwd` (`HIS.BaseAPI/Controllers/LoginController.cs`),
  nhận `Request<LoginBO>` với `Data = {Username, Password}` (Password dạng md5 như
  web login gửi từ JS).
- **AppId là bắt buộc:** `RequestBase.AppId` có `[Required]` nên `[ApiController]`
  trả 400 khi thiếu AppId. Vì vậy GetToken luôn gửi `AppId` của chính API gọi
  (lấy từ config của API đó, theo mẫu `LoginAppService.RequestAsync`:
  `AppId = ClientAppId`, `Timestamp = now`). Chuỗi fallback trong code:
  `BusinessConfig:AppId` → `AppId` → tên mặc định của API.
- **Host BaseAPI:** `BusinessConfig:AppAPIHost:HisAppBaseAPIHost` →
  `AppAPIHost:HisAppBaseAPIHost` → `http://127.0.0.1:7001/` (fallback).
- **Envelope bọc ngoài (bẫy đã gặp và sửa):** CheckPwd KHÔNG trả trực tiếp
  `{code, data}` mà trả `{httpStatus, busData: {code, msg, data}}` (đúng mẫu
  `LoginAppService`: `PostAsync<Result>` rồi đọc `BusData`). Lần thử MRQC đầu
  tiên trả `AUTH_FAILED` sai vì code đọc `code` ở lớp ngoài (= 0). Đã sửa cho
  cả 8 API: bóc `busData` trước rồi mới đọc `code/data`.
- **Kết quả CheckPwd trả về:** `Data = {Account, Id, TopOrganizeId, Locked}` —
  **KHÔNG có OrganizeId.** Vì vậy identity lưu Redis dùng
  `OrganizeId = TopOrganizeId` (đúng fallback của web login trong
  `BuildLoginStatusOprAsync`: OrganizeId trống thì lấy TopOrganizeId).
- **Tài khoản khóa:** `UserLogin` trả FAIL (không Data) khi `Locked = true`,
  trước cả bước so mật khẩu. Nhánh `Locked == true` trong code GetToken là
  phòng thủ thêm, thường không tới được qua CheckPwd hiện tại (xem §5).

### 1.2. Shape request thống nhất

`POST api/Auth/GetToken { Account, Password (md5), TokenType }` (+ `AppId`,
`Timestamp` theo envelope từng API). Bổ sung trường `Password` vào mọi biến thể
`AuthGetTokenRequest`; `TokenType` lấy từ `RequestBase` (trừ EMR — xem §4.6).
Các trường `UserId/OrganizeId/TopOrganizeId` do client gửi (nếu có) **không còn
dùng để dựng identity** — identity lấy từ kết quả CheckPwd.

| API | File request đổi | Trường bổ sung |
|---|---|---|
| MRQC.API | `Newtouch.MRQC.API/Requests/Requests.cs` | `Account`, `Password` |
| OR.API | `Newtouch.HIS.OR/Newtouch.OR.API/Requests/Requests.cs` | `Account`, `Password` |
| MRMS.API | `Newtouch.HIS.MRMS/Newtouch.MRMS.API/Requests/Requests.cs` | `Account`, `Password` |
| HOSP.API | `Newtouch.HIS.Base/Newtouch.HIS.Base.HOSP.API/Requests/AuthGetTokenRequest.cs` | `Password` (đã có `Account`) |
| CIS.API | `Newtouch.HIS.CIS/Newtouch.CIS.API/Requests/Requests.cs` | `Password` (đã có `Account`) |
| CIS Infra (biến thể không được controller dùng) | `Newtouch.HIS.CIS/Newtouch.CIS.Infrastructure/APIRequest/AuthGetTokenRequest.cs` | `Password` (thống nhất shape) |
| EMR.API | `Newtouch.HIS.EMR/Newtouch.EMR.Infrastructure/APIRequest/AuthGetTokenRequest.cs` | `Account`, `Password`, `TokenType` (RequestBase EMR không có TokenType) |
| Herp.API | `Newtouch.HIS.Herp/Newtouch.Herp.API/Requests/ApiRequests.cs` | `Account`, `Password` |
| PDS.API | `Newtouch.HIS.PDS/Newtouch.PDS.Infrastructure/AuthGetTokenRequest.cs` | `Password` (đã có `Account`) |

### 1.3. Cơ chế gọi BaseAPI và lưu token theo từng API

- **HTTP:** tất cả dùng `IHttpClientHelper` đã đăng ký sẵn trong `Program.cs`
  của từng API (Herp dùng `HerpHttpClientHelper` override sẵn — IPv4 thuần).
  Không chế cơ chế HTTP mới. Lỗi gọi/không tới được BaseAPI → fail-closed
  (40004, không cấp token), không 500.
- **TokenStore/SaveIdentity — giữ nguyên cơ chế từng API, chỉ bổ sung chỗ thiếu:**
  - MRQC/OR/MRMS/CIS/PDS: `TokenStore` trước đây CHỈ có `GetIdentity`, chưa có
    `SaveIdentity` → bổ sung `SaveIdentity` (GUID thô làm key Redis, JSON
    identity, TTL 20 phút; `GetIdentity` vốn đã gia hạn 20 phút mỗi lần đọc).
  - Herp: `ITokenStore.SaveIdentity` sẵn có (GUID thô, TTL 20 phút) → dùng
    nguyên; chỉ đổi kiểu lưu thành `UserIdentity` (lớp con có
    `OrganizeId/TopOrganizeId`, vì `Identity` gốc của Herp không có 2 trường
    này; resolver deserialize về `UserIdentity` nên vẫn đọc được org).
  - HOSP: `ApiControllerBase.SaveIdentity` sẵn có → dùng nguyên, **TTL 120 phút**
    (hành vi sẵn có, dùng chung với `GetTokenbyProxy` — KHÔNG đổi thành 20
    phút để khỏi đổi hành vi token của `GetTokenbyProxy`).
  - EMR: `ApiControllerBase.SaveIdentity` sẵn có → dùng nguyên, key
    `EMR:ApiIdentity:{token}`, token dạng `N` (không gạch), **TTL 2 giờ**.
- **Từ chối cấp token khi:** CheckPwd không SUCCESS; `Locked == true`; hoặc
  `TopOrganizeId` từ CheckPwd khác `Top_OrganizeId` cấu hình của API
  (tất cả config demo hiện đều là `6d5752a7-234a-403e-aa1c-df8b45d3469f`;
  nếu API không cấu hình `Top_OrganizeId` thì bỏ qua bước này vì CheckPwd
  vốn đã giới hạn theo `Top_OrganizeId` của BaseAPI).
- **Không đổi:** envelope (`code/sub_code/sub_msg/data`, 10000/40004;
  `jsonpCallback` ở API nào có — qua `CreateResponse(request, response)` như cũ),
  route `api/Auth/GetToken`, các mã `TOKEN_TYPE_IS_REQUIRED /
  TOKEN_TYPE_NOT_MATCH / TIMESTAMP_IS_REQUIRED / TOKEN_IS_REQUIRED /
  TOKEN_IS_EXPIRED / TOKEN_IS_ILLEGAL`, hành vi "Query không cần token vẫn chạy
  khi Timestamp hợp lệ" của action không có `[DefaultAuthorize]`.
- **Ghi chú HTTPS-only (trong code comment của cả 8 AuthController):**
  production CHỈ phơi route này qua HTTPS — reverse proxy chặn HTTP. Việc chặn
  ở proxy thuộc giai đoạn đóng gói sau; giai đoạn này KHÔNG tự dựng TLS.

### 1.4. Lưu ý Timestamp (cho client migration)

Request deserialize bằng System.Text.Json của `[ApiController]`: Timestamp phải
dạng ISO 8601 (`2026-10-27T11:07:41+07:00`); gửi `yyyy-MM-dd HH:mm:ss` sẽ bị
400 validation (hành vi sẵn có, không phải do GetToken). Kiểm chứng trong
giai đoạn này đều dùng ISO.

## 2. Danh sách client hiện đang gọi GetToken (grep toàn cây trước khi bật)

### 2.1. Client gọi `api/Auth/GetToken` THẬT — sẽ phải migration (chưa gửi Password)

| Client (file) | Đích | TokenType gửi | Tình trạng sau giai đoạn này |
|---|---|---|---|
| PDS Infra `SiteBaseAPIHelper.GetTokenFromAPI` | HOSP.API 5333 | `PDS` | Gửi UserId/Account/OrganizeId/TopOrganizeId/AppId/Timestamp, **không Password** → nay nhận 40004, không còn lấy được token. Cần migration. |
| PDS Infra `SiteCisAPIHelper.GetTokenFromApi` | CIS.API 5334 | `PDS` | Như trên → nay 40004. Cần migration. |
| Base Infra `SitePdsAPIHelper.GetTokenFromApi` | PDS.API 5337 | `Base.web` | Như trên → nay 40004. Cần migration. |
| PDS Infra `SiteSettAPIHelper` (`api/Auth/GetToken`) | Sett-API host 7002 (**chưa tồn tại**, giai đoạn 3) | `PDS` | Chưa có host để test; khi làm host 7002 phải dùng shape mới (kèm Password). |

Cả 4 client đều cache token vào Redis (19–20 phút) và chỉ gọi lại khi hết hạn,
nhưng lần gọi lại đầu tiên sau giai đoạn này sẽ thất bại vì thiếu Password.
**Quyết định cần điều phối viên/user chốt (không tự bịa):** các client này lấy
credential từ đâu — tài khoản dịch vụ riêng hay mật khẩu người dùng hiện tại
truyền qua? Giai đoạn này KHÔNG sửa các client đó (ngoài phạm vi: chỉ bật
GetToken phía server).

### 2.2. Client tên GetToken nhưng KHÔNG gọi module GetToken — không ảnh hưởng

- `AuthenManageHelper.GetToken` (MRQC, Sett, CIS, PDS Infra) → AuthCenter 8015
  `api/Auth/GetAppToken` (JWT app-to-app). Không đổi.
- EMR `AuthenHelper.GetToken`, `EmrHelpers.SiteHispatinfoAPIHelper.GetToken`,
  `SiteCISAPIHelper.GetToken` → cũng `GetAppToken` 8015. Không đổi.
- Sett `Helpers.GetToken()` trả `""` (stub). Không đổi.
- `AuthCenter`/`WebAPI.Manage` `GetAppToken`/`UserTokenRefresh`/`GetUserToken`
  là loại JWT/khác, không thuộc phạm vi này.

## 3. Ma trận kiểm chứng §1.4 — phương pháp chung

- Bằng curl thật vào API đang chạy (không chỉ build). Tài khoản kiểm chứng duy
  nhất: `000000` / mật khẩu `000000` (dạng md5 tính bằng `md5sum`, không gõ tay).
- Timestamp gửi dạng ISO 8601. Token trong báo cáo chỉ ghi 8 ký tự đầu.
- **Sai mật khẩu + không ghi Redis:** kiểm chứng 2 lớp — response không có
  token nào, và `DBSIZE` Redis trước/sau không đổi (code path return trước
  `SaveIdentity`).
- **Token hết hạn:** KHÔNG chờ TTL code — mint token test rồi `EXPIRE` key đó
  về 1 giây (chỉ key token test do mình tạo), đợi hết hạn rồi gọi action.
  TTL code vẫn giữ nguyên theo từng API (§1.3). Với EMR, key expire là
  `EMR:ApiIdentity:{token}` (không phải token thô).
- **Token "?" kiểu bản gốc:** GUID ngẫu nhiên không có trong Redis.
- **Tài khoản khóa:** xem §5 (không có tài khoản khóa sẵn).
- **HTTP ở production bị chặn:** CHƯA kiểm chứng được cho tới giai đoạn đóng gói
  (chưa có TLS/reverse proxy) — KHÔNG báo là xong.

## 4. Kết quả từng API (thứ tự làm đúng yêu cầu)

### 4.1. MRQC.API 5320 — XONG, đủ ma trận (trừ ô khóa: code-verified)

- Build: **Release** (`run-mrqc-api.sh`). PID cũ 3261 → **PID mới 6463**.
- File đổi: `Controllers/AuthController.cs` (viết lại GetToken thật),
  `Requests/Requests.cs` (+Account/Password), `Services/TokenStore.cs`
  (+SaveIdentity 20 phút).
- Action Query dùng test: `api/Default/Query` (Sys_Config).

| Ô ma trận | Kết quả thật |
|---|---|
| Đúng credential + TokenType `MRQC` | 10000, token prefix `3e848675` |
| Dùng token gọi Query | 10000, data = [] (bảng `Sys_Config` của DB demo MRQC rỗng thật — đã đối chiếu bằng sqlcmd `COUNT(*) = 0`; token hợp lệ được chứng minh bằng tương phản các ô 5/7/8) |
| Sai mật khẩu | 40004 `AUTH_FAILED`, không token; DBSIZE 24 → 24 |
| Tài khoản khóa | Code-verified, chưa live-test (xem §5) |
| Thiếu TokenType (GetToken) | 40004 `TOKEN_TYPE_IS_REQUIRED` |
| TokenType không khớp (Query) | 40004 `TOKEN_TYPE_NOT_MATCH` |
| Thiếu Timestamp (Query) | 40004 `TIMESTAMP_IS_REQUIRED` |
| Token hết hạn (EXPIRE 1s) | 40004 `TOKEN_IS_EXPIRED` |
| GUID ngẫu nhiên | 40004 `TOKEN_IS_EXPIRED` |
| HTTP production bị chặn | Chưa kiểm chứng được (đợi giai đoạn đóng gói) |

### 4.2. OR.API 5331 — XONG, đủ ma trận (trừ ô khóa: code-verified)

- Build: **Debug** (`run-or-api.sh`). PID cũ 2667 → **PID mới 6898**.
- File đổi: như MRQC (AuthController/Requests/TokenStore).
- Action Query: `api/Default/Query` (Sys_GlobalConfig, data rỗng — cùng lý do
  bảng demo rỗng; token hợp lệ chứng minh bằng tương phản ô 5/7/8).

| Ô ma trận | Kết quả thật |
|---|---|
| Đúng credential + TokenType `OR` | 10000, token prefix `e5fc72a0` |
| Dùng token gọi Query | 10000, data = [] |
| Sai mật khẩu | 40004 `AUTH_FAILED`; DBSIZE 24 → 24 |
| Tài khoản khóa | Code-verified, chưa live-test (§5) |
| Thiếu TokenType | 40004 `TOKEN_TYPE_IS_REQUIRED` |
| TokenType không khớp | 40004 `TOKEN_TYPE_NOT_MATCH` |
| Thiếu Timestamp | 40004 `TIMESTAMP_IS_REQUIRED` |
| Token hết hạn (EXPIRE 1s) | 40004 `TOKEN_IS_EXPIRED` |
| GUID ngẫu nhiên | 40004 `TOKEN_IS_EXPIRED` |
| HTTP production bị chặn | Chưa kiểm chứng được |

### 4.3. MRMS.API 5332 — XONG, đủ ma trận (trừ ô khóa: code-verified)

- Build: **Release** (`run-mrms-api.sh`). PID cũ 3162 → **PID mới 7110**.
- File đổi: như MRQC.
- Action Query: `api/Default/Query` (Sys_Config).

| Ô ma trận | Kết quả thật |
|---|---|
| Đúng credential + TokenType `MRMS` | 10000, token prefix `3add7da2` |
| Dùng token gọi Query | 10000, **data 9 dòng thật** |
| Sai mật khẩu | 40004 `AUTH_FAILED`; DBSIZE 24 → 24 |
| Tài khoản khóa | Code-verified, chưa live-test (§5) |
| Thiếu TokenType | 40004 `TOKEN_TYPE_IS_REQUIRED` |
| TokenType không khớp | 40004 `TOKEN_TYPE_NOT_MATCH` |
| Thiếu Timestamp | 40004 `TIMESTAMP_IS_REQUIRED` |
| Token hết hạn (EXPIRE 1s) | 40004 `TOKEN_IS_EXPIRED` |
| GUID ngẫu nhiên | 40004 `TOKEN_IS_EXPIRED` |
| HTTP production bị chặn | Chưa kiểm chứng được |

### 4.4. HOSP.API 5333 — XONG, đủ ma trận (trừ ô khóa: code-verified)

- Build: **Release** (`run-base-api.sh`). PID cũ 2976 → **PID mới 7410**.
- File đổi: `Controllers/AuthController.cs` (chỉ viết lại `GetToken`;
  **`GetTokenbyProxy` giữ nguyên 100%**), `Requests/AuthGetTokenRequest.cs`
  (+Password). Không đổi TokenStore (SaveIdentity sẵn có).
- Action Query: `api/MedicineSupplier/Query` — request bắt buộc kèm
  `OrganizeId` (do `OrgRequestBase` có `[Required] OrganizeId`, hành vi sẵn có;
  lần test đầu thiếu OrganizeId đã bị 400 validation, đã bổ sung OrganizeId
  demo vào request test).
- TTL token GetToken mới: **120 phút** (SaveIdentity sẵn có của HOSP, §1.3).

| Ô ma trận | Kết quả thật |
|---|---|
| Đúng credential + TokenType `HOSP` | 10000, token prefix `7244e697` |
| Dùng token gọi Query | 10000, **data 3 dòng thật** |
| Sai mật khẩu | 40004 `AUTH_FAILED`; DBSIZE 25 → 25 |
| Tài khoản khóa | Code-verified, chưa live-test (§5) |
| Thiếu TokenType | 40004 `TOKEN_TYPE_IS_REQUIRED` |
| TokenType không khớp | 40004 `TOKEN_TYPE_NOT_MATCH` |
| Thiếu Timestamp | 40004 `TIMESTAMP_IS_REQUIRED` |
| Token hết hạn (EXPIRE 1s) | 40004 `TOKEN_IS_EXPIRED` |
| GUID ngẫu nhiên | 40004 `TOKEN_IS_EXPIRED` |
| HTTP production bị chặn | Chưa kiểm chứng được |
| Hồi quy GetTokenbyProxy | Vẫn trả 40004 `AUTH_FAILED` cho request trống như cũ (không 500, không đổi hành vi) |

### 4.5. CIS.API 5334 — XONG, đủ ma trận (trừ ô khóa: code-verified)

- Build: **Debug** (`run-5334.sh`). PID cũ 3067 → **PID mới 7878**.
- File đổi: AuthController/Requests/TokenStore (+SaveIdentity) + biến thể
  CIS Infra AuthGetTokenRequest (+Password, thống nhất shape).
- Action Query: `api/PatientSeekingInfo/PatientTreatmentInfoQuery` (read-only,
  `mzh = ""`; CIS DefaultController không có action Query).

| Ô ma trận | Kết quả thật |
|---|---|
| Đúng credential + TokenType `CIS` | 10000, token prefix `dcd4e7c7` |
| Dùng token gọi Query | 10000, data = [] (truy vấn `mzh` rỗng) |
| Sai mật khẩu | 40004 `AUTH_FAILED`; DBSIZE 25 → 25 |
| Tài khoản khóa | Code-verified, chưa live-test (§5) |
| Thiếu TokenType | 40004 `TOKEN_TYPE_IS_REQUIRED` |
| TokenType không khớp | 40004 `TOKEN_TYPE_NOT_MATCH` |
| Thiếu Timestamp | 40004 `TIMESTAMP_IS_REQUIRED` |
| Token hết hạn (EXPIRE 1s) | 40004 `TOKEN_IS_EXPIRED` |
| GUID ngẫu nhiên | 40004 `TOKEN_IS_EXPIRED` |
| HTTP production bị chặn | Chưa kiểm chứng được |

### 4.6. EMR.API 5335 — XONG, ma trận đủ theo envelope EMR (2 ô lệch do base sẵn có)

- Build: **Debug** (`run-emr-api.sh`). PID cũ 2835 → **PID mới 8174**.
- File đổi: `Controllers/AuthController.cs` (viết lại GetToken thật, gọi
  `CommonExecute(..., ignoreTokenDecrypt: true)` vì GetToken không cần token cũ),
  `Newtouch.HIS.EMR.Infrastructure/APIRequest/AuthGetTokenRequest.cs`
  (+Account/Password/TokenType). Không đổi TokenStore/base (SaveIdentity sẵn có).
- Action Query: `api/Default/Query` (Sys_Config), token gửi qua **header
  `token`** (cách base EMR đọc identity).
- **Khác biệt EMR giữ nguyên (không bịa kết quả):**
  - Envelope EMR chỉ có `code/msg/data`, không có `sub_code` → lỗi mang mã
    trong `msg` (ví dụ `TOKEN_TYPE_IS_REQUIRED: ...`).
  - Base EMR **không kiểm tra TokenType** khi dùng token → ô "TokenType không
    khớp" trả **10000** (token vẫn resolve). Đây là hành vi sẵn có của base,
    giai đoạn này không đổi validation hiện tại theo yêu cầu.
  - Base EMR **không kiểm tra Timestamp** (Timestamp là `DateTime` không
    nullable nên "thiếu" không phân biệt được) → ô "Thiếu Timestamp" trả
    **10000**. Cũng là hành vi sẵn có, không đổi.
  - `AuthGetTokenRequest` kế thừa `RequestBase` có `[Required] AppId` → GetToken
    thiếu AppId bị 400 validation (đã kiểm chứng live). Client EMR bắt buộc gửi
    AppId.
  - TTL token: **2 giờ**, key `EMR:ApiIdentity:{token}` (quy ước sẵn có).

| Ô ma trận | Kết quả thật |
|---|---|
| Đúng credential + TokenType `EMR` | 10000, token prefix `3e48b530` |
| Dùng token gọi Query (header token) | 10000, **data 6 dòng thật** |
| Sai mật khẩu | 40004, msg "Tên đăng nhập hoặc mật khẩu không đúng..."; DBSIZE 25 → 25 |
| Tài khoản khóa | Code-verified, chưa live-test (§5) |
| Thiếu TokenType (GetToken) | 40004, msg `TOKEN_TYPE_IS_REQUIRED: ...` |
| TokenType không khớp (Query) | **10000 — base EMR không kiểm tra TokenType (lệch có ghi rõ, không đổi)** |
| Thiếu Timestamp (Query) | **10000 — base EMR không kiểm tra Timestamp (lệch có ghi rõ, không đổi)** |
| Token hết hạn (EXPIRE 1s key `EMR:ApiIdentity:{token}`) | 40004, msg "Token không hợp lệ hoặc đã hết hạn" |
| GUID ngẫu nhiên | 40004, msg "Token không hợp lệ hoặc đã hết hạn" |
| HTTP production bị chặn | Chưa kiểm chứng được |

### 4.7. Herp.API 5336 — XONG, đủ ma trận (trừ ô khóa: code-verified)

- Build: **Release** (`run-5336.sh`). PID cũ 2750 → **PID mới 8466**.
- File đổi: `Controllers/AuthController.cs` (viết lại GetToken thật; stub cũ
  trả `AUTH_NOT_IMPLEMENTED`, nay thay bằng GetToken thật),
  `Requests/ApiRequests.cs` (+Account/Password). Không đổi TokenStore
  (SaveIdentity sẵn có, 20 phút).
- Action Query: `api/Default/Query` (Sys_Config Herp).

| Ô ma trận | Kết quả thật |
|---|---|
| Đúng credential + TokenType `Herp` | 10000, token prefix `7e017570` |
| Dùng token gọi Query | 10000, **data 1 dòng thật** |
| Sai mật khẩu | 40004 `AUTH_FAILED`; DBSIZE 25 → 25 |
| Tài khoản khóa | Code-verified, chưa live-test (§5) |
| Thiếu TokenType | 40004 `TOKEN_TYPE_IS_REQUIRED` |
| TokenType không khớp | 40004 `TOKEN_TYPE_NOT_MATCH` |
| Thiếu Timestamp | 40004 `TIMESTAMP_IS_REQUIRED` |
| Token hết hạn (EXPIRE 1s) | 40004 `TOKEN_IS_EXPIRED` |
| GUID ngẫu nhiên | 40004 `TOKEN_IS_EXPIRED` |
| HTTP production bị chặn | Chưa kiểm chứng được |

### 4.8. PDS.API 5337 — XONG, đủ ma trận (trừ ô khóa: code-verified)

- Build: **Release** (`run-pds-api.sh`). PID cũ 3211 → 8762 (bản đầu) →
  **PID mới 8967** (build lại lần cuối chỉ để thêm ghi chú code về pre-check đã
  bỏ; đã smoke-test lại sau restart cuối: GetToken 10000, token prefix
  `232dce96`).
- File đổi: `Controllers/AuthController.cs` (viết lại GetToken thật),
  `Newtouch.PDS.Infrastructure/AuthGetTokenRequest.cs` (+Password),
  `Services/TokenStore.cs` (+SaveIdentity 20 phút).
- **Thay đổi hành vi ghi rõ:** stub cũ kiểm tra khác-rỗng
  UserId/Account/OrganizeId/TopOrganizeId rồi mới trả `GETTOKEN_DISABLED`;
  bản thật BỎ bước đó (identity lấy từ CheckPwd) — request chỉ cần
  Account + Password + TokenType (+ Timestamp). Thiếu Account/Password vẫn →
  `AUTH_FAILED`.
- Action Query: `api/Stock/query` (read-only, `yplist = []`).

| Ô ma trận | Kết quả thật |
|---|---|
| Đúng credential + TokenType `PDS` | 10000, token prefix `1a998cf7` |
| Dùng token gọi Query | 10000, data = null (StockQuery với `yplist` rỗng trả null theo hành vi app; token hợp lệ chứng minh bằng tương phản ô 5/7/8) |
| Sai mật khẩu | 40004 `AUTH_FAILED`; DBSIZE 25 → 25 |
| Tài khoản khóa | Code-verified, chưa live-test (§5) |
| Thiếu TokenType | 40004 `TOKEN_TYPE_IS_REQUIRED` |
| TokenType không khớp | 40004 `TOKEN_TYPE_NOT_MATCH` |
| Thiếu Timestamp | 40004 `TIMESTAMP_IS_REQUIRED` (PDS base: Timestamp == default) |
| Token hết hạn (EXPIRE 1s) | 40004 `TOKEN_IS_EXPIRED` |
| GUID ngẫu nhiên | 40004 `TOKEN_IS_EXPIRED` |
| HTTP production bị chặn | Chưa kiểm chứng được |

## 5. Ô "tài khoản bị khóa" — code-verified, chưa live-test (cả 8 API)

- Đã tra DB (chỉ đọc): `V_C_Sys_User WHERE Locked = 1` → **0 dòng**.
  `000000` (Locked=0), `xzadmin` (NULL), `admin` (0) đều không khóa.
- Theo lệnh: TUYỆT ĐỐI KHÔNG khóa tài khoản demo, không brute-force để kích
  hoạt khóa. Vì vậy nhánh Locked chỉ kiểm chứng bằng soát code:
  - Code GetToken cả 8 API đều có nhánh `user.Locked == true → 40004
    AUTH_FAILED` trước khi `SaveIdentity`, và code path sai credential return
    trước `SaveIdentity` nên không ghi Redis.
  - Nhưng lưu ý trung thực: `UserLogin` của BaseAPI trả FAIL (không Data) cho
    tài khoản khóa **trước** khi GetToken kịp đọc `Locked`, nên thực tế nhánh
    `Locked == true` trong GetToken thường không tới được — tài khoản khóa vẫn
    bị từ chối (40004, không token) qua nhánh `user == null`. Hành vi cuối
    cùng là đúng (fail-closed), chỉ khác đường code.
- Cần live-test thật khi có tài khoản khóa (ví dụ trên staging với tài khoản
  test riêng, không phải tài khoản demo) ở giai đoạn sau.

## 6. Những gì KHÔNG làm / chưa chắc chắn còn lại

1. **Không sửa client GetToken (§2.1):** 3 client thật (PDS→HOSP, PDS→CIS,
   Base→PDS) và 1 client tới host 7002 tương lai hiện gửi request không Password
   → sẽ nhận 40004. Cần quyết định credential cho chúng (tài khoản dịch vụ hay
   mật khẩu user) trước khi cắt chạy.
2. **Không khóa tài khoản sau N lần sai:** `UserLogin` chỉ *đọc* cờ `Locked`,
   không thấy code tăng đếm sai trong đường CheckPwd. Trong suốt giai đoạn này,
   tài khoản `000000` vẫn đăng nhập được sau hàng chục lần gọi sai mật khẩu
   (kiểm chứng gián tiếp qua các lần GetToken đúng sau đó) → **số lần sai qua
   GetToken dường như không tự khóa tài khoản**. Ngưỡng khóa
   `LoginFailedTimesLimit_Count = 3` trong config chưa rõ được kích hoạt ở đâu
   (có thể ở web login hoặc job khác) — cần xác minh khi làm hardening.
3. **EMR không kiểm tra TokenType/Timestamp khi dùng token** (§4.6) — hành vi
   base sẵn có, giữ nguyên theo yêu cầu "không đổi validation hiện tại". Nếu
   production yêu cầu chặt, đó là một task sửa base EMR riêng (ảnh hưởng mọi
   action EMR hiện tại).
4. **TTL không đồng nhất theo API:** mới bổ sung 20 phút (MRQC/OR/MRMS/CIS/PDS);
   Herp 20 phút sẵn có; HOSP 120 phút; EMR 2 giờ. Giữ nguyên theo từng API để
   không đổi hành vi token cũ (đặc biệt GetTokenbyProxy của HOSP). Nếu muốn
   thống nhất 20 phút, phải quyết riêng và kiểm chứng lại GetTokenbyProxy.
5. **Phạm vi tổ chức chỉ kiểm tra TopOrganizeId** (vì CheckPwd không trả
   OrganizeId riêng). Môi trường đa tổ chức con cần kiểm tra sâu hơn khi có
   dữ liệu thật.
6. **HTTP production bị chặn:** chưa kiểm chứng được (đợi giai đoạn đóng gói
   có nginx/TLS). Giai đoạn này không dựng TLS.
7. **md5:** Password vẫn dạng md5 như web login (theo spec §1.3 — an toàn dựa
   vào HTTPS + khóa tài khoản + rate limit; chuyển KDF mạnh là dự án riêng,
   không làm chung).
8. **Không ghi DB demo:** GetToken chỉ đọc CheckPwd + ghi Redis. Token test
   trong Redis là phù du (TTL theo từng API), các token đã dùng test đều đã
   hết hạn bằng EXPIRE, trừ token smoke-test PDS cuối (tự hết hạn sau 20 phút).
9. **Công cụ kiểm chứng:** `redis-cli` kèm theo không chạy được (thiếu
   `liblzf.so.1` trong `LD_LIBRARY_PATH` mặc định) → dùng client RESP tối thiểu
   bằng Python (`/tmp/redis.py`, đọc mật khẩu Redis từ file env lúc chạy, không
   in ra) cho các lệnh PING/DBSIZE/EXPIRE. Không ảnh hưởng stack.

## 7. Diff chưa commit (để điều phối viên soát, KHÔNG commit/push)

22 file, 1351 insertions, 134 deletions:

- MRQC.API: `Controllers/AuthController.cs`, `Requests/Requests.cs`,
  `Services/TokenStore.cs`
- OR.API: `Controllers/AuthController.cs`, `Requests/Requests.cs`,
  `Services/TokenStore.cs`
- MRMS.API: `Controllers/AuthController.cs`, `Requests/Requests.cs`,
  `Services/TokenStore.cs`
- HOSP.API: `Controllers/AuthController.cs` (GetTokenbyProxy nguyên vẹn),
  `Requests/AuthGetTokenRequest.cs`
- CIS.API: `Controllers/AuthController.cs`, `Requests/Requests.cs`,
  `Services/TokenStore.cs`; CIS Infra: `APIRequest/AuthGetTokenRequest.cs`
- EMR.API: `Controllers/AuthController.cs`; EMR Infra:
  `APIRequest/AuthGetTokenRequest.cs`
- Herp.API: `Controllers/AuthController.cs`, `Requests/ApiRequests.cs`
- PDS.API: `Controllers/AuthController.cs`, `Services/TokenStore.cs`;
  PDS Infra: `AuthGetTokenRequest.cs`

Không sửa: checkout `~/workspace/lmsoft_openhis`, run copy
`~/workspace/lmsoft_openhis_run`, mật khẩu SA trong
`HIS.BaseAPI/appsettings.json`, code/token log (không log mật khẩu/token).

## 8. PID mới sau restart (tổng hợp)

| API | Cổng | Build | Launcher | PID cũ → PID mới |
|---|---|---|---|---|
| MRQC.API | 5320 | Release | `run-mrqc-api.sh` | 3261 → 6463 |
| OR.API | 5331 | Debug | `run-or-api.sh` | 2667 → 6898 |
| MRMS.API | 5332 | Release | `run-mrms-api.sh` | 3162 → 7110 |
| HOSP.API | 5333 | Release | `run-base-api.sh` | 2976 → 7410 |
| CIS.API | 5334 | Debug | `Newtouch.HIS.CIS/run-5334.sh` | 3067 → 7878 |
| EMR.API | 5335 | Debug | `run-emr-api.sh` | 2835 → 8174 |
| Herp.API | 5336 | Release | `Newtouch.HIS.Herp/run-5336.sh` | 2750 → 8466 |
| PDS.API | 5337 | Release | `run-pds-api.sh` | 3211 → 8967 |

Quét cuối: 25/25 cổng UP (1433, 6379, 41142, 7001, 5208, 5220, 5221, 5231–5238,
5320, 5331–5337, 8015, 8016). BaseAPI 7001 và SSO 5208 không restart trong giai
đoạn này.
