# SWEEP-A — Báo cáo kiểm chứng cuối: SSO + MRQC + Center

- Worker: SWEEP-A (SSO portal + MRQC Web/API + Center AuthCenter/WebAPI.Manage).
- Base: V2.2 HEAD `e66c7bda`, prod đã redeploy 23/23 app (theo điều phối viên).
- Thời gian sweep: 2026-10-28 (ICT), từ ~07:30 đến ~09:05.
- Nguyên tắc tuân thủ: prod CHỈ đọc / kiểm tra validation (không ghi DB demo dưới mọi
  hình thức); mọi kiểm thử ghi chạy qua app STAGING; proxy env UNSET;
  `DOTNET_SYSTEM_NET_DISABLEIPV6=true`; kill đúng PID; mật khẩu SA giữ nguyên;
  KHÔNG sửa code, KHÔNG commit (đợt sweep chỉ kiểm chứng).
- Tài khoản kiểm chứng duy nhất: `000000` / `000000` (mật khẩu demo đã công khai,
  theo bổ sung của điều phối viên). Khi POST trực tiếp, password là md5 của `000000`
  tính bằng `md5sum`, không gõ tay hash. Không thử bất kỳ mật khẩu nào khác.
- KHÔNG sửa `roles-shots-progress.md` (điều phối viên sở hữu). File này là báo cáo
  riêng của worker A.

## 0. Tóm tắt kết quả

| Hạng mục | Kết quả |
|---|---|
| SSO prod (5208) | Login OK (sau khi mở khóa), 7 trang 200, endpoint lưới/cây/vai trò/app-list OK, DashBoard redirect OK, no-cookie 302 OK. Lỗi mới: Notice inbox (msgtype=1) lỗi SqlFunc.IIF (xem A-01). Footer còn tiếng Trung (A-03). |
| SSO staging (15208) | SyncForm bước 2 submit THÀNH CÔNG, nhưng phát hiện lỗi liên kết cây menu (A-02). Đã dọn sạch. |
| MRQC Web prod (5220) | Login OK, 9 trang 200, 0 chuỗi Trung nhìn thấy trên mọi trang (HTML). Endpoint đọc OK. Lưới MedicalCenter: endpoint trả 1 dòng demo nhưng trang mở lên trống (A-06 chưa kết luận). |
| MRQC API prod (5320) | Fail-closed OK: no-token → TOKEN_IS_REQUIRED; GetToken tài khoản không tồn tại → AUTH_FAILED; thiếu Timestamp → TIMESTAMP_IS_REQUIRED trước cổng token. |
| MRQC staging (15220) | Chu trình ghi SysItem tạo/đọc/xóa SẠCH; SaveScoreDate tạo/xóa SẠCH. MedicalCenter cần Center-stg mới chạy (xem §6). |
| Center prod (8015/8016) | GetAppToken không còn echo secret (fix CTR-01 đã live); UserLogin validation OK; các route kiểm tra no-token → 401; SysNotice PushMsg/getmsg → 401. |
| Center staging (18015/18016) | NoticeJob: cổng Data HOẠT ĐỘNG (thiếu field → 40004), nhưng đổi trạng thái KHÔNG JWT vẫn thành công (CTR-05 còn mở, xem A-10). UserLogin cấp JWT thật + sai mật khẩu → 40004. Đã dọn sạch. |
| Mobile 390×844 (emulation, KHÔNG app native) | SSO-07: carousel vẫn chồng lấn, sidebar chỉ còn dải icon, bảng WorkSpace bị cắt (A-04). MRQC-05: form lọc sạch, lưới trống khi mở, tràn ngang 15px (A-05). Ảnh đã chụp (xem §7). |

## 1. Phương pháp và môi trường (những gì đã ảnh hưởng đến sweep)

1. **Tài khoản demo prod bị khóa (đầu sweep).** Lúc ~07:37, login prod báo
   “Nhập sai mật khẩu liên tiếp quá 3 lần, tài khoản đã bị khóa”; SQL chỉ-đọc xác
   nhận `NewtouchHIS_Base.V_C_Sys_User` Account `000000` có `Locked=1, zt=1`
   (trong khi `NewtouchHIS_Base_STG` là `Locked=0`). Mọi lần thử của worker A đều
   chỉ dùng đúng `000000`/`000000`, không đoán mật khẩu. Điều phối viên sau đó xác
   nhận 1 worker khác đã đoán mật khẩu trước khi nhận bổ sung, đã mở khóa bằng SQL
   và kiểm chứng login prod thành công trở lại. Worker A kiểm chứng lại: `Locked=0`
   và login prod thành công (CheckLogin 200 → `/Home/Index`).
2. **Các lần login prod báo thất bại chung sau đó KHÔNG phải khóa.** Thông điệp
   “Đăng nhập thất bại, vui lòng liên hệ quản trị viên” (không kèm “khóa”/“sai mật
   khẩu”) xuất hiện đúng các cửa sổ SQL Server chết (listener 1433 biến mất, xem
   mục 3). Khi SQL hồi, cùng 1 tài khoản/mật khẩu login thành công ngay. Theo quy
   tắc sắt, worker A dừng đường login trình duyệt khi gặp thất bại, chẩn đoán bằng
   curl/kiểm tra backend, không thử dồn dập.
3. **SQL Server chết 3 lần trong sweep** (khoảng 07:49, 08:07, 08:41; load avg lên
   tới ~21, RAM trống có lúc ~200MB). Lần 1 tự hồi (hoặc worker khác/watchdog hồi)
   trước khi worker A can thiệp. Lần 2 và 3, worker A chỉ chạy đúng lệnh khởi động
   `sqlservr` ở §2 của `recovery.sh` (chỉ SQL, không restart app nào, không chạm
   dữ liệu); SQL lên lại sau ~5–50 giây và login thành công trở lại. Đây là vấn đề
   hạ tầng dùng chung, không phải lỗi app, nhưng nó làm gián đoạn nhiều lượt kiểm
   chứng (ghi rõ ở từng mục bị ảnh hưởng).
4. **Trình duyệt Firefox trên VM không ổn định khi tải cao.** Tab crash
   (“Page crashed”), goto timeout 60s, thậm chí Firefox chết ngay khi launch
   (`TargetClosedError`) trong các khung giờ tải cao. Vì vậy phần lớn kiểm chứng
   trang prod/staging được thực hiện bằng phiên curl có cookie (login thật qua
   CheckLogin) + endpoint JSON — nhanh và ổn định — còn trình duyệt chỉ dùng cho
   những gì bắt buộc phải render (SyncForm cây, ảnh mobile, lưới MedicalCenter).
   Những gì chỉ kiểm chứng bằng curl được ghi rõ là “qua curl”, không tuyên bố là
   đã xem bằng trình duyệt.
