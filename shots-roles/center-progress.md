# Center progress — AuthCenter 8015 + WebAPI.Manage 8016 (API thuần, không UI)

## Ghi chú về file này

- Không có file progress Center riêng trước đợt này; worker thay thế (2026-10-27)
tạo file này theo đúng quy ước shots-roles/<app>-progress.md, KHÔNG sửa
roles-shots-progress.md ở root (điều phối viên sở hữu).
- Worker Center trước đã errored runtime, KHÔNG để lại báo cáo hay mục progress nào.
Audit dưới đây làm lại đầy đủ từ đầu, không giả định kết quả của worker trước.

## Phần kế thừa từ worker trước (đã kiểm chứng, giữ lại)

- Uncommitted diff Center (264 file vs HEAD bdbf9bf) chủ yếu là sweep dịch
Trung → Việt (comment, Description, message). Kiểm tra bằng diff ASCII-only:
thay đổi chức năng duy nhất là block `<Content Include="DATA\chs_drg_11\...">`
trong NewtouchHIS.WebAPI.Manage.csproj (copy bảng DRG ra output DATA\ khi
rebuild — đúng, giữ lại; DATA\*.dat đã có đủ trong bin).
- Lib.Base.dll tươi (build Release 2026-10-27 05:41 UTC, khớp source hiện tại)
đã nằm trong NewtouchHIS.Lib output nhưng CHƯA được copy vào
his-dll-common/Net6 — worker trước dừng trước bước này (xem mục fix của tôi).

## Parity audit Center 2026-10-27 (worker thay thế)

Đối chiếu `git show V2.2:<path>` trong ~/workspace/lmsoft_openhis với
~/workspace/openhis-core/Newtouch.HIS.Center. Lưu ý: WebAPI.Manage/AuthCenter
ở V2.2 vốn đã là.NET 6 (Microsoft.AspNetCore.Mvc, [Route("api/[controller]")]),
nên bản chuyển đổi là copy trung thực — giải thích parity tuyệt đối dưới đây.

- FILE: 338/338 file.cs gốc đều có trong bản chuyển đổi, 0 file thiếu,
0 file ported-only.
- API: 34 controller, 130/130 action, 0 lệch route/verb/template
(so cả attribute [HttpGet/HttpPost/Route] từng action). AuthCenter: Auth
(GetAppToken, GetAppFrienAuthToken, UserTokenRefresh, GetUserToken) +
Login (UserLogin). Không có [Area] nhưng route tuyệt đối api/[controller]
nên không cần. GetOpenId: KHÔNG có trong cây Center gốc lẫn bản chuyển đổi
(chuỗi "GetOpenId" chỉ xuất hiện ở LoginController các web module, nơi gọi
sang SSO API `api/Auth/GetOpenId` — đó là endpoint phía SSO, không phải Center).
- FIELD: 2600/2600 property (Models/Entity/BO: 2144; ValueObjects/Model: 456),
0 thiếu, 0 thừa. Service-layer (DomainService/Repository/Services):
0 lệch public method.
- MÀN HÌNH: N/A — Center là API thuần, không view. Thay bằng kiểm tra
route GET: không có action GET nào ngoài các route POST chuẩn; root /
trả 404 là đúng (không có default route).
- Envelope: BusResult code 10000 (SUCCESS) / 40004 (FAIL) / 20000
(validation/exception) giữ nguyên; token: GetAppToken cấp JWT thật,
GetToken các module API vẫn stub 40004 GETTOKEN_DISABLED (đã kiểm chứng
lại ở MRQC.API 5320, không bật lại).

## Fix duy nhất trong đợt này (worker thay thế)

- Vấn đề: bin Center đang chạy dùng NewtouchHIS.Lib.Base.dll cũ
(2026-10-26, 131584 byte) qua HintPath tới his-dll-common/Net6, nên
message validation trả về tiếng Trung "应用Id（AppId）必传" dù source Lib
đã dịch sang "Id ứng dụng (AppId) bắt buộc phải truyền".
- Fix: copy Lib.Base.dll tươi (133120 byte, build Release khớp source hiện tại)
vào openhis-core/his-dll-common/Net6/ (backup DLL cũ ở /tmp, file này
tracked nên hiện M trong git status — KHÔNG commit), rebuild Release
cả 2 host (0 error), kill đúng PID cũ (3912, 30436), khởi động lại bằng
run-authcenter.sh / run-apimanage.sh (PID mới 35819/35855, 2026-10-27 06:28 UTC).
- Sau fix: message AppId-missing đã tiếng Việt; message ngoại lệ proxy đã
"[Ngoại lệ]:" thay cho ":". Không sửa source Lib, không động DLL
app khác (thư mục Net6 này theo ghi nhận trước chỉ Center dùng).

## Kiểm chứng live sau fix (không ghi DB)

- GetAppToken MRQC/CIS/EMR (Data=API.Manage) → 10000 + JWT thật;
unknown app → 40004 tiếng Việt.
- 8016 không token → HTTP 200, envelope HttpStatus 401
"Xác thực token thất bại:".
- MedicalCenterPatInfo (token EMR, OrganizeId demo) → 10000 + 1 dòng bệnh
nhân thật (tên/Xm, tuổi dạng "25岁" là DỮ LIỆU DB tiếng Trung — ngoài
phạm vi code, báo riêng). Thiếu OrganizeId → 40004 tiếng Việt.
- NoticeQuery (token MRQC, DB rỗng) → 40004 "Không tìm thấy dữ liệu khả dụng".
- DoctorMeetingApply applyId giả (token CIS) → 40004 tiếng Việt, dừng ở
validation, không ghi.
- DRG: null Data → 40004 tiếng Việt; bản ghi dummy (không phải bệnh nhân
thật, chỉ tính toán, không ghi DB) → 10000, mdc=MDCH, adrg=HU1, drg=HU15,
status=分组成功 (status là chuỗi gốc của binary engine, không phải code ta).
- Sett-area proxy: OutpatientRegist validation (null Data) → 40004 tiếng Việt;
dummy kh+ScheduId → 20000 "[Ngoại lệ]:Connection refused (localhost:17361)".
BillOrderPay đủ field dummy → 20000 connection refused (localhost:17361).
Nguyên nhân: launcher chạy ASPNETCORE_ENVIRONMENT=Development nên
appsettings.Development.json (gốc V2.2) override SiteSettAPIHost thành
localhost:17361 (appsettings.json gốc là localhost:7002) — cả hai đều
không tồn tại. Đây là hành vi trung thực của bản gốc, KHÔNG bịa host mới.
- Log AuthCenter vẫn chứa dòng JWT (62 dòng khớp eyJ, chỉ đếm, không trích):
hành vi built-in, không sửa trong đợt này theo yêu cầu.
- Framework DataAnnotations không custom ErrorMessage vẫn tiếng Anh
(vd "The zyh field is required.") — residual đã biết, ngoài phạm vi Center.
- Không có ảnh UI: Center là API thuần, bằng chứng là response live trên.
- Không commit, không push, không ghi DB trong đợt này.
