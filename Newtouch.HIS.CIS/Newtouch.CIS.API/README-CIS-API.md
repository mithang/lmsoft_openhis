# NewtouchCIS.API — bản port .NET 6 (port 5334)

Port của `NewtouchCIS.API` gốc (Framework, 13 controller) sang .NET 6, theo đúng
tiền lệ OR.API: envelope `code/sub_code/sub_msg/data`, SUCCESS=10000 / ERROR=20000 /
FAIL=40004, `Timestamp` bắt buộc, chấp nhận cả `yyyy-MM-dd HH:mm:ss` lẫn ISO 8601
(Newtonsoft + suppress model-state filter, như PDS.API — F-API-02; Timestamp sai hẳn
trả envelope, không trả HTTP 400 trần), JSON Newtonsoft indent + `yyyy-MM-dd HH:mm:ss`,
hỗ trợ `jsonpCallback` (request JSONRequestBase).

## Chạy

- Launcher: `../run-5334.sh` (proxy env UNSET, `DOTNET_SYSTEM_NET_DISABLEIPV6=true`,
  `ASPNETCORE_URLS=http://127.0.0.1:5334`, `BusinessDB__DefaultConn` lấy từ
  `~/workspace/.sqlserver.env`, dùng chung `appsettings.json` của CIS Web với
  `AddEnvironmentVariables()` đặt CUỐI — bài học OR.API: không thì file sẽ lấn env).
- Build: `dotnet build Newtouch.CIS.API.csproj -c Debug` — 0 errors.
- DI: copy nguyên 146 dòng `AddScoped` của CIS Web (cùng Infrastructure project)
  để mọi ctor dependency đều resolve như Web. `TokenStore` singleton, CORS AllowAll.
- Recovery: `~/workspace/recovery.sh` section 20 (guarded, chỉ start khi 5334 down
  và process vắng). Watchdog cron chưa cập nhật 5334 — parent sắp xếp.

## Ma trận controller (13/13, không 404)

| Route | Trạng thái | Ghi chú |
|---|---|---|
| `api/Auth/GetToken` | STUB `GETTOKEN_DISABLED` | Bản gốc cấp token không kiểm tra credential (chỉ có comment "khi cần thì bổ sung kiểm tra", identity lấy thẳng từ request). KHÔNG BAO GIỜ trả token. Mở lại cần thiết kế kiểm tra credential thật. |
| `api/CQkrlis/GetLisReport` | STUB `LIS_NOT_AVAILABLE` | Bản gốc dùng OLEDB + DLL native tới máy chủ LIS ngoài (conn string hardcode KHÔNG copy sang port). Linux không có OLEDB/native tương đương, máy chủ LIS vắng. |
| `api/cmmtests/TCM_HIS_01` | CHẠY THẬT (JSON thô, không envelope) | Trung thực với gốc: action gốc trả JsonResult trực tiếp, không kiểm tra token. Sửa bug gốc: field app không được gán → inject DI. Overload TcmHis01Request của gốc đã bị comment-out nên không port. |
| `api/Default` (GET/POST/PUT/DELETE) | STUB `NOT_IMPLEMENTED` | Scaffold mẫu template, không nghiệp vụ (tiền lệ OR.API). |
| `api/Hospitalized/UpdateApplyNoStatus`, `UpdateReturnApplyNoStatus` | CHẠY THẬT (ghi) | Sửa bug gốc: service không được gán → inject DI. Thông điệp lỗi dịch sang tiếng Việt (msg gốc → sub_msg). |
| `api/InsideGeneral/MqGeneralTask` | CHẠY THẬT | Sửa bug gốc: app không được gán → inject DI. Chỉ dùng `body` (map sang DTO Infrastructure). Không gắn [DefaultAuthorize] như gốc. |
| `api/MzBespeakRegister/*` (4 action) | CHẠY THẬT | Sửa bug gốc: cả 3 field không được gán → inject DI. 3 tra cứu đọc được; KeepAnAppointment ghi. Không gắn [DefaultAuthorize] như gốc nhưng action đọc UserIdentity — gọi không token rơi về envelope lỗi thay vì NullReference 500 như gốc. |
| `api/PatInfo/*` (3 action) | CHẠY THẬT | [DefaultAuthorize]. Sửa bug gốc: cả 2 field không được gán → inject DI. 2 action ghi, 1 đọc. |
| `api/PatientSeekingInfo/PatientTreatmentInfoQuery` | CHẠY THẬT (đọc) | Sửa bug gốc: service không được gán → inject DI. Lưu ý route gốc là PatientSeekingInfo, không phải PatientTreatmentInfo. |
| `api/Prescription/*` (3 action) | CHẠY THẬT (ghi) | Sửa bug gốc: repo không được gán → inject DI. UpdateChargeStatusApi giữ nguyên semantics [IgnoreTokenDecrypt] + null Token/TokenType + dùng OrganizeId/AppId từ request. |
| `api/Review/GetBaShData` | CHẠY THẬT | [DefaultAuthorize]. Sửa bug gốc: service không được gán → inject DI. Tên method gốc GetBashData giữ nguyên. |
| `api/Tests` | SCAFFOLD GIỮ NGUYÊN | Controller gốc kế thừa ApiController thuần (không envelope) → giữ value1/value2 (tiền lệ HOSP.API). |
| `api/UA/GetUserInfo` | CHẠY THẬT | Token thô tra Redis + gia hạn 120 phút (gốc [IgnoreTokenDecrypt]) — không bị ảnh hưởng bởi giới hạn DES. |