5. **Ngoại lệ giới hạn 2 app staging.** Bình thường worker A chỉ chạy tối đa 2 app
   staging mới cùng lúc (và đã kill đúng PID sau mỗi lượt). Có MỘT ngoại lệ ghi rõ:
   để kiểm chứng đường MedicalCenter của MRQC-stg + UserLogin của Center-stg, worker
   A đã chạy đồng thời 3 app staging mới (AuthCenter-stg 18015 PID 51182,
   Manage-stg 18016 PID 51183, MRQC-stg 15220 PID 51184) vì đường MedicalCenter của
   MRQC-stg phụ thuộc cứng vào cả 18015 (GetPatList) lẫn 18016 (SendMsg) — xem §6.
   Cả 3 đã kill đúng PID ngay sau lượt kiểm chứng, cổng đã xuống, RAM giải phóng.
   SSO-stg 15208, BaseAPI-stg 17001, Redis-stg 6380 là cụm dùng chung có sẵn từ
   trước, worker A KHÔNG kill (không phải hạ tầng của mình).
6. **Staging binary SSO/MRQC** chạy từ worktree `~/workspace/openhis-stg` (theo
   launcher `run-sso-stg.sh` / `run-mrqc-stg.sh`), còn Center-stg chạy từ cây
   `openhis-core` với env override STG. Nội dung kiểm chứng trên staging vì vậy là
   kiểm chứng code đã sửa ở phase 2 (đã commit `e66c7bda`), không phải build trực
   tiếp từ HEAD trong lượt sweep (đợt sweep cấm sửa/build lại).
7. **Lần thử mật khẩu sai duy nhất** trong sweep là 1 lần gọi UserLogin staging
   18015 với mật khẩu sai có chủ đích (để kiểm chứng fail-closed). Đó là tài khoản
   STG, sau đó kiểm chứng lại `Locked=0` trên STG. Không có lần thử sai nào trên
   prod.

## 2. Danh mục chức năng (từ controller source + menu/view)

### 2.1 SSO portal (5208 prod / 15208 staging)

| Nhóm chức năng | Controller / Action | Kiểm thử | Viewport | Kết quả |
|---|---|---|---|---|
| Đăng nhập / phiên | `LoginController` (portal) → BaseAPI CheckPwd | Browser + curl CheckLogin | Desktop, mobile 390 | OK (prod + staging). No-cookie → 302 về `/Login/Index` (prod + staging). |
| Trang chủ portal | `HomeController.Index` | Curl 200 + quét CJK; browser mobile | Desktop (curl), mobile 390 | 200. Chỉ còn footer tiếng Trung (A-03). Mobile: sidebar dải icon, tab cắt (A-04). |
| WorkSpace / bàn làm việc | `HomeController.WorkSpace` | Curl 200 + quét CJK; browser mobile | Desktop (curl), mobile 390 | 200, 0 CJK. Mobile: carousel chồng lấn, bảng cắt (A-04). |
| DashBoard | `HomeController.DashBoard` | Curl + browser staging | Desktop | Redirect 302 → `/Home` (prod + staging) — fix SSO-05 còn hiệu lực. |
| Quản lý menu (lưới/cây) | `SysModuleController` + `SysModuleBaseController`: `GetTreeGridJson`, `GetBsTreeSelectJson`, `GetTreeSelectJson`, `GetAppMenuTreeGridJson`, `GetFormJson`, `SubmitForm`, `DeleteForm`, `GetPermissionTree` | Curl endpoint (UI-faithful params) + trang Index 200 | Desktop (curl) | Lưới/cây trả đủ dữ liệu, `id` có trong tree JSON (fix SSO-03 phía server còn hiệu lực). `SubmitForm`/`DeleteForm` (thêm/sửa/xóa menu trực tiếp): KHÔNG thử (xem §8). |
| Đồng bộ menu (SyncForm) | `SysModuleController.SyncForm` + `SubmitMenuSync` | Bước 1: trang 200 + grid endpoint; Bước 2: submit thật trên STG + đối chiếu DB + dọn sạch | Desktop (curl + endpoint) | Bước 2 success nhưng LỖI LIÊN KẾT (A-02). Browser bước 1/2 sập do tải (xem §8) — bước 2 được kiểm chứng bằng đúng endpoint + payload của UI, ghi rõ phương pháp. |
| Vai trò | `SysRoleController`: `GetGridJson`, `GetFormJson`, `SubmitForm`, `submitRoleUser`, `DeleteForm`, `GetRoleList` | Trang Index/Form 200; `GetGridJson` trả dòng `HospAdministrator` | Desktop (curl) | Đọc OK. Ghi (tạo/sửa/xóa role, gán user): KHÔNG thử (xem §8). |
| Người dùng | `UserController` (+ `SysUserBaseController`): `GetPagintionGridJson`, `GetSysUserSelectorTree`, `UpdateUserRole`, `Selector`, `UserRoles` | Trang Index/Selector/UserRoles 200 (curl) | Desktop (curl) | Đọc OK. Ghi (đổi role user): KHÔNG thử (xem §8). |
| Thông báo | `NoticeController` (+ `SysNoticeBaseController`): `GetNoticeList`, `NoticeStuRead` | Trang Index 200; `GetNoticeList` theo msgtype | Desktop (curl) | **A-01: inbox (msgtype=1, mặc định UI) LỖI**; hộp thư đi (2) và quản lý (999) OK. `NoticeStuRead`: KHÔNG thử (ghi). |
| App đã đăng ký | `CommonController` → `SystemBaseController.GetRegAppList` | Curl qua Center | Desktop (curl) | Prod (qua 8015) và staging (qua 18015 khi Center-stg chạy) đều trả 11 app. Khi Center-stg chết: lỗi `Connection refused (127.0.0.1:18015)` (xem §6). |

### 2.2 MRQC Web (5220 prod / 15220 staging) + MRQC API (5320 prod)

