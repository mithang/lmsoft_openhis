# PDS.API — .NET 6 port (port 5337)

Port của Newtouch.PDS.API (Framework Web API 2) sang ASP.NET Core 6.0, theo đúng
precedent OR.API / CIS.API / MRMS.API. Thêm vào Newtouch.HIS.PDS.sln; dùng chung
SqlSugar infra + DI registrations của PDS Web (không tự bịa data layer mới).

## Chạy

```bash
~/workspace/openhis-core/run-pds-api.sh   # Release, 127.0.0.1:5337, log pds-api.log
```

Launcher dùng chung appsettings.json của PDS Web (BusinessDB/RedisConfig), override
BusinessDB DefaultConn bằng env từ ~/.sqlserver.env (không ghi password vào file).
Khôi phục sau restart: recovery.sh section 23 (watchdog entry vẫn nợ parent).

## Envelope (tái tạo từ mirror source trong chính cây PDS)

- `App_Start/ApiControllerBase.cs` của PDS gốc là bản mirror đầy đủ của
  FrameworkBase.API.ApiControllerBase — envelope ở đây bám sát nó:
  code/sub_code/sub_msg/data (+ msg), SUCCESS=10000 / ERROR=20000 / FAIL=40004,
  Newtonsoft indent + yyyy-MM-dd HH:mm:ss.
- Token: key Redis thô, value JSON identity, gia hạn 20 phút (ApiControllerBaseEx).
  Token mã hóa DES không resolve được (thuật toán TokenDecryptFilter nằm trong DLL
  API.Common, không có source) → TOKEN_IS_EXPIRED. Giới hạn đã biết, giống mọi API
  anh em; không tự bịa thuật toán giải mã.
- Không controller/action nào của PDS mang [DefaultAuthorize] (đã grep toàn bộ cây
  API gốc) nên request không token vẫn qua Authorize khi có Timestamp — đúng hành
  vi gốc, giống Query của OR.API/MRMS.API. Token khi được gửi thì vẫn bị kiểm tra
  đầy đủ (TOKEN_TYPE_IS_REQUIRED / TOKEN_IS_EXPIRED / TOKEN_TYPE_NOT_MATCH).
- Thích ứng với RequestBase shim đã port trong PDS.Infrastructure (Timestamp là
  DateTime không nullable → thiếu Timestamp deserialize thành MinValue → kiểm tra
  `Timestamp == default`; shim không có Validate() nên base tự kiểm tra [Required]
  bằng reflection; shim không có jsonpCallback và mọi controller gốc của PDS đều
  gọi overload CreateResponse một tham số nên không có jsonp thực tế).
- [ApiController] mặc định chặn [Required] bằng HTTP 400 — đã tắt
  (SuppressModelStateInvalidFilter) để CommonExecute trả {FIELD}_IS_REQUIRED đúng
  như gốc; body null cũng nhờ vậy mà trả SOMEARGUMENTS_IS_REQUIRED thay vì 400.
- Formatter input là Newtonsoft (AddNewtonsoftJson), như formatter gốc: DTO
  SysPharmacyDepartmentRequestDto khai báo lại `organizeId` khác case với
  `OrganizeId` của base làm System.Text.Json ném HTTP 500; Newtonsoft không lỗi.
- sub_msg lỗi hệ thống chỉ chứa Exception.Message, không kèm StackTrace (gốc kèm
  cả stacktrace — rò rỉ nội bộ).
- ResponseBase có thêm trường `msg` vì StockController gốc gán resp.msg
  (PrepareMedicine, WithdrawalPreparation...) — các API anh em không có trường này.

## Endpoints (tất cả đã port)