## Giới hạn đã biết (ghi rõ, không giấu)

- **Token mã hóa DES**: thuật toán TokenDecrypt nằm trong DLL API.Common, không có
  source — port dùng token thô làm key Redis (như OR.API). Client gửi token đã mã
  hóa DES sẽ nhận `TOKEN_IS_EXPIRED`. UA GetUserInfo không bị ảnh hưởng.
- **Token 20 phút**: resolve thành công gia hạn 20 phút (DefaultAuthorize); UA 120 phút.
- **Bảng demo thiếu**: `Newtouch_CIS.dbo.zy_bqksby` không có trong DB demo V1.8 —
  `HospitalizedBusiness.UpdateApplyNoStatus` chạm SELECT tới bảng này và nhận lỗi
  SQL (envelope `INTERNAL_SERVER_ERROR`), không phải lỗi port (Framework gốc chạy
  cùng câu query cũng sẽ lỗi). Chưa có UPDATE nào chạy tới bước ghi.
- **Thông điệp Infra còn tiếng Trung**: `InsideGeneralApp.PublicPortal` ném
  `FailedException` của Lib (khác type với FailedException của envelope) nên rơi
  về catch chung → `sub_msg` giữ nguyên tiếng Trung của impl (ví dụ "报文body不能为空").
  Thuộc về đợt dịch Infra sau, không sửa bằng cách bịa sub_code.
- **Đường ghi chưa live-test**: mọi action ghi (HospitalizedBusiness x2, PatInfo x2,
  MzBespeakRegister KeepAnAppointment, Prescription x3) biên dịch + wiring đã kiểm
  chứng bằng trường hợp không ghi (cfList null → SUCCESS, không chạy UPDATE), nhưng
  đường ghi thật CHƯA chạy thử theo chính sách không ghi DB.
- **Chưa live-test nghiệp vụ**: `CmmTests.TCM_HIS_01` (có thể ghi bản ghi YHCT) chỉ
  kiểm chứng route bằng JSON sai định dạng → 400 (không chạy nghiệp vụ);
  `Review.GetBaShData` (gọi dịch vụ duyệt ngoài) chỉ kiểm chứng envelope/token qua
  controller cùng [DefaultAuthorize], không gọi nghiệp vụ.
- **Request DTO riêng**: DTO gốc trong Infrastructure kế thừa RequestBase RỖNG
  (Compat/BinaryStubs.cs) nên không mang được Token/Timestamp — API định nghĩa lại
  DTO trong `Requests/Requests.cs` (nguyên trường, comment tiếng Việt). Không sửa
  DTO dùng chung để tránh ảnh hưởng Web.

## Kiểm chứng live (2026-10-26, PID 66173)

- `GET api/Tests` → 200 `["value1","value2"]`.
- `POST api/Auth/GetToken` → 40004 `GETTOKEN_DISABLED` (không token nào được cấp).
- Thiếu `Timestamp` → 40004 `TIMESTAMP_IS_REQUIRED`.
- Không token trên controller [DefaultAuthorize] → `TOKEN_IS_REQUIRED`; token giả → `TOKEN_IS_EXPIRED`.
- Token Redis tạm (tạo rồi XÓA, EXISTS 0 sau kiểm chứng) → `InpatientBaseInfo` 10000
  data null (zyh không tồn tại — đúng rỗng), `UA GetUserInfo` 10000 Account 000000 +
  OrganizeId thật, `MzBespeakRegisterSchedulingQuery` 10000 data [], `MzBespeakRegisterQuery`
  10000 data [], `Prescription UpdateChargeStatus/UpdateChargeTbz` với cfList null →
  10000 (không ghi nào chạy), `PatientTreatmentInfoQuery` không token → 10000 data [].
- `CQKRLIs` → 40004 `LIS_NOT_AVAILABLE`; `Default GET` → 40004 `NOT_IMPLEMENTED`;
  `InsideGeneral` body rỗng → 20000 `INTERNAL_SERVER_ERROR` (lỗi impl, xem trên);
  `CmmTests` JSON sai → HTTP 400 (route tồn tại).