| Nhóm chức năng | Controller / Action | Kiểm thử | Viewport | Kết quả |
|---|---|---|---|---|
| Đăng nhập / phiên | `LoginController` (MRQC) → BaseAPI CheckPwd | Browser + curl CheckLogin | Desktop, mobile 390 | OK. No-cookie → 302 (prod, đã kiểm chứng đầu sweep). |
| Trang chủ | `HomeController.Index`, `MessageBox` | Curl 200 + quét CJK | Desktop (curl) | 200, 0 CJK. Home là trang chủ thật tiếng Việt (link tới các chức năng), không còn stub tạm. |
| Trung tâm kiểm soát (MedicalCenter) | `MedicalCenterController`: `Index`, `GetPatList`, `GetMedicalCenterDiag`, `GetMedicalCenterBlTree`, `GetDoctorsAdviceRecord`, `GetInspectionExaminationRecore`, `GetTyzkxm`, `Getybbxbldata`, `MessageQuery`, `MessageSendQuery`, `GetDclMsgJson`, `SendMsg`, `SaveScoreDate`, `ScoreFrom`, `QcMessageManage`, `PreView`, `MoreHandleDCWriterServicePage` | Trang 200 (prod + staging); endpoint đọc prod; ghi SaveScoreDate staging; browser mobile + desktop (tìm kiếm) | Desktop, mobile 390 | Đọc OK (GetPatList trả 1 dòng demo; Diag/BlTree/Tyzkxm trả dữ liệu). SaveScoreDate staging OK + dọn sạch. SendMsg: chỉ kiểm chứng đường lỗi khi Center-stg chết; bản ghi thành công KHÔNG thử (xem §8). PreView: trang 200, editor DCSoft Windows-only không test được (xem §8). Lưới tự tải: A-06 chưa kết luận. |
| Đăng ký bệnh án (Apply) | `ApplyController`: `Index`, `GetBlApplyList`, `ApprovalApply` | Trang 200; `GetBlApplyList` đọc (prod + staging) | Desktop (curl) | Đọc OK (prod trả cả dòng `PILOT-DEMO-003` — chỉ đọc, không xóa; việc xóa PILOT-DEMO thuộc chương trình chung, không phải worker A). `ApprovalApply` (duyệt): KHÔNG thử (xem §8). |
| Hạng mục QC (SysItem) | `SysItemController`: `Index`, `Form`, `GetQcBllxMbTree`, `GetZkxmList`, `GetItemMXData`, `SubmitForm`, `ZkDetailDel` | Trang 200; chu trình ghi staging đầy đủ | Desktop (curl) | **Ghi OK, đã dọn sạch:** tạo Id 1002 → đọc thấy → xóa → đọc lại rỗng, danh sách về 2 dòng. Ghi chú: `Code` gửi lên bị server ghi đè thành `3` (xem §5); `GetZkxmList?keyword=` lọc theo `BlmbId`, không phải `Name` (xem §5). |
| Nhân sự theo chức vụ | `SystemManage/CommonController.GetStaffListByDutyCode` | Không gọi riêng | — | Không test (xem §8). |
| MRQC API — token | `AuthController.GetToken` | Curl prod: sai tài khoản, thiếu field | — | Fail-closed OK (xem §3). Token thật hợp lệ: đã kiểm chứng ở phase 2, sweep này không cấp token prod (tránh tạo token thật trên prod). |
| MRQC API — truy vấn | `DefaultController.Query` (+ `Get`/`Post`/`Put`/`Delete` chuẩn) | Curl prod no-token / thiếu Timestamp | — | No-token → `TOKEN_IS_REQUIRED`; thiếu Timestamp → `TIMESTAMP_IS_REQUIRED` trước cổng token (xem §3). `Get`/`Post`/`Put`/`Delete`: không thử riêng (xem §8). |

### 2.3 Center — AuthCenter (8015 prod / 18015 staging) + WebAPI.Manage (8016 prod / 18016 staging)

| Nhóm chức năng | Controller / Route | Kiểm thử | Kết quả |
|---|---|---|---|
| Cấp token app | `AuthController.GetAppToken` (8015), `GetAppFrienAuthToken` (8015), `AuthController.GetAppToken/GetUserToken` (8016) | Curl prod: AppId/Data bogus; staging: không gọi lại GetAppToken staging | Prod 8015: `40004 “Không tìm thấy ứng dụng đích”`, KHÔNG echo secret (fix CTR-01 đã live). `GetAppFrienAuthToken` và `GetUserToken` (8016): không thử riêng (xem §8). |
| Đăng nhập Center / JWT | `LoginController.UserLogin` (8015), `AuthController.UserTokenRefresh` (8015) | Curl prod validation (Data null); curl staging đầy đủ (đúng + sai mật khẩu) | Prod validation: `40004 “Đăng nhập thất bại,Vui lòng nhập tên người dùng và mật khẩu”`. Staging: đúng → `10000` + JWT; sai → `40004 “Mật khẩu không đúng…”`, không JWT. `UserTokenRefresh`: không thử (xem §8). |
| NoticeJob (HisJob) | `NoticeJobController`: `NoticeImmediatelyWaitProc`, `NoticeUnReadProc` (đọc, AllowAnonymous); `NoticeProcSuccess`, `NoticeProcFailed`, `NoticeSendSuccess`, `NoticeSendFailed` (đổi trạng thái, AllowAnonymous); `NoticeRead`, `NoticeListRead` (cần auth) | Curl staging đầy đủ: cổng thiếu field (4 trường hợp), đổi trạng thái dòng test thật + đối chiếu DB + dọn sạch; curl prod: chỉ cổng AppId (không dùng QueueId thật trên prod) | **Cổng Data HOẠT ĐỘNG; đổi trạng thái KHÔNG JWT vẫn thành công (A-10, quyết định còn mở).** Route cần auth: no-JWT → 401. Đọc: `NoticeImmediatelyWaitProc` → 10000, data null (không có dòng Wait). `NoticeProcFailed`/`NoticeSendFailed`/`NoticeListRead`/`NoticeUnReadProc`: không gọi riêng (xem §8). |
| Điểm QC qua Center | `MRQCScoreController.GetMrqcScore`, `MedicalRecordApplyController.AddMedicalApplyRecord` | Curl prod no-token | No-token → 401 (đúng). Có-token / ghi thật: không thử (xem §8). |
| Hệ thống qua Center | `AppManageController` (ApplicationQuery/Add/Del), `BaseDataController` (GetChargeCategory/Medicine/ChargeItem/Material + Page), `MsgQueueController` (NewNoticeGroup/GetNoticeGroup/NewNotice/GetNoticeInfo/NoticeQuery), `SysOrgController.GetOrgList`, `SysUserController` (GetUserList đã comment-out trong code) | Curl prod no-token (ApplicationQuery, GetChargeCategory, NoticeQuery, GetOrgList) | No-token → 401 (đúng). Có-token / ghi: không thử (xem §8). |
| File | `FileController`: UploadFile, WebUploaFile, DrgExcelUpload, FileDelete | Không thử | Không test (xem §8 — upload/delete file không thuộc phạm vi sweep A và rủi ro ghi file). |
| Thông báo SignalR | `SysNoticeController.PushMsg`, `getmsg/{nick}/{id}` | Curl prod no-auth | Cả hai → 401 `Xác thực token thất bại` (đã chặn, khác với HOSP getmsg của worker E — xem §9, không nhầm lẫn). |
| Các area proxy khác qua 8016 | CIS (MedicalDoctorsAdvice, OutpMedicalRecord, RemoteTreated), EMR (MedicalApplyHandle, MedicalBl, MedicalHome), DRGS (DRGGrouper), ExtClinic, LIS, PACS, PDS (DrugStorage), PatientCenter (PatBasicInfo), Sett (Booking, InHosPatientService, OutpatientService, Patient), OrderCenter (PayOrder) | Liệt kê route từ source (xem §2.3 cuối) | Chỉ liệt kê + cổng no-token spot-check ở nhóm System/MRQC. Kiểm thử sâu thuộc worker app tương ứng (Sett-API: SWEEP-B đã xong theo thông báo điều phối viên). Worker A KHÔNG tự kiểm thử sâu các area này. |