| Route | Ghi chú |
|---|---|
| POST api/Auth/GetToken | STUB: giữ bước kiểm tra khác-rỗng của gốc (thiếu → AUTH_FAILED) rồi luôn trả GETTOKEN_DISABLED. Gốc chỉ kiểm tra khác-rỗng rồi cấp token với comment "giả sử xác thực đã thành công" — không credential thật. Không bao giờ trả token; muốn mở lại phải gắn kiểm tra credential thật (quyết định của chủ hệ thống). |
| POST api/ClinicTfOperate/UpdateCfByTf | `new ClinicTfOperateApp(par).Process()` như gốc; field resolve qua ServiceLocator (AppBase ctor). |
| POST api/DeptPharmacyDrugs/GetDeptPharmacyDrugs | Giữ nguyên chuỗi hard-code của gốc (không tự bịa nghiệp vụ). |
| POST api/Outpatient/SupplementPatientBaseInfo | SỬA BUG GỐC: field không được gán → DI IPatientBaseInfoApp. |
| POST api/Zyypyz/yzzx | SỬA BUG GỐC: field không được gán → DI IResourcesOperateApp. |
| POST api/ResourcesOperate/* (9 actions) | SỬA BUG GỐC: field không được gán → DI IResourcesOperateApp. OutpatientPartReturnBeforeDispensingMedicine giữ [IgnoreTokenDecrypt] như gốc (no-op vì mọi token đều dùng thô). |
| POST api/PharmacyDepartment/* (3 actions) | SỬA BUG GỐC: cả hai field không được gán → DI dmnservice + app. |
| POST api/Stock/query, PrepareMedicine, WithdrawalPreparation, PrepareMedicineReturn, WithdrawalPreparationReturn | SỬA BUG GỐC: field không được gán → DI IDrugStorageApp. [HandlerAuthorizeIgnore] của gốc là attribute MVC, không tác dụng trong pipeline Web API (ghi chú, không bỏ qua auth). PrepareMedicine... gán resp.msg như gốc. |
| GET api/test/TestMultithreadTrans, TestEFDbTransaction | Nối dây qua lớp cụ thể (interface port sẵn chỉ khai báo testEFDbTransaction). KHÔNG gọi thử live (transaction + dữ liệu thử nghiệm — write-path). |
| POST api/test/TestToken | Trả SUCCESS vô điều kiện, KHÔNG qua CommonExecute — đúng gốc. |
| api/values (GET/POST/PUT/DELETE) | Scaffold template → NOT_IMPLEMENTED (quy ước MRMS.API). |
| GET / | Chuỗi trạng thái (host API không có view HelpPage). |
| POST api/Hospitalization/ZYTYInterface | KHÔNG envelope, KHÔNG auth, KHÔNG [ApiController] (để body null trả ActResult ValidationFailure như gốc). SQL gốc qua SqlSugar Ado, đúng tên bảng 3 phần. Thông điệp lỗi dịch sang tiếng Việt, logic/mã giữ nguyên. QUIRK GỐC GIỮ NGUYÊN: ResultCode luôn = AllFailure kể cả khi mọi dòng thành công. KHÔNG gọi thử live nhánh thành công (chạy UPDATE thật). |

## Kiểm chứng live (2026-10-27, temp Redis token tạo rồi DEL, EXISTS 0 sau mỗi lần)

- GetToken: thiếu Timestamp → TIMESTAMP_IS_REQUIRED; thiếu trường → AUTH_FAILED;
  đủ trường → GETTOKEN_DISABLED; body null → SOMEARGUMENTS_IS_REQUIRED.
- DeptPharmacyDrugs: thiếu Timestamp → TIMESTAMP_IS_REQUIRED; thiếu DeptCode →
  DEPTCODE_IS_REQUIRED; không token → 10000 + chuỗi gốc; token giả →
  TOKEN_IS_EXPIRED; token không type → TOKEN_TYPE_IS_REQUIRED; token thật
  (TokenType PDS) → 10000; token thật sai type → TOKEN_TYPE_NOT_MATCH.
- Stock/query: không token và có token → 10000 (đọc tồn kho, yplist rỗng → data null).
- PharmacyDepartment query: ypId thật 822 → 10000 + dòng yfbm thật (tên yfbm
  tiếng Trung do DB trả về — thuộc nhóm DB-sourced, không dịch ở code);
  ypId không tồn tại → 10000 []; ypId "x" → lỗi chuyển đổi int (đầu vào sai, gốc
  cũng lỗi tương tự).
- Hospitalization: body [] → ValidationFailure (tiếng Việt); thiếu yzId → Error1;
  zxId giả → Error3 (chỉ SELECT, không UPDATE nào chạy).
- Write-path (ResourcesOperate, Outpatient, Zyypyz, Empowerment*, Stock
  Prepare/Withdrawal, ClinicTfOperate): chỉ kiểm chứng tới cổng validation
  (TIMESTAMP_IS_REQUIRED / {FIELD}_IS_REQUIRED) — KHÔNG thực thi nghiệp vụ ghi
  trên DB demo theo chính sách no-write.
- PDS Web 5237 login vẫn 200 sau khi port (không bị phá).
- Hai bug gặp lúc port: (1) Program.cs bản đầu thiếu AddWebServiceBase (SysConfig
  không được init → OperatorProvider cctor NRE) và thiếu AddMemoryCache (ICache
  cần IMemoryCache) — đã sửa, lấy đúng thứ tự của PDS Web. (2) Launcher bản đầu
  ghi nhầm password thành chuỗi placeholder — đã sửa thành $MSSQL_SA_PASSWORD.