Route proxy 8016 đã liệt kê từ source (để đối chiếu, không phải kết quả test):
CIS: GetDoctorAdviceRecore, GetInspectionExaminationRecore, GetPrescriptionData,
RemoteTreatedApplyZfOnly, DoctorMeetingApply, UserJoinMeetingApply, TreatedApplyInfo,
TreatedApplyRefused. EMR: ModifyApplyApprove, MedicalbllxRecord, MedicalblmbRecord,
MedicalbllxTreeRecord, MedicalCenterPatInfo, MedicalCenterDiaglist, MedicalCenterBlwsTree,
MedicalHomeRecord, MedicalHomeRecordFormtDrg. DRGS: DrgGroupByMedRecord,
DrgGroupByMedRecordList, DrgGroupByXls. ExtClinic: GetClinicToken,
GetClinicPatMedicalRecord, SendHisMedicalRecord, SendHisOutpPrescriptionData,
TreatedApplyResult. LIS (`api/lis`): HisDeptQuery, HisDoctorQuery, HisDiagnoseQuery,
HisFeeItemQuery, HisOperationQuery, HisPatientInfoQuery, HisUserDeptQuery, LisDeptQuery,
LisDoctorQuery, LisFeeItemQuery. PACS (`api/pacs`): PacsDeptQuery, PacsDocQuery,
PacsFeeitemQuery, PACSPatientReportV2. PDS: GetDepartmentDrugs, GetDepartmentDrugsPage,
UpdateMedicationStatus. PatientCenter (`api/patient`): PatientQuerySelfPay,
PatientAddressQuery/Update/Add/Delete, PatientOrderAddressQuery. Sett: GetOutpDepartment,
GetOutpDoctor, GetOutSchedule(+Page), PatBookListQuery, PatBookDetailQuery,
ScheduleBookApply/Cancel, BookOutpatientRegist, OutpatientRegist. OrderCenter:
OrderQuery, OrderDetail, BillOrderCreate, BillOrderLockApply, BillOrderPay.

## 3. Bug và phát hiện (verify-only — KHÔNG sửa)

### A-01 [MỚI, CAO] — SSO Notice: lưới hộp thư đến luôn lỗi trên prod

- App/trang/action: SSO portal, `/SysManage/Notice/Index` → `GetNoticeList`
  (viewport: endpoint, đã kiểm chứng bằng curl với session prod thật).
- Tái hiện: đăng nhập SSO prod → POST `/SysManage/Notice/GetNoticeList` với
  `limit=5&offset=0` và `queryParams[msgtype]=1` (đúng giá trị mặc định của select
  `#msgtype` = “hộp thư đến” trên trang), hoặc không gửi `queryParams` (VO mặc định
  cũng có `msgtype = 1` — xem `MsgNoticeQueueQueryVO.msgtype { get; set; } = 1`).
- Mong đợi: `{"rows":[...],"total":N}`.
- Thực tế (3 lần, prod): `{"state":"error","message":"The SqlFunc.IIF(arg1,arg2,arg3) ,
  a.Recipient.Contains(value(NewtouchHIS.Base.DomainService.MsgQueueDmnService+<>c__DisplayClass10_0).request.Recipient)
  argument do not support ","data":null,"code":null,"exStackTrace":null}`.
- Đối chứng: `msgtype=2` (“hộp thư đi”) → `{"rows":[],"total":0}` OK;
  `msgtype=999` (“quản lý tin nhắn”, admin) → `{"rows":[],"total":0}` OK.
- Căn nguyên (đọc code): `MsgQueueDmnService.NoticeQueryPage` dòng ~411:
  `a.RecipientType == (int)RecipientTypeEnum.user ? a.Recipient == request.Recipient
  : a.Recipient.Contains(request.Recipient)` — toán tử 3 ngôi trong biểu thức
  SqlSugar không dịch được (SqlFunc.IIF + Contains). Nhánh này chỉ được thêm khi
  `Recipient` khác rỗng, tức đúng đường inbox (controller gán
  `Recipient = UserIdentity.UserCode` khi `msgtype == 1`).
- Ảnh hưởng: trang Notice mở lên 200 nhưng lưới hộp thư đến (mặc định) không bao
  giờ nạp được dữ liệu trên prod. Lỗi JSON còn lộ tên kiểu nội bộ
  (`MsgQueueDmnService+<>c__DisplayClass10_0`) — rò rỉ thông tin nhẹ, nhưng lần này
  `exStackTrace` là null (không lộ stack như các lỗi khác).
- Không phải lỗi do probe sai: đã đối chứng 2/999 OK trên cùng session prod.

### A-02 [MỚI, CAO] — SyncForm bước 2: menu đồng bộ không nằm dưới menu cha đã chọn, menu con trỏ sai cha

- App/trang/action: SSO portal, `/SysManage/SysModule/SyncForm` → `SubmitMenuSync`
  (viewport: endpoint trên SSO-stg 15208 + đối chiếu DB STG).
- Tái hiện (STG): chọn nguồn `MRQC` (5 menu chưa đồng bộ, gồm gốc
  `20d5a18a-…` “Kiem soat chat luong Quan ly” + 4 con) → bước 2 đặt tên menu cha
  mới `SWEEP-A-SYNC-PARENT` (không chọn node cha có sẵn) → bấm Hoàn thành.
  (Do trình duyệt sập vì tải hệ thống, lượt này gọi đúng endpoint `SubmitMenuSync`
  với đúng payload form của UI — `list[]` là 2 dòng: gốc + con
  `24a940d0-…` “Trung tam kiem soat chat luong”, `fromAppId=MRQC` — phương pháp
  được ghi rõ, không giấu.)
- Mong đợi: cây Union (portal) có thêm node cha `SWEEP-A-SYNC-PARENT`, dưới nó là
  bản sao cây MRQC (gốc sao chép → con sao chép).
- Thực tế (DB `Newtouch_Union_STG.Sys_Module`, đã đối chiếu): response
  `{"state":"success"}` và tạo đúng 3 dòng mới lúc 08:20:14–08:20:16:
  1. `573fbd7b-…` `SWEEP-A-SYNC-PARENT`, `ParentId = NULL` (đúng là node mới);
  2. `bf1277c1-…` bản sao gốc MRQC, `ParentId = NULL` — **không nằm dưới node cha
     mới** (đáng lẽ phải là `573fbd7b-…`);
  3. `99361387-…a` bản sao con MRQC, `ParentId = 20d5a18a-…` — **là Id GỐC của
     MRQC** (chỉ tồn tại ở `Newtouch_MRQC_STG`, không tồn tại ở Union) — tức menu
     con trỏ tới một cha không tồn tại trong DB portal (dangling).
- Căn nguyên (đọc code `SysModuleDmnService.AddRange`): dòng con được gán
  `c.ParentId = parentMenu?.FirstOrDefault(a => a.Name == thisParent.Name)?.Id
  ?? defaultParentId`, trong đó `parentMenu` là danh sách VO **trước khi** đổi Id
  mới — nên `.Id` trả về vẫn là Id gốc của nguồn, không phải Guid mới của bản sao
  cha vừa tạo (`pEntityList`). Còn dòng cha cấp cao nhất không bao giờ được gán
  `defaultParentId` (code chỉ dùng `defaultParentId` cho dòng con khi cha không có
  trong danh sách gửi lên). Vì vậy luồng UI bình thường (chọn cả cây cha+con, đặt
  tên cha mới hoặc chọn node cha có sẵn) cũng sẽ tạo ra cùng cấu trúc sai — đây
  không phải lỗi do payload curl.
- Tách bạch rõ: tên bản sao trong DB có dạng `Kiem%20soat%20…` (chứa `%20`) là
  **lỗi double-encode của harness curl của worker A** (đã pre-encode rồi curl lại
  encode lần nữa), KHÔNG phải lỗi app — luồng UI thật gửi tên có khoảng trắng
  bình thường. Lỗi liên kết (ParentId) thì độc lập với việc encode tên.
- Dọn dẹp: đã xóa đúng 3 Id trên khỏi Union_STG (lưu ý: lần xóa đầu thiếu ký tự
  cuối của Id dòng con nên sót lại 1 dòng; đã phát hiện và xóa bổ sung, kiểm chứng
  cuối `COUNT = 0` cho cả 3 Id và `Name LIKE '%SWEEP-A%'`). Dòng nguồn MRQC_STG
  còn nguyên 2/2. STG sạch.

### A-03 [CÒN TỒN, TRUNG BÌNH] — Footer tiếng Trung trên trang chủ SSO và trang login MRQC

- Chuỗi còn lại (quét HTML server, prod + staging giống hệt nhau):
  `Copyright © 2025湖北天天数链技术有限公司` và `《天天开源软件（社区版）许可协议》`
  trên `SSO /Home/Index` và `MRQC /Views/Login/Index.cshtml`.
- Đây KHÔNG thuộc ngoại lệ đã ghi (tên cột SQL, tên proc, font, mã nội bộ) — là
  tên công ty + tên giấy phép của vendor trong footer hiển thị cho người dùng.
  Cần quyết định ở vòng fix (đổi footer hay giữ nguyên văn bản pháp lý gốc).
  Tất cả trang SysManage của SSO và mọi trang MRQC còn lại: 0 chuỗi Trung nhìn
  thấy (HTML). Xem thêm §4 về các chuỗi Trung được chấp nhận là ngoại lệ.

### A-04 [CÒN TỒN, TRUNG BÌNH] — SSO-07 mobile: carousel vẫn chồng lấn, sidebar chỉ còn dải icon

- Bằng chứng: ảnh `sweepA-sso-portal-mobile.png`, `sweepA-sso-workspace-mobile.png`
  (390×844, prod, xem §7).
- Portal mobile: sidebar trái chỉ còn dải tối hẹp với nút `<<` (không dùng được);
  thanh tab trên cùng bị cắt (`trang chủ`, `thao tác tab`, `hệ thố…`); cụm badge
  carousel phía trên chồng lấn nhau; footer tiếng Trung bị ép thành dải dọc mép trái.
- WorkSpace mobile: carousel trên cùng vẫn chồng lấn (3 badge `hệ thống…` đè nhau,
  nút mũi tên ở mép) — phần “cần cấu hình lại carousel, không phải sửa CSS nhỏ”
  từ phase 2 vẫn đúng. Đã cải thiện so với trước: không còn đè chữ dọc kiểu
  line-height 100px (media query WorkSpace có tác dụng); bảng thống kê không tràn
  trang (`scrollWidth == clientWidth == 390` ở WorkSpace) nhưng các cột vẫn bị cắt
  trong khung chụp (cần vuốt ngang trong bảng).
- Overflow đo được: portal `sw=393/cw=390` (tràn 3px, không đáng kể); WorkSpace
  `sw=390/cw=390`.

### A-05 [MỚI, TRUNG BÌNH-THẤP] — MRQC-05 mobile: form lọc sạch, lưới trống, tràn ngang nhẹ

- Bằng chứng: ảnh `sweepA-mrqc-medicalcenter-mobile.png` (390×844, prod).
- Form lọc hiển thị tốt, tiếng Việt, không chồng lấn: radio “Bệnh nhân đang nằm
  viện / đã xuất viện / Khám ngoại trú”, 2 ô ngày, ô “Họ tên / Số nhập viện” + nút
  tìm kiếm.
- Lưới: chỉ có header (`Khoa | Số giường | Họ tên | Giới tính | Tuổi | Ng…`), thân
  lưới trống khi mở trang (liên quan A-06). Tràn ngang đo được `sw=405/cw=390`
  (15px) — lưới hơi rộng hơn viewport.

### A-06 [CHƯA KẾT LUẬN] — MRQC-03: lưới MedicalCenter có tự tải khi mở trang không?

- Endpoint `GetPatList` (prod, tham số rộng 2020-01-01→2026-10-28) trả đúng 1 dòng
  demo (`Zyh=01000`, `Xm=A Binh`, `DeptCode=KS01`).
- Trang mở lên (mobile + desktop, prod): thân lưới trống.
- Một lần bấm nút tìm kiếm trên desktop (nút `fa-search` đầu tiên trong 3 nút giống
  hệt nhau trên trang) không nạp dòng; ảnh `sweepA-mrqc-medicalcenter-search.png`
  cho thấy panel trái bị ép hẹp, panel phải trống.
- **Không kết luận** vì chưa xác định được nút đã bấm có phải nút tìm kiếm chính
  của lưới không (3 nút giống nhau, selector chưa phân biệt). Cần một lượt kiểm
  chứng UI riêng với selector nút chính xác + đếm dòng của đúng bảng lưới (trước
  đó worker A đếm nhầm bảng khác được 16 dòng). Không báo “lưới hỏng” khi chưa đủ
  bằng chứng.

### A-07 [THẤP] — `GetTreeGridJson` với tham số thiếu: NRE + lộ stack trace (staging)

- Tái hiện (staging, session thật): POST `/SysManage/SysModule/GetTreeGridJson` chỉ
  với `limit/offset`, không có object `queryParams` → `{"state":"error","message":
  "Object reference not set to an instance of an object.","exStackTrace":"   at
  NewtouchHIS.Framework.Web.Controllers.SysManage.SysModuleBaseController.GetTreeGridJson(...)
  in /home/hatch/workspace/openhis-stg/.../SysModuleBaseController.cs:line 156 ..."}`
  (lộ đường dẫn source server).
- Đường UI (đủ `queryParams`) hoạt động bình thường (trả cây menu). Đây là lỗi
  robustness + rò rỉ stack khi input thiếu field, cùng họ với các lỗi stack trace
  đã sửa ở phase 7 cho API — nhưng ở đây là endpoint Web MVC. Prod CHƯA kiểm
  chứng trường hợp này (không gửi tham số hỏng lên prod khi chưa cần), ghi rõ là
  staging-only.

### A-08 [THẤP, HỌ ĐÃ BIẾT API-13/CTR-07] — Thông điệp lỗi kết nối DB chứa tiếng Trung + ConfigId (staging, lúc SQL chập chờn)

- Trong cửa sổ SQL chết/hồi, 1 lần gọi `GetAppMenuTreeGridJson` (staging) trả:
  `{"state":"error","message":"中文提示 : 连接数据库过程中发生错误，检查服务器是否正常连接字符串是否正确，…DbType=\"SqlServer\";ConfigId=\"UnionDb\"",
  "exStackTrace":"   at SqlSugar.Check.Exception... "}` (kèm stack SqlSugar).
- Gọi lại sau khi SQL hồi thì thành công (5 dòng MRQC) — lỗi này là nhất thời do
  SQL, nhưng **mẫu thông điệp** (template tiếng Trung của SqlSugar + ConfigId +
  stack) là có thật và người dùng cuối có thể thấy khi DB lỗi. Thuộc họ lỗi đã
  biết ở API; lần này quan sát được trên endpoint Web.

### A-09 [THÔNG TIN] — Các quan sát nhỏ khác (không kết luận là bug)

- `SysItem.SubmitForm` (staging): gửi `Code=SWEEP-A-001` nhưng dòng tạo ra có
  `Code='3'` (server tự đánh số tuần tự, trường Code gửi lên bị ghi đè/bỏ qua).
  UI Form có thể vốn không gửi Code — vòng fix xác nhận hành vi mong muốn.
- `SysItem.GetZkxmList?keyword=` lọc theo `BlmbId` (code dòng 68:
  `p.BlmbId == keyword`), không lọc theo `Name` — nên `keyword=SWEEP-A` trả rỗng
  dù `Name` chứa SWEEP-A. UI có thể vốn truyền BlmbId vào đây; chỉ là tên tham số
  gây nhầm, không kết luận bug.
- `SaveScoreDate` (đọc code `QcBlzkDmnService`): xóa toàn bộ điểm cũ của
  (zyh, bllx, org) rồi insert lại — là ngữ nghĩa “lưu đè toàn bộ” theo thiết kế,
  không phải bug. Lượt test dùng `bllx` mới nên không chạm dữ liệu điểm cũ
  (zyh 01000 trước/sau đều 1 dòng cũ).
- Panel trái MedicalCenter trên desktop 1366 (ảnh search): bị ép hẹp, header bị
  cắt — có thể là vị trí mặc định của splitter, chưa tương tác splitter nên không
  kết luận là bug.

### A-10 [QUYẾT ĐỊNH CÒN MỞ — GHI NHẬN HIỆN TRẠNG, KHÔNG TỰ QUYẾT] — CTR-05 NoticeJob không JWT

- Quyết định đã ghi (qa-phase2/api-auth.md §6): **KHÔNG tự gắn auth** (sẽ phá luồng
  worker-to-worker); phương án cần quyết định là (a) giới hạn network 8016
  NoticeJob (chỉ worker host nội bộ được tới) hoặc (b) shared secret/header nội bộ.
  Worker A thực thi đúng cổng đã quyết, không bịa quyết định mới.
- Kiểm chứng lại trên staging 18016 (dòng test `SWEEP-A-NOTICE-001`,
  `NoticeStu=Send=1`, `zt='1'`, org demo, bảng `Msg_NoticeQueue` STG ban đầu rỗng):
  - Cổng AppId (filter): thiếu AppId → `40004 “Id ứng dụng (AppId) bắt buộc phải
    truyền”`.
  - Cổng Data (action): thiếu OrganizeId, hoặc `Data=null`, hoặc `Data={}` (thiếu
    QueueId) → `40004 “Thông tin tổ chức,Tin nhắnId không được để trống”`,
    `data=false` (3 trường hợp đều đúng cổng).
  - **Đổi trạng thái KHÔNG JWT vẫn thành công:** `NoticeProcSuccess` → `10000`,
    `data=true`, DB `NoticeStu 1→0`; `NoticeSendSuccess` → `10000`, `data=true`,
    DB `0→1`. Kết luận phase 2 còn nguyên hiệu lực trên build hiện tại.
  - Route cần auth: `NoticeRead` không JWT → `401`. Route đọc AllowAnonymous:
    `NoticeImmediatelyWaitProc` (org demo) → `10000`, `data=null` (không có dòng Wait).
  - Dọn dẹp: dòng test đã xóa, `COUNT(Msg_NoticeQueue) = 0` trở lại.
- `NoticeProcFailed`, `NoticeSendFailed`, `NoticeListRead`, `NoticeUnReadProc`
  không gọi riêng trong sweep này (cùng họ AllowAnonymous/auth với các route đã
  kiểm chứng; ghi rõ là chưa gọi).

## 4. Kiểm chứng UI/UX nhất quán tiếng Việt

- SSO: mọi trang SysManage (Module/Role/User/Notice/SyncForm) và WorkSpace: **0
  chuỗi Trung nhìn thấy** trong HTML server (prod + staging giống nhau).
  Ngoại lệ duy nhất: footer trang chủ (A-03) — không thuộc ngoại lệ đã ghi.
- MRQC: mọi trang (Home, MedicalCenter, MessageQuery, ScoreFrom, QcMessageManage,
  PreView, Apply, SysItem, MessageBox): **0 chuỗi Trung nhìn thấy** trong HTML
  server (prod). Ngoại lệ duy nhất: trang Login mang cùng footer vendor (A-03).
- Quét source `.cshtml` MRQC (để đối chiếu, không phải UI render):
  - `MedicalCenter/Index.cshtml`: `中间/开始/结束` + ảnh `结束.png` — là hằng số
    của control soạn thảo DCSoft (Windows-only), không phải chuỗi giao diện người
    dùng thường; ghi nhận là ứng viên ngoại lệ “mã nội bộ” cho điều phối viên
    quyết, worker A không tự chấp nhận.
  - `MedicalCenter/PreView.cshtml`: `宋体` (font SimSun) — thuộc ngoại lệ font.
  - `Views/Shared/Error.cshtml`: `微软雅黑` (font Microsoft YaHei) — thuộc ngoại lệ
    font.
  - `Views/Login/Index.cshtml`: footer vendor (A-03) — không phải ngoại lệ.
- Center: API trả thông điệp tiếng Việt; các lỗi còn thấy: thông điệp DB của
  SqlSugar bằng tiếng Trung khi DB lỗi (A-08) và tên kiểu nội bộ trong lỗi Notice
  (A-01) — đều không phải chuỗi giao diện thường, nhưng vẫn là tiếng Trung/kỹ
  thuật lộ ra client khi lỗi.

## 5. Kiểm chứng API/endpoint chi tiết (prod, chỉ đọc/validation)

- MRQC API 5320: `Query` không token (+Timestamp) →
  `{"code":40004,"sub_code":"TOKEN_IS_REQUIRED","sub_msg":"Token truy cập trống"}`;
  `GetToken` tài khoản không tồn tại (+TokenType) →
  `{"code":40004,"sub_code":"AUTH_FAILED","sub_msg":"Tên đăng nhập hoặc mật khẩu
  không đúng, hoặc tài khoản đã bị khóa"}`; thiếu Timestamp → `TIMESTAMP_IS_REQUIRED`
  (validation field chạy trước cổng token — thứ tự đã biết, không phải bypass).
  Token thật hợp lệ không cấp trên prod (tránh tạo token thật); phase 2 đã kiểm
  chứng GetToken thật cho các module API.
- Center 8015: `GetAppToken` với AppId/Data bogus →
  `40004 “Không tìm thấy ứng dụng đích”`, không còn nối Data/secret vào message
  (fix CTR-01 đã live trên prod). `UserLogin` thiếu Data → `40004` đúng dạng.
- Center 8016: `MRQCScore/GetMrqcScore`, `MedicalRecordApply/AddMedicalApplyRecord`,
  `AppManage/ApplicationQuery`, `BaseData/GetChargeCategory`, `MsgQueue/NoticeQuery`,
  `SysOrg/GetOrgList` (no-token) → `401 “Xác thực token thất bại”` (đúng).
  `SysNotice/PushMsg` và `SysNotice/getmsg/testuser/testid` (no-auth) → `401`
  (đã chặn; đây là SysNotice của 8016, khác HOSP getmsg của worker E — xem §9).
- Prod BaseAPI 7001 (phục vụ chẩn đoán, không chạm demo): CheckPwd tài khoản không
  tồn tại → `40004 “Không tìm thấy thông tin người dùng”` (1 lần đo được
  `timeOut=148891ms` trong cửa sổ tải cao — bằng chứng backend chậm, không phải
  lỗi logic).

## 6. Phụ thuộc hạ tầng staging (không phải lỗi app, nhưng ảnh hưởng kiểm chứng)

- SSO-stg `GetRegAppList` gọi AuthCenter-stg 18015: khi 18015 chết →
  `{"state":"error","message":"Connection refused (127.0.0.1:18015)"}`. Khi 18015
  lên, trả đủ 11 app. Prod không bị (8015 luôn lên).
- MRQC-stg `GetPatList` gọi 18015: khi 18015 chết →
  `{"state":"error","message":"Connection refused (127.0.0.1:18015)"}` + stack.
  Khi 18015 lên, trả đúng 1 dòng demo (giống prod).
- MRQC-stg `SendMsg` cần 18016 (lấy mã định danh Api): khi 18016 chết →
  `{"state":"error","message":"[Xác thực] Lỗi khi lấy mã định danh Api:Connection
  refused (127.0.0.1:18016)"}`.
- Kết luận: cụm staging tối thiểu cho đường MRQC-stg MedicalCenter phải gồm cả
  Center-stg (18015+18016); cụm staging để chạy sẵn trước sweep (15208/17001/6380)
  là chưa đủ cho các đường này. Prod không bị vì 8015/8016 luôn lên.

## 7. Ảnh đã chụp (viewport emulation, KHÔNG app native)

Tất cả trong `~/workspace/openhis-core/shots-sweep/` (thư mục này đã nằm trong
`.gitignore` qua mẫu `shots*/` — ảnh sẽ KHÔNG lọt vào commit):

| Ảnh | Nội dung / kết luận |
|---|---|
| `sweepA-sso-portal-mobile.png` (390×844, prod) | Portal SSO mobile: sidebar dải icon, tab cắt, carousel chồng lấn, footer bị ép dọc (A-04). |
| `sweepA-sso-workspace-mobile.png` (390×844, prod) | WorkSpace mobile: carousel chồng lấn, link truy cập nhanh + date picker tốt, bảng thống kê bị cắt ngang (A-04). |
| `sweepA-mrqc-medicalcenter-mobile.png` (390×844, prod) | MedicalCenter mobile: form lọc sạch, lưới trống khi mở, tràn ngang 15px (A-05, A-06). |
| `sweepA-mrqc-medicalcenter-search.png` (1366×900, prod) | MedicalCenter desktop sau 1 lần bấm nút tìm kiếm (chưa xác định đúng nút): panel trái ép hẹp, panel phải trống (A-06). |

Ảnh spinner cũ `sweepA-sso-portal-mobile.png` lúc 07:41 (chỉ có vòng xoay tải trang,
do lượt chạy trình duyệt sập) đã bị ghi đè bằng ảnh portal thật lúc 08:57.
Không có ảnh nào tuyên bố là “app native” — OpenHIS không có app native, tất cả
đều là emulation viewport trên Firefox local.

## 8. Phần không tái hiện được / không test được + lý do

- **Trình duyệt render lưới SSO (SysModule/SysRole/User/Notice) và trang MRQC
  desktop:** tab Firefox crash/timeout liên tục trong khung tải cao (xem §1.4).
  Bù bằng curl trang 200 + endpoint JSON với session thật. Không tuyên bố đã xem
  các lưới này bằng trình duyệt.
- **SyncForm bước 1 bằng trình duyệt (cây 27 node, gõ tìm kiếm không xóa cây):**
  đã kiểm chứng ở phase 2 (staging), sweep này chỉ kiểm chứng lại trang 200 +
  endpoint lưới qua curl (trình duyệt sập trước khi tới SyncForm trong 2 lượt chạy
  staging/prod). Bước 2 thì kiểm chứng đầy đủ bằng endpoint + DB (§ A-02).
- **MRQC-03 (A-06):** chưa kết luận (lý do ở A-06).
- **Ghi chưa thử trên STG:** `SysRole.SubmitForm/DeleteForm`, `SysModule.SubmitForm/
  DeleteForm` (trực tiếp), `User.UpdateUserRole`, `Notice.NoticeStuRead`,
  `MRQC Apply.ApprovalApply`, `MRQC SendMsg` (ghi thành công), `MRQC SaveScoreDate`
  qua UI (chỉ qua endpoint — đã đủ), `Center` các route có-token/ghi
  (AppManage Add/Del, MsgQueue NewNotice*, BaseData chỉ đọc đã đủ ở mức no-token),
  `Center FileController` (upload/delete — rủi ro ghi file, ngoài phạm vi),
  `Center UserTokenRefresh`, `GetAppFrienAuthToken`, `GetUserToken` (8016),
  `NoticeJob ProcFailed/SendFailed/ListRead/UnReadProc`, MRQC API
  `Get/Post/Put/Delete`, `MedicalCenter GetDoctorsAdviceRecord/
  GetInspectionExaminationRecore/Getybbxbldata/MessageSendQuery/GetDclMsgJson`
  (endpoint tồn tại, chưa gọi riêng), `SystemManage GetStaffListByDutyCode`,
  `MRQC GetQcBllxMbTree` (cần Center EMR API — chưa gọi riêng).
  Lý do chung: hết cửa sổ backend khỏe + giới hạn app staging + tránh ghi không
  dọn được; các mục này cần lượt follow-up riêng, không được báo là “xong”.
- **PreView editor DCSoft (MRQC-01):** Windows-only, không test được trên Linux
  (quyết định deferred từ trước). Trang PreView chỉ kiểm chứng 200 + 0 CJK HTML.
- **Mobile:** chỉ có 390×844 emulation. Không có thiết bị thật, không có app
  native, không test xoay ngang/tablet.
- **Prod demo DB:** không ghi gì (không mở khóa, không sửa user, không tạo dữ
  liệu). Việc mở khóa do điều phối viên làm bằng SQL, worker A chỉ đọc trạng thái
  `Locked` để chẩn đoán.

## 9. Các quyết định mở — CHỈ ghi nhận hiện trạng, KHÔNG tự quyết hay redesign

- **Endpoint tokenless hợp lệ cần chặn ở tầng mạng:** hiện trạng trong phạm vi A
  là NoticeJob 8016 (A-10): cổng Data hoạt động nhưng đổi trạng thái không JWT vẫn
  chạy. Phương án (giới hạn network hoặc shared secret/header nội bộ) CHƯA được
  quyết — worker A không gắn auth, không dựng network rule, không redesign.
- **HOSP getmsg:** không thuộc phạm vi A (worker E). Worker A chỉ ghi nhận
  SysNotice 8016 (`PushMsg`, `getmsg`) hiện trả 401 — hai thứ khác nhau, không
  suy ra trạng thái HOSP getmsg.
- **Schema `zy_bqksby` / `zy_ksbyth`:** không gặp trong phạm vi A (không có truy
  vấn nào chạm 2 bảng này trong SSO/MRQC/Center đã kiểm chứng). Ghi nhận là chưa
  gặp, không kết luận.
- **E-11 `GetAuthedWardSelectJson`:** không gặp trong phạm vi A (không phải
  endpoint của SSO/MRQC/Center). Ghi nhận là chưa gặp.
- **Ứng viên dịch M-09 `xt_sfxm`:** không gặp trong phạm vi A (thuộc Sett). Ghi
  nhận là chưa gặp, không đề xuất dịch.
- **Tiền tố số chứng từ:** trong phạm vi SSO/MRQC/Center đã kiểm chứng, worker A
  KHÔNG gặp luồng sinh số chứng từ nào (không có ReceiptNoManage/ReceiptNoHelper/
  hardcode nào được thực thi). Theo đúng chỉ thị, ghi nhận: không áp dụng trong
  phạm vi này; không tự thống nhất tiền tố. (Hiện trạng chung đã biết từ chương
  trình: luồng ReceiptNoManage = tiền tố Việt mới, luồng ReceiptNoHelper/hardcode
  = tiền tố cũ — worker A không kiểm chứng lại, chỉ nhắc lại nguyên văn hiện
  trạng đã ghi, không sửa.)

## 10. Dọn dẹp và kiểm chứng cuối (staging + repo)

- STG đã dọn: `Msg_NoticeQueue` STG về 0 dòng (dòng `SWEEP-A-NOTICE-001` đã xóa);
  Union_STG `Sys_Module` về 0 dòng sweep (3 Id đã xóa, kiểm chứng cuối COUNT=0);
  MRQC_STG `Mr_Qc_Score` với `BllxId='SWEEP-A-BLLX'` về 0, điểm cũ của zyh 01000
  còn nguyên 1 dòng; MRQC_STG SysItem về 2 dòng (Id 2, 3 — dòng test Id 1002 đã
  xóa); nguồn menu MRQC_STG còn nguyên 2/2 dòng đã dùng.
- STG account `000000`: `Locked=0, zt=1` sau sweep (kể cả sau 1 lần thử sai mật khẩu
  có chủ đích trên UserLogin staging).
- Demo DB (prod): worker A KHÔNG ghi gì. Trạng thái cuối đọc được: `Locked=0`.
- App staging worker A đã khởi động và đã kill đúng PID: MRQC-stg 32778 (lần 1,
  kẹt D-state, chưa từng listen) và 50598 (lần 2, đã kiểm chứng xong); Center-stg
  33507/33508 (lần 1) và 51182/51183/51184 (lần 2, cùng MRQC-stg). Cổng
  15220/18015/18016 đã xuống. Còn chạy (có sẵn từ trước, không kill): SSO-stg
  15208, BaseAPI-stg 17001, Redis-stg 6380, toàn bộ prod (5208/5220/5320/8015/8016/
  7001/6379/41142).
- Repo: worker A KHÔNG sửa code, KHÔNG commit. `git status` cuối: `M
  roles-shots-progress.md` (thay đổi của điều phối viên từ trước sweep, worker A
  không chạm vào file này); `?? scripts/sweep-final/` (các báo cáo sweep, trong
  đó có file này); `shots-sweep/` đã gitignore nên ảnh không lọt commit.
- File báo cáo này: `~/workspace/openhis-core/scripts/sweep-final/sweep-A-REPORT.md`
  (file duy nhất worker A tạo trong repo, ngoài ảnh gitignore trong shots-sweep/).

## 11. Đề xuất cho vòng fix sau (không phải quyết định, chỉ là danh sách chuyển giao)

1. Sửa `NoticeQueryPage` nhánh Recipient (A-01) — tách toán tử 3 ngôi khỏi biểu
   thức SqlSugar (vd. rẽ nhánh `if` ngoài, thêm 2 `whereExp` riêng).
2. Sửa `SysModuleDmnService.AddRange` (A-02) — map lại ParentId của dòng con sang
   Guid mới của bản sao cha (dùng `pEntityList`), và gán `defaultParentId` cho
   dòng cha cấp cao nhất khi có cha được chọn/tạo.
3. Quyết định footer vendor (A-03): giữ nguyên văn bản pháp lý gốc hay dịch/thay
   bằng tên bệnh viện thật (phụ thuộc dữ liệu thật của bệnh viện — chưa có).
4. Quyết định CTR-05 (A-10): giới hạn network 8016 NoticeJob hoặc shared
   secret/header nội bộ — vẫn đang chờ quyết định.
5. Lượt follow-up UI riêng cho MRQC-03 (A-06) với selector nút tìm kiếm chính xác.
6. Lượt follow-up cho các mục ghi chưa thử ở §8 (role/user/module ghi trực tiếp,
   Apply duyệt, SendMsg ghi, File, UserTokenRefresh…).
7. Xem xét watchdog/giám sát cho SQL Server (chết 3 lần trong 1 sweep, mỗi lần
   làm toàn bộ login sập theo) — quyết định hạ tầng, không thuộc worker A.
